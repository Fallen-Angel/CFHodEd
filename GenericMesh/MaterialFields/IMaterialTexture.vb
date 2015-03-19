Namespace MaterialFields
 ''' <summary>
 ''' Enables a structure implementing <c>IMaterial</c> to have
 ''' a texture name field.
 ''' </summary>
 Public Interface IMaterialTexture
  Inherits IMaterial

  ''' <summary>
  ''' Returns the texture name used by the specified stage.
  ''' </summary>
  ''' <param name="Index">
  ''' The index of the stage being retrieved.
  ''' </param>
  Function GetTextureName(Optional ByVal Index As Integer = 0) As String

  ''' <summary>
  ''' Sets the texture name used by the specified stage.
  ''' </summary>
  ''' <param name="V">
  ''' Texture name to set.
  ''' </param>
  ''' <param name="Index">
  ''' The index of the stage being set.
  ''' </param>
  ''' <returns>
  ''' Modified material.
  ''' </returns>
  Function SetTextureName(ByVal V As String, Optional ByVal Index As Integer = 0) As IMaterial

 End Interface

End Namespace
