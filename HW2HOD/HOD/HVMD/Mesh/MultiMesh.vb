''' <summary>
''' Class representing Homeworld2 Multi Mesh. Used for rendering ships,
''' containing different LODs in different basic meshes.
''' </summary>
Public NotInheritable Class MultiMesh
    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Name of mesh (usually name of parent postfixed with "_mesh").</summary>
    Private m_Name As String

    ''' <summary>Name of mesh parent (joint).</summary>
    Private m_ParentName As String

    Private m_Tags As String

    ''' <summary>List of LODs (level of detail).</summary>
    Private WithEvents m_LODs As New EventList(Of BasicMesh)

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    Public Sub New()
        Initialize()
    End Sub

    ''' <summary>
    ''' Copy constructor.
    ''' </summary>
    Public Sub New(ByVal mm As MultiMesh)
        With mm
            m_Name = .m_Name
            m_ParentName = .m_ParentName

            For I As Integer = 0 To .m_LODs.Count - 1
                m_LODs.Add(New BasicMesh(.m_LODs(I)))

            Next I ' For I As Integer = 0 To .m_LODs.Count - 1
        End With ' With mm
    End Sub

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns\Sets name of mesh (usually name of parent postfixed
    ''' with "_mesh").
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
    ''' Returns the list of LODs (level of details).
    ''' </summary>
    Public ReadOnly Property LOD() As IList(Of BasicMesh)
        Get
            Return m_LODs
        End Get
    End Property

    ''' <summary>
    ''' Returns\Sets whether the specified LOD is visible or not.
    ''' Note that this is only applicable when rendering through this
    ''' object. Individually, the basic meshes can still be rendered.
    ''' </summary>
    ''' <param name="LOD">
    ''' The LOD whose visibility is being read\written.
    ''' </param>
    ''' <remarks>
    ''' This is purely a rendering related property. It does not
    ''' affect what is written in the HOD
    ''' </remarks>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when the specified LOD does not exist.
    ''' </exception>
    Public Property Visible(ByVal LOD As Integer) As Boolean
        Get
            If (LOD < 0) OrElse (LOD >= m_LODs.Count) Then _
                Throw New ArgumentOutOfRangeException("LOD") _
                    : Exit Property

            Return m_LODs(LOD).Visible
        End Get

        Set(ByVal value As Boolean)
            If (LOD < 0) OrElse (LOD >= m_LODs.Count) Then _
                Throw New ArgumentOutOfRangeException("LOD") _
                    : Exit Property

            m_LODs(LOD).Visible = value
        End Set
    End Property

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Returns the name of this multi mesh.
    ''' </summary>
    Public Overrides Function ToString() As String
        Return m_Name
    End Function

    ''' <summary>
    ''' Updates the LODs when an item is removed.
    ''' </summary>
    Private Sub LODs_Update(Optional ByVal index As Integer = 0) _
        Handles m_LODs.AddItem, m_LODs.InsertItem, m_LODs.RemoveItem
        For I As Integer = 0 To m_LODs.Count - 1
            m_LODs(I).LOD = I

        Next I ' For I As Integer = 0 To m_LODs.Count - 1
    End Sub

    ''' <summary>
    ''' Reads a multi mesh from an IFF reader.
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

        ' Read LOD count.
        Dim LODCount As Integer = IFF.ReadInt32()

        ' Add handlers
        IFF.AddHandler("BMSH", Homeworld2.IFF.ChunkType.Normal, AddressOf ReadBMSHChunk, 1400)
        IFF.AddHandler("BMSH", Homeworld2.IFF.ChunkType.Normal, AddressOf ReadBMSHChunk, 1401)


        IFF.AddHandler("TAGS", Homeworld2.IFF.ChunkType.Form, AddressOf ReadTAGSChunk)

        ' Read all LODs.
        IFF.Parse()

        ' Make sure we read all the mentioned LODs.
        If LODCount <> m_LODs.Count Then _
            Trace.TraceError("Multi mesh has an invalid LOD count!")
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
        If m_LODs.Count = 0 Then _
            Exit Sub

        IFF.Push("MULT", Homeworld2.IFF.ChunkType.Normal, 1400)

        ' Write name
        IFF.Write(m_Name)

        ' Write parent name
        IFF.Write(m_ParentName)

        ' Write LOD count.
        IFF.WriteInt32(m_LODs.Count)

        If (m_Tags <> Nothing) Then
            IFF.Push("TAGS", Homeworld2.IFF.ChunkType.Form)
            IFF.Write(m_Tags)
            IFF.Pop()
        End If

        ' Write all LODs.
        For I As Integer = 0 To m_LODs.Count - 1
            m_LODs(I).WriteIFF(IFF)

            For J As Integer = 0 To m_LODs(I).PartCount - 1
                If m_LODs(I).Part(J).Material.Index < 0 Then _
                    Trace.TraceWarning("Multi mesh " & m_Name & " part " & CStr(J) & " has no material assigned to it.")

            Next J ' For J As Integer = 0 To m_LODs(I).PartCount - 1
        Next I ' For I As Integer = 0 To m_LODs.Count - 1

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
        Dim BasicMesh As New BasicMesh

        ' Read the basic mesh.
        BasicMesh.ReadIFF(IFF, ChunkAttributes)

        ' Assert before the basic mesh is added, because it may get modified after adding.
        If BasicMesh.LOD <> m_LODs.Count Then _
            Trace.TraceWarning("One of the LODs of a multi mesh has invalid LOD order!")

        ' Now add it.
        m_LODs.Add(BasicMesh)

        ' Only make the first (i.e. zeroth) LOD visible. 
        If m_LODs.Count = 1 Then _
            BasicMesh.Visible = True _
            Else _
            BasicMesh.Visible = False
    End Sub

    ''' <summary>
    ''' Initializes the multi mesh.
    ''' </summary>
    Private Sub Initialize()
        m_Name = "Root_mesh"
        m_ParentName = "Root"
        m_LODs.Clear()
    End Sub

    ''' <summary>
    ''' Updates the multi mesh so that it can be rendered properly.
    ''' </summary>
    Friend Sub Update(ByVal Materials As IList(Of Material))
        For I As Integer = 0 To m_LODs.Count - 1
            m_LODs(I).Update(Materials)

        Next I ' For I As Integer = 0 To m_LODs.Count - 1
    End Sub

    ''' <summary>
    ''' Locks the meshes so that they can be rendered.
    ''' </summary>
    ''' <param name="Device">
    ''' The device in which the meshes will be rendered.
    ''' </param>
    Public Sub Lock(ByVal Device As Direct3D.Device)
        For I As Integer = 0 To m_LODs.Count - 1
            m_LODs(I).Lock(Device)

        Next I ' For I As Integer = 0 To m_LODs.Count - 1
    End Sub

    ''' <summary>
    ''' Unlocks the meshes so that they can be edited.
    ''' </summary>
    Public Sub Unlock()
        For I As Integer = 0 To m_LODs.Count - 1
            m_LODs(I).Unlock()

        Next I ' For I As Integer = 0 To m_LODs.Count - 1
    End Sub

    ''' <summary>
    ''' Renders the multi mesh.
    ''' </summary>
    Friend Sub Render(ByVal Device As Direct3D.Device)
        For I As Integer = 0 To m_LODs.Count - 1
            If m_LODs(I).Visible Then _
                m_LODs(I).Render(Device)

        Next I ' For I As Integer = 0 To m_LODs.Count - 1
    End Sub
End Class
