Namespace TextDisplay

 ''' <summary>
 ''' Class to track and display frame-rate in a Direct3D Device.
 ''' </summary>
 ''' <remarks></remarks>
 Public NotInheritable Class FPSDisplay
  Inherits TextDisplay

  ' -------------------------
  ' Interface(s) Implemented.
  ' -------------------------
  ' None

  ' --------------
  ' Class Members.
  ' --------------
  ''' <summary>Number of frames rendered since last reset.</summary>
  Private m_FrameCount As Integer

  ''' <summary>Remainder obtained by dividing <c>m_FrameCount</c> by <c>m_ModN</c>.</summary>
  ''' <remarks>This is for average per 'N' frames.</remarks>
  Private m_ModFrameCount As Integer

  ''' <summary>Number of last 'N' frames used to calculate average.</summary>
  Private m_ModN As Integer

  ''' <summary>Time when the device was last reset.</summary>
  Private m_LastResetTime As Double

  ''' <summary>Render times for the last 'N' frame.</summary>
  Private m_LastFrameTimes() As Double

  ''' <summary>Time when the previous frame was rendered.</summary>
  Private m_LastUpdateTime As Double

  ''' <summary>This stores the instantaneous frame rate.</summary>
  Private m_InstantFPS As Integer

  ''' <summary>This stores the average frame rate.</summary>
  Private m_AvgFPS As Integer

  ''' <summary>This stores the average frame rate for the last 'N' frames.</summary>
  Private m_ModFPS As Integer

  ''' <summary>Whether to display instantaneous frame rate.</summary>
  Private m_DisplayInstantFPS As Boolean

  ''' <summary>Whether to display average frame rate.</summary>
  Private m_DisplayAvgFPS As Boolean

  ''' <summary>Whether to display average frame rate for the last 'N' frames.</summary>
  Private m_DisplayModFPS As Boolean

  ' ------------------------
  ' Constructors\Finalizers.
  ' ------------------------
  ''' <summary>
  ''' Class constructor.
  ''' </summary>
  ''' <param name="Device">
  ''' The device whose frame rate is to be measured.
  ''' </param>
  Public Sub New(ByVal Device As Direct3D.Device, _
        Optional ByVal FontName As String = "Arial", _
        Optional ByVal FontSize As Single = 12, _
        Optional ByVal FontStyle As Drawing.FontStyle = FontStyle.Bold)

   ' Call base constructor.
   MyBase.New(Device, FontName, FontSize, FontStyle)

   ' Initialize to default values.
   m_ModN = 100
   m_LastResetTime = Microsoft.VisualBasic.Timer

   ' Initialize last frame times.
   ReDim m_LastFrameTimes(m_ModN - 1)

   ' Display only framerate for 'N' frames by default.
   m_DisplayInstantFPS = False
   m_DisplayAvgFPS = False
   m_DisplayModFPS = True

  End Sub

  ' -----------------
  ' Class properties.
  ' -----------------
  ''' <summary>
  ''' Whether to display instantaneous frame rate.
  ''' </summary>
  Public Property DisplayInstantaneousFramerate() As Boolean
   Get
    Return m_DisplayInstantFPS

   End Get

   Set(ByVal value As Boolean)
    m_DisplayInstantFPS = value

   End Set

  End Property

  ''' <summary>
  ''' Whether to display average frame rate.
  ''' </summary>
  Public Property DisplayAverageFramerate() As Boolean
   Get
    Return m_DisplayAvgFPS

   End Get

   Set(ByVal value As Boolean)
    m_DisplayAvgFPS = value

   End Set

  End Property

  ''' <summary>
  ''' Whether to display average frame rate for the last 'N' frames.
  ''' </summary>
  Public Property DisplayModNFramerate() As Boolean
   Get
    Return m_DisplayModFPS

   End Get

   Set(ByVal value As Boolean)
    m_DisplayModFPS = value

   End Set

  End Property

  ''' <summary>
  ''' Number of last 'N' frames used to calculate Mod FPS.
  ''' </summary>
  ''' <exception cref="ArgumentOutOfRangeException">
  ''' Thrown when <c>value</c> is less than 2.
  ''' </exception>
  Public Property ModN() As Integer
   Get
    Return m_ModN

   End Get

   Set(ByVal value As Integer)
    If (value <= 1) Then _
     Throw New ArgumentOutOfRangeException("value") _
   : Exit Property

    m_ModN = value

   End Set

  End Property

  ' ---------
  ' Operators
  ' ---------
  ' None

  ' -----------------
  ' Member Functions.
  ' -----------------
  ''' <summary>
  ''' Resets the frame statistics.
  ''' </summary>
  Public Sub Reset()
   ' Reset the frame counts.
   m_FrameCount = 0
   m_ModFrameCount = 0

   ' Update the last reset time.
   m_LastResetTime = Microsoft.VisualBasic.Timer

   ' Allocate the array to store display times for last 'N' frames.
   ReDim m_LastFrameTimes(m_ModN - 1)

  End Sub

  ''' <summary>
  ''' Updates the frame statistics and draws the frame-rate statistics.
  ''' </summary>
  ''' <remarks>
  ''' This should be called every time a frame is drawn.
  ''' </remarks>
  Public Sub Update(Optional ByVal Sprite As Direct3D.Sprite = Nothing)
   Dim FramerateText As String = ""
   Dim Display As Boolean = m_DisplayInstantFPS Or m_DisplayAvgFPS Or m_DisplayModFPS
   Dim DisplayAll As Boolean = m_DisplayInstantFPS And m_DisplayAvgFPS And m_DisplayModFPS
   Dim DisplayOdd As Boolean = m_DisplayInstantFPS Xor m_DisplayAvgFPS Xor m_DisplayModFPS
   Dim TimeElapsed As Double, ModTimeElapsed As Double = 0.0

   ' Increment the frame count.
   ' --------------------------
   m_FrameCount += 1
   m_ModFrameCount = m_FrameCount Mod m_ModN

   ' Get the time elapsed and set the last update time for 
   ' instantaneous frame rate.
   ' -----------------------------------------------------
   TimeElapsed = Microsoft.VisualBasic.Timer - m_LastUpdateTime
   m_LastUpdateTime = Microsoft.VisualBasic.Timer

   ' Get the time elapsed and set the last update time for
   ' Mod 'N' frame rate.
   ' -----------------------------------------------------
   ModTimeElapsed = m_LastUpdateTime - m_LastFrameTimes(m_ModFrameCount)
   m_LastFrameTimes(m_ModFrameCount) = m_LastUpdateTime

   ' Calculate instantaneous frame rate.
   ' -----------------------------------
   If TimeElapsed <> 0.0 Then _
    m_InstantFPS = CInt(1 / TimeElapsed) _
   Else _
    m_InstantFPS = -1

   ' Calculate average frame rate.
   ' -----------------------------
   If m_LastUpdateTime <> m_LastResetTime Then _
    m_AvgFPS = CInt(m_FrameCount / (m_LastUpdateTime - m_LastResetTime)) _
   Else _
    m_AvgFPS = -1

   ' Calculate average for the last 'N' frames.
   ' ------------------------------------------
   If ModTimeElapsed <> 0.0 Then _
    m_ModFPS = CInt(m_ModN / ModTimeElapsed) _
   Else _
    m_ModFPS = -1

   ' Build the text to display.
   ' --------------------------
   If DisplayOdd And Not DisplayAll Then
    ' Use a simple display (do not show what type of frame rate) when displaying 
    ' exactly one type of frame rate.
    ' --------------------------------------------------------------------------
    If m_DisplayInstantFPS Then _
     FramerateText = "FPS: " & CStr(m_InstantFPS)

    If m_DisplayAvgFPS Then _
     FramerateText = "FPS: " & CStr(m_AvgFPS)

    If m_DisplayModFPS Then _
     FramerateText = "FPS: " & CStr(m_ModFPS)

   Else ' If DisplayOdd And Not DisplayAll Then
    ' Otherwise, if we are showing more than one type of frame rate, mention what
    ' kind it is.
    ' ---------------------------------------------------------------------------
    If m_DisplayInstantFPS Then _
     FramerateText = "FPS (Instantaneous): " & CStr(m_InstantFPS) & vbCrLf

    If m_DisplayAvgFPS Then _
     FramerateText &= "FPS (Average): " & CStr(m_AvgFPS) & vbCrLf

    If m_DisplayModFPS Then _
     FramerateText &= "FPS (Per " & CStr(m_ModN) & " frames): " & CStr(m_ModFPS)

   End If ' If Not SimpleDisplay Then

   ' Draw text if needed.
   ' --------------------
   If Display Then _
    MyBase.DrawText(FramerateText, Sprite)

  End Sub

 End Class

End Namespace
