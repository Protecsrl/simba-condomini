using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Collections.Generic;
using System.ComponentModel;

namespace CAMS.Module.DBMaps
{
    [DefaultClassOptions, Persistent("V_RISORSETEAMULTIMAPOSIZIONE")]
     [System.ComponentModel.DefaultProperty("RisorseTeam")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Ultima Posizione")]
    [ImageName("BO_Country_v92")]
    [NavigationItem("Ticket")]
    public class RisorseTeamUltimaPosizione : XPLiteObject, IMapsMarker
    {
         public RisorseTeamUltimaPosizione()
            : base()
        {
        }

         public RisorseTeamUltimaPosizione(Session session)
            : base(session)
        {
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

        private RisorseTeam fRisorseTeam;
        [Persistent("RISORSETEAM"), XafDisplayName("RisorseTeam")]
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

        Risorse fRisorsaCapo;
        [Persistent("RISORSACAPO"), XafDisplayName("Capo Squadra")]
        public Risorse RisorsaCapo { get { return fRisorsaCapo; } set { SetPropertyValue<Risorse>("RisorsaCapo", ref fRisorsaCapo, value); } }


        private Immobile fImmobile;
        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        [VisibleInListView(false)]
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
        private string fTipo;
        [Persistent("TIPO"), XafDisplayName("Tipo")]
        [Browsable(false)]
        public string Tipo
        {
            get
            {
                return fTipo;
            }
            set
            {
                SetPropertyValue<string>("Tipo", ref fTipo, value);
            }
        }

        //private double fLat;
        //[Size(50),
        //Persistent("GEOLAT"),        XafDisplayName("Georeferenziazione Ultima Posizione Latitudine")]
        //[MemberDesignTimeVisibility(false)]
        //public double Latitude
        //{
        //    get
        //    {
        //        return fLat;
        //    }
        //    set
        //    {
        //        SetPropertyValue<double>("Latitude", ref fLat, value);
        //    }
        //}

        //private double fLng;
        //[Size(50),
        //Persistent("GEOLNG"),        XafDisplayName("Georeferenziazione Ultima Posizione Longitudine")]
        //[MemberDesignTimeVisibility(false)]
        //public double Longitude
        //{
        //    get
        //    {
        //        return fLng;
        //    }
        //    set
        //    {
        //        SetPropertyValue<double>("Longitude", ref fLng, value);
        //    }
        //}

        private string fIndirizzodaGeo;
        [Size(250), Persistent("GEOINDIRIZZO"), XafDisplayName("Indirizzo Vicino")]
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
        /// <summary>
        /// ////////////////
        /// </summary>
        private string fTitle;
        [Size(250), Persistent("TITLE"), XafDisplayName("Risorsa")]
        [DbType("varchar(250)")]
        public string Title
        {
            get
            {
                return fTitle;
            }
            set
            {
                SetPropertyValue<string>("Title", ref fTitle, value);
            }
        }

        [Browsable(false)]
        public string Tooltip
        {
            get
            {
                string valuesStr = "";
                string[] captions = { "Category A", "Category B", "Category C" };
                float[] values = { NumeroAttivitaAgenda, NumeroAttivitaSospese, NumeroAttivitaEmergenza };


                int index = 0;
                foreach (float value in values)
                {
                    if (index != 0)
                        valuesStr += "<br>";
                    if (index >= captions.Length)
                        index = captions.Length - 1;
                    valuesStr += string.Format("{0}: {1}%", captions[index++], value);
                }
                return string.Format("<b>{0}</b><br>{1}", Title, valuesStr);
            }
        }

        //[Browsable(false)]
        //public string IconPath
        //{
        //    get
        //    {
        //        string IconaPat = @"Team.png";
        //        if (this.Tipo.Contains("Immobile"))
        //        {
        //            IconaPat = @"edificio1.png";
                     
        //        }
        //        else
        //        {
        //             IconaPat = @"Team.png";
        //            var tempObject = EvaluateAlias("NumeroAttivitaAgenda");
        //            if (tempObject != null)
        //            {
        //                int Conta = int.Parse(tempObject.ToString());
        //                if (Conta > 0 && Conta < 5)
        //                {
        //                    IconaPat = @"omino.png";
        //                }
        //                else if (Conta > 5 && Conta <= 10)
        //                {
        //                    IconaPat = @"omino1.png";
        //                }
        //                else if (Conta > 10 && Conta <= 15)
        //                {
        //                    IconaPat = @"omino2.png";
        //                }
        //                else if (Conta > 15 && Conta <= 20)
        //                {
        //                    IconaPat = @"omino3.png";
        //                }
        //                else
        //                {
        //                    IconaPat = @"omino_4.png";
        //                }
        //            }
        //        }
        //        return string.Format("{0}", IconaPat);
        //    }
        //}
        
        [Persistent("INDIVIDUALICON"), DevExpress.Xpo.DisplayName("ICONA")]
        [VisibleInListView(false),VisibleInDetailView(true)]
        public string IndividualMarkerIcon { get; set; }
        //public double Latitude { get; set; }
        //public double Longitude { get; set; }
        private double fLatitude;
        [Size(50), Persistent("LATITUDE"), XafDisplayName("Latitudine")]
        public double Latitude
        {
            get
            {
                return fLatitude;
            }
            set
            {
                SetPropertyValue<double>("Latitude", ref fLatitude, value);
            }
        }
      
        private double fLongitude;
        [Size(50), Persistent("LONGITUDE"), XafDisplayName("Longitude")]
        public double Longitude
        {
            get
            {
                return fLongitude;
            }
            set
            {
                SetPropertyValue<double>("Longitude", ref fLongitude, value);
            }
        }

   

        [Browsable(false)]
        public IList<float> Values
        {
            get
            {
                return new List<float> { NumeroAttivitaAgenda, NumeroAttivitaSospese, NumeroAttivitaEmergenza };
            }
        }
     

        [PersistentAlias("RisorseTeam.NumeroAttivitaAgenda"), XafDisplayName("Attività in Agenda")]
        public float NumeroAttivitaAgenda
        {
            get
            {
                var tempObject = EvaluateAlias("NumeroAttivitaAgenda");
                if (tempObject != null)
                {
                     
                    return float.Parse(tempObject.ToString());
                }
                else
                {
                    return float.MinValue;
                }
            }
        }
        [PersistentAlias("RisorseTeam.NumeroAttivitaSospese"), XafDisplayName("Attività Sospese")]
        public float NumeroAttivitaSospese
        {
            get
            {
                var tempObject = EvaluateAlias("NumeroAttivitaSospese");
                if (tempObject != null)
                {
                    return float.Parse(tempObject.ToString());
                }
                else
                {
                    return float.MinValue;
                }
            }
        }
        [PersistentAlias("RisorseTeam.NumeroAttivitaEmergenza"), XafDisplayName("Attività in Emergenza")]
        public float NumeroAttivitaEmergenza
        {
            get
            {
                var tempObject = EvaluateAlias("NumeroAttivitaEmergenza");
                if (tempObject != null)
                {
                    return float.Parse(tempObject.ToString());
                }
                else
                {
                    return float.MinValue;
                }
            }
        }
       

       
    
    }

    }

//private Risorse fRisorsa;
//[Association(@"vRegistroPosizioni_Risorsa"),
//Persistent("RISORSA"),
//XafDisplayName("Registro Posizioni Risorsa ")]
//public Risorse Risorsa
//{
//    get
//    {
//        return fRisorsa;
//    }
//    set
//    {
//        SetPropertyValue<Risorse>("Risorsa", ref fRisorsa, value);
//    }
//}

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

//private DateTime fDataOra;
//[Persistent("DATAORA"),
//DisplayName("Data Ora"),
//DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
//public DateTime DataOra
//{
//    get
//    {
//        return fDataOra;
//    }
//    set
//    {
//        SetPropertyValue<DateTime>("DataOra", ref fDataOra, value);
//    }
//}