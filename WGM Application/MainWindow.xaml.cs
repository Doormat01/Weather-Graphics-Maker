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
using WGM_Application.C_;
using WGM_Application.AppWindows;
using Microsoft.UI.Xaml.Media.Animation;

namespace WGM_Application
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadToggleState();
        }
        public void openActions(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Hello world!");
        }
        public void openTemplate(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Opening Template!");
        }

        private async void OnLocationTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                string[] suggestions = await locationBasedWeatherAPI.GetCitySuggestions(sender.Text);
                sender.ItemsSource = suggestions;
            }
        }

        private void OnLocationChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            location = args.SelectedItem.ToString(); // Use chosen suggestion
            LocationDisplay.Text = "Location: " + location;

            utils.SetSavedLocation(location); // Save chosen location
        }


        private string location = "";
        // Poll user location
        private void PollLocationEnabled(object sender, RoutedEventArgs e)
        {
            PollLocation();
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["PollingEnabled"] = true;
        }

        // Eventually move this to be in the api handling
        public async void PollLocation()
        {
            var geolocator = new Geolocator();
            Geoposition position = await geolocator.GetGeopositionAsync();

            double latitude = position.Coordinate.Point.Position.Latitude;
            double longitude = position.Coordinate.Point.Position.Longitude;

            location = await locationBasedWeatherAPI.GetTownFromLatLong(latitude, longitude);
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

        private void OnUseCurrentLocationToggled(object sender, RoutedEventArgs e)
        {
            var toggle = sender as ToggleSwitch;
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["UseCurrentLocation"] = toggle.IsOn; // Save toggle state
        }

        private void OpenSettings(object sender, RoutedEventArgs e)
        {
            AppWindows.SettingsWindow settingsWindow = new AppWindows.SettingsWindow();
            settingsWindow.Activate();
        }

        private void LoadToggleState()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            // Check if Polling is enabled and call PollLocation() if true
            if (localSettings.Values.ContainsKey("PollingEnabled") &&
                localSettings.Values["PollingEnabled"] is bool pollingEnabled && pollingEnabled)
            {
                System.Diagnostics.Trace.WriteLine("Polling Enabled, calling PollLocation()");
                PollLocation();
            }
            else if (localSettings.Values.ContainsKey("UseCurrentLocation") &&
                     localSettings.Values["UseCurrentLocation"] is bool useLocation && useLocation)
            {
                // Use Saved Location when UseCurrentLocation is enabled
                System.Diagnostics.Trace.WriteLine("Using current location");
                UseCurrentLocationToggle.IsOn = true;

                LocationDisplay.Text = "Location: " +
                    (localSettings.Values.ContainsKey("SavedLocation")
                        ? localSettings.Values["SavedLocation"].ToString()
                        : "No location saved.");
            }
            else
            {
                // If neither Polling nor UseCurrentLocation are enabled, remove saved location
                System.Diagnostics.Trace.WriteLine("No polling or current location set, removing SavedLocation.");
                localSettings.Values.Remove("SavedLocation");

                // Set LocationDisplay to default prompt
                LocationDisplay.Text = "Input Location or Enable Polling";
            }
        }
    }
}
