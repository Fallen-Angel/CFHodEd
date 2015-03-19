''' <summary>
''' Type of IFF chunk.
''' </summary>
Public Enum ChunkType
 ''' <summary>No special header.</summary>
 [Default]

 ''' <summary>Chunk in a 'NRML' header.</summary>
 Normal

 ''' <summary>Chunk in a 'FORM' header.</summary>
 Form

End Enum

''' <summary>
''' Function to handle reading a chunk.
''' </summary>
''' <param name="Reader">
''' IFF Reader handling the chunk.
''' </param>
''' <param name="ChunkAttributes">
''' Attributes of the chunk.
''' </param>
Public Delegate Sub ChunkHandler(ByVal Reader As IFFReader, _
                                 ByVal ChunkAttributes As ChunkAttributes)