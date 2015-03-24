Namespace Standard
    ''' <summary>
    ''' Mesh using the standard data types for all it's components
    ''' (i.e. <c>Vertex</c>, <c>Integer</c> and <c>Material</c>).
    ''' </summary>
    Public NotInheritable Class BasicMesh
        Inherits GBasicMesh(Of Vertex, Integer, Material)

        ''' <summary>
        ''' Class constructor.
        ''' </summary>
        Public Sub New()
            ' Call default constructor.
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Copy constructor.
        ''' </summary>
        ''' <param name="obj">
        ''' The object whose copy is to be made.
        ''' </param>
        ''' <remarks>
        ''' This is equivalent to a normal constructor if <c>obj Is Nothing</c>.
        ''' Also, the new mesh is not locked, even if the input is.
        ''' </remarks>
        Public Sub New(ByVal obj As BasicMesh)
            ' Call default constructor.
            MyBase.New(obj)
        End Sub
    End Class
End Namespace
