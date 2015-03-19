Namespace Operators

 ''' <summary>
 ''' Provides operators for <c>Long</c> data-type.
 ''' </summary>
 <HideModuleName()> Friend Module [Long]
  ''' <summary>
  ''' Initializes the functions for <c>Long</c> data-type.
  ''' </summary>
  Friend Sub Initialize()
   Arithmetic(Of Long).UnaryPlus = _
                                        Function(V As Long) (V)
   Arithmetic(Of Long).UnaryMinus = _
                                        Function(V As Long) (-V)
   Arithmetic(Of Long).Add = _
                                        Function(L As Long, R As Long) (L + R)
   Arithmetic(Of Long).Subtract = _
                                        Function(L As Long, R As Long) (L - R)
   Arithmetic(Of Long).Multiply = _
                                        Function(L As Long, R As Long) (L * R)
   Arithmetic(Of Long).Divide = _
                                        Function(L As Long, R As Long) (L \ R)
   Arithmetic(Of Long).Power = _
                                        Function(L As Long, R As Long) CLng(L ^ R)
   Arithmetic(Of Long).IntegerDivide = _
                                        Function(L As Long, R As Long) (L \ R)
   Arithmetic(Of Long).Modulus = _
                                        Function(L As Long, R As Long) (L Mod R)
   Arithmetic(Of Long).Equal = _
                                        Function(L As Long, R As Long) (L = R)
   Arithmetic(Of Long).NotEqual = _
                                        Function(L As Long, R As Long) (L <> R)
   Arithmetic(Of Long).LessThan = _
                                        Function(L As Long, R As Long) (L < R)
   Arithmetic(Of Long).LessThanEqual = _
                                        Function(L As Long, R As Long) (L <= R)
   Arithmetic(Of Long).MoreThan = _
                                        Function(L As Long, R As Long) (L > R)
   Arithmetic(Of Long).MoreThanEqual = _
                                        Function(L As Long, R As Long) (L >= R)

  End Sub

 End Module

End Namespace