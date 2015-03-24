Namespace WavefrontObjectHelper
    ''' <summary>
    ''' Class representing the data in the wavefront object file.
    ''' </summary>
    ''' <remarks>
    ''' For converting data.
    ''' </remarks>
    Friend Class ObjFileDataStoreConverter
        ''' <summary>
        ''' Parts, representing various materials.
        ''' </summary>
        ''' <remarks></remarks>
        Dim m_Parts() As ObjFileDataStoreConverterPart

        ''' <summary>
        ''' Class constructor.
        ''' </summary>
        ''' <param name="_ObjFile">
        ''' The reference wavefront object file.
        ''' </param>
        Public Sub New(ByVal _ObjFile As ObjFileDataStore)
            ' Check the length of object file.
            If _ObjFile Is Nothing Then _
                Throw New ArgumentNullException("_ObjFile") _
                    : Exit Sub

            ' Resize the parts array.
            ReDim m_Parts(_ObjFile.Mat.Count - 1)

            ' Initialize each part.
            For I As Integer = 0 To _ObjFile.Mat.Count - 1
                m_Parts(I) = New ObjFileDataStoreConverterPart(_ObjFile, I)

            Next I ' For I As Integer = 0 To _ObjFile.Mat.Count - 1

            ' Now process all indices.
            For I As Integer = 0 To _ObjFile.Ind.Count - 1
                ' Get the indice group.
                Dim Ind As IndiceGroup = _ObjFile.Ind(I)

                ' Add it in the proper part.
                m_Parts(Ind.Material).AddIndices(I)

            Next I ' For I As Integer = 0 To _ObjFile.Ind.Count - 1
        End Sub

        ''' <summary>
        ''' Class destructor.
        ''' </summary>
        Protected Overrides Sub Finalize()
            ' Free all parts.
            For I As Integer = 0 To UBound(m_Parts)
                m_Parts(I) = Nothing

            Next I ' For I As Integer = 0 To UBound(m_Parts)

            ' Free the array.
            m_Parts = Nothing

            ' Call base destructor.
            MyBase.Finalize()
        End Sub

        ''' <summary>
        ''' Copies the data in this object to a mesh.
        ''' </summary>
        ''' <typeparam name="VertexType">
        ''' Vertex format of the output mesh.
        ''' </typeparam>
        ''' <typeparam name="IndiceType">
        ''' Indice format of the output mesh.
        ''' </typeparam>
        ''' <typeparam name="MaterialType">
        ''' Material format of the output mesh.
        ''' </typeparam>
        ''' <param name="OutMesh">
        ''' The mesh to which copy to.
        ''' </param>
        ''' <exception cref="ArgumentNullException">
        ''' Thrown when <c>OutPart Is Nothing</c>.
        ''' </exception>
        Public Sub CopyTo (Of VertexType As {Structure, IVertex}, _
            IndiceType As {Structure, IConvertible}, _
            MaterialType As {Structure, IMaterial}) _
            (ByVal OutMesh As GBasicMesh(Of VertexType, IndiceType, MaterialType))
            ' Check inputs.
            If OutMesh Is Nothing Then _
                Throw New ArgumentNullException("OutMesh") _
                    : Exit Sub

            ' Set the part count.
            OutMesh.PartCount = m_Parts.Length

            ' Copy each part.
            For I As Integer = 0 To UBound(m_Parts)
                m_Parts(I).CopyTo(OutMesh.Part(I))

            Next I ' For I As Integer = 0 To UBound(m_Parts)

            ' Remove unused parts.
            For I As Integer = UBound(m_Parts) To 0 Step - 1
                If (OutMesh.Part(I).Vertices.Count = 0) OrElse
                   (OutMesh.Part(I).PrimitiveGroupCount = 0) Then _
                    OutMesh.Remove(I)

            Next I ' For I As Integer = UBound(m_Parts) To 0 Step -1
        End Sub
    End Class
End Namespace
