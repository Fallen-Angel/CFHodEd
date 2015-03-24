Namespace Operators
    ''' <summary>
    ''' Provides operators for <c>Integer</c> data-type.
    ''' </summary>
    <HideModuleName()>
    Friend Module [Integer]
        ''' <summary>
        ''' Initializes the functions for <c>Integer</c> data-type.
        ''' </summary>
        Friend Sub Initialize()
            Arithmetic (Of Integer).UnaryPlus =
                Function(V As Integer) (V)
            Arithmetic (Of Integer).UnaryMinus =
                Function(V As Integer) (- V)
            Arithmetic (Of Integer).Add =
                Function(L As Integer, R As Integer) (L + R)
            Arithmetic (Of Integer).Subtract =
                Function(L As Integer, R As Integer) (L - R)
            Arithmetic (Of Integer).Multiply =
                Function(L As Integer, R As Integer) (L*R)
            Arithmetic (Of Integer).Divide =
                Function(L As Integer, R As Integer) (L\R)
            Arithmetic (Of Integer).Power =
                Function(L As Integer, R As Integer) CInt(L^R)
            Arithmetic (Of Integer).IntegerDivide =
                Function(L As Integer, R As Integer) (L\R)
            Arithmetic (Of Integer).Modulus =
                Function(L As Integer, R As Integer) (L Mod R)
            Arithmetic (Of Integer).Equal =
                Function(L As Integer, R As Integer) (L = R)
            Arithmetic (Of Integer).NotEqual =
                Function(L As Integer, R As Integer) (L <> R)
            Arithmetic (Of Integer).LessThan =
                Function(L As Integer, R As Integer) (L < R)
            Arithmetic (Of Integer).LessThanEqual =
                Function(L As Integer, R As Integer) (L <= R)
            Arithmetic (Of Integer).MoreThan =
                Function(L As Integer, R As Integer) (L > R)
            Arithmetic (Of Integer).MoreThanEqual =
                Function(L As Integer, R As Integer) (L >= R)
        End Sub
    End Module
End Namespace