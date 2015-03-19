Namespace Operators

 ''' <summary>
 ''' Provides operators for <c>Double</c> data-type.
 ''' </summary>
 <HideModuleName()> Friend Module [Double]
  ''' <summary>Performs integral division.</summary>
  Private Function IntegerDivide(ByVal L As Double, ByVal R As Double) As Double
   Return CDbl(CLng(L) \ CLng(R))

  End Function

  ''' <summary>
  ''' Initializes the functions for <c>Double</c> datatype.
  ''' </summary>
  Friend Sub Initialize()
   Arithmetic(Of Double).UnaryPlus = _
                                        Function(V As Double) (V)
   Arithmetic(Of Double).UnaryMinus = _
                                        Function(V As Double) (-V)
   Arithmetic(Of Double).Add = _
                                        Function(L As Double, R As Double) (L + R)
   Arithmetic(Of Double).Subtract = _
                                        Function(L As Double, R As Double) (L - R)
   Arithmetic(Of Double).Multiply = _
                                        Function(L As Double, R As Double) (L * R)
   Arithmetic(Of Double).Divide = _
                                        Function(L As Double, R As Double) (L / R)
   Arithmetic(Of Double).Power = _
                                        Function(L As Double, R As Double) (L ^ R)
   Arithmetic(Of Double).IntegerDivide = _
                                        AddressOf IntegerDivide
   Arithmetic(Of Double).Modulus = _
                                        Function(L As Double, R As Double) (L Mod R)
   Arithmetic(Of Double).Equal = _
                                        Function(L As Double, R As Double) (L = R)
   Arithmetic(Of Double).NotEqual = _
                                        Function(L As Double, R As Double) (L <> R)
   Arithmetic(Of Double).LessThan = _
                                        Function(L As Double, R As Double) (L < R)
   Arithmetic(Of Double).LessThanEqual = _
                                        Function(L As Double, R As Double) (L <= R)
   Arithmetic(Of Double).MoreThan = _
                                        Function(L As Double, R As Double) (L > R)
   Arithmetic(Of Double).MoreThanEqual = _
                                        Function(L As Double, R As Double) (L >= R)
  End Sub

 End Module

End Namespace
