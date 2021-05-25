using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
using System.Collections.Generic;
#pragma warning restore CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
using System.Linq;
#pragma warning restore CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Text' è già presente in questo spazio dei nomi
using System.Text;
#pragma warning restore CS0105 // La direttiva using per 'System.Text' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Threading.Tasks' è già presente in questo spazio dei nomi
using System.Threading.Tasks;
#pragma warning restore CS0105 // La direttiva using per 'System.Threading.Tasks' è già presente in questo spazio dei nomi

using CAMS.Module.Classi;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Drawing;
using DevExpress.Persistent.BaseImpl;

namespace CAMS.Module.DBTransazioni
{
    [DefaultClassOptions, Persistent("TRANSAZIONIREGISTRODETT")]
    [System.ComponentModel.DisplayName("Registro Transazioni Dettaglio")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Transazioni Dettaglio")]
    [ImageName("GroupFieldCollection")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    //[NavigationItem("Data Import")]
    public class RegistroTransazioniDettaglio : XPObject
    {
        public RegistroTransazioniDettaglio() : base() { }

        public RegistroTransazioniDettaglio(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        //RegistroTransazioniDettaglio.RegistroTransazioni
        [Association(@"RegistroTransazioniDettaglio.RegistroTransazioni")]
        [Persistent("TRANSAZIONIREGISTRO"), DisplayName("Registro Transazioni")]
        [Delayed(true)]
        public RegistroTransazioni RegistroTransazioni
        {
            get { return GetDelayedPropertyValue<RegistroTransazioni>("RegistroTransazioni"); }
            set { SetDelayedPropertyValue<RegistroTransazioni>("RegistroTransazioni", value); }
        }

        private string fDescrizione;
        [Size(200), Persistent("DESCRIZIONE")]
        [DbType("varchar(200)")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }


        [Persistent("DATAINIZIO")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd.MM.yyyy H:mm:ss tt")]
        [DevExpress.Xpo.DisplayName("Data Inizio")]
        //[VisibleInDetailView(true), VisibleInListView(false)]
        [Delayed(true)]
        public DateTime DataInizio
        {
            get { return GetDelayedPropertyValue<DateTime>("DataInizio"); }
            set { SetDelayedPropertyValue<DateTime>("DataInizio", value); }
        }


        [Persistent("DATAFINE")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd.MM.yyyy H:mm:ss tt")]
        [DevExpress.Xpo.DisplayName("Data Fine")]
        //[VisibleInDetailView(true), VisibleInListView(false)]
        [Delayed(true)]
        public DateTime DataFine
        {
            get { return GetDelayedPropertyValue<DateTime>("DataFine"); }
            set { SetDelayedPropertyValue<DateTime>("DataFine", value); }
        }

        private StatoElaborazioneJob fStatoElaborazioneJob;
        [Persistent("STATOELABORAZIONEJOB"), DisplayName("Stato Elaborazione Job")]
        [Appearance("RegistroTransazioniDettaglio.StatoElaborazioneJob.Enabled", AppearanceItemType.LayoutItem, "1=1", Enabled = false, FontStyle = FontStyle.Bold, FontColor = "Black")]
        public StatoElaborazioneJob StatoElaborazioneJob
        {
            get { return fStatoElaborazioneJob; }
            set { SetPropertyValue<StatoElaborazioneJob>("StatoElaborazioneJob", ref fStatoElaborazioneJob, value); }
        }

        [Persistent("FILEDATAIMPORT"), DevExpress.Xpo.DisplayName("File Data Import")]
        //  [RuleRequiredField("RReqField.RegistroDataImportTentativi.FileDataImport", DefaultContexts.Save, "File Data Import è un campo obbligatorio")]
        [Delayed(true)]
        public FileData FileDataImport
        {
            get { return GetDelayedPropertyValue<FileData>("FileDataImport"); }
            set { SetDelayedPropertyValue<FileData>("FileDataImport", value); }
        }
    }
}
