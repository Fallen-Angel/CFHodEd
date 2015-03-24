#Const UseTriangleListsInsteadOfStrips = True

Namespace Standard
    Partial Public NotInheritable Class BasicMesh
        ''' <summary>
        ''' Class containing functions for creating various shapes for
        ''' <c>Standard.BasicMesh</c> class.
        ''' </summary>
        Public Class MeshTemplates
            ''' <summary>
            ''' Creates a (rectangular or trapezoidal) plane whose normal is the Z axis.
            ''' </summary>
            ''' <param name="SizeXUp">
            ''' Size of the plane in X-dimension (when Y = 0).
            ''' </param>
            ''' <param name="SizeXDown">
            ''' Size of the plane in X-dimension (when Y = <c>SizeY</c>)
            ''' </param>
            ''' <param name="SizeY">
            ''' Size of the plane in Y-dimension.
            ''' </param>
            ''' <param name="AlignLeft">
            ''' Whether the top part is aligned to the left.
            ''' </param>
            ''' <param name="AlignRight">
            ''' Whether the top part is aligned to the right.
            ''' </param>
            ''' <param name="SubdivisionX">
            ''' Number of sub-divisions in X-dimension.
            ''' </param>
            ''' <param name="SubdivisionY">
            ''' Number of sub-divisions in Y-dimension.
            ''' </param>
            ''' <param name="ColourUp">
            ''' Colour on the top edge (X = 0) or (Y = 0).
            ''' </param>
            ''' <param name="ColourDown">
            ''' Colour on the bottom edge (X = <c>SizeX</c>) or (Y = <c>SizeY</c>).
            ''' </param>
            ''' <param name="ColourBlendY">
            ''' <c>True</c> if colour is blended using Y co-ordinate, otherwise
            ''' colour is blended using X co-ordinate.
            ''' </param>
            ''' <param name="ReverseFaces">
            ''' Whether the plane's normal is +ve Z or -ve Z axis.
            ''' </param>
            ''' <remarks>
            ''' (X = 0) or (X = <c>SizeX</c>) or (Y = 0) or (Y = <c>SizeY</c>)
            ''' actually refers to relative position and not absolute position
            ''' (in space).
            ''' </remarks>
            Public Shared Function CreatePlane(ByVal SizeXUp As Single, ByVal SizeXDown As Single, ByVal SizeY As Single,
                                               ByVal SubdivisionX As Integer, ByVal SubdivisionY As Integer,
                                               ByVal ColourUp As ColorValue, ByVal ColourDown As ColorValue,
                                               Optional ByVal AlignLeft As Boolean = False,
                                               Optional ByVal AlignRight As Boolean = False,
                                               Optional ByVal ColourBlendY As Boolean = True,
                                               Optional ByVal ReverseFaces As Boolean = False) As BasicMesh

                Dim M As BasicMesh

                ' Check inputs.
                ' -------------
                ' Check inputs
                If (SizeXUp < 0) OrElse (SizeXDown < 0) OrElse (SizeY <= 0) OrElse
                   ((SizeXUp = 0) AndAlso (SizeXDown = 0)) Then _
                    Throw New ArgumentException("Invalid extents.") _
                        : Exit Function

                ' Check inputs.
                If (SubdivisionX <= 0) Or (SubdivisionY <= 0) Then _
                    Throw New ArgumentException("Invalid sub-division.") _
                        : Exit Function

                ' Check alignment flags.
                If AlignLeft And AlignRight Then _
                    Throw New ArgumentException("Invalid alignment flags.") _
                        : Exit Function

                ' Add a new part and it's primitives.
                ' -----------------------------------
                M = New BasicMesh()
                M.Add(1)
                M.Part(0).Vertices.Count = (SubdivisionX + 1)*(SubdivisionY + 1)

#If UseTriangleListsInsteadOfStrips Then
                M.Part(0).PrimitiveGroupCount = 1
                M.Part(0).PrimitiveGroups(0).Type = PrimitiveType.TriangleList

#Else ' #If UseTriangleListsInsteadOfStrips Then
    M.Part(0).PrimitiveGroupCount = SubdivisionX * SubdivisionY

