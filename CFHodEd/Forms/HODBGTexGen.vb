Imports Microsoft.DirectX
Imports D3DHelper
Imports Homeworld2.HOD

''' <summary>
''' Form to convert Homeworld2 backgrounds to images and to display them.
''' </summary>
Friend NotInheritable Class HODBGTexGen
    ''' <summary>Whether to offset half mesh or not.</summary>
    Private m_HalfOffset As Boolean

    ''' <summary>Colour of upper part of screen.</summary>
    Private m_ColourTop As Integer

    ''' <summary>Colour of lower part of screen.</summary>
    Private m_ColourBottom As Integer

    ''' <summary>Back buffer width.</summary>
    Private m_BackBufferWidth As Integer

    ''' <summary>Back buffer height.</summary>
    Private m_BackBufferHeight As Integer

    ''' <summary>D3D Manager.</summary>
    Private WithEvents m_D3DManager As New D3DManager

    ''' <summary>Camera.</summary>
    Private m_Camera As Camera.UserCamera

    ''' <summary>Meshes.</summary>
    Private m_Meshes(- 1) As BasicMesh

    ''' <summary>
    ''' Class contructor.
    ''' </summary>
    Public Sub New(ByVal HOD As HOD,
                   Optional ByVal doubleTone As Boolean = True,
                   Optional ByVal halfOffset As Boolean = True,
                   Optional ByVal multiplier As Single = 1.0F)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Check multiplier.
        If (multiplier <= 0.0F) OrElse (multiplier > 2.0F) Then _
            multiplier = 1.0F

        ' Store settings.
        m_BackBufferWidth = CInt(2048*multiplier)
        m_BackBufferHeight = CInt(1024*multiplier)

        ' Unwrap mesh.
        m_Meshes = GenerateBGMSTexture(HOD)

        ' Double tone if needed.
        If doubleTone Then
            Dim col As Direct3D.ColorValue

            ' Double the generated mesh's colour.
            For I As Integer = 0 To m_Meshes.Length - 1
                For J As Integer = 0 To m_Meshes(I).PartCount - 1
                    For K As Integer = 0 To m_Meshes(I).Part(J).Vertices.Count - 1
                        ' Get the vertex.
                        Dim v As BasicVertex = m_Meshes(I).Part(J).Vertices(K)

                        ' Get the colour.
                        col = Direct3D.ColorValue.FromArgb(v.Diffuse)

                        ' Double the colour.
                        col.Red *= 2
                        col.Green *= 2
                        col.Blue *= 2
                        col.Alpha *= 2

                        ' Set the colour.
                        v.Diffuse = col.ToArgb()

                        ' Set the vertex.
                        m_Meshes(I).Part(J).Vertices(K) = v

                    Next K ' For K As Integer = 0 To m_Meshes(I).Part(J).Vertices.Count - 1
                Next J ' For J As Integer = 0 To m_Meshes(I).PartCount - 1
            Next I ' For I As Integer = 0 To m_Meshes.Length - 1

            ' Also double tone the copied colours.
            ' Top colour.
            col = Direct3D.ColorValue.FromArgb(m_ColourTop)

            ' Double the colour.
            col.Red *= 2
            col.Green *= 2
            col.Blue *= 2
            col.Alpha *= 2

            ' Set the colour.
            m_ColourTop = col.ToArgb()

            ' Bottom colour.
            col = Direct3D.ColorValue.FromArgb(m_ColourBottom)

            ' Double the colour.
            col.Red *= 2
            col.Green *= 2
            col.Blue *= 2
            col.Alpha *= 2

            ' Set the colour.
            m_ColourBottom = col.ToArgb()

        End If ' If doubleTone Then

        ' Set half offset flag.
        m_HalfOffset = halfOffset
    End Sub

    ''' <summary>
    ''' Generates a series of meshes by unwrapping the background mesh, which can be
    ''' horizontally tiled so as to get the image from which the background was created.
    ''' </summary>
    ''' <remarks>
    ''' The resultant meshes may have to be rendered thrice --
    ''' first with a translation of (-2, 0, 0), 
    ''' then without any tranform, 
    ''' then with a translation of (2, 0, 0) 
    ''' to achieve a reasonable output.
    ''' Also, another requirement would be to have two rectangles --
    ''' one with vertices (-10, 10, -1), (10, 10, -1), (-10, 0, -1), (10, 0, -1) and 
    ''' other with vertices (-10, -10, -1), (10, -10, -1), (-10, 0, -1), (10, 0, -1)
    ''' having colour of the top and bottom of background to get a usable image.
    ''' </remarks>
    Private Function GenerateBGMSTexture(ByVal HOD As HOD) As BasicMesh()
        Dim out(- 1) As BasicMesh
        Dim highestY As Single = - 1.0E+30F,
            lowestY As Single = 1.0E+30F

        ' If no meshes, then return empty array.
        If HOD.BackgroundMeshes.Count = 0 Then _
            Return out

        ' Resize mesh array.
        ReDim out(HOD.BackgroundMeshes.Count - 1)

        ' Copy all meshes.
        For I As Integer = 0 To HOD.BackgroundMeshes.Count - 1
            ' Copy mesh.
            out(I) = New BasicMesh(HOD.BackgroundMeshes(I))

            ' Merge all groups.
            out(I).MergeAll()

            ' See if there are any parts...
            If out(I).PartCount = 0 Then _
                Continue For

            ' Convert to triangle list.
            out(I).Part(0).ConvertToList()

            ' See if it has any parts.
            If out(I).PartCount = 0 Then _
                Continue For

            ' Unwrap vertices and store in UV co-ordinates.
            With out(I).Part(0)
                For J As Integer = 0 To .Vertices.Count - 1
                    ' Get the vertex and position.
                    Dim V As BasicVertex = .Vertices(J)
                    Dim P As Vector3 = V.Position

                    ' Unwrap.
                    V.Tex.X = 0.5F*CSng(Math.Atan2(P.Z, P.X)/Math.PI + 1)
                    V.Tex.Y = 0.5F + CSng(Math.Atan2(P.Y, Math.Sqrt(P.X*P.X + P.Z*P.Z))/Math.PI)

                    ' Copy back.
                    .Vertices(J) = V

                    ' See if this is the highest vertex. If so, set the new highest,
                    ' and copy colour.
                    If P.Y > highestY Then _
                        highestY = P.Y _
                            : m_ColourTop = V.Diffuse

                    ' See if this is the lowest vertex. If so, set the new lowest,
                    ' and copy colour.
                    If P.Y < lowestY Then _
                        lowestY = P.Y _
                            : m_ColourBottom = V.Diffuse

                Next J ' For J As Integer = 0 To .Vertices.Count - 1

                ' Process all triangles.
                For J As Integer = 0 To .PrimitiveGroupCount - 1
                    ' If not a triangle list, continue.
                    If .PrimitiveGroups(J).Type <> Direct3D.PrimitiveType.TriangleList Then _
                        Continue For

                    ' Get all indice triplets.
                    For K As Integer = 0 To .PrimitiveGroups(J).IndiceCount - 3 Step 3
                        ' Get triangle indices.
                        Dim ind1 As Integer = .PrimitiveGroups(J).Indice(K),
                            ind2 As Integer = .PrimitiveGroups(J).Indice(K + 1),
                            ind3 As Integer = .PrimitiveGroups(J).Indice(K + 2)

                        ' Get triangle vertices.
                        Dim V1 As BasicVertex = .Vertices(ind1),
                            V2 As BasicVertex = .Vertices(ind2),
                            V3 As BasicVertex = .Vertices(ind3)

                        ' Get centroid.
                        Dim c As Vector3 = (V1.Position + V2.Position + V3.Position)*(1.0F/3.0F)

                        ' Unwrap centroid.
                        Dim UV As New Vector2(0.5F*CSng(Math.Atan2(c.Z, c.X)/Math.PI + 1),
                                              0.5F + CSng(Math.Atan2(c.Y, Math.Sqrt(c.X*c.X + c.Z*c.Z))/Math.PI))

                        ' Check for special cases of centroid.
                        If UV.X < 0.25 Then
                            ' First quadrant. Check for any vertices in fourth quadrant.
                            If (V1.Tex.X > 0.5) OrElse (V2.Tex.X > 0.5) OrElse (V3.Tex.X > 0.5) Then
                                ' Make a copy of vertices.
                                Dim V4 As BasicVertex = V1,
                                    V5 As BasicVertex = V2,
                                    V6 As BasicVertex = V3

                                ' If a vertex is in fourth quadrant, move to first. But do not
                                ' dirturb original vertex, since it may be shared. So add a new one
                                ' and make the indice refer to it.
                                If V1.Tex.X > 0.5 Then _
                                    V1.Tex.X -= 1 _
                                        : .PrimitiveGroups(J).Indice(K) = CUShort(.Vertices.Count) _
                                        : .Vertices.Append(New BasicVertex() {V1})

                                ' ...
                                If V2.Tex.X > 0.5 Then _
                                    V2.Tex.X -= 1 _
                                        : .PrimitiveGroups(J).Indice(K + 1) = CUShort(.Vertices.Count) _
                                        : .Vertices.Append(New BasicVertex() {V2})

                                ' ...
                                If V3.Tex.X > 0.5 Then _
                                    V3.Tex.X -= 1 _
                                        : .PrimitiveGroups(J).Indice(K + 2) = CUShort(.Vertices.Count) _
                                        : .Vertices.Append(New BasicVertex() {V3})

                                ' If the vertex is in first quadrant, move to fourth. This is done
                                ' to split the triangle into two and get coverage on both sides.
                                If V4.Tex.X < 0.5 Then _
                                    V4.Tex.X += 1

                                ' ...
                                If V5.Tex.X < 0.5 Then _
                                    V5.Tex.X += 1

                                ' ...
                                If V6.Tex.X < 0.5 Then _
                                    V6.Tex.X += 1

                                ' Get starting indice.
                                Dim vcount As Integer = .Vertices.Count

                                ' Now add the second triangle's vertices and indices.
                                .Vertices.Append(New BasicVertex() {V4, V5, V6})
                                .PrimitiveGroups(J).Append(New Integer() {vcount, vcount + 1, vcount + 2})

                            End If ' If (V1.Tex.X > 0.5) OrElse (V2.Tex.X > 0.5) OrElse (V3.Tex.X > 0.5) Then

                        ElseIf UV.X > 0.75 Then
                            ' Fourth quadrant. Check for any vertices in first quadrant.
                            If (V1.Tex.X < 0.5) OrElse (V2.Tex.X < 0.5) OrElse (V3.Tex.X < 0.5) Then
                                ' Make a copy of vertices.
                                Dim V4 As BasicVertex = V1,
                                    V5 As BasicVertex = V2,
                                    V6 As BasicVertex = V3

                                ' If a vertex is in first quadrant, move to fourth. But do not
                                ' dirturb original vertex, since it may be shared. So add a new one
                                ' and make the indice refer to it.
                                If V1.Tex.X < 0.5 Then _
                                    V1.Tex.X += 1 _
                                        : .PrimitiveGroups(J).Indice(K) = CUShort(.Vertices.Count) _
                                        : .Vertices.Append(New BasicVertex() {V1})

                                ' ...
                                If V2.Tex.X < 0.5 Then _
                                    V2.Tex.X += 1 _
                                        : .PrimitiveGroups(J).Indice(K + 1) = CUShort(.Vertices.Count) _
                                        : .Vertices.Append(New BasicVertex() {V2})

                                ' ...
                                If V3.Tex.X < 0.5 Then _
                                    V3.Tex.X += 1 _
                                        : .PrimitiveGroups(J).Indice(K + 2) = CUShort(.Vertices.Count) _
                                        : .Vertices.Append(New BasicVertex() {V3})

                                ' If the vertex is in fourth quadrant, move to first. This is done
                                ' to split the triangle into two and get coverage on both sides.
                                If V4.Tex.X > 0.5 Then _
                                    V4.Tex.X -= 1

                                ' ...
                                If V5.Tex.X > 0.5 Then _
                                    V5.Tex.X -= 1

                                ' ...
                                If V6.Tex.X > 0.5 Then _
                                    V6.Tex.X -= 1

                                ' Get starting indice.
                                Dim vcount As Integer = .Vertices.Count

                                ' Now add the second triangle's vertices and indices.
                                .Vertices.Append(New BasicVertex() {V4, V5, V6})
                                .PrimitiveGroups(J).Append(New Integer() {vcount, vcount + 1, vcount + 2})

                            End If ' If (V1.Tex.X < 0.5) OrElse (V2.Tex.X < 0.5) OrElse (V3.Tex.X < 0.5) Then
                        End If ' The check for special cases, first and fourth quadrant.
                    Next K ' For K As Integer = 0 To .PrimitiveGroups(J).IndiceCount - 3 Step 3
                Next J ' For J As Integer = 0 To .PrimitiveGroupCount

                ' Now, assign UV to position for all vertices.
                For J As Integer = 0 To .Vertices.Count - 1
                    ' Get the vertex.
                    Dim V As BasicVertex = .Vertices(J)

                    ' Bias UV and put into position.
                    V.Position.X = V.Tex.X - 0.5F
                    V.Position.Y = V.Tex.Y - 0.5F
                    V.Position.Z = 0

                    ' Set the vertex.
                    .Vertices(J) = V

                Next J ' For J As Integer = 0 To .Vertices.Count - 1

#If DEBUG Then
    ' Validate mesh to make sure we didn't corrupt any data.
    out(I).Validate()

#End If

            End With ' With out(I).Part(0)
        Next I ' For I As Integer = 0 To HOD.BackgroundMeshes.Count - 1

        ' Return the meshes.
        Return out
    End Function

    Private Sub m_D3DManager_DeviceCreated(ByVal DeviceCreationFlags As D3DHelper.Utility.DeviceCreationFlags) _
        Handles m_D3DManager.DeviceCreated
        ' Resize the back buffers.
        If DeviceCreationFlags.Width <> m_BackBufferWidth Then
            ' Get present parameters.
            Dim PP As Direct3D.PresentParameters = m_D3DManager.Device.PresentationParameters

            ' Set back buffer size.
            PP.BackBufferWidth = m_BackBufferWidth
            PP.BackBufferHeight = m_BackBufferHeight

            ' Reset device.
            m_D3DManager.Device.Reset(PP)

        End If ' If DeviceCreationFlags.Width <> m_BackBufferWidth Then

        ' Enable MSAA if multi sampling is available.
        If DeviceCreationFlags.Multisampling Then _
            m_D3DManager.Device.RenderState.MultiSampleAntiAlias = True

        ' Disable lighting.
        m_D3DManager.Device.RenderState.Ambient = Color.White
        m_D3DManager.Device.RenderState.Lighting = False

        ' Create camera.
        m_Camera = New Camera.UserCamera With { _
            .Device = m_D3DManager.Device, _
            .ProjectionType = Camera.ProjectionType.Orthogonal, _
            .Width = m_BackBufferWidth, _
            .Height = m_BackBufferHeight, _
            .ZNear = 0.01F, _
            .ZFar = 10.0F _
            }
    End Sub

    Private Sub D3DManager_DeviceLost(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles m_D3DManager.DeviceLost
        For I As Integer = 0 To m_Meshes.Length - 1
            m_Meshes(I).Unlock()

        Next I ' For I As Integer = 0 To m_Meshes.Length - 1
    End Sub

    Private Sub D3DManager_DeviceReset(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles m_D3DManager.DeviceReset
        For I As Integer = 0 To m_Meshes.Length - 1
            m_Meshes(I).Lock(m_D3DManager.Device)

        Next I ' For I As Integer = 0 To m_Meshes.Length - 1
    End Sub

    Private Sub D3DManager_Disposing(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_D3DManager.Disposing
        For I As Integer = 0 To m_Meshes.Length - 1
            m_Meshes(I).Unlock()

        Next I ' For I As Integer = 0 To m_Meshes.Length - 1
    End Sub

    Private Sub D3DManager_Render() Handles m_D3DManager.Render
        With m_D3DManager.Device
            ' Present previous scene, clear, and begin scene.
            .Present()
            .Clear(Direct3D.ClearFlags.Target Or Direct3D.ClearFlags.ZBuffer, Color.Black, 1.0, 0)
            .BeginScene()

            ' Update camera, light, FPS display.
            m_Camera.Update()

            ' Render background mesh's background if needed.
            If m_Meshes.Length <> 0 Then
                ' Prepare upper rectangle's vertices.
                Dim upperRect() As Direct3D.CustomVertex.PositionColored = New Direct3D.CustomVertex.PositionColored() { _
                                                                                                                           New _
                        Direct3D.CustomVertex.PositionColored(- 10, 10, - 1, m_ColourTop),
                                                                                                                           New _
                        Direct3D.CustomVertex.PositionColored(- 10, 0, - 1, m_ColourTop),
                                                                                                                           New _
                        Direct3D.CustomVertex.PositionColored(10, 10, - 1, m_ColourTop),
                                                                                                                           New _
                        Direct3D.CustomVertex.PositionColored(10, 0, - 1, m_ColourTop)
                                                                                                                       }

                ' Prepare lower rectangle's vertices.
                Dim lowerRect() As Direct3D.CustomVertex.PositionColored = New Direct3D.CustomVertex.PositionColored() { _
                                                                                                                           New _
                        Direct3D.CustomVertex.PositionColored(- 10, 0, - 1, m_ColourBottom),
                                                                                                                           New _
                        Direct3D.CustomVertex.PositionColored(- 10, - 10, - 1, m_ColourBottom),
                                                                                                                           New _
                        Direct3D.CustomVertex.PositionColored(10, 0, - 1, m_ColourBottom),
                                                                                                                           New _
                        Direct3D.CustomVertex.PositionColored(10, - 10, - 1, m_ColourBottom)
                                                                                                                       }

                ' Set FVF.
                .VertexFormat = Direct3D.CustomVertex.PositionColored.Format

                ' Render the rectangles.
                .DrawUserPrimitives(Direct3D.PrimitiveType.TriangleStrip, 2, upperRect)
                .DrawUserPrimitives(Direct3D.PrimitiveType.TriangleStrip, 2, lowerRect)

            End If ' If m_Meshes.Length <> 0 Then

            ' Render mesh.
            For I As Integer = - 1 To 1
                Dim m As Matrix, m2 As Matrix

                ' Make component matrices.
                If m_HalfOffset Then _
                    m.Translate(I - 0.5F, 0, 0) _
                    Else _
                    m.Translate(I, 0, 0)

                m2.Scale(m_BackBufferWidth, m_BackBufferHeight, 1)

                ' Calculate transform.
                .Transform.World = m*m2

                ' Now render all meshes.
                For J As Integer = 0 To m_Meshes.Length - 1
                    m_Meshes(J).Render(m_D3DManager.Device)

                Next J ' For J As Integer = 0 To m_Meshes.Length - 1
            Next I ' For I As Integer = -2 To 2 Step 2

            ' End scene.
            .EndScene()

        End With ' With m_D3DManager.Device
    End Sub

    Private Sub HODBGTexGen_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) _
        Handles Me.FormClosing
        ' Stop rendering.
        m_D3DManager.RenderLoopStop()

        ' Dispose device.
        m_D3DManager.Device.Dispose()
    End Sub

    Private Sub HODBGTexGen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Create device.
        m_D3DManager.CreateDevice(pbxDisplay, "", False, False)

        ' Start rendering.
        m_D3DManager.RenderLoopBegin()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        ' Close form.
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim ext As String = ""

        ' See if user pressed cancel.
        If SaveFileDialog.ShowDialog() = DialogResult.Cancel Then _
            Exit Sub

        ' Get extension.
        ext = IO.Path.GetExtension(SaveFileDialog.FileName).ToLower()

        ' Decide output format.
        Dim f As Direct3D.ImageFileFormat = Direct3D.ImageFileFormat.Tga

        Select Case ext
            Case ".bmp" : f = Direct3D.ImageFileFormat.Bmp
            Case ".dds" : f = Direct3D.ImageFileFormat.Dds
            Case ".jpg" : f = Direct3D.ImageFileFormat.Jpg
            Case ".png" : f = Direct3D.ImageFileFormat.Png
            Case ".tga" : f = Direct3D.ImageFileFormat.Tga
            Case Else : Trace.TraceWarning("Unknown format: '" & ext & "', using TGA.")
        End Select ' Select Case ext

        ' Pause rendering.
        m_D3DManager.RenderLoopPause()

        ' Get surface.
        Dim s As Direct3D.Surface = m_D3DManager.Device.GetBackBuffer(0, 0, Direct3D.BackBufferType.Mono)

        Try
            ' Write to file.
            Direct3D.SurfaceLoader.Save(SaveFileDialog.FileName, f, s)

        Catch ex As Exception
            MsgBox("Error while trying to write to file: " & vbCrLf & ex.ToString(), MsgBoxStyle.Critical, Me.Text)

        End Try

        ' Dispose the surface.
        s.Dispose()

        ' Resume rendering.
        m_D3DManager.RenderLoopResume()
    End Sub
End Class
