using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WGM_Application;
using Windows.Media.Miracast;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WGM_Application.AppWindows
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            PollingEnabledCheck();
        }

        // Sets polling to true when checked. Just another way to enable polling
        private void Polling_Checked(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["PollingEnabled"] = true;

        }

        // Disables Polling (Only way to do so for now)
        private void Polling_Unchecked(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey("PollingEnabled") == true)
            {
                localSettings.Values["PollingEnabled"] = false;
            }
        }

        //Makes the box checked if polling is enabled
        private void PollingEnabledCheck()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (localSettings.Values.ContainsKey("PollingEnabled"))
            {
                PollingCheckbox.IsChecked = (bool)localSettings.Values["PollingEnabled"]; // Use stored value
                System.Diagnostics.Trace.WriteLine("Value: " + localSettings.Values["PollingEnabled"]);
            }
            System.Diagnostics.Trace.WriteLine("Hello world!");
        }

    }
}
