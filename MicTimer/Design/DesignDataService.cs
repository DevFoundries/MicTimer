using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using MicTimer.Model;

namespace MicTimer.Design
{
    public class DesignDataService : IDataService
    {
	    public List<DurationOption> GetDurationOptions()
	    {
			return new List<DurationOption>()
			{
				new DurationOption() {Minutes = 10, Label = "10 Minutes"},
				new DurationOption() {Minutes = 12, Label = "12 Minutes"},
				new DurationOption() {Minutes = 15, Label = "15 Minutes"},
				new DurationOption() {Minutes = 20, Label = "20 Minutes"},
			};
	    }
	    public void SaveDurationOptions(List<DurationOption> durationOptions)
	    {
	    }

        public Settings GetSettings()
        {
            return new Settings()
            {
                AlertAtSeconds = 0,
                WarnAtSeconds = 120
            };
        }

        public void SaveSettings(Settings settings)
        {
        }
    }
}