namespace WalletWise.Services;

public static class CategoryData
{
    public static List<string> GetDefaultCategories()
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
}