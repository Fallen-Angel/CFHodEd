Namespace VertexFields
 ''' <summary>
 ''' Class containing functions for reading various fields from generic
 ''' vertices.
 ''' </summary>
 Public NotInheritable Class VertexFieldReader
  ''' <summary>
  ''' Reads the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Function Position3(Of VertexType As {Structure, IVertex}) _
                                  (ByVal V As VertexType) As Vector3

   ' Check presence.
   If TypeOf V Is IVertexPosition3 Then _
    Return CType(V, IVertexPosition3).GetPosition3()

  End Function

  ''' <summary>
  ''' Reads the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Function Position4(Of VertexType As {Structure, IVertex}) _
                                  (ByVal V As VertexType) As Vector4

   ' Check presence.
   If TypeOf V Is IVertexPosition4 Then _
    Return CType(V, IVertexPosition4).GetPosition4()

  End Function

  ''' <summary>
  ''' Reads the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Function Normal3(Of VertexType As {Structure, IVertex}) _
                                (ByVal V As VertexType) As Vector3

   ' Check presence.
   If TypeOf V Is IVertexNormal3 Then _
    Return CType(V, IVertexNormal3).GetNormal3()

  End Function

  ''' <summary>
  ''' Reads the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Function TexCoords1(Of VertexType As {Structure, IVertex}) _
                                   (ByVal V As VertexType, Optional ByVal Index As Integer = 0) As Single
   ' Check presence.
   If TypeOf V Is IVertexTex1 Then _
    Return CType(V, IVertexTex1).GetTexCoords1(Index)

  End Function

  ''' <summary>
  ''' Reads the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Function TexCoords2(Of VertexType As {Structure, IVertex}) _
                                   (ByVal V As VertexType, Optional ByVal Index As Integer = 0) As Vector2

   ' Check presence.
   If TypeOf V Is IVertexTex2 Then _
    Return CType(V, IVertexTex2).GetTexCoords2(Index)

  End Function

  ''' <summary>
  ''' Reads the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Function TexCoords3(Of VertexType As {Structure, IVertex}) _
                                   (ByVal V As VertexType, Optional ByVal Index As Integer = 0) As Vector3

   ' Check presence.
   If TypeOf V Is IVertexTex3 Then _
    Return CType(V, IVertexTex3).GetTexCoords3(Index)

  End Function

  ''' <summary>
  ''' Reads the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Function TexCoords4(Of VertexType As {Structure, IVertex}) _
                                   (ByVal V As VertexType, Optional ByVal Index As Integer = 0) As Vector4

   ' Check presence.
   If TypeOf V Is IVertexTex4 Then _
    Return CType(V, IVertexTex4).GetTexCoords4(Index)

  End Function

  ''' <summary>
  ''' Reads the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Function Diffuse(Of VertexType As {Structure, IVertex}) _
                                (ByVal V As VertexType) As ColorValue

   ' Check presence.
   If TypeOf V Is IVertexDiffuse Then _
    Return CType(V, IVertexDiffuse).GetDiffuse()

  End Function

  ''' <summary>
  ''' Reads the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Function Specular(Of VertexType As {Structure, IVertex}) _
                                 (ByVal V As VertexType) As ColorValue

   ' Check presence.
   If TypeOf V Is IVertexSpecular Then _
    Return CType(V, IVertexSpecular).GetSpecular()

  End Function

  ''' <summary>
  ''' Reads the specified field. For more information see the respective interface.
  ''' </summary>
  Public Shared Function PointSize(Of VertexType As {Structure, IVertex}) _
                                  (ByVal V As VertexType) As Single

   ' Check presence.
   If TypeOf V Is IVertexPointSize Then _
    Return CType(V, IVertexPointSize).GetPointSize()

  End Function

 End Class

End Namespace
