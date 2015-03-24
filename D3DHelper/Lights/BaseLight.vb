Imports D3DHelper.Camera
Imports Microsoft.DirectX

Namespace Lights
    ''' <summary>
    ''' Class defining light attributes.
    ''' </summary>
    Public MustInherit Class BaseLight
        ' --------------
        ' Class Members.
        ' --------------
        ''' <summary>Ambient colour.</summary>
        Protected m_Ambient As Direct3D.ColorValue

        ''' <summary>Diffuse colour.</summary>
        Protected m_Diffuse As Direct3D.ColorValue

        ''' <summary>Specular colour.</summary>
        Protected m_Specular As Direct3D.ColorValue

        ''' <summary>Whether the light is enabled or not.</summary>
        Protected m_Enabled As Boolean

        ''' <summary>Camera to which light is attached.</summary>
        Protected m_Camera As BaseCamera

        ' ------------------------
        ' Constructors\Finalizers.
        ' ------------------------
        ''' <summary>
        ''' Class constructor.
        ''' </summary>
        Protected Sub New()
            m_Ambient = New Direct3D.ColorValue(0.2F, 0.2F, 0.2F)
            m_Diffuse = New Direct3D.ColorValue(0.8F, 0.8F, 0.8F)
            m_Specular = New Direct3D.ColorValue(0.5F, 0.5F, 0.5F)

            m_Enabled = True
        End Sub

        ' -----------------
        ' Class properties.
        ' -----------------
        ''' <summary>
        ''' Returns the type of light.
        ''' </summary>
        Public MustOverride ReadOnly Property Type() As Direct3D.LightType

        ''' <summary>
        ''' Returns\Sets the ambient colour value.
        ''' </summary>
        Public Property Ambient() As Direct3D.ColorValue
            Get
                Return m_Ambient
            End Get

            Set(ByVal value As Direct3D.ColorValue)
                m_Ambient = value
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets diffuse colour value.
        ''' </summary>
        Public Property Diffuse() As Direct3D.ColorValue
            Get
                Return m_Diffuse
            End Get

            Set(ByVal value As Direct3D.ColorValue)
                m_Diffuse = value
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets specular colour value.
        ''' </summary>
        Public Property Specular() As Direct3D.ColorValue
            Get
                Return m_Specular
            End Get

            Set(ByVal value As Direct3D.ColorValue)
                m_Specular = value
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets whether the light is enabled or not.
        ''' </summary>
        Public Property Enabled() As Boolean
            Get
                Return m_Enabled
            End Get

            Set(ByVal value As Boolean)
                m_Enabled = value
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
        ''' Attaches the light to a camera.
        ''' </summary>
        ''' <param name="Camera">
        ''' Camera to which to attach.
        ''' </param>
        ''' <remarks>
        ''' To detach from camera, pass the parameter <c>Nothing</c>.
        ''' </remarks>
        Public Sub Attach(Optional ByVal Camera As BaseCamera = Nothing)
            m_Camera = Camera
        End Sub

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
        Public Overridable Sub Update(ByVal _Light As Direct3D.Light,
                                      Optional ByVal Enable As TriState = TriState.UseDefault)

            If _Light Is Nothing Then _
                Throw New ArgumentNullException("_Light Is Nothing") _
                    : Exit Sub

            ' Update variable if asked to do so.
            If Enable <> TriState.UseDefault Then _
                m_Enabled = CBool(Enable)

            With _Light
                ' Set the type.
                .Type = Type

                ' Set the colours.
                .AmbientColor = m_Ambient
                .DiffuseColor = m_Diffuse
                .SpecularColor = m_Specular

                ' Enable and update the light.
                .Enabled = m_Enabled
                .Update()

            End With ' With _Light
        End Sub

        ''' <summary>
        ''' Updates and enables the light.
        ''' </summary>
        ''' <param name="_Light">
        ''' The light to update.
        ''' </param>
        ''' <param name="Enable">
        ''' Whether the light is enabled or not. Modifies existing state.
        ''' </param>
        Public Sub Update(ByVal _Light As Direct3D.Light, ByVal Enable As Boolean)
            If Enable Then _
                Update(_Light, TriState.True) _
                Else _
                Update(_Light, TriState.False)
        End Sub
    End Class
End Namespace
