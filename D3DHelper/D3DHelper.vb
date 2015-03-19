''' <summary>
''' Module containing initialization procedure for D3D Helper
''' module.
''' </summary>
Public Module D3DHelper
 ''' <summary>
 ''' Initializes D3D Helper Module.
 ''' </summary>
 Public Sub InitializeD3DHelper()
  ' Load the 'Microsoft.DirectX' assembly.
  Reflection.Assembly.Load(GetType(Microsoft.DirectX.Vector3).Assembly.GetName())

  ' Load the 'Microsoft.DirectX.Direct3D' assembly.
  Reflection.Assembly.Load(GetType(Microsoft.DirectX.Direct3D.Device).Assembly.GetName())

  ' Load the 'Microsoft.DirectX.Direct3DX' assembly.
  Reflection.Assembly.Load(GetType(Microsoft.DirectX.Direct3D.Font).Assembly.GetName())

 End Sub

End Module
