Namespace Operators
    ''' <summary>
    ''' Provides operators for <c>Single</c> data-type.
    ''' </summary>
    <HideModuleName()>
    Friend Module [Single]
        ''' <summary>Performs integral division.</summary>
        Private Function IntegerDivide(ByVal L As Single, ByVal R As Single) As Single
            Return CSng(CLng(L)\CLng(R))
        End Function

        ''' <summary>
        ''' Initializes the functions for <c>Single</c> datatype.
        ''' </summary>
        Friend Sub Initialize()
            Arithmetic (Of Single).UnaryPlus =
                Function(V As Single) (V)
            Arithmetic (Of Single).UnaryMinus =
                Function(V As Single) (- V)
            Arithmetic (Of Single).Add =
                Function(L As Single, R As Single) (L + R)
            Arithmetic (Of Single).Subtract =
                Function(L As Single, R As Single) (L - R)
            Arithmetic (Of Single).Multiply =
                Function(L As Single, R As Single) (L*R)
            Arithmetic (Of Single).Divide =
                Function(L As Single, R As Single) (L/R)
            Arithmetic (Of Single).Power =
                Function(L As Single, R As Single) CSng(L^R)
            Arithmetic (Of Single).IntegerDivide =
                AddressOf IntegerDivide
            Arithmetic (Of Single).Modulus =
                Function(L As Single, R As Single) (L Mod R)
            Arithmetic (Of Single).Equal =
                Function(L As Single, R As Single) (L = R)
            Arithmetic (Of Single).NotEqual =
                Function(L As Single, R As Single) (L <> R)
            Arithmetic (Of Single).LessThan =
                Function(L As Single, R As Single) (L < R)
            Arithmetic (Of Single).LessThanEqual =
                Function(L As Single, R As Single) (L <= R)
            Arithmetic (Of Single).MoreThan =
                Function(L As Single, R As Single) (L > R)
            Arithmetic (Of Single).MoreThanEqual =
                Function(L As Single, R As Single) (L >= R)
        End Sub
    End Module
End Namespace