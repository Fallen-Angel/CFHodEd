Partial Class HOD
    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Team colour.</summary>
    Private m_TeamColour As New Direct3D.ColorValue(0.5F, 0.5F, 0.5F)

    ''' <summary>Stripe colour.</summary>
    Private m_StripeColour As New Direct3D.ColorValue(0.5F, 0.5F, 0.5F)

    ''' <summary>Thruster power.</summary>
    Private m_ThrusterPower As Single = 0

    ''' <summary>Location of texture containing badge.</summary>
    Private m_Badge As String = ""

    ''' <summary>Badge texture.</summary>
    Private m_BadgeTexture As Direct3D.Texture

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns\Sets the team colour of ship (if applicable).
    ''' </summary>
    ''' <remarks>
    ''' Team\Stripe colour effects require support of pixel shader 1.4.
    ''' If no support is available, team\stripe colour effects will not be
    ''' visible.
    ''' Also, this is purely a rendering related property. It does not
    ''' affect what is written in the HOD.
    ''' </remarks>
    Public Property TeamColour() As Direct3D.ColorValue
        Get
            Return m_TeamColour
        End Get

        Set(ByVal value As Direct3D.ColorValue)
            m_TeamColour = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets the stripe colour of ship (if applicable).
    ''' </summary>
    ''' <remarks>
    ''' Team\Stripe colour effects require support of pixel shader 1.4.
    ''' If no support is available, team\stripe colour effects will not be
    ''' visible.
    ''' Also, this is purely a rendering related property. It does not
    ''' affect what is written in the HOD.
    ''' </remarks>
    Public Property StripeColour() As Direct3D.ColorValue
        Get
            Return m_StripeColour
        End Get

        Set(ByVal value As Direct3D.ColorValue)
            m_StripeColour = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets the thruster power of ship's thruster engines (if applicable).
    ''' </summary>
    ''' <remarks>
    ''' Should be in the range of [0.0, 1.0] (otherwise it will be clamped), with
    ''' 0 indicating off texture and 1 indicating on texture. Also, this effect
    ''' requires support of pixel shader 1.4. If no support is available, then this
    ''' effect will not be visible.
    ''' Also, this is purely a rendering related property. It does not
    ''' affect what is written in the HOD.
    ''' </remarks>
    Public Property ThrusterPower() As Single
        Get
            Return m_ThrusterPower
        End Get

        Set(ByVal value As Single)
            m_ThrusterPower = Math.Max(0.0F, Math.Min(1.0F, value))
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets location of texture containing badge.
    ''' </summary>
    ''' <remarks>
    ''' This is purely a rendering related property. It does not
    ''' affect what is written in the HOD.
    ''' </remarks>
    Public Property Badge() As String
        Get
            Return m_Badge
        End Get

        Set(ByVal value As String)
            m_Badge = value
            m_BadgeTexture = Nothing
        End Set
    End Property

    ''' <summary>
    ''' Returns the badge texture.
    ''' </summary>
    Friend ReadOnly Property BadgeTexture() As Direct3D.Texture
        Get
            Return m_BadgeTexture
        End Get
    End Property

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Finalizes rendering related members.
    ''' </summary>
    Private Sub FinalizeRender()
        ' Release the badge texture.
        If m_BadgeTexture IsNot Nothing Then _
            m_BadgeTexture.Dispose() _
                : m_BadgeTexture = Nothing
    End Sub

    ''' <summary>
    ''' Updates rendering related parameters of the HOD.
    ''' </summary>
    Private Sub UpdateRender(ByVal Device As Direct3D.Device)
        ' If we already have a texture then do nothing.
        If m_BadgeTexture IsNot Nothing Then _
            Exit Sub

        ' Try to load the badge texture...
        Try
            If (m_Badge <> "") AndAlso (IO.File.Exists(m_Badge)) Then _
                m_BadgeTexture = Direct3D.TextureLoader.FromFile(Device, m_Badge)

        Catch ex As Exception
            ' Do nothing.

        End Try
    End Sub

    ''' <summary>
    ''' Locks the meshes so that they can be rendered.
    ''' </summary>
    ''' <param name="Device">
    ''' The device in which the meshes will be rendered.
    ''' </param>
    ''' <remarks>
    ''' Not locking meshes before rendering will cause an exception.
    ''' </remarks>
    Public Sub LockMeshes(ByVal Device As Direct3D.Device)
        LockBGMS(Device)
        LockHVMD(Device)
        LockDTRM(Device)
    End Sub

    ''' <summary>
    ''' Unlocks the meshes so that they can be edited.
    ''' </summary>
    ''' <remarks>
    ''' Meshes must be unlocked even when they are being written
    ''' to a file.
    ''' </remarks>
    Public Sub UnlockMeshes()
        UnlockBGMS()
        UnlockHVMD()
        UnlockDTRM()
    End Sub

    ''' <summary>
    ''' Loads the current transform shader constants.
    ''' </summary>
    Friend Shared Sub Render_LoadTransforms(ByVal Device As Direct3D.Device)
        With Device
            Dim WorldViewMatrix As Matrix = .Transform.World*.Transform.View
            Dim WorldViewProjMatrix As Matrix = WorldViewMatrix*.Transform.Projection

            .SetVertexShaderConstant(0, .Transform.World)
            .SetVertexShaderConstant(4, Matrix.TransposeMatrix(.Transform.World))

            .SetVertexShaderConstant(8, .Transform.View)
            .SetVertexShaderConstant(12, Matrix.TransposeMatrix(.Transform.View))

            .SetVertexShaderConstant(16, WorldViewProjMatrix)
            .SetVertexShaderConstant(20, Matrix.TransposeMatrix(WorldViewProjMatrix))

            .SetVertexShaderConstant(24, WorldViewMatrix)
            .SetVertexShaderConstant(28, Matrix.Invert(WorldViewMatrix))

        End With ' With Device
    End Sub

    ''' <summary>
    ''' Loads the shader contants.
    ''' </summary>
    Private Sub Render_LoadShaderConstants(ByVal Device As Direct3D.Device, ByVal CameraPosition As Vector3)
        Render_LoadTransforms(Device)

        With Device
            .SetVertexShaderConstant(32, New Vector4(CameraPosition.X, CameraPosition.Y, CameraPosition.Z, 1.0F))
            .SetVertexShaderConstant(44,
                                     New Vector4(.RenderState.Ambient.R, .RenderState.Ambient.G, .RenderState.Ambient.B,
                                                 .RenderState.Ambient.A))

            For I As Integer = 0 To 7
                .SetVertexShaderConstant(48 + 6*I,
                                         New Vector4(.Lights(I).Position.X, .Lights(I).Position.Y, .Lights(I).Position.Z,
                                                     1.0F))
                .SetVertexShaderConstant(49 + 6*I,
                                         New Vector4(.Lights(I).Direction.X, .Lights(I).Direction.Y,
                                                     .Lights(I).Direction.Z, 1.0F))

                If .Lights(I).Enabled Then
                    .SetVertexShaderConstant(50 + 6*I,
                                             New Vector4(.Lights(I).AmbientColor.Red, .Lights(I).AmbientColor.Green,
                                                         .Lights(I).AmbientColor.Blue, .Lights(I).AmbientColor.Alpha))
                    .SetVertexShaderConstant(51 + 6*I,
                                             New Vector4(.Lights(I).DiffuseColor.Red, .Lights(I).DiffuseColor.Green,
                                                         .Lights(I).DiffuseColor.Blue, .Lights(I).DiffuseColor.Alpha))
                    .SetVertexShaderConstant(52 + 6*I,
                                             New Vector4(.Lights(I).SpecularColor.Red, .Lights(I).SpecularColor.Green,
                                                         .Lights(I).SpecularColor.Blue, .Lights(I).SpecularColor.Alpha))

                Else ' If .Lights(I).Enabled Then
                    .SetVertexShaderConstant(50 + 6*I, New Vector4(0, 0, 0, 0))
                    .SetVertexShaderConstant(51 + 6*I, New Vector4(0, 0, 0, 0))
                    .SetVertexShaderConstant(52 + 6*I, New Vector4(0, 0, 0, 0))

                End If ' If .Lights(I).Enabled Then

                .SetVertexShaderConstant(53 + 6*I,
                                         New Vector4(.Lights(I).Attenuation0, .Lights(I).Attenuation1,
                                                     .Lights(I).Attenuation2, 0.0F))

            Next I ' For I As Integer = 0 To 7

            .SetPixelShaderConstant(0, New Vector4(m_TeamColour.Red, m_TeamColour.Green, m_TeamColour.Blue, 1.0F))
            .SetPixelShaderConstant(1, New Vector4(m_StripeColour.Red, m_StripeColour.Green, m_StripeColour.Blue, 1.0F))
            .SetPixelShaderConstant(2, New Vector4(0, 0, 0, m_ThrusterPower))

        End With ' With m_D3DManager.Device
    End Sub

    ''' <summary>
    ''' Purges the shader contants.
    ''' </summary>
    Private Sub Render_PurgeShaderConstants(ByVal Device As Direct3D.Device)
        With Device
            .SetVertexShaderConstant(0, Matrix.Zero)
            .SetVertexShaderConstant(4, Matrix.Zero)
            .SetVertexShaderConstant(8, Matrix.Zero)
            .SetVertexShaderConstant(12, Matrix.Zero)
            .SetVertexShaderConstant(16, Matrix.Zero)
            .SetVertexShaderConstant(20, Matrix.Zero)
            .SetVertexShaderConstant(24, Matrix.Zero)
            .SetVertexShaderConstant(28, Matrix.Zero)

            .SetVertexShaderConstant(32, New Vector4(0, 0, 0, 0))
            .SetVertexShaderConstant(44, New Vector4(0, 0, 0, 0))

            For I As Integer = 0 To 7
                .SetVertexShaderConstant(48 + 6*I, New Vector4(0, 0, 0, 0))
                .SetVertexShaderConstant(49 + 6*I, New Vector4(0, 0, 0, 0))
                .SetVertexShaderConstant(50 + 6*I, New Vector4(0, 0, 0, 0))
                .SetVertexShaderConstant(51 + 6*I, New Vector4(0, 0, 0, 0))
                .SetVertexShaderConstant(52 + 6*I, New Vector4(0, 0, 0, 0))
                .SetVertexShaderConstant(53 + 6*I, New Vector4(0, 0, 0, 0))

            Next I ' For I As Integer = 0 To 7

            .SetPixelShaderConstant(0, New Vector4(0, 0, 0, 0))
            .SetPixelShaderConstant(1, New Vector4(0, 0, 0, 0))
            .SetPixelShaderConstant(2, New Vector4(0, 0, 0, 0))

        End With ' With m_D3DManager.Device
    End Sub

    ''' <summary>
    ''' Renders the HOD.
    ''' </summary>
    ''' <param name="Device">
    ''' The device to render to.
    ''' </param>
    ''' <param name="Transform">
    ''' The transform to use.
    ''' </param>
    ''' <param name="CameraPosition">
    ''' Camera position, passed as a parameter to vertex shaders.
    ''' </param>
    ''' <remarks>
    ''' This modifies the following items:
    ''' First three registers of pixel shader;
    ''' Render states (Lighting, SpecularEnable, Ambient, NormalizeNormals);
    ''' Four texture stages (0 to 3);
    ''' Transforms (World)
    ''' Material;
    ''' Viewport
    ''' </remarks>
    Public Sub Render(ByVal Device As Direct3D.Device, ByVal Transform As Matrix, ByVal CameraPosition As Vector3)
        Dim oldWorld As Matrix = Device.Transform.World

        ' Set constants for use in shaders.
        Render_LoadShaderConstants(Device, CameraPosition)

        ' Load badge texture if needed.
        UpdateRender(Device)

        ' Assign "current" badge texture.
        Material.BadgeTexture = m_BadgeTexture

        ' Render background meshes.
        RenderBGMS(Device, Transform)

        ' Render HVMD chunk.
        RenderHVMD(Device, Transform)

        ' Render DTRM chunk.
        RenderDTRM(Device, Transform)

        ' Clear "current" badge texture.
        Material.BadgeTexture = Nothing

        ' Set constants for use in shaders.
        Render_PurgeShaderConstants(Device)

        ' Set old world transform.
        Device.Transform.World = oldWorld
    End Sub
End Class
