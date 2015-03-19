''' <summary>
''' Form to display an exception to user.
''' </summary>
Friend NotInheritable Class ExceptionDisplay
 ''' <summary>Stream to accept trace input.</summary>
 Private m_Stream As IO.MemoryStream

 ''' <summary>Trace listener.</summary>
 Private m_Listener As Diagnostics.TextWriterTraceListener

 ''' <summary>The exception.</summary>
 Private m_Exception As Exception

 ''' <summary>
 ''' Class contructor.
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

 ''' <summary>
 ''' Sets the exception.
 ''' </summary>
 Public Sub SetException(ByVal ex As Exception)
  m_Exception = ex

 End Sub

 Private Sub ExceptionDisplay_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
  m_Listener.Dispose()
  m_Stream.Dispose()

 End Sub

 Private Sub ExceptionDisplay_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
  ' Flush the trace listener.
  m_Listener.Flush()

  ' Rewind the stream.
  m_Stream.Position = 0

  ' Unregister the trace listener.
  Trace.Listeners.Remove(m_Listener)

  ' Initialize the text reader.
  Dim TR As New IO.StreamReader(m_Stream)

  ' Set trace log.
  With txtTrace
   .Text = TR.ReadToEnd()
   .SelectionStart = 0
   .SelectionLength = 0

  End With ' With txtTrace  

  ' Dispose the stream.
  TR.Dispose()

  ' Set exception text.
  With txtException
   If m_Exception IsNot Nothing Then _
    .Text = m_Exception.ToString() _
   Else _
    .Text = ""

   .SelectionStart = 0
   .SelectionLength = 0

  End With ' With txtException

 End Sub

End Class