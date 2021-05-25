using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.PropertyEditors;
using DevExpress.Persistent.Validation;

namespace CAMS.Module.DBMisure
{
    [DefaultClassOptions, Persistent("REGMISURE")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Misure")]
    [ImageName("Action_Inline_Edit")]
    [Appearance("RegMisure.Disabilita.DettaglioMisureMagZero", TargetItems = "Immobile;Impianto;Master;DataInserimento", Criteria = "RegMisureDettaglios.Count() > 0", Enabled = false)]
    [NavigationItem("Misure")]
    public class RegMisure : XPObject
    {
        public RegMisure() : base() { }

        public RegMisure(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        private string fDescrizione;
        [Size(150), Persistent("DESCRIZIONE"), DisplayName("Descrizione")]
        //[Appearance("RegMisure.Abilita.Utente", Enabled = false)]
        [DbType("varchar(100)")]
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
        [Persistent("IMMOBILE"), DisplayName("Immobile")]
        [Appearance("RegMisure.Abilita.Immobile", Criteria = "not (Impianto is null)", Enabled = false)]
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

        private Servizio fImpianto;
        [Association(@"RegMisure_Servizio"), Persistent("IMPIANTO"), DisplayName("Impianto")]
        [Appearance("RegMisure.Abilita.Servizio", Criteria = "Immobile is null Or not (Master is null)", Enabled = false)]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Immobile = '@This.Immobile'")]
        [ExplicitLoading()]
        public Servizio Servizio
        {
            get
            {
                return fImpianto;
            }
            set
            {
                SetPropertyValue<Servizio>("Servizio", ref fImpianto, value);
            }
        }

        private Master fMaster;
        //[Association(@"RegMisure_Master"), 
        [Persistent("MASTER"), DisplayName("Master Misure")]
        [Appearance("RegMisure.Abilita.Master", Criteria = "Immobile is null Or Impianto is null", Enabled = false)]
        [DataSourceCriteria("Immobile = '@This.Immobile'")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public Master Master
        {
            get
            {
                return fMaster;
            }
            set
            {
                SetPropertyValue<Master>("Master", ref fMaster, value);
            }
        }


        #region  settimana mese anno

        private int fAnno;
        [Persistent("ANNO"), System.ComponentModel.DisplayName("Anno")]
        [RuleRange("RegMisure.Anno", DefaultContexts.Save, 2014, 2100, CustomMessageTemplate = "Il Valore deve Essere Compreso tra 2014 e 2100.")]
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

        
        #endregion


        private DateTime fDataInserimento;
        [Persistent("DATAINSERIMENTO"), DisplayName("Data Rilevazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask)]
        //[Appearance("RegMisure.Abilita.DataInserimento", Criteria = "RegMisureDettaglios.Count() = 0", Enabled = false)]
        public DateTime DataInserimento
        {
            get
            {
                return fDataInserimento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataInserimento", ref fDataInserimento, value);
            }
        }

        [Association(@"RegMisureDettaglio_RegMisure", typeof(RegMisureDettaglio)),Aggregated, DisplayName("Dettaglio Misure")]
        [Appearance("RegMisure.Abilita.RegMisureDettaglios", Criteria = "Oid < 0", Enabled = false)]
        public XPCollection<RegMisureDettaglio> RegMisureDettaglios
        {
            get
            {
                return GetCollection<RegMisureDettaglio>("RegMisureDettaglios");
            }
        }



        private string fUtente;
        [Size(100), Persistent("UTENTE"), DisplayName("Utente")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Appearance("RegMisure.Abilita.Utente", Enabled = false)]
        [DbType("varchar(100)")]
        public string Utente
        {
            get
            {
                return fUtente;
            }
            set
            {
                SetPropertyValue<string>("Utente", ref fUtente, value);
            }
        }

    }
}
