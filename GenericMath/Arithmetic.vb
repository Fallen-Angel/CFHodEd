''' <summary>
''' Represents a generic structure which can perform arithmetic on 
''' generic data types; this acts as a wrapper for numeric types.
''' </summary>
''' <typeparam name="T">
''' The type of data type to perform arithmetic on.
''' </typeparam>
''' <remarks>
''' The functions that are intended to be used with the data-type
''' must have been set; there is no exception handling provided.
''' Also, before using this structure, make sure to call 
''' <c>InitializeGenericMath()</c>
''' </remarks>
Public Structure Arithmetic(Of T)
 ' --------------
 ' Class members.
 ' --------------
 ''' <summary>Unary plus operator.</summary>
 Public Shared UnaryPlus As Func(Of T, T)

 ''' <summary>Unary minus operator.</summary>
 Public Shared UnaryMinus As Func(Of T, T)

 ''' <summary>Function to perform addition.</summary>
 Public Shared Add As Func(Of T, T, T)

 ''' <summary>Function to perform subtraction.</summary>
 Public Shared Subtract As Func(Of T, T, T)

 ''' <summary>Function to perform multiplication.</summary>
 Public Shared Multiply As Func(Of T, T, T)

 ''' <summary>Function to perform division.</summary>
 Public Shared Divide As Func(Of T, T, T)

 ''' <summary>Function to perform integer division (return quotient).</summary>
 Public Shared IntegerDivide As Func(Of T, T, T)

 ''' <summary>Function to perform modulus arithmetic (return remainder).</summary>
 Public Shared Modulus As Func(Of T, T, T)

 ''' <summary>Function to perform exponentiation.</summary>
 Public Shared Power As Func(Of T, T, T)

 ''' <summary>'Equality' operator (=).</summary>
 Public Shared Equal As Func(Of T, T, Boolean)

 ''' <summary>'Inequality' operator (&lt;&gt;).</summary>
 Public Shared NotEqual As Func(Of T, T, Boolean)

 ''' <summary>'Less than' operator (&lt;)</summary>
 Public Shared LessThan As Func(Of T, T, Boolean)

 ''' <summary>'Less than or equal to' operator (&lt;=).</summary>
 Public Shared LessThanEqual As Func(Of T, T, Boolean)

 ''' <summary>'More than' operator (&gt;).</summary>
 Public Shared MoreThan As Func(Of T, T, Boolean)

 ''' <summary>'More than or equal to' operator (&gt;=).</summary>
 Public Shared MoreThanEqual As Func(Of T, T, Boolean)

 ''' <summary>
 ''' The data stored in this instance.
 ''' </summary>
 Public Value As T

 ' -----------------------
 ' Constructors\Finalizer.
 ' -----------------------
 ''' <summary>
 ''' Constructor for the <c>Arithmetic</c> structure.
 ''' </summary>
 ''' <param name="_Value">
 ''' The data to store.
 ''' </param>
 Public Sub New(ByVal _Value As T)
  Value = _Value

 End Sub

 ' ----------
 ' Operators.
 ' ----------
 ''' <summary>
 ''' Generic data type(<c>T</c>) to <c>Arithmetic</c> conversion operator.
 ''' </summary>
 ''' <param name="Value">
 ''' The data (in generic data type(<c>T</c>)) to convert.
 ''' </param>
 ''' <returns>
 ''' Converted <c>Arithmetic</c> structure.
 ''' </returns>
 ''' <remarks>
 ''' Operator is widening in the sense that it can never fail (since
 ''' this is not a conversion, but mere copying).
 ''' </remarks>
 Public Shared Widening Operator CType(ByVal Value As T) As Arithmetic(Of T)
  Dim obj As Arithmetic(Of T)
  obj.Value = Value
  Return obj

 End Operator

 ''' <summary>
 ''' <c>Arithmetic</c> to generic data type(<c>T</c>) conversion operator.
 ''' </summary>
 ''' <param name="obj">
 ''' The <c>Arithmetic</c> structure to convert.
 ''' </param>
 ''' <returns>
 ''' Converted data (in generic data type(<c>T</c>)).
 ''' </returns>
 ''' <remarks>
 ''' Operator is widening in the sense that it can never fail (since
 ''' this is not a conversion, but mere copying).
 ''' </remarks>
 Public Shared Widening Operator CType(ByVal obj As Arithmetic(Of T)) As T
  Return obj.Value

 End Operator

 ''' <summary>Unary plus operator.</summary>
 Public Shared Operator +(ByVal V As Arithmetic(Of T)) As Arithmetic(Of T)
  Return Arithmetic(Of T).UnaryPlus(V.Value)

 End Operator

 ''' <summary>
 ''' Unary minus operator.
 ''' </summary>
 ''' <exception cref="OverflowException">
 ''' Thrown when the unary negation operator is used with unsigned generic
 ''' data type (<c>T</c>). In other words this happens for the following
 ''' data types: <c>Byte</c>, <c>UShort</c> <c>UInteger</c>, and <c>ULong</c>.
 ''' </exception>
 Public Shared Operator -(ByVal V As Arithmetic(Of T)) As Arithmetic(Of T)
  Return Arithmetic(Of T).UnaryMinus(V.Value)

 End Operator

 ''' <summary>
 ''' Function to perform addition.
 ''' </summary>
 Public Shared Operator +(ByVal L As Arithmetic(Of T), ByVal R As Arithmetic(Of T)) As Arithmetic(Of T)
  Return Arithmetic(Of T).Add(L.Value, R.Value)

 End Operator

 ''' <summary>
 ''' Function to perform subtraction.
 ''' </summary>
 Public Shared Operator -(ByVal L As Arithmetic(Of T), ByVal R As Arithmetic(Of T)) As Arithmetic(Of T)
  Return Arithmetic(Of T).Subtract(L.Value, R.Value)

 End Operator

 ''' <summary>
 ''' Function to perform multiplication.
 ''' </summary>
 Public Shared Operator *(ByVal L As Arithmetic(Of T), ByVal R As Arithmetic(Of T)) As Arithmetic(Of T)
  Return Arithmetic(Of T).Multiply(L.Value, R.Value)

 End Operator

 ''' <summary>
 ''' Function to perform division.
 ''' </summary>
 Public Shared Operator /(ByVal L As Arithmetic(Of T), ByVal R As Arithmetic(Of T)) As Arithmetic(Of T)
  Return Arithmetic(Of T).Divide(L.Value, R.Value)

 End Operator

 ''' <summary>
 ''' Function to perform integer division (return quotient).
 ''' </summary>
 ''' <remarks>
 ''' NOTE: Avoid on floating-point data types; because they are first converted
 ''' to <c>Long</c> data type before performing integer divide, and then re-converted.
 ''' </remarks>
 Public Shared Operator \(ByVal L As Arithmetic(Of T), ByVal R As Arithmetic(Of T)) As Arithmetic(Of T)
  Return Arithmetic(Of T).IntegerDivide(L.Value, R.Value)

 End Operator

 ''' <summary>
 ''' Function to perform modular division (return remainder).
 ''' </summary>
 Public Shared Operator Mod(ByVal L As Arithmetic(Of T), ByVal R As Arithmetic(Of T)) As Arithmetic(Of T)
  Return Arithmetic(Of T).Modulus(L.Value, R.Value)

 End Operator

 ''' <summary>
 ''' Function to perform exponentiation.
 ''' </summary>
 Public Shared Operator ^(ByVal L As Arithmetic(Of T), ByVal R As Arithmetic(Of T)) As Arithmetic(Of T)
  Return Arithmetic(Of T).Power(L.Value, R.Value)

 End Operator

 ''' <summary>
 ''' 'Equality' operator (=).
 ''' </summary>
 Public Shared Operator =(ByVal L As Arithmetic(Of T), ByVal R As Arithmetic(Of T)) As Boolean
  Return Arithmetic(Of T).Equal(L.Value, R.Value)

 End Operator

 ''' <summary>
 ''' 'Inequality' operator (&lt;&gt;).
 ''' </summary>
 Public Shared Operator <>(ByVal L As Arithmetic(Of T), ByVal R As Arithmetic(Of T)) As Boolean
  Return Arithmetic(Of T).NotEqual(L.Value, R.Value)

 End Operator

 ''' <summary>
 ''' 'Less than' operator (&lt;).
 ''' </summary>
 Public Shared Operator <(ByVal L As Arithmetic(Of T), ByVal R As Arithmetic(Of T)) As Boolean
  Return Arithmetic(Of T).LessThan(L.Value, R.Value)

 End Operator

 ''' <summary>
 ''' 'Less than or equal to' operator (&lt;=).
 ''' </summary>
 Public Shared Operator <=(ByVal L As Arithmetic(Of T), ByVal R As Arithmetic(Of T)) As Boolean
  Return Arithmetic(Of T).LessThanEqual(L.Value, R.Value)

 End Operator

 ''' <summary>
 ''' 'More than' operator (&gt;).
 ''' </summary>
 Public Shared Operator >(ByVal L As Arithmetic(Of T), ByVal R As Arithmetic(Of T)) As Boolean
  Return Arithmetic(Of T).MoreThan(L.Value, R.Value)

 End Operator

 ''' <summary>
 ''' 'More than or equal to' operator (&gt;=).
 ''' </summary>
 Public Shared Operator >=(ByVal L As Arithmetic(Of T), ByVal R As Arithmetic(Of T)) As Boolean
  Return Arithmetic(Of T).MoreThanEqual(L.Value, R.Value)

 End Operator

 ' -----------------
 ' Member functions.
 ' -----------------
 ''' <summary>
 ''' Returns the value in the specified format.
 ''' </summary>
 ''' <typeparam name="_T">
 ''' The type of value to return.
 ''' </typeparam>
 ''' <returns>
 ''' The converted value using 'CType'.
 ''' </returns>
 ''' <remarks>
 ''' This involves boxing and unboxing (i.e. an
 ''' object is used as an intermediate for conversion).
 ''' </remarks>
 Public Function ValueGet(Of _T)() As _T
  Return CType(CObj(Value), _T)

 End Function

 ''' <summary>
 ''' Sets the value in the specified format.
 ''' </summary>
 ''' <typeparam name="_T">
 ''' The type of value to set.
 ''' </typeparam>
 ''' <remarks>
 ''' This involves boxing and unboxing (i.e. an
 ''' object is used as an intermediate for conversion).
 ''' </remarks>
 Public Sub ValueSet(Of _T)(ByVal V As _T)
  Value = CType(CObj(V), T)

 End Sub

End Structure
