Namespace Operators
    ''' <summary>
    ''' Provides operators for <c>ULong</c> data-type.
    ''' </summary>
    <HideModuleName()>
    Friend Module [ULong]
        ''' <summary>Performs the unary minus operation (sign negation).</summary>
        Private Function UnaryMinus(ByVal V As ULong) As ULong
            If V = 0 Then _
                Return 0

            ' Not possible with unsigned data type.
            Throw New System.OverflowException("Arithmetic operation resulted in an overflow.")
        End Function

        ''' <summary>
        ''' Initializes the functions for <c>ULong</c> data-type.
        ''' </summary>
        Friend Sub Initialize()
            Arithmetic (Of ULong).UnaryPlus =
                Function(V As ULong) (V)
            Arithmetic (Of ULong).UnaryMinus =
                AddressOf UnaryMinus
            Arithmetic (Of ULong).Add =
                Function(L As ULong, R As ULong) (L + R)
            Arithmetic (Of ULong).Subtract =
                Function(L As ULong, R As ULong) (L - R)
            Arithmetic (Of ULong).Multiply =
                Function(L As ULong, R As ULong) (L*R)
            Arithmetic (Of ULong).Divide =
                Function(L As ULong, R As ULong) (L\R)
            Arithmetic (Of ULong).Power =
                Function(L As ULong, R As ULong) CULng(L^R)
            Arithmetic (Of ULong).IntegerDivide =
                Function(L As ULong, R As ULong) (L\R)
            Arithmetic (Of ULong).Modulus =
                Function(L As ULong, R As ULong) (L Mod R)
            Arithmetic (Of ULong).Equal =
                Function(L As ULong, R As ULong) (L = R)
            Arithmetic (Of ULong).NotEqual =
                Function(L As ULong, R As ULong) (L <> R)
            Arithmetic (Of ULong).LessThan =
                Function(L As ULong, R As ULong) (L < R)
            Arithmetic (Of ULong).LessThanEqual =
                Function(L As ULong, R As ULong) (L <= R)
            Arithmetic (Of ULong).MoreThan =
                Function(L As ULong, R As ULong) (L > R)
            Arithmetic (Of ULong).MoreThanEqual =
                Function(L As ULong, R As ULong) (L >= R)
        End Sub
    End Module
End Namespace