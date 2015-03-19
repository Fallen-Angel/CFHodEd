Imports Microsoft.DirectX

Namespace Lights
 ''' <summary>
 ''' Class implementing directional light.
 ''' </summary>
 Public NotInheritable Class DirectionalLight
  Inherits BaseLight

  ' --------------
  ' Class Members.
  ' --------------
  ''' <summary>Direction of this light.</summary>
  Private m_Direction As Vector3

  ' ------------------------
  ' Constructors\Finalizers.
  ' ------------------------
  ' Class constructor.
  Public Sub New()
   ' Set default direction.
   m_Direction = New Vector3(0, 0, -1)

  End Sub

  ' -----------------
  ' Class properties.
  ' -----------------
  ''' <summary>
  ''' Returns the type of light.
  ''' </summary>
  Public Overrides ReadOnly Property Type() As Microsoft.DirectX.Direct3D.LightType
   Get
    Return Direct3D.LightType.Directional

   End Get

  End Property

  ''' <summary>
  ''' Returns\Sets the light direction.
  ''' </summary>
  Public Property Direction() As Vector3
   Get
    Return m_Direction

   End Get

   Set(ByVal value As Vector3)
    ' Check inputs.
    If value.LengthSq() = 0 Then _
     Throw New ArgumentException("Direction vector cannot be of zero length.") _
   : Exit Property

    ' Set the direction.
    m_Direction = value
    m_Direction.Normalize()

   End Set

  End Property

  ' ---------
  ' Operators
  ' ---------
  ' None

  ' -----------------
  ' Member Functions.
  ' -----------------
  ''' <summary>
  ''' Updates and enables the light.
  ''' </summary>
  ''' <param name="_Light">
  ''' The light to update.
  ''' </param>
  ''' <param name="Enable">
  ''' Whether the light is enabled or not. Default value is the
  ''' existing state.
  ''' </param>
  Public Overrides Sub Update(ByVal _Light As Microsoft.DirectX.Direct3D.Light, _
                     Optional ByVal Enable As TriState = TriState.UseDefault)

   If _Light Is Nothing Then _
    Throw New ArgumentNullException("_Light") _
  : Exit Sub

   With _Light
    ' Update the direction if attached to a camera.
    ' Otherwise read the stored direction.
    If m_Camera IsNot Nothing Then _
     .Direction = Vector3.Normalize(m_Camera.GetDirection()) _
    Else _
     .Direction = m_Direction

   End With ' With _Light 

   ' Call the base subroutine.
   MyBase.Update(_Light, Enable)

  End Sub

 End Class

End Namespace
