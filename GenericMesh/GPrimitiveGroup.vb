Imports GenericMath
Imports GenericMesh.Exceptions

''' <summary>
''' The primitive group of generic mesh is represented using this class.
''' </summary>
''' <typeparam name="IndiceType">
''' The type of indices this primitive uses.
''' </typeparam>
''' <remarks>
''' Only system-defined types should be used.
''' </remarks>
Public NotInheritable Class GPrimitiveGroup (Of IndiceType As {Structure, IConvertible})
    ' -------------------------
    ' Interface(s) Implemented.
    ' -------------------------
    Implements ICloneable

    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>
    ''' This indicates the type of primitive.
    ''' </summary>
    Private m_Type As PrimitiveType

    ''' <summary>
    ''' The indices in this type.
    ''' </summary>
    Protected m_Indices As List(Of IndiceType)

    ' ------------------------
    ' Constructors\Finalizers.
    ' ------------------------
    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    Public Sub New()
        ' Initalize the list.
        m_Indices = New List(Of IndiceType)
    End Sub

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    ''' <param name="IndiceData">
    ''' Creates a primitive group with the given indices.
    ''' </param>
    ''' <exception cref="ArgumentException">
    ''' Thrown when input array isn't of rank 1.
    ''' </exception>
    ''' <remarks>
    ''' If input array does not exist, then no exception
    ''' is thrown (and is equivalent to calling constructor without
    ''' any arguments). The input array, however must have rank = 1.
    ''' </remarks>
    Public Sub New(ByVal IndiceData() As IndiceType)
        ' Call default constructor.
        Me.New()

        ' Append all elements.
        Append(IndiceData)
    End Sub

    ''' <summary>
    ''' Makes a deep copy of this instance (copy constructor).
    ''' </summary>
    ''' <param name="obj">
    ''' The primitive group whose deep copy is to be made.
    ''' </param>
    ''' <remarks>
    ''' This does nothing if <c>obj Is Nothing</c> and is equivalent
    ''' to calling the default constructor.
    ''' </remarks>
    Public Sub New(ByVal obj As GPrimitiveGroup(Of IndiceType))
        ' Call default constructor
        Me.New()

        ' Copy the primitive type.
        If obj IsNot Nothing Then _
            obj.CopyTo(Me)
    End Sub

    ''' <summary>
    ''' Class destructor.
    ''' </summary>
    Protected Overrides Sub Finalize()
        ' Release list.
        m_Indices = Nothing

        ' Base destructor.
        MyBase.Finalize()
    End Sub

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns\Sets the primitive type.
    ''' </summary>
    ''' <exception cref="ArgumentException">
    ''' Thrown when <c>value</c> is not a member of <c>PrimitiveType</c>.
    ''' </exception>
    Public Property Type() As PrimitiveType
        Get
            ' Return data.
            Return m_Type
        End Get

        Set(ByVal value As PrimitiveType)
            ' Check valid input.
            If (value < PrimitiveType.PointList) OrElse (value > PrimitiveType.TriangleFan) Then _
                Throw New ArgumentException("Invalid 'value'.") _
                    : Exit Property

            ' Set the data.
            m_Type = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets the number of indices in the primitive group.
    ''' </summary>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>value</c> is negative.
    ''' </exception>
    Public Property IndiceCount() As Integer
        Get
            Return m_Indices.Count
        End Get

        Set(ByVal value As Integer)
            Dim OldSize As Integer = m_Indices.Count

            ' Verify new size.
            If value < 0 Then _
                Throw New ArgumentOutOfRangeException("value") _
                    : Exit Property

            ' Free the extra elements.
            If value < OldSize Then _
                m_Indices.RemoveRange(value, OldSize - value)

            ' Allocate new elements.
            For I As Integer = OldSize To value - 1
                m_Indices.Add(New IndiceType)

            Next I ' For I As Integer = OldSize To value - 1
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets the primitive count.
    ''' </summary>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>value &lt; 0</c>.
    ''' </exception>
    ''' <exception cref="MeshPrimitiveTypeException">
    ''' Thrown when the primitive type is not set.
    ''' </exception>
    ''' <remarks>
    ''' This is NOT guaranteed to return correct count. It is assumed that the primitive
    ''' type has proper indices (requirements of indices). Also, if type is not set or 
    ''' invalid, this will throw an exception.
    ''' </remarks>
    Public Property PrimitiveCount() As Integer
        Get
            If m_Indices.Count = 0 Then _
                Return 0

            Select Case m_Type
                ' POINT LIST
                Case PrimitiveType.PointList
                    Return m_Indices.Count

                    ' LINE LIST
                Case PrimitiveType.LineList
                    Return m_Indices.Count\2

                    ' LINE STRIP
                Case PrimitiveType.LineStrip
                    Return m_Indices.Count - 1

                    ' TRIANGLE LIST
                Case PrimitiveType.TriangleList
                    Return m_Indices.Count\3

                    ' TRIANGLE STRIP
                Case PrimitiveType.TriangleStrip
                    Return m_Indices.Count - 2

                    ' TRIANGLE FAN
                Case PrimitiveType.TriangleFan
                    Return m_Indices.Count - 2

                    ' UNKNOWN (error)
                Case Else
                    Throw New MeshPrimitiveTypeException("Primitive type not defined.")
                    Exit Property

            End Select ' Select Case m_Type
        End Get

        Set(ByVal value As Integer)
            If value < 0 Then _
                Throw New ArgumentOutOfRangeException("value") _
                    : Exit Property

            ' Clean-up
            If (value = 0) Then _
                IndiceCount = 0 _
                    : Exit Property

            Select Case m_Type
                ' POINT LIST
                Case PrimitiveType.PointList
                    IndiceCount = value

                    ' LINE LIST
                Case PrimitiveType.LineList
                    IndiceCount = value*2

                    ' LINE STRIP
                Case PrimitiveType.LineStrip
                    IndiceCount = value + 1

                    ' TRIANGLE LIST
                Case PrimitiveType.TriangleList
                    IndiceCount = value*3

                    ' TRIANGLE STRIP
                Case PrimitiveType.TriangleStrip
                    IndiceCount = value + 2

                    ' TRIANGLE FAN
                Case PrimitiveType.TriangleFan
                    IndiceCount = value + 2

                    ' UNKNOWN (error)
                Case Else
                    Throw New MeshPrimitiveTypeException("Primitive type not defined.")
                    Exit Property

            End Select ' Select Case m_Type
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets indice data.
    ''' </summary>
    ''' <param name="Index">
    ''' The indice to access\modify.
    ''' </param>
    ''' <exception  cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>Index</c> is out of bounds.
    ''' </exception>
    Default Public Property Indice(ByVal Index As Integer) As IndiceType
        Get
            ' Check if indice exists.
            If (Index < 0) OrElse (Index >= m_Indices.Count) Then _
                Throw New ArgumentOutOfRangeException("Index") _
                    : Exit Property

            ' Return the data.
            Return m_Indices(Index)
        End Get

        Set(ByVal value As IndiceType)
            ' Check if indice exists.
            If (Index < 0) OrElse (Index >= m_Indices.Count) Then _
                Throw New ArgumentOutOfRangeException("Index") _
                    : Exit Property

            ' Set the data.
            m_Indices(Index) = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the number of indices per primitive when converted
    ''' to "list" type. 
    ''' </summary>
    ''' <remarks>
    ''' Obviously this is not applicable for non-"list" primitives.
    ''' </remarks>
    Public ReadOnly Property IndicesPerPrimitive() As Integer
        Get
            Select Case m_Type
                ' POINT type.
                Case PrimitiveType.PointList
                    Return 1

                    ' LINE type.
                Case PrimitiveType.LineList, PrimitiveType.LineStrip
                    Return 2

                    ' TRIANGLE type.
                Case PrimitiveType.TriangleList, PrimitiveType.TriangleStrip, PrimitiveType.TriangleFan
                    Return 3

                    ' UNKNOWN (error).
                Case Else
                    Return 0

            End Select ' Select Case m_Type
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
    ''' Converts and appends indices to the array.
    ''' </summary>
    ''' <typeparam name="InIndiceType">
    ''' The input format of the indices.
    ''' </typeparam>
    ''' <param name="IndiceData">
    ''' The indices to be added.
    ''' </param>
    ''' <param name="_Converter">
    ''' The converter to convert indices from <c>InIndiceType</c> to
    ''' <c>IndiceType</c>.
    ''' </param>
    ''' <exception cref="RankException">
    ''' Thrown when input array has invalid rank (&lt;&gt; 1).
    ''' </exception> 
    ''' <remarks>
    ''' This does nothing when <c>ElementData Is Nothing</c>.
    ''' </remarks>
    Public Function Append (Of InIndiceType As {Structure, IConvertible}) _
        (ByVal IndiceData() As InIndiceType,
         Optional ByVal _Converter As System.Converter(Of InIndiceType, IndiceType) = Nothing) As Boolean

        Dim InputIndices() As IndiceType

        ' Check inputs.
        ' -------------
        ' Check if we have an input array.
        If (IndiceData Is Nothing) OrElse (IndiceData.Length = 0) Then _
            Return False

        ' Check whether we have valid input elements or not.
        If IndiceData.Rank <> 1 Then _
            Throw New RankException("'IndiceData.Rank <> 1'") _
                : Exit Function

        ' Use the default converter if there isn't any.
        If _Converter Is Nothing Then _
            _Converter = AddressOf Utility.IndiceConverter

        ' Convert the indices.
        ' --------------------
        If GetType(InIndiceType) Is GetType(IndiceType) Then _
            InputIndices = CType(CObj(IndiceData), IndiceType()) _
            Else _
            InputIndices = Array.ConvertAll(IndiceData, _Converter)

        ' Now add the indices.
        ' --------------------
        m_Indices.AddRange(InputIndices)

        ' Free temporary array.
        ' ---------------------
        InputIndices = Nothing

        Return True
    End Function

    ''' <summary>
    ''' Appends a primitive group to the primitive group.
    ''' </summary>
    ''' <typeparam name="InIndiceType">
    ''' The format of input indices (in the input primitive group).
    ''' </typeparam>
    ''' <param name="obj">
    ''' The input primitive group, which is to be converted and added.
    ''' </param>
    ''' <param name="_Converter">
    ''' The converter to convert indices from input format to format
    ''' of primitive.
    ''' </param>
    ''' <exception cref="MeshPrimitiveTypeException">
    ''' Thrown when an incompatible type of primitive group is appended
    ''' to this instance (i.e. when <c>IndicesPerPrimitive</c> is not same
    ''' for both).
    ''' </exception>
    ''' <remarks>
    ''' This does nothing when <c>obj Is Nothing</c>.
    ''' Also, this converts the current instance to a "list" type
    ''' if it's already not a list type.
    ''' </remarks>
    Public Function Append (Of InIndiceType As {Structure, IConvertible}) _
        (ByVal obj As GPrimitiveGroup(Of InIndiceType),
         Optional ByVal _Converter As System.Converter(Of InIndiceType, IndiceType) = Nothing) As Boolean

        Dim _obj As GPrimitiveGroup(Of InIndiceType)
        Dim InputIndices As List(Of IndiceType)

        ' Check if we have valid inputs.
        ' ------------------------------
        ' Verify argument.
        If obj Is Nothing Then _
            Return False

        ' Verify argument.
        If obj Is Me Then _
            Throw New Exception("'obj Is Me'") _
                : Exit Function

        ' Use the default converter if there isn't any.
        If _Converter Is Nothing Then _
            _Converter = AddressOf Utility.IndiceConverter

        ' Now check for any sort of errors, either in this mesh,
        ' or the input mesh.
        ' ------------------------------------------------------
        Me.Validate()
        obj.Validate()

        ' Finally check if both types of primitives are compatible or not.
        ' ----------------------------------------------------------------
        If Me.IndicesPerPrimitive <> obj.IndicesPerPrimitive Then _
            Throw New MeshPrimitiveTypeException("'IndicesPerPrimitive' not same for current primitive " &
                                                 "group and input primitive group.") _
                : Exit Function

        ' Convert this instance to "list" type if needed.
        If (Me.m_Type = PrimitiveType.LineStrip) OrElse
           (Me.m_Type = PrimitiveType.TriangleStrip) OrElse
           (Me.m_Type = PrimitiveType.TriangleFan) Then _
            Me.ConvertToList()

        ' Convert input to "list" type if needed, but
        ' first make a copy first. Otherwise a reference is fine.
        If (obj.m_Type = PrimitiveType.LineStrip) OrElse
           (obj.m_Type = PrimitiveType.TriangleStrip) OrElse
           (obj.m_Type = PrimitiveType.TriangleFan) Then _
            _obj = New GPrimitiveGroup(Of InIndiceType)(obj) _
                : _obj.ConvertToList() _
            Else _
            _obj = obj

        ' Convert the format if needed.
        ' -----------------------------
        If GetType(InIndiceType) Is GetType(IndiceType) Then _
            InputIndices = CType(CObj(_obj.m_Indices), List(Of IndiceType)) _
            Else _
            InputIndices = _obj.m_Indices.ConvertAll(_Converter)

        ' Add the elements.
        ' -----------------
        m_Indices.AddRange(InputIndices)

        ' Remove the temporary object.
        ' ----------------------------
        InputIndices = Nothing

        Return True
    End Function

    ''' <summary>
    ''' Copies the indices to an array.
    ''' </summary>
    ''' <typeparam name="OutIndiceType">
    ''' Output format of the indices.
    ''' </typeparam>
    ''' <param name="P">
    ''' The array to which indices are to be copied.
    ''' </param>
    ''' <param name="_Converter">
    ''' The converter to convert indices from <c>IndiceType</c> to
    ''' <c>OutIndiceType</c>.
    ''' </param>
    ''' <param name="SourceIndex">
    ''' The index from which indices are copied from source array.
    ''' </param>
    ''' <param name="DestinationIndex">
    ''' The index from which indices are copied to destination array.
    ''' </param>
    ''' <param name="Length">
    ''' The number of indices to copy.
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
    Public Function CopyTo (Of OutIndiceType As {Structure, IConvertible}) _
        (ByVal P() As OutIndiceType,
         Optional ByVal SourceIndex As Integer = 0,
         Optional ByVal DestinationIndex As Integer = 0,
         Optional ByVal Length As Integer = - 1,
         Optional ByVal _Converter As System.Converter(Of IndiceType, OutIndiceType) = Nothing) As Boolean

        ' Check inputs.
        ' -------------
        ' Check if we have indices.
        If (m_Indices.Count = 0) Then _
            Return False

        ' Check input array.
        If P Is Nothing Then _
            Throw New ArgumentNullException("P") _
                : Exit Function

        ' Check input array.
        If P.Rank <> 1 Then _
            Throw New RankException("'P.Rank <> 1'") _
                : Exit Function

        ' Check bounds.
        If (SourceIndex < 0) OrElse (SourceIndex >= m_Indices.Count) Then _
            Throw New ArgumentOutOfRangeException("SourceIndex") _
                : Exit Function

        ' Check bounds.
        If (DestinationIndex < 0) OrElse (DestinationIndex > UBound(P)) Then _
            Throw New ArgumentOutOfRangeException("DestinationIndex") _
                : Exit Function

        ' Check bounds.
        If (Length < - 1) OrElse (Length = 0) Then _
            Throw New ArgumentOutOfRangeException("Length") _
                : Exit Function

        ' Set the proper element count if not given.
        If Length = - 1 Then _
            Length = Math.Min(m_Indices.Count - SourceIndex, P.Length - DestinationIndex)

        ' Check bounds.
        If (Length > m_Indices.Count - SourceIndex) OrElse (Length > P.Length - DestinationIndex) Then _
            Throw New ArgumentOutOfRangeException("Length") _
                : Exit Function

        ' Use the default converter if there isn't any.
        If _Converter Is Nothing Then _
            _Converter = AddressOf Utility.IndiceConverter

        ' Do we need to convert the indices?
        ' ----------------------------------
        If GetType(OutIndiceType) Is GetType(IndiceType) Then
            ' Copy directly.
            ' --------------
            m_Indices.CopyTo(SourceIndex,
                             CType(CObj(P), IndiceType()),
                             DestinationIndex,
                             Length)

        Else ' If GetType(OutIndiceType) Is GetType(IndiceType) Then
            ' Convert and copy indices.
            ' -------------------------
            For I As Integer = 0 To Length - 1
                P(DestinationIndex + I) = _Converter(m_Indices(SourceIndex + I))

            Next I ' For I As Integer = 0 To Length - 1

        End If ' If GetType(OutIndiceType) Is GetType(IndiceType) Then

        Return True
    End Function

    ''' <summary>
    ''' Copies the primitive group to another primitive group.
    ''' </summary>
    ''' <param name="obj">
    ''' The primitive group to which this primitive group is to be copied.
    ''' </param>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>obj Is Nothing</c>.
    ''' </exception>
    ''' <remarks>
    ''' Overwrites the input object.
    ''' </remarks>
    Public Function CopyTo (Of OutIndiceType As {Structure, IConvertible}) _
        (ByVal obj As GPrimitiveGroup(Of OutIndiceType),
         Optional ByVal _Converter As System.Converter(Of IndiceType, OutIndiceType) = Nothing) As Boolean

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

        ' Empty, set the type, and copy.
        ' ------------------------------
        obj.m_Indices.Clear()
        obj.m_Type = m_Type

        ' Use the default converter if there isn't any.
        If _Converter Is Nothing Then _
            _Converter = AddressOf Utility.IndiceConverter

        If GetType(IndiceType) Is GetType(OutIndiceType) Then _
            obj.m_Indices.AddRange(CType(CObj(m_Indices), List(Of OutIndiceType))) _
            Else _
            obj.m_Indices = m_Indices.ConvertAll(_Converter)

        Return True
    End Function

    ''' <summary>
    ''' Removes indices.
    ''' </summary>
    ''' <param name="Index">
    ''' The index of the first indice to be removed.
    ''' </param>
    ''' <param name="RemoveCount">
    ''' Number of indices to be removed.
    ''' </param>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>Index</c> is out of bounds.
    ''' Also thrown when <c>RemoveCount</c> is either negative,
    ''' zero, or larger than the count of indices that may be removed.
    ''' </exception>
    ''' <remarks>
    ''' Note that this does nothing if array is empty.
    ''' </remarks>
    Public Function Remove(ByVal Index As Integer, Optional ByVal RemoveCount As Integer = 1) As Boolean
        ' Check inputs.
        ' -------------
        ' Check if empty.
        If m_Indices.Count = 0 Then _
            Return False

        ' Check the index of first element to be removed.
        If (Index < 0) OrElse (Index >= m_Indices.Count) Then _
            Throw New ArgumentOutOfRangeException("Index") _
                : Exit Function

        ' Check the count of elements to be removed.
        If (RemoveCount <= 0) OrElse (RemoveCount > m_Indices.Count) Then _
            Throw New ArgumentOutOfRangeException("RemoveCount") _
                : Exit Function

        ' Check whether the index of the last element to be removed is in bounds or not.
        If Index + RemoveCount > m_Indices.Count Then _
            Throw New ArgumentOutOfRangeException("RemoveCount") _
                : Exit Function

        ' Remove the indices.
        ' -------------------
        m_Indices.RemoveRange(Index, RemoveCount)

        Return True
    End Function

    ''' <summary>
    ''' Removes indices.
    ''' </summary>
    ''' <param name="StartIndex">
    ''' The index of the first indice (inclusive) which is removed.
    ''' </param>
    ''' <param name="EndIndex">
    ''' The index of the last indice (inclusive) which is removed.
    ''' </param>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>StartIndex</c> or <c>EndIndex</c> is out of bounds.
    ''' </exception>
    Public Function RemoveRange(ByVal StartIndex As Integer, ByVal EndIndex As Integer) As Boolean
        ' Check inputs.
        ' -------------
        ' Check if empty.
        If m_Indices.Count = 0 Then _
            Return False

        ' Check bounds.
        If (StartIndex < 0) OrElse (StartIndex >= m_Indices.Count) Then _
            Throw New ArgumentOutOfRangeException("StartIndex") _
                : Exit Function

        ' Check bounds.
        If (EndIndex < 0) OrElse (EndIndex >= m_Indices.Count) Then _
            Throw New ArgumentOutOfRangeException("EndIndex") _
                : Exit Function

        ' Check bounds.
        If (StartIndex > EndIndex) Then _
            Throw New ArgumentOutOfRangeException("StartIndex' or 'EndIndex'") _
                : Exit Function

        ' Remove the indices.
        ' -------------------
        m_Indices.RemoveRange(StartIndex, EndIndex - StartIndex + 1)

        Return True
    End Function

    ''' <summary>
    ''' Removes all primitives in the primitive group.
    ''' </summary>
    Public Function RemoveAll() As Boolean
        m_Indices.Clear()

        Return True
    End Function

    ''' <summary>
    ''' Clones this primitive group and returns it.
    ''' </summary>
    ''' <remarks>
    ''' Copy is a deep copy.
    ''' </remarks>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return New GPrimitiveGroup(Of IndiceType)(Me)
    End Function

    ''' <summary>
    ''' Converts the primitives in "Strip" or "Fan" format (for both lines and triangles)
    ''' into "List" format.
    ''' </summary>
    ''' <remarks>
    ''' This modifies the existing primitive group.
    ''' </remarks>
    Public Function ConvertToList() As Boolean
        Dim I, J As Integer
        Dim NewIndices() As IndiceType

        ' Check if there are any indices.
        If m_Indices.Count = 0 Then _
            Return True

        ' Check if it is already in a list.
        If (m_Type = PrimitiveType.TriangleList) OrElse
           (m_Type = PrimitiveType.LineList) OrElse
           (m_Type = PrimitiveType.PointList) Then _
            Return True

        Validate()

        ' OK, now convert to list.
        Select Case m_Type
            ' LINE STRIP
            Case PrimitiveType.LineStrip
                ReDim NewIndices(2*(m_Indices.Count - 1) - 1)

                For I = 0 To m_Indices.Count - 2
                    NewIndices(J) = m_Indices(I)
                    NewIndices(J + 1) = m_Indices(I + 1)

                    ' Increment 'NewIndice' index.
                    J = J + 2
                Next I ' For I = 0 To m_Indices.Count - 2

                ' Set new type.
                m_Type = PrimitiveType.LineList

                ' TRIANGLE STRIP
            Case PrimitiveType.TriangleStrip
                Dim Reverse As Boolean

                ReDim NewIndices(3*(m_Indices.Count - 2) - 1)

                For I = 0 To m_Indices.Count - 3
                    If Reverse Then
                        NewIndices(J + 2) = m_Indices(I)
                        NewIndices(J + 1) = m_Indices(I + 1)
                        NewIndices(J) = m_Indices(I + 2)

                    Else ' If Reverse Then
                        NewIndices(J) = m_Indices(I)
                        NewIndices(J + 1) = m_Indices(I + 1)
                        NewIndices(J + 2) = m_Indices(I + 2)

                    End If ' If Reverse Then

                    ' Increment 'NewIndice' index.
                    J = J + 3

                    ' Set the reverse flag.
                    Reverse = Not Reverse
                Next I ' For I = 0 To m_Indices.Count - 3

                ' Set new type.
                m_Type = PrimitiveType.TriangleList

                ' TRIANGLE FAN
            Case PrimitiveType.TriangleFan
                ReDim NewIndices(3*(m_Indices.Count - 2) - 1)

                For I = 1 To m_Indices.Count - 2
                    NewIndices(J) = m_Indices(0)
                    NewIndices(J + 1) = m_Indices(I)
                    NewIndices(J + 2) = m_Indices(I + 1)

                    ' Increment 'NewIndice' index.
                    J = J + 3
                Next I ' For I = 1 To m_Indices.Count - 2

                ' Set new type.
                m_Type = PrimitiveType.TriangleList

                ' UNKNOWN (error)
            Case Else
                Throw New MeshPrimitiveTypeException("Primitive type not defined.")
                Exit Function

        End Select ' Select Case m_Type

        ' Remove old indices.
        m_Indices.Clear()

        ' Set new indices.
        m_Indices.AddRange(NewIndices)

        Return True
    End Function

    ''' <summary>
    ''' Reverses the order of all triangles.
    ''' </summary>
    ''' <remarks>
    ''' The primitive group is converted to "list" type first. Also,
    ''' this has no effect on point and line primitives.
    ''' </remarks>
    Public Function ReverseFaceOrder() As Boolean
        ' Check type of primitive group.
        If (m_Type = PrimitiveType.PointList) OrElse
           (m_Type = PrimitiveType.LineList) OrElse
           (m_Type = PrimitiveType.LineStrip) Then _
            Return False

        ' First convert to list if needed.
        If m_Type <> PrimitiveType.TriangleList Then _
            ConvertToList()

        ' Now reverse indices for each primitive.
        For I As Integer = 0 To m_Indices.Count - 1 Step 3
            Dim Ind As IndiceType = m_Indices(I + 2)

            ' Swap the indices for I and I + 2.
            m_Indices(I + 2) = m_Indices(I)
            m_Indices(I) = Ind

        Next I ' For I As Integer = 0 To m_Indices.Count - 1 Step 3

        Return True
    End Function

    ''' <summary>
    ''' Shifts all indices in the primitive group by the specified amount.
    ''' </summary>
    ''' <param name="IndiceShift">
    ''' The value by which indices are shifted (i.e. this number is added).
    ''' </param>
    Public Function ShiftIndices(ByVal IndiceShift As IndiceType) As Boolean
        Dim _IndiceShift As Arithmetic(Of IndiceType) = IndiceShift

        For I As Integer = 0 To m_Indices.Count - 1
            ' Perform shift.
            m_Indices(I) += _IndiceShift

        Next I ' For I As Integer = 0 To m_Indices.Count - 1

        Return True
    End Function

    ''' <summary>
    ''' Checks for any sort of errors.
    ''' </summary>
    ''' <exception cref="MeshPrimitiveTypeException">
    ''' Thrown when: 
    ''' Type is not set; 
    ''' Type is <c>LineList</c>, but <c>Count</c> is non-multiple of 2; 
    ''' Type is <c>LineStrip</c>, but <c>Count</c> is less than 2; 
    ''' Type is <c>TriangleList</c>, but <c>Count</c> is non-multiple of 3; 
    ''' Type is <c>TriangleStrip</c>, but <c>Count</c> is less than 3; 
    ''' Type is <c>TriangleFan</c>, but <c>Count</c> is less than 3; 
    ''' </exception>
    ''' <remarks>
    ''' It is not possible to check the bounds of indices i.e. whether they
    ''' refer to a valid vertex or not. Hence this must be done seperately
    ''' if desired.
    ''' </remarks>
    Friend Function Validate() As Boolean
        ' Are there any indices?
        If m_Indices.Count = 0 Then _
            Return True

        Select Case m_Type
            ' POINT LIST
            Case PrimitiveType.PointList
                ' Nothing done here.

                ' LINE LIST
            Case PrimitiveType.LineList
                ' Check indice count.
                If m_Indices.Count Mod 2 <> 0 Then _
                    Throw New MeshPrimitiveTypeException("Indice count not a multiple of 2 for a 'LineList'.") _
                        : Exit Function

                ' LINE STRIP
            Case PrimitiveType.LineStrip
                ' Check indice count.
                If m_Indices.Count < 2 Then _
                    Throw New MeshPrimitiveTypeException("Not enough indices for a 'LineStrip'.") _
                        : Exit Function

                ' TRIANGLE LIST1
            Case PrimitiveType.TriangleList
                ' Check indice count.
                If m_Indices.Count Mod 3 <> 0 Then _
                    Throw New MeshPrimitiveTypeException("Indice count not a multiple of 3 for a 'TriangleList'.") _
                        : Exit Function

                ' TRIANGLE STRIP
            Case PrimitiveType.TriangleStrip
                ' Check indice count.
                If m_Indices.Count < 3 Then _
                    Throw New MeshPrimitiveTypeException("Not enough indices for a 'TriangleStrip'.") _
                        : Exit Function

                ' TRIANGLE FAN
            Case PrimitiveType.TriangleFan
                ' Check indice count.
                If m_Indices.Count < 3 Then _
                    Throw New MeshPrimitiveTypeException("Not enough indices for a 'TriangleFan'.") _
                        : Exit Function

                ' UNKNOWN (error)
            Case Else
                ' Invalid primtive type.
                Throw New MeshPrimitiveTypeException("Primitive type not defined.")
                Exit Function

        End Select ' Select Case m_Type

        Return True
    End Function

    ''' <summary>
    ''' Checks for any sort of errors.
    ''' </summary>
    ''' <param name="MinIndiceValue">
    ''' Minimum value that an indice may have (including this value).
    ''' </param>
    ''' <param name="MaxIndiceValue">
    ''' Maximum value that an indice may have (including this value).
    ''' </param>
    ''' <exception cref="Exception">
    ''' Thrown when:
    ''' <c>Validate()</c> throws an exception.
    ''' Indices are out of given range.
    ''' </exception>
    Friend Function Validate(ByVal MinIndiceValue As IndiceType, ByVal MaxIndiceValue As IndiceType) As Boolean
        Dim Min As Arithmetic(Of IndiceType) = MinIndiceValue
        Dim Max As Arithmetic(Of IndiceType) = MaxIndiceValue

        ' Perform default validation first.
        Validate()

        ' Now check each and every indice.
        If Not m_Indices.TrueForAll(Function(V As IndiceType) ((V >= Min) AndAlso V <= Max)) Then _
            Throw New Exception("One or more indices is out of given bounds.") _
                : Exit Function

        Return True
    End Function

    ''' <summary>
    ''' Returns the indice data array.
    ''' </summary>
    Friend Function GetData() As Object
        Static Indices() As IndiceType = Nothing

        If Indices IsNot Nothing Then _
            Indices = Nothing

        Indices = m_Indices.ToArray()

        Return Indices
    End Function

    ''' <summary>
    ''' Writes the indice data to the buffer.
    ''' </summary>
    ''' <param name="IB">
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
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>IB Is Nothing</c>.
    ''' </exception>
    Friend Function WriteDataToBuffer(ByVal IB As IndexBuffer, ByVal LockAtOffsetInBytes As Integer,
                                      ByVal _LockFlags As LockFlags) As Integer
        Dim IndSize As Integer

        ' Is there data to write?
        If m_Indices.Count = 0 Then _
            Return 0

        ' Check inputs.
        If IB Is Nothing Then _
            Throw New ArgumentNullException("IB") _
                : Exit Function

        ' Get the indice size.
        If IB.Description.Is16BitIndices Then _
            IndSize = 2 _
            Else _
            IndSize = 4

        ' Do we need to upsize\downsize the indices?
        If Len(m_Indices(0)) = IndSize Then
            ' Same size.
            Dim Ind() As IndiceType

            ' Get array.
            Ind = m_Indices.ToArray()

            ' Set the data.
            IB.SetData(Ind, LockAtOffsetInBytes, _LockFlags)

            ' Remove the temporary array.
            Ind = Nothing

            Return m_Indices.Count*IndSize

        Else ' If Len(m_Indices(0)) <= MaxIndSize Then
            ' Buffer uses different size as compared to internal structures.
            ' Need to resize before copying.
            If IndSize = 2 Then _
                Return WriteDataToBuffer (Of UShort)(IB, LockAtOffsetInBytes, _LockFlags) _
                Else _
                Return WriteDataToBuffer (Of UInteger)(IB, LockAtOffsetInBytes, _LockFlags)

        End If ' If Len(m_Indices(0)) <= MaxIndSize Then
    End Function

    ''' <summary>
    ''' Writes the indice data to the buffer.
    ''' </summary>
    ''' <typeparam name="OutIndiceType">
    ''' Output format of the indices.
    ''' </typeparam>
    ''' <param name="IB">
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
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>IB Is Nothing</c>.
    ''' </exception>
    Private Function WriteDataToBuffer (Of OutIndiceType As {Structure, IConvertible}) _
        (ByVal IB As IndexBuffer,
         ByVal LockAtOffsetInBytes As Integer,
         ByVal _LockFlags As LockFlags) As Integer

        Dim Ind() As OutIndiceType, OutIndSize As Integer

        ' Is there data to write?
        If m_Indices.Count = 0 Then _
            Return 0

        ' Check inputs.
        If IB Is Nothing Then _
            Throw New ArgumentNullException("'IB Is Nothing'") _
                : Exit Function

        ' Allocate space for the indices.
        ReDim Ind(m_Indices.Count - 1)

        ' Get the size of one indice.
        OutIndSize = Len(Ind(0))

        ' Get the indices.
        CopyTo(Ind)

        ' Set the data.
        IB.SetData(Ind, LockAtOffsetInBytes, _LockFlags)

        ' Remove the temporary array.
        Ind = Nothing

        ' Return length.
        Return m_Indices.Count*OutIndSize
    End Function
End Class
