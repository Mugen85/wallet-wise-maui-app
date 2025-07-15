Wallet Wise: Un'App per la Finanza Personale e una Sfida di Crescita
Un'applicazione cross-platform, semplice e intuitiva per prendere il controllo delle proprie finanze, costruita con .NET MAUI.

La Doppia Sfida: Tecnologia e Disciplina
Questo progetto nasce da una duplice esigenza che rappresenta una sfida tanto professionale quanto personale.

La Sfida Tecnica: Come sviluppatore con l'ambizione di diventare architetto software, credo fermamente che uscire dalla propria zona di comfort sia l'unico modo per crescere. Non ho mai usato .NET MAUI prima d'ora. Questo progetto è il mio "banco di prova": un'immersione totale in un framework moderno e multipiattaforma per impararne i segreti, le best practice e le potenzialità, costruendo qualcosa di reale e funzionante da zero.

La Sfida Personale: Ammetto di non essere mai stato un mago nella gestione delle mie finanze. Spese, conti multipli, piccoli investimenti... tenere traccia di tutto è sempre stato un punto debole. Ho deciso di trasformare questa debolezza in un punto di forza, affrontandola con gli strumenti che conosco meglio: il codice. L'obiettivo è creare un'app che risolva i miei problemi, con la speranza che la sua semplicità possa essere d'aiuto anche ad altri.

Questo repository, quindi, non è solo una vetrina di codice, ma il diario di un duplice viaggio: l'apprendimento di una nuova tecnologia e il percorso verso una maggiore consapevolezza finanziaria.

Obiettivi del Progetto
L'obiettivo non è creare l'ennesima app piena di funzioni complesse, ma realizzare uno strumento essenziale e "smart".

Dashboard Intuitiva: Una schermata principale che offra una visione chiara e immediata del proprio patrimonio netto, distinguendo tra liquidità disponibile e capitale investito.

Gestione Multi-Conto: Possibilità di tracciare facilmente conti correnti, conti di risparmio e conti di investimento separati.

Inserimento Semplice: Un'interfaccia minimale e veloce per registrare entrate e uscite senza attriti.

Architettura Pulita: Costruire un'applicazione robusta, testabile e manutenibile seguendo i principi del clean code e dei design pattern moderni.

Multipiattaforma: Un'unica codebase per un'esperienza fluida su Windows, Android e (in futuro) iOS.

Stack Tecnologico e Architettura
La qualità del software si basa su scelte architetturali solide. Questo progetto adotta un approccio moderno e standardizzato.

Framework: .NET MAUI - Per lo sviluppo di interfacce utente native e multipiattaforma da un'unica base di codice C#.

Linguaggio: C# - Il linguaggio principale per tutta la logica di business e di presentazione.

UI: XAML - Per la definizione dichiarativa e strutturata delle interfacce utente.

Architettura: MVVM (Model-View-ViewModel) - Per garantire una netta separazione tra la logica di presentazione (ViewModel), i dati (Model) e l'interfaccia utente (View), migliorando la testabilità e la manutenibilità del codice.

Database: SQLite - Un motore di database leggero, serverless e self-contained, perfetto per lo storage locale su applicazioni desktop e mobile.

Data Access: Entity Framework Core - L'Object-Relational Mapper (ORM) di Microsoft per interagire con il database SQLite in modo astratto e orientato agli oggetti, scrivendo meno codice SQL possibile.

Stato del Progetto
Attualmente il progetto è in fase di configurazione iniziale.

[x] Creazione della struttura del progetto.

[x] Collegamento al repository Git.

[ ] Definizione dei Model iniziali.

[ ] Configurazione del database con EF Core.

[ ] Sviluppo delle prime View e ViewModel.

Questo progetto è sviluppato con passione e voglia di imparare. Ogni feedback, suggerimento o critica costruttiva è più che benvenuto.