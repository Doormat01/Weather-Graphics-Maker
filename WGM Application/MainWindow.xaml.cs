using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Newtonsoft.Json.Linq;
using WGM_Application.API_Handling;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WGM_Application
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void openActions(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Hello world!");
        }
        public void openTemplate(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Opening Template!");
        }

        // This is for the user based location data and everything directly under will be removed
        private readonly List<string> locations = new List<string>
{
    "New York", "Los Angeles", "Chicago", "Houston", "Miami"
};

        private void OnLocationTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var filteredLocations = locations.Where(l => l.StartsWith(sender.Text, StringComparison.OrdinalIgnoreCase)).ToList();
                sender.ItemsSource = filteredLocations;
            }
        }

        private void OnLocationChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            sender.Text = args.SelectedItem.ToString();
        }

        private string location = "";
        // Poll user location
        private async void pollLocation(object sender, RoutedEventArgs e)
    {
        var geolocator = new Geolocator();
        Geoposition position = await geolocator.GetGeopositionAsync();

        double latitude = position.Coordinate.Point.Position.Latitude;
        double longitude = position.Coordinate.Point.Position.Longitude;

        System.Diagnostics.Trace.WriteLine($"User Location: {latitude}, {longitude}");

        location = await locationBasedWeatherAPI.GetTownFromLatLong( latitude, longitude );
        System.Diagnostics.Trace.WriteLine(location);
        LocationDisplay.Text = "Location: " + location;
    }



    
        private async void OpenLocationDialog(object sender, RoutedEventArgs e)
        {
            ContentDialogResult result = await LocationDialog.ShowAsync();

            if (string.IsNullOrWhiteSpace(LocationInput.Text))
            {
                LocationDisplay.Text = "Please enter a valid location.";
            }
            else
            {
                location = LocationInput.Text;
                LocationDisplay.Text = "Location: " + location;
            }

        }

    }
}
