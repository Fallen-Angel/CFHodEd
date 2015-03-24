Imports GenericMesh.VertexFields

Namespace Standard
    ''' <summary>
    ''' Untransformed and unlit vertex type containing position (XYZ only), normal, 
    ''' diffuse colour and 1 set of texture co-ordinates.
    ''' </summary>
    ''' <remarks>
    ''' The default vertex type associated with the mesh class.
    ''' </remarks>
    Public Structure Vertex
        ' -------------------------
        ' Interface(s) Implemented.
        ' -------------------------
        Implements IVertex
        Implements IVertexPosition3
        Implements IVertexNormal3
        Implements IVertexTex2
        Implements IVertexDiffuse
        Implements IVertexTransformable

        ' --------------
        ' Class Members.
        ' --------------
        ''' <summary>
        ''' Position.
        ''' </summary>
        Public Position As Vector3

        ''' <summary>
        ''' Normal.
        ''' </summary>
        Public Normal As Vector3

        ''' <summary>
        ''' Diffuse colour.
        ''' </summary>
        Public DiffuseARGB As Int32

        ''' <summary>
        ''' Texture co-ordinates.
        ''' </summary>
        Public TexCoords As Vector2

        ' -----------------------
        ' Constructors\Finalizer.
        ' -----------------------
        ''' <summary>
        ''' Constructs a new vertex with given data.
        ''' </summary>
        ''' <param name="_Position">
        ''' Position.
        ''' </param>
        ''' <param name="_Normal">
        ''' Normal.
        ''' </param>
        ''' <param name="_TexCoords">
        ''' Texture co-ordinates.
        ''' </param>
        ''' <param name="_Colour">
        ''' Diffuse colour.
        ''' </param>
        Public Sub New(ByVal _Position As Vector3,
                       ByVal _Normal As Vector3,
                       ByVal _TexCoords As Vector2,
                       Optional ByVal _Colour As Integer = - 1)

            Position = _Position
            Normal = _Normal
            TexCoords = _TexCoords
            DiffuseARGB = _Colour
        End Sub

        ''' <summary>
        ''' Constructs a new vertex with given data.
        ''' </summary>
        ''' <param name="_Position">
        ''' Position.
        ''' </param>
        ''' <param name="_Normal">
        ''' Normal.
        ''' </param>
        ''' <param name="_TexCoords">
        ''' Texture co-ordinates.
        ''' </param>
        ''' <param name="_Colour">
        ''' Diffuse colour.
        ''' </param>
        Public Sub New(ByVal _Position As Vector3,
                       ByVal _Normal As Vector3,
                       ByVal _TexCoords As Vector2,
                       ByVal _Colour As ColorValue)

            Position = _Position
            Normal = _Normal
            TexCoords = _TexCoords
            DiffuseARGB = _Colour.ToArgb()
        End Sub

        ' -----------------
        ' Class properties.
        ' -----------------
        ''' <summary>
        ''' Returns\Sets the diffuse colour.
        ''' </summary>
        Public Property Diffuse() As ColorValue
            Get
                Return ColorValue.FromArgb(DiffuseARGB)
            End Get

            Set(ByVal value As Microsoft.DirectX.Direct3D.ColorValue)
                DiffuseARGB = value.ToArgb()
            End Set
        End Property

        ''' <summary>
        ''' FVF (Flexible Vertex Format) of the vertex.
        ''' </summary>
        ''' <value>
        ''' FVF (Flexible Vertex Format) of the vertex.
        ''' </value>
        ''' <returns>
        ''' Returns the FVF (Flexible Vertex Format) of the vertex.
        ''' </returns>
        Public ReadOnly Property Format() As VertexFormats Implements IVertex.Format
            Get
                Return _
                    VertexFormats.Position Or
                    VertexFormats.Normal Or
                    VertexFormats.Diffuse Or
                    VertexFormats.Texture1
            End Get
        End Property

        ''' <summary>
        ''' Size of vertex.
        ''' </summary>
        ''' <value>
        ''' Size of vertex.
        ''' </value>
        ''' <remarks>
        ''' Returns the size of vertex.
        ''' </remarks>
        Public ReadOnly Property VertexSize() As Integer Implements GenericMesh.IVertex.VertexSize
            Get
                Return Len(Me)
            End Get
        End Property

        ' ---------
        ' Operators
        ' ---------
        ''' <summary>
        ''' Compares two expressions and returns <c>True</c> if they are equal.
        ''' Otherwise, returns False.
        ''' </summary>
        ''' <param name="L">
        ''' Left operand.
        ''' </param>
        ''' <param name="R">
        ''' Right operand.
        ''' </param>
        ''' <returns>
        ''' <c>True</c> if left operand is equal to right operand;
        ''' <c>False</c> otherwise.
        ''' </returns>
        Public Shared Operator =(ByVal L As Vertex, ByVal R As Vertex) As Boolean
            If (L.Position = R.Position) AndAlso
               (L.Normal = R.Normal) AndAlso
               (L.DiffuseARGB = R.DiffuseARGB) AndAlso
               (L.TexCoords = R.TexCoords) Then _
                Return True _
                Else _
                Return False
        End Operator

        ''' <summary>
        ''' Compares two expressions and returns <c>True</c> if they are not equal.
        ''' Otherwise, returns False.
        ''' </summary>
        ''' <param name="L">
        ''' Left operand.
        ''' </param>
        ''' <param name="R">
        ''' Right operand.
        ''' </param>
        ''' <returns>
        ''' <c>True</c> if left operand is not equal to right operand;
        ''' <c>False</c> otherwise.
        ''' </returns>
        Public Shared Operator <>(ByVal L As Vertex, ByVal R As Vertex) As Boolean
            If (L.Position <> R.Position) OrElse
               (L.Normal <> R.Normal) OrElse
               (L.DiffuseARGB <> R.DiffuseARGB) OrElse
               (L.TexCoords <> R.TexCoords) Then _
                Return True _
                Else _
                Return False
        End Operator

        ' -----------------
        ' Member Functions.
        ' -----------------
        ''' <summary>
        ''' Returns whether another vertex is equivalent to this instance
        ''' or not.
        ''' </summary>
        ''' <param name="obj">
        ''' The vertex which is to be compared.
        ''' </param>
        ''' <returns>
        ''' <c>True</c> or <c>False</c>, depending on whether the vertices
        ''' are equivalent or not.
        ''' </returns>
        ''' <exception cref="ArgumentException">
        ''' <c>obj</c> is not the same type as this instance.
        ''' </exception>
        Private Function Equal(ByVal obj As IVertex) As Boolean Implements IEquatable(Of IVertex).Equals
            ' Check if 'obj' is of same type.
            If Not TypeOf obj Is Vertex Then _
                Throw New ArgumentException("Object must be of type " & TypeName(Me).ToString & ".") _
                    : Exit Function

            ' Get the vertex.
            Dim V As Vertex = CType(obj, Vertex)

            ' Perform comparision.
            Return (Me = V)
        End Function

        ''' <summary>
        ''' Sets the default values for non-zero fields.
        ''' </summary>
        Private Sub Initialize() Implements IVertex.Initialize
            Position = New Vector3(0, 0, 0)
            Normal = New Vector3(0, 0, 1)
            TexCoords = New Vector2(0, 0)
            DiffuseARGB = - 1
        End Sub

        ''' <summary>
        ''' Retrieves 3D co-ordinates of the vertex.
        ''' </summary>
        Private Function GetPosition3() As Vector3 Implements VertexFields.IVertexPosition3.GetPosition3
            Return Position
        End Function

        ''' <summary>
        ''' Sets 3D co-ordinates of the vertex.
        ''' </summary>
        ''' <param name="V">
        ''' The co-ordinates.
        ''' </param>
        ''' <returns>
        ''' The modified vertex.
        ''' </returns>
        Private Function SetPosition3(ByVal V As Vector3) As IVertex _
            Implements VertexFields.IVertexPosition3.SetPosition3
            Position = V
            Return Me
        End Function

        ''' <summary>
        ''' Retrieves normal of the vertex.
        ''' </summary>
        Private Function GetNormal3() As Vector3 Implements VertexFields.IVertexNormal3.GetNormal3
            Return Normal
        End Function

        ''' <summary>
        ''' Sets normal of the vertex.
        ''' </summary>
        ''' <param name="V">
        ''' The normal.
        ''' </param>
        ''' <returns>
        ''' The modified vertex.
        ''' </returns>
        Private Function SetNormal3(ByVal V As Vector3) As IVertex Implements VertexFields.IVertexNormal3.SetNormal3
            Normal = V
            Return Me
        End Function

        ''' <summary>
        ''' Retrieves texture co-ordinates.
        ''' </summary>
        ''' <param name="Index">
        ''' The texture co-ordinate set to retrieve.
        ''' </param>
        ''' <remarks>
        ''' All index values point to the same set.
        ''' </remarks>
        Private Function GetTexCoords2(Optional ByVal Index As Integer = 0) As Vector2 _
            Implements VertexFields.IVertexTex2.GetTexCoords2
            Return TexCoords
        End Function

        ''' <summary>
        ''' Sets texture co-ordinates.
        ''' </summary>
        ''' <param name="V">
        ''' The texture co-ordinates.
        ''' </param>
        ''' <param name="Index">
        ''' The texture co-ordinate set to set.
        ''' </param>
        ''' <returns>
        ''' The modified vertex.
        ''' </returns>
        ''' <remarks>
        ''' All index values point to the same set.
        ''' </remarks>
        Private Function SetTexCoords2(ByVal V As Vector2, Optional ByVal Index As Integer = 0) As IVertex _
            Implements VertexFields.IVertexTex2.SetTexCoords2
            TexCoords = V
            Return Me
        End Function

        ''' <summary>
        ''' Retrieves diffuse colour of the vertex.
        ''' </summary>
        Private Function GetDiffuse() As ColorValue Implements VertexFields.IVertexDiffuse.GetDiffuse
            Return Diffuse
        End Function

        ''' <summary>
        ''' Sets diffuse colour of the vertex.
        ''' </summary>
        ''' <param name="V">
        ''' The colour.
        ''' </param>
        ''' <returns>
        ''' The modified vertex.
        ''' </returns>
        Private Function SetDiffuse(ByVal V As ColorValue) As IVertex Implements VertexFields.IVertexDiffuse.SetDiffuse
            Diffuse = V
            Return Me
        End Function

        ''' <summary>
        ''' Function to transform the vertex.
        ''' </summary>
        ''' <param name="M">
        ''' The transforming matrix.
        ''' </param>
        ''' <returns>
        ''' Modified vertex.
        ''' </returns>
        Public Function Transform(ByVal M As Matrix) As IVertex Implements VertexFields.IVertexTransformable.Transform
            Position.TransformCoordinate(M)
            Normal.TransformNormal(M)

            Return Me
        End Function
    End Structure
End Namespace
