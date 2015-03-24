''' <summary>
''' Form to display I/O results to user.
''' </summary>
Friend NotInheritable Class IOResult
    ''' <summary>Stream to accept trace input.</summary>
    Private m_Stream As IO.MemoryStream

    ''' <summary>Trace listener.</summary>
    Private m_Listener As Diagnostics.TextWriterTraceListener

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Initialize the stream and the trace listener.
        m_Stream = New IO.MemoryStream
        m_Listener = New Diagnostics.TextWriterTraceListener(m_Stream)

        ' Register the trace listener.
        Trace.Listeners.Add(m_Listener)
    End Sub

    Private Sub IOResult_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) _
        Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub IOResult_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) _
        Handles Me.FormClosing
        m_Listener.Dispose()
        m_Stream.Dispose()
    End Sub

    Private Sub IOResult_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Flush the trace listener.
        m_Listener.Flush()

        ' See if there is any text.
        If m_Stream.Position = 0 Then _
            Me.Close() _
                : Exit Sub

        ' Rewind the stream.
        m_Stream.Position = 0

        ' Unregister the trace listener.
        Trace.Listeners.Remove(m_Listener)

        ' Initialize the text reader.
        Dim TR As New IO.StreamReader(m_Stream)

        ' Display the text.
        TextBox.Text = TR.ReadToEnd()
        TextBox.SelectionStart = 0
        TextBox.SelectionLength = 0

        ' Dispose the stream.
        TR.Dispose()
    End Sub
End Class
