# Timeline di sviluppo - WalletWise

Questo documento è il mio diario di bordo del viaggio nello sviluppo di WalletWise. Traccia le tappe principali, le decisioni architetturali e, soprattutto, le lezioni che ho imparato lungo il percorso. È una mappa in continua evoluzione della strada che sto costruendo.

## 🛣️ Tappa 1: Le fondamenta (setup e architettura di base)

Usando una similitudine con la mia passione per la moto e i viaggi in moto, in questa fase ho preparato la moto e pianificato il viaggio, assicurandomi che il telaio fosse solido e il motore pronto a partire.

- **📍 Creazione del progetto:** Ho avviato un nuovo progetto cross-platform con .NET MAUI.
- **🔗 Controllo di versione:** Ho configurato il repository su GitHub.
- **🏛️ Definizione architetturale:** Ho adottato il pattern MVVM e definito la struttura delle cartelle.
- **🧱 Primi mattoni:** Ho creato i modelli di base (account, transazione).

### 🔧 Lezioni imparate / troubleshooting

**Nomenclatura:** Ho imparato l'importanza di rinominare correttamente il progetto fin dall'inizio.

**Cache di Visual Studio:** Ho scoperto che la cartella nascosta `.vs` può mantenere impostazioni "vecchie" e che la sua eliminazione è una tecnica di reset fondamentale.

## 🏔️ Tappa 2: La strada dissestata (configurazione del database)

Questa è stata la mia prima vera salita, un passo di montagna pieno di tornanti stretti e imprevisti. Superarlo mi ha reso un pilota più esperto.

- **⚙️ Integrazione di Entity Framework Core:** Ho aggiunto i pacchetti NuGet per EF Core e SQLite.
- **🌉 Creazione del DbContext:** Ho implementato il `WalletWiseDbContext`.
- **🚀 Dependency injection:** Ho registrato il DbContext nel file `MauiProgram.cs`.
- **🏆 La prima migrazione:** Dopo una lunga battaglia, ho generato con successo la prima migrazione.

### 🔧 Lezioni imparate / troubleshooting

**Conflitti di versione:** Ho imparato a gestire i conflitti tra le versioni dei pacchetti NuGet e il workload .NET.

**Isolare la persistenza:** La soluzione definitiva è stata creare un progetto separato (`WalletWise.Persistence`) per tutta la logica di EF Core.

## 🏙️ Tappa 3: La prima città (gestione dei conti)

Arrivato in cima al passo, ho raggiunto la mia prima città. Qui ho costruito la prima funzionalità completa e funzionante.

- **🔩 Introduzione dei service:** Ho creato l'`AccountService`.
- **🎨 Creazione della AccountsPage e AddAccountPage:** Ho sviluppato le interfacce XAML.
- **🗺️ Navigazione:** Ho implementato un flusso di navigazione completo tra le pagine.

### 🔧 Lezioni imparate / troubleshooting

**Il capriccio del picker:** Ho imparato a disaccoppiare il caricamento dei dati dal costruttore del ViewModel, invocandolo nel metodo `OnAppearing` del code-behind.

## 🎨 Tappa 4: Il cruscotto prende vita (dashboard e UI)

Con la moto perfettamente funzionante, è arrivato il momento di montare un cruscotto degno di questo nome.

- **📊 Creazione della dashboard:** Ho implementato la `DashboardPage` per mostrare il patrimonio totale.
- **#️⃣ Navigazione a schede:** Ho trasformato la navigazione in una TabBar moderna.
- **✨ Miglioramenti estetici:** Ho definito una palette di colori e stili globali.

## 🛑 Tappa 5: Il guado (stabilizzazione e decisioni difficili)

A volte, il viaggio ti mette di fronte a una strada impraticabile. La scelta saggia è fermarsi, consultare la mappa e tornare a una strada sicura.

- **📉 Il problema del grafico:** Ho incontrato una serie di problemi insormontabili con le librerie per i grafici.
- **🎯 La decisione strategica: ritorno all'essenziale:** Ho rimosso la funzionalità del grafico per garantire la stabilità del progetto.
- **💶 Messa a punto finale: la valuta:** Ho risolto il problema della formattazione della valuta forzando la cultura "it-IT".

