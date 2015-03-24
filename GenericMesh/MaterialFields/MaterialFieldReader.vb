Namespace MaterialFields
    ''' <summary>
    ''' Class containing functions for reading from various fields of a generic
    ''' material.
    ''' </summary>
    Public NotInheritable Class MaterialFieldReader
        ''' <summary>
        ''' Reads the specified field. For more information see the respective interface.
        ''' </summary>
        Public Shared Function MaterialName (Of MaterialType As {Structure, IMaterial}) _
            (ByVal V As MaterialType) As String

            ' Check presence.
            If TypeOf V Is IMaterialName Then _
                Return CType(V, IMaterialName).GetMaterialName() _
                Else _
                Return ""
        End Function

        ''' <summary>
        ''' Reads the specified field. For more information see the respective interface.
        ''' </summary>
        Public Shared Function TextureName (Of MaterialType As {Structure, IMaterial}) _
            (ByVal V As MaterialType, Optional ByVal Index As Integer = 0) As String

            ' Check presence.
            If TypeOf V Is IMaterialTexture Then _
                Return CType(V, IMaterialTexture).GetTextureName(Index) _
                Else _
                Return ""
        End Function

        ''' <summary>
        ''' Reads the specified field. For more information see the respective interface.
        ''' </summary>
        Public Shared Function Attributes (Of MaterialType As {Structure, IMaterial}) _
            (ByVal V As MaterialType) As Direct3D.Material

            ' Check presence.
            If TypeOf V Is IMaterialAttributes Then _
                Return CType(V, IMaterialAttributes).GetAttributes() _
                Else _
                Return New Direct3D.Material With { _
                    .AmbientColor = New ColorValue(0.2, 0.2, 0.2), _
                    .DiffuseColor = New ColorValue(0.8, 0.8, 0.8) _
                    }
        End Function
    End Class
End Namespace
