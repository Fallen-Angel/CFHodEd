''' <summary>
''' Module containing initialization procedure for generic math
''' module.
''' </summary>
Public Module GenericMath
 ''' <summary>
 ''' Performs initialization of the generic math module.
 ''' </summary>
 ''' <remarks>
 ''' Remember to initialize by calling this method before using
 ''' any arithmetic of the number class, otherwise an 
 ''' 'NullReferenceException' will result.
 ''' </remarks>
 Public Sub InitializeGenericMath()
  ' Initialize the operators.
  Operators.Byte.Initialize()
  Operators.Decimal.Initialize()
  Operators.Double.Initialize()
  Operators.Integer.Initialize()
  Operators.Long.Initialize()
  Operators.SByte.Initialize()
  Operators.Short.Initialize()
  Operators.Single.Initialize()
  Operators.UInteger.Initialize()
  Operators.ULong.Initialize()
  Operators.UShort.Initialize()

  ' Initialize the math functions.
  MathFunctions.Decimal.Initialize()
  MathFunctions.Double.Initialize()
  MathFunctions.Single.Initialize()

 End Sub

End Module
