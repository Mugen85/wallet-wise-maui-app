// in WalletWise.Tests/RecurringTransactionTests.cs
using WalletWise.Persistence.Models;

namespace WalletWise.Tests;

public class RecurringTransactionTests
{
    [Fact] // [Fact] significa "questo è un test singolo"
    public void Should_Set_Defaults_Correctly()
    {
        // Arrange (Preparazione)
        // Creiamo un nuovo pezzo vuoto
        var transaction = new RecurringTransaction();

        // Act (Azione)
        // (In questo caso non c'è azione, verifichiamo lo stato iniziale)

        // Assert (Verifica)
        // Verifichiamo che i valori di default siano quelli "intelligenti" che abbiamo scelto.

        // La data di inizio deve essere "oggi" (o simile, controlliamo che non sia MinValue)
        Assert.NotEqual(DateTime.MinValue, transaction.StartDate);

        // La data di fine deve essere null (per sempre)
        Assert.Null(transaction.EndDate);

        // Le stringhe non devono essere null (per evitare crash)
        Assert.NotNull(transaction.Description);
        Assert.NotNull(transaction.Category);
    }
}