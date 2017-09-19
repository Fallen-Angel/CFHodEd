Partial Class HOD
    Private m_Pool As Pool.Pool

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns the list of background meshes in the HOD.
    ''' </summary>
    Public ReadOnly Property PoolData() As Pool.Pool
        Get
            Return m_Pool
        End Get
    End Property

    Private Sub ReadPOOLChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
        m_Pool = Pool.Pool.Read(IFF, ChunkAttributes)
    End Sub
End Class
