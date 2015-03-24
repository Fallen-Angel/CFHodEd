Imports GenericMesh

Partial Class HOD
    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Root joint.</summary>
    Private m_RootJoint As New Joint

    ''' <summary>Whether to draw joint skeleton or not.</summary>
    Private m_SkeletonVisible As Boolean

    ''' <summary>Scale applied on skeleton.</summary>
    Private m_SkeletonScale As Single = 1.0F

    ''' <summary>Mesh used to render a joint.</summary>
    Private m_JointMesh As Standard.BasicMesh

    ''' <summary>Mesh used to render link between two joints.</summary>
    Private m_JointLinkMesh As Standard.BasicMesh

    ''' <summary>Collision Meshes.</summary>
    Private m_CollisionMeshes As New EventList(Of CollisionMesh)

    ''' <summary>Collision Mesh boundary sphere.</summary>
    Private m_CollisionMeshSphere As Standard.BasicMesh

    ''' <summary>Engine shapes.</summary>
    Private m_EngineShapes As New EventList(Of EngineShape)

    ''' <summary>Engine glows.</summary>
    Private m_EngineGlows As New EventList(Of EngineGlow)

    ''' <summary>Engine burns.</summary>
    Private m_EngineBurns As New EventList(Of EngineBurn)

    ''' <summary>NavLights.</summary>
    Private m_NavLights As New EventList(Of NavLight)

    ''' <summary>Mesh used to render a navlight.</summary>
    Private m_NavLightMesh As GBasicMesh(Of PNVertex, Integer, Standard.Material)

    ''' <summary>Markers.</summary>
    Private m_Markers As New EventList(Of Marker)

    ''' <summary>Scale applied on marker.</summary>
    Private m_MarkerScale As Single = 1.0F

    ''' <summary>Mesh used to render a marker.</summary>
    Private m_MarkerMesh As Standard.BasicMesh

    ''' <summary>Dockpaths.</summary>
    Private m_Dockpaths As New EventList(Of Dockpath)

    ''' <summary>Scale applied on dockpoints.</summary>
    Private m_DockpointScale As Single = 1.0F

    ''' <summary>Mesh used to render a dockpoint.</summary>
    Private m_DockpointMesh As Standard.BasicMesh

    ''' <summary>Mesh used to render the tolerance region of a dockpoint mesh.</summary>
    Private m_DockpointToleranceMesh As Standard.BasicMesh

    ''' <summary>Star fields.</summary>
    Private m_StarFields As New EventList(Of StarField)

    ''' <summary>Textured star fields.</summary>
    Private m_StarFieldsT As New EventList(Of StarFieldT)

    Private m_Scar As List(Of Byte())

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns the root joint of this HOD, if applicable.
    ''' </summary>
    Public ReadOnly Property Root() As Joint
        Get
            Return m_RootJoint
        End Get
    End Property

    ''' <summary>
    ''' Returns\Sets whether the joint skeleton is visible or not.
    ''' </summary>
    ''' <remarks>
    ''' This is purely a rendering related property. It does not
    ''' affect what is written in the HOD.
    ''' </remarks>
    Public Property SkeletonVisible() As Boolean
        Get
            Return m_SkeletonVisible
        End Get

        Set(ByVal value As Boolean)
            m_SkeletonVisible = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets the scale applied to skeleton, while rendering it.
    ''' </summary>
    ''' <remarks>
    ''' This is purely a rendering related property. It does not
    ''' affect what is written in the HOD.
    ''' </remarks>
    Public Property SkeletonScale() As Single
        Get
            Return m_SkeletonScale
        End Get

        Set(ByVal value As Single)
            m_SkeletonScale = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the list of collision meshes in this HOD.
    ''' </summary>
    Public ReadOnly Property CollisionMeshes() As IList(Of CollisionMesh)
        Get
            Return m_CollisionMeshes
        End Get
    End Property

    ''' <summary>
    ''' Returns\Sets whether the specified collision mesh is visible or not.
    ''' Note that this is only applicable when rendering through this
    ''' object. Individually, the ccllision meshes can still be rendered.
    ''' </summary>
    ''' <param name="index">
    ''' Index of collision mesh whose visibility is being set.
    ''' </param>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>index</c> is out of range.
    ''' </exception>
    ''' <remarks>
    ''' This is purely a rendering related property. It does not
    ''' affect what is written in the HOD.
    ''' </remarks>
    Public Property CollisionMeshVisible(ByVal index As Integer) As Boolean
        Get
            If (index < 0) OrElse (index >= m_CollisionMeshes.Count) Then _
                Throw New ArgumentOutOfRangeException("index") _
                    : Exit Property

            Return m_CollisionMeshes(index).Visible
        End Get

        Set(ByVal value As Boolean)
            If (index < 0) OrElse (index >= m_CollisionMeshes.Count) Then _
                Throw New ArgumentOutOfRangeException("index") _
                    : Exit Property

            m_CollisionMeshes(index).Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the list of engine shapes in this HOD.
    ''' </summary>
    Public ReadOnly Property EngineShapes() As IList(Of EngineShape)
        Get
            Return m_EngineShapes
        End Get
    End Property

    ''' <summary>
    ''' Returns\Sets whether the specified engine shapes is visible or not.
    ''' Note that this is only applicable when rendering through this
    ''' object. Individually, the simple meshes can still be rendered.
    ''' </summary>
    ''' <param name="index">
    ''' Index of engine shape whose visibility is being set.
    ''' </param>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>index</c> is out of range.
    ''' </exception>
    ''' <remarks>
    ''' This is purely a rendering related property. It does not
    ''' affect what is written in the HOD.
    ''' </remarks>
    Public Property EngineShapeVisible(ByVal index As Integer) As Boolean
        Get
            If (index < 0) OrElse (index >= m_EngineShapes.Count) Then _
                Throw New ArgumentOutOfRangeException("index") _
                    : Exit Property

            Return m_EngineShapes(index).Visible
        End Get

        Set(ByVal value As Boolean)
            If (index < 0) OrElse (index >= m_EngineShapes.Count) Then _
                Throw New ArgumentOutOfRangeException("index") _
                    : Exit Property

            m_EngineShapes(index).Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the list of engine glows in this HOD.
    ''' </summary>
    Public ReadOnly Property EngineGlows() As IList(Of EngineGlow)
        Get
            Return m_EngineGlows
        End Get
    End Property

    ''' <summary>
    ''' Returns the list of engine burns in this HOD.
    ''' </summary>
    Public ReadOnly Property EngineBurns() As IList(Of EngineBurn)
        Get
            Return m_EngineBurns
        End Get
    End Property

    ''' <summary>
    ''' Returns the list of NavLights in this HOD.
    ''' </summary>
    Public ReadOnly Property NavLights() As IList(Of NavLight)
        Get
            Return m_NavLights
        End Get
    End Property

    ''' <summary>
    ''' Returns the list of markers in this HOD.
    ''' </summary>
    Public ReadOnly Property Markers() As IList(Of Marker)
        Get
            Return m_Markers
        End Get
    End Property

    ''' <summary>
    ''' Returns\Sets the scale applied to marker, while rendering it.
    ''' </summary>
    ''' <remarks>
    ''' This is purely a rendering related property. It does not
    ''' affect what is written in the HOD.
    ''' </remarks>
    Public Property MarkerScale() As Single
        Get
            Return m_MarkerScale
        End Get

        Set(ByVal value As Single)
            m_MarkerScale = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the list of dockpaths in this HOD.
    ''' </summary>
    Public ReadOnly Property Dockpaths() As IList(Of Dockpath)
        Get
            Return m_Dockpaths
        End Get
    End Property

    ''' <summary>
    ''' Returns\Sets the scale applied to dockpoint, while rendering it.
    ''' </summary>
    ''' <remarks>
    ''' This is purely a rendering related property. It does not
    ''' affect what is written in the HOD.
    ''' </remarks>
    Public Property DockpointScale() As Single
        Get
            Return m_DockpointScale
        End Get

        Set(ByVal value As Single)
            m_DockpointScale = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the list of starfields.
    ''' </summary>
    Public ReadOnly Property StarFields() As IList(Of StarField)
        Get
            Return m_StarFields
        End Get
    End Property

    ''' <summary>
    ''' Returns the list of textured starfields.
    ''' </summary>
    Public ReadOnly Property StarFieldsT() As IList(Of StarFieldT)
        Get
            Return m_StarFieldsT
        End Get
    End Property

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Reads the DTRM chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadDTRMChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        ' Add handlers.
        IFF.AddHandler("HIER", Homeworld2.IFF.ChunkType.Default, AddressOf ReadHIERChunk)
        IFF.AddHandler("ETSH", Homeworld2.IFF.ChunkType.Default, AddressOf ReadETSHChunk)
        IFF.AddHandler("GLOW", Homeworld2.IFF.ChunkType.Form, AddressOf ReadGLOWChunk)
        IFF.AddHandler("BURN", Homeworld2.IFF.ChunkType.Default, AddressOf ReadBURNChunk)
        IFF.AddHandler("NAVL", Homeworld2.IFF.ChunkType.Normal, AddressOf ReadNAVLChunk, 3)
        IFF.AddHandler("DOCK", Homeworld2.IFF.ChunkType.Default, AddressOf ReadDOCKChunk)
        IFF.AddHandler("MRKR", Homeworld2.IFF.ChunkType.Form, AddressOf ReadMRKRChunk)
        IFF.AddHandler("BGSG", Homeworld2.IFF.ChunkType.Form, AddressOf ReadBGSGChunk)
        IFF.AddHandler("STRF", Homeworld2.IFF.ChunkType.Normal, AddressOf ReadSTRFChunk, 1000)
        IFF.AddHandler("BNDV", Homeworld2.IFF.ChunkType.Default, AddressOf ReadBNDVChunk)
        IFF.AddHandler("COLD", Homeworld2.IFF.ChunkType.Form, AddressOf ReadCOLDChunk)
        IFF.AddHandler("BSRM", Homeworld2.IFF.ChunkType.Form, AddressOf ReadBSRMChunk)

        m_Scar = New List(Of Byte())()
        IFF.AddHandler("SCAR", Homeworld2.IFF.ChunkType.Normal, AddressOf ReadSCARChunk, 2015)

        ' Read the file.
        IFF.Parse()
    End Sub

    Private Sub ReadSCARChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        m_Scar.Add(IFF.ReadBytes(ChunkAttributes.Size))
    End Sub

    ''' <summary>
    ''' Writes the DTRM chunk to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteDTRMChunk(ByVal IFF As IFF.IFFWriter)
        IFF.Push("DTRM", Homeworld2.IFF.ChunkType.Form)

        WriteHIERChunk(IFF)
        WriteETSHChunk(IFF)
        WriteGLOWChunk(IFF)
        WriteBURNChunk(IFF)
        WriteNAVLChunk(IFF)
        WriteDOCKChunk(IFF)
        WriteMRKRChunk(IFF)
        WriteBGSGChunk(IFF)
        WriteSTRFChunk(IFF)
        WriteBNDVChunk(IFF)
        WriteCOLDChunk(IFF)

        For Each scar As Byte() In m_Scar
            IFF.Push("SCAR", Homeworld2.IFF.ChunkType.Normal, 2015)
            IFF.Write(scar)
            IFF.Pop()
        Next

        WriteBSRMChunk(IFF)

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Reads the HIER chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadHIERChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        Joint.ReadHIERChunk(IFF, m_RootJoint)
    End Sub

    ''' <summary>
    ''' Writes the HIER chunk to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteHIERChunk(ByVal IFF As IFF.IFFWriter)
        Joint.WriteHIERChunk(IFF, m_RootJoint)
    End Sub

    ''' <summary>
    ''' Reads the ETSH chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadETSHChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        Dim etsh As New EngineShape
        etsh.ReadIFF(IFF)
        m_EngineShapes.Add(etsh)
    End Sub

    ''' <summary>
    ''' Writes the ETSH chunk to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteETSHChunk(ByVal IFF As IFF.IFFWriter)
        For I As Integer = 0 To m_EngineShapes.Count - 1
            If Not m_RootJoint.Contains(m_EngineShapes(I).ParentName) Then _
                Trace.TraceWarning(
                    "Engine shape '" & m_EngineShapes(I).Name & "' refers to a parent that does not exist. Please fix.")

            m_EngineShapes(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To m_EngineShapes.Count - 1
    End Sub

    ''' <summary>
    ''' Reads the GLOW chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadGLOWChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        Dim glow As New EngineGlow
        glow.ReadIFF(IFF, ChunkAttributes)
        m_EngineGlows.Add(glow)
    End Sub

    ''' <summary>
    ''' Writes the GLOW chunk to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteGLOWChunk(ByVal IFF As IFF.IFFWriter)
        For I As Integer = 0 To m_EngineGlows.Count - 1
            If Not m_RootJoint.Contains(m_EngineGlows(I).ParentName) Then _
                Trace.TraceWarning(
                    "Engine glow '" & m_EngineGlows(I).Name & "' refers to a parent that does not exist. Please fix.")

            m_EngineGlows(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To m_EngineGlows.Count - 1
    End Sub

    ''' <summary>
    ''' Reads the BURN chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadBURNChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        Dim burn As New EngineBurn
        burn.ReadIFF(IFF)
        m_EngineBurns.Add(burn)
    End Sub

    ''' <summary>
    ''' Writes the BURN chunk to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteBURNChunk(ByVal IFF As IFF.IFFWriter)
        For I As Integer = 0 To m_EngineBurns.Count - 1
            If Not m_RootJoint.Contains(m_EngineBurns(I).ParentName) Then _
                Trace.TraceWarning(
                    "Engine burn '" & m_EngineBurns(I).Name & "' refers to a parent that does not exist. Please fix.")

            m_EngineBurns(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To m_EngineBurns.Count - 1
    End Sub

    ''' <summary>
    ''' Reads the NAVL chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadNAVLChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        ' Get the number of navlights in the HOD.
        Dim count As Integer = IFF.ReadInt32()

        ' Read all.
        For I As Integer = 0 To count - 1
            Dim n As New NavLight
            n.ReadIFF(IFF)
            m_NavLights.Add(n)

        Next I ' For I As Integer = 0 To count - 1
    End Sub

    ''' <summary>
    ''' Writes the NAVL chunk to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteNAVLChunk(ByVal IFF As IFF.IFFWriter)
        ' Do not write NAVL chunk if no navlights.
        If m_NavLights.Count = 0 Then _
            Exit Sub

        IFF.Push("NAVL", Homeworld2.IFF.ChunkType.Normal, 3)
        IFF.WriteInt32(m_NavLights.Count)

        For I As Integer = 0 To m_NavLights.Count - 1
            m_NavLights(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To m_NavLights.Count - 1

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Reads the DOCK chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadDOCKChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        ' Get the dockpath count.
        Dim numPaths As Integer = IFF.ReadInt32()

        ' Read all paths.
        For I As Integer = 1 To numPaths
            Dim d As New Dockpath
            d.ReadIFF(IFF)
            m_Dockpaths.Add(d)

        Next I ' For I As Integer = 1 To numPaths
    End Sub

    ''' <summary>
    ''' Writes the DOCK chunk to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteDOCKChunk(ByVal IFF As IFF.IFFWriter)
        ' If no dockpaths, do nothing.
        If m_Dockpaths.Count = 0 Then _
            Exit Sub

        IFF.Push("DOCK")
        IFF.WriteInt32(m_Dockpaths.Count)

        For I As Integer = 0 To m_Dockpaths.Count - 1
            m_Dockpaths(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To m_Dockpaths.Count - 1

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Reads the MRKR chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadMRKRChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        Dim mrkr As New Marker
        mrkr.ReadIFF(IFF)
        m_Markers.Add(mrkr)
    End Sub

    ''' <summary>
    ''' Writes the MRKR chunk to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteMRKRChunk(ByVal IFF As IFF.IFFWriter)
        For I As Integer = 0 To m_Markers.Count - 1
            m_Markers(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To m_Markers.Count - 1
    End Sub

    ''' <summary>
    ''' Reads the STRF chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadSTRFChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        Dim strf As New StarField
        strf.ReadIFF(IFF)
        m_StarFields.Add(strf)
    End Sub

    ''' <summary>
    ''' Writes the BNDV chunk to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteSTRFChunk(ByVal IFF As IFF.IFFWriter)
        For I As Integer = 0 To m_StarFields.Count - 1
            m_StarFields(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To m_StarFields.Count - 1
    End Sub

    ''' <summary>
    ''' Reads the BGSG chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadBGSGChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        Dim bgsg As New StarFieldT
        bgsg.ReadIFF(IFF)
        m_StarFieldsT.Add(bgsg)
    End Sub

    ''' <summary>
    ''' Writes the BGSG chunk to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteBGSGChunk(ByVal IFF As IFF.IFFWriter)
        For I As Integer = 0 To m_StarFieldsT.Count - 1
            m_StarFieldsT(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To m_StarFieldsT.Count - 1
    End Sub

    ''' <summary>
    ''' Reads (actually does nothing) the BNDV chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadBNDVChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        ' Nothing here.
    End Sub

    ''' <summary>
    ''' Writes the BNDV chunk to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteBNDVChunk(ByVal IFF As IFF.IFFWriter)
        Dim min, max As Vector3

        ' Get the HOD extents.
        If Not GetHODExtents(min, max) Then _
            min = New Vector3(9999999.0F, 9999999.0F, 9999999.0F) _
                : max = New Vector3(- 9999999.0F, - 9999999.0F, - 9999999.0F)

        ' Write boundary values.
        IFF.Push("BNDV")

        IFF.Write("Whole")
        IFF.Write("Root")

        ' Write vertex information.
        IFF.WriteInt32(8)

        For I As Integer = 0 To 7
            Select Case I
                Case 0
                    IFF.Write(min.X)
                    IFF.Write(min.Y)
                    IFF.Write(min.Z)

                Case 6
                    IFF.Write(max.X)
                    IFF.Write(max.Y)
                    IFF.Write(max.Z)

                Case Else
                    IFF.Write(CSng(0))
                    IFF.Write(CSng(0))
                    IFF.Write(CSng(0))

            End Select ' Select Case I
        Next I ' For I As Integer = 0 To 7

        ' Write side information.
        IFF.WriteInt32(0)

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Reads the COLD chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadCOLDChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        Dim cm As New CollisionMesh
        cm.ReadIFF(IFF)
        m_CollisionMeshes.Add(cm)
    End Sub

    ''' <summary>
    ''' Writes the COLD chunk to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteCOLDChunk(ByVal IFF As IFF.IFFWriter)
        For I As Integer = 0 To m_CollisionMeshes.Count - 1
            If Not m_RootJoint.Contains(m_CollisionMeshes(I).Name) Then _
                Trace.TraceWarning(
                    "Collision mesh '" & m_CollisionMeshes(I).Name &
                    "' refers to a joint that does not exist. Please fix.")

            m_CollisionMeshes(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To m_CollisionMeshes.Count - 1
    End Sub

    ''' <summary>
    ''' Reads (actually does nothing) the BSRM chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadBSRMChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        ' Nothing here.
    End Sub

    ''' <summary>
    ''' Writes the BSRM chunk to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteBSRMChunk(ByVal IFF As IFF.IFFWriter)
        ' Write BSRM for multi meshes.
        For I As Integer = 0 To m_MultiMeshes.Count - 1
            BSRM.WriteMultiMesh(IFF, m_MultiMeshes(I))

        Next I ' For I As Integer = 0 To m_MultiMeshes.Count - 1

        ' Write BSRM for goblin meshes.
        For I As Integer = 0 To m_GoblinMeshes.Count - 1
            BSRM.WriteGoblinMesh(IFF, m_GoblinMeshes(I))
        Next I ' For I As Integer = 0 To m_GoblinMeshes.Count - 1
    End Sub

    ''' <summary>
    ''' Initializes the DTRM chunk.
    ''' </summary>
    Private Sub InitializeDTRM()
        Dim white As New Direct3D.ColorValue(255, 255, 255),
            red As New Direct3D.ColorValue(255, 0, 0),
            blue As New Direct3D.ColorValue(0, 0, 255)

        ' Create the joint meshes, if needed.
        If m_JointMesh Is Nothing Then _
            m_JointMesh = Standard.BasicMesh.MeshTemplates.CreateSphere(1, blue, blue, 7, 7) _
                : m_JointMesh.CreateThreeColourCrosshair(False) _
                : m_JointMesh.Part(1).Scaling(2, 2, 2) _
                : m_JointMesh.MergeAll()

        If m_JointLinkMesh Is Nothing Then _
            m_JointLinkMesh = Standard.BasicMesh.MeshTemplates.CreatePrism(1, 0, 1, 2, 7, blue, blue) _
                : m_JointLinkMesh.Translation(0, 0, 0.5F)

        ' Create the collision mesh boundary sphere, if needed.
        If m_CollisionMeshSphere Is Nothing Then _
            m_CollisionMeshSphere = Standard.BasicMesh.MeshTemplates.CreateSphere(1, red, red, 7, 7)

        ' Create the navlight meshes, if needed.
        If m_NavLightMesh Is Nothing Then _
            m_NavLightMesh = New GBasicMesh(Of PNVertex, Integer, Standard.Material) _
                : m_NavLightMesh.CreateSphere(1)

        ' Create the marker mesh, if needed.
        If m_MarkerMesh Is Nothing Then _
            m_MarkerMesh = New Standard.BasicMesh _
                : m_MarkerMesh.CreateThreeColourCrosshair() _
                : _
                m_MarkerMesh.Material(0) = New Standard.Material _
                    With {.Attributes = New Direct3D.Material With {.AmbientColor = white}}

        ' Create the dockpoint mesh, if needed.
        If m_DockpointMesh Is Nothing Then _
            m_DockpointMesh = Standard.BasicMesh.MeshTemplates.CreatePrism(1, 0, 0.4, 1, 12, white, white, 30, 120)

        If m_DockpointToleranceMesh Is Nothing Then _
            m_DockpointToleranceMesh = Standard.BasicMesh.MeshTemplates.CreateSphere(1, red, red, 7, 7) _
                : _
                m_DockpointToleranceMesh.Material(0) = New Standard.Material _
                    With {.Attributes = New Direct3D.Material With {.EmissiveColor = red}}

        ' Unlock meshes.
        UnlockDTRM()

        ' Clear lists.
        m_RootJoint.Initialize("Root")
        m_RootJoint.Visible = True
        m_CollisionMeshes.Clear()
        m_EngineShapes.Clear()
        m_EngineGlows.Clear()
        m_EngineBurns.Clear()
        m_NavLights.Clear()
        m_Dockpaths.Clear()
        m_Markers.Clear()
        m_StarFields.Clear()
        m_StarFieldsT.Clear()

        ' Update properties.
        m_SkeletonVisible = False
        m_SkeletonScale = 1
        m_MarkerScale = 1
        m_DockpointScale = 1
    End Sub

    ''' <summary>
    ''' Updates the (chunks of the) DTRM chunk so that it can be rendered
    ''' properly.
    ''' </summary>
    Private Sub UpdateDTRM(ByVal Device As Direct3D.Device)
    End Sub

    ''' <summary>
    ''' Locks the meshes so that they can be rendered.
    ''' </summary>
    ''' <param name="Device">
    ''' The device in which the meshes will be rendered.
    ''' </param>
    Private Sub LockDTRM(ByVal Device As Direct3D.Device)
        m_JointMesh.Lock(Device)
        m_JointLinkMesh.Lock(Device)
        m_CollisionMeshSphere.Lock(Device)
        m_NavLightMesh.Lock(Device)
        m_MarkerMesh.Lock(Device)
        m_DockpointMesh.Lock(Device)
        m_DockpointToleranceMesh.Lock(Device)

        For I As Integer = 0 To m_CollisionMeshes.Count - 1
            m_CollisionMeshes(I).Lock(Device)

        Next I ' For I As Integer = 0 To m_CollisionMeshes.Count - 1

        For I As Integer = 0 To m_EngineShapes.Count - 1
            m_EngineShapes(I).Lock(Device)

        Next I ' For I As Integer = 0 To m_EngineShapes.Count - 1

        For I As Integer = 0 To m_EngineGlows.Count - 1
            m_EngineGlows(I).Mesh.Lock(Device)

        Next I ' For I As Integer = 0 To m_EngineGlows.Count - 1
    End Sub

    ''' <summary>
    ''' Unlocks the meshes so that they can be edited.
    ''' </summary>
    Private Sub UnlockDTRM()
        m_JointMesh.Unlock()
        m_JointLinkMesh.Unlock()
        m_CollisionMeshSphere.Unlock()
        m_NavLightMesh.Unlock()
        m_MarkerMesh.Unlock()
        m_DockpointMesh.Unlock()
        m_DockpointToleranceMesh.Unlock()

        For I As Integer = 0 To m_CollisionMeshes.Count - 1
            m_CollisionMeshes(I).Unlock()

        Next I ' For I As Integer = 0 To m_CollisionMeshes.Count - 1

        For I As Integer = 0 To m_EngineShapes.Count - 1
            m_EngineShapes(I).Unlock()

        Next I ' For I As Integer = 0 To m_EngineShapes.Count - 1

        For I As Integer = 0 To m_EngineGlows.Count - 1
            m_EngineGlows(I).Mesh.Unlock()

        Next I ' For I As Integer = 0 To m_EngineGlows.Count - 1
    End Sub

    ''' <summary>
    ''' Renders the DTRM contents, actual rendered meshes depend on which
    ''' type of HOD this is.
    ''' </summary>
    ''' <param name="Device">
    ''' The device to render to.
    ''' </param>
    ''' <param name="Transform">
    ''' The transform to use.
    ''' </param>
    Private Sub RenderDTRM(ByVal Device As Direct3D.Device, ByVal Transform As Matrix)
        ' Perform multi-mesh specific rendering.
        If (m_Name = Name_MultiMesh) OrElse (m_Name = Name_VariableMesh) Then
            ' Render skeleton only for multi mesh HODs.
            RenderSkeleton(Device, Transform)

            ' Render collision meshes.
            For I As Integer = 0 To m_CollisionMeshes.Count - 1
                If m_CollisionMeshes(I).Visible Then
                    ' Get the parent joint.
                    Dim j As Joint = m_RootJoint.GetJointByName(m_CollisionMeshes(I).Name)

                    ' If it does not exist, use root.
                    If j Is Nothing Then _
                        j = m_RootJoint

                    ' Get the net transform.
                    Dim xform As Matrix = j.Transform*Transform

                    ' Set transform.
                    Device.Transform.World = xform

                    ' Render.
                    m_CollisionMeshes(I).Render(Device)
                    m_CollisionMeshes(I).RenderBox(Device, xform)
                    m_CollisionMeshes(I).RenderSphere(Device, m_CollisionMeshSphere, xform)

                End If ' If m_CollisionMeshes(I).Visible Then
            Next I ' For I As Integer = 0 To m_CollisionMeshes.Count - 1

            ' Render engine shapes.
            For I As Integer = 0 To m_EngineShapes.Count - 1
                If m_EngineShapes(I).Visible Then
                    ' Get the parent joint.
                    Dim j As Joint = m_RootJoint.GetJointByName(m_EngineShapes(I).ParentName)

                    ' If it does not exist, use root.
                    If j Is Nothing Then _
                        j = m_RootJoint

                    ' Set transform.
                    Device.Transform.World = j.Transform*Transform

                    ' Render.
                    m_EngineShapes(I).Render(Device)

                End If ' If m_EngineShapes(I).Visible Then
            Next I ' For I As Integer = 0 To m_EngineShapes.Count - 1

            ' Render engine glows.
            For I As Integer = 0 To m_EngineGlows.Count - 1
                ' Get the parent joint.
                Dim j As Joint = m_RootJoint.GetJointByName(m_EngineGlows(I).ParentName)

                ' If it does not exist, use root.
                If j Is Nothing Then _
                    j = m_RootJoint

                ' Set transform.
                Device.Transform.World = j.Transform*Transform

                ' Render.
                m_EngineGlows(I).Render(Device)

            Next I ' For I As Integer = 0 To m_EngineGlows.Count - 1

            ' Render engine burns.
            For I As Integer = 0 To m_EngineBurns.Count - 1
                ' Get the parent joint.
                Dim j As Joint = m_RootJoint.GetJointByName(m_EngineBurns(I).ParentName)

                ' If it does not exist, use root.
                If j Is Nothing Then _
                    j = m_RootJoint

                ' Set transform.
                Device.Transform.World = j.Transform*Transform

                ' Render.
                m_EngineBurns(I).Render(Device)

            Next I ' For I As Integer = 0 To m_EngineBurns.Count - 1

            ' Render navlights.
            For I As Integer = 0 To m_NavLights.Count - 1
                ' Get the parent joint.
                Dim j As Joint = m_RootJoint.GetJointByName(m_NavLights(I).Name)

                ' If it does not exist, use root.
                If j Is Nothing Then _
                    j = m_RootJoint

                ' Render.
                m_NavLights(I).Render(Device, j.Transform*Transform, m_NavLightMesh)

            Next I ' For I As Integer = 0 To m_NavLights.Count - 1

            ' Render markers.
            For I As Integer = 0 To m_Markers.Count - 1
                ' Get the parent joint.
                Dim j As Joint = m_RootJoint.GetJointByName(m_Markers(I).ParentName)

                ' If it does not exist, use root.
                If j Is Nothing Then _
                    j = m_RootJoint

                ' Render.
                m_Markers(I).Render(Device, j.Transform*Transform, m_MarkerMesh, m_MarkerScale)

            Next I ' For I As Integer = 0 To m_Markers.Count - 1

            ' Render dockpaths and dockpoints.
            For I As Integer = 0 To m_Dockpaths.Count - 1
                ' Get the parent joint.
                Dim j As Joint = m_RootJoint.GetJointByName(m_Dockpaths(I).ParentName)

                ' If it does not exist, use root.
                If j Is Nothing Then _
                    j = m_RootJoint

                ' Render.
                m_Dockpaths(I).Render(Device, j.Transform*Transform, m_DockpointMesh, m_DockpointToleranceMesh,
                                      m_DockpointScale)

            Next I ' For I As Integer = 0 To m_Dockpaths.Count - 1
        End If ' If (m_Name = Name_MultiMesh) OrElse (m_Name = Name_VariableMesh) Then

        ' Render star fields.
        For I As Integer = 0 To m_StarFields.Count - 1
            m_StarFields(I).Render(Device, Transform)

        Next I ' For I As Integer = 0 To m_StarFields.Count - 1

        ' Render textured star fields.
        For I As Integer = 0 To m_StarFieldsT.Count - 1
            m_StarFieldsT(I).Render(Device, Transform)

        Next I ' For I As Integer = 0 To m_StarFieldsT.Count - 1
    End Sub

    ''' <summary>
    ''' Renders the joint skeleton.
    ''' </summary>
    Private Sub RenderSkeleton(ByVal Device As Direct3D.Device, ByVal Transform As Matrix)
        ' Render skeleton if needed.
        If Not m_SkeletonVisible Then _
            Exit Sub

        With Device
            ' Get the viewport.
            Dim v As Direct3D.Viewport = .Viewport,
                oldV As Direct3D.Viewport = .Viewport

            ' Limit the Z range to render into foreground.
            v.MinZ = 0
            v.MaxZ = 0

            ' Set the viewport.
            .Viewport = v

            ' Render skeleton.
            m_RootJoint.Render(Device, Transform, m_JointMesh, m_JointLinkMesh, m_SkeletonScale)

            ' Set the old viewport.
            .Viewport = oldV

        End With ' With Device
    End Sub
End Class
