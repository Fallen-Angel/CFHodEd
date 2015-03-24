Imports GenericMesh
Imports GenericMesh.VertexFields

''' <summary>
''' Structure representing Homeworld2 Simple Mesh Vertex.
''' </summary>
Public Structure SimpleVertex
    Implements IVertex
    Implements IVertexTransformable

    Implements IVertexPosition3
    Implements IVertexNormal3
    Implements IVertexTex2

    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Position.</summary>
    Public Position As Vector3

    ''' <summary>Normal.</summary>
    Public Normal As Vector3

    ''' <summary>Texture co-ordinates.</summary>
    Public Tex As Vector2

    ''' <summary>W component of position.</summary>
    Private PositionW As Single

    ''' <summary>W component of normal.</summary>
    Private NormalW As Single

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
    Public ReadOnly Property Format() As Direct3D.VertexFormats Implements IVertex.Format
        Get
            Return Direct3D.VertexFormats.Position Or
                   Direct3D.VertexFormats.Normal Or
                   Direct3D.VertexFormats.Texture1
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

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Sets the default values for non-zero fields.
    ''' </summary>
    Friend Sub Initialize() Implements IVertex.Initialize
        Position = New Vector3(0, 0, 0)
        Normal = New Vector3(0, 0, 1)
        Tex = New Vector2(0, 0)
        PositionW = 1
        NormalW = 1
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
    Private Function Equal(ByVal other As IVertex) As Boolean Implements System.IEquatable(Of IVertex).Equals
        ' Check if 'other' is of same type.
        If Not TypeOf other Is BasicVertex Then _
            Throw New ArgumentException("Object must be of type " & TypeName(Me).ToString & ".") _
                : Exit Function

        ' Get the vertex.
        Dim V As SimpleVertex = CType(other, SimpleVertex)

        ' Compare.
        If (V.Position <> Position) OrElse
           (V.Normal <> Normal) OrElse
           (V.Tex <> Tex) OrElse
           (V.PositionW <> PositionW) OrElse
           (V.NormalW <> NormalW) Then _
            Return False _
            Else _
            Return True
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
    Private Function Transform(ByVal M As Matrix) As IVertex Implements IVertexTransformable.Transform
        Position.TransformCoordinate(M)
        Normal.TransformNormal(M)

        Return Me
    End Function

    ''' <summary>
    ''' Retrieves 3D co-ordinates of the vertex.
    ''' </summary>
    Private Function GetPosition3() As Vector3 Implements IVertexPosition3.GetPosition3
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
    Private Function SetPosition3(ByVal V As Vector3) As IVertex Implements IVertexPosition3.SetPosition3
        Position = V
        Return Me
    End Function

    ''' <summary>
    ''' Retrieves normal of the vertex.
    ''' </summary>
    Private Function GetNormal3() As Vector3 Implements IVertexNormal3.GetNormal3
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
    Private Function SetNormal3(ByVal V As Vector3) As IVertex Implements IVertexNormal3.SetNormal3
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
    Private Function GetTexCoords2(Optional ByVal Index As Integer = 0) As Vector2 Implements IVertexTex2.GetTexCoords2
        Return Tex
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
        Implements IVertexTex2.SetTexCoords2
        Tex = V
        Return Me
    End Function

    ''' <summary>
    ''' Reads the vertex from an IFF file.
    ''' </summary>
    ''' <param name="IFF">
    ''' The IFF reader to read from.
    ''' </param>
    Friend Shared Function ReadIFF(ByVal IFF As IFF.IFFReader) As SimpleVertex
        Dim V As SimpleVertex

        With V
            ' Initialize the vertex to account for missing fields.
            .Initialize()

            .Position.X = IFF.ReadSingle()
            .Position.Y = IFF.ReadSingle()
            .Position.Z = IFF.ReadSingle()
            .PositionW = IFF.ReadSingle()

            .Normal.X = IFF.ReadSingle()
            .Normal.Y = IFF.ReadSingle()
            .Normal.Z = IFF.ReadSingle()
            .NormalW = IFF.ReadSingle()

            .Tex.X = IFF.ReadSingle()
            .Tex.Y = IFF.ReadSingle()

        End With

        Return V
    End Function

    ''' <summary>
    ''' Writes the vertex to an IFF file.
    ''' </summary>
    ''' <param name="IFF">
    ''' The IFF writer to write to.
    ''' </param>
    Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
        IFF.Write(Position.X)
        IFF.Write(Position.Y)
        IFF.Write(Position.Z)
        IFF.Write(PositionW)

        IFF.Write(Normal.X)
        IFF.Write(Normal.Y)
        IFF.Write(Normal.Z)
        IFF.Write(NormalW)

        IFF.Write(Tex.X)
        IFF.Write(Tex.Y)
    End Sub
End Structure
