Namespace Operators

 ''' <summary>
 ''' Provides operators for <c>UInteger</c> data-type.
 ''' </summary>
 <HideModuleName()> Friend Module [UInteger]
  ''' <summary>Performs the unary minus operation (sign negation).</summary>
  Private Function UnaryMinus(ByVal V As UInteger) As UInteger
   If V = 0 Then _
    Return 0

   ' Not possible with unsigned data type.
   Throw New System.OverflowException("Arithmetic operation resulted in an overflow.")

  End Function

  ''' <summary>
  ''' Initializes the functions for <c>UInteger</c> data-type.
  ''' </summary>
  Friend Sub Initialize()
   Arithmetic(Of UInteger).UnaryPlus = _
                                        Function(V As UInteger) (V)
   Arithmetic(Of UInteger).UnaryMinus = _
                                        AddressOf UnaryMinus
   Arithmetic(Of UInteger).Add = _
                                        Function(L As UInteger, R As UInteger) (L + R)
   Arithmetic(Of UInteger).Subtract = _
                                        Function(L As UInteger, R As UInteger) (L - R)
   Arithmetic(Of UInteger).Multiply = _
                                        Function(L As UInteger, R As UInteger) (L * R)
   Arithmetic(Of UInteger).Divide = _
                                        Function(L As UInteger, R As UInteger) (L \ R)
   Arithmetic(Of UInteger).Power = _
                                        Function(L As UInteger, R As UInteger) CUInt(L ^ R)
   Arithmetic(Of UInteger).IntegerDivide = _
                                        Function(L As UInteger, R As UInteger) (L \ R)
   Arithmetic(Of UInteger).Modulus = _
                                        Function(L As UInteger, R As UInteger) (L Mod R)
   Arithmetic(Of UInteger).Equal = _
                                        Function(L As UInteger, R As UInteger) (L = R)
   Arithmetic(Of UInteger).NotEqual = _
                                        Function(L As UInteger, R As UInteger) (L <> R)
   Arithmetic(Of UInteger).LessThan = _
                                        Function(L As UInteger, R As UInteger) (L < R)
   Arithmetic(Of UInteger).LessThanEqual = _
                                        Function(L As UInteger, R As UInteger) (L <= R)
   Arithmetic(Of UInteger).MoreThan = _
                                        Function(L As UInteger, R As UInteger) (L > R)
   Arithmetic(Of UInteger).MoreThanEqual = _
                                        Function(L As UInteger, R As UInteger) (L >= R)
  End Sub

 End Module

End Namespace