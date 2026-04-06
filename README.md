# **Wallet Wise**

Un'app di finanza personale per dominare il caos, un commit alla volta.

Il mio obiettivo principale è crescere come Software Developer e Architetto, ma questo progetto è la prova della mia filosofia nello sviluppo software, ereditata da anni in officina: **la sostanza vince sull'apparenza**. Un codice bello da leggere non serve a nulla se si spacca in produzione.

## **🏍 Il mio biglietto da visita: usabilità e architettura**

Wallet Wise è progettato per essere facilmente utilizzabile anche da chi non sa nulla di finanza. Sotto il cofano, l'architettura è stata costruita per garantire che l'app sia veloce, affidabile e, soprattutto, compilabile in modo sicuro con i moderni standard AOT (Ahead-of-Time).

## **📱 Prova l'app su Android**

Vuoi toccare con mano la "sostanza"? Scarica l'ultima versione dell'app direttamente sul tuo smartphone.

👉 [**SCARICA L'APK (ultima versione)**](https://github.com/Mugen85/wallet-wise-maui-app/releases/tag/v1.2.0)

**Nota per l'installazione:** poiché questa è un'app demo e non proviene dal Google Play Store, il tuo telefono potrebbe chiederti di autorizzare l'installazione da "Origini sconosciute" o dal browser. È sicuro: il codice è tutto qui, open source\!

## **🖼️ Anteprima rapida**

### **Onboarding (primo avvio)**

L'utente è guidato a creare il primo conto senza vedere schermate vuote.

### **Flusso principale**

Demo rapida di creazione conto, budget e transazione.

## **🎯 Obiettivi e funzionalità attuali**

Niente fronzoli. Solo le funzioni essenziali per avere il controllo.

* ✅ **Onboarding intuitivo**: l'utente è guidato a creare il primo conto, evitando schermate vuote e intimidatorie. La logica di avvio è a prova di bug.  
* ✅ **Budgeting solido**: i budget sono visualizzati con barre di progresso chiare e riutilizzano le impostazioni del mese precedente, rendendo l'app "intelligente" e riducendo il lavoro manuale.  
* ✅ **Pilota Automatico**: infrastruttura completa per la gestione delle transazioni ricorrenti (stipendio, affitto, ecc.). Questo è il cuore della nostra usabilità.  
* 📊 **Dashboard chiara**: una visione d'insieme del patrimonio netto.  
* 💳 **Gestione multi-conto**: tracciamento semplice di conti correnti, risparmi e investimenti.

## **💻 Stack tecnologico e architettura (La Sostanza)**

La qualità del software si fonda su scelte architetturali consapevoli che garantiscono manutenibilità e affidabilità. Niente accrocchi.

* **Framework**: .NET 9 MAUI \- Per lo sviluppo di UI native e multipiattaforma.  
* **Architettura**: Clean Architecture con MVVM (Community Toolkit) \- Separazione netta e invalicabile tra UI (Presentation), logica di business e dati (Persistence).  
* **Database**: SQLite \+ Entity Framework Core 9\.  
* **Costrutti a Prova di Bug (AOT Compliant)**: utilizzo di Dependency Injection rigorosa, e soprattutto **zero dipendenze dalla reflection a runtime** per il data binding. Questo garantisce che l'app non si spacchi quando passa sotto la fresa del linker in fase di build.

## **🔧 Sotto il cofano (Battle Scars & Problem Solving)**

**Il problema del Trimmer AOT in Release:**

Durante la migrazione a .NET 9, i Picker della UI risultavano vuoti esclusivamente nelle build Release per Android, mentre in Debug funzionavano perfettamente.

* **Diagnosi:** Il Trimmer (Linker) di .NET 9 elimina aggressivamente il codice non richiamato staticamente. Il binding XAML basato su ItemDisplayBinding="{Binding Name}" usa la reflection, rendendo la proprietà invisibile al Trimmer, che la rimuoveva silenziosamente dall'APK finale.  
* **Soluzione architetturale:** Invece di forzare il Linker con file XML (soluzione fragile), ho applicato il polimorfismo. Ho creato dei DisplayModel specifici per la UI, sovrascrivendo il metodo .ToString() (che è AOT-safe e non viene mai trimmato). Il ViewModel fa da "meccanico": mappa rigidamente l'Enum di Dominio verso il Wrapper della UI per la visualizzazione, e fa il percorso inverso durante il salvataggio nel DB. Zero reflection, prestazioni UI migliorate e build Release blindata.

## **📈 Stato del progetto: In Corso**

* \[x\] Setup iniziale del progetto e della struttura delle cartelle.  
* \[x\] Definizione dei model di base (Account, Transaction, Budget, RecurringTransaction).  
* \[x\] Configurazione del database con Entity Framework Core (Migrazioni completate).  
* \[x\] Implementazione dell'Onboarding e del sistema di avvio a prova di bug.  
* \[x\] Implementazione della Dashboard principale e della visualizzazione dei Budget con barre di progresso.  
* \[x\] Sviluppo della funzionalità "Pilota Automatico" (Logica di salvataggio e visualizzazione).  
* \[x\] Aggiunta dell'infrastruttura di Unit Testing (xUnit).  
* \[x\] **Refactoring architetturale per compatibilità totale con compilazione AOT/Release su Android .NET 9\.**

## **🤝 Contributi e Feedback**

Questo progetto è un'avventura di apprendimento e crescita. Ogni feedback, suggerimento o critica costruttiva è essenziale per migliorare. Se sei un Senior e vedi qualcosa che faresti diversamente, apri una issue.

**Sostanza sopra tutto. Sempre.**