using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TourPlannerUi.Converters {
    public class DateTimeToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is DateTime date) {
                // You can change the format as per your requirement
                return date.ToString("dd/mm/yyyy HH:mm");
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is string str && DateTime.TryParse(str, out DateTime date)) {
                return date;
            }
            return DateTime.MinValue;
        }
    }
}
