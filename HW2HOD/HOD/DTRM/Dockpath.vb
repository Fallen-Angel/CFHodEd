Imports GenericMesh

''' <summary>
''' Class to represent Homeworld2 Dockpath.
''' </summary>
Public NotInheritable Class Dockpath
    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Name.</summary>
    Private m_Name As String

    ''' <summary>Parent Name.</summary>
    Private m_ParentName As String

    ''' <summary>Globals.</summary>
    Private m_Global As New DockpathGlobalProperties

    ''' <summary>Dockpoints.</summary>
    Private m_Points As New EventList(Of Dockpoint)

    ' ------------------------
    ' Constructors\Finalizers.
    ' ------------------------
    ''' <summary>
    ''' Class contructor.
    ''' </summary>
    Public Sub New()
        Initialize()
    End Sub

    ''' <summary>
    ''' Copy contructor.
    ''' </summary>
    Public Sub New(ByVal d As Dockpath)
        m_Name = d.m_Name
        m_ParentName = d.m_ParentName
        m_Global = New DockpathGlobalProperties(d.m_Global)

        For I As Integer = 0 To d.m_Points.Count - 1
            m_Points.Add(New Dockpoint(d.m_Points(I)))

        Next I ' For I As Integer = 0 To d.m_Points.Count - 1
    End Sub

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns\Sets name, usually 'pathX' where X is a number.
    ''' </summary>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>value Is Nothing</c>.
    ''' </exception>
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
    ''' Returns\Sets parent name, usually 'world'.
    ''' </summary>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>value Is Nothing</c>.
    ''' </exception>
    Public Property ParentName() As String
        Get

            Return m_ParentName
        End Get

        Set(ByVal value As String)
            If (value Is Nothing) OrElse (value = "") Then _
                Throw New ArgumentNullException("value") _
                    : Exit Property

            m_ParentName = value
        End Set
    End Property

    ''' <summary>
    ''' Returns the set of global properties for this dockpath..
    ''' </summary>
    Public ReadOnly Property [Global]() As DockpathGlobalProperties
        Get
            Return m_Global
        End Get
    End Property

    ''' <summary>
    ''' Returns the list of dockpoints in this dockpath.
    ''' </summary>
    Public ReadOnly Property Dockpoints() As IList(Of Dockpoint)
        Get
            Return m_Points
        End Get
    End Property

    ''' <summary>
    ''' Whether this dockpath is visible or not.
    ''' </summary>
    ''' <remarks>
    ''' This is purely a rendering related field. It does not
    ''' affect what is written in the HOD.
    ''' </remarks>
    Public Property Visible() As Boolean
        Get
            Return m_Global.Visible
        End Get

        Set(ByVal value As Boolean)
            m_Global.Visible = value
        End Set
    End Property

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Returns the name of this dockpath.
    ''' </summary>
    Public Overrides Function ToString() As String
        Return m_Name
    End Function

    ''' <summary>
    ''' Reads the dockpath from an IFF reader.
    ''' </summary>
    Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader)
        ' Initialize.
        Initialize()

        ' Read name and parent name.
        m_Name = IFF.ReadString()
        m_ParentName = IFF.ReadString()

        ' Read global and local property count.
        Dim numGlobals As Integer = IFF.ReadInt32()
        Dim numLocals As Integer = IFF.ReadInt32()

        ' Read globals.
        m_Global.ReadIFF(IFF, numGlobals)

        ' Read number of points.
        Dim numPoints As Integer = IFF.ReadInt32()

        ' Read all points.
        For I As Integer = 1 To numPoints
            Dim p As New Dockpoint
            p.ReadIFF(IFF, numLocals)
            m_Points.Add(p)

        Next I ' For I As Integer = 1 To numPoints
    End Sub

    ''' <summary>
    ''' Writes the dockpath to an IFF writer.
    ''' </summary>
    Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
        ' Write name and parent name.
        IFF.Write(m_Name)
        IFF.Write(m_ParentName)

        ' Write globals and locals count.
        IFF.WriteInt32(6)
        IFF.WriteInt32(10)

        ' Write globals.
        m_Global.WriteIFF(IFF)

        ' Write point count.
        IFF.WriteInt32(m_Points.Count)

        ' Write points.
        For I As Integer = 0 To m_Points.Count - 1
            m_Points(I).WriteIFF(IFF)

        Next I ' For I As Integer = 0 To m_Points.Count - 1
    End Sub

    ''' <summary>
    ''' Renders the specified dockpoint.
    ''' </summary>
    Friend Sub Render(ByVal Device As Direct3D.Device, ByVal Transform As Matrix,
                      Optional ByVal DockpointMesh As Standard.BasicMesh = Nothing,
                      Optional ByVal DockpointToleranceMesh As Standard.BasicMesh = Nothing,
                      Optional ByVal DockpointScale As Single = 1.0F)

        ' If not visible do nothing.
        If Not m_Global.Visible Then _
            Exit Sub

        ' Cache some states.
        Dim oldLighting As Boolean = Device.RenderState.Lighting,
            oldSpecularEnable As Boolean = Device.RenderState.SpecularEnable,
            oldAmbient As Drawing.Color = Device.RenderState.Ambient,
            oldViewport As Direct3D.Viewport = Device.Viewport,
            vwprt As Direct3D.Viewport = Device.Viewport

        ' Set new states. Also set the viewport so that the dockpath
        ' is rendered in foreground only.
        Device.Transform.World = Transform
        Device.RenderState.Lighting = False
        Device.RenderState.SpecularEnable = False
        Device.RenderState.Ambient = Drawing.Color.White
        vwprt.MinZ = 0
        vwprt.MaxZ = 0

        ' Render dockpath.
        For I As Integer = 0 To m_Points.Count - 2
            ' Get the position vectors.
            Dim vtx() As Vector3 = New Vector3() {m_Points(I).Position, m_Points(I + 1).Position}

            ' Set FVF.
            Device.VertexFormat = Direct3D.VertexFormats.Position

            ' Render line segment.
            Device.DrawUserPrimitives(Direct3D.PrimitiveType.LineList, 1, vtx)

        Next I ' For I As Integer = 0 To m_Points.Count - 2

        ' Set old states.
        Device.RenderState.Lighting = oldLighting
        Device.RenderState.SpecularEnable = oldSpecularEnable
        Device.RenderState.Ambient = oldAmbient
        Device.Viewport = oldViewport

        ' Render the dockpoints, if needed.
        If DockpointMesh IsNot Nothing Then
            For I As Integer = 0 To m_Points.Count - 1
                ' See if it's visible.
                If Not m_Points(I).Visible Then _
                    Continue For

                ' Get the dockpoint.
                Dim p As Dockpoint = m_Points(I)

                ' Make sure the dockpath mesh exists before continuing.
                If DockpointMesh IsNot Nothing Then
                    ' Set world transform.
                    Device.Transform.World = Matrix.Scaling(DockpointScale, DockpointScale, DockpointScale)*
                                             Matrix.RotationX(p.Rotation.X)*
                                             Matrix.RotationY(p.Rotation.Y)*
                                             Matrix.RotationZ(p.Rotation.Z)*
                                             Matrix.Translation(p.Position)*
                                             Transform

                    ' Render mesh.
                    DockpointMesh.Render(Device)

                End If ' If DockpointMesh IsNot Nothing Then

                ' Make sure the dockpath point tolerance mesh exists before continuing.
                ' Also have a look at point tolerance to see if we need to render the mesh.
                If (DockpointToleranceMesh IsNot Nothing) OrElse (p.PointTolerance < 0.001) Then
                    ' Get fill mode and cull mode.
                    Dim oldFillMode As Direct3D.FillMode = Device.RenderState.FillMode
                    Dim oldCullMode As Direct3D.Cull = Device.RenderState.CullMode

                    ' Set new fill mode and cull mode.
                    Device.RenderState.FillMode = Direct3D.FillMode.WireFrame
                    Device.RenderState.CullMode = Direct3D.Cull.None

                    ' Set world transform.
                    Device.Transform.World = Matrix.Scaling(p.PointTolerance, p.PointTolerance, p.PointTolerance)*
                                             Matrix.Translation(p.Position)*
                                             Transform

                    ' Render mesh.
                    DockpointToleranceMesh.Render(Device)

                    ' Set old fill mode and cull mode.
                    Device.RenderState.FillMode = oldFillMode
                    Device.RenderState.CullMode = oldCullMode

                End If ' If (DockpointToleranceMesh IsNot Nothing) OrElse (p.PointTolerance < 0.001) Then
            Next I ' For I As Integer = 0 To m_Points.Count - 1
        End If ' If DockpointMesh IsNot Nothing Then
    End Sub

    ''' <summary>
    ''' Initializes the dockpoint.
    ''' </summary>
    Private Sub Initialize()
        m_Name = "path"
        m_ParentName = "world"
        m_Points.Clear()
    End Sub
End Class

''' <summary>
''' Class to represent Homeworld2 dockpath global properties.
''' </summary>
Public NotInheritable Class DockpathGlobalProperties
    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Is Exit?</summary>
    Public IsExit As Boolean

    ''' <summary>Is Latch?</summary>
    Public IsLatch As Boolean

    ''' <summary>Global tolerance.</summary>
    ''' <remarks>Doesn't seem to be used.</remarks>
    Public Tolerance As Single

    ''' <summary>Comma seperated list of dock families.</summary>
    Public DockFamilies As String

    ''' <summary>Use Animation?</summary>
    Public UseAnimation As Boolean

    ''' <summary>Comma seperated list of linked paths.</summary>
    Public LinkedPaths As String

    ''' <summary>Whether this dockpath is visible or not.</summary>
    Friend Visible As Boolean

    ' ------------------------
    ' Constructors\Finalizers.
    ' ------------------------
    ''' <summary>
    ''' Class contructor.
    ''' </summary>
    Public Sub New()
        Initialize()
    End Sub

    ''' <summary>
    ''' Copy constructor.
    ''' </summary>
    Public Sub New(ByVal d As DockpathGlobalProperties)
        IsExit = d.IsExit
        IsLatch = d.IsLatch
        Tolerance = d.Tolerance
        DockFamilies = d.DockFamilies
        UseAnimation = d.UseAnimation
        LinkedPaths = d.LinkedPaths
        Visible = d.Visible
    End Sub

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Reads a dockpath global properties structure from an IFF reader.
    ''' </summary>
    Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader, ByVal NumGlobalProperties As Integer)
        If NumGlobalProperties > 0 Then _
            If IFF.ReadInt32() <> 0 Then _
                IsExit = True _
                Else _
                IsExit = False

        If NumGlobalProperties > 1 Then _
            If IFF.ReadInt32() <> 0 Then _
                IsLatch = True _
                Else _
                IsLatch = False

        If NumGlobalProperties > 2 Then _
            Tolerance = IFF.ReadSingle()

        If NumGlobalProperties > 3 Then _
            DockFamilies = IFF.ReadString()

        If NumGlobalProperties > 4 Then _
            If IFF.ReadInt32() <> 0 Then _
                UseAnimation = True _
                Else _
                UseAnimation = False

        If NumGlobalProperties > 5 Then _
            LinkedPaths = IFF.ReadString()
    End Sub

    ''' <summary>
    ''' Writes a dockpath global properties structure to an IFF writer.
    ''' </summary>
    Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
        If IsExit Then _
            IFF.WriteInt32(1) _
            Else _
            IFF.WriteInt32(0)

        If IsLatch Then _
            IFF.WriteInt32(1) _
            Else _
            IFF.WriteInt32(0)

        IFF.Write(Tolerance)

        IFF.Write(DockFamilies)

        If UseAnimation Then _
            IFF.WriteInt32(1) _
            Else _
            IFF.WriteInt32(0)

        IFF.Write(LinkedPaths)
    End Sub

    ''' <summary>
    ''' Initializes the dockpath global properties structure.
    ''' </summary>
    Private Sub Initialize()
        IsExit = False
        IsLatch = False
        Tolerance = 0
        DockFamilies = ""
        UseAnimation = False
        LinkedPaths = ""
        Visible = False
    End Sub
