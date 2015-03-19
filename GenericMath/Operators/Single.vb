Namespace Operators

 ''' <summary>
 ''' Provides operators for <c>Single</c> data-type.
 ''' </summary>
 <HideModuleName()> Friend Module [Single]
  ''' <summary>Performs integral division.</summary>
  Private Function IntegerDivide(ByVal L As Single, ByVal R As Single) As Single
   Return CSng(CLng(L) \ CLng(R))

  End Function

  ''' <summary>
  ''' Initializes the functions for <c>Single</c> datatype.
  ''' </summary>
  Friend Sub Initialize()
   Arithmetic(Of Single).UnaryPlus = _
                                        Function(V As Single) (V)
   Arithmetic(Of Single).UnaryMinus = _
                                        Function(V As Single) (-V)
   Arithmetic(Of Single).Add = _
                                        Function(L As Single, R As Single) (L + R)
   Arithmetic(Of Single).Subtract = _
                                        Function(L As Single, R As Single) (L - R)
   Arithmetic(Of Single).Multiply = _
                                        Function(L As Single, R As Single) (L * R)
   Arithmetic(Of Single).Divide = _
                                        Function(L As Single, R As Single) (L / R)
   Arithmetic(Of Single).Power = _
                                        Function(L As Single, R As Single) CSng(L ^ R)
   Arithmetic(Of Single).IntegerDivide = _
                                        AddressOf IntegerDivide
   Arithmetic(Of Single).Modulus = _
                                        Function(L As Single, R As Single) (L Mod R)
   Arithmetic(Of Single).Equal = _
                                        Function(L As Single, R As Single) (L = R)
   Arithmetic(Of Single).NotEqual = _
                                        Function(L As Single, R As Single) (L <> R)
   Arithmetic(Of Single).LessThan = _
                                        Function(L As Single, R As Single) (L < R)
   Arithmetic(Of Single).LessThanEqual = _
                                        Function(L As Single, R As Single) (L <= R)
   Arithmetic(Of Single).MoreThan = _
                                        Function(L As Single, R As Single) (L > R)
   Arithmetic(Of Single).MoreThanEqual = _
                                        Function(L As Single, R As Single) (L >= R)
  End Sub

 End Module

End Namespace