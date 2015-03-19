Imports Microsoft.DirectX

Namespace Camera
 ''' <summary>
 ''' Enumeration describing camera targets.
 ''' </summary>
 Public Enum CameraTarget
  ''' <summary>
  ''' Camera targets a fixed point.
  ''' </summary>
  FixedPoint

  ''' <summary>
  ''' Camera has a fixed direction.
  ''' </summary>
  Direction

 End Enum

 ''' <summary>
 ''' Enumeration describing type of projection matrix.
 ''' </summary>
 Public Enum ProjectionType
  ''' <summary>
  ''' Perspective matrix.
  ''' </summary>
  Perspective

  ''' <summary>
  ''' Perspective matrix based on field of view.
  ''' </summary>
  PerspectiveFov

  ''' <summary>
  ''' Orthogonal matrix.
  ''' </summary>
  Orthogonal

 End Enum

 ''' <summary>
 ''' Class providing basic camera features.
 ''' </summary>
 Public MustInherit Class BaseCamera
  ' --------------
  ' Class Members.
  ' --------------
  ''' <summary>Camera mode.</summary>
  Protected m_Mode As CameraTarget

  ''' <summary>Position of camera.</summary>
  Protected m_EyePos As Vector3

  ''' <summary>Camera look at target.</summary>
  Protected m_LookAtPosition As Vector3

  ''' <summary>Camera look at direction.</summary>
  Protected m_LookDirection As Vector3

  ''' <summary>Camera up vector.</summary>
  Protected m_UpVector As Vector3

  ''' <summary>View matrix.</summary>
  Protected m_View As Matrix

  ''' <summary>Field of view angle.</summary>
  Protected m_FOV As Single

  ''' <summary>Width of viewport.</summary>
  Protected m_Width As Single

  ''' <summary>Height of viewport.</summary>
  Protected m_Height As Single

  ''' <summary>Near clip-plane.</summary>
  Protected m_ZNear As Single

  ''' <summary>Far clip-plane.</summary>
  Protected m_ZFar As Single

  ''' <summary>Projection Type</summary>
  Protected m_ProjType As ProjectionType

  ''' <summary>Projection matrix.</summary>
  Protected m_Proj As Matrix

  ''' <summary>Device associated with the camera.</summary>
  Protected m_Device As Direct3D.Device

  ' ------------------------
  ' Constructors\Finalizers.
  ' ------------------------
  ''' <summary>
  ''' Class constructor.
  ''' </summary>
  Protected Sub New()
   ' View variables.
   m_Mode = CameraTarget.FixedPoint
   m_EyePos = New Vector3(0, 0, 1)
   m_LookAtPosition = New Vector3(0, 0, 0)
   m_LookDirection = m_LookAtPosition - m_EyePos
   m_UpVector = New Vector3(0, 1, 0)

   ' Projection variables.
   m_FOV = CSng(Math.PI) / 2
   m_Width = Screen.PrimaryScreen.Bounds.Width
   m_Height = Screen.PrimaryScreen.Bounds.Height
   m_ZNear = 0.1
   m_ZFar = 10000
   m_ProjType = ProjectionType.PerspectiveFov

   ' Default view/projection matrices.
   BuildViewMatrix()
   BuildProjMatrix()

  End Sub

  ''' <summary>
  ''' Class finalizer.
  ''' </summary>
  Protected Overrides Sub Finalize()
   ' Nothing here.
   MyBase.Finalize()

  End Sub

  ' -----------------
  ' Class properties.
  ' -----------------
  ''' <summary>
  ''' Returns\Sets the camera target.
  ''' </summary>
  Protected Property CameraTarget() As CameraTarget
   Get
    Return m_Mode

   End Get

   Set(ByVal value As CameraTarget)
    If (value <> CameraTarget.FixedPoint) AndAlso _
       (value <> CameraTarget.Direction) Then _
     Throw New ArgumentException("Invalid 'value'.") _
   : Exit Property

    ' Set the mode.
    m_Mode = value

    ' Build the view matrix.
    BuildViewMatrix()

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets camera (eye) position.
  ''' </summary>
  Protected Property Position() As Vector3
   Get
    Return m_EyePos

   End Get

   Set(ByVal value As Vector3)
    ' Set the position.
    m_EyePos = value

    ' Build the view matrix.
    BuildViewMatrix()

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets camera look at target.
  ''' </summary>
  ''' <remarks>
  ''' Applicable only for <c>CameraTarget.FixedPoint</c> mode. Hence
  ''' setting this value may change the <c>CameraTarget</c> property.
  ''' </remarks>
  Protected Property LookAtTarget() As Vector3
   Get
    If m_Mode = CameraTarget.FixedPoint Then _
     Return m_LookAtPosition _
    Else _
     Return m_EyePos + m_LookDirection

   End Get

   Set(ByVal value As Vector3)
    ' Set the look at position.
    m_LookAtPosition = value
    m_Mode = CameraTarget.FixedPoint

    ' Build the view matrix.
    BuildViewMatrix()

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets camera look at direction.
  ''' </summary>
  ''' <remarks>
  ''' Applicable only for <c>CameraTarget.Direction</c> mode. Hence
  ''' setting this value may change the <c>CameraTarget</c> property.
  ''' </remarks>
  Protected Property LookAtDirection() As Vector3
   Get
    If m_Mode = CameraTarget.Direction Then _
     Return m_LookDirection _
    Else _
     Return m_LookAtPosition - m_EyePos

   End Get

   Set(ByVal value As Vector3)
    ' Set the look at direction.
    m_LookDirection = value
    m_Mode = CameraTarget.Direction

    ' Build the view matrix.
    BuildViewMatrix()

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets camera up-vector.
  ''' </summary>
  Protected Property UpVector() As Vector3
   Get
    Return m_UpVector

   End Get

   Set(ByVal value As Vector3)
    If value.LengthSq() = 0 Then _
     Throw New ArgumentException("Vector 'value' has zero length.") _
   : Exit Property

    ' Set the up vector.
    m_UpVector = value

    ' Build the view matrix.
    BuildViewMatrix()

   End Set

  End Property

  ''' <summary>
  ''' Returns the view matrix.
  ''' </summary>
  Protected ReadOnly Property ViewMatrix() As Matrix
   Get
    Return m_View

   End Get

  End Property

  ''' <summary>
  ''' Returns\Sets the field of view.
  ''' </summary>
  ''' <remarks>
  ''' Applicable only for orthogonal projection. Hence this
  ''' may change the <c>ProjectionType</c> property.
  ''' </remarks>
  Protected Property FOV() As Single
   Get
    If m_ProjType = ProjectionType.PerspectiveFov Then _
     Return m_FOV _
    Else _
     Return 0

   End Get

   Set(ByVal value As Single)
    If (value < 0) OrElse (value > Math.PI) Then _
     Throw New ArgumentException("Invalid 'value'.") _
   : Exit Property

    ' Set the field of view.
    m_FOV = value
    m_ProjType = ProjectionType.PerspectiveFov

    ' Build the projection matrix.
    BuildProjMatrix()

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets the camera width.
  ''' </summary>
  ''' <remarks>
  ''' Applicable only for orthogonal projection. Hence this
  ''' may change the <c>ProjectionType</c> property.
  ''' </remarks>
  Protected Property Width() As Single
   Get
    Return m_Width

   End Get

   Set(ByVal value As Single)
    If (value < 0) Then _
     Throw New ArgumentOutOfRangeException("value") _
   : Exit Property

    ' Set the width.
    m_Width = value

    ' Build the projection matrix.
    BuildProjMatrix()

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets the camera height.
  ''' </summary>
  ''' <remarks>
  ''' Applicable only for orthogonal projection. Hence this
  ''' may change the <c>ProjectionType</c> property.
  ''' </remarks>
  Protected Property Height() As Single
   Get
    Return m_Height

   End Get

   Set(ByVal value As Single)
    If (value < 0) Then _
     Throw New ArgumentOutOfRangeException("value") _
   : Exit Property

    ' Set the height.
    m_Height = value

    ' Build the projection matrix.
    BuildProjMatrix()

   End Set

  End Property

  ''' <summary>
  ''' Returns the aspect ratio.
  ''' </summary>
  Protected ReadOnly Property AspectRatio() As Single
   Get
    Return m_Width / m_Height

   End Get

  End Property

  ''' <summary>
  ''' Returns\Sets the near clip-plane distance.
  ''' </summary>
  Protected Property ZNear() As Single
   Get
    Return m_ZNear

   End Get

   Set(ByVal value As Single)
    If (value < 0) Then _
     Throw New ArgumentOutOfRangeException("value") _
   : Exit Property

    ' Set the near clip-plane distance.
    m_ZNear = value

    ' Build the projection matrix.
    BuildProjMatrix()

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets the near clip-plane distance.
  ''' </summary>
  Protected Property ZFar() As Single
   Get
    Return m_ZFar

   End Get

   Set(ByVal value As Single)
    If (value < 0) Then _
     Throw New ArgumentOutOfRangeException("value") _
   : Exit Property

    ' Set the far clip-plane.
    m_ZFar = value

    ' Build the projection matrix.
    BuildProjMatrix()

   End Set

  End Property

  ''' <summary>
  ''' Returns\Sets whether orthogonal projection is used or not.
  ''' </summary>
  Protected Property ProjectionType() As ProjectionType
   Get
    Return m_ProjType

   End Get

   Set(ByVal value As ProjectionType)
    If (value <> ProjectionType.Perspective) AndAlso _
       (value <> ProjectionType.PerspectiveFov) AndAlso _
       (value <> ProjectionType.Orthogonal) Then _
     Throw New ArgumentException("Invalid 'value'.") _
   : Exit Property

    ' Set the flag.
    m_ProjType = value

    ' Build the projection matrix.
    BuildProjMatrix()

   End Set

  End Property

  ''' <summary>
  ''' Returns the projection matrix.
  ''' </summary>
  Protected ReadOnly Property ProjectionMatrix() As Matrix
   Get
    Return m_Proj

   End Get

  End Property

  ''' <summary>
  ''' Returns\Sets the device associated with this instance.
  ''' </summary>
  Public Overridable Property Device() As Direct3D.Device
   Get
    Return m_Device

   End Get

   Set(ByVal value As Direct3D.Device)
    If value Is Nothing Then _
     Throw New ArgumentNullException("value") _
   : Exit Property

    m_Device = value

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
  ''' Sets the 'ZNear' and 'ZFar' properties simultaneously.
  ''' </summary>
  ''' <param name="ZNear">
  ''' Sets the Z-near plane.
  ''' </param>
  ''' <param name="ZFar">
  ''' Sets the Z-far plane.
  ''' </param>
  Protected Sub SetZ(ByVal ZNear As Single, ByVal ZFar As Single)
   ' Check z-near plane distance.
   If (ZNear <= 0) Then _
    Throw New ArgumentOutOfRangeException("ZNear") _
  : Exit Sub

   ' Check z-far plane distance.
   If (ZFar <= 0) Then _
    Throw New ArgumentOutOfRangeException("ZFar") _
  : Exit Sub

   ' Check relative distances.
   If (ZNear >= ZFar) Then _
    Throw New ArgumentException("ZNear >= ZFar") _
  : Exit Sub

   ' Set valuses.
   m_ZNear = ZNear
   m_ZFar = ZFar

   ' Build projection matrix.
   BuildProjMatrix()

  End Sub

  ''' <summary>
  ''' Builds the view matrix.
  ''' </summary>
  Protected Sub BuildViewMatrix()
   Select Case m_Mode
    ' FIXED CAMERA TARGET
    Case CameraTarget.FixedPoint
     m_View = Matrix.LookAtLH(m_EyePos, m_LookAtPosition, m_UpVector)

     ' FIXED CAMERA DIRECTION
    Case CameraTarget.Direction
     m_View = Matrix.LookAtLH(m_EyePos, m_EyePos + m_LookDirection, m_UpVector)

     ' UNKNOWN (error)
    Case Else
     Throw New Exception("Internal Error.")

   End Select ' Select Case m_Mode

  End Sub

  ''' <summary>
  ''' Builds the projection matrix.
  ''' </summary>
  Protected Sub BuildProjMatrix()
   ' Check clip planes.
   If m_ZNear >= m_ZFar Then _
    Throw New Exception("'m_ZNear >= m_ZFar'") _
  : Exit Sub

   Select Case m_ProjType
    ' PERSPECTIVE
    Case ProjectionType.Perspective
     ' Build the perspective matrix.
     m_Proj = Matrix.PerspectiveLH(m_Width, m_Height, m_ZNear, m_ZFar)

     ' PERSPECTIVE FOV
    Case ProjectionType.PerspectiveFov
     ' Build the perspective matrix.
     m_Proj = Matrix.PerspectiveFovLH(m_FOV, m_Width / m_Height, m_ZNear, m_ZFar)

     ' ORTHOGONAL
    Case ProjectionType.Orthogonal
     ' Build the orthogonal matrix.
     m_Proj = Matrix.OrthoLH(m_Width, m_Height, m_ZNear, m_ZFar)

     ' UNKNOWN (error)
    Case Else
     Throw New Exception("Internal error.")

   End Select ' Select Case m_ProjType

  End Sub

  ''' <summary>
  ''' Returns the camera position.
  ''' </summary>
  Friend Function GetPosition() As Vector3
   Return Position

  End Function

  ''' <summary>
  ''' Returns the camera direction.
  ''' </summary>
  Friend Function GetDirection() As Vector3
   Return LookAtDirection

  End Function

  ''' <summary>
  ''' Resets camera position and target.
  ''' </summary>
  Public Overridable Sub Reset(Optional ByVal DistanceFromOrigin As Single = 1)
   ' Check inputs.
   If DistanceFromOrigin <= 0 Then _
    Throw New ArgumentOutOfRangeException("DistanceFromOrigin") _
  : Exit Sub

   ' Now set the vectors.
   m_EyePos = New Vector3(0, 0, DistanceFromOrigin)
   m_LookAtPosition = New Vector3(0, 0, 0)
   m_LookDirection = New Vector3(0, 0, -1)
   m_UpVector = New Vector3(0, 1, 0)

   ' Update view matrix.
   BuildViewMatrix()

  End Sub

  ''' <summary>
  ''' Updates camera matrices to the device.
  ''' </summary>
  Public Overridable Sub Update()
   ' Now set the matrices.
   If m_Device Is Nothing Then _
    Exit Sub

   With m_Device.Transform
    .View = m_View
    .Projection = m_Proj

   End With ' With m_Device.Transform 

  End Sub

 End Class

End Namespace
