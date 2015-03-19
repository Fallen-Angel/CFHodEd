<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TexturePreview
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
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TexturePreview))
  Me.splMain = New System.Windows.Forms.SplitContainer
  Me.pnlDisplay = New System.Windows.Forms.Panel
  Me.pbxDisplay = New System.Windows.Forms.PictureBox
  Me.chkAlphaBelnd = New System.Windows.Forms.CheckBox
  Me.lblMag = New System.Windows.Forms.Label
  Me.Label1 = New System.Windows.Forms.Label
  Me.cmdExit = New System.Windows.Forms.Button
  Me.cmdZoomOut = New System.Windows.Forms.Button
  Me.cmdZoomIn = New System.Windows.Forms.Button
  Me.cmdReset = New System.Windows.Forms.Button
  Me.splMain.Panel1.SuspendLayout()
  Me.splMain.Panel2.SuspendLayout()
  Me.splMain.SuspendLayout()
  Me.pnlDisplay.SuspendLayout()
  CType(Me.pbxDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.SuspendLayout()
  '
  'splMain
  '
  Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
  Me.splMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
  Me.splMain.IsSplitterFixed = True
  Me.splMain.Location = New System.Drawing.Point(0, 0)
  Me.splMain.Name = "splMain"
  '
  'splMain.Panel1
  '
  Me.splMain.Panel1.Controls.Add(Me.pnlDisplay)
  '
  'splMain.Panel2
  '
  Me.splMain.Panel2.AutoScroll = True
  Me.splMain.Panel2.Controls.Add(Me.chkAlphaBelnd)
  Me.splMain.Panel2.Controls.Add(Me.lblMag)
  Me.splMain.Panel2.Controls.Add(Me.Label1)
  Me.splMain.Panel2.Controls.Add(Me.cmdExit)
  Me.splMain.Panel2.Controls.Add(Me.cmdZoomOut)
  Me.splMain.Panel2.Controls.Add(Me.cmdZoomIn)
  Me.splMain.Panel2.Controls.Add(Me.cmdReset)
  Me.splMain.Size = New System.Drawing.Size(506, 306)
  Me.splMain.SplitterDistance = 400
  Me.splMain.TabIndex = 0
  '
  'pnlDisplay
  '
  Me.pnlDisplay.Controls.Add(Me.pbxDisplay)
  Me.pnlDisplay.Dock = System.Windows.Forms.DockStyle.Fill
  Me.pnlDisplay.Location = New System.Drawing.Point(0, 0)
  Me.pnlDisplay.Name = "pnlDisplay"
  Me.pnlDisplay.Size = New System.Drawing.Size(400, 306)
  Me.pnlDisplay.TabIndex = 1
  '
  'pbxDisplay
  '
  Me.pbxDisplay.Location = New System.Drawing.Point(32, 32)
  Me.pbxDisplay.Name = "pbxDisplay"
  Me.pbxDisplay.Size = New System.Drawing.Size(80, 80)
  Me.pbxDisplay.TabIndex = 0
  Me.pbxDisplay.TabStop = False
  '
  'chkAlphaBelnd
  '
  Me.chkAlphaBelnd.Location = New System.Drawing.Point(8, 160)
  Me.chkAlphaBelnd.Name = "chkAlphaBelnd"
  Me.chkAlphaBelnd.Size = New System.Drawing.Size(88, 56)
  Me.chkAlphaBelnd.TabIndex = 6
  Me.chkAlphaBelnd.Text = "Enable Alpha Blending"
  Me.chkAlphaBelnd.UseVisualStyleBackColor = True
  '
  'lblMag
  '
  Me.lblMag.Location = New System.Drawing.Point(8, 128)
  Me.lblMag.Name = "lblMag"
  Me.lblMag.Size = New System.Drawing.Size(88, 24)
  Me.lblMag.TabIndex = 5
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.BackColor = System.Drawing.SystemColors.Control
  Me.Label1.Location = New System.Drawing.Point(8, 104)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(70, 13)
  Me.Label1.TabIndex = 4
  Me.Label1.Text = "Magnification"
  '
  'cmdExit
  '
  Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
  Me.cmdExit.Location = New System.Drawing.Point(8, 224)
  Me.cmdExit.Name = "cmdExit"
  Me.cmdExit.Size = New System.Drawing.Size(88, 23)
  Me.cmdExit.TabIndex = 3
  Me.cmdExit.Text = "Exit"
  Me.cmdExit.UseVisualStyleBackColor = True
  '
  'cmdZoomOut
  '
  Me.cmdZoomOut.Location = New System.Drawing.Point(8, 72)
  Me.cmdZoomOut.Name = "cmdZoomOut"
  Me.cmdZoomOut.Size = New System.Drawing.Size(80, 23)
  Me.cmdZoomOut.TabIndex = 2
  Me.cmdZoomOut.Text = "Zoom Out"
  Me.cmdZoomOut.UseVisualStyleBackColor = True
  '
  'cmdZoomIn
  '
  Me.cmdZoomIn.Location = New System.Drawing.Point(8, 40)
  Me.cmdZoomIn.Name = "cmdZoomIn"
  Me.cmdZoomIn.Size = New System.Drawing.Size(80, 23)
  Me.cmdZoomIn.TabIndex = 1
  Me.cmdZoomIn.Text = "Zoom In"
  Me.cmdZoomIn.UseVisualStyleBackColor = True
  '
  'cmdReset
  '
  Me.cmdReset.Location = New System.Drawing.Point(8, 8)
  Me.cmdReset.Name = "cmdReset"
  Me.cmdReset.Size = New System.Drawing.Size(80, 23)
  Me.cmdReset.TabIndex = 0
  Me.cmdReset.Text = "Reset"
  Me.cmdReset.UseVisualStyleBackColor = True
  '
  'TexturePreview
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.CancelButton = Me.cmdExit
  Me.ClientSize = Global.CFHodEd.My.MySettings.Default.Form_TexturePreview_ClientSize
  Me.Controls.Add(Me.splMain)
  Me.DataBindings.Add(New System.Windows.Forms.Binding("ClientSize", Global.CFHodEd.My.MySettings.Default, "Form_TexturePreview_ClientSize", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.CFHodEd.My.MySettings.Default, "Form_TexturePreview_Location", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
  Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
  Me.Location = Global.CFHodEd.My.MySettings.Default.Form_TexturePreview_Location
  Me.MaximizeBox = False
  Me.MinimizeBox = False
  Me.MinimumSize = New System.Drawing.Size(512, 256)
  Me.Name = "TexturePreview"
  Me.ShowIcon = False
  Me.ShowInTaskbar = False
  Me.Text = "Texture Preview"
  Me.splMain.Panel1.ResumeLayout(False)
  Me.splMain.Panel2.ResumeLayout(False)
  Me.splMain.Panel2.PerformLayout()
  Me.splMain.ResumeLayout(False)
  Me.pnlDisplay.ResumeLayout(False)
  CType(Me.pbxDisplay, System.ComponentModel.ISupportInitialize).EndInit()
  Me.ResumeLayout(False)

 End Sub
 Friend WithEvents splMain As System.Windows.Forms.SplitContainer
 Friend WithEvents cmdExit As System.Windows.Forms.Button
 Friend WithEvents cmdZoomOut As System.Windows.Forms.Button
 Friend WithEvents cmdZoomIn As System.Windows.Forms.Button
 Friend WithEvents cmdReset As System.Windows.Forms.Button
 Friend WithEvents pbxDisplay As System.Windows.Forms.PictureBox
 Friend WithEvents lblMag As System.Windows.Forms.Label
 Friend WithEvents Label1 As System.Windows.Forms.Label
 Friend WithEvents chkAlphaBelnd As System.Windows.Forms.CheckBox
 Friend WithEvents pnlDisplay As System.Windows.Forms.Panel
End Class
