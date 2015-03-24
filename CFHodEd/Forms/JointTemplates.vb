Imports Microsoft.DirectX
Imports Homeworld2.HOD

''' <summary>
''' Form to allow user to insert pre-defined joint templates.
''' </summary>
Friend NotInheritable Class JointTemplates
    ''' <summary>HOD.</summary>
    Private m_HOD As HOD

    ''' <summary>Joint.</summary>
    Private m_Joint As Joint

    ''' <summary>
    ''' Class contructor.
    ''' </summary>
    Public Sub New(ByVal HOD As HOD, ByVal Joint As Joint)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Set the HOD and parent joint.
        m_HOD = HOD
        m_Joint = Joint
    End Sub

    Private Sub opt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles optWeapon.CheckedChanged, optTurret.CheckedChanged, optHardpoint.CheckedChanged,
                optCapturePoint.CheckedChanged, optRepairPoint.CheckedChanged, optResourcingOrientation.CheckedChanged,
                optCaptureOrientation.CheckedChanged

        ' Get the radio button.
        Dim radioButton As RadioButton = CType(sender, RadioButton)

        ' If it is unchecked, exit.
        If Not radioButton.Checked Then _
            Exit Sub

        ' Enable\Disable text box.
        txtName.ReadOnly = (radioButton Is optResourcingOrientation) OrElse
                           (radioButton Is optCaptureOrientation)

        ' If read-only, clear text.
        If txtName.ReadOnly Then _
            txtName.Text = ""
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Const Weapon As String = "Weapon_",
              Hardpoint As String = "Hardpoint_",
              Position As String = "_Position",
              Rest As String = "_Rest",
              Direction As String = "_Direction",
              Latitude As String = "_Latitude",
              Muzzle As String = "_Muzzle",
              CapturePoint As String = "CapturePoint",
              RepairPoint As String = "RepairPoint",
              Heading As String = "Heading",
              Up As String = "Up",
              Left As String = "Left",
              LatchOrientation As String = "LatchOrientation",
              SalvageOrientation As String = "SalvageOrientation"

        If (optWeapon.Checked OrElse optTurret.Checked OrElse optHardpoint.Checked OrElse
            optRepairPoint.Checked OrElse optCapturePoint.Checked) AndAlso
           ((txtName.Text Is Nothing) OrElse (txtName.Text = "")) Then _
            MsgBox("Please enter a base name for the joint!", MsgBoxStyle.Exclamation, Me.Text) _
                : Exit Sub

        If (optRepairPoint.Checked OrElse optCapturePoint.Checked) AndAlso
           (Not IsNumeric(txtName.Text)) Then _
            MsgBox("Please enter a numeric value!", MsgBoxStyle.Exclamation, Me.Text) _
                : Exit Sub

        ' Get name of joint.
        Dim name As String = txtName.Text

        ' See which radio button is selected.
        If optWeapon.Checked Then
            ' See if the joints can be added.
            If m_HOD.Root.Contains(Weapon & name & Position) OrElse
               m_HOD.Root.Contains(Weapon & name & Rest) OrElse
               m_HOD.Root.Contains(Weapon & name & Direction) Then _
                MsgBox("One of the joints that will be added already exists!" & vbCrLf &
                       "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
                    : Exit Sub

            ' Make individual joints.
            Dim j1 As New Joint With { _
                    .Name = Weapon & name & Position _
                    }

            Dim j2 As New Joint With { _
                    .Name = Weapon & name & Rest, _
                    .Position = New Vector3(0, 0, 10) _
                    }

            Dim j3 As New Joint With { _
                    .Name = Weapon & name & Direction, _
                    .Position = New Vector3(0, 10, 0) _
                    }

            ' Make hierarchy.
            j1.Children.Add(j2)
            j1.Children.Add(j3)

            ' Add to joint.
            m_Joint.Children.Add(j1)

        ElseIf optTurret.Checked Then
            ' See if the joints can be added.
            If m_HOD.Root.Contains(Weapon & name & Position) OrElse
               m_HOD.Root.Contains(Weapon & name & Rest) OrElse
               m_HOD.Root.Contains(Weapon & name & Direction) OrElse
               m_HOD.Root.Contains(Weapon & name & Latitude) OrElse
               m_HOD.Root.Contains(Weapon & name & Muzzle) Then _
                MsgBox("One of the joints that will be added already exists!" & vbCrLf &
                       "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
                    : Exit Sub

            ' Make individual joints.
            Dim j1 As New Joint With { _
                    .Name = Weapon & name & Position _
                    }

            Dim j2 As New Joint With { _
                    .Name = Weapon & name & Rest, _
                    .Position = New Vector3(0, 0, 10) _
                    }

            Dim j3 As New Joint With { _
                    .Name = Weapon & name & Direction, _
                    .Position = New Vector3(0, 10, 0) _
                    }

            Dim j4 As New Joint With { _
                    .Name = Weapon & name & Latitude, _
                    .Position = New Vector3(0, 5, 0) _
                    }

            Dim j5 As New Joint With { _
                    .Name = Weapon & name & Muzzle, _
                    .Position = New Vector3(0, 0, 10) _
                    }

            ' Make hierarchy.
            j4.Children.Add(j5)
            j1.Children.Add(j2)
            j1.Children.Add(j3)
            j1.Children.Add(j4)

            ' Add to joint.
            m_Joint.Children.Add(j1)

        ElseIf optHardpoint.Checked Then
            ' See if the joints can be added.
            If m_HOD.Root.Contains(Hardpoint & name & Position) OrElse
               m_HOD.Root.Contains(Hardpoint & name & Rest) OrElse
               m_HOD.Root.Contains(Hardpoint & name & Direction) Then _
                MsgBox("One of the joints that will be added already exists!" & vbCrLf &
                       "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
                    : Exit Sub

            ' Make individual joints.
            Dim j1 As New Joint With { _
                    .Name = Hardpoint & name & Position _
                    }

            Dim j2 As New Joint With { _
                    .Name = Hardpoint & name & Rest, _
                    .Position = New Vector3(0, 0, 10) _
                    }

            Dim j3 As New Joint With { _
                    .Name = Hardpoint & name & Direction, _
                    .Position = New Vector3(0, 10, 0) _
                    }

            ' Make hierarchy.
            j1.Children.Add(j2)
            j1.Children.Add(j3)

            ' Add to joint.
            m_Joint.Children.Add(j1)

        ElseIf optCapturePoint.Checked Then
            ' See if the joints can be added.
            If m_HOD.Root.Contains(CapturePoint & name) OrElse
               m_HOD.Root.Contains(CapturePoint & name & Heading) OrElse
               m_HOD.Root.Contains(CapturePoint & name & Up) OrElse
               m_HOD.Root.Contains(CapturePoint & name & Left) Then _
                MsgBox("One of the joints that will be added already exists!" & vbCrLf &
                       "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
                    : Exit Sub

            ' Make individual joints.
            Dim j1 As New Joint With { _
                    .Name = CapturePoint & name _
                    }

            Dim j2 As New Joint With { _
                    .Name = CapturePoint & name & Heading, _
                    .Position = New Vector3(0, 0, 10) _
                    }

            Dim j3 As New Joint With { _
                    .Name = CapturePoint & name & Up, _
                    .Position = New Vector3(0, 10, 0) _
                    }

            Dim j4 As New Joint With { _
                    .Name = CapturePoint & name & Left, _
                    .Position = New Vector3(10, 0, 0) _
                    }

            ' Make hierarchy.
            j1.Children.Add(j2)
            j1.Children.Add(j3)
            j1.Children.Add(j4)

            ' Add to joint.
            m_Joint.Children.Add(j1)

        ElseIf optRepairPoint.Checked Then
            ' See if the joints can be added.
            If m_HOD.Root.Contains(RepairPoint & name) OrElse
               m_HOD.Root.Contains(RepairPoint & name & Heading) OrElse
               m_HOD.Root.Contains(RepairPoint & name & Up) OrElse
               m_HOD.Root.Contains(RepairPoint & name & Left) Then _
                MsgBox("One of the joints that will be added already exists!" & vbCrLf &
                       "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
                    : Exit Sub

            ' Make individual joints.
            Dim j1 As New Joint With { _
                    .Name = RepairPoint & name _
                    }

            Dim j2 As New Joint With { _
                    .Name = RepairPoint & name & Heading, _
                    .Position = New Vector3(0, 0, 10) _
                    }

            Dim j3 As New Joint With { _
                    .Name = RepairPoint & name & Up, _
                    .Position = New Vector3(0, 10, 0) _
                    }

            Dim j4 As New Joint With { _
                    .Name = RepairPoint & name & Left, _
                    .Position = New Vector3(10, 0, 0) _
                    }

            ' Make hierarchy.
            j1.Children.Add(j2)
            j1.Children.Add(j3)
            j1.Children.Add(j4)

            ' Add to joint.
            m_Joint.Children.Add(j1)

        ElseIf optResourcingOrientation.Checked Then
            ' See if the joints can be added.
            If m_HOD.Root.Contains(LatchOrientation) OrElse
               m_HOD.Root.Contains(LatchOrientation & Heading) OrElse
               m_HOD.Root.Contains(LatchOrientation & Up) OrElse
               m_HOD.Root.Contains(LatchOrientation & Left) Then _
                MsgBox("One of the joints that will be added already exists!" & vbCrLf &
                       "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
                    : Exit Sub

            ' Make individual joints.
            Dim j1 As New Joint With { _
                    .Name = LatchOrientation _
                    }

            Dim j2 As New Joint With { _
                    .Name = LatchOrientation & Heading, _
                    .Position = New Vector3(0, 0, 10) _
                    }

            Dim j3 As New Joint With { _
                    .Name = LatchOrientation & Up, _
                    .Position = New Vector3(0, 10, 0) _
                    }

            Dim j4 As New Joint With { _
                    .Name = LatchOrientation & Left, _
                    .Position = New Vector3(10, 0, 0) _
                    }

            ' Make hierarchy.
            j1.Children.Add(j2)
            j1.Children.Add(j3)
            j1.Children.Add(j4)

            ' Add to joint.
            m_Joint.Children.Add(j1)

        ElseIf optCaptureOrientation.Checked Then
            ' See if the joints can be added.
            If m_HOD.Root.Contains(SalvageOrientation) OrElse
               m_HOD.Root.Contains(SalvageOrientation & Heading) OrElse
               m_HOD.Root.Contains(SalvageOrientation & Up) OrElse
               m_HOD.Root.Contains(SalvageOrientation & Left) Then _
                MsgBox("One of the joints that will be added already exists!" & vbCrLf &
                       "Please enter another name.", MsgBoxStyle.Exclamation, Me.Text) _
                    : Exit Sub

            ' Make individual joints.
            Dim j1 As New Joint With { _
                    .Name = SalvageOrientation _
                    }

            Dim j2 As New Joint With { _
                    .Name = SalvageOrientation & Heading, _
                    .Position = New Vector3(0, 0, 10) _
                    }

            Dim j3 As New Joint With { _
                    .Name = SalvageOrientation & Up, _
                    .Position = New Vector3(0, 10, 0) _
                    }

            Dim j4 As New Joint With { _
                    .Name = SalvageOrientation & Left, _
                    .Position = New Vector3(10, 0, 0) _
                    }

            ' Make hierarchy.
            j1.Children.Add(j2)
            j1.Children.Add(j3)
            j1.Children.Add(j4)

            ' Add to joint.
            m_Joint.Children.Add(j1)

        Else
            Trace.Assert(False, "Internal error.")

        End If

        ' Close form.
        DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub
End Class
