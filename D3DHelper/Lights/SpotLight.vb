Imports Microsoft.DirectX

Namespace Lights
    ''' <summary>
    ''' Class implementing spot light.
    ''' </summary>
    Public NotInheritable Class SpotLight
        Inherits BaseLight

        ' --------------
        ' Class Members.
        ' --------------
        ''' <summary>Position of the light.</summary>
        Private m_Position As Vector3

        ''' <summary>Direction of the light</summary>
        Private m_Direction As Vector3

        ''' <summary>Attenuation factor, constant term.</summary>
        Private m_Attenuation0 As Single

        ''' <summary>Attenuation factor, d term.</summary>
        Private m_Attenuation1 As Single

        ''' <summary>Attenuation factor, d² term.</summary>
        Private m_Attenuation2 As Single

        ''' <summary>Range of light.</summary>
        Private m_Range As Single

        ''' <summary>Fall-off factor.</summary>
        Private m_Falloff As Single

        ''' <summary>Inner cone angle.</summary>
        Private m_InnerConeAngle As Single

        ''' <summary>Outer cone angle.</summary>
        Private m_OuterConeAngle As Single

        ' ------------------------
        ' Constructors\Finalizers.
        ' ------------------------
        ''' <summary>
        ''' Class constructor.
        ''' </summary>
        Public Sub New()
            ' Set the position and direction.
            m_Position = New Vector3(0, 0, 500)
            m_Direction = New Vector3(0, 0, - 1)

            ' Set the attenuation.
            m_Attenuation0 = 1
            m_Attenuation1 = 0
            m_Attenuation2 = 0

            ' Set the range.
            m_Range = 1000

            ' Set the spot light values.
            m_Falloff = 1.0
            m_InnerConeAngle = CSng(Math.PI/3)
            m_OuterConeAngle = CSng(Math.PI/2)
        End Sub

        ' -----------------
        ' Class properties.
        ' -----------------
        ''' <summary>
        ''' Returns the type of light.
        ''' </summary>
        Public Overrides ReadOnly Property Type() As Microsoft.DirectX.Direct3D.LightType
            Get
                Return Direct3D.LightType.Spot
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
                    Throw New ArgumentException("value") _
                        : Exit Property

                m_Range = value
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets the fall-off factor.
        ''' </summary>
        Public Property Falloff() As Single
            Get
                Return m_Falloff
            End Get

            Set(ByVal value As Single)
                m_Falloff = value
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets the inner cone angle.
        ''' </summary>
        Public Property InnerConeAngle() As Single
            Get
                Return m_InnerConeAngle
            End Get

            Set(ByVal value As Single)
                If (value < 0) OrElse (value > Math.PI) Then _
                    Throw New ArgumentOutOfRangeException("value") _
                        : Exit Property

                m_InnerConeAngle = value
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets the outer cone angle.
        ''' </summary>
        Public Property OuterConeAngle() As Single
            Get
                Return m_OuterConeAngle
            End Get

            Set(ByVal value As Single)
                If (value < 0) OrElse (value > Math.PI) Then _
                    Throw New ArgumentOutOfRangeException("value > Math.PI") _
                        : Exit Property

                m_OuterConeAngle = value
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
        Public Overrides Sub Update(ByVal _Light As Microsoft.DirectX.Direct3D.Light,
                                    Optional ByVal Enable As TriState = TriState.UseDefault)

            If _Light Is Nothing Then _
                Throw New ArgumentNullException("_Light") _
                    : Exit Sub

            With _Light
                ' Update the position if attached to a camera.
                ' Otherwise read the stored position.
                If m_Camera IsNot Nothing Then _
                    .Position = m_Camera.GetPosition() _
                        : .Direction = m_Camera.GetDirection() _
                        : .Direction.Normalize() _
                    Else _
                    .Position = m_Position _
                        : .Direction = m_Direction

                ' Set attenuation factor.
                .Attenuation0 = m_Attenuation0
                .Attenuation1 = m_Attenuation1
                .Attenuation2 = m_Attenuation2

                ' Set the range.
                .Range = m_Range

                ' Set the spot-light values.
                .Falloff = m_Falloff
                .InnerConeAngle = m_InnerConeAngle
                .OuterConeAngle = m_OuterConeAngle

            End With ' With _Light

            ' Call base method.
            MyBase.Update(_Light, Enable)
        End Sub
    End Class
End Namespace
