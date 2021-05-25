using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CAMS.Module.Classi;
using CAMS.Module.DBTask;
using CAMS.Module.PropertyEditors;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//namespace CAMS.Module.DBGestOrari
//{
//    class GestioneSettimanaTipo
//    {
//    }
//}
namespace CAMS.Module.DBGestOrari
{
    [DefaultClassOptions, Persistent("GESTIONESETTIMANATIPO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Gestione Settimana Tipo")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem("Gestione Orari")]
    [DefaultProperty("Descrizione")]
    //[RuleCombinationOfPropertiesIsUnique("RuleUnic.GestioneOrari.Descrizione", DefaultContexts.Save, "Descrizione, SecurityUser, Stagione")]
    //[Appearance("GestioneOrari.panToolF4.noVisibile", AppearanceItemType.LayoutItem,
    //    @"Oid == -1 Or 1=1", TargetItems = "ToolF5", Visibility = ViewItemVisibility.Hide)]
    //[Appearance("GestioneOrari.panToolF1234.noVisibile", AppearanceItemType.LayoutItem,
    //       @"Oid == -1", TargetItems = "ToolF4", Visibility = ViewItemVisibility.Hide)]
    //[Appearance("GestioneOrari.Colorefield.SAutorizzativo.colore1", AppearanceItemType.ViewItem, @"SAutorizzativo.Oid =8",
    //         TargetItems = "SAutorizzativo", Context = "Any", Priority = 1, FontColor = "Black")]
    //[Appearance("GestioneOrari.Colorefield.SAutorizzativo.colore2", AppearanceItemType.ViewItem, @"SAutorizzativo.Oid =9",
    //         TargetItems = "SAutorizzativo", Context = "Any", Priority = 1, FontColor = "Violet")]
    //[Appearance("GestioneOrari.Colorefield.SAutorizzativo.colore3", AppearanceItemType.ViewItem, @"SAutorizzativo.Oid =10",
    //         TargetItems = "SAutorizzativo", Context = "Any", Priority = 1, FontColor = "Blue")]
    //[Appearance("GestioneOrari.Colorefield.SAutorizzativo.colore4", AppearanceItemType.ViewItem, @"SAutorizzativo.Oid =11",
    //         TargetItems = "SAutorizzativo", Context = "Any", Priority = 1, FontColor = "Green")]
    //[Appearance("GestioneOrari.Colorefield.SAutorizzativo.color6", AppearanceItemType.ViewItem, @"SAutorizzativo.Oid =12",
    //         TargetItems = "SAutorizzativo", Context = "Any", Priority = 1, FontColor = "LimeGreen")]
    //[Appearance("GestioneOrari.Colorefield.SAutorizzativo.color7", AppearanceItemType.ViewItem, @"SAutorizzativo.Oid =13",
    //         TargetItems = "SAutorizzativo", Context = "Any", Priority = 1, FontColor = "Orange")]
    //[Appearance("GestioneOrari.Colorefield.SAutorizzativo.color8", AppearanceItemType.ViewItem, @"SAutorizzativo.Oid =14",
    //         TargetItems = "SAutorizzativo", Context = "Any", Priority = 1, FontColor = "Salmon")]
    //[Appearance("GestioneOrari.Colorefield.SAutorizzativo.color9", AppearanceItemType.ViewItem, @"SAutorizzativo.Oid =15",
    //         TargetItems = "SAutorizzativo", Context = "Any", Priority = 1, FontColor = "Red")]
    //[Appearance("GestioneOrari.nota.noVisibile", AppearanceItemType.LayoutItem,
    //    @"SAutorizzativo.Oid=9 Or SAutorizzativo.Oid=8", TargetItems = "Nota", Visibility = ViewItemVisibility.Hide)]
    //[Appearance("GestioneOrari.nota.enabled", TargetItems = "Nota",
    //    Criteria = @"(SAutorizzativo.Oid In(12,13,16))", FontColor = "Black", Enabled = false)]

    //    OID STATOAUTORIZZATIVO  FASE
    //8	Impostazione di Filtro Impostazione di Filtro
    //9	Modifica Orari  Modifica Orari
    //10	in attesa di approvazione	in attesa di approvazione
    //11	Approvato con modifiche Approvato con modifiche
    //12	Approvato Approvato
    //13	in lavorazione	in lavorazione
    //14	Annulato Annulato
    //15	Sospeso Sospeso
    //16	Completato Completato
    //[RuleCriteria("RC.GestioneOrari.Informativa.StatoAutorizzativo8", DefaultContexts.Save,
    //    @"SAutorizzativo.Oid In(8,9,10,11,12)",
    //CustomMessageTemplate = "Messaggio Informativo",
    //SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Information)]



    //#region  criteri di verifica data di motdifica record
    //[RuleCriteria("RC.GestioneOrari.Valida.DataModifica", "AzioneModificaGOrari_Data", @"[F1_ModDataOra] >= dataora_dal And [F1_ModDataOra] <= dataora_Al",
    //CustomMessageTemplate = "la data di modifica deve essere compresa tra la data di dalle e alle!",
    //SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Error)]
    //#endregion

    //#region  criteri di verifica fascie orarie
    //[RuleCriteria("RC.GestioneOrari.Valida.Fascia1_Time", DefaultContexts.Save,
    //    @"(f1startTipoSetOrario = 0 And f1endTipoSetOrario=0) Or (f1startTipoSetOrario = 1 And f1endTipoSetOrario=1) Or (f1startTipoSetOrario <= (f1endTipoSetOrario - 8))",
    //CustomMessageTemplate = "Fascia 1: ora di start deve essere minore della ora di end e la differenza tra le ore di almeno 4 ore!",
    //SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Error)]