#End If ' #If UseTriangleListsInsteadOfStrips Then

                ' Get some data.
                ' --------------
                ' Get the maximum width.
                Dim MaxSizeX As Single = Math.Max(SizeXUp, SizeXDown)

                ' Retrieve the colours.
                Dim MinColR, MinColG, MinColB, MinColA As Single
                Dim MaxColR, MaxColG, MaxColB, MaxColA As Single

                MinColR = ColourUp.Red
                MinColG = ColourUp.Green
                MinColB = ColourUp.Blue
                MinColA = ColourUp.Alpha

                MaxColR = ColourDown.Red
                MaxColG = ColourDown.Green
                MaxColB = ColourDown.Blue
                MaxColA = ColourDown.Alpha

                ' Now calculate all vertices.
                ' ---------------------------
                For J As Integer = 0 To SubdivisionY
                    Dim EffSizeX As Single = SizeXUp + CSng(J/SubdivisionY)*(SizeXDown - SizeXUp)

                    For I As Integer = 0 To SubdivisionX
                        Dim V As Vertex
                        With V
                            ' Calculate UVs, from which position is calculated.
                            ' Left align * * * *
                            If AlignLeft AndAlso Not AlignRight Then _
                                .TexCoords = New Vector2((EffSizeX/MaxSizeX)*CSng(I/SubdivisionX),
                                                         CSng(J/SubdivisionY))

                            ' * * * * Right align
                            If AlignRight AndAlso Not AlignLeft Then _
                                .TexCoords = New Vector2((EffSizeX/MaxSizeX)*CSng(I/SubdivisionX - 1.0F) + 1.0F,
                                                         CSng(J/SubdivisionY))

                            ' * * Center align * *
                            If Not (AlignLeft Xor AlignRight) Then _
                                .TexCoords = New Vector2((EffSizeX/MaxSizeX)*CSng(I/SubdivisionX - 0.5F) + 0.5F,
                                                         CSng(J/SubdivisionY))


                            ' Calculate position from UVs.
                            .Position = New Vector3((.TexCoords.X - 0.5F)*MaxSizeX,
                                                    (.TexCoords.Y - 0.5F)*- SizeY,
                                                    0)
                            ' Calculate normals.
                            If ReverseFaces Then _
                                .Normal = New Vector3(0, 0, - 1) _
                                Else _
                                .Normal = New Vector3(0, 0, 1)

                            ' Calculate the blended colour.
                            If ColourBlendY Then
                                ' Blend using Y.
                                Dim BlendFactor As Single

                                ' This is nothing but the flipped V co-ordinate.
                                BlendFactor = 1 - .TexCoords.Y

                                ' Now blend the colour.
                                .Diffuse = New ColorValue(
                                    (MinColR*BlendFactor + MaxColR*(1 - BlendFactor)),
                                    (MinColG*BlendFactor + MaxColG*(1 - BlendFactor)),
                                    (MinColB*BlendFactor + MaxColB*(1 - BlendFactor)),
                                    (MinColA*BlendFactor + MaxColA*(1 - BlendFactor))
                                    )
                            Else ' If ColourBlendY Then
                                ' Blend using X.
                                Dim BlendFactor As Single

                                ' For this, we first need to find the acutal X (UV contains aligned X).
                                BlendFactor = 1 - CSng(I/SubdivisionX)

                                ' Now blend the colour.
                                .Diffuse = New ColorValue(
                                    (MinColR*BlendFactor + MaxColR*(1 - BlendFactor)),
                                    (MinColG*BlendFactor + MaxColG*(1 - BlendFactor)),
                                    (MinColB*BlendFactor + MaxColB*(1 - BlendFactor)),
                                    (MinColA*BlendFactor + MaxColA*(1 - BlendFactor))
                                    )
                            End If ' If ColourBlendY Then
                        End With ' With V

                        ' Set the vertex.
                        M.Part(0).Vertices((SubdivisionX + 1)*J + I) = V

