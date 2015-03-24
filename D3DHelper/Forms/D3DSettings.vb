''' <summary>
''' Form containing code for setting some of the Direct3D settings by end-user.
''' </summary>
Friend NotInheritable Class D3DSettings
    ''' <summary>
    ''' Locks\Unlocks the 'Windowed' checkbox to either checked, or unchecked.
    ''' </summary>
    ''' <param name="Lock">
    ''' <c>Tristate.UseDefault</c> to enable the checkbox.
    ''' <c>Tristate.True</c> to disable the checkbox and check it.
    ''' <c>Tristate.False</c> to disable the checkbox and uncheck it.
    ''' </param>
    Public Sub LockWindowedCheckbox(Optional ByVal Lock As Microsoft.VisualBasic.TriState = TriState.UseDefault)
        Select Case Lock
            Case TriState.True
                chkWindowed.Enabled = False
                chkWindowed.Checked = True

            Case TriState.False
                chkWindowed.Enabled = False
                chkWindowed.Checked = False

            Case TriState.UseDefault
                chkWindowed.Enabled = True
                chkWindowed.Checked = D3DConfigurer.FullScreen

        End Select ' Select Case Lock
    End Sub

    ''' <summary>
    ''' Builds a string using the back-buffer attributes.
    ''' </summary>
    ''' <param name="Width">
    ''' Width of the back-buffer.
    ''' </param>
    ''' <param name="Height">
    ''' Height of the back-buffer.
    ''' </param>
    ''' <param name="Format">
    ''' Format of the back-buffer.
    ''' </param>
    ''' <returns>
    ''' String representing the description.
    ''' </returns>
    Private Function MakeDisplayModeListItem(ByVal Width As Integer, ByVal Height As Integer, ByVal Format As Format) _
        As String
        Return CStr(Width) &
               "x" &
               CStr(Height) &
               "x" &
               CStr(FormatBitCount(Format)) &
               " (" &
               Format.ToString() &
               ")"
    End Function

    ''' <summary>
    ''' Takes a string representation of the back-buffer
    ''' attributes and returns the single attributes.
    ''' </summary>
    ''' <param name="DispMode">
    ''' Input string.
    ''' </param>
    ''' <param name="Width">
    ''' Width of the back-buffer.
    ''' </param>
    ''' <param name="Height">
    ''' Height of the back-buffer.
    ''' </param>
    ''' <param name="Format">
    ''' Format of the back-buffer.
    ''' </param>
    Private Shared Sub SplitDisplayModeListItem(ByVal DispMode As String, ByRef Width As Integer,
                                                ByRef Height As Integer, ByRef Format As Format)
        Dim s() As String = Split(DispMode, "x")

        If UBound(s) <> 2 Then _
            Trace.Assert(False, "Warning: Invalid 'DispMode'.") _
                : Exit Sub

        ' Trim the strings.
        s(0) = Trim(s(0))
        s(1) = Trim(s(1))
        s(2) = Trim(s(2))

        ' Set the width.
        If IsNumeric(s(0)) Then _
            Width = CInt(s(0)) _
            Else _
            Width = CInt(Val(s(0)))

        ' Set the height.
        If IsNumeric(s(1)) Then _
            Height = CInt(s(1)) _
            Else _
            Height = CInt(Val(s(1)))

        ' Set the format.
        s(2) = Strings.Left(s(2), Len(s(2)) - 1)
        s(2) = Strings.Right(s(2), Len(s(2)) - InStr(s(2), "("))
        Format = FormatFromString(s(2))

        ' Erase temporary array.
        s = Nothing
    End Sub

    ''' <summary>
    ''' Populates the display modes list.
    ''' </summary>
    Private Function PopulateDisplayModes() As Boolean
        Dim Adapter As Integer, _DeviceType As DeviceType
        Dim Windowed As Boolean, DefaultMode As String, OldMode As Object
        Dim DisplayModes(), CurrDispMode As DisplayMode
        Dim DisplayFormats() As Format

        ' If not selected then exit.
        If cboAdapter.SelectedIndex = - 1 Then _
            Return False

        ' If not selected then exit.
        If cboDeviceType.SelectedIndex = - 1 Then _
            Return False

        ' Retrieve some variables...
        Adapter = cboAdapter.SelectedIndex
        _DeviceType = CType(cboDeviceType.Tag, DeviceType)
        CurrDispMode = Manager.Adapters(Adapter).CurrentDisplayMode
        DisplayModes = D3DConfigurer.GetDisplayModes()
        DisplayFormats = D3DConfigurer.GetBackBufferFormats()
        Windowed = chkWindowed.Checked
        OldMode = lstDisplayModes.SelectedItem

        ' Remove all items.
        lstDisplayModes.Items.Clear()

        ' Check each combination of display mode and format.
        For I As Integer = 0 To UBound(DisplayFormats)
            For J As Integer = 0 To UBound(DisplayModes)
                ' Check display mode extents. Should be smaller than screen size if windowed.
                If ((DisplayModes(J).Width <= Screen.PrimaryScreen.Bounds.Width) AndAlso
                    (DisplayModes(J).Height <= Screen.PrimaryScreen.Bounds.Height)) OrElse
                   (Not Windowed) Then
                    ' If both display mode and format are available, add it to list.
                    If _
                        CheckDisplayFormat(Adapter, _DeviceType, CurrDispMode.Format, DisplayFormats(I), Windowed) AndAlso
                        (Windowed OrElse CheckDisplayMode(Adapter, DisplayModes(J).Width, DisplayModes(J).Height)) Then _
                        lstDisplayModes.Items.Add(
                            MakeDisplayModeListItem(
                                DisplayModes(J).Width,
                                DisplayModes(J).Height,
                                DisplayFormats(I))
                            )

                End If _
                ' If (DisplayModes(J).Width <= Screen.PrimaryScreen.Bounds.Width) AndAlso (DisplayModes(J).Height <= Screen.PrimaryScreen.Bounds.Height) Then
            Next J ' For J As Integer = 0 To UBound(DisplayModes)

            ' Also check for desktop resolution.
            If CheckDisplayFormat(Adapter, _DeviceType, CurrDispMode.Format, DisplayFormats(I), Windowed) Then _
                lstDisplayModes.Items.Add(
                    MakeDisplayModeListItem(
                        CurrDispMode.Width,
                        CurrDispMode.Height,
                        DisplayFormats(I))
                    )

        Next I ' For I As Integer = 0 To UBound(DisplayFormats)

        ' Add the default mode if needed. In this case there is no need to check because it will always be
        ' available, since it's the mode we're in now.
        DefaultMode = MakeDisplayModeListItem(CurrDispMode.Width,
                                              CurrDispMode.Height,
                                              CurrDispMode.Format)

        ' Add it, if not there.
        If Not lstDisplayModes.Items.Contains(DefaultMode) Then _
            lstDisplayModes.Items.Add(DefaultMode)

        ' Select item that was selected last time, if possible,
        ' otherwise select first item.
        If (OldMode IsNot Nothing) AndAlso (lstDisplayModes.Items.Contains(OldMode)) Then _
            lstDisplayModes.SelectedItem = OldMode _
            Else _
            lstDisplayModes.SelectedIndex = 0

        ' Remove temporary arrays.
        DisplayModes = Nothing
        DisplayFormats = Nothing
    End Function

    ''' <summary>
    ''' Saves all settings to registry.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Name of application (for registry settings).
    ''' </param>
    Private Function SaveAllSettings(ByVal ApplicationName As String) As Boolean
        Dim Width, Height As Integer, _Format As Format
        Dim Adapter As Integer, _DeviceType As DeviceType
        Dim _CreateFlags As CreateFlags, _PresentParameters As PresentParameters

        ' Check if all required elements are selected.
        If cboAdapter.SelectedIndex = - 1 Then _
            Return False

        ' Check if all required elements are selected.
        If cboDeviceType.SelectedIndex = - 1 Then _
            Return False

        ' Check if all required elements are selected.
        If lstDisplayModes.SelectedIndex = - 1 Then _
            Return False

        ' Get the device creation parameters.
        Adapter = cboAdapter.SelectedIndex
        _DeviceType = CType(cboDeviceType.Tag, DeviceType)
        _CreateFlags = D3DConfigurer.BuildCreationFlags(Adapter, _DeviceType)

        ' Convert display mode string to format.
        SplitDisplayModeListItem(CStr(lstDisplayModes.SelectedItem),
                                 Width,
                                 Height,
                                 _Format)

        ' Build the presentation parameters.
        _PresentParameters = D3DConfigurer.BuildPresentParameters(cboAdapter.SelectedIndex,
                                                                  CType(cboDeviceType.Tag, DeviceType),
                                                                  Nothing,
                                                                  Width,
                                                                  Height,
                                                                  _Format,
                                                                  CType(
                                                                      IIf(
                                                                          chkWindowed.Checked,
                                                                          TriState.True,
                                                                          TriState.False
                                                                          ),
                                                                      TriState)
                                                                  )

        ' OK, save settings.
        SaveAdapter(ApplicationName, Adapter)
        SaveDeviceType(ApplicationName, _DeviceType)
        SaveCreateFlags(ApplicationName, _CreateFlags)
        SavePresentationParameters(ApplicationName, _PresentParameters)

        ' Remove temporary objects.
        _PresentParameters = Nothing
    End Function

    ''' <summary>
    ''' Loads all settings from registry.
    ''' </summary>
    ''' <param name="ApplicationName">
    ''' Name of application (for registry settings).
    ''' </param>
    Private Sub LoadAllSettings(ByVal ApplicationName As String)
        Dim Adapter As Integer
        Dim _DeviceType As DeviceType
        Dim _PresentParameters As PresentParameters
        Dim DispMode As String

        ' Load settings.
        Adapter = LoadAdapter(ApplicationName)
        _DeviceType = LoadDeviceType(ApplicationName)
        _PresentParameters = LoadPresentParameters(ApplicationName)
        DispMode = MakeDisplayModeListItem(_PresentParameters.BackBufferWidth,
                                           _PresentParameters.BackBufferHeight,
                                           _PresentParameters.BackBufferFormat)

        ' Set the adapter.
        If Adapter < cboAdapter.Items.Count Then _
            cboAdapter.SelectedIndex = Adapter _
            Else _
            cboAdapter.SelectedIndex = 0

        ' Set the device type.
        Select Case _DeviceType
            ' Harware abstraction layer (HAL)
            Case DeviceType.Hardware
                cboDeviceType.SelectedIndex = 0

                ' Reference rasterizer (REF)
            Case DeviceType.Reference
                cboDeviceType.SelectedIndex = 1

                ' Unknown (ERROR)
            Case Else
                cboDeviceType.SelectedIndex = 0

        End Select ' Select Case _DeviceType

        ' Set the windowed flag if enabled.
        If chkWindowed.Enabled Then _
            chkWindowed.Checked = _PresentParameters.Windowed

        ' Try to set the resolution and display mode.
        If lstDisplayModes.Items.Contains(DispMode) Then _
            lstDisplayModes.SelectedItem = DispMode
    End Sub

    Private Sub D3DSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Add available adapters to the list.
        For I As Integer = 0 To Manager.Adapters.Count - 1
            cboAdapter.Items.Add(Manager.Adapters(I).Information.Description)

        Next I ' For I As Integer = 0 To Manager.Adapters.Count - 1

        ' Select an adapter and a device type.
        If SettingsExists(D3DConfigurer.AppNameFull) Then
            LoadAllSettings(D3DConfigurer.AppNameFull)

        Else ' If SettingsExists(AppNameFull) Then
            cboAdapter.SelectedIndex = 0
            cboDeviceType.SelectedIndex = 0

        End If ' If SettingsExists(AppNameFull) Then

        cmdApply.Enabled = False
    End Sub

    Private Sub cboAdapter_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles cboAdapter.SelectedIndexChanged
        PopulateDisplayModes()

        cmdApply.Enabled = False
    End Sub

    Private Sub cboDeviceType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles cboDeviceType.SelectedIndexChanged
        Select Case cboDeviceType.SelectedIndex
            ' Hardware abstraction layer (HAL)
            Case 0
                cboDeviceType.Tag = DeviceType.Hardware

                ' Reference rasterizer (REF)
            Case 1
                cboDeviceType.Tag = DeviceType.Reference

                ' Unknown (ERROR)
            Case Else
                cboDeviceType.Tag = DeviceType.Hardware

        End Select ' Select Case cboDeviceType.SelectedIndex

        ' Refresh the list items.
        PopulateDisplayModes()

        cmdApply.Enabled = True
    End Sub

    Private Sub chkWindowed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles chkWindowed.CheckedChanged
        PopulateDisplayModes()

        cmdApply.Enabled = True
    End Sub

    Private Sub cmdAdapterInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles cmdAdapterInfo.Click
        Dim f As D3DAdapterInfo

        If cboAdapter.SelectedIndex = - 1 Then _
            MsgBox("Please select an adapter first!",
                   MsgBoxStyle.Exclamation Or MsgBoxStyle.DefaultButton1 Or MsgBoxStyle.OkOnly,
                   "Adapter not selected.") _
                : Exit Sub

        f = New D3DAdapterInfo

        f.Adapter = cboAdapter.SelectedIndex

        f.ShowDialog()

        f.Dispose()

        f = Nothing
    End Sub

    Private Sub cmdApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApply.Click
        ' Make sure all settings are selected.
        If cboAdapter.SelectedIndex = - 1 Then _
            MsgBox("Please select an adapter first!",
                   MsgBoxStyle.Exclamation Or MsgBoxStyle.DefaultButton1 Or MsgBoxStyle.OkOnly,
                   "Adapter not selected.") _
                : Exit Sub

        ' Make sure all settings are selected.
        If cboDeviceType.SelectedIndex = - 1 Then _
            MsgBox("Please select a device type first!",
                   MsgBoxStyle.Exclamation Or MsgBoxStyle.DefaultButton1 Or MsgBoxStyle.OkOnly,
                   "Device type not selected.") _
                : Exit Sub

        ' Make sure all settings are selected.
        If lstDisplayModes.SelectedIndex = - 1 Then _
            MsgBox("Please select a display mode first!",
                   MsgBoxStyle.Exclamation Or MsgBoxStyle.DefaultButton1 Or MsgBoxStyle.OkOnly,
                   "Display mode not selected.") _
                : Exit Sub

        ' Save the settings.
        SaveAllSettings(D3DConfigurer.AppNameFull)

        ' Disable this command box.
        cmdApply.Enabled = False
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        ' Make sure all settings are selected.
        If cboAdapter.SelectedIndex = - 1 Then _
            MsgBox("Please select an adapter first!",
                   MsgBoxStyle.Exclamation Or MsgBoxStyle.DefaultButton1 Or MsgBoxStyle.OkOnly,
                   "Adapter not selected.") _
                : Exit Sub

        ' Make sure all settings are selected.
        If cboDeviceType.SelectedIndex = - 1 Then _
            MsgBox("Please select a device type first!",
                   MsgBoxStyle.Exclamation Or MsgBoxStyle.DefaultButton1 Or MsgBoxStyle.OkOnly,
                   "Device type not selected.") _
                : Exit Sub

        ' Make sure all settings are selected.
        If lstDisplayModes.SelectedIndex = - 1 Then _
            MsgBox("Please select a display mode first!",
                   MsgBoxStyle.Exclamation Or MsgBoxStyle.DefaultButton1 Or MsgBoxStyle.OkOnly,
                   "Display mode not selected.") _
                : Exit Sub

        ' Now press apply.
        cmdApply_Click(sender, e)

        ' Finally OK.
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cmdPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPreview.Click
        Dim f As New DisplayTest

        If SettingsExists(f.GetType.ToString()) Then _
            DeleteSettings(f.GetType.ToString())

        SaveAllSettings(f.GetType.ToString())

        f.ShowDialog()

        DeleteSettings(f.GetType.ToString())

        f.Dispose()

        f = Nothing
    End Sub

    Private Sub lstDisplayModes_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles lstDisplayModes.SelectedValueChanged
        cmdApply.Enabled = True
    End Sub
End Class