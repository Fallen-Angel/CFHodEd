Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D
Imports GenericMesh.VertexFields
Imports GenericMesh.MaterialFields
Imports GenericMesh.Translators.WavefrontObjectHelper

''' <summary>
''' Module which implements wavefront object translator.
''' </summary>
''' <remarks>
''' NOTE: The reader\writer are loosely implemented. Not 
''' all features of the wavefront object specification are
''' implemented, and some are added as a convinience. There
''' are some implementations which do not exist.
''' </remarks>
Public Class WavefrontObject
    ''' <summary>
    ''' Reads a wavefront object file.
    ''' </summary>
    ''' <typeparam name="VertexType">
    ''' Vertex format of the mesh.
    ''' </typeparam>
    ''' <typeparam name="IndiceType">
    ''' Indice format of the mesh.
    ''' </typeparam>
    ''' <typeparam name="MaterialType">
    ''' Material type of the mesh.
    ''' </typeparam>
    ''' <param name="FileName">
    ''' The name of file to read.
    ''' </param>
    ''' <param name="OutMesh">
    ''' The mesh to which the file is read.
    ''' </param>
    ''' <exception cref="ArgumentException">
    ''' Thrown when file cannot be opened due to some reason.
    ''' </exception>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>Filename Is Nothing</c> or <c>Filename = ""</c>.
    ''' Also thrown when <c>OutMesh Is Nothing</c>.
    ''' </exception>
    Public Shared Sub ReadFile (Of VertexType As {Structure, IVertex}, _
        IndiceType As {Structure, IConvertible}, _
        MaterialType As {Structure, IMaterial}) _
        (ByVal Filename As String,
         ByVal OutMesh As GBasicMesh(Of VertexType, IndiceType, MaterialType),
         Optional ByVal ReverseUVs As Boolean = False)

        Dim ObjFile As New ObjFileDataStore,
            ObjFile2 As ObjFileDataStoreConverter

        ' -------------------------
        ' Attempt to read the file.
        ' -------------------------
        Try
            ' Read the file.
            ObjFile.ReadFile(Filename)

        Catch ex As Exception
            ' Free the object.
            ObjFile = Nothing

            ' Pass exception.
            Throw New Exception("Cannot read file: '" & Filename & "'.", ex)
            Exit Sub

        End Try

        ' ----------------------
        ' Reverse UVs if needed.
        ' ----------------------
        If ReverseUVs Then
            For I As Integer = 0 To ObjFile.UV.Count - 1
                ' Flip U and set UV.
                ObjFile.UV(I) = New Vector2(ObjFile.UV(I).X, 1.0F - ObjFile.UV(I).Y)

            Next I ' For I As Integer = 0 To ObjFile.UV.Count - 1
        End If ' If ReverseUVs Then

        ' -------------------------
        ' Initialize the converter.
        ' -------------------------
        ObjFile2 = New ObjFileDataStoreConverter(ObjFile)

        ' --------------------------------------------
        ' Convert the file into an appropriate format.
        ' --------------------------------------------
        ObjFile2.CopyTo(OutMesh)

        ' -----------------
        ' Free the objects.
        ' -----------------
        ObjFile = Nothing
        ObjFile2 = Nothing
    End Sub

    ''' <summary>
    ''' Writes a wavefront object material file.
    ''' </summary>
    ''' <param name="Filename">
    ''' The name of the file to which to write..
    ''' </param>
    ''' <param name="Mesh">
    ''' The object whose materials are to be written.
    ''' </param>
    ''' <exception cref="Exception">
    ''' Thrown when file cannot be opened due to some reason.
    ''' </exception>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>Filename Is Nothing</c> or <c>Filename = ""</c>.
    ''' Also thrown when <c>OutObjData Is Nothing</c>.
    ''' </exception>
    Private Shared Sub WriteMtlFile (Of VertexType As {Structure, IVertex}, _
        IndiceType As {Structure, IConvertible}, _
        MaterialType As {Structure, IMaterial}) _
        (ByVal Filename As String,
         ByVal Mesh As GBasicMesh(Of VertexType, IndiceType, MaterialType))
        Dim F As IO.StreamWriter

        Try
            ' Try to open the file.
            F = My.Computer.FileSystem.OpenTextFileWriter(Filename, False)

        Catch ex As Exception
            ' Throw an exception.
            Throw New Exception("Coudn't open file for writing: '" & Filename & "'.", ex)
            Exit Sub

        End Try

        ' Write each material.
        For I As Integer = 0 To Mesh.PartCount - 1
            Dim S1 As String, S2 As String
            Dim Mtl As Direct3D.Material

            With Mesh.Part(I)
                ' Retrieve the material name.
                S1 = MaterialFieldReader.MaterialName(.Material)

                If S1 = "" Then _
                    S1 = "Material_" & CStr(I)

                ' Retrieve texture name
                If TypeOf .Material Is IMaterialTexture Then _
                    S2 = MaterialFieldReader.TextureName(.Material) _
                    Else _
                    S2 = ""

                ' Retrieve the material.
                If TypeOf .Material Is IMaterialAttributes Then _
                    Mtl = MaterialFieldReader.Attributes(.Material) _
                    Else _
                    Mtl.AmbientColor = New ColorValue(0.2, 0.2, 0.2) _
                        : Mtl.DiffuseColor = New ColorValue(0.8, 0.8, 0.8)

            End With ' With Mesh.Part(I)

            ' Write the material name and it's attributes.
            F.WriteLine("newmtl " & S1)
            F.WriteLine("illum 2")

            With Mtl.Diffuse
                F.WriteLine("Kd " & _SngToStr(.R) & " " & _SngToStr(.G) & " " & _SngToStr(.B))
            End With ' With Mtl.Diffuse

            With Mtl.Ambient
                F.WriteLine("Ka " & _SngToStr(.R) & " " & _SngToStr(.G) & " " & _SngToStr(.B))
            End With ' With Mtl.Diffuse

            With Mtl.Specular
                F.WriteLine("Ks " & _SngToStr(.R) & " " & _SngToStr(.G) & " " & _SngToStr(.B))
            End With ' With Mtl.Diffuse

            With Mtl.Emissive
                F.WriteLine("Ke " & _SngToStr(.R) & " " & _SngToStr(.G) & " " & _SngToStr(.B))
            End With ' With Mtl.Diffuse

            F.WriteLine("Ns " & CStr(Mtl.SpecularSharpness))

            ' Write texture name if present.
            If Len(S2) <> 0 Then _
                F.WriteLine("map_Kd " & S2)

            F.WriteLine()

        Next I ' For I As Integer = 0 To Mesh.PartCount - 1

        ' Close the file.
        F.Close()
    End Sub

    ''' <summary>
    ''' Writes a wavefront object file.
    ''' </summary>
    ''' <typeparam name="VertexType">
    ''' Vertex format of the mesh.
    ''' </typeparam>
    ''' <typeparam name="IndiceType">
    ''' Indice format of the mesh.
    ''' </typeparam>
    ''' <typeparam name="MaterialType">
    ''' Material type of the mesh.
    ''' </typeparam>
    ''' <param name="FileName">
    ''' The name of file to write.
    ''' </param>
    ''' <param name="Mesh">
    ''' The mesh whose data is written to the file.
    ''' </param>
    ''' <exception cref="ArgumentException">
    ''' Thrown when file cannot be opened due to some reason.
    ''' </exception>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>Filename Is Nothing</c> or <c>Filename = ""</c>.
    ''' Also thrown when <c>OutMesh Is Nothing</c>.
    ''' </exception>
    Public Shared Sub WriteFile (Of VertexType As {Structure, IVertex}, _
        IndiceType As {Structure, IConvertible}, _
        MaterialType As {Structure, IMaterial}) _
        (ByVal Filename As String,
         ByVal Mesh As GBasicMesh(Of VertexType, IndiceType, MaterialType),
         Optional ByVal BreakIntoGroups As Boolean = True,
         Optional ByVal WriteUVs As Boolean = True,
         Optional ByVal WriteNormals As Boolean = True,
         Optional ByVal WriteColourData As Boolean = False,
         Optional ByVal ReverseUVs As Boolean = False)
        Dim Offset As Integer, MtlFilename As String
        Dim F As System.IO.StreamWriter

        ' -------------------------
        ' Check for invalid inputs.
        ' -------------------------
        ' Check input filename.
        If Filename Is Nothing Or Filename = "" Then _
            Throw New ArgumentNullException("Filename") _
                : Exit Sub

        ' Check input mesh.
        If Mesh Is Nothing Then _
            Throw New ArgumentNullException("Mesh") _
                : Exit Sub

        ' ----------------------------
        ' See if we can open the file.
        ' ----------------------------
        Try
            ' Try to open the file.
            F = My.Computer.FileSystem.OpenTextFileWriter(Filename, False)

        Catch ex As Exception
            ' Throw an exception.
            Throw New Exception("Coudn't open file for writing: '" & Filename & "'.", ex)
            Exit Sub

        End Try

        ' ------------------------------------
        ' Write the material and object files.
        ' ------------------------------------
        ' Retrieve material filename.
        MtlFilename = IO.Path.ChangeExtension(Filename, ".mtl")

        ' Write the material file.
        WriteMtlFile(MtlFilename, Mesh)

        ' Write the material library name.
        F.WriteLine("mtllib " & IO.Path.GetFileName(MtlFilename))
        F.WriteLine()

        ' Write the group name, either now, or later as needed.
        If Not BreakIntoGroups Then _
            F.WriteLine("g Mesh") _
                : F.WriteLine()

        ' Set offset to 1.
        Offset = 1

        ' Write the data in all parts.
        For I As Integer = 0 To Mesh.PartCount - 1
            Dim S1 As String = "", S2 As String = ""
            Dim Mtl As Direct3D.Material

            With Mesh.Part(I)
                ' Retrieve the material name.
                S1 = MaterialFieldReader.MaterialName(.Material)

                If S1 = "" Then _
                    S1 = "Material_" & CStr(I)

                ' Retrieve the material.
                Mtl = MaterialFieldReader.Attributes(.Material)
            End With ' With Mesh.Part(I)

            ' Write the group name (if needed).
            If BreakIntoGroups Then _
                F.WriteLine("g " & S1)

            ' Write the material name.
            F.WriteLine("usemtl " & S1)
            F.WriteLine()

            ' Now write the vertex data.
            For J As Integer = 0 To Mesh.Part(I).Vertices.Count - 1
                Dim V As VertexType, P, N As Vector3
                Dim TC As Vector2, C As ColorValue

                ' Retrieve the vertex.
                V = Mesh.Part(I).Vertices(J)

                ' Write the position.
                If TypeOf V Is IVertexPosition3 Then _
                    P = VertexFieldReader.Position3(V) _
                        : F.WriteLine("v " & _SngToStr(P.X) & " " & _SngToStr(P.Y) & " " & _SngToStr(P.Z))

                ' Write the texture co-ordinates, if needed.
                If TypeOf V Is IVertexTex2 Then _
                    TC = VertexFieldReader.TexCoords2(V) _
                        : If WriteUVs Then _
                            F.WriteLine(
                                "vt " & _SngToStr(TC.X) & " " & _SngToStr(CSng(IIf(ReverseUVs, 1.0F - TC.Y, TC.Y))))

                ' Write the vertex normal, if needed.
                If TypeOf V Is IVertexNormal3 Then _
                    N = VertexFieldReader.Normal3(V) _
                        : If WriteNormals Then _
                            F.WriteLine("vn " & _SngToStr(N.X) & " " & _SngToStr(N.Y) & " " & _SngToStr(N.Z))

                ' Write the diffuse colour, if needed.
                If TypeOf V Is IVertexDiffuse Then _
                    C = VertexFieldReader.Diffuse(V) _
                        : If WriteColourData Then _
                            F.WriteLine("vc " & _SngToStr(CInt(255*C.Red)) & " " & _SngToStr(CInt(255*C.Green)) & " " &
                                        _SngToStr(CInt(255*C.Blue)) & " " & _SngToStr(CInt(255*C.Alpha)))

                ' One empty line acts as a good seperator.
                F.WriteLine()

            Next J ' For J As Integer = 0 To Mesh.Part(I).Vertices.Count - 1

            F.WriteLine("# " & Mesh.Part(I).Vertices.Count & " vertice(s).")
            F.WriteLine()

            For J As Integer = 0 To Mesh.Part(I).PrimitiveGroupCount - 1
                Dim P As New GPrimitiveGroup(Of IndiceType)

                ' Convert the indices.
                Mesh.Part(I).PrimitiveGroups(J).CopyTo(P)

                ' Convert the duplicated primitive group to list.
                P.ConvertToList()

                ' Write the indices.
                For K As Integer = 0 To P.IndiceCount - 1 Step P.IndicesPerPrimitive
                    S1 = ""

                    ' Build the group.
                    For L As Integer = 0 To P.IndicesPerPrimitive - 1
                        Dim Ind As Integer = P.Indice(K + L).ToInt32(Nothing)

                        ' Get the indice value.
                        S2 = CStr(Offset + Ind)

                        ' Write the position, UV, normal indices as needed.
                        S1 &= " " & S2 & "/" &
                              CStr(IIf(WriteUVs, S2, "")) & "/" &
                              CStr(IIf(WriteNormals, S2, ""))

                    Next L ' For L As Integer = 0 To P.IndicesPerPrimitive - 1

                    ' Don't forget to prefix "f".
                    S1 = "f" & S1

                    ' Write the face data.
                    F.WriteLine(S1)

                Next K ' For K As Integer = 0 To P.IndiceCount - 1 Step P.IndicesPerPrimitive

                ' Write the indice & primitive count.
                F.WriteLine("# " & CStr(P.IndiceCount) & " indices; " &
                            CStr(P.PrimitiveCount) & " primitives.")
                F.WriteLine()

            Next J ' For J As Integer = 0 To Mesh.Part(I).PrimitiveGroupCount - 1

            ' Increment the offset.
            Offset += Mesh.Part(I).Vertices.Count

            ' Write the primitive group count.
            F.WriteLine("# " & CStr(Mesh.Part(I).PrimitiveGroupCount) & " primitive groups.")
            F.WriteLine()

        Next I ' For I As Integer = 0 To Mesh.PartCount - 1

        ' Write the part group count.
        F.WriteLine("# " & CStr(Mesh.PartCount) & " parts.")
        F.WriteLine()

        ' ---------------
        ' Close the file.
        ' ---------------
        F.Close()
    End Sub

    ''' <summary>Formatting for input\output.</summary>
    Friend Shared ReadOnly _FormatProvider As IFormatProvider = Globalization.CultureInfo.CreateSpecificCulture("en-US")

    ''' <summary>
    ''' Converts single to string.
    ''' </summary>
    Private Shared Function _SngToStr(ByVal sng As Single) As String
        Return sng.ToString(_FormatProvider)

        ' Return CStr(sng)
    End Function
End Class
