''' <summary>
''' Class to represent Homeworld2 Engine Burn (the trail left by
''' fighters and corvettes).
''' </summary>
Public NotInheritable Class EngineBurn
 ' --------------
 ' Class Members.
 ' --------------
 ''' <summary>Name.</summary>
 Private m_Name As String

 ''' <summary>Parent name.</summary>
 Private m_ParentName As String

 ''' <summary>Vertices.</summary>
 Private m_Vertices(4) As Vector3

 ''' <summary>Whether the mesh is visible or not.</summary>
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
 ''' Copy constructor.
 ''' </summary>
 Public Sub New(ByVal eb As EngineBurn)
  m_Name = eb.m_Name
  m_ParentName = eb.m_ParentName

  For I As Integer = 0 To m_Vertices.Length - 1
   m_Vertices(I) = eb.m_Vertices(I)

  Next I ' For I As Integer = 0 To m_Vertices.Length - 1

 End Sub

 ' -----------------
 ' Class properties.
 ' -----------------
 ''' <summary>
 ''' Name of engine burn. Usually "EngineBurnX" where X is a number.
 ''' </summary>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when <c>value Is Nothing</c>.
 ''' </exception>
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
 ''' Name of joint under which this engine burn is parented.
 ''' Usually "EngineNozzleX" where X is a number.
 ''' </summary>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when <c>value Is Nothing</c>.
 ''' </exception>
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
 ''' Returns the vertex count.
 ''' </summary>
 Public ReadOnly Property VertexCount() As Integer
  Get
   Return m_Vertices.Length

  End Get

 End Property

 ''' <summary>
 ''' Returns\Sets the engine burn position data.
 ''' </summary>
 ''' <param name="index">
 ''' The vertex being changed.
 ''' </param>
 ''' <exception cref="ArgumentOutOfRangeException">
 ''' Thrown when <c>index</c> is out of range.
 ''' </exception>
 Public Property Vertices(ByVal index As Integer) As Vector3
  Get
   If (index < 0) OrElse (index >= VertexCount) Then _
    Throw New ArgumentOutOfRangeException("index") _
  : Exit Property

   Return m_Vertices(index)

  End Get

  Set(ByVal value As Vector3)
   If (index < 0) OrElse (index >= VertexCount) Then _
    Throw New ArgumentOutOfRangeException("index") _
  : Exit Property

   m_Vertices(index) = value

  End Set

 End Property

 ''' <summary>
 ''' Returns\Sets whether the mesh is visible or not.
 ''' </summary>
 ''' <remarks>
 ''' This is purely a rendering related property. It does not
 ''' affect what is written in the HOD
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
 ''' Returns the name of this mesh.
 ''' </summary>
 Public Overrides Function ToString() As String
  Return m_Name

 End Function

 ''' <summary>
 ''' Reads the engine burn mesh from an IFF reader.
 ''' </summary>
 Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader)
  ' Initialize.
  Initialize()

  ' Read name and parent name.
  m_Name = IFF.ReadString()
  m_ParentName = IFF.ReadString()

  ' Read division count and flame count.
  Dim numDivisions As Integer = IFF.ReadInt32()
  Dim numFlames As Integer = IFF.ReadInt32()

  Trace.Assert(numDivisions = 5, "Number of divisions in engine burn did not match!")
  Trace.Assert(numFlames = 1, "Number of flames in engine burn did not match!")

  ' Do nothing if an assert failed.
  If (numDivisions <> 5) OrElse (numFlames <> 1) Then _
   Exit Sub

  ' Read data.
  For I As Integer = 0 To numFlames - 1
   For J As Integer = 0 To numDivisions - 1
    With m_Vertices(numDivisions * I + J)
     .X = IFF.ReadSingle()
     .Y = IFF.ReadSingle()
     .Z = IFF.ReadSingle()

    End With ' With m_Vertices(numDivisions * I + J)
   Next J ' For J As Integer = 0 To numDivisions - 1
  Next I ' For I As Integer = 0 To numFlames - 1

 End Sub

 ''' <summary>
 ''' Writes the engine burn mesh to an IFF writer.
 ''' </summary>
 Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
  IFF.Push("BURN")

  IFF.Write(m_Name)
  IFF.Write(m_ParentName)
  IFF.WriteInt32(m_Vertices.Length)
  IFF.WriteInt32(1)

  For I As Integer = 0 To m_Vertices.Length - 1
   IFF.Write(m_Vertices(I).X)
   IFF.Write(m_Vertices(I).Y)
   IFF.Write(m_Vertices(I).Z)

  Next I ' For I As Integer = 0 To m_Vertices.Length - 1

  IFF.Pop()

 End Sub

 ''' <summary>
 ''' Renders this engine burn to a Direct3D Device.
 ''' </summary>
 Friend Sub Render(ByVal Device As Direct3D.Device)
  ' If not visible, do nothing.
  If Not m_Visible Then _
   Exit Sub

  With Device
   ' Use an all-white material.
   .Material = New Direct3D.Material With { _
    .AmbientColor = New Direct3D.ColorValue(1.0F, 1.0F, 1.0F), _
    .DiffuseColor = New Direct3D.ColorValue(1.0F, 1.0F, 1.0F), _
    .SpecularColor = New Direct3D.ColorValue(1.0F, 1.0F, 1.0F), _
    .EmissiveColor = New Direct3D.ColorValue(1.0F, 1.0F, 1.0F) _
   }

   ' Set FVF
   .VertexFormat = Direct3D.VertexFormats.Position

   ' Render as line strip.
   .DrawUserPrimitives(Direct3D.PrimitiveType.LineStrip, m_Vertices.Length - 1, m_Vertices)

   ' Reset FVF
   .VertexFormat = Direct3D.VertexFormats.None

  End With ' With Device

 End Sub

 ''' <summary>
 ''' Initializes this engine burn.
 ''' </summary>
 Private Sub Initialize()
  m_Name = "EngineBurn"
  m_ParentName = "Root"

  m_Vertices(0) = New Vector3(0, 0, 0)
  m_Vertices(1) = New Vector3(0, 0, 1)
  m_Vertices(2) = New Vector3(0, 0, 2)
  m_Vertices(3) = New Vector3(0, 0, 3)
  m_Vertices(4) = New Vector3(0, 0, 4)

 End Sub

End Class
