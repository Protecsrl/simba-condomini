using CAMS.Module.DBPlant;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("V_REGPOSIZIONIDETT")]
    //  [System.ComponentModel.DefaultProperty("RisorseTeam")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Reg.Posizioni Dett")]
    [ImageName("BO_Country_v92")]
    [NavigationItem(false)]
    public class RegistroPosizioniDettVista : XPLiteObject
    {
         public RegistroPosizioniDettVista()
            : base()
        {
        }

        public RegistroPosizioniDettVista(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fcodice;
        [Key,   Persistent("CODICE"), MemberDesignTimeVisibility(false)]
        public string Codice
        {
            get
            {
                return fcodice;
            }
            set
            {
                SetPropertyValue<string>("Codice", ref fcodice, value);
            }
        }


        private Risorse fRisorsa;
        [Association(@"vRegistroPosizioni_Risorsa"),Persistent("RISORSA"),DisplayName("Registro Posizioni Risorsa ")]
        [ExplicitLoading]
        public Risorse Risorsa
        {
            get
            {
                return fRisorsa;
            }
            set
            {
                SetPropertyValue<Risorse>("Risorsa", ref fRisorsa, value);
            }
        }

        //private RegistroOperativoDettaglio fRegistroOperativoDettaglio;
        //[Persistent("REGOPERATIVODETTAGLIO"),
        //DisplayName("Registro Operativo")]
        //public RegistroOperativoDettaglio RegistroOperativoDettaglio
        //{
        //    get
        //    {
        //        return fRegistroOperativoDettaglio;
        //    }
        //    set
        //    {
        //        SetPropertyValue<RegistroOperativoDettaglio>("RegistroOperativoDettaglio", ref fRegistroOperativoDettaglio, value);
        //    }
        //}

        private RisorseTeam fRisorseTeam;
        [Association(@"RisorsaTeam_RegPosizioniDettVista"), Persistent("RISORSETEAM"), DisplayName("RisorseTeam")]
        [ExplicitLoading]
        public RisorseTeam RisorseTeam
        {
            get
            {
                return fRisorseTeam;
            }
            set
            {
                SetPropertyValue<RisorseTeam>("RisorseTeam", ref fRisorseTeam, value);
            }
        }





        private double fLat;
        [Size(50),
        Persistent("GEOLAT"),
        DisplayName("Georeferenziazione Ultima Posizione Latitudine")]
        [MemberDesignTimeVisibility(false)]
        public double Latitude
        {
            get
            {
                return fLat;
            }
            set
            {
                SetPropertyValue<double>("Latitude", ref fLat, value);
            }
        }

        private double fLng;
        [Size(50),
        Persistent("GEOLNG"),
        DisplayName("Georeferenziazione Ultima Posizione Longitudine")]
        [MemberDesignTimeVisibility(false)]
        public double Longitude
        {
            get
            {
                return fLng;
            }
            set
            {
                SetPropertyValue<double>("Longitude", ref fLng, value);
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

        private DateTime fDataOra;
        [Persistent("DATAORA"),
        DisplayName("Data Ora"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
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

        private Immobile fImmobile;
        [Persistent("IMMOBILE"), DisplayName("Immobile")]
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


        private string _Url = string.Empty;
        [NonPersistent,
        DisplayName("Visualizza Ultima Posizione")]
        [EditorAlias("HyperLinkPropertyEditor")]
        public string Url
        {
            get
            {
                var tempLat = Evaluate("Latitude");
                var tempLon = Evaluate("Longitude");
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

    }

    }

