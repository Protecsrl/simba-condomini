
using CAMS.Module.Classi;
using CAMS.Module.Costi;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBDocument;
using CAMS.Module.DBPlanner;
using CAMS.Module.DBPlant;
using CAMS.Module.DBSpazi;
using CAMS.Module.DBTask.AppsCAMS;
using CAMS.Module.DBTask.Guasti;
using CAMS.Module.DBTask.Vista;
using CAMS.Module.PropertyEditors;
//using DevExpress.CodeRush.StructuralParser;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using CAMS.Module.DBPlant.Vista;

//1	In Attesa di Assegnazione  2	Assegnata  3	Emessa In lavorazione  4	Lavorazione Conclusa  5	Richiesta Chiusa 6	Sospesa SO
//7	Annullato 8	Rendicontazione Operativa 9	Rendicontazione Economica 10	In Emergenza da Assegnare 11	Gestione da Sala Operativa
namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("RDL")]  //[System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Richieste Di Lavoro")]
    [Indices("DataPianificata", "DataRichiesta", "DataCompletamento")]
    [ImageName("ShowTestReport")]
    [NavigationItem("Ticket")]
    //   per la RdL_DetailView     +++++++++++++++++++++++++++++++++++*********************************************  IN FASE DI CREAZIONE RDL  
    [Appearance("RdL.MostraRisorsa.Layout.Visible", AppearanceItemType.LayoutItem, @"Oid = -1",
             TargetItems = "panRisorsa;panCausaRimedio;panSmistamentoOperativo;DateOperative;panCompletamento;DataCreazione;UtenteCreatoRichiesta;CodiciRdLOdL", Context = "Any", Priority = 1, Visibility = ViewItemVisibility.Hide)]

    [Appearance("RdL.noEditabili.nero", TargetItems = "UtenteCreatoRichiesta;DataCreazione;CodiciRdLOdL", FontColor = "Black", Enabled = false)]

    //   per la RdL_DetailView     +++++++++++++++++++++++++++++++++++*********************************************  IN FASE DI SMISTAMENTO 10 MULTI TEAM 
    [Appearance("RdL.Emergenza.FontColor.Red", TargetItems = "*", FontColor = "Red", FontStyle = FontStyle.Bold, Priority = 2,
        Context = "Any", Criteria = "UltimoStatoSmistamento.Oid = 10")] //In Emergenza da Assegnare

    [Appearance("RdL.SSmistamento.Emergenza.noVisibile", TargetItems = "panCompletamento;panRisorsa;DateOperative",
         Context = "DetailView", Criteria = @"UltimoStatoSmistamento.Oid = 10", Visibility = ViewItemVisibility.Hide)]
    //  Context = "RdL_DetailView_Gestione;RdL_DetailView"                              DE
    //[Appearance("RdL.SSmistamento.Annullata.BlueDisable", TargetItems = "*",
    //     Context = "RdL_DetailView_Gestione;RdL_DetailView", //"RdL_DetailView_Gestione;RdL_DetailView",
    //     Criteria = @"UltimoStatoSmistamento.Oid = 7", FontColor = "Blue", Enabled = false)]

    // DETAIL VIEW       TUTTE                                            RisorseTeam

    //   per la RdL_DetailView     +++++++++++++++++++++++++++++++++++*********************************************  IN FASE DI IN ATTESA ASSEGNAZIONE E IN EMERGENZA MULTI TEAM  
    [Appearance("RdL.panRisorsa.noVisibile", AppearanceItemType.LayoutItem, @"Oid == -1 Or UltimoStatoSmistamento.Oid In(1,10)",
        TargetItems = "panCompletamento;panRisorsa", Visibility = ViewItemVisibility.Hide)]


    //   per la RdL_DetailView     +++++++++++++++++++++++++++++++++++*********************************************  IN TUTTE LE FSI IN CUI NON C'E' IL COMPLETAMENTO 
    [Appearance("RdL.panCompletamento.noVisibile", AppearanceItemType.LayoutItem,
        @"Oid == -1 Or UltimoStatoSmistamento.Oid In(2,3,6,7,11)", TargetItems = "panCompletamento", Visibility = ViewItemVisibility.Hide)]

    //   per la RdL_DetailView     *******************************************  IN TUTTE LE FSI DOVE è DIVERSO LA MANUTENZIONE GUASTO NON CI DEVE ESSERE IL panProblemaCausaRimedio
    [Appearance("RdL.ProblemaCausaRimedio.LayoutItemVisibility", AppearanceItemType.LayoutItem, @"Categoria.Oid != 4",
       TargetItems = "panProblemaCausaRimedio", Context = "DetailView", Visibility = ViewItemVisibility.Hide)]

    //[Appearance("RdL.inCreazione.noVisibile", TargetItems = "UltimoStatoSmistamento;CausaRimedio;RisorseTeam;RicercaRisorseTeam;DataAssegnazioneOdl;OdL;UltimoStatoOperativo;RegistroRdL;" +
    //"DataCompletamento;DataRiavvio;DataSopralluogo;DataAzioniTampone;DataInizioLavori;NoteCompletamento;Codice;CodOdL;CodRegRdL;CodiciRdLOdL;RdLUnivoco;RrdLTTMP",
    //           Criteria = @"Oid == -1", Visibility = ViewItemVisibility.Hide)]


    // solo quando è in statosmistamento gestione sala operativa si puo modulare stato operativo
    [Appearance("RdL.SOperativo.Enabled", TargetItems = "UltimoStatoOperativo",
        Criteria = @"not(UltimoStatoSmistamento.Oid In(11,4))", FontColor = "Black", Enabled = false)]

    [Appearance("RdL.RisorseTeam.enable_ss11", TargetItems = "RisorseTeam",
        Criteria = @"UltimoStatoSmistamento.Oid == 1 Or IsMP_count", Enabled = false)]   ///   ************  da migliorare
    // richiesta completata operativamente   //CAMS.Module.Classi.SetVarSessione.IsAdminRuolo
    [Appearance("RdL.SSmistamento.LavorazioneConclusa",
    TargetItems = "Richiedente;Impianto;Apparato;StdApparato;Urgenza;Categoria;TipoIntervento;Prob;Causa;Rimedio;Descrizione;" +
    "DataCreazione;DataRichiesta;UtenteCreatoRichiesta;DataAggiornamento;RisorseTeam;DataAssegnazioneOdl;OdL;RegistroRdL;" +
    "DataFermo;MpAttivitaPianificateDetts;RdLSollecitis;RdLApparatoSchedaMPs",  //   "DataFermo;ClusterEdifici;Scenario;CodOdL;CodRegRdL;MpAttivitaPianificateDetts;RdLSollecitis;RdLApparatoSchedaMPs",
    Criteria = @"UltimoStatoSmistamento.Oid = 4", FontColor = "Black", Enabled = false)]
    // And not(UserIsAdminRuolo)
    //Richiesta conclusa definitivamente
    [Appearance("RdL.SSmistamento.RichiestaChiusa", TargetItems = "*", Criteria = @"UltimoStatoSmistamento.Oid = 5",
                                                    FontColor = "Black", Enabled = false)]//CAMS.Module.Classi.SetVarSessione.IsAdminRuolo And not(UserIsAdminRuolo)
    [Appearance("RdL.Richiedente.LayoutItemVisibility", AppearanceItemType.LayoutItem,
         @"Categoria.Oid != 4",
        TargetItems = "PanRichiedente2;PanRichiedente1", Visibility = ViewItemVisibility.Hide)]

    [Appearance("RdL.GradoSoddisfazione.Visibility", TargetItems = "Soddisfazione",
    Criteria = @"Iif(Immobile is null,true,not(Immobile.Commesse.MostraSoddisfazioneCliente))", Visibility = ViewItemVisibility.Hide)]  //  nrRdL


    //    dataarrivo_autorizzativo

    [Appearance("RdL.dataarrivo_autorizzativo.Autorizzazione.visible", AppearanceItemType.LayoutItem,
        @"Iif(StatoAutorizzativo is null,true,Iif(StatoAutorizzativo.Oid = 0,true,false))", TargetItems = "dataarrivo_autorizzativo",
 Visibility = ViewItemVisibility.Hide)]  //  nrRdL

    #region


    [Appearance("RdL.DateOperativeNulle.Guasto.Layout.Visible", AppearanceItemType.LayoutItem,
    @"Categoria.Oid In (1,2,5)",  ////******************
    TargetItems = "DateOperative", //Priority = 1,
                                   //  Context = "RdL_DetailView_Gestione;RdL_DetailView", Priority = 1,
    Visibility = ViewItemVisibility.Hide)]



    [Appearance("RdL.RichiedenteOpzioni.LayoutGuasto.Visible", AppearanceItemType.LayoutItem,
        @"Iif(Immobile is null,true,false) Or Categoria.Oid In (0,1,2,5) Or Immobile.Commesse.AbilitaRichiedenteOpzioni = false",
             TargetItems = "panRichiedenteOpzioni", Context = "Any", Priority = 1, Visibility = ViewItemVisibility.Hide)]

    #region VISIBILE L DATE  OPERATIVE
    [Appearance("RdL.DateOperative.DataRiavvio", TargetItems = "DataRiavvio;DataFermo",
        Criteria = @"Categoria.Oid != 4 Or (not(RegistroRdL.MostraDataOraFermo) And UltimoStatoSmistamento.Oid != 4)",
                                                                                 Visibility = ViewItemVisibility.Hide)]
    //Or (SopralluogoEseguito != 'Si' )
    [Appearance("RdL.DateOperative.SopralluogoEseguito.Visibility", TargetItems = "SopralluogoEseguito",
        Criteria = @"Categoria.Oid != 4 Or not(RegistroRdL.MostraDataOraSopralluogo) Or not(UltimoStatoSmistamento.Oid In(2,3,11,4))", Enabled = false, FontColor = "Black")]
    //modificato Or not(UltimoStatoSmistamento.Oid In(2,3,11) in not(UltimoStatoSmistamento.Oid In(2,3,11,4)
    [Appearance("RdL.DateOperative.SopralluogoEseguito.enabled", TargetItems = "SopralluogoEseguito",
        Criteria = @"Categoria.Oid != 4 Or not(RegistroRdL.MostraDataOraSopralluogo) Or UltimoStatoSmistamento.Oid = 1", Visibility = ViewItemVisibility.Hide)]

    [Appearance("RdL.DateOperative.DataSopralluogo", TargetItems = "DataSopralluogo",
        Criteria = @"Categoria.Oid != 4 Or not(RegistroRdL.MostraDataOraSopralluogo) Or SopralluogoEseguito != 'Si'", Visibility = ViewItemVisibility.Hide)]

    [Appearance("RdL.DateOperative.DataInizioLavori", TargetItems = "DataInizioLavori",
       Criteria = @"Categoria.Oid != 4 Or UltimoStatoSmistamento.Oid in (1,10) Or not(RegistroRdL.MostraDataOraInizioLavori) Or SopralluogoEseguito != 'Si'", Visibility = ViewItemVisibility.Hide)]

    [Appearance("RdL.DateOperative.DataAzioniTampone", TargetItems = "DataAzioniTampone",
        Criteria = @"Categoria.Oid != 4 Or UltimoStatoSmistamento.Oid in (1,10) Or not(RegistroRdL.MostraDataOraAzioniTampone) Or SopralluogoEseguito != 'Si'", Visibility = ViewItemVisibility.Hide)]
    //[Appearance("RdL.DateOperative.DataInizioLavori", TargetItems = "DataInizioLavori",
    //    Criteria = @"Categoria.Oid != 4 Or UltimoStatoSmistamento.Oid in (1,2,10) Or not(RegistroRdL.MostraDataOraInizioLavori)", Visibility = ViewItemVisibility.Hide)]

    //  diverso da 4 Lavorazione Conclusa                                                ****************************************************************
    [Appearance("RdL.DateOperative.DataCompletamento.Visibile", AppearanceItemType.LayoutItem,
                                                         @"Oid=-1 And UltimoStatoSmistamento.Oid != 4", TargetItems = "panDataCompletamentoSoddisfazione",
                                                          Visibility = ViewItemVisibility.Hide)]
    #endregion

    [Appearance("RdL.MostraPianiLocali.Layout.Visible", AppearanceItemType.LayoutItem, @"Iif(Immobile is null,false,[Immobile.Commesse.MostraPianiLocali]==0)",
             TargetItems = "panPianoLocale", Context = "Any", Priority = 1, Visibility = ViewItemVisibility.Hide)]


    [Appearance("RuleInfo.RdL.DataPianificata.Rosso", AppearanceItemType.ViewItem,
              @"[DataPianificata] <= [DataNow] And [UltimoStatoSmistamento.Oid] In(1,2)", TargetItems = "DataPianificata", FontColor = "Red")]


    [Appearance("RuleInfo.RdL.noModificaMPcount.Rosso", AppearanceItemType.ViewItem,
       @"IsMP_count", TargetItems = "RisorseTeam;UltimoStatoSmistamento;UltimoStatoOperativo", Priority = 1, Enabled = false, FontColor = "Red")]

    // apparete per intecenter   Ruolo
    [Appearance("RuleInfo.RdL.intercenter.nonVisibile", AppearanceItemType.LayoutItem,
       @"RuoloIntercenter", TargetItems = "Note;DataRiavvio;DataFermo;DateOperative;panSmistamentoOperativo;RdLNotes;Documentis", Priority = 1, Context = "RdL_DetailView", Visibility = ViewItemVisibility.Hide)]


    //Oid

    #endregion
    #region validazioni stato operativo
    [RuleCriteria("RC.RdL.Valida.Assegna", DefaultContexts.Save, @"[UltimoStatoSmistamento.Oid] = 2 And [RisorseTeam] Is Null",
    CustomMessageTemplate = "Selezionare la Risorsa di Assegnazione Intervento!",
    SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    [RuleCriteria("RC.RdL.Valida.operation", DefaultContexts.Save, @"[UltimoStatoSmistamento.Oid] = 11 And [RisorseTeam] Is Null",
    CustomMessageTemplate = "Selezionare la Risorsa di Assegnazione Intervento!",
    SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    [RuleCriteria("RC.RdL.Valida.Completa", DefaultContexts.Save, @"[UltimoStatoSmistamento.Oid] = 4 And [DataCompletamento] Is Null",
    CustomMessageTemplate = "Per Completare la Lavorazione è necessario impostare la Data di Completamento!",
    SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    [RuleCriteria("RC.RdL.Valida.Completa.noProgrammata", DefaultContexts.Save, @"[UltimoStatoSmistamento.Oid] = 4 And [Categoria.Oid] <> 1 And
    DateDiffMinute(DataRichiesta,DataCompletamento) < 0",
   CustomMessageTemplate = "Per Completare la Lavorazione è necessario impostare la Data di Completamento maggiore della Data Richiesta!",
   SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]
    //[DataCompletamento] < [DataRichiesta]", sostituito da DateDiffMinute(DataCompletamento, DataRichiesta) > 0"
    //DateDiffMinute(startDate, endDate)

    [RuleCriteria("RuleInfo.RdL.SKMpNonAssegnate", DefaultContexts.Save, @"(Categoria.Oid In(3,5,2) And RdLApparatoSchedaMPs.Count() = 0)",
CustomMessageTemplate = "Informazione: Richiesta di Lavoro di tipo ({Categoria}) Senza Selezione di Procdure di Attività di Manutenzione, sei sicuro di voler Salvare?.",
SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Information)]
    #endregion

    #region regole date operative
    //    [RuleCriteria("RuleInfo.RdL.DataSopralluogo", DefaultContexts.Save, @"[DataRichiesta] <= [DataSopralluogo]",
    //CustomMessageTemplate = "Informazione: La Data di Sopralluogo ({DataSopralluogo}) deve essere maggiore della data di Richiesta({DataRichiesta}).",
    //SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Information)]

    //    [RuleCriteria("RuleInfo.RdL.DataInizioLavori", DefaultContexts.Save, @"[DataRichiesta] <= DataInizioLavori",
    //CustomMessageTemplate = "Informazione: La Data di InizioLavori RdL ({DataInizioLavori}) deve essere maggiore della data di Richiesta({DataRichiesta}).",
    //SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Information)]

    [RuleCriteria("RuleInfo.RdL.DataPianificata", DefaultContexts.Save
            , @"[DataPianificata] <= [DataNow] And Categoria.Oid = 4 And [UltimoStatoSmistamento.Oid] = 2 And [UltimoStatoSmistamento.Oid] != old_SSmistamento_Oid",
    CustomMessageTemplate = "Informazione: La Data Pianificata ({DataPianificata}) deve essere maggiore della data attuale ({DataNow}).",
    SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Information)]

    [Appearance("RuleInfo.RdL.DataPianificataEnd.Rosso", AppearanceItemType.ViewItem,
           @"[DataPianificataEnd] <= [DataPianificata] And [UltimoStatoSmistamento.Oid] In(1,2)", TargetItems = "DataPianificataEnd", FontColor = "Red")]

    //  sopra le regole precedenti ao 02.08.2019  --> sotto le regole successive al 02.08.2919
    // questa è valida per le editor properti
    //    [RuleCriteria("RuleInfo.RdL.DataSopralluogo-DataAzioniTampone", DefaultContexts.Save,
    //        @"[DataRichiesta] <= [DataSopralluogo] Or [DataRichiesta] <= [DataAzioniTampone] Or [DataRichiesta] <= [DataCompletamento]",
    //CustomMessageTemplate = "Informazione: La Date Operative devono essere maggiori della data di Richiesta({DataRichiesta}).",
    //SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Warning)]

    //this.fDataSopralluogoVisibile = false;
    //  this.fDataAzioniTamponeVisibile = false;   
    //  this.fDataInizioLavoriVisibile = false;


    // data fermo e riavvio

    // data sopralluogo       SopralluogoEseguito != 'NonNecessario' And 
    //[Appearance("RdL.Visibility.DataSopralluogo", TargetItems = "DataSopralluogo;SopralluogoEseguito", Criteria = @"!DataSopralluogoVisibile", Visibility = ViewItemVisibility.Hide)]

    [RuleCriteria("RuleInfo.RdL.Warning.DataSopralluogo-DataAzioniTampone", DefaultContexts.Save,
        @"SopralluogoEseguito != 'NonNecessario' And DataSopralluogoVisibile And DataAzioniTamponeVisibile  And DateDiffMinute([DataSopralluogo], [DataAzioniTampone])<0",
CustomMessageTemplate = "Informazione: La Data di Sopralluogo ({DataSopralluogo}) deve essere minore o uguale di Data Intervento Sicurezza ).",
SkipNullOrEmptyValues = true, InvertResult = true, ResultType = ValidationResultType.Error)]
    // DateDiffMinute([DataSopralluogo], [DataAzioniTampone])<0 --condizione originale And ([DataSopralluogo] > [DataAzioniTampone])
    [RuleCriteria("RuleInfo.RdL.Warning.DataSopralluogo-DataInizioLavori", DefaultContexts.Save,
        @"SopralluogoEseguito != 'NonNecessario' And DataSopralluogoVisibile And DataInizioLavoriVisibile  And DateDiffMinute([DataSopralluogo], [DataInizioLavori])<0",
CustomMessageTemplate = "Informazione: La Data di Sopralluogo ({DataSopralluogo}) deve essere minore o uguale di Data Inizio Lavori).",
SkipNullOrEmptyValues = true, InvertResult = true, ResultType = ValidationResultType.Error)]
    // And (DateDiffMinute([DataSopralluogo], [DataInizioLavori])<0  --condizione originale And ([DataSopralluogo] > [DataInizioLavori] 

    [Appearance("RdL.Visibility.DataAzioniTampone", TargetItems = "DataAzioniTampone", Criteria = @"!DataAzioniTamponeVisibile", Visibility = ViewItemVisibility.Hide)]
    [RuleCriteria("RuleInfo.RdL.Warning.DataAzioniTampone-DataInizioLavori", DefaultContexts.Save,
        @"SopralluogoEseguito != 'NonNecessario' And DataAzioniTamponeVisibile And DataInizioLavoriVisibile And DateDiffMinute([DataAzioniTampone], [DataInizioLavori])<0",
CustomMessageTemplate = "Informazione: La Data di Messa in Sicurezza ({DataAzioniTampone}) deve essere maggiore o uguale a Data Sopralluogo e minore o uguale a Data Inizio Lavori).",
SkipNullOrEmptyValues = true, InvertResult = true, ResultType = ValidationResultType.Error)]
    // And (DateDiffMinute([DataAzioniTampone], [DataInizioLavori])<0  --condizione originale And ([DataAzioniTampone] > [DataInizioLavori]) 

    // data inizio lavori
    //    [Appearance("RdL.Visibility.DataInizioLavori", TargetItems = "DataInizioLavori", Criteria = @"!DataInizioLavoriVisibile", Visibility = ViewItemVisibility.Hide)]
    //    [RuleCriteria("RuleInfo.RdL.Warning.DataInizioLavori", DefaultContexts.Save,
    //        @"DataInizioLavoriVisibile And ([DataSopralluogo] > [DataInizioLavori] Or [DataAzioniTampone] > [DataInizioLavori])",
    //CustomMessageTemplate = "Informazione: La Data di Inizio Lavori ({DataInizioLavori}) deve essere uguale o maggiore delle date di Sopralluogo e messa in sicurezza).",
    //SkipNullOrEmptyValues = true, InvertResult = true, ResultType = ValidationResultType.Warning)]

    // data COMPLETAMENTO lavori


    //    [RuleCriteria("RuleInfo.RdL.Warning.DataCompletamento-DataSopralluogoVisibile", DefaultContexts.Save,
    //                @"SopralluogoEseguito != 'NonNecessario' And DataSopralluogoVisibile And ([DataSopralluogo] > [DataCompletamento] Or SopralluogoEseguito != 'Si')",
    //CustomMessageTemplate = "Informazione: La Data Completamento ({DataCompletamento}) deve essere maggiore della data Sopralluogo ({DataSopralluogo}) ed quindi eseguito Sopralluogo su SI .",
    //SkipNullOrEmptyValues = true, InvertResult = true, ResultType = ValidationResultType.Warning)]

    //inizio aggiunti il 14/07/2020
    [RuleCriteria("RuleInfo.RdL.Warning.DataCompletamento-DataSopralluogoVisibile_n01", DefaultContexts.Save,
                @"SopralluogoEseguito = 'Si' And DataSopralluogoVisibile And [DataSopralluogo] > [DataCompletamento] And [UltimoStatoSmistamento.Oid] = 4 And [Categoria.Oid] <> 1",
CustomMessageTemplate = "Informazione: La Data Completamento ({DataCompletamento}) deve essere maggiore della data Sopralluogo ({DataSopralluogo})",
SkipNullOrEmptyValues = true, InvertResult = true, ResultType = ValidationResultType.Warning)]

    [RuleCriteria("RuleInfo.RdL.Warning.DataCompletamento-DataSopralluogoVisibile_n02", DefaultContexts.Save,
                @"SopralluogoEseguito = 'No' And DataSopralluogoVisibile And [UltimoStatoSmistamento.Oid] = 4 And [Categoria.Oid] <> 1",
CustomMessageTemplate = "Informazione: Impostare SopralluogoEseguito a SI oppure impostare SopralluogoEseguito = 'Non Necessario",
SkipNullOrEmptyValues = true, InvertResult = true, ResultType = ValidationResultType.Error)]

    //fine aggiunti il 14/07/2020

    [RuleCriteria("RuleInfo.RdL.Warning.DataCompletamento-DataAzioniTamponeVisibile", DefaultContexts.Save,
                @"SopralluogoEseguito != 'NonNecessario' And DataAzioniTamponeVisibile And ([DataAzioniTampone] > [DataCompletamento])",
CustomMessageTemplate = "Informazione: La Data Completamento ({DataCompletamento}) deve essere maggiore della data di messa in sicurezza ({DataAzioniTampone}).",
SkipNullOrEmptyValues = true, InvertResult = true, ResultType = ValidationResultType.Warning)]

    [RuleCriteria("RuleInfo.RdL.Warning.DataCompletamento-DataInizioLavoriVisibile", DefaultContexts.Save,
         @"SopralluogoEseguito != 'NonNecessario' And DataInizioLavoriVisibile And ([DataInizioLavori] > [DataCompletamento])",
CustomMessageTemplate = "Informazione: La Data Completamento ({DataCompletamento}) deve essere maggiore della data inizio lavori ({DataInizioLavori}).",
SkipNullOrEmptyValues = true, InvertResult = true, ResultType = ValidationResultType.Warning)]


    //  [RuleValueComparison("", DefaultContexts.Save, ValueComparisonType.GreaterThan,
    //  "OrderDateTime", ParametersMode.Expression)]
    //  [RuleFromBoolProperty("ContactUniqueEmail", DefaultContexts.Save,
    //"Contact with this Email already exists", UsedProperties = "Email")]

    #endregion
    #region filtro tampone
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "All Data", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Categoria.manprog", "Categoria.Oid = 1", "Manutenzione Programmata", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Categoria.conduzione", "Categoria.Oid = 2", "Conduzione", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Categoria.mancond", "Categoria.Oid = 3", "Manutenzione A Condizione", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Categoria.mangu", "Categoria.Oid = 4", "Manutenzione Guasto", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Categoria.manprogspot", "Categoria.Oid = 5", "Manutenzione Programmata Spot", Index = 5)]
    // [Appearance("BoldDetailView", AppearanceItemType = "LayoutItem", TargetItems = "*", Context = "BusinessGoals_DetailView", FontStyle = FontStyle.Bold)]
    // [DevExpress.ExpressApp.SystemModule.ListViewFilter("Open Goals", "dtDeleted is null", true)] --ListViewFilter("Deleted Goals", "dtDeleted is not null")] [ListViewFilter("All Goals", "")]

    #endregion


    public class RdL : XPObject
    {// private const int GiorniRitardoRicerca = -7;
        public RdL() : base() { }
        public RdL(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this != null)
                if (this.Oid == -1) // crea RdL
                {

                    if (fUtenteCreatoRichiesta == null)
                        fUtenteCreatoRichiesta = SecuritySystem.CurrentUserName;
                    //this.IsMP_count
                    StatoAutorizzativo = Session.GetObjectByKey<StatoAutorizzativo>(0);
                    fSopralluogoEseguito = Fatto.No;
                }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (this != null)
            {
                this.IsMP_count = false;
                int numrdl = 0;
                if (this.RegistroRdL != null && this.Categoria != null && this.Categoria.Oid == 1)
                    numrdl = this.RegistroRdL.RdLes.Count;

                #region scheduler
                if (numrdl > 1 && this.Categoria.Oid == 1 && !Session.IsNewObject(this))
                {
                    this.IsMP_count = true;
                }
                #endregion

                //this.slaSopralluogo = string.Empty;
                //this.slaRipristino = string.Empty;
                this.fDataSopralluogoVisibile = false;
                this.fDataAzioniTamponeVisibile = false;
                this.fDataInizioLavoriVisibile = false;
                this.fVisualizzaSLA = SetVarSessione.VisualizzaSLA;
                this.VisualizzaSLA = SetVarSessione.VisualizzaSLA;
                if (this.Categoria != null && this.Categoria.Oid == 4 && this.Oid > 0 && this.RegistroRdL != null)
                {
                    if (SetVarSessione.VisualizzaSLA)
                    {
                        RdLListViewGuasto RdLListViewGuasto = Session.GetObjectByKey<RdLListViewGuasto>(this.Oid);
                        if (RdLListViewGuasto != null)
                        {
                            if (string.IsNullOrEmpty(this.slaSopralluogo))
                            {
                                this.slaSopralluogo = RdLListViewGuasto.SLASopralluogo; // DateTime.Now.ToLongDateString();
                            }
                            if (string.IsNullOrEmpty(this.slaRipristino))
                            {
                                this.slaRipristino = RdLListViewGuasto.SLARipristino; // DateTime.Now.ToLongDateString();
                            }
                        }
                    }

                    //Commesse com = this.Immobile.Commesse;
                    this.fDataSopralluogoVisibile = this.RegistroRdL.MostraDataOraSopralluogo; //com.MostraDataOraSopralluogo;
                    this.fDataAzioniTamponeVisibile = this.RegistroRdL.MostraDataOraAzioniTampone; //com.MostraDataOraAzioniTampone;
                    this.fDataInizioLavoriVisibile = this.RegistroRdL.MostraDataOraInizioLavori;// com.MostraDataOraInizioLavori;
                }
            }
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        //private Richiedente fRichiedente;
        [Persistent("RICHIEDENTE"), System.ComponentModel.DisplayName("Richiedente")]
        [DataSourceCriteria("Commesse.Oid = '@This.Immobile.Commesse.Oid'")]
        [RuleRequiredField("RdL.Rihiedente.Obblig.su.Guasto", DefaultContexts.Save, SkipNullOrEmptyValues = false, TargetCriteria = "[Categoria.Oid] == 4")]
        [Appearance("RdL.Abilita.Richiedente", Criteria = "Immobile is null Or [UltimoStatoSmistamento.Oid] > 2", FontColor = "Black", Enabled = false)]
        [Appearance("RdL.Richiedente.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Richiedente)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public Richiedente Richiedente
        {
            get
            {
                return GetDelayedPropertyValue<Richiedente>("Richiedente");
            }
            set
            {
                SetDelayedPropertyValue<Richiedente>("Richiedente", value);
            }
        }

        #region RICHIEDENTE     -     invia Mail + SMS


        //private TipologiaSpedizioneRdL fTipologiaSpedizione;
        [Persistent("TIPOSPEDIZIONE"), DevExpress.Xpo.DisplayName("Tipo Spedizione")]
        //[DataSourceCriteria("Commesse = '@This.Immobile.Commesse'")]
        [ImmediatePostData(true)]
        [Appearance("RdL.Abilita.TipologiaSpedizioneRdL", Criteria = "Richiedente  is null", FontColor = "Black", Enabled = false)]
        [Delayed(true)]
        public TipologiaSpedizioneRdL TipologiaSpedizione
        {
            get { return GetDelayedPropertyValue<TipologiaSpedizioneRdL>("TipologiaSpedizione"); }
            set { SetDelayedPropertyValue<TipologiaSpedizioneRdL>("TipologiaSpedizione", value); }
        }

        //private string fEmail;
        // private const string UrlStringEditMask = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,3})$";
        private const string UrlStringEditMask = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        //private const string                                    RuleRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        [RuleRegularExpression("rdl.Email_RuleRegularExpression", DefaultContexts.Save,
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
            , CustomMessageTemplate = "Attenzione inserire un corretto indirizzo mail(nome.cognome@dominio.com oppure nomecognome@dominio.xxx).")]
        [ToolTip("Inserire una email, con il seguente formato: nome.cognome@dominio.com oppure nomecognome@dominio.xxx", null)]
        [Size(100), Persistent("DESEMAIL"), DevExpress.Xpo.DisplayName("Email")]
        [DbType("varchar(100)")]
        [VisibleInListView(false), VisibleInDashboards(false)]
        [Appearance("RdL.TipologiaSpedRdL.BrownEmail", AppearanceItemType.LayoutItem, "TipologiaSpedizione == 'MAIL' Or TipologiaSpedizione == 'MAILSMS'", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RdL.TipologiaSpedRdL.EnabledEmail", Criteria = "TipologiaSpedizione == 'No' Or TipologiaSpedizione == 'SMS'", Enabled = false)]//FontColor = "Black",
        [Delayed(true)]
        public string Email
        {
            get { return GetDelayedPropertyValue<string>("Email"); }
            set { SetDelayedPropertyValue<string>("Email", value); }
        }


        //private string _PhoneString;
        private const string PhoneStringEditMask = "(0000)000-0000009";
        [DevExpress.ExpressApp.Model.ModelDefault("EditMaskType", "Simple")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + PhoneStringEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", PhoneStringEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("indicare il numero telefonico del mobile che riceve l'SMS, : " + PhoneStringEditMask, null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Size(20), Persistent("TELEFONO"), DevExpress.Xpo.DisplayName("Telefono Mobile")]
        [DbType("varchar(20)")]
        [VisibleInListView(false)]
        [Appearance("RdL.TipologiaSpedRdL.Brown.PhoneString", AppearanceItemType.LayoutItem, "TipologiaSpedizione == 'SMS' Or TipologiaSpedizione == 'MAILSMS'", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RdL.TipologiaSpedRdL.Enabled.PhoneString", Criteria = "TipologiaSpedizione == 'No' Or TipologiaSpedizione == 'MAIL'", Enabled = false)]//FontColor = "Black",
        [Delayed(true)]
        public string PhoneString
        {
            get { return GetDelayedPropertyValue<string>("PhoneString"); }
            set { SetDelayedPropertyValue<string>("PhoneString", value); }
        }




        #endregion

        #region Immobile
        private Immobile fImmobile;
        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        [VisibleInListView(false)]
        [Appearance("RdL.Immobile.FontColor.Black", FontColor = "Black")]
        [RuleRequiredField("RuleReq.RdL.Immobile", DefaultContexts.Save, "Immobile è un campo obbligatorio")]
        [Appearance("RdL.Immobile.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Immobile)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ImmediatePostData(true)]
        [Delayed(true)]
        public Immobile Immobile
        {
            get
            {
                return GetDelayedPropertyValue<Immobile>("Immobile");
            }
            set
            {
                SetDelayedPropertyValue<Immobile>("Immobile", value);
            }
        }

        [NonPersistent, Size(25)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false), VisibleInDashboards(false), VisibleInReports(false)]
        public string RicercaImmobile
        {
            get;
            set;
        }

        [NonPersistent, Size(25)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false), VisibleInDashboards(false), VisibleInReports(false)]
        public string RicercaApparato
        {
            get;
            set;
        }

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDashboards(false), VisibleInReports(false)]
        [PersistentAlias("Iif(Immobile is not null, Immobile.Note,null)"), DevExpress.Xpo.DisplayName("Note Immobile")]
        [Appearance("RdL.ImmobileNote.Black", Criteria = "not (ImmobileNote  is null)", FontColor = "Blue")]
        [Appearance("RdL.ImmobileNote.Italic", FontStyle = FontStyle.Italic)]
        public string ImmobileNote
        {
            get
            {
                if (this == null)
                    return null;

                if (this.Immobile == null) return null;
                return string.Format("{0}", this.Immobile.Note);
            }
        }


        [PersistentAlias("Iif(Immobile is not null, Immobile.RifReperibile + '('+ Immobile.Commesse.ReferenteCofely.NomeCognome + ', ' + Immobile.Commesse.ReferenteCofely.Telefono + ')',null)"), DevExpress.Xpo.DisplayName("Riferimenti Reperibile Immobile")]
        [Appearance("RdL.ImmobileRifReperibile.Black", Criteria = "not (ImmobileRifReperibile  is null)", FontColor = "Blue")]
        [Size(4000)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDashboards(false), VisibleInReports(false)]
        [Appearance("RdL.ImmobileRifReperibile.Italic", FontStyle = FontStyle.Italic)]
        public string ImmobileRifReperibile
        {
            get
            {
                if (this == null)
                    return null;

                if (this.Immobile == null) return null;
                else
                {
                    if (this.Immobile.Contratti != null)
                    {
                        if (this.Immobile.Contratti.ReferenteContratto != null)
                            return string.Format("Reperibile:{0}, PM:({1}, {2})", this.Immobile.RifReperibile, Immobile.Contratti.ReferenteContratto.NomeCognome, Immobile.Contratti.ReferenteContratto.Telefono);
                        else
                            return string.Format("Reperibile:{0}", this.Immobile.RifReperibile);
                    }
                    else
                        return string.Format("Reperibile:{0}", this.Immobile.RifReperibile);
                }

            }
        }

        private string slaSopralluogo;
        [NonPersistent, Size(100)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false), VisibleInDashboards(false), VisibleInReports(false)]
        [Appearance("RdL.SLASopralluogo.BackColor.Red1", BackColor = "Green", FontColor = "Black", Priority = 1, Criteria = "Contains([SLASopralluogo],'(1)')")]
        [Appearance("RdL.SLASopralluogo.BackColor.Red2", BackColor = "Yellow", FontColor = "Black", Priority = 2, Criteria = "Contains([SLASopralluogo],'(2)')")]
        [Appearance("RdL.SLASopralluogo.BackColor.Red3", BackColor = "Salmon", FontColor = "Black", Priority = 3, Criteria = "Contains([SLASopralluogo],'(3)')")]
        [Appearance("RdL.SLASopralluogo.BackColor.Red4", BackColor = "Red", FontColor = "Black", Priority = 4, Criteria = "Contains([SLASopralluogo],'(4)')")]
        public string SLASopralluogo
        {
            get { return slaSopralluogo; }
            set { SetPropertyValue("SLASopralluogo", ref slaSopralluogo, value); }
        }

        private string slaRipristino;
        [NonPersistent, Size(100)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false), VisibleInDashboards(false), VisibleInReports(false)]
        [Appearance("RdL.SLARipristino.BackColor.Red1", BackColor = "Green", FontColor = "Black", Priority = 1, Criteria = "Contains([SLARipristino],'(1)')")]
        [Appearance("RdL.SLARipristino.BackColor.Red2", BackColor = "Yellow", FontColor = "Black", Priority = 2, Criteria = "Contains([SLARipristino],'(2)')")]
        [Appearance("RdL.SLARipristino.BackColor.Red3", BackColor = "Salmon", FontColor = "Black", Priority = 3, Criteria = "Contains([SLARipristino],'(3)')")]
        [Appearance("RdL.SLARipristino.BackColor.Red4", BackColor = "Red", FontColor = "Black", Priority = 4, Criteria = "Contains([SLARipristino],'(4)')")]
        public string SLARipristino
        {
            get { return slaRipristino; }
            set { SetPropertyValue("SLARipristino", ref slaRipristino, value); }
        }

        private bool fVisualizzaSLA;
        [NonPersistent]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false), VisibleInDashboards(false), VisibleInReports(false)]
        public bool VisualizzaSLA
        {
            get { return fVisualizzaSLA; }
            set { SetPropertyValue("VisualizzaSLA", ref fVisualizzaSLA, value); }
        }

        #endregion

        #region Piano e stanza
        private Piani fPiano;
        [Persistent("PIANO"), System.ComponentModel.DisplayName("Piano")]
        [Appearance("RdL.Abilita.Piano", FontColor = "Black", Criteria = "Oid > 0 Or IsNullOrEmpty(Immobile) Or !IsNullOrEmpty(Locale)", Enabled = false)]
        [DataSourceCriteria("Immobile.Oid = '@This.Immobile.Oid'")]// QUESTO SIGNIFICA CHE TI DA SOLO I PIANI CHE SONO DELL'Immobile SELEZIONATO
        [ExplicitLoading()]
        [ImmediatePostData(true)]
        [Delayed(true)]
        public Piani Piano
        {
            get
            {
                return GetDelayedPropertyValue<Piani>("Piano");
            }
            set
            {
                SetDelayedPropertyValue<Piani>("Piano", value);
            }
        }

        //   Association(@"LOCALI_APPARATO"),
        private Locali fLocale;
        [Persistent("LOCALI"), DevExpress.Xpo.DisplayName("Locali")]
        [Appearance("RdL.Locali.FontColor.Black", FontColor = "Black")]
        [Appearance("RdL.Locali", Criteria = "IsNullOrEmpty(Piano)", Context = "DetailView", Enabled = false)]
        [DataSourceCriteria("Piano.Oid = '@This.Piano.Oid'")]// su rdl si chiama piano, su locali piani
        [ExplicitLoading()]
        [ImmediatePostData(true)]
        [Delayed(true)]
        public Locali Locale
        {
            get { return GetDelayedPropertyValue<Locali>("Locale"); }
            set { SetDelayedPropertyValue<Locali>("Locale", value); }

            //get { return fLocale; }
            //set { SetPropertyValue<Locali>("Locale", ref fLocale, value); }
        }

        [NonPersistent, Size(25)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string RicercaLocale
        {
            get;
            set;
        }

        private string fReparto;
        [Size(250), Persistent("REPARTO"), System.ComponentModel.DisplayName("Reparto")]
        [DbType("varchar(250)")]
        [VisibleInListView(false)]
        [VisibleInDetailView(true)]
        [Delayed(true)]
        public string Reparto
        {
            get { return GetDelayedPropertyValue<string>("Reparto"); }
            set { SetDelayedPropertyValue<string>("Reparto", value); }
        }

        #endregion


        private Servizio fServizio;
        [Persistent("SERVIZIO"), System.ComponentModel.DisplayName("Impianto")]
        [Appearance("RdL.Abilita.Servizio", Criteria = "(Immobile  is null) OR (not (StdApparato is null))", FontColor = "Black", Enabled = false)]
        [RuleRequiredField("RuleReq.RdL.Servizio", DefaultContexts.Save, "Impianto è un campo obbligatorio")]
        [Appearance("RdL.Servizio.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Impianto)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Immobile.Oid = '@This.Immobile.Oid'")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Servizio Servizio
        {
            get
            {
                return GetDelayedPropertyValue<Servizio>("Servizio");
            }
            set
            {
                SetDelayedPropertyValue<Servizio>("Servizio", value);
            }
        }

        private Asset fApparato;  ///[UltimoStatoSmistamento.Oid] > 1
        [Persistent("ASSET"), System.ComponentModel.DisplayName("Asset")]
        [Appearance("RdL.Asset.FontColor.Black", FontColor = "Black")]
        [Appearance("RdL.Abilita.Apparato", Criteria = "(Impianto is null) OR (not (Prob is null)) Or  [UltimoStatoSmistamento.Oid] In(2,3,4,5,6,7,8,9,10)", FontColor = "Black", Enabled = false)]
        [Appearance("RdL.Asset.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Asset)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [RuleRequiredField("RuleReq.RdL.Apparato", DefaultContexts.Save, "Apparato è un campo obbligatorio")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Asset Asset
        {
            get
            {
                return GetDelayedPropertyValue<Asset>("Apparato");
            }
            set
            {
                SetDelayedPropertyValue<Asset>("Apparato", value);
            }
        }



        [PersistentAlias("Asset.StdApparato"), System.ComponentModel.DisplayName("Tipo Asset")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), VisibleInDashboards(false), VisibleInReports(false)]
        [ExplicitLoading()]
        //  [Delayed(true)]
        public StdAsset StdAsset
        {
            get
            {
                if (Asset == null)
                    return null;
                var tempObject = EvaluateAlias("StdAsset");
                if (tempObject != null)
                {
                    return (StdAsset)tempObject;
                }
                else
                {
                    return null;
                }

            }
        }


        #region SEZIONE DATE OPERATIVE
        private DateTime fDataSopralluogo;
        [Persistent("DATA_SOPRALLUOGO"), System.ComponentModel.DisplayName("Data Sopralluogo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //   [ToolTip("Data di Sopralluogo dell intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
        [ImmediatePostData(true)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        [Delayed(true)]
        public DateTime DataSopralluogo
        {
            get { return GetDelayedPropertyValue<DateTime>("DataSopralluogo"); }
            set { SetDelayedPropertyValue<DateTime>("DataSopralluogo", value); }
        }

        private bool fDataSopralluogoVisibile;
        [NonPersistent]
        [Browsable(false)]
        public bool DataSopralluogoVisibile
        {
            get { return fDataSopralluogoVisibile; }
            set { SetPropertyValue("DataSopralluogoVisibile", ref fDataSopralluogoVisibile, value); }
        }

        private Fatto fSopralluogoEseguito;
        [Persistent("FATTO_SOPRALLUOGO"), System.ComponentModel.DisplayName("Eseguito Sopralluogo")]
        [ImmediatePostData(true)]
        public Fatto SopralluogoEseguito
        {
            get
            {
                return fSopralluogoEseguito;
            }
            set
            {
                SetPropertyValue<Fatto>("SopralluogoEseguito", ref fSopralluogoEseguito, value);
            }
        }


        private DateTime fDataAzioniTampone;
        [Persistent("DATA_AZIONI_TAMPONE"), System.ComponentModel.DisplayName("Data Intervento Sicurezza")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        // [ToolTip("Data di Avvenuta Azione Tampone", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
        //[RuleRange("RuleRangeObject.LastSevenDays_RuleRange", "Save", "DataSopralluogo", "DataInizioLavori", "messaggio verifca data", ParametersMode.Expression)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        [Delayed(true)]
        public DateTime DataAzioniTampone
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAzioniTampone"); }
            set { SetDelayedPropertyValue<DateTime>("DataAzioniTampone", value); }
        }

        private bool fDataAzioniTamponeVisibile;
        [NonPersistent]
        [Browsable(false)]
        public bool DataAzioniTamponeVisibile
        {
            get { return fDataAzioniTamponeVisibile; }
            set { SetPropertyValue("DataAzioniTamponeVisibile", ref fDataAzioniTamponeVisibile, value); }
        }


        private DateTime fDataInizioLavori;
        [Persistent("DATA_INIZIO_LAVORI"), System.ComponentModel.DisplayName("Data Inizio Lavori")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        // [ToolTip("Data di Inizio Lavori per risolvere l'intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        [Delayed(true)]
        public DateTime DataInizioLavori
        {
            get { return GetDelayedPropertyValue<DateTime>("DataInizioLavori"); }
            set { SetDelayedPropertyValue<DateTime>("DataInizioLavori", value); }
        }


        private bool fDataInizioLavoriVisibile;
        [NonPersistent]
        [Browsable(false)]
        public bool DataInizioLavoriVisibile
        {
            get { return fDataInizioLavoriVisibile; }
            set { SetPropertyValue("DataInizioLavoriVisibile", ref fDataInizioLavoriVisibile, value); }
        }



        #endregion


        private Urgenza fUrgenza;
        [Persistent("URGENZA"), System.ComponentModel.DisplayName("Priorità")]
        [DataSourceCriteria("[<CommessePriorita>][^.Oid == Urgenza.Oid And Commesse.Oid = '@This.Immobile.Commesse.Oid' And Gruppo=='@This.Impianto.Gruppo']")]

        [RuleRequiredField("RuleReq.RdL.Urgenza", DefaultContexts.Save, "Urgenza è un campo obbligatorio")]
        [Appearance("RdL.Urgenza.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Urgenza)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RdL.Abilita.Urgenza", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        [Delayed(true)]
        public Urgenza Urgenza
        {
            get { return GetDelayedPropertyValue<Urgenza>("Urgenza"); }
            set { SetDelayedPropertyValue<Urgenza>("Urgenza", value); }

        }

        private Categoria fCategoria;
        [Persistent("CATEGORIA"), System.ComponentModel.DisplayName("Categoria Manutenzione")]
        [RuleRequiredField("RuleReq.RdL.Categoria", DefaultContexts.Save, "Categoria è un campo obbligatorio")]
        //[DataSourceCriteria("Oid In(2,3,4,5)")]
        [ImmediatePostData(true)]
        [Appearance("RdL.Abilita.Categoria", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        [Appearance("RdL.Categoria.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Categoria)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ExplicitLoading()]
        [Delayed(true)]
        [DataSourceCriteria("Oid in (2,3,4,5)")]

        public Categoria Categoria
        {
            get { return GetDelayedPropertyValue<Categoria>("Categoria"); }
            set { SetDelayedPropertyValue<Categoria>("Categoria", value); }
        }

        private TipoIntervento fTipoIntervento;
        //[DataSourceCriteria("Oid In(2,6,7)")]
        [Persistent("TIPOINTERVENTO"), System.ComponentModel.DisplayName("Tipo Intervento")]
        [DataSourceCriteria("[<CommesseTipoIntervento>][^.Oid == TipoIntervento.Oid And Commesse.Oid == '@This.Immobile.Commesse.Oid']")]
        [RuleRequiredField("RuleReq.RdL.TipoIntervento", DefaultContexts.Save, "Tipo Intervento è un campo obbligatorio")]
        [Appearance("RdL.TipoIntervento.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(TipoIntervento)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RdL.Abilita.TipoIntervento", FontColor = "Black", Enabled = false, Criteria = "[UltimoStatoSmistamento.Oid] In(4,5,7,8,9)")]
        //[ExplicitLoading()]
        [Delayed(true)]
        public TipoIntervento TipoIntervento
        {
            get { return GetDelayedPropertyValue<TipoIntervento>("TipoIntervento"); }
            set { SetDelayedPropertyValue<TipoIntervento>("TipoIntervento", value); }
        }

        #region  @@@@@@@@@@@   APPARATO PROBLEMA CAUSA E RIMEDIO E COMBO
        //private ApparatoProblema fProblema;   /// @@@@@@@@@@@@@  obligatorio per le manutenzioni a guasto oid=4     
        //[Persistent("PCRAPPPROBLEMA"), System.ComponentModel.DisplayName("Problema"), Association(@"RdL_Problema")]

        //[Persistent("PCRAPPPROBLEMA"), System.ComponentModel.DisplayName("Problema")]
        //[Appearance("RdL.Abilita.Problema", Criteria = "[Apparato] is null Or [ProblemaCausa] Is Not Null", FontColor = "Black", Enabled = false)]
        //[ImmediatePostData(true), VisibleInListView(false)]      //  [DataSourceProperty("ListaFiltraApparatoProblemas", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        //[DataSourceCriteria("StdApparato = '@This.Apparato.StdApparato'")]  //  deve essere persistente @@@@@@@@@@@@@@@ò  filtra per apparato     [ApparatoSchedaMP] Is Not Null   
        //[ExplicitLoading()]
        //[Delayed(true)]
        //public ApparatoProblema Problema
        //{
        //    get
        //    {
        //        return GetDelayedPropertyValue<ApparatoProblema>("Problema");
        //    }
        //    set
        //    {
        //        SetDelayedPropertyValue<ApparatoProblema>("Problema", value);
        //    }
        //}

        [Persistent("PCR_PROBLEMA"), System.ComponentModel.DisplayName("Problema")]
        //[Appearance("RdL.Abilita.Prob", Criteria = "[Apparato] is null Or [ProblemaCausa] Is Not Null", FontColor = "Black", Enabled = false)]
        [ImmediatePostData(true), VisibleInListView(false)]      //  [DataSourceProperty("ListaFiltraApparatoProblemas", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        //[DataSourceCriteria("StdApparato = '@This.Apparato.StdApparato'")]  // 
        [DataSourceCriteria("[<ApparatoProblema>][^.Oid == Problemi.Oid And StdApparato.Oid == '@This.Apparato.StdApparato.Oid']")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Problemi Prob
        {
            get
            {
                return GetDelayedPropertyValue<Problemi>("Prob");
            }
            set
            {
                SetDelayedPropertyValue<Problemi>("Prob", value);
            }
        }

        ////private ProblemaCausa fProblemaCausa; // @@@@@@@@@@@@@  obligatorio (solo in completamento)  per le manutenzioni a guasto oid=4       
        ////[Persistent("PCRPROBCAUSA"), System.ComponentModel.DisplayName("Causa"), Association(@"RdL_Causa")]
        //[Persistent("PCRPROBCAUSA"), System.ComponentModel.DisplayName("Causa")]
        //[Appearance("RdL.ProblemaCausa", Enabled = false, Criteria = "IsNullOrEmpty(Problema) Or not IsNullOrEmpty(CausaRimedio)", FontColor = "Black", Context = "DetailView")]
        //[ImmediatePostData(true), VisibleInListView(false)]        // [DataSourceProperty("ListaApparatoProblemaCausas", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        //[DataSourceCriteria("ApparatoProblema = '@This.Problema'")]  //filtra per apparato
        //[ExplicitLoading()]
        //[Delayed(true)]
        //public ProblemaCausa ProblemaCausa
        //{
        //    get
        //    {
        //        return GetDelayedPropertyValue<ProblemaCausa>("ProblemaCausa");
        //    }
        //    set
        //    {
        //        SetDelayedPropertyValue<ProblemaCausa>("ProblemaCausa", value);
        //    }
        //}

        [Persistent("PCR_CAUSA"), System.ComponentModel.DisplayName("Causa ")]
        [Appearance("RdL.Causa", Enabled = false, Criteria = "IsNullOrEmpty(Prob) Or not IsNullOrEmpty(Rimedio)", FontColor = "Black", Context = "DetailView")]
        [ImmediatePostData(true), VisibleInListView(false)]        // [DataSourceProperty("ListaApparatoProblemaCausas", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        //[DataSourceCriteria("ApparatoProblema = '@This.Problema'")]  //filtra per apparato
        [DataSourceCriteria("[<ProblemaCausa>][^.Oid == Cause.Oid And ApparatoProblema.Problemi.Oid == '@This.Prob.Oid']")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Cause Causa
        {
            get
            {
                //ProblemaCausa.ApparatoProblema.Problemi
                return GetDelayedPropertyValue<Cause>("Causa");
            }
            set
            {
                SetDelayedPropertyValue<Cause>("Causa", value);
            }
        }


        //private CausaRimedio fCausaRimedio; // @@@@@@@@@@@@@  obligatorio (solo in completamento)  per le manutenzioni a guasto oid=4
        //////[Persistent("PCRCAUSARIMEDIO"), System.ComponentModel.DisplayName("Rimedio"), Association(@"RdL_Rimedio")]
        //[Persistent("PCRCAUSARIMEDIO"), System.ComponentModel.DisplayName("Rimedio"),]
        //[Appearance("RdL.CausaRimedio", Enabled = false, Criteria = "IsNullOrEmpty(ProblemaCausa)", FontColor = "Black", Context = "DetailView")]
        //[ImmediatePostData, VisibleInListView(false)]
        //[DataSourceCriteria("ProblemaCausa = '@This.ProblemaCausa'")]  //filtra per apparato
        //[ExplicitLoading()]
        //[Delayed(true)]
        //public CausaRimedio CausaRimedio
        //{
        //    get
        //    {
        //        return GetDelayedPropertyValue<CausaRimedio>("CausaRimedio");
        //    }
        //    set
        //    {
        //        SetDelayedPropertyValue<CausaRimedio>("CausaRimedio", value);
        //    }

        //}

        //private Rimedio Rimedio; // @@@@@@@@@@@@@  obligatorio (solo in completamento)  per le manutenzioni a guasto oid=4
        ////[Persistent("PCRCAUSARIMEDIO"), System.ComponentModel.DisplayName("Rimedio"), Association(@"RdL_Rimedio")]
        [Persistent("PCR_RIMEDIO"), System.ComponentModel.DisplayName("Rimedio "),]
        [Appearance("RdL.Rimedio", Enabled = false, Criteria = "IsNullOrEmpty(Causa)", FontColor = "Black", Context = "DetailView")]
        [ImmediatePostData, VisibleInListView(false)]
        //[DataSourceCriteria("ProblemaCausa = '@This.ProblemaCausa'")]  //filtra per apparato
        [DataSourceCriteria("[<CausaRimedio>][^.Oid == Rimedi.Oid And ProblemaCausa.Cause.Oid == '@This.Causa.Oid']")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Rimedi Rimedio
        {
            get
            {

                return GetDelayedPropertyValue<Rimedi>("Rimedio");
            }
            set
            {
                SetDelayedPropertyValue<Rimedi>("Rimedio", value);
            }

        }


        //private StatoImpiantoInArrivo fStatoImpiantoInArrivo;
        //[Persistent("RDLSTATOINARRIVO"), System.ComponentModel.DisplayName("Stato Impianto in Arrivo")]
        //[Appearance("RdL.StatoImpiantoInArrivo.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(StatoImpiantoInArrivo)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        //[Appearance("RdL.Abilita.StatoImpiantoInArrivo", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        //[ExplicitLoading()]
        //[Delayed(true)]
        //public StatoImpiantoInArrivo StatoImpiantoInArrivo
        //{
        //    get { return GetDelayedPropertyValue<StatoImpiantoInArrivo>("StatoImpiantoInArrivo"); }
        //    set { SetDelayedPropertyValue<StatoImpiantoInArrivo>("StatoImpiantoInArrivo", value); }

        //}

        private DichiarazioneArrivo fDichiarazioneArrivo;
        [Persistent("RDLDICHIARAZIONEARRIVO"), System.ComponentModel.DisplayName("Tempo di Arrivo")]
        //[RuleRequiredField("RuleReq.RdL.DichiarazioneArrivo", DefaultContexts.Save, "Dichiarazione Tempo di Arrivo è un campo obbligatorio")]
        [Appearance("RdL.DichiarazioneArrivo.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(StatoImpiantoInArrivo)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RdL.Abilita.DichiarazioneArrivo", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        [ExplicitLoading()]
        [Delayed(true)]
        public DichiarazioneArrivo DichiarazioneArrivo
        {
            get { return GetDelayedPropertyValue<DichiarazioneArrivo>("DichiarazioneArrivo"); }
            set { SetDelayedPropertyValue<DichiarazioneArrivo>("DichiarazioneArrivo", value); }
        }



        #endregion

        private string fDescrizione;
        [Size(4000), Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione Intervento")]
        [RuleRequiredField("RdL.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        [Appearance("RdL.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [DbType("varchar(4000)")]
        [VisibleInListView(false)]
        //[Delayed(true)]
        public string Descrizione
        {
            get
            {
                return fDescrizione;
            }
            set
            {
                SetPropertyValue<string>("Descrizione", ref fDescrizione, value);
            }
        }

        private DateTime fDataCreazione;
        [Persistent("DATACREAZIONE"), System.ComponentModel.DisplayName("Data Creazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        // [ToolTip("Data di Creazione della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
        [VisibleInListView(false)]
        //[Delayed(true)]
        public DateTime DataCreazione
        {
            get
            {
                return fDataCreazione;
            }
            set
            {
                SetPropertyValue<DateTime>("DataCreazione", ref fDataCreazione, value);
            }
        }

        private DateTime fDataRichiesta;
        [Persistent("DATARICHIESTA"), System.ComponentModel.DisplayName("Data Richiesta")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //  [ToolTip("Data di Inserimento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]   In(1,2,11))
        [Appearance("RdL.DataRichiesta.Enabled", @"Oid > 0 And UtenteCreatoRichiesta != CurrentUserId() And !([UltimoStatoSmistamento.Oid] In(1))", FontColor = "Black", Enabled = false)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        //[Delayed(true)]
        public DateTime DataRichiesta
        {
            get
            {
                return fDataRichiesta;
            }
            set
            {
                SetPropertyValue<DateTime>("DataRichiesta", ref fDataRichiesta, value);
            }
        }

        [NonPersistent]
        [Appearance("RdL.vDataRichiesta.noVisibile", Visibility = ViewItemVisibility.Hide)]
        public DateTime vDataRichiesta { get; set; }

        private DateTime fDataPianificata;
        [Persistent("DATAPIANIFICATA"), System.ComponentModel.DisplayName("Data Pianificata")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //[EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_RdLPianificata_Editor)]//
        //[Delayed(true)]
        [ImmediatePostData]
        public DateTime DataPianificata
        {
            get
            {
                return fDataPianificata;
            }
            set
            {
                SetPropertyValue<DateTime>("DataPianificata", ref fDataPianificata, value);
            }
        }

        private DateTime fDataNow;
        [NonPersistent]
        [MemberDesignTimeVisibility(false)]
        public DateTime DataNow
        {
            get
            {
                return DateTime.Now;
            }

        }

        private DateTime fDataPianificataEnd;
        [Persistent("DATAPIANIFICATAEND"), System.ComponentModel.DisplayName("Data Pianificata Fine")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        //[Delayed(true)]
        public DateTime DataPianificataEnd
        {
            get
            {
                return fDataPianificataEnd;
            }
            set
            {
                SetPropertyValue<DateTime>("DataPianificataEnd", ref fDataPianificataEnd, value);
            }
        }


        private DateTime fDataPrevistoArrivo;
        [Persistent("DATAPREVISTOARRIVO"), System.ComponentModel.DisplayName("Data Previsto Arrivo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        [Delayed(true)]
        public DateTime DataPrevistoArrivo
        {
            get
            {
                return GetDelayedPropertyValue<DateTime>("DataPrevistoArrivo");
            }
            set
            {
                SetDelayedPropertyValue<DateTime>("DataPrevistoArrivo", value);
            }
        }

        private Soddisfazione fSoddisfazione;
        [Persistent("SODDISFAZIONE"), System.ComponentModel.DisplayName("Soddisfazione")]
        [Appearance("RdL.Visibility.Soddisfazione", Criteria = "[UltimoStatoSmistamento.Oid] != 4", Visibility = ViewItemVisibility.Hide)]
        //[ExplicitLoading()]
        [Delayed(true)]
        public Soddisfazione Soddisfazione
        {
            get
            {
                return GetDelayedPropertyValue<Soddisfazione>("Soddisfazione");
            }
            set
            {
                SetDelayedPropertyValue<Soddisfazione>("Soddisfazione", value);
            }
        }


        private StatoAutorizzativo fStatoAutorizzativo;
        [Persistent("AUTORIZZAZIONE"), System.ComponentModel.DisplayName("Autorizzazione")]
        [ExplicitLoading()]
        [Delayed(true)]
        public StatoAutorizzativo StatoAutorizzativo
        {
            get { return GetDelayedPropertyValue<StatoAutorizzativo>("StatoAutorizzativo"); }
            set { SetDelayedPropertyValue<StatoAutorizzativo>("StatoAutorizzativo", value); }
        }

        #region caso Intercent-ER   ---   NUOVI CAMPI  
        //// 
        private string fCodiceOut;
        [Persistent("CODICEOUT"), System.ComponentModel.DisplayName("Codice Out")]
        [DbType("varchar(100)")]
        public string CodiceOut
        {
            get { return fCodiceOut; }
            set { SetPropertyValue<string>("CodiceOut", ref fCodiceOut, value); }
        }

        private string fRichReparto;
        [Persistent("RICHREPARTO"), Size(50), System.ComponentModel.DisplayName("Richiedente Reparto")]
        [Appearance("RdL.RichReparto.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(RichReparto)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [RuleRequiredField("RdL.RichReparto.Obblig.su.Guasto", DefaultContexts.Save, SkipNullOrEmptyValues = false, TargetCriteria = "[Categoria.Oid] == 4 And Immobile.Commesse.AbilitaRichiedenteOpzioni = true")]
        [DbType("varchar(50)")]
        public string RichReparto
        {
            get { return fRichReparto; }
            set { SetPropertyValue<string>("RichReparto", ref fRichReparto, value); }
        }

        private string fRichReferente;
        [Persistent("RICHREFERENTE"), Size(50), System.ComponentModel.DisplayName("Richiedente Referente")]
        [Appearance("RdL.RichReferente.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(RichReferente)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [RuleRequiredField("RdL.RichReferente.Obblig.su.Guasto", DefaultContexts.Save, SkipNullOrEmptyValues = false, TargetCriteria = "[Categoria.Oid] == 4 And Immobile.Commesse.AbilitaRichiedenteOpzioni = true")]
        [DbType("varchar(50)")]
        public string RichReferente
        {
            get { return fRichReferente; }
            set { SetPropertyValue<string>("RichReferente", ref fRichReferente, value); }
        }

        private string fRichTelefono;
        [Persistent("RICHTELEFONO"), Size(20), System.ComponentModel.DisplayName("Richiedente Telefono")]
        [Appearance("RdL.RichTelefono.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(RichTelefono)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [RuleRequiredField("RdL.RichTelefono.Obblig.su.Guasto", DefaultContexts.Save, SkipNullOrEmptyValues = false, TargetCriteria = "[Categoria.Oid] == 4 And Immobile.Commesse.AbilitaRichiedenteOpzioni = true")]
        [DbType("varchar(20)")]
        public string RichTelefono
        {
            get { return fRichTelefono; }
            set { SetPropertyValue<string>("RichTelefono", ref fRichTelefono, value); }
        }

        #endregion

        private string fUtenteCreatoRichiesta;
        [Size(25), Persistent("UTENTEINSERIMENTO"), System.ComponentModel.DisplayName("Utente")]
        [DbType("varchar(4000)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public string UtenteCreatoRichiesta
        {
            get { return GetDelayedPropertyValue<string>("UtenteCreatoRichiesta"); }
            set { SetDelayedPropertyValue<string>("UtenteCreatoRichiesta", value); }
            //get
            //{
            //    return fUtenteCreatoRichiesta;
            //}
            //set
            //{
            //    SetPropertyValue<string>("UtenteCreatoRichiesta", ref fUtenteCreatoRichiesta, value);
            //}
        }

        private string fUtenteUltimo;
        [Persistent("ULTIMOUTENTE"), Size(100), XafDisplayName("Ultimo Utente")]
        [DbType("varchar(100)")]
        [Delayed(true)]
        [VisibleInListViewAttribute(false), VisibleInDetailView(true)]
        public string UtenteUltimo
        {
            get { return GetDelayedPropertyValue<string>("UtenteUltimo"); }
            set { SetDelayedPropertyValue<string>("UtenteUltimo", value); }
            //get
            //{
            //    return fUtenteUltimo;
            //}
            //set
            //{
            //    SetPropertyValue<string>("UtenteUltimo", ref fUtenteUltimo, value);
            //}
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"), System.ComponentModel.DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [System.ComponentModel.Browsable(true)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public DateTime DataAggiornamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAggiornamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataAggiornamento", value); }
        }

        #region RisorsaTeam e attributi
        private RisorseTeam fRisorseTeam;  //  @@@@@@@@@@  obligatoria solo in assegnazione
        //[ Association(@"TEAMRISORSE_RdL") ]
        [Persistent("RISORSATEAM"), System.ComponentModel.DisplayName("Team")]
        [DataSourceCriteria("RisorsaCapo.CentroOperativo == '@This.Immobile.CentroOperativoBase'")]
        [Appearance("RdL.RisorseTeam.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(RisorseTeam) AND ([UltimoStatoSmistamento.Oid] In(2,11))", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RdL.RisorseTeam.nero", AppearanceItemType.LayoutItem, "not IsNullOrEmpty(RisorseTeam)", FontStyle = FontStyle.Bold, FontColor = "Black")]
        [Appearance("RdL.RisorseTeam.neroItem", AppearanceItemType.ViewItem, "not IsNullOrEmpty(RisorseTeam)", Enabled = false, FontStyle = FontStyle.Bold, FontColor = "Black")]
        //[System.ComponentModel.Browsable(false)]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public RisorseTeam RisorseTeam
        {
            get { return GetDelayedPropertyValue<RisorseTeam>("RisorseTeam"); }
            set { SetDelayedPropertyValue<RisorseTeam>("RisorseTeam", value); }
        }

        private string fRicercaRisorseTeam;
        [NonPersistent, Size(25)]//, DisplayName("Filtro"), , Size(25)
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false), VisibleInDashboards(false), VisibleInReports(false)]
        public string RicercaRisorseTeam
        {
            get;
            set;
        }

        [PersistentAlias("Iif(RisorseTeam is not null,RisorseTeam.Mansione,null)"), System.ComponentModel.DisplayName("Mansione")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true), VisibleInDashboards(false), VisibleInReports(false)]
        public string Mansione
        {
            get
            {
                var tempObject = EvaluateAlias("Mansione");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return null;
                }
            }
        }

        [PersistentAlias("Iif(RisorseTeam is not null,RisorseTeam.RisorsaCapo.Telefono + ',' + RisorseTeam.RisorsaCapo.Email,null)"), System.ComponentModel.DisplayName("Telefono/eMail Risorsa")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true), VisibleInDashboards(false), VisibleInReports(false)]
        [Size(551)]
        public string RisorseTeamTelefono
        {
            get
            {
                if (this.Oid == -1) return null;
                var tempObject = EvaluateAlias("RisorseTeamTelefono");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return null;
                }
            }
        }


        #endregion


        //private DateTime fDataAssegnazioneOdl;
        [Persistent("DATAASSEGNAZIONEODL"), System.ComponentModel.DisplayName("Data di Assegnazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [Appearance("RdL.DataAssegnazioneOdl.noneditabile", FontColor = "Black", Enabled = false)]
        //[EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public DateTime DataAssegnazioneOdl
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAssegnazioneOdl"); }
            set { SetDelayedPropertyValue<DateTime>("DataAssegnazioneOdl", value); }
        }

        //private OdL fOdL; //Association(@"OdLRefRdL"),
        [Persistent("ODL"), System.ComponentModel.DisplayName("Ordine di Lavoro")]
        //[MemberDesignTimeVisibility(false)]
        [Delayed(true)]
        public OdL OdL
        {
            get
            {
                return GetDelayedPropertyValue<OdL>("OdL");
            }
            set
            {
                SetDelayedPropertyValue<OdL>("OdL", value);
            }
        }

        private bool fisMP_count;
        [System.ComponentModel.Browsable(false)]
        [NonPersistent]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true), VisibleInDashboards(false), VisibleInReports(false)]
        public bool IsMP_count
        {
            get { return fisMP_count; }
            set
            {
                fisMP_count = value;
                //OnChanged("Hint");
                //this.RisorseTeam
            }
        }



        #region stato smistamento e operativo
        //[PersistentAlias("[<BTLabelItemType>][[<BTItemTypeProduct>][ICProductSK=^.^.ICProductSK And BTItemTypeSK=^.BTItemTypeSK] And LabelType=1].Single(BTLabelSK)")]
        //public BTLabel BTLabelSK_Item { get { return (BTLabel)EvaluateAlias("BTLabelSK_Item"); } }
        /// <summary>   [PersistentAlias("[<BTLabelItemType>][[<BTItemTypeProduct>][ICProductSK=^.^.ICProductSK And BTItemTypeSK=^.BTItemTypeSK] And LabelType=1].Single(BTLabelSK)")]
        /// /stato operativo dove statosmistamento è uguale a rdl.statosmistamento    fVisualizzaSLA
        /// 
        private StatoSmistamento fUltimoStatoSmistamento;
        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.DisplayName("Stato Smistamento")]
        [RuleRequiredField("RRq.RdL.UltimoStatoSmistamento1", DefaultContexts.Save, "La StatoSmistamento è un campo obbligatorio")]
        [Appearance("RdL.UltimoStatoSmistamento1.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(UltimoStatoSmistamento)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RdL.UltimoStatoSmistamento1.Evidenza", AppearanceItemType.LayoutItem, "not(IsNullOrEmpty(UltimoStatoSmistamento))", FontStyle = FontStyle.Bold, BackColor = "Yellow", FontColor = "Black")]
        //[DataSourceCriteria("[<StatoSmistamentoCombo>][^.Oid == StatoSmistamentoxCombo.Oid And StatoSmistamento.Oid == '@This.old_SSmistamento_Oid']")]
        [DataSourceCriteria("'@This' is null or [<StatoSmistamentoCombo>][^.Oid == StatoSmistamentoxCombo.Oid And StatoSmistamento.Oid == '@This.old_SSmistamento_Oid']")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        //[Delayed(true)]
        public StatoSmistamento UltimoStatoSmistamento
        {
            get
            {
                return fUltimoStatoSmistamento;
            }
            set
            {
                SetPropertyValue<StatoSmistamento>("UltimoStatoSmistamento", ref fUltimoStatoSmistamento, value);
            }
        }

        private StatoOperativo fUltimoStatoOperativo;
        [Persistent("STATOOPERATIVO"), System.ComponentModel.DisplayName("Stato Operativo")]
        //[DataSourceCriteria("[<StatoSmistamento_SOperativoSO>][^.Oid == StatoOperativoSO.Oid And StatoSmistamento.Oid == '@This.old_SSmistamento_Oid'] ")]
        [DataSourceCriteria("[<StatoSmistamento_SOperativoSO>][^.Oid == StatoOperativoSO.Oid And StatoSmistamento.Oid == '@This.UltimoStatoSmistamento.Oid'] ")]
        [ExplicitLoading()]
        public StatoOperativo UltimoStatoOperativo
        {
            get
            {
                return fUltimoStatoOperativo;
            }
            set
            {
                SetPropertyValue<StatoOperativo>("UltimoStatoOperativo", ref fUltimoStatoOperativo, value);
            }
        }

        private string fStatoSmistamentoCliente;
        [NonPersistent(), System.ComponentModel.DisplayName("Stato Intervento")]
        [Appearance("RdL.StatoIntervento.Evidenza", AppearanceItemType.LayoutItem, "not(IsNullOrEmpty(StatoSmistamentoCliente))", FontStyle = FontStyle.Bold, BackColor = "Yellow", FontColor = "Black")]
        public string StatoSmistamentoCliente
        {
            get
            {
                return fStatoSmistamentoCliente;
            }
            set
            {
                SetPropertyValue<string>("StatoSmistamentoCliente", ref fStatoSmistamentoCliente, value);
            }
        }

        #endregion

        //private RegistroRdL fRegistroRdL;
        [Association(@"REGISTRORDLRefRdl"), Persistent("REGRDL"), System.ComponentModel.DisplayName("Registro RdL")]
        //[System.ComponentModel.Browsable(false)]
        //[ExplicitLoading()]
        [Delayed(true)]
        public RegistroRdL RegistroRdL
        {
            get { return GetDelayedPropertyValue<RegistroRdL>("RegistroRdL"); }
            set { SetDelayedPropertyValue<RegistroRdL>("RegistroRdL", value); }
        }

        private string fOBJWEB;
        [NonPersistent, Size(100), DevExpress.Xpo.DisplayName("web")]
        [VisibleInListView(false)]
        public string OBJWEB
        {
            get { return fOBJWEB; }
            set { SetPropertyValue<string>("OBJWEB", ref fOBJWEB, value); }
        }



        #region Dati completamento
        //private DateTime fDataCompletamento;
        [Persistent("DATACOMPLETAMENTO"), System.ComponentModel.DisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
        [Appearance("RdL.DataCompletamento.EvidenzaObligatorio", AppearanceItemType.LayoutItem,
            "IsNullOrEmpty(DataCompletamento) AND ([UltimoStatoSmistamento.Oid] In(4))", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ImmediatePostData(true)]
        [Delayed(true)]
        public DateTime DataCompletamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataCompletamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataCompletamento", value); }

        }

        //private DateTime fDataCompletamentoSistema;
        [Persistent("DATACOMPLETAMENTOSYS"), System.ComponentModel.DisplayName("Data inserimento Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [MemberDesignTimeVisibility(false)]
        [Delayed(true)]
        public DateTime DataCompletamentoSistema
        {
            get { return GetDelayedPropertyValue<DateTime>("DataCompletamentoSistema"); }
            set { SetDelayedPropertyValue<DateTime>("DataCompletamentoSistema", value); }

        }

        //private string fNoteCompletamento;
        [Persistent("NOTECOMPLETAMENTO"), Size(4000), System.ComponentModel.DisplayName("Note Completamento")]
        [RuleRequiredField("Rulereq.RdL.NoteCompletamento", DefaultContexts.Save,
            TargetCriteria = "IsNullOrEmpty(NoteCompletamento) And Immobile.Commesse.AbilitaTestoCompletamentoObligatorio And Categoria.Oid = 4 And [UltimoStatoSmistamento.Oid] = 4",
            CustomMessageTemplate = "La Nota Completamento è un campo obbligatorio")]
        //[Appearance("RdL.NoteCompletamento.Visible", AppearanceItemType.LayoutItem, @"Oid = -1 And [UltimoStatoSmistamento.Oid] In(1,2,3,10,11)",
        // Priority = 1, Visibility = ViewItemVisibility.Hide)]
        [DbType("varchar(4000)")]
        //[ImmediatePostData(true)]
        [Delayed(true)]
        public string NoteCompletamento
        {
            get { return GetDelayedPropertyValue<string>("NoteCompletamento"); }
            set { SetDelayedPropertyValue<string>("NoteCompletamento", value); }

        }

        [Persistent("NOTE"), Size(4000), System.ComponentModel.DisplayName("Note")]
        [Appearance("RdL.Note.Visible", AppearanceItemType.LayoutItem, @"Oid = -1", Visibility = ViewItemVisibility.Hide)]
        [DbType("varchar(4000)")]
        [Delayed(true)]
        public string Note
        {
            get { return GetDelayedPropertyValue<string>("Note"); }
            set { SetDelayedPropertyValue<string>("Note", value); }

        }

        //private DateTime fDataFermo;
        [Persistent("DATAFERMO"), System.ComponentModel.DisplayName("Data Fermo"), VisibleInListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        [ImmediatePostData(true)]
        [Delayed(true)]
        public DateTime DataFermo
        {
            get { return GetDelayedPropertyValue<DateTime>("DataFermo"); }
            set { SetDelayedPropertyValue<DateTime>("DataFermo", value); }
        }

        //private DateTime fDataRiavvio;
        [Persistent("DATARIAVVIO"), System.ComponentModel.DisplayName("Data Riavvio")]
        [VisibleInListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [Appearance("RdL.Abilita.DataRiavvio", Criteria = "DataFermo  is null", Enabled = false)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)] // 
        [ImmediatePostData(true)]
        [Delayed(true)]
        public DateTime DataRiavvio
        {
            get
            {
                return GetDelayedPropertyValue<DateTime>("DataRiavvio");
            }
            set
            {
                SetDelayedPropertyValue<DateTime>("DataRiavvio", value);
            }
        }

        //private RdL fRdLSuccessiva;
        [Persistent("RDL_SUCCESSIVA")]
        //[System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public RdL RdLSuccessiva
        {
            get
            {
                return GetDelayedPropertyValue<RdL>("RdLSuccessiva");
            }
            set
            {
                SetDelayedPropertyValue<RdL>("RdLSuccessiva", value);
            }
        }

        #endregion

        #region Associazioni e alias
        [PersistentAlias("Oid")]
        [System.ComponentModel.DisplayName("Codice")]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("RdL.Codice.ColoreGiallo", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[UltimoStatoSmistamento.Oid] In(1)", BackColor = "Yellow")]
        [Appearance("RdL.Codice.ColoreRosso", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[UltimoStatoSmistamento.Oid] In(10)", BackColor = "Red")]
        [Appearance("RdL.Codice.ColoreVerde", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[UltimoStatoSmistamento.Oid] In(2,3,11)", BackColor = "LightGreen")]
        [Appearance("RdL.Codice.ColoreArancio", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[UltimoStatoSmistamento.Oid] In(4,5)", BackColor = "0xfff0f0")]
        [Appearance("RdL.Codice.ColoreBlue", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[UltimoStatoSmistamento.Oid] In(6, 7)", BackColor = "LightSteelBlue")]
        [Appearance("RdL.Codice.violet", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[UltimoStatoSmistamento.Oid] = 1 And UltimoStatoOperativo.Oid = 101", Priority = 1, BackColor = "#ee82ee")]

        public string Codice
        {
            get
            {
                var tempObject = EvaluateAlias("Codice");
                if (tempObject != null)
                {
                    return (string)tempObject.ToString();
                }
                return null;
            }
        }


        //[PersistentAlias("Iif(Oid>0,Oid,'')  +  Iif(OdL is not null,'/' + OdL.Oid,'//') +  Iif(RegistroRdL is not null,'/' + RegistroRdL.Oid,'')")]
        [PersistentAlias("Iif(Oid>0,Oid,'')  + Iif(RegistroRdL is not null,'/' + RegistroRdL.Oid,'') +  Iif(OdL is not null,'/' + OdL.Oid,'//')")]
        [System.ComponentModel.DisplayName("Codici Sistema RdL/RegRdL/OdL")]
        [VisibleInDashboards(false), VisibleInReports(false)]
        public string CodiciRdLOdL
        {
            get
            {
                if (this.Oid == -1) return null;

                var tempObject = EvaluateAlias("CodiciRdLOdL");
                if (tempObject != null)
                {
                    return (string)tempObject.ToString();
                }
                return null;
            }
        }


        [PersistentAlias("OdL.Oid"), System.ComponentModel.DisplayName("Cod OdL"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [MemberDesignTimeVisibility(false)]
        public int CodOdL
        {
            get
            {
                if (this.Oid == -1) return 0;
                var tempObject = EvaluateAlias("CodOdL");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        [PersistentAlias("Iif(RegistroRdL is not null,RegistroRdL.Oid,0)"), System.ComponentModel.DisplayName("Cod Reg.RdL")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDashboards(false), VisibleInReports(false)]
        public int CodRegRdL
        {
            get
            {
                if (this.Oid == -1) return 0;
                var tempObject = EvaluateAlias("CodRegRdL");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }


        #region lista rdl gia caricate e presenti possibili ambigue

        private XPCollection<RdLListSimiliView> fListaRdLSimilis;
        [PersistentAlias("fListaRdLSimilis"), System.ComponentModel.DisplayName("Richieste intervento simili x Immobile")]
        [Appearance("RdL.ListaRdLSimilis", Criteria = "(Oid > -1 Or Immobile is null Or Categoria.Oid != 4)", Visibility = ViewItemVisibility.Hide)]
        [Appearance("RdL.ListaRdLSimilis.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "(Oid > -1 Or Immobile is null Or Categoria.Oid != 4)", Visibility = ViewItemVisibility.Hide)]
        //[RuleObjectExists("ObjectExists1", DefaultContexts.Save, @"[ItemSubject] = '@Owner.Subject' or Contains([Name], [Owner.Name])", IncludeCurrentObject = true)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true), VisibleInDashboards(false), VisibleInReports(false)]

        public XPCollection<RdLListSimiliView> ListaRdLSimilis
        {
            get
            {
                if (this.Oid > 0) return null;

                if (this.Oid == -1 && fListaRdLSimilis == null && Immobile != null && Categoria != null)
                {
                    //fListaRdLSimilis = new XPCollection<RdLListSimiliView>(Session);
                    RefreshfListaRdLSimilis();
                    if (fListaRdLSimilis != null) fListaRdLSimilis.Count();
                }
                return fListaRdLSimilis;
            }
        }

        private void RefreshfListaRdLSimilis()
        {
            if (IsInvalidated)
                return;
            if (this.Categoria == null || this.Immobile == null)
                return;

            if (!this.IsLoading)
            {
                if (this.Oid == -1 && Immobile != null && Categoria != null && fListaRdLSimilis == null) //fListaRdLSimilis == null || 
                {
                    string ggritardo = Session.Query<CAMS.Module.DBAngrafica.ParametriGeneraliDB>().Where(w => w.NomeParametro == "GiorniRitardoRicerca")
                                     .Select(s => s.Valore).First();
                    if (!int.TryParse(ggritardo, out CAMS.Module.Classi.SetVarSessione.GiorniRitardoRicerca))
                        CAMS.Module.Classi.SetVarSessione.GiorniRitardoRicerca = 0;

                    var ParCriteria = string.Format("OidImmobile = {0} And OidCategoria == {1}  And DataRichiesta > #{2}/{3}/{4}#",
                                                          Immobile.Oid.ToString(), Categoria.Oid.ToString(),
                                                          DateTime.Now.AddDays(SetVarSessione.GiorniRitardoRicerca).Month, DateTime.Now.AddDays(SetVarSessione.GiorniRitardoRicerca).Day,
                                                          DateTime.Now.AddDays(SetVarSessione.GiorniRitardoRicerca).Year);
                    fListaRdLSimilis = new XPCollection<RdLListSimiliView>(Session, CriteriaOperator.Parse(ParCriteria));
                    OnChanged("ListaRdLSimilis");
                }
            }
        }


        #endregion


        #region lista combo stato smistamento e stato operativo

        private int fold_SSmistamento_Oid;
        [Persistent("OLD_SSMISTAMENTO")]
        [System.ComponentModel.DisplayName("SSmistamento Precedente")]
        //[MemberDesignTimeVisibility(false)]
        //[Browsable(false)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int old_SSmistamento_Oid
        {
            get { return fold_SSmistamento_Oid; }
            set { SetPropertyValue<int>("old_SSmistamento_Oid", ref fold_SSmistamento_Oid, value); }
            //get { return GetDelayedPropertyValue<int>("old_SSmistamento_Oid"); }
            //set { SetDelayedPropertyValue<int>("old_SSmistamento_Oid", value); }
        }

        private int fold_RisorseTeam_Oid;
        [Persistent("OLD_RISOSRATEAM")]
        [MemberDesignTimeVisibility(false)]
        public int old_RisorseTeam_Oid
        {
            get { return fold_RisorseTeam_Oid; }
            set { SetPropertyValue<int>("old_RisorseTeam_Oid", ref fold_RisorseTeam_Oid, value); }
            //get { return GetDelayedPropertyValue<int>("old_SSmistamento_Oid"); }
            //set { SetDelayedPropertyValue<int>("old_SSmistamento_Oid", value); }
        }

        [NonPersistent]
        [MemberDesignTimeVisibility(false)]
        public bool UserIsAdminRuolo
        {
            get
            {
                return CAMS.Module.Classi.SetVarSessione.IsAdminRuolo;
            }
        }
        [NonPersistent]
        //[MemberDesignTimeVisibility(false)]
        public bool RuoloIntercenter
        {
            get
            {
                //SecuritySystem.CurrentUser
                if (GetRuleName((DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)SecuritySystem.CurrentUser).Contains("INTERCENTER"))
                {
                    return true;
                }
                else
                {
                    return false;
                }

                return false;
                //return CAMS.Module.Classi.SetVarSessione.IsAdminRuolo;
            }
        }
        private string GetRuleName(DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser Utente)
        {
            var NomiRuolo = Utente.Roles.Select(s => s.Name).ToArray();
            if (NomiRuolo.Length < 1) return string.Empty;

            return string.Join(";", NomiRuolo);

        }
        //[NonPersistent]
        [PersistentAlias("Iif(Immobile is not null,Immobile.Commesse.MostraDataOraFermo,1=2)")]
        [System.ComponentModel.Browsable(false)]
        public bool VisualizzaDataFermo
        {
            get
            {
                return (bool)EvaluateAlias("VisualizzaDataFermo");
            }
        }

        #endregion
        [Association(@"Documenti_RdL", typeof(Documenti)), DevExpress.Xpo.Aggregated, System.ComponentModel.DisplayName("Documenti")]
        [Appearance("RdL.Documentis.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<Documenti> Documentis
        {
            get
            {
                if (this.Oid == -1) return null;

                return GetCollection<Documenti>("Documentis");
            }
        }

        //
        [Association("RdL_AttivitaPianificateDett", typeof(MpAttivitaPianificateDett)), DevExpress.Xpo.Aggregated]
        [DevExpress.ExpressApp.DC.XafDisplayName("Elenco Attività Pianificate in Dettaglio")]
        [Appearance("RdL.MpAttivitaPianificateDetts.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or Categoria.Oid != 1 Or MpAttivitaPianificateDetts.Count = 0", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<MpAttivitaPianificateDett> MpAttivitaPianificateDetts
        {
            get
            {
                if (this.Oid == -1) return null;

                return GetCollection<MpAttivitaPianificateDett>("MpAttivitaPianificateDetts");

            }
        }

        private XPCollection<NotificheEmergenze> fNotificheEmergenze_;
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        [Appearance("RdL.NotificheEmergenze.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or UltimoStatoSmistamento.Oid != 10", Visibility = ViewItemVisibility.Hide)]
        [XafDisplayName("Notifica Emergenze")]
        public XPCollection<NotificheEmergenze> NotificheEmergenze_
        {
            get
            {
                if (Oid == -1)
                { return null; }
                else
                {
                    if (fNotificheEmergenze_ == null)
                    {
                        //NotificheEmergenze aa = new NotificheEmergenze();
                        //aa.RegNotificheEmergenze.RdL
                        CriteriaOperator op = CriteriaOperator.Parse("RegNotificheEmergenze.RdL.Oid == ?", this.Oid);
                        fNotificheEmergenze_ = new XPCollection<NotificheEmergenze>(Session, op);//.Where(w => w.RegistroRdL.Oid == this.RegistroRdL.Oid);           
                        fNotificheEmergenze_.BindingBehavior = CollectionBindingBehavior.AllowNone;
                    }
                }
                return fNotificheEmergenze_;
            }
        }

        [Association(@"RdLNote_RdL", typeof(RdLNote)), System.ComponentModel.DisplayName("RdL Note")]
        // [Appearance("RdL.RdLNotes.Hide", Criteria = "Oid = -1 Or Categoria.Oid != 4", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)] Or RdLNotes.Count = 0
        [Appearance("RdL.RdLNotes.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or Categoria.Oid != 4", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<RdLNote> RdLNotes
        {
            get
            {
                if (Oid == -1) return null;
                return GetCollection<RdLNote>("RdLNotes");
            }
        }


        [Association(@"RdLApparatoSchedeMp_RdL", typeof(RdLApparatoSchedeMP)), System.ComponentModel.DisplayName("Elenco Procedure MP Collegate")]
        [Appearance("RdL.RdLApparatoSchedaMPs.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or Categoria.Oid = 4 Or RdLApparatoSchedaMPs.Count() = 0", Visibility = ViewItemVisibility.Hide)]
        [ToolTip("Elenco delle Attività di manutenzione Associate alla Richiesta di Lavoro")]
        //      [RuleValueComparison("RuleValueComparisonObject.Collection.RuleValueComparison1", DefaultContexts.Save, ValueComparisonType.GreaterThan, 100, TargetPropertyName = "SumIsGreaterThan100", TargetCollectionAggregate = Aggregate.Sum, CustomMessageTemplate = @"The sum of the ""{TargetPropertyName}"" values must be greater than ""{RightOperand}"". The current value is ""{AggregatedTargetValue}"".")]
        //   [RuleValueComparison("RuleValueComparisonObject.Collection.RuleValueComparison2", DefaultContexts.Save, ValueComparisonType.Equals, 3, TargetCollectionAggregate = Aggregate.Count, CustomMessageTemplate = @"The ""{TargetCollectionOwnerType}.{TargetCollectionPropertyName}"" collection must contain {RightOperand} elements. Currently, it contains {AggregatedTargetValue} elements.")]

        public XPCollection<RdLApparatoSchedeMP> RdLApparatoSchedaMPs
        {
            get
            {
                if (Oid == -1) return null;
                return GetCollection<RdLApparatoSchedeMP>("RdLApparatoSchedaMPs");
            }
        }


        [Association(@"RdLMultiRisorseTeam_RdL", typeof(RdLMultiRisorseTeam)), DevExpress.Xpo.Aggregated, System.ComponentModel.DisplayName("Elenco RisorseTeam Collegate")]
        [Appearance("RdL.RdLMultiRisorseTeam.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or UltimoStatoSmistamento.Oid In(1,10)", Visibility = ViewItemVisibility.Hide)]
        [ToolTip("Elenco RisorseTeam Collegate")]
        public XPCollection<RdLMultiRisorseTeam> RdLMultiRisorseTeams
        {
            get
            {
                if (Oid == -1) return null;
                return GetCollection<RdLMultiRisorseTeam>("RdLMultiRisorseTeams");
            }
        }




        private XPCollection<RegistroLavori> fRegistroLavori_;
        [Appearance("RdL.RegistroLavori_.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or UltimoStatoSmistamento.Oid == 1", Visibility = ViewItemVisibility.Hide)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        [XafDisplayName("Registro Lavori")]
        public XPCollection<RegistroLavori> RegistroLavori_
        {
            get
            {
                if (Oid == -1) return null;
                if (this.RegistroRdL != null && this.UltimoStatoSmistamento.Oid > 1)
                {
                    if (fRegistroLavori_ == null)
                    {
                        //this.RegistroRdL.Oid
                        CriteriaOperator op = CriteriaOperator.Parse("RegistroRdL.Oid == ?", this.RegistroRdL.Oid);
                        fRegistroLavori_ = new XPCollection<RegistroLavori>(Session, op);//.Where(w => w.RegistroRdL.Oid == this.RegistroRdL.Oid);           
                        fRegistroLavori_.BindingBehavior = CollectionBindingBehavior.AllowNone;
                    }
                }
                return fRegistroLavori_;
            }
        }


        #endregion
        bool suppressOnChanged = false;
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!this.IsLoading)
            {
                if (this.Oid == -1)
                {
                    #region in fase di creazione
                    //if (newValue != null)
                    //{
                    //    #region per oid = -1
                    string Sw_propertyName = propertyName;
                    switch (Sw_propertyName)
                    {
                        case "Categoria":
                            if (newValue != oldValue && newValue != null)
                            {
                                int newOid = ((DevExpress.Xpo.XPObject)(newValue)).Oid;
                                if (newOid != 4)
                                {
                                    this.fTipoIntervento = Session.GetObjectByKey<TipoIntervento>(0);
                                }

                                if (newValue != oldValue && this.Immobile != null)
                                {
                                    fListaRdLSimilis = null;
                                    RefreshfListaRdLSimilis();
                                    OnChanged("ListaRdLSimilis");
                                }
                            }
                            break;
                        // todo: controllare se funziona
                        case "Immobile":
                            #region edifici
                            if (newValue != oldValue && !suppressOnChanged)
                            {
                                suppressOnChanged = true;
                                try
                                {
                                    Immobile newImmobile = ((Immobile)(newValue));
                                    if (newImmobile != null)
                                    {
                                        this.Servizio = null;
                                        this.Asset = null;
                                        this.Piano = null;
                                        this.Locale = null;

                                        int conta = newImmobile.Impianti.Count;

                                        if (conta == 1)
                                            this.Servizio = newImmobile.Impianti[0];

                                        fListaRdLSimilis = null;
                                        RefreshfListaRdLSimilis();
                                        OnChanged("ListaRdLSimilis");
                                    }
                                    else
                                    {
                                        this.Servizio = null;
                                        this.Asset = null;
                                        this.Richiedente = null;
                                        this.Piano = null;
                                        this.Locale = null;
                                        this.Urgenza = null;
                                        fListaRdLSimilis = null;
                                        RefreshfListaRdLSimilis();
                                        OnChanged("ListaRdLSimilis");
                                    }
                                    suppressOnChanged = false;
                                }
                                catch
                                {
                                    this.Servizio = null;
                                    this.Asset = null;
                                    this.Richiedente = null;
                                    this.Piano = null;
                                    this.Locale = null;
                                    fListaRdLSimilis = null;
                                    RefreshfListaRdLSimilis();
                                    OnChanged("ListaRdLSimilis");
                                }
                                finally
                                {
                                    suppressOnChanged = false;
                                }
                            }
                            #endregion
                            break;
                        #region vecchi case

                        #endregion

                        case "Impianto":
                            #region case impianto nuova
                            if (newValue != oldValue && !suppressOnChanged)
                            {
                                //suppressOnChanged = true;
                                try
                                {
                                    Servizio newImpianto = ((Servizio)(newValue));

                                }
                                catch
                                {

                                }
                                finally
                                {
                                }
                            }
                            #endregion
                            break;

                        case "DataFermo":
                            DateTime newDate = (DateTime)(newValue);
                            if (newDate != DateTime.MinValue)
                            {
                                this.DataRiavvio = newDate;
                            }
                            break;
                        case "DataRichiesta":
                            DateTime newDataRichiesta = (DateTime)(newValue);
                            if (this.Oid == -1 && (this.vDataRichiesta == null || this.vDataRichiesta == DateTime.MinValue))
                            {
                                this.vDataRichiesta = DataRichiesta;
                            }
                            break;
                        //case "RisorseTeam":
                        //    if (newValue != null)
                        //        this.DataAssegnazioneOdl = DateTime.Now;
                        //    else
                        //        this.DataAssegnazioneOdl = DateTime.MinValue;
                        //    break;
                        //TipologiaSpedizione
                        case "TipologiaSpedizione":
                            if (newValue != null)
                            {
                                TipologiaSpedizioneRdL newTipSpedizioneRdL = (TipologiaSpedizioneRdL)(newValue);
                                if (newTipSpedizioneRdL == TipologiaSpedizioneRdL.No)
                                {
                                    this.Email = null;
                                    this.PhoneString = null;
                                }
                                else if (newTipSpedizioneRdL == TipologiaSpedizioneRdL.MAIL)
                                {
                                    if (this.Richiedente.Mail != null && this.Email != Richiedente.Mail)
                                        this.Email = this.Richiedente.Mail;

                                    this.PhoneString = null;
                                }
                                else if (newTipSpedizioneRdL == TipologiaSpedizioneRdL.SMS)
                                {
                                    this.Email = null;
                                    if (this.Richiedente.PhoneMobString != null && this.PhoneString != Richiedente.PhoneMobString)
                                        this.PhoneString = Richiedente.PhoneMobString;

                                }
                                else if (newTipSpedizioneRdL == TipologiaSpedizioneRdL.MAILSMS)
                                {
                                    if (this.Richiedente.Mail != null && this.Email != Richiedente.Mail)
                                        this.Email = this.Richiedente.Mail;
                                    if (this.Richiedente.PhoneMobString != null && this.PhoneString != Richiedente.PhoneMobString)
                                        this.PhoneString = Richiedente.PhoneMobString;
                                }
                            }
                            break;

                        case "Problema":
                            if (newValue != null)
                            {
                                int minutidiintervento = 60;
                                if (this.Prob != null)  //    if (this.Problema != null)
                                {
                                    //minutidiintervento = Convert.ToInt32(this.Problema.Problemi.Valore);
                                    minutidiintervento = Convert.ToInt32(this.Prob.Valore);
                                    if (this.DataPianificataEnd < this.DataPianificata.AddMinutes(minutidiintervento))
                                        this.fDataPianificataEnd = this.DataPianificata.AddMinutes(minutidiintervento);
                                }
                            }
                            break;
                        case "Urgenza":
                            if (newValue != null)
                            {
                                Urgenza newPUrgenza = (Urgenza)(newValue);
                                int minuti = newPUrgenza.Val;
                                int da = Convert.ToInt32(minuti / 2);
                                int a = Convert.ToInt32((minuti - (minuti * 0.1)));             // Possibili valori di numeroCasuale: {1, 2, 3, 4, 5, 6}
                                Random random = new Random();
                                int numeroCasuale = random.Next(da, a);
                                if (numeroCasuale < 16)
                                    numeroCasuale = 16;
                                if (numeroCasuale > 16)  //    if (this.Problema != null)
                                {
                                    this.fDataSopralluogo = DateTime.Now.AddMinutes(numeroCasuale);
                                    this.fDataAzioniTampone = DateTime.Now.AddMinutes(numeroCasuale);
                                    this.fDataInizioLavori = DateTime.Now.AddMinutes(numeroCasuale);
                                }
                            }
                            break;
                    }
                    //}
                    //#endregion
                    #endregion
                }


                if (this.Oid > 1)
                {
                    #region se già esistente
                    switch (propertyName)
                    {
                        case "UltimoStatoSmistamento":
                            if (newValue != null && !suppressOnChanged)
                            {
                                try
                                {
                                    StatoSmistamento newUltimoStatoSmistamento = ((StatoSmistamento)(newValue));

                                    if (newValue != oldValue)
                                    {
                                        suppressOnChanged = true;
                                        #region stato smistamento
                                        switch (newUltimoStatoSmistamento.Oid)
                                        {
                                            case 1:// in assegnata smatphone           fListaFiltraComboRisorseTeam  //  1	In Attesa di Assegnazione
                                                this.UltimoStatoOperativo = null;
                                                this.RisorseTeam = null;
                                                // this.fListaFiltraComboRisorseTeam = null;
                                                break;

                                            case 2:// in assegnata smatphone  //2	Assegnata
                                                this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(19);   //this.RegistroRdL.UltimoStatoSmistamento = Session.GetObjectByKey<StatoSmistamento>(2);              //this.RegistroRdL.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(19);
                                                if (RisorseTeam != null)
                                                {
                                                    if (RisorseTeam.RisorsaCapo != null)
                                                    {
                                                        if (RisorseTeam.RisorsaCapo.SecurityUser == null)  /// se assegnata deve avere per forza il username di smartphone
                                                        {
                                                            this.RisorseTeam = null;
                                                        }
                                                    }
                                                }
                                                break;
                                            case 5:
                                                this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(4);
                                                break;
                                            case 7:
                                                this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(13);
                                                break;
                                            case 6:
                                                this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(12);
                                                break;
                                            case 4: //4	Lavorazione Conclusa
                                                if (this.UltimoStatoOperativo != null)
                                                    this.UltimoStatoOperativo.Reload();
                                                this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(11);


                                                this.DataCompletamento = DateTime.Now;
                                                break;
                                            case 11: //11	Gestione da Sala Operativa
                                                this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(19);
                                                // this.fListaFiltraComboRisorseTeam = null;
                                                break;
                                            default:
                                                this.UltimoStatoOperativo = null;
                                                break;
                                        }
                                        #endregion

                                        suppressOnChanged = false;
                                    }
                                }
                                catch
                                {
                                    this.RisorseTeam = null;
                                }
                                finally
                                {
                                    suppressOnChanged = false;
                                }
                            }
                            break;
                        //case "UltimoStatoOperativo":
                        //    if (newValue != null && newValue != oldValue)
                        //    {
                        //        #region aggiorna registro rdl
                        //        //this.RegistroRdL.UltimoStatoOperativo = ((StatoOperativo)(newValue)); 
                        //        #endregion
                        //    }
                        //    break;
                        case "RisorseTeam":
                            if (!suppressOnChanged)
                            {
                                suppressOnChanged = true;
                                if (newValue != null)
                                {
                                    this.DataAssegnazioneOdl = DateTime.Now;
                                    //StatoSmistamento temp = this.UltimoStatoSmistamento;                                    //this.UltimoStatoSmistamento = null;                                    //this.UltimoStatoSmistamento = temp;
                                }
                                else
                                {
                                    this.DataAssegnazioneOdl = DateTime.MinValue;
                                }
                                suppressOnChanged = false;
                            }
                            break;
                        case "DataCompletamento":
                            if (!suppressOnChanged)
                            {
                                suppressOnChanged = true;
                                if (newValue != null)
                                {
                                    this.DataCompletamentoSistema = DateTime.Now;
                                }
                                else
                                {
                                    this.DataCompletamentoSistema = DateTime.MinValue;
                                }
                                suppressOnChanged = false;
                            }
                            break;

                        case "DataPianificata":
                            if (!suppressOnChanged)
                            {
                                suppressOnChanged = true;
                                if (newValue != null)
                                {

                                    this.DataPianificataEnd = ((DateTime)newValue).AddMinutes(30);
                                }
                                else
                                {
                                    this.DataPianificataEnd = DateTime.MinValue;
                                }
                                suppressOnChanged = false;
                            }
                            break;


                    }
                    #endregion
                }
            }

        }

        #region  preietta in mappa quadro è apparato  @"Iif(Immobile is null,true,not(Immobile.Commesse.LivelloAutorizzazioneGuasto))", Enabled = false)
        [NonPersistent]
        private XPCollection<AssetoMap> _AssetMaps;
        [DevExpress.ExpressApp.DC.XafDisplayName("Mappa")]
        [DevExpress.ExpressApp.CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        [Appearance("RdL.AssetMaps.LayoutItemVisibility", AppearanceItemType.LayoutItem,
             @"Iif (Oid=-1,true, Iif ( Asset.GeoLocalizzazione is null, true,false) )", //Apparato is null and 
            TargetItems = "AssetMaps", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<AssetoMap> AssetMaps
        {
            get
            {
                if (Asset == null)
                    return null;

                if (Asset.GeoLocalizzazione == null)
                    return null;

                int oidAppPadre = 0;
                int oidApp = Asset.Oid;

                if (Asset.AssetoPadre != null)
                {
                    oidAppPadre = Asset.AssetoPadre.Oid;
                }

                _AssetMaps = new XPCollection<AssetoMap>(Session, CriteriaOperator.Parse(
                    "OidAsset = ? Or OidAsset = ?", oidApp, oidAppPadre));
                return _AssetMaps;
            }
        }
        #endregion

        //    [RuleFromBoolProperty(
        //"RuleFromBoolPropertyObject.IsComplexExpressionValid_RuleFromBoolProperty",
        //DefaultContexts.Save,
        //"The MustBeTrue property must be set to true, the LengthMoreThan10 property's value length must be whose length is more than 10 and the ContainsValid property value must contain 'Valid'",
        //UsedProperties = "ContainsValid,LengthMoreThan10,MustBeTrue", SkipNullOrEmptyValues = false)]
        //    [NonPersistent]
        //    public bool IsComplexExpressionValid
        //    {
        //        get
        //        {
        //            return containsValid.Contains("Valid")
        //                && lengthMoreThan10.Length > 10
        //                && mustBeTrue;
        //        }
        //    }

        //protected override void OnSaving()
        //{
        //    base.OnSaving();

        //    System.Diagnostics.Debug.WriteLine(String.Format("e salvato {0} is saving {1}", IsInvalidated, IsSaving));
        //    //if (Session.IsNewObject(this))
        //    //{
        //        System.Diagnostics.Debug.WriteLine(String.Format("e salvato {0}  ", Session.GetObjectsToSave().Count));
        //    //}
        //}

        //protected override void OnSaved()
        //{
        //    base.OnSaved();
        //    Debug.WriteLine(DateTime.Now.ToString() + "RdL - OnSaved");
        //    // fListaFiltraComboSmistamento = null;
        //}

        public override string ToString()
        {
            if (this.Oid == -1) return null;
            if (this.Descrizione == null) return null;

            if (Codice != null)
                return string.Format("{0}({1})", Descrizione.Length < 101 ? Descrizione : Descrizione.Remove(100) + "...", Codice);
            else
                return string.Format("{0}", Descrizione.Length < 101 ? Descrizione : Descrizione.Remove(100) + "...");
        }
    }
}






#region dettaglio campi calcolati   commenntati
#endregion

//[Indices("Name", "Name;Age", "Age;ChildCount")]
//public class Person : XPObject
//{   [Size(32)]
//    public String Name;
//    [Indexed(Unique = true), Size(64)]
//    public String FullName;
//    public int Age;
//    public int ChildCount;}

//    [RuleCriteria("RuleInfo.RdL.DataPianificataEnd", DefaultContexts.Save
//    , @"[DataPianificataEnd] <= [DataPianificata] And Categoria.Oid = 4 And [UltimoStatoSmistamento.Oid] = 2 And [UltimoStatoSmistamento.Oid] != old_SSmistamento_Oid",
//CustomMessageTemplate = "Informazione: La Data Pianificata Fine ({DataPianificataEnd}) deve essere maggiore della data pianificata ({DataPianificata}).",
//SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Warning)]
//    [RuleCriteria("RuleInfo.RdL.noModificaMPcount", DefaultContexts.Save
//    , @"[UltimoStatoSmistamento.Oid] In(2,10,11) And Categoria.Oid In(1,5) And [UltimoStatoSmistamento.Oid] != old_SSmistamento_Oid",
//CustomMessageTemplate = "Informazione: non + possibile modificare una Attività MP compresa nel Registro RdL ({RegistroRdL}) ad accorpamento multipla.",
//SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Warning)][UltimoStatoSmistamento.Oid] In(2,10,11) And Categoria.Oid In(1,5) And [UltimoStatoSmistamento.Oid] != old_SSmistamento_Oid
//[Appearance("RdL.Autorizzazione.Visibility", TargetItems = "Autorizzazione",
// Criteria = @"Immobile.Commesse.LivelloAutorizzazioneGuasto != 4", Visibility = ViewItemVisibility.Hide)]  //  nrRdL
// [Appearance("RdL.Autorizzazione.Enabled", TargetItems = "StatoAutorizzativo",
//Criteria = @"Iif(Immobile is null,true,not(Immobile.Commesse.LivelloAutorizzazioneGuasto))", Enabled = false)]  //  nrRdL
//[Appearance("RdL.Richiedente.LayoutItemVisibility", AppearanceItemType.LayoutItem,
//     @"Categoria.Oid != 4",
//    TargetItems = "PanRichiedente2;PanRichiedente1", Visibility = ViewItemVisibility.Hide)]

//  MostraSoddisfazioneCliente                 [Appearance("RdL.SSmistamento.Assegnata.Enabled", TargetItems = "UltimoStatoSmistamento",
//Criteria = @"UltimoStatoSmistamento.Oid In(2,3,4,5,6,7,8,9,10)", Enabled = true)]
//[Appearance("RdL.DateOperative.DataCompletamento.Visibile", TargetItems = "DataCompletamento;NoteCompletamento;RdL_col6",
//    Criteria = @"UltimoStatoSmistamento.Oid != 4", Visibility = ViewItemVisibility.Hide)]
//CAMS.Module.Classi.SetVarSessione.IsAdminRuolo
//[Appearance("RdL.Mai.Editabili", TargetItems = "UtenteCreatoRichiesta;DataCreazione;Codice;CodOdL;CodRegRdL;DataCompletamentoSistema;StatoAutorizzativo", FontColor = "Black", Enabled = false)]
//[Appearance("RdL.TipoIntervento.Enabled", TargetItems = "TipoIntervento", Criteria = @"Categoria.Oid != 4",
//    Visibility = ViewItemVisibility.Hide)]