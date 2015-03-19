Partial Class Material
 ''' <summary>
 ''' Homeworld2 material parameter.
 ''' </summary>
 Public NotInheritable Class Parameter
  ''' <summary>
  ''' List of Homeworld2 material parameter types.
  ''' </summary>
  Public Enum ParameterType
   Bool    ' This may not be supported...
   Float   ' This may not be supported...
   Int     ' This may not be supported...
   Vector  ' This may not be supported...
   Colour
   Texture
   Matrix  ' This may not be supported...

  End Enum

  ' --------------
  ' Class Members.
  ' --------------
  ''' <summary>Name of the paramter.</summary>
  Private m_Name As String

  ''' <summary>Type of parameter.</summary>
  Private m_Type As ParameterType

  ''' <summary>Colour data, RGBA.</summary>
  Private m_Colour As Vector4

  ''' <summary>Index to texture being used, -1 if no texture.</summary>
  Private m_TextureIndex As Integer

  ''' <summary>Texture to use.</summary>
  Private m_Texture As Direct3D.Texture

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
  Public Sub New(ByVal p As Parameter)
   m_Name = p.m_Name
   m_Type = p.m_Type
   m_Colour = p.m_Colour
   m_Texture = Nothing
   m_TextureIndex = p.m_TextureIndex

  End Sub

  ' -----------------
  ' Class properties.
  ' -----------------
  ''' <summary>
  ''' Returns\Sets name of the parameter.
  ''' </summary>
  ''' <exception cref="ArgumentNullException">
  ''' Thrown when <c>value is Nothing</c>.
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
  ''' Returns\Sets the type of parameter.
  ''' </summary>
  ''' <remarks>
  ''' Only <c>Colour</c> and <c>Texture</c> are regarded valid, all others
  ''' are considered invalid.
  ''' </remarks>
  ''' <exception cref="ArgumentException">
  ''' Throw when invalid <c>value</c> is passed.
  ''' </exception>
  Public Property Type() As ParameterType
   Get
    Return m_Type

   End Get

   Set(ByVal value As ParameterType)
    Select Case value
     Case ParameterType.Colour
      m_TextureIndex = -1
      m_Type = value

     Case ParameterType.Texture
      m_Colour = New Vector4(1, 1, 1, 1)
      m_Type = value

     Case Else
      Throw New ArgumentException("Invalid 'value'.")
      Exit Property

    End Select ' Select Case value

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets the colour field of this parameter.
  ''' </summary>
  ''' <remarks>
  ''' This works only if the type of parameter is colour. Otherwise,
  ''' this does nothing.
  ''' </remarks>
  Public Property Colour() As Vector4
   Get
    If m_Type <> ParameterType.Colour Then _
     Return New Vector4(1, 1, 1, 1)

    Return m_Colour

   End Get

   Set(ByVal value As Vector4)
    If m_Type <> ParameterType.Colour Then _
     Exit Property

    m_Colour = value

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets the texture index field of this parameter (use -1 to
  ''' indicate no texture).
  ''' </summary>
  ''' <remarks>
  ''' This works only if the type of parameter is texture. Otherwise,
  ''' this does nothing.
  ''' </remarks>
  Public Property TextureIndex() As Integer
   Get
    If m_Type <> ParameterType.Texture Then _
     Return -1

    Return m_TextureIndex

   End Get

   Set(ByVal value As Integer)
    If m_Type <> ParameterType.Texture Then _
     Exit Property

    If (m_TextureIndex <> -1) AndAlso (m_TextureIndex < 0) Then _
     Throw New ArgumentException("Invalid 'value'.") _
   : Exit Property

    m_TextureIndex = value
    m_Texture = Nothing

   End Set

  End Property

  ''' <summary>
  ''' The texture to use.
  ''' </summary>
  Friend Property Texture() As Direct3D.Texture
   Get
    Return m_Texture

   End Get

   Set(ByVal value As Direct3D.Texture)
    m_Texture = value

   End Set

  End Property

  ' -----------------
  ' Member Functions.
  ' -----------------
  ''' <summary>
  ''' Returns the name of this parameter.
  ''' </summary>
  Public Overrides Function ToString() As String
   Return m_Name

  End Function

  ''' <summary>
  ''' Reads a material parameter from an IFF reader.
  ''' </summary>
  ''' <param name="IFF">
  ''' IFF reader to read from.
  ''' </param>
  ''' <param name="ReadName">
  ''' Whether to read name or not.
  ''' Note that this is for compatibility with version 1000 STAT chunks, which do not have a name.
  ''' Version 1001 STAT chunks have names for parameters.
  ''' </param>
  Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader, Optional ByVal ReadName As Boolean = True)
   ' Set default data.
   Initialize()

   ' Read type.
   m_Type = CType(IFF.ReadInt32(), ParameterType)

   ' Read data length.
   Dim dataLength As Integer = IFF.ReadInt32()

   Select Case m_Type
    Case ParameterType.Colour
     If (dataLength <> 16) Then
      ' Skip unknown data...
      If dataLength <> 0 Then _
       IFF.ReadChars(dataLength)

      Trace.TraceError("STAT chunk may be corrupted.")

     Else ' If dataLength <> 16 Then
      ' Read colour.
      m_Colour.X = IFF.ReadSingle()
      m_Colour.Y = IFF.ReadSingle()
      m_Colour.Z = IFF.ReadSingle()
      m_Colour.W = IFF.ReadSingle()

     End If ' If dataLength <> 16 Then

    Case ParameterType.Texture
     If dataLength <> 4 Then
      ' Skip unknown data...
      If dataLength <> 0 Then _
       IFF.ReadChars(dataLength)

      Trace.TraceError("STAT chunk may be corrupted.")

     Else ' If dataLength <> 4 Then
      ' Read texture index.
      m_TextureIndex = IFF.ReadInt32()

     End If ' If dataLength <> 4 Then

    Case Else
     ' Set type to texture.
     m_Type = ParameterType.Texture

     ' Skip unknown data...
     If dataLength <> 0 Then _
      IFF.BaseStream.Position += dataLength

     Trace.TraceError("STAT chunk may be corrupted.")

   End Select ' Select Case m_Type

   ' Read name if needed.
   If ReadName Then _
    m_Name = IFF.ReadString() _
   Else _
    m_Name = ""

  End Sub

  ''' <summary>
  ''' Writes a material parameter to an IFF writer.
  ''' </summary>
  ''' <param name="IFF">
  ''' IFF writer to write to.
  ''' </param>
  ''' <remarks>
  ''' This writes version 1001 STAT chunk parameters.
  ''' </remarks>
  Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
   IFF.WriteInt32(m_Type)

   Select Case m_Type
    Case ParameterType.Colour
     ' Data length
     IFF.WriteInt32(16)

     ' Colour
     IFF.Write(m_Colour.X)
     IFF.Write(m_Colour.Y)
     IFF.Write(m_Colour.Z)
     IFF.Write(m_Colour.W)

    Case ParameterType.Texture
     ' Data length
     IFF.WriteInt32(4)

     ' Texture index.
     IFF.WriteInt32(m_TextureIndex)

    Case Else
     ' Data length
     IFF.WriteInt32(0)

     ' Nothing written...

   End Select ' Select Case m_Type

   IFF.Write(m_Name)

  End Sub

  ''' <summary>
  ''' Initializes the material parameter to default data.
  ''' </summary>
  Private Sub Initialize()
   m_Type = ParameterType.Texture
   m_Colour = New Vector4(1, 1, 1, 1)
   m_TextureIndex = -1
   m_Texture = Nothing

  End Sub

  ''' <summary>
  ''' Updates the parameter so that it may be used for rendering.
  ''' </summary>
  Friend Function Update(ByVal Textures As IList(Of Texture)) As Parameter
   If (m_Type <> ParameterType.Texture) OrElse _
      (m_TextureIndex < 0) OrElse _
      (m_TextureIndex >= Textures.Count) Then _
    m_Texture = Nothing _
  : Return Me

   m_Texture = Textures(m_TextureIndex).Texture
   Return Me

  End Function

 End Class

End Class
