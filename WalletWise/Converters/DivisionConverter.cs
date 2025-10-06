using System.Globalization;

namespace WalletWise.Converters;

public class DivisionConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        try
        {
            var numerator = System.Convert.ToDouble(value);
            var denominator = System.Convert.ToDouble(parameter);

            if (denominator == 0) return 0.0;

            return numerator / denominator;
        }
        catch
        {
            return 0.0;
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
