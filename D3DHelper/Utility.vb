''' <summary>
''' Module containing helper code for the D3DHelper Library.
''' </summary>
Public Module Utility
 ''' <summary>
 ''' Represents various attributes at the time of creation of device.
 ''' </summary>
 Public Structure DeviceCreationFlags
  ''' <summary>Back-buffer width.</summary>
  Dim Width As Integer

  ''' <summary>Back-buffer height.</summary>
  Dim Height As Integer

  ''' <summary>Back-buffer count.</summary>
  Dim BackBufferCount As Integer

  ''' <summary>Whether depth-buffers are supported.</summary>
  Dim DepthBuffer As Boolean

  ''' <summary>Whether stencil-buffers are supported.</summary>
  Dim StencilBuffer As Boolean

  ''' <summary>Whether multi-sampling supported.</summary>
  Dim Multisampling As Boolean

 End Structure

 ''' <summary>
 ''' Describes the creation parameters for a device. 
 ''' </summary>
 Public Structure CreationParameters
  ''' <summary>Adapter</summary>
  Dim Adapter As Integer

  ''' <summary>Device Type</summary>
  Dim DeviceType As DeviceType

  ''' <summary>Create Flags</summary>
  Dim CreateFlags As CreateFlags

  ''' <summary>
  ''' Converts the <c>DeviceCreationParameters</c> object to
  ''' <c>CreationParameters</c> object.
  ''' </summary>
  ''' <param name="value">
  ''' Object to convert.
  ''' </param>
  ''' <returns>
  ''' Converted object.
  ''' </returns>
  Public Shared Widening Operator CType(ByVal value As DeviceCreationParameters) As CreationParameters
   Dim CP As CreationParameters

   CP.Adapter = value.AdapterOrdinal
   CP.DeviceType = value.DeviceType
   CP.CreateFlags = value.Behavior.Value

   Return CP

  End Operator

 End Structure

 ''' <summary>
 ''' The exception that is thrown when the device has already
 ''' been created making the current operation invalid.
 ''' </summary>
 Public Class DeviceAlreadyCreatedException
  Inherits Exception

  ''' <summary>
  ''' Initializes a new instance of the <c>DeviceAlreadyCreatedException</c>
  ''' class.
  ''' </summary>
  Public Sub New()
   MyBase.New()

  End Sub

  ''' <summary>
  ''' Initializes a new instance of the <c>DeviceAlreadyCreatedException</c>
  ''' class with a specified error message.
  ''' </summary>
  ''' <param name="message">
  ''' The message that describes the error.
  ''' </param>
  Public Sub New(ByVal message As String)
   MyBase.New(message)

  End Sub

  ''' <summary>
  ''' Initializes a new instance of the <c>DeviceAlreadyCreatedException</c>
  ''' class with a specified error message and a reference to the inner
  ''' exception that is the cause of this exception.
  ''' </summary>
  ''' <param name="message">
  ''' The message that describes the error.
  ''' </param>
  ''' <param name="innerException">
  ''' The exception that is the cause of the current exception, or a null
  ''' reference (<c>Nothing</c> in Visual Basic) if no inner exception is
  ''' specified.
  ''' </param>
  Public Sub New(ByVal message As String, ByVal innerException As Exception)
   MyBase.New(message, innerException)

  End Sub

 End Class

 ''' <summary>
 ''' The exception that is thrown when the device has not
 ''' been created making the current operation invalid.
 ''' </summary>
 Public Class DeviceNotCreatedException
  Inherits Exception

  ''' <summary>
  ''' Initializes a new instance of the <c>DeviceNotCreatedException</c>
  ''' class.
  ''' </summary>
  Public Sub New()
   MyBase.New()

  End Sub

  ''' <summary>
  ''' Initializes a new instance of the <c>DeviceNotCreatedException</c>
  ''' class with a specified error message.
  ''' </summary>
  ''' <param name="message">
  ''' The message that describes the error.
  ''' </param>
  Public Sub New(ByVal message As String)
   MyBase.New(message)

  End Sub

  ''' <summary>
  ''' Initializes a new instance of the <c>DeviceNotCreatedException</c>
  ''' class with a specified error message and a reference to the inner
  ''' exception that is the cause of this exception.
  ''' </summary>
  ''' <param name="message">
  ''' The message that describes the error.
  ''' </param>
  ''' <param name="innerException">
  ''' The exception that is the cause of the current exception, or a null
  ''' reference (<c>Nothing</c> in Visual Basic) if no inner exception is
  ''' specified.
  ''' </param>
  Public Sub New(ByVal message As String, ByVal innerException As Exception)
   MyBase.New(message, innerException)

  End Sub

 End Class

 ''' <summary>Returns whether the given format is of the type ARGB, RGB, XRGB or not.</summary>
 ''' <param name="f">Input format.</param>
 Friend Function FormatIsRGB(ByVal f As Format) As Boolean
  Select Case f
   Case Format.R3G3B2, _
        Format.A8R3G3B2, Format.A1R5G5B5, Format.X1R5G5B5, Format.A4R4G4B4, Format.X4R4G4B4, Format.R5G6B5, _
        Format.R8G8B8, _
        Format.A8R8G8B8, Format.A8B8G8R8, Format.X8R8G8B8, Format.X8B8G8R8, Format.R8G8B8G8, Format.G8R8G8B8, Format.A2R10G10B10, Format.A2B10G10R10, _
        Format.A16B16G16R16, Format.A16B16G16R16F, _
        Format.A32B32G32R32F
    ' It is one of the listed formats.
    Return True

   Case Else
    ' Not _RGB colour space.
    Return False

  End Select ' Select Case f

 End Function

 ''' <summary>Returns the number of bits in the given display format (ARGB, RGB, XRGB colour-space only).</summary>
 ''' <param name="f">Input format.</param>
 Friend Function FormatBitCount(ByVal f As Format) As Integer
  Select Case f
   ' 8-BIT FORMAT
   Case Format.R3G3B2
    Return 8

    ' 16-BIT FORMAT
   Case Format.A8R3G3B2, Format.A1R5G5B5, Format.X1R5G5B5, Format.A4R4G4B4, Format.X4R4G4B4, Format.R5G6B5
    Return 16

    ' 24-BIT FORMAT
   Case Format.R8G8B8
    Return 24

    ' 32-BIT FORMAT
   Case Format.A8R8G8B8, Format.A8B8G8R8, Format.X8R8G8B8, Format.X8B8G8R8, Format.R8G8B8G8, Format.G8R8G8B8, Format.A2R10G10B10, Format.A2B10G10R10
    Return 32

    ' 64-BIT FORMAT
   Case Format.A16B16G16R16, Format.A16B16G16R16F
    Return 64

    ' 128-BIT FORMAT
   Case Format.A32B32G32R32F
    Return 128

    ' UNKNWON (error)
   Case Else
    Return -1

  End Select  '   Select Case f

 End Function

 ''' <summary>Returns whether the format has an alpha channel.</summary>
 ''' <param name="f">Input format.</param>
 ''' <remarks>Formats of the type XRGB (or XBGR) are considered not having alpha.</remarks>
 Friend Function FormatHasAlpha(ByVal f As Format) As Boolean
  Select Case f
   ' ALPHA PRESENT
   Case Format.A8R3G3B2, Format.A1R5G5B5, Format.A4R4G4B4, _
        Format.A8R8G8B8, Format.A8B8G8R8, Format.A2R10G10B10, Format.A2B10G10R10, _
        Format.A16B16G16R16, Format.A16B16G16R16F, _
        Format.A32B32G32R32F
    Return True

    ' ALPHA NOT PRESENT OR UNKNWON
   Case Else
    Return False

  End Select ' Select Case f

 End Function

 ''' <summary>Returns the number of bits for alpha in a color format.</summary>
 ''' <param name="f">Input format.</param>
 ''' <remarks>This returns the count of 'X' if the format is of type XRGB (or XBGR).</remarks>
 Friend Function FormatAlphaBitCount(ByVal f As Format) As Integer
  Select Case f
   ' 0-BIT ALPHA
   Case Format.R3G3B2, Format.R5G6B5, Format.R8G8B8, Format.R8G8B8G8, Format.G8R8G8B8
    Return 0

    ' 1-BIT ALPHA
   Case Format.A1R5G5B5, Format.X1R5G5B5
    Return 1

    ' 2-BIT FORMAT
   Case Format.A2R10G10B10, Format.A2B10G10R10
    Return 2

    ' 4-BIT ALPHA
   Case Format.A4R4G4B4, Format.X4R4G4B4
    Return 4

    ' 8-BIT ALPHA
   Case Format.A8R3G3B2, Format.A8R8G8B8, Format.A8B8G8R8, Format.X8R8G8B8, Format.X8B8G8R8
    Return 8

    ' 16-BIT ALPHA
   Case Format.A16B16G16R16, Format.A16B16G16R16F
    Return 16

    ' 32-BIT ALPHA
   Case Format.A32B32G32R32F
    Return 32

    ' UNKNWON (error)
   Case Else
    Return -1

  End Select  '   Select Case f

 End Function

 ''' <summary>Returns the number of bits for red in a color format.</summary>
 ''' <param name="f">Input format.</param>
 Friend Function FormatRedBitCount(ByVal f As Format) As Integer
  Select Case f
   ' 3-BIT RED CHANNEL
   Case Format.R3G3B2, Format.A8R3G3B2
    Return 3

    ' 4-BIT RED CHANNEL
   Case Format.A4R4G4B4, Format.X4R4G4B4
    Return 4

    ' 5-BIT RED CHANNEL
   Case Format.A1R5G5B5, Format.X1R5G5B5, Format.R5G6B5
    Return 5

    ' 8-BIT RED CHANNEL
   Case Format.R8G8B8, Format.A8R8G8B8, Format.A8B8G8R8, Format.X8R8G8B8, Format.X8B8G8R8, Format.R8G8B8G8, Format.G8R8G8B8
    Return 8

    ' 10-BIT RED CHANNEL
   Case Format.A2R10G10B10, Format.A2B10G10R10
    Return 10

    ' 16-BIT RED CHANNEL
   Case Format.A16B16G16R16, Format.A16B16G16R16F
    Return 16

    ' 32-BIT RED CHANNEL
   Case Format.A32B32G32R32F
    Return 32

    ' UNKNWON (error)
   Case Else
    Return -1

  End Select ' Select Case f

 End Function

 ''' <summary>Returns the number of bits for green in a color format.</summary>
 ''' <param name="f">Input format.</param>
 Friend Function FormatGreenBitCount(ByVal f As Format) As Integer
  Select Case f
   ' 3-BIT GREEN CHANNEL
   Case Format.R3G3B2, Format.A8R3G3B2
    Return 3

    ' 4-BIT GREEN CHANNEL
   Case Format.A4R4G4B4, Format.X4R4G4B4
    Return 4

    ' 5-BIT GREEN CHANNEL
   Case Format.A1R5G5B5, Format.X1R5G5B5
    Return 5

    ' 6-BIT GREEN CHANNEL
   Case Format.R5G6B5
    Return 6

    ' 8-BIT GREEN CHANNEL
   Case Format.R8G8B8, Format.A8R8G8B8, Format.A8B8G8R8, Format.X8R8G8B8, Format.X8B8G8R8, Format.R8G8B8G8, Format.G8R8G8B8
    Return 8

    ' 10-BIT GREEN CHANNEL
   Case Format.A2R10G10B10, Format.A2B10G10R10
    Return 10

    ' 16-BIT GREEN CHANNEL
   Case Format.A16B16G16R16, Format.A16B16G16R16F
    Return 16

    ' 32-BIT GREEN CHANNEL
   Case Format.A32B32G32R32F
    Return 32

    ' UNKNWON (error)
   Case Else
    Return -1

  End Select  '   Select Case f

 End Function

 ''' <summary>Returns the number of bits for blue in a color format.</summary>
 ''' <param name="f">Input format.</param>
 Friend Function FormatBlueBitCount(ByVal f As Format) As Integer
  Select Case f
   ' 3-BIT BLUE CHANNEL
   Case Format.R3G3B2, Format.A8R3G3B2
    Return 3

    ' 4-BIT BLUE CHANNEL
   Case Format.A4R4G4B4, Format.X4R4G4B4
    Return 4

    ' 5-BIT BLUE CHANNEL
   Case Format.A1R5G5B5, Format.X1R5G5B5, Format.R5G6B5
    Return 5

    ' 8-BIT BLUE CHANNEL
   Case Format.R8G8B8, Format.A8R8G8B8, Format.A8B8G8R8, Format.X8R8G8B8, Format.X8B8G8R8, Format.R8G8B8G8, Format.G8R8G8B8
    Return 8

    ' 10-BIT BLUE CHANNEL
   Case Format.A2R10G10B10, Format.A2B10G10R10
    Return 10

    ' 16-BIT BLUE CHANNEL
   Case Format.A16B16G16R16, Format.A16B16G16R16F
    Return 16

    ' 32-BIT BLUE CHANNEL
   Case Format.A32B32G32R32F
    Return 32

    ' UNKNWON (error)
   Case Else
    Return -1

  End Select ' Select Case f

 End Function

 ''' <summary>Returns whether two formats are compatible or not.</summary>
 ''' <param name="f1">Input format 1.</param>
 ''' <param name="f2">Input format 2.</param>
 ''' <remarks> (ARGB, RGB, XRGB type formats only)</remarks>
 Friend Function FormatsAreEquivalent(ByVal f1 As Format, ByVal f2 As Format) As Boolean
  ' Return alpha, then green, then red and finally blue channel count.
  If (FormatAlphaBitCount(f1) = FormatAlphaBitCount(f2)) AndAlso _
     (FormatGreenBitCount(f1) = FormatGreenBitCount(f2)) AndAlso _
     (FormatRedBitCount(f1) = FormatRedBitCount(f2)) AndAlso _
     (FormatBlueBitCount(f1) = FormatBlueBitCount(f2)) Then _
   Return True _
  Else _
   Return False

 End Function

 ''' <summary>Returns an equivalent format containing alpha channel, if possible.</summary>
 ''' <param name="f">Input format.</param>
 ''' <remarks>Applicable for only XRGB, XBGR, ARGB and ABGR type formats.</remarks>
 Friend Function FormatToAlphaFormat(ByVal f As Format) As Format
  Select Case f
   ' 1-BIT ALPHA
   Case Format.A1R5G5B5, Format.X1R5G5B5
    Return Format.A1R5G5B5

    ' 4-BIT ALPHA
   Case Format.A4R4G4B4, Format.X4R4G4B4
    Return Format.A4R4G4B4

    ' 8-BIT ALPHA, 16-BIT FORMAT
   Case Format.A8R3G3B2
    Return Format.A8R3G3B2

    ' 8-BIT ALPHA, 32-BIT FORMAT
   Case Format.A8R8G8B8, Format.X8R8G8B8
    Return Format.A8R8G8B8

    ' UNKNOWN (error)
   Case Else
    Return Format.Unknown

  End Select  '   Select Case f

 End Function

 ''' <summary>Returns an equivalent format containing alpha channel, if possible.</summary>
 ''' <param name="f">Input format.</param>
 ''' ''' <remarks>Applicable for only XRGB, XBGR, ARGB and ABGR type formats.</remarks>
 Friend Function FormatToNoAlphaFormat(ByVal f As Format) As Format
  Select Case f
   ' NO ALPHA SLOT
   Case Format.R3G3B2, Format.R5G6B5, Format.R8G8B8
    Return f

    ' 1-BIT ALPHA
   Case Format.A1R5G5B5, Format.X1R5G5B5
    Return Format.X1R5G5B5

    ' 4-BIT ALPHA
   Case Format.A4R4G4B4, Format.X4R4G4B4
    Return Format.X4R4G4B4

    ' 8-BIT ALPHA, 16-BIT FORMAT.
   Case Format.A8R3G3B2
    ' No compatible substitute.
    Return Format.Unknown

    ' 8-BIT ALPHA
   Case Format.A8R8G8B8, Format.X8R8G8B8
    Return Format.X8R8G8B8

    ' UNKNOWN (error)
   Case Else
    Return Format.Unknown

  End Select  '   Select Case f

 End Function

 ''' <summary>Returns whether the given format is a valid depth stencil format.</summary>
 ''' <param name="f">Input format.</param>
 Friend Function FormatIsDepthStencil(ByVal f As Format) As Boolean
  Select Case f
   Case Format.D15S1, Format.D16, Format.D16Lockable, _
        Format.D24X8, Format.D24X4S4, Format.D24S8, Format.D24SingleS8, _
        Format.D32, Format.D32SingleLockable
    ' It is one of the listed formats.
    Return True

   Case Else
    ' Not a depth stencil format.
    Return False

  End Select ' Select Case f

 End Function

 ''' <summary>Returns whether the given format is a valid depth stencil format.</summary>
 ''' <param name="f">Input <c>DepthFormat</c>.</param>
 Friend Function FormatIsDepthStencil(ByVal f As DepthFormat) As Boolean
  ' Return other function's value.
  Return FormatIsDepthStencil(CType(f, Format))

 End Function

 ''' <summary>Returns whether the given format is a lockable depth format.</summary>
 ''' <param name="f">Input <c>Format</c>.</param>
 Friend Function FormatIsDepthLockable(ByVal f As Format) As Boolean
  If (f = Format.D16Lockable) OrElse _
     (f = Format.D32SingleLockable) Then _
   Return True

  Return False

 End Function

 ''' <summary>Returns whether the given format is a lockable depth format.</summary>
 ''' <param name="f">Input <c>DepthFormat</c>.</param>
 Friend Function FormatIsDepthLockable(ByVal f As DepthFormat) As Boolean
  ' Return other function's value.,
  Return FormatIsDepthLockable(CType(f, Format))

 End Function

 ''' <summary>Returns the number of depth bits in a depth-stencil format.</summary>
 ''' <param name="f">Input format.</param>
 Friend Function FormatDepthBitCount(ByVal f As Format) As Integer
  Select Case f
   ' 15-BIT DEPTH
   Case Format.D15S1
    Return 15

    ' 16-BIT DEPTH
   Case Format.D16, Format.D16Lockable
    Return 16

    ' 24-BIT DEPTH
   Case Format.D24X8, Format.D24X4S4, Format.D24S8, Format.D24SingleS8
    Return 24

    ' 32-BIT DEPTH
   Case Format.D32, Format.D32SingleLockable
    Return 32

  End Select ' Select Case f

 End Function

 ''' <summary>Returns the number of depth bits in a depth-stencil format.</summary>
 ''' <param name="f">Input format.</param>
 Friend Function FormatDepthBitCount(ByVal f As DepthFormat) As Integer
  ' Return other function's value.
  Return FormatDepthBitCount(CType(f, Format))

 End Function

 ''' <summary>Returns the number of stencil bits in a depth-stencil format.</summary>
 ''' <param name="f">Input format.</param>
 Friend Function FormatStencilBitCount(ByVal f As Format) As Integer
  Select Case f
   ' NO STENCIL
   Case Format.D16, Format.D16Lockable, Format.D24X8, Format.D32, Format.D32SingleLockable
    Return 0

    ' 1-BIT STENCIL
   Case Format.D15S1
    Return 1

    ' 4-BIT STENCIL
   Case Format.D24X4S4
    Return 4

    ' 8-BIT STENCIL
   Case Format.D24X8, Format.D24SingleS8
    Return 8

    ' UNKNOWN (error)
   Case Else
    Return -1

  End Select ' Select Case f

 End Function

 ''' <summary>Returns the number of stencil bits in a depth-stencil format.</summary>
 ''' <param name="f">Input format.</param>
 Friend Function FormatStencilBitCount(ByVal f As DepthFormat) As Integer
  ' Return value of other function.
  Return FormatStencilBitCount(CType(f, Format))

 End Function

 ''' <summary>Returns a format using its name.</summary>
 ''' <param name="value">Input format (in string).</param>
 ''' <returns>Output format (an enumerated value).</returns>
 ''' <remarks>This ignores case.</remarks>
 Public Function FormatFromString(ByVal value As String) As Format
  Select Case value.ToLower()
   Case Format.A16B16G16R16.ToString.ToLower()
    Return Format.A16B16G16R16

   Case Format.A16B16G16R16F.ToString.ToLower()
    Return Format.A16B16G16R16F

   Case Format.A1R5G5B5.ToString.ToLower()
    Return Format.A1R5G5B5

   Case Format.A2B10G10R10.ToString.ToLower()
    Return Format.A2B10G10R10

   Case Format.A2R10G10B10.ToString.ToLower()
    Return Format.A2R10G10B10

   Case Format.A2W10V10U10.ToString.ToLower()
    Return Format.A2W10V10U10

   Case Format.A32B32G32R32F.ToString.ToLower()
    Return Format.A32B32G32R32F

   Case Format.A4L4.ToString.ToLower()
    Return Format.A4L4

   Case Format.A4R4G4B4.ToString.ToLower()
    Return Format.A4R4G4B4

   Case Format.A8.ToString.ToLower()
    Return Format.A8

   Case Format.A8B8G8R8.ToString.ToLower()
    Return Format.A8B8G8R8

   Case Format.A8L8.ToString.ToLower()
    Return Format.A8L8

   Case Format.A8P8.ToString.ToLower()
    Return Format.A8P8

   Case Format.A8R3G3B2.ToString.ToLower()
    Return Format.A8R3G3B2

   Case Format.A8R8G8B8.ToString.ToLower()
    Return Format.A8R8G8B8

   Case Format.CxV8U8.ToString.ToLower()
    Return Format.CxV8U8

   Case Format.D15S1.ToString.ToLower()
    Return Format.D15S1

   Case Format.D16.ToString.ToLower()
    Return Format.D16

   Case Format.D16Lockable.ToString.ToLower()
    Return Format.D16Lockable

   Case Format.D24S8.ToString.ToLower()
    Return Format.D24S8

   Case Format.D24SingleS8.ToString.ToLower()
    Return Format.D24SingleS8

   Case Format.D24X4S4.ToString.ToLower()
    Return Format.D24X4S4

   Case Format.D24X8.ToString.ToLower()
    Return Format.D24X8

   Case Format.D32.ToString.ToLower()
    Return Format.D32

   Case Format.D32SingleLockable.ToString.ToLower()
    Return Format.D32SingleLockable

   Case Format.Dxt1.ToString.ToLower()
    Return Format.Dxt1

   Case Format.Dxt2.ToString.ToLower()
    Return Format.Dxt2

   Case Format.Dxt3.ToString.ToLower()
    Return Format.Dxt3

   Case Format.Dxt4.ToString.ToLower()
    Return Format.Dxt4

   Case Format.Dxt5.ToString.ToLower()
    Return Format.Dxt5

   Case Format.G16R16.ToString.ToLower()
    Return Format.G16R16

   Case Format.G16R16F.ToString.ToLower()
    Return Format.G16R16F

   Case Format.G32R32F.ToString.ToLower()
    Return Format.G32R32F

   Case Format.G8R8G8B8.ToString.ToLower()
    Return Format.Dxt5

   Case Format.G8R8G8B8.ToString.ToLower()
    Return Format.Dxt5

   Case Format.L16.ToString.ToLower()
    Return Format.L16

   Case Format.L6V5U5.ToString.ToLower()
    Return Format.L6V5U5

   Case Format.L8.ToString.ToLower()
    Return Format.L8

   Case Format.Multi2Argb8.ToString.ToLower()
    Return Format.Multi2Argb8

   Case Format.P8.ToString.ToLower()
    Return Format.P8

   Case Format.Q16W16V16U16.ToString.ToLower()
    Return Format.Q16W16V16U16

   Case Format.Q8W8V8U8.ToString.ToLower()
    Return Format.Q8W8V8U8

   Case Format.R16F.ToString.ToLower()
    Return Format.R16F

   Case Format.R32F.ToString.ToLower()
    Return Format.R32F

   Case Format.R3G3B2.ToString.ToLower()
    Return Format.R3G3B2

   Case Format.R5G6B5.ToString.ToLower()
    Return Format.R5G6B5

   Case Format.R8G8B8.ToString.ToLower()
    Return Format.R8G8B8

   Case Format.R8G8B8G8.ToString.ToLower()
    Return Format.R8G8B8G8

   Case Format.Uyvy.ToString.ToLower()
    Return Format.Uyvy

   Case Format.V16U16.ToString.ToLower()
    Return Format.V16U16

   Case Format.V8U8.ToString.ToLower()
    Return Format.V8U8

   Case Format.VertexData.ToString.ToLower()
    Return Format.VertexData

   Case Format.X1R5G5B5.ToString.ToLower()
    Return Format.X1R5G5B5

   Case Format.X4R4G4B4.ToString.ToLower()
    Return Format.X4R4G4B4

   Case Format.X8B8G8R8.ToString.ToLower()
    Return Format.X8B8G8R8

   Case Format.X8L8V8U8.ToString.ToLower()
    Return Format.X8L8V8U8

   Case Format.X8R8G8B8.ToString.ToLower()
    Return Format.X8R8G8B8

   Case Format.Yuy2.ToString.ToLower()
    Return Format.Yuy2

   Case Else
    Return Format.Unknown

  End Select ' Select Case value.ToLower()

 End Function

 ''' <summary>Returns the number of processors available on a machine.</summary>
 Friend Function GetCPUCount() As Integer
  Dim CPUCount As Integer
  Dim RegKey As Microsoft.Win32.RegistryKey

  ' Try to open the registry key.
  RegKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey( _
              "HARDWARE\DESCRIPTION\System\CentralProcessor" _
           )

  ' Get the count.
  CPUCount = RegKey.SubKeyCount

  ' Close the key.
  RegKey.Close()

  ' Return count.
  Return CPUCount

 End Function

End Module
