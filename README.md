# Wallet Wise

Un'app di finanza personale per dominare il caos, un commit alla volta.

Il mio obiettivo principale è crescere come montatore meccanico, ma questo progetto è la prova della mia filosofia nello sviluppo software: **la sostanza vince sull'apparenza**.

## 🏍Il mio biglietto da visita: usabilità e sostanza

Wallet Wise è progettato per essere facilmente utilizzabile anche da chi non sa nulla di finanza. L'architettura è stata costruita per garantire che l'app sia veloce, affidabile e che l'utente sia guidato in ogni fase.

## 📱 Prova l'app su Android

Vuoi toccare con mano la "sostanza"? Scarica l'ultima versione dell'app direttamente sul tuo smartphone.

👉 [**SCARICA L'APK (ultima versione)**](https://github.com/Mugen85/wallet-wise-maui-app/releases/latest)

**Nota per l'installazione:** poiché questa è un'app demo e non proviene dal Google Play Store, il tuo telefono potrebbe chiederti di autorizzare l'installazione da "Origini sconosciute" o dal browser. È sicuro: il codice è tutto qui, open source!

## 🖼️ Anteprima rapida

### Onboarding (primo avvio)

![Screenshot Onboarding](docs/images/onboarding_screenshot.png)

L'utente è guidato a creare il primo conto senza vedere schermate vuote.

### Flusso principale

![Flusso di lavoro rapido](docs/images/flow_demo.gif)

Demo rapida di creazione conto, budget e transazione.

## 🎯 Obiettivi e funzionalità attuali

Niente fronzoli. Solo le funzioni essenziali per avere il controllo.

* ✅ **Onboarding intuitivo (NEW)**: l'utente è guidato a creare il primo conto, evitando schermate vuote e intimidatorie. La logica di avvio è a prova di bug.
* ✅ **Budgeting solido**: i budget sono visualizzati con barre di progresso chiare e riutilizzano le impostazioni del mese precedente, rendendo l'app "intelligente" e riducendo il lavoro manuale.
* ✅ **Pilota Automatico (In Lavorazione)**: infrastruttura completa per la gestione delle transazioni ricorrenti (stipendio, affitto, ecc.). Questo è il cuore della nostra usabilità futura.
* 📊 **Dashboard chiara**: una visione d'insieme del patrimonio netto.
* 💳 **Gestione multi-conto**: tracciamento semplice di conti correnti, risparmi e investimenti.

## 💻 Stack tecnologico e architettura (sostanza)

La qualità del software si fonda su scelte architetturali consapevoli che garantiscono manutenibilità e affidabilità.

* **Framework**: .NET MAUI - Per lo sviluppo di UI native e multipiattaforma.
* **Architettura**: MVVM con Community Toolkit MVVM - Separazione netta tra UI, logica e dati.
* **Database**: SQLite + Entity Framework Core.
* **Costrutti a Prova di Bug**: utilizzo massimo di Dependency Injection per l'iniezione dei ViewModel e adozione di layout generati in Code-Behind (C#) per le liste complesse, bypassando noti bug di rendering XAML e garantendo la stabilità.

## 📈 Stato del progetto: In Corso

* [x] Setup iniziale del progetto e della struttura delle cartelle.
* [x] Definizione dei model di base (`Account`, `Transaction`, `Budget`, `RecurringTransaction`).
* [x] Configurazione del database con Entity Framework Core (Migrazioni completate).
* [x] Implementazione dell'Onboarding e del sistema di avvio a prova di bug.
* [x] Implementazione della Dashboard principale e della visualizzazione dei Budget con barre di progresso.
* [x] Aggiunta dell'infrastruttura di Unit Testing (xUnit).
* [x] **Completato**: Sviluppo della funzionalità "Pilota Automatico" (Logica di salvataggio e visualizzazione).

## 🤝 Contributi e Feedback

Questo progetto è un'avventura di apprendimento e crescita. Ogni feedback, suggerimento o critica costruttiva è essenziale per migliorare.

---

**Sostanza sopra tutto. Sempre.**