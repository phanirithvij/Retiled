# Retiled

An attempt at creating a "desktop" environment mainly for Linux phones and tablets that's similar in function to some parts of Microsoft's Windows Phone 8.x, primarily these features:
- Start screen
  - This is just tiles minus the "live" part at the moment because that would be a little complicated, though maybe in the future some people or I could figure out a good way to integrate Python scripts to display "live" data
  - There's also the app list to the right of the tiles, and the search box there along with the letter categorization are both essential to Windows Phone and Windows Phone-like environments
- Search app
  - Something like the old Bing search app from WP8.0 rather than Cortana
    - Would be cool if someone built something similar to Cortana that used an open-source backend, though
  - Currently this is just a textbox and a button that opens Bing with the search term in the default browser
    - Eventually it'll support choosing the default search engine
  - What would be really neat is if it had support for daily images if the user wants to show them, including but not limited to the Bing Image of the Day
    - This would just be the US at the beginning because that's what's easiest for me to test, but eventually it would have the option to use any region that has Bing Image of the Day stuff
    - Not sure if Microsoft would be ok with someone's project having built-in displaying of their images. Probably should have something that either goes to the Bing homepage (or the page of the daily wallpaper source that has image copyright info) or get the image's copyright info from Bing to display it in a popup that shows up when you press the `i` button in the Command Bar. Probably should just have an `i` button that goes to the page with image copyright info.
- Navigation bar
- Action Center
- Status bar

Development is mainly being done using the PinePhone, so that'll be the main supported device. For now it's in the prototype stage as I don't really know what I'm doing with C#, Avalonia, and .NET 5 on Linux.

## Notes

**NB:** You may need to install the `ttf-ms-fonts` package (name may vary by distro) so that the proper fonts are available to Avalonia, otherwise it'll crash.

## License stuff

>This project (Retiled) is Copyright (C) 2021 Drew Naylor and is licensed under the Apache License 2.0.<br>
Windows Phone and all other related copyrights and trademarks are property of Microsoft Corporation. All rights reserved.<br>
Retiled is not associated with Microsoft in any way, and Microsoft does not endorse Retiled.<br>
Any other copyrights and trademarks belong to their respective people and companies/organizations.<br>
Components of the Retiled project include [AvaloniaUI](https://avaloniaui.net/), [.NET 5](https://docs.microsoft.com/en-us/dotnet/core/dotnet-five). Anything else that's used in the future will be added to this list.

## Video demos

- [RetiledStart prototype running on the PinePhone](https://youtu.be/NpUnrb1wC_8)

## Screenshots
Below are some prototype screenshots in case you want to see how things are going so far.

RetiledStart running on the PinePhone:<br>
<img src="/docs/images/retiledstart-running-on-pinephone.png" width="360"><br>
<br>
RetiledStart in a three-column view:<br>
<img src="/docs/images/retiledstart-tiles-that-look-like-wp.png" width="360"><br>
<br>
Search app prototype:<br>
![](/docs/images/search-prototype.png?raw=true)<br>
