
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Drawing;
using System;

namespace CAMS.Module.DBPlant.Vista
{
    [DefaultClassOptions, Persistent("APPARATO_MAP")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Apparecchiatura in Mappa")]
    [NavigationItem("Patrimonio")]
    //  AppPadreDescrizione  OidApparatoPadre
    [Appearance("ApparatoMap.ApparatoPadre.Visibility", AppearanceItemType.LayoutItem,
                @"[OidApparatoPadre] == 0",
                TargetItems = "pAppPadre",
               Priority = 1,
                Visibility = ViewItemVisibility.Hide)]
    [Appearance("ApparatoMap.AppSostegnoDescrizione.Visibility", AppearanceItemType.LayoutItem,
            @"[OidApparatoSostegno] == 0",
            TargetItems = "pAppSostegno",
           Priority = 1,
            Visibility = ViewItemVisibility.Hide)]

    [Appearance("ApparatoMap.AppPadreDescrizione.Visibility", AppearanceItemType.LayoutItem,
            @"IsNullOrEmpty(AppPadreDescrizione)",
            TargetItems = "ApparatiRelazionati",
           Priority = 1,
            Visibility = ViewItemVisibility.Hide)]

    [Appearance("ApparatoMap.ApparatoinSostegnos.Visibility", AppearanceItemType.LayoutItem,
            @"ApparatoinSostegnos.Count=0",
            TargetItems = "ApparatoinSostegnos",
           Priority = 1,
            Visibility = ViewItemVisibility.Hide)]

    [Appearance("ApparatoMap.ApparatoFiglis.Visibility", AppearanceItemType.LayoutItem,
            @"ApparatoFiglis.Count=0",
            TargetItems = "ApparatoFiglis",
           Priority = 1,
            Visibility = ViewItemVisibility.Hide)]
    public class AssetoMap : XPObject, IMapsMarker
    {
        public AssetoMap() : base() { }
        public AssetoMap(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        //private string fcodice;
        //[Key, Persistent("CODICE"), MemberDesignTimeVisibility(false)]
        //[DbType("varchar(250)")]
        [PersistentAlias("Oid"), MemberDesignTimeVisibility(false)]
        public string Codice
        {
            get { return Codice.ToString(); }
            //set
            //{
            //    SetPropertyValue<string>("Codice", ref fcodice, value);
            //}
        }

        //private int foidimpianto;
        [Persistent("OIDEDIFICIO")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public int OidEdificio
        {
            get { return GetDelayedPropertyValue<int>("OidEdificio"); }
            set { SetDelayedPropertyValue<int>("OidEdificio", value); }
        }



        [Persistent("EDIFICIO_DESCRIZIONE")]
        [DevExpress.Xpo.DisplayName("Immobile")]
        [DbType("varchar(400)")]
        [Delayed(true)]
        public string Edificio_Descrizione
        {
            get { return GetDelayedPropertyValue<string>("Edificio_Descrizione"); }
            set { SetDelayedPropertyValue<string>("Edificio_Descrizione", value); }
        }



        [Persistent("OIDIMPIANTO")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public int OidImpianto
        {
            get { return GetDelayedPropertyValue<int>("OidImpianto"); }
            set { SetDelayedPropertyValue<int>("OidImpianto", value); }
        }

        [Persistent("IMPIANTO_DESCRIZIONE")]
        [DevExpress.Xpo.DisplayName("Impianto")]
        [DbType("varchar(400)")]
        [Delayed(true)]
        public string Impianto_Descrizione
        {
            get { return GetDelayedPropertyValue<string>("Impianto_Descrizione"); }
            set { SetDelayedPropertyValue<string>("Impianto_Descrizione", value); }
        }

        [Persistent("OIDAPPARATO")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public int OidApparato
        {
            get { return GetDelayedPropertyValue<int>("OidApparato"); }
            set { SetDelayedPropertyValue<int>("OidApparato", value); }
        }

        [Persistent("DESCRIZIONE")]
        [DevExpress.Xpo.DisplayName("Descrizione")]
        [DbType("varchar(400)")]
        [Delayed(true)]
        public string Descrizione
        {
            get { return GetDelayedPropertyValue<string>("Descrizione"); }
            set { SetDelayedPropertyValue<string>("Descrizione", value); }
        }


        [Persistent("TITLE")]
        [DbType("varchar(4000)")]
        private string ftitle;

        [PersistentAlias("ftitle")]
        [DevExpress.Xpo.DisplayName("ftitle")]
        [Browsable(false)]
        public string Title
        {
            get { return ftitle; }
        }
        #region stadard apparato
        //[Persistent("OIDSTDAPPARATO")]
        //private int fOidstdapparato;

        //[PersistentAlias("fOidstdapparato")]
        //[DevExpress.Xpo.DisplayName("Oid StdApparato")]
        //[System.ComponentModel.Browsable(false)]
        //public int OidStdApparato
        //{
        //    get { return fOidstdapparato; }
        //}

        [Persistent("OIDSTDAPPARATO")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public int OidStdApparato
        {
            get { return GetDelayedPropertyValue<int>("OidStdApparato"); }
            set { SetDelayedPropertyValue<int>("OidStdApparato", value); }
        }

        //[Persistent("STDAPPARATO_DESCRIZIONE")]
        //[DbType("varchar(400)")]
        //private string fstdapparatodescrizione;

        //[PersistentAlias("fstdapparatodescrizione")]
        //[DevExpress.Xpo.DisplayName("Tipo Apparato")]
        //public string StdApparatoDescrizione
        //{
        //    get { return fstdapparatodescrizione; }
        //}
        [Persistent("STDAPPARATO_DESCRIZIONE")]
        [DevExpress.Xpo.DisplayName("Tipo Apparato")]
        [DbType("varchar(400)")]
        [Delayed(true)]
        public string StdApparatoDescrizione
        {
            get { return GetDelayedPropertyValue<string>("StdApparatoDescrizione"); }
            set { SetDelayedPropertyValue<string>("StdApparatoDescrizione", value); }
        }



        [Persistent("OIDAPPARATOSTDCLASSI")]
        [DevExpress.Xpo.DisplayName("Oid Classe di Tipo Apparato")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public int OidStdApparatoClassi
        {
            get { return GetDelayedPropertyValue<int>("OidStdApparatoClassi"); }
            set { SetDelayedPropertyValue<int>("OidStdApparatoClassi", value); }
        }


        //[Persistent("APPARATOSTDCLASSI_DESCRIZIONE")]
        //[DbType("varchar(400)")]
        //private string fstdapparatoclassidescrizione;

        //[PersistentAlias("fstdapparatoclassidescrizione")]
        //[DevExpress.Xpo.DisplayName("Classe di Tipo Apparato")]
        //public string StdApparatoClassiDescrizione
        //{
        //    get { return fstdapparatoclassidescrizione; }
        //}
        [Persistent("APPARATOSTDCLASSI_DESCRIZIONE")]
        [DevExpress.Xpo.DisplayName("Classe di Tipo Apparato")]
        [DbType("varchar(400)")]
        [Delayed(true)]
        public string StdApparatoClassiDescrizione
        {
            get { return GetDelayedPropertyValue<string>("StdApparatoClassiDescrizione"); }
            set { SetDelayedPropertyValue<string>("StdApparatoClassiDescrizione", value); }
        }

        [Persistent("PLSTDSINSOSTEGNO")]
        [DevExpress.Xpo.DisplayName("Tipo Apparato PLSTDSINSOSTEGNO")]
        [DbType("varchar(100)")]
        [Delayed(true)]
        [MemberDesignTimeVisibility(false)]
        public string PLStdinSostegno
        {
            get { return GetDelayedPropertyValue<string>("PLStdinSostegno"); }
            set { SetDelayedPropertyValue<string>("PLStdinSostegno", value); }
        }
        //[Persistent("NOMEICONAMAPPA")]
        //[DbType("varchar(400)")]
        //private string fnomeiconamappa;

        //[PersistentAlias("fnomeiconamappa")]
        //[DevExpress.Xpo.DisplayName("Nome Icona in Mappa")]
        //[Browsable(false)]
        //public string NomeIconaMappa
        //{
        //    get { return fnomeiconamappa; }
        //}
        [Persistent("NOMEICONAMAPPA")]
        [DevExpress.Xpo.DisplayName("Classe di Tipo Apparato")]
        [DbType("varchar(400)")]
        [Browsable(false)]
        [Delayed(true)]
        [MemberDesignTimeVisibility(false)]
        public string NomeIconaMappa
        {
            get { return GetDelayedPropertyValue<string>("NomeIconaMappa"); }
            set { SetDelayedPropertyValue<string>("NomeIconaMappa", value); }
        }
        #endregion

        [Persistent("LATITUDE")]
        private double flatitude;

        [PersistentAlias("flatitude")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        public double Latitude
        {
            get { return flatitude; }
        }


        [Persistent("LONGITUDE")]
        private double flongitude;

        [PersistentAlias("flongitude")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        public double Longitude
        {
            get { return flongitude; }
        }

        [Persistent("INDIVIDUALICON"), DevExpress.Xpo.DisplayName("ICONA")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public string IndividualMarkerIcon { get; set; }

        [Persistent("STRADAINPROSSIMITA")]
        [DevExpress.Xpo.DisplayName("Strada Vicina")]
        [DbType("varchar(100)")]
        [Delayed(true)]
        public string StradaInProssimita
        {
            get { return GetDelayedPropertyValue<string>("StradaInProssimita"); }
            set { SetDelayedPropertyValue<string>("StradaInProssimita", value); }
        }

        [Persistent("OIDSTRADA")]
        [DevExpress.Xpo.DisplayName("Oid Oidstrada")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public int OidStrada
        {
            get { return GetDelayedPropertyValue<int>("OidStrada"); }
            set { SetDelayedPropertyValue<int>("OidStrada", value); }
        }

        [Persistent("STRADA_DESCRIZIONE")]
        [DevExpress.Xpo.DisplayName("Strada")]
        [DbType("varchar(100)")]
        [Delayed(true)]
        public string Strada_Descrizione
        {
            get { return GetDelayedPropertyValue<string>("Strada_Descrizione"); }
            set { SetDelayedPropertyValue<string>("Strada_Descrizione", value); }
        }

        [Persistent("OIDAPPARATOPADRE")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public int OidApparatoPadre
        {
            get { return GetDelayedPropertyValue<int>("OidApparatoPadre"); }
            set { SetDelayedPropertyValue<int>("OidApparatoPadre", value); }
        }


        [Persistent("APPPADREDESCRIZIONE")]
        [DevExpress.Xpo.DisplayName("Apparecchiatura Padre")]
        [DbType("varchar(400)")]
        [Delayed(true)]
        public string AppPadreDescrizione
        {
            get { return GetDelayedPropertyValue<string>("AppPadreDescrizione"); }
            set { SetDelayedPropertyValue<string>("AppPadreDescrizione", value); }
        }

        [Persistent("OIDAPPARATOSOSTEGNO")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public int OidApparatoSostegno
        {
            get { return GetDelayedPropertyValue<int>("OidApparatoSostegno"); }
            set { SetDelayedPropertyValue<int>("OidApparatoSostegno", value); }
        }

        [Appearance("ApparatoMap.AppSostegnoDescrizione.Hide", Criteria = "OidApparatoSostegno == 0", Visibility = ViewItemVisibility.Hide)]
        [Persistent("APPASOSTEGNODESCRIZIONE")]
        [DevExpress.Xpo.DisplayName("Apparecchiatura Sostegno")]
        [DbType("varchar(400)")]
        [Delayed(true)]
        public string AppSostegnoDescrizione
        {
            get { return GetDelayedPropertyValue<string>("AppSostegnoDescrizione"); }
            set { SetDelayedPropertyValue<string>("AppSostegnoDescrizione", value); }
        }


        [Persistent("QUANTITA")]
        [DevExpress.Xpo.DisplayName("Quantità")]
        [Delayed(true)]
        public int Quantita
        {
            get { return GetDelayedPropertyValue<int>("Quantita"); }
            set { SetDelayedPropertyValue<int>("Quantita", value); }
        }


        [Persistent("MARCA")]
        [DevExpress.Xpo.DisplayName("Marca")]
        [DbType("varchar(100)")]
        [Delayed(true)]
        public string Marca
        {
            get { return GetDelayedPropertyValue<string>("Marca"); }
            set { SetDelayedPropertyValue<string>("Marca", value); }
        }


        [Persistent("MODELLO")]
        [DevExpress.Xpo.DisplayName("Modello")]
        [DbType("varchar(100)")]
        [Delayed(true)]
        public string Modello
        {
            get { return GetDelayedPropertyValue<string>("Modello"); }
            set { SetDelayedPropertyValue<string>("Modello", value); }
        }


        //[Persistent("CARATTERISTICHETECNICHE")]
        //[DevExpress.Xpo.DisplayName("Caratteristiche Tecniche")]
        //[DbType("varchar(4000)")]
        //[Delayed(true)]
        //public string Caratteristichetecniche
        //{
        //    get { return GetDelayedPropertyValue<string>("Caratteristichetecniche"); }
        //    set { SetDelayedPropertyValue<string>("Caratteristichetecniche", value); }
        //}


        //[Persistent("NOTE")]
        //[DevExpress.Xpo.DisplayName("Note")]
        //[DbType("varchar(1000)")]
        //[Delayed(true)]
        //public string Note
        //{
        //    get { return GetDelayedPropertyValue<string>("Note"); }
        //    set { SetDelayedPropertyValue<string>("Note", value); }
        //}



        [Persistent("ZONA")]
        [DevExpress.Xpo.DisplayName("Zona")]
        [DbType("varchar(100)")]
        [Delayed(true)]
        public string Zona
        {
            get { return GetDelayedPropertyValue<string>("Zona"); }
            set { SetDelayedPropertyValue<string>("Zona", value); }
        }



        [Persistent("ENTITAAPPARATO")]
        [DevExpress.Xpo.DisplayName("Entita Apparato")]
        [DbType("varchar(1000)")]
        [Delayed(true)]
        public string Entitaapparato
        {
            get { return GetDelayedPropertyValue<string>("Entitaapparato"); }
            set { SetDelayedPropertyValue<string>("Entitaapparato", value); }
        }



        [Persistent("TARGHETTA")]
        [DevExpress.Xpo.DisplayName("Targhetta")]
        [DbType("varchar(100)")]
        [Delayed(true)]
        public string Targhetta
        {
            get { return GetDelayedPropertyValue<string>("Targhetta"); }
            set { SetDelayedPropertyValue<string>("Targhetta", value); }
        }

        [Persistent("OIDREGRDLVRFST")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public int OidRegRdLVrst
        {
            get { return GetDelayedPropertyValue<int>("OidRegRdLVrst"); }
            set { SetDelayedPropertyValue<int>("OidRegRdLVrst", value); }
        }
        [Persistent("OIDREGRDLTT")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public int OidRegRdLTT
        {
            get { return GetDelayedPropertyValue<int>("OidRegRdLTT"); }
            set { SetDelayedPropertyValue<int>("OidRegRdLTT", value); }
        }


        //private const string UrlStringEditMask = @"(((http|https|ftp)\://)?[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;amp;%\$#\=~])*)|([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6})";
        //private string _UrlQrCode;
        //[Persistent("URLQRCODE")]
        ////[System.ComponentModel.Browsable(false)]
        //[VisibleInListView(false), VisibleInDetailView(false)]
        //[ModelDefault("EditMaskType", "RegEx")]
        //[ModelDefault("EditMask", UrlStringEditMask)]
        //[ToolTip("Specify a web or email address in the following format: " + UrlStringEditMask, null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
        //[Delayed(true)]
        //public string UrlQrCode
        //{
        //    get { return GetDelayedPropertyValue<string>("UrlQrCode"); }
        //    set { SetDelayedPropertyValue<string>("UrlQrCode", value); }
        //}

        //[Persistent("DATASHEET"), DevExpress.Xpo.DisplayName("Data Sheet")]
        //[VisibleInListView(false), VisibleInLookupListView(false)]
        //[FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        //[FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        //[Delayed(true)]
        //public FileData DataSheet
        //{
        //    get
        //    {
        //        return GetDelayedPropertyValue<FileData>("DataSheet");
        //    }
        //    set
        //    {
        //        SetDelayedPropertyValue<FileData>("DataSheet", value);
        //    }
        //}



        [NonPersistent]
        private XPCollection<AsettCaratteristicheTecniche> fApparatoCaratteristicheTecniches;
        [DevExpress.Xpo.DisplayName(@"Caratteristiche Tecniche")]
        [DevExpress.ExpressApp.CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<AsettCaratteristicheTecniche> ApparatoCaratteristicheTecniches
        {
            get
            {
                //string ParCriteria = string.Format("Apparato == {0}", Evaluate("OidApparato"));
                fApparatoCaratteristicheTecniches = new XPCollection<AsettCaratteristicheTecniche>(Session,
                    CriteriaOperator.Parse("Apparato = ?", Evaluate("OidApparato")));
                return fApparatoCaratteristicheTecniches;
                //return GetCollection<ApparatoCaratteristicheTecniche>("ApparatoCaratteristicheTecniches");
            }
        }


        [NonPersistent]
        private XPCollection<AssetoMap> _ApparatoMaps;
        [DevExpress.ExpressApp.DC.XafDisplayName("Mappa")]
        [DevExpress.ExpressApp.CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<AssetoMap> ApparatoMaps
        {
            get
            {
                //OperandValue[] parameters = new OperandValue();
                //parameters[0].Value = "Saloon";
                //parameters[1].Value = 100000;
                //string ParCriteria = string.Format("[<Apparato>][^.Oid == GeoLocalizzazione And Oid == {0}]", );
                _ApparatoMaps = new XPCollection<AssetoMap>(Session, CriteriaOperator.Parse(
                    "OidApparato = ? Or OidApparatoPadre = ?", Evaluate("OidApparato"), Evaluate("OidApparato")));
                //ParCriteria));

                return _ApparatoMaps;
            }
        }

        [NonPersistent]
        private XPCollection<Asset> _ApparatoinSostegnos;
        [DevExpress.ExpressApp.DC.XafDisplayName("Apparati in Sostegno")]
        [DevExpress.ExpressApp.CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<Asset> ApparatoinSostegnos
        {
            get
            {
                _ApparatoinSostegnos = new XPCollection<Asset>(Session, CriteriaOperator.Parse(
                    "ApparatoSostegno.Oid = ?", Evaluate("OidApparato")));
                //ParCriteria));     "Oid = ? Or ApparatoSostegno.Oid = ?", Evaluate("OidApparato"), Evaluate("OidApparato")));

                return _ApparatoinSostegnos;
            }
        }

        [NonPersistent]
        private XPCollection<Asset> _ApparatoFigli;
        [DevExpress.ExpressApp.DC.XafDisplayName("Apparati Figli")]
        [DevExpress.ExpressApp.CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<Asset> ApparatoFiglis
        {
            get
            {
                _ApparatoFigli = new XPCollection<Asset>(Session, CriteriaOperator.Parse(
                    "Oid = ? Or ApparatoPadre.Oid = ?", Evaluate("OidApparato"), Evaluate("OidApparato")));     

                return _ApparatoFigli;
            }
        }

    }
}

// non persistent e alias
//private Apparato fApparatoFiglio;
//[NonPersistent, DevExpress.Xpo.DisplayName("Apparato Figlio")]
//[Appearance("ApparatoMap.ApparatoFiglio", Enabled = false)]
//[VisibleInListView(false)]  
//public Apparato ApparatoFiglio
//{
//    get
//    {
//        return fApparatoFiglio;
//    }
//    set
//    {
//        SetPropertyValue<Apparato>("ApparatoFiglio", ref fApparatoFiglio, value);
//    }
//}



//[Persistent("DATASHEET")]
//private FileData fdatasheet;

//[PersistentAlias("fdatasheet")]
//[DevExpress.Xpo.DisplayName("Datasheet")]
//[DevExpress.Xpo.Size(SizeAttribute.Unlimited)]
//[VisibleInListView(false), VisibleInLookupListView(false)]
//public FileData Datasheet
//{
//    get { return fdatasheet; }
//}
//private DevExpress.Persistent.BaseImpl.FileData fDataSheet;

//[NonPersistent]
//private XPCollection<ApparatoMap> fApparatiFiglis;
//[DevExpress.ExpressApp.DC.XafDisplayName("Apparati a Valle")]
//[DevExpress.ExpressApp.CollectionOperationSet(AllowAdd = false, AllowRemove = false)]

//public XPCollection<ApparatoMap> ApparatiFiglis
//{
//    get
//    {
//        //fApparatiFiglis = new XPCollection<ApparatoMap>(Session, CriteriaOperator.Parse("[OidApparatoPadre] = 17820 Or [OidApparatoSostegno] = ?", Evaluate("OidApparato")));
//        fApparatiFiglis = new XPCollection<ApparatoMap>(Session, CriteriaOperator.Parse("[OidApparatoSostegno] = ?", Evaluate("foidapparato")));
//        return fApparatiFiglis;
//    }
//}





//[Persistent("PLINSOSTEGNO")]
//[DbType("varchar(4000)")]
//private string fplinsostegno;

//[PersistentAlias("fplinsostegno")]
//[DevExpress.Xpo.DisplayName("PL in Sostegno")]
//[Size(SizeAttribute.Unlimited)]
//public string PLinSostegno
//{
//    get { return fplinsostegno; }
//}
//[Persistent("PLINSOSTEGNO")]
//[DevExpress.Xpo.DisplayName("PL in Sostegno")]
//[DbType("varchar(4000)")]
//[Delayed(true)]
//public string PLinSostegno
//{
//    get { return GetDelayedPropertyValue<string>("PLinSostegno"); }
//    set { SetDelayedPropertyValue<string>("PLinSostegno", value); }
//}
//[Persistent("ENTITAAPPARATO")]
//[DbType("varchar(1000)")]
//private string fentitaapparato;

//[PersistentAlias("fentitaapparato")]
//[DevExpress.Xpo.DisplayName("Entita Apparato")]
//public string Entitaapparato
//{
//    get { return fentitaapparato; }
//}
//[Persistent("MATRICOLA")]
//[DbType("varchar(1000)")]
//private string fmatricola;

//[PersistentAlias("fmatricola")]
//[DevExpress.Xpo.DisplayName("Matricola")]
//public string Matricola
//{
//    get { return fmatricola; }
//}
//[Persistent("TARGHETTA")]
//[DbType("varchar(1000)")]
//private string ftarghetta;

//[PersistentAlias("ftarghetta")]
//[DevExpress.Xpo.DisplayName("Targhetta")]
//public string Targhetta
//{
//    get { return ftarghetta; }
//}

//[Persistent("OIDIMPIANTO")]
//private int foidimpianto;

//[PersistentAlias("foidimpianto")]

//public int OidImpianto
//{
//    get { return foidimpianto; }
//}
//[Persistent("EDIFICIO_DESCRIZIONE")]
//[DbType("varchar(400)")]
//private string fedificio_descrizione;

//[PersistentAlias("fedificio_descrizione")]
//[DevExpress.Xpo.DisplayName("Immobile")]
//public string Edificio_Descrizione
//{
//    get { return fedificio_descrizione; }
//}


//[Persistent("OIDEDIFICIO")]
//private int foidedificio;

//[PersistentAlias("foidedificio")]
//[System.ComponentModel.Browsable(false)]
//public int OidEdificio
//{
//    get { return foidedificio; }
//}

//[Persistent("OIDSTRADA")]
//private int fOidstrada;

//[PersistentAlias("fOidstrada")]
//[DevExpress.Xpo.DisplayName("Oid fOidstrada")]
//[System.ComponentModel.Browsable(false)]
//public int OidStrada
//{
//    get { return fOidstrada; }
//}
//[Persistent("STRADAINPROSSIMITA")]
//[DbType("varchar(100)")]
//private string fstradainprossimita;

//[PersistentAlias("fstradainprossimita")]
//[DevExpress.Xpo.DisplayName("Strada Vicina")]
//public string StradaInProssimita
//{
//    get { return fstradainprossimita; }
//}
//[Persistent("DESCRIZIONE")]
//[DbType("varchar(400)")]
//private string fdescrizione;

//[PersistentAlias("fdescrizione")]
//[DevExpress.Xpo.DisplayName("Descrizione")]
//public string Descrizione
//{
//    get { return fdescrizione; }
//}
//[Persistent("OIDAPPARATO")]
//private int foidapparato;

//[PersistentAlias("foidapparato")]
//[System.ComponentModel.Browsable(false)]
//public int OidApparato
//{
//    get { return foidapparato; }
//}
//[Persistent("IMPIANTO_DESCRIZIONE")]
//[DbType("varchar(400)")]
//private string fimpianto_descrizione;

//[PersistentAlias("fimpianto_descrizione")]
//[DevExpress.Xpo.DisplayName("Impianto")]
//public string Impianto_Descrizione
//{
//    get { return fimpianto_descrizione; }
//}