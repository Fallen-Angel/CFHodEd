Namespace Operators

 ''' <summary>
 ''' Provides operators for <c>ULong</c> data-type.
 ''' </summary>
 <HideModuleName()> Friend Module [ULong]
  ''' <summary>Performs the unary minus operation (sign negation).</summary>
  Private Function UnaryMinus(ByVal V As ULong) As ULong
   If V = 0 Then _
    Return 0

   ' Not possible with unsigned data type.
   Throw New System.OverflowException("Arithmetic operation resulted in an overflow.")

  End Function

  ''' <summary>
  ''' Initializes the functions for <c>ULong</c> data-type.
  ''' </summary>
  Friend Sub Initialize()
   Arithmetic(Of ULong).UnaryPlus = _
                                        Function(V As ULong) (V)
   Arithmetic(Of ULong).UnaryMinus = _
                                        AddressOf UnaryMinus
   Arithmetic(Of ULong).Add = _
                                        Function(L As ULong, R As ULong) (L + R)
   Arithmetic(Of ULong).Subtract = _
                                        Function(L As ULong, R As ULong) (L - R)
   Arithmetic(Of ULong).Multiply = _
                                        Function(L As ULong, R As ULong) (L * R)
   Arithmetic(Of ULong).Divide = _
                                        Function(L As ULong, R As ULong) (L \ R)
   Arithmetic(Of ULong).Power = _
                                        Function(L As ULong, R As ULong) CULng(L ^ R)
   Arithmetic(Of ULong).IntegerDivide = _
                                        Function(L As ULong, R As ULong) (L \ R)
   Arithmetic(Of ULong).Modulus = _
                                        Function(L As ULong, R As ULong) (L Mod R)
   Arithmetic(Of ULong).Equal = _
                                        Function(L As ULong, R As ULong) (L = R)
   Arithmetic(Of ULong).NotEqual = _
                                        Function(L As ULong, R As ULong) (L <> R)
   Arithmetic(Of ULong).LessThan = _
                                        Function(L As ULong, R As ULong) (L < R)
   Arithmetic(Of ULong).LessThanEqual = _
                                        Function(L As ULong, R As ULong) (L <= R)
   Arithmetic(Of ULong).MoreThan = _
                                        Function(L As ULong, R As ULong) (L > R)
   Arithmetic(Of ULong).MoreThanEqual = _
                                        Function(L As ULong, R As ULong) (L >= R)
  End Sub

 End Module

End Namespace