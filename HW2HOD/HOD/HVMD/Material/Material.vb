''' <summary>
''' Class representing a Homeworld2 material.
''' </summary>
Public NotInheritable Class Material
 ' --------------
 ' Class Members.
 ' --------------
 ''' <summary>Name of the material.</summary>
 Private m_MaterialName As String

 ''' <summary>Name of the shader used by this material.</summary>
 Private m_ShaderName As String

 ''' <summary>List of parameters of this material.</summary>
 Private m_Parameters As New EventList(Of Parameter)

 ''' <summary>Badge texture to use.</summary>
 Private Shared m_BadgeTexture As Direct3D.Texture

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
 Public Sub New(ByVal m As Material)
  m_MaterialName = m.m_MaterialName
  m_ShaderName = m.m_ShaderName

  For I As Integer = 0 To m.m_Parameters.Count - 1
   m_Parameters.Add(New Parameter(m.m_Parameters(I)))

  Next I ' For I As Integer = 0 To m.m_Parameters.Count - 1

 End Sub

 ' -----------------
 ' Class properties.
 ' -----------------
 ''' <summary>
 ''' Returns\Sets the name of this material.
 ''' </summary>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when <c>value is Nothing</c>.
 ''' </exception>
 Public Property MaterialName() As String
  Get
   Return m_MaterialName

  End Get

  Set(ByVal value As String)
   If (value Is Nothing) OrElse (value = "") Then _
    Throw New ArgumentNullException("value") _
  : Exit Property

   m_MaterialName = value

  End Set

 End Property

 ''' <summary>
 ''' Returns\Sets the name of this material's shader.
 ''' </summary>
 ''' <remarks>
 ''' There should only be the filename of the shader, without
 ''' extension and without it's path.
 ''' </remarks>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when <c>value is Nothing</c>.
 ''' </exception>
 Public Property ShaderName() As String
  Get
   Return m_ShaderName

  End Get

  Set(ByVal value As String)
   If (value Is Nothing) OrElse (value = "") Then _
    Throw New ArgumentNullException("value") _
  : Exit Property

   m_ShaderName = value

  End Set

 End Property

 ''' <summary>
 ''' Returns the list of parameters of this material.
 ''' </summary>
 Public ReadOnly Property Parameters() As IList(Of Parameter)
  Get
   Return m_Parameters

  End Get

 End Property

 ''' <summary>
 ''' Returns\Sets the "current" badge texture.
 ''' </summary>
 Friend Shared Property BadgeTexture() As Direct3D.Texture
  Get
   Return m_BadgeTexture

  End Get

  Set(ByVal value As Direct3D.Texture)
   m_BadgeTexture = value

  End Set

 End Property

 ' -----------------
 ' Member Functions.
 ' -----------------
 ''' <summary>
 ''' Returns the name of this material.
 ''' </summary>
 Public Overrides Function ToString() As String
  Return m_MaterialName

 End Function

 ''' <summary>
 ''' Sets the material parameters by parsing a Homeworld2 Shader
 ''' (.st) file.
 ''' </summary>
 ''' <param name="Filename">
 ''' Name of file.
 ''' </param>
 ''' <remarks>
 ''' This will overwrite any existing parameters. If the read fails,
 ''' it is possible that existing parameters are purged.
 ''' </remarks>
 ''' <exception cref="ArgumentNullException">
 ''' Thrown when <c>Filename Is Nothing</c>.
 ''' </exception>
 Public Sub ParseShader(ByVal Filename As String)
  Dim TS As IO.StreamReader = Nothing

  If (Filename Is Nothing) OrElse (Filename = "") Then _
   Throw New ArgumentNullException("Filename") _
 : Exit Sub

  ' Try to open the file.
  Try
   TS = New IO.StreamReader(Filename)

  Catch ex As Exception
   Trace.TraceError("Error while trying to open file: " & vbCrLf & ex.ToString())

  End Try

  ' Check if an error occured. In that case, do not continue.
  If TS Is Nothing Then _
   Exit Sub

  ' Set the shader name.
  m_ShaderName = IO.Path.GetFileNameWithoutExtension(Filename)

  ' Remove existing parameters.
  m_Parameters.Clear()

  ' Read till end of file.
  While Not TS.EndOfStream
   ' Read this line.
   Dim currLine As String = TS.ReadLine()

   ' Replace all tabs with spaces.
   currLine = currLine.Replace(vbTab, " ")

   ' Remove all double spaces.
   While InStr(currLine, "  ") <> 0
    currLine = currLine.Replace("  ", " ")

   End While ' While InStr(currLine, "  ") <> 0

   ' Trim whitespace from from both ends.
   currLine = currLine.Trim()

   ' See if there is something left...
   If currLine.Length = 0 Then _
    Continue While

   ' Now split into various parameters.
   Dim tokens() As String = Split(currLine)

   ' Check length.
   If tokens.Length = 3 Then
    If (LCase(tokens(0)) = "static") OrElse (LCase(tokens(0)) = "static_opt") Then
     Select Case LCase(tokens(1))
      Case "colour"
       m_Parameters.Add(New Parameter() With { _
                         .Name = tokens(2), _
                         .Type = Parameter.ParameterType.Colour, _
                         .Colour = New Vector4(1, 1, 1, 1) _
                        })

      Case "texture"
       m_Parameters.Add(New Parameter() With { _
                         .Name = tokens(2), _
                         .Type = Parameter.ParameterType.Texture, _
                         .TextureIndex = -1 _
                        })

      Case Else
       ' Something erroneous.
       Trace.TraceError("Shader refers an unknown parameter type: " & tokens(1))

       ' Do nothing.
       Continue While

     End Select ' Select Case LCase(tokens(1))
    End If ' If (LCase(tokens(0)) = "static") OrElse (LCase(tokens(0)) = "static_opt") Then
   End If ' If tokens.Length = 3 Then
  End While ' While Not TS.EndOfStream

 End Sub

 ''' <summary>
 ''' Reads a material from an IFF reader.
 ''' </summary>
 ''' <param name="IFF">
 ''' IFF reader to read from.
 ''' </param>
 ''' <param name="ChunkAttributes">
 ''' Chunk attributes.
 ''' </param>
 Friend Sub ReadIFF(ByVal IFF As IFF.IFFReader, ByVal ChunkAttributes As IFF.ChunkAttributes)
  ' Remove existing data.
  Initialize()

  ' Read material name.
  m_MaterialName = IFF.ReadString()

  ' Read shader name.
  m_ShaderName = IFF.ReadString()

  ' Read parameter count.
  Dim parameterCount As Integer = IFF.ReadInt32()

  ' Read all parameters.
  For I As Integer = 1 To parameterCount
   Dim parameter As New Parameter

   ' Check for old version. Don't read names and assign a name
   ' if it is an old STAT chunk.
   If ChunkAttributes.Type = Homeworld2.IFF.ChunkType.Default Then _
    parameter.ReadIFF(IFF, False) _
  : parameter.Name = parameter.Type.ToString() & I.ToString() _
   Else _
    parameter.ReadIFF(IFF)

   m_Parameters.Add(parameter)

  Next I ' For I As Integer = 1 To parameterCount

 End Sub

 ''' <summary>
 ''' Writes the material to an IFF writer.
 ''' </summary>
 ''' <param name="IFF">
 ''' IFF writer to write to.
 ''' </param>
 Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter)
  IFF.Push("STAT", Homeworld2.IFF.ChunkType.Normal, 1001)

  ' Write material name.
  IFF.Write(MaterialName)

  ' Write shader name.
  IFF.Write(m_ShaderName)

  ' Write parameter count.
  IFF.WriteInt32(m_Parameters.Count)

  For I As Integer = 0 To m_Parameters.Count - 1
   ' Write the parameter.
   m_Parameters(I).WriteIFF(IFF)

  Next I ' For I As Integer = 0 To m_Parameters.Count - 1

  IFF.Pop()

 End Sub

 ''' <summary>
 ''' Initializes the material.
 ''' </summary>
 Private Sub Initialize()
  m_MaterialName = "hw2Shader"
  m_ShaderName = "default"
  m_Parameters.Clear()

 End Sub

 ''' <summary>
 ''' Updates the texture so that it may be used for rendering.
 ''' </summary>
 Friend Sub Update(ByVal Textures As IList(Of Texture))
  For I As Integer = 0 To m_Parameters.Count - 1
   m_Parameters(I) = m_Parameters(I).Update(Textures)

  Next I ' For I As Integer = 0 To m_Parameters.Count - 1

 End Sub

End Class
