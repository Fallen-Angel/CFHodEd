''' <summary>
''' Class acting as an interface for math functions on 
''' different data types.
''' </summary>
''' <typeparam name="T">
''' Data on which to operate upon.
''' </typeparam>
Public NotInheritable Class Math(Of T)
 ' ---------------
 ' POWER FUNCTIONS
 ' ---------------
 ''' <summary>Math function for custom data type.</summary>
 Public Shared Sqrt As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Cubrt As Func(Of T, T)

 ' -----------------
 ' SPECIAL FUNCTIONS
 ' -----------------
 ''' <summary>Math function for custom data type.</summary>
 Public Shared Abs As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Sign As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Ceiling As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Floor As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Truncate As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared UnitStep As Func(Of T, T)

 ' -----------------------
 ' TRIGONOMETRIC FUNCTIONS
 ' -----------------------
 ''' <summary>Math function for custom data type.</summary>
 Public Shared Sin As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Cos As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Tan As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Cosec As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Sec As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Cot As Func(Of T, T)

 ' --------------------
 ' HYPERBOLIC FUNCTIONS
 ' --------------------
 ''' <summary>Math function for custom data type.</summary>
 Public Shared Sinh As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Cosh As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Tanh As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Cosech As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Sech As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Coth As Func(Of T, T)

 ' -------------------------------
 ' INVERSE TRIGONOMETRIC FUNCTIONS
 ' -------------------------------
 ''' <summary>Math function for custom data type.</summary>
 Public Shared Asin As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Acos As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Atan As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Acosec As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Asec As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Acot As Func(Of T, T)

 ' ----------------------------
 ' INVERSE HYPERBOLIC FUNCTIONS
 ' ----------------------------
 ''' <summary>Math function for custom data type.</summary>
 Public Shared Asinh As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Acosh As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Atanh As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Acosech As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Asech As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Acoth As Func(Of T, T)

 ' ---------------------------------
 ' EXPONENTIAL\LOGARITHMIC FUNCTIONS
 ' ---------------------------------
 ''' <summary>Math function for custom data type.</summary>
 Public Shared Exp As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Exp10 As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Log As Func(Of T, T)

 ''' <summary>Math function for custom data type.</summary>
 Public Shared Log10 As Func(Of T, T)

End Class
