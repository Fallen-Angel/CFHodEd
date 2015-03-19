Namespace VertexFields
 ''' <summary>
 ''' Class containing functions for writing various fields to generic
 ''' vertices.
 ''' </summary>
 Public NotInheritable Class VertexFieldWriter
  ''' <summary>
  ''' Writes the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Sub Position3(Of VertexType As {Structure, IVertex}) _
                             (ByRef Vtx As VertexType, ByVal V As Vector3)

   ' Check presence.
   If TypeOf Vtx Is IVertexPosition3 Then _
    Vtx = CType(CType(Vtx, IVertexPosition3).SetPosition3(V), VertexType)

  End Sub

  ''' <summary>
  ''' Writes the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Sub Position4(Of VertexType As {Structure, IVertex}) _
                             (ByRef Vtx As VertexType, ByVal V As Vector4)

   ' Check presence.
   If TypeOf Vtx Is IVertexPosition4 Then _
    Vtx = CType(CType(Vtx, IVertexPosition4).SetPosition4(V), VertexType)

  End Sub

  ''' <summary>
  ''' Writes the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Sub Normal3(Of VertexType As {Structure, IVertex}) _
                           (ByRef Vtx As VertexType, ByVal V As Vector3)

   ' Check presence.
   If TypeOf Vtx Is IVertexNormal3 Then _
    Vtx = CType(CType(Vtx, IVertexNormal3).SetNormal3(V), VertexType)

  End Sub

  ''' <summary>
  ''' Writes the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Sub TexCoords1(Of VertexType As {Structure, IVertex}) _
                              (ByRef Vtx As VertexType, ByVal V As Single, _
                      Optional ByVal Index As Integer = 0)

   ' Check presence.
   If TypeOf Vtx Is IVertexTex1 Then _
    Vtx = CType(CType(Vtx, IVertexTex1).SetTexCoords1(V, Index), VertexType)

  End Sub

  ''' <summary>
  ''' Writes the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Sub TexCoords2(Of VertexType As {Structure, IVertex}) _
                              (ByRef Vtx As VertexType, ByVal V As Vector2, _
                      Optional ByVal Index As Integer = 0)

   ' Check presence.
   If TypeOf Vtx Is IVertexTex2 Then _
    Vtx = CType(CType(Vtx, IVertexTex2).SetTexCoords2(V, Index), VertexType)

  End Sub

  ''' <summary>
  ''' Writes the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Sub TexCoords3(Of VertexType As {Structure, IVertex}) _
                              (ByRef Vtx As VertexType, ByVal V As Vector3, _
                      Optional ByVal Index As Integer = 0)

   ' Check presence.
   If TypeOf Vtx Is IVertexTex3 Then _
    Vtx = CType(CType(Vtx, IVertexTex3).SetTexCoords3(V, Index), VertexType)

  End Sub

  ''' <summary>
  ''' Writes the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Sub TexCoords4(Of VertexType As {Structure, IVertex}) _
                              (ByRef Vtx As VertexType, ByVal V As Vector4, _
                      Optional ByVal Index As Integer = 0)

   ' Check presence.
   If TypeOf Vtx Is IVertexTex4 Then _
    Vtx = CType(CType(Vtx, IVertexTex4).SetTexCoords4(V, Index), VertexType)

  End Sub

  ''' <summary>
  ''' Writes the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Sub Diffuse(Of VertexType As {Structure, IVertex}) _
                           (ByRef Vtx As VertexType, ByVal V As ColorValue)

   ' Check presence.
   If TypeOf Vtx Is IVertexDiffuse Then _
    Vtx = CType(CType(Vtx, IVertexDiffuse).SetDiffuse(V), VertexType)

  End Sub

  ''' <summary>
  ''' Writes the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Sub Specular(Of VertexType As {Structure, IVertex}) _
                            (ByRef Vtx As VertexType, ByVal V As ColorValue)

   ' Check presence.
   If TypeOf Vtx Is IVertexSpecular Then _
    Vtx = CType(CType(Vtx, IVertexSpecular).SetSpecular(V), VertexType)

  End Sub

  ''' <summary>
  ''' Writes the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Sub PointSize(Of VertexType As {Structure, IVertex}) _
                             (ByRef Vtx As VertexType, ByVal V As Single)

   ' Check presence.
   If TypeOf Vtx Is IVertexPointSize Then _
    Vtx = CType(CType(Vtx, IVertexPointSize).SetPointSize(V), VertexType)

  End Sub

 End Class

End Namespace

