using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestProjectDataTable.Tools;
[ValueConversion(typeof(double), typeof(double))]
public class RectangleSizeConverter : IValueConverter
{
    private double _multiplyer = 10.0;
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        double candidate = (double)value;
        return candidate * _multiplyer;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
