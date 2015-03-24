Partial Class Light
    ''' <summary>
    ''' Type of light.
    ''' </summary>
    Public Enum LightType
        Ambient
        Point
        Directional
    End Enum

    ''' <summary>
    ''' Type of attenuation (the way in which the light intensity decreases as
    ''' one moves away from it).
    ''' </summary>
    Public Enum LightAttenuation
        None
        Linear
        Quadratic
    End Enum
End Class
