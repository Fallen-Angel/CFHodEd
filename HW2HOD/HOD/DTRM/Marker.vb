''' <summary>
''' Class representing Homeworld2 marker.
''' </summary>
Public NotInheritable Class Marker
 ' --------------
 ' Class Members.
 ' --------------
 ''' <summary>Name.</summary>
 Private m_Name As String

 ''' <summary>Parent Name.</summary>
 Private m_ParentName As String

 ''' <summary>Position.</summary>
 Private m_Position As Vector3

 ''' <summary>Rotation.</summary>
 Private m_Rotation As Vector3

 ''' <summary>Animation curves.</summary>
 Private m_AnimationCurves As New EventList(Of AnimationCurve)

 ''' <summary>Visible.</summary>
 Private m_Visible As Boolean

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
 Public Sub New(ByVal m As Marker)
  m_Name = m.m_Name
  m_ParentName = m.m_ParentName
  m_Position = m.m_Position
  m_Rotation = m.m_Rotation

  For I As Integer = 0 To m.m_AnimationCurves.Count - 1
   m_AnimationCurves.Add(New AnimationCurve(m.m_AnimationCurves(I)))

  Next I   ' For I As Integer = 0 To m.m_AnimationCurves.Count - 1
 End Sub

 ' -----------------
 ' Class properties.
 ' -----------------
 ''' <summary>
 ''' Returns\Sets the name of this marker.
 ''' </summary>
 Public Property Name() As String
  Get
   Return m_Name

  End Get

  Set(ByVal value As String)
   If (value Is Nothing) OrElse (value = "") Then _
    Throw New ArgumentNullException("value") _
  : Exit Property

   m_Name = value

  End Set

 End Property

 ''' <summary>
 ''' Returns\Sets the name of parent of this marker.
 ''' </summary>
 Public Property ParentName() As String
  Get
   Return m_ParentName

  End Get

  Set(ByVal value As String)
   If (value Is Nothing) OrElse (value = "") Then _
    Throw New ArgumentNullException("value") _
  : Exit Property

   m_ParentName = value

  End Set

 End Property

 ''' <summary>
 ''' Returns\Sets position of this marker.
 ''' </summary>
 Public Property Position() As Vector3
  Get
   Return m_Position

  End Get

  Set(ByVal value As Vector3)
   m_Position = value

  End Set

 End Property

 ''' <summary>
 ''' Returns\Sets rotation of this marker.
 ''' </summary>
 Public Property Rotation() As Vector3
  Get
   Return m_Rotation

  End Get

  Set(ByVal value As Vector3)
   m_Rotation = value

  End Set

 End Property

 ''' <summary>
 ''' Returns the list of animation curves.
 ''' </summary>
 Friend ReadOnly Property AnimationCurves() As IList(Of AnimationCurve)
  Get
   Return m_AnimationCurves

  End Get

 End Property

 ''' <summary>
 ''' Returns\Sets whether this marker is visible or not.
 ''' </summary>
 ''' <remarks>
 ''' This is purely a rendering related property. It does not
 ''' affect what is written in the HOD.
 ''' </remarks>
 Public Property Visible() As Boolean
  Get
   Return m_Visible

  End Get

  Set(ByVal value As Boolean)
   m_Visible = value

  End Set

 End Property

 ' -----------------
 ' Member Functions.
 ' -----------------
 ''' <summary>
 ''' Returns the name of this marker.
 ''' </summary>
 Public Overrides Function ToString() As String
  Return m_Name

 End Function

 ''' <summary>
 ''' Reads the marker from an IFF reader.
 ''' </summary>
 Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader)
  ' Add the handlers for HEAD and KEYF chunk and parse.
  IFF.AddHandler("HEAD", Homeworld2.IFF.ChunkType.Normal, AddressOf ReadHEADChunk, 1)
  IFF.AddHandler("KEYF", Homeworld2.IFF.ChunkType.Form, AddressOf ReadKEYFChunk)
  IFF.Parse()

 End Sub

 ''' <summary>
 ''' Writes the marker to an IFF writer.
 ''' </summary>
 Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
  Dim startTime As Single = 0, _
      endTime As Single = 0, _
      first As Boolean = True

  For I As Integer = 0 To m_AnimationCurves.Count - 1
   For J As Integer = 0 To m_AnimationCurves(I).Keyframes.Count - 1
    Dim time As Single = CSng(m_AnimationCurves(I).Keyframes(J).Time)

    If first Then _
     startTime = time _
   : endTime = time _
   : first = False _
    Else _
     startTime = Math.Min(startTime, time) _
   : endTime = Math.Max(endTime, time)

   Next J ' For J As Integer = 0 To m_AnimationCurves(I).Keyframes.Count - 1
  Next I ' For I As Integer = 0 To m_AnimationCurves.Count - 1

  IFF.Push("MRKR", Homeworld2.IFF.ChunkType.Form)

  IFF.Push("HEAD", Homeworld2.IFF.ChunkType.Normal, 1)

  IFF.Write(m_Name)
  IFF.Write(m_ParentName)

  IFF.Write(startTime)
  IFF.Write(endTime)

  IFF.Write(CDbl(m_Position.X))
  IFF.Write(CDbl(m_Position.Y))
  IFF.Write(CDbl(m_Position.Z))

  IFF.Write(CDbl(m_Rotation.X))
  IFF.Write(CDbl(m_Rotation.Y))
  IFF.Write(CDbl(m_Rotation.Z))

  IFF.Pop() ' HEAD

  IFF.Push("KEYF", Homeworld2.IFF.ChunkType.Form)

  For I As Integer = 0 To m_AnimationCurves.Count - 1
   m_AnimationCurves(I).WriteIFF(IFF)

  Next I ' For I As Integer = 0 To m_AnimationCurves.Count - 1

  IFF.Pop() ' KEYF

  IFF.Pop() ' MRKR

 End Sub

 ''' <summary>
 ''' Reads the HEAD chunk from an IFF reader.
 ''' </summary>
 Private Sub ReadHEADChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
  ' Read the name and parent name.
  m_Name = IFF.ReadString()
  m_ParentName = IFF.ReadString()

  ' Read the animation start and end times.
  IFF.ReadSingle()
  IFF.ReadSingle()

  ' Read position and rotation.
  m_Position.X = CSng(IFF.ReadDouble())
  m_Position.Y = CSng(IFF.ReadDouble())
  m_Position.Z = CSng(IFF.ReadDouble())

  m_Rotation.X = CSng(IFF.ReadDouble())
  m_Rotation.Y = CSng(IFF.ReadDouble())
  m_Rotation.Z = CSng(IFF.ReadDouble())

 End Sub

 ''' <summary>
 ''' Reads the KEYF chunk from an IFF reader.
 ''' </summary>
 Private Sub ReadKEYFChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
  ' Add the ANIM handler and parse the HOD.
  IFF.AddHandler("ANIM", Homeworld2.IFF.ChunkType.Normal, AddressOf ReadANIMChunk, 1)
  IFF.Parse()

 End Sub

 ''' <summary>
 ''' Reads the KEYF chunk from an IFF reader.
 ''' </summary>
 Private Sub ReadANIMChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
  Dim anim As New AnimationCurve
  anim.ReadIFF(IFF)
  m_AnimationCurves.Add(anim)

 End Sub

 ''' <summary>
 ''' Initializes the marker.
 ''' </summary>
 Private Sub Initialize()
  m_Name = "marker"
  m_ParentName = "Root"
  m_Position = New Vector3(0, 0, 0)
  m_Rotation = New Vector3(0, 0, 0)
  m_AnimationCurves.Clear()

 End Sub

 ''' <summary>
 ''' Renders the marker using the specified mesh.
 ''' </summary>
 Friend Sub Render(ByVal Device As Direct3D.Device, _
                   ByVal Transform As Matrix, _
                   ByVal Mesh As GenericMesh.Standard.BasicMesh, _
          Optional ByVal Scale As Single = 1.0F)

  If Not m_Visible Then _
   Exit Sub

  ' Set transform.
  Device.Transform.World = Matrix.Scaling(Scale, Scale, Scale) * _
                           Matrix.RotationX(m_Rotation.X) * _
                           Matrix.RotationY(m_Rotation.Y) * _
                           Matrix.RotationZ(m_Rotation.Z) * _
                           Matrix.Translation(m_Position) * _
                           Transform

  ' Render mesh.
  Mesh.Render(Device)

 End Sub

End Class

''' <summary>
''' Class representing a Homeworld2 animation curve.
''' </summary>
Friend NotInheritable Class AnimationCurve
 ' --------------
 ' Class Members.
 ' --------------
 ''' <summary>Name.</summary>
 Private m_Name As String

 ''' <summary>List of keyframes.</summary>
 Private m_Keyframes As New EventList(Of Keyframe)

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
 Public Property Name() As String
  Get
   Return m_Name

  End Get

  Set(ByVal value As String)
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
 Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader)
  ' Initialize.
  Initialize()

  ' Read name of animation curve.
  m_Name = IFF.ReadString()

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
 Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
  IFF.Push("ANIM", Homeworld2.IFF.ChunkType.Normal, 1)

  ' Write animation curve name.
  IFF.Write(m_Name)

  ' Write keyframe count.
  IFF.WriteInt32(m_Keyframes.Count)

  ' Write the keyframes.
  For I As Integer = 0 To m_Keyframes.Count - 1
   m_Keyframes(I).WriteIFF(IFF)

  Next I ' For I As Integer = 0 To m_Keyframes.Count - 1

  ' Write pre infinity and post infinity.
  IFF.WriteInt32(m_PreInfinity)
  IFF.WriteInt32(m_PostInfinity)

  IFF.Pop()

 End Sub

 ''' <summary>
 ''' Initializes the animation curve.
 ''' </summary>
 Private Sub Initialize()
  m_Name = "Animation Curve"
  m_Keyframes.Clear()

 End Sub

End Class

''' <summary>
''' Class representing a Homeworld2 animation curve keyframe.
''' </summary>
Friend Structure Keyframe
 ' --------------
 ' Class Members.
 ' --------------
 ''' <summary>Time of keyframe.</summary>
 Public Time As Double

 ''' <summary>Value at keyframe.</summary>
 Public Value As Double

 ''' <summary>In tangent at keyframe.</summary>
 Friend InTangent As Vector2

 ''' <summary>Out tangent at keyframe.</summary>
 Friend OutTangent As Vector2

 ' ------------------------
 ' Constructors\Finalizers.
 ' ------------------------
 ''' <summary>
 ''' Structure copy constructor.
 ''' </summary>
 Public Sub New(ByVal k As Keyframe)
  Time = k.Time
  Value = k.Value
  InTangent = k.InTangent
  OutTangent = k.OutTangent

 End Sub

 ' -----------------
 ' Member Functions.
 ' -----------------
 ''' <summary>
 ''' Returns a string representation of this keyframe.
 ''' </summary>
 Public Overrides Function ToString() As String
  Return "{ " & FormatNumber(Time, 3) & ", " & FormatNumber(Value, 3) & " }"

 End Function

 ''' <summary>
 ''' Compares two keyframe objects to determine if they're equal.
 ''' </summary>
 Public Overrides Function Equals(ByVal obj As Object) As Boolean
  ' Check for same type.
  If Not TypeOf obj Is Keyframe Then _
   Return False

  ' Equate.
  Return CType(obj, Keyframe) = Me

 End Function

 ''' <summary>
 ''' Operator that tests for equality.
 ''' </summary>
 Public Shared Operator =(ByVal L As Keyframe, ByVal R As Keyframe) As Boolean
  If (L.Time = R.Time) AndAlso (L.Value = R.Value) Then _
   Return True _
  Else _
   Return False

 End Operator

 ''' <summary>
 ''' Operator that tests for inequality.
 ''' </summary>
 Public Shared Operator <>(ByVal L As Keyframe, ByVal R As Keyframe) As Boolean
  If (L.Time <> R.Time) OrElse (L.Value <> R.Value) Then _
   Return True _
  Else _
   Return False

 End Operator

 ''' <summary>
 ''' Reads a keyframe from an IFF reader.
 ''' </summary>
 Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader)
  Time = IFF.ReadDouble()
  Value = IFF.ReadDouble()
  InTangent.X = IFF.ReadSingle()
  InTangent.Y = IFF.ReadSingle()
  OutTangent.X = IFF.ReadSingle()
  OutTangent.Y = IFF.ReadSingle()

 End Sub

 ''' <summary>
 ''' Writes the keyframe to an IFF reader.
 ''' </summary>
 Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
  IFF.Write(Time)
  IFF.Write(Value)
  IFF.Write(InTangent.X)
  IFF.Write(InTangent.Y)
  IFF.Write(OutTangent.X)
  IFF.Write(OutTangent.Y)

 End Sub

 ''' <summary>
 ''' Sets the in tangent.
 ''' </summary>
 Friend Function SetInTangent(ByVal v As Vector2) As Keyframe
  InTangent = v
  Return Me

 End Function

 ''' <summary>
 ''' Sets the out tangent.
 ''' </summary>
 Friend Function SetOutTangent(ByVal v As Vector2) As Keyframe
  OutTangent = v
  Return Me

 End Function

End Structure

''' <summary>
''' Enumeration describing the behaviour of extreme keyframes
''' (first keyframe -> pre infinity, last keyframe -> post infinity.
''' </summary>
Friend Enum InfinityType
 Constant
 Linear
 Cycle
 CycleWithOffset
 Oscillate

End Enum
