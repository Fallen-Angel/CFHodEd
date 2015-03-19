Namespace MathFunctions

 ''' <summary>
 ''' Module containing code for arithmetic\math functions (on 
 ''' <c>Single</c> data type).
 ''' </summary>
 <HideModuleName()> Friend Module [Single]
  ''' <summary>Math function for <c>Single</c> data type.</summary>
  Private Function UnitStep(ByVal value As Single) As Single
   If value <= 0 Then _
    Return 0 _
   Else _
    Return 1

  End Function

  ''' <summary>Math function for <c>Single</c> data type.</summary>
  Private Function Asinh(ByVal value As Single) As Single
   Return CSng(Math.Log(value + Math.Sqrt(value * value + 1)))

  End Function

  ''' <summary>Math function for <c>Single</c> data type.</summary>
  Private Function Acosh(ByVal value As Single) As Single
   Return CSng(Math.Log(value + Math.Sqrt(value * value - 1)))

  End Function

  ''' <summary>Math function for <c>Single</c> data type.</summary>
  Private Function Atanh(ByVal value As Single) As Single
   Return CSng(0.5 * Math.Log((1 + value) / (1 - value)))

  End Function

  ''' <summary>Math function for <c>Single</c> data type.</summary>
  Private Function Acosech(ByVal value As Single) As Single
   value = 1 / value
   Return CSng(Math.Log(value + Math.Sqrt(value * value + 1)))

  End Function

  ''' <summary>Math function for <c>Single</c> data type.</summary>
  Private Function Asech(ByVal value As Single) As Single
   value = 1 / value
   Return CSng(Math.Log(value + Math.Sqrt(value * value - 1)))

  End Function

  ''' <summary>Math function for <c>Single</c> data type.</summary>
  Private Function Acoth(ByVal value As Single) As Single
   Return CSng(0.5 * Math.Log((value + 1) / (value - 1)))

  End Function

  ''' <summary>
  ''' Initializes the single functions.
  ''' </summary>
  Friend Sub Initialize()
   Math(Of Single).Sqrt = _
                                        Function(V As Single) CSng(Math.Sqrt(V))
   Math(Of Single).Cubrt = _
                                        Function(V As Single) CSng(V ^ (1 / 3))
   Math(Of Single).Abs = _
                                        Function(V As Single) (Math.Abs(V))
   Math(Of Single).Sign = _
                                        Function(V As Single) (Math.Sign(V))
   Math(Of Single).Ceiling = _
                                        Function(V As Single) CSng(Math.Ceiling(V))
   Math(Of Single).Floor = _
                                        Function(V As Single) CSng(Math.Floor(V))
   Math(Of Single).Truncate = _
                                        Function(V As Single) CSng(Math.Truncate(V))
   Math(Of Single).UnitStep = _
                                        AddressOf [Single].UnitStep
   Math(Of Single).Sin = _
                                        Function(V As Single) CSng(Math.Sin(V))
   Math(Of Single).Cos = _
                                        Function(V As Single) CSng(Math.Cos(V))
   Math(Of Single).Tan = _
                                        Function(V As Single) CSng(Math.Tan(V))
   Math(Of Single).Cosec = _
                                        Function(V As Single) CSng(1 / Math.Sin(V))
   Math(Of Single).Sec = _
                                        Function(V As Single) CSng(1 / Math.Cos(V))
   Math(Of Single).Cot = _
                                        Function(V As Single) CSng(1 / Math.Tan(V))
   Math(Of Single).Sinh = _
                                        Function(V As Single) CSng(Math.Sinh(V))
   Math(Of Single).Cosh = _
                                        Function(V As Single) CSng(Math.Cosh(V))
   Math(Of Single).Tanh = _
                                        Function(V As Single) CSng(Math.Tanh(V))
   Math(Of Single).Cosech = _
                                        Function(V As Single) CSng(1 / Math.Sinh(V))
   Math(Of Single).Sech = _
                                        Function(V As Single) CSng(1 / Math.Cosh(V))
   Math(Of Single).Coth = _
                                        Function(V As Single) CSng(1 / Math.Tanh(V))
   Math(Of Single).Asin = _
                                        Function(V As Single) CSng(Math.Asin(V))
   Math(Of Single).Acos = _
                                        Function(V As Single) CSng(Math.Acos(V))
   Math(Of Single).Atan = _
                                        Function(V As Single) CSng(Math.Atan(V))
   Math(Of Single).Acosec = _
                                        Function(V As Single) CSng(Math.Asin(1 / V))
   Math(Of Single).Asec = _
                                        Function(V As Single) CSng(Math.Acos(1 / V))
   Math(Of Single).Acot = _
                                        Function(V As Single) CSng(Math.Atan(1 / V))
   Math(Of Single).Asinh = _
                                        AddressOf [Single].Asinh
   Math(Of Single).Acosh = _
                                        AddressOf [Single].Acosh
   Math(Of Single).Atanh = _
                                        AddressOf [Single].Atanh
   Math(Of Single).Acosech = _
                                        AddressOf [Single].Acosech
   Math(Of Single).Asech = _
                                        AddressOf [Single].Asech
   Math(Of Single).Acoth = _
                                        AddressOf [Single].Acoth
   Math(Of Single).Exp = _
                                        Function(V As Single) CSng(Math.Exp(V))
   Math(Of Single).Exp10 = _
                                        Function(V As Single) CSng(10 ^ V)
   Math(Of Single).Log = _
                                        Function(V As Single) CSng(Math.Log(V))
   Math(Of Single).Log10 = _
                                        Function(V As Single) CSng(Math.Log10(V))
  End Sub

 End Module

End Namespace
