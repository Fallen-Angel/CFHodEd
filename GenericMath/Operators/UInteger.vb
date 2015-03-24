Namespace Operators
    ''' <summary>
    ''' Provides operators for <c>UInteger</c> data-type.
    ''' </summary>
    <HideModuleName()>
    Friend Module [UInteger]
        ''' <summary>Performs the unary minus operation (sign negation).</summary>
        Private Function UnaryMinus(ByVal V As UInteger) As UInteger
            If V = 0 Then _
                Return 0

            ' Not possible with unsigned data type.
            Throw New System.OverflowException("Arithmetic operation resulted in an overflow.")
        End Function

        ''' <summary>
        ''' Initializes the functions for <c>UInteger</c> data-type.
        ''' </summary>
        Friend Sub Initialize()
            Arithmetic (Of UInteger).UnaryPlus =
                Function(V As UInteger) (V)
            Arithmetic (Of UInteger).UnaryMinus =
                AddressOf UnaryMinus
            Arithmetic (Of UInteger).Add =
                Function(L As UInteger, R As UInteger) (L + R)
            Arithmetic (Of UInteger).Subtract =
                Function(L As UInteger, R As UInteger) (L - R)
            Arithmetic (Of UInteger).Multiply =
                Function(L As UInteger, R As UInteger) (L*R)
            Arithmetic (Of UInteger).Divide =
                Function(L As UInteger, R As UInteger) (L\R)
            Arithmetic (Of UInteger).Power =
                Function(L As UInteger, R As UInteger) CUInt(L^R)
            Arithmetic (Of UInteger).IntegerDivide =
                Function(L As UInteger, R As UInteger) (L\R)
            Arithmetic (Of UInteger).Modulus =
                Function(L As UInteger, R As UInteger) (L Mod R)
            Arithmetic (Of UInteger).Equal =
                Function(L As UInteger, R As UInteger) (L = R)
            Arithmetic (Of UInteger).NotEqual =
                Function(L As UInteger, R As UInteger) (L <> R)
            Arithmetic (Of UInteger).LessThan =
                Function(L As UInteger, R As UInteger) (L < R)
            Arithmetic (Of UInteger).LessThanEqual =
                Function(L As UInteger, R As UInteger) (L <= R)
            Arithmetic (Of UInteger).MoreThan =
                Function(L As UInteger, R As UInteger) (L > R)
            Arithmetic (Of UInteger).MoreThanEqual =
                Function(L As UInteger, R As UInteger) (L >= R)
        End Sub
    End Module
End Namespace