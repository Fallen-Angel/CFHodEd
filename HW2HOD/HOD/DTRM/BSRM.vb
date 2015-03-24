''' <summary>
''' Class providing support for writing Homeworld2 Battle Scar Reference
''' Meshes (BSRMs) to IFF writers.
''' </summary>
Friend NotInheritable Class BSRM
    ''' <summary>
    ''' LOD level of a BSRM.
    ''' </summary>
    Private Enum LODLevel
        Goblins
        LOD1
    End Enum

    ''' <summary>
    ''' Prepares the basic mesh for export. Since this modifies the mesh,
    ''' care should be taken that the mesh is duplicated first.
    ''' </summary>
    Private Shared Sub PrepareBasicMesh(ByVal bm As BasicMesh)
        ' If no parts, no preparation.
        If bm.PartCount = 0 Then _
            Exit Sub

        ' Merge all parts.
        bm.MergeAll()

        ' Convert to list type.
        bm.Part(0).ConvertToList()

        ' Remove un-needed primitive groups.
        For I As Integer = bm.Part(0).PrimitiveGroupCount - 1 To 0 Step - 1
            If bm.Part(0).PrimitiveGroups(I).Type <> Direct3D.PrimitiveType.TriangleList Then _
                bm.Part(0).RemovePrimitiveGroups(I)

        Next I ' For I As Integer = bm.Part(0).PrimitiveGroupCount - 1 To 0 Step -1
    End Sub

    ''' <summary>
    ''' Writes the BSRM DESC chunk to an IFF writer.
    ''' </summary>
    Private Shared Sub WriteDesc(ByVal IFF As IFF.IFFWriter,
                                 ByVal name As String, ByVal parentName As String,
                                 ByVal LODLevel As LODLevel)

        IFF.Push("DESC", Homeworld2.IFF.ChunkType.Normal, 1000)
        IFF.Write(name)
        IFF.Write(parentName)
        IFF.WriteInt32(LODLevel)
        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Writes the BSRM boundary volume (BBOX and BSPH) to an IFF writer.
    ''' </summary>
    Private Shared Sub WriteBoundaryVolume(ByVal IFF As IFF.IFFWriter, ByVal mesh As BasicMesh)
        Dim min, max, center As Vector3
        Dim radius As Single

        ' Get mesh's bounding box.
        If Not mesh.GetMeshExtents(min, max) Then _
            min = Vector3.Empty _
                : max = Vector3.Empty

        ' Get mesh's boundary sphere.
        If Not mesh.GetMeshSphere(center, radius) Then _
            center = Vector3.Empty _
                : radius = 0.0F

        ' Write bounding box.
        IFF.Push("BBOX", Homeworld2.IFF.ChunkType.Normal, 1000)

        IFF.Write(min.X)
        IFF.Write(min.Y)
        IFF.Write(min.Z)

        IFF.Write(max.X)
        IFF.Write(max.Y)
        IFF.Write(max.Z)

        IFF.Pop()

        ' Write boundary sphere.
        IFF.Push("BSPH", Homeworld2.IFF.ChunkType.Normal, 1000)

        IFF.Write(center.X)
        IFF.Write(center.Y)
        IFF.Write(center.Z)

        IFF.Write(radius)

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Writes the triangle vertex positions and indices in the BSRM
    ''' to an IFF writer.
    ''' </summary>
    Private Shared Sub WriteTriangles(ByVal IFF As IFF.IFFWriter, ByVal bm As BasicMesh)
        IFF.Push("TRIS", Homeworld2.IFF.ChunkType.Normal, 1000)

        If (bm.PartCount <> 0) AndAlso (bm.TotalVertexCount <> 0) AndAlso (bm.TotalIndiceCount <> 0) Then
            ' Write vertices.
            IFF.WriteInt32(bm.Part(0).Vertices.Count)

            For I As Integer = 0 To bm.Part(0).Vertices.Count - 1
                With bm.Part(0).Vertices(I).Position
                    IFF.Write(.X)
                    IFF.Write(.Y)
                    IFF.Write(.Z)

                End With ' With bm.Part(0).Vertices(I).Position
            Next I ' For I As Integer = 0 To bm.Part(0).Vertices.Count - 1

            ' Write indices.
            With bm.Part(0).PrimitiveGroups(0)
                IFF.WriteInt32(.IndiceCount)

                For I As Integer = 0 To .IndiceCount - 1
                    IFF.WriteUInt16(.Indice(I))

                Next I ' For I As Integer = 0 To .IndiceCount - 1
            End With ' With bm.Part(0).PrimitiveGroups(0)

        Else ' If (bm.PartCount <> 0) AndAlso (bm.TotalVertexCount <> 0) AndAlso (bm.TotalIndiceCount <> 0) Then
            IFF.WriteInt32(0)
            IFF.WriteInt32(0)

        End If ' If (bm.PartCount <> 0) AndAlso (bm.TotalVertexCount <> 0) AndAlso (bm.TotalIndiceCount <> 0) Then

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Writes the triangle vertex positions and indices in the BSRM
    ''' to an IFF writer.
    ''' </summary>
    Private Shared Sub WriteNormals(ByVal IFF As IFF.IFFWriter, ByVal bm As BasicMesh)
        IFF.Push("VNRM", Homeworld2.IFF.ChunkType.Normal, 1000)

        If (bm.PartCount <> 0) AndAlso (bm.TotalVertexCount <> 0) AndAlso (bm.TotalIndiceCount <> 0) Then
            ' Write vertices.
            IFF.WriteInt32(bm.Part(0).Vertices.Count)

            For I As Integer = 0 To bm.Part(0).Vertices.Count - 1
                With bm.Part(0).Vertices(I).Normal
                    IFF.Write(.X)
                    IFF.Write(.Y)
                    IFF.Write(.Z)

                End With ' With bm.Part(0).Vertices(I).Normal
            Next I ' For I As Integer = 0 To bm.Part(0).Vertices.Count - 1

            ' Write indices.
            With bm.Part(0).PrimitiveGroups(0)
                IFF.WriteInt32(.IndiceCount)

                For I As Integer = 0 To .IndiceCount - 1
                    IFF.WriteUInt16(.Indice(I))

                Next I ' For I As Integer = 0 To .IndiceCount - 1
            End With ' With bm.Part(0).PrimitiveGroups(0)

        Else ' If (bm.PartCount <> 0) AndAlso (bm.TotalVertexCount <> 0) AndAlso (bm.TotalIndiceCount <> 0) Then
            IFF.WriteInt32(0)
            IFF.WriteInt32(0)

        End If ' If (bm.PartCount <> 0) AndAlso (bm.TotalVertexCount <> 0) AndAlso (bm.TotalIndiceCount <> 0) Then

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Writes the spatial subdivision contruct in the BSRM
    ''' to an IFF writer.
    ''' </summary>
    Private Shared Sub WriteSpatialSubdivision(ByVal IFF As IFF.IFFWriter, ByVal bm As BasicMesh)
        ' See if the mesh has any data. If not, write empty SSUB chunk.
        If (bm.PartCount = 0) OrElse (bm.TotalVertexCount = 0) OrElse (bm.TotalIndiceCount = 0) Then _
            IFF.Push("SSUB", Homeworld2.IFF.ChunkType.Normal, 1000) _
                : IFF.WriteInt32(0) _
                : IFF.WriteInt32(0) _
                : IFF.WriteInt32(0) _
                : IFF.Pop() _
                : Exit Sub

        ' Average number of triangles in a cell. Although this value was originally
        ' ten, due to the cheap algorithms used here (not just in terms of running time),
        ' this was decreased to five.
        Const AvgTrisPerCell As Integer = 5

        ' Min\Max extents of mesh.
        Dim min, max As Vector3

        ' Find out number of triangles.
        Dim numTris As Integer = bm.TotalIndiceCount\3

        ' Find out approximate number of cells.
        Dim numCellsGuess As Integer = numTris\AvgTrisPerCell

        ' Get extents.
        bm.GetMeshExtents(min, max)

        ' Find out cell size.
        Dim cellSize As Vector3 = max - min

        ' Find out number of cells.
        Dim numCells As Integer = 1

        ' Find out number of divisions.
        Dim xDiv As Integer = 1,
            yDiv As Integer = 1,
            zDiv As Integer = 1

        ' Sub-divide until we have atleast the number of cells we guessed.
        Do While numCells < numCellsGuess
            ' Sub-divide along the longest dimension.
            If (cellSize.X > cellSize.Y) AndAlso (cellSize.X > cellSize.Z) Then _
                cellSize.X *= 0.5F _
                    : numCells *= 2 _
                    : xDiv *= 2 _
                    : Continue Do

            ' Check if Y is the longest dimension.
            If (cellSize.Y > cellSize.X) AndAlso (cellSize.Y > cellSize.Z) Then _
                cellSize.Y *= 0.5F _
                    : numCells *= 2 _
                    : yDiv *= 2 _
                    : Continue Do

            ' Z is the longest dimension.
            cellSize.Z *= 0.5F
            numCells *= 2
            zDiv *= 2

        Loop ' Do While numCells < numCellsGuess

        ' Assuming that this is an optimal sub-division (although it may not be), write it.
        IFF.Push("SSUB", Homeworld2.IFF.ChunkType.Normal, 1000)

        IFF.WriteInt32(xDiv)
        IFF.WriteInt32(yDiv)
        IFF.WriteInt32(zDiv)

        For Z As Integer = 0 To zDiv - 1
            For Y As Integer = 0 To yDiv - 1
                For X As Integer = 0 To xDiv - 1
                    ' Get the cell extents.
                    Dim cellMin As Vector3 = min + New Vector3(X*cellSize.X, Y*cellSize.Y, Z*cellSize.Z)
                    Dim cellMax As Vector3 = cellMin + cellSize

                    ' Make a new list of indices.
                    Dim l As New List(Of UShort)

                    ' Get the triangles in cell.
                    With bm.Part(0)
                        For I As Integer = 0 To .PrimitiveGroups(0).IndiceCount - 3 Step 3
                            ' Get indices.
                            Dim ind1 As Integer = .PrimitiveGroups(0).Indice(I),
                                ind2 As Integer = .PrimitiveGroups(0).Indice(I + 1),
                                ind3 As Integer = .PrimitiveGroups(0).Indice(I + 2)

                            ' Get vertices.
                            Dim v1 As Vector3 = .Vertices(ind1).Position,
                                v2 As Vector3 = .Vertices(ind2).Position,
                                v3 As Vector3 = .Vertices(ind3).Position

                            ' See if it lies in cell.
                            If (cellMin.X <= v1.X) AndAlso (cellMin.Y <= v1.Y) AndAlso (cellMin.Z <= v1.Z) AndAlso
                               (cellMax.X >= v1.X) AndAlso (cellMax.Y >= v1.Y) AndAlso (cellMax.Z >= v1.Z) Then _
                                l.Add(CUShort(I\3)) _
                                    : Continue For

                            ' See if it lies in cell.
                            If (cellMin.X <= v2.X) AndAlso (cellMin.Y <= v2.Y) AndAlso (cellMin.Z <= v2.Z) AndAlso
                               (cellMax.X >= v2.X) AndAlso (cellMax.Y >= v2.Y) AndAlso (cellMax.Z >= v2.Z) Then _
                                l.Add(CUShort(I\3)) _
                                    : Continue For

                            ' See if it lies in cell.
                            If (cellMin.X <= v3.X) AndAlso (cellMin.Y <= v3.Y) AndAlso (cellMin.Z <= v3.Z) AndAlso
                               (cellMax.X >= v3.X) AndAlso (cellMax.Y >= v3.Y) AndAlso (cellMax.Z >= v3.Z) Then _
                                l.Add(CUShort(I\3)) _
                                    : Continue For

                            ' Get center.
                            Dim v As Vector3 = (1.0F/3.0F)*(v1 + v2 + v3)

                            ' See if it lies in cell.
                            If (cellMin.X <= v.X) AndAlso (cellMin.Y <= v.Y) AndAlso (cellMin.Z <= v.Z) AndAlso
                               (cellMax.X >= v.X) AndAlso (cellMax.Y >= v.Y) AndAlso (cellMax.Z >= v.Z) Then _
                                l.Add(CUShort(I\3))

                        Next I ' For I As Integer = 0 To .PrimitiveGroups(0).IndiceCount - 1 Step 3
                    End With ' With bm.Part(0)

                    ' Write it.
                    IFF.WriteInt32(l.Count)

                    ' Write indices.
                    For I As Integer = 0 To l.Count - 1
                        IFF.WriteUInt16(l(I))

                    Next I ' For I As Integer = 0 To l.Count - 1
                Next X ' For X As Integer = 0 To xDiv - 1
            Next ' For Y As Integer = 0 To yDiv - 1
        Next ' For Z As Integer = 0 To zDiv - 1

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Writes the BSRM of a goblin mesh to the IFF writer.
    ''' </summary>
    Friend Shared Sub WriteGoblinMesh(ByVal IFF As IFF.IFFWriter, ByVal gm As GoblinMesh)
        If (gm.Mesh.PartCount = 0) OrElse
           (gm.Mesh.TotalVertexCount = 0) OrElse
           (gm.Mesh.TotalIndiceCount = 0) Then _
            Trace.TraceWarning("Goblin mesh '" & gm.ToString() & "' has no data. Please fix.") _
                : Exit Sub

        ' Duplicate the basic mesh.
        Dim bm As New BasicMesh(gm.Mesh)

        ' Prepare.
        PrepareBasicMesh(bm)

        IFF.Push("BSRM", Homeworld2.IFF.ChunkType.Form)

        ' Write DESC.
        WriteDesc(IFF, gm.Name, gm.ParentName, LODLevel.Goblins)

        ' Write BBOX, BSPH.
        WriteBoundaryVolume(IFF, bm)

        ' Write TRIS, VNRM.
        WriteTriangles(IFF, bm)
        WriteNormals(IFF, bm)

        ' Write SSUB.
        WriteSpatialSubdivision(IFF, bm)

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Writes the BSRM of a multi mesh to the IFF writer.
    ''' </summary>
    Friend Shared Sub WriteMultiMesh(ByVal IFF As IFF.IFFWriter, ByVal mm As MultiMesh)
        If mm.LOD.Count = 0 Then _
            Trace.TraceWarning("Multi mesh '" & mm.ToString() & "' has no LODs. Please remove.") _
                : Exit Sub

        If (mm.LOD(0).PartCount = 0) OrElse
           (mm.LOD(0).TotalVertexCount = 0) OrElse
           (mm.LOD(0).TotalIndiceCount = 0) Then _
            Trace.TraceWarning("Multi mesh '" & mm.ToString() & "' LOD0 has no data. Please fix.")

        ' Duplicate the basic mesh.
        Dim bm As New BasicMesh(mm.LOD(0))

        ' Prepare.
        PrepareBasicMesh(bm)

        IFF.Push("BSRM", Homeworld2.IFF.ChunkType.Form)

        ' Write DESC.
        WriteDesc(IFF, mm.Name, mm.ParentName, LODLevel.LOD1)

        ' Write BBOX, BSPH.
        WriteBoundaryVolume(IFF, bm)

        ' Write TRIS, VNRM.
        WriteTriangles(IFF, bm)
        WriteNormals(IFF, bm)

        ' Write SSUB.
        WriteSpatialSubdivision(IFF, bm)

        IFF.Pop()
    End Sub
End Class
