Namespace Operators
    ''' <summary>
    ''' Provides operators for <c>Byte</c> data-type.
    ''' </summary>
    <HideModuleName()>
    Friend Module [Byte]
        ''' <summary>Performs the unary minus operation (sign negation).</summary>
        Private Function UnaryMinus(ByVal V As Byte) As Byte
            If V = 0 Then _
                Return 0

            ' Not possible with unsigned data type.
            Throw New System.OverflowException("Arithmetic operation resulted in an overflow.")
        End Function

        ''' <summary>
        ''' Initializes the functions for <c>Byte</c> data-type.
        ''' </summary>
        Friend Sub Initialize()
            Arithmetic (Of Byte).UnaryPlus =
                Function(V As Byte) (V)
            Arithmetic (Of Byte).UnaryMinus =
                AddressOf UnaryMinus
            Arithmetic (Of Byte).Add =
                Function(L As Byte, R As Byte) (L + R)
            Arithmetic (Of Byte).Subtract =
                Function(L As Byte, R As Byte) (L - R)
            Arithmetic (Of Byte).Multiply =
                Function(L As Byte, R As Byte) (L*R)
            Arithmetic (Of Byte).Divide =
                Function(L As Byte, R As Byte) (L\R)
            Arithmetic (Of Byte).Power =
                Function(L As Byte, R As Byte) CByte(L^R)
            Arithmetic (Of Byte).IntegerDivide =
                Function(L As Byte, R As Byte) (L\R)
            Arithmetic (Of Byte).Modulus =
                Function(L As Byte, R As Byte) (L Mod R)
            Arithmetic (Of Byte).Equal =
                Function(L As Byte, R As Byte) (L = R)
            Arithmetic (Of Byte).NotEqual =
                Function(L As Byte, R As Byte) (L <> R)
            Arithmetic (Of Byte).LessThan =
                Function(L As Byte, R As Byte) (L < R)
            Arithmetic (Of Byte).LessThanEqual =
                Function(L As Byte, R As Byte) (L <= R)
            Arithmetic (Of Byte).MoreThan =
                Function(L As Byte, R As Byte) (L > R)
            Arithmetic (Of Byte).MoreThanEqual =
                Function(L As Byte, R As Byte) (L >= R)
        End Sub
    End Module
End Namespace
