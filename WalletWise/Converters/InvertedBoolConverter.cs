// in WalletWise/Converters/InvertedBoolConverter.cs
using System.Globalization;

namespace WalletWise.Converters;

// Questo è un costrutto standard e solido per invertire un segnale.
// Lo useremo per dire: "Mostra questo SE 'HasBudgets' è FALSO".
public class InvertedBoolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Se il valore in ingresso è 'true', noi restituiamo 'false'.
        // Se è 'false', restituiamo 'true'.
        return !(value is bool boolValue && boolValue);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Questo non ci serve, ma l'interfaccia lo richiede.
        throw new NotImplementedException();
    }
}