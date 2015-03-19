<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MaterialSubstitute
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
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MaterialSubstitute))
  Me.cboMaterial = New System.Windows.Forms.ComboBox
  Me.cmdOK = New System.Windows.Forms.Button
  Me.SuspendLayout()
  '
  'cboMaterial
  '
  Me.cboMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
  Me.cboMaterial.FormattingEnabled = True
  Me.cboMaterial.Location = New System.Drawing.Point(8, 8)
  Me.cboMaterial.Name = "cboMaterial"
  Me.cboMaterial.Size = New System.Drawing.Size(368, 21)
  Me.cboMaterial.TabIndex = 0
  '
  'cmdOK
  '
  Me.cmdOK.Location = New System.Drawing.Point(152, 32)
  Me.cmdOK.Name = "cmdOK"
  Me.cmdOK.Size = New System.Drawing.Size(80, 24)
  Me.cmdOK.TabIndex = 1
  Me.cmdOK.Text = "OK"
  Me.cmdOK.UseVisualStyleBackColor = True
  '
  'MaterialSubstitute
  '
  Me.AcceptButton = Me.cmdOK
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = Global.CFHodEd.My.MySettings.Default.Form_MaterialSubstitute_ClientSize
  Me.ControlBox = False
  Me.Controls.Add(Me.cmdOK)
  Me.Controls.Add(Me.cboMaterial)
  Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.CFHodEd.My.MySettings.Default, "Form_MaterialSubstitute_Location", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.DataBindings.Add(New System.Windows.Forms.Binding("ClientSize", Global.CFHodEd.My.MySettings.Default, "Form_MaterialSubstitute_ClientSize", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
  Me.Location = Global.CFHodEd.My.MySettings.Default.Form_MaterialSubstitute_Location
  Me.MaximizeBox = False
  Me.MaximumSize = New System.Drawing.Size(4000, 100)
  Me.MinimizeBox = False
  Me.MinimumSize = New System.Drawing.Size(400, 100)
  Me.Name = "MaterialSubstitute"
  Me.ShowIcon = False
  Me.ShowInTaskbar = False
  Me.Tag = ""
  Me.Text = "MaterialSubstitute"
  Me.ResumeLayout(False)

 End Sub
 Friend WithEvents cboMaterial As System.Windows.Forms.ComboBox
 Friend WithEvents cmdOK As System.Windows.Forms.Button
End Class
