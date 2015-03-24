Namespace VertexFields
    ''' <summary>
    ''' Enables a structure implementing <c>IVertex</c> to have
    ''' single\multiple texture co-ordinate set(s) (3D).
    ''' </summary>
    Public Interface IVertexTex3
        Inherits IVertex

        ''' <summary>
        ''' Retrieves texture co-ordinates.
        ''' </summary>
        ''' <param name="Index">
        ''' The texture co-ordinate set to retrieve.
        ''' </param>
        Function GetTexCoords3(Optional ByVal Index As Integer = 0) As Vector3

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
        Function SetTexCoords3(ByVal V As Vector3, Optional ByVal Index As Integer = 0) As IVertex
    End Interface
End Namespace
