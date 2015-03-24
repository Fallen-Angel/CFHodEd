Imports GenericMesh.Exceptions
Imports GenericMesh.VertexFields

''' <summary>
''' The part of the mesh class is represented using this class.
''' </summary>
''' <typeparam name="VertexType">
''' Vertex format of the mesh part.
''' </typeparam>
''' <typeparam name="IndiceType">
''' Indice format of the mesh part.
''' </typeparam>
''' <typeparam name="MaterialType">
''' Type of material used by the mesh part.
''' </typeparam>
Public NotInheritable Class GMeshPart (Of VertexType As {Structure, IVertex},
    IndiceType As {Structure, IConvertible},
    MaterialType As {Structure, IMaterial})
    ' -------------------------
    ' Interface(s) Implemented.
    ' -------------------------
    Implements ICloneable

    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>
    ''' The vertices in the mesh part.
    ''' </summary>
    Private m_Vertices As GVertexGroup(Of VertexType)

    ''' <summary>
    ''' This is the primitive group array in the mesh part.
    ''' </summary>
    Private m_PrimitiveGroups As List(Of GPrimitiveGroup(Of IndiceType))

    ''' <summary>
    ''' The material used by the mesh part.
    ''' </summary>
    Public Material As MaterialType

    ' ------------------------
    ' Constructors\Finalizers.
    ' ------------------------
    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    Public Sub New()
        ' Initialize the vertex group.
        m_Vertices = New GVertexGroup(Of VertexType)

        ' Initialize the primitive group list.
        m_PrimitiveGroups = New List(Of GPrimitiveGroup(Of IndiceType))

        ' Initialize the material
        Material.Initialize()
    End Sub

    ''' <summary>
    ''' Copy constructor.
    ''' </summary>
    ''' <param name="obj">
    ''' The object whose copy is to be made.
    ''' </param>
    ''' <remarks>
    ''' This is equivalent to a normal constructor if <c>obj Is Nothing</c>.
    ''' </remarks>
    Public Sub New(ByVal obj As GMeshPart(Of VertexType, IndiceType, MaterialType))
        ' First call default constructor.
        Me.New()

        ' Now call the append method.
        Append(obj)
    End Sub

    ''' <summary>
    ''' Class destructor.
    ''' </summary>
    Protected Overrides Sub Finalize()
        ' Remove vertices.
        m_Vertices = Nothing

        ' Remove the primitive group array.
        m_PrimitiveGroups = Nothing

        ' Call base destructor.
        MyBase.Finalize()
    End Sub

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns the vertex group.
    ''' </summary>
    Public ReadOnly Property Vertices() As GVertexGroup(Of VertexType)
        Get
            ' Return the vertex group.
            Return m_Vertices
        End Get
    End Property

    ''' <summary>
    ''' Returns\Sets the primitive group count.
    ''' </summary>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>value</c> is negative.
    ''' </exception>
    Public Property PrimitiveGroupCount() As Integer
        Get
            Return m_PrimitiveGroups.Count
        End Get

        Set(ByVal value As Integer)
            Dim OldSize As Integer = m_PrimitiveGroups.Count

            ' Verify new size.
            If value < 0 Then _
                Throw New ArgumentOutOfRangeException("value") _
                    : Exit Property

            ' Free the extra elements.
            If value < OldSize Then _
                m_PrimitiveGroups.RemoveRange(value, OldSize - value)

            ' Allocate new elements.
            For I As Integer = OldSize To value - 1
                m_PrimitiveGroups.Add(New GPrimitiveGroup(Of IndiceType))

            Next I ' For I As Integer = OldSize To value - 1
        End Set
    End Property

    ''' <summary>
    ''' Returns the primitive group.
    ''' </summary>
    ''' <param name="Index">
    ''' The index of primitive group to fetch.
    ''' </param>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>Index</c> is out of bounds.
    ''' </exception>
    Public ReadOnly Property PrimitiveGroups(ByVal Index As Integer) As GPrimitiveGroup(Of IndiceType)
        Get
            ' Check index.
            If (Index < 0) OrElse (Index >= m_PrimitiveGroups.Count) Then _
                Throw New ArgumentOutOfRangeException("Index") _
                    : Exit Property

            ' Return the primitive group.
            Return m_PrimitiveGroups(Index)
        End Get
    End Property

    ''' <summary>
    ''' Returns the total indices in this mesh part.
    ''' </summary>
    Public ReadOnly Property TotalIndiceCount() As Integer
        Get
            Return m_PrimitiveGroups.Aggregate(
                0,
                Function(C As Integer, V As GPrimitiveGroup(Of IndiceType)) _
                                                  (C + V.IndiceCount)
                )
        End Get
    End Property

    ' ---------
    ' Operators
    ' ---------
    ' None

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Adds a primitive group to the mesh part.
    ''' </summary>
    ''' <typeparam name="InIndiceType">
    ''' Indice type of the input primitive.
    ''' </typeparam>
    ''' <param name="P">
    ''' The primitive to add.
    ''' </param>
    ''' <param name="_Converter">
    ''' The converter to convert indices from input indice format
    ''' to indice format of this instance.
    ''' </param>
    ''' <remarks>
    ''' This does nothing if <c>P Is Nothing</c>. Also, the input
    ''' primitive group is cloned before adding.
    ''' </remarks>
    Public Function AddPrimitiveGroup (Of InIndiceType As {Structure, IConvertible}) _
        (ByVal P As GPrimitiveGroup(Of InIndiceType),
         Optional ByVal _Converter As Converter(Of InIndiceType, IndiceType) = Nothing) As Boolean

        Dim _P As GPrimitiveGroup(Of IndiceType)

        ' Do we have an object?
        If P Is Nothing Then _
            Return False

        ' Create a new primitive group.
        _P = New GPrimitiveGroup(Of IndiceType)

        ' Clone the primitive group.
        P.CopyTo(_P, _Converter)

        ' Add the primitive.
        m_PrimitiveGroups.Add(_P)

        ' Remove the primitive group.
        _P = Nothing

        Return True
    End Function

    ''' <summary>
    ''' Adds primitive groups to the part.
    ''' </summary>
    ''' <param name="_Count">
    ''' The number of primitive groups to add.
    ''' </param>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>_Count</c> is negative.
    ''' </exception>
    ''' <remarks>
    ''' This does nothing when <c>_Count = 0</c>.
    ''' </remarks>
    Public Function AddPrimitiveGroup(ByVal _Count As Integer) As Boolean
        ' Check for valid inputs.
        If _Count < 0 Then _
            Throw New ArgumentOutOfRangeException("_Count") _
                : Exit Function

        ' Do we have anything to do?
        If _Count = 0 Then _
            Return False

        ' Add elements.
        PrimitiveGroupCount += _Count

        Return True
    End Function

    ''' <summary>
    ''' Appends the input mesh part to this mesh part.
    ''' </summary>
    ''' <typeparam name="InVertexType">
    ''' Vertex type of input mesh part.
    ''' </typeparam>
    ''' <typeparam name="InIndiceType">
    ''' Indice type of input mesh part.
    ''' </typeparam>
    ''' <typeparam name="InMaterialType">
    ''' Material type of input mesh part (not used).
    ''' </typeparam>
    ''' <param name="obj">
    ''' The mesh part to add to this mesh part.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter from input mesh part format to
    ''' format of this instance.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter from input mesh part format to
    ''' format of this instance.
    ''' </param>
    ''' <remarks>
    ''' This does nothing if <c>obj Is Nothing</c>.
    ''' </remarks>
    Public Function Append (Of InVertexType As {Structure, IVertex}, _
        InIndiceType As {Structure, IConvertible}, _
        InMaterialType As {Structure, IMaterial}) _
        (ByVal obj As GMeshPart(Of InVertexType, InIndiceType, InMaterialType),
         Optional ByVal _VC As System.Converter(Of InVertexType, VertexType) = Nothing,
         Optional ByVal _IC As System.Converter(Of InIndiceType, IndiceType) = Nothing) As Boolean

        Dim IndiceOffset As IndiceType
        Dim PGOffset As Integer

        ' Check if the object exists.
        ' ---------------------------
        If obj Is Nothing Then _
            Return False

        ' Get the indice offset and primitive group offset.
        ' -------------------------------------------------
        PGOffset = m_PrimitiveGroups.Count
        IndiceOffset = Utility.IndiceConverter (Of Integer, IndiceType)(m_Vertices.Count)

        ' Append the vertices.
        ' --------------------
        m_Vertices.Append(obj.m_Vertices, _VC)

        ' Append the primtive groups.
        ' ---------------------------
        For I As Integer = 0 To obj.PrimitiveGroupCount - 1
            ' Convert and add the primitive group.
            AddPrimitiveGroup(obj.m_PrimitiveGroups(I), _IC)

            ' Shift indices.
            m_PrimitiveGroups(PGOffset + I).ShiftIndices(IndiceOffset)

        Next I ' For I As Integer = 0 To obj.PrimitiveGroupCount - 1

        ' Merge all "list" type primitives.
        ' ---------------------------------
        MergeListPrimitive()

        Return True
    End Function

    ''' <summary>
    ''' Copies this mesh part to another mesh part.
    ''' </summary>
    ''' <typeparam name="OutVertexType">
    ''' Vertex type of output mesh part.
    ''' </typeparam>
    ''' <typeparam name="OutIndiceType">
    ''' Indice type of output mesh part.
    ''' </typeparam>
    ''' <typeparam name="OutMaterialType">
    ''' Material type of output mesh part.
    ''' </typeparam>
    ''' <param name="obj">
    ''' The part to which this part is to be copied.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter from input mesh part format to
    ''' format of this instance.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter from input mesh part format to
    ''' format of this instance.
    ''' </param>
    ''' <param name="_MC">
    ''' Material converter from input mesh part format to
    ''' format of this instance.
    ''' </param>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>obj Is Nothing</c>
    ''' </exception>
    Public Function CopyTo (Of OutVertexType As {Structure, IVertex}, _
        OutIndiceType As {Structure, IConvertible}, _
        OutMaterialType As {Structure, IMaterial}) _
        (ByVal obj As GMeshPart(Of OutVertexType, OutIndiceType, OutMaterialType),
         Optional ByVal _VC As System.Converter(Of VertexType, OutVertexType) = Nothing,
         Optional ByVal _IC As System.Converter(Of IndiceType, OutIndiceType) = Nothing,
         Optional ByVal _MC As System.Converter(Of MaterialType, OutMaterialType) = Nothing) As Boolean

        ' Check output part.
        ' ------------------
        If obj Is Nothing Then _
            Throw New ArgumentNullException("obj") _
                : Exit Function

        ' Copy the vertices.
        ' ------------------
        m_Vertices.CopyTo(obj.m_Vertices, _VC)

        ' Copy the primitive groups.
        ' --------------------------
        ' Remove previous primitive groups.
        obj.RemovePrimitiveGroupsAll()

        ' Now convert and add primitives.
        m_PrimitiveGroups.ForEach(
            Function(V As GPrimitiveGroup(Of IndiceType)) _
                                     (obj.AddPrimitiveGroup(V, _IC))
            )

        ' Copy the material.
        ' ------------------
        ' Use the default converter if there isn't any.
        If _MC Is Nothing Then _
            _MC = AddressOf Utility.MaterialConverter

        obj.Material = _MC(Material)

        Return True
    End Function

    ''' <summary>
    ''' Removes primitive group(s) from the mesh part.
    ''' </summary>
    ''' <param name="Index">
    ''' Index of the primitive to be removed.
    ''' </param>
    ''' <param name="RemoveCount">
    ''' Number of primitive groups to be removed.
    ''' </param>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>Index</c> is out of bounds.
    ''' Also thrown when <c>RemoveCount</c> is either negative,
    ''' zero, or larger than the count of elements that may be removed.
    ''' </exception>
    Public Function RemovePrimitiveGroups(ByVal Index As Integer,
                                          Optional ByVal RemoveCount As Integer = 1) As Boolean

        ' Check inputs.
        ' -------------
        ' Check if empty.
        If m_PrimitiveGroups.Count = 0 Then _
            Return False

        ' Check the index of first element to be removed.
        If (Index < 0) OrElse (Index >= m_PrimitiveGroups.Count) Then _
            Throw New ArgumentOutOfRangeException("Index") _
                : Exit Function

        ' Check the count of elements to be removed.
        If (RemoveCount <= 0) OrElse (RemoveCount > m_PrimitiveGroups.Count) Then _
            Throw New ArgumentOutOfRangeException("RemoveCount") _
                : Exit Function

        ' Check whether the index of the last element to be removed is in bounds or not.
        If Index + RemoveCount > m_PrimitiveGroups.Count Then _
            Throw New ArgumentOutOfRangeException("RemoveCount") _
                : Exit Function

        ' Remove the primitive groups.
        ' ----------------------------
        m_PrimitiveGroups.RemoveRange(Index, RemoveCount)

        Return True
    End Function

    ''' <summary>
    ''' Removes primitive groups from the mesh part.
    ''' </summary>
    ''' <param name="StartIndex">
    ''' The index of the first primitive group which is removed.
    ''' </param>
    ''' <param name="EndIndex">
    ''' The index of the last primitive group which is removed.
    ''' </param>
    ''' <remarks>
    ''' Indices in the range [StartIndex, EndIndex] (i.e. both limits included)
    ''' are removed.
    ''' </remarks>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>StartIndex</c> or <c>EndIndex</c> is out of bounds.
    ''' </exception>
    Public Function RemovePrimitiveGroupsRange(ByVal StartIndex As Integer,
                                               ByVal EndIndex As Integer) As Boolean

        ' Check inputs.
        ' -------------
        ' Check if empty.
        If m_PrimitiveGroups.Count = 0 Then _
            Return False

        ' Check bounds.
        If (StartIndex < 0) OrElse (StartIndex >= m_PrimitiveGroups.Count) Then _
            Throw New ArgumentOutOfRangeException("StartIndex") _
                : Exit Function

        ' Check bounds.
        If (EndIndex < 0) OrElse (EndIndex >= m_PrimitiveGroups.Count) Then _
            Throw New ArgumentOutOfRangeException("EndIndex") _
                : Exit Function

        ' Check bounds.
        If (StartIndex > EndIndex) Then _
            Throw New ArgumentOutOfRangeException("StartIndex' or 'EndIndex'") _
                : Exit Function

        ' Remove the primitive groups.
        ' ----------------------------
        m_PrimitiveGroups.RemoveRange(StartIndex, EndIndex - StartIndex + 1)

        Return True
    End Function

    ''' <summary>
    ''' Removes all the primitive groups.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function RemovePrimitiveGroupsAll() As Boolean
        ' Remove all primitive group references.
        m_PrimitiveGroups.Clear()

        Return True
    End Function

    ''' <summary>
    ''' Clones this mesh part and returns it.
    ''' </summary>
    ''' <remarks>
    ''' Copy is a deep copy.
    ''' </remarks>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return New GMeshPart(Of VertexType, IndiceType, MaterialType)(Me)
    End Function

    ''' <summary>
    ''' Transforms all vertices in the mesh part.
    ''' </summary>
    ''' <param name="M">
    ''' Matrix to be used for the transformer.
    ''' </param>
    ''' <param name="_VertexTransformer">
    ''' Function to be used for performing transformation.
    ''' </param>
    Public Function Transform(ByVal M As Matrix,
                              Optional ByVal _VertexTransformer As Func(Of VertexType, Matrix, VertexType) = Nothing) _
        As Boolean

        Return m_Vertices.Transform(M, _VertexTransformer)
    End Function

    ''' <summary>
    ''' Transforms all vertices in the mesh part.
    ''' </summary>
    ''' <param name="_VertexTransformer">
    ''' Function to be used for performing transformation.
    ''' </param>
    Public Function Translation(ByVal X As Single, ByVal Y As Single, ByVal Z As Single,
                                Optional ByVal _VertexTransformer As Func(Of VertexType, Matrix, VertexType) = Nothing) _
        As Boolean

        Return m_Vertices.Transform(Matrix.Translation(X, Y, Z), _VertexTransformer)
    End Function

    ''' <summary>
    ''' Transforms all vertices in the mesh part.
    ''' </summary>
    ''' <param name="V">
    ''' Vector to be used for transformation.
    ''' </param>
    ''' <param name="_VertexTransformer">
    ''' Function to be used for performing transformation.
    ''' </param>
    Public Function Translation(ByVal V As Vector3,
                                Optional ByVal _VertexTransformer As Func(Of VertexType, Matrix, VertexType) = Nothing) _
        As Boolean

        Return m_Vertices.Transform(Matrix.Translation(V), _VertexTransformer)
    End Function

    ''' <summary>
    ''' Transforms all vertices in the mesh part.
    ''' </summary>
    ''' <param name="_VertexTransformer">
    ''' Function to be used for performing transformation.
    ''' </param>
    Public Function Rotation(ByVal X As Single, ByVal Y As Single, ByVal Z As Single,
                             Optional ByVal _VertexTransformer As Func(Of VertexType, Matrix, VertexType) = Nothing) _
        As Boolean

        Return m_Vertices.Transform((Matrix.RotationX(X)*
                                     Matrix.RotationY(Y)*
                                     Matrix.RotationZ(Z)),
                                    _VertexTransformer)
    End Function

    ''' <summary>
    ''' Transforms all vertices in the mesh part.
    ''' </summary>
    ''' <param name="V">
    ''' Vector to be used for transformation.
    ''' </param>
    ''' <param name="_VertexTransformer">
    ''' Function to be used for performing transformation.
    ''' </param>
    Public Function Rotation(ByVal V As Vector3,
                             Optional ByVal _VertexTransformer As Func(Of VertexType, Matrix, VertexType) = Nothing) _
        As Boolean

        Return m_Vertices.Transform((Matrix.RotationX(V.X)*
                                     Matrix.RotationY(V.Y)*
                                     Matrix.RotationZ(V.Z)),
                                    _VertexTransformer)
    End Function

    ''' <summary>
    ''' Transforms all vertices in the mesh part.
    ''' </summary>
    ''' <param name="_VertexTransformer">
    ''' Function to be used for performing transformation.
    ''' </param>
    Public Function Scaling(ByVal X As Single, ByVal Y As Single, ByVal Z As Single,
                            Optional ByVal _VertexTransformer As Func(Of VertexType, Matrix, VertexType) = Nothing) _
        As Boolean

        Return m_Vertices.Transform(Matrix.Scaling(X, Y, Z), _VertexTransformer)
    End Function

    ''' <summary>
    ''' Transforms all vertices in the mesh part.
    ''' </summary>
    ''' <param name="V">
    ''' Vector to be used for transformation.
    ''' </param>
    ''' <param name="_VertexTransformer">
    ''' Function to be used for performing transformation.
    ''' </param>
    Public Function Scaling(ByVal V As Vector3,
                            Optional ByVal _VertexTransformer As Func(Of VertexType, Matrix, VertexType) = Nothing) _
        As Boolean

        Return m_Vertices.Transform(Matrix.Scaling(V), _VertexTransformer)
    End Function

    ''' <summary>
    ''' Converts all primitive groups to "list" type, joins them,
    ''' and sets it as the only primitive group.
    ''' </summary>
    Public Function ConvertToList() As Boolean
        Dim P1, P2, P3 As GPrimitiveGroup(Of IndiceType)

        ' Do we have any primitives?
        If m_PrimitiveGroups.Count = 0 Then _
            Return False

        ' This is to avoid a warning.
        P1 = Nothing
        P2 = Nothing
        P3 = Nothing

        ' Append the primitives.
        For I As Integer = 0 To m_PrimitiveGroups.Count - 1
            ' Must have proper primitive type.
            m_PrimitiveGroups(I).Validate()

            Select Case m_PrimitiveGroups(I).IndicesPerPrimitive
                ' POINT LIST
                Case 1
                    ' Copy the reference or append.
                    If P1 Is Nothing Then _
                        P1 = m_PrimitiveGroups(I) _
                        Else _
                        P1.Append(m_PrimitiveGroups(I))

                    ' LINE PRIMITIVE
                Case 2
                    ' Copy the reference or append.
                    If P3 Is Nothing Then _
                        P3 = m_PrimitiveGroups(I) _
                        Else _
                        P3.Append(m_PrimitiveGroups(I))

                    ' TRIANGLE PRIMITIVE
                Case 3
                    ' Copy the reference or append.
                    If P3 Is Nothing Then _
                        P3 = m_PrimitiveGroups(I) _
                        Else _
                        P3.Append(m_PrimitiveGroups(I))

            End Select ' Select Case m_PrimitiveGroups(I).IndicesPerPrimitive

        Next I ' For I As Integer = 0 To m_PrimitiveGroups.Count - 1

        ' Remove all primitives.
        RemovePrimitiveGroupsAll()

        ' Now add the only primitives.
        If P1 IsNot Nothing Then _
            AddPrimitiveGroup(P1)

        If P2 IsNot Nothing Then _
            AddPrimitiveGroup(P2)

        If P3 IsNot Nothing Then _
            AddPrimitiveGroup(P3)

        Return True
    End Function

    ''' <summary>
    ''' Merges all "list" type primitives into one group.
    ''' This eliminates multiple "list" type primitives (that
    ''' usually happens after merging two parts of a mesh).
    ''' </summary>
    Private Function MergeListPrimitive() As Boolean
        Dim FirstP, FirstL, FirstT As Integer

        ' Initialize.
        FirstP = - 1
        FirstL = - 1
        FirstT = - 1

        ' Get the first list primitives.
        For I As Integer = 0 To m_PrimitiveGroups.Count - 1
            ' Point primitive.
            If (FirstP = - 1) AndAlso (m_PrimitiveGroups(I).Type = PrimitiveType.PointList) Then _
                FirstP = I _
                    : Continue For

            ' Line primitive.
            If (FirstL = - 1) AndAlso (m_PrimitiveGroups(I).Type = PrimitiveType.LineList) Then _
                FirstL = I _
                    : Continue For

            ' Triangle primitive.
            If (FirstT = - 1) AndAlso (m_PrimitiveGroups(I).Type = PrimitiveType.TriangleList) Then _
                FirstT = I

        Next I ' For I As Integer = 0 To m_PrimitiveGroups.Count - 1

        ' Now start merging.
        For I As Integer = m_PrimitiveGroups.Count - 1 To 0 Step - 1
            ' Is this the first primitive?
            If (I = FirstP) OrElse (I = FirstL) OrElse (I = FirstT) Then _
                Continue For

            ' Merge into respective first primitive.
            Select Case m_PrimitiveGroups(I).Type
                ' POINT LIST
                Case PrimitiveType.PointList
                    m_PrimitiveGroups(FirstP).Append(m_PrimitiveGroups(I))

                    ' Remove this primitive.
                    RemovePrimitiveGroups(I)

                    ' LINE LIST
                Case PrimitiveType.LineList
                    m_PrimitiveGroups(FirstL).Append(m_PrimitiveGroups(I))

                    ' Remove this primitive.
                    RemovePrimitiveGroups(I)

                    ' TRIANGLE LIST
                Case PrimitiveType.TriangleList
                    m_PrimitiveGroups(FirstT).Append(m_PrimitiveGroups(I))

                    ' Remove this primitive.
                    RemovePrimitiveGroups(I)

            End Select ' Select Case m_PrimitiveGroups(I).Type

        Next I ' For I As Integer = m_PrimitiveGroups.Count - 1 To 0 Step -1

        Return True
    End Function

    ''' <summary>
    ''' Welds the vertices of this part.
    ''' </summary>
    ''' <remarks>
    ''' NOTE: This method calls <c>Validate()</c>.
    ''' Also, the primitive groups are converted to "list" type
    ''' before this operation.
    ''' </remarks>
    Public Function Weld() As Boolean
        Dim VtxCount As Integer, IndCount As Integer
        Dim VtxIndice As Integer, Indices() As Integer
        Dim VT As Dictionary(Of VertexType, Integer), OutVG As GVertexGroup(Of VertexType)

        ' Check if we have any data.
        If (m_Vertices.Count = 0) OrElse
           (m_PrimitiveGroups.Count = 0) OrElse
           (TotalIndiceCount = 0) Then _
            Return False

        ' Validate the mesh.
        Validate()

        ' First convert to list type primitive group.
        ConvertToList()

        ' Cache some properties and data.
        ' Get the vertex\indice counts.
        VtxCount = m_Vertices.Count
        IndCount = m_PrimitiveGroups(0).IndiceCount

        ' Allocate indices.
        ReDim Indices(IndCount)

        ' Get the indices.
        m_PrimitiveGroups(0).CopyTo(Indices)

        ' Initialize hashtable and output vertex group.
        VT = New Dictionary(Of VertexType, Integer)
        OutVG = New GVertexGroup(Of VertexType)

        ' Allocate the output vertices.
        OutVG.Count = m_Vertices.Count

        ' Now process each indice.
        For I As Integer = 0 To IndCount - 1
            Dim _VtxIndice As Integer
            Dim VtxToFind As VertexType = m_Vertices(Indices(I))

            ' See if the vertex is already hashed.
            If VT.ContainsKey(VtxToFind) Then
                ' If it is, then get it's indice.
                _VtxIndice = VT(VtxToFind)

            Else ' If VT.ContainsKey(VtxToFind) Then
                ' Otherwise, add it and then get it's indice.
                VT.Add(VtxToFind, VtxIndice)
                _VtxIndice = VtxIndice

                ' Also, copy the vertex.
                OutVG(VtxIndice) = VtxToFind

                ' Increment count.
                VtxIndice += 1

            End If ' If VT.ContainsKey(VtxToFind) Then

            ' Set the output indice (which is in this part's
            ' only primitive group).
            m_PrimitiveGroups(0)(I) = Utility.IndiceConverter (Of Integer, IndiceType)(_VtxIndice)

        Next I ' For I As Integer = 0 To IndCount - 1

        ' Now prune the output vertex group to proper size.
        OutVG.Count = VtxIndice

        ' Finally, replace with this part's vertex group.
        m_Vertices = Nothing
        m_Vertices = OutVG

        ' Free the reference.
        VT = Nothing
        OutVG = Nothing
        Indices = Nothing

        Return True
    End Function

    ''' <summary> 
    ''' Unwelds the vertices of this part.
    ''' </summary>
    ''' <remarks>
    ''' NOTE: This method calls <c>Validate()</c>.
    ''' Also, the primitive groups are converted to "list" type
    ''' before this operation.
    ''' </remarks>
    Public Function Unweld() As Boolean
        Dim NewVG As GVertexGroup(Of VertexType)
        Dim VtxUBound As Integer

        ' Check if we have any data.
        If (m_Vertices.Count = 0) OrElse
           (m_PrimitiveGroups.Count = 0) OrElse
           (TotalIndiceCount = 0) Then _
            Return False

        ' Validate the mesh.
        Validate()

        ' First convert to list type primitive group.
        ConvertToList()

        ' Create new vertex group.
        NewVG = New GVertexGroup(Of VertexType)

        ' Set the vertex count.
        NewVG.Count = m_PrimitiveGroups(0).IndiceCount
        VtxUBound = NewVG.Count - 1

        ' Set the vertices and the indices.
        For I As Integer = 0 To VtxUBound
            ' Set the vertex.
            NewVG(I) = m_Vertices(m_PrimitiveGroups(0)(I).ToInt32(Nothing))

            ' Set the indice.
            m_PrimitiveGroups(0)(I) = Utility.IndiceConverter (Of Integer, IndiceType)(I)

        Next I ' For I As Integer = 0 To VtxUBound

        ' Set the new vertex group.
        m_Vertices = Nothing
        m_Vertices = NewVG

        Return True
    End Function

    ''' <summary>
    ''' Flips the normal of all vertices.
    ''' </summary>
    Public Function FlipNormals() As Boolean
        Return m_Vertices.FlipNormals()
    End Function

    ''' <summary>
    ''' Reverses the order of all triangles.
    ''' </summary>
    ''' <remarks>
    ''' The part is triangulated first.
    ''' </remarks>
    Public Function ReverseFaceOrder() As Boolean
        ' Do we have anything to do?
        If m_PrimitiveGroups.Count = 0 Then _
            Return False

        ' Reverse face order for all primitive groups.
        m_PrimitiveGroups.ForEach(
            Function(V As GPrimitiveGroup(Of IndiceType)) _
                                     (V.ReverseFaceOrder())
            )

        ' Merge all list primitives in case we caused them.
        MergeListPrimitive()

        Return True
    End Function

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
    ''' <exception cref="Exception">
    ''' Thrown when <c>VertexType</c> does not implement <c>IVertexPosition3</c>.
    ''' </exception>
    ''' <remarks>
    ''' If <c>MinExtents</c> &lt; <c>MaxExtents</c> then mesh has no vertices.
    ''' If <c>MinExtents</c> = <c>MaxExtents</c> then mesh has no vertices.
    ''' Otherwise, atleast one component of <c>MinExtents</c> &lt; <c>MaxExtents</c>.
    ''' </remarks>
    Public Function GetMeshExtents(ByRef MinExtents As Vector3, ByRef MaxExtents As Vector3) As Boolean
        Return m_Vertices.GetMeshExtents(MinExtents, MaxExtents)
    End Function

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
    ''' <exception cref="Exception">
    ''' Thrown when <c>VertexType</c> does not implement <c>IVertexPosition3</c>.
    ''' </exception>
    ''' <remarks>
    ''' If <c>MinExtents</c> &lt; <c>MaxExtents</c> then mesh has no vertices.
    ''' If <c>MinExtents</c> = <c>MaxExtents</c> then mesh has no vertices.
    ''' Otherwise, atleast one component of <c>MinExtents</c> &lt; <c>MaxExtents</c>.
    ''' </remarks>
    Public Function GetMeshExtents(ByRef MinExtents As Vector4, ByRef MaxExtents As Vector4) As Boolean
        Return m_Vertices.GetMeshExtents(MinExtents, MaxExtents)
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
    ''' <exception cref="Exception">
    ''' Thrown when <c>VertexType</c> does not implement <c>IVertexPosition3</c>.
    ''' </exception>
    ''' <remarks>
    ''' If <c>Center</c> = {0, 0, 0} and <c>Radius</c> = 0 then mesh has no vertices.
    ''' If <c>Center</c> &lt;&gt; {0, 0, 0} and <c>Radius</c> = 0 then mesh has 1 vertex.
    ''' Otherwise, the <c>Center</c> and <c>Radius</c> are correct.
    ''' </remarks>
    Public Function GetMeshSphere(ByRef Center As Vector3, ByRef Radius As Single) As Boolean
        Return m_Vertices.GetMeshSphere(Center, Radius)
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
    ''' <exception cref="Exception">
    ''' Thrown when <c>VertexType</c> does not implement <c>IVertexPosition3</c>.
    ''' </exception>
    ''' <remarks>
    ''' If <c>Center</c> = {0, 0, 0} and <c>Radius</c> = 0 then mesh has no vertices.
    ''' If <c>Center</c> &lt;&gt; {0, 0, 0} and <c>Radius</c> = 0 then mesh has 1 vertex.
    ''' Otherwise, the <c>Center</c> and <c>Radius</c> are correct.
    ''' </remarks>
    Public Function GetMeshSphere(ByRef Center As Vector4, ByRef Radius As Single) As Boolean
        Return m_Vertices.GetMeshSphere(Center, Radius)
    End Function

    ''' <summary>
    ''' Calculates the normals in a part.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Thrown when <c>VertexType</c> does not implement either of
    ''' <c>IVertexPosition3</c> or <c>IVertexPosition4</c> and <c>IVertexNormal3</c>.
    ''' </exception>
    ''' <remarks>
    ''' This does nothing if there are no vertices\primitive groups.
    ''' </remarks>
    Public Function CalculateNormals() As Boolean
        Dim V As New VertexType
        Dim V1, V2, V3, V4 As Vector3
        Dim Ind1, Ind2, Ind3 As Integer
        Dim Normals() As Vector3, Ind() As IndiceType

        ' See if we have anything to do.
        If (m_Vertices.Count = 0) OrElse
           (m_PrimitiveGroups.Count = 0) OrElse
           (TotalIndiceCount = 0) Then _
            Return False

        ' Check if it supports the required fields.
        If (Not TypeOf V Is IVertexPosition3) AndAlso (Not TypeOf V Is IVertexPosition4) Then _
            Throw New Exception("'VertexType' does not implement either of 'IVertexPosition3' and 'IVertexPosition4'.") _
                : Exit Function

        ' Check if it supports the required fields.
        If Not TypeOf V Is IVertexNormal3 Then _
            Throw New Exception("'VertexType' does not implement 'IVertexNormal3'.") _
                : Exit Function

        ' Validate the part.
        Validate()

        ' Resize the normals' array.
        ReDim Normals(m_Vertices.Count - 1)

        ' Now calculate normals.
        For I As Integer = 0 To m_PrimitiveGroups.Count - 1
            Dim P As GPrimitiveGroup(Of IndiceType)

            ' Check if it has indices.
            If m_PrimitiveGroups(I).IndiceCount = 0 Then _
                Continue For

            ' Check if it is a triangle primitive.
            If (m_PrimitiveGroups(I).Type = PrimitiveType.PointList) OrElse
               (m_PrimitiveGroups(I).Type = PrimitiveType.LineList) OrElse
               (m_PrimitiveGroups(I).Type = PrimitiveType.LineStrip) Then _
                Continue For

            ' Allocate temporary indice space.
            ReDim Ind(3*m_PrimitiveGroups(I).PrimitiveCount - 1)

            ' Convert the primitive to list type, and get it's indices.
            P = CType(m_PrimitiveGroups(I).Clone(), GPrimitiveGroup(Of IndiceType))
            P.ConvertToList()
            P.CopyTo(Ind)
            P = Nothing

            For J As Integer = 0 To UBound(Ind) Step 3
                Ind1 = Ind(J).ToInt32(Nothing)
                Ind2 = Ind(J + 1).ToInt32(Nothing)
                Ind3 = Ind(J + 2).ToInt32(Nothing)

                ' Check indices.
                If (Ind1 < 0) OrElse (Ind2 < 0) OrElse (Ind3 < 0) OrElse
                   (Ind1 >= m_Vertices.Count) OrElse (Ind2 >= m_Vertices.Count) OrElse (Ind3 >= m_Vertices.Count) Then _
                    Trace.TraceWarning("Mesh refers to invalid vertices.")

                ' Keep indices in range.
                Ind1 = Math.Max(0, Math.Min(m_Vertices.Count - 1, Ind1))
                Ind2 = Math.Max(0, Math.Min(m_Vertices.Count - 1, Ind2))
                Ind3 = Math.Max(0, Math.Min(m_Vertices.Count - 1, Ind3))

                ' Get the vertices.
                If TypeOf V Is IVertexPosition3 Then
                    ' Read X, Y, Z.
                    V1 = VertexFieldReader.Position3(m_Vertices(Ind1))
                    V2 = VertexFieldReader.Position3(m_Vertices(Ind2))
                    V3 = VertexFieldReader.Position3(m_Vertices(Ind3))

                Else ' If TypeOf V Is IVertexPosition3 Then
                    Dim _V1, _V2, _V3 As Vector4

                    ' Read X, Y, Z, W.
                    _V1 = VertexFieldReader.Position4(m_Vertices(Ind1))
                    _V2 = VertexFieldReader.Position4(m_Vertices(Ind2))
                    _V3 = VertexFieldReader.Position4(m_Vertices(Ind3))

                    ' Remove W component.
                    V1 = New Vector3(_V1.X, _V1.Y, _V1.Z)
                    V2 = New Vector3(_V2.X, _V2.Y, _V2.Z)
                    V3 = New Vector3(_V3.X, _V3.Y, _V3.Z)

                End If ' If TypeOf V Is IVertexPosition3 Then

                ' Calculate face normal.
                V4 = Vector3.Cross(V2 - V1, V3 - V1)
                V4.Normalize()

                ' Add to normals of all three vertices.
                Normals(Ind1) += V4
                Normals(Ind2) += V4
                Normals(Ind3) += V4

            Next J ' For J As Integer = 0 To UBound(Ind) Step 3

            ' Free indices.
            Ind = Nothing

        Next I ' For I As Integer = 0 To m_PrimitiveGroups.Count - 1

        ' Now normalize all normals and copy to vertex.
        For I As Integer = 0 To UBound(Normals)
            Normals(I).Normalize()

            ' Copy the normal.
            VertexFieldWriter.Normal3(m_Vertices(I), Normals(I))

        Next I ' For I As Integer = 0 To UBound(Normals)

        ' Free the array.
        Normals = Nothing

        Return False
    End Function

    ''' <summary>
    ''' Renders the primitive from the provided buffer.
    ''' </summary>
    ''' <param name="Device">
    ''' Device to render on.
    ''' </param>
    ''' <param name="VB">
    ''' The vertex buffer.
    ''' </param>
    ''' <param name="IB">
    ''' The index buffer.
    ''' </param>
    ''' <param name="BaseVertexOffset">
    ''' Offset in the vertex buffer.
    ''' </param>
    ''' <param name="BaseIndiceOffset">
    ''' Offset in the indice buffer.
    ''' </param>
    ''' <returns>
    ''' The number of indices rendered.
    ''' </returns>
    ''' <remarks>
    ''' The offsets are not in bytes but number of 
    ''' elements.
    ''' </remarks>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when either of <c>VB</c> or <c>IB</c> is 
    ''' <c>Nothing</c>.
    ''' </exception>
    Public Function Render(ByVal Device As Direct3D.Device, ByVal VB As VertexBuffer, ByVal IB As IndexBuffer,
                           ByVal BaseVertexOffset As Integer, ByVal BaseIndiceOffset As Integer) As Integer

        Dim Vtx As VertexType
        Dim BufIndSize, Offset As Integer

        ' Check inputs.
        If VB Is Nothing Then _
            Throw New ArgumentNullException("VB") _
                : Exit Function

        ' Check inputs.
        If IB Is Nothing Then _
            Throw New ArgumentNullException("IB") _
                : Exit Function

        ' Check inputs.
        If (BaseVertexOffset < 0) OrElse (BaseIndiceOffset < 0) Then _
            Throw New ArgumentOutOfRangeException(
                CStr(
                    IIf(
                        BaseVertexOffset < 0,
                        "BaseVertexOffset",
                        "BaseIndiceOffset"
                        ))) _
                : Exit Function

        ' Get the buffer indice size.
        If IB.Description.Is16BitIndices Then _
            BufIndSize = 2 _
            Else _
            BufIndSize = 4

        ' Check inputs.
        'Trace.Assert(VB.Device Is IB.Device) (???)

        ' Check if copy operation is possible.
        If VB.SizeInBytes < (BaseVertexOffset + Vertices.Count)*Vtx.VertexSize Then _
            Throw New ArgumentException("Vertex buffer too small.") _
                : Exit Function

        ' Check if copy operati3on is possible.
        If IB.SizeInBytes < (BaseIndiceOffset + TotalIndiceCount)*BufIndSize Then _
            Throw New ArgumentException("Index buffer too small..") _
                : Exit Function

        ' Set the active material.
        Material.Apply(Device)

        With Device
            ' Save old vertex format.
            Dim VertexFormat As VertexFormats = .VertexFormat

            ' Set the vertex buffer.
            .SetStreamSource(0, VB, 0)

            ' Set the vertex format.
            .VertexFormat = Vtx.Format

            ' Set the index buffer.
            .Indices = IB

            ' Render all primitives.
            For I As Integer = 0 To m_PrimitiveGroups.Count - 1
                Dim minVtxIndex As Integer = 0,
                    maxVtxIndex As Integer = m_Vertices.Count - 1

                ' See if this is a triangle strip or triangle fan. If it is, then
                ' calculate the minimum and maximum vertex indices.
                If (m_PrimitiveGroups(I).Type = PrimitiveType.TriangleStrip) OrElse
                   (m_PrimitiveGroups(I).Type = PrimitiveType.TriangleFan) Then
                    ' Store the first indice.
                    minVtxIndex = m_PrimitiveGroups(I).Indice(0).ToInt32(Nothing)
                    maxVtxIndex = m_PrimitiveGroups(I).Indice(0).ToInt32(Nothing)

                    For J As Integer = 1 To m_PrimitiveGroups(I).IndiceCount - 1
                        ' Get the indice.
                        Dim ind As Integer = m_PrimitiveGroups(I).Indice(J).ToInt32(Nothing)

                        ' Perform comparision.
                        If ind < minVtxIndex Then minVtxIndex = ind
                        If ind > maxVtxIndex Then maxVtxIndex = ind

                    Next J ' For J As Integer = 1 To m_PrimitiveGroups(I).IndiceCount - 1
                End If ' If (m_PrimitiveGroups(I).Type = PrimitiveType.TriangleStrip) OrElse _
                '           (m_PrimitiveGroups(I).Type = PrimitiveType.TriangleFan) Then

                ' Draw the primitive.
                .DrawIndexedPrimitives(
                    m_PrimitiveGroups(I).Type,
                    BaseVertexOffset,
                    minVtxIndex,
                    maxVtxIndex - minVtxIndex + 1,
                    BaseIndiceOffset + Offset,
                    m_PrimitiveGroups(I).PrimitiveCount
                    )

                ' Increment offset.
                Offset += m_PrimitiveGroups(I).IndiceCount

            Next I ' For I As Integer = 0 To m_PrimitiveGroups.Count - 1

            ' Load old vertex format.
            .VertexFormat = VertexFormat

            ' Remove references.
            .SetStreamSource(0, Nothing, 0)
            .Indices = Nothing

        End With ' With Device

        ' Reset the active material.
        Material.Reset(Device)

        ' Return the offset.
        Return Offset
    End Function

    ''' <summary>
    ''' Performs unbuffered rendering.
    ''' </summary>
    ''' <param name="_Device">
    ''' The device to which render to.
    ''' </param>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>_Device Is Nothing</c>.
    ''' </exception>
    Public Function Render(ByVal _Device As Device) As Boolean
        ' Check inputs.
        If _Device Is Nothing Then _
            Throw New ArgumentNullException("_Device") _
                : Exit Function

        ' Set the active material.
        Material.Apply(_Device)

        ' Render each primitive.
        For I As Integer = 0 To m_PrimitiveGroups.Count - 1
            Dim Ind As IndiceType

            ' Set the vertex format.
            _Device.VertexFormat = GVertexGroup (Of VertexType).VertexFormat

            ' Render the primitive.
            _Device.DrawIndexedUserPrimitives(m_PrimitiveGroups(I).Type,
                                              0,
                                              m_Vertices.Count,
                                              m_PrimitiveGroups(I).PrimitiveCount,
                                              m_PrimitiveGroups(I).GetData(),
                                              CBool(Len(Ind) = 2),
                                              m_Vertices.GetData()
                                              )

        Next I ' For I As Integer = 0 To m_PrimitiveGroups.Count - 1

        ' Reset the active material.
        Material.Reset(_Device)

        Return True
    End Function

    ''' <summary>
    ''' Checks for any sort of errors.
    ''' </summary>
    Friend Function Validate() As Boolean
#If DEBUG Then

  Dim MinIndiceValue, MaxIndiceValue As IndiceType

  ' Set the minimum and maximum indice values.
  MinIndiceValue = Utility.IndiceConverter(Of Integer, IndiceType)(0)
  MaxIndiceValue = Utility.IndiceConverter(Of Integer, IndiceType)(m_Vertices.Count - 1)

  ' Call validate function for each primitive with the given bounds.
  For I As Integer = 0 To m_PrimitiveGroups.Count - 1
   Try
    m_PrimitiveGroups(I).Validate(MinIndiceValue, MaxIndiceValue)

   Catch ex As Exception
    Throw New Exception("Error in m_PrimitiveGroups(" & CStr(I) & ").", ex)
    Exit Function

   End Try

  Next I ' For I As Integer = 0 To m_PrimitiveGroups.Count - 1

#End If

        Return True
    End Function
End Class
