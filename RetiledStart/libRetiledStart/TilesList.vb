﻿' libRetiledStart - Utility library for RetiledStart written in VB.NET so that
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



Public Class TilesList

End Class


Public Class StartScreenTileEntry
    ' Adding a new class so we can get and store
    ' information for tiles.
    ' Details:
    ' https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/concepts/linq/how-to-create-a-list-of-items

    ' Properties:
    Public Property FileNameProperty As String
    Public Property NameKeyValueProperty As String
    ' Tile width and height are self-explanatory.
    Public Property TileWidth As Integer
    Public Property TileHeight As Integer
    ' For now we'll store tile colors in strings,
    ' but this may be changed eventually if the "Color"
    ' type makes more sense to use. Probably should
    ' look at what properties MahApps.Metro uses
    ' for their tiles.
    Public Property TileColor As String
    ' The text at the bottom of the tile.
    Public Property TileAppNameAreaText As String
    ' Tile image. This isn't used right now as
    ' the code for getting app icons is unimplemented.
    Public Property TileImage As String

    ' Not exactly sure why this is required.
    Public Sub New()

    End Sub

    Public Sub New(ByVal fileName As String,
                   ByVal nameKeyValue As String,
                   ByVal tileWidthValue As Integer,
                   ByVal tileHeightValue As Integer,
                   ByVal tileColorValue As String,
                   ByVal tileAppNameAreaTextValue As String)
        ' Set the properties to be the parameters.
        FileNameProperty = fileName
        NameKeyValueProperty = nameKeyValue

    End Sub

End Class