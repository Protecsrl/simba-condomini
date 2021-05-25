using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
    Persistent("REGOPERATIVODETTAGLIO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Reg. Operativo")]
    [ImageName("NewTask")]
    [NavigationItem(false)]
    public class RegistroOperativoDettaglio : XPObject
    {
        public RegistroOperativoDettaglio()
            : base()
        {
        }
        public RegistroOperativoDettaglio(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private RegistroRdL fRegistroRdL;
        [Association(@"REGISTRORDLRefDETTAGLIOOPERATIVO"),
        Persistent("REGRDL"),
        DisplayName("Registro RdL")]
        [ExplicitLoading]
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

        private RegistroSmistamentoDett fRegistroSmistamentoDett;
        [Persistent("REGSMISTAMENTODETT"),Association(@"RegistroSmistamentoDett_RegistroOperativoDettaglio"),
        DisplayName("Smistamento")]
        [Appearance("ROperativo.RegistroSmistamento.Enabled", Enabled = false)]
        [ExplicitLoading]
        public RegistroSmistamentoDett RegistroSmistamentoDett
        {
            get
            {
                
                return fRegistroSmistamentoDett;
            }
            set
            {
                SetPropertyValue<RegistroSmistamentoDett>("RegistroSmistamentoDett", ref fRegistroSmistamentoDett, value);
            }
        }


        private StatoOperativo fStatoOperativo;
        [Persistent("STATOOPERATIVO"),
        DisplayName("Stato Operativo")]
        [ExplicitLoading]
        public StatoOperativo StatoOperativo
        {
            get
            {
                return fStatoOperativo;
            }
            set
            {
                SetPropertyValue<StatoOperativo>("StatoOperativo", ref fStatoOperativo, value);
            }
        }

        private DateTime fDataOra;
        [Persistent("DATAORA"),
        DisplayName("Data Ora"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt"),
        DevExpress.ExpressApp.Model.ModelDefault("EditMask", "dd/MM/yyyy H:mm:ss tt")]
        public DateTime DataOra
        {
            get
            {
                return fDataOra;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOra", ref fDataOra, value);
            }
        }

        private int fDeltaTempoStato;
        [Persistent("DELTATEMPO"), DisplayName("Tempo di Stato")]
        [ModelDefault("DisplayFormat", "{0:D}")]
        public int DeltaTempoStato
        {
            get
            {
                return fDeltaTempoStato;
            }
            set
            {
                SetPropertyValue<int>("DeltaTempoStato", ref fDeltaTempoStato, value);
            }
        }


        private sbyte fIcona;
        [Persistent("ICONA"),
        DisplayName("Icona")]
        [MemberDesignTimeVisibility(false)]
        public sbyte Icona
        {
            get
            {
                return fIcona;
            }
            set
            {
                SetPropertyValue<sbyte>("Icona", ref fIcona, value);
            }
        }

        private DateTime fDataCompletamentoManuale;
        [Persistent("DATACOMPLETAMAN"),
        DisplayName("Data Completamento Manuale"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        public DateTime DataCompletamentoManuale
        {
            get
            {
                return fDataCompletamentoManuale;
            }
            set
            {
                SetPropertyValue<DateTime>("DataCompletamentoManuale", ref fDataCompletamentoManuale, value);
            }
        }
        private Boolean fFlagDataCompletamentoManuale;
        [Persistent("FLAGDATACOMPLETAMAN"),
      DisplayName("FlagDataCompletamentoManuale")]
        public Boolean FlagDataCompletamentoManuale
        {
            get
            {
                return fFlagDataCompletamentoManuale;
            }
            set
            {
                SetPropertyValue<Boolean>("FlagDataCompletamentoManuale", ref fFlagDataCompletamentoManuale, value);
            }
        }

        private Boolean fFlagCompletatoSO;
        [Persistent("FLAGCOMPLETATOSO"), DisplayName("Completato Sala Operativa")]
        public Boolean FlagCompletatoSO
        {
            get
            {
                return fFlagCompletatoSO;
            }
            set
            {
                SetPropertyValue<Boolean>("FlagCompletatoSO", ref fFlagCompletatoSO, value);
            }
        }

        private double fLat;
        [Size(50),
        Persistent("GEOLAT"),
        DisplayName("Georeferenziazione Latitudine")]
        [MemberDesignTimeVisibility(false)]
        public double GeoLatitude
        {
            get
            {
                return fLat;
            }
            set
            {
                SetPropertyValue<double>("GeoLatitude", ref fLat, value);
            }
        }

        private double fLng;
        [Size(50),
        Persistent("GEOLNG"),
        DisplayName("Georeferenziazione Longitudine")]
        [MemberDesignTimeVisibility(false)]
        public double GeoLongitude
        {
            get
            {
                return fLng;
            }
            set
            {
                SetPropertyValue<double>("GeoLongitude", ref fLng, value);
            }
        }

        private string fNoteOperative;
        [Size(250),
        Persistent("NOTEOPERATIVE"),
        DisplayName("Note Operative")]
        [DbType("varchar(250)")]
        public string NoteOperative
        {
            get
            {
                return fNoteOperative;
            }
            set
            {
                SetPropertyValue<string>("NoteOperative", ref fNoteOperative, value);
            }
        }

        private string fIndirizzodaGeo;
        [Size(250),
        Persistent("GEOINDIRIZZO"),
        DisplayName("Indirizzo Vicino")]
        [DbType("varchar(250)")]
        [MemberDesignTimeVisibility(false)]
        public string IndirizzodaGeo
        {
            get
            {
                return fIndirizzodaGeo;
            }
            set
            {
                SetPropertyValue<string>("IndirizzodaGeo", ref fIndirizzodaGeo, value);
            }
        }
        private string fDescrizione;
        [Size(4000), Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione")]
        //[RuleRequiredField("RdL.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //[Appearance("RdL.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]

        [DbType("varchar(4000)")]
        [VisibleInListView(false)]
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

        private string f_Utente;
        [Persistent("UTENTE"), Size(100), DevExpress.ExpressApp.DC.XafDisplayName("Utente")]
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

        private string _Url = string.Empty;
        [NonPersistent,
        DisplayName("Visualizza Posizione")]
        [EditorAlias("HyperLinkPropertyEditor")]
        public string Url
        {
            get
            {
                var tempLat = Evaluate("GeoLatitude");
                var tempLon = Evaluate("GeoLongitude");
                if (tempLat != null && tempLon != null)
                {
                    return String.Format("http://www.google.com/maps/place/{0},{1}/@{0},{1},{2}",
                        tempLat.ToString().Replace(",", "."), tempLon.ToString().Replace(",", "."), "16");
                }
                return null;
            }
            set
            {
                SetPropertyValue("Url", ref _Url, value);
            }
        }

        private string fTempoTrascorso = string.Empty;
        [NonPersistent, DisplayName("Tempo Trascorso")]
        public string TempoTrascorso
        {
            get
            {
                int minuti = this.DeltaTempoStato;
                string sDTS = TrasformaMinuti(minuti);
                return sDTS;
            }
        }

        private string TrasformaMinuti(int minuti)
        {
            var result = TimeSpan.FromMinutes(minuti);

            int days = (int)result.TotalDays;
            int hours = (int)result.TotalHours - (days * 24);
            int minutes = (int)result.TotalMinutes - ((int)result.TotalHours * 60);
            if (days != 0)
                return string.Format("{0} gg {1} h {2} min", days.ToString(), hours.ToString(), minutes.ToString());
            else if (hours != 0)
                return string.Format("{0} h {1} min",  hours.ToString(), minutes.ToString());
            else
                return string.Format("{0} min", minutes.ToString());
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", RegistroRdL.Descrizione, StatoOperativo!=null? StatoOperativo.CodStato:string.Empty);
        }
    }
}
