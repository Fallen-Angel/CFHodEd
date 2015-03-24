''' <summary>
''' Class representing a Homeworld2 animation marker.
''' </summary>
Public Class Animation
    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Name.</summary>
    Private m_Name As String

    ''' <summary>Start time.</summary>
    Private m_StartTime As Single

    ''' <summary>End time.</summary>
    Private m_EndTime As Single

    ''' <summary>Loop start time.</summary>
    Private m_LoopStartTime As Single

    ''' <summary>Loop end time.</summary>
    Private m_LoopEndTime As Single

    ''' <summary>Joints list.</summary>
    Private m_Joints As New EventList(Of AnimatedJoint)

    ' ------------------------
    ' Constructors\Finalizers.
    ' ------------------------
    ''' <summary>
    ''' Class contructor.
    ''' </summary>
    Public Sub New()
        Initialize()
    End Sub

    ''' <summary>
    ''' Copy contructor.
    ''' </summary>
    Public Sub New(ByVal a As Animation)
        m_Name = a.m_Name
        m_StartTime = a.m_StartTime
        m_EndTime = a.m_EndTime
        m_LoopStartTime = a.m_LoopStartTime
        m_LoopEndTime = a.m_LoopEndTime
        m_Joints.Clear()

        For I As Integer = 0 To a.m_Joints.Count - 1
            m_Joints.Add(New AnimatedJoint(a.m_Joints(I)))

        Next I '   For I As Integer = 0 To a.m_Joints.Count - 1
    End Sub

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns\Sets name of this animation.
    ''' </summary>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>value Is Nothing</c>.
    ''' </exception>
    Public Property Name() As String
        Get
            Return m_Name
        End Get

        Set(ByVal value As String)
            If (value Is Nothing) OrElse (value = "") Then _
                Throw New ArgumentNullException("value") _
                    : Exit Property

            m_Name = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets animation start time.
    ''' </summary>
    Public Property StartTime() As Single
        Get
            Return m_StartTime
        End Get

        Set(ByVal value As Single)
            If value < 0 Then _
                Throw New ArgumentOutOfRangeException("value") _
                    : Exit Property

            m_StartTime = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets animation end time.
    ''' </summary>
    Public Property EndTime() As Single
        Get
            Return m_EndTime
        End Get

        Set(ByVal value As Single)
            If value < 0 Then _
                Throw New ArgumentOutOfRangeException("value") _
                    : Exit Property

            m_EndTime = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets animation loop start time.
    ''' </summary>
    Public Property LoopStartTime() As Single
        Get
            Return m_LoopStartTime
        End Get

        Set(ByVal value As Single)
            If value < 0 Then _
                Throw New ArgumentOutOfRangeException("value") _
                    : Exit Property

            m_LoopStartTime = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets animation loop end time.
    ''' </summary>
    Public Property LoopEndTime() As Single
        Get
            Return m_LoopEndTime
        End Get

        Set(ByVal value As Single)
            If value < 0 Then _
                Throw New ArgumentOutOfRangeException("value") _
                    : Exit Property

            m_LoopEndTime = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the list of joints animated by this animation.
    ''' </summary>
    Public ReadOnly Property Joints() As IList(Of AnimatedJoint)
        Get
            Return m_Joints
        End Get
    End Property

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Returns the name of this animation.
    ''' </summary>
    Public Overrides Function ToString() As String
        Return m_Name
    End Function

    ''' <summary>
    ''' Reads the animation from an IFF file.
    ''' </summary>
    Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader, ByVal NameFromSTRI As Func(Of Integer, String), ByVal HOD As HOD.HOD)
        ' Initialize.
        Initialize()

        ' Read name.
        m_Name = NameFromSTRI(IFF.ReadInt32())

        ' Read properties.
        m_StartTime = IFF.ReadSingle()
        m_EndTime = IFF.ReadSingle()
        m_LoopStartTime = IFF.ReadSingle()
        m_LoopEndTime = IFF.ReadSingle()

        ' Read joint count.
        Dim numJoints As Integer = IFF.ReadInt32()

        ' Read joints.
        For I As Integer = 1 To numJoints
            ' Create new joint.
            Dim j As New AnimatedJoint

            ' Read it.
            j.ReadIFF(IFF, NameFromSTRI, HOD)

            ' Add to list.
            m_Joints.Add(j)

        Next I ' For I As Integer = 1 To numJoints
    End Sub

    ''' <summary>
    ''' Writes the animation to an IFF writer.
    ''' </summary>
    Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter, ByRef striPosition As Integer, ByRef index As Integer)
        ' The following piece of code is now executed in the MAD export
        ' code itself.
#If 0 Then
  ' Clean-up this animation.
  PrepareAnimationBeforeExport()

#End If

        ' Write name.
        IFF.WriteInt32(striPosition)

        ' Update reference.
        striPosition += Me.ToString().Length + 1

        ' Write fields.
        IFF.Write(m_StartTime)
        IFF.Write(m_EndTime)
        IFF.Write(m_LoopStartTime)
        IFF.Write(m_LoopEndTime)

        ' Write joint count.
        IFF.Write(m_Joints.Count)

        ' Write joints.
        For I As Integer = 0 To m_Joints.Count - 1
            m_Joints(I).WriteIFF(IFF, striPosition, index)

        Next I ' For I As Integer = 0 To m_Joints.Count - 1
    End Sub

    ''' <summary>
    ''' Prepares animation before export by removing redundant joints.
    ''' </summary>
    Friend Sub PrepareAnimationBeforeExport()
        For I As Integer = m_Joints.Count - 1 To 0 Step - 1
            ' Remove joints that are not attached to a HOD joint.
            If m_Joints(I).Joint Is Nothing Then _
                m_Joints.RemoveAt(I) _
                    : Continue For

            ' Remove joints with no channels.
            If m_Joints(I).ChannelCount = 0 Then _
                m_Joints.RemoveAt(I) _
                    : Continue For

        Next I ' For I As Integer = m_Joints.Count - 1 To 0 Step -1
    End Sub

    ''' <summary>
    ''' Initializes this animation.
    ''' </summary>
    Private Sub Initialize()
        m_Name = "animation"
        m_StartTime = 0
        m_EndTime = 1
        m_LoopStartTime = 0
        m_LoopEndTime = 1
        m_Joints.Clear()
    End Sub

    ''' <summary>
    ''' Resets the joint properties.
    ''' </summary>
    Public Sub Reset()
        For I As Integer = 0 To m_Joints.Count - 1
            m_Joints(I).Reset()

        Next I ' For I As Integer = 0 To m_Joints.Count - 1
    End Sub

    ''' <summary>
    ''' Updates the joint properties.
    ''' </summary>
    Public Sub Update(ByVal time As Single)
        For I As Integer = 0 To m_Joints.Count - 1
            m_Joints(I).Update(time)

        Next I ' For I As Integer = 0 To m_Joints.Count - 1
    End Sub

    ''' <summary>
    ''' Updates references of all joints.
    ''' </summary>
    Friend Sub UpdateReferences(ByVal animationCurves As IList(Of AnimationCurve))
        For I As Integer = 0 To m_Joints.Count - 1
            m_Joints(I).UpdateReferences(animationCurves)

        Next I ' For I As Integer = 0 To m_Joints.Count - 1
    End Sub
End Class
