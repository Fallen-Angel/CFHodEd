<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JointSelector
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
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(JointSelector))
  Me.cmdCancel = New System.Windows.Forms.Button
  Me.cmdOK = New System.Windows.Forms.Button
  Me.tvwJoints = New System.Windows.Forms.TreeView
  Me.splMain = New System.Windows.Forms.SplitContainer
  Me.pnlLower = New System.Windows.Forms.Panel
  Me.splMain.Panel1.SuspendLayout()
  Me.splMain.Panel2.SuspendLayout()
  Me.splMain.SuspendLayout()
  Me.pnlLower.SuspendLayout()
  Me.SuspendLayout()
  '
  'cmdCancel
  '
  Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
  Me.cmdCancel.Location = New System.Drawing.Point(96, 0)
  Me.cmdCancel.Name = "cmdCancel"
  Me.cmdCancel.Size = New System.Drawing.Size(80, 24)
  Me.cmdCancel.TabIndex = 6
  Me.cmdCancel.Text = "Cancel"
  Me.cmdCancel.UseVisualStyleBackColor = True
  '
  'cmdOK
  '
  Me.cmdOK.Location = New System.Drawing.Point(8, 0)
  Me.cmdOK.Name = "cmdOK"
  Me.cmdOK.Size = New System.Drawing.Size(80, 24)
  Me.cmdOK.TabIndex = 5
  Me.cmdOK.Text = "OK"
  Me.cmdOK.UseVisualStyleBackColor = True
  '
  'tvwJoints
  '
  Me.tvwJoints.Dock = System.Windows.Forms.DockStyle.Fill
  Me.tvwJoints.Location = New System.Drawing.Point(0, 0)
  Me.tvwJoints.Name = "tvwJoints"
  Me.tvwJoints.ShowRootLines = False
  Me.tvwJoints.Size = New System.Drawing.Size(280, 218)
  Me.tvwJoints.TabIndex = 7
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
  Me.splMain.Panel1.Controls.Add(Me.tvwJoints)
  '
  'splMain.Panel2
  '
  Me.splMain.Panel2.Controls.Add(Me.pnlLower)
  Me.splMain.Size = New System.Drawing.Size(280, 252)
  Me.splMain.SplitterDistance = 218
  Me.splMain.TabIndex = 8
  '
  'pnlLower
  '
  Me.pnlLower.Controls.Add(Me.cmdOK)
  Me.pnlLower.Controls.Add(Me.cmdCancel)
  Me.pnlLower.Dock = System.Windows.Forms.DockStyle.Fill
  Me.pnlLower.Location = New System.Drawing.Point(0, 0)
  Me.pnlLower.Name = "pnlLower"
  Me.pnlLower.Size = New System.Drawing.Size(280, 30)
  Me.pnlLower.TabIndex = 0
  '
  'JointSelector
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = Global.CFHodEd.My.MySettings.Default.Form_JointSelector_ClientSize
  Me.ControlBox = False
  Me.Controls.Add(Me.splMain)
  Me.DataBindings.Add(New System.Windows.Forms.Binding("ClientSize", Global.CFHodEd.My.MySettings.Default, "Form_JointSelector_ClientSize", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.CFHodEd.My.MySettings.Default, "Form_JointSelector_Location", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
  Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
  Me.Location = Global.CFHodEd.My.MySettings.Default.Form_JointSelector_Location
  Me.MaximizeBox = False
  Me.MinimizeBox = False
  Me.MinimumSize = New System.Drawing.Size(296, 286)
  Me.Name = "JointSelector"
  Me.ShowIcon = False
  Me.ShowInTaskbar = False
  Me.Text = "Select a joint to add..."
  Me.splMain.Panel1.ResumeLayout(False)
  Me.splMain.Panel2.ResumeLayout(False)
  Me.splMain.ResumeLayout(False)
  Me.pnlLower.ResumeLayout(False)
  Me.ResumeLayout(False)

 End Sub
 Friend WithEvents cmdCancel As System.Windows.Forms.Button
 Friend WithEvents cmdOK As System.Windows.Forms.Button
 Friend WithEvents tvwJoints As System.Windows.Forms.TreeView
 Friend WithEvents splMain As System.Windows.Forms.SplitContainer
 Friend WithEvents pnlLower As System.Windows.Forms.Panel
End Class
