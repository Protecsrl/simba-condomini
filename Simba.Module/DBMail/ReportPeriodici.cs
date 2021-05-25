using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//namespace CAMS.Module.DBMail
//{
//    class ReportPeriodici
//    {
//    }
//}
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

//namespace CAMS.Module.DBMail
//{
//    class ReportGiornalieri
//    {
//    }
//}

#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.Classi;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Validation;
using CAMS.Module.DBAngrafica;

namespace CAMS.Module.DBMail
{
    [DefaultClassOptions,  Persistent("REPORTPERIODICI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Report Periodici")]
    //[Appearance("Master.Disabilita.Dettagliomaggioredizero", TargetItems = "Descrizione;Immobile;UnitaMisura", Criteria = "MasterDettaglios.Count() > 0", Enabled = false)]
    [NavigationItem("Amministrazione")]
    [ImageName("Action_CreateDashboard")]
    public class ReportPeriodici : XPObject
    {
        public ReportPeriodici()
            : base()
        {
        }
        public ReportPeriodici(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();           

        }
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private string fDescrizione;
        [Size(200), Persistent("DESCRIZIONE")]
        [DbType("varchar(200)")]
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

        private CentroOperativo fCentroOperativo;
        [Persistent("CENTROOPERATIVO"),           XafDisplayName("Centro Operativo")]
        [RuleRequiredField("RReqField.ReportPeriodici.CentroOperativo", DefaultContexts.Save, "Il Centro Operativo è un campo obbligatorio")]
        [ExplicitLoading]
        [Delayed(true)]
        public CentroOperativo CentroOperativo
        {
            get { return GetDelayedPropertyValue<CentroOperativo>("CentroOperativo"); }
            set { SetDelayedPropertyValue<CentroOperativo>("CentroOperativo", value); }
        }

        //private Destinatari fDestinatari;
        [Persistent("DESTINATARI"), XafDisplayName("Destinatari e-mail")]
        [RuleRequiredField("RReqField.ReportPeriodici.Destinatari", DefaultContexts.Save, "Il Centro Operativo è un campo obbligatorio")]
        [Delayed(true)]
        public Destinatari Destinatari
        {
            get { return GetDelayedPropertyValue<Destinatari>("Destinatari"); }
            set { SetDelayedPropertyValue<Destinatari>("Destinatari", value); }
        }

        private TipoAzioniSpedizioneMail fTipoAzioniSpedizioneMail;
        [Persistent("TIPOAZIONISPEDIZIONEMAIL"), DevExpress.Xpo.DisplayName("Tipo Azioni Spedizione Mail")]
        public TipoAzioniSpedizioneMail TipoAzioniSpedizioneMail
        {
            get
            {
                return fTipoAzioniSpedizioneMail;
            }
            set
            {
                SetPropertyValue<TipoAzioniSpedizioneMail>("TipoAzioniSpedizioneMail", ref fTipoAzioniSpedizioneMail, value);
            }
        }

        private DateTime fDataUltimaSpedizione;
        [Persistent("DATAUPDATE")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Data ultima Spedizione")]
        [VisibleInListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataAggiornamento
        {
            get
            {
                return fDataUltimaSpedizione;
            }
            set
            {
                SetPropertyValue<DateTime>("DataUltimaSpedizione", ref fDataUltimaSpedizione, value);
            }
        }

        [Association(@"ReportPeriodici_Dettaglio", typeof(ReportPeriodiciDettaglio))]//Aggregated, 
        [ DisplayName("Elenco Apparati Assegnati")]
        //[Appearance("Master.Abilita.MasterDettaglio", Criteria = "Immobile is null", Enabled = false)]
        public XPCollection<ReportPeriodiciDettaglio> ReportPeriodiciDettaglios
        {
            get
            {
                return GetCollection<ReportPeriodiciDettaglio>("ReportPeriodiciDettaglios");
            }
        }




        public override string ToString()
        {
            if (this.Descrizione != null)
                return String.Format("{0}-{1}({2})", Descrizione, Destinatari.Cognome, Destinatari.Nome);

            return null;
        }
    }
}
