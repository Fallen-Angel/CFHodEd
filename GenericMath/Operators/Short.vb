Namespace Operators
    ''' <summary>
    ''' Provides operators for <c>Short</c> data-type.
    ''' </summary>
    <HideModuleName()>
    Friend Module [Short]
        ''' <summary>
        ''' Initializes the functions for <c>Short</c> data-type.
        ''' </summary>
        Friend Sub Initialize()
            Arithmetic (Of Short).UnaryPlus =
                Function(V As Short) (V)
            Arithmetic (Of Short).UnaryMinus =
                Function(V As Short) (- V)
            Arithmetic (Of Short).Add =
                Function(L As Short, R As Short) (L + R)
            Arithmetic (Of Short).Subtract =
                Function(L As Short, R As Short) (L - R)
            Arithmetic (Of Short).Multiply =
                Function(L As Short, R As Short) (L*R)
            Arithmetic (Of Short).Divide =
                Function(L As Short, R As Short) (L\R)
            Arithmetic (Of Short).Power =
                Function(L As Short, R As Short) CShort(L^R)
            Arithmetic (Of Short).IntegerDivide =
                Function(L As Short, R As Short) (L\R)
            Arithmetic (Of Short).Modulus =
                Function(L As Short, R As Short) (L Mod R)
            Arithmetic (Of Short).Equal =
                Function(L As Short, R As Short) (L = R)
            Arithmetic (Of Short).NotEqual =
                Function(L As Short, R As Short) (L <> R)
            Arithmetic (Of Short).LessThan =
                Function(L As Short, R As Short) (L < R)
            Arithmetic (Of Short).LessThanEqual =
                Function(L As Short, R As Short) (L <= R)
            Arithmetic (Of Short).MoreThan =
                Function(L As Short, R As Short) (L > R)
            Arithmetic (Of Short).MoreThanEqual =
                Function(L As Short, R As Short) (L >= R)
        End Sub
    End Module
End Namespace
