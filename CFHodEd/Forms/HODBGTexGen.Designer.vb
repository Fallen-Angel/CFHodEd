<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HODBGTexGen
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
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HODBGTexGen))
  Me.splMain = New System.Windows.Forms.SplitContainer
  Me.pbxDisplay = New System.Windows.Forms.PictureBox
  Me.tlpLower = New System.Windows.Forms.TableLayoutPanel
  Me.cmdSave = New System.Windows.Forms.Button
  Me.cmdExit = New System.Windows.Forms.Button
  Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog
  Me.splMain.Panel1.SuspendLayout()
  Me.splMain.Panel2.SuspendLayout()
  Me.splMain.SuspendLayout()
  CType(Me.pbxDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.tlpLower.SuspendLayout()
  Me.SuspendLayout()
  '
  'splMain
  '
  Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
  Me.splMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
  Me.splMain.IsSplitterFixed = True
  Me.splMain.Location = New System.Drawing.Point(0, 0)
  Me.splMain.Name = "splMain"
  Me.splMain.Orientation = System.Windows.Forms.Orientation.Horizontal
  '
  'splMain.Panel1
  '
  Me.splMain.Panel1.Controls.Add(Me.pbxDisplay)
  '
  'splMain.Panel2
  '
  Me.splMain.Panel2.Controls.Add(Me.tlpLower)
  Me.splMain.Panel2MinSize = 30
  Me.splMain.Size = New System.Drawing.Size(384, 284)
  Me.splMain.SplitterDistance = 250
  Me.splMain.TabIndex = 0
  '
  'pbxDisplay
  '
  Me.pbxDisplay.Dock = System.Windows.Forms.DockStyle.Fill
  Me.pbxDisplay.Location = New System.Drawing.Point(0, 0)
  Me.pbxDisplay.Name = "pbxDisplay"
  Me.pbxDisplay.Size = New System.Drawing.Size(384, 250)
  Me.pbxDisplay.TabIndex = 0
  Me.pbxDisplay.TabStop = False
  '
  'tlpLower
  '
  Me.tlpLower.ColumnCount = 4
  Me.tlpLower.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
  Me.tlpLower.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
  Me.tlpLower.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
  Me.tlpLower.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
  Me.tlpLower.Controls.Add(Me.cmdSave, 2, 0)
  Me.tlpLower.Controls.Add(Me.cmdExit, 1, 0)
  Me.tlpLower.Dock = System.Windows.Forms.DockStyle.Fill
  Me.tlpLower.Location = New System.Drawing.Point(0, 0)
  Me.tlpLower.Name = "tlpLower"
  Me.tlpLower.RowCount = 1
  Me.tlpLower.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
  Me.tlpLower.Size = New System.Drawing.Size(384, 30)
  Me.tlpLower.TabIndex = 0
  '
  'cmdSave
  '
  Me.cmdSave.Dock = System.Windows.Forms.DockStyle.Fill
  Me.cmdSave.Location = New System.Drawing.Point(195, 3)
  Me.cmdSave.Name = "cmdSave"
  Me.cmdSave.Size = New System.Drawing.Size(74, 24)
  Me.cmdSave.TabIndex = 1
  Me.cmdSave.Text = "Save"
  Me.cmdSave.UseVisualStyleBackColor = True
  '
  'cmdExit
  '
  Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
  Me.cmdExit.Dock = System.Windows.Forms.DockStyle.Fill
  Me.cmdExit.Location = New System.Drawing.Point(115, 3)
  Me.cmdExit.Name = "cmdExit"
  Me.cmdExit.Size = New System.Drawing.Size(74, 24)
  Me.cmdExit.TabIndex = 0
  Me.cmdExit.Text = "Exit"
  Me.cmdExit.UseVisualStyleBackColor = True
  '
  'SaveFileDialog
  '
  Me.SaveFileDialog.DefaultExt = "tga"
  Me.SaveFileDialog.Filter = "Image files (*.bmp; *.dds; *.jpg; *.png; *.tga)|*.bmp;*.dds;*.jpg;*.png;*.tga"
  '
  'HODBGTexGen
  '
  Me.AcceptButton = Me.cmdSave
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.CancelButton = Me.cmdExit
  Me.ClientSize = Global.CFHodEd.My.MySettings.Default.Form_HODBGTexGen_ClientSize
  Me.ControlBox = False
  Me.Controls.Add(Me.splMain)
  Me.DataBindings.Add(New System.Windows.Forms.Binding("ClientSize", Global.CFHodEd.My.MySettings.Default, "Form_HODBGTexGen_ClientSize", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.CFHodEd.My.MySettings.Default, "Form_HODBGTexGen_Location", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
  Me.Location = Global.CFHodEd.My.MySettings.Default.Form_HODBGTexGen_Location
  Me.MaximizeBox = False
  Me.MinimizeBox = False
  Me.MinimumSize = New System.Drawing.Size(400, 300)
  Me.Name = "HODBGTexGen"
  Me.ShowIcon = False
  Me.ShowInTaskbar = False
  Me.Text = "HOD Background Texture Generator"
  Me.splMain.Panel1.ResumeLayout(False)
  Me.splMain.Panel2.ResumeLayout(False)
  Me.splMain.ResumeLayout(False)
  CType(Me.pbxDisplay, System.ComponentModel.ISupportInitialize).EndInit()
  Me.tlpLower.ResumeLayout(False)
  Me.ResumeLayout(False)

 End Sub
 Private WithEvents splMain As System.Windows.Forms.SplitContainer
 Friend WithEvents pbxDisplay As System.Windows.Forms.PictureBox
 Friend WithEvents tlpLower As System.Windows.Forms.TableLayoutPanel
 Friend WithEvents cmdSave As System.Windows.Forms.Button
 Friend WithEvents cmdExit As System.Windows.Forms.Button
 Friend WithEvents SaveFileDialog As System.Windows.Forms.SaveFileDialog
End Class
