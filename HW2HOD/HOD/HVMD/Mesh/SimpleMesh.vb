Imports GenericMesh

''' <summary>
''' Class representing a Homeworld2 Basic Mesh, used to form ship meshes.
''' </summary>
''' <remarks>
''' Although 
''' </remarks>
Public NotInheritable Class SimpleMesh
    Inherits GenericMesh.GBasicMesh(Of SimpleVertex, Integer, Material.Default)

    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Name of mesh.</summary>
    Private m_Name As String

    ''' <summary>Whether or not this mesh is a wireframe mesh.</summary>
    Private m_IsWireframe As Boolean

    ''' <summary>Whether the mesh is visible or not.</summary>
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
    Public Sub New(ByVal sm As SimpleMesh)
        ' Call base contructor.
        MyBase.New(sm)

        m_Name = sm.m_Name
        m_IsWireframe = sm.m_IsWireframe
        m_Visible = sm.m_Visible
    End Sub

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' Returns\Sets name of mesh.
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
    ''' Returns\Sets whether this mesh is a wireframe mesh or not.
    ''' </summary>
    ''' <remarks>
    ''' Setting this property erases the mesh.
    ''' </remarks>
    Public Property IsWireframe() As Boolean
        Get
            Return m_IsWireframe
        End Get

        Friend Set(ByVal value As Boolean)
            ' Erase mesh if needed.
            If m_IsWireframe <> value Then _
                Me.RemoveAll()

            ' Set field.
            m_IsWireframe = value
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
    ''' Reads a simple mesh from an IFF reader.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF reader to read from.
    ''' </param>
    ''' <param name="Wireframe">
    ''' Whether the mesh is wireframe or not.
    ''' </param>
    Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader, ByVal Wireframe As Boolean)
        ' Initialize mesh first.
        Initialize()

        ' Set wireframe attribute.
        IsWireframe = Wireframe

        ' Read name.
        m_Name = IFF.ReadString()

        ' Initialize part.
        Me.PartCount = 1

        With Me.Part(0)
            ' Read vertex count.
            .Vertices.Count = IFF.ReadInt32()

            ' Read all vertices, and only the specified fields.
            For I As Integer = 0 To .Vertices.Count - 1
                .Vertices(I) = SimpleVertex.ReadIFF(IFF)

            Next I ' For I As Integer = 0 To .Vertices.Count - 1

            ' Initialize primitive group count.
            .PrimitiveGroupCount = 1

            With .PrimitiveGroups(0)
                ' Initialize primitive group type.
                If m_IsWireframe Then _
                    .Type = Direct3D.PrimitiveType.LineList _
                    Else _
                    .Type = Direct3D.PrimitiveType.TriangleList

                ' Read indice count.
                .IndiceCount = IFF.ReadInt32()

                ' Read all indices.
                For I As Integer = 0 To .IndiceCount - 1
                    .Indice(I) = IFF.ReadInt32()

                Next I ' For I As Integer = 0 To .IndiceCount - 1
            End With ' With .PrimitiveGroups(0)

            ' Check if this is a wireframe mesh and has no indices but vertices only.
            If (m_IsWireframe) AndAlso (.PrimitiveGroups(0).IndiceCount = 0) AndAlso (.Vertices.Count > 0) Then
                ' Generate indices.
                Dim ind(.Vertices.Count - 1) As Integer

                ' Assign indices.
                For I As Integer = 0 To UBound(ind)
                    ind(I) = I

                Next I ' For I As Integer = 0 To UBound(ind)

                ' Append indices.
                .PrimitiveGroups(0).Append(ind)

                ' Erase array.
                Erase ind

            End If ' If (m_IsWireframe) AndAlso (.PrimitiveGroups(0).IndiceCount = 0) AndAlso (.Vertices.Count > 0) Then
        End With ' With Me.Part(0)
    End Sub

    ''' <summary>
    ''' Writes the simple mesh to an IFF writer.
    ''' </summary>
    ''' <param name="IFF">
    ''' IFF writer to write to.
    ''' </param>
    Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
        If (Me.PartCount = 0) OrElse
           (Me.TotalVertexCount = 0) OrElse
           (Me.TotalIndiceCount = 0) Then _
            Trace.TraceWarning("Simple mesh will not be exported because it doesn't have any data.") _
                : Exit Sub

        ' Remove unneeded primitives before writing mesh.
        PreWrite()

        ' Write to file. Normal solid meshes are exported in usual way.
        ' But only vertices are exported in wireframe meshes.
        If m_IsWireframe Then _
            WriteWireframe(IFF) _
            Else _
            WriteSolid(IFF)
    End Sub

    ''' <summary>
    ''' Fixes the mesh before writing it.
    ''' </summary>
    Private Sub PreWrite()
        ' Make sure mesh has parts.
        If PartCount = 0 Then _
            Exit Sub

        ' Make sure the mesh is in proper format before writing
        ' to file.
        If PartCount <> 1 Then _
            Me.MergeAll()

        ' Convert to list type.
        Me.Part(0).ConvertToList()

        ' Remove unneeded primitives.
        With Me.Part(0)
            For I As Integer = .PrimitiveGroupCount - 1 To 0 Step - 1
                If ((m_IsWireframe) AndAlso (.PrimitiveGroups(I).Type <> Direct3D.PrimitiveType.LineList)) OrElse
                   ((Not m_IsWireframe) AndAlso (.PrimitiveGroups(I).Type <> Direct3D.PrimitiveType.TriangleList)) Then _
                    Trace.TraceWarning(
                        "Primitive in simple mesh removed because it doesn't have the proper primitive type.") _
                        : .RemovePrimitiveGroups(I)

            Next I ' For I As Integer = .PrimitiveGroupCount - 1 To 0 Step -1

            If .PrimitiveGroupCount = 0 Then _
                .PrimitiveGroupCount = 1

            If m_IsWireframe Then _
                .PrimitiveGroups(0).Type = Direct3D.PrimitiveType.LineList _
                Else _
                .PrimitiveGroups(0).Type = Direct3D.PrimitiveType.TriangleList

        End With ' With Me.Part(0)
    End Sub

    ''' <summary>
    ''' Writes the mesh assuming it to be a solid mesh.
    ''' </summary>
    Private Sub WriteSolid(ByVal IFF As IFF.IFFWriter)
        IFF.Push("SIMP")

        IFF.Write(m_Name)

        With Me.Part(0)
            IFF.WriteInt32(.Vertices.Count)

            For I As Integer = 0 To .Vertices.Count - 1
                .Vertices(I).WriteIFF(IFF)

            Next I ' For I As Integer = 0 To .Vertices.Count - 1

            With .PrimitiveGroups(0)
                ' Write the indice count.
                IFF.WriteInt32(.IndiceCount)

                ' Write all the indices.
                For I As Integer = 0 To .IndiceCount - 1
                    IFF.WriteInt32(.Indice(I))

                Next I ' For I As Integer = 0 To .IndiceCount - 1
            End With ' With .PrimitiveGroups(0)
        End With ' With Me.Part(0)

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Writes the mesh assuming it to be a wireframe mesh.
    ''' </summary>
    Private Sub WriteWireframe(ByVal IFF As IFF.IFFWriter)
        IFF.Push("SIMP")

        ' Do not actually write name, since wireframe meshes have no name.
        IFF.Write("")

        With Me.Part(0)
            ' Write vertex count.
            IFF.WriteInt32(.PrimitiveGroups(0).IndiceCount)

            ' Write all the vertices.
            For I As Integer = 0 To .PrimitiveGroups(0).IndiceCount - 1
                ' Get indice.
                Dim Ind As Integer = .PrimitiveGroups(0).Indice(I)

                ' Get vertex.
                Dim vtx As SimpleVertex

                ' Get the vertex.
                If (Ind < 0) OrElse (Ind >= .Vertices.Count) Then _
                    Trace.TraceWarning("Simple mesh references a non-existant vertex!") _
                        : vtx.Initialize() _
                    Else _
                    vtx = .Vertices(Ind)

                ' Write vertex.
                vtx.WriteIFF(IFF)

            Next I ' For I As Integer = 0 To .PrimitiveGroups(0).IndiceCount - 1

            ' Write the indice count.
            IFF.WriteInt32(0)

            ' Write indices.
            ' Nothing here.

        End With ' With Me.Part(0)

        IFF.Pop()
    End Sub

    ''' <summary>
    ''' Initializes the basic mesh.
    ''' </summary>
    Friend Sub Initialize()
        Me.RemoveAll()

        m_Name = "<no name>"
        m_IsWireframe = False
        m_Visible = True
    End Sub
End Class
