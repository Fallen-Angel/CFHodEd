Imports Microsoft.DirectX

Namespace Camera
    ''' <summary>
    ''' Class providing basic camera functionality.
    ''' </summary>
    ''' <remarks>
    ''' This is intended to be a code-controlled camera.
    ''' </remarks>
    Public Class Camera
        Inherits BaseCamera

        ' --------------
        ' Class Members.
        ' --------------
        ' None

        ' ------------------------
        ' Constructors\Finalizers.
        ' ------------------------
        ' None

        ' -----------------
        ' Class properties.
        ' -----------------
        ''' <summary>
        ''' Returns\Sets the camera target.
        ''' </summary>
        Public Shadows Property CameraTarget() As CameraTarget
            Get
                Return MyBase.CameraTarget
            End Get

            Set(ByVal value As CameraTarget)
                MyBase.CameraTarget = value
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets camera (eye) position.
        ''' </summary>
        Public Shadows Property Position() As Vector3
            Get
                Return MyBase.Position()
            End Get

            Set(ByVal value As Vector3)
                MyBase.Position = value
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets camera look at target.
        ''' </summary>
        ''' <remarks>
        ''' Applicable only for <c>CameraTarget.FixedPoint</c> mode. Hence
        ''' setting this value may change the <c>CameraTarget</c> property.
        ''' </remarks>
        Public Shadows Property LookAtTarget() As Vector3
            Get
                Return MyBase.LookAtTarget
            End Get

            Set(ByVal value As Vector3)
                MyBase.LookAtTarget = value
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets camera look at direction.
        ''' </summary>
        ''' <remarks>
        ''' Applicable only for <c>CameraTarget.Direction</c> mode. Hence
        ''' setting this value may change the <c>CameraTarget</c> property.
        ''' </remarks>
        Public Shadows Property LookAtDirection() As Vector3
            Get
                Return MyBase.LookAtDirection
            End Get

            Set(ByVal value As Vector3)
                MyBase.LookAtDirection = value
            End Set
        End Property

        ''' <summary>
        ''' Returns\Sets camera up-vector.
        ''' </summary>
        Public Shadows Property UpVector() As Vector3
            Get
                Return MyBase.UpVector
            End Get

            Set(ByVal value As Vector3)
                MyBase.UpVector = value
            End Set
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
        ' None.
    End Class
End Namespace
