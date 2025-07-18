# Timeline di sviluppo - WalletWise

Questo documento è il mio diario di bordo del viaggio nello sviluppo di WalletWise. Traccia le tappe principali, le decisioni architetturali e, soprattutto, le lezioni che ho imparato lungo il percorso. È una mappa in continua evoluzione della strada che sto costruendo.

## 🛣️ Tappa 1: Le fondamenta (Setup e architettura di base)

Usando una similitudine con la mia passione per la moto e i viaggi in moto, in questa fase ho preparato la moto e pianificato il viaggio, assicurandomi che il telaio fosse solido e il motore pronto a partire.

### 📍 Creazione del progetto
Ho avviato un nuovo progetto cross-platform con .NET MAUI, scegliendo la tecnologia più moderna e flessibile per il futuro.

### 🔗 Controllo di versione
Ho configurato il repository su GitHub, stabilendo fin da subito un flusso di lavoro professionale.

### 🏛️ Definizione architetturale
Ho adottato il pattern MVVM (Model-View-ViewModel) e definito la struttura delle cartelle (Views, ViewModels, Models, Services) per garantire un codice pulito e manutenibile.

### 🧱 Primi mattoni
Ho creato i Model di base (Account, Transaction) che rappresentano il cuore dei dati della mia applicazione.

### 🔧 Lezioni imparate / Troubleshooting

**Nomenclatura:** Ho imparato l'importanza di rinominare correttamente il progetto fin dall'inizio per evitare spazi e caratteri non standard, correggendo manualmente i file `.sln`, `.csproj` e il namespace predefinito.

**Cache di Visual Studio:** Ho scoperto che la cartella nascosta `.vs` può mantenere impostazioni "vecchie" e che la sua eliminazione è una tecnica di reset fondamentale.

## 🏔️ Tappa 2: La strada dissestata (configurazione del database)

Questa è stata la mia prima vera salita, un passo di montagna pieno di tornanti stretti e imprevisti. Superarlo mi ha reso un pilota più esperto.

### ⚙️ Integrazione di Entity Framework Core
Ho aggiunto i pacchetti NuGet per EF Core e SQLite per dare una "memoria" persistente alla mia app.

### 🌉 Creazione del DbContext
Ho implementato il `WalletWiseDbContext` come ponte tra il mio codice C# e il database.

### 🚀 Dependency Injection
Ho registrato il DbContext nel file `MauiProgram.cs` per renderlo disponibile in tutta l'applicazione.

### 🏆 La prima migrazione
Dopo una lunga battaglia, ho generato con successo la prima migrazione (`InitialCreate`) per creare lo schema del database.

### 🔧 Lezioni imparate / Troubleshooting

**Conflitti di versione:** Ho imparato a gestire i conflitti tra le versioni dei pacchetti NuGet e il workload .NET installato, allineando tutto a .NET 8.

**Limiti degli strumenti di EF Core:** Ho scoperto che gli strumenti `dotnet ef` hanno difficoltà a lavorare con progetti MAUI. La mia soluzione è stata una scalata di tecniche:

- Cambiare il target da Android a Windows Machine
- Usare il .NET CLI (`dotnet ef migrations add...`) invece della console di VS
- Forzare il framework di destinazione con il flag `--framework`

**Isolare la persistenza:** La soluzione definitiva è stata creare un progetto di libreria di classi separato (`WalletWise.Persistence`) per ospitare tutta la logica di EF Core, disaccoppiandola completamente da MAUI. Questo è ora il mio standard architetturale.

## 🏙️ Tappa 3: la prima città (gestione dei conti)

Arrivato in cima al passo, ho raggiunto la mia prima città. Qui ho costruito la prima funzionalità completa e funzionante, vedendo finalmente il risultato del mio lavoro.

### 🔩 Introduzione dei Service
Ho creato l'`AccountService` per incapsulare la logica di interazione con il database, mantenendo i ViewModel puliti.

### 🧠 Logica nel AccountsViewModel
Ho implementato il ViewModel per la gestione della lista dei conti, usando il `CommunityToolkit.Mvvm` per ridurre il codice ripetitivo.

### 🎨 Creazione della AccountsPage
Ho sviluppato l'interfaccia XAML per visualizzare la lista dei conti.

### 🗺️ Navigazione
Ho implementato un flusso di navigazione completo per passare dalla lista dei conti alla nuova pagina per l'aggiunta di un conto.

### ✍️ Input utente
Ho creato la `AddAccountPage` con i relativi campi di input (Entry, Picker) per permettere all'utente di inserire nuovi dati.

### 🔧 Lezioni imparate / Troubleshooting

**Binding nel DataTemplate:** Ho risolto un errore di binding specificando `x:DataType` nel `DataTemplate` di una `CollectionView`, una best practice per la robustezza del codice XAML.

**Il capriccio del Picker:** Ho affrontato un bug noto del controllo `Picker` che non si popolava correttamente. La soluzione definitiva è stata disaccoppiare il caricamento dei dati dal costruttore del ViewModel e invocarlo nel metodo `OnAppearing` del code-behind della pagina, garantendo una perfetta sincronizzazione con il ciclo di vita della UI.