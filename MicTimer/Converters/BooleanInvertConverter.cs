using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace MicTimer.Converters
{
    public class BooleanInvertConverter : IValueConverter
	{
	    public object Convert(object value, Type targetType, object parameter, string language)
	    {
		    bool original = (bool)value;
		    return !original;
	    }

	    public object ConvertBack(object value, Type targetType, object parameter, string language)
	    {
		    bool original = (bool)value;
		    return !original;
	    }
	}
}
