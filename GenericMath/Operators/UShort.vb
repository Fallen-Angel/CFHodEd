Namespace Operators
    ''' <summary>
    ''' Provides operators for <c>UShort</c> data-type.
    ''' </summary>
    <HideModuleName()>
    Friend Module [UShort]
        ''' <summary>Performs the unary minus operation (sign negation).</summary>
        Private Function UnaryMinus(ByVal V As UShort) As UShort
            If V = 0 Then _
                Return 0

            ' Not possible with unsigned data type.
            Throw New System.OverflowException("Arithmetic operation resulted in an overflow.")
        End Function

        ''' <summary>
        ''' Initializes the functions for <c>UShort</c> data-type.
        ''' </summary>
        Friend Sub Initialize()
            Arithmetic (Of UShort).UnaryPlus =
                Function(V As UShort) (V)
            Arithmetic (Of UShort).UnaryMinus =
                AddressOf UnaryMinus
            Arithmetic (Of UShort).Add =
                Function(L As UShort, R As UShort) (L + R)
            Arithmetic (Of UShort).Subtract =
                Function(L As UShort, R As UShort) (L - R)
            Arithmetic (Of UShort).Multiply =
                Function(L As UShort, R As UShort) (L*R)
            Arithmetic (Of UShort).Divide =
                Function(L As UShort, R As UShort) (L\R)
            Arithmetic (Of UShort).Power =
                Function(L As UShort, R As UShort) CUShort(L^R)
            Arithmetic (Of UShort).IntegerDivide =
                Function(L As UShort, R As UShort) (L\R)
            Arithmetic (Of UShort).Modulus =
                Function(L As UShort, R As UShort) (L Mod R)
            Arithmetic (Of UShort).Equal =
                Function(L As UShort, R As UShort) (L = R)
            Arithmetic (Of UShort).NotEqual =
                Function(L As UShort, R As UShort) (L <> R)
            Arithmetic (Of UShort).LessThan =
                Function(L As UShort, R As UShort) (L < R)
            Arithmetic (Of UShort).LessThanEqual =
                Function(L As UShort, R As UShort) (L <= R)
            Arithmetic (Of UShort).MoreThan =
                Function(L As UShort, R As UShort) (L > R)
            Arithmetic (Of UShort).MoreThanEqual =
                Function(L As UShort, R As UShort) (L >= R)
        End Sub
    End Module
End Namespace
