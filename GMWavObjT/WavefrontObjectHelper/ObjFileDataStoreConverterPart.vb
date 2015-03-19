Imports Microsoft.DirectX.Direct3D

Namespace WavefrontObjectHelper
 ''' <summary>
 ''' Class representing a part of the data in the wavefront object file.
 ''' </summary>
 ''' <remarks>
 ''' For converting data.
 ''' </remarks>
 Friend Class ObjFileDataStoreConverterPart
  ''' <summary>
  ''' Array of vertices.
  ''' </summary>
  Dim m_Verts As List(Of Standard.Vertex)

  ''' <summary>
  ''' Hashtable containing mapping from <c>IndiceValue</c>
  ''' to indices according to vertex array (<c>m_Verts</c>).
  ''' </summary>
  Dim m_VertMap As Dictionary(Of IndiceValues, Integer)

  ''' <summary>
  ''' Primitive group of points.
  ''' </summary>
  Dim m_Ind1 As List(Of Integer)

  ''' <summary>
  ''' Primitive group of lines.
  ''' </summary>
  Dim m_Ind2 As List(Of Integer)

  ''' <summary>
  ''' Primitive group of triangles.
  ''' </summary>
  Dim m_Ind3 As List(Of Integer)

  ''' <summary>
  ''' List of primitive groups of triangle fan (polygon).
  ''' </summary>
  Dim m_IndF As List(Of List(Of Integer))

  ''' <summary>
  ''' Material used by the part.
  ''' </summary>
  Dim m_Mat As Standard.Material

  ''' <summary>
  ''' The reference wavefront object file data.
  ''' </summary>
  Dim m_ObjData As ObjFileDataStore

  ''' <summary>
  ''' Class constructor.
  ''' </summary>
  ''' <exception cref="ArgumentNullException">
  ''' Thrown when <c>_ObjFile Is Nothing</c>.
  ''' </exception>
  Public Sub New(ByVal _ObjFile As ObjFileDataStore, ByVal MatIndex As Integer)
   If _ObjFile Is Nothing Then _
    Throw New ArgumentNullException("_ObjFile") _
  : Exit Sub

   ' Allocate vertex related data.
   m_Verts = New List(Of Standard.Vertex)
   m_VertMap = New Dictionary(Of IndiceValues, Integer)

   ' Allocate data for indices.
   m_Ind1 = New List(Of Integer)
   m_Ind2 = New List(Of Integer)
   m_Ind3 = New List(Of Integer)
   m_IndF = New List(Of List(Of Integer))

   ' Set the material.
   m_Mat = _ObjFile.Mat(MatIndex)

   ' Set the reference object file.
   m_ObjData = _ObjFile

  End Sub

  ''' <summary>
  ''' Class destructor.
  ''' </summary>
  Protected Overrides Sub Finalize()
   m_IndF.Clear()

   m_Verts = Nothing
   m_VertMap = Nothing

   m_Ind1 = Nothing
   m_Ind2 = Nothing
   m_Ind3 = Nothing
   m_IndF = Nothing

   MyBase.Finalize()

  End Sub

  ''' <summary>
  ''' Returns the indice of the specified vertex or adds it.
  ''' </summary>
  ''' <param name="IndData">
  ''' Indice data.
  ''' </param>
  ''' <returns>
  ''' The index of the vertex. Adds the vertex if needed.
  ''' </returns>
  ''' <remarks>
  ''' This returns -1 if any of the indices (of position, normal, UV, colour)
  ''' are out of bounds.
  ''' </remarks>
  Private Function GetVertexOrAdd(ByRef IndData As IndiceValues) As Integer
   Dim VtxInd As Integer

   ' Check the range of values.
   If (IndData.Position < 0) OrElse (IndData.Position >= m_ObjData.P.Count) OrElse _
      (IndData.Normal < 0) OrElse (IndData.Normal >= m_ObjData.N.Count) OrElse _
      (IndData.UV < 0) OrElse (IndData.UV >= m_ObjData.UV.Count) OrElse _
      (IndData.Colour < 0) OrElse (IndData.Colour >= m_ObjData.Col.Count) Then _
    Trace.TraceWarning("Invalid indice reference found.") _
  : Return -1

   ' Do we already have the vertex? If so, get it's index.
   ' Otherwise, add the element first and then get it's index.
   ' The vertex (itself) must be added as well in the latter case.
   If m_VertMap.ContainsKey(IndData) Then _
    VtxInd = m_VertMap(IndData) _
   Else _
    VtxInd = m_Verts.Count _
  : m_VertMap.Add(IndData, VtxInd) _
  : m_Verts.Add(New Standard.Vertex( _
                                     m_ObjData.P(IndData.Position), _
                                     m_ObjData.N(IndData.Normal), _
                                     m_ObjData.UV(IndData.UV), _
                                     m_ObjData.Col(IndData.Colour).ToArgb()) _
                                   )

   Return VtxInd

  End Function

  ''' <summary>
  ''' Adds the specified indice in the object file to this part.
  ''' </summary>
  ''' <param name="Ind">
  ''' Index of the indice to be added.
  ''' </param>
  Public Sub AddIndices(ByVal Ind As Integer)
   ' Check bounds.
   If (Ind < 0) OrElse (Ind >= m_ObjData.Ind.Count) Then _
    Throw New ArgumentOutOfRangeException("Ind") _
  : Exit Sub

   With m_ObjData.Ind(Ind)
    ' Add the group.
    Select Case .Count
     ' ----------
     ' DEGENERATE
     ' ----------
     Case 0
      Trace.TraceWarning("Degenerate indice group: " & CStr(Ind) & ".")

      ' POINT
     Case 1
      ' Add the vertex if needed.
      Dim IndValue As Integer = GetVertexOrAdd(.Indices(0))

      ' Check value before adding.
      If IndValue = -1 Then _
       Exit Sub

      ' Add indice.
      m_Ind1.Add(GetVertexOrAdd(.Indices(0)))

      ' LINE
     Case 2
      ' Add the vertices if needed.
      Dim IndValue1 As Integer = GetVertexOrAdd(.Indices(0))
      Dim IndValue2 As Integer = GetVertexOrAdd(.Indices(1))

      ' Check value before adding.
      If (IndValue1 = -1) OrElse (IndValue2 = -1) Then _
       Exit Sub

      ' Add indices.
      m_Ind2.Add(GetVertexOrAdd(.Indices(0)))
      m_Ind2.Add(GetVertexOrAdd(.Indices(1)))

      ' TRIANGLE
     Case 3
      ' Add the vertices if needed.
      Dim IndValue1 As Integer = GetVertexOrAdd(.Indices(0))
      Dim IndValue2 As Integer = GetVertexOrAdd(.Indices(1))
      Dim IndValue3 As Integer = GetVertexOrAdd(.Indices(2))

      ' Check value before adding.
      If (IndValue1 = -1) OrElse (IndValue2 = -1) OrElse (IndValue3 = -1) Then _
       Exit Sub

      ' Add the triangle.
      m_Ind3.Add(IndValue1)
      m_Ind3.Add(IndValue2)
      m_Ind3.Add(IndValue3)

      ' POLYGON
     Case Else
      Dim pg As New List(Of Integer)

      For I As Integer = 0 To .Count - 1
       ' Add the vertices if needed.
       Dim IndValue As Integer = GetVertexOrAdd(.Indices(I))

       ' Check value before adding.
       If (IndValue = -1) Then _
        pg.Clear() _
      : pg = Nothing _
      : Exit Sub

       ' Add the triangle fan indice.
       pg.Add(IndValue)

      Next I ' For I As Integer = 0 To .Count - 1

      ' Add the group.
      m_IndF.Add(pg)

    End Select ' Select Case .Count
   End With ' With m_ObjData.Ind(In)

  End Sub

  ''' <summary>
  ''' Copies the data in this part to a mesh part.
  ''' </summary>
  ''' <typeparam name="VertexType">
  ''' Vertex format of the output part.
  ''' </typeparam>
  ''' <typeparam name="IndiceType">
  ''' Indice format of the output part.
  ''' </typeparam>
  ''' <typeparam name="MaterialType">
  ''' Material format of the output part.
  ''' </typeparam>
  ''' <param name="OutPart">
  ''' The part to which copy to.
  ''' </param>
  ''' <exception cref="ArgumentNullException">
  ''' Thrown when <c>OutPart Is Nothing</c>.
  ''' </exception>
  Public Sub CopyTo(Of VertexType As {Structure, IVertex}, _
                       IndiceType As {Structure, IConvertible}, _
                       MaterialType As {Structure, IMaterial}) _
                   (ByVal OutPart As GMeshPart(Of VertexType, IndiceType, MaterialType), _
           Optional ByVal _VC As Converter(Of Standard.Vertex, VertexType) = Nothing, _
           Optional ByVal _IC As Converter(Of Integer, IndiceType) = Nothing, _
           Optional ByVal _MC As Converter(Of Standard.Material, MaterialType) = Nothing)

   ' Check inputs.
   ' -------------
   ' Check output part.
   If OutPart Is Nothing Then _
    Throw New ArgumentNullException("OutPart") _
  : Exit Sub

   ' Use the default converter if one isn't available.
   If _VC Is Nothing Then _
    _VC = AddressOf Utility.VertexConverter

   ' Use the default converter if one isn't available.
   If _IC Is Nothing Then _
    _IC = AddressOf Utility.IndiceConverter

   ' Use the default converter if one isn't available.
   If _MC Is Nothing Then _
    _MC = AddressOf Utility.MaterialConverter

   ' Clean the mesh part.
   ' --------------------
   OutPart.Vertices.Count = m_Verts.Count
   OutPart.RemovePrimitiveGroupsAll()

   ' Perform vertex conversion.
   ' --------------------------
   For I As Integer = 0 To m_Verts.Count - 1
    OutPart.Vertices(I) = _VC(m_Verts(I))

   Next I ' For I As Integer = 0 To m_Verts.Count - 1

   ' Perform indice conversion.
   ' --------------------------
   ' Note: A loop is used here, because almost all of the conversion
   ' procedure is same for points, lines and triangles.
   For I As Integer = 0 To 2
    Dim IndGroup As List(Of Integer) = Nothing
    Dim IndGroupType As PrimitiveType

    ' Select the indice group.
    Select Case I
     Case 0 : IndGroup = m_Ind1 : IndGroupType = PrimitiveType.PointList
     Case 1 : IndGroup = m_Ind2 : IndGroupType = PrimitiveType.LineList
     Case 2 : IndGroup = m_Ind3 : IndGroupType = PrimitiveType.TriangleList
    End Select ' Select Case I

    ' Does it have indices?
    If IndGroup.Count = 0 Then _
     Continue For

    ' Create a new primitive group.
    Dim pg As New GPrimitiveGroup(Of IndiceType)

    With pg
     ' Set the primitive group type.
     .Type = IndGroupType

     ' Allocate the indices.
     .IndiceCount = IndGroup.Count

     ' Set the indices.
     For J As Integer = 0 To IndGroup.Count - 1
      .Indice(J) = _IC(IndGroup(J))

     Next J ' For J As Integer = 0 To IndGroup.Count - 1
    End With ' With pg

    ' Add to mesh.
    OutPart.AddPrimitiveGroup(pg)

   Next I ' For I As Integer = 0 To 2

   ' Now add the triangle fan primitive groups.
   For I As Integer = 0 To m_IndF.Count - 1
    ' Create the primitive group.
    Dim pg As New GPrimitiveGroup(Of IndiceType)

    ' Set it's type and indice count.
    pg.Type = PrimitiveType.TriangleFan
    pg.IndiceCount = m_IndF(I).Count

    ' Set the indices.
    For J As Integer = 0 To m_IndF(I).Count - 1
     pg.Indice(J) = _IC(m_IndF(I)(J))

    Next J ' For J As Integer = 0 To m_IndF(I).Count - 1

    ' Add to mesh.
    OutPart.AddPrimitiveGroup(pg)

   Next I ' For I As Integer = 0 To m_IndF.Count - 1

   ' Perform material conversion.
   ' ----------------------------
   OutPart.Material = _MC(m_Mat)

  End Sub

 End Class

End Namespace
