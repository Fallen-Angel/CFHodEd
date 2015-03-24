Namespace MathFunctions
    ''' <summary>
    ''' Module containing code for arithmetic\math functions (on 
    ''' <c>Double</c> data type).
    ''' </summary>
    <HideModuleName()>
    Friend Module [Double]
        ''' <summary>Math function for <c>Double</c> data type.</summary>
        Private Function UnitStep(ByVal value As Double) As Double
            If value <= 0 Then _
                Return 0 _
                Else _
                Return 1
        End Function

        ''' <summary>Math function for <c>Double</c> data type.</summary>
        Private Function Asinh(ByVal value As Double) As Double
            Return Math.Log(value + Math.Sqrt(value*value + 1))
        End Function

        ''' <summary>Math function for <c>Double</c> data type.</summary>
        Private Function Acosh(ByVal value As Double) As Double
            Return Math.Log(value + Math.Sqrt(value*value - 1))
        End Function

        ''' <summary>Math function for <c>Double</c> data type.</summary>
        Private Function Atanh(ByVal value As Double) As Double
            Return 0.5*Math.Log((1 + value)/(1 - value))
        End Function

        ''' <summary>Math function for <c>Double</c> data type.</summary>
        Private Function Acosech(ByVal value As Double) As Double
            value = 1/value
            Return Math.Log(value + Math.Sqrt(value*value + 1))
        End Function

        ''' <summary>Math function for <c>Double</c> data type.</summary>
        Private Function Asech(ByVal value As Double) As Double
            value = 1/value
            Return Math.Log(value + Math.Sqrt(value*value - 1))
        End Function

        ''' <summary>Math function for <c>Double</c> data type.</summary>
        Private Function Acoth(ByVal value As Double) As Double
            Return 0.5*Math.Log((value + 1)/(value - 1))
        End Function

        ''' <summary>
        ''' Initializes the double functions.
        ''' </summary>
        Friend Sub Initialize()
            Math (Of Double).Sqrt =
                AddressOf Math.Sqrt
            Math (Of Double).Cubrt =
                Function(V As Double) (V^(1/3))
            Math (Of Double).Abs =
                AddressOf Math.Abs
            Math (Of Double).Sign =
                AddressOf Math.Sign
            Math (Of Double).Ceiling =
                AddressOf Math.Ceiling
            Math (Of Double).Floor =
                AddressOf Math.Floor
            Math (Of Double).Truncate =
                AddressOf Math.Truncate
            Math (Of Double).UnitStep =
                AddressOf [Double].UnitStep
            Math (Of Double).Sin =
                AddressOf Math.Sin
            Math (Of Double).Cos =
                AddressOf Math.Cos
            Math (Of Double).Tan =
                AddressOf Math.Tan
            Math (Of Double).Cosec =
                Function(V As Double) (1/Math.Sin(V))
            Math (Of Double).Sec =
                Function(V As Double) (1/Math.Cos(V))
            Math (Of Double).Cot =
                Function(V As Double) (1/Math.Tan(V))
            Math (Of Double).Sinh =
                AddressOf Math.Sinh
            Math (Of Double).Cosh =
                AddressOf Math.Cosh
            Math (Of Double).Tanh =
                AddressOf Math.Tanh
            Math (Of Double).Cosech =
                Function(V As Double) (1/Math.Sinh(V))
            Math (Of Double).Sech =
                Function(V As Double) (1/Math.Cosh(V))
            Math (Of Double).Coth =
                Function(V As Double) (1/Math.Tanh(V))
            Math (Of Double).Asin =
                AddressOf Math.Asin
            Math (Of Double).Acos =
                AddressOf Math.Acos
            Math (Of Double).Atan =
                AddressOf Math.Atan
            Math (Of Double).Acosec =
                Function(V As Double) (Math.Asin(1/V))
            Math (Of Double).Asec =
                Function(V As Double) (Math.Acos(1/V))
            Math (Of Double).Acot =
                Function(V As Double) (Math.Atan(1/V))
            Math (Of Double).Asinh =
                AddressOf [Double].Asinh
            Math (Of Double).Acosh =
                AddressOf [Double].Acosh
            Math (Of Double).Atanh =
                AddressOf [Double].Atanh
            Math (Of Double).Acosech =
                AddressOf [Double].Acosech
            Math (Of Double).Asech =
                AddressOf [Double].Asech
            Math (Of Double).Acoth =
                AddressOf [Double].Acoth
            Math (Of Double).Exp =
                AddressOf Math.Exp
            Math (Of Double).Exp10 =
                Function(V As Double) (10^V)
            Math (Of Double).Log =
                AddressOf Math.Log
            Math (Of Double).Log10 =
                AddressOf Math.Log10
        End Sub
    End Module
End Namespace
