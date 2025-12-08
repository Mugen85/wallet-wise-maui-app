using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using WalletWise.Persistence.Data;
using WalletWise.Persistence.Models;
using WalletWise.Services;
using Xunit;

namespace WalletWise.Tests
{
    public class TransactionServiceTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly DbContextOptions<WalletWiseDbContext> _options;

        public TransactionServiceTests()
        {
            // 1. Creiamo connessione in RAM
            // "Filename=:memory:" crea un DB volatile che vive solo finché la connessione è aperta
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            // 2. Configuriamo EF Core per usare SQLite
            _options = new DbContextOptionsBuilder<WalletWiseDbContext>()
                .UseSqlite(_connection)
                .Options;

            // 3. Creiamo lo schema del database (Tabelle vuote)
            using var context = new WalletWiseDbContext(_options);
            context.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddTransaction_Should_Save_Transaction_To_Database()
        {
            // --- ARRANGE (Preparazione) ---

            // PASSO CRUCIALE: Creiamo un Account finto per soddisfare la Foreign Key
            // Senza questo, SQLite blocca l'inserimento perché l'AccountId non esiste.
            using (var seedContext = new WalletWiseDbContext(_options))
            {
                seedContext.Accounts.Add(new Account
                {
                    Id = 1,
                    Name = "Conto Principale",
                    InitialBalance = 1000,
                    Type = AccountType.StipendioSpese
                });
                seedContext.SaveChanges();
            }

            // Mockiamo la Factory per restituire il nostro DB in memoria
            var mockFactory = new Mock<IDbContextFactory<WalletWiseDbContext>>();
            mockFactory.Setup(f => f.CreateDbContextAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(() => new WalletWiseDbContext(_options));

            // Istanziamo il servizio
            var service = new TransactionService(mockFactory.Object);

            // Creiamo la transazione
            var newTransaction = new Transaction
            {
                Amount = 50,
                Description = "Pizza Test",
                Date = DateTime.Now,
                Category = "Alimentari", // Stringa come da tuo Model
                Type = TransactionType.Expense,
                AccountId = 1 // Colleghiamo all'account creato sopra
            };

            // --- ACT (Azione) ---
            await service.AddTransactionAsync(newTransaction);

            // --- ASSERT (Verifica) ---
            using var verifyContext = new WalletWiseDbContext(_options);
            var savedTransaction = await verifyContext.Transactions.FirstOrDefaultAsync();

            Assert.NotNull(savedTransaction);
            Assert.Equal("Pizza Test", savedTransaction.Description);
            Assert.Equal(50, savedTransaction.Amount);
            Assert.Equal(1, savedTransaction.AccountId);
        }

        public void Dispose()
        {
            // Chiudiamo la connessione, cancellando il DB in memoria
            _connection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}