''' <summary>
''' Defines the generalized properties that a structure implements to
''' create a suitable vertex type for the generic mesh class.
''' </summary>
Public Interface IVertex
 Inherits IEquatable(Of IVertex)

 ''' <summary>
 ''' FVF (Flexible Vertex Format) of the vertex.
 ''' </summary>
 ''' <value>
 ''' FVF (Flexible Vertex Format) of the vertex.
 ''' </value>
 ''' <returns>
 ''' Returns the FVF (Flexible Vertex Format) of the vertex.
 ''' </returns>
 ReadOnly Property Format() As VertexFormats

 ''' <summary>
 ''' Size of vertex.
 ''' </summary>
 ''' <value>
 ''' Size of vertex.
 ''' </value>
 ''' <remarks>
 ''' Returns the size of vertex.
 ''' </remarks>
 ReadOnly Property VertexSize() As Integer

 ''' <summary>
 ''' Initializes the vertex.
 ''' </summary>
 ''' <remarks>
 ''' If there is anything to be initialized (e.g. colour).
 ''' </remarks>
 Sub Initialize()

End Interface
