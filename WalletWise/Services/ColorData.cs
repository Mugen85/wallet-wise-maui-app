// in WalletWise/Services/ColorData.cs
namespace WalletWise.Services;

public static class ColorData
{
    private static List<Color>? _palette;

    private static List<Color> Palette => _palette ??=
    [
        Color.FromArgb("#6a0dad"),
        Color.FromArgb("#0077b6"),
        Color.FromArgb("#2a9d8f"),
        Color.FromArgb("#e9c46a"),
        Color.FromArgb("#f4a261"),
        Color.FromArgb("#e76f51"),
        Color.FromArgb("#e63946")
    ];

    public static Color GetColorByIndex(int index)
    {
        if (index < 0) return Palette[0];
        return Palette[index % Palette.Count];
    }
}