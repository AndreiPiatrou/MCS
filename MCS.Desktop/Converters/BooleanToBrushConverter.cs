using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MCS.Desktop.Converters
{
    public class BooleanToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = Equals(value, true);

            return boolValue ? TrueValueBrush : FalseValueBrush;
        }

        public Brush FalseValueBrush { get; set; }

        public Brush TrueValueBrush { get; set; }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
