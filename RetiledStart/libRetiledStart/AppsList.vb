' libRetiledStart - Utility library for RetiledStart written in VB.NET so that
'                   I don't have to write everything in C#.
' Copyright (C) 2021 Drew Naylor
' (Note that the copyright years include the years left out by the hyphen.)
' Windows Phone and all other related copyrights and trademarks are property
' of Microsoft Corporation. All rights reserved.
'
' This file is a part of the Retiled project.
' Neither Retiled nor Drew Naylor are associated with Microsoft
' and Microsoft does not endorse Retiled.
' Any other copyrights and trademarks belong to their
' respective people and companies/organizations.
'
'
'   Licensed under the Apache License, Version 2.0 (the "License");
'   you may not use this file except in compliance with the License.
'   You may obtain a copy of the License at
'
'     http://www.apache.org/licenses/LICENSE-2.0
'
'   Unless required by applicable law or agreed to in writing, software
'   distributed under the License is distributed on an "AS IS" BASIS,
'   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
'   See the License for the specific language governing permissions and
'   limitations under the License.



Imports libdotdesktop_standard
Public Class AppsList

    Public Shared Sub RunApp(ExecFilename As String)
        ' Runs what's passed to it.
        ' Try/Catch so that it doesn't crash if it can't find the program.
        Try
            ' Clean the Exec key and store it in a string so it's easy to access.
            Dim CleanedExecKey As String = desktopEntryStuff.cleanExecKey(ExecFilename)
            Debug.WriteLine(CleanedExecKey)

            If CleanedExecKey.StartsWith("gapplication launch") Then
                ' Some apps such as GNOME Maps uses "gapplication launch" to start apps.
                ' We need to ensure that's what's actually launched, then have
                ' the app be passed to it as an argument.
                ' For some reason, the "launch" isn't being included in the Exec key, so
                ' it needs to be included manually.
                ' Actually, the app we want to run isn't being included, so it's not working.
                Process.Start("gapplication launch", CleanedExecKey.Replace("gapplication launch", String.Empty))
            Else
                ' This app doesn't want to use "gapplication launch".
                Process.Start(CleanedExecKey)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Function GetDotDesktopFiles() As ObjectModel.ObservableCollection(Of String)
        ' Gets all .desktop files in /usr/share/applications
        ' on Linux or my desktop on Windows.

        ' Define a collection of filenames to use.
        Dim DotDesktopFilesList As New List(Of DotDesktopEntryInAllAppsList)
        ' Define a path we'll set later.
        ' We're setting up a fallback, too.
        Dim DotDesktopFilesPath As String = "/usr/share/applications"

        If OperatingSystem.IsLinux = True Then
            DotDesktopFilesPath = "/usr/share/applications"

        ElseIf OperatingSystem.IsWindows = True Then
            DotDesktopFilesPath = "C:\Users\Drew\Desktop"
            'DotDesktopFilesPath = "C:\Users\drewn\Desktop"
        End If

        For Each DotDesktopFile As String In FileIO.FileSystem.GetFiles(DotDesktopFilesPath)
            ' Check if the file ends with .desktop.
            If DotDesktopFile.EndsWith(".desktop") Then

                If Not desktopEntryStuff.getInfo(DotDesktopFile, "NoDisplay") = "true" Then
                    ' Make sure this .desktop file is supposed to be shown.
                    ' Add its name if it's in the file.
                    If desktopEntryStuff.getInfo(DotDesktopFile.ToString, "Name") IsNot Nothing Then
                        DotDesktopFilesList.Add(New DotDesktopEntryInAllAppsList(DotDesktopFile.ToString,
                                                                                 desktopEntryStuff.getInfo(DotDesktopFile.ToString, "Name")))
                    Else
                        ' It's not in the file, so add its filename.
                        DotDesktopFilesList.Add(New DotDesktopEntryInAllAppsList(DotDesktopFile.ToString,
                                                                                 DotDesktopFile.ToString))
                    End If

                End If

            End If
        Next

        ' This is where we actually sort the list.
        ' Stuff here ended up being really useful.
        ' Didn't know list items could have properties.
        ' Maybe one of my other programs that uses a List
        ' could benefit from this.
        ' https://stackoverflow.com/questions/11735902/sort-a-list-of-object-in-vb-net
        ' This answer in particular is what worked I think:
        ' https://stackoverflow.com/a/11736001
        DotDesktopFilesList = DotDesktopFilesList.OrderBy(Function(x) x.NameKeyValueProperty).ToList()

        ' Define a new ObservableCollection that we'll use to copy the file paths into.
        Dim NewDotDesktopFilesList As New ObjectModel.ObservableCollection(Of String)

        ' Add all of the items that are file paths to the new ObservableCollection.
        For Each Item In DotDesktopFilesList
            NewDotDesktopFilesList.Add(Item.FileNameProperty)
        Next

        ' Return the collection.
        Return NewDotDesktopFilesList

    End Function

    Public Shared Function GetDotDesktopNameKey(DotDesktopFile As String) As String
        ' Checks if the .desktop file actually has a "Name" key.
        If desktopEntryStuff.getInfo(DotDesktopFile, "Name") = String.Empty Then
            ' Return the filename.
            ' Preferably this wouldn't include the file path,
            ' but that would require using what dotbakker does
            ' where a file path is passed in and just the filename
            ' without the rest of the path is returned
            ' and it would take a little longer to get working.
            ' Had a bit of trouble figuring this out, but it turned out
            ' that it was returning String.Empty, rather than Nothing.
            Debug.WriteLine(DotDesktopFile)
            Return DotDesktopFile
        Else
            ' Return what's in the "Name" key.
            Debug.WriteLine(desktopEntryStuff.getInfo(DotDesktopFile, "Name"))
            Return desktopEntryStuff.getInfo(DotDesktopFile, "Name")
        End If
    End Function

End Class

Public Class DotDesktopEntryInAllAppsList
    ' Adding a new class so we can add them to a list
    ' so that sorting the list is easy.
    ' Details:
    ' https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/concepts/linq/how-to-create-a-list-of-items

    ' Properties:
    Public Property FileNameProperty As String
    Public Property NameKeyValueProperty As String

    ' Not exactly sure why this is required.
    Public Sub New()

    End Sub

    Public Sub New(ByVal fileName As String,
                   ByVal nameKeyValue As String)
        ' Set the properties to be the parameters.
        FileNameProperty = fileName
        NameKeyValueProperty = nameKeyValue

    End Sub

End Class
