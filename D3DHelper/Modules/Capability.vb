''' <summary>
''' Module containing functions for D3D capability checking.
''' </summary>
Friend Module Capability
    ''' <summary>
    ''' Checks for availibility of the given mode.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to check.
    ''' </param>
    ''' <param name="Windowed">
    ''' Whether to check for windowed only, or full-screen only, or both.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if mode is supported.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    Public Function CheckDeviceType(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                    Optional ByVal Windowed As TriState = TriState.UseDefault) As Boolean
        Dim DispMode As DisplayMode

        ' Check inputs.
        If ValidateAdapter(Adapter) <> Adapter Then _
            Throw New ArgumentException("Invalid 'Adapter'.") _
                : Exit Function

        ' Check inputs.
        If ValidateDeviceType(_DeviceType) <> _DeviceType Then _
            Throw New ArgumentException("Invalid '_DeviceType'.") _
                : Exit Function

        ' Get current display mode.
        DispMode = Manager.Adapters(Adapter).CurrentDisplayMode

        ' Try the device type.
        Select Case Windowed
            ' WINDOWED ONLY
            Case TriState.True
                If Manager.CheckDeviceType(Adapter, _DeviceType, DispMode.Format, DispMode.Format, True) Then _
                    Return True

                ' FULL-SCREEN ONLY
            Case TriState.False
                If Manager.CheckDeviceType(Adapter, _DeviceType, DispMode.Format, DispMode.Format, False) Then _
                    Return True

                ' WINDOWED OR FULL-SCREEN
            Case Else
                If Manager.CheckDeviceType(Adapter, _DeviceType, DispMode.Format, DispMode.Format, False) OrElse
                   Manager.CheckDeviceType(Adapter, _DeviceType, DispMode.Format, DispMode.Format, True) Then _
                    Return True

        End Select ' Select Case Windowed

        ' Not supported.
        Return False
    End Function

    ''' <summary>
    ''' Checks whether a given display format is available or not.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to check.
    ''' </param>
    ''' <param name="DisplayFormat">
    ''' Format of display (front-buffer).
    ''' </param>
    ''' <param name="BackBufferFormat">
    ''' Format of back-buffer.
    ''' </param>
    ''' <param name="Windowed">
    ''' Whether to check for windowed mode, or full-screen mode.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if mode is supported.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    ''' <remarks>
    ''' When <c>DisplayFormat</c> is <c>Unknown</c> then the current
    ''' display format of the given adapter is used.
    ''' </remarks>
    Public Function CheckDisplayFormat(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                       ByVal DisplayFormat As Format, ByVal BackBufferFormat As Format,
                                       ByVal Windowed As Boolean) As Boolean
        ' Check if device type is supported.
        If Not CheckDeviceType(Adapter, _DeviceType, CType(Windowed, TriState)) Then _
            Throw New ArgumentException("'_DeviceType' not supported.") _
                : Exit Function

        ' Check inputs.
        If DisplayFormat = Format.Unknown Then _
            DisplayFormat = Manager.Adapters(Adapter).CurrentDisplayMode.Format

        ' Now check the format.
        If Manager.CheckDeviceType(Adapter, _DeviceType, DisplayFormat, BackBufferFormat, Windowed) AndAlso
           Manager.CheckDeviceFormat(Adapter, _DeviceType, DisplayFormat, Usage.RenderTarget, ResourceType.Surface,
                                     BackBufferFormat) Then _
            Return True

        ' Not supported.
        Return False
    End Function

    ''' <summary>
    ''' Checks whether a given display depth (stencil) format is available or not.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to check.
    ''' </param>
    ''' <param name="DisplayFormat">
    ''' Format of display (front-buffer).
    ''' </param>
    ''' <param name="BackBufferFormat">
    ''' Format of back-buffer.
    ''' </param>
    ''' <param name="DepthStencilFormat">
    ''' The depth format to check.
    ''' </param>
    ''' <param name="Windowed">
    ''' Whether to check for windowed mode, or full-screen mode.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if mode is supported.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    ''' <remarks>
    ''' When <c>DisplayFormat</c> is <c>Unknown</c> then the current
    ''' display format of the given adapter is used.
    ''' </remarks>
    Public Function CheckDepthStencilFormat(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                            ByVal DisplayFormat As Format, ByVal BackBufferFormat As Format,
                                            ByVal DepthStencilFormat As DepthFormat, ByVal Windowed As Boolean) _
        As Boolean
        ' Check inputs.
        If Not FormatIsDepthStencil(DepthStencilFormat) Then _
            Throw New ArgumentException("Invalid 'DepthStencilFormat'.") _
                : Exit Function

        ' Check if given display formats are feasible.
        If Not CheckDisplayFormat(Adapter, _DeviceType, DisplayFormat, BackBufferFormat, Windowed) Then _
            Throw New Exception("'CheckDisplayFormat' failed for given combination; " &
                                "device creation will fail.") _
                : Exit Function

        ' Check inputs.
        If DisplayFormat = Format.Unknown Then _
            DisplayFormat = Manager.Adapters(Adapter).CurrentDisplayMode.Format

        ' Check if format is avilable.
        If _
            Manager.CheckDeviceFormat(Adapter, _DeviceType, DisplayFormat, Usage.DepthStencil, ResourceType.Surface,
                                      DepthStencilFormat) AndAlso
            Manager.CheckDepthStencilMatch(Adapter, _DeviceType, DisplayFormat, BackBufferFormat, DepthStencilFormat) _
            Then _
            Return True

        ' Not supported.
        Return False
    End Function

    ''' <summary>
    ''' Checks whether a given display depth (stencil) format is available or not.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to check.
    ''' </param>
    ''' <param name="DisplayFormat">
    ''' Format of display (front-buffer).
    ''' </param>
    ''' <param name="BackBufferFormat">
    ''' Format of back-buffer.
    ''' </param>
    ''' <param name="DepthStencilFormat">
    ''' The depth format to check.
    ''' </param>
    ''' <param name="Windowed">
    ''' Whether to check for windowed mode, or full-screen mode.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if mode is supported.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    ''' <remarks>
    ''' When <c>DisplayFormat</c> is <c>Unknown</c> then the current
    ''' display format of the given adapter is used.
    ''' </remarks>
    Public Function CheckDepthStencilFormat(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                            ByVal DisplayFormat As Format, ByVal BackBufferFormat As Format,
                                            ByVal DepthStencilFormat As Format, ByVal Windowed As Boolean) As Boolean
        ' Call the other function.
        Return CheckDepthStencilFormat(Adapter, _DeviceType, DisplayFormat, BackBufferFormat,
                                       CType(DepthStencilFormat, DepthFormat), Windowed)
    End Function

    ''' <summary>
    ''' Checks whether a given multi-sampling technique is available or not.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to check.
    ''' </param>
    ''' <param name="DisplayFormat">
    ''' Format of display (front-buffer).
    ''' </param>
    ''' <param name="BackBufferFormat">
    ''' Format of back-buffer.
    ''' </param>
    ''' <param name="DepthStencilFormat">
    ''' The depth format used.
    ''' </param>
    ''' <param name="_MultiSampleType">
    ''' The type of multi-sampling technique to check.
    ''' </param>
    ''' <param name="Windowed">
    ''' Whether to check for windowed mode, or full-screen mode.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if mode is supported.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    ''' <remarks>
    ''' When <c>DisplayFormat</c> is <c>Unknown</c> then the current
    ''' display format of the given adapter is used.
    ''' </remarks>
    Public Function CheckMultiSampleType(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                         ByVal DisplayFormat As Format, ByVal BackBufferFormat As Format,
                                         ByVal DepthStencilFormat As Format, ByVal _MultiSampleType As MultiSampleType,
                                         ByVal Windowed As Boolean) As Boolean
        ' Check inputs.
        If ValidateMultiSampleType(_MultiSampleType) <> _MultiSampleType Then _
            Throw New ArgumentException("Invalid '_MultiSampleType'.") _
                : Exit Function

        ' Check if given display formats are feasible.
        If Not CheckDisplayFormat(Adapter, _DeviceType, DisplayFormat, BackBufferFormat, Windowed) Then _
            Throw New Exception("'CheckDisplayFormat' failed for given combination; " &
                                "device creation will fail.") _
                : Exit Function


        ' Check if multi-sampling is possible with given technique.
        If Not CheckDepthStencilFormat(Adapter, _DeviceType, DisplayFormat, BackBufferFormat,
                                       DepthStencilFormat, Windowed) Then _
            Throw New ArgumentException("'CheckDepthStencilFormat' failed for given combination; " &
                                        "device creation will fail.") _
                : Exit Function

        ' Check inputs.
        If DisplayFormat = Format.Unknown Then _
            DisplayFormat = Manager.Adapters(Adapter).CurrentDisplayMode.Format

        ' Check if given technique is available or not.
        If _
            Manager.CheckDeviceMultiSampleType(Adapter, _DeviceType, BackBufferFormat, Windowed, _MultiSampleType) AndAlso
            Manager.CheckDeviceMultiSampleType(Adapter, _DeviceType, DepthStencilFormat, Windowed, _MultiSampleType) _
            Then _
            Return True

        ' Not supported.
        Return False
    End Function

    ''' <summary>
    ''' Checks whether a given multi-sampling technique is available or not.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to check.
    ''' </param>
    ''' <param name="DisplayFormat">
    ''' Format of display (front-buffer).
    ''' </param>
    ''' <param name="BackBufferFormat">
    ''' Format of back-buffer.
    ''' </param>
    ''' <param name="DepthStencilFormat">
    ''' The depth format used.
    ''' </param>
    ''' <param name="_MultiSampleType">
    ''' The type of multi-sampling technique to check.
    ''' </param>
    ''' <param name="Windowed">
    ''' Whether to check for windowed mode, or full-screen mode.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if mode is supported.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    ''' <remarks>
    ''' When <c>DisplayFormat</c> is <c>Unknown</c> then the current
    ''' display format of the given adapter is used.
    ''' </remarks>
    Public Function CheckMultiSampleType(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                         ByVal DisplayFormat As Format, ByVal BackBufferFormat As Format,
                                         ByVal DepthStencilFormat As DepthFormat,
                                         ByVal _MultiSampleType As MultiSampleType,
                                         ByVal Windowed As Boolean) As Boolean
        ' Call the other function.
        Return CheckMultiSampleType(Adapter, _DeviceType, DisplayFormat,
                                    BackBufferFormat, CType(DepthStencilFormat, Format),
                                    _MultiSampleType, Windowed)
    End Function

    ''' <summary>
    ''' Checks whether a given multi-sampling technique with
    ''' given quality is available or not.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to check.
    ''' </param>
    ''' <param name="DisplayFormat">
    ''' Format of display (front-buffer).
    ''' </param>
    ''' <param name="BackBufferFormat">
    ''' Format of back-buffer.
    ''' </param>
    ''' <param name="DepthStencilFormat">
    ''' The depth format used.
    ''' </param>
    ''' <param name="_MultiSampleType">
    ''' The type of multi-sampling technique to check.
    ''' </param>
    ''' <param name="MultiSampleQuality">
    ''' The required quality of multi-sampling.
    ''' </param>
    ''' <param name="Windowed">
    ''' Whether to check for windowed mode, or full-screen mode.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if mode is supported.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    ''' <remarks>
    ''' When <c>DisplayFormat</c> is <c>Unknown</c> then the current
    ''' display format of the given adapter is used.
    ''' </remarks>
    Public Function CheckMultiSampleType(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                         ByVal DisplayFormat As Format, ByVal BackBufferFormat As Format,
                                         ByVal DepthStencilFormat As Format, ByVal _MultiSampleType As MultiSampleType,
                                         ByVal MultiSampleQuality As Integer, ByVal Windowed As Boolean) As Boolean
        Dim HResult, QualityLevels As Integer
        Dim Result As Boolean

        ' Check inputs.
        If ValidateMultiSampleQuality(MultiSampleQuality) <> MultiSampleQuality Then _
            Throw New ArgumentException("Invalid 'MultiSampleQuality'.") _
                : Exit Function

        ' Check if quality 0 multi-sampling is available or not.
        If Not CheckMultiSampleType(Adapter, _DeviceType, DisplayFormat, BackBufferFormat,
                                    DepthStencilFormat, _MultiSampleType, Windowed) Then _
            Return False

        ' Check inputs.
        If DisplayFormat = Format.Unknown Then _
            DisplayFormat = Manager.Adapters(Adapter).CurrentDisplayMode.Format

        ' Check if given quality is available or not (for back-buffer).
        Result = Manager.CheckDeviceMultiSampleType(Adapter, _DeviceType, BackBufferFormat,
                                                    Windowed, _MultiSampleType, HResult,
                                                    QualityLevels)

        ' Now check the (failure) conditions (for back-buffer).
        If (Not Result) OrElse (QualityLevels <= MultiSampleQuality) Then _
            Return False

        ' Check if given quality is available or not (for depth-stencil buffer).
        Result = Manager.CheckDeviceMultiSampleType(Adapter, _DeviceType, DepthStencilFormat,
                                                    Windowed, _MultiSampleType, HResult,
                                                    QualityLevels)

        ' Now check the (success) conditions (for depth-stencil buffer).
        If (Result) AndAlso (MultiSampleQuality < QualityLevels) Then _
            Return True

        ' Not supported (because depth-stencil not compatible).
        Return False
    End Function

    ''' <summary>
    ''' Checks whether a given multi-sampling technique with
    ''' given quality is available or not.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to check.
    ''' </param>
    ''' <param name="DisplayFormat">
    ''' Format of display (front-buffer).
    ''' </param>
    ''' <param name="BackBufferFormat">
    ''' Format of back-buffer.
    ''' </param>
    ''' <param name="DepthStencilFormat">
    ''' The depth format used.
    ''' </param>
    ''' <param name="_MultiSampleType">
    ''' The type of multi-sampling technique to check.
    ''' </param>
    ''' <param name="MultiSampleQuality">
    ''' The required quality of multi-sampling.
    ''' </param>
    ''' <param name="Windowed">
    ''' Whether to check for windowed mode, or full-screen mode.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if mode is supported.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    ''' <remarks>
    ''' When <c>DisplayFormat</c> is <c>Unknown</c> then the current
    ''' display format of the given adapter is used.
    ''' </remarks>
    Public Function CheckMultiSampleType(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                         ByVal DisplayFormat As Format, ByVal BackBufferFormat As Format,
                                         ByVal DepthStencilFormat As DepthFormat,
                                         ByVal _MultiSampleType As MultiSampleType,
                                         ByVal MultiSampleQuality As Integer, ByVal Windowed As Boolean) As Boolean
        ' Call the other function.
        Return CheckMultiSampleType(Adapter, _DeviceType, DisplayFormat,
                                    BackBufferFormat, CType(DepthStencilFormat, Format),
                                    _MultiSampleType, MultiSampleQuality, Windowed)
    End Function

    ''' <summary>
    ''' Checks whether a given multi-sampling technique with
    ''' given quality is available or not.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to check.
    ''' </param>
    ''' <param name="_PresentInterval">
    ''' The presentation interval to check.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if mode is supported.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    ''' <remarks>
    ''' Applicable for full-screen only. DO NOT USE FOR
    ''' WINDOWED MODE, may give incorrect results.
    ''' </remarks>
    Public Function CheckPresentationInterval(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                              ByVal _PresentInterval As PresentInterval) As Boolean
        ' Check if device type is supported.
        If Not CheckDeviceType(Adapter, _DeviceType, TriState.True) Then _
            Throw New ArgumentException("'_DeviceType' not supported.") _
                : Exit Function

        ' Check if supported.
        If ((Manager.GetDeviceCaps(Adapter, _DeviceType).PresentationIntervals And _PresentInterval) = _PresentInterval) _
            Then _
            Return True _
            Else _
            Return False
    End Function

    ''' <summary>
    ''' Checks whether HWTL is available or not.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to check.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if supported.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    Public Function CheckHardwareVertexProcessing(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType) As Boolean
        ' Check if device type is supported.
        If Not CheckDeviceType(Adapter, _DeviceType, TriState.True) Then _
            Throw New ArgumentException("'_DeviceType' not supported.") _
                : Exit Function

        ' Check if supported.
        Return Manager.GetDeviceCaps(Adapter, _DeviceType).DeviceCaps.SupportsHardwareTransformAndLight
    End Function

    ''' <summary>
    ''' Checks whether pure device capability is available or not.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to check.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if supported.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    Public Function CheckPureDevice(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType) As Boolean
        ' Check if device type is supported.
        If Not CheckDeviceType(Adapter, _DeviceType, TriState.True) Then _
            Throw New ArgumentException("'_DeviceType' not supported.") _
                : Exit Function

        ' Check if supported.
        Return Manager.GetDeviceCaps(Adapter, _DeviceType).DeviceCaps.SupportsPureDevice
    End Function

    ''' <summary>
    ''' Returns whether the given display mode is supported or not.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DisplayMode">
    ''' The display mode to check.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if supported.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    ''' <remarks>
    ''' To disable refresh-rate checking, set the refresh rate in
    ''' input structure to 0. Note that this does not check if the 
    ''' format is valid for display.
    ''' </remarks>
    Public Function CheckDisplayMode(ByVal Adapter As Integer, ByVal _DisplayMode As DisplayMode) As Boolean
        ' Iterate through the supported display modes to see if the exact mode is supported.
        For Each DispMode As DisplayMode In Manager.Adapters(Adapter).SupportedDisplayModes(_DisplayMode.Format)
            If (DispMode.Width = _DisplayMode.Width) AndAlso
               (DispMode.Height = _DisplayMode.Height) AndAlso
               ((_DisplayMode.RefreshRate = 0) OrElse (DispMode.RefreshRate = _DisplayMode.RefreshRate)) Then _
                Return True

        Next DispMode _
        ' For Each DispMode As DisplayMode In Manager.Adapters(Adapter).SupportedDisplayModes(_DisplayMode.Format)

        ' Not supported.
        Return False
    End Function

    ''' <summary>
    ''' Returns whether the given display mode is supported or not.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="Width">
    ''' Width
    ''' </param>
    ''' <param name="Height">
    ''' Height
    ''' </param>
    ''' <param name="_Format">
    ''' Format
    ''' </param>
    ''' <param name="RefreshRate">
    ''' Refresh Rate. If 0, then this is not compared.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if supported.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    ''' <remarks>
    ''' To disable refresh-rate checking, set the refresh rate in
    ''' input structure to 0. Note that this does not check if the 
    ''' format is valid for display. Also, when <c>DisplayFormat</c>
    ''' is <c>Unknown</c> then the current display format of the
    ''' given adapter is used.
    ''' </remarks>
    Public Function CheckDisplayMode(ByVal Adapter As Integer, ByVal Width As Integer, ByVal Height As Integer,
                                     Optional ByVal _Format As Format = Format.Unknown,
                                     Optional ByVal RefreshRate As Integer = 0) As Boolean
        Dim _DisplayMode As DisplayMode

        ' Check inputs.
        If _Format = Format.Unknown Then _
            _Format = Manager.Adapters(Adapter).CurrentDisplayMode.Format

        ' Build the display mode.
        With _DisplayMode
            .Width = Width
            .Height = Height
            .Format = _Format
            .RefreshRate = RefreshRate
        End With ' With _DisplayMode

        ' Call the other function.
        Return CheckDisplayMode(Adapter, _DisplayMode)
    End Function

    ''' <summary>
    ''' Checks whether the given number of back-buffers are available or not.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to check.
    ''' </param>
    ''' <param name="BackBufferWidth">
    ''' Width of back-buffer.
    ''' </param>
    ''' <param name="BackBufferHeight">
    ''' Height of back-buffer.
    ''' </param>
    ''' <param name="BackBufferFormat">
    ''' Format of back-buffer.
    ''' </param>
    ''' <param name="DepthStencilFormat">
    ''' The depth format used.
    ''' </param>
    ''' <param name="_MultiSampleType">
    ''' The type of multi-sampling technique to check.
    ''' </param>
    ''' <param name="Windowed">
    ''' Whether to check for windowed mode, or full-screen mode.
    ''' </param>
    ''' <param name="CheckCount">
    ''' The count to check.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if supported.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    Public Function CheckBackBufferCount(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                         ByVal BackBufferWidth As Integer, ByVal BackBufferHeight As Integer,
                                         ByVal BackBufferFormat As Format, ByVal DepthStencilFormat As DepthFormat,
                                         ByVal _MultiSampleType As MultiSampleType, ByVal Windowed As Boolean,
                                         ByVal CheckCount As Integer) As Boolean
        Dim _PresentParameters As New PresentParameters

        ' Create a new presentation parameters object and set it's values.
        With _PresentParameters
            .BackBufferWidth = BackBufferWidth
            .BackBufferHeight = BackBufferHeight
            .BackBufferFormat = BackBufferFormat
            .BackBufferCount = CheckCount

            If DepthStencilFormat = DepthFormat.Unknown Then _
                .EnableAutoDepthStencil = False _
                Else _
                .EnableAutoDepthStencil = True

            .AutoDepthStencilFormat = DepthStencilFormat

            .MultiSample = _MultiSampleType
            .SwapEffect = SwapEffect.Discard
            .Windowed = Windowed
        End With ' With _PresentParameters

        ' Now call the other function.
        Return CheckBackBufferCount(Adapter, _DeviceType, _PresentParameters, CheckCount)
    End Function

    ''' <summary>
    ''' Checks whether the given number of back-buffers are available or not.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to check.
    ''' </param>
    ''' <param name="BackBufferWidth">
    ''' Width of back-buffer.
    ''' </param>
    ''' <param name="BackBufferHeight">
    ''' Height of back-buffer.
    ''' </param>
    ''' <param name="BackBufferFormat">
    ''' Format of back-buffer.
    ''' </param>
    ''' <param name="DepthStencilFormat">
    ''' The depth format used.
    ''' </param>
    ''' <param name="_MultiSampleType">
    ''' The type of multi-sampling technique to check.
    ''' </param>
    ''' <param name="Windowed">
    ''' Whether to check for windowed mode, or full-screen mode.
    ''' </param>
    ''' <param name="CheckCount">
    ''' The count to check.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if supported.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    Public Function CheckBackBufferCount(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                         ByVal BackBufferWidth As Integer, ByVal BackBufferHeight As Integer,
                                         ByVal BackBufferFormat As Format, ByVal DepthStencilFormat As Format,
                                         ByVal _MultiSampleType As MultiSampleType, ByVal Windowed As Boolean,
                                         ByVal CheckCount As Integer) As Boolean
        ' Call the other function.
        Return CheckBackBufferCount(Adapter, _DeviceType,
                                    BackBufferWidth, BackBufferHeight, BackBufferFormat,
                                    CType(DepthStencilFormat, DepthFormat),
                                    _MultiSampleType, Windowed, CheckCount)
    End Function

    ''' <summary>
    ''' Checks whether the given number of back-buffers are available or not.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device to check.
    ''' </param>
    ''' <param name="_PresentParameters">
    ''' The <c>PresentParamters</c> class to use.
    ''' </param>
    ''' <param name="CheckCount">
    ''' The count to check.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if supported.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>_PresentParameters Is Nothing</c>.
    ''' </exception>
    Public Function CheckBackBufferCount(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                         ByVal _PresentParameters As PresentParameters, ByVal CheckCount As Integer) _
        As Boolean
        ' Check if device type is supported.
        If Not CheckDeviceType(Adapter, _DeviceType) Then _
            Throw New ArgumentException("'CheckDeviceType' failed with given combination; " &
                                        "device creation will fail.") _
                : Exit Function

        ' Check inputs.
        If _PresentParameters Is Nothing Then _
            Throw New ArgumentNullException("_PresentParameters") _
                : Exit Function

        ' Check if presentation parameters are feasible.
        If Not ValidatePresentParametersA(_PresentParameters) Then _
            Throw New ArgumentException("'ValidatePresentParametersA' failed; " &
                                        "cannot verify back-buffer count.") _
                : Exit Function

        ' Get the max back buffer count and compare with the given count.
        Return (CheckCount <= GetMaxBackBufferCount(Adapter, _DeviceType, _PresentParameters))
    End Function

    ''' <summary>
    ''' Returns true if the device can be created with given parameters.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter to use.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device.
    ''' </param>
    ''' <param name="_CreateFlags">
    ''' Creation flags.
    ''' </param>
    ''' <param name="_PresentParameters">
    ''' Presentation parameters.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if device can be created and there is no
    ''' incompatibility.
    ''' </returns>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>_PresentParameters Is Nothing</c>.
    ''' </exception>
    ''' <remarks>
    ''' Validation is performed; as well as capabilities are checked.
    ''' However, <c>BackBufferCount</c> is not checked. This does not
    ''' modify any of the arguments.
    ''' </remarks>
    Public Function CanCreateDevice(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                    ByVal _CreateFlags As CreateFlags, ByVal _PresentParameters As PresentParameters) _
        As Boolean
        ' Check inputs.
        If _PresentParameters Is Nothing Then _
            Throw New ArgumentNullException("_PresentParameters") _
                : Exit Function

        Try
            ' Check device type.
            If ValidateDeviceType(Adapter, _DeviceType) <> _DeviceType Then _
                Return False

            ' Check creation flags.
            If ValidateCreateFlags(Adapter, _DeviceType, _CreateFlags) <> _CreateFlags Then _
                Return False

            ' Check presentation parameters.
            If Not ValidatePresentParametersA(Adapter, _DeviceType, _PresentParameters) Then _
                Return False

        Catch ex As Exception
            Trace.Assert(False, "Warning: An exception occured: ", ex.Message)
            Return False

        End Try

        ' Probably we can create this device.
        Return True
    End Function

    ''' <summary>
    ''' Returns true if the device can be created with given parameters.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter to use.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device.
    ''' </param>
    ''' <param name="_CreateFlags">
    ''' Creation flags.
    ''' </param>
    ''' <param name="_PresentParameters">
    ''' Presentation parameters.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> if device can be created and there is no
    ''' incompatibility.
    ''' </returns>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>_PresentParameters Is Nothing</c>.
    ''' </exception>
    ''' <remarks>
    ''' Validation is performed; as well as capabilities are checked.
    ''' However, <c>BackBufferCount</c> is not checked. This modifies
    ''' the arguments to possibly enable creation, if it is not possible.
    ''' </remarks>
    Public Function CanCreateDeviceEx(ByRef Adapter As Integer, ByRef _DeviceType As DeviceType,
                                      ByRef _CreateFlags As CreateFlags, ByVal _PresentParameters As PresentParameters) _
        As Boolean
        ' Check inputs.
        If _PresentParameters Is Nothing Then _
            Throw New ArgumentNullException("_PresentParameters") _
                : Exit Function

        ' Check inputs.
        Trace.Assert(Adapter = ValidateAdapter(Adapter), "Warning: Invalid 'Adapter'")
        Trace.Assert(_DeviceType = ValidateDeviceType(_DeviceType), "Warning: Invalid '_DeviceType'")
        Trace.Assert(_CreateFlags = ValidateCreateFlags(_CreateFlags), "Warning: Invalid '_CreateFlags'")
        Trace.Assert(ValidatePresentParametersA(_PresentParameters), "Warning: Invalid '_PresentParameters'")

        ' Validate inputs.
        Adapter = ValidateAdapter(Adapter)
        _DeviceType = ValidateDeviceType(_DeviceType)
        _CreateFlags = ValidateCreateFlags(_CreateFlags)
        _PresentParameters = ValidatePresentParameters(_PresentParameters)

        Try
            ' Validate device type.
            _DeviceType = ValidateDeviceType(Adapter, _DeviceType)

            ' Validate creation flags.
            _CreateFlags = ValidateCreateFlags(Adapter, _DeviceType, _CreateFlags)

            ' Check presentation parameters.
            _PresentParameters = ValidatePresentParameters(Adapter, _DeviceType, _PresentParameters)

        Catch ex As Exception
            ' Some un-recoverable error.
            Trace.Assert(False, "Warning: An exception occured: ", ex.Message)
            Return False

        End Try

        ' Validate once again.
        Adapter = ValidateAdapter(Adapter)
        _DeviceType = ValidateDeviceType(_DeviceType)
        _CreateFlags = ValidateCreateFlags(_CreateFlags)
        _PresentParameters = ValidatePresentParameters(_PresentParameters)

        ' Probably we can create this device.
        Return True
    End Function
End Module
