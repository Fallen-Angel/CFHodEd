Namespace Exceptions
    ''' <summary>
    ''' Class which is 'thrown' for errors when the mesh is
    ''' not (already) locked.
    ''' </summary>
    Public NotInheritable Class MeshNotLockedException
        Inherits Exception

        ''' <summary>
        ''' Initializes a new instance of the <c>MeshNotLockedException</c> class.
        ''' </summary>
        Public Sub New()
            ' Nothing to do here.
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <c>MeshNotLockedException</c> class
        ''' with a specified error message.
        ''' </summary>
        ''' <param name="message">
        ''' The message that describes the error.
        ''' </param>
        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the <c>MeshNotLockedException</c> class
        ''' with a specified error message and a reference to the inner exception that
        ''' is the cause of this exception.
        ''' </summary>
        ''' <param name="message">
        ''' The error message that explains the reason for the exception.
        ''' </param>
        ''' <param name="innerException">
        ''' The exception that is the cause of the current exception, or a null reference 
        ''' (<c>Nothing</c> in Visual Basic) if no inner exception is specified.
        ''' </param> 
        Public Sub New(ByVal message As String, ByVal innerException As System.Exception)
            MyBase.New(message, innerException)
        End Sub

        ' Nothing here.
    End Class
End Namespace
