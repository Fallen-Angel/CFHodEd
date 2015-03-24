Namespace MathFunctions
    ''' <summary>
    ''' Module containing code for arithmetic\math functions (on 
    ''' <c>Decimal</c> data type).
    ''' </summary>
    <HideModuleName()>
    Friend Module [Decimal]
        ''' <summary>Math function for <c>Decimal</c> data type.</summary>
        Private Function UnitStep(ByVal value As Decimal) As Decimal
            If value <= 0 Then _
                Return 0 _
                Else _
                Return 1
        End Function

        ''' <summary>Math function for <c>Decimal</c> data type.</summary>
        Private Function Asinh(ByVal value As Decimal) As Decimal
            Return CDec(Math.Log(value + Math.Sqrt(value*value + 1)))
        End Function

        ''' <summary>Math function for <c>Decimal</c> data type.</summary>
        Private Function Acosh(ByVal value As Decimal) As Decimal
            Return CDec(Math.Log(value + Math.Sqrt(value*value - 1)))
        End Function

        ''' <summary>Math function for <c>Decimal</c> data type.</summary>
        Private Function Atanh(ByVal value As Decimal) As Decimal
            Return CDec(0.5*Math.Log((1 + value)/(1 - value)))
        End Function

        ''' <summary>Math function for <c>Decimal</c> data type.</summary>
        Private Function Acosech(ByVal value As Decimal) As Decimal
            value = 1/value
            Return CDec(Math.Log(value + Math.Sqrt(value*value + 1)))
        End Function

        ''' <summary>Math function for <c>Decimal</c> data type.</summary>
        Private Function Asech(ByVal value As Decimal) As Decimal
            value = 1/value
            Return CDec(Math.Log(value + Math.Sqrt(value*value - 1)))
        End Function

        ''' <summary>Math function for <c>Decimal</c> data type.</summary>
        Private Function Acoth(ByVal value As Decimal) As Decimal
            Return CDec(0.5*Math.Log((value + 1)/(value - 1)))
        End Function

        ''' <summary>
        ''' Initializes the decimal functions.
        ''' </summary>
        Friend Sub Initialize()
            Math (Of Decimal).Sqrt =
                Function(V As Decimal) CDec(Math.Sqrt(V))
            Math (Of Decimal).Cubrt =
                Function(V As Decimal) CDec(V^(1/3))
            Math (Of Decimal).Abs =
                Function(V As Decimal) (Math.Abs(V))
            Math (Of Decimal).Sign =
                Function(V As Decimal) (Math.Sign(V))
            Math (Of Decimal).Ceiling =
                Function(V As Decimal) (Math.Ceiling(V))
            Math (Of Decimal).Floor =
                Function(V As Decimal) (Math.Floor(V))
            Math (Of Decimal).Truncate =
                Function(V As Decimal) (Math.Truncate(V))
            Math (Of Decimal).UnitStep =
                AddressOf [Decimal].UnitStep
            Math (Of Decimal).Sin =
                Function(V As Decimal) CDec(Math.Sin(V))
            Math (Of Decimal).Cos =
                Function(V As Decimal) CDec(Math.Cos(V))
            Math (Of Decimal).Tan =
                Function(V As Decimal) CDec(Math.Tan(V))
            Math (Of Decimal).Cosec =
                Function(V As Decimal) CDec(1/Math.Sin(V))
            Math (Of Decimal).Sec =
                Function(V As Decimal) CDec(1/Math.Cos(V))
            Math (Of Decimal).Cot =
                Function(V As Decimal) CDec(1/Math.Tan(V))
            Math (Of Decimal).Sinh =
                Function(V As Decimal) CDec(Math.Sinh(V))
            Math (Of Decimal).Cosh =
                Function(V As Decimal) CDec(Math.Cosh(V))
            Math (Of Decimal).Tanh =
                Function(V As Decimal) CDec(Math.Tanh(V))
            Math (Of Decimal).Cosech =
                Function(V As Decimal) CDec(1/Math.Sinh(V))
            Math (Of Decimal).Sech =
                Function(V As Decimal) CDec(1/Math.Cosh(V))
            Math (Of Decimal).Coth =
                Function(V As Decimal) CDec(1/Math.Tanh(V))
            Math (Of Decimal).Asin =
                Function(V As Decimal) CDec(Math.Asin(V))
            Math (Of Decimal).Acos =
                Function(V As Decimal) CDec(Math.Acos(V))
            Math (Of Decimal).Atan =
                Function(V As Decimal) CDec(Math.Atan(V))
            Math (Of Decimal).Acosec =
                Function(V As Decimal) CDec(Math.Asin(1/V))
            Math (Of Decimal).Asec =
                Function(V As Decimal) CDec(Math.Acos(1/V))
            Math (Of Decimal).Acot =
                Function(V As Decimal) CDec(Math.Atan(1/V))
            Math (Of Decimal).Asinh =
                AddressOf [Decimal].Asinh
            Math (Of Decimal).Acosh =
                AddressOf [Decimal].Acosh
            Math (Of Decimal).Atanh =
                AddressOf [Decimal].Atanh
            Math (Of Decimal).Acosech =
                AddressOf [Decimal].Acosech
            Math (Of Decimal).Asech =
                AddressOf [Decimal].Asech
            Math (Of Decimal).Acoth =
                AddressOf [Decimal].Acoth
            Math (Of Decimal).Exp =
                Function(V As Decimal) CDec(Math.Exp(V))
            Math (Of Decimal).Exp10 =
                Function(V As Decimal) CDec(10^V)
            Math (Of Decimal).Log =
                Function(V As Decimal) CDec(Math.Log(V))
            Math (Of Decimal).Log10 =
                Function(V As Decimal) CDec(Math.Log10(V))
        End Sub
    End Module
End Namespace