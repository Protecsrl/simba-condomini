using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
//namespace CAMS.Module.DBTransazioni
//{
//    class RegistroReportBatch : XPObject
//    {
//    }
//}
namespace CAMS.Module.DBTransazioni
{
    [DefaultClassOptions, Persistent("REGISTROREPORTBATCH")]
    [System.ComponentModel.DisplayName("Registro Report Differiti")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Report")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [ImageName("GroupFieldCollection")]
    //[NavigationItem("Data Import")]
    public class RegistroReportBatch : XPObject
    {
        public RegistroReportBatch() : base() { }
        public RegistroReportBatch(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        //[Persistent("TRANSAZIONIAPP"), DisplayName("App Transazioni")]
        //[Delayed(true)]
        //public AppTransazioni AppTransazioni
        //{
        //    get { return GetDelayedPropertyValue<AppTransazioni>("AppTransazioni"); }
        //    set { SetDelayedPropertyValue<AppTransazioni>("AppTransazioni", value); }
        //}

        private string fDescrizione;
        [Size(200), Persistent("DESCRIZIONE")]
        [DbType("varchar(200)")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        private Contratti fCommesse;
        [Persistent("CONTRATTO"), DevExpress.ExpressApp.DC.XafDisplayName("Contratto")]
        [RuleRequiredField("RegistroReportBatch.Commesse", DefaultContexts.Save, "La commessa è un campo obbligatorio")]
        //[Appearance("Immobile.Commesse", Criteria = "CodDescrizione != '" + NA + "'", Context = "DetailView")]
   
        [Delayed(true)]
        public Contratti Commesse
        {
            get { return GetDelayedPropertyValue<Contratti>("Commesse"); }
            set { SetDelayedPropertyValue<Contratti>("Commesse", value); }
        }


        private StatoSmistamento fStatoSmistamento;
        [Persistent("STATOSMISTAMENTO"), DisplayName("Stato StatoSmistamento")]
        public StatoSmistamento StatoSmistamento
        {
            get { return fStatoSmistamento; }
            set { SetPropertyValue<StatoSmistamento>("StatoSmistamento", ref fStatoSmistamento, value); }
        }


        [Persistent("DATAPIANIFICATA")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd.MM.yyyy H:mm:ss tt")]
        [DevExpress.Xpo.DisplayName("Data Pianificata")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [Delayed(true)]
        public DateTime DataPianificata
        {
            get { return GetDelayedPropertyValue<DateTime>("DataPianificata"); }
            set { SetDelayedPropertyValue<DateTime>("DataPianificata", value); }
        }




        //private StatoElaborazioneJob fStatoElaborazioneJob;
        //[Persistent("STATOELABORAZIONEJOB"), DisplayName("Stato Elaborazione Job")]
        //public StatoElaborazioneJob StatoElaborazioneJob
        //{
        //    get { return fStatoElaborazioneJob; }
        //    set { SetPropertyValue<StatoElaborazioneJob>("StatoElaborazioneJob", ref fStatoElaborazioneJob, value); }
        //}

        //private FlgAbilitato fAbilitato;
        //[Persistent("ABILITATO"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo")]
        ////[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        //[ImmediatePostData(true)]
        //public FlgAbilitato Abilitato
        //{
        //    get { return fAbilitato; }
        //    set { SetPropertyValue<FlgAbilitato>("Abilitato", ref fAbilitato, value); }
        //}

        //[Association(@"RegistroTransazioniDettaglio.RegistroTransazioni", typeof(RegistroTransazioniDettaglio)), Aggregated]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Dettaglio")]
        //public XPCollection<RegistroTransazioniDettaglio> RegistroTransazioniDettaglios
        //{
        //    get
        //    {
        //        return GetCollection<RegistroTransazioniDettaglio>("RegistroTransazioniDettaglios");
        //    }
        //}

        //[Persistent("DATAULTIMAESEC")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd.MM.yyyy H:mm:ss tt")]
        //[DevExpress.Xpo.DisplayName("Data Ultima Esecuzione")]
        //[VisibleInDetailView(true), VisibleInListView(false)]
        //[Delayed(true)]
        //public DateTime DataUltimaEsecuzione
        //{
        //    get { return GetDelayedPropertyValue<DateTime>("DataUltimaEsecuzione"); }
        //    set { SetDelayedPropertyValue<DateTime>("DataUltimaEsecuzione", value); }
        //}

    }
}
