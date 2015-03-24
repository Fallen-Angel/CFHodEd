Namespace VertexFields
    ''' <summary>
    ''' Enables a structure implementing <c>IVertex</c> to have
    ''' a specular colour.
    ''' </summary>
    Public Interface IVertexSpecular
        Inherits IVertex

        ''' <summary>
        ''' Retrieves specular colour of the vertex.
        ''' </summary>
        Function GetSpecular() As ColorValue

        ''' <summary>
        ''' Sets specular colour of the vertex.
        ''' </summary>
        ''' <param name="V">
        ''' The colour.
        ''' </param>
        ''' <returns>
        ''' The modified vertex.
        ''' </returns>
        Function SetSpecular(ByVal V As ColorValue) As IVertex
    End Interface
End Namespace
