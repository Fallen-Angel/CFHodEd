Namespace VertexFields
    ''' <summary>
    ''' Enables a structure implementing <c>IVertex</c> to have
    ''' a diffuse colour.
    ''' </summary>
    Public Interface IVertexDiffuse
        Inherits IVertex

        ''' <summary>
        ''' Retrieves diffuse colour of the vertex.
        ''' </summary>
        Function GetDiffuse() As ColorValue

        ''' <summary>
        ''' Sets diffuse colour of the vertex.
        ''' </summary>
        ''' <param name="V">
        ''' The colour.
        ''' </param>
        ''' <returns>
        ''' The modified vertex.
        ''' </returns>
        Function SetDiffuse(ByVal V As ColorValue) As IVertex
    End Interface
End Namespace
