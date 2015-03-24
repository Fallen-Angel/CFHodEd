Namespace WavefrontObjectHelper
    ''' <summary>
    ''' Indice group (may be a point (1 element), line (2 elements), triangle (3 elements)
    ''' or a polygon (more than 3 elements).
    ''' </summary>
    Friend Structure IndiceGroup
        ''' <summary>Indice data.</summary>
        Private _Indices() As IndiceValues

        ''' <summary>Index of material used.</summary>
        Dim Material As Integer

        ''' <summary>
        ''' Returns\Sets the number of indices.
        ''' </summary>
        ''' <exception cref="ArgumentOutOfRangeException">
        ''' Thrown when the new size is invalid (&lt; 0).
        ''' </exception>
        Public Property Count() As Integer
            Get
                ' Check if the array was initialized.
                If _Indices Is Nothing Then _
                    Return 0

                ' Return the length.
                Return _Indices.Length
            End Get

            Set(ByVal value As Integer)
                ' Check bounds.
                If (value < 0) Then _
                    Throw New ArgumentOutOfRangeException("value") _
                        : Exit Property

                ' Resize the array.
                If _Indices Is Nothing Then _
                    ReDim _Indices(value - 1) _
                    Else _
                    ReDim Preserve _Indices(value - 1)
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets the indices.
        ''' </summary>
        ''' <param name="Index">
        ''' The index of the indice to be read\written.
        ''' </param>
        ''' <exception cref="ArgumentOutOfRangeException">
        ''' Thrown when <c>Index</c> is out of bounds.
        ''' </exception>
        Public Property Indices(ByVal Index As Integer) As IndiceValues
            Get
                ' See if the array exists and then check bounds.
                If (_Indices Is Nothing) OrElse (Index < 0) OrElse (Index >= _Indices.Length) Then _
                    Throw New ArgumentOutOfRangeException("Index") _
                        : Exit Property

                ' Return the indice.
                Return _Indices(Index)
            End Get

            Set(ByVal value As IndiceValues)
                ' See if the array exists and then check bounds.
                If (_Indices Is Nothing) OrElse (Index < 0) OrElse (Index >= _Indices.Length) Then _
                    Throw New ArgumentOutOfRangeException("Index") _
                        : Exit Property

                ' Set the indice.
                _Indices(Index) = value
            End Set
        End Property
    End Structure
End Namespace
