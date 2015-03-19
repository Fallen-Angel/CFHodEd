<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JointTemplates
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
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(JointTemplates))
  Me.fraType = New System.Windows.Forms.GroupBox
  Me.optCaptureOrientation = New System.Windows.Forms.RadioButton
  Me.optResourcingOrientation = New System.Windows.Forms.RadioButton
  Me.optRepairPoint = New System.Windows.Forms.RadioButton
  Me.optCapturePoint = New System.Windows.Forms.RadioButton
  Me.optHardpoint = New System.Windows.Forms.RadioButton
  Me.optTurret = New System.Windows.Forms.RadioButton
  Me.optWeapon = New System.Windows.Forms.RadioButton
  Me.txtName = New System.Windows.Forms.TextBox
  Me.cmdOK = New System.Windows.Forms.Button
  Me.cmdCancel = New System.Windows.Forms.Button
  Me.Label = New System.Windows.Forms.Label
  Me.fraType.SuspendLayout()
  Me.SuspendLayout()
  '
  'fraType
  '
  Me.fraType.Controls.Add(Me.optCaptureOrientation)
  Me.fraType.Controls.Add(Me.optResourcingOrientation)
  Me.fraType.Controls.Add(Me.optRepairPoint)
  Me.fraType.Controls.Add(Me.optCapturePoint)
  Me.fraType.Controls.Add(Me.optHardpoint)
  Me.fraType.Controls.Add(Me.optTurret)
  Me.fraType.Controls.Add(Me.optWeapon)
  Me.fraType.Location = New System.Drawing.Point(8, 8)
  Me.fraType.Name = "fraType"
  Me.fraType.Size = New System.Drawing.Size(376, 200)
  Me.fraType.TabIndex = 0
  Me.fraType.TabStop = False
  Me.fraType.Text = "Type"
  '
  'optCaptureOrientation
  '
  Me.optCaptureOrientation.Location = New System.Drawing.Point(8, 168)
  Me.optCaptureOrientation.Name = "optCaptureOrientation"
  Me.optCaptureOrientation.Size = New System.Drawing.Size(360, 24)
  Me.optCaptureOrientation.TabIndex = 7
  Me.optCaptureOrientation.Text = "Capture orientation"
  Me.optCaptureOrientation.UseVisualStyleBackColor = True
  '
  'optResourcingOrientation
  '
  Me.optResourcingOrientation.Location = New System.Drawing.Point(8, 144)
  Me.optResourcingOrientation.Name = "optResourcingOrientation"
  Me.optResourcingOrientation.Size = New System.Drawing.Size(360, 24)
  Me.optResourcingOrientation.TabIndex = 5
  Me.optResourcingOrientation.Text = "Resourcing orientation"
  Me.optResourcingOrientation.UseVisualStyleBackColor = True
  '
  'optRepairPoint
  '
  Me.optRepairPoint.Location = New System.Drawing.Point(8, 120)
  Me.optRepairPoint.Name = "optRepairPoint"
  Me.optRepairPoint.Size = New System.Drawing.Size(360, 24)
  Me.optRepairPoint.TabIndex = 4
  Me.optRepairPoint.Text = "Repair point"
  Me.optRepairPoint.UseVisualStyleBackColor = True
  '
  'optCapturePoint
  '
  Me.optCapturePoint.Location = New System.Drawing.Point(8, 96)
  Me.optCapturePoint.Name = "optCapturePoint"
  Me.optCapturePoint.Size = New System.Drawing.Size(360, 24)
  Me.optCapturePoint.TabIndex = 3
  Me.optCapturePoint.Text = "Capture point"
  Me.optCapturePoint.UseVisualStyleBackColor = True
  '
  'optHardpoint
  '
  Me.optHardpoint.Location = New System.Drawing.Point(8, 72)
  Me.optHardpoint.Name = "optHardpoint"
  Me.optHardpoint.Size = New System.Drawing.Size(360, 24)
  Me.optHardpoint.TabIndex = 2
  Me.optHardpoint.Text = "Hardpoint"
  Me.optHardpoint.UseVisualStyleBackColor = True
  '
  'optTurret
  '
  Me.optTurret.Location = New System.Drawing.Point(8, 48)
  Me.optTurret.Name = "optTurret"
  Me.optTurret.Size = New System.Drawing.Size(360, 24)
  Me.optTurret.TabIndex = 1
  Me.optTurret.Text = "Turret"
  Me.optTurret.UseVisualStyleBackColor = True
  '
  'optWeapon
  '
  Me.optWeapon.Checked = True
  Me.optWeapon.Location = New System.Drawing.Point(8, 24)
  Me.optWeapon.Name = "optWeapon"
  Me.optWeapon.Size = New System.Drawing.Size(360, 24)
  Me.optWeapon.TabIndex = 0
  Me.optWeapon.TabStop = True
  Me.optWeapon.Text = "Weapon"
  Me.optWeapon.UseVisualStyleBackColor = True
  '
  'txtName
  '
  Me.txtName.Location = New System.Drawing.Point(88, 216)
  Me.txtName.Name = "txtName"
  Me.txtName.Size = New System.Drawing.Size(296, 20)
  Me.txtName.TabIndex = 2
  '
  'cmdOK
  '
  Me.cmdOK.Location = New System.Drawing.Point(216, 244)
  Me.cmdOK.Name = "cmdOK"
  Me.cmdOK.Size = New System.Drawing.Size(80, 24)
  Me.cmdOK.TabIndex = 3
  Me.cmdOK.Text = "OK"
  Me.cmdOK.UseVisualStyleBackColor = True
  '
  'cmdCancel
  '
  Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
  Me.cmdCancel.Location = New System.Drawing.Point(304, 244)
  Me.cmdCancel.Name = "cmdCancel"
  Me.cmdCancel.Size = New System.Drawing.Size(80, 24)
  Me.cmdCancel.TabIndex = 4
  Me.cmdCancel.Text = "Cancel"
  Me.cmdCancel.UseVisualStyleBackColor = True
  '
  'Label
  '
  Me.Label.Location = New System.Drawing.Point(8, 216)
  Me.Label.Name = "Label"
  Me.Label.Size = New System.Drawing.Size(72, 20)
  Me.Label.TabIndex = 5
  Me.Label.Text = "Name:"
  Me.Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
  '
  'JointTemplates
  '
  Me.AcceptButton = Me.cmdOK
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.CancelButton = Me.cmdCancel
  Me.ClientSize = New System.Drawing.Size(394, 272)
  Me.ControlBox = False
  Me.Controls.Add(Me.Label)
  Me.Controls.Add(Me.cmdCancel)
  Me.Controls.Add(Me.cmdOK)
  Me.Controls.Add(Me.txtName)
  Me.Controls.Add(Me.fraType)
  Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.CFHodEd.My.MySettings.Default, "Form_JointTemplates_Location", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
  Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
  Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
  Me.Location = Global.CFHodEd.My.MySettings.Default.Form_JointTemplates_Location
  Me.MaximizeBox = False
  Me.MinimizeBox = False
  Me.Name = "JointTemplates"
  Me.ShowIcon = False
  Me.ShowInTaskbar = False
  Me.Text = "Add Hardpoint"
  Me.fraType.ResumeLayout(False)
  Me.ResumeLayout(False)
  Me.PerformLayout()

 End Sub
 Friend WithEvents fraType As System.Windows.Forms.GroupBox
 Friend WithEvents optCaptureOrientation As System.Windows.Forms.RadioButton
 Friend WithEvents optResourcingOrientation As System.Windows.Forms.RadioButton
 Friend WithEvents optRepairPoint As System.Windows.Forms.RadioButton
 Friend WithEvents optCapturePoint As System.Windows.Forms.RadioButton
 Friend WithEvents optHardpoint As System.Windows.Forms.RadioButton
 Friend WithEvents optTurret As System.Windows.Forms.RadioButton
 Friend WithEvents optWeapon As System.Windows.Forms.RadioButton
 Friend WithEvents txtName As System.Windows.Forms.TextBox
 Friend WithEvents cmdOK As System.Windows.Forms.Button
 Friend WithEvents cmdCancel As System.Windows.Forms.Button
 Friend WithEvents Label As System.Windows.Forms.Label
End Class
