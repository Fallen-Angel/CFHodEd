Partial Class HOD
 ''' <summary>HOD owner.</summary>
 Private m_Owner As String

 ''' <summary>Warnings in the HOD, by the HW2 Maya exporter.</summary>
 Private m_Warnings(-1) As Byte

 ''' <summary>Errors in the HOD, by the HW2 Maya exporter.</summary>
 Private m_Errors(-1) As Byte

 ''' <summary>
 ''' Returns\Sets owner of HOD.
 ''' </summary>
 Public Property Owner() As String
  Get
   Return m_Owner

  End Get

  Set(ByVal value As String)
   m_Owner = value

  End Set

 End Property

 ''' <summary>
 ''' Reads the INFO chunk from an IFF reader.
 ''' </summary>
 ''' <param name="IFF">
 ''' IFF reader to read from.
 ''' </param>
 ''' <param name="ChunkAttributes">
 ''' Attributes of the chunk.
 ''' </param>
 Private Sub ReadINFOChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
  ' Add handlers.
  IFF.AddHandler("WARN", Homeworld2.IFF.ChunkType.Default, AddressOf ReadWARNChunk)
  IFF.AddHandler("ERRR", Homeworld2.IFF.ChunkType.Default, AddressOf ReadERRRChunk)
  IFF.AddHandler("OWNR", Homeworld2.IFF.ChunkType.Default, AddressOf ReadOWNRChunk)

  ' Parse the HOD.
  IFF.Parse()

 End Sub

 ''' <summary>
 ''' Reads the WARN chunk from an IFF reader.
 ''' </summary>
 ''' <param name="IFF">
 ''' IFF reader to read from.
 ''' </param>
 ''' <param name="ChunkAttributes">
 ''' Attributes of the chunk.
 ''' </param>
 Private Sub ReadWARNChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
  Dim count As Integer = IFF.ReadInt32()

  If count <> 0 Then _
   m_Warnings = IFF.ReadBytes(count)

 End Sub

 ''' <summary>
 ''' Reads the ERRR chunk from an IFF reader.
 ''' </summary>
 ''' <param name="IFF">
 ''' IFF reader to read from.
 ''' </param>
 ''' <param name="ChunkAttributes">
 ''' Attributes of the chunk.
 ''' </param>
 Private Sub ReadERRRChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
  Dim count As Integer = IFF.ReadInt32()

  If count <> 0 Then _
   m_Errors = IFF.ReadBytes(count)

 End Sub

 ''' <summary>
 ''' Reads the OWNR chunk from an IFF reader.
 ''' </summary>
 ''' <param name="IFF">
 ''' IFF reader to read from.
 ''' </param>
 ''' <param name="ChunkAttributes">
 ''' Attributes of the chunk.
 ''' </param>
 Private Sub ReadOWNRChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
  m_Owner = IFF.ReadString()

 End Sub

 Private Sub WriteINFOChunk(ByVal IFF As IFF.IFFWriter)
  ' Do we need to write the INFO chunk?
  If (m_Warnings.Length = 0) AndAlso (m_Errors.Length = 0) AndAlso (m_Owner.Length = 0) Then _
   Exit Sub

  IFF.Push("INFO", Homeworld2.IFF.ChunkType.Form)

  If m_Warnings.Length <> 0 Then
   IFF.Push("WARN")
   IFF.WriteInt32(m_Warnings.Length)

   For I As Integer = 0 To m_Warnings.Length - 1
    IFF.Write(m_Warnings(I))

   Next I ' For I As Integer = 0 To m_Warnings.Length - 1

   IFF.Pop()

  End If ' If m_Warnings.Length <> 0 Then

  If m_Errors.Length <> 0 Then
   IFF.Push("ERRR")
   IFF.WriteInt32(m_Errors.Length)

   For I As Integer = 0 To m_Errors.Length - 1
    IFF.Write(m_Errors(I))

   Next I ' For I As Integer = 0 To m_Errors.Length - 1

   IFF.Pop()

  End If ' If m_Errors.Length <> 0 Then

  If m_Owner.Length <> 0 Then
   IFF.Push("OWNR")

   IFF.Write(m_Owner)

   IFF.Pop()

  End If ' If m_Owner.Length <> 0 Then

  IFF.Pop()

 End Sub

 ''' <summary>
 ''' Initializes the INFO chunk of the HOD.
 ''' </summary>
 Private Sub InitializeINFO()
  m_Owner = ""

  ReDim m_Warnings(-1)
  ReDim m_Errors(-1)

 End Sub

End Class