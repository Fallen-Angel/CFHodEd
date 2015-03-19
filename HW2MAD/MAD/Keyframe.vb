''' <summary>
''' Class representing a Homeworld2 animation curve keyframe.
''' </summary>
Friend Structure Keyframe
 Implements IComparable(Of Keyframe)

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

 ''' <summary>
 ''' Compares two keyframes, according to time.
 ''' </summary>
 ''' <param name="other">
 ''' The keyframe to compare to.
 ''' </param>
 Public Function CompareTo(ByVal other As Keyframe) As Integer Implements System.IComparable(Of Keyframe).CompareTo
  Return Time.CompareTo(other.Time)

 End Function

End Structure
