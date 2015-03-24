''' <summary>
''' Class for managing all available Homeworld2 shader translations.
''' </summary>
Public NotInheritable Class ShaderLibrary
    ''' <summary>List of Homeworld2 vertex shaders.</summary>
    Private Shared m_VertexShaders As Dictionary(Of String, Direct3D.VertexShader)

    ''' <summary>List of Homeworld2 pixel shaders.</summary>
    Private Shared m_PixelShaders As Dictionary(Of String, Direct3D.PixelShader)

    ''' <summary>List of Homeworld2 vertex shader overrides.</summary>
    Private Shared m_VertexShadersOverride As Dictionary(Of String, Direct3D.VertexShader)

    ''' <summary>List of Homeworld2 pixel shader overrides.</summary>
    Private Shared m_PixelShadersOverride As Dictionary(Of String, Direct3D.PixelShader)

    ''' <summary>
    ''' Class constructor.
    ''' </summary>
    Shared Sub New()
        m_VertexShaders = New Dictionary(Of String, Direct3D.VertexShader)
        m_PixelShaders = New Dictionary(Of String, Direct3D.PixelShader)
        m_VertexShadersOverride = New Dictionary(Of String, Direct3D.VertexShader)
        m_PixelShadersOverride = New Dictionary(Of String, Direct3D.PixelShader)
    End Sub

    ''' <summary>
    ''' Returns whether a shader is HLSL or not.
    ''' </summary>
    Private Shared Function IsHLSL(ByVal shader As String, Optional ByRef profile As String = "") As Boolean
        Const doubleLf As String = vbLf & vbLf

        ' Replace carriage return with line feed.
        shader = shader.Replace(vbCr, vbLf)

        ' Replace double line feeds with single line feed.
        Do While InStr(shader, doubleLf) <> 0
            shader = shader.Replace(doubleLf, vbLf)

        Loop ' Do While InStr(shader, doubleLf) <> 0

        ' Split lines.
        Dim lines() As String = shader.Split(CChar(vbLf))

        ' Read each line.
        For I As Integer = 0 To lines.Length - 1
            Const doubleSpace As String = "  "

            ' Remove tabs.
            Dim line As String = lines(I).Replace(vbTab, " "c)

            ' Trim line.
            line = line.Trim()

            ' Remove double spaces.
            Do While InStr(line, doubleSpace) <> 0
                line = line.Replace(doubleSpace, " "c)

            Loop ' Do While InStr(line, doubleSpace) <> 0

            ' Lower case the line.
            line = line.ToLower()

            ' See if it's a vertex shader or pixel shader assembly.
            If (line = "vs_1_0") OrElse (line = "ps_1_0") OrElse (line = "vs_1_1") OrElse
               (line = "ps_1_1") OrElse (line = "ps_1_2") OrElse (line = "ps_1_3") OrElse
               (line = "ps_1_4") OrElse (line = "vs_2_0") OrElse (line = "ps_2_0") OrElse
               (line = "vs_2_x") OrElse (line = "ps_2_x") OrElse (line = "vs_3_0") OrElse
               (line = "ps_3_0") Then _
                Return False

            ' See if it's tagged to be compiled as a particular shader.
            If (line = "//vs_1_0//") OrElse (line = "//ps_1_0//") OrElse (line = "//vs_1_1//") OrElse
               (line = "//ps_1_1//") OrElse (line = "//ps_1_2//") OrElse (line = "//ps_1_3//") OrElse
               (line = "//ps_1_4//") OrElse (line = "//vs_2_0//") OrElse (line = "//ps_2_0//") OrElse
               (line = "//vs_2_x//") OrElse (line = "//ps_2_x//") OrElse (line = "//vs_3_0//") OrElse
               (line = "//ps_3_0//") Then _
                profile = line.Substring(2, line.Length - 4) _
                    : Return True

        Next I ' For I As Integer = 0 To lines.Length - 1

        ' Assume it to be HLSL.
        Return True
    End Function

    ''' <summary>
    ''' Loads a shader.
    ''' </summary>
    Private Shared Function LoadShader(ByVal name As String, ByVal contents As String, ByVal Device As Direct3D.Device,
                                       ByVal override As Boolean) As Boolean
        ' Get the name.
        Dim shaderName As String = IO.Path.GetFileNameWithoutExtension(name).ToLower()

        ' Get the extension.
        Dim ext As String = IO.Path.GetExtension(name)

        ' Decide whether this is a vertex shader, a pixel shader, or none.
        Dim PS As Boolean

        ' See if it's a pixel shader or a vertex shader, or none.
        If String.Compare(ext, ".ps") = 0 Then _
            PS = True _
            Else If String.Compare(ext, ".vs") = 0 Then _
            PS = False _
            Else _
            Trace.TraceWarning("Unknown file: '" & name & "', ignoring.") _
                : Return False

        ' String to store error if shader compile\assemble goes wrong.
        Dim errString As String = "", profile As String = ""
        Dim graphicsStream As GraphicsStream = Nothing

        ' Try to compile\assemble the shader.
        Try
            ' See if it's HLSL.
            If IsHLSL(contents, profile) Then
                ' Decide a profile if it was not detected.
                If (profile = "") AndAlso (PS) Then _
                    profile = Direct3D.ShaderLoader.GetPixelShaderProfile(Device) _
                    Else If profile = "" Then _
                    profile = Direct3D.ShaderLoader.GetVertexShaderProfile(Device)

                ' Try to compile the shader.
                graphicsStream = Direct3D.ShaderLoader.CompileShader(contents, "main", Nothing, Nothing, profile,
                                                                     Direct3D.ShaderFlags.None, errString, Nothing)

            Else ' If IsHLSL(contents, profile) Then
                ' Try to assemble the shader.
                graphicsStream = Direct3D.ShaderLoader.FromString(contents, Nothing, Direct3D.ShaderFlags.None,
                                                                  errString)

            End If ' If IsHLSL(contents, profile) Then

        Catch ex As Exception
            If graphicsStream IsNot Nothing Then _
                graphicsStream.Dispose()

            If errString <> "" Then _
                Trace.TraceError("Error while compiling\assembling shader: " & vbCrLf & errString) _
                    : Return False

            Trace.TraceError("Error while compiling\assembling shader: " & vbCrLf & ex.ToString())
            Return False

        End Try

        ' Print warnings\errors.
        If errString <> "" Then _
            Trace.TraceError("Error while compiling\assembling shader: " & vbCrLf & errString)

        ' If the shader doesn't compile, don't add it.
        If graphicsStream Is Nothing Then _
            Trace.TraceError("Error while compiling\assembling shader.") _
                : Return False

        ' Check shader version. If not available, then don't add it.
        If Direct3D.ShaderLoader.GetShaderVersion(graphicsStream) >
           Direct3D.Manager.GetDeviceCaps(0, Direct3D.DeviceType.Hardware).PixelShaderVersion Then _
            Trace.TraceWarning("Shader '" & shaderName & "' is not supported on current hardware.") _
                : Return False

        If PS Then
            ' If the preceeding tests don't fail, assemble the shader.
            Dim p As Direct3D.PixelShader = New Direct3D.PixelShader(Device, graphicsStream)

            ' Add to list.
            If override Then _
                m_PixelShadersOverride.Add(shaderName, p) _
                Else _
                m_PixelShaders.Add(shaderName, p)

        Else ' If PS Then
            ' If the preceeding tests don't fail, assemble the shader.
            Dim v As Direct3D.VertexShader = New Direct3D.VertexShader(Device, graphicsStream)

            ' Add to list.
            If override Then _
                m_VertexShadersOverride.Add(shaderName, v) _
                Else _
                m_VertexShaders.Add(shaderName, v)

        End If ' If PS Then

        Return True
    End Function

    ''' <summary>
    ''' Loads an override shader, that, if loaded, will be used instead of the
    ''' stock shader. This would allow usage of custom shaders.
    ''' </summary>
    ''' <param name="name">
    ''' Name of the shader. Can be the full path, or the filename only. But it must have
    ''' either a ".ps" extension if it's a pixel shader, or ".vs" extension if it's a
    ''' vertex shader.
    ''' </param>
    ''' <param name="contents">
    ''' Contents of shader.
    ''' </param>
    ''' <param name="Device">
    ''' Device with which it will be used.
    ''' </param>
    Public Shared Function LoadShader(ByVal name As String, ByVal contents As String, ByVal Device As Direct3D.Device) _
        As Boolean
        Return LoadShader(name, contents, Device, True)
    End Function

    ''' <summary>
    ''' Loads all shaders from the HW2HOD DLL.
    ''' </summary>
    ''' <param name="Device">
    ''' Direct3D device to use.
    ''' </param>
    ''' <exception cref="ArgumentNullException">
    ''' Thrown when <c>Device Is Nothing</c>.
    ''' </exception>
    Public Shared Sub LoadShaders(ByVal Device As Direct3D.Device)
        If Device Is Nothing Then _
            Throw New ArgumentNullException("Device") _
                : Exit Sub

        ' Dispose existing shaders.
        DisposeShaders()

        With Reflection.Assembly.GetExecutingAssembly()
            ' Get all shaders.
            Dim names() As String = .GetManifestResourceNames()

            ' Process all shaders.
            For Each s As String In names
                ' Get the shader name.
                Dim tokens() As String = s.Split("."c)
                Dim shaderName As String = tokens(tokens.Length - 2) & "." & tokens(tokens.Length - 1)

                ' Get the stream and prepare a reader.
                Dim resStream As IO.Stream = .GetManifestResourceStream(s)
                Dim TR As New IO.StreamReader(resStream)

                ' Read the shader.
                Dim shaderText As String = TR.ReadToEnd()

                ' Dispose stream and reader.
                TR.Dispose()
                resStream.Dispose()

                ' Add to list.
                LoadShader(shaderName, shaderText, Device, False)

            Next s ' For Each s As String In names
        End With ' With Reflection.Assembly.GetExecutingAssembly()
    End Sub

    ''' <summary>
    ''' Disposes all loaded shaders.
    ''' </summary>
    Public Shared Sub DisposeShaders()
        For Each p As Direct3D.PixelShader In m_PixelShadersOverride.Values
            p.Dispose()

        Next p ' For Each p As Direct3D.PixelShader In m_PixelShadersOverride.Values

        For Each v As Direct3D.VertexShader In m_VertexShadersOverride.Values
            v.Dispose()

        Next v ' For Each v As Direct3D.VertexShader In m_VertexShadersOverride.Values

        For Each p As Direct3D.PixelShader In m_PixelShaders.Values
            p.Dispose()

        Next p ' For Each p As Direct3D.PixelShader In m_PixelShaders.Values

        For Each v As Direct3D.VertexShader In m_VertexShaders.Values
            v.Dispose()

        Next v ' For Each v As Direct3D.VertexShader In m_VertexShaders.Values

        m_PixelShadersOverride.Clear()
        m_VertexShadersOverride.Clear()
        m_PixelShaders.Clear()
        m_VertexShaders.Clear()
    End Sub

    ''' <summary>
    ''' Retrieves the specified vertex shader.
    ''' </summary>
    ''' <param name="Name">
    ''' Name of shader to retrieve.
    ''' </param>
    Friend Shared ReadOnly Property VertexShader(ByVal Name As String) As Direct3D.VertexShader
        Get
            Dim key As String = Name.ToLower()

            If m_VertexShadersOverride.ContainsKey(key) Then _
                Return m_VertexShadersOverride.Item(key)

            If m_VertexShaders.ContainsKey(key) Then _
                Return m_VertexShaders.Item(key)

            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' Retrieves the specified pixel shader.
    ''' </summary>
    ''' <param name="Name">
    ''' Name of shader to retrieve.
    ''' </param>
    Friend Shared ReadOnly Property PixelShader(ByVal Name As String) As Direct3D.PixelShader
        Get
            Dim key As String = Name.ToLower()

            If m_PixelShadersOverride.ContainsKey(key) Then _
                Return m_PixelShadersOverride.Item(key)

            If m_PixelShaders.ContainsKey(key) Then _
                Return m_PixelShaders.Item(key)

            Return Nothing
        End Get
    End Property
End Class