#If UseTriangleListsInsteadOfStrips Then
                        ' We add a primitive for all vertices, except the ones at the far edges (right, bottom).
                        ' --------------------------------------------------------------------------------------
                        If (I <> SubdivisionX) AndAlso (J <> SubdivisionY) Then
                            Dim Ind() As Integer

                            ' Build the indice array.
                            If ReverseFaces Then _
                                Ind = New Integer() {(SubdivisionX + 1)*J + I,
                                                     (SubdivisionX + 1)*J + (I + 1),
                                                     (SubdivisionX + 1)*(J + 1) + I,
                                                     (SubdivisionX + 1)*(J + 1) + (I + 1),
                                                     (SubdivisionX + 1)*(J + 1) + I,
                                                     (SubdivisionX + 1)*J + (I + 1)} _
                                Else _
                                Ind = New Integer() {(SubdivisionX + 1)*J + I,
                                                     (SubdivisionX + 1)*(J + 1) + I,
                                                     (SubdivisionX + 1)*J + (I + 1),
                                                     (SubdivisionX + 1)*(J + 1) + (I + 1),
                                                     (SubdivisionX + 1)*J + (I + 1),
                                                     (SubdivisionX + 1)*(J + 1) + I}

                            ' Set the primitive group data.
                            M.Part(0).PrimitiveGroups(0).Append(Ind)

                            ' Remove the indices.
                            Erase Ind

                        End If ' If (I <> SubdivisionX) AndAlso (J <> SubdivisionY) Then
#Else ' #If UseTriangleListsInsteadOfStrips Then
      ' We add a primitive for all vertices, except the ones at the far edges (right, bottom).
      ' --------------------------------------------------------------------------------------
      If (I <> SubdivisionX) AndAlso (J <> SubdivisionY) Then
       Dim Ind() As Integer

       ' Build the indice array.
       If ReverseFaces Then _
        Ind = New Integer() {(SubdivisionX + 1) * J + I, _
                             (SubdivisionX + 1) * J + (I + 1), _
                             (SubdivisionX + 1) * (J + 1) + I, _
                             (SubdivisionX + 1) * (J + 1) + (I + 1)} _
      Else _
        Ind = New Integer() {(SubdivisionX + 1) * J + I, _
                             (SubdivisionX + 1) * (J + 1) + I, _
                             (SubdivisionX + 1) * J + (I + 1), _
                             (SubdivisionX + 1) * (J + 1) + (I + 1)}

       ' Set the primitive group data.
       With M.Part(0).PrimitiveGroups(J * SubdivisionX + I)
        .Type = PrimitiveType.TriangleStrip
        .Append(Ind)

       End With ' With M.Part(0).PrimitiveGroups(J * SubdivisionX + I)

       ' Remove the indices.
       Erase Ind

      End If ' If (I <> SubdivisionX) AndAlso (J <> SubdivisionY) Then
