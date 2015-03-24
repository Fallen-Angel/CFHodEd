''' <summary>
''' Single node in a handler list.
''' </summary>
Friend Structure HandlerNode
    ''' <summary>Function to handle the chunk.</summary>
    Dim Handler As ChunkHandler

    ''' <summary>Type of chunk.</summary>
    Dim Type As ChunkType

    ''' <summary>ID of the chunk.</summary>
    Dim ID As String

    ''' <summary>Version of the chunk.</summary>
    Dim Version As UInteger
End Structure
