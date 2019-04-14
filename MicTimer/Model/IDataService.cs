using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicTimer.Model
{
    public interface IDataService
    {
	    List<DurationOption> GetDurationOptions();
	    void SaveDurationOptions(List<DurationOption> durationOptions);

    }

    public class DurationOption
    {
	    public int Minutes { get; set; }
		public string Label { get; set; }
    }
}