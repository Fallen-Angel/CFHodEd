''' <summary>
''' Class representing a single IFF chunk.
''' </summary>
Friend Structure Chunk
    ''' <summary>Type of chunk.</summary>
    Public Type As ChunkType

    ''' <summary>ID of Chunk.</summary>
    Public ID As String

    ''' <summary>Version of chunk.</summary>
    Public Version As UInteger

    ''' <summary>Position where the chunk starts in file.</summary>
    Public StartPosition As Long

    ''' <summary>Size of chunk.</summary>
    Public Size As Long

    ''' <summary>IFF Reader this instance is associated with.</summary>
    Private m_IFFReader As IFFReader

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    ''' <param name="IFFReader">
    ''' IFF Reader this instance will be associated with.
    ''' </param>
    Public Sub New(ByVal IFFReader As IFFReader)
        ' Set the association.
        m_IFFReader = IFFReader

        ' Set the type.
        Type = ChunkType.Default

        ' Set the default chunk parameters.
        ID = "    "
        StartPosition = - 1
        Size = - 1
        Version = 0
    End Sub

    ''' <summary>
    ''' Reads the chunk.
    ''' </summary>
    Sub Read()
        Dim Handler As ChunkHandler

        ' Check size.
        If (Size < 0) Then _
            Throw New Exception("'Size < 0'") _
                : Exit Sub

        ' Handle the chunk.
        Handler = m_IFFReader.FindHandler(ID, Type, Version)

        ' If handler does not exist then skip this chunk.
        If Handler Is Nothing Then _
            m_IFFReader.BaseStream.Position += Size _
                : Exit Sub

        ' Read the bytes.
        Dim Data() As Byte = m_IFFReader.ReadBytes(CInt(Size))

        ' Create the backing store for the chunk IFF reader.
        Dim ChunkStream As New IO.MemoryStream(Data, 0, Data.Length, False, False)

        ' Create a chunk IFF reader which would be used for reading the chunk.
        Dim ChunkHandler As New IFFReader(ChunkStream)

        ' Read the chunk.
        Handler(ChunkHandler, New ChunkAttributes(ID, CInt(Size), Type, Version))

        ' Dispose the objects.
        ChunkHandler.Close()
        ChunkStream.Close()

        ' Erase the data.
        Erase Data
    End Sub
End Structure
