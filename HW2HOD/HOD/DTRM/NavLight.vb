Imports GenericMesh

''' <summary>
''' Class representing a Homeworld2 Nav Light.
''' </summary>
Public NotInheritable Class NavLight
 ' --------------
 ' Class Members.
 ' --------------
 ''' <summary>Name.</summary>
 Private m_Name As String

 ''' <summary>Section.</summary>
 Private m_Section As UInteger

 ''' <summary>Size.</summary>
 Private m_Size As Single

 ''' <summary>Phase.</summary>
 Private m_Phase As Single

 ''' <summary>Frequency.</summary>
 Private m_Frequency As Single

 ''' <summary>Style.</summary>
 Private m_Style As String

 ''' <summary>Colour.</summary>
 Private m_Colour As Vector3

 ''' <summary>Distance.</summary>
 Private m_Distance As Single

 ''' <summary>Sprite visible.</summary>
 Private m_SpriteVisible As Boolean

 ''' <summary>High end only.</summary>
 Private m_HighEndOnly As Boolean

 ''' <summary>Whether the NavLight is visible or not.</summary>
 Private m_Visible As Boolean

 ' ------------------------
 ' Constructors\Finalizers.
 ' ------------------------
 ''' <summary>
 ''' Class constructor.
 ''' </summary>
 Public Sub New()
  Initialize()

 End Sub

 ''' <summary>
 ''' Copy constructor.
 ''' </summary>
 Public Sub New(ByVal n As NavLight)
  m_Name = n.m_Name
  m_Section = n.m_Section
  m_Size = n.m_Size
  m_Phase = n.m_Phase
  m_Frequency = n.m_Frequency
  m_Style = n.m_Style
  m_Distance = n.m_Distance
  m_Colour = n.m_Colour
  m_SpriteVisible = n.m_SpriteVisible
  m_HighEndOnly = n.m_HighEndOnly
  m_Visible = n.m_Visible

 End Sub

 ' -----------------
 ' Class properties.
 ' -----------------
 ''' <summary>
 ''' Name of NavLight. A joint of same name exists. Usually "NavLightX"
 ''' where X is a number.
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
 ''' Section.
 ''' </summary>
 Public Property Section() As UInteger
  Get
   Return m_Section

  End Get

  Set(ByVal value As UInteger)
   m_Section = value

  End Set

 End Property

 ''' <summary>
 ''' Size of NavLight.
 ''' </summary>
 ''' <exception cref="ArgumentOutOfRangeException">
 ''' Thrown when <c>value</c> &lt; 0.
 ''' </exception>
 Public Property Size() As Single
  Get
   Return m_Size

  End Get

  Set(ByVal value As Single)
   If (value < 0) Then _
    Throw New ArgumentOutOfRangeException("value") _
  : Exit Property

   m_Size = value

  End Set

 End Property

 ''' <summary>
 ''' Phase, or the startup delay, in seconds.
 ''' </summary>
 ''' <exception cref="ArgumentOutOfRangeException">
 ''' Thrown when <c>value</c> &lt; 0.
 ''' </exception>
 Public Property Phase() As Single
  Get
   Return m_Phase

  End Get

  Set(ByVal value As Single)
   If (value < 0) Then _
    Throw New ArgumentOutOfRangeException("value") _
  : Exit Property

   m_Phase = value

  End Set

 End Property

 ''' <summary>
 ''' Frequency of NavLight.
 ''' </summary>
 ''' <exception cref="ArgumentOutOfRangeException">
 ''' Thrown when <c>value</c> &lt; 0.
 ''' </exception>
 Public Property Frequency() As Single
  Get
   Return m_Frequency

  End Get

  Set(ByVal value As Single)
   If (value < 0) Then _
    Throw New ArgumentOutOfRangeException("value") _
  : Exit Property

   m_Frequency = value

  End Set

 End Property

 ''' <summary>
 ''' Style script used by NavLight. Usually "default".
 ''' </summary>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when <c>value Is Nothing</c>.
 ''' </exception>
 Public Property Style() As String
  Get
   Return m_Style

  End Get

  Set(ByVal value As String)
   If (value Is Nothing) OrElse (value = "") Then _
    Throw New ArgumentNullException("value") _
  : Exit Property

   m_Style = value

  End Set

 End Property

 ''' <summary>
 ''' NavLight colour.
 ''' </summary>
 Public Property Colour() As Direct3D.ColorValue
  Get
   Return New Direct3D.ColorValue(m_Colour.X, m_Colour.Y, m_Colour.Z)

  End Get

  Set(ByVal value As Direct3D.ColorValue)
   m_Colour.X = value.Red
   m_Colour.Y = value.Green
   m_Colour.Z = value.Blue

  End Set

 End Property

 ''' <summary>
 ''' Distance upto which light is emitted by this NavLight.
 ''' </summary>
 ''' <exception cref="ArgumentOutOfRangeException">
 ''' Thrown when <c>value</c> &lt; 0.
 ''' </exception>
 Public Property Distance() As Single
  Get
   Return m_Distance

  End Get

  Set(ByVal value As Single)
   If (value < 0) Then _
    Throw New ArgumentOutOfRangeException("value") _
  : Exit Property

   m_Distance = value

  End Set

 End Property

 ''' <summary>
 ''' Whether the sprite is visible or not.
 ''' </summary>
 Public Property SpriteVisible() As Boolean
  Get
   Return m_SpriteVisible

  End Get

  Set(ByVal value As Boolean)
   m_SpriteVisible = value

  End Set

 End Property

 ''' <summary>
 ''' Whether to cast light on High end systems only.
 ''' </summary>
 Public Property HighEndOnly() As Boolean
  Get
   Return m_HighEndOnly

  End Get

  Set(ByVal value As Boolean)
   m_HighEndOnly = value

  End Set

 End Property

 ''' <summary>
 ''' Returns\Sets whether this navlight is visible or not.
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
 ''' Returns the name of this NavLight.
 ''' </summary>
 Public Overrides Function ToString() As String
  Return m_Name

 End Function

 ''' <summary>
 ''' Initializes the NavLight.
 ''' </summary>
 Private Sub Initialize()
  m_Name = "Root"
  m_Section = 0
  m_Size = 1
  m_Phase = 0
  m_Frequency = 1
  m_Style = "default"
  m_Distance = 0
  m_Colour = New Vector3(1, 1, 1)
  m_SpriteVisible = True
  m_HighEndOnly = False
  m_Visible = False

 End Sub

 ''' <summary>
 ''' Reads the NavLight from an IFF reader.
 ''' </summary>
 Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader)
  Initialize()

  m_Name = IFF.ReadString()
  m_Section = IFF.ReadUInt32()
  m_Size = IFF.ReadSingle()
  m_Phase = IFF.ReadSingle()
  m_Frequency = IFF.ReadSingle()
  m_Style = IFF.ReadString()

  m_Colour.X = IFF.ReadSingle()
  m_Colour.Y = IFF.ReadSingle()
  m_Colour.Z = IFF.ReadSingle()

  IFF.ReadSingle() ' 1.0f

  m_Distance = IFF.ReadSingle()

  m_SpriteVisible = (IFF.ReadByte() <> 0)
  m_HighEndOnly = (IFF.ReadByte() <> 0)

 End Sub

 ''' <summary>
 ''' Writes the NavLight to an IFF writer.
 ''' </summary>
 Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
  ' Check for proper name.
  If (m_Name.Length < 8) OrElse (String.Compare(m_Name.Substring(0, 8), "NavLight", True) <> 0) Then _
   Trace.TraceWarning("NavLight '" & m_Name & "' doesn't have a 'NavLight' prefix.")

  IFF.Write(m_Name)
  IFF.WriteUInt32(m_Section)
  IFF.Write(m_Size)
  IFF.Write(m_Phase)
  IFF.Write(m_Frequency)
  IFF.Write(m_Style)

  IFF.Write(m_Colour.X)
  IFF.Write(m_Colour.Y)
  IFF.Write(m_Colour.Z)
  IFF.Write(CSng(1))

  IFF.Write(m_Distance)

  If m_SpriteVisible Then _
   IFF.Write(CByte(1)) _
  Else _
   IFF.Write(CByte(0))

  If m_HighEndOnly Then _
   IFF.Write(CByte(1)) _
  Else _
   IFF.Write(CByte(0))

 End Sub

 ''' <summary>
 ''' Renders the NavLight on the specified device using the specified
 ''' transform and mesh. The mesh must be a unit radius sphere without colour.
 ''' </summary>
 Friend Sub Render(ByVal Device As Direct3D.Device, _
                   ByVal Transform As Matrix, _
                   ByVal Mesh As GBasicMesh(Of PNVertex, Integer, Standard.Material))

  ' If not visible then do nothing.
  If Not m_Visible Then _
   Exit Sub

  ' Get the material.
  Dim m As Standard.Material = Mesh.Material(0)

  ' Set material attributes.
  m.Attributes = New Direct3D.Material With { _
   .AmbientColor = New Direct3D.ColorValue(0, 0, 0), _
   .DiffuseColor = New Direct3D.ColorValue(0, 0, 0), _
   .SpecularColor = New Direct3D.ColorValue(0, 0, 0), _
   .EmissiveColor = New Direct3D.ColorValue(m_Colour.X, m_Colour.Y, m_Colour.Z) _
  }

  ' Set material.
  Mesh.Material(0) = m

  ' Set transform.
  Device.Transform.World = Matrix.Scaling(0.5F * m_Size, 0.5F * m_Size, 0.5F * m_Size) * Transform

  ' Render mesh.
  Mesh.Render(Device)

 End Sub

End Class
