Partial Class HOD
    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>List of background meshes in this chunk.</summary>
    Private m_BackgroundMeshes As New EventList(Of BasicMesh)

    ''' <summary>Material to use for rendering backgrounds.</summary>
    Private m_BackgroundMaterial As Material

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns the list of background meshes in the HOD.
    ''' </summary>
    Public ReadOnly Property BackgroundMeshes() As IList(Of BasicMesh)
        Get
            Return m_BackgroundMeshes
        End Get
    End Property

    ''' <summary>
    ''' Returns\Sets whether the specified background mesh is visible or not.
    ''' Note that this is only applicable when rendering through this
    ''' object. Individually, the basic meshes can still be rendered.
    ''' </summary>
    ''' <param name="index">
    ''' The Index of the mesh whose visibility is being read\written.
    ''' </param>
    ''' <remarks>
    ''' This is purely a rendering related property. It does not
    ''' affect what is written in the HOD
    ''' </remarks>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when the specified index is out of range.
    ''' </exception>
    Public Property BackgroundMeshVisible(ByVal index As Integer) As Boolean
        Get
            If (index < 0) OrElse (index >= m_BackgroundMeshes.Count) Then _
                Throw New ArgumentOutOfRangeException("index") _
                    : Exit Property

            Return m_BackgroundMeshes(index).Visible
        End Get

        Set(ByVal value As Boolean)
            If (index < 0) OrElse (index >= m_BackgroundMeshes.Count) Then _
                Throw New ArgumentOutOfRangeException("index") _
                    : Exit Property

            m_BackgroundMeshes(index).Visible = value
        End Set
    End Property

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Reads the BGMS chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadBGMSChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        ' Add handlers.
        IFF.AddHandler("BMSH", Homeworld2.IFF.ChunkType.Normal, AddressOf ReadBackgroundMesh, 1400)

        ' Read the file.
        IFF.Parse()
    End Sub

    ''' <summary>
    ''' Writes the BGMS chunk to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteBGMSChunk(ByVal IFF As IFF.IFFWriter)
        If m_BackgroundMeshes.Count = 0 Then _
            Exit Sub

        IFF.Push("BGMS", Homeworld2.IFF.ChunkType.Form)

        For I As Integer = 0 To m_BackgroundMeshes.Count - 1
            ' Set the material index to 0 for all parts.
            For J As Integer = 0 To m_BackgroundMeshes(I).PartCount - 1
                ' Set material index to 0.
                Dim m As Material.Reference = m_BackgroundMeshes(I).Material(J)
                m.Index = 0
                m_BackgroundMeshes(I).Material(J) = m

            Next J ' For J As Integer = 0 To m_BackgroundMeshes(I).PartCount - 1

            ' Write the basic mesh.
            m_BackgroundMeshes(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To m_BackgroundMeshes.Count - 1

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Reads a background mesh chunk (BMSH) from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadBackgroundMesh(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        Dim BasicMesh As New BasicMesh
        BasicMesh.ReadIFF(IFF, ChunkAttributes)
        m_BackgroundMeshes.Add(BasicMesh)
    End Sub

    ''' <summary>
    ''' Writes the BGLT chunk to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteBGLTChunk(ByVal IFF As IFF.IFFWriter)
        IFF.Push("BGLT", Homeworld2.IFF.ChunkType.Normal, 0)
        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Initializes the BGMS chunk.
    ''' </summary>
    Private Sub InitializeBGMS()
        ' Clear background mesh list.
        m_BackgroundMeshes.Clear()

        ' Initialize background material.
        m_BackgroundMaterial = New Material With { _
            .MaterialName = "background", _
            .ShaderName = "background" _
            }
    End Sub

    ''' <summary>
    ''' Updates the BGMS chunk so that it can be rendered properly.
    ''' </summary>
    Private Sub UpdateBGMS(ByVal Device As Direct3D.Device)
        For I As Integer = 0 To m_BackgroundMeshes.Count - 1
            m_BackgroundMeshes(I).SetMaterial(m_BackgroundMaterial)

        Next I ' For I As Integer = 0 To m_BackgroundMeshes.Count - 1
    End Sub

    ''' <summary>
    ''' Locks the meshes so that they can be rendered.
    ''' </summary>
    ''' <param name="Device">
    ''' The device in which the meshes will be rendered.
    ''' </param>
    Private Sub LockBGMS(ByVal Device As Direct3D.Device)
        For I As Integer = 0 To m_BackgroundMeshes.Count - 1
            m_BackgroundMeshes(I).Lock(Device)

        Next I ' For I As Integer = 0 To m_BackgroundMeshes.Count - 1
    End Sub

    ''' <summary>
    ''' Unlocks the meshes so that they can be edited.
    ''' </summary>
    Private Sub UnlockBGMS()
        For I As Integer = 0 To m_BackgroundMeshes.Count - 1
            m_BackgroundMeshes(I).Unlock()

        Next I ' For I As Integer = 0 To m_BackgroundMeshes.Count - 1
    End Sub

    ''' <summary>
    ''' Renders the background, if HOD is a background HOD.
    ''' </summary>
    ''' <param name="Device">
    ''' The device to render to.
    ''' </param>
    ''' <param name="Transform">
    ''' The transform to use.
    ''' </param>
    Private Sub RenderBGMS(ByVal Device As Direct3D.Device, ByVal Transform As Matrix)
        Dim oldLighting As Boolean,
            oldSpecularEnable As Boolean,
            oldAmbient As Drawing.Color,
            oldViewport As Direct3D.Viewport

        ' See if this is a background HOD.
        If m_Version <> 1000 Then _
            Exit Sub

        With Device
            ' Get old states.
            oldLighting = .RenderState.Lighting
            oldSpecularEnable = .RenderState.SpecularEnable
            oldAmbient = .RenderState.Ambient
            oldViewport = .Viewport

            ' Set device states.
            '.Transform.World = Transform
            .RenderState.Lighting = False
            .RenderState.SpecularEnable = False
            .RenderState.Ambient = Drawing.Color.White

            ' Set viewport to render into background.
            Dim v As Direct3D.Viewport = .Viewport
            v.MinZ = 1
            v.MaxZ = 1
            .Viewport = v

        End With ' With Device

        ' Update references.
        UpdateBGMS(Device)

        ' Render all background meshes.
        For I As Integer = 0 To m_BackgroundMeshes.Count - 1
            If m_BackgroundMeshes(I).Visible Then _
                m_BackgroundMeshes(I).Render(Device)

        Next I ' For I As Integer = 0 To m_BackgroundMeshes.Count - 1

        ' Set old states.
        With Device
            .RenderState.Lighting = oldLighting
            .RenderState.SpecularEnable = oldSpecularEnable
            .RenderState.Ambient = oldAmbient
            .Viewport = oldViewport

        End With ' With Device
    End Sub
End Class
