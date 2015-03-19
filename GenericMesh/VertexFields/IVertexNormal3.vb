Namespace VertexFields
 ''' <summary>
 ''' Enables a structure implementing <c>IVertex</c> to have
 ''' a normal field.
 ''' </summary>
 Public Interface IVertexNormal3
  Inherits IVertex

  ''' <summary>
  ''' Retrieves normal of the vertex.
  ''' </summary>
  Function GetNormal3() As Vector3

  ''' <summary>
  ''' Sets normal of the vertex.
  ''' </summary>
  ''' <param name="V">
  ''' The normal.
  ''' </param>
  ''' <returns>
  ''' The modified vertex.
  ''' </returns>
  Function SetNormal3(ByVal V As Vector3) As IVertex

 End Interface

End Namespace
