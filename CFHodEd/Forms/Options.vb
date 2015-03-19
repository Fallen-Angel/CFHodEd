Imports Microsoft.DirectX
Imports Homeworld2.HOD

''' <summary>
''' Form to allow user to change CFHE settings.
''' </summary>
Friend NotInheritable Class Options
 ''' <summary>HOD.</summary>
 Private m_HOD As HOD

 ''' <summary>Stripe colour bitmap.</summary>
 Private m_TeamColourBitmap As Drawing.Bitmap

 ''' <summary>Team colour bitmap.</summary>
 Private m_StripeColourBitmap As Drawing.Bitmap

 ''' <summary>
 ''' Class contructor.
 ''' </summary>
 Public Sub New(ByVal HOD As HOD)
  ' This call is required by the Windows Form Designer.
  InitializeComponent()

  ' Set HOD.
  m_HOD = HOD

 End Sub

 ''' <summary>
 ''' Fills an image with the specified colour.
 ''' </summary>
 Private Shared Sub FillImage(ByVal image As Image, ByVal r As Integer, ByVal g As Integer, ByVal b As Integer)
  Dim graphics As Graphics = graphics.FromImage(image)
  graphics.Clear(Color.FromArgb(r, g, b))
  graphics.Dispose()

 End Sub

 Private Sub Options_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
  ' Remove reference to image.
  pbxTeamColour.Image = Nothing
  pbxStripeColour.Image = Nothing

  ' Dispose the bitmaps.
  m_TeamColourBitmap.Dispose()
  m_StripeColourBitmap.Dispose()

 End Sub

 Private Sub Options_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
  ' Create new bitmas.
  m_TeamColourBitmap = New Drawing.Bitmap(pbxTeamColour.Width, pbxTeamColour.Height, Imaging.PixelFormat.Format24bppRgb)
  m_StripeColourBitmap = New Drawing.Bitmap(pbxStripeColour.Width, pbxStripeColour.Height, Imaging.PixelFormat.Format24bppRgb)

  ' Assign bitmaps.
  pbxTeamColour.Image = m_TeamColourBitmap
  pbxStripeColour.Image = m_StripeColourBitmap

  ' Assign to the appropriate sliders.
  sldTeamRed.Value = Color.FromArgb(My.Settings.HOD_TeamColour).R
  sldTeamGreen.Value = Color.FromArgb(My.Settings.HOD_TeamColour).G
  sldTeamBlue.Value = Color.FromArgb(My.Settings.HOD_TeamColour).B
  sldStripeRed.Value = Color.FromArgb(My.Settings.HOD_StripeColour).R
  sldStripeGreen.Value = Color.FromArgb(My.Settings.HOD_StripeColour).G
  sldStripeBlue.Value = Color.FromArgb(My.Settings.HOD_StripeColour).B
  sldTeamStripe_Scroll(Nothing, EventArgs.Empty)

  ' Load the badge texture path.
  txtBadgeTexture.Text = My.Settings.HOD_BadgeTexture

  ' Load miscellaneous settings.
  txtUserName.Text = My.Settings.HOD_Owner
  chkProcessBGLighting.Checked = My.Settings.HOD_ProcessBGLighting
  chkUseShaders.Checked = My.Settings.HOD_UseShaders
  chkBackup.Checked = My.Settings.HOD_MakeBackup
  chkDisplayFramerate.Checked = My.Settings.D3D_DisplayFrameRate

 End Sub

 Private Sub pbxTeamStripeColour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbxTeamColour.Click, pbxStripeColour.Click
  ' Delegate to team command button.
  If sender Is pbxTeamColour Then _
   cmdTeamStripeChoose_Click(cmdTeamChoose, EventArgs.Empty)

  ' Delegate to stripe command button.
  If sender Is pbxStripeColour Then _
   cmdTeamStripeChoose_Click(cmdStripeChoose, EventArgs.Empty)

 End Sub

 Private Sub sldTeamStripe_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sldTeamRed.Scroll, sldTeamGreen.Scroll, sldTeamBlue.Scroll, sldStripeRed.Scroll, sldStripeGreen.Scroll, sldStripeBlue.Scroll
  ' Update all components of team colour.
  lblTeamRed.Text = CStr(sldTeamRed.Value)
  lblTeamGreen.Text = CStr(sldTeamGreen.Value)
  lblTeamBlue.Text = CStr(sldTeamBlue.Value)

  ' Update all components of stripe colour.
  lblStripeRed.Text = CStr(sldStripeRed.Value)
  lblStripeGreen.Text = CStr(sldStripeGreen.Value)
  lblStripeBlue.Text = CStr(sldStripeBlue.Value)

  ' Update picture boxes.
  FillImage(m_TeamColourBitmap, sldTeamRed.Value, sldTeamGreen.Value, sldTeamBlue.Value)
  FillImage(m_StripeColourBitmap, sldStripeRed.Value, sldStripeGreen.Value, sldStripeBlue.Value)

  ' Mark for repaint.
  pbxTeamColour.Invalidate()
  pbxStripeColour.Invalidate()

  ' Update the team and stripe colours.
  m_HOD.TeamColour = New Direct3D.ColorValue(sldTeamRed.Value, sldTeamGreen.Value, sldTeamBlue.Value)
  m_HOD.StripeColour = New Direct3D.ColorValue(sldStripeRed.Value, sldStripeGreen.Value, sldStripeBlue.Value)

 End Sub

 Private Sub cmdTeamStripeChoose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTeamChoose.Click, cmdStripeChoose.Click
  ' Set initial colour to team colour if needed.
  If sender Is cmdTeamChoose Then _
   ColorDialog.Color = Color.FromArgb(sldTeamRed.Value, sldTeamGreen.Value, sldTeamBlue.Value)

  ' Set initial colour to stripe colour if needed.
  If sender Is cmdStripeChoose Then _
   ColorDialog.Color = Color.FromArgb(sldStripeRed.Value, sldStripeGreen.Value, sldStripeBlue.Value)

  ' See if user pressed cancel.
  If ColorDialog.ShowDialog() = Windows.Forms.DialogResult.Cancel Then _
   Exit Sub

  ' Set new team colour if needed.
  If sender Is cmdTeamChoose Then _
   sldTeamRed.Value = ColorDialog.Color.R _
 : sldTeamGreen.Value = ColorDialog.Color.G _
 : sldTeamBlue.Value = ColorDialog.Color.B

  ' Set new stripe colour if needed.
  If sender Is cmdStripeChoose Then _
   sldStripeRed.Value = ColorDialog.Color.R _
 : sldStripeGreen.Value = ColorDialog.Color.G _
 : sldStripeBlue.Value = ColorDialog.Color.B

  ' Update sliders.
  sldTeamStripe_Scroll(Nothing, EventArgs.Empty)

 End Sub

 Private Sub cmdBadgeTextureBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBadgeTextureBrowse.Click
  ' See if user pressed cancel.
  If OpenBadgeFileDialog.ShowDialog() = Windows.Forms.DialogResult.Cancel Then _
   Exit Sub

  ' Set badge texture.
  m_HOD.Badge = OpenBadgeFileDialog.FileName()

  ' Update text box.
  txtBadgeTexture.Text = m_HOD.Badge

 End Sub

 Private Sub cmdBadgeNoTexture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBadgeNoTexture.Click
  ' Set badge texture.
  m_HOD.Badge = ""

  ' Update text box.
  txtBadgeTexture.Text = m_HOD.Badge

 End Sub

 Private Sub ctlNextTime_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
 Handles chkProcessBGLighting.CheckedChanged, chkUseShaders.CheckedChanged, chkDisplayFramerate.CheckedChanged

  ' Get the control.
  Dim control As Control = CType(sender, Control)

  ' See if it's focused.
  If Not control.Focused Then _
   Exit Sub

  ' See if user has been altered.
  If control.Tag Is Nothing Then _
   MsgBox("This setting will fully take effect the next time you start" & vbCrLf & _
          "Cold Fusion HOD Editor.", MsgBoxStyle.Information, "Settings changed") _
 : control.Tag = control

 End Sub

 Private Sub cmdApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApply.Click
  ' Save settings.
  My.Settings.HOD_TeamColour = m_HOD.TeamColour.ToArgb()
  My.Settings.HOD_StripeColour = m_HOD.StripeColour.ToArgb()
  My.Settings.HOD_BadgeTexture = m_HOD.Badge

  My.Settings.HOD_Owner = txtUserName.Text
  My.Settings.HOD_ProcessBGLighting = chkProcessBGLighting.Checked
  My.Settings.HOD_UseShaders = chkUseShaders.Checked
  My.Settings.HOD_MakeBackup = chkBackup.Checked
  My.Settings.D3D_DisplayFrameRate = chkDisplayFramerate.Checked

  ' Close form.
  Me.Close()

 End Sub

 Private Sub cmdResetAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdResetAll.Click
  ' See if the user really meant to reset all settings.
  If MsgBox("This will reset all settings, including window positions and" & vbCrLf & _
            "sizes and close this window without saving changes. Continue?", _
            MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirmation") = MsgBoxResult.No Then _
   Exit Sub

  ' Reset all settings.
  My.Settings.Reset()

  ' Press cancel.
  cmdCancel_Click(Nothing, EventArgs.Empty)

 End Sub

 Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
  ' Reset to last saved settings.
  m_HOD.TeamColour = Direct3D.ColorValue.FromArgb(My.Settings.HOD_TeamColour)
  m_HOD.StripeColour = Direct3D.ColorValue.FromArgb(My.Settings.HOD_StripeColour)
  m_HOD.Badge = My.Settings.HOD_BadgeTexture

  ' Close form.
  Me.Close()

 End Sub

End Class
