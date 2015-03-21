Imports GenericMesh

''' <summary>
''' Class representing a Homeworld2 Basic Mesh, used to form ship meshes.
''' </summary>
Public NotInheritable Class BasicMesh
 Inherits GenericMesh.GBasicMesh(Of BasicVertex, UShort, Material.Reference)

 ''' <summary>
 ''' Lists the different Homeworld2 primitive types.
 ''' </summary>
 Private Enum PrimitiveType
  Invalid = 0
  Point = &H200   ' may not be actually supported...
  Line            ' may not be actually supported...
  TriangleList
  LineLoop        ' may not be actually supported...
  LineStrip       ' may not be actually supported...
  TriangleFan
  TriangleStrip
  Quad            ' may not be actually supported...
  QuadStrip       ' may not be actually supported...
  Polygon         ' may not be actually supported...

 End Enum

 ' --------------
 ' Class Members.
 ' --------------
 ''' <summary>Level of Detail</summary>
 Private m_LOD As Integer

    ''' <summary>Whether the mesh is visible or not.</summary>
    Private m_Visible As Boolean = True

    Private m_Version As UInteger

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
 Public Sub New(ByVal bm As BasicMesh)
  ' Call base constructor.
  MyBase.New(bm)

  With bm
   m_LOD = .m_LOD
   m_Visible = .m_Visible

   For I As Integer = 0 To PartCount - 1
    Dim m As Material.Reference = Material(I)

    m.VertexMask = .Material(I).VertexMask
    m.Index = .Material(I).Index

    Material(I) = m

   Next I ' For I As Integer = 0 To PartCount - 1
  End With ' With bm

 End Sub

 ' -----------------
 ' Class properties.
 ' -----------------
 ''' <summary>
 ''' Returns\Sets the LOD (level of detail) of this mesh.
 ''' </summary>
 ''' <exception cref="ArgumentOutOfRangeException">
 ''' Thrown when <c>value</c> is negative.
 ''' </exception>
 Friend Property LOD() As Integer
  Get
   Return m_LOD

  End Get

  Set(ByVal value As Integer)
   If (value < 0) Then _
    Throw New ArgumentOutOfRangeException("value") _
  : Exit Property

   m_LOD = value

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
 ''' Reads a basic mesh from an IFF reader.
 ''' </summary>
 ''' <param name="IFF">
 ''' IFF reader to read from.
 ''' </param>
 ''' <param name="ChunkAttributes">
 ''' Chunk attributes.
 ''' </param>
 Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
  ' Initialize mesh first.
  Initialize()

  ' Read LoD.
  m_LOD = IFF.ReadInt32()

        ' Read number of parts and initialize.
        Me.PartCount = IFF.ReadInt32()

        m_Version = ChunkAttributes.Version

        ' Read all parts.
        For I As Integer = 0 To Me.PartCount - 1
   With Me.Part(I)
    ' Read material index.
    .Material.Index = IFF.ReadInt32()

    ' Read vertex mask.
    .Material.VertexMask = CType(IFF.ReadInt32(), VertexMasks)

    ' Read vertex count.
    .Vertices.Count = IFF.ReadInt32()

    ' Read all vertices, and only the specified fields.
    For J As Integer = 0 To .Vertices.Count - 1
                    .Vertices(J) = BasicVertex.ReadIFF(IFF, .Material.VertexMask, m_Version)

                Next J ' For I As Integer = 0 To .Vertices.Count - 1

    ' Read the number of primitive groups.
    .PrimitiveGroupCount = IFF.ReadInt16()

    ' Read all primitive groups.
    For J As Integer = 0 To .PrimitiveGroupCount - 1
     ' Read primitive type.
     Dim Type As PrimitiveType = CType(IFF.ReadInt32(), PrimitiveType)

     Select Case Type
      Case PrimitiveType.TriangleList
       .PrimitiveGroups(J).Type = Direct3D.PrimitiveType.TriangleList

      Case PrimitiveType.TriangleStrip
       .PrimitiveGroups(J).Type = Direct3D.PrimitiveType.TriangleStrip

      Case PrimitiveType.TriangleFan
       .PrimitiveGroups(J).Type = Direct3D.PrimitiveType.TriangleFan

      Case Else
       .PrimitiveGroups(J).Type = Direct3D.PrimitiveType.PointList
       Trace.TraceError("Unknown primitive type: " & Type.ToString())

     End Select ' Select Case Type

     ' Read indice count.
     .PrimitiveGroups(J).IndiceCount = IFF.ReadInt32()

     ' Read all indices.
     For K As Integer = 0 To .PrimitiveGroups(J).IndiceCount - 1
      .PrimitiveGroups(J).Indice(K) = IFF.ReadUInt16()

     Next K ' For K As Integer = 0 To .PrimitiveGroups(J).IndiceCount - 1
    Next J ' For J As Integer = 0 To .PrimitiveGroupCount - 1
   End With ' With Me.Part(I)
  Next I ' For I As Integer = 0 To Me.PartCount - 1

 End Sub

 ''' <summary>
 ''' Writes the basic mesh to an IFF writer.
 ''' </summary>
 ''' <param name="IFF">
 ''' IFF writer to write to.
 ''' </param>
 Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
        IFF.Push("BMSH", Homeworld2.IFF.ChunkType.Normal, m_Version)

        IFF.WriteInt32(m_LOD)
  IFF.WriteInt32(Me.PartCount)

  For I As Integer = 0 To Me.PartCount - 1
   With Me.Part(I)
    IFF.WriteInt32(.Material.Index)
    IFF.WriteInt32(.Material.VertexMask)

    IFF.WriteInt32(.Vertices.Count)

    For J As Integer = 0 To .Vertices.Count - 1
     .Vertices(J).WriteIFF(IFF, .Material.VertexMask)

    Next J ' For J As Integer = 0 To .Vertices.Count - 1

    ' Find out number of triangle primitive groups.
    Dim primGroupCount As Integer = 0

    For J As Integer = 0 To .PrimitiveGroupCount - 1
     Select Case .PrimitiveGroups(J).Type
      Case Direct3D.PrimitiveType.TriangleList, _
           Direct3D.PrimitiveType.TriangleStrip, _
           Direct3D.PrimitiveType.TriangleFan
       primGroupCount += 1

     End Select ' Select Case .PrimitiveGroups(J).Type
    Next J ' For J As Integer = 0 To .PrimitiveGroupCount - 1

    If (primGroupCount <> .PrimitiveGroupCount) Then _
     Trace.TraceWarning("Basic mesh has non-triangle primitive groups!")

    ' Write the triangle primitive group count.
    IFF.WriteUInt16(primGroupCount)

    For J As Integer = 0 To .PrimitiveGroupCount - 1
     ' Write the primitive group type.
     Select Case .PrimitiveGroups(J).Type
      Case Direct3D.PrimitiveType.TriangleList
       IFF.WriteInt32(PrimitiveType.TriangleList)

      Case Direct3D.PrimitiveType.TriangleStrip
       IFF.WriteInt32(PrimitiveType.TriangleStrip)

      Case Direct3D.PrimitiveType.TriangleFan
       IFF.WriteInt32(PrimitiveType.TriangleFan)

      Case Else
       ' Don't write non-triangle primitive groups.
       Continue For

     End Select ' Select Case .PrimitiveGroups(J).Type

     ' Write the indice count.
     IFF.WriteInt32(.PrimitiveGroups(J).IndiceCount)

     ' Write all the indices.
     For K As Integer = 0 To .PrimitiveGroups(J).IndiceCount - 1
      IFF.WriteUInt16(.PrimitiveGroups(J).Indice(K))

     Next K ' For K As Integer = 0 To .PrimitiveGroups(J).IndiceCount - 1
    Next J ' For J As Integer = 0 To .PrimitiveGroupCount - 1
   End With ' With Me.Part(I)
  Next I ' For I As Integer = 0 To Me.PartCount - 1

  IFF.Pop()

 End Sub

 ''' <summary>
 ''' Initializes the basic mesh.
 ''' </summary>
 ''' <remarks></remarks>
 Friend Sub Initialize()
  Me.RemoveAll()

  m_LOD = 0
  m_Visible = True

 End Sub

 ''' <summary>
 ''' Updates the basic mesh so that it renders properly.
 ''' </summary>
 Friend Sub Update(ByVal Materials As IList(Of Material))
  For I As Integer = 0 To PartCount - 1
   Material(I) = Material(I).Update(Materials)

  Next I ' For I As Integer = 0 To PartCount - 1

 End Sub

 ''' <summary>
 ''' Sets the material of all parts of this mesh to the specified
 ''' material.
 ''' </summary>
 Friend Sub SetMaterial(ByVal _Material As Material)
  For I As Integer = 0 To PartCount - 1
   Dim m As Material.Reference = Material(I)
   m.Material = _Material
   Material(I) = m

  Next I ' For I As Integer = 0 To PartCount - 1

 End Sub

 ''' <summary>
 ''' Recolourizes the mesh part using the given texture as input.
 ''' </summary>
 ''' <param name="Texture">
 ''' Texture to use for re-colouring, in A8R8G8B8 format.
 ''' </param>
 ''' <remarks>
 ''' Although this function is for use with background meshes,
 ''' it can be used, in general, with any mesh.
 ''' </remarks>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when <c>Texture Is Nothing</c>.
 ''' </exception>
 ''' <exception cref="ArgumentException">
 ''' Thrown when texture format is not A8R8G8B8.
 ''' </exception>
 Public Sub Recolourize(ByVal Texture As Direct3D.Texture)
  If (Texture Is Nothing) OrElse (Texture.LevelCount <= 0) Then _
   Throw New ArgumentNullException("Texture") _
 : Exit Sub

  Dim w, h As Integer

  ' Get dimensions.
  With Texture.GetLevelDescription(0)
   w = .Width
   h = .Height

   ' Check format.
   If .Format <> Direct3D.Format.A8R8G8B8 Then _
    Throw New ArgumentException("Texture isn't of format 'A8R8G8B8'!") _
  : Exit Sub

  End With ' With Texture.GetLevelDescription(0)

  ' Lock texture.
  Dim g As GraphicsStream = Texture.LockRectangle(0, Direct3D.LockFlags.ReadOnly)
  Dim BR As New IO.BinaryReader(g)

  ' Make array.
  Dim tex(w * h - 1) As Integer, oldPos As Long = g.Position

  ' Read data.
  For Y As Integer = 0 To h - 1
   For X As Integer = 0 To w - 1
    tex(w * Y + X) = BR.ReadInt32()

   Next X ' For X As Integer = 0 To w - 1
  Next Y ' For Y As Integer = 0 To h - 1

  ' Unlock texture.
  g.Position = oldPos
  Texture.UnlockRectangle(0)

  ' Call other function.
  Recolourize(w, h, tex)

  ' Erase array.
  Erase tex

 End Sub

 ''' <summary>
 ''' Recolourizes the mesh part using the given texture as input.
 ''' </summary>
 ''' <param name="Width">
 ''' Width of input texture.
 ''' </param>
 ''' <param name="Height">
 ''' Height of input texture.
 ''' </param>
 ''' <param name="Texture">
 ''' The input texture, in A8R8G8B8 format.
 ''' </param>
 ''' <remarks>
 ''' Although this function is for use with background meshes,
 ''' it can be used, in general, with any mesh.
 ''' </remarks>
 ''' <exception cref="ArgumentOutOfRangeException">
 ''' Thrown when <c>Width</c> or <c>Height</c> &lt; 1
 ''' </exception>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when <c>Texture Is Nothing</c>.
 ''' </exception>
 ''' <exception cref="ArgumentException">
 ''' Thrown when <c>Texture.Length &lt;&gt; Width * Height</c>.
 ''' </exception>
 Public Sub Recolourize(ByVal Width As Integer, ByVal Height As Integer, ByVal Texture() As Int32)
  If (Width < 1) Then _
   Throw New ArgumentOutOfRangeException("Width") _
 : Exit Sub

  If (Height < 1) Then _
   Throw New ArgumentOutOfRangeException("Height") _
 : Exit Sub

  If Texture Is Nothing Then _
   Throw New ArgumentNullException("Texture") _
 : Exit Sub

  If Texture.Length < Width * Height Then _
   Throw New ArgumentException("Texture is of invalid length.") _
 : Exit Sub

  ' Re-colour each part.
  For I As Integer = 0 To PartCount - 1
   With Part(I)
    ' Make the colour field active.
    Dim m As Material.Reference = Material(I)
    m.VertexMask = m.VertexMask Or VertexMasks.Colour
    Material(I) = m

    ' Process each vertex.
    For J As Integer = 0 To .Vertices.Count - 1
     ' Get position.
     Dim P As Vector3 = .Vertices(J).Position

     ' Get the UV co-ordinates from position.
     Dim UV As New Vector2(0.5F * CSng(Math.Atan2(P.Z, P.X) / Math.PI + 1), _
                           0.5F - CSng(Math.Atan2(P.Y, Math.Sqrt(P.X * P.X + P.Z * P.Z)) / Math.PI))

     ' Use existing UVs if available.
     If (m.VertexMask And VertexMasks.Texture0) <> 0 Then _
      UV = .Vertices(J).Tex

     ' Multiply by dimensions.
     UV.X *= (Width - 1)
     UV.Y *= (Height - 1)

     ' Get the pixel co-ordinates.
     Dim X As Integer = CInt(Math.Floor(UV.X)), _
         Y As Integer = CInt(Math.Floor(UV.Y))

     ' Clamp.
     If X > UV.X Then X -= 1
     If Y > UV.Y Then Y -= 1

     ' Clamp.
     X = Math.Min(Width - 1, Math.Max(0, X))
     Y = Math.Min(Height - 1, Math.Max(0, Y))

     ' Now get blend fractions.
     UV.X -= X
     UV.Y -= Y

     ' Get pixels.
     Dim t1, t2, t3, t4 As Integer

     ' Get indexes.
     t1 = Width * Y + X

     If X <> Width - 1 Then _
      t2 = Width * Y + X + 1 _
     Else _
      t2 = t1

     If Y <> Height - 1 Then _
      t3 = Width * (Y + 1) + X _
     Else _
      Y = t1

     If (X <> Width - 1) AndAlso (Y <> Height - 1) Then _
      t4 = Width * (Y + 1) + X + 1 _
     Else If (X = Width - 1) Then _
      t4 = t3 _
     Else _
      t4 = t2

     ' Get colours.
     t1 = Texture(t1)
     t2 = Texture(t2)
     t3 = Texture(t3)
     t4 = Texture(t4)

     ' Make a few assertions...
     Debug.Assert((UV.X >= 0) AndAlso (UV.Y >= 0) AndAlso (UV.X <= 1) AndAlso (UV.Y <= 1))

     ' Blend and store result.
     Dim v As BasicVertex = .Vertices(J)
     v.Diffuse = __blend(t1, t2, t3, t4, UV.X * UV.Y, UV.X * (1.0F - UV.Y), (1.0F - UV.X) * UV.Y, (1.0F - UV.X) * (1.0F - UV.Y))
     .Vertices(J) = v

    Next J ' For J As Integer = 0 To .Vertices.Count - 1
   End With ' With Part(I)
  Next I ' For I As Integer = 0 To PartCount - 1

 End Sub

 ''' <summary>
 ''' Performs byte-wise blend using formula:
 ''' result = saturate(A * xA + B * xB + C * xC + D * xD)
 ''' </summary>
 Private Shared Function __blend(ByVal A As Int32, ByVal B As Int32, ByVal C As Int32, ByVal D As Int32, _
                                 ByVal xA As Single, ByVal xB As Single, ByVal xC As Single, ByVal xD As Single) As Int32

  Dim A1, A2, A3, A4, B1, B2, B3, B4, C1, C2, C3, C4, D1, D2, D3, D4 As Integer

  A1 = A And &HFF : A2 = (A And &HFF00) >> 8 : A3 = (A And &HFF0000) >> 16 : A4 = (A And &HFF000000) >> 24
  B1 = B And &HFF : B2 = (B And &HFF00) >> 8 : B3 = (B And &HFF0000) >> 16 : B4 = (B And &HFF000000) >> 24
  C1 = C And &HFF : C2 = (C And &HFF00) >> 8 : C3 = (C And &HFF0000) >> 16 : C4 = (C And &HFF000000) >> 24
  D1 = D And &HFF : D2 = (D And &HFF00) >> 8 : D3 = (D And &HFF0000) >> 16 : D4 = (D And &HFF000000) >> 24

  Dim R1, R2, R3, R4 As Integer

  R1 = CInt(Math.Min(255, Math.Max(0, xA * A1 + xB * B1 + xC * C1 + xD * D1)))
  R2 = CInt(Math.Min(255, Math.Max(0, xA * A2 + xB * B2 + xC * C2 + xD * D2)))
  R3 = CInt(Math.Min(255, Math.Max(0, xA * A3 + xB * B3 + xC * C3 + xD * D3)))
  R4 = CInt(Math.Min(255, Math.Max(0, xA * A4 + xB * B4 + xC * C4 + xD * D4)))

  Return R1 Or (R2 << 8) Or (R3 << 16) Or (R4 << 24)

 End Function

 ''' <summary>
 ''' Calculates the tangents in the mesh.
 ''' </summary>
 Public Sub CalculateTangents()
  For I As Integer = 0 To Me.PartCount - 1
   ' Calculate tangents.
   _GenerateTangents(Me.Part(I))

   ' Update vertex masks.
   Dim m As Material.Reference = Me.Part(I).Material
   m.VertexMask = m.VertexMask Or VertexMasks.Tangent Or VertexMasks.Binormal
   Me.Part(I).Material = m

  Next I ' For I As Integer = 0 To Me.PartCount - 1

 End Sub

 ' CnlPepper's tangent calculation code:

 Private Sub _GenerateTangents(ByRef Mesh As GMeshPart(Of BasicVertex, UShort, Material.Reference))

  'Dim svec As Vector3, tvec As Vector3, norm As Vector3, nT As Vector3, nB As Vector3
  Dim fT As Vector3, fB As Vector3, fH As Integer
  Dim altv() As Integer
  Dim handedness() As Integer
  'Dim dp As Single, V As Single
  Dim i1 As Integer, i2 As Integer, i3 As Integer, N As Integer
  Dim I As Integer, J As Integer
  Dim tolerance As Single

  ' tangent comparison tolerance
  tolerance = 0.01F

  Debug.Print("Calculating tangent vectors...")

  With Mesh

   'Debug.Print "numPrims="; .numPrims

   ' purge old tangents and bitangents
   For I = 0 To .Vertices.Count - 1
    .Vertices(I) = CType(.Vertices(I).SetTangent(New Vector3(0, 0, 0)), BasicVertex)
    .Vertices(I) = CType(.Vertices(I).SetBinormal(New Vector3(0, 0, 0)), BasicVertex)
   Next I

   ' create and initialise alternate handed vertex map and handedness array
   ReDim altv(.Vertices.Count - 1)
   ReDim handedness(.Vertices.Count - 1)
   For I = 0 To .Vertices.Count - 1
    altv(I) = -1
    handedness(I) = 0
   Next I

   ' 4E534B: Convert to list first...
   .ConvertToList()

   ' calculate tangents
   For I = 0 To .PrimitiveGroupCount - 1

    'Debug.Print "I="; I
    'Debug.Print "numIndices="; .faceData(I).NumIndices / 3

    ' process vertexes for each face
    For J = 0 To .PrimitiveGroups(I).IndiceCount - 3 Step 3

     'Debug.Print "J="; J

     ' assemble vertex information
     i1 = .PrimitiveGroups(I).Indice(J)
     i2 = .PrimitiveGroups(I).Indice(J + 1)
     i3 = .PrimitiveGroups(I).Indice(J + 2)

     'Debug.Print "vindex="; i1; ", "; i2; ", "; i3
     'Debug.Print "v1: "; .vertData(i1).P.X; ", "; .vertData(i1).P.Y; ", "; .vertData(i1).P.Z
     'Debug.Print "v2: "; .vertData(i2).P.X; ", "; .vertData(i2).P.Y; ", "; .vertData(i2).P.Z
     'Debug.Print "v3: "; .vertData(i3).P.X; ", "; .vertData(i3).P.Y; ", "; .vertData(i3).P.Z

     ' calculate face tangents
     _CalcFaceTangents(Mesh, fT, fB, fH, i1, i2, i3)

     ' update the vertex tangents and polygon references
     _UpdateVertTangents(Mesh, altv, handedness, fT, fB, fH, i1, N)
     .PrimitiveGroups(I).Indice(J) = CUShort(N)

     _UpdateVertTangents(Mesh, altv , handedness, fT, fB, fH, i2, N)
     .PrimitiveGroups(I).Indice(J + 1) = CUShort(N)

     _UpdateVertTangents(Mesh, altv, handedness, fT, fB, fH, i3, N)
     .PrimitiveGroups(I).Indice(J + 2) = CUShort(N)

    Next J

   Next I

   ' orthogonalise and normalise tangents
   For I = 0 To .Vertices.Count - 1
    _NormaliseVertTangents(Mesh, handedness(I), I)
   Next I


  End With

 End Sub

 Private Shared Sub _CalcFaceTangents(ByRef Mesh As GMeshPart(Of BasicVertex, UShort, Material.Reference), _
                                      ByRef tangent As Vector3, ByRef bitangent As Vector3, ByRef hand As Integer, _
                                      ByRef i1 As Integer, ByRef i2 As Integer, ByRef i3 As Integer)

  Dim X As Vector3, Y As Vector3, Z As Vector3, S As Vector3, t As Vector3
  Dim norm As Vector3, temp As Vector3, v1 As Vector3, v2 As Vector3, v3 As Vector3
  Dim R As Single, dp As Single

  With Mesh

   X.X = .Vertices(i2).Position.X - .Vertices(i1).Position.X
   X.Y = .Vertices(i3).Position.X - .Vertices(i1).Position.X
   'Debug.Print "x="; X.X; ", "; X.Y

   Y.X = .Vertices(i2).Position.Y - .Vertices(i1).Position.Y
   Y.Y = .Vertices(i3).Position.Y - .Vertices(i1).Position.Y
   'Debug.Print "y="; Y.X; ", "; Y.Y

   Z.X = .Vertices(i2).Position.Z - .Vertices(i1).Position.Z
   Z.Y = .Vertices(i3).Position.Z - .Vertices(i1).Position.Z
   'Debug.Print "z="; Z.X; ", "; Z.Y

   S.X = .Vertices(i2).Tex.X - .Vertices(i1).Tex.X
   S.Y = .Vertices(i3).Tex.X - .Vertices(i1).Tex.X
   'Debug.Print "s="; S.X; ", "; S.Y

   t.X = .Vertices(i2).Tex.Y - .Vertices(i1).Tex.Y
   t.Y = .Vertices(i3).Tex.Y - .Vertices(i1).Tex.Y
   'Debug.Print "t="; t.X; ", "; t.Y

   R = S.X * t.Y - S.Y * t.X
   'Debug.Print "r (pre recip) ="; R

   If R <> 0 Then
    R = 1.0F / R
   Else
    R = 0
   End If
   'Debug.Print "r (post recip) ="; R

   tangent.X = (t.Y * X.X - t.X * X.Y) * R
   tangent.Y = (t.Y * Y.X - t.X * Y.Y) * R
   tangent.Z = (t.Y * Z.X - t.X * Z.Y) * R

   If tangent.X = 0 And tangent.Y = 0 And tangent.Z = 0 Then
    ' no actual texture variation so use vertex vector to help prevent things breaking
    tangent.X = X.X
    tangent.Y = Y.X
    tangent.Z = Z.X
    Debug.Print("Zero tangent found, aligning with vector between first and second vertex")
   End If

   tangent.Normalize()
   'Debug.Print "tangent: "; tangent.X; ", "; tangent.Y; ", "; tangent.Z

   bitangent.X = (S.X * X.Y - S.Y * X.X) * R
   bitangent.Y = (S.X * Y.Y - S.Y * Y.X) * R
   bitangent.Z = (S.X * Z.Y - S.Y * Z.X) * R

   If bitangent.X = 0 And bitangent.Y = 0 And bitangent.Z = 0 Then
    ' no actual texture variation so use vertex vector to help prevent things breaking
    bitangent.X = X.Y
    bitangent.Y = Y.Y
    bitangent.Z = Z.Y
    Debug.Print("Zero bitangent found, aligning with vector between first and third vertex")
   End If

   bitangent.Normalize()
   'Debug.Print "bitangent: "; bitangent.X; ", "; bitangent.Y; ", "; bitangent.Z

   ' calculate face normal
   v1 = .Vertices(i1).Position
   v2 = .Vertices(i2).Position
   v3 = .Vertices(i3).Position
   norm = Vector3.Cross(v2 - v1, v3 - v1)
   norm.Normalize()

   ' calculate face handedness
   temp = Vector3.Cross(norm, tangent)
   dp = Vector3.Dot(temp, bitangent)
   If dp < 0 Then
    hand = -1
   Else
    hand = 1
   End If
   'Debug.Print "handedness ="; hand

  End With

 End Sub


 Private Sub _UpdateVertTangents(ByRef Mesh As GMeshPart(Of BasicVertex, UShort, Material.Reference), _
                                 ByRef altv() As Integer, ByRef handedness() As Integer, ByRef fT As Vector3, _
                                 ByRef fB As Vector3, ByRef fH As Integer, ByRef V As Integer, ByRef N As Integer)

  With Mesh

   If handedness(V) <> 0 And fH <> handedness(V) Then

    ' need to search for or create an alternately handed vertex
    N = altv(V)

    If N = -1 Then

     ' no alternate vertex found, clone current, reset tangents incase they have been set
     .Vertices.Append(New BasicVertex() {.Vertices(V)})
     N = .Vertices.Count - 1

     .Vertices(N) = CType(.Vertices(N).SetTangent(New Vector3(0, 0, 0)), BasicVertex)

     .Vertices(N) = CType(.Vertices(N).SetBinormal(New Vector3(0, 0, 0)), BasicVertex)

     ReDim Preserve handedness(.Vertices.Count - 1)
     handedness(N) = fH

     altv(V) = N

    End If

    ' accumulate
    .Vertices(N) = CType(.Vertices(N).SetTangent(.Vertices(N).Tangent + fT), BasicVertex)
    .Vertices(N) = CType(.Vertices(N).SetBinormal(.Vertices(N).Binormal + fB), BasicVertex)

   Else

    ' same handedness or no handedness set so accumulate tangents and confirm handedness
    .Vertices(N) = CType(.Vertices(N).SetTangent(.Vertices(N).Tangent + fT), BasicVertex)
    .Vertices(N) = CType(.Vertices(N).SetBinormal(.Vertices(N).Binormal + fB), BasicVertex)
    handedness(V) = fH

    ' return vertex index
    N = V

   End If

  End With

 End Sub

 Private Shared Sub _NormaliseVertTangents(ByRef Mesh As GMeshPart(Of BasicVertex, UShort, Material.Reference), _
                                           ByRef handedness As Integer, ByRef I As Integer)

  Dim dp As Single
  Dim norm As Vector3
  Dim nTangent As Vector3
  Dim nBitangent As Vector3
  Dim temp As Vector3


  With Mesh.Vertices(I)

   ' normalise (average) accumulated tangents
   nTangent = Vector3.Normalize(.Tangent)
   nBitangent = Vector3.Normalize(.Binormal)

   ' Gram-Schmidt orthogonalize
   norm.X = .Normal.X
   norm.Y = .Normal.Y
   norm.Z = .Normal.Z
   norm.Normalize()    'Paranoia!
   'Debug.Print "normal: "; norm.X; ", "; norm.Y; ", "; norm.Z

   dp = Vector3.Dot(norm, nTangent)
   If dp = 1 Or dp = -1 Then Debug.Print("Vertex" & CStr(I) & ": Tangent generation failed, normal and tangent parallel.")
   temp = norm * dp
   temp = nTangent - temp
   Mesh.Vertices(I) = CType(.SetTangent(Vector3.Normalize(temp)), BasicVertex)

   'Debug.Print "tangent: "; .tangent.X; ", "; .tangent.Y; ", "; .tangent.Z

   ' Calculate Bitangent
   'dp = D3DXVec3Dot(norm, nBitangent)
   'If dp = 1 Or dp = -1 Then Debug.Print "Vertex"; I; ": Bitangent generation failed, normal and bitangent parallel."
   'D3DXVec3Scale temp, norm, dp
   'D3DXVec3Subtract temp, nBitangent, temp
   'D3DXVec3Normalize .Binorm, temp
   Mesh.Vertices(I) = CType(.SetBinormal(Vector3.Cross(norm, .Tangent)), BasicVertex)
   'Debug.Print "bitangent: "; .Binorm.X; ", "; Binorm.Y; ", "; Binorm.Z

   ' Flip Bitangent according to face handedness
   'If handedness < 0 Then
   '    .Binorm.X = -.Binorm.X
   '    .Binorm.Y = -.Binorm.Y
   '    .Binorm.Z = -.Binorm.Z
   'End If

   ' Flip Bitangent according to local handedness
   temp = Vector3.Cross(norm, nTangent)
   dp = Vector3.Dot(temp, nBitangent)
   If dp < 0 Then _
    Mesh.Vertices(I) = CType(.SetBinormal(-.Binormal), BasicVertex)


  End With

 End Sub

End Class
