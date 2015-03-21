Imports System.Linq
Imports GenericMesh

''' <summary>
''' Class representing a Homeworld2 joint
''' </summary>
Public NotInheritable Class Joint
 ' --------------
 ' Class Members.
 ' --------------
 ''' <summary>Name.</summary>
 Private m_Name As String

 ''' <summary>Visible.</summary>
 Private m_Visible As Boolean

 ''' <summary>Parent.</summary>
 Private m_Parent As Joint

 ''' <summary>Children.</summary>
 Private WithEvents m_Children As New EventList(Of Joint)

 ''' <summary>Position.</summary>
 Private m_Position As Vector3

 ''' <summary>Normal.</summary>
 Private m_Rotation As Vector3

 ''' <summary>Scale.</summary>
 Private m_Scale As Vector3

 ''' <summary>Axis.</summary>
 Private m_Axis As Vector3

 ''' <summary>Degree of freedom.</summary>
 Private m_DegreeOfFreedom As Vector3

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
 Public Sub New(ByVal j As Joint)
  m_Name = j.m_Name
  m_Visible = j.m_Visible
  m_Position = j.m_Position
  m_Rotation = j.m_Rotation
  m_Scale = j.m_Scale
  m_Axis = j.m_Axis
  m_DegreeOfFreedom = j.m_DegreeOfFreedom

  For I As Integer = 0 To j.m_Children.Count - 1
   m_Children.Add(New Joint(j.m_Children(I)))

  Next I ' For I As Integer = 0 To j.m_Children.Count - 1
  
 End Sub

 ' -----------------
 ' Class properties.
 ' -----------------
 ''' <summary>
 ''' Returns\Sets name of joint.
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
 ''' Returns\Sets whether this joint is visible or not.
 ''' </summary>
 ''' <remarks>
 ''' This is purely a rendering related property. It does not
 ''' affect what is written in the HOD.
 ''' </remarks>
 Public Property Visible() As Boolean
  Get
   Return m_Visible

  End Get

  Set(ByVal value As Boolean)
   m_Visible = value

  End Set

 End Property

 ''' <summary>
 ''' Returns the parent of this joint.
 ''' </summary>
 Public ReadOnly Property Parent() As Joint
  Get
   Return m_Parent

  End Get

 End Property

 ''' <summary>
 ''' Returns the list of children.
 ''' </summary>
 Public ReadOnly Property Children() As IList(Of Joint)
  Get
   Return m_Children

  End Get

 End Property

 ''' <summary>
 ''' Returns\Sets position of joint.
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
 ''' Returns\Sets rotation of joint.
 ''' </summary>
 Public Property Rotation() As Vector3
  Get
   Return m_Rotation

  End Get

  Set(ByVal value As Vector3)
   m_Rotation = value

  End Set

 End Property

 ''' <summary>
 ''' Returns\Sets scale of joint.
 ''' </summary>
 Public Property Scale() As Vector3
  Get
   Return m_Scale

  End Get

  Set(ByVal value As Vector3)
   m_Scale = value

  End Set

 End Property

 ''' <summary>
 ''' Returns\Sets axis of joint.
 ''' </summary>
 Public Property Axis() As Vector3
  Get
   Return m_Axis

  End Get

  Set(ByVal value As Vector3)
   m_Axis = value

  End Set

 End Property

 ''' <summary>
 ''' Returns\Sets degree of freedom.
 ''' </summary>
 ''' <remarks>
 ''' Any non-zero value will be assumed to be <c>True</c>
 ''' and a zero will be assumed to be <c>False</c>.
 ''' </remarks>
 Public Property DegreeOfFreedom() As Vector3
  Get
   Return m_DegreeOfFreedom

  End Get

  Set(ByVal value As Vector3)
   If value.X <> 0.0F Then _
    m_DegreeOfFreedom.X = 1 _
   Else _
    m_DegreeOfFreedom.X = 0

   If value.Y <> 0.0F Then _
    m_DegreeOfFreedom.Y = 1 _
   Else _
    m_DegreeOfFreedom.Y = 0

   If value.Z <> 0.0F Then _
    m_DegreeOfFreedom.Z = 1 _
   Else _
    m_DegreeOfFreedom.Z = 0

  End Set

 End Property

 ''' <summary>
 ''' Returns the transform caused by this joint only, 
 ''' not it's parents.
 ''' </summary>
 Private ReadOnly Property MyTransform() As Matrix
  Get
   Return Matrix.Scaling(m_Scale) * _
          Matrix.RotationX(m_Rotation.X + m_Axis.X) * _
          Matrix.RotationY(m_Rotation.Y + m_Axis.Y) * _
          Matrix.RotationZ(m_Rotation.Z + m_Axis.Z) * _
          Matrix.Translation(m_Position)

  End Get

 End Property

 ''' <summary>
 ''' Returns the transform of this joint, upto Root.
 ''' </summary>
 Public ReadOnly Property Transform() As Matrix
  Get
   Dim j As Joint = Me
   Dim mOut As Matrix = Matrix.Identity

   Do Until j Is Nothing
    ' Multiply matrices and accumulate.
    mOut *= j.MyTransform

    ' Get it's parent.
    j = j.m_Parent

   Loop ' Do Until j Is Nothing

   Return mOut

  End Get

 End Property

 ' -----------------
 ' Member Functions.
 ' -----------------
 ''' <summary>
 ''' Returns the name of this joint.
 ''' </summary>
 Public Overrides Function ToString() As String
  Return m_Name

 End Function

 ''' <summary>
 ''' Reads a joint (not it's children) from an IFF reader.
 ''' </summary>
 ''' <param name="IFF">
 ''' IFF reader to read from.
 ''' </param>
 ''' <param name="parentName">
 ''' Returns the parent name of this joint.
 ''' </param>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when 'IFF' is nothing.
 ''' </exception>
 Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader, ByRef parentName As String)
  ' Read name and parent name.
  m_Name = IFF.ReadString()
  parentName = IFF.ReadString()

  ' Read position.
  m_Position.X = IFF.ReadSingle()
  m_Position.Y = IFF.ReadSingle()
  m_Position.Z = IFF.ReadSingle()

  ' Read rotation.
  m_Rotation.X = IFF.ReadSingle()
  m_Rotation.Y = IFF.ReadSingle()
  m_Rotation.Z = IFF.ReadSingle()

  ' Read scale.
  m_Scale.X = IFF.ReadSingle()
  m_Scale.Y = IFF.ReadSingle()
  m_Scale.Z = IFF.ReadSingle()

  ' Read axis.
  m_Axis.X = IFF.ReadSingle()
  m_Axis.Y = IFF.ReadSingle()
  m_Axis.Z = IFF.ReadSingle()

  ' Read degree of freedom.
  m_DegreeOfFreedom.X = IFF.ReadByte()
  m_DegreeOfFreedom.Y = IFF.ReadByte()
  m_DegreeOfFreedom.Z = IFF.ReadByte()

 End Sub

 ''' <summary>
 ''' Writes a joint (not it's children) to an IFF writer.
 ''' </summary>
 ''' <param name="IFF">
 ''' IFF writer to write to.
 ''' </param>
 ''' <param name="parentName">
 ''' Name of this joint's parent.
 ''' </param>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when 'IFF' is nothing.
 ''' </exception>
 Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter, ByVal parentName As String)
  ' Write name and parent name.
  IFF.Write(m_Name)
  IFF.Write(parentName)

  ' Write position.
  IFF.Write(m_Position.X)
  IFF.Write(m_Position.Y)
  IFF.Write(m_Position.Z)

  ' Write rotation.
  IFF.Write(m_Rotation.X)
  IFF.Write(m_Rotation.Y)
  IFF.Write(m_Rotation.Z)

  ' Write scale.
  IFF.Write(m_Scale.X)
  IFF.Write(m_Scale.Y)
  IFF.Write(m_Scale.Z)

  ' Write axis.
  IFF.Write(m_Axis.X)
  IFF.Write(m_Axis.Y)
  IFF.Write(m_Axis.Z)

  ' Write degree of freedom.
  If m_DegreeOfFreedom.X <> 0 Then _
   IFF.Write(CByte(1)) _
  Else _
   IFF.Write(CByte(0))

  If m_DegreeOfFreedom.Y <> 0 Then _
   IFF.Write(CByte(1)) _
  Else _
   IFF.Write(CByte(0))

  If m_DegreeOfFreedom.Z <> 0 Then _
   IFF.Write(CByte(1)) _
  Else _
   IFF.Write(CByte(0))

 End Sub

 ''' <summary>
 ''' Reads a joint hierarchy chunk from an IFF reader.
 ''' </summary>
 ''' <param name="IFF">
 ''' IFF reader to read from.
 ''' </param>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when 'IFF' is nothing.
 ''' </exception>
 Friend Shared Sub ReadHIERChunk(ByVal IFF As IFF.IFFReader, ByVal jRoot As Joint)
  Dim str As String = ""

  ' Get joint count.
  Dim count As Integer = IFF.ReadInt32()

  ' Read this joint.
  jRoot.ReadIFF(IFF, str)

  ' Read remaining joints.
  For I As Integer = 2 To count
   ' Make new joint.
   Dim j As New Joint

   ' Read joint.
   j.ReadIFF(IFF, str)

   ' Try to get parent.
   Dim parent As Joint = jRoot.GetJointByName(str)

   ' Add to parent if possible.
   If parent Is Nothing Then _
    Trace.TraceError("Joint '" & j.m_Name & "' refers to non-existant parent '" & str & "', now parented under '" & jRoot.m_Name & "'.") _
  : jRoot.m_Children.Add(j) _
   Else _
    parent.m_Children.Add(j)

  Next I ' For I As Integer = 2 To count

 End Sub

 ''' <summary>
 ''' Writes a joint hierarchy chunk to an IFF writer.
 ''' </summary>
 ''' <param name="IFF">
 ''' IFF writer to write to.
 ''' </param>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when 'IFF' is nothing.
 ''' </exception>
 Friend Shared Sub WriteHIERChunk(ByVal IFF As IFF.IFFWriter, ByVal jRoot As Joint)
  Dim Q As New Stack(Of Joint)

  ' Get joint count.
  Dim jcount As Integer = jRoot.Count()
  Dim jprocessed As Integer = 0

  IFF.Push("HIER")

  ' Write joint count.
  IFF.WriteInt32(jcount)

  ' Check name.
  If jRoot.m_Name <> "Root" Then _
   Trace.TraceError("HOD doesn't have a 'Root' joint.") _
 : jRoot.m_Name = "Root"

  ' Enqueue self.
  Q.Push(jRoot)

  ' Process each joint.
  Do Until Q.Count = 0
   ' Dequeue joint.
   Dim j As Joint = Q.Pop()

            ' Enqueue children of this joint.
   For Each Child As Joint In j.m_Children.Reverse()
    Q.Push(Child)

   Next ' For I As Integer = 0 To j.m_Children.Count - 1

   ' Get parent name
   Dim parentName As String = ""

   If j.m_Parent IsNot Nothing Then _
    parentName = j.m_Parent.Name

   ' Write joint.
   j.WriteIFF(IFF, parentName)
   jprocessed += 1

  Loop ' Do Until Q.Count = 0

  If jcount <> jprocessed Then _
   Trace.TraceError("Error while writing joints, count mismatch.")

  IFF.Pop()

 End Sub

 ''' <summary>
 ''' Returns the number of joints, parented under this joint
 ''' and including this joint.
 ''' </summary>
 Public Function Count() As Integer
  Dim Q As New Queue(Of Joint)
  Dim jcount As Integer = 0

  ' Enqueue self.
  Q.Enqueue(Me)

  ' Process each joint.
  Do Until Q.Count = 0
   ' Dequeue joint.
   Dim j As Joint = Q.Dequeue()

   ' Enqueue children of this joint.
   For I As Integer = 0 To j.m_Children.Count - 1
    Q.Enqueue(j.m_Children(I))

   Next I ' For I As Integer = 0 To j.m_Children.Count - 1

   ' Increment count.
   jcount += 1

  Loop ' Do Until Q.Count = 0

  Return jcount

 End Function

 ''' <summary>
 ''' Returns an array of all joints, parented under this joint,
 ''' including this joint.
 ''' </summary>
 Public Function ToArray() As Joint()
  Dim Q As New Queue(Of Joint)
  Dim out As New List(Of Joint)

  ' Enqueue self.
  Q.Enqueue(Me)

  ' Process each joint.
  Do Until Q.Count = 0
   ' Dequeue joint.
   Dim j As Joint = Q.Dequeue()

   ' Enqueue children of this joint.
   For I As Integer = 0 To j.m_Children.Count - 1
    Q.Enqueue(j.m_Children(I))

   Next I ' For I As Integer = 0 To j.m_Children.Count - 1

   ' Add to list.
   out.Add(j)

  Loop ' Do Until Q.Count = 0

  Return out.ToArray()

 End Function

 ''' <summary>
 ''' Returns whether a joint contains another joint.
 ''' </summary>
 ''' <param name="name">
 ''' Name of joint to look for.
 ''' </param>
 ''' <remarks>
 ''' The search is case insensitive.
 ''' </remarks>
 Public Function Contains(ByVal name As String) As Boolean
  Dim Q As New Queue(Of Joint)

  ' Enqueue self.
  Q.Enqueue(Me)

  ' Process each joint.
  Do Until Q.Count = 0
   ' Dequeue joint.
   Dim j As Joint = Q.Dequeue()

   ' Enqueue children of this joint.
   For I As Integer = 0 To j.m_Children.Count - 1
    Q.Enqueue(j.m_Children(I))

   Next I ' For I As Integer = 0 To j.m_Children.Count - 1

   ' Compare name.
   If String.Compare(j.m_Name, name, True) = 0 Then _
    Return True

  Loop ' Do Until Q.Count = 0

  Return False

 End Function

 ''' <summary>
 ''' Returns the joint with the specified name if present, otherwise,
 ''' <c>Nothing</c>.
 ''' </summary>
 ''' <param name="name">
 ''' Name of joint to look for.
 ''' </param>
 ''' <remarks>
 ''' The search is case insensitive.
 ''' </remarks>
 Public Function GetJointByName(ByVal name As String) As Joint
  Dim Q As New Queue(Of Joint)

  ' Enqueue self.
  Q.Enqueue(Me)

  ' Process each joint.
  Do Until Q.Count = 0
   ' Dequeue joint.
   Dim j As Joint = Q.Dequeue()

   ' Enqueue children of this joint.
   For I As Integer = 0 To j.m_Children.Count - 1
    Q.Enqueue(j.m_Children(I))

   Next I ' For I As Integer = 0 To j.m_Children.Count - 1

   ' Compare name.
   If String.Compare(j.m_Name, name, True) = 0 Then _
    Return j

  Loop ' Do Until Q.Count = 0

  Return Nothing

 End Function

 ''' <summary>
 ''' Returns the joint which has a child with specified name, if present,
 ''' otherwise, <c>Nothing</c>.
 ''' </summary>
 ''' <param name="name">
 ''' Name of joint whose parent to look for.
 ''' </param>
 ''' <remarks>
 ''' The search is case insensitive.
 ''' </remarks>
 Public Function GetJointParentByName(ByVal name As String) As Joint
  ' Get the joint.
  Dim j As Joint = GetJointByName(name)

  ' If it doesn't exist, it's parent doesn't exist.
  If j Is Nothing Then _
   Return Nothing

  ' Return it's parent.
  Return j.m_Parent

 End Function

 ''' <summary>
 ''' Deletes this joint and all it's children, by unlinking them.
 ''' Do this when you want to remove a joint permanently.
 ''' </summary>
 Public Sub Delete()
  ' Perform delete on all child nodes.
  For I As Integer = 0 To m_Children.Count - 1
   m_Children(I).Delete()

  Next I ' For I As Integer = 0 To m_Children.Count - 1

  ' Remove all children.
  m_Children.Clear()

  ' Remove parent reference.
  m_Parent = Nothing

 End Sub

 ''' <summary>
 ''' Initializes the joint.
 ''' </summary>
 Friend Sub Initialize(Optional ByVal name As String = "Joint")
  ' Perform delete.
  Delete()

  ' Set name.
  m_Name = name
  m_Visible = False

  ' Now reset properties.
  m_Position = New Vector3(0, 0, 0)
  m_Rotation = New Vector3(0, 0, 0)
  m_Scale = New Vector3(1, 1, 1)
  m_Axis = New Vector3(0, 0, 0)
  m_DegreeOfFreedom = New Vector3(1, 1, 1)

 End Sub

 ''' <summary>
 ''' Renders the joint and it's children.
 ''' </summary>
 ''' <remarks>
 ''' The meshes must be locked.
 ''' </remarks>
 Friend Sub Render(ByVal Device As Direct3D.Device, ByVal _Transform As Matrix, _
                   ByVal JointMesh As Standard.BasicMesh, _
          Optional ByVal JointLinkMesh As Standard.BasicMesh = Nothing, _
          Optional ByVal SkeletonScale As Single = 1.0F)

  ' Get root transform.
  Dim m As Matrix = Transform * _Transform

  With Device
   ' Set transform.
   .Transform.World = Matrix.Scaling(SkeletonScale, SkeletonScale, SkeletonScale) * m

   ' Draw joint.
   If m_Visible Then _
    JointMesh.Render(Device)

   ' Draw links if needed.
   If JointLinkMesh IsNot Nothing Then
    ' Draw links to all children.
    For I As Integer = 0 To m_Children.Count - 1
     ' Render child.
     m_Children(I).Render(Device, _Transform, JointMesh, JointLinkMesh, SkeletonScale)

     ' See if we need to render this link.
     If (Not m_Visible) OrElse (Not m_Children(I).m_Visible) Then _
      Continue For

     ' Get the position of the child joint.
     Dim p As Vector3 = m_Children(I).Position

     ' Make sure that the link is long enough to make a difference.
     If p.Length() < SkeletonScale Then _
      Continue For

     ' Get the rotation caused by link to a unit length bar with
     ' one end at origin and other on +ve Z axis.
     Dim mLink As Matrix = Matrix.Scaling(SkeletonScale, SkeletonScale, p.Length - SkeletonScale) * _
                           Matrix.Translation(0, 0, SkeletonScale) * _
                           Matrix.RotationX(CSng(Math.Atan2(-p.Y, Math.Sqrt(p.X * p.X + p.Z * p.Z)))) * _
                           Matrix.RotationY(CSng(Math.Atan2(p.X, p.Z)))

     ' Apply transform.
     .Transform.World = mLink * m

     ' Render link.
     JointLinkMesh.Render(Device)

    Next I  ' For I As Integer = 0 To m_Children.Count - 1
   End If ' If JointLinkMesh IsNot Nothing Then
  End With ' With Device

 End Sub

 ''' <summary>
 ''' Adds a parent reference to specified or last joint.
 ''' </summary>
 Private Sub m_Children_AddInsertItem(Optional ByVal index As Integer = -1) Handles m_Children.AddItem, m_Children.InsertItem
  ' See if this method was called through add or insert event.
  If index = -1 Then _
   index = m_Children.Count - 1

  ' Make sure the joint being added isn't this joint itself.
  If (m_Children(index) Is Me) Then _
   m_Children.RemoveAt(index) _
 : Throw New ArgumentException("Adding this joint would create a cycle.") _
 : Exit Sub

  ' Make sure the joint doesn't have any parent.
  If (m_Children(index).m_Parent IsNot Nothing) AndAlso _
     (m_Children(index).m_Parent IsNot Me) Then _
   m_Children.RemoveAt(index) _
 : Throw New ArgumentException("Joint already has a parent.") _
 : Exit Sub

  ' Set it's parent.
  m_Children(index).m_Parent = Me

 End Sub

 ''' <summary>
 ''' Removes the parent reference from specified or all joints.
 ''' </summary>
 Private Sub m_Children_PreRemoveClear(Optional ByVal Index As Integer = -1) Handles m_Children.PreClearList, m_Children.PreRemoveItem
  ' See if this method was called through remove or clear.
  If Index <> -1 Then _
   m_Children(Index).m_Parent = Nothing _
 : Exit Sub

  ' Remove parent reference of all joints.
  For I As Integer = 0 To m_Children.Count - 1
   m_Children(I).m_Parent = Nothing

  Next I ' For I As Integer = 0 To m_Children.Count - 1

 End Sub

End Class
