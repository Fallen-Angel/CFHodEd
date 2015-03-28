Imports GenericMesh
Imports GenericMesh.MaterialFields

Partial Class Material
    ''' <summary>
    ''' Structure acting as a proxy material to the generic mesh objects. It
    ''' "sits" between the generic mesh's material reference and Homeworld2
    ''' material object. Plus it provides additional per-part fields (like
    ''' vertex mask).
    ''' </summary>
    Public Structure Reference
        Implements IMaterial
        Implements IMaterialName
        Implements IMaterialTexture

        ' --------------
        ' Class Members.
        ' --------------
        ''' <summary>Name of material.</summary>
        Private m_Name As String

        ''' <summary>Name of texture.</summary>
        Private m_TextureName As String

        ''' <summary>Index of material used.</summary>
        Private m_Index As Integer

        ''' <summary>Vertex Mask.</summary>
        Private m_VertexMask As VertexMasks

        ''' <summary>Reference to material.</summary>
        Private m_Material As Material

        ''' <summary>Whether to use textures or not.</summary>
        Friend Shared EnableTextures As Boolean = True

        ' -----------------
        ' Class properties.
        ' -----------------
        ''' <summary>
        ''' Returns\Sets the name of material used. But this neither changes
        ''' the material itself, nor the material referenced. This is only
        ''' supposed to be used while using a generic mesh translator.
        ''' </summary>
        Public Property Name() As String
            Get
                Return m_Name

            End Get

            Set(ByVal value As String)
                m_Name = value

            End Set

        End Property

        ''' <summary>
        ''' Returns\Sets the name of material used. But this neither changes
        ''' the material (or texture) itself, nor the material (or texture)
        ''' referenced. This is only supposed to be used while using a generic
        ''' mesh translator.
        ''' </summary>
        Public Property TextureName() As String
            Get
                Return m_TextureName

            End Get

            Set(ByVal value As String)
                m_TextureName = value

            End Set

        End Property

        ''' <summary>
        ''' Returns\Sets the index of material used.
        ''' </summary>
        ''' <exception cref="ArgumentOutOfRangeException">
        ''' Thrown when <c>value</c> is less than -1.
        ''' </exception>
        Public Property Index() As Integer
            Get
                Return m_Index

            End Get

            Set(ByVal value As Integer)
                If (value < -1) Then _
                 Throw New ArgumentOutOfRangeException("value") _
                : Exit Property

                ' Set the index and remove material reference.
                m_Index = value
                m_Material = Nothing

            End Set

        End Property

        ''' <summary>
        ''' Returns\Sets the vertex mask.
        ''' </summary>
        ''' <exception cref="ArgumentException">
        ''' Thrown when any of the <c>Texture2</c> through <c>Texture7</c>
        ''' or <c>HVertex</c> or <c>PVertex</c> flags are set.
        ''' Also thrown when any other unknown bit is set.
        ''' Also thrown when position bit is not set.
        ''' </exception>
        Public Property VertexMask() As VertexMasks
            Get
                Return m_VertexMask

            End Get

            Set(ByVal value As VertexMasks)
                ' Check for forbidden bits.
                'If ((value And VertexMasks.Texture2) <> 0) OrElse _
                If ((value And VertexMasks.Texture3) <> 0) OrElse
                   ((value And VertexMasks.Texture4) <> 0) OrElse
                   ((value And VertexMasks.Texture5) <> 0) OrElse
                   ((value And VertexMasks.Texture6) <> 0) OrElse
                   ((value And VertexMasks.Texture7) <> 0) OrElse
                   ((value And VertexMasks.HVertex) <> 0) OrElse
                   ((value And VertexMasks.SVertex) <> 0) Then _
                 Throw New ArgumentException("Invalid vertex mask(s); forbidden bit(s) set.") _
                : Exit Property

                ' Check for other (unknown) bits.
                If (value And VertexMasks.All) <> value Then _
                 Throw New ArgumentException("Invalid vertex mask(s); unknown bit(s) set.") _
                : Exit Property

                ' Check if position flag is missing.
                If (value And VertexMasks.Position) = 0 Then _
                 Throw New ArgumentException("Invalid vertex mask(s); missing position bit.") _
                : Exit Property

                ' Check if texture 1 is present but texture 0 is missing.
                If ((value And VertexMasks.Texture0) = 0) AndAlso
                   ((value And VertexMasks.Texture1) <> 0) Then _
                 Throw New ArgumentException("Invalid vertex mask(s); higher texture bits without texture 0.") _
                : Exit Property

                m_VertexMask = value

            End Set

        End Property

        ''' <summary>
        ''' Reference to material.
        ''' </summary>
        Friend Property Material() As Material
            Get
                Return m_Material

            End Get

            Set(ByVal value As Material)
                m_Material = value

            End Set

        End Property

        ' -----------------
        ' Member Functions.
        ' -----------------
        ''' <summary>
        ''' Checks whether two material paramters are equal or not.
        ''' </summary>
        ''' <param name="obj">
        ''' The other object to compare to.
        ''' </param>
        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            ' Check type.
            If Not TypeOf obj Is Reference Then _
             Return False

            ' Perform check.
            Return Equal(CType(obj, IMaterial))

        End Function

        ''' <summary>
        ''' Applies the material.
        ''' </summary>
        ''' <param name="_Device">
        ''' The device whose states are set to apply the material.
        ''' </param>
        Private Sub Apply(ByVal _Device As Direct3D.Device) Implements GenericMesh.IMaterial.Apply
            Dim attrib As New Direct3D.Material With {
             .AmbientColor = New Direct3D.ColorValue(1.0F, 1.0F, 1.0F),
             .DiffuseColor = New Direct3D.ColorValue(1.0F, 1.0F, 1.0F),
             .SpecularColor = New Direct3D.ColorValue(1.0F, 1.0F, 1.0F),
             .EmissiveColor = New Direct3D.ColorValue(0.0F, 0.0F, 0.0F),
             .SpecularSharpness = 96
            }

            ' The name of shader to use.
            Dim shaderName As String = "default"

            ' The textures of various stages.
            Dim tex0 As Direct3D.Texture = Nothing,
                tex1 As Direct3D.Texture = Nothing,
                tex2 As Direct3D.Texture = Nothing,
                tex3 As Direct3D.Texture = Nothing

            If m_Material IsNot Nothing Then
                ' Get the shader name if we have a material.
                shaderName = m_Material.m_ShaderName.ToLower()

                ' Parse all parameters to get the different texture stages, if needed.
                If EnableTextures Then
                    For I As Integer = 0 To m_Material.Parameters.Count - 1
                        Dim name As String = m_Material.Parameters(I).Name.ToLower()

                        If m_Material.Parameters(I).Type <> Parameter.ParameterType.Texture Then _
                         Continue For

                        ' ship, thruster, resource, resourcenm shader
                        If (name = "$diffuse") OrElse (name = "$diffuseoff") OrElse (name = "$diffusea") Then _
                            tex0 = m_Material.Parameters(I).Texture _
                        : Continue For

                        ' ship, thruster shader
                        If (name = "$glow") OrElse (name = "$glowoff") Then _
                            tex1 = m_Material.Parameters(I).Texture _
                        : Continue For

                        ' thruster shader
                        If name = "$diffuseon" Then _
                            tex2 = m_Material.Parameters(I).Texture _
                        : Continue For

                        ' thruster shader
                        If name = "$glowon" Then _
                            tex3 = m_Material.Parameters(I).Texture _
                        : Continue For

                        ' badge shader
                        If name = "$mask" Then _
                            tex2 = Material.BadgeTexture _
                        : tex3 = m_Material.Parameters(I).Texture _
                        : Continue For

                        If name = "$team" Then _
                            tex2 = m_Material.Parameters(I).Texture _
                        : Continue For

                        ' resource, resourcenm shader.
                        If name = "$normals" Then _
                            tex1 = m_Material.Parameters(I).Texture _
                        : Continue For

                    Next I ' For I As Integer = 0 To m_Material.Parameters.Count - 1
                End If ' If EnableTextures Then

                ' ---------- '
                ' EXCEPTIONS '
                ' ---------- '
                ' 1) If we're using a 'badge' shader and no badge texture is available then
                ' use the 'ship' shader instead.
                If (shaderName = "badge") AndAlso (Material.m_BadgeTexture Is Nothing) Then _
                 shaderName = "ship"

                ' 2) If main texture is not available use NO shader (unless it's a background
                ' shader ofcourse, which requires no textures).
                If (tex0 Is Nothing) AndAlso (shaderName <> "background") Then _
                 shaderName = ""

            End If ' If m_Material IsNot Nothing Then

            With _Device
                Dim amb, diff, spec, ems, pow As Vector4

                ' HACK: Is there a better way than this? Sum up light colours.
                For I As Integer = 0 To .Lights.Count - 1
                    ' Check to see if light is enabled.
                    If Not .Lights(I).Enabled Then _
                     Continue For

                    ' Add.
                    amb += New Vector4(.Lights(I).AmbientColor.Red, .Lights(I).AmbientColor.Green, .Lights(I).AmbientColor.Blue, .Lights(I).AmbientColor.Alpha)
                    diff += New Vector4(.Lights(I).DiffuseColor.Red, .Lights(I).DiffuseColor.Green, .Lights(I).DiffuseColor.Blue, .Lights(I).DiffuseColor.Alpha)
                    spec += New Vector4(.Lights(I).SpecularColor.Red, .Lights(I).SpecularColor.Green, .Lights(I).SpecularColor.Blue, .Lights(I).SpecularColor.Alpha)

                Next I ' For I As Integer = 0 To .Lights.Count - 1

                ' Calculate product.
                amb = New Vector4(.Material.AmbientColor.Red * amb.X, .Material.AmbientColor.Green * amb.Y, .Material.AmbientColor.Blue * amb.Z, .Material.AmbientColor.Alpha * amb.W)
                diff = New Vector4(.Material.DiffuseColor.Red * diff.X, .Material.DiffuseColor.Green * diff.Y, .Material.DiffuseColor.Blue * diff.Z, .Material.DiffuseColor.Alpha * diff.W)
                spec = New Vector4(.Material.SpecularColor.Red * spec.X, .Material.SpecularColor.Green * spec.Y, .Material.SpecularColor.Blue * spec.Z, .Material.SpecularColor.Alpha * spec.W)
                ems = New Vector4(.Material.EmissiveColor.Red, .Material.EmissiveColor.Green, .Material.EmissiveColor.Blue, .Material.EmissiveColor.Alpha)
                pow = New Vector4(.Material.SpecularSharpness, .Material.SpecularSharpness, .Material.SpecularSharpness, .Material.SpecularSharpness)

                ' Set device states.
                .Material = attrib
                .VertexShader = ShaderLibrary.VertexShader(shaderName)
                .PixelShader = ShaderLibrary.PixelShader(shaderName)

                .SetTexture(0, tex0)
                .SetTexture(1, tex1)
                .SetTexture(2, tex2)
                .SetTexture(3, tex3)

                ' Set VS constants.
                .SetVertexShaderConstant(36, New Vector4(attrib.AmbientColor.Red, attrib.AmbientColor.Green, attrib.AmbientColor.Blue, attrib.AmbientColor.Alpha))
                .SetVertexShaderConstant(37, New Vector4(attrib.DiffuseColor.Red, attrib.DiffuseColor.Green, attrib.DiffuseColor.Blue, attrib.DiffuseColor.Alpha))
                .SetVertexShaderConstant(38, New Vector4(attrib.SpecularColor.Red, attrib.SpecularColor.Green, attrib.SpecularColor.Blue, attrib.SpecularColor.Alpha))
                .SetVertexShaderConstant(39, New Vector4(attrib.EmissiveColor.Red, attrib.EmissiveColor.Green, attrib.EmissiveColor.Blue, attrib.EmissiveColor.Alpha))
                .SetVertexShaderConstant(40, New Vector4(attrib.SpecularSharpness, attrib.SpecularSharpness, attrib.SpecularSharpness, attrib.SpecularSharpness))

                ' Set PS contants.
                .SetPixelShaderConstant(4, amb)
                .SetPixelShaderConstant(5, diff)
                .SetPixelShaderConstant(6, spec)
                .SetPixelShaderConstant(7, ems)
                .SetPixelShaderConstant(8, pow)

            End With ' With _Device

        End Sub

        ''' <summary>
        ''' Sets the default properties.
        ''' </summary>
        Public Sub Initialize() Implements GenericMesh.IMaterial.Initialize
            m_Index = -1
            m_VertexMask = VertexMasks.Position
            m_Material = Nothing

        End Sub

        ''' <summary>
        ''' Removes the effect of the material; i.e. sets the 
        ''' device to it's normal state (only for the states changed).
        ''' </summary>
        ''' <param name="_Device">
        ''' The device whose states are set to normal states.
        ''' </param>
        Private Sub Reset(ByVal _Device As Direct3D.Device) Implements GenericMesh.IMaterial.Reset
            With _Device
                .VertexShader = Nothing
                .PixelShader = Nothing

                .SetTexture(3, Nothing)
                .SetTexture(2, Nothing)
                .SetTexture(1, Nothing)
                .SetTexture(0, Nothing)

                .SetVertexShaderConstant(36, New Vector4(0, 0, 0, 0))
                .SetVertexShaderConstant(37, New Vector4(0, 0, 0, 0))
                .SetVertexShaderConstant(38, New Vector4(0, 0, 0, 0))
                .SetVertexShaderConstant(39, New Vector4(0, 0, 0, 0))
                .SetVertexShaderConstant(40, New Vector4(0, 0, 0, 0))

                .SetPixelShaderConstant(4, New Vector4(0, 0, 0, 0))
                .SetPixelShaderConstant(5, New Vector4(0, 0, 0, 0))
                .SetPixelShaderConstant(6, New Vector4(0, 0, 0, 0))
                .SetPixelShaderConstant(7, New Vector4(0, 0, 0, 0))
                .SetPixelShaderConstant(8, New Vector4(0, 0, 0, 0))

            End With ' With _Device

        End Sub

        ''' <summary>
        ''' Returns whether another material is equivalent to this instance
        ''' or not.
        ''' </summary>
        ''' <param name="other">
        ''' The material which is to be compared.
        ''' </param>
        ''' <returns>
        ''' <c>True</c> or <c>False</c>, depending on whether the materials
        ''' are equivalent or not.
        ''' </returns>
        ''' <exception cref="ArgumentException">
        ''' <c>other</c> is not the same type as this instance.
        ''' </exception>
        Private Function Equal(ByVal other As GenericMesh.IMaterial) As Boolean Implements System.IEquatable(Of GenericMesh.IMaterial).Equals
            ' Check if 'other' is of same type.
            If Not TypeOf other Is Reference Then _
             Throw New ArgumentException("Object must be of type " & TypeName(Me).ToString & ".") _
            : Exit Function

            Dim obj As Reference = CType(CObj(other), Reference)

            Return (obj.m_Index = m_Index)

        End Function

        ''' <summary>
        ''' Returns the material name.
        ''' </summary>
        Private Function GetMaterialName() As String Implements GenericMesh.MaterialFields.IMaterialName.GetMaterialName
            Return m_Name

        End Function

        ''' <summary>
        ''' Sets the material name.
        ''' </summary>
        Private Function SetMaterialName(ByVal V As String) As GenericMesh.IMaterial Implements GenericMesh.MaterialFields.IMaterialName.SetMaterialName
            m_Name = V
            Return Me

        End Function

        ''' <summary>
        ''' Returns the texture name.
        ''' </summary>
        Private Function GetTextureName(Optional ByVal Index As Integer = 0) As String Implements GenericMesh.MaterialFields.IMaterialTexture.GetTextureName
            Return m_TextureName

        End Function

        ''' <summary>
        ''' Sets the texture name.
        ''' </summary>
        Private Function SetTextureName(ByVal V As String, Optional ByVal Index As Integer = 0) As GenericMesh.IMaterial Implements GenericMesh.MaterialFields.IMaterialTexture.SetTextureName
            m_TextureName = V
            Return Me

        End Function

        ''' <summary>
        ''' Updates the reference so that it may be used for rendering.
        ''' </summary>
        Friend Function Update(ByVal Materials As IList(Of Material)) As Reference
            If (m_Index < 0) OrElse (m_Index >= Materials.Count) Then _
             m_Material = Nothing _
            : m_Name = "No Material" _
            : Return Me

            m_Material = Materials(m_Index)

            Return Me

        End Function

    End Structure

End Class
