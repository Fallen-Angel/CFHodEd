<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Options
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Options))
  Me.ColorDialog = New System.Windows.Forms.ColorDialog
  Me.pbxTeamColour = New System.Windows.Forms.PictureBox
  Me.cmdResetAll = New System.Windows.Forms.Button
  Me.cmdCancel = New System.Windows.Forms.Button
  Me.OpenBadgeFileDialog = New System.Windows.Forms.OpenFileDialog
  Me.cmdApply = New System.Windows.Forms.Button
  Me.fraTeam = New System.Windows.Forms.GroupBox
  Me.cmdTeamChoose = New System.Windows.Forms.Button
  Me.Label3 = New System.Windows.Forms.Label
  Me.Label2 = New System.Windows.Forms.Label
  Me.Label1 = New System.Windows.Forms.Label
  Me.lblTeamBlue = New System.Windows.Forms.Label
  Me.lblTeamGreen = New System.Windows.Forms.Label
  Me.sldTeamBlue = New System.Windows.Forms.TrackBar
  Me.sldTeamGreen = New System.Windows.Forms.TrackBar
  Me.lblTeamRed = New System.Windows.Forms.Label
  Me.sldTeamRed = New System.Windows.Forms.TrackBar
  Me.fraBadge = New System.Windows.Forms.GroupBox
  Me.cmdBadgeNoTexture = New System.Windows.Forms.Button
  Me.txtBadgeTexture = New System.Windows.Forms.TextBox
  Me.cmdBadgeTextureBrowse = New System.Windows.Forms.Button
  Me.Label13 = New System.Windows.Forms.Label
  Me.fraStripeColour = New System.Windows.Forms.GroupBox
  Me.cmdStripeChoose = New System.Windows.Forms.Button
  Me.Label4 = New System.Windows.Forms.Label
  Me.Label5 = New System.Windows.Forms.Label
  Me.Label6 = New System.Windows.Forms.Label
  Me.lblStripeBlue = New System.Windows.Forms.Label
  Me.lblStripeGreen = New System.Windows.Forms.Label
  Me.sldStripeBlue = New System.Windows.Forms.TrackBar
  Me.sldStripeGreen = New System.Windows.Forms.TrackBar
  Me.lblStripeRed = New System.Windows.Forms.Label
  Me.sldStripeRed = New System.Windows.Forms.TrackBar
  Me.pbxStripeColour = New System.Windows.Forms.PictureBox
  Me.tabMain = New System.Windows.Forms.TabControl
  Me.tabHOD = New System.Windows.Forms.TabPage
  Me.tabMisc = New System.Windows.Forms.TabPage
  Me.chkProcessBGLighting = New System.Windows.Forms.CheckBox
  Me.txtUserName = New System.Windows.Forms.TextBox
  Me.Label7 = New System.Windows.Forms.Label
  Me.chkBackup = New System.Windows.Forms.CheckBox
  Me.chkDisplayFramerate = New System.Windows.Forms.CheckBox
  Me.chkUseShaders = New System.Windows.Forms.CheckBox
  CType(Me.pbxTeamColour, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.fraTeam.SuspendLayout()
  CType(Me.sldTeamBlue, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.sldTeamGreen, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.sldTeamRed, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.fraBadge.SuspendLayout()
  Me.fraStripeColour.SuspendLayout()
  CType(Me.sldStripeBlue, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.sldStripeGreen, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.sldStripeRed, System.ComponentModel.ISupportInitialize).BeginInit()
  CType(Me.pbxStripeColour, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.tabMain.SuspendLayout()
  Me.tabHOD.SuspendLayout()
  Me.tabMisc.SuspendLayout()
  Me.SuspendLayout()
  '
  'ColorDialog
  '
  Me.ColorDialog.AnyColor = True
  Me.ColorDialog.FullOpen = True
  '
  'pbxTeamColour
  '
  Me.pbxTeamColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
  Me.pbxTeamColour.Location = New System.Drawing.Point(264, 24)
  Me.pbxTeamColour.Name = "pbxTeamColour"
  Me.pbxTeamColour.Size = New System.Drawing.Size(32, 104)
  Me.pbxTeamColour.TabIndex = 12
  Me.pbxTeamColour.TabStop = False
  '
  'cmdResetAll
  '
  Me.cmdResetAll.Location = New System.Drawing.Point(280, 304)
  Me.cmdResetAll.Name = "cmdResetAll"
  Me.cmdResetAll.Size = New System.Drawing.Size(96, 24)
  Me.cmdResetAll.TabIndex = 10
  Me.cmdResetAll.Text = "Reset All"
  Me.cmdResetAll.UseVisualStyleBackColor = True
  '
  'cmdCancel
  '
  Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
  Me.cmdCancel.Location = New System.Drawing.Point(568, 304)
  Me.cmdCancel.Name = "cmdCancel"
  Me.cmdCancel.Size = New System.Drawing.Size(80, 24)
  Me.cmdCancel.TabIndex = 11
  Me.cmdCancel.Text = "Cancel"
  Me.cmdCancel.UseVisualStyleBackColor = True
  '
  'OpenBadgeFileDialog
  '
  Me.OpenBadgeFileDialog.Filter = "Image Files (*.DDS; *.TGA; *.BMP; *.JPG; *.PNG)|*.DDS;*.TGA;*.BMP;*.JPG;*.PNG"
  '
  'cmdApply
  '
  Me.cmdApply.DialogResult = System.Windows.Forms.DialogResult.Cancel
  Me.cmdApply.Location = New System.Drawing.Point(8, 304)
  Me.cmdApply.Name = "cmdApply"
  Me.cmdApply.Size = New System.Drawing.Size(80, 24)
  Me.cmdApply.TabIndex = 12
  Me.cmdApply.Text = "Apply"
  Me.cmdApply.UseVisualStyleBackColor = True
  '
  'fraTeam
  '
  Me.fraTeam.Controls.Add(Me.cmdTeamChoose)
  Me.fraTeam.Controls.Add(Me.Label3)
  Me.fraTeam.Controls.Add(Me.Label2)
  Me.fraTeam.Controls.Add(Me.Label1)
  Me.fraTeam.Controls.Add(Me.lblTeamBlue)
  Me.fraTeam.Controls.Add(Me.lblTeamGreen)
  Me.fraTeam.Controls.Add(Me.sldTeamBlue)
  Me.fraTeam.Controls.Add(Me.sldTeamGreen)
  Me.fraTeam.Controls.Add(Me.lblTeamRed)
  Me.fraTeam.Controls.Add(Me.sldTeamRed)
  Me.fraTeam.Controls.Add(Me.pbxTeamColour)
  Me.fraTeam.Location = New System.Drawing.Point(12, 10)
  Me.fraTeam.Name = "fraTeam"
  Me.fraTeam.Size = New System.Drawing.Size(304, 176)
  Me.fraTeam.TabIndex = 13
  Me.fraTeam.TabStop = False
  Me.fraTeam.Text = "Team Colour"
  '
  'cmdTeamChoose
  '
  Me.cmdTeamChoose.Location = New System.Drawing.Point(264, 136)
  Me.cmdTeamChoose.Name = "cmdTeamChoose"
  Me.cmdTeamChoose.Size = New System.Drawing.Size(32, 32)
  Me.cmdTeamChoose.TabIndex = 25
  Me.cmdTeamChoose.Text = "..."
  Me.cmdTeamChoose.UseVisualStyleBackColor = True
  '
  'Label3
  '
  Me.Label3.Location = New System.Drawing.Point(8, 120)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(40, 45)
  Me.Label3.TabIndex = 24
  Me.Label3.Text = "Blue"
  Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'Label2
  '
  Me.Label2.Location = New System.Drawing.Point(8, 72)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(40, 45)
  Me.Label2.TabIndex = 23
  Me.Label2.Text = "Green"
  Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'Label1
  '
  Me.Label1.Location = New System.Drawing.Point(8, 24)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(40, 45)
  Me.Label1.TabIndex = 22
  Me.Label1.Text = "Red"
  Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'lblTeamBlue
  '
  Me.lblTeamBlue.Location = New System.Drawing.Point(232, 120)
  Me.lblTeamBlue.Name = "lblTeamBlue"
  Me.lblTeamBlue.Size = New System.Drawing.Size(32, 45)
  Me.lblTeamBlue.TabIndex = 18
  Me.lblTeamBlue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'lblTeamGreen
  '
  Me.lblTeamGreen.Location = New System.Drawing.Point(232, 72)
  Me.lblTeamGreen.Name = "lblTeamGreen"
  Me.lblTeamGreen.Size = New System.Drawing.Size(32, 45)
  Me.lblTeamGreen.TabIndex = 16
  Me.lblTeamGreen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'sldTeamBlue
  '
  Me.sldTeamBlue.LargeChange = 15
  Me.sldTeamBlue.Location = New System.Drawing.Point(48, 120)
  Me.sldTeamBlue.Maximum = 255
  Me.sldTeamBlue.Name = "sldTeamBlue"
  Me.sldTeamBlue.Size = New System.Drawing.Size(184, 45)
  Me.sldTeamBlue.TabIndex = 17
  Me.sldTeamBlue.TickFrequency = 15
  Me.sldTeamBlue.TickStyle = System.Windows.Forms.TickStyle.Both
  '
  'sldTeamGreen
  '
  Me.sldTeamGreen.LargeChange = 15
  Me.sldTeamGreen.Location = New System.Drawing.Point(48, 72)
  Me.sldTeamGreen.Maximum = 255
  Me.sldTeamGreen.Name = "sldTeamGreen"
  Me.sldTeamGreen.Size = New System.Drawing.Size(184, 45)
  Me.sldTeamGreen.TabIndex = 15
  Me.sldTeamGreen.TickFrequency = 15
  Me.sldTeamGreen.TickStyle = System.Windows.Forms.TickStyle.Both
  '
  'lblTeamRed
  '
  Me.lblTeamRed.Location = New System.Drawing.Point(232, 24)
  Me.lblTeamRed.Name = "lblTeamRed"
  Me.lblTeamRed.Size = New System.Drawing.Size(32, 45)
  Me.lblTeamRed.TabIndex = 14
  Me.lblTeamRed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'sldTeamRed
  '
  Me.sldTeamRed.LargeChange = 15
  Me.sldTeamRed.Location = New System.Drawing.Point(48, 24)
  Me.sldTeamRed.Maximum = 255
  Me.sldTeamRed.Name = "sldTeamRed"
  Me.sldTeamRed.Size = New System.Drawing.Size(184, 45)
  Me.sldTeamRed.TabIndex = 13
  Me.sldTeamRed.TickFrequency = 15
  Me.sldTeamRed.TickStyle = System.Windows.Forms.TickStyle.Both
  '
  'fraBadge
  '
  Me.fraBadge.Controls.Add(Me.cmdBadgeNoTexture)
  Me.fraBadge.Controls.Add(Me.txtBadgeTexture)
  Me.fraBadge.Controls.Add(Me.cmdBadgeTextureBrowse)
  Me.fraBadge.Controls.Add(Me.Label13)
  Me.fraBadge.Location = New System.Drawing.Point(12, 194)
  Me.fraBadge.Name = "fraBadge"
  Me.fraBadge.Size = New System.Drawing.Size(616, 56)
  Me.fraBadge.TabIndex = 27
  Me.fraBadge.TabStop = False
  Me.fraBadge.Text = "Badge"
  '
  'cmdBadgeNoTexture
  '
  Me.cmdBadgeNoTexture.Location = New System.Drawing.Point(544, 24)
  Me.cmdBadgeNoTexture.Name = "cmdBadgeNoTexture"
  Me.cmdBadgeNoTexture.Size = New System.Drawing.Size(64, 24)
  Me.cmdBadgeNoTexture.TabIndex = 30
  Me.cmdBadgeNoTexture.Text = "No Badge"
  Me.cmdBadgeNoTexture.UseVisualStyleBackColor = True
  '
  'txtBadgeTexture
  '
  Me.txtBadgeTexture.Location = New System.Drawing.Point(104, 24)
  Me.txtBadgeTexture.Name = "txtBadgeTexture"
  Me.txtBadgeTexture.ReadOnly = True
  Me.txtBadgeTexture.Size = New System.Drawing.Size(360, 20)
  Me.txtBadgeTexture.TabIndex = 29
  '
  'cmdBadgeTextureBrowse
  '
  Me.cmdBadgeTextureBrowse.Location = New System.Drawing.Point(472, 24)
  Me.cmdBadgeTextureBrowse.Name = "cmdBadgeTextureBrowse"
  Me.cmdBadgeTextureBrowse.Size = New System.Drawing.Size(64, 24)
  Me.cmdBadgeTextureBrowse.TabIndex = 28
  Me.cmdBadgeTextureBrowse.Text = "Browse"
  Me.cmdBadgeTextureBrowse.UseVisualStyleBackColor = True
  '
  'Label13
  '
  Me.Label13.Location = New System.Drawing.Point(8, 24)
  Me.Label13.Name = "Label13"
  Me.Label13.Size = New System.Drawing.Size(88, 23)
  Me.Label13.TabIndex = 27
  Me.Label13.Text = "Badge Texture:"
  Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'fraStripeColour
  '
  Me.fraStripeColour.Controls.Add(Me.cmdStripeChoose)
  Me.fraStripeColour.Controls.Add(Me.Label4)
  Me.fraStripeColour.Controls.Add(Me.Label5)
  Me.fraStripeColour.Controls.Add(Me.Label6)
  Me.fraStripeColour.Controls.Add(Me.lblStripeBlue)
  Me.fraStripeColour.Controls.Add(Me.lblStripeGreen)
  Me.fraStripeColour.Controls.Add(Me.sldStripeBlue)
  Me.fraStripeColour.Controls.Add(Me.sldStripeGreen)
  Me.fraStripeColour.Controls.Add(Me.lblStripeRed)
  Me.fraStripeColour.Controls.Add(Me.sldStripeRed)
  Me.fraStripeColour.Controls.Add(Me.pbxStripeColour)
  Me.fraStripeColour.Location = New System.Drawing.Point(324, 10)
  Me.fraStripeColour.Name = "fraStripeColour"
  Me.fraStripeColour.Size = New System.Drawing.Size(304, 176)
  Me.fraStripeColour.TabIndex = 25
  Me.fraStripeColour.TabStop = False
  Me.fraStripeColour.Text = "Stripe Colour"
  '
  'cmdStripeChoose
  '
  Me.cmdStripeChoose.Location = New System.Drawing.Point(264, 136)
  Me.cmdStripeChoose.Name = "cmdStripeChoose"
  Me.cmdStripeChoose.Size = New System.Drawing.Size(32, 32)
  Me.cmdStripeChoose.TabIndex = 26
  Me.cmdStripeChoose.Text = "..."
  Me.cmdStripeChoose.UseVisualStyleBackColor = True
  '
  'Label4
  '
  Me.Label4.Location = New System.Drawing.Point(8, 120)
  Me.Label4.Name = "Label4"
  Me.Label4.Size = New System.Drawing.Size(40, 45)
  Me.Label4.TabIndex = 24
  Me.Label4.Text = "Blue"
  Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'Label5
  '
  Me.Label5.Location = New System.Drawing.Point(8, 72)
  Me.Label5.Name = "Label5"
  Me.Label5.Size = New System.Drawing.Size(40, 45)
  Me.Label5.TabIndex = 23
  Me.Label5.Text = "Green"
  Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'Label6
  '
  Me.Label6.Location = New System.Drawing.Point(8, 24)
  Me.Label6.Name = "Label6"
  Me.Label6.Size = New System.Drawing.Size(40, 45)
  Me.Label6.TabIndex = 22
  Me.Label6.Text = "Red"
  Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'lblStripeBlue
  '
  Me.lblStripeBlue.Location = New System.Drawing.Point(232, 120)
  Me.lblStripeBlue.Name = "lblStripeBlue"
  Me.lblStripeBlue.Size = New System.Drawing.Size(32, 45)
  Me.lblStripeBlue.TabIndex = 18
  Me.lblStripeBlue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'lblStripeGreen
  '
  Me.lblStripeGreen.Location = New System.Drawing.Point(232, 72)
  Me.lblStripeGreen.Name = "lblStripeGreen"
  Me.lblStripeGreen.Size = New System.Drawing.Size(32, 45)
  Me.lblStripeGreen.TabIndex = 16
  Me.lblStripeGreen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'sldStripeBlue
  '
  Me.sldStripeBlue.LargeChange = 15
  Me.sldStripeBlue.Location = New System.Drawing.Point(48, 120)
  Me.sldStripeBlue.Maximum = 255
  Me.sldStripeBlue.Name = "sldStripeBlue"
  Me.sldStripeBlue.Size = New System.Drawing.Size(184, 45)
  Me.sldStripeBlue.TabIndex = 17
  Me.sldStripeBlue.TickFrequency = 15
  Me.sldStripeBlue.TickStyle = System.Windows.Forms.TickStyle.Both
  '
  'sldStripeGreen
  '
  Me.sldStripeGreen.LargeChange = 15
  Me.sldStripeGreen.Location = New System.Drawing.Point(48, 72)
  Me.sldStripeGreen.Maximum = 255
  Me.sldStripeGreen.Name = "sldStripeGreen"
  Me.sldStripeGreen.Size = New System.Drawing.Size(184, 45)
  Me.sldStripeGreen.TabIndex = 15
  Me.sldStripeGreen.TickFrequency = 15
  Me.sldStripeGreen.TickStyle = System.Windows.Forms.TickStyle.Both
  '
  'lblStripeRed
  '
  Me.lblStripeRed.Location = New System.Drawing.Point(232, 24)
  Me.lblStripeRed.Name = "lblStripeRed"
  Me.lblStripeRed.Size = New System.Drawing.Size(32, 45)
  Me.lblStripeRed.TabIndex = 14
  Me.lblStripeRed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'sldStripeRed
  '
  Me.sldStripeRed.LargeChange = 15
  Me.sldStripeRed.Location = New System.Drawing.Point(48, 24)
  Me.sldStripeRed.Maximum = 255
  Me.sldStripeRed.Name = "sldStripeRed"
  Me.sldStripeRed.Size = New System.Drawing.Size(184, 45)
  Me.sldStripeRed.TabIndex = 13
  Me.sldStripeRed.TickFrequency = 15
  Me.sldStripeRed.TickStyle = System.Windows.Forms.TickStyle.Both
  '
  'pbxStripeColour
  '
  Me.pbxStripeColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
  Me.pbxStripeColour.Location = New System.Drawing.Point(264, 24)
  Me.pbxStripeColour.Name = "pbxStripeColour"
  Me.pbxStripeColour.Size = New System.Drawing.Size(32, 104)
  Me.pbxStripeColour.TabIndex = 12
  Me.pbxStripeColour.TabStop = False
  '
  'tabMain
  '
  Me.tabMain.Controls.Add(Me.tabHOD)
  Me.tabMain.Controls.Add(Me.tabMisc)
  Me.tabMain.Location = New System.Drawing.Point(4, 8)
  Me.tabMain.Name = "tabMain"
  Me.tabMain.SelectedIndex = 0
  Me.tabMain.Size = New System.Drawing.Size(648, 288)
  Me.tabMain.TabIndex = 28
  '
  'tabHOD
  '
  Me.tabHOD.Controls.Add(Me.fraTeam)
  Me.tabHOD.Controls.Add(Me.fraStripeColour)
  Me.tabHOD.Controls.Add(Me.fraBadge)
  Me.tabHOD.Location = New System.Drawing.Point(4, 22)
  Me.tabHOD.Name = "tabHOD"
  Me.tabHOD.Padding = New System.Windows.Forms.Padding(3)
  Me.tabHOD.Size = New System.Drawing.Size(640, 262)
  Me.tabHOD.TabIndex = 0
  Me.tabHOD.Text = "HOD"
  Me.tabHOD.UseVisualStyleBackColor = True
  '
  'tabMisc
  '
  Me.tabMisc.Controls.Add(Me.chkUseShaders)
  Me.tabMisc.Controls.Add(Me.chkProcessBGLighting)
  Me.tabMisc.Controls.Add(Me.txtUserName)
  Me.tabMisc.Controls.Add(Me.Label7)
  Me.tabMisc.Controls.Add(Me.chkBackup)
  Me.tabMisc.Controls.Add(Me.chkDisplayFramerate)
  Me.tabMisc.Location = New System.Drawing.Point(4, 22)
  Me.tabMisc.Name = "tabMisc"
  Me.tabMisc.Padding = New System.Windows.Forms.Padding(3)
  Me.tabMisc.Size = New System.Drawing.Size(640, 262)
  Me.tabMisc.TabIndex = 1
  Me.tabMisc.Text = "Miscellaneous"
  Me.tabMisc.UseVisualStyleBackColor = True
  '
  'chkProcessBGLighting
  '
  Me.chkProcessBGLighting.Location = New System.Drawing.Point(8, 40)
  Me.chkProcessBGLighting.Name = "chkProcessBGLighting"
  Me.chkProcessBGLighting.Size = New System.Drawing.Size(624, 24)
  Me.chkProcessBGLighting.TabIndex = 4
  Me.chkProcessBGLighting.Text = "When opening\saving background HODs, open\save correspond BG-Lighting HODs as wel" & _
      "l"
  Me.chkProcessBGLighting.UseVisualStyleBackColor = True
  '
  'txtUserName
  '
  Me.txtUserName.Location = New System.Drawing.Point(232, 8)
  Me.txtUserName.Name = "txtUserName"
  Me.txtUserName.Size = New System.Drawing.Size(392, 20)
  Me.txtUserName.TabIndex = 3
  '
  'Label7
  '
  Me.Label7.Location = New System.Drawing.Point(8, 8)
  Me.Label7.Name = "Label7"
  Me.Label7.Size = New System.Drawing.Size(224, 20)
  Me.Label7.TabIndex = 2
  Me.Label7.Text = "User Name (this is written into the HOD):"
  Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'chkBackup
  '
  Me.chkBackup.Location = New System.Drawing.Point(8, 104)
  Me.chkBackup.Name = "chkBackup"
  Me.chkBackup.Size = New System.Drawing.Size(624, 24)
  Me.chkBackup.TabIndex = 1
  Me.chkBackup.Text = "Backup HOD files after opening"
  Me.chkBackup.UseVisualStyleBackColor = True
  '
  'chkDisplayFramerate
  '
  Me.chkDisplayFramerate.Location = New System.Drawing.Point(8, 136)
  Me.chkDisplayFramerate.Name = "chkDisplayFramerate"
  Me.chkDisplayFramerate.Size = New System.Drawing.Size(624, 24)
  Me.chkDisplayFramerate.TabIndex = 0
  Me.chkDisplayFramerate.Text = "Display frame rate"
  Me.chkDisplayFramerate.UseVisualStyleBackColor = True
  '
  'chkUseShaders
  '
  Me.chkUseShaders.Location = New System.Drawing.Point(8, 72)
  Me.chkUseShaders.Name = "chkUseShaders"
  Me.chkUseShaders.Size = New System.Drawing.Size(624, 24)
  Me.chkUseShaders.TabIndex = 5
  Me.chkUseShaders.Text = "Use vertex and pixel shaders to render the HOD"
  Me.chkUseShaders.UseVisualStyleBackColor = True
  '
  'Options
  '
  Me.AcceptButton = Me.cmdApply
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.CancelButton = Me.cmdCancel
  Me.ClientSize = New System.Drawing.Size(658, 332)
  Me.Controls.Add(Me.tabMain)
  Me.Controls.Add(Me.cmdApply)
  Me.Controls.Add(Me.cmdCancel)
  Me.Controls.Add(Me.cmdResetAll)
  Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.CFHodEd.My.MySettings.Default, "Form_Options_Location", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
  Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
  Me.Location = Global.CFHodEd.My.MySettings.Default.Form_Options_Location
  Me.MaximizeBox = False
  Me.MinimizeBox = False
  Me.Name = "Options"
  Me.ShowIcon = False
  Me.ShowInTaskbar = False
  Me.Text = "Options"
  CType(Me.pbxTeamColour, System.ComponentModel.ISupportInitialize).EndInit()
  Me.fraTeam.ResumeLayout(False)
  Me.fraTeam.PerformLayout()
  CType(Me.sldTeamBlue, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.sldTeamGreen, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.sldTeamRed, System.ComponentModel.ISupportInitialize).EndInit()
  Me.fraBadge.ResumeLayout(False)
  Me.fraBadge.PerformLayout()
  Me.fraStripeColour.ResumeLayout(False)
  Me.fraStripeColour.PerformLayout()
  CType(Me.sldStripeBlue, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.sldStripeGreen, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.sldStripeRed, System.ComponentModel.ISupportInitialize).EndInit()
  CType(Me.pbxStripeColour, System.ComponentModel.ISupportInitialize).EndInit()
  Me.tabMain.ResumeLayout(False)
  Me.tabHOD.ResumeLayout(False)
  Me.tabMisc.ResumeLayout(False)
  Me.tabMisc.PerformLayout()
  Me.ResumeLayout(False)

 End Sub
 Friend WithEvents ColorDialog As System.Windows.Forms.ColorDialog
 Friend WithEvents pbxTeamColour As System.Windows.Forms.PictureBox
 Friend WithEvents cmdResetAll As System.Windows.Forms.Button
 Friend WithEvents cmdCancel As System.Windows.Forms.Button
 Friend WithEvents OpenBadgeFileDialog As System.Windows.Forms.OpenFileDialog
 Friend WithEvents cmdApply As System.Windows.Forms.Button
 Friend WithEvents fraTeam As System.Windows.Forms.GroupBox
 Friend WithEvents Label3 As System.Windows.Forms.Label
 Friend WithEvents Label2 As System.Windows.Forms.Label
 Friend WithEvents Label1 As System.Windows.Forms.Label
 Friend WithEvents lblTeamBlue As System.Windows.Forms.Label
 Friend WithEvents lblTeamGreen As System.Windows.Forms.Label
 Friend WithEvents sldTeamBlue As System.Windows.Forms.TrackBar
 Friend WithEvents sldTeamGreen As System.Windows.Forms.TrackBar
 Friend WithEvents lblTeamRed As System.Windows.Forms.Label
 Friend WithEvents sldTeamRed As System.Windows.Forms.TrackBar
 Friend WithEvents fraBadge As System.Windows.Forms.GroupBox
 Friend WithEvents cmdBadgeNoTexture As System.Windows.Forms.Button
 Friend WithEvents txtBadgeTexture As System.Windows.Forms.TextBox
 Friend WithEvents cmdBadgeTextureBrowse As System.Windows.Forms.Button
 Friend WithEvents Label13 As System.Windows.Forms.Label
 Friend WithEvents fraStripeColour As System.Windows.Forms.GroupBox
 Friend WithEvents Label4 As System.Windows.Forms.Label
 Friend WithEvents Label5 As System.Windows.Forms.Label
 Friend WithEvents Label6 As System.Windows.Forms.Label
 Friend WithEvents lblStripeBlue As System.Windows.Forms.Label
 Friend WithEvents lblStripeGreen As System.Windows.Forms.Label
 Friend WithEvents sldStripeBlue As System.Windows.Forms.TrackBar
 Friend WithEvents sldStripeGreen As System.Windows.Forms.TrackBar
 Friend WithEvents lblStripeRed As System.Windows.Forms.Label
 Friend WithEvents sldStripeRed As System.Windows.Forms.TrackBar
 Friend WithEvents pbxStripeColour As System.Windows.Forms.PictureBox
 Friend WithEvents cmdTeamChoose As System.Windows.Forms.Button
 Friend WithEvents cmdStripeChoose As System.Windows.Forms.Button
 Friend WithEvents tabMain As System.Windows.Forms.TabControl
 Friend WithEvents tabHOD As System.Windows.Forms.TabPage
 Friend WithEvents tabMisc As System.Windows.Forms.TabPage
 Friend WithEvents chkDisplayFramerate As System.Windows.Forms.CheckBox
 Friend WithEvents chkBackup As System.Windows.Forms.CheckBox
 Friend WithEvents txtUserName As System.Windows.Forms.TextBox
 Friend WithEvents Label7 As System.Windows.Forms.Label
 Friend WithEvents chkProcessBGLighting As System.Windows.Forms.CheckBox
 Friend WithEvents chkUseShaders As System.Windows.Forms.CheckBox
End Class
