# Wallet Wise

**Un'app di finanza personale per dominare il caos, un commit alla volta.**

📖 **[Leggi la timeline di sviluppo](DEVELOPMENT_TIMELINE.md)** - Il mio diario di bordo dettagliato del viaggio nello sviluppo di WalletWise

## 🚀 La doppia sfida: crescita tecnica e personale

Questo progetto è più di una semplice applicazione: è un campo di battaglia personale e professionale.

### La sfida tecnica

Da sviluppatore che punta a diventare architetto software, la mia filosofia è semplice: **la crescita avviene fuori dalla comfort zone**. Non ho mai sviluppato un'app con **.NET MAUI** e questo progetto è la mia immersione totale. L'obiettivo è padroneggiare un framework moderno e multipiattaforma, non seguendo un tutorial, ma costruendo da zero una soluzione reale a un problema reale.

### La sfida personale

La verità? La mia gestione finanziaria è sempre stata caotica. Conti multipli, spese, piccoli investimenti... un disastro. Ho deciso di affrontare questa debolezza con l'arma più potente che ho: **il codice**. Wallet Wise nasce per risolvere i *miei* problemi, con un'ossessione per la semplicità e l'intuitività.

Questo repository è il diario di bordo di questo duplice viaggio.

## 🎯 Obiettivi del progetto

Niente fronzoli. Solo le funzioni essenziali per avere il controllo.

* **📊 Dashboard chiara:** Una visione d'insieme del patrimonio netto, che distingua chiaramente tra liquidità e capitale investito.
* **💳 Gestione multi-conto:** Tracciamento semplice di conti correnti, risparmi e investimenti.
* **✍️ Inserimento rapido:** Un'interfaccia minimale per registrare entrate e uscite in pochi secondi.
* **🏗️ Architettura solida:** Un'applicazione robusta, testabile e manutenibile, costruita su principi di clean code.
* **📱 Esperienza multipiattaforma:** Un'unica codebase per un'esperienza nativa su Windows, Android e iOS.

## 💻 Stack tecnologico e architettura

La qualità del software si fonda su scelte architetturali consapevoli.

* **Framework:** **.NET MAUI** - Per lo sviluppo di ui native e multipiattaforma da un'unica base di codice C#.
* **Linguaggio:** **C#** - Il cuore di tutta la logica di business e di presentazione.
* **UI:** **XAML** - Per una definizione dichiarativa e pulita delle interfacce utente.
* **Architettura:** **MVVM (Model-View-ViewModel)** - Per una separazione netta tra ui, logica e dati, garantendo testabilità e manutenibilità.
* **Database:** **SQLite** - Motore di database leggero e serverless, ideale per lo storage locale.
* **Data Access:** **Entity Framework Core** - L'orm di riferimento per interagire con il database in modo astratto e object-oriented.

## 📈 Stato del progetto: in corso

* [x] Setup iniziale del progetto e della struttura delle cartelle.
* [x] Integrazione con Git e configurazione del repository.
* [x] Definizione dei model di base (`Account`, `Transaction`).
* [x] Configurazione del database con Entity Framework Core.
* [ ] **In corso:** Sviluppo delle prime view e viewmodel (Gestione Conti).
* [ ] **In corso:** Implementazione della dashboard principale.

---

*Questo progetto è un'avventura di apprendimento. Ogni feedback, suggerimento o critica costruttiva non è solo benvenuto, ma è essenziale. Grazie!*