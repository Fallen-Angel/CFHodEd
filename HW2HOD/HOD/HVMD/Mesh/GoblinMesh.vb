''' <summary>
''' Class representing Homeworld2 Goblin Mesh. Used for rendering minute
''' detail on ships, using a single basic mesh.
''' </summary>
Public NotInheritable Class GoblinMesh
    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Name of mesh (usually name of parent postfixed with "_goblins").</summary>
    Private m_Name As String

    ''' <summary>Name of mesh parent (joint).</summary>
    Private m_ParentName As String

    ''' <summary>The basic mesh.</summary>
    Private m_Mesh As New BasicMesh

    Private m_Tags As String

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
    ''' Copy constructor.
    ''' </summary>
    Public Sub New(ByVal gm As GoblinMesh)
        m_Name = gm.m_Name
        m_ParentName = gm.m_ParentName
        m_Mesh.Append(gm.m_Mesh)
    End Sub

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns\Sets name of mesh (usually name of parent postfixed
    ''' with "_goblins").
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
    ''' Returns\Sets the name of mesh parent (joint).
    ''' </summary>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>value Is Nothing</c>.
    ''' </exception>
    Public Property ParentName() As String
        Get
            Return m_ParentName
        End Get

        Set(ByVal value As String)
            If (value Is Nothing) OrElse (value = "") Then _
                Throw New ArgumentNullException("value") _
                    : Exit Property

            m_ParentName = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the mesh.
    ''' </summary>
    Public ReadOnly Property Mesh() As BasicMesh
        Get
            Return m_Mesh
        End Get
    End Property

    ''' <summary>
    ''' Returns\Sets whether the mesh is visible or not.
    ''' Note that this is only applicable when rendering through this
    ''' object. Individually, the basic mesh can still be rendered.
    ''' </summary>
    ''' <remarks>
    ''' This is purely a rendering related property. It does not
    ''' affect what is written in the HOD
    ''' </remarks>
    Public Property Visible() As Boolean
        Get
            Return m_Mesh.Visible
        End Get

        Set(ByVal value As Boolean)
            m_Mesh.Visible = value
        End Set
    End Property

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Returns the name of this mesh.
    ''' </summary>
    Public Overrides Function ToString() As String
        Return m_Name
    End Function

    ''' <summary>
    ''' Reads a goblin mesh from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Chunk attributes.
    ''' </param>
    Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        ' Initialize mesh first.
        Initialize()

        ' Read name.
        m_Name = IFF.ReadString()

        ' Read parent name.
        m_ParentName = IFF.ReadString()

        ' Add handlers.
        IFF.AddHandler("BMSH", Homeworld2.IFF.ChunkType.Normal, AddressOf ReadBMSHChunk, 1400)
        IFF.AddHandler("BMSH", Homeworld2.IFF.ChunkType.Normal, AddressOf ReadBMSHChunk, 1401)

        IFF.AddHandler("TAGS", Homeworld2.IFF.ChunkType.Form, AddressOf ReadTAGSChunk)

        ' Read mesh.
        IFF.Parse()
    End Sub

    Private Sub ReadTAGSChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        ' Add handlers.
        m_Tags = IFF.ReadString()
    End Sub

    ''' <summary>
    ''' Writes the multi mesh to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
        IFF.Push("GOBG", Homeworld2.IFF.ChunkType.Normal, 1000)

        ' Write name
        IFF.Write(m_Name)

        ' Write parent name
        IFF.Write(m_ParentName)

        If (m_Tags <> Nothing) Then
            IFF.Push("TAGS", Homeworld2.IFF.ChunkType.Form)
            IFF.Write(m_Tags)
            IFF.Pop()
        End If

        ' Write mesh.
        m_Mesh.WriteIFF(IFF)

        For I As Integer = 0 To m_Mesh.PartCount - 1
            If m_Mesh.Part(I).Material.Index < 0 Then _
                Trace.TraceWarning("Goblin mesh " & m_Name & " part " & CStr(I) & " has no material assigned to it.")

        Next I ' For I As Integer = 0 To m_Mesh.PartCount - 1

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Reads the BMSH chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadBMSHChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        m_Mesh.ReadIFF(IFF, ChunkAttributes)
    End Sub

    ''' <summary>
    ''' Initializes the goblin mesh.
    ''' </summary>
    Private Sub Initialize()
        m_Name = "Root_goblins"
        m_ParentName = "Root"
        m_Mesh.Initialize()
    End Sub

    ''' <summary>
    ''' Updates the goblin mesh so that it renders properly.
    ''' </summary>
    Friend Sub Update(ByVal Materials As IList(Of Material))
        m_Mesh.Update(Materials)
    End Sub

    ''' <summary>
    ''' Locks the meshes so that they can be rendered.
    ''' </summary>
    ''' <param name="Device">
    ''' The device in which the meshes will be rendered.
    ''' </param>
    Friend Sub Lock(ByVal Device As Direct3D.Device)
        m_Mesh.Lock(Device)
    End Sub

    ''' <summary>
    ''' Unlocks the meshes so that they can be edited.
    ''' </summary>
    Friend Sub Unlock()
        m_Mesh.Unlock()
    End Sub

    ''' <summary>
    ''' Renders the goblin mesh.
    ''' </summary>
    Friend Sub Render(ByVal Device As Direct3D.Device)
        If m_Mesh.Visible Then _
            m_Mesh.Render(Device)
    End Sub
End Class
