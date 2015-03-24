Namespace WavefrontObjectHelper
    ''' <summary>
    ''' Data of an indice in wavefront object files.
    ''' </summary>
    ''' <remarks>
    ''' Helper wavefront object structure.
    ''' </remarks>
    Friend Structure IndiceValues
        ''' <summary>Index into position array.</summary>
        Dim Position As Integer

        ''' <summary>Index into normal array.</summary>
        Dim Normal As Integer

        ''' <summary>Index into UV array.</summary>
        Dim UV As Integer

        ''' <summary>Index into colour array.</summary>
        Dim Colour As Integer
    End Structure
End Namespace
