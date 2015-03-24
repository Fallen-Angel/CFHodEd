Imports Microsoft.DirectX

Namespace TextDisplay
    ''' <summary>
    ''' One element of the font cache.
    ''' </summary>
    Friend NotInheritable Class FontCacheElement
        ' -------------------------
        ' Interface(s) Implemented.
        ' -------------------------
        Implements IDisposable

        ' --------------
        ' Class Members.
        ' --------------
        ''' <summary>Device associated with font.</summary>
        Private m_Device As Direct3D.Device

        ''' <summary><c>System.Drawing.Font</c> font instance.</summary>
        Private m_SystemFont As Drawing.Font

        ''' <summary><c>Microsoft.DirectX.Direct3D.Font</c> font instance.</summary>
        Private m_Direct3DFont As Direct3D.Font

        ''' <summary>Whether the object has been disposed or not.</summary>
        Private m_DisposedValue As Boolean = False

        ' ------------------------
        ' Constructors\Finalizers.
        ' ------------------------
        ''' <summary>
        ''' Class constructor.
        ''' </summary>
        ''' <param name="FontName">
        ''' Name of font.
        ''' </param>
        ''' <param name="FontSize">
        ''' Size of font.
        ''' </param>
        ''' <param name="_Device">
        ''' Device to use.
        ''' </param>
        ''' <exception cref="ArgumentException">
        ''' Thrown when <c>FontSize &lt;= 0</c>.
        ''' </exception>
        ''' <exception cref="ArgumentNullException">
        ''' Thrown when <c>_Device Is Nothing</c>
        ''' </exception>
        ''' <exception cref="Exception">
        ''' Thrown when the input font is not available.
        ''' </exception>
        Public Sub New(ByVal FontName As String, ByVal FontSize As Single,
                       ByVal FontStyle As Drawing.FontStyle, ByVal _Device As Direct3D.Device)

            Dim FontAvailable As Boolean = False

            ' Check font size.
            If FontSize <= 0 Then _
                Throw New ArgumentException("'FontSize <= 0'") _
                    : Exit Sub

            ' Check for valid device.
            If _Device Is Nothing Then _
                Throw New ArgumentNullException("'_Device Is Nothing'") _
                    : Exit Sub

            ' Check for availability of font.
            Using FontCol As New Drawing.Text.InstalledFontCollection
                For Each FontFamily As FontFamily In FontCol.Families
                    ' Check if the name matches.
                    If FontFamily.Name = FontName Then _
                        FontAvailable = True _
                            : Exit For

                    ' Check if name matches without case sensitivity.
                    If LCase(FontFamily.Name) = LCase(FontName) Then _
                        FontName = FontFamily.Name _
                            : FontAvailable = True _
                            : Exit For

                Next FontFamily ' For Each FontFamily As FontFamily In FontCol.Families
            End Using ' Using New Drawing.Text.InstalledFontCollection

            If Not FontAvailable Then _
                Throw New Exception("The specified font is not available.") _
                    : Exit Sub

            ' Cache the device.
            m_Device = _Device

            ' Create the system font.
            m_SystemFont = New Drawing.Font(FontName, FontSize, FontStyle)

            ' Create the Direct3D font.
            m_Direct3DFont = New Direct3D.Font(_Device, m_SystemFont)
        End Sub

        ''' <summary>
        ''' Class constructor.
        ''' </summary>
        ''' <param name="_SystemFont">
        ''' The font to use.
        ''' </param>
        ''' <param name="_Device">
        ''' The Direct3D Device to use.
        ''' </param>
        ''' <exception cref="ArgumentNullException">
        ''' Thrown when <c>_SystemFont Is Nothing</c> or <c>_Device Is Nothing</c>
        ''' </exception>
        Public Sub New(ByVal _SystemFont As Drawing.Font, ByVal _Device As Direct3D.Device)
            ' Check inputs.
            If _SystemFont Is Nothing Then _
                Throw New ArgumentNullException("'SystemFont Is Nothing'") _
                    : Exit Sub

            ' Check inputs.
            If _Device Is Nothing Then _
                Throw New ArgumentException("'Device Is Nothing'") _
                    : Exit Sub

            ' Cache the variables.
            m_Device = _Device
            m_SystemFont = _SystemFont

            ' Create a new Direct3D font.
            m_Direct3DFont = New Direct3D.Font(_Device, _SystemFont)
        End Sub

        ''' <summary>
        ''' Class finalizer.
        ''' </summary>
        Protected Overrides Sub Finalize()
            Dispose(True)
        End Sub

        ' -----------------
        ' Class properties.
        ' -----------------
        ''' <summary>
        ''' Returns font used for the text.
        ''' </summary>
        Public ReadOnly Property Name() As String
            Get
                Return m_SystemFont.Name
            End Get
        End Property

        ''' <summary>
        ''' Returns size of font used for the text.
        ''' </summary>
        Public ReadOnly Property Size() As Single
            Get
                Return m_SystemFont.Size
            End Get
        End Property

        ''' <summary>
        ''' Returns style information applied to text.
        ''' </summary>
        Public ReadOnly Property Style() As Drawing.FontStyle
            Get
                Return m_SystemFont.Style
            End Get
        End Property

        ''' <summary>
        ''' Returns the font used by the text.
        ''' </summary>
        Public ReadOnly Property SystemFont() As Drawing.Font
            Get
                Return m_SystemFont
            End Get
        End Property

        ''' <summary>
        ''' Returns the device used by the font.
        ''' </summary>
        Public ReadOnly Property Device() As Direct3D.Device
            Get
                Return m_Device
            End Get
        End Property

        ''' <summary>
        ''' Returns the font used by the text.
        ''' </summary>
        Public ReadOnly Property Direct3DFont() As Direct3D.Font
            Get
                Return m_Direct3DFont
            End Get
        End Property

        ' ---------
        ' Operators
        ' ---------
        ' None

        ' -----------------
        ' Member Functions.
        ' -----------------
        ''' <summary>
        ''' For <c>IDisposable</c>.
        ''' </summary>
        Protected Sub Dispose(ByVal disposing As Boolean)
            If m_DisposedValue Then _
                Exit Sub

            ' Dispose the fonts.
            m_Direct3DFont.Dispose()
            m_SystemFont.Dispose()

            m_DisposedValue = True
        End Sub

        ''' <summary>
        ''' For <c>IDisposable</c>.
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
    End Class
End Namespace
