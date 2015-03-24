Partial Class HOD
    ''' <summary>
    ''' Copy constructor.
    ''' </summary>
    Public Sub New(ByVal H As HOD)
        ' Initialize HOD.
        Initialize()

        ' Version and name.
        m_Version = H.m_Version
        m_Name = H.m_Name

        ' Lights
        For I As Integer = 0 To H.m_Lights.Count - 1
            m_Lights.Add(New Light(H.m_Lights(I)))

        Next I ' For I As Integer = 0 To H.m_Lights.Count - 1

        ' Textures.
        For I As Integer = 0 To H.m_Textures.Count - 1
            m_Textures.Add(New Texture(H.m_Textures(I)))

        Next I ' For I As Integer = 0 To H.m_Textures.Count - 1

        ' Materials.
        For I As Integer = 0 To H.m_Materials.Count - 1
            m_Materials.Add(New Material(H.m_Materials(I)))

        Next I ' For I As Integer = 0 To H.m_Materials.Count - 1

        ' Multi meshes.
        For I As Integer = 0 To H.m_MultiMeshes.Count - 1
            m_MultiMeshes.Add(New MultiMesh(H.m_MultiMeshes(I)))

        Next I ' For I As Integer = 0 To H.m_MultiMeshes.Count - 1

        ' Goblin meshes.
        For I As Integer = 0 To H.m_GoblinMeshes.Count - 1
            m_GoblinMeshes.Add(New GoblinMesh(H.m_GoblinMeshes(I)))

        Next I ' For I As Integer = 0 To H.m_GoblinMeshes.Count - 1

        ' Background meshes.
        For I As Integer = 0 To H.m_BackgroundMeshes.Count - 1
            m_BackgroundMeshes.Add(New BasicMesh(H.m_BackgroundMeshes(I)))

        Next I ' For I As Integer = 0 To H.m_BackgroundMeshes.Count - 1

        ' Simple meshes.
        For I As Integer = 0 To H.m_SimpleMeshes.Count - 1
            m_SimpleMeshes.Add(New SimpleMesh(H.m_SimpleMeshes(I)))

        Next I ' For I As Integer = 0 To H.m_SimpleMeshes.Count - 1

        ' Variable meshes.
        For I As Integer = 0 To H.m_VariableMeshes.Count - 1
            m_VariableMeshes.Add(New VariableMesh(H.m_VariableMeshes(I)))

        Next I ' For I As Integer = 0 To H.m_VariableMeshes.Count - 1

        ' Joints.
        m_RootJoint = New Joint(H.m_RootJoint)

        ' Engine Shapes.
        For I As Integer = 0 To H.m_EngineShapes.Count - 1
            m_EngineShapes.Add(New EngineShape(H.m_EngineShapes(I)))

        Next I ' For I As Integer = 0 To H.m_EngineShapes.Count - 1

        ' Engine Glows.
        For I As Integer = 0 To H.m_EngineGlows.Count - 1
            m_EngineGlows.Add(New EngineGlow(H.m_EngineGlows(I)))

        Next I ' For I As Integer = 0 To H.m_EngineGlows.Count - 1

        ' Engine Burns.
        For I As Integer = 0 To H.m_EngineBurns.Count - 1
            m_EngineBurns.Add(New EngineBurn(H.m_EngineBurns(I)))

        Next I ' For I As Integer = 0 To H.m_EngineBurns.Count - 1

        ' NavLights.
        For I As Integer = 0 To H.m_NavLights.Count - 1
            m_NavLights.Add(New NavLight(H.m_NavLights(I)))

        Next I ' For I As Integer = 0 To H.m_NavLights.Count - 1

        ' Dockpaths.
        For I As Integer = 0 To H.m_Dockpaths.Count - 1
            m_Dockpaths.Add(New Dockpath(H.m_Dockpaths(I)))

        Next I ' For I As Integer = 0 To H.m_Dockpaths.Count - 1

        ' Markers
        For I As Integer = 0 To H.m_Markers.Count - 1
            m_Markers.Add(New Marker(H.m_Markers(I)))

        Next I ' For I As Integer = 0 To H.m_Markers.Count - 1

        ' Star fields.
        For I As Integer = 0 To H.m_StarFields.Count - 1
            m_StarFields.Add(New StarField(H.m_StarFields(I)))

        Next I ' For I As Integer = 0 To H.m_StarFields.Count - 1

        ' Textured star fields.
        For I As Integer = 0 To H.m_StarFieldsT.Count - 1
            m_StarFieldsT.Add(New StarFieldT(H.m_StarFieldsT(I)))

        Next I ' For I As Integer = 0 To H.m_StarFieldsT.Count - 1

        ' Collision meshes.
        For I As Integer = 0 To H.m_CollisionMeshes.Count - 1
            m_CollisionMeshes.Add(New CollisionMesh(H.m_CollisionMeshes(I)))

        Next I ' For I As Integer = 0 To H.m_CollisionMeshes.Count - 1

        ' INFO chunk fields.
        ReDim m_Warnings(H.m_Warnings.Length - 1)
        ReDim m_Errors(H.m_Errors.Length - 1)

        If m_Warnings.Length <> 0 Then _
            H.m_Warnings.CopyTo(m_Warnings, 0)

        If m_Errors.Length <> 0 Then _
            H.m_Errors.CopyTo(m_Errors, 0)

        m_Owner = H.m_Owner

        ' Other fields.
        m_TeamColour = H.m_TeamColour
        m_StripeColour = H.m_StripeColour
        m_ThrusterPower = H.m_ThrusterPower
        m_Badge = H.m_Badge
        m_SkeletonVisible = H.m_SkeletonVisible
        m_SkeletonScale = H.m_SkeletonScale
        m_MarkerScale = H.m_MarkerScale
        m_DockpointScale = H.m_DockpointScale
    End Sub

    ''' <summary>
    ''' Removes the specified texture and updates references.
    ''' </summary>
    ''' <param name="index">
    ''' Index of texture to remove.
    ''' </param>
    Private Sub Textures_RemoveItem(ByVal index As Integer) Handles m_Textures.RemoveItem
        ' Update materials.
        For I As Integer = 0 To m_Materials.Count - 1
            For J As Integer = 0 To m_Materials(I).Parameters.Count - 1
                ' Get the parameter.
                Dim p As Material.Parameter = m_Materials(I).Parameters(J)

                ' If the parameter references this texture, set reference
                ' to no texture.
                If p.TextureIndex = index Then _
                    p.TextureIndex = - 1

                ' If the parameter references to a texture after this,
                ' decrement reference to correct it.
                If p.TextureIndex > index Then _
                    p.TextureIndex -= 1

            Next J ' For J As Integer = 0 To m_Materials(I).Parameters.Count - 1
        Next I ' For I As Integer = 0 To m_Materials.Count - 1
    End Sub

    ''' <summary>
    ''' Removes the specified material and updates references.
    ''' </summary>
    Private Sub Materials_RemoveItem(ByVal index As Integer) Handles m_Materials.RemoveItem
        ' Update multi meshes.
        For I As Integer = 0 To m_MultiMeshes.Count - 1
            For J As Integer = 0 To m_MultiMeshes(I).LOD.Count - 1
                For K As Integer = 0 To m_MultiMeshes(I).LOD(J).PartCount - 1
                    Dim mat As Material.Reference = m_MultiMeshes(I).LOD(J).Material(K)

                    ' If the part references to this material, set reference
                    ' to no material.
                    If mat.Index = index Then _
                        mat.Index = - 1

                    ' If the part refernces a material after this material, 
                    ' then decrement the reference.
                    If mat.Index > index Then _
                        mat.Index -= 1

                    m_MultiMeshes(I).LOD(J).Material(K) = mat

                Next K ' For K As Integer = 0 To m_MultiMeshes(I).LOD(J).PartCount - 1
            Next J ' For J As Integer = 0 To m_MultiMeshes(I).LOD.Count - 1
        Next I ' For I As Integer = 0 To m_MultiMeshes.Count - 1

        ' Update goblins.
        For I As Integer = 0 To m_GoblinMeshes.Count - 1
            For J As Integer = 0 To m_GoblinMeshes(I).Mesh.PartCount - 1
                Dim mat As Material.Reference = m_GoblinMeshes(I).Mesh.Material(J)

                ' If the part references to this material, set reference
                ' to no material.
                If mat.Index = index Then _
                    mat.Index = - 1

                ' If the part refernces a material after this material, 
                ' then decrement the reference.
                If mat.Index > index Then _
                    mat.Index -= 1

                m_GoblinMeshes(I).Mesh.Material(J) = mat

            Next J ' For J As Integer = 0 To m_GoblinMeshes(I).Mesh.PartCount - 1
        Next I ' For I As Integer = 0 To m_GoblinMeshes.Count - 1
    End Sub

    ''' <summary>
    ''' Sets the 'IsWireframe' property of a simple mesh added to the list.
    ''' </summary>
    Private Sub SimpleMeshes_AddItem() Handles m_SimpleMeshes.AddItem
        ' Set it's wireframe property.
        m_SimpleMeshes(m_SimpleMeshes.Count - 1).IsWireframe = (m_Name = Name_WireframeMesh)
    End Sub

    ''' <summary>
    ''' Sets the 'IsWireframe' property of a simple mesh inserted into the list.
    ''' </summary>
    Private Sub SimpleMeshes_InsertItem(ByVal index As Integer) Handles m_SimpleMeshes.InsertItem
        ' Set it's wireframe property.
        m_SimpleMeshes(index).IsWireframe = (m_Name = Name_WireframeMesh)
    End Sub

    ''' <summary>
    ''' Returns the HOD's min\max extents.
    ''' </summary>
    ''' <param name="MinExtents">
    ''' Variable to return the minimum extents.
    ''' </param>
    ''' <param name="MaxExtents">
    ''' Variable to return the maximum extents.
    ''' </param>
    ''' <remarks>
    ''' If <c>MinExtents</c> &lt; <c>MaxExtents</c> then HOD has no vertices.
    ''' If <c>MinExtents</c> = <c>MaxExtents</c> then mesh has 1 vertex.
    ''' Otherwise, atleast one component of <c>MinExtents</c> &lt; <c>MaxExtents</c>.
    ''' </remarks>
    Public Function GetHODExtents(ByRef MinExtents As Vector3, ByRef MaxExtents As Vector3) As Boolean
        Dim FirstMesh As Boolean = True
        Const OnlyLOD0 As Boolean = True

        ' Set default values.
        MinExtents = New Vector3(1, 1, 1)
        MaxExtents = New Vector3(- 1, - 1, - 1)

        ' See if this is a multi mesh or a variable mesh.
        If (m_Name = Name_MultiMesh) OrElse (m_Name = Name_VariableMesh) Then
            ' Multi Meshes
            For I As Integer = 0 To m_MultiMeshes.Count - 1
                For J As Integer = 0 To m_MultiMeshes(I).LOD.Count - 1
                    Dim Min, Max As Vector3

                    ' See if we need to get extents.
                    If (OnlyLOD0) AndAlso (J <> 0) Then _
                        Continue For

                    ' Get extents.
                    m_MultiMeshes(I).LOD(J).GetMeshExtents(Min, Max)

                    If FirstMesh Then _
                        MinExtents = Min _
                            : MaxExtents = Max _
                            : FirstMesh = False _
                        Else _
                        MinExtents.Minimize(Min) _
                            : MaxExtents.Maximize(Max)

                Next J ' For J As Integer = 0 To m_MultiMeshes(I).LOD.Count - 1
            Next I ' For I As Integer = 0 To m_MultiMeshes.Count - 1

            ' Goblin meshes.
            For I As Integer = 0 To m_GoblinMeshes.Count - 1
                Dim Min, Max As Vector3

                ' Get extents.
                m_GoblinMeshes(I).Mesh.GetMeshExtents(Min, Max)

                If FirstMesh Then _
                    MinExtents = Min _
                        : MaxExtents = Max _
                        : FirstMesh = False _
                    Else _
                    MinExtents.Minimize(Min) _
                        : MaxExtents.Maximize(Max)

            Next I ' For I As Integer = 0 To m_GoblinMeshes.Count - 1

            ' Variable meshes.
            For I As Integer = 0 To m_VariableMeshes.Count - 1
                Dim Min, Max As Vector3

                ' Get extents.
                m_VariableMeshes(I).GetMeshExtents(Min, Max)

                If FirstMesh Then _
                    MinExtents = Min _
                        : MaxExtents = Max _
                        : FirstMesh = False _
                    Else _
                    MinExtents.Minimize(Min) _
                        : MaxExtents.Maximize(Max)

            Next I ' For I As Integer = 0 To m_VariableMeshes.Count - 1
        End If ' If (m_Name = Name_MultiMesh) OrElse (m_Name = Name_VariableMesh) Then

        ' See if this is a simple mesh or a wireframe mesh.
        If (m_Name = Name_SimpleMesh) OrElse (m_Name = Name_WireframeMesh) Then
            ' Simple meshes.
            For I As Integer = 0 To m_SimpleMeshes.Count - 1
                Dim Min, Max As Vector3

                ' Get extents.
                m_SimpleMeshes(I).GetMeshExtents(Min, Max)

                If FirstMesh Then _
                    MinExtents = Min _
                        : MaxExtents = Max _
                        : FirstMesh = False _
                    Else _
                    MinExtents.Minimize(Min) _
                        : MaxExtents.Maximize(Max)

            Next I ' For I As Integer = 0 To m_SimpleMeshes.Count - 1
        End If ' If (m_Name = Name_SimpleMesh) OrElse (m_Name = Name_WireframeMesh) Then

        ' See if this is a background mesh.
        If m_Version = 1000 Then
            ' Background meshes.
            For I As Integer = 0 To m_BackgroundMeshes.Count - 1
                Dim Min, Max As Vector3

                ' Get extents.
                m_BackgroundMeshes(I).GetMeshExtents(Min, Max)

                If FirstMesh Then _
                    MinExtents = Min _
                        : MaxExtents = Max _
                        : FirstMesh = False _
                    Else _
                    MinExtents.Minimize(Min) _
                        : MaxExtents.Maximize(Max)

            Next I ' For I As Integer = 0 To m_BackgroundMeshes.Count - 1
        End If ' If m_Version = 1000 Then

        Return Not FirstMesh
    End Function

    ''' <summary>
    ''' Returns the HOD's boudary sphere.
    ''' </summary>
    ''' <param name="Center">
    ''' Center of the HOD.
    ''' </param>
    ''' <param name="Radius">
    ''' Radius of the HOD.
    ''' </param>
    ''' <remarks>
    ''' If <c>Center</c> = {0, 0, 0} and <c>Radius</c> = 0 then HOD has no vertices.
    ''' If <c>Center</c> &lt;&gt; {0, 0, 0} and <c>Radius</c> = 0 then HOD has 1 vertex.
    ''' Otherwise, the <c>Center</c> and <c>Radius</c> are correct.
    ''' </remarks>
    Public Function GetHODSphere(ByRef Center As Vector3, ByRef Radius As Single) As Boolean
        Dim Min, Max As Vector3

        ' Get the mesh extents.
        If Not GetHODExtents(Min, Max) Then _
            Center = New Vector3(0, 0, 0) _
                : Radius = 0 _
                : Return False

        ' Get the center.
        Center = (Min + Max)*0.5

        ' Get the radius.
        Radius = (Max - Min).Length()/2

        Return True
    End Function

    ''' <summary>
    ''' Translates the whole HOD, without affecting the "Root" joint.
    ''' </summary>
    ''' <param name="v">
    ''' The translation vector.
    ''' </param>
    ''' <remarks>
    ''' Affect background meshes, lights, and stars.
    ''' </remarks>
    Public Sub Translation(ByVal v As Vector3)
        ' Lights
        For I As Integer = 0 To m_Lights.Count - 1
            If m_Lights(I).Type = Light.LightType.Point Then _
                m_Lights(I).Position += v

        Next I ' For I As Integer = 0 To m_Lights.Count - 1

        ' Multi meshes.
        For I As Integer = 0 To m_MultiMeshes.Count - 1
            For J As Integer = 0 To m_MultiMeshes(I).LOD.Count - 1
                m_MultiMeshes(I).LOD(J).Translation(v)

            Next J ' For J As Integer = 0 To m_MultiMeshes(I).LOD.Count - 1
        Next I ' For I As Integer = 0 To m_MultiMeshes.Count - 1

        ' Goblin meshes.
        For I As Integer = 0 To m_GoblinMeshes.Count - 1
            m_GoblinMeshes(I).Mesh.Translation(v)

        Next I ' For I As Integer = 0 To m_GoblinMeshes.Count - 1

        ' Background meshes.
        For I As Integer = 0 To m_BackgroundMeshes.Count - 1
            m_BackgroundMeshes(I).Translation(v)

        Next I ' For I As Integer = 0 To m_BackgroundMeshes.Count - 1

        ' Simple meshes.
        For I As Integer = 0 To m_SimpleMeshes.Count - 1
            m_SimpleMeshes(I).Translation(v)

        Next I ' For I As Integer = 0 To m_SimpleMeshes.Count - 1

        ' Joints.
        For I As Integer = 0 To m_RootJoint.Children.Count - 1
            m_RootJoint.Children(I).Position += v

        Next I ' For I As Integer = 0 To m_RootJoint.Children.Count - 1

        ' Engine Shapes.
        For I As Integer = 0 To m_EngineShapes.Count - 1
            m_EngineShapes(I).Translation(v)

        Next I ' For I As Integer = 0 To m_EngineShapes.Count - 1

        ' Engine Glows.
        For I As Integer = 0 To m_EngineGlows.Count - 1
            m_EngineGlows(I).Mesh.Translation(v)

        Next I ' For I As Integer = 0 To m_EngineGlows.Count - 1

        ' Engine Burns.
        For I As Integer = 0 To m_EngineBurns.Count - 1
            For J As Integer = 0 To m_EngineBurns.Count - 1
                m_EngineBurns(I).Vertices(J) += v

            Next J ' For J As Integer = 0 To m_EngineBurns.Count - 1
        Next I ' For I As Integer = 0 To m_EngineBurns.Count - 1

        ' Dockpaths.
        For I As Integer = 0 To m_Dockpaths.Count - 1
            For J As Integer = 0 To m_Dockpaths(I).Dockpoints.Count - 1
                m_Dockpaths(I).Dockpoints(J).Position += v

            Next J ' For J As Integer = 0 To m_Dockpaths(I).Dockpoints.Count - 1
        Next I ' For I As Integer = 0 To m_Dockpaths.Count - 1

        ' Markers
        For I As Integer = 0 To m_Markers.Count - 1
            m_Markers(I).Position += v

        Next I ' For I As Integer = 0 To m_Markers.Count - 1

        ' Star fields.
        For I As Integer = 0 To m_StarFields.Count - 1
            For J As Integer = 0 To m_StarFields(I).Count - 1
                Dim s As Star = m_StarFields(I)(J)
                s.Position += v
                m_StarFields(I)(J) = s

            Next J ' For J As Integer = 0 To m_StarFields(I).Count - 1
        Next I ' For I As Integer = 0 To m_StarFields.Count - 1

        ' Textured star fields.
        For I As Integer = 0 To m_StarFieldsT.Count - 1
            For J As Integer = 0 To m_StarFieldsT(I).Count - 1
                Dim s As Star = m_StarFieldsT(I)(J)
                s.Position += v
                m_StarFieldsT(I)(J) = s

            Next J ' For J As Integer = 0 To m_StarFieldsT(I).Count - 1
        Next I ' For I As Integer = 0 To m_StarFieldsT.Count - 1

        ' Collision meshes.
        For I As Integer = 0 To m_CollisionMeshes.Count - 1
            m_CollisionMeshes(I).Translation(v)

        Next I ' For I As Integer = 0 To m_CollisionMeshes.Count - 1
    End Sub

    ''' <summary>
    ''' Rotates the whole HOD, without affecting the "Root" joint.
    ''' </summary>
    ''' <param name="v">
    ''' The rotation vector (with rotations in radians).
    ''' </param>
    ''' <remarks>
    ''' Affect background meshes, lights, and stars.
    ''' </remarks>
    Public Sub Rotation(ByVal v As Vector3)
        Dim m As Matrix = Matrix.RotationX(v.X)*
                          Matrix.RotationY(v.Y)*
                          Matrix.RotationZ(v.Z)

        ' Lights
        For I As Integer = 0 To m_Lights.Count - 1
            If m_Lights(I).Type = Light.LightType.Point Then _
                m_Lights(I).Position = Vector3.TransformCoordinate(m_Lights(I).Position, m)

            If m_Lights(I).Type = Light.LightType.Point Then _
                m_Lights(I).Direction = Vector3.TransformCoordinate(m_Lights(I).Direction, m)

        Next I ' For I As Integer = 0 To m_Lights.Count - 1

        ' Multi meshes.
        For I As Integer = 0 To m_MultiMeshes.Count - 1
            For J As Integer = 0 To m_MultiMeshes(I).LOD.Count - 1
                m_MultiMeshes(I).LOD(J).Rotation(v)

            Next J ' For J As Integer = 0 To m_MultiMeshes(I).LOD.Count - 1
        Next I ' For I As Integer = 0 To m_MultiMeshes.Count - 1

        ' Goblin meshes.
        For I As Integer = 0 To m_GoblinMeshes.Count - 1
            m_GoblinMeshes(I).Mesh.Rotation(v)

        Next I ' For I As Integer = 0 To m_GoblinMeshes.Count - 1

        ' Background meshes.
        For I As Integer = 0 To m_BackgroundMeshes.Count - 1
            m_BackgroundMeshes(I).Rotation(v)

        Next I ' For I As Integer = 0 To m_BackgroundMeshes.Count - 1

        ' Simple meshes.
        For I As Integer = 0 To m_SimpleMeshes.Count - 1
            m_SimpleMeshes(I).Rotation(v)

        Next I ' For I As Integer = 0 To m_SimpleMeshes.Count - 1

        ' Joints.
        Dim joints() As Joint = m_RootJoint.ToArray()

        For I As Integer = 0 To joints.Length - 1
            joints(I).Position = Vector3.TransformCoordinate(joints(I).Position, m)
            'joints(I).Rotation += v

        Next I ' For I As Integer = 0 To joints.Length - 1

        Erase joints

        ' Engine Shapes.
        For I As Integer = 0 To m_EngineShapes.Count - 1
            m_EngineShapes(I).Rotation(v)

        Next I ' For I As Integer = 0 To m_EngineShapes.Count - 1

        ' Engine Glows.
        For I As Integer = 0 To m_EngineGlows.Count - 1
            m_EngineGlows(I).Mesh.Rotation(v)

        Next I ' For I As Integer = 0 To m_EngineGlows.Count - 1

        ' Engine Burns.
        For I As Integer = 0 To m_EngineBurns.Count - 1
            For J As Integer = 0 To m_EngineBurns.Count - 1
                m_EngineBurns(I).Vertices(J) = Vector3.TransformCoordinate(m_EngineBurns(I).Vertices(J), m)

            Next J ' For J As Integer = 0 To m_EngineBurns.Count - 1
        Next I ' For I As Integer = 0 To m_EngineBurns.Count - 1

        ' Dockpaths.
        For I As Integer = 0 To m_Dockpaths.Count - 1
            For J As Integer = 0 To m_Dockpaths(I).Dockpoints.Count - 1
                m_Dockpaths(I).Dockpoints(J).Position =
                    Vector3.TransformCoordinate(m_Dockpaths(I).Dockpoints(J).Position, m)
                m_Dockpaths(I).Dockpoints(J).Rotation += v

            Next J ' For J As Integer = 0 To m_Dockpaths(I).Dockpoints.Count - 1
        Next I ' For I As Integer = 0 To m_Dockpaths.Count - 1

        ' Markers
        For I As Integer = 0 To m_Markers.Count - 1
            m_Markers(I).Position = Vector3.TransformCoordinate(m_Markers(I).Position, m)
            m_Markers(I).Rotation += v

        Next I ' For I As Integer = 0 To m_Markers.Count - 1

        ' Star fields.
        For I As Integer = 0 To m_StarFields.Count - 1
            For J As Integer = 0 To m_StarFields(I).Count - 1
                Dim s As Star = m_StarFields(I)(J)
                s.Position = Vector3.TransformCoordinate(s.Position, m)
                m_StarFields(I)(J) = s

            Next J ' For J As Integer = 0 To m_StarFields(I).Count - 1
        Next I ' For I As Integer = 0 To m_StarFields.Count - 1

        ' Textured star fields.
        For I As Integer = 0 To m_StarFieldsT.Count - 1
            For J As Integer = 0 To m_StarFieldsT(I).Count - 1
                Dim s As Star = m_StarFieldsT(I)(J)
                s.Position = Vector3.TransformCoordinate(s.Position, m)
                m_StarFieldsT(I)(J) = s

            Next J ' For J As Integer = 0 To m_StarFieldsT(I).Count - 1
        Next I ' For I As Integer = 0 To m_StarFieldsT.Count - 1

        ' Collision meshes.
        For I As Integer = 0 To m_CollisionMeshes.Count - 1
            m_CollisionMeshes(I).Rotation(v)

        Next I ' For I As Integer = 0 To m_CollisionMeshes.Count - 1
    End Sub

    ''' <summary>
    ''' Scales the whole HOD, without affecting the "Root" joint.
    ''' </summary>
    ''' <param name="v">
    ''' The scaling vector.
    ''' </param>
    ''' <remarks>
    ''' Affect background meshes, lights, and stars.
    ''' </remarks>
    Public Sub Scaling(ByVal v As Vector3)
        Dim m As Matrix = Matrix.Scaling(v)

        ' Lights
        For I As Integer = 0 To m_Lights.Count - 1
            If m_Lights(I).Type = Light.LightType.Point Then _
                m_Lights(I).Position = Vector3.TransformCoordinate(m_Lights(I).Position, m)

        Next I ' For I As Integer = 0 To m_Lights.Count - 1

        ' Multi meshes.
        For I As Integer = 0 To m_MultiMeshes.Count - 1
            For J As Integer = 0 To m_MultiMeshes(I).LOD.Count - 1
                m_MultiMeshes(I).LOD(J).Scaling(v)

            Next J ' For J As Integer = 0 To m_MultiMeshes(I).LOD.Count - 1
        Next I ' For I As Integer = 0 To m_MultiMeshes.Count - 1

        ' Goblin meshes.
        For I As Integer = 0 To m_GoblinMeshes.Count - 1
            m_GoblinMeshes(I).Mesh.Scaling(v)

        Next I ' For I As Integer = 0 To m_GoblinMeshes.Count - 1

        ' Background meshes.
        For I As Integer = 0 To m_BackgroundMeshes.Count - 1
            m_BackgroundMeshes(I).Scaling(v)

        Next I ' For I As Integer = 0 To m_BackgroundMeshes.Count - 1

        ' Simple meshes.
        For I As Integer = 0 To m_SimpleMeshes.Count - 1
            m_SimpleMeshes(I).Scaling(v)

        Next I ' For I As Integer = 0 To m_SimpleMeshes.Count - 1

        ' Joints.
        Dim joints() As Joint = m_RootJoint.ToArray()

        For I As Integer = 0 To joints.Length - 1
            joints(I).Position = Vector3.TransformCoordinate(joints(I).Position, m)

        Next I ' For I As Integer = 0 To joints.Length - 1

        Erase joints

        ' Engine Shapes.
        For I As Integer = 0 To m_EngineShapes.Count - 1
            m_EngineShapes(I).Scaling(v)

        Next I ' For I As Integer = 0 To m_EngineShapes.Count - 1

        ' Engine Glows.
        For I As Integer = 0 To m_EngineGlows.Count - 1
            m_EngineGlows(I).Mesh.Scaling(v)

        Next I ' For I As Integer = 0 To m_EngineGlows.Count - 1

        ' Engine Burns.
        For I As Integer = 0 To m_EngineBurns.Count - 1
            For J As Integer = 0 To m_EngineBurns.Count - 1
                m_EngineBurns(I).Vertices(J) = Vector3.TransformCoordinate(m_EngineBurns(I).Vertices(J), m)

            Next J ' For J As Integer = 0 To m_EngineBurns.Count - 1
        Next I ' For I As Integer = 0 To m_EngineBurns.Count - 1

        ' Dockpaths.
        For I As Integer = 0 To m_Dockpaths.Count - 1
            For J As Integer = 0 To m_Dockpaths(I).Dockpoints.Count - 1
                m_Dockpaths(I).Dockpoints(J).Position =
                    Vector3.TransformCoordinate(m_Dockpaths(I).Dockpoints(J).Position, m)

            Next J ' For J As Integer = 0 To m_Dockpaths(I).Dockpoints.Count - 1
        Next I ' For I As Integer = 0 To m_Dockpaths.Count - 1

        ' Markers
        For I As Integer = 0 To m_Markers.Count - 1
            m_Markers(I).Position = Vector3.TransformCoordinate(m_Markers(I).Position, m)

        Next I ' For I As Integer = 0 To m_Markers.Count - 1

        ' Star fields.
        For I As Integer = 0 To m_StarFields.Count - 1
            For J As Integer = 0 To m_StarFields(I).Count - 1
                Dim s As Star = m_StarFields(I)(J)
                s.Position = Vector3.TransformCoordinate(s.Position, m)
                m_StarFields(I)(J) = s

            Next J ' For J As Integer = 0 To m_StarFields(I).Count - 1
        Next I ' For I As Integer = 0 To m_StarFields.Count - 1

        ' Textured star fields.
        For I As Integer = 0 To m_StarFieldsT.Count - 1
            For J As Integer = 0 To m_StarFieldsT(I).Count - 1
                Dim s As Star = m_StarFieldsT(I)(J)
                s.Position = Vector3.TransformCoordinate(s.Position, m)
                m_StarFieldsT(I)(J) = s

            Next J ' For J As Integer = 0 To m_StarFieldsT(I).Count - 1
        Next I ' For I As Integer = 0 To m_StarFieldsT.Count - 1

        ' Collision meshes.
        For I As Integer = 0 To m_CollisionMeshes.Count - 1
            m_CollisionMeshes(I).Scaling(v)

        Next I ' For I As Integer = 0 To m_CollisionMeshes.Count - 1
    End Sub

    ''' <summary>
    ''' Mirrors the whole HOD, without affecting the "Root" joint.
    ''' </summary>
    ''' <param name="v">
    ''' A vector with:
    ''' X co-ordinate non-zero to mirror about YZ plane;
    ''' Y co-ordinate non-zero to mirror about XZ plane;
    ''' Z co-ordinate non-zero to mirror about XY plane.
    ''' </param>
    ''' <remarks>
    ''' Affect background meshes, lights, and stars.
    ''' </remarks>
    Public Sub Mirroring(ByVal v As Vector3)
        ' Prepare the scaling vector.
        Dim scale As New Vector3(CSng(IIf(v.X <> 0, - 1, 1)),
                                 CSng(IIf(v.Y <> 0, - 1, 1)),
                                 CSng(IIf(v.Z <> 0, - 1, 1)))

        ' Decide whether to reverse faces or not.
        Dim reverseFaces As Boolean = (v.X <> 0) Xor (v.Y <> 0) Xor (v.Z <> 0)

        ' Scale HOD.
        Scaling(scale)

        ' Now reverse faces if needed.
        If Not reverseFaces Then _
            Exit Sub

        ' Multi meshes.
        For I As Integer = 0 To m_MultiMeshes.Count - 1
            For J As Integer = 0 To m_MultiMeshes(I).LOD.Count - 1
                m_MultiMeshes(I).LOD(J).ReverseFaceOrder()

            Next J ' For J As Integer = 0 To m_MultiMeshes(I).LOD.Count - 1
        Next I ' For I As Integer = 0 To m_MultiMeshes.Count - 1

        ' Goblin meshes.
        For I As Integer = 0 To m_GoblinMeshes.Count - 1
            m_GoblinMeshes(I).Mesh.ReverseFaceOrder()

        Next I ' For I As Integer = 0 To m_GoblinMeshes.Count - 1

        ' Background meshes.
        For I As Integer = 0 To m_BackgroundMeshes.Count - 1
            m_BackgroundMeshes(I).ReverseFaceOrder()

        Next I ' For I As Integer = 0 To m_BackgroundMeshes.Count - 1

        ' Simple meshes.
        For I As Integer = 0 To m_SimpleMeshes.Count - 1
            m_SimpleMeshes(I).ReverseFaceOrder()

        Next I ' For I As Integer = 0 To m_SimpleMeshes.Count - 1

        ' Engine Shapes.
        For I As Integer = 0 To m_EngineShapes.Count - 1
            m_EngineShapes(I).ReverseFaceOrder()

        Next I ' For I As Integer = 0 To m_EngineShapes.Count - 1

        ' Engine Glows.
        For I As Integer = 0 To m_EngineGlows.Count - 1
            m_EngineGlows(I).Mesh.ReverseFaceOrder()

        Next I ' For I As Integer = 0 To m_EngineGlows.Count - 1

        ' Collision meshes.
        For I As Integer = 0 To m_CollisionMeshes.Count - 1
            m_CollisionMeshes(I).ReverseFaceOrder()

        Next I ' For I As Integer = 0 To m_CollisionMeshes.Count - 1
    End Sub
End Class
