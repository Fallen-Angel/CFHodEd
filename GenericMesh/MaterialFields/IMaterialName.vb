Namespace MaterialFields
 ''' <summary>
 ''' Enables a structure implementing <c>IMaterial</c> to have
 ''' a material name field.
 ''' </summary>
 Public Interface IMaterialName
  Inherits IMaterial

  ''' <summary>
  ''' Retrieves the name of the material.
  ''' </summary>
  Function GetMaterialName() As String

  ''' <summary>
  ''' Sets the name of the material.
  ''' </summary>
  ''' <param name="V">
  ''' The name to set.
  ''' </param>
  ''' <returns>
  ''' Modified material.
  ''' </returns>
  Function SetMaterialName(ByVal V As String) As IMaterial

 End Interface

End Namespace