    //[RuleCriteria("RC.GestioneOrari.Valida.Fascia2_Time", DefaultContexts.Save,
    //     @"(f2startTipoSetOrario = 0 And f2endTipoSetOrario=0) Or (f2startTipoSetOrario = 1 And f2endTipoSetOrario=1) Or (f2startTipoSetOrario <= (f2endTipoSetOrario - 4))",
    //CustomMessageTemplate = "Fascia 2: ora di start deve essere minore della ora di end e la differenza tra le ore di almeno 2 ore!",
    //SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Error)]

    //[RuleCriteria("RC.GestioneOrari.Valida.Fascia3_Time", DefaultContexts.Save,
    //    @"(f3startTipoSetOrario = 0 And f3endTipoSetOrario=0) Or (f3startTipoSetOrario = 1 And f3endTipoSetOrario=1) Or (f3startTipoSetOrario <= (f3endTipoSetOrario - 4))",
    //CustomMessageTemplate = "Fascia 3: ora di start deve essere minore della ora di end e la differenza tra le ore di almeno 2 ore!",
    //SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Error)]

    //[RuleCriteria("RC.GestioneOrari.Valida.Fascia4_Time", DefaultContexts.Save,
    //    @"(f4startTipoSetOrario = 0 And f4endTipoSetOrario=0) Or (f4startTipoSetOrario = 1 And f4endTipoSetOrario=1) Or (f4startTipoSetOrario <= (f4endTipoSetOrario - 4))",
    //CustomMessageTemplate = "Fascia 4: ora di start deve essere minore della ora di end e la differenza tra le ore di almeno 2 ore!",
    //SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Error)]

    //[RuleCriteria("RC.GestioneOrari.Valida.Fascia12_Time", DefaultContexts.Save,
    //     @"((f1endTipoSetOrario>1 and f2startTipoSetOrario>1) and (f2startTipoSetOrario <f1endTipoSetOrario + 1))",
    //CustomMessageTemplate = "Fascia 2: ora start deve essere maggiore della data di end della Fascia 1 ",
    //SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    //[RuleCriteria("RC.GestioneOrari.Valida.Fascia23_Time", DefaultContexts.Save,
    //     @"((f2endTipoSetOrario>1 and f3startTipoSetOrario>1) and (f3startTipoSetOrario <f2endTipoSetOrario + 1))",
    //CustomMessageTemplate = "Fascia 3: ora start deve essere maggiore della data di end della Fascia 2 ",
    //SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    //[RuleCriteria("RC.GestioneOrari.Valida.Fascia34_Time", DefaultContexts.Save,
    //     @"((f3endTipoSetOrario>1 and f4startTipoSetOrario>1) and (f4startTipoSetOrario <f3endTipoSetOrario + 1))",
    //CustomMessageTemplate = "Fascia 4: ora start deve essere maggiore della data di end della Fascia 3 ",
    //SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    //// DA QUI LE REGOLE PER IL PULSANTE AGGIORNA
    //[RuleCriteria("RC.GestioneOrari.AzioneModificaGOrari.Valida.fascie.nulle", "AzioneModificaGOrari_FascieNulle",
    // criteria: @"f1endTipoSetOrario < 9",
    ////targetContextIDs: "f1endTipoSetOrario",
    //CustomMessageTemplate = "Attenzione le Fascie Orarie di Modifica non sono state valorizzate!",
    //SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    //[RuleCriteria("RC.GestioneOrari.AzioneModificaGOrari.Valida.Fascia1_Time", "AzioneModificaGOrari",
    //    @"(f1startTipoSetOrario = 0 And f1endTipoSetOrario=0) Or (f1startTipoSetOrario = 1 And f1endTipoSetOrario=1) Or (f1startTipoSetOrario <= (f1endTipoSetOrario - 8))",
    //CustomMessageTemplate = "Fascia 1: ora di start deve essere minore della ora di end e la differenza tra le ore di almeno 4 ore!",
    //SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Error)]

    //[RuleCriteria("RC.GestioneOrari.AzioneModificaGOrari.Valida.Fascia2_Time", "AzioneModificaGOrari",
    //    @"(f2startTipoSetOrario = 0 And f2endTipoSetOrario=0) Or  (f2startTipoSetOrario = 1 And f2endTipoSetOrario=1) Or (f2startTipoSetOrario <= (f2endTipoSetOrario - 4))",
    //CustomMessageTemplate = "Fascia 2: ora di start deve essere minore della ora di end e la differenza tra le ore di almeno 2 ore!",
    //SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Error)]

    //[RuleCriteria("RC.GestioneOrari.AzioneModificaGOrari.Valida.Fascia3_Time", "AzioneModificaGOrari",
    //    @"(f3startTipoSetOrario = 0 And f3endTipoSetOrario=0) Or (f3startTipoSetOrario = 1 And f3endTipoSetOrario=1) Or (f3startTipoSetOrario <= (f3endTipoSetOrario - 4))",
    //CustomMessageTemplate = "Fascia 3: ora di start deve essere minore della ora di end e la differenza tra le ore di almeno 2 ore!",
    //SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Error)]

    //[RuleCriteria("RC.GestioneOrari.AzioneModificaGOrari.Valida.Fascia4_Time", "AzioneModificaGOrari",
    //    @"(f4startTipoSetOrario = 0 And f4endTipoSetOrario=0) Or (f4startTipoSetOrario = 1 And f4endTipoSetOrario=1) Or (f4startTipoSetOrario <= (f4endTipoSetOrario - 4))",
    //CustomMessageTemplate = "Fascia 4: ora di start deve essere minore della ora di end e la differenza tra le ore di almeno 2 ore!",
    //SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Error)]

