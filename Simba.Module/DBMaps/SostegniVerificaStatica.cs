//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CAMS.Module.DBMaps
//{
//    class SostegniVerificaStatica
//    {
//    }
//}



using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Collections.Generic;
using System.ComponentModel;

namespace CAMS.Module.DBMaps
{
    [DefaultClassOptions, Persistent("V_REGRDLSOVERSTATICA")]
    [System.ComponentModel.DefaultProperty("CodApparato")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Sostegni Verifica Statica Dettaglio ")]
    [ImageName("BO_Country_v92")]
    [NavigationItem("Avvisi Periodici")]

    
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("V_REGRDLSOVERSTATICA.CommessaAielli", "OidCommessa = 4179", "Aielli", true, Index = 3)]
    public class SostegniVerificaStatica : XPLiteObject,  IMapsMarker
    {
        public SostegniVerificaStatica()
           : base()
        {
        }

        public SostegniVerificaStatica(Session session)
           : base(session)
        {
        }


        private string fcodice;
        [Key, Persistent("CODICE"), MemberDesignTimeVisibility(false)]
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


        //   anno stato          LATITUDE     LONGITUDE   PRESCRIZIONE commessa    oidcommessa INDIVIDUALICON
        //2021	Altri stati	40,546455	17,438106	-	Comune di Grottaglie	3408	


        private int fRegRdl;
        [Persistent("REGRDL"), XafDisplayName("RegRdl")]
        [ExplicitLoading]
        public int RegRdl
        {
            get
            {
                return fRegRdl;
            }
            set
            {
                SetPropertyValue<int>("RegRdl", ref fRegRdl, value);
            }
        }


        private string fCodApparato;
        [Persistent("CODAPPARATO"), XafDisplayName("CodApparato")]
        [ExplicitLoading]
        public string CodApparato
        {
            get
            {
                return fCodApparato;
            }
            set
            {
                SetPropertyValue<string>("CodApparato", ref fCodApparato, value);
            }
        }


        

        private int fAnno;
        [Persistent("ANNO"), XafDisplayName("Anno")]
        [ExplicitLoading]
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


        private string fStato;
        [Persistent("STATO"), XafDisplayName("Stato")]
        [ExplicitLoading]
        public string Stato
        {
            get
            {
                return fStato;
            }
            set
            {
                SetPropertyValue<string>("Stato", ref fStato, value);
            }
        }



        private string fPrescrizione;
        [Persistent("PRESCRIZIONE"), XafDisplayName("Prescrizione")]
        [ExplicitLoading]
        public string Prescrizione
        {
            get
            {
                return fPrescrizione;
            }
            set
            {
                SetPropertyValue<string>("Prescrizione", ref fPrescrizione, value);
            }
        }

      



        private double fLat;
        [Size(50),
        Persistent("LATITUDE"), XafDisplayName("Latitudine")]
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
        Persistent("LONGITUDE"), XafDisplayName("Longitudine")]
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

        private string fTitle;
        [Size(250), Persistent("TITLE"), XafDisplayName("Sostegno")]
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

  

        [Persistent("INDIVIDUALICON"), DevExpress.Xpo.DisplayName("ICONA")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        public string IndividualMarkerIcon { get; set; }




        private string fCommessa;
        [Persistent("CONTRATTO"), XafDisplayName("Contratto")]
        [ExplicitLoading]
        public string Commessa
        {
            get
            {
                return fCommessa;
            }
            set
            {
                SetPropertyValue<string>("Commessa", ref fCommessa, value);
            }
        }


        private int fOidCommessa;
        [Persistent("OIDCOMMESSA"), XafDisplayName("OidCommessa")]
        [ExplicitLoading]
        public int OidCommessa
        {
            get
            {
                return fOidCommessa;
            }
            set
            {
                SetPropertyValue<int>("OidCommessa", ref fOidCommessa, value);
            }
        }




        //public double Latitude { get; set; }
        //public double Longitude { get; set; }




        //private string fIndirizzodaGeo;
        //[Size(250), Persistent("GEOINDIRIZZO"), XafDisplayName("Indirizzo Vicino")]
        //[DbType("varchar(250)")]
        //[MemberDesignTimeVisibility(false)]
        //public string IndirizzodaGeo
        //{
        //    get
        //    {
        //        return fIndirizzodaGeo;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("IndirizzodaGeo", ref fIndirizzodaGeo, value);
        //    }
        //}
        /// <summary>
        /// ////////////////
        /// </summary>




    }

}