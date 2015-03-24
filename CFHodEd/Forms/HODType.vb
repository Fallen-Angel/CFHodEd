Imports Homeworld2.HOD

''' <summary>
''' Form to edit HOD type.
''' </summary>
Friend NotInheritable Class HODType
    ''' <summary>HOD whose type is being set.</summary>
    Friend m_HOD As HOD

    ''' <summary>
    ''' Class contructor.
    ''' </summary>
    Public Sub New(ByVal HOD As HOD)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Set reference.
        m_HOD = HOD

        ' Set option button.
        If m_HOD.Version = 1000 Then _
            optHODTypeBackground.Checked = True _
                : Exit Sub

        If m_HOD.Name = HOD.Name_SimpleMesh Then _
            optHODTypeSimple.Checked = True _
                : Exit Sub

        If m_HOD.Name = HOD.Name_WireframeMesh Then _
            optHODTypeWireframe.Checked = True _
                : Exit Sub

        If m_HOD.Name = HOD.Name_MultiMesh Then _
            optHODTypeMulti.Checked = True _
                : Exit Sub
    End Sub

    Private Sub optHODType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles optHODTypeMulti.CheckedChanged, optHODTypeBackground.CheckedChanged, optHODTypeSimple.CheckedChanged,
                optHODTypeWireframe.CheckedChanged
        Dim RadioButton As RadioButton = CType(sender, RadioButton)

        If Not RadioButton.Checked Then _
            Exit Sub

        If sender Is optHODTypeMulti Then _
            m_HOD.Version = &H200 _
                : m_HOD.Name = HOD.Name_MultiMesh

        If sender Is optHODTypeBackground Then _
            m_HOD.Version = 1000 _
                : m_HOD.Name = ""

        If sender Is optHODTypeSimple Then _
            m_HOD.Version = &H200 _
                : m_HOD.Name = HOD.Name_SimpleMesh

        If sender Is optHODTypeWireframe Then _
            m_HOD.Version = &H200 _
                : m_HOD.Name = HOD.Name_WireframeMesh
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.Close()
    End Sub
End Class
