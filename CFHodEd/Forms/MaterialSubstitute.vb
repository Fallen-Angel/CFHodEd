Imports Homeworld2.HOD

''' <summary>
''' Form to replace materials in a basic mesh.
''' </summary>
Friend NotInheritable Class MaterialSubstitute
 ''' <summary>Part Index.</summary>
 Private m_PartIndex As Integer

 ''' <summary>Basic Mesh.</summary>
 Private m_BasicMesh As BasicMesh

 ''' <summary>HOD</summary>
 Private m_HOD As HOD

 ''' <summary>Format of form title.</summary>
 Private Const m_Title As String = "Select substitute for '%s' Material (part '%d')..."

 ''' <summary>
 ''' Class constructor.
 ''' </summary>
 Public Sub New(ByVal PartIndex As Integer, ByVal BasicMesh As BasicMesh, ByVal HOD As HOD, ByVal OldMaterialName As String)
  ' This call is required by the Windows Form Designer.
  InitializeComponent()

  ' Set title.
  Me.Text = m_Title.Replace("%s", OldMaterialName).Replace("%d", CStr(PartIndex))

  ' Set part index.
  m_PartIndex = PartIndex

  ' Set basic mesh.
  m_BasicMesh = BasicMesh

  ' Set HOD.
  m_HOD = HOD

  ' Add it's materials.
  For I As Integer = 0 To m_HOD.Materials.Count - 1
   ' Get the material name.
   Dim matName As String = m_HOD.Materials(I).ToString()

   ' Add to combo box.
   cboMaterial.Items.Add(matName)

   ' See if it's the one.
   If String.Compare(matName, OldMaterialName, True) = 0 Then _
    cboMaterial.SelectedIndex = I

  Next I ' For I As Integer = 0 To m_HOD.Materials.Count - 1

 End Sub

 Private Sub MaterialSubstitute_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
  cboMaterial.Width = Me.ClientSize.Width - 2 * cboMaterial.Left
  cmdOK.Left = (Me.ClientSize.Width - cmdOK.Width) \ 2

 End Sub

 Private Sub cboMaterial_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMaterial.SelectedIndexChanged
  Dim m As Material.Reference = m_BasicMesh.Material(m_PartIndex)
  m.Index = cboMaterial.SelectedIndex
  m_BasicMesh.Material(m_PartIndex) = m

 End Sub

 Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
  Me.Close()

 End Sub

End Class
