''' <summary>
''' Masks for basic mesh vertex.
''' </summary>
Public Enum VertexMasks
 Position = &H1
 Normal = &H2
 Colour = &H4
 Texture0 = &H8
 Texture1 = &H10  ' HW2 writes 'Texture0' into this according to plugin source code
 Texture2 = &H20  ' HW2 writes 'Texture0' into this according to plugin source code
 Texture3 = &H40  ' HW2 writes 'Texture0' into this according to plugin source code
 Texture4 = &H80  ' HW2 writes 'Texture0' into this according to plugin source code
 Texture5 = &H100 ' HW2 writes 'Texture0' into this according to plugin source code
 Texture6 = &H200 ' HW2 writes 'Texture0' into this according to plugin source code
 Texture7 = &H400 ' HW2 writes 'Texture0' into this according to plugin source code
 HVertex = &H800  ' ???
 SVertex = &H1000 ' ???
 Tangent = &H2000
 Binormal = &H4000

 All = Position Or Normal Or Colour Or Texture0 Or _
       Texture1 Or Texture2 Or Texture3 Or Texture4 Or _
       Texture5 Or Texture6 Or Texture7 Or HVertex Or _
       SVertex Or Tangent Or Binormal

End Enum
