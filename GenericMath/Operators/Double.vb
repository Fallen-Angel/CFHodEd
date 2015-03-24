Namespace Operators
    ''' <summary>
    ''' Provides operators for <c>Double</c> data-type.
    ''' </summary>
    <HideModuleName()>
    Friend Module [Double]
        ''' <summary>Performs integral division.</summary>
        Private Function IntegerDivide(ByVal L As Double, ByVal R As Double) As Double
            Return CDbl(CLng(L)\CLng(R))
        End Function

        ''' <summary>
        ''' Initializes the functions for <c>Double</c> datatype.
        ''' </summary>
        Friend Sub Initialize()
            Arithmetic (Of Double).UnaryPlus =
                Function(V As Double) (V)
            Arithmetic (Of Double).UnaryMinus =
                Function(V As Double) (- V)
            Arithmetic (Of Double).Add =
                Function(L As Double, R As Double) (L + R)
            Arithmetic (Of Double).Subtract =
                Function(L As Double, R As Double) (L - R)
            Arithmetic (Of Double).Multiply =
                Function(L As Double, R As Double) (L*R)
            Arithmetic (Of Double).Divide =
                Function(L As Double, R As Double) (L/R)
            Arithmetic (Of Double).Power =
                Function(L As Double, R As Double) (L^R)
            Arithmetic (Of Double).IntegerDivide =
                AddressOf IntegerDivide
            Arithmetic (Of Double).Modulus =
                Function(L As Double, R As Double) (L Mod R)
            Arithmetic (Of Double).Equal =
                Function(L As Double, R As Double) (L = R)
            Arithmetic (Of Double).NotEqual =
                Function(L As Double, R As Double) (L <> R)
            Arithmetic (Of Double).LessThan =
                Function(L As Double, R As Double) (L < R)
            Arithmetic (Of Double).LessThanEqual =
                Function(L As Double, R As Double) (L <= R)
            Arithmetic (Of Double).MoreThan =
                Function(L As Double, R As Double) (L > R)
            Arithmetic (Of Double).MoreThanEqual =
                Function(L As Double, R As Double) (L >= R)
        End Sub
    End Module
End Namespace
