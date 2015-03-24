Namespace Operators
    ''' <summary>
    ''' Provides operators for <c>SByte</c> data-type.
    ''' </summary>
    <HideModuleName()>
    Friend Module [SByte]
        ''' <summary>
        ''' Initializes the functions for <c>SByte</c> data-type.
        ''' </summary>
        Friend Sub Initialize()
            Arithmetic (Of SByte).UnaryPlus =
                Function(V As SByte) (V)
            Arithmetic (Of SByte).UnaryMinus =
                Function(V As SByte) (- V)
            Arithmetic (Of SByte).Add =
                Function(L As SByte, R As SByte) (L + R)
            Arithmetic (Of SByte).Subtract =
                Function(L As SByte, R As SByte) (L - R)
            Arithmetic (Of SByte).Multiply =
                Function(L As SByte, R As SByte) (L*R)
            Arithmetic (Of SByte).Divide =
                Function(L As SByte, R As SByte) (L\R)
            Arithmetic (Of SByte).Power =
                Function(L As SByte, R As SByte) CSByte(L^R)
            Arithmetic (Of SByte).IntegerDivide =
                Function(L As SByte, R As SByte) (L\R)
            Arithmetic (Of SByte).Modulus =
                Function(L As SByte, R As SByte) (L Mod R)
            Arithmetic (Of SByte).Equal =
                Function(L As SByte, R As SByte) (L = R)
            Arithmetic (Of SByte).NotEqual =
                Function(L As SByte, R As SByte) (L <> R)
            Arithmetic (Of SByte).LessThan =
                Function(L As SByte, R As SByte) (L < R)
            Arithmetic (Of SByte).LessThanEqual =
                Function(L As SByte, R As SByte) (L <= R)
            Arithmetic (Of SByte).MoreThan =
                Function(L As SByte, R As SByte) (L > R)
            Arithmetic (Of SByte).MoreThanEqual =
                Function(L As SByte, R As SByte) (L >= R)
        End Sub
    End Module
End Namespace
