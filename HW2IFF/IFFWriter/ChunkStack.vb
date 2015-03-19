''' <summary>
''' Class which acts as a chunk stack for writing IFF files.
''' </summary>
Friend Class ChunkStack
 ''' <summary>Stack to hold the chunks being written.</summary>
 Private m_Stack As Stack(Of ChunkMarker)

 ''' <summary>The writer which is associated with this instance.</summary>
 Private m_Writer As IFFWriter

 ''' <summary>
 ''' Class constructor.
 ''' </summary>
 ''' <param name="Writer">
 ''' The writer which will be associated with this instance.
 ''' </param>
 Public Sub New(ByVal Writer As IFFWriter)
  ' Check inputs.
  If Writer Is Nothing Then _
   Throw New ArgumentNullException("Writer") _
 : Exit Sub

  ' Initialize the members.
  m_Stack = New Stack(Of ChunkMarker)
  m_Writer = Writer

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
  ' Check inputs.
  Try
   Dim CA As New ChunkAttributes(ID, 0, Type, Version)
  Catch ex As Exception
   Throw ex
  End Try

  ' Make a new chunk marker and add to stack.
  m_Stack.Push(New ChunkMarker With { _
   .ID = ID, _
   .Type = Type, _
   .StartPosition = m_Writer.BaseStream.Position + 8 _
  })

  Select Case Type
   ' NORMAL
   Case ChunkType.Normal
    m_Writer.Write("NRML", 4)
    m_Writer.WriteUInt32(0)
    m_Writer.Write(ID, 4)
    m_Writer.WriteUInt32(__swap(Version))

    ' FORM
   Case ChunkType.Form
    m_Writer.Write("FORM", 4)
    m_Writer.WriteUInt32(0)
    m_Writer.Write(ID, 4)

    ' DEFAULT
   Case Else
    m_Writer.Write(ID, 4)
    m_Writer.WriteUInt32(0)

  End Select

 End Sub

 ''' <summary>
 ''' Pops a chunk off the stack and returns it.
 ''' </summary>
 Public Function Pop() As ChunkMarker
  ' Check if there is any chunk to pop.
  If m_Stack.Count = 0 Then _
   Throw New Exception("No chunk to pop.") _
 : Exit Function

  ' Get the chunk.
  Dim Chunk As ChunkMarker = m_Stack.Pop()

  ' Get the current position.
  Dim CurrPos As Long = m_Writer.BaseStream.Position

  ' Get the chunk size.
  Dim Size As UInteger = CUInt(CurrPos - Chunk.StartPosition)

  ' Seek to appropriate position to write size.
  m_Writer.BaseStream.Position = Chunk.StartPosition - 4

  ' Now write the size.
  m_Writer.WriteUInt32(__swap(Size))

  ' Return to original position.
  m_Writer.BaseStream.Position = CurrPos

  Return Chunk

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

End Class
