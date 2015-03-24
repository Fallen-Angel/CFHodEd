Imports GenericMesh.VertexFields
Imports GenericMesh.MaterialFields

Namespace Utility
    ''' <summary>
    ''' Represents a function which processes (reads\writes) vertices
    ''' in some manner.
    ''' </summary>
    ''' <typeparam name="T">
    ''' Associated data type.
    ''' </typeparam>
    ''' <typeparam name="VertexType">
    ''' Mesh vertex type.
    ''' </typeparam>
    ''' <param name="Data">
    ''' Associated data.
    ''' </param>
    ''' <param name="MeshVertex">
    ''' Mesh's vertices.
    ''' </param>
    ''' <remarks>
    ''' Typically this is intended to read some fields in the mesh's
    ''' vertices and output them, or write some fields in the mesh's
    ''' vertices using input arguments (without losing data of other
    ''' fields). Hence this may be used for all such purposes.
    ''' For processes in which reading\writing at one go is possible,
    ''' use the <c>CopyTo</c> or <c>Append</c> methods instead.
    ''' </remarks>
    Public Delegate Sub VertexProcessor (Of T As {Structure}, VertexType As {Structure, IVertex}) _
        (ByRef Data As T, ByRef MeshVertex As VertexType)

    ''' <summary>
    ''' Module containing default converters (for vertices, indices, materials).
    ''' </summary>
    Public Module Utility
        ''' <summary>
        ''' Transforms the input vertex and returns it.
        ''' </summary>
        ''' <typeparam name="VertexType">
        ''' Type of vertex to transform.
        ''' </typeparam>
        ''' <param name="V">
        ''' Input vertex.
        ''' </param>
        ''' <param name="M">
        ''' Transform matrix.
        ''' </param>
        ''' <returns>
        ''' Transformed vertex.
        ''' </returns>
        ''' <remarks>
        ''' If the vertex implements <c>IVertexTransformable</c>, then
        ''' it is used for transforming the vertex. Otherwise, transformation
        ''' for position and normal is peformed if the respective interfaces
        ''' are implemented. Otherwise, this does nothing.
        ''' </remarks>
        Public Function TransformVertex (Of VertexType As {Structure, IVertex}) _
            (ByVal V As VertexType, ByVal M As Matrix) As VertexType

            ' If the vertex is transformable, then directly transform it.
            If TypeOf V Is IVertexTransformable Then _
                Return CType(CType(V, IVertexTransformable).Transform(M), VertexType)

            ' Since the vertex is not transformable, use the fields to transform manually.
            ' Try transforming the 'Position3' field.
            If TypeOf V Is IVertexPosition3 Then _
                VertexFieldWriter.Position3(V, Vector3.TransformCoordinate(VertexFieldReader.Position3(V), M))

            ' Try transforming the 'Position4' field.
            If (TypeOf V Is IVertexPosition4) AndAlso (Not TypeOf V Is IVertexPosition3) Then _
                VertexFieldWriter.Position4(V, Vector4.Transform(VertexFieldReader.Position4(V), M))

            ' Try transforming the 'Normal3' field.
            VertexFieldWriter.Normal3(V, Vector3.TransformNormal(VertexFieldReader.Normal3(V), M))

            Return V
        End Function

        ''' <summary>
        ''' Converts between vertices of different types.
        ''' </summary>
        ''' <typeparam name="InVertexType">
        ''' Input vertex type.
        ''' </typeparam>
        ''' <typeparam name="OutVertexType">
        ''' Output vertex type.
        ''' </typeparam>
        ''' <param name="V">
        ''' Input value.
        ''' </param>
        ''' <returns>
        ''' Output value.
        ''' </returns>
        ''' <remarks>
        ''' This performs a field-by-field copy after initializing.
        ''' </remarks>
        Public Function VertexConverter (Of InVertexType As {Structure, IVertex}, _
            OutVertexType As {Structure, IVertex}) _
            (ByVal V As InVertexType) As OutVertexType
            Dim OutVtx As New OutVertexType

            ' See if the types are same.
            If GetType(InVertexType) Is GetType(OutVertexType) Then _
                Return CType(CObj(V), OutVertexType)

            ' Initialize the vertex.
            OutVtx.Initialize()

            ' Set 'Position3' field.
            If (TypeOf OutVtx Is IVertexPosition3) AndAlso (TypeOf OutVtx Is IVertexPosition3) Then _
                VertexFieldWriter.Position3(OutVtx, VertexFieldReader.Position3(V))

            ' Set 'Position4' field.
            If (TypeOf OutVtx Is IVertexPosition4) AndAlso (TypeOf OutVtx Is IVertexPosition4) Then _
                VertexFieldWriter.Position4(OutVtx, VertexFieldReader.Position4(V))

            ' Set 'Normal3' field.
            If (TypeOf OutVtx Is IVertexNormal3) AndAlso (TypeOf OutVtx Is IVertexNormal3) Then _
                VertexFieldWriter.Normal3(OutVtx, VertexFieldReader.Normal3(V))

            ' Set 'Tex1' field(s).
            If (TypeOf OutVtx Is IVertexTex1) AndAlso (TypeOf OutVtx Is IVertexTex1) Then
                For I As Integer = 0 To 7
                    VertexFieldWriter.TexCoords1(OutVtx, VertexFieldReader.TexCoords1(V, I), I)

                Next I ' For I As Integer = 0 To 7

            End If ' If (TypeOf OutVtx Is IVertexTex1) AndAlso (TypeOf OutVtx Is IVertexTex1) Then

            ' Set 'Tex2' field(s).
            If (TypeOf OutVtx Is IVertexTex2) AndAlso (TypeOf OutVtx Is IVertexTex2) Then
                For I As Integer = 0 To 7
                    VertexFieldWriter.TexCoords2(OutVtx, VertexFieldReader.TexCoords2(V, I), I)

                Next I ' For I As Integer = 0 To 7

            End If ' If (TypeOf OutVtx Is IVertexTex2) AndAlso (TypeOf OutVtx Is IVertexTex2) Then

            ' Set 'Tex3' field(s).
            If (TypeOf OutVtx Is IVertexTex3) AndAlso (TypeOf OutVtx Is IVertexTex3) Then
                For I As Integer = 0 To 7
                    VertexFieldWriter.TexCoords3(OutVtx, VertexFieldReader.TexCoords3(V, I), I)

                Next I ' For I As Integer = 0 To 7

            End If ' If (TypeOf OutVtx Is IVertexTex3) AndAlso (TypeOf OutVtx Is IVertexTex3) Then

            ' Set 'Tex4' field(s).
            If (TypeOf OutVtx Is IVertexTex4) AndAlso (TypeOf OutVtx Is IVertexTex4) Then
                For I As Integer = 0 To 7
                    VertexFieldWriter.TexCoords4(OutVtx, VertexFieldReader.TexCoords4(V, I), I)

                Next I ' For I As Integer = 0 To 7

            End If ' If (TypeOf OutVtx Is IVertexTex4) AndAlso (TypeOf OutVtx Is IVertexTex4) Then

            ' Set 'Diffuse' field.
            If (TypeOf OutVtx Is IVertexDiffuse) AndAlso (TypeOf OutVtx Is IVertexDiffuse) Then _
                VertexFieldWriter.Diffuse(OutVtx, VertexFieldReader.Diffuse(V))

            ' Set 'Specular' field.
            If (TypeOf OutVtx Is IVertexSpecular) AndAlso (TypeOf OutVtx Is IVertexSpecular) Then _
                VertexFieldWriter.Specular(OutVtx, VertexFieldReader.Specular(V))

            ' Set 'PointSize' field.
            If (TypeOf OutVtx Is IVertexPointSize) AndAlso (TypeOf OutVtx Is IVertexPointSize) Then _
                VertexFieldWriter.PointSize(OutVtx, VertexFieldReader.PointSize(V))

            Return OutVtx
        End Function

        ''' <summary>
        ''' Converts between indices of different types.
        ''' </summary>
        ''' <typeparam name="InIndiceType">
        ''' Input indice type.
        ''' </typeparam>
        ''' <typeparam name="OutIndiceType">
        ''' Output indice type.
        ''' </typeparam>
        ''' <param name="V">
        ''' Input value.
        ''' </param>
        ''' <returns>
        ''' Output value.
        ''' </returns>
        ''' <remarks>
        ''' System types only.
        ''' </remarks>
        Public Function IndiceConverter (Of InIndiceType As {Structure, IConvertible}, _
            OutIndiceType As {Structure, IConvertible}) _
            (ByVal V As InIndiceType) As OutIndiceType

            ' See if the types are same.
            If GetType(InIndiceType) Is GetType(OutIndiceType) Then _
                Return CType(CObj(V), OutIndiceType)

            ' Convert and return value.
            Return CType(Convert.ChangeType(V, GetType(OutIndiceType)), OutIndiceType)
        End Function

        ''' <summary>
        ''' Converts between materials of different types.
        ''' </summary>
        ''' <typeparam name="InMaterialType">
        ''' Input material type.
        ''' </typeparam>
        ''' <typeparam name="OutMaterialType">
        ''' Output material type.
        ''' </typeparam>
        ''' <param name="V">
        ''' Input value.
        ''' </param>
        ''' <returns>
        ''' Converted value.
        ''' </returns>
        ''' <remarks>
        ''' This performs a field-by-field copy after initializing.
        ''' </remarks>
        Public Function MaterialConverter (Of InMaterialType As {Structure, IMaterial}, _
            OutMaterialType As {Structure, IMaterial}) _
            (ByVal V As InMaterialType) As OutMaterialType

            Dim OutMat As New OutMaterialType

            ' See if the types are same.
            If GetType(InMaterialType) Is GetType(OutMaterialType) Then _
                Return CType(CObj(V), OutMaterialType)

            ' Initialize the material.
            OutMat.Initialize()

            ' Set 'Name' field.
            If (TypeOf V Is IMaterialName) AndAlso (TypeOf OutMat Is IMaterialName) Then _
                MaterialFieldWriter.MaterialName(OutMat, MaterialFieldReader.MaterialName(V))

            ' Set 'Texture' field.
            If (TypeOf V Is IMaterialTexture) AndAlso (TypeOf OutMat Is IMaterialTexture) Then _
                MaterialFieldWriter.TextureName(OutMat, MaterialFieldReader.TextureName(V))

            ' Set 'Attributes' field.
            If (TypeOf V Is IMaterialAttributes) AndAlso (TypeOf OutMat Is IMaterialAttributes) Then _
                MaterialFieldWriter.Attributes(OutMat, MaterialFieldReader.Attributes(V))

            Return OutMat
        End Function
    End Module
End Namespace
