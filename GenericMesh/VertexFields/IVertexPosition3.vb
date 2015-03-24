Namespace VertexFields
    ''' <summary>
    ''' Enables a structure implementing <c>IVertex</c> to have
    ''' a position (3D) field.
    ''' </summary>
    Public Interface IVertexPosition3
        Inherits IVertex

        ''' <summary>
        ''' Retrieves 3D co-ordinates of the vertex.
        ''' </summary>
        Function GetPosition3() As Vector3

        ''' <summary>
        ''' Sets 3D co-ordinates of the vertex.
        ''' </summary>
        ''' <param name="V">
        ''' The co-ordinates.
        ''' </param>
        ''' <returns>
        ''' The modified vertex.
        ''' </returns>
        Function SetPosition3(ByVal V As Vector3) As IVertex
    End Interface
End Namespace
