Imports Microsoft.DirectX

Namespace TextDisplay
 ''' <summary>
 ''' Class to display text in a Direct3D Device.
 ''' </summary>
 Public Class TextDisplay
  ' --------------
  ' Class Members.
  ' --------------
  ''' <summary>All the initialized fonts are stored in this element.</summary>
  Private Shared m_FontCache As FontCache

  ''' <summary>Text position and bounds.</summary>
  Private m_Rect As Drawing.Rectangle

  ''' <summary>Text colour.</summary>
  Private m_Colour As Direct3D.ColorValue

  ''' <summary>Format.</summary>
  Private m_Format As Direct3D.DrawTextFormat

  ''' <summary>Direct3D device associated with the font.</summary>
  Private m_Device As Direct3D.Device

  ''' <summary>All the font attributes are stored in this element.</summary>
  Private m_Font As FontCacheElement

  ' ------------------------
  ' Constructors\Finalizers.
  ' ------------------------
  ''' <summary>
  ''' Class constructor.
  ''' </summary>
  ''' <param name="Device">
  ''' Device associated with the display.
  ''' </param>
  ''' <param name="FontName">
  ''' Name of font used.
  ''' </param>
  ''' <param name="FontSize">
  ''' Size of font used.
  ''' </param>
  ''' <param name="FontStyle">
  ''' Font formatting parameters.
  ''' </param>
  Public Sub New(ByVal Device As Direct3D.Device, _
        Optional ByVal FontName As String = "Arial", _
        Optional ByVal FontSize As Single = 12, _
        Optional ByVal FontStyle As Drawing.FontStyle = FontStyle.Bold)

   ' Check device.
   If Device Is Nothing Then _
    Throw New ArgumentNullException("'Device Is Nothing'") _
  : Exit Sub

   ' Set the default format.
   m_Format = Direct3D.DrawTextFormat.Top Or _
              Direct3D.DrawTextFormat.Left

   ' Set the default colour.
   m_Colour = New Direct3D.ColorValue(255, 255, 255)

   ' Set the device.
   m_Device = Device

   ' Set the default bounds.
   With m_Rect
    .X = 0
    .Y = 0
    .Width = m_Device.PresentationParameters.BackBufferWidth
    .Height = m_Device.PresentationParameters.BackBufferHeight
   End With ' With m_Rect

   ' Now initialize.
   SetAttributes(FontName, FontSize, FontStyle)

  End Sub

  ' -----------------
  ' Class properties.
  ' -----------------
  ''' <summary>
  ''' Returns\Sets the name of font used.
  ''' </summary>
  Public Property FontName() As String
   Get
    Return m_Font.Name

   End Get

   Set(ByVal value As String)
    SetAttributes(value, m_Font.Size, m_Font.Style)

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets the size of font used.
  ''' </summary>
  Public Property FontSize() As Single
   Get
    Return m_Font.Size

   End Get

   Set(ByVal value As Single)
    SetAttributes(m_Font.Name, value, m_Font.Style)

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets the font formatting used.
  ''' </summary>
  Public Property FontStyle() As Drawing.FontStyle
   Get
    Return m_Font.Style

   End Get

   Set(ByVal value As Drawing.FontStyle)
    SetAttributes(m_Font.Name, m_Font.Size, value)

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets the region where the text is drawn.
  ''' </summary>
  Public Property Region() As Drawing.Rectangle
   Get
    Return m_Rect

   End Get

   Set(ByVal value As Drawing.Rectangle)
    m_Rect = value

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets formatting of text.
  ''' </summary>
  Public Property Format() As Direct3D.DrawTextFormat
   Get
    Return m_Format

   End Get

   Set(ByVal value As Direct3D.DrawTextFormat)
    m_Format = value

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets the colour in which text is drawn.
  ''' </summary>
  Public Property Colour() As Direct3D.ColorValue
   Get
    Return m_Colour

   End Get

   Set(ByVal value As Direct3D.ColorValue)
    m_Colour = value

   End Set

  End Property

  ''' <summary>Returns the Direct3D device used.</summary>
  Protected ReadOnly Property m_pDevice() As Direct3D.Device
   Get
    Return m_Device

   End Get

  End Property

  ''' <summary>Returns the font used.</summary>
  Protected ReadOnly Property m_pFont() As Direct3D.Font
   Get
    Return m_Font.Direct3DFont

   End Get

  End Property

  ' ---------
  ' Operators
  ' ---------
  ' None

  ' -----------------
  ' Member Functions.
  ' -----------------
  ''' <summary>
  ''' Modifies the font attributes.
  ''' </summary>
  ''' <param name="FontName">
  ''' Name of the font used.
  ''' </param>
  ''' <param name="FontSize">
  ''' Size of the font used.
  ''' </param>
  ''' <param name="FontStyle">
  ''' Font style.
  ''' </param>
  Public Sub SetAttributes(ByVal FontName As String, ByVal FontSize As Single, _
                           ByVal FontStyle As Drawing.FontStyle)

   ' Check inputs.
   If FontName = "" Then _
    Throw New ArgumentException("'FontName = """"'") _
  : Exit Sub

   ' Check inputs.
   If FontSize <= 0 Then _
    Throw New ArgumentException("'FontSize <= 0'") _
  : Exit Sub

   ' Get the font.
   m_Font = m_FontCache.RegisterFont(FontName, FontSize, FontStyle, m_Device)

  End Sub

  ''' <summary>
  ''' Draws the specified text.
  ''' </summary>
  ''' <param name="Text">
  ''' The text to draw.
  ''' </param>
  Public Overridable Function DrawText(ByVal Text As String, Optional ByVal Sprite As Direct3D.Sprite = Nothing) As Integer
   m_Font.Direct3DFont.DrawText(Sprite, Text, m_Rect, m_Format, m_Colour.ToArgb())

  End Function

  ''' <summary>
  ''' Releases all references to video memory resources and deletes all state blocks.
  ''' </summary>
  Public Shared Sub OnLostDevice(ByVal Device As Direct3D.Device)
   m_FontCache.OnLostDevice(Device)

  End Sub

  ''' <summary>
  ''' Should be called after the device is reset and before any other methods are called,
  ''' if <c>Microsoft.DirectX.Direct3D.Device.IsUsingEventHandlers</c> is set to false.
  ''' </summary>
  Public Shared Sub OnResetDevice(ByVal Device As Direct3D.Device)
   m_FontCache.OnResetDevice(Device)

  End Sub

  ''' <summary>
  ''' Should be called when the device is disposing.
  ''' </summary>
  Public Shared Sub OnDeviceDisposing(ByVal Device As Direct3D.Device)
   m_FontCache.OnDeviceDisposing(Device)

  End Sub

 End Class

End Namespace
