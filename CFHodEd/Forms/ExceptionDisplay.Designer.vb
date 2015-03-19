<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExceptionDisplay
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
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExceptionDisplay))
  Me.tlpMain = New System.Windows.Forms.TableLayoutPanel
  Me.fraException = New System.Windows.Forms.GroupBox
  Me.txtException = New System.Windows.Forms.TextBox
  Me.fraTrace = New System.Windows.Forms.GroupBox
  Me.txtTrace = New System.Windows.Forms.TextBox
  Me.splTop = New System.Windows.Forms.SplitContainer
  Me.Label = New System.Windows.Forms.Label
  Me.cmdContinue = New System.Windows.Forms.Button
  Me.cmdExit = New System.Windows.Forms.Button
  Me.tlpMain.SuspendLayout()
  Me.fraException.SuspendLayout()
  Me.fraTrace.SuspendLayout()
  Me.splTop.Panel1.SuspendLayout()
  Me.splTop.Panel2.SuspendLayout()
  Me.splTop.SuspendLayout()
  Me.SuspendLayout()
  '
  'tlpMain
  '
  Me.tlpMain.ColumnCount = 1
  Me.tlpMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
  Me.tlpMain.Controls.Add(Me.fraException, 0, 1)
  Me.tlpMain.Controls.Add(Me.fraTrace, 0, 2)
  Me.tlpMain.Controls.Add(Me.splTop, 0, 0)
  Me.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill
  Me.tlpMain.Location = New System.Drawing.Point(0, 0)
  Me.tlpMain.Name = "tlpMain"
  Me.tlpMain.RowCount = 3
  Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
  Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
  Me.tlpMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
  Me.tlpMain.Size = New System.Drawing.Size(484, 284)
  Me.tlpMain.TabIndex = 0
  '
  'fraException
  '
  Me.fraException.Controls.Add(Me.txtException)
  Me.fraException.Dock = System.Windows.Forms.DockStyle.Fill
  Me.fraException.Location = New System.Drawing.Point(3, 83)
  Me.fraException.Name = "fraException"
  Me.fraException.Size = New System.Drawing.Size(478, 96)
  Me.fraException.TabIndex = 0
  Me.fraException.TabStop = False
  Me.fraException.Text = "Exception Details"
  '
  'txtException
  '
  Me.txtException.Dock = System.Windows.Forms.DockStyle.Fill
  Me.txtException.Location = New System.Drawing.Point(3, 16)
  Me.txtException.Multiline = True
  Me.txtException.Name = "txtException"
  Me.txtException.ReadOnly = True
  Me.txtException.Size = New System.Drawing.Size(472, 77)
  Me.txtException.TabIndex = 0
  '
  'fraTrace
  '
  Me.fraTrace.Controls.Add(Me.txtTrace)
  Me.fraTrace.Dock = System.Windows.Forms.DockStyle.Fill
  Me.fraTrace.Location = New System.Drawing.Point(3, 185)
  Me.fraTrace.Name = "fraTrace"
  Me.fraTrace.Size = New System.Drawing.Size(478, 96)
  Me.fraTrace.TabIndex = 1
  Me.fraTrace.TabStop = False
  Me.fraTrace.Text = "Trace Log"
  '
  'txtTrace
  '
  Me.txtTrace.Dock = System.Windows.Forms.DockStyle.Fill
  Me.txtTrace.Location = New System.Drawing.Point(3, 16)
  Me.txtTrace.Multiline = True
  Me.txtTrace.Name = "txtTrace"
  Me.txtTrace.ReadOnly = True
  Me.txtTrace.Size = New System.Drawing.Size(472, 77)
  Me.txtTrace.TabIndex = 1
  '
  'splTop
  '
  Me.splTop.Dock = System.Windows.Forms.DockStyle.Fill
  Me.splTop.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
  Me.splTop.IsSplitterFixed = True
  Me.splTop.Location = New System.Drawing.Point(3, 3)
  Me.splTop.Name = "splTop"
  '
  'splTop.Panel1
  '
  Me.splTop.Panel1.Controls.Add(Me.Label)
  '
  'splTop.Panel2
  '
  Me.splTop.Panel2.Controls.Add(Me.cmdContinue)
  Me.splTop.Panel2.Controls.Add(Me.cmdExit)
  Me.splTop.Panel2MinSize = 88
  Me.splTop.Size = New System.Drawing.Size(478, 74)
  Me.splTop.SplitterDistance = 386
  Me.splTop.TabIndex = 2
  '
  'Label
  '
  Me.Label.Dock = System.Windows.Forms.DockStyle.Fill
  Me.Label.Location = New System.Drawing.Point(0, 0)
  Me.Label.Name = "Label"
  Me.Label.Size = New System.Drawing.Size(386, 74)
  Me.Label.TabIndex = 0
  Me.Label.Text = "An unhandled exception has occured!"
  Me.Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
  '
  'cmdContinue
  '
  Me.cmdContinue.DialogResult = System.Windows.Forms.DialogResult.Ignore
  Me.cmdContinue.Location = New System.Drawing.Point(8, 40)
  Me.cmdContinue.Name = "cmdContinue"
  Me.cmdContinue.Size = New System.Drawing.Size(75, 23)
  Me.cmdContinue.TabIndex = 1
  Me.cmdContinue.Text = "Continue"
  Me.cmdContinue.UseVisualStyleBackColor = True
  '
  'cmdExit
  '
  Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Abort
  Me.cmdExit.Location = New System.Drawing.Point(8, 8)
  Me.cmdExit.Name = "cmdExit"
  Me.cmdExit.Size = New System.Drawing.Size(75, 23)
  Me.cmdExit.TabIndex = 0
  Me.cmdExit.Text = "Exit"
  Me.cmdExit.UseVisualStyleBackColor = True
  '
  'ExceptionDisplay
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = Global.CFHodEd.My.MySettings.Default.Form_ExceptionDisplay_ClientSize
  Me.ControlBox = False
  Me.Controls.Add(Me.tlpMain)
  Me.DataBindings.Add(New System.Windows.Forms.Binding("ClientSize", Global.CFHodEd.My.MySettings.Default, "Form_ExceptionDisplay_ClientSize", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.CFHodEd.My.MySettings.Default, "Form_ExceptionDisplay_Location", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
  Me.Location = Global.CFHodEd.My.MySettings.Default.Form_ExceptionDisplay_Location
  Me.MaximizeBox = False
  Me.MinimizeBox = False
  Me.MinimumSize = New System.Drawing.Size(500, 300)
  Me.Name = "ExceptionDisplay"
  Me.ShowIcon = False
  Me.ShowInTaskbar = False
  Me.Text = "An error has occured!"
  Me.tlpMain.ResumeLayout(False)
  Me.fraException.ResumeLayout(False)
  Me.fraException.PerformLayout()
  Me.fraTrace.ResumeLayout(False)
  Me.fraTrace.PerformLayout()
  Me.splTop.Panel1.ResumeLayout(False)
  Me.splTop.Panel2.ResumeLayout(False)
  Me.splTop.ResumeLayout(False)
  Me.ResumeLayout(False)

 End Sub
 Friend WithEvents tlpMain As System.Windows.Forms.TableLayoutPanel
 Friend WithEvents fraException As System.Windows.Forms.GroupBox
 Friend WithEvents txtException As System.Windows.Forms.TextBox
 Friend WithEvents fraTrace As System.Windows.Forms.GroupBox
 Friend WithEvents txtTrace As System.Windows.Forms.TextBox
 Friend WithEvents splTop As System.Windows.Forms.SplitContainer
 Friend WithEvents Label As System.Windows.Forms.Label
 Friend WithEvents cmdContinue As System.Windows.Forms.Button
 Friend WithEvents cmdExit As System.Windows.Forms.Button
End Class
