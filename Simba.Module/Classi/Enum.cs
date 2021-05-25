using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace CAMS.Module.Classi
{

    public enum TipoRicorrenze
    {
        [XafDisplayName("Minuti")]
        Minuti,
        [XafDisplayName("Ore")]
        Ore,
        [XafDisplayName("Giorni")]
        Giorni,
        [XafDisplayName("Settimana")]
        Settimana
    }

    public enum TipoAzioneRoleDestinatari
    {
        [XafDisplayName("Controlli Normativi")]
        ControlliNormativi,
        [XafDisplayName("Trasmissione Segnalazione")]
        TrasmissioneSegnalazione,
        [XafDisplayName("Trasmissione Ticket Assistenza EAMS")]
        TrasmissioneTicketAssistenza,
        [XafDisplayName("Inserimento Modifica Scheda MP")]
        InserimentoModificaSchedaMP
    }

    public enum TipologiaSpedizione
    {
        [XafDisplayName("Mail")]
        MAIL,
        [XafDisplayName("SMS")]
        SMS,
        [XafDisplayName("Mail + SMS")]
        MAILSMS,
        [XafDisplayName("SMS Breve")]
        SMSBREVE,
        [XafDisplayName("Mail + SMS Breve")]
        MAILSMSBREVE
    }
    public enum TipologiaSpedizioneRdL
    {
        [XafDisplayName("Mail")]
        MAIL,
        [XafDisplayName("SMS")]
        SMS,
        [XafDisplayName("Mail + SMS")]
        MAILSMS,
        [XafDisplayName("nessun invio")]
        No
    }

    public enum ConditionalAppearanceSuperpositionOfRulesAllPropertiesDisabledExcept
    {
        EnableAll,
        RuoloGdL,
        RuoloGdLInsert,
        RuoloSTF,
        RuoloSTFInsert
    }

    public enum Soddisfazione
    {
        [XafDisplayName("Molto Insoddisfatto")]
        MOLTO_INSODDISFATTO,
        [XafDisplayName("Insoddisfatto")]
        INSODDISFATTO,
        [XafDisplayName("Indifferente")]
        INDIFFERENTE,
        [XafDisplayName("Soddisfatto")]
        SODDISFATTO,
        [XafDisplayName("Molto Soddisfatto")]
        MOLTO_SODDISFATTO
    }



    public enum TipoModifichePMP
    {
        GruppoLavoroMetodi,

        Modificata,
        NuovoInserimento
    }

    public enum TipoModifica
    {
        Inserimento,
        Modifica
    }

    public enum KDefault
    {
        No,
        Si
    }

    public enum FlgAbilitato
    {
        No,
        Si
    }
    public enum FlgApprovato
    {
        nd,
        nonApprovato,
        Approvato
    }

    public enum CensimentoFatto
    {
        No,
        Si
    }

    public enum Fatto
    {
        [XafDisplayName("Non Definito")]
        ND,
        [XafDisplayName("No")]
        No,
        [XafDisplayName("Si")]
        Si,
        [XafDisplayName("Non Necessario")]
        NonNecessario
    }

    public enum Eseguito
    {
        [XafDisplayName("No")]
        No,
        [XafDisplayName("Si")]
        Si
    }

    public enum TipoAggiornamento
    {
        [XafDisplayName("Annullamento")]
        Annullamento,
        [XafDisplayName("Gestione Sala Operativa")]
        GestioneSO,
        [XafDisplayName("Assegnazione")]
        Assegnazione,
        [XafDisplayName("Completamento")]
        Completamento
    }


    public enum NumeroPlafoniere
    {
        nd,
        uno,
        due,
        tre,
        quattro,
        cinque,
        sei,
        sette,
        otto,
        nove,
        dieci
    }


    public enum TraslazioneSchedulazione
    {
        [XafDisplayName("Fisso")]
        Fisso,//0
        [XafDisplayName("1 Settimana")]
        UnaSettimana,//1
        [XafDisplayName("2 Settimana")]
        DueSettimane,//2
        [XafDisplayName("3 Settimana")]
        TreSettimane,//3
        [XafDisplayName("4 Settimana")]
        QuattroSettimane,//4
        [XafDisplayName("5 Settimana")]
        CinqueSettimane//5
    }

    public enum TipoCadenze
    {
        [XafDisplayName("Anno")]
        Anno,
        [XafDisplayName("Escluso")]
        Escluso,
        [XafDisplayName("Giorno")]
        Giorno,
        [XafDisplayName("Mese")]
        Mese,
        [XafDisplayName("Settimana")]
        Settimana,
        [XafDisplayName("Periodico a Ore")]
        PeriodicoaOre,
        [XafDisplayName("Evento a Ore")]
        aEventoaOre,
        [XafDisplayName("a Condizione")]
        aCondizione,
        [XafDisplayName("Stagionale")]
        Stagionale
    }

    public enum TipoGestione
    {
        [XafDisplayName("da Operativo")]
        daOpearativo,
        [XafDisplayName("da SalaOperativa")]
        daSalaOperativa
    }

    public enum TipoModificheLibreriaImp
    {
        Altro,
        Modificata,
        NuovoInserimento
    }

    public enum EntitaAsset
    {
        Raggruppamento = 0,
        Reale = 1,
        Virtuale = 2
    }



    public enum TipoAssociazioneGostRisorseTeam
    {
        perMansione,
        perSkill,
    }

    public enum TipoCogenzaNormativa
    {
        [XafDisplayName("No")]
        No,
        [XafDisplayName("Si")]
        Si
    }

    public enum RisorsaAssegnataaTeam
    {
        Assegnato,
        AssegnatoCoppiaLinkata,
        ErroreAssegnazione,
        NonAssegnato
    }

    public enum GhostAssegnataaTeam
    {
        Assegnato,
        NonAssegnato
    }


    public enum TaskStatusOdL
    {
        [ImageName("State_Task_Completed")]
        Completato = 4,
        [ImageName("State_Task_NotStarted")]
        daIniziare = 0,
        [ImageName("State_Task_WaitingForSomeoneElse")]
        InAttesadiSpecialistico = 2,
        [ImageName("State_Task_InProgress")]
        InLavorazione = 1,
        [ImageName("State_Task_Deferred")]
        Sospeso = 3
    }

    public enum MPSettimane
    {
        Prima,
        Quarta,
        Quinta,
        Seconda,
        Terza
    }


    public enum Presidiato
    {
        No,
        Si,
        NonDefinito
    }
    //TipoStraordinario
    public enum TipoStraordinario
    {
        [XafDisplayName("non Consentito")]
        nonConsentito,
        [XafDisplayName("1%")]
        perc1,
        [XafDisplayName("2%")]
        perc2,
        [XafDisplayName("3%")]
        perc3,
        [XafDisplayName("4%")]
        perc4,
        [XafDisplayName("5%")]
        perc5,
        [XafDisplayName("6%")]
        perc6,
        [XafDisplayName("7%")]
        perc7,
        [XafDisplayName("8%")]
        perc8,
        [XafDisplayName("9%")]
        perc9,
        [XafDisplayName("10%")]
        perc10,
        [XafDisplayName("11%")]
        perc11,
        [XafDisplayName("12%")]
        perc12,
        [XafDisplayName("13%")]
        perc13
    }


    public enum TipoCO
    {
        DiPresidio,
        Itinerante,
    }

    public enum TipoAggregazioneRegMP
    {
        [XafDisplayName("Manuale x Aggregazioni")]
        ManualexAggregazioni,
        [XafDisplayName("Carico Giornaliero Risorsa")]
        CaricoGiornalieroRisorsa
    }

    public enum Saturazione
    {
        NonSaturo,
        Saturo,
        NonDefinito
    }

    public enum TipoGhost
    {
        [XafDisplayName("di Presidio")]
        DiPresidio,
        [XafDisplayName("Fisso su Immobile")]
        FissoPerEdificio,
        [XafDisplayName("Itinerante su Cluster")]
        ItinerantePerCluster,
        [XafDisplayName("Itinerante su  Scenario")]
        ItinerantePerScenario,
    }
    public enum CoppiaLinkataGhost
    {
        No,
        NonCompatibile,
        NonDefinito,
        Si
    }
    public enum TipoMpGiorno
    {
        No,
        Si,
    }




    public enum StatiNotificaEmergenza
    {
        [ImageName("State_Task_WaitingForSomeoneElse")]
        [XafDisplayName("Non Visualizzato")]
        NonVisualizzato,
        [ImageName("State_Task_NotStarted")]
        [XafDisplayName("Visualizzato")]
        Visualizzato,
        [ImageName("State_Task_NotStarted")]
        [XafDisplayName("Accettato")]
        Accettato,
        [ImageName("State_Task_Completed")]
        [XafDisplayName("Impegnato altra Emergenza")]
        ImpegnatoAltraEmergenza,
        [ImageName("State_Task_Completed")]
        [XafDisplayName("Rifutato")]
        Rifutato,
        [ImageName("State_Task_Completed")]
        [XafDisplayName("Accettato da Altra Risorsa")]
        AccettatodaAltraRisorsa

    }
    public enum RegStatiNotificaEmergenza
    {
        [ImageName("State_Task_WaitingForSomeoneElse")]
        [XafDisplayName("da Assegnare")]
        daAssegnare,
        [ImageName("State_Task_NotStarted")]
        [XafDisplayName("Assegnato")]
        Assegnato,
        [ImageName("State_Task_Completed")]
        [XafDisplayName("Completato")]
        Completato,
        [ImageName("State_Task_Completed")]
        [XafDisplayName("Rifutato")]
        Rifutato,
        [ImageName("State_Task_WaitingForSomeoneElse")]
        [XafDisplayName("da Assegnare (ripetuto)")]
        daAssegnare_Ripetuto,
        [ImageName("State_Task_Completed")]
        [XafDisplayName("Annullato")]
        Annullato

    }

    public enum TipoAutorizzazioniRegRdL
    {
        [XafDisplayName("Mostra Data e Ora Fermo")]
        MostraDataOraFermo,
        [XafDisplayName("Mostra Data e Ora Riavvio")]
        MostraDataOraRiavvio,
        [XafDisplayName("Mostra Data e Ora del Sopralluogo")]
        MostraDataOraSopralluogo,
        [XafDisplayName("Mostra Data e Ora Azione Tampone")]
        MostraDataOraAzioniTampone,
        [XafDisplayName("Mostra Data e Ora di Inizio Lavori")]
        MostraDataOraInizioLavori,
        [XafDisplayName("Mostra Data e Ora Completamento")]
        MostraDataOraCompletamento,
        [XafDisplayName("Mostra in Apps Elenco Cause e Rimedi")]
        MostraElencoCauseRimedi,
        [XafDisplayName("Mostra inserimento Piani Locali")]
        MostraPianiLocali,
        [XafDisplayName("Mostra CodProgRdL")]
        MostraCodProgRdL,
        [XafDisplayName("Mostra Soddisfazione Cliente")]
        MostraSoddisfazioneCliente
    }


    public enum TipoStato
    {
        Aperto,
        Chiuso,
        PresoInCarica
    }
    public enum TipoStatoMAIL
    {
        nonInviata,
        Inviata
    }

    public enum StatoScenarioClusterEdificio
    {
        Aperto,
        Bloccato,
        nonDefinito
    }

    public enum StatoClusterEdificio
    {
        Aperto,
        Bloccato,
        nonDefinito
    }


    public enum TipoDocumento
    {
        [XafDisplayName("Datasheet Apparato")] //0
        DocumentoApparato,
        [XafDisplayName("Datasheet Impianto")]  //1
        DocumentoImpianto,
        [XafDisplayName("Datasheet Immobile")]  //2
        DocumentoEdificio,
        [XafDisplayName("Allegato dell'Apparato")]  //3
        AllegatoApparato,
        [XafDisplayName("Allegato dell'Impianto")]  //4
        AllegatoImpianto,
        [XafDisplayName("Allegato dell'Immobile")]  //5
        AllegatoEdificio,
        [XafDisplayName("Allegato della RdL")]  //6
        DocumentoRdl,//associato rdl
        [XafDisplayName("Dettaglio del Registro Costi")]  //7
        RegistroCostiDettaglio,//associato ai costi
        [XafDisplayName("Documenti Normativi")]  //8
        AllegatoControlloNormativo,//Associato Controlli norm
        [XafDisplayName("Manuale dell'Apparato")]  //9
        ManualeApparato,
        [XafDisplayName("Riferimenti Normativi")]  //10
        RiferimentiNormativi,//Associato riferimenti normativi
        [XafDisplayName("Istruzione Operativa")] //11
        IstruzioneOperativa,
        [XafDisplayName("Allegato del Piano")] //12
        AllegatodelPiano,
        [XafDisplayName("Allegato del Locale")] //13
        AllegatodelLocale,
        [XafDisplayName("Allegato del RegRdL")] //14
        DocumentoRegRdL,//associato RegRdL       
        [XafDisplayName("Allegato della Commessa")] //15
        AllegatoCommessa,

    }

    public enum TipoPlanimetria
    {
        [XafDisplayName("pdf")]
        [ImageName("State_Priority_Low")]
        pdf,

        [XafDisplayName("Dwf")]
        [ImageName("State_Priority_Low")]
        dwf,
        [XafDisplayName("Image")]
        [ImageName("State_Priority_Low")]
        img,

        [XafDisplayName("dwg")]
        [ImageName("State_Priority_Low")]
        dwg
    }

    public enum Insourcing
    {
        Si, //0
        No
    }

    public enum StatoComponente
    {
        InServizio,  // 0
        FuoriServizio
    }

    public enum StatoControlloNormativo
    {
        // [XafDisplayName("Una Persona")]  TipoContattoCliente
        [ImageName("State_Priority_Low")]
        InCreazione,
        //  [XafDisplayName("Due Persone")]
        [ImageName("State_Priority_Low")]
        Pianificato,
        // [XafDisplayName("Una Persona")]  TipoContattoCliente
        //[ImageName("State_Priority_Normal")]
        //InScadenza,
        //  [XafDisplayName("Due Persone")]
        [ImageName("State_Priority_High")]
        Completato,

    }

    public enum StatoSegnalazione
    {
        [XafDisplayName("Nuovo")]
        [ImageName("State_Priority_Low")]
        Nuovo,//0
        [XafDisplayName("Non Decodificato")]
        [ImageName("State_Priority_Low")]
        NonDecodificato,//1
        [XafDisplayName("Decodificato")]
        [ImageName("State_Priority_Low")]
        Decodificato,//2
        [XafDisplayName("Creato RdL")]
        [ImageName("State_Priority_Normal")]
        CreatoRdL,//3
        [XafDisplayName("Trasmesso")]
        [ImageName("State_Priority_Normal")]
        Trasmesso,//4
        [XafDisplayName("Completato")]
        [ImageName("State_Priority_High")]
        Completato,//5

    }

    public enum TipoNumeroManutentori
    {
        [XafDisplayName("Non Definito")]
        NonDefinito,
        [XafDisplayName("Una Persona")]
        unaPersona,
        [XafDisplayName("Due Persone")]
        duePersone,
        [XafDisplayName("Tre Persone")]
        trePersone,
        [XafDisplayName("Quattro Persone")]
        quattroPersone,
        [XafDisplayName("Cinque Persone")]
        cinquePersone,
        [XafDisplayName("Sei Persone")]
        seiPersone,
        [XafDisplayName("molte Persone")]
        moltePersone
    }

    public enum TipoAssociazioneTRisorsa
    {
        [XafDisplayName("Non Definito")]
        NonDefinito,
        [XafDisplayName("Sala Operativa")]
        Sala_Operativa,
        [XafDisplayName("Smartphone")]
        Smartphone
    }


    public enum TipoStatoOperativoAttivitaDettaglio
    {
        [XafDisplayName("non definito")]
        nd,//0
        [XafDisplayName("Trasf. da C.O.")]
        TrasfdaCO,//1
        [XafDisplayName("In Sito")]
        InSito,//2
        [XafDisplayName("Trasf. da Sito")]
        TrasfdaSitoaSito,//3
        [XafDisplayName("Trasf. da Sito e ritorno in C.O.")]
        TrasfdaSitoaSitoeCO,//4
        [XafDisplayName("Trasf. ritorno in C.O.")]
        TrasfdaSitoaeCO,//5
        [XafDisplayName("Trasf. Andata e Ritorno")]
        TrasfdaSitoaCOeCOaSito,//6
    }
    public enum TipoContattoCliente
    {
        [XafDisplayName("Amministratore")]
        Amministratore,
        [XafDisplayName("Referente in Loco")]
        ReferenteinLoco,
    }

    public enum TipoReferenteContratto
    {
        [XafDisplayName("Project Manager")]
        PM,
        [XafDisplayName("Assistente al PM")]
        AS,
        [XafDisplayName("Referente Operativo")]
        Operativo,
        [XafDisplayName("Terzo Responsabile")]
        TerzoResponsabile
    }



    public enum StatoCommessa
    {
        [ImageName("State_Task_Completed")]
        InEsercizio = 4,
        [ImageName("State_Task_NotStarted")]
        daIniziare = 0,
        [ImageName("State_Task_WaitingForSomeoneElse")]
        InAttesadiSpecialistico = 2,
        [ImageName("State_Task_InProgress")]
        InLavorazione = 1,
        [ImageName("State_Task_Deferred")]
        Sospeso = 3
    }

    public enum Meserif
    {
        [XafDisplayName("Selezionare Mese")]
        Nomese = 0,
        [XafDisplayName("Gennaio")]
        Gennaio = 1,
        [XafDisplayName("Febbraio")]
        Febbraio = 2,
        [XafDisplayName("Marzo")]
        Marzo = 3,
        [XafDisplayName("Aprile")]
        Aprile = 4,
        [XafDisplayName("Maggio")]
        Maggio = 5,
        [XafDisplayName("Giugno")]
        Giugno = 6,
        [XafDisplayName("Luglio")]
        Luglio = 7,
        [XafDisplayName("Agosto")]
        Agosto = 8,
        [XafDisplayName("Settembre")]
        Settembre = 9,
        [XafDisplayName("Ottobre")]
        Ottobre = 10,
        [XafDisplayName("Novembre")]
        Novembre = 11,
        [XafDisplayName("Dicembre")]
        Dicembre = 12

    }





    public enum InManutenzione
    {
        [XafDisplayName("Si")]
        Si,
        [XafDisplayName("No")]
        No
    }

    public enum TipoNota
    {
        [XafDisplayName("Sollecito Cliente")]
        Sollecito,
        [XafDisplayName("Nota Sala Operativa")]
        NotaSalaOperativa,
        [XafDisplayName("Reclamo Cliente")]
        Reclamo,
        [XafDisplayName("Nota Tecnico")]
        NotaTecnico
    }

    public enum tipoMTBF
    {
        [XafDisplayName("Disservizio")]
        conDisservizio,
        [XafDisplayName("In Servizio")]
        InServizio
    }


    public enum StatoSirvizioServizio
    {
        [XafDisplayName("Acceso")]
        [ImageName("State_Priority_Low")]
        Acceso,
        [XafDisplayName("in Riposo Stagionale")]
        [ImageName("State_Priority_Normal")]
        InRiposoStagionale,
        [XafDisplayName("Spento Per Manutenzione")]
        [ImageName("State_Priority_High")]
        SpentoxMS,
        [XafDisplayName("Spento Per Guasto")]
        [ImageName("State_Priority_High")]
        SpentoxGusto
    }

    public enum StatoTLCServizio
    {
        [XafDisplayName("ND")]
        [ImageName("State_Priority_Low")]
        ND,
        [XafDisplayName("TLC in Automatico")]
        [ImageName("State_Priority_Normal")]
        TLCinAutomatico,
        [XafDisplayName("TLC in Manuale")]
        [ImageName("State_Priority_High")]
        TLCinManuale,
        [XafDisplayName("In Manuale")]
        [ImageName("State_Priority_High")]
        InManuale
    }

    #region sms enum
    //L'enum SMSStatus può assumere i seguenti valori:

    public enum TipoInvio
    {
        [XafDisplayName("eMail")]
        [ImageName("State_Priority_Low")]
        mail,	// postponed, not jet arrived  //SCHEDULED	// posticipato, non ancora inviato
        [XafDisplayName("SMS")]
        [ImageName("State_Priority_Low")]
        sms,		// sent, wait for delivery notification (depending on message type) //SENT	// inviato, non attende delivery
    }

    public enum EsitoInvioMailSMS
    {
        [XafDisplayName("posticipato, non ancora inviato")]
        [ImageName("State_Priority_Low")]
        SCHEDULED,	// postponed, not jet arrived  //SCHEDULED	// posticipato, non ancora inviato
        [XafDisplayName("Consegnato")] //inviato, non attende delivery
        [ImageName("State_Priority_Low")]
        SENT,		// sent, wait for delivery notification (depending on message type) //SENT	// inviato, non attende delivery
        [XafDisplayName("l'SMS è stato correttamente ricevuto")]
        [ImageName("State_Priority_Low")]
        DLVRD,		// the sms has been correctly delivered to the mobile phone   DLVRD	// l'SMS è stato correttamente ricevuto
        [XafDisplayName("errore in invio dell'SMS")]
        [ImageName("State_Priority_Low")]
        ERROR,		// error sending sms   ERROR	// errore in invio dell'SMS
        [XafDisplayName("l'operatore non ha fornito informazioni sull'SMS entro le 48 ore")]
        [ImageName("State_Priority_Low")]
        TIMEOUT,	// cannot deliver sms to the mobile in 48 hours  TIMEOUT	// l'operatore non ha fornito informazioni sull'SMS entro le 48 ore
        [XafDisplayName("troppi SMS per lo stesso destinatario nelle ultime 24 ore")]
        [ImageName("State_Priority_Low")]
        TOOM4NUM,	// too many messages sent to this number (spam warning)  TOOM4NUM	// troppi SMS per lo stesso destinatario nelle ultime 24 ore
        [XafDisplayName("troppi SMS inviati dall'utente nelle ultime 24 ore")]
        [ImageName("State_Priority_Low")]
        TOOM4USER,	// too many messages sent by this user  TOOM4USER	// troppi SMS inviati dall'utente nelle ultime 24 ore
        [XafDisplayName("prefisso SMS non valido o sconosciuto")]
        [ImageName("State_Priority_Low")]
        UNKNPFX,	// unknown/unparsable mobile phone prefixUNKNPFX	// prefisso SMS non valido o sconosciuto
        [XafDisplayName("numero di telefono del destinatario non valido o sconosciuto")]
        [ImageName("State_Priority_Low")]
        UNKNRCPT,	// unknown recipient   UNKNRCPT	// numero di telefono del destinatario non valido o sconosciuto
        [XafDisplayName("messaggio inviato, in attesa di delivery")]
        [ImageName("State_Priority_Low")]
        WAIT4DLVR,	// message sent, waiting for delivery notification WAIT4DLVR	// messaggio inviato, in attesa di delivery

        [XafDisplayName("in attesa, non ancora inviato")]
        [ImageName("State_Priority_Low")]
        WAITING,	// not yet sent (still active)WAITING	// in attesa, non ancora inviato
        [XafDisplayName("stato sconosciuto")]
        [ImageName("State_Priority_Low")]
        UNKNOWN,		// received an unknown status code from server (should never happen!)UNKNOWN	// stato sconosciuto
        /// <summary>
        ///  SATO INVIO SMS GENERICO (sopra è di dettaglio)
        /// </summary>
        [XafDisplayName("consegnato.")]
        [ImageName("State_Priority_Low")]
        CONSEGNATO_SMSGen,	//  è stato consegnato      -  13    
        [XafDisplayName("in attesa ...")]
        [ImageName("State_Priority_Low")]
        INATTESA_SMSGen,		//                           --  14

        ///////-----------------------------------        
        //   da qui mail                  MAIL
        [XafDisplayName("Inviata")]
        Inviata = 100,
        [XafDisplayName("Errore nell'Invio")]
        ErrorediInvio = 101,
        [XafDisplayName("non Inviata")]
        NonInviata = 102,

    }

    public enum TipoValoreCaratteristicaTecnica
    {
        [XafDisplayName("Intero")]
        [ImageName("State_Priority_Low")]
        Intero = 0,
        [XafDisplayName("Testo")]
        [ImageName("State_Priority_Normal")]
        Testo = 1,
        [XafDisplayName("Tendina")]
        [ImageName("State_Priority_High")]
        Tendina = 2,
        [XafDisplayName("Decimale")]
        [ImageName("State_Priority_High")]
        Decimale = 3
    }





    #endregion



    public enum FasiContrattualiConsip
    {
        [XafDisplayName("in Lavorazione")]
        inLavorazione,
        [XafDisplayName("Emesso Ordine OPF")]
        EmessoOrdineOPF,
        [XafDisplayName("Emessa Rinuncia")]
        Rinuncia
    }

    public enum TipoContrattoConsip
    {
        [XafDisplayName("Ordine di Fornitura")]
        OPF,
        [XafDisplayName("Atto Aggiuntivo")]
        AttoAggiuntivo
    }

    public enum TipoRiservato
    {
        No,
        Si,
    }


    public enum TipoCampo
    {
        [ImageName("State_Task_WaitingForSomeoneElse")]
        [XafDisplayName("Campo Testata")]
        CampoTestata,
        [ImageName("State_Task_NotStarted")]
        [XafDisplayName("Lista Multi Colonne")]
        ListaMultiColonne

    }
    public enum TipoEsitoImport
    {
        [ImageName("State_Task_WaitingForSomeoneElse")]
        [XafDisplayName("nd")]
        nd,
        [ImageName("State_Task_NotStarted")]
        [XafDisplayName("Conforme")]
        Conforme,
        [ImageName("State_Task_NotStarted")]
        [XafDisplayName("non Conforme")]
        nonConforme,
        [ImageName("State_Task_NotStarted")]
        [XafDisplayName("Import Excel")]
        ncImportExcel,
        [ImageName("State_Task_NotStarted")]
        [XafDisplayName("Anagrafica")]
        ncAnagrafica,
        [ImageName("State_Task_NotStarted")]
        [XafDisplayName("Altro")]
        Altro

    }

    public enum ReportExcelFiltro
    {
        [XafDisplayName("con Criteri di filtro selezionati da PopUp")]
        conCriteridiPopUp,
        [XafDisplayName("senza PopUp di filtro")]
        senzaCriteridiPopUp
    }
    public enum TipoMisura
    {

        [XafDisplayName("nd")]
        nd,
        [XafDisplayName("Stimata")]
        Stimata,
        [XafDisplayName("Effettiva")]
        Effettiva,
        [XafDisplayName("Telecontrollo")]
        Telecontrollo,
        [XafDisplayName("Bolletta")]
        Bolletta,
        [XafDisplayName("Stima da Effemeridi")]
        StimaEffemeridi
    }
    public enum TipoDatoSystem
    {
        [ImageName("State_Task_WaitingForSomeoneElse")]
        [XafDisplayName("nd")]
        nd,
        [ImageName("State_Task_WaitingForSomeoneElse")]
        [XafDisplayName("Intero")]
        Integer,
        [ImageName("State_Task_WaitingForSomeoneElse")]
        [XafDisplayName("Stringa")]
        String,
        [ImageName("State_Task_NotStarted")]
        [XafDisplayName("Decimale")]
        Double,
        [ImageName("State_Task_NotStarted")]
        [XafDisplayName("Data Ora")]
        DataTime,
        [ImageName("State_Task_NotStarted")]
        [XafDisplayName("Oggetto Referenziato")]
        RefObject

    }

    //public enum TipoVincoloCampo
    //{
    //    [ImageName("State_Task_WaitingForSomeoneElse")]
    //    [XafDisplayName("nd")]
    //    nd,
    //    [ImageName("State_Task_WaitingForSomeoneElse")]
    //    [XafDisplayName("Intero")]
    //    Integer,
    //    [ImageName("State_Task_WaitingForSomeoneElse")]
    //    [XafDisplayName("Stringa")]
    //    String,
    //    [ImageName("State_Task_NotStarted")]
    //    [XafDisplayName("Decimale")]
    //    Double,
    //    [ImageName("State_Task_NotStarted")]
    //    [XafDisplayName("Data Ora")]
    //    DataTime

    //}

    public enum TipologiaFornituraBolletta
    {
        [XafDisplayName("Energia Elettrica")]
        Elettrica,
        [XafDisplayName("Gas Metano")]
        GasMetano,
        [XafDisplayName("Acqua")]
        Acqua,
        [XafDisplayName("Non Definito")]
        NonDefinito
    }

    public enum TipoLetturaBolletta
    {
        [XafDisplayName("Effettiva")]
        Effettiva,
        [XafDisplayName("Stimata")]
        Stimata
    }

    public enum TipoMercato
    {
        [XafDisplayName("Libero")]
        Libero,
        [XafDisplayName("Tutela")]
        Tutela
    }

    public enum TipoMasterMisura
    {
        [XafDisplayName("Non Definito")]
        [ImageName("State_Priority_Low")]
        NonDefinito,
        [XafDisplayName("Manuale")]
        [ImageName("State_Priority_Normal")]
        Manuale,
        [XafDisplayName("Telecontrollo")]
        [ImageName("State_Priority_High")]
        Telecontrollo,
    }

    public enum Congruo
    {
        [XafDisplayName("Conforme")]
        Conforme = 0,
        [XafDisplayName("non Conforme")]
        nonConforme = 1
    }

    #region scheduler
    public enum LabelListView
    {
        [XafDisplayName("0 non definito")]
        nondefinito,
        [XafDisplayName("1 in attesa di dichiarazione tecnico")]
        in_attesa_di_dichiarazione_tecnico,
        [XafDisplayName("2 in attesa accettazione tempo so")]
        in_attesa_accettazione_so,
        [XafDisplayName("3 in attesa della presa in carico")]
        in_attesa_presa_in_carico,
        [XafDisplayName("4 presa in carico (in Lavorazione)")]
        Presa_in_Carico_inLavorazione //,
        //[XafDisplayName("5 in Sito")]
        //in_Sito,
        //[XafDisplayName("6 Sospeso")]
        //Sospeso,
        //[XafDisplayName("7 Completato intervento")]
        //Completato_intervento,
        //[XafDisplayName("8 sospeso da riprendere in carico ")]
        //Sospeso_da_riprendere_in_carico,
        //[XafDisplayName("9 in Urgenza - Attesa presa in carico")]
        //In_Urgenza_Attesa_presa_in_carico
    }
    #endregion

    public enum enTrimestre
    {
        [XafDisplayName("I Trimestre")]
        I_Trimestre = 0,
        [XafDisplayName("II Trimestre")]
        II_Trimestre = 1,
        [XafDisplayName("III Trimestre")]
        III_Trimestre = 2,
        [XafDisplayName("IV Trimestre")]
        IV_Trimestre = 3,

        [XafDisplayName("V Trimestre")]
        V_Trimestre = 4,
        [XafDisplayName("VI Trimestre")]
        VI_Trimestre = 5,
        [XafDisplayName("VII Trimestre")]
        VII_Trimestre = 6,
        [XafDisplayName("VIII Trimestre")]
        VIII_Trimestre = 7,
        [XafDisplayName("IX Trimestre")]
        IX_Trimestre = 8,
        [XafDisplayName("X Trimestre")]
        X_Trimestre = 9,
        [XafDisplayName("XI Trimestre")]
        XI_Trimestre = 10,
        [XafDisplayName("XII Trimestre")]
        XII_Trimestre = 11,
        [XafDisplayName("XIII Trimestre")]
        XIII_Trimestre = 12,
        [XafDisplayName("XIV Trimestre")]
        XIV_Trimestre = 13

    }
    public enum StatoPOI
    {
        [XafDisplayName("non Definito")]
        [ImageName("State_Priority_Low")]
        nonDefinito,
        [XafDisplayName("Pianificato")]
        [ImageName("State_Priority_Low")]
        Pianificato,
        [XafDisplayName("Eseguito")]
        [ImageName("State_Priority_High")]
        Eseguito,
        [XafDisplayName("Scaduto")]
        [ImageName("State_Priority_High")]
        Scaduto,
        [XafDisplayName("Trasmesso")]
        [ImageName("State_Priority_High")]
        Trasmesso,
        [XafDisplayName("Schedulato")]
        [ImageName("State_Priority_High")]
        Schedulato

    }


    public enum TipoAssegnazione
    {
        [XafDisplayName("non definito")]
        ND,
        [XafDisplayName("Sala Operativa")]
        SO,
        [XafDisplayName("Smartphone")]
        SM,
        [XafDisplayName("Smartphone Emergenza")]
        SME,
        [XafDisplayName("Sala Operativa by Mail")]
        SOBYMAIL,
        [XafDisplayName("Smartphone by Mail")]
        SMBYMAIL,
        [XafDisplayName("Smartphone Emergenza by Mail")]
        SMEBYMAIL,
    }



    public enum TipoRegola
    {
        [XafDisplayName("non definito")]
        ND,
        [XafDisplayName("Regola Automatismi Assegnazione")]
        RegolaAutomatismiAssegnazione,
        [XafDisplayName("Regola Selezione RisorseTeam")]
        RegolaSelezioneRisorseTeam
    }
    public enum AggiungiRisorsaVicina
    {
        [XafDisplayName("non definito")]
        ND,
        [XafDisplayName(" a una Risorsa")]
        unaRisorsa,
        [XafDisplayName(" alla Risorsa e alle Risorse in Elenco")]
        RisorsaeElencoRisorse,
        [XafDisplayName(" a una delle Risorse in elenco: la piu Vicina altrimenti la piu Scarica")]
        UnaRisorsa_piuVicino_o_piuScarica,
        [XafDisplayName(" a una delle Risorse in elenco: la piu Scarica")]
        UnaRisorsa_piuScarica,
        [XafDisplayName(" a due delle Risorse in elenco: la piu Vicina e la piu Scarica")]
        dueRisorse_UnaPiuVicino_e_UnaPiuScarica,
        [XafDisplayName(" a due delle Risorse in elenco: le due piu Vicine altrimenti le due piu Scariche")]
        dueRisorse_DuePiuVicine_o_DuePiuScariche,
        [XafDisplayName(" a tre delle Risorse in elenco: le tre piu Vicine altrimenti le tre piu Scariche")]
        treRisorse_TrePiuVicine_o_TrePiuScariche,
        [XafDisplayName(" a una Risorse in Assegnazione Ottimizzata")]
        unaRisorsa_scarica_piuvicina_ottimizzata
    }
    //RiassegnazioneRisorsaVicina
    public enum RiassegnazioneRisorsa
    {
        [XafDisplayName("non definito")]
        ND,
        [XafDisplayName(" a una Risorsa")]
        unaRisorsa,
        [XafDisplayName(" alla Risorsa e alle Risorse in Elenco")]
        RisorsaeElencoRisorse,
        [XafDisplayName(" a una delle Risorse in elenco: la piu Vicina altrimenti la piu Scarica")]
        UnaRisorsa_piuVicino_o_piuScarica,
        [XafDisplayName(" a una delle Risorse in elenco: la piu Scarica")]
        UnaRisorsa_piuScarica,
        [XafDisplayName(" a due delle Risorse in elenco: la piu Vicina e la piu Scarica")]
        dueRisorse_UnaPiuVicino_e_UnaPiuScarica,
        [XafDisplayName(" a due delle Risorse in elenco: le due piu Vicine altrimenti le due piu Scariche")]
        dueRisorse_DuePiuVicine_o_DuePiuScariche,
        [XafDisplayName(" a tre delle Risorse in elenco: le tre piu Vicine altrimenti le tre piu Scariche")]
        treRisorse_TrePiuVicine_o_TrePiuScariche,
    }


    public enum TipoGiornoSettimana
    {
        [XafDisplayName("Domenica")]
        Domenica = 0,
        [XafDisplayName("Lunedi")]
        Lunedi = 1,
        [XafDisplayName("Martedi")]
        Martedi = 2,
        [XafDisplayName("Mercoledi")]
        Mercoledi = 3,
        [XafDisplayName("Giovedi")]
        Giovedi = 4,
        [XafDisplayName("Vernerdi")]
        Vernerdi = 5,
        [XafDisplayName("Sabato")]
        Sabato = 6
    }

    public enum TipoSelezioneGestioneOrarioGiornoSettimana
    {
    
        [XafDisplayName("Lunedi")]
        Lunedi = 0,
        [XafDisplayName("Martedi")]
        Martedi = 1,
        [XafDisplayName("Mercoledi")]
        Mercoledi = 2,
        [XafDisplayName("Giovedi")]
        Giovedi = 3,
        [XafDisplayName("Vernerdi")]
        Vernerdi = 4,
        [XafDisplayName("dal Lunedi al Venerdi")]
        Lunedi_Venerdi = 5,
        [XafDisplayName("Lunedi-Mercoledi-Vernerdi")]
        LunediMercolediVernerdi = 6,
        [XafDisplayName("Martedi-Giovedi")]
        MartediGiovedi = 7,
        [XafDisplayName("Sabato")]
        Sabato = 8,
        [XafDisplayName("Domenica")]
        Domenica = 9,
        [XafDisplayName("Sabato-Domenica")]
        SabatoDomenica = 10,
    }

    public enum TipoQualifica
    {
        [XafDisplayName("Operaio")]
        Operaio = 0,
        [XafDisplayName("Impiegato")]
        Impiegato = 1,
        [XafDisplayName("Operaio e Impiegato")]
        Operaio_Impiegato = 2,
        [XafDisplayName("Consulente")]
        Consulente = 3,
        [XafDisplayName("Dirigente")]
        Dirigente = 4,
        [XafDisplayName("Project Manager")]
        PM = 5,
        [XafDisplayName("Operation Manager")]
        OM = 6,
        [XafDisplayName("Assistente Manager")]
        AS = 7,
        [XafDisplayName("Generale")]
        Generale = 8
    }


    public enum TipoOggettoHTML
    {
        [XafDisplayName("Data")]
        Data = 0,
        [XafDisplayName("Ora")]
        Ora = 1
    }

    public enum TipoStatoConnessione
    {
        [XafDisplayName("Connessione non definita")]
        nd,
        [XafDisplayName("non Connesso")]
        nonConnesso,
        [XafDisplayName("Connesso non in Lavorazione")]
        Connesso,
        [XafDisplayName("Connesso in Lavorazione")]
        ConnessoinLavorazione,
        [XafDisplayName("Connesso in Pausa")]
        ConnessoinPausa,
        [XafDisplayName("Connesso in attività accessoria")]
        ConnessoinAttivitaAccessoria
    }
    public enum MostraSLA
    {
        [XafDisplayName("non Mostrare")]
        no,
        [XafDisplayName("SLA 1° Livello")]
        SLA1,
        [XafDisplayName("SLA 2° Livello")]
        SLA2
    }


    public enum StatoCEL
    {
        [XafDisplayName("ND")]
        nd,
        [XafDisplayName("Richiesto")]
        Richiesto,
        [XafDisplayName("Ricevuto")]
        Ricevuto

    }

    public enum TipoLavori
    {
        [XafDisplayName("ND")]
        nd,
        [XafDisplayName("Servizio")]
        Servizio,
        [XafDisplayName("Lavori")]
        Lavori

    }


    public enum TipoAcquisizione
    {
        [XafDisplayName("Non Definita")]
        nd,
        [XafDisplayName("Automatico")]
        Automatico,
        [XafDisplayName("Manuale")]
        Manuale

    }


    //public enum FilePopolamentoConforme
    //{

    //    [XafDisplayName("Conforme")]
    //    Conforme,
    //    [XafDisplayName("Non Conforme")]
    //    NonConforme

    //}

    public enum CategoriaSOA
    {
        [XafDisplayName("ND")]
        nd,
        [XafDisplayName("OG10")]
        OG10,
        [XafDisplayName("OS9")]
        OS9

    }

    public enum StatoElaborazioneJob
    {
        [XafDisplayName("Pianificato Esecutivo")]
        PianificatoExec,
        [XafDisplayName("Pianificato Simulazione")]
        PianificatoSimulazione,
        [XafDisplayName("Sospeso")]
        Sospeso,
        [XafDisplayName("in Esecuzione")]
        inEsecuzione,
        [XafDisplayName("Eseguito Simulazione")]
        EseguitoSimulazione,
        [XafDisplayName("Eseguito Esecutivo")]
        EseguitoExec,
        [XafDisplayName("In Modifica")]
        InModifica,
        [XafDisplayName("Bloccato")]
        Bloccato,
        [XafDisplayName("Fallito Simulazione")]
        FallitoSimulazione,
        [XafDisplayName("Fallito Esecutivo")]
        FallitoExec
    }
    //StepImportazione
    public enum StepImportazione
    {
        [XafDisplayName("Tutti")]
        Tutti,  // in automatico
        [XafDisplayName("Contratto")]
        Commessa,  // step di Manuale
        [XafDisplayName("Immobile")]
        Immobile,// step di Manuale
        [XafDisplayName("Impianto")]
        Impianto,// step di Manuale
        [XafDisplayName("Apparato")]
        Apparato// step di Manuale
    }


    public enum TipoVerificaStatica
    {
        [XafDisplayName("nd")]
        nd,
        [XafDisplayName("Escluso")]
        Escluso,
        [XafDisplayName("Pianificato")]
        Pianificato,
        [XafDisplayName("Eseguito")]
        Eseguito,
        [XafDisplayName("Eseguito con Prescrizione")]
        EseguitoPrescrizione,
    }


    public enum TipoNavigationItem
    {
        [XafDisplayName("nd")]
        nd,
        [XafDisplayName("Visualizza")]
        Visualizza,
        [XafDisplayName("Pagina Iniziale")]
        StartPage,
        [XafDisplayName("Nuovo Item")]
        NuovoItem,
        //[XafDisplayName("Eseguito con Prescrizione")]
        //EseguitoPrescrizione,
    }

    public enum TipoConfigurazione
    {
        [XafDisplayName("nd")]
        nd,
        [XafDisplayName("Navigation")]
        Navigation,
        [XafDisplayName("Azione")]
        Azione,
        [XafDisplayName("Nuovo Item")]
        NuovoItem,
        //[XafDisplayName("Eseguito con Prescrizione")]
        //EseguitoPrescrizione,
    }

    public enum TipoTiketOrari
    {
        [ImageName("State_Task_Completed")]
        [XafDisplayName("nd")]
        nd = 0,
        [ImageName("State_Task_Completed")]
        [XafDisplayName("Programmazione")]
        Programmazione = 0,
        [ImageName("State_Task_NotStarted")]
        [XafDisplayName("Variazione")]
        Variazione = 0
    }
    //public enum NomeGiornoOrari
    //{

    //    [XafDisplayName("Lunedi")]
    //    Lunedi = 0,
    //    [XafDisplayName("Martedi")]
    //    Martedi = 1,
    //    [XafDisplayName("Mercoledi")]
    //    Mercoledi = 2,
    //    [XafDisplayName("Giovedi")]
    //    Giovedi = 3,
    //    [XafDisplayName("Vernerdi")]
    //    Vernerdi = 4,
    //    [XafDisplayName("Sabato")]
    //    Sabato = 5,
    //    [XafDisplayName("Domenica")]
    //    Domenica = 6
    //}
    public enum TipoOrario
    {
        [XafDisplayName("Non Definito")]
        NonDefinito,
        [XafDisplayName("Ordinario")]
        Ordinario,
        [XafDisplayName("Reperibilità")]
        Reperibilita,
        [XafDisplayName("Sub Appalto")]
        SubAppalto,
        [XafDisplayName("Sub Appalto Reperibilità")]
        SubAppaltoReperibilita
    }
    public enum TipoTurnista
    {
        [XafDisplayName("Non Definito")]
        NonDefinito,   //-- lavora dal lun al venerdi - si aggiungono i zero dal lun al ven
        [XafDisplayName("Non Turnista")]
        NonTurnista,   //-- lavora dal lun al venerdi - si aggiungono i zero dal lun al ven
        [XafDisplayName("2 Turni")]
        Turni2,       //-- lavora su 2 turni quindi vale 480 x 2 -  si aggiungono i  zero dal lun al ven
        [XafDisplayName("3 Turni")]
        Turni3,      //-- lavora su 3 turni quindi vale 480 x 2 -  si aggiungono i zero dal lun al ven
        [XafDisplayName("Turnista")]
        Turnista,    //-- lavora dal lun al venerdi O SAB E DOM  - NON  si aggiungono i zero
        [XafDisplayName("Altro")]
        Altro         //-- lavora dal lun al venerdi O SAB E DOM  - NON  si aggiungono i zero
    }

    //[XafDisplayName("Non Turnista")]
    //NonTurnista,   //-- lavora dal lun al venerdi - si aggiungono i zero dal lun al ven
    //    [XafDisplayName("2 Turni")]
    //Turni2,       //-- lavora su 2 turni quindi vale 480 x 2 -  si aggiungono i  zero dal lun al ven
    //    [XafDisplayName("3 Turni")]
    //Turni3,      //-- lavora su 3 turni quindi vale 480 x 2 -  si aggiungono i zero dal lun al ven
    //    [XafDisplayName("Turnista")]
    //Turnista,    //-- lavora dal lun al venerdi O SAB E DOM  - NON  si aggiungono i zero
    //    [XafDisplayName("Altro")]
    //Altro         //-- lavora dal lun al venerdi O SAB E DOM  - NON  si aggiungono i zero


    public enum TipoBloccoDati
    {
        [XafDisplayName("Sbloccato")]
        Sbloccato,
        [XafDisplayName("Bloccato")]
        Bloccato,
        [XafDisplayName("Bloccato Solo Inserimento")]
        BloccatoSoloInserimento,
        [XafDisplayName("Bloccato Solo Modifica")]
        BloccatoSoloModifica
    }

    public enum TipoSetOrario
    {
        [XafDisplayName("--:--")]
        oxx_xx,
        [XafDisplayName("00:00")]
        o00_00,
        [XafDisplayName("00:30")]
        o00_30,
        [XafDisplayName("01:00")]
        o01_00,
        [XafDisplayName("01:30")]
        o01_30,
        [XafDisplayName("02:00")]
        o02_00,
        [XafDisplayName("02:30")]
        o02_30,
        [XafDisplayName("03:00")]
        o03_00,
        [XafDisplayName("03:30")]
        o03_30,
        [XafDisplayName("04:00")]
        o04_00,
        [XafDisplayName("04:30")]
        o04_30,
        [XafDisplayName("05:00")]
        o05_00,
        [XafDisplayName("05:30")]
        o05_30,
        [XafDisplayName("06:00")]
        o06_00,
        [XafDisplayName("06:30")]
        o06_30,
        [XafDisplayName("07:00")]
        o07_00,
        [XafDisplayName("07:30")]
        o07_30,
        [XafDisplayName("08:00")]
        o08_00,
        [XafDisplayName("08:30")]
        o08_30,
        [XafDisplayName("09:00")]
        o09_00,
        [XafDisplayName("09:30")]
        o09_30,
        [XafDisplayName("10:00")]
        o10_00,
        [XafDisplayName("10:30")]
        o10_30,
        [XafDisplayName("11:00")]
        o11_00,
        [XafDisplayName("11:30")]
        o11_30,
        [XafDisplayName("12:00")]
        o12_00,
        [XafDisplayName("12:30")]
        o12_30,
        [XafDisplayName("13:00")]
        o13_00,
        [XafDisplayName("13:30")]
        o13_30,
        [XafDisplayName("14:00")]
        o14_00,
        [XafDisplayName("14:30")]
        o14_30,
        [XafDisplayName("15:00")]
        o15_00,
        [XafDisplayName("15:30")]
        o15_30,
        [XafDisplayName("16:00")]
        o16_00,
        [XafDisplayName("16:30")]
        o16_30,
        [XafDisplayName("17:00")]
        o17_00,
        [XafDisplayName("17:30")]
        o17_30,
        [XafDisplayName("18:00")]
        o18_00,
        [XafDisplayName("18:30")]
        o18_30,
        [XafDisplayName("19:00")]
        o19_00,
        [XafDisplayName("19:30")]
        o19_30,
        [XafDisplayName("20:00")]
        o20_00,
        [XafDisplayName("20:30")]
        o20_30,
        [XafDisplayName("21:00")]
        o21_00,
        [XafDisplayName("21:30")]
        o21_30,
        [XafDisplayName("22:00")]
        o22_00,
        [XafDisplayName("22:30")]
        o22_30,
        [XafDisplayName("23:00")]
        o23_00,
        [XafDisplayName("23:30")]
        o23_30,
        [XafDisplayName("24:00")]
        o24_00

    }

}
