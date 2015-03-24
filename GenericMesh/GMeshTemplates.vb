Imports GenericMesh.Exceptions
Imports GenericMesh.Standard.BasicMesh

Partial Class GBasicMesh (Of VertexType As {Structure, IVertex},
    IndiceType As {Structure, IConvertible},
    MaterialType As {Structure, IMaterial})

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Creates a plane with Z-axis as it's normal.
    ''' </summary>
    ''' <param name="SizeX">
    ''' Size of the plane in X-dimension.
    ''' </param>
    ''' <param name="SizeY">
    ''' Size of the plane in Y-dimension.
    ''' </param>
    ''' <param name="SubdivisionX">
    ''' Number of sub-divisions in X-dimension.
    ''' </param>
    ''' <param name="SubdivisionY">
    ''' Number of sub-divisions in Y-dimension.
    ''' </param>
    ''' <param name="DoubleSided">
    ''' Whether the plane is double sided or not.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter to write vertex data.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter to add indice data.
    ''' </param>
    ''' <remarks>
    ''' To make the plane double-sided, the plane is duplicated and it's faces
    ''' are reversed.
    ''' </remarks>
    Public Sub CreatePlane(ByVal SizeX As Single, ByVal SizeY As Single,
                           Optional ByVal SubdivisionX As Integer = 4,
                           Optional ByVal SubdivisionY As Integer = 4,
                           Optional ByVal DoubleSided As Boolean = False,
                           Optional ByVal _VC As Converter(Of Standard.Vertex, VertexType) = Nothing,
                           Optional ByVal _IC As Converter(Of Integer, IndiceType) = Nothing)

        Dim Col As New ColorValue(1.0F, 1.0F, 1.0F)

        ' Create the front-side plane.
        CreatePlane(SizeX, SizeY, SubdivisionX, SubdivisionY, Col, Col, False, True, _VC, _IC)

        ' Create the back-side plane, if needed, and then join it.
        If DoubleSided Then _
            CreatePlane(SizeX, SizeY, SubdivisionX, SubdivisionY, Col, Col, True, False, _VC, _IC) _
                : MergeAll()
    End Sub

    ''' <summary>
    ''' Creates a plane with Z-axis as it's normal.
    ''' </summary>
    ''' <param name="SizeX">
    ''' Size of the plane in X-dimension.
    ''' </param>
    ''' <param name="SizeY">
    ''' Size of the plane in Y-dimension.
    ''' </param>
    ''' <param name="SubdivisionX">
    ''' Number of sub-divisions in X-dimension.
    ''' </param>
    ''' <param name="SubdivisionY">
    ''' Number of sub-divisions in Y-dimension.
    ''' </param>
    ''' <param name="ColourMin">
    ''' Colour on the top edge (Y = 0).
    ''' </param>
    ''' <param name="ColourMax">
    ''' Colour on the bottom edge (Y = <c>Size.Y</c>).
    ''' </param>
    ''' <param name="ReverseFaces">
    ''' Whether the plane's normal is +ve Z or -ve Z axis.
    ''' </param>
    ''' <param name="RemovePrevData">
    ''' <c>True</c> to remove previous data.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter to write vertex data.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter to add indice data.
    ''' </param>
    ''' <remarks>
    ''' (Y = 0) or (Y = <c>SizeY</c>) actually refers to relative position
    ''' and not absolute position (in space).
    ''' </remarks>
    Public Sub CreatePlane(ByVal SizeX As Single, ByVal SizeY As Single,
                           ByVal SubdivisionX As Integer, ByVal SubdivisionY As Integer,
                           ByVal ColourMin As ColorValue, ByVal ColourMax As ColorValue,
                           Optional ByVal ReverseFaces As Boolean = False,
                           Optional ByVal RemovePrevData As Boolean = False,
                           Optional ByVal _VC As Converter(Of Standard.Vertex, VertexType) = Nothing,
                           Optional ByVal _IC As Converter(Of Integer, IndiceType) = Nothing)

        ' Delegate to subroutine.
        CreatePlane(SizeX,
                    SizeX,
                    SizeY,
                    SubdivisionX,
                    SubdivisionY,
                    ColourMin,
                    ColourMax,
                    False,
                    False,
                    True,
                    ReverseFaces,
                    RemovePrevData,
                    _VC,
                    _IC)
    End Sub

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
    ''' </param> ''' 
    ''' <param name="AlignLeft">
    ''' Whether the top part is aligned to the left.
    ''' </param>
    ''' <param name="AlignRight">
    ''' Whether the top part is aligned to the right.
    ''' </param>
    ''' <param name="ColourBlendY">
    ''' <c>True</c> if colour is blended using Y co-ordinate, otherwise
    ''' colour is blended using X co-ordinate.
    ''' </param>
    ''' <param name="ReverseFaces">
    ''' Whether the plane's normal is +ve Z or -ve Z axis.
    ''' </param>
    ''' <param name="RemovePrevData">
    ''' <c>True</c> to remove previous data.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter to write vertex data.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter to add indice data.
    ''' </param>
    ''' <remarks>
    ''' (X = 0) or (X = <c>SizeX</c>) or (Y = 0) or (Y = <c>SizeY</c>)
    ''' actually refers to relative position and not absolute position
    ''' (in space).
    ''' </remarks>
    Public Sub CreatePlane(ByVal SizeXUp As Single, ByVal SizeXDown As Single, ByVal SizeY As Single,
                           ByVal SubdivisionX As Integer, ByVal SubdivisionY As Integer,
                           ByVal ColourUp As ColorValue, ByVal ColourDown As ColorValue,
                           Optional ByVal AlignLeft As Boolean = False,
                           Optional ByVal AlignRight As Boolean = False,
                           Optional ByVal ColourBlendY As Boolean = True,
                           Optional ByVal ReverseFaces As Boolean = False,
                           Optional ByVal RemovePrevData As Boolean = False,
                           Optional ByVal _VC As Converter(Of Standard.Vertex, VertexType) = Nothing,
                           Optional ByVal _IC As Converter(Of Integer, IndiceType) = Nothing)

        Dim M As Standard.BasicMesh

        ' Create the mesh from standard template.
        M = MeshTemplates.CreatePlane(SizeXUp, SizeXDown, SizeY,
                                      SubdivisionX, SubdivisionY,
                                      ColourUp, ColourDown,
                                      AlignLeft, AlignRight,
                                      ColourBlendY, ReverseFaces)

        ' Remove previous data if needed.
        If RemovePrevData Then _
            RemoveAll()

        ' Append the mesh.
        Append(M, _VC, _IC)

        ' Remove the mesh.
        M = Nothing
    End Sub

    ''' <summary>
    ''' Creates a polygon on the XY plane centered at origin.
    ''' </summary>
    ''' <param name="EdgeLength">
    ''' Length of each edge.
    ''' </param>
    ''' <param name="EdgeCount">
    ''' Number of edges.
    ''' </param>
    ''' <param name="DoubleSided">
    ''' Whether the polygon is double sided or not.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter to write vertex data.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter to add indice data.
    ''' </param>
    Public Sub CreatePolygon(ByVal EdgeLength As Single, ByVal EdgeCount As Integer,
                             Optional ByVal DoubleSided As Boolean = False,
                             Optional ByVal _VC As Converter(Of Standard.Vertex, VertexType) = Nothing,
                             Optional ByVal _IC As Converter(Of Integer, IndiceType) = Nothing)

        Dim Col As New ColorValue(1.0F, 1.0F, 1.0F)

        ' Create the first polygon.
        CreatePolygon(EdgeLength, EdgeCount, Col, Col, 0, 360, False, True)

        ' Create the back-side and merge if needed.
        If DoubleSided Then _
            CreatePolygon(EdgeLength, EdgeCount, Col, Col, 0, 360, True, False) _
                : MergeAll()
    End Sub

    ''' <summary>
    ''' Creates a polygon on the XY plane centered at origin.
    ''' </summary>
    ''' <param name="EdgeLength">
    ''' Length of each edge.
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
    ''' <param name="RemovePrevData">
    ''' <c>True</c> to remove previous data.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter to write vertex data.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter to add indice data.
    ''' </param>
    Public Sub CreatePolygon(ByVal EdgeLength As Single, ByVal EdgeCount As Integer,
                             ByVal ColourCenter As ColorValue, ByVal ColourRim As ColorValue,
                             Optional ByVal AngleOffsetInDegrees As Single = 0,
                             Optional ByVal AngleSizeInDegrees As Single = 360,
                             Optional ByVal ReverseFaces As Boolean = False,
                             Optional ByVal RemovePrevData As Boolean = False,
                             Optional ByVal _VC As Converter(Of Standard.Vertex, VertexType) = Nothing,
                             Optional ByVal _IC As Converter(Of Integer, IndiceType) = Nothing)

        ' Check inputs.
        If EdgeCount < 3 Then _
            Throw New ArgumentException("Invalid edge count.") _
                : Exit Sub

        ' Create the polygon.
        CreatePolygonA(EdgeLength/(2.0F*CSng(Math.Sin(Math.PI/EdgeCount))),
                       EdgeCount,
                       ColourCenter,
                       ColourRim,
                       AngleOffsetInDegrees,
                       AngleSizeInDegrees,
                       ReverseFaces,
                       RemovePrevData)
    End Sub

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
    ''' <param name="RemovePrevData">
    ''' <c>True</c> to remove previous data.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter to write vertex data.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter to add indice data.
    ''' </param>
    Public Sub CreatePolygonA(ByVal CircumRadius As Single, ByVal EdgeCount As Integer,
                              ByVal ColourCenter As ColorValue, ByVal ColourRim As ColorValue,
                              Optional ByVal AngleOffsetInDegrees As Single = 0,
                              Optional ByVal AngleSizeInDegrees As Single = 360,
                              Optional ByVal ReverseFaces As Boolean = False,
                              Optional ByVal RemovePrevData As Boolean = False,
                              Optional ByVal _VC As Converter(Of Standard.Vertex, VertexType) = Nothing,
                              Optional ByVal _IC As Converter(Of Integer, IndiceType) = Nothing)

        Dim M As Standard.BasicMesh

        ' Create the mesh from standard template.
        M = MeshTemplates.CreatePolygon(CircumRadius, EdgeCount, ColourCenter, ColourRim,
                                        AngleOffsetInDegrees, AngleSizeInDegrees, ReverseFaces)

        ' Remove previous data if needed.
        If RemovePrevData Then _
            RemoveAll()

        ' Append the mesh.
        Append(M, _VC, _IC)

        ' Remove the mesh.
        M = Nothing
    End Sub

    ''' <summary>
    ''' Creates a sphere of given radius centered at origin.
    ''' </summary>
    ''' <param name="Radius">
    ''' Radius of the sphere.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter to write vertex data.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter to add indice data.
    ''' </param>
    Public Sub CreateSphere(ByVal Radius As Single,
                            Optional ByVal _VC As Converter(Of Standard.Vertex, VertexType) = Nothing,
                            Optional ByVal _IC As Converter(Of Integer, IndiceType) = Nothing)

        Dim Col As New ColorValue(1.0F, 1.0F, 1.0F)

        ' Create the sphere.
        CreateSphere(Radius, Col, Col, 8, 8, False, True)
    End Sub

    ''' <summary>
    ''' Creates a sphere of given radius centered at origin.
    ''' </summary>
    ''' <param name="Radius">
    ''' Radius of the sphere.
    ''' </param>
    ''' <param name="ColourTop">
    ''' Colour of the sphere (top-most vertex).
    ''' </param>
    ''' <param name="ColourBottom">
    ''' Colour of the sphere (bottom-most vertex).
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
    ''' <param name="RemovePrevData">
    ''' <c>True</c> to remove previous data.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter to write vertex data.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter to add indice data.
    ''' </param>
    Public Sub CreateSphere(ByVal Radius As Single,
                            ByVal ColourTop As ColorValue,
                            ByVal ColourBottom As ColorValue,
                            Optional ByVal NumStacks As Integer = 10,
                            Optional ByVal NumSlices As Integer = 10,
                            Optional ByVal ReverseFaces As Boolean = False,
                            Optional ByVal RemovePrevData As Boolean = False,
                            Optional ByVal _VC As Converter(Of Standard.Vertex, VertexType) = Nothing,
                            Optional ByVal _IC As Converter(Of Integer, IndiceType) = Nothing)

        Dim M As Standard.BasicMesh

        ' Create the mesh from standard template.
        M = MeshTemplates.CreateSphere(Radius, ColourTop, ColourBottom,
                                       NumSlices, NumSlices, ReverseFaces)

        ' Remove previous data if needed.
        If RemovePrevData Then _
            RemoveAll()

        ' Append the mesh.
        Append(M, _VC, _IC)

        ' Remove the mesh.
        M = Nothing
    End Sub

    ''' <summary>
    ''' Creates a box with the given size centered at origin.
    ''' </summary>
    ''' <param name="Size">
    ''' Size of the box.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter to write vertex data.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter to add indice data.
    ''' </param>
    Public Sub CreateBox(ByVal Size As Vector3,
                         Optional ByVal _VC As Converter(Of Standard.Vertex, VertexType) = Nothing,
                         Optional ByVal _IC As Converter(Of Integer, IndiceType) = Nothing)

        Dim Col As New ColorValue(1.0F, 1.0F, 1.0F)

        ' Create the box.
        CreateBox(Size, Col, Col, 1, 1, 1, False, True, _VC, _IC)
    End Sub

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
    ''' <param name="_VC">
    ''' Vertex converter to write vertex data.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter to add indice data.
    ''' </param>
    Public Sub CreateBox(ByRef Size As Vector3,
                         ByVal ColourTop As ColorValue,
                         ByVal ColourBottom As ColorValue,
                         Optional ByVal DensityX As Integer = 1,
                         Optional ByVal DensityY As Integer = 1,
                         Optional ByVal DensityZ As Integer = 1,
                         Optional ByVal ReverseFaces As Boolean = False,
                         Optional ByVal RemovePrevData As Boolean = False,
                         Optional ByVal _VC As Converter(Of Standard.Vertex, VertexType) = Nothing,
                         Optional ByVal _IC As Converter(Of Integer, IndiceType) = Nothing)

        Dim M As Standard.BasicMesh

        ' Create the mesh from standard template.
        M = MeshTemplates.CreateBox(Size, ColourTop, ColourBottom,
                                    DensityX, DensityY, DensityZ, ReverseFaces)

        ' Remove previous data if needed.
        If RemovePrevData Then _
            RemoveAll()

        ' Append the mesh.
        Append(M, _VC, _IC)

        ' Remove the mesh.
        M = Nothing
    End Sub

    ''' <summary>
    ''' Creates a prism whose axis is parallel to Z axis passing through origin.
    ''' </summary>
    ''' <param name="AxisLength">
    ''' Length of axis.
    ''' </param>
    ''' <param name="EdgeLength">
    ''' Length of an edge of top\bottom polygon.
    ''' </param>
    ''' <param name="NumRectSides">
    ''' Number of rectangular sides.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter to write vertex data.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter to add indice data.
    ''' </param>
    ''' <param name="_VT">
    ''' Vertex Transformer.
    ''' </param>
    Public Sub CreatePrism(ByVal AxisLength As Single, ByVal EdgeLength As Single,
                           ByVal NumRectSides As Integer,
                           Optional ByVal _VC As Converter(Of Standard.Vertex, VertexType) = Nothing,
                           Optional ByVal _IC As Converter(Of Integer, IndiceType) = Nothing,
                           Optional ByVal _VT As Func(Of VertexType, Matrix, VertexType) = Nothing)

        Dim Col As New ColorValue(1.0F, 1.0F, 1.0F)

        ' Create a new prism.
        CreatePrism(AxisLength, EdgeLength, 4, NumRectSides, Col, Col,
                    _VC := _VC, _IC := _IC, _VT := _VT)
    End Sub

    ''' <summary>
    ''' Creates a prism whose axis is parallel to Z axis passing through origin.
    ''' </summary>
    ''' <param name="AxisLength">
    ''' Length of axis.
    ''' </param>
    ''' <param name="EdgeLength">
    ''' Length of an edge of front\back polygon.
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
    ''' <param name="RemovePrevData">
    ''' <c>True</c> to remove previous data.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter to write vertex data.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter to add indice data.
    ''' </param>
    ''' <param name="_VT">
    ''' Vertex transformer.
    ''' </param>
    Public Sub CreatePrism(ByVal AxisLength As Single, ByVal EdgeLength As Single,
                           ByVal NumStacks As Integer, ByVal NumRectSides As Integer,
                           ByVal ColourFront As ColorValue,
                           ByVal ColourBack As ColorValue,
                           Optional ByVal AngleOffsetInDegrees As Single = 0,
                           Optional ByVal AngleSizeInDegrees As Single = 360,
                           Optional ByVal ClosedFront As Boolean = True,
                           Optional ByVal ClosedBack As Boolean = True,
                           Optional ByVal ClosedShape As Boolean = True,
                           Optional ByVal CloseShapeDensity As Integer = 1,
                           Optional ByVal ReverseFaces As Boolean = False,
                           Optional ByVal RemovePrevData As Boolean = True,
                           Optional ByVal _VC As Converter(Of Standard.Vertex, VertexType) = Nothing,
                           Optional ByVal _IC As Converter(Of Integer, IndiceType) = Nothing,
                           Optional ByVal _VT As Func(Of VertexType, Matrix, VertexType) = Nothing)

        ' Check inputs.
        If NumRectSides < 3 Then _
            Throw New ArgumentException("Invalid edge count.") _
                : Exit Sub

        ' Get the radius of the circumscribing circle.
        EdgeLength = EdgeLength/(2.0F*CSng(Math.Sin(Math.PI/NumRectSides)))

        ' Now call the other function.
        CreatePrismA(AxisLength,
                     EdgeLength,
                     EdgeLength,
                     NumStacks,
                     NumRectSides,
                     ColourFront,
                     ColourBack,
                     AngleOffsetInDegrees,
                     AngleSizeInDegrees,
                     ClosedFront,
                     ClosedBack,
                     ClosedShape,
                     CloseShapeDensity,
                     ReverseFaces,
                     RemovePrevData,
                     _VC,
                     _IC,
                     _VT)
    End Sub

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
    ''' <param name="RemovePrevData">
    ''' <c>True</c> to remove previous data.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter to write vertex data.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter to add indice data.
    ''' </param>
    ''' <param name="_VT">
    ''' Vertex transformer.
    ''' </param>
    Public Sub CreatePrismA(ByVal AxisLength As Single,
                            ByVal CircumRadiusFront As Single, ByVal CircumRadiusBack As Single,
                            ByVal NumStacks As Integer, ByVal NumRectSides As Integer,
                            ByVal ColourFront As ColorValue, ByVal ColourBack As ColorValue,
                            Optional ByVal AngleOffsetInDegrees As Single = 0,
                            Optional ByVal AngleSizeInDegrees As Single = 360,
                            Optional ByVal ClosedFront As Boolean = True,
                            Optional ByVal ClosedBack As Boolean = True,
                            Optional ByVal ClosedShape As Boolean = True,
                            Optional ByVal CloseShapeDensity As Integer = 1,
                            Optional ByVal ReverseFaces As Boolean = False,
                            Optional ByVal RemovePrevData As Boolean = True,
                            Optional ByVal _VC As Converter(Of Standard.Vertex, VertexType) = Nothing,
                            Optional ByVal _IC As Converter(Of Integer, IndiceType) = Nothing,
                            Optional ByVal _VT As Func(Of VertexType, Matrix, VertexType) = Nothing)

        Dim M As Standard.BasicMesh

        ' Create the mesh from standard template.
        M = MeshTemplates.CreatePrism(AxisLength, CircumRadiusFront, CircumRadiusBack,
                                      NumStacks, NumRectSides, ColourFront, ColourBack,
                                      AngleOffsetInDegrees, AngleSizeInDegrees,
                                      ClosedFront, ClosedBack, ClosedShape,
                                      CloseShapeDensity, ReverseFaces)

        ' Remove previous data if needed.
        If RemovePrevData Then _
            RemoveAll()

        ' Append the mesh.
        Append(M, _VC, _IC)

        ' Remove the mesh.
        M = Nothing
    End Sub

    ''' <summary>
    ''' Creates a pyramid whose axis is parallel to Z axis passing through origin.
    ''' </summary>
    ''' <param name="AxisLength">
    ''' Length of axis.
    ''' </param>
    ''' <param name="EdgeLength">
    ''' Length of an edge of top\bottom polygon.
    ''' </param>
    ''' <param name="NumRectSides">
    ''' Number of rectangular sides.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter to write vertex data.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter to add indice data.
    ''' </param>
    ''' <param name="_VT">
    ''' Vertex Transformer.
    ''' </param>
    Public Sub CreatePyramid(ByVal AxisLength As Single, ByVal EdgeLength As Single,
                             ByVal NumRectSides As Integer,
                             Optional ByVal _VC As Converter(Of Standard.Vertex, VertexType) = Nothing,
                             Optional ByVal _IC As Converter(Of Integer, IndiceType) = Nothing,
                             Optional ByVal _VT As Func(Of VertexType, Matrix, VertexType) = Nothing)

        Dim Col As New ColorValue(1.0F, 1.0F, 1.0F)

        ' Create a new pyramid.
        CreatePyramid(AxisLength, EdgeLength, 4, NumRectSides, Col, Col,
                      _VC := _VC, _IC := _IC, _VT := _VT)
    End Sub

    ''' <summary>
    ''' Creates a pyramid whose axis is parallel to Z axis passing through origin.
    ''' </summary>
    ''' <param name="AxisLength">
    ''' Length of axis.
    ''' </param>
    ''' <param name="EdgeLength">
    ''' Length of an edge of front\back polygon.
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
    ''' <param name="RemovePrevData">
    ''' <c>True</c> to remove previous data.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter to write vertex data.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter to add indice data.
    ''' </param>
    ''' <param name="_VT">
    ''' Vertex transformer.
    ''' </param>
    Public Sub CreatePyramid(ByVal AxisLength As Single, ByVal EdgeLength As Single,
                             ByVal NumStacks As Integer, ByVal NumRectSides As Integer,
                             ByVal ColourFront As ColorValue,
                             ByVal ColourBack As ColorValue,
                             Optional ByVal AngleOffsetInDegrees As Single = 0,
                             Optional ByVal AngleSizeInDegrees As Single = 360,
                             Optional ByVal ClosedFront As Boolean = True,
                             Optional ByVal ClosedBack As Boolean = True,
                             Optional ByVal ClosedShape As Boolean = True,
                             Optional ByVal CloseShapeDensity As Integer = 1,
                             Optional ByVal ReverseFaces As Boolean = False,
                             Optional ByVal RemovePrevData As Boolean = True,
                             Optional ByVal _VC As Converter(Of Standard.Vertex, VertexType) = Nothing,
                             Optional ByVal _IC As Converter(Of Integer, IndiceType) = Nothing,
                             Optional ByVal _VT As Func(Of VertexType, Matrix, VertexType) = Nothing)

        ' Check inputs.
        If NumRectSides < 3 Then _
            Throw New ArgumentException("Invalid edge count.") _
                : Exit Sub

        ' Get the radius of the circumscribing circle.
        EdgeLength = EdgeLength/(2.0F*CSng(Math.Sin(Math.PI/NumRectSides)))

        ' Now call the other function.
        CreatePrismA(AxisLength,
                     0,
                     EdgeLength,
                     NumStacks,
                     NumRectSides,
                     ColourFront,
                     ColourBack,
                     AngleOffsetInDegrees,
                     AngleSizeInDegrees,
                     ClosedFront,
                     ClosedBack,
                     ClosedShape,
                     CloseShapeDensity,
                     ReverseFaces,
                     RemovePrevData,
                     _VC,
                     _IC,
                     _VT)
    End Sub

    ''' <summary>
    ''' Creates a three colour (Red for X, Green for Y, Blue for Z) crosshair
    ''' with a small triangle pointing towards the positive for each axis.
    ''' </summary>
    ''' <param name="RemovePrevData">
    ''' <c>True</c> to remove previous data.
    ''' </param>
    ''' <param name="_VC">
    ''' Vertex converter to write vertex data.
    ''' </param>
    ''' <param name="_IC">
    ''' Indice converter to add indice data.
    ''' </param>
    ''' <param name="_VT">
    ''' Vertex transformer.
    ''' </param>
    Public Sub CreateThreeColourCrosshair(Optional ByVal RemovePrevData As Boolean = True,
                                          Optional ByVal _VC As Converter(Of Standard.Vertex, VertexType) = Nothing,
                                          Optional ByVal _IC As Converter(Of Integer, IndiceType) = Nothing,
                                          Optional ByVal _VT As Func(Of VertexType, Matrix, VertexType) = Nothing)
        ' Colours.
        Dim red As New Direct3D.ColorValue(255, 0, 0),
            green As New Direct3D.ColorValue(0, 255, 0),
            blue As New Direct3D.ColorValue(0, 0, 255)

        ' Remove old parts if needed.
        If RemovePrevData Then _
            Me.RemoveAll()

        ' Create the X bar.
        CreatePrism(1, 0.1, 1, 4, red, red, ClosedFront := False, RemovePrevData := False, _VC := _VC, _IC := _IC,
                    _VT := _VT)
        m_Parts(m_Parts.Count - 1).Transform(Matrix.RotationZ(Math.PI/4.0F)*Matrix.RotationY(Math.PI/2.0F), _VT)

        ' Create the +X cap.
        CreatePyramid(0.2, 0.1, 1, 4, red, red, ClosedBack := False, RemovePrevData := False, _VC := _VC, _IC := _IC,
                      _VT := _VT)
        m_Parts(m_Parts.Count - 1).Transform(
            Matrix.Translation(0, 0, 0.6)*Matrix.RotationZ(Math.PI/4.0F)*Matrix.RotationY(Math.PI/2.0F), _VT)

        ' Create the Y bar.
        CreatePrism(1, 0.1, 1, 4, green, green, ClosedFront := False, RemovePrevData := False, _VC := _VC, _IC := _IC,
                    _VT := _VT)
        m_Parts(m_Parts.Count - 1).Transform(Matrix.RotationZ(Math.PI/4.0F)*Matrix.RotationX(- Math.PI/2.0F), _VT)

        ' Create the +Y cap.
        CreatePyramid(0.2, 0.1, 1, 4, green, green, ClosedBack := False, RemovePrevData := False, _VC := _VC, _IC := _IC,
                      _VT := _VT)
        m_Parts(m_Parts.Count - 1).Transform(
            Matrix.Translation(0, 0, 0.6)*Matrix.RotationZ(Math.PI/4.0F)*Matrix.RotationX(- Math.PI/2.0F), _VT)

        ' Create the Z bar.
        CreatePrism(1, 0.1, 1, 4, blue, blue, ClosedFront := False, RemovePrevData := False, _VC := _VC, _IC := _IC,
                    _VT := _VT)
        m_Parts(m_Parts.Count - 1).Transform(Matrix.RotationZ(Math.PI/4.0F), _VT)

        ' Create the +Z cap
        CreatePyramid(0.2, 0.1, 1, 4, blue, blue, ClosedBack := False, RemovePrevData := False, _VC := _VC, _IC := _IC,
                      _VT := _VT)
        m_Parts(m_Parts.Count - 1).Transform(Matrix.Translation(0, 0, 0.6)*Matrix.RotationZ(Math.PI/4.0F), _VT)

        ' Merge the last six parts.
        MergeRange(m_Parts.Count - 6, m_Parts.Count - 1)

        ' Calculate normals.
        m_Parts(m_Parts.Count - 1).CalculateNormals()
    End Sub
End Class
