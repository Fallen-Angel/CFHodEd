''' <summary>
''' Class representing a Homeworld2 textured star field.
''' </summary>
Public NotInheritable Class StarFieldT
    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Texture.</summary>
    Private m_Texture As String

    ''' <summary>Star array.</summary>
    Private m_Stars() As Star

    ''' <summary>Visible.</summary>
    Private m_Visible As Boolean

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
    Public Sub New(ByVal sf As StarFieldT)
        Initialize()

        If sf.m_Stars.Length = 0 Then _
            Exit Sub

        ReDim m_Stars(sf.m_Stars.Length - 1)

        For I As Integer = 0 To sf.m_Stars.Length - 1
            m_Stars(I) = sf.m_Stars(I)

        Next I ' For I As Integer = 0 To sf.m_Stars.Length - 1
    End Sub

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns\Sets the number of stars in this star field.
    ''' </summary>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>index</c> is out of range.
    ''' </exception>
    Public Property Count() As Integer
        Get
            Return m_Stars.Length
        End Get

        Set(ByVal value As Integer)
            If value < 0 Then _
                Throw New ArgumentOutOfRangeException("value") _
                    : Exit Property

            ReDim Preserve m_Stars(value - 1)
        End Set
    End Property

    ''' <summary>
    ''' Retuns\Sets the specified star.
    ''' </summary>
    ''' <param name="index">
    ''' Index of star to get\set.
    ''' </param>
    Default Public Property Item(ByVal index As Integer) As Star
        Get
            Return m_Stars(index)
        End Get

        Set(ByVal value As Star)
            m_Stars(index) = value
        End Set
    End Property

    ''' <summary>
    ''' Returs\Sets texture used by this star field group.
    ''' </summary>
    Public Property Texture() As String
        Get
            Return m_Texture
        End Get

        Set(ByVal value As String)
            m_Texture = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets whether the star field is visible or not.
    ''' </summary>
    ''' <remarks>
    ''' This is purely a rendering related property. It does not
    ''' affect what is written in the HOD
    ''' </remarks>
    Public Property Visible() As Boolean
        Get
            Return m_Visible
        End Get

        Set(ByVal value As Boolean)
            m_Visible = value
        End Set
    End Property

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Adds a star to the list.
    ''' </summary>
    Public Sub Add(ByVal s As Star)
        ' Resize array.
        ReDim Preserve m_Stars(m_Stars.Length)

        ' Copy element.
        m_Stars(m_Stars.Length - 1) = s
    End Sub

    ''' <summary>
    ''' Inserts a star to the list.
    ''' </summary>
    ''' <param name="index">
    ''' Index where item is to be inserted.
    ''' </param>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>index</c> is out of range.
    ''' </exception>
    Public Sub Insert(ByVal index As Integer, ByVal s As Star)
        ' Check range.
        If (index < 0) OrElse (index > m_Stars.Length) Then _
            Throw New ArgumentOutOfRangeException("index") _
                : Exit Sub

        ' Resize array.
        ReDim Preserve m_Stars(m_Stars.Length)

        ' Copy elements.
        For I As Integer = m_Stars.Length - 2 To index Step - 1
            m_Stars(I + 1) = m_Stars(I)

        Next I ' For I As Integer = index To m_Stars.Length - 2

        m_Stars(index) = s
    End Sub

    ''' <summary>
    ''' Removes the specified star.
    ''' </summary>
    ''' <param name="index">
    ''' Index of the item to be removed.
    ''' </param>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>index</c> is out of range.
    ''' </exception>
    Public Sub Remove(ByVal index As Integer)
        ' Check range.
        If (index < 0) OrElse (index >= m_Stars.Length) Then _
            Throw New ArgumentOutOfRangeException("index") _
                : Exit Sub

        ' Shift back elements by one.
        For I As Integer = index To m_Stars.Length - 2
            m_Stars(I) = m_Stars(I + 1)

        Next I ' For I As Integer = index To m_Stars.Length - 2

        ' Resize array.
        ReDim Preserve m_Stars(m_Stars.Length - 2)
    End Sub

    ''' <summary>
    ''' Removes all stars.
    ''' </summary>
    Public Sub Clear()
        Erase m_Stars
        ReDim m_Stars(- 1)
    End Sub

    ''' <summary>
    ''' Reads the star field from an IFF reader.
    ''' </summary>
    Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader)
        ' Initialize.
        Initialize()

        ' Read texture name.
        m_Texture = IFF.ReadString()

        ' Read star count.
        Dim numStars As Integer = Math.Max(0, IFF.ReadInt32())

        ' Resize array.
        ReDim m_Stars(numStars - 1)

        ' Read all stars.
        For I As Integer = 0 To numStars - 1
            m_Stars(I) = Star.ReadIFF(IFF, False)

        Next I ' For I As Integer = 0 To numStars - 1
    End Sub

    ''' <summary>
    ''' Writes the star field to an IFF writer.
    ''' </summary>
    Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
        ' Write nothing if no stars.
        If m_Stars.Length = 0 Then _
            Exit Sub

        IFF.Push("BGSG", Homeworld2.IFF.ChunkType.Form)
        IFF.Write(m_Texture)
        IFF.WriteInt32(m_Stars.Length)

        For I As Integer = 0 To m_Stars.Length - 1
            m_Stars(I).WriteIFF(IFF, False)

        Next I ' For I As Integer = 0 To m_Stars.Length - 1

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Initializes this star field.
    ''' </summary>
    Private Sub Initialize()
        Erase m_Stars
        ReDim m_Stars(- 1)

        m_Texture = "B02.tga"
        m_Visible = True
    End Sub

    ''' <summary>
    ''' Renders the star field.
    ''' </summary>
    Friend Sub Render(ByVal Device As Direct3D.Device, ByVal Transform As Matrix)
        Dim star As Star

        If m_Stars.Length = 0 Then _
            Exit Sub

        If Not m_Visible Then _
            Exit Sub

        With Device
            ' Cache some states.
            Dim oldAmbient As Drawing.Color = .RenderState.Ambient,
                oldLighting As Boolean = .RenderState.Lighting,
                oldSpecularEnable As Boolean = .RenderState.SpecularEnable

            ' Set new states.
            .RenderState.Ambient = Drawing.Color.White
            .RenderState.Lighting = False
            .RenderState.SpecularEnable = False
            .Transform.World = Matrix.Identity

            ' Set FVF.
            .VertexFormat = star.Format

            ' Render stars.
            .DrawUserPrimitives(Direct3D.PrimitiveType.PointList, m_Stars.Length, m_Stars)

            ' Reset FVF.
            .VertexFormat = Direct3D.VertexFormats.Texture0

            ' Set old states.
            .RenderState.Ambient = oldAmbient
            .RenderState.Lighting = oldLighting
            .RenderState.SpecularEnable = oldSpecularEnable

        End With ' With Device
    End Sub
End Class
