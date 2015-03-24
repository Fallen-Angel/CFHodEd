Imports GenericMesh
Imports GenericMesh.VertexFields

''' <summary>
''' Structure representing a star in a Homeworld2 star field.
''' </summary>
Public Structure Star
    Implements IVertex
    Implements IVertexTransformable

    Implements IVertexPosition3
    Implements IVertexPointSize
    Implements IVertexDiffuse

    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Position</summary>
    Private m_Position As Vector3

    ''' <summary>Size.</summary>
    Private m_Size As Single

    ''' <summary>Diffuse colour.</summary>
    Private m_Diffuse As Integer

    ''' <summary>Diffuse colour.</summary>
    Private m_Colour As Vector4

    ' -----------------------------
    ' Class contructors\finalizers.
    ' -----------------------------
    ''' <summary>
    ''' Structure copy constructor.
    ''' </summary>
    Public Sub New(ByVal s As Star)
        m_Position = s.m_Position
        m_Size = s.m_Size
        m_Diffuse = s.m_Diffuse
        m_Colour = s.m_Colour
    End Sub

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' FVF (Flexible Vertex Format) of the vertex.
    ''' </summary>
    ''' <value>
    ''' FVF (Flexible Vertex Format) of the vertex.
    ''' </value>
    ''' <returns>
    ''' Returns the FVF (Flexible Vertex Format) of the vertex.
    ''' </returns>
    Friend ReadOnly Property Format() As Microsoft.DirectX.Direct3D.VertexFormats Implements GenericMesh.IVertex.Format
        Get
            Return Direct3D.VertexFormats.Position Or
                   Direct3D.VertexFormats.PointSize Or
                   Direct3D.VertexFormats.Diffuse
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
    Friend ReadOnly Property VertexSize() As Integer Implements GenericMesh.IVertex.VertexSize
        Get
            Return 36
        End Get
    End Property

    ''' <summary>
    ''' Returns\Sets position of the star.
    ''' </summary>
    Public Property Position() As Vector3
        Get
            Return m_Position
        End Get

        Set(ByVal value As Vector3)
            m_Position = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets the star colour.
    ''' </summary>
    Public Property Colour() As Direct3D.ColorValue
        Get
            Return Direct3D.ColorValue.FromArgb(m_Diffuse)
        End Get

        Set(ByVal value As Direct3D.ColorValue)
            m_Diffuse = value.ToArgb()

            m_Colour.X = value.Red
            m_Colour.Y = value.Green
            m_Colour.Z = value.Blue
            m_Colour.W = value.Alpha
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets the size of star.
    ''' </summary>
    Public Property Size() As Single
        Get
            Return m_Size
        End Get

        Set(ByVal value As Single)
            m_Size = value
        End Set
    End Property

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Sets the default values for non-zero fields.
    ''' </summary>
    Private Sub Initialize() Implements GenericMesh.IVertex.Initialize
        m_Position = New Vector3(0, 0, 0)
        m_Size = 1
        m_Diffuse = - 1
        m_Colour = New Vector4(1, 1, 1, 1)
    End Sub

    ''' <summary>
    ''' Returns whether another vertex is equivalent to this instance
    ''' or not.
    ''' </summary>
    ''' <param name="other">
    ''' The vertex which is to be compared.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> or <c>False</c>, depending on whether the vertices
    ''' are equivalent or not.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' <c>other</c> is not the same type as this instance.
    ''' </exception>
    Private Function Equal(ByVal other As GenericMesh.IVertex) As Boolean _
        Implements System.IEquatable(Of GenericMesh.IVertex).Equals
        ' Check if 'other' is of same type.
        If Not TypeOf other Is BasicVertex Then _
            Throw New ArgumentException("Object must be of type " & TypeName(Me).ToString & ".") _
                : Exit Function

        ' Get the vertex.
        Dim obj As Star = CType(other, Star)

        ' Compare.
        If (m_Position = obj.m_Position) AndAlso
           (m_Size = obj.m_Size) AndAlso
           (m_Diffuse = obj.m_Diffuse) Then _
            Return True _
            Else _
            Return False
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
    Private Function Transform(ByVal M As Microsoft.DirectX.Matrix) As GenericMesh.IVertex _
        Implements GenericMesh.VertexFields.IVertexTransformable.Transform
        m_Position.TransformCoordinate(M)
        m_Size *= M.Determinant

        Return Me
    End Function

    ''' <summary>
    ''' Retrieves 3D co-ordinates of the vertex.
    ''' </summary>
    Private Function GetPosition3() As Microsoft.DirectX.Vector3 _
        Implements GenericMesh.VertexFields.IVertexPosition3.GetPosition3
        Return m_Position
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
    Private Function SetPosition3(ByVal V As Microsoft.DirectX.Vector3) As GenericMesh.IVertex _
        Implements GenericMesh.VertexFields.IVertexPosition3.SetPosition3
        m_Position = V
        Return Me
    End Function

    ''' <summary>
    ''' Retrieves the point size.
    ''' </summary>
    Private Function GetPointSize() As Single Implements GenericMesh.VertexFields.IVertexPointSize.GetPointSize
        Return m_Size
    End Function

    ''' <summary>
    ''' Sets the point size.
    ''' </summary>
    ''' <param name="V">
    ''' Point size to set.
    ''' </param>
    ''' <returns>
    ''' Modified vertex.
    ''' </returns>
    Private Function SetPointSize(ByVal V As Single) As GenericMesh.IVertex _
        Implements GenericMesh.VertexFields.IVertexPointSize.SetPointSize
        m_Size = V
        Return Me
    End Function

    ''' <summary>
    ''' Retrieves diffuse colour of the vertex.
    ''' </summary>
    Private Function GetDiffuse() As Microsoft.DirectX.Direct3D.ColorValue _
        Implements GenericMesh.VertexFields.IVertexDiffuse.GetDiffuse
        Return Direct3D.ColorValue.FromArgb(m_Diffuse)
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
    Private Function SetDiffuse(ByVal V As Microsoft.DirectX.Direct3D.ColorValue) As GenericMesh.IVertex _
        Implements GenericMesh.VertexFields.IVertexDiffuse.SetDiffuse
        m_Diffuse = V.ToArgb()
        m_Colour.X = V.Red
        m_Colour.Y = V.Green
        m_Colour.Z = V.Blue
        m_Colour.W = V.Alpha

        Return Me
    End Function

    ''' <summary>
    ''' Reads the vertex from an IFF file.
    ''' </summary>
    ''' <param name="IFF">
    ''' The IFF reader to read from.
    ''' </param>
    Friend Shared Function ReadIFF(ByVal IFF As IFF.IFFReader, ByVal STRF As Boolean) As Star
        Dim V As Star

        With V
            ' Initialize the vertex to account for missing fields.
            .Initialize()

            ' Read fields.
            .m_Position.X = IFF.ReadSingle()
            .m_Position.Y = IFF.ReadSingle()
            .m_Position.Z = IFF.ReadSingle()

            If STRF Then
                .m_Colour.X = IFF.ReadSingle()
                .m_Colour.Y = IFF.ReadSingle()
                .m_Colour.Z = IFF.ReadSingle()
                .m_Colour.W = 1

                .m_Size = IFF.ReadSingle

            Else ' If STRF Then
                .m_Size = IFF.ReadSingle

                .m_Colour.X = IFF.ReadSingle()
                .m_Colour.Y = IFF.ReadSingle()
                .m_Colour.Z = IFF.ReadSingle()
                .m_Colour.W = IFF.ReadSingle()

            End If ' If STRF Then

            ' Set colour.
            .m_Diffuse = New Direct3D.ColorValue(.m_Colour.X, .m_Colour.Y, .m_Colour.Z, .m_Colour.W).ToArgb()

        End With

        Return V
    End Function

    ''' <summary>
    ''' Writes the vertex to an IFF file.
    ''' </summary>
    ''' <param name="IFF">
    ''' The IFF writer to write to.
    ''' </param>
    Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter, ByVal STRF As Boolean)
        IFF.Write(m_Position.X)
        IFF.Write(m_Position.Y)
        IFF.Write(m_Position.Z)

        If STRF Then
            IFF.Write(m_Colour.X)
            IFF.Write(m_Colour.Y)
            IFF.Write(m_Colour.Z)

            IFF.Write(m_Size)

        Else ' If STRF Then
            IFF.Write(m_Size)

            IFF.Write(m_Colour.X)
            IFF.Write(m_Colour.Y)
            IFF.Write(m_Colour.Z)
            IFF.Write(m_Colour.W)

        End If ' If STRF Then
    End Sub
End Structure
