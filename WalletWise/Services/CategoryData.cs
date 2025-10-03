namespace WalletWise.Services;

public static class CategoryData
{
    // Rinominiamo il metodo per chiarezza
    public static List<string> GetExpenseCategories()
    {
        return
        [
            "Spesa Alimentare",
            "Trasporti",
            "Svago",
            "Bollette",
            "Salute",
            "Casa",
            "Altro"
        ];
    }

    // Aggiungiamo il nuovo metodo per le entrate
    public static List<string> GetIncomeCategories()
    {
        return
        [
            "Stipendio",
            "Dividendi/Proventi",
            "Risparmio",
            "Altro"
        ];
    }
}