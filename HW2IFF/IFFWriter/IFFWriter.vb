''' <summary>
''' Class that can write data in Homeworld2 IFF files.
''' </summary>
Public NotInheritable Class IFFWriter
 Inherits IO.BinaryWriter

 ''' <summary>Stack to hold the chunks being written.</summary>
 Private m_ChunkStack As ChunkStack

 ''' <summary>
 ''' Class constructor.
 ''' </summary>
 ''' <param name="output">
 ''' Output stream to use for writing.
 ''' </param>
 Public Sub New(ByVal output As IO.Stream)
  ' Call base constructor.
  MyBase.New(output)

  ' Initialize our objects.
  m_ChunkStack = New ChunkStack(Me)

 End Sub

 ''' <summary>
 ''' Opens a file for writing.
 ''' </summary>
 ''' <param name="Filename">
 ''' File to open.
 ''' </param>
 ''' <returns>
 ''' The object if successful, <c>Nothing</c> otherwise.
 ''' </returns>
 Public Shared Function Open(ByVal Filename As String) As IFFWriter
  Dim Writer As IFFWriter

  ' Try to open the file.
  Try
   ' Try to create a binary writer.
   Writer = New IFFWriter(IO.File.OpenWrite(Filename))

  Catch ex As Exception
   Return Nothing

  End Try

  Return Writer

 End Function

 ''' <summary>
 ''' Pushes a default chunk to the stack.
 ''' </summary>
 ''' <param name="ID">
 ''' ID of the chunk.
 ''' </param>
 Public Sub Push(ByVal ID As String)
  m_ChunkStack.Push(ID, ChunkType.Default)

 End Sub

 ''' <summary>
 ''' Pushes a chunk to the stack.
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
 Public Sub Push(ByVal ID As String, ByVal Type As ChunkType, Optional ByVal Version As UInteger = 0)
  m_ChunkStack.Push(ID, Type, Version)

 End Sub

 ''' <summary>
 ''' Pops a chunk off the stack and returns it.
 ''' </summary>
 Public Function Pop() As String
  Return m_ChunkStack.Pop().ID

 End Function

 ''' <summary>
 ''' Writes a fixed length string to the stream.
 ''' </summary>
 ''' <param name="String">
 ''' String to write.
 ''' </param>
 ''' <param name="Length">
 ''' Length of string.
 ''' </param>
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
 Public Overloads Sub Write(ByVal [String] As String, ByVal Length As Integer)
  Dim str As Byte()

  If (Length < 0) Then _
   Throw New ArgumentOutOfRangeException("Length") _
 : Exit Sub

  If Length = 0 Then _
   Exit Sub

  ' Initialize output string.
  ReDim str(Length - 1)

  ' Build the output string.
  If [String] IsNot Nothing Then
   For I As Integer = 0 To CInt(Length) - 1
    ' Check string length.
    If I = [String].Length Then _
     Exit For

    ' Copy the character.
    str(I) = CByte(Asc([String](I)) And &HFF)

   Next I ' For I As Integer = 0 To CInt(Length) - 1
  End If ' If [String] IsNot Nothing Then

  ' Write the string.
  Write(str)

 End Sub

 ''' <summary>
 ''' Writes a string to the stream.
 ''' </summary> 
 ''' <param name="String">
 ''' String to write.
 ''' </param>
 ''' <exception cref="IO.IOException">
 ''' An I/O error occurs.
 ''' </exception>
 ''' <exception cref="IO.EndOfStreamException">
 ''' The end of the stream is reached.
 ''' </exception>
 ''' <exception cref="ObjectDisposedException">
 ''' The stream is closed.
 ''' </exception> 
 Public Overrides Sub Write(ByVal [String] As String)
  ' Write the size of the string.
  WriteInt32([String].Length)

  ' Write the string itself.
  For I As Integer = 0 To [String].Length - 1
   Write(CByte(&HFF And Asc([String](I))))

  Next I ' For I As Integer = 0 To [String].Length - 1


 End Sub

 ''' <summary>
 ''' Writes a 16-bit integer to the stream.
 ''' </summary>
 ''' <param name="Value">
 ''' Value to write.
 ''' </param>
 Public Sub WriteInt16(Of T As Structure)(ByVal Value As T)
  Write(CType(CObj(Value), Int16))

 End Sub

 ''' <summary>
 ''' Writes a 16-bit unsigned integer to the stream.
 ''' </summary>
 ''' <param name="Value">
 ''' Value to write.
 ''' </param>
 Public Sub WriteUInt16(Of T As Structure)(ByVal Value As T)
  Write(CType(CObj(Value), UInt16))

 End Sub

 ''' <summary>
 ''' Writes a 32-bit integer to the stream.
 ''' </summary>
 ''' <param name="Value">
 ''' Value to write.
 ''' </param>
 Public Sub WriteInt32(Of T As Structure)(ByVal Value As T)
  Write(CType(CObj(Value), Int32))

 End Sub

 ''' <summary>
 ''' Writes a 32-bit unsigned integer to the stream.
 ''' </summary>
 ''' <param name="Value">
 ''' Value to write.
 ''' </param>
 Public Sub WriteUInt32(Of T As Structure)(ByVal Value As T)
  Write(CType(CObj(Value), UInt32))

 End Sub

End Class