End Class

''' <summary>
''' Class to represent Homeworld2 Dockpath point (keyframe).
''' </summary>
Public NotInheritable Class Dockpoint
    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Position.</summary>
    Public Position As Vector3

    ''' <summary>Rotation.</summary>
    Public Rotation As Vector3

    ''' <summary>Use rotation?</summary>
    Public UseRotation As Boolean

    ''' <summary>Point tolerance.</summary>
    Public PointTolerance As Single

    ''' <summary>Drop focus?</summary>
    Public DropFocus As Boolean

    ''' <summary>Max speed.</summary>
    Public MaxSpeed As Single

    ''' <summary>Check rotation?</summary>
    Public CheckRotation As Boolean

    ''' <summary>Force close behavior?</summary>
    Public ForceCloseBehavior As Boolean

    ''' <summary>Player is in control?</summary>
    Public PlayerIsInControl As Boolean

    ''' <summary>Queue origin?</summary>
    Public QueueOrigin As Boolean

    ''' <summary>Use clip plane?</summary>
    Public UseClipPlane As Boolean

    ''' <summary>Clear reservation?</summary>
    Public ClearReservation As Boolean

    ''' <summary>Whether this dockpoint is visible or not.</summary>
    ''' <remarks>This is purely a rendering related field.
    ''' It does not affect what is written in the HOD.</remarks>
    Public Visible As Boolean

    ' ------------------------
    ' Constructors\Finalizers.
    ' ------------------------
    ''' <summary>
    ''' Class contructor.
    ''' </summary>
    Public Sub New()
        Initialize()
    End Sub

    ''' <summary>
    ''' Copy contructor.
    ''' </summary>
    Public Sub New(ByVal d As Dockpoint)
        Position = d.Position
        Rotation = d.Rotation
        UseRotation = d.UseRotation
        PointTolerance = d.PointTolerance
        DropFocus = d.DropFocus
        MaxSpeed = d.MaxSpeed
        CheckRotation = d.CheckRotation
        ForceCloseBehavior = d.ForceCloseBehavior
        PlayerIsInControl = d.PlayerIsInControl
        QueueOrigin = d.QueueOrigin
        UseClipPlane = d.UseClipPlane
        ClearReservation = d.ClearReservation
        Visible = d.Visible
    End Sub

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Reads a dockpoint from an IFF reader.
    ''' </summary>
    Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader, ByVal NumPointProperties As Integer)
        Initialize()

        Position.X = IFF.ReadSingle()
        Position.Y = IFF.ReadSingle()
        Position.Z = IFF.ReadSingle()

        Rotation.X = IFF.ReadSingle()
        Rotation.Y = IFF.ReadSingle()
        Rotation.Z = IFF.ReadSingle()

        If NumPointProperties > 0 Then _
            If IFF.ReadInt32() <> 0 Then _
                UseRotation = True _
                Else _
                UseRotation = False

        If NumPointProperties > 1 Then _
            PointTolerance = IFF.ReadSingle()

        If NumPointProperties > 2 Then _
            If IFF.ReadInt32() <> 0 Then _
                DropFocus = True _
                Else _
                DropFocus = False

        If NumPointProperties > 3 Then _
            MaxSpeed = IFF.ReadSingle()

        If NumPointProperties > 4 Then _
            If IFF.ReadInt32() <> 0 Then _
                CheckRotation = True _
                Else _
                CheckRotation = False

        If NumPointProperties > 5 Then _
            If IFF.ReadInt32() <> 0 Then _
                ForceCloseBehavior = True _
                Else _
                ForceCloseBehavior = False

        If NumPointProperties > 6 Then _
            If IFF.ReadInt32() <> 0 Then _
                PlayerIsInControl = True _
                Else _
                PlayerIsInControl = False

        If NumPointProperties > 7 Then _
            If IFF.ReadInt32() <> 0 Then _
                QueueOrigin = True _
                Else _
                QueueOrigin = False

        If NumPointProperties > 8 Then _
            If IFF.ReadInt32() <> 0 Then _
                UseClipPlane = True _
                Else _
                UseClipPlane = False

        If NumPointProperties > 9 Then _
            If IFF.ReadInt32() <> 0 Then _
                ClearReservation = True _
                Else _
                ClearReservation = False
    End Sub

    ''' <summary>
    ''' Writes a dockpoint to an IFF writer.
    ''' </summary>
    Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
        IFF.Write(Position.X)
        IFF.Write(Position.Y)
        IFF.Write(Position.Z)

        IFF.Write(Rotation.X)
        IFF.Write(Rotation.Y)
        IFF.Write(Rotation.Z)

        If UseRotation Then _
            IFF.WriteInt32(1) _
            Else _
            IFF.WriteInt32(0)

        IFF.Write(PointTolerance)

        If DropFocus Then _
            IFF.WriteInt32(1) _
            Else _
            IFF.WriteInt32(0)

        IFF.Write(MaxSpeed)

        If CheckRotation Then _
            IFF.WriteInt32(1) _
            Else _
            IFF.WriteInt32(0)

        If ForceCloseBehavior Then _
            IFF.WriteInt32(1) _
            Else _
            IFF.WriteInt32(0)

        If PlayerIsInControl Then _
            IFF.WriteInt32(1) _
            Else _
            IFF.WriteInt32(0)

        If QueueOrigin Then _
            IFF.WriteInt32(1) _
            Else _
            IFF.WriteInt32(0)

        If UseClipPlane Then _
            IFF.WriteInt32(1) _
            Else _
            IFF.WriteInt32(0)

        If ClearReservation Then _
            IFF.WriteInt32(1) _
            Else _
            IFF.WriteInt32(0)
    End Sub

    ''' <summary>
    ''' Initializes the dockpoint.
    ''' </summary>
    Private Sub Initialize()
        Position = New Vector3(0, 0, 0)
        Rotation = New Vector3(0, 0, 0)
        UseRotation = False
        PointTolerance = 0
        DropFocus = False
        MaxSpeed = 0
        CheckRotation = False
        ForceCloseBehavior = False
        PlayerIsInControl = False
        QueueOrigin = False
        UseClipPlane = False
        ClearReservation = False
        Visible = True
    End Sub
End Class
