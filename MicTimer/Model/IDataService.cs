using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicTimer.Model
{
    public interface IDataService
    {
	    List<DurationOption> GetDurationOptions();
	    void SaveDurationOptions(List<DurationOption> durationOptions);
        Settings GetSettings();
        void SaveSettings(Settings settings);

    }

    public class DurationOption
    {
	    public int Minutes { get; set; }
		public string Label { get; set; }
    }

    public class Settings
    {
        public int WarnAtSeconds { get; set; }
        public int AlertAtSeconds { get; set; }
    }
}