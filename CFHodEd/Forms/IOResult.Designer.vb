<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IOResult
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
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IOResult))
  Me.TextBox = New System.Windows.Forms.TextBox
  Me.SuspendLayout()
  '
  'TextBox
  '
  Me.TextBox.Dock = System.Windows.Forms.DockStyle.Fill
  Me.TextBox.Location = New System.Drawing.Point(0, 0)
  Me.TextBox.Multiline = True
  Me.TextBox.Name = "TextBox"
  Me.TextBox.ReadOnly = True
  Me.TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
  Me.TextBox.Size = New System.Drawing.Size(496, 222)
  Me.TextBox.TabIndex = 0
  '
  'IOResult
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = Global.CFHodEd.My.MySettings.Default.Form_IOResult_ClientSize
  Me.Controls.Add(Me.TextBox)
  Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.CFHodEd.My.MySettings.Default, "Form_IOResult_Location", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.DataBindings.Add(New System.Windows.Forms.Binding("ClientSize", Global.CFHodEd.My.MySettings.Default, "Form_IOResult_ClientSize", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
  Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
  Me.Location = Global.CFHodEd.My.MySettings.Default.Form_IOResult_Location
  Me.MaximizeBox = False
  Me.MinimizeBox = False
  Me.MinimumSize = New System.Drawing.Size(512, 256)
  Me.Name = "IOResult"
  Me.ShowIcon = False
  Me.Text = "I/O Result..."
  Me.ResumeLayout(False)
  Me.PerformLayout()

 End Sub
 Friend WithEvents TextBox As System.Windows.Forms.TextBox
End Class
