Namespace VertexFields
    ''' <summary>
    ''' Enables a structure implementing <c>IVertex</c> to be
    ''' transformable.
    ''' </summary>
    Public Interface IVertexTransformable
        Inherits IVertex

        ''' <summary>
        ''' Function to transform the vertex.
        ''' </summary>
        ''' <param name="M">
        ''' The transforming matrix.
        ''' </param>
        ''' <returns>
        ''' Modified vertex.
        ''' </returns>
        Function Transform(ByVal M As Matrix) As IVertex
    End Interface
End Namespace
