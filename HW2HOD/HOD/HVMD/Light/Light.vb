''' <summary>
''' Class representing Homeworld2 light, used in background display.
''' </summary>
Public NotInheritable Class Light
    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Name of light.</summary>
    Private m_Name As String

    ''' <summary>Type of light.</summary>
    Private m_Type As LightType

    ''' <summary>
    ''' Transform data of the light. This is:
    ''' Zero for ambient light,
    ''' Position for point light,
    ''' Direction vector (but of magnitude 10,000) for directional light.
    ''' </summary>
    Private m_Transform As Vector3

    ''' <summary>
    ''' Colour emitted by the light.
    ''' </summary>
    ''' <remarks>
    ''' Component X = Colour R,
    ''' Component Y = Colour G,
    ''' Component Z = Colour B
    ''' </remarks>
    Private m_Colour As Vector3

    ''' <summary>Specular colour emitted by the light.</summary>
    Private m_Specular As Vector3

    ''' <summary>Type of attenuation.</summary>
    Private m_Attenuation As LightAttenuation

    ''' <summary>Coefficient of attenuation.</summary>
    Private m_AttenuationDistance As Single

    ''' <summary>Visible.</summary>
    Private m_Visible As Boolean

    ' ------------------------
    ' Constructors\Finalizers.
    ' ------------------------
    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    Public Sub New()
        Initialize()
    End Sub

    ''' <summary>
    ''' Copy constructor.
    ''' </summary>
    Public Sub New(ByVal l As Light)
        m_Name = l.m_Name
        m_Type = l.m_Type
        m_Transform = l.m_Transform
        m_Colour = l.m_Colour
        m_Specular = l.m_Specular
        m_Attenuation = l.m_Attenuation
        m_AttenuationDistance = l.m_AttenuationDistance
        m_Visible = l.m_Visible
    End Sub

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns\Sets the name of this light.
    ''' </summary>
    Public Property Name() As String
        Get
            Return m_Name
        End Get

        Set(ByVal value As String)
            If (value Is Nothing) OrElse (value = "") Then _
                Throw New ArgumentNullException("value") _
                    : Exit Property

            m_Name = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets the type of light.
    ''' </summary>
    ''' <exception cref="ArgumentException">
    ''' Thrown when an invalid input is passed.
    ''' </exception>
    Public Property Type() As LightType
        Get
            Return m_Type
        End Get

        Set(ByVal value As LightType)
            Select Case value
                Case LightType.Ambient
                    ' No transform and attenuation parameters for directional lights.
                    m_Transform = New Vector3(0, 0, 0)
                    m_Attenuation = LightAttenuation.None
                    m_AttenuationDistance = 0

                Case LightType.Point
                    ' All fields are applicable here.

                Case LightType.Directional
                    ' No attenuation parameters for directional lights.
                    m_Attenuation = LightAttenuation.None
                    m_AttenuationDistance = 0

                Case Else
                    Throw New ArgumentException("Invalid 'value'.")
                    Exit Property

            End Select

            m_Type = value
        End Set
    End Property

    ''' <summary>
    ''' Sets the position of a point light.
    ''' </summary>
    ''' <remarks>
    ''' This only works for point lights. For other types,
    ''' this does nothing.
    ''' </remarks>
    Public Property Position() As Vector3
        Get
            If (m_Type <> LightType.Point) Then _
                Return Vector3.Empty

            Return m_Transform
        End Get

        Set(ByVal value As Vector3)
            If (m_Type <> LightType.Point) Then _
                Exit Property

            m_Transform = value
        End Set
    End Property

    ''' <summary>
    ''' Sets the direction of a directional light.
    ''' </summary>
    ''' <remarks>
    ''' This only works for directional lights. For other types,
    ''' this does nothing.
    ''' </remarks>
    Public Property Direction() As Vector3
        Get
            If (m_Type <> LightType.Directional) Then _
                Return Vector3.Empty

            Return - m_Transform
        End Get

        Set(ByVal value As Vector3)
            If (m_Type <> LightType.Directional) Then _
                Exit Property

            m_Transform = - value
        End Set
    End Property

    Public Property Transform() As Vector3
        Get
            Return m_Transform
        End Get

        Set(ByVal value As Vector3)
            m_Transform = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets the diffuse colour for a point or directional light,
    ''' or the ambient colour for an ambient light.
    ''' </summary>
    Public Property Colour() As Vector3
        Get
            Return m_Colour
        End Get

        Set(ByVal value As Vector3)
            m_Colour = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets the specular colour for a light.
    ''' </summary>
    Public Property Specular() As Vector3
        Get
            Return m_Specular
        End Get

        Set(ByVal value As Vector3)
            m_Specular = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets the attenuation for a point light. Attenuation is the
    ''' manner in which the intensity of a light fades as we move away from it.
    ''' It can be constant (none), inversely proportional to distance 1/D (linear),
    ''' or inversely proportional to the second power of distance 1/D² (quadratic).
    ''' </summary>
    ''' <remarks>
    ''' This only works for point lights. For other types, this does nothing.
    ''' </remarks>
    ''' <exception cref="ArgumentException">
    ''' Thrown when an invalid input is passed.
    ''' </exception>
    Public Property Attenuation() As LightAttenuation
        Get
            If (m_Type <> LightType.Point) Then _
                Return LightAttenuation.None
        End Get

        Set(ByVal value As LightAttenuation)
            If (m_Type <> LightType.Point) Then _
                Exit Property

            Select Case value
                Case LightAttenuation.None
                Case LightAttenuation.Linear
                Case LightAttenuation.Quadratic
                Case Else
                    Throw New ArgumentException("Invalid 'value'.")
                    Exit Property

            End Select ' Select Case value

            m_Attenuation = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets the attenuation distance (the distance beyond which
    ''' point lights have no effect?)
    ''' </summary>
    ''' <remarks>
    ''' This only works for point lights. For other types, this does nothing.
    ''' </remarks>
    ''' <exception cref="ArgumentException">
    ''' Thrown when <c>value</c> is negative.
    ''' </exception>
    Public Property AttenuationDistance() As Single
        Get
            If (m_Type <> LightType.Point) Then _
                Return 0

            Return m_AttenuationDistance
        End Get

        Set(ByVal value As Single)
            If (m_Type <> LightType.Point) Then _
                Exit Property

            If (value < 0) Then _
                Throw New ArgumentException("'value' is negative.") _
                    : Exit Property

            m_AttenuationDistance = value
        End Set
    End Property

    ''' <summary>
    ''' Returns\Sets whether the light is visible or not.
    ''' </summary>
    ''' <remarks>
    ''' This is purely a rendering related property. It does not
    ''' affect what is written in the HOD
    ''' </remarks>
    Public Property Visible() As Boolean
        Get
            Return m_Visible
        End Get

        Set(ByVal value As Boolean)
            m_Visible = value
        End Set
    End Property

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Returns the name of this light.
    ''' </summary>
    Public Overrides Function ToString() As String
        Return m_Name
    End Function

    ''' <summary>
    ''' Sets the light parameters of this light into a Direct3D light structure.
    ''' </summary>
    ''' <param name="lOut">
    ''' The output light.
    ''' </param>
    ''' <returns>
    ''' <c>True</c>, if success, <c>False</c> otherwise.
    ''' </returns>
    ''' <exception cref="ArgumentNullException">
    ''' When the output light is not specified.
    ''' </exception>
    Public Function SetLight(ByVal lOut As Direct3D.Light) As Boolean
        If lOut Is Nothing Then _
            Throw New ArgumentNullException("lOut") _
                : Return False

        ' Clear the light structure.
        lOut.Position = New Vector3(0, 0, 0)
        lOut.Direction = New Vector3(0, 0, 0)

        lOut.AmbientColor = New Direct3D.ColorValue(0, 0, 0)
        lOut.DiffuseColor = New Direct3D.ColorValue(0, 0, 0)
        lOut.SpecularColor = New Direct3D.ColorValue(0, 0, 0)

        lOut.Attenuation0 = 0
        lOut.Attenuation1 = 0
        lOut.Attenuation2 = 0
        lOut.Range = 0

        lOut.Falloff = 0
        lOut.InnerConeAngle = 0
        lOut.OuterConeAngle = 0

        ' Now set the appropriate properties.
        Select Case m_Type
            Case LightType.Ambient
                ' This is a cheap-code costly-resources approximation to ambient lights.
                lOut.Type = Direct3D.LightType.Point
                lOut.AmbientColor = New Direct3D.ColorValue(m_Colour.X, m_Colour.Y, m_Colour.Z)
                lOut.Attenuation0 = 1
                lOut.Range = 1.0E+15

            Case LightType.Point
                ' Point lights have color and position within a scene, but no single direction.
                lOut.Type = Direct3D.LightType.Point
                lOut.Position = m_Transform
                lOut.DiffuseColor = New Direct3D.ColorValue(m_Colour.X, m_Colour.Y, m_Colour.Z)
                lOut.SpecularColor = New Direct3D.ColorValue(m_Specular.X, m_Specular.Y, m_Specular.Z)

                ' Point lights are affected by attenuation and range.
                Select Case m_Attenuation
                    Case LightAttenuation.None
                        lOut.Attenuation0 = 1
                        lOut.Range = m_AttenuationDistance

                    Case LightAttenuation.Linear
                        lOut.Attenuation1 = 1
                        lOut.Range = m_AttenuationDistance

                    Case LightAttenuation.Quadratic
                        lOut.Attenuation2 = 1
                        lOut.Range = m_AttenuationDistance

                End Select ' Select Case Attenuation

            Case LightType.Directional
                ' Directional lights have only color and direction.
                lOut.Type = Direct3D.LightType.Directional
                lOut.Direction = - m_Transform

                lOut.DiffuseColor = New Direct3D.ColorValue(m_Colour.X, m_Colour.Y, m_Colour.Z)
                lOut.SpecularColor = New Direct3D.ColorValue(m_Specular.X, m_Specular.Y, m_Specular.Z)

                ' Check for zero length direction vector.
                If m_Transform.LengthSq() < 0.000001 Then _
                    lOut.Direction = New Vector3(0, 0, - 1)

            Case Else
                Return False

        End Select ' Select Case Type

        Return False
    End Function

    ''' <summary>
    ''' Reads a light from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when 'IFF' is nothing.
    ''' </exception>
    Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader)
        Initialize()

        m_Name = IFF.ReadString()
        m_Type = CType(IFF.ReadInt32(), LightType)

        m_Transform.X = IFF.ReadSingle()
        m_Transform.Y = IFF.ReadSingle()
        m_Transform.Z = IFF.ReadSingle()

        m_Colour.X = IFF.ReadSingle()
        m_Colour.Y = IFF.ReadSingle()
        m_Colour.Z = IFF.ReadSingle()

        m_Specular.X = IFF.ReadSingle()
        m_Specular.Y = IFF.ReadSingle()
        m_Specular.Z = IFF.ReadSingle()

        m_Attenuation = CType(IFF.ReadInt32, LightAttenuation)
        m_AttenuationDistance = IFF.ReadSingle()
    End Sub

    ''' <summary>
    ''' Writes a light to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when 'IFF' is nothing.
    ''' </exception>
    Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
        If m_Type = LightType.Directional Then
            ' Check and adjust the direction vector.
            If m_Transform.LengthSq < 0.000001 Then _
                Trace.TraceError(
                    "Light '" & m_Name & "' has a direction vector of near zero (< 0.001) length. Please fix.") _
                    : m_Transform = New Vector3(0, 0, - 10000) _
                Else _
                m_Transform.Normalize() _
                    : m_Transform.Multiply(10000)

        End If ' If m_Type = LightType.Directional Then

        IFF.Write(m_Name)
        IFF.WriteInt32(m_Type)

        IFF.Write(m_Transform.X)
        IFF.Write(m_Transform.Y)
        IFF.Write(m_Transform.Z)

        IFF.Write(m_Colour.X)
        IFF.Write(m_Colour.Y)
        IFF.Write(m_Colour.Z)

        IFF.Write(m_Specular.X)
        IFF.Write(m_Specular.Y)
        IFF.Write(m_Specular.Z)

        IFF.WriteInt32(m_Attenuation)
        IFF.Write(m_AttenuationDistance)
    End Sub

    Friend Sub Render(ByVal Device As Direct3D.Device,
                      ByVal PointMesh As GenericMesh.Standard.BasicMesh,
                      ByVal DirectionalMesh As GenericMesh.Standard.BasicMesh)

        If Not m_Visible Then _
            Exit Sub

        ' Set colour.
        PointMesh.Material(0) = New GenericMesh.Standard.Material With { _
            .Attributes = New Direct3D.Material With { _
                .EmissiveColor = New Direct3D.ColorValue(m_Colour.X,
                                                         m_Colour.Y,
                                                         m_Colour.Z) _
                } _
            }

        DirectionalMesh.Material(0) = PointMesh.Material(0)

        ' Check the type of light.
        Select Case m_Type
            Case Light.LightType.Point
                ' Set transform.
                Device.Transform.World = Matrix.Translation(m_Transform)

                ' Render mesh.
                PointMesh.Render(Device)

            Case Light.LightType.Directional
                ' Get position.
                Dim p As Vector3 = - m_Transform

                ' Set transform.
                Device.Transform.World = Matrix.Scaling(1.0F, 1.0F, 2.1F)*
                                         Matrix.RotationX(CSng(Math.Atan2(- p.Y, Math.Sqrt(p.X*p.X + p.Z*p.Z))))*
                                         Matrix.RotationY(CSng(Math.Atan2(p.X, p.Z)))

                ' Render mesh.
                DirectionalMesh.Render(Device)

        End Select ' Select m_Type
    End Sub

    ''' <summary>
    ''' Initializes the light.
    ''' </summary>
    Private Sub Initialize()
        m_Name = "Light"
        m_Transform = New Vector3(0, 0, 0)
        m_Type = LightType.Ambient
        m_Colour = New Vector3(1, 1, 1)
        m_Specular = New Vector3(0.5, 0.5, 0.5)
        m_Attenuation = LightAttenuation.None
        m_AttenuationDistance = 0
        m_Visible = False
    End Sub
End Class
