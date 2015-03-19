Imports Microsoft.DirectX
Imports D3DHelper
Imports Homeworld2.HOD
Imports Homeworld2.MAD

''' <summary>
''' Advanced HOD Editor form.
''' </summary>
Friend NotInheritable Class HODEditorA
 
#Region " Variables\Constructors "

 ''' <summary>HOD.</summary>
 Private m_HOD As New HOD With { _
  .TeamColour = Direct3D.ColorValue.FromArgb(My.Settings.HOD_TeamColour), _
  .StripeColour = Direct3D.ColorValue.FromArgb(My.Settings.HOD_StripeColour), _
  .Badge = My.Settings.HOD_BadgeTexture _
 }

 ''' <summary>MAD.</summary>
 Private m_MAD As New MAD

 ''' <summary>Animation being played.</summary>
 Private m_MAD_CurrAnim As Animation

 ''' <summary>Animation time.</summary>
 Private m_MAD_Time As Single

 ''' <summary>Name of currently open file.</summary>
 Private m_Filename As String

 ''' <summary>Object scale.</summary>
 Private m_ObjectScale As Single = 1.0F

 ''' <summary>Skeleton scale.</summary>
 Private m_SkeletonScale As Single = 1.0F

 ''' <summary>Marker scale.</summary>
 Private m_MarkerScale As Single = 1.0F

 ''' <summary>Dockpoint scale.</summary>
 Private m_DockpointScale As Single = 1.0F

 ''' <summary>Camera sensitivity</summary>
 Private m_CameraSensitivity As Single = 1.0F

 ''' <summary>Direct3D Manager object.</summary>
 Private WithEvents m_D3DManager As New D3DManager

 ''' <summary>Camera.</summary>
 Private m_Camera As Camera.UserCamera

 ''' <summary>Light.</summary>
 Private m_Light As Lights.BaseLight

 ''' <summary>Frame-rate display.</summary>
 Private m_FPSDisplay As TextDisplay.FPSDisplay

 ''' <summary>
 ''' Class constructor.
 ''' </summary>
 Public Sub New()
  ' This call is required by the Windows Form Designer.
  InitializeComponent()

  ' Setup scales.
  m_HOD.SkeletonScale = m_SkeletonScale
  m_HOD.MarkerScale = m_MarkerScale
  m_HOD.DockpointScale = m_DockpointScale

 End Sub

#End Region

#Region " Utility "

 ''' <summary>
 ''' Loads all shaders.
 ''' </summary>
 Private Sub _LoadShaders()
  ' See if we have to use shaders.
  If Not My.Settings.HOD_UseShaders Then _
   Exit Sub

  ' Initialize a new IOResult window.
  Dim f As New IOResult

  ' Load shaders.
  ShaderLibrary.LoadShaders(m_D3DManager.Device)

  ' Load custom shaders. So get the directory name.
  Dim dir As String = Application.StartupPath & "\shaders"

  If IO.Directory.Exists(dir) Then
   ' Get filenames.
   Dim files() As String = IO.Directory.GetFiles(dir)

   ' Load all files.
   For Each s As String In files
    Dim TR As IO.StreamReader = Nothing

    Try
     ' Open the text reader.
     TR = New IO.StreamReader(s)

     ' Read all text.
     Dim shaderText As String = TR.ReadToEnd()

     ' Dispose the text reader.
     TR.Dispose()

     ' Load shader.
     Dim result As Boolean = ShaderLibrary.LoadShader(s, shaderText, m_D3DManager.Device)

#If DEBUG Then
     ' Display result.
     If result Then _
      Trace.TraceInformation("Successfully loaded shader '" & s & "'.")

#End If

    Catch ex As Exception
     Trace.TraceError("Error while loading shader '" & s & "':" & vbCrLf & ex.ToString())

    Finally
     ' Dispose the text reader.
     If TR IsNot Nothing Then _
      TR.Dispose()

    End Try
   Next s ' For Each s As String In files
  End If ' If IO.Directory.Exists(dir) Then

  ' Display the result.
  f.Show()

 End Sub

 ''' <summary>
 ''' Tries to open a stream. Returns it if successful, otherwise
 ''' returns nothing.
 ''' </summary>
 ''' <param name="Filename">
 ''' File to open.
 ''' </param>
 Private Function _TryOpenStream(ByVal Filename As String, ByVal Read As Boolean) As IO.FileStream
  Dim stream As IO.FileStream

  Try
   If Read Then _
    stream = New IO.FileStream(Filename, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read) _
   Else _
    stream = New IO.FileStream(Filename, IO.FileMode.Create, IO.FileAccess.Write)

  Catch ex As ArgumentNullException
   ' path is null.
   MsgBox("Invalid file name!", MsgBoxStyle.Exclamation, Me.Text)
   Return Nothing

  Catch ex As ArgumentException
   ' path is an empty string (""), contains only white space, or contains one or more invalid characters. -or-
   ' path refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.
   MsgBox("Invalid file name!", MsgBoxStyle.Exclamation, Me.Text)
   Return Nothing

  Catch ex As NotSupportedException
   ' path refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.
   MsgBox("Invalid file name!", MsgBoxStyle.Exclamation, Me.Text)
   Return Nothing

  Catch ex As IO.FileNotFoundException
   ' The file cannot be found, such as when mode is FileMode.Truncate or FileMode.Open, and the file
   ' specified by path does not exist. The file must already exist in these modes.
   MsgBox("File not found!", MsgBoxStyle.Exclamation, Me.Text)
   Return Nothing

  Catch ex As System.Security.SecurityException
   ' The caller does not have the required permission.
   MsgBox("The application doesn't have the required permission" & vbCrLf & _
          "to open this file!", MsgBoxStyle.Exclamation, Me.Text)
   Return Nothing

  Catch ex As IO.DirectoryNotFoundException
   ' The specified path is invalid, such as being on an unmapped drive.
   MsgBox("Directory not found!", MsgBoxStyle.Exclamation, Me.Text)
   Return Nothing

  Catch ex As UnauthorizedAccessException
   ' The access requested is not permitted by the operating system for the specified path,
   ' such as when access is Write or ReadWrite and the file or directory is set for read-only access.
   ' The caller does not have the required permission.
   MsgBox("The application doesn't have the required access" & vbCrLf & _
          "to open this file!", MsgBoxStyle.Exclamation, Me.Text)
   Return Nothing

  Catch ex As Exception
   ' Some unknown
   MsgBox("Error while trying to open file: " & vbCrLf & ex.ToString(), _
          MsgBoxStyle.Critical, Me.Text)

   Return Nothing

  End Try

  Return stream

 End Function

 ''' <summary>
 ''' Backups the specified file, if needed.
 ''' </summary>
 Private Sub _BackupFile(ByVal filename As String)
  ' See if we're supposed to make backup.
  If Not My.Settings.HOD_MakeBackup Then _
   Exit Sub

  Try
   ' Get the full file name.
   filename = IO.Path.GetFullPath(filename)

   ' Decide a name for the backup file.
   Dim destFile As String = IO.Path.GetDirectoryName(filename) & "\" & _
                            IO.Path.GetFileNameWithoutExtension(filename) & "_backup" & _
                            IO.Path.GetExtension(filename)

   ' Copy file.
   IO.File.Copy(filename, destFile, True)

  Catch ex As Exception
   MsgBox("Warning: Coudn't backup file:" & vbCrLf & _
          "'" & filename & "'.", MsgBoxStyle.Critical, Me.Text)

  End Try

 End Sub

 ''' <summary>
 ''' Sets the file name.
 ''' </summary>
 Private Sub _SetFilename(ByVal f As String)
  m_Filename = f

  If (f Is Nothing) OrElse (f = "") Then _
   sbrLabel.Text = "untitled" _
  Else _
   sbrLabel.Text = f

 End Sub

 ''' <summary>
 ''' Returns whether a collection has the specified string, comparing
 ''' without regard to case.
 ''' </summary>
 Private Function _ListBoxHasString(ByVal collection As ListBox.ObjectCollection, ByVal str As String) As Boolean
  For I As Integer = 0 To collection.Count - 1
   If (TypeOf collection(I) Is String) AndAlso _
      (CStr(collection(I)).Equals(str, StringComparison.OrdinalIgnoreCase)) Then _
    Return True

  Next I ' For I As Integer = 0 To collection.Count - 1

  Return False

 End Function

 ''' <summary>
 ''' Returns whether a collection has the specified string, comparing
 ''' without regard to case.
 ''' </summary>
 Private Function _ComboBoxHasString(ByVal collection As ComboBox.ObjectCollection, ByVal str As String) As Boolean
  For I As Integer = 0 To collection.Count - 1
   If (TypeOf collection(I) Is String) AndAlso _
      (CStr(collection(I)).Equals(str, StringComparison.OrdinalIgnoreCase)) Then _
    Return True

  Next I ' For I As Integer = 0 To collection.Count - 1

  Return False

 End Function


 ''' <summary>
 ''' Asks a Vector3 from user and returns it.
 ''' </summary>
 Private Function _AskVector3FromUser(ByRef Canceled As Boolean, _
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

#End Region

#Region " D3DManager Events "

 ''' <summary>Whether to render in wireframe or solid.</summary>
 Private m_RenderWireframe As Boolean

 Private Sub D3DManager_DeviceCreated(ByVal DeviceCreationFlags As D3DHelper.Utility.DeviceCreationFlags) Handles m_D3DManager.DeviceCreated
  ' Enable the normalize normals state.
  m_D3DManager.Device.RenderState.NormalizeNormals = True

  ' Enable multi sample anti alias if possible.
  If DeviceCreationFlags.Multisampling Then _
   m_D3DManager.Device.RenderState.MultiSampleAntiAlias = True

  ' Initialize camera.
  m_Camera = New Camera.UserCamera With { _
   .Device = m_D3DManager.Device, _
   .ProjectionType = Camera.ProjectionType.PerspectiveFov, _
   .FOV = Math.PI / 2, _
   .Width = m_D3DManager.Device.PresentationParameters.BackBufferWidth, _
   .Height = m_D3DManager.Device.PresentationParameters.BackBufferHeight, _
   .ZNear = 0.1, _
   .ZFar = 100 _
  }

  ' Initialize light.
  m_Light = New Lights.DirectionalLight With { _
   .Ambient = New Direct3D.ColorValue(0.0F, 0.0F, 0.0F), _
   .Diffuse = New Direct3D.ColorValue(1.0F, 1.0F, 1.0F), _
   .Specular = New Direct3D.ColorValue(0.5F, 0.5F, 0.5F), _
   .Direction = New Vector3(0, 0, -1), _
   .Enabled = pbxDisplay_cmsLights.Checked _
  }

  ' Initialize FPS display.
  m_FPSDisplay = New TextDisplay.FPSDisplay(m_D3DManager.Device) With { _
   .Format = Direct3D.DrawTextFormat.Top Or Direct3D.DrawTextFormat.Right, _
   .DisplayModNFramerate = My.Settings.D3D_DisplayFrameRate, _
   .DisplayAverageFramerate = False, _
   .DisplayInstantaneousFramerate = False _
  }

  ' Attach light to camera.
  m_Light.Attach(m_Camera)

  ' Load shaders.
  _LoadShaders()

  ' Fire the form resized event.
  HODEditorA_Resize(Nothing, EventArgs.Empty)

 End Sub

 Private Sub D3DManager_DeviceLost(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_D3DManager.DeviceLost
  ' Notify text display that device was lost.
  TextDisplay.TextDisplay.OnLostDevice(m_D3DManager.Device)

  ' Unlock meshes if we haven't.
  m_HOD.UnlockMeshes()

  ' Dispose shaders.
  ShaderLibrary.DisposeShaders()

 End Sub

 Private Sub D3DManager_DeviceReset(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_D3DManager.DeviceReset
  ' Notify text display that device has reset.
  TextDisplay.TextDisplay.OnResetDevice(m_D3DManager.Device)

  ' Lock meshes.
  m_HOD.LockMeshes(m_D3DManager.Device)

  ' Load shaders.
  ShaderLibrary.LoadShaders(m_D3DManager.Device)

 End Sub

 Private Sub D3DManager_Disposing(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_D3DManager.Disposing
  ' Notify text display that device is disposing.
  TextDisplay.TextDisplay.OnDeviceDisposing(m_D3DManager.Device)

  ' Unlock meshes if we haven't.
  m_HOD.UnlockMeshes()

  ' Dispose shaders.
  ShaderLibrary.DisposeShaders()

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

   ' Update camera, light, framerate display.
   m_Camera.Update()
   m_Light.Update(.Lights(0), pbxDisplay_cmsLights.Checked)
   m_FPSDisplay.Region = New Rectangle(OffsetX, OffsetY, PBW, PBH)
   m_FPSDisplay.Update()

   ' Decide the fill mode.
   Dim fillMode As Direct3D.FillMode

   If m_RenderWireframe Then _
    fillMode = Direct3D.FillMode.WireFrame _
   Else _
    fillMode = Direct3D.FillMode.Solid

   ' Set whether to enable or disable textures.
   HOD.EnableTextures = (fillMode = Direct3D.FillMode.Solid)

   ' Set fill mode.
   .RenderState.Lighting = (fillMode = Direct3D.FillMode.Solid)
   .RenderState.SpecularEnable = True
   .RenderState.FillMode = fillMode

   ' Modify viewport to render into whole view.
   Dim vwprt As Direct3D.Viewport = .Viewport
   vwprt.MinZ = 0 : vwprt.MaxZ = 1
   .Viewport = vwprt

   ' Update animation.
   _UpdateAnimation()

   ' Render HOD.
   m_HOD.Render(m_D3DManager.Device, Matrix.Identity, m_Camera.Position)

   ' Render wireframe overlay if needed.
   If (fillMode <> Direct3D.FillMode.WireFrame) AndAlso (pbxDisplay_cmsWireframeOverlay.Checked) Then
    .RenderState.Lighting = False
    .RenderState.SpecularEnable = False
    .RenderState.FillMode = Direct3D.FillMode.WireFrame

    ' Modify viewport to render into foreground.
    vwprt = .Viewport
    vwprt.MinZ = 0 : vwprt.MaxZ = 0
    .Viewport = vwprt

    ' Disable textures.
    HOD.EnableTextures = False

    ' Render HOD.
    m_HOD.Render(m_D3DManager.Device, Matrix.Identity, m_Camera.Position)

   End If ' If (fillMode <> Direct3D.FillMode.WireFrame) AndAlso (pbxDisplay_cmsWireframeOverlay.Checked) Then

   ' End scene.
   .EndScene()

  End With ' With m_D3DManager.Device

 End Sub

#End Region

#Region " Form "

 Private Sub HODEditorA_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
  ' If the valiladtion event or something else is stopping us from exiting then
  ' do not continue.
  If e.Cancel Then _
   Exit Sub

  ' Stop rendering.
  m_D3DManager.RenderLoopStop()

  ' Dispose the device.
  m_D3DManager.Device.Dispose()

  ' Save settings.
  My.Settings.Form_HODEditorA_WindowState = Me.WindowState
  My.Settings.Form_HODEditorA_Location = Me.Location
  My.Settings.Form_HODEditorA_Size = Me.Size

 End Sub

 Private Sub HODEditorA_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
  ' Load settings.
  Me.WindowState = My.Settings.Form_HODEditorA_WindowState

  ' If not maximized, load location and window size.
  If Me.WindowState = FormWindowState.Normal Then _
   Me.Location = My.Settings.Form_HODEditorA_Location _
 : Me.Size = My.Settings.Form_HODEditorA_Size

  ' Create the device.
  m_D3DManager.CreateDevice(pbxDisplay, Me.Text, False, False)

  ' Setup resonable texture filtering modes.
  m_D3DManager.SetupTextureFilteringModes(4)

  ' Create new HOD. Note that this step requires the device, and must
  ' be done before rendering as well.
  mnuFileNew_Click(Nothing, EventArgs.Empty)

  ' See if we have an initial filename.
  _SetFilename(My.Application.InitialFilename)

  ' Open HOD and reset camera.
  If m_Filename <> "" Then _
   _OpenHOD()

  ' Begin rendering.
  m_D3DManager.RenderLoopBegin()

 End Sub

 Private Sub HODEditorA_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
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

#End Region

#Region " Menus and Toolbar "

 ''' <summary>
 ''' Opens the HOD specified by the filename variable.
 ''' </summary>
 Private Function _OpenHOD() As Boolean
  Dim stream As IO.Stream

  If m_Filename = "" Then _
   Debug.Assert(False) _
 : Return False

  ' Try to initialize stream.
  stream = _TryOpenStream(m_Filename, True)

  If stream Is Nothing Then _
   Return False

  ' Initialize a new IOResult window.
  Dim f As New IOResult

  ' Reset MAD.
  m_MAD.Initialize()

  ' Backup HOD.
  _BackupFile(m_Filename)

  ' Unlock HOD.
  m_HOD.UnlockMeshes()

  ' Read the HOD.
  m_HOD.Read(stream)
  m_HOD.LockMeshes(m_D3DManager.Device)

  ' Dispose stream.
  stream.Dispose()

  ' See if this is a background HOD.
  If (m_HOD.Version = 1000) AndAlso (My.Settings.HOD_ProcessBGLighting) Then
   ' Try to load the lights file.
   Try
    ' First get full path.
    Dim lightFile As String = IO.Path.GetFullPath(m_Filename)

    ' Now add "_light", to name.
    lightFile = IO.Path.GetDirectoryName(lightFile) & "\" & _
                IO.Path.GetFileNameWithoutExtension(lightFile) & "_light" & _
                IO.Path.GetExtension(lightFile)

    ' Try to open the file.
    stream = _TryOpenStream(lightFile, True)

    If stream Is Nothing Then _
     Throw New Exception("Error while opening file '" & lightFile & "'.")

    ' Backup HOD.
    _BackupFile(lightFile)

    ' Make new HOD.
    Dim H As New HOD

    ' Read the HOD.
    H.Read(stream)

    ' Copy lights.
    For I As Integer = 0 To H.Lights.Count - 1
     m_HOD.Lights.Add(H.Lights(I))

    Next I ' For I As Integer = 0 To H.Lights.Count - 1

    ' Remove HOD.
    H = Nothing

    ' Dispose stream.
    stream.Dispose()

   Catch ex As Exception
    Trace.TraceError("Error occured while loading background lights file: " & vbCrLf & ex.ToString())

   End Try

  Else ' If (m_HOD.Version = 1000) AndAlso (My.Settings.HOD_ProcessBGLighting) Then
   ' Try to load animation file.
   Try
    ' First get full path.
    Dim madFile As String = IO.Path.GetFullPath(m_Filename)

    ' Now add "_light", to name.
    madFile = IO.Path.GetDirectoryName(madFile) & "\" & _
              IO.Path.GetFileNameWithoutExtension(madFile) & _
              ".mad"

    If IO.File.Exists(madFile) Then
     ' Try to open the file.
     stream = _TryOpenStream(madFile, True)

     If stream Is Nothing Then _
      Throw New Exception("Error while opening file '" & madFile & "'.")

     ' Backup MAD.
     _BackupFile(madFile)

     ' Open MAD file.
     m_MAD.Read(stream, m_HOD)

     ' Dispose stream.
     stream.Dispose()

    End If ' If IO.File.Exists(madFile) Then

   Catch ex As Exception
    Trace.TraceError("Error occured while loading animation file: " & vbCrLf & ex.ToString())

   End Try
  End If ' If (m_HOD.Version = 1000) AndAlso (My.Settings.HOD_ProcessBGLighting) Then

  ' Update tabs.
  _UpdateTabs()

  ' Reset camera.
  pbxDisplay_cmsReset_Click(Nothing, EventArgs.Empty)

  ' Reset light state.
  pbxDisplay_cmsLights.Enabled = True

  ' Display the result.
  f.Show(Me)

  Return True

 End Function

 ''' <summary>
 ''' Saves the HOD to the file specified by the filename variable.
 ''' </summary>
 Private Function _SaveHOD() As Boolean
  Dim stream As IO.Stream

  If m_Filename = "" Then _
   Debug.Assert(False) _
 : Return False

  ' Try to initialize stream.
  stream = _TryOpenStream(m_Filename, False)

  If stream Is Nothing Then _
   Return False

  ' Initialize a new IOResult window.
  Dim f As New IOResult

  ' Reset animation.
  m_MAD.Reset()

  ' Unlock meshes.
  m_HOD.UnlockMeshes()

  ' Write the HOD.
  m_HOD.Write(stream)

  ' Lock meshes.
  m_HOD.LockMeshes(m_D3DManager.Device)

  ' Dispose stream.
  stream.Dispose()

  ' See if this is a background HOD.
  If (m_HOD.Version = 1000) AndAlso (My.Settings.HOD_ProcessBGLighting) Then
   ' Try to load the lights file.
   Try
    If m_HOD.Lights.Count = 0 Then _
     Throw New Exception("No level lights. BG-Lighting file not written.")

    ' First get full path.
    Dim lightFile As String = IO.Path.GetFullPath(m_Filename)

    ' Now add "_light", to name.
    lightFile = IO.Path.GetDirectoryName(lightFile) & "\" & _
                IO.Path.GetFileNameWithoutExtension(lightFile) & "_light" & _
                IO.Path.GetExtension(lightFile)

    ' Try to open the file.
    stream = _TryOpenStream(lightFile, False)

    If stream Is Nothing Then _
     Throw New Exception("Error while opening file '" & lightFile & "'.")


    ' Create new HOD.
    Dim H As New HOD With { _
     .Version = &H200, _
     .Name = HOD.Name_MultiMesh _
    }

    ' Add lights.
    For I As Integer = 0 To m_HOD.Lights.Count - 1
     H.Lights.Add(New Light(m_HOD.Lights(I)))

    Next I ' For I As Integer = 0 To m_HOD.Lights.Count - 1

    ' Write to file.
    H.Write(stream)

    ' Remove HOD.
    H.Lights.Clear()
    H = Nothing

    ' Dispose stream.
    stream.Dispose()

   Catch ex As Exception
    Trace.TraceError("Error occured while saving background lights file: " & vbCrLf & ex.ToString())

   End Try

  Else ' If (m_HOD.Version = 1000) AndAlso (My.Settings.HOD_ProcessBGLighting) Then
   ' Try to save the animation file if needed.
   If m_MAD.Animations.Count <> 0 Then
    Try
     ' First get full path.
     Dim madFile As String = IO.Path.GetFullPath(m_Filename)

     ' Now add "_light", to name.
     madFile = IO.Path.GetDirectoryName(madFile) & "\" & _
               IO.Path.GetFileNameWithoutExtension(madFile) & _
               ".mad"

     ' Try to open the file.
     stream = _TryOpenStream(madFile, False)

     If stream Is Nothing Then _
      Throw New Exception("Error while opening file '" & madFile & "'.")

     ' Write to file.
     m_MAD.Write(stream)

     ' Dispose stream.
     stream.Dispose()

    Catch ex As Exception
     Trace.TraceError("Error occured while saving animation file: " & vbCrLf & ex.ToString())

    End Try
   End If ' If m_MAD.Animations.Count <> 0 Then
  End If ' If (m_HOD.Version = 1000) AndAlso (My.Settings.HOD_ProcessBGLighting) Then

  ' Display the result.
  f.Show(Me)

 End Function

 Private Sub mnuFileNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileNew.Click
  ' Reset filename.
  _SetFilename("")

  ' Pause render.
  m_D3DManager.RenderLoopPause()

  ' Initialize HOD.
  m_HOD.Initialize()
  m_HOD.LockMeshes(m_D3DManager.Device)

  ' Initialize MAD.
  m_MAD.Initialize()

  ' Resume render.
  m_D3DManager.RenderLoopResume()

  ' Set owner name.
  m_HOD.Owner = My.Settings.HOD_Owner

  ' Get type of HOD if clicked via menu.
  If sender Is mnuFileNew Then
   Dim f As New HODType(m_HOD)
   f.ShowDialog()
   f.Dispose()

  End If ' If sender is mnuFileNew then 

  ' Update tabs.
  _UpdateTabs()

  ' Reset camera.
  m_Camera.Reset(1001)

  ' Reset light state.
  pbxDisplay_cmsLights.Enabled = True

 End Sub

 Private Sub mnuFileOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileOpen.Click
        ' See if user clicked cancel.
        If OpenHODFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Set filename.
        _SetFilename(OpenHODFileDialog.FileName)

        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Try to open HOD.
        _OpenHOD()

        ' Resume render.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub mnuFileSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileSave.Click
        ' If no filename, then goto 'Save As' procedure.
        If m_Filename = "" Then _
   mnuFileSaveAs_Click(Nothing, EventArgs.Empty) _
        : Exit Sub

        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Save HOD.
        _SaveHOD()

        ' Resume render.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub mnuFileSaveAs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileSaveAs.Click
        ' See if user clicked cancel.
        If SaveHODFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Set filename.
        _SetFilename(SaveHODFileDialog.FileName)

        ' Save the file.
        mnuFileSave_Click(Nothing, EventArgs.Empty)

    End Sub

    Private Sub mnuFileExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileExit.Click
        ' Close the form.
        Me.Close()

    End Sub

    ''' <summary>Object on the CFHE clipboard.</summary>
    Private mnuEditClipboard As Object

    Private Sub mnuEditCut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditCut.Click
        ' First get the active control.
        Dim control As Control = Me.ActiveControl

        ' If no active control, then exit.
        If control Is Nothing Then _
   Exit Sub

        Do While TypeOf control Is ContainerControl
            ' Get the actual control.
            control = CType(control, ContainerControl).ActiveControl

            ' If no active control, then exit.
            If control Is Nothing Then _
    Exit Sub

        Loop ' Do While TypeOf control Is ContainerControl

        ' See if the control is a text box.
        If TypeOf control Is TextBox Then _
   CType(control, TextBox).Cut() _
        : mnuEditClipboard = Nothing _
        : Exit Sub

        ' Find the control which fired this event.
        If control Is lstTextures Then
            lstTextures_Cut()

        ElseIf control Is lstMaterials Then
            lstMaterials_Cut()

        ElseIf control Is lstMaterialParameters Then
            lstMaterialParameters_Cut()

        ElseIf control Is lstShipMeshes Then
            lstShipMeshes_Cut()

        ElseIf control Is cstShipMeshesLODs Then
            cstShipMeshesLODs_Cut()

        ElseIf control Is cstGoblins Then
            cstGoblins_Cut()

        ElseIf control Is cstBackgroundMeshes Then
            cstBackgroundMeshes_Cut()

        ElseIf control Is cstUIMeshes Then
            cstUIMeshes_Cut()

        ElseIf control Is tvwJoints Then
            tvwJoints_Cut()

        ElseIf control Is cstCM Then
            cstCM_Cut()

        ElseIf control Is cstEngineShapes Then
            cstEngineShapes_Cut()

        ElseIf control Is cstEngineGlows Then
            cstEngineGlows_Cut()

        ElseIf control Is cstEngineBurns Then
            cstEngineBurns_Cut()

        ElseIf control Is cstNavLights Then
            cstNavLights_Cut()

        ElseIf control Is cstMarkers Then
            cstMarkers_Cut()

        ElseIf control Is cstDockpaths Then
            cstDockpaths_Cut()

        ElseIf control Is cstLights Then
            cstLights_Cut()

        ElseIf control Is lstAnimations Then
            lstAnimations_Cut()

        End If

    End Sub

    Private Sub mnuEditCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditCopy.Click
        ' First get the active control.
        Dim control As Control = Me.ActiveControl

        ' If no active control, then exit.
        If control Is Nothing Then _
   Exit Sub

        Do While TypeOf control Is ContainerControl
            ' Get the actual control.
            control = CType(control, ContainerControl).ActiveControl

            ' If no active control, then exit.
            If control Is Nothing Then _
    Exit Sub

        Loop ' Do While TypeOf control Is ContainerControl

        ' See if the control is a text box.
        If TypeOf control Is TextBox Then _
   CType(control, TextBox).Copy() _
        : mnuEditClipboard = Nothing _
        : Exit Sub

        ' Find the control which fired this event.
        If control Is lstTextures Then
            lstTextures_Copy()

        ElseIf control Is lstMaterials Then
            lstMaterials_Copy()

        ElseIf control Is lstMaterialParameters Then
            lstMaterialParameters_Copy()

        ElseIf control Is lstShipMeshes Then
            lstShipMeshes_Copy()

        ElseIf control Is cstShipMeshesLODs Then
            cstShipMeshesLODs_Copy()

        ElseIf control Is cstGoblins Then
            cstGoblins_Copy()

        ElseIf control Is cstBackgroundMeshes Then
            cstBackgroundMeshes_Copy()

        ElseIf control Is cstUIMeshes Then
            cstUIMeshes_Copy()

        ElseIf control Is tvwJoints Then
            tvwJoints_Copy()

        ElseIf control Is cstCM Then
            cstCM_Copy()

        ElseIf control Is cstEngineShapes Then
            cstEngineShapes_Copy()

        ElseIf control Is cstEngineGlows Then
            cstEngineGlows_Copy()

        ElseIf control Is cstEngineBurns Then
            cstEngineBurns_Copy()

        ElseIf control Is cstNavLights Then
            cstNavLights_Copy()

        ElseIf control Is cstMarkers Then
            cstMarkers_Copy()

        ElseIf control Is cstDockpaths Then
            cstDockpaths_Copy()

        ElseIf control Is cstLights Then
            cstLights_Copy()

        ElseIf control Is lstAnimations Then
            lstAnimations_Copy()

        End If

    End Sub

    Private Sub mnuEditPaste_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditPaste.Click
        ' First get the active control.
        Dim control As Control = Me.ActiveControl

        ' If no active control, then exit.
        If control Is Nothing Then _
   Exit Sub

        Do While TypeOf control Is ContainerControl
            ' Get the actual control.
            control = CType(control, ContainerControl).ActiveControl

            ' If no active control, then exit.
            If control Is Nothing Then _
    Exit Sub

        Loop ' Do While TypeOf control Is ContainerControl

        ' See if the control is a text box.
        If TypeOf control Is TextBox Then _
   CType(control, TextBox).Paste() _
        : mnuEditClipboard = Nothing _
        : Exit Sub

        ' Find the control which fired this event.
        If control Is lstTextures Then
            lstTextures_Paste()

        ElseIf control Is lstMaterials Then
            lstMaterials_Paste()

        ElseIf control Is lstMaterialParameters Then
            lstMaterialParameters_Paste()

        ElseIf control Is lstShipMeshes Then
            lstShipMeshes_Paste()

        ElseIf control Is cstShipMeshesLODs Then
            cstShipMeshesLODs_Paste()

        ElseIf control Is cstGoblins Then
            cstGoblins_Paste()

        ElseIf control Is cstBackgroundMeshes Then
            cstBackgroundMeshes_Paste()

        ElseIf control Is cstUIMeshes Then
            cstUIMeshes_Paste()

        ElseIf control Is tvwJoints Then
            tvwJoints_Paste()

        ElseIf control Is cstCM Then
            cstCM_Paste()

        ElseIf control Is cstEngineShapes Then
            cstEngineShapes_Paste()

        ElseIf control Is cstEngineGlows Then
            cstEngineGlows_Paste()

        ElseIf control Is cstEngineBurns Then
            cstEngineBurns_Paste()

        ElseIf control Is cstNavLights Then
            cstNavLights_Paste()

        ElseIf control Is cstMarkers Then
            cstMarkers_Paste()

        ElseIf control Is cstDockpaths Then
            cstDockpaths_Paste()

        ElseIf control Is cstLights Then
            cstLights_Paste()

        ElseIf control Is lstAnimations Then
            lstAnimations_Paste()

        End If

    End Sub

    Private Sub mnuToolsOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsOptions.Click
        Dim f As New Options(m_HOD)

        ' Show the options display, then dispose.
        f.ShowDialog()
        f.Dispose()

    End Sub

    Private Sub mnuToolsRenormalMeshes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsRenormalMeshes.Click
        ' Make sure we have ship meshes\goblins first.
        If (m_HOD.MultiMeshes.Count = 0) AndAlso (m_HOD.GoblinMeshes.Count = 0) Then _
   Exit Sub

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Ship meshes.
        For I As Integer = 0 To m_HOD.MultiMeshes.Count - 1
            ' Make sure this mesh has LODs.
            If m_HOD.MultiMeshes(I).LOD.Count = 0 Then _
    Continue For

            ' Unlock meshes.
            m_HOD.MultiMeshes(I).Unlock()

            ' Calculate normals.
            For J As Integer = 0 To m_HOD.MultiMeshes(I).LOD.Count - 1
                ' Make sure this mesh has parts.
                If m_HOD.MultiMeshes(I).LOD(J).PartCount = 0 Then _
     Continue For

                ' Calculate normals.
                m_HOD.MultiMeshes(I).LOD(J).CalculateNormals()

            Next J ' For J As Integer = 0 To m_HOD.MultiMeshes(I).LOD.Count - 1

            ' Lock meshes.
            m_HOD.MultiMeshes(I).Lock(m_D3DManager.Device)

        Next I ' For I As Integer = 0 To m_HOD.MultiMeshes.Count - 1

        ' Goblin meshes.
        For I As Integer = 0 To m_HOD.GoblinMeshes.Count - 1
            ' Make sure this mesh has parts.
            If m_HOD.GoblinMeshes(I).Mesh.PartCount = 0 Then _
    Continue For

            ' Unlock mesh.
            m_HOD.GoblinMeshes(I).Mesh.Unlock()

            ' Calculate normals.
            m_HOD.GoblinMeshes(I).Mesh.CalculateNormals()

            ' Lock mesh.
            m_HOD.GoblinMeshes(I).Mesh.Lock(m_D3DManager.Device)

        Next I ' For I As Integer = 0 To m_HOD.GoblinMeshes.Count - 1

        ' Resume render.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub mnuToolsRetangentMeshes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuToolsRetangentMeshes.Click
        ' Make sure we have ship meshes\goblins first.
        If (m_HOD.MultiMeshes.Count = 0) AndAlso (m_HOD.GoblinMeshes.Count = 0) Then _
   Exit Sub

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Ship meshes.
        For I As Integer = 0 To m_HOD.MultiMeshes.Count - 1
            ' Make sure this mesh has LODs.
            If m_HOD.MultiMeshes(I).LOD.Count = 0 Then _
    Continue For

            ' Unlock meshes.
            m_HOD.MultiMeshes(I).Unlock()

            ' Calculate normals.
            For J As Integer = 0 To m_HOD.MultiMeshes(I).LOD.Count - 1
                ' Make sure this mesh has parts.
                If m_HOD.MultiMeshes(I).LOD(J).PartCount = 0 Then _
     Continue For

                ' Calculate tangents.
                m_HOD.MultiMeshes(I).LOD(J).CalculateTangents()

            Next J ' For J As Integer = 0 To m_HOD.MultiMeshes(I).LOD.Count - 1

            ' Lock meshes.
            m_HOD.MultiMeshes(I).Lock(m_D3DManager.Device)

        Next I ' For I As Integer = 0 To m_HOD.MultiMeshes.Count - 1

        ' Goblin meshes.
        For I As Integer = 0 To m_HOD.GoblinMeshes.Count - 1
            ' Make sure this mesh has parts.
            If m_HOD.GoblinMeshes(I).Mesh.PartCount = 0 Then _
    Continue For

            ' Unlock mesh.
            m_HOD.GoblinMeshes(I).Mesh.Unlock()

            ' Calculate tangents.
            m_HOD.GoblinMeshes(I).Mesh.CalculateTangents()

            ' Lock mesh.
            m_HOD.GoblinMeshes(I).Mesh.Lock(m_D3DManager.Device)

        Next I ' For I As Integer = 0 To m_HOD.GoblinMeshes.Count - 1

        ' Resume render.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub mnuToolsTranslate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuToolsTranslate.Click
        ' Variable to check if user presses cancel.
        Dim cancel As Boolean = False

        ' Set the vector.
        Dim v As Vector3 = _AskVector3FromUser(cancel)

        ' See if user pressed cancel.
        If cancel Then _
   Exit Sub

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Unlock meshes.
        m_HOD.UnlockMeshes()

        ' Perform transform.
        m_HOD.Translation(v)

        ' Lock meshes.
        m_HOD.LockMeshes(m_D3DManager.Device)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub mnuToolsRotate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuToolsRotate.Click
        ' Variable to check if user presses cancel.
        Dim cancel As Boolean = False

        ' Set the vector.
        Dim v As Vector3 = _AskVector3FromUser(cancel)

        ' See if user pressed cancel.
        If cancel Then _
   Exit Sub

        ' Convert to radians.
        v *= CSng(Math.PI / 180)

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Unlock meshes.
        m_HOD.UnlockMeshes()

        ' Perform transform.
        m_HOD.Rotation(v)

        ' Lock meshes.
        m_HOD.LockMeshes(m_D3DManager.Device)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub mnuToolsScale_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuToolsScale.Click
        ' Variable to check if user presses cancel.
        Dim cancel As Boolean = False

        ' Set the vector.
        Dim v As Vector3 = _AskVector3FromUser(cancel, False, False)

        ' See if user pressed cancel.
        If cancel Then _
   Exit Sub

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Unlock meshes.
        m_HOD.UnlockMeshes()

        ' Perform transform.
        m_HOD.Scaling(v)

        ' Lock meshes.
        m_HOD.LockMeshes(m_D3DManager.Device)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub mnuToolsMirror_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuToolsMirror.Click
        Dim v As Vector3

        Select Case MsgBox("Mirror about YZ plane?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question, Me.Text)
            Case MsgBoxResult.Yes
                v.X = 1

            Case MsgBoxResult.No
                v.X = 0

            Case MsgBoxResult.Cancel
                Exit Sub

        End Select ' Select Case MsgBox("Mirror about YZ plane?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question, Me.Text)

        Select Case MsgBox("Mirror about XZ plane?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question, Me.Text)
            Case MsgBoxResult.Yes
                v.Y = 1

            Case MsgBoxResult.No
                v.Y = 0

            Case MsgBoxResult.Cancel
                Exit Sub

        End Select ' Select Case MsgBox("Mirror about XZ plane?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question, Me.Text)

        Select Case MsgBox("Mirror about XY plane?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question, Me.Text)
            Case MsgBoxResult.Yes
                v.Z = 1

            Case MsgBoxResult.No
                v.Z = 0

            Case MsgBoxResult.Cancel
                Exit Sub

        End Select ' Select Case MsgBox("Mirror about XY plane?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question, Me.Text)

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Unlock meshes.
        m_HOD.UnlockMeshes()

        ' Perform transform.
        m_HOD.Mirroring(v)

        ' Lock meshes.
        m_HOD.LockMeshes(m_D3DManager.Device)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub mnuToolsVARYToMULT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuToolsVARYToMULT.Click
        If m_HOD.VariableMeshes.Count = 0 Then _
   MsgBox("No progressive meshes in HOD!", MsgBoxStyle.Exclamation, Me.Text) _
        : Exit Sub

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Unlock meshes.
        m_HOD.UnlockMeshes()

        For I As Integer = m_HOD.VariableMeshes.Count - 1 To 0
            ' Convert and insert multi mesh to HOD.
            m_HOD.MultiMeshes.Insert(0, m_HOD.VariableMeshes(I).ToMultiMesh())

            ' Remove variable mesh.
            m_HOD.VariableMeshes.RemoveAt(I)

        Next I ' For I As Integer = m_HOD.VariableMeshes.Count - 1 To 0

        ' Lock meshes.
        m_HOD.LockMeshes(m_D3DManager.Device)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub mnuHelpAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHelpAbout.Click
        Dim f As New AboutBox

        ' Show the about display, then dispose.
        f.ShowDialog()
        f.Dispose()

    End Sub

    Private Sub tspNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tspNew.Click
        mnuFileNew_Click(mnuFileNew, EventArgs.Empty)

    End Sub

    Private Sub tspOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tspOpen.Click
        mnuFileOpen_Click(mnuFileOpen, EventArgs.Empty)

    End Sub

    Private Sub tspSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tspSave.Click
        mnuFileSave_Click(mnuFileSave, EventArgs.Empty)

    End Sub

    Private Sub tspCut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tspCut.Click
        mnuEditCut_Click(mnuEditCut, EventArgs.Empty)

    End Sub

    Private Sub tspCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tspCopy.Click
        mnuEditCopy_Click(mnuEditCopy, EventArgs.Empty)

    End Sub

    Private Sub tspPaste_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tspPaste.Click
        mnuEditPaste_Click(mnuEditPaste, EventArgs.Empty)

    End Sub

    Private Sub tspMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tspMode.SelectedIndexChanged
        With tabMain.TabPages
            ' Remove all tabs.
            .Clear()

            Select Case CStr(tspMode.SelectedItem)
                Case "Background"
                    ' This is a background mesh HOD.
                    .Add(tabBGMS)

                    If My.Settings.HOD_ProcessBGLighting Then _
      .Add(tabLights)

                    .Add(tabStarFields)
                    .Add(tabStarFieldsT)

                    ' Update currently selected tab.
                    tabBGMS_Enter(Nothing, EventArgs.Empty)

                Case "UI"
                    ' This is a simple mesh HOD.
                    .Add(tabUI)

                    ' Update currently selected tab.
                    tabUI_Enter(Nothing, EventArgs.Empty)

                Case "Model"
                    ' This is a multi mesh HOD.
                    .Add(tabTextures)
                    .Add(tabMaterials)
                    .Add(tabMultiMeshes)
                    .Add(tabGoblins)
                    .Add(tabJoints)
                    .Add(tabEngineShapes)
                    .Add(tabEngineGlows)
                    .Add(tabEngineBurns)
                    .Add(tabNavLights)
                    .Add(tabDockpaths)
                    .Add(tabMarkers)
                    .Add(tabCM)

                    If Not My.Settings.HOD_ProcessBGLighting Then _
      .Add(tabLights)

                    ' Update currently selected tab.
                    tabTextures_Enter(Nothing, EventArgs.Empty)

                    ' Reset animation.
                    m_MAD.Reset()

                Case "Animation"
                    ' Add animation tab.
                    .Add(tabAnimations)

                    ' Update currently selected tab.
                    tabAnimations_Enter(Nothing, EventArgs.Empty)

            End Select ' Select Case CStr(tspMode.SelectedItem)

        End With ' With tabMain.TabPages 

    End Sub

    Private Sub tspObjectScale_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tspObjectScale.TextChanged
        ' Set scale.
        If IsNumeric(tspObjectScale.Text) Then _
   m_ObjectScale = CSng(tspObjectScale.Text)

        ' Update scales.
        m_HOD.SkeletonScale = m_ObjectScale * m_SkeletonScale
        m_HOD.MarkerScale = m_ObjectScale * m_MarkerScale
        m_HOD.DockpointScale = m_ObjectScale * m_DockpointScale

    End Sub

    Private Sub tspCameraSensitivity_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tspCameraSensitivity.TextChanged
        If IsNumeric(tspCameraSensitivity.Text) Then _
   m_CameraSensitivity = CSng(tspCameraSensitivity.Text)

    End Sub

#End Region

#Region " Picture Box UI "

    ''' <summary>
    ''' The number of seconds in which a 'MouseDown' and 'MouseUp' event must occur
    ''' in order to display the context menu, and not rotate view.
    ''' </summary>
    Private Const pbxDisplay_RightClickThreshold As Double = 0.2

    ''' <summary>
    ''' Last time the picture box was clicked on.
    ''' </summary>
    Private pbxDisplay_LaskClickTime As Double

    ''' <summary>
    ''' Whether to ignore the next 'MouseMove' event. This is reset after
    ''' ignoring the event.
    ''' </summary>
    Private pbxDisplay_IgnoreMouseMove As Boolean

    Private Sub pbxDisplay_cms_Closed(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripDropDownClosedEventArgs) Handles pbxDisplay_cms.Closed
        ' Ignore the first mouse move event.
        pbxDisplay_IgnoreMouseMove = True

    End Sub

    Private Sub pbxDisplay_cmsReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbxDisplay_cmsReset.Click
        Dim v As Vector3, r As Single

        ' Get the mesh's radius.
        m_HOD.GetHODSphere(v, r)

        ' Calculate total distance from origin.
        r += v.Length()

        ' Check for zero.
        If r <= 0 Then _
   r = 1

        ' Set the clip plane near and far distances.
        Select Case r
            Case Is < 10 ' UI meshes\very small ships.
                m_Camera.SetZ(0.01F, 100.0F)

            Case 10 To 100 ' Small ships.
                m_Camera.SetZ(0.1F, 1000.0F)

            Case 100 To 1000 ' Medium ships.
                m_Camera.SetZ(1.0F, 10000.0F)

            Case Is > 1000 ' Large ships\megaliths.
                m_Camera.SetZ(5.0F, 50000.0F)

        End Select ' Select Case r

        ' Set fog states.
        With m_D3DManager.Device
            .RenderState.FogColor = Color.Black
            .RenderState.FogTableMode = Direct3D.FogMode.Linear
            .RenderState.FogStart = 0.25F * m_Camera.ZFar
            .RenderState.FogEnd = 0.69F * m_Camera.ZFar
            .RenderState.FogEnable = True

        End With

        ' Set distance.
        m_Camera.Reset(1.25F * r)

        ' Set scales.
        m_SkeletonScale = r / 100
        m_MarkerScale = r / 25
        m_DockpointScale = r / 10

        ' Update
        tspObjectScale_TextChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub pbxDisplay_cmsWireframeSolid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbxDisplay_cmsWireframe.Click, pbxDisplay_cmsSolid.Click
        ' Set flag.
        m_RenderWireframe = (sender Is pbxDisplay_cmsWireframe)

        ' Enable\Disable textures.
        HOD.EnableTextures = Not m_RenderWireframe

        ' Update checks.
        pbxDisplay_cmsWireframe.Checked = m_RenderWireframe
        pbxDisplay_cmsSolid.Checked = Not m_RenderWireframe

    End Sub

    Private Sub pbxDisplay_cmsWireframeOverlay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbxDisplay_cmsWireframeOverlay.Click
        ' Toggle state.
        pbxDisplay_cmsWireframeOverlay.Checked = Not pbxDisplay_cmsWireframeOverlay.Checked

    End Sub

    Private Sub pbxDisplay_cmsLights_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles pbxDisplay_cmsLights.Click
        ' Toggle state.
        pbxDisplay_cmsLights.Checked = Not pbxDisplay_cmsLights.Checked

    End Sub

    Private Sub pbxDisplay_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbxDisplay.MouseDown
        ' Remember the last time mouse was clicked.
        pbxDisplay_LaskClickTime = Microsoft.VisualBasic.Timer

    End Sub

    Private Sub pbxDisplay_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbxDisplay.MouseMove
        ' This static variable is being assigned values so that a first move
        ' doesn't cause a jump.
        Static OldPos As New Vector2(e.Location.X, e.Location.Y)

        ' Get the new position, and delta.
        Dim NewPos As New Vector2(e.Location.X, e.Location.Y),
      Delta As Vector2 = NewPos - OldPos

        ' If we are to ignore mouse move, then do so.
        If Not pbxDisplay_IgnoreMouseMove Then
            Select Case e.Button
                Case Windows.Forms.MouseButtons.Left
                    m_Camera.CameraRotate(m_CameraSensitivity * Delta)

                Case Windows.Forms.MouseButtons.Right
                    m_Camera.CameraZoom(m_CameraSensitivity * Delta)

                Case Windows.Forms.MouseButtons.Middle,
         Windows.Forms.MouseButtons.Left Or
         Windows.Forms.MouseButtons.Right
                    m_Camera.CameraPan(m_CameraSensitivity * NewPos,
                        m_CameraSensitivity * OldPos,
                        Matrix.Identity)

            End Select ' Select Case e.Button

        Else ' End If ' If Not pbxDisplay_IgnoreMouseMove Then
            ' Do not ignore next event.
            pbxDisplay_IgnoreMouseMove = False

        End If ' If Not pbxDisplay_IgnoreMouseMove Then

        ' Store new position.
        OldPos = NewPos

    End Sub

    Private Sub pbxDisplay_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbxDisplay.MouseUp
        If e.Button <> Windows.Forms.MouseButtons.Right Then _
   Exit Sub

        ' Get the time for which the mouse was pressed.
        Dim timePressed As Double = Microsoft.VisualBasic.Timer - pbxDisplay_LaskClickTime

        ' If it's less than threshold, display context menu.
        If timePressed < pbxDisplay_RightClickThreshold Then
            ' Decide a direction for the menu.
            Dim orientation As ToolStripDropDownDirection

            ' First see if it cannot be accomodated in both dimensions, then perform
            ' checks for one dimension.
            If (e.Location.X + pbxDisplay_cms.Width > pbxDisplay.ClientSize.Width) AndAlso
      (e.Location.Y + pbxDisplay_cms.Height > pbxDisplay.ClientSize.Height) Then
                ' The menu cannot be accomodated, horizontally and vertically.
                orientation = ToolStripDropDownDirection.AboveLeft

            ElseIf (e.Location.X + pbxDisplay_cms.Width > pbxDisplay.ClientSize.Width) Then
                ' The menu cannot be accomodated, horizontally only.
                orientation = ToolStripDropDownDirection.BelowLeft

            ElseIf (e.Location.Y + pbxDisplay_cms.Height > pbxDisplay.ClientSize.Height) Then
                ' The menu cannot be accomodated, vertically only.
                orientation = ToolStripDropDownDirection.AboveRight

            Else
                ' The menu can be accomodated, both horizontally and vertically.
                orientation = ToolStripDropDownDirection.BelowRight

            End If ' If e.Location.X + pbxDisplay_cms.Width > pbxDisplay.ClientSize.Width Then

            ' Display the menu.
            pbxDisplay_cms.Show(pbxDisplay, e.Location, orientation)

        End If ' If timePressed < pbxDisplay_RightClickThreshold Then

    End Sub

    Private Sub pbxDisplay_cmsEditLight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbxDisplay_cmsEditLight.Click
        ' Create a new light editor.
        Dim f As New LightEditor(m_Light)

        ' Show the dialog and then dispose it.
        f.ShowDialog()
        f.Dispose()

    End Sub

#End Region

#Region " Misc. UI "

    ''' <summary>
    ''' Updates tabs to reflect the HOD type.
    ''' </summary>
    Private Sub _UpdateTabs()
        With tspMode.Items
            ' Remove existing items.
            .Clear()

            ' Is this a background HOD?
            If m_HOD.Version = 1000 Then
                ' This is a background mesh HOD.
                .Add("Background")

            Else ' If m_HOD.Version = 1000 Then
                ' See if this is a simple mesh HOD, or a multi mesh HOD.
                If (m_HOD.Name = HOD.Name_SimpleMesh) OrElse
       (m_HOD.Name = HOD.Name_WireframeMesh) Then _
     .Add("UI") _
    Else _
     .Add("Model") _
                : .Add("Animation")

            End If ' If m_HOD.Version = 1000 Then

        End With ' With tspMode.Items

        ' Select first item.
        tspMode.SelectedIndex = 0

    End Sub

    Private Sub TextBox_NonNegative_Integer_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
 Handles txtEngineGlowsLOD.Validating, txtNavLightsSection.Validating

        Dim int As Integer

        ' Get the text box.
        Dim TextBox As TextBox = CType(sender, TextBox)

        ' Make sure the text box is enabled.
        If Not TextBox.Enabled Then _
   e.Cancel = False _
        : Exit Sub

        ' Assume that the validation fails.
        e.Cancel = True

        ' Check for no input.
        If TextBox.Text = "" Then _
   ErrorProvider.SetError(TextBox, "Please enter a value.") _
        : Exit Sub

        ' Check for non-numeric input.
        If Not Integer.TryParse(TextBox.Text, int) Then _
   ErrorProvider.SetError(TextBox, "Please enter a numeric, integral value.") _
        : Exit Sub

        ' Check if it's negative.
        If int < 0 Then _
   ErrorProvider.SetError(TextBox, "Please enter a positive value.") _
        : Exit Sub

        ' Validation succeeded.
        e.Cancel = False

    End Sub

    Private Sub TextBox_Decimal_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
 Handles txtMaterialColourR.Validating, txtMaterialColourG.Validating, txtMaterialColourB.Validating, txtMaterialColourA.Validating,
         txtJointsPositionX.Validating, txtJointsPositionY.Validating, txtJointsPositionZ.Validating,
         txtJointsRotationX.Validating, txtJointsRotationY.Validating, txtJointsRotationZ.Validating,
         txtJointsAxisX.Validating, txtJointsAxisY.Validating, txtJointsAxisZ.Validating,
         txtEngineBurn1X.Validating, txtEngineBurn1Y.Validating, txtEngineBurn1Z.Validating,
         txtEngineBurn2X.Validating, txtEngineBurn2Y.Validating, txtEngineBurn2Z.Validating,
         txtEngineBurn3X.Validating, txtEngineBurn3Y.Validating, txtEngineBurn3Z.Validating,
         txtEngineBurn4X.Validating, txtEngineBurn4Y.Validating, txtEngineBurn4Z.Validating,
         txtEngineBurn5X.Validating, txtEngineBurn5Y.Validating, txtEngineBurn5Z.Validating,
         txtCMMinX.Validating, txtCMMinY.Validating, txtCMMinZ.Validating,
         txtCMMaxX.Validating, txtCMMaxY.Validating, txtCMMaxZ.Validating,
         txtCMCX.Validating, txtCMCY.Validating, txtCMCZ.Validating,
         txtNavlightsColourR.Validating, txtNavlightsColourG.Validating, txtNavlightsColourB.Validating,
         txtMarkerPositionX.Validating, txtMarkerPositionY.Validating, txtMarkerPositionZ.Validating,
         txtMarkerRotationX.Validating, txtMarkerRotationY.Validating, txtMarkerRotationZ.Validating,
         txtDockpathsKeyframePositionX.Validating, txtDockpathsKeyframePositionY.Validating, txtDockpathsKeyframePositionZ.Validating,
         txtDockpathsKeyframeRotationX.Validating, txtDockpathsKeyframeRotationY.Validating, txtDockpathsKeyframeRotationZ.Validating,
         txtLightTX.Validating, txtLightTY.Validating, txtLightTZ.Validating,
         txtLightCR.Validating, txtLightCG.Validating, txtLightCB.Validating,
         txtLightSR.Validating, txtLightSG.Validating, txtLightSB.Validating,
         txtAnimationsJointsPX.Validating, txtAnimationsJointsPY.Validating, txtAnimationsJointsPZ.Validating,
         txtAnimationsJointsRX.Validating, txtAnimationsJointsRY.Validating, txtAnimationsJointsRZ.Validating

        ' Get the text box.
        Dim TextBox As TextBox = CType(sender, TextBox)

        ' Make sure the text box is enabled.
        If Not TextBox.Enabled Then _
   e.Cancel = False _
        : Exit Sub

        ' Assume that the validation fails.
        e.Cancel = True

        ' Check for no input.
        If TextBox.Text = "" Then _
   ErrorProvider.SetError(TextBox, "Please enter a value.") _
        : Exit Sub

        ' Check for non-numeric input.
        If Not IsNumeric(TextBox.Text) Then _
   ErrorProvider.SetError(TextBox, "Please enter a numeric value.") _
        : Exit Sub

        ' Validation succeeded.
        e.Cancel = False

    End Sub

    Private Sub TextBox_Positive_Decimal_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
 Handles txtCMRadius.Validating, txtNavLightsSize.Validating, txtNavLightsPhase.Validating,
         txtNavLightsFrequency.Validating, txtNavLightsDistance.Validating, txtDockpathsGlobalTolerance.Validating,
         txtDockpathsKeyframeTolerance.Validating, txtDockpathsKeyframeMaxSpeed.Validating,
         txtLightAttDist.Validating, txtAnimationsST.Validating, txtAnimationsET.Validating,
         txtAnimationsLST.Validating, txtAnimationsLET.Validating, txtAnimationsJointsTime.Validating

        ' Get the text box.
        Dim TextBox As TextBox = CType(sender, TextBox)

        ' Make sure the text box is enabled.
        If Not TextBox.Enabled Then _
   e.Cancel = False _
        : Exit Sub

        ' Assume that the validation fails.
        e.Cancel = True

        ' Check for no input.
        If TextBox.Text = "" Then _
   ErrorProvider.SetError(TextBox, "Please enter a value.") _
        : Exit Sub

        ' Check for non-numeric input.
        If Not IsNumeric(TextBox.Text) Then _
   ErrorProvider.SetError(TextBox, "Please enter a numeric value.") _
        : Exit Sub

        ' Check for non-zero positive input.
        If CSng(TextBox.Text) < 0.0F Then _
   ErrorProvider.SetError(TextBox, "Please enter a positive value.") _
        : Exit Sub

        ' Validation succeeded.
        e.Cancel = False

    End Sub

    Private Sub TextBox_Positive_NonZero_Decimal_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
 Handles txtJointsScaleX.Validating, txtJointsScaleY.Validating, txtJointsScaleZ.Validating

        ' Get the text box.
        Dim TextBox As TextBox = CType(sender, TextBox)

        ' Make sure the text box is enabled.
        If Not TextBox.Enabled Then _
   e.Cancel = False _
        : Exit Sub

        ' Assume that the validation fails.
        e.Cancel = True

        ' Check for no input.
        If TextBox.Text = "" Then _
   ErrorProvider.SetError(TextBox, "Please enter a value.") _
        : Exit Sub

        ' Check for non-numeric input.
        If Not IsNumeric(TextBox.Text) Then _
   ErrorProvider.SetError(TextBox, "Please enter a numeric value.") _
        : Exit Sub

        ' Check for non-zero positive input.
        If CSng(TextBox.Text) <= 0.0F Then _
   ErrorProvider.SetError(TextBox, "Please enter a non-zero positive value.") _
        : Exit Sub

        ' Validation succeeded.
        e.Cancel = False

    End Sub

#End Region

#Region " Textures UI "

    ''' <summary>
    ''' Returns the texture filename (without extension) from a given texture
    ''' path.
    ''' </summary>
    Private Shared Function _GetTextureFilenameFromPath(ByVal path As String) As String
        ' Get the extension.
        Dim ext As String = IO.Path.GetExtension(path)

        ' Return the file name only, if there is no extension.
        If ext = "" Then _
   Return IO.Path.GetFileNameWithoutExtension(path)

        ' Get the number in the extension.
        Dim number As String = ""

        For I As Integer = 0 To ext.Length - 1
            ' If it's a digit, append it to the number string.
            If Char.IsDigit(ext(I)) Then _
    number &= ext(I)

        Next I ' For I As Integer = 0 To ext.Length - 1

        ' Enclose the number string in brackets.
        If number <> "" Then _
   number = "[" & number & "]"

        ' Return the file name with the [x] tag.
        Return IO.Path.GetFileNameWithoutExtension(path) & number

    End Function

    Private Sub tabTextures_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabTextures.Enter
        ' See if the tab enter event is marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        With lstTextures.Items
            ' Remove old entries.
            .Clear()

            ' Add new textures.
            For I As Integer = 0 To m_HOD.Textures.Count - 1
                .Add(m_HOD.Textures(I).ToString())

            Next I ' For I As Integer = 0 To m_HOD.Textures.Count - 1

        End With ' With lstTextures.Items

        ' Update selection.
        lstTextures_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub lstTextures_Cut()
        ' See if any item is selected.
        If lstTextures.SelectedIndex = -1 Then _
   Exit Sub

        ' Get it's index.
        Dim ind As Integer = lstTextures.SelectedIndex

        ' Copy to clipboard.
        mnuEditClipboard = New Texture(m_HOD.Textures(ind))

        ' Remove from HOD.
        m_HOD.Textures.RemoveAt(ind)

        ' Refresh.
        lstTextures.Items.RemoveAt(ind)

    End Sub

    Private Sub lstTextures_Copy()
        ' See if any item is selected.
        If lstTextures.SelectedIndex = -1 Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New Texture(m_HOD.Textures(lstTextures.SelectedIndex))

    End Sub

    Private Sub lstTextures_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is Texture Then _
   Exit Sub

        ' Get texture
        Dim tex As Texture = CType(mnuEditClipboard, Texture)

        ' Add to HOD.
        m_HOD.Textures.Add(tex)

        ' Refresh.
        lstTextures.Items.Add(tex.ToString())

        ' Remove reference.
        mnuEditClipboard = Nothing

    End Sub

    Private Sub lstTextures_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTextures.SelectedIndexChanged
        If lstTextures.SelectedIndex = -1 Then
            ' Since no texture is selected, disable all UI controls
            ' except the add button.
            cmdTextureRemove.Enabled = False
            cmdTextureImport.Enabled = False
            cmdTextureExport.Enabled = False
            cmdTexturePreview.Enabled = False

            ' Reset labels.
            lblTextureDimensions.Text = "-"
            lblTextureMipCount.Text = "-"
            lblTextureFormat.Text = "-"
            lblTexturePath.Text = "-"

        Else ' If lstTextures.SelectedIndex = -1 Then
            ' Since a texture is selected, enable all UI controls.
            cmdTextureRemove.Enabled = True
            cmdTextureImport.Enabled = True
            cmdTextureExport.Enabled = True
            cmdTexturePreview.Enabled = True

            ' Update the labels.
            With m_HOD.Textures(lstTextures.SelectedIndex)
                lblTextureDimensions.Text = CStr(.Width) & " x " & CStr(.Height) & " pixels."
                lblTextureMipCount.Text = CStr(.NumMips)
                lblTextureFormat.Text = .FourCC
                lblTexturePath.Text = .Path

            End With ' With m_HOD.Textures(lstTextures.SelectedIndex)
        End If ' If lstTextures.SelectedIndex = -1 Then

    End Sub

    Private Sub cmdTextureAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTextureAdd.Click
        Dim number As Integer = lstTextures.Items.Count + 1
        Dim name As String

        Do
            ' Decide a name for texture.
            name = "Texture " & CStr(number)

            ' See if it's a duplicate.
            If _ListBoxHasString(lstTextures.Items, name) Then _
    number += 1 _
            : Continue Do

            ' Name is OK. Add the texture.
            m_HOD.Textures.Add(New Texture With {.Path = name})

            ' Update list.
            lstTextures.Items.Add(name)
            lstTextures.SelectedIndex = lstTextures.Items.Count - 1

            ' Exit loop
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cmdTextureRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTextureRemove.Click
        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Remove the texture.
        m_HOD.Textures.RemoveAt(lstTextures.SelectedIndex)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Update list.
        lstTextures.Items.RemoveAt(lstTextures.SelectedIndex)
        lstTextures.SelectedIndex = lstTextures.Items.Count - 1

    End Sub

    Private Sub cmdTexturePreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTexturePreview.Click
        ' See if the texture has mips.
        If m_HOD.Textures(lstTextures.SelectedIndex).NumMips = 0 Then _
   MsgBox("No texture to display!", MsgBoxStyle.Exclamation, Me.Text) _
        : Exit Sub

        ' Get the texture in a memory stream.
        Dim stream As New IO.MemoryStream
        Dim BW As New IO.BinaryWriter(stream)

        ' Write the texture to stream.
        m_HOD.Textures(lstTextures.SelectedIndex).Write(BW)

        ' Rewind stream.
        stream.Position = 0

        ' Create the form.
        Dim f As New TexturePreview(stream)

        ' Dispose stream.
        BW.Close()
        stream.Dispose()

        ' Display the form.
        f.ShowDialog()
        f.Dispose()

    End Sub

    Private Sub cmdTextureImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTextureImport.Click
        Dim stream As IO.FileStream

        ' See if the user pressed cancel.
        If OpenTextureFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Try to create a stream.
        stream = _TryOpenStream(OpenTextureFileDialog.FileName, True)

        If stream Is Nothing Then _
   Exit Sub

        ' Create a new binary reader.
        Dim BR As New IO.BinaryReader(stream)

        ' Initialize a new IOResult window.
        Dim f As New IOResult

        ' Pause rendering
        m_D3DManager.RenderLoopPause()

        ' Create a new texture.
        Dim t As New Texture

        ' Import the texture.
        t.Read(BR)

        ' See if the read was successful.
        If t.NumMips <> 0 Then _
   m_HOD.Textures(lstTextures.SelectedIndex) = t _
        : m_HOD.Textures(lstTextures.SelectedIndex).Path = OpenTextureFileDialog.FileName

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Display the result.
        f.Show(Me)

        ' Update the list.
        lstTextures.Items(lstTextures.SelectedIndex) = m_HOD.Textures(lstTextures.SelectedIndex).ToString()

        ' Close the reader and dispose the stream.
        BR.Close()
        stream.Dispose()

    End Sub

    Private Sub cmdTextureExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTextureExport.Click
        Dim stream As IO.FileStream
        Dim BW As IO.BinaryWriter

        Const DDSFilter As String = "DirectDraw Surface (*.dds)|*.dds"
        Const TGAFilter As String = "Truevision Targa file (*.tga)|*.tga"

        Dim tex As Texture = m_HOD.Textures(lstTextures.SelectedIndex)

        ' Check if there are mips.
        If tex.NumMips = 0 Then _
   MsgBox("No texture to export!", MsgBoxStyle.Exclamation, Me.Text) _
        : Exit Sub

        ' Setup the open file dialog.
        If (tex.FourCC = "8888") AndAlso (tex.NumMips = 1) Then _
   SaveTextureFileDialog.Filter = TGAFilter _
        : SaveTextureFileDialog.DefaultExt = "tga" _
  Else _
   SaveTextureFileDialog.Filter = DDSFilter _
        : SaveTextureFileDialog.DefaultExt = "dds"

        ' See if the user pressed cancel.
        If SaveTextureFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Try to create a stream.
        stream = _TryOpenStream(SaveTextureFileDialog.FileName, False)

        If stream Is Nothing Then _
   Exit Sub

        ' Create a binary writer.
        BW = New IO.BinaryWriter(stream)

        ' Initialize a new IOResult window.
        Dim f As New IOResult

        ' Export the texture.
        m_HOD.Textures(lstTextures.SelectedIndex).Write(BW)

        ' Display the result.
        f.Show(Me)

        ' Close the reader and dispose the stream.
        BW.Close()
        stream.Dispose()

    End Sub

    Private Sub cmdTexturesExportAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTexturesExportAll.Click
        ' See if the user pressed cancel.
        If FolderBrowserDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Initialize a new IOResult window.
        Dim f As New IOResult

        ' Try to export each texture.
        For I As Integer = 0 To m_HOD.Textures.Count - 1
            ' See if the texture has mips.
            If m_HOD.Textures(I).NumMips = 0 Then _
    Trace.TraceInformation("Texture " & CStr(I) & " not exported because it has no mips!") _
            : Continue For

            ' Get the extension for the texture.
            Dim ext As String

            ' See if it's a TGA or DDS.
            If m_HOD.Textures(I).FourCC = "8888" Then _
    ext = ".tga" _
   Else _
    ext = ".dds"

            ' Decide the file name for the texture.
            Dim filename As String = FolderBrowserDialog.SelectedPath & "\" &
                            _GetTextureFilenameFromPath(m_HOD.Textures(I).Path) &
                            ext

            ' Try to open the file...
            Dim stream As IO.FileStream = _TryOpenStream(filename, False)

            ' See if it opened.
            If stream Is Nothing Then _
    Trace.TraceWarning("Coudn't open file '" & filename & "' for writing.") _
            : Continue For

            ' Open a binary writer.
            Dim BW As New IO.BinaryWriter(stream)

            ' Write the texture.
            m_HOD.Textures(I).Write(BW)

            ' Close the writer and dispose the stream.
            BW.Close()
            stream.Dispose()

        Next I ' For I As Integer = 0 To m_HOD.Textures.Count - 1

        ' Display the result.
        f.Show(Me)

    End Sub

#End Region

#Region " Materials UI "

    Private Sub tabMaterials_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabMaterials.Enter
        ' See if the tab enter events are marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        ' Update material texture index combo box.
        With cboMaterialTextureIndex.Items
            ' Remove all textures.
            .Clear()

            ' Add textures.
            For I As Integer = 0 To m_HOD.Textures.Count - 1
                .Add(m_HOD.Textures(I).ToString())

            Next I ' For I As Integer = 0 To m_HOD.Textures.Count - 1
        End With ' With cboMaterialTextureIndex.Items

        ' Update the materials list.
        With lstMaterials.Items
            ' Remove all materials.
            .Clear()

            ' Add materials.
            For I As Integer = 0 To m_HOD.Materials.Count - 1
                .Add(m_HOD.Materials(I).ToString())

            Next I ' For I As Integer = 0 To m_HOD.Materials.Count - 1
        End With ' With lstMaterials.Items

        ' Update selection.
        lstMaterials_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub lstMaterials_Cut()
        ' See if any item is selected.
        If lstMaterials.SelectedIndex = -1 Then _
   Exit Sub

        ' Get it's index.
        Dim ind As Integer = lstMaterials.SelectedIndex

        ' Copy to clipboard.
        mnuEditClipboard = New Material(m_HOD.Materials(ind))

        ' Remove from HOD.
        m_HOD.Materials.RemoveAt(ind)

        ' Refresh.
        lstMaterials.Items.Remove(ind)

    End Sub

    Private Sub lstMaterials_Copy()
        ' See if any item is selected.
        If lstMaterials.SelectedIndex = -1 Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New Material(m_HOD.Materials(lstMaterials.SelectedIndex))

    End Sub

    Private Sub lstMaterials_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is Material Then _
   Exit Sub

        ' Get the item.
        Dim mat As Material = CType(mnuEditClipboard, Material)

        ' Remove reference.
        mnuEditClipboard = Nothing

        ' See if it's already present.
        If Not lstMaterials.Items.Contains(mat.ToString()) Then _
   m_HOD.Materials.Add(mat) _
        : lstMaterials.Items.Add(mat.ToString()) _
        : Exit Sub

        ' Rename material.
        Dim number As Integer = 2

        Do
            ' See if this name is present.
            If lstMaterials.Items.Contains(mat.ToString() & CStr(number)) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, add it.
            mat.MaterialName &= CStr(number)
            m_HOD.Materials.Add(mat)
            lstMaterials.Items.Add(mat.ToString())

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub lstMaterials_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstMaterials.SelectedIndexChanged
        If lstMaterials.SelectedIndex = -1 Then
            ' Since no material is selected, disable all UI controls,
            ' except the add button.
            cmdMaterialRemove.Enabled = False
            cmdMaterialRename.Enabled = False
            cmdMaterialShaderRename.Enabled = False
            cmdMaterialParameterAdd.Enabled = False
            cmdMaterialParametersFromFile.Enabled = False

            ' Reset labels.
            lblMaterialName.Text = "-"
            lblMaterialShaderName.Text = "-"

            ' Reset the list.
            lstMaterialParameters.Items.Clear()
            lstMaterialParameters_SelectedIndexChanged(Nothing, EventArgs.Empty)

        Else ' If lstMaterials.SelectedIndex = -1 Then
            With m_HOD.Materials(lstMaterials.SelectedIndex)
                ' Since a material is selected, enable all UI controls.
                cmdMaterialRemove.Enabled = True
                cmdMaterialRename.Enabled = True
                cmdMaterialShaderRename.Enabled = True
                cmdMaterialParameterAdd.Enabled = True
                cmdMaterialParametersFromFile.Enabled = True

                ' Update labels.
                lblMaterialName.Text = .MaterialName
                lblMaterialShaderName.Text = .ShaderName

                ' Update list.
                lstMaterialParameters.Items.Clear()
                lstMaterialParameters_SelectedIndexChanged(Nothing, EventArgs.Empty)

                ' Add all parameters.
                For I As Integer = 0 To .Parameters.Count - 1
                    lstMaterialParameters.Items.Add(.Parameters(I).ToString())

                Next I ' For I As Integer = 0 to .Parameters.Count - 1
            End With ' With m_HOD.Materials(lstMaterials.SelectedIndex)
        End If ' If lstMaterials.SelectedIndex = -1 Then

    End Sub

    Private Sub cmdMaterialAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMaterialAdd.Click
        Dim number As Integer = lstMaterials.Items.Count + 1
        Dim name As String

        Do
            ' Decide a name for the material.
            name = "Material " & CStr(number)

            ' See if it's a duplicate.
            If _ListBoxHasString(lstMaterials.Items, name) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, add it.
            m_HOD.Materials.Add(New Material With {.MaterialName = name})

            ' Add the material to the list.
            lstMaterials.Items.Add(name)
            lstMaterials.SelectedIndex = lstMaterials.Items.Count - 1

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cmdMaterialRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMaterialRemove.Click
        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Remove the material.
        m_HOD.Materials.RemoveAt(lstMaterials.SelectedIndex)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Update list.
        lstMaterials.Items.RemoveAt(lstMaterials.SelectedIndex)
        lstMaterials.SelectedIndex = lstMaterials.Items.Count - 1

    End Sub

    Private Sub cmdMaterialRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMaterialRename.Click
        Dim name As String

        ' Get old name.
        Dim oldName As String = m_HOD.Materials(lstMaterials.SelectedIndex).MaterialName

        Do
            ' Get new name.
            name = InputBox("Enter new name for material: ", Me.Text, oldName)

            ' See if the user pressed cancel.
            If name = "" Then _
    Exit Sub

            ' HACK: If only casing is changed, then rename list item twice.
            ' Otherwise, the change doesn't seem to be reflected in the list.
            If String.Compare(oldName, name, True) = 0 Then _
    lstMaterials.Items(lstMaterials.SelectedIndex) = "" _
            : Exit Do

            ' See if it's a duplicate.
            ' For that, first rename the material (in list only) to something else
            ' so that it doesn't interfere in our search.
            lstMaterials.Items(lstMaterials.SelectedIndex) = ""

            ' Now see if it's a duplicate.
            If _ListBoxHasString(lstMaterials.Items, name) Then _
    lstMaterials.Items(lstMaterials.SelectedIndex) = oldName _
            : MsgBox("The name you entered is a duplicate!" & vbCrLf &
           "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
            : Continue Do

            ' Exit loop.
            Exit Do

        Loop ' Do

        ' Not a duplicate. OK to rename.
        m_HOD.Materials(lstMaterials.SelectedIndex).MaterialName = name

        ' Update list.
        lstMaterials.Items(lstMaterials.SelectedIndex) = name

        ' Update UI
        lstMaterials_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cmdMaterialShaderRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdMaterialShaderRename.Click
        ' Get the new name.
        Dim name As String = InputBox("Enter new shader name: ", Me.Text, lblMaterialShaderName.Text)

        ' See if the user pressed cancel.
        If name = "" Then _
   Exit Sub

        ' Update name.
        m_HOD.Materials(lstMaterials.SelectedIndex).ShaderName = name

        ' Update label.
        lblMaterialShaderName.Text = name

    End Sub

    Private Sub lstMaterialParameters_Cut()
        ' See if any item is selected.
        If lstMaterials.SelectedIndex = -1 Then _
   Exit Sub

        ' See if any item is selected.
        If lstMaterialParameters.SelectedIndex = -1 Then _
   Exit Sub

        ' Get it's index.
        Dim ind As Integer = lstMaterials.SelectedIndex,
      ind2 As Integer = lstMaterialParameters.SelectedIndex

        ' Copy to clipboard.
        mnuEditClipboard = New Material.Parameter(m_HOD.Materials(ind).Parameters(ind2))

        ' Remove from HOD.
        m_HOD.Materials(ind).Parameters.RemoveAt(ind2)

        ' Refresh.
        lstMaterialParameters.Items.RemoveAt(ind2)

    End Sub

    Private Sub lstMaterialParameters_Copy()
        ' See if any item is selected.
        If lstMaterials.SelectedIndex = -1 Then _
   Exit Sub

        ' See if any item is selected.
        If lstMaterialParameters.SelectedIndex = -1 Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New Material.Parameter(m_HOD.Materials(lstMaterials.SelectedIndex).Parameters(lstMaterialParameters.SelectedIndex))

    End Sub

    Private Sub lstMaterialParameters_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is Material.Parameter Then _
   Exit Sub

        ' See if a material is selected.
        If lstMaterials.SelectedIndex = -1 Then _
   Exit Sub

        ' Get the item.
        Dim param As Material.Parameter = CType(mnuEditClipboard, Material.Parameter)

        ' Remove reference.
        mnuEditClipboard = Nothing

        ' See if it's already present.
        If Not lstMaterialParameters.Items.Contains(param.ToString()) Then _
   m_HOD.Materials(lstMaterials.SelectedIndex).Parameters.Add(param) _
        : lstMaterialParameters.Items.Add(param.ToString()) _
        : Exit Sub

        ' Rename material parameter.
        Dim number As Integer = 2

        Do
            ' See if this name is present.
            If lstMaterialParameters.Items.Contains(param.ToString() & CStr(number)) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, add it.
            param.Name &= CStr(number)
            m_HOD.Materials(lstMaterials.SelectedIndex).Parameters.Add(param)
            lstMaterialParameters.Items.Add(param.ToString())

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub lstMaterialParameters_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstMaterialParameters.SelectedIndexChanged
        If lstMaterialParameters.SelectedIndex = -1 Then
            ' Since no parameter is selected, disable all UI controls,
            ' except the add button and the load parameters button.
            cmdMaterialParameterRemove.Enabled = False
            cmdMaterialParameterRename.Enabled = False
            cmdMaterialNoTexture.Enabled = False

            optMaterialParameterTexture.Enabled = False
            optMaterialParameterColour.Enabled = False
            cboMaterialTextureIndex.Enabled = False

            txtMaterialColourR.Enabled = False
            txtMaterialColourG.Enabled = False
            txtMaterialColourB.Enabled = False
            txtMaterialColourA.Enabled = False

            ' Reset option boxes.
            optMaterialParameterTexture.Checked = False
            optMaterialParameterColour.Checked = False

            ' Reset text box.
            txtMaterialColourR.Text = CStr(1.0F)
            txtMaterialColourG.Text = CStr(1.0F)
            txtMaterialColourB.Text = CStr(1.0F)
            txtMaterialColourA.Text = CStr(1.0F)

            ' Reset combo box.
            cboMaterialTextureIndex.SelectedIndex = -1

        Else ' If lstMaterialParameters.SelectedIndex = -1 Then
            ' Since a parameter is selected, enable all UI controls.
            cmdMaterialParameterRemove.Enabled = True
            cmdMaterialParameterRename.Enabled = True
            cmdMaterialNoTexture.Enabled = True
            optMaterialParameterTexture.Enabled = True
            optMaterialParameterColour.Enabled = True

            ' Update radio button check.
            If m_HOD.Materials(lstMaterials.SelectedIndex) _
           .Parameters(lstMaterialParameters.SelectedIndex) _
           .Type = Material.Parameter.ParameterType.Texture Then _
    optMaterialParameterTexture.Checked = True _
   Else _
    optMaterialParameterColour.Checked = True

            ' The changed event may not have fired, but we need it to, so manually fire it.
            optMaterialParameterType_CheckedChanged(Nothing, EventArgs.Empty)

        End If ' If lstMaterialParameters.SelectedIndex = -1 Then

    End Sub

    Private Sub cmdMaterialParameterAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMaterialParameterAdd.Click
        Dim number As Integer = lstMaterialParameters.Items.Count + 1

        Do
            ' Decide a name for the parameter.
            Dim name As String = "Parameter " & CStr(number)

            ' See if it's a duplicate.
            If _ListBoxHasString(lstMaterialParameters.Items, name) Then _
    number += 1 _
            : Continue Do

            ' Not a duplicate. Add the parameter.
            m_HOD.Materials(lstMaterials.SelectedIndex).Parameters.Add(New Material.Parameter With {
    .Name = name,
    .Type = Material.Parameter.ParameterType.Texture})

            ' Add the parameter to the list.
            lstMaterialParameters.Items.Add(name)
            lstMaterialParameters.SelectedIndex = lstMaterialParameters.Items.Count - 1

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cmdMaterialParameterRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMaterialParameterRemove.Click
        ' Remove the material parameter.
        m_HOD.Materials(lstMaterials.SelectedIndex).Parameters.RemoveAt(lstMaterialParameters.SelectedIndex)

        ' Update list.
        lstMaterialParameters.Items.RemoveAt(lstMaterialParameters.SelectedIndex)
        lstMaterialParameters.SelectedIndex = lstMaterialParameters.Items.Count - 1

    End Sub

    Private Sub cmdMaterialParameterRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMaterialParameterRename.Click
        Dim name As String

        ' Get old name.
        Dim oldName As String = m_HOD.Materials(lstMaterials.SelectedIndex) _
                               .Parameters(lstMaterialParameters.SelectedIndex) _
                               .Name

        Do
            ' Get new name.
            name = InputBox("Enter new name for material parameter: ", Me.Text, oldName)

            ' Check if user pressed cancel.
            If name = "" Then _
    Exit Sub

            ' HACK: If only casing is changed, then rename list item twice.
            ' Otherwise, the change doesn't seem to be reflected in the list.
            If String.Compare(name, oldName, True) = 0 Then _
    lstMaterialParameters.Items(lstMaterialParameters.SelectedIndex) = "" _
            : Exit Do

            ' See if it's a duplicate.
            ' For that, first rename the parameter (in list only) to something else
            ' so that it doesn't interfere in our search.
            lstMaterialParameters.Items(lstMaterialParameters.SelectedIndex) = ""

            ' Now see if it's a duplicate.
            If _ListBoxHasString(lstMaterialParameters.Items, name) Then _
    lstMaterialParameters.Items(lstMaterialParameters.SelectedIndex) = oldName _
            : MsgBox("The name you entered is a duplicate!" & vbCrLf &
           "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
            : Continue Do

            ' Exit loop.
            Exit Do

        Loop ' Do

        ' Not a duplicate. OK to rename.
        m_HOD.Materials(lstMaterials.SelectedIndex) _
       .Parameters(lstMaterialParameters.SelectedIndex) _
       .Name = name

        ' Update list.
        lstMaterialParameters.Items(lstMaterialParameters.SelectedIndex) = name

    End Sub

    Private Sub cmdMaterialParametersFromFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMaterialParametersFromFile.Click
        ' See if the user pressed cancel.
        If OpenShaderFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Create a new IOResult form.
        Dim f As New IOResult

        ' Try to parse the shader.
        Try
            m_HOD.Materials(lstMaterials.SelectedIndex).ParseShader(OpenShaderFileDialog.FileName)

        Catch ex As Exception
            MsgBox("Error occured while trying to parse shader: " & vbCrLf &
           ex.ToString(), MsgBoxStyle.Critical, Me.Text)

            ' Dispose the form.
            f.Dispose()

            Exit Sub

        End Try

        ' Update the shader name.
        m_HOD.Materials(lstMaterials.SelectedIndex).ShaderName =
     IO.Path.GetFileNameWithoutExtension(OpenShaderFileDialog.FileName)

        ' Update the UI.
        lstMaterials_SelectedIndexChanged(Nothing, EventArgs.Empty)

        ' Show the result.
        f.Show(Me)

    End Sub

    Private Sub optMaterialParameterType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optMaterialParameterTexture.CheckedChanged, optMaterialParameterColour.CheckedChanged
        ' If the event is for an uncheck, then do not continue.
        If (sender IsNot Nothing) AndAlso (Not CType(sender, RadioButton).Checked) Then _
   Exit Sub

        With m_HOD.Materials(lstMaterials.SelectedIndex).Parameters(lstMaterialParameters.SelectedIndex)
            ' Before modifying HOD data, see to it that the control is focused.
            If (sender IsNot Nothing) AndAlso (CType(sender, RadioButton).Focused) Then
                ' If this event was caused due to the colour radio button,
                ' then set the parameter type to colour.
                If sender Is optMaterialParameterColour Then _
     .Type = Material.Parameter.ParameterType.Colour

                ' If this event was caused due to the texture radio button,
                ' then set the parameter type to texture.
                If sender Is optMaterialParameterTexture Then _
     .Type = Material.Parameter.ParameterType.Texture

            End If ' If (sender isnot Nothing ) andalso (CType(sender, RadioButton ).Focused ) then 

            If .Type = Material.Parameter.ParameterType.Texture Then
                ' Since the paramter type is texture, disable colour
                ' related controls.
                txtMaterialColourR.Enabled = False
                txtMaterialColourG.Enabled = False
                txtMaterialColourB.Enabled = False
                txtMaterialColourA.Enabled = False
                cboMaterialTextureIndex.Enabled = True

                ' Reset text boxes.
                txtMaterialColourR.Text = CStr(1.0F)
                txtMaterialColourG.Text = CStr(1.0F)
                txtMaterialColourB.Text = CStr(1.0F)
                txtMaterialColourA.Text = CStr(1.0F)

                ' Check index.
                If (.TextureIndex < -1) OrElse
       (.TextureIndex >= cboMaterialTextureIndex.Items.Count) Then _
     MsgBox("This material refers to a non-existant texture!" & vbCrLf &
            "The reference has been removed.", MsgBoxStyle.Information, Me.Text) _
                : .TextureIndex = -1

                ' Update combo box.
                cboMaterialTextureIndex.SelectedIndex = .TextureIndex

            Else ' If .Type = Material.Parameter.ParameterType.Texture Then
                ' Since the paramter type is colour, disable texture
                ' related controls.
                txtMaterialColourR.Enabled = True
                txtMaterialColourG.Enabled = True
                txtMaterialColourB.Enabled = True
                txtMaterialColourA.Enabled = True
                cboMaterialTextureIndex.Enabled = False

                ' Update text boxes.
                txtMaterialColourR.Text = CStr(.Colour.X)
                txtMaterialColourG.Text = CStr(.Colour.Y)
                txtMaterialColourB.Text = CStr(.Colour.Z)
                txtMaterialColourA.Text = CStr(.Colour.W)

                ' Reset combo box.
                cboMaterialTextureIndex.SelectedIndex = -1

            End If ' If .Type = Material.Parameter.ParameterType.Texture Then
        End With ' With m_HOD.Materials(lstMaterials.SelectedIndex).Parameters(lstMaterialParameters.SelectedIndex)

    End Sub

    Private Sub txtMaterialColour_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMaterialColourR.Validated, txtMaterialColourG.Validated, txtMaterialColourB.Validated, txtMaterialColourA.Validated
        If (lstMaterials.SelectedIndex = -1) OrElse (lstMaterialParameters.SelectedIndex = -1) Then _
   Exit Sub

        ' Get the colour.
        Dim V As Vector4 = m_HOD.Materials(lstMaterials.SelectedIndex) _
                          .Parameters(lstMaterialParameters.SelectedIndex) _
                          .Colour

        ' Set the red field if needed.
        If sender Is txtMaterialColourR Then _
   V.X = CSng(txtMaterialColourR.Text)

        ' Set the green field if needed.
        If sender Is txtMaterialColourG Then _
   V.Y = CSng(txtMaterialColourG.Text)

        ' Set the blue field if needed.
        If sender Is txtMaterialColourB Then _
   V.Z = CSng(txtMaterialColourB.Text)

        ' Set the alpha field if needed.
        If sender Is txtMaterialColourA Then _
   V.W = CSng(txtMaterialColourA.Text)

        ' Set the colour.
        m_HOD.Materials(lstMaterials.SelectedIndex) _
       .Parameters(lstMaterialParameters.SelectedIndex) _
       .Colour = V

        ' Reset the error.
        ErrorProvider.SetError(CType(sender, TextBox), "")

    End Sub

    Private Sub cboMaterialTextureIndex_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMaterialTextureIndex.SelectedIndexChanged
        ' Set the texture index if the combo box is focused.
        If CType(sender, ComboBox).Focused Then _
   m_HOD.Materials(lstMaterials.SelectedIndex) _
        .Parameters(lstMaterialParameters.SelectedIndex) _
        .TextureIndex = cboMaterialTextureIndex.SelectedIndex

    End Sub

    Private Sub cmdMaterialNoTexture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMaterialNoTexture.Click
        ' Focus the combo box.
        cboMaterialTextureIndex.Focus()

        ' Deselect the texture, rest will be handled by the combo box.
        cboMaterialTextureIndex.SelectedIndex = -1

    End Sub

#End Region

#Region " Ship Meshes UI "

    ''' <summary>
    ''' Prepares a basic mesh for export by updating it's material reference
    ''' fields.
    ''' </summary>
    Private Sub _PrepareBasicMeshForExport(ByVal basicMesh As BasicMesh)
        For I As Integer = 0 To basicMesh.PartCount - 1
            ' Get the material reference.
            Dim matRef As Material.Reference = basicMesh.Material(I)

            ' Get the material index.
            Dim matIndex As Integer = matRef.Index

            ' Update material properties if possible.
            If (matIndex >= 0) AndAlso (matIndex < m_HOD.Materials.Count) Then
                ' Get the material.
                Dim mat As Material = m_HOD.Materials(matIndex)

                ' Set name.
                matRef.Name = mat.MaterialName

                ' Get diffuse texture.
                For J As Integer = 0 To mat.Parameters.Count - 1
                    If (String.Compare(mat.Parameters(J).Name, "$diffuse", True) = 0) OrElse
        (String.Compare(mat.Parameters(J).Name, "$diffuseOff", True) = 0) Then
                        ' Get texture index.
                        Dim texIndex As Integer = mat.Parameters(J).TextureIndex

                        ' Set the texture property if possible.
                        If (texIndex >= 0) AndAlso (texIndex < m_HOD.Textures.Count) Then
                            ' Get texture filename.
                            matRef.TextureName = _GetTextureFilenameFromPath(m_HOD.Textures(texIndex).Path)

                            If m_HOD.Textures(texIndex).FourCC = "8888" Then _
        matRef.TextureName &= ".tga" _
       Else _
        matRef.TextureName &= ".dds"

                        Else ' If (texIndex >= 0) AndAlso (texIndex < m_HOD.Textures.Count) Then
                            matRef.TextureName = ""

                        End If ' If (texIndex >= 0) AndAlso (texIndex < m_HOD.Textures.Count) Then
                    End If ' If (String.Compare(mat.Parameters(J).Name, "$diffuse", True) = 0) OrElse _
                    '           (String.Compare(mat.Parameters(J).Name, "$diffuseOff", True) = 0) Then
                Next J ' For J As Integer = 0 To mat.Parameters.Count - 1

            Else ' If (matIndex >= 0) AndAlso (matIndex < m_HOD.Materials.Count) Then
                matRef.Name = "Unknown"
                matRef.TextureName = ""

            End If ' If (matIndex >= 0) AndAlso (matIndex < m_HOD.Materials.Count) Then

            ' Finally, update material.
            basicMesh.Material(I) = matRef

        Next I ' For I As Integer = 0 To basicMesh.PartCount - 1

    End Sub

    ''' <summary>
    ''' Removes the basic mesh parts' material references fields.
    ''' </summary>
    Private Sub _UnPrepareBasicMeshForExport(ByVal basicMesh As BasicMesh)
        For I As Integer = 0 To basicMesh.PartCount - 1
            Dim m As Material.Reference = basicMesh.Material(I)
            m.Name = ""
            m.TextureName = ""
            basicMesh.Material(I) = m

        Next I ' For I As Integer = 0 To basicMesh.PartCount - 1

    End Sub

    ''' <summary>
    ''' Sets the basic mesh parts' vertex flags.
    ''' </summary>
    Private Sub _SetBasicMeshVertexMasks(ByVal basicMesh As BasicMesh, ByVal vertexMasks As VertexMasks)
        For I As Integer = 0 To basicMesh.PartCount - 1
            Dim m As Material.Reference = basicMesh.Material(I)
            m.VertexMask = vertexMasks
            basicMesh.Material(I) = m

        Next I ' For I As Integer = 0 To basicMesh.PartCount - 1

    End Sub

    Private Sub tabMultiMeshes_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabMultiMeshes.Enter
        ' See if the tab enter event is marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        ' Update the combo box.
        With cboShipMeshesParent.Items
            ' Get joints.
            Dim joints() As Joint = m_HOD.Root.ToArray()

            ' Clear old items.
            .Clear()

            ' Add all joints.
            For I As Integer = 0 To joints.Length - 1
                .Add(joints(I).Name)

            Next I ' For I As Integer = 0 To joints.Length - 1

            ' Erase array.
            Erase joints

        End With ' With cboShipMeshesParent.Items

        ' Update the checked list box.
        With lstShipMeshes.Items
            ' Clear old items.
            .Clear()

            ' Add all meshes.
            For I As Integer = 0 To m_HOD.MultiMeshes.Count - 1
                .Add(m_HOD.MultiMeshes(I).ToString())

            Next I ' For I As Integer = 0 To m_HOD.MultiMeshes.Count - 1

        End With ' With lstShipMeshes.Items

        ' Update selection.
        lstShipMeshes_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub lstShipMeshes_Cut()
        ' See if any item is selected.
        If lstShipMeshes.SelectedIndex = -1 Then _
   Exit Sub

        ' Get it's index.
        Dim ind As Integer = lstShipMeshes.SelectedIndex

        ' Copy to clipboard.
        mnuEditClipboard = New MultiMesh(m_HOD.MultiMeshes(ind))

        ' Remove from HOD.
        m_HOD.MultiMeshes.RemoveAt(ind)

        ' Refresh.
        lstShipMeshes.Items.RemoveAt(ind)

    End Sub

    Private Sub lstShipMeshes_Copy()
        ' See if any item is selected.
        If lstShipMeshes.SelectedIndex = -1 Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New MultiMesh(m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex))

    End Sub

    Private Sub lstShipMeshes_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is MultiMesh Then _
   Exit Sub

        ' Get the item.
        Dim mm As MultiMesh = CType(mnuEditClipboard, MultiMesh)

        ' Remove reference.
        mnuEditClipboard = Nothing

        ' Lock mesh.
        mm.Lock(m_D3DManager.Device)

        ' See if it's already present.
        If Not lstShipMeshes.Items.Contains(mm.ToString()) Then _
   m_HOD.MultiMeshes.Add(mm) _
        : lstShipMeshes.Items.Add(mm.ToString()) _
        : Exit Sub

        ' Rename item.
        Dim number As Integer = 2

        Do
            ' See if this name is present.
            If lstShipMeshes.Items.Contains(mm.ToString() & CStr(number)) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, add it.
            mm.Name &= CStr(number)
            m_HOD.MultiMeshes.Add(mm)
            lstShipMeshes.Items.Add(mm.ToString())

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub lstShipMeshes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstShipMeshes.SelectedIndexChanged
        If lstShipMeshes.SelectedIndex = -1 Then
            ' Since no mesh is selected, disable all UI controls,
            ' except the add button.
            cmdShipMeshesRemove.Enabled = False
            cmdShipMeshesRename.Enabled = False
            cmdShipMeshesRetangent.Enabled = False
            cmdShipMeshesRenormal.Enabled = False
            cmdShipMeshesLODAdd.Enabled = False
            cboShipMeshesParent.Enabled = False
            cboShipMeshesParent.SelectedIndex = -1

            cstShipMeshesLODs.Items.Clear()
            cstShipMeshesLODs_SelectedIndexChanged(Nothing, EventArgs.Empty)

        Else ' If lstShipMeshes.SelectedIndex = -1 Then
            ' Since a mesh is selected, enable all UI controls.
            cmdShipMeshesRemove.Enabled = True
            cmdShipMeshesRename.Enabled = True
            cmdShipMeshesRetangent.Enabled = True
            cmdShipMeshesRenormal.Enabled = True
            cmdShipMeshesLODAdd.Enabled = True
            cboShipMeshesParent.Enabled = True

            ' Check if the parent doesn't exist.
            If Not _ComboBoxHasString(cboShipMeshesParent.Items, m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).ParentName) Then _
    m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).ParentName = "Root" _
            : MsgBox("The parent joint specified by this ship mesh does not exist!" & vbCrLf &
           "It has been changed to 'Root'.", MsgBoxStyle.Information, Me.Text)

            ' Update combo box.
            cboShipMeshesParent.SelectedItem = m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).ParentName

            ' Update list.
            cstShipMeshesLODs.Items.Clear()
            cstShipMeshesLODs_SelectedIndexChanged(Nothing, EventArgs.Empty)

            For I As Integer = 0 To m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD.Count - 1
                cstShipMeshesLODs.Items.Add("LOD " & CStr(I), m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).Visible(I))

            Next I ' For I As Integer = 0 To m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD.Count - 1
        End If ' If lstShipMeshes.SelectedIndex = -1 Then

    End Sub

    Private Sub cmdShipMeshesAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipMeshesAdd.Click
        Dim number As Integer = lstShipMeshes.Items.Count + 1
        Dim name As String

        Do
            ' Decide a name for the mesh.
            name = "Ship Mesh " & CStr(number)

            ' See if it's a duplicate.
            If _ListBoxHasString(lstShipMeshes.Items, name) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, add it.
            m_HOD.MultiMeshes.Add(New MultiMesh With {
    .Name = name,
    .ParentName = "Root"
   })

            ' Add the mesh to the list.
            lstShipMeshes.Items.Add(name)
            lstShipMeshes.SelectedIndex = lstShipMeshes.Items.Count - 1

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cmdShipMeshesRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipMeshesRemove.Click
        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Remove the mesh.
        m_HOD.MultiMeshes.RemoveAt(lstShipMeshes.SelectedIndex)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Update list.
        lstShipMeshes.Items.RemoveAt(lstShipMeshes.SelectedIndex)
        lstShipMeshes.SelectedIndex = lstShipMeshes.Items.Count - 1

    End Sub

    Private Sub cmdShipMeshesRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipMeshesRename.Click
        Dim name As String

        ' Get old name.
        Dim oldName As String = m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).Name

        Do
            ' Get new name.
            name = InputBox("Enter new name for mesh: ", Me.Text, oldName)

            ' See if the user pressed cancel.
            If name = "" Then _
    Exit Sub

            ' HACK: If only casing is changed, then rename list item twice.
            ' Otherwise, the change doesn't seem to be reflected in the list.
            If String.Compare(oldName, name, True) = 0 Then _
    lstShipMeshes.Items(lstShipMeshes.SelectedIndex) = "" _
            : Exit Do

            ' See if it's a duplicate.
            ' For that, first rename the mesh (in list only) to something else
            ' so that it doesn't interfere in our search.
            lstShipMeshes.Items(lstShipMeshes.SelectedIndex) = ""

            ' Now see if it's a duplicate.
            If _ListBoxHasString(lstShipMeshes.Items, name) Then _
    lstShipMeshes.Items(lstShipMeshes.SelectedIndex) = oldName _
            : MsgBox("The name you entered is a duplicate!" & vbCrLf &
           "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
            : Continue Do

            ' Exit loop.
            Exit Do

        Loop ' Do

        ' Not a duplicate. OK to rename.
        m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).Name = name

        ' Update list.
        lstShipMeshes.Items(lstShipMeshes.SelectedIndex) = name

        ' Update UI
        lstShipMeshes_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cmdShipMeshesRenormal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipMeshesRenormal.Click
        ' Get the multi mesh.
        Dim mm As MultiMesh = m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex)

        ' Notify users if there are no normals.
        If mm.LOD.Count = 0 Then _
   MsgBox("Mesh has no LODs to recalculate normals!", MsgBoxStyle.Information, Me.Text) _
        : Exit Sub

        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        For I As Integer = 0 To mm.LOD.Count - 1
            ' Get the mesh.
            Dim m As BasicMesh = m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(I)

            ' Unlock mesh.
            m.Unlock()

            ' Recalculate normals.
            m.CalculateNormals()

            ' Lock mesh.
            m.Lock(m_D3DManager.Device)

        Next I ' For I As Integer = 0 To mm.LOD.Count - 1

        ' Resume rendering.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub cmdShipMeshesRetangent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipMeshesRetangent.Click
        ' Get the multi mesh.
        Dim mm As MultiMesh = m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex)

        ' Notify users if there are no normals.
        If mm.LOD.Count = 0 Then _
   MsgBox("Mesh has no LODs to recalculate tangents!", MsgBoxStyle.Information, Me.Text) _
        : Exit Sub

        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        For I As Integer = 0 To mm.LOD.Count - 1
            ' Get the mesh.
            Dim m As BasicMesh = m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(I)

            ' Unlock mesh.
            m.Unlock()

            ' Recalculate tangents.
            m.CalculateTangents()

            ' Lock mesh.
            m.Lock(m_D3DManager.Device)

        Next I ' For I As Integer = 0 To mm.LOD.Count - 1

        ' Resume rendering.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub cboShipMeshesParent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboShipMeshesParent.SelectedIndexChanged
        ' Set the parent name if the combo box is focused.
        If (sender IsNot Nothing) AndAlso (CType(sender, ComboBox).Focused) Then _
   m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).ParentName = CStr(cboShipMeshesParent.SelectedItem)

    End Sub

    Private Sub cstShipMeshesLODs_Cut()
        ' See if any item is selected.
        If lstShipMeshes.SelectedIndex = -1 Then _
   Exit Sub

        ' See if any item is selected.
        If cstShipMeshesLODs.SelectedIndex = -1 Then _
   Exit Sub

        ' Get it's index.
        Dim ind As Integer = lstShipMeshes.SelectedIndex,
      ind2 As Integer = cstShipMeshesLODs.SelectedIndex

        ' Copy to clipboard.
        mnuEditClipboard = New BasicMesh(m_HOD.MultiMeshes(ind).LOD(ind2))

        ' Remove from HOD.
        m_HOD.MultiMeshes(ind).LOD.RemoveAt(ind2)

        ' Refresh.
        cstShipMeshesLODs.Items.RemoveAt(ind2)

    End Sub

    Private Sub cstShipMeshesLODs_Copy()
        ' See if any item is selected.
        If lstShipMeshes.SelectedIndex = -1 Then _
   Exit Sub

        ' See if any item is selected.
        If cstShipMeshesLODs.SelectedIndex = -1 Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New BasicMesh(m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(cstShipMeshesLODs.SelectedIndex))

    End Sub

    Private Sub cstShipMeshesLODs_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is BasicMesh Then _
   Exit Sub

        ' See if a ship mesh is selected.
        If lstShipMeshes.SelectedIndex = -1 Then _
   Exit Sub

        ' Get the item.
        Dim bm As BasicMesh = CType(mnuEditClipboard, BasicMesh)

        ' Remove reference.
        mnuEditClipboard = Nothing

        ' Lock mesh.
        bm.Lock(m_D3DManager.Device)

        ' Add it.
        m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD.Add(bm)

        ' Get it's index.
        Dim ind As Integer = m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD.Count - 1

        ' Update list.
        cstShipMeshesLODs.Items.Add("LOD " & CStr(ind), m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).Visible(ind))

    End Sub

    Private Sub cstShipMeshesLODs_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles cstShipMeshesLODs.ItemCheck
        If lstShipMeshes.SelectedIndex = -1 Then _
   Debug.Assert(False) _
        : Exit Sub

        ' Set the visible flag if the checked list box is focused.
        If (sender IsNot Nothing) AndAlso (CType(sender, CheckedListBox).Focused) Then _
   m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).Visible(e.Index) = (e.NewValue = CheckState.Checked)

    End Sub

    Private Sub cstShipMeshesLODs_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cstShipMeshesLODs.SelectedIndexChanged
        If cstShipMeshesLODs.SelectedIndex = -1 Then
            ' Since no LOD is selected, disable all UI controls,
            ' except the add button.
            cmdShipMeshesLODRemove.Enabled = False
            cmdShipMeshesLODImport.Enabled = False
            cmdShipMeshesLODExport.Enabled = False
            cmdShipMeshesLODTransform.Enabled = False
            cmdShipMeshesLODReMaterial.Enabled = False
            cmdShipMeshesLODRenormal.Enabled = False
            cmdShipMeshesLODRetangent.Enabled = False

        Else ' If cstShipMeshesLODs.SelectedIndex = -1 Then
            cmdShipMeshesLODRemove.Enabled = True
            cmdShipMeshesLODImport.Enabled = True
            cmdShipMeshesLODExport.Enabled = True
            cmdShipMeshesLODTransform.Enabled = True
            cmdShipMeshesLODReMaterial.Enabled = True
            cmdShipMeshesLODRenormal.Enabled = True
            cmdShipMeshesLODRetangent.Enabled = True

        End If ' If cstShipMeshesLODs.SelectedIndex = -1 Then

    End Sub

    Private Sub cmdShipMeshesLODAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipMeshesLODAdd.Click
        Dim LOD As Integer = cstShipMeshesLODs.Items.Count

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Make a new basic mesh.
        Dim BasicMesh As New BasicMesh

        ' Lock the mesh.
        BasicMesh.Lock(m_D3DManager.Device)

        ' Add LOD.
        m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD.Add(BasicMesh)

        ' Update list.
        cstShipMeshesLODs.Items.Add("LOD " & LOD, m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).Visible(LOD))
        cstShipMeshesLODs.SelectedIndex = LOD

        ' Resume render.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub cmdShipMeshesLODRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipMeshesLODRemove.Click
        Dim LOD As Integer = cstShipMeshesLODs.SelectedIndex

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Remove LOD.
        m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD.RemoveAt(LOD)

        ' Remove from list.
        cstShipMeshesLODs.Items.RemoveAt(LOD)

        ' Update list.
        For I As Integer = LOD To cstShipMeshesLODs.Items.Count - 1
            cstShipMeshesLODs.Items(I) = "LOD " & CStr(I)

        Next I ' For I As Integer = LOD To cstShipMeshesLODs.Items.Count - 1

        ' Resume render.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub cmdShipMeshesLODTransform_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipMeshesLODTransform.Click
        ' Get the selected mesh.
        Dim m As BasicMesh = m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(cstShipMeshesLODs.SelectedIndex)

        ' Initialize new MeshTransformer.
        Dim f As New MeshTransformer

        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Unlock mesh.
        m.Unlock()

        ' Set the mesh.
        f.SetMesh(m)

        ' Lock mesh.
        m.Lock(m_D3DManager.Device)

        ' Resume rendering.
        m_D3DManager.RenderLoopResume()

        ' Display form.
        f.ShowDialog()

        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Unlock mesh.
        m.Unlock()

        ' Reverse faces if needed.
        If f.GetReverseFaces() Then _
   m.ReverseFaceOrder()

        ' Dispose form.
        f.Dispose()

        ' Transform the mesh.
        m.Transform(f.GetTransform())

        ' Lock mesh.
        m.Lock(m_D3DManager.Device)

        ' Resume rendering.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub cmdShipMeshesLODImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipMeshesLODImport.Click
        ' See if user pressed cancel.
        If OpenOBJFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Initialize a new IOResult form.
        Dim f As New IOResult

        ' Create a new mesh to read.
        Dim m As New BasicMesh

        ' Ask whether to reverse U of UVs.
        Dim reverseUVs As Boolean = (MsgBox("Reverse UVs?", MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes)

        ' Try to read the wavefront object file.
        Try
            ' Read the wavefront object file.
            GenericMesh.Translators.WavefrontObject.ReadFile(OpenOBJFileDialog.FileName, m, reverseUVs)

            ' Validate the mesh.
            m.Validate()

        Catch ex As Exception
            ' Log the error.
            Trace.TraceError(ex.ToString)

            ' Clear the temp mesh.
            m.RemoveAll()
            m = Nothing

        End Try

        ' If the read succeeded...
        If m IsNot Nothing Then
            ' Pause render.
            m_D3DManager.RenderLoopPause()

            ' Unlock mesh.
            m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(cstShipMeshesLODs.SelectedIndex).Unlock()

            ' Update mesh.
            m.CopyTo(m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(cstShipMeshesLODs.SelectedIndex))
            _UnPrepareBasicMeshForExport(m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(cstShipMeshesLODs.SelectedIndex))
            _SetBasicMeshVertexMasks(m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(cstShipMeshesLODs.SelectedIndex),
                            VertexMasks.Position Or VertexMasks.Normal Or VertexMasks.Texture0 Or VertexMasks.Texture1)

            ' Lock mesh.
            m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(cstShipMeshesLODs.SelectedIndex).Lock(m_D3DManager.Device)

            ' Resume render.
            m_D3DManager.RenderLoopResume()

            ' Now replace each material.
            For I As Integer = 0 To m.PartCount - 1
                Dim frm As New MaterialSubstitute(I,
                                      m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(cstShipMeshesLODs.SelectedIndex),
                                      m_HOD,
                                      m.Material(I).Name)

                frm.ShowDialog()

            Next ' For I As Integer = 0 To m.PartCount - 1

            ' Remove the read mesh.
            m.RemoveAll()
            m = Nothing

        End If ' If m IsNot Nothing Then

        ' Display the result.
        f.Show()

    End Sub

    Private Sub cmdShipMeshesLODExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipMeshesLODExport.Click
        ' See if user pressed cancel.
        If SaveOBJFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Initialize a new IOResult form.
        Dim f As New IOResult

        ' Ask whether to reverse U of UVs.
        Dim reverseUVs As Boolean = (MsgBox("Reverse UVs?", MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes)

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Unlock mesh.
        m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(cstShipMeshesLODs.SelectedIndex).Unlock()

        ' Update a few material properties...
        _PrepareBasicMeshForExport(m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(cstShipMeshesLODs.SelectedIndex))

        ' Try to write the wavefront object file.
        Try
            GenericMesh.Translators.WavefrontObject.WriteFile(
    SaveOBJFileDialog.FileName,
    m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(cstShipMeshesLODs.SelectedIndex),
    ReverseUVs:=reverseUVs
   )

        Catch ex As Exception
            Trace.TraceError(ex.ToString)

        End Try

        ' Remove the material properties that were set...
        _UnPrepareBasicMeshForExport(m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(cstShipMeshesLODs.SelectedIndex))

        ' Lock mesh.
        m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(cstShipMeshesLODs.SelectedIndex).Lock(m_D3DManager.Device)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Display the result.
        f.Show()

    End Sub

    Private Sub cmdShipMeshesLODReMaterial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipMeshesLODReMaterial.Click
        ' Get the mesh.
        Dim m As BasicMesh = m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(cstShipMeshesLODs.SelectedIndex)

        ' Notify user if there are no parts.
        If m.PartCount = 0 Then _
   MsgBox("Mesh has no parts to re-assign materials!", MsgBoxStyle.Information, Me.Text) _
        : Exit Sub

        ' First update the mesh's material name and texture name fields.
        ' This needs to be done because that's from where we'll read old names.
        _PrepareBasicMeshForExport(m)

        ' Now substitute material for all parts.
        For I As Integer = 0 To m.PartCount - 1
            Dim f As New MaterialSubstitute(I, m, m_HOD, m.Material(I).Name)
            f.ShowDialog()
            f.Dispose()

        Next I ' For I As Integer = 0 To m.PartCount - 1

        ' Remove the material properties that were set...
        _UnPrepareBasicMeshForExport(m)

    End Sub

    Private Sub cmdShipMeshesLODRenormal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipMeshesLODRenormal.Click
        ' Get the mesh.
        Dim m As BasicMesh = m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(cstShipMeshesLODs.SelectedIndex)

        ' Notify user if there are no parts.
        If m.PartCount = 0 Then _
   MsgBox("Mesh has no parts to recalculate normals!", MsgBoxStyle.Information, Me.Text) _
        : Exit Sub

        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Unlock mesh.
        m.Unlock()

        ' Recalculate normals.
        m.CalculateNormals()

        ' Lock mesh.
        m.Lock(m_D3DManager.Device)

        ' Resume rendering.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub cmdShipMeshesLODRetangent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShipMeshesLODRetangent.Click
        ' Get the mesh.
        Dim m As BasicMesh = m_HOD.MultiMeshes(lstShipMeshes.SelectedIndex).LOD(cstShipMeshesLODs.SelectedIndex)

        ' Notify user if there are no parts.
        If m.PartCount = 0 Then _
   MsgBox("Mesh has no parts to recalculate tangents!", MsgBoxStyle.Information, Me.Text) _
        : Exit Sub

        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Unlock mesh.
        m.Unlock()

        ' Recalculate tangents.
        m.CalculateTangents()

        ' Lock mesh.
        m.Lock(m_D3DManager.Device)

        ' Resume rendering.
        m_D3DManager.RenderLoopResume()

    End Sub

#End Region

#Region " Goblins UI "

    Private Sub tabGoblins_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabGoblins.Enter
        ' See if the tab enter event is marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        ' Update the combo box.
        With cboGoblinsParent.Items
            ' Get joints.
            Dim joints() As Joint = m_HOD.Root.ToArray()

            ' Clear old items.
            .Clear()

            ' Add all joints.
            For I As Integer = 0 To joints.Length - 1
                .Add(joints(I).Name)

            Next I ' For I As Integer = 0 To joints.Length - 1

            ' Erase array.
            Erase joints

        End With ' With cboGoblinsParent.Items

        ' Update the checked list box.
        With cstGoblins.Items
            ' Clear old items.
            .Clear()

            ' Add all meshes.
            For I As Integer = 0 To m_HOD.GoblinMeshes.Count - 1
                .Add(m_HOD.GoblinMeshes(I).ToString(), m_HOD.GoblinMeshes(I).Visible)

            Next I ' For I As Integer = 0 To m_HOD.GoblinMeshes.Count - 1

        End With ' With cstGoblins.Items

        ' Update selection.
        cstGoblins_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cstGoblins_Cut()
        ' See if any item is selected.
        If cstGoblins.SelectedIndex = -1 Then _
   Exit Sub

        ' Get it's index.
        Dim ind As Integer = cstGoblins.SelectedIndex

        ' Copy to clipboard.
        mnuEditClipboard = New GoblinMesh(m_HOD.GoblinMeshes(ind))

        ' Remove from HOD.
        m_HOD.GoblinMeshes.RemoveAt(ind)

        ' Refresh.
        cstGoblins.Items.RemoveAt(ind)

    End Sub

    Private Sub cstGoblins_Copy()
        ' See if any item is selected.
        If cstGoblins.SelectedIndex = -1 Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New GoblinMesh(m_HOD.GoblinMeshes(cstGoblins.SelectedIndex))

    End Sub

    Private Sub cstGoblins_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is GoblinMesh Then _
   Exit Sub

        ' Get the item.
        Dim gm As GoblinMesh = CType(mnuEditClipboard, GoblinMesh)

        ' Remove reference.
        mnuEditClipboard = Nothing

        ' Lock mesh.
        gm.Mesh.Lock(m_D3DManager.Device)

        ' See if it's already present.
        If Not cstGoblins.Items.Contains(gm.ToString()) Then _
   m_HOD.GoblinMeshes.Add(gm) _
        : cstGoblins.Items.Add(gm.ToString(), gm.Visible) _
        : Exit Sub

        ' Rename item.
        Dim number As Integer = 2

        Do
            ' See if this name is present.
            If cstGoblins.Items.Contains(gm.ToString() & CStr(number)) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, add it.
            gm.Name &= CStr(number)
            m_HOD.GoblinMeshes.Add(gm)
            cstGoblins.Items.Add(gm.ToString(), gm.Visible)

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cstGoblins_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles cstGoblins.ItemCheck
        ' Set the visible flag if the checked list box is focused.
        If (sender IsNot Nothing) AndAlso (CType(sender, CheckedListBox).Focused) Then _
   m_HOD.GoblinMeshes(e.Index).Visible = (e.NewValue = CheckState.Checked)

    End Sub

    Private Sub cstGoblins_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cstGoblins.SelectedIndexChanged
        If cstGoblins.SelectedIndex = -1 Then
            ' Since no mesh is selected, disable all UI controls,
            ' except the add button.
            cmdGoblinsRemove.Enabled = False
            cmdGoblinsRename.Enabled = False
            cmdGoblinsImport.Enabled = False
            cmdGoblinsExport.Enabled = False
            cmdGoblinsTransform.Enabled = False
            cmdGoblinsRematerial.Enabled = False
            cmdGoblinsRenormal.Enabled = False
            cmdGoblinsRetangent.Enabled = False
            cboGoblinsParent.Enabled = False
            cboGoblinsParent.SelectedIndex = -1

        Else ' If cstGoblins.SelectedIndex = -1 Then
            ' Since a mesh is selected, enable all UI controls.
            cmdGoblinsRemove.Enabled = True
            cmdGoblinsRename.Enabled = True
            cmdGoblinsImport.Enabled = True
            cmdGoblinsExport.Enabled = True
            cmdGoblinsTransform.Enabled = True
            cmdGoblinsRematerial.Enabled = True
            cmdGoblinsRenormal.Enabled = True
            cmdGoblinsRetangent.Enabled = True
            cboGoblinsParent.Enabled = True

            ' Check if the parent doesn't exist.
            If Not _ComboBoxHasString(cboGoblinsParent.Items, m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).ParentName) Then _
    m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).ParentName = "Root" _
            : MsgBox("The parent joint specified by this goblin mesh does not exist!" & vbCrLf &
           "It has been changed to 'Root'.", MsgBoxStyle.Information, Me.Text)

            ' Update combo box.
            cboGoblinsParent.SelectedItem = m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).ParentName

        End If ' If cstGoblins.SelectedIndex = -1 Then

    End Sub

    Private Sub cmdGoblinsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoblinsAdd.Click
        Dim number As Integer = cstGoblins.Items.Count + 1
        Dim name As String

        Do
            ' Decide a name for the mesh.
            name = "Goblin " & CStr(number)

            ' See if it's a duplicate.
            If _ListBoxHasString(cstGoblins.Items, name) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, make a new goblin mesh.
            Dim g As New GoblinMesh With {
    .Name = name,
    .ParentName = "Root"
   }

            ' Add it.
            m_HOD.GoblinMeshes.Add(g)

            ' Add the mesh to the list.
            cstGoblins.Items.Add(g.ToString(), g.Visible)
            cstGoblins.SelectedIndex = cstGoblins.Items.Count - 1

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cmdGoblinsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoblinsRemove.Click
        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Remove the mesh.
        m_HOD.GoblinMeshes.RemoveAt(cstGoblins.SelectedIndex)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Update list.
        cstGoblins.Items.RemoveAt(cstGoblins.SelectedIndex)
        cstGoblins.SelectedIndex = cstGoblins.Items.Count - 1

    End Sub

    Private Sub cmdGoblinsRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoblinsRename.Click
        Dim name As String

        ' Get old name.
        Dim oldName As String = m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).Name

        Do
            ' Get new name.
            name = InputBox("Enter new name for mesh: ", Me.Text, oldName)

            ' See if the user pressed cancel.
            If name = "" Then _
    Exit Sub

            ' HACK: If only casing is changed, then rename list item twice.
            ' Otherwise, the change doesn't seem to be reflected in the list.
            If String.Compare(oldName, name, True) = 0 Then _
    cstGoblins.Items(cstGoblins.SelectedIndex) = "" _
            : Exit Do

            ' See if it's a duplicate.
            ' For that, first rename the mesh (in list only) to something else
            ' so that it doesn't interfere in our search.
            cstGoblins.Items(cstGoblins.SelectedIndex) = ""

            ' Now see if it's a duplicate.
            If _ListBoxHasString(cstGoblins.Items, name) Then _
    cstGoblins.Items(cstGoblins.SelectedIndex) = oldName _
            : MsgBox("The name you entered is a duplicate!" & vbCrLf &
           "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
            : Continue Do

            ' Exit loop.
            Exit Do

        Loop ' Do

        ' Not a duplicate. OK to rename.
        m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).Name = name

        ' Update list.
        cstGoblins.Items(cstGoblins.SelectedIndex) = name

        ' Update UI
        cstGoblins_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cmdGoblinsImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoblinsImport.Click
        ' See if user pressed cancel.
        If OpenOBJFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Initialize a new IOResult form.
        Dim f As New IOResult

        ' Create a new mesh to read.
        Dim m As New BasicMesh

        ' Ask whether to reverse U of UVs.
        Dim reverseUVs As Boolean = (MsgBox("Reverse UVs?", MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes)

        ' Try to read the wavefront object file.
        Try
            ' Read the wavefront object file.
            GenericMesh.Translators.WavefrontObject.ReadFile(OpenOBJFileDialog.FileName, m, reverseUVs)

            ' Validate the mesh.
            m.Validate()

        Catch ex As Exception
            ' Log the error.
            Trace.TraceError(ex.ToString)

            ' Clear the temp mesh.
            m.RemoveAll()
            m = Nothing

        End Try

        ' If the read succeeded...
        If m IsNot Nothing Then
            ' Pause render.
            m_D3DManager.RenderLoopPause()

            ' Unlock mesh.
            m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).Mesh.Unlock()

            ' Update mesh.
            m.CopyTo(m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).Mesh)
            _UnPrepareBasicMeshForExport(m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).Mesh)
            _SetBasicMeshVertexMasks(m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).Mesh,
                            VertexMasks.Position Or VertexMasks.Normal Or VertexMasks.Texture0 Or VertexMasks.Texture1)

            ' Lock mesh.
            m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).Mesh.Lock(m_D3DManager.Device)

            ' Resume render.
            m_D3DManager.RenderLoopResume()

            ' Now replace each material.
            For I As Integer = 0 To m.PartCount - 1
                Dim frm As New MaterialSubstitute(I,
                                      m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).Mesh,
                                      m_HOD,
                                      m.Material(I).Name)

                frm.ShowDialog()

            Next ' For I As Integer = 0 To m.PartCount - 1

            ' Remove the read mesh.
            m.RemoveAll()
            m = Nothing

        End If ' If m IsNot Nothing Then

        ' Display the result.
        f.Show()

    End Sub

    Private Sub cmdGoblinsExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoblinsExport.Click
        ' See if user pressed cancel.
        If SaveOBJFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Initialize a new IOResult form.
        Dim f As New IOResult

        ' Ask whether to reverse U of UVs.
        Dim reverseUVs As Boolean = (MsgBox("Reverse UVs?", MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes)

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Unlock mesh.
        m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).Mesh.Unlock()

        ' Update a few material properties...
        _PrepareBasicMeshForExport(m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).Mesh)

        ' Try to write the wavefront object file.
        Try
            GenericMesh.Translators.WavefrontObject.WriteFile(
    SaveOBJFileDialog.FileName,
    m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).Mesh,
    ReverseUVs:=reverseUVs
   )

        Catch ex As Exception
            Trace.TraceError(ex.ToString)

        End Try

        ' Remove the material properties that were set...
        _UnPrepareBasicMeshForExport(m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).Mesh)

        ' Lock mesh.
        m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).Mesh.Lock(m_D3DManager.Device)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Display the result.
        f.Show()

    End Sub

    Private Sub cmdGoblinsTransform_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoblinsTransform.Click
        ' Get the selected mesh.
        Dim m As BasicMesh = m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).Mesh

        ' Initialize new MeshTransformer.
        Dim f As New MeshTransformer

        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Unlock mesh.
        m.Unlock()

        ' Set the mesh.
        f.SetMesh(m)

        ' Lock mesh.
        m.Lock(m_D3DManager.Device)

        ' Resume rendering.
        m_D3DManager.RenderLoopResume()

        ' Display form.
        f.ShowDialog()

        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Unlock mesh.
        m.Unlock()

        ' Reverse faces if needed.
        If f.GetReverseFaces() Then _
   m.ReverseFaceOrder()

        ' Dispose form.
        f.Dispose()

        ' Transform the mesh.
        m.Transform(f.GetTransform())

        ' Lock mesh.
        m.Lock(m_D3DManager.Device)

        ' Resume rendering.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub cmdGoblinsRematerial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoblinsRematerial.Click
        ' Get the mesh.
        Dim m As BasicMesh = m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).Mesh

        ' Notify user if there are no parts.
        If m.PartCount = 0 Then _
   MsgBox("Mesh has no parts to re-assign materials!", MsgBoxStyle.Information, Me.Text) _
        : Exit Sub

        ' First update the mesh's material name and texture name fields.
        ' This needs to be done because that's from where we'll read old names.
        _PrepareBasicMeshForExport(m)

        ' Now substitute material for all parts.
        For I As Integer = 0 To m.PartCount - 1
            Dim f As New MaterialSubstitute(I, m, m_HOD, m.Material(I).Name)
            f.ShowDialog()
            f.Dispose()

        Next I ' For I As Integer = 0 To m.PartCount - 1

        ' Remove the material properties that were set...
        _UnPrepareBasicMeshForExport(m)

    End Sub

    Private Sub cmdGoblinsRenormal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoblinsRenormal.Click
        ' Get the mesh.
        Dim m As BasicMesh = m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).Mesh

        ' Notify user if there are no parts.
        If m.PartCount = 0 Then _
   MsgBox("Mesh has no parts to recalculate normals!", MsgBoxStyle.Information, Me.Text) _
        : Exit Sub

        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Unlock mesh.
        m.Unlock()

        ' Recalculate normals.
        m.CalculateNormals()

        ' Lock mesh.
        m.Lock(m_D3DManager.Device)

        ' Resume rendering.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub cmdGoblinsRetangent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoblinsRetangent.Click
        ' Get the mesh.
        Dim m As BasicMesh = m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).Mesh

        ' Notify user if there are no parts.
        If m.PartCount = 0 Then _
   MsgBox("Mesh has no parts to recalculate tangents!", MsgBoxStyle.Information, Me.Text) _
        : Exit Sub

        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Unlock mesh.
        m.Unlock()

        ' Recalculate tangents.
        m.CalculateTangents()

        ' Lock mesh.
        m.Lock(m_D3DManager.Device)

        ' Resume rendering.
        m_D3DManager.RenderLoopResume()

    End Sub

    Private Sub cboGoblinsParent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboGoblinsParent.SelectedIndexChanged
        ' Set the parent name if the combo box is focused.
        If (sender IsNot Nothing) AndAlso (CType(sender, ComboBox).Focused) Then _
   m_HOD.GoblinMeshes(cstGoblins.SelectedIndex).ParentName = CStr(cboGoblinsParent.SelectedItem)

    End Sub

#End Region

#Region " UI Meshes UI "

    ''' <summary>
    ''' Prepares the simple mesh for use after it has been imported, by
    ''' removing redundant primitive groups.
    ''' </summary>
    Private Sub _PrepareSimpleMeshAfterImport(ByVal m As SimpleMesh, ByVal out As SimpleMesh)
        ' Merge all parts of input mesh.
        m.MergeAll()

        ' Convert to triangle list.
        If m.PartCount <> 0 Then _
   m.Part(0).ConvertToList()

        ' Convert to wireframe if needed.
        If m_HOD.Name = HOD.Name_WireframeMesh Then
            ' Remove all parts and add a new one.
            out.RemoveAll()
            out.PartCount = 1

            With out.Part(0)
                ' Add vertices.
                .Vertices.Append(m.Part(0).Vertices)

                ' Add primitive group.
                .PrimitiveGroupCount = 1
                .PrimitiveGroups(0).Type = Direct3D.PrimitiveType.LineList

                For I As Integer = 0 To m.Part(0).PrimitiveGroupCount - 1
                    Select Case m.Part(0).PrimitiveGroups(I).Type
                        Case Direct3D.PrimitiveType.TriangleList
                            ' Append the edges of each triangle in the triangle primitive group.
                            For J As Integer = 0 To m.Part(0).PrimitiveGroups(I).IndiceCount - 3 Step 3
                                ' Get indices.
                                Dim ind1 As Integer = m.Part(0).PrimitiveGroups(I).Indice(J),
            ind2 As Integer = m.Part(0).PrimitiveGroups(I).Indice(J + 1),
            ind3 As Integer = m.Part(0).PrimitiveGroups(I).Indice(J + 2)

                                ' Make edge list.
                                Dim indiceArray() As Integer = New Integer() {ind1, ind2, ind2, ind3, ind3, ind1}

                                ' Append.
                                .PrimitiveGroups(0).Append(indiceArray)

                            Next J ' For J As Integer = 0 To m.Part(0).PrimitiveGroups(I).IndiceCount - 3 Step 3

                        Case Direct3D.PrimitiveType.LineList
                            ' Append the line primitive group.
                            .PrimitiveGroups(0).Append(m.Part(0).PrimitiveGroups(I))

                        Case Else
                            ' Probably a point list, ignore.
                            Trace.TraceWarning(m.Part(0).PrimitiveGroups(I).Type.ToString() &
                          " primitive group was ignored while importing simple mesh.")

                    End Select ' Select Case m.Part(0).PrimitiveGroups(I).Type
                Next I ' For I As Integer = 0 To m.Part(0).PrimitiveGroupCount - 1
            End With ' With out.Part(0)

        Else ' If m_HOD.Name = HOD.Name_WireframeMesh Then
            ' First see if mesh has parts.
            If m.PartCount <> 0 Then
                ' Remove non-triangle primitive groups.
                For I As Integer = m.Part(0).PrimitiveGroupCount - 1 To 0 Step -1
                    ' If not a triangle list, remove.
                    If m.Part(0).PrimitiveGroups(I).Type <> Direct3D.PrimitiveType.TriangleList Then _
      m.Part(0).RemovePrimitiveGroups(I)

                Next I ' For I As Integer = m.Part(0).PrimitiveGroupCount - 1 To 0 Step -1
            End If ' If m.PartCount <> 0 Then

            ' Update mesh.
            m.CopyTo(out)

        End If ' If m_HOD.Name = HOD.Name_WireframeMesh Then

    End Sub

    Private Sub tabUI_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabUI.Enter
        ' See if the tab enter event is marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        ' Update the checked list box.
        With cstUIMeshes.Items
            ' Clear old items.
            .Clear()

            ' Add all meshes.
            For I As Integer = 0 To m_HOD.SimpleMeshes.Count - 1
                Dim nameStr As String = m_HOD.SimpleMeshes(I).ToString()

                If nameStr = "" Then _
     If m_HOD.Name = HOD.Name_SimpleMesh Then _
      nameStr = "Solid Mesh " & CStr(I) _
     Else _
      nameStr = "Wireframe Mesh " & CStr(I)

                .Add(nameStr, m_HOD.SimpleMeshVisible(I))

            Next I ' For I As Integer = 0 To m_HOD.SimpleMeshes.Count - 1

        End With ' With cstUIMeshes.Items

        ' Update selection.
        cstUIMeshes_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cstUIMeshes_Cut()
        ' See if any item is selected.
        If cstUIMeshes.SelectedIndex = -1 Then _
   Exit Sub

        ' Get it's index.
        Dim ind As Integer = cstUIMeshes.SelectedIndex

        ' Copy to clipboard.
        mnuEditClipboard = New SimpleMesh(m_HOD.SimpleMeshes(ind))

        ' Remove from HOD.
        m_HOD.SimpleMeshes.RemoveAt(ind)

        ' Refresh.
        cstUIMeshes.Items.RemoveAt(ind)

    End Sub

    Private Sub cstUIMeshes_Copy()
        ' See if any item is selected.
        If cstUIMeshes.SelectedIndex = -1 Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New SimpleMesh(m_HOD.SimpleMeshes(cstUIMeshes.SelectedIndex))

    End Sub

    Private Sub cstUIMeshes_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is SimpleMesh Then _
   Exit Sub

        ' Get the item.
        Dim sm As SimpleMesh = CType(mnuEditClipboard, SimpleMesh)

        ' Remove reference.
        mnuEditClipboard = Nothing

        ' Lock mesh.
        sm.Lock(m_D3DManager.Device)

        ' Treat wireframe meshes seperately.
        If m_HOD.Name = HOD.Name_WireframeMesh Then _
   m_HOD.SimpleMeshes.Add(sm) _
        : cstUIMeshes.Items.Add("Wireframe Mesh " & CStr(m_HOD.SimpleMeshes.Count - 1),
                          m_HOD.SimpleMeshVisible(m_HOD.SimpleMeshes.Count - 1)) _
        : Exit Sub

        ' See if it's already present.
        If Not cstUIMeshes.Items.Contains(sm.ToString()) Then _
   m_HOD.SimpleMeshes.Add(sm) _
        : cstUIMeshes.Items.Add(sm, m_HOD.SimpleMeshVisible(m_HOD.SimpleMeshes.Count - 1)) _
        : Exit Sub

        ' Rename item.
        Dim number As Integer = 2

        Do
            ' See if this name is present.
            If cstUIMeshes.Items.Contains(sm.ToString() & CStr(number)) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, add it.
            sm.Name &= CStr(number)
            m_HOD.SimpleMeshes.Add(sm)
            cstUIMeshes.Items.Add(sm.ToString(), m_HOD.SimpleMeshVisible(m_HOD.SimpleMeshes.Count - 1))

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cstUIMeshes_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles cstUIMeshes.ItemCheck
        ' Set the visible flag if the checked list box is focused.
        If (sender IsNot Nothing) AndAlso (CType(sender, CheckedListBox).Focused) Then _
   m_HOD.SimpleMeshVisible(e.Index) = (e.NewValue = CheckState.Checked)

    End Sub

    Private Sub cstUIMeshes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cstUIMeshes.SelectedIndexChanged
        Dim enable As Boolean = (cstUIMeshes.SelectedIndex <> -1)

        ' Depending on whether a mesh is selected or not, enable or
        ' disable UI controls, except add button.
        cmdUIMeshesRemove.Enabled = enable
        cmdUIMeshesRename.Enabled = enable
        cmdUIMeshesImport.Enabled = enable
        cmdUIMeshesExport.Enabled = enable
        cmdUIMeshesRenormal.Enabled = enable

    End Sub

    Private Sub cstUIMeshesAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUIMeshesAdd.Click
        Dim number As Integer = cstUIMeshes.Items.Count + 1
        Dim name As String

        Do
            ' Decide a name for the material.
            name = "UI Mesh " & CStr(number)

            ' See if it's a duplicate.
            If _ListBoxHasString(cstUIMeshes.Items, name) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, add it.
            m_HOD.SimpleMeshes.Add(New SimpleMesh With {.Name = name})

            ' Add the material to the list.
            cstUIMeshes.Items.Add(name, m_HOD.SimpleMeshVisible(cstUIMeshes.Items.Count))
            cstUIMeshes.SelectedIndex = cstUIMeshes.Items.Count - 1

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cstUIMeshesRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUIMeshesRemove.Click
        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Remove the mesh.
        m_HOD.SimpleMeshes.RemoveAt(cstUIMeshes.SelectedIndex)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Update list.
        cstUIMeshes.Items.RemoveAt(cstUIMeshes.SelectedIndex)
        cstUIMeshes.SelectedIndex = cstUIMeshes.Items.Count - 1

        If m_HOD.Name = HOD.Name_WireframeMesh Then
            For I As Integer = 0 To cstUIMeshes.Items.Count - 1
                cstUIMeshes.Items(I) = "Wireframe Mesh " & CStr(I)

            Next I ' For I As Integer = 0 To cstUIMeshes.Items.Count - 1
        End If  ' If m_HOD.Name = HOD.Name_WireframeMesh Then

    End Sub

    Private Sub cstUIMeshesRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUIMeshesRename.Click
        Dim name As String

        ' Get old name.
        Dim oldName As String = m_HOD.SimpleMeshes(cstUIMeshes.SelectedIndex).Name

        Do
            ' Get new name.
            name = InputBox("Enter new name for mesh: ", Me.Text, oldName)

            ' See if the user pressed cancel.
            If name = "" Then _
    Exit Sub

            ' HACK: If only casing is changed, then rename list item twice.
            ' Otherwise, the change doesn't seem to be reflected in the list.
            If String.Compare(oldName, name, True) = 0 Then _
    cstUIMeshes.Items(cstUIMeshes.SelectedIndex) = "" _
            : Exit Do

            ' See if it's a duplicate.
            ' For that, first rename the mesh (in list only) to something else
            ' so that it doesn't interfere in our search.
            cstUIMeshes.Items(cstUIMeshes.SelectedIndex) = ""

            ' Now see if it's a duplicate.
            If _ListBoxHasString(cstUIMeshes.Items, name) Then _
    cstUIMeshes.Items(cstUIMeshes.SelectedIndex) = oldName _
            : MsgBox("The name you entered is a duplicate!" & vbCrLf &
           "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
            : Continue Do

            ' Exit loop.
            Exit Do

        Loop ' Do

        ' Not a duplicate. OK to rename.
        m_HOD.SimpleMeshes(cstUIMeshes.SelectedIndex).Name = name

        ' Update list.
        cstUIMeshes.Items(cstUIMeshes.SelectedIndex) = name

    End Sub

    Private Sub cstUIMeshesImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUIMeshesImport.Click
        ' See if user pressed cancel.
        If OpenOBJFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Initialize a new IOResult form.
        Dim f As New IOResult

        ' Create a new mesh to read.
        Dim m As New SimpleMesh

        ' Try to read the wavefront object file.
        Try
            ' Read the wavefront object file.
            GenericMesh.Translators.WavefrontObject.ReadFile(OpenOBJFileDialog.FileName, m)

            ' Validate the mesh.
            m.Validate()

        Catch ex As Exception
            ' Log the error.
            Trace.TraceError(ex.ToString)

            ' Clear the temp mesh.
            m.RemoveAll()
            m = Nothing

        End Try

        ' If the read succeeded...
        If m IsNot Nothing Then
            ' Pause render.
            m_D3DManager.RenderLoopPause()

            ' Unlock mesh.
            m_HOD.SimpleMeshes(cstUIMeshes.SelectedIndex).Unlock()

            ' Prepare mesh.
            _PrepareSimpleMeshAfterImport(m, m_HOD.SimpleMeshes(cstUIMeshes.SelectedIndex))

            ' Remove the read mesh.
            m.RemoveAll()
            m = Nothing

            ' Lock mesh.
            m_HOD.SimpleMeshes(cstUIMeshes.SelectedIndex).Lock(m_D3DManager.Device)

            ' Resume render.
            m_D3DManager.RenderLoopResume()

        End If ' If m IsNot Nothing Then

        ' Display the result.
        f.Show()

    End Sub

    Private Sub cstUIMeshesExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUIMeshesExport.Click
        ' See if user pressed cancel.
        If SaveOBJFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Initialize a new IOResult form.
        Dim f As New IOResult

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Unlock mesh.
        m_HOD.SimpleMeshes(cstUIMeshes.SelectedIndex).Unlock()

        ' Try to write the wavefront object file.
        Try
            GenericMesh.Translators.WavefrontObject.WriteFile(
    SaveOBJFileDialog.FileName,
    m_HOD.SimpleMeshes(cstUIMeshes.SelectedIndex)
   )

        Catch ex As Exception
            Trace.TraceError(ex.ToString)

        End Try

        ' Lock mesh.
        m_HOD.SimpleMeshes(cstUIMeshes.SelectedIndex).Lock(m_D3DManager.Device)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Display the result.
        f.Show()

    End Sub

    Private Sub cmdUIMeshesRenormal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUIMeshesRenormal.Click
        ' Get the mesh.
        Dim m As SimpleMesh = m_HOD.SimpleMeshes(cstUIMeshes.SelectedIndex)

        ' Notift user if wireframe mesh.
        If m.IsWireframe Then _
   MsgBox("Wireframe meshes do not require normals.", MsgBoxStyle.Information, Me.Text) _
        : Exit Sub

        ' Notify user if there are no parts.
        If m.PartCount = 0 Then _
   MsgBox("Mesh has no parts to recalculate normals!", MsgBoxStyle.Information, Me.Text) _
        : Exit Sub

        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Unlock mesh.
        m.Unlock()

        ' Recalculate normals.
        m.CalculateNormals()

        ' Lock mesh.
        m.Lock(m_D3DManager.Device)

        ' Resume rendering.
        m_D3DManager.RenderLoopResume()

    End Sub

#End Region

#Region " Background Meshes UI "

    Private Sub tabBGMS_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabBGMS.Enter
        ' See if the tab enter event is marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        ' Update the checked list box.
        With cstBackgroundMeshes.Items
            ' Clear old items.
            .Clear()

            ' Add all meshes.
            For I As Integer = 0 To m_HOD.BackgroundMeshes.Count - 1
                .Add("Background Mesh " & CStr(I + 1), m_HOD.BackgroundMeshVisible(I))

            Next I ' For I As Integer = 0 To m_HOD.BackgroundMeshes.Count - 1
        End With ' With cstBackgroundMeshes.Items

        ' Update selection.
        cstBackgroundMeshes_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cstBackgroundMeshes_Cut()
        ' See if any item is selected.
        If cstBackgroundMeshes.SelectedIndex = -1 Then _
   Exit Sub

        ' Get it's index.
        Dim ind As Integer = cstBackgroundMeshes.SelectedIndex

        ' Copy to clipboard.
        mnuEditClipboard = New BasicMesh(m_HOD.BackgroundMeshes(ind))

        ' Remove from HOD.
        m_HOD.BackgroundMeshes.RemoveAt(ind)

        ' Refresh.
        cstBackgroundMeshes.Items.RemoveAt(ind)

    End Sub

    Private Sub cstBackgroundMeshes_Copy()
        ' See if any item is selected.
        If cstBackgroundMeshes.SelectedIndex = -1 Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New BasicMesh(m_HOD.BackgroundMeshes(cstBackgroundMeshes.SelectedIndex))

    End Sub

    Private Sub cstBackgroundMeshes_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is BasicMesh Then _
   Exit Sub

        ' Get the item.
        Dim bm As BasicMesh = CType(mnuEditClipboard, BasicMesh)

        ' Remove reference.
        mnuEditClipboard = Nothing

        ' Lock it.
        bm.Lock(m_D3DManager.Device)

        ' Add it.
        m_HOD.BackgroundMeshes.Add(bm)
        cstBackgroundMeshes.Items.Add("Background Mesh " & CStr(m_HOD.BackgroundMeshes.Count),
                                m_HOD.BackgroundMeshVisible(m_HOD.BackgroundMeshes.Count - 1))

    End Sub

    Private Sub cstBackgroundMeshes_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles cstBackgroundMeshes.ItemCheck
        ' Set the visible flag if the checked list box is focused.
        If (sender IsNot Nothing) AndAlso (CType(sender, CheckedListBox).Focused) Then _
   m_HOD.BackgroundMeshVisible(e.Index) = (e.NewValue = CheckState.Checked)

    End Sub

    Private Sub cstBackgroundMeshes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cstBackgroundMeshes.SelectedIndexChanged
        Dim enable As Boolean = (cstBackgroundMeshes.SelectedIndex <> -1)

        ' Depending on whether a mesh is selected or not, enable or
        ' disable UI controls, except add button.
        cmdBackgroundMeshesRemove.Enabled = enable
        cmdBackgroundMeshesRecolourize.Enabled = enable
        cmdBackgroundMeshesImport.Enabled = enable
        cmdBackgroundMeshesExport.Enabled = enable

    End Sub

    Private Sub cmdBackgroundMeshesAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBackgroundMeshesAdd.Click
        Dim number As Integer = cstBackgroundMeshes.Items.Count

        ' Add mesh.
        m_HOD.BackgroundMeshes.Add(New BasicMesh)

        ' Update list.
        cstBackgroundMeshes.Items.Add("Background Mesh " & CStr(number + 1), m_HOD.BackgroundMeshVisible(number))
        cstBackgroundMeshes.SelectedIndex = number

    End Sub

    Private Sub cmdBackgroundMeshesRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBackgroundMeshesRemove.Click
        Dim ind As Integer = cstBackgroundMeshes.SelectedIndex

        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Remove mesh.
        m_HOD.BackgroundMeshes.RemoveAt(ind)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Remove from list.
        cstBackgroundMeshes.Items.RemoveAt(ind)
        cstBackgroundMeshes.SelectedIndex = cstBackgroundMeshes.Items.Count - 1

        ' Update list.
        For I As Integer = ind To m_HOD.BackgroundMeshes.Count - 1
            cstBackgroundMeshes.Items(I) = "Background Mesh " & CStr(I + 1)

        Next I ' For I As Integer = Ind To m_HOD.BackgroundMeshes.Count - 1

    End Sub

    Private Sub cmdBackgroundMeshesImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBackgroundMeshesImport.Click
        ' See if user pressed cancel.
        If OpenOBJFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Initialize a new IOResult form.
        Dim f As New IOResult

        ' Create a new mesh to read.
        Dim m As New BasicMesh

        ' Try to read the wavefront object file.
        Try
            ' Read the wavefront object file.
            GenericMesh.Translators.WavefrontObject.ReadFile(OpenOBJFileDialog.FileName, m)

            ' Validate the mesh.
            m.Validate()

        Catch ex As Exception
            ' Log the error.
            Trace.TraceError(ex.ToString)

            ' Clear the temp mesh.
            m.RemoveAll()
            m = Nothing

        End Try

        ' If the read succeeded...
        If m IsNot Nothing Then
            ' Pause render.
            m_D3DManager.RenderLoopPause()

            ' Unlock mesh.
            m_HOD.BackgroundMeshes(cstBackgroundMeshes.SelectedIndex).Unlock()

            ' Prepare mesh.
            m.CopyTo(m_HOD.BackgroundMeshes(cstBackgroundMeshes.SelectedIndex))
            _SetBasicMeshVertexMasks(m_HOD.BackgroundMeshes(cstBackgroundMeshes.SelectedIndex),
                            VertexMasks.Position Or VertexMasks.Colour)

            ' Remove the read mesh.
            m.RemoveAll()
            m = Nothing

            ' Recalculate normals.
            m_HOD.BackgroundMeshes(cstBackgroundMeshes.SelectedIndex).CalculateNormals()

            ' Lock mesh.
            m_HOD.BackgroundMeshes(cstBackgroundMeshes.SelectedIndex).Lock(m_D3DManager.Device)

            ' Resume render.
            m_D3DManager.RenderLoopResume()

        End If ' If m IsNot Nothing Then

        ' Display the result.
        f.Show()

    End Sub

    Private Sub cmdBackgroundMeshesExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBackgroundMeshesExport.Click
        ' See if user pressed cancel.
        If SaveOBJFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Initialize a new IOResult form.
        Dim f As New IOResult

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Unlock mesh.
        m_HOD.BackgroundMeshes(cstBackgroundMeshes.SelectedIndex).Unlock()

        ' Prepare for export.
        _UnPrepareBasicMeshForExport(m_HOD.BackgroundMeshes(cstBackgroundMeshes.SelectedIndex))

        ' Try to write the wavefront object file.
        Try
            GenericMesh.Translators.WavefrontObject.WriteFile(
    SaveOBJFileDialog.FileName,
    m_HOD.BackgroundMeshes(cstBackgroundMeshes.SelectedIndex),
    False, False, True, True
   )

        Catch ex As Exception
            Trace.TraceError(ex.ToString)

        End Try

        ' Lock mesh.
        m_HOD.BackgroundMeshes(cstBackgroundMeshes.SelectedIndex).Lock(m_D3DManager.Device)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Display the result.
        f.Show()

    End Sub

    Private Sub cmdBackgroundMeshesRecolourize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBackgroundMeshesRecolourize.Click
        Dim tex As Direct3D.Texture = Nothing
        Dim stream As IO.Stream

        ' See if user pressed cancel.
        If OpenTextureFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Try to open stream...
        stream = _TryOpenStream(OpenTextureFileDialog.FileName, True)

        If stream Is Nothing Then _
   Exit Sub

        ' Try to open texture...
        Try
            tex = Direct3D.TextureLoader.FromStream(m_D3DManager.Device,
                                           stream,
                                           0, 0, 1,
                                           Direct3D.Usage.None,
                                           Direct3D.Format.A8R8G8B8,
                                           Direct3D.Pool.SystemMemory,
                                           Direct3D.Filter.None,
                                           Direct3D.Filter.None,
                                           0)

        Catch ex As Direct3D.InvalidDataException
            MsgBox("The texture you provided seems to be invalid!",
          MsgBoxStyle.Critical, Me.Text)

        Catch ex As Exception
            MsgBox("Error while trying to open texture: " & vbCrLf &
          ex.ToString(), MsgBoxStyle.Critical, Me.Text)

        End Try

        If tex Is Nothing Then _
   Exit Sub

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Unlock mesh.
        m_HOD.BackgroundMeshes(cstBackgroundMeshes.SelectedIndex).Unlock()

        ' Re-colour the part.
        m_HOD.BackgroundMeshes(cstBackgroundMeshes.SelectedIndex).Recolourize(tex)

        ' Lock mesh.
        m_HOD.BackgroundMeshes(cstBackgroundMeshes.SelectedIndex).Lock(m_D3DManager.Device)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Dispose texture.
        tex.Dispose()

    End Sub

    Private Sub cmdBackgroundMeshesRecolourizeAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBackgroundMeshesRecolourizeAll.Click
        Dim tex As Direct3D.Texture = Nothing
        Dim stream As IO.Stream

        ' See if user pressed cancel.
        If OpenTextureFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Try to open stream...
        stream = _TryOpenStream(OpenTextureFileDialog.FileName, True)

        If stream Is Nothing Then _
   Exit Sub

        ' Try to open texture...
        Try
            tex = Direct3D.TextureLoader.FromStream(m_D3DManager.Device,
                                           stream,
                                           0, 0, 1,
                                           Direct3D.Usage.None,
                                           Direct3D.Format.A8R8G8B8,
                                           Direct3D.Pool.SystemMemory,
                                           Direct3D.Filter.None,
                                           Direct3D.Filter.None,
                                           0)

        Catch ex As Direct3D.InvalidDataException
            MsgBox("The texture you provided seems to be invalid!",
          MsgBoxStyle.Critical, Me.Text)

        Catch ex As Exception
            MsgBox("Error while trying to open texture: " & vbCrLf &
          ex.ToString(), MsgBoxStyle.Critical, Me.Text)

        End Try

        If tex Is Nothing Then _
   Exit Sub

        ' Convert texture to array.
        Dim w, h As Integer

        With tex.GetLevelDescription(0)
            w = .Width
            h = .Height

        End With ' With tex.GetLevelDescription(0)

        ' Check size.
        If (w <= 1) OrElse (h <= 1) Then _
   MsgBox("The texture is too small. Please use a larger texture!",
          MsgBoxStyle.Exclamation, Me.Text) _
        : Exit Sub

        ' Make array.
        Dim texture(w * h - 1) As Integer

        ' Lock texture.
        Dim g As GraphicsStream = tex.LockRectangle(0, Direct3D.LockFlags.ReadOnly)
        Dim BR As New IO.BinaryReader(g)
        Dim oldPos As Long = g.Position

        ' Read array.
        For Y As Integer = 0 To h - 1
            For X As Integer = 0 To w - 1
                texture(Y * h + X) = BR.ReadInt32()

            Next X ' For X As Integer = 0 To w - 1
        Next Y ' For Y As Integer = 0 To h - 1

        ' Unlock rectangle.
        g.Position = oldPos
        tex.UnlockRectangle(0)

        ' Dispose texture.
        tex.Dispose()

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Re-colour all the parts.
        For I As Integer = 0 To m_HOD.BackgroundMeshes.Count - 1
            ' Unlock mesh.
            m_HOD.BackgroundMeshes(I).Unlock()

            ' Re-colour part.
            m_HOD.BackgroundMeshes(I).Recolourize(w, h, texture)

            ' Lock mesh.
            m_HOD.BackgroundMeshes(I).Lock(m_D3DManager.Device)

        Next I ' For I As Integer = 0 To m_HOD.BackgroundMeshes.Count - 1

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Erase array.
        Erase texture

    End Sub

    Private Sub cmdBackgroundMeshesGenerateTexture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBackgroundMeshesGenerateTexture.Click
        Dim doubleTone, halfOffset As Boolean
        Dim sizeMultiplier As Single

        ' Ask whether to double tone.
        Select Case MsgBox("Double tone image?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text)
            Case MsgBoxResult.Yes
                doubleTone = True

            Case MsgBoxResult.No
                doubleTone = False

            Case MsgBoxResult.Cancel
                Exit Sub

        End Select ' Select Case MsgBox("Double tone image?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text)

        ' Ask whether give a half offset.
        Select Case MsgBox("Offset center of mesh?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text)
            Case MsgBoxResult.Yes
                halfOffset = True

            Case MsgBoxResult.No
                halfOffset = False

            Case MsgBoxResult.Cancel
                Exit Sub

        End Select ' Select Case MsgBox("Offset center of mesh?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text)

        ' Ask size multiplier.
        Do
            Dim str As String = InputBox("Enter size multiplier (0, 2]:" & vbCrLf &
                                "(for example," & vbCrLf &
                                " 0.5 gives an image of size 1024 x 512, " & vbCrLf &
                                " 1.0 gives an image of size 2048 x 1024, " & vbCrLf &
                                " 2.0 gives an image of size 4096 x 2048)", Me.Text, CStr(1.0F))

            ' See if user pressed cancel.
            If str = "" Then _
    Exit Sub

            ' See if it's numeric.
            If Not IsNumeric(str) Then _
    MsgBox("Please enter a numeric value!", MsgBoxStyle.Exclamation, Me.Text) _
            : Continue Do

            ' Convert to number.
            sizeMultiplier = CSng(str)

            ' See if it's valid.
            If (sizeMultiplier <= 0) OrElse (sizeMultiplier > 2) Then _
    MsgBox("Size multiplier must be greater than zero" & vbCrLf &
            "and less than or equal to two!", MsgBoxStyle.Exclamation, Me.Text) _
            : Continue Do

            ' Must be OK. Exit loop.
            Exit Do

        Loop ' Do

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Unlock meshes.
        For I As Integer = 0 To m_HOD.BackgroundMeshes.Count - 1
            m_HOD.BackgroundMeshes(I).Unlock()

        Next I ' For I As Integer = 0 To m_HOD.BackgroundMeshes.Count - 1

        ' Initialize new form.
        Dim f As New HODBGTexGen(m_HOD, doubleTone, halfOffset, sizeMultiplier)

        ' Lock meshes.
        For I As Integer = 0 To m_HOD.BackgroundMeshes.Count - 1
            m_HOD.BackgroundMeshes(I).Lock(m_D3DManager.Device)

        Next I ' For I As Integer = 0 To m_HOD.BackgroundMeshes.Count - 1

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Show form. Then dispose it.
        f.ShowDialog()
        f.Dispose()

    End Sub

#End Region

#Region " Joints UI "

    ''' <summary>
    ''' Adds a joint and it's children to a tree view node collection,
    ''' recursively.
    ''' </summary>
    Private Sub _AddJoint(ByVal j As Joint, ByVal parent As Windows.Forms.TreeNodeCollection)
        ' Add the node and get parent.
        Dim node As TreeNode = parent.Add(j.Name)

        ' Set visible flag.
        node.Checked = j.Visible

        ' Add all children of this joint to the node we just got.
        For I As Integer = 0 To j.Children.Count - 1
            _AddJoint(j.Children(I), node.Nodes)

        Next I ' For I As Integer = 0 To j.Children.Count - 1

    End Sub

    ''' <summary>
    ''' Gets a joint from a tree node.
    ''' </summary>
    Private Function _GetJoint(ByVal j As TreeNode) As Joint
        Dim s As New Stack(Of TreeNode)
        Dim out As Joint = m_HOD.Root

        ' Push the path from current node to top.
        Do Until j Is Nothing
            s.Push(j)
            j = j.Parent

        Loop ' Do Until j Is Nothing

        ' Pop the root node.
        s.Pop()

        ' Get back to joint by popping existing nodes.
        Do Until s.Count = 0
            out = out.Children(s.Pop().Index)

        Loop ' Do Until s.Count = 0

        ' Return the joint.
        Return out

    End Function

    ''' <summary>
    ''' Checks a node and all it's children, recursively.
    ''' </summary>
    Private Sub _CheckNodeAndChildren(ByVal node As TreeNode, ByVal check As Boolean)
        node.Checked = check

        For I As Integer = 0 To node.Nodes.Count - 1
            _CheckNodeAndChildren(node.Nodes(I), check)

        Next I ' For I As Integer = 0 To node.Nodes.Count - 1

    End Sub

    Private Sub tabJoints_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabJoints.Enter
        ' See if the tab enter event is marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        ' Suppress repainting the tree view until all
        ' the items have been added.
        tvwJoints.BeginUpdate()

        ' Clear old items.
        tvwJoints.Nodes.Clear()

        ' Add root and it's children.
        _AddJoint(m_HOD.Root, tvwJoints.Nodes)

        ' Expand the root node.
        tvwJoints.Nodes(0).Expand()

        ' Select the root node.
        tvwJoints.SelectedNode = tvwJoints.Nodes(0)

        ' Begin repainting the tree view.
        tvwJoints.EndUpdate()

        ' Update check box.
        chkJointsRender.Checked = m_HOD.SkeletonVisible

    End Sub

    Private Sub tvwJoints_Cut()
        ' See if any item is selected.
        If tvwJoints.SelectedNode Is Nothing Then _
   Exit Sub

        ' Get the item.
        Dim node As TreeNode = tvwJoints.SelectedNode
        Dim parent As TreeNode = node.Parent
        Dim jNode As Joint = _GetJoint(node)
        Dim jParent As Joint = jNode.Parent

        ' Copy to clipboard.
        mnuEditClipboard = New Joint(jNode)

        ' Refresh.
        parent.Nodes.Remove(node)

        ' Remove from HOD.
        jParent.Children.Remove(jNode)

    End Sub

    Private Sub tvwJoints_Copy()
        ' See if any item is selected.
        If tvwJoints.SelectedNode Is Nothing Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New Joint(_GetJoint(tvwJoints.SelectedNode))

    End Sub

    Private Sub tvwJoints_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is Joint Then _
   Exit Sub

        ' Get the item.
        Dim j As Joint = CType(mnuEditClipboard, Joint)

        ' Remove reference.
        mnuEditClipboard = Nothing

        ' Get the joints in list.
        Dim joints() As Joint = j.ToArray()
        Dim rename As Boolean = False

        ' See if it's already present.
        For I As Integer = 0 To joints.Length - 1
            If m_HOD.Root.Contains(joints(I).Name) Then _
    rename = True _
            : Exit For

        Next I ' For I As Integer = 0 To joints.Length - 1

        ' If not already present, then add.
        If Not rename Then _
   _GetJoint(tvwJoints.SelectedNode).Children.Add(j) _
        : tabJoints_Enter(Nothing, EventArgs.Empty) _
        : Erase joints _
        : Exit Sub

        ' Rename item.
        Dim number As Integer = 2

        Do
            ' Reset flag.
            rename = False

            ' See if this name is present.
            For I As Integer = 0 To joints.Length - 1
                If m_HOD.Root.Contains(joints(I).Name & CStr(number)) Then _
     rename = True _
                : Exit For

            Next I ' For I As Integer = 0 To joints.Length - 1

            ' Check for duplicate.
            If rename Then _
    number += 1 _
            : Continue Do

            ' Rename joints.
            For I As Integer = 0 To joints.Length - 1
                joints(I).Name &= CStr(number)

            Next I ' For I As Integer = 0 To joints.Length - 1

            ' Since it's not a duplicate, add it.
            _GetJoint(tvwJoints.SelectedNode).Children.Add(j)
            tabJoints_Enter(Nothing, EventArgs.Empty)

            ' Erase array.
            Erase joints

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub tvwJoints_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwJoints.AfterCheck
        _GetJoint(e.Node).Visible = e.Node.Checked

    End Sub

    Private Sub tvwJoints_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvwJoints.AfterLabelEdit
        ' Get the joint from node.
        Dim j As Joint = _GetJoint(e.Node)

        ' See if editing was canceled.
        If (e.Label Is Nothing) OrElse (e.Label = "") Then _
   e.CancelEdit = True _
        : Exit Sub

        ' If only casing was changed, do not validate.
        If String.Compare(e.Node.Text, e.Label, True) = 0 Then _
   j.Name = e.Label _
        : Exit Sub

        ' Rename joint to a name that will not interfere with our search.
        j.Name = New String(Chr(0), 1)

        If m_HOD.Root.Contains(e.Label) Then _
   MsgBox("The name you entered is a duplicate!", MsgBoxStyle.Exclamation, Me.Text) _
        : j.Name = e.Node.Text _
        : e.CancelEdit = True _
        : Exit Sub

        ' Allow change, since it's not a duplicate.
        j.Name = e.Label

    End Sub

    Private Sub tvwJoints_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwJoints.AfterSelect
        ' Get the joint.
        Dim j As Joint = _GetJoint(e.Node)

        ' Setup the text boxes.
        txtJointsPositionX.Text = CStr(j.Position.X)
        txtJointsPositionY.Text = CStr(j.Position.Y)
        txtJointsPositionZ.Text = CStr(j.Position.Z)

        txtJointsRotationX.Text = FormatNumber(180 * j.Rotation.X / Math.PI, 3, TriState.True, TriState.False, TriState.False)
        txtJointsRotationY.Text = FormatNumber(180 * j.Rotation.Y / Math.PI, 3, TriState.True, TriState.False, TriState.False)
        txtJointsRotationZ.Text = FormatNumber(180 * j.Rotation.Z / Math.PI, 3, TriState.True, TriState.False, TriState.False)

        txtJointsScaleX.Text = CStr(j.Scale.X)
        txtJointsScaleY.Text = CStr(j.Scale.Y)
        txtJointsScaleZ.Text = CStr(j.Scale.Z)

        txtJointsAxisX.Text = FormatNumber(180 * j.Axis.X / Math.PI, 3, TriState.True, TriState.False, TriState.False)
        txtJointsAxisY.Text = FormatNumber(180 * j.Axis.Y / Math.PI, 3, TriState.True, TriState.False, TriState.False)
        txtJointsAxisZ.Text = FormatNumber(180 * j.Axis.Z / Math.PI, 3, TriState.True, TriState.False, TriState.False)

        ' Setup the check boxes.
        chkJointsDegreeOfFreedomX.Checked = (j.DegreeOfFreedom.X <> 0)
        chkJointsDegreeOfFreedomY.Checked = (j.DegreeOfFreedom.Y <> 0)
        chkJointsDegreeOfFreedomZ.Checked = (j.DegreeOfFreedom.Z <> 0)

    End Sub

    Private Sub tvwJoints_BeforeLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles tvwJoints.BeforeLabelEdit
        ' Do not allow user to edit top level node.
        If e.Node.Parent Is Nothing Then _
   MsgBox("Cannot rename Root level node!", MsgBoxStyle.Exclamation, Me.Text) _
        : e.CancelEdit = True _
        : Exit Sub

    End Sub

    Private Sub tvwJoints_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvwJoints.MouseClick
        ' Select node on right click.
        If e.Button = Windows.Forms.MouseButtons.Right Then _
   tvwJoints.SelectedNode = tvwJoints.GetNodeAt(e.Location)

    End Sub

    Private Sub tvwJoints_cms_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles tvwJoints_cms.Opening
        ' Disable rename item if root joint is selected.
        tvwJoints_cmsRename.Enabled = tvwJoints.SelectedNode IsNot tvwJoints.Nodes(0)

    End Sub

    Private Sub tvwJoints_cmsRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tvwJoints_cmsRename.Click
        ' Begin rename.
        tvwJoints.SelectedNode.BeginEdit()

    End Sub

    Private Sub tvwJoints_cmsHideShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvwJoints_cmsHide.Click, tvwJoints_cmsShow.Click
        ' See if the joints are to be marked visible or not.
        Dim visible As Boolean = (sender Is tvwJoints_cmsShow)

        ' Pause update to tree view.
        tvwJoints.BeginUpdate()

        ' Check node and children.
        _CheckNodeAndChildren(tvwJoints.SelectedNode, visible)

        ' Update tree view.
        tvwJoints.EndUpdate()

    End Sub

    Private Sub tvwJoints_cmsCollapse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tvwJoints_cmsCollapse.Click, tvwJoints_cmsExpand.Click
        ' Collapse, including children.
        If sender Is tvwJoints_cmsCollapse Then _
   tvwJoints.SelectedNode.Collapse(False)

        ' Expand, including children.
        If sender Is tvwJoints_cmsExpand Then _
   tvwJoints.SelectedNode.ExpandAll()

    End Sub

    Private Sub chkJointsRender_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkJointsRender.CheckedChanged
        If Not chkJointsRender.Focused Then _
   Exit Sub

        m_HOD.SkeletonVisible = chkJointsRender.Checked

    End Sub

    Private Sub cmdJointsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdJointsAdd.Click
        Dim number As Integer = tvwJoints.SelectedNode.Nodes.Count + 1
        Dim name As String

        ' Get the parent joint.
        Dim j As Joint = _GetJoint(tvwJoints.SelectedNode)

        Do
            ' Decide a name for joint.
            name = "Joint " & CStr(number)

            ' See if it's a duplicate.
            If m_HOD.Root.Contains(name) Then _
    number += 1 _
            : Continue Do

            ' Name is OK. Add the joint.
            j.Children.Add(New Joint With {.Name = name})

            ' Update tree.
            tvwJoints.SelectedNode = tvwJoints.SelectedNode.Nodes.Add(name)

            ' Exit loop
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cmdJointsAddTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdJointsAddTemplate.Click
        ' Get the selected joint.
        Dim j As Joint = _GetJoint(tvwJoints.SelectedNode)

        ' Create new form.
        Dim f As New JointTemplates(m_HOD, _GetJoint(tvwJoints.SelectedNode))

        ' Show the form, then refresh tree view if needed.
        If f.ShowDialog() = DialogResult.OK Then _
   tabJoints_Enter(Nothing, EventArgs.Empty)

        ' Dispose form.
        f.Dispose()

    End Sub

    Private Sub cmdJointsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdJointsRemove.Click
        ' Get the selected joint.
        Dim j As Joint = _GetJoint(tvwJoints.SelectedNode)

        ' Do not let user remove root joint.
        If j Is m_HOD.Root Then _
   MsgBox("Cannot remove root joint!", MsgBoxStyle.Exclamation, Me.Text) _
        : Exit Sub

        ' Remove from tree.
        tvwJoints.SelectedNode.Parent.Nodes.Remove(tvwJoints.SelectedNode)
        tvwJoints.SelectedNode = tvwJoints.Nodes(0)

        ' Remove it from it's parent's list.
        j.Parent.Children.Remove(j)

        ' Remove references in MAD.
        For I As Integer = 0 To m_MAD.Animations.Count - 1
            For K As Integer = m_MAD.Animations(I).Joints.Count - 1 To 0 Step -1
                ' If this joint is referenced, remove it.
                If m_MAD.Animations(I).Joints(K).Joint Is j Then _
     m_MAD.Animations(I).Joints.RemoveAt(K)

            Next K 'For K As Integer = m_MAD.Animations(I).Joints.Count - 1 To 0 Step -1
        Next I ' For I As Integer = 0 To m_MAD.Animations.Count - 1

        ' Delete the joint itself.
        j.Delete()

    End Sub

    Private Sub txtJoints_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtJointsPositionX.TextChanged, txtJointsPositionY.TextChanged, txtJointsPositionZ.TextChanged,
         txtJointsRotationX.TextChanged, txtJointsRotationY.TextChanged, txtJointsRotationZ.TextChanged,
         txtJointsScaleX.TextChanged, txtJointsScaleY.TextChanged, txtJointsScaleZ.TextChanged,
         txtJointsAxisX.TextChanged, txtJointsAxisY.TextChanged, txtJointsAxisZ.TextChanged

        ' Get the text box.
        Dim textBox As TextBox = CType(sender, TextBox)

        ' See if it's focused.
        If Not textBox.Focused Then _
   Exit Sub

        ' See if it has valid, numeric data.
        If (textBox.Text = "") OrElse (Not IsNumeric(textBox.Text)) Then _
   Exit Sub

        ' Get the joint.
        Dim j As Joint = _GetJoint(tvwJoints.SelectedNode)

        ' Get the number.
        Dim number As Single = CSng(textBox.Text)
        Dim rotation As Single = CSng(Math.PI * number / 180)

        ' Find out which text box caused this event.
        ' Position X
        If sender Is txtJointsPositionX Then _
   j.Position = New Vector3(number, j.Position.Y, j.Position.Z)

        ' Position Y
        If sender Is txtJointsPositionY Then _
   j.Position = New Vector3(j.Position.X, number, j.Position.Z)

        ' Position Z
        If sender Is txtJointsPositionZ Then _
   j.Position = New Vector3(j.Position.X, j.Position.Y, number)

        ' Rotation X
        If sender Is txtJointsRotationX Then _
   j.Rotation = New Vector3(rotation, j.Rotation.Y, j.Rotation.Z)

        ' Rotation Y
        If sender Is txtJointsRotationY Then _
   j.Rotation = New Vector3(j.Rotation.X, rotation, j.Rotation.Z)

        ' Rotation Z
        If sender Is txtJointsRotationZ Then _
   j.Rotation = New Vector3(j.Rotation.X, j.Rotation.Y, rotation)

        ' Scale X
        If sender Is txtJointsScaleX Then _
   j.Scale = New Vector3(number, j.Scale.Y, j.Scale.Z)

        ' Scale Y
        If sender Is txtJointsScaleY Then _
   j.Scale = New Vector3(j.Scale.X, number, j.Scale.Z)

        ' Scale Z
        If sender Is txtJointsScaleZ Then _
   j.Scale = New Vector3(j.Scale.X, j.Scale.Y, number)

        ' Axis X
        If sender Is txtJointsAxisX Then _
   j.Axis = New Vector3(rotation, j.Axis.Y, j.Axis.Z)

        ' Axis Y
        If sender Is txtJointsAxisY Then _
   j.Axis = New Vector3(j.Axis.X, rotation, j.Axis.Z)

        ' Axis Z
        If sender Is txtJointsAxisZ Then _
   j.Axis = New Vector3(j.Axis.X, j.Axis.Y, rotation)

        ' Update animations if needed.
        If (m_MAD.Animations.Count <> 0) Then
            For I As Integer = 0 To m_MAD.Animations.Count - 1
                For K As Integer = 0 To m_MAD.Animations(I).Joints.Count - 1
                    ' Update default transform if needed.
                    If m_MAD.Animations(I).Joints(K).Joint Is j Then _
      m_MAD.Animations(I).Joints(K).Joint = j

                Next K ' For K As Integer = 0 To m_MAD.Animations(I).Joints.Count - 1
            Next I ' For I As Integer = 0 To m_MAD.Animations.Count - 1
        End If ' If (m_MAD.Animations.Count <> 0) Then

    End Sub

    Private Sub txtJoints_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtJointsPositionX.Validated, txtJointsPositionY.Validated, txtJointsPositionZ.Validated,
         txtJointsRotationX.Validated, txtJointsRotationY.Validated, txtJointsRotationZ.Validated,
         txtJointsScaleX.Validated, txtJointsScaleY.Validated, txtJointsScaleZ.Validated,
         txtJointsAxisX.Validated, txtJointsAxisY.Validated, txtJointsAxisZ.Validated

        ' Update error to no error.
        ErrorProvider.SetError(CType(sender, Control), "")

    End Sub

    Private Sub chkJointsDegreeOfFreedom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles chkJointsDegreeOfFreedomX.CheckedChanged, chkJointsDegreeOfFreedomY.CheckedChanged, chkJointsDegreeOfFreedomZ.CheckedChanged

        ' Get the check box.
        Dim checkBox As CheckBox = CType(sender, CheckBox)

        ' See if it's focused.
        If (checkBox Is Nothing) OrElse (Not checkBox.Focused) Then _
   Exit Sub

        ' Get the value.
        Dim value As Single = CSng(IIf(checkBox.Checked, 1.0F, 0.0F))

        ' Get the joint.
        Dim j As Joint = _GetJoint(tvwJoints.SelectedNode)

        ' Find out which check box caused this event.
        ' Degree of freedom X.
        If sender Is chkJointsDegreeOfFreedomX Then _
   j.DegreeOfFreedom = New Vector3(value, j.DegreeOfFreedom.Y, j.DegreeOfFreedom.Z) _
        : Exit Sub

        ' Degree of freedom Y.
        If sender Is chkJointsDegreeOfFreedomY Then _
   j.DegreeOfFreedom = New Vector3(j.DegreeOfFreedom.X, value, j.DegreeOfFreedom.Z) _
        : Exit Sub

        ' Degree of freedom Z.
        If sender Is chkJointsDegreeOfFreedomZ Then _
   j.DegreeOfFreedom = New Vector3(j.DegreeOfFreedom.X, j.DegreeOfFreedom.Z, value) _
        : Exit Sub

    End Sub

    Private Sub tvwJoints_cmsMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tvwJoints_cmsMoveUp.Click
        ' Do nothing for root node.
        If tvwJoints.SelectedNode Is tvwJoints.Nodes(0) Then _
   Exit Sub

        ' Do nothing if it's already on top.
        If tvwJoints.SelectedNode.Index = 0 Then _
   Exit Sub

        ' Get it's index and parent.
        Dim index As Integer = tvwJoints.SelectedNode.Index
        Dim parent As TreeNode = tvwJoints.SelectedNode.Parent
        Dim node As TreeNode = tvwJoints.SelectedNode

        ' Get the joints.
        Dim jNode As Joint = _GetJoint(tvwJoints.SelectedNode)
        Dim jParent As Joint = jNode.Parent

        ' Pause updates to tree.
        tvwJoints.BeginUpdate()

        ' Remove item.
        parent.Nodes.RemoveAt(index)
        jParent.Children.RemoveAt(index)

        ' Insert at index - 1
        parent.Nodes.Insert(index - 1, node)
        jParent.Children.Insert(index - 1, jNode)
        tvwJoints.SelectedNode = node

        ' Update tree.
        tvwJoints.EndUpdate()

    End Sub

    Private Sub tvwJoints_cmsMoveDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvwJoints_cmsMoveDown.Click
        ' Do nothing for root node.
        If tvwJoints.SelectedNode Is tvwJoints.Nodes(0) Then _
   Exit Sub

        ' Do nothing if it's already at bottom.
        If tvwJoints.SelectedNode.Index = tvwJoints.SelectedNode.Parent.Nodes.Count - 1 Then _
   Exit Sub

        ' Get it's index and parent.
        Dim index As Integer = tvwJoints.SelectedNode.Index
        Dim parent As TreeNode = tvwJoints.SelectedNode.Parent
        Dim node As TreeNode = tvwJoints.SelectedNode

        ' Get the joints.
        Dim jNode As Joint = _GetJoint(tvwJoints.SelectedNode)
        Dim jParent As Joint = jNode.Parent

        ' Pause updates to tree.
        tvwJoints.BeginUpdate()

        ' Remove item.
        parent.Nodes.RemoveAt(index)
        jParent.Children.RemoveAt(index)

        ' Insert at index - 1
        parent.Nodes.Insert(index + 1, node)
        jParent.Children.Insert(index + 1, jNode)
        tvwJoints.SelectedNode = node

        ' Update tree.
        tvwJoints.EndUpdate()

    End Sub

#End Region

#Region " Collision Mesh UI "

    Private Sub _PrepareCollisionMeshAfterImport(ByVal m As CollisionMesh)
        ' Merge all parts.
        m.MergeAll()

        ' Remove redundant data from mesh.
        If m.PartCount <> 0 Then
            ' Convert to list type.
            m.Part(0).ConvertToList()

            ' Remove non triangle list primitive groups.
            For I As Integer = m.Part(0).PrimitiveGroupCount - 1 To 0 Step -1
                If m.Part(0).PrimitiveGroups(I).Type <> Direct3D.PrimitiveType.TriangleList Then _
     m.Part(0).RemovePrimitiveGroups(I)

            Next I ' For I As Integer = m.Part(0).PrimitiveGroupCount - 1 To 0 Step -1
        End If ' If m.PartCount <> 0 Then

    End Sub

    Private Sub tabCM_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabCM.Enter
        ' See if the tab enter event is marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        ' Update the combo box.
        With cboCMName.Items
            ' Get joints.
            Dim joints() As Joint = m_HOD.Root.ToArray()

            ' Clear old items.
            .Clear()

            ' Add all joints.
            For I As Integer = 0 To joints.Length - 1
                .Add(joints(I).Name)

            Next I ' For I As Integer = 0 To joints.Length - 1

            ' Erase array.
            Erase joints

        End With ' With cboCMName.Items

        ' Update the checked list box.
        With cstCM.Items
            ' Clear old items.
            .Clear()

            ' Add all meshes.
            For I As Integer = 0 To m_HOD.CollisionMeshes.Count - 1
                .Add(m_HOD.CollisionMeshes(I).ToString(), m_HOD.CollisionMeshVisible(I))

            Next I ' For I As Integer = 0 To m_HOD.CollisionMeshes.Count - 1

        End With ' With cstCM.Items

        ' Update selection.
        cstCM_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cstCM_Cut()
        ' See if any item is selected.
        If cstCM.SelectedIndex = -1 Then _
   Exit Sub

        ' Get it's index.
        Dim ind As Integer = cstCM.SelectedIndex

        ' Copy to clipboard.
        mnuEditClipboard = New CollisionMesh(m_HOD.CollisionMeshes(ind))

        ' Remove from HOD.
        m_HOD.CollisionMeshes.RemoveAt(ind)

        ' Refresh.
        cstCM.Items.RemoveAt(ind)

    End Sub

    Private Sub cstCM_Copy()
        ' See if any item is selected.
        If cstCM.SelectedIndex = -1 Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New CollisionMesh(m_HOD.CollisionMeshes(cstCM.SelectedIndex))

    End Sub

    Private Sub cstCM_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is CollisionMesh Then _
   Exit Sub

        ' Get the item.
        Dim cm As CollisionMesh = CType(mnuEditClipboard, CollisionMesh)

        ' Remove reference.
        mnuEditClipboard = Nothing

        ' Lock mesh.
        cm.Lock(m_D3DManager.Device)

        ' Add.
        m_HOD.CollisionMeshes.Add(cm)
        cstCM.Items.Add(cm.ToString(), m_HOD.CollisionMeshVisible(m_HOD.CollisionMeshes.Count - 1))

    End Sub

    Private Sub cstCM_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles cstCM.ItemCheck
        ' Set the visible flag if the checked list box is focused.
        If (sender IsNot Nothing) AndAlso (CType(sender, CheckedListBox).Focused) Then _
   m_HOD.CollisionMeshVisible(e.Index) = (e.NewValue = CheckState.Checked)

    End Sub

    Private Sub cstCM_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cstCM.SelectedIndexChanged
        ' Decide whether to enable or disable the UI.
        Dim enable As Boolean = (cstCM.SelectedIndex <> -1)

        ' Enable\Disable the UI depending upon whether an
        ' item has been selected or not.
        cmdCMRemove.Enabled = enable
        cmdCMImport.Enabled = enable
        cmdCMExport.Enabled = enable
        cmdCMRecalc.Enabled = enable
        cboCMName.Enabled = enable
        txtCMMinX.Enabled = enable
        txtCMMinY.Enabled = enable
        txtCMMinZ.Enabled = enable
        txtCMMaxX.Enabled = enable
        txtCMMaxY.Enabled = enable
        txtCMMaxZ.Enabled = enable
        txtCMCX.Enabled = enable
        txtCMCY.Enabled = enable
        txtCMCZ.Enabled = enable
        txtCMRadius.Enabled = enable

        If cstCM.SelectedIndex = -1 Then
            ' Update combo box.
            cboCMName.SelectedIndex = -1

            ' Update text boxes.
            txtCMMinX.Text = ""
            txtCMMinY.Text = ""
            txtCMMinZ.Text = ""
            txtCMMaxX.Text = ""
            txtCMMaxY.Text = ""
            txtCMMaxZ.Text = ""
            txtCMCX.Text = ""
            txtCMCY.Text = ""
            txtCMCZ.Text = ""
            txtCMRadius.Text = ""

        Else ' If cstCM.SelectedIndex = -1 Then
            ' Check if the parent doesn't exist.
            If Not _ComboBoxHasString(cboCMName.Items, m_HOD.CollisionMeshes(cstCM.SelectedIndex).Name) Then _
    m_HOD.CollisionMeshes(cstCM.SelectedIndex).Name = "Root" _
            : MsgBox("The joint specified by this collision mesh does not exist!" & vbCrLf &
           "It has been changed to 'Root'.", MsgBoxStyle.Information, Me.Text)

            With m_HOD.CollisionMeshes(cstCM.SelectedIndex)
                ' Update combo box.
                cboCMName.SelectedItem = .Name

                ' Update text boxes.
                txtCMMinX.Text = CStr(.MinimumExtents.X)
                txtCMMinY.Text = CStr(.MinimumExtents.Y)
                txtCMMinZ.Text = CStr(.MinimumExtents.Z)
                txtCMMaxX.Text = CStr(.MaximumExtents.X)
                txtCMMaxY.Text = CStr(.MaximumExtents.Y)
                txtCMMaxZ.Text = CStr(.MaximumExtents.Z)
                txtCMCX.Text = CStr(.Center.X)
                txtCMCY.Text = CStr(.Center.Y)
                txtCMCZ.Text = CStr(.Center.Z)
                txtCMRadius.Text = CStr(.Radius)

            End With ' With m_HOD.CollisionMeshes(cstCM.SelectedIndex)
        End If ' If cstCM.SelectedIndex = -1 Then

    End Sub

    Private Sub cmdCMAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCMAdd.Click
        Dim cm As New CollisionMesh With {.Name = "Root"}

        ' Add to HOD.
        m_HOD.CollisionMeshes.Add(cm)

        ' Update list box.
        cstCM.Items.Add(cm.ToString(), m_HOD.CollisionMeshVisible(m_HOD.CollisionMeshes.Count - 1))
        cstCM.SelectedIndex = cstCM.Items.Count - 1

    End Sub

    Private Sub cmdCMRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCMRemove.Click
        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Remove the mesh.
        m_HOD.CollisionMeshes.RemoveAt(cstCM.SelectedIndex)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Update list.
        cstCM.Items.RemoveAt(cstCM.SelectedIndex)
        cstCM.SelectedIndex = cstCM.Items.Count - 1

    End Sub

    Private Sub cmdCMImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCMImport.Click
        ' See if user pressed cancel.
        If OpenOBJFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Initialize a new IOResult form.
        Dim f As New IOResult

        ' Create a new mesh to read.
        Dim m As New CollisionMesh

        ' Try to read the wavefront object file.
        Try
            ' Read the wavefront object file.
            GenericMesh.Translators.WavefrontObject.ReadFile(OpenOBJFileDialog.FileName, m)

            ' Validate the mesh.
            m.Validate()

        Catch ex As Exception
            ' Log the error.
            Trace.TraceError(ex.ToString)

            ' Clear the temp mesh.
            m.RemoveAll()
            m = Nothing

        End Try

        ' If the read succeeded...
        If m IsNot Nothing Then
            ' Prepare mesh.
            _PrepareCollisionMeshAfterImport(m)

            ' Pause render.
            m_D3DManager.RenderLoopPause()

            ' Unlock mesh.
            m_HOD.CollisionMeshes(cstCM.SelectedIndex).Unlock()

            ' Update mesh.
            m.CopyTo(m_HOD.CollisionMeshes(cstCM.SelectedIndex))
            m_HOD.CollisionMeshes(cstCM.SelectedIndex).CalculateExtents()

            ' Lock mesh.
            m_HOD.CollisionMeshes(cstCM.SelectedIndex).Lock(m_D3DManager.Device)

            ' Resume render.
            m_D3DManager.RenderLoopResume()

            ' Remove the read mesh.
            m.RemoveAll()
            m = Nothing

        End If ' If m IsNot Nothing Then

        ' Display the result.
        f.Show()

        ' Update selection.
        cstCM_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cmdCMExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCMExport.Click
        ' See if user pressed cancel.
        If SaveOBJFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Initialize a new IOResult form.
        Dim f As New IOResult

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Unlock mesh.
        m_HOD.CollisionMeshes(cstCM.SelectedIndex).Unlock()

        ' Try to write the wavefront object file.
        Try
            GenericMesh.Translators.WavefrontObject.WriteFile(
    SaveOBJFileDialog.FileName,
    m_HOD.CollisionMeshes(cstCM.SelectedIndex),
    False, False
   )

        Catch ex As Exception
            Trace.TraceError(ex.ToString)

        End Try

        ' Lock mesh.
        m_HOD.CollisionMeshes(cstCM.SelectedIndex).Lock(m_D3DManager.Device)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Display the result.
        f.Show()

    End Sub

    Private Sub cmdCMRecalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCMRecalc.Click
        ' Re-calculate extents.
        m_HOD.CollisionMeshes(cstCM.SelectedIndex).CalculateExtents()

        ' Update selection.
        cstCM_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cboCMName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCMName.SelectedIndexChanged
        ' Set the parent name if the combo box is focused.
        If CType(sender, ComboBox).Focused Then _
   m_HOD.CollisionMeshes(cstCM.SelectedIndex).Name = CStr(cboCMName.SelectedItem) _
        : cstCM.Items(cstCM.SelectedIndex) = cboCMName.SelectedItem

    End Sub

    Private Sub txtCM_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtCMMinX.TextChanged, txtCMMinY.TextChanged, txtCMMinZ.TextChanged,
         txtCMMaxX.TextChanged, txtCMMaxY.TextChanged, txtCMMaxZ.TextChanged,
         txtCMCX.TextChanged, txtCMCY.TextChanged, txtCMCZ.TextChanged,
         txtCMRadius.TextChanged

        ' Get the text box.
        Dim textBox As TextBox = CType(sender, TextBox)

        ' See if it's focused.
        If Not textBox.Focused Then _
   Exit Sub

        ' See if it has valid, numeric data.
        If (textBox.Text = "") OrElse (Not IsNumeric(textBox.Text)) Then _
   Exit Sub

        ' Get the collision mesh.
        Dim cm As CollisionMesh = m_HOD.CollisionMeshes(cstCM.SelectedIndex)

        ' Get the number.
        Dim number As Single = CSng(textBox.Text)

        ' Set data.
        If sender Is txtCMMinX Then _
   cm.MinimumExtents = New Vector3(number, cm.MinimumExtents.Y, cm.MinimumExtents.Z)

        If sender Is txtCMMinY Then _
   cm.MinimumExtents = New Vector3(cm.MinimumExtents.X, number, cm.MinimumExtents.Z)

        If sender Is txtCMMinZ Then _
   cm.MinimumExtents = New Vector3(cm.MinimumExtents.X, cm.MinimumExtents.Y, number)

        If sender Is txtCMMaxX Then _
   cm.MaximumExtents = New Vector3(number, cm.MaximumExtents.Y, cm.MaximumExtents.Z)

        If sender Is txtCMMaxY Then _
   cm.MaximumExtents = New Vector3(cm.MaximumExtents.X, number, cm.MaximumExtents.Z)

        If sender Is txtCMMaxZ Then _
   cm.MaximumExtents = New Vector3(cm.MaximumExtents.X, cm.MaximumExtents.Y, number)

        If sender Is txtCMCX Then _
   cm.Center = New Vector3(number, cm.Center.Y, cm.Center.Z)

        If sender Is txtCMCY Then _
   cm.Center = New Vector3(cm.Center.X, number, cm.Center.Z)

        If sender Is txtCMCZ Then _
   cm.Center = New Vector3(cm.Center.X, cm.Center.Y, number)

        If (sender Is txtCMRadius) AndAlso (number > 0) Then _
   cm.Radius = number

    End Sub

    Private Sub txtCM_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtCMMinX.Validated, txtCMMinY.Validated, txtCMMinZ.Validated,
         txtCMMaxX.Validated, txtCMMaxY.Validated, txtCMMaxZ.Validated,
         txtCMCX.Validated, txtCMCY.Validated, txtCMCZ.Validated,
         txtCMRadius.Validated

        ' Update error to no error.
        ErrorProvider.SetError(CType(sender, Control), "")

    End Sub

#End Region

#Region " Engine Shapes UI "

    Private Sub _PrepareEngineShapeAfterImport(ByVal es As EngineShape)
        ' Merge all parts.
        es.MergeAll()

        ' If no part, do nothing.
        If es.PartCount = 0 Then _
   Exit Sub

        ' Remove un-needed primitive groups.
        For I As Integer = es.Part(0).PrimitiveGroupCount - 1 To 0 Step -1
            If (es.Part(0).PrimitiveGroups(I).Type <> Direct3D.PrimitiveType.TriangleList) AndAlso
      (es.Part(0).PrimitiveGroups(I).Type <> Direct3D.PrimitiveType.TriangleFan) Then _
    es.Part(0).RemovePrimitiveGroups(I)

        Next I ' For I As Integer = es.Part(0).PrimitiveGroupCount - 1 To 0 Step -1

    End Sub

    Private Sub tabEngineShapes_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabEngineShapes.Enter
        ' See if the tab enter event is marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        ' Update the combo box.
        With cboEngineShapesParent.Items
            ' Get joints.
            Dim joints() As Joint = m_HOD.Root.ToArray()

            ' Clear old items.
            .Clear()

            ' Add all joints.
            For I As Integer = 0 To joints.Length - 1
                .Add(joints(I).Name)

            Next I ' For I As Integer = 0 To joints.Length - 1

            ' Erase array.
            Erase joints

        End With ' With cboEngineShapesParent.Items

        ' Update the checked list box.
        With cstEngineShapes.Items
            ' Clear old items.
            .Clear()

            ' Add all meshes.
            For I As Integer = 0 To m_HOD.EngineShapes.Count - 1
                .Add(m_HOD.EngineShapes(I).ToString(), m_HOD.EngineShapeVisible(I))

            Next I ' For I As Integer = 0 To m_HOD.EngineShapes.Count - 1

        End With ' With cstEngineShapes.Items

        ' Update selection.
        cstEngineShapes_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cstEngineShapes_Cut()
        ' See if any item is selected.
        If cstEngineShapes.SelectedIndex = -1 Then _
   Exit Sub

        ' Get it's index.
        Dim ind As Integer = cstEngineShapes.SelectedIndex

        ' Copy to clipboard.
        mnuEditClipboard = New EngineShape(m_HOD.EngineShapes(ind))

        ' Remove from HOD.
        m_HOD.EngineShapes.RemoveAt(ind)

        ' Refresh.
        cstEngineShapes.Items.RemoveAt(ind)

    End Sub

    Private Sub cstEngineShapes_Copy()
        ' See if any item is selected.
        If cstEngineShapes.SelectedIndex = -1 Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New EngineShape(m_HOD.EngineShapes(cstEngineShapes.SelectedIndex))

    End Sub

    Private Sub cstEngineShapes_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is EngineShape Then _
   Exit Sub

        ' Get the item.
        Dim es As EngineShape = CType(mnuEditClipboard, EngineShape)

        ' Remove reference.
        mnuEditClipboard = Nothing

        ' Lock mesh.
        es.Lock(m_D3DManager.Device)

        ' See if it's already present.
        If Not cstEngineShapes.Items.Contains(es.ToString()) Then _
   m_HOD.EngineShapes.Add(es) _
        : cstEngineShapes.Items.Add(es.ToString(), m_HOD.EngineShapeVisible(cstEngineShapes.Items.Count)) _
        : Exit Sub

        ' Rename item.
        Dim number As Integer = 2

        Do
            ' See if this name is present.
            If cstEngineShapes.Items.Contains(es.ToString() & CStr(number)) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, add it.
            es.Name &= CStr(number)
            m_HOD.EngineShapes.Add(es)
            cstEngineShapes.Items.Add(es.ToString(), m_HOD.EngineShapeVisible(cstEngineShapes.Items.Count))

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cstEngineShapes_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles cstEngineShapes.ItemCheck
        ' Set the visible flag if the checked list box is focused.
        If (sender IsNot Nothing) AndAlso (CType(sender, CheckedListBox).Focused) Then _
   m_HOD.EngineShapeVisible(e.Index) = (e.NewValue = CheckState.Checked)

    End Sub

    Private Sub cstEngineShapes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cstEngineShapes.SelectedIndexChanged
        If cstEngineShapes.SelectedIndex = -1 Then
            ' Since no mesh is selected, disable all UI controls,
            ' except the add button.
            cmdEngineShapesRemove.Enabled = False
            cmdEngineShapesRename.Enabled = False
            cmdEngineShapesImport.Enabled = False
            cmdEngineShapesExport.Enabled = False
            cboEngineShapesParent.Enabled = False
            cboEngineShapesParent.SelectedIndex = -1

        Else ' If cstEngineShapes.SelectedIndex = -1 Then
            ' Since a mesh is selected, enable all UI controls.
            cmdEngineShapesRemove.Enabled = True
            cmdEngineShapesRename.Enabled = True
            cmdEngineShapesImport.Enabled = True
            cmdEngineShapesExport.Enabled = True
            cboEngineShapesParent.Enabled = True

            ' Check if the parent doesn't exist.
            If Not _ComboBoxHasString(cboEngineShapesParent.Items, m_HOD.EngineShapes(cstEngineShapes.SelectedIndex).ParentName) Then _
    m_HOD.EngineShapes(cstEngineShapes.SelectedIndex).ParentName = "Root" _
            : MsgBox("The parent joint specified by this engine shape does not exist!" & vbCrLf &
           "It has been changed to 'Root'.", MsgBoxStyle.Information, Me.Text)

            ' Update combo box.
            cboEngineShapesParent.SelectedItem = m_HOD.EngineShapes(cstEngineShapes.SelectedIndex).ParentName

        End If ' If cstEngineShapes.SelectedIndex = -1 Then

    End Sub

    Private Sub cmdEngineShapesAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEngineShapesAdd.Click
        Dim number As Integer = cstEngineShapes.Items.Count + 1
        Dim name As String

        Do
            ' Decide a name for the mesh.
            name = "EngineShape" & CStr(number)

            ' See if it's a duplicate.
            If _ListBoxHasString(cstEngineShapes.Items, name) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, make a new mesh.
            Dim es As New EngineShape With {
    .Name = name,
    .ParentName = "Root"
   }

            ' Add it.
            m_HOD.EngineShapes.Add(es)

            ' Add the mesh to the list.
            cstEngineShapes.Items.Add(es.ToString(), m_HOD.EngineShapeVisible(cstEngineShapes.Items.Count))
            cstEngineShapes.SelectedIndex = cstEngineShapes.Items.Count - 1

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cmdEngineShapesRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEngineShapesRemove.Click
        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Remove the mesh.
        m_HOD.EngineShapes.RemoveAt(cstEngineShapes.SelectedIndex)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Update list.
        cstEngineShapes.Items.RemoveAt(cstEngineShapes.SelectedIndex)
        cstEngineShapes.SelectedIndex = cstEngineShapes.Items.Count - 1

    End Sub

    Private Sub cmdEngineShapesRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEngineShapesRename.Click
        Dim name As String

        ' Get old name.
        Dim oldName As String = m_HOD.EngineShapes(cstEngineShapes.SelectedIndex).Name

        Do
            ' Get new name.
            name = InputBox("Enter new name for mesh: ", Me.Text, oldName)

            ' See if the user pressed cancel.
            If name = "" Then _
    Exit Sub

            ' HACK: If only casing is changed, then rename list item twice.
            ' Otherwise, the change doesn't seem to be reflected in the list.
            If String.Compare(oldName, name, True) = 0 Then _
    cstEngineShapes.Items(cstEngineShapes.SelectedIndex) = "" _
            : Exit Do

            ' See if it's a duplicate.
            ' For that, first rename the mesh (in list only) to something else
            ' so that it doesn't interfere in our search.
            cstEngineShapes.Items(cstEngineShapes.SelectedIndex) = ""

            ' Now see if it's a duplicate.
            If _ListBoxHasString(cstEngineShapes.Items, name) Then _
    cstEngineShapes.Items(cstEngineShapes.SelectedIndex) = oldName _
            : MsgBox("The name you entered is a duplicate!" & vbCrLf &
           "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
            : Continue Do

            ' Exit loop.
            Exit Do

        Loop ' Do

        ' Not a duplicate. OK to rename.
        m_HOD.EngineShapes(cstEngineShapes.SelectedIndex).Name = name

        ' Update list.
        cstEngineShapes.Items(cstEngineShapes.SelectedIndex) = name

        ' Update UI
        cstEngineShapes_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cmdEngineShapesImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEngineShapesImport.Click
        ' See if user pressed cancel.
        If OpenOBJFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Initialize a new IOResult form.
        Dim f As New IOResult

        ' Create a new mesh to read.
        Dim es As New EngineShape

        ' Try to read the wavefront object file.
        Try
            ' Read the wavefront object file.
            GenericMesh.Translators.WavefrontObject.ReadFile(OpenOBJFileDialog.FileName, es)

            ' Validate the mesh.
            es.Validate()

        Catch ex As Exception
            ' Log the error.
            Trace.TraceError(ex.ToString)

            ' Clear the temp mesh.
            es.RemoveAll()
            es = Nothing

        End Try

        ' If the read succeeded...
        If es IsNot Nothing Then
            ' Prepare mesh.
            _PrepareEngineShapeAfterImport(es)

            ' Pause render.
            m_D3DManager.RenderLoopPause()

            ' Unlock mesh.
            m_HOD.EngineShapes(cstEngineShapes.SelectedIndex).Unlock()

            ' Update mesh.
            es.CopyTo(m_HOD.EngineShapes(cstEngineShapes.SelectedIndex))

            ' Lock mesh.
            m_HOD.EngineShapes(cstEngineShapes.SelectedIndex).Lock(m_D3DManager.Device)

            ' Resume render.
            m_D3DManager.RenderLoopResume()

            ' Remove the read mesh.
            es.RemoveAll()
            es = Nothing

        End If ' If m IsNot Nothing Then

        ' Display the result.
        f.Show()

    End Sub

    Private Sub cmdEngineShapesExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEngineShapesExport.Click
        ' See if user pressed cancel.
        If SaveOBJFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Initialize a new IOResult form.
        Dim f As New IOResult

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Unlock mesh.
        m_HOD.EngineShapes(cstEngineShapes.SelectedIndex).Unlock()

        ' Try to write the wavefront object file.
        Try
            GenericMesh.Translators.WavefrontObject.WriteFile(
    SaveOBJFileDialog.FileName,
    m_HOD.EngineShapes(cstEngineShapes.SelectedIndex),
    False, False
   )

        Catch ex As Exception
            Trace.TraceError(ex.ToString)

        End Try

        ' Lock mesh.
        m_HOD.EngineShapes(cstEngineShapes.SelectedIndex).Lock(m_D3DManager.Device)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Display the result.
        f.Show()

    End Sub

    Private Sub cboEngineShapesParent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboEngineShapesParent.SelectedIndexChanged
        ' Set the parent name if the combo box is focused.
        If CType(sender, ComboBox).Focused Then _
   m_HOD.EngineShapes(cstEngineShapes.SelectedIndex).ParentName = CStr(cboEngineShapesParent.SelectedItem)

    End Sub

#End Region

#Region " Engine Glows UI "

    Private Sub tabEngineGlows_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabEngineGlows.Enter
        ' See if the tab enter event is marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        ' Update the combo box.
        With cboEngineGlowsParent.Items
            ' Get joints.
            Dim joints() As Joint = m_HOD.Root.ToArray()

            ' Clear old items.
            .Clear()

            ' Add all joints.
            For I As Integer = 0 To joints.Length - 1
                .Add(joints(I).Name)

            Next I ' For I As Integer = 0 To joints.Length - 1

            ' Erase array.
            Erase joints

        End With ' With cboEngineGlowsParent.Items

        ' Update the checked list box.
        With cstEngineGlows.Items
            ' Clear old items.
            .Clear()

            ' Add all meshes.
            For I As Integer = 0 To m_HOD.EngineGlows.Count - 1
                .Add(m_HOD.EngineGlows(I).ToString(), m_HOD.EngineGlows(I).Visible)

            Next I ' For I As Integer = 0 To m_HOD.EngineGlows.Count - 1

        End With ' With cstEngineGlows.Items

        ' Update selection.
        cstEngineGlows_SelectedIndexChanged(Nothing, EventArgs.Empty)

        ' Update slider.
        sldEngineGlowsThrusterPowerFactor.Value = CInt(100 * m_HOD.ThrusterPower)

    End Sub

    Private Sub cstEngineGlows_Cut()
        ' See if any item is selected.
        If cstEngineGlows.SelectedIndex = -1 Then _
   Exit Sub

        ' Get it's index.
        Dim ind As Integer = cstEngineGlows.SelectedIndex

        ' Copy to clipboard.
        mnuEditClipboard = New EngineGlow(m_HOD.EngineGlows(ind))

        ' Remove from HOD.
        m_HOD.EngineGlows.RemoveAt(ind)

        ' Refresh.
        cstEngineGlows.Items.RemoveAt(ind)

    End Sub

    Private Sub cstEngineGlows_Copy()
        ' See if any item is selected.
        If cstEngineGlows.SelectedIndex = -1 Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New EngineGlow(m_HOD.EngineGlows(cstEngineGlows.SelectedIndex))

    End Sub

    Private Sub cstEngineGlows_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is EngineGlow Then _
   Exit Sub

        ' Get the item.
        Dim gm As EngineGlow = CType(mnuEditClipboard, EngineGlow)

        ' Remove reference.
        mnuEditClipboard = Nothing

        ' Lock mesh.
        gm.Mesh.Lock(m_D3DManager.Device)

        ' See if it's already present.
        If Not cstEngineGlows.Items.Contains(gm.ToString()) Then _
   m_HOD.EngineGlows.Add(gm) _
        : cstEngineGlows.Items.Add(gm.ToString(), gm.Visible) _
        : Exit Sub

        ' Rename item.
        Dim number As Integer = 2

        Do
            ' See if this name is present.
            If cstEngineGlows.Items.Contains(gm.ToString() & CStr(number)) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, add it.
            gm.Name &= CStr(number)
            m_HOD.EngineGlows.Add(gm)
            cstEngineGlows.Items.Add(gm.ToString(), gm.Visible)

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cstEngineGlows_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles cstEngineGlows.ItemCheck
        ' Set the visible flag if the checked list box is focused.
        If (sender IsNot Nothing) AndAlso (CType(sender, CheckedListBox).Focused) Then _
   m_HOD.EngineGlows(e.Index).Visible = (e.NewValue = CheckState.Checked)

    End Sub

    Private Sub cstEngineGlows_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cstEngineGlows.SelectedIndexChanged
        If cstEngineGlows.SelectedIndex = -1 Then
            ' Since no mesh is selected, disable all UI controls,
            ' except the add button.
            cmdEngineGlowsRemove.Enabled = False
            cmdEngineGlowsRename.Enabled = False
            cmdEngineGlowsImport.Enabled = False
            cmdEngineGlowsExport.Enabled = False
            cboEngineGlowsParent.Enabled = False
            txtEngineGlowsLOD.Enabled = False
            cboEngineGlowsParent.SelectedIndex = -1
            txtEngineGlowsLOD.Text = ""

        Else ' If cstEngineGlows.SelectedIndex = -1 Then
            ' Since a mesh is selected, enable all UI controls.
            cmdEngineGlowsRemove.Enabled = True
            cmdEngineGlowsRename.Enabled = True
            cmdEngineGlowsImport.Enabled = True
            cmdEngineGlowsExport.Enabled = True
            cboEngineGlowsParent.Enabled = True
            txtEngineGlowsLOD.Enabled = True

            ' Update text box.
            txtEngineGlowsLOD.Text = CStr(m_HOD.EngineGlows(cstEngineGlows.SelectedIndex).LOD)

            ' Check if the parent doesn't exist.
            If Not _ComboBoxHasString(cboEngineGlowsParent.Items, m_HOD.EngineGlows(cstEngineGlows.SelectedIndex).ParentName) Then _
    m_HOD.EngineGlows(cstEngineGlows.SelectedIndex).ParentName = "Root" _
            : MsgBox("The parent joint specified by this engine glow does not exist!" & vbCrLf &
           "It has been changed to 'Root'.", MsgBoxStyle.Information, Me.Text)

            ' Update combo box.
            cboEngineGlowsParent.SelectedItem = m_HOD.EngineGlows(cstEngineGlows.SelectedIndex).ParentName

        End If ' If cstEngineGlows.SelectedIndex = -1 Then

    End Sub

    Private Sub cmdEngineGlowsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEngineGlowsAdd.Click
        Dim number As Integer = cstEngineGlows.Items.Count + 1
        Dim name As String

        Do
            ' Decide a name for the mesh.
            name = "EngineGlow" & CStr(number)

            ' See if it's a duplicate.
            If _ListBoxHasString(cstEngineGlows.Items, name) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, make a new mesh.
            Dim g As New EngineGlow With {
    .Name = name,
    .ParentName = "Root"
   }

            ' Add it.
            m_HOD.EngineGlows.Add(g)

            ' Add the mesh to the list.
            cstEngineGlows.Items.Add(g.ToString(), g.Visible)
            cstEngineGlows.SelectedIndex = cstEngineGlows.Items.Count - 1

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cmdEngineGlowsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEngineGlowsRemove.Click
        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Remove the mesh.
        m_HOD.EngineGlows.RemoveAt(cstEngineGlows.SelectedIndex)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Update list.
        cstEngineGlows.Items.RemoveAt(cstEngineGlows.SelectedIndex)
        cstEngineGlows.SelectedIndex = cstEngineGlows.Items.Count - 1

    End Sub

    Private Sub cmdEngineGlowsRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEngineGlowsRename.Click
        Dim name As String

        ' Get old name.
        Dim oldName As String = m_HOD.EngineGlows(cstEngineGlows.SelectedIndex).Name

        Do
            ' Get new name.
            name = InputBox("Enter new name for mesh: ", Me.Text, oldName)

            ' See if the user pressed cancel.
            If name = "" Then _
    Exit Sub

            ' HACK: If only casing is changed, then rename list item twice.
            ' Otherwise, the change doesn't seem to be reflected in the list.
            If String.Compare(oldName, name, True) = 0 Then _
    cstEngineGlows.Items(cstEngineGlows.SelectedIndex) = "" _
            : Exit Do

            ' See if it's a duplicate.
            ' For that, first rename the mesh (in list only) to something else
            ' so that it doesn't interfere in our search.
            cstEngineGlows.Items(cstEngineGlows.SelectedIndex) = ""

            ' Now see if it's a duplicate.
            If _ListBoxHasString(cstEngineGlows.Items, name) Then _
    cstEngineGlows.Items(cstEngineGlows.SelectedIndex) = oldName _
            : MsgBox("The name you entered is a duplicate!" & vbCrLf &
           "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
            : Continue Do

            ' Exit loop.
            Exit Do

        Loop ' Do

        ' Not a duplicate. OK to rename.
        m_HOD.EngineGlows(cstEngineGlows.SelectedIndex).Name = name

        ' Update list.
        cstEngineGlows.Items(cstEngineGlows.SelectedIndex) = name

        ' Update UI
        cstEngineGlows_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cmdEngineGlowsImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEngineGlowsImport.Click
        ' See if user pressed cancel.
        If OpenOBJFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Initialize a new IOResult form.
        Dim f As New IOResult

        ' Create a new mesh to read.
        Dim m As New BasicMesh

        ' Ask whether to reverse U of UVs.
        Dim reverseUVs As Boolean = (MsgBox("Reverse UVs?", MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes)

        ' Try to read the wavefront object file.
        Try
            ' Read the wavefront object file.
            GenericMesh.Translators.WavefrontObject.ReadFile(OpenOBJFileDialog.FileName, m, reverseUVs)

            ' Validate the mesh.
            m.Validate()

        Catch ex As Exception
            ' Log the error.
            Trace.TraceError(ex.ToString)

            ' Clear the temp mesh.
            m.RemoveAll()
            m = Nothing

        End Try

        ' If the read succeeded...
        If m IsNot Nothing Then
            ' Pause render.
            m_D3DManager.RenderLoopPause()

            ' Unlock mesh.
            m_HOD.EngineGlows(cstEngineGlows.SelectedIndex).Mesh.Unlock()

            ' Update mesh.
            m.CopyTo(m_HOD.EngineGlows(cstEngineGlows.SelectedIndex).Mesh)
            _UnPrepareBasicMeshForExport(m_HOD.EngineGlows(cstEngineGlows.SelectedIndex).Mesh)
            _SetBasicMeshVertexMasks(m_HOD.EngineGlows(cstEngineGlows.SelectedIndex).Mesh,
                            VertexMasks.Position Or VertexMasks.Normal Or VertexMasks.Texture0)

            ' Lock mesh.
            m_HOD.EngineGlows(cstEngineGlows.SelectedIndex).Mesh.Lock(m_D3DManager.Device)

            ' Resume render.
            m_D3DManager.RenderLoopResume()

            ' Remove the read mesh.
            m.RemoveAll()
            m = Nothing

        End If ' If m IsNot Nothing Then

        ' Display the result.
        f.Show()

    End Sub

    Private Sub cmdEngineGlowsExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEngineGlowsExport.Click
        ' See if user pressed cancel.
        If SaveOBJFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Initialize a new IOResult form.
        Dim f As New IOResult

        ' Ask whether to reverse U of UVs.
        Dim reverseUVs As Boolean = (MsgBox("Reverse UVs?", MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes)

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Unlock mesh.
        m_HOD.EngineGlows(cstEngineGlows.SelectedIndex).Mesh.Unlock()

        ' Update a few material properties...
        _UnPrepareBasicMeshForExport(m_HOD.EngineGlows(cstEngineGlows.SelectedIndex).Mesh)

        ' Try to write the wavefront object file.
        Try
            GenericMesh.Translators.WavefrontObject.WriteFile(
    SaveOBJFileDialog.FileName,
    m_HOD.EngineGlows(cstEngineGlows.SelectedIndex).Mesh,
    ReverseUVs:=reverseUVs
   )

        Catch ex As Exception
            Trace.TraceError(ex.ToString)

        End Try

        ' Lock mesh.
        m_HOD.EngineGlows(cstEngineGlows.SelectedIndex).Mesh.Lock(m_D3DManager.Device)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Display the result.
        f.Show()

    End Sub

    Private Sub cboEngineGlowsParent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboEngineGlowsParent.SelectedIndexChanged
        ' Set the parent name if the combo box is focused.
        If CType(sender, ComboBox).Focused Then _
   m_HOD.EngineGlows(cstEngineGlows.SelectedIndex).ParentName = CStr(cboEngineGlowsParent.SelectedItem)

    End Sub

    Private Sub txtEngineGlowsLOD_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEngineGlowsLOD.Validated
        If cstEngineGlows.SelectedIndex = -1 Then _
   Exit Sub

        ' Set LOD
        m_HOD.EngineGlows(cstEngineGlows.SelectedIndex).LOD = Integer.Parse(txtEngineGlowsLOD.Text)

        ' Remove error.
        ErrorProvider.SetError(txtEngineGlowsLOD, "")

    End Sub

    Private Sub sldEngineGlowsThrusterPowerFactor_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sldEngineGlowsThrusterPowerFactor.ValueChanged
        ' Set the value if slider is focused.
        If sldEngineGlowsThrusterPowerFactor.Focused Then _
   m_HOD.ThrusterPower = sldEngineGlowsThrusterPowerFactor.Value / 100.0F

    End Sub

#End Region

#Region " Engine Burns UI "

    Private Sub tabEngineBurns_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabEngineBurns.Enter
        ' See if the tab enter event is marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        ' Update the combo box.
        With cboEngineBurnsParent.Items
            ' Get joints.
            Dim joints() As Joint = m_HOD.Root.ToArray()

            ' Clear old items.
            .Clear()

            ' Add all joints.
            For I As Integer = 0 To joints.Length - 1
                .Add(joints(I).Name)

            Next I ' For I As Integer = 0 To joints.Length - 1

            ' Erase array.
            Erase joints

        End With ' With cboEngineBurnsParent.Items

        ' Update the checked list box.
        With cstEngineBurns.Items
            ' Clear old items.
            .Clear()

            ' Add all meshes.
            For I As Integer = 0 To m_HOD.EngineBurns.Count - 1
                .Add(m_HOD.EngineBurns(I).ToString(), m_HOD.EngineBurns(I).Visible)

            Next I ' For I As Integer = 0 To m_HOD.EngineBurns.Count - 1

        End With ' With cstEngineBurns.Items

        ' Update selection.
        cstEngineBurns_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cstEngineBurns_Cut()
        ' See if any item is selected.
        If cstEngineBurns.SelectedIndex = -1 Then _
   Exit Sub

        ' Get it's index.
        Dim ind As Integer = cstEngineBurns.SelectedIndex

        ' Copy to clipboard.
        mnuEditClipboard = New EngineBurn(m_HOD.EngineBurns(ind))

        ' Remove from HOD.
        m_HOD.EngineBurns.RemoveAt(ind)

        ' Refresh.
        cstEngineBurns.Items.RemoveAt(ind)

    End Sub

    Private Sub cstEngineBurns_Copy()
        ' See if any item is selected.
        If cstEngineBurns.SelectedIndex = -1 Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New EngineBurn(m_HOD.EngineBurns(cstEngineBurns.SelectedIndex))

    End Sub

    Private Sub cstEngineBurns_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is EngineBurn Then _
   Exit Sub

        ' Get the item.
        Dim eb As EngineBurn = CType(mnuEditClipboard, EngineBurn)

        ' Remove reference.
        mnuEditClipboard = Nothing

        ' See if it's already present.
        If Not cstEngineBurns.Items.Contains(eb.ToString()) Then _
   m_HOD.EngineBurns.Add(eb) _
        : cstEngineBurns.Items.Add(eb.ToString(), eb.Visible) _
        : Exit Sub

        ' Rename item.
        Dim number As Integer = 2

        Do
            ' See if this name is present.
            If cstEngineBurns.Items.Contains(eb.ToString() & CStr(number)) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, add it.
            eb.Name &= CStr(number)
            m_HOD.EngineBurns.Add(eb)
            cstEngineBurns.Items.Add(eb.ToString(), eb.Visible)

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cstEngineBurns_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles cstEngineBurns.ItemCheck
        ' Set the visible flag if the checked list box is focused.
        If (sender IsNot Nothing) AndAlso (CType(sender, CheckedListBox).Focused) Then _
   m_HOD.EngineBurns(e.Index).Visible = (e.NewValue = CheckState.Checked)

    End Sub

    Private Sub cstEngineBurns_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cstEngineBurns.SelectedIndexChanged
        Dim enabled As Boolean = (cstEngineBurns.SelectedIndex <> -1)

        ' Depending on whether a mesh is selected or not, 
        ' enable\disable controls.
        cmdEngineBurnsRemove.Enabled = enabled
        cmdEngineBurnsRename.Enabled = enabled
        cboEngineBurnsParent.Enabled = enabled

        txtEngineBurn1X.Enabled = enabled
        txtEngineBurn1Y.Enabled = enabled
        txtEngineBurn1Z.Enabled = enabled
        txtEngineBurn2X.Enabled = enabled
        txtEngineBurn2Y.Enabled = enabled
        txtEngineBurn2Z.Enabled = enabled
        txtEngineBurn3X.Enabled = enabled
        txtEngineBurn3Y.Enabled = enabled
        txtEngineBurn3Z.Enabled = enabled
        txtEngineBurn4X.Enabled = enabled
        txtEngineBurn4Y.Enabled = enabled
        txtEngineBurn4Z.Enabled = enabled
        txtEngineBurn5X.Enabled = enabled
        txtEngineBurn5Y.Enabled = enabled
        txtEngineBurn5Z.Enabled = enabled

        If cstEngineBurns.SelectedIndex = -1 Then
            ' Since no mesh is selected, clear all UI controls.
            txtEngineBurn1X.Text = ""
            txtEngineBurn1Y.Text = ""
            txtEngineBurn1Z.Text = ""
            txtEngineBurn2X.Text = ""
            txtEngineBurn2Y.Text = ""
            txtEngineBurn2Z.Text = ""
            txtEngineBurn3X.Text = ""
            txtEngineBurn3Y.Text = ""
            txtEngineBurn3Z.Text = ""
            txtEngineBurn4X.Text = ""
            txtEngineBurn4Y.Text = ""
            txtEngineBurn4Z.Text = ""
            txtEngineBurn5X.Text = ""
            txtEngineBurn5Y.Text = ""
            txtEngineBurn5Z.Text = ""

            cboEngineBurnsParent.SelectedIndex = -1

        Else ' If cstEngineBurns.SelectedIndex = -1 Then
            ' Since a mesh is selected, update all UI controls.
            With m_HOD.EngineBurns(cstEngineBurns.SelectedIndex)
                txtEngineBurn1X.Text = CStr(.Vertices(0).X)
                txtEngineBurn1Y.Text = CStr(.Vertices(0).Y)
                txtEngineBurn1Z.Text = CStr(.Vertices(0).Z)
                txtEngineBurn2X.Text = CStr(.Vertices(1).X)
                txtEngineBurn2Y.Text = CStr(.Vertices(1).Y)
                txtEngineBurn2Z.Text = CStr(.Vertices(1).Z)
                txtEngineBurn3X.Text = CStr(.Vertices(2).X)
                txtEngineBurn3Y.Text = CStr(.Vertices(2).Y)
                txtEngineBurn3Z.Text = CStr(.Vertices(2).Z)
                txtEngineBurn4X.Text = CStr(.Vertices(3).X)
                txtEngineBurn4Y.Text = CStr(.Vertices(3).Y)
                txtEngineBurn4Z.Text = CStr(.Vertices(3).Z)
                txtEngineBurn5X.Text = CStr(.Vertices(4).X)
                txtEngineBurn5Y.Text = CStr(.Vertices(4).Y)
                txtEngineBurn5Z.Text = CStr(.Vertices(4).Z)

                ' Check if the parent doesn't exist.
                If Not _ComboBoxHasString(cboEngineBurnsParent.Items, .ParentName) Then _
     .ParentName = "Root" _
                : MsgBox("The parent joint specified by this engine burn does not exist!" & vbCrLf &
            "It has been changed to 'Root'.", MsgBoxStyle.Information, Me.Text)

                ' Update combo box.
                cboEngineBurnsParent.SelectedItem = .ParentName

            End With ' With m_HOD.EngineBurns(cstEngineBurns.SelectedIndex)
        End If ' If cstEngineBurns.SelectedIndex = -1 Then

    End Sub

    Private Sub cmdEngineBurnsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEngineBurnsAdd.Click
        Dim number As Integer = cstEngineBurns.Items.Count + 1
        Dim name As String

        Do
            ' Decide a name for the mesh.
            name = "EngineBurn" & CStr(number)

            ' See if it's a duplicate.
            If _ListBoxHasString(cstEngineBurns.Items, name) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, make a new EngineBurn mesh.
            Dim g As New EngineBurn With {
    .Name = name,
    .ParentName = "Root"
   }

            ' Add it.
            m_HOD.EngineBurns.Add(g)

            ' Add the mesh to the list.
            cstEngineBurns.Items.Add(g.ToString(), g.Visible)
            cstEngineBurns.SelectedIndex = cstEngineBurns.Items.Count - 1

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cmdEngineBurnsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEngineBurnsRemove.Click
        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Remove the mesh.
        m_HOD.EngineBurns.RemoveAt(cstEngineBurns.SelectedIndex)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Update list.
        cstEngineBurns.Items.RemoveAt(cstEngineBurns.SelectedIndex)
        cstEngineBurns.SelectedIndex = cstEngineBurns.Items.Count - 1

    End Sub

    Private Sub cmdEngineBurnsRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEngineBurnsRename.Click
        Dim name As String

        ' Get old name.
        Dim oldName As String = m_HOD.EngineBurns(cstEngineBurns.SelectedIndex).Name

        Do
            ' Get new name.
            name = InputBox("Enter new name for mesh: ", Me.Text, oldName)

            ' See if the user pressed cancel.
            If name = "" Then _
    Exit Sub

            ' HACK: If only casing is changed, then rename list item twice.
            ' Otherwise, the change doesn't seem to be reflected in the list.
            If String.Compare(oldName, name, True) = 0 Then _
    cstEngineBurns.Items(cstEngineBurns.SelectedIndex) = "" _
            : Exit Do

            ' See if it's a duplicate.
            ' For that, first rename the mesh (in list only) to something else
            ' so that it doesn't interfere in our search.
            cstEngineBurns.Items(cstEngineBurns.SelectedIndex) = ""

            ' Now see if it's a duplicate.
            If _ListBoxHasString(cstEngineBurns.Items, name) Then _
    cstEngineBurns.Items(cstEngineBurns.SelectedIndex) = oldName _
            : MsgBox("The name you entered is a duplicate!" & vbCrLf &
           "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
            : Continue Do

            ' Exit loop.
            Exit Do

        Loop ' Do

        ' Not a duplicate. OK to rename.
        m_HOD.EngineBurns(cstEngineBurns.SelectedIndex).Name = name

        ' Update list.
        cstEngineBurns.Items(cstEngineBurns.SelectedIndex) = name

        ' Update UI
        cstEngineBurns_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cboEngineBurnsParent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboEngineBurnsParent.SelectedIndexChanged
        ' Set the parent name if the combo box is focused.
        If (sender IsNot Nothing) AndAlso (CType(sender, ComboBox).Focused) Then _
   m_HOD.EngineBurns(cstEngineBurns.SelectedIndex).ParentName = CStr(cboEngineBurnsParent.SelectedItem)

    End Sub

    Private Sub txtEngineBurn_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtEngineBurn1X.TextChanged, txtEngineBurn1Y.TextChanged, txtEngineBurn1Z.TextChanged,
         txtEngineBurn2X.TextChanged, txtEngineBurn2Y.TextChanged, txtEngineBurn2Z.TextChanged,
         txtEngineBurn3X.TextChanged, txtEngineBurn3Y.TextChanged, txtEngineBurn3Z.TextChanged,
         txtEngineBurn4X.TextChanged, txtEngineBurn4Y.TextChanged, txtEngineBurn4Z.TextChanged,
         txtEngineBurn5X.TextChanged, txtEngineBurn5Y.TextChanged, txtEngineBurn5Z.TextChanged

        ' Get the text box.
        Dim TextBox As TextBox = CType(sender, TextBox)

        ' See if control has focus.
        If Not TextBox.Focused Then _
   Exit Sub

        ' See if input is numeric.
        If Not IsNumeric(TextBox.Text) Then _
   Exit Sub

        ' Get the number.
        Dim number As Single = CSng(TextBox.Text)

        ' Update data.
        With m_HOD.EngineBurns(cstEngineBurns.SelectedIndex)
            If sender Is txtEngineBurn1X Then _
    .Vertices(0) = New Vector3(number, .Vertices(0).Y, .Vertices(0).Z)

            If sender Is txtEngineBurn1Y Then _
    .Vertices(0) = New Vector3(.Vertices(0).X, number, .Vertices(0).Z)

            If sender Is txtEngineBurn1Z Then _
    .Vertices(0) = New Vector3(.Vertices(0).X, .Vertices(0).Y, number)

            If sender Is txtEngineBurn2X Then _
    .Vertices(1) = New Vector3(number, .Vertices(1).Y, .Vertices(1).Z)

            If sender Is txtEngineBurn2Y Then _
    .Vertices(1) = New Vector3(.Vertices(1).X, number, .Vertices(1).Z)

            If sender Is txtEngineBurn2Z Then _
    .Vertices(1) = New Vector3(.Vertices(1).X, .Vertices(1).Y, number)

            If sender Is txtEngineBurn3X Then _
    .Vertices(2) = New Vector3(number, .Vertices(2).Y, .Vertices(2).Z)

            If sender Is txtEngineBurn3Y Then _
    .Vertices(2) = New Vector3(.Vertices(2).X, number, .Vertices(2).Z)

            If sender Is txtEngineBurn3Z Then _
    .Vertices(2) = New Vector3(.Vertices(2).X, .Vertices(2).Y, number)

            If sender Is txtEngineBurn4X Then _
    .Vertices(3) = New Vector3(number, .Vertices(3).Y, .Vertices(3).Z)

            If sender Is txtEngineBurn4Y Then _
    .Vertices(3) = New Vector3(.Vertices(3).X, number, .Vertices(3).Z)

            If sender Is txtEngineBurn4Z Then _
    .Vertices(3) = New Vector3(.Vertices(3).X, .Vertices(3).Y, number)

            If sender Is txtEngineBurn5X Then _
    .Vertices(4) = New Vector3(number, .Vertices(4).Y, .Vertices(4).Z)

            If sender Is txtEngineBurn5Y Then _
    .Vertices(4) = New Vector3(.Vertices(4).X, number, .Vertices(4).Z)

            If sender Is txtEngineBurn5Z Then _
    .Vertices(4) = New Vector3(.Vertices(4).X, .Vertices(4).Y, number)

        End With ' With m_HOD.EngineBurns(cstEngineBurns.SelectedIndex)

    End Sub

    Private Sub txtEngineBurn_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtEngineBurn1X.Validated, txtEngineBurn1Y.Validated, txtEngineBurn1Z.Validated,
         txtEngineBurn2X.Validated, txtEngineBurn2Y.Validated, txtEngineBurn2Z.Validated,
         txtEngineBurn3X.Validated, txtEngineBurn3Y.Validated, txtEngineBurn3Z.Validated,
         txtEngineBurn4X.Validated, txtEngineBurn4Y.Validated, txtEngineBurn4Z.Validated,
         txtEngineBurn5X.Validated, txtEngineBurn5Y.Validated, txtEngineBurn5Z.Validated

        ' Get the text box.
        Dim TextBox As TextBox = CType(sender, TextBox)

        ' Reset error.
        ErrorProvider.SetError(TextBox, "")

    End Sub

#End Region

#Region " NavLights UI "

    ''' <summary>
    ''' Fills the image with the specified colour.
    ''' </summary>
    Private Sub _NavLightFillImage(ByVal image As Image, Optional ByVal r As Single = 0, Optional ByVal g As Single = 0,
                                                      Optional ByVal b As Single = 0)

        ' Get the colour blended against black.
        Dim _r As Integer = CInt(Math.Max(0, Math.Min(255, 255 * r)))
        Dim _g As Integer = CInt(Math.Max(0, Math.Min(255, 255 * g)))
        Dim _b As Integer = CInt(Math.Max(0, Math.Min(255, 255 * b)))

        ' Fill colour.
        Dim graphics As Graphics = Graphics.FromImage(image)
        graphics.Clear(Color.FromArgb(255, _r, _g, _b))
        graphics.Dispose()

    End Sub

    Private Sub tabNavLights_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabNavLights.Enter
        ' See if the tab enter event is marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        ' Update the combo box.
        With cboNavLightsName.Items
            ' Get joints.
            Dim joints() As Joint = m_HOD.Root.ToArray()

            ' Clear old items.
            .Clear()

            ' Add all joints.
            For I As Integer = 0 To joints.Length - 1
                .Add(joints(I).Name)

            Next I ' For I As Integer = 0 To joints.Length - 1

            ' Erase array.
            Erase joints

        End With ' With cboNavLightsName.Items

        ' Update the checked list box.
        With cstNavLights.Items
            ' Clear old items.
            .Clear()

            ' Add all items.
            For I As Integer = 0 To m_HOD.NavLights.Count - 1
                .Add(m_HOD.NavLights(I).ToString(), m_HOD.NavLights(I).Visible)

            Next I ' For I As Integer = 0 To m_HOD.NavLights.Count - 1

        End With ' With cstNavLights.Items

        ' Update picture box.
        If pbxNavLightsSample.Image Is Nothing Then _
   pbxNavLightsSample.Image = New Bitmap(pbxNavLightsSample.ClientSize.Width,
                                         pbxNavLightsSample.ClientSize.Height,
                                         Imaging.PixelFormat.Format24bppRgb)

        ' Update selection.
        cstNavLights_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cstNavLights_Cut()
        ' See if any item is selected.
        If cstNavLights.SelectedIndex = -1 Then _
   Exit Sub

        ' Get the item.
        Dim n As NavLight = m_HOD.NavLights(cstNavLights.SelectedIndex)

        ' Copy to clipboard.
        mnuEditClipboard = New NavLight(n)

        ' Remove from HOD.
        m_HOD.NavLights.Remove(n)

        ' Refresh.
        cstNavLights.Items.RemoveAt(cstNavLights.SelectedIndex)

    End Sub

    Private Sub cstNavLights_Copy()
        ' See if any item is selected.
        If cstNavLights.SelectedIndex = -1 Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New NavLight(m_HOD.NavLights(cstNavLights.SelectedIndex))

    End Sub

    Private Sub cstNavLights_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is NavLight Then _
   Exit Sub

        ' Get the item.
        Dim n As NavLight = CType(mnuEditClipboard, NavLight)

        ' Remove reference.
        mnuEditClipboard = Nothing

        ' Add.
        m_HOD.NavLights.Add(n)
        cstNavLights.Items.Add(n.ToString(), n.Visible)

    End Sub

    Private Sub cstNavLights_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles cstNavLights.ItemCheck
        ' Set the visible flag if the checked list box is focused.
        If (sender IsNot Nothing) AndAlso (CType(sender, CheckedListBox).Focused) Then _
   m_HOD.NavLights(e.Index).Visible = (e.NewValue = CheckState.Checked)

    End Sub

    Private Sub cstNavLights_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cstNavLights.SelectedIndexChanged
        If cstNavLights.SelectedIndex = -1 Then
            ' Since no item is selected, disable all controls except the
            ' add button.
            cmdNavLightsRemove.Enabled = False
            pbxNavLightsSample.Enabled = False
            cboNavLightsName.Enabled = False
            chkNavLightsVisibleSprite.Enabled = False
            chkNavLightsVisibleSprite.Checked = False
            chkNavLightsHighEnd.Enabled = False
            chkNavLightsHighEnd.Checked = False
            _NavLightFillImage(pbxNavLightsSample.Image)
            pbxNavLightsSample.Refresh()

            For Each t As Control In tabNavLights.Controls
                If Not TypeOf t Is TextBox Then _
     Continue For

                t.Enabled = False
                t.Text = ""

            Next t ' For Each t As Control In tabNavLights.Controls

        Else ' If cstNavLights.SelectedIndex = -1 Then
            ' Since an item is selected, enable all controls.
            cmdNavLightsRemove.Enabled = True
            pbxNavLightsSample.Enabled = True
            cboNavLightsName.Enabled = True
            chkNavLightsVisibleSprite.Enabled = True
            chkNavLightsHighEnd.Enabled = True

            For Each t As Control In tabNavLights.Controls
                If Not TypeOf t Is TextBox Then _
     Continue For

                t.Enabled = True

            Next t ' For Each t As Control In tabNavLights.Controls

            With m_HOD.NavLights(cstNavLights.SelectedIndex)
                ' Check if the parent doesn't exist.
                If Not _ComboBoxHasString(cboNavLightsName.Items, .Name) Then _
     .Name = "Root" _
                : MsgBox("The joint specified by this navlight does not exist!" & vbCrLf &
            "It has been changed to 'Root'.", MsgBoxStyle.Information, Me.Text)

                ' Update combo box.
                cboNavLightsName.SelectedItem = .Name

                ' Update text boxes.
                txtNavLightsSection.Text = CStr(.Section)
                txtNavLightsSize.Text = CStr(.Size)
                txtNavLightsPhase.Text = CStr(.Phase)
                txtNavLightsFrequency.Text = CStr(.Frequency)
                txtNavLightsStyle.Text = .Style
                txtNavLightsDistance.Text = CStr(.Distance)

                txtNavlightsColourR.Text = CStr(.Colour.Red)
                txtNavlightsColourG.Text = CStr(.Colour.Green)
                txtNavlightsColourB.Text = CStr(.Colour.Blue)

                ' Update check boxes.
                chkNavLightsVisibleSprite.Checked = .SpriteVisible
                chkNavLightsHighEnd.Checked = .HighEndOnly

                ' Update picture box.
                _NavLightFillImage(pbxNavLightsSample.Image, .Colour.Red, .Colour.Green, .Colour.Blue)
                pbxNavLightsSample.Refresh()

            End With ' With m_HOD.NavLights(cstNavLights.SelectedIndex)

        End If ' If cstNavLights.SelectedIndex = -1 Then

    End Sub

    Private Sub cmdNavLightsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNavLightsAdd.Click
        Dim n As New NavLight

        ' Add to lists.
        m_HOD.NavLights.Add(n)

        ' Update list.
        cstNavLights.Items.Add(n.ToString(), n.Visible)
        cstNavLights.SelectedIndex = cstNavLights.Items.Count - 1

    End Sub

    Private Sub cmdNavLightsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNavLightsRemove.Click
        ' Remove from lists.
        m_HOD.NavLights.RemoveAt(cstNavLights.SelectedIndex)

        ' Update list.
        cstNavLights.Items.RemoveAt(cstNavLights.SelectedIndex)
        cstNavLights.SelectedIndex = cstNavLights.Items.Count - 1

    End Sub

    Private Sub pbxNavLightsSample_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbxNavLightsSample.Click
        ' Set initial colour.
        ColorDialog.Color = Color.FromArgb(m_HOD.NavLights(cstNavLights.SelectedIndex).Colour.ToArgb())

        ' If user pressed cancel then exit.
        If ColorDialog.ShowDialog() = DialogResult.Cancel Then _
   Exit Sub

        ' Update colours.
        m_HOD.NavLights(cstNavLights.SelectedIndex).Colour = Direct3D.ColorValue.FromArgb(ColorDialog.Color.ToArgb())

        ' Update text boxes.
        txtNavlightsColourR.Text = CStr(m_HOD.NavLights(cstNavLights.SelectedIndex).Colour.Red)
        txtNavlightsColourG.Text = CStr(m_HOD.NavLights(cstNavLights.SelectedIndex).Colour.Green)
        txtNavlightsColourB.Text = CStr(m_HOD.NavLights(cstNavLights.SelectedIndex).Colour.Blue)

    End Sub

    Private Sub cboNavLightsName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNavLightsName.SelectedIndexChanged
        ' Update name if focused.
        If cboNavLightsName.Focused Then _
   m_HOD.NavLights(cstNavLights.SelectedIndex).Name = CStr(cboNavLightsName.SelectedItem) _
        : cstNavLights.Items(cstNavLights.SelectedIndex) = CStr(cboNavLightsName.SelectedItem)

    End Sub

    Private Sub txtNavLights_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtNavLightsSection.TextChanged, txtNavLightsSize.TextChanged, txtNavLightsPhase.TextChanged, txtNavLightsFrequency.TextChanged,
         txtNavLightsStyle.TextChanged, txtNavLightsDistance.TextChanged, txtNavlightsColourR.TextChanged, txtNavlightsColourG.TextChanged,
         txtNavlightsColourB.TextChanged
        ' Get the text box.
        Dim textBox As TextBox = CType(sender, TextBox)

        ' See if the text box is focused.
        If Not textBox.Focused Then _
   Exit Sub

        ' See if any data has been entered.
        If textBox.Text = "" Then _
   Exit Sub

        ' See if entered data is numeric.
        If (textBox IsNot txtNavLightsStyle) AndAlso (Not IsNumeric(textBox.Text)) Then _
   Exit Sub

        ' Get the number.
        Dim number As Single
        Dim uint As UInteger

        If Single.TryParse(textBox.Text, number) Then _
   number = CSng(textBox.Text)

        ' Update data as needed.
        With m_HOD.NavLights(cstNavLights.SelectedIndex)
            If (sender Is txtNavLightsSection) AndAlso (UInteger.TryParse(textBox.Text, uint)) Then _
    .Section = uint

            If (sender Is txtNavLightsSize) AndAlso (number >= 0.0F) Then _
    .Size = number

            If (sender Is txtNavLightsPhase) AndAlso (number >= 0.0F) Then _
    .Phase = number

            If (sender Is txtNavLightsFrequency) AndAlso (number >= 0.0F) Then _
    .Frequency = number

            If sender Is txtNavLightsStyle Then _
    .Style = textBox.Text

            If (sender Is txtNavLightsDistance) AndAlso (number >= 0.0F) Then _
    .Distance = number

            If sender Is txtNavlightsColourR Then _
    .Colour = New Direct3D.ColorValue(number, .Colour.Green, .Colour.Blue) _
            : _NavLightFillImage(pbxNavLightsSample.Image, .Colour.Red, .Colour.Green, .Colour.Blue) _
            : pbxNavLightsSample.Refresh()

            If sender Is txtNavlightsColourG Then _
    .Colour = New Direct3D.ColorValue(.Colour.Red, number, .Colour.Blue) _
            : _NavLightFillImage(pbxNavLightsSample.Image, .Colour.Red, .Colour.Green, .Colour.Blue) _
            : pbxNavLightsSample.Refresh()

            If sender Is txtNavlightsColourB Then _
    .Colour = New Direct3D.ColorValue(.Colour.Red, .Colour.Green, number) _
            : _NavLightFillImage(pbxNavLightsSample.Image, .Colour.Red, .Colour.Green, .Colour.Blue) _
            : pbxNavLightsSample.Refresh()

        End With ' With m_HOD.NavLights(cstNavLights.SelectedIndex)

    End Sub

    Private Sub txtNavLights_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtNavLightsSection.Validated, txtNavLightsSize.Validated, txtNavLightsPhase.Validated, txtNavLightsFrequency.Validated,
         txtNavLightsStyle.Validated, txtNavLightsDistance.Validated, txtNavlightsColourR.Validated, txtNavlightsColourG.Validated,
         txtNavlightsColourB.Validated

        ' Get the text box.
        Dim textBox As TextBox = CType(sender, TextBox)

        ' Clear the error.
        ErrorProvider.SetError(textBox, "")

    End Sub

    Private Sub chkNavLights_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles chkNavLightsVisibleSprite.CheckedChanged, chkNavLightsHighEnd.CheckedChanged

        ' If not focused, do nothing.
        If Not CType(sender, CheckBox).Focused Then _
   Exit Sub

        ' Update data.
        If sender Is chkNavLightsVisibleSprite Then _
   m_HOD.NavLights(cstNavLights.SelectedIndex).SpriteVisible = chkNavLightsVisibleSprite.Checked

        If sender Is chkNavLightsHighEnd Then _
   m_HOD.NavLights(cstNavLights.SelectedIndex).HighEndOnly = chkNavLightsHighEnd.Checked

    End Sub

#End Region

#Region " Markers UI "

    Private Sub tabMarkers_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabMarkers.Enter
        ' See if the tab enter event is marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        ' Update the combo box.
        With cboMarkersParent.Items
            ' Get joints.
            Dim joints() As Joint = m_HOD.Root.ToArray()

            ' Clear old items.
            .Clear()

            ' Add all joints.
            For I As Integer = 0 To joints.Length - 1
                .Add(joints(I).Name)

            Next I ' For I As Integer = 0 To joints.Length - 1

            ' Erase array.
            Erase joints

        End With ' With cboMarkersParent.Items

        ' Update the checked list box.
        With cstMarkers.Items
            ' Clear old items.
            .Clear()

            ' Add all meshes.
            For I As Integer = 0 To m_HOD.Markers.Count - 1
                .Add(m_HOD.Markers(I).ToString(), m_HOD.Markers(I).Visible)

            Next I ' For I As Integer = 0 To m_HOD.Markers.Count - 1

        End With ' With cstMarkers.Items

        ' Update selection.
        cstMarkers_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cstMarkers_Cut()
        ' See if any item is selected.
        If cstMarkers.SelectedIndex = -1 Then _
   Exit Sub

        ' Get it's index.
        Dim ind As Integer = cstMarkers.SelectedIndex

        ' Copy to clipboard.
        mnuEditClipboard = New Marker(m_HOD.Markers(ind))

        ' Remove from HOD.
        m_HOD.Markers.RemoveAt(ind)

        ' Refresh.
        cstMarkers.Items.RemoveAt(ind)

    End Sub

    Private Sub cstMarkers_Copy()
        ' See if any item is selected.
        If cstMarkers.SelectedIndex = -1 Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New Marker(m_HOD.Markers(cstMarkers.SelectedIndex))

    End Sub

    Private Sub cstMarkers_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is Marker Then _
   Exit Sub

        ' Get the item.
        Dim m As Marker = CType(mnuEditClipboard, Marker)

        ' Remove reference.
        mnuEditClipboard = Nothing

        ' See if it's already present.
        If Not cstMarkers.Items.Contains(m.ToString()) Then _
   m_HOD.Markers.Add(m) _
        : cstMarkers.Items.Add(m.ToString(), m.Visible) _
        : Exit Sub

        ' Rename item.
        Dim number As Integer = 2

        Do
            ' See if this name is present.
            If cstMarkers.Items.Contains(m.ToString() & CStr(number)) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, add it.
            m.Name &= CStr(number)
            m_HOD.Markers.Add(m)
            cstMarkers.Items.Add(m.ToString(), m.Visible)

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cstMarkers_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles cstMarkers.ItemCheck
        ' Set the visible flag if the checked list box is focused.
        If CType(sender, CheckedListBox).Focused Then _
   m_HOD.Markers(e.Index).Visible = (e.NewValue = CheckState.Checked)

    End Sub

    Private Sub cstMarkers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cstMarkers.SelectedIndexChanged
        If cstMarkers.SelectedIndex = -1 Then
            ' Since no marker is selected, disable all UI controls,
            ' except the add button.
            cmdMarkersRemove.Enabled = False
            cmdMarkersRename.Enabled = False
            cboMarkersParent.Enabled = False
            txtMarkerPositionX.Enabled = False
            txtMarkerPositionY.Enabled = False
            txtMarkerPositionZ.Enabled = False
            txtMarkerRotationX.Enabled = False
            txtMarkerRotationY.Enabled = False
            txtMarkerRotationZ.Enabled = False

            cboMarkersParent.SelectedIndex = -1
            txtMarkerPositionX.Text = ""
            txtMarkerPositionY.Text = ""
            txtMarkerPositionZ.Text = ""
            txtMarkerRotationX.Text = ""
            txtMarkerRotationY.Text = ""
            txtMarkerRotationZ.Text = ""

        Else ' If cstmarkers.SelectedIndex = -1 Then
            ' Since a marker is selected, enable all UI controls.
            cmdMarkersRemove.Enabled = True
            cmdMarkersRename.Enabled = True
            cboMarkersParent.Enabled = True
            txtMarkerPositionX.Enabled = True
            txtMarkerPositionY.Enabled = True
            txtMarkerPositionZ.Enabled = True
            txtMarkerRotationX.Enabled = True
            txtMarkerRotationY.Enabled = True
            txtMarkerRotationZ.Enabled = True

            ' Update text boxes.
            txtMarkerPositionX.Text = CStr(m_HOD.Markers(cstMarkers.SelectedIndex).Position.X)
            txtMarkerPositionY.Text = CStr(m_HOD.Markers(cstMarkers.SelectedIndex).Position.Y)
            txtMarkerPositionZ.Text = CStr(m_HOD.Markers(cstMarkers.SelectedIndex).Position.Z)
            txtMarkerRotationX.Text = FormatNumber(180 * m_HOD.Markers(cstMarkers.SelectedIndex).Rotation.X / Math.PI, 3, TriState.True, TriState.False, TriState.False)
            txtMarkerRotationY.Text = FormatNumber(180 * m_HOD.Markers(cstMarkers.SelectedIndex).Rotation.Y / Math.PI, 3, TriState.True, TriState.False, TriState.False)
            txtMarkerRotationZ.Text = FormatNumber(180 * m_HOD.Markers(cstMarkers.SelectedIndex).Rotation.Z / Math.PI, 3, TriState.True, TriState.False, TriState.False)

            ' Check if the parent doesn't exist.
            If Not _ComboBoxHasString(cboMarkersParent.Items, m_HOD.Markers(cstMarkers.SelectedIndex).ParentName) Then _
    m_HOD.Markers(cstMarkers.SelectedIndex).ParentName = "Root" _
            : MsgBox("The parent joint specified by this marker does not exist!" & vbCrLf &
           "It has been changed to 'Root'.", MsgBoxStyle.Information, Me.Text)

            ' Update combo box.
            cboMarkersParent.SelectedItem = m_HOD.Markers(cstMarkers.SelectedIndex).ParentName

        End If ' If cstmarkers.SelectedIndex = -1 Then

    End Sub

    Private Sub cmdMarkersAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMarkersAdd.Click
        Dim number As Integer = cstMarkers.Items.Count + 1
        Dim name As String

        Do
            ' Decide a name for the mesh.
            name = "marker" & CStr(number)

            ' See if it's a duplicate.
            If _ListBoxHasString(cstMarkers.Items, name) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, make a new marker mesh.
            Dim g As New Marker With {
    .Name = name,
    .ParentName = "Root"
   }

            ' Add it.
            m_HOD.Markers.Add(g)

            ' Add the mesh to the list.
            cstMarkers.Items.Add(g.ToString(), g.Visible)
            cstMarkers.SelectedIndex = cstMarkers.Items.Count - 1

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cmdMarkersRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMarkersRemove.Click
        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Remove the mesh.
        m_HOD.Markers.RemoveAt(cstMarkers.SelectedIndex)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Update list.
        cstMarkers.Items.RemoveAt(cstMarkers.SelectedIndex)
        cstMarkers.SelectedIndex = cstMarkers.Items.Count - 1

    End Sub

    Private Sub cmdMarkersRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMarkersRename.Click
        Dim name As String

        ' Get old name.
        Dim oldName As String = m_HOD.Markers(cstMarkers.SelectedIndex).Name

        Do
            ' Get new name.
            name = InputBox("Enter new name for marker: ", Me.Text, oldName)

            ' See if the user pressed cancel.
            If name = "" Then _
    Exit Sub

            ' HACK: If only casing is changed, then rename list item twice.
            ' Otherwise, the change doesn't seem to be reflected in the list.
            If String.Compare(oldName, name, True) = 0 Then _
    cstMarkers.Items(cstMarkers.SelectedIndex) = "" _
            : Exit Do

            ' See if it's a duplicate.
            ' For that, first rename the mesh (in list only) to something else
            ' so that it doesn't interfere in our search.
            cstMarkers.Items(cstMarkers.SelectedIndex) = ""

            ' Now see if it's a duplicate.
            If _ListBoxHasString(cstMarkers.Items, name) Then _
    cstMarkers.Items(cstMarkers.SelectedIndex) = oldName _
            : MsgBox("The name you entered is a duplicate!" & vbCrLf &
           "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
            : Continue Do

            ' Exit loop.
            Exit Do

        Loop ' Do

        ' Not a duplicate. OK to rename.
        m_HOD.Markers(cstMarkers.SelectedIndex).Name = name

        ' Update list.
        cstMarkers.Items(cstMarkers.SelectedIndex) = name

        ' Update UI
        cstMarkers_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cboMarkersParent_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMarkersParent.SelectedIndexChanged
        ' Set the parent name if the combo box is focused.
        If CType(sender, ComboBox).Focused Then _
   m_HOD.Markers(cstMarkers.SelectedIndex).ParentName = CStr(cboMarkersParent.SelectedItem)

    End Sub

    Private Sub txtMarker_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtMarkerPositionX.TextChanged, txtMarkerPositionY.TextChanged, txtMarkerPositionZ.TextChanged,
         txtMarkerRotationX.TextChanged, txtMarkerRotationY.TextChanged, txtMarkerRotationZ.TextChanged

        ' Get the text box.
        Dim TextBox As TextBox = CType(sender, TextBox)

        ' See if it is focused.
        If (TextBox IsNot Nothing) AndAlso (Not TextBox.Focused) Then _
   Exit Sub

        ' See if any text has been entered.
        If TextBox.Text = "" Then _
   Exit Sub

        ' See if entered text is numeric.
        If Not IsNumeric(TextBox.Text) Then _
   Exit Sub

        ' Get the number.
        Dim number As Single = CSng(TextBox.Text)
        Dim rotation As Single = CSng(Math.PI * number / 180)

        ' Set the appropriate fields.
        With m_HOD.Markers(cstMarkers.SelectedIndex)
            If sender Is txtMarkerPositionX Then _
    .Position = New Vector3(number, .Position.Y, .Position.Z)

            If sender Is txtMarkerPositionY Then _
    .Position = New Vector3(.Position.X, number, .Position.Z)

            If sender Is txtMarkerPositionZ Then _
    .Position = New Vector3(.Position.X, .Position.Y, number)

            If sender Is txtMarkerRotationX Then _
    .Rotation = New Vector3(rotation, .Rotation.Y, .Rotation.Z)

            If sender Is txtMarkerRotationY Then _
    .Rotation = New Vector3(.Rotation.X, rotation, .Rotation.Z)

            If sender Is txtMarkerRotationZ Then _
    .Rotation = New Vector3(.Rotation.X, .Rotation.Y, rotation)

        End With ' With m_HOD.Markers(cstMarkers.SelectedIndex)

    End Sub

    Private Sub txtMarker_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtMarkerPositionX.Validated, txtMarkerPositionY.Validated, txtMarkerPositionZ.Validated,
         txtMarkerRotationX.Validated, txtMarkerRotationY.Validated, txtMarkerRotationZ.Validated

        ' Get the text box.
        Dim TextBox As TextBox = CType(sender, TextBox)

        ' Remove error.
        ErrorProvider.SetError(TextBox, "")

    End Sub

#End Region

#Region " Dockpaths UI "

    Private Sub tabDockpaths_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabDockpaths.Enter
        ' See if the tab enter event is marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        ' Update the checked list box.
        With cstDockpaths.Items
            ' Clear old items.
            .Clear()

            ' Add all paths.
            For I As Integer = 0 To m_HOD.Dockpaths.Count - 1
                .Add(m_HOD.Dockpaths(I).ToString(), m_HOD.Dockpaths(I).Visible)

            Next I ' For I As Integer = 0 To m_HOD.dockpaths.Count - 1

        End With ' With cstdockpaths.Items

        ' Update selection.
        cstDockpaths_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cstDockpaths_Cut()
        ' See if any item is selected.
        If cstDockpaths.SelectedIndex = -1 Then _
   Exit Sub

        ' Get it's index.
        Dim ind As Integer = cstDockpaths.SelectedIndex

        ' Copy to clipboard.
        mnuEditClipboard = New Dockpath(m_HOD.Dockpaths(ind))

        ' Remove from HOD.
        m_HOD.Dockpaths.RemoveAt(ind)

        ' Refresh.
        cstDockpaths.Items.RemoveAt(ind)

    End Sub

    Private Sub cstDockpaths_Copy()
        ' See if any item is selected.
        If cstDockpaths.SelectedIndex = -1 Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New Dockpath(m_HOD.Dockpaths(cstDockpaths.SelectedIndex))

    End Sub

    Private Sub cstDockpaths_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is Dockpath Then _
   Exit Sub

        ' Get the item.
        Dim d As Dockpath = CType(mnuEditClipboard, Dockpath)

        ' Remove reference.
        mnuEditClipboard = Nothing

        ' See if it's already present.
        If Not cstDockpaths.Items.Contains(d.ToString()) Then _
   m_HOD.Dockpaths.Add(d) _
        : cstDockpaths.Items.Add(d.ToString(), d.Visible) _
        : Exit Sub

        ' Rename item.
        Dim number As Integer = 2

        Do
            ' See if this name is present.
            If cstDockpaths.Items.Contains(d.ToString() & CStr(number)) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, add it.
            d.Name &= CStr(number)
            m_HOD.Dockpaths.Add(d)
            cstDockpaths.Items.Add(d.ToString(), d.Visible)

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cstDockpaths_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles cstDockpaths.ItemCheck
        ' Set the visible flag if the checked list box is focused.
        If (sender IsNot Nothing) AndAlso (CType(sender, CheckedListBox).Focused) Then _
   m_HOD.Dockpaths(e.Index).Visible = (e.NewValue = CheckState.Checked)

    End Sub

    Private Sub cstDockpaths_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cstDockpaths.SelectedIndexChanged
        Dim enable As Boolean = (cstDockpaths.SelectedIndex <> -1)

        ' Depending on whether an item is selected or not,
        ' enable\disable all UI controls, except the add button.
        cmdDockpathsRemove.Enabled = enable
        cmdDockpathsRename.Enabled = enable
        cmdDockpathsKeyframesAdd.Enabled = enable
        txtDockpathsParentName.Enabled = enable
        txtDockpathsGlobalTolerance.Enabled = enable
        txtDockpathsDockFamilies.Enabled = enable
        txtDockpathsLinkedPaths.Enabled = enable
        chkDockpathsExitPath.Enabled = enable
        chkDockpathsLatchPath.Enabled = enable
        chkDockpathsUseAnim.Enabled = enable
        sldDockpathsKeyframe.Enabled = enable

        If cstDockpaths.SelectedIndex = -1 Then
            ' Update text boxes.
            txtDockpathsParentName.Text = ""
            txtDockpathsGlobalTolerance.Text = ""
            txtDockpathsDockFamilies.Text = ""
            txtDockpathsLinkedPaths.Text = ""

            ' Update check boxes.
            chkDockpathsExitPath.Checked = False
            chkDockpathsLatchPath.Checked = False
            chkDockpathsUseAnim.Checked = False

            ' Update slider
            sldDockpathsKeyframe.Enabled = False
            sldDockpathsKeyframe.Value = 0
            sldDockpathsKeyframe.Maximum = 0

        Else ' If cstdockpaths.SelectedIndex = -1 Then
            ' Update UI.
            With m_HOD.Dockpaths(cstDockpaths.SelectedIndex)
                ' Update text boxes.
                txtDockpathsParentName.Text = .ParentName
                txtDockpathsGlobalTolerance.Text = CStr(.Global.Tolerance)
                txtDockpathsDockFamilies.Text = .Global.DockFamilies
                txtDockpathsLinkedPaths.Text = .Global.LinkedPaths

                ' Update check boxes.
                chkDockpathsExitPath.Checked = .Global.IsExit
                chkDockpathsLatchPath.Checked = .Global.IsLatch
                chkDockpathsUseAnim.Checked = .Global.UseAnimation

                ' Update slider.
                If .Dockpoints.Count = 0 Then _
     sldDockpathsKeyframe.Enabled = False _
                : sldDockpathsKeyframe.Value = 0 _
                : sldDockpathsKeyframe.Maximum = 0 _
    Else _
     sldDockpathsKeyframe.Enabled = True _
                : sldDockpathsKeyframe.Value = 0 _
                : sldDockpathsKeyframe.Maximum = .Dockpoints.Count - 1

            End With ' With m_HOD.Dockpaths(cstDockpaths.SelectedIndex)
        End If ' If cstdockpaths.SelectedIndex = -1 Then

        ' Force update of slider because the value changed event may not have fired.
        sldDockpathsKeyframe_ValueChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cmdDockpathsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDockpathsAdd.Click
        Dim number As Integer = cstDockpaths.Items.Count + 1
        Dim name As String

        Do
            ' Decide a name for the mesh.
            name = "path" & CStr(number)

            ' See if it's a duplicate.
            If _ListBoxHasString(cstDockpaths.Items, name) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, make a new dockpath mesh.
            Dim g As New Dockpath With {
    .Name = name,
    .ParentName = "world"
   }

            ' Add it.
            m_HOD.Dockpaths.Add(g)

            ' Add the mesh to the list.
            cstDockpaths.Items.Add(g.ToString(), g.Visible)
            cstDockpaths.SelectedIndex = cstDockpaths.Items.Count - 1

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cmdDockpathsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDockpathsRemove.Click
        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Remove the mesh.
        m_HOD.Dockpaths.RemoveAt(cstDockpaths.SelectedIndex)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Update list.
        cstDockpaths.Items.RemoveAt(cstDockpaths.SelectedIndex)
        cstDockpaths.SelectedIndex = cstDockpaths.Items.Count - 1

    End Sub

    Private Sub cmdDockpathsRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDockpathsRename.Click
        Dim name As String

        ' Get old name.
        Dim oldName As String = m_HOD.Dockpaths(cstDockpaths.SelectedIndex).Name

        Do
            ' Get new name.
            name = InputBox("Enter new name for dockpath: ", Me.Text, oldName)

            ' See if the user pressed cancel.
            If name = "" Then _
    Exit Sub

            ' HACK: If only casing is changed, then rename list item twice.
            ' Otherwise, the change doesn't seem to be reflected in the list.
            If String.Compare(oldName, name, True) = 0 Then _
    cstDockpaths.Items(cstDockpaths.SelectedIndex) = "" _
            : Exit Do

            ' See if it's a duplicate.
            ' For that, first rename the mesh (in list only) to something else
            ' so that it doesn't interfere in our search.
            cstDockpaths.Items(cstDockpaths.SelectedIndex) = ""

            ' Now see if it's a duplicate.
            If _ListBoxHasString(cstDockpaths.Items, name) Then _
    cstDockpaths.Items(cstDockpaths.SelectedIndex) = oldName _
            : MsgBox("The name you entered is a duplicate!" & vbCrLf &
           "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
            : Continue Do

            ' Exit loop.
            Exit Do

        Loop ' Do

        ' Not a duplicate. OK to rename.
        m_HOD.Dockpaths(cstDockpaths.SelectedIndex).Name = name

        ' Update list.
        cstDockpaths.Items(cstDockpaths.SelectedIndex) = name

        ' Update UI
        cstDockpaths_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub txtDockpathsString_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtDockpathsParentName.TextChanged, txtDockpathsDockFamilies.TextChanged, txtDockpathsLinkedPaths.TextChanged

        ' Get the text-box.
        Dim textBox As TextBox = CType(sender, TextBox)

        ' See if it is focused.
        If Not textBox.Focused Then _
   Exit Sub

        ' Set data.
        If sender Is txtDockpathsParentName Then _
   m_HOD.Dockpaths(cstDockpaths.SelectedIndex).ParentName = txtDockpathsParentName.Text

        If sender Is txtDockpathsDockFamilies Then _
   m_HOD.Dockpaths(cstDockpaths.SelectedIndex).Global.DockFamilies = txtDockpathsDockFamilies.Text

        If sender Is txtDockpathsLinkedPaths Then _
   m_HOD.Dockpaths(cstDockpaths.SelectedIndex).Global.LinkedPaths = txtDockpathsLinkedPaths.Text

    End Sub

    Private Sub txtDockpathsGlobalTolerance_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtDockpathsGlobalTolerance.TextChanged

        ' Get the text-box.
        Dim textBox As TextBox = CType(sender, TextBox)

        ' See if it is focused.
        If Not textBox.Focused Then _
   Exit Sub

        ' See if any data has been entered.
        If textBox.Text = "" Then _
   Exit Sub

        ' See if numeric data was entered.
        If Not IsNumeric(textBox.Text) Then _
   Exit Sub

        ' Get the number.
        Dim number As Single = CSng(textBox.Text)

        ' Set data.
        If sender Is txtDockpathsGlobalTolerance Then _
   m_HOD.Dockpaths(cstDockpaths.SelectedIndex).Global.Tolerance = number

    End Sub

    Private Sub txtDockpathsString_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtDockpathsParentName.Validated, txtDockpathsGlobalTolerance.Validated,
         txtDockpathsDockFamilies.Validated, txtDockpathsLinkedPaths.Validated

        ' Get the text-box.
        Dim textBox As TextBox = CType(sender, TextBox)

        ' Set error.
        ErrorProvider.SetError(textBox, "")

    End Sub

    Private Sub chkDockpaths_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles chkDockpathsExitPath.CheckedChanged, chkDockpathsLatchPath.CheckedChanged, chkDockpathsUseAnim.CheckedChanged

        ' If the check box is not focused, do nothing.
        If Not CType(sender, CheckBox).Focused Then _
   Exit Sub

        ' Get whether it's checked or not.
        Dim check As Boolean = CType(sender, CheckBox).Checked

        ' Set data.
        If sender Is chkDockpathsExitPath Then _
   m_HOD.Dockpaths(cstDockpaths.SelectedIndex).Global.IsExit = check

        If sender Is chkDockpathsLatchPath Then _
   m_HOD.Dockpaths(cstDockpaths.SelectedIndex).Global.IsLatch = check

        If sender Is chkDockpathsUseAnim Then _
   m_HOD.Dockpaths(cstDockpaths.SelectedIndex).Global.UseAnimation = check

    End Sub

    Private Sub sldDockpathsKeyframe_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sldDockpathsKeyframe.ValueChanged
        Dim enable As Boolean = sldDockpathsKeyframe.Enabled

        ' Depending on whether this control is enable or not,
        ' enable\disable other UI controls.
        cmdDockpathsKeyframesInsert.Enabled = enable
        cmdDockpathsKeyframesRemove.Enabled = enable
        txtDockpathsKeyframePositionX.Enabled = enable
        txtDockpathsKeyframePositionY.Enabled = enable
        txtDockpathsKeyframePositionZ.Enabled = enable
        txtDockpathsKeyframeRotationX.Enabled = enable
        txtDockpathsKeyframeRotationY.Enabled = enable
        txtDockpathsKeyframeRotationZ.Enabled = enable
        txtDockpathsKeyframeTolerance.Enabled = enable
        txtDockpathsKeyframeMaxSpeed.Enabled = enable
        chkDockpathsKeyframeUseRotation.Enabled = enable
        chkDockpathsKeyframeDropFocus.Enabled = enable
        chkDockpathsKeyframeCheckRotation.Enabled = enable
        chkDockpathsKeyframeForceCloseBehaviour.Enabled = enable
        chkDockpathsKeyframePlayerInControl.Enabled = enable
        chkDockpathsKeyframeQueueOrigin.Enabled = enable
        chkDockpathsKeyframeUseClipPlane.Enabled = enable
        chkDockpathsKeyframeClearReservation.Enabled = enable

        ' See if this control is enabled or not.
        If sldDockpathsKeyframe.Enabled Then
            With m_HOD.Dockpaths(cstDockpaths.SelectedIndex)
                ' Update visibility.
                For I As Integer = 0 To .Dockpoints.Count - 1
                    .Dockpoints(I).Visible = (I = sldDockpathsKeyframe.Value)

                Next I ' For I As Integer = 0 To .Dockpoints.Count - 1

                With .Dockpoints(sldDockpathsKeyframe.Value)
                    ' Update text boxes.
                    txtDockpathsKeyframePositionX.Text = CStr(.Position.X)
                    txtDockpathsKeyframePositionY.Text = CStr(.Position.Y)
                    txtDockpathsKeyframePositionZ.Text = CStr(.Position.Z)
                    txtDockpathsKeyframeRotationX.Text = FormatNumber(180 * .Rotation.X / Math.PI, 3, TriState.True, TriState.False, TriState.False)
                    txtDockpathsKeyframeRotationY.Text = FormatNumber(180 * .Rotation.Y / Math.PI, 3, TriState.True, TriState.False, TriState.False)
                    txtDockpathsKeyframeRotationZ.Text = FormatNumber(180 * .Rotation.Z / Math.PI, 3, TriState.True, TriState.False, TriState.False)
                    txtDockpathsKeyframeTolerance.Text = CStr(.PointTolerance)
                    txtDockpathsKeyframeMaxSpeed.Text = CStr(.MaxSpeed)

                    ' Update checkboxes.
                    chkDockpathsKeyframeUseRotation.Checked = .UseRotation
                    chkDockpathsKeyframeDropFocus.Checked = .DropFocus
                    chkDockpathsKeyframeCheckRotation.Checked = .CheckRotation
                    chkDockpathsKeyframeForceCloseBehaviour.Checked = .ForceCloseBehavior
                    chkDockpathsKeyframePlayerInControl.Checked = .PlayerIsInControl
                    chkDockpathsKeyframeQueueOrigin.Checked = .QueueOrigin
                    chkDockpathsKeyframeUseClipPlane.Checked = .UseClipPlane
                    chkDockpathsKeyframeClearReservation.Checked = .ClearReservation

                End With ' With .Dockpoints(sldDockpathsKeyframe.Value)
            End With ' With m_HOD.Dockpaths(cstDockpaths.SelectedIndex)

        Else ' If sldDockpathsKeyframe.Enabled Then
            ' Update text boxes.
            txtDockpathsKeyframePositionX.Text = ""
            txtDockpathsKeyframePositionY.Text = ""
            txtDockpathsKeyframePositionZ.Text = ""
            txtDockpathsKeyframeRotationX.Text = ""
            txtDockpathsKeyframeRotationY.Text = ""
            txtDockpathsKeyframeRotationZ.Text = ""
            txtDockpathsKeyframeTolerance.Text = ""
            txtDockpathsKeyframeMaxSpeed.Text = ""

            ' Update checkboxes.
            chkDockpathsKeyframeUseRotation.Checked = False
            chkDockpathsKeyframeDropFocus.Checked = False
            chkDockpathsKeyframeCheckRotation.Checked = False
            chkDockpathsKeyframeForceCloseBehaviour.Checked = False
            chkDockpathsKeyframePlayerInControl.Checked = False
            chkDockpathsKeyframeQueueOrigin.Checked = False
            chkDockpathsKeyframeUseClipPlane.Checked = False
            chkDockpathsKeyframeClearReservation.Checked = False

        End If ' If sldDockpathsKeyframe.Enabled Then

    End Sub

    Private Sub cmdDockpathsKeyframesAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDockpathsKeyframesAdd.Click
        ' Add new dockpoint.
        m_HOD.Dockpaths(cstDockpaths.SelectedIndex).Dockpoints.Add(New Dockpoint)

        ' Update and enable control.
        sldDockpathsKeyframe.Enabled = True
        sldDockpathsKeyframe.Maximum = m_HOD.Dockpaths(cstDockpaths.SelectedIndex).Dockpoints.Count - 1
        sldDockpathsKeyframe.Value = sldDockpathsKeyframe.Maximum

        ' Fire the change event manually, since it may not have fired.
        sldDockpathsKeyframe_ValueChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cmdDockpathsKeyframesInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDockpathsKeyframesInsert.Click
        ' Insert new dockpoint.
        m_HOD.Dockpaths(cstDockpaths.SelectedIndex).Dockpoints.Insert(sldDockpathsKeyframe.Value, New Dockpoint)

        ' Update control.
        sldDockpathsKeyframe.Maximum = m_HOD.Dockpaths(cstDockpaths.SelectedIndex).Dockpoints.Count - 1
        sldDockpathsKeyframe_ValueChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cmdDockpathsKeyframesRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDockpathsKeyframesRemove.Click
        ' Remove dockpoint.
        m_HOD.Dockpaths(cstDockpaths.SelectedIndex).Dockpoints.RemoveAt(sldDockpathsKeyframe.Value)

        ' Update.
        cstDockpaths_SelectedIndexChanged(Nothing, EventArgs.Empty)

        ' Set new selected index.
        sldDockpathsKeyframe.Value = sldDockpathsKeyframe.Maximum

    End Sub

    Private Sub txtPositionRotationToleranceMaxSpeed_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtDockpathsKeyframePositionX.TextChanged, txtDockpathsKeyframePositionY.TextChanged, txtDockpathsKeyframePositionZ.TextChanged,
         txtDockpathsKeyframeRotationX.TextChanged, txtDockpathsKeyframeRotationY.TextChanged, txtDockpathsKeyframeRotationZ.TextChanged,
         txtDockpathsKeyframeTolerance.TextChanged, txtDockpathsKeyframeMaxSpeed.TextChanged

        ' Get the text-box.
        Dim textBox As TextBox = CType(sender, TextBox)

        ' See if it is focused.
        If Not textBox.Focused Then _
   Exit Sub

        ' See if any data has been entered.
        If textBox.Text = "" Then _
   Exit Sub

        ' See if numeric data was entered.
        If Not IsNumeric(textBox.Text) Then _
   Exit Sub

        ' Get the number.
        Dim number As Single = CSng(textBox.Text)
        Dim rotation As Single = CSng(Math.PI * number / 180)

        ' Get the dockpoint.
        Dim d As Dockpoint = m_HOD.Dockpaths(cstDockpaths.SelectedIndex).Dockpoints(sldDockpathsKeyframe.Value)

        ' Set data.
        If sender Is txtDockpathsKeyframePositionX Then _
   d.Position.X = number

        If sender Is txtDockpathsKeyframePositionY Then _
   d.Position.Y = number

        If sender Is txtDockpathsKeyframePositionZ Then _
   d.Position.Z = number

        If sender Is txtDockpathsKeyframeRotationX Then _
   d.Rotation.X = rotation

        If sender Is txtDockpathsKeyframeRotationY Then _
   d.Rotation.Y = rotation

        If sender Is txtDockpathsKeyframeRotationZ Then _
   d.Rotation.Z = rotation

        If sender Is txtDockpathsKeyframeTolerance Then _
   d.PointTolerance = number

        If sender Is txtDockpathsKeyframeMaxSpeed Then _
   d.MaxSpeed = number

    End Sub

    Private Sub txtPositionRotationToleranceMaxSpeed_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtDockpathsKeyframePositionX.Validated, txtDockpathsKeyframePositionY.Validated, txtDockpathsKeyframePositionZ.Validated,
         txtDockpathsKeyframeRotationX.Validated, txtDockpathsKeyframeRotationY.Validated, txtDockpathsKeyframeRotationZ.Validated,
         txtDockpathsKeyframeTolerance.Validated, txtDockpathsKeyframeMaxSpeed.Validated

        ' Get the text-box.
        Dim textBox As TextBox = CType(sender, TextBox)

        ' Set error.
        ErrorProvider.SetError(textBox, "")

    End Sub

    Private Sub chkDockpathsKeyframe_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles chkDockpathsKeyframeUseRotation.CheckedChanged, chkDockpathsKeyframeDropFocus.CheckedChanged,
         chkDockpathsKeyframeCheckRotation.CheckedChanged, chkDockpathsKeyframeForceCloseBehaviour.CheckedChanged,
         chkDockpathsKeyframePlayerInControl.CheckedChanged, chkDockpathsKeyframeQueueOrigin.CheckedChanged,
         chkDockpathsKeyframeUseClipPlane.CheckedChanged, chkDockpathsKeyframeClearReservation.CheckedChanged

        ' If the check box is not focused, do nothing.
        If Not CType(sender, CheckBox).Focused Then _
   Exit Sub

        ' Get whether it's checked or not.
        Dim check As Boolean = CType(sender, CheckBox).Checked

        ' Get the dockpoint.
        Dim d As Dockpoint = m_HOD.Dockpaths(cstDockpaths.SelectedIndex).Dockpoints(sldDockpathsKeyframe.Value)

        ' Set data.
        If sender Is chkDockpathsKeyframeUseRotation Then _
   d.UseRotation = check

        If sender Is chkDockpathsKeyframeDropFocus Then _
   d.DropFocus = check

        If sender Is chkDockpathsKeyframeCheckRotation Then _
   d.CheckRotation = check

        If sender Is chkDockpathsKeyframeForceCloseBehaviour Then _
   d.ForceCloseBehavior = check

        If sender Is chkDockpathsKeyframePlayerInControl Then _
   d.PlayerIsInControl = check

        If sender Is chkDockpathsKeyframeQueueOrigin Then _
   d.QueueOrigin = check

        If sender Is chkDockpathsKeyframeUseClipPlane Then _
   d.UseClipPlane = check

        If sender Is chkDockpathsKeyframeClearReservation Then _
   d.ClearReservation = check

    End Sub

#End Region

#Region " Level Lights UI "

    Private Sub tabLights_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabLights.Enter
        ' See if the tab enter event is marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        ' Update the checked list box.
        With cstLights.Items
            ' Clear old items.
            .Clear()

            ' Add all meshes.
            For I As Integer = 0 To m_HOD.Lights.Count - 1
                .Add(m_HOD.Lights(I).ToString(), m_HOD.Lights(I).Visible)

            Next I ' For I As Integer = 0 To m_HOD.lights.Count - 1

        End With ' With cstlights.Items

        ' Update selection.
        cstLights_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cstLights_Cut()
        ' See if any item is selected.
        If cstLights.SelectedIndex = -1 Then _
   Exit Sub

        ' Get it's index.
        Dim ind As Integer = cstLights.SelectedIndex

        ' Copy to clipboard.
        mnuEditClipboard = New Light(m_HOD.Lights(ind))

        ' Remove from HOD.
        m_HOD.Lights.RemoveAt(ind)

        ' Refresh.
        cstLights.Items.RemoveAt(ind)

    End Sub

    Private Sub cstLights_Copy()
        ' See if any item is selected.
        If cstLights.SelectedIndex = -1 Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New Light(m_HOD.Lights(cstLights.SelectedIndex))

    End Sub

    Private Sub cstLights_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is Light Then _
   Exit Sub

        ' Get the item.
        Dim l As Light = CType(mnuEditClipboard, Light)

        ' Remove reference.
        mnuEditClipboard = Nothing

        ' See if it's already present.
        If Not cstLights.Items.Contains(l.ToString()) Then _
   m_HOD.Lights.Add(l) _
        : cstLights.Items.Add(l.ToString(), l.Visible) _
        : Exit Sub

        ' Rename item.
        Dim number As Integer = 2

        Do
            ' See if this name is present.
            If cstLights.Items.Contains(l.ToString() & CStr(number)) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, add it.
            l.Name &= CStr(number)
            m_HOD.Lights.Add(l)
            cstLights.Items.Add(l.ToString(), l.Visible)

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cstLights_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles cstLights.ItemCheck
        ' Set the visible flag if the checked list box is focused.
        If (sender IsNot Nothing) AndAlso (CType(sender, CheckedListBox).Focused) Then _
   m_HOD.Lights(e.Index).Visible = (e.NewValue = CheckState.Checked)

    End Sub

    Private Sub cstLights_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cstLights.SelectedIndexChanged
        Dim enable As Boolean = (cstLights.SelectedIndex <> -1)

        ' Enable\Disable controls depending upon whether an item
        ' has been selected or not.
        cmdLightsRemove.Enabled = enable
        cmdLightsRename.Enabled = enable
        cboLightType.Enabled = enable
        cboLightAtt.Enabled = enable
        txtLightTX.Enabled = enable
        txtLightTY.Enabled = enable
        txtLightTZ.Enabled = enable
        txtLightCR.Enabled = enable
        txtLightCG.Enabled = enable
        txtLightCB.Enabled = enable
        txtLightSR.Enabled = enable
        txtLightSG.Enabled = enable
        txtLightSB.Enabled = enable
        txtLightAttDist.Enabled = enable

        If cstLights.SelectedIndex = -1 Then
            ' Update combo boxes.
            cboLightType.SelectedIndex = -1
            cboLightAtt.SelectedIndex = -1

            ' Update text boxes.
            txtLightTX.Text = ""
            txtLightTY.Text = ""
            txtLightTZ.Text = ""
            txtLightCR.Text = ""
            txtLightCG.Text = ""
            txtLightCB.Text = ""
            txtLightSR.Text = ""
            txtLightSG.Text = ""
            txtLightSB.Text = ""
            txtLightAttDist.Text = ""

        Else ' If cstLights.SelectedIndex = -1 Then
            With m_HOD.Lights(cstLights.SelectedIndex)
                Try
                    ' Update combo boxes.
                    cboLightType.SelectedIndex = .Type
                    cboLightAtt.SelectedIndex = .Attenuation

                Catch ex As Exception
                    ' Update combo boxes.
                    cboLightType.SelectedIndex = 0
                    cboLightAtt.SelectedIndex = 0

                    ' Reset data.
                    .Type = Light.LightType.Ambient
                    .Attenuation = Light.LightAttenuation.None

                    ' Inform user.
                    MsgBox("Error while setting type\attenuation." & vbCrLf &
            "They have been reset to default.", MsgBoxStyle.Critical, Me.Text)
                End Try

                ' Update text boxes.
                txtLightTX.Text = FormatNumber(.Transform.X, 3, TriState.True, TriState.False, TriState.False)
                txtLightTY.Text = FormatNumber(.Transform.Y, 3, TriState.True, TriState.False, TriState.False)
                txtLightTZ.Text = FormatNumber(.Transform.Z, 3, TriState.True, TriState.False, TriState.False)
                txtLightCR.Text = FormatNumber(.Colour.X, 3, TriState.True, TriState.False, TriState.False)
                txtLightCG.Text = FormatNumber(.Colour.Y, 3, TriState.True, TriState.False, TriState.False)
                txtLightCB.Text = FormatNumber(.Colour.Z, 3, TriState.True, TriState.False, TriState.False)
                txtLightSR.Text = FormatNumber(.Specular.X, 3, TriState.True, TriState.False, TriState.False)
                txtLightSG.Text = FormatNumber(.Specular.Y, 3, TriState.True, TriState.False, TriState.False)
                txtLightSB.Text = FormatNumber(.Specular.Z, 3, TriState.True, TriState.False, TriState.False)

                txtLightAttDist.Text = CStr(.AttenuationDistance)

            End With ' With m_HOD.Lights(cstLights.SelectedIndex)
        End If ' If cstLights.SelectedIndex = -1 Then

    End Sub

    Private Sub cmdLightsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLightsAdd.Click
        Dim number As Integer = cstLights.Items.Count + 1
        Dim name As String

        Do
            ' Decide a name for the mesh.
            name = "Light " & CStr(number)

            ' See if it's a duplicate.
            If _ListBoxHasString(cstLights.Items, name) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, make a new light mesh.
            Dim g As New Light With {.Name = name}

            ' Add it.
            m_HOD.Lights.Add(g)

            ' Add the mesh to the list.
            cstLights.Items.Add(g.ToString(), g.Visible)
            cstLights.SelectedIndex = cstLights.Items.Count - 1

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cmdLightsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLightsRemove.Click
        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Remove the mesh.
        m_HOD.Lights.RemoveAt(cstLights.SelectedIndex)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Update list.
        cstLights.Items.RemoveAt(cstLights.SelectedIndex)
        cstLights.SelectedIndex = cstLights.Items.Count - 1

    End Sub

    Private Sub cmdLightsRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLightsRename.Click
        Dim name As String

        ' Get old name.
        Dim oldName As String = m_HOD.Lights(cstLights.SelectedIndex).Name

        Do
            ' Get new name.
            name = InputBox("Enter new name for light: ", Me.Text, oldName)

            ' See if the user pressed cancel.
            If name = "" Then _
    Exit Sub

            ' HACK: If only casing is changed, then rename list item twice.
            ' Otherwise, the change doesn't seem to be reflected in the list.
            If String.Compare(oldName, name, True) = 0 Then _
    cstLights.Items(cstLights.SelectedIndex) = "" _
            : Exit Do

            ' See if it's a duplicate.
            ' For that, first rename the mesh (in list only) to something else
            ' so that it doesn't interfere in our search.
            cstLights.Items(cstLights.SelectedIndex) = ""

            ' Now see if it's a duplicate.
            If _ListBoxHasString(cstLights.Items, name) Then _
    cstLights.Items(cstLights.SelectedIndex) = oldName _
            : MsgBox("The name you entered is a duplicate!" & vbCrLf &
           "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
            : Continue Do

            ' Exit loop.
            Exit Do

        Loop ' Do

        ' Not a duplicate. OK to rename.
        m_HOD.Lights(cstLights.SelectedIndex).Name = name

        ' Update list.
        cstLights.Items(cstLights.SelectedIndex) = name

        ' Update UI
        cstLights_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cboLight_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles cboLightType.SelectedIndexChanged, cboLightAtt.SelectedIndexChanged

        If cstLights.SelectedIndex = -1 Then _
   Exit Sub

        ' Get the combo box.
        Dim comboBox As ComboBox = CType(sender, ComboBox)

        ' Set data.
        With m_HOD.Lights(cstLights.SelectedIndex)
            If sender Is cboLightType Then _
    .Type = CType(comboBox.SelectedIndex, Light.LightType)

            If sender Is cboLightAtt Then _
    .Attenuation = CType(comboBox.SelectedIndex, Light.LightAttenuation)

        End With ' With m_HOD.Lights(cstLights.SelectedIndex)

    End Sub

    Private Sub txtLight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtLightTX.TextChanged, txtLightTY.TextChanged, txtLightTZ.TextChanged,
         txtLightCR.TextChanged, txtLightCG.TextChanged, txtLightCB.TextChanged,
         txtLightSR.TextChanged, txtLightSG.TextChanged, txtLightSB.TextChanged,
         txtLightAttDist.TextChanged

        ' Get the text box.
        Dim textBox As TextBox = CType(sender, TextBox)

        ' See if it's focused.
        If Not textBox.Focused Then _
   Exit Sub

        ' See if there is any data.
        If textBox.Text = "" Then _
   Exit Sub

        ' See if it's numeric.
        If Not IsNumeric(textBox.Text) Then _
   Exit Sub

        ' Get the number.
        Dim number As Single = CSng(textBox.Text)

        ' Set required fields.
        With m_HOD.Lights(cstLights.SelectedIndex)
            If sender Is txtLightTX Then _
    .Transform = New Vector3(number, .Transform.Y, .Transform.Z)

            If sender Is txtLightTY Then _
    .Transform = New Vector3(.Transform.X, number, .Transform.Z)

            If sender Is txtLightTZ Then _
    .Transform = New Vector3(.Transform.X, .Transform.Y, number)

            If sender Is txtLightCR Then _
    .Colour = New Vector3(number, .Colour.Y, .Colour.Z)

            If sender Is txtLightCG Then _
    .Colour = New Vector3(.Colour.X, number, .Colour.Z)

            If sender Is txtLightCB Then _
    .Colour = New Vector3(.Colour.X, .Colour.Y, number)

            If sender Is txtLightSR Then _
    .Specular = New Vector3(number, .Specular.Y, .Specular.Z)

            If sender Is txtLightSG Then _
    .Specular = New Vector3(.Specular.X, number, .Specular.Z)

            If sender Is txtLightSB Then _
    .Specular = New Vector3(.Specular.X, .Specular.Y, number)

            If sender Is txtLightAttDist Then _
    .AttenuationDistance = number

        End With ' With m_HOD.Lights(cstLights.SelectedIndex)

    End Sub

    Private Sub txtLight_Validated(ByVal sender As Object, ByVal e As System.EventArgs) _
 Handles txtLightTX.Validated, txtLightTY.Validated, txtLightTZ.Validated,
         txtLightCR.Validated, txtLightCG.Validated, txtLightCB.Validated,
         txtLightSR.Validated, txtLightSG.Validated, txtLightSB.Validated,
         txtLightAttDist.Validated

        ' Get the text box.
        Dim textBox As TextBox = CType(sender, TextBox)

        ' Set error.
        ErrorProvider.SetError(textBox, "")

    End Sub

#End Region

#Region " Star Fields UI "

    ''' <summary>
    ''' Imports stars from a texture.
    ''' </summary>
    Private Function _MakeStarFieldFromTexture(ByVal stars As List(Of Star)) As Boolean
        ' Get path from user to save the texture.
        If OpenTextureFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Return False

        ' Create new texture.
        Dim T As Direct3D.Texture = Direct3D.TextureLoader.FromFile(m_D3DManager.Device, OpenTextureFileDialog.FileName)

        If (T.LevelCount = 0) OrElse (T.GetLevelDescription(0).Format <> Direct3D.Format.A8R8G8B8) Then _
   MsgBox("Invalid texture. Format must be A8R8G8B8.", MsgBoxStyle.Information, Me.Text) _
        : T.Dispose() _
        : Return False

        ' Get size.
        Dim w As Integer = T.GetLevelDescription(0).Width,
      h As Integer = T.GetLevelDescription(0).Height

        ' Lock it.
        Dim g As GraphicsStream = T.LockRectangle(0, Direct3D.LockFlags.None)

        ' Clear existing data.
        stars.Clear()

        ' Create all stars.
        For Y As Integer = 0 To h - 1
            For X As Integer = 0 To w - 1
                ' Read colour.
                Dim c As Direct3D.ColorValue = Direct3D.ColorValue.FromArgb(CType(g.Read(GetType(Int32)), Int32))

                ' Check alpha.
                If c.Alpha < 0.1 Then _
     Continue For

                ' Get angles.
                Dim theta As Single = CSng(Math.PI * (1 - 2 * X / (w - 1))),
        phi As Single = CSng(Math.PI * (0.5 - Y / (h - 1)))

                ' Get position.
                Dim P As New Vector3(CSng(Math.Cos(theta) * Math.Cos(phi)),
                         CSng(Math.Sin(phi)),
                         CSng(Math.Sin(theta) * Math.Cos(phi)))

                ' Set size.
                Dim size As Single = c.Alpha * 10

                ' Set colour.
                c.Alpha = 1.0F

                ' Add star.
                stars.Add(New Star With {.Position = 98 * P, .Size = size, .Colour = c})

            Next X ' For X As Integer = 0 To w - 1
        Next Y ' For Y As Integer = 0 To h - 1

        ' Unlock it and then dispose it.
        T.UnlockRectangle(0)
        T.Dispose()

        Return True

    End Function

    ''' <summary>
    ''' Returns the extents of all unprojected background meshes.
    ''' </summary>
    Private Sub _UnprojectedBackgroundBounds(ByVal min As List(Of Vector2), ByVal min3D As List(Of Vector3),
                                          ByVal max As List(Of Vector2), ByVal max3D As List(Of Vector3),
                                          ByVal valid As List(Of Boolean))

        ' Clear lists.
        min.Clear()
        max.Clear()
        min3D.Clear()
        max3D.Clear()
        valid.Clear()

        For I As Integer = 0 To m_HOD.BackgroundMeshes.Count - 1
            Dim minI, maxI As Vector3

            ' Update lists.
            min.Add(New Vector2)
            max.Add(New Vector2)
            min3D.Add(New Vector3)
            max3D.Add(New Vector3)
            valid.Add(False)

            ' Duplicate the mesh.
            Dim m As New BasicMesh(m_HOD.BackgroundMeshes(I))

            ' Skip part if needed.
            If m.PartCount = 0 Then _
    Continue For

            ' Merge all parts.
            m.MergeAll()

            ' Get extents.
            m.GetMeshExtents(minI, maxI)
            min3D(I) = minI
            max3D(I) = maxI

            ' Unproject all vertices.
            For J As Integer = 0 To m.Part(0).Vertices.Count - 1
                ' Get vertex.
                Dim vtx As BasicVertex = m.Part(0).Vertices(J)

                ' Get position.
                Dim P As Vector3 = vtx.Position

                ' Unproject.
                Dim V As New Vector2(0.5F * CSng(1 - Math.Atan2(P.Z, P.X) / Math.PI),
                         0.5F - CSng(Math.Atan2(P.Y, Math.Sqrt(P.X * P.X + P.Z * P.Z)) / Math.PI))

                ' Set position.
                vtx.Position = New Vector3(V.X, V.Y, 0)

                ' Set vertex.
                m.Part(0).Vertices(J) = vtx

            Next J ' For J As Integer = 0 To m.Part(0).Vertices.Count - 1

            ' Now get the bounds.
            valid(I) = m.GetMeshExtents(minI, maxI)
            min(I) = New Vector2(minI.X, minI.Y)
            max(I) = New Vector2(maxI.X, maxI.Y)

            ' Dump the mesh.
            m.RemoveAll()
            m = Nothing

        Next I ' For I As Integer = 0 To m_HOD.BackgroundMeshes.Count - 1

    End Sub

    ''' <summary>
    ''' Returns the star field group where a star should belong, depending on
    ''' the position of the star and number of background mesh groups.
    ''' </summary>
    Private Function _ClassifyStar(ByVal P As Vector3,
                                ByVal min As List(Of Vector2), ByVal min3D As List(Of Vector3),
                                ByVal max As List(Of Vector2), ByVal max3D As List(Of Vector3),
                                ByVal valid As List(Of Boolean)) As Integer

        ' See that lengths are equal.
        Trace.Assert(min.Count = max.Count)
        Trace.Assert(min.Count = min3D.Count)
        Trace.Assert(min.Count = max3D.Count)
        Trace.Assert(min.Count = valid.Count)

        ' Unproject star.
        Dim V As New Vector2(0.5F * CSng(1 - Math.Atan2(P.Z, P.X) / Math.PI),
                       0.5F - CSng(Math.Atan2(P.Y, Math.Sqrt(P.X * P.X + P.Z * P.Z)) / Math.PI))

        ' Check out each group.
        For I As Integer = 0 To min.Count - 1
            If Not valid(I) Then _
    Continue For

            If (min3D(I).X >= P.X) OrElse (min3D(I).Y >= P.Y) OrElse (min3D(I).Z >= P.Z) OrElse
      (P.X >= max3D(I).X) OrElse (P.Y >= max3D(I).Y) OrElse (P.Z >= max3D(I).Z) Then _
    Continue For

            If (min(I).X <= V.X) AndAlso (min(I).Y <= V.Y) AndAlso
      (V.X <= max(I).X) AndAlso (V.Y <= max(I).Y) Then _
    Return I

        Next I ' For I As Integer = 0 To min.Count - 1

        Debug.Print("Warning: Coudn't find appropriate group.")

        ' Since we coudn't find a good candidate, compare with all vertices.
        Dim dist As Single = 1.0E+30,
      closest As Integer = -1

        ' Get the vertex with minimum distance from this star.
        For I As Integer = 0 To m_HOD.BackgroundMeshes.Count - 1
            For J As Integer = 0 To m_HOD.BackgroundMeshes(I).PartCount - 1
                For K As Integer = 0 To m_HOD.BackgroundMeshes(I).Part(J).Vertices.Count - 1
                    ' Get distance from the vertex to this star.
                    Dim thisDist As Single = (m_HOD.BackgroundMeshes(I).Part(J).Vertices(K).Position - P).LengthSq()

                    ' Check distance and update closest distance if needed.
                    If dist > thisDist Then _
      dist = thisDist _
                    : closest = I

                Next K ' For K As Integer = 0 To m_HOD.BackgroundMeshes(I).Part(J).Vertices.Count - 1
            Next J ' For J As Integer = 0 To m_HOD.BackgroundMeshes(I).PartCount - 1
        Next I ' For I As Integer = 0 To m_HOD.BackgroundMeshes.Count - 1

        ' Return closest part.
        Return closest

    End Function

    ''' <summary>
    ''' Makes a texture to export star fields.
    ''' </summary>
    Private Function _MakeStarFieldTexture(ByVal stars As List(Of Star)) As Boolean
        Dim str As String
        Dim w, h As Integer

        Do
            ' Get width.
            str = InputBox("Enter width of output image:", Me.Text, CStr(2048))

            ' See if user pressed cancel.
            If str = "" Then _
    Return False

            ' Try to parse number.
            If Not Integer.TryParse(str, w) Then _
    MsgBox("Please enter a valid integer!", MsgBoxStyle.Information, Me.Text) _
            : Continue Do

            ' Check for +ve value.
            If w <= 0 Then _
    MsgBox("Please enter a positive value!", MsgBoxStyle.Information, Me.Text) _
            : Continue Do

            Exit Do

        Loop ' Do

        Do
            ' Get height.
            str = InputBox("Enter height of output image:", Me.Text, CStr(1024))

            ' See if user pressed cancel.
            If str = "" Then _
    Return False

            ' Try to parse number.
            If Not Integer.TryParse(str, h) Then _
    MsgBox("Please enter a valid integer!", MsgBoxStyle.Information, Me.Text) _
            : Continue Do

            ' Check for +ve value.
            If h <= 0 Then _
    MsgBox("Please enter a positive value!", MsgBoxStyle.Information, Me.Text) _
            : Continue Do

            Exit Do

        Loop ' Do

        SaveTextureFileDialog.Filter = "Truevision Targa files (*.tga)|*.tga"

        ' Get path from user to save the texture.
        If SaveTextureFileDialog.ShowDialog() = DialogResult.Cancel Then _
   Return False

        ' Create new texture.
        Dim T As New Direct3D.Texture(m_D3DManager.Device, w, h, 1, Direct3D.Usage.None,
                                Direct3D.Format.A8R8G8B8, Direct3D.Pool.SystemMemory)

        ' Lock it.
        Dim g As GraphicsStream = T.LockRectangle(0, Direct3D.LockFlags.None)

        ' Fill it with black.
        For I As Integer = 1 To w * h
            g.Write(CType(0, Int32))

        Next I ' For I As Integer = 1 To w * h

        ' Now unwrap stars and write to texture.
        For I As Integer = 0 To stars.Count - 1
            ' Get the star position.
            Dim P As Vector3 = stars(I).Position

            ' Unwrap it.
            Dim V As New Vector2(0.5F * CSng(1 - Math.Atan2(P.Z, P.X) / Math.PI),
                        0.5F - CSng(Math.Atan2(P.Y, Math.Sqrt(P.X * P.X + P.Z * P.Z)) / Math.PI))

            ' Get the pixel to write on.
            V.X *= w
            V.Y *= h

            ' Clamp.
            V.X = CInt(Math.Max(0, Math.Min(w - 1, V.X)))
            V.Y = CInt(Math.Max(0, Math.Min(h - 1, V.Y)))

            ' Get the colour.
            Dim c As Direct3D.ColorValue = stars(I).Colour

            ' Set alpha.
            c.Alpha = Math.Max(0, Math.Min(1, stars(I).Size / 10.0F))

            ' Write to output.
            g.Position = 4 * CInt(w * V.Y + V.X)
            g.Write(CType(c.ToArgb(), Int32))

        Next I ' For I As Integer = 0 To stars.Count - 1

        ' Unlock it.
        T.UnlockRectangle(0)

        ' Save it.
        Direct3D.TextureLoader.Save(SaveTextureFileDialog.FileName, Direct3D.ImageFileFormat.Tga, T)

        ' Dispose the texture.
        T.Dispose()

        Return True

    End Function

    Private Sub tabStarFields_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabStarFields.Enter
        ' See if the tab enter event is marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        ' Update the checked list box.
        With cstStarFields.Items
            ' Clear old items.
            .Clear()

            ' Add all star fields.
            For I As Integer = 0 To m_HOD.StarFields.Count - 1
                .Add("Star field group " & CStr(I + 1), m_HOD.StarFields(I).Visible)

            Next I ' For I As Integer = 0 To m_HOD.StarFields.Count - 1
        End With ' With cstStarFields.Items

        ' Update selection.
        cstStarFields_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cstStarFields_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles cstStarFields.ItemCheck
        If cstStarFields.Focused Then _
   m_HOD.StarFields(e.Index).Visible = (e.NewValue = CheckState.Checked)

    End Sub

    Private Sub cstStarFields_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cstStarFields.SelectedIndexChanged
        ' Decide whether to enable\disable all UI controls.
        Dim enable As Boolean = (cstStarFields.SelectedIndex <> -1)

        cmdStarFieldsRemove.Enabled = enable
        cmdStarFieldsAddStar.Enabled = enable
        cmdStarFieldsRemoveStar.Enabled = enable
        dgvStarfields.Enabled = enable

        ' Disable the grid view's updates and clear it.
        dgvStarfields.Tag = dgvStarfields
        dgvStarfields.Rows.Clear()
        cstStarFields.BeginUpdate()

        If enable Then
            ' Make a list of rows to be added.
            Dim rows(m_HOD.StarFields(cstStarFields.SelectedIndex).Count - 1) As DataGridViewRow

            For I As Integer = 0 To m_HOD.StarFields(cstStarFields.SelectedIndex).Count - 1
                ' Get the star.
                Dim s As Star = m_HOD.StarFields(cstStarFields.SelectedIndex)(I)

                ' Make new row.
                rows(I) = New DataGridViewRow

                ' Set data.
                rows(I).SetValues(New Object() {s.Position.X, s.Position.Y, s.Position.Z, s.Size,
                                    s.Colour.Red, s.Colour.Green, s.Colour.Blue})

            Next I ' For I As Integer = 0 To m_HOD.StarFields(cstStarFields.SelectedIndex).Count - 1

            ' Add all rows at once.
            dgvStarfields.Rows.AddRange(rows)

            ' Erase array.
            Erase rows

        End If ' If enable Then

        ' Enable updates.
        cstStarFields.EndUpdate()
        dgvStarfields.Tag = Nothing

    End Sub

    Private Sub cmdStarFieldsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStarFieldsAdd.Click
        Dim s As New StarField

        ' Add to HOD.
        m_HOD.StarFields.Add(s)

        ' Update list.
        cstStarFields.Items.Add("Star field group " & CStr(m_HOD.StarFields.Count), s.Visible)
        cstStarFields.SelectedIndex = cstStarFields.Items.Count - 1

    End Sub

    Private Sub cmdStarFieldsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStarFieldsRemove.Click
        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Remove the mesh.
        m_HOD.StarFields.RemoveAt(cstStarFields.SelectedIndex)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Update list.
        dgvStarfields.Tag = dgvStarfields
        cstStarFields.Items.RemoveAt(cstStarFields.SelectedIndex)
        cstStarFields.SelectedIndex = cstStarFields.Items.Count - 1
        dgvStarfields.Tag = Nothing

        For I As Integer = 0 To cstStarFields.Items.Count - 1
            cstStarFields.Items(I) = "Star field group " & CStr(I)

        Next I ' For I As Integer = 0 To cstStarFields.Items.Count - 1

    End Sub

    Private Sub cmdStarfieldsAddStar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStarFieldsAddStar.Click
        Dim s As New Star

        If cstStarFields.SelectedIndex = -1 Then _
   Exit Sub

        ' Make new row.
        Dim r As New DataGridViewRow

        ' Set data.
        r.SetValues(New Object() {s.Position.X, s.Position.Y, s.Position.Z, s.Size,
                            s.Colour.Red, s.Colour.Green, s.Colour.Blue})

        ' Add new star.
        If dgvStarfields.CurrentRow Is Nothing Then _
   dgvStarfields.Rows.Add(r) _
  Else _
   dgvStarfields.Rows.Insert(dgvStarfields.CurrentRow.Index, r)

    End Sub

    Private Sub cmdStarFieldsRemoveStar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStarFieldsRemoveStar.Click
        If cstStarFields.SelectedIndex = -1 Then _
   Exit Sub

        Dim rowList As New List(Of DataGridViewRow)

        ' Get the selected rows.
        For Each row As DataGridViewRow In dgvStarfields.SelectedRows
            rowList.Add(row)

        Next row ' For Each row As DataGridViewRow In dgvStarfields.SelectedRows

        ' Remove the current row, if none are selected.
        If (rowList.Count = 0) AndAlso (dgvStarfields.CurrentRow IsNot Nothing) Then _
   rowList.Add(dgvStarfields.CurrentRow)

        ' Inform if there's nothing to remove.
        If rowList.Count = 0 Then _
   MsgBox("No star to remove!", MsgBoxStyle.Information, Me.Text)

        ' Remove them.
        For Each row As DataGridViewRow In rowList
            dgvStarfields.Rows.Remove(row)

        Next row ' For Each row As DataGridViewRow In rowList

        ' Erase list.
        rowList.Clear()
        rowList = Nothing

    End Sub

    Private Sub cmdStarFieldsImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStarFieldsImport.Click
        Dim stars As New List(Of Star)

        ' Make star field from texture.
        If Not _MakeStarFieldFromTexture(stars) Then _
   Exit Sub

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Remove all star fields.
        m_HOD.StarFields.Clear()

        ' Add new star fields.
        For I As Integer = 0 To m_HOD.BackgroundMeshes.Count - 1
            m_HOD.StarFields.Add(New StarField)

        Next I ' For I As Integer = 0 To m_HOD.BackgroundMeshes.Count - 1

        ' Make a new group for unclassified stars.
        Dim misc As New StarField

        ' Prepare variables for mesh extents.
        Dim min, max As New List(Of Vector2)
        Dim min3D, max3D As New List(Of Vector3)
        Dim valid As New List(Of Boolean)

        ' Get extents of mesh.
        _UnprojectedBackgroundBounds(min, min3D, max, max3D, valid)

        ' Add each star now.
        For I As Integer = 0 To stars.Count - 1
            ' Get the part to put this star into.
            Dim J As Integer = _ClassifyStar(stars(I).Position, min, min3D, max, max3D, valid)

            ' Put it in.
            If J <> -1 Then _
    m_HOD.StarFields(J).Add(stars(I)) _
   Else _
    misc.Add(stars(I))

        Next I ' For I As Integer = 0 To stars.Count - 1

        ' Add the miscellaneous group if needed.
        If misc.Count <> 0 Then _
   m_HOD.StarFields.Add(misc) _
  Else _
   misc = Nothing

        ' Clear lists.
        min.Clear()
        max.Clear()
        min3D.Clear()
        max3D.Clear()
        stars.Clear()
        valid.Clear()

        min = Nothing
        max = Nothing
        min3D = Nothing
        max3D = Nothing
        stars = Nothing
        valid = Nothing

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Update UI.
        tabStarFields_Enter(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cmdStarFieldsExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStarFieldsExport.Click
        Dim L As New List(Of Star)

        ' Gather all stars in one list.
        For I As Integer = 0 To m_HOD.StarFields.Count - 1
            Dim L2 As New List(Of Star)

            ' Gather stars in this group.
            For J As Integer = 0 To m_HOD.StarFields(I).Count - 1
                L2.Add(m_HOD.StarFields(I)(J))

            Next J ' For J As Integer = 0 To m_HOD.StarFields(I).Count - 1

            ' Append this group to main list.
            L.AddRange(L2)

            ' Free list.
            L2.Clear()
            L2 = Nothing

        Next I ' For I As Integer = 0 To m_HOD.StarFields.Count - 1

        ' Export stars.
        _MakeStarFieldTexture(L)

        ' Free list.
        L.Clear()
        L = Nothing

    End Sub

    Private Sub dgvStarfields_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgvStarfields.CellValidating
        If (cstStarFields.SelectedIndex = -1) OrElse (e.RowIndex = -1) Then _
   Exit Sub

        ' Assume the validation fails.
        e.Cancel = True

        ' Get the string.
        Dim str As String = CStr(dgvStarfields(e.ColumnIndex, e.RowIndex).EditedFormattedValue)

        ' See if it's empty.
        If str = "" Then _
   Exit Sub

        ' See it it's not numeric.
        If Not IsNumeric(str) Then _
   Exit Sub

        ' Validation succeeded.
        e.Cancel = False

    End Sub

    Private Sub dgvStarfields_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvStarfields.CellValueChanged
        If (cstStarFields.SelectedIndex = -1) OrElse (e.RowIndex = -1) Then _
   Exit Sub

        ' Get the string.
        Dim str As String = CStr(dgvStarfields(e.ColumnIndex, e.RowIndex).EditedFormattedValue)

        ' See if it's empty.
        If str = "" Then _
   Exit Sub

        ' See it it's not numeric.
        If Not IsNumeric(str) Then _
   Exit Sub

        ' Get the number.
        Dim number As Single = CSng(str)

        ' Get the star.
        Dim s As Star = m_HOD.StarFields(cstStarFields.SelectedIndex)(e.RowIndex)

        ' Set data.
        Select Case e.ColumnIndex
            Case 0 ' Position X
                s.Position = New Vector3(number, s.Position.Y, s.Position.Z)

            Case 1 ' Position Y
                s.Position = New Vector3(s.Position.X, number, s.Position.Z)

            Case 2 ' Position Z
                s.Position = New Vector3(s.Position.X, s.Position.Y, number)

            Case 3 ' Size
                s.Size = number

            Case 4 ' Colour R
                s.Colour = New Direct3D.ColorValue(number, s.Colour.Green, s.Colour.Blue)

            Case 5 ' Colour G
                s.Colour = New Direct3D.ColorValue(s.Colour.Red, number, s.Colour.Blue)

            Case 6 ' Colour B
                s.Colour = New Direct3D.ColorValue(s.Colour.Red, s.Colour.Green, number)

            Case Else
                Debug.Assert(False, "Error in UI.")

        End Select ' Select Case e.ColumnIndex

        ' Set star.
        m_HOD.StarFields(cstStarFields.SelectedIndex)(e.RowIndex) = s

    End Sub

    Private Sub dgvStarfields_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles dgvStarfields.CellValueNeeded
        If (cstStarFields.SelectedIndex = -1) OrElse (e.RowIndex = -1) Then _
   e.Value = 0.0F _
        : Exit Sub

        If e.RowIndex >= m_HOD.StarFields(cstStarFields.SelectedIndex).Count Then _
   e.Value = 0.0F _
        : Exit Sub

        ' Get the star.
        Dim s As Star = m_HOD.StarFields(cstStarFields.SelectedIndex)(e.RowIndex)

        ' Set data.
        Select Case e.ColumnIndex
            Case 0 ' Position X
                e.Value = s.Position.X

            Case 1 ' Position Y
                e.Value = s.Position.Y

            Case 2 ' Position Z
                e.Value = s.Position.Z

            Case 3 ' Size
                e.Value = s.Size

            Case 4 ' Colour R
                e.Value = FormatNumber(s.Colour.Red, 3, TriState.True, TriState.False, TriState.False)

            Case 5 ' Colour G
                e.Value = FormatNumber(s.Colour.Green, 3, TriState.True, TriState.False, TriState.False)

            Case 6 ' Colour B
                e.Value = FormatNumber(s.Colour.Blue, 3, TriState.True, TriState.False, TriState.False)

            Case Else
                Debug.Assert(False, "Error in UI.")

        End Select ' Select Case e.ColumnIndex

    End Sub

    Private Sub dgvStarfields_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvStarfields.KeyUp
        ' Add new row.
        If e.KeyCode = Keys.Insert Then _
   cmdStarfieldsAddStar_Click(cmdStarFieldsAddStar, EventArgs.Empty) _
        : e.Handled = True

        ' Remove selected rows.
        If e.KeyCode = Keys.Delete Then _
   cmdStarFieldsRemoveStar_Click(cmdStarFieldsRemoveStar, EventArgs.Empty) _
        : e.Handled = True

    End Sub

    Private Sub dgvStarfields_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles dgvStarfields.RowsAdded
        ' See if the event is marked to be supressed...
        If dgvStarfields.Tag Is dgvStarfields Then _
   Exit Sub

        For I As Integer = 0 To e.RowCount - 1
            m_HOD.StarFields(cstStarFields.SelectedIndex).Insert(e.RowIndex + I, New Star)

        Next I ' For I As Integer = 0 To e.RowCount - 1

    End Sub

    Private Sub dgvStarfields_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvStarfields.RowsRemoved
        ' See if the event is marked to be supressed...
        If dgvStarfields.Tag Is dgvStarfields Then _
   Exit Sub

        ' Remove star.
        For I As Integer = e.RowCount - 1 To 0 Step -1
            m_HOD.StarFields(cstStarFields.SelectedIndex).Remove(e.RowIndex + I)

        Next I ' For I As Integer = e.RowCount - 1 To 0 Step -1

    End Sub

#End Region

#Region " Textured Star Fields UI "

    Private Sub tabStarFieldsT_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabStarFieldsT.Enter
        ' See if the tab enter event is marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        ' Update the checked list box.
        With cstStarFieldsT.Items
            ' Clear old items.
            .Clear()

            ' Add all star fields.
            For I As Integer = 0 To m_HOD.StarFieldsT.Count - 1
                .Add("Star field group " & CStr(I + 1), m_HOD.StarFieldsT(I).Visible)

            Next I ' For I As Integer = 0 To m_HOD.StarFieldsT.Count - 1
        End With ' With cstStarFieldsT.Items

        ' Update selection.
        cstStarFieldsT_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cstStarFieldsT_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles cstStarFieldsT.ItemCheck
        If cstStarFieldsT.Focused Then _
   m_HOD.StarFieldsT(e.Index).Visible = (e.NewValue = CheckState.Checked)

    End Sub

    Private Sub cstStarFieldsT_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cstStarFieldsT.SelectedIndexChanged
        ' Decide whether to enable\disable all UI controls.
        Dim enable As Boolean = (cstStarFieldsT.SelectedIndex <> -1)

        cmdStarFieldsTRemove.Enabled = enable
        cmdStarFieldsTAddStar.Enabled = enable
        cmdStarFieldsTRemoveStar.Enabled = enable
        cmdStarFieldsTImport.Enabled = enable
        cmdStarFieldsTExport.Enabled = enable
        txtStarFieldsTStarName.Enabled = enable
        dgvStarFieldsT.Enabled = enable

        ' Disable the grid view's updates and clear it.
        dgvStarFieldsT.Tag = dgvStarFieldsT
        dgvStarFieldsT.Rows.Clear()
        txtStarFieldsTStarName.Text = ""
        cstStarFieldsT.BeginUpdate()

        If enable Then
            ' Update text box.
            txtStarFieldsTStarName.Text = m_HOD.StarFieldsT(cstStarFieldsT.SelectedIndex).Texture

            ' Make a list of rows to be added.
            Dim rows(m_HOD.StarFieldsT(cstStarFieldsT.SelectedIndex).Count - 1) As DataGridViewRow

            For I As Integer = 0 To m_HOD.StarFieldsT(cstStarFieldsT.SelectedIndex).Count - 1
                ' Get the star.
                Dim s As Star = m_HOD.StarFieldsT(cstStarFieldsT.SelectedIndex)(I)

                ' Make new row.
                rows(I) = New DataGridViewRow

                ' Set data.
                rows(I).SetValues(New Object() {s.Position.X, s.Position.Y, s.Position.Z, s.Size,
                                    s.Colour.Red, s.Colour.Green, s.Colour.Blue})

            Next I ' For I As Integer = 0 To m_HOD.StarFieldsT(cstStarFieldsT.SelectedIndex).Count - 1

            ' Add all rows at once.
            dgvStarFieldsT.Rows.AddRange(rows)

            ' Erase array.
            Erase rows

        End If ' If enable Then

        ' Enable updates.
        cstStarFieldsT.EndUpdate()
        dgvStarFieldsT.Tag = Nothing

    End Sub

    Private Sub cmdStarFieldsTAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStarFieldsTAdd.Click
        Dim s As New StarFieldT

        ' Add to HOD.
        m_HOD.StarFieldsT.Add(s)

        ' Update list.
        cstStarFieldsT.Items.Add("Star field group " & CStr(m_HOD.StarFieldsT.Count), s.Visible)
        cstStarFieldsT.SelectedIndex = cstStarFieldsT.Items.Count - 1

    End Sub

    Private Sub cmdStarFieldsTRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStarFieldsTRemove.Click
        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Remove the mesh.
        m_HOD.StarFieldsT.RemoveAt(cstStarFieldsT.SelectedIndex)

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Update list.
        dgvStarFieldsT.Tag = dgvStarFieldsT
        cstStarFieldsT.Items.RemoveAt(cstStarFieldsT.SelectedIndex)
        cstStarFieldsT.SelectedIndex = cstStarFieldsT.Items.Count - 1
        dgvStarFieldsT.Tag = Nothing

        For I As Integer = 0 To cstStarFieldsT.Items.Count - 1
            cstStarFieldsT.Items(I) = "Star field group " & CStr(I)

        Next I ' For I As Integer = 0 To cstStarFieldsT.Items.Count - 1

    End Sub

    Private Sub cmdStarFieldsTAddStar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStarFieldsTAddStar.Click
        Dim s As New Star

        If cstStarFieldsT.SelectedIndex = -1 Then _
   Exit Sub

        ' Make new row.
        Dim r As New DataGridViewRow

        ' Set data.
        r.SetValues(New Object() {s.Position.X, s.Position.Y, s.Position.Z, s.Size,
                            s.Colour.Red, s.Colour.Green, s.Colour.Blue})

        ' Add new star.
        If dgvStarFieldsT.CurrentRow Is Nothing Then _
   dgvStarFieldsT.Rows.Add(r) _
  Else _
   dgvStarFieldsT.Rows.Insert(dgvStarFieldsT.CurrentRow.Index, r)

    End Sub

    Private Sub cmdStarFieldsTRemoveStar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStarFieldsTRemoveStar.Click
        If cstStarFieldsT.SelectedIndex = -1 Then _
   Exit Sub

        Dim rowList As New List(Of DataGridViewRow)

        ' Get the selected rows.
        For Each row As DataGridViewRow In dgvStarFieldsT.SelectedRows
            rowList.Add(row)

        Next row ' For Each row As DataGridViewRow In dgvStarFieldsT.SelectedRows

        ' Remove the current row, if none are selected.
        If (rowList.Count = 0) AndAlso (dgvStarFieldsT.CurrentRow IsNot Nothing) Then _
   rowList.Add(dgvStarFieldsT.CurrentRow)

        ' Inform if there's nothing to remove.
        If rowList.Count = 0 Then _
   MsgBox("No star to remove!", MsgBoxStyle.Information, Me.Text)

        ' Remove them.
        For Each row As DataGridViewRow In rowList
            dgvStarFieldsT.Rows.Remove(row)

        Next row ' For Each row As DataGridViewRow In rowList

        ' Erase list.
        rowList.Clear()
        rowList = Nothing

    End Sub

    Private Sub cmdStarFieldsTImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStarFieldsTImport.Click
        Dim stars As New List(Of Star)
        Dim starField As StarFieldT = m_HOD.StarFieldsT(cstStarFieldsT.SelectedIndex)

        ' Make star field from texture.
        If Not _MakeStarFieldFromTexture(stars) Then _
   Exit Sub

        ' Pause render.
        m_D3DManager.RenderLoopPause()

        ' Remove existing stars.
        starField.Clear()

        ' Add each star now.
        For I As Integer = 0 To stars.Count - 1
            starField.Add(stars(I))

        Next I ' For I As Integer = 0 To stars.Count - 1

        ' Resume render.
        m_D3DManager.RenderLoopResume()

        ' Update UI.
        cstStarFieldsT_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cmdStarFieldsTExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStarFieldsTExport.Click
        Dim L As New List(Of Star)
        Dim starField As StarFieldT = m_HOD.StarFieldsT(cstStarFieldsT.SelectedIndex)

        ' Gather stars in this group.
        For I As Integer = 0 To starField.Count - 1
            L.Add(starField(I))

        Next I ' For I As Integer = 0 To starField.Count - 1

        ' Export stars.
        _MakeStarFieldTexture(L)

        ' Free list.
        L.Clear()
        L = Nothing

    End Sub

    Private Sub txtStarFieldsTStarName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStarFieldsTStarName.TextChanged
        ' Update only if focused.
        If txtStarFieldsTStarName.Focused Then _
   m_HOD.StarFieldsT(cstStarFieldsT.SelectedIndex).Texture = txtStarFieldsTStarName.Text

    End Sub

    Private Sub dgvStarFieldsT_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgvStarFieldsT.CellValidating
        If (cstStarFieldsT.SelectedIndex = -1) OrElse (e.RowIndex = -1) Then _
   Exit Sub

        ' Assume the validation fails.
        e.Cancel = True

        ' Get the string.
        Dim str As String = CStr(dgvStarFieldsT(e.ColumnIndex, e.RowIndex).EditedFormattedValue)

        ' See if it's empty.
        If str = "" Then _
   Exit Sub

        ' See it it's not numeric.
        If Not IsNumeric(str) Then _
   Exit Sub

        ' Validation succeeded.
        e.Cancel = False

    End Sub

    Private Sub dgvStarFieldsT_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvStarFieldsT.CellValueChanged
        If (cstStarFieldsT.SelectedIndex = -1) OrElse (e.RowIndex = -1) Then _
   Exit Sub

        ' Get the string.
        Dim str As String = CStr(dgvStarFieldsT(e.ColumnIndex, e.RowIndex).EditedFormattedValue)

        ' See if it's empty.
        If str = "" Then _
   Exit Sub

        ' See it it's not numeric.
        If Not IsNumeric(str) Then _
   Exit Sub

        ' Get the number.
        Dim number As Single = CSng(str)

        ' Get the star.
        Dim s As Star = m_HOD.StarFieldsT(cstStarFieldsT.SelectedIndex)(e.RowIndex)

        ' Set data.
        Select Case e.ColumnIndex
            Case 0 ' Position X
                s.Position = New Vector3(number, s.Position.Y, s.Position.Z)

            Case 1 ' Position Y
                s.Position = New Vector3(s.Position.X, number, s.Position.Z)

            Case 2 ' Position Z
                s.Position = New Vector3(s.Position.X, s.Position.Y, number)

            Case 3 ' Size
                s.Size = number

            Case 4 ' Colour R
                s.Colour = New Direct3D.ColorValue(number, s.Colour.Green, s.Colour.Blue, s.Colour.Alpha)

            Case 5 ' Colour G
                s.Colour = New Direct3D.ColorValue(s.Colour.Red, number, s.Colour.Blue, s.Colour.Alpha)

            Case 6 ' Colour B
                s.Colour = New Direct3D.ColorValue(s.Colour.Red, s.Colour.Green, number, s.Colour.Alpha)

            Case 7 ' Colour A
                s.Colour = New Direct3D.ColorValue(s.Colour.Red, s.Colour.Green, s.Colour.Blue, number)

            Case Else
                Debug.Assert(False, "Error in UI.")

        End Select ' Select Case e.ColumnIndex

        ' Set star.
        m_HOD.StarFieldsT(cstStarFieldsT.SelectedIndex)(e.RowIndex) = s

    End Sub

    Private Sub dgvStarFieldsT_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles dgvStarFieldsT.CellValueNeeded
        If (cstStarFieldsT.SelectedIndex = -1) OrElse (e.RowIndex = -1) Then _
   e.Value = 0.0F _
        : Exit Sub

        If e.RowIndex >= m_HOD.StarFieldsT(cstStarFieldsT.SelectedIndex).Count Then _
   e.Value = 0.0F _
        : Exit Sub

        ' Get the star.
        Dim s As Star = m_HOD.StarFieldsT(cstStarFieldsT.SelectedIndex)(e.RowIndex)

        ' Set data.
        Select Case e.ColumnIndex
            Case 0 ' Position X
                e.Value = s.Position.X

            Case 1 ' Position Y
                e.Value = s.Position.Y

            Case 2 ' Position Z
                e.Value = s.Position.Z

            Case 3 ' Size
                e.Value = s.Size

            Case 4 ' Colour R
                e.Value = FormatNumber(s.Colour.Red, 3, TriState.True, TriState.False, TriState.False)

            Case 5 ' Colour G
                e.Value = FormatNumber(s.Colour.Green, 3, TriState.True, TriState.False, TriState.False)

            Case 6 ' Colour B
                e.Value = FormatNumber(s.Colour.Blue, 3, TriState.True, TriState.False, TriState.False)

            Case 7 ' Colour A
                e.Value = FormatNumber(s.Colour.Alpha, 3, TriState.True, TriState.False, TriState.False)

            Case Else
                Debug.Assert(False, "Error in UI.")

        End Select ' Select Case e.ColumnIndex

    End Sub

    Private Sub dgvStarFieldsT_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvStarFieldsT.KeyUp
        ' Add new row.
        If e.KeyCode = Keys.Insert Then _
   cmdStarFieldsTAddStar_Click(cmdStarFieldsTAddStar, EventArgs.Empty) _
        : e.Handled = True

        ' Remove selected rows.
        If e.KeyCode = Keys.Delete Then _
   cmdStarFieldsTRemoveStar_Click(cmdStarFieldsTRemoveStar, EventArgs.Empty) _
        : e.Handled = True

    End Sub

    Private Sub dgvStarFieldsT_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles dgvStarFieldsT.RowsAdded
        ' See if the event is marked to be supressed...
        If dgvStarFieldsT.Tag Is dgvStarFieldsT Then _
   Exit Sub

        For I As Integer = 0 To e.RowCount - 1
            m_HOD.StarFieldsT(cstStarFieldsT.SelectedIndex).Insert(e.RowIndex + I, New Star)

        Next I ' For I As Integer = 0 To e.RowCount - 1

    End Sub

    Private Sub dgvStarFieldsT_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvStarFieldsT.RowsRemoved
        ' See if the event is marked to be supressed...
        If dgvStarFieldsT.Tag Is dgvStarFieldsT Then _
   Exit Sub

        ' Remove star.
        For I As Integer = e.RowCount - 1 To 0 Step -1
            m_HOD.StarFieldsT(cstStarFieldsT.SelectedIndex).Remove(e.RowIndex + I)

        Next I ' For I As Integer = e.RowCount - 1 To 0 Step -1

    End Sub

#End Region

#Region " Animations UI "

    ''' <summary>
    ''' Plays an animation.
    ''' </summary>
    Private Sub _PlayAnimation(ByVal a As Animation)
        ' Update fields.
        m_MAD_CurrAnim = a
        m_MAD_Time = a.StartTime

        ' Update text.
        cmdAnimationsPlay.Invoke(CType(AddressOf __Rename, Action(Of Control, String)),
                           New Object() {cmdAnimationsPlay, "Stop"})

    End Sub

    ''' <summary>
    ''' Stops playing an animation.
    ''' </summary>
    Private Sub _StopAnimation()
        ' Update fields.
        m_MAD_CurrAnim = Nothing
        m_MAD_Time = 0

        ' Update text.
        cmdAnimationsPlay.Invoke(CType(AddressOf __Rename, Action(Of Control, String)),
                           New Object() {cmdAnimationsPlay, "Play"})

        ' Reset joints.
        m_MAD.Reset()

    End Sub

    ''' <summary>
    ''' Updates the animation.
    ''' </summary>
    Private Sub _UpdateAnimation()
        Static LastRenderTime As Double = Microsoft.VisualBasic.Timer

        ' Update animation if needed.
        If m_MAD_CurrAnim IsNot Nothing Then
            ' Get the time elapsed.
            Dim timeElapsed As Single = CSng(Microsoft.VisualBasic.Timer - LastRenderTime)

            ' Update time.
            m_MAD_Time += timeElapsed

            ' Update MAD animation.
            m_MAD_CurrAnim.Update(m_MAD_Time)

            ' Check time. Stop animation if needed.
            If m_MAD_Time > m_MAD_CurrAnim.EndTime Then _
    _StopAnimation()

        End If ' If m_MAD_CurrAnim IsNot Nothing Then

        ' Update last render time.
        LastRenderTime = Microsoft.VisualBasic.Timer

    End Sub

    ''' <summary>
    ''' Modifies the text property of a control.
    ''' </summary>
    Private Sub __Rename(ByVal ctl As Control, ByVal txt As String)
        ctl.Text = txt

    End Sub

    Private Sub tabAnimations_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabAnimations.Enter
        ' See if the tab enter event is marked to be supressed...
        If tabMain.Tag Is tabMain Then _
   Exit Sub

        ' Update the list box.
        With lstAnimations.Items
            ' Clear old items.
            .Clear()

            ' Add new items.
            For I As Integer = 0 To m_MAD.Animations.Count - 1
                .Add(m_MAD.Animations(I).ToString())

            Next I ' For I As Integer = 0 To m_MAD.Animations.Count - 1
        End With ' With lstAnimations.Items

        ' Update selection.
        lstAnimations_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub lstAnimations_Cut()
        ' See if any item is selected.
        If lstAnimations.SelectedIndex = -1 Then _
   Exit Sub

        ' Get it's index.
        Dim ind As Integer = lstAnimations.SelectedIndex

        ' Copy to clipboard.
        mnuEditClipboard = New Animation(m_MAD.Animations(ind))

        ' Remove from MADD.
        m_MAD.Animations.RemoveAt(ind)

        ' Refresh.
        lstAnimations.Items.RemoveAt(ind)

    End Sub

    Private Sub lstAnimations_Copy()
        ' See if any item is selected.
        If lstAnimations.SelectedIndex = -1 Then _
   Exit Sub

        ' Copy to clipboard.
        mnuEditClipboard = New Animation(m_MAD.Animations(lstAnimations.SelectedIndex))

    End Sub

    Private Sub lstAnimations_Paste()
        ' See if it is of correct type.
        If Not TypeOf mnuEditClipboard Is Animation Then _
   Exit Sub

        ' Get the item.
        Dim a As Animation = CType(mnuEditClipboard, Animation)

        ' Remove reference.
        mnuEditClipboard = Nothing

        ' See if it's already present.
        If Not lstAnimations.Items.Contains(a.ToString()) Then _
   m_MAD.Animations.Add(a) _
        : lstAnimations.Items.Add(a.ToString()) _
        : Exit Sub

        ' Rename item.
        Dim number As Integer = 2

        Do
            ' See if this name is present.
            If lstAnimations.Items.Contains(a.ToString() & CStr(number)) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, add it.
            a.Name &= CStr(number)
            m_MAD.Animations.Add(a)
            lstAnimations.Items.Add(a.ToString())

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub lstAnimations_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstAnimations.SelectedIndexChanged
        ' Enable\Disable UI depending upon whether an animation
        ' has been seleced or not.
        Dim enable As Boolean = (lstAnimations.SelectedIndex <> -1)

        ' Enable\Disable UI.
        cmdAnimationsRemove.Enabled = enable
        cmdAnimationsRename.Enabled = enable
        cmdAnimationsJointsAdd.Enabled = enable
        cmdAnimationsPlay.Enabled = enable
        txtAnimationsST.Enabled = enable
        txtAnimationsET.Enabled = enable
        txtAnimationsLST.Enabled = enable
        txtAnimationsLET.Enabled = enable

        lstAnimationsJoints.Items.Clear()

        If lstAnimations.SelectedIndex <> -1 Then
            ' Update text boxes.
            txtAnimationsST.Text = CStr(m_MAD.Animations(lstAnimations.SelectedIndex).StartTime)
            txtAnimationsET.Text = CStr(m_MAD.Animations(lstAnimations.SelectedIndex).EndTime)
            txtAnimationsLST.Text = CStr(m_MAD.Animations(lstAnimations.SelectedIndex).LoopStartTime)
            txtAnimationsLET.Text = CStr(m_MAD.Animations(lstAnimations.SelectedIndex).LoopEndTime)

            ' Update list box.
            For I As Integer = 0 To m_MAD.Animations(lstAnimations.SelectedIndex).Joints.Count - 1
                lstAnimationsJoints.Items.Add(m_MAD.Animations(lstAnimations.SelectedIndex).Joints(I).ToString())

            Next I ' For I As Integer = 0 To m_MAD.Animations(lstAnimations.SelectedIndex).Joints.Count - 1

        Else ' If lstAnimations.SelectedIndex <> -1 Then
            ' Update text boxes.
            txtAnimationsST.Text = ""
            txtAnimationsET.Text = ""
            txtAnimationsLST.Text = ""
            txtAnimationsLET.Text = ""

        End If ' If lstAnimations.SelectedIndex <> -1 Then

        ' Update selection.
        lstAnimationsJoints.SelectedIndex = -1
        lstAnimationsJoints_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cmdAnimationsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnimationsAdd.Click
        Dim number As Integer = lstAnimations.Items.Count + 1
        Dim name As String

        Do
            ' Decide a name for the animation.
            name = "Animation " & CStr(number)

            ' See if it's a duplicate.
            If _ListBoxHasString(lstAnimations.Items, name) Then _
    number += 1 _
            : Continue Do

            ' Since it's not a duplicate, make a new animation.
            Dim a As New Animation With {.Name = name}

            ' Add it.
            m_MAD.Animations.Add(a)

            ' Add the animation to the list.
            lstAnimations.Items.Add(a.ToString())
            lstAnimations.SelectedIndex = lstAnimations.Items.Count - 1

            ' Exit loop.
            Exit Do

        Loop ' Do

    End Sub

    Private Sub cmdAnimationsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnimationsRemove.Click
        ' Remove animation.
        m_MAD.Animations.RemoveAt(lstAnimations.SelectedIndex)

        ' Update list.
        lstAnimations.Items.RemoveAt(lstAnimations.SelectedIndex)
        lstAnimations.SelectedIndex = lstAnimations.Items.Count - 1

    End Sub

    Private Sub cmdAnimationsRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnimationsRename.Click
        Dim name As String

        ' Get old name.
        Dim oldName As String = m_MAD.Animations(lstAnimations.SelectedIndex).Name

        Do
            ' Get new name.
            name = InputBox("Enter new name for animation: ", Me.Text, oldName)

            ' See if the user pressed cancel.
            If name = "" Then _
    Exit Sub

            ' HACK: If only casing is changed, then rename list item twice.
            ' Otherwise, the change doesn't seem to be reflected in the list.
            If String.Compare(oldName, name, True) = 0 Then _
    lstAnimations.Items(lstAnimations.SelectedIndex) = "" _
            : Exit Do

            ' See if it's a duplicate.
            ' For that, first rename the animation (in list only) to something else
            ' so that it doesn't interfere in our search.
            lstAnimations.Items(lstAnimations.SelectedIndex) = ""

            ' Now see if it's a duplicate.
            If _ListBoxHasString(lstAnimations.Items, name) Then _
    lstAnimations.Items(lstAnimations.SelectedIndex) = oldName _
            : MsgBox("The name you entered is a duplicate!" & vbCrLf &
           "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
            : Continue Do

            ' Exit loop.
            Exit Do

        Loop ' Do

        ' Not a duplicate. OK to rename.
        m_MAD.Animations(lstAnimations.SelectedIndex).Name = name

        ' Update list.
        lstAnimations.Items(lstAnimations.SelectedIndex) = name

        ' Update UI
        lstAnimations_SelectedIndexChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub txtAnimations_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtAnimationsST.TextChanged, txtAnimationsET.TextChanged,
         txtAnimationsLST.TextChanged, txtAnimationsLET.TextChanged

        ' Get the text box.
        Dim textBox As TextBox = CType(sender, TextBox)

        ' See if it's focused.
        If Not textBox.Focused Then _
   Exit Sub

        ' See if there is any data.
        If textBox.Text = "" Then _
   Exit Sub

        ' See if it's numeric.
        If Not IsNumeric(textBox.Text) Then _
   Exit Sub

        ' Get the number.
        Dim number As Single = CSng(textBox.Text)

        ' Check number.
        If number < 0 Then _
   Exit Sub

        ' Set required fields.
        If sender Is txtAnimationsST Then _
   m_MAD.Animations(lstAnimations.SelectedIndex).StartTime = number

        If sender Is txtAnimationsET Then _
   m_MAD.Animations(lstAnimations.SelectedIndex).EndTime = number

        If sender Is txtAnimationsLST Then _
   m_MAD.Animations(lstAnimations.SelectedIndex).LoopStartTime = number

        If sender Is txtAnimationsLET Then _
   m_MAD.Animations(lstAnimations.SelectedIndex).LoopEndTime = number

    End Sub

    Private Sub txtAnimations_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtAnimationsST.Validated, txtAnimationsET.Validated,
         txtAnimationsLST.Validated, txtAnimationsLET.Validated

        ' Get the text box.
        Dim textBox As TextBox = CType(sender, TextBox)

        ' Set error.
        ErrorProvider.SetError(textBox, "")

    End Sub

    Private Sub lstAnimationsJoints_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstAnimationsJoints.SelectedIndexChanged
        ' Enable\Disable UI depending on whether a joint is selected or not.
        Dim enable As Boolean = (lstAnimationsJoints.SelectedIndex <> -1) AndAlso
                          (m_MAD.Animations(lstAnimations.SelectedIndex) _
                                .Joints(lstAnimationsJoints.SelectedIndex) _
                                .KeyframeCount <> 0)

        ' Enable\Disable UI.
        cmdAnimationsJointsRemove.Enabled = (lstAnimationsJoints.SelectedIndex <> -1)
        cmdAnimationsJointsAddKeyframe.Enabled = (lstAnimationsJoints.SelectedIndex <> -1)
        sldAnimationsTime.Enabled = enable

        If Not enable Then
            sldAnimationsTime.Value = 0
            sldAnimationsTime.Maximum = 0

        Else ' If Not enable Then
            sldAnimationsTime.Value = 0
            sldAnimationsTime.Maximum = m_MAD.Animations(lstAnimations.SelectedIndex) _
                                    .Joints(lstAnimationsJoints.SelectedIndex) _
                                    .KeyframeCount - 1

        End If ' If Not enable Then

        ' Force update of slider.
        sldAnimationsTime_ValueChanged(Nothing, EventArgs.Empty)

    End Sub

    Private Sub cmdAnimationsJointsAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnimationsJointsAdd.Click '
        ' Create a new form.
        Dim f As New JointSelector(m_HOD, m_MAD.Animations(lstAnimations.SelectedIndex))

        ' Show it. Update selection if needed.
        If f.ShowDialog() = DialogResult.OK Then _
   lstAnimations_SelectedIndexChanged(Nothing, EventArgs.Empty)

        ' Dispose.
        f.Dispose()

 End Sub

 Private Sub cmdAnimationsPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnimationsPlay.Click
  ' Play\stop animation.
  If m_MAD_CurrAnim Is Nothing Then _
   _PlayAnimation(m_MAD.Animations(lstAnimations.SelectedIndex)) _
  Else _
   _StopAnimation()

 End Sub

 Private Sub cmdAnimationsPlay_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnimationsPlay.LostFocus
  ' Stop animation.
  If m_MAD_CurrAnim IsNot Nothing Then _
   _StopAnimation()

 End Sub

 Private Sub cmdAnimationsJointsRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnimationsJointsRemove.Click
  ' Remove joint from animation.
  m_MAD.Animations(lstAnimations.SelectedIndex).Joints.RemoveAt(lstAnimationsJoints.SelectedIndex)

  ' Update list.
  lstAnimationsJoints.Items.RemoveAt(lstAnimations.SelectedIndex)
  lstAnimationsJoints.SelectedIndex = lstAnimationsJoints.Items.Count - 1

 End Sub

 Private Sub sldAnimationsTime_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sldAnimationsTime.ValueChanged
  ' Enable\Disable controls depending upon whether the slider
  ' is enabled or not.
  cmdAnimationsJointsRemoveKeyframe.Enabled = sldAnimationsTime.Enabled
  txtAnimationsJointsTime.Enabled = sldAnimationsTime.Enabled
  txtAnimationsJointsPX.Enabled = sldAnimationsTime.Enabled
  txtAnimationsJointsPY.Enabled = sldAnimationsTime.Enabled
  txtAnimationsJointsPZ.Enabled = sldAnimationsTime.Enabled
  txtAnimationsJointsRX.Enabled = sldAnimationsTime.Enabled
  txtAnimationsJointsRY.Enabled = sldAnimationsTime.Enabled
  txtAnimationsJointsRZ.Enabled = sldAnimationsTime.Enabled

  If sldAnimationsTime.Enabled Then
   ' Get the current keyframe.
   Dim k As Integer = sldAnimationsTime.Value

   ' Update text boxes.
   With m_MAD.Animations(lstAnimations.SelectedIndex).Joints(lstAnimationsJoints.SelectedIndex)
    txtAnimationsJointsTime.Text = FormatNumber(.KeyframeTime(k), 6, TriState.True, TriState.False, TriState.False)
    txtAnimationsJointsPX.Text = CStr(.KeyframePosition(k).X)
    txtAnimationsJointsPY.Text = CStr(.KeyframePosition(k).Y)
    txtAnimationsJointsPZ.Text = CStr(.KeyframePosition(k).Z)
    txtAnimationsJointsRX.Text = FormatNumber(180 * .KeyframeRotation(k).X / Math.PI, 3, TriState.True, TriState.False, TriState.False)
    txtAnimationsJointsRY.Text = FormatNumber(180 * .KeyframeRotation(k).Y / Math.PI, 3, TriState.True, TriState.False, TriState.False)
    txtAnimationsJointsRZ.Text = FormatNumber(180 * .KeyframeRotation(k).Z / Math.PI, 3, TriState.True, TriState.False, TriState.False)

    ' Update joints.
    m_MAD.Animations(lstAnimations.SelectedIndex).Update(CSng(.KeyframeTime(k)))

   End With ' With m_MAD.Animations(lstAnimations.SelectedIndex).Joints(lstAnimationsJoints.SelectedIndex)

  Else ' If sldAnimationsTime.Enabled Then
   ' Update text boxes.
   txtAnimationsJointsTime.Text = ""
   txtAnimationsJointsPX.Text = ""
   txtAnimationsJointsPY.Text = ""
   txtAnimationsJointsPZ.Text = ""
   txtAnimationsJointsRX.Text = ""
   txtAnimationsJointsRY.Text = ""
   txtAnimationsJointsRZ.Text = ""

   ' Update joints.
   m_MAD.Reset()

  End If ' If sldAnimationsTime.Enabled Then

 End Sub

 Private Sub cmdAnimationsJointsAddKeyframe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnimationsJointsAddKeyframe.Click
  Dim str As String
  Dim time As Double

  Do
   ' Get time from user.
   str = InputBox("Enter time for new keyframe:", Me.Text, "0")

   ' See if user pressed cancel.
   If str = "" Then _
    Exit Sub

   ' Check for a numeric value.
   If Not IsNumeric(str) Then _
    MsgBox("Please enter a numeric value!", MsgBoxStyle.Exclamation, Me.Text) _
  : Continue Do

   ' Convert.
   time = CDbl(str)

   ' Check for positive value.
   If time < 0 Then _
    MsgBox("Please enter a non-negative value!", MsgBoxStyle.Exclamation, Me.Text) _
  : Continue Do

   ' Check for duplicate time.
   With m_MAD.Animations(lstAnimations.SelectedIndex).Joints(lstAnimationsJoints.SelectedIndex)
    For I As Integer = 0 To .KeyframeCount - 1
     If .KeyframeTime(I) = time Then _
      MsgBox("A keyframe with this time already exists!" & vbCrLf & _
              "Please enter some other value for time.", MsgBoxStyle.Information, Me.Text) _
    : Continue Do

    Next I ' For I As Integer = 0 To .KeyframeCount - 1
   End With ' With m_MAD.Animations(lstAnimations.SelectedIndex).Joints(lstAnimationsJoints.SelectedIndex)

   ' All OK.
   Exit Do

  Loop ' Do

  ' Add the keyframe.
  With m_MAD.Animations(lstAnimations.SelectedIndex).Joints(lstAnimationsJoints.SelectedIndex)
   ' Add keyframe.
   .AddKeyframe(time)

   ' Update slider.
   sldAnimationsTime.Enabled = True
   sldAnimationsTime.Maximum = .KeyframeCount - 1

   For I As Integer = 0 To .KeyframeCount - 1
    If .KeyframeTime(I) = time Then _
     sldAnimationsTime.Value = I _
   : Exit For

   Next I ' For I As Integer = 0 To .KeyframeCount - 1
  End With ' With m_MAD.Animations(lstAnimations.SelectedIndex).Joints(lstAnimationsJoints.SelectedIndex)

  ' Force fire value changed event.
  sldAnimationsTime_ValueChanged(Nothing, EventArgs.Empty)

 End Sub

 Private Sub cmdAnimationsJointsRemoveKeyframe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnimationsJointsRemoveKeyframe.Click
  ' Remove the keyframe.
  With m_MAD.Animations(lstAnimations.SelectedIndex).Joints(lstAnimationsJoints.SelectedIndex)
   ' Remove the keyframe.
   .RemoveKeyframe(sldAnimationsTime.Value)

   ' Update slider.
   If .KeyframeCount = 0 Then _
    sldAnimationsTime.Value = 0 _
  : sldAnimationsTime.Maximum = 0 _
  : sldAnimationsTime.Enabled = False _
   Else _
    sldAnimationsTime.Value = .KeyframeCount - 1 _
  : sldAnimationsTime.Maximum = .KeyframeCount - 1

  End With ' With m_MAD.Animations(lstAnimations.SelectedIndex).Joints(lstAnimationsJoints.SelectedIndex)

  ' Force fire value changed event.
  sldAnimationsTime_ValueChanged(Nothing, EventArgs.Empty)

 End Sub

 Private Sub txtAnimationsJoints_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtAnimationsJointsTime.TextChanged, _
         txtAnimationsJointsPX.TextChanged, txtAnimationsJointsPY.TextChanged, txtAnimationsJointsPZ.TextChanged, _
         txtAnimationsJointsRX.TextChanged, txtAnimationsJointsRY.TextChanged, txtAnimationsJointsRZ.TextChanged

  ' Get the text box.
  Dim textBox As TextBox = CType(sender, TextBox)

  ' See if it's focused.
  If Not textBox.Focused Then _
   Exit Sub

  ' See if there is any data.
  If textBox.Text = "" Then _
   Exit Sub

  ' See if it's numeric.
  If Not IsNumeric(textBox.Text) Then _
   Exit Sub

  ' Get the number.
  Dim number As Single = CSng(textBox.Text)
  Dim rotation As Single = CSng(Math.PI * number / 180)

  ' Get the keyframe.
  Dim k As Integer = sldAnimationsTime.Value

  ' Set required fields.
  With m_MAD.Animations(lstAnimations.SelectedIndex).Joints(lstAnimationsJoints.SelectedIndex)

   If sender Is txtAnimationsJointsPX Then _
    .KeyframePosition(k) = New Vector3(number, .KeyframePosition(k).Y, .KeyframePosition(k).Z)

   If sender Is txtAnimationsJointsPY Then _
    .KeyframePosition(k) = New Vector3(.KeyframePosition(k).X, number, .KeyframePosition(k).Z)

   If sender Is txtAnimationsJointsPZ Then _
    .KeyframePosition(k) = New Vector3(.KeyframePosition(k).X, .KeyframePosition(k).Y, number)

   If sender Is txtAnimationsJointsRX Then _
    .KeyframeRotation(k) = New Vector3(rotation, .KeyframeRotation(k).Y, .KeyframeRotation(k).Z)

   If sender Is txtAnimationsJointsRY Then _
    .KeyframeRotation(k) = New Vector3(.KeyframeRotation(k).X, rotation, .KeyframeRotation(k).Z)

   If sender Is txtAnimationsJointsRZ Then _
    .KeyframeRotation(k) = New Vector3(.KeyframeRotation(k).X, .KeyframeRotation(k).Y, rotation)

   ' Update joints.
   m_MAD.Animations(lstAnimations.SelectedIndex).Update(CSng(.KeyframeTime(k)))

  End With ' With m_MAD.Animations(lstAnimations.SelectedIndex).Joints(lstAnimationsJoints.SelectedIndex)

 End Sub

 Private Sub txtAnimationsJoints_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtAnimationsJointsTime.Validated, _
         txtAnimationsJointsPX.Validated, txtAnimationsJointsPY.Validated, txtAnimationsJointsPZ.Validated, _
         txtAnimationsJointsRX.Validated, txtAnimationsJointsRY.Validated, txtAnimationsJointsRZ.Validated

  ' Get the text box.
  Dim textBox As TextBox = CType(sender, TextBox)

  ' Set error.
  ErrorProvider.SetError(textBox, "")

  With m_MAD.Animations(lstAnimations.SelectedIndex).Joints(lstAnimationsJoints.SelectedIndex)
   If sender Is txtAnimationsJointsTime Then
    ' Set time.
    .KeyframeTime(sldAnimationsTime.Value) = CDbl(txtAnimationsJointsTime.Text)

    ' Focus the slider and select the moved keyframe.
    For I As Integer = 0 To .KeyframeCount - 1
     If .KeyframeTime(I) = CDbl(txtAnimationsJointsTime.Text) Then _
      sldAnimationsTime.Focus() _
    : sldAnimationsTime.Value = I _
    : Exit For

    Next I ' For I As Integer = 0 To .KeyframeCount - 1
   End If ' If sender Is txtAnimationsJointsTime Then
  End With ' With m_MAD.Animations(lstAnimations.SelectedIndex).Joints(lstAnimationsJoints.SelectedIndex)

 End Sub

#End Region

End Class
