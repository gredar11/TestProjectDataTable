using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestProjectDataTable.Tools;
[ValueConversion(typeof(bool), typeof(string))]
public class YesNoToBooleanConverter : IValueConverter
{
    string oldValue = string.Empty;
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        switch (value.ToString().ToLower())
        {
            case "yes":
                oldValue = string.Empty;
                return true;
            case "no":
                oldValue = string.Empty;
                return false;
            default:
                oldValue = value.ToString();
                break;

        }
        return false;
    }

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (oldValue != string.Empty)
            return oldValue;
        if (value is bool)
        {
            if ((bool)value == true)
                return "yes";
            else
                return "no";
        }
        return string.Empty;
    }
}
