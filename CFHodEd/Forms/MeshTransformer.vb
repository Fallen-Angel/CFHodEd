Imports Microsoft.DirectX
Imports GenericMesh
Imports D3DHelper

''' <summary>
''' Form for transforming basic meshes.
''' </summary>
Friend NotInheritable Class MeshTransformer
 ''' <summary>
 ''' Lists of transforms available.
 ''' </summary>
 Private Enum Transforms
  Translation
  Rotation
  Scaling
  Mirror

 End Enum

 ''' <summary>
 ''' A single transform.
 ''' </summary>
 Private Structure Transform
  ''' <summary>Transform type.</summary>
  Public T As Transforms

  ''' <summary>Transform argument.</summary>
  Public A As Vector3

  ''' <summary>
  ''' Returns string representation of this transform.
  ''' </summary>
  Public Overrides Function ToString() As String
   If T = Transforms.Mirror Then _
    Return T.ToString() & " { YZ = " & CStr(A.X) & "; XZ = " & CStr(A.Y) & "; XY = " & CStr(A.Z) & " }" _
   Else _
    Return T.ToString() & " { X = " & CStr(A.X) & "; Y = " & CStr(A.Y) & "; Z = " & CStr(A.Z) & " }"

  End Function

  ''' <summary>
  ''' Returns the matrix transform of this transform.
  ''' </summary>
  Public Function ToMatrix() As Matrix
   Select Case T
    Case Transforms.Translation
     Dim m As Matrix
     m.Translate(A)
     Return m

    Case Transforms.Rotation
     Dim matX, matY, matZ As Matrix
     matX.RotateX(CSng(Math.PI * A.X / 180))
     matY.RotateY(CSng(Math.PI * A.Y / 180))
     matZ.RotateZ(CSng(Math.PI * A.Z / 180))
     Return matX * matY * matZ

    Case Transforms.Scaling
     Dim m As Matrix
     m.Scale(A)
     Return m

    Case Transforms.Mirror
     ' For this to work correctly, A.X, A.Y, A.Z, must be zero OR one.
     Return New Matrix() With { _
      .M11 = 1 - 2 * A.X, _
      .M22 = 1 - 2 * A.Y, _
      .M33 = 1 - 2 * A.Z, _
      .M44 = 1 _
     }

    Case Else
     Trace.TraceError("Error in 'Matrix()' function of transform structure.")
     Return Matrix.Identity

   End Select ' Select Case T

  End Function

 End Structure

 ''' <summary>Transform list.</summary>
 Private m_TransformList As New List(Of Transform)

 ''' <summary>Transform matrix.</summary>
 Private m_World As Matrix

 ''' <summary>Whether to reverse faces or not.</summary>
 Private m_ReverseFaces As Boolean

 ''' <summary>D3D Manager.</summary>
 Private WithEvents m_D3DManager As New D3DManager

 ''' <summary>Camera.</summary>
 Private m_Camera As Camera.UserCamera

 ''' <summary>Light.</summary>
 Private m_Light As Lights.DirectionalLight

 ''' <summary>FPS Display.</summary>
 Private m_FPSDisplay As TextDisplay.FPSDisplay

 ''' <summary>Basic mesh.</summary>
 Private m_Mesh As New Standard.BasicMesh

 ''' <summary>
 ''' Updates the UI, and the world matrix.
 ''' </summary>
 Private Sub UpdateData()
  ' Set matrix to identity.
  m_World = Matrix.Identity

  With lstOp.Items
   ' Clear list.
   .Clear()

   ' Add all transforms.
   For I As Integer = 0 To m_TransformList.Count - 1
    ' Add to list.
    .Add(m_TransformList(I).ToString())

    ' Update world matrix.
    m_World *= m_TransformList(I).ToMatrix()

   Next I ' For I As Integer = 0 To m_TransformList.Count - 1
  End With ' With lstOp.Items

  ' Update label.
  lblReverseFaces.Visible = m_ReverseFaces

  ' Update text boxes.
  txtM00.Text = CStr(m_World.M11)
  txtM01.Text = CStr(m_World.M12)
  txtM02.Text = CStr(m_World.M13)
  txtM03.Text = CStr(m_World.M14)

  txtM10.Text = CStr(m_World.M21)
  txtM11.Text = CStr(m_World.M22)
  txtM12.Text = CStr(m_World.M23)
  txtM13.Text = CStr(m_World.M24)

  txtM20.Text = CStr(m_World.M31)
  txtM21.Text = CStr(m_World.M32)
  txtM22.Text = CStr(m_World.M33)
  txtM23.Text = CStr(m_World.M34)

  txtM30.Text = CStr(m_World.M41)
  txtM31.Text = CStr(m_World.M42)
  txtM32.Text = CStr(m_World.M43)
  txtM33.Text = CStr(m_World.M44)

 End Sub

 ''' <summary>
 ''' Asks a Vector3 from user and returns it.
 ''' </summary>
 Private Function AskVector3FromUser(ByRef Canceled As Boolean, _
                            Optional ByVal AllowZero As Boolean = True, _
                            Optional ByVal AllowNegative As Boolean = True) As Vector3

  Dim strDefault As String = "0"
  Dim strX, strY, strZ As String
  Dim A As Vector3

  ' See if zero is allowed.
  If Not AllowZero Then _
   strDefault = "1"

  Do
   ' Read X value.
   strX = InputBox("Enter X value: ", Me.Text, strDefault)

   ' If user pressed cancel then exit.
   If strX = "" Then _
    Canceled = True _
  : Exit Function

   ' Check if it's numeric.
   If Not IsNumeric(strX) Then _
    MsgBox("Please enter a numeric value!", MsgBoxStyle.Exclamation, Me.Text) _
  : Continue Do

   A.X = CSng(strX)

   ' Check if it's zero.
   If (Not AllowZero) AndAlso (A.X = 0) Then _
    MsgBox("Please enter a non-zero value!", MsgBoxStyle.Exclamation, Me.Text) _
  : Continue Do

   ' Check if it's negative.
   If (Not AllowNegative) AndAlso (A.X < 0) Then _
    MsgBox("Please enter a non-negative value!", MsgBoxStyle.Exclamation, Me.Text) _
  : Continue Do

   Exit Do

  Loop ' Do

  Do
   ' Read Y value.
   strY = InputBox("Enter Y value: ", Me.Text, strDefault)

   ' If user pressed cancel then exit.
   If strY = "" Then _
    Canceled = True _
  : Exit Function

   ' Check if it's numeric.
   If Not IsNumeric(strY) Then _
    MsgBox("Please enter a numeric value!", MsgBoxStyle.Exclamation, Me.Text) _
  : Continue Do

   A.Y = CSng(strY)

   ' Check if it's zero.
   If (Not AllowZero) AndAlso (A.Y = 0) Then _
    MsgBox("Please enter a non-zero value!", MsgBoxStyle.Exclamation, Me.Text) _
  : Continue Do

   ' Check if it's negative.
   If (Not AllowNegative) AndAlso (A.Y < 0) Then _
    MsgBox("Please enter a non-negative value!", MsgBoxStyle.Exclamation, Me.Text) _
  : Continue Do

   Exit Do

  Loop ' Do

  Do
   ' Read Z value.
   strZ = InputBox("Enter Z value: ", Me.Text, strDefault)

   ' If user pressed cancel then exit.
   If strZ = "" Then _
    Canceled = True _
  : Exit Function

   ' Check if it's numeric.
   If Not IsNumeric(strZ) Then _
    MsgBox("Please enter a numeric value!", MsgBoxStyle.Exclamation, Me.Text) _
  : Continue Do

   A.Z = CSng(strZ)

   ' Check if it's zero.
   If (Not AllowZero) AndAlso (A.Z = 0) Then _
    MsgBox("Please enter a non-zero value!", MsgBoxStyle.Exclamation, Me.Text) _
  : Continue Do

   ' Check if it's negative.
   If (Not AllowNegative) AndAlso (A.Z < 0) Then _
    MsgBox("Please enter a non-negative value!", MsgBoxStyle.Exclamation, Me.Text) _
  : Continue Do

   Exit Do

  Loop ' Do

  Canceled = False

  Return A

 End Function

 ''' <summary>
 ''' Sets the mesh to be transformed.
 ''' </summary>
 ''' <remarks>
 ''' The mesh is copied.
 ''' </remarks>
 Public Sub SetMesh(Of VertexType As {Structure, IVertex}, _
                       IndiceType As {Structure, IConvertible}, _
                       MaterialType As {Structure, IMaterial})(ByVal Mesh As GBasicMesh(Of VertexType, IndiceType, MaterialType))
  ' Copy the mesh.
  Mesh.CopyTo(m_Mesh)

 End Sub

 ''' <summary>
 ''' Returns whether the transform(s) performed on the mesh (like mirroring)
 ''' require it's face order to be reversed.
 ''' </summary>
 Public Function GetReverseFaces() As Boolean
  Return m_ReverseFaces

 End Function

 ''' <summary>
 ''' Returns the transform set by user.
 ''' </summary>
 Public Function GetTransform() As Matrix
  Return m_World

 End Function

 Private Sub m_D3DManager_DeviceCreated(ByVal DeviceCreationFlags As D3DHelper.Utility.DeviceCreationFlags) Handles m_D3DManager.DeviceCreated
  ' Enable MSAA if possible.
  If DeviceCreationFlags.Multisampling Then _
   m_D3DManager.Device.RenderState.MultiSampleAntiAlias = True

  ' Create the camera.
  m_Camera = New Camera.UserCamera With { _
   .Device = m_D3DManager.Device, _
   .ProjectionType = Camera.ProjectionType.PerspectiveFov, _
   .FOV = Math.PI / 2.0F, _
   .Width = m_D3DManager.Device.PresentationParameters.BackBufferWidth, _
   .Height = m_D3DManager.Device.PresentationParameters.BackBufferHeight, _
   .ZNear = 0.1F, _
   .ZFar = 10000.0F _
  }

  ' Create the light.
  m_Light = New Lights.DirectionalLight With { _
   .Ambient = New Direct3D.ColorValue(0.2, 0.2, 0.2), _
   .Diffuse = New Direct3D.ColorValue(0.8, 0.8, 0.8), _
   .Specular = New Direct3D.ColorValue(0, 0, 0), _
   .Direction = New Vector3(0, 0, -1), _
   .Enabled = True _
  }

  ' Attach light to camera.
  m_Light.Attach(m_Camera)

  ' Create FPS display.
  m_FPSDisplay = New TextDisplay.FPSDisplay(m_D3DManager.Device) With { _
   .Format = Direct3D.DrawTextFormat.Top Or Direct3D.DrawTextFormat.Left, _
   .DisplayModNFramerate = My.Settings.D3D_DisplayFrameRate, _
   .DisplayAverageFramerate = False, _
   .DisplayInstantaneousFramerate = False _
  }

  ' Lock mesh.
  m_Mesh.Lock(m_D3DManager.Device)

  ' Fire the window resize event.
  MeshTransformer_Resize(Nothing, EventArgs.Empty)

 End Sub

 Private Sub D3DManager_DeviceLost(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_D3DManager.DeviceLost
  TextDisplay.TextDisplay.OnLostDevice(m_D3DManager.Device)
  m_Mesh.Unlock()

 End Sub

 Private Sub D3DManager_DeviceReset(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_D3DManager.DeviceReset
  TextDisplay.TextDisplay.OnResetDevice(m_D3DManager.Device)
  m_Mesh.Lock(m_D3DManager.Device)

 End Sub

 Private Sub D3DManager_Disposing(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_D3DManager.Disposing
  TextDisplay.TextDisplay.OnDeviceDisposing(m_D3DManager.Device)
  m_Mesh.Unlock()

 End Sub

 Private Sub D3DManager_Render() Handles m_D3DManager.Render
  With m_D3DManager.Device
   ' Get the backbuffer dimensions.
   Dim BBW As Integer = .PresentationParameters.BackBufferWidth, _
       BBH As Integer = .PresentationParameters.BackBufferHeight

   ' Get the panel dimensions.
   Dim PBW As Integer = pnlDisplay.ClientSize.Width, _
       PBH As Integer = pnlDisplay.ClientSize.Height

   ' Calculate the offset.
   Dim OffsetX As Integer = (BBW - PBW) \ 2, _
       OffsetY As Integer = (BBH - PBH) \ 2

   ' Present previous scene, clear, and begin scene.
   .Present()
   .Clear(Direct3D.ClearFlags.Target Or Direct3D.ClearFlags.ZBuffer, Color.Black, 1.0, 0)
   .BeginScene()

   ' Update camera, light, FPS display.
   m_Camera.Update()
   m_Light.Update(.Lights(0))
   m_FPSDisplay.Region = New Rectangle(OffsetX, OffsetY, PBW, PBH)
   m_FPSDisplay.Update()

   ' Update transform.
   .Transform.World = m_World

   ' Render mesh.
   m_Mesh.Render(m_D3DManager.Device)

   ' End scene.
   .EndScene()

  End With ' With m_D3DManager.Device

 End Sub

 Private Sub MeshTransformer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
  ' Stop rendering.
  m_D3DManager.RenderLoopStop()

  ' Dispose device.
  m_D3DManager.Device.Dispose()

 End Sub

 Private Sub MeshTransformer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
  ' Create device.
  m_D3DManager.CreateDevice(pbxDisplay, "", False, False)

  ' Start rendering.
  m_D3DManager.RenderLoopBegin()

  ' Update data and UI.
  UpdateData()

  ' Reset camera.
  cmdResetCamera_Click(Nothing, EventArgs.Empty)

 End Sub

 Private Sub MeshTransformer_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
  ' Do nothing if device has not been created.
  If (m_D3DManager Is Nothing) OrElse (m_D3DManager.Device Is Nothing) Then _
   Exit Sub

  ' Get the backbuffer dimensions.
  Dim BBW As Integer = m_D3DManager.Device.PresentationParameters.BackBufferWidth, _
      BBH As Integer = m_D3DManager.Device.PresentationParameters.BackBufferHeight

  ' Get the panel dimensions.
  Dim PBW As Integer = pnlDisplay.ClientSize.Width, _
      PBH As Integer = pnlDisplay.ClientSize.Height

  ' Calculate the offset.
  Dim OffsetX As Integer = (BBW - PBW) \ 2, _
      OffsetY As Integer = (BBH - PBH) \ 2

  ' Position picture box.
  pbxDisplay.Location = New Point(-OffsetX, -OffsetY)
  pbxDisplay.Size = New Size(BBW, BBH)

 End Sub

 Private Sub cmdTranslate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTranslate.Click
  Dim canceled As Boolean

  ' Make new transform.
  Dim t As New Transform With { _
   .T = Transforms.Translation, _
   .A = AskVector3FromUser(canceled) _
  }

  ' See if user canceled.
  If canceled Then _
   Exit Sub

  ' Add to list.
  m_TransformList.Add(t)

  ' Update data and UI
  UpdateData()

 End Sub

 Private Sub cmdRotate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRotate.Click
  Dim canceled As Boolean

  ' Make new transform.
  Dim t As New Transform With { _
   .T = Transforms.Rotation, _
   .A = AskVector3FromUser(canceled) _
  }

  ' See if user canceled.
  If canceled Then _
   Exit Sub

  ' Add to list.
  m_TransformList.Add(t)

  ' Update data and UI
  UpdateData()

 End Sub

 Private Sub cmdScaling_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdScaling.Click
  Dim canceled As Boolean

  ' Make new transform.
  Dim t As New Transform With { _
   .T = Transforms.Scaling, _
   .A = AskVector3FromUser(canceled) _
  }

  ' See if user canceled.
  If canceled Then _
   Exit Sub

  ' Add to list.
  m_TransformList.Add(t)

  ' Update data and UI
  UpdateData()

 End Sub

 Private Sub cmdMirror_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMirror.Click
  Dim reply As MsgBoxResult
  Dim oldReverseMesh As Boolean = m_ReverseFaces
  Dim t As New Transform With {.T = Transforms.Mirror}

  ' Ask user if mesh is to be mirrored about YZ plane.
  reply = MsgBox("Mirror about YZ plane?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question, Me.Text)

  ' See if user pressed cancel.
  If reply = MsgBoxResult.Cancel Then _
   Exit Sub

  ' Set value.
  If reply = MsgBoxResult.Yes Then _
   t.A.X = 1 _
 : m_ReverseFaces = Not m_ReverseFaces _
  Else _
   t.A.X = 0

  ' Ask user if mesh is to be mirrored about XZ plane.
  reply = MsgBox("Mirror about XZ plane?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question, Me.Text)

  ' See if user pressed cancel.
  If reply = MsgBoxResult.Cancel Then _
   Exit Sub

  ' Set value.
  If reply = MsgBoxResult.Yes Then _
   t.A.Y = 1 _
 : m_ReverseFaces = Not m_ReverseFaces _
  Else _
   t.A.Y = 0

  ' Ask user if mesh is to be mirrored about XY plane.
  reply = MsgBox("Mirror about XY plane?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question, Me.Text)

  ' See if user pressed cancel.
  If reply = MsgBoxResult.Cancel Then _
   Exit Sub

  ' Set value.
  If reply = MsgBoxResult.Yes Then _
   t.A.Z = 1 _
 : m_ReverseFaces = Not m_ReverseFaces _
  Else _
   t.A.Z = 0

  ' Add new transform.
  m_TransformList.Add(t)

  ' Update data and UI.
  UpdateData()

  ' Reverse mesh if needed.
  If oldReverseMesh <> m_ReverseFaces Then
   ' Pause render, unlock mesh.
   m_D3DManager.RenderLoopPause()
   m_Mesh.Unlock()

   ' Reverse faces.
   m_Mesh.ReverseFaceOrder()

   ' Lock mesh, resume render.
   m_Mesh.Lock(m_D3DManager.Device)
   m_D3DManager.RenderLoopResume()

  End If ' If oldReverseMesh <> m_ReverseFaces Then

 End Sub

 Private Sub cmdReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReset.Click
  ' See if we need to reverse the mesh in order to get the mesh into
  ' original state.
  If m_ReverseFaces Then
   ' Pause render, unlock mesh.
   m_D3DManager.RenderLoopPause()
   m_Mesh.Unlock()

   ' Reverse faces.
   m_Mesh.ReverseFaceOrder()

   ' Lock mesh, resume render.
   m_Mesh.Lock(m_D3DManager.Device)
   m_D3DManager.RenderLoopResume()

  End If ' If m_ReverseFaces Then

  ' Reset reverse flag.
  m_ReverseFaces = False

  ' Clear all transforms.
  m_TransformList.Clear()

  ' Update data and UI.
  UpdateData()

 End Sub

 Private Sub cmdApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApply.Click
  ' Close form.
  Me.Close()

 End Sub

 Private Sub cmdResetCamera_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdResetCamera.Click
  ' Decide the distance from camera.
  Dim v As Vector3, r As Single

  ' Get the mesh's radius.
  m_Mesh.GetMeshSphere(v, r)

  ' Calculate total distance from origin.
  r += v.Length()

  ' Make sure it's not zero.
  If r <= 0 Then _
   r = 1.0F

  ' Set camera distance.
  m_Camera.Reset(1.25F * r)

 End Sub

 Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
  ' Reset transform.
  cmdReset_Click(Nothing, EventArgs.Empty)

  ' Apply transform.
  cmdApply_Click(Nothing, EventArgs.Empty)

 End Sub

 Private Sub pbxDisplay_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbxDisplay.MouseMove
  Static OldPos As Vector2 = New Vector2(e.Location.X, e.Location.Y)
  Dim NewPos As New Vector2(e.Location.X, e.Location.Y), _
      Delta As Vector2 = NewPos - OldPos

  Select Case e.Button
   Case Windows.Forms.MouseButtons.Left
    m_Camera.CameraRotate(Delta)

   Case Windows.Forms.MouseButtons.Right
    m_Camera.CameraZoom(Delta)

   Case Windows.Forms.MouseButtons.Middle, _
        Windows.Forms.MouseButtons.Left Or _
        Windows.Forms.MouseButtons.Right
    m_Camera.CameraPan(NewPos, OldPos, Matrix.Identity)

  End Select ' Select Case e.Button

  OldPos = NewPos

 End Sub

End Class
