using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
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
    [DefaultClassOptions, Persistent("INDIRIZZO")]
    [System.ComponentModel.DefaultProperty("FullName")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Indirizzi")]
    [Appearance("Indirizzo.inCreazione.noVisibile", TargetItems = "Url;Edifici", Criteria = @"Oid == -1", Visibility = ViewItemVisibility.Hide)]
    [ImageName("ConvertToParagraphs")]
    [NavigationItem("Patrimonio")]
    public class Indirizzo : XPObject, IMapsMarker
    {
        public Indirizzo()
            : base()
        {
        }
        public Indirizzo(Session session)
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


        private string fStrada;
        [Persistent("STRADA")]
        [Size(100), DevExpress.Xpo.DisplayName("Strada")]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.Indirizzo.Strada", DefaultContexts.Save, "è un campo obbligatorio")]
        [ImmediatePostData(true)]
        public string Strada
        {
            get
            {
                return fStrada;
            }
            set
            {
                SetPropertyValue<string>("Strada", ref fStrada, value);
            }
        }

        //  public double Latitude { get; set; }

        private double fLatitude;
        [Size(50), Persistent("LATITUDE"), DevExpress.Xpo.DisplayName("Latitudine")]
        [ModelDefault("DisplayFormat", "{0:f8}")]
        [ModelDefault("EditMask", "0:f8")]
        [Delayed(true)]
        public double Latitude
        {
            get
            {
                return GetDelayedPropertyValue<double>("Latitude");
            }
            set
            {
                SetDelayedPropertyValue<double>("Latitude", value);
            }

            //get
            //{
            //    return fLatitude;
            //}
            //set
            //{
            //    SetPropertyValue<double>("Latitude", ref fLatitude, value);
            //}
        }
        // public double Longitude { get; set; }
        private double fLongitude;
        [Size(50), Persistent("LONGITUDE"), DevExpress.Xpo.DisplayName("Longitude")]
        [ModelDefault("DisplayFormat", "{0:f8}")]
        [ModelDefault("EditMask", "0:f8")]
        [Delayed(true)]
        public double Longitude
        {
            get
            {
                return GetDelayedPropertyValue<double>("Longitude");
            }
            set
            {
                SetDelayedPropertyValue<double>("Longitude", value);
            }

            //get
            //{
            //    return fLongitude;
            //}
            //set
            //{
            //    SetPropertyValue<double>("Longitude", ref fLongitude, value);
            //}
        }


        //public string Title { get; set; }
        private string fTitle;
        [Size(100), Persistent("TITLE"), DevExpress.Xpo.DisplayName("Denominazione")]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.Indirizzo.Title", DefaultContexts.Save, "è un campo obbligatorio")]
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
       
         [ Persistent("INDIVIDUALICON"), DevExpress.Xpo.DisplayName("ICONA")]
         [VisibleInListView(false), VisibleInDetailView(true)]
        public string IndividualMarkerIcon { get; set; }
        //// or 
        //public string CommonMarkerIcon { 
        //    get { 
        //        return "http://js.devexpress.com/Demos/RealtorApp/images/map-marker.png"; 
        //    }



        private double? fZoomMap;
        [Size(50), Persistent("ZOOMMAP"), DevExpress.Xpo.DisplayName("Zoom in Mappa"), Appearance("Indirizzo.ZoomMap", Enabled = false)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [RuleRange("RuleRangeObject.Indirizzo.ZoomMap", "Save", 1, 10)]
        //[System.ComponentModel.DefaultValue(5)]
        [Appearance("indirizzo.ZoomMap.visible", Criteria = "1=1", Visibility = ViewItemVisibility.Hide)]
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

        private Regione fRegione;
        [Persistent("REGIONE"),
        DevExpress.Xpo.DisplayName("Regione")]
        [Appearance("Indirizzo.Abilita.Regione", Criteria = "not (Provincia is null)", Enabled = false)]
        [ImmediatePostData(true)]
        [RuleRequiredField("RReqField.Indirizzo.Regione", DefaultContexts.Save, "è un campo obbligatorio")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Regione Regione
        {
            get
            {
                return GetDelayedPropertyValue<Regione>("Regione");
            }
            set
            {
                SetDelayedPropertyValue<Regione>("Regione", value);
            }

            //get
            //{
            //    return fRegione;
            //}
            //set
            //{
            //    SetPropertyValue<Regione>("Regione", ref fRegione, value);
            //}
        }

        private Provincia fProvincia;
        [Persistent("PROVINCIA"),
        DevExpress.Xpo.DisplayName("Provincia")]
        [Appearance("Indirizzo.Abilita.Provincia", Criteria = "(Regione is null) OR (not (Comuni is null))", Enabled = false)]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Regione = '@This.Regione'")]
        [RuleRequiredField("RReqField.Indirizzo.Provincia", DefaultContexts.Save, "è un campo obbligatorio")]
        [ExplicitLoading]
        [Delayed(true)]
        public Provincia Provincia
        {
            get
            {
                return GetDelayedPropertyValue<Provincia>("Provincia");
            }
            set
            {
                SetDelayedPropertyValue<Provincia>("Provincia", value);
            }

            //get
            //{
            //    return fProvincia;
            //}
            //set
            //{
            //    SetPropertyValue<Provincia>("Provincia", ref fProvincia, value);
            //}
        }

        private Comuni fComuni;
        [Persistent("COMUNI"),
        DevExpress.Xpo.DisplayName("Comuni")]
        [Appearance("Indirizzo.Abilita.Comuni", Criteria = "Provincia is null", Enabled = false)]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Provincia = '@This.Provincia'")]
        [RuleRequiredField("RReqField.Indirizzo.Comuni", DefaultContexts.Save, "è un campo obbligatorio")]
        [ExplicitLoading]
        [Delayed(true)]
        public Comuni Comuni
        {
            get
            {
                return GetDelayedPropertyValue<Comuni>("Comuni");
            }
            set
            {
                SetDelayedPropertyValue<Comuni>("Comuni", value);
            }
            //get
            //{
            //    return fComuni;
            //}
            //set
            //{
            //    SetPropertyValue<Comuni>("Comuni", ref fComuni, value);
            //}
        }

        private string fCivico;
        [Size(50), Persistent("CIVICO"), DevExpress.Xpo.DisplayName("Civico")]
        [DbType("varchar(50)")]
        [ImmediatePostData(true)]
        public string Civico
        {
            get
            {
                return fCivico;
            }
            set
            {
                SetPropertyValue<string>("Civico", ref fCivico, value);
            }
        }

        private string fCap;
        [Size(50), Persistent("CAP"), DevExpress.Xpo.DisplayName("CAP")]
        [DbType("varchar(50)")]
        //[RuleRequiredField("RReqField.Indirizzo.Civico", DefaultContexts.Save, "è un campo obbligatorio")]
        public string Cap
        {
            get
            {
                return fCap;
            }
            set
            {
                SetPropertyValue<string>("Cap", ref fCap, value);
            }
        }

        [Association(@"Indirizzo_Edificio", typeof(Immobile)), DevExpress.Xpo.DisplayName("Immobili")]
        public XPCollection<Immobile> Immobili
        {
            get
            {
                return GetCollection<Immobile>("Immobili");
            }
        }


        private string f_Utente;
        [DbType("varchar(100)")]
        [Persistent("UTENTE"),
        Size(100),
        DevExpress.Xpo.DisplayName("Utente")]
        // Appearance("Indirizzo.Utente", Enabled = false)]
        //  [VisibleInListView(false),
        //  VisibleInLookupListView(false)]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public string Utente
        {
            get
            {
                return GetDelayedPropertyValue<string>("Utente");
            }
            set
            {
                SetDelayedPropertyValue<string>("Utente", value);
            }

            //get
            //{
            //    return f_Utente;
            //}
            //set
            //{
            //    SetPropertyValue<string>("Utente", ref f_Utente, value);
            //}
        }

        private DateTime? fDataAggiornamento;
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        [Persistent("DATAUPDATE"),
        Size(100),
        DevExpress.Xpo.DisplayName("Data Aggiornamento")]
        //Appearance("Indirizzo.DataAggiornamento", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        // [VisibleInListView(false),
        //  VisibleInLookupListView(false)]
        [Delayed(true)]
        public DateTime? DataAggiornamento
        {
            get
            {
                return GetDelayedPropertyValue<DateTime?>("DataAggiornamento");
            }
            set
            {
                SetDelayedPropertyValue<DateTime?>("DataAggiornamento", value);
            }

            //get
            //{
            //    return fDataAggiornamento;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime?>("DataAggiornamento", ref fDataAggiornamento, value);
            //}
        }

        //Via della Travicella, Roma, Italia  Latitudine: 41.870346 | Longitudine: 12.50054
        [PersistentAlias("Strada + ', ' + Civico  + ', ' + Comuni.DenomIta")]
        // [Calculated("FirstName + ' ' + LastName")]
        [XafDisplayName("Indirizzo")]
        [VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string FullName
        {
            get
            {

                if (Strada == null) return null;
                if (Comuni == null) return null;
                return string.Format("{0},{1} ({2})", this.Strada, this.Civico, this.Comuni.DenomIta);
            }
        }

        private string _Url = string.Empty;
        [NonPersistent, DevExpress.Xpo.DisplayName("Visualizza Posizione")]
        [Appearance("indirizzo.Url.visualizza", Criteria = "InModifica", Visibility = ViewItemVisibility.Hide)]//, Appearance("_Url_Indirizzo.Url", Enabled = false)
        [EditorAlias("HyperLinkPropertyEditor")]
        [VisibleInListView(false)]

        public string Url
        {
            get
            {
                //Latitude
                // Longitude  
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

        private string _Url_Indirizzo = string.Empty;
        [NonPersistent, DevExpress.Xpo.DisplayName("Visualizza Posizione da indirizzo")]
        [Appearance("indirizzo.Url_Indirizzo.visible", Criteria = "InModifica", Visibility = ViewItemVisibility.Hide)]//, Appearance("_Url_Indirizzo.Url", Enabled = false)
        [EditorAlias("HyperLinkPropertyEditor")]
        public string Url_Indirizzo
        {
            get
            {
                var tempLat = Evaluate("Strada");
                var tempLon = Evaluate("Comuni");
                if (tempLat != null && tempLon != null)
                {
                    // https://www.google.it/maps/place/Via+della+Travicella 61,+Roma 
                    // Longitude  Latitude
                    if (tempLat != null && tempLon != null)
                    {
                        return String.Format("https://www.google.com/maps/place/{0},{1}", Strada.ToString(), Comuni.ToString());
                    }
                }
                return null;
            }
            set
            {
                SetPropertyValue("Url_Indirizzo", ref _Url_Indirizzo, value);
            }
        }
        //, Appearance("Indirizzo.fStrRicercaGeo", Enabled = false)
        private string fStrRicercaGeo = string.Empty;
        [NonPersistent, DevExpress.Xpo.DisplayName("Ricerca Geo Posizione")]
        [VisibleInListView(false)]
        [Appearance("indirizzo.StrRicercaGeo.visible", Criteria = "not InModifica", Visibility = ViewItemVisibility.Hide)]//, Appearance("_Url_Indirizzo.Url", Enabled = false)
        public string StrRicercaGeo
        {
            get
            {
                var tempLat = Evaluate("Strada");
                var tempLon = Evaluate("Comuni");
                if (tempLat != null && tempLon != null)
                {
                    return String.Format("{0} {1}, {2}", Strada, Civico, this.Comuni);
                }
                return null;
            }
            set
            {
                SetPropertyValue("Url_Indirizzo", ref _Url_Indirizzo, value);
            }

        }

        #region e stesso

        //Dennis: This was added.
        //[NonPersistentDc]
        //[VisibleInListView(false)]
        //[DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        //[DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        //[DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        //[Appearance("mappa.disabilitata", Enabled = false)]
        //public IMapsMarker Self
        //{
        //    get { return this; }
        //}


        //Dennis: This was added.
        //[NonPersistentDc]
        //[VisibleInListView(false)]
        //[DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        //[DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        //[DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        //[Appearance("mappa.disabilitata1", Enabled = false)]
        //[DevExpress.Xpo.DisplayName("Mappe")]
        //public XPCollection<Indirizzo> Mappe
        //{
        //    get
        //    {
        //        var fff = GetCollection<Indirizzo>("Mappe");
        //        fff.Criteria = CriteriaOperator.Parse("Oid == 0");
        //        return fff;
        //    }
        //}



        #endregion
        #region indirizzo
        private XPCollection<Indirizzo> fListaFiltrataIndirizzos;
        [PersistentAlias("fListaFiltrataIndirizzos"), System.ComponentModel.DisplayName("Visualizza in Mappa")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<Indirizzo> ListaFiltrataIndirizzos
        {
            get
            {
                if (fListaFiltrataIndirizzos == null && this.Strada != null)
                {
                    fListaFiltrataIndirizzos = new XPCollection<Indirizzo>(Session);
                    RefreshfListaFiltrataRdLs();
                }
                return fListaFiltrataIndirizzos;
            }
        }
        private void RefreshfListaFiltrataRdLs()
        {
            if (fListaFiltrataIndirizzos == null)
            {
                return;
            }
            if (Strada == null)
            {
                return;
            }
            var ParCriteria = string.Format("Oid = {0} ", this.Oid);
            fListaFiltrataIndirizzos.Criteria = CriteriaOperator.Parse(ParCriteria);
            OnChanged("ListaFiltrataRdLs");
        }
        #endregion

        protected override void OnSaving()
        {
            base.OnSaving();
            // this.Strada = this.Title;
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (IsInvalidated)
                return;

            if (!this.IsLoading)
            {
                if (this.Oid > 1)
                {//fDataFermo
                    if (newValue != null && propertyName == "Strada")
                    {
                        string newV = (string)(newValue);
                        if (newV != null)
                        {
                            Title = string.Format("{0} {1}, {2}", newV, this.Civico, this.Comuni);
                        }
                    }

                    if (newValue != null && propertyName == "Civico")
                    {
                        string newV = (string)(newValue);
                        if (newV != null)
                        {
                            Title = string.Format("{0} {1}, {2}", this.Strada, newV, this.Comuni);
                        }
                    }
                    if (newValue != null && propertyName == "Comuni")
                    {
                        Comuni newV = (Comuni)(newValue);
                        if (newV != null)
                        {
                            Title = string.Format("{0} {1}, {2}", this.Strada, this.Civico, newV.ToString());
                        }
                    }
                }
            }
            //Richiedente

        }

        private bool fInModifica;
        [NonPersistent, Appearance("Indirizzo.InModifica", Enabled = false)]
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



    }
}
