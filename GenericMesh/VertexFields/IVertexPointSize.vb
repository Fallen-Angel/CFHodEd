Namespace VertexFields
 ''' <summary>
 ''' Enables a structure implementing <c>IVertex</c> to have
 ''' point size.
 ''' </summary>
 Public Interface IVertexPointSize
  Inherits IVertex

  ''' <summary>
  ''' Retrieves the point size.
  ''' </summary>
  Function GetPointSize() As Single

  ''' <summary>
  ''' Sets the point size.
  ''' </summary>
  ''' <param name="V">
  ''' Point size to set.
  ''' </param>
  ''' <returns>
  ''' Modified vertex.
  ''' </returns>
  Function SetPointSize(ByVal V As Single) As IVertex

 End Interface

End Namespace
