<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MeshTransformer
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
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MeshTransformer))
  Me.pbxDisplay = New System.Windows.Forms.PictureBox
  Me.fraMatrix = New System.Windows.Forms.GroupBox
  Me.lblReverseFaces = New System.Windows.Forms.Label
  Me.txtM33 = New System.Windows.Forms.TextBox
  Me.txtM32 = New System.Windows.Forms.TextBox
  Me.txtM31 = New System.Windows.Forms.TextBox
  Me.txtM30 = New System.Windows.Forms.TextBox
  Me.txtM23 = New System.Windows.Forms.TextBox
  Me.txtM22 = New System.Windows.Forms.TextBox
  Me.txtM21 = New System.Windows.Forms.TextBox
  Me.txtM20 = New System.Windows.Forms.TextBox
  Me.txtM13 = New System.Windows.Forms.TextBox
  Me.txtM03 = New System.Windows.Forms.TextBox
  Me.txtM12 = New System.Windows.Forms.TextBox
  Me.txtM02 = New System.Windows.Forms.TextBox
  Me.txtM11 = New System.Windows.Forms.TextBox
  Me.txtM10 = New System.Windows.Forms.TextBox
  Me.txtM01 = New System.Windows.Forms.TextBox
  Me.txtM00 = New System.Windows.Forms.TextBox
  Me.splMain = New System.Windows.Forms.SplitContainer
  Me.tlpLeft = New System.Windows.Forms.TableLayoutPanel
  Me.flpLowerLeft = New System.Windows.Forms.FlowLayoutPanel
  Me.cmdApply = New System.Windows.Forms.Button
  Me.cmdResetCamera = New System.Windows.Forms.Button
  Me.cmdCancel = New System.Windows.Forms.Button
  Me.fraOperations = New System.Windows.Forms.GroupBox
  Me.cmdReset = New System.Windows.Forms.Button
  Me.cmdMirror = New System.Windows.Forms.Button
  Me.cmdRotate = New System.Windows.Forms.Button
  Me.cmdScaling = New System.Windows.Forms.Button
  Me.cmdTranslate = New System.Windows.Forms.Button
  Me.lstOp = New System.Windows.Forms.ListBox
  Me.pnlDisplay = New System.Windows.Forms.Panel
  CType(Me.pbxDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.fraMatrix.SuspendLayout()
  Me.splMain.Panel1.SuspendLayout()
  Me.splMain.Panel2.SuspendLayout()
  Me.splMain.SuspendLayout()
  Me.tlpLeft.SuspendLayout()
  Me.flpLowerLeft.SuspendLayout()
  Me.fraOperations.SuspendLayout()
  Me.pnlDisplay.SuspendLayout()
  Me.SuspendLayout()
  '
  'pbxDisplay
  '
  Me.pbxDisplay.Location = New System.Drawing.Point(32, 24)
  Me.pbxDisplay.Name = "pbxDisplay"
  Me.pbxDisplay.Size = New System.Drawing.Size(80, 80)
  Me.pbxDisplay.TabIndex = 1
  Me.pbxDisplay.TabStop = False
  '
  'fraMatrix
  '
  Me.fraMatrix.Controls.Add(Me.lblReverseFaces)
  Me.fraMatrix.Controls.Add(Me.txtM33)
  Me.fraMatrix.Controls.Add(Me.txtM32)
  Me.fraMatrix.Controls.Add(Me.txtM31)
  Me.fraMatrix.Controls.Add(Me.txtM30)
  Me.fraMatrix.Controls.Add(Me.txtM23)
  Me.fraMatrix.Controls.Add(Me.txtM22)
  Me.fraMatrix.Controls.Add(Me.txtM21)
  Me.fraMatrix.Controls.Add(Me.txtM20)
  Me.fraMatrix.Controls.Add(Me.txtM13)
  Me.fraMatrix.Controls.Add(Me.txtM03)
  Me.fraMatrix.Controls.Add(Me.txtM12)
  Me.fraMatrix.Controls.Add(Me.txtM02)
  Me.fraMatrix.Controls.Add(Me.txtM11)
  Me.fraMatrix.Controls.Add(Me.txtM10)
  Me.fraMatrix.Controls.Add(Me.txtM01)
  Me.fraMatrix.Controls.Add(Me.txtM00)
  Me.fraMatrix.Dock = System.Windows.Forms.DockStyle.Fill
  Me.fraMatrix.Location = New System.Drawing.Point(3, 267)
  Me.fraMatrix.Name = "fraMatrix"
  Me.fraMatrix.Size = New System.Drawing.Size(274, 138)
  Me.fraMatrix.TabIndex = 5
  Me.fraMatrix.TabStop = False
  Me.fraMatrix.Text = "Transform Matrix"
  '
  'lblReverseFaces
  '
  Me.lblReverseFaces.AutoSize = True
  Me.lblReverseFaces.ForeColor = System.Drawing.Color.Red
  Me.lblReverseFaces.Location = New System.Drawing.Point(8, 120)
  Me.lblReverseFaces.Name = "lblReverseFaces"
  Me.lblReverseFaces.Size = New System.Drawing.Size(127, 13)
  Me.lblReverseFaces.TabIndex = 16
  Me.lblReverseFaces.Text = "With reversed face order."
  '
  'txtM33
  '
  Me.txtM33.Location = New System.Drawing.Point(200, 96)
  Me.txtM33.Name = "txtM33"
  Me.txtM33.ReadOnly = True
  Me.txtM33.Size = New System.Drawing.Size(56, 20)
  Me.txtM33.TabIndex = 15
  '
  'txtM32
  '
  Me.txtM32.Location = New System.Drawing.Point(136, 96)
  Me.txtM32.Name = "txtM32"
  Me.txtM32.ReadOnly = True
  Me.txtM32.Size = New System.Drawing.Size(56, 20)
  Me.txtM32.TabIndex = 14
  '
  'txtM31
  '
  Me.txtM31.Location = New System.Drawing.Point(72, 96)
  Me.txtM31.Name = "txtM31"
  Me.txtM31.ReadOnly = True
  Me.txtM31.Size = New System.Drawing.Size(56, 20)
  Me.txtM31.TabIndex = 13
  '
  'txtM30
  '
  Me.txtM30.Location = New System.Drawing.Point(8, 96)
  Me.txtM30.Name = "txtM30"
  Me.txtM30.ReadOnly = True
  Me.txtM30.Size = New System.Drawing.Size(56, 20)
  Me.txtM30.TabIndex = 12
  '
  'txtM23
  '
  Me.txtM23.Location = New System.Drawing.Point(200, 72)
  Me.txtM23.Name = "txtM23"
  Me.txtM23.ReadOnly = True
  Me.txtM23.Size = New System.Drawing.Size(56, 20)
  Me.txtM23.TabIndex = 11
  '
  'txtM22
  '
  Me.txtM22.Location = New System.Drawing.Point(136, 72)
  Me.txtM22.Name = "txtM22"
  Me.txtM22.ReadOnly = True
  Me.txtM22.Size = New System.Drawing.Size(56, 20)
  Me.txtM22.TabIndex = 10
  '
  'txtM21
  '
  Me.txtM21.Location = New System.Drawing.Point(72, 72)
  Me.txtM21.Name = "txtM21"
  Me.txtM21.ReadOnly = True
  Me.txtM21.Size = New System.Drawing.Size(56, 20)
  Me.txtM21.TabIndex = 9
  '
  'txtM20
  '
  Me.txtM20.Location = New System.Drawing.Point(8, 72)
  Me.txtM20.Name = "txtM20"
  Me.txtM20.ReadOnly = True
  Me.txtM20.Size = New System.Drawing.Size(56, 20)
  Me.txtM20.TabIndex = 8
  '
  'txtM13
  '
  Me.txtM13.Location = New System.Drawing.Point(200, 48)
  Me.txtM13.Name = "txtM13"
  Me.txtM13.ReadOnly = True
  Me.txtM13.Size = New System.Drawing.Size(56, 20)
  Me.txtM13.TabIndex = 7
  '
  'txtM03
  '
  Me.txtM03.Location = New System.Drawing.Point(200, 24)
  Me.txtM03.Name = "txtM03"
  Me.txtM03.ReadOnly = True
  Me.txtM03.Size = New System.Drawing.Size(56, 20)
  Me.txtM03.TabIndex = 3
  '
  'txtM12
  '
  Me.txtM12.Location = New System.Drawing.Point(136, 48)
  Me.txtM12.Name = "txtM12"
  Me.txtM12.ReadOnly = True
  Me.txtM12.Size = New System.Drawing.Size(56, 20)
  Me.txtM12.TabIndex = 6
  '
  'txtM02
  '
  Me.txtM02.Location = New System.Drawing.Point(136, 24)
  Me.txtM02.Name = "txtM02"
  Me.txtM02.ReadOnly = True
  Me.txtM02.Size = New System.Drawing.Size(56, 20)
  Me.txtM02.TabIndex = 2
  '
  'txtM11
  '
  Me.txtM11.Location = New System.Drawing.Point(72, 48)
  Me.txtM11.Name = "txtM11"
  Me.txtM11.ReadOnly = True
  Me.txtM11.Size = New System.Drawing.Size(56, 20)
  Me.txtM11.TabIndex = 5
  '
  'txtM10
  '
  Me.txtM10.Location = New System.Drawing.Point(8, 48)
  Me.txtM10.Name = "txtM10"
  Me.txtM10.ReadOnly = True
  Me.txtM10.Size = New System.Drawing.Size(56, 20)
  Me.txtM10.TabIndex = 4
  '
  'txtM01
  '
  Me.txtM01.Location = New System.Drawing.Point(72, 24)
  Me.txtM01.Name = "txtM01"
  Me.txtM01.ReadOnly = True
  Me.txtM01.Size = New System.Drawing.Size(56, 20)
  Me.txtM01.TabIndex = 1
  '
  'txtM00
  '
  Me.txtM00.Location = New System.Drawing.Point(8, 24)
  Me.txtM00.Name = "txtM00"
  Me.txtM00.ReadOnly = True
  Me.txtM00.Size = New System.Drawing.Size(56, 20)
  Me.txtM00.TabIndex = 0
  '
  'splMain
  '
  Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
  Me.splMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
  Me.splMain.IsSplitterFixed = True
  Me.splMain.Location = New System.Drawing.Point(0, 0)
  Me.splMain.Name = "splMain"
  '
  'splMain.Panel1
  '
  Me.splMain.Panel1.Controls.Add(Me.tlpLeft)
  '
  'splMain.Panel2
  '
  Me.splMain.Panel2.Controls.Add(Me.pnlDisplay)
  Me.splMain.Size = New System.Drawing.Size(634, 452)
  Me.splMain.SplitterDistance = 280
  Me.splMain.TabIndex = 1
  '
  'tlpLeft
  '
  Me.tlpLeft.ColumnCount = 1
  Me.tlpLeft.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
  Me.tlpLeft.Controls.Add(Me.flpLowerLeft, 0, 2)
  Me.tlpLeft.Controls.Add(Me.fraOperations, 0, 0)
  Me.tlpLeft.Controls.Add(Me.fraMatrix, 0, 1)
  Me.tlpLeft.Dock = System.Windows.Forms.DockStyle.Fill
  Me.tlpLeft.Location = New System.Drawing.Point(0, 0)
  Me.tlpLeft.Name = "tlpLeft"
  Me.tlpLeft.RowCount = 3
  Me.tlpLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 264.0!))
  Me.tlpLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 144.0!))
  Me.tlpLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
  Me.tlpLeft.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
  Me.tlpLeft.Size = New System.Drawing.Size(280, 452)
  Me.tlpLeft.TabIndex = 6
  '
  'flpLowerLeft
  '
  Me.flpLowerLeft.Controls.Add(Me.cmdApply)
  Me.flpLowerLeft.Controls.Add(Me.cmdResetCamera)
  Me.flpLowerLeft.Controls.Add(Me.cmdCancel)
  Me.flpLowerLeft.Dock = System.Windows.Forms.DockStyle.Fill
  Me.flpLowerLeft.Location = New System.Drawing.Point(3, 411)
  Me.flpLowerLeft.Name = "flpLowerLeft"
  Me.flpLowerLeft.Size = New System.Drawing.Size(274, 38)
  Me.flpLowerLeft.TabIndex = 2
  '
  'cmdApply
  '
  Me.cmdApply.Location = New System.Drawing.Point(8, 8)
  Me.cmdApply.Margin = New System.Windows.Forms.Padding(8, 8, 8, 0)
  Me.cmdApply.Name = "cmdApply"
  Me.cmdApply.Size = New System.Drawing.Size(72, 24)
  Me.cmdApply.TabIndex = 7
  Me.cmdApply.Text = "Apply"
  Me.cmdApply.UseVisualStyleBackColor = True
  '
  'cmdResetCamera
  '
  Me.cmdResetCamera.Location = New System.Drawing.Point(88, 8)
  Me.cmdResetCamera.Margin = New System.Windows.Forms.Padding(0, 8, 8, 0)
  Me.cmdResetCamera.Name = "cmdResetCamera"
  Me.cmdResetCamera.Size = New System.Drawing.Size(96, 24)
  Me.cmdResetCamera.TabIndex = 8
  Me.cmdResetCamera.Text = "Reset Camera"
  Me.cmdResetCamera.UseVisualStyleBackColor = True
  '
  'cmdCancel
  '
  Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
  Me.cmdCancel.Location = New System.Drawing.Point(192, 8)
  Me.cmdCancel.Margin = New System.Windows.Forms.Padding(0, 8, 8, 0)
  Me.cmdCancel.Name = "cmdCancel"
  Me.cmdCancel.Size = New System.Drawing.Size(72, 24)
  Me.cmdCancel.TabIndex = 9
  Me.cmdCancel.Text = "Cancel"
  Me.cmdCancel.UseVisualStyleBackColor = True
  '
  'fraOperations
  '
  Me.fraOperations.Controls.Add(Me.cmdReset)
  Me.fraOperations.Controls.Add(Me.cmdMirror)
  Me.fraOperations.Controls.Add(Me.cmdRotate)
  Me.fraOperations.Controls.Add(Me.cmdScaling)
  Me.fraOperations.Controls.Add(Me.cmdTranslate)
  Me.fraOperations.Controls.Add(Me.lstOp)
  Me.fraOperations.Dock = System.Windows.Forms.DockStyle.Fill
  Me.fraOperations.Location = New System.Drawing.Point(3, 3)
  Me.fraOperations.Name = "fraOperations"
  Me.fraOperations.Size = New System.Drawing.Size(274, 258)
  Me.fraOperations.TabIndex = 1
  Me.fraOperations.TabStop = False
  Me.fraOperations.Text = "Transform Operations"
  '
  'cmdReset
  '
  Me.cmdReset.Location = New System.Drawing.Point(96, 224)
  Me.cmdReset.Name = "cmdReset"
  Me.cmdReset.Size = New System.Drawing.Size(80, 24)
  Me.cmdReset.TabIndex = 5
  Me.cmdReset.Text = "Reset"
  Me.cmdReset.UseVisualStyleBackColor = True
  '
  'cmdMirror
  '
  Me.cmdMirror.Location = New System.Drawing.Point(8, 224)
  Me.cmdMirror.Name = "cmdMirror"
  Me.cmdMirror.Size = New System.Drawing.Size(80, 24)
  Me.cmdMirror.TabIndex = 4
  Me.cmdMirror.Text = "Mirror"
  Me.cmdMirror.UseVisualStyleBackColor = True
  '
  'cmdRotate
  '
  Me.cmdRotate.Location = New System.Drawing.Point(96, 192)
  Me.cmdRotate.Name = "cmdRotate"
  Me.cmdRotate.Size = New System.Drawing.Size(80, 24)
  Me.cmdRotate.TabIndex = 3
  Me.cmdRotate.Text = "Rotate"
  Me.cmdRotate.UseVisualStyleBackColor = True
  '
  'cmdScaling
  '
  Me.cmdScaling.Location = New System.Drawing.Point(184, 192)
  Me.cmdScaling.Name = "cmdScaling"
  Me.cmdScaling.Size = New System.Drawing.Size(80, 24)
  Me.cmdScaling.TabIndex = 2
  Me.cmdScaling.Text = "Scaling"
  Me.cmdScaling.UseVisualStyleBackColor = True
  '
  'cmdTranslate
  '
  Me.cmdTranslate.Location = New System.Drawing.Point(8, 192)
  Me.cmdTranslate.Name = "cmdTranslate"
  Me.cmdTranslate.Size = New System.Drawing.Size(80, 24)
  Me.cmdTranslate.TabIndex = 1
  Me.cmdTranslate.Text = "Translate"
  Me.cmdTranslate.UseVisualStyleBackColor = True
  '
  'lstOp
  '
  Me.lstOp.FormattingEnabled = True
  Me.lstOp.Location = New System.Drawing.Point(8, 24)
  Me.lstOp.Name = "lstOp"
  Me.lstOp.Size = New System.Drawing.Size(256, 160)
  Me.lstOp.TabIndex = 0
  '
  'pnlDisplay
  '
  Me.pnlDisplay.Controls.Add(Me.pbxDisplay)
  Me.pnlDisplay.Dock = System.Windows.Forms.DockStyle.Fill
  Me.pnlDisplay.Location = New System.Drawing.Point(0, 0)
  Me.pnlDisplay.Name = "pnlDisplay"
  Me.pnlDisplay.Size = New System.Drawing.Size(350, 452)
  Me.pnlDisplay.TabIndex = 2
  '
  'MeshTransformer
  '
  Me.AcceptButton = Me.cmdApply
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.CancelButton = Me.cmdCancel
  Me.ClientSize = Global.CFHodEd.My.MySettings.Default.Form_MeshTransformer_ClientSize
  Me.ControlBox = False
  Me.Controls.Add(Me.splMain)
  Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.CFHodEd.My.MySettings.Default, "Form_MeshTransformer_Location", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.DataBindings.Add(New System.Windows.Forms.Binding("ClientSize", Global.CFHodEd.My.MySettings.Default, "Form_MeshTransformer_ClientSize", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
  Me.Location = Global.CFHodEd.My.MySettings.Default.Form_MeshTransformer_Location
  Me.MaximizeBox = False
  Me.MinimizeBox = False
  Me.MinimumSize = New System.Drawing.Size(600, 468)
  Me.Name = "MeshTransformer"
  Me.ShowIcon = False
  Me.ShowInTaskbar = False
  Me.Text = "Mesh Transformer"
  CType(Me.pbxDisplay, System.ComponentModel.ISupportInitialize).EndInit()
  Me.fraMatrix.ResumeLayout(False)
  Me.fraMatrix.PerformLayout()
  Me.splMain.Panel1.ResumeLayout(False)
  Me.splMain.Panel2.ResumeLayout(False)
  Me.splMain.ResumeLayout(False)
  Me.tlpLeft.ResumeLayout(False)
  Me.flpLowerLeft.ResumeLayout(False)
  Me.fraOperations.ResumeLayout(False)
  Me.pnlDisplay.ResumeLayout(False)
  Me.ResumeLayout(False)

 End Sub
 Friend WithEvents pbxDisplay As System.Windows.Forms.PictureBox
 Friend WithEvents fraMatrix As System.Windows.Forms.GroupBox
 Friend WithEvents txtM33 As System.Windows.Forms.TextBox
 Friend WithEvents txtM32 As System.Windows.Forms.TextBox
 Friend WithEvents txtM31 As System.Windows.Forms.TextBox
 Friend WithEvents txtM30 As System.Windows.Forms.TextBox
 Friend WithEvents txtM23 As System.Windows.Forms.TextBox
 Friend WithEvents txtM22 As System.Windows.Forms.TextBox
 Friend WithEvents txtM21 As System.Windows.Forms.TextBox
 Friend WithEvents txtM20 As System.Windows.Forms.TextBox
 Friend WithEvents txtM13 As System.Windows.Forms.TextBox
 Friend WithEvents txtM03 As System.Windows.Forms.TextBox
 Friend WithEvents txtM12 As System.Windows.Forms.TextBox
 Friend WithEvents txtM02 As System.Windows.Forms.TextBox
 Friend WithEvents txtM11 As System.Windows.Forms.TextBox
 Friend WithEvents txtM10 As System.Windows.Forms.TextBox
 Friend WithEvents txtM01 As System.Windows.Forms.TextBox
 Friend WithEvents txtM00 As System.Windows.Forms.TextBox
 Friend WithEvents splMain As System.Windows.Forms.SplitContainer
 Friend WithEvents tlpLeft As System.Windows.Forms.TableLayoutPanel
 Friend WithEvents fraOperations As System.Windows.Forms.GroupBox
 Friend WithEvents cmdReset As System.Windows.Forms.Button
 Friend WithEvents cmdMirror As System.Windows.Forms.Button
 Friend WithEvents cmdRotate As System.Windows.Forms.Button
 Friend WithEvents cmdScaling As System.Windows.Forms.Button
 Friend WithEvents cmdTranslate As System.Windows.Forms.Button
 Friend WithEvents lstOp As System.Windows.Forms.ListBox
 Friend WithEvents lblReverseFaces As System.Windows.Forms.Label
 Friend WithEvents flpLowerLeft As System.Windows.Forms.FlowLayoutPanel
 Friend WithEvents cmdApply As System.Windows.Forms.Button
 Friend WithEvents cmdResetCamera As System.Windows.Forms.Button
 Friend WithEvents cmdCancel As System.Windows.Forms.Button
 Friend WithEvents pnlDisplay As System.Windows.Forms.Panel
End Class
