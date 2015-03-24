''' <summary>
''' Class representing Homeworld2 Engine Glow.
''' </summary>
Public NotInheritable Class EngineGlow
    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Name.</summary>
    Private m_Name As String

    ''' <summary>Parent name.</summary>
    Private m_ParentName As String

    ''' <summary>LOD.</summary>
    Private m_LOD As Integer

    ''' <summary>The basic mesh.</summary>
    Private m_Mesh As New BasicMesh

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
    Public Sub New(ByVal eg As EngineGlow)
        m_Name = eg.m_Name
        m_ParentName = eg.m_ParentName
        m_LOD = eg.m_LOD
        m_Mesh.Append(eg.m_Mesh)
    End Sub

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns\Sets name of mesh (usually "EngineGlowX" where X is
    ''' a number)
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
    ''' Returns\Sets mesh LOD (level of detail).
    ''' </summary>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>value</c> is negative.
    ''' </exception>
    Public Property LOD() As Integer
        Get
            Return m_LOD
        End Get

        Set(ByVal value As Integer)
            m_LOD = value
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

        ' Add handlers.
        IFF.AddHandler("INFO", Homeworld2.IFF.ChunkType.Normal, AddressOf ReadINFOChunk, 1000)
        IFF.AddHandler("BMSH", Homeworld2.IFF.ChunkType.Normal, AddressOf ReadBMSHChunk, 1400)

        ' Read mesh.
        IFF.Parse()

        ' Set visible to false.
        m_Mesh.Visible = False
    End Sub

    ''' <summary>
    ''' Writes the multi mesh to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
        IFF.Push("GLOW", Homeworld2.IFF.ChunkType.Form)

        ' Write name, parent name, LOD.
        IFF.Push("INFO", Homeworld2.IFF.ChunkType.Normal, 1000)
        IFF.Write(m_Name)
        IFF.Write(m_ParentName)
        IFF.WriteInt32(m_LOD)
        IFF.Pop()

        ' Write mesh.
        m_Mesh.WriteIFF(IFF)

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Reads the INFO chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadINFOChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        ' Read name.
        m_Name = IFF.ReadString()

        ' Read parent name.
        m_ParentName = IFF.ReadString()

        ' Read LOD.
        m_LOD = IFF.ReadInt32()
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
        m_Name = "EngineGlow"
        m_ParentName = "Root"
        m_Mesh.Initialize()
        m_Mesh.Visible = False
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
    ''' Renders the engine glow mesh.
    ''' </summary>
    Friend Sub Render(ByVal Device As Direct3D.Device)
        If m_Mesh.Visible Then _
            m_Mesh.Render(Device)
    End Sub
End Class
