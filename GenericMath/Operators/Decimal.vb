Namespace Operators
    ''' <summary>
    ''' Provides operators for <c>Decimal</c> data-type.
    ''' </summary>
    <HideModuleName()>
    Friend Module [Decimal]
        ''' <summary>Performs integral division.</summary>
        Private Function IntegerDivide(ByVal L As Decimal, ByVal R As Decimal) As Decimal
            Return CDec(CLng(L)\CLng(R))
        End Function

        ''' <summary>
        ''' Initializes the functions for <c>Decimal</c> datatype.
        ''' </summary>
        Friend Sub Initialize()
            Arithmetic (Of Decimal).UnaryPlus =
                Function(V As Decimal) (V)
            Arithmetic (Of Decimal).UnaryMinus =
                Function(V As Decimal) (- V)
            Arithmetic (Of Decimal).Add =
                Function(L As Decimal, R As Decimal) (L + R)
            Arithmetic (Of Decimal).Subtract =
                Function(L As Decimal, R As Decimal) (L - R)
            Arithmetic (Of Decimal).Multiply =
                Function(L As Decimal, R As Decimal) (L*R)
            Arithmetic (Of Decimal).Divide =
                Function(L As Decimal, R As Decimal) (L/R)
            Arithmetic (Of Decimal).Power =
                Function(L As Decimal, R As Decimal) CDec(L^R)
            Arithmetic (Of Decimal).IntegerDivide =
                AddressOf IntegerDivide
            Arithmetic (Of Decimal).Modulus =
                Function(L As Decimal, R As Decimal) (L Mod R)
            Arithmetic (Of Decimal).Equal =
                Function(L As Decimal, R As Decimal) (L = R)
            Arithmetic (Of Decimal).NotEqual =
                Function(L As Decimal, R As Decimal) (L <> R)
            Arithmetic (Of Decimal).LessThan =
                Function(L As Decimal, R As Decimal) (L < R)
            Arithmetic (Of Decimal).LessThanEqual =
                Function(L As Decimal, R As Decimal) (L <= R)
            Arithmetic (Of Decimal).MoreThan =
                Function(L As Decimal, R As Decimal) (L > R)
            Arithmetic (Of Decimal).MoreThanEqual =
                Function(L As Decimal, R As Decimal) (L >= R)
        End Sub
    End Module
End Namespace

