using System;
using CAMS.Module.Classi;
using DevExpress.ExpressApp.Model;

using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Drawing;
using DevExpress.Persistent.BaseImpl;

namespace CAMS.Module.DBCensimento
{
    [DefaultClassOptions, Persistent("CENSIMENTOBASE")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Censimento Base")]
    [ImageName("BO_Organization")]
    //[DeferredDeletion(true)]
    //[RuleCombinationOfPropertiesIsUnique("Unique.CENSIMENTOBASE.Descrizione", DefaultContexts.Save, "Commesse,CodDescrizione, Descrizione")]
    //[RuleCriteria("RuleWarning.Ed.Indirizzo.Geo.nonPre", DefaultContexts.Save, @"IndirizzoGeoValorizzato > 0",
    //  CustomMessageTemplate =
    //  "Attenzione:La L'indirizzo Selezionato non ha riferimenti di Georeferenziazione, \n\r inserirli prima di continuare!{IndirizzoGeoValorizzato}",
    //   SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Warning)]

    #region Abilitazione

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("CensimentoBase_SoloAttivo", "Abilitato == 1", "Attivi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("CensimentoBase_SoloNonAttivo", "Abilitato == 0", "non Attivi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("CensimentoBase_Tutto", "", "Tutto", Index = 2)]

    #endregion
    [NavigationItem("Censimento")]
    public class CensimentoBase : XPObject
    {
        private const string NA = "N/A";
        private const string FormattazioneCodice = "{0:000}";
                
        public CensimentoBase() : base() { }
        public CensimentoBase(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {
                this.Abilitato = FlgAbilitato.Si;
            }
        }

        //----------------------------
        private int foidcommessa;
        [Persistent("OIDCOMMESSA"), Size(100), DevExpress.Xpo.DisplayName("Oid Commessa")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [Delayed(true)]
        public int OidCommessa
        {
            get { return GetDelayedPropertyValue<int>("OidCommessa"); }
            set { SetDelayedPropertyValue<int>("OidCommessa", value); }
        }

        private string fDescrizione_Commessa;
        [Persistent("COMMESSA_DESCRIZIONE"), DevExpress.Xpo.DisplayName("Descrizione Commessa")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public string Descrizione_Commessa
        {
            get { return GetDelayedPropertyValue<string>("Descrizione_Commessa"); }
            set { SetDelayedPropertyValue<string>("Descrizione_Commessa", value); }
        }

        private int fOidEdificio;
        [Persistent("OIDEDIFICIO"), DevExpress.Xpo.DisplayName("OidEdificio")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        //[DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public int OidEdificio
        {
            get { return GetDelayedPropertyValue<int>("OidEdificio"); }
            set { SetDelayedPropertyValue<int>("OidEdificio", value); }
        }


        private string fEdificio_Descrizione;
        [Persistent("EDIFICIO_DESCRIZIONE"), DevExpress.Xpo.DisplayName("Descrizione Immobile")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public string Edificio_Descrizione
        {
            get { return GetDelayedPropertyValue<string>("Edificio_Descrizione"); }
            set { SetDelayedPropertyValue<string>("Edificio_Descrizione", value); }
        }


        private int fOidImpianto;
        [Persistent("OIDIMPIANTO"), DevExpress.Xpo.DisplayName("OidImpianto")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        //[DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public int OidImpianto
        {
            get { return GetDelayedPropertyValue<int>("OidImpianto"); }
            set { SetDelayedPropertyValue<int>("OidImpianto", value); }
        }


        private string fImpianto_Descrizione;
        [Persistent("IMPIANTO_DESCRIZIONE"), DevExpress.Xpo.DisplayName("Impianto Descrizione")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public string Impianto_Descrizione
        {
            get { return GetDelayedPropertyValue<string>("Impianto_Descrizione"); }
            set { SetDelayedPropertyValue<string>("Impianto_Descrizione", value); }
        }



        private int fOidApparato;
        [Persistent("OIDAPPARATO"), DevExpress.Xpo.DisplayName("OidApparato")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        //[DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public int OidApparato
        {
            get { return GetDelayedPropertyValue<int>("OidApparato"); }
            set { SetDelayedPropertyValue<int>("OidApparato", value); }
        }

        private string fQuadro_Descrizione;
        [Persistent("QUADRO_DESCRIZIONE"), DevExpress.Xpo.DisplayName("Quadro Descrizione")]
        [DbType("varchar(250)"), Size(250)]
        public string Quadro_Descrizione
        {
            get
            {
                return fQuadro_Descrizione;
            }
            set
            {
                SetPropertyValue<string>("Quadro_Descrizione", ref fQuadro_Descrizione, value);
            }
        }

        private string fQuadro_Strada;
        [Persistent("QUADRO_STRADA"), DevExpress.Xpo.DisplayName("Quadro Strada")]
        [DbType("varchar(250)"), Size(250)]
        public string Quadro_Strada
        {
            get
            {
                return fQuadro_Strada;
            }
            set
            {
                SetPropertyValue<string>("Quadro_Strada", ref fQuadro_Strada, value);
            }
        }


        private string fCodCL;
        [Persistent("CODCL"), DevExpress.Xpo.DisplayName("Cod. CL")]
        [DbType("varchar(250)"), Size(250)]
         public string CodCL
        {
            get
            {
                return fCodCL;
            }
            set
            {
                SetPropertyValue<string>("CodCL", ref fCodCL, value);
            }
        }

        private string fUbicazione;
        [Persistent("UBICAZIONE"), DevExpress.Xpo.DisplayName("Ubicazione")]
        [DbType("varchar(500)"), Size(250)]
        public string Ubicazione
        {
            get
            {
                return fUbicazione;
            }
            set
            {
                SetPropertyValue<string>("Ubicazione", ref fUbicazione, value);
            }
        }

        private string fPL_Tipo;
        [Persistent("PL_TIPO"), DevExpress.Xpo.DisplayName("PL Tipo")]
        [DbType("varchar(250)"), Size(250)]
        public string PL_Tipo
        {
            get
            {
                return fPL_Tipo;
            }
            set
            {
                SetPropertyValue<string>("PL_Tipo", ref fPL_Tipo, value);
            }
        }

        private string fPL_Potenza;
        [Persistent("PL_POTENZA"), DevExpress.Xpo.DisplayName("PL Potenza")]
        [DbType("varchar(50)"), Size(50)]
        public string PL_Potenza
        {
            get
            {
                return fPL_Potenza;
            }
            set
            {
                SetPropertyValue<string>("PL_Potenza", ref fPL_Potenza, value);
            }
        }
        private string fPL_Nota;
        [Persistent("PL_NOTA"), DevExpress.Xpo.DisplayName("PL Nota")]
        [DbType("varchar(400)"), Size(400)]
        public string PL_Nota
        {
            get
            {
                return fPL_Nota;
            }
            set
            {
                SetPropertyValue<string>("PL_Nota", ref fPL_Nota, value);
            }
        }


        private string fSO_Tipo;
        [Persistent("SO_TIPO"), DevExpress.Xpo.DisplayName("SO Tipo")]
        [DbType("varchar(250)"), Size(250)]
        public string SO_Tipo
        {
            get
            {
                return fSO_Tipo;
            }
            set
            {
                SetPropertyValue<string>("SO_Tipo", ref fSO_Tipo, value);
            }
        }

        private string fSO_AltezzaPalo;
        [Persistent("SO_ALTEZZAPALO"), DevExpress.Xpo.DisplayName("SO Altezza Palo")]
        [DbType("varchar(50)"), Size(50)]
        public string SO_AltezzaPalo
        {
            get
            {
                return fSO_AltezzaPalo;
            }
            set
            {
                SetPropertyValue<string>("SO_AltezzaPalo", ref fSO_AltezzaPalo, value);
            }
        }
        private string fSO_Nota;
        [Persistent("SO_NOTA"), DevExpress.Xpo.DisplayName("SO Nota")]
        [DbType("varchar(400)"), Size(400)]
        public string SO_Nota
        {
            get
            {
                return fSO_Nota;
            }
            set
            {
                SetPropertyValue<string>("SO_Nota", ref fSO_Nota, value);
            }
        }


        private string fCodPalo;
        [Persistent("CODPALO"), DevExpress.Xpo.DisplayName("Codice Palo")]
        [DbType("varchar(250)"), Size(250)]
        public string CodPalo
        {
            get
            {
                return fCodPalo;
            }
            set
            {
                SetPropertyValue<string>("CodPalo", ref fCodPalo, value);
            }
        }

        private string fCodPalo_Strada;
        [Persistent("PALO_STRADA"), DevExpress.Xpo.DisplayName("Palo Strada")]
        [DbType("varchar(250)"), Size(250)]
        public string CodPalo_Strada
        {
            get
            {
                return fCodPalo_Strada;
            }
            set
            {
                SetPropertyValue<string>("CodPalo_Strada", ref fCodPalo_Strada, value);
            }
        }

        private string fLinkQrCode;
        [Persistent("LINKQRCODE"), DevExpress.Xpo.DisplayName("Link Qr Code")]
        [DbType("varchar(2000)"), Size(2000)]
        public string LinkQrCode
        {
            get
            {
                return fLinkQrCode;
            }
            set
            {
                SetPropertyValue<string>("LinkQrCode", ref fLinkQrCode, value);
            }
        }
        private string fAssemblaggio;
        [Persistent("ASSEMBLAGGIO"), DevExpress.Xpo.DisplayName("Note")]
        [DbType("varchar(4000)"), Size(2000)]
        public string Assemblaggio
        {
            get
            {
                return fAssemblaggio;
            }
            set
            {
                SetPropertyValue<string>("Assemblaggio", ref fAssemblaggio, value);
            }
        }
        private string fNote;
        [Persistent("NOTE"), DevExpress.Xpo.DisplayName("Note")]
        [DbType("varchar(2000)"), Size(2000)]
        public string Note
        {
            get
            {
                return fNote;
            }
            set
            {
                SetPropertyValue<string>("Note", ref fNote, value);
            }
        }

        private NumeroPlafoniere fNumeroPlafoniere;
        [Persistent("NRPLAFONIERE"), DevExpress.ExpressApp.DC.XafDisplayName("Numero Plafoniere nel palo")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        [ImmediatePostData(true)]
        public NumeroPlafoniere NumeroPlafoniere
        {
            get
            {
                return fNumeroPlafoniere;
            }
            set
            {
                SetPropertyValue<NumeroPlafoniere>("NumeroPlafoniere", ref fNumeroPlafoniere, value);
            }
        }

        //------   , , , , , P, INTERVENTO_PL, TIPO_SS, HFT, INTERVENTO_SS, CODPALO, OID

        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        [ImmediatePostData(true)]
        public FlgAbilitato Abilitato
        {
            get
            {
                return fAbilitato;
            }
            set
            {
                SetPropertyValue<FlgAbilitato>("Abilitato", ref fAbilitato, value);
            }
        }

        private CensimentoFatto fCensimentoFatto;
        [Persistent("CENSIMENTOFATTO"), DevExpress.ExpressApp.DC.XafDisplayName("CensimentoFatto")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)] 
        public CensimentoFatto CensimentoFatto
        {
            get
            {
                return fCensimentoFatto;
            }
            set
            {
                SetPropertyValue<CensimentoFatto>("CensimentoFatto", ref fCensimentoFatto, value);
            }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAAGGIORNAMENTO")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy"), DevExpress.Xpo.DisplayName("Data Aggiornamento")]
        [VisibleInDetailView(true), VisibleInListView(false)] 
        [Delayed(true)]
        public DateTime DataAggiornamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAggiornamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataAggiornamento", value); }
        }
        private string f_Utente;
        [Persistent("UTENTE"),
        Size(200),
        DisplayName("Utente")]
        [DbType("varchar(200)")]
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

        private string f_LatLon;
        [Persistent("LATLON"),
        Size(200),
        DisplayName("Latitudine Longitudine")]
        [DbType("varchar(200)")]
        public string LatLon
        {
            get
            {
                return f_LatLon;
            }
            set
            {
                SetPropertyValue<string>("LatLon", ref f_LatLon, value);
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

        private string fConfermaCodPalo;
        [Persistent("CONFERMACODPALO"),
        Size(200),
        DisplayName("Conferma CodPalo")]
        [DbType("varchar(200)")]
        public string ConfermaCodPalo
        {
            get
            {
                return fConfermaCodPalo;
            }
            set
            {
                SetPropertyValue<string>("ConfermaCodPalo", ref fConfermaCodPalo, value);
            }
        }

        private int fNApparati;
        [Persistent("NRAPPARATI"), DevExpress.Xpo.DisplayName("Nr Apparati per tipologia")]
        [VisibleInListView(true), VisibleInLookupListView(true), VisibleInDetailView(true)]
        //[DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public int NApparati
        {
            get { return GetDelayedPropertyValue<int>("NApparati"); }
            set { SetDelayedPropertyValue<int>("NApparati", value); }
        }

        
        
        
        [Persistent("FILE"), DisplayName("File")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [ExplicitLoading()]
        [Delayed(true)]
        public FileData File
        {
            get
            {
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("File");
            }
            set
            {
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("File", value);
            }
        }
        private string fDescrizioneFile;
        [Persistent("DESCR_FILE"),
        Size(200),
        DisplayName("DescrizioneFile")]
        [DbType("varchar(200)")]
        public string DescrizioneFile
        {
            get
            {
                return fDescrizioneFile;
            }
            set
            {
                SetPropertyValue<string>("DescrizioneFile", ref fDescrizioneFile, value);
            }
        }


        private string fLAstLinkQrCode;
        [Persistent("LASTLINKQRCODE"), DevExpress.Xpo.DisplayName("Link Qr Code Precedente")]
        [DbType("varchar(2000)"), Size(2000)]
        public string LAstLinkQrCode
        {
            get
            {
                return fLAstLinkQrCode;
            }
            set
            {
                SetPropertyValue<string>("LAstLinkQrCode", ref fLAstLinkQrCode, value);
            }
        }

        private string fLinkQrCodeCensimento;
        [Persistent("LINKQRCODECENSIMENTO"), DevExpress.Xpo.DisplayName("Link Qr Code Censimento")]
        [DbType("varchar(2000)"), Size(2000)]
        public string LinkQrCodeCensimento
        {
            get
            {
                return fLinkQrCodeCensimento;
            }
            set
            {
                SetPropertyValue<string>("LinkQrCodeCensimento", ref fLinkQrCodeCensimento, value);            }
        }


        private string fLinkQrTecnico;
        [Persistent("LINKQRCODETECNICO"), DevExpress.Xpo.DisplayName("Link Qr Code Tecnico")]
        [DbType("varchar(2000)"), Size(2000)]
        public string LinkQrTecnico
        {
            get
            {
                return fLinkQrTecnico;
            }
            set
            {
                SetPropertyValue<string>("LinkQrTecnico", ref fLinkQrTecnico, value);
            }
        }

    }
}
