Namespace Operators

 ''' <summary>
 ''' Provides operators for <c>SByte</c> data-type.
 ''' </summary>
 <HideModuleName()> Friend Module [SByte]
  ''' <summary>
  ''' Initializes the functions for <c>SByte</c> data-type.
  ''' </summary>
  Friend Sub Initialize()
   Arithmetic(Of SByte).UnaryPlus = _
                                        Function(V As SByte) (V)
   Arithmetic(Of SByte).UnaryMinus = _
                                        Function(V As SByte) (-V)
   Arithmetic(Of SByte).Add = _
                                        Function(L As SByte, R As SByte) (L + R)
   Arithmetic(Of SByte).Subtract = _
                                        Function(L As SByte, R As SByte) (L - R)
   Arithmetic(Of SByte).Multiply = _
                                        Function(L As SByte, R As SByte) (L * R)
   Arithmetic(Of SByte).Divide = _
                                        Function(L As SByte, R As SByte) (L \ R)
   Arithmetic(Of SByte).Power = _
                                        Function(L As SByte, R As SByte) CSByte(L ^ R)
   Arithmetic(Of SByte).IntegerDivide = _
                                        Function(L As SByte, R As SByte) (L \ R)
   Arithmetic(Of SByte).Modulus = _
                                        Function(L As SByte, R As SByte) (L Mod R)
   Arithmetic(Of SByte).Equal = _
                                        Function(L As SByte, R As SByte) (L = R)
   Arithmetic(Of SByte).NotEqual = _
                                        Function(L As SByte, R As SByte) (L <> R)
   Arithmetic(Of SByte).LessThan = _
                                        Function(L As SByte, R As SByte) (L < R)
   Arithmetic(Of SByte).LessThanEqual = _
                                        Function(L As SByte, R As SByte) (L <= R)
   Arithmetic(Of SByte).MoreThan = _
                                        Function(L As SByte, R As SByte) (L > R)
   Arithmetic(Of SByte).MoreThanEqual = _
                                        Function(L As SByte, R As SByte) (L >= R)
  End Sub

 End Module

End Namespace
