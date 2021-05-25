using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions,
    Persistent("MPDETTCREAODL")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettagli Creazione OdL MP")]
    [ImageName("Feature")]
    [NavigationItem(false)]
    [VisibleInDashboards(false)]
    public class DettaglioCreazioneOdL : XPObject
    {
        public DettaglioCreazioneOdL()
            : base()
        {
        }

        public DettaglioCreazioneOdL(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"),
        Size(4000)]
        [DbType("varchar(4000)"),
        DisplayName("Descrizione")]
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

        private string fSettimana;
        [Persistent("SETTIMANA")]
        [DisplayName("Settimana")]
        public string Settimana
        {
            get
            {
                return fSettimana;
            }
            set
            {
                SetPropertyValue<string>("Settimana", ref fSettimana, value);
            }
        }

        private DateTime fData;
        [Persistent("DATA"),
        DisplayName("Data"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        public DateTime Data
        {
            get
            {
                return fData;
            }
            set
            {
                SetPropertyValue<DateTime>("Data", ref fData, value);
            }
        }

        private string f_Utente;
        [Persistent("UTENTE"),
        Size(100),
        DisplayName("Utente")]
        [DbType("varchar(100)")]
        [System.ComponentModel.Browsable(false)]
        public string Utente
        {
            get
            {
                return f_Utente;
            }
            set
            {
                SetPropertyValue<string>("Utente", ref f_Utente, value);
            }
        }

        private RegPianificazioneMP fRegistroPianificazioneMP;
        [MemberDesignTimeVisibility(false),
        Association(@"RegPianiMP_DettCreazOdL", typeof(RegPianificazioneMP))]
        [Persistent("MPREGPIANIFICAZIONE"),
        DisplayName("Registro Pianificazione MP")]
        public RegPianificazioneMP RegistroPianificazioneMP
        {
            get
            {
                return fRegistroPianificazioneMP;
            }
            set
            {
                SetPropertyValue<RegPianificazioneMP>("RegistroPianificazioneMP", ref fRegistroPianificazioneMP, value);
            }
        }
    }
}
