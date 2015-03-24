''' <summary>
''' Module containing code for performing Direct3D
''' settings\conifuration settings.
''' </summary>
Friend Module Validator
    ''' <summary>
    ''' Validates an adapter ordinal.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Input value.
    ''' </param>
    ''' <returns>
    ''' Validated value.
    ''' </returns>
    Public Function ValidateAdapter(ByVal Adapter As Integer) As Integer
        ' Check if value is out of range.
        If (Adapter < 0) Or (Adapter >= Manager.Adapters.Count) Then _
            Trace.Assert(False, "Warning: Invalid adapter ordinal.") _
                : Return Manager.Adapters.Default.Adapter

        Return Adapter
    End Function

    ''' <summary>
    ''' Validates a device type.
    ''' </summary>
    ''' <param name="_DeviceType">
    ''' Input value.
    ''' </param>
    ''' <returns>
    ''' Checked value.
    ''' </returns>
    Public Function ValidateDeviceType(ByVal _DeviceType As DeviceType) As DeviceType
        ' Check if device type is anything other than what's possible.
        If (_DeviceType <> DeviceType.Hardware) AndAlso
           (_DeviceType <> DeviceType.Reference) AndAlso
           (_DeviceType <> DeviceType.Software) AndAlso
           (_DeviceType <> DeviceType.NullReference) Then _
            Trace.Assert(False, "Warning: Invalid '_DeviceType'.") _
                : Return DeviceType.Hardware

        Return _DeviceType
    End Function

    ''' <summary>
    ''' Validates a device type.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Input value.
    ''' </param>
    ''' <returns>
    ''' Checked value.
    ''' </returns>
    ''' <remarks>
    ''' This also checks for compatibility on given devices.
    ''' </remarks>
    Public Function ValidateDeviceType(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType) As DeviceType
        ' Check inputs.
        If ValidateAdapter(Adapter) <> Adapter Then _
            Throw New ArgumentException("Invalid 'Adapter'.") _
                : Exit Function

        ' First validate the device type for any incompatibility.
        _DeviceType = ValidateDeviceType(_DeviceType)

        ' Now check for support.
        If Not CheckDeviceType(Adapter, _DeviceType) Then _
            Trace.Assert(False, "Warning: '_DeviceType' not supported.") _
                : _DeviceType = DeviceType.Hardware

        Return _DeviceType
    End Function

    ''' <summary>
    ''' Validates creation flags
    ''' </summary>
    ''' <param name="_CreateFlags">
    ''' Input value.
    ''' </param>
    ''' <returns>
    ''' Checked value.
    ''' </returns>
    Public Function ValidateCreateFlags(ByVal _CreateFlags As CreateFlags) As CreateFlags
        Dim FPUP, MT, PD As Boolean
        Dim HVP, SVP, MVP As Boolean
        Dim DDM, AGD, DDMX, NWC As Boolean
        Dim NewCreateFlags As CreateFlags

        FPUP = ((_CreateFlags And CreateFlags.FpuPreserve) = CreateFlags.FpuPreserve)
        MT = ((_CreateFlags And CreateFlags.MultiThreaded) = CreateFlags.MultiThreaded)
        PD = ((_CreateFlags And CreateFlags.PureDevice) = CreateFlags.PureDevice)

        SVP = ((_CreateFlags And CreateFlags.SoftwareVertexProcessing) = CreateFlags.SoftwareVertexProcessing)
        HVP = ((_CreateFlags And CreateFlags.HardwareVertexProcessing) = CreateFlags.HardwareVertexProcessing)
        MVP = ((_CreateFlags And CreateFlags.MixedVertexProcessing) = CreateFlags.MixedVertexProcessing)

        DDM = ((_CreateFlags And CreateFlags.DisableDriverManagement) = CreateFlags.DisableDriverManagement)
        AGD = ((_CreateFlags And CreateFlags.AdapterGroupDevice) = CreateFlags.AdapterGroupDevice)
        DDMX = ((_CreateFlags And CreateFlags.DisableDriverManagementEx) = CreateFlags.DisableDriverManagementEx)
        NWC = ((_CreateFlags And CreateFlags.NoWindowChanges) = CreateFlags.NoWindowChanges)

        ' Check if there is no SWTL, HWTL and MVP flag.
        If Not (SVP OrElse HVP OrElse MVP) Then _
            Trace.Assert(False, "Warning: No SWTL, HWTL or MVP flags set.") _
                : _CreateFlags = _CreateFlags Or CreateFlags.SoftwareVertexProcessing _
                : SVP = True

        ' Check if there is a PD flag without HWTL.
        If PD And Not HVP Then _
            Trace.Assert(False, "Warning: Pure device set without HWTL flag.") _
                : _CreateFlags = _CreateFlags And Not CreateFlags.PureDevice _
                : PD = False

        ' Check if there are two or more than two mutually exclusive flags set.
        If (SVP AndAlso HVP) OrElse (HVP AndAlso MVP) OrElse (MVP AndAlso SVP) Then _
            Trace.Assert(False, "Warning: Two or more mutually exclusive flags for creation parameters are set.") _
                : _CreateFlags = _CreateFlags And Not CreateFlags.HardwareVertexProcessing _
                : _CreateFlags = _CreateFlags And Not CreateFlags.MixedVertexProcessing _
                : _CreateFlags = _CreateFlags Or CreateFlags.SoftwareVertexProcessing _
                : HVP = False _
                : MVP = False _
                : SVP = True

        ' Check if both DDM and DDMX flags are set.
        If DDM AndAlso DDMX Then _
            Trace.Assert(False, "Warning: Two or more mutually exclusive flags for creation parameters are set.") _
                : _CreateFlags = _CreateFlags And Not CreateFlags.DisableDriverManagement _
                : _CreateFlags = _CreateFlags Or CreateFlags.DisableDriverManagementEx _
                : DDM = False _
                : DDMX = True

        ' Now build the flags.
        NewCreateFlags = CType(IIf(FPUP, CreateFlags.FpuPreserve, False), CreateFlags) Or
                         CType(IIf(MT, CreateFlags.MultiThreaded, False), CreateFlags) Or
                         CType(IIf(PD, CreateFlags.PureDevice, False), CreateFlags) Or
                         CType(IIf(SVP, CreateFlags.SoftwareVertexProcessing, False), CreateFlags) Or
                         CType(IIf(HVP, CreateFlags.HardwareVertexProcessing, False), CreateFlags) Or
                         CType(IIf(MVP, CreateFlags.MixedVertexProcessing, False), CreateFlags) Or
                         CType(IIf(DDM, CreateFlags.DisableDriverManagement, False), CreateFlags) Or
                         CType(IIf(AGD, CreateFlags.AdapterGroupDevice, False), CreateFlags) Or
                         CType(IIf(DDMX, CreateFlags.DisableDriverManagementEx, False), CreateFlags) Or
                         CType(IIf(NWC, CreateFlags.NoWindowChanges, False), CreateFlags)

        ' Compare for residue.
        Trace.Assert(NewCreateFlags = _CreateFlags,
                     "Warning: '_CreateFlags' contains one or more unknown bits.")

        ' Return the flags.
        Return NewCreateFlags
    End Function

    ''' <summary>
    ''' Validates creation flags.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to check.
    ''' </param>
    ''' <param name="_CreateFlags">
    ''' Input value.
    ''' </param>
    ''' <returns>
    ''' Checked value.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    ''' <remarks>
    ''' This also checks for compatibility on given devices.
    ''' </remarks>
    Public Function ValidateCreateFlags(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                        ByVal _CreateFlags As CreateFlags) As CreateFlags
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

        ' First validate the creation flags for any incompatibility.
        _CreateFlags = ValidateCreateFlags(_CreateFlags)

        ' Pure Device enabled but not available.
        If ((_CreateFlags And CreateFlags.PureDevice) = CreateFlags.PureDevice) AndAlso
           (Not CheckPureDevice(Adapter, _DeviceType)) Then _
            Trace.Assert(False, "Warning: Pure Device enabled but not available.") _
                : _CreateFlags = _CreateFlags And Not CreateFlags.PureDevice

        ' Hardware Vertex Processing enabled but not available.
        If ((_CreateFlags And CreateFlags.HardwareVertexProcessing) = CreateFlags.HardwareVertexProcessing) AndAlso
           (Not CheckHardwareVertexProcessing(Adapter, _DeviceType)) Then _
            Trace.Assert(False, "Warning: Hardware Vertex Processing enabled but not available.") _
                : _CreateFlags = _CreateFlags And Not CreateFlags.HardwareVertexProcessing

        ' Mixed Vertex Processing enabled but not available.
        If ((_CreateFlags And CreateFlags.MixedVertexProcessing) = CreateFlags.MixedVertexProcessing) AndAlso
           (Not CheckHardwareVertexProcessing(Adapter, _DeviceType)) Then _
            Trace.Assert(False, "Warning: Mixed Vertex Processing enabled but not available.") _
                : _CreateFlags = _CreateFlags And Not CreateFlags.MixedVertexProcessing

        ' Once again, perform validation.
        Return ValidateCreateFlags(_CreateFlags)
    End Function

    ''' <summary>
    ''' Validates a <c>PresentInterval</c> value.
    ''' </summary>
    ''' <param name="_PresentInterval">
    ''' Input value.
    ''' </param>
    ''' <returns>
    ''' Checked value.
    ''' </returns>
    Public Function ValidatePresentInterval(ByVal _PresentInterval As PresentInterval) As PresentInterval
        Dim OldPresentInterval As PresentInterval

        ' Cache old present interval.
        OldPresentInterval = _PresentInterval

        Select Case _PresentInterval
            ' KNOWN VALUE
            Case PresentInterval.Default, PresentInterval.One, PresentInterval.Two,
                PresentInterval.Three, PresentInterval.Four, PresentInterval.Immediate
                Return _PresentInterval

                ' UNKOWN (error)
            Case Else
                Trace.Assert(False, "Warning: '_PresentInterval' contains an unknown value.")

                Return PresentInterval.Default

        End Select
    End Function

    ''' <summary>
    ''' Validates a <c>PresentFlag</c> value.
    ''' </summary>
    ''' <param name="_PresentFlag">
    ''' Input value.
    ''' </param>
    ''' <returns>
    ''' Checked value.
    ''' </returns>
    Public Function ValidatePresentFlag(ByVal _PresentFlag As PresentFlag) As PresentFlag
        ' Check for unknown bits.
        If ((_PresentFlag And Not (PresentFlag.LockableBackBuffer Or
                                   PresentFlag.DiscardDepthStencil Or
                                   PresentFlag.DeviceClip Or
                                   PresentFlag.Video)) <> PresentFlag.None) Then _
            Trace.Assert(False, "Warning: '_PresentFlag' has unknown bits set.") _
                : Return _PresentFlag And Not (PresentFlag.LockableBackBuffer Or
                                               PresentFlag.DiscardDepthStencil Or
                                               PresentFlag.DeviceClip Or
                                               PresentFlag.Video)

        Return _PresentFlag
    End Function

    ''' <summary>
    ''' Validates a multi-sample type value.
    ''' </summary>
    ''' <param name="_MultiSampleType">
    ''' Input value.
    ''' </param>
    ''' <returns>
    ''' Checked value.
    ''' </returns>
    Public Function ValidateMultiSampleType(ByVal _MultiSampleType As MultiSampleType) As MultiSampleType
        ' Check for flag.
        If (_MultiSampleType = MultiSampleType.None) OrElse
           (_MultiSampleType = MultiSampleType.NonMaskable) Then _
            Return _MultiSampleType

        ' Check for sample count.
        If (_MultiSampleType >= MultiSampleType.TwoSamples) AndAlso
           (_MultiSampleType <= MultiSampleType.SixteenSamples) Then _
            Return _MultiSampleType _
            Else _
            Trace.Assert(False, "Warning: '_MultiSampleType' has an unknown value.") _
                : Return MultiSampleType.None
    End Function

    ''' <summary>
    ''' Validates a multi-sample quality value.
    ''' </summary>
    ''' <param name="MultiSampleQuality">
    ''' Input value.
    ''' </param>
    ''' <returns>
    ''' Checked value.
    ''' </returns>
    Public Function ValidateMultiSampleQuality(ByVal MultiSampleQuality As Integer) As Integer
        ' Check for negative values.
        If MultiSampleQuality < 0 Then _
            Trace.Assert(False, "Warning: 'MultiSampleQuality' has an invalid value.") _
                : Return 0

        Return MultiSampleQuality
    End Function

    ''' <summary>
    ''' Validates a swap-effect value.
    ''' </summary>
    ''' <param name="_SwapEffect">
    ''' Input value.
    ''' </param>
    ''' <returns>
    ''' Checked value.
    ''' </returns>
    Public Function ValidateSwapEffect(ByVal _SwapEffect As SwapEffect) As SwapEffect
        Select Case _SwapEffect
            Case SwapEffect.Discard, SwapEffect.Flip, SwapEffect.Copy
                ' It is one of the listed formats.
                Return _SwapEffect
            Case Else
                Trace.Assert(False, "Warning: '_SwapEffect' has an invalid value.")

                Return SwapEffect.Discard

        End Select ' Select Case _SwapEffect
    End Function

    ''' <summary>
    ''' Validates the presentation parameters.
    ''' </summary>
    ''' <param name="_PresentParameters">
    ''' Input object.
    ''' </param>
    ''' <returns>
    ''' Checked object.
    ''' </returns>
    ''' <remarks>
    ''' This modifies the existing object.
    ''' </remarks>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>_PresentParameters Is Nothing</c>.
    ''' </exception>
    Public Function ValidatePresentParameters(ByVal _PresentParameters As PresentParameters) As PresentParameters
        ' Check inputs.
        If _PresentParameters Is Nothing Then _
            Throw New ArgumentNullException("_PresentParameters") _
                : Exit Function

        With _PresentParameters
            ' ----------------------------------
            ' Check if depth stencil is enabled.
            ' ----------------------------------
            If .EnableAutoDepthStencil Then
                ' Check depth stencil format.
                If Not FormatIsDepthStencil(.AutoDepthStencilFormat) Then _
                    Trace.Assert(False,
                                 "Warning: DepthStencil format seems to be invalid: " &
                                 .AutoDepthStencilFormat.ToString() & ".") _
                        : .EnableAutoDepthStencil = False _
                        : .AutoDepthStencilFormat = DepthFormat.Unknown

            Else ' If PP.EnableAutoDepthStencil Then
                ' Clear depth-stencil format.
                .AutoDepthStencilFormat = DepthFormat.Unknown

            End If ' If PP.EnableAutoDepthStencil Then

            ' ------------------------
            ' Check backbuffer format.
            ' ------------------------
            If Not FormatIsRGB(.BackBufferFormat) Then _
                Trace.Assert(False,
                             "Warning: Backbuffer format seems to be invalid: " & .BackBufferFormat.ToString() & ".") _
                    : .BackBufferFormat = Format.Unknown

            ' ---------------------------------------------
            ' Check refresh rate and presentation interval.
            ' ---------------------------------------------
            If .Windowed Then
                ' Set these flags to zero.
                .FullScreenRefreshRateInHz = 0

            Else ' If PP.Windowed Then
                ' Check the refresh rate.
                If .FullScreenRefreshRateInHz < 0 Then _
                    Trace.Assert(False, "Warning: Invalid 'FullScreenRefreshRateInHz'.") _
                        : .FullScreenRefreshRateInHz = 0

                ' Check the presentation inverval.
                If ValidatePresentInterval(.PresentationInterval) <> .PresentationInterval Then _
                    Trace.Assert(False, "Warning: Unknown presentation interval: " & .PresentationInterval.ToString()) _
                        : .PresentationInterval = PresentInterval.Default

            End If ' If PP.Windowed Then

            ' --------------------
            ' Check present flags.
            ' --------------------
            If ValidatePresentFlag(.PresentFlag) <> .PresentFlag Then _
                Trace.Assert(False, "Warning: Presentation flags seem to be invalid.") _
                    : .PresentFlag = ValidatePresentFlag(.PresentFlag)

            ' Multi-sampling not compatible with lockable back buffers.
            If ((.PresentFlag And PresentFlag.DiscardDepthStencil) = PresentFlag.DiscardDepthStencil) AndAlso
               (.MultiSample <> MultiSampleType.None) Then _
                Trace.Assert(False, "Warning: Multisampling incompatible with lockable depth-stencil buffers.") _
                    : .MultiSample = MultiSampleType.None _
                    : .MultiSampleQuality = 0

            ' Lockable depth buffer with discard stencil option.
            If ((.PresentFlag And PresentFlag.DiscardDepthStencil) = PresentFlag.DiscardDepthStencil) AndAlso
               ((.AutoDepthStencilFormat = DepthFormat.D16Lockable) OrElse
                (.AutoDepthStencilFormat = DepthFormat.D32SingleLockable)) Then _
                Trace.Assert(False, "Warning: Lockable depth buffer incompatible with discard depth stencil option.") _
                    : If .AutoDepthStencilFormat = DepthFormat.D16Lockable Then _
                        .AutoDepthStencilFormat = DepthFormat.D16 _
                        Else _
                        .AutoDepthStencilFormat = DepthFormat.D32

            ' --------------------------------------
            ' Check multi-sampling type and quality.
            ' --------------------------------------
            If ValidateMultiSampleType(.MultiSample) <> .MultiSample Then _
                Trace.Assert(False, "Warning: Unknown multisampling technique: " & .MultiSample.ToString() & ".") _
                    : .MultiSample = MultiSampleType.None

            ' ------------------------
            ' Check back-buffer count.
            ' ------------------------
            If .BackBufferCount < 0 Then _
                Trace.Assert(False, "Warning: Invalid backbuffer count.") _
                    : .BackBufferCount = 1

            If .BackBufferCount > Present.BackBuffersMax Then _
                Trace.Assert(False, "Warning: BackBufferCount greater than maximum backbuffer count.") _
                    : .BackBufferCount = 1

            If .BackBufferCount = 0 Then _
                .BackBufferCount = 1

            ' -----------------------
            ' Check back-buffer size.
            ' -----------------------
            If .BackBufferWidth < 0 Then _
                Trace.Assert(False, "Warning: 'BackBufferWidth' is invalid.") _
                    : .BackBufferWidth = 0

            If .BackBufferHeight < 0 Then _
                Trace.Assert(False, "Warning: 'BackBufferHeight' is invalid.") _
                    : .BackBufferHeight = 0

            ' ------------------
            ' Check swap effect.
            ' ------------------
            If ValidateSwapEffect(.SwapEffect) <> .SwapEffect Then _
                Trace.Assert(False, "Warning: Unknown swap effect: " & .SwapEffect.ToString() & ".") _
                    : .SwapEffect = SwapEffect.Discard

            ' Check if multisampling is enabled, but swap effect is not 'Discard'.
            If (.MultiSample <> MultiSampleType.None) AndAlso (.SwapEffect <> SwapEffect.Discard) Then _
                Trace.Assert(False, "Warning: Use of multisampling requires 'SwapEffect.Discard'.") _
                    : .SwapEffect = SwapEffect.Discard

            ' Check if single buffers is used and swap effect is 'Flip'.
            If (.BackBufferCount = 1) AndAlso (.SwapEffect = SwapEffect.Flip) Then _
                Trace.Assert(False, "Warning: 'SwapEffect.Flip' requires atleast 2 backbuffers.") _
                    : .SwapEffect = SwapEffect.Discard

        End With ' With _PresentParameters

        Return _PresentParameters
    End Function

    ''' <summary>
    ''' Validates the presentation parameters.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to check.
    ''' </param>
    ''' <param name="_PresentParameters">
    ''' Input object.
    ''' </param>
    ''' <returns>
    ''' Checked object.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>_PresentParameters Is Nothing</c>.
    ''' </exception>
    ''' <remarks>
    ''' This also checks for compatibility on given devices.
    ''' Also, this modifies the existing object.
    ''' </remarks>
    Public Function ValidatePresentParameters(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                              ByVal _PresentParameters As PresentParameters) As PresentParameters
        Dim DispMode As DisplayMode

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

        ' Get the current display mode.
        DispMode = Manager.Adapters(Adapter).CurrentDisplayMode

        ' First validate the creation flags for any incompatibility.
        _PresentParameters = ValidatePresentParameters(_PresentParameters)

        ' Now perform actual validation.
        With _PresentParameters
            ' Check width.
            If .BackBufferWidth = 0 Then _
                Trace.Assert(False, "Warning: Back Buffer width was changed.") _
                    : .BackBufferWidth = DispMode.Width

            ' Check height.
            If .BackBufferHeight = 0 Then _
                Trace.Assert(False, "Warning: Back Buffer height was changed.") _
                    : .BackBufferHeight = DispMode.Height

            ' Check display format.
            If .BackBufferFormat = Format.Unknown Then _
                Trace.Assert(False, "Warning: Back Buffer format was changed.") _
                    : .BackBufferFormat = DispMode.Format

            ' Display mode not available.
            If Not .Windowed Then _
                If Not CheckDisplayMode(Adapter, .BackBufferWidth, .BackBufferHeight) Then _
                    Trace.Assert(False, "Warning: Display mode not available.") _
                        : _PresentParameters.BackBufferWidth = DispMode.Width _
                        : _PresentParameters.BackBufferHeight = DispMode.Height

            ' Display format not available.
            If Not CheckDisplayFormat(Adapter, _DeviceType,
                                      Manager.Adapters(Adapter).CurrentDisplayMode.Format,
                                      .BackBufferFormat, .Windowed) Then _
                Trace.Assert(False, "Warning: Display format not available.") _
                    : _PresentParameters.BackBufferFormat = DispMode.Format

            ' Is Depth-Stencil enabled?
            If .EnableAutoDepthStencil Then
                ' Depth-Stencil format not available.
                If Not CheckDepthStencilFormat(Adapter, _DeviceType,
                                               Manager.Adapters(Adapter).CurrentDisplayMode.Format,
                                               .BackBufferFormat, .AutoDepthStencilFormat, .Windowed) Then _
                    Trace.Assert(False, "Warning: Depth-Stencil format not available.") _
                        : .EnableAutoDepthStencil = False _
                        : .AutoDepthStencilFormat = DepthFormat.Unknown
            End If ' If .EnableAutoDepthStencil Then

            ' Is multi-sampling enabled?
            If .MultiSample <> MultiSampleType.None Then
                ' Multi-sampling technique\quality not available.
                If Not CheckMultiSampleType(Adapter, _DeviceType,
                                            Manager.Adapters(Adapter).CurrentDisplayMode.Format,
                                            .BackBufferFormat, .AutoDepthStencilFormat,
                                            .MultiSample, .MultiSampleQuality, .Windowed) Then _
                    Trace.Assert(False, "Warning: Multi-sampling technique\quality not available.") _
                        : .MultiSampleQuality = 0

                ' Multi-sampling technique not available.
                If Not CheckMultiSampleType(Adapter, _DeviceType,
                                            Manager.Adapters(Adapter).CurrentDisplayMode.Format,
                                            .BackBufferFormat, .AutoDepthStencilFormat,
                                            .MultiSample, .MultiSampleQuality, .Windowed) Then _
                    Trace.Assert(False, "Warning: Multi-sampling technique not available.") _
                        : .MultiSample = MultiSampleType.None

                ' Swap effect not compatible with multi-sampling.
                ' Note that we must check if multi-sampling is still
                ' enabled or not, because we may have disabled it.
                If .MultiSample <> MultiSampleType.None Then _
                    If .SwapEffect <> SwapEffect.Discard Then _
                        Trace.Assert(False, "Warning: Swap effect not compatible with multi-sampling.") _
                            : .SwapEffect = SwapEffect.Discard

            End If ' If .MultiSample <> MultiSampleType.None Then

            ' Are we in full-screen?
            If Not .Windowed Then
                ' Check refresh rate.
                If .FullScreenRefreshRateInHz = 0 Then _
                    Trace.Assert(False, "Warning: Refresh rate was changed.") _
                        : .FullScreenRefreshRateInHz = DispMode.RefreshRate

                ' Presentation inverval not available.
                If Not CheckPresentationInterval(Adapter, _DeviceType, .PresentationInterval) Then _
                    Trace.Assert(False, "Warning: Presentation interval not available.") _
                        : .PresentationInterval = PresentInterval.Default

                ' Refresh rate not available.
                If Not CheckDisplayMode(Adapter, .BackBufferWidth, .BackBufferHeight, , .FullScreenRefreshRateInHz) Then _
                    Trace.Assert(False, "Warning: Refresh rate not available.") _
                        : .FullScreenRefreshRateInHz = DispMode.RefreshRate

            Else ' If Not .Windowed Then
                .FullScreenRefreshRateInHz = 0

            End If ' If Not .Windowed Then
        End With ' With _PresentParameters

        ' Once again, perform validation.
        _PresentParameters = ValidatePresentParameters(_PresentParameters)

        Return _PresentParameters
    End Function

    ''' <summary>
    ''' Validates the presentation parameters.
    ''' </summary>
    ''' <param name="_PresentParameters">
    ''' Input object.
    ''' </param>
    ''' <returns>
    ''' Checked object.
    ''' </returns>
    ''' <remarks>
    ''' This does not modify the existing object.
    ''' </remarks>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>_PresentParameters Is Nothing</c>.
    ''' </exception>
    Public Function ValidatePresentParametersA(ByVal _PresentParameters As PresentParameters) As Boolean
        Dim PP As PresentParameters = CType(_PresentParameters.Clone(), PresentParameters)

        ' Validate the presentation parameters.
        ValidatePresentParameters(PP)

        ' Compare and return value.
        Return PresentParametersEqual(_PresentParameters, PP)
    End Function

    ''' <summary>
    ''' Validates the presentation parameters.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to check.
    ''' </param>
    ''' <param name="_PresentParameters">
    ''' Input object.
    ''' </param>
    ''' <returns>
    ''' Checked object.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>_PresentParameters Is Nothing</c>.
    ''' </exception>
    ''' <remarks>
    ''' This also checks for compatibility on given devices.
    ''' Also, this modifies the existing object.
    ''' </remarks>
    Public Function ValidatePresentParametersA(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                               ByVal _PresentParameters As PresentParameters) As Boolean
        Dim PP As PresentParameters = CType(_PresentParameters.Clone(), PresentParameters)

        ' Validate the presentation parameters.
        ValidatePresentParameters(Adapter, _DeviceType, PP)

        ' Compare and return value.
        Return PresentParametersEqual(_PresentParameters, PP)
    End Function

    ''' <summary>
    ''' Returns whether or not two presentation parameters
    ''' have equal effect while creating\resetting a 
    ''' device\swap chain.
    ''' </summary>
    ''' <param name="PP1">
    ''' Left object.
    ''' </param>
    ''' <param name="PP2">
    ''' Right object.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if equivalent, <c>False</c> otherwise.</returns>
    Private Function PresentParametersEqual(ByVal PP1 As PresentParameters, ByVal PP2 As PresentParameters) As Boolean
        With PP1
            ' Check dimensions and format.
            If (.BackBufferWidth <> PP2.BackBufferWidth) OrElse
               (.BackBufferHeight <> PP2.BackBufferHeight) OrElse
               (.BackBufferFormat <> PP2.BackBufferFormat) Then _
                Return False

            ' Check back-buffer count.
            If (.BackBufferCount <> 0) AndAlso
               (.BackBufferCount <> PP2.BackBufferCount) Then _
                Return False

            ' Check if depth-stencil is enabled.
            If (Not .EnableAutoDepthStencil) AndAlso
               ((PP2.EnableAutoDepthStencil) AndAlso
                (PP2.AutoDepthStencilFormat <> DepthFormat.Unknown)) Then _
                Return False

            ' Check depth-stencil.
            If (.EnableAutoDepthStencil) AndAlso
               (.AutoDepthStencilFormat <> PP2.AutoDepthStencilFormat) Then _
                Return False

            ' Check multi-sampling.
            If (.MultiSample <> PP2.MultiSample) OrElse
               (.MultiSampleQuality <> PP2.MultiSampleQuality) Then _
                Return False

            ' Check swap effect.
            If .SwapEffect <> PP2.SwapEffect Then _
                Return False

            ' Check other flags.
            If (.PresentationInterval <> PP2.PresentationInterval) OrElse
               (.PresentFlag <> PP2.PresentFlag) OrElse
               (.Windowed <> PP2.Windowed) Then _
                Return False

            ' Check full-screen related flags.
            If Not .Windowed Then _
                If .FullScreenRefreshRateInHz <> PP2.FullScreenRefreshRateInHz Then _
                    Return False

        End With ' With _PresentParameters

        Return True
    End Function
End Module
