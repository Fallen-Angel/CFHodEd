''' <summary>
''' Class representing a Homeworld2 animation curve.
''' </summary>
Friend NotInheritable Class AnimationCurve
 ''' <summary>
 ''' Lists the different animation channels.
 ''' </summary>
 Friend Enum AnimationChannel
  TranslateX
  TranslateY
  TranslateZ

  RotateX
  RotateY
  RotateZ

  ScaleX ' may not be actually supported by HW2...
  ScaleY ' may not be actually supported by HW2...
  ScaleZ ' may not be actually supported by HW2...

  Invalid = -1

 End Enum

 ' --------------
 ' Class Members.
 ' --------------
 ''' <summary>Name.</summary>
 Private m_Name As String

 ''' <summary>List of keyframes.</summary>
 Private WithEvents m_Keyframes As New EventList(Of Keyframe)

 ''' <summary>Pre infinity.</summary>
 Private m_PreInfinity As InfinityType

 ''' <summary>Post infinity.</summary>
 Private m_PostInfinity As InfinityType

 ' ------------------------
 ' Constructors\Finalizers.
 ' ------------------------
 ''' <summary>
 ''' Class contructor.
 ''' </summary>
 Public Sub New()
  Initialize()

 End Sub

 ''' <summary>
 ''' Copy contructor.
 ''' </summary>
 Public Sub New(ByVal ac As AnimationCurve)
  m_Name = ac.m_Name

  For I As Integer = 0 To ac.m_Keyframes.Count - 1
   m_Keyframes.Add(New Keyframe(ac.m_Keyframes(I)))

  Next I ' For I As Integer = 0 To ac.m_Keyframes.Count - 1

  m_PreInfinity = ac.m_PreInfinity
  m_PostInfinity = ac.m_PostInfinity

 End Sub

 ' -----------------
 ' Class properties.
 ' -----------------
 ''' <summary>
 ''' Returns\Sets the name of this animation curve.
 ''' </summary>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when <c>value Is Nothing</c>.
 ''' </exception>
 Public Property Name() As String
  Get
   Return m_Name

  End Get

  Friend Set(ByVal value As String)
   If (value Is Nothing) OrElse (value = "") Then _
    Throw New ArgumentNullException("value") _
  : Exit Property

   m_Name = value

  End Set

 End Property

 ''' <summary>
 ''' Returns the list of keyframes in this animation curve.
 ''' </summary>
 Public ReadOnly Property Keyframes() As IList(Of Keyframe)
  Get
   Return m_Keyframes

  End Get

 End Property

 ''' <summary>
 ''' Returns\Sets the pre infinity (behaviour before the first keyframe).
 ''' </summary>
 Public Property PreInfinty() As InfinityType
  Get
   Return m_PreInfinity

  End Get

  Set(ByVal value As InfinityType)
   If IsNumeric(value.ToString()) Then _
    Throw New ArgumentException("Invalid 'value'.") _
  : Exit Property

   m_PreInfinity = value

  End Set

 End Property

 ''' <summary>
 ''' Returns\Sets the post infinity (behaviour after the last keyframe).
 ''' </summary>
 Public Property PostInfinity() As InfinityType
  Get
   Return m_PostInfinity

  End Get

  Set(ByVal value As InfinityType)
   If IsNumeric(value.ToString()) Then _
    Throw New ArgumentException("Invalid 'value'.") _
  : Exit Property

   m_PostInfinity = value

  End Set

 End Property

 ''' <summary>
 ''' Returns the channel animated by this animation curve.
 ''' </summary>
 Friend ReadOnly Property Channel() As AnimationChannel
  Get
   ' Check if no name has been set, or if it doesn't have a "_".
   If (m_Name = "") OrElse (InStr(m_Name, "_") = 0) Then _
    Debug.Assert(False) _
  : Return AnimationChannel.Invalid

   ' See if the last "_" is the last character itself.
   If m_Name(m_Name.Length - 1) = "_" Then _
    Debug.Assert(False) _
  : Return AnimationChannel.Invalid

   ' Get the string after the last "_".
   Dim lastPart As String = m_Name.Substring(m_Name.LastIndexOf("_") + 1)

   ' Return index.
   ' Translate X
   If String.Compare(lastPart, "translateX") = 0 Then _
    Return AnimationChannel.TranslateX

   ' Translate Y
   If String.Compare(lastPart, "translateY") = 0 Then _
    Return AnimationChannel.TranslateY

   ' Translate Z
   If String.Compare(lastPart, "translateZ") = 0 Then _
    Return AnimationChannel.TranslateZ

   ' Rotate X
   If String.Compare(lastPart, "rotateX") = 0 Then _
    Return AnimationChannel.RotateX

   ' Rotate Y
   If String.Compare(lastPart, "rotateY") = 0 Then _
    Return AnimationChannel.RotateY

   ' Rotate Z
   If String.Compare(lastPart, "rotateZ") = 0 Then _
    Return AnimationChannel.RotateZ

   ' Scale X
   If String.Compare(lastPart, "scaleX") = 0 Then _
    Return AnimationChannel.ScaleX

   ' Scale Y
   If String.Compare(lastPart, "scaleY") = 0 Then _
    Return AnimationChannel.ScaleY

   ' Scale Z
   If String.Compare(lastPart, "scaleZ") = 0 Then _
    Return AnimationChannel.ScaleZ

   ' Unknown channel.
   Debug.Assert(False)
   Return AnimationChannel.Invalid

  End Get

 End Property

 ''' <summary>
 ''' Returns the value at specified by interpolation.
 ''' </summary>
 ''' <remarks>
 ''' This does not take tangents into account and performs linear
 ''' interpolation instead. Also, pre-infinity and post-infinity
 ''' are ignored, and assumed to be constant.
 ''' </remarks>
 Friend ReadOnly Property At(ByVal time As Single) As Single
  Get
   ' No keyframes, no animation.
   If m_Keyframes.Count = 0 Then _
    Debug.Assert(False) _
  : Return 0

   ' Before time, return constant value.
   If time <= m_Keyframes(0).Time Then _
    Return CSng(m_Keyframes(0).Value)

   ' After time, return constant value.
   If time >= m_Keyframes(m_Keyframes.Count - 1).Time Then _
    Return CSng(m_Keyframes(m_Keyframes.Count - 1).Value)

   ' Interpolate.
   For I As Integer = 0 To m_Keyframes.Count - 2
    ' Skip to the correct keyframe.
    If (time <= m_Keyframes(I).Time) OrElse (time > m_Keyframes(I + 1).Time) Then _
     Continue For

    ' Calculate interpolation factor.
    Dim k As Single = CSng((time - m_Keyframes(I).Time) / _
                           (m_Keyframes(I + 1).Time - m_Keyframes(I).Time))

    ' Finally, linearly interpolate and return value.
    Return CSng(m_Keyframes(I).Value + k * (m_Keyframes(I + 1).Value - m_Keyframes(I).Value))

   Next I ' For I As Integer = 0 To m_Keyframes.Count - 2

  End Get

 End Property

 ' -----------------
 ' Member Functions.
 ' -----------------
 ''' <summary>
 ''' Returns the name of this animation curve.
 ''' </summary>
 Public Overrides Function ToString() As String
  Return m_Name

 End Function

 ''' <summary>
 ''' Reads the animation curve from an IFF file.
 ''' </summary>
 Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader, ByVal NameFromSTRI As Func(Of Integer, String))
  ' Initialize.
  Initialize()

  ' Read name of animation curve.
  m_Name = NameFromSTRI(IFF.ReadInt32())

  ' Read keyframe count.
  Dim keyframeCount As Integer = IFF.ReadInt32()

  ' Read all keyframes.
  For I As Integer = 1 To keyframeCount
   Dim k As New Keyframe
   k.ReadIFF(IFF)
   m_Keyframes.Add(k)

  Next I ' For I As Integer = 1 To keyframeCount

  ' Read pre infinity and post infinity.
  m_PreInfinity = CType(IFF.ReadInt32(), InfinityType)
  m_PostInfinity = CType(IFF.ReadInt32(), InfinityType)

 End Sub

 ''' <summary>
 ''' Writes the animation curve to an IFF writer.
 ''' </summary>
 Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter, ByRef striPosition As Integer)
  ' First calculate tangents.
  CalculateTangents()

  ' Write animation curve name.
  IFF.Write(striPosition)

  ' Update position.
  striPosition += Me.ToString.Length + 1

  ' Write keyframe count.
  IFF.WriteInt32(m_Keyframes.Count)

  ' Write the keyframes.
  For I As Integer = 0 To m_Keyframes.Count - 1
   m_Keyframes(I).WriteIFF(IFF)

  Next I ' For I As Integer = 0 To m_Keyframes.Count - 1

  ' Write pre infinity and post infinity.
  IFF.WriteInt32(m_PreInfinity)
  IFF.WriteInt32(m_PostInfinity)

 End Sub

 ''' <summary>
 ''' Calculates the in tangents and out tangents for each keyframe for
 ''' a linear interpolation.
 ''' </summary>
 Friend Sub CalculateTangents()
  ' See if we have any keyframes.
  If m_Keyframes.Count = 0 Then _
   Exit Sub

  ' Set the first keyframe's in tangent.
  m_Keyframes(0) = m_Keyframes(0).SetInTangent(New Vector2(1, 0))

  For I As Integer = 0 To m_Keyframes.Count - 2
   ' Get the difference between time and value of next keyframe and this keyframe.
   Dim dv As New Vector2(CSng(m_Keyframes(I + 1).Time - m_Keyframes(I).Time), _
                         CSng(m_Keyframes(I + 1).Value - m_Keyframes(I).Value))

   ' Normalize.
   dv.Normalize()

   ' Set this key frame's out tangent and next keyframe's in tangent.
   m_Keyframes(I) = m_Keyframes(I).SetOutTangent(dv)
   m_Keyframes(I + 1) = m_Keyframes(I + 1).SetInTangent(dv)

  Next I ' For I As Integer = 0 To m_Keyframes.Count - 1

  ' Set the last keyframe's out tangent.
  m_Keyframes(m_Keyframes.Count - 1) = m_Keyframes(m_Keyframes.Count - 1).SetOutTangent(New Vector2(1, 0))

 End Sub

 ''' <summary>
 ''' Initializes the animation curve.
 ''' </summary>
 Private Sub Initialize()
  m_Name = "AnimationCurve"
  m_Keyframes.Clear()

 End Sub

 ''' <summary>
 ''' Sorts the keyframes when the list is modified.
 ''' </summary>
 Private Sub Keyframes_Sort() _
 Handles m_Keyframes.AddItem, m_Keyframes.InsertItem, m_Keyframes.ModifiedItem, m_Keyframes.RemoveItem

  m_Keyframes.Sort()

 End Sub

End Class
