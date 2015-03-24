Imports GenericMesh.MaterialFields

Namespace Standard
    ''' <summary>
    ''' Material supporting one texture.
    ''' </summary>
    ''' <remarks>
    ''' The default material type associated with the mesh class.
    ''' </remarks>
    Public Structure Material
        ' -------------------------
        ' Interface(s) Implemented.
        ' -------------------------
        Implements IMaterial
        Implements IMaterialName, IMaterialTexture, IMaterialAttributes

        ' --------------
        ' Class Members.
        ' --------------
        ''' <summary>
        ''' Material Name.
        ''' </summary>
        Private m_MaterialName As String

        ''' <summary>
        ''' Material attributes.
        ''' </summary>
        Private m_Attributes As Direct3D.Material

        ''' <summary>
        ''' Texture Name.
        ''' </summary>
        Private m_TextureName As String

        ''' <summary>
        ''' Texture.
        ''' </summary>
        Private m_Texture As BaseTexture

        ' -----------------------
        ' Constructors\Finalizer.
        ' -----------------------
        ''' <summary>
        ''' Initializes the material with the given data.
        ''' </summary>
        ''' <param name="MatName">
        ''' Name of material.
        ''' </param>
        ''' <param name="TexName">
        ''' Name of texture used by the material.
        ''' </param>
        Public Sub New(ByVal MatName As String, Optional ByVal TexName As String = "")
            Initialize()

            m_MaterialName = MatName
            m_TextureName = TexName
        End Sub

        ''' <summary>
        ''' Returns\Sets the material name.
        ''' </summary>
        Public Property MaterialName() As String
            Get
                Return m_MaterialName
            End Get

            Set(ByVal value As String)
                m_MaterialName = value
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets the material attributes.
        ''' </summary>
        Public Property Attributes() As Microsoft.DirectX.Direct3D.Material
            Get
                Return m_Attributes
            End Get

            Set(ByVal value As Microsoft.DirectX.Direct3D.Material)
                m_Attributes = value
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets the texture name.
        ''' </summary>
        ''' <param name="Index">
        ''' The stage whose texture is being accessed\modified.
        ''' </param>
        Public Property TextureName(Optional ByVal Index As Integer = 0) As String
            Get
                Return m_TextureName
            End Get

            Set(ByVal value As String)
                m_TextureName = value
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets the texture used by the material.
        ''' </summary>
        Public Property Texture() As BaseTexture
            Get
                Return m_Texture
            End Get

            Set(ByVal value As BaseTexture)
                m_Texture = value
            End Set
        End Property

        ' ---------
        ' Operators
        ' ---------
        ''' <summary>
        ''' Compares two expressions and returns <c>True</c> if they are equal.
        ''' Otherwise, returns False.
        ''' </summary>
        ''' <param name="L">
        ''' Left operand.
        ''' </param>
        ''' <param name="R">
        ''' Right operand.
        ''' </param>
        ''' <returns>
        ''' <c>True</c> if left operand is equal to right operand;
        ''' <c>False</c> otherwise.
        ''' </returns>
        Public Shared Operator =(ByVal L As Material, ByVal R As Material) As Boolean
            If (L.m_MaterialName = R.m_MaterialName) AndAlso
               (L.m_Attributes.Equals(R.m_Attributes)) AndAlso
               (L.m_TextureName = R.m_TextureName) AndAlso
               (L.m_Texture Is R.m_Texture) Then _
                Return True _
                Else _
                Return False
        End Operator

        ''' <summary>
        ''' Compares two expressions and returns <c>True</c> if they are not equal.
        ''' Otherwise, returns False.
        ''' </summary>
        ''' <param name="L">
        ''' Left operand.
        ''' </param>
        ''' <param name="R">
        ''' Right operand.
        ''' </param>
        ''' <returns>
        ''' <c>True</c> if left operand is not equal to right operand;
        ''' <c>False</c> otherwise.
        ''' </returns>
        Public Shared Operator <>(ByVal L As Material, ByVal R As Material) As Boolean
            If (L.m_MaterialName <> R.m_MaterialName) OrElse
               (Not L.m_Attributes.Equals(R.m_Attributes)) OrElse
               (L.m_TextureName <> R.m_TextureName) OrElse
               (L.m_Texture IsNot R.m_Texture) Then _
                Return True _
                Else _
                Return False
        End Operator

        ' -----------------
        ' Member Functions.
        ' -----------------
        ''' <summary>
        ''' Returns whether another material is equivalent to this instance
        ''' or not.
        ''' </summary>
        ''' <param name="obj">
        ''' The material which is to be compared.
        ''' </param>
        ''' <returns>
        ''' <c>True</c> or <c>False</c>, depending on whether the materials
        ''' are equivalent or not.
        ''' </returns>
        ''' <exception cref="ArgumentException">
        ''' <c>obj</c> is not the same type as this instance.
        ''' </exception>
        Private Function Equal(ByVal obj As IMaterial) As Boolean Implements IEquatable(Of IMaterial).Equals
            ' Check if 'obj' is of same type.
            If Not TypeOf obj Is Material Then _
                Throw New ArgumentException("Object must be of type " & TypeName(Me).ToString & ".") _
                    : Exit Function

            ' Get the material.
            Dim Mat As Material = CType(obj, Material)

            ' Perform comparision.
            Return (Me = Mat)
        End Function

        ''' <summary>
        ''' Sets the default properties.
        ''' </summary>
        Public Sub Initialize() Implements IMaterial.Initialize
            m_MaterialName = ""
            m_TextureName = ""
            m_Texture = Nothing

            With m_Attributes
                .AmbientColor = New ColorValue(0.2, 0.2, 0.2)
                .DiffuseColor = New ColorValue(0.8, 0.8, 0.8)
                .SpecularColor = New ColorValue(0, 0, 0)
                .EmissiveColor = New ColorValue(0, 0, 0)
                .SpecularSharpness = 0
            End With ' With m_Attributes
        End Sub

        ''' <summary>
        ''' Applies the material.
        ''' </summary>
        ''' <param name="_Device">
        ''' The device whose states are set to apply the material.
        ''' </param>
        Public Sub Apply(ByVal _Device As Device) Implements IMaterial.Apply
            With _Device
                ' Set the textures upto stage 1.
                .SetTexture(0, m_Texture)
                .SetTexture(1, Nothing)

                ' Set the material.
                .Material = m_Attributes

            End With ' With _Device
        End Sub

        ''' <summary>
        ''' Removes the effect of the material; i.e. sets the 
        ''' device to it's previous state (for the states changed).
        ''' </summary>
        ''' <param name="_Device">
        ''' The device whose states are set to older states.
        ''' </param>
        Public Sub Reset(ByVal _Device As Device) Implements IMaterial.Reset
            With _Device
                ' Reset all the states set.
                .SetTexture(0, Nothing)
                .Material = New Direct3D.Material()

            End With ' With _Device
        End Sub

        ''' <summary>
        ''' Sets the diffuse colour.
        ''' </summary>
        ''' <param name="C">
        ''' Value to set.
        ''' </param>
        Public Sub SetDiffuse(ByVal C As ColorValue)
            m_Attributes.DiffuseColor = C
        End Sub

        ''' <summary>
        ''' Sets the ambient colour.
        ''' </summary>
        ''' <param name="C">
        ''' Value to set.
        ''' </param>
        Public Sub SetAmbient(ByVal C As ColorValue)
            m_Attributes.AmbientColor = C
        End Sub

        ''' <summary>
        ''' Sets the specular colour.
        ''' </summary>
        ''' <param name="C">
        ''' Value to set.
        ''' </param>
        Public Sub SetSpecular(ByVal C As ColorValue)
            m_Attributes.SpecularColor = C
        End Sub

        ''' <summary>
        ''' Sets the emissive colour.
        ''' </summary>
        ''' <param name="C">
        ''' Value to set.
        ''' </param>
        Public Sub SetEmissive(ByVal C As ColorValue)
            m_Attributes.EmissiveColor = C
        End Sub

        ''' <summary>
        ''' Sets the specular sharpness.
        ''' </summary>
        ''' <param name="SS">
        ''' Value to set.
        ''' </param>
        Public Sub SetSpecularSharpness(ByVal SS As Single)
            m_Attributes.SpecularSharpness = SS
        End Sub

        ''' <summary>
        ''' Retrieves the name of the material.
        ''' </summary>
        Private Function GetMaterialName() As String Implements MaterialFields.IMaterialName.GetMaterialName
            Return m_MaterialName
        End Function

        ''' <summary>
        ''' Sets the name of the material.
        ''' </summary>
        ''' <param name="V">
        ''' The name to set.
        ''' </param>
        ''' <returns>
        ''' Modified material.
        ''' </returns>
        Public Function SetMaterialName(ByVal V As String) As IMaterial _
            Implements MaterialFields.IMaterialName.SetMaterialName
            m_MaterialName = V
            Return Me
        End Function

        ''' <summary>
        ''' Returns the texture name used by the specified stage.
        ''' </summary>
        ''' <param name="Index">
        ''' The index of the stage being retrieved.
        ''' </param>
        ''' <remarks>
        ''' All values of index point to the same texture.
        ''' </remarks>
        Private Function GetTextureName(Optional ByVal Index As Integer = 0) As String _
            Implements MaterialFields.IMaterialTexture.GetTextureName
            Return m_TextureName
        End Function

        ''' <summary>
        ''' Sets the texture name used by the specified stage.
        ''' </summary>
        ''' <param name="V">
        ''' Texture name to set.
        ''' </param>
        ''' <param name="Index">
        ''' The index of the stage being set.
        ''' </param>
        ''' <returns>
        ''' Modified material.
        ''' </returns>
        ''' <remarks>
        ''' All values of index point to the same texture.
        ''' </remarks>
        Public Function SetTextureName(ByVal V As String, Optional ByVal Index As Integer = 0) As IMaterial _
            Implements MaterialFields.IMaterialTexture.SetTextureName
            m_TextureName = V
            Return Me
        End Function

        ''' <summary>
        ''' Retrieves the attributes of the material.
        ''' </summary>
        Private Function GetAttributes() As Direct3D.Material _
            Implements MaterialFields.IMaterialAttributes.GetAttributes
            Return m_Attributes
        End Function

        ''' <summary>
        ''' Sets the attributes of the material.
        ''' </summary>
        ''' <param name="V">
        ''' The attributes to set.
        ''' </param>
        ''' <returns>
        ''' Modified material.
        ''' </returns>
        Public Function SetAttributes(ByVal V As Direct3D.Material) As IMaterial _
            Implements MaterialFields.IMaterialAttributes.SetAttributes
            m_Attributes = V
            Return Me
        End Function
    End Structure
End Namespace
