Public NotInheritable Class LightEditor
 ''' <summary>Light.</summary>
 Private m_Light As D3DHelper.Lights.BaseLight

 ''' <summary>
 ''' Class contructor.
 ''' </summary>
 Public Sub New(ByVal light As D3DHelper.Lights.BaseLight)
  ' This call is required by the Windows Form Designer.
  InitializeComponent()

  ' Set light.
  m_Light = light

 End Sub

 Private Sub LightEditor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
  txtAmbientRed.Text = CStr(m_Light.Ambient.Red)
  txtAmbientGreen.Text = CStr(m_Light.Ambient.Green)
  txtAmbientBlue.Text = CStr(m_Light.Ambient.Blue)

  txtDiffuseRed.Text = CStr(m_Light.Diffuse.Red)
  txtDiffuseGreen.Text = CStr(m_Light.Diffuse.Green)
  txtDiffuseBlue.Text = CStr(m_Light.Diffuse.Blue)

  txtSpecularRed.Text = CStr(m_Light.Specular.Red)
  txtSpecularGreen.Text = CStr(m_Light.Specular.Green)
  txtSpecularBlue.Text = CStr(m_Light.Specular.Blue)

 End Sub

 Private Sub txt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
 Handles txtAmbientRed.TextChanged, txtAmbientGreen.TextChanged, txtAmbientBlue.TextChanged, _
         txtDiffuseRed.TextChanged, txtDiffuseGreen.TextChanged, txtDiffuseBlue.TextChanged, _
         txtSpecularRed.TextChanged, txtSpecularGreen.TextChanged, txtSpecularBlue.TextChanged

  ' Get the text box.
  Dim TextBox As TextBox = CType(sender, TextBox)

  ' If the text box is not focused then do nothing.
  If Not TextBox.Focused Then _
   Exit Sub

  ' If data is not numeric, do nothing.
  If Not IsNumeric(TextBox.Text) Then _
   Exit Sub

  ' Get the number.
  Dim number As Single = CSng(TextBox.Text)

  ' Place to store the color value.
  Dim color As Microsoft.DirectX.Direct3D.ColorValue

  ' Set the required fields.
  ' Ambient Red
  If sender Is txtAmbientRed Then _
   color = m_Light.Ambient _
 : color.Red = number _
 : m_Light.Ambient = color

  ' Ambient Green
  If sender Is txtAmbientGreen Then _
   color = m_Light.Ambient _
 : color.Green = number _
 : m_Light.Ambient = color

  ' Ambient Blue
  If sender Is txtAmbientBlue Then _
   color = m_Light.Ambient _
 : color.Blue = number _
 : m_Light.Ambient = color

  ' Diffuse Red
  If sender Is txtDiffuseRed Then _
   color = m_Light.Diffuse _
 : color.Red = number _
 : m_Light.Diffuse = color

  ' Diffuse Green
  If sender Is txtDiffuseGreen Then _
   color = m_Light.Diffuse _
 : color.Green = number _
 : m_Light.Diffuse = color

  ' Diffuse Blue
  If sender Is txtDiffuseBlue Then _
   color = m_Light.Diffuse _
 : color.Blue = number _
 : m_Light.Diffuse = color

  ' Specular Red
  If sender Is txtSpecularRed Then _
   color = m_Light.Specular _
 : color.Red = number _
 : m_Light.Specular = color

  ' Specular Green
  If sender Is txtSpecularGreen Then _
   color = m_Light.Specular _
 : color.Green = number _
 : m_Light.Specular = color

  ' Specular Blue
  If sender Is txtSpecularBlue Then _
   color = m_Light.Specular _
 : color.Blue = number _
 : m_Light.Specular = color

 End Sub

 Private Sub txt_Validated(ByVal sender As Object, ByVal e As System.EventArgs) _
 Handles txtAmbientRed.Validated, txtAmbientGreen.Validated, txtAmbientBlue.Validated, _
         txtDiffuseRed.Validated, txtDiffuseGreen.Validated, txtDiffuseBlue.Validated, _
         txtSpecularRed.Validated, txtSpecularGreen.Validated, txtSpecularBlue.Validated

  ' Get the text box.
  Dim TextBox As TextBox = CType(sender, TextBox)

  ' Clear error.
  ErrorProvider.SetError(TextBox, "")

 End Sub

 Private Sub txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
 Handles txtAmbientRed.Validating, txtAmbientGreen.Validating, txtAmbientBlue.Validating, _
         txtDiffuseRed.Validating, txtDiffuseGreen.Validating, txtDiffuseBlue.Validating, _
         txtSpecularRed.Validating, txtSpecularGreen.Validating, txtSpecularBlue.Validating

  ' Get the text box.
  Dim TextBox As TextBox = CType(sender, TextBox)

  ' Assume the validation fails.
  e.Cancel = True

  ' If data is not entered, set error.
  If TextBox.Text = "" Then _
   ErrorProvider.SetError(TextBox, "Please enter a value.") _
 : Exit Sub

  ' If data is not numeric, set error.
  If Not IsNumeric(TextBox.Text) Then _
   ErrorProvider.SetError(TextBox, "Please enter a numeric value.") _
 : Exit Sub

  ' Validation succeeded.
  e.Cancel = False

 End Sub

 Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
  Me.Close()

 End Sub

End Class