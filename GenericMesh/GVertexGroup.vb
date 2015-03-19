Imports GenericMesh.VertexFields

''' <summary>
''' The vertex group of generic mesh is represented using this class.
''' </summary>
''' <typeparam name="VertexType">
''' The type of vertices this vertex group uses.
''' </typeparam>
Public NotInheritable Class GVertexGroup(Of VertexType As {Structure, IVertex})
 ' -------------------------
 ' Interface(s) Implemented.
 ' -------------------------
 Implements ICloneable

 ' --------------
 ' Class Members.
 ' --------------
 ''' <summary>
 ''' The vertices in this type.
 ''' </summary>
 Private m_Vertices As List(Of VertexType)

 ' ------------------------
 ' Constructors\Finalizers.
 ' ------------------------
 ''' <summary>
 ''' Class constructor.
 ''' </summary>
 Public Sub New()
  m_Vertices = New List(Of VertexType)

 End Sub

 ''' <summary>
 ''' Class constructor.
 ''' </summary>
 ''' <param name="VertexData">
 ''' Creates a vertex group with the given vertices.
 ''' </param>
 ''' <exception cref="ArgumentException">
 ''' Thrown when input array isn't of rank 1.
 ''' </exception>
 ''' <remarks>
 ''' If input array does not exist, then no exception
 ''' is thrown (and is equivalent to calling constructor without
 ''' any arguments). The input array, however must have rank = 1.
 ''' </remarks>
 Public Sub New(ByVal VertexData() As VertexType)
  ' Call default constructor.
  Me.New()

  ' Copy the vertices.
  Append(VertexData)

 End Sub

 ''' <summary>
 ''' Makes a deep copy of this instance (copy constructor).
 ''' </summary>
 ''' <param name="obj">
 ''' The vertex group whose deep copy is to be made.
 ''' </param>
 ''' <remarks>
 ''' This does nothing if <c>obj Is Nothing</c> and is equivalent
 ''' to calling the default constructor.
 ''' </remarks>
 Public Sub New(ByVal obj As GVertexGroup(Of VertexType))
  ' Call default constructor.
  Me.New()

  ' Copy the vertex group if present.
  If obj IsNot Nothing Then _
   obj.CopyTo(Me)

 End Sub

 ''' <summary>
 ''' Class destructor.
 ''' </summary>
 Protected Overrides Sub Finalize()
  ' Release list
  m_Vertices = Nothing

  ' Base destructor.
  MyBase.Finalize()

 End Sub

 ' -----------------
 ' Class properties.
 ' -----------------
 ''' <summary>
 ''' Returns the vertex format of the vertex.
 ''' </summary>
 Public Shared ReadOnly Property VertexFormat() As VertexFormats
  Get
   Dim V As VertexType
   Return V.Format

  End Get

 End Property

 ''' <summary>
 ''' Returns the size of vertex.
 ''' </summary>
 Public Shared ReadOnly Property VertexSize() As Integer
  Get
   Dim V As VertexType
   Return V.VertexSize

  End Get

 End Property

 ''' <summary>
 ''' Returns\Sets the number of vertices in the vertex group.
 ''' </summary>
 ''' <exception cref="ArgumentOutOfRangeException">
 ''' Thrown when <c>value</c> is negative.
 ''' </exception>
 Public Property Count() As Integer
  Get
   Return m_Vertices.Count

  End Get

  Set(ByVal value As Integer)
   Dim OldSize As Integer = m_Vertices.Count

   ' Verify new size.
   If value < 0 Then _
    Throw New ArgumentOutOfRangeException("value") _
  : Exit Property

   ' Free the extra elements.
   If value < OldSize Then _
    m_Vertices.RemoveRange(value, OldSize - value)

   ' Allocate new elements.
   For I As Integer = OldSize To value - 1
    m_Vertices.Add(New VertexType)

   Next I ' For I As Integer = OldSize To value - 1

  End Set

 End Property

 ''' <summary>
 ''' Returns\Sets vertex data.
 ''' </summary>
 ''' <exception  cref="ArgumentOutOfRangeException">
 ''' Thrown when <c>Index</c> is out of bounds.
 ''' </exception>
 Default Public Property Vertex(ByVal Index As Integer) As VertexType
  Get
   ' Check if indice exists.
   If (Index < 0) OrElse (Index >= m_Vertices.Count) Then _
    Throw New ArgumentOutOfRangeException("Index") _
  : Exit Property

   ' Return the data.
   Return m_Vertices(Index)

  End Get

  Set(ByVal value As VertexType)
   ' Check if indice exists.
   If (Index < 0) OrElse (Index >= m_Vertices.Count) Then _
    Throw New ArgumentOutOfRangeException("Index") _
  : Exit Property

   ' Set the data.
   m_Vertices(Index) = value

  End Set

 End Property

 ' ---------
 ' Operators
 ' ---------
 ' None

 ' -----------------
 ' Member Functions.
 ' -----------------
 ''' <summary>
 ''' Converts and appends vertices to the group.
 ''' </summary>
 ''' <typeparam name="InVertexType">
 ''' The input format of the vertices.
 ''' </typeparam>
 ''' <param name="VertexData">
 ''' The vertices to be added.
 ''' </param>
 ''' <param name="_Converter">
 ''' The converter to convert vertices from <c>InVertexType</c> to
 ''' <c>VertexType</c>.
 ''' </param>
 ''' <exception cref="RankException">
 ''' Thrown when input array has invalid rank (&lt;&gt; 1).
 ''' </exception>
 ''' <remarks>
 ''' This does nothing when <c>VertexData Is Nothing</c>.
 ''' </remarks>
 Public Function Append(Of InVertexType As {Structure, IVertex}) _
                       (ByVal VertexData() As InVertexType, _
               Optional ByVal _Converter As System.Converter(Of InVertexType, VertexType) = Nothing) As Boolean

  Dim InputVertices() As VertexType

  ' Check inputs.
  ' -------------
  ' Check if we have an input array.
  If (VertexData Is Nothing) OrElse (VertexData.Length = 0) Then _
   Return False

  ' Check whether we have valid input elements or not.
  If VertexData.Rank <> 1 Then _
   Throw New RankException("'VertexData.Rank <> 1'") _
 : Exit Function

  ' Check if we have a valid converter. If we don't have a converter, use default.
  If (GetType(InVertexType) IsNot GetType(VertexType)) AndAlso (_Converter Is Nothing) Then _
   _Converter = AddressOf Utility.VertexConverter

  ' Convert input indices.
  ' ----------------------
  If GetType(InVertexType) Is GetType(VertexType) Then _
   InputVertices = CType(CObj(VertexData), VertexType()) _
  Else _
   InputVertices = Array.ConvertAll(VertexData, _Converter)

  ' Now add the vertices.
  ' ---------------------
  m_Vertices.AddRange(InputVertices)

  ' Free temporary array.
  ' ---------------------
  InputVertices = Nothing

  Return True

 End Function

 ''' <summary>
 ''' Appends an <c>GVertexGroup</c> to this vertex group.
 ''' </summary>
 ''' <typeparam name="InVertexType">
 ''' The format of input vertices (in the input vertex group).
 ''' </typeparam>
 ''' <param name="obj">
 ''' The input vertex group, which is to be converted and added.
 ''' </param>
 ''' <param name="_Converter">
 ''' The converter to convert vertices from input format to format
 ''' of primitive.
 ''' </param>
 ''' <remarks>
 ''' This does nothing when <c>obj Is Nothing</c>.
 ''' </remarks>
 Public Function Append(Of InVertexType As {Structure, IVertex}) _
                       (ByVal obj As GVertexGroup(Of InVertexType), _
               Optional ByVal _Converter As System.Converter(Of InVertexType, VertexType) = Nothing) As Boolean

  ' Check inputs.
  ' -------------
  ' Check whether we have valid input elements or not.
  If (obj Is Nothing) OrElse (obj.m_Vertices.Count = 0) Then _
   Return False

  ' Verify argument.
  If obj Is Me Then _
   Throw New Exception("'obj Is Me'") _
 : Exit Function

  ' Check if we have a valid converter. If we don't have a converter, use default.
  If (GetType(InVertexType) IsNot GetType(VertexType)) AndAlso (_Converter Is Nothing) Then _
   _Converter = AddressOf Utility.VertexConverter

  ' Do we need to convert the elements?
  ' -----------------------------------
  If GetType(InVertexType) Is GetType(VertexType) Then
   ' Add the vertices.
   ' -----------------
   m_Vertices.AddRange(CType(CObj(obj.m_Vertices), List(Of VertexType)))

  Else ' If GetType(InVertexType) Is GetType(VertexType) Then
   ' Convert the vertices.
   ' ---------------------
   Dim InputVertices As List(Of VertexType)

   ' Convert the vertices.
   InputVertices = obj.m_Vertices.ConvertAll(_Converter)

   ' Now add the elements.
   m_Vertices.AddRange(InputVertices)

   ' Free the input elements.
   InputVertices = Nothing

  End If ' If GetType(InVertexType) Is GetType(VertexType) Then   

  Return True

 End Function

 ''' <summary>
 ''' Copies the vertices to an array.
 ''' </summary>
 ''' <typeparam name="OutVertexType">
 ''' Output format of the vertices.
 ''' </typeparam>
 ''' <param name="P">
 ''' The array to which vertices are to be copied.
 ''' </param>
 ''' <param name="SourceIndex">
 ''' The index from which vertices are copied from element array.
 ''' </param>
 ''' <param name="DestinationIndex">
 ''' The index from which vertices are copied to destination array.
 ''' </param>
 ''' <param name="Length">
 ''' The number of vertices to copy.
 ''' </param>
 ''' <param name="_Converter">
 ''' The converter to convert vertices from <c>VertexType</c> to
 ''' <c>OutVertexType</c>.
 ''' </param>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when <c>P Is Nothing</c>.
 ''' </exception>
 ''' <exception cref="ArgumentOutOfRangeException">
 ''' Thrown when <c>StartIndex</c> or <c>DestinationIndex</c> has
 ''' values out of range (for the respective arrays). Also thrown
 ''' when <c>Length</c> is too large for either of the arrays.
 ''' </exception>
 ''' <exception cref="RankException">
 ''' Thrown when input array is of invalid rank (&lt;&gt; 1).
 ''' </exception>
 ''' <remarks>
 ''' NOTE: The output array is not modified if there are no indices in 
 ''' the source array (i.e. this instance).
 ''' </remarks>
 Public Function CopyTo(Of OutVertexType As {Structure, IVertex}) _
                       (ByVal P() As OutVertexType, _
               Optional ByVal SourceIndex As Integer = 0, _
               Optional ByVal DestinationIndex As Integer = 0, _
               Optional ByVal Length As Integer = -1, _
               Optional ByVal _Converter As System.Converter(Of VertexType, OutVertexType) = Nothing) As Boolean

  ' Check inputs.
  ' -------------
  ' Check if we have vertices.
  If (m_Vertices.Count = 0) Then _
   Return False

  ' Check input array.
  If P Is Nothing Then _
   Throw New ArgumentNullException("P") _
 : Exit Function

  ' Check input array.
  If P.Rank <> 1 Then _
   Throw New RankException( "'P.Rank <> 1'") _
 : Exit Function

  ' Check bounds.
  If (SourceIndex < 0) OrElse (SourceIndex >= m_Vertices.Count) Then _
   Throw New ArgumentOutOfRangeException("SourceIndex") _
 : Exit Function

  ' Check bounds.
  If (DestinationIndex < 0) OrElse (DestinationIndex > UBound(P)) Then _
   Throw New ArgumentOutOfRangeException("DestinationIndex") _
 : Exit Function

  ' Check bounds.
  If (Length < -1) OrElse (Length = 0) Then _
   Throw New ArgumentOutOfRangeException("Length") _
 : Exit Function

  ' Set the proper element count if not given.
  If Length = -1 Then _
   Length = Math.Min(m_Vertices.Count - SourceIndex, P.Length - DestinationIndex)

  ' Check bounds.
  If (Length > m_Vertices.Count - SourceIndex) OrElse (Length > P.Length - DestinationIndex) Then _
   Throw New ArgumentOutOfRangeException("Length") _
 : Exit Function

  ' Check if we have a valid converter. If we don't have a converter, use default.
  If (GetType(VertexType) IsNot GetType(OutVertexType)) AndAlso (_Converter Is Nothing) Then _
   _Converter = AddressOf Utility.VertexConverter

  ' Do we need to convert the indices?
  ' ----------------------------------
  If GetType(OutVertexType) Is GetType(VertexType) Then
   ' Copy directly.
   ' --------------
   m_Vertices.CopyTo(SourceIndex, _
                     CType(CObj(P), VertexType()), _
                     DestinationIndex, _
                     Length)

  Else ' If GetType(OutVertexType) Is GetType(VertexType) Then
   ' Convert and copy vertices.
   ' --------------------------
   For I As Integer = 0 To Length - 1
    P(DestinationIndex + I) = _Converter(m_Vertices(SourceIndex + I))

   Next I ' For I As Integer = 0 To Length - 1

  End If ' If GetType(OutVertexType) Is GetType(VertexType) Then

  Return False

 End Function

 ''' <summary>
 ''' Copies the vertex group to another <c>GVertexGroup</c> class.
 ''' </summary>
 ''' <typeparam name="OutVertexType">
 ''' Output format of the vertices.
 ''' </typeparam>
 ''' <param name="obj">
 ''' The vertex group to which this vertex group is to be copied.
 ''' </param> 
 ''' <param name="_Converter">
 ''' The converter which will convert vertices from vertex group
 ''' format to output format.
 ''' </param>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when <c>obj Is Nothing</c>.
 ''' </exception>
 ''' <remarks>
 ''' Overwrites the input object.
 ''' </remarks>
 Public Function CopyTo(Of OutVertexType As {Structure, IVertex}) _
                       (ByVal obj As GVertexGroup(Of OutVertexType), _
               Optional ByVal _Converter As System.Converter(Of VertexType, OutVertexType) = Nothing) As Boolean

  ' Check inputs.
  ' -------------
  ' Check input object.
  If obj Is Nothing Then _
   Throw New ArgumentNullException("obj") _
 : Exit Function

  ' Check input object.
  If obj Is Me Then _
   Throw New Exception("'obj is Me'") _
 : Exit Function

  ' Empty, and copy.
  ' ----------------
  obj.m_Vertices.Clear()
  obj.Append(Me, _Converter)

  Return True

 End Function

 ''' <summary>
 ''' Removes vertices.
 ''' </summary>
 ''' <param name="Index">
 ''' The index of the first vertex to be removed.
 ''' </param>
 ''' <param name="RemoveCount">
 ''' Number of vertices to be removed.
 ''' </param>
 ''' <exception cref="ArgumentOutOfRangeException">
 ''' Thrown when <c>Index</c> is out of bounds.
 ''' Also thrown when <c>RemoveCount</c> is either negative,
 ''' zero, or larger than the count of elements that may be removed.
 ''' </exception>
 ''' <remarks>
 ''' Note that this does nothing if array is empty.
 ''' </remarks>
 Public Function Remove(ByVal Index As Integer, Optional ByVal RemoveCount As Integer = 1) As Boolean
  ' Check inputs.
  ' -------------
  ' Check if empty.
  If m_Vertices.Count = 0 Then _
   Return False

  ' Check the index of first vertex to be removed.
  If (Index < 0) OrElse (Index >= m_Vertices.Count) Then _
   Throw New ArgumentOutOfRangeException("Index") _
 : Exit Function

  ' Check the count of vertices to be removed.
  If (RemoveCount <= 0) OrElse (RemoveCount > m_Vertices.Count) Then _
   Throw New ArgumentOutOfRangeException("RemoveCount") _
 : Exit Function

  ' Check whether the index of the last vertex to be removed is in bounds or not.
  If Index + RemoveCount > m_Vertices.Count Then _
   Throw New ArgumentOutOfRangeException("RemoveCount") _
 : Exit Function

  ' Remove the vertices.
  ' --------------------
  m_Vertices.RemoveRange(Index, RemoveCount)

  Return True

 End Function

 ''' <summary>
 ''' Removes vertices.
 ''' </summary>
 ''' <param name="StartIndex">
 ''' The index of the first vertex (inclusive) which is removed.
 ''' </param>
 ''' <param name="EndIndex">
 ''' The index of the last vertex (inclusive) which is removed.
 ''' </param>
 ''' <exception cref="ArgumentOutOfRangeException">
 ''' Thrown when <c>StartIndex</c> or <c>EndIndex</c> is out of bounds.
 ''' </exception>
 Public Function RemoveRange(ByVal StartIndex As Integer, ByVal EndIndex As Integer) As Boolean
  ' Check inputs.
  ' -------------
  ' Check if empty.
  If m_Vertices.Count = 0 Then _
   Return False

  ' Check bounds.
  If (StartIndex < 0) OrElse (StartIndex >= m_Vertices.Count) Then _
   Throw New ArgumentOutOfRangeException("StartIndex") _
 : Exit Function

  ' Check bounds.
  If (EndIndex < 0) OrElse (EndIndex >= m_Vertices.Count) Then _
   Throw New ArgumentOutOfRangeException("EndIndex") _
 : Exit Function

  ' Check bounds.
  If (StartIndex > EndIndex) Then _
   Throw New ArgumentOutOfRangeException("StartIndex' or 'EndIndex'") _
 : Exit Function

  ' Remove the vertices.
  ' --------------------
  m_Vertices.RemoveRange(StartIndex, EndIndex - StartIndex + 1)

  Return True

 End Function

 ''' <summary>
 ''' Removes all vertices from this group.
 ''' </summary>
 Public Function RemoveAll() As Boolean
  m_Vertices.Clear()

  Return True

 End Function

 ''' <summary>
 ''' Clones this primitive group and returns it.
 ''' </summary>
 ''' <remarks>
 ''' Copy is a deep copy.
 ''' </remarks>
 Public Function Clone() As Object Implements System.ICloneable.Clone
  Return New GVertexGroup(Of VertexType)(Me)

 End Function

 ''' <summary>
 ''' Transforms all (or some) vertices in the vertex group.
 ''' </summary>
 ''' <param name="M">
 ''' Matrix to be used for the transformation.
 ''' </param>
 ''' <param name="_VertexTransformer">
 ''' Function to be used for performing transformation.
 ''' </param>
 ''' <param name="StartIndex">
 ''' The index of the first vertex which will be transformed.
 ''' </param>
 ''' <param name="Count">
 ''' Number of vertices that will be transformed.
 ''' </param>
 ''' <exception cref="ArgumentOutOfRangeException">
 ''' Thrown when <c>StartIndex</c> is out of bounds.
 ''' Also thrown when <c>Count</c> is either negative, zero, 
 ''' or larger than the count of vertices that may be transformed.
 ''' </exception>
 ''' <remarks>
 ''' If you do not provide the optional arguments, then all vertices
 ''' are transformed.
 ''' </remarks>
 Public Function Transform(ByVal M As Matrix, _
                  Optional ByVal _VertexTransformer As Func(Of VertexType, Matrix, VertexType) = Nothing, _
                  Optional ByVal StartIndex As Integer = 0, _
                  Optional ByVal Count As Integer = -1) As Boolean

  ' Check inputs.
  ' -------------
  ' Check the index of first element to be removed.
  If (StartIndex < 0) OrElse (StartIndex >= m_Vertices.Count) Then _
   Throw New ArgumentOutOfRangeException("StartIndex") _
 : Exit Function

  ' If the count was not provided, assume one.
  If (Count = -1) Then _
   Count = m_Vertices.Count - StartIndex

  ' Check the count of elements to be removed.
  If (Count <= 0) OrElse (Count > m_Vertices.Count) Then _
   Throw New ArgumentOutOfRangeException("Count") _
 : Exit Function

  ' Check whether the index of the last element to be removed is in bounds or not.
  If StartIndex + Count > m_Vertices.Count Then _
   Throw New ArgumentOutOfRangeException("Count") _
 : Exit Function

  ' Check input function.
  If _VertexTransformer Is Nothing Then _
   _VertexTransformer = AddressOf Utility.TransformVertex

  ' Now transform the vertices.
  ' ---------------------------
  For I As Integer = StartIndex To StartIndex + Count - 1
   m_Vertices(I) = _VertexTransformer(m_Vertices(I), M)

  Next I ' For I As Integer = StartIndex To StartIndex + Count - 1

  Return True

 End Function

 ''' <summary>
 ''' Transforms a range of vertices in the vertex group.
 ''' </summary>
 ''' <param name="M">
 ''' Matrix to be used for the transformation.
 ''' </param>
 ''' <param name="_VertexTransformer">
 ''' Function to be used for performing transformation.
 ''' </param>
 ''' <param name="StartIndex">
 ''' The index of the first vertex which will be transformed.
 ''' </param>
 ''' <param name="EndIndex">
 ''' The index of the last vertex which will be transformed.
 ''' </param>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when <c>_VertexTransformer Is Nothing</c>.
 ''' </exception>
 ''' <exception cref="ArgumentOutOfRangeException">
 ''' Thrown when <c>StartIndex</c> or <c>EndIndex</c> is out of bounds.
 ''' </exception>
 Public Function TransformRange(ByVal M As Matrix, _
                                ByVal _VertexTransformer As Func(Of VertexType, Matrix, VertexType), _
                                ByVal StartIndex As Integer, _
                                ByVal EndIndex As Integer) As Boolean

  ' Check inputs.
  ' -------------
  ' Check bounds.
  If (StartIndex < 0) OrElse (StartIndex >= m_Vertices.Count) Then _
   Throw New ArgumentOutOfRangeException("StartIndex") _
 : Exit Function

  ' Check bounds.
  If (EndIndex < 0) OrElse (EndIndex >= m_Vertices.Count) Then _
   Throw New ArgumentOutOfRangeException("EndIndex") _
 : Exit Function

  ' Check bounds.
  If (StartIndex > EndIndex) Then _
   Throw New ArgumentOutOfRangeException("StartIndex' or 'EndIndex'") _
 : Exit Function

  ' Delegate call to other function.
  ' --------------------------------
  Return Transform(M, _VertexTransformer, StartIndex, EndIndex - StartIndex + 1)

 End Function

 ''' <summary>
 ''' Calls the given method for the specified range of vertices.
 ''' </summary>
 ''' <typeparam name="DataType">
 ''' Format of associated data.
 ''' </typeparam>
 ''' <param name="P">
 ''' The associated data array.
 ''' </param>
 ''' <param name="_VP">
 ''' The method which processes the data and vertices.
 ''' </param>
 ''' <param name="StartIndex">
 ''' The index from where processing is started for mesh vertices.
 ''' </param>
 ''' <param name="PBaseIndex">
 ''' The index from where processing is started for associated data.
 ''' </param>
 ''' <param name="Count">
 ''' The number of vertices to process.
 ''' </param>
 ''' <exception cref="RankException">
 ''' Thrown when <c>P</c> array has invalid rank (&lt;&gt; 1).
 ''' </exception>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when <c>P Is Nothing</c>.
 ''' Also thrown when <c>_VP Is Nothing</c>.
 ''' </exception>
 ''' <exception cref="ArgumentOutOfRangeException">
 ''' Thrown when <c>StartIndex</c> or <c>PBaseIndex</c> is out of bounds,
 ''' or when <c>Length</c> is invalid or out of bounds for any of the
 ''' arrays.
 ''' </exception>
 ''' <remarks>
 ''' This does nothing if there aren't any vertices.
 ''' </remarks>
 Public Function ProcessVertices(Of DataType As {Structure}) _
                                (ByVal P() As DataType, _
                                 ByVal _VP As Utility.VertexProcessor(Of DataType, VertexType), _
                        Optional ByVal StartIndex As Integer = 0, _
                        Optional ByVal PBaseIndex As Integer = 0, _
                        Optional ByVal Count As Integer = -1) As Boolean

  ' Check inputs.
  ' -------------
  ' Check if we have indices.
  If m_Vertices.Count = 0 Then _
   Return False

  ' Check 'P' array.
  If P Is Nothing Then _
   Throw New ArgumentNullException("P") _
 : Exit Function

  ' Check 'P' array.
  If P.Rank <> 1 Then _
   Throw New RankException("'P.Rank <> 1'") _
 : Exit Function

  ' Check bounds.
  If (StartIndex < 0) OrElse (StartIndex >= m_Vertices.Count) Then _
   Throw New ArgumentOutOfRangeException("StartIndex") _
 : Exit Function

  ' Check bounds.
  If (PBaseIndex < 0) OrElse (PBaseIndex > UBound(P)) Then _
   Throw New ArgumentOutOfRangeException("PBaseIndex") _
 : Exit Function

  ' Check bounds.
  If (Count < -1) OrElse (Count = 0) Then _
   Throw New ArgumentOutOfRangeException("Count") _
 : Exit Function

  ' Set the proper element count if not given.
  If Count = -1 Then _
   Count = Math.Min(m_Vertices.Count - StartIndex, P.Length - PBaseIndex)

  ' Check bounds.
  If (Count > m_Vertices.Count - StartIndex) OrElse (Count > P.Length - PBaseIndex) Then _
   Throw New ArgumentOutOfRangeException("Count") _
 : Exit Function

  ' Check vertex processor.
  If _VP Is Nothing Then _
   Throw New ArgumentNullException("_VP") _
 : Exit Function

  ' Process data.
  ' -------------
  For I As Integer = 0 To Count - 1
   _VP(P(PBaseIndex + I), m_Vertices(StartIndex + I))

  Next I ' For I As Integer = 0 To Count - 1

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
  Dim V As VertexType = Nothing

  ' Check inputs.
  If Not TypeOf V Is IVertexPosition3 Then _
   Throw New Exception("'VertexType' does not implement 'IVertexPosition3'.") _
 : Exit Function

  ' Check if we have verices.
  If m_Vertices.Count = 0 Then _
   MinExtents = New Vector3(1, 1, 1) _
 : MaxExtents = New Vector3(-1, -1, -1) _
 : Return False

  ' Set the initial extents.
  MinExtents = VertexFieldReader.Position3(m_Vertices(0))
  MaxExtents = MinExtents

  ' Now process other vertices.
  For I As Integer = 1 To m_Vertices.Count - 1
   ' Get the position vector.
   Dim P As Vector3 = VertexFieldReader.Position3(m_Vertices(I))

   ' Minimize and maximize the min\max vectors.
   MinExtents.Minimize(P)
   MaxExtents.Maximize(P)

  Next I ' For I As Integer = 1 To m_Vertices.Count - 1

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
 Public Function GetMeshExtents(ByRef MinExtents As Vector4, ByRef MaxExtents As Vector4) As Boolean
  Dim V As VertexType = Nothing

  ' Check inputs.
  If Not TypeOf V Is IVertexPosition4 Then _
   Throw New Exception("'VertexType' does not implement 'IVertexPosition4'.") _
 : Exit Function

  ' Check if we have verices.
  If m_Vertices.Count = 0 Then _
   MinExtents = New Vector4(1, 1, 1, 1) _
 : MaxExtents = New Vector4(-1, -1, -1, -1) _
 : Return False

  ' Set the initial extents.
  MinExtents = VertexFieldReader.Position4(m_Vertices(0))
  MaxExtents = MinExtents

  ' Now process other vertices.
  For I As Integer = 1 To m_Vertices.Count - 1
   ' Get the position vector.
   Dim P As Vector4 = VertexFieldReader.Position4(m_Vertices(I))

   ' Minimize and maximize the min\max vectors.
   MinExtents.Minimize(P)
   MaxExtents.Maximize(P)

  Next I ' For I As Integer = 1 To m_Vertices.Count - 1

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
 ''' <exception cref="Exception">
 ''' Thrown when <c>VertexType</c> does not implement <c>IVertexPosition3</c>.
 ''' </exception>
 ''' <remarks>
 ''' If <c>Center</c> = {0, 0, 0} and <c>Radius</c> = 0 then mesh has no vertices.
 ''' If <c>Center</c> &lt;&gt; {0, 0, 0} and <c>Radius</c> = 0 then mesh has 1 vertex.
 ''' Otherwise, the <c>Center</c> and <c>Radius</c> are correct.
 ''' </remarks>
 Public Function GetMeshSphere(ByRef Center As Vector3, ByRef Radius As Single) As Boolean
  Dim Min, Max As Vector3

  If m_Vertices.Count = 0 Then _
   Center = New Vector3(0, 0, 0) _
 : Radius = 0 _
 : Return False

  ' Get the mesh extents.
  GetMeshExtents(Min, Max)

  ' Get the center.
  Center = (Min + Max) * 0.5

  ' Get the radius.
  Radius = (Max - Min).Length() / 2

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
 ''' <exception cref="Exception">
 ''' Thrown when <c>VertexType</c> does not implement <c>IVertexPosition3</c>.
 ''' </exception>
 ''' <remarks>
 ''' If <c>Center</c> = {0, 0, 0, 1} and <c>Radius</c> = 0 then mesh has no vertices.
 ''' If <c>Center</c> &lt;&gt; {0, 0, 0, 1} and <c>Radius</c> = 0 then mesh has 1 vertex.
 ''' Otherwise, the <c>Center</c> and <c>Radius</c> are correct.
 ''' </remarks>
 Public Function GetMeshSphere(ByRef Center As Vector4, ByRef Radius As Single) As Boolean
  Dim Min, Max As Vector4

  If m_Vertices.Count = 0 Then _
   Center = New Vector4(0, 0, 0, 1) _
 : Radius = 0 _
 : Return False

  ' Get the mesh extents.
  GetMeshExtents(Min, Max)

  ' Get the center.
  Center = (Min + Max) * 0.5

  ' Get the radius.
  Radius = (Max - Min).Length() / 2

  Return True

 End Function

 ''' <summary>
 ''' Flips the normal of all vertices.
 ''' </summary>
 Public Function FlipNormals() As Boolean
  Dim V As VertexType = Nothing

  If m_Vertices.Count = 0 Then _
   Return False

  ' Check inputs.
  If Not TypeOf V Is IVertexNormal3 Then _
   Throw New Exception("'VertexType' does not implement 'IVertexNormal3'.") _
 : Exit Function

  ' Flip normals.
  For I As Integer = 0 To m_Vertices.Count - 1
   ' Get the vertex.
   V = m_Vertices(I)

   ' Get the vertex and reverse it's normal.
   VertexFieldWriter.Normal3(V, -VertexFieldReader.Normal3(V))

   ' Write the vertex.
   m_Vertices(I) = V

  Next I ' For I As Integer = 0 To m_Vertices.Count - 1

  Return True

 End Function

 ''' <summary>
 ''' Returns the vertex data array.
 ''' </summary>
 Friend Function GetData() As Object
  Static Vertices() As VertexType = Nothing

  If Vertices IsNot Nothing Then _
   Vertices = Nothing

  Vertices = m_Vertices.ToArray()

  Return Vertices

 End Function

 ''' <summary>
 ''' Writes the vertex data to the buffer.
 ''' </summary>
 ''' <param name="VB">
 ''' The buffer to which write to.
 ''' </param>
 ''' <param name="LockAtOffsetInBytes">
 ''' Data required for calling the <c>SetData</c> method.
 ''' </param>
 ''' <param name="_LockFlags">
 ''' Data required for calling the <c>SetData</c> method.
 ''' </param>
 ''' <returns>
 ''' The size of data written.
 ''' </returns>
 Friend Function WriteDataToBuffer(ByVal VB As VertexBuffer, ByVal LockAtOffsetInBytes As Integer, ByVal _LockFlags As LockFlags) As Integer
  Dim Vtx() As VertexType

  ' Is there any data to write?
  If m_Vertices.Count = 0 Then _
   Return 0

  ' Check inputs.
  If VB Is Nothing Then _
   Throw New ArgumentNullException("VB") _
 : Exit Function

  ' Get the vertex data.
  Vtx = m_Vertices.ToArray()

  ' Set the data.
  VB.SetData(Vtx, LockAtOffsetInBytes, _LockFlags)

  ' Remove the temporary array.
  Vtx = Nothing

  ' Return the size of data written.
  Return m_Vertices.Count * m_Vertices(0).VertexSize

 End Function

End Class
