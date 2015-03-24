Imports GenericMesh.Exceptions

''' <summary>
''' The mesh class which can store 2D\3D objects.
''' </summary>
''' <typeparam name="VertexType">
''' Vertex format of the mesh.
''' </typeparam>
''' <typeparam name="IndiceType">
''' Indice format of the mesh.
''' </typeparam>
''' <typeparam name="MaterialType">
''' Type of material used by the mesh.
''' </typeparam>
Public Class GBasicMesh (Of VertexType As {Structure, IVertex},
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
    ''' The parts of this mesh.
    ''' </summary>
    Private m_Parts As List(Of GMeshPart(Of VertexType, IndiceType, MaterialType))

    ''' <summary>
    ''' Whether or not the mesh is locked.
    ''' A mesh is locked so as to make it renderable
    ''' using buffers.
    ''' </summary>
    Private m_Locked As Boolean

    ''' <summary>
    ''' Whether or not the mesh uses internal
    ''' buffers.
    ''' </summary>
    Private m_InternalBuffers As Boolean

    ''' <summary>
    ''' Vertex buffer, containing the vertices.
    ''' </summary>
    Private WithEvents m_VB As VertexBuffer

    ''' <summary>
    ''' Base vertex offset. Used only for 
    ''' external buffers.
    ''' </summary>
    Private m_BaseVertexOffset As Integer

    ''' <summary>
    ''' Indice buffer, containing the indices.
    ''' </summary>
    Private WithEvents m_IB As IndexBuffer

    ''' <summary>
    ''' Base indice offset. Used only for 
    ''' external buffers.
    ''' </summary>
    Private m_BaseIndiceOffset As Integer

    ' ------------------------
    ' Constructors\Finalizers.
    ' ------------------------
    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    Public Sub New()
        ' Initialize a new list.
        m_Parts = New List(Of GMeshPart(Of VertexType, IndiceType, MaterialType))
    End Sub

    ''' <summary>
    ''' Copy constructor.
    ''' </summary>
    ''' <param name="obj">
    ''' The object whose copy is to be made.
    ''' </param>
    ''' <remarks>
    ''' This is equivalent to a normal constructor if <c>obj Is Nothing</c>.
    ''' Also, the new mesh is not locked, even if the input is.
    ''' </remarks>
    Public Sub New(ByVal obj As GBasicMesh(Of VertexType, IndiceType, MaterialType))
        ' Call default constructor.
        Me.New()

        ' Now copy the mesh if present.
        If obj IsNot Nothing Then _
            obj.CopyTo(Me)
    End Sub

    ''' <summary>
    ''' Class destructor.
    ''' </summary>
    Protected Overrides Sub Finalize()
        ' Free the buffers if allocated.
        Unlock()

        ' Free list.
        m_Parts = Nothing
    End Sub

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns\Sets the mesh part count.
    ''' </summary>
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    Public Property PartCount() As Integer
        Get
            Return m_Parts.Count
        End Get

        Set(ByVal value As Integer)
            Dim OldSize As Integer = m_Parts.Count

            ' Is the mesh locked?
            If m_Locked Then _
                Throw New MeshLockedException _
                    : Exit Property

            ' Verify new size.
            If value < 0 Then _
                Throw New ArgumentOutOfRangeException("value") _
                    : Exit Property

            ' Free the extra elements.
            If value < OldSize Then _
                m_Parts.RemoveRange(value, OldSize - value)

            ' Allocate new elements.
            For I As Integer = OldSize To value - 1
                m_Parts.Add(New GMeshPart(Of VertexType, IndiceType, MaterialType))

            Next I ' For I As Integer = OldSize To value - 1
        End Set
    End Property

    ''' <summary>
    ''' Returns the specified mesh part.
    ''' </summary>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>Index</c> is out of bounds.
    ''' </exception>
    ''' <remarks>
    ''' This throws no exception when the mesh is locked.
    ''' However, it returns a <c>Nothing</c> in that case.
    ''' </remarks>
    Public ReadOnly Property Part(ByVal Index As Integer) As GMeshPart(Of VertexType, IndiceType, MaterialType)
        Get
            ' Do not return the part if the mesh is locked.
            If m_Locked = True Then _
                Return Nothing

            ' Check index.
            If (Index < 0) OrElse (Index >= m_Parts.Count) Then _
                Throw New ArgumentOutOfRangeException("Index") _
                    : Exit Property

            Return m_Parts(Index)
        End Get
    End Property

    ''' <summary>
    ''' Returns\Sets the specified mesh part's material.
    ''' </summary>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>Index</c> is out of bounds.
    ''' </exception>
    ''' <remarks>
    ''' This is to be used when the mesh is locked, but material(s)
    ''' are to be edited.
    ''' </remarks>
    Public Property Material(ByVal Index As Integer) As MaterialType
        Get
            ' Check index.
            If (Index < 0) OrElse (Index >= m_Parts.Count) Then _
                Throw New ArgumentOutOfRangeException("Index") _
                    : Exit Property

            ' Return the material.
            Return m_Parts(Index).Material
        End Get

        Set(ByVal value As MaterialType)
            ' Check index.
            If (Index < 0) OrElse (Index >= m_Parts.Count) Then _
                Throw New ArgumentOutOfRangeException("Index") _
                    : Exit Property

            ' Set the material.
            m_Parts(Index).Material = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the total number of vertices in the mesh.
    ''' </summary>
    Public ReadOnly Property TotalVertexCount() As Integer
        Get
            Return m_Parts.Aggregate(
                0,
                Function(TVC As Integer, P As GMeshPart(Of VertexType, IndiceType, MaterialType)) _
                                        (TVC + P.Vertices.Count)
                )
        End Get
    End Property

    ''' <summary>
    ''' Returns the total number of indices in the mesh.
    ''' </summary>
    Public ReadOnly Property TotalIndiceCount() As Integer
        Get
            Return m_Parts.Aggregate(
                0,
                Function(TIC As Integer, P As GMeshPart(Of VertexType, IndiceType, MaterialType)) _
                                        (TIC + P.TotalIndiceCount)
                )
        End Get
    End Property

    ''' <summary>
    ''' Returns whether the mesh is locked or not.
    ''' </summary>
    ''' <remarks>
    ''' The mesh must be locked in order to render it via buffers.
    ''' The mesh must be unlocked in order to edit it.
    ''' </remarks>
    Public ReadOnly Property Locked() As Boolean
        Get
            Return m_Locked
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
    ''' Adds a mesh part to the mesh.
    ''' </summary>
    ''' <typeparam name="InVertexType">
    ''' Vertex type of input mesh.
    ''' </typeparam>
    ''' <typeparam name="InIndiceType">
    ''' Indice type of input mesh.
    ''' </typeparam>
    ''' <typeparam name="InMaterialType">
    ''' Material type of input mesh.
    ''' </typeparam>
    ''' <param name="P">
    ''' The mesh part to add.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter from input mesh format to
    ''' format of this instance.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter from input mesh format to
    ''' format of this instance.
    ''' </param>
    ''' <param name="_MC">
    ''' Material converter from input mesh format to
    ''' format of this instance.
    ''' </param>
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    ''' <remarks>
    ''' This does nothing if <c>P Is Nothing</c>.
    ''' </remarks>
    Public Function Add (Of InVertexType As {Structure, IVertex}, _
        InIndiceType As {Structure, IConvertible}, _
        InMaterialType As {Structure, IMaterial}) _
        (ByVal P As GMeshPart(Of InVertexType, InIndiceType, InMaterialType),
         Optional ByVal _VC As Converter(Of InVertexType, VertexType) = Nothing,
         Optional ByVal _IC As Converter(Of InIndiceType, IndiceType) = Nothing,
         Optional ByVal _MC As Converter(Of InMaterialType, MaterialType) = Nothing) As Boolean

        Dim _P As GMeshPart(Of VertexType, IndiceType, MaterialType)

        ' Do we have an object?
        If P Is Nothing Then _
            Return False

        ' Is the mesh locked?
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Create a temporary part.
        _P = New GMeshPart(Of VertexType, IndiceType, MaterialType)

        ' Convert the mesh part.
        P.CopyTo(_P, _VC, _IC, _MC)

        ' Add it.
        m_Parts.Add(_P)

        ' Remove the temporary part.
        _P = Nothing

        Return True
    End Function

    ''' <summary>
    ''' Adds parts to this mesh.
    ''' </summary>
    ''' <param name="_Count">
    ''' The number of parts to add.
    ''' </param>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>_Count</c> is negative.
    ''' </exception>
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    ''' <remarks>
    ''' This does nothing when <c>_Count = 0</c>.
    ''' </remarks>
    Public Function Add(ByVal _Count As Integer) As Boolean
        ' Do we have anything to do?
        If _Count = 0 Then _
            Return False

        ' Is the mesh locked?
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Check for valid inputs.
        If _Count < 0 Then _
            Throw New ArgumentOutOfRangeException("_Count") _
                : Exit Function

        ' Set the new part count.
        PartCount += _Count

        Return True
    End Function

    ''' <summary>
    ''' Appends the input mesh to this mesh.
    ''' </summary>
    ''' <typeparam name="InVertexType">
    ''' Vertex type of input mesh.
    ''' </typeparam>
    ''' <typeparam name="InIndiceType">
    ''' Indice type of input mesh.
    ''' </typeparam>
    ''' <typeparam name="InMaterialType">
    ''' Material type of input mesh.
    ''' </typeparam>
    ''' <param name="obj">
    ''' The mesh to add to this mesh.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter from input mesh format to
    ''' format of this instance.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter from input mesh format to
    ''' format of this instance.
    ''' </param>
    ''' <param name="_MC">
    ''' Material converter from input mesh format to
    ''' format of this instance.
    ''' </param>
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    ''' <remarks>
    ''' This does nothing if <c>obj Is Nothing</c>.
    ''' </remarks>
    Public Function Append (Of InVertexType As {Structure, IVertex}, _
        InIndiceType As {Structure, IConvertible}, _
        InMaterialType As {Structure, IMaterial}) _
        (ByVal obj As GBasicMesh(Of InVertexType, InIndiceType, InMaterialType),
         Optional ByVal _VC As System.Converter(Of InVertexType, VertexType) = Nothing,
         Optional ByVal _IC As System.Converter(Of InIndiceType, IndiceType) = Nothing,
         Optional ByVal _MC As System.Converter(Of InMaterialType, MaterialType) = Nothing) As Boolean

        ' Check whether we have valid input elements or not.
        If (obj Is Nothing) OrElse (obj.m_Parts.Count = 0) Then _
            Return False

        ' Is the mesh locked?
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Verify argument.
        If obj Is Me Then _
            Throw New Exception("'obj is Me'") _
                : Exit Function

        ' Add the parts of the input mesh.
        obj.m_Parts.ForEach(
            Function(V As GMeshPart(Of InVertexType, InIndiceType, InMaterialType)) _
                               (Add(V, _VC, _IC, _MC))
            )

        Return True
    End Function

    ''' <summary>
    ''' Copies this mesh to another mesh.
    ''' </summary>
    ''' <typeparam name="OutVertexType">
    ''' Vertex type of input mesh.
    ''' </typeparam>
    ''' <typeparam name="OutIndiceType">
    ''' Indice type of input mesh.
    ''' </typeparam>
    ''' <typeparam name="OutMaterialType">
    ''' Material type of input mesh.
    ''' </typeparam>
    ''' <param name="obj">
    ''' The mesh to which this mesh is to be copied.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter from input mesh format to
    ''' format of this instance.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter from input mesh format to
    ''' format of this instance.
    ''' </param>
    ''' <param name="_MC">
    ''' Material converter from input mesh format to
    ''' format of this instance.
    ''' </param>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>obj Is Nothing</c>
    ''' </exception>
    ''' <exception cref="MeshLockedException">
    ''' Thrown when <c>obj</c> is locked.
    ''' </exception>
    Public Function CopyTo (Of OutVertexType As {Structure, IVertex}, _
        OutIndiceType As {Structure, IConvertible}, _
        OutMaterialType As {Structure, IMaterial}) _
        (ByVal obj As GBasicMesh(Of OutVertexType, OutIndiceType, OutMaterialType),
         Optional ByVal _VC As System.Converter(Of VertexType, OutVertexType) = Nothing,
         Optional ByVal _IC As System.Converter(Of IndiceType, OutIndiceType) = Nothing,
         Optional ByVal _MC As System.Converter(Of MaterialType, OutMaterialType) = Nothing) As Boolean

        ' Check input object.
        If obj Is Nothing Then _
            Throw New ArgumentNullException("obj") _
                : Exit Function

        ' Check input object.
        If obj Is Me Then _
            Throw New Exception("'obj Is Me'") _
                : Exit Function

        ' Check if the mesh is locked.
        If obj.m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Empty, and append.
        obj.RemoveAll()
        obj.Append(Me, _VC, _IC, _MC)

        Return True
    End Function

    ''' <summary>
    ''' Removes a part from the mesh.
    ''' </summary>
    ''' <param name="Index">
    ''' Index of the part to be removed.
    ''' </param>
    ''' <param name="RemoveCount">
    ''' Number of parts to be removed.
    ''' </param>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>Index</c> is out of bounds.
    ''' Also thrown when <c>RemoveCount</c> is either negative,
    ''' zero, or larger than the count of elements that may be removed.
    ''' </exception>
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    Public Function Remove(ByVal Index As Integer, Optional ByVal RemoveCount As Integer = 1) As Boolean
        ' Check inputs.
        ' -------------
        ' Check if empty.
        If m_Parts.Count = 0 Then _
            Return False

        ' Is the mesh locked?
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Check the index of first element to be removed.
        If (Index < 0) OrElse (Index >= m_Parts.Count) Then _
            Throw New ArgumentOutOfRangeException("Index") _
                : Exit Function

        ' Check the count of elements to be removed.
        If (RemoveCount <= 0) OrElse (RemoveCount > m_Parts.Count) Then _
            Throw New ArgumentOutOfRangeException("RemoveCount") _
                : Exit Function

        ' Check whether the index of the last element to be removed is in bounds or not.
        If Index + RemoveCount > m_Parts.Count Then _
            Throw New ArgumentOutOfRangeException("RemoveCount") _
                : Exit Function

        ' Remove the parts.
        ' -----------------
        m_Parts.RemoveRange(Index, RemoveCount)

        Return True
    End Function

    ''' <summary>
    ''' Removes parts from the mesh.
    ''' </summary>
    ''' <param name="StartIndex">
    ''' The index of the first part which is removed.
    ''' </param>
    ''' <param name="EndIndex">
    ''' The index of the last part which is removed.
    ''' </param>
    ''' <remarks>
    ''' Parts in the range [StartIndex, EndIndex] (i.e. both limits included)
    ''' are removed.
    ''' </remarks>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>StartIndex</c> or <c>EndIndex</c> is out of bounds.
    ''' </exception>
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    Public Function RemoveRange(ByVal StartIndex As Integer, ByVal EndIndex As Integer) As Boolean
        ' Check inputs.
        ' -------------
        ' Check if empty.
        If m_Parts.Count = 0 Then _
            Return False

        ' Is the mesh locked?
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Check bounds.
        If (StartIndex < 0) OrElse (StartIndex >= m_Parts.Count) Then _
            Throw New ArgumentOutOfRangeException("StartIndex") _
                : Exit Function

        ' Check bounds.
        If (EndIndex < 0) OrElse (EndIndex >= m_Parts.Count) Then _
            Throw New ArgumentOutOfRangeException("EndIndex") _
                : Exit Function

        ' Check bounds.
        If (StartIndex > EndIndex) Then _
            Throw New ArgumentOutOfRangeException("StartIndex' or 'EndIndex") _
                : Exit Function

        ' Remove the parts.
        ' -----------------
        m_Parts.RemoveRange(StartIndex, EndIndex - StartIndex + 1)

        Return True
    End Function

    ''' <summary>
    ''' Removes all parts of the mesh.
    ''' </summary>
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    Public Function RemoveAll() As Boolean
        ' Check if the mesh is locked.
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Remove all the parts.
        m_Parts.Clear()

        Return True
    End Function

    ''' <summary>
    ''' Clones this primitive group and returns it.
    ''' </summary>
    ''' <remarks>
    ''' Copy is a deep copy. Note that the copy is not
    ''' locked, even if this instance is.
    ''' </remarks>
    Public Overridable Function Clone() As Object Implements System.ICloneable.Clone
        Return New GBasicMesh(Of VertexType, IndiceType, MaterialType)(Me)
    End Function

    ''' <summary>
    ''' Removes all parts from the mesh.
    ''' </summary>
    ''' <remarks>
    ''' This removes internal buffers, if the mesh is locked.
    ''' </remarks>
    Public Function Free() As Boolean
        ' Remove buffers if mesh is locked and unlock it.
        If m_Locked Then _
            Unlock()

        ' Remove all parts.
        Return RemoveAll()
    End Function

    ''' <summary>
    ''' Transforms all vertices in the mesh.
    ''' </summary>
    ''' <param name="M">
    ''' Matrix to be used for the transformer.
    ''' </param>
    ''' <param name="_VertexTransformer">
    ''' Function to be used for performing transformation.
    ''' </param>
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    Public Function Transform(ByVal M As Matrix,
                              Optional ByVal _VertexTransformer As Func(Of VertexType, Matrix, VertexType) = Nothing) _
        As Boolean

        ' Is the mesh locked?
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Do we have anything to do?
        If m_Parts.Count = 0 Then _
            Return False

        ' Call for all parts.
        m_Parts.ForEach(
            Function(V As GMeshPart(Of VertexType, IndiceType, MaterialType)) _
                           (V.Transform(M, _VertexTransformer))
            )

        Return True
    End Function

    ''' <summary>
    ''' Transforms all vertices in the mesh part.
    ''' </summary>
    ''' <param name="_VertexTransformer">
    ''' Function to be used for performing transformation.
    ''' </param>
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    Public Function Translation(ByVal X As Single, ByVal Y As Single, ByVal Z As Single,
                                Optional ByVal _VertexTransformer As Func(Of VertexType, Matrix, VertexType) = Nothing) _
        As Boolean

        Return Translation(New Vector3(X, Y, Z))
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
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    Public Function Translation(ByVal V As Vector3,
                                Optional ByVal _VertexTransformer As Func(Of VertexType, Matrix, VertexType) = Nothing) _
        As Boolean

        ' Is the mesh locked?
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Do we have anything to do?
        If m_Parts.Count = 0 Then _
            Return False

        ' Call for all parts.
        m_Parts.ForEach(
            Function(_V As GMeshPart(Of VertexType, IndiceType, MaterialType)) _
                           (_V.Translation(V, _VertexTransformer))
            )

        Return True
    End Function

    ''' <summary>
    ''' Transforms all vertices in the mesh part.
    ''' </summary>
    ''' <param name="_VertexTransformer">
    ''' Function to be used for performing transformation.
    ''' </param>
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    Public Function Rotation(ByVal X As Single, ByVal Y As Single, ByVal Z As Single,
                             Optional ByVal _VertexTransformer As Func(Of VertexType, Matrix, VertexType) = Nothing) _
        As Boolean

        Return Rotation(New Vector3(X, Y, Z))
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
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    Public Function Rotation(ByVal V As Vector3,
                             Optional ByVal _VertexTransformer As Func(Of VertexType, Matrix, VertexType) = Nothing) _
        As Boolean

        ' Is the mesh locked?
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Do we have anything to do?
        If m_Parts.Count = 0 Then _
            Return False

        ' Call for all parts.
        m_Parts.ForEach(
            Function(_V As GMeshPart(Of VertexType, IndiceType, MaterialType)) _
                           (_V.Rotation(V, _VertexTransformer))
            )

        Return True
    End Function

    ''' <summary>
    ''' Transforms all vertices in the mesh part.
    ''' </summary>
    ''' <param name="_VertexTransformer">
    ''' Function to be used for performing transformation.
    ''' </param>
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    Public Function Scaling(ByVal X As Single, ByVal Y As Single, ByVal Z As Single,
                            Optional ByVal _VertexTransformer As Func(Of VertexType, Matrix, VertexType) = Nothing) _
        As Boolean

        Return Scaling(New Vector3(X, Y, Z))
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
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    Public Function Scaling(ByVal V As Vector3,
                            Optional ByVal _VertexTransformer As Func(Of VertexType, Matrix, VertexType) = Nothing) _
        As Boolean

        ' Is the mesh locked?
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Do we have anything to do?
        If m_Parts.Count = 0 Then _
            Return False

        ' Call for all parts.
        m_Parts.ForEach(
            Function(_V As GMeshPart(Of VertexType, IndiceType, MaterialType)) _
                           (_V.Scaling(V, _VertexTransformer))
            )

        Return True
    End Function

    ''' <summary>
    ''' Welds the vertices of this mesh.
    ''' </summary>
    ''' <remarks>
    ''' NOTE: This method calls <c>Validate()</c>.
    ''' </remarks>
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    Public Function Weld() As Boolean
        ' Is the mesh locked?
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Do we have anything to do?
        If m_Parts.Count = 0 Then _
            Return False

        ' Call for all parts.
        m_Parts.ForEach(
            Function(V As GMeshPart(Of VertexType, IndiceType, MaterialType)) _
                           (V.Weld())
            )

        Return True
    End Function

    ''' <summary>
    ''' Unwelds the vertices of this mesh.
    ''' </summary>
    ''' <remarks>
    ''' NOTE: This method calls <c>Validate()</c>.
    ''' </remarks>
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    Public Function Unweld() As Boolean
        ' Is the mesh locked?
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Do we have anything to do?
        If m_Parts.Count = 0 Then _
            Return False

        ' Call for all parts.
        m_Parts.ForEach(
            Function(V As GMeshPart(Of VertexType, IndiceType, MaterialType)) _
                           (V.Unweld())
            )

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
    ''' <remarks>
    ''' If <c>MinExtents</c> &lt; <c>MaxExtents</c> then mesh has no vertices.
    ''' If <c>MinExtents</c> = <c>MaxExtents</c> then mesh has 1 vertex.
    ''' Otherwise, atleast one component of <c>MinExtents</c> &lt; <c>MaxExtents</c>.
    ''' </remarks>
    Public Function GetMeshExtents(ByRef MinExtents As Vector3, ByRef MaxExtents As Vector3) As Boolean
        Dim FirstPart As Boolean = True

        ' Check if we have verices.
        If TotalVertexCount = 0 Then _
            MinExtents = New Vector3(1, 1, 1) _
                : MaxExtents = New Vector3(- 1, - 1, - 1) _
                : Return False

        For I As Integer = 0 To m_Parts.Count - 1
            If m_Parts(I).Vertices.Count <> 0 Then
                Dim Min, Max As Vector3

                ' Get the part's extents.
                m_Parts(I).Vertices.GetMeshExtents(Min, Max)

                If FirstPart Then _
                    MinExtents = Min _
                        : MaxExtents = Max _
                        : FirstPart = False _
                    Else _
                    MinExtents.Minimize(Min) _
                        : MaxExtents.Maximize(Max)

            End If ' If m_Parts(I).Vertices.Count <> 0 Then
        Next I ' For I As Integer = 0 To m_Parts.Count - 1

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
    ''' <remarks>
    ''' If <c>MinExtents</c> &lt; <c>MaxExtents</c> then mesh has no vertices.
    ''' If <c>MinExtents</c> = <c>MaxExtents</c> then mesh has no vertices.
    ''' Otherwise, atleast one component of <c>MinExtents</c> &lt; <c>MaxExtents</c>.
    ''' </remarks>
    Public Function GetMeshExtents(ByRef MinExtents As Vector4, ByRef MaxExtents As Vector4) As Boolean
        Dim FirstPart As Boolean = True

        ' Check if we have verices.
        If TotalVertexCount = 0 Then _
            MinExtents = New Vector4(1, 1, 1, 1) _
                : MaxExtents = New Vector4(- 1, - 1, - 1, - 1) _
                : Return False

        For I As Integer = 0 To m_Parts.Count - 1
            If m_Parts(I).Vertices.Count <> 0 Then
                Dim Min, Max As Vector4

                ' Get the part's extents.
                m_Parts(I).Vertices.GetMeshExtents(Min, Max)

                If FirstPart Then _
                    MinExtents = Min _
                        : MaxExtents = Max _
                        : FirstPart = False _
                    Else _
                    MinExtents.Minimize(Min) _
                        : MaxExtents.Maximize(Max)

            End If ' If m_Parts(I).Vertices.Count <> 0 Then
        Next I ' For I As Integer = 0 To m_Parts.Count - 1

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
    Public Function GetMeshSphere(ByRef Center As Vector3, ByRef Radius As Single) As Boolean
        Dim Min, Max As Vector3

        If TotalVertexCount = 0 Then _
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
    Public Function GetMeshSphere(ByRef Center As Vector4, ByRef Radius As Single) As Boolean
        Dim Min, Max As Vector4

        If TotalVertexCount = 0 Then _
            Center = New Vector4(0, 0, 0, 1) _
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
    ''' Flips the normals of all vertices.
    ''' </summary>
    Public Function FlipNormals() As Boolean
        ' Is the mesh locked?
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Do we have anything to do?
        If m_Parts.Count = 0 Then _
            Return False

        ' Call for all parts.
        m_Parts.ForEach(
            Function(V As GMeshPart(Of VertexType, IndiceType, MaterialType)) _
                           (V.FlipNormals())
            )

        Return True
    End Function

    ''' <summary>
    ''' Reverses the order of all triangles.
    ''' </summary>
    ''' <remarks>
    ''' The mesh is triangulated first.
    ''' </remarks>
    Public Function ReverseFaceOrder() As Boolean
        ' Is the mesh locked?
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Do we have anything to do?
        If m_Parts.Count = 0 Then _
            Return False

        ' Call for all parts.
        m_Parts.ForEach(
            Function(V As GMeshPart(Of VertexType, IndiceType, MaterialType)) _
                           (V.ReverseFaceOrder())
            )

        Return True
    End Function

    ''' <summary>
    ''' Calculates the normals in the mesh.
    ''' </summary>
    ''' <remarks>
    ''' This does nothing if there are no parts.
    ''' </remarks>
    Public Function CalculateNormals() As Boolean
        ' Is the mesh locked?
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Do we have anything to do?
        If m_Parts.Count = 0 Then _
            Return False

        ' Call for all parts.
        m_Parts.ForEach(
            Function(V As GMeshPart(Of VertexType, IndiceType, MaterialType)) _
                           (V.CalculateNormals())
            )

        Return True
    End Function

    ''' <summary>
    ''' Merges two parts and places it in the first part.
    ''' </summary>
    ''' <param name="Part1Index">
    ''' The index of the first part.
    ''' </param>
    ''' <param name="Part2Index">
    ''' The index of the second part.
    ''' </param>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>Part1Index</c>, <c>Part2Index</c> is
    ''' out of bounds.
    ''' </exception>
    ''' <remarks>
    ''' This does nothing if <c>Part1Index = Part2Index</c>.
    ''' </remarks>
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    Public Function Merge(ByVal Part1Index As Integer, ByVal Part2Index As Integer) As Boolean
        ' Do we have anything to do?
        If Part1Index = Part2Index Then _
            Return False

        ' Is the mesh locked?
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Check inputs.
        If (Part1Index < 0) OrElse (Part1Index >= m_Parts.Count) Then _
            Throw New ArgumentOutOfRangeException("Part1Index") _
                : Exit Function

        ' Check inputs.
        If (Part2Index < 0) OrElse (Part2Index >= m_Parts.Count) Then _
            Throw New ArgumentOutOfRangeException("Part2Index") _
                : Exit Function

        ' Simply append part 2 to part 1.
        m_Parts(Part1Index).Append(m_Parts(Part2Index))

        ' Remove input part.
        Me.Remove(Part2Index)

        Return True
    End Function

    ''' <summary>
    ''' Merges a range of parts and places it in the first part.
    ''' </summary>
    ''' <param name="StartIndex">
    ''' The index of the first part in the merge operation.
    ''' </param>
    ''' <param name="EndIndex">
    ''' The index of the last part in the merge operation.
    ''' </param>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>StartIndex</c>, <c>EndIndex</c> is
    ''' out of bounds.
    ''' </exception>
    ''' <exception cref="MeshLockedException">
    ''' Thrown when the mesh is locked.
    ''' </exception>
    ''' <remarks>
    ''' This does nothing if <c>StartIndex = EndIndex</c>.
    ''' When <c>StartIndex > EndIndex</c>, both are swapped.
    ''' </remarks>
    Public Function MergeRange(ByVal StartIndex As Integer, ByVal EndIndex As Integer) As Boolean
        ' Do we have anything to do?
        If StartIndex = EndIndex Then _
            Return False

        ' Is the mesh locked?
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Check inputs.
        If (StartIndex < 0) OrElse (StartIndex >= m_Parts.Count) Then _
            Throw New ArgumentOutOfRangeException("StartIndex") _
                : Exit Function

        ' Check inputs.
        If (EndIndex < 0) OrElse (EndIndex >= m_Parts.Count) Then _
            Throw New ArgumentOutOfRangeException("EndIndex") _
                : Exit Function

        ' Check if the two inputs need to be swapped.
        If StartIndex > EndIndex Then
            Dim I As Integer = StartIndex
            StartIndex = EndIndex
            EndIndex = I
        End If ' If StartIndex > EndIndex Then

        ' Merge all the specified parts.
        For I As Integer = StartIndex + 1 To EndIndex
            ' Append the part.
            m_Parts(StartIndex).Append(m_Parts(I))

        Next I ' For I As Integer = StartIndex + 1 To EndIndex

        ' Remove the parts.
        RemoveRange(StartIndex + 1, EndIndex)

        Return True
    End Function

    ''' <summary>
    ''' Merges all parts of the mesh into one single part.
    ''' </summary>
    Public Function MergeAll() As Boolean
        If m_Parts.Count = 0 Then _
            Return False

        Return MergeRange(0, m_Parts.Count - 1)
    End Function

    ''' <summary>
    ''' Reads the mesh from a file.
    ''' </summary>
    ''' <param name="Filename">
    ''' The file from which the mesh is to be read.
    ''' </param>
    ''' <remarks>
    ''' This is only a place holder, this does nothing.
    ''' </remarks>
    Public Overridable Function ReadFile(ByVal Filename As String) As Boolean
        Return False
    End Function

    ''' <summary>
    ''' Writes this mesh to a file.
    ''' </summary>
    ''' <param name="Filename">
    ''' The file to which the mesh is to be written.
    ''' </param>
    ''' <remarks>
    ''' This is only a place holder, this does nothing.
    ''' </remarks>
    Public Overridable Function WriteFile(ByVal Filename As String) As Boolean
        Return False
    End Function

    ''' <summary>
    ''' Locks the mesh and prepares an independent buffer for the 
    ''' mesh. This is needed for buffered rendering.
    ''' </summary>
    ''' <param name="_Device">
    ''' The device associated with the buffers.
    ''' </param>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>_Device Is Nothing</c>.
    ''' </exception>
    Public Function Lock(ByVal _Device As Device) As Boolean
        Dim Vtx As VertexType, Ind As IndiceType
        Dim IndSize, MaxIndSize As Integer

        ' Check if there is data to lock.
        If (TotalVertexCount = 0) OrElse (TotalIndiceCount = 0) Then _
            Return False

        ' Determine maximum indice size.
        If _Device.DeviceCaps.MaxVertexIndex <= &HFFFF Then _
            MaxIndSize = 2 _
            Else _
            MaxIndSize = 4

        ' Get the actual indice size now.
        IndSize = Math.Min(MaxIndSize, Len(Ind))

        ' Create new buffers and lock the mesh.
        Lock(
            _Device,
            LockFlags.None,
            New VertexBuffer(GetType(VertexType),
                             TotalVertexCount,
                             _Device,
                             Usage.WriteOnly,
                             Vtx.Format,
                             Pool.Managed
                             ),
            0,
            New IndexBuffer(_Device,
                            IndSize*TotalIndiceCount,
                            Usage.WriteOnly,
                            Pool.Managed,
                            CBool(IndSize <> 4)
                            ),
            0
            )

        ' Set the flags.
        m_Locked = True
        m_InternalBuffers = True

        Return True
    End Function

    ''' <summary>
    ''' Locks the mesh and prepares the input buffer for the 
    ''' mesh. This is needed for buffered rendering.
    ''' </summary>
    ''' <param name="_Device">
    ''' The device associated with the buffers.
    ''' </param>
    ''' <param name="_LockFlags">
    ''' Lock flags used for <c>SetData</c>.
    ''' </param>
    ''' <param name="VB">
    ''' The (external) vertex buffer to write to.
    ''' </param>
    ''' <param name="BaseVertexOffset">
    ''' The base vertex offset in the buffer.
    ''' </param>
    ''' <param name="IB">
    ''' The (external) index buffer to write to.
    ''' </param>
    ''' <param name="BaseIndiceOffset">
    ''' The base indice offset in the buffer.
    ''' </param>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>BaseVertexOffset</c> or <c>BaseIndiceOffset</c>
    ''' is negative.
    ''' </exception>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when one of <c>_Device</c>, <c>VB</c>, <c>IB</c> is
    ''' <c>Nothing</c>. Also thrown when any one of the buffers is
    ''' of insufficient length.
    ''' </exception>
    Friend Function Lock(ByVal _Device As Device,
                         ByVal _LockFlags As LockFlags,
                         ByVal VB As VertexBuffer,
                         ByVal BaseVertexOffset As Integer,
                         ByVal IB As IndexBuffer,
                         ByVal BaseIndiceOffset As Integer) As Boolean

        Dim Vtx As VertexType
        Dim OffsetV As Integer, OffsetI As Integer
        Dim BufIndSize As Integer

        ' Check if mesh is already locked.
        If m_Locked Then _
            Throw New MeshLockedException _
                : Exit Function

        ' Check inputs.
        If _Device Is Nothing Then _
            Throw New ArgumentNullException("_Device") _
                : Exit Function

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

        ' Get the indice size of the buffer.
        If IB.Description.Is16BitIndices Then _
            BufIndSize = 2 _
            Else _
            BufIndSize = 4

        ' Check inputs.
        'Trace.Assert(VB.Device Is IB.Device) (???)

        ' Check if copy operation is possible.
        If VB.SizeInBytes < (BaseVertexOffset + TotalVertexCount)*Vtx.VertexSize Then _
            Throw New ArgumentException("Vertex buffer too small.") _
                : Exit Function

        ' Check if copy operation is possible.
        If IB.SizeInBytes < (BaseVertexOffset + TotalIndiceCount)*BufIndSize Then _
            Throw New ArgumentException("Indice buffer too small.") _
                : Exit Function

        ' Validate the mesh.
        Validate()

        ' Copy references to the buffer.
        m_VB = VB
        m_IB = IB

        ' Copy the offsets.
        m_BaseVertexOffset = BaseVertexOffset
        m_BaseIndiceOffset = BaseIndiceOffset

        ' Write the data in the parts.
        For I As Integer = 0 To m_Parts.Count - 1
            With m_Parts(I)
                ' Write the vertices in this part.
                OffsetV += .Vertices.WriteDataToBuffer(
                    m_VB,
                    BaseVertexOffset*Vtx.VertexSize + OffsetV,
                    _LockFlags
                    )

                ' Write the indices in all the primitive groups.
                For J As Integer = 0 To m_Parts(I).PrimitiveGroupCount - 1
                    ' Do nothing if no indices.
                    If .PrimitiveGroups(J).IndiceCount = 0 Then _
                        Continue For

                    ' Write the indices.
                    OffsetI += .PrimitiveGroups(J).WriteDataToBuffer(
                        m_IB,
                        BaseIndiceOffset*BufIndSize + OffsetI,
                        _LockFlags
                        )

                Next J ' For J As Integer = 0 To m_Parts(I).PrimitiveGroupCount - 1
            End With ' With m_Parts(I)
        Next I ' For I As Integer = 0 To m_Parts.Count - 1

        ' Set the flags.
        m_Locked = True
        m_InternalBuffers = False

        Return True
    End Function

    ''' <summary> 
    ''' Unlocks the mesh, and removes the buffers
    ''' associated.
    ''' </summary>
    Public Function Unlock() As Boolean
        ' Free the buffers if they are internal.
        If m_InternalBuffers Then
            If m_VB IsNot Nothing Then _
                m_VB.Dispose()

            If m_IB IsNot Nothing Then _
                m_IB.Dispose()

        End If ' If m_InternalBuffers then

        ' Remove references.
        m_VB = Nothing
        m_IB = Nothing

        ' Reset flags.
        m_Locked = False
        m_InternalBuffers = False

        ' Reset offsets.
        m_BaseVertexOffset = 0
        m_BaseIndiceOffset = 0

        Return True
    End Function

    ''' <summary>
    ''' Performs buffered rendering.
    ''' </summary>
    ''' <param name="_Device">
    ''' The device to which render to.
    ''' </param>
    ''' <remarks>
    ''' The mesh must be locked before calling this method.
    ''' </remarks>
    Public Function Render(ByVal _Device As Device) As Boolean
        Dim VOffset, IOffset As Integer

        ' Check if there is data to render.
        If (TotalVertexCount = 0) OrElse (TotalIndiceCount = 0) Then _
            Return False

        ' Check if the mesh is locked.
        If Not m_Locked Then _
            Throw New MeshNotLockedException _
                : Exit Function

        ' Render all parts.
        For I As Integer = 0 To m_Parts.Count - 1
            ' Render the part.
            Dim _IndRendered As Integer =
                    m_Parts(I).Render(
                        _Device, m_VB, m_IB,
                        m_BaseVertexOffset + VOffset,
                        m_BaseIndiceOffset + IOffset
                        )

            ' Increment the offsets.
            VOffset += m_Parts(I).Vertices.Count
            IOffset += _IndRendered

        Next I ' For I As Integer = 0 To m_Parts.Count - 1

        Return True
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
    Public Function RenderUP(ByVal _Device As Device) As Boolean
        Dim Mat As MaterialType

        ' Check inputs.
        If _Device Is Nothing Then _
            Throw New ArgumentNullException("_Device") _
                : Exit Function

        ' Render all parts.
        m_Parts.ForEach(
            Function(V As GMeshPart(Of VertexType, IndiceType, MaterialType)) _
                           (V.Render(_Device))
            )

        ' Reset the material attributes we set.
        Mat.Reset(_Device)
    End Function

    ''' <summary>
    ''' Checks for any sort of errors.
    ''' </summary>
    Public Function Validate() As Boolean
#If DEBUG Then

  ' Call validate function for each primitive with the given bounds.
  For I As Integer = 0 To m_Parts.Count - 1
   Try
    m_Parts(I).Validate()

   Catch ex As Exception
    Throw New Exception("Error in m_Parts(" & CStr(I) & ").", ex)
    Return False

   End Try

  Next I ' For I As Integer = 0 To m_Parts.Count - 1

#End If

        Return True
    End Function
End Class
