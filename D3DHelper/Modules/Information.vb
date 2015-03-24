''' <summary>
''' Module containing code for D3D Information checking
''' functions.
''' </summary>
Friend Module Information
    ''' <summary>
    ''' Returns true if the device can be created with given parameters.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter to use.
    ''' </param>
    ''' <param name="_DeviceType">
    ''' Type of device.
    ''' </param>
    ''' <param name="_PresentParameters">
    ''' Presentation parameters.
    ''' </param>
    ''' <returns>
    ''' <c>Nothing</c> if device creation causes no errors. Otherwise,
    ''' returns the exception.
    ''' </returns>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>_PresentParameters Is Nothing</c>.
    ''' </exception>
    ''' <remarks>
    ''' No validation is performed. This may modify the presentation paramters.
    ''' </remarks>
    Public Function TryCreateDevice(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                    ByVal _PresentParameters As PresentParameters) As Exception
        Dim D As Device = Nothing,
            f As DeviceTest = Nothing,
            ex As Exception = Nothing

        ' Check inputs.
        If _PresentParameters Is Nothing Then _
            Throw New ArgumentNullException("_PresentParameters") _
                : Exit Function

        ' Load the form.
        f = New DeviceTest

        ' Try to create the device.
        Try
            D = New Device(Adapter, _DeviceType, f, CreateFlags.SoftwareVertexProcessing, _PresentParameters)

        Catch _ex As Exception
            ' Creation failed -- return the exception.
            ex = _ex

        End Try

        ' Dispose the objects.
        If D IsNot Nothing Then _
            D.Dispose()

        ' Close the form.
        f.Close()
        f.Dispose()

        ' Release the objects.
        D = Nothing
        f = Nothing

        ' Report creation result.
        Return ex
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
    ''' <c>True</c> if device creation causes no errors.
    ''' </returns>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>_PresentParameters Is Nothing</c>.
    ''' </exception>
    ''' <remarks>
    ''' No validation is performed. This may modify the presentation paramters.
    ''' </remarks>
    Public Function TryCreateDeviceA(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                     ByVal _CreateFlags As CreateFlags, ByVal _PresentParameters As PresentParameters) _
        As Boolean
        ' Call the other function.
        If TryCreateDevice(Adapter, _DeviceType, _PresentParameters) Is Nothing Then _
            Return True _
            Else _
            Return False
    End Function

    ''' <summary>
    ''' Returns the maximum possible refresh rate for a given display mode.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter on which to check.
    ''' </param>
    ''' <param name="Width">
    ''' Width of display mode.
    ''' </param>
    ''' <param name="Height">
    ''' Height of display mode.
    ''' </param>
    ''' <param name="_Format">
    ''' Format used for the display mode.
    ''' </param>
    ''' <returns>
    ''' Maximum refresh rate supported.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation for any argument fails, or display mode
    ''' is not supported.
    ''' </exception>
    Public Function GetMaxRefreshRate(ByVal Adapter As Integer, ByVal Width As Integer, ByVal Height As Integer,
                                      ByVal _Format As Format) As Integer
        Dim _DisplayMode As DisplayMode
        Dim MaxRefreshRate As Integer

        ' Check inputs.
        If ValidateAdapter(Adapter) <> Adapter Then _
            Throw New ArgumentException("Invalid 'Adapter'.") _
                : Exit Function

        ' Check inputs.
        If (Width < 0) OrElse (Height < 0) Then _
            Throw New ArgumentException("'(Width < 0) Or (Height < 0)'") _
                : Exit Function

        ' Check inputs.
        If Not FormatIsRGB(_Format) Then _
            Throw New ArgumentException("Invalid '_Format'.") _
                : Exit Function

        ' This is for full-screen only.
        If Not CheckDisplayMode(Adapter, Width, Height, _Format) Then _
            Throw New ArgumentException("Invalid display mode.") _
                : Exit Function

        ' Iterate through all display modes to get the refresh rate.
        For Each _DisplayMode In Manager.Adapters(Adapter).SupportedDisplayModes(_Format)
            ' Check display mode width and height. If same, then check refresh rate.
            If (_DisplayMode.Width = Width) AndAlso (_DisplayMode.Height = Height) Then _
                MaxRefreshRate = Math.Max(MaxRefreshRate, _DisplayMode.RefreshRate)

        Next _DisplayMode ' For Each _DisplayMode In Manager.Adapters(Adapter).SupportedDisplayModes(_Format)

        Return MaxRefreshRate
    End Function

    ''' <summary>
    ''' Returns the maximum number of available back-buffers using the 
    ''' given settings.
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
    ''' <returns>
    ''' The maximum number of back-buffers available.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    ''' <remarks>
    ''' This actually creates a device, hence, avoid calling
    ''' to frequently.
    ''' </remarks>
    Public Function GetMaxBackBufferCount(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                          ByVal BackBufferWidth As Integer, ByVal BackBufferHeight As Integer,
                                          ByVal BackBufferFormat As Format, ByVal DepthStencilFormat As DepthFormat,
                                          ByVal _MultiSampleType As MultiSampleType, ByVal Windowed As Boolean) _
        As Integer
        Dim _PresentParameters As New PresentParameters

        ' Create a new presentation parameters object and set it's values.
        With _PresentParameters
            .BackBufferWidth = BackBufferWidth
            .BackBufferHeight = BackBufferHeight
            .BackBufferFormat = BackBufferFormat
            .BackBufferCount = Present.BackBuffersMax

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
        Return GetMaxBackBufferCount(Adapter, _DeviceType, _PresentParameters)
    End Function

    ''' <summary>
    ''' Returns the maximum number of available back-buffers using the 
    ''' given settings.
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
    ''' <returns>
    ''' The maximum number of back-buffers available.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    ''' <remarks>
    ''' This actually creates a device, hence, avoid calling
    ''' to frequently.
    ''' </remarks>
    Public Function GetMaxBackBufferCount(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                          ByVal BackBufferWidth As Integer, ByVal BackBufferHeight As Integer,
                                          ByVal BackBufferFormat As Format, ByVal DepthStencilFormat As Format,
                                          ByVal _MultiSampleType As MultiSampleType, ByVal Windowed As Boolean) _
        As Integer
        ' Call the other function.
        Return GetMaxBackBufferCount(Adapter, _DeviceType,
                                     BackBufferWidth, BackBufferHeight, BackBufferFormat,
                                     CType(DepthStencilFormat, DepthFormat),
                                     _MultiSampleType, Windowed)
    End Function

    ''' <summary>
    ''' Returns the maximum number of available back-buffers using the 
    ''' given settings.
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
    ''' <returns>
    ''' The maximum number of back-buffers available.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' Thrown when validation fails for any parameter.
    ''' </exception>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>_PresentParameters Is Nothing</c>.
    ''' </exception>
    ''' <remarks>
    ''' This actually creates a device, hence, avoid calling
    ''' to frequently. Also, this may modify the presentation
    ''' parameters.
    ''' </remarks>
    Public Function GetMaxBackBufferCount(ByVal Adapter As Integer, ByVal _DeviceType As DeviceType,
                                          ByVal _PresentParameters As PresentParameters) As Integer
        Dim ex As Exception
        Dim PP As PresentParameters

        ' Check inputs.
        If _PresentParameters Is Nothing Then _
            Throw New ArgumentNullException("_PresentParameters") _
                : Exit Function

        ' Create a clone of the presentation parameters.
        PP = CType(_PresentParameters.Clone(), PresentParameters)

        ' A few modifications in the present parameters.
        With PP
            ' Since there is no exact information on number of back buffers,
            ' try a SwapEffect.Discard; since it's going to work on all
            ' possible back-buffer counts. Also, set the maximum back-buffer count.
            .SwapEffect = SwapEffect.Discard

            ' Set the buffer count to one plus the maximum, this is because
            ' it will be decreased by 1 in the loop.
            .BackBufferCount = Present.BackBuffersMax
        End With ' With PP

        For I As Integer = Present.BackBuffersMax To 1 Step - 1
            ' See if we can create a device.
            If Not CanCreateDevice(Adapter, _DeviceType, CreateFlags.SoftwareVertexProcessing, PP) Then _
                Throw New ArgumentException("Cannot create device with given parameters.") _
                    : Exit Function

            ' Set the count.
            PP.BackBufferCount = I

            ' Try to create the device.
            ex = TryCreateDevice(Adapter, _DeviceType, PP)

            ' Check for exceptions.
            If ex Is Nothing Then _
                Return I

            ' Check for out of memory exceptions. 
            If (TypeOf ex Is OutOfMemoryException) OrElse
               (TypeOf ex Is OutOfVideoMemoryException) Then

                ' Try to reduce the back-buffers, if possible. If we cannot further
                ' reduce back-buffers, it means there isn't enough memory (video or
                ' system, depending upon create flags) and settings must be toned down.
                If I = 1 Then _
                    Return - 1 _
                    Else _
                    Continue For

            End If ' If (TypeOf ex Is OutOfMemoryException) OrElse (TypeOf ex Is OutOfVideoMemoryException) Then

            ' Check for other exceptions. Since this is an unexpected exception, 
            ' we can't deal with it. Throw it.
            Throw ex

        Next I ' For I As Integer = Present.BackBuffersMax To 1 Step -1

        ' Return the count, if successful.
        Return PP.BackBufferCount
    End Function

    ''' <summary>
    ''' Compares two display modes.
    ''' </summary>
    ''' <param name="DispMode1">
    ''' Left argument.
    ''' </param>
    ''' <param name="DispMode2">
    ''' Right argument.
    ''' </param>
    ''' <returns>
    ''' -1 if <c>DispMode1</c> &lt; <c>DispMode2</c>;
    ''' 0 if <c>DispMode1</c> = <c>DispMode2</c>;
    ''' 1 if <c>DispMode1</c> &gt; <c>DispMode2</c>.
    ''' </returns>
    ''' <remarks>
    ''' Sorts according to following order:
    ''' Width; Height; Format (bit count); Refresh Rate.
    ''' </remarks>
    Private Function DisplayModeComparer(ByVal DispMode1 As DisplayMode, ByVal DispMode2 As DisplayMode) As Integer
        ' Width
        If DispMode1.Width < DispMode2.Width Then _
            Return - 1

        If DispMode1.Width > DispMode2.Width Then _
            Return 1

        ' Height
        If DispMode1.Height < DispMode2.Height Then _
            Return - 1

        If DispMode1.Height > DispMode2.Height Then _
            Return 1

        ' Format (bit count)
        If FormatBitCount(DispMode1.Format) < FormatBitCount(DispMode2.Format) Then _
            Return - 1

        If FormatBitCount(DispMode1.Format) > FormatBitCount(DispMode2.Format) Then _
            Return 1

        ' Refresh rate.
        If DispMode1.RefreshRate < DispMode2.RefreshRate Then _
            Return - 1

        If DispMode1.RefreshRate > DispMode2.RefreshRate Then _
            Return 1

        ' Format ordering.
        If DispMode1.Format < DispMode2.Format Then _
            Return - 1

        If DispMode1.Format > DispMode2.Format Then _
            Return 1

        ' Probably equal.
        Return 0
    End Function

    ''' <summary>
    ''' Returns the sorted display mode collection.
    ''' </summary>
    ''' <param name="Adapter">
    ''' Adapter to use.
    ''' </param>
    ''' <param name="f">
    ''' Formats to use.
    ''' </param>
    ''' <returns>
    ''' The sorted display modes.
    ''' </returns>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>f Is Nothing</c>.
    ''' </exception>
    ''' <exception cref="ArgumentException">
    ''' Thrown when input array has invalid rank, or when 
    ''' validation for any parameter fails.
    ''' </exception>
    Public Function GetDisplayModes(ByVal Adapter As Integer, ByVal f() As Format) As ICollection(Of DisplayMode)
        Dim DispModes As DisplayModeCollection
        Dim List As List(Of DisplayMode)

        ' Check inputs.
        If f Is Nothing Then _
            Throw New ArgumentNullException("f") _
                : Exit Function

        ' Check rank.
        If f.Rank <> 1 Then _
            Throw New ArgumentException("'f.Rank <> 1'") _
                : Exit Function

        ' Check inputs.
        If ValidateAdapter(Adapter) <> Adapter Then _
            Throw New ArgumentException("Invalid 'Adapter'.") _
                : Exit Function

        ' Check inputs.
        For I As Integer = 0 To UBound(f)
            If Not FormatIsRGB(f(I)) Then _
                Throw New ArgumentException("Invalid 'f(" & CStr(I) & ")'.") _
                    : Exit Function

        Next I ' For I As Integer = 0 To UBound(f)

        ' Initialize the list.
        List = New List(Of DisplayMode)

        ' Process all formats in list.
        For I As Integer = 0 To UBound(f)
            ' Get the available display modes.
            DispModes = Manager.Adapters(Adapter).SupportedDisplayModes(f(I))

            ' Reset the iterator.
            DispModes.Reset()

            ' Iterate through all modes and add the display mode.
            Do While DispModes.MoveNext()
                List.Add(CType(DispModes.Current, DisplayMode))
            Loop ' Do While DispModes.MoveNext()

        Next I ' For I As Integer = 0 to UBound(f)

        ' Sort the list.
        If List.Count <> 0 Then _
            List.Sort(AddressOf DisplayModeComparer)

        ' Return the list.
        Return List
    End Function
End Module
