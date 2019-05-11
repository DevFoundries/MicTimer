using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MicTimer.Model
{
    public class DataService : IDataService
    {
	    public List<DurationOption> GetDurationOptions()
	    {
			Windows.Storage.ApplicationDataContainer localSettings =
				Windows.Storage.ApplicationData.Current.LocalSettings;
			var options = localSettings.Values["DurationOptions"] as string;
			if (options == null)
			{
				var defaultOptions = new List<DurationOption>()
				{
					new DurationOption() {Minutes = 10, Label = "10 Minutes"},
					new DurationOption() {Minutes = 12, Label = "12 Minutes"},
					new DurationOption() {Minutes = 15, Label = "15 Minutes"},
					new DurationOption() {Minutes = 20, Label = "20 Minutes"},
				};
				SaveDurationOptions(defaultOptions);
			}
			else
			{

				return JsonConvert.DeserializeObject<List<DurationOption>>(options).OrderBy(x => x.Minutes).ToList();
			}

			return GetDurationOptions();
	    }

	    public void SaveDurationOptions(List<DurationOption> options)
	    {
		    string toSave = JsonConvert.SerializeObject(options);
		    Windows.Storage.ApplicationDataContainer localSettings =
			    Windows.Storage.ApplicationData.Current.LocalSettings;
		    localSettings.Values["DurationOptions"] = toSave;

	    }

        public Settings GetSettings()
        {
            Windows.Storage.ApplicationDataContainer localSettings =
                Windows.Storage.ApplicationData.Current.LocalSettings;
            var settings = localSettings.Values["Settings"] as string;
            if (settings == null)
            {
                var defaultSettings = new Settings()
                {
                      AlertAtSeconds = 0,
                      WarnAtSeconds = 150
                };
                SaveSettings(defaultSettings);
            }
            else
            {

                return JsonConvert.DeserializeObject<Settings>(settings);
            }

            return GetSettings();
        }

        public void SaveSettings(Settings settings)
        {
            string toSave = JsonConvert.SerializeObject(settings);
            Windows.Storage.ApplicationDataContainer localSettings =
                Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["Settings"] = toSave;
        }
    }
}