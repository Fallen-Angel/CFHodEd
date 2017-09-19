Imports System.IO
Imports GenericMesh
Imports GenericMesh.VertexFields

''' <summary>
''' Structure representing a vertex with position and normal fields.
''' </summary>
Public Structure PNVertex
    Implements IVertex
    Implements IVertexTransformable

    Implements IVertexPosition3
    Implements IVertexNormal3

    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Position.</summary>
    Public Position As Vector3

    ''' <summary>Normal.</summary>
    Public Normal As Vector3

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
                   Direct3D.VertexFormats.Normal
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
        Dim V As BasicVertex = CType(other, BasicVertex)

        ' Compare.
        If (V.Position <> Position) OrElse
           (V.Normal <> Normal) Then _
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
    ''' Reads the vertex from an IFF file.
    ''' </summary>
    ''' <param name="IFF">
    ''' The IFF reader to read from.
    ''' </param>
    Friend Shared Function ReadIFF(ByVal IFF As BinaryReader) As PNVertex
        Dim V As PNVertex

        With V
            ' Initialize the vertex to account for missing fields.
            .Initialize()

            .Position.X = IFF.ReadSingle()
            .Position.Y = IFF.ReadSingle()
            .Position.Z = IFF.ReadSingle()

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
    End Sub
End Structure