### 🔧 Lezioni imparate / troubleshooting

**Accettare la sconfitta tecnica:** Ho imparato a riconoscere quando una funzionalità sta diventando un "debito tecnico" troppo grande.

**Controllo della globalizzazione:** Ho imparato a impostare esplicitamente la `CultureInfo` per avere il pieno controllo sulla formattazione.

## ⚙️ Tappa 6: L'upgrade del motore (refactoring architetturale)

Dopo aver costruito le transazioni, mi sono scontrato con un problema critico: i saldi non si aggiornavano in tempo reale. Invece di soluzioni temporanee, ho deciso di fare un upgrade architetturale.

- **🔩 Implementazione delle transazioni:** Ho creato tutta l'infrastruttura per aggiungere nuove transazioni.
- **🎯 Il problema: dati "vecchi" (stale data):** Ho capito che la causa era la gestione del DbContext (Singleton/Scoped) e la sua cache interna.
- **🔧 Il refactoring: DbContextFactory:** Ho sostituito l'iniezione diretta con una `IDbContextFactory`, garantendo che ogni operazione riceva un'istanza nuova del DbContext.
- **🧹 Pulizia finale:** Ho rimosso i workaround precedenti (es. `IMessenger`), semplificando il codice.

### 🔧 Lezioni imparate / troubleshooting

**Singleton vs. transient vs. factory:** Ho imparato l'importanza del ciclo di vita dei servizi. Per le operazioni di database, la `DbContextFactory` è la soluzione più robusta.

**No ai workaround:** Ho imparato che di fronte a un problema architetturale, la scelta più veloce è fermarsi e fare un refactoring pesante.

## 🗺️ Tappa 7: Il navigatore di bordo (gestione completa del budget)

Con un motore affidabile, è arrivato il momento di installare lo strumento più importante del nostro viaggio: il navigatore GPS che ci tiene sulla rotta finanziaria corretta. Questa tappa è stata dedicata a costruire la sezione budget, rendendola non solo funzionante, ma intelligente e robusta.

### 🎯 La sfida: coerenza dei dati

Il primo approccio, basato sull'inserimento manuale delle categorie, si è rivelato inaffidabile. Una semplice maiuscola diversa tra un budget e una transazione era sufficiente a rompere il collegamento.

### 🔧 La soluzione architetturale: fonte unica di verità

**Centralizzazione:** Ho creato una classe statica (`CategoryData.cs`) come unica fonte autorevole per tutte le categorie di spesa.

**Standardizzazione UI:** Ho sostituito gli entry testuali con dei picker (menu a tendina) sia nella creazione dei budget che delle transazioni, forzando l'utente a scegliere da una lista predefinita e garantendo la coerenza dei dati al 100%.

### 🔄 Aggiornamenti in tempo reale

Ho implementato la logica nel metodo `OnAppearing` della `BudgetPage` per forzare il ricaricamento dei dati ogni volta che la pagina diventa visibile. Questo garantisce che i progressi dei budget siano sempre aggiornati dopo l'inserimento di una nuova transazione.

### ✅ Funzionalità complete (CRUD)

Ho completato l'intera gestione dei budget, implementando la creazione, la modifica (con pre-compilazione dei dati) e la cancellazione, proteggendo quest'ultima con un dialogo di conferma gestito da un `AlertService` riutilizzabile.

### ✨ Messa a punto finale: coerenza UI

Ho standardizzato l'aspetto dei pulsanti di cancellazione in tutta l'app, sostituendo la vecchia "X" testuale nella pagina dei conti con lo stesso stile di pulsante usato per i budget, per un'esperienza utente più pulita e professionale.

### 🔧 Lezioni imparate / troubleshooting

**La coerenza dei dati è regina:** Per dati critici come le categorie, una fonte centralizzata e un input controllato (picker) sono infinitamente superiori all'inserimento di testo libero.

**L'importanza del "refresh":** L'interfaccia utente non sempre sa quando i dati sottostanti sono cambiati. A volte, è necessario dirle esplicitamente di ricaricare il suo stato (`OnAppearing` è uno strumento potente per questo).

**La coerenza è professionalità:** Un'interfaccia utente coerente non è solo una questione estetica, ma rende l'app più prevedibile, intuitiva e facile da usare.