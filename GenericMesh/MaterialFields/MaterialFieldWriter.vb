Namespace MaterialFields
 ''' <summary>
 ''' Class containing functions for writing to various fields of a generic
 ''' material.
 ''' </summary>
 Public NotInheritable Class MaterialFieldWriter
  ''' <summary>
  ''' Writes the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Sub MaterialName(Of MaterialType As {Structure, IMaterial}) _
                                (ByRef Mat As MaterialType, ByVal V As String)

   ' Check presence.
   If TypeOf Mat Is IMaterialName Then _
    Mat = CType(CType(Mat, IMaterialName).SetMaterialName(V), MaterialType)

  End Sub

  ''' <summary>
  ''' Writes the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Sub TextureName(Of MaterialType As {Structure, IMaterial}) _
                               (ByRef Mat As MaterialType, ByVal V As String, _
                       Optional ByVal Index As Integer = 0)

   ' Check presence.
   If TypeOf Mat Is IMaterialTexture Then _
    Mat = CType(CType(Mat, IMaterialTexture).SetTextureName(V, Index), MaterialType)

  End Sub

  ''' <summary>
  ''' Writes the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Sub Attributes(Of MaterialType As {Structure, IMaterial}) _
                              (ByRef Mat As MaterialType, ByVal V As Direct3D.Material)

   ' Check presence.
   If TypeOf Mat Is IMaterialAttributes Then _
    Mat = CType(CType(Mat, IMaterialAttributes).SetAttributes(V), MaterialType)

  End Sub

 End Class

End Namespace
