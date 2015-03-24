''' <summary>
''' Module containing code for reading\writing settings from\to registry.
''' </summary>
Friend Module Registry
    ' Names of keys in which different settings are stored.
    Private Const REG_ADAPTER As String = "Adapter Ordinal"
    Private Const REG_DEVICETYPE As String = "Device Type"
    Private Const REG_CREATEFLAGS As String = "Create Flags"
    Private Const REG_WIDTH As String = "Back Buffer Width"
    Private Const REG_HEIGHT As String = "Back Buffer Height"
    Private Const REG_FORMAT As String = "Back Buffer Format"
    Private Const REG_BACKBUFFERCOUNT As String = "Back Buffer Count"
    Private Const REG_ENABLEAUTODEPTHSTENCIL As String = "Enable Depth-Stencil"
    Private Const REG_DEPTHSTENCILFORMAT As String = "Depth-Stencil Format"
    Private Const REG_REFRESHRATE As String = "Refresh Rate"
    Private Const REG_PRESENTATIONINTERVAL As String = "Presentation Interval"
    Private Const REG_PRESENTATIONFLAG As String = "Presentation Flags"
    Private Const REG_SWAPEFFECT As String = "Swap Effect"
    Private Const REG_MULTISAMPLETYPE As String = "Multisample Type"
    Private Const REG_MULTISAMPLEQUALITY As String = "Multisample Quality"
    Private Const REG_WINDOWED As String = "Windowed"
    Private Const REG_FORCENOMULTITHREADEDFLAG As String = "Force no Multi-threaded flag"
    Private Const REG_SUBKEY_PREFIX As String = "Software\"
    Private Const REG_SUBKEY_POSTFIX As String = "\D3DHelper"

    ''' <summary>
    ''' Returns whether or not a key exists in the registry.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Company\Application name.
    ''' </param>
    ''' <param name="Name">
    ''' Setting name.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if it exists.
    ''' </returns>
    Private Function SettingExists(ByVal ApplicationName As String, ByVal Name As String) As Boolean
        Dim k As Microsoft.Win32.RegistryKey

        ' Try to open the key.
        k = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(REG_SUBKEY_PREFIX & ApplicationName & REG_SUBKEY_POSTFIX)

        ' Check if key exists.
        If k Is Nothing Then _
            Return False

        ' Check if it exists.
        If k.GetValue(Name) Is Nothing Then _
            Return False _
            Else _
            k.Close() _
                : Return True
    End Function

    ''' <summary>
    ''' Returns the type of key in the registry.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Company\Application name.
    ''' </param>
    ''' <param name="Name">
    ''' Setting name.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if it exists.
    ''' </returns>
    Private Function SettingType(ByVal ApplicationName As String, ByVal Name As String) _
        As Microsoft.Win32.RegistryValueKind
        Dim k As Microsoft.Win32.RegistryKey
        Dim t As Microsoft.Win32.RegistryValueKind

        ' Try to open the key.
        k = My.Computer.Registry.CurrentUser.OpenSubKey(REG_SUBKEY_PREFIX & ApplicationName & REG_SUBKEY_POSTFIX)

        ' Check if it exists.
        If k.GetValue(Name) Is Nothing Then _
            Return Microsoft.Win32.RegistryValueKind.Unknown _
            Else _
            t = k.GetValueKind(Name) _
                : k.Close() _
                : Return t
    End Function

    ''' <summary>
    ''' Saves a registry setting.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Company\Application name.
    ''' </param>
    ''' <param name="Name">
    ''' Setting name.
    ''' </param>
    ''' <param name="Value">
    ''' Value to store.
    ''' </param>
    Private Sub SaveRegistrySetting(ByVal ApplicationName As String, ByVal Name As String, ByVal Value As Integer)
        Dim k As Microsoft.Win32.RegistryKey

        ' Check inputs.
        If (ApplicationName Is Nothing) OrElse (ApplicationName.Length = 0) Then _
            Throw New ArgumentNullException("ApplicationName") _
                : Exit Sub

        ' Check inputs.
        If (Name Is Nothing) OrElse (Name.Length = 0) Then _
            Throw New ArgumentNullException("Name") _
                : Exit Sub

        ' Try to open the key or create it if not present.
        k = My.Computer.Registry.CurrentUser.CreateSubKey(REG_SUBKEY_PREFIX & ApplicationName & REG_SUBKEY_POSTFIX)

        ' Set the value.
        k.SetValue(Name, Value, Microsoft.Win32.RegistryValueKind.DWord)

        ' Close the key.
        k.Close()
    End Sub

    ''' <summary>
    ''' Saves a registry setting.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Company\Application name.
    ''' </param>
    ''' <param name="Name">
    ''' Setting name.
    ''' </param>
    ''' <param name="Value">
    ''' Value to store.
    ''' </param>
    Private Sub SaveRegistrySetting(ByVal ApplicationName As String, ByVal Name As String, ByVal Value As Boolean)
        Dim k As Microsoft.Win32.RegistryKey

        ' Check inputs.
        If (ApplicationName Is Nothing) OrElse (ApplicationName.Length = 0) Then _
            Throw New ArgumentNullException("ApplicationName") _
                : Exit Sub

        ' Check inputs.
        If (Name Is Nothing) OrElse (Name.Length = 0) Then _
            Throw New ArgumentNullException("Name") _
                : Exit Sub

        ' Try to open the key or create it if not present.
        k = My.Computer.Registry.CurrentUser.CreateSubKey(REG_SUBKEY_PREFIX & ApplicationName & REG_SUBKEY_POSTFIX)

        ' Set the value.
        k.SetValue(Name, CInt(Value), Microsoft.Win32.RegistryValueKind.DWord)

        ' Close the key.
        k.Close()
    End Sub

    ''' <summary>
    ''' Saves a registry setting.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Company\Application name.
    ''' </param>
    ''' <param name="Name">
    ''' Setting name.
    ''' </param>
    ''' <param name="Value">
    ''' Value to store.
    ''' </param>
    Private Sub SaveRegistrySetting(ByVal ApplicationName As String, ByVal Name As String, ByVal Value As String)
        Dim k As Microsoft.Win32.RegistryKey

        ' Check inputs.
        If (ApplicationName Is Nothing) OrElse (ApplicationName.Length = 0) Then _
            Throw New ArgumentNullException("ApplicationName") _
                : Exit Sub

        ' Check inputs.
        If (Name Is Nothing) OrElse (Name.Length = 0) Then _
            Throw New ArgumentNullException("Name") _
                : Exit Sub

        ' Try to open the key or create it if not present.
        k = My.Computer.Registry.CurrentUser.CreateSubKey(REG_SUBKEY_PREFIX & ApplicationName & REG_SUBKEY_POSTFIX)

        ' Set the value.
        k.SetValue(Name, Value, Microsoft.Win32.RegistryValueKind.String)

        ' Close the key.
        k.Close()
    End Sub

    ''' <summary>
    ''' Load a registry setting.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Company\Application name.
    ''' </param>
    ''' <param name="Name">
    ''' Setting name.
    ''' </param>
    ''' <param name="Value">
    ''' Value where to load.
    ''' </param>
    Private Sub LoadRegistrySetting(ByVal ApplicationName As String, ByVal Name As String, ByRef Value As Integer)
        Dim k As Microsoft.Win32.RegistryKey, o As Object

        ' Check inputs.
        If (ApplicationName Is Nothing) OrElse (ApplicationName.Length = 0) Then _
            Throw New ArgumentNullException("ApplicationName") _
                : Exit Sub

        ' Check inputs.
        If (Name Is Nothing) OrElse (Name.Length = 0) Then _
            Throw New ArgumentNullException("Name") _
                : Exit Sub

        ' Try to open the key.
        k = My.Computer.Registry.CurrentUser.OpenSubKey(REG_SUBKEY_PREFIX & ApplicationName & REG_SUBKEY_POSTFIX)

        ' Check if key exists.
        If k Is Nothing Then _
            Value = 0 _
                : Exit Sub

        ' Check type of key.
        If k.GetValue(Name) IsNot Nothing Then _
            If k.GetValueKind(Name) <> Microsoft.Win32.RegistryValueKind.DWord Then _
                Trace.Assert(False, "Warning: Invalid type of key.") _
                    : Value = 0 _
                    : k.Close() _
                    : Exit Sub

        ' Read the value.
        o = k.GetValue(Name)

        ' Close the key.
        k.Close()

        ' Set the value.
        If o Is Nothing Then _
            Value = 0 _
            Else _
            Value = CInt(o)
    End Sub

    ''' <summary>
    ''' Load a registry setting.
    ''' </summary>
    ''' <typeparam name="T">
    ''' The type of parameter to load.
    ''' </typeparam>
    ''' <param name="ApplicationName">
    ''' Company\Application name.
    ''' </param>
    ''' <param name="Name">
    ''' Setting name.
    ''' </param>
    ''' <param name="Value">
    ''' Value where to load.
    ''' </param>
    ''' <remarks>
    ''' Typically this should be used to load
    ''' enumerated data types; nothing else.
    ''' </remarks>
    Private Sub LoadRegistrySetting (Of T)(ByVal ApplicationName As String, ByVal Name As String, ByRef Value As T,
                                           Optional ByVal ExpectedKeyType As Microsoft.Win32.RegistryValueKind =
                                              Microsoft.Win32.RegistryValueKind.Unknown)
        Dim k As Microsoft.Win32.RegistryKey, o As Object

        ' Check inputs.
        If (ApplicationName Is Nothing) OrElse (ApplicationName.Length = 0) Then _
            Throw New ArgumentNullException("ApplicationName") _
                : Exit Sub

        ' Check inputs.
        If (Name Is Nothing) OrElse (Name.Length = 0) Then _
            Throw New ArgumentNullException("Name") _
                : Exit Sub

        ' Try to open the key.
        k = My.Computer.Registry.CurrentUser.OpenSubKey(REG_SUBKEY_PREFIX & ApplicationName & REG_SUBKEY_POSTFIX)

        ' Check if key exists.
        If k Is Nothing Then _
            Value = CType(Nothing, T) _
                : Exit Sub

        ' Check type of key.
        If k.GetValue(Name) IsNot Nothing Then _
            If ExpectedKeyType <> Microsoft.Win32.RegistryValueKind.Unknown Then _
                If k.GetValueKind(Name) <> ExpectedKeyType Then _
                    Trace.Assert(False, "Warning: Invalid type of key.") _
                        : Value = CType(Nothing, T) _
                        : Exit Sub

        ' Read the value.
        o = k.GetValue(Name)

        ' Close the key.
        k.Close()

        ' Set the value.
        If o Is Nothing Then _
            Value = CType(CObj(0), T) _
            Else _
            Value = CType(o, T)
    End Sub

    ''' <summary>
    ''' Load a registry setting.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Company\Application name.
    ''' </param>
    ''' <param name="Name">
    ''' Setting name.
    ''' </param>
    ''' <param name="Value">
    ''' Value where to load.
    ''' </param>
    Private Sub LoadRegistrySetting(ByVal ApplicationName As String, ByVal Name As String, ByRef Value As Boolean)
        Dim k As Microsoft.Win32.RegistryKey, o As Object

        ' Check inputs.
        If (ApplicationName Is Nothing) OrElse (ApplicationName.Length = 0) Then _
            Throw New ArgumentNullException("ApplicationName") _
                : Exit Sub

        ' Check inputs.
        If (Name Is Nothing) OrElse (Name.Length = 0) Then _
            Throw New ArgumentNullException("Name") _
                : Exit Sub

        ' Try to open the key.
        k = My.Computer.Registry.CurrentUser.OpenSubKey(REG_SUBKEY_PREFIX & ApplicationName & REG_SUBKEY_POSTFIX)

        ' Check if key exists.
        If k Is Nothing Then _
            Value = False _
                : Exit Sub

        ' Check type of key.
        If k.GetValue(Name) IsNot Nothing Then _
            If k.GetValueKind(Name) <> Microsoft.Win32.RegistryValueKind.DWord Then _
                Trace.Assert(False, "Warning: Invalid type of key.") _
                    : Value = False _
                    : k.Close() _
                    : Exit Sub

        ' Read the value.
        o = k.GetValue(Name)

        ' Close the key.
        k.Close()

        ' Set the value.
        If o Is Nothing Then _
            Value = False _
            Else _
            Value = CBool(IIf(CInt(o) <> 0, True, False))
    End Sub

    ''' <summary>
    ''' Load a registry setting.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Company\Application name.
    ''' </param>
    ''' <param name="Name">
    ''' Setting name.
    ''' </param>
    ''' <param name="Value">
    ''' Value where to load.
    ''' </param>
    Private Sub LoadRegistrySetting(ByVal ApplicationName As String, ByVal Name As String, ByRef Value As String)
        Dim k As Microsoft.Win32.RegistryKey, o As Object

        ' Check inputs.
        If (ApplicationName Is Nothing) OrElse (ApplicationName.Length = 0) Then _
            Throw New ArgumentNullException("ApplicationName") _
                : Exit Sub

        ' Check inputs.
        If (Name Is Nothing) OrElse (Name.Length = 0) Then _
            Throw New ArgumentNullException("Name") _
                : Exit Sub

        ' Try to open the key.
        k = My.Computer.Registry.CurrentUser.OpenSubKey(REG_SUBKEY_PREFIX & ApplicationName & REG_SUBKEY_POSTFIX)

        ' Check if key exists.
        If k Is Nothing Then _
            Value = "" _
                : k.Close() _
                : Exit Sub

        ' Check type of key.
        If k.GetValue(Name) IsNot Nothing Then _
            If (k.GetValueKind(Name) <> Microsoft.Win32.RegistryValueKind.String) AndAlso
               (k.GetValueKind(Name) <> Microsoft.Win32.RegistryValueKind.MultiString) Then _
                Trace.Assert(False, "Warning: Invalid type of key.") _
                    : Value = "" _
                    : k.Close() _
                    : Exit Sub

        ' Read the value.
        o = k.GetValue(Name)

        ' Close the key.
        k.Close()

        ' Set the value.
        If o Is Nothing Then _
            Value = "" _
            Else _
            Value = CStr(o)
    End Sub

    ''' <summary>
    ''' Saves the adapter ordinal.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Company\Application name.
    ''' </param>
    ''' <param name="Adapter">
    ''' Value to store.
    ''' </param>
    Public Sub SaveAdapter(ByVal ApplicationName As String, ByVal Adapter As Integer)
        ' Verify adapter ordinal.
        If ValidateAdapter(Adapter) <> Adapter Then _
            Trace.Assert(False, "Warning: Invalid adapter count.") _
                : Adapter = ValidateAdapter(Adapter)

        ' Save the setting.
        SaveRegistrySetting(ApplicationName, REG_ADAPTER, Adapter)
    End Sub

    ''' <summary>
    ''' Saves the device type.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Company\Application name.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Value to store.
    ''' </param>
    Public Sub SaveDeviceType(ByVal ApplicationName As String, ByVal _DeviceType As DeviceType)
        ' Verify device type.
        If ValidateDeviceType(_DeviceType) <> _DeviceType Then _
            Trace.Assert(False, "Warning: Invalid '_DeviceType'.") _
                : _DeviceType = ValidateDeviceType(_DeviceType)

        ' Save the setting.
        SaveRegistrySetting(ApplicationName, REG_DEVICETYPE, _DeviceType)
    End Sub

    ''' <summary>
    ''' Saves the creation flags.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Company\Application name.
    ''' </param>
    ''' <param name="_CreateFlags">
    ''' Value to store.
    ''' </param>
    Public Sub SaveCreateFlags(ByVal ApplicationName As String, ByVal _CreateFlags As CreateFlags)
        ' Check create flags.
        If ValidateCreateFlags(_CreateFlags) <> _CreateFlags Then _
            Trace.Assert(False, "Warning: Invalid '_CreateFlags'.") _
                : _CreateFlags = ValidateCreateFlags(_CreateFlags)

        ' Now save the setting.
        SaveRegistrySetting(ApplicationName, REG_CREATEFLAGS, _CreateFlags)
    End Sub

    ''' <summary>
    ''' Saves the presentation paramters.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Company\Application name.
    ''' </param>
    ''' <param name="_PresentationParameters">
    ''' Value to store.
    ''' </param>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>_PresentationParamters Is Nothing</c>.
    ''' </exception>
    Public Sub SavePresentationParameters(ByVal ApplicationName As String,
                                          ByVal _PresentationParameters As PresentParameters)
        Dim PP As PresentParameters

        ' Check inputs.
        If _PresentationParameters Is Nothing Then _
            Throw New ArgumentNullException("_PresentationParameters") _
                : Exit Sub

        ' Check presentation parameters.
        If Not ValidatePresentParametersA(_PresentationParameters) Then _
            Trace.Assert(False, "Warning: Invalid '_PresentationParameters'.")

        ' Validate and copy presentation parameters.
        PP = ValidatePresentParameters(_PresentationParameters)

        ' Save the settings.
        With PP
            SaveRegistrySetting(ApplicationName, REG_WIDTH, .BackBufferWidth)
            SaveRegistrySetting(ApplicationName, REG_HEIGHT, .BackBufferHeight)
            SaveRegistrySetting(ApplicationName, REG_FORMAT, .BackBufferFormat)
            SaveRegistrySetting(ApplicationName, REG_BACKBUFFERCOUNT, .BackBufferCount)

            SaveRegistrySetting(ApplicationName, REG_ENABLEAUTODEPTHSTENCIL, .EnableAutoDepthStencil)
            SaveRegistrySetting(ApplicationName, REG_DEPTHSTENCILFORMAT, .AutoDepthStencilFormat)

            SaveRegistrySetting(ApplicationName, REG_REFRESHRATE, .FullScreenRefreshRateInHz)
            SaveRegistrySetting(ApplicationName, REG_PRESENTATIONINTERVAL, .PresentationInterval)
            SaveRegistrySetting(ApplicationName, REG_PRESENTATIONFLAG, .PresentFlag)

            SaveRegistrySetting(ApplicationName, REG_SWAPEFFECT, .SwapEffect)
            SaveRegistrySetting(ApplicationName, REG_MULTISAMPLETYPE, .MultiSample)
            SaveRegistrySetting(ApplicationName, REG_MULTISAMPLEQUALITY, .MultiSampleQuality)

            SaveRegistrySetting(ApplicationName, REG_WINDOWED, .Windowed)
            SaveRegistrySetting(ApplicationName, REG_FORCENOMULTITHREADEDFLAG, .ForceNoMultiThreadedFlag)

        End With ' With PP
    End Sub

    ''' <summary>
    ''' Loads the adapter ordinal.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Company\Application name.
    ''' </param>
    Public Function LoadAdapter(ByVal ApplicationName As String) As Integer
        Dim Adapter As Integer

        ' Load the setting if it exists.
        If SettingExists(ApplicationName, REG_ADAPTER) Then _
            LoadRegistrySetting(ApplicationName, REG_ADAPTER, Adapter) _
            Else _
            Adapter = Manager.Adapters.Default.Adapter

        ' Verify adapter ordinal.
        If ValidateAdapter(Adapter) <> Adapter Then _
            Trace.Assert(False, "Warning: Invalid adapter reference.") _
                : Adapter = ValidateAdapter(Adapter)

        Return Adapter
    End Function

    ''' <summary>
    ''' Loads the device type.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Company\Application name.
    ''' </param>
    Public Function LoadDeviceType(ByVal ApplicationName As String) As DeviceType
        Dim _DeviceType As DeviceType

        ' Load the setting if it exists.
        If SettingExists(ApplicationName, REG_DEVICETYPE) Then _
            LoadRegistrySetting(ApplicationName, REG_DEVICETYPE, _DeviceType) _
            Else _
            _DeviceType = DeviceType.Hardware

        ' Verify device type.
        If ValidateDeviceType(_DeviceType) <> _DeviceType Then _
            Trace.Assert(False, "Warning: Invalid device type reference.") _
                : _DeviceType = ValidateDeviceType(_DeviceType)

        ' Check the device type.
        If Not CheckDeviceType(LoadAdapter(ApplicationName), _DeviceType) Then _
            Trace.Assert(False, "Warning: Device type not supported.") _
                : _DeviceType = DeviceType.Hardware

        Return _DeviceType
    End Function

    ''' <summary>
    ''' Loads the creation flags.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Company\Application name.
    ''' </param>
    Public Function LoadCreateFlags(ByVal ApplicationName As String) As CreateFlags
        Dim Adapter As Integer, _DeviceType As DeviceType
        Dim _CreateFlags As CreateFlags

        ' Load the adapter to use.
        Adapter = LoadAdapter(ApplicationName)

        ' Load the device type to use.
        _DeviceType = LoadDeviceType(ApplicationName)

        ' Load the setting if it exists.
        If SettingExists(ApplicationName, REG_DEVICETYPE) Then _
            LoadRegistrySetting(ApplicationName, REG_CREATEFLAGS, _CreateFlags) _
            Else _
            _CreateFlags = CreateFlags.SoftwareVertexProcessing

        ' Validate and return the flags.
        Return ValidateCreateFlags(Adapter, _DeviceType, _CreateFlags)
    End Function

    ''' <summary>
    ''' Loads the presentation paramters.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Company\Application name.
    ''' </param>
    Public Function LoadPresentParameters(ByVal ApplicationName As String) As PresentParameters
        Dim Adapter As Integer, _DeviceType As DeviceType
        Dim DispMode As DisplayMode
        Dim _PresentParameters As New PresentParameters

        ' Get the adapter ordinal.
        Adapter = LoadAdapter(ApplicationName)

        ' Get the device type.
        _DeviceType = LoadDeviceType(ApplicationName)

        ' Get the display format.
        DispMode = Manager.Adapters(Adapter).CurrentDisplayMode

        ' Load the settings.
        With _PresentParameters
            LoadRegistrySetting(ApplicationName, REG_WIDTH, .BackBufferWidth)
            LoadRegistrySetting(ApplicationName, REG_HEIGHT, .BackBufferHeight)
            LoadRegistrySetting(ApplicationName, REG_FORMAT, .BackBufferFormat)
            LoadRegistrySetting(ApplicationName, REG_BACKBUFFERCOUNT, .BackBufferCount)

            LoadRegistrySetting(ApplicationName, REG_ENABLEAUTODEPTHSTENCIL, .EnableAutoDepthStencil)
            LoadRegistrySetting(ApplicationName, REG_DEPTHSTENCILFORMAT, .AutoDepthStencilFormat)

            LoadRegistrySetting(ApplicationName, REG_REFRESHRATE, .FullScreenRefreshRateInHz)
            LoadRegistrySetting(ApplicationName, REG_PRESENTATIONINTERVAL, .PresentationInterval)
            LoadRegistrySetting(ApplicationName, REG_PRESENTATIONFLAG, .PresentFlag)

            LoadRegistrySetting(ApplicationName, REG_SWAPEFFECT, .SwapEffect)
            LoadRegistrySetting(ApplicationName, REG_MULTISAMPLETYPE, .MultiSample)
            LoadRegistrySetting(ApplicationName, REG_MULTISAMPLEQUALITY, .MultiSampleQuality)

            LoadRegistrySetting(ApplicationName, REG_WINDOWED, .Windowed)
            LoadRegistrySetting(ApplicationName, REG_FORCENOMULTITHREADEDFLAG, .ForceNoMultiThreadedFlag)
        End With ' With DH_Reg_LoadPresentationParameters

        ' Validate the parameters.
        _PresentParameters = ValidatePresentParameters(_PresentParameters)

        ' Find out whether the device can be created. If not, set the lowest possible supported settings.
        If Not CanCreateDevice(Adapter, _DeviceType, CreateFlags.SoftwareVertexProcessing, _PresentParameters) Then
            Trace.Assert(False, "Warning: Device settings not supported " &
                                "setting default configuration.")

            ' Set the lowest possible settings guaranteed to work.
            With _PresentParameters
                .BackBufferWidth = DispMode.Width
                .BackBufferHeight = DispMode.Height
                .BackBufferFormat = DispMode.Format
                .BackBufferCount = 1

                .EnableAutoDepthStencil = False
                .AutoDepthStencilFormat = DepthFormat.Unknown

                .FullScreenRefreshRateInHz = 0
                .PresentationInterval = PresentInterval.Default
                .PresentFlag = PresentFlag.None

                .SwapEffect = SwapEffect.Discard
                .MultiSample = MultiSampleType.None
                .MultiSampleQuality = 0

                .Windowed = True
                .ForceNoMultiThreadedFlag = False
            End With ' With _PresentParameters

        End If _
        ' If Not CanCreateDevice(Adapter, _DeviceType, CreateFlags.SoftwareVertexProcessing, _PresentParameters) Then

        Return _PresentParameters
    End Function

    ''' <summary>
    ''' Returns whether or not the complete set of settings
    ''' is present in the registry or not.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Name of the application.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if the whole set of settings exist and 
    ''' are of correct type.
    ''' </returns>
    Public Function SettingsExists(ByVal ApplicationName As String) As Boolean
        ' Check creation parameters.
        If (Not SettingExists(ApplicationName, REG_ADAPTER)) OrElse
           (SettingType(ApplicationName, REG_ADAPTER) <> Microsoft.Win32.RegistryValueKind.DWord) Then _
            Return False

        If (Not SettingExists(ApplicationName, REG_DEVICETYPE)) OrElse
           (SettingType(ApplicationName, REG_ADAPTER) <> Microsoft.Win32.RegistryValueKind.DWord) Then _
            Return False

        If (Not SettingExists(ApplicationName, REG_CREATEFLAGS)) OrElse
           (SettingType(ApplicationName, REG_ADAPTER) <> Microsoft.Win32.RegistryValueKind.DWord) Then _
            Return False

        ' Check presentation parameters.
        If (Not SettingExists(ApplicationName, REG_WIDTH)) OrElse
           (SettingType(ApplicationName, REG_ADAPTER) <> Microsoft.Win32.RegistryValueKind.DWord) Then _
            Return False

        If (Not SettingExists(ApplicationName, REG_HEIGHT)) OrElse
           (SettingType(ApplicationName, REG_ADAPTER) <> Microsoft.Win32.RegistryValueKind.DWord) Then _
            Return False

        If (Not SettingExists(ApplicationName, REG_FORMAT)) OrElse
           (SettingType(ApplicationName, REG_ADAPTER) <> Microsoft.Win32.RegistryValueKind.DWord) Then _
            Return False

        If (Not SettingExists(ApplicationName, REG_BACKBUFFERCOUNT)) OrElse
           (SettingType(ApplicationName, REG_ADAPTER) <> Microsoft.Win32.RegistryValueKind.DWord) Then _
            Return False

        If (Not SettingExists(ApplicationName, REG_ENABLEAUTODEPTHSTENCIL)) OrElse
           (SettingType(ApplicationName, REG_ADAPTER) <> Microsoft.Win32.RegistryValueKind.DWord) Then _
            Return False

        If (Not SettingExists(ApplicationName, REG_DEPTHSTENCILFORMAT)) OrElse
           (SettingType(ApplicationName, REG_ADAPTER) <> Microsoft.Win32.RegistryValueKind.DWord) Then _
            Return False

        If (Not SettingExists(ApplicationName, REG_REFRESHRATE)) OrElse
           (SettingType(ApplicationName, REG_ADAPTER) <> Microsoft.Win32.RegistryValueKind.DWord) Then _
            Return False

        If (Not SettingExists(ApplicationName, REG_PRESENTATIONINTERVAL)) OrElse
           (SettingType(ApplicationName, REG_ADAPTER) <> Microsoft.Win32.RegistryValueKind.DWord) Then _
            Return False

        If (Not SettingExists(ApplicationName, REG_PRESENTATIONFLAG)) OrElse
           (SettingType(ApplicationName, REG_ADAPTER) <> Microsoft.Win32.RegistryValueKind.DWord) Then _
            Return False

        If (Not SettingExists(ApplicationName, REG_MULTISAMPLETYPE)) OrElse
           (SettingType(ApplicationName, REG_ADAPTER) <> Microsoft.Win32.RegistryValueKind.DWord) Then _
            Return False

        If (Not SettingExists(ApplicationName, REG_MULTISAMPLEQUALITY)) OrElse
           (SettingType(ApplicationName, REG_ADAPTER) <> Microsoft.Win32.RegistryValueKind.DWord) Then _
            Return False

        If (Not SettingExists(ApplicationName, REG_WINDOWED)) OrElse
           (SettingType(ApplicationName, REG_ADAPTER) <> Microsoft.Win32.RegistryValueKind.DWord) Then _
            Return False

        If (Not SettingExists(ApplicationName, REG_FORCENOMULTITHREADEDFLAG)) OrElse
           (SettingType(ApplicationName, REG_ADAPTER) <> Microsoft.Win32.RegistryValueKind.DWord) Then _
            Return False

        ' Probably all settings exist and are of correct type.
        Return True
    End Function

    ''' <summary>
    ''' Deletes all settings stored in the registry.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Name of the application.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if the settings were deleted.
    ''' </returns>
    Public Function DeleteSettings(ByVal ApplicationName As String) As Boolean
        Try
            ' Delete the sub-key.
            Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree(
                REG_SUBKEY_PREFIX & ApplicationName & REG_SUBKEY_POSTFIX)

        Catch ex As Exception
            Return False

        End Try

        Return True
    End Function
End Module
