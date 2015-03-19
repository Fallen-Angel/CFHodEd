Imports Microsoft.DirectX

Namespace Lights
 ''' <summary>
 ''' Class implementing point light.
 ''' </summary>
 Public NotInheritable Class PointLight
  Inherits BaseLight

  ' --------------
  ' Class Members.
  ' --------------
  ''' <summary>Position of the light.</summary>
  Private m_Position As Vector3

  ''' <summary>Attenuation factor, constant term.</summary>
  Private m_Attenuation0 As Single

  ''' <summary>Attenuation factor, d term.</summary>
  Private m_Attenuation1 As Single

  ''' <summary>Attenuation factor, d² term.</summary>
  Private m_Attenuation2 As Single

  ''' <summary>Range of light.</summary>
  Private m_Range As Single

  ' ------------------------
  ' Constructors\Finalizers.
  ' ------------------------
  ''' <summary>
  ''' Class constructor.
  ''' </summary>
  Public Sub New()
   ' Set default attenuation.
   m_Attenuation0 = 1

   ' Set default position.
   m_Position = New Vector3(0, 0, 500)

   ' Set default range.
   m_Range = 1000


  End Sub

  ' -----------------
  ' Class properties.
  ' -----------------
  ''' <summary>
  ''' Returns the type of light.
  ''' </summary>
  Public Overrides ReadOnly Property Type() As Microsoft.DirectX.Direct3D.LightType
   Get
    Return Direct3D.LightType.Point

   End Get

  End Property

  ''' <summary>
  ''' Returns\Sets the position of the light.
  ''' </summary>
  Public Property Position() As Vector3
   Get
    Return m_Position

   End Get

   Set(ByVal value As Vector3)
    m_Position = value

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets the Attenuation factor (constant term).
  ''' </summary>
  Public Property Attenuation0() As Single
   Get
    Return m_Attenuation0

   End Get

   Set(ByVal value As Single)
    ' Check inputs.
    If value < 0 Then _
     Throw New ArgumentOutOfRangeException("value") _
   : Exit Property

    ' Set value.
    m_Attenuation0 = value

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets the Attenuation factor (d term).
  ''' </summary>
  Public Property Attenuation1() As Single
   Get
    Return m_Attenuation1

   End Get

   Set(ByVal value As Single)
    ' Check inputs.
    If value < 0 Then _
     Throw New ArgumentOutOfRangeException("value") _
   : Exit Property

    ' Set value.
    m_Attenuation1 = value
   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets the Attenuation factor (d² term).
  ''' </summary>
  Public Property Attenuation2() As Single
   Get
    Return m_Attenuation2

   End Get

   Set(ByVal value As Single)
    ' Check inputs.
    If value < 0 Then _
     Throw New ArgumentOutOfRangeException("value") _
   : Exit Property

    ' Set value.
    m_Attenuation2 = value

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets the range of light.
  ''' </summary>
  Public Property Range() As Single
   Get
    Return m_Range

   End Get

   Set(ByVal value As Single)
    If value <= 0 Then _
     Throw New ArgumentOutOfRangeException("value") _
   : Exit Property

    m_Range = value

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
    ' Update the position if attached to a camera.
    ' Otherwise read the stored position.
    If m_Camera IsNot Nothing Then _
     .Position = m_Camera.GetPosition() _
    Else _
     .Position = m_Position

    ' Set attenuation factor.
    .Attenuation0 = m_Attenuation0
    .Attenuation1 = m_Attenuation1
    .Attenuation2 = m_Attenuation2

    ' Set the range.
    .Range = m_Range
   End With ' With _Light

   ' Call base method.
   MyBase.Update(_Light, Enable)

  End Sub

 End Class

End Namespace
