Imports Homeworld2.HOD
Imports Homeworld2.MAD

''' <summary>
''' Form to allow user to select and add a joint.
''' </summary>
Friend NotInheritable Class JointSelector
    ''' <summary>HOD.</summary>
    Private m_HOD As HOD

    ''' <summary>Animation.</summary>
    Private m_Animation As Animation

    ''' <summary>
    ''' Class contructor.
    ''' </summary>
    Public Sub New(ByVal HOD As HOD, ByVal Anim As Animation)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Set references.
        m_HOD = HOD
        m_Animation = Anim

        ' Add joints.
        _AddJoint(m_HOD.Root, tvwJoints.Nodes)

        ' Expand all.
        tvwJoints.Nodes(0).ExpandAll()

        ' Select it.
        tvwJoints.SelectedNode = tvwJoints.Nodes(0)
    End Sub

    ''' <summary>
    ''' Adds a joint and it's children to a tree view node collection,
    ''' recursively.
    ''' </summary>
    Private Sub _AddJoint(ByVal j As Joint, ByVal parent As Windows.Forms.TreeNodeCollection)
        ' Add the node and get parent.
        Dim node As TreeNode = parent.Add(j.Name)

        ' Set visible flag.
        node.Checked = j.Visible

        ' Add all children of this joint to the node we just got.
        For I As Integer = 0 To j.Children.Count - 1
            _AddJoint(j.Children(I), node.Nodes)

        Next I ' For I As Integer = 0 To j.Children.Count - 1
    End Sub

    ''' <summary>
    ''' Gets a joint from a tree node.
    ''' </summary>
    Private Function _GetJoint(ByVal j As TreeNode) As Joint
        Dim s As New Stack(Of TreeNode)
        Dim out As Joint = m_HOD.Root

        ' Push the path from current node to top.
        Do Until j Is Nothing
            s.Push(j)
            j = j.Parent

        Loop ' Do Until j Is Nothing

        ' Pop the root node.
        s.Pop()

        ' Get back to joint by popping existing nodes.
        Do Until s.Count = 0
            out = out.Children(s.Pop().Index)

        Loop ' Do Until s.Count = 0

        ' Return the joint.
        Return out
    End Function

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        ' Get the joint.
        Dim j As Joint = _GetJoint(tvwJoints.SelectedNode)

        ' See if it's already present.
        For I As Integer = 0 To m_Animation.Joints.Count - 1
            If m_Animation.Joints(I).Joint Is j Then _
                MsgBox("The selected joint is already present in the animation!" & vbCrLf &
                       "Please select another joint.", MsgBoxStyle.Information, Me.Text) _
                    : Exit Sub

        Next I ' For I As Integer = 0 To m_Animation.Joints.Count - 1

        ' Add the joint.
        m_Animation.Joints.Add(New AnimatedJoint With {.Joint = j})

        ' Close.
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class