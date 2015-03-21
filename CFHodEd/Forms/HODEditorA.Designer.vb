<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HODEditorA
 Inherits System.Windows.Forms.Form

 'Form overrides dispose to clean up the component list.
 <System.Diagnostics.DebuggerNonUserCode()> _
 Protected Overrides Sub Dispose(ByVal disposing As Boolean)
  Try
   If disposing AndAlso components IsNot Nothing Then
    components.Dispose()
   End If
  Finally
   MyBase.Dispose(disposing)
  End Try
 End Sub

 'Required by the Windows Form Designer
 Private components As System.ComponentModel.IContainer

 'NOTE: The following procedure is required by the Windows Form Designer
 'It can be modified using the Windows Form Designer.  
 'Do not modify it using the code editor.
 <System.Diagnostics.DebuggerStepThrough()> _
 Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HODEditorA))
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileSepr1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuFileSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileSaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileSepr2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuFileExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditCut = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditPaste = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuToolsOptions = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuToolsSeperator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuToolsRenormalMeshes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuToolsRetangentMeshes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuToolsSeperator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuToolsTranslate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuToolsRotate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuToolsScale = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuToolsMirror = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuToolsSeperator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuToolsVARYToMULT = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelpAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.tspNew = New System.Windows.Forms.ToolStripButton()
        Me.tspOpen = New System.Windows.Forms.ToolStripButton()
        Me.tspSave = New System.Windows.Forms.ToolStripButton()
        Me.tspSeperator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tspCut = New System.Windows.Forms.ToolStripButton()
        Me.tspCopy = New System.Windows.Forms.ToolStripButton()
        Me.tspPaste = New System.Windows.Forms.ToolStripButton()
        Me.tspSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tspLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.tspMode = New System.Windows.Forms.ToolStripComboBox()
        Me.tspSeperator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tspLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.tspObjectScale = New System.Windows.Forms.ToolStripTextBox()
        Me.tspSeperator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tspLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.tspCameraSensitivity = New System.Windows.Forms.ToolStripTextBox()
        Me.pbxDisplay_cms = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.pbxDisplay_cmsReset = New System.Windows.Forms.ToolStripMenuItem()
        Me.pbxDisplay_cmsSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.pbxDisplay_cmsWireframe = New System.Windows.Forms.ToolStripMenuItem()
        Me.pbxDisplay_cmsSolid = New System.Windows.Forms.ToolStripMenuItem()
        Me.pbxDisplay_cmsSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.pbxDisplay_cmsWireframeOverlay = New System.Windows.Forms.ToolStripMenuItem()
        Me.pbxDisplay_cmsSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.pbxDisplay_cmsLights = New System.Windows.Forms.ToolStripMenuItem()
        Me.pbxDisplay_cmsEditLight = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenHODFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SaveHODFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.OpenTextureFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SaveTextureFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.OpenShaderFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.OpenOBJFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SaveOBJFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.pbxDisplay = New System.Windows.Forms.PictureBox()
        Me.tabMain = New System.Windows.Forms.TabControl()
        Me.tabTextures = New System.Windows.Forms.TabPage()
        Me.cmdTexturesExportAll = New System.Windows.Forms.Button()
        Me.lblTextureMipCount = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblTextureDimensions = New System.Windows.Forms.Label()
        Me.lblTextureFormat = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblTexturePath = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdTexturePreview = New System.Windows.Forms.Button()
        Me.cmdTextureExport = New System.Windows.Forms.Button()
        Me.cmdTextureRemove = New System.Windows.Forms.Button()
        Me.cmdTextureImport = New System.Windows.Forms.Button()
        Me.cmdTextureAdd = New System.Windows.Forms.Button()
        Me.lstTextures = New System.Windows.Forms.ListBox()
        Me.tabMaterials = New System.Windows.Forms.TabPage()
        Me.cmdMaterialShaderRename = New System.Windows.Forms.Button()
        Me.lstMaterials = New System.Windows.Forms.ListBox()
        Me.fraMaterialParameters = New System.Windows.Forms.GroupBox()
        Me.optMaterialParameterColour = New System.Windows.Forms.RadioButton()
        Me.optMaterialParameterTexture = New System.Windows.Forms.RadioButton()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmdMaterialNoTexture = New System.Windows.Forms.Button()
        Me.cboMaterialTextureIndex = New System.Windows.Forms.ComboBox()
        Me.txtMaterialColourA = New System.Windows.Forms.TextBox()
        Me.txtMaterialColourB = New System.Windows.Forms.TextBox()
        Me.txtMaterialColourG = New System.Windows.Forms.TextBox()
        Me.txtMaterialColourR = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmdMaterialParameterRename = New System.Windows.Forms.Button()
        Me.cmdMaterialParameterRemove = New System.Windows.Forms.Button()
        Me.cmdMaterialParameterAdd = New System.Windows.Forms.Button()
        Me.lstMaterialParameters = New System.Windows.Forms.ListBox()
        Me.cmdMaterialParametersFromFile = New System.Windows.Forms.Button()
        Me.lblMaterialShaderName = New System.Windows.Forms.Label()
        Me.lblMaterialName = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmdMaterialRename = New System.Windows.Forms.Button()
        Me.cmdMaterialRemove = New System.Windows.Forms.Button()
        Me.cmdMaterialAdd = New System.Windows.Forms.Button()
        Me.tabMultiMeshes = New System.Windows.Forms.TabPage()
        Me.cmdShipMeshesRetangent = New System.Windows.Forms.Button()
        Me.cmdShipMeshesRenormal = New System.Windows.Forms.Button()
        Me.lstShipMeshes = New System.Windows.Forms.ListBox()
        Me.cboShipMeshesParent = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmdShipMeshesRename = New System.Windows.Forms.Button()
        Me.fraShipMeshes = New System.Windows.Forms.GroupBox()
        Me.cmdShipMeshesLODRetangent = New System.Windows.Forms.Button()
        Me.cmdShipMeshesLODRenormal = New System.Windows.Forms.Button()
        Me.cstShipMeshesLODs = New System.Windows.Forms.CheckedListBox()
        Me.cmdShipMeshesLODReMaterial = New System.Windows.Forms.Button()
        Me.cmdShipMeshesLODExport = New System.Windows.Forms.Button()
        Me.cmdShipMeshesLODImport = New System.Windows.Forms.Button()
        Me.cmdShipMeshesLODTransform = New System.Windows.Forms.Button()
        Me.cmdShipMeshesLODRemove = New System.Windows.Forms.Button()
        Me.cmdShipMeshesLODAdd = New System.Windows.Forms.Button()
        Me.cmdShipMeshesRemove = New System.Windows.Forms.Button()
        Me.cmdShipMeshesAdd = New System.Windows.Forms.Button()
        Me.tabGoblins = New System.Windows.Forms.TabPage()
        Me.cmdGoblinsRetangent = New System.Windows.Forms.Button()
        Me.cmdGoblinsRenormal = New System.Windows.Forms.Button()
        Me.cboGoblinsParent = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmdGoblinsRename = New System.Windows.Forms.Button()
        Me.cstGoblins = New System.Windows.Forms.CheckedListBox()
        Me.cmdGoblinsRematerial = New System.Windows.Forms.Button()
        Me.cmdGoblinsExport = New System.Windows.Forms.Button()
        Me.cmdGoblinsImport = New System.Windows.Forms.Button()
        Me.cmdGoblinsTransform = New System.Windows.Forms.Button()
        Me.cmdGoblinsRemove = New System.Windows.Forms.Button()
        Me.cmdGoblinsAdd = New System.Windows.Forms.Button()
        Me.tabBGMS = New System.Windows.Forms.TabPage()
        Me.cmdBackgroundMeshesGenerateTexture = New System.Windows.Forms.Button()
        Me.cmdBackgroundMeshesRecolourizeAll = New System.Windows.Forms.Button()
        Me.cmdBackgroundMeshesRecolourize = New System.Windows.Forms.Button()
        Me.cstBackgroundMeshes = New System.Windows.Forms.CheckedListBox()
        Me.cmdBackgroundMeshesExport = New System.Windows.Forms.Button()
        Me.cmdBackgroundMeshesImport = New System.Windows.Forms.Button()
        Me.cmdBackgroundMeshesRemove = New System.Windows.Forms.Button()
        Me.cmdBackgroundMeshesAdd = New System.Windows.Forms.Button()
        Me.tabUI = New System.Windows.Forms.TabPage()
        Me.cmdUIMeshesRenormal = New System.Windows.Forms.Button()
        Me.cmdUIMeshesRename = New System.Windows.Forms.Button()
        Me.cstUIMeshes = New System.Windows.Forms.CheckedListBox()
        Me.cmdUIMeshesExport = New System.Windows.Forms.Button()
        Me.cmdUIMeshesImport = New System.Windows.Forms.Button()
        Me.cmdUIMeshesRemove = New System.Windows.Forms.Button()
        Me.cmdUIMeshesAdd = New System.Windows.Forms.Button()
        Me.tabJoints = New System.Windows.Forms.TabPage()
        Me.chkJointsRender = New System.Windows.Forms.CheckBox()
        Me.fraJoint = New System.Windows.Forms.GroupBox()
        Me.chkJointsDegreeOfFreedomZ = New System.Windows.Forms.CheckBox()
        Me.chkJointsDegreeOfFreedomY = New System.Windows.Forms.CheckBox()
        Me.chkJointsDegreeOfFreedomX = New System.Windows.Forms.CheckBox()
        Me.txtJointsAxisZ = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtJointsAxisY = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtJointsAxisX = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtJointsScaleZ = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtJointsScaleY = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtJointsScaleX = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtJointsRotationZ = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtJointsRotationY = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtJointsRotationX = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtJointsPositionZ = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtJointsPositionY = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtJointsPositionX = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cmdJointsAddTemplate = New System.Windows.Forms.Button()
        Me.cmdJointsRemove = New System.Windows.Forms.Button()
        Me.cmdJointsAdd = New System.Windows.Forms.Button()
        Me.tvwJoints = New System.Windows.Forms.TreeView()
        Me.tvwJoints_cms = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tvwJoints_cmsRename = New System.Windows.Forms.ToolStripMenuItem()
        Me.tvwJoints_cmsSeperator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tvwJoints_cmsHide = New System.Windows.Forms.ToolStripMenuItem()
        Me.tvwJoints_cmsShow = New System.Windows.Forms.ToolStripMenuItem()
        Me.tvwJoints_cmsSeperator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tvwJoints_cmsCollapse = New System.Windows.Forms.ToolStripMenuItem()
        Me.tvwJoints_cmsExpand = New System.Windows.Forms.ToolStripMenuItem()
        Me.tvwJoints_cmsSeperator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tvwJoints_cmsMoveUp = New System.Windows.Forms.ToolStripMenuItem()
        Me.tvwJoints_cmsMoveDown = New System.Windows.Forms.ToolStripMenuItem()
        Me.tabCM = New System.Windows.Forms.TabPage()
        Me.cmdCMRecalc = New System.Windows.Forms.Button()
        Me.fraCMBSPH = New System.Windows.Forms.GroupBox()
        Me.txtCMCX = New System.Windows.Forms.TextBox()
        Me.txtCMRadius = New System.Windows.Forms.TextBox()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.txtCMCZ = New System.Windows.Forms.TextBox()
        Me.txtCMCY = New System.Windows.Forms.TextBox()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.fraCMBBOX = New System.Windows.Forms.GroupBox()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.txtCMMaxZ = New System.Windows.Forms.TextBox()
        Me.txtCMMinX = New System.Windows.Forms.TextBox()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.txtCMMaxY = New System.Windows.Forms.TextBox()
        Me.txtCMMinY = New System.Windows.Forms.TextBox()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.txtCMMaxX = New System.Windows.Forms.TextBox()
        Me.txtCMMinZ = New System.Windows.Forms.TextBox()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.cboCMName = New System.Windows.Forms.ComboBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.cmdCMExport = New System.Windows.Forms.Button()
        Me.cmdCMImport = New System.Windows.Forms.Button()
        Me.cmdCMRemove = New System.Windows.Forms.Button()
        Me.cmdCMAdd = New System.Windows.Forms.Button()
        Me.cstCM = New System.Windows.Forms.CheckedListBox()
        Me.tabEngineShapes = New System.Windows.Forms.TabPage()
        Me.cboEngineShapesParent = New System.Windows.Forms.ComboBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.cmdEngineShapesRename = New System.Windows.Forms.Button()
        Me.cstEngineShapes = New System.Windows.Forms.CheckedListBox()
        Me.cmdEngineShapesExport = New System.Windows.Forms.Button()
        Me.cmdEngineShapesImport = New System.Windows.Forms.Button()
        Me.cmdEngineShapesRemove = New System.Windows.Forms.Button()
        Me.cmdEngineShapesAdd = New System.Windows.Forms.Button()
        Me.tabEngineGlows = New System.Windows.Forms.TabPage()
        Me.fraEngineGlowsMisc = New System.Windows.Forms.GroupBox()
        Me.sldEngineGlowsThrusterPowerFactor = New System.Windows.Forms.TrackBar()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtEngineGlowsLOD = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.cboEngineGlowsParent = New System.Windows.Forms.ComboBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.cmdEngineGlowsRename = New System.Windows.Forms.Button()
        Me.cstEngineGlows = New System.Windows.Forms.CheckedListBox()
        Me.cmdEngineGlowsExport = New System.Windows.Forms.Button()
        Me.cmdEngineGlowsImport = New System.Windows.Forms.Button()
        Me.cmdEngineGlowsRemove = New System.Windows.Forms.Button()
        Me.cmdEngineGlowsAdd = New System.Windows.Forms.Button()
        Me.tabEngineBurns = New System.Windows.Forms.TabPage()
        Me.txtEngineBurn5Z = New System.Windows.Forms.TextBox()
        Me.txtEngineBurn5Y = New System.Windows.Forms.TextBox()
        Me.txtEngineBurn5X = New System.Windows.Forms.TextBox()
        Me.txtEngineBurn4Z = New System.Windows.Forms.TextBox()
        Me.txtEngineBurn4Y = New System.Windows.Forms.TextBox()
        Me.txtEngineBurn4X = New System.Windows.Forms.TextBox()
        Me.txtEngineBurn3Z = New System.Windows.Forms.TextBox()
        Me.txtEngineBurn3Y = New System.Windows.Forms.TextBox()
        Me.txtEngineBurn3X = New System.Windows.Forms.TextBox()
        Me.txtEngineBurn2Z = New System.Windows.Forms.TextBox()
        Me.txtEngineBurn2Y = New System.Windows.Forms.TextBox()
        Me.txtEngineBurn2X = New System.Windows.Forms.TextBox()
        Me.txtEngineBurn1Z = New System.Windows.Forms.TextBox()
        Me.txtEngineBurn1Y = New System.Windows.Forms.TextBox()
        Me.txtEngineBurn1X = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.cboEngineBurnsParent = New System.Windows.Forms.ComboBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.cmdEngineBurnsRename = New System.Windows.Forms.Button()
        Me.cstEngineBurns = New System.Windows.Forms.CheckedListBox()
        Me.cmdEngineBurnsRemove = New System.Windows.Forms.Button()
        Me.cmdEngineBurnsAdd = New System.Windows.Forms.Button()
        Me.tabNavLights = New System.Windows.Forms.TabPage()
        Me.pbxNavLightsSample = New System.Windows.Forms.PictureBox()
        Me.chkNavLightsHighEnd = New System.Windows.Forms.CheckBox()
        Me.chkNavLightsVisibleSprite = New System.Windows.Forms.CheckBox()
        Me.txtNavlightsColourB = New System.Windows.Forms.TextBox()
        Me.txtNavlightsColourG = New System.Windows.Forms.TextBox()
        Me.txtNavlightsColourR = New System.Windows.Forms.TextBox()
        Me.txtNavLightsDistance = New System.Windows.Forms.TextBox()
        Me.txtNavLightsStyle = New System.Windows.Forms.TextBox()
        Me.txtNavLightsFrequency = New System.Windows.Forms.TextBox()
        Me.txtNavLightsPhase = New System.Windows.Forms.TextBox()
        Me.txtNavLightsSize = New System.Windows.Forms.TextBox()
        Me.txtNavLightsSection = New System.Windows.Forms.TextBox()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.cboNavLightsName = New System.Windows.Forms.ComboBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.cstNavLights = New System.Windows.Forms.CheckedListBox()
        Me.cmdNavLightsRemove = New System.Windows.Forms.Button()
        Me.cmdNavLightsAdd = New System.Windows.Forms.Button()
        Me.tabMarkers = New System.Windows.Forms.TabPage()
        Me.fraMarkersTransform = New System.Windows.Forms.GroupBox()
        Me.txtMarkerRotationZ = New System.Windows.Forms.TextBox()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.txtMarkerRotationY = New System.Windows.Forms.TextBox()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.txtMarkerRotationX = New System.Windows.Forms.TextBox()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.txtMarkerPositionZ = New System.Windows.Forms.TextBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.txtMarkerPositionY = New System.Windows.Forms.TextBox()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.txtMarkerPositionX = New System.Windows.Forms.TextBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.cboMarkersParent = New System.Windows.Forms.ComboBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.cmdMarkersRename = New System.Windows.Forms.Button()
        Me.cstMarkers = New System.Windows.Forms.CheckedListBox()
        Me.cmdMarkersRemove = New System.Windows.Forms.Button()
        Me.cmdMarkersAdd = New System.Windows.Forms.Button()
        Me.tabDockpaths = New System.Windows.Forms.TabPage()
        Me.chkDockpathsUseAnim = New System.Windows.Forms.CheckBox()
        Me.chkDockpathsLatchPath = New System.Windows.Forms.CheckBox()
        Me.chkDockpathsExitPath = New System.Windows.Forms.CheckBox()
        Me.txtDockpathsLinkedPaths = New System.Windows.Forms.TextBox()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.txtDockpathsDockFamilies = New System.Windows.Forms.TextBox()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.txtDockpathsGlobalTolerance = New System.Windows.Forms.TextBox()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.fraDockpathsKeyframes = New System.Windows.Forms.GroupBox()
        Me.chkDockpathsKeyframeClearReservation = New System.Windows.Forms.CheckBox()
        Me.chkDockpathsKeyframeUseClipPlane = New System.Windows.Forms.CheckBox()
        Me.chkDockpathsKeyframeQueueOrigin = New System.Windows.Forms.CheckBox()
        Me.chkDockpathsKeyframePlayerInControl = New System.Windows.Forms.CheckBox()
        Me.chkDockpathsKeyframeForceCloseBehaviour = New System.Windows.Forms.CheckBox()
        Me.chkDockpathsKeyframeCheckRotation = New System.Windows.Forms.CheckBox()
        Me.chkDockpathsKeyframeDropFocus = New System.Windows.Forms.CheckBox()
        Me.chkDockpathsKeyframeUseRotation = New System.Windows.Forms.CheckBox()
        Me.txtDockpathsKeyframeMaxSpeed = New System.Windows.Forms.TextBox()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.txtDockpathsKeyframeTolerance = New System.Windows.Forms.TextBox()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.txtDockpathsKeyframeRotationZ = New System.Windows.Forms.TextBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.txtDockpathsKeyframeRotationY = New System.Windows.Forms.TextBox()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.txtDockpathsKeyframeRotationX = New System.Windows.Forms.TextBox()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.txtDockpathsKeyframePositionZ = New System.Windows.Forms.TextBox()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.txtDockpathsKeyframePositionY = New System.Windows.Forms.TextBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.txtDockpathsKeyframePositionX = New System.Windows.Forms.TextBox()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.sldDockpathsKeyframe = New System.Windows.Forms.TrackBar()
        Me.cmdDockpathsKeyframesInsert = New System.Windows.Forms.Button()
        Me.cmdDockpathsKeyframesAdd = New System.Windows.Forms.Button()
        Me.cmdDockpathsKeyframesRemove = New System.Windows.Forms.Button()
        Me.txtDockpathsParentName = New System.Windows.Forms.TextBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.cmdDockpathsRename = New System.Windows.Forms.Button()
        Me.cstDockpaths = New System.Windows.Forms.CheckedListBox()
        Me.cmdDockpathsRemove = New System.Windows.Forms.Button()
        Me.cmdDockpathsAdd = New System.Windows.Forms.Button()
        Me.tabLights = New System.Windows.Forms.TabPage()
        Me.cboLightAtt = New System.Windows.Forms.ComboBox()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.txtLightAttDist = New System.Windows.Forms.TextBox()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.txtLightSB = New System.Windows.Forms.TextBox()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.txtLightSG = New System.Windows.Forms.TextBox()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.txtLightSR = New System.Windows.Forms.TextBox()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.txtLightCB = New System.Windows.Forms.TextBox()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.txtLightCG = New System.Windows.Forms.TextBox()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.txtLightCR = New System.Windows.Forms.TextBox()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.txtLightTZ = New System.Windows.Forms.TextBox()
        Me.lblLightTZ = New System.Windows.Forms.Label()
        Me.txtLightTY = New System.Windows.Forms.TextBox()
        Me.lblLightTY = New System.Windows.Forms.Label()
        Me.txtLightTX = New System.Windows.Forms.TextBox()
        Me.lblLightTX = New System.Windows.Forms.Label()
        Me.cboLightType = New System.Windows.Forms.ComboBox()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.cmdLightsRename = New System.Windows.Forms.Button()
        Me.cstLights = New System.Windows.Forms.CheckedListBox()
        Me.cmdLightsRemove = New System.Windows.Forms.Button()
        Me.cmdLightsAdd = New System.Windows.Forms.Button()
        Me.tabStarFields = New System.Windows.Forms.TabPage()
        Me.cmdStarFieldsRemoveStar = New System.Windows.Forms.Button()
        Me.cmdStarFieldsAddStar = New System.Windows.Forms.Button()
        Me.dgvStarfields = New System.Windows.Forms.DataGridView()
        Me.PositionX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PositionY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PositionZ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StarSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColourR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColourG = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColourB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cstStarFields = New System.Windows.Forms.CheckedListBox()
        Me.cmdStarFieldsExport = New System.Windows.Forms.Button()
        Me.cmdStarFieldsImport = New System.Windows.Forms.Button()
        Me.cmdStarFieldsRemove = New System.Windows.Forms.Button()
        Me.cmdStarFieldsAdd = New System.Windows.Forms.Button()
        Me.tabStarFieldsT = New System.Windows.Forms.TabPage()
        Me.txtStarFieldsTStarName = New System.Windows.Forms.TextBox()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.cmdStarFieldsTRemoveStar = New System.Windows.Forms.Button()
        Me.cmdStarFieldsTAddStar = New System.Windows.Forms.Button()
        Me.dgvStarFieldsT = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColourA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cstStarFieldsT = New System.Windows.Forms.CheckedListBox()
        Me.cmdStarFieldsTExport = New System.Windows.Forms.Button()
        Me.cmdStarFieldsTImport = New System.Windows.Forms.Button()
        Me.cmdStarFieldsTRemove = New System.Windows.Forms.Button()
        Me.cmdStarFieldsTAdd = New System.Windows.Forms.Button()
        Me.tabAnimations = New System.Windows.Forms.TabPage()
        Me.txtAnimationsST = New System.Windows.Forms.TextBox()
        Me.fraAnimationsJoints = New System.Windows.Forms.GroupBox()
        Me.txtAnimationsJointsTime = New System.Windows.Forms.TextBox()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.txtAnimationsJointsRZ = New System.Windows.Forms.TextBox()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.txtAnimationsJointsRY = New System.Windows.Forms.TextBox()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.txtAnimationsJointsRX = New System.Windows.Forms.TextBox()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.txtAnimationsJointsPZ = New System.Windows.Forms.TextBox()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.txtAnimationsJointsPY = New System.Windows.Forms.TextBox()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.txtAnimationsJointsPX = New System.Windows.Forms.TextBox()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.cmdAnimationsPlay = New System.Windows.Forms.Button()
        Me.sldAnimationsTime = New System.Windows.Forms.TrackBar()
        Me.cmdAnimationsJointsRemoveKeyframe = New System.Windows.Forms.Button()
        Me.cmdAnimationsJointsAddKeyframe = New System.Windows.Forms.Button()
        Me.cmdAnimationsJointsRemove = New System.Windows.Forms.Button()
        Me.cmdAnimationsJointsAdd = New System.Windows.Forms.Button()
        Me.lstAnimationsJoints = New System.Windows.Forms.ListBox()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.txtAnimationsLET = New System.Windows.Forms.TextBox()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.txtAnimationsLST = New System.Windows.Forms.TextBox()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.txtAnimationsET = New System.Windows.Forms.TextBox()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.cmdAnimationsRename = New System.Windows.Forms.Button()
        Me.cmdAnimationsRemove = New System.Windows.Forms.Button()
        Me.cmdAnimationsAdd = New System.Windows.Forms.Button()
        Me.lstAnimations = New System.Windows.Forms.ListBox()
        Me.pnlDisplay = New System.Windows.Forms.Panel()
        Me.ColorDialog = New System.Windows.Forms.ColorDialog()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.sbrDummy = New System.Windows.Forms.ToolStripStatusLabel()
        Me.sbrLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.splMain = New System.Windows.Forms.SplitContainer()
        Me.MenuStrip.SuspendLayout()
        Me.ToolStrip.SuspendLayout()
        Me.pbxDisplay_cms.SuspendLayout()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMain.SuspendLayout()
        Me.tabTextures.SuspendLayout()
        Me.tabMaterials.SuspendLayout()
        Me.fraMaterialParameters.SuspendLayout()
        Me.tabMultiMeshes.SuspendLayout()
        Me.fraShipMeshes.SuspendLayout()
        Me.tabGoblins.SuspendLayout()
        Me.tabBGMS.SuspendLayout()
        Me.tabUI.SuspendLayout()
        Me.tabJoints.SuspendLayout()
        Me.fraJoint.SuspendLayout()
        Me.tvwJoints_cms.SuspendLayout()
        Me.tabCM.SuspendLayout()
        Me.fraCMBSPH.SuspendLayout()
        Me.fraCMBBOX.SuspendLayout()
        Me.tabEngineShapes.SuspendLayout()
        Me.tabEngineGlows.SuspendLayout()
        Me.fraEngineGlowsMisc.SuspendLayout()
        CType(Me.sldEngineGlowsThrusterPowerFactor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabEngineBurns.SuspendLayout()
        Me.tabNavLights.SuspendLayout()
        CType(Me.pbxNavLightsSample, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabMarkers.SuspendLayout()
        Me.fraMarkersTransform.SuspendLayout()
        Me.tabDockpaths.SuspendLayout()
        Me.fraDockpathsKeyframes.SuspendLayout()
        CType(Me.sldDockpathsKeyframe, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabLights.SuspendLayout()
        Me.tabStarFields.SuspendLayout()
        CType(Me.dgvStarfields, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabStarFieldsT.SuspendLayout()
        CType(Me.dgvStarFieldsT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabAnimations.SuspendLayout()
        Me.fraAnimationsJoints.SuspendLayout()
        CType(Me.sldAnimationsTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDisplay.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.splMain.Panel1.SuspendLayout()
        Me.splMain.Panel2.SuspendLayout()
        Me.splMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuEdit, Me.mnuTools, Me.mnuHelp})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Size = New System.Drawing.Size(784, 24)
        Me.MenuStrip.TabIndex = 0
        Me.MenuStrip.Text = "MenuStrip1"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFileNew, Me.mnuFileOpen, Me.mnuFileSepr1, Me.mnuFileSave, Me.mnuFileSaveAs, Me.mnuFileSepr2, Me.mnuFileExit})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuFile.Text = "&File"
        '
        'mnuFileNew
        '
        Me.mnuFileNew.Image = CType(resources.GetObject("mnuFileNew.Image"), System.Drawing.Image)
        Me.mnuFileNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuFileNew.Name = "mnuFileNew"
        Me.mnuFileNew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.mnuFileNew.Size = New System.Drawing.Size(152, 22)
        Me.mnuFileNew.Text = "&New"
        '
        'mnuFileOpen
        '
        Me.mnuFileOpen.Image = CType(resources.GetObject("mnuFileOpen.Image"), System.Drawing.Image)
        Me.mnuFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuFileOpen.Name = "mnuFileOpen"
        Me.mnuFileOpen.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.mnuFileOpen.Size = New System.Drawing.Size(146, 22)
        Me.mnuFileOpen.Text = "&Open"
        '
        'mnuFileSepr1
        '
        Me.mnuFileSepr1.Name = "mnuFileSepr1"
        Me.mnuFileSepr1.Size = New System.Drawing.Size(143, 6)
        '
        'mnuFileSave
        '
        Me.mnuFileSave.Image = CType(resources.GetObject("mnuFileSave.Image"), System.Drawing.Image)
        Me.mnuFileSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuFileSave.Name = "mnuFileSave"
        Me.mnuFileSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mnuFileSave.Size = New System.Drawing.Size(146, 22)
        Me.mnuFileSave.Text = "&Save"
        '
        'mnuFileSaveAs
        '
        Me.mnuFileSaveAs.Name = "mnuFileSaveAs"
        Me.mnuFileSaveAs.Size = New System.Drawing.Size(146, 22)
        Me.mnuFileSaveAs.Text = "Save &As"
        '
        'mnuFileSepr2
        '
        Me.mnuFileSepr2.Name = "mnuFileSepr2"
        Me.mnuFileSepr2.Size = New System.Drawing.Size(143, 6)
        '
        'mnuFileExit
        '
        Me.mnuFileExit.Name = "mnuFileExit"
        Me.mnuFileExit.Size = New System.Drawing.Size(146, 22)
        Me.mnuFileExit.Text = "E&xit"
        '
        'mnuEdit
        '
        Me.mnuEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEditCut, Me.mnuEditCopy, Me.mnuEditPaste})
        Me.mnuEdit.Name = "mnuEdit"
        Me.mnuEdit.Size = New System.Drawing.Size(39, 20)
        Me.mnuEdit.Text = "&Edit"
        '
        'mnuEditCut
        '
        Me.mnuEditCut.Image = CType(resources.GetObject("mnuEditCut.Image"), System.Drawing.Image)
        Me.mnuEditCut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuEditCut.Name = "mnuEditCut"
        Me.mnuEditCut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.mnuEditCut.Size = New System.Drawing.Size(144, 22)
        Me.mnuEditCut.Text = "Cu&t"
        '
        'mnuEditCopy
        '
        Me.mnuEditCopy.Image = CType(resources.GetObject("mnuEditCopy.Image"), System.Drawing.Image)
        Me.mnuEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuEditCopy.Name = "mnuEditCopy"
        Me.mnuEditCopy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.mnuEditCopy.Size = New System.Drawing.Size(144, 22)
        Me.mnuEditCopy.Text = "&Copy"
        '
        'mnuEditPaste
        '
        Me.mnuEditPaste.Image = CType(resources.GetObject("mnuEditPaste.Image"), System.Drawing.Image)
        Me.mnuEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuEditPaste.Name = "mnuEditPaste"
        Me.mnuEditPaste.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.mnuEditPaste.Size = New System.Drawing.Size(144, 22)
        Me.mnuEditPaste.Text = "&Paste"
        '
        'mnuTools
        '
        Me.mnuTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuToolsOptions, Me.mnuToolsSeperator1, Me.mnuToolsRenormalMeshes, Me.mnuToolsRetangentMeshes, Me.mnuToolsSeperator2, Me.mnuToolsTranslate, Me.mnuToolsRotate, Me.mnuToolsScale, Me.mnuToolsMirror, Me.mnuToolsSeperator3, Me.mnuToolsVARYToMULT})
        Me.mnuTools.Name = "mnuTools"
        Me.mnuTools.Size = New System.Drawing.Size(47, 20)
        Me.mnuTools.Text = "&Tools"
        '
        'mnuToolsOptions
        '
        Me.mnuToolsOptions.Name = "mnuToolsOptions"
        Me.mnuToolsOptions.Size = New System.Drawing.Size(256, 22)
        Me.mnuToolsOptions.Text = "&Options"
        '
        'mnuToolsSeperator1
        '
        Me.mnuToolsSeperator1.Name = "mnuToolsSeperator1"
        Me.mnuToolsSeperator1.Size = New System.Drawing.Size(253, 6)
        '
        'mnuToolsRenormalMeshes
        '
        Me.mnuToolsRenormalMeshes.Name = "mnuToolsRenormalMeshes"
        Me.mnuToolsRenormalMeshes.Size = New System.Drawing.Size(256, 22)
        Me.mnuToolsRenormalMeshes.Text = "Re-normal meshes"
        '
        'mnuToolsRetangentMeshes
        '
        Me.mnuToolsRetangentMeshes.Name = "mnuToolsRetangentMeshes"
        Me.mnuToolsRetangentMeshes.Size = New System.Drawing.Size(256, 22)
        Me.mnuToolsRetangentMeshes.Text = "Re-tangent meshes"
        '
        'mnuToolsSeperator2
        '
        Me.mnuToolsSeperator2.Name = "mnuToolsSeperator2"
        Me.mnuToolsSeperator2.Size = New System.Drawing.Size(253, 6)
        '
        'mnuToolsTranslate
        '
        Me.mnuToolsTranslate.Name = "mnuToolsTranslate"
        Me.mnuToolsTranslate.Size = New System.Drawing.Size(256, 22)
        Me.mnuToolsTranslate.Text = "Translate HOD"
        '
        'mnuToolsRotate
        '
        Me.mnuToolsRotate.Name = "mnuToolsRotate"
        Me.mnuToolsRotate.Size = New System.Drawing.Size(256, 22)
        Me.mnuToolsRotate.Text = "Rotate HOD"
        '
        'mnuToolsScale
        '
        Me.mnuToolsScale.Name = "mnuToolsScale"
        Me.mnuToolsScale.Size = New System.Drawing.Size(256, 22)
        Me.mnuToolsScale.Text = "Scale HOD"
        '
        'mnuToolsMirror
        '
        Me.mnuToolsMirror.Name = "mnuToolsMirror"
        Me.mnuToolsMirror.Size = New System.Drawing.Size(256, 22)
        Me.mnuToolsMirror.Text = "Mirror HOD"
        '
        'mnuToolsSeperator3
        '
        Me.mnuToolsSeperator3.Name = "mnuToolsSeperator3"
        Me.mnuToolsSeperator3.Size = New System.Drawing.Size(253, 6)
        '
        'mnuToolsVARYToMULT
        '
        Me.mnuToolsVARYToMULT.Name = "mnuToolsVARYToMULT"
        Me.mnuToolsVARYToMULT.Size = New System.Drawing.Size(256, 22)
        Me.mnuToolsVARYToMULT.Text = "Convert progressive to multi mesh"
        '
        'mnuHelp
        '
        Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuHelpAbout})
        Me.mnuHelp.Name = "mnuHelp"
        Me.mnuHelp.Size = New System.Drawing.Size(44, 20)
        Me.mnuHelp.Text = "&Help"
        '
        'mnuHelpAbout
        '
        Me.mnuHelpAbout.Name = "mnuHelpAbout"
        Me.mnuHelpAbout.Size = New System.Drawing.Size(116, 22)
        Me.mnuHelpAbout.Text = "&About..."
        '
        'ToolStrip
        '
        Me.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tspNew, Me.tspOpen, Me.tspSave, Me.tspSeperator1, Me.tspCut, Me.tspCopy, Me.tspPaste, Me.tspSeparator2, Me.tspLabel1, Me.tspMode, Me.tspSeperator2, Me.tspLabel2, Me.tspObjectScale, Me.tspSeperator3, Me.tspLabel3, Me.tspCameraSensitivity})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(784, 25)
        Me.ToolStrip.TabIndex = 1
        '
        'tspNew
        '
        Me.tspNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tspNew.Image = CType(resources.GetObject("tspNew.Image"), System.Drawing.Image)
        Me.tspNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tspNew.Name = "tspNew"
        Me.tspNew.Size = New System.Drawing.Size(23, 22)
        Me.tspNew.Text = "&New"
        '
        'tspOpen
        '
        Me.tspOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tspOpen.Image = CType(resources.GetObject("tspOpen.Image"), System.Drawing.Image)
        Me.tspOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tspOpen.Name = "tspOpen"
        Me.tspOpen.Size = New System.Drawing.Size(23, 22)
        Me.tspOpen.Text = "&Open"
        '
        'tspSave
        '
        Me.tspSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tspSave.Image = CType(resources.GetObject("tspSave.Image"), System.Drawing.Image)
        Me.tspSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tspSave.Name = "tspSave"
        Me.tspSave.Size = New System.Drawing.Size(23, 22)
        Me.tspSave.Text = "&Save"
        '
        'tspSeperator1
        '
        Me.tspSeperator1.Name = "tspSeperator1"
        Me.tspSeperator1.Size = New System.Drawing.Size(6, 25)
        '
        'tspCut
        '
        Me.tspCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tspCut.Image = CType(resources.GetObject("tspCut.Image"), System.Drawing.Image)
        Me.tspCut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tspCut.Name = "tspCut"
        Me.tspCut.Size = New System.Drawing.Size(23, 22)
        Me.tspCut.Text = "C&ut"
        '
        'tspCopy
        '
        Me.tspCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tspCopy.Image = CType(resources.GetObject("tspCopy.Image"), System.Drawing.Image)
        Me.tspCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tspCopy.Name = "tspCopy"
        Me.tspCopy.Size = New System.Drawing.Size(23, 22)
        Me.tspCopy.Text = "&Copy"
        '
        'tspPaste
        '
        Me.tspPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tspPaste.Image = CType(resources.GetObject("tspPaste.Image"), System.Drawing.Image)
        Me.tspPaste.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tspPaste.Name = "tspPaste"
        Me.tspPaste.Size = New System.Drawing.Size(23, 22)
        Me.tspPaste.Text = "&Paste"
        '
        'tspSeparator2
        '
        Me.tspSeparator2.Name = "tspSeparator2"
        Me.tspSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'tspLabel1
        '
        Me.tspLabel1.Name = "tspLabel1"
        Me.tspLabel1.Size = New System.Drawing.Size(41, 22)
        Me.tspLabel1.Text = "Mode:"
        '
        'tspMode
        '
        Me.tspMode.AutoSize = False
        Me.tspMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tspMode.Items.AddRange(New Object() {"Model", "Animation"})
        Me.tspMode.Name = "tspMode"
        Me.tspMode.Size = New System.Drawing.Size(121, 23)
        '
        'tspSeperator2
        '
        Me.tspSeperator2.Name = "tspSeperator2"
        Me.tspSeperator2.Size = New System.Drawing.Size(6, 25)
        '
        'tspLabel2
        '
        Me.tspLabel2.Name = "tspLabel2"
        Me.tspLabel2.Size = New System.Drawing.Size(75, 22)
        Me.tspLabel2.Text = "Object Scale:"
        '
        'tspObjectScale
        '
        Me.tspObjectScale.Name = "tspObjectScale"
        Me.tspObjectScale.Size = New System.Drawing.Size(100, 25)
        Me.tspObjectScale.Text = "1"
        '
        'tspSeperator3
        '
        Me.tspSeperator3.Name = "tspSeperator3"
        Me.tspSeperator3.Size = New System.Drawing.Size(6, 25)
        '
        'tspLabel3
        '
        Me.tspLabel3.Name = "tspLabel3"
        Me.tspLabel3.Size = New System.Drawing.Size(107, 22)
        Me.tspLabel3.Text = "Camera Sensitivity:"
        '
        'tspCameraSensitivity
        '
        Me.tspCameraSensitivity.Name = "tspCameraSensitivity"
        Me.tspCameraSensitivity.Size = New System.Drawing.Size(100, 25)
        Me.tspCameraSensitivity.Text = "1"
        '
        'pbxDisplay_cms
        '
        Me.pbxDisplay_cms.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pbxDisplay_cmsReset, Me.pbxDisplay_cmsSeparator1, Me.pbxDisplay_cmsWireframe, Me.pbxDisplay_cmsSolid, Me.pbxDisplay_cmsSeparator2, Me.pbxDisplay_cmsWireframeOverlay, Me.pbxDisplay_cmsSeparator3, Me.pbxDisplay_cmsLights, Me.pbxDisplay_cmsEditLight})
        Me.pbxDisplay_cms.Name = "pbxDisplay_cms"
        Me.pbxDisplay_cms.Size = New System.Drawing.Size(173, 154)
        '
        'pbxDisplay_cmsReset
        '
        Me.pbxDisplay_cmsReset.Name = "pbxDisplay_cmsReset"
        Me.pbxDisplay_cmsReset.Size = New System.Drawing.Size(172, 22)
        Me.pbxDisplay_cmsReset.Text = "Reset camera"
        '
        'pbxDisplay_cmsSeparator1
        '
        Me.pbxDisplay_cmsSeparator1.Name = "pbxDisplay_cmsSeparator1"
        Me.pbxDisplay_cmsSeparator1.Size = New System.Drawing.Size(169, 6)
        '
        'pbxDisplay_cmsWireframe
        '
        Me.pbxDisplay_cmsWireframe.Name = "pbxDisplay_cmsWireframe"
        Me.pbxDisplay_cmsWireframe.Size = New System.Drawing.Size(172, 22)
        Me.pbxDisplay_cmsWireframe.Text = "Wireframe"
        '
        'pbxDisplay_cmsSolid
        '
        Me.pbxDisplay_cmsSolid.Checked = True
        Me.pbxDisplay_cmsSolid.CheckState = System.Windows.Forms.CheckState.Checked
        Me.pbxDisplay_cmsSolid.Name = "pbxDisplay_cmsSolid"
        Me.pbxDisplay_cmsSolid.Size = New System.Drawing.Size(172, 22)
        Me.pbxDisplay_cmsSolid.Text = "Solid"
        '
        'pbxDisplay_cmsSeparator2
        '
        Me.pbxDisplay_cmsSeparator2.Name = "pbxDisplay_cmsSeparator2"
        Me.pbxDisplay_cmsSeparator2.Size = New System.Drawing.Size(169, 6)
        '
        'pbxDisplay_cmsWireframeOverlay
        '
        Me.pbxDisplay_cmsWireframeOverlay.Name = "pbxDisplay_cmsWireframeOverlay"
        Me.pbxDisplay_cmsWireframeOverlay.Size = New System.Drawing.Size(172, 22)
        Me.pbxDisplay_cmsWireframeOverlay.Text = "Wireframe Overlay"
        '
        'pbxDisplay_cmsSeparator3
        '
        Me.pbxDisplay_cmsSeparator3.Name = "pbxDisplay_cmsSeparator3"
        Me.pbxDisplay_cmsSeparator3.Size = New System.Drawing.Size(169, 6)
        '
        'pbxDisplay_cmsLights
        '
        Me.pbxDisplay_cmsLights.Checked = True
        Me.pbxDisplay_cmsLights.CheckState = System.Windows.Forms.CheckState.Checked
        Me.pbxDisplay_cmsLights.Name = "pbxDisplay_cmsLights"
        Me.pbxDisplay_cmsLights.Size = New System.Drawing.Size(172, 22)
        Me.pbxDisplay_cmsLights.Text = "Default light"
        Me.pbxDisplay_cmsLights.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
        '
        'pbxDisplay_cmsEditLight
        '
        Me.pbxDisplay_cmsEditLight.Name = "pbxDisplay_cmsEditLight"
        Me.pbxDisplay_cmsEditLight.Size = New System.Drawing.Size(172, 22)
        Me.pbxDisplay_cmsEditLight.Text = "Edit light"
        '
        'OpenHODFileDialog
        '
        Me.OpenHODFileDialog.DefaultExt = "hod"
        Me.OpenHODFileDialog.Filter = "Homeworld2 HOD files (*.HOD)|*.hod"
        '
        'SaveHODFileDialog
        '
        Me.SaveHODFileDialog.DefaultExt = "hod"
        Me.SaveHODFileDialog.Filter = "Homeworld2 HOD files (*.HOD)|*.hod"
        '
        'OpenTextureFileDialog
        '
        Me.OpenTextureFileDialog.DefaultExt = "dds"
        Me.OpenTextureFileDialog.Filter = "DirectDraw Surface (*.dds)|*.dds|Truevision Targa files (*.tga)|*.tga"
        '
        'OpenShaderFileDialog
        '
        Me.OpenShaderFileDialog.DefaultExt = "st"
        Me.OpenShaderFileDialog.Filter = "Homeworld2 Shader files (*.st)|*.st"
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'OpenOBJFileDialog
        '
        Me.OpenOBJFileDialog.DefaultExt = "obj"
        Me.OpenOBJFileDialog.Filter = "Wavefront Object files (*.obj)|*.obj"
        '
        'SaveOBJFileDialog
        '
        Me.SaveOBJFileDialog.DefaultExt = "obj"
        Me.SaveOBJFileDialog.Filter = "Wavefront Object files (*.obj)|*.obj"
        '
        'pbxDisplay
        '
        Me.pbxDisplay.Location = New System.Drawing.Point(32, 24)
        Me.pbxDisplay.Name = "pbxDisplay"
        Me.pbxDisplay.Size = New System.Drawing.Size(80, 80)
        Me.pbxDisplay.TabIndex = 0
        Me.pbxDisplay.TabStop = False
        '
        'tabMain
        '
        Me.tabMain.Controls.Add(Me.tabTextures)
        Me.tabMain.Controls.Add(Me.tabMaterials)
        Me.tabMain.Controls.Add(Me.tabMultiMeshes)
        Me.tabMain.Controls.Add(Me.tabGoblins)
        Me.tabMain.Controls.Add(Me.tabBGMS)
        Me.tabMain.Controls.Add(Me.tabUI)
        Me.tabMain.Controls.Add(Me.tabJoints)
        Me.tabMain.Controls.Add(Me.tabCM)
        Me.tabMain.Controls.Add(Me.tabEngineShapes)
        Me.tabMain.Controls.Add(Me.tabEngineGlows)
        Me.tabMain.Controls.Add(Me.tabEngineBurns)
        Me.tabMain.Controls.Add(Me.tabNavLights)
        Me.tabMain.Controls.Add(Me.tabMarkers)
        Me.tabMain.Controls.Add(Me.tabDockpaths)
        Me.tabMain.Controls.Add(Me.tabLights)
        Me.tabMain.Controls.Add(Me.tabStarFields)
        Me.tabMain.Controls.Add(Me.tabStarFieldsT)
        Me.tabMain.Controls.Add(Me.tabAnimations)
        Me.tabMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabMain.Location = New System.Drawing.Point(0, 0)
        Me.tabMain.Multiline = True
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedIndex = 0
        Me.tabMain.Size = New System.Drawing.Size(296, 491)
        Me.tabMain.TabIndex = 4
        '
        'tabTextures
        '
        Me.tabTextures.AutoScroll = True
        Me.tabTextures.Controls.Add(Me.cmdTexturesExportAll)
        Me.tabTextures.Controls.Add(Me.lblTextureMipCount)
        Me.tabTextures.Controls.Add(Me.Label4)
        Me.tabTextures.Controls.Add(Me.lblTextureDimensions)
        Me.tabTextures.Controls.Add(Me.lblTextureFormat)
        Me.tabTextures.Controls.Add(Me.Label1)
        Me.tabTextures.Controls.Add(Me.lblTexturePath)
        Me.tabTextures.Controls.Add(Me.Label3)
        Me.tabTextures.Controls.Add(Me.Label2)
        Me.tabTextures.Controls.Add(Me.cmdTexturePreview)
        Me.tabTextures.Controls.Add(Me.cmdTextureExport)
        Me.tabTextures.Controls.Add(Me.cmdTextureRemove)
        Me.tabTextures.Controls.Add(Me.cmdTextureImport)
        Me.tabTextures.Controls.Add(Me.cmdTextureAdd)
        Me.tabTextures.Controls.Add(Me.lstTextures)
        Me.tabTextures.Location = New System.Drawing.Point(4, 112)
        Me.tabTextures.Name = "tabTextures"
        Me.tabTextures.Padding = New System.Windows.Forms.Padding(3)
        Me.tabTextures.Size = New System.Drawing.Size(288, 375)
        Me.tabTextures.TabIndex = 1
        Me.tabTextures.Text = "Textures"
        Me.tabTextures.UseVisualStyleBackColor = True
        '
        'cmdTexturesExportAll
        '
        Me.cmdTexturesExportAll.Location = New System.Drawing.Point(184, 208)
        Me.cmdTexturesExportAll.Name = "cmdTexturesExportAll"
        Me.cmdTexturesExportAll.Size = New System.Drawing.Size(80, 24)
        Me.cmdTexturesExportAll.TabIndex = 16
        Me.cmdTexturesExportAll.Text = "Export All"
        Me.cmdTexturesExportAll.UseVisualStyleBackColor = True
        '
        'lblTextureMipCount
        '
        Me.lblTextureMipCount.Location = New System.Drawing.Point(120, 264)
        Me.lblTextureMipCount.Name = "lblTextureMipCount"
        Me.lblTextureMipCount.Size = New System.Drawing.Size(144, 24)
        Me.lblTextureMipCount.TabIndex = 15
        Me.lblTextureMipCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(8, 264)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 24)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Number of Mips:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTextureDimensions
        '
        Me.lblTextureDimensions.Location = New System.Drawing.Point(120, 288)
        Me.lblTextureDimensions.Name = "lblTextureDimensions"
        Me.lblTextureDimensions.Size = New System.Drawing.Size(144, 24)
        Me.lblTextureDimensions.TabIndex = 13
        Me.lblTextureDimensions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTextureFormat
        '
        Me.lblTextureFormat.Location = New System.Drawing.Point(120, 240)
        Me.lblTextureFormat.Name = "lblTextureFormat"
        Me.lblTextureFormat.Size = New System.Drawing.Size(144, 24)
        Me.lblTextureFormat.TabIndex = 12
        Me.lblTextureFormat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 312)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 24)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Texture Path:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTexturePath
        '
        Me.lblTexturePath.Location = New System.Drawing.Point(8, 336)
        Me.lblTexturePath.Name = "lblTexturePath"
        Me.lblTexturePath.Size = New System.Drawing.Size(256, 48)
        Me.lblTexturePath.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 288)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 24)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Texture Dimensions:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 240)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 24)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Texture Format:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdTexturePreview
        '
        Me.cmdTexturePreview.Location = New System.Drawing.Point(184, 176)
        Me.cmdTexturePreview.Name = "cmdTexturePreview"
        Me.cmdTexturePreview.Size = New System.Drawing.Size(80, 24)
        Me.cmdTexturePreview.TabIndex = 5
        Me.cmdTexturePreview.Text = "Preview"
        Me.cmdTexturePreview.UseVisualStyleBackColor = True
        '
        'cmdTextureExport
        '
        Me.cmdTextureExport.Location = New System.Drawing.Point(96, 208)
        Me.cmdTextureExport.Name = "cmdTextureExport"
        Me.cmdTextureExport.Size = New System.Drawing.Size(80, 24)
        Me.cmdTextureExport.TabIndex = 4
        Me.cmdTextureExport.Text = "Export"
        Me.cmdTextureExport.UseVisualStyleBackColor = True
        '
        'cmdTextureRemove
        '
        Me.cmdTextureRemove.Location = New System.Drawing.Point(96, 176)
        Me.cmdTextureRemove.Name = "cmdTextureRemove"
        Me.cmdTextureRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdTextureRemove.TabIndex = 3
        Me.cmdTextureRemove.Text = "Remove"
        Me.cmdTextureRemove.UseVisualStyleBackColor = True
        '
        'cmdTextureImport
        '
        Me.cmdTextureImport.Location = New System.Drawing.Point(8, 208)
        Me.cmdTextureImport.Name = "cmdTextureImport"
        Me.cmdTextureImport.Size = New System.Drawing.Size(80, 24)
        Me.cmdTextureImport.TabIndex = 2
        Me.cmdTextureImport.Text = "Import"
        Me.cmdTextureImport.UseVisualStyleBackColor = True
        '
        'cmdTextureAdd
        '
        Me.cmdTextureAdd.Location = New System.Drawing.Point(8, 176)
        Me.cmdTextureAdd.Name = "cmdTextureAdd"
        Me.cmdTextureAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdTextureAdd.TabIndex = 1
        Me.cmdTextureAdd.Text = "Add"
        Me.cmdTextureAdd.UseVisualStyleBackColor = True
        '
        'lstTextures
        '
        Me.lstTextures.FormattingEnabled = True
        Me.lstTextures.HorizontalScrollbar = True
        Me.lstTextures.Location = New System.Drawing.Point(8, 8)
        Me.lstTextures.Name = "lstTextures"
        Me.lstTextures.Size = New System.Drawing.Size(256, 160)
        Me.lstTextures.TabIndex = 0
        '
        'tabMaterials
        '
        Me.tabMaterials.AutoScroll = True
        Me.tabMaterials.Controls.Add(Me.cmdMaterialShaderRename)
        Me.tabMaterials.Controls.Add(Me.lstMaterials)
        Me.tabMaterials.Controls.Add(Me.fraMaterialParameters)
        Me.tabMaterials.Controls.Add(Me.lblMaterialShaderName)
        Me.tabMaterials.Controls.Add(Me.lblMaterialName)
        Me.tabMaterials.Controls.Add(Me.Label6)
        Me.tabMaterials.Controls.Add(Me.Label5)
        Me.tabMaterials.Controls.Add(Me.cmdMaterialRename)
        Me.tabMaterials.Controls.Add(Me.cmdMaterialRemove)
        Me.tabMaterials.Controls.Add(Me.cmdMaterialAdd)
        Me.tabMaterials.Location = New System.Drawing.Point(4, 112)
        Me.tabMaterials.Name = "tabMaterials"
        Me.tabMaterials.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMaterials.Size = New System.Drawing.Size(288, 375)
        Me.tabMaterials.TabIndex = 2
        Me.tabMaterials.Text = "Materials"
        Me.tabMaterials.UseVisualStyleBackColor = True
        '
        'cmdMaterialShaderRename
        '
        Me.cmdMaterialShaderRename.Location = New System.Drawing.Point(208, 232)
        Me.cmdMaterialShaderRename.Name = "cmdMaterialShaderRename"
        Me.cmdMaterialShaderRename.Size = New System.Drawing.Size(56, 23)
        Me.cmdMaterialShaderRename.TabIndex = 15
        Me.cmdMaterialShaderRename.Text = "Rename"
        Me.cmdMaterialShaderRename.UseVisualStyleBackColor = True
        '
        'lstMaterials
        '
        Me.lstMaterials.FormattingEnabled = True
        Me.lstMaterials.Location = New System.Drawing.Point(8, 8)
        Me.lstMaterials.Name = "lstMaterials"
        Me.lstMaterials.Size = New System.Drawing.Size(256, 160)
        Me.lstMaterials.TabIndex = 13
        '
        'fraMaterialParameters
        '
        Me.fraMaterialParameters.Controls.Add(Me.optMaterialParameterColour)
        Me.fraMaterialParameters.Controls.Add(Me.optMaterialParameterTexture)
        Me.fraMaterialParameters.Controls.Add(Me.Label12)
        Me.fraMaterialParameters.Controls.Add(Me.cmdMaterialNoTexture)
        Me.fraMaterialParameters.Controls.Add(Me.cboMaterialTextureIndex)
        Me.fraMaterialParameters.Controls.Add(Me.txtMaterialColourA)
        Me.fraMaterialParameters.Controls.Add(Me.txtMaterialColourB)
        Me.fraMaterialParameters.Controls.Add(Me.txtMaterialColourG)
        Me.fraMaterialParameters.Controls.Add(Me.txtMaterialColourR)
        Me.fraMaterialParameters.Controls.Add(Me.Label11)
        Me.fraMaterialParameters.Controls.Add(Me.Label10)
        Me.fraMaterialParameters.Controls.Add(Me.Label9)
        Me.fraMaterialParameters.Controls.Add(Me.Label8)
        Me.fraMaterialParameters.Controls.Add(Me.Label7)
        Me.fraMaterialParameters.Controls.Add(Me.cmdMaterialParameterRename)
        Me.fraMaterialParameters.Controls.Add(Me.cmdMaterialParameterRemove)
        Me.fraMaterialParameters.Controls.Add(Me.cmdMaterialParameterAdd)
        Me.fraMaterialParameters.Controls.Add(Me.lstMaterialParameters)
        Me.fraMaterialParameters.Controls.Add(Me.cmdMaterialParametersFromFile)
        Me.fraMaterialParameters.Location = New System.Drawing.Point(8, 264)
        Me.fraMaterialParameters.Name = "fraMaterialParameters"
        Me.fraMaterialParameters.Size = New System.Drawing.Size(256, 304)
        Me.fraMaterialParameters.TabIndex = 12
        Me.fraMaterialParameters.TabStop = False
        Me.fraMaterialParameters.Text = "Parameters"
        '
        'optMaterialParameterColour
        '
        Me.optMaterialParameterColour.Location = New System.Drawing.Point(144, 152)
        Me.optMaterialParameterColour.Name = "optMaterialParameterColour"
        Me.optMaterialParameterColour.Size = New System.Drawing.Size(76, 24)
        Me.optMaterialParameterColour.TabIndex = 28
        Me.optMaterialParameterColour.TabStop = True
        Me.optMaterialParameterColour.Text = "Colour"
        Me.optMaterialParameterColour.UseVisualStyleBackColor = True
        '
        'optMaterialParameterTexture
        '
        Me.optMaterialParameterTexture.Location = New System.Drawing.Point(64, 152)
        Me.optMaterialParameterTexture.Name = "optMaterialParameterTexture"
        Me.optMaterialParameterTexture.Size = New System.Drawing.Size(76, 24)
        Me.optMaterialParameterTexture.TabIndex = 27
        Me.optMaterialParameterTexture.TabStop = True
        Me.optMaterialParameterTexture.Text = "Texture"
        Me.optMaterialParameterTexture.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(12, 152)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(52, 21)
        Me.Label12.TabIndex = 26
        Me.Label12.Text = "Type:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdMaterialNoTexture
        '
        Me.cmdMaterialNoTexture.Location = New System.Drawing.Point(224, 272)
        Me.cmdMaterialNoTexture.Name = "cmdMaterialNoTexture"
        Me.cmdMaterialNoTexture.Size = New System.Drawing.Size(24, 23)
        Me.cmdMaterialNoTexture.TabIndex = 25
        Me.cmdMaterialNoTexture.Text = "X"
        Me.cmdMaterialNoTexture.UseVisualStyleBackColor = True
        '
        'cboMaterialTextureIndex
        '
        Me.cboMaterialTextureIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaterialTextureIndex.FormattingEnabled = True
        Me.cboMaterialTextureIndex.Location = New System.Drawing.Point(68, 272)
        Me.cboMaterialTextureIndex.Name = "cboMaterialTextureIndex"
        Me.cboMaterialTextureIndex.Size = New System.Drawing.Size(152, 21)
        Me.cboMaterialTextureIndex.TabIndex = 24
        '
        'txtMaterialColourA
        '
        Me.txtMaterialColourA.Location = New System.Drawing.Point(68, 248)
        Me.txtMaterialColourA.Name = "txtMaterialColourA"
        Me.txtMaterialColourA.Size = New System.Drawing.Size(152, 20)
        Me.txtMaterialColourA.TabIndex = 23
        '
        'txtMaterialColourB
        '
        Me.txtMaterialColourB.Location = New System.Drawing.Point(68, 224)
        Me.txtMaterialColourB.Name = "txtMaterialColourB"
        Me.txtMaterialColourB.Size = New System.Drawing.Size(152, 20)
        Me.txtMaterialColourB.TabIndex = 22
        '
        'txtMaterialColourG
        '
        Me.txtMaterialColourG.Location = New System.Drawing.Point(68, 200)
        Me.txtMaterialColourG.Name = "txtMaterialColourG"
        Me.txtMaterialColourG.Size = New System.Drawing.Size(152, 20)
        Me.txtMaterialColourG.TabIndex = 21
        '
        'txtMaterialColourR
        '
        Me.txtMaterialColourR.Location = New System.Drawing.Point(68, 176)
        Me.txtMaterialColourR.Name = "txtMaterialColourR"
        Me.txtMaterialColourR.Size = New System.Drawing.Size(152, 20)
        Me.txtMaterialColourR.TabIndex = 20
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(12, 272)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(52, 21)
        Me.Label11.TabIndex = 19
        Me.Label11.Text = "Texture:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(12, 248)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(52, 20)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Colour A:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(12, 224)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(52, 20)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Colour B:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(12, 200)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 20)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Colour G:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(12, 176)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 20)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Colour R:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdMaterialParameterRename
        '
        Me.cmdMaterialParameterRename.Location = New System.Drawing.Point(176, 88)
        Me.cmdMaterialParameterRename.Name = "cmdMaterialParameterRename"
        Me.cmdMaterialParameterRename.Size = New System.Drawing.Size(72, 24)
        Me.cmdMaterialParameterRename.TabIndex = 14
        Me.cmdMaterialParameterRename.Text = "Rename"
        Me.cmdMaterialParameterRename.UseVisualStyleBackColor = True
        '
        'cmdMaterialParameterRemove
        '
        Me.cmdMaterialParameterRemove.Location = New System.Drawing.Point(88, 88)
        Me.cmdMaterialParameterRemove.Name = "cmdMaterialParameterRemove"
        Me.cmdMaterialParameterRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdMaterialParameterRemove.TabIndex = 13
        Me.cmdMaterialParameterRemove.Text = "Remove"
        Me.cmdMaterialParameterRemove.UseVisualStyleBackColor = True
        '
        'cmdMaterialParameterAdd
        '
        Me.cmdMaterialParameterAdd.Location = New System.Drawing.Point(8, 88)
        Me.cmdMaterialParameterAdd.Name = "cmdMaterialParameterAdd"
        Me.cmdMaterialParameterAdd.Size = New System.Drawing.Size(72, 24)
        Me.cmdMaterialParameterAdd.TabIndex = 12
        Me.cmdMaterialParameterAdd.Text = "Add"
        Me.cmdMaterialParameterAdd.UseVisualStyleBackColor = True
        '
        'lstMaterialParameters
        '
        Me.lstMaterialParameters.FormattingEnabled = True
        Me.lstMaterialParameters.Location = New System.Drawing.Point(8, 24)
        Me.lstMaterialParameters.Name = "lstMaterialParameters"
        Me.lstMaterialParameters.Size = New System.Drawing.Size(240, 56)
        Me.lstMaterialParameters.TabIndex = 11
        '
        'cmdMaterialParametersFromFile
        '
        Me.cmdMaterialParametersFromFile.Location = New System.Drawing.Point(8, 120)
        Me.cmdMaterialParametersFromFile.Name = "cmdMaterialParametersFromFile"
        Me.cmdMaterialParametersFromFile.Size = New System.Drawing.Size(240, 24)
        Me.cmdMaterialParametersFromFile.TabIndex = 10
        Me.cmdMaterialParametersFromFile.Text = "Load material parameters from shader file"
        Me.cmdMaterialParametersFromFile.UseVisualStyleBackColor = True
        '
        'lblMaterialShaderName
        '
        Me.lblMaterialShaderName.Location = New System.Drawing.Point(88, 232)
        Me.lblMaterialShaderName.Name = "lblMaterialShaderName"
        Me.lblMaterialShaderName.Size = New System.Drawing.Size(120, 23)
        Me.lblMaterialShaderName.TabIndex = 11
        Me.lblMaterialShaderName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMaterialName
        '
        Me.lblMaterialName.Location = New System.Drawing.Point(88, 208)
        Me.lblMaterialName.Name = "lblMaterialName"
        Me.lblMaterialName.Size = New System.Drawing.Size(176, 23)
        Me.lblMaterialName.TabIndex = 10
        Me.lblMaterialName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(8, 232)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 23)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Shader Name:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(8, 208)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 23)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Material Name:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdMaterialRename
        '
        Me.cmdMaterialRename.Location = New System.Drawing.Point(184, 176)
        Me.cmdMaterialRename.Name = "cmdMaterialRename"
        Me.cmdMaterialRename.Size = New System.Drawing.Size(80, 24)
        Me.cmdMaterialRename.TabIndex = 6
        Me.cmdMaterialRename.Text = "Rename"
        Me.cmdMaterialRename.UseVisualStyleBackColor = True
        '
        'cmdMaterialRemove
        '
        Me.cmdMaterialRemove.Location = New System.Drawing.Point(96, 176)
        Me.cmdMaterialRemove.Name = "cmdMaterialRemove"
        Me.cmdMaterialRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdMaterialRemove.TabIndex = 5
        Me.cmdMaterialRemove.Text = "Remove"
        Me.cmdMaterialRemove.UseVisualStyleBackColor = True
        '
        'cmdMaterialAdd
        '
        Me.cmdMaterialAdd.Location = New System.Drawing.Point(8, 176)
        Me.cmdMaterialAdd.Name = "cmdMaterialAdd"
        Me.cmdMaterialAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdMaterialAdd.TabIndex = 4
        Me.cmdMaterialAdd.Text = "Add"
        Me.cmdMaterialAdd.UseVisualStyleBackColor = True
        '
        'tabMultiMeshes
        '
        Me.tabMultiMeshes.AutoScroll = True
        Me.tabMultiMeshes.Controls.Add(Me.cmdShipMeshesRetangent)
        Me.tabMultiMeshes.Controls.Add(Me.cmdShipMeshesRenormal)
        Me.tabMultiMeshes.Controls.Add(Me.lstShipMeshes)
        Me.tabMultiMeshes.Controls.Add(Me.cboShipMeshesParent)
        Me.tabMultiMeshes.Controls.Add(Me.Label13)
        Me.tabMultiMeshes.Controls.Add(Me.cmdShipMeshesRename)
        Me.tabMultiMeshes.Controls.Add(Me.fraShipMeshes)
        Me.tabMultiMeshes.Controls.Add(Me.cmdShipMeshesRemove)
        Me.tabMultiMeshes.Controls.Add(Me.cmdShipMeshesAdd)
        Me.tabMultiMeshes.Location = New System.Drawing.Point(4, 112)
        Me.tabMultiMeshes.Name = "tabMultiMeshes"
        Me.tabMultiMeshes.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMultiMeshes.Size = New System.Drawing.Size(288, 375)
        Me.tabMultiMeshes.TabIndex = 3
        Me.tabMultiMeshes.Text = "Ship Meshes"
        Me.tabMultiMeshes.UseVisualStyleBackColor = True
        '
        'cmdShipMeshesRetangent
        '
        Me.cmdShipMeshesRetangent.Location = New System.Drawing.Point(96, 208)
        Me.cmdShipMeshesRetangent.Name = "cmdShipMeshesRetangent"
        Me.cmdShipMeshesRetangent.Size = New System.Drawing.Size(80, 24)
        Me.cmdShipMeshesRetangent.TabIndex = 25
        Me.cmdShipMeshesRetangent.Text = "Re-tangent"
        Me.cmdShipMeshesRetangent.UseVisualStyleBackColor = True
        '
        'cmdShipMeshesRenormal
        '
        Me.cmdShipMeshesRenormal.Location = New System.Drawing.Point(8, 208)
        Me.cmdShipMeshesRenormal.Name = "cmdShipMeshesRenormal"
        Me.cmdShipMeshesRenormal.Size = New System.Drawing.Size(80, 24)
        Me.cmdShipMeshesRenormal.TabIndex = 24
        Me.cmdShipMeshesRenormal.Text = "Re-normal"
        Me.cmdShipMeshesRenormal.UseVisualStyleBackColor = True
        '
        'lstShipMeshes
        '
        Me.lstShipMeshes.FormattingEnabled = True
        Me.lstShipMeshes.Location = New System.Drawing.Point(8, 8)
        Me.lstShipMeshes.Name = "lstShipMeshes"
        Me.lstShipMeshes.Size = New System.Drawing.Size(256, 160)
        Me.lstShipMeshes.TabIndex = 12
        '
        'cboShipMeshesParent
        '
        Me.cboShipMeshesParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShipMeshesParent.FormattingEnabled = True
        Me.cboShipMeshesParent.Location = New System.Drawing.Point(96, 240)
        Me.cboShipMeshesParent.Name = "cboShipMeshesParent"
        Me.cboShipMeshesParent.Size = New System.Drawing.Size(168, 21)
        Me.cboShipMeshesParent.TabIndex = 11
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(8, 240)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(88, 23)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "Parent:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdShipMeshesRename
        '
        Me.cmdShipMeshesRename.Location = New System.Drawing.Point(184, 176)
        Me.cmdShipMeshesRename.Name = "cmdShipMeshesRename"
        Me.cmdShipMeshesRename.Size = New System.Drawing.Size(80, 24)
        Me.cmdShipMeshesRename.TabIndex = 9
        Me.cmdShipMeshesRename.Text = "Rename"
        Me.cmdShipMeshesRename.UseVisualStyleBackColor = True
        '
        'fraShipMeshes
        '
        Me.fraShipMeshes.Controls.Add(Me.cmdShipMeshesLODRetangent)
        Me.fraShipMeshes.Controls.Add(Me.cmdShipMeshesLODRenormal)
        Me.fraShipMeshes.Controls.Add(Me.cstShipMeshesLODs)
        Me.fraShipMeshes.Controls.Add(Me.cmdShipMeshesLODReMaterial)
        Me.fraShipMeshes.Controls.Add(Me.cmdShipMeshesLODExport)
        Me.fraShipMeshes.Controls.Add(Me.cmdShipMeshesLODImport)
        Me.fraShipMeshes.Controls.Add(Me.cmdShipMeshesLODTransform)
        Me.fraShipMeshes.Controls.Add(Me.cmdShipMeshesLODRemove)
        Me.fraShipMeshes.Controls.Add(Me.cmdShipMeshesLODAdd)
        Me.fraShipMeshes.Location = New System.Drawing.Point(8, 272)
        Me.fraShipMeshes.Name = "fraShipMeshes"
        Me.fraShipMeshes.Size = New System.Drawing.Size(256, 192)
        Me.fraShipMeshes.TabIndex = 8
        Me.fraShipMeshes.TabStop = False
        Me.fraShipMeshes.Text = "Level of Detail(s)"
        '
        'cmdShipMeshesLODRetangent
        '
        Me.cmdShipMeshesLODRetangent.Location = New System.Drawing.Point(88, 160)
        Me.cmdShipMeshesLODRetangent.Name = "cmdShipMeshesLODRetangent"
        Me.cmdShipMeshesLODRetangent.Size = New System.Drawing.Size(80, 24)
        Me.cmdShipMeshesLODRetangent.TabIndex = 23
        Me.cmdShipMeshesLODRetangent.Text = "Re-tangent"
        Me.cmdShipMeshesLODRetangent.UseVisualStyleBackColor = True
        '
        'cmdShipMeshesLODRenormal
        '
        Me.cmdShipMeshesLODRenormal.Location = New System.Drawing.Point(8, 160)
        Me.cmdShipMeshesLODRenormal.Name = "cmdShipMeshesLODRenormal"
        Me.cmdShipMeshesLODRenormal.Size = New System.Drawing.Size(72, 24)
        Me.cmdShipMeshesLODRenormal.TabIndex = 22
        Me.cmdShipMeshesLODRenormal.Text = "Re-normal"
        Me.cmdShipMeshesLODRenormal.UseVisualStyleBackColor = True
        '
        'cstShipMeshesLODs
        '
        Me.cstShipMeshesLODs.FormattingEnabled = True
        Me.cstShipMeshesLODs.Location = New System.Drawing.Point(8, 24)
        Me.cstShipMeshesLODs.Name = "cstShipMeshesLODs"
        Me.cstShipMeshesLODs.Size = New System.Drawing.Size(240, 64)
        Me.cstShipMeshesLODs.TabIndex = 21
        Me.cstShipMeshesLODs.ThreeDCheckBoxes = True
        '
        'cmdShipMeshesLODReMaterial
        '
        Me.cmdShipMeshesLODReMaterial.Location = New System.Drawing.Point(176, 128)
        Me.cmdShipMeshesLODReMaterial.Name = "cmdShipMeshesLODReMaterial"
        Me.cmdShipMeshesLODReMaterial.Size = New System.Drawing.Size(72, 24)
        Me.cmdShipMeshesLODReMaterial.TabIndex = 20
        Me.cmdShipMeshesLODReMaterial.Text = "Re-material"
        Me.cmdShipMeshesLODReMaterial.UseVisualStyleBackColor = True
        '
        'cmdShipMeshesLODExport
        '
        Me.cmdShipMeshesLODExport.Location = New System.Drawing.Point(88, 128)
        Me.cmdShipMeshesLODExport.Name = "cmdShipMeshesLODExport"
        Me.cmdShipMeshesLODExport.Size = New System.Drawing.Size(80, 24)
        Me.cmdShipMeshesLODExport.TabIndex = 19
        Me.cmdShipMeshesLODExport.Text = "Export"
        Me.cmdShipMeshesLODExport.UseVisualStyleBackColor = True
        '
        'cmdShipMeshesLODImport
        '
        Me.cmdShipMeshesLODImport.Location = New System.Drawing.Point(8, 128)
        Me.cmdShipMeshesLODImport.Name = "cmdShipMeshesLODImport"
        Me.cmdShipMeshesLODImport.Size = New System.Drawing.Size(72, 24)
        Me.cmdShipMeshesLODImport.TabIndex = 18
        Me.cmdShipMeshesLODImport.Text = "Import"
        Me.cmdShipMeshesLODImport.UseVisualStyleBackColor = True
        '
        'cmdShipMeshesLODTransform
        '
        Me.cmdShipMeshesLODTransform.Location = New System.Drawing.Point(176, 96)
        Me.cmdShipMeshesLODTransform.Name = "cmdShipMeshesLODTransform"
        Me.cmdShipMeshesLODTransform.Size = New System.Drawing.Size(72, 24)
        Me.cmdShipMeshesLODTransform.TabIndex = 17
        Me.cmdShipMeshesLODTransform.Text = "Transform"
        Me.cmdShipMeshesLODTransform.UseVisualStyleBackColor = True
        '
        'cmdShipMeshesLODRemove
        '
        Me.cmdShipMeshesLODRemove.Location = New System.Drawing.Point(88, 96)
        Me.cmdShipMeshesLODRemove.Name = "cmdShipMeshesLODRemove"
        Me.cmdShipMeshesLODRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdShipMeshesLODRemove.TabIndex = 16
        Me.cmdShipMeshesLODRemove.Text = "Remove"
        Me.cmdShipMeshesLODRemove.UseVisualStyleBackColor = True
        '
        'cmdShipMeshesLODAdd
        '
        Me.cmdShipMeshesLODAdd.Location = New System.Drawing.Point(8, 96)
        Me.cmdShipMeshesLODAdd.Name = "cmdShipMeshesLODAdd"
        Me.cmdShipMeshesLODAdd.Size = New System.Drawing.Size(72, 24)
        Me.cmdShipMeshesLODAdd.TabIndex = 15
        Me.cmdShipMeshesLODAdd.Text = "Add"
        Me.cmdShipMeshesLODAdd.UseVisualStyleBackColor = True
        '
        'cmdShipMeshesRemove
        '
        Me.cmdShipMeshesRemove.Location = New System.Drawing.Point(96, 176)
        Me.cmdShipMeshesRemove.Name = "cmdShipMeshesRemove"
        Me.cmdShipMeshesRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdShipMeshesRemove.TabIndex = 6
        Me.cmdShipMeshesRemove.Text = "Remove"
        Me.cmdShipMeshesRemove.UseVisualStyleBackColor = True
        '
        'cmdShipMeshesAdd
        '
        Me.cmdShipMeshesAdd.Location = New System.Drawing.Point(8, 176)
        Me.cmdShipMeshesAdd.Name = "cmdShipMeshesAdd"
        Me.cmdShipMeshesAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdShipMeshesAdd.TabIndex = 5
        Me.cmdShipMeshesAdd.Text = "Add"
        Me.cmdShipMeshesAdd.UseVisualStyleBackColor = True
        '
        'tabGoblins
        '
        Me.tabGoblins.AutoScroll = True
        Me.tabGoblins.Controls.Add(Me.cmdGoblinsRetangent)
        Me.tabGoblins.Controls.Add(Me.cmdGoblinsRenormal)
        Me.tabGoblins.Controls.Add(Me.cboGoblinsParent)
        Me.tabGoblins.Controls.Add(Me.Label14)
        Me.tabGoblins.Controls.Add(Me.cmdGoblinsRename)
        Me.tabGoblins.Controls.Add(Me.cstGoblins)
        Me.tabGoblins.Controls.Add(Me.cmdGoblinsRematerial)
        Me.tabGoblins.Controls.Add(Me.cmdGoblinsExport)
        Me.tabGoblins.Controls.Add(Me.cmdGoblinsImport)
        Me.tabGoblins.Controls.Add(Me.cmdGoblinsTransform)
        Me.tabGoblins.Controls.Add(Me.cmdGoblinsRemove)
        Me.tabGoblins.Controls.Add(Me.cmdGoblinsAdd)
        Me.tabGoblins.Location = New System.Drawing.Point(4, 112)
        Me.tabGoblins.Name = "tabGoblins"
        Me.tabGoblins.Padding = New System.Windows.Forms.Padding(3)
        Me.tabGoblins.Size = New System.Drawing.Size(288, 375)
        Me.tabGoblins.TabIndex = 4
        Me.tabGoblins.Text = "Goblins"
        Me.tabGoblins.UseVisualStyleBackColor = True
        '
        'cmdGoblinsRetangent
        '
        Me.cmdGoblinsRetangent.Location = New System.Drawing.Point(184, 240)
        Me.cmdGoblinsRetangent.Name = "cmdGoblinsRetangent"
        Me.cmdGoblinsRetangent.Size = New System.Drawing.Size(80, 24)
        Me.cmdGoblinsRetangent.TabIndex = 33
        Me.cmdGoblinsRetangent.Text = "Re-tangent"
        Me.cmdGoblinsRetangent.UseVisualStyleBackColor = True
        '
        'cmdGoblinsRenormal
        '
        Me.cmdGoblinsRenormal.Location = New System.Drawing.Point(96, 240)
        Me.cmdGoblinsRenormal.Name = "cmdGoblinsRenormal"
        Me.cmdGoblinsRenormal.Size = New System.Drawing.Size(80, 24)
        Me.cmdGoblinsRenormal.TabIndex = 32
        Me.cmdGoblinsRenormal.Text = "Re-normal"
        Me.cmdGoblinsRenormal.UseVisualStyleBackColor = True
        '
        'cboGoblinsParent
        '
        Me.cboGoblinsParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGoblinsParent.FormattingEnabled = True
        Me.cboGoblinsParent.Location = New System.Drawing.Point(96, 272)
        Me.cboGoblinsParent.Name = "cboGoblinsParent"
        Me.cboGoblinsParent.Size = New System.Drawing.Size(168, 21)
        Me.cboGoblinsParent.TabIndex = 31
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(8, 272)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(88, 23)
        Me.Label14.TabIndex = 30
        Me.Label14.Text = "Parent:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdGoblinsRename
        '
        Me.cmdGoblinsRename.Location = New System.Drawing.Point(184, 176)
        Me.cmdGoblinsRename.Name = "cmdGoblinsRename"
        Me.cmdGoblinsRename.Size = New System.Drawing.Size(80, 24)
        Me.cmdGoblinsRename.TabIndex = 29
        Me.cmdGoblinsRename.Text = "Rename"
        Me.cmdGoblinsRename.UseVisualStyleBackColor = True
        '
        'cstGoblins
        '
        Me.cstGoblins.FormattingEnabled = True
        Me.cstGoblins.Location = New System.Drawing.Point(8, 8)
        Me.cstGoblins.Name = "cstGoblins"
        Me.cstGoblins.Size = New System.Drawing.Size(256, 154)
        Me.cstGoblins.TabIndex = 28
        Me.cstGoblins.ThreeDCheckBoxes = True
        '
        'cmdGoblinsRematerial
        '
        Me.cmdGoblinsRematerial.Location = New System.Drawing.Point(8, 240)
        Me.cmdGoblinsRematerial.Name = "cmdGoblinsRematerial"
        Me.cmdGoblinsRematerial.Size = New System.Drawing.Size(80, 24)
        Me.cmdGoblinsRematerial.TabIndex = 27
        Me.cmdGoblinsRematerial.Text = "Re-material"
        Me.cmdGoblinsRematerial.UseVisualStyleBackColor = True
        '
        'cmdGoblinsExport
        '
        Me.cmdGoblinsExport.Location = New System.Drawing.Point(96, 208)
        Me.cmdGoblinsExport.Name = "cmdGoblinsExport"
        Me.cmdGoblinsExport.Size = New System.Drawing.Size(80, 24)
        Me.cmdGoblinsExport.TabIndex = 26
        Me.cmdGoblinsExport.Text = "Export"
        Me.cmdGoblinsExport.UseVisualStyleBackColor = True
        '
        'cmdGoblinsImport
        '
        Me.cmdGoblinsImport.Location = New System.Drawing.Point(8, 208)
        Me.cmdGoblinsImport.Name = "cmdGoblinsImport"
        Me.cmdGoblinsImport.Size = New System.Drawing.Size(80, 24)
        Me.cmdGoblinsImport.TabIndex = 25
        Me.cmdGoblinsImport.Text = "Import"
        Me.cmdGoblinsImport.UseVisualStyleBackColor = True
        '
        'cmdGoblinsTransform
        '
        Me.cmdGoblinsTransform.Location = New System.Drawing.Point(184, 208)
        Me.cmdGoblinsTransform.Name = "cmdGoblinsTransform"
        Me.cmdGoblinsTransform.Size = New System.Drawing.Size(80, 24)
        Me.cmdGoblinsTransform.TabIndex = 24
        Me.cmdGoblinsTransform.Text = "Transform"
        Me.cmdGoblinsTransform.UseVisualStyleBackColor = True
        '
        'cmdGoblinsRemove
        '
        Me.cmdGoblinsRemove.Location = New System.Drawing.Point(96, 176)
        Me.cmdGoblinsRemove.Name = "cmdGoblinsRemove"
        Me.cmdGoblinsRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdGoblinsRemove.TabIndex = 23
        Me.cmdGoblinsRemove.Text = "Remove"
        Me.cmdGoblinsRemove.UseVisualStyleBackColor = True
        '
        'cmdGoblinsAdd
        '
        Me.cmdGoblinsAdd.Location = New System.Drawing.Point(8, 176)
        Me.cmdGoblinsAdd.Name = "cmdGoblinsAdd"
        Me.cmdGoblinsAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdGoblinsAdd.TabIndex = 22
        Me.cmdGoblinsAdd.Text = "Add"
        Me.cmdGoblinsAdd.UseVisualStyleBackColor = True
        '
        'tabBGMS
        '
        Me.tabBGMS.AutoScroll = True
        Me.tabBGMS.Controls.Add(Me.cmdBackgroundMeshesGenerateTexture)
        Me.tabBGMS.Controls.Add(Me.cmdBackgroundMeshesRecolourizeAll)
        Me.tabBGMS.Controls.Add(Me.cmdBackgroundMeshesRecolourize)
        Me.tabBGMS.Controls.Add(Me.cstBackgroundMeshes)
        Me.tabBGMS.Controls.Add(Me.cmdBackgroundMeshesExport)
        Me.tabBGMS.Controls.Add(Me.cmdBackgroundMeshesImport)
        Me.tabBGMS.Controls.Add(Me.cmdBackgroundMeshesRemove)
        Me.tabBGMS.Controls.Add(Me.cmdBackgroundMeshesAdd)
        Me.tabBGMS.Location = New System.Drawing.Point(4, 112)
        Me.tabBGMS.Name = "tabBGMS"
        Me.tabBGMS.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBGMS.Size = New System.Drawing.Size(288, 375)
        Me.tabBGMS.TabIndex = 5
        Me.tabBGMS.Text = "Background Meshes"
        Me.tabBGMS.UseVisualStyleBackColor = True
        '
        'cmdBackgroundMeshesGenerateTexture
        '
        Me.cmdBackgroundMeshesGenerateTexture.Location = New System.Drawing.Point(136, 248)
        Me.cmdBackgroundMeshesGenerateTexture.Name = "cmdBackgroundMeshesGenerateTexture"
        Me.cmdBackgroundMeshesGenerateTexture.Size = New System.Drawing.Size(120, 24)
        Me.cmdBackgroundMeshesGenerateTexture.TabIndex = 36
        Me.cmdBackgroundMeshesGenerateTexture.Text = "Generate Texture"
        Me.cmdBackgroundMeshesGenerateTexture.UseVisualStyleBackColor = True
        '
        'cmdBackgroundMeshesRecolourizeAll
        '
        Me.cmdBackgroundMeshesRecolourizeAll.Location = New System.Drawing.Point(8, 248)
        Me.cmdBackgroundMeshesRecolourizeAll.Name = "cmdBackgroundMeshesRecolourizeAll"
        Me.cmdBackgroundMeshesRecolourizeAll.Size = New System.Drawing.Size(120, 24)
        Me.cmdBackgroundMeshesRecolourizeAll.TabIndex = 35
        Me.cmdBackgroundMeshesRecolourizeAll.Text = "Recolourize All"
        Me.cmdBackgroundMeshesRecolourizeAll.UseVisualStyleBackColor = True
        '
        'cmdBackgroundMeshesRecolourize
        '
        Me.cmdBackgroundMeshesRecolourize.Location = New System.Drawing.Point(184, 176)
        Me.cmdBackgroundMeshesRecolourize.Name = "cmdBackgroundMeshesRecolourize"
        Me.cmdBackgroundMeshesRecolourize.Size = New System.Drawing.Size(80, 24)
        Me.cmdBackgroundMeshesRecolourize.TabIndex = 34
        Me.cmdBackgroundMeshesRecolourize.Text = "Recolourize"
        Me.cmdBackgroundMeshesRecolourize.UseVisualStyleBackColor = True
        '
        'cstBackgroundMeshes
        '
        Me.cstBackgroundMeshes.FormattingEnabled = True
        Me.cstBackgroundMeshes.Location = New System.Drawing.Point(8, 8)
        Me.cstBackgroundMeshes.Name = "cstBackgroundMeshes"
        Me.cstBackgroundMeshes.Size = New System.Drawing.Size(256, 154)
        Me.cstBackgroundMeshes.TabIndex = 33
        Me.cstBackgroundMeshes.ThreeDCheckBoxes = True
        '
        'cmdBackgroundMeshesExport
        '
        Me.cmdBackgroundMeshesExport.Location = New System.Drawing.Point(96, 208)
        Me.cmdBackgroundMeshesExport.Name = "cmdBackgroundMeshesExport"
        Me.cmdBackgroundMeshesExport.Size = New System.Drawing.Size(80, 24)
        Me.cmdBackgroundMeshesExport.TabIndex = 32
        Me.cmdBackgroundMeshesExport.Text = "Export"
        Me.cmdBackgroundMeshesExport.UseVisualStyleBackColor = True
        '
        'cmdBackgroundMeshesImport
        '
        Me.cmdBackgroundMeshesImport.Location = New System.Drawing.Point(8, 208)
        Me.cmdBackgroundMeshesImport.Name = "cmdBackgroundMeshesImport"
        Me.cmdBackgroundMeshesImport.Size = New System.Drawing.Size(80, 24)
        Me.cmdBackgroundMeshesImport.TabIndex = 31
        Me.cmdBackgroundMeshesImport.Text = "Import"
        Me.cmdBackgroundMeshesImport.UseVisualStyleBackColor = True
        '
        'cmdBackgroundMeshesRemove
        '
        Me.cmdBackgroundMeshesRemove.Location = New System.Drawing.Point(96, 176)
        Me.cmdBackgroundMeshesRemove.Name = "cmdBackgroundMeshesRemove"
        Me.cmdBackgroundMeshesRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdBackgroundMeshesRemove.TabIndex = 30
        Me.cmdBackgroundMeshesRemove.Text = "Remove"
        Me.cmdBackgroundMeshesRemove.UseVisualStyleBackColor = True
        '
        'cmdBackgroundMeshesAdd
        '
        Me.cmdBackgroundMeshesAdd.Location = New System.Drawing.Point(8, 176)
        Me.cmdBackgroundMeshesAdd.Name = "cmdBackgroundMeshesAdd"
        Me.cmdBackgroundMeshesAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdBackgroundMeshesAdd.TabIndex = 29
        Me.cmdBackgroundMeshesAdd.Text = "Add"
        Me.cmdBackgroundMeshesAdd.UseVisualStyleBackColor = True
        '
        'tabUI
        '
        Me.tabUI.AutoScroll = True
        Me.tabUI.Controls.Add(Me.cmdUIMeshesRenormal)
        Me.tabUI.Controls.Add(Me.cmdUIMeshesRename)
        Me.tabUI.Controls.Add(Me.cstUIMeshes)
        Me.tabUI.Controls.Add(Me.cmdUIMeshesExport)
        Me.tabUI.Controls.Add(Me.cmdUIMeshesImport)
        Me.tabUI.Controls.Add(Me.cmdUIMeshesRemove)
        Me.tabUI.Controls.Add(Me.cmdUIMeshesAdd)
        Me.tabUI.Location = New System.Drawing.Point(4, 112)
        Me.tabUI.Name = "tabUI"
        Me.tabUI.Padding = New System.Windows.Forms.Padding(3)
        Me.tabUI.Size = New System.Drawing.Size(288, 375)
        Me.tabUI.TabIndex = 6
        Me.tabUI.Text = "UI Meshes"
        Me.tabUI.UseVisualStyleBackColor = True
        '
        'cmdUIMeshesRenormal
        '
        Me.cmdUIMeshesRenormal.Location = New System.Drawing.Point(184, 208)
        Me.cmdUIMeshesRenormal.Name = "cmdUIMeshesRenormal"
        Me.cmdUIMeshesRenormal.Size = New System.Drawing.Size(80, 24)
        Me.cmdUIMeshesRenormal.TabIndex = 36
        Me.cmdUIMeshesRenormal.Text = "Re-normal"
        Me.cmdUIMeshesRenormal.UseVisualStyleBackColor = True
        '
        'cmdUIMeshesRename
        '
        Me.cmdUIMeshesRename.Location = New System.Drawing.Point(184, 176)
        Me.cmdUIMeshesRename.Name = "cmdUIMeshesRename"
        Me.cmdUIMeshesRename.Size = New System.Drawing.Size(80, 24)
        Me.cmdUIMeshesRename.TabIndex = 35
        Me.cmdUIMeshesRename.Text = "Rename"
        Me.cmdUIMeshesRename.UseVisualStyleBackColor = True
        '
        'cstUIMeshes
        '
        Me.cstUIMeshes.FormattingEnabled = True
        Me.cstUIMeshes.Location = New System.Drawing.Point(8, 8)
        Me.cstUIMeshes.Name = "cstUIMeshes"
        Me.cstUIMeshes.Size = New System.Drawing.Size(256, 154)
        Me.cstUIMeshes.TabIndex = 34
        Me.cstUIMeshes.ThreeDCheckBoxes = True
        '
        'cmdUIMeshesExport
        '
        Me.cmdUIMeshesExport.Location = New System.Drawing.Point(96, 208)
        Me.cmdUIMeshesExport.Name = "cmdUIMeshesExport"
        Me.cmdUIMeshesExport.Size = New System.Drawing.Size(80, 24)
        Me.cmdUIMeshesExport.TabIndex = 33
        Me.cmdUIMeshesExport.Text = "Export"
        Me.cmdUIMeshesExport.UseVisualStyleBackColor = True
        '
        'cmdUIMeshesImport
        '
        Me.cmdUIMeshesImport.Location = New System.Drawing.Point(8, 208)
        Me.cmdUIMeshesImport.Name = "cmdUIMeshesImport"
        Me.cmdUIMeshesImport.Size = New System.Drawing.Size(80, 24)
        Me.cmdUIMeshesImport.TabIndex = 32
        Me.cmdUIMeshesImport.Text = "Import"
        Me.cmdUIMeshesImport.UseVisualStyleBackColor = True
        '
        'cmdUIMeshesRemove
        '
        Me.cmdUIMeshesRemove.Location = New System.Drawing.Point(96, 176)
        Me.cmdUIMeshesRemove.Name = "cmdUIMeshesRemove"
        Me.cmdUIMeshesRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdUIMeshesRemove.TabIndex = 31
        Me.cmdUIMeshesRemove.Text = "Remove"
        Me.cmdUIMeshesRemove.UseVisualStyleBackColor = True
        '
        'cmdUIMeshesAdd
        '
        Me.cmdUIMeshesAdd.Location = New System.Drawing.Point(8, 176)
        Me.cmdUIMeshesAdd.Name = "cmdUIMeshesAdd"
        Me.cmdUIMeshesAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdUIMeshesAdd.TabIndex = 30
        Me.cmdUIMeshesAdd.Text = "Add"
        Me.cmdUIMeshesAdd.UseVisualStyleBackColor = True
        '
        'tabJoints
        '
        Me.tabJoints.AutoScroll = True
        Me.tabJoints.Controls.Add(Me.chkJointsRender)
        Me.tabJoints.Controls.Add(Me.fraJoint)
        Me.tabJoints.Controls.Add(Me.cmdJointsAddTemplate)
        Me.tabJoints.Controls.Add(Me.cmdJointsRemove)
        Me.tabJoints.Controls.Add(Me.cmdJointsAdd)
        Me.tabJoints.Controls.Add(Me.tvwJoints)
        Me.tabJoints.Location = New System.Drawing.Point(4, 112)
        Me.tabJoints.Name = "tabJoints"
        Me.tabJoints.Padding = New System.Windows.Forms.Padding(3)
        Me.tabJoints.Size = New System.Drawing.Size(288, 375)
        Me.tabJoints.TabIndex = 7
        Me.tabJoints.Text = "Joints"
        Me.tabJoints.UseVisualStyleBackColor = True
        '
        'chkJointsRender
        '
        Me.chkJointsRender.Location = New System.Drawing.Point(8, 176)
        Me.chkJointsRender.Name = "chkJointsRender"
        Me.chkJointsRender.Size = New System.Drawing.Size(256, 24)
        Me.chkJointsRender.TabIndex = 36
        Me.chkJointsRender.Text = "Render joints"
        Me.chkJointsRender.UseVisualStyleBackColor = True
        '
        'fraJoint
        '
        Me.fraJoint.Controls.Add(Me.chkJointsDegreeOfFreedomZ)
        Me.fraJoint.Controls.Add(Me.chkJointsDegreeOfFreedomY)
        Me.fraJoint.Controls.Add(Me.chkJointsDegreeOfFreedomX)
        Me.fraJoint.Controls.Add(Me.txtJointsAxisZ)
        Me.fraJoint.Controls.Add(Me.Label24)
        Me.fraJoint.Controls.Add(Me.txtJointsAxisY)
        Me.fraJoint.Controls.Add(Me.Label25)
        Me.fraJoint.Controls.Add(Me.txtJointsAxisX)
        Me.fraJoint.Controls.Add(Me.Label26)
        Me.fraJoint.Controls.Add(Me.txtJointsScaleZ)
        Me.fraJoint.Controls.Add(Me.Label21)
        Me.fraJoint.Controls.Add(Me.txtJointsScaleY)
        Me.fraJoint.Controls.Add(Me.Label22)
        Me.fraJoint.Controls.Add(Me.txtJointsScaleX)
        Me.fraJoint.Controls.Add(Me.Label23)
        Me.fraJoint.Controls.Add(Me.txtJointsRotationZ)
        Me.fraJoint.Controls.Add(Me.Label18)
        Me.fraJoint.Controls.Add(Me.txtJointsRotationY)
        Me.fraJoint.Controls.Add(Me.Label19)
        Me.fraJoint.Controls.Add(Me.txtJointsRotationX)
        Me.fraJoint.Controls.Add(Me.Label20)
        Me.fraJoint.Controls.Add(Me.txtJointsPositionZ)
        Me.fraJoint.Controls.Add(Me.Label17)
        Me.fraJoint.Controls.Add(Me.txtJointsPositionY)
        Me.fraJoint.Controls.Add(Me.Label16)
        Me.fraJoint.Controls.Add(Me.txtJointsPositionX)
        Me.fraJoint.Controls.Add(Me.Label15)
        Me.fraJoint.Location = New System.Drawing.Point(8, 232)
        Me.fraJoint.Name = "fraJoint"
        Me.fraJoint.Size = New System.Drawing.Size(256, 416)
        Me.fraJoint.TabIndex = 35
        Me.fraJoint.TabStop = False
        Me.fraJoint.Text = "Joint Transform"
        '
        'chkJointsDegreeOfFreedomZ
        '
        Me.chkJointsDegreeOfFreedomZ.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkJointsDegreeOfFreedomZ.Location = New System.Drawing.Point(8, 384)
        Me.chkJointsDegreeOfFreedomZ.Name = "chkJointsDegreeOfFreedomZ"
        Me.chkJointsDegreeOfFreedomZ.Size = New System.Drawing.Size(216, 24)
        Me.chkJointsDegreeOfFreedomZ.TabIndex = 85
        Me.chkJointsDegreeOfFreedomZ.Text = "Degree of Freedom Z:"
        Me.chkJointsDegreeOfFreedomZ.UseVisualStyleBackColor = True
        '
        'chkJointsDegreeOfFreedomY
        '
        Me.chkJointsDegreeOfFreedomY.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkJointsDegreeOfFreedomY.Location = New System.Drawing.Point(8, 360)
        Me.chkJointsDegreeOfFreedomY.Name = "chkJointsDegreeOfFreedomY"
        Me.chkJointsDegreeOfFreedomY.Size = New System.Drawing.Size(216, 24)
        Me.chkJointsDegreeOfFreedomY.TabIndex = 84
        Me.chkJointsDegreeOfFreedomY.Text = "Degree of Freedom Y:"
        Me.chkJointsDegreeOfFreedomY.UseVisualStyleBackColor = True
        '
        'chkJointsDegreeOfFreedomX
        '
        Me.chkJointsDegreeOfFreedomX.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkJointsDegreeOfFreedomX.Location = New System.Drawing.Point(8, 336)
        Me.chkJointsDegreeOfFreedomX.Name = "chkJointsDegreeOfFreedomX"
        Me.chkJointsDegreeOfFreedomX.Size = New System.Drawing.Size(216, 24)
        Me.chkJointsDegreeOfFreedomX.TabIndex = 83
        Me.chkJointsDegreeOfFreedomX.Text = "Degree of Freedom X:"
        Me.chkJointsDegreeOfFreedomX.UseVisualStyleBackColor = True
        '
        'txtJointsAxisZ
        '
        Me.txtJointsAxisZ.Location = New System.Drawing.Point(80, 303)
        Me.txtJointsAxisZ.Name = "txtJointsAxisZ"
        Me.txtJointsAxisZ.Size = New System.Drawing.Size(144, 20)
        Me.txtJointsAxisZ.TabIndex = 82
        '
        'Label24
        '
        Me.Label24.Location = New System.Drawing.Point(8, 303)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(72, 20)
        Me.Label24.TabIndex = 81
        Me.Label24.Text = "Axis Z:"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtJointsAxisY
        '
        Me.txtJointsAxisY.Location = New System.Drawing.Point(80, 279)
        Me.txtJointsAxisY.Name = "txtJointsAxisY"
        Me.txtJointsAxisY.Size = New System.Drawing.Size(144, 20)
        Me.txtJointsAxisY.TabIndex = 80
        '
        'Label25
        '
        Me.Label25.Location = New System.Drawing.Point(8, 279)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(72, 20)
        Me.Label25.TabIndex = 79
        Me.Label25.Text = "Axis Y:"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtJointsAxisX
        '
        Me.txtJointsAxisX.Location = New System.Drawing.Point(80, 255)
        Me.txtJointsAxisX.Name = "txtJointsAxisX"
        Me.txtJointsAxisX.Size = New System.Drawing.Size(144, 20)
        Me.txtJointsAxisX.TabIndex = 78
        '
        'Label26
        '
        Me.Label26.Location = New System.Drawing.Point(8, 255)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(72, 20)
        Me.Label26.TabIndex = 77
        Me.Label26.Text = "Axis X:"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtJointsScaleZ
        '
        Me.txtJointsScaleZ.Location = New System.Drawing.Point(80, 224)
        Me.txtJointsScaleZ.Name = "txtJointsScaleZ"
        Me.txtJointsScaleZ.Size = New System.Drawing.Size(144, 20)
        Me.txtJointsScaleZ.TabIndex = 76
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(8, 224)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(72, 20)
        Me.Label21.TabIndex = 75
        Me.Label21.Text = "Scale Z:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtJointsScaleY
        '
        Me.txtJointsScaleY.Location = New System.Drawing.Point(80, 200)
        Me.txtJointsScaleY.Name = "txtJointsScaleY"
        Me.txtJointsScaleY.Size = New System.Drawing.Size(144, 20)
        Me.txtJointsScaleY.TabIndex = 74
        '
        'Label22
        '
        Me.Label22.Location = New System.Drawing.Point(8, 200)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(72, 20)
        Me.Label22.TabIndex = 73
        Me.Label22.Text = "Scale Y:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtJointsScaleX
        '
        Me.txtJointsScaleX.Location = New System.Drawing.Point(80, 176)
        Me.txtJointsScaleX.Name = "txtJointsScaleX"
        Me.txtJointsScaleX.Size = New System.Drawing.Size(144, 20)
        Me.txtJointsScaleX.TabIndex = 72
        '
        'Label23
        '
        Me.Label23.Location = New System.Drawing.Point(8, 176)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(72, 20)
        Me.Label23.TabIndex = 71
        Me.Label23.Text = "Scale X:"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtJointsRotationZ
        '
        Me.txtJointsRotationZ.Location = New System.Drawing.Point(80, 144)
        Me.txtJointsRotationZ.Name = "txtJointsRotationZ"
        Me.txtJointsRotationZ.Size = New System.Drawing.Size(144, 20)
        Me.txtJointsRotationZ.TabIndex = 70
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(8, 144)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(72, 20)
        Me.Label18.TabIndex = 69
        Me.Label18.Text = "Rotation Z:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtJointsRotationY
        '
        Me.txtJointsRotationY.Location = New System.Drawing.Point(80, 120)
        Me.txtJointsRotationY.Name = "txtJointsRotationY"
        Me.txtJointsRotationY.Size = New System.Drawing.Size(144, 20)
        Me.txtJointsRotationY.TabIndex = 68
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(8, 120)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(72, 20)
        Me.Label19.TabIndex = 67
        Me.Label19.Text = "Rotation Y:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtJointsRotationX
        '
        Me.txtJointsRotationX.Location = New System.Drawing.Point(80, 96)
        Me.txtJointsRotationX.Name = "txtJointsRotationX"
        Me.txtJointsRotationX.Size = New System.Drawing.Size(144, 20)
        Me.txtJointsRotationX.TabIndex = 66
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(8, 96)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(72, 20)
        Me.Label20.TabIndex = 65
        Me.Label20.Text = "Rotation X:"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtJointsPositionZ
        '
        Me.txtJointsPositionZ.Location = New System.Drawing.Point(80, 64)
        Me.txtJointsPositionZ.Name = "txtJointsPositionZ"
        Me.txtJointsPositionZ.Size = New System.Drawing.Size(144, 20)
        Me.txtJointsPositionZ.TabIndex = 64
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(8, 64)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(72, 20)
        Me.Label17.TabIndex = 63
        Me.Label17.Text = "Position Z:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtJointsPositionY
        '
        Me.txtJointsPositionY.Location = New System.Drawing.Point(80, 40)
        Me.txtJointsPositionY.Name = "txtJointsPositionY"
        Me.txtJointsPositionY.Size = New System.Drawing.Size(144, 20)
        Me.txtJointsPositionY.TabIndex = 62
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(8, 40)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(72, 20)
        Me.Label16.TabIndex = 61
        Me.Label16.Text = "Position Y:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtJointsPositionX
        '
        Me.txtJointsPositionX.Location = New System.Drawing.Point(80, 16)
        Me.txtJointsPositionX.Name = "txtJointsPositionX"
        Me.txtJointsPositionX.Size = New System.Drawing.Size(144, 20)
        Me.txtJointsPositionX.TabIndex = 60
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(8, 16)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(72, 20)
        Me.Label15.TabIndex = 59
        Me.Label15.Text = "Position X:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdJointsAddTemplate
        '
        Me.cmdJointsAddTemplate.Location = New System.Drawing.Point(88, 200)
        Me.cmdJointsAddTemplate.Name = "cmdJointsAddTemplate"
        Me.cmdJointsAddTemplate.Size = New System.Drawing.Size(96, 24)
        Me.cmdJointsAddTemplate.TabIndex = 34
        Me.cmdJointsAddTemplate.Text = "Add Template"
        Me.cmdJointsAddTemplate.UseVisualStyleBackColor = True
        '
        'cmdJointsRemove
        '
        Me.cmdJointsRemove.Location = New System.Drawing.Point(192, 200)
        Me.cmdJointsRemove.Name = "cmdJointsRemove"
        Me.cmdJointsRemove.Size = New System.Drawing.Size(72, 24)
        Me.cmdJointsRemove.TabIndex = 33
        Me.cmdJointsRemove.Text = "Remove"
        Me.cmdJointsRemove.UseVisualStyleBackColor = True
        '
        'cmdJointsAdd
        '
        Me.cmdJointsAdd.Location = New System.Drawing.Point(8, 200)
        Me.cmdJointsAdd.Name = "cmdJointsAdd"
        Me.cmdJointsAdd.Size = New System.Drawing.Size(72, 24)
        Me.cmdJointsAdd.TabIndex = 32
        Me.cmdJointsAdd.Text = "Add"
        Me.cmdJointsAdd.UseVisualStyleBackColor = True
        '
        'tvwJoints
        '
        Me.tvwJoints.CheckBoxes = True
        Me.tvwJoints.ContextMenuStrip = Me.tvwJoints_cms
        Me.tvwJoints.HideSelection = False
        Me.tvwJoints.LabelEdit = True
        Me.tvwJoints.Location = New System.Drawing.Point(8, 8)
        Me.tvwJoints.Name = "tvwJoints"
        Me.tvwJoints.ShowRootLines = False
        Me.tvwJoints.Size = New System.Drawing.Size(256, 160)
        Me.tvwJoints.TabIndex = 0
        '
        'tvwJoints_cms
        '
        Me.tvwJoints_cms.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tvwJoints_cmsRename, Me.tvwJoints_cmsSeperator1, Me.tvwJoints_cmsHide, Me.tvwJoints_cmsShow, Me.tvwJoints_cmsSeperator2, Me.tvwJoints_cmsCollapse, Me.tvwJoints_cmsExpand, Me.tvwJoints_cmsSeperator3, Me.tvwJoints_cmsMoveUp, Me.tvwJoints_cmsMoveDown})
        Me.tvwJoints_cms.Name = "tvwJoints_cms"
        Me.tvwJoints_cms.Size = New System.Drawing.Size(139, 176)
        '
        'tvwJoints_cmsRename
        '
        Me.tvwJoints_cmsRename.Name = "tvwJoints_cmsRename"
        Me.tvwJoints_cmsRename.Size = New System.Drawing.Size(138, 22)
        Me.tvwJoints_cmsRename.Text = "Rename"
        '
        'tvwJoints_cmsSeperator1
        '
        Me.tvwJoints_cmsSeperator1.Name = "tvwJoints_cmsSeperator1"
        Me.tvwJoints_cmsSeperator1.Size = New System.Drawing.Size(135, 6)
        '
        'tvwJoints_cmsHide
        '
        Me.tvwJoints_cmsHide.Name = "tvwJoints_cmsHide"
        Me.tvwJoints_cmsHide.Size = New System.Drawing.Size(138, 22)
        Me.tvwJoints_cmsHide.Text = "Hide"
        '
        'tvwJoints_cmsShow
        '
        Me.tvwJoints_cmsShow.Name = "tvwJoints_cmsShow"
        Me.tvwJoints_cmsShow.Size = New System.Drawing.Size(138, 22)
        Me.tvwJoints_cmsShow.Text = "Show"
        '
        'tvwJoints_cmsSeperator2
        '
        Me.tvwJoints_cmsSeperator2.Name = "tvwJoints_cmsSeperator2"
        Me.tvwJoints_cmsSeperator2.Size = New System.Drawing.Size(135, 6)
        '
        'tvwJoints_cmsCollapse
        '
        Me.tvwJoints_cmsCollapse.Name = "tvwJoints_cmsCollapse"
        Me.tvwJoints_cmsCollapse.Size = New System.Drawing.Size(138, 22)
        Me.tvwJoints_cmsCollapse.Text = "Collapse"
        '
        'tvwJoints_cmsExpand
        '
        Me.tvwJoints_cmsExpand.Name = "tvwJoints_cmsExpand"
        Me.tvwJoints_cmsExpand.Size = New System.Drawing.Size(138, 22)
        Me.tvwJoints_cmsExpand.Text = "Expand"
        '
        'tvwJoints_cmsSeperator3
        '
        Me.tvwJoints_cmsSeperator3.Name = "tvwJoints_cmsSeperator3"
        Me.tvwJoints_cmsSeperator3.Size = New System.Drawing.Size(135, 6)
        '
        'tvwJoints_cmsMoveUp
        '
        Me.tvwJoints_cmsMoveUp.Name = "tvwJoints_cmsMoveUp"
        Me.tvwJoints_cmsMoveUp.Size = New System.Drawing.Size(138, 22)
        Me.tvwJoints_cmsMoveUp.Text = "Move Up"
        '
        'tvwJoints_cmsMoveDown
        '
        Me.tvwJoints_cmsMoveDown.Name = "tvwJoints_cmsMoveDown"
        Me.tvwJoints_cmsMoveDown.Size = New System.Drawing.Size(138, 22)
        Me.tvwJoints_cmsMoveDown.Text = "Move Down"
        '
        'tabCM
        '
        Me.tabCM.AutoScroll = True
        Me.tabCM.Controls.Add(Me.cmdCMRecalc)
        Me.tabCM.Controls.Add(Me.fraCMBSPH)
        Me.tabCM.Controls.Add(Me.fraCMBBOX)
        Me.tabCM.Controls.Add(Me.cboCMName)
        Me.tabCM.Controls.Add(Me.Label27)
        Me.tabCM.Controls.Add(Me.cmdCMExport)
        Me.tabCM.Controls.Add(Me.cmdCMImport)
        Me.tabCM.Controls.Add(Me.cmdCMRemove)
        Me.tabCM.Controls.Add(Me.cmdCMAdd)
        Me.tabCM.Controls.Add(Me.cstCM)
        Me.tabCM.Location = New System.Drawing.Point(4, 112)
        Me.tabCM.Name = "tabCM"
        Me.tabCM.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCM.Size = New System.Drawing.Size(288, 375)
        Me.tabCM.TabIndex = 8
        Me.tabCM.Text = "Collision Meshes"
        Me.tabCM.UseVisualStyleBackColor = True
        '
        'cmdCMRecalc
        '
        Me.cmdCMRecalc.Location = New System.Drawing.Point(184, 208)
        Me.cmdCMRecalc.Name = "cmdCMRecalc"
        Me.cmdCMRecalc.Size = New System.Drawing.Size(80, 24)
        Me.cmdCMRecalc.TabIndex = 103
        Me.cmdCMRecalc.Text = "Re-calculate"
        Me.cmdCMRecalc.UseVisualStyleBackColor = True
        '
        'fraCMBSPH
        '
        Me.fraCMBSPH.Controls.Add(Me.txtCMCX)
        Me.fraCMBSPH.Controls.Add(Me.txtCMRadius)
        Me.fraCMBSPH.Controls.Add(Me.Label94)
        Me.fraCMBSPH.Controls.Add(Me.Label91)
        Me.fraCMBSPH.Controls.Add(Me.Label93)
        Me.fraCMBSPH.Controls.Add(Me.txtCMCZ)
        Me.fraCMBSPH.Controls.Add(Me.txtCMCY)
        Me.fraCMBSPH.Controls.Add(Me.Label92)
        Me.fraCMBSPH.Location = New System.Drawing.Point(8, 462)
        Me.fraCMBSPH.Name = "fraCMBSPH"
        Me.fraCMBSPH.Size = New System.Drawing.Size(256, 138)
        Me.fraCMBSPH.TabIndex = 102
        Me.fraCMBSPH.TabStop = False
        Me.fraCMBSPH.Text = "Collision Mesh Boundary Sphere"
        '
        'txtCMCX
        '
        Me.txtCMCX.Location = New System.Drawing.Point(80, 24)
        Me.txtCMCX.Name = "txtCMCX"
        Me.txtCMCX.Size = New System.Drawing.Size(144, 20)
        Me.txtCMCX.TabIndex = 88
        '
        'txtCMRadius
        '
        Me.txtCMRadius.Location = New System.Drawing.Point(80, 104)
        Me.txtCMRadius.Name = "txtCMRadius"
        Me.txtCMRadius.Size = New System.Drawing.Size(144, 20)
        Me.txtCMRadius.TabIndex = 94
        '
        'Label94
        '
        Me.Label94.Location = New System.Drawing.Point(8, 22)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(72, 20)
        Me.Label94.TabIndex = 87
        Me.Label94.Text = "Center X:"
        Me.Label94.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label91
        '
        Me.Label91.Location = New System.Drawing.Point(8, 102)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(72, 20)
        Me.Label91.TabIndex = 93
        Me.Label91.Text = "Radius:"
        Me.Label91.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label93
        '
        Me.Label93.Location = New System.Drawing.Point(8, 46)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(72, 20)
        Me.Label93.TabIndex = 89
        Me.Label93.Text = "Center Y:"
        Me.Label93.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCMCZ
        '
        Me.txtCMCZ.Location = New System.Drawing.Point(80, 72)
        Me.txtCMCZ.Name = "txtCMCZ"
        Me.txtCMCZ.Size = New System.Drawing.Size(144, 20)
        Me.txtCMCZ.TabIndex = 92
        '
        'txtCMCY
        '
        Me.txtCMCY.Location = New System.Drawing.Point(80, 48)
        Me.txtCMCY.Name = "txtCMCY"
        Me.txtCMCY.Size = New System.Drawing.Size(144, 20)
        Me.txtCMCY.TabIndex = 90
        '
        'Label92
        '
        Me.Label92.Location = New System.Drawing.Point(8, 70)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(72, 20)
        Me.Label92.TabIndex = 91
        Me.Label92.Text = "Center Z:"
        Me.Label92.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fraCMBBOX
        '
        Me.fraCMBBOX.Controls.Add(Me.Label97)
        Me.fraCMBBOX.Controls.Add(Me.txtCMMaxZ)
        Me.fraCMBBOX.Controls.Add(Me.txtCMMinX)
        Me.fraCMBBOX.Controls.Add(Me.Label98)
        Me.fraCMBBOX.Controls.Add(Me.Label96)
        Me.fraCMBBOX.Controls.Add(Me.txtCMMaxY)
        Me.fraCMBBOX.Controls.Add(Me.txtCMMinY)
        Me.fraCMBBOX.Controls.Add(Me.Label99)
        Me.fraCMBBOX.Controls.Add(Me.Label95)
        Me.fraCMBBOX.Controls.Add(Me.txtCMMaxX)
        Me.fraCMBBOX.Controls.Add(Me.txtCMMinZ)
        Me.fraCMBBOX.Controls.Add(Me.Label100)
        Me.fraCMBBOX.Location = New System.Drawing.Point(8, 270)
        Me.fraCMBBOX.Name = "fraCMBBOX"
        Me.fraCMBBOX.Size = New System.Drawing.Size(256, 184)
        Me.fraCMBBOX.TabIndex = 101
        Me.fraCMBBOX.TabStop = False
        Me.fraCMBBOX.Text = "Collision Mesh Bounding Box"
        '
        'Label97
        '
        Me.Label97.Location = New System.Drawing.Point(8, 24)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(72, 20)
        Me.Label97.TabIndex = 73
        Me.Label97.Text = "Minimum X:"
        Me.Label97.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCMMaxZ
        '
        Me.txtCMMaxZ.Location = New System.Drawing.Point(80, 152)
        Me.txtCMMaxZ.Name = "txtCMMaxZ"
        Me.txtCMMaxZ.Size = New System.Drawing.Size(144, 20)
        Me.txtCMMaxZ.TabIndex = 100
        '
        'txtCMMinX
        '
        Me.txtCMMinX.Location = New System.Drawing.Point(80, 26)
        Me.txtCMMinX.Name = "txtCMMinX"
        Me.txtCMMinX.Size = New System.Drawing.Size(144, 20)
        Me.txtCMMinX.TabIndex = 74
        '
        'Label98
        '
        Me.Label98.Location = New System.Drawing.Point(8, 150)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(72, 20)
        Me.Label98.TabIndex = 99
        Me.Label98.Text = "Maximum Z:"
        Me.Label98.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label96
        '
        Me.Label96.Location = New System.Drawing.Point(8, 48)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(72, 20)
        Me.Label96.TabIndex = 75
        Me.Label96.Text = "Minimum Y:"
        Me.Label96.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCMMaxY
        '
        Me.txtCMMaxY.Location = New System.Drawing.Point(80, 128)
        Me.txtCMMaxY.Name = "txtCMMaxY"
        Me.txtCMMaxY.Size = New System.Drawing.Size(144, 20)
        Me.txtCMMaxY.TabIndex = 98
        '
        'txtCMMinY
        '
        Me.txtCMMinY.Location = New System.Drawing.Point(80, 50)
        Me.txtCMMinY.Name = "txtCMMinY"
        Me.txtCMMinY.Size = New System.Drawing.Size(144, 20)
        Me.txtCMMinY.TabIndex = 76
        '
        'Label99
        '
        Me.Label99.Location = New System.Drawing.Point(8, 126)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(72, 20)
        Me.Label99.TabIndex = 97
        Me.Label99.Text = "Maximum Y:"
        Me.Label99.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label95
        '
        Me.Label95.Location = New System.Drawing.Point(8, 72)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(72, 20)
        Me.Label95.TabIndex = 77
        Me.Label95.Text = "Minimum Z:"
        Me.Label95.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCMMaxX
        '
        Me.txtCMMaxX.Location = New System.Drawing.Point(80, 104)
        Me.txtCMMaxX.Name = "txtCMMaxX"
        Me.txtCMMaxX.Size = New System.Drawing.Size(144, 20)
        Me.txtCMMaxX.TabIndex = 96
        '
        'txtCMMinZ
        '
        Me.txtCMMinZ.Location = New System.Drawing.Point(80, 74)
        Me.txtCMMinZ.Name = "txtCMMinZ"
        Me.txtCMMinZ.Size = New System.Drawing.Size(144, 20)
        Me.txtCMMinZ.TabIndex = 78
        '
        'Label100
        '
        Me.Label100.Location = New System.Drawing.Point(8, 102)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(72, 20)
        Me.Label100.TabIndex = 95
        Me.Label100.Text = "Maximum X:"
        Me.Label100.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboCMName
        '
        Me.cboCMName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCMName.FormattingEnabled = True
        Me.cboCMName.Location = New System.Drawing.Point(96, 240)
        Me.cboCMName.Name = "cboCMName"
        Me.cboCMName.Size = New System.Drawing.Size(168, 21)
        Me.cboCMName.TabIndex = 37
        '
        'Label27
        '
        Me.Label27.Location = New System.Drawing.Point(8, 240)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(88, 23)
        Me.Label27.TabIndex = 36
        Me.Label27.Text = "Parent:"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdCMExport
        '
        Me.cmdCMExport.Location = New System.Drawing.Point(96, 208)
        Me.cmdCMExport.Name = "cmdCMExport"
        Me.cmdCMExport.Size = New System.Drawing.Size(80, 24)
        Me.cmdCMExport.TabIndex = 35
        Me.cmdCMExport.Text = "Export"
        Me.cmdCMExport.UseVisualStyleBackColor = True
        '
        'cmdCMImport
        '
        Me.cmdCMImport.Location = New System.Drawing.Point(8, 208)
        Me.cmdCMImport.Name = "cmdCMImport"
        Me.cmdCMImport.Size = New System.Drawing.Size(80, 24)
        Me.cmdCMImport.TabIndex = 34
        Me.cmdCMImport.Text = "Import"
        Me.cmdCMImport.UseVisualStyleBackColor = True
        '
        'cmdCMRemove
        '
        Me.cmdCMRemove.Location = New System.Drawing.Point(96, 176)
        Me.cmdCMRemove.Name = "cmdCMRemove"
        Me.cmdCMRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdCMRemove.TabIndex = 33
        Me.cmdCMRemove.Text = "Remove"
        Me.cmdCMRemove.UseVisualStyleBackColor = True
        '
        'cmdCMAdd
        '
        Me.cmdCMAdd.Location = New System.Drawing.Point(8, 176)
        Me.cmdCMAdd.Name = "cmdCMAdd"
        Me.cmdCMAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdCMAdd.TabIndex = 32
        Me.cmdCMAdd.Text = "Add"
        Me.cmdCMAdd.UseVisualStyleBackColor = True
        '
        'cstCM
        '
        Me.cstCM.FormattingEnabled = True
        Me.cstCM.Location = New System.Drawing.Point(8, 8)
        Me.cstCM.Name = "cstCM"
        Me.cstCM.Size = New System.Drawing.Size(256, 154)
        Me.cstCM.TabIndex = 0
        '
        'tabEngineShapes
        '
        Me.tabEngineShapes.AutoScroll = True
        Me.tabEngineShapes.Controls.Add(Me.cboEngineShapesParent)
        Me.tabEngineShapes.Controls.Add(Me.Label28)
        Me.tabEngineShapes.Controls.Add(Me.cmdEngineShapesRename)
        Me.tabEngineShapes.Controls.Add(Me.cstEngineShapes)
        Me.tabEngineShapes.Controls.Add(Me.cmdEngineShapesExport)
        Me.tabEngineShapes.Controls.Add(Me.cmdEngineShapesImport)
        Me.tabEngineShapes.Controls.Add(Me.cmdEngineShapesRemove)
        Me.tabEngineShapes.Controls.Add(Me.cmdEngineShapesAdd)
        Me.tabEngineShapes.Location = New System.Drawing.Point(4, 112)
        Me.tabEngineShapes.Name = "tabEngineShapes"
        Me.tabEngineShapes.Padding = New System.Windows.Forms.Padding(3)
        Me.tabEngineShapes.Size = New System.Drawing.Size(288, 375)
        Me.tabEngineShapes.TabIndex = 9
        Me.tabEngineShapes.Text = "Engine Shapes"
        Me.tabEngineShapes.UseVisualStyleBackColor = True
        '
        'cboEngineShapesParent
        '
        Me.cboEngineShapesParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEngineShapesParent.FormattingEnabled = True
        Me.cboEngineShapesParent.Location = New System.Drawing.Point(96, 240)
        Me.cboEngineShapesParent.Name = "cboEngineShapesParent"
        Me.cboEngineShapesParent.Size = New System.Drawing.Size(168, 21)
        Me.cboEngineShapesParent.TabIndex = 39
        '
        'Label28
        '
        Me.Label28.Location = New System.Drawing.Point(8, 240)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(88, 23)
        Me.Label28.TabIndex = 38
        Me.Label28.Text = "Parent:"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdEngineShapesRename
        '
        Me.cmdEngineShapesRename.Location = New System.Drawing.Point(184, 176)
        Me.cmdEngineShapesRename.Name = "cmdEngineShapesRename"
        Me.cmdEngineShapesRename.Size = New System.Drawing.Size(80, 24)
        Me.cmdEngineShapesRename.TabIndex = 37
        Me.cmdEngineShapesRename.Text = "Rename"
        Me.cmdEngineShapesRename.UseVisualStyleBackColor = True
        '
        'cstEngineShapes
        '
        Me.cstEngineShapes.FormattingEnabled = True
        Me.cstEngineShapes.Location = New System.Drawing.Point(8, 8)
        Me.cstEngineShapes.Name = "cstEngineShapes"
        Me.cstEngineShapes.Size = New System.Drawing.Size(256, 154)
        Me.cstEngineShapes.TabIndex = 36
        Me.cstEngineShapes.ThreeDCheckBoxes = True
        '
        'cmdEngineShapesExport
        '
        Me.cmdEngineShapesExport.Location = New System.Drawing.Point(96, 208)
        Me.cmdEngineShapesExport.Name = "cmdEngineShapesExport"
        Me.cmdEngineShapesExport.Size = New System.Drawing.Size(80, 24)
        Me.cmdEngineShapesExport.TabIndex = 35
        Me.cmdEngineShapesExport.Text = "Export"
        Me.cmdEngineShapesExport.UseVisualStyleBackColor = True
        '
        'cmdEngineShapesImport
        '
        Me.cmdEngineShapesImport.Location = New System.Drawing.Point(8, 208)
        Me.cmdEngineShapesImport.Name = "cmdEngineShapesImport"
        Me.cmdEngineShapesImport.Size = New System.Drawing.Size(80, 24)
        Me.cmdEngineShapesImport.TabIndex = 34
        Me.cmdEngineShapesImport.Text = "Import"
        Me.cmdEngineShapesImport.UseVisualStyleBackColor = True
        '
        'cmdEngineShapesRemove
        '
        Me.cmdEngineShapesRemove.Location = New System.Drawing.Point(96, 176)
        Me.cmdEngineShapesRemove.Name = "cmdEngineShapesRemove"
        Me.cmdEngineShapesRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdEngineShapesRemove.TabIndex = 33
        Me.cmdEngineShapesRemove.Text = "Remove"
        Me.cmdEngineShapesRemove.UseVisualStyleBackColor = True
        '
        'cmdEngineShapesAdd
        '
        Me.cmdEngineShapesAdd.Location = New System.Drawing.Point(8, 176)
        Me.cmdEngineShapesAdd.Name = "cmdEngineShapesAdd"
        Me.cmdEngineShapesAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdEngineShapesAdd.TabIndex = 32
        Me.cmdEngineShapesAdd.Text = "Add"
        Me.cmdEngineShapesAdd.UseVisualStyleBackColor = True
        '
        'tabEngineGlows
        '
        Me.tabEngineGlows.AutoScroll = True
        Me.tabEngineGlows.Controls.Add(Me.fraEngineGlowsMisc)
        Me.tabEngineGlows.Controls.Add(Me.txtEngineGlowsLOD)
        Me.tabEngineGlows.Controls.Add(Me.Label40)
        Me.tabEngineGlows.Controls.Add(Me.cboEngineGlowsParent)
        Me.tabEngineGlows.Controls.Add(Me.Label29)
        Me.tabEngineGlows.Controls.Add(Me.cmdEngineGlowsRename)
        Me.tabEngineGlows.Controls.Add(Me.cstEngineGlows)
        Me.tabEngineGlows.Controls.Add(Me.cmdEngineGlowsExport)
        Me.tabEngineGlows.Controls.Add(Me.cmdEngineGlowsImport)
        Me.tabEngineGlows.Controls.Add(Me.cmdEngineGlowsRemove)
        Me.tabEngineGlows.Controls.Add(Me.cmdEngineGlowsAdd)
        Me.tabEngineGlows.Location = New System.Drawing.Point(4, 112)
        Me.tabEngineGlows.Name = "tabEngineGlows"
        Me.tabEngineGlows.Padding = New System.Windows.Forms.Padding(3)
        Me.tabEngineGlows.Size = New System.Drawing.Size(288, 375)
        Me.tabEngineGlows.TabIndex = 10
        Me.tabEngineGlows.Text = "Engine Glows"
        Me.tabEngineGlows.UseVisualStyleBackColor = True
        '
        'fraEngineGlowsMisc
        '
        Me.fraEngineGlowsMisc.Controls.Add(Me.sldEngineGlowsThrusterPowerFactor)
        Me.fraEngineGlowsMisc.Controls.Add(Me.Label41)
        Me.fraEngineGlowsMisc.Location = New System.Drawing.Point(8, 304)
        Me.fraEngineGlowsMisc.Name = "fraEngineGlowsMisc"
        Me.fraEngineGlowsMisc.Size = New System.Drawing.Size(256, 120)
        Me.fraEngineGlowsMisc.TabIndex = 51
        Me.fraEngineGlowsMisc.TabStop = False
        Me.fraEngineGlowsMisc.Text = "Miscellaneous"
        '
        'sldEngineGlowsThrusterPowerFactor
        '
        Me.sldEngineGlowsThrusterPowerFactor.LargeChange = 10
        Me.sldEngineGlowsThrusterPowerFactor.Location = New System.Drawing.Point(8, 64)
        Me.sldEngineGlowsThrusterPowerFactor.Maximum = 100
        Me.sldEngineGlowsThrusterPowerFactor.Name = "sldEngineGlowsThrusterPowerFactor"
        Me.sldEngineGlowsThrusterPowerFactor.Size = New System.Drawing.Size(240, 45)
        Me.sldEngineGlowsThrusterPowerFactor.TabIndex = 51
        Me.sldEngineGlowsThrusterPowerFactor.TickFrequency = 10
        Me.sldEngineGlowsThrusterPowerFactor.TickStyle = System.Windows.Forms.TickStyle.Both
        '
        'Label41
        '
        Me.Label41.Location = New System.Drawing.Point(8, 24)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(240, 40)
        Me.Label41.TabIndex = 50
        Me.Label41.Text = "Thruster Power Factor: (for materials using ""glow.st"" shader)"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtEngineGlowsLOD
        '
        Me.txtEngineGlowsLOD.Location = New System.Drawing.Point(96, 272)
        Me.txtEngineGlowsLOD.Name = "txtEngineGlowsLOD"
        Me.txtEngineGlowsLOD.Size = New System.Drawing.Size(144, 20)
        Me.txtEngineGlowsLOD.TabIndex = 49
        '
        'Label40
        '
        Me.Label40.Location = New System.Drawing.Point(8, 272)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(88, 20)
        Me.Label40.TabIndex = 48
        Me.Label40.Text = "Level of Detail:"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboEngineGlowsParent
        '
        Me.cboEngineGlowsParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEngineGlowsParent.FormattingEnabled = True
        Me.cboEngineGlowsParent.Location = New System.Drawing.Point(96, 240)
        Me.cboEngineGlowsParent.Name = "cboEngineGlowsParent"
        Me.cboEngineGlowsParent.Size = New System.Drawing.Size(168, 21)
        Me.cboEngineGlowsParent.TabIndex = 47
        '
        'Label29
        '
        Me.Label29.Location = New System.Drawing.Point(8, 240)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(88, 23)
        Me.Label29.TabIndex = 46
        Me.Label29.Text = "Parent:"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdEngineGlowsRename
        '
        Me.cmdEngineGlowsRename.Location = New System.Drawing.Point(184, 176)
        Me.cmdEngineGlowsRename.Name = "cmdEngineGlowsRename"
        Me.cmdEngineGlowsRename.Size = New System.Drawing.Size(80, 24)
        Me.cmdEngineGlowsRename.TabIndex = 45
        Me.cmdEngineGlowsRename.Text = "Rename"
        Me.cmdEngineGlowsRename.UseVisualStyleBackColor = True
        '
        'cstEngineGlows
        '
        Me.cstEngineGlows.FormattingEnabled = True
        Me.cstEngineGlows.Location = New System.Drawing.Point(8, 8)
        Me.cstEngineGlows.Name = "cstEngineGlows"
        Me.cstEngineGlows.Size = New System.Drawing.Size(256, 154)
        Me.cstEngineGlows.TabIndex = 44
        Me.cstEngineGlows.ThreeDCheckBoxes = True
        '
        'cmdEngineGlowsExport
        '
        Me.cmdEngineGlowsExport.Location = New System.Drawing.Point(96, 208)
        Me.cmdEngineGlowsExport.Name = "cmdEngineGlowsExport"
        Me.cmdEngineGlowsExport.Size = New System.Drawing.Size(80, 24)
        Me.cmdEngineGlowsExport.TabIndex = 43
        Me.cmdEngineGlowsExport.Text = "Export"
        Me.cmdEngineGlowsExport.UseVisualStyleBackColor = True
        '
        'cmdEngineGlowsImport
        '
        Me.cmdEngineGlowsImport.Location = New System.Drawing.Point(8, 208)
        Me.cmdEngineGlowsImport.Name = "cmdEngineGlowsImport"
        Me.cmdEngineGlowsImport.Size = New System.Drawing.Size(80, 24)
        Me.cmdEngineGlowsImport.TabIndex = 42
        Me.cmdEngineGlowsImport.Text = "Import"
        Me.cmdEngineGlowsImport.UseVisualStyleBackColor = True
        '
        'cmdEngineGlowsRemove
        '
        Me.cmdEngineGlowsRemove.Location = New System.Drawing.Point(96, 176)
        Me.cmdEngineGlowsRemove.Name = "cmdEngineGlowsRemove"
        Me.cmdEngineGlowsRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdEngineGlowsRemove.TabIndex = 41
        Me.cmdEngineGlowsRemove.Text = "Remove"
        Me.cmdEngineGlowsRemove.UseVisualStyleBackColor = True
        '
        'cmdEngineGlowsAdd
        '
        Me.cmdEngineGlowsAdd.Location = New System.Drawing.Point(8, 176)
        Me.cmdEngineGlowsAdd.Name = "cmdEngineGlowsAdd"
        Me.cmdEngineGlowsAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdEngineGlowsAdd.TabIndex = 40
        Me.cmdEngineGlowsAdd.Text = "Add"
        Me.cmdEngineGlowsAdd.UseVisualStyleBackColor = True
        '
        'tabEngineBurns
        '
        Me.tabEngineBurns.AutoScroll = True
        Me.tabEngineBurns.Controls.Add(Me.txtEngineBurn5Z)
        Me.tabEngineBurns.Controls.Add(Me.txtEngineBurn5Y)
        Me.tabEngineBurns.Controls.Add(Me.txtEngineBurn5X)
        Me.tabEngineBurns.Controls.Add(Me.txtEngineBurn4Z)
        Me.tabEngineBurns.Controls.Add(Me.txtEngineBurn4Y)
        Me.tabEngineBurns.Controls.Add(Me.txtEngineBurn4X)
        Me.tabEngineBurns.Controls.Add(Me.txtEngineBurn3Z)
        Me.tabEngineBurns.Controls.Add(Me.txtEngineBurn3Y)
        Me.tabEngineBurns.Controls.Add(Me.txtEngineBurn3X)
        Me.tabEngineBurns.Controls.Add(Me.txtEngineBurn2Z)
        Me.tabEngineBurns.Controls.Add(Me.txtEngineBurn2Y)
        Me.tabEngineBurns.Controls.Add(Me.txtEngineBurn2X)
        Me.tabEngineBurns.Controls.Add(Me.txtEngineBurn1Z)
        Me.tabEngineBurns.Controls.Add(Me.txtEngineBurn1Y)
        Me.tabEngineBurns.Controls.Add(Me.txtEngineBurn1X)
        Me.tabEngineBurns.Controls.Add(Me.Label39)
        Me.tabEngineBurns.Controls.Add(Me.Label38)
        Me.tabEngineBurns.Controls.Add(Me.Label37)
        Me.tabEngineBurns.Controls.Add(Me.Label36)
        Me.tabEngineBurns.Controls.Add(Me.Label35)
        Me.tabEngineBurns.Controls.Add(Me.Label33)
        Me.tabEngineBurns.Controls.Add(Me.Label34)
        Me.tabEngineBurns.Controls.Add(Me.Label32)
        Me.tabEngineBurns.Controls.Add(Me.Label31)
        Me.tabEngineBurns.Controls.Add(Me.cboEngineBurnsParent)
        Me.tabEngineBurns.Controls.Add(Me.Label30)
        Me.tabEngineBurns.Controls.Add(Me.cmdEngineBurnsRename)
        Me.tabEngineBurns.Controls.Add(Me.cstEngineBurns)
        Me.tabEngineBurns.Controls.Add(Me.cmdEngineBurnsRemove)
        Me.tabEngineBurns.Controls.Add(Me.cmdEngineBurnsAdd)
        Me.tabEngineBurns.Location = New System.Drawing.Point(4, 112)
        Me.tabEngineBurns.Name = "tabEngineBurns"
        Me.tabEngineBurns.Padding = New System.Windows.Forms.Padding(3)
        Me.tabEngineBurns.Size = New System.Drawing.Size(288, 375)
        Me.tabEngineBurns.TabIndex = 11
        Me.tabEngineBurns.Text = "Engine Burns"
        Me.tabEngineBurns.UseVisualStyleBackColor = True
        '
        'txtEngineBurn5Z
        '
        Me.txtEngineBurn5Z.Location = New System.Drawing.Point(192, 384)
        Me.txtEngineBurn5Z.Name = "txtEngineBurn5Z"
        Me.txtEngineBurn5Z.Size = New System.Drawing.Size(56, 20)
        Me.txtEngineBurn5Z.TabIndex = 71
        '
        'txtEngineBurn5Y
        '
        Me.txtEngineBurn5Y.Location = New System.Drawing.Point(112, 384)
        Me.txtEngineBurn5Y.Name = "txtEngineBurn5Y"
        Me.txtEngineBurn5Y.Size = New System.Drawing.Size(56, 20)
        Me.txtEngineBurn5Y.TabIndex = 70
        '
        'txtEngineBurn5X
        '
        Me.txtEngineBurn5X.Location = New System.Drawing.Point(32, 384)
        Me.txtEngineBurn5X.Name = "txtEngineBurn5X"
        Me.txtEngineBurn5X.Size = New System.Drawing.Size(56, 20)
        Me.txtEngineBurn5X.TabIndex = 69
        '
        'txtEngineBurn4Z
        '
        Me.txtEngineBurn4Z.Location = New System.Drawing.Point(192, 360)
        Me.txtEngineBurn4Z.Name = "txtEngineBurn4Z"
        Me.txtEngineBurn4Z.Size = New System.Drawing.Size(56, 20)
        Me.txtEngineBurn4Z.TabIndex = 68
        '
        'txtEngineBurn4Y
        '
        Me.txtEngineBurn4Y.Location = New System.Drawing.Point(112, 360)
        Me.txtEngineBurn4Y.Name = "txtEngineBurn4Y"
        Me.txtEngineBurn4Y.Size = New System.Drawing.Size(56, 20)
        Me.txtEngineBurn4Y.TabIndex = 67
        '
        'txtEngineBurn4X
        '
        Me.txtEngineBurn4X.Location = New System.Drawing.Point(32, 360)
        Me.txtEngineBurn4X.Name = "txtEngineBurn4X"
        Me.txtEngineBurn4X.Size = New System.Drawing.Size(56, 20)
        Me.txtEngineBurn4X.TabIndex = 66
        '
        'txtEngineBurn3Z
        '
        Me.txtEngineBurn3Z.Location = New System.Drawing.Point(192, 336)
        Me.txtEngineBurn3Z.Name = "txtEngineBurn3Z"
        Me.txtEngineBurn3Z.Size = New System.Drawing.Size(56, 20)
        Me.txtEngineBurn3Z.TabIndex = 65
        '
        'txtEngineBurn3Y
        '
        Me.txtEngineBurn3Y.Location = New System.Drawing.Point(112, 336)
        Me.txtEngineBurn3Y.Name = "txtEngineBurn3Y"
        Me.txtEngineBurn3Y.Size = New System.Drawing.Size(56, 20)
        Me.txtEngineBurn3Y.TabIndex = 64
        '
        'txtEngineBurn3X
        '
        Me.txtEngineBurn3X.Location = New System.Drawing.Point(32, 336)
        Me.txtEngineBurn3X.Name = "txtEngineBurn3X"
        Me.txtEngineBurn3X.Size = New System.Drawing.Size(56, 20)
        Me.txtEngineBurn3X.TabIndex = 63
        '
        'txtEngineBurn2Z
        '
        Me.txtEngineBurn2Z.Location = New System.Drawing.Point(192, 312)
        Me.txtEngineBurn2Z.Name = "txtEngineBurn2Z"
        Me.txtEngineBurn2Z.Size = New System.Drawing.Size(56, 20)
        Me.txtEngineBurn2Z.TabIndex = 62
        '
        'txtEngineBurn2Y
        '
        Me.txtEngineBurn2Y.Location = New System.Drawing.Point(112, 312)
        Me.txtEngineBurn2Y.Name = "txtEngineBurn2Y"
        Me.txtEngineBurn2Y.Size = New System.Drawing.Size(56, 20)
        Me.txtEngineBurn2Y.TabIndex = 61
        '
        'txtEngineBurn2X
        '
        Me.txtEngineBurn2X.Location = New System.Drawing.Point(32, 312)
        Me.txtEngineBurn2X.Name = "txtEngineBurn2X"
        Me.txtEngineBurn2X.Size = New System.Drawing.Size(56, 20)
        Me.txtEngineBurn2X.TabIndex = 60
        '
        'txtEngineBurn1Z
        '
        Me.txtEngineBurn1Z.Location = New System.Drawing.Point(192, 288)
        Me.txtEngineBurn1Z.Name = "txtEngineBurn1Z"
        Me.txtEngineBurn1Z.Size = New System.Drawing.Size(56, 20)
        Me.txtEngineBurn1Z.TabIndex = 59
        '
        'txtEngineBurn1Y
        '
        Me.txtEngineBurn1Y.Location = New System.Drawing.Point(112, 288)
        Me.txtEngineBurn1Y.Name = "txtEngineBurn1Y"
        Me.txtEngineBurn1Y.Size = New System.Drawing.Size(56, 20)
        Me.txtEngineBurn1Y.TabIndex = 58
        '
        'txtEngineBurn1X
        '
        Me.txtEngineBurn1X.Location = New System.Drawing.Point(32, 288)
        Me.txtEngineBurn1X.Name = "txtEngineBurn1X"
        Me.txtEngineBurn1X.Size = New System.Drawing.Size(56, 20)
        Me.txtEngineBurn1X.TabIndex = 57
        '
        'Label39
        '
        Me.Label39.Location = New System.Drawing.Point(192, 264)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(56, 23)
        Me.Label39.TabIndex = 56
        Me.Label39.Text = "Z"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label38
        '
        Me.Label38.Location = New System.Drawing.Point(112, 264)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(56, 23)
        Me.Label38.TabIndex = 55
        Me.Label38.Text = "Y"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label37
        '
        Me.Label37.Location = New System.Drawing.Point(32, 264)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(56, 23)
        Me.Label37.TabIndex = 54
        Me.Label37.Text = "X"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label36
        '
        Me.Label36.Location = New System.Drawing.Point(8, 384)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(24, 20)
        Me.Label36.TabIndex = 53
        Me.Label36.Text = "5:"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label35
        '
        Me.Label35.Location = New System.Drawing.Point(8, 360)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(24, 20)
        Me.Label35.TabIndex = 52
        Me.Label35.Text = "4:"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label33
        '
        Me.Label33.Location = New System.Drawing.Point(8, 336)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(24, 20)
        Me.Label33.TabIndex = 51
        Me.Label33.Text = "3:"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label34
        '
        Me.Label34.Location = New System.Drawing.Point(8, 312)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(24, 20)
        Me.Label34.TabIndex = 50
        Me.Label34.Text = "2:"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label32
        '
        Me.Label32.Location = New System.Drawing.Point(8, 288)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(24, 20)
        Me.Label32.TabIndex = 49
        Me.Label32.Text = "1:"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label31
        '
        Me.Label31.Location = New System.Drawing.Point(8, 240)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(256, 23)
        Me.Label31.TabIndex = 48
        Me.Label31.Text = "Vertex Data"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboEngineBurnsParent
        '
        Me.cboEngineBurnsParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEngineBurnsParent.FormattingEnabled = True
        Me.cboEngineBurnsParent.Location = New System.Drawing.Point(96, 208)
        Me.cboEngineBurnsParent.Name = "cboEngineBurnsParent"
        Me.cboEngineBurnsParent.Size = New System.Drawing.Size(168, 21)
        Me.cboEngineBurnsParent.TabIndex = 47
        '
        'Label30
        '
        Me.Label30.Location = New System.Drawing.Point(8, 208)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(80, 23)
        Me.Label30.TabIndex = 46
        Me.Label30.Text = "Parent:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdEngineBurnsRename
        '
        Me.cmdEngineBurnsRename.Location = New System.Drawing.Point(184, 176)
        Me.cmdEngineBurnsRename.Name = "cmdEngineBurnsRename"
        Me.cmdEngineBurnsRename.Size = New System.Drawing.Size(80, 24)
        Me.cmdEngineBurnsRename.TabIndex = 45
        Me.cmdEngineBurnsRename.Text = "Rename"
        Me.cmdEngineBurnsRename.UseVisualStyleBackColor = True
        '
        'cstEngineBurns
        '
        Me.cstEngineBurns.FormattingEnabled = True
        Me.cstEngineBurns.Location = New System.Drawing.Point(8, 8)
        Me.cstEngineBurns.Name = "cstEngineBurns"
        Me.cstEngineBurns.Size = New System.Drawing.Size(256, 154)
        Me.cstEngineBurns.TabIndex = 44
        Me.cstEngineBurns.ThreeDCheckBoxes = True
        '
        'cmdEngineBurnsRemove
        '
        Me.cmdEngineBurnsRemove.Location = New System.Drawing.Point(96, 176)
        Me.cmdEngineBurnsRemove.Name = "cmdEngineBurnsRemove"
        Me.cmdEngineBurnsRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdEngineBurnsRemove.TabIndex = 41
        Me.cmdEngineBurnsRemove.Text = "Remove"
        Me.cmdEngineBurnsRemove.UseVisualStyleBackColor = True
        '
        'cmdEngineBurnsAdd
        '
        Me.cmdEngineBurnsAdd.Location = New System.Drawing.Point(8, 176)
        Me.cmdEngineBurnsAdd.Name = "cmdEngineBurnsAdd"
        Me.cmdEngineBurnsAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdEngineBurnsAdd.TabIndex = 40
        Me.cmdEngineBurnsAdd.Text = "Add"
        Me.cmdEngineBurnsAdd.UseVisualStyleBackColor = True
        '
        'tabNavLights
        '
        Me.tabNavLights.AutoScroll = True
        Me.tabNavLights.Controls.Add(Me.pbxNavLightsSample)
        Me.tabNavLights.Controls.Add(Me.chkNavLightsHighEnd)
        Me.tabNavLights.Controls.Add(Me.chkNavLightsVisibleSprite)
        Me.tabNavLights.Controls.Add(Me.txtNavlightsColourB)
        Me.tabNavLights.Controls.Add(Me.txtNavlightsColourG)
        Me.tabNavLights.Controls.Add(Me.txtNavlightsColourR)
        Me.tabNavLights.Controls.Add(Me.txtNavLightsDistance)
        Me.tabNavLights.Controls.Add(Me.txtNavLightsStyle)
        Me.tabNavLights.Controls.Add(Me.txtNavLightsFrequency)
        Me.tabNavLights.Controls.Add(Me.txtNavLightsPhase)
        Me.tabNavLights.Controls.Add(Me.txtNavLightsSize)
        Me.tabNavLights.Controls.Add(Me.txtNavLightsSection)
        Me.tabNavLights.Controls.Add(Me.Label50)
        Me.tabNavLights.Controls.Add(Me.Label51)
        Me.tabNavLights.Controls.Add(Me.Label52)
        Me.tabNavLights.Controls.Add(Me.Label101)
        Me.tabNavLights.Controls.Add(Me.Label47)
        Me.tabNavLights.Controls.Add(Me.Label46)
        Me.tabNavLights.Controls.Add(Me.Label45)
        Me.tabNavLights.Controls.Add(Me.Label44)
        Me.tabNavLights.Controls.Add(Me.Label43)
        Me.tabNavLights.Controls.Add(Me.cboNavLightsName)
        Me.tabNavLights.Controls.Add(Me.Label42)
        Me.tabNavLights.Controls.Add(Me.cstNavLights)
        Me.tabNavLights.Controls.Add(Me.cmdNavLightsRemove)
        Me.tabNavLights.Controls.Add(Me.cmdNavLightsAdd)
        Me.tabNavLights.Location = New System.Drawing.Point(4, 112)
        Me.tabNavLights.Name = "tabNavLights"
        Me.tabNavLights.Padding = New System.Windows.Forms.Padding(3)
        Me.tabNavLights.Size = New System.Drawing.Size(288, 375)
        Me.tabNavLights.TabIndex = 12
        Me.tabNavLights.Text = "NavLights"
        Me.tabNavLights.UseVisualStyleBackColor = True
        '
        'pbxNavLightsSample
        '
        Me.pbxNavLightsSample.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pbxNavLightsSample.Location = New System.Drawing.Point(184, 176)
        Me.pbxNavLightsSample.Name = "pbxNavLightsSample"
        Me.pbxNavLightsSample.Size = New System.Drawing.Size(80, 24)
        Me.pbxNavLightsSample.TabIndex = 75
        Me.pbxNavLightsSample.TabStop = False
        '
        'chkNavLightsHighEnd
        '
        Me.chkNavLightsHighEnd.Location = New System.Drawing.Point(8, 479)
        Me.chkNavLightsHighEnd.Name = "chkNavLightsHighEnd"
        Me.chkNavLightsHighEnd.Size = New System.Drawing.Size(240, 24)
        Me.chkNavLightsHighEnd.TabIndex = 74
        Me.chkNavLightsHighEnd.Text = "Cast light on high-end systems only"
        Me.chkNavLightsHighEnd.UseVisualStyleBackColor = True
        '
        'chkNavLightsVisibleSprite
        '
        Me.chkNavLightsVisibleSprite.Location = New System.Drawing.Point(8, 456)
        Me.chkNavLightsVisibleSprite.Name = "chkNavLightsVisibleSprite"
        Me.chkNavLightsVisibleSprite.Size = New System.Drawing.Size(240, 24)
        Me.chkNavLightsVisibleSprite.TabIndex = 73
        Me.chkNavLightsVisibleSprite.Text = "Visible sprite"
        Me.chkNavLightsVisibleSprite.UseVisualStyleBackColor = True
        '
        'txtNavlightsColourB
        '
        Me.txtNavlightsColourB.Location = New System.Drawing.Point(96, 427)
        Me.txtNavlightsColourB.Name = "txtNavlightsColourB"
        Me.txtNavlightsColourB.Size = New System.Drawing.Size(152, 20)
        Me.txtNavlightsColourB.TabIndex = 71
        '
        'txtNavlightsColourG
        '
        Me.txtNavlightsColourG.Location = New System.Drawing.Point(96, 403)
        Me.txtNavlightsColourG.Name = "txtNavlightsColourG"
        Me.txtNavlightsColourG.Size = New System.Drawing.Size(152, 20)
        Me.txtNavlightsColourG.TabIndex = 70
        '
        'txtNavlightsColourR
        '
        Me.txtNavlightsColourR.Location = New System.Drawing.Point(96, 379)
        Me.txtNavlightsColourR.Name = "txtNavlightsColourR"
        Me.txtNavlightsColourR.Size = New System.Drawing.Size(152, 20)
        Me.txtNavlightsColourR.TabIndex = 69
        '
        'txtNavLightsDistance
        '
        Me.txtNavLightsDistance.Location = New System.Drawing.Point(96, 355)
        Me.txtNavLightsDistance.Name = "txtNavLightsDistance"
        Me.txtNavLightsDistance.Size = New System.Drawing.Size(152, 20)
        Me.txtNavLightsDistance.TabIndex = 68
        '
        'txtNavLightsStyle
        '
        Me.txtNavLightsStyle.Location = New System.Drawing.Point(96, 331)
        Me.txtNavLightsStyle.Name = "txtNavLightsStyle"
        Me.txtNavLightsStyle.Size = New System.Drawing.Size(152, 20)
        Me.txtNavLightsStyle.TabIndex = 67
        '
        'txtNavLightsFrequency
        '
        Me.txtNavLightsFrequency.Location = New System.Drawing.Point(96, 307)
        Me.txtNavLightsFrequency.Name = "txtNavLightsFrequency"
        Me.txtNavLightsFrequency.Size = New System.Drawing.Size(152, 20)
        Me.txtNavLightsFrequency.TabIndex = 66
        '
        'txtNavLightsPhase
        '
        Me.txtNavLightsPhase.Location = New System.Drawing.Point(96, 283)
        Me.txtNavLightsPhase.Name = "txtNavLightsPhase"
        Me.txtNavLightsPhase.Size = New System.Drawing.Size(152, 20)
        Me.txtNavLightsPhase.TabIndex = 65
        '
        'txtNavLightsSize
        '
        Me.txtNavLightsSize.Location = New System.Drawing.Point(96, 259)
        Me.txtNavLightsSize.Name = "txtNavLightsSize"
        Me.txtNavLightsSize.Size = New System.Drawing.Size(152, 20)
        Me.txtNavLightsSize.TabIndex = 64
        '
        'txtNavLightsSection
        '
        Me.txtNavLightsSection.Location = New System.Drawing.Point(96, 235)
        Me.txtNavLightsSection.Name = "txtNavLightsSection"
        Me.txtNavLightsSection.Size = New System.Drawing.Size(152, 20)
        Me.txtNavLightsSection.TabIndex = 63
        '
        'Label50
        '
        Me.Label50.Location = New System.Drawing.Point(8, 424)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(80, 20)
        Me.Label50.TabIndex = 61
        Me.Label50.Text = "Colour B:"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label51
        '
        Me.Label51.Location = New System.Drawing.Point(8, 400)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(80, 20)
        Me.Label51.TabIndex = 60
        Me.Label51.Text = "Colour G:"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label52
        '
        Me.Label52.Location = New System.Drawing.Point(8, 376)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(80, 20)
        Me.Label52.TabIndex = 59
        Me.Label52.Text = "Colour R:"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label101
        '
        Me.Label101.Location = New System.Drawing.Point(8, 352)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(80, 20)
        Me.Label101.TabIndex = 58
        Me.Label101.Text = "Distance:"
        Me.Label101.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label47
        '
        Me.Label47.Location = New System.Drawing.Point(8, 328)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(80, 20)
        Me.Label47.TabIndex = 57
        Me.Label47.Text = "Style:"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label46
        '
        Me.Label46.Location = New System.Drawing.Point(8, 304)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(80, 20)
        Me.Label46.TabIndex = 56
        Me.Label46.Text = "Frequency:"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label45
        '
        Me.Label45.Location = New System.Drawing.Point(8, 280)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(80, 20)
        Me.Label45.TabIndex = 55
        Me.Label45.Text = "Phase:"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label44
        '
        Me.Label44.Location = New System.Drawing.Point(8, 256)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(80, 20)
        Me.Label44.TabIndex = 54
        Me.Label44.Text = "Size:"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label43
        '
        Me.Label43.Location = New System.Drawing.Point(8, 232)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(80, 20)
        Me.Label43.TabIndex = 53
        Me.Label43.Text = "Section:"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboNavLightsName
        '
        Me.cboNavLightsName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNavLightsName.FormattingEnabled = True
        Me.cboNavLightsName.Location = New System.Drawing.Point(96, 208)
        Me.cboNavLightsName.Name = "cboNavLightsName"
        Me.cboNavLightsName.Size = New System.Drawing.Size(168, 21)
        Me.cboNavLightsName.TabIndex = 52
        '
        'Label42
        '
        Me.Label42.Location = New System.Drawing.Point(8, 208)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(80, 23)
        Me.Label42.TabIndex = 51
        Me.Label42.Text = "Name:"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cstNavLights
        '
        Me.cstNavLights.FormattingEnabled = True
        Me.cstNavLights.Location = New System.Drawing.Point(8, 8)
        Me.cstNavLights.Name = "cstNavLights"
        Me.cstNavLights.Size = New System.Drawing.Size(256, 154)
        Me.cstNavLights.TabIndex = 50
        Me.cstNavLights.ThreeDCheckBoxes = True
        '
        'cmdNavLightsRemove
        '
        Me.cmdNavLightsRemove.Location = New System.Drawing.Point(96, 176)
        Me.cmdNavLightsRemove.Name = "cmdNavLightsRemove"
        Me.cmdNavLightsRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdNavLightsRemove.TabIndex = 49
        Me.cmdNavLightsRemove.Text = "Remove"
        Me.cmdNavLightsRemove.UseVisualStyleBackColor = True
        '
        'cmdNavLightsAdd
        '
        Me.cmdNavLightsAdd.Location = New System.Drawing.Point(8, 176)
        Me.cmdNavLightsAdd.Name = "cmdNavLightsAdd"
        Me.cmdNavLightsAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdNavLightsAdd.TabIndex = 48
        Me.cmdNavLightsAdd.Text = "Add"
        Me.cmdNavLightsAdd.UseVisualStyleBackColor = True
        '
        'tabMarkers
        '
        Me.tabMarkers.AutoScroll = True
        Me.tabMarkers.Controls.Add(Me.fraMarkersTransform)
        Me.tabMarkers.Controls.Add(Me.cboMarkersParent)
        Me.tabMarkers.Controls.Add(Me.Label48)
        Me.tabMarkers.Controls.Add(Me.cmdMarkersRename)
        Me.tabMarkers.Controls.Add(Me.cstMarkers)
        Me.tabMarkers.Controls.Add(Me.cmdMarkersRemove)
        Me.tabMarkers.Controls.Add(Me.cmdMarkersAdd)
        Me.tabMarkers.Location = New System.Drawing.Point(4, 112)
        Me.tabMarkers.Name = "tabMarkers"
        Me.tabMarkers.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMarkers.Size = New System.Drawing.Size(288, 375)
        Me.tabMarkers.TabIndex = 13
        Me.tabMarkers.Text = "Markers"
        Me.tabMarkers.UseVisualStyleBackColor = True
        '
        'fraMarkersTransform
        '
        Me.fraMarkersTransform.Controls.Add(Me.txtMarkerRotationZ)
        Me.fraMarkersTransform.Controls.Add(Me.Label58)
        Me.fraMarkersTransform.Controls.Add(Me.txtMarkerRotationY)
        Me.fraMarkersTransform.Controls.Add(Me.Label59)
        Me.fraMarkersTransform.Controls.Add(Me.txtMarkerRotationX)
        Me.fraMarkersTransform.Controls.Add(Me.Label60)
        Me.fraMarkersTransform.Controls.Add(Me.txtMarkerPositionZ)
        Me.fraMarkersTransform.Controls.Add(Me.Label61)
        Me.fraMarkersTransform.Controls.Add(Me.txtMarkerPositionY)
        Me.fraMarkersTransform.Controls.Add(Me.Label62)
        Me.fraMarkersTransform.Controls.Add(Me.txtMarkerPositionX)
        Me.fraMarkersTransform.Controls.Add(Me.Label63)
        Me.fraMarkersTransform.Location = New System.Drawing.Point(8, 240)
        Me.fraMarkersTransform.Name = "fraMarkersTransform"
        Me.fraMarkersTransform.Size = New System.Drawing.Size(256, 176)
        Me.fraMarkersTransform.TabIndex = 54
        Me.fraMarkersTransform.TabStop = False
        Me.fraMarkersTransform.Text = "Marker Transform"
        '
        'txtMarkerRotationZ
        '
        Me.txtMarkerRotationZ.Location = New System.Drawing.Point(80, 144)
        Me.txtMarkerRotationZ.Name = "txtMarkerRotationZ"
        Me.txtMarkerRotationZ.Size = New System.Drawing.Size(144, 20)
        Me.txtMarkerRotationZ.TabIndex = 70
        '
        'Label58
        '
        Me.Label58.Location = New System.Drawing.Point(8, 144)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(72, 20)
        Me.Label58.TabIndex = 69
        Me.Label58.Text = "Rotation Z:"
        Me.Label58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMarkerRotationY
        '
        Me.txtMarkerRotationY.Location = New System.Drawing.Point(80, 120)
        Me.txtMarkerRotationY.Name = "txtMarkerRotationY"
        Me.txtMarkerRotationY.Size = New System.Drawing.Size(144, 20)
        Me.txtMarkerRotationY.TabIndex = 68
        '
        'Label59
        '
        Me.Label59.Location = New System.Drawing.Point(8, 120)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(72, 20)
        Me.Label59.TabIndex = 67
        Me.Label59.Text = "Rotation Y:"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMarkerRotationX
        '
        Me.txtMarkerRotationX.Location = New System.Drawing.Point(80, 96)
        Me.txtMarkerRotationX.Name = "txtMarkerRotationX"
        Me.txtMarkerRotationX.Size = New System.Drawing.Size(144, 20)
        Me.txtMarkerRotationX.TabIndex = 66
        '
        'Label60
        '
        Me.Label60.Location = New System.Drawing.Point(8, 96)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(72, 20)
        Me.Label60.TabIndex = 65
        Me.Label60.Text = "Rotation X:"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMarkerPositionZ
        '
        Me.txtMarkerPositionZ.Location = New System.Drawing.Point(80, 64)
        Me.txtMarkerPositionZ.Name = "txtMarkerPositionZ"
        Me.txtMarkerPositionZ.Size = New System.Drawing.Size(144, 20)
        Me.txtMarkerPositionZ.TabIndex = 64
        '
        'Label61
        '
        Me.Label61.Location = New System.Drawing.Point(8, 64)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(72, 20)
        Me.Label61.TabIndex = 63
        Me.Label61.Text = "Position Z:"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMarkerPositionY
        '
        Me.txtMarkerPositionY.Location = New System.Drawing.Point(80, 40)
        Me.txtMarkerPositionY.Name = "txtMarkerPositionY"
        Me.txtMarkerPositionY.Size = New System.Drawing.Size(144, 20)
        Me.txtMarkerPositionY.TabIndex = 62
        '
        'Label62
        '
        Me.Label62.Location = New System.Drawing.Point(8, 40)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(72, 20)
        Me.Label62.TabIndex = 61
        Me.Label62.Text = "Position Y:"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtMarkerPositionX
        '
        Me.txtMarkerPositionX.Location = New System.Drawing.Point(80, 16)
        Me.txtMarkerPositionX.Name = "txtMarkerPositionX"
        Me.txtMarkerPositionX.Size = New System.Drawing.Size(144, 20)
        Me.txtMarkerPositionX.TabIndex = 60
        '
        'Label63
        '
        Me.Label63.Location = New System.Drawing.Point(8, 16)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(72, 20)
        Me.Label63.TabIndex = 59
        Me.Label63.Text = "Position X:"
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboMarkersParent
        '
        Me.cboMarkersParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMarkersParent.FormattingEnabled = True
        Me.cboMarkersParent.Location = New System.Drawing.Point(96, 208)
        Me.cboMarkersParent.Name = "cboMarkersParent"
        Me.cboMarkersParent.Size = New System.Drawing.Size(168, 21)
        Me.cboMarkersParent.TabIndex = 53
        '
        'Label48
        '
        Me.Label48.Location = New System.Drawing.Point(8, 208)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(80, 23)
        Me.Label48.TabIndex = 52
        Me.Label48.Text = "Parent:"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdMarkersRename
        '
        Me.cmdMarkersRename.Location = New System.Drawing.Point(184, 176)
        Me.cmdMarkersRename.Name = "cmdMarkersRename"
        Me.cmdMarkersRename.Size = New System.Drawing.Size(80, 24)
        Me.cmdMarkersRename.TabIndex = 51
        Me.cmdMarkersRename.Text = "Rename"
        Me.cmdMarkersRename.UseVisualStyleBackColor = True
        '
        'cstMarkers
        '
        Me.cstMarkers.FormattingEnabled = True
        Me.cstMarkers.Location = New System.Drawing.Point(8, 8)
        Me.cstMarkers.Name = "cstMarkers"
        Me.cstMarkers.Size = New System.Drawing.Size(256, 154)
        Me.cstMarkers.TabIndex = 50
        Me.cstMarkers.ThreeDCheckBoxes = True
        '
        'cmdMarkersRemove
        '
        Me.cmdMarkersRemove.Location = New System.Drawing.Point(96, 176)
        Me.cmdMarkersRemove.Name = "cmdMarkersRemove"
        Me.cmdMarkersRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdMarkersRemove.TabIndex = 49
        Me.cmdMarkersRemove.Text = "Remove"
        Me.cmdMarkersRemove.UseVisualStyleBackColor = True
        '
        'cmdMarkersAdd
        '
        Me.cmdMarkersAdd.Location = New System.Drawing.Point(8, 176)
        Me.cmdMarkersAdd.Name = "cmdMarkersAdd"
        Me.cmdMarkersAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdMarkersAdd.TabIndex = 48
        Me.cmdMarkersAdd.Text = "Add"
        Me.cmdMarkersAdd.UseVisualStyleBackColor = True
        '
        'tabDockpaths
        '
        Me.tabDockpaths.AutoScroll = True
        Me.tabDockpaths.Controls.Add(Me.chkDockpathsUseAnim)
        Me.tabDockpaths.Controls.Add(Me.chkDockpathsLatchPath)
        Me.tabDockpaths.Controls.Add(Me.chkDockpathsExitPath)
        Me.tabDockpaths.Controls.Add(Me.txtDockpathsLinkedPaths)
        Me.tabDockpaths.Controls.Add(Me.Label67)
        Me.tabDockpaths.Controls.Add(Me.txtDockpathsDockFamilies)
        Me.tabDockpaths.Controls.Add(Me.Label68)
        Me.tabDockpaths.Controls.Add(Me.txtDockpathsGlobalTolerance)
        Me.tabDockpaths.Controls.Add(Me.Label69)
        Me.tabDockpaths.Controls.Add(Me.fraDockpathsKeyframes)
        Me.tabDockpaths.Controls.Add(Me.txtDockpathsParentName)
        Me.tabDockpaths.Controls.Add(Me.Label49)
        Me.tabDockpaths.Controls.Add(Me.cmdDockpathsRename)
        Me.tabDockpaths.Controls.Add(Me.cstDockpaths)
        Me.tabDockpaths.Controls.Add(Me.cmdDockpathsRemove)
        Me.tabDockpaths.Controls.Add(Me.cmdDockpathsAdd)
        Me.tabDockpaths.Location = New System.Drawing.Point(4, 112)
        Me.tabDockpaths.Name = "tabDockpaths"
        Me.tabDockpaths.Padding = New System.Windows.Forms.Padding(3)
        Me.tabDockpaths.Size = New System.Drawing.Size(288, 375)
        Me.tabDockpaths.TabIndex = 14
        Me.tabDockpaths.Text = "Dockpaths"
        Me.tabDockpaths.UseVisualStyleBackColor = True
        '
        'chkDockpathsUseAnim
        '
        Me.chkDockpathsUseAnim.Location = New System.Drawing.Point(8, 360)
        Me.chkDockpathsUseAnim.Name = "chkDockpathsUseAnim"
        Me.chkDockpathsUseAnim.Size = New System.Drawing.Size(248, 24)
        Me.chkDockpathsUseAnim.TabIndex = 92
        Me.chkDockpathsUseAnim.Text = "Use docking animation"
        Me.chkDockpathsUseAnim.UseVisualStyleBackColor = True
        '
        'chkDockpathsLatchPath
        '
        Me.chkDockpathsLatchPath.Location = New System.Drawing.Point(8, 336)
        Me.chkDockpathsLatchPath.Name = "chkDockpathsLatchPath"
        Me.chkDockpathsLatchPath.Size = New System.Drawing.Size(248, 24)
        Me.chkDockpathsLatchPath.TabIndex = 91
        Me.chkDockpathsLatchPath.Text = "Latch path"
        Me.chkDockpathsLatchPath.UseVisualStyleBackColor = True
        '
        'chkDockpathsExitPath
        '
        Me.chkDockpathsExitPath.Location = New System.Drawing.Point(8, 312)
        Me.chkDockpathsExitPath.Name = "chkDockpathsExitPath"
        Me.chkDockpathsExitPath.Size = New System.Drawing.Size(248, 24)
        Me.chkDockpathsExitPath.TabIndex = 90
        Me.chkDockpathsExitPath.Text = "Exit path"
        Me.chkDockpathsExitPath.UseVisualStyleBackColor = True
        '
        'txtDockpathsLinkedPaths
        '
        Me.txtDockpathsLinkedPaths.Location = New System.Drawing.Point(96, 285)
        Me.txtDockpathsLinkedPaths.Name = "txtDockpathsLinkedPaths"
        Me.txtDockpathsLinkedPaths.Size = New System.Drawing.Size(144, 20)
        Me.txtDockpathsLinkedPaths.TabIndex = 82
        '
        'Label67
        '
        Me.Label67.Location = New System.Drawing.Point(8, 285)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(88, 20)
        Me.Label67.TabIndex = 81
        Me.Label67.Text = "Linked paths:"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDockpathsDockFamilies
        '
        Me.txtDockpathsDockFamilies.Location = New System.Drawing.Point(96, 261)
        Me.txtDockpathsDockFamilies.Name = "txtDockpathsDockFamilies"
        Me.txtDockpathsDockFamilies.Size = New System.Drawing.Size(144, 20)
        Me.txtDockpathsDockFamilies.TabIndex = 80
        '
        'Label68
        '
        Me.Label68.Location = New System.Drawing.Point(8, 261)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(88, 20)
        Me.Label68.TabIndex = 79
        Me.Label68.Text = "Dock families:"
        Me.Label68.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDockpathsGlobalTolerance
        '
        Me.txtDockpathsGlobalTolerance.Location = New System.Drawing.Point(96, 237)
        Me.txtDockpathsGlobalTolerance.Name = "txtDockpathsGlobalTolerance"
        Me.txtDockpathsGlobalTolerance.Size = New System.Drawing.Size(144, 20)
        Me.txtDockpathsGlobalTolerance.TabIndex = 78
        '
        'Label69
        '
        Me.Label69.Location = New System.Drawing.Point(8, 237)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(88, 20)
        Me.Label69.TabIndex = 77
        Me.Label69.Text = "Global tolerance"
        Me.Label69.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'fraDockpathsKeyframes
        '
        Me.fraDockpathsKeyframes.Controls.Add(Me.chkDockpathsKeyframeClearReservation)
        Me.fraDockpathsKeyframes.Controls.Add(Me.chkDockpathsKeyframeUseClipPlane)
        Me.fraDockpathsKeyframes.Controls.Add(Me.chkDockpathsKeyframeQueueOrigin)
        Me.fraDockpathsKeyframes.Controls.Add(Me.chkDockpathsKeyframePlayerInControl)
        Me.fraDockpathsKeyframes.Controls.Add(Me.chkDockpathsKeyframeForceCloseBehaviour)
        Me.fraDockpathsKeyframes.Controls.Add(Me.chkDockpathsKeyframeCheckRotation)
        Me.fraDockpathsKeyframes.Controls.Add(Me.chkDockpathsKeyframeDropFocus)
        Me.fraDockpathsKeyframes.Controls.Add(Me.chkDockpathsKeyframeUseRotation)
        Me.fraDockpathsKeyframes.Controls.Add(Me.txtDockpathsKeyframeMaxSpeed)
        Me.fraDockpathsKeyframes.Controls.Add(Me.Label65)
        Me.fraDockpathsKeyframes.Controls.Add(Me.txtDockpathsKeyframeTolerance)
        Me.fraDockpathsKeyframes.Controls.Add(Me.Label66)
        Me.fraDockpathsKeyframes.Controls.Add(Me.txtDockpathsKeyframeRotationZ)
        Me.fraDockpathsKeyframes.Controls.Add(Me.Label53)
        Me.fraDockpathsKeyframes.Controls.Add(Me.txtDockpathsKeyframeRotationY)
        Me.fraDockpathsKeyframes.Controls.Add(Me.Label54)
        Me.fraDockpathsKeyframes.Controls.Add(Me.txtDockpathsKeyframeRotationX)
        Me.fraDockpathsKeyframes.Controls.Add(Me.Label55)
        Me.fraDockpathsKeyframes.Controls.Add(Me.txtDockpathsKeyframePositionZ)
        Me.fraDockpathsKeyframes.Controls.Add(Me.Label56)
        Me.fraDockpathsKeyframes.Controls.Add(Me.txtDockpathsKeyframePositionY)
        Me.fraDockpathsKeyframes.Controls.Add(Me.Label57)
        Me.fraDockpathsKeyframes.Controls.Add(Me.txtDockpathsKeyframePositionX)
        Me.fraDockpathsKeyframes.Controls.Add(Me.Label64)
        Me.fraDockpathsKeyframes.Controls.Add(Me.sldDockpathsKeyframe)
        Me.fraDockpathsKeyframes.Controls.Add(Me.cmdDockpathsKeyframesInsert)
        Me.fraDockpathsKeyframes.Controls.Add(Me.cmdDockpathsKeyframesAdd)
        Me.fraDockpathsKeyframes.Controls.Add(Me.cmdDockpathsKeyframesRemove)
        Me.fraDockpathsKeyframes.Location = New System.Drawing.Point(8, 392)
        Me.fraDockpathsKeyframes.Name = "fraDockpathsKeyframes"
        Me.fraDockpathsKeyframes.Size = New System.Drawing.Size(256, 520)
        Me.fraDockpathsKeyframes.TabIndex = 41
        Me.fraDockpathsKeyframes.TabStop = False
        Me.fraDockpathsKeyframes.Text = "Dockpoints"
        '
        'chkDockpathsKeyframeClearReservation
        '
        Me.chkDockpathsKeyframeClearReservation.Location = New System.Drawing.Point(8, 488)
        Me.chkDockpathsKeyframeClearReservation.Name = "chkDockpathsKeyframeClearReservation"
        Me.chkDockpathsKeyframeClearReservation.Size = New System.Drawing.Size(240, 24)
        Me.chkDockpathsKeyframeClearReservation.TabIndex = 94
        Me.chkDockpathsKeyframeClearReservation.Text = "Clear reservation"
        Me.chkDockpathsKeyframeClearReservation.UseVisualStyleBackColor = True
        '
        'chkDockpathsKeyframeUseClipPlane
        '
        Me.chkDockpathsKeyframeUseClipPlane.Location = New System.Drawing.Point(8, 464)
        Me.chkDockpathsKeyframeUseClipPlane.Name = "chkDockpathsKeyframeUseClipPlane"
        Me.chkDockpathsKeyframeUseClipPlane.Size = New System.Drawing.Size(240, 24)
        Me.chkDockpathsKeyframeUseClipPlane.TabIndex = 93
        Me.chkDockpathsKeyframeUseClipPlane.Text = "Use Clip Plane"
        Me.chkDockpathsKeyframeUseClipPlane.UseVisualStyleBackColor = True
        '
        'chkDockpathsKeyframeQueueOrigin
        '
        Me.chkDockpathsKeyframeQueueOrigin.Location = New System.Drawing.Point(8, 440)
        Me.chkDockpathsKeyframeQueueOrigin.Name = "chkDockpathsKeyframeQueueOrigin"
        Me.chkDockpathsKeyframeQueueOrigin.Size = New System.Drawing.Size(240, 24)
        Me.chkDockpathsKeyframeQueueOrigin.TabIndex = 92
        Me.chkDockpathsKeyframeQueueOrigin.Text = "Queue origin"
        Me.chkDockpathsKeyframeQueueOrigin.UseVisualStyleBackColor = True
        '
        'chkDockpathsKeyframePlayerInControl
        '
        Me.chkDockpathsKeyframePlayerInControl.Location = New System.Drawing.Point(8, 416)
        Me.chkDockpathsKeyframePlayerInControl.Name = "chkDockpathsKeyframePlayerInControl"
        Me.chkDockpathsKeyframePlayerInControl.Size = New System.Drawing.Size(240, 24)
        Me.chkDockpathsKeyframePlayerInControl.TabIndex = 91
        Me.chkDockpathsKeyframePlayerInControl.Text = "Player is in control"
        Me.chkDockpathsKeyframePlayerInControl.UseVisualStyleBackColor = True
        '
        'chkDockpathsKeyframeForceCloseBehaviour
        '
        Me.chkDockpathsKeyframeForceCloseBehaviour.Location = New System.Drawing.Point(8, 392)
        Me.chkDockpathsKeyframeForceCloseBehaviour.Name = "chkDockpathsKeyframeForceCloseBehaviour"
        Me.chkDockpathsKeyframeForceCloseBehaviour.Size = New System.Drawing.Size(240, 24)
        Me.chkDockpathsKeyframeForceCloseBehaviour.TabIndex = 90
        Me.chkDockpathsKeyframeForceCloseBehaviour.Text = "Force close behaviour"
        Me.chkDockpathsKeyframeForceCloseBehaviour.UseVisualStyleBackColor = True
        '
        'chkDockpathsKeyframeCheckRotation
        '
        Me.chkDockpathsKeyframeCheckRotation.Location = New System.Drawing.Point(8, 368)
        Me.chkDockpathsKeyframeCheckRotation.Name = "chkDockpathsKeyframeCheckRotation"
        Me.chkDockpathsKeyframeCheckRotation.Size = New System.Drawing.Size(240, 24)
        Me.chkDockpathsKeyframeCheckRotation.TabIndex = 89
        Me.chkDockpathsKeyframeCheckRotation.Text = "Check rotation"
        Me.chkDockpathsKeyframeCheckRotation.UseVisualStyleBackColor = True
        '
        'chkDockpathsKeyframeDropFocus
        '
        Me.chkDockpathsKeyframeDropFocus.Location = New System.Drawing.Point(8, 344)
        Me.chkDockpathsKeyframeDropFocus.Name = "chkDockpathsKeyframeDropFocus"
        Me.chkDockpathsKeyframeDropFocus.Size = New System.Drawing.Size(240, 24)
        Me.chkDockpathsKeyframeDropFocus.TabIndex = 88
        Me.chkDockpathsKeyframeDropFocus.Text = "Drop focus"
        Me.chkDockpathsKeyframeDropFocus.UseVisualStyleBackColor = True
        '
        'chkDockpathsKeyframeUseRotation
        '
        Me.chkDockpathsKeyframeUseRotation.Location = New System.Drawing.Point(8, 320)
        Me.chkDockpathsKeyframeUseRotation.Name = "chkDockpathsKeyframeUseRotation"
        Me.chkDockpathsKeyframeUseRotation.Size = New System.Drawing.Size(240, 24)
        Me.chkDockpathsKeyframeUseRotation.TabIndex = 87
        Me.chkDockpathsKeyframeUseRotation.Text = "Use Rotation"
        Me.chkDockpathsKeyframeUseRotation.UseVisualStyleBackColor = True
        '
        'txtDockpathsKeyframeMaxSpeed
        '
        Me.txtDockpathsKeyframeMaxSpeed.Location = New System.Drawing.Point(80, 288)
        Me.txtDockpathsKeyframeMaxSpeed.Name = "txtDockpathsKeyframeMaxSpeed"
        Me.txtDockpathsKeyframeMaxSpeed.Size = New System.Drawing.Size(144, 20)
        Me.txtDockpathsKeyframeMaxSpeed.TabIndex = 86
        '
        'Label65
        '
        Me.Label65.Location = New System.Drawing.Point(8, 288)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(72, 20)
        Me.Label65.TabIndex = 85
        Me.Label65.Text = "Max Speed:"
        Me.Label65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDockpathsKeyframeTolerance
        '
        Me.txtDockpathsKeyframeTolerance.Location = New System.Drawing.Point(80, 264)
        Me.txtDockpathsKeyframeTolerance.Name = "txtDockpathsKeyframeTolerance"
        Me.txtDockpathsKeyframeTolerance.Size = New System.Drawing.Size(144, 20)
        Me.txtDockpathsKeyframeTolerance.TabIndex = 84
        '
        'Label66
        '
        Me.Label66.Location = New System.Drawing.Point(8, 264)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(72, 20)
        Me.Label66.TabIndex = 83
        Me.Label66.Text = "Tolerance:"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDockpathsKeyframeRotationZ
        '
        Me.txtDockpathsKeyframeRotationZ.Location = New System.Drawing.Point(80, 232)
        Me.txtDockpathsKeyframeRotationZ.Name = "txtDockpathsKeyframeRotationZ"
        Me.txtDockpathsKeyframeRotationZ.Size = New System.Drawing.Size(144, 20)
        Me.txtDockpathsKeyframeRotationZ.TabIndex = 82
        '
        'Label53
        '
        Me.Label53.Location = New System.Drawing.Point(8, 232)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(72, 20)
        Me.Label53.TabIndex = 81
        Me.Label53.Text = "Rotation Z:"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDockpathsKeyframeRotationY
        '
        Me.txtDockpathsKeyframeRotationY.Location = New System.Drawing.Point(80, 208)
        Me.txtDockpathsKeyframeRotationY.Name = "txtDockpathsKeyframeRotationY"
        Me.txtDockpathsKeyframeRotationY.Size = New System.Drawing.Size(144, 20)
        Me.txtDockpathsKeyframeRotationY.TabIndex = 80
        '
        'Label54
        '
        Me.Label54.Location = New System.Drawing.Point(8, 208)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(72, 20)
        Me.Label54.TabIndex = 79
        Me.Label54.Text = "Rotation Y:"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDockpathsKeyframeRotationX
        '
        Me.txtDockpathsKeyframeRotationX.Location = New System.Drawing.Point(80, 184)
        Me.txtDockpathsKeyframeRotationX.Name = "txtDockpathsKeyframeRotationX"
        Me.txtDockpathsKeyframeRotationX.Size = New System.Drawing.Size(144, 20)
        Me.txtDockpathsKeyframeRotationX.TabIndex = 78
        '
        'Label55
        '
        Me.Label55.Location = New System.Drawing.Point(8, 184)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(72, 20)
        Me.Label55.TabIndex = 77
        Me.Label55.Text = "Rotation X:"
        Me.Label55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDockpathsKeyframePositionZ
        '
        Me.txtDockpathsKeyframePositionZ.Location = New System.Drawing.Point(80, 152)
        Me.txtDockpathsKeyframePositionZ.Name = "txtDockpathsKeyframePositionZ"
        Me.txtDockpathsKeyframePositionZ.Size = New System.Drawing.Size(144, 20)
        Me.txtDockpathsKeyframePositionZ.TabIndex = 76
        '
        'Label56
        '
        Me.Label56.Location = New System.Drawing.Point(8, 152)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(72, 20)
        Me.Label56.TabIndex = 75
        Me.Label56.Text = "Position Z:"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDockpathsKeyframePositionY
        '
        Me.txtDockpathsKeyframePositionY.Location = New System.Drawing.Point(80, 128)
        Me.txtDockpathsKeyframePositionY.Name = "txtDockpathsKeyframePositionY"
        Me.txtDockpathsKeyframePositionY.Size = New System.Drawing.Size(144, 20)
        Me.txtDockpathsKeyframePositionY.TabIndex = 74
        '
        'Label57
        '
        Me.Label57.Location = New System.Drawing.Point(8, 128)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(72, 20)
        Me.Label57.TabIndex = 73
        Me.Label57.Text = "Position Y:"
        Me.Label57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDockpathsKeyframePositionX
        '
        Me.txtDockpathsKeyframePositionX.Location = New System.Drawing.Point(80, 104)
        Me.txtDockpathsKeyframePositionX.Name = "txtDockpathsKeyframePositionX"
        Me.txtDockpathsKeyframePositionX.Size = New System.Drawing.Size(144, 20)
        Me.txtDockpathsKeyframePositionX.TabIndex = 72
        '
        'Label64
        '
        Me.Label64.Location = New System.Drawing.Point(8, 104)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(72, 20)
        Me.Label64.TabIndex = 71
        Me.Label64.Text = "Position X:"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'sldDockpathsKeyframe
        '
        Me.sldDockpathsKeyframe.LargeChange = 1
        Me.sldDockpathsKeyframe.Location = New System.Drawing.Point(8, 16)
        Me.sldDockpathsKeyframe.Maximum = 1
        Me.sldDockpathsKeyframe.Name = "sldDockpathsKeyframe"
        Me.sldDockpathsKeyframe.Size = New System.Drawing.Size(240, 45)
        Me.sldDockpathsKeyframe.TabIndex = 37
        Me.sldDockpathsKeyframe.TickStyle = System.Windows.Forms.TickStyle.Both
        '
        'cmdDockpathsKeyframesInsert
        '
        Me.cmdDockpathsKeyframesInsert.Location = New System.Drawing.Point(88, 72)
        Me.cmdDockpathsKeyframesInsert.Name = "cmdDockpathsKeyframesInsert"
        Me.cmdDockpathsKeyframesInsert.Size = New System.Drawing.Size(80, 24)
        Me.cmdDockpathsKeyframesInsert.TabIndex = 40
        Me.cmdDockpathsKeyframesInsert.Text = "Insert here"
        Me.cmdDockpathsKeyframesInsert.UseVisualStyleBackColor = True
        '
        'cmdDockpathsKeyframesAdd
        '
        Me.cmdDockpathsKeyframesAdd.Location = New System.Drawing.Point(8, 72)
        Me.cmdDockpathsKeyframesAdd.Name = "cmdDockpathsKeyframesAdd"
        Me.cmdDockpathsKeyframesAdd.Size = New System.Drawing.Size(72, 24)
        Me.cmdDockpathsKeyframesAdd.TabIndex = 38
        Me.cmdDockpathsKeyframesAdd.Text = "Add"
        Me.cmdDockpathsKeyframesAdd.UseVisualStyleBackColor = True
        '
        'cmdDockpathsKeyframesRemove
        '
        Me.cmdDockpathsKeyframesRemove.Location = New System.Drawing.Point(176, 72)
        Me.cmdDockpathsKeyframesRemove.Name = "cmdDockpathsKeyframesRemove"
        Me.cmdDockpathsKeyframesRemove.Size = New System.Drawing.Size(72, 24)
        Me.cmdDockpathsKeyframesRemove.TabIndex = 39
        Me.cmdDockpathsKeyframesRemove.Text = "Remove"
        Me.cmdDockpathsKeyframesRemove.UseVisualStyleBackColor = True
        '
        'txtDockpathsParentName
        '
        Me.txtDockpathsParentName.Location = New System.Drawing.Point(96, 213)
        Me.txtDockpathsParentName.Name = "txtDockpathsParentName"
        Me.txtDockpathsParentName.Size = New System.Drawing.Size(144, 20)
        Me.txtDockpathsParentName.TabIndex = 36
        '
        'Label49
        '
        Me.Label49.Location = New System.Drawing.Point(8, 213)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(88, 20)
        Me.Label49.TabIndex = 35
        Me.Label49.Text = "Parent:"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdDockpathsRename
        '
        Me.cmdDockpathsRename.Location = New System.Drawing.Point(184, 176)
        Me.cmdDockpathsRename.Name = "cmdDockpathsRename"
        Me.cmdDockpathsRename.Size = New System.Drawing.Size(80, 24)
        Me.cmdDockpathsRename.TabIndex = 34
        Me.cmdDockpathsRename.Text = "Rename"
        Me.cmdDockpathsRename.UseVisualStyleBackColor = True
        '
        'cstDockpaths
        '
        Me.cstDockpaths.FormattingEnabled = True
        Me.cstDockpaths.Location = New System.Drawing.Point(8, 8)
        Me.cstDockpaths.Name = "cstDockpaths"
        Me.cstDockpaths.Size = New System.Drawing.Size(256, 154)
        Me.cstDockpaths.TabIndex = 33
        Me.cstDockpaths.ThreeDCheckBoxes = True
        '
        'cmdDockpathsRemove
        '
        Me.cmdDockpathsRemove.Location = New System.Drawing.Point(96, 176)
        Me.cmdDockpathsRemove.Name = "cmdDockpathsRemove"
        Me.cmdDockpathsRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdDockpathsRemove.TabIndex = 32
        Me.cmdDockpathsRemove.Text = "Remove"
        Me.cmdDockpathsRemove.UseVisualStyleBackColor = True
        '
        'cmdDockpathsAdd
        '
        Me.cmdDockpathsAdd.Location = New System.Drawing.Point(8, 176)
        Me.cmdDockpathsAdd.Name = "cmdDockpathsAdd"
        Me.cmdDockpathsAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdDockpathsAdd.TabIndex = 31
        Me.cmdDockpathsAdd.Text = "Add"
        Me.cmdDockpathsAdd.UseVisualStyleBackColor = True
        '
        'tabLights
        '
        Me.tabLights.AutoScroll = True
        Me.tabLights.Controls.Add(Me.cboLightAtt)
        Me.tabLights.Controls.Add(Me.Label81)
        Me.tabLights.Controls.Add(Me.txtLightAttDist)
        Me.tabLights.Controls.Add(Me.Label80)
        Me.tabLights.Controls.Add(Me.txtLightSB)
        Me.tabLights.Controls.Add(Me.Label77)
        Me.tabLights.Controls.Add(Me.txtLightSG)
        Me.tabLights.Controls.Add(Me.Label78)
        Me.tabLights.Controls.Add(Me.txtLightSR)
        Me.tabLights.Controls.Add(Me.Label79)
        Me.tabLights.Controls.Add(Me.txtLightCB)
        Me.tabLights.Controls.Add(Me.Label74)
        Me.tabLights.Controls.Add(Me.txtLightCG)
        Me.tabLights.Controls.Add(Me.Label75)
        Me.tabLights.Controls.Add(Me.txtLightCR)
        Me.tabLights.Controls.Add(Me.Label76)
        Me.tabLights.Controls.Add(Me.txtLightTZ)
        Me.tabLights.Controls.Add(Me.lblLightTZ)
        Me.tabLights.Controls.Add(Me.txtLightTY)
        Me.tabLights.Controls.Add(Me.lblLightTY)
        Me.tabLights.Controls.Add(Me.txtLightTX)
        Me.tabLights.Controls.Add(Me.lblLightTX)
        Me.tabLights.Controls.Add(Me.cboLightType)
        Me.tabLights.Controls.Add(Me.Label70)
        Me.tabLights.Controls.Add(Me.cmdLightsRename)
        Me.tabLights.Controls.Add(Me.cstLights)
        Me.tabLights.Controls.Add(Me.cmdLightsRemove)
        Me.tabLights.Controls.Add(Me.cmdLightsAdd)
        Me.tabLights.Location = New System.Drawing.Point(4, 112)
        Me.tabLights.Name = "tabLights"
        Me.tabLights.Padding = New System.Windows.Forms.Padding(3)
        Me.tabLights.Size = New System.Drawing.Size(288, 375)
        Me.tabLights.TabIndex = 15
        Me.tabLights.Text = "Level Lights"
        Me.tabLights.UseVisualStyleBackColor = True
        '
        'cboLightAtt
        '
        Me.cboLightAtt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLightAtt.FormattingEnabled = True
        Me.cboLightAtt.Items.AddRange(New Object() {"None", "Linear", "Quadratic"})
        Me.cboLightAtt.Location = New System.Drawing.Point(96, 480)
        Me.cboLightAtt.Name = "cboLightAtt"
        Me.cboLightAtt.Size = New System.Drawing.Size(168, 21)
        Me.cboLightAtt.TabIndex = 86
        '
        'Label81
        '
        Me.Label81.Location = New System.Drawing.Point(8, 480)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(88, 23)
        Me.Label81.TabIndex = 85
        Me.Label81.Text = "Attenuation:"
        Me.Label81.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLightAttDist
        '
        Me.txtLightAttDist.Location = New System.Drawing.Point(96, 512)
        Me.txtLightAttDist.Name = "txtLightAttDist"
        Me.txtLightAttDist.Size = New System.Drawing.Size(144, 20)
        Me.txtLightAttDist.TabIndex = 84
        '
        'Label80
        '
        Me.Label80.Location = New System.Drawing.Point(8, 512)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(88, 20)
        Me.Label80.TabIndex = 83
        Me.Label80.Text = "Att. Distance:"
        Me.Label80.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLightSB
        '
        Me.txtLightSB.Location = New System.Drawing.Point(96, 448)
        Me.txtLightSB.Name = "txtLightSB"
        Me.txtLightSB.Size = New System.Drawing.Size(144, 20)
        Me.txtLightSB.TabIndex = 82
        '
        'Label77
        '
        Me.Label77.Location = New System.Drawing.Point(8, 448)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(88, 20)
        Me.Label77.TabIndex = 81
        Me.Label77.Text = "Specular Blue:"
        Me.Label77.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLightSG
        '
        Me.txtLightSG.Location = New System.Drawing.Point(96, 424)
        Me.txtLightSG.Name = "txtLightSG"
        Me.txtLightSG.Size = New System.Drawing.Size(144, 20)
        Me.txtLightSG.TabIndex = 80
        '
        'Label78
        '
        Me.Label78.Location = New System.Drawing.Point(8, 424)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(88, 20)
        Me.Label78.TabIndex = 79
        Me.Label78.Text = "Specular Green:"
        Me.Label78.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLightSR
        '
        Me.txtLightSR.Location = New System.Drawing.Point(96, 400)
        Me.txtLightSR.Name = "txtLightSR"
        Me.txtLightSR.Size = New System.Drawing.Size(144, 20)
        Me.txtLightSR.TabIndex = 78
        '
        'Label79
        '
        Me.Label79.Location = New System.Drawing.Point(8, 400)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(88, 20)
        Me.Label79.TabIndex = 77
        Me.Label79.Text = "Specular Red:"
        Me.Label79.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLightCB
        '
        Me.txtLightCB.Location = New System.Drawing.Point(96, 368)
        Me.txtLightCB.Name = "txtLightCB"
        Me.txtLightCB.Size = New System.Drawing.Size(144, 20)
        Me.txtLightCB.TabIndex = 76
        '
        'Label74
        '
        Me.Label74.Location = New System.Drawing.Point(8, 368)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(88, 20)
        Me.Label74.TabIndex = 75
        Me.Label74.Text = "Colour Blue:"
        Me.Label74.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLightCG
        '
        Me.txtLightCG.Location = New System.Drawing.Point(96, 344)
        Me.txtLightCG.Name = "txtLightCG"
        Me.txtLightCG.Size = New System.Drawing.Size(144, 20)
        Me.txtLightCG.TabIndex = 74
        '
        'Label75
        '
        Me.Label75.Location = New System.Drawing.Point(8, 344)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(88, 20)
        Me.Label75.TabIndex = 73
        Me.Label75.Text = "Colour Green:"
        Me.Label75.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLightCR
        '
        Me.txtLightCR.Location = New System.Drawing.Point(96, 320)
        Me.txtLightCR.Name = "txtLightCR"
        Me.txtLightCR.Size = New System.Drawing.Size(144, 20)
        Me.txtLightCR.TabIndex = 72
        '
        'Label76
        '
        Me.Label76.Location = New System.Drawing.Point(8, 320)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(88, 20)
        Me.Label76.TabIndex = 71
        Me.Label76.Text = "Colour Red:"
        Me.Label76.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLightTZ
        '
        Me.txtLightTZ.Location = New System.Drawing.Point(96, 288)
        Me.txtLightTZ.Name = "txtLightTZ"
        Me.txtLightTZ.Size = New System.Drawing.Size(144, 20)
        Me.txtLightTZ.TabIndex = 70
        '
        'lblLightTZ
        '
        Me.lblLightTZ.Location = New System.Drawing.Point(8, 288)
        Me.lblLightTZ.Name = "lblLightTZ"
        Me.lblLightTZ.Size = New System.Drawing.Size(88, 20)
        Me.lblLightTZ.TabIndex = 69
        Me.lblLightTZ.Text = "Transform Z:"
        Me.lblLightTZ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLightTY
        '
        Me.txtLightTY.Location = New System.Drawing.Point(96, 264)
        Me.txtLightTY.Name = "txtLightTY"
        Me.txtLightTY.Size = New System.Drawing.Size(144, 20)
        Me.txtLightTY.TabIndex = 68
        '
        'lblLightTY
        '
        Me.lblLightTY.Location = New System.Drawing.Point(8, 264)
        Me.lblLightTY.Name = "lblLightTY"
        Me.lblLightTY.Size = New System.Drawing.Size(88, 20)
        Me.lblLightTY.TabIndex = 67
        Me.lblLightTY.Text = "Transform Y:"
        Me.lblLightTY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLightTX
        '
        Me.txtLightTX.Location = New System.Drawing.Point(96, 240)
        Me.txtLightTX.Name = "txtLightTX"
        Me.txtLightTX.Size = New System.Drawing.Size(144, 20)
        Me.txtLightTX.TabIndex = 66
        '
        'lblLightTX
        '
        Me.lblLightTX.Location = New System.Drawing.Point(8, 240)
        Me.lblLightTX.Name = "lblLightTX"
        Me.lblLightTX.Size = New System.Drawing.Size(88, 20)
        Me.lblLightTX.TabIndex = 65
        Me.lblLightTX.Text = "Transform X:"
        Me.lblLightTX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboLightType
        '
        Me.cboLightType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLightType.FormattingEnabled = True
        Me.cboLightType.Items.AddRange(New Object() {"Ambient Light", "Point Light", "Directional Light"})
        Me.cboLightType.Location = New System.Drawing.Point(96, 208)
        Me.cboLightType.Name = "cboLightType"
        Me.cboLightType.Size = New System.Drawing.Size(168, 21)
        Me.cboLightType.TabIndex = 35
        '
        'Label70
        '
        Me.Label70.Location = New System.Drawing.Point(8, 208)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(88, 23)
        Me.Label70.TabIndex = 34
        Me.Label70.Text = "Type:"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdLightsRename
        '
        Me.cmdLightsRename.Location = New System.Drawing.Point(184, 176)
        Me.cmdLightsRename.Name = "cmdLightsRename"
        Me.cmdLightsRename.Size = New System.Drawing.Size(80, 24)
        Me.cmdLightsRename.TabIndex = 33
        Me.cmdLightsRename.Text = "Rename"
        Me.cmdLightsRename.UseVisualStyleBackColor = True
        '
        'cstLights
        '
        Me.cstLights.FormattingEnabled = True
        Me.cstLights.Location = New System.Drawing.Point(8, 8)
        Me.cstLights.Name = "cstLights"
        Me.cstLights.Size = New System.Drawing.Size(256, 154)
        Me.cstLights.TabIndex = 32
        Me.cstLights.ThreeDCheckBoxes = True
        '
        'cmdLightsRemove
        '
        Me.cmdLightsRemove.Location = New System.Drawing.Point(96, 176)
        Me.cmdLightsRemove.Name = "cmdLightsRemove"
        Me.cmdLightsRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdLightsRemove.TabIndex = 31
        Me.cmdLightsRemove.Text = "Remove"
        Me.cmdLightsRemove.UseVisualStyleBackColor = True
        '
        'cmdLightsAdd
        '
        Me.cmdLightsAdd.Location = New System.Drawing.Point(8, 176)
        Me.cmdLightsAdd.Name = "cmdLightsAdd"
        Me.cmdLightsAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdLightsAdd.TabIndex = 30
        Me.cmdLightsAdd.Text = "Add"
        Me.cmdLightsAdd.UseVisualStyleBackColor = True
        '
        'tabStarFields
        '
        Me.tabStarFields.Controls.Add(Me.cmdStarFieldsRemoveStar)
        Me.tabStarFields.Controls.Add(Me.cmdStarFieldsAddStar)
        Me.tabStarFields.Controls.Add(Me.dgvStarfields)
        Me.tabStarFields.Controls.Add(Me.cstStarFields)
        Me.tabStarFields.Controls.Add(Me.cmdStarFieldsExport)
        Me.tabStarFields.Controls.Add(Me.cmdStarFieldsImport)
        Me.tabStarFields.Controls.Add(Me.cmdStarFieldsRemove)
        Me.tabStarFields.Controls.Add(Me.cmdStarFieldsAdd)
        Me.tabStarFields.Location = New System.Drawing.Point(4, 112)
        Me.tabStarFields.Name = "tabStarFields"
        Me.tabStarFields.Padding = New System.Windows.Forms.Padding(3)
        Me.tabStarFields.Size = New System.Drawing.Size(288, 375)
        Me.tabStarFields.TabIndex = 16
        Me.tabStarFields.Text = "Star Fields"
        Me.tabStarFields.UseVisualStyleBackColor = True
        '
        'cmdStarFieldsRemoveStar
        '
        Me.cmdStarFieldsRemoveStar.Location = New System.Drawing.Point(96, 208)
        Me.cmdStarFieldsRemoveStar.Name = "cmdStarFieldsRemoveStar"
        Me.cmdStarFieldsRemoveStar.Size = New System.Drawing.Size(80, 24)
        Me.cmdStarFieldsRemoveStar.TabIndex = 36
        Me.cmdStarFieldsRemoveStar.Text = "Remove Star"
        Me.cmdStarFieldsRemoveStar.UseVisualStyleBackColor = True
        '
        'cmdStarFieldsAddStar
        '
        Me.cmdStarFieldsAddStar.Location = New System.Drawing.Point(8, 208)
        Me.cmdStarFieldsAddStar.Name = "cmdStarFieldsAddStar"
        Me.cmdStarFieldsAddStar.Size = New System.Drawing.Size(80, 24)
        Me.cmdStarFieldsAddStar.TabIndex = 35
        Me.cmdStarFieldsAddStar.Text = "Add Star"
        Me.cmdStarFieldsAddStar.UseVisualStyleBackColor = True
        '
        'dgvStarfields
        '
        Me.dgvStarfields.AllowUserToAddRows = False
        Me.dgvStarfields.AllowUserToDeleteRows = False
        Me.dgvStarfields.AllowUserToResizeColumns = False
        Me.dgvStarfields.AllowUserToResizeRows = False
        Me.dgvStarfields.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvStarfields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvStarfields.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PositionX, Me.PositionY, Me.PositionZ, Me.StarSize, Me.ColourR, Me.ColourG, Me.ColourB})
        Me.dgvStarfields.Location = New System.Drawing.Point(8, 240)
        Me.dgvStarfields.Name = "dgvStarfields"
        Me.dgvStarfields.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvStarfields.Size = New System.Drawing.Size(256, 149)
        Me.dgvStarfields.TabIndex = 34
        Me.dgvStarfields.VirtualMode = True
        '
        'PositionX
        '
        Me.PositionX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.PositionX.HeaderText = "Position X"
        Me.PositionX.Name = "PositionX"
        Me.PositionX.Width = 79
        '
        'PositionY
        '
        Me.PositionY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.PositionY.HeaderText = "Position Y"
        Me.PositionY.Name = "PositionY"
        Me.PositionY.Width = 79
        '
        'PositionZ
        '
        Me.PositionZ.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.PositionZ.HeaderText = "Position Z"
        Me.PositionZ.Name = "PositionZ"
        Me.PositionZ.Width = 79
        '
        'StarSize
        '
        Me.StarSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.StarSize.HeaderText = "Size"
        Me.StarSize.Name = "StarSize"
        Me.StarSize.Width = 52
        '
        'ColourR
        '
        Me.ColourR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.ColourR.HeaderText = "Col R"
        Me.ColourR.Name = "ColourR"
        Me.ColourR.Width = 58
        '
        'ColourG
        '
        Me.ColourG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.ColourG.HeaderText = "Col G"
        Me.ColourG.Name = "ColourG"
        Me.ColourG.Width = 58
        '
        'ColourB
        '
        Me.ColourB.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.ColourB.HeaderText = "Col B"
        Me.ColourB.Name = "ColourB"
        Me.ColourB.Width = 57
        '
        'cstStarFields
        '
        Me.cstStarFields.FormattingEnabled = True
        Me.cstStarFields.Location = New System.Drawing.Point(8, 8)
        Me.cstStarFields.Name = "cstStarFields"
        Me.cstStarFields.Size = New System.Drawing.Size(256, 154)
        Me.cstStarFields.TabIndex = 33
        Me.cstStarFields.ThreeDCheckBoxes = True
        '
        'cmdStarFieldsExport
        '
        Me.cmdStarFieldsExport.Location = New System.Drawing.Point(184, 208)
        Me.cmdStarFieldsExport.Name = "cmdStarFieldsExport"
        Me.cmdStarFieldsExport.Size = New System.Drawing.Size(80, 24)
        Me.cmdStarFieldsExport.TabIndex = 32
        Me.cmdStarFieldsExport.Text = "Export"
        Me.cmdStarFieldsExport.UseVisualStyleBackColor = True
        '
        'cmdStarFieldsImport
        '
        Me.cmdStarFieldsImport.Location = New System.Drawing.Point(184, 178)
        Me.cmdStarFieldsImport.Name = "cmdStarFieldsImport"
        Me.cmdStarFieldsImport.Size = New System.Drawing.Size(80, 24)
        Me.cmdStarFieldsImport.TabIndex = 31
        Me.cmdStarFieldsImport.Text = "Import"
        Me.cmdStarFieldsImport.UseVisualStyleBackColor = True
        '
        'cmdStarFieldsRemove
        '
        Me.cmdStarFieldsRemove.Location = New System.Drawing.Point(96, 176)
        Me.cmdStarFieldsRemove.Name = "cmdStarFieldsRemove"
        Me.cmdStarFieldsRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdStarFieldsRemove.TabIndex = 30
        Me.cmdStarFieldsRemove.Text = "Remove"
        Me.cmdStarFieldsRemove.UseVisualStyleBackColor = True
        '
        'cmdStarFieldsAdd
        '
        Me.cmdStarFieldsAdd.Location = New System.Drawing.Point(8, 176)
        Me.cmdStarFieldsAdd.Name = "cmdStarFieldsAdd"
        Me.cmdStarFieldsAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdStarFieldsAdd.TabIndex = 29
        Me.cmdStarFieldsAdd.Text = "Add"
        Me.cmdStarFieldsAdd.UseVisualStyleBackColor = True
        '
        'tabStarFieldsT
        '
        Me.tabStarFieldsT.Controls.Add(Me.txtStarFieldsTStarName)
        Me.tabStarFieldsT.Controls.Add(Me.Label71)
        Me.tabStarFieldsT.Controls.Add(Me.cmdStarFieldsTRemoveStar)
        Me.tabStarFieldsT.Controls.Add(Me.cmdStarFieldsTAddStar)
        Me.tabStarFieldsT.Controls.Add(Me.dgvStarFieldsT)
        Me.tabStarFieldsT.Controls.Add(Me.cstStarFieldsT)
        Me.tabStarFieldsT.Controls.Add(Me.cmdStarFieldsTExport)
        Me.tabStarFieldsT.Controls.Add(Me.cmdStarFieldsTImport)
        Me.tabStarFieldsT.Controls.Add(Me.cmdStarFieldsTRemove)
        Me.tabStarFieldsT.Controls.Add(Me.cmdStarFieldsTAdd)
        Me.tabStarFieldsT.Location = New System.Drawing.Point(4, 112)
        Me.tabStarFieldsT.Name = "tabStarFieldsT"
        Me.tabStarFieldsT.Padding = New System.Windows.Forms.Padding(3)
        Me.tabStarFieldsT.Size = New System.Drawing.Size(288, 375)
        Me.tabStarFieldsT.TabIndex = 17
        Me.tabStarFieldsT.Text = "Textured Star Fields"
        Me.tabStarFieldsT.UseVisualStyleBackColor = True
        '
        'txtStarFieldsTStarName
        '
        Me.txtStarFieldsTStarName.Location = New System.Drawing.Point(96, 240)
        Me.txtStarFieldsTStarName.Name = "txtStarFieldsTStarName"
        Me.txtStarFieldsTStarName.Size = New System.Drawing.Size(168, 20)
        Me.txtStarFieldsTStarName.TabIndex = 68
        '
        'Label71
        '
        Me.Label71.Location = New System.Drawing.Point(8, 240)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(88, 23)
        Me.Label71.TabIndex = 67
        Me.Label71.Text = "Texture Name:"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdStarFieldsTRemoveStar
        '
        Me.cmdStarFieldsTRemoveStar.Location = New System.Drawing.Point(96, 208)
        Me.cmdStarFieldsTRemoveStar.Name = "cmdStarFieldsTRemoveStar"
        Me.cmdStarFieldsTRemoveStar.Size = New System.Drawing.Size(80, 24)
        Me.cmdStarFieldsTRemoveStar.TabIndex = 44
        Me.cmdStarFieldsTRemoveStar.Text = "Remove Star"
        Me.cmdStarFieldsTRemoveStar.UseVisualStyleBackColor = True
        '
        'cmdStarFieldsTAddStar
        '
        Me.cmdStarFieldsTAddStar.Location = New System.Drawing.Point(8, 208)
        Me.cmdStarFieldsTAddStar.Name = "cmdStarFieldsTAddStar"
        Me.cmdStarFieldsTAddStar.Size = New System.Drawing.Size(80, 24)
        Me.cmdStarFieldsTAddStar.TabIndex = 43
        Me.cmdStarFieldsTAddStar.Text = "Add Star"
        Me.cmdStarFieldsTAddStar.UseVisualStyleBackColor = True
        '
        'dgvStarFieldsT
        '
        Me.dgvStarFieldsT.AllowUserToAddRows = False
        Me.dgvStarFieldsT.AllowUserToDeleteRows = False
        Me.dgvStarFieldsT.AllowUserToResizeColumns = False
        Me.dgvStarFieldsT.AllowUserToResizeRows = False
        Me.dgvStarFieldsT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvStarFieldsT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvStarFieldsT.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.ColourA})
        Me.dgvStarFieldsT.Location = New System.Drawing.Point(8, 272)
        Me.dgvStarFieldsT.Name = "dgvStarFieldsT"
        Me.dgvStarFieldsT.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvStarFieldsT.Size = New System.Drawing.Size(256, 117)
        Me.dgvStarFieldsT.TabIndex = 42
        Me.dgvStarFieldsT.VirtualMode = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridViewTextBoxColumn1.HeaderText = "Position X"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 79
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridViewTextBoxColumn2.HeaderText = "Position Y"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 79
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridViewTextBoxColumn3.HeaderText = "Position Z"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 79
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridViewTextBoxColumn4.HeaderText = "Size"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 52
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridViewTextBoxColumn5.HeaderText = "Col R"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 58
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridViewTextBoxColumn6.HeaderText = "Col G"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.Width = 58
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.DataGridViewTextBoxColumn7.HeaderText = "Col B"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.Width = 57
        '
        'ColourA
        '
        Me.ColourA.HeaderText = "Col A"
        Me.ColourA.Name = "ColourA"
        '
        'cstStarFieldsT
        '
        Me.cstStarFieldsT.FormattingEnabled = True
        Me.cstStarFieldsT.Location = New System.Drawing.Point(8, 8)
        Me.cstStarFieldsT.Name = "cstStarFieldsT"
        Me.cstStarFieldsT.Size = New System.Drawing.Size(256, 154)
        Me.cstStarFieldsT.TabIndex = 41
        Me.cstStarFieldsT.ThreeDCheckBoxes = True
        '
        'cmdStarFieldsTExport
        '
        Me.cmdStarFieldsTExport.Location = New System.Drawing.Point(184, 208)
        Me.cmdStarFieldsTExport.Name = "cmdStarFieldsTExport"
        Me.cmdStarFieldsTExport.Size = New System.Drawing.Size(80, 24)
        Me.cmdStarFieldsTExport.TabIndex = 40
        Me.cmdStarFieldsTExport.Text = "Export"
        Me.cmdStarFieldsTExport.UseVisualStyleBackColor = True
        '
        'cmdStarFieldsTImport
        '
        Me.cmdStarFieldsTImport.Location = New System.Drawing.Point(184, 178)
        Me.cmdStarFieldsTImport.Name = "cmdStarFieldsTImport"
        Me.cmdStarFieldsTImport.Size = New System.Drawing.Size(80, 24)
        Me.cmdStarFieldsTImport.TabIndex = 39
        Me.cmdStarFieldsTImport.Text = "Import"
        Me.cmdStarFieldsTImport.UseVisualStyleBackColor = True
        '
        'cmdStarFieldsTRemove
        '
        Me.cmdStarFieldsTRemove.Location = New System.Drawing.Point(96, 176)
        Me.cmdStarFieldsTRemove.Name = "cmdStarFieldsTRemove"
        Me.cmdStarFieldsTRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdStarFieldsTRemove.TabIndex = 38
        Me.cmdStarFieldsTRemove.Text = "Remove"
        Me.cmdStarFieldsTRemove.UseVisualStyleBackColor = True
        '
        'cmdStarFieldsTAdd
        '
        Me.cmdStarFieldsTAdd.Location = New System.Drawing.Point(8, 176)
        Me.cmdStarFieldsTAdd.Name = "cmdStarFieldsTAdd"
        Me.cmdStarFieldsTAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdStarFieldsTAdd.TabIndex = 37
        Me.cmdStarFieldsTAdd.Text = "Add"
        Me.cmdStarFieldsTAdd.UseVisualStyleBackColor = True
        '
        'tabAnimations
        '
        Me.tabAnimations.AutoScroll = True
        Me.tabAnimations.Controls.Add(Me.txtAnimationsST)
        Me.tabAnimations.Controls.Add(Me.fraAnimationsJoints)
        Me.tabAnimations.Controls.Add(Me.Label87)
        Me.tabAnimations.Controls.Add(Me.txtAnimationsLET)
        Me.tabAnimations.Controls.Add(Me.Label88)
        Me.tabAnimations.Controls.Add(Me.txtAnimationsLST)
        Me.tabAnimations.Controls.Add(Me.Label89)
        Me.tabAnimations.Controls.Add(Me.txtAnimationsET)
        Me.tabAnimations.Controls.Add(Me.Label90)
        Me.tabAnimations.Controls.Add(Me.cmdAnimationsRename)
        Me.tabAnimations.Controls.Add(Me.cmdAnimationsRemove)
        Me.tabAnimations.Controls.Add(Me.cmdAnimationsAdd)
        Me.tabAnimations.Controls.Add(Me.lstAnimations)
        Me.tabAnimations.Location = New System.Drawing.Point(4, 112)
        Me.tabAnimations.Name = "tabAnimations"
        Me.tabAnimations.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAnimations.Size = New System.Drawing.Size(288, 375)
        Me.tabAnimations.TabIndex = 18
        Me.tabAnimations.Text = "Animations"
        Me.tabAnimations.UseVisualStyleBackColor = True
        '
        'txtAnimationsST
        '
        Me.txtAnimationsST.Location = New System.Drawing.Point(88, 212)
        Me.txtAnimationsST.Name = "txtAnimationsST"
        Me.txtAnimationsST.Size = New System.Drawing.Size(144, 20)
        Me.txtAnimationsST.TabIndex = 94
        '
        'fraAnimationsJoints
        '
        Me.fraAnimationsJoints.Controls.Add(Me.txtAnimationsJointsTime)
        Me.fraAnimationsJoints.Controls.Add(Me.Label86)
        Me.fraAnimationsJoints.Controls.Add(Me.txtAnimationsJointsRZ)
        Me.fraAnimationsJoints.Controls.Add(Me.Label72)
        Me.fraAnimationsJoints.Controls.Add(Me.txtAnimationsJointsRY)
        Me.fraAnimationsJoints.Controls.Add(Me.Label73)
        Me.fraAnimationsJoints.Controls.Add(Me.txtAnimationsJointsRX)
        Me.fraAnimationsJoints.Controls.Add(Me.Label82)
        Me.fraAnimationsJoints.Controls.Add(Me.txtAnimationsJointsPZ)
        Me.fraAnimationsJoints.Controls.Add(Me.Label83)
        Me.fraAnimationsJoints.Controls.Add(Me.txtAnimationsJointsPY)
        Me.fraAnimationsJoints.Controls.Add(Me.Label84)
        Me.fraAnimationsJoints.Controls.Add(Me.txtAnimationsJointsPX)
        Me.fraAnimationsJoints.Controls.Add(Me.Label85)
        Me.fraAnimationsJoints.Controls.Add(Me.cmdAnimationsPlay)
        Me.fraAnimationsJoints.Controls.Add(Me.sldAnimationsTime)
        Me.fraAnimationsJoints.Controls.Add(Me.cmdAnimationsJointsRemoveKeyframe)
        Me.fraAnimationsJoints.Controls.Add(Me.cmdAnimationsJointsAddKeyframe)
        Me.fraAnimationsJoints.Controls.Add(Me.cmdAnimationsJointsRemove)
        Me.fraAnimationsJoints.Controls.Add(Me.cmdAnimationsJointsAdd)
        Me.fraAnimationsJoints.Controls.Add(Me.lstAnimationsJoints)
        Me.fraAnimationsJoints.Location = New System.Drawing.Point(8, 312)
        Me.fraAnimationsJoints.Name = "fraAnimationsJoints"
        Me.fraAnimationsJoints.Size = New System.Drawing.Size(256, 416)
        Me.fraAnimationsJoints.TabIndex = 33
        Me.fraAnimationsJoints.TabStop = False
        Me.fraAnimationsJoints.Text = "Animated joints"
        '
        'txtAnimationsJointsTime
        '
        Me.txtAnimationsJointsTime.Location = New System.Drawing.Point(80, 224)
        Me.txtAnimationsJointsTime.Name = "txtAnimationsJointsTime"
        Me.txtAnimationsJointsTime.Size = New System.Drawing.Size(144, 20)
        Me.txtAnimationsJointsTime.TabIndex = 86
        '
        'Label86
        '
        Me.Label86.Location = New System.Drawing.Point(8, 224)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(72, 20)
        Me.Label86.TabIndex = 84
        Me.Label86.Text = "Time:"
        Me.Label86.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAnimationsJointsRZ
        '
        Me.txtAnimationsJointsRZ.Location = New System.Drawing.Point(80, 384)
        Me.txtAnimationsJointsRZ.Name = "txtAnimationsJointsRZ"
        Me.txtAnimationsJointsRZ.Size = New System.Drawing.Size(144, 20)
        Me.txtAnimationsJointsRZ.TabIndex = 95
        '
        'Label72
        '
        Me.Label72.Location = New System.Drawing.Point(8, 384)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(72, 20)
        Me.Label72.TabIndex = 97
        Me.Label72.Text = "Rotation Z:"
        Me.Label72.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAnimationsJointsRY
        '
        Me.txtAnimationsJointsRY.Location = New System.Drawing.Point(80, 360)
        Me.txtAnimationsJointsRY.Name = "txtAnimationsJointsRY"
        Me.txtAnimationsJointsRY.Size = New System.Drawing.Size(144, 20)
        Me.txtAnimationsJointsRY.TabIndex = 93
        '
        'Label73
        '
        Me.Label73.Location = New System.Drawing.Point(8, 360)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(72, 20)
        Me.Label73.TabIndex = 96
        Me.Label73.Text = "Rotation Y:"
        Me.Label73.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAnimationsJointsRX
        '
        Me.txtAnimationsJointsRX.Location = New System.Drawing.Point(80, 336)
        Me.txtAnimationsJointsRX.Name = "txtAnimationsJointsRX"
        Me.txtAnimationsJointsRX.Size = New System.Drawing.Size(144, 20)
        Me.txtAnimationsJointsRX.TabIndex = 92
        '
        'Label82
        '
        Me.Label82.Location = New System.Drawing.Point(8, 336)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(72, 20)
        Me.Label82.TabIndex = 94
        Me.Label82.Text = "Rotation X:"
        Me.Label82.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAnimationsJointsPZ
        '
        Me.txtAnimationsJointsPZ.Location = New System.Drawing.Point(80, 304)
        Me.txtAnimationsJointsPZ.Name = "txtAnimationsJointsPZ"
        Me.txtAnimationsJointsPZ.Size = New System.Drawing.Size(144, 20)
        Me.txtAnimationsJointsPZ.TabIndex = 91
        '
        'Label83
        '
        Me.Label83.Location = New System.Drawing.Point(8, 304)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(72, 20)
        Me.Label83.TabIndex = 90
        Me.Label83.Text = "Position Z:"
        Me.Label83.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAnimationsJointsPY
        '
        Me.txtAnimationsJointsPY.Location = New System.Drawing.Point(80, 280)
        Me.txtAnimationsJointsPY.Name = "txtAnimationsJointsPY"
        Me.txtAnimationsJointsPY.Size = New System.Drawing.Size(144, 20)
        Me.txtAnimationsJointsPY.TabIndex = 89
        '
        'Label84
        '
        Me.Label84.Location = New System.Drawing.Point(8, 280)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(72, 20)
        Me.Label84.TabIndex = 88
        Me.Label84.Text = "Position Y:"
        Me.Label84.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAnimationsJointsPX
        '
        Me.txtAnimationsJointsPX.Location = New System.Drawing.Point(80, 256)
        Me.txtAnimationsJointsPX.Name = "txtAnimationsJointsPX"
        Me.txtAnimationsJointsPX.Size = New System.Drawing.Size(144, 20)
        Me.txtAnimationsJointsPX.TabIndex = 87
        '
        'Label85
        '
        Me.Label85.Location = New System.Drawing.Point(8, 256)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(72, 20)
        Me.Label85.TabIndex = 85
        Me.Label85.Text = "Position X:"
        Me.Label85.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdAnimationsPlay
        '
        Me.cmdAnimationsPlay.Location = New System.Drawing.Point(88, 104)
        Me.cmdAnimationsPlay.Name = "cmdAnimationsPlay"
        Me.cmdAnimationsPlay.Size = New System.Drawing.Size(80, 24)
        Me.cmdAnimationsPlay.TabIndex = 83
        Me.cmdAnimationsPlay.Text = "Play"
        Me.cmdAnimationsPlay.UseVisualStyleBackColor = True
        '
        'sldAnimationsTime
        '
        Me.sldAnimationsTime.LargeChange = 1
        Me.sldAnimationsTime.Location = New System.Drawing.Point(8, 136)
        Me.sldAnimationsTime.Maximum = 0
        Me.sldAnimationsTime.Name = "sldAnimationsTime"
        Me.sldAnimationsTime.Size = New System.Drawing.Size(240, 45)
        Me.sldAnimationsTime.TabIndex = 36
        Me.sldAnimationsTime.TickStyle = System.Windows.Forms.TickStyle.Both
        '
        'cmdAnimationsJointsRemoveKeyframe
        '
        Me.cmdAnimationsJointsRemoveKeyframe.Location = New System.Drawing.Point(136, 192)
        Me.cmdAnimationsJointsRemoveKeyframe.Name = "cmdAnimationsJointsRemoveKeyframe"
        Me.cmdAnimationsJointsRemoveKeyframe.Size = New System.Drawing.Size(112, 24)
        Me.cmdAnimationsJointsRemoveKeyframe.TabIndex = 35
        Me.cmdAnimationsJointsRemoveKeyframe.Text = "Remove Keyframe"
        Me.cmdAnimationsJointsRemoveKeyframe.UseVisualStyleBackColor = True
        '
        'cmdAnimationsJointsAddKeyframe
        '
        Me.cmdAnimationsJointsAddKeyframe.Location = New System.Drawing.Point(8, 192)
        Me.cmdAnimationsJointsAddKeyframe.Name = "cmdAnimationsJointsAddKeyframe"
        Me.cmdAnimationsJointsAddKeyframe.Size = New System.Drawing.Size(112, 24)
        Me.cmdAnimationsJointsAddKeyframe.TabIndex = 34
        Me.cmdAnimationsJointsAddKeyframe.Text = "Add Keyframe"
        Me.cmdAnimationsJointsAddKeyframe.UseVisualStyleBackColor = True
        '
        'cmdAnimationsJointsRemove
        '
        Me.cmdAnimationsJointsRemove.Location = New System.Drawing.Point(176, 104)
        Me.cmdAnimationsJointsRemove.Name = "cmdAnimationsJointsRemove"
        Me.cmdAnimationsJointsRemove.Size = New System.Drawing.Size(72, 24)
        Me.cmdAnimationsJointsRemove.TabIndex = 33
        Me.cmdAnimationsJointsRemove.Text = "Remove"
        Me.cmdAnimationsJointsRemove.UseVisualStyleBackColor = True
        '
        'cmdAnimationsJointsAdd
        '
        Me.cmdAnimationsJointsAdd.Location = New System.Drawing.Point(8, 104)
        Me.cmdAnimationsJointsAdd.Name = "cmdAnimationsJointsAdd"
        Me.cmdAnimationsJointsAdd.Size = New System.Drawing.Size(72, 24)
        Me.cmdAnimationsJointsAdd.TabIndex = 32
        Me.cmdAnimationsJointsAdd.Text = "Add"
        Me.cmdAnimationsJointsAdd.UseVisualStyleBackColor = True
        '
        'lstAnimationsJoints
        '
        Me.lstAnimationsJoints.FormattingEnabled = True
        Me.lstAnimationsJoints.Location = New System.Drawing.Point(8, 24)
        Me.lstAnimationsJoints.Name = "lstAnimationsJoints"
        Me.lstAnimationsJoints.Size = New System.Drawing.Size(240, 69)
        Me.lstAnimationsJoints.TabIndex = 0
        '
        'Label87
        '
        Me.Label87.Location = New System.Drawing.Point(16, 212)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(72, 20)
        Me.Label87.TabIndex = 92
        Me.Label87.Text = "Start Time:"
        Me.Label87.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAnimationsLET
        '
        Me.txtAnimationsLET.Location = New System.Drawing.Point(88, 284)
        Me.txtAnimationsLET.Name = "txtAnimationsLET"
        Me.txtAnimationsLET.Size = New System.Drawing.Size(144, 20)
        Me.txtAnimationsLET.TabIndex = 99
        '
        'Label88
        '
        Me.Label88.Location = New System.Drawing.Point(16, 284)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(72, 20)
        Me.Label88.TabIndex = 98
        Me.Label88.Text = "Loop End:"
        Me.Label88.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAnimationsLST
        '
        Me.txtAnimationsLST.Location = New System.Drawing.Point(88, 260)
        Me.txtAnimationsLST.Name = "txtAnimationsLST"
        Me.txtAnimationsLST.Size = New System.Drawing.Size(144, 20)
        Me.txtAnimationsLST.TabIndex = 97
        '
        'Label89
        '
        Me.Label89.Location = New System.Drawing.Point(16, 260)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(72, 20)
        Me.Label89.TabIndex = 96
        Me.Label89.Text = "Loop Start:"
        Me.Label89.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAnimationsET
        '
        Me.txtAnimationsET.Location = New System.Drawing.Point(88, 236)
        Me.txtAnimationsET.Name = "txtAnimationsET"
        Me.txtAnimationsET.Size = New System.Drawing.Size(144, 20)
        Me.txtAnimationsET.TabIndex = 95
        '
        'Label90
        '
        Me.Label90.Location = New System.Drawing.Point(16, 236)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(72, 20)
        Me.Label90.TabIndex = 93
        Me.Label90.Text = "End Time:"
        Me.Label90.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdAnimationsRename
        '
        Me.cmdAnimationsRename.Location = New System.Drawing.Point(184, 176)
        Me.cmdAnimationsRename.Name = "cmdAnimationsRename"
        Me.cmdAnimationsRename.Size = New System.Drawing.Size(80, 24)
        Me.cmdAnimationsRename.TabIndex = 32
        Me.cmdAnimationsRename.Text = "Rename"
        Me.cmdAnimationsRename.UseVisualStyleBackColor = True
        '
        'cmdAnimationsRemove
        '
        Me.cmdAnimationsRemove.Location = New System.Drawing.Point(96, 176)
        Me.cmdAnimationsRemove.Name = "cmdAnimationsRemove"
        Me.cmdAnimationsRemove.Size = New System.Drawing.Size(80, 24)
        Me.cmdAnimationsRemove.TabIndex = 31
        Me.cmdAnimationsRemove.Text = "Remove"
        Me.cmdAnimationsRemove.UseVisualStyleBackColor = True
        '
        'cmdAnimationsAdd
        '
        Me.cmdAnimationsAdd.Location = New System.Drawing.Point(8, 176)
        Me.cmdAnimationsAdd.Name = "cmdAnimationsAdd"
        Me.cmdAnimationsAdd.Size = New System.Drawing.Size(80, 24)
        Me.cmdAnimationsAdd.TabIndex = 30
        Me.cmdAnimationsAdd.Text = "Add"
        Me.cmdAnimationsAdd.UseVisualStyleBackColor = True
        '
        'lstAnimations
        '
        Me.lstAnimations.FormattingEnabled = True
        Me.lstAnimations.Location = New System.Drawing.Point(8, 8)
        Me.lstAnimations.Name = "lstAnimations"
        Me.lstAnimations.Size = New System.Drawing.Size(256, 160)
        Me.lstAnimations.TabIndex = 0
        '
        'pnlDisplay
        '
        Me.pnlDisplay.Controls.Add(Me.pbxDisplay)
        Me.pnlDisplay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDisplay.Location = New System.Drawing.Point(0, 0)
        Me.pnlDisplay.Name = "pnlDisplay"
        Me.pnlDisplay.Size = New System.Drawing.Size(476, 483)
        Me.pnlDisplay.TabIndex = 1
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.sbrDummy, Me.sbrLabel})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 540)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(784, 22)
        Me.StatusStrip.TabIndex = 1
        '
        'sbrDummy
        '
        Me.sbrDummy.Name = "sbrDummy"
        Me.sbrDummy.Size = New System.Drawing.Size(58, 17)
        Me.sbrDummy.Text = "Filename:"
        '
        'sbrLabel
        '
        Me.sbrLabel.Name = "sbrLabel"
        Me.sbrLabel.Size = New System.Drawing.Size(48, 17)
        Me.sbrLabel.Text = "untitled"
        '
        'splMain
        '
        Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.splMain.IsSplitterFixed = True
        Me.splMain.Location = New System.Drawing.Point(0, 49)
        Me.splMain.Name = "splMain"
        '
        'splMain.Panel1
        '
        Me.splMain.Panel1.Controls.Add(Me.tabMain)
        '
        'splMain.Panel2
        '
        Me.splMain.Panel2.Controls.Add(Me.pnlDisplay)
        Me.splMain.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 8, 8)
        Me.splMain.Size = New System.Drawing.Size(784, 491)
        Me.splMain.SplitterDistance = 296
        Me.splMain.TabIndex = 6
        Me.splMain.TabStop = False
        '
        'HODEditorA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 562)
        Me.Controls.Add(Me.splMain)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.ToolStrip)
        Me.Controls.Add(Me.MenuStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip
        Me.MinimumSize = New System.Drawing.Size(640, 480)
        Me.Name = "HODEditorA"
        Me.Text = "Cold Fusion HOD Remastered Editor"
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.pbxDisplay_cms.ResumeLayout(False)
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMain.ResumeLayout(False)
        Me.tabTextures.ResumeLayout(False)
        Me.tabMaterials.ResumeLayout(False)
        Me.fraMaterialParameters.ResumeLayout(False)
        Me.fraMaterialParameters.PerformLayout()
        Me.tabMultiMeshes.ResumeLayout(False)
        Me.fraShipMeshes.ResumeLayout(False)
        Me.tabGoblins.ResumeLayout(False)
        Me.tabBGMS.ResumeLayout(False)
        Me.tabUI.ResumeLayout(False)
        Me.tabJoints.ResumeLayout(False)
        Me.fraJoint.ResumeLayout(False)
        Me.fraJoint.PerformLayout()
        Me.tvwJoints_cms.ResumeLayout(False)
        Me.tabCM.ResumeLayout(False)
        Me.fraCMBSPH.ResumeLayout(False)
        Me.fraCMBSPH.PerformLayout()
        Me.fraCMBBOX.ResumeLayout(False)
        Me.fraCMBBOX.PerformLayout()
        Me.tabEngineShapes.ResumeLayout(False)
        Me.tabEngineGlows.ResumeLayout(False)
        Me.tabEngineGlows.PerformLayout()
        Me.fraEngineGlowsMisc.ResumeLayout(False)
        Me.fraEngineGlowsMisc.PerformLayout()
        CType(Me.sldEngineGlowsThrusterPowerFactor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabEngineBurns.ResumeLayout(False)
        Me.tabEngineBurns.PerformLayout()
        Me.tabNavLights.ResumeLayout(False)
        Me.tabNavLights.PerformLayout()
        CType(Me.pbxNavLightsSample, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabMarkers.ResumeLayout(False)
        Me.fraMarkersTransform.ResumeLayout(False)
        Me.fraMarkersTransform.PerformLayout()
        Me.tabDockpaths.ResumeLayout(False)
        Me.tabDockpaths.PerformLayout()
        Me.fraDockpathsKeyframes.ResumeLayout(False)
        Me.fraDockpathsKeyframes.PerformLayout()
        CType(Me.sldDockpathsKeyframe, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabLights.ResumeLayout(False)
        Me.tabLights.PerformLayout()
        Me.tabStarFields.ResumeLayout(False)
        CType(Me.dgvStarfields, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabStarFieldsT.ResumeLayout(False)
        Me.tabStarFieldsT.PerformLayout()
        CType(Me.dgvStarFieldsT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabAnimations.ResumeLayout(False)
        Me.tabAnimations.PerformLayout()
        Me.fraAnimationsJoints.ResumeLayout(False)
        Me.fraAnimationsJoints.PerformLayout()
        CType(Me.sldAnimationsTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDisplay.ResumeLayout(False)
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.splMain.Panel1.ResumeLayout(False)
        Me.splMain.Panel2.ResumeLayout(False)
        Me.splMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
 Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuFileNew As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuFileOpen As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuFileSepr1 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents mnuFileSave As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuFileSaveAs As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuFileSepr2 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents mnuFileExit As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuEdit As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuEditCut As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuEditCopy As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuEditPaste As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuTools As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuToolsOptions As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuHelpAbout As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
 Friend WithEvents tspNew As System.Windows.Forms.ToolStripButton
 Friend WithEvents tspOpen As System.Windows.Forms.ToolStripButton
 Friend WithEvents tspSave As System.Windows.Forms.ToolStripButton
 Friend WithEvents tspSeperator1 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents tspCut As System.Windows.Forms.ToolStripButton
 Friend WithEvents tspCopy As System.Windows.Forms.ToolStripButton
 Friend WithEvents tspPaste As System.Windows.Forms.ToolStripButton
 Friend WithEvents OpenHODFileDialog As System.Windows.Forms.OpenFileDialog
 Friend WithEvents SaveHODFileDialog As System.Windows.Forms.SaveFileDialog
 Friend WithEvents OpenTextureFileDialog As System.Windows.Forms.OpenFileDialog
 Friend WithEvents SaveTextureFileDialog As System.Windows.Forms.SaveFileDialog
 Friend WithEvents OpenShaderFileDialog As System.Windows.Forms.OpenFileDialog
 Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
 Friend WithEvents FolderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
 Friend WithEvents OpenOBJFileDialog As System.Windows.Forms.OpenFileDialog
 Friend WithEvents SaveOBJFileDialog As System.Windows.Forms.SaveFileDialog
 Friend WithEvents pbxDisplay_cmsReset As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents pbxDisplay_cmsLights As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents pbxDisplay_cms As System.Windows.Forms.ContextMenuStrip
 Friend WithEvents tabMain As System.Windows.Forms.TabControl
 Friend WithEvents tabTextures As System.Windows.Forms.TabPage
 Friend WithEvents cmdTexturesExportAll As System.Windows.Forms.Button
 Friend WithEvents lblTextureMipCount As System.Windows.Forms.Label
 Friend WithEvents Label4 As System.Windows.Forms.Label
 Friend WithEvents lblTextureDimensions As System.Windows.Forms.Label
 Friend WithEvents lblTextureFormat As System.Windows.Forms.Label
 Friend WithEvents Label1 As System.Windows.Forms.Label
 Friend WithEvents lblTexturePath As System.Windows.Forms.Label
 Friend WithEvents Label3 As System.Windows.Forms.Label
 Friend WithEvents Label2 As System.Windows.Forms.Label
 Friend WithEvents cmdTexturePreview As System.Windows.Forms.Button
 Friend WithEvents cmdTextureExport As System.Windows.Forms.Button
 Friend WithEvents cmdTextureRemove As System.Windows.Forms.Button
 Friend WithEvents cmdTextureImport As System.Windows.Forms.Button
 Friend WithEvents cmdTextureAdd As System.Windows.Forms.Button
 Friend WithEvents lstTextures As System.Windows.Forms.ListBox
 Friend WithEvents tabMaterials As System.Windows.Forms.TabPage
 Friend WithEvents cmdMaterialShaderRename As System.Windows.Forms.Button
 Friend WithEvents lstMaterials As System.Windows.Forms.ListBox
 Friend WithEvents fraMaterialParameters As System.Windows.Forms.GroupBox
 Friend WithEvents optMaterialParameterColour As System.Windows.Forms.RadioButton
 Friend WithEvents optMaterialParameterTexture As System.Windows.Forms.RadioButton
 Friend WithEvents Label12 As System.Windows.Forms.Label
 Friend WithEvents cmdMaterialNoTexture As System.Windows.Forms.Button
 Friend WithEvents cboMaterialTextureIndex As System.Windows.Forms.ComboBox
 Friend WithEvents txtMaterialColourA As System.Windows.Forms.TextBox
 Friend WithEvents txtMaterialColourB As System.Windows.Forms.TextBox
 Friend WithEvents txtMaterialColourG As System.Windows.Forms.TextBox
 Friend WithEvents txtMaterialColourR As System.Windows.Forms.TextBox
 Friend WithEvents Label11 As System.Windows.Forms.Label
 Friend WithEvents Label10 As System.Windows.Forms.Label
 Friend WithEvents Label9 As System.Windows.Forms.Label
 Friend WithEvents Label8 As System.Windows.Forms.Label
 Friend WithEvents Label7 As System.Windows.Forms.Label
 Friend WithEvents cmdMaterialParameterRename As System.Windows.Forms.Button
 Friend WithEvents cmdMaterialParameterRemove As System.Windows.Forms.Button
 Friend WithEvents cmdMaterialParameterAdd As System.Windows.Forms.Button
 Friend WithEvents lstMaterialParameters As System.Windows.Forms.ListBox
 Friend WithEvents cmdMaterialParametersFromFile As System.Windows.Forms.Button
 Friend WithEvents lblMaterialShaderName As System.Windows.Forms.Label
 Friend WithEvents lblMaterialName As System.Windows.Forms.Label
 Friend WithEvents Label6 As System.Windows.Forms.Label
 Friend WithEvents Label5 As System.Windows.Forms.Label
 Friend WithEvents cmdMaterialRename As System.Windows.Forms.Button
 Friend WithEvents cmdMaterialRemove As System.Windows.Forms.Button
 Friend WithEvents cmdMaterialAdd As System.Windows.Forms.Button
 Friend WithEvents tabMultiMeshes As System.Windows.Forms.TabPage
 Friend WithEvents lstShipMeshes As System.Windows.Forms.ListBox
 Friend WithEvents cboShipMeshesParent As System.Windows.Forms.ComboBox
 Friend WithEvents Label13 As System.Windows.Forms.Label
 Friend WithEvents cmdShipMeshesRename As System.Windows.Forms.Button
 Friend WithEvents fraShipMeshes As System.Windows.Forms.GroupBox
 Friend WithEvents cstShipMeshesLODs As System.Windows.Forms.CheckedListBox
 Friend WithEvents cmdShipMeshesLODReMaterial As System.Windows.Forms.Button
 Friend WithEvents cmdShipMeshesLODExport As System.Windows.Forms.Button
 Friend WithEvents cmdShipMeshesLODImport As System.Windows.Forms.Button
 Friend WithEvents cmdShipMeshesLODTransform As System.Windows.Forms.Button
 Friend WithEvents cmdShipMeshesLODRemove As System.Windows.Forms.Button
 Friend WithEvents cmdShipMeshesLODAdd As System.Windows.Forms.Button
 Friend WithEvents cmdShipMeshesRemove As System.Windows.Forms.Button
 Friend WithEvents cmdShipMeshesAdd As System.Windows.Forms.Button
 Friend WithEvents tabGoblins As System.Windows.Forms.TabPage
 Friend WithEvents cboGoblinsParent As System.Windows.Forms.ComboBox
 Friend WithEvents Label14 As System.Windows.Forms.Label
 Friend WithEvents cmdGoblinsRename As System.Windows.Forms.Button
 Friend WithEvents cstGoblins As System.Windows.Forms.CheckedListBox
 Friend WithEvents cmdGoblinsRematerial As System.Windows.Forms.Button
 Friend WithEvents cmdGoblinsExport As System.Windows.Forms.Button
 Friend WithEvents cmdGoblinsImport As System.Windows.Forms.Button
 Friend WithEvents cmdGoblinsTransform As System.Windows.Forms.Button
 Friend WithEvents cmdGoblinsRemove As System.Windows.Forms.Button
 Friend WithEvents cmdGoblinsAdd As System.Windows.Forms.Button
 Friend WithEvents tabBGMS As System.Windows.Forms.TabPage
 Friend WithEvents tabUI As System.Windows.Forms.TabPage
 Friend WithEvents cmdUIMeshesRename As System.Windows.Forms.Button
 Friend WithEvents cstUIMeshes As System.Windows.Forms.CheckedListBox
 Friend WithEvents cmdUIMeshesExport As System.Windows.Forms.Button
 Friend WithEvents cmdUIMeshesImport As System.Windows.Forms.Button
 Friend WithEvents cmdUIMeshesRemove As System.Windows.Forms.Button
 Friend WithEvents cmdUIMeshesAdd As System.Windows.Forms.Button
 Friend WithEvents pbxDisplay As System.Windows.Forms.PictureBox
 Friend WithEvents cstBackgroundMeshes As System.Windows.Forms.CheckedListBox
 Friend WithEvents cmdBackgroundMeshesExport As System.Windows.Forms.Button
 Friend WithEvents cmdBackgroundMeshesImport As System.Windows.Forms.Button
 Friend WithEvents cmdBackgroundMeshesRemove As System.Windows.Forms.Button
 Friend WithEvents cmdBackgroundMeshesAdd As System.Windows.Forms.Button
 Friend WithEvents cmdBackgroundMeshesRecolourize As System.Windows.Forms.Button
 Friend WithEvents cmdBackgroundMeshesGenerateTexture As System.Windows.Forms.Button
 Friend WithEvents cmdBackgroundMeshesRecolourizeAll As System.Windows.Forms.Button
 Friend WithEvents cmdShipMeshesLODRenormal As System.Windows.Forms.Button
 Friend WithEvents cmdGoblinsRenormal As System.Windows.Forms.Button
 Friend WithEvents cmdUIMeshesRenormal As System.Windows.Forms.Button
 Friend WithEvents tabJoints As System.Windows.Forms.TabPage
 Friend WithEvents tvwJoints As System.Windows.Forms.TreeView
 Friend WithEvents cmdJointsAddTemplate As System.Windows.Forms.Button
 Friend WithEvents cmdJointsRemove As System.Windows.Forms.Button
 Friend WithEvents cmdJointsAdd As System.Windows.Forms.Button
 Friend WithEvents fraJoint As System.Windows.Forms.GroupBox
 Friend WithEvents chkJointsDegreeOfFreedomZ As System.Windows.Forms.CheckBox
 Friend WithEvents chkJointsDegreeOfFreedomY As System.Windows.Forms.CheckBox
 Friend WithEvents chkJointsDegreeOfFreedomX As System.Windows.Forms.CheckBox
 Friend WithEvents txtJointsAxisZ As System.Windows.Forms.TextBox
 Friend WithEvents Label24 As System.Windows.Forms.Label
 Friend WithEvents txtJointsAxisY As System.Windows.Forms.TextBox
 Friend WithEvents Label25 As System.Windows.Forms.Label
 Friend WithEvents txtJointsAxisX As System.Windows.Forms.TextBox
 Friend WithEvents Label26 As System.Windows.Forms.Label
 Friend WithEvents txtJointsScaleZ As System.Windows.Forms.TextBox
 Friend WithEvents Label21 As System.Windows.Forms.Label
 Friend WithEvents txtJointsScaleY As System.Windows.Forms.TextBox
 Friend WithEvents Label22 As System.Windows.Forms.Label
 Friend WithEvents txtJointsScaleX As System.Windows.Forms.TextBox
 Friend WithEvents Label23 As System.Windows.Forms.Label
 Friend WithEvents txtJointsRotationZ As System.Windows.Forms.TextBox
 Friend WithEvents Label18 As System.Windows.Forms.Label
 Friend WithEvents txtJointsRotationY As System.Windows.Forms.TextBox
 Friend WithEvents Label19 As System.Windows.Forms.Label
 Friend WithEvents txtJointsRotationX As System.Windows.Forms.TextBox
 Friend WithEvents Label20 As System.Windows.Forms.Label
 Friend WithEvents txtJointsPositionZ As System.Windows.Forms.TextBox
 Friend WithEvents Label17 As System.Windows.Forms.Label
 Friend WithEvents txtJointsPositionY As System.Windows.Forms.TextBox
 Friend WithEvents Label16 As System.Windows.Forms.Label
 Friend WithEvents txtJointsPositionX As System.Windows.Forms.TextBox
 Friend WithEvents Label15 As System.Windows.Forms.Label
 Friend WithEvents tvwJoints_cms As System.Windows.Forms.ContextMenuStrip
 Friend WithEvents tvwJoints_cmsRename As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents tvwJoints_cmsCollapse As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents tvwJoints_cmsExpand As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents tvwJoints_cmsMoveUp As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents tvwJoints_cmsMoveDown As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents tvwJoints_cmsSeperator3 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents chkJointsRender As System.Windows.Forms.CheckBox
 Friend WithEvents tvwJoints_cmsSeperator1 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents tvwJoints_cmsHide As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents tvwJoints_cmsShow As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents tvwJoints_cmsSeperator2 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents pbxDisplay_cmsSeparator1 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents pbxDisplay_cmsWireframe As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents pbxDisplay_cmsSolid As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents pbxDisplay_cmsSeparator2 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents pnlDisplay As System.Windows.Forms.Panel
 Friend WithEvents tabCM As System.Windows.Forms.TabPage
 Friend WithEvents cboCMName As System.Windows.Forms.ComboBox
 Friend WithEvents Label27 As System.Windows.Forms.Label
 Friend WithEvents cmdCMExport As System.Windows.Forms.Button
 Friend WithEvents cmdCMImport As System.Windows.Forms.Button
 Friend WithEvents cmdCMRemove As System.Windows.Forms.Button
 Friend WithEvents cmdCMAdd As System.Windows.Forms.Button
 Friend WithEvents cstCM As System.Windows.Forms.CheckedListBox
 Friend WithEvents tabEngineShapes As System.Windows.Forms.TabPage
 Friend WithEvents tabEngineGlows As System.Windows.Forms.TabPage
 Friend WithEvents tabEngineBurns As System.Windows.Forms.TabPage
 Friend WithEvents cboEngineShapesParent As System.Windows.Forms.ComboBox
 Friend WithEvents Label28 As System.Windows.Forms.Label
 Friend WithEvents cmdEngineShapesRename As System.Windows.Forms.Button
 Friend WithEvents cstEngineShapes As System.Windows.Forms.CheckedListBox
 Friend WithEvents cmdEngineShapesExport As System.Windows.Forms.Button
 Friend WithEvents cmdEngineShapesImport As System.Windows.Forms.Button
 Friend WithEvents cmdEngineShapesRemove As System.Windows.Forms.Button
 Friend WithEvents cmdEngineShapesAdd As System.Windows.Forms.Button
 Friend WithEvents cboEngineGlowsParent As System.Windows.Forms.ComboBox
 Friend WithEvents Label29 As System.Windows.Forms.Label
 Friend WithEvents cmdEngineGlowsRename As System.Windows.Forms.Button
 Friend WithEvents cstEngineGlows As System.Windows.Forms.CheckedListBox
 Friend WithEvents cmdEngineGlowsExport As System.Windows.Forms.Button
 Friend WithEvents cmdEngineGlowsImport As System.Windows.Forms.Button
 Friend WithEvents cmdEngineGlowsRemove As System.Windows.Forms.Button
 Friend WithEvents cmdEngineGlowsAdd As System.Windows.Forms.Button
 Friend WithEvents txtEngineBurn5Z As System.Windows.Forms.TextBox
 Friend WithEvents txtEngineBurn5Y As System.Windows.Forms.TextBox
 Friend WithEvents txtEngineBurn5X As System.Windows.Forms.TextBox
 Friend WithEvents txtEngineBurn4Z As System.Windows.Forms.TextBox
 Friend WithEvents txtEngineBurn4Y As System.Windows.Forms.TextBox
 Friend WithEvents txtEngineBurn4X As System.Windows.Forms.TextBox
 Friend WithEvents txtEngineBurn3Z As System.Windows.Forms.TextBox
 Friend WithEvents txtEngineBurn3Y As System.Windows.Forms.TextBox
 Friend WithEvents txtEngineBurn3X As System.Windows.Forms.TextBox
 Friend WithEvents txtEngineBurn2Z As System.Windows.Forms.TextBox
 Friend WithEvents txtEngineBurn2Y As System.Windows.Forms.TextBox
 Friend WithEvents txtEngineBurn2X As System.Windows.Forms.TextBox
 Friend WithEvents txtEngineBurn1Z As System.Windows.Forms.TextBox
 Friend WithEvents txtEngineBurn1Y As System.Windows.Forms.TextBox
 Friend WithEvents txtEngineBurn1X As System.Windows.Forms.TextBox
 Friend WithEvents Label39 As System.Windows.Forms.Label
 Friend WithEvents Label38 As System.Windows.Forms.Label
 Friend WithEvents Label37 As System.Windows.Forms.Label
 Friend WithEvents Label36 As System.Windows.Forms.Label
 Friend WithEvents Label35 As System.Windows.Forms.Label
 Friend WithEvents Label33 As System.Windows.Forms.Label
 Friend WithEvents Label34 As System.Windows.Forms.Label
 Friend WithEvents Label32 As System.Windows.Forms.Label
 Friend WithEvents Label31 As System.Windows.Forms.Label
 Friend WithEvents cboEngineBurnsParent As System.Windows.Forms.ComboBox
 Friend WithEvents Label30 As System.Windows.Forms.Label
 Friend WithEvents cmdEngineBurnsRename As System.Windows.Forms.Button
 Friend WithEvents cstEngineBurns As System.Windows.Forms.CheckedListBox
 Friend WithEvents cmdEngineBurnsRemove As System.Windows.Forms.Button
 Friend WithEvents cmdEngineBurnsAdd As System.Windows.Forms.Button
 Friend WithEvents txtEngineGlowsLOD As System.Windows.Forms.TextBox
 Friend WithEvents Label40 As System.Windows.Forms.Label
 Friend WithEvents fraEngineGlowsMisc As System.Windows.Forms.GroupBox
 Friend WithEvents sldEngineGlowsThrusterPowerFactor As System.Windows.Forms.TrackBar
 Friend WithEvents Label41 As System.Windows.Forms.Label
 Friend WithEvents pbxDisplay_cmsEditLight As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents tabNavLights As System.Windows.Forms.TabPage
 Friend WithEvents cboNavLightsName As System.Windows.Forms.ComboBox
 Friend WithEvents Label42 As System.Windows.Forms.Label
 Friend WithEvents cstNavLights As System.Windows.Forms.CheckedListBox
 Friend WithEvents cmdNavLightsRemove As System.Windows.Forms.Button
 Friend WithEvents cmdNavLightsAdd As System.Windows.Forms.Button
 Friend WithEvents pbxNavLightsSample As System.Windows.Forms.PictureBox
 Friend WithEvents chkNavLightsHighEnd As System.Windows.Forms.CheckBox
 Friend WithEvents chkNavLightsVisibleSprite As System.Windows.Forms.CheckBox
 Friend WithEvents txtNavlightsColourB As System.Windows.Forms.TextBox
 Friend WithEvents txtNavlightsColourG As System.Windows.Forms.TextBox
 Friend WithEvents txtNavlightsColourR As System.Windows.Forms.TextBox
 Friend WithEvents txtNavLightsDistance As System.Windows.Forms.TextBox
 Friend WithEvents txtNavLightsStyle As System.Windows.Forms.TextBox
 Friend WithEvents txtNavLightsFrequency As System.Windows.Forms.TextBox
 Friend WithEvents txtNavLightsPhase As System.Windows.Forms.TextBox
 Friend WithEvents txtNavLightsSize As System.Windows.Forms.TextBox
 Friend WithEvents txtNavLightsSection As System.Windows.Forms.TextBox
 Friend WithEvents Label50 As System.Windows.Forms.Label
 Friend WithEvents Label51 As System.Windows.Forms.Label
 Friend WithEvents Label52 As System.Windows.Forms.Label
 Friend WithEvents Label101 As System.Windows.Forms.Label
 Friend WithEvents Label47 As System.Windows.Forms.Label
 Friend WithEvents Label46 As System.Windows.Forms.Label
 Friend WithEvents Label45 As System.Windows.Forms.Label
 Friend WithEvents Label44 As System.Windows.Forms.Label
 Friend WithEvents Label43 As System.Windows.Forms.Label
 Friend WithEvents ColorDialog As System.Windows.Forms.ColorDialog
 Friend WithEvents tabMarkers As System.Windows.Forms.TabPage
 Friend WithEvents fraMarkersTransform As System.Windows.Forms.GroupBox
 Friend WithEvents txtMarkerRotationZ As System.Windows.Forms.TextBox
 Friend WithEvents Label58 As System.Windows.Forms.Label
 Friend WithEvents txtMarkerRotationY As System.Windows.Forms.TextBox
 Friend WithEvents Label59 As System.Windows.Forms.Label
 Friend WithEvents txtMarkerRotationX As System.Windows.Forms.TextBox
 Friend WithEvents Label60 As System.Windows.Forms.Label
 Friend WithEvents txtMarkerPositionZ As System.Windows.Forms.TextBox
 Friend WithEvents Label61 As System.Windows.Forms.Label
 Friend WithEvents txtMarkerPositionY As System.Windows.Forms.TextBox
 Friend WithEvents Label62 As System.Windows.Forms.Label
 Friend WithEvents txtMarkerPositionX As System.Windows.Forms.TextBox
 Friend WithEvents Label63 As System.Windows.Forms.Label
 Friend WithEvents cboMarkersParent As System.Windows.Forms.ComboBox
 Friend WithEvents Label48 As System.Windows.Forms.Label
 Friend WithEvents cmdMarkersRename As System.Windows.Forms.Button
 Friend WithEvents cstMarkers As System.Windows.Forms.CheckedListBox
 Friend WithEvents cmdMarkersRemove As System.Windows.Forms.Button
 Friend WithEvents cmdMarkersAdd As System.Windows.Forms.Button
 Friend WithEvents pbxDisplay_cmsWireframeOverlay As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents pbxDisplay_cmsSeparator3 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents tabDockpaths As System.Windows.Forms.TabPage
 Friend WithEvents tabLights As System.Windows.Forms.TabPage
 Friend WithEvents sldDockpathsKeyframe As System.Windows.Forms.TrackBar
 Friend WithEvents txtDockpathsParentName As System.Windows.Forms.TextBox
 Friend WithEvents Label49 As System.Windows.Forms.Label
 Friend WithEvents cmdDockpathsRename As System.Windows.Forms.Button
 Friend WithEvents cstDockpaths As System.Windows.Forms.CheckedListBox
 Friend WithEvents cmdDockpathsRemove As System.Windows.Forms.Button
 Friend WithEvents cmdDockpathsAdd As System.Windows.Forms.Button
 Friend WithEvents fraDockpathsKeyframes As System.Windows.Forms.GroupBox
 Friend WithEvents chkDockpathsKeyframeClearReservation As System.Windows.Forms.CheckBox
 Friend WithEvents chkDockpathsKeyframeUseClipPlane As System.Windows.Forms.CheckBox
 Friend WithEvents chkDockpathsKeyframeQueueOrigin As System.Windows.Forms.CheckBox
 Friend WithEvents chkDockpathsKeyframePlayerInControl As System.Windows.Forms.CheckBox
 Friend WithEvents chkDockpathsKeyframeForceCloseBehaviour As System.Windows.Forms.CheckBox
 Friend WithEvents chkDockpathsKeyframeCheckRotation As System.Windows.Forms.CheckBox
 Friend WithEvents chkDockpathsKeyframeDropFocus As System.Windows.Forms.CheckBox
 Friend WithEvents chkDockpathsKeyframeUseRotation As System.Windows.Forms.CheckBox
 Friend WithEvents txtDockpathsKeyframeMaxSpeed As System.Windows.Forms.TextBox
 Friend WithEvents Label65 As System.Windows.Forms.Label
 Friend WithEvents txtDockpathsKeyframeTolerance As System.Windows.Forms.TextBox
 Friend WithEvents Label66 As System.Windows.Forms.Label
 Friend WithEvents txtDockpathsKeyframeRotationZ As System.Windows.Forms.TextBox
 Friend WithEvents Label53 As System.Windows.Forms.Label
 Friend WithEvents txtDockpathsKeyframeRotationY As System.Windows.Forms.TextBox
 Friend WithEvents Label54 As System.Windows.Forms.Label
 Friend WithEvents txtDockpathsKeyframeRotationX As System.Windows.Forms.TextBox
 Friend WithEvents Label55 As System.Windows.Forms.Label
 Friend WithEvents txtDockpathsKeyframePositionZ As System.Windows.Forms.TextBox
 Friend WithEvents Label56 As System.Windows.Forms.Label
 Friend WithEvents txtDockpathsKeyframePositionY As System.Windows.Forms.TextBox
 Friend WithEvents Label57 As System.Windows.Forms.Label
 Friend WithEvents txtDockpathsKeyframePositionX As System.Windows.Forms.TextBox
 Friend WithEvents Label64 As System.Windows.Forms.Label
 Friend WithEvents cmdDockpathsKeyframesInsert As System.Windows.Forms.Button
 Friend WithEvents cmdDockpathsKeyframesAdd As System.Windows.Forms.Button
 Friend WithEvents cmdDockpathsKeyframesRemove As System.Windows.Forms.Button
 Friend WithEvents chkDockpathsUseAnim As System.Windows.Forms.CheckBox
 Friend WithEvents chkDockpathsLatchPath As System.Windows.Forms.CheckBox
 Friend WithEvents chkDockpathsExitPath As System.Windows.Forms.CheckBox
 Friend WithEvents txtDockpathsLinkedPaths As System.Windows.Forms.TextBox
 Friend WithEvents Label67 As System.Windows.Forms.Label
 Friend WithEvents txtDockpathsDockFamilies As System.Windows.Forms.TextBox
 Friend WithEvents Label68 As System.Windows.Forms.Label
 Friend WithEvents txtDockpathsGlobalTolerance As System.Windows.Forms.TextBox
 Friend WithEvents Label69 As System.Windows.Forms.Label
 Friend WithEvents tabStarFields As System.Windows.Forms.TabPage
 Friend WithEvents cboLightAtt As System.Windows.Forms.ComboBox
 Friend WithEvents Label81 As System.Windows.Forms.Label
 Friend WithEvents txtLightAttDist As System.Windows.Forms.TextBox
 Friend WithEvents Label80 As System.Windows.Forms.Label
 Friend WithEvents txtLightSB As System.Windows.Forms.TextBox
 Friend WithEvents Label77 As System.Windows.Forms.Label
 Friend WithEvents txtLightSG As System.Windows.Forms.TextBox
 Friend WithEvents Label78 As System.Windows.Forms.Label
 Friend WithEvents txtLightSR As System.Windows.Forms.TextBox
 Friend WithEvents Label79 As System.Windows.Forms.Label
 Friend WithEvents txtLightCB As System.Windows.Forms.TextBox
 Friend WithEvents Label74 As System.Windows.Forms.Label
 Friend WithEvents txtLightCG As System.Windows.Forms.TextBox
 Friend WithEvents Label75 As System.Windows.Forms.Label
 Friend WithEvents txtLightCR As System.Windows.Forms.TextBox
 Friend WithEvents Label76 As System.Windows.Forms.Label
 Friend WithEvents txtLightTZ As System.Windows.Forms.TextBox
 Friend WithEvents lblLightTZ As System.Windows.Forms.Label
 Friend WithEvents txtLightTY As System.Windows.Forms.TextBox
 Friend WithEvents lblLightTY As System.Windows.Forms.Label
 Friend WithEvents txtLightTX As System.Windows.Forms.TextBox
 Friend WithEvents lblLightTX As System.Windows.Forms.Label
 Friend WithEvents cboLightType As System.Windows.Forms.ComboBox
 Friend WithEvents Label70 As System.Windows.Forms.Label
 Friend WithEvents cmdLightsRename As System.Windows.Forms.Button
 Friend WithEvents cstLights As System.Windows.Forms.CheckedListBox
 Friend WithEvents cmdLightsRemove As System.Windows.Forms.Button
 Friend WithEvents cmdLightsAdd As System.Windows.Forms.Button
 Friend WithEvents cmdShipMeshesRetangent As System.Windows.Forms.Button
 Friend WithEvents cmdShipMeshesRenormal As System.Windows.Forms.Button
 Friend WithEvents cmdShipMeshesLODRetangent As System.Windows.Forms.Button
 Friend WithEvents cmdGoblinsRetangent As System.Windows.Forms.Button
 Friend WithEvents mnuToolsSeperator1 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents mnuToolsRenormalMeshes As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuToolsRetangentMeshes As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents cstStarFields As System.Windows.Forms.CheckedListBox
 Friend WithEvents cmdStarFieldsExport As System.Windows.Forms.Button
 Friend WithEvents cmdStarFieldsImport As System.Windows.Forms.Button
 Friend WithEvents cmdStarFieldsRemove As System.Windows.Forms.Button
 Friend WithEvents cmdStarFieldsAdd As System.Windows.Forms.Button
 Friend WithEvents tabStarFieldsT As System.Windows.Forms.TabPage
 Friend WithEvents dgvStarfields As System.Windows.Forms.DataGridView
 Friend WithEvents PositionX As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents PositionY As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents PositionZ As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents StarSize As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents ColourR As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents ColourG As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents ColourB As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents cmdStarFieldsRemoveStar As System.Windows.Forms.Button
 Friend WithEvents cmdStarFieldsAddStar As System.Windows.Forms.Button
 Friend WithEvents txtStarFieldsTStarName As System.Windows.Forms.TextBox
 Friend WithEvents Label71 As System.Windows.Forms.Label
 Friend WithEvents cmdStarFieldsTRemoveStar As System.Windows.Forms.Button
 Friend WithEvents cmdStarFieldsTAddStar As System.Windows.Forms.Button
 Friend WithEvents dgvStarFieldsT As System.Windows.Forms.DataGridView
 Friend WithEvents cstStarFieldsT As System.Windows.Forms.CheckedListBox
 Friend WithEvents cmdStarFieldsTExport As System.Windows.Forms.Button
 Friend WithEvents cmdStarFieldsTImport As System.Windows.Forms.Button
 Friend WithEvents cmdStarFieldsTRemove As System.Windows.Forms.Button
 Friend WithEvents cmdStarFieldsTAdd As System.Windows.Forms.Button
 Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents ColourA As System.Windows.Forms.DataGridViewTextBoxColumn
 Friend WithEvents mnuToolsSeperator2 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents mnuToolsTranslate As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuToolsRotate As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuToolsScale As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuToolsMirror As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents mnuToolsSeperator3 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents mnuToolsVARYToMULT As System.Windows.Forms.ToolStripMenuItem
 Friend WithEvents tabAnimations As System.Windows.Forms.TabPage
 Friend WithEvents fraAnimationsJoints As System.Windows.Forms.GroupBox
 Friend WithEvents lstAnimationsJoints As System.Windows.Forms.ListBox
 Friend WithEvents cmdAnimationsRename As System.Windows.Forms.Button
 Friend WithEvents cmdAnimationsRemove As System.Windows.Forms.Button
 Friend WithEvents cmdAnimationsAdd As System.Windows.Forms.Button
 Friend WithEvents lstAnimations As System.Windows.Forms.ListBox
 Friend WithEvents cmdAnimationsJointsRemove As System.Windows.Forms.Button
 Friend WithEvents cmdAnimationsJointsAdd As System.Windows.Forms.Button
 Friend WithEvents cmdAnimationsJointsRemoveKeyframe As System.Windows.Forms.Button
 Friend WithEvents cmdAnimationsJointsAddKeyframe As System.Windows.Forms.Button
 Friend WithEvents cmdAnimationsPlay As System.Windows.Forms.Button
 Friend WithEvents sldAnimationsTime As System.Windows.Forms.TrackBar
 Friend WithEvents tspSeparator2 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents tspMode As System.Windows.Forms.ToolStripComboBox
 Friend WithEvents tspLabel1 As System.Windows.Forms.ToolStripLabel
 Friend WithEvents txtAnimationsJointsTime As System.Windows.Forms.TextBox
 Friend WithEvents Label86 As System.Windows.Forms.Label
 Friend WithEvents txtAnimationsJointsRZ As System.Windows.Forms.TextBox
 Friend WithEvents Label72 As System.Windows.Forms.Label
 Friend WithEvents txtAnimationsJointsRY As System.Windows.Forms.TextBox
 Friend WithEvents Label73 As System.Windows.Forms.Label
 Friend WithEvents txtAnimationsJointsRX As System.Windows.Forms.TextBox
 Friend WithEvents Label82 As System.Windows.Forms.Label
 Friend WithEvents txtAnimationsJointsPZ As System.Windows.Forms.TextBox
 Friend WithEvents Label83 As System.Windows.Forms.Label
 Friend WithEvents txtAnimationsJointsPY As System.Windows.Forms.TextBox
 Friend WithEvents Label84 As System.Windows.Forms.Label
 Friend WithEvents txtAnimationsJointsPX As System.Windows.Forms.TextBox
 Friend WithEvents Label85 As System.Windows.Forms.Label
 Friend WithEvents txtAnimationsST As System.Windows.Forms.TextBox
 Friend WithEvents Label87 As System.Windows.Forms.Label
 Friend WithEvents txtAnimationsLET As System.Windows.Forms.TextBox
 Friend WithEvents Label88 As System.Windows.Forms.Label
 Friend WithEvents txtAnimationsLST As System.Windows.Forms.TextBox
 Friend WithEvents Label89 As System.Windows.Forms.Label
 Friend WithEvents txtAnimationsET As System.Windows.Forms.TextBox
 Friend WithEvents Label90 As System.Windows.Forms.Label
 Friend WithEvents tspSeperator2 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents tspLabel2 As System.Windows.Forms.ToolStripLabel
 Friend WithEvents tspObjectScale As System.Windows.Forms.ToolStripTextBox
 Friend WithEvents tspSeperator3 As System.Windows.Forms.ToolStripSeparator
 Friend WithEvents tspLabel3 As System.Windows.Forms.ToolStripLabel
 Friend WithEvents tspCameraSensitivity As System.Windows.Forms.ToolStripTextBox
 Friend WithEvents cmdCMRecalc As System.Windows.Forms.Button
 Friend WithEvents fraCMBSPH As System.Windows.Forms.GroupBox
 Friend WithEvents txtCMCX As System.Windows.Forms.TextBox
 Friend WithEvents txtCMRadius As System.Windows.Forms.TextBox
 Friend WithEvents Label94 As System.Windows.Forms.Label
 Friend WithEvents Label91 As System.Windows.Forms.Label
 Friend WithEvents Label93 As System.Windows.Forms.Label
 Friend WithEvents txtCMCZ As System.Windows.Forms.TextBox
 Friend WithEvents txtCMCY As System.Windows.Forms.TextBox
 Friend WithEvents Label92 As System.Windows.Forms.Label
 Friend WithEvents fraCMBBOX As System.Windows.Forms.GroupBox
 Friend WithEvents Label97 As System.Windows.Forms.Label
 Friend WithEvents txtCMMaxZ As System.Windows.Forms.TextBox
 Friend WithEvents txtCMMinX As System.Windows.Forms.TextBox
 Friend WithEvents Label98 As System.Windows.Forms.Label
 Friend WithEvents Label96 As System.Windows.Forms.Label
 Friend WithEvents txtCMMaxY As System.Windows.Forms.TextBox
 Friend WithEvents txtCMMinY As System.Windows.Forms.TextBox
 Friend WithEvents Label99 As System.Windows.Forms.Label
 Friend WithEvents Label95 As System.Windows.Forms.Label
 Friend WithEvents txtCMMaxX As System.Windows.Forms.TextBox
 Friend WithEvents txtCMMinZ As System.Windows.Forms.TextBox
 Friend WithEvents Label100 As System.Windows.Forms.Label
 Friend WithEvents splMain As System.Windows.Forms.SplitContainer
 Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
 Friend WithEvents sbrLabel As System.Windows.Forms.ToolStripStatusLabel
 Friend WithEvents sbrDummy As System.Windows.Forms.ToolStripStatusLabel

End Class
