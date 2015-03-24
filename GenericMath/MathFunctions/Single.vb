Namespace MathFunctions
    ''' <summary>
    ''' Module containing code for arithmetic\math functions (on 
    ''' <c>Single</c> data type).
    ''' </summary>
    <HideModuleName()>
    Friend Module [Single]
        ''' <summary>Math function for <c>Single</c> data type.</summary>
        Private Function UnitStep(ByVal value As Single) As Single
            If value <= 0 Then _
                Return 0 _
                Else _
                Return 1
        End Function

        ''' <summary>Math function for <c>Single</c> data type.</summary>
        Private Function Asinh(ByVal value As Single) As Single
            Return CSng(Math.Log(value + Math.Sqrt(value*value + 1)))
        End Function

        ''' <summary>Math function for <c>Single</c> data type.</summary>
        Private Function Acosh(ByVal value As Single) As Single
            Return CSng(Math.Log(value + Math.Sqrt(value*value - 1)))
        End Function

        ''' <summary>Math function for <c>Single</c> data type.</summary>
        Private Function Atanh(ByVal value As Single) As Single
            Return CSng(0.5*Math.Log((1 + value)/(1 - value)))
        End Function

        ''' <summary>Math function for <c>Single</c> data type.</summary>
        Private Function Acosech(ByVal value As Single) As Single
            value = 1/value
            Return CSng(Math.Log(value + Math.Sqrt(value*value + 1)))
        End Function

        ''' <summary>Math function for <c>Single</c> data type.</summary>
        Private Function Asech(ByVal value As Single) As Single
            value = 1/value
            Return CSng(Math.Log(value + Math.Sqrt(value*value - 1)))
        End Function

        ''' <summary>Math function for <c>Single</c> data type.</summary>
        Private Function Acoth(ByVal value As Single) As Single
            Return CSng(0.5*Math.Log((value + 1)/(value - 1)))
        End Function

        ''' <summary>
        ''' Initializes the single functions.
        ''' </summary>
        Friend Sub Initialize()
            Math (Of Single).Sqrt =
                Function(V As Single) CSng(Math.Sqrt(V))
            Math (Of Single).Cubrt =
                Function(V As Single) CSng(V^(1/3))
            Math (Of Single).Abs =
                Function(V As Single) (Math.Abs(V))
            Math (Of Single).Sign =
                Function(V As Single) (Math.Sign(V))
            Math (Of Single).Ceiling =
                Function(V As Single) CSng(Math.Ceiling(V))
            Math (Of Single).Floor =
                Function(V As Single) CSng(Math.Floor(V))
            Math (Of Single).Truncate =
                Function(V As Single) CSng(Math.Truncate(V))
            Math (Of Single).UnitStep =
                AddressOf [Single].UnitStep
            Math (Of Single).Sin =
                Function(V As Single) CSng(Math.Sin(V))
            Math (Of Single).Cos =
                Function(V As Single) CSng(Math.Cos(V))
            Math (Of Single).Tan =
                Function(V As Single) CSng(Math.Tan(V))
            Math (Of Single).Cosec =
                Function(V As Single) CSng(1/Math.Sin(V))
            Math (Of Single).Sec =
                Function(V As Single) CSng(1/Math.Cos(V))
            Math (Of Single).Cot =
                Function(V As Single) CSng(1/Math.Tan(V))
            Math (Of Single).Sinh =
                Function(V As Single) CSng(Math.Sinh(V))
            Math (Of Single).Cosh =
                Function(V As Single) CSng(Math.Cosh(V))
            Math (Of Single).Tanh =
                Function(V As Single) CSng(Math.Tanh(V))
            Math (Of Single).Cosech =
                Function(V As Single) CSng(1/Math.Sinh(V))
            Math (Of Single).Sech =
                Function(V As Single) CSng(1/Math.Cosh(V))
            Math (Of Single).Coth =
                Function(V As Single) CSng(1/Math.Tanh(V))
            Math (Of Single).Asin =
                Function(V As Single) CSng(Math.Asin(V))
            Math (Of Single).Acos =
                Function(V As Single) CSng(Math.Acos(V))
            Math (Of Single).Atan =
                Function(V As Single) CSng(Math.Atan(V))
            Math (Of Single).Acosec =
                Function(V As Single) CSng(Math.Asin(1/V))
            Math (Of Single).Asec =
                Function(V As Single) CSng(Math.Acos(1/V))
            Math (Of Single).Acot =
                Function(V As Single) CSng(Math.Atan(1/V))
            Math (Of Single).Asinh =
                AddressOf [Single].Asinh
            Math (Of Single).Acosh =
                AddressOf [Single].Acosh
            Math (Of Single).Atanh =
                AddressOf [Single].Atanh
            Math (Of Single).Acosech =
                AddressOf [Single].Acosech
            Math (Of Single).Asech =
                AddressOf [Single].Asech
            Math (Of Single).Acoth =
                AddressOf [Single].Acoth
            Math (Of Single).Exp =
                Function(V As Single) CSng(Math.Exp(V))
            Math (Of Single).Exp10 =
                Function(V As Single) CSng(10^V)
            Math (Of Single).Log =
                Function(V As Single) CSng(Math.Log(V))
            Math (Of Single).Log10 =
                Function(V As Single) CSng(Math.Log10(V))
        End Sub
    End Module
End Namespace
