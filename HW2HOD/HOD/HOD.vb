''' <summary>
''' Class representing a Homeworld2 HOD file.
''' </summary>
Public NotInheritable Class HOD
 ' --------------
 ' Class Members.
 ' --------------
 ''' <summary>Maya HOD Exporter plugin's version.</summary>
 Private m_Version As Integer

 ''' <summary>String containing the name of mesh format.</summary>
 Private m_Name As String

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
 ''' Class finalizer.
 ''' </summary>
 Protected Overrides Sub Finalize()
  FinalizeRender()

 End Sub

 ' -----------------
 ' Class properties.
 ' -----------------
 ''' <summary>
 ''' Returns\Sets Maya HOD Exporter plugin's version.
 ''' </summary>
 ''' <remarks>
 ''' For non-background files (including light-only HODs), this is 0x200 (reversed).
 ''' For background files (light-only HODs not included in this category) this is 1000 (not reversed).
 ''' This may change the HOD name to mantain consistency.
 ''' </remarks>
 ''' <exception cref="ArgumentException">
 ''' Thrown when a value other than 0x200 or 1000 is set.
 ''' </exception>
 Public Property Version() As Integer
  Get
   Return m_Version

  End Get

  Set(ByVal value As Integer)
   Select Case value
    Case &H200
     If m_Name = "" Then _
      m_Name = Name_MultiMesh
     m_Version = &H200

    Case 1000
     m_Name = ""
     m_Version = 1000

    Case Else
     Throw New ArgumentException("Invalid 'value'.")
     Exit Property

   End Select ' Select Case value

  End Set

 End Property

 ''' <summary>
 ''' Returns\Sets string containing the name of mesh format. Usually one of:
 ''' "Homeworld2 Variable Mesh File" (this is the older format),
 ''' "Homeworld2 Simple Mesh File",
 ''' "Homeworld2 Wireframe Mesh File",
 ''' "Homeworld2 Multi Mesh File",
 ''' "Homeworld2 Basic Mesh File" (this has not been seen in any file).
 ''' </summary>
 ''' <remarks>
 ''' For very old backgrounds, this is "Homeworld2 Variable Mesh File".
 ''' For UI files, this may be either of "Homeworld2 Simple Mesh File" or "Homeworld2 Wireframe Mesh File".
 ''' For ships, subsystems, and other HODs, this is "Homeworld2 Multi Mesh File".
 ''' For background-lighting HODs, this is "Homeworld2 Multi Mesh File".
 ''' For backgrounds, this is not present.
 ''' This may change HOD version to mantain consistency.
 ''' Also, this may change the simple meshes' 'IsWireframe' property. Due to this, a wireframe
 ''' mesh loses it's data when it becomes a simple mesh and vice versa.
 ''' </remarks>
 ''' <exception cref="ArgumentException">
 ''' Thrown when a value other than 0x200 or 1000 is set.
 ''' </exception>
 Public Property Name() As String
  Get
   Return m_Name

  End Get

  Set(ByVal value As String)
   Select Case m_Name
    Case ""
     m_Version = 1000
     m_Name = ""

    Case Name_MultiMesh, Name_VariableMesh, _
         Name_SimpleMesh, Name_WireframeMesh
     m_Version = &H200
     m_Name = value

     ' Set the simple meshes' 'IsWireframe' property.
     If (value = Name_SimpleMesh) OrElse (value = Name_WireframeMesh) Then
      For I As Integer = 0 To m_SimpleMeshes.Count - 1
       m_SimpleMeshes(I).IsWireframe = (value = Name_WireframeMesh)

      Next I ' For I As Integer = 0 To m_SimpleMeshes.Count - 1
     End If ' If (value = Name_SimpleMesh) OrElse (value = Name_WireframeMesh) Then

    Case Else
     Throw New ArgumentException("Invalid 'value'.")
     Exit Property

   End Select ' Select Case m_Name

  End Set

 End Property

 ' -----------------
 ' Member Functions.
 ' -----------------
 ''' <summary>
 ''' Reads a Homeworld2 HOD file from a stream.
 ''' </summary>
 ''' <param name="stream">
 ''' The underlying stream to use.
 ''' </param>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when <c>stream Is Nothing</c>.
 ''' </exception>
 ''' <exception cref="ArgumentException">
 ''' Thrown when <c>stream.CanRead</c> is <c>False</c>.
 ''' </exception>
 Public Sub Read(ByVal stream As IO.Stream)
  Dim IFF As IFF.IFFReader = Nothing

  If stream Is Nothing Then _
   Throw New ArgumentNullException("stream") _
 : Exit Sub

  If Not stream.CanRead Then _
   Throw New ArgumentException("Cannot read from stream.") _
 : Exit Sub

  ' Prepare a new IFF reader.
  IFF = New IFF.IFFReader(stream)

  ' See if stream is valid.
  If IFF Is Nothing Then _
   Exit Sub

  ' Initialize the HOD.
  Initialize()

  ' Add handlers.
  IFF.AddHandler("VERS", Homeworld2.IFF.ChunkType.Form, AddressOf ReadVERSChunk)
  IFF.AddHandler("NAME", Homeworld2.IFF.ChunkType.Form, AddressOf ReadNAMEChunk)
  IFF.AddHandler("BGMS", Homeworld2.IFF.ChunkType.Form, AddressOf ReadBGMSChunk)
  IFF.AddHandler("HVMD", Homeworld2.IFF.ChunkType.Form, AddressOf ReadHVMDChunk)

  IFF.AddHandler("DTRM", Homeworld2.IFF.ChunkType.Form, AddressOf ReadDTRMChunk)
  IFF.AddHandler("BGSG", Homeworld2.IFF.ChunkType.Form, AddressOf ReadBGSGChunk)
  IFF.AddHandler("STRF", Homeworld2.IFF.ChunkType.Normal, AddressOf ReadSTRFChunk, 1000)
  IFF.AddHandler("INFO", Homeworld2.IFF.ChunkType.Form, AddressOf ReadINFOChunk)

  ' Add handlers for vestigial chunks.
  IFF.AddHandler("BGLT", Homeworld2.IFF.ChunkType.Normal, AddressOf ReadVestigialChunk, 0)

  ' Finally, read the file.
  IFF.Parse()

 End Sub

 ''' <summary>
 ''' Writes a Homeworld2 HOD file to a stream.
 ''' </summary>
 ''' <param name="stream">
 ''' The underlying stream to use.
 ''' </param>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when <c>stream Is Nothing</c>.
 ''' </exception>
 ''' <exception cref="ArgumentException">
 ''' Thrown when <c>stream.CanWrite</c> is <c>False</c>.
 ''' </exception>
 Public Sub Write(ByVal stream As IO.Stream)
  If stream Is Nothing Then _
   Throw New ArgumentNullException("stream") _
 : Exit Sub

  If Not stream.CanWrite Then _
   Throw New ArgumentException("Cannot write to stream.") _
 : Exit Sub

  ' Prepare new IFF writer.
  Dim IFF As New IFF.IFFWriter(stream)

  ' Write the VERS chunk.
  IFF.Push("VERS", Homeworld2.IFF.ChunkType.Form)
  IFF.WriteInt32(m_Version)
  IFF.Pop()

  ' Write the NAME chunk, only for version 0x200.
  If m_Version = &H200 Then
   IFF.Push("NAME", Homeworld2.IFF.ChunkType.Form)
   IFF.Write(m_Name, m_Name.Length)
   IFF.Pop()

  End If ' If m_Version = &H200 Then

  ' Write the BGLT, BGMS, BGSG, STRF chunk(s), only for version 1000.
  ' Without the BGLT chunk, HW2 crashes.
  If m_Version = 1000 Then _
   WriteBGLTChunk(IFF) _
 : WriteBGMSChunk(IFF) _
 : WriteBGSGChunk(IFF) _
 : WriteSTRFChunk(IFF) _
 : If m_BackgroundMeshes.Count <> m_StarFields.Count Then _
    Trace.TraceError("The number of star fields does not equal the number of background meshes. Please fix.")

  ' Write the HVMD, DTRM and INFO chunks, only for version 0x200.
  If m_Version = &H200 Then _
   WriteHVMDChunk(IFF) _
 : WriteDTRMChunk(IFF) _
 : WriteINFOChunk(IFF)

 End Sub

 ''' <summary>
 ''' Reads the VERS chunk from an IFF reader.
 ''' </summary>
 ''' <param name="IFF">
 ''' IFF reader to read from.
 ''' </param>
 ''' <param name="ChunkAttributes">
 ''' Attributes of the chunk.
 ''' </param>
 Friend Sub ReadVERSChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
  Version = IFF.ReadInt32()

 End Sub

 ''' <summary>
 ''' Reads the NAME chunk from an IFF reader.
 ''' </summary>
 ''' <param name="IFF">
 ''' IFF reader to read from.
 ''' </param>
 ''' <param name="ChunkAttributes">
 ''' Attributes of the chunk.
 ''' </param>
 Friend Sub ReadNAMEChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
  Name = IFF.ReadString(ChunkAttributes.Size)

 End Sub

 ''' <summary>
 ''' Reads (actually, silently skips) a vestigial chunk.
 ''' </summary>
 Friend Sub ReadVestigialChunk(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
  ' Nothing here.

 End Sub

 ''' <summary>
 ''' Initializes the HOD, by clearing all data, but retaining properties that
 ''' are not written into the HOD (like team\stripe colour, badge).
 ''' </summary>
 Public Sub Initialize()
  m_Version = &H200
  m_Name = Name_MultiMesh

  InitializeBGMS()
  InitializeHVMD()
  InitializeDTRM()
  InitializeINFO()

 End Sub

End Class
