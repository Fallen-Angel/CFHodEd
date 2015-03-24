Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D

Namespace WavefrontObjectHelper
    ''' <summary>
    ''' Class representing the data in the wavefront object file.
    ''' </summary>
    ''' <remarks>
    ''' For reading data.
    ''' </remarks>
    Friend Class ObjFileDataStore
        ''' <summary>Position</summary>
        Private _P As List(Of Vector3)

        ''' <summary>Normal</summary>
        Private _N As List(Of Vector3)

        ''' <summary>UV co-ordinates</summary>
        Private _UV As List(Of Vector2)

        ''' <summary>Colour data</summary>
        Private _Col As List(Of ColorValue)

        ''' <summary>Indice group</summary>
        Private _Ind As List(Of IndiceGroup)

        ''' <summary>Material attributes</summary>
        Private _Mat As List(Of Standard.Material)

        ''' <summary>Class constructor.</summary>
        Public Sub New()
            _P = New List(Of Vector3)
            _N = New List(Of Vector3)
            _UV = New List(Of Vector2)
            _Col = New List(Of ColorValue)
            _Ind = New List(Of IndiceGroup)
            _Mat = New List(Of Standard.Material)
        End Sub

        ''' <summary>Class destructor.</summary>
        Protected Overrides Sub Finalize()
            _P = Nothing
            _N = Nothing
            _UV = Nothing
            _Col = Nothing
            _Ind = Nothing
            _Mat = Nothing

            MyBase.Finalize()
        End Sub

        ''' <summary>Position</summary>
        Public ReadOnly Property P() As List(Of Vector3)
            Get
                Return _P
            End Get
        End Property

        ''' <summary>Normal</summary>
        Public ReadOnly Property N() As List(Of Vector3)
            Get
                Return _N
            End Get
        End Property

        ''' <summary>UV co-ordinates</summary>
        Public ReadOnly Property UV() As List(Of Vector2)
            Get
                Return _UV
            End Get
        End Property

        ''' <summary>Colour data</summary>
        Public ReadOnly Property Col() As List(Of ColorValue)
            Get
                Return _Col
            End Get
        End Property

        ''' <summary>Indice group</summary>
        Public ReadOnly Property Ind() As List(Of IndiceGroup)
            Get
                Return _Ind
            End Get
        End Property

        ''' <summary>Material attributes</summary>
        Public ReadOnly Property Mat() As List(Of Standard.Material)
            Get
                Return _Mat
            End Get
        End Property

        ''' <summary>
        ''' Removes all data from this instance.
        ''' </summary>
        Public Sub Clear()
            _P.Clear()
            _N.Clear()
            _UV.Clear()
            _Col.Clear()
            _Ind.Clear()
            _Mat.Clear()
        End Sub

        ''' <summary>
        ''' Reads a single line and processes it.
        ''' </summary>
        ''' <param name="F">
        ''' The file stream from which line is to be read.
        ''' </param>
        Private Function ReadLine(ByVal F As System.IO.StreamReader,
                                  ByRef NewLine As Boolean,
                                  ByRef S1 As String) As String
            Dim CurrLine As String = ""

            ' Read the current line.
            CurrLine = F.ReadLine()

            ' Remove all tabs and spaces.
            CurrLine = Trim(Replace(CurrLine, vbTab, " "))

            ' Skip empy lines (independent lines only).
            ' If this is an empty line, and a continuation, then it implies 
            ' line continuation ends here.
            If (CurrLine.Length = 0) Then _
                If NewLine Then _
                    Return "" _
                    Else _
                    CurrLine = S1 _
                        : NewLine = True

            ' If this is a comment (on a new, independent line) then ignore it.
            ' The same previous logic applies here.
            If Left(CurrLine, 1) = "#" Then _
                If NewLine Then _
                    Return "" _
                    Else _
                    CurrLine = S1 _
                        : NewLine = True

            ' Remove multiple spaces.
            Do While InStr(CurrLine, "  ") <> 0
                CurrLine = CurrLine.Replace("  ", " ")

            Loop ' Do While InStr(CurrLine, "  ") <> 0

            ' If this is a continuation of older line(s) then
            ' concatenate them.
            If Not NewLine Then _
                NewLine = True _
                    : CurrLine = S1 & CurrLine

            ' It has been observed that continuations sometimes cause problems.
            CurrLine = CurrLine.Trim()

            ' Remove all spaces.
            Do While InStr(CurrLine, "  ") <> 0
                CurrLine = CurrLine.Replace("  ", " ")

            Loop ' Do While InStr(CurrLine, "  ") <> 0

            ' Do we have something to do?
            If CurrLine.Length = 0 Then _
                Return ""

            ' If this line has a continuation to next line, then
            ' store this line (removing first the slash, and then
            ' the extra space(s) that may be present), and set the
            ' flag. Skip to read next line.
            If Right(CurrLine, 1) = "\" Then _
                NewLine = False _
                    : S1 = Trim(Left(CurrLine, CurrLine.Length - 1)) & " " _
                    : Return ""

            Return CurrLine
        End Function

        ''' <summary>
        ''' Reads a wavefront object material file.
        ''' </summary>
        ''' <param name="Filename">
        ''' The name of the file to read.
        ''' </param>
        ''' <exception cref="Exception">
        ''' Thrown when file cannot be opened due to some reason.
        ''' </exception>
        ''' <exception cref="ArgumentNullException">
        ''' Thrown when <c>Filename Is Nothing</c> or <c>Filename = ""</c>.
        ''' </exception>
        Private Sub ReadMtlFile(ByVal Filename As String)
            Dim F As System.IO.StreamReader

            ' ---------------------
            ' Check input filename.
            ' ---------------------
            If (Filename Is Nothing) Or (Filename = "") Then _
                Throw New ArgumentNullException("Filename") _
                    : Exit Sub

            ' -------------------------
            ' Attempt to open the file.
            ' -------------------------
            Try
                ' Try to open the file.
                F = New IO.StreamReader(Filename)

            Catch ex As Exception
                ' Throw the exception.
                Throw New Exception("File coudn't be opened: '" & Filename & "'.", ex)
                Exit Sub

            End Try

            ' ---------------------------------------------
            Dim NewLine As Boolean
            Dim CurrMtl As Integer, CurrLine As String = ""
            Dim S1 As String = "", SA1() As String
            ' ---------------------------------------------

            ' Read the material file, line by line.
            Do Until F.EndOfStream
                ' Read the current line.
                CurrLine = ReadLine(F, NewLine, S1)

                ' Do we need to skip it?
                If CurrLine.Length = 0 Then _
                    Continue Do

                ' Split the string.
                SA1 = Split(CurrLine, " ")

                ' Check Line.
                If SA1.Length = 0 Then _
                    Trace.TraceWarning("Skipping line: '" & CurrLine & "'") _
                        : Continue Do

                ' Convert to lower case (all comparisions are performed in lower case).
                SA1(0) = SA1(0).ToLower()

                Select Case SA1(0)
                    ' ------------
                    ' NEW MATERIAL
                    ' ------------
                    Case "newmtl"
                        ' Expecting a minimum of 1 parameter.
                        If UBound(SA1) < 1 Then _
                            Trace.TraceWarning("Skipping line: """ & CurrLine & """") _
                                : Continue Do

                        ' Clear the first element, we don't want this
                        ' to appear in the material name.
                        SA1(0) = ""

                        ' Acquire full name (may get broken due to spaces)
                        S1 = Join(SA1, " ")

                        ' Try to locate the material.
                        For I As Integer = 0 To _Mat.Count - 1
                            If _Mat(I).MaterialName = S1 Then _
                                CurrMtl = I _
                                    : Continue Do

                        Next I ' For I As Integer = 0 To _Mat.Count - 1

                        ' No such material.
                        CurrMtl = - 1

                        ' ----------------
                        ' ILLUMINATION (?)
                        ' ----------------
                    Case "illum"
                        ' Nothing to do here.

                        ' -------
                        ' DIFFUSE
                        ' -------
                    Case "kd"
                        Dim R, G, B, A As Single

                        ' Expecting 3 or 4 parameters.
                        If (UBound(SA1) <> 3) AndAlso (UBound(SA1) <> 4) Then _
                            Trace.TraceWarning("Skipping line: '" & CurrLine & "'") _
                                : Continue Do

                        R = _StrToSng(SA1(1))
                        G = _StrToSng(SA1(2))
                        B = _StrToSng(SA1(3))

                        If UBound(SA1) = 4 Then _
                            A = _StrToSng(SA1(4)) _
                            Else _
                            A = 1.0F

                        ' Set material attributes.
                        If CurrMtl <> - 1 Then _
                            _Mat(CurrMtl).SetDiffuse(New ColorValue(R, G, B, A))

                        ' AMBIENT
                    Case "ka"
                        Dim R, G, B, A As Single

                        ' Expecting 3 or 4 parameters.
                        If (UBound(SA1) <> 3) AndAlso (UBound(SA1) <> 4) Then _
                            Trace.TraceWarning("Skipping line: '" & CurrLine & "'") _
                                : Continue Do

                        R = _StrToSng(SA1(1))
                        G = _StrToSng(SA1(2))
                        B = _StrToSng(SA1(3))

                        If UBound(SA1) = 4 Then _
                            A = _StrToSng(SA1(4)) _
                            Else _
                            A = 1.0F

                        ' Set material attributes.
                        If CurrMtl <> - 1 Then _
                            _Mat(CurrMtl).SetAmbient(New ColorValue(R, G, B, A))

                        ' SPECULAR
                    Case "ks"
                        Dim R, G, B, A As Single

                        ' Expecting 3 or 4 parameters.
                        If (UBound(SA1) <> 3) AndAlso (UBound(SA1) <> 4) Then _
                            Trace.TraceWarning("Skipping line: '" & CurrLine & "'") _
                                : Continue Do

                        R = _StrToSng(SA1(1))
                        G = _StrToSng(SA1(2))
                        B = _StrToSng(SA1(3))

                        If UBound(SA1) = 4 Then _
                            A = _StrToSng(SA1(4)) _
                            Else _
                            A = 1.0F

                        ' Set material attributes.
                        If CurrMtl <> - 1 Then _
                            _Mat(CurrMtl).SetSpecular(New ColorValue(R, G, B, A))

                        ' --------
                        ' EMISSIVE
                        ' --------
                    Case "ke"
                        Dim R, G, B, A As Single

                        ' Expecting 3 or 4 parameters.
                        If (UBound(SA1) <> 3) AndAlso (UBound(SA1) <> 4) Then _
                            Trace.TraceWarning("Skipping line: '" & CurrLine & "'") _
                                : Continue Do

                        R = _StrToSng(SA1(1))
                        G = _StrToSng(SA1(2))
                        B = _StrToSng(SA1(3))

                        If UBound(SA1) = 4 Then _
                            A = _StrToSng(SA1(4)) _
                            Else _
                            A = 1.0F

                        ' Set material attributes.
                        If CurrMtl <> - 1 Then _
                            _Mat(CurrMtl).SetEmissive(New ColorValue(R, G, B, A))

                        ' ---------
                        ' POWER (?)
                        ' ---------
                    Case "ns"
                        Dim SS As Single

                        ' Expecting exactly 1 parameter.
                        If UBound(SA1) <> 1 Then _
                            Trace.TraceWarning("Skipping line: '" & CurrLine & "'") _
                                : Continue Do

                        SS = _StrToSng(SA1(1))

                        '' Read the member.
                        'If IsNumeric(SA1(1)) Then _
                        ' SS = CSng(SA1(1)) _
                        'Else _
                        ' SS = CSng(Val(SA1(1)))

                        ' Set the member.
                        If CurrMtl <> - 1 Then _
                            _Mat(CurrMtl).SetSpecularSharpness(SS)

                        ' ---------------
                        ' DIFFUSE TEXTURE
                        ' ---------------
                    Case "map_kd"
                        ' Expecting a minimum of 1 parameter.
                        If UBound(SA1) < 1 Then _
                            Trace.TraceWarning("Skipping line: '" & CurrLine & "'") _
                                : Continue Do

                        ' Clear the first element, we don't want this
                        ' to appear in the material name.
                        SA1(0) = ""

                        ' Acquire full name (may get broken due to spaces)
                        S1 = Join(SA1, " ")

                        ' Cache the material's texture name.
                        If CurrMtl <> - 1 Then _
                            _Mat(CurrMtl).SetTextureName(S1)

                End Select ' Select Case SA1(0)
            Loop ' Do Until F.EndOfStream

            ' Close the file.
            F.Close()
        End Sub

        ''' <summary>
        ''' Reads a wavefront object file.
        ''' </summary>
        ''' <param name="Filename">
        ''' The name of file to read.
        ''' </param>
        ''' <param name="ReadMtlFileIfPresent">
        ''' This overrides the default behaviour of reading the object file
        ''' if it's present. Materials are determined using names in the .obj
        ''' file, and attributes are read from .mtl file, if present. Otherwise,
        ''' default attributes are used.
        ''' </param>
        ''' <exception cref="Exception">
        ''' Thrown when file cannot be opened due to some reason.
        ''' </exception>
        ''' <exception cref="ArgumentNullException">
        ''' Thrown when <c>Filename Is Nothing</c> or <c>Filename = ""</c>.
        ''' </exception>
        Public Sub ReadFile(ByVal Filename As String, Optional ByVal ReadMtlFileIfPresent As Boolean = True)
            Dim F As System.IO.StreamReader

            ' ---------------------
            ' Check input filename.
            ' ---------------------
            If (Filename Is Nothing) Or (Filename = "") Then _
                Throw New ArgumentNullException("Filename") _
                    : Exit Sub

            ' -------------------------
            ' Attempt to open the file.
            ' -------------------------
            Try
                ' Try to open the file.
                F = My.Computer.FileSystem.OpenTextFileReader(Filename)

            Catch ex As Exception
                ' Throw the exception.
                Throw New Exception("File coudn't be opened: '" & Filename & "'.", ex)
                Exit Sub

            End Try

            ' ------------------------------------------------
            Dim NewLine As Boolean, MtlFilename As String = ""
            Dim CurrMtl As Integer, CurrLine As String = ""
            Dim S1 As String = "", SA1(), SA2() As String
            ' ------------------------------------------------

            ' ----------------------------------------
            ' Clear this instance; add default values.
            ' ----------------------------------------
            ' Clear this instance.
            Clear()

            ' Add default data.
            _P.Add(New Vector3(0, 0, 0))
            _N.Add(New Vector3(0, 0, 0))
            _UV.Add(New Vector2(0, 0))
            _Col.Add(New ColorValue(255, 255, 255, 255))
            _Mat.Add(New Standard.Material("___default___"))

            ' --------------------
            ' Read the whole file.
            ' --------------------
            Do Until F.EndOfStream
                ' Read the current line.
                CurrLine = ReadLine(F, NewLine, S1)

                ' Do we need to skip it?
                If CurrLine.Length = 0 Then _
                    Continue Do

                ' Split the string.
                SA1 = Split(CurrLine, " ")

                ' Check Line.
                If SA1.Length = 0 Then _
                    Trace.TraceWarning("Skipping line: '" & CurrLine & "'") _
                        : Continue Do

                ' Convert to lower case (all comparisions are performed in lower case).
                SA1(0) = SA1(0).ToLower()

                ' Check the type of field.
                Select Case SA1(0)
                    ' ---------------------
                    ' MATERIAL LIBRARY NAME
                    ' ---------------------
                    Case "mtllib"
                        ' Expecting a minimum of 1 parameter.
                        If UBound(SA1) < 1 Then _
                            Trace.TraceWarning("Skipping line: '" & CurrLine & "'") _
                                : Continue Do

                        ' Clear the first element, we don't want this
                        ' to appear in the material name.
                        SA1(0) = ""

                        ' Acquire full name (may get broken due to spaces)
                        MtlFilename = Join(SA1, " ").Trim()

                        ' -------------
                        ' MATERIAL NAME
                        ' -------------
                    Case "usemtl"
                        Dim I As Integer

                        ' Expecting a minimum of 1 parameter.
                        If UBound(SA1) < 1 Then _
                            Trace.TraceWarning("Skipping line: """ & CurrLine & """") _
                                : Continue Do

                        ' Clear the first element, we don't want this
                        ' to appear in the material name.
                        SA1(0) = ""

                        ' Acquire full name (may get broken due to spaces)
                        S1 = Join(SA1, " ").Trim()

                        ' Locate whether an existing material of the same name
                        ' exists already. Then set the active index.
                        For I = 1 To _Mat.Count - 1
                            If StrComp(_Mat(I).MaterialName, S1, CompareMethod.Text) = 0 Then _
                                Exit For

                        Next I ' For I = 1 To _Mat.Count - 1

                        ' If the material is absent, then add it.
                        If I = _Mat.Count Then _
                            _Mat.Add(New Standard.Material(S1))

                        ' Set the active material
                        CurrMtl = I

                        ' ----------
                        ' GROUP NAME
                        ' ----------
                    Case "g"
                        ' Nothing to do here.

                        ' -----------
                        ' OBJECT NAME
                        ' -----------
                    Case "o"
                        ' Nothing to do here.

                        ' ---------------
                        ' SMOOTHING GROUP
                        ' ---------------
                    Case "s"
                        ' Nothing to do here.

                        ' --------
                        ' POSITION
                        ' --------
                    Case "v"
                        Dim X, Y, Z As Single

                        ' Expecting 2-4 parameters (XY, XYZ, XYZW).
                        If (UBound(SA1) < 2) OrElse (UBound(SA1) > 4) Then _
                            Trace.TraceWarning("Skipping line: '" & CurrLine & "'") _
                                : Continue Do

                        ' Read the X, Y, Z values.
                        X = _StrToSng(SA1(1))
                        Y = _StrToSng(SA1(2))

                        If UBound(SA1) >= 3 Then _
                            Z = _StrToSng(SA1(3))

                        ' W
                        ' (discarded at present)

                        ' Add the position vector.
                        _P.Add(New Vector3(X, Y, Z))

                        ' --------------------
                        ' TEXTURE CO-ORDINATES
                        ' --------------------
                    Case "vt"
                        Dim X, Y As Single

                        ' Expecting 2 to 4 parameters (UV).
                        If (UBound(SA1) < 2) OrElse (UBound(SA1) > 4) Then _
                            Trace.TraceWarning("Skipping line: '" & CurrLine & "'") _
                                : Continue Do

                        ' Read the X, Y, Z values.
                        X = _StrToSng(SA1(1))
                        Y = _StrToSng(SA1(2))

                        ' Add the UV co-ordinate.
                        _UV.Add(New Vector2(X, Y))

                        ' -------
                        ' NORMALS
                        ' -------
                    Case "vn"
                        Dim X, Y, Z As Single

                        ' Expecting 2-4 parameters (XY, XYZ, XYZW).
                        If (UBound(SA1) < 3) OrElse (UBound(SA1) > 4) Then _
                            Trace.TraceWarning("Skipping line: '" & CurrLine & "'") _
                                : Continue Do

                        ' Read the X, Y, Z values.
                        X = _StrToSng(SA1(1))
                        Y = _StrToSng(SA1(2))

                        If UBound(SA1) >= 3 Then _
                            Z = _StrToSng(SA1(3))

                        ' W
                        ' (discarded at present)

                        ' Add the position vector.
                        _N.Add(New Vector3(X, Y, Z))

                        ' -------
                        ' COLOURS
                        ' -------
                    Case "vc"
                        Dim I, J, K, L As Integer

                        ' Expecting 3 or 4 parameters (RGB, RGBA).
                        If (UBound(SA1) <> 3) AndAlso (UBound(SA1) <> 4) Then _
                            Trace.TraceWarning("Skipping line: '" & CurrLine & "'") _
                                : Continue Do

                        ' Read the red, green, blue and alpha values (if present).
                        ' R
                        If IsNumeric(SA1(1)) Then _
                            I = CInt(SA1(1)) _
                            Else _
                            I = CInt(Val(SA1(1)))

                        ' G
                        If IsNumeric(SA1(2)) Then _
                            J = CInt(SA1(2)) _
                            Else _
                            J = CInt(Val(SA1(2)))

                        ' B
                        If IsNumeric(SA1(3)) Then _
                            K = CInt(SA1(3)) _
                            Else _
                            K = CInt(Val(SA1(3)))

                        ' A (if present)
                        If UBound(SA1) = 4 Then _
                            If IsNumeric(SA1(4)) Then _
                                L = CInt(SA1(4)) _
                                Else _
                                L = CInt(Val(SA1(4))) _
                            Else _
                            L = 255

                        ' Clamp the colour values.
                        I = Math.Min(255, Math.Max(0, I))
                        J = Math.Min(255, Math.Max(0, J))
                        K = Math.Min(255, Math.Max(0, K))
                        L = Math.Min(255, Math.Max(0, L))

                        ' Add the entry.
                        _Col.Add(New ColorValue(I, J, K, L))

                        ' ----
                        ' FACE
                        ' ----
                    Case "f"
                        Dim IG As New IndiceGroup

                        ' Expecting a minimum of 1 parameter.
                        If UBound(SA1) < 1 Then _
                            Trace.TraceWarning("Skipping line: '" & CurrLine & "'") _
                                : Continue Do

                        ' Read the indice group.
                        With IG
                            .Count = UBound(SA1)
                            .Material = CurrMtl

                            ' Read all parts.
                            For I As Integer = 0 To .Count - 1
                                Dim IV As IndiceValues

                                ' Split indice values.
                                SA2 = Split(SA1(I + 1), "/")

                                With IV
                                    ' Is position data present?
                                    If UBound(SA2) >= 0 Then _
                                        If IsNumeric(SA2(0)) Then _
                                            .Position = CInt(SA2(0)) _
                                            Else _
                                            .Position = CInt(Val(SA2(0)))

                                    ' Support for relative indexing:
                                    If .Position < 0 Then _
                                        .Position += _P.Count + 1

                                    ' Is UV data present?
                                    If UBound(SA2) >= 1 Then _
                                        If IsNumeric(SA2(1)) Then _
                                            .UV = CInt(SA2(1)) _
                                            Else _
                                            .UV = CInt(Val(SA2(1)))

                                    ' Support for relative indexing:
                                    If .Normal < 0 Then _
                                        .Normal += _N.Count + 1

                                    ' Is normal data present?
                                    If UBound(SA2) >= 2 Then _
                                        If IsNumeric(SA2(2)) Then _
                                            .Normal = CInt(SA2(2)) _
                                            Else _
                                            .Normal = CInt(Val(SA2(2)))

                                    ' Support for relative indexing:
                                    If .UV < 0 Then _
                                        .UV += _UV.Count + 1

                                    ' Is colour data present?
                                    If UBound(SA2) >= 3 Then _
                                        If IsNumeric(SA2(3)) Then _
                                            .Colour = CInt(SA2(3)) _
                                            Else _
                                            .Colour = CInt(Val(SA2(3))) _
                                        Else _
                                        If _Col.Count = 1 Then _
                                            .Colour = 0 _
                                            Else _
                                            .Colour = .Position

                                    ' Support for relative indexing:
                                    If .Colour < 0 Then _
                                        .Colour += _Col.Count + 1

                                    ' NOTE: For any data that is not assigned, gets 
                                    ' default values (0, ...) by default. Those assigned, get
                                    ' proper values since the wavefront object files have 
                                    ' 1-base indices.
                                End With ' With IV

                                ' Set the indice value.
                                IG.Indices(I) = IV

                            Next I ' For I As Integer = 0 To .Count - 1
                        End With ' With IG

                        ' Add the indice.
                        _Ind.Add(IG)

                        ' -------
                        ' UNKNOWN
                        ' -------
                    Case Else
                        Trace.TraceWarning("Skipping unknown statement: """ & CurrLine & """")

                End Select ' Select Case SA1(0)
            Loop ' Do Until F.EndOfStream

            ' Close stream.
            F.Close()
            F.Dispose()

            ' ------------------------------------
            ' Read the material file, as required.
            ' ------------------------------------
            ' Do we have to read the material file, and do we also have the 
            ' path to the material file?
            If (ReadMtlFileIfPresent) AndAlso (MtlFilename.Length <> 0) Then
                ' First get the current directory.
                Dim OldCurrDir As String = IO.Directory.GetCurrentDirectory()

                ' Try to change the current directory, and then open the file.
                Try
                    ' Set the current working directory to that of the material
                    ' file.
                    IO.Directory.SetCurrentDirectory(IO.Path.GetDirectoryName(Filename))

                    ' Now read the material file.
                    ReadMtlFile(IO.Path.GetFullPath(MtlFilename))

                Catch _ex As Exception
                    ' No can do.
                    Throw New Exception("Cannot open material file: '" & MtlFilename & "'.", _ex)
                    Exit Sub

                Finally
                    ' Set the old directory.
                    IO.Directory.SetCurrentDirectory(OldCurrDir)

                End Try
            End If ' If (ReadMtlFileIfPresent) AndAlso (MtlFilename.Length <> 0) Then
        End Sub

        ''' <summary>
        ''' Converts string to single.
        ''' </summary>
        Friend Shared Function _StrToSng(ByVal str As String) As Single
            Dim result As Single

            If Not Single.TryParse(str, Globalization.NumberStyles.Float, WavefrontObject._FormatProvider, result) Then _
                result = CSng(Val(str))

            Return result

            'If IsNumeric(str) Then _
            ' Return CSng(str) _
            'Else _
            ' Return CSng(Val(str))
        End Function
    End Class
End Namespace