#End If ' #If UseTriangleListsInsteadOfStrips Then

                    Next I ' For I As Integer = 0 To SubdivisionX
                Next J ' For J As Integer = 0 To SubdivisionY

                Return M
            End Function

            ''' <summary>
            ''' Creates a polygon on the XY plane centered at origin (with circumscribing circle's radius).
            ''' </summary>
            ''' <param name="CircumRadius">
            ''' Radius of the circumscribing circle.
            ''' </param>
            ''' <param name="EdgeCount">
            ''' Number of edges.
            ''' </param>
            ''' <param name="ColourCenter">
            ''' Colour at the center.
            ''' </param>
            ''' <param name="ColourRim">
            ''' Colour at the periphery (rim).
            ''' </param>
            ''' <param name="AngleOffsetInDegrees">
            ''' Starting angle, from +ve X axis, in degrees.
            ''' </param>
            ''' <param name="AngleSizeInDegrees">
            ''' Size of the angle, measured in anti-clockwise direction, in degrees.
            ''' </param>
            ''' <param name="ReverseFaces">
            ''' Whether the plane's normal is +ve Z or -ve Z axis.
            ''' </param>
            Public Shared Function CreatePolygon(ByVal CircumRadius As Single, ByVal EdgeCount As Integer,
                                                 ByVal ColourCenter As ColorValue, ByVal ColourRim As ColorValue,
                                                 Optional ByVal AngleOffsetInDegrees As Single = 0,
                                                 Optional ByVal AngleSizeInDegrees As Single = 360,
                                                 Optional ByVal ReverseFaces As Boolean = False) As BasicMesh

                Dim M As BasicMesh
                Dim Verts() As Standard.Vertex, Ind() As Integer

                ' Check inputs.
                ' -------------
                ' Check inputs.
                If CircumRadius <= 0 Then _
                    Throw New ArgumentException("Invalid circum-radius.") _
                        : Exit Function

                ' Check inputs.
                If EdgeCount < 3 Then _
                    Throw New ArgumentException("Invalid edge count.") _
                        : Exit Function

                ' Check inputs.
                If AngleSizeInDegrees <= 0 Then _
                    Throw New ArgumentException("Invalid angle size.") _
                        : Exit Function

                ' Calculate angles.
                ' -----------------
                Dim AngleOffset As Single = AngleOffsetInDegrees*PI/180
                Dim AngleSize As Single = AngleSizeInDegrees*PI/180

                ' Allocate space for vertices and indices.
                ' ----------------------------------------
                ReDim Verts(EdgeCount + 1)
                ReDim Ind(EdgeCount + 1)

                ' Write data for the central vertex.
                ' ----------------------------------
                With Verts(0)
                    ' Set position.
                    .Position = New Vector3(0, 0, 0)

                    ' Set normal.
                    If ReverseFaces Then _
                        .Normal = New Vector3(0, 0, - 1) _
                        Else _
                        .Normal = New Vector3(0, 0, 1)

                    ' Set texture co-ordinates.
                    .TexCoords = New Vector2(0.5, 0.5)

                    ' Set colour.
                    .Diffuse = ColourCenter

                End With ' With Verts(0)

                ' Write the central indice.
                ' -------------------------
                Ind(0) = 0

                For I As Integer = 0 To EdgeCount
                    ' Build vertex data.
                    ' ------------------
                    With Verts(I + 1)
                        ' Calculate the rotation angle.
                        Dim Theta As Single = AngleOffset + AngleSize*CSng(I/EdgeCount)

                        ' Calculate position.
                        .Position = New Vector3(CircumRadius*Cos(Theta),
                                                CircumRadius*Sin(Theta),
                                                0)

                        ' Set normal.
                        If ReverseFaces Then _
                            .Normal = New Vector3(0, 0, - 1) _
                            Else _
                            .Normal = New Vector3(0, 0, 1)

                        ' Calculate UV
                        .TexCoords = New Vector2((1 + Cos(Theta))/2,
                                                 (1 + Sin(Theta))/2)

                        ' Set colour
                        .Diffuse = ColourRim
                    End With ' With Verts(I + 1)

                    ' Set the indice.
                    ' ---------------
                    Ind(I + 1) = I + 1
                Next I ' For I As Integer = 0 To EdgeCount

                ' Prepare the output mesh.
                ' ------------------------
                ' Prepare a new mesh.
                M = New BasicMesh

                ' Add a new part.
                M.Add(1)

                ' Set it's data.
                With M.Part(0)
                    .PrimitiveGroupCount = 1
                    .Vertices.Append(Verts)
                    .PrimitiveGroups(0).Type = PrimitiveType.TriangleFan
                    .PrimitiveGroups(0).Append(Ind)

                    ' Reverse face order if needed.
                    If ReverseFaces Then _
                        .ReverseFaceOrder()

                End With ' With M.Part(0)

                ' Remove temporary arrays.
                ' ------------------------
                Erase Verts
                Erase Ind

                Return M
            End Function

            ''' <summary>
            ''' Creates a sphere of given radius centered at origin.
            ''' </summary>
            ''' <param name="Radius">
            ''' Radius of the sphere.
            ''' </param>
            ''' <param name="ColourTop">
            ''' Colour of the sphere.
            ''' </param>
            ''' <param name="ColourBottom">
            ''' Colour of the sphere.
            ''' </param>
            ''' <param name="NumStacks">
            ''' Number of stacks.
            ''' </param>
            ''' <param name="NumSlices">
            ''' Number of slices.
            ''' </param>
            ''' <param name="ReverseFaces">
            ''' Whether the plane's normal is +ve Z or -ve Z axis.
            ''' </param>
            Public Shared Function CreateSphere(ByVal Radius As Single,
                                                ByVal ColourTop As ColorValue, ByVal ColourBottom As ColorValue,
                                                Optional ByVal NumStacks As Integer = 10,
                                                Optional ByVal NumSlices As Integer = 10,
                                                Optional ByVal ReverseFaces As Boolean = False) As BasicMesh

                Dim M As BasicMesh

                ' Check inputs.
                ' -------------
                ' Check inputs.
                If Radius <= 0 Then _
                    Throw New ArgumentException("Invalid radius.") _
                        : Exit Function

                ' Check inputs.
                If (NumStacks < 3) Or (NumSlices < 3) Then _
                    Throw New ArgumentException("Invalid stack\slice count (must be atleast 3).") _
                        : Exit Function

                ' Build the initial radius vector.
                ' --------------------------------
                Dim vRadius As New Vector3(0, 1, 0)

                ' Create a plane.
                ' ---------------
                M = CreatePlane(1, 1, 1, NumSlices, NumStacks, ColourTop, ColourBottom, False, False, True, ReverseFaces)

                ' Transform plane to sphere.
                ' --------------------------
                For I As Integer = 0 To M.Part(0).Vertices.Count - 1
                    Dim V As Vertex = M.Part(0).Vertices(I)

                    ' Calculate the normal, by rotating the radius vector.
                    V.Normal = Vector3.TransformCoordinate(
                        vRadius,
                        (Matrix.RotationX(V.TexCoords.Y*PI)*
                         Matrix.RotationY(V.TexCoords.X*PI*2))
                        )

                    ' Get the position.
                    V.Position = Radius*V.Normal

                    If ReverseFaces Then _
                        V.Normal = - V.Normal

                    ' Set the vertex data.
                    M.Part(0).Vertices(I) = V

                Next I ' For I As Integer = 0 To M.Part(0).Vertices.Count - 1

                Return M
            End Function

            ''' <summary>
            ''' Creates a box with the given size centered at origin.
            ''' </summary>
            ''' <param name="Size">
            ''' Size of the box.
            ''' </param>
            ''' <param name="ColourTop">
            ''' Colour of the top face of the box.
            ''' </param>
            ''' <param name="ColourBottom">
            ''' Colour of the bottom face of the box.
            ''' </param>
            ''' <param name="DensityX">
            ''' Number of sub-divisions in the X-dimension.
            ''' </param>
            ''' <param name="DensityY">
            ''' Number of sub-divisions in the Y-dimension.
            ''' </param>
            ''' <param name="DensityZ">
            ''' Number of sub-divisions in the Z-dimension.
            ''' </param>
            ''' <param name="ReverseFaces">
            ''' Whether the plane's normal is +ve Z or -ve Z axis.
            ''' </param>
            ''' <param name="RemovePrevData">
            ''' <c>True</c> to remove previous data.
            ''' </param>
            Public Shared Function CreateBox(ByVal Size As Vector3,
                                             ByVal ColourTop As ColorValue,
                                             ByVal ColourBottom As ColorValue,
                                             Optional ByVal DensityX As Integer = 1,
                                             Optional ByVal DensityY As Integer = 1,
                                             Optional ByVal DensityZ As Integer = 1,
                                             Optional ByVal ReverseFaces As Boolean = False,
                                             Optional ByVal RemovePrevData As Boolean = False) As BasicMesh

                Dim M, M2, M3, M4, M5, M6 As BasicMesh

                ' Check mesh extents.
                If (Size.X <= 0) OrElse (Size.Y <= 0) OrElse (Size.Z <= 0) Then _
                    Throw New ArgumentException("Invalid extents.") _
                        : Exit Function

                ' Check mesh density.
                If (DensityX <= 0) OrElse (DensityY <= 0) OrElse (DensityZ <= 0) Then _
                    Throw New ArgumentException("Invalid density.") _
                        : Exit Function

                ' Create the -X plane.
                M = CreatePlane(1, 1, 1, DensityZ, DensityY, ColourTop, ColourBottom, ReverseFaces := Not ReverseFaces)
                M.Transform(Matrix.RotationY(PI/2)*Matrix.Translation(- 0.5F, 0, 0))

                ' Create the +X plane.
                M2 = CreatePlane(1, 1, 1, DensityZ, DensityY, ColourTop, ColourBottom, ReverseFaces := ReverseFaces)
                M2.Transform(Matrix.RotationY(PI/2)*Matrix.Translation(0.5F, 0, 0))

                ' Create the -Y plane.
                M3 = CreatePlane(1, 1, 1, DensityX, DensityZ, ColourBottom, ColourBottom, ReverseFaces := ReverseFaces)
                M3.Transform(Matrix.RotationX(PI/2)*Matrix.Translation(0, - 0.5F, 0))

                ' Create the +Y plane.
                M4 = CreatePlane(1, 1, 1, DensityX, DensityZ, ColourTop, ColourTop, ReverseFaces := Not ReverseFaces)
                M4.Transform(Matrix.RotationX(PI/2)*Matrix.Translation(0, 0.5F, 0))

                ' Create the -Z plane.
                M5 = CreatePlane(1, 1, 1, DensityX, DensityY, ColourTop, ColourBottom, ReverseFaces := Not ReverseFaces)
                M5.Translation(0, 0, - 0.5F)

                ' Create the +Z plane.
                M6 = CreatePlane(1, 1, 1, DensityX, DensityY, ColourTop, ColourBottom, ReverseFaces := ReverseFaces)
                M6.Translation(0, 0, 0.5F)

                ' Make a single part from all the parts that were created.
                M.Append(M2)
                M.Append(M3)
                M.Append(M4)
                M.Append(M5)
                M.Append(M6)
                M.MergeAll()

                ' Remove the temporary parts.
                M2 = Nothing
                M3 = Nothing
                M4 = Nothing
                M5 = Nothing
                M6 = Nothing

                ' Resize the cube to proper size.
                M.Scaling(Size)

                Return M
            End Function

            ''' <summary>
            ''' Creates a prism whose axis is parallel to Z axis passing through origin
            ''' (with circumscribing circle's radius).
            ''' </summary>
            ''' <param name="AxisLength">
            ''' Length of axis.
            ''' </param>
            ''' <param name="CircumRadiusFront">
            ''' Radius of the circumscribing circle at the front.
            ''' </param>
            ''' <param name="CircumRadiusBack">
            ''' Radius of the circumscribing circle at the back.
            ''' </param>
            ''' <param name="NumStacks">
            ''' Number of stack (this decides the density of the rectangular faces).
            ''' </param>
            ''' <param name="NumRectSides">
            ''' Number of rectangular sides.
            ''' </param>
            ''' <param name="ColourFront">
            ''' Colour at the front.
            ''' </param>
            ''' <param name="ColourBack">
            ''' Colour at the back.
            ''' </param>
            ''' <param name="AngleOffsetInDegrees">
            ''' Starting angle, from +ve X axis, in degrees.
            ''' </param>
            ''' <param name="AngleSizeInDegrees">
            ''' Size of the angle, measured in anti-clockwise direction, in degrees.
            ''' </param>
            ''' <param name="ClosedFront">
            ''' Whether the front side is closed or not.
            ''' </param>
            ''' <param name="ClosedBack">
            ''' Whether the back side is closed or not.
            ''' </param>
            ''' <param name="ClosedShape">
            ''' If angle is not full 360 degrees, then whether the shape is closed or not
            ''' from the remaining angle by a rectangle.
            ''' </param>
            ''' <param name="CloseShapeDensity">
            ''' Density of the closer rectangle if <c>CloseShape</c> is true.
            ''' </param>
            ''' <param name="ReverseFaces">
            ''' Whether the plane's normal is +ve Z or -ve Z axis.
            ''' </param>
            Public Shared Function CreatePrism(ByVal AxisLength As Single,
                                               ByVal CircumRadiusFront As Single, ByVal CircumRadiusBack As Single,
                                               ByVal NumStacks As Integer, ByVal NumRectSides As Integer,
                                               ByVal ColourFront As ColorValue,
                                               ByVal ColourBack As ColorValue,
                                               Optional ByVal AngleOffsetInDegrees As Single = 0,
                                               Optional ByVal AngleSizeInDegrees As Single = 360,
                                               Optional ByVal ClosedFront As Boolean = True,
                                               Optional ByVal ClosedBack As Boolean = True,
                                               Optional ByVal ClosedShape As Boolean = True,
                                               Optional ByVal CloseShapeDensity As Integer = 1,
                                               Optional ByVal ReverseFaces As Boolean = False) As BasicMesh

                Dim M, M1, M2, M3, M4 As BasicMesh

                ' Check inputs.
                ' -------------
                ' Check inputs.
                If (AxisLength <= 0) Or (CircumRadiusFront < 0) Or (CircumRadiusBack < 0) Then _
                    Throw New ArgumentException("Invalid mesh extents.") _
                        : Exit Function

                ' Check inputs.
                If (CircumRadiusBack < CircumRadiusFront) Then _
                    Return CreatePrism(AxisLength, CircumRadiusBack, CircumRadiusFront,
                                       NumStacks, NumRectSides, ColourFront, ColourBack,
                                       AngleOffsetInDegrees, AngleSizeInDegrees,
                                       ClosedFront, ClosedBack, ClosedShape, CloseShapeDensity,
                                       ReverseFaces)

                ' Check inputs.
                If (NumStacks < 1) Or (NumRectSides < 3) Then _
                    Throw New ArgumentException("Invalid Stack\Rect Size count (must be atleast 1 or 3 respectively).") _
                        : Exit Function

                ' Check inputs.
                If (AngleSizeInDegrees <= 0) OrElse (AngleSizeInDegrees > 360) Then _
                    Throw New ArgumentException("Invalid angle size.") _
                        : Exit Function

                ' Check inputs.
                If CloseShapeDensity < 1 Then _
                    Throw New ArgumentException("Invalid 'CloseShapeDensity'.") _
                        : Exit Function

                ' Calculate some data.
                ' --------------------
                ' Calculate the angle subtended by the curved side w.r.t. the axis.
                Dim SemiVerticalAngle As Single = Atan((CircumRadiusBack - CircumRadiusFront)/AxisLength)

                ' Calculate angles.
                Dim AngleOffset As Single = AngleOffsetInDegrees*PI/180
                Dim AngleSize As Single = AngleSizeInDegrees*PI/180

                ' Do we need to close this shape? Yes if it does not span a 360 degrees and we're supposed to close it.
                Dim NeedToCloseShape As Boolean = ClosedShape And (AngleSizeInDegrees < 360)

                ' Similarly, front-side and back-side need to be closed if their radii are non-zero.
                Dim NeedToCloseFront As Boolean = ClosedFront And (CircumRadiusFront > 0)
                Dim NeedToCloseBack As Boolean = ClosedBack And (CircumRadiusBack > 0)

                ' Create the two corners of the cylinder.
                ' ---------------------------------------
                ' Front side closer.
                If NeedToCloseFront Then _
                    M1 = CreatePolygon(CircumRadiusFront, NumRectSides, ColourFront, ColourFront,
                                       AngleOffsetInDegrees, AngleSizeInDegrees, ReverseFaces) _
                        : M1.Translation(New Vector3(0, 0, AxisLength/2)) _
                    Else _
                    M1 = Nothing

                ' Back side closer.
                If NeedToCloseBack Then _
                    M2 = CreatePolygon(CircumRadiusBack, NumRectSides, ColourBack, ColourBack,
                                       AngleOffsetInDegrees, AngleSizeInDegrees, Not ReverseFaces) _
                        : M2.Translation(0, 0, - AxisLength/2) _
                    Else _
                    M2 = Nothing

                ' Create 2 rects to close this shape.
                ' -----------------------------------
                If NeedToCloseShape Then
                    ' Create the starting plane.
                    M3 = CreatePlane(CircumRadiusFront, CircumRadiusBack, AxisLength,
                                     CloseShapeDensity, NumStacks, ColourFront, ColourBack,
                                     True, False, True, ReverseFaces)

                    M3.Transform((Matrix.Translation(CircumRadiusBack/2, 0, 0)*
                                  Matrix.RotationX(PI/2)*
                                  Matrix.RotationZ(AngleOffset)))

                    ' Create the ending plane.
                    M4 = CreatePlane(CircumRadiusBack, CircumRadiusFront, AxisLength,
                                     CloseShapeDensity, NumStacks, ColourBack, ColourFront,
                                     True, False, True, ReverseFaces)

                    M4.Transform((Matrix.Translation(CircumRadiusBack/2, 0, 0)*
                                  Matrix.RotationX(3*PI/2)*
                                  Matrix.RotationZ(AngleOffset + AngleSize)))

                Else ' If NeedToCloseShape Then
                    M3 = Nothing
                    M4 = Nothing

                End If ' If NeedToCloseShape Then

                ' Create a plane (the main surface of the prism).
                ' -----------------------------------------------
                M = CreatePlane(1, 1, 1, NumRectSides, NumStacks, ColourFront, ColourBack,
                                False, False, True, ReverseFaces)

                M.Rotation(PI/2, 0, 0)

                ' Convert it into a cylinder by wrapping the X co-ordinate
                ' to (X, Y) values.
                ' --------------------------------------------------------
                With M.Part(0).Vertices
                    For I As Integer = 0 To .Count - 1
                        Dim V As Vertex = .Vertex(I)

                        ' Calculate angle to rotate about Z axis (to get a curved layer).
                        Dim Theta As Single = AngleOffset + (V.Position.X + 0.5F)*AngleSize

                        ' Calculate the effective radius of the ring we're at now
                        ' (by blending the Z co-ordinate using the max length).
                        Dim EffRadius As Single = CircumRadiusFront +
                                                  (0.5F - V.Position.Z)*(CircumRadiusBack - CircumRadiusFront)

                        ' Calculate X, Y, Z
                        V.Position.X = Cos(Theta)*EffRadius
                        V.Position.Y = Sin(Theta)*EffRadius
                        V.Position.Z = AxisLength*V.Position.Z

                        ' Set normal.
                        V.Normal.X = Cos(SemiVerticalAngle)*V.Position.X
                        V.Normal.Y = Cos(SemiVerticalAngle)*V.Position.Y
                        V.Normal.Z = Sin(SemiVerticalAngle)*EffRadius

                        ' Reverse normals if needed.
                        If ReverseFaces Then _
                            V.Normal = - V.Normal

                        ' Normalize the normal.
                        V.Normal.Normalize()

                        ' Set the vertex.
                        .Vertex(I) = V

                    Next I ' For I As Integer = 0 To .Count - 1
                End With ' With M.Part(0).Vertices

                ' Finally, join the parts.
                ' ------------------------
                M.Append(M4)
                M.Append(M3)
                M.Append(M2)
                M.Append(M1)
                M.MergeAll()

                ' Free the extra parts.
                ' ---------------------
                M1 = Nothing
                M2 = Nothing
                M3 = Nothing
                M4 = Nothing

                ' If this is a conical or pyramidal surface, then calculate normals.
                ' ------------------------------------------------------------------
                If (CircumRadiusFront <> CircumRadiusBack) Then _
                    M.CalculateNormals()

                Return M
            End Function

            ' --- Utility Math Functions --- '
            Private Const PI As Single = CSng(Math.PI)

            Private Shared Function Sin(ByVal d As Single) As Single
                Return CSng(Math.Sin(d))
            End Function

            Private Shared Function Cos(ByVal d As Single) As Single
                Return CSng(Math.Cos(d))
            End Function

            Private Shared Function Atan(ByVal d As Single) As Single
                Return CSng(Math.Atan(d))
            End Function
        End Class
    End Class
End Namespace
