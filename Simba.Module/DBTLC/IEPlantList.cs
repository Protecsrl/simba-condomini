using CAMS.Module.Classi;
using CAMS.Module.DBMisure;
using CAMS.Module.DBPlant;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace CAMS.Module.DBTLC
{
    [DefaultClassOptions, Persistent("TLC_IEPLANTLIST")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Plant List")]
    [ImageName("ShowTestReport")]
    [NavigationItem("Telecontrollo")]
    #region
    //    [RuleCriteria("RuleInfo.RdL.DataInizioLavori11", DefaultContexts.Save, @"[DataRichiesta] <= DataInizioLavori",
    //    CustomMessageTemplate = "Informazione: La Data di InizioLavori RdL ({DataInizioLavori}) deve essere maggiore della data di Richiesta({DataRichiesta}).",
    //    SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Information)]
    #endregion

    public class IEPlantList : XPObject
    {
        public IEPlantList() : base() { }
        public IEPlantList(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        [Association(@"IEPlantList_Master"), Persistent("MASTER")]
        [Delayed(true)]
        public Master Master
        {
            get { return GetDelayedPropertyValue<Master>("Master"); }
            set { SetDelayedPropertyValue<Master>("Master", value); }
        }        

        #region Anagrafica di Censimento
        private Immobile fImmobile;
        [PersistentAlias("Master.Immobile")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Immobile")]
        public Immobile Immobile
        {
            get
            {
                var tempObject = EvaluateAlias("Immobile");
                if (tempObject != null)
                    return (Immobile) tempObject;
                else
                    return null;
            }
        }
        
        private Servizio fServizio;
        [Persistent("SERVIZIO"), DevExpress.ExpressApp.DC.XafDisplayName("Servizio")]
        [ImmediatePostData(true)]
        [Delayed(true)]
        public Servizio Servizio
        {
            get { return GetDelayedPropertyValue<Servizio>("Servizio"); }
            set { SetDelayedPropertyValue<Servizio>("Servizio", value); }
        }

        private Asset fApparato;  ///[UltimoStatoSmistamento.Oid] > 1
        [Persistent("ASSET"), DevExpress.ExpressApp.DC.XafDisplayName("Asset")]       
        [ImmediatePostData(true)]
        [Delayed(true)]
        public Asset Asset
        {
            get { return GetDelayedPropertyValue<Asset>("Asset"); }
            set { SetDelayedPropertyValue<Asset>("Asset", value); }
        }
        [NonPersistent, Size(25)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string RicercaApparato
        {
            get;
            set;
        }

        #endregion
        private string fDescrizione;
        [Size(1024), Persistent("DESCRIZIONE"), DevExpress.ExpressApp.DC.XafDisplayName("Descrizione")]
        ///  [RuleRequiredField("SegnalazioneCC.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //  [Appearance("SegnalazioneCC.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [DbType("varchar(1024)")]
        [Delayed(true)]
        public string Descrizione
        {
            get { return GetDelayedPropertyValue<string>("Descrizione"); }
            set { SetDelayedPropertyValue<string>("Descrizione", value); }
        }

        [Size(20), Persistent("DEVICE"), DevExpress.ExpressApp.DC.XafDisplayName("Device")]
        ///  [RuleRequiredField("SegnalazioneCC.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //  [Appearance("SegnalazioneCC.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [DbType("varchar(20)")]
        [Delayed(true)]
        public string Device
        {
            get { return GetDelayedPropertyValue<string>("Device"); }
            set { SetDelayedPropertyValue<string>("Device", value); }
        }

        [Size(250), Persistent("PLANTNAME"), DevExpress.ExpressApp.DC.XafDisplayName("Plant Name")]
        ///  [RuleRequiredField("SegnalazioneCC.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //  [Appearance("SegnalazioneCC.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [DbType("varchar(250)")]
        [Delayed(true)]
        public string PlantName
        {
            get { return GetDelayedPropertyValue<string>("PlantName"); }
            set { SetDelayedPropertyValue<string>("PlantName", value); }
        }

        [Size(20), Persistent("POINT"), DevExpress.ExpressApp.DC.XafDisplayName("Point")]
        ///  [RuleRequiredField("SegnalazioneCC.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //  [Appearance("SegnalazioneCC.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [DbType("varchar(20)")]
        [Delayed(true)]
        public string Point
        {
            get { return GetDelayedPropertyValue<string>("Point"); }
            set { SetDelayedPropertyValue<string>("Point", value); }
        }

        [Size(250), Persistent("POINTNAME"),DevExpress.ExpressApp.DC.XafDisplayName("Point Name")]
        ///  [RuleRequiredField("SegnalazioneCC.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //  [Appearance("SegnalazioneCC.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [DbType("varchar(250)")]
        [Delayed(true)]
        public string PointName
        {
            get { return GetDelayedPropertyValue<string>("PointName"); }
            set { SetDelayedPropertyValue<string>("PointName", value); }
        }

         [Persistent("TIPOCAMPO"), DevExpress.ExpressApp.DC.XafDisplayName("Tipo Campo"), ToolTip("il Tipo Campo è legato alla testata o alla lista")]
        [DataSourceProperty("StaticTipoCampo")]
        [Delayed(true)]
        public TipoDatoSystem tipoCampo
        {
            get { return GetDelayedPropertyValue<TipoDatoSystem>("tipoCampo"); }
            set { SetDelayedPropertyValue<TipoDatoSystem>("tipoCampo", value); }

            //get { return fTipoCampo; }
            //set { SetPropertyValue<TipoDatoSystem>("TipoCampo", ref fTipoCampo, value); }
        }

        [Size(250), Persistent("KEY"), DevExpress.ExpressApp.DC.XafDisplayName("Key TLC")]
        ///  [RuleRequiredField("SegnalazioneCC.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //  [Appearance("SegnalazioneCC.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [DbType("varchar(250)")]
        [VisibleInListView(false)]
        [Delayed(true)]
        public string Key
        {
            get { return GetDelayedPropertyValue<string>("Key"); }
            set { SetDelayedPropertyValue<string>("Key", value); }
        }

        private UnitaMisura fUnitaMisura;
        //[Association(@"Master_UnitaMisura")]
        [Persistent("UNITAMISURA"), DevExpress.ExpressApp.DC.XafDisplayName("Unità di misura")]
        public UnitaMisura UnitaMisura
        {
            get { return fUnitaMisura; }
            set { SetPropertyValue<UnitaMisura>("UnitaMisura", ref fUnitaMisura, value); }
        }

        //private TipoValoreCaratteristicaTecnica fTipoValoreMisura;
        //[Persistent("TIPOCARATTERISTICATECNICA"), DevExpress.ExpressApp.DC.XafDisplayName("Tipo Valore Misura")]
        //[DataSourceProperty("StaticTipoValoreCaratteristicaTecnica")]
        ////[VisibleInListView(false), VisibleInDetailView(true)]
        //[ImmediatePostData(true)]
        ////[ExplicitLoading()]
        //public TipoValoreCaratteristicaTecnica TipoValoreMisura
        //{
        //    get { return fTipoValoreMisura; }
        //    set { SetPropertyValue<TipoValoreCaratteristicaTecnica>("TipoValoreMisura", ref fTipoValoreMisura, value); }
        //}


        [Persistent("DATAUPDATE"), DevExpress.ExpressApp.DC.XafDisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //  [ToolTip("Data di Inserimento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]   In(1,2,11))
        //  [Appearance("SegnalazioneCC.DataRichiesta.Enabled", @"Oid > 0 And UtenteCreatoRichiesta != CurrentUserId() And !([UltimoStatoSmistamento.Oid] In(1))", FontColor = "Black", Enabled = false)]
        //[EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        [VisibleInListView(false)]
        [Delayed(true)]
        public DateTime DataAggiornamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAggiornamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataAggiornamento", value); }
        }

        [Browsable(false)]
        public static IList<TipoDatoSystem?> StaticTipoCampo
        {
            get
            {
                return new TipoDatoSystem?[] {
                 TipoDatoSystem.String,
                TipoDatoSystem.Integer,
                 TipoDatoSystem.Double};
            }
        }

        [Association(@"Plant.IEValueList", typeof(IEValueList)), Aggregated, DevExpress.ExpressApp.DC.XafDisplayName("Dati")]
        public XPCollection<IEValueList> IEValueLists
        {
            get
            {
                return GetCollection<IEValueList>("IEValueLists");
            }
        }



        protected override void OnSaved()
        {
            base.OnSaved();
        }

        public override string ToString()
        {
            return this.Descrizione;
        }
    }
}
