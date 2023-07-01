using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TourPlannerUi.Converters {
    public class StringToTimeSpanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            TimeSpan time = (TimeSpan)value;
            return time.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            string timeString = (string)value;
            if (TimeSpan.TryParse(timeString, out TimeSpan time)) {
                return time;
            }
            return TimeSpan.Zero;
        }
    }
}
