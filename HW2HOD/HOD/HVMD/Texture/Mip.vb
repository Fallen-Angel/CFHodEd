Partial Class Texture
 ''' <summary>
 ''' Class representing a single mip in a texture.
 ''' </summary>
 Private NotInheritable Class Mip
  ' --------------
  ' Class Members.
  ' --------------
  ''' <summary>Width of this mip.</summary>
  Private m_Width As Integer

  ''' <summary>Height of this mip.</summary>
  Private m_Height As Integer

  ''' <summary>Data contained in this mip.</summary>
  Private m_Data() As Byte

  ' ------------------------
  ' Constructors\Finalizers.
  ' ------------------------
  ''' <summary>
  ''' Class constructor.
  ''' </summary>
  Public Sub New()
   ' Nothing here.

  End Sub

  ''' <summary>
  ''' Copy constructor.
  ''' </summary>
  Public Sub New(ByVal m As Mip)
   m_Width = m.m_Width
   m_Height = m.m_Height

   If m.m_Data Is Nothing Then _
    Exit Sub

   ' Make empty array if needed.
   If m.m_Data.Length = 0 Then _
    ReDim m_Data(-1) _
  : Exit Sub

   ' Allocate space.
   ReDim m_Data(m.m_Data.Length)

   ' Copy data.
   m.m_Data.CopyTo(m_Data, 0)

  End Sub

  ' -----------------
  ' Class properties.
  ' -----------------
  ''' <summary>
  ''' Returns the width of this mip.
  ''' </summary>
  Public ReadOnly Property Width() As Integer
   Get
    Return m_Width

   End Get

  End Property

  ''' <summary>
  ''' Returns the height of this mip.
  ''' </summary>
  Public ReadOnly Property Height() As Integer
   Get
    Return m_Height

   End Get

  End Property

  ''' <summary>
  ''' Returns the size of data of this mip, in bytes.
  ''' </summary>
  Public ReadOnly Property DataSize() As Integer
   Get
    If (m_Data Is Nothing) Then _
     Return 0

    Return m_Data.Length

   End Get

  End Property

  ' -----------------
  ' Member Functions.
  ' -----------------
  ''' <summary>
  ''' Calculates mip size for a given texture.
  ''' </summary>
  ''' <param name="Width">
  ''' Width of texture.
  ''' </param>
  ''' <param name="Height">
  ''' Height of texture.
  ''' </param>
  ''' <param name="Mip">
  ''' Mip number.
  ''' Mip 1 is the texture itself (Width x Height),
  ''' Mip 2 is (Width / 2 x Height / 2 ),
  ''' Mip 3 is (Width / 4 x Height / 4 ), 
  ''' and so on.
  ''' </param>
  ''' <param name="FourCC">
  ''' Four character code of the texture. Possible values are "8888",
  ''' "DXT1", "DXT3" and "DXT5".
  ''' </param>
  Private Shared Function Size(ByVal Width As Integer, ByVal Height As Integer, _
                               ByVal Mip As Integer, ByRef FourCC As String) As Integer

   If (Width < 0) OrElse (Height < 0) Then _
    Throw New ArgumentException("Invalid dimension(s).") _
  : Return 0

   If (Mip <= 0) Then _
    Throw New ArgumentOutOfRangeException("Mip") _
  : Return 0

   If (FourCC Is Nothing) OrElse (FourCC.Length <> 4) Then _
    Throw New ArgumentException("Invalid 'FourCC'.") _
  : Return 0

   Select Case FourCC
    ' TGA 32-bit R8G8B8A8 format.
    Case "8888"
     Return Math.Max(1, Width >> (Mip - 1)) * _
            Math.Max(1, Height >> (Mip - 1)) * 4

     ' Formula for mip size:
     ' max(1,[width of mip]/4)*max(1,[height of mip]/4)*((szFourCC == "DXT1")?(8):(16))
     ' which would turn out as: Dimension / (2 ^ (Mip - 1)) / 4
     ' or: Dimension / (2 ^ (Mip + 1))
    Case "DXT1"
     Return Math.Max(1, Width >> (Mip + 1)) * _
            Math.Max(1, Height >> (Mip + 1)) * 8

    Case Else
     Return Math.Max(1, Width >> (Mip + 1)) * _
            Math.Max(1, Height >> (Mip + 1)) * 16

   End Select

  End Function

  ''' <summary>
  ''' Reads the mip from an IFF file.
  ''' </summary>
  ''' <param name="IFF">
  ''' The IFF reader to read from.
  ''' </param>
  ''' <param name="FourCC">
  ''' Four character code of mip.
  ''' </param>
  Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader, ByVal FourCC As String)
   ' Uppercase the FourCC.
   FourCC.ToUpper()

   If (FourCC <> "DXT1") AndAlso (FourCC <> "DXT3") AndAlso _
      (FourCC <> "DXT5") AndAlso (FourCC <> "8888") Then _
    Throw New ArgumentException("Invalid FourCC.") _
  : Exit Sub

   ' Erase old data if needed.
   Erase m_Data

   ' Read dimensions.
   m_Width = IFF.ReadInt32()
   m_Height = IFF.ReadInt32()

   ' Read data.
   m_Data = IFF.ReadBytes(Mip.Size(m_Width, m_Height, 1, FourCC))

   ' Perform RGBA -> ARGB
   If FourCC = "8888" Then
    For I As Integer = 0 To m_Data.Length - 1 Step 4
     ' RGBA
     Dim A As Byte = m_Data(I), _
         B As Byte = m_Data(I + 1), _
         G As Byte = m_Data(I + 2), _
         R As Byte = m_Data(I + 3)

     ' ARGB
     m_Data(I) = B
     m_Data(I + 1) = G
     m_Data(I + 2) = R
     m_Data(I + 3) = A

    Next I ' For I As Integer = 0 To m_Data.Length - 1 Step 4
   End If ' If FourCC = "8888" Then

  End Sub

  ''' <summary>
  ''' Writes the mip to an IFF file.
  ''' </summary>
  ''' <param name="IFF">
  ''' The IFF writer to write to.
  ''' </param>
  ''' <param name="FourCC">
  ''' Four character code of mip.
  ''' </param>
  Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter, ByVal FourCC As String)
   ' Uppercase the FourCC.
   FourCC.ToUpper()

   If (FourCC <> "DXT1") AndAlso (FourCC <> "DXT3") AndAlso _
      (FourCC <> "DXT5") AndAlso (FourCC <> "8888") Then _
    Throw New ArgumentException("Invalid FourCC.") _
  : Exit Sub

   ' Write dimensions.
   IFF.WriteInt32(m_Width)
   IFF.WriteInt32(m_Height)

   If FourCC <> "8888" Then
    ' Directly write data.
    IFF.Write(m_Data)

   Else ' If FourCC <> "8888" Then
    ' Resize array.
    Dim data(m_Data.Length - 1) As Byte

    ' Perform ARGB -> RGBA.
    For I As Integer = 0 To data.Length - 1 Step 4
     ' ARGB
     Dim B As Byte = m_Data(I), _
         G As Byte = m_Data(I + 1), _
         R As Byte = m_Data(I + 2), _
         A As Byte = m_Data(I + 3)

     ' RGBA
     data(I) = A
     data(I + 1) = B
     data(I + 2) = G
     data(I + 3) = R

    Next I ' For I As Integer = 0 To data.Length - 1 Step 4

    ' Write data.
    IFF.Write(data)

    ' Erase temp array.
    Erase data

   End If ' If FourCC <> "8888" Then

  End Sub

  ''' <summary>
  ''' Reads the mip from a binary reader.
  ''' </summary>
  ''' <param name="BR">
  ''' The binary reader to read from.
  ''' </param>
  ''' <param name="_Width">
  ''' Width of mip.
  ''' </param>
  ''' <param name="_Height">
  ''' Height of mip.
  ''' </param>
  ''' <param name="FourCC">
  ''' Four character code of mip.
  ''' </param>
  ''' <remarks>
  ''' This does not read dimensions.
  ''' </remarks>
  ''' <exception cref="ArgumentException">
  ''' Thrown when invalid dimensions are supplied.
  ''' </exception>
  Friend Sub Read(ByVal BR As IO.BinaryReader, ByVal _Width As Integer, ByVal _Height As Integer, ByVal FourCC As String)
   ' Uppercase the FourCC.
   FourCC.ToUpper()

   If (_Width < 0) OrElse (_Height < 0) Then _
    Throw New ArgumentException("Invalid dimension(s).") _
  : Exit Sub

   If (FourCC <> "DXT1") AndAlso (FourCC <> "DXT3") AndAlso _
      (FourCC <> "DXT5") AndAlso (FourCC <> "8888") Then _
    Throw New ArgumentException("Invalid FourCC.") _
  : Exit Sub

   ' Erase old data.
   m_Width = 0
   m_Height = 0
   Erase m_Data

   ' Set dimensions.
   m_Width = _Width
   m_Height = _Height

   ' Read data.
   m_Data = BR.ReadBytes(Mip.Size(_Width, _Height, 1, FourCC))

  End Sub

  ''' <summary>
  ''' Writes the mip to a binary writer.
  ''' </summary>
  ''' <param name="BW">
  ''' The binary writer to write to.
  ''' </param>
  ''' <remarks>
  ''' Does not write dimensions.
  ''' </remarks>
  Friend Sub Write(ByVal BW As IO.BinaryWriter)
   ' Write data.
   BW.Write(m_Data)

  End Sub

 End Class

End Class
