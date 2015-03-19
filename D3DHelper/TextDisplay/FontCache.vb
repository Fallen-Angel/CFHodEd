Imports Microsoft.DirectX

Namespace TextDisplay
 ''' <summary>
 ''' Font cache.
 ''' </summary>
 Friend Structure FontCache
  ' ------------------
  ' Structure Members.
  ' ------------------
  ''' <summary>
  ''' List of system fonts.
  ''' </summary>
  ''' <remarks></remarks>
  Private m_SystemFonts As List(Of Drawing.Font)

  ''' <summary>
  ''' Elements of the cache.
  ''' </summary>
  Private m_Elements As List(Of FontCacheElement)

  ' ------------------------
  ' Constructors\Finalizers.
  ' ------------------------
  ' None

  ' -----------------
  ' Class properties.
  ' -----------------
  ' None

  ' ---------
  ' Operators
  ' ---------
  ' None

  ' -----------------
  ' Member Functions.
  ' -----------------
  ''' <summary>
  ''' Initializes the font cache.
  ''' </summary>
  Private Sub Initialize()
   m_SystemFonts = New List(Of Drawing.Font)
   m_Elements = New List(Of FontCacheElement)

  End Sub

  ''' <summary>
  ''' Adds the specified font or returns it.
  ''' </summary>
  ''' <param name="FontName">
  ''' Name of font.
  ''' </param>
  ''' <param name="FontSize">
  ''' Size of font.
  ''' </param>
  ''' <param name="FontStyle">
  ''' Font style.
  ''' </param>
  ''' <param name="Device">
  ''' Device associated with the font.
  ''' </param>
  Public Function RegisterFont(ByVal FontName As String, ByVal FontSize As Single, _
                               ByVal FontStyle As Drawing.FontStyle, _
                               ByVal Device As Direct3D.Device) As FontCacheElement

   Dim SysFont As Drawing.Font
   Dim Element As FontCacheElement

   ' Initialize if needed.
   If m_Elements Is Nothing Then _
    Initialize()

   ' Check inputs.
   If FontName = "" Then _
    Throw New ArgumentException("'FontName = """"'") _
  : Exit Function

   ' Check inputs.
   If FontSize <= 0 Then _
    Throw New ArgumentException("'FontSize <= 0'") _
  : Exit Function

   ' Check device.
   If Device Is Nothing Then _
    Throw New ArgumentNullException("'Device Is Nothing'") _
  : Exit Function

   ' Try to locate the system font.
   SysFont = m_SystemFonts.Find( _
              Function(V As Drawing.Font) ( _
               (V.Name = FontName) AndAlso _
               (V.SizeInPoints = FontSize) AndAlso _
               (V.Style = FontStyle) _
              ) _
             )

   ' Try to locate the element.
   Element = m_Elements.Find( _
              Function(V As FontCacheElement) ( _
               (V.Name = FontName) AndAlso _
               (V.Size = FontSize) AndAlso _
               (V.Style = FontStyle) AndAlso _
               (V.Device Is Device) _
              ) _
             )

   ' If not present then add one.
   If Element Is Nothing Then
    If SysFont Is Nothing Then
     Element = New FontCacheElement(FontName, FontSize, FontStyle, Device)
     m_Elements.Add(Element)
     m_SystemFonts.Add(Element.SystemFont)

    Else ' If SysFont Is Nothing Then
     Element = New FontCacheElement(SysFont, Device)
     m_Elements.Add(Element)

    End If ' If SysFont Is Nothing Then
   End If ' If Element Is Nothing Then

   ' Now return it.
   Return Element

  End Function

  ''' <summary>
  ''' Releases all references to video memory resources and deletes all state blocks.
  ''' </summary>
  Public Sub OnLostDevice(ByVal Device As Direct3D.Device)
   ' Check if initialized.
   If m_Elements Is Nothing Then _
    Exit Sub

   ' Check inputs.
   If Device Is Nothing Then _
    Exit Sub

   ' Call the method for each element with the given device.
   For Each V As FontCacheElement In m_Elements
    If V.Device Is Device Then _
     V.Direct3DFont.OnLostDevice()

   Next V ' For Each V As FontCacheElement In m_Elements

  End Sub

  ''' <summary>
  ''' Should be called after the device is reset and before any other methods are called,
  ''' if <c>Microsoft.DirectX.Direct3D.Device.IsUsingEventHandlers</c> is set to false.
  ''' </summary>
  Public Sub OnResetDevice(ByVal Device As Direct3D.Device)
   ' Check if initialized.
   If m_Elements Is Nothing Then _
    Exit Sub

   ' Check inputs.
   If Device Is Nothing Then _
    Exit Sub

   ' Call the method for each element with the given device.
   For Each V As FontCacheElement In m_Elements
    If V.Device Is Device Then _
     V.Direct3DFont.OnResetDevice()

   Next V ' For Each V As FontCacheElement In m_Elements

  End Sub

  ''' <summary>
  ''' Should be called when the device is disposing.
  ''' </summary>
  Public Sub OnDeviceDisposing(ByVal Device As Direct3D.Device)
   ' Check if initialized.
   If m_Elements Is Nothing Then _
    Exit Sub

   ' Check inputs.
   If Device Is Nothing Then _
    Exit Sub

   ' Call the method for each element with the given device.
   For Each V As FontCacheElement In m_Elements
    If V.Device Is Device Then _
     V.Direct3DFont.Dispose()

   Next V ' For Each V As FontCacheElement In m_Elements

   ' Remove the disposed elements.
   m_Elements.RemoveAll(Function(V As FontCacheElement) (V.Direct3DFont.Disposed))

  End Sub

 End Structure

End Namespace
