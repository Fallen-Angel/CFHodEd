Namespace VertexFields
    ''' <summary>
    ''' Enables a structure implementing <c>IVertex</c> to have
    ''' single\multiple texture co-ordinate set(s) (4D).
    ''' </summary>
    Public Interface IVertexTex4
        Inherits IVertex

        ''' <summary>
        ''' Retrieves texture co-ordinates.
        ''' </summary>
        ''' <param name="Index">
        ''' The texture co-ordinate set to retrieve.
        ''' </param>
        Function GetTexCoords4(Optional ByVal Index As Integer = 0) As Vector4

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
        Function SetTexCoords4(ByVal V As Vector4, Optional ByVal Index As Integer = 0) As IVertex
    End Interface
End Namespace
