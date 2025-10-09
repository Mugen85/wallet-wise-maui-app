// in WalletWise/Services/ColorData.cs
namespace WalletWise.Services;

/// <summary>
/// Fornisce una palette di colori standard e riutilizzabili per l'interfaccia.
/// </summary>
public static class ColorData
{
    private static readonly List<Color> Palette =
    [
        Color.FromArgb("#6a0dad"), // Viola
        Color.FromArgb("#0077b6"), // Blu
        Color.FromArgb("#2a9d8f"), // Verde Acqua
        Color.FromArgb("#e9c46a"), // Giallo Ocra
        Color.FromArgb("#f4a261"), // Arancione
        Color.FromArgb("#e76f51"), // Terracotta
        Color.FromArgb("#e63946")  // Rosso Chiaro
    ];

    /// <summary>
    /// Restituisce un colore dalla palette in modo ciclico.
    /// </summary>
    /// <param name="index">L'indice del budget nella lista.</param>
    /// <returns>Un colore per la UI.</returns>
    public static Color GetColorByIndex(int index)
    {
        if (index < 0)
        {
            return Palette[0];
        }
        // L'operatore modulo (%) garantisce che, se finiscono i colori, si ricomincia dal primo.
        return Palette[index % Palette.Count];
    }
}
