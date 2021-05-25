using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using CAMS.Module.DBPlant;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBTask;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Validation;
using CAMS.Module.Classi;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBControlliNormativi
{
    [DefaultClassOptions,    Persistent("CONTROLLINORMATIVI")]
    [System.ComponentModel.DisplayName("Avvisi Periodici")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    //[RuleCriteria("RuleError.ControlliNormativi.DataCompletamento", DefaultContexts.Save, @"(DataInizioControllo < DataCompletamento)",
    //CustomMessageTemplate = "La data di completamento deve essere maggiore della data di inizio controllo", SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Error)]
    [RuleCriteria("RuleError.ControlliNormativi.DataInizioControllo", DefaultContexts.Save, @"(DataInizioControllo is not null)",
    CustomMessageTemplate = "La data di inizio controllo non può essere nulla", SkipNullOrEmptyValues = false, ResultType = ValidationResultType.Error)]
    ////////Descrizione;Immobile;Impianto;Apparato;Frequenza;TipoControlloNormativo;Todos;DataInizioControllo
    //[Appearance("Abilita_Stato_Completato", TargetItems = "*", Criteria = "StatoControlloNormativo == 'Completato'", Enabled = false)]
    //////////[Appearance("Abilita_Stato_Pianificato", TargetItems = "Descrizione;DataInizioControllo;DataPianificataControllo;Frequenza;Immobile;Impianto;Apparato;TipoControlloNormativo;Precedente;StatoControlloNormativo;Successivo;Nome", Criteria = "StatoControlloNormativo == 'Pianificato'", Enabled = false)]
    [Appearance("Abilita_Stato_InCreazione", TargetItems = "DataCompletamento;NoteCompletamento;FileUpload;Precedente;Successivo", Criteria = "StatoControlloNormativo == 'InCreazione' Or  [Oid] < 0", Visibility = ViewItemVisibility.Hide)] //Enabled = false
    [NavigationItem("Avvisi Periodici")]
    [ImageName("BO_Lead")]
    public class ControlliNormativi : XPObject
    {
        public ControlliNormativi()
            : base()
        {
        }
        public ControlliNormativi(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
                this.StatoControlloNormativo = StatoControlloNormativo.InCreazione;
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy";


        private string fNome;
        [Size(250), Persistent("NOME")]
        [RuleRequiredField("RuleReq.ControlliNormativi.Nome", DefaultContexts.Save, "Nome è un campo obbligatorio")]
        [DbType("varchar(250)")]
        public string Nome
        {
            get
            {
                return fNome;
            }
            set
            {
                SetPropertyValue<string>("Nome", ref fNome, value);
            }
        }

        private string fDescrizione;
        [Size(4000), Persistent("DESCRIZIONE")]
        [RuleRequiredField("RuleReq.ControlliNormativi.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
        [DbType("varchar(4000)")]
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



       // private Mansioni fMansione;
       // [Persistent("MANSIONE"),
       //XafDisplayName("Mansione")]
       // [RuleRequiredField("RReqField.Risorse.Mansione", DefaultContexts.Save, "La Mansione è un campo obbligatorio")]
       // [ExplicitLoading()]
       // [Delayed(true)]
       // public Mansioni Mansione
       // {
       //     get
       //     {
       //         return GetDelayedPropertyValue<Mansioni>("Mansione");
       //     }
       //     set
       //     {
       //         SetDelayedPropertyValue<Mansioni>("Mansione", value);
       //     }

       // }
        
        
        
        private Immobile fImmobile;
        [Association(@"ControlliNormativi_Edificio"),
        Persistent("Immobile"),
        DisplayName("Immobile")]
        [RuleRequiredField("RuleReq.ControlliNormativi.Immobile", DefaultContexts.Save, "Immobile è un campo obbligatorio")]
        [Appearance("ControlliNormativi.Abilita.Immobile", Criteria = "not (Impianto  is null)", Enabled = false)]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public Immobile Immobile
        {
            get
            {
                return fImmobile;
            }
            set
            {
                SetPropertyValue<Immobile>("Immobile", ref fImmobile, value);
            }
        }

        private Servizio fServizio;
        [Association(@"ControlliNormativi_Servizio"),
        Persistent("SERVIZIO"),
        DisplayName("Servizio")]
        [Appearance("ControlliNormativi.Abilita.Servizio", Criteria = "(Immobile  is null) OR (not (Apparato is null))", Enabled = false)]
        [RuleRequiredField("RuleReq.ControlliNormativi.Servizio", DefaultContexts.Save, "Impianto è un campo obbligatorio")]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Immobile = '@This.Immobile'")]
        [ExplicitLoading()]
        public Servizio Servizio
        {
            get
            {
                return fServizio;
            }
            set
            {
                SetPropertyValue<Servizio>("Servizio", ref fServizio, value);
            }
        }

        private Asset fAsset;
        [Association(@"ControlliNormativi_Asset"),
        Persistent("ASSET"),
        DisplayName("Asset")]
        [Appearance("ControlliNormativi.Abilita.Asset", Criteria = "Servizio is null", Enabled = false)]
        // [RuleRequiredField("RuleReq.ControlliNormativi.Apparato", DefaultContexts.Save, "Apparato è un campo obbligatorio")]
        [DataSourceCriteria("Servizio = '@This.Servizio'")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public Asset Asset
        {
            get
            {
                return fAsset;
            }
            set
            {
                SetPropertyValue<Asset>("Asset", ref fAsset, value);
            }
        }

        private Frequenze fFrequenza;
        [Association(@"ControlliNormativi_Frequenza"), Persistent("FREQUENZA"), DisplayName("Frequenza")]
        [RuleRequiredField("RuleReq.ControlliNormativi.Frequenza", DefaultContexts.Save, "Frequenza è un campo obbligatorio")]
        [DataSourceCriteria("TipoCadenze = 'Anno' Or TipoCadenze = 'Mese'")]
        [SearchMemberOptions(SearchMemberMode.Include)]
        [ExplicitLoading()]
        public Frequenze Frequenza
        {
            get
            {

                return fFrequenza;
            }
            set
            {
                SetPropertyValue<Frequenze>("Frequenza", ref fFrequenza, value);
            }
        }

        private TipoControlloNormativo fTipoControlloNormativo;
        [Association(@"ControlliNormativi_TipoControlloNormativo"), Persistent("TIPOCONTROLLINORMATIVI"), DisplayName("Tipo Controllo")]
        [RuleRequiredField("RuleReq.ControlliNormativi.TipoControlloNormativo", DefaultContexts.Save, "Tipo Controllo è un campo obbligatorio")]
        [ExplicitLoading()]
        public TipoControlloNormativo TipoControlloNormativo
        {
            get
            {
                return fTipoControlloNormativo;
            }
            set
            {
                SetPropertyValue<TipoControlloNormativo>("TipoControlloNormativo", ref fTipoControlloNormativo, value);
            }
        }




        private DateTime fDataInizioControllo;
        [Persistent("DATAINIZIO"), DisplayName("Data Scadenza")]
        [RuleRequiredField("RuleReq.ControlliNormativi.DataInizioControllo", DefaultContexts.Save, "La Data di Scadenza è un campo obbligatorio")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Scadenza della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime DataInizioControllo
        {
            get
            {
                return fDataInizioControllo;
            }
            set
            {
                SetPropertyValue<DateTime>("DataInizioControllo", ref fDataInizioControllo, value);
            }
        }

        private DateTime fDataPianificataControllo;
        [Persistent("DATAPIANIFICATA"), DisplayName("Data Pianificata"), MemberDesignTimeVisibility(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Pianificata della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime DataPianificataControllo
        {
            get
            {
                return fDataPianificataControllo;
            }
            set
            {
                SetPropertyValue<DateTime>("DataPianificataControllo", ref fDataPianificataControllo, value);
            }
        }

        private DateTime fDataCompletamento;
        [Persistent("DATACOMPLETAMENTO"),
        DisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Completamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Appearance("ControlliNormativi.Abilita.DataCompletamento", Criteria = "[DataInizioControllo] Is Null And [Oid] < 0", Enabled = false)]
        public DateTime DataCompletamento
        {
            get
            {
                return fDataCompletamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataCompletamento", ref fDataCompletamento, value);
            }
        }

        private string fNoteCompletamento;
        [Size(4000),
        Persistent("NOTECOMPLETAMENTO")]
        [DbType("varchar(4000)")]
        [Appearance("ControlliNormativi.Abilita.NoteCompletamento", Criteria = "[DataInizioControllo] Is Null And [Oid] < 0", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string NoteCompletamento
        {
            get
            {
                return fNoteCompletamento;
            }
            set
            {
                SetPropertyValue<string>("NoteCompletamento", ref fNoteCompletamento, value);
            }
        }

        [Persistent("FILEUPLOAD"),      DisplayName("File in upload")]
        [Appearance("ControlliNormativi.Abilita.FileUpload", Criteria = "[DataInizioControllo] Is Null And [Oid] < 0", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData FileUpload
        {
            get
            {
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("FileUpload");
                //return fFileUpload;
            }
            set
            {
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("FileUpload", value);
                //SetPropertyValue<FileData>("FileUpload", ref fFileUpload, value);
            }
        }

        private StatoControlloNormativo fStatoControlloNormativo;  //TipoContattoCliente
        [Persistent("STATO"), DisplayName("Stato Lavorazione")]
        [ImmediatePostData]
        public StatoControlloNormativo StatoControlloNormativo
        {
            get
            {
                return fStatoControlloNormativo;
            }
            set
            {
                SetPropertyValue<StatoControlloNormativo>("StatoControlloNormativo", ref fStatoControlloNormativo, value);
            }
        }

        private RegistroRdL fRegistroRdL;  //TipoContattoCliente
        [Persistent("REGRDL"), DisplayName("Registro RdL")]
        [ImmediatePostData]
        public RegistroRdL RegistroRdL
        {
            get
            {
                return fRegistroRdL;
            }
            set
            {
                SetPropertyValue<RegistroRdL>("RegistroRdL", ref fRegistroRdL, value);
            }
        }


        private ControlliNormativi fPrecedente;
        [Persistent("OIDPRECEDENTE"), DisplayName("Controllo Normativo Precedente")]
        [Appearance("ControlliNormativi.OIDPRECEDENTE", Criteria = "Precedente Is Null", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public ControlliNormativi Precedente
        {
            get
            {
                return fPrecedente;
            }
            set
            {
                SetPropertyValue<ControlliNormativi>("Precedente", ref fPrecedente, value);
            }
        }
        private ControlliNormativi fSuccessivo;
        [Persistent("OIDSUCCESSIVO"), DisplayName("Controllo Normativo Successivo")]
        [Appearance("ControlliNormativi.fSuccessivo", Criteria = "Successivo Is Null", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public ControlliNormativi Successivo
        {
            get
            {
                return fSuccessivo;
            }
            set
            {
                SetPropertyValue<ControlliNormativi>("Successivo", ref fSuccessivo, value);
            }
        }


        private string _Scaduto = string.Empty;
        [NonPersistent, DisplayName("Stato Scadenza")]
        [Appearance("ControlliNormativi.DataInizioControlloBackColor.Green", BackColor = "Green", FontColor = "Black", Criteria = "DataInizioControllo > '@CurrentDate' And [DataCompletamento] Is Null")]
        [Appearance("ControlliNormativi.DataInizioControlloBackColor.Red", BackColor = "Red", FontColor = "Black", Criteria = "DataInizioControllo <= '@CurrentDate' And [DataCompletamento] Is Null")]
        public string Scaduto
        {
            get
            {
                //var Tmp = Evaluate("DataInizioControllo");                
                var Tmp = Evaluate("DataCompletamento");
                if (Tmp != null)
                {
                    // è eseguito
                    DateTime vDataCompletamento = (DateTime)Tmp;
                    bool inRitardo = vDataCompletamento < this.DataInizioControllo;
                    if (inRitardo)
                    {
                        return String.Format("Eseguito in Ritardo");
                    }
                    else
                    {
                        return String.Format("Eseguito");
                    }
                }
                else
                {
                    var dts = Evaluate("DataInizioControllo");
                    if (dts != null)
                    {
                        DateTime vDataPianificataControllo = (DateTime)dts;
                        if (vDataPianificataControllo < DateTime.Now)
                            return String.Format("Scaduto");
                        if (vDataPianificataControllo >= DateTime.Now)
                        {
                            int gg = (vDataPianificataControllo - DateTime.Now).Days;
                            if (gg < 30)
                                return String.Format("In Prossima Scadenza");
                            else
                                return String.Format("In Attesa");
                        }

                    }
                }

                return null;
            }

        }

        [Association(@"COntrolliNormativi_Log", typeof(ControlliNormativiRifLog)), DevExpress.Xpo.DisplayName("Registro Avvisi Inviati")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<ControlliNormativiRifLog> ControlliNormativiRifLogS
        {
            get
            {
                return GetCollection<ControlliNormativiRifLog>("ControlliNormativiRifLogS");
            }
        }
        protected override void OnSaved()
        {
            base.OnSaved();
            //if (SetVarSessione.CountSave == 1)
            //{
            //    SetVarSessione.CountSave = 0;
            //}
            //else
            //{
            //    //if (DataCompletamento != null)
            //    //{
            //    //    SetVarSessione.CountSave = 1;
            //    //    var db = new Classi.DB();
            //    //    db.CreaNuovoControlloNormativo(Oid);
            //    //    Reload();
            //    //}
            //}
        }
    }
}
