''' <summary>
''' Class that can read data from Homeworld2 IFF files.
''' </summary>
Public NotInheritable Class IFFReader
 Inherits IO.BinaryReader

 ''' <summary>List of chunks read.</summary>
 Private m_ChunkList As List(Of Chunk)

 ''' <summary>Handler for chunks.</summary>
 Private m_Handlers As HandlerList

 ''' <summary>
 ''' Class constructor.
 ''' </summary>
 ''' <param name="input">
 ''' Input stream to use for reading.
 ''' </param>
 Public Sub New(ByVal input As IO.Stream)
  ' Call base constructor.
  MyBase.New(input)

  ' Initialize our objects.
  m_ChunkList = New List(Of Chunk)
  m_Handlers = New HandlerList(Me)
  m_Handlers.DefaultHandler = AddressOf __defaultChunkHandler

 End Sub

 ''' <summary>
 ''' Returns\Sets the default handler.
 ''' </summary>
 Public Property DefaultHandler() As ChunkHandler
  Get
   Return m_Handlers.DefaultHandler

  End Get

  Set(ByVal value As ChunkHandler)
   m_Handlers.DefaultHandler = value

  End Set

 End Property

 ''' <summary>
 ''' Opens a file for reading.
 ''' </summary>
 ''' <param name="Filename">
 ''' File to open.
 ''' </param>
 ''' <returns>
 ''' The object if successful, <c>Nothing</c> otherwise.
 ''' </returns>
 Public Shared Function Open(ByVal Filename As String) As IFFReader
  Dim Reader As IFFReader

  ' Try to open the file.
  Try
   ' Try to create a binary reader.
   Reader = New IFFReader(IO.File.OpenRead(Filename))

  Catch ex As Exception
   Return Nothing

  End Try

  Return Reader

 End Function

 ''' <summary>
 ''' Adds a handler for the specified chunk.
 ''' </summary>
 ''' <param name="ID">
 ''' ID of the chunk.
 ''' </param>
 ''' <param name="Type">
 ''' Type of chunk.
 ''' </param>
 ''' <param name="Version">
 ''' Version of the chunk
 ''' </param>
 Public Sub [AddHandler](ByVal ID As String, ByVal Type As ChunkType, ByVal Handler As ChunkHandler, _
                Optional ByVal Version As UInteger = 0)

  m_Handlers.AddHandler(ID, Type, Handler, Version)

 End Sub

 ''' <summary>
 ''' Finds a handler for the specified type of chunk.
 ''' </summary>
 ''' <param name="ID">
 ''' ID of the chunk.
 ''' </param>
 ''' <param name="Type">
 ''' Type of chunk.
 ''' </param>
 ''' <param name="Version">
 ''' Version of the chunk
 ''' </param>
 ''' <returns>
 ''' The handler if set, or nothing.
 ''' </returns>
 Friend Function FindHandler(ByVal ID As String, ByVal Type As ChunkType, _
                    Optional ByVal Version As UInteger = 0) As ChunkHandler

  Return m_Handlers.FindHandler(ID, Type, Version)

 End Function

 ''' <summary>
 ''' Parses (i.e. reads) the file.
 ''' </summary>
 ''' <param name="FromBegining">
 ''' Set to <c>True</c> to reset position of stream to 0.
 ''' Set to <c>False</c> to avoid this change.
 ''' </param>
 Public Sub Parse(Optional ByVal FromBegining As Boolean = False)
  ' Clear existing chunks if present.
  If m_ChunkList.Count <> 0 Then _
   m_ChunkList.Clear()

  ' Go to the file begining.
  If (FromBegining) AndAlso (BaseStream.Position <> 0) Then _
   BaseStream.Position = 0

  ' Read chunks until end of file.
  While BaseStream.Position < BaseStream.Length
   Dim chunk As New Chunk(Me)

   ' Get the position.
   chunk.StartPosition = BaseStream.Position

   ' Get the ID.
   chunk.ID = ReadString(4)

   ' Get the size.
   chunk.Size = __swap(ReadUInt32())

   ' See what type it is.
   Select Case chunk.ID
    ' NORMAL
    Case "NRML"
     chunk.Type = ChunkType.Normal

     ' Get the real ID.
     chunk.ID = ReadString(4)

     ' Get the version.
     chunk.Version = __swap(ReadUInt32())

     ' Subtract size of 'NRML' head and version.
     chunk.Size -= 8

     ' FORM
    Case "FORM"
     chunk.Type = ChunkType.Form

     ' Get the real ID.
     chunk.ID = ReadString(4)

     ' Subtract size of 'FORM' head.
     chunk.Size -= 4

     ' DEFAULT
    Case Else
     ' Nothing to do here.

   End Select ' Select Case chunk.ID

   ' Check chunk length.
   If BaseStream.Position + chunk.Size > BaseStream.Length Then _
    Trace.TraceError("Input IFF file has an invalid chunk! (""" & chunk.ID & """)") _
  : Exit While

   ' Add the chunk.
   m_ChunkList.Add(chunk)

   ' Read it.
   chunk.Read()

  End While ' While BaseStream.Position < BaseStream.Length

 End Sub

 ''' <summary>
 ''' Reads a fixed length string from the stream.
 ''' </summary>
 ''' <param name="Length">
 ''' Length of string.
 ''' </param>
 ''' <returns>
 ''' A string read from the stream.
 ''' </returns>
 ''' <exception cref="IO.IOException">
 ''' An I/O error occurs.
 ''' </exception>
 ''' <exception cref="IO.EndOfStreamException">
 ''' The end of the stream is reached.
 ''' </exception>
 ''' <exception cref="ObjectDisposedException">
 ''' The stream is closed.
 ''' </exception>
 ''' <exception cref="ArgumentOutOfRangeException">
 ''' <c>Length</c> is negative.
 ''' </exception>
 Public Overloads Function ReadString(ByVal Length As Integer) As String
  Dim str As Byte(), outStr As String = ""

  ' Read the string.
  str = ReadBytes(Length)

  ' Build the output string.
  If Length <> 0 Then
   For I As Integer = 0 To CInt(Length) - 1
    outStr &= Chr(str(I))

   Next I ' For I As Integer = 0 To CInt(Length) - 1
  End If ' If Length <> 0 Then

  ' Return it.
  Return outStr

 End Function

 ''' <summary>
 ''' Reads a string from the stream.
 ''' </summary>
 ''' <returns>
 ''' A string read from the stream.
 ''' </returns> 
 ''' <exception cref="IO.IOException">
 ''' An I/O error occurs.
 ''' </exception>
 ''' <exception cref="IO.EndOfStreamException">
 ''' The end of the stream is reached.
 ''' </exception>
 ''' <exception cref="ObjectDisposedException">
 ''' The stream is closed.
 ''' </exception> 
 Public Overrides Function ReadString() As String
  Dim size As Integer

  ' Read the size of string.
  size = ReadInt32()

  ' Check size.
  If size > 256 Then _
   Trace.TraceWarning("Input string may be invalid.")

  ' Return it.
  Return ReadString(size)

 End Function

 ''' <summary>
 ''' Endian converter.
 ''' </summary>
 ''' <param name="v">
 ''' The <c>UInt32</c> to convert.
 ''' </param>
 Private Shared Function __swap(ByVal v As UInteger) As UInteger
  Return ((CUInt((v And &HFF000000) >> 24)) Or _
          (CUInt((v And &HFF0000) >> 16) << 8) Or _
          (CUInt((v And &HFF00) >> 8) << 16) Or _
          (CUInt((v And &HFF)) << 24))

 End Function

 ''' <summary>
 ''' Default chunk handler.
 ''' </summary>
 ''' <param name="Reader">
 ''' IFF Reader handling the chunk.
 ''' </param>
 ''' <param name="ChunkAttributes">
 ''' Attributes of the chunk.
 ''' </param>
 Private Shared Sub __defaultChunkHandler(ByVal Reader As IFFReader, ByVal ChunkAttributes As ChunkAttributes)
  Trace.TraceInformation("Skipping chunk:" & vbCrLf & _
                         " ID: """ & ChunkAttributes.ID & """" & vbCrLf & _
                         " Type: '" & ChunkAttributes.Type.ToString() & "'" & vbCrLf & _
                         CStr(IIf(ChunkAttributes.Type = ChunkType.Normal, " Version: " & CStr(ChunkAttributes.Version), "")))

 End Sub

End Class
