// in WalletWise/Converters/StringDecimalConverter.cs
using System.Globalization;

namespace WalletWise.Converters;

public class StringDecimalConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Converte da decimal (ViewModel) a string (UI)
        if (value is decimal decValue)
        {
            // Mostra il valore solo se è maggiore di zero
            return decValue > 0 ? decValue.ToString(culture) : string.Empty;
        }
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Converte da string (UI) a decimal (ViewModel)
        if (decimal.TryParse(value as string, culture, out decimal result))
        {
            return result;
        }
        // Se la conversione fallisce (es. campo vuoto), restituisce 0
        return 0m;
    }
}
