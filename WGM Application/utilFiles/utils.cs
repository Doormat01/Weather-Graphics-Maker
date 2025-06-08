using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGM_Application.C_
{
    internal class utils
    {
        public static void SetSavedLocation(string location)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (localSettings.Values.ContainsKey("UseCurrentLocation") && (bool)localSettings.Values["UseCurrentLocation"])
            {
                localSettings.Values["SavedLocation"] = location; // Use passed-in location, not LocationInput.Text
            }
        }
    }
}

