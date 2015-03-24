''' <summary>
''' Class representing Homeworld2 Variable mesh.
''' </summary>
''' <remarks>
''' This is an unsupported mesh format.
''' </remarks>
Public NotInheritable Class VariableMesh
    Private Structure Wedge
        Public VertexNo As Integer
        Public Normal As Vector3
        Public UV As Vector2
        Public Colour As Integer

        Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader)
            VertexNo = IFF.ReadInt32()

            Normal.X = IFF.ReadSingle()
            Normal.Y = IFF.ReadSingle()
            Normal.Z = IFF.ReadSingle()

            UV.X = IFF.ReadSingle()
            UV.Y = IFF.ReadSingle()

            Colour = IFF.ReadInt32()
        End Sub

        Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
            IFF.WriteInt32(VertexNo)

            IFF.Write(Normal.X)
            IFF.Write(Normal.Y)
            IFF.Write(Normal.Z)

            IFF.Write(UV.X)
            IFF.Write(UV.Y)

            IFF.WriteInt32(Colour)
        End Sub
    End Structure

    Private Structure Face
        Public MaterialNo As Integer

        Public Wedge1 As Integer
        Public Wedge2 As Integer
        Public Wedge3 As Integer

        Public Face1 As Integer
        Public Face2 As Integer
        Public Face3 As Integer

        Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader)
            MaterialNo = IFF.ReadInt32()

            Wedge1 = IFF.ReadInt32()
            Wedge2 = IFF.ReadInt32()
            Wedge3 = IFF.ReadInt32()

            Face1 = IFF.ReadInt32()
            Face2 = IFF.ReadInt32()
            Face3 = IFF.ReadInt32()
        End Sub

        Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
            IFF.WriteInt32(MaterialNo)

            IFF.WriteInt32(Wedge1)
            IFF.WriteInt32(Wedge2)
            IFF.WriteInt32(Wedge3)

            IFF.WriteInt32(Face1)
            IFF.WriteInt32(Face2)
            IFF.WriteInt32(Face3)
        End Sub
    End Structure

    Private Structure CollapseFixUp
        Public Type As Integer
        Public Data0 As Integer
        Public Data1 As Short
        Public Data2 As Short

        Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader)
            Type = IFF.ReadInt32()
            Data0 = IFF.ReadInt32()
            Data1 = IFF.ReadInt16()
            Data2 = IFF.ReadInt16()
        End Sub

        Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
            IFF.WriteInt32(Type)
            IFF.WriteInt32(Data0)
            IFF.WriteInt32(Data1)
            IFF.WriteInt32(Data2)
        End Sub
    End Structure

    Private Structure Collapse
        Public FixUps() As CollapseFixUp

        Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader)
            Dim Count As Integer = IFF.ReadInt32()

            If Count < 0 Then _
                Trace.TraceError("VARY chunk seems to be corrupted.") _
                    : ReDim FixUps(- 1) _
                    : Exit Sub

            ReDim FixUps(Count - 1)

            For I As Integer = 0 To Count - 1
                FixUps(I).ReadIFF(IFF)

            Next I ' For I As Integer = 0 To Count - 1
        End Sub

        Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
            If FixUps Is Nothing Then _
                IFF.WriteInt32(0) _
                    : Exit Sub

            IFF.WriteInt32(FixUps.Length)

            For I As Integer = 0 To FixUps.Length - 1
                FixUps(I).WriteIFF(IFF)

            Next I ' For I As Integer = 0 To FixUps.Length - 1
        End Sub
    End Structure

    Private m_Name As String
    Private m_ParentName As String
    Private m_NumUVSets As Integer

    Private m_Vertices() As Vector3
    Private m_Wedges() As Wedge
    Private m_Faces() As Face
    Private m_Collapses() As Collapse

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    Friend Sub New()
        ' Nothing here.
    End Sub

    ''' <summary>
    ''' Copy constructor.
    ''' </summary>
    Friend Sub New(ByVal vm As VariableMesh)
        m_Name = vm.m_Name
        m_ParentName = vm.m_ParentName
        m_NumUVSets = vm.m_NumUVSets

        If vm.m_Vertices.Length <> 0 Then _
            ReDim m_Vertices(vm.m_Vertices.Length - 1) _
                : vm.m_Vertices.CopyTo(m_Vertices, 0) _
            Else _
            ReDim m_Vertices(- 1)

        If vm.m_Wedges.Length <> 0 Then _
            ReDim m_Wedges(vm.m_Wedges.Length - 1) _
                : vm.m_Wedges.CopyTo(m_Wedges, 0) _
            Else _
            ReDim m_Wedges(- 1)

        If vm.m_Faces.Length <> 0 Then _
            ReDim m_Faces(vm.m_Faces.Length - 1) _
                : vm.m_Faces.CopyTo(m_Faces, 0) _
            Else _
            ReDim m_Faces(- 1)

        If vm.m_Collapses.Length = 0 Then _
            ReDim m_Collapses(- 1) _
                : Exit Sub

        ReDim m_Collapses(vm.m_Collapses.Length - 1)

        For I As Integer = 0 To m_Collapses.Length - 1
            If vm.m_Collapses(I).FixUps.Length <> 0 Then _
                ReDim m_Collapses(I).FixUps(vm.m_Collapses(I).FixUps.Length - 1) _
                    : vm.m_Collapses(I).FixUps.CopyTo(m_Collapses(I).FixUps, 0) _
                Else _
                ReDim m_Collapses(I).FixUps(- 1)

        Next I ' For I As Integer = 0 To m_Collapses.Length - 1
    End Sub

    ''' <summary>
    ''' Reads a variable mesh from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Chunk attributes.
    ''' </param>
    Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        Dim VtxCount As Integer,
            WedgeCount As Integer,
            FaceCount As Integer,
            CollapseCount As Integer

        m_Name = IFF.ReadString()
        m_ParentName = IFF.ReadString()

        If (m_ParentName Is Nothing) OrElse (m_ParentName = "") Then _
            m_ParentName = "Root"

        If (m_Name Is Nothing) OrElse (m_Name = "") Then _
            m_Name = m_ParentName & "_mesh"

        VtxCount = IFF.ReadInt32()
        WedgeCount = IFF.ReadInt32()
        FaceCount = IFF.ReadInt32()
        CollapseCount = IFF.ReadInt32()
        m_NumUVSets = IFF.ReadInt32()

        If (VtxCount < 0) OrElse (WedgeCount < 0) OrElse (FaceCount < 0) OrElse
           (CollapseCount < 0) OrElse (m_NumUVSets <> 1) Then _
            Erase m_Vertices _
                : Erase m_Wedges _
                : Erase m_Faces _
                : Erase m_Collapses _
                : m_NumUVSets = 1 _
                : Trace.TraceError("VARY chunk seems to be corrupted.") _
                : Exit Sub

        ReDim m_Vertices(VtxCount - 1)
        ReDim m_Wedges(WedgeCount - 1)
        ReDim m_Faces(FaceCount - 1)
        ReDim m_Collapses(CollapseCount - 1)

        For I As Integer = 0 To VtxCount - 1
            m_Vertices(I).X = IFF.ReadSingle()
            m_Vertices(I).Y = IFF.ReadSingle()
            m_Vertices(I).Z = IFF.ReadSingle()

        Next I ' For I As Integer = 0 To VtxCount - 1

        For I As Integer = 0 To WedgeCount - 1
            m_Wedges(I).ReadIFF(IFF)

        Next I ' For I As Integer = 0 To WedgeCount - 1

        For I As Integer = 0 To FaceCount - 1
            m_Faces(I).ReadIFF(IFF)

        Next I ' For I As Integer = 0 To FaceCount - 1

        For I As Integer = 0 To CollapseCount - 1
            m_Collapses(I).ReadIFF(IFF)

        Next I ' For I As Integer = 0 To CollapseCount - 1
    End Sub

    ''' <summary>
    ''' Writes the texture to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
        Dim VtxCount As Integer,
            WedgeCount As Integer,
            FaceCount As Integer,
            CollapseCount As Integer

        If m_Vertices Is Nothing Then _
            VtxCount = 0 _
            Else _
            VtxCount = m_Vertices.Length

        If m_Wedges Is Nothing Then _
            WedgeCount = 0 _
            Else _
            WedgeCount = m_Wedges.Length

        If m_Faces Is Nothing Then _
            FaceCount = 0 _
            Else _
            FaceCount = m_Faces.Length

        If m_Collapses Is Nothing Then _
            CollapseCount = 0 _
            Else _
            CollapseCount = m_Collapses.Length

        IFF.Push("VARY")

        IFF.Write(m_Name)
        IFF.Write(m_ParentName)

        IFF.WriteInt32(VtxCount)
        IFF.WriteInt32(WedgeCount)
        IFF.WriteInt32(FaceCount)
        IFF.WriteInt32(CollapseCount)
        IFF.WriteInt32(m_NumUVSets)

        For I As Integer = 0 To VtxCount - 1
            IFF.Write(m_Vertices(I).X)
            IFF.Write(m_Vertices(I).Y)
            IFF.Write(m_Vertices(I).Z)

        Next I ' For I As Integer = 0 To VtxCount - 1

        For I As Integer = 0 To WedgeCount - 1
            m_Wedges(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To WedgeCount - 1

        For I As Integer = 0 To FaceCount - 1
            m_Faces(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To FaceCount - 1

        For I As Integer = 0 To CollapseCount - 1
            m_Collapses(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To CollapseCount - 1

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Returns the rectangular parallelopiped in which the mesh can
    ''' be enclosed.
    ''' </summary>
    ''' <param name="MinExtents">
    ''' Variable to return the minimum extents.
    ''' </param>
    ''' <param name="MaxExtents">
    ''' Variable to return the maximum extents.
    ''' </param>
    ''' <remarks>
    ''' If <c>MinExtents</c> &lt; <c>MaxExtents</c> then mesh has no vertices.
    ''' If <c>MinExtents</c> = <c>MaxExtents</c> then mesh has 1 vertex.
    ''' Otherwise, atleast one component of <c>MinExtents</c> &lt; <c>MaxExtents</c>.
    ''' </remarks>
    Friend Function GetMeshExtents(ByRef MinExtents As Vector3, ByRef MaxExtents As Vector3) As Boolean
        ' Check if we have verices.
        If m_Vertices.Length = 0 Then _
            MinExtents = New Vector3(1, 1, 1) _
                : MaxExtents = New Vector3(- 1, - 1, - 1) _
                : Return False

        ' Get the first vertex.
        MinExtents = m_Vertices(0)
        MaxExtents = m_Vertices(0)

        ' Get the extents for the remaining vertices.
        For I As Integer = 1 To m_Vertices.Length - 1
            MinExtents.Minimize(m_Vertices(I))
            MaxExtents.Maximize(m_Vertices(I))

        Next I ' For I As Integer = 1 To m_Vertices.Length - 1

        Return True
    End Function

    ''' <summary>
    ''' Calculates the sphere in which the mesh can be enclosed.
    ''' </summary>
    ''' <param name="Center">
    ''' Center of the mesh.
    ''' </param>
    ''' <param name="Radius">
    ''' Radius of the mesh.
    ''' </param>
    ''' <remarks>
    ''' If <c>Center</c> = {0, 0, 0} and <c>Radius</c> = 0 then mesh has no vertices.
    ''' If <c>Center</c> &lt;&gt; {0, 0, 0} and <c>Radius</c> = 0 then mesh has 1 vertex.
    ''' Otherwise, the <c>Center</c> and <c>Radius</c> are correct.
    ''' </remarks>
    Friend Function GetMeshSphere(ByRef Center As Vector3, ByRef Radius As Single) As Boolean
        Dim Min, Max As Vector3

        If m_Vertices.Length = 0 Then _
            Center = New Vector3(0, 0, 0) _
                : Radius = 0 _
                : Return False

        ' Get the mesh extents.
        GetMeshExtents(Min, Max)

        ' Get the center.
        Center = (Min + Max)*0.5

        ' Get the radius.
        Radius = (Max - Min).Length()/2

        Return True
    End Function

    ''' <summary>
    ''' Converts this variable mesh to a multi mesh.
    ''' </summary>
    ''' <returns>
    ''' The multi mesh containing the (approximate?) contents
    ''' of this variable mesh.
    ''' </returns>
    ''' <remarks>
    ''' Because variable meshes are not fully supported, the
    ''' returned mesh may not be an exact conversion.
    ''' </remarks>
    Public Function ToMultiMesh() As MultiMesh
        Dim MM As New MultiMesh
        Dim BM As New BasicMesh

        MM.Name = m_Name
        MM.ParentName = m_ParentName
        MM.LOD.Add(BM)

        With BM
            For I As Integer = 0 To m_Faces.Length - 1
                Dim Material As Integer = m_Faces(I).MaterialNo
                Dim WedgeIndex() As Integer = {0, 0, 0}
                Dim FaceIndex() As Integer = {0, 0, 0}

                WedgeIndex(0) = m_Faces(I).Wedge1
                WedgeIndex(1) = m_Faces(I).Wedge2
                WedgeIndex(2) = m_Faces(I).Wedge3

                FaceIndex(0) = m_Faces(I).Face1
                FaceIndex(1) = m_Faces(I).Face2
                FaceIndex(2) = m_Faces(I).Face3

                Dim PartIndex As Integer = - 1

                For J As Integer = 0 To .PartCount - 1
                    If .Part(J).Material.Index = Material Then _
                        PartIndex = J _
                            : Exit For

                Next J ' For J As Integer = 0 To .PartCount - 1

                If PartIndex = - 1 Then _
                    PartIndex = .PartCount _
                        : .PartCount += 1 _
                        : .Part(PartIndex).Material.Initialize() _
                        : .Part(PartIndex).Material.Index = PartIndex _
                        : .Part(PartIndex).Material.VertexMask = VertexMasks.Position Or VertexMasks.Normal Or
                                                                 VertexMasks.Texture0 Or VertexMasks.Texture1 Or
                                                                 VertexMasks.Colour _
                        : .Part(PartIndex).PrimitiveGroupCount = 1 _
                        : .Part(PartIndex).PrimitiveGroups(0).Type = Direct3D.PrimitiveType.TriangleList

                With .Part(PartIndex)
                    For J As Integer = 0 To 2
                        Dim V(0) As BasicVertex, Ind(0) As UInteger

                        V(0).Initialize()
                        V(0).Position = m_Vertices(m_Wedges(WedgeIndex(J)).VertexNo)
                        V(0).Normal = m_Wedges(WedgeIndex(J)).Normal
                        V(0).Tex = m_Wedges(WedgeIndex(J)).UV
                        V(0).Diffuse = m_Wedges(WedgeIndex(J)).Colour

                        Ind(0) = CUShort(.Vertices.Count)

                        .Vertices.Append(V)
                        .PrimitiveGroups(0).Append(Ind)

                    Next J ' For J As Integer = 0 To 2
                End With ' With .Part(PartIndex)
            Next I ' For I As Integer = 0 To m_Faces.Length - 1
        End With ' With BM

        Return MM
    End Function
End Class
