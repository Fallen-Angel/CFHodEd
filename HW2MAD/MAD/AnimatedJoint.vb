''' <summary>
''' Represents a Homeworld2 MAD file joint.
''' </summary>
Public Class AnimatedJoint
    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Joint.</summary>
    Private m_Joint As HOD.Joint

    ''' <summary>Default position.</summary>
    Private m_DefaultPosition As Vector3

    ''' <summary>Default rotation.</summary>
    Private m_DefaultRotation As Vector3

    ''' <summary>Default rotation.</summary>
    Private m_DefaultScale As Vector3

    ''' <summary>Channel animation curve.</summary>
    Private m_TranslateX As AnimationCurve

    ''' <summary>Channel animation curve.</summary>
    Private m_TranslateY As AnimationCurve

    ''' <summary>Channel animation curve.</summary>
    Private m_TranslateZ As AnimationCurve

    ''' <summary>Channel animation curve.</summary>
    Private m_RotateX As AnimationCurve

    ''' <summary>Channel animation curve.</summary>
    Private m_RotateY As AnimationCurve

    ''' <summary>Channel animation curve.</summary>
    Private m_RotateZ As AnimationCurve

    ''' <summary>Channel animation curve.</summary>
    Private m_ScaleX As AnimationCurve

    ''' <summary>Channel animation curve.</summary>
    Private m_ScaleY As AnimationCurve

    ''' <summary>Channel animation curve.</summary>
    Private m_ScaleZ As AnimationCurve

    ''' <summary>Animation curve indices.</summary>
    ''' <remarks>Only used as helper in read algorithm.</remarks>
    Private m_Indices As New List(Of Integer)

    ' ------------------------
    ' Constructors\Finalizers.
    ' ------------------------
    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    Public Sub New()
        ' Initialize.
        Initialize()
    End Sub

    ''' <summary>
    ''' Copy constructor.
    ''' </summary>
    Public Sub New(ByVal aj As AnimatedJoint)
        m_Joint = aj.m_Joint

        m_DefaultPosition = aj.m_DefaultPosition
        m_DefaultRotation = aj.m_DefaultRotation
        m_DefaultScale = aj.m_DefaultScale

        m_TranslateX = New AnimationCurve(aj.m_TranslateX)
        m_TranslateY = New AnimationCurve(aj.m_TranslateY)
        m_TranslateZ = New AnimationCurve(aj.m_TranslateZ)

        m_RotateX = New AnimationCurve(aj.m_RotateX)
        m_RotateY = New AnimationCurve(aj.m_RotateY)
        m_RotateZ = New AnimationCurve(aj.m_RotateZ)

        m_ScaleX = New AnimationCurve(aj.m_ScaleX)
        m_ScaleY = New AnimationCurve(aj.m_ScaleY)
        m_ScaleZ = New AnimationCurve(aj.m_ScaleZ)

        m_Indices.Clear()
    End Sub

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns\Sets the joint animated by this object.
    ''' </summary>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>value Is Nothing</c>.
    ''' </exception>
    ''' <remarks>
    ''' Setting the joint property erases existing joint
    ''' animation information. Also, whenever you change
    ''' the joint's default transform, remember to re-assign
    ''' the joint here.
    ''' </remarks>
    Public Property Joint() As HOD.Joint
        Get
            Return m_Joint
        End Get

        Set(ByVal value As HOD.Joint)
            Dim makeNewCurves As Boolean = True

            If value Is Nothing Then _
                Throw New ArgumentNullException("value") _
                    : Exit Property

            If value Is m_Joint Then _
                makeNewCurves = False

            ' Set the joint.
            m_Joint = value

            ' Set the default transform.
            m_DefaultPosition = value.Position
            m_DefaultRotation = value.Rotation
            m_DefaultScale = value.Scale

            ' Create new animation curves.
            If makeNewCurves Then
                m_TranslateX = New AnimationCurve With {.Name = value.Name & "_translateX"}
                m_TranslateY = New AnimationCurve With {.Name = value.Name & "_translateY"}
                m_TranslateZ = New AnimationCurve With {.Name = value.Name & "_translateZ"}

                m_RotateX = New AnimationCurve With {.Name = value.Name & "_rotateX"}
                m_RotateY = New AnimationCurve With {.Name = value.Name & "_rotateY"}
                m_RotateZ = New AnimationCurve With {.Name = value.Name & "_rotateZ"}

                m_ScaleX = New AnimationCurve With {.Name = value.Name & "_scaleX"}
                m_ScaleY = New AnimationCurve With {.Name = value.Name & "_scaleY"}
                m_ScaleZ = New AnimationCurve With {.Name = value.Name & "_scaleZ"}

            End If ' If makeNewCurves Then
        End Set
    End Property

    ''' <summary>
    ''' Returns the animation curve for this channel.
    ''' </summary>
    Friend ReadOnly Property TranslateX() As AnimationCurve
        Get
            Return m_TranslateX
        End Get
    End Property

    ''' <summary>
    ''' Returns the animation curve for this channel.
    ''' </summary>
    Friend ReadOnly Property TranslateY() As AnimationCurve
        Get
            Return m_TranslateY
        End Get
    End Property

    ''' <summary>
    ''' Returns the animation curve for this channel.
    ''' </summary>
    Friend ReadOnly Property TranslateZ() As AnimationCurve
        Get
            Return m_TranslateZ
        End Get
    End Property

    ''' <summary>
    ''' Returns the animation curve for this channel.
    ''' </summary>
    Friend ReadOnly Property RotateX() As AnimationCurve
        Get
            Return m_RotateX
        End Get
    End Property

    ''' <summary>
    ''' Returns the animation curve for this channel.
    ''' </summary>
    Friend ReadOnly Property RotateY() As AnimationCurve
        Get
            Return m_RotateY
        End Get
    End Property

    ''' <summary>
    ''' Returns the animation curve for this channel.
    ''' </summary>
    Friend ReadOnly Property RotateZ() As AnimationCurve
        Get
            Return m_RotateZ
        End Get
    End Property

    ''' <summary>
    ''' Returns the animation curve for this channel.
    ''' </summary>
    Friend ReadOnly Property ScaleX() As AnimationCurve
        Get
            Return m_ScaleX
        End Get
    End Property

    ''' <summary>
    ''' Returns the animation curve for this channel.
    ''' </summary>
    Friend ReadOnly Property ScaleY() As AnimationCurve
        Get
            Return m_ScaleY
        End Get
    End Property

    ''' <summary>
    ''' Returns the animation curve for this channel.
    ''' </summary>
    Friend ReadOnly Property ScaleZ() As AnimationCurve
        Get
            Return m_ScaleZ
        End Get
    End Property

    ''' <summary>
    ''' Returns the number of channels actually animated.
    ''' </summary>
    Friend ReadOnly Property ChannelCount() As Integer
        Get
            Dim count As Integer = 0

            If m_TranslateX.Keyframes.Count <> 0 Then _
                count += 1

            If m_TranslateY.Keyframes.Count <> 0 Then _
                count += 1

            If m_TranslateZ.Keyframes.Count <> 0 Then _
                count += 1

            If m_RotateX.Keyframes.Count <> 0 Then _
                count += 1

            If m_RotateY.Keyframes.Count <> 0 Then _
                count += 1

            If m_RotateZ.Keyframes.Count <> 0 Then _
                count += 1

            If m_ScaleX.Keyframes.Count <> 0 Then _
                count += 1

            If m_ScaleY.Keyframes.Count <> 0 Then _
                count += 1

            If m_ScaleZ.Keyframes.Count <> 0 Then _
                count += 1

            Return count
        End Get
    End Property

    ''' <summary>
    ''' Returns the number of keyframes for this joint.
    ''' </summary>
    Public ReadOnly Property KeyframeCount() As Integer
        Get
            Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateY.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateZ.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateX.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateY.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateZ.Keyframes.Count)

            Return m_TranslateX.Keyframes.Count
        End Get
    End Property

    ''' <summary>
    ''' Returns\Sets the time of the specified keyframe.
    ''' </summary>
    ''' <param name="Index">
    ''' Index of keyframe being modified.
    ''' </param>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>Index</c> is out of range, or when
    ''' <c>value</c> is negative.
    ''' </exception>
    Public Property KeyframeTime(ByVal Index As Integer) As Double
        Get
            Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateY.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateZ.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateX.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateY.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateZ.Keyframes.Count)

            If (Index < 0) OrElse (Index >= m_TranslateX.Keyframes.Count) Then _
                Throw New ArgumentOutOfRangeException("Index") _
                    : Exit Property

            Return m_TranslateX.Keyframes(Index).Time
        End Get

        Set(ByVal value As Double)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateY.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateZ.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateX.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateY.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateZ.Keyframes.Count)

            If (Index < 0) OrElse (Index >= m_TranslateX.Keyframes.Count) Then _
                Throw New ArgumentOutOfRangeException("Index") _
                    : Exit Property

            If value < 0 Then _
                Throw New ArgumentOutOfRangeException("value") _
                    : Exit Property

            m_TranslateX.Keyframes(Index) = New Keyframe _
                With {.Time = value, .Value = m_TranslateX.Keyframes(Index).Value}
            m_TranslateY.Keyframes(Index) = New Keyframe _
                With {.Time = value, .Value = m_TranslateY.Keyframes(Index).Value}
            m_TranslateZ.Keyframes(Index) = New Keyframe _
                With {.Time = value, .Value = m_TranslateZ.Keyframes(Index).Value}

            m_RotateX.Keyframes(Index) = New Keyframe With {.Time = value, .Value = m_RotateX.Keyframes(Index).Value}
            m_RotateY.Keyframes(Index) = New Keyframe With {.Time = value, .Value = m_RotateY.Keyframes(Index).Value}
            m_RotateZ.Keyframes(Index) = New Keyframe With {.Time = value, .Value = m_RotateZ.Keyframes(Index).Value}
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets position of the joint at the specified keyframe.
    ''' </summary>
    ''' <param name="Index">
    ''' Index of keyframe being modified.
    ''' </param>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>Index</c> is out of range.
    ''' </exception>
    Public Property KeyframePosition(ByVal index As Integer) As Vector3
        Get
            Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateY.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateZ.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateX.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateY.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateZ.Keyframes.Count)

            If (index < 0) OrElse (index >= m_TranslateX.Keyframes.Count) Then _
                Throw New ArgumentOutOfRangeException("Index") _
                    : Exit Property

            Return New Vector3(CSng(m_TranslateX.Keyframes(index).Value),
                               CSng(m_TranslateY.Keyframes(index).Value),
                               CSng(m_TranslateZ.Keyframes(index).Value))
        End Get

        Set(ByVal value As Vector3)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateY.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateZ.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateX.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateY.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateZ.Keyframes.Count)

            If (index < 0) OrElse (index >= m_TranslateX.Keyframes.Count) Then _
                Throw New ArgumentOutOfRangeException("Index") _
                    : Exit Property

            m_TranslateX.Keyframes(index) = New Keyframe _
                With {.Time = m_TranslateX.Keyframes(index).Time, .Value = value.X}
            m_TranslateY.Keyframes(index) = New Keyframe _
                With {.Time = m_TranslateY.Keyframes(index).Time, .Value = value.Y}
            m_TranslateZ.Keyframes(index) = New Keyframe _
                With {.Time = m_TranslateZ.Keyframes(index).Time, .Value = value.Z}
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets rotation of the joint at the specified keyframe.
    ''' </summary>
    ''' <param name="Index">
    ''' Index of keyframe being modified.
    ''' </param>
    ''' <exception cref="ArgumentOutOfRangeException">
    ''' Thrown when <c>Index</c> is out of range.
    ''' </exception>
    Public Property KeyframeRotation(ByVal index As Integer) As Vector3
        Get
            Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateY.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateZ.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateX.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateY.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateZ.Keyframes.Count)

            If (index < 0) OrElse (index >= m_TranslateX.Keyframes.Count) Then _
                Throw New ArgumentOutOfRangeException("Index") _
                    : Exit Property

            Return New Vector3(CSng(m_RotateX.Keyframes(index).Value),
                               CSng(m_RotateY.Keyframes(index).Value),
                               CSng(m_RotateZ.Keyframes(index).Value))
        End Get

        Set(ByVal value As Vector3)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateY.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateZ.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateX.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateY.Keyframes.Count)
            Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateZ.Keyframes.Count)

            If (index < 0) OrElse (index >= m_TranslateX.Keyframes.Count) Then _
                Throw New ArgumentOutOfRangeException("Index") _
                    : Exit Property

            m_RotateX.Keyframes(index) = New Keyframe With {.Time = m_RotateX.Keyframes(index).Time, .Value = value.X}
            m_RotateY.Keyframes(index) = New Keyframe With {.Time = m_RotateY.Keyframes(index).Time, .Value = value.Y}
            m_RotateZ.Keyframes(index) = New Keyframe With {.Time = m_RotateZ.Keyframes(index).Time, .Value = value.Z}
        End Set
    End Property

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Returns the name of this animated joint.
    ''' </summary>
    Public Overrides Function ToString() As String
        If m_Joint Is Nothing Then _
            Return "unknown" _
            Else _
            Return m_Joint.Name
    End Function

    ''' <summary>
    ''' Reads the animated joint from an IFF file.
    ''' </summary>
    Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader, ByVal NameFromSTRI As Func(Of Integer, String), ByVal HOD As HOD.HOD)
        ' Initialize.
        Initialize()

        ' Read the name.
        Dim name As String = NameFromSTRI(IFF.ReadInt32())

        ' Get the joint.
        Dim j As HOD.Joint = HOD.Root.GetJointByName(name)

        ' Set joint if possible.
        If j Is Nothing Then _
            Trace.TraceError("Joint '" & name & "' referenced in MAD, does not exist in HOD.") _
            Else _
            Joint = j

        ' Read indices.
        Dim indCount As Integer = IFF.ReadInt32()

        ' Read indices.
        For I As Integer = 1 To indCount
            m_Indices.Add(IFF.ReadInt32())

        Next I ' For I As Integer = 1 To indCount
    End Sub

    ''' <summary>
    ''' Writes the animated joint to an IFF writer.
    ''' </summary>
    Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter, ByRef striPosition As Integer, ByRef index As Integer)
        ' Write name.
        IFF.WriteInt32(striPosition)

        ' Increment reference.
        striPosition += Me.ToString().Length + 1

        ' Write channel count.
        IFF.WriteInt32(Me.ChannelCount)

        ' Write channel indices.
        If m_TranslateX.Keyframes.Count <> 0 Then _
            IFF.WriteInt32(index) _
                : index += 1

        If m_TranslateY.Keyframes.Count <> 0 Then _
            IFF.WriteInt32(index) _
                : index += 1

        If m_TranslateZ.Keyframes.Count <> 0 Then _
            IFF.WriteInt32(index) _
                : index += 1

        If m_RotateX.Keyframes.Count <> 0 Then _
            IFF.WriteInt32(index) _
                : index += 1

        If m_RotateY.Keyframes.Count <> 0 Then _
            IFF.WriteInt32(index) _
                : index += 1

        If m_RotateZ.Keyframes.Count <> 0 Then _
            IFF.WriteInt32(index) _
                : index += 1

        If m_ScaleX.Keyframes.Count <> 0 Then _
            IFF.WriteInt32(index) _
                : index += 1

        If m_ScaleY.Keyframes.Count <> 0 Then _
            IFF.WriteInt32(index) _
                : index += 1

        If m_ScaleZ.Keyframes.Count <> 0 Then _
            IFF.WriteInt32(index) _
                : index += 1
    End Sub

    ''' <summary>
    ''' Initializes the animated joint.
    ''' </summary>
    Private Sub Initialize()
        m_Joint = Nothing

        m_DefaultPosition = New Vector3(0, 0, 0)
        m_DefaultRotation = New Vector3(0, 0, 0)
        m_DefaultScale = New Vector3(1, 1, 1)

        m_TranslateX = New AnimationCurve With {.Name = ToString() & "_translateX"}
        m_TranslateY = New AnimationCurve With {.Name = ToString() & "_translateY"}
        m_TranslateZ = New AnimationCurve With {.Name = ToString() & "_translateZ"}

        m_RotateX = New AnimationCurve With {.Name = ToString() & "_rotateX"}
        m_RotateY = New AnimationCurve With {.Name = ToString() & "_rotateY"}
        m_RotateZ = New AnimationCurve With {.Name = ToString() & "_rotateZ"}

        m_ScaleX = New AnimationCurve With {.Name = ToString() & "_scaleX"}
        m_ScaleY = New AnimationCurve With {.Name = ToString() & "_scaleY"}
        m_ScaleZ = New AnimationCurve With {.Name = ToString() & "_scaleZ"}

        m_Indices.Clear()
    End Sub

    ''' <summary>
    ''' Resets the joint properties.
    ''' </summary>
    Friend Sub Reset()
        ' Check whether bound to a joint.
        If m_Joint Is Nothing Then _
            Exit Sub

        ' Set defaults.
        m_Joint.Position = m_DefaultPosition
        m_Joint.Rotation = m_DefaultRotation
        m_Joint.Scale = m_DefaultScale
    End Sub

    ''' <summary>
    ''' Updates the joint properties.
    ''' </summary>
    Friend Sub Update(ByVal time As Single)
        ' Check whether bound to a joint.
        If m_Joint Is Nothing Then _
            Exit Sub

        ' Set defaults.
        Dim position As Vector3 = m_DefaultPosition,
            rotation As Vector3 = m_DefaultRotation,
            scale As Vector3 = m_DefaultScale

        ' Animate each channel if present.
        ' Translate X
        If m_TranslateX.Keyframes.Count <> 0 Then _
            position.X = m_TranslateX.At(time)

        ' Translate Y
        If m_TranslateY.Keyframes.Count <> 0 Then _
            position.Y = m_TranslateY.At(time)

        ' Translate Z
        If m_TranslateZ.Keyframes.Count <> 0 Then _
            position.Z = m_TranslateZ.At(time)

        ' Rotate X
        If m_RotateX.Keyframes.Count <> 0 Then _
            rotation.X = m_RotateX.At(time)

        ' Rotate Y
        If m_RotateY.Keyframes.Count <> 0 Then _
            rotation.Y = m_RotateY.At(time)

        ' Rotate Z
        If m_RotateZ.Keyframes.Count <> 0 Then _
            rotation.Z = m_RotateZ.At(time)

        ' Scale X
        If m_ScaleX.Keyframes.Count <> 0 Then _
            scale.X = m_ScaleX.At(time)

        ' Scale Y
        If m_ScaleY.Keyframes.Count <> 0 Then _
            scale.Y = m_ScaleY.At(time)

        ' Scale Z
        If m_ScaleZ.Keyframes.Count <> 0 Then _
            scale.Z = m_ScaleZ.At(time)

        ' Set transform values.
        m_Joint.Position = position
        m_Joint.Rotation = rotation
        m_Joint.Scale = scale
    End Sub

    ''' <summary>
    ''' Updates references by copying keyframes.
    ''' </summary>
    Friend Sub UpdateReferences(ByVal animationCurves As IList(Of AnimationCurve))
        For I As Integer = 0 To m_Indices.Count - 1
            ' Get the indice.
            Dim ind As Integer = m_Indices(I)

            ' Check it.
            If (ind < 0) OrElse (ind >= animationCurves.Count) Then _
                Trace.TraceError("Joint refers to invalid animation curve.") _
                    : Continue For

            ' Get the animation curve.
            Dim anim As AnimationCurve = animationCurves(ind)

            ' Decide the destination animation curve.
            Dim dest As AnimationCurve

            Select Case anim.Channel
                Case AnimationCurve.AnimationChannel.TranslateX
                    dest = m_TranslateX

                Case AnimationCurve.AnimationChannel.TranslateY
                    dest = m_TranslateY

                Case AnimationCurve.AnimationChannel.TranslateZ
                    dest = m_TranslateZ

                Case AnimationCurve.AnimationChannel.RotateX
                    dest = m_RotateX

                Case AnimationCurve.AnimationChannel.RotateY
                    dest = m_RotateY

                Case AnimationCurve.AnimationChannel.RotateZ
                    dest = m_RotateZ

                Case AnimationCurve.AnimationChannel.ScaleX
                    dest = m_ScaleX

                Case AnimationCurve.AnimationChannel.ScaleY
                    dest = m_ScaleY

                Case AnimationCurve.AnimationChannel.ScaleZ
                    dest = m_ScaleZ

                Case Else
                    Trace.TraceError("Animation curve '" & anim.Name & "' refers to invalid channel.")
                    Continue For

            End Select ' Select Case anim.Channel

            ' Copy it's indices.
            dest.Keyframes.Clear()

            For J As Integer = 0 To anim.Keyframes.Count - 1
                dest.Keyframes.Add(New Keyframe(anim.Keyframes(J)))

            Next J ' For J As Integer = 0 To anim.Keyframes.Count - 1
        Next I ' For I As Integer = 0 To m_Indices.Count - 1

        ' Clear indices.
        m_Indices.Clear()

        ' Update keyframes.
        UpdateKeyframes()
    End Sub

    ''' <summary>
    ''' Updates all keyframes, i.e. adds missing keyframes so that
    ''' all animation curves have the same number of keyframes.
    ''' </summary>
    Private Sub UpdateKeyframes()
        Dim times As New List(Of Double)
        Dim curves As New List(Of AnimationCurve)

        ' Make a list of animation curves that will be affected.
        curves.Add(m_TranslateX)
        curves.Add(m_TranslateY)
        curves.Add(m_TranslateZ)

        curves.Add(m_RotateX)
        curves.Add(m_RotateY)
        curves.Add(m_RotateZ)

        If m_ScaleX.Keyframes.Count <> 0 Then _
            Debug.Assert(False) _
                : curves.Add(m_ScaleX)

        If m_ScaleY.Keyframes.Count <> 0 Then _
            Debug.Assert(False) _
                : curves.Add(m_ScaleY)

        If m_ScaleZ.Keyframes.Count <> 0 Then _
            Debug.Assert(False) _
                : curves.Add(m_ScaleZ)

        ' First clean keyframes.
        For I As Integer = 0 To curves.Count - 1
            For J As Integer = curves(I).Keyframes.Count - 1 To 0 Step - 1
                If times.Contains(curves(I).Keyframes(J).Time) Then _
                    curves(I).Keyframes.RemoveAt(J) _
                        : Trace.TraceWarning("Keyframe with duplicate times removed.") _
                    Else _
                    times.Add(curves(I).Keyframes(J).Time)

            Next J ' For J As Integer = curves(I).Keyframes.Count - 1 To 0 Step -1

            times.Clear()

        Next I ' For I As Integer = 0 To curves.Count - 1

        ' Now add times of all channels in list.
        For I As Integer = 0 To curves.Count - 1
            For J As Integer = 0 To curves(I).Keyframes.Count - 1
                Dim hasTime As Boolean = False

                ' See if this keyframe is present.
                For K As Integer = 0 To times.Count - 1
                    If times(K) = curves(I).Keyframes(J).Time Then _
                        hasTime = True _
                            : Exit For

                Next K ' For K As Integer = 0 To times.Count - 1

                If Not hasTime Then _
                    times.Add(curves(I).Keyframes(J).Time)

            Next J ' For J As Integer = 0 To curves(I).Keyframes.Count - 1
        Next I ' For I As Integer = 0 To curves.Count - 1

        ' Finally add missing keyframes for all channels in list.
        For I As Integer = 0 To curves.Count - 1
            For J As Integer = 0 To times.Count - 1
                Dim hasTime As Boolean = False

                ' See if this keyframe is present.
                For K As Integer = 0 To curves(I).Keyframes.Count - 1
                    If times(J) = curves(I).Keyframes(K).Time Then _
                        hasTime = True _
                            : Exit For

                Next K ' For K As Integer = 0 To curves(I).Keyframes.Count - 1

                ' If this keyframe is present, continue.
                If hasTime Then _
                    Continue For

                ' Decide the value that will be added.
                Dim value As Double = 0.0!

                If curves(I).Keyframes.Count <> 0 Then
                    value = curves(I).At(CSng(times(J)))

                Else ' If curves(I).Keyframes.Count = 0 Then
                    Select Case curves(I).Channel
                        Case AnimationCurve.AnimationChannel.TranslateX
                            value = m_DefaultPosition.X

                        Case AnimationCurve.AnimationChannel.TranslateY
                            value = m_DefaultPosition.Y

                        Case AnimationCurve.AnimationChannel.TranslateZ
                            value = m_DefaultPosition.Z

                        Case AnimationCurve.AnimationChannel.RotateX
                            value = m_DefaultRotation.X

                        Case AnimationCurve.AnimationChannel.RotateY
                            value = m_DefaultRotation.Y

                        Case AnimationCurve.AnimationChannel.RotateZ
                            value = m_DefaultRotation.Z

                        Case AnimationCurve.AnimationChannel.ScaleX
                            value = m_DefaultScale.X

                        Case AnimationCurve.AnimationChannel.ScaleY
                            value = m_DefaultScale.Y

                        Case AnimationCurve.AnimationChannel.ScaleZ
                            value = m_DefaultScale.Z

                    End Select ' Select Case curves(I).Channel
                End If ' If curves(I).Keyframes.Count = 0 Then

                ' Add keyframe.
                curves(I).Keyframes.Add(New Keyframe With {.Time = times(J), .Value = value})

            Next J ' For J As Integer = 0 To times.Count - 1

            Debug.Assert(curves(I).Keyframes.Count = times.Count)
        Next I ' For I As Integer = 0 To curves.Count - 1

        Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateY.Keyframes.Count)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateZ.Keyframes.Count)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateX.Keyframes.Count)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateY.Keyframes.Count)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateZ.Keyframes.Count)
    End Sub


    ''' <summary>
    ''' Adds a keyframe.
    ''' </summary>
    ''' <param name="Time">
    ''' Time.
    ''' </param>
    Public Sub AddKeyframe(ByVal Time As Double)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateY.Keyframes.Count)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateZ.Keyframes.Count)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateX.Keyframes.Count)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateY.Keyframes.Count)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateZ.Keyframes.Count)

        Dim position As Vector3 = m_DefaultPosition
        Dim rotation As Vector3 = m_DefaultRotation

        If m_TranslateX.Keyframes.Count <> 0 Then _
            position.X = m_TranslateX.At(csng(Time)) _
                : position.Y = m_TranslateY.At(csng(Time)) _
                : position.Z = m_TranslateZ.At(csng(Time)) _
                : rotation.X = m_RotateX.At(csng(Time)) _
                : rotation.Y = m_RotateY.At(csng(Time)) _
                : rotation.Z = m_RotateZ.At(csng(Time))

        m_TranslateX.Keyframes.Add(New Keyframe With {.Time = Time, .Value = Position.X})
        m_TranslateY.Keyframes.Add(New Keyframe With {.Time = Time, .Value = Position.Y})
        m_TranslateZ.Keyframes.Add(New Keyframe With {.Time = Time, .Value = Position.Z})

        m_RotateX.Keyframes.Add(New Keyframe With {.Time = Time, .Value = Rotation.X})
        m_RotateY.Keyframes.Add(New Keyframe With {.Time = Time, .Value = Rotation.Y})
        m_RotateZ.Keyframes.Add(New Keyframe With {.Time = Time, .Value = Rotation.Z})
    End Sub

    ''' <summary>
    ''' Adds a keyframe.
    ''' </summary>
    ''' <param name="Time">
    ''' Time.
    ''' </param>
    ''' <param name="Position">
    ''' Position.
    ''' </param>
    ''' <param name="Rotation">
    ''' Rotation.
    ''' </param>
    Public Sub AddKeyframe(ByVal Time As Double, ByVal Position As Vector3, ByVal Rotation As Vector3)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateY.Keyframes.Count)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateZ.Keyframes.Count)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateX.Keyframes.Count)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateY.Keyframes.Count)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateZ.Keyframes.Count)

        For I As Integer = 0 To m_TranslateX.Keyframes.Count - 1
            If Time.CompareTo(m_TranslateX.Keyframes(I).Time) = 0 Then _
                Throw New ArgumentException("Keyframe already present.") _
                    : Exit Sub

        Next I ' For I As Integer = 0 To m_TranslateX.Keyframes.Count - 1

        m_TranslateX.Keyframes.Add(New Keyframe With {.Time = Time, .Value = Position.X})
        m_TranslateY.Keyframes.Add(New Keyframe With {.Time = Time, .Value = Position.Y})
        m_TranslateZ.Keyframes.Add(New Keyframe With {.Time = Time, .Value = Position.Z})

        m_RotateX.Keyframes.Add(New Keyframe With {.Time = Time, .Value = Rotation.X})
        m_RotateY.Keyframes.Add(New Keyframe With {.Time = Time, .Value = Rotation.Y})
        m_RotateZ.Keyframes.Add(New Keyframe With {.Time = Time, .Value = Rotation.Z})
    End Sub

    ''' <summary>
    ''' Removes a keyframe.
    ''' </summary>
    ''' <param name="Index">
    ''' Index of keyframe to remove.
    ''' </param>
    Public Sub RemoveKeyframe(ByVal Index As Integer)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateY.Keyframes.Count)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_TranslateZ.Keyframes.Count)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateX.Keyframes.Count)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateY.Keyframes.Count)
        Debug.Assert(m_TranslateX.Keyframes.Count = m_RotateZ.Keyframes.Count)

        If (Index < 0) OrElse (Index >= m_TranslateX.Keyframes.Count) Then _
            Throw New ArgumentOutOfRangeException("Index") _
                : Exit Sub

        m_TranslateX.Keyframes.RemoveAt(Index)
        m_TranslateY.Keyframes.RemoveAt(Index)
        m_TranslateZ.Keyframes.RemoveAt(Index)

        m_RotateX.Keyframes.RemoveAt(Index)
        m_RotateY.Keyframes.RemoveAt(Index)
        m_RotateZ.Keyframes.RemoveAt(Index)
    End Sub
End Class
