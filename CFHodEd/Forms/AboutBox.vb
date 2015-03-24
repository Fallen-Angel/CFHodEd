''' <summary>
''' About CFHE form.
''' </summary>
Friend NotInheritable Class AboutBox
    Private Sub AboutBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LabelVersion.Text = "Version " & My.Application.Info.Version.ToString()
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub
End Class
