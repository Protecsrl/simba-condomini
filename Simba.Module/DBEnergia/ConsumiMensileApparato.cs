using CAMS.Module.Classi;
using CAMS.Module.DBMisure;
using CAMS.Module.DBPlant;
using CAMS.Module.DBSpazi;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBEnergia
{
    [DefaultClassOptions, Persistent("ENMENSILEXAPPARATO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Consumi Mensili x Apparato")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem("Energia")]
    public class ConsumiMensileApparato : XPObject
    {
        public ConsumiMensileApparato() : base() { }

        public ConsumiMensileApparato(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

         [Association(@"ConsumiMensileImpianto_Apparato"), Persistent("ENMENSILEXIMPIANTO")]
        [Delayed(true)]
        public ConsumiMensileImpianto ConsumiMensileImpianto
        {
            get { return GetDelayedPropertyValue<ConsumiMensileImpianto>("ConsumiMensileImpianto"); }
            set { SetDelayedPropertyValue<ConsumiMensileImpianto>("ConsumiMensileImpianto", value); }
        }

        private string fDescrizione;
        [Size(100), Persistent("DESCRIZIONE"), DisplayName("Descrizione")]
        [DbType("varchar(100)")]
        [Delayed(true)]
        public string Descrizione
        {
            get { return GetDelayedPropertyValue<string>("Descrizione"); }
            set { SetDelayedPropertyValue<string>("Descrizione", value); }
        }

        [Persistent("TIPOMISURA"), DisplayName("Tipo Misura")]
        [Delayed(true)]
        public TipoMisura TipoMisura
        {
            get { return GetDelayedPropertyValue<TipoMisura>("TipoMisura"); }
            set { SetDelayedPropertyValue<TipoMisura>("TipoMisura", value); }
        }

        [Persistent("IMMOBILE"), DisplayName("Immobile")]
        [Delayed(true)]
        public Immobile Immobile
        {
            get { return GetDelayedPropertyValue<Immobile>("Immobile"); }
            set { SetDelayedPropertyValue<Immobile>("Immobile", value); }
        }

        private Servizio fServizio;
        [Persistent("SERVIZIO"), DisplayName("Servizio")]
        [Delayed(true)]
        public Servizio Servizio

        {
            get { return GetDelayedPropertyValue<Servizio>("Servizio"); }
            set { SetDelayedPropertyValue<Servizio>("Servizio", value); }
        }



        [Persistent("ASSET"), DisplayName("Asset")]
        [Delayed(true)]
        public Asset Asset
        {
            get { return GetDelayedPropertyValue<Asset>("Asset"); }
            set { SetDelayedPropertyValue<Asset>("Asset", value); }
        }

        [Persistent("PIANO"), System.ComponentModel.DisplayName("Piano")]
        [Delayed(true)]
        public Piani Piano
        {
            get { return GetDelayedPropertyValue<Piani>("Piano"); }
            set { SetDelayedPropertyValue<Piani>("Piano", value); }
        }

        [Persistent("LOCALI"), DevExpress.Xpo.DisplayName("Locali")]
        [Delayed(true)]
        public Locali Locale
        {
            get { return GetDelayedPropertyValue<Locali>("Locale"); }
            set { SetDelayedPropertyValue<Locali>("Locale", value); }
        }

        [Persistent("DATARIFERIMENTO"), DisplayName("Data Riferimento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask)]
        [Delayed(true)]
        public DateTime DataRiferimento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataRiferimento"); }
            set { SetDelayedPropertyValue<DateTime>("DataRiferimento", value); }
        }

        [Persistent("UNITAMISURA"), DisplayName("Unità di misura")]
        [Delayed(true)]
        public UnitaMisura UnitaMisura
        {
            get { return GetDelayedPropertyValue<UnitaMisura>("UnitaMisura"); }
            set { SetDelayedPropertyValue<UnitaMisura>("UnitaMisura", value); }
        }

        [Persistent("TOTALE"), DisplayName("Valore")]
        [Delayed(true)]
        public Double Totale
        {
            get { return GetDelayedPropertyValue<Double>("Totale"); }
            set { SetDelayedPropertyValue<Double>("Totale", value); }
        }

        [Association(@"ConsumiMensileApparato_Giornaliero", typeof(ConsumiGiornalieriApparato)), Aggregated, DisplayName("Dettaglio x Apparato")]
        public XPCollection<ConsumiGiornalieriApparato> ConsumiGiornalieriApparatos
        {
            get
            {
                return GetCollection<ConsumiGiornalieriApparato>("ConsumiGiornalieriApparatos");
            }
        }

        #region  settimana mese anno

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"), DisplayName("Data Aggiornamento"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        [Appearance("ConsumiMensileApparato.DataAggiornamento", Enabled = false)]
        [VisibleInListViewAttribute(false), VisibleInDetailView(true)]
        [Delayed(true)]
        public DateTime DataAggiornamento  //DataAggiornamento    UltimaDataAggiornamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAggiornamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataAggiornamento", value); }
        }        
        #endregion
    }
}
