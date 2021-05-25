using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.ConditionalAppearance;
// 
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System.ComponentModel;
using CAMS.Module.PropertyEditors;
using DevExpress.Persistent.BaseImpl;
using CAMS.Module.DBPlant;
using DevExpress.Persistent.Validation;
using System.Drawing;
using CAMS.Module.Classi;
using CAMS.Module.DBAngrafica;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;


namespace CAMS.Module.DBTask.POI
{
    [DefaultClassOptions, Persistent("V_REGISTRO_RDL_POI"), System.ComponentModel.DisplayName("Registro Programma Operativo Interventi")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro POI")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem("Ticket")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
    //[ DefaultListViewOptions(true, )]
    //[Appearance("RegistroPOI.BlueDisable", TargetItems = "*", Criteria = @"Oid = 7", FontColor = "Blue", Enabled = false)]

    [Appearance("RegistroPOI.DataPianificata.Rosso", AppearanceItemType.ViewItem,
          @"[DataPianificataSchedulazione] <= [DataNow] And (StatoPOI = 'Pianificato' Or StatoPOI = 'nonDefinito')", TargetItems = "DataPianificataSchedulazione", FontColor = "Red")]

    //[Appearance("RegistroPOI.ContaPOI.Rosso", AppearanceItemType.ViewItem, @"ContaPOI == 0", TargetItems = "*", FontColor = "Red")]

    [Appearance("RegistroPOI.desabilita", AppearanceItemType.ViewItem, @"StatoPOI = 'Eseguito' Or StatoPOI = 'Trasmesso'",
        TargetItems = "_ReportExcel;Immobile;Impianto;Trimestre;Anno", FontColor = "Black", Enabled = false)]



    #region filtro tampone
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("ListPOI_Anno", "", "Tutto", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("ListPOI_Anno_eseguito", "StatoPOI = 'Eseguito' Or StatoPOI = 'Trasmesso'", "eseguito o trasmesso", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("ListPOI_Anno_da_eseguire", "StatoPOI = 'nonDefinito' Or StatoPOI = 'Pianificato'", "da eseguire", Index = 2)]



    #endregion

    public class RegistroPOI : XPObject
    {
        public RegistroPOI() : base() { }
        public RegistroPOI(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //_old_SSmistamento_Oid = 0;
            if (this.Oid == -1) // crea RdL
            {
                this.Anno = DateTime.Now.Year;
                this.StatoPOI = StatoPOI.nonDefinito;
                this.DataPianificataSchedulazione = DateTime.Now.AddHours(1);
            }


        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
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

        private Immobile fImmobile;
        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        [Appearance("RegistroPOI.Abilita.Immobile", Criteria = "not (Impianto  is null)", FontColor = "Black", Enabled = false)]
        [RuleRequiredField("RegistroPOI.RdL.Immobile", DefaultContexts.Save, "Immobile è un campo obbligatorio")]
        [Appearance("RegistroPOI.Immobile.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Immobile)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Abilitato = 'Si'")]  // And Commesse.DataAl <= LocalDateTimeToday()
        //[ExplicitLoading()]
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


        private Servizio fServizio;
        [Persistent("SERVIZIO"), System.ComponentModel.DisplayName("Servizio")]
        [Appearance("RegistroPOI.Abilita.Servizio", Criteria = "Immobile  is null", FontColor = "Black", Enabled = false)]
        [RuleRequiredField("RuleReq.RegistroPOI.Servizio", DefaultContexts.Save, "Servizio è un campo obbligatorio")]
        [Appearance("RegistroPOI.Impianto.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Servizio)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Immobile = '@This.Immobile'")]
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


        #region  settimana mese anno

        private enTrimestre fTrimestre;
        [Persistent("TRIMESTRE"), System.ComponentModel.DisplayName("Trimestre")]
        //[Appearance("RegistroPOI.Abilita.ReportExcel", Criteria = "StatoPOI = 'Eseguito' Or StatoPOI = 'Trasmesso'", FontColor = "Black", Enabled = false)]
        public enTrimestre Trimestre
        {
            get
            {
                return fTrimestre;
            }
            set
            {
                SetPropertyValue<enTrimestre>("Trimestre", ref fTrimestre, value);
            }
        }


        private int fAnno;
        [Persistent("ANNO"), System.ComponentModel.DisplayName("Anno")]
        [RuleRange("RegistroPOI.Anno", DefaultContexts.Save, 2014, 2100, CustomMessageTemplate = "Il Valore deve Essere Compreso tra 2014 e 2100.")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int Anno
        {
            get
            {
                return fAnno;
            }
            set
            {
                SetPropertyValue<int>("Anno", ref fAnno, value);
            }
        }

        private DateTime fDataPianificataSchedulazione;
        [Persistent("DATAPIANIFICATASK"), System.ComponentModel.DisplayName("Data Pianificata")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_EditMask)]
        //[EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("La data pianifica stabilisce il limite oltre il quale deve essere elaborato il POI.", "Data di Pianificazione è a cura del supervisore", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[Appearance("RegistroPOI.Abilita.ReportExcel", Criteria = "StatoPOI = 'Eseguito' Or StatoPOI = 'Trasmesso'", FontColor = "Black", Enabled = false)]
        [ImmediatePostData]
        public DateTime DataPianificataSchedulazione
        {
            get
            {
                return fDataPianificataSchedulazione;
            }
            set
            {
                SetPropertyValue<DateTime>("DataPianificataSchedulazione", ref fDataPianificataSchedulazione, value);
            }
        }

        // [XafDisplayName("non Definito")]
        //[ImageName("State_Priority_Low")]
        //nonDefinito,
        //[XafDisplayName("Pianificato")]
        //[ImageName("State_Priority_Low")]
        //Pianificato,
        //[XafDisplayName("Eseguito")]
        //[ImageName("State_Priority_High")]
        //Eseguito,
        //[XafDisplayName("Scaduto")]
        //[ImageName("State_Priority_High")]
        //Scaduto,
        //      [XafDisplayName("Scaduto")]
        //[ImageName("State_Priority_High")]
        //Trasmesso

        private StatoPOI fStatoPOI;
        [Persistent("STATO"), System.ComponentModel.DisplayName("Stato")]
        //[Appearance("RegistroPOI.StatoPOI.ContaPOI", AppearanceItemType.ViewItem, "ContaPOI = 0", FontStyle = FontStyle.Bold, Enabled=false)]
        public StatoPOI StatoPOI
        {
            get
            {
                return fStatoPOI;
            }
            set
            {
                SetPropertyValue<StatoPOI>("StatoPOI", ref fStatoPOI, value);
            }
        }

        private ReportExcel fReportExcel;
        [Persistent("REPORTEXCEL"), System.ComponentModel.DisplayName("Report Excel")]
        //[Appearance("RegistroPOI.Abilita.ReportExcel", Criteria = "StatoPOI = 'Eseguito' Or StatoPOI = 'Trasmesso'", FontColor = "Black", Enabled = false)]
        //[Appearance("RegistroPOI.Richiedente.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Richiedente)", FontStyle = FontStyle.Bold, FontColor = "Brown")]      
        [DataSourceProperty("SoloPOI")]
        [VisibleInListView(false)]
        [Delayed(true)]
        public ReportExcel _ReportExcel
        {
            get
            {
                return GetDelayedPropertyValue<ReportExcel>("_ReportExcel");
            }
            set
            {
                SetDelayedPropertyValue<ReportExcel>("_ReportExcel", value);
            }

            //get //ObjectType
            //{
            //    //this.StatoPOI
            //    //this.ReportExcel.ObjectType.FullName.ToString().Contains
            //    return fReportExcel;
            //}
            //set
            //{
            //    SetPropertyValue<ReportExcel>("_ReportExcel", ref fReportExcel, value);
            //}
        }

        private XPCollection<ReportExcel> _SoloPOI;
        [Browsable(false)]
        public XPCollection<ReportExcel> SoloPOI
        {
            get
            {
                if (_SoloPOI == null)
                {
                    //  .Where(w => w.ObjectType == View.ObjectTypeInfo.Type)
                    CriteriaOperator cr = CriteriaOperator.Parse("[ObjectType] = ?", typeof(ListPOI));
                    //string filtro = string.Format("{0}", ObjectType.FullName);
                    //var filter = new DevExpress.Data.Filtering.BinaryOperator( filtro,typeof(ListPOI).FullName.ToString());
                    _SoloPOI = new XPCollection<ReportExcel>(Session, cr);
                }
                return _SoloPOI;
            }
        }

        //private FileData fFilePOI;
        //[Persistent("FILEPOI")]
        //[DevExpress.ExpressApp.DC.XafDisplayName("File POI")]
        //[VisibleInListView(false)]
        //public FileData FilePOI
        //{
        //    get
        //    {
        //        return fFilePOI;
        //    }
        //    set
        //    {
        //        SetPropertyValue<FileData>("FilePOI", ref fFilePOI, value);
        //    }
        //}
        [Persistent("FILEPOI")]
        [DevExpress.ExpressApp.DC.XafDisplayName("File POI")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.xls", "*.xlsx")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [VisibleInListView(false)]
        [Delayed(true)]
        public FileData FilePOI
        {
            get
            {
                return GetDelayedPropertyValue<FileData>("FilePOI");
            }
            set
            {
                SetDelayedPropertyValue<FileData>("FilePOI", value);
            }
        }


        private DateTime fDataConferma;
        [Persistent("DATACONFERMA"), System.ComponentModel.DisplayName("Data Conferma")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_EditMask)]
        [RuleRequiredField("RuleReq.RegistroPOI.DataConferma", DefaultContexts.Save, "Il campo Data Conferma è un campo obbligatorio")]
        //[EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("La data fissa il limite prima del quale le programmata è confermata.", "Data di Conferma a cura del supervisore.", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[Appearance("RegistroPOI.Abilita.ReportExcel", Criteria = "StatoPOI = 'Eseguito' Or StatoPOI = 'Trasmesso'", FontColor = "Black", Enabled = false)]
        [ImmediatePostData]
        public DateTime DataConferma
        {
            get
            {
                return fDataConferma;
            }
            set
            {
                SetPropertyValue<DateTime>("DataConferma", ref fDataConferma, value);
            }
        }


        #endregion

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Data Aggiornamento")]
        [VisibleInListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [Appearance("RegistroPOI.Abilita.DataAggiornamento", FontColor = "Black", Enabled = false)]
        public DateTime DataAggiornamento
        {
            get
            {
                return fDataAggiornamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value);
            }
        }

        private DateTime fDataElaborazionePOI;
        [Persistent("DATAELABORAZIONEPOI")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Data Elaborazione POI")]
        [VisibleInListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [Appearance("RegistroPOI.Abilita.DataElaborazionePOI", FontColor = "Black", Enabled = false)]
        public DateTime DataElaborazionePOI
        {
            get
            {
                return fDataElaborazionePOI;
            }
            set
            {
                SetPropertyValue<DateTime>("DataElaborazionePOI", ref fDataElaborazionePOI, value);
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

        //  [DevExpress.ExpressApp.DC.XafDisplayName("Righe POI")]
        ////  [PersistentAlias("Iif(Impianto is null,0,[<RdL>][^.Impianto.Oid = Impianto.Oid And Categoria.Oid In(5,3,1) And GetYear(DataPianificata) = ^.Anno ].Count())")]
        //  //[PersistentAlias("1")]
        //  [NonPersistent]
        //  [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        //  //[Appearance("RegistroPOI.ContaPOI.EvidenzaObligatorio", AppearanceItemType.ViewItem, "ContaPOI = 0", FontStyle = FontStyle.Bold, FontColor = "Red")]
        //  [VisibleInListView(false)]
        //  public int ContaPOI
        //  {
        //      get
        //      {
        //          //var tempObject = EvaluateAlias("ContaPOI");
        //          //if (tempObject != null)
        //          //    return (int)tempObject;
        //          //else
        //              return 1;
        //      }
        //  }

        private int fContaPOI;
        [NonPersistent]
        [DevExpress.ExpressApp.DC.XafDisplayName("Righe POI")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        [VisibleInListView(false)]
        [Appearance("RegistroPOI.ContaPOI.EvidenzaObligatorio", AppearanceItemType.ViewItem,"1=1", Enabled = false, FontStyle = FontStyle.Bold)]

        public int ContaPOI
        {
            get
            {
                return fContaPOI;
            }
            set
            {
                SetPropertyValue<int>("ContaPOI", ref fContaPOI, value);
            }
        }


    }

}


