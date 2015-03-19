Namespace MathFunctions

 ''' <summary>
 ''' Module containing code for arithmetic\math functions (on 
 ''' <c>Double</c> data type).
 ''' </summary>
 <HideModuleName()> Friend Module [Double]
  ''' <summary>Math function for <c>Double</c> data type.</summary>
  Private Function UnitStep(ByVal value As Double) As Double
   If value <= 0 Then _
    Return 0 _
   Else _
    Return 1

  End Function

  ''' <summary>Math function for <c>Double</c> data type.</summary>
  Private Function Asinh(ByVal value As Double) As Double
   Return Math.Log(value + Math.Sqrt(value * value + 1))

  End Function

  ''' <summary>Math function for <c>Double</c> data type.</summary>
  Private Function Acosh(ByVal value As Double) As Double
   Return Math.Log(value + Math.Sqrt(value * value - 1))

  End Function

  ''' <summary>Math function for <c>Double</c> data type.</summary>
  Private Function Atanh(ByVal value As Double) As Double
   Return 0.5 * Math.Log((1 + value) / (1 - value))

  End Function

  ''' <summary>Math function for <c>Double</c> data type.</summary>
  Private Function Acosech(ByVal value As Double) As Double
   value = 1 / value
   Return Math.Log(value + Math.Sqrt(value * value + 1))

  End Function

  ''' <summary>Math function for <c>Double</c> data type.</summary>
  Private Function Asech(ByVal value As Double) As Double
   value = 1 / value
   Return Math.Log(value + Math.Sqrt(value * value - 1))

  End Function

  ''' <summary>Math function for <c>Double</c> data type.</summary>
  Private Function Acoth(ByVal value As Double) As Double
   Return 0.5 * Math.Log((value + 1) / (value - 1))

  End Function

  ''' <summary>
  ''' Initializes the double functions.
  ''' </summary>
  Friend Sub Initialize()
   Math(Of Double).Sqrt = _
                                        AddressOf Math.Sqrt
   Math(Of Double).Cubrt = _
                                        Function(V As Double) (V ^ (1 / 3))
   Math(Of Double).Abs = _
                                        AddressOf Math.Abs
   Math(Of Double).Sign = _
                                        AddressOf Math.Sign
   Math(Of Double).Ceiling = _
                                        AddressOf Math.Ceiling
   Math(Of Double).Floor = _
                                        AddressOf Math.Floor
   Math(Of Double).Truncate = _
                                        AddressOf Math.Truncate
   Math(Of Double).UnitStep = _
                                        AddressOf [Double].UnitStep
   Math(Of Double).Sin = _
                                        AddressOf Math.Sin
   Math(Of Double).Cos = _
                                        AddressOf Math.Cos
   Math(Of Double).Tan = _
                                        AddressOf Math.Tan
   Math(Of Double).Cosec = _
                                        Function(V As Double) (1 / Math.Sin(V))
   Math(Of Double).Sec = _
                                        Function(V As Double) (1 / Math.Cos(V))
   Math(Of Double).Cot = _
                                        Function(V As Double) (1 / Math.Tan(V))
   Math(Of Double).Sinh = _
                                        AddressOf Math.Sinh
   Math(Of Double).Cosh = _
                                        AddressOf Math.Cosh
   Math(Of Double).Tanh = _
                                        AddressOf Math.Tanh
   Math(Of Double).Cosech = _
                                        Function(V As Double) (1 / Math.Sinh(V))
   Math(Of Double).Sech = _
                                        Function(V As Double) (1 / Math.Cosh(V))
   Math(Of Double).Coth = _
                                        Function(V As Double) (1 / Math.Tanh(V))
   Math(Of Double).Asin = _
                                        AddressOf Math.Asin
   Math(Of Double).Acos = _
                                        AddressOf Math.Acos
   Math(Of Double).Atan = _
                                        AddressOf Math.Atan
   Math(Of Double).Acosec = _
                                        Function(V As Double) (Math.Asin(1 / V))
   Math(Of Double).Asec = _
                                        Function(V As Double) (Math.Acos(1 / V))
   Math(Of Double).Acot = _
                                        Function(V As Double) (Math.Atan(1 / V))
   Math(Of Double).Asinh = _
                                        AddressOf [Double].Asinh
   Math(Of Double).Acosh = _
                                        AddressOf [Double].Acosh
   Math(Of Double).Atanh = _
                                        AddressOf [Double].Atanh
   Math(Of Double).Acosech = _
                                        AddressOf [Double].Acosech
   Math(Of Double).Asech = _
                                        AddressOf [Double].Asech
   Math(Of Double).Acoth = _
                                        AddressOf [Double].Acoth
   Math(Of Double).Exp = _
                                        AddressOf Math.Exp
   Math(Of Double).Exp10 = _
                                        Function(V As Double) (10 ^ V)
   Math(Of Double).Log = _
                                        AddressOf Math.Log
   Math(Of Double).Log10 = _
                                        AddressOf Math.Log10
  End Sub

 End Module

End Namespace
