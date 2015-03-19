<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HODType
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
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HODType))
  Me.Label1 = New System.Windows.Forms.Label
  Me.optHODTypeWireframe = New System.Windows.Forms.RadioButton
  Me.optHODTypeSimple = New System.Windows.Forms.RadioButton
  Me.optHODTypeBackground = New System.Windows.Forms.RadioButton
  Me.optHODTypeMulti = New System.Windows.Forms.RadioButton
  Me.cmdOK = New System.Windows.Forms.Button
  Me.SuspendLayout()
  '
  'Label1
  '
  Me.Label1.Location = New System.Drawing.Point(8, 8)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(296, 23)
  Me.Label1.TabIndex = 9
  Me.Label1.Text = "Select type of HOD:"
  Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'optHODTypeWireframe
  '
  Me.optHODTypeWireframe.Location = New System.Drawing.Point(8, 112)
  Me.optHODTypeWireframe.Name = "optHODTypeWireframe"
  Me.optHODTypeWireframe.Size = New System.Drawing.Size(296, 24)
  Me.optHODTypeWireframe.TabIndex = 8
  Me.optHODTypeWireframe.TabStop = True
  Me.optHODTypeWireframe.Text = "Wireframe UI Mesh"
  Me.optHODTypeWireframe.UseVisualStyleBackColor = True
  '
  'optHODTypeSimple
  '
  Me.optHODTypeSimple.Location = New System.Drawing.Point(8, 88)
  Me.optHODTypeSimple.Name = "optHODTypeSimple"
  Me.optHODTypeSimple.Size = New System.Drawing.Size(296, 24)
  Me.optHODTypeSimple.TabIndex = 7
  Me.optHODTypeSimple.TabStop = True
  Me.optHODTypeSimple.Text = "Solid UI Mesh"
  Me.optHODTypeSimple.UseVisualStyleBackColor = True
  '
  'optHODTypeBackground
  '
  Me.optHODTypeBackground.Location = New System.Drawing.Point(8, 64)
  Me.optHODTypeBackground.Name = "optHODTypeBackground"
  Me.optHODTypeBackground.Size = New System.Drawing.Size(296, 24)
  Me.optHODTypeBackground.TabIndex = 6
  Me.optHODTypeBackground.TabStop = True
  Me.optHODTypeBackground.Text = "Background Mesh"
  Me.optHODTypeBackground.UseVisualStyleBackColor = True
  '
  'optHODTypeMulti
  '
  Me.optHODTypeMulti.Location = New System.Drawing.Point(8, 40)
  Me.optHODTypeMulti.Name = "optHODTypeMulti"
  Me.optHODTypeMulti.Size = New System.Drawing.Size(296, 24)
  Me.optHODTypeMulti.TabIndex = 5
  Me.optHODTypeMulti.TabStop = True
  Me.optHODTypeMulti.Text = "Ship Mesh"
  Me.optHODTypeMulti.UseVisualStyleBackColor = True
  '
  'cmdOK
  '
  Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
  Me.cmdOK.Location = New System.Drawing.Point(216, 144)
  Me.cmdOK.Name = "cmdOK"
  Me.cmdOK.Size = New System.Drawing.Size(91, 23)
  Me.cmdOK.TabIndex = 10
  Me.cmdOK.Text = "&OK"
  Me.cmdOK.UseVisualStyleBackColor = True
  '
  'HODType
  '
  Me.AcceptButton = Me.cmdOK
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(314, 172)
  Me.ControlBox = False
  Me.Controls.Add(Me.cmdOK)
  Me.Controls.Add(Me.Label1)
  Me.Controls.Add(Me.optHODTypeWireframe)
  Me.Controls.Add(Me.optHODTypeSimple)
  Me.Controls.Add(Me.optHODTypeBackground)
  Me.Controls.Add(Me.optHODTypeMulti)
  Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.CFHodEd.My.MySettings.Default, "Form_HODType_Location", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
  Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
  Me.Location = Global.CFHodEd.My.MySettings.Default.Form_HODType_Location
  Me.MaximizeBox = False
  Me.MaximumSize = New System.Drawing.Size(320, 200)
  Me.MinimizeBox = False
  Me.MinimumSize = New System.Drawing.Size(320, 200)
  Me.Name = "HODType"
  Me.ShowIcon = False
  Me.ShowInTaskbar = False
  Me.Text = "Select HOD Type..."
  Me.ResumeLayout(False)

 End Sub
 Friend WithEvents Label1 As System.Windows.Forms.Label
 Friend WithEvents optHODTypeWireframe As System.Windows.Forms.RadioButton
 Friend WithEvents optHODTypeSimple As System.Windows.Forms.RadioButton
 Friend WithEvents optHODTypeBackground As System.Windows.Forms.RadioButton
 Friend WithEvents optHODTypeMulti As System.Windows.Forms.RadioButton
 Friend WithEvents cmdOK As System.Windows.Forms.Button
End Class
