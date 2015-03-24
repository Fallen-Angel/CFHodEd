''' <summary>
''' List of handlers for reading an IFF file.
''' </summary>
Friend Class HandlerList
    ''' <summary>IFF Reader this instance is associated with.</summary>
    Private m_IFFReader As IFFReader

    ''' <summary>List of available handlers.</summary>
    Private m_Handlers As List(Of HandlerNode)

    ''' <summary>The handler to use when no other handler is available.</summary>
    Public DefaultHandler As ChunkHandler

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    ''' <param name="IFFReader">
    ''' IFF Reader this instance will be associated with.
    ''' </param>
    Public Sub New(ByVal IFFReader As IFFReader)
        m_IFFReader = IFFReader
        m_Handlers = New List(Of HandlerNode)
    End Sub

    ''' <summary>
    ''' Adds a handler for the specified chunk.
    ''' </summary>
    ''' <param name="ID">
    ''' ID of the chunk.
    ''' </param>
    ''' <param name="Type">
    ''' Type of chunk.
    ''' </param>
    ''' <param name="Handler">
    ''' Handler to set.
    ''' </param>
    ''' <param name="Version">
    ''' Version of the chunk
    ''' </param>
    ''' <remarks>
    ''' This removes any existing handler for the same ID, Type, and Version
    ''' combination.
    ''' </remarks>
    Public Sub [AddHandler](ByVal ID As String, ByVal Type As ChunkType,
                            ByVal Handler As ChunkHandler,
                            Optional ByVal Version As UInteger = 0)

        Dim hn As HandlerNode

        ' Check inputs.
        If Handler Is Nothing Then _
            Throw New ArgumentNullException("Handler")

        ' Try to create the handler node.
        hn = __MakeHandlerNode(ID, Type, Version)

        ' See if it was created.
        If hn.ID = "" Then _
            Exit Sub

        ' Try to find an existing handler. If present, then remove it.
        If FindHandler(ID, Type, Version) IsNot Nothing Then _
            m_Handlers.RemoveAll(Function(h As HandlerNode) __CompareHandlers(h, hn))

        ' Set the handler.
        hn.Handler = Handler

        ' Add to list.
        m_Handlers.Add(hn)
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
    Public Function FindHandler(ByVal ID As String, ByVal Type As ChunkType,
                                Optional ByVal Version As UInteger = 0) As ChunkHandler

        ' See if we have any handlers to begin with.
        If m_Handlers.Count = 0 Then _
            Return DefaultHandler

        ' Try to create the handler.
        Dim hn As HandlerNode = __MakeHandlerNode(ID, Type, Version)

        ' See if it's valid.
        If hn.ID = "" Then _
            Return Nothing

        ' If the handler exists, return it, else return nothing.
        If m_Handlers.Exists(Function(h As HandlerNode) __CompareHandlers(h, hn)) Then _
            Return m_Handlers.Find(Function(h As HandlerNode) __CompareHandlers(h, hn)).Handler _
            Else _
            Return DefaultHandler
    End Function

    ''' <summary>
    ''' Compares two handler nodes.
    ''' </summary>
    Private Function __CompareHandlers(ByVal h1 As HandlerNode, ByVal h2 As HandlerNode) As Boolean
        If (h1.ID = h2.ID) AndAlso
           (h1.Type = h2.Type) AndAlso
           ((h1.Type <> ChunkType.Normal) OrElse (h1.Version = h2.Version)) Then _
            Return True _
            Else _
            Return False
    End Function

    ''' <summary>
    ''' Verifies and makes a handler node with the given information.
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
    ''' The created handler node, or nothing.
    ''' </returns>
    Private Function __MakeHandlerNode(ByVal ID As String, ByVal Type As ChunkType,
                                       Optional ByVal Version As UInteger = 0) As HandlerNode

        ' Check inputs.
        Try
            Dim CA As New ChunkAttributes(ID, 0, Type, Version)
        Catch ex As Exception
            Throw ex
        End Try

        ' Return the object.
        Return New HandlerNode With { _
            .ID = ID, _
            .Type = Type, _
            .Version = Version _
            }
    End Function
End Class
