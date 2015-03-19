''' <summary>
''' Form containing code for displaying adapter information to end-user.
''' </summary>
Friend NotInheritable Class D3DAdapterInfo
 Private m_Adapter As Integer

 ''' <summary>
 ''' Displays the adapter information.
 ''' </summary>
 Private Sub DisplayAdapterInfo()
  With Manager.Adapters(m_Adapter).GetWhqlInformation()
   Label11.Text = .DeviceIdentifier.ToString()
   Label12.Text = .DeviceName.ToString()
   Label13.Text = .Description
   Label14.Text = .DriverName
   Label15.Text = .DriverVersion.ToString()
   Label16.Text = .WhqlLevel.ToString()
   Label17.Text = Hex(.VendorId)
   Label18.Text = Hex(.DeviceId)
   Label19.Text = Hex(.SubSystemId)
   Label20.Text = Hex(.Revision)
  End With ' With Manager.Adapters(m_Adapter).GetWhqlInformation()

 End Sub

 ''' <summary>
 ''' Returns\Sets the adapter whose information is to be displayed.
 ''' </summary>
 Public Property Adapter() As Integer
  Get
   Return m_Adapter

  End Get

  Set(ByVal value As Integer)
   If (value < 0) OrElse (value >= Manager.Adapters.Count) Then _
    Throw New ArgumentException("Invalid 'value'.") _
  : Exit Property

   ' Set the adapter.
   m_Adapter = value

   ' Refresh the information.
   DisplayAdapterInfo()

  End Set

 End Property

 Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
  Me.DialogResult = DialogResult.OK
  Me.Close()

 End Sub

End Class
