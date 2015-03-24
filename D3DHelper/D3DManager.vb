
''' <summary>
''' Class providing functionality for creating a Direct3D Device.
''' </summary>
Public NotInheritable Class D3DManager
    ''' <summary>Device.</summary>
    Private WithEvents m_Device As Device

    ''' <summary>Creation parameters.</summary>
    ''' <remarks>As of creation of device, that is.</remarks>
    Private m_CP As CreationParameters

    ''' <summary>Presentation parameters.</summary>
    ''' <remarks>As of creation of device, that is.</remarks>
    Private m_PP As PresentParameters

    ''' <summary>Whether we are rendering a scene or not.</summary>
    Private m_Rendering As Boolean

    ''' <summary>Whether we've paused rendering.</summary>
    Private m_Paused As Boolean

    ''' <summary>Whether the rendering thread has acknowledged the pause.</summary>
    Private m_PauseAck As Boolean

    ''' <summary>Reference to the window used.</summary>
    Private m_Window As Form

    ''' <summary>Initial border style of the form.</summary>
    Private m_WindowBorderStyle As FormBorderStyle

    ''' <summary>Initial window state of the form.</summary>
    Private m_WindowState As FormWindowState

    ''' <summary>The object for multi-threaded rendering.</summary>
    Private m_RenderThread As Threading.Thread

    ''' <summary>Whether or not to cause resize event when caused by device.</summary>
    Public Shared IsUsingResizeEventHandler As Boolean = False

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Class destructor.
    ''' </summary>
    Protected Overrides Sub Finalize()
        ' Release the device.
        ReleaseDevice()

        ' Call base destructor.
        MyBase.Finalize()
    End Sub

    ''' <summary>
    ''' Returns the device.
    ''' </summary>
    Public ReadOnly Property Device() As Device
        Get
            Return m_Device
        End Get
    End Property

    ''' <summary>
    ''' Creates the Direct3D Device.
    ''' </summary>
    ''' <param name="DeviceWindow">
    ''' The control\window to use.
    ''' </param>
    ''' <param name="ApplicationName">
    ''' The application name to use for saving\loading registry
    ''' settings. Specify blank (or <c>Nothing</c>) to use default.
    ''' </param>
    ''' <param name="LoadSettingsFromRegistry">
    ''' Whether to load settings from registry if they
    ''' are present.
    ''' </param>
    ''' <param name="SaveSettingsToRegistry">
    ''' Whether to save settings to registry if device
    ''' creation is successful.
    ''' </param>
    ''' <remarks>
    ''' This should be called only when the device has 
    ''' not been created. Also, if application name cannot
    ''' be determined then registry read\write is not done.
    ''' </remarks>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>DeviceWindow Is Nothing</c>.
    ''' </exception>
    ''' <exception cref="DeviceAlreadyCreatedException">
    ''' Thrown when the device has already been created and this
    ''' subroutine is called.
    ''' </exception> 
    Public Sub CreateDevice(ByVal DeviceWindow As Control,
                            Optional ByVal ApplicationName As String = "",
                            Optional ByVal LoadSettingsFromRegistry As Boolean = True,
                            Optional ByVal SaveSettingsToRegistry As Boolean = True)

        Dim DCF As DeviceCreationFlags

        ' -------------
        ' Check inputs.
        ' -------------
        If DeviceWindow Is Nothing Then _
            Throw New ArgumentNullException("Window") _
                : Exit Sub

        ' Try to get the application name if not given.
        If (ApplicationName Is Nothing) OrElse (ApplicationName.Length = 0) Then _
            ApplicationName = D3DConfigurer.AppNameFull

        ' Disable registry read\write since we still do not have a name.
        If (ApplicationName Is Nothing) OrElse (ApplicationName.Length = 0) Then _
            LoadSettingsFromRegistry = False _
                : SaveSettingsToRegistry = False

        ' Check for redundant calls.
        If m_Device IsNot Nothing Then _
            Throw New DeviceAlreadyCreatedException _
                : Exit Sub

        ' --------------
        ' Load settings.
        ' --------------
        m_CP.Adapter = LoadAdapter(ApplicationName)
        m_CP.DeviceType = LoadDeviceType(ApplicationName)

        ' -------------------------------
        ' Create presentation parameters.
        ' -------------------------------
        ' If we are to load settings from registry, and the settings exist,
        ' then we can load settings from registry. Otherwise, not.
        If (LoadSettingsFromRegistry) AndAlso
           (SettingsExists(ApplicationName)) Then
            ' Load settings from registry.
            m_CP.CreateFlags = LoadCreateFlags(ApplicationName)
            m_PP = LoadPresentParameters(ApplicationName)

        Else ' If (LoadSettingsFromRegistry) AndAlso (SettingsExists(ApplicationName)) Then
            ' Build creation and presentation paramters.
            m_CP.CreateFlags = D3DConfigurer.BuildCreationFlags(m_CP.Adapter, m_CP.DeviceType)
            m_PP = D3DConfigurer.BuildPresentParameters(m_CP.Adapter, m_CP.DeviceType)

        End If ' If (LoadSettingsFromRegistry) AndAlso (SettingsExists(ApplicationName)) Then

        ' Set the device window.
        m_PP.DeviceWindow = DeviceWindow

        ' Create the device.
        m_Device = New Device(m_CP.Adapter,
                              m_CP.DeviceType,
                              m_PP.DeviceWindow,
                              m_CP.CreateFlags,
                              m_PP)

        ' ------------------
        ' Save the settings.
        ' ------------------
        If SaveSettingsToRegistry Then _
            SaveAdapter(ApplicationName, m_CP.Adapter) _
                : SaveDeviceType(ApplicationName, m_CP.DeviceType) _
                : SaveCreateFlags(ApplicationName, m_CP.CreateFlags) _
                : SavePresentationParameters(ApplicationName, m_PP)

        ' ----------------------------------
        ' Prepare the device creation flags.
        ' ----------------------------------  
        With m_PP
            DCF.Width = .BackBufferWidth
            DCF.Height = .BackBufferHeight
            DCF.BackBufferCount = .BackBufferCount

            DCF.DepthBuffer = ((.EnableAutoDepthStencil) AndAlso
                               (FormatDepthBitCount(.AutoDepthStencilFormat) > 0))

            DCF.StencilBuffer = ((.EnableAutoDepthStencil) AndAlso
                                 (FormatStencilBitCount(.AutoDepthStencilFormat) > 0))

            DCF.Multisampling = (.MultiSample <> MultiSampleType.None)

        End With ' With DCF

        ' -----------------------
        ' Set internal variables.
        ' -----------------------
        m_Rendering = False
        m_Paused = False

        ' Set the window reference.
        If TypeOf DeviceWindow Is Form Then _
            m_Window = CType(DeviceWindow, Form) _
            Else _
            m_Window = DeviceWindow.FindForm()

        ' Cache some states.
        m_WindowBorderStyle = m_Window.FormBorderStyle
        m_WindowState = m_Window.WindowState

        ' -------------------------------
        ' Raise the device created event.
        ' -------------------------------
        RaiseEvent DeviceCreated(DCF)
    End Sub

    ''' <summary>
    ''' Creates the Direct3D Device.
    ''' </summary>
    ''' <param name="DeviceWindowHandle">
    ''' The control\window handle to use.
    ''' </param>
    ''' <param name="ApplicationName">
    ''' The application name to use for saving\loading registry
    ''' settings. Specify blank (or <c>Nothing</c>) to use default.
    ''' </param>
    ''' <param name="LoadSettingsFromRegistry">
    ''' Whether to load settings from registry if they
    ''' are present.
    ''' </param>
    ''' <param name="SaveSettingsToRegistry">
    ''' Whether to save settings to registry if device
    ''' creation is successful.
    ''' </param>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>Form Is Nothing</c>.
    ''' </exception>
    ''' <exception cref="ArgumentException">
    ''' Thrown when the <c>DeviceWindowHandle</c> is invalid
    ''' (Null or doesn't point to a control).
    ''' </exception>
    ''' <exception cref="DeviceAlreadyCreatedException">
    ''' Thrown when the device has already been created and this
    ''' subroutine is called.
    ''' </exception>
    ''' <remarks>
    ''' This should be called only when the device has 
    ''' not been created. Also, if application name cannot
    ''' be determined then registry read\write is not done.
    ''' </remarks>
    Public Sub CreateDevice(ByVal DeviceWindowHandle As IntPtr,
                            Optional ByVal ApplicationName As String = "",
                            Optional ByVal LoadSettingsFromRegistry As Boolean = True,
                            Optional ByVal SaveSettingsToRegistry As Boolean = True)
        Dim ctl As Control

        ' Get the control.
        ctl = Control.FromHandle(DeviceWindowHandle)

        ' Call the other function.
        CreateDevice(ctl, ApplicationName, LoadSettingsFromRegistry, SaveSettingsToRegistry)
    End Sub

    ''' <summary>
    ''' Tries to reset the device if it is lost.
    ''' </summary>
    ''' <returns>
    ''' <c>False</c> if device is reset successfully;
    ''' <c>True</c> if device is lost (and cannot be reset).
    ''' </returns>
    Private Function ResetDeviceIfLost() As Boolean
        Dim LostDevice As Boolean

        ' Time to sleep if device is lost.
        Const DeviceLostSleepTime As Integer = 100

        ' Try to check the device's status. Since this method always throws
        ' exceptions if lost, or not reset, it is enclosed in a try block.
        Try
            ' Test co-operative level.
            m_Window.Invoke(CType(AddressOf m_Device.TestCooperativeLevel, Action))

        Catch ex As Exception
            Select Case ex.GetType.Name
                ' DEVICE LOST
                Case GetType(DeviceLostException).Name
                    ' Wait for some time (otherwise it seems that the 
                    ' application is frozen).
                    Threading.Thread.Sleep(DeviceLostSleepTime)

                    ' Yield process. This is needed so that the window
                    ' can be restored if clicked on (otherwise it seems
                    ' that the window is frozen).
                    Application.DoEvents()

                    ' Device in lost state.
                    LostDevice = True

                    ' DEVICE LOST NOT RESET
                Case GetType(DeviceNotResetException).Name
                    ' Reset the device. Note that this method
                    ' can still "lose" the device.
                    Try
                        ' Reset.
                        m_Window.Invoke(
                            CType(AddressOf m_Device.Reset, Action(Of PresentParameters)),
                            New Object() {m_PP}
                            )

                    Catch ex2 As DeviceLostException
                        ' Could not reset the device.
                        LostDevice = True

                    End Try

                    ' Reset succeeded.
                    LostDevice = False

                    ' UNKNOWN (error)
                Case Else
                    ' Since this was not expected, throw it.
                    Throw ex

            End Select ' Select Case ex.GetType.Name

        End Try

        ' Return status.
        Return LostDevice
    End Function

    ''' <summary>
    ''' Renders a single frame.
    ''' </summary>
    ''' <param name="DoEvents">
    ''' Whether to call <c>System.Windows.Forms.Application.DoEvents()</c>.
    ''' This may be needed to prevent the UI from freezing.
    ''' </param>
    Private Sub RenderFrame(ByVal DoEvents As Boolean)
        Dim SleepTime As Integer = 0

        ' Maximum frame-rate if window is not focused.
        Const MaxInactiveFrameRate As Integer = 15

        ' Decide the sleep time.
        If Windows.Forms.Form.ActiveForm IsNot m_Window Then _
            SleepTime = 1000\MaxInactiveFrameRate

        ' Raise the render event if the device exists and is not disposed.
        ' Otherwise, stop. If we've paused, then do not raise render event.
        If (m_Device Is Nothing) OrElse (m_Device.Disposed) Then _
            RenderLoopStop() _
            Else _
            If Not m_Paused Then _
                RaiseEvent Render()

        m_PauseAck = m_Paused

        ' Yield execution.
        If SleepTime <> 0 Then _
            Threading.Thread.Sleep(SleepTime)

        ' Yield execution.
        If DoEvents Then _
            Application.DoEvents()
    End Sub

    ''' <summary>
    ''' Renders frames continuously.
    ''' </summary>
    ''' <param name="DoEvents">
    ''' Whether to call <c>System.Windows.Forms.Application.DoEvents()</c>.
    ''' This may be needed to prevent the UI from freezing.
    ''' </param>
    Private Sub RenderLoop(Optional ByVal DoEvents As Boolean = True)
        Dim LostDevice As Boolean

        ' Window state must be set to normal; otherwise mouse clicks
        ' somehow hit the window below in full screen mode.
        m_Window.Invoke(CType(AddressOf ___AssignWindowStatesA, Action))

        ' Check if device exists or if a cancel request is pending
        ' In that case cancel rendering now.
        Do While (m_Rendering) AndAlso (m_Device IsNot Nothing)
            ' Render in try block to catch lost device exceptions.
            Try
                ' Check if the device is lost:
                ' -> If lost, then try to reset the device.
                ' -> If not lost, then do normal routine.
                If LostDevice Then _
                    LostDevice = ResetDeviceIfLost() _
                    Else _
                    RenderFrame(DoEvents)

            Catch ex As DeviceLostException
                ' Set the lost flag.
                LostDevice = True

            Catch ex As DriverInternalErrorException
                ' Stop rendering.
                RenderLoopStop()

                ' Warning message.
                Trace.Assert(False, "Warning: Encountered " &
                                    "'DriverInternalErrorException'.")

                ' Stop rendering.
                Exit Sub
            End Try ' Try
        Loop ' Do While (m_Rendering) AndAlso (m_Device IsNot Nothing)
    End Sub

    ''' <summary>
    ''' Begins rendering loop.
    ''' </summary>
    ''' <exception cref="DeviceNotCreatedException">
    ''' Thrown when the device has not been created yet.
    ''' </exception>
    Public Sub RenderLoopBegin()
        ' Check if device exists.
        If m_Device Is Nothing Then _
            Throw New DeviceNotCreatedException _
                : Exit Sub

        ' Check for redundant calls.
        If m_Rendering Then _
            Exit Sub

        ' Now we are rendering the scene.
        m_Rendering = True
        m_Paused = False

        ' Render while we're rendering.
        m_RenderThread = New Threading.Thread(AddressOf RenderLoop)
        m_RenderThread.IsBackground = True
        m_RenderThread.Priority = Threading.ThreadPriority.BelowNormal
        m_RenderThread.Start()
    End Sub

    ''' <summary>
    ''' Pauses render loop.
    ''' </summary>
    ''' <remarks>
    ''' This has no effect when not rendering, or when the device has
    ''' not been created.
    ''' </remarks>
    Public Sub RenderLoopPause()
        ' Check if device exists.
        If m_Device Is Nothing Then _
            Exit Sub

        ' Check if rendering.
        If Not m_Rendering Then _
            Exit Sub

        m_PauseAck = False
        m_Paused = True

        Do Until m_PauseAck
            Threading.Thread.Sleep(0)

        Loop ' Do Until m_PauseAck
    End Sub

    ''' <summary>
    ''' Resumes rendering paused using <c>RenderLoopPause()</c>.
    ''' </summary>
    ''' <remarks>
    ''' This has no effect when not rendering.
    ''' </remarks>
    ''' <exception cref="DeviceNotCreatedException">
    ''' Thrown when the device has not been created yet.
    ''' </exception>
    Public Sub RenderLoopResume()
        ' Check if device exists.
        If m_Device Is Nothing Then _
            Throw New DeviceNotCreatedException _
                : Exit Sub

        ' Check if rendering.
        If Not m_Rendering Then _
            Exit Sub

        m_PauseAck = True
        m_Paused = False

        Do While m_PauseAck
            Threading.Thread.Sleep(0)

        Loop ' Do While m_PauseAck
    End Sub

    ''' <summary>
    ''' Stops rendering.
    ''' </summary>
    ''' <exception cref="DeviceNotCreatedException">
    ''' Thrown when the device has not been created yet.
    ''' </exception>
    ''' <remarks>
    ''' If the thread doesn't stop, it is terminated by throwing an
    ''' exception. Hence, make sure 'Device.EndScene()' is always called
    '''  whenever 'Device.BeginScene()' is called, if, rendering will be
    ''' started again, without resetting the device.
    ''' </remarks>
    Public Sub RenderLoopStop()
        ' Check if device exists.
        If m_Device Is Nothing Then _
            Throw New DeviceNotCreatedException _
                : Exit Sub

        ' Check for redundant calls.
        If Not m_Rendering Then _
            Exit Sub

        ' Now we're not rendering.
        m_Rendering = False
        m_Paused = False

        ' Wait for thread to exit.
        If m_RenderThread IsNot Nothing Then _
            m_RenderThread.Join(100)

        ' Terminate thread if needed.
        If m_RenderThread.IsAlive Then _
            Trace.TraceWarning("Render thread was terminated.") _
                : m_RenderThread.Abort()

        ' Remove reference to thread.
        m_RenderThread = Nothing
    End Sub

    ''' <summary>
    ''' Goes to windowed mode.
    ''' </summary>
    ''' <remarks>
    ''' Do not call in any of the device events when multi-threading,
    ''' otherwise this could fail.
    ''' </remarks>
    Public Sub ToWindowed()
        Dim Rendering As Boolean = m_Rendering

        ' Check device.
        If m_Device Is Nothing Then _
            Throw New InvalidCallException("'Device not created yet.") _
                : Exit Sub

        ' Check if already in windowed mode.
        If m_Device.PresentationParameters.Windowed Then _
            Exit Sub

        With m_PP
            .FullScreenRefreshRateInHz = 0
            .PresentationInterval = PresentInterval.Default
            .Windowed = True
        End With ' With m_PP

        ' Stop rendering if needed.
        If m_Rendering Then _
            RenderLoopStop()

        ' Window state must be set to normal; otherwise mouse clicks
        ' somehow hit the window below in full screen mode.
        m_Window.Invoke(CType(AddressOf ___AssignWindowStatesA, Action))

        ' Now reset the device.
        m_Device.Reset(m_PP)

        ' Finally begin rendering if needed.
        RenderLoopBegin()
    End Sub

    ''' <summary>
    ''' Goes to fullscreen mode.
    ''' </summary>
    Public Sub ToFullscreen()
        Dim PP As PresentParameters
        Dim Rendering As Boolean = m_Rendering

        ' Check device.
        If m_Device Is Nothing Then _
            Throw New InvalidCallException("'Device not created yet.") _
                : Exit Sub

        ' Check if already in full screen mode.
        If Not m_Device.PresentationParameters.Windowed Then _
            Exit Sub

        ' Load settings from registry.
        PP = Registry.LoadPresentParameters(D3DConfigurer.AppNameFull)

        With m_PP
            ' Check if settings loaded from registry say we're in windowed. Otherwise, copy the needed settings.
            If PP.Windowed Then _
                .FullScreenRefreshRateInHz = GetMaxRefreshRate(m_CP.Adapter,
                                                               .BackBufferWidth,
                                                               .BackBufferHeight,
                                                               Manager.Adapters(m_CP.Adapter).CurrentDisplayMode.Format) _
                    : .PresentationInterval = PresentInterval.Default _
                Else _
                .FullScreenRefreshRateInHz = PP.FullScreenRefreshRateInHz _
                    : .PresentationInterval = PP.PresentationInterval

            .Windowed = False

            ' Remove reference.
            PP = Nothing

        End With ' With m_PP

        ' Stop rendering if needed.
        If m_Rendering Then _
            RenderLoopStop()

        ' Window state must be set to normal; otherwise mouse clicks
        ' somehow hit the window below in full screen mode.
        m_Window.Invoke(CType(AddressOf ___AssignWindowStatesA, Action))

        ' Now reset the device.
        m_Device.Reset(m_PP)

        ' Begin rendering if needed.
        RenderLoopBegin()
    End Sub

    ''' <summary>
    ''' Sets up texture filtering to linear or anisotropic, whichever is available.
    ''' If anisotropic filtering is available, highest is set.
    ''' </summary>
    ''' <param name="NumTextureStages">
    ''' Number of texture stages to set.
    ''' </param>
    ''' <remarks>
    ''' If called with an argument of 0, then this sets all stages.
    ''' </remarks>
    ''' <exception cref="InvalidCallException">
    ''' Thrown when device has not been created.
    ''' </exception>
    ''' <exception cref="ArgumentException">
    ''' Thrown when <c>NumTextureStages</c> is negative.
    ''' </exception>
    Public Sub SetupTextureFilteringModes(Optional ByVal NumTextureStages As Integer = 0)
        ' Check device.
        If m_Device Is Nothing Then _
            Throw New InvalidCallException("'Device not created yet.") _
                : Exit Sub

        ' Get device caps.
        Dim Caps As Direct3D.Caps = Manager.GetDeviceCaps(0, DeviceType.Hardware)

        ' Check argument.
        If (NumTextureStages < 0) OrElse
           (NumTextureStages > Caps.MaxTextureBlendStages) Then _
            Throw New ArgumentException("Invalid 'NumTextureStages'.") _
                : Exit Sub

        ' Modify argument if needed.
        If NumTextureStages = 0 Then _
            NumTextureStages = Caps.MaxTextureBlendStages

        ' Decide the filters and anisotropy, if applicable.
        Dim MinFilter As TextureFilter = TextureFilter.None
        Dim MagFilter As TextureFilter = TextureFilter.None
        Dim MaxAnisotropy As Integer = 1

        ' Point filtering.
        If Caps.TextureFilterCaps.SupportsMinifyPoint Then _
            MinFilter = TextureFilter.Point

        If Caps.TextureFilterCaps.SupportsMagnifyPoint Then _
            MagFilter = TextureFilter.Point

        ' Linear filtering.
        If Caps.TextureFilterCaps.SupportsMinifyLinear Then _
            MinFilter = TextureFilter.Linear

        If Caps.TextureFilterCaps.SupportsMagnifyLinear Then _
            MagFilter = TextureFilter.Linear

        ' Anisotropic filtering.
        If Caps.TextureFilterCaps.SupportsMinifyAnisotropic Then _
            MinFilter = TextureFilter.Anisotropic _
                : MaxAnisotropy = Caps.MaxAnisotropy

        If Caps.TextureFilterCaps.SupportsMagnifyAnisotropic Then _
            MagFilter = TextureFilter.Anisotropic _
                : MaxAnisotropy = Caps.MaxAnisotropy

        With m_Device
            For I As Integer = 0 To NumTextureStages - 1
                ' Set min filter.
                If MinFilter <> TextureFilter.None Then _
                    .SamplerState(I).MinFilter = MinFilter

                ' Set mag filter.
                If MagFilter <> TextureFilter.None Then _
                    .SamplerState(I).MagFilter = MagFilter

                ' Set anisotropy.
                If MaxAnisotropy <> 1 Then _
                    .SamplerState(I).MaxAnisotropy = MaxAnisotropy

            Next I ' For I As Integer = 0 To NumTextureStages - 1
        End With ' With m_Device
    End Sub

    ''' <summary>
    ''' Sets the given form's window state.
    ''' </summary>
    ''' <remarks>
    ''' Function is not thread-safe!
    ''' </remarks>
    Private Sub ___AssignWindowStatesA()
        If (m_Window Is Nothing) OrElse (m_PP Is Nothing) Then _
            Exit Sub

        With m_Window
            ' Set the states.
            If m_PP.Windowed Then
                .WindowState = m_WindowState
                .FormBorderStyle = m_WindowBorderStyle

            Else ' If m_PP.Windowed Then
                .WindowState = FormWindowState.Normal
                .FormBorderStyle = FormBorderStyle.None
                .Location = New Point(0, 0)
                .Size = New Size(m_PP.BackBufferWidth, m_PP.BackBufferHeight)

            End If ' If m_PP.Windowed Then
        End With ' With m_Window 
    End Sub

    ''' <summary>
    ''' Releases the device.
    ''' </summary>
    Public Sub ReleaseDevice()
        ' Check for redundant calls.
        If m_Device Is Nothing Then _
            Exit Sub

        ' Go to windowed mode.
        ' HACK: Some pretty nasty error is avoided by doing this... how it is so is unkown now.
        ' BUG: It seems that the buffers created by the GBasicMesh class somehow interfere in 
        ' finalization process of Direct3D (and how could that affect device destruction process?) due
        ' to which the window is never closed and subsequent creation of device causes either an
        ' 'InvalidCallException' or 'DeviceLostException' (as of yet cannot be determined which one will
        ' be thrown). There is no known solution for this as far as I know, much less a way to avoid this
        ' mess, but interestingly this doesn't happen in windowed mode. So, for now, this mess is being
        ' avoided by switching to windowed mode.
        ToWindowed()

        ' Stop rendering.
        RenderLoopStop()

        ' Dispose the device.
        m_Device.Dispose()

        ' Remove the device and related references.
        m_CP = Nothing
        m_PP = Nothing

        m_Device = Nothing
        m_Window = Nothing
    End Sub

    ''' <summary>
    ''' Raised when the device is created. Applications may use
    ''' it to check whether all requirements are met, to decide whether to
    ''' continue or not.
    ''' </summary>
    Public Event DeviceCreated(ByVal DeviceCreationFlags As DeviceCreationFlags)

    ''' <summary>
    ''' Raised when a frame has to be rendered. This is so that
    ''' frames are rendered less frequently when window is not focused
    ''' or not visible.
    ''' </summary>
    Public Event Render()

    ''' <summary>
    ''' Occurs when a device is about to be lost (for example,
    ''' immediately prior to a reset).
    ''' </summary>
    ''' <remarks>
    ''' This is a device generated event.
    ''' </remarks>
    Public Event DeviceLost(ByVal sender As Object, ByVal e As System.EventArgs)

    ''' <summary>
    ''' Occurs when a device is about to be lost (for example,
    ''' immediately prior to a reset).
    ''' </summary>
    ''' <remarks>
    ''' This is a device generated event.
    ''' </remarks>
    Private Sub m_Device_DeviceLost(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_Device.DeviceLost
        ' Nothing to do here.
        RaiseEvent DeviceLost(sender, e)
    End Sub

    ''' <summary>
    ''' Occurs after a device is reset, allowing an application to
    ''' re-create all <c>Microsoft.DirectX.Direct3D.Pool.Pool.Default</c>
    ''' resources.
    ''' </summary>
    ''' <remarks>
    ''' This is a device generated event.
    ''' </remarks>
    Public Event DeviceReset(ByVal sender As Object, ByVal e As System.EventArgs)

    ''' <summary>
    ''' Occurs after a device is reset, allowing an application to
    ''' re-create all <c>Microsoft.DirectX.Direct3D.Pool.Pool.Default</c>
    ''' resources.
    ''' </summary>
    ''' <remarks>
    ''' This is a device generated event.
    ''' </remarks>
    Private Sub m_Device_DeviceReset(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_Device.DeviceReset
        ' Nothing to do here.
        RaiseEvent DeviceReset(sender, e)
    End Sub

    ''' <summary>
    ''' Occurs when a device is resizing, allowing the application to cancel
    ''' the default handling of the resize.
    ''' </summary>
    ''' <remarks>
    ''' This is a device generated event.
    ''' </remarks>
    Public Event DeviceResizing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)

    ''' <summary>
    ''' Occurs when a device is resizing, allowing the application to cancel
    ''' the default handling of the resize.
    ''' </summary>
    ''' <remarks>
    ''' This is a device generated event.
    ''' </remarks>
    Private Sub m_Device_DeviceResizing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
        Handles m_Device.DeviceResizing
        ' Bypass event if needed.
        If IsUsingResizeEventHandler Then _
            RaiseEvent DeviceResizing(sender, e) _
            Else _
            e.Cancel = True
    End Sub

    ''' <summary>
    ''' Occurs when the <c>Microsoft.DirectX.Direct3D.Device.Dispose</c> method
    ''' is called or when the <c>Microsoft.DirectX.Direct3D.Device</c> object is
    ''' finalized and collected by the garbage collector of the Microsoft .NET
    ''' common language runtime.
    ''' </summary>
    ''' <remarks>
    ''' This is a device generated event.
    ''' </remarks>
    Public Event Disposing(ByVal sender As Object, ByVal e As System.EventArgs)

    ''' <summary>
    ''' Occurs when the <c>Microsoft.DirectX.Direct3D.Device.Dispose</c> method
    ''' is called or when the <c>Microsoft.DirectX.Direct3D.Device</c> object is
    ''' finalized and collected by the garbage collector of the Microsoft .NET
    ''' common language runtime.
    ''' </summary>
    ''' <remarks>
    ''' This is a device generated event.
    ''' </remarks>
    Private Sub m_Device_Disposing(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_Device.Disposing
        ' Stop rendering.
        RenderLoopStop()

        ' Clean-up on our side. Note: ReleaseDevice() is not called because
        ' it in-turn depends on Dispose() method of the device; we'd end up
        ' here, again.
        m_Device = Nothing

        ' Raise the event.
        RaiseEvent Disposing(sender, e)
    End Sub
End Class
