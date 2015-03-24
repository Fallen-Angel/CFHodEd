Imports GenericMesh

Partial Class HOD
    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>List of lights in this chunk.</summary>
    Private m_Lights As New EventList(Of Light)

    ''' <summary>Point light mesh.</summary>
    Private m_LightPointMesh As Standard.BasicMesh

    ''' <summary>Directional light mesh.</summary>
    Private m_LightDirectionalMesh As Standard.BasicMesh

    ''' <summary>List of materials in this chunk.</summary>
    Private WithEvents m_Materials As New EventList(Of Material)

    ''' <summary>List of textures in this chunk.</summary>
    Private WithEvents m_Textures As New EventList(Of Texture)

    ''' <summary>List of multi meshes in this chunk.</summary>
    Private m_MultiMeshes As New EventList(Of MultiMesh)

    ''' <summary>List of goblin meshes in this chunk.</summary>
    Private m_GoblinMeshes As New EventList(Of GoblinMesh)

    ''' <summary>List of simple meshes in this chunk.</summary>
    Private WithEvents m_SimpleMeshes As New EventList(Of SimpleMesh)

    ''' <summary>List of variable meshes in this chunk.</summary>
    Private m_VariableMeshes As New EventList(Of VariableMesh)

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns the list of lights in the HOD.
    ''' </summary>
    Public ReadOnly Property Lights() As IList(Of Light)
        Get
            Return m_Lights
        End Get
    End Property

    ''' <summary>
    ''' Returns the list of materials in the HOD.
    ''' </summary>
    ''' <remarks>
    ''' When a material is removed, all affected references are
    ''' automatically updated.
    ''' </remarks>
    Public ReadOnly Property Materials() As IList(Of Material)
        Get
            Return m_Materials
        End Get
    End Property

    ''' <summary>
    ''' Returns the list of textures in the HOD.
    ''' </summary>
    ''' <remarks>
    ''' When a texture is removed, all affected references are
    ''' automatically updated.
    ''' </remarks>
    Public ReadOnly Property Textures() As IList(Of Texture)
        Get
            Return m_Textures
        End Get
    End Property

    ''' <summary>
    ''' Returns the list of multi meshes in the HOD.
    ''' </summary>
    Public ReadOnly Property MultiMeshes() As IList(Of MultiMesh)
        Get
            Return m_MultiMeshes
        End Get
    End Property

    ''' <summary>
    ''' Returns the list of goblin meshes in the HOD.
    ''' </summary>
    Public ReadOnly Property GoblinMeshes() As IList(Of GoblinMesh)
        Get
            Return m_GoblinMeshes
        End Get
    End Property

    ''' <summary>
    ''' Returns\Sets whether textures are used while rendering, or not.
    ''' However, note that this doesn't stop the class from loading textures.
    ''' </summary>
    ''' <remarks>
    ''' This is purely a rendering related property. It does not
    ''' affect what is written in the HOD
    ''' </remarks>
    Public Shared Property EnableTextures() As Boolean
        Get
            Return Material.Reference.EnableTextures
        End Get

        Set(ByVal value As Boolean)
            Material.Reference.EnableTextures = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the list of simple meshes in the HOD.
    ''' </summary>
    Public ReadOnly Property SimpleMeshes() As IList(Of SimpleMesh)
        Get
            Return m_SimpleMeshes
        End Get
    End Property

    ''' <summary>
    ''' Returns\Sets whether the specified simple mesh is visible or not.
    ''' Note that this is only applicable when rendering through this
    ''' object. Individually, the simple meshes can still be rendered.
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
    Public Property SimpleMeshVisible(ByVal index As Integer) As Boolean
        Get
            If (index < 0) OrElse (index >= m_SimpleMeshes.Count) Then _
                Throw New ArgumentOutOfRangeException("index") _
                    : Exit Property

            Return m_SimpleMeshes(index).Visible
        End Get

        Set(ByVal value As Boolean)
            If (index < 0) OrElse (index >= m_SimpleMeshes.Count) Then _
                Throw New ArgumentOutOfRangeException("index") _
                    : Exit Property

            m_SimpleMeshes(index).Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the list of variable meshes in the HOD.
    ''' </summary>
    Public ReadOnly Property VariableMeshes() As IList(Of VariableMesh)
        Get
            Return m_VariableMeshes
        End Get
    End Property

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Reads the HVMD chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadHVMDChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        ' Add handlers.
        IFF.AddHandler("LITE", Homeworld2.IFF.ChunkType.Default, AddressOf ReadLITEChunk)
        IFF.AddHandler("STAT", Homeworld2.IFF.ChunkType.Normal, AddressOf ReadSTATChunk, 1001)
        IFF.AddHandler("LMIP", Homeworld2.IFF.ChunkType.Default, AddressOf ReadLMIPChunk)
        IFF.AddHandler("MULT", Homeworld2.IFF.ChunkType.Normal, AddressOf ReadMULTChunk, 1400)
        IFF.AddHandler("GOBG", Homeworld2.IFF.ChunkType.Normal, AddressOf ReadGOBGChunk, 1000)
        IFF.AddHandler("SIMP", Homeworld2.IFF.ChunkType.Default, AddressOf ReadSIMPChunk)

        ' Add handlers for compatibility with old HODs.
        IFF.AddHandler("VARY", Homeworld2.IFF.ChunkType.Default, AddressOf ReadVARYChunk)
        IFF.AddHandler("STAT", Homeworld2.IFF.ChunkType.Default, AddressOf ReadSTATChunk)

        ' Read the file.
        IFF.Parse()
    End Sub

    ''' <summary>
    ''' Writes the HVMD chunk to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteHVMDChunk(ByVal IFF As IFF.IFFWriter)
        IFF.Push("HVMD", Homeworld2.IFF.ChunkType.Form)

        If (m_Name = Name_MultiMesh) OrElse
           (m_Name = Name_VariableMesh) Then _
            WriteLITEChunk(IFF) _
                : WriteSTATChunk(IFF) _
                : WriteLMIPChunk(IFF) _
                : WriteMULTChunk(IFF) _
                : WriteGOBGChunk(IFF) _
                : WriteVARYChunk(IFF)

        If (m_Name = Name_SimpleMesh) OrElse
           (m_Name = Name_WireframeMesh) Then _
            WriteSIMPChunk(IFF)

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Reads the LITE chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadLITEChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        ' Get the number of lights.
        Dim LightCount As Integer = IFF.ReadInt32()

        ' Read all lights.
        For I As Integer = 1 To LightCount
            Dim Light As New Light
            Light.ReadIFF(IFF)
            m_Lights.Add(Light)

        Next I ' For I As Integer = 1 To LightCount
    End Sub

    ''' <summary>
    ''' Writes the LITE chunk to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteLITEChunk(ByVal IFF As IFF.IFFWriter)
        If m_Lights.Count <> 0 Then
            IFF.Push("LITE")
            IFF.WriteInt32(m_Lights.Count)

            For I As Integer = 0 To m_Lights.Count - 1
                m_Lights(I).WriteIFF(IFF)

            Next I ' For I As Integer = 0 To m_Lights.Count - 1

            IFF.Pop()

        End If ' If m_Lights.Count <> 0 Then
    End Sub

    ''' <summary>
    ''' Reads the STAT chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadSTATChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        Dim Material As New Material
        Material.ReadIFF(IFF, ChunkAttributes)
        m_Materials.Add(Material)
    End Sub

    ''' <summary>
    ''' Writes all the STAT chunks to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteSTATChunk(ByVal IFF As IFF.IFFWriter)
        For I As Integer = 0 To m_Materials.Count - 1
            m_Materials(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To m_Materials.Count - 1
    End Sub

    ''' <summary>
    ''' Reads the LMIP chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadLMIPChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        Dim Texture As New Texture
        Texture.ReadIFF(IFF, ChunkAttributes)
        m_Textures.Add(Texture)
    End Sub

    ''' <summary>
    ''' Writes all the LMIP chunks to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteLMIPChunk(ByVal IFF As IFF.IFFWriter)
        For I As Integer = 0 To m_Textures.Count - 1
            m_Textures(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To m_Textures.Count - 1
    End Sub

    ''' <summary>
    ''' Reads the MULT chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadMULTChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        Dim MultiMesh As New MultiMesh
        MultiMesh.ReadIFF(IFF, ChunkAttributes)
        m_MultiMeshes.Add(MultiMesh)
    End Sub

    ''' <summary>
    ''' Writes all the MULT chunks to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteMULTChunk(ByVal IFF As IFF.IFFWriter)
        For I As Integer = 0 To m_MultiMeshes.Count - 1
            ' See if the joint exists.
            If m_RootJoint.GetJointByName(m_MultiMeshes(I).ParentName) Is Nothing Then _
                Trace.TraceWarning("The joint referenced by multi mesh " & CStr(I) & " does not exist, changed to root.") _
                    : m_MultiMeshes(I).ParentName = "Root"

            ' Write to file.
            m_MultiMeshes(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To m_MultiMeshes.Count - 1
    End Sub

    ''' <summary>
    ''' Reads the GOBG chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadGOBGChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        Dim GoblinMesh As New GoblinMesh
        GoblinMesh.ReadIFF(IFF, ChunkAttributes)
        m_GoblinMeshes.Add(GoblinMesh)
    End Sub

    ''' <summary>
    ''' Writes all the GOBG chunks to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteGOBGChunk(ByVal IFF As IFF.IFFWriter)
        For I As Integer = 0 To m_GoblinMeshes.Count - 1
            ' See if the joint exists.
            If m_RootJoint.GetJointByName(m_GoblinMeshes(I).ParentName) Is Nothing Then _
                Trace.TraceWarning(
                    "The joint referenced by goblin mesh " & CStr(I) & " does not exist, changed to root.") _
                    : m_GoblinMeshes(I).ParentName = "Root"

            ' Write to file.
            m_GoblinMeshes(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To m_GoblinMeshes.Count - 1
    End Sub

    ''' <summary>
    ''' Reads the SIMP chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadSIMPChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        Dim SimpleMesh As New SimpleMesh
        Dim Wireframe As Boolean = False

        If m_Name = Name_WireframeMesh Then _
            Wireframe = True

        SimpleMesh.ReadIFF(IFF, Wireframe)
        m_SimpleMeshes.Add(SimpleMesh)
    End Sub

    ''' <summary>
    ''' Writes all the SIMP chunks to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteSIMPChunk(ByVal IFF As IFF.IFFWriter)
        For I As Integer = 0 To m_SimpleMeshes.Count - 1
            m_SimpleMeshes(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To m_SimpleMeshes.Count - 1
    End Sub

    ''' <summary>
    ''' Reads the VARY chunk from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="ChunkAttributes">
    ''' Attributes of the chunk.
    ''' </param>
    Private Sub ReadVARYChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        Dim VariableMesh As New VariableMesh
        VariableMesh.ReadIFF(IFF, ChunkAttributes)
        m_VariableMeshes.Add(VariableMesh)
    End Sub

    ''' <summary>
    ''' Writes all the VARY chunks to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Private Sub WriteVARYChunk(ByVal IFF As IFF.IFFWriter)
        For I As Integer = 0 To m_VariableMeshes.Count - 1
            m_VariableMeshes(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To m_VariableMeshes.Count - 1
    End Sub

    ''' <summary>
    ''' Initializes the HVMD chunk.
    ''' </summary>
    Private Sub InitializeHVMD()
        Dim black As New Direct3D.ColorValue(0, 0, 0)

        ' Make light meshes.
        If m_LightPointMesh Is Nothing Then _
            m_LightPointMesh = Standard.BasicMesh.MeshTemplates.CreateSphere(10, black, black)

        If m_LightDirectionalMesh Is Nothing Then _
            m_LightDirectionalMesh = Standard.BasicMesh.MeshTemplates.CreatePrism(100, 1, 1, 1, 10, black, black) _
                : m_LightDirectionalMesh.CreatePrismA(20, 0, 2.5, 1, 10, black, black, RemovePrevData := False) _
                : m_LightDirectionalMesh.Part(1).Translation(0, 0, 60) _
                : m_LightDirectionalMesh.MergeAll()

        ' Unlock meshes.
        UnlockHVMD()

        ' Clear all lists.
        m_Lights.Clear()
        m_Materials.Clear()
        m_Textures.Clear()
        m_MultiMeshes.Clear()
        m_GoblinMeshes.Clear()
        m_SimpleMeshes.Clear()
        m_VariableMeshes.Clear()

        ' Set properties.
        Material.Reference.EnableTextures = True
    End Sub

    ''' <summary>
    ''' Updates the (chunks of the) HVMD chunk so that it can be rendered
    ''' properly.
    ''' </summary>
    Private Sub UpdateHVMD(ByVal Device As Direct3D.Device)
        ' Load textures from memory to device.
        For I As Integer = 0 To m_Textures.Count - 1
            m_Textures(I).Update(Device)

        Next I ' For I As Integer = 0 To m_Textures.Count - 1

        ' Assign textures to materials.
        For I As Integer = 0 To m_Materials.Count - 1
            m_Materials(I).Update(m_Textures)

        Next I ' For I As Integer = 0 To m_Materials.Count - 1

        ' Assign materials to multi meshes.
        For I As Integer = 0 To m_MultiMeshes.Count - 1
            m_MultiMeshes(I).Update(m_Materials)

        Next I ' For I As Integer = 0 To m_MultiMeshes.Count - 1

        ' Assign materials to goblin meshes.
        For I As Integer = 0 To m_GoblinMeshes.Count - 1
            m_GoblinMeshes(I).Update(m_Materials)

        Next I ' For I As Integer = 0 To m_GoblinMeshes.Count - 1
    End Sub

    ''' <summary>
    ''' Locks the meshes so that they can be rendered.
    ''' </summary>
    ''' <param name="Device">
    ''' The device in which the meshes will be rendered.
    ''' </param>
    Private Sub LockHVMD(ByVal Device As Direct3D.Device)
        ' Lock created meshes.
        m_LightPointMesh.Lock(Device)
        m_LightDirectionalMesh.Lock(Device)

        ' Lock multi-meshes.
        For I As Integer = 0 To m_MultiMeshes.Count - 1
            m_MultiMeshes(I).Lock(Device)

        Next I ' For I As Integer = 0 To m_MultiMeshes.Count - 1

        ' Lock goblins.
        For I As Integer = 0 To m_GoblinMeshes.Count - 1
            m_GoblinMeshes(I).Lock(Device)

        Next I ' For I As Integer = 0 To m_GoblinMeshes.Count - 1

        ' Lock simple meshes.
        For I As Integer = 0 To m_SimpleMeshes.Count - 1
            m_SimpleMeshes(I).Lock(Device)

        Next I ' For I As Integer = 0 To m_SimpleMeshes.Count - 1
    End Sub

    ''' <summary>
    ''' Unlocks the meshes so that they can be edited.
    ''' </summary>
    Private Sub UnlockHVMD()
        ' Unlock created meshes.
        m_LightPointMesh.Unlock()
        m_LightDirectionalMesh.Unlock()

        ' Lock multi-meshes.
        For I As Integer = 0 To m_MultiMeshes.Count - 1
            m_MultiMeshes(I).Unlock()

        Next I ' For I As Integer = 0 To m_MultiMeshes.Count - 1

        ' Lock goblins.
        For I As Integer = 0 To m_GoblinMeshes.Count - 1
            m_GoblinMeshes(I).Unlock()

        Next I ' For I As Integer = 0 To m_GoblinMeshes.Count - 1

        ' Lock simple meshes.
        For I As Integer = 0 To m_SimpleMeshes.Count - 1
            m_SimpleMeshes(I).Unlock()

        Next I ' For I As Integer = 0 To m_SimpleMeshes.Count - 1
    End Sub

    ''' <summary>
    ''' Renders the HVMD contents, actual rendered meshes depend on which
    ''' type of HOD this is.
    ''' </summary>
    ''' <param name="Device">
    ''' The device to render to.
    ''' </param>
    ''' <param name="Transform">
    ''' The transform to use.
    ''' </param>
    Private Sub RenderHVMD(ByVal Device As Direct3D.Device, ByVal Transform As Matrix)
        ' Get old states.
        Dim oldLighting As Boolean = Device.RenderState.Lighting,
            oldSpecularEnable As Boolean = Device.RenderState.Lighting,
            oldAmbient As Drawing.Color = Device.RenderState.Ambient

        ' Update references.
        UpdateHVMD(Device)

        ' See if this is a multi mesh HOD.
        If (m_Name = Name_MultiMesh) OrElse (m_Name = Name_VariableMesh) Then _
            RenderHVMDMulti(Device, Transform)

        ' See it this is a simple mesh HOD.
        If (m_Name = Name_SimpleMesh) OrElse (m_Name = Name_WireframeMesh) Then _
            RenderHVMDSimple(Device, Transform)

        ' Render lights.
        For I As Integer = 0 To m_Lights.Count - 1
            m_Lights(I).Render(Device, m_LightPointMesh, m_LightDirectionalMesh)

        Next I ' For I As Integer = 0 To m_Lights.Count - 1

        ' Set old states.
        With Device.RenderState
            .Lighting = oldLighting
            .SpecularEnable = oldSpecularEnable
            .Ambient = oldAmbient

        End With ' With Device.RenderState
    End Sub

    ''' <summary>
    ''' Renders the HVMD assuming the HOD to be a "Homeworld2 Multi Mesh File".
    ''' </summary>
    ''' <param name="Device">
    ''' The device to render to.
    ''' </param>
    Private Sub RenderHVMDMulti(ByVal Device As Direct3D.Device, ByVal Transform As Matrix)
        ' Render multi-meshes.
        For I As Integer = 0 To m_MultiMeshes.Count - 1
            ' Get the joint.
            Dim j As Joint = m_RootJoint.GetJointByName(m_MultiMeshes(I).ParentName)

            ' Use root joint if not available.
            If j Is Nothing Then _
                j = m_RootJoint

            ' Set transform.
            Device.Transform.World = j.Transform*Transform

            ' Load transforms.
            HOD.Render_LoadTransforms(Device)

            ' Render mesh.
            m_MultiMeshes(I).Render(Device)

        Next I ' For I As Integer = 0 To m_MultiMeshes.Count - 1

        ' Render goblins
        For I As Integer = 0 To m_GoblinMeshes.Count - 1
            ' Get the joint.
            Dim j As Joint = m_RootJoint.GetJointByName(m_GoblinMeshes(I).ParentName)

            ' Use root joint if not available.
            If j Is Nothing Then _
                j = m_RootJoint

            ' Set transform.
            Device.Transform.World = j.Transform*Transform

            ' Load transforms.
            HOD.Render_LoadTransforms(Device)

            ' Render mesh.
            m_GoblinMeshes(I).Render(Device)

        Next I ' For I As Integer = 0 To m_GoblinMeshes.Count - 1
    End Sub

    ''' <summary>
    ''' Renders the HVMD assuming the HOD to be a "Homeworld2 Simple Mesh File".
    ''' </summary>
    ''' <param name="Device">
    ''' The device to render to.
    ''' </param>
    Private Sub RenderHVMDSimple(ByVal Device As Direct3D.Device, ByVal Transform As Matrix)
        Dim Wireframe As Boolean = (m_Name = Name_WireframeMesh)

        With Device
            ' Set device states.
            .Transform.World = Transform

            ' For wireframe mode, disable lighting and set ambient to full white.
            If Wireframe Then _
                .RenderState.Lighting = False _
                    : .RenderState.SpecularEnable = False _
                    : .RenderState.Ambient = Drawing.Color.White

        End With ' With Device

        ' Render simple meshes.
        For I As Integer = 0 To m_SimpleMeshes.Count - 1
            If m_SimpleMeshes(I).Visible Then _
                m_SimpleMeshes(I).Render(Device)

        Next I ' For I As Integer = 0 To m_SimpleMeshes.Count - 1
    End Sub
End Class
