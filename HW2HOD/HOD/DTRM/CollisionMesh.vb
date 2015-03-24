Imports GenericMesh

''' <summary>
''' Class representing a Homeworld2 collision mesh.
''' </summary>
Public NotInheritable Class CollisionMesh
    Inherits GBasicMesh(Of PNVertex, UShort, Material.Default)

    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Name.</summary>
    Private m_Name As String

    ''' <summary>Whether the mesh is visible or not.</summary>
    Private m_Visible As Boolean

    ''' <summary>Minimum extents.</summary>
    Private m_Min As Vector3

    ''' <summary>Maximum extents.</summary>
    Private m_Max As Vector3

    ''' <summary>Center.</summary>
    Private m_Center As Vector3

    ''' <summary>Radius.</summary>
    Private m_Radius As Single

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
    ''' Copy constructor.
    ''' </summary>
    Public Sub New(ByVal cm As CollisionMesh)
        ' Call base contructor.
        MyBase.New(cm)

        m_Name = cm.m_Name
        m_Visible = cm.m_Visible
        m_Min = cm.m_Min
        m_Max = cm.m_Max
        m_Center = cm.m_Center
        m_Radius = cm.m_Radius
    End Sub

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns\Sets name of collision mesh, also the name of joint
    ''' under which it is parented.
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
    ''' Returns\Sets the minimum extents of the bounding box of this collision mesh.
    ''' </summary>
    Public Property MinimumExtents() As Vector3
        Get
            Return m_Min
        End Get

        Set(ByVal value As Vector3)
            m_Min = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets the maximum extents of the bounding box of this collision mesh.
    ''' </summary>
    Public Property MaximumExtents() As Vector3
        Get
            Return m_Max
        End Get

        Set(ByVal value As Vector3)
            m_Max = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets the center of the boundary sphere of this collision mesh.
    ''' </summary>
    Public Property Center() As Vector3
        Get
            Return m_Center
        End Get

        Set(ByVal value As Vector3)
            m_Center = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets the radius of the boundary sphere of this collision mesh.
    ''' </summary>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when radius is negative.
    ''' </exception>
    Public Property Radius() As Single
        Get
            Return m_Radius
        End Get

        Set(ByVal value As Single)
            If value < 0 Then _
                Throw New ArgumentOutOfRangeException("value") _
                    : Exit Property

            m_Radius = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets whether the mesh is visible or not.
    ''' </summary>
    Friend Property Visible() As Boolean
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
    ''' Returns the name of this mesh.
    ''' </summary>
    Public Overrides Function ToString() As String
        Return m_Name
    End Function

    ''' <summary>
    ''' Reads the collision mesh from an IFF reader.
    ''' </summary>
    Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader)
        ' Remove old data.
        Initialize()

        ' Add handlers.
        IFF.AddHandler("BBOX", Homeworld2.IFF.ChunkType.Default, AddressOf ReadBBOXChunk)
        IFF.AddHandler("BSPH", Homeworld2.IFF.ChunkType.Default, AddressOf ReadBSPHChunk)
        IFF.AddHandler("TRIS", Homeworld2.IFF.ChunkType.Default, AddressOf ReadTRISChunk)

        ' Read name.
        m_Name = IFF.ReadString()

        ' Read the file.
        IFF.Parse()
    End Sub

    ''' <summary>
    ''' Writes the collision mesh to an IFF writer.
    ''' </summary>
    Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
        ' Prepare mesh for export.
        PrepareMeshForExport()

        If (PartCount = 0) OrElse (TotalVertexCount = 0) OrElse (TotalIndiceCount = 0) Then _
            Trace.TraceError("Collision mesh '" & m_Name & "' has no data, please remove.") _
                : Exit Sub

        ' Write to HOD.
        IFF.Push("COLD", Homeworld2.IFF.ChunkType.Form)

        IFF.Write(m_Name)

        IFF.Push("BBOX")
        IFF.Write(m_Min.X)
        IFF.Write(m_Min.Y)
        IFF.Write(m_Min.Z)
        IFF.Write(m_Max.X)
        IFF.Write(m_Max.Y)
        IFF.Write(m_Max.Z)
        IFF.Pop() ' BBOX

        IFF.Push("BSPH")
        IFF.Write(m_Center.X)
        IFF.Write(m_Center.Y)
        IFF.Write(m_Center.Z)
        IFF.Write(m_Radius)
        IFF.Pop() ' BSPH

        IFF.Push("TRIS")
        IFF.Write(Part(0).Vertices.Count)

        For I As Integer = 0 To Part(0).Vertices.Count - 1
            Part(0).Vertices(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To Part(0).Vertices.Count - 1

        IFF.Write(Part(0).PrimitiveGroups(0).IndiceCount)

        For I As Integer = 0 To Part(0).PrimitiveGroups(0).IndiceCount - 1
            IFF.WriteUInt16(Part(0).PrimitiveGroups(0).Indice(I))

        Next I ' For I As Integer = 0 To Part(0).PrimitiveGroups(0).IndiceCount - 1

        IFF.Pop() ' TRIS

        IFF.Pop() ' COLD
    End Sub

    ''' <summary>
    ''' Prepares the collision detection mesh for export.
    ''' </summary>
    Private Sub PrepareMeshForExport()
        ' See if the mesh has any data.
        If (PartCount = 0) OrElse (TotalVertexCount = 0) OrElse (TotalIndiceCount = 0) Then _
            Exit Sub

        ' Merge all parts.
        Me.MergeAll()

        ' Convert to list.
        Me.Part(0).ConvertToList()

        ' Remove redundant primitive groups.
        For I As Integer = Me.Part(0).PrimitiveGroupCount - 1 To 0 Step - 1
            If Me.Part(0).PrimitiveGroups(I).Type <> Direct3D.PrimitiveType.TriangleList Then _
                Me.Part(0).RemovePrimitiveGroups(I)

        Next I ' For I As Integer = Me.Part(0).PrimitiveGroupCount - 1 To 0 Step -1

        ' See if any primitive group is left.
        If Me.Part(0).PrimitiveGroupCount = 0 Then _
            Exit Sub

        ' Prepare lists to accomplish two taks:
        ' - remove zero area triangles,
        ' - sort according to triangle area.
        Dim vtx As New List(Of PNVertex)
        Dim vtxDic As New Dictionary(Of PNVertex, UShort)
        Dim vtxArea As New List(Of Single)
        Dim ind As New List(Of UShort)

        With Me.Part(0)
            For I As Integer = 0 To .PrimitiveGroups(0).IndiceCount - 3 Step 3
                ' Get indices.
                Dim ind1 As Integer = .PrimitiveGroups(0).Indice(I),
                    ind2 As Integer = .PrimitiveGroups(0).Indice(I + 1),
                    ind3 As Integer = .PrimitiveGroups(0).Indice(I + 2)

                ' Get vertices.
                Dim v1 As PNVertex = .Vertices(ind1),
                    v2 As PNVertex = .Vertices(ind2),
                    v3 As PNVertex = .Vertices(ind3)

                ' If length is small then do not add triangle.
                If (v2.Position - v1.Position).LengthSq() < 0.01F Then _
                    Continue For

                ' If length is small then do not add triangle.
                If (v3.Position - v1.Position).LengthSq() < 0.01F Then _
                    Continue For

                ' Calculate area.
                Dim area As Single = 0.5F*Math.Abs(Vector3.Cross(v2.Position - v1.Position,
                                                                 v3.Position - v1.Position).Length())

                ' If area is small then do not add triangle.
                If area < 0.01F Then _
                    Continue For

                ' Find vertex 1.
                If vtxDic.ContainsKey(v1) Then _
                    ind1 = vtxDic.Item(v1) _
                    Else _
                    vtxDic.Add(v1, CUShort(vtx.Count)) _
                        : vtx.Add(v1) _
                        : ind1 = vtx.Count - 1

                ' Find vertex 2.
                If vtxDic.ContainsKey(v2) Then _
                    ind2 = vtxDic.Item(v2) _
                    Else _
                    vtxDic.Add(v2, CUShort(vtx.Count)) _
                        : vtx.Add(v2) _
                        : ind2 = vtx.Count - 1

                ' Find vertex 3.
                If vtxDic.ContainsKey(v3) Then _
                    ind3 = vtxDic.Item(v3) _
                    Else _
                    vtxDic.Add(v3, CUShort(vtx.Count)) _
                        : vtx.Add(v3) _
                        : ind3 = vtx.Count - 1

                ' Locate where it's to be added.
                Dim locn As Integer = 0

                ' Compare area with all vertices.
                For J As Integer = 0 To vtxArea.Count - 1
                    If area > vtxArea(J) Then _
                        locn += 1 _
                        Else _
                        Exit For

                Next J ' For J As Integer = 0 To vtxArea.Count - 1

                ' Add to area array.
                vtxArea.Insert(locn, area)

                ' Add indices.
                ind.InsertRange(3*locn, New UShort() {CUShort(ind1), CUShort(ind2), CUShort(ind3)})

            Next I ' For I As Integer = 0 To .PrimitiveGroups(0).IndiceCount - 3 Step 3

            ' Remove vertex and indice data.
            .Vertices.RemoveAll()
            .PrimitiveGroups(0).RemoveAll()

            ' Get the optimized data.
            Dim vtxA() As PNVertex = vtx.ToArray()
            Dim indA() As UShort = ind.ToArray()

            ' Remove lists.
            vtx.Clear()
            vtxDic.Clear()
            vtxArea.Clear()
            ind.Clear()

            vtx = Nothing
            vtxDic = Nothing
            vtxArea = Nothing
            ind = Nothing

            ' Add the optimized data.
            .Vertices.Append(vtxA)
            .PrimitiveGroups(0).Append(indA)

            ' Remove temporary data.
            Erase vtxA
            Erase indA

        End With ' With Me.Part(0)
    End Sub

    ''' <summary>
    ''' Reads (actually does nothing) the BBOX chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadBBOXChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        ' Read min\max.
        m_Min.X = IFF.ReadSingle()
        m_Min.Y = IFF.ReadSingle()
        m_Min.Z = IFF.ReadSingle()
        m_Max.X = IFF.ReadSingle()
        m_Max.Y = IFF.ReadSingle()
        m_Max.Z = IFF.ReadSingle()
    End Sub

    ''' <summary>
    ''' Reads (actually does nothing) the BSPH chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadBSPHChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        ' Read center\radius.
        m_Center.X = IFF.ReadSingle()
        m_Center.Y = IFF.ReadSingle()
        m_Center.Z = IFF.ReadSingle()
        m_Radius = IFF.ReadSingle()
    End Sub

    ''' <summary>
    ''' Reads (actually does nothing) the TRIS chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadTRISChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        ' Set part count.
        Me.PartCount = 1

        ' Read vertex count.
        Me.Part(0).Vertices.Count = IFF.ReadInt32()

        ' Read vertices.
        For I As Integer = 0 To Me.Part(0).Vertices.Count - 1
            Me.Part(0).Vertices(I) = PNVertex.ReadIFF(IFF)

        Next I ' For I As Integer = 0 To Me.Part(0).Vertices.Count - 1

        ' Add primitive group.
        Me.Part(0).PrimitiveGroupCount = 1
        Me.Part(0).PrimitiveGroups(0).Type = Direct3D.PrimitiveType.TriangleList
        Me.Part(0).PrimitiveGroups(0).IndiceCount = IFF.ReadInt32()

        ' Read indices.
        For I As Integer = 0 To Me.Part(0).PrimitiveGroups(0).IndiceCount - 1
            Me.Part(0).PrimitiveGroups(0).Indice(I) = IFF.ReadUInt16()

        Next I ' For I As Integer = 0 To Me.Part(0).PrimitiveGroups(0).IndiceCount - 1

        ' Calculate normals for rendering.
        Me.CalculateNormals()
    End Sub

    ''' <summary>
    ''' Renders the bounding box of this mesh.
    ''' </summary>
    Friend Sub RenderBox(ByVal Device As Direct3D.Device, ByVal Transform As Matrix)
        If Not m_Visible Then _
            Exit Sub

        ' Colour by which the bounding box will be rendered.
        Dim c As Integer = Drawing.Color.Yellow.ToArgb()

        ' Prepare the extreme verices.
        Dim v() As Direct3D.CustomVertex.PositionColored = { _
                                                               New Direct3D.CustomVertex.PositionColored(m_Min.X,
                                                                                                         m_Min.Y,
                                                                                                         m_Min.Z, c),
                                                               New Direct3D.CustomVertex.PositionColored(m_Max.X,
                                                                                                         m_Min.Y,
                                                                                                         m_Min.Z, c),
                                                               New Direct3D.CustomVertex.PositionColored(m_Max.X,
                                                                                                         m_Min.Y,
                                                                                                         m_Max.Z, c),
                                                               New Direct3D.CustomVertex.PositionColored(m_Min.X,
                                                                                                         m_Min.Y,
                                                                                                         m_Max.Z, c),
                                                               New Direct3D.CustomVertex.PositionColored(m_Min.X,
                                                                                                         m_Max.Y,
                                                                                                         m_Min.Z, c),
                                                               New Direct3D.CustomVertex.PositionColored(m_Max.X,
                                                                                                         m_Max.Y,
                                                                                                         m_Min.Z, c),
                                                               New Direct3D.CustomVertex.PositionColored(m_Max.X,
                                                                                                         m_Max.Y,
                                                                                                         m_Max.Z, c),
                                                               New Direct3D.CustomVertex.PositionColored(m_Min.X,
                                                                                                         m_Max.Y,
                                                                                                         m_Max.Z, c)}

        ' Prepare vertices to be rendered.
        Dim vtx() As Direct3D.CustomVertex.PositionColored = { _
                                                                 v(0), v(1), v(1), v(2), v(2), v(3), v(3), v(0),
                                                                 v(4), v(5), v(5), v(6), v(6), v(7), v(7), v(4),
                                                                 v(0), v(4), v(1), v(5), v(2), v(6), v(3), v(7)}

        ' Cache some states.
        Dim oldAmbient As Integer = Device.RenderState.AmbientColor,
            oldLighting As Boolean = Device.RenderState.Lighting

        ' Set some states.
        Device.RenderState.Ambient = Drawing.Color.Yellow
        Device.RenderState.Lighting = False

        ' Draw box.
        Device.Transform.World = Transform
        Device.VertexFormat = Direct3D.CustomVertex.PositionColored.Format
        Device.DrawUserPrimitives(Direct3D.PrimitiveType.LineList, 12, vtx)
        Device.VertexFormat = Direct3D.VertexFormats.None

        ' Set old states.
        Device.RenderState.AmbientColor = oldAmbient
        Device.RenderState.Lighting = oldLighting
    End Sub

    ''' <summary>
    ''' Renders the boundary sphere of this mesh.
    ''' </summary>
    Friend Sub RenderSphere(ByVal Device As Direct3D.Device, ByVal Sphere As Standard.BasicMesh,
                            ByVal Transform As Matrix)
        If Not m_Visible Then _
            Exit Sub

        Dim oldAmbient As Integer = Device.RenderState.AmbientColor,
            oldCullMode As Direct3D.Cull = Device.RenderState.CullMode,
            oldFillMode As Direct3D.FillMode = Device.RenderState.FillMode,
            oldLighting As Boolean = Device.RenderState.Lighting

        ' Set some states.
        Device.RenderState.Ambient = Drawing.Color.Red
        Device.RenderState.CullMode = Direct3D.Cull.None
        Device.RenderState.FillMode = Direct3D.FillMode.WireFrame
        Device.RenderState.Lighting = False
        Device.Transform.World = Matrix.Scaling(m_Radius, m_Radius, m_Radius)*
                                 Matrix.Translation(m_Center)*
                                 Transform

        ' Draw sphere.
        Sphere.Render(Device)

        ' Set old states.
        Device.RenderState.AmbientColor = oldAmbient
        Device.RenderState.CullMode = oldCullMode
        Device.RenderState.FillMode = oldFillMode
        Device.RenderState.Lighting = oldLighting
    End Sub

    ''' <summary>
    ''' Initializes the collision mesh.
    ''' </summary>
    Private Sub Initialize()
        Me.RemoveAll()

        m_Name = "Collision Mesh"
        m_Visible = False
    End Sub

    ''' <summary>
    ''' Calculates the min\max extents and the center\radius.
    ''' </summary>
    Public Sub CalculateExtents()
        ' Get extents and sphere.
        GetMeshExtents(m_Min, m_Max)
        GetMeshSphere(m_Center, m_Radius)
    End Sub
End Class