    //[RuleCriteria("RC.GestioneOrari.Valida.Fascia12A_Time", "AzioneModificaGOrari",
    //     @"((f1endTipoSetOrario>1 and f2startTipoSetOrario>1) and (f2startTipoSetOrario <f1endTipoSetOrario + 1))",
    //CustomMessageTemplate = "Fascia 2: ora start deve essere maggiore della data di end della Fascia 1 ",
    //SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    //[RuleCriteria("RC.GestioneOrari.Valida.Fascia23A_Time", "AzioneModificaGOrari",
    //     @"((f2endTipoSetOrario>1 and f3startTipoSetOrario>1) and (f3startTipoSetOrario <f2endTipoSetOrario + 1))",
    //CustomMessageTemplate = "Fascia 3: ora start deve essere maggiore della data di end della Fascia 2 ",
    //SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    //[RuleCriteria("RC.GestioneOrari.Valida.Fascia34A_Time", "AzioneModificaGOrari",
    //     @"((f3endTipoSetOrario>1 and f4startTipoSetOrario>1) and (f4startTipoSetOrario <f3endTipoSetOrario + 1))",
    //CustomMessageTemplate = "Fascia 4: ora start deve essere maggiore della data di end della Fascia 3 ",
    //SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    //#endregion

    public class GestioneSettimanaTipo : XPObject
    {
        public GestioneSettimanaTipo() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public GestioneSettimanaTipo(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        [PersistentAlias("Oid")]
        public int Codice
        {
            get
            {
                object tempObject = EvaluateAlias("Codice");
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

        string fDescrizione;
        [System.ComponentModel.DisplayName("Descrizione")]
        [RuleRequiredField("RuleReq.GestioneSettimanaTipo.Descrizione.Save", DefaultContexts.Save, "Descrizione è un campo obbligatorio!, inserisci una descrizione della Gestione Orari che stai inserendo")]
        [RuleRequiredField("RuleReq.GestioneSettimanaTipo.Descrizione.AzioneModificaGOrari", "AzioneModificaGOrari", "Descrizione è un campo obbligatorio!, inserisci una descrizione della Gestione Orari che stai inserendo")]
        //[RuleUniqueValue("RuleUni.GestioneOrari.Descrizione", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, CustomMessageTemplate ="la descrizione deve essere univoca")]

        [Size(300)]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>(nameof(Descrizione), ref fDescrizione, value); }
        }



        string fNota;
        [System.ComponentModel.DisplayName("Nota")]
        [Size(300)]
        public string Nota
        {
            get { return fNota; }
            set { SetPropertyValue<string>(nameof(Nota), ref fNota, value); }
        }



        private StatoAutorizzativo fSAutorizzativo;
        [Persistent("AUTORIZZAZIONE"), System.ComponentModel.DisplayName("Autorizzazione")]
        [DataSourceProperty("StatoAutorizzativoList", DataSourcePropertyIsNullMode.SelectNothing)]
        //[DataSourceCriteria(" Oid In(Escalation)")]
        //[DataSourceProperty("StatoAutorizzativoList")]
        //[Delayed(true)]
        public StatoAutorizzativo SAutorizzativo
        {
            get { return fSAutorizzativo; }
            set { SetPropertyValue<StatoAutorizzativo>(nameof(SAutorizzativo), ref fSAutorizzativo, value); }
        }

        private List<StatoAutorizzativo> fStatoAutorizzativoList;
        [Browsable(false)]
        private List<StatoAutorizzativo> StatoAutorizzativoList
        {
            get
            {
                // cornelio labeone 47
                if (SAutorizzativo != null)
                    if (fStatoAutorizzativoList == null)
                    {
                        string SAutorizzativoEscalation = SAutorizzativo.Escalation;
                        CriteriaOperator op = CriteriaOperator.Parse("[Oid] In(?)", SAutorizzativoEscalation);
                        string[] subs = SAutorizzativoEscalation.Split(';');

                        fStatoAutorizzativoList = new XPQuery<StatoAutorizzativo>(Session)
                            .Where(w => subs.Contains(w.Oid.ToString())).ToList();
                    }
                return fStatoAutorizzativoList;
            }
        }

        // BackColor = "Red",
        [PersistentAlias("Iif(SAutorizzativo is null,null,SAutorizzativo.MessaggioInformativo)")]
        [Appearance("GestioneSettimanaTipo.MessaggioInformativo.BackColor.Red", FontColor = "Red", Priority = 1, Criteria = "SAutorizzativo is not null")]
        //[ModelDefault("DisplayFormat", "{0:C}")] //[ModelDefault("EditMask", "C")]
        public string MessaggioInformativo
        {
            get
            {
                var tempObject = EvaluateAlias("MessaggioInformativo");
                if (tempObject != null)
                {
                    return Convert.ToString(tempObject);
                }
                return "";
            }
        }


        string fStagione;
        [Indexed(Name = @"GestioneSettimanaTipo_idx_Stagione")]
        [Size(255)]
        [ImmediatePostData(true)]
        [EditorAlias(CAMSEditorAliases.CustomStringStagioneEditor)]
        public string Stagione
        {
            get { return fStagione; }
            set { SetPropertyValue<string>(nameof(Stagione), ref fStagione, value); }
        }


        [Persistent("CIRCUITO"), System.ComponentModel.DisplayName("Circuiti")]
        [DataSourceCriteria("stagione == '@This.Stagione'")]
        //[DataSourceCriteria("[<CommessePriorita>][^.Oid == Priorita.Oid And Commesse.Oid = '@This.Edificio.Commesse.Oid' And Gruppo=='@This.Impianto.Gruppo']")]
        //[RuleRequiredField("RuleReq.RdL.Priorita", DefaultContexts.Save, "Priorita è un campo obbligatorio")]
        [Delayed(true)]
        public tbcircuiti Circuito
        {
            get { return GetDelayedPropertyValue<tbcircuiti>("Circuito"); }
            set { SetDelayedPropertyValue<tbcircuiti>("Circuito", value); }

        }

        
        #region   DATE FASCIA ORARIA 1

        TipoSetOrario ff1startTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F1 Dalle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f1startTipoSetOrario
        {
            get { return ff1startTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f1startTipoSetOrario), ref ff1startTipoSetOrario, value); }
        }
        TipoSetOrario ff1endTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F1 Alle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [DataSourceCriteria("f1endTipoSetOrario > 8")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f1endTipoSetOrario
        {
            get { return ff1endTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f1endTipoSetOrario), ref ff1endTipoSetOrario, value); }
        }

        #endregion

        #region   DATE FASCIA ORARIA 2

        TipoSetOrario ff2startTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F2 Dalle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f2startTipoSetOrario
        {
            get { return ff2startTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f2startTipoSetOrario), ref ff2startTipoSetOrario, value); }
        }
        TipoSetOrario ff2endTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F2 Alle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f2endTipoSetOrario
        {
            get { return ff2endTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f2endTipoSetOrario), ref ff2endTipoSetOrario, value); }
        }

       
        #endregion

        #region   DATE FASCIA ORARIA 3

        TipoSetOrario ff3startTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F3 Dalle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f3startTipoSetOrario
        {
            get { return ff3startTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f3startTipoSetOrario), ref ff3startTipoSetOrario, value); }
        }
        TipoSetOrario ff3endTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F3 Alle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f3endTipoSetOrario
        {
            get { return ff3endTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f3endTipoSetOrario), ref ff3endTipoSetOrario, value); }
        }

        #endregion

        #region   DATE FASCIA ORARIA 4

        TipoSetOrario ff4startTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F4 Dalle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f4startTipoSetOrario
        {
            get { return ff4startTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f4startTipoSetOrario), ref ff4startTipoSetOrario, value); }
        }
        TipoSetOrario ff4endTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F4 Alle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f4endTipoSetOrario
        {
            get { return ff4endTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f4endTipoSetOrario), ref ff4endTipoSetOrario, value); }
        }


        #endregion

        #region   DATE FASCIA ORARIA 5

        //TipoSetOrario ff5startTipoSetOrario;
        //[DevExpress.ExpressApp.DC.XafDisplayName("F5 Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public TipoSetOrario f5startTipoSetOrario
        //{
        //    get { return ff5startTipoSetOrario; }
        //    set { SetPropertyValue<TipoSetOrario>(nameof(f5startTipoSetOrario), ref ff5startTipoSetOrario, value); }
        //}
        //TipoSetOrario ff5endTipoSetOrario;
        //[DevExpress.ExpressApp.DC.XafDisplayName("F5 Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public TipoSetOrario f5endTipoSetOrario
        //{
        //    get { return ff5endTipoSetOrario; }
        //    set { SetPropertyValue<TipoSetOrario>(nameof(f5endTipoSetOrario), ref ff5endTipoSetOrario, value); }
        //}

        //string ff5start;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public string f5start
        //{
        //    get { return ff5start; }
        //    set { SetPropertyValue<string>(nameof(f5start), ref ff5start, value); }
        //}
        //string ff5end;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public string f5end
        //{
        //    get { return ff5end; }
        //    set { SetPropertyValue<string>(nameof(f5end), ref ff5end, value); }
        //}

        #endregion


        //[Association("GestioneOrari_tbCalendarioUtente", typeof(tbCalendarioUtente)), DevExpress.Xpo.Aggregated]
        private XPCollection<tbSettimanaTipoUtente> tbSettimanaTipoUtentes_;
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        [DevExpress.ExpressApp.DC.XafDisplayName("Elenco Richieste Approvate Settimana Tipo")]
        //[Appearance("RdL.MpAttivitaPianificateDetts.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or Categoria.Oid != 1 Or MpAttivitaPianificateDetts.Count = 0", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<tbSettimanaTipoUtente> tbSettimanaTipoUtentes
        {
            get
            {
                if (Oid == -1)

                { return null; }
                if (new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(SAutorizzativo.Oid))
                { return null; }
                else
                {
                    if (tbSettimanaTipoUtentes_ == null)
                    {
                        var OidSettimanaTipoUtente = this.Oid;
                        GroupOperator criteria_tbSettimanaTipoUtente = new GroupOperator();
                        CriteriaOperator opOr = CriteriaOperator.Parse("GestioneSettimanaTipo = ?", OidSettimanaTipoUtente);
                        criteria_tbSettimanaTipoUtente.Operands.Add(opOr);
                        opOr = CriteriaOperator.Parse("statoEAMS = ?", "APP");
                        criteria_tbSettimanaTipoUtente.Operands.Add(opOr);

                        tbSettimanaTipoUtentes_ = new XPCollection<tbSettimanaTipoUtente>(Session, criteria_tbSettimanaTipoUtente)
                        {
                            BindingBehavior = CollectionBindingBehavior.AllowNone
                        };
                    }
                }
                return tbSettimanaTipoUtentes_;
            }
        }


        private XPCollection<SettimanaTipo> ListSettimanaTIpoRichiestaUtente_;
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        //[Appearance("RdL.NotificheEmergenze.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or UltimoStatoSmistamento.Oid != 10", Visibility = ViewItemVisibility.Hide)]
        [DevExpress.ExpressApp.DC.XafDisplayName("Elenco Richieste Utente Settimana Tipo")]
        public XPCollection<SettimanaTipo> ListSettimanaTipoRichiestaUtentes
        {
            get
            {
                if (Oid == -1)
                { return null; }

                if (new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(SAutorizzativo.Oid))
                { return null; }
                else
                {
                    if (ListSettimanaTIpoRichiestaUtente_ == null)
                    {
                        var OidGestioneOrariUtente = this.Oid;
                        GroupOperator criteria_tbCalendarioUtente = new GroupOperator();
                        CriteriaOperator opOr = CriteriaOperator.Parse("GestioneOrari = ?", OidGestioneOrariUtente);
                        criteria_tbCalendarioUtente.Operands.Add(opOr);
                        opOr = CriteriaOperator.Parse("statoEAMS = ?", "INS");
                        criteria_tbCalendarioUtente.Operands.Add(opOr);

                        ListSettimanaTIpoRichiestaUtente_ = new XPCollection<tbCalendarioUtente>(Session, criteria_tbCalendarioUtente)
                        {
                            BindingBehavior = CollectionBindingBehavior.AllowNone
                        };
                    }
                }
                return ListSettimanaTIpoRichiestaUtente_;
            }
        }



        //DateTime fdataora_dal;
        //[ImmediatePostData(true)]
        //public DateTime dataora_dal
        //{
        //    get { return fdataora_dal; }
        //    set { SetPropertyValue<DateTime>(nameof(dataora_dal), ref fdataora_dal, value); }
        //}

        //DateTime fdataora_Al;
        //[ImmediatePostData(true)]
        //public DateTime dataora_Al
        //{
        //    get { return fdataora_Al; }
        //    set { SetPropertyValue<DateTime>(nameof(dataora_Al), ref fdataora_Al, value); }
        //}

        //[Persistent("CIRCUITO"), System.ComponentModel.DisplayName("Circuiti")]
        //[DataSourceCriteria("stagione == '@This.Stagione'")]
        ////[DataSourceCriteria("[<CommessePriorita>][^.Oid == Priorita.Oid And Commesse.Oid = '@This.Edificio.Commesse.Oid' And Gruppo=='@This.Impianto.Gruppo']")]
        ////[RuleRequiredField("RuleReq.RdL.Priorita", DefaultContexts.Save, "Priorita è un campo obbligatorio")]
        //[Delayed(true)]
        //public tbcircuiti Circuito
        //{
        //    get { return GetDelayedPropertyValue<tbcircuiti>("Circuito"); }
        //    set { SetDelayedPropertyValue<tbcircuiti>("Circuito", value); }

        //}

        //[Persistent("SECURITYUSERID"), XafDisplayName("Security User")]
        ////[RuleUniqueValue("ParametriPivot.SecuritySystemUser.", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //[Delayed(true)]
        //public SecuritySystemUser SecurityUser
        //{
        //    get { return GetDelayedPropertyValue<SecuritySystemUser>("SecurityUser"); }
        //    set { SetDelayedPropertyValue<SecuritySystemUser>("SecurityUser", value); }

        //}


        //[Persistent("SECURITYUSERIDAPPROVATORE"), XafDisplayName("Security User Approvatore")]
        ////[RuleUniqueValue("ParametriPivot.SecuritySystemUser.", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //[Delayed(true)]
        //public SecuritySystemUser SecurityUserApprovatore
        //{
        //    get { return GetDelayedPropertyValue<SecuritySystemUser>("SecurityUserApprovatore"); }
        //    set { SetDelayedPropertyValue<SecuritySystemUser>("SecurityUserApprovatore", value); }

        //}

        //[Persistent("DATACREAZIONE"), XafDisplayName("Data Inserimento")]
        //[Delayed(true)]
        //public DateTime DataInserimento
        //{
        //    get { return GetDelayedPropertyValue<DateTime>("DataInserimento"); }
        //    set { SetDelayedPropertyValue<DateTime>("DataInserimento", value); }
        //}

        //[Persistent("DATAAGGIORNAMENTO"), XafDisplayName("Data Aggiornamento")]
        //[Delayed(true)]
        //public DateTime DataAggiornamento
        //{
        //    get { return GetDelayedPropertyValue<DateTime>("DataAggiornamento"); }
        //    set { SetDelayedPropertyValue<DateTime>("DataAggiornamento", value); }
        //}

        //[Persistent("GESTIONEORARIUTENTE"), System.ComponentModel.DisplayName("GestioneOrari Utente")]
        ////[DataSourceCriteria("[<CommessePriorita>][^.Oid == Priorita.Oid And Commesse.Oid = '@This.Edificio.Commesse.Oid' And Gruppo=='@This.Impianto.Gruppo']")]
        ////[RuleRequiredField("RuleReq.RdL.Priorita", DefaultContexts.Save, "Priorita è un campo obbligatorio")]
        //[Delayed(true)]
        //public GestioneOrari GestioneOrariUtente
        //{
        //    get { return GetDelayedPropertyValue<GestioneOrari>("GestioneOrariUtente"); }
        //    set { SetDelayedPropertyValue<GestioneOrari>("GestioneOrariUtente", value); }

        //}

        ////  multi circuiti
        //[Association("GestioneOrari_GestioneOrariCircuiti", typeof(GestioneOrariCircuiti)), DevExpress.Xpo.Aggregated]
        //[DevExpress.ExpressApp.DC.XafDisplayName("GestioneOrariCircuiti Dettaglio")]
        ////[Appearance("RdL.MpAttivitaPianificateDetts.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or Categoria.Oid != 1 Or MpAttivitaPianificateDetts.Count = 0", Visibility = ViewItemVisibility.Hide)]
        //public XPCollection<GestioneOrariCircuiti> GestioneOrariCircuitis
        //{
        //    get
        //    {
        //        if (this.Oid == -1) return null;
        //        return GetCollection<GestioneOrariCircuiti>("GestioneOrariCircuitis");
        //    }
        //}

        ////[Association("GestioneOrari_tbCalendarioUtente", typeof(tbCalendarioUtente)), DevExpress.Xpo.Aggregated]
        //private XPCollection<tbCalendarioUtente> tbCalendarioUtentes_;
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Elenco Richieste Approvate su Calendario")]
        ////[Appearance("RdL.MpAttivitaPianificateDetts.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or Categoria.Oid != 1 Or MpAttivitaPianificateDetts.Count = 0", Visibility = ViewItemVisibility.Hide)]
        //public XPCollection<tbCalendarioUtente> tbCalendarioUtentes
        //{
        //    get
        //    {
        //        if (Oid == -1)

        //        { return null; }
        //        if (new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(SAutorizzativo.Oid))
        //        { return null; }
        //        else
        //        {
        //            if (tbCalendarioUtentes_ == null)
        //            {
        //                var OidGestioneOrariUtente = this.Oid;
        //                GroupOperator criteria_tbCalendarioUtente = new GroupOperator();
        //                CriteriaOperator opOr = CriteriaOperator.Parse("GestioneOrari = ?", OidGestioneOrariUtente);
        //                criteria_tbCalendarioUtente.Operands.Add(opOr);
        //                opOr = CriteriaOperator.Parse("statoEAMS = ?", "APP");
        //                criteria_tbCalendarioUtente.Operands.Add(opOr);

        //                tbCalendarioUtentes_ = new XPCollection<tbCalendarioUtente>(Session, criteria_tbCalendarioUtente)
        //                {
        //                    BindingBehavior = CollectionBindingBehavior.AllowNone
        //                };
        //            }
        //        }
        //        return tbCalendarioUtentes_;
        //    }
        //}

        //private XPCollection<tbcalendario> ftbcalendario_;
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        ////[Appearance("RdL.NotificheEmergenze.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or UltimoStatoSmistamento.Oid != 10", Visibility = ViewItemVisibility.Hide)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("tbCalendario")]
        //public XPCollection<tbcalendario> tbCalendarios
        //{
        //    get
        //    {
        //        if (Oid == -1)
        //        { return null; }
        //        else
        //        {
        //            if (ftbcalendario_ == null)
        //            {
        //                GroupOperator criteriaOP_OR = new GroupOperator();
        //                CriteriaOperator opOr = CriteriaOperator.Parse("idTicketEAMS = ?", this.Oid);
        //                criteriaOP_OR.Operands.Add(opOr);

        //                GroupOperator criteriaOP2 = new GroupOperator(GroupOperatorType.And);
        //                CriteriaOperator op = CriteriaOperator.Parse("vnoflag == ?", "Y");
        //                criteriaOP2.Operands.Add(op);
        //                op = CriteriaOperator.Parse("stagione == ?", this.Stagione);
        //                //criteriaOP2.Operands.Add(op);
        //                //op = CriteriaOperator.Parse("data > ?", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0));
        //                //criteriaOP2.Operands.Add(op);
        //                //op = CriteriaOperator.Parse("data between (?, ?)", this.dataora_dal, this.dataora_Al);
        //                op = CriteriaOperator.Parse("data >= ? And data <= ?", this.dataora_dal, this.dataora_Al);
        //                criteriaOP2.Operands.Add(op);



        //                if (this.Circuito != null)
        //                {
        //                    op = CriteriaOperator.Parse("idcircuito == ?", this.Circuito.idcircappic);
        //                    //criteriaOP2.Operands.Add(op);
        //                }
        //                //int clkD1 = Session.QueryInTransaction<GestioneOrariCircuiti>().Where(w => w.GestioneOrari.Oid == this.Oid).Count();
        //                List<int> listOid = GestioneOrariCircuitis.Select(s => s.Circuiti.Oid).ToList();
        //                if (this.GestioneOrariCircuitis.Count() > 0 || this.Circuito != null)
        //                {
        //                    if (this.Circuito != null)
        //                    {
        //                        listOid.Add(this.Circuito.Oid);
        //                    }
        //                    string joinOid = string.Join(",", listOid);

        //                    op = CriteriaOperator.Parse(string.Format("Circuiti.Oid in({0})", joinOid));
        //                    criteriaOP2.Operands.Add(op);
        //                }

        //                GroupOperator criteria_All = new GroupOperator(GroupOperatorType.Or, criteriaOP2, criteriaOP_OR);

        //                ftbcalendario_ = new XPCollection<tbcalendario>(Session, criteria_All)
        //                {
        //                    BindingBehavior = CollectionBindingBehavior.AllowNone
        //                };
        //            }
        //        }
        //        return ftbcalendario_;
        //    }
        //}

        //private XPCollection<tbCalendarioUtente> ListCalendariodiRichiestaUtente_;
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        ////[Appearance("RdL.NotificheEmergenze.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or UltimoStatoSmistamento.Oid != 10", Visibility = ViewItemVisibility.Hide)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Elenco Richieste Utente su Calendario")]
        //public XPCollection<tbCalendarioUtente> ListCalendariodiRichiestaUtentes
        //{
        //    get
        //    {
        //        if (Oid == -1)
        //        { return null; }

        //        if (new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(SAutorizzativo.Oid))
        //        { return null; }
        //        else
        //        {
        //            if (ListCalendariodiRichiestaUtente_ == null)
        //            {
        //                var OidGestioneOrariUtente = this.Oid;
        //                GroupOperator criteria_tbCalendarioUtente = new GroupOperator();
        //                CriteriaOperator opOr = CriteriaOperator.Parse("GestioneOrari = ?", OidGestioneOrariUtente);
        //                criteria_tbCalendarioUtente.Operands.Add(opOr);
        //                opOr = CriteriaOperator.Parse("statoEAMS = ?", "INS");
        //                criteria_tbCalendarioUtente.Operands.Add(opOr);

        //                ListCalendariodiRichiestaUtente_ = new XPCollection<tbCalendarioUtente>(Session, criteria_tbCalendarioUtente)
        //                {
        //                    BindingBehavior = CollectionBindingBehavior.AllowNone
        //                };
        //            }
        //        }
        //        return ListCalendariodiRichiestaUtente_;
        //    }
        //}


        //#region


        //#region PARAMETRI AZIONI MASSIVE SU CALENDARIO  --  non persistenti

        //TipoSelezioneGestioneOrarioGiornoSettimana fGiornoSettimana;
        //[NonPersistent]
        //public TipoSelezioneGestioneOrarioGiornoSettimana GiornoSettimana
        //{
        //    get { return fGiornoSettimana; }
        //    set { SetPropertyValue<TipoSelezioneGestioneOrarioGiornoSettimana>(nameof(GiornoSettimana), ref fGiornoSettimana, value); }
        //}

        //bool fSovrascriviEccezioni;
        //[NonPersistent]
        //public bool SovrascriviEccezioni
        //{
        //    get { return fSovrascriviEccezioni; }
        //    set { SetPropertyValue<bool>(nameof(SovrascriviEccezioni), ref fSovrascriviEccezioni, value); }
        //}
        //// F1
        //DateTime fF1_ModDataOra;
        ////[ImmediatePostData(true)]
        //[NonPersistent]
        //[EditorAlias(CAMSEditorAliases.Pers_GOrario_DataModifica_Editor)]//
        //public DateTime F1_ModDataOra
        //{
        //    get { return fF1_ModDataOra; }
        //    set { SetPropertyValue<DateTime>(nameof(F1_ModDataOra), ref fF1_ModDataOra, value); }
        //}
        //#endregion

        //#region   DATE FASCIA ORARIA 1

        //TipoSetOrario ff1startTipoSetOrario;
        //[DevExpress.ExpressApp.DC.XafDisplayName("F1 Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public TipoSetOrario f1startTipoSetOrario
        //{
        //    get { return ff1startTipoSetOrario; }
        //    set { SetPropertyValue<TipoSetOrario>(nameof(f1startTipoSetOrario), ref ff1startTipoSetOrario, value); }
        //}
        //TipoSetOrario ff1endTipoSetOrario;
        //[DevExpress.ExpressApp.DC.XafDisplayName("F1 Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[DataSourceCriteria("f1endTipoSetOrario > 8")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public TipoSetOrario f1endTipoSetOrario
        //{
        //    get { return ff1endTipoSetOrario; }
        //    set { SetPropertyValue<TipoSetOrario>(nameof(f1endTipoSetOrario), ref ff1endTipoSetOrario, value); }
        //}

        //#endregion

        //#region   DATE FASCIA ORARIA 2

        //TipoSetOrario ff2startTipoSetOrario;
        //[DevExpress.ExpressApp.DC.XafDisplayName("F2 Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public TipoSetOrario f2startTipoSetOrario
        //{
        //    get { return ff2startTipoSetOrario; }
        //    set { SetPropertyValue<TipoSetOrario>(nameof(f2startTipoSetOrario), ref ff2startTipoSetOrario, value); }
        //}
        //TipoSetOrario ff2endTipoSetOrario;
        //[DevExpress.ExpressApp.DC.XafDisplayName("F2 Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public TipoSetOrario f2endTipoSetOrario
        //{
        //    get { return ff2endTipoSetOrario; }
        //    set { SetPropertyValue<TipoSetOrario>(nameof(f2endTipoSetOrario), ref ff2endTipoSetOrario, value); }
        //}

        ////string ff2start;
        ////[Size(5)]
        ////[DevExpress.ExpressApp.DC.XafDisplayName("Dalle")]
        ////[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        ////[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        ////public string f2start
        ////{
        ////    get { return ff2start; }
        ////    set { SetPropertyValue<string>(nameof(f2start), ref ff2start, value); }
        ////}

        ////string ff2end;
        ////[Size(5)]
        ////[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
        ////[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        ////[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        ////public string f2end
        ////{
        ////    get { return ff2end; }
        ////    set { SetPropertyValue<string>(nameof(f2end), ref ff2end, value); }
        ////}
        //#endregion

        //#region   DATE FASCIA ORARIA 3

        //TipoSetOrario ff3startTipoSetOrario;
        //[DevExpress.ExpressApp.DC.XafDisplayName("F3 Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public TipoSetOrario f3startTipoSetOrario
        //{
        //    get { return ff3startTipoSetOrario; }
        //    set { SetPropertyValue<TipoSetOrario>(nameof(f3startTipoSetOrario), ref ff3startTipoSetOrario, value); }
        //}
        //TipoSetOrario ff3endTipoSetOrario;
        //[DevExpress.ExpressApp.DC.XafDisplayName("F3 Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public TipoSetOrario f3endTipoSetOrario
        //{
        //    get { return ff3endTipoSetOrario; }
        //    set { SetPropertyValue<TipoSetOrario>(nameof(f3endTipoSetOrario), ref ff3endTipoSetOrario, value); }
        //}

        ////string ff3start;
        ////[Size(5)]
        ////[DevExpress.ExpressApp.DC.XafDisplayName("Dalle")]
        ////[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        ////[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        ////public string f3start
        ////{
        ////    get { return ff3start; }
        ////    set { SetPropertyValue<string>(nameof(f3start), ref ff3start, value); }
        ////}
        ////string ff3end;
        ////[Size(5)]
        ////[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
        ////[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        ////[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        ////public string f3end
        ////{
        ////    get { return ff3end; }
        ////    set { SetPropertyValue<string>(nameof(f3end), ref ff3end, value); }
        ////}
        //#endregion

        //#region   DATE FASCIA ORARIA 4

        //TipoSetOrario ff4startTipoSetOrario;
        //[DevExpress.ExpressApp.DC.XafDisplayName("F4 Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public TipoSetOrario f4startTipoSetOrario
        //{
        //    get { return ff4startTipoSetOrario; }
        //    set { SetPropertyValue<TipoSetOrario>(nameof(f4startTipoSetOrario), ref ff4startTipoSetOrario, value); }
        //}
        //TipoSetOrario ff4endTipoSetOrario;
        //[DevExpress.ExpressApp.DC.XafDisplayName("F4 Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public TipoSetOrario f4endTipoSetOrario
        //{
        //    get { return ff4endTipoSetOrario; }
        //    set { SetPropertyValue<TipoSetOrario>(nameof(f4endTipoSetOrario), ref ff4endTipoSetOrario, value); }
        //}


        //#endregion

        //#region   DATE FASCIA ORARIA 5

        //TipoSetOrario ff5startTipoSetOrario;
        //[DevExpress.ExpressApp.DC.XafDisplayName("F5 Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public TipoSetOrario f5startTipoSetOrario
        //{
        //    get { return ff5startTipoSetOrario; }
        //    set { SetPropertyValue<TipoSetOrario>(nameof(f5startTipoSetOrario), ref ff5startTipoSetOrario, value); }
        //}
        //TipoSetOrario ff5endTipoSetOrario;
        //[DevExpress.ExpressApp.DC.XafDisplayName("F5 Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public TipoSetOrario f5endTipoSetOrario
        //{
        //    get { return ff5endTipoSetOrario; }
        //    set { SetPropertyValue<TipoSetOrario>(nameof(f5endTipoSetOrario), ref ff5endTipoSetOrario, value); }
        //}

        ////string ff5start;
        ////[Size(5)]
        ////[DevExpress.ExpressApp.DC.XafDisplayName("Dalle")]
        ////[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        ////[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        ////public string f5start
        ////{
        ////    get { return ff5start; }
        ////    set { SetPropertyValue<string>(nameof(f5start), ref ff5start, value); }
        ////}
        ////string ff5end;
        ////[Size(5)]
        ////[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
        ////[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        ////[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        ////public string f5end
        ////{
        ////    get { return ff5end; }
        ////    set { SetPropertyValue<string>(nameof(f5end), ref ff5end, value); }
        ////}

        //#endregion

        //#endregion






        //bool suppressOnChanged = false;
        //protected override void OnChanged(string propertyName, object oldValue, object newValue)
        //{
        //    base.OnChanged(propertyName, oldValue, newValue);
        //    if (!this.IsLoading)
        //    {
        //        if (this.Oid == -1 || this.Oid > -1)
        //        {
        //            string Sw_propertyName = propertyName;
        //            switch (Sw_propertyName)
        //            {
        //                case "dataora_dal":
        //                    if (newValue != oldValue && newValue != null)
        //                    {
        //                        ftbcalendario_ = null;
        //                        //RefreshfListaRdLSimilis();
        //                        OnChanged("tbCalendarios");
        //                    }
        //                    break;
        //                case "dataora_Al":

        //                    if (newValue != oldValue && newValue != null)
        //                    {
        //                        ftbcalendario_ = null;
        //                        //RefreshfListaRdLSimilis();
        //                        OnChanged("tbCalendarios");
        //                    }
        //                    break;
        //                case "Circuiti":
        //                    if (newValue != oldValue && newValue != null)
        //                    {
        //                        ftbcalendario_ = null;
        //                        //RefreshfListaRdLSimilis();
        //                        OnChanged("tbCalendarios");
        //                    }
        //                    break;
        //            }
        //        }
        //    }
        //}


    }
}
////string fstato;
////[Indexed(Name = @"idx_stato")]
////[Size(3)]
////public string stato
////{
////    get { return fstato; }
////    set { SetPropertyValue<string>(nameof(stato), ref fstato, value); }
////}
//string ff1start;
//[Size(5)]
//[DevExpress.ExpressApp.DC.XafDisplayName("Dalle")]
//[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
//[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
//public string f1start
//{
//    get { return ff1start; }
//    set { SetPropertyValue<string>(nameof(f1start), ref ff1start, value); }
//}
//string ff1end;
//[Size(5)]
//[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
//[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
//[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
//public string f1end
//{
//    get { return ff1end; }
//    set { SetPropertyValue<string>(nameof(f1end), ref ff1end, value); }
//}


