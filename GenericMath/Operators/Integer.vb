Namespace Operators

 ''' <summary>
 ''' Provides operators for <c>Integer</c> data-type.
 ''' </summary>
 <HideModuleName()> Friend Module [Integer]
  ''' <summary>
  ''' Initializes the functions for <c>Integer</c> data-type.
  ''' </summary>
  Friend Sub Initialize()
   Arithmetic(Of Integer).UnaryPlus = _
                                        Function(V As Integer) (V)
   Arithmetic(Of Integer).UnaryMinus = _
                                        Function(V As Integer) (-V)
   Arithmetic(Of Integer).Add = _
                                        Function(L As Integer, R As Integer) (L + R)
   Arithmetic(Of Integer).Subtract = _
                                        Function(L As Integer, R As Integer) (L - R)
   Arithmetic(Of Integer).Multiply = _
                                        Function(L As Integer, R As Integer) (L * R)
   Arithmetic(Of Integer).Divide = _
                                        Function(L As Integer, R As Integer) (L \ R)
   Arithmetic(Of Integer).Power = _
                                        Function(L As Integer, R As Integer) CInt(L ^ R)
   Arithmetic(Of Integer).IntegerDivide = _
                                        Function(L As Integer, R As Integer) (L \ R)
   Arithmetic(Of Integer).Modulus = _
                                        Function(L As Integer, R As Integer) (L Mod R)
   Arithmetic(Of Integer).Equal = _
                                        Function(L As Integer, R As Integer) (L = R)
   Arithmetic(Of Integer).NotEqual = _
                                        Function(L As Integer, R As Integer) (L <> R)
   Arithmetic(Of Integer).LessThan = _
                                        Function(L As Integer, R As Integer) (L < R)
   Arithmetic(Of Integer).LessThanEqual = _
                                        Function(L As Integer, R As Integer) (L <= R)
   Arithmetic(Of Integer).MoreThan = _
                                        Function(L As Integer, R As Integer) (L > R)
   Arithmetic(Of Integer).MoreThanEqual = _
                                        Function(L As Integer, R As Integer) (L >= R)

  End Sub

 End Module

End Namespace