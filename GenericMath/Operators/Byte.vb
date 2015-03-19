Namespace Operators
 ''' <summary>
 ''' Provides operators for <c>Byte</c> data-type.
 ''' </summary>
 <HideModuleName()> Friend Module [Byte]
  ''' <summary>Performs the unary minus operation (sign negation).</summary>
  Private Function UnaryMinus(ByVal V As Byte) As Byte
   If V = 0 Then _
    Return 0

   ' Not possible with unsigned data type.
   Throw New System.OverflowException("Arithmetic operation resulted in an overflow.")

  End Function

  ''' <summary>
  ''' Initializes the functions for <c>Byte</c> data-type.
  ''' </summary>
  Friend Sub Initialize()
   Arithmetic(Of Byte).UnaryPlus = _
                                        Function(V As Byte) (V)
   Arithmetic(Of Byte).UnaryMinus = _
                                        AddressOf UnaryMinus
   Arithmetic(Of Byte).Add = _
                                        Function(L As Byte, R As Byte) (L + R)
   Arithmetic(Of Byte).Subtract = _
                                        Function(L As Byte, R As Byte) (L - R)
   Arithmetic(Of Byte).Multiply = _
                                        Function(L As Byte, R As Byte) (L * R)
   Arithmetic(Of Byte).Divide = _
                                        Function(L As Byte, R As Byte) (L \ R)
   Arithmetic(Of Byte).Power = _
                                        Function(L As Byte, R As Byte) CByte(L ^ R)
   Arithmetic(Of Byte).IntegerDivide = _
                                        Function(L As Byte, R As Byte) (L \ R)
   Arithmetic(Of Byte).Modulus = _
                                        Function(L As Byte, R As Byte) (L Mod R)
   Arithmetic(Of Byte).Equal = _
                                        Function(L As Byte, R As Byte) (L = R)
   Arithmetic(Of Byte).NotEqual = _
                                        Function(L As Byte, R As Byte) (L <> R)
   Arithmetic(Of Byte).LessThan = _
                                        Function(L As Byte, R As Byte) (L < R)
   Arithmetic(Of Byte).LessThanEqual = _
                                        Function(L As Byte, R As Byte) (L <= R)
   Arithmetic(Of Byte).MoreThan = _
                                        Function(L As Byte, R As Byte) (L > R)
   Arithmetic(Of Byte).MoreThanEqual = _
                                        Function(L As Byte, R As Byte) (L >= R)
  End Sub

 End Module

End Namespace
