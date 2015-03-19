Namespace Operators

 ''' <summary>
 ''' Provides operators for <c>UShort</c> data-type.
 ''' </summary>
 <HideModuleName()> Friend Module [UShort]
  ''' <summary>Performs the unary minus operation (sign negation).</summary>
  Private Function UnaryMinus(ByVal V As UShort) As UShort
   If V = 0 Then _
    Return 0

   ' Not possible with unsigned data type.
   Throw New System.OverflowException("Arithmetic operation resulted in an overflow.")

  End Function

  ''' <summary>
  ''' Initializes the functions for <c>UShort</c> data-type.
  ''' </summary>
  Friend Sub Initialize()
   Arithmetic(Of UShort).UnaryPlus = _
                                        Function(V As UShort) (V)
   Arithmetic(Of UShort).UnaryMinus = _
                                        AddressOf UnaryMinus
   Arithmetic(Of UShort).Add = _
                                        Function(L As UShort, R As UShort) (L + R)
   Arithmetic(Of UShort).Subtract = _
                                        Function(L As UShort, R As UShort) (L - R)
   Arithmetic(Of UShort).Multiply = _
                                        Function(L As UShort, R As UShort) (L * R)
   Arithmetic(Of UShort).Divide = _
                                        Function(L As UShort, R As UShort) (L \ R)
   Arithmetic(Of UShort).Power = _
                                        Function(L As UShort, R As UShort) CUShort(L ^ R)
   Arithmetic(Of UShort).IntegerDivide = _
                                        Function(L As UShort, R As UShort) (L \ R)
   Arithmetic(Of UShort).Modulus = _
                                        Function(L As UShort, R As UShort) (L Mod R)
   Arithmetic(Of UShort).Equal = _
                                        Function(L As UShort, R As UShort) (L = R)
   Arithmetic(Of UShort).NotEqual = _
                                        Function(L As UShort, R As UShort) (L <> R)
   Arithmetic(Of UShort).LessThan = _
                                        Function(L As UShort, R As UShort) (L < R)
   Arithmetic(Of UShort).LessThanEqual = _
                                        Function(L As UShort, R As UShort) (L <= R)
   Arithmetic(Of UShort).MoreThan = _
                                        Function(L As UShort, R As UShort) (L > R)
   Arithmetic(Of UShort).MoreThanEqual = _
                                        Function(L As UShort, R As UShort) (L >= R)
  End Sub

 End Module

End Namespace
