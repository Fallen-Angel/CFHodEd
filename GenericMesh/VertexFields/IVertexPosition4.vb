Namespace VertexFields
 ''' <summary>
 ''' Enables a structure implementing <c>IVertex</c> to have
 ''' a position (4D) field.
 ''' </summary>
 Public Interface IVertexPosition4
  Inherits IVertex

  ''' <summary>
  ''' Retrieves 4D co-ordinates of the vertex.
  ''' </summary>
  Function GetPosition4() As Vector4

  ''' <summary>
  ''' Sets 4D co-ordinates of the vertex.
  ''' </summary>
  ''' <param name="V">
  ''' The co-ordinates.
  ''' </param>
  ''' <returns>
  ''' The modified vertex.
  ''' </returns>
  Function SetPosition4(ByVal V As Vector4) As IVertex

 End Interface

End Namespace
