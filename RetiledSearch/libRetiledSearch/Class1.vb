Imports System.Runtime.InteropServices

Public Class Class1

    Public Shared Sub BeginSearch(SearchTerm As String)
        If RuntimeInformation.IsOSPlatform(OSPlatform.Windows) Then
            Dim SearchRunner As New ProcessStartInfo
            SearchRunner.FileName = "https://bing.com/search?q=" & SearchTerm
            SearchRunner.UseShellExecute = True

            Process.Start(SearchRunner)

        ElseIf RuntimeInformation.IsOSPlatform(OSPlatform.Linux) Then
            Dim SearchTermFixer As String = "https://bing.com/search?q={0}" & SearchTerm
            Process.Start("xdg-open", "'" & SearchTermFixer & "'")
        End If
    End Sub

    ' //if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    '//    {
    '//    var SearchRunner = new ProcessStartInfo
    '//    {
    '//        FileName = "https://bing.com/search?q=" + SearchTerm,
    '//        UseShellExecute = true
    '//    };

    '//    Process.Start(SearchRunner);
    '//}
    '//else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    '//{
    '//    Process.Start("xdg-open", "'https://bing.com/search?q=" + SearchTerm.Replace(" ", "%20") + "'");
    '//}

End Class
