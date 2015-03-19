Imports GenericMesh

''' <summary>
''' Class to represent a Homeworld2 engine shape.
''' </summary>
Public NotInheritable Class EngineShape
 Inherits GBasicMesh(Of PNVertex, Integer, Material.Default)

 ' --------------
 ' Class Members.
 ' --------------
 ''' <summary>Name.</summary>
 Private m_Name As String

 ''' <summary>Parent name.</summary>
 Private m_ParentName As String

 ''' <summary>Whether the mesh is visible or not.</summary>
 Private m_Visible As Boolean

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
 Public Sub New(ByVal es As EngineShape)
  ' Call base contructor.
  MyBase.New(es)

  m_Name = es.m_Name
  m_ParentName = es.m_ParentName
  m_Visible = es.m_Visible

 End Sub

 ' -----------------
 ' Class properties.
 ' -----------------
 ''' <summary>
 ''' Returns\Sets name of engine shape mesh, usually of the form
 ''' "EngineShapeX" where X is a number.
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
 ''' Returns\Sets name of parent of this mesh, usually of the form
 ''' "EngineNozzleX" where X is a number.
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
 ''' Returns\Sets whether the mesh is visible or not.
 ''' </summary>
 Friend Property Visible() As Boolean
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
 ''' Returns the name of this mesh.
 ''' </summary>
 Public Overrides Function ToString() As String
  Return m_Name

 End Function

 ''' <summary>
 ''' Reads the engine shape mesh from an IFF reader.
 ''' </summary>
 Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader)
  ' Initialize mesh.
  Initialize()

  ' Read name and parent name.
  m_Name = IFF.ReadString()
  m_ParentName = IFF.ReadString()

  ' Set part count to 1.
  Me.PartCount = 1

  ' Read all groups.
  Do Until IFF.BaseStream.Position = IFF.BaseStream.Length
   Dim pg As New GPrimitiveGroup(Of Integer)

   ' Set type.
   pg.Type = Direct3D.PrimitiveType.TriangleFan

   ' Read indice count.
   pg.IndiceCount = IFF.ReadInt32()

   ' Read vertices and set indices.
   For I As Integer = 0 To pg.IndiceCount - 1
    ' Set indice.
    pg.Indice(I) = Me.Part(0).Vertices.Count

    ' Read and add vertex.
    Me.Part(0).Vertices.Append(New PNVertex() {PNVertex.ReadIFF(IFF)})

   Next I ' For I As Integer = 0 To pg.IndiceCount - 1

   ' Add primitive group.
   Me.Part(0).AddPrimitiveGroup(pg)

  Loop ' Do Until IFF.BaseStream.Position = IFF.BaseStream.Length

  ' Calculate normals.
  Me.CalculateNormals()

 End Sub

 ''' <summary>
 ''' Writes the engine shape mesh to an IFF writer.
 ''' </summary>
 Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
  ' Prepare mesh for export.
  PrepareMeshForExport()

  ' See if it has any data.
  If (Me.PartCount = 0) OrElse (Me.TotalVertexCount = 0) OrElse (Me.TotalIndiceCount = 0) Then _
   Trace.TraceWarning("Engine shape mesh '" & m_Name & "' is empty. Please fix.") _
 : Exit Sub

  ' Write to file.
  IFF.Push("ETSH")

  IFF.Write(m_Name)
  IFF.Write(m_ParentName)

  For I As Integer = 0 To Me.Part(0).PrimitiveGroupCount - 1
   With Me.Part(0).PrimitiveGroups(I)
    Select Case .Type
     Case Direct3D.PrimitiveType.TriangleList
      ' Write each triangle as a triangle fan group.
      For J As Integer = 0 To .IndiceCount - 3 Step 3
       ' Write number of indices in this triangle fan group (3)
       IFF.WriteInt32(3)

       ' Write vertices.
       Me.Part(0).Vertices(.Indice(J)).WriteIFF(IFF)
       Me.Part(0).Vertices(.Indice(J + 1)).WriteIFF(IFF)
       Me.Part(0).Vertices(.Indice(J + 2)).WriteIFF(IFF)

      Next J ' For J As Integer = 0 To .IndiceCount - 3 Step 3

     Case Direct3D.PrimitiveType.TriangleFan
      ' Write the triangle fan primitive group.
      IFF.WriteInt32(.IndiceCount)

      For J As Integer = 0 To .IndiceCount - 1
       Me.Part(0).Vertices(.Indice(J)).WriteIFF(IFF)

      Next J ' For J As Integer = 0 To .IndiceCount - 1

     Case Else
      Trace.Assert(False, "Internal error")

    End Select  ' Select Case .Type
   End With ' With Me.Part(0).PrimitiveGroups(I)
  Next I ' For I As Integer = 0 To Me.Part(0).PrimitiveGroupCount - 1

  IFF.Pop()

 End Sub

 ''' <summary>
 ''' Prepares the engine shape mesh for export.
 ''' </summary>
 Private Sub PrepareMeshForExport()
  ' Merge all parts.
  Me.MergeAll()

  ' See if it has any parts.
  If Me.PartCount = 0 Then _
   Exit Sub

  ' Remove redundant primitive groups.
  For I As Integer = Me.Part(0).PrimitiveGroupCount - 1 To 0 Step -1
   If (Me.Part(0).PrimitiveGroups(I).Type <> Direct3D.PrimitiveType.TriangleList) AndAlso _
      (Me.Part(0).PrimitiveGroups(I).Type <> Direct3D.PrimitiveType.TriangleFan) Then _
    Me.Part(0).RemovePrimitiveGroups(I)

  Next I ' For I As Integer = Me.Part(0).PrimitiveGroupCount - 1 To 0 Step -1

 End Sub

 ''' <summary>
 ''' Initializes the engine shape mesh.
 ''' </summary>
 Private Sub Initialize()
  Me.RemoveAll()

  m_Name = "EngineShape"
  m_ParentName = "Root"
  m_Visible = False

 End Sub

End Class
