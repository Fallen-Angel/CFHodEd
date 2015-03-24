Imports Microsoft.DirectX
Imports D3DHelper

''' <summary>
''' Form to display a texture.
''' </summary>
Friend NotInheritable Class TexturePreview
    ''' <summary>Texture to display.</summary>
    Private m_Texture As Direct3D.Texture

    ''' <summary>Texture size.</summary>
    Private m_Size As New Vector2(1, 1)

    ''' <summary>Texture offset.</summary>
    Private m_Offset As New Vector2(0, 0)

    ''' <summary>Magnification factor.</summary>
    Private m_Zoom As Single = 1.0

    ''' <summary>D3D Manager.</summary>
    Private WithEvents m_D3DManager As New D3DManager

    ''' <summary>
    ''' Class contructor.
    ''' </summary>
    Public Sub New(ByVal stream As IO.Stream)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Create device.
        m_D3DManager.CreateDevice(pbxDisplay, "", False, False)

        ' Set the texture.
        m_Texture = Direct3D.TextureLoader.FromStream(m_D3DManager.Device, stream)

        ' Set the texture dimensions.
        If m_Texture IsNot Nothing Then
            If m_Texture.LevelCount <> 0 Then
                With m_Texture.GetLevelDescription(0)
                    m_Size.X = .Width
                    m_Size.Y = .Height

                End With ' With m_Texture.GetLevelDescription(0)
            End If ' If m_Texture.LevelCount <> 0 Then
        End If ' If m_Texture IsNot Nothing Then
    End Sub

    Private Sub m_D3DManager_DeviceCreated(ByVal DeviceCreationFlags As D3DHelper.Utility.DeviceCreationFlags) _
        Handles m_D3DManager.DeviceCreated
        ' Enable MSAA if possible.
        If DeviceCreationFlags.Multisampling Then _
            m_D3DManager.Device.RenderState.MultiSampleAntiAlias = True

        m_D3DManager.SetupTextureFilteringModes(1)

        With m_D3DManager.Device
            .RenderState.BlendOperation = Direct3D.BlendOperation.Add
            .RenderState.SourceBlend = Direct3D.Blend.SourceAlpha
            .RenderState.DestinationBlend = Direct3D.Blend.InvSourceAlpha

        End With ' With m_D3DManager.Device

        ' Fire the window resize event.
        TexturePreview_Resize(Nothing, EventArgs.Empty)
    End Sub

    Private Sub m_D3DManager_Disposing(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles m_D3DManager.Disposing
        ' Dispose the texture.
        m_Texture.Dispose()
    End Sub

    Private Sub m_D3DManager_Render() Handles m_D3DManager.Render
        Dim vtx() As Direct3D.CustomVertex.TransformedTextured = { _
                                                                     New Direct3D.CustomVertex.TransformedTextured(
                                                                         m_Offset.X, m_Offset.Y, 0, 1, 0, 0),
                                                                     New Direct3D.CustomVertex.TransformedTextured(
                                                                         m_Offset.X + m_Size.X*m_Zoom, m_Offset.Y, 0, 1,
                                                                         1, 0),
                                                                     New Direct3D.CustomVertex.TransformedTextured(
                                                                         m_Offset.X, m_Offset.Y + m_Size.Y*m_Zoom, 0, 1,
                                                                         0, 1),
                                                                     New Direct3D.CustomVertex.TransformedTextured(
                                                                         m_Offset.X + m_Size.X*m_Zoom,
                                                                         m_Offset.Y + m_Size.Y*m_Zoom, 0, 1, 1, 1)
                                                                 }

        With m_D3DManager.Device
            ' Present, clear and begin scene.
            .Present()
            .Clear(Direct3D.ClearFlags.Target Or Direct3D.ClearFlags.ZBuffer, Color.Black, 1, 0)
            .BeginScene()

            ' Enable\Disable alpha blending.
            .RenderState.AlphaBlendEnable = chkAlphaBelnd.Checked

            ' Set texture and vertex format.
            .SetTexture(0, m_Texture)
            .VertexFormat = Direct3D.CustomVertex.TransformedTextured.Format

            ' Draw primitive.
            .DrawUserPrimitives(Direct3D.PrimitiveType.TriangleStrip, 2, vtx)

            ' End scene.
            .EndScene()

        End With ' With m_D3DManager.Device
    End Sub

    Private Sub TexturePreview_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) _
        Handles Me.FormClosing
        ' Stop rendering.
        m_D3DManager.RenderLoopStop()

        ' Release device.
        m_D3DManager.ReleaseDevice()
    End Sub

    Private Sub TexturePreview_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Start rendering.
        m_D3DManager.RenderLoopBegin()

        ' Update zoom label.
        cmdZoom_Click(Nothing, EventArgs.Empty)
    End Sub

    Private Sub TexturePreview_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If (m_D3DManager Is Nothing) OrElse (m_D3DManager.Device Is Nothing) Then _
            Exit Sub

        ' Get the backbuffer dimensions.
        Dim BBW As Integer = m_D3DManager.Device.PresentationParameters.BackBufferWidth,
            BBH As Integer = m_D3DManager.Device.PresentationParameters.BackBufferHeight

        ' Position picture box.
        pbxDisplay.Location = New Point(0, 0)
        pbxDisplay.Size = New Size(BBW, BBH)
    End Sub

    Private Sub pbxDisplay_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles pbxDisplay.MouseMove
        Static oldPos As New Vector2(e.X, e.Y)
        Dim newPos As New Vector2(e.X, e.Y),
            delta As Vector2 = newPos - oldPos

        Select Case e.Button
            Case Windows.Forms.MouseButtons.Left
                m_Offset.X += delta.X
                m_Offset.Y += delta.Y

            Case Windows.Forms.MouseButtons.Right
                If (delta.X < 0) OrElse (delta.Y < 0) Then _
                    m_Zoom *= (1 - delta.Length()/100) _
                    Else _
                    m_Zoom *= (1 + delta.Length()/100)

                cmdZoom_Click(Nothing, EventArgs.Empty)

        End Select ' Select Case e.Button

        oldPos = newPos
    End Sub

    Private Sub cmdReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReset.Click
        m_Offset.X = 0
        m_Offset.Y = 0
        m_Zoom = 1

        cmdZoom_Click(Nothing, EventArgs.Empty)
    End Sub

    Private Sub cmdZoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles cmdZoomIn.Click, cmdZoomOut.Click
        If sender Is cmdZoomIn Then _
            m_Zoom += 0.1F

        If sender Is cmdZoomOut Then _
            m_Zoom -= 0.1F

        If m_Zoom < 0.1 Then _
            m_Zoom = 0.1

        If m_Zoom > 10 Then _
            m_Zoom = 10

        lblMag.Text = FormatNumber(m_Zoom, 3) & "x"
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub
End Class
