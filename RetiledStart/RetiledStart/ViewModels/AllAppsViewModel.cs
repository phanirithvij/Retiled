﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using libdotdesktop_standard;
using ReactiveUI;

namespace RetiledStart.ViewModels
{
    class AllAppsViewModel : ViewModelBase
    {

        //public void RunFirefox()
        //{
        //    // Placeholder code to run Firefox for testing.
        //    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        //    {
        //        libRetiledStart.AppsList.RunApp(@"C:\Program Files\Mozilla Firefox\firefox.exe");
        //    }
        //    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        //    {
        //        libRetiledStart.AppsList.RunApp("/usr/bin/firefox");
        //    }
        //}

        //public void RunAngelfish()
        //{
        //    // Placeholder code to run Angelfish for testing.
        //    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        //    {
        //        libRetiledStart.AppsList.RunApp("/usr/bin/angelfish");
        //    }
        //}

        public void RunApp(string ExecFilename)
        {
            // Send it to the other code.
                Debug.WriteLine(desktopEntryStuff.getInfo(ExecFilename, "Name"));
                libRetiledStart.AppsList.RunApp(desktopEntryStuff.getInfo(ExecFilename, "Exec"));

        }

        public string GetText(string DotDesktopFilename)
        {
            // Get .desktop file text for displaying on the button.
            return desktopEntryStuff.getInfo(DotDesktopFilename, "Name");
        }

        // Couldn't figure out how to do this, so I based
        // this code off this SO answer:
        // https://stackoverflow.com/a/64552332

        private ObservableCollection<string> _GetDotDesktopFiles = new ObservableCollection<string> (libRetiledStart.AppsList.GetDotDesktopFiles());

        public ObservableCollection<string> GetDotDesktopFiles
        {
            // Get the list of .desktop files.
            get => _GetDotDesktopFiles;
            set => this.RaiseAndSetIfChanged(ref _GetDotDesktopFiles, value);
            
        }


    }

    // IMultiValueConverter for the app list.
    // Can't bind with an IValueConverter, so I have
    // to use this.
    // This is from MSDN:
    // https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.imultivalueconverter?view=net-5.0
    public class AppNameConverter : IMultiValueConverter
    {
        public object Convert(IList<object> values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return desktopEntryStuff.getInfo((string)values[0], "Name");
        }

        public object? ConvertBack(IList<object> values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "null";
        }
    }
}
