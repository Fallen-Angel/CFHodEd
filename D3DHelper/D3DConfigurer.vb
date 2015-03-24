''' <summary>
''' Class containing miscellaneous code related to application
''' settings.
''' </summary> 
Public NotInheritable Class D3DConfigurer
    ''' <summary>Stores the application name.</summary>
    ''' <remarks>Used in registry setting read\write.</remarks>
    Private Shared m_ApplicationName As New String(" "c, 0)

    ''' <summary>Stores the company name.</summary>
    ''' <remarks>Used in registry setting read\write.</remarks>
    Private Shared m_CompanyName As New String(" "c, 0)

    ''' <summary>List of display modes to use.</summary>
    ''' <remarks>This may not use all fields</remarks>
    Private Shared m_DisplayModes As New Stack(Of DisplayMode)

    ''' <summary>
    ''' List of back-buffer formats to use.
    ''' </summary>
    Private Shared m_BackBufferFormats As New Stack(Of Format)

    ''' <summary>
    ''' List of depth-stencil formats to use.
    ''' </summary>
    Private Shared m_DepthStencilFormats As New Stack(Of DepthFormat)

    ''' <summary>
    ''' List of mutli-sampling techniques to use.
    ''' </summary>
    Private Shared m_MultiSamplingTechniquesA As New Stack(Of MultiSampleType)

    ''' <summary>
    ''' List of mutli-sampling qualities to use.
    ''' </summary>
    Private Shared m_MultiSamplingTechniquesB As New Stack(Of Integer)

    ''' <summary>
    ''' List of presentation intervals to use.
    ''' </summary>
    Private Shared m_PresentInterval As New Stack(Of PresentInterval)

    ''' <summary>List of back-buffer counts to use.</summary>
    Private Shared m_BackBufferCounts As New Stack(Of Integer)

    ''' <summary>Creation flags to use.</summary>
    Private Shared m_CreationFlags As CreateFlags = 0

    ''' <summary>Presentation flags to use.</summary>
    Public Shared PresentFlags As PresentFlag = PresentFlag.None

    ''' <summary>Full screen.</summary>
    Public Shared FullScreen As Boolean = False

    ''' <summary>Forces no multi-threading.</summary>
    Public Shared ForceNoMultithreadingFlag As Boolean = False

    ''' <summary>
    ''' Class Constructor.
    ''' </summary>
    ''' <remarks>
    ''' This actually does nothing.
    ''' </remarks>
    Private Sub New()
        ' Do nothing.
    End Sub

    ''' <summary>
    ''' Returns\Sets the application name.
    ''' </summary>
    ''' <exception cref="InvalidCallException">
    ''' Thrown when the property is set for the second time 
    ''' (in other words, this property can be set only once).
    ''' </exception>
    ''' <exception cref="ArgumentException">
    ''' Thrown when <c>value Is Nothing</c> or
    ''' <c>Value.Length = 0</c>.
    ''' </exception>
    ''' <remarks>
    ''' Used in registry setting read\write operations.
    ''' This must be set before using it.
    ''' </remarks>
    Public Shared Property AppName() As String
        Get
            ' Return the value.
            Return m_ApplicationName
        End Get

        Set(ByVal value As String)
            ' Check inputs.
            If (value Is Nothing) OrElse (value.Length = 0) Then _
                Throw New ArgumentException("Invalid 'value'.") _
                    : Exit Property

            ' See if the property was already set.
            If m_ApplicationName.Length <> 0 Then _
                Throw New InvalidCallException("Cannot modify property.") _
                    : Exit Property

            If value.Contains("\") Then _
                Throw New ArgumentException("'value' contains a slash; cannot be used as a name.") _
                    : Exit Property

            ' Set the value.
            m_ApplicationName = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets the application company name.
    ''' </summary>
    ''' <exception cref="InvalidCallException">
    ''' Thrown when the property is set for the second time 
    ''' (in other words, this property can be set only once).
    ''' </exception>
    ''' <exception cref="ArgumentException">
    ''' Thrown when <c>value Is Nothing</c> or
    ''' <c>Value.Length = 0</c>.
    ''' </exception>
    ''' <remarks>
    ''' Used in registry setting read\write operations.
    ''' If this is not set, then the company name key
    ''' is not created\used.
    ''' </remarks>
    Public Shared Property AppCompanyName() As String
        Get
            ' Return the value.
            Return m_CompanyName
        End Get

        Set(ByVal value As String)
            ' Check inputs.
            If value Is Nothing Then _
                Throw New ArgumentException("Invalid 'value'.") _
                    : Exit Property

            ' See if the property was already set.
            If m_CompanyName.Length <> 0 Then _
                Throw New InvalidCallException("Cannot modify property.") _
                    : Exit Property

            ' Set the value.
            m_CompanyName = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the full aplication name in the format
    ''' [&lt;Company Name&gt;\]&lt;Application Name&gt;
    ''' </summary>
    ''' <remarks>
    ''' Used in registry setting read\write operations.
    ''' </remarks>
    Public Shared ReadOnly Property AppNameFull() As String
        Get
            ' See if both properties were set.
            If m_CompanyName.Length <> 0 Then _
                Return m_CompanyName & "\" & m_ApplicationName

            ' If only application name was set, then return it.
            Return m_ApplicationName
        End Get
    End Property

    ''' <summary>
    ''' Returns\Sets the device creation flags.
    ''' </summary>
    Public Shared Property CreationFlags() As CreateFlags
        Get
            If m_CreationFlags <> 0 Then _
                Return m_CreationFlags _
                Else _
                Return CreateFlags.SoftwareVertexProcessing
        End Get

        Set(ByVal value As CreateFlags)
            ' Validate the creation flags.
            Trace.Assert(ValidateCreateFlags(value) = value, "Warning: Invalid creation flags.")

            ' Set it.
            m_CreationFlags = ValidateCreateFlags(value)
        End Set
    End Property

    ''' <summary>
    ''' Returns the display mode array.
    ''' </summary>
    ''' <remarks>
    ''' This creates a new array for each call.
    ''' </remarks>
    Public Shared Function GetDisplayModes() As DisplayMode()
        Return m_DisplayModes.ToArray()
    End Function

    ''' <summary>
    ''' Returns the back-buffer formats.
    ''' </summary>
    ''' <remarks>
    ''' This creates a new array for each call.
    ''' </remarks>
    Public Shared Function GetBackBufferFormats() As Format()
        Return m_BackBufferFormats.ToArray()
    End Function

    ''' <summary>
    ''' Returns the back-buffer counts.
    ''' </summary>
    ''' <remarks>
    ''' This creates a new array for each call.
    ''' </remarks>
    Public Shared Function GetBackBufferCounts() As Integer()
        Return m_BackBufferCounts.ToArray()
    End Function

    ''' <summary>
    ''' Returns the depth stencil formats.
    ''' </summary>
    ''' <remarks>
    ''' This creates a new array for each call.
    ''' </remarks>
    Public Shared Function GetDepthStencilFormats() As DepthFormat()
        Return m_DepthStencilFormats.ToArray()
    End Function

    ''' <summary>
    ''' Returns the multi-sampling types.
    ''' </summary>
    ''' <remarks>
    ''' This creates a new array for each call.
    ''' </remarks>
    Public Shared Function GetMultisamplingTypes() As MultiSampleType()
        Return m_MultiSamplingTechniquesA.ToArray()
    End Function

    ''' <summary>
    ''' Returns the multi-sampling quality levels.
    ''' </summary>
    ''' <remarks>
    ''' This creates a new array for each call.
    ''' </remarks>
    Public Shared Function GetMultisamplingQualities() As Integer()
        Return m_MultiSamplingTechniquesB.ToArray()
    End Function

    ''' <summary>
    ''' Returns the presentation intervals.
    ''' </summary>
    ''' <remarks>
    ''' This creates a new array for each call.
    ''' </remarks>
    Public Shared Function GetPresentIntervals() As PresentInterval()
        Return m_PresentInterval.ToArray()
    End Function

    ''' <summary>
    ''' Returns the display mode array.
    ''' </summary>
    ''' <remarks>
    ''' This creates a new array for each call.
    ''' </remarks>
    Public Shared Sub ResetDisplayModes()
        m_DisplayModes.Clear()
    End Sub

    ''' <summary>
    ''' Returns the back-buffer formats.
    ''' </summary>
    ''' <remarks>
    ''' This creates a new array for each call.
    ''' </remarks>
    Public Shared Sub ResetBackBufferFormats()
        m_BackBufferFormats.Clear()
    End Sub

    ''' <summary>
    ''' Returns the back-buffer counts.
    ''' </summary>
    ''' <remarks>
    ''' This creates a new array for each call.
    ''' </remarks>
    Public Shared Sub ResetBackBufferCounts()
        m_BackBufferCounts.Clear()
    End Sub

    ''' <summary>
    ''' Returns the depth stencil formats.
    ''' </summary>
    ''' <remarks>
    ''' This creates a new array for each call.
    ''' </remarks>
    Public Shared Sub ResetDepthStencilFormats()
        m_DepthStencilFormats.Clear()
    End Sub

    ''' <summary>
    ''' Returns the multi-sampling types and qualities.
    ''' </summary>
    ''' <remarks>
    ''' This creates a new array for each call.
    ''' </remarks>
    Public Shared Sub ResetMultisampling()
        m_MultiSamplingTechniquesA.Clear()
        m_MultiSamplingTechniquesB.Clear()
    End Sub

    ''' <summary>
    ''' Returns the presentation intervals.
    ''' </summary>
    ''' <remarks>
    ''' This creates a new array for each call.
    ''' </remarks>
    Public Shared Sub ResetPresentIntervals()
        m_PresentInterval.Clear()
    End Sub

    ''' <summary>
    ''' Clears all preferences.
    ''' </summary>
    Public Shared Sub ClearPreferences()
        m_DisplayModes.Clear()
        m_BackBufferFormats.Clear()
        m_DepthStencilFormats.Clear()
        m_MultiSamplingTechniquesA.Clear()
        m_MultiSamplingTechniquesB.Clear()
        m_BackBufferCounts.Clear()
        m_PresentInterval.Clear()
        m_CreationFlags = 0

        PresentFlags = PresentFlag.None
        FullScreen = False
        ForceNoMultithreadingFlag = False
    End Sub

    ''' <summary>
    ''' Adds a display mode to the list. Note that always the
    ''' maximum possible refresh rate is chosen.
    ''' </summary>
    ''' <param name="Width">
    ''' Width.
    ''' </param>
    ''' <param name="Height">
    ''' Height.
    ''' </param>
    ''' <remarks>
    ''' The element added first has least preference,
    ''' the element added last has most preference.
    ''' </remarks>
    ''' <exception cref="ArgumentException">
    ''' Thrown when <c>Width</c> or <c>Height</c> is invalid.
    ''' </exception>
    Public Shared Sub AddDisplayMode(ByVal Width As Integer, ByVal Height As Integer)
        Dim DispMode As DisplayMode

        ' Check inputs.
        If (Width <= 0) OrElse (Height <= 0) Then _
            Throw New ArgumentException("Invalid 'Width' or 'Height'.") _
                : Exit Sub

        ' Set the fields.
        DispMode.Width = Width
        DispMode.Height = Height

        ' Add
        m_DisplayModes.Push(DispMode)
    End Sub

    ''' <summary>
    ''' Adds a back-buffer format to the list.
    ''' </summary>
    ''' <param name="_Format">
    ''' The format to add.
    ''' </param>
    ''' <remarks>
    ''' The element added first has least preference,
    ''' the element added last has most preference.
    ''' </remarks>
    ''' <exception cref="ArgumentException">
    ''' Thrown when <c>_Format</c> is invalid.
    ''' </exception>
    Public Shared Sub AddBackBufferFormat(ByVal _Format As Format)
        ' Check inputs.
        If Not FormatIsRGB(_Format) Then _
            Throw New ArgumentException("Invalid '_Format'.") _
                : Exit Sub

        ' Add it.
        m_BackBufferFormats.Push(_Format)
    End Sub

    ''' <summary>
    ''' Adds a depth-stencil format to the list.
    ''' </summary>
    ''' <param name="_DepthFormat">
    ''' The format to add.
    ''' </param>
    ''' <remarks>
    ''' The element added first has least preference,
    ''' the element added last has most preference.
    ''' </remarks>
    ''' <exception cref="ArgumentException">
    ''' Thrown when <c>_Format</c> is invalid.
    ''' </exception>
    Public Shared Sub AddDepthStencilFormat(ByVal _DepthFormat As DepthFormat)
        ' Check inputs.
        If Not FormatIsDepthStencil(_DepthFormat) Then _
            Throw New ArgumentException("Invalid '_DepthFormat'.") _
                : Exit Sub

        ' Add it.
        m_DepthStencilFormats.Push(_DepthFormat)
    End Sub

    ''' <summary>
    ''' Adds a depth-stencil format to the list.
    ''' </summary>
    ''' <param name="_Format">
    ''' The format to add.
    ''' </param>
    ''' <remarks>
    ''' The element added first has least preference,
    ''' the element added last has most preference.
    ''' </remarks>
    ''' <exception cref="ArgumentException">
    ''' Thrown when <c>_Format</c> is invalid.
    ''' </exception>
    Public Shared Sub AddDepthStencilFormat(ByVal _Format As Format)
        ' Call other method.
        D3DConfigurer.AddDepthStencilFormat(CType(_Format, DepthFormat))
    End Sub

    ''' <summary>
    ''' Adds a multi-sampling technique.
    ''' </summary>
    ''' <param name="_MultiSampleType">
    ''' Multi-sample type.
    ''' </param>
    ''' <param name="MultiSampleQuality">
    ''' Multi-sample quality.
    ''' </param>
    ''' <remarks>
    ''' The element added first has least preference,
    ''' the element added last has most preference.
    ''' </remarks> ''' 
    ''' <exception cref="ArgumentException">
    ''' Thrown when any of the arguments fail validation.
    ''' </exception>
    Public Shared Sub AddMultiSamplingTechnique(ByVal _MultiSampleType As MultiSampleType,
                                                ByVal MultiSampleQuality As Integer)
        ' Check inputs.
        If ValidateMultiSampleType(_MultiSampleType) <> _MultiSampleType Then _
            Throw New ArgumentException("Invalid '_MultiSampleType'.") _
                : Exit Sub

        ' Check inputs.
        If ValidateMultiSampleQuality(MultiSampleQuality) <> MultiSampleQuality Then _
            Throw New ArgumentException("Invalid 'MultiSampleQuality'.") _
                : Exit Sub

        ' Add it.
        m_MultiSamplingTechniquesA.Push(_MultiSampleType)
        m_MultiSamplingTechniquesB.Push(MultiSampleQuality)
    End Sub

    ''' <summary>
    ''' Adds a value of presentation inverval. Note that PresentInterval.Immediate
    ''' is not recommeneded to be used.
    ''' </summary>
    ''' <param name="_PresentInterval">
    ''' Presentation interval to add.
    ''' </param>
    ''' <remarks>
    ''' The element added first has least preference,
    ''' the element added last has most preference.
    ''' </remarks>
    ''' <exception cref="ArgumentException">
    ''' Thrown when <c>_PresentInterval</c> is invalid.
    ''' </exception>
    Public Shared Sub AddPresentInterval(ByVal _PresentInterval As PresentInterval)
        ' Check inputs.
        If ValidatePresentInterval(_PresentInterval) <> _PresentInterval Then _
            Throw New ArgumentException("Invalid '_PresentInterval'.") _
                : Exit Sub

        ' Add it.
        m_PresentInterval.Push(_PresentInterval)
    End Sub

    ''' <summary>
    ''' Adds a value of back-buffer count.
    ''' </summary>
    ''' <param name="BackBufferCount">
    ''' Back-Buffer count.
    ''' </param>
    ''' <remarks>
    ''' The element added first has least preference,
    ''' the element added last has most preference.
    ''' </remarks>
    ''' <exception cref="ArgumentException">
    ''' Thrown when <c>BackBufferCount</c> is invalid.
    ''' </exception>
    Public Shared Sub AddBackBufferCount(ByVal BackBufferCount As Integer)
        ' Check inputs.
        If (BackBufferCount <= 0) OrElse (BackBufferCount > Present.BackBuffersMax) Then _
            Throw New ArgumentException("Invalid 'BackBufferCount'.") _
                : Exit Sub

        ' Add it.
        m_BackBufferCounts.Push(BackBufferCount)
    End Sub

    ''' <summary>
    ''' Shows the Direct3D settings window.
    ''' </summary>
    ''' <param name="LockWindowed">
    ''' Whether the 'Windowed' checkbox is checked or not. If set to default,
    ''' can be selected by user. Otherwise, not.
    ''' </param>
    ''' <remarks>
    ''' This method disables the checkbox, hence it cannot be changed by user.
    ''' Use this method to force windowed or full-screen mode.
    ''' </remarks>
    Public Shared Sub ShowD3DSettingsWindow(
                                            Optional ByVal LockWindowed As Microsoft.VisualBasic.TriState =
                                               TriState.UseDefault)
        Static EnabledVisualStyles As Boolean = False

        ' Enable visual styles if we haven't done so.
        If Not EnabledVisualStyles Then _
            Application.EnableVisualStyles() _
                : EnabledVisualStyles = True

        Dim f As D3DSettings

        f = New D3DSettings

        f.LockWindowedCheckbox(LockWindowed)

        f.ShowDialog()

        f.Dispose()

        f = Nothing
    End Sub

    ''' <summary>
    ''' Builds the device creation flags.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter to use.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to use.
    ''' </param>
    ''' <exception cref="ArgumentException">
    ''' Thrown when any of the arguments fail validation.
    ''' </exception>
    Friend Shared Function BuildCreationFlags(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType) As CreateFlags
        ' Check inputs.
        If ValidateAdapter(Adapter) <> Adapter Then _
            Throw New ArgumentException("Invalid 'Adapter'.") _
                : Exit Function

        ' Check inputs.
        If ValidateDeviceType(_DeviceType) <> _DeviceType Then _
            Throw New ArgumentException("Invalid '_DeviceType'.") _
                : Exit Function

        ' Check inputs.
        If Not CheckDeviceType(Adapter, _DeviceType) Then _
            Throw New ArgumentException("'_DeviceType' not supported on 'Adapter'.") _
                : Exit Function

        ' Check if we have been given some creation flags.
        If m_CreationFlags <> 0 Then
            ' See if HVP or MVP is set and HWTL is not available.
            ' In that case remove HVP or MVP flag and put SVP flag.
            If (((m_CreationFlags And CreateFlags.HardwareVertexProcessing) <> 0) OrElse
                ((m_CreationFlags And CreateFlags.MixedVertexProcessing) <> 0)) AndAlso
               (Not CheckHardwareVertexProcessing(Adapter, _DeviceType)) Then _
                Trace.TraceWarning("HWTL not supported.") _
                    : _
                    m_CreationFlags = m_CreationFlags And
                                      Not (CreateFlags.HardwareVertexProcessing Or CreateFlags.MixedVertexProcessing) _
                    : m_CreationFlags = m_CreationFlags Or CreateFlags.SoftwareVertexProcessing

            ' Now the creation parameters are valid.
            Return m_CreationFlags

        Else ' If m_CreationFlags <> 0 Then
            ' Build our own creation flags according to device capabilities.
            If CheckHardwareVertexProcessing(Adapter, _DeviceType) Then _
                Return CreateFlags.HardwareVertexProcessing _
                Else _
                Return CreateFlags.SoftwareVertexProcessing

        End If ' If m_CreationFlags <> 0 Then
    End Function

    ''' <summary>
    ''' Builds a presentation paramters object.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter to use.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to use.
    ''' </param>
    ''' <param name="_PresentParamters">
    ''' Input object.
    ''' </param>
    ''' <param name="Width_OVERRIDE">
    ''' Override back-buffer width. Must be compatible for full-screen.
    ''' </param>
    ''' <param name="Height_OVERRIDE">
    ''' Override back-buffer height. Must be compatible for full-screen.
    ''' </param>
    ''' <param name="BackBufferFormat_OVERRIDE" >
    ''' Override back-buffer format. Must be compatible.
    ''' </param>
    ''' <param name="Windowed_OVERRIDE">
    ''' Override windowed mode.
    ''' </param>
    ''' <returns>
    ''' Presentation paramters.
    ''' </returns>
    ''' <remarks>
    ''' This modifies the existing object. Also,
    ''' it creates a new object if input is nothing.
    ''' </remarks>
    ''' <exception cref="ArgumentException">
    ''' Thrown when <c>Adapter</c> or <c>_DeviceType</c> fail 
    ''' validation.
    ''' </exception>
    Friend Shared Function BuildPresentParameters(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                                  Optional ByVal _PresentParamters As PresentParameters = Nothing,
                                                  Optional ByVal Width_OVERRIDE As Integer = 0,
                                                  Optional ByVal Height_OVERRIDE As Integer = 0,
                                                  Optional ByVal BackBufferFormat_OVERRIDE As Format = Format.Unknown,
                                                  Optional ByVal Windowed_OVERRIDE As TriState = TriState.UseDefault) _
        As PresentParameters
        Dim CurrDispMode As DisplayMode,
            MaxBackBufferCount As Integer,
            ChosenDisplayMode As Integer = - 1

        Dim DisplayModes() As DisplayMode,
            BackBufferFormats() As Format,
            BackBufferCounts() As Integer,
            DepthStencilFormats() As DepthFormat,
            MultiSamplingA() As MultiSampleType,
            MultiSamplingB() As Integer,
            PresentIntervals() As PresentInterval

        ' Check inputs.
        If ValidateAdapter(Adapter) <> Adapter Then _
            Throw New ArgumentException("Invalid 'Adapter'.") _
                : Exit Function

        ' Check inputs.
        If ValidateDeviceType(_DeviceType) <> _DeviceType Then _
            Throw New ArgumentException("Invalid '_DeviceType'.") _
                : Exit Function

        ' Check inputs.
        If Not CheckDeviceType(Adapter, _DeviceType) Then _
            Throw New ArgumentException("'_DeviceType' not available on 'Adapter'.") _
                : Exit Function

        ' Build new presentation paramters, if needed.
        If _PresentParamters Is Nothing Then _
            _PresentParamters = New PresentParameters

        ' Get the current display mode.
        CurrDispMode = Manager.Adapters(Adapter).CurrentDisplayMode

        ' Get the display modes, back-buffer formats, depth-stencil formats,
        ' multi-sampling techniques, presentation intervals, etc.
        DisplayModes = D3DConfigurer.GetDisplayModes()
        BackBufferFormats = D3DConfigurer.GetBackBufferFormats()
        BackBufferCounts = D3DConfigurer.GetBackBufferCounts()
        DepthStencilFormats = D3DConfigurer.GetDepthStencilFormats()
        MultiSamplingA = D3DConfigurer.GetMultisamplingTypes()
        MultiSamplingB = D3DConfigurer.GetMultisamplingQualities()
        PresentIntervals = D3DConfigurer.GetPresentIntervals()

        ' Debug check.
        Trace.Assert(MultiSamplingA.Length = MultiSamplingB.Length,
                     "Internal error",
                     "L(MSA) <> L(MSB)")

        With _PresentParamters
            ' ----------------------
            ' Set the boolean flags.
            ' ----------------------
            .ForceNoMultiThreadedFlag = ForceNoMultithreadingFlag

            ' ----------------------
            ' Set the windowed flag.
            ' ----------------------
            Select Case Windowed_OVERRIDE
                Case TriState.True
                    .Windowed = True
                Case TriState.False
                    .Windowed = False
                Case Else
                    .Windowed = Not FullScreen
            End Select ' Select Case Windowed_OVERRIDE

            ' --------------------------------
            ' Set the display mode and format.
            ' --------------------------------
            If ((Width_OVERRIDE <> 0) AndAlso (Height_OVERRIDE <> 0)) AndAlso
               (.Windowed OrElse CheckDisplayMode(Adapter, Width_OVERRIDE, Height_OVERRIDE)) Then
                ' ------------------------------------------
                ' Set the overriden display mode and format. 
                ' ------------------------------------------
                .BackBufferWidth = Width_OVERRIDE
                .BackBufferHeight = Height_OVERRIDE
                .BackBufferFormat = BackBufferFormat_OVERRIDE

                ' Set the refresh rate.
                If .Windowed Then _
                    .FullScreenRefreshRateInHz = 0 _
                    Else _
                    .FullScreenRefreshRateInHz = GetMaxRefreshRate(Adapter,
                                                                   .BackBufferWidth,
                                                                   .BackBufferHeight,
                                                                   CurrDispMode.Format)

            Else _
                ' If ((Width_OVERRIDE <> 0) AndAlso (Height_OVERRIDE <> 0)) AndAlso (.Windowed OrElse CheckDisplayMode(Adapter, Width_OVERRIDE, Height_OVERRIDE)) Then
                ' ------------------------
                ' Select the display mode.
                ' ------------------------
                ' Set the default mode in case none is compatible.
                .BackBufferWidth = CurrDispMode.Width
                .BackBufferHeight = CurrDispMode.Height
                .FullScreenRefreshRateInHz = CInt(IIf(.Windowed, 0, CurrDispMode.RefreshRate))

                ' This mode may not be present in the input list. But if it is, then
                ' it doesn't matter because it may get selected or other higher priority
                ' mode may get selected.
                ChosenDisplayMode = - 1

                If .Windowed Then
                    ' Select the first mode, smaller than or of screen size, if possible.
                    ' The reason for doing this should be obvious: We may have much larger
                    ' resolutions in list than that of current screen, and they may not be
                    ' really of much use.
                    For I As Integer = 0 To DisplayModes.Length - 1
                        If (DisplayModes(I).Width <= .BackBufferWidth) AndAlso
                           (DisplayModes(I).Height <= .BackBufferHeight) Then _
                            .BackBufferWidth = DisplayModes(I).Width _
                                : .BackBufferHeight = DisplayModes(I).Height _
                                : .FullScreenRefreshRateInHz = 0 _
                                : ChosenDisplayMode = I _
                                : Exit For

                    Next I ' For I As Integer = 0 To DisplayModes.Length - 1

                Else ' If .Windowed Then
                    ' Try one and all of the listed display modes (as needed) in order.
                    For I As Integer = 0 To UBound(DisplayModes)
                        ' Check for compatibility.
                        If CheckDisplayMode(Adapter, DisplayModes(I).Width, DisplayModes(I).Height,
                                            CurrDispMode.Format) Then _
                            .BackBufferWidth = DisplayModes(I).Width _
                                : .BackBufferHeight = DisplayModes(I).Height _
                                : .FullScreenRefreshRateInHz = GetMaxRefreshRate(Adapter,
                                                                                 DisplayModes(I).Width,
                                                                                 DisplayModes(I).Height,
                                                                                 CurrDispMode.Format) _
                                : ChosenDisplayMode = I _
                                : Exit For

                    Next I ' For I As Integer = 0 To UBound(DisplayModes)
                End If ' If .Windowed Then

                ' ---------------------------
                ' Set the back-buffer format.
                ' ---------------------------
                ' Set the default format mode in case none is compatible.
                .BackBufferFormat = CurrDispMode.Format

                ' Try one and all of the listed formats (as needed) in order.
                For I As Integer = 0 To UBound(BackBufferFormats)
                    ' Check for compatibility.
                    If CheckDisplayFormat(Adapter, _DeviceType, CurrDispMode.Format,
                                          BackBufferFormats(I), Not FullScreen) Then _
                        .BackBufferFormat = BackBufferFormats(I) _
                            : Exit For

                Next I ' For I As Integer = 0 To UBound(BackBufferFormats)
            End If _
            ' If ((Width_OVERRIDE <> 0) AndAlso (Height_OVERRIDE <> 0)) AndAlso (.Windowed OrElse CheckDisplayMode(Adapter, Width_OVERRIDE, Height_OVERRIDE)) Then

            ' -----------------------------
            ' Set the depth-stencil format.
            ' -----------------------------
            ' Set the default depth-stencil format in case none is compatible.
            .EnableAutoDepthStencil = False
            .AutoDepthStencilFormat = DepthFormat.Unknown

            ' Try one and all of the listed formats (as needed) in order.
            For I As Integer = 0 To UBound(DepthStencilFormats)
                ' Check for compatibility.
                If CheckDepthStencilFormat(Adapter, _DeviceType, CurrDispMode.Format,
                                           .BackBufferFormat, DepthStencilFormats(I),
                                           .Windowed) Then _
                    .EnableAutoDepthStencil = True _
                        : .AutoDepthStencilFormat = DepthStencilFormats(I) _
                        : Exit For

            Next I ' For I As Integer = 0 To UBound(DepthStencilFormats)

            ' ---------------------------------
            ' Set the multi-sampling technique.
            ' ---------------------------------
            ' Set the default multi-sampling technique.
            .MultiSample = MultiSampleType.None
            .MultiSampleQuality = 0

            ' Check if multi-sampling is possible.
            If Not FormatIsDepthLockable(.AutoDepthStencilFormat) Then
                ' Try one and all of the multi-sampling techniques.
                For I As Integer = 0 To UBound(MultiSamplingA)
                    ' Check for compatibility.
                    If CheckMultiSampleType(Adapter, _DeviceType, CurrDispMode.Format,
                                            .BackBufferFormat, .AutoDepthStencilFormat,
                                            MultiSamplingA(I), MultiSamplingB(I), .Windowed) Then _
                        .MultiSample = MultiSamplingA(I) _
                            : .MultiSampleQuality = MultiSamplingB(I) _
                            : Exit For

                Next I ' For I As Integer = 0 To UBound(MultiSamplingA)
            Else ' If Not FormatIsDepthLockable(.AutoDepthStencilFormat) Then
                ' Give a warning.
                Trace.Assert(False, "Warning: Lockable depth buffers are " &
                                    "not compatible with multi-sampling")


                ' Note that, here we do not select a multi-sampling technique,
                ' assuming that lockable depth buffer is more important.
                .MultiSample = MultiSampleType.None
                .MultiSampleQuality = 0
            End If ' If Not FormatIsDepthLockable(.AutoDepthStencilFormat) Then

            ' ----------------------------------------
            ' Set the presentation interval and flags.
            ' ----------------------------------------
            ' Set the default presentation interval, in case none is compatible.
            .PresentationInterval = PresentInterval.Default
            .PresentFlag = ValidatePresentFlag(PresentFlags)

            ' Try one and all of the presentation intervals (as needed) in order.
            For I As Integer = 0 To UBound(PresentIntervals)
                ' Check for compatibility.
                If CheckPresentationInterval(Adapter, _DeviceType, PresentIntervals(I)) Then _
                    .PresentationInterval = PresentIntervals(I) _
                        : Exit For

            Next I ' For I As Integer = 0 To UBound(PresentIntervals)

            ' --------------------------
            ' Set the back-buffer count.
            ' --------------------------
            ' Set the default back-buffer count.
            .BackBufferCount = 1

            ' Get the maximum number of back-buffers.
            MaxBackBufferCount = GetMaxBackBufferCount(Adapter, _DeviceType, _PresentParamters)

            ' Do we have usable back-buffer count?
            If MaxBackBufferCount = - 1 Then
                ' Try from next mode onwards if we chose a mode.
                If ChosenDisplayMode <> - 1 Then
                    For I As Integer = ChosenDisplayMode To UBound(DisplayModes)
                        If (.Windowed) Or (CheckDisplayMode(Adapter, DisplayModes(I).Width, DisplayModes(I).Height)) _
                            Then
                            ' Set the display size.
                            .BackBufferWidth = DisplayModes(I).Width
                            .BackBufferHeight = DisplayModes(I).Height
                            ChosenDisplayMode = I

                            ' Now check the count.
                            MaxBackBufferCount = GetMaxBackBufferCount(Adapter, _DeviceType, _PresentParamters)

                            ' Not possible to continue.
                            If MaxBackBufferCount = - 1 Then _
                                Continue For

                            ' Usable back-buffer count.
                            Exit For
                        End If _
                        ' If (.Windowed) Or (CheckDisplayMode(Adapter, DisplayModes(I).Width, DisplayModes(I).Height)) Then
                    Next I ' For I As Integer = ChosenDisplayMode To UBound(DisplayModes)
                End If ' If ChosenDisplayMode <> -1 Then
            End If ' If MaxBackBufferCount = -1 then

            ' Still no usable back-buffer count?
            If MaxBackBufferCount = - 1 Then
                ' Lower display resolution to the very bare minimum.
                .BackBufferWidth = 320
                .BackBufferHeight = 240

                ' Now check the count.
                MaxBackBufferCount = GetMaxBackBufferCount(Adapter, _DeviceType, _PresentParamters)

                ' Not possible to continue.
                If MaxBackBufferCount = - 1 Then _
                    Throw New OutOfMemoryException("Insufficient video\system memory.") _
                        : Exit Function

            End If ' If MaxBackBufferCount = -1 Then

            ' See if we can use multiple buffering.
            If .MultiSample = MultiSampleType.None Then
                ' Try one and all of the back-buffer counts (as needed) in order.
                For I As Integer = 0 To UBound(BackBufferCounts)
                    If BackBufferCounts(I) <= MaxBackBufferCount Then _
                        .BackBufferCount = BackBufferCounts(I) _
                            : Exit For

                Next I ' For I As Integer = 0 To UBound(BackBufferCounts)
            End If ' If .MultiSample = MultiSampleType.None then

            ' --------------------
            ' Set the swap effect.
            ' --------------------
            If .MultiSample = MultiSampleType.None Then
                ' As multi-sampling is not used, we don't need to use
                ' discard swap effect. Use flip (for multiple back-buffers),
                ' or copy (for single back-buffer).
                If .BackBufferCount > 1 Then _
                    .SwapEffect = SwapEffect.Flip _
                    Else _
                    .SwapEffect = SwapEffect.Copy

            Else ' If .MultiSample = MultiSampleType.None Then
                ' Can't use any other swap effect other than discard when
                ' multi-sampling is used.
                .SwapEffect = SwapEffect.Discard

            End If ' If .MultiSample = MultiSampleType.None Then
        End With ' With _PresentParamters

        ' -----------------
        ' Clear all arrays.
        ' -----------------
        DisplayModes = Nothing
        BackBufferFormats = Nothing
        BackBufferCounts = Nothing
        DepthStencilFormats = Nothing
        MultiSamplingA = Nothing
        MultiSamplingB = Nothing
        PresentIntervals = Nothing

        ' Validate and return the object.
        Return ValidatePresentParameters(Adapter, _DeviceType, _PresentParamters)
    End Function
End Class
