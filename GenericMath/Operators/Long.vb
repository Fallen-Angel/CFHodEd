Namespace Operators
    ''' <summary>
    ''' Provides operators for <c>Long</c> data-type.
    ''' </summary>
    <HideModuleName()>
    Friend Module [Long]
        ''' <summary>
        ''' Initializes the functions for <c>Long</c> data-type.
        ''' </summary>
        Friend Sub Initialize()
            Arithmetic (Of Long).UnaryPlus =
                Function(V As Long) (V)
            Arithmetic (Of Long).UnaryMinus =
                Function(V As Long) (- V)
            Arithmetic (Of Long).Add =
                Function(L As Long, R As Long) (L + R)
            Arithmetic (Of Long).Subtract =
                Function(L As Long, R As Long) (L - R)
            Arithmetic (Of Long).Multiply =
                Function(L As Long, R As Long) (L*R)
            Arithmetic (Of Long).Divide =
                Function(L As Long, R As Long) (L\R)
            Arithmetic (Of Long).Power =
                Function(L As Long, R As Long) CLng(L^R)
            Arithmetic (Of Long).IntegerDivide =
                Function(L As Long, R As Long) (L\R)
            Arithmetic (Of Long).Modulus =
                Function(L As Long, R As Long) (L Mod R)
            Arithmetic (Of Long).Equal =
                Function(L As Long, R As Long) (L = R)
            Arithmetic (Of Long).NotEqual =
                Function(L As Long, R As Long) (L <> R)
            Arithmetic (Of Long).LessThan =
                Function(L As Long, R As Long) (L < R)
            Arithmetic (Of Long).LessThanEqual =
                Function(L As Long, R As Long) (L <= R)
            Arithmetic (Of Long).MoreThan =
                Function(L As Long, R As Long) (L > R)
            Arithmetic (Of Long).MoreThanEqual =
                Function(L As Long, R As Long) (L >= R)
        End Sub
    End Module
End Namespace