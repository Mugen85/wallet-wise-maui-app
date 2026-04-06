using System.Globalization;

namespace WalletWise.Converters;

public class StringDecimalConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is decimal decimalValue)
        {
            // Mostra il numero nella UI in formato locale (es. "100,50" in Italia)
            return decimalValue == 0 ? string.Empty : decimalValue.ToString("0.##", CultureInfo.CurrentCulture);
        }
        return string.Empty;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var stringValue = value as string;
        if (string.IsNullOrWhiteSpace(stringValue))
            return 0m;

        // Normalizzazione brutale: cambiamo tutte le virgole in punti
        // e usiamo l'Invariant Culture per forzare il parsing. 
        // Questo resiste a qualsiasi tastiera Android/iOS.
        var normalizedString = stringValue.Replace(",", ".");

        if (decimal.TryParse(normalizedString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
        {
            return result;
        }

        // Se l'utente ha scritto lettere o roba strana, restituiamo 0
        return 0m;
    }
}