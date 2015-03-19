Namespace Operators

 ''' <summary>
 ''' Provides operators for <c>Short</c> data-type.
 ''' </summary>
 <HideModuleName()> Friend Module [Short]
  ''' <summary>
  ''' Initializes the functions for <c>Short</c> data-type.
  ''' </summary>
  Friend Sub Initialize()
   Arithmetic(Of Short).UnaryPlus = _
                                        Function(V As Short) (V)
   Arithmetic(Of Short).UnaryMinus = _
                                        Function(V As Short) (-V)
   Arithmetic(Of Short).Add = _
                                        Function(L As Short, R As Short) (L + R)
   Arithmetic(Of Short).Subtract = _
                                        Function(L As Short, R As Short) (L - R)
   Arithmetic(Of Short).Multiply = _
                                        Function(L As Short, R As Short) (L * R)
   Arithmetic(Of Short).Divide = _
                                        Function(L As Short, R As Short) (L \ R)
   Arithmetic(Of Short).Power = _
                                        Function(L As Short, R As Short) CShort(L ^ R)
   Arithmetic(Of Short).IntegerDivide = _
                                        Function(L As Short, R As Short) (L \ R)
   Arithmetic(Of Short).Modulus = _
                                        Function(L As Short, R As Short) (L Mod R)
   Arithmetic(Of Short).Equal = _
                                        Function(L As Short, R As Short) (L = R)
   Arithmetic(Of Short).NotEqual = _
                                        Function(L As Short, R As Short) (L <> R)
   Arithmetic(Of Short).LessThan = _
                                        Function(L As Short, R As Short) (L < R)
   Arithmetic(Of Short).LessThanEqual = _
                                        Function(L As Short, R As Short) (L <= R)
   Arithmetic(Of Short).MoreThan = _
                                        Function(L As Short, R As Short) (L > R)
   Arithmetic(Of Short).MoreThanEqual = _
                                        Function(L As Short, R As Short) (L >= R)
  End Sub

 End Module

End Namespace
