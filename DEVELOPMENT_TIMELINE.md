# Timeline di sviluppo - WalletWise

Questo documento è il mio diario di bordo del viaggio nello sviluppo di WalletWise. Traccia le tappe principali, le decisioni architetturali e, soprattutto, le lezioni che ho imparato lungo il percorso. È una mappa in continua evoluzione della strada che sto costruendo.

## 🛣️ Tappa 1: Le fondamenta (setup e architettura di base)

Usando una similitudine con la mia passione per la moto e i viaggi in moto, in questa fase ho preparato la moto e pianificato il viaggio, assicurandomi che il telaio fosse solido e il motore pronto a partire.

### 📍 Creazione del progetto
Ho avviato un nuovo progetto cross-platform con .NET MAUI, scegliendo la tecnologia più moderna e flessibile per il futuro.

### 🔗 Controllo di versione
Ho configurato il repository su GitHub, stabilendo fin da subito un flusso di lavoro professionale.

### 🏛️ Definizione architetturale
Ho adottato il pattern MVVM (Model-View-ViewModel) e definito la struttura delle cartelle (Views, ViewModels, Models, Services) per garantire un codice pulito e manutenibile.

### 🧱 Primi mattoni
Ho creato i Model di base (Account, Transaction) che rappresentano il cuore dei dati della mia applicazione.

### 🔧 Lezioni imparate / troubleshooting
- **Nomenclatura**: Ho imparato l'importanza di rinominare correttamente il progetto fin dall'inizio per evitare spazi e caratteri non standard, correggendo manualmente i file .sln, .csproj e il namespace predefinito.
- **Cache di Visual Studio**: Ho scoperto che la cartella nascosta .vs può mantenere impostazioni "vecchie" e che la sua eliminazione è una tecnica di reset fondamentale.

## 🏔️ Tappa 2: La strada dissestata (configurazione del database)

Questa è stata la mia prima vera salita, un passo di montagna pieno di tornanti stretti e imprevisti. Superarlo mi ha reso un pilota più esperto.

### ⚙️ Integrazione di Entity Framework Core
Ho aggiunto i pacchetti NuGet per EF Core e SQLite per dare una "memoria" persistente alla mia app.

### 🌉 Creazione del DbContext
Ho implementato il WalletWiseDbContext come ponte tra il mio codice C# e il database.

### 🚀 Dependency injection
Ho registrato il DbContext nel file MauiProgram.cs per renderlo disponibile in tutta l'applicazione.

### 🏆 La prima migrazione
Dopo una lunga battaglia, ho generato con successo la prima migrazione (InitialCreate) per creare lo schema del database.

### 🔧 Lezioni imparate / troubleshooting
- **Conflitti di versione**: Ho imparato a gestire i conflitti tra le versioni dei pacchetti NuGet e il workload .NET installato, allineando tutto a .NET 8.
- **Isolare la persistenza**: La soluzione definitiva per i problemi con gli strumenti di EF Core è stata creare un progetto di libreria di classi separato (WalletWise.Persistence) per ospitare tutta la logica di EF Core, disaccoppiandola completamente da MAUI.

## 🏙️ Tappa 3: La prima città (gestione dei conti)

Arrivato in cima al passo, ho raggiunto la mia prima città. Qui ho costruito la prima funzionalità completa e funzionante, vedendo finalmente il risultato del mio lavoro.

### 🔩 Introduzione dei service
Ho creato l'AccountService per incapsulare la logica di interazione con il database.

### 🎨 Creazione della AccountsPage e AddAccountPage
Ho sviluppato le interfacce XAML per visualizzare, creare ed eliminare i conti.

### 🗺️ Navigazione
Ho implementato un flusso di navigazione completo tra le pagine.

### 🔧 Lezioni imparate / troubleshooting
- **Il capriccio del Picker**: Ho affrontato un bug noto del controllo Picker che non si popolava correttamente. La soluzione definitiva è stata disaccoppiare il caricamento dei dati dal costruttore del ViewModel e invocarlo nel metodo OnAppearing del code-behind della pagina.

## 🎨 Tappa 4: Il cruscotto prende vita (dashboard e UI)

Con la moto perfettamente funzionante, è arrivato il momento di montare un cruscotto degno di questo nome.

