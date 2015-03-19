<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class D3DSettings
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
  Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(D3DSettings))
  Me.Label1 = New System.Windows.Forms.Label
  Me.cboAdapter = New System.Windows.Forms.ComboBox
  Me.Label2 = New System.Windows.Forms.Label
  Me.cboDeviceType = New System.Windows.Forms.ComboBox
  Me.chkWindowed = New System.Windows.Forms.CheckBox
  Me.lstDisplayModes = New System.Windows.Forms.ListBox
  Me.Label3 = New System.Windows.Forms.Label
  Me.cmdOK = New System.Windows.Forms.Button
  Me.cmdCancel = New System.Windows.Forms.Button
  Me.cmdApply = New System.Windows.Forms.Button
  Me.cmdPreview = New System.Windows.Forms.Button
  Me.cmdAdapterInfo = New System.Windows.Forms.Button
  Me.SuspendLayout()
  '
  'Label1
  '
  Me.Label1.AutoSize = True
  Me.Label1.Location = New System.Drawing.Point(8, 8)
  Me.Label1.Name = "Label1"
  Me.Label1.Size = New System.Drawing.Size(44, 13)
  Me.Label1.TabIndex = 0
  Me.Label1.Text = "Adapter"
  '
  'cboAdapter
  '
  Me.cboAdapter.AllowDrop = True
  Me.cboAdapter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
  Me.cboAdapter.FormattingEnabled = True
  Me.cboAdapter.Location = New System.Drawing.Point(96, 8)
  Me.cboAdapter.Name = "cboAdapter"
  Me.cboAdapter.Size = New System.Drawing.Size(296, 21)
  Me.cboAdapter.TabIndex = 1
  '
  'Label2
  '
  Me.Label2.AccessibleRole = System.Windows.Forms.AccessibleRole.Indicator
  Me.Label2.AutoSize = True
  Me.Label2.Location = New System.Drawing.Point(8, 40)
  Me.Label2.Name = "Label2"
  Me.Label2.Size = New System.Drawing.Size(68, 13)
  Me.Label2.TabIndex = 2
  Me.Label2.Text = "Device Type"
  '
  'cboDeviceType
  '
  Me.cboDeviceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
  Me.cboDeviceType.FormattingEnabled = True
  Me.cboDeviceType.Items.AddRange(New Object() {"Hardware Abstraction Layer (HAL)", "Reference Rasterizer (REF)"})
  Me.cboDeviceType.Location = New System.Drawing.Point(96, 40)
  Me.cboDeviceType.Name = "cboDeviceType"
  Me.cboDeviceType.Size = New System.Drawing.Size(416, 21)
  Me.cboDeviceType.TabIndex = 3
  '
  'chkWindowed
  '
  Me.chkWindowed.AutoSize = True
  Me.chkWindowed.Checked = True
  Me.chkWindowed.CheckState = System.Windows.Forms.CheckState.Checked
  Me.chkWindowed.Location = New System.Drawing.Point(8, 72)
  Me.chkWindowed.Name = "chkWindowed"
  Me.chkWindowed.Size = New System.Drawing.Size(77, 17)
  Me.chkWindowed.TabIndex = 4
  Me.chkWindowed.Text = "Windowed"
  Me.chkWindowed.UseVisualStyleBackColor = True
  '
  'lstDisplayModes
  '
  Me.lstDisplayModes.FormattingEnabled = True
  Me.lstDisplayModes.Location = New System.Drawing.Point(8, 112)
  Me.lstDisplayModes.Name = "lstDisplayModes"
  Me.lstDisplayModes.Size = New System.Drawing.Size(504, 134)
  Me.lstDisplayModes.Sorted = True
  Me.lstDisplayModes.TabIndex = 5
  '
  'Label3
  '
  Me.Label3.AutoSize = True
  Me.Label3.Location = New System.Drawing.Point(8, 96)
  Me.Label3.Name = "Label3"
  Me.Label3.Size = New System.Drawing.Size(152, 13)
  Me.Label3.TabIndex = 6
  Me.Label3.Text = "List of available display modes:"
  '
  'cmdOK
  '
  Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
  Me.cmdOK.Location = New System.Drawing.Point(8, 252)
  Me.cmdOK.Name = "cmdOK"
  Me.cmdOK.Size = New System.Drawing.Size(120, 24)
  Me.cmdOK.TabIndex = 7
  Me.cmdOK.Text = "&OK"
  Me.cmdOK.UseVisualStyleBackColor = True
  '
  'cmdCancel
  '
  Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
  Me.cmdCancel.Location = New System.Drawing.Point(136, 252)
  Me.cmdCancel.Name = "cmdCancel"
  Me.cmdCancel.Size = New System.Drawing.Size(120, 24)
  Me.cmdCancel.TabIndex = 8
  Me.cmdCancel.Text = "C&ancel"
  Me.cmdCancel.UseVisualStyleBackColor = True
  '
  'cmdApply
  '
  Me.cmdApply.Location = New System.Drawing.Point(264, 252)
  Me.cmdApply.Name = "cmdApply"
  Me.cmdApply.Size = New System.Drawing.Size(120, 24)
  Me.cmdApply.TabIndex = 9
  Me.cmdApply.Text = "&Apply"
  Me.cmdApply.UseVisualStyleBackColor = True
  '
  'cmdPreview
  '
  Me.cmdPreview.Location = New System.Drawing.Point(392, 252)
  Me.cmdPreview.Name = "cmdPreview"
  Me.cmdPreview.Size = New System.Drawing.Size(120, 24)
  Me.cmdPreview.TabIndex = 10
  Me.cmdPreview.Text = "&Preview"
  Me.cmdPreview.UseVisualStyleBackColor = True
  '
  'cmdAdapterInfo
  '
  Me.cmdAdapterInfo.Location = New System.Drawing.Point(400, 8)
  Me.cmdAdapterInfo.Name = "cmdAdapterInfo"
  Me.cmdAdapterInfo.Size = New System.Drawing.Size(112, 24)
  Me.cmdAdapterInfo.TabIndex = 11
  Me.cmdAdapterInfo.Text = "Adapter Information"
  Me.cmdAdapterInfo.UseVisualStyleBackColor = True
  '
  'D3DSettings
  '
  Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
  Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
  Me.ClientSize = New System.Drawing.Size(520, 280)
  Me.Controls.Add(Me.cmdAdapterInfo)
  Me.Controls.Add(Me.cmdPreview)
  Me.Controls.Add(Me.cmdApply)
  Me.Controls.Add(Me.cmdCancel)
  Me.Controls.Add(Me.cmdOK)
  Me.Controls.Add(Me.Label3)
  Me.Controls.Add(Me.lstDisplayModes)
  Me.Controls.Add(Me.chkWindowed)
  Me.Controls.Add(Me.cboDeviceType)
  Me.Controls.Add(Me.Label2)
  Me.Controls.Add(Me.cboAdapter)
  Me.Controls.Add(Me.Label1)
  Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
  Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
  Me.MaximizeBox = False
  Me.MinimizeBox = False
  Me.Name = "D3DSettings"
  Me.ShowInTaskbar = False
  Me.Text = "Direct3D Settings"
  Me.ResumeLayout(False)
  Me.PerformLayout()

 End Sub
 Friend WithEvents Label1 As System.Windows.Forms.Label
 Friend WithEvents cboAdapter As System.Windows.Forms.ComboBox
 Friend WithEvents Label2 As System.Windows.Forms.Label
 Friend WithEvents cboDeviceType As System.Windows.Forms.ComboBox
 Friend WithEvents chkWindowed As System.Windows.Forms.CheckBox
 Friend WithEvents lstDisplayModes As System.Windows.Forms.ListBox
 Friend WithEvents Label3 As System.Windows.Forms.Label
 Friend WithEvents cmdOK As System.Windows.Forms.Button
 Friend WithEvents cmdCancel As System.Windows.Forms.Button
 Friend WithEvents cmdApply As System.Windows.Forms.Button
 Friend WithEvents cmdPreview As System.Windows.Forms.Button
 Friend WithEvents cmdAdapterInfo As System.Windows.Forms.Button
End Class
