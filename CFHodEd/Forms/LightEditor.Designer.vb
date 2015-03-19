<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LightEditor
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
  Me.components = New System.ComponentModel.Container
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LightEditor))
  Me.Label1 = New System.Windows.Forms.Label
  Me.Label2 = New System.Windows.Forms.Label
  Me.Label3 = New System.Windows.Forms.Label
  Me.txtAmbientRed = New System.Windows.Forms.TextBox
  Me.txtAmbientGreen = New System.Windows.Forms.TextBox
  Me.txtAmbientBlue = New System.Windows.Forms.TextBox
  Me.txtDiffuseBlue = New System.Windows.Forms.TextBox
  Me.txtDiffuseGreen = New System.Windows.Forms.TextBox
  Me.txtDiffuseRed = New System.Windows.Forms.TextBox
  Me.txtSpecularBlue = New System.Windows.Forms.TextBox
  Me.txtSpecularGreen = New System.Windows.Forms.TextBox
  Me.txtSpecularRed = New System.Windows.Forms.TextBox
  Me.Label4 = New System.Windows.Forms.Label
  Me.Label5 = New System.Windows.Forms.Label
  Me.Label6 = New System.Windows.Forms.Label
  Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
  Me.cmdExit = New System.Windows.Forms.Button
  CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
  Me.SuspendLayout()
  '
  'Label1
  '
  Me.Label1.Location = New System.Drawing.Point(8, 32)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(64, 20)
  Me.Label1.TabIndex = 0
  Me.Label1.Text = "Ambient"
  Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'Label2
  '
  Me.Label2.Location = New System.Drawing.Point(8, 56)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(64, 20)
  Me.Label2.TabIndex = 1
  Me.Label2.Text = "Diffuse"
  Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'Label3
  '
  Me.Label3.Location = New System.Drawing.Point(8, 80)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(64, 20)
  Me.Label3.TabIndex = 2
  Me.Label3.Text = "Specular"
  Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'txtAmbientRed
  '
  Me.txtAmbientRed.Location = New System.Drawing.Point(72, 32)
  Me.txtAmbientRed.Name = "txtAmbientRed"
  Me.txtAmbientRed.Size = New System.Drawing.Size(72, 20)
  Me.txtAmbientRed.TabIndex = 3
  '
  'txtAmbientGreen
  '
  Me.txtAmbientGreen.Location = New System.Drawing.Point(168, 32)
  Me.txtAmbientGreen.Name = "txtAmbientGreen"
  Me.txtAmbientGreen.Size = New System.Drawing.Size(72, 20)
  Me.txtAmbientGreen.TabIndex = 4
  '
  'txtAmbientBlue
  '
  Me.txtAmbientBlue.Location = New System.Drawing.Point(264, 32)
  Me.txtAmbientBlue.Name = "txtAmbientBlue"
  Me.txtAmbientBlue.Size = New System.Drawing.Size(72, 20)
  Me.txtAmbientBlue.TabIndex = 5
  '
  'txtDiffuseBlue
  '
  Me.txtDiffuseBlue.Location = New System.Drawing.Point(264, 56)
  Me.txtDiffuseBlue.Name = "txtDiffuseBlue"
  Me.txtDiffuseBlue.Size = New System.Drawing.Size(72, 20)
  Me.txtDiffuseBlue.TabIndex = 8
  '
  'txtDiffuseGreen
  '
  Me.txtDiffuseGreen.Location = New System.Drawing.Point(168, 56)
  Me.txtDiffuseGreen.Name = "txtDiffuseGreen"
  Me.txtDiffuseGreen.Size = New System.Drawing.Size(72, 20)
  Me.txtDiffuseGreen.TabIndex = 7
  '
  'txtDiffuseRed
  '
  Me.txtDiffuseRed.Location = New System.Drawing.Point(72, 56)
  Me.txtDiffuseRed.Name = "txtDiffuseRed"
  Me.txtDiffuseRed.Size = New System.Drawing.Size(72, 20)
  Me.txtDiffuseRed.TabIndex = 6
  '
  'txtSpecularBlue
  '
  Me.txtSpecularBlue.Location = New System.Drawing.Point(264, 80)
  Me.txtSpecularBlue.Name = "txtSpecularBlue"
  Me.txtSpecularBlue.Size = New System.Drawing.Size(72, 20)
  Me.txtSpecularBlue.TabIndex = 11
  '
  'txtSpecularGreen
  '
  Me.txtSpecularGreen.Location = New System.Drawing.Point(168, 80)
  Me.txtSpecularGreen.Name = "txtSpecularGreen"
  Me.txtSpecularGreen.Size = New System.Drawing.Size(72, 20)
  Me.txtSpecularGreen.TabIndex = 10
  '
  'txtSpecularRed
  '
  Me.txtSpecularRed.Location = New System.Drawing.Point(72, 80)
  Me.txtSpecularRed.Name = "txtSpecularRed"
  Me.txtSpecularRed.Size = New System.Drawing.Size(72, 20)
  Me.txtSpecularRed.TabIndex = 9
  '
  'Label4
  '
  Me.Label4.Location = New System.Drawing.Point(72, 8)
  Me.Label4.Name = "Label4"
  Me.Label4.Size = New System.Drawing.Size(72, 20)
  Me.Label4.TabIndex = 12
  Me.Label4.Text = "Red"
  Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
  '
  'Label5
  '
  Me.Label5.Location = New System.Drawing.Point(168, 8)
  Me.Label5.Name = "Label5"
  Me.Label5.Size = New System.Drawing.Size(72, 20)
  Me.Label5.TabIndex = 13
  Me.Label5.Text = "Green"
  Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
  '
  'Label6
  '
  Me.Label6.Location = New System.Drawing.Point(264, 8)
  Me.Label6.Name = "Label6"
  Me.Label6.Size = New System.Drawing.Size(72, 20)
  Me.Label6.TabIndex = 14
  Me.Label6.Text = "Blue"
  Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
  '
  'ErrorProvider
  '
  Me.ErrorProvider.ContainerControl = Me
  '
  'cmdExit
  '
  Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
  Me.cmdExit.Location = New System.Drawing.Point(280, 112)
  Me.cmdExit.Name = "cmdExit"
  Me.cmdExit.Size = New System.Drawing.Size(80, 24)
  Me.cmdExit.TabIndex = 15
  Me.cmdExit.Text = "Exit"
  Me.cmdExit.UseVisualStyleBackColor = True
  '
  'LightEditor
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.CancelButton = Me.cmdExit
  Me.ClientSize = New System.Drawing.Size(370, 144)
  Me.ControlBox = False
  Me.Controls.Add(Me.cmdExit)
  Me.Controls.Add(Me.Label6)
  Me.Controls.Add(Me.Label5)
  Me.Controls.Add(Me.Label4)
  Me.Controls.Add(Me.txtSpecularBlue)
  Me.Controls.Add(Me.txtSpecularGreen)
  Me.Controls.Add(Me.txtSpecularRed)
  Me.Controls.Add(Me.txtDiffuseBlue)
  Me.Controls.Add(Me.txtDiffuseGreen)
  Me.Controls.Add(Me.txtDiffuseRed)
  Me.Controls.Add(Me.txtAmbientBlue)
  Me.Controls.Add(Me.txtAmbientGreen)
  Me.Controls.Add(Me.txtAmbientRed)
  Me.Controls.Add(Me.Label3)
  Me.Controls.Add(Me.Label2)
  Me.Controls.Add(Me.Label1)
  Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.CFHodEd.My.MySettings.Default, "Form_LightEditor_Location", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
  Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
  Me.Location = Global.CFHodEd.My.MySettings.Default.Form_LightEditor_Location
  Me.MaximizeBox = False
  Me.MinimizeBox = False
  Me.Name = "LightEditor"
  Me.ShowIcon = False
  Me.ShowInTaskbar = False
  Me.Text = "Light Editor"
  CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
  Me.ResumeLayout(False)
  Me.PerformLayout()

 End Sub
 Friend WithEvents Label1 As System.Windows.Forms.Label
 Friend WithEvents Label2 As System.Windows.Forms.Label
 Friend WithEvents Label3 As System.Windows.Forms.Label
 Friend WithEvents txtAmbientRed As System.Windows.Forms.TextBox
 Friend WithEvents txtAmbientGreen As System.Windows.Forms.TextBox
 Friend WithEvents txtAmbientBlue As System.Windows.Forms.TextBox
 Friend WithEvents txtDiffuseBlue As System.Windows.Forms.TextBox
 Friend WithEvents txtDiffuseGreen As System.Windows.Forms.TextBox
 Friend WithEvents txtDiffuseRed As System.Windows.Forms.TextBox
 Friend WithEvents txtSpecularBlue As System.Windows.Forms.TextBox
 Friend WithEvents txtSpecularGreen As System.Windows.Forms.TextBox
 Friend WithEvents txtSpecularRed As System.Windows.Forms.TextBox
 Friend WithEvents Label4 As System.Windows.Forms.Label
 Friend WithEvents Label5 As System.Windows.Forms.Label
 Friend WithEvents Label6 As System.Windows.Forms.Label
 Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
 Friend WithEvents cmdExit As System.Windows.Forms.Button
End Class
