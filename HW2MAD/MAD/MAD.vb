''' <summary>
''' Class representing a Homeworld2 MAD file.
''' </summary>
Public NotInheritable Class MAD
    ''' <summary>Name of MAD file.</summary>
    Private Const MAD_Name As String = "Homeworld2 MAD File"

    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Frames per second.</summary>
    Private m_FPS As Integer

    ''' <summary>Strings chunk.</summary>
    ''' <remarks>Only used as a helper in the read\write algorithms.</remarks>
    Private m_STRI As String

    ''' <summary>Animations.</summary>
    Private m_Animations As New EventList(Of Animation)

    ''' <summary>Animation curves.</summary>
    ''' <remarks>Only used as a helper in the read\write algorithms.</remarks>
    Private m_AnimationCurves As New EventList(Of AnimationCurve)

    ''' <summary>HOD.</summary>
    ''' <remarks>Only used as a helper in the read\write algorithms.</remarks>
    Private m_HOD As HOD.HOD

    ' ------------------------
    ' Constructors\Finalizers.
    ' ------------------------
    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    Public Sub New()
        Initialize()
    End Sub

    ''' <summary>
    ''' Copy contructor.
    ''' </summary>
    Public Sub New(ByVal m As MAD)
        m_FPS = m.m_FPS
        m_STRI = ""
        m_HOD = m.m_HOD

        For I As Integer = 0 To m.m_Animations.Count - 1
            m_Animations.Add(New Animation(m.m_Animations(I)))

        Next I ' For I As Integer = 0 To m.m_Animations.Count - 1

        m_AnimationCurves.Clear()
    End Sub

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns\Sets number of frames per second.
    ''' </summary>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>value</c> is not positive.
    ''' </exception>
    ''' <remarks>
    ''' The function of this field is unknown as all animation
    ''' time units are in seconds (and not in frames).
    ''' </remarks>
    Public Property FPS() As Integer
        Get
            Return m_FPS
        End Get

        Set(ByVal value As Integer)
            If (value <= 0) Then _
                Throw New ArgumentOutOfRangeException("value") _
                    : Exit Property

            m_FPS = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the animations in the HOD.
    ''' </summary>
    Public ReadOnly Property Animations() As IList(Of Animation)
        Get
            Return m_Animations
        End Get
    End Property

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Reads a Homeworld2 MAD file from a stream.
    ''' </summary>
    ''' <param name="stream">
    ''' The underlying stream to use.
    ''' </param>
    ''' <param name="HOD">
    ''' The HOD which this file will animate.
    ''' </param>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>stream Is Nothing</c>.
    ''' </exception>
    Public Sub Read(ByVal stream As IO.Stream, ByVal HOD As HOD.HOD)
        Dim IFF As IFF.IFFReader = Nothing

        If stream Is Nothing Then _
            Throw New ArgumentNullException("stream") _
                : Exit Sub

        If HOD Is Nothing Then _
            Throw New ArgumentNullException("HOD") _
                : Exit Sub

        ' Prepare a new IFF reader.
        IFF = New IFF.IFFReader(stream)

        ' See if stream is valid.
        If IFF Is Nothing Then _
            Exit Sub

        ' Initialize the MAD.
        Initialize()

        ' Set the HOD.
        m_HOD = HOD

        ' Add handlers.
        IFF.AddHandler("MAD ", Homeworld2.IFF.ChunkType.Form, AddressOf ReadMADChunk)

        ' Finally, read the file.
        IFF.Parse()

        ' Now prepare file after import.
        For I As Integer = 0 To m_Animations.Count - 1
            m_Animations(I).UpdateReferences(m_AnimationCurves)

        Next I ' For I As Integer = 0 To m_Animations.Count - 1

        ' Remove all animation curves; they are no longer needed.
        m_AnimationCurves.Clear()

        ' Clear the HOD field, it is no longer needed.
        m_HOD = Nothing
    End Sub

    ''' <summary>
    ''' Writes a Homeworld2 MAD file to a stream.
    ''' </summary>
    ''' <param name="stream">
    ''' The underlying stream to use.
    ''' </param>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>stream Is Nothing</c>.
    ''' </exception>
    Public Sub Write(ByVal stream As IO.Stream)
        Dim STRIPosition As Integer = 0

        If stream Is Nothing Then _
            Throw New ArgumentNullException("stream") _
                : Exit Sub

        ' Prepare new IFF writer.
        Dim IFF As New IFF.IFFWriter(stream)

        ' Prepare for export.
        _PrepareMADForExport()

        ' Push in the topmost, MAD chunk.
        IFF.Push("MAD ", Homeworld2.IFF.ChunkType.Form)

        ' Write the VERS chunk.
        IFF.Push("VERS")
        IFF.WriteInt32(&H104)
        IFF.Pop()

        ' Write the NAME chunk.
        IFF.Push("NAME")
        IFF.Write(MAD_Name, MAD_Name.Length)
        IFF.Pop()

        ' Write the INFO, STRI, MARK, CURV chunks.
        WriteINFOChunk(IFF)
        WriteSTRIChunk(IFF)
        WriteMARKChunk(IFF, STRIPosition)
        WriteCURVChunk(IFF, STRIPosition)

        IFF.Pop() ' MAD 

        ' Clear temp data.
        m_STRI = ""
        m_AnimationCurves.Clear()
    End Sub

    ''' <summary>
    ''' Generates data needed to write the MAD file.
    ''' </summary>
    Private Sub _PrepareMADForExport()
        Dim SB As New Text.StringBuilder

        ' Make a list of animation curves to be written
        ' and record their names.
        For I As Integer = 0 To m_Animations.Count - 1
            ' Prepare for export.
            m_Animations(I).PrepareAnimationBeforeExport()

            ' Append to SB.
            SB.Append(m_Animations(I).ToString())
            SB.Append(Chr(0))

            ' Append joint names.
            For J As Integer = 0 To m_Animations(I).Joints.Count - 1 '
                ' Get the joint.
                Dim aj As AnimatedJoint = m_Animations(I).Joints(J)

                ' Write it's name.
                SB.Append(aj.ToString())
                SB.Append(Chr(0))

                ' Process all of it's channels and dump them in the animation
                ' curves list if exported.
                ' Translate X
                If aj.TranslateX.Keyframes.Count <> 0 Then _
                    m_AnimationCurves.Add(aj.TranslateX)

                ' Translate Y
                If aj.TranslateY.Keyframes.Count <> 0 Then _
                    m_AnimationCurves.Add(aj.TranslateY)

                ' Translate Z
                If aj.TranslateZ.Keyframes.Count <> 0 Then _
                    m_AnimationCurves.Add(aj.TranslateZ)

                ' Rotate X
                If aj.RotateX.Keyframes.Count <> 0 Then _
                    m_AnimationCurves.Add(aj.RotateX)

                ' Rotate Y
                If aj.RotateY.Keyframes.Count <> 0 Then _
                    m_AnimationCurves.Add(aj.RotateY)

                ' Rotate Z
                If aj.RotateZ.Keyframes.Count <> 0 Then _
                    m_AnimationCurves.Add(aj.RotateZ)

                ' Scale X
                If aj.ScaleX.Keyframes.Count <> 0 Then _
                    m_AnimationCurves.Add(aj.ScaleX)

                ' Scale Y
                If aj.ScaleY.Keyframes.Count <> 0 Then _
                    m_AnimationCurves.Add(aj.ScaleY)

                ' Scale Z
                If aj.ScaleZ.Keyframes.Count <> 0 Then _
                    m_AnimationCurves.Add(aj.ScaleZ)

            Next J ' For J As Integer = 0 To m_Animations(I).Joints.Count - 1 '
        Next I ' For I As Integer = 0 To m_Animations.Count - 1

        For I As Integer = 0 To m_AnimationCurves.Count - 1
            ' Write it's name.
            SB.Append(m_AnimationCurves(I).ToString())
            SB.Append(Chr(0))

        Next I ' For I As Integer = 0 To m_AnimationCurves.Count - 1

        ' Setup STRI block.
        m_STRI = SB.ToString()

        ' Remvoe SB.
        SB.Length = 0
        SB = Nothing
    End Sub

    ''' <summary>
    ''' Reads the MAD chunk.
    ''' </summary>
    Private Sub ReadMADChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        ' Add handlers.
        IFF.AddHandler("VERS", Homeworld2.IFF.ChunkType.Default, AddressOf ReadIDChunk)
        IFF.AddHandler("NAME", Homeworld2.IFF.ChunkType.Default, AddressOf ReadIDChunk)
        IFF.AddHandler("INFO", Homeworld2.IFF.ChunkType.Default, AddressOf ReadINFOChunk)
        IFF.AddHandler("STRI", Homeworld2.IFF.ChunkType.Default, AddressOf ReadSTRIChunk)
        IFF.AddHandler("MARK", Homeworld2.IFF.ChunkType.Default, AddressOf ReadMARKChunk)
        IFF.AddHandler("CURV", Homeworld2.IFF.ChunkType.Default, AddressOf ReadCURVChunk)

        ' Parse the file.
        IFF.Parse()
    End Sub

    ''' <summary>
    ''' Reads (actually, tests) an ID chunk.
    ''' </summary>
    Private Sub ReadIDChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        If ChunkAttributes.ID = "VERS" Then _
            Trace.Assert(IFF.ReadInt32() = &H104, "VERS chunk ID test failed.") _
                : Exit Sub

        If ChunkAttributes.ID = "NAME" Then _
            Trace.Assert(String.Compare(IFF.ReadString(ChunkAttributes.Size), MAD_Name) = 0,
                         "NAME chunk ID test failed.") _
                : Exit Sub

        Trace.Assert(False, "Chunk ID test failed.")
    End Sub

    ''' <summary>
    ''' Reads the INFO chunk.
    ''' </summary>
    Private Sub ReadINFOChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        Dim animationCount As Integer,
            animationCurveCount As Integer,
            jointMapCount As Integer,
            indiceCount As Integer

        m_FPS = IFF.ReadInt32()
        animationCount = IFF.ReadInt32()
        animationCurveCount = IFF.ReadInt32()
        jointMapCount = IFF.ReadInt32()
        indiceCount = IFF.ReadInt32()

        ' Resize animation list.
        For I As Integer = 1 To animationCount
            m_Animations.Add(New Animation)

        Next I ' For I As Integer = 1 To animationCount

        ' Resize animation curve list.
        For I As Integer = 1 To animationCurveCount
            m_AnimationCurves.Add(New AnimationCurve)

        Next I ' For I As Integer = 1 To animationCurveCount
    End Sub

    ''' <summary>
    ''' Writes the INFO chunk.
    ''' </summary>
    Private Sub WriteINFOChunk(ByVal IFF As IFF.IFFWriter)
        Dim jointMapCount As Integer = 0,
            indiceCount As Integer = 0

        For I As Integer = 0 To m_Animations.Count - 1
            jointMapCount += m_Animations(I).Joints.Count

            For J As Integer = 0 To m_Animations(I).Joints.Count - 1
                indiceCount += m_Animations(I).Joints(J).ChannelCount

            Next J ' For J As Integer = 0 To m_Animations(I).Joints.Count
        Next I ' For I As Integer = 0 To m_Animations.Count - 1

        IFF.Push("INFO")

        IFF.WriteInt32(m_FPS)
        IFF.WriteInt32(m_Animations.Count)
        IFF.WriteInt32(m_AnimationCurves.Count)
        IFF.WriteInt32(jointMapCount)
        IFF.WriteInt32(indiceCount)

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Reads the STRI chunk.
    ''' </summary>
    Private Sub ReadSTRIChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        m_STRI = IFF.ReadString(ChunkAttributes.Size)
    End Sub

    ''' <summary>
    ''' Writes the STRI chunk.
    ''' </summary>
    Private Sub WriteSTRIChunk(ByVal IFF As IFF.IFFWriter)
        IFF.Push("STRI")
        IFF.Write(m_STRI, m_STRI.Length)
        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Returns a string from the STRI chunk.
    ''' </summary>
    Private Function _GetNameFromSTRI(ByVal pos As Integer) As String
        ' Check index.
        If (pos < 0) OrElse (pos >= m_STRI.Length) Then _
            Debug.Assert(False) _
                : Return "Unknown"

        ' Get the null after this string.
        Dim null As Integer = m_STRI.IndexOf(Chr(0), pos)

        ' Return the whole string if null is not found.
        ' Note that this should not happen.
        If null = - 1 Then _
            Debug.Assert(False) _
                : Return m_STRI.Substring(pos)

        ' Return the string minus the null.
        Return m_STRI.Substring(pos, null - pos)
    End Function

    ''' <summary>
    ''' Reads the MARK chunk.
    ''' </summary>
    Private Sub ReadMARKChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        For I As Integer = 0 To m_Animations.Count - 1
            m_Animations(I).ReadIFF(IFF, AddressOf _GetNameFromSTRI, m_HOD)

        Next I ' For I As Integer = 0 To m_Animations.Count - 1
    End Sub

    ''' <summary>
    ''' Writes the MARK chunk.
    ''' </summary>
    Private Sub WriteMARKChunk(ByVal IFF As IFF.IFFWriter, ByRef STRIPosition As Integer)
        Dim index As Integer = 0

        IFF.Push("MARK")

        For I As Integer = 0 To m_Animations.Count - 1
            m_Animations(I).WriteIFF(IFF, STRIPosition, index)

        Next I  ' For I As Integer = 0 To m_Animations.Count - 1

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Reads the CURV chunk.
    ''' </summary>
    Private Sub ReadCURVChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        For I As Integer = 0 To m_AnimationCurves.Count - 1
            m_AnimationCurves(I).ReadIFF(IFF, AddressOf _GetNameFromSTRI)

        Next I ' For I As Integer = 0 To m_AnimationCurves.Count - 1
    End Sub

    ''' <summary>
    ''' Writes the CURV chunk.
    ''' </summary>
    Private Sub WriteCURVChunk(ByVal IFF As IFF.IFFWriter, ByRef STRIPosition As Integer)
        IFF.Push("CURV")

        For I As Integer = 0 To m_AnimationCurves.Count - 1
            m_AnimationCurves(I).WriteIFF(IFF, STRIPosition)

        Next I ' For I As Integer = 0 To m_AnimationCurves.Count - 1

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Initializes the MAD.
    ''' </summary>
    Public Sub Initialize()
        m_FPS = 30
        m_STRI = ""
        m_HOD = Nothing

        m_Animations.Clear()
        m_AnimationCurves.Clear()
    End Sub

    ''' <summary>
    ''' Resets any changes made by any animation.
    ''' </summary>
    ''' <remarks>
    ''' This is done by calling reset for all animations.
    ''' </remarks>
    Public Sub Reset()
        For I As Integer = 0 To m_Animations.Count - 1
            m_Animations(I).Reset()

        Next I ' For I As Integer = 0 To m_Animations.Count - 1
    End Sub
End Class
