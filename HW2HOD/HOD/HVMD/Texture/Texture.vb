''' <summary>
''' Class representing a Homeworld2 texture.
''' </summary>
Public NotInheritable Class Texture
 ' --------------
 ' Class Members.
 ' --------------
 ''' <summary>
 ''' Full texture path. Used for texture sharing in-game.
 ''' </summary>
 Private m_Path As String

 ''' <summary>
 ''' Texture format.
 ''' </summary>
 Private m_FourCC As String

 ''' <summary>
 ''' Mips in texture.
 ''' </summary>
 Private m_Mips As New List(Of Mip)

 ''' <summary>
 ''' Direct3D texture.
 ''' </summary>
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
 Public Sub New(ByVal t As Texture)
  ' Call default constructor first.
  m_Path = t.m_Path
  m_FourCC = t.m_FourCC
  m_Texture = Nothing

  For I As Integer = 0 To t.m_Mips.Count - 1
   m_Mips.Add(New Mip(t.m_Mips(I)))

  Next I ' For I As Integer = 0 To t.m_Mips.Count - 1

 End Sub

 ''' <summary>
 ''' Class finalizer.
 ''' </summary>
 Protected Overrides Sub Finalize()
  ' Discard texture.
  If m_Texture IsNot Nothing Then _
   m_Texture.Dispose() _
 : m_Texture = Nothing

  MyBase.Finalize()

 End Sub

 ' -----------------
 ' Class properties.
 ' -----------------
 ''' <summary>
 ''' Returns the four character code of the format of this texture.
 ''' </summary>
 Public ReadOnly Property FourCC() As String
  Get
   Return m_FourCC

  End Get

 End Property

 ''' <summary>
 ''' Returns the number of mips in this texture.
 ''' </summary>
 Public ReadOnly Property NumMips() As Integer
  Get
   Return m_Mips.Count

  End Get

 End Property

 ''' <summary>
 ''' Returns the width of this texture.
 ''' </summary>
 Public ReadOnly Property Width() As Integer
  Get
   If m_Mips.Count = 0 Then _
    Return 0

   Return m_Mips(0).Width

  End Get

 End Property

 ''' <summary>
 ''' Returns the width of this texture.
 ''' </summary>
 Public ReadOnly Property Height() As Integer
  Get
   If m_Mips.Count = 0 Then _
    Return 0

   Return m_Mips(0).Height

  End Get

 End Property

 ''' <summary>
 ''' Returns\Sets the texture path. This is used for texture sharing,
 ''' in-game.
 ''' </summary>
 Public Property Path() As String
  Get
   Return m_Path

  End Get

  Set(ByVal value As String)
   If (value Is Nothing) OrElse (value = "") Then _
    Throw New ArgumentNullException("value") _
  : Exit Property

   m_Path = value

  End Set

 End Property

 ''' <summary>
 ''' Direct3D texture.
 ''' </summary>
 Friend ReadOnly Property Texture() As Direct3D.Texture
  Get
   Return m_Texture

  End Get

 End Property

 ' -----------------
 ' Member Functions.
 ' -----------------
 ''' <summary>
 ''' Returns the name of this texture.
 ''' </summary>
 Public Overrides Function ToString() As String
  Return IO.Path.GetFileName(m_Path)

 End Function

 ''' <summary>
 ''' Reads a texture to this object.
 ''' </summary>
 ''' <param name="BR">
 ''' The binary reader to use.
 ''' </param>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when <c>BR Is Nothing</c>.
 ''' </exception>
 Public Sub Read(ByVal BR As IO.BinaryReader)
  ' Check whether we have a valid strean.
  If (BR Is Nothing) OrElse (Not BR.BaseStream.CanRead) OrElse (Not BR.BaseStream.CanSeek) Then _
   Throw New ArgumentNullException("BR") _
 : Exit Sub

  ' See if it has enough data.
  If BR.BaseStream.Length - BR.BaseStream.Position < 4 Then _
   Trace.TraceError("Invalid texture; cannot identify format.") _
 : Exit Sub

  ' Read the magic string.
  Dim magic As String = __ReadASCIIString(BR, 4)

  ' Seek to begining.
  BR.BaseStream.Position -= 4

  ' See if it's a DDS.
  If magic = "DDS " Then _
   ReadDDS(BR) _
  Else _
   ReadTGA(BR)

 End Sub

 ''' <summary>
 ''' Writes this object to a texture.
 ''' </summary>
 ''' <param name="BW">
 ''' The binary writer to use for writing the surface.
 ''' </param>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when <c>BW Is Nothing</c>.
 ''' </exception>
 ''' <exception cref="Exception">
 ''' Thrown when the format or number of mips is invalid. In other words,
 ''' the texture is not initialized, either by assigning a surface to it,
 ''' or by reading from a HOD.
 ''' </exception>
 ''' <remarks>
 ''' The output texture is a TGA iff the FourCC is 8888 and there is only one mip.
 ''' Otherwise, the output texture is a DDS.
 ''' </remarks>
 Public Sub Write(ByVal BW As IO.BinaryWriter)
  If (BW Is Nothing) OrElse (Not BW.BaseStream.CanWrite) Then _
   Throw New ArgumentNullException("BW") _
 : Exit Sub

  If (m_Mips.Count <= 0) Then _
   Throw New Exception("Texture has no mips.") _
 : Exit Sub

  Select Case m_FourCC
   Case "8888", "DXT1", "DXT3", "DXT5"
    ' If the image is 8888 and there is only one mip then write as TGA,
    ' else as DDS. 
    If (m_FourCC = "8888") AndAlso (m_Mips.Count = 1) Then _
     WriteTGA(BW) _
    Else _
     WriteDDS(BW)

   Case Else
    Throw New Exception("Invalid texture format.")
    Exit Sub

  End Select ' Select Case m_FourCC

 End Sub

 ''' <summary>
 ''' Reads a DDS texture.
 ''' </summary>
 Private Sub ReadDDS(ByVal BR As IO.BinaryReader)
  Dim FourCC As String

  ' Seek to read height and width.
  BR.BaseStream.Position += 12

  ' Read height and width.
  Dim Height As Integer = BR.ReadInt32()
  Dim Width As Integer = BR.ReadInt32()

  ' Seek to read mip count.
  BR.BaseStream.Position += 8

  ' Read mip count.
  Dim NumMips As Integer = BR.ReadInt32()

  ' There is always 1 mip.
  If NumMips = 0 Then _
   NumMips = 1

  ' Seek to read pixel format.
  BR.BaseStream.Position += 48

  ' Read pixel format flags.
  Dim pfFlags As Integer = BR.ReadInt32()

  ' Read appropriate fields.
  Select Case pfFlags
   Case &H4 ' FourCC
    ' Read pixel format.
    FourCC = __ReadASCIIString(BR, 4)

    Select Case FourCC
     Case "DXT1", "DXT3", "DXT5"
      ' Do nothing.

     Case Else
      Trace.TraceError("Unknown pixel format: " & FourCC)
      Exit Sub

    End Select ' Select Case PixelFormat

    ' Seek to read first mip.
    BR.BaseStream.Position += 40

   Case &H41 ' RGBA
    ' Skip FourCC.
    BR.BaseStream.Position += 4

    ' Read bit count and masks.
    Dim bitCount As Integer = BR.ReadInt32()
    Dim maskR As Integer = BR.ReadInt32()
    Dim maskG As Integer = BR.ReadInt32()
    Dim maskB As Integer = BR.ReadInt32()
    Dim maskA As Integer = BR.ReadInt32()

    ' Check fields.
    If (bitCount <> 32) OrElse (maskR <> &HFF0000) OrElse (maskG <> &HFF00) OrElse _
       (maskB <> &HFF) OrElse (maskA <> &HFF000000) Then _
     Trace.TraceError("DDS is not A8R8G8B8 format.") _
   : Exit Sub

    ' Skip to read first mip.
    BR.BaseStream.Position += 20

    ' Update FourCC.
    FourCC = "8888"

   Case Else
    Trace.TraceError("DDS is neither A8R8G8B8 nor DXTn format.")
    Exit Sub

  End Select ' Select Case pfFlags
  
  ' Allocate new list for mips in case read fails.
  Dim Mips As New List(Of Mip)

  ' Read all mips.
  For I As Integer = 1 To NumMips
   Dim Mip As New Mip

   ' Set data.
   Mip.Read(BR, Width, Height, FourCC)

   ' Add to list.
   Mips.Add(Mip)

   ' Halve dimensions.
   Width >>= 1
   Height >>= 1

  Next I ' For I As Integer = 1 To NumMips

  ' Check for EOF.
  If BR.BaseStream.Position <> BR.BaseStream.Length Then _
   Trace.TraceError("Texture seems to be invalid!") _
 : Exit Sub

  ' Initialize texture.
  Initialize()

  ' Add mips and clear temp list.
  m_Mips.AddRange(Mips)
  Mips.Clear()

  ' Set FourCC.
  m_FourCC = FourCC

 End Sub

 ''' <summary>
 ''' Writes as DDS texture to file.
 ''' </summary>
 Private Sub WriteDDS(ByVal BW As IO.BinaryWriter)
  ' Write magic word.
  __WriteASCIIString(BW, "DDS ")

  ' Write header.
  ' Structure size.
  BW.Write(CType(124, Int32))

  ' Flags: 0x1, 0x2, 0x4, 0x1000, 0x20000.
  BW.Write(CType(&H21007, Int32))

  ' Height, width, linear size, depth, mip count.
  BW.Write(CType(m_Mips(0).Height, Int32))
  BW.Write(CType(m_Mips(0).Width, Int32))
  BW.Write(CType(0, Int32))
  BW.Write(CType(0, Int32))
  BW.Write(CType(m_Mips.Count, Int32))

  ' Reserved block of 44 bytes.
  BW.Write(CType(0, Int64))
  BW.Write(CType(0, Int64))
  BW.Write(CType(0, Int64))
  BW.Write(CType(0, Int64))
  BW.Write(CType(0, Int64))
  BW.Write(CType(0, Int32))

  ' Pixel format.
  ' Structure size.
  BW.Write(CType(32, Int32))

  Select Case m_FourCC
   Case "DXT1", "DXT3", "DXT5"
    ' Flags: 0x4
    BW.Write(CType(&H4, Int32))

    ' FourCC.
    __WriteASCIIString(BW, m_FourCC)

    ' RGB bit count, R, G, B, A bit masks.
    ' All zero, 20 bytes.
    BW.Write(CType(0, Int64))
    BW.Write(CType(0, Int64))
    BW.Write(CType(0, Int32))

   Case "8888"
    ' Flags: 0x1, 0x40
    BW.Write(CType(&H41, Int32))

    ' FourCC.
    BW.Write(CType(0, Int32))

    ' RGB bit count, R, G, B, A bit masks.
    BW.Write(CType(32, Int32))
    BW.Write(CType(&HFF0000, Int32))
    BW.Write(CType(&HFF00, Int32))
    BW.Write(CType(&HFF, Int32))
    BW.Write(CType(&HFF000000, Int32))

   Case Else
    Throw New Exception("Invalid FourCC code.")
    Exit Sub

  End Select ' Select Case m_FourCC

  ' Caps1: 0x1000, 0x8, 0x400000.
  If m_Mips.Count = 1 Then _
   BW.Write(CType(&H1000, Int32)) _
  Else _
   BW.Write(CType(&H401008, Int32))

  ' Caps2, Caps3, Caps4 all zero.
  ' 12 bytes of zeros.
  BW.Write(CType(0, Int32))
  BW.Write(CType(0, Int64))

  ' Reserved.
  ' 4 bytes of zeros.
  BW.Write(CType(0, Int32))

  ' Primary and additional surfaces.
  For I As Integer = 0 To m_Mips.Count - 1
   m_Mips(I).Write(BW)

  Next I ' For I As Integer = 0 To m_Mips.Count - 1

 End Sub

 ''' <summary>
 ''' Reads a TGA texture.
 ''' </summary>
 Private Sub ReadTGA(ByVal BR As IO.BinaryReader)
  ' Read TGA header.
  Dim lenID As Integer = BR.ReadByte()
  Dim colMapType As Integer = BR.ReadByte()
  Dim imageType As Integer = BR.ReadByte()

  ' Skip colour map portion.
  BR.BaseStream.Position += 5

  ' This doesn't need to be read...
  Dim originX As Integer = BR.ReadInt16()
  Dim originY As Integer = BR.ReadInt16()

  ' Read dimensions.
  Dim Width As Integer = BR.ReadUInt16()
  Dim Height As Integer = BR.ReadUInt16()

  ' Read last fields.
  Dim pixelDepth As Integer = BR.ReadByte()
  Dim imageDescriptor As Integer = BR.ReadByte()

  ' Read ID field.
  BR.BaseStream.Position += lenID

  ' Check if it has a palette.
  If colMapType <> 0 Then _
   Trace.TraceError("Colour-mapped TGA files are not supported.") _
 : Exit Sub

  ' Check if it is true colour.
  If imageType <> 2 Then _
   Trace.TraceError("The TGA file is not an uncompressed true colour image.") _
 : Exit Sub

  ' Check depth.
  If pixelDepth <> 32 Then _
   Trace.TraceError("The TGA file does not have 32 bits per pixel.") _
 : Exit Sub

  ' Check alpha bits count.
  If (imageDescriptor And &HF) <> 8 Then _
   Trace.TraceError("The TGA file doesn't have an 8-bit alpha channel.") _
 : Exit Sub

  ' See if the image has to be flipped.
  Dim flipX As Boolean = ((imageDescriptor And 16) <> 0)
  Dim flipY As Boolean = ((imageDescriptor And 32) = 0)

  ' Allocate pixels.
  Dim data(4 * Width * Height - 1) As Byte

  ' Read pixels.
  For J As Integer = 0 To Height - 1
   For I As Integer = 0 To Width - 1
    Dim X As Integer = I, _
        Y As Integer = J

    ' Flip X if needed.
    If flipX Then _
     X = Width - (X + 1)

    ' Flip Y if needed.
    If flipY Then _
     Y = Height - (Y + 1)

    ' Calculate offset.
    Dim dest As Integer = 4 * (Y * Width + X)

    ' Read pixel.
    data(dest) = BR.ReadByte()
    data(dest + 1) = BR.ReadByte()
    data(dest + 2) = BR.ReadByte()
    data(dest + 3) = BR.ReadByte()

   Next I ' For I As Integer = 0 To Width - 1
  Next J ' For J As Integer = 0 To Height - 1

  ' Initialize new stream.
  Dim stream As New IO.MemoryStream(data, False)

  ' Initialize new reader.
  Dim MBR As New IO.BinaryReader(stream)

  ' Make new mip.
  Dim m As New Mip

  ' Read data to mip object.
  m.Read(MBR, Width, Height, "8888")

  ' Clear array.
  MBR.Close()
  stream.Dispose()
  Erase data

  ' Initialize texture.
  Initialize()

  ' Add mip.
  m_Mips.Add(m)

  ' Set FourCC.
  m_FourCC = "8888"

 End Sub

 ''' <summary>
 ''' Writes as TGA texture to file.
 ''' </summary>
 Private Sub WriteTGA(ByVal BW As IO.BinaryWriter)
  ' Header.
  ' File ID length, colour map type, image type.
  BW.Write(CType(0, Byte))
  BW.Write(CType(0, Byte))
  BW.Write(CType(2, Byte))

  ' Colour map specification, 5 bytes, all zeros.
  BW.Write(CType(0, Byte))
  BW.Write(CType(0, Int32))

  ' Image specification.
  ' Origin X, origin Y, width, height, pixel depth, image descriptor.
  BW.Write(CType(0, Int16))
  BW.Write(CType(0, Int16))
  BW.Write(CType(m_Mips(0).Width, Int16))
  BW.Write(CType(m_Mips(0).Height, Int16))
  BW.Write(CType(32, Byte))
  BW.Write(CType(40, Byte))

  ' Image data.
  m_Mips(0).Write(BW)

  ' Footer.
  ' Offsets to extension and developer areas,
  ' 8 bytes, all zero.
  BW.Write(CType(0, Int64))

  ' Signature
  __WriteASCIIString(BW, "TRUEVISION-XFILE.")

  ' Null
  BW.Write(CType(0, Byte))

 End Sub

 ''' <summary>
 ''' Reads a texture from an IFF reader.
 ''' </summary>
 ''' <param name="IFF">
 ''' IFF reader to read from.
 ''' </param>
 ''' <param name="ChunkAttributes">
 ''' Chunk attributes.
 ''' </param>
 Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
  ' Erase old data.
  Initialize()

  ' Read texture path.
  m_Path = IFF.ReadString()

  ' Read texture format.
  m_FourCC = IFF.ReadString(4)

  ' Get mip count.
  Dim MipCount As Integer = IFF.ReadInt32()

  For I As Integer = 1 To MipCount
   Dim Mip As New Mip
   Mip.ReadIFF(IFF, m_FourCC)
   m_Mips.Add(Mip)

  Next I ' For I As Integer = 1 To MipCount

 End Sub

 ''' <summary>
 ''' Writes the texture to an IFF writer.
 ''' </summary>
 ''' <param name="IFF">
 ''' IFF writer to write to.
 ''' </param>
 Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
  IFF.Push("LMIP")

  ' Write texture path.
  IFF.Write(m_Path)

  ' Write texture format.
  IFF.Write(m_FourCC, 4)

  ' Write mip count.
  IFF.WriteInt32(m_Mips.Count)

  For I As Integer = 0 To m_Mips.Count - 1
   m_Mips(I).WriteIFF(IFF, m_FourCC)

  Next I ' For I As Integer = 0 To m_Mips.Count - 1

  IFF.Pop()

 End Sub

 ''' <summary>
 ''' Initializes the texture.
 ''' </summary>
 Private Sub Initialize()
  m_Path = "no path"
  m_FourCC = "8888"
  m_Mips.Clear()

  ' Discard texture.
  If m_Texture IsNot Nothing Then _
   m_Texture.Dispose() _
 : m_Texture = Nothing

 End Sub

 ''' <summary>
 ''' Updates the texture referenced by this object.
 ''' </summary>
 Friend Sub Update(ByVal Device As Direct3D.Device)
  ' If the texture is already present, then exit.
  If m_Texture IsNot Nothing Then _
   Exit Sub

  ' If the texture has no mips, do nothing.
  If m_Mips.Count = 0 Then _
   Exit Sub

  ' Prepare a new memory stream and binary writer.
  Dim TexStream As New IO.MemoryStream
  Dim BW As New IO.BinaryWriter(TexStream)

  ' Write the texture to stream.
  Write(BW)

  ' Reposition to begining.
  TexStream.Position = 0

  ' Read the texture using Direct3D and convert to surface.
  m_Texture = Direct3D.TextureLoader.FromStream(Device, TexStream)

  ' Relase the stream and writer.
  BW.Close()
  TexStream.Dispose()

 End Sub

 ''' <summary>
 ''' Reads an ASCII string.
 ''' </summary>
 Private Function __ReadASCIIString(ByVal BR As IO.BinaryReader, ByVal len As Integer) As String
  Dim out As String = ""

  For I As Integer = 1 To len
   out &= Chr(BR.ReadByte())

  Next I ' For I As Integer = 1 To len

  Return out

 End Function

 ''' <summary>
 ''' Writes an ASCII string.
 ''' </summary>
 Private Sub __WriteASCIIString(ByVal BW As IO.BinaryWriter, ByVal str As String)
  For I As Integer = 0 To str.Length - 1
   BW.Write(CByte(&HFF And Asc(str(I))))

  Next I ' For I As Integer = 0 To str.Length - 1

 End Sub

End Class