### 📊 Creazione della dashboard
Ho implementato la DashboardPage e il suo DashboardViewModel per mostrare il patrimonio totale.

### #️⃣ Navigazione a schede
Ho trasformato la navigazione dell'app in una TabBar moderna per un'esperienza utente più fluida.

### ✨ Miglioramenti estetici
Ho definito una palette di colori e stili globali (Colors.xaml e Styles.xaml) per dare all'app un aspetto coerente e professionale.

## 🛑 Tappa 5: Il guado (stabilizzazione e decisioni difficili)

A volte, il viaggio ti mette di fronte a una strada impraticabile. Insistere significa rischiare di rompere la moto. La scelta saggia è fermarsi, consultare la mappa e decidere di tornare a una strada sicura per poi ripartire. Questa tappa è stata esattamente questo.

### 📉 Il problema del grafico
Dopo aver tentato di implementare un grafico prima con un GraphicsView custom e poi con la libreria esterna LiveCharts2, ho incontrato una serie di problemi di rendering, di aggiornamento e di build insormontabili e troppo dispendiosi in termini di tempo.

### 🎯 La decisione strategica: ritorno all'essenziale
Ho preso la decisione consapevole di rimuovere completamente la funzionalità del grafico per garantire la stabilità del progetto. La priorità assoluta è avere un'app pulita, funzionante e senza errori, piuttosto che una con una funzione "azzoppata".

### 💶 Messa a punto finale: la valuta
Ho risolto il problema della formattazione della valuta, forzando l'applicazione a usare la cultura "it-IT" (Euro) indipendentemente dalle impostazioni del sistema operativo.

### 🔧 Lezioni imparate / troubleshooting
- **Accettare la sconfitta tecnica**: La lezione più importante di questa tappa è stata imparare a riconoscere quando una funzionalità, per quanto desiderabile, sta diventando un "debito tecnico" troppo grande. Abbandonare temporaneamente il grafico non è un fallimento, ma una decisione architetturale matura per proteggere la salute del progetto.
- **Controllo della globalizzazione**: Ho imparato a impostare esplicitamente la CultureInfo all'avvio dell'app per avere il pieno controllo su come vengono formattati numeri, date e valute.

## ⚙️ Tappa 6: l'upgrade del motore (Refactoring Architetturale)

Dopo aver costruito la funzionalità delle transazioni, mi sono scontrato con un problema critico: i saldi dei conti non si aggiornavano in tempo reale. L'acceleratore rispondeva in ritardo. Invece di applicare soluzioni temporanee, ho deciso di fermarmi e fare un vero e proprio **upgrade architetturale**.

### 🔩 Implementazione delle Transazioni
Ho creato tutta l'infrastruttura per aggiungere nuove transazioni (modelli, service, viewmodel e view), rendendo il patrimonio totale dinamico.

### 🎯 Il problema: dati "vecchi" (Stale Data)
Ho capito che la causa del mancato aggiornamento era la gestione del `DbContext`. L'istanza, registrata come `Singleton` o `Scoped`, veniva riutilizzata e la sua cache interna non rifletteva le ultime modifiche al database.

### 🔧 Il Refactoring: `DbContextFactory`
Ho sostituito l'iniezione diretta del `DbContext` con una `IDbContextFactory`. Questo pattern, raccomandato da Microsoft, garantisce che ogni singola operazione dei miei service riceva una nuova istanza del `DbContext`, fresca e pulita, eliminando alla radice ogni problema di caching.

### 🧹 Pulizia finale
Con il problema risolto alla fonte, ho potuto rimuovere tutti i workaround precedenti, come il sistema di messaggistica (`IMessenger`), rendendo il codice più semplice e lineare.

### 🔧 Lezioni imparate / troubleshooting
- **Singleton vs. Transient vs. Factory**: Questa tappa è stata una lezione fondamentale sul ciclo di vita dei servizi e sull'importanza di scegliere la strategia giusta. Per le operazioni di database in app di lunga durata, la `DbContextFactory` è la soluzione più robusta.
- **No ai Workaround**: Ho imparato che, di fronte a un problema architetturale, insistere con soluzioni temporanee è controproducente. A volte, la scelta più veloce e sicura è fermarsi, fare un refactoring pesante e risolvere il problema alla radice.