Namespace MathFunctions

 ''' <summary>
 ''' Module containing code for arithmetic\math functions (on 
 ''' <c>Decimal</c> data type).
 ''' </summary>
 <HideModuleName()> Friend Module [Decimal]
  ''' <summary>Math function for <c>Decimal</c> data type.</summary>
  Private Function UnitStep(ByVal value As Decimal) As Decimal
   If value <= 0 Then _
    Return 0 _
   Else _
    Return 1

  End Function

  ''' <summary>Math function for <c>Decimal</c> data type.</summary>
  Private Function Asinh(ByVal value As Decimal) As Decimal
   Return CDec(Math.Log(value + Math.Sqrt(value * value + 1)))

  End Function

  ''' <summary>Math function for <c>Decimal</c> data type.</summary>
  Private Function Acosh(ByVal value As Decimal) As Decimal
   Return CDec(Math.Log(value + Math.Sqrt(value * value - 1)))

  End Function

  ''' <summary>Math function for <c>Decimal</c> data type.</summary>
  Private Function Atanh(ByVal value As Decimal) As Decimal
   Return CDec(0.5 * Math.Log((1 + value) / (1 - value)))

  End Function

  ''' <summary>Math function for <c>Decimal</c> data type.</summary>
  Private Function Acosech(ByVal value As Decimal) As Decimal
   value = 1 / value
   Return CDec(Math.Log(value + Math.Sqrt(value * value + 1)))

  End Function

  ''' <summary>Math function for <c>Decimal</c> data type.</summary>
  Private Function Asech(ByVal value As Decimal) As Decimal
   value = 1 / value
   Return CDec(Math.Log(value + Math.Sqrt(value * value - 1)))

  End Function

  ''' <summary>Math function for <c>Decimal</c> data type.</summary>
  Private Function Acoth(ByVal value As Decimal) As Decimal
   Return CDec(0.5 * Math.Log((value + 1) / (value - 1)))

  End Function

  ''' <summary>
  ''' Initializes the decimal functions.
  ''' </summary>
  Friend Sub Initialize()
   Math(Of Decimal).Sqrt = _
                                        Function(V As Decimal) CDec(Math.Sqrt(V))
   Math(Of Decimal).Cubrt = _
                                        Function(V As Decimal) CDec(V ^ (1 / 3))
   Math(Of Decimal).Abs = _
                                        Function(V As Decimal) (Math.Abs(V))
   Math(Of Decimal).Sign = _
                                        Function(V As Decimal) (Math.Sign(V))
   Math(Of Decimal).Ceiling = _
                                        Function(V As Decimal) (Math.Ceiling(V))
   Math(Of Decimal).Floor = _
                                        Function(V As Decimal) (Math.Floor(V))
   Math(Of Decimal).Truncate = _
                                        Function(V As Decimal) (Math.Truncate(V))
   Math(Of Decimal).UnitStep = _
                                        AddressOf [Decimal].UnitStep
   Math(Of Decimal).Sin = _
                                        Function(V As Decimal) CDec(Math.Sin(V))
   Math(Of Decimal).Cos = _
                                        Function(V As Decimal) CDec(Math.Cos(V))
   Math(Of Decimal).Tan = _
                                        Function(V As Decimal) CDec(Math.Tan(V))
   Math(Of Decimal).Cosec = _
                                        Function(V As Decimal) CDec(1 / Math.Sin(V))
   Math(Of Decimal).Sec = _
                                        Function(V As Decimal) CDec(1 / Math.Cos(V))
   Math(Of Decimal).Cot = _
                                        Function(V As Decimal) CDec(1 / Math.Tan(V))
   Math(Of Decimal).Sinh = _
                                        Function(V As Decimal) CDec(Math.Sinh(V))
   Math(Of Decimal).Cosh = _
                                        Function(V As Decimal) CDec(Math.Cosh(V))
   Math(Of Decimal).Tanh = _
                                        Function(V As Decimal) CDec(Math.Tanh(V))
   Math(Of Decimal).Cosech = _
                                        Function(V As Decimal) CDec(1 / Math.Sinh(V))
   Math(Of Decimal).Sech = _
                                        Function(V As Decimal) CDec(1 / Math.Cosh(V))
   Math(Of Decimal).Coth = _
                                        Function(V As Decimal) CDec(1 / Math.Tanh(V))
   Math(Of Decimal).Asin = _
                                        Function(V As Decimal) CDec(Math.Asin(V))
   Math(Of Decimal).Acos = _
                                        Function(V As Decimal) CDec(Math.Acos(V))
   Math(Of Decimal).Atan = _
                                        Function(V As Decimal) CDec(Math.Atan(V))
   Math(Of Decimal).Acosec = _
                                        Function(V As Decimal) CDec(Math.Asin(1 / V))
   Math(Of Decimal).Asec = _
                                        Function(V As Decimal) CDec(Math.Acos(1 / V))
   Math(Of Decimal).Acot = _
                                        Function(V As Decimal) CDec(Math.Atan(1 / V))
   Math(Of Decimal).Asinh = _
                                        AddressOf [Decimal].Asinh
   Math(Of Decimal).Acosh = _
                                        AddressOf [Decimal].Acosh
   Math(Of Decimal).Atanh = _
                                        AddressOf [Decimal].Atanh
   Math(Of Decimal).Acosech = _
                                        AddressOf [Decimal].Acosech
   Math(Of Decimal).Asech = _
                                        AddressOf [Decimal].Asech
   Math(Of Decimal).Acoth = _
                                        AddressOf [Decimal].Acoth
   Math(Of Decimal).Exp = _
                                        Function(V As Decimal) CDec(Math.Exp(V))
   Math(Of Decimal).Exp10 = _
                                        Function(V As Decimal) CDec(10 ^ V)
   Math(Of Decimal).Log = _
                                        Function(V As Decimal) CDec(Math.Log(V))
   Math(Of Decimal).Log10 = _
                                        Function(V As Decimal) CDec(Math.Log10(V))
  End Sub

 End Module

End Namespace