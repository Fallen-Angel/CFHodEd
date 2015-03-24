Imports GenericMesh
Imports GenericMesh.VertexFields
Imports Microsoft.DirectX.PrivateImplementationDetails

''' <summary>
''' Structure representing Homeworld2 Basic Mesh Vertex.
''' </summary>
Public Structure BasicVertex
    Implements IVertex
    Implements IVertexTransformable

    Implements IVertexPosition3
    Implements IVertexNormal3
    Implements IVertexDiffuse
    Implements IVertexTex2

    ' --------------
    ' Class Members.
    ' --------------
    ''' <summary>Position.</summary>
    Public Position As Vector3

    ''' <summary>Normal.</summary>
    Public Normal As Vector3

    ''' <summary>Diffuse colour.</summary>
    Public Diffuse As Int32

    ''' <summary>Texture co-ordinates.</summary>
    Public Tex As Vector2

    Public Tex1 As Vector2
    Public Tex2 As Vector2

    ''' <summary>Tangent.</summary>
    Public Tangent As Vector3

    ''' <summary>Binormal.</summary>
    Public Binormal As Vector3

    ''' <summary>W component of position.</summary>
    Private PositionW As Single

    ''' <summary>W component of normal.</summary>
    Private NormalW As Single

    Private Version As UInteger

    ' -----------------
    ' Class properties.
    ' -----------------
    ''' <summary>
    ''' FVF (Flexible Vertex Format) of the vertex.
    ''' </summary>
    ''' <value>
    ''' FVF (Flexible Vertex Format) of the vertex.
    ''' </value>
    ''' <returns>
    ''' Returns the FVF (Flexible Vertex Format) of the vertex.
    ''' </returns>
    Public ReadOnly Property Format() As Direct3D.VertexFormats Implements IVertex.Format
        Get
            Return Direct3D.VertexFormats.Position Or
                   Direct3D.VertexFormats.Normal Or
                   Direct3D.VertexFormats.Diffuse Or
                   Direct3D.VertexFormats.Texture3
        End Get
    End Property

    ''' <summary>
    ''' Size of vertex.
    ''' </summary>
    ''' <value>
    ''' Size of vertex.
    ''' </value>
    ''' <remarks>
    ''' Returns the size of vertex.
    ''' </remarks>
    Public ReadOnly Property VertexSize() As Integer Implements GenericMesh.IVertex.VertexSize
        Get
            Return 88
        End Get
    End Property

    ' -----------------
    ' Member Functions.
    ' -----------------
    ''' <summary>
    ''' Sets the default values for non-zero fields.
    ''' </summary>
    Friend Sub Initialize() Implements IVertex.Initialize
        Position = New Vector3(0, 0, 0)
        Normal = New Vector3(0, 0, 1)
        Diffuse = - 1
        Tex = New Vector2(0, 0)
        Tangent = New Vector3(0, 0, 0)
        Binormal = New Vector3(0, 0, 0)
        PositionW = 1
        NormalW = 1
    End Sub

    ''' <summary>
    ''' Returns whether another vertex is equivalent to this instance
    ''' or not.
    ''' </summary>
    ''' <param name="other">
    ''' The vertex which is to be compared.
    ''' </param>
    ''' <returns>
    ''' <c>True</c> or <c>False</c>, depending on whether the vertices
    ''' are equivalent or not.
    ''' </returns>
    ''' <exception cref="ArgumentException">
    ''' <c>other</c> is not the same type as this instance.
    ''' </exception>
    Private Function Equal(ByVal other As IVertex) As Boolean Implements System.IEquatable(Of IVertex).Equals
        ' Check if 'other' is of same type.
        If Not TypeOf other Is BasicVertex Then _
            Throw New ArgumentException("Object must be of type " & TypeName(Me).ToString & ".") _
                : Exit Function

        ' Get the vertex.
        Dim V As BasicVertex = CType(other, BasicVertex)

        ' Compare.
        If (V.Position <> Position) OrElse
           (V.Normal <> Normal) OrElse
           (V.Diffuse <> Diffuse) OrElse
           (V.Tex <> Tex) OrElse
           (V.Tangent <> Tangent) OrElse
           (V.Binormal <> Binormal) OrElse
           (V.PositionW <> PositionW) OrElse
           (V.NormalW <> NormalW) Then _
            Return False _
            Else _
            Return True
    End Function

    ''' <summary>
    ''' Function to transform the vertex.
    ''' </summary>
    ''' <param name="M">
    ''' The transforming matrix.
    ''' </param>
    ''' <returns>
    ''' Modified vertex.
    ''' </returns>
    Friend Function Transform(ByVal M As Matrix) As IVertex Implements IVertexTransformable.Transform
        Position.TransformCoordinate(M)
        Normal.TransformNormal(M)
        Tangent.TransformNormal(M)
        Binormal.TransformNormal(M)

        Return Me
    End Function

    ''' <summary>
    ''' Retrieves 3D co-ordinates of the vertex.
    ''' </summary>
    Friend Function GetPosition3() As Vector3 Implements IVertexPosition3.GetPosition3
        Return Position
    End Function

    ''' <summary>
    ''' Sets 3D co-ordinates of the vertex.
    ''' </summary>
    ''' <param name="V">
    ''' The co-ordinates.
    ''' </param>
    ''' <returns>
    ''' The modified vertex.
    ''' </returns>
    Friend Function SetPosition3(ByVal V As Vector3) As IVertex Implements IVertexPosition3.SetPosition3
        Position = V
        Return Me
    End Function

    ''' <summary>
    ''' Retrieves normal of the vertex.
    ''' </summary>
    Friend Function GetNormal3() As Vector3 Implements IVertexNormal3.GetNormal3
        Return Normal
    End Function

    ''' <summary>
    ''' Sets normal of the vertex.
    ''' </summary>
    ''' <param name="V">
    ''' The normal.
    ''' </param>
    ''' <returns>
    ''' The modified vertex.
    ''' </returns>
    Friend Function SetNormal3(ByVal V As Vector3) As IVertex Implements IVertexNormal3.SetNormal3
        Normal = V
        Return Me
    End Function

    ''' <summary>
    ''' Retrieves diffuse colour of the vertex.
    ''' </summary>
    Friend Function GetDiffuse() As Direct3D.ColorValue Implements IVertexDiffuse.GetDiffuse
        Return Direct3D.ColorValue.FromArgb(Diffuse)
    End Function

    ''' <summary>
    ''' Sets diffuse colour of the vertex.
    ''' </summary>
    ''' <param name="V">
    ''' The colour.
    ''' </param>
    ''' <returns>
    ''' The modified vertex.
    ''' </returns>
    Friend Function SetDiffuse(ByVal V As Direct3D.ColorValue) As IVertex Implements IVertexDiffuse.SetDiffuse
        Diffuse = V.ToArgb()
        Return Me
    End Function

    ''' <summary>
    ''' Retrieves texture co-ordinates.
    ''' </summary>
    ''' <param name="Index">
    ''' The texture co-ordinate set to retrieve.
    ''' </param>
    ''' <remarks>
    ''' All index values point to the same set.
    ''' </remarks>
    Friend Function GetTexCoords2(Optional ByVal Index As Integer = 0) As Vector2 Implements IVertexTex2.GetTexCoords2
        Return Tex
    End Function

    ''' <summary>
    ''' Sets texture co-ordinates.
    ''' </summary>
    ''' <param name="V">
    ''' The texture co-ordinates.
    ''' </param>
    ''' <param name="Index">
    ''' The texture co-ordinate set to set.
    ''' </param>
    ''' <returns>
    ''' The modified vertex.
    ''' </returns>
    ''' <remarks>
    ''' All index values point to the same set.
    ''' </remarks>
    Friend Function SetTexCoords2(ByVal V As Vector2, Optional ByVal Index As Integer = 0) As IVertex _
        Implements IVertexTex2.SetTexCoords2
        Tex = V
        Return Me
    End Function

    ''' <summary>
    ''' Retrieves tangent.
    ''' </summary>
    Friend Function GetTangent() As Vector3
        Return Tangent
    End Function

    ''' <summary>
    ''' Sets tangent.
    ''' </summary>
    ''' <param name="V">
    ''' The tangent.
    ''' </param>
    ''' <returns>
    ''' The modified vertex.
    ''' </returns>
    Friend Function SetTangent(ByVal V As Vector3) As IVertex
        Tangent = V
        Return Me
    End Function

    ''' <summary>
    ''' Retrieves binormal.
    ''' </summary>
    Friend Function GetBinormal() As Vector3
        Return Binormal
    End Function

    ''' <summary>
    ''' Sets binormal.
    ''' </summary>
    ''' <param name="V">
    ''' The binormal.
    ''' </param>
    ''' <returns>
    ''' The modified vertex.
    ''' </returns>
    Friend Function SetBinormal(ByVal V As Vector3) As IVertex
        Binormal = V
        Return Me
    End Function

    ''' <summary>
    ''' Reads the vertex from an IFF file.
    ''' </summary>
    ''' <param name="IFF">
    ''' The IFF reader to read from.
    ''' </param>
    ''' <param name="VertexMasks">
    ''' The vertex fields to read.
    ''' </param>
    Friend Shared Function ReadIFF(ByVal IFF As IFF.IFFReader, ByVal VertexMasks As VertexMasks,
                                   ByVal Version As UInteger) As BasicVertex
        Dim V As BasicVertex

        With V
            ' Initialize the vertex to account for missing fields.
            .Initialize()

            .Version = Version

            If (VertexMasks And VertexMasks.Position) <> 0 Then _
                .Position.X = IFF.ReadSingle() _
                    : .Position.Y = IFF.ReadSingle() _
                    : .Position.Z = IFF.ReadSingle() _
                    : .PositionW = IFF.ReadSingle()

            If (VertexMasks And VertexMasks.Normal) <> 0 Then _
                .Normal.X = IFF.ReadSingle() _
                    : .Normal.Y = IFF.ReadSingle() _
                    : .Normal.Z = IFF.ReadSingle() _
                    : .NormalW = IFF.ReadSingle()

            If (VertexMasks And VertexMasks.Colour) <> 0 Then _
                .Diffuse = __swap1(IFF.ReadInt32())

            If (VertexMasks And VertexMasks.Texture0) <> 0 Then _
                .Tex.X = IFF.ReadSingle() _
                    : .Tex.Y = IFF.ReadSingle()

            If (.Version = 1401) Then
                If (VertexMasks And VertexMasks.Texture1) <> 0 Then _
                    .Tex1.X = IFF.ReadSingle() _
                        : .Tex1.Y = IFF.ReadSingle()

                If (VertexMasks And VertexMasks.Texture2) <> 0 Then _
                    .Tex2.X = IFF.ReadSingle() _
                        : .Tex2.Y = IFF.ReadSingle()
            End If

            If (VertexMasks And VertexMasks.Tangent) <> 0 Then _
                .Tangent.X = IFF.ReadSingle() _
                    : .Tangent.Y = IFF.ReadSingle() _
                    : .Tangent.Z = IFF.ReadSingle()

            If (VertexMasks And VertexMasks.Binormal) <> 0 Then _
                .Binormal.X = IFF.ReadSingle() _
                    : .Binormal.Y = IFF.ReadSingle() _
                    : .Binormal.Z = IFF.ReadSingle()


        End With


        Return V
    End Function

    ''' <summary>
    ''' Writes the vertex to an IFF file.
    ''' </summary>
    ''' <param name="IFF">
    ''' The IFF writer to write to.
    ''' </param>
    ''' <param name="VertexMasks">
    ''' The vertex fields to write.
    ''' </param>
    Friend Sub WriteIFF(ByVal IFF As IFF.IFFWriter, ByVal VertexMasks As VertexMasks)
        If (VertexMasks And VertexMasks.Position) <> 0 Then _
            IFF.Write(Position.X) _
                : IFF.Write(Position.Y) _
                : IFF.Write(Position.Z) _
                : IFF.Write(PositionW)

        If (VertexMasks And VertexMasks.Normal) <> 0 Then _
            IFF.Write(Normal.X) _
                : IFF.Write(Normal.Y) _
                : IFF.Write(Normal.Z) _
                : IFF.Write(NormalW)

        If (VertexMasks And VertexMasks.Colour) <> 0 Then _
            IFF.WriteInt32(__swap2(Diffuse))

        If (VertexMasks And VertexMasks.Texture0) <> 0 Then _
            IFF.Write(Tex.X) _
                : IFF.Write(Tex.Y)

        If (Version = 1401) Then
            If (VertexMasks And VertexMasks.Texture1) <> 0 Then _
                IFF.Write(Tex1.X) _
                    : IFF.Write(Tex1.Y)

            If (VertexMasks And VertexMasks.Texture2) <> 0 Then _
                IFF.Write(Tex2.X) _
                    : IFF.Write(Tex2.Y)
        End If

        If (VertexMasks And VertexMasks.Tangent) <> 0 Then _
            IFF.Write(Tangent.X) _
                : IFF.Write(Tangent.Y) _
                : IFF.Write(Tangent.Z)

        If (VertexMasks And VertexMasks.Binormal) <> 0 Then _
            IFF.Write(Binormal.X) _
                : IFF.Write(Binormal.Y) _
                : IFF.Write(Binormal.Z)
    End Sub

    ''' <summary>
    ''' Colour converter (HW2 -> D3D)
    ''' </summary>
    Private Shared Function __swap1(ByVal v As Int32) As Int32
        Dim c As Direct3D.ColorValue = Direct3D.ColorValue.FromArgb(v)
        Return New Direct3D.ColorValue(c.Alpha, c.Red, c.Green, c.Blue).ToArgb()
    End Function

    ''' <summary>
    ''' Colour converter (D3D -> HW2).
    ''' </summary>
    Private Shared Function __swap2(ByVal v As Int32) As Int32
        Dim c As Direct3D.ColorValue = Direct3D.ColorValue.FromArgb(v)
        Return New Direct3D.ColorValue(c.Green, c.Blue, c.Alpha, c.Red).ToArgb()
    End Function
End Structure
