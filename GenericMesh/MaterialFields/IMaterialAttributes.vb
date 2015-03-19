Namespace MaterialFields
 ''' <summary>
 ''' Enables a structure implementing <c>IMaterial</c> to have
 ''' an attributes field.
 ''' </summary>
 Public Interface IMaterialAttributes
  Inherits IMaterial

  ''' <summary>
  ''' Retrieves the attributes of the material.
  ''' </summary>
  Function GetAttributes() As Direct3D.Material

  ''' <summary>
  ''' Sets the attributes of the material.
  ''' </summary>
  ''' <param name="V">
  ''' The attributes to set.
  ''' </param>
  ''' <returns>
  ''' Modified material.
  ''' </returns>
  Function SetAttributes(ByVal V As Direct3D.Material) As IMaterial

 End Interface

End Namespace
