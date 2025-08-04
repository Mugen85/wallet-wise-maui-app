// Converters/EnumToDescriptionConverter.cs
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace WalletWise.Converters;

public class EnumToDescriptionConverter : IValueConverter
{
    // Aggiunti '?' per rispettare il contratto dell'interfaccia IValueConverter
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Enum enumValue)
            return string.Empty;

        return GetEnumDescription(enumValue);
    }

    // Aggiunti '?' per rispettare il contratto dell'interfaccia IValueConverter
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private static string GetEnumDescription(Enum enumObj)
    {
        // Aggiunto controllo di nullabilità per rendere il codice più robusto
        FieldInfo? fieldInfo = enumObj.GetType().GetField(enumObj.ToString());
        if (fieldInfo == null)
            return enumObj.ToString();

        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes.Length > 0 ? attributes[0].Description : enumObj.ToString();
    }
}
