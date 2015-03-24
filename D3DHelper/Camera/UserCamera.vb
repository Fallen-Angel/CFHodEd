Imports Microsoft.DirectX

Namespace Camera
    ''' <summary>
    ''' Class providing functionality for camera which accepts user
    ''' input and is focused on a location.
    ''' </summary>
    ''' <remarks>
    ''' This is intended to be a user controlled camera.
    ''' </remarks>
    Public Class UserCamera
        Inherits BaseCamera

        ' --------------
        ' Class Members.
        ' --------------
        Private m_Rot As Vector2
        Private m_Zoom As Single

        ' ------------------------
        ' Constructors\Finalizers.
        ' ------------------------
        ''' <summary>
        ''' Class constructor.
        ''' </summary>
        Public Sub New()
            ' Call base constructor.
            MyBase.New()

            ' Now set some fields.
            m_Mode = CameraTarget.FixedPoint

            ' Now build the matrices.
            BuildViewMatrix()
            BuildProjMatrix()
        End Sub

        ' -----------------
        ' Class properties.
        ' -----------------
        ''' <summary>
        ''' Returns camera (eye) position.
        ''' </summary>
        Public Shadows ReadOnly Property Position() As Vector3
            Get
                Return MyBase.Position
            End Get
        End Property

        ''' <summary>
        ''' Returns camera look at target.
        ''' </summary>
        ''' <remarks>
        ''' Applicable only for <c>CameraTarget.FixedPoint</c> mode. Hence
        ''' setting this value may change the <c>CameraTarget</c> property.
        ''' </remarks>
        Public Shadows ReadOnly Property LookAtTarget() As Vector3
            Get
                Return MyBase.LookAtTarget
            End Get
        End Property

        ''' <summary>
        ''' Returns camera up-vector.
        ''' </summary>
        Public Shadows ReadOnly Property UpVector() As Vector3
            Get
                Return MyBase.UpVector
            End Get
        End Property

        ''' <summary>
        ''' Returns the view matrix.
        ''' </summary>
        Public Shadows ReadOnly Property ViewMatrix() As Matrix
            Get
                Return MyBase.ViewMatrix
            End Get
        End Property

        ''' <summary>
        ''' Returns\Sets the field of view.
        ''' </summary>
        ''' <remarks>
        ''' Applicable only for orthogonal projection. Hence this
        ''' may change the <c>ProjectionType</c> property.
        ''' </remarks>
        Public Shadows Property FOV() As Single
            Get
                Return MyBase.FOV
            End Get

            Set(ByVal value As Single)
                MyBase.FOV = value
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets the camera width.
        ''' </summary>
        ''' <remarks>
        ''' Applicable only for orthogonal projection. Hence this
        ''' may change the <c>ProjectionType</c> property.
        ''' </remarks>
        Public Shadows Property Width() As Single
            Get
                Return MyBase.Width
            End Get

            Set(ByVal value As Single)
                MyBase.Width = value
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets the camera height.
        ''' </summary>
        ''' <remarks>
        ''' Applicable only for orthogonal projection. Hence this
        ''' may change the <c>ProjectionType</c> property.
        ''' </remarks>
        Public Shadows Property Height() As Single
            Get
                Return MyBase.Height
            End Get

            Set(ByVal value As Single)
                MyBase.Height = value
            End Set
        End Property

        ''' <summary>
        ''' Returns the aspect ratio.
        ''' </summary>
        Public Shadows ReadOnly Property AspectRatio() As Single
            Get
                Return MyBase.AspectRatio
            End Get
        End Property

        ''' <summary>
        ''' Returns\Sets the near clip-plane distance.
        ''' </summary>
        Public Shadows Property ZNear() As Single
            Get
                Return MyBase.ZNear()
            End Get

            Set(ByVal value As Single)
                MyBase.ZNear = value
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets the near clip-plane distance.
        ''' </summary>
        Public Shadows Property ZFar() As Single
            Get
                Return MyBase.ZFar
            End Get

            Set(ByVal value As Single)
                MyBase.ZFar = value
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets whether orthogonal projection is used or not.
        ''' </summary>
        Public Shadows Property ProjectionType() As ProjectionType
            Get
                Return MyBase.ProjectionType
            End Get

            Set(ByVal value As ProjectionType)
                MyBase.ProjectionType = value
            End Set
        End Property

        ''' <summary>
        ''' Returns the projection matrix.
        ''' </summary>
        Public Shadows ReadOnly Property ProjectionMatrix() As Matrix
            Get
                Return MyBase.ProjectionMatrix
            End Get
        End Property

        ' -----------------
        ' Member Functions.
        ' -----------------
        ''' <summary>
        ''' Sets the 'ZNear' and 'ZFar' properties simultaneously.
        ''' </summary>
        ''' <param name="ZNear">
        ''' Sets the Z-near plane.
        ''' </param>
        ''' <param name="ZFar">
        ''' Sets the Z-far plane.
        ''' </param>
        Public Shadows Sub SetZ(ByVal ZNear As Single, ByVal ZFar As Single)
            MyBase.SetZ(ZNear, ZFar)
        End Sub

        ''' <summary>
        ''' Pans the camera.
        ''' </summary>
        ''' <param name="NewPos">
        ''' New position of mouse.
        ''' </param>
        ''' <param name="OldPos">
        ''' Old position of mouse.
        ''' </param>
        ''' <param name="World">
        ''' World matrix.
        ''' </param>
        Public Sub CameraPan(ByVal NewPos As Vector2, ByVal OldPos As Vector2, ByVal World As Matrix)
            ' Build the viewport.
            Dim Viewport As New Direct3D.Viewport With { _
                    .X = 0, _
                    .Y = 0, _
                    .Width = CInt(m_Width), _
                    .Height = CInt(m_Height), _
                    .MinZ = 0, _
                    .MaxZ = 1 _
                    }

            ' Get the old position corresponding to the initial mouse position.
            Dim OldPos3 As Vector3 = Vector3.Unproject(New Vector3(OldPos.X, OldPos.Y, 0),
                                                       Viewport,
                                                       m_Proj,
                                                       m_View,
                                                       World)

            ' Get the new position corresponding to the final mouse position.
            Dim NewPos3 As Vector3 = Vector3.Unproject(New Vector3(NewPos.X, NewPos.Y, 0),
                                                       Viewport,
                                                       m_Proj,
                                                       m_View,
                                                       World)

            ' Update the look at position.
            m_EyePos -= m_LookAtPosition
            m_LookAtPosition += (NewPos3 - OldPos3)*25
            m_EyePos += m_LookAtPosition

            ' Build the view matrix.
            BuildViewMatrix()
        End Sub

        ''' <summary>
        ''' Rotates the camera.
        ''' </summary>
        ''' <param name="Delta">
        ''' Change in mouse co-ordinates.
        ''' </param>
        Public Sub CameraRotate(ByVal Delta As Vector2)
            Dim Len As Single = CSng(2^m_Zoom)

            ' Update the rotation vector.
            m_Rot.X += 5.0F*Delta.X/m_Width
            m_Rot.Y -= 5.0F*Delta.Y/m_Height

            ' Check the X-rotation angle.
            If Math.Abs(m_Rot.Y) > Math.PI/2 Then _
                m_Rot.Y = Math.Sign(m_Rot.Y)*CSng(Math.PI/2)

            ' Build the transform matrix.
            Dim M As Matrix = Matrix.RotationX(m_Rot.Y)*
                              Matrix.RotationY(m_Rot.X)

            ' Initialize initial vectors.
            m_EyePos = New Vector3(0, 0, Len)
            m_UpVector = New Vector3(0, 1, 0)

            ' Transform.
            m_EyePos.TransformCoordinate(M)
            m_UpVector.TransformCoordinate(M)

            ' Add the look-at position.
            m_EyePos += m_LookAtPosition

            ' Build the view matrix.
            BuildViewMatrix()
        End Sub

        ''' <summary>
        ''' Zooms the camera.
        ''' </summary>
        ''' <param name="Delta">
        ''' Change in mouse co-ordinates.
        ''' </param>
        Public Sub CameraZoom(ByVal Delta As Vector2)
            ' Check for +ve direction.
            If (Delta.X < 0) OrElse (Delta.Y < 0) Then _
                m_Zoom -= Delta.Length()/10 _
                Else _
                m_Zoom += Delta.Length()/10

            ' Calculate distance from 'LookAt' position.
            Dim Len As Single = CSng(2^m_Zoom)

            ' Check length of vector; if length = 0 then eye position and
            ' look at position co-incide.
            If Len < 0.001 Then _
                Len = 0.001 _
                    : m_Zoom = CSng(Math.Log(Len)/Math.Log(2))

            ' Subtract look-at position from eye position to get the direction.
            m_EyePos -= m_LookAtPosition

            ' Change length of vector.
            m_EyePos.Normalize()
            m_EyePos.Scale(Len)

            ' Add look-at position to eye direction to get the position.
            m_EyePos += m_LookAtPosition

            ' Build the view matrix.
            BuildViewMatrix()
        End Sub

        ''' <summary>
        ''' Resets camera position and target.
        ''' </summary>
        Public Overrides Sub Reset(Optional ByVal DistanceFromOrigin As Single = 1)
            ' Check inputs.
            If DistanceFromOrigin <= 0 Then _
                Throw New ArgumentOutOfRangeException("DistanceFromOrigin") _
                    : Exit Sub

            ' Call the base method.
            MyBase.Reset(DistanceFromOrigin)

            ' Set the zoom parameter.
            m_Zoom = CSng(Math.Log(DistanceFromOrigin)/Math.Log(2))

            ' Set the rotation parameter.
            m_Rot = New Vector2(0, 0)
        End Sub
    End Class
End Namespace
