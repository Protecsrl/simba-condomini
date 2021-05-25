using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
namespace CAMS.Module.DBPlant
{   
    [DefaultClassOptions, Persistent("GEOLOCALIZZAZIONE")]
    [System.ComponentModel.DefaultProperty("FullName")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "GeoLocalizzazione")]
     [ImageName("ConvertToParagraphs")]
    [NavigationItem("Patrimonio")]
    public class GeoLocalizzazione : XPObject, IMapsMarker
    {
         public GeoLocalizzazione()
            : base()
        {
        }
         public GeoLocalizzazione(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {//Via della Travicella, Roma, Italia  Latitudine: 41.870346 | Longitudine: 12.50054

                this.Latitude = 41.87;
                this.Longitude = 12.50;
                this.ZoomMap = 5;
            }
        }


        private double fLatitude;
        [Size(50), Persistent("LATITUDE"), DevExpress.Xpo.DisplayName("Latitudine")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
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
        // public double Longitude { get; set; }
        private double fLongitude;
        [Size(50), Persistent("LONGITUDE"), DevExpress.Xpo.DisplayName("Longitude")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
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


        //public string Title { get; set; }
        private string fTitle;
        [Size(100), Persistent("TITLE"), DevExpress.Xpo.DisplayName("In Prossimità di")]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.GeoLocalizzazione.Title", DefaultContexts.Save, "è un campo obbligatorio")]
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
        [VisibleInListView(false), VisibleInDetailView(true)]
        public string IndividualMarkerIcon { get; set; }

        private double? fZoomMap;
        [Size(50), Persistent("ZOOMMAP"), DevExpress.Xpo.DisplayName("Zoom in Mappa")]
        [Appearance("GeoLocalizzazione.ZoomMap", Enabled = false)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [RuleRange("RuleRangeObject.GeoLocalizzazione.ZoomMap", "Save", 1, 10)]
        //[System.ComponentModel.DefaultValue(5)]
        [Appearance("GeoLocalizzazione.ZoomMap.visible", Visibility = ViewItemVisibility.Hide)]
        public double? ZoomMap
        {
            get
            {
                return fZoomMap;
            }
            set
            {
                SetPropertyValue<double?>("ZoomMap", ref fZoomMap, value);
            }
        }



        #region

        private string f_Utente;
        [DbType("varchar(100)")]
        [Persistent("UTENTE"),
        Size(100),
        DevExpress.Xpo.DisplayName("Utente")]
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

        private DateTime? fDataAggiornamento;
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        [Persistent("DATAUPDATE"),
        Size(100),
        DevExpress.Xpo.DisplayName("Data Aggiornamento")]
        [System.ComponentModel.Browsable(false)]

        public DateTime? DataAggiornamento
        {
            get
            {
                return fDataAggiornamento;
            }
            set
            {
                SetPropertyValue<DateTime?>("DataAggiornamento", ref fDataAggiornamento, value);
            }
        }
        #endregion
        //Via della Travicella, Roma, Italia  Latitudine: 41.870346 | Longitudine: 12.50054
        [PersistentAlias("Title + ' (' + Latitude  + ', ' + Longitude + ')'")]
        [XafDisplayName("Localizzazione")]
        [VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string FullName
        {
            get
            {
                if (Latitude == null) return null;
                if (Longitude == null) return null;
                return string.Format("{0} ({1} , {2})", this.Title, this.Latitude, this.Longitude);
            }
        }

        private string _Url = string.Empty;
        [NonPersistent, DevExpress.Xpo.DisplayName("Visualizza Posizione")]
        [Appearance("GeoLocalizzazione.Url.visualizza", Criteria = "InModifica", Visibility = ViewItemVisibility.Hide)]//, Appearance("_Url_Indirizzo.Url", Enabled = false)
        [EditorAlias("HyperLinkPropertyEditor")]
        [VisibleInListView(false)]
        public string Url
        {
            get
            {
                //Latitude    // Longitude  
                var tempLat = Evaluate("Latitude");
                var tempLon = Evaluate("Longitude");
                if (tempLat != null && tempLon != null)
                {
                    return String.Format("http://www.google.com/maps/place/{0},{1}/@{0},{1},{2}",
                         tempLat.ToString().Replace(",", "."), tempLon.ToString().Replace(",", "."), "5");
                }
                return null;
            }
            set
            {
                SetPropertyValue("Url", ref _Url, value);
            }
        }

        private bool fInModifica;
        [NonPersistent, Appearance("GeoLocalizzazione.InModifica", Enabled = false)]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public bool InModifica
        {
            get
            {
                return fInModifica;
            }
            set
            {
                SetPropertyValue<bool>("InModifica", ref fInModifica, value);
            }
        }

        #region indirizzo

        [NonPersistent]
        private XPCollection<GeoLocalizzazione> fGeoLocalizzazioneMaps;
        [XafDisplayName("Mappa")]
        [DevExpress.ExpressApp.CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        [Appearance("GeoLocalizzazione.GeoLocalizzazioneMaps.visible", Criteria = "Oid ==-1", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<GeoLocalizzazione> GeoLocalizzazioneMaps
        {
            get
            {
                if (this.Oid == -1) return null;
                    string ParCriteria = string.Format("Oid == {0}", Evaluate("Oid"));
                    fGeoLocalizzazioneMaps = new XPCollection<GeoLocalizzazione>(Session, CriteriaOperator.Parse(ParCriteria));              

                    return fGeoLocalizzazioneMaps;
            }
        }
                
        #endregion

        
    }
}


       //private bool fInModifica;
       // [NonPersistent, Appearance("GeoLocalizzazione.InModifica", Enabled = false)]
       // [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
       // public bool InModifica
       // {
       //     get
       //     {
       //         return fInModifica;
       //     }
       //     set
       //     {
       //         SetPropertyValue<bool>("InModifica", ref fInModifica, value);
       //     }

       // }
          

//private string _Url_Indirizzo = string.Empty;
//[NonPersistent, DevExpress.Xpo.DisplayName("Visualizza Posizione da indirizzo")]
//[Appearance("GeoLocalizzazione.Url_Indirizzo.visible", Criteria = "InModifica", Visibility = ViewItemVisibility.Hide)]//, Appearance("_Url_Indirizzo.Url", Enabled = false)
//[EditorAlias("HyperLinkPropertyEditor")]
//public string Url_Indirizzo
//{
//    get
//    {
//        var tempLat = Evaluate("Strada");
//        var tempLon = Evaluate("Comuni");
//        if (tempLat != null && tempLon != null)
//        {
//            // https://www.google.it/maps/place/Via+della+Travicella 61,+Roma 
//            // Longitude  Latitude
//            if (tempLat != null && tempLon != null)
//            {
//                return String.Format("https://www.google.com/maps/place/{0},{1}", Strada.ToString(), Comuni.ToString());
//            }
//        }
//        return null;
//    }
//    set
//    {
//        SetPropertyValue("Url_Indirizzo", ref _Url_Indirizzo, value);
//    }
//}
//, Appearance("Indirizzo.fStrRicercaGeo", Enabled = false)
//private string fStrRicercaGeo = string.Empty;
//[NonPersistent, DevExpress.Xpo.DisplayName("Ricerca Geo Posizione")]
//[VisibleInListView(false)]
//[Appearance("GeoLocalizzazione.StrRicercaGeo.visible", Criteria = "not InModifica", Visibility = ViewItemVisibility.Hide)]//, Appearance("_Url_Indirizzo.Url", Enabled = false)
//public string StrRicercaGeo
//{
//    get
//    {

//        var tempLat = Evaluate("Strada");
//        var tempLon = Evaluate("Comuni");
//        if (tempLat != null && tempLon != null)
//        {
//            return String.Format("{0} {1}, {2}", Strada, Civico, this.Comuni);
//        }
//        return null;
//    }
//    set
//    {
//        SetPropertyValue("Url_Indirizzo", ref _Url_Indirizzo, value);
//    }

//}

