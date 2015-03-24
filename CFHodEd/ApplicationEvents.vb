Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D

Imports GenericMath
Imports D3DHelper

Namespace My
    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        ''' <summary>Expception display window.</summary>
        Private m_ExceptionDisplay As ExceptionDisplay

        ''' <summary>Initial filename.</summary>
        Private m_InitialFilename As String = ""

        ''' <summary>
        ''' Returns initial file name.
        ''' </summary>
        Friend ReadOnly Property InitialFilename() As String
            Get
                ' Get initial file name.
                Dim out As String = m_InitialFilename

                ' Reset.
                m_InitialFilename = ""

                ' Return.
                Return out
            End Get
        End Property

        ''' <summary>
        ''' Initializes D3D configurer.
        ''' </summary>
        Private Sub InitializeD3DConfigurer()
            D3DConfigurer.AppName = "Cold Fusion HOD Editor"
            D3DConfigurer.AppCompanyName = ""

            D3DConfigurer.AddBackBufferCount(1)
            D3DConfigurer.AddBackBufferCount(2)
            D3DConfigurer.AddBackBufferCount(3)

            D3DConfigurer.AddBackBufferFormat(Format.A4R4G4B4)
            D3DConfigurer.AddBackBufferFormat(Format.A1R5G5B5)
            D3DConfigurer.AddBackBufferFormat(Format.A8R8G8B8)
            D3DConfigurer.AddBackBufferFormat(Format.A2R10G10B10)

            D3DConfigurer.AddDepthStencilFormat(Format.D15S1)
            D3DConfigurer.AddDepthStencilFormat(Format.D16)
            D3DConfigurer.AddDepthStencilFormat(Format.D24X8)
            D3DConfigurer.AddDepthStencilFormat(Format.D32)

            D3DConfigurer.AddMultiSamplingTechnique(Microsoft.DirectX.Direct3D.MultiSampleType.TwoSamples, 0)
            D3DConfigurer.AddMultiSamplingTechnique(Microsoft.DirectX.Direct3D.MultiSampleType.ThreeSamples, 0)
            D3DConfigurer.AddMultiSamplingTechnique(Microsoft.DirectX.Direct3D.MultiSampleType.FourSamples, 0)
            D3DConfigurer.AddMultiSamplingTechnique(Microsoft.DirectX.Direct3D.MultiSampleType.FiveSamples, 0)
            D3DConfigurer.AddMultiSamplingTechnique(Microsoft.DirectX.Direct3D.MultiSampleType.SixSamples, 0)
            D3DConfigurer.AddMultiSamplingTechnique(Microsoft.DirectX.Direct3D.MultiSampleType.SevenSamples, 0)
            D3DConfigurer.AddMultiSamplingTechnique(Microsoft.DirectX.Direct3D.MultiSampleType.EightSamples, 0)
            D3DConfigurer.AddMultiSamplingTechnique(Microsoft.DirectX.Direct3D.MultiSampleType.NineSamples, 0)
            D3DConfigurer.AddMultiSamplingTechnique(Microsoft.DirectX.Direct3D.MultiSampleType.TenSamples, 0)
            D3DConfigurer.AddMultiSamplingTechnique(Microsoft.DirectX.Direct3D.MultiSampleType.ElevenSamples, 0)
            D3DConfigurer.AddMultiSamplingTechnique(Microsoft.DirectX.Direct3D.MultiSampleType.TwelveSamples, 0)
            D3DConfigurer.AddMultiSamplingTechnique(Microsoft.DirectX.Direct3D.MultiSampleType.ThirteenSamples, 0)
            D3DConfigurer.AddMultiSamplingTechnique(Microsoft.DirectX.Direct3D.MultiSampleType.FourteenSamples, 0)
            D3DConfigurer.AddMultiSamplingTechnique(Microsoft.DirectX.Direct3D.MultiSampleType.FifteenSamples, 0)
            D3DConfigurer.AddMultiSamplingTechnique(Microsoft.DirectX.Direct3D.MultiSampleType.SixteenSamples, 0)

            D3DConfigurer.AddPresentInterval(PresentInterval.One)

            D3DConfigurer.CreationFlags = CreateFlags.HardwareVertexProcessing Or
                                          CreateFlags.FpuPreserve

            D3DConfigurer.FullScreen = False

            D3DManager.IsUsingResizeEventHandler = False
        End Sub

        ''' <summary>
        ''' Initializes all components.
        ''' </summary>
        Private Sub InitializeComponents()
            InitializeGenericMath()

            Try
                InitializeD3DHelper()

            Catch ex As IO.FileNotFoundException
                MsgBox("Please install the latest version of DirectX (August 2007 or later).",
                       MsgBoxStyle.Critical, "Cold Fusion HOD Editor")

                End

            End Try

            InitializeD3DConfigurer()
        End Sub

        ''' <summary>
        ''' Raised after all application forms are closed. 
        ''' This event is not raised if the application terminates abnormally.
        ''' </summary>
        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
            ' Dispose form
            If m_ExceptionDisplay IsNot Nothing Then _
                m_ExceptionDisplay.Dispose()
        End Sub

        ''' <summary>
        ''' Raised when the application starts, before the startup form is created.
        ''' </summary>
        Private Sub MyApplication_Startup(ByVal sender As Object,
                                          ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) _
            Handles Me.Startup
            ' Setup the exception display.
            m_ExceptionDisplay = New ExceptionDisplay

            ' Initialize components.
            InitializeComponents()

            ' Get the command line.
            For I As Integer = 0 To e.CommandLine.Count - 1
                m_InitialFilename &= e.CommandLine(I) & CStr(IIf(I = e.CommandLine.Count - 1, "", " "))

            Next I ' For I As Integer = 0 To e.CommandLine.Count - 1
        End Sub

        ''' <summary>
        ''' Raised if the application encounters an unhandled exception.
        ''' </summary>
        Private Sub MyApplication_UnhandledException(ByVal sender As Object,
                                                     ByVal e As _
                                                        Microsoft.VisualBasic.ApplicationServices.
                                                        UnhandledExceptionEventArgs) Handles Me.UnhandledException
            ' Set excepiton.
            m_ExceptionDisplay.SetException(e.Exception)

            ' Display form and get result.
            If m_ExceptionDisplay.ShowDialog() = DialogResult.Abort Then _
                e.ExitApplication = True _
                Else _
                e.ExitApplication = False
        End Sub
    End Class
End Namespace
