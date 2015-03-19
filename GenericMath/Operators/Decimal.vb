Namespace Operators

 ''' <summary>
 ''' Provides operators for <c>Decimal</c> data-type.
 ''' </summary>
 <HideModuleName()> Friend Module [Decimal]
  ''' <summary>Performs integral division.</summary>
  Private Function IntegerDivide(ByVal L As Decimal, ByVal R As Decimal) As Decimal
   Return CDec(CLng(L) \ CLng(R))

  End Function

  ''' <summary>
  ''' Initializes the functions for <c>Decimal</c> datatype.
  ''' </summary>
  Friend Sub Initialize()
   Arithmetic(Of Decimal).UnaryPlus = _
                                        Function(V As Decimal) (V)
   Arithmetic(Of Decimal).UnaryMinus = _
                                        Function(V As Decimal) (-V)
   Arithmetic(Of Decimal).Add = _
                                        Function(L As Decimal, R As Decimal) (L + R)
   Arithmetic(Of Decimal).Subtract = _
                                        Function(L As Decimal, R As Decimal) (L - R)
   Arithmetic(Of Decimal).Multiply = _
                                        Function(L As Decimal, R As Decimal) (L * R)
   Arithmetic(Of Decimal).Divide = _
                                        Function(L As Decimal, R As Decimal) (L / R)
   Arithmetic(Of Decimal).Power = _
                                        Function(L As Decimal, R As Decimal) CDec(L ^ R)
   Arithmetic(Of Decimal).IntegerDivide = _
                                        AddressOf IntegerDivide
   Arithmetic(Of Decimal).Modulus = _
                                        Function(L As Decimal, R As Decimal) (L Mod R)
   Arithmetic(Of Decimal).Equal = _
                                        Function(L As Decimal, R As Decimal) (L = R)
   Arithmetic(Of Decimal).NotEqual = _
                                        Function(L As Decimal, R As Decimal) (L <> R)
   Arithmetic(Of Decimal).LessThan = _
                                        Function(L As Decimal, R As Decimal) (L < R)
   Arithmetic(Of Decimal).LessThanEqual = _
                                        Function(L As Decimal, R As Decimal) (L <= R)
   Arithmetic(Of Decimal).MoreThan = _
                                        Function(L As Decimal, R As Decimal) (L > R)
   Arithmetic(Of Decimal).MoreThanEqual = _
                                        Function(L As Decimal, R As Decimal) (L >= R)
  End Sub

 End Module

End Namespace

