Imports GenericMesh
Imports GenericMesh.MaterialFields

Partial Class Material
 ''' <summary>
 ''' Structure representing the default material for meshes that do not have
 ''' a material.
 ''' </summary>
 Public Structure [Default]
  Implements IMaterial
  Implements IMaterialName
  Implements IMaterialTexture

  ''' <summary>
  ''' Dummy variable.
  ''' </summary>
  Private m_Dummy As Integer

  ''' <summary>
  ''' Applies the material.
  ''' </summary>
  Private Sub Apply(ByVal _Device As Direct3D.Device) Implements GenericMesh.IMaterial.Apply
   _Device.Material = New Direct3D.Material With { _
    .AmbientColor = New Direct3D.ColorValue(0.2F, 0.2F, 0.2F), _
    .DiffuseColor = New Direct3D.ColorValue(0.8F, 0.8F, 0.8F), _
    .SpecularColor = New Direct3D.ColorValue(0, 0, 0), _
    .EmissiveColor = New Direct3D.ColorValue(0, 0, 0), _
    .SpecularSharpness = 0 _
   }

  End Sub

  ''' <summary>
  ''' This does nothing.
  ''' </summary>
  Private Sub Initialize() Implements GenericMesh.IMaterial.Initialize

  End Sub

  ''' <summary>
  ''' This does nothing.
  ''' </summary>
  Private Sub Reset(ByVal _Device As Direct3D.Device) Implements GenericMesh.IMaterial.Reset

  End Sub

  ''' <summary>
  ''' This does nothing.
  ''' </summary>
  Private Function Equal(ByVal other As GenericMesh.IMaterial) As Boolean Implements System.IEquatable(Of GenericMesh.IMaterial).Equals
   If TypeOf other Is [Default] Then _
    Return True _
   Else _
    Return False

  End Function

  ''' <summary>
  ''' Checks whether two materials are equal or not.
  ''' </summary>
  ''' <param name="obj">
  ''' The other object to compare to.
  ''' </param>
  Public Overrides Function Equals(ByVal obj As Object) As Boolean
   ' Check type.
   If Not TypeOf obj Is Reference Then _
    Return False

   ' Perform check.
   Return Equal(CType(obj, IMaterial))

  End Function

  ''' <summary>
  ''' Returns the name of this material.
  ''' </summary>
  Private Function GetMaterialName() As String Implements GenericMesh.MaterialFields.IMaterialName.GetMaterialName
   Return "Default"

  End Function

  ''' <summary>
  ''' This does nothing.
  ''' </summary>
  Private Function SetMaterialName(ByVal V As String) As GenericMesh.IMaterial Implements GenericMesh.MaterialFields.IMaterialName.SetMaterialName
   Return Me

  End Function

  ''' <summary>
  ''' Returns the name if texture used by this material.
  ''' </summary>
  Private Function GetTextureName(Optional ByVal Index As Integer = 0) As String Implements GenericMesh.MaterialFields.IMaterialTexture.GetTextureName
   Return ""

  End Function

  ''' <summary>
  ''' This does nothing.
  ''' </summary>
  Private Function SetTextureName(ByVal V As String, Optional ByVal Index As Integer = 0) As GenericMesh.IMaterial Implements GenericMesh.MaterialFields.IMaterialTexture.SetTextureName
   Return Me

  End Function

 End Structure

End Class
