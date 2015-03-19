''' <summary>
''' Defines the generalized properties that a material implements to
''' create a suitable material type for the generic mesh class.
''' </summary>
Public Interface IMaterial
 Inherits IEquatable(Of IMaterial)

 ''' <summary>
 ''' Sets the default properties.
 ''' </summary>
 Sub Initialize()

 ''' <summary>
 ''' Applies the material.
 ''' </summary>
 ''' <param name="_Device">
 ''' The device whose states are set to apply the material.
 ''' </param>
 Sub Apply(ByVal _Device As Device)

 ''' <summary>
 ''' Removes the effect of the material; i.e. sets the 
 ''' device to it's normal state (only for the states changed).
 ''' </summary>
 ''' <param name="_Device">
 ''' The device whose states are set to normal states.
 ''' </param>
 Sub Reset(ByVal _Device As Device)

End Interface