//[Browsable(false)]
//private TimeSpan Fascia1_Time( string ora)
//{
//    TimeSpan ts = new TimeSpan( );
//    try
//    {
//        ts = TimeSpan.Parse(ora);
//    }
//    catch (Exception ex)
//    {
//        ts = TimeSpan.Parse("00:00");
//    }
//    return ts;
//}
//[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
//[Browsable(false)]
//public TimeSpan Fascia1_end_Time
//{
//    get
//    {
//        TimeSpan ts = new TimeSpan();
//        try
//        {
//            ts = TimeSpan.Parse(f1end);
//        }
//        catch (Exception ex)
//        {
//            ts = TimeSpan.Parse("00:00");
//        }
//        return ts;
//    }
//}

//string ff4start;
//[Size(5)]
//[DevExpress.ExpressApp.DC.XafDisplayName("Dalle")]
//[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
//[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
//public string f4start
//{
//    get { return ff4start; }
//    set { SetPropertyValue<string>(nameof(f4start), ref ff4start, value); }
//}
//string ff4end;
//[Size(5)]
//[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
//[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
//[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
//public string f4end
//{
//    get { return ff4end; }
//    set { SetPropertyValue<string>(nameof(f4end), ref ff4end, value); }
//}

//XPView xpv = new XPView(Session, typeof(StatoAutorizzativo));
//xpv.AddProperty("*", "*", true);
//xpv.Criteria = op;
//var aaaa = xpv.AsQueryable().GetEnumerator();
//foreach (ViewRecord r in xpv)
//    Console.WriteLine("QuestionID={0} AnswerAvg={1}", r["QuestionID"], r["AnswerAvg"]);
//var aa = Session.Query< StatoAutorizzativo >().
//fStatoAutorizzativo/*L*/ist = xpv.AsQueryable().Cast<StatoAutorizzativo>();