''' <summary>
''' Chunk marker for using in a IFF writer chunk stack.
''' </summary>
Friend Structure ChunkMarker
    ''' <summary>ID of chunk.</summary>
    Dim ID As String

    ''' <summary>Type of chunk.</summary>
    Dim Type As ChunkType

    ''' <summary>Position at which the chunk starts in file.</summary>
    Dim StartPosition As Long
End Structure
