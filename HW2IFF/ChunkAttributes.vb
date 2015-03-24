''' <summary>
''' Structure which stores chunk attributes.
''' </summary>
Public Structure ChunkAttributes
    ''' <summary>ID of chunk.</summary>
    Private m_ID As String

    ''' <summary>Size of chunk.</summary>
    Private m_Size As Integer

    ''' <summary>Type of chunk.</summary>
    Private m_Type As ChunkType

    ''' <summary>Version of chunk.</summary>
    Private m_Version As UInteger

    ''' <summary>
    ''' Structure Initializer.
    ''' </summary>
    ''' <param name="ID">
    ''' ID of chunk.
    ''' </param>
    ''' <param name="Size">
    ''' Size of chunk.
    ''' </param>
    ''' <param name="Type">
    ''' Type of chunk.
    ''' </param>
    ''' <param name="Version">
    ''' Version of chunk.
    ''' </param>
    Friend Sub New(ByVal ID As String, ByVal Size As Integer, ByVal Type As ChunkType,
                   Optional ByVal Version As UInteger = 0)
        ' Check inputs.
        If (ID Is Nothing) OrElse (ID = "") Then _
            Throw New ArgumentNullException("ID") _
                : Exit Sub

        ' Check inputs.
        If ID.Length <> 4 Then _
            Throw New ArgumentException("'ID.Length <> 4'") _
                : Exit Sub

        ' Check inputs.
        If (Type <> ChunkType.Default) AndAlso
           (Type <> ChunkType.Normal) AndAlso
           (Type <> ChunkType.Form) Then _
            Throw New ArgumentOutOfRangeException("Type") _
                : Exit Sub

        ' Set data.
        m_ID = ID
        m_Size = Size
        m_Type = Type
        m_Version = Version
    End Sub

    ''' <summary>
    ''' Returns ID of chunk.
    ''' </summary>
    Public ReadOnly Property ID() As String
        Get
            Return m_ID
        End Get
    End Property

    Public ReadOnly Property Size() As Integer
        Get
            Return m_Size
        End Get
    End Property

    ''' <summary>
    ''' Returns type of chunk.
    ''' </summary>
    Public ReadOnly Property Type() As ChunkType
        Get
            Return m_Type
        End Get
    End Property

    ''' <summary>
    ''' Returns version of chunk.
    ''' </summary>
    Public ReadOnly Property Version() As UInteger
        Get
            Return m_Version
        End Get
    End Property
End Structure
