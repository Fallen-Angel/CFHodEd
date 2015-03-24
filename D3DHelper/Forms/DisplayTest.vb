''' <summary>
''' Form containing code for display testing with specified settings.
''' </summary>
Friend NotInheritable Class DisplayTest
    Private m_ClearFlags As ClearFlags
    Private m_Camera As Camera.BaseCamera
    Private m_Light As Lights.BaseLight
    Private m_Mesh As Direct3D.Mesh
    Private m_FPSDisplay As TextDisplay.FPSDisplay
    Private WithEvents m_D3DM As D3DManager

    Private Sub DisplayTest_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) _
        Handles Me.FormClosing
        m_D3DM.ReleaseDevice()

        m_FPSDisplay = Nothing
        m_Mesh = Nothing
        m_Light = Nothing
        m_Camera = Nothing
    End Sub

    Private Sub DisplayTest_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
        Handles Me.KeyPress
        ' Exit on escape.
        If e.KeyChar = Chr(ConsoleKey.Escape) Then _
            e.Handled = True _
                : Close()
    End Sub

    Private Sub DisplayTest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        m_D3DM = New D3DManager
        m_D3DM.CreateDevice(Me, Me.GetType.ToString())
    End Sub

    Private Sub DisplayTest_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        m_D3DM.RenderLoopBegin()
    End Sub

    Private Sub m_D3DM_DeviceCreated(ByVal DeviceCreationFlags As Utility.DeviceCreationFlags) _
        Handles m_D3DM.DeviceCreated
        Dim Mat As Material
        Dim adjacency As GraphicsStream = Nothing

        ' Build the mesh cube.
        ' --------------------
        m_Mesh = Mesh.Box(m_D3DM.Device, 1, 1, 1, adjacency)
        m_Mesh.OptimizeInPlace(MeshFlags.OptimizeAttributeSort Or
                               MeshFlags.OptimizeCompact Or
                               MeshFlags.OptimizeVertexCache,
                               adjacency)

        ' Initialize the objects.
        ' -----------------------
        m_Camera = New Camera.Camera
        m_Camera.Device = m_D3DM.Device
        m_Camera.Reset(2.5)

        m_Light = New Lights.DirectionalLight
        m_Light.Attach(m_Camera)

        m_FPSDisplay = New TextDisplay.FPSDisplay(m_D3DM.Device)
        m_FPSDisplay.Reset()

        ' Build the material.
        ' -------------------
        With Mat
            .AmbientColor = New ColorValue(0.2, 0.2, 0.2)
            .DiffuseColor = New ColorValue(0.8, 0.8, 0.8)
        End With ' With Mat

        m_D3DM.Device.Material = Mat

        ' Set the clear flags.
        ' --------------------
        m_ClearFlags = ClearFlags.Target

        If DeviceCreationFlags.DepthBuffer Then _
            m_ClearFlags = m_ClearFlags Or ClearFlags.ZBuffer

        If DeviceCreationFlags.StencilBuffer Then _
            m_ClearFlags = m_ClearFlags Or ClearFlags.Stencil

        ' Enable multi-sampling if possible.
        ' ----------------------------------
        If DeviceCreationFlags.Multisampling Then _
            m_D3DM.Device.RenderState.MultiSampleAntiAlias = True
    End Sub

    Private Sub m_D3DM_DeviceReset(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_D3DM.DeviceReset
        Dim Mat As Material

        ' Initialize the objects.
        m_FPSDisplay.Reset()

        ' Build the material.
        With Mat
            .AmbientColor = New ColorValue(0.2, 0.2, 0.2)
            .DiffuseColor = New ColorValue(0.8, 0.8, 0.8)
        End With ' With Mat

        m_D3DM.Device.Material = Mat

        ' Enable multi-sampling if possible.
        If m_D3DM.Device.PresentationParameters.MultiSample <> MultiSampleType.None Then _
            m_D3DM.Device.RenderState.MultiSampleAntiAlias = True
    End Sub

    Private Sub m_D3DM_Render() Handles m_D3DM.Render
        With m_D3DM.Device
            .Present()

            .Clear(m_ClearFlags, Color.Black, 1.0, 0)
            .BeginScene()

            m_FPSDisplay.Update()
            m_Camera.Update()
            m_Light.Update(.Lights(0))

            .Transform.World = Matrix.RotationX(CSng(Microsoft.VisualBasic.Timer/2))*
                               Matrix.RotationY(CSng(Microsoft.VisualBasic.Timer/1.5))*
                               Matrix.RotationZ(CSng(Microsoft.VisualBasic.Timer))

            ' Draw the mesh.
            If m_Mesh IsNot Nothing Then
                For I As Integer = 0 To m_Mesh.NumberAttributes - 1
                    m_Mesh.DrawSubset(I)

                Next I ' For I As Integer = 0 To M.NumberAttributes - 1
            End If ' If m_Mesh IsNot Nothing Then

            .EndScene()

        End With ' With D.Device
    End Sub
End Class
