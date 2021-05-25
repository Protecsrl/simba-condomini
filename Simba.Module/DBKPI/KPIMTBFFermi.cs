using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
namespace CAMS.Module.DBKPI
{
    [DefaultClassOptions, Persistent("KPI_MTBF_FERMI")] //fai fare la vista ad andrea
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "KPI MTBF Fermi")]
    [ImageName("StackedLine")]
    [NavigationItem("KPI")]

    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
    public class KPIMTBFFermi :  XPLiteObject   
    {
        public KPIMTBFFermi()
            : base()
        {
        }

        public KPIMTBFFermi(Session session)
            : base(session)
        {
        }

        private string fcodice;
        [Key, Persistent("CODICE"), MemberDesignTimeVisibility(false)]
        [DbType("varchar(50)")]
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


        private string fEdificio;
        [Persistent("IMMOBILE"),  System.ComponentModel.DisplayName("Immobile")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(300)"), Size(300)]
        public string Immobile
        {
            get
            {
                return fEdificio;
            }
            set
            {
                SetPropertyValue<string>("Immobile", ref fEdificio, value);
            }
        }

        private string fImpianto;
        [Persistent("IMPIANTO"), System.ComponentModel.DisplayName("Impianto")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(300)"), Size(300)]
        public string Impianto
        {
            get
            {
                return fImpianto;
            }
            set
            {
                SetPropertyValue<string>("Impianto", ref fImpianto, value);
            }
        }

        private string fAsset;
        [Persistent("ASSET"), System.ComponentModel.DisplayName("Asset")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(300)"), Size(300)]
        public string Asset
        {
            get
            {
                return fAsset;
            }
            set
            {
                SetPropertyValue<string>("Asset", ref fAsset, value);
            }
        }

        private string fTipoAsset;
        [Persistent("TIPOASSET"), System.ComponentModel.DisplayName("Tipo Asset")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(300)"), Size(300)]
        public string TipoAsset
        {
            get
            {
                return fTipoAsset;
            }
            set
            {
                SetPropertyValue<string>("TipoAsset", ref fTipoAsset, value);
            }
        }
        // Area di Polo
        private string fAreadiPolo;
        [Persistent("AREADIPOLO"), System.ComponentModel.DisplayName("Area di Polo")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(100)"), Size(100)]
        public string AreadiPolo
        {
            get
            {
                return fAreadiPolo;
            }
            set
            {
                SetPropertyValue<string>("AreadiPolo", ref fAreadiPolo, value);
            }
        }

        private string fCentrodiCosto;
        [Persistent("CENTROCOSTO"), System.ComponentModel.DisplayName("Centro di Costo")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(100)"), Size(100)]
        public string CentrodiCosto
        {
            get
            {
                return fCentrodiCosto;
            }
            set
            {
                SetPropertyValue<string>("CentrodiCosto", ref fCentrodiCosto, value);
            }
        }

        private int fSettimana;
        [Persistent("SETTIMANA"), System.ComponentModel.DisplayName("Settimana")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int Settimana
        {
            get
            {
                return fSettimana;
            }
            set
            {
                SetPropertyValue<int>("Settimana", ref fSettimana, value);
            }
        }

        private int fMese;
        [Persistent("MESE"), System.ComponentModel.DisplayName("Mese")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Mese
        {
            get
            {
                return fMese;
            }
            set
            {
                SetPropertyValue<int>("Mese", ref fMese, value);
            }
        }

        private int fAnno;
        [Persistent("ANNO"), System.ComponentModel.DisplayName("Anno")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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



        private int ftempoMTBF;
        [Persistent("MTBF"), DisplayName("Mean Time Between To Failure")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int tempoMTBF
        {
            get
            {
                return ftempoMTBF;
            }
            set
            {
                SetPropertyValue<int>("tempoMTBF", ref ftempoMTBF, value);
            }
        }
        //  "dd\.hh\:mm\:ss"   "{0:hh\\:mm}"  mm\\:ss
        private TimeSpan ftMTBF;
        [NonPersistent, DisplayName("MTBF")]
        [ModelDefault("EditMaskType", "RegEx")]
        [ModelDefault("EditMask", "{0:dd\\.hh\\:mm}")]
        [ModelDefault("DisplayFormat", "{0:dd\\.hh\\:mm} days.hh.min")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public TimeSpan tMTBF
        {
            get
            {

                var tempObject = Evaluate("tempoMTBF");
                if (tempObject != null)
                {
                    TimeSpan duration = new TimeSpan(0, 0, this.tempoMTBF, 0);
                    return duration;
                }
                else
                {
                    TimeSpan duration = new TimeSpan(0, 0, 0, 0);
                    return duration;
                }
            }
        }
        // TimeSpan duration = new TimeSpan(1, 12, 23, 62);

        private int ftempoMTTR;
        [Persistent("MTTR"), DisplayName("Mean Time to Repaire")]
        [ToolTip("Espresso in minuti")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int tempoMTTR
        {
            get
            {
                return ftempoMTTR;
            }
            set
            {
                SetPropertyValue<int>("tempoMTTR", ref ftempoMTTR, value);
            }
        }

        private int ftempoMTTF;
        [Persistent("MTTF"), DisplayName("Mean Time to Failure")]
        [ToolTip("Espresso in minuti")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int tempoMTTF
        {
            get
            {
                return ftempoMTTF;
            }
            set
            {
                SetPropertyValue<int>("tempoMTTF", ref ftempoMTTF, value);
            }
        }


    

        private int fRdL0;
        [Persistent("CODRDL_0"), DisplayName("RdL Precedente")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int RdL0
        {
            get
            {
                return fRdL0;
            }
            set
            {
                SetPropertyValue<int>("RdL0", ref fRdL0, value);
            }
        }

        private int fRdL;
        [Persistent("CODRDL"), DisplayName("RdL")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int RdL
        {
            get
            {
                return fRdL;
            }
            set
            {
                SetPropertyValue<int>("RdL", ref fRdL, value);
            }
        }



        private DateTime fDataRiavvioPrecedente;
        [Persistent("DATA_RIAVVIO_PRECEDENTE"), System.ComponentModel.DisplayName("Data Riavvio Precedente")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMS.Module.PropertyEditors.CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask",CAMS.Module.PropertyEditors.CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask)]
        public DateTime DataRiavvioPrecedente
        {
            get
            {
                return fDataRiavvioPrecedente;
            }
            set
            {
                SetPropertyValue<DateTime>("DataRiavvioPrecedente", ref fDataRiavvioPrecedente, value);
            }
        }

        private DateTime fDataFermo;
        [Persistent("DATAFERMO"), System.ComponentModel.DisplayName("Data Fermo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMS.Module.PropertyEditors.CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMS.Module.PropertyEditors.CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask)]
        public DateTime DataFermo
        {
            get
            {
                return fDataFermo;
            }
            set
            {
                SetPropertyValue<DateTime>("DataFermo", ref fDataFermo, value);
            }
        }

        private DateTime fDataRiavvio;
        [Persistent("DATARIAVVIO"), System.ComponentModel.DisplayName("DataRiavvio")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMS.Module.PropertyEditors.CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMS.Module.PropertyEditors.CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask)]
        public DateTime DataRiavvio
        {
            get
            {
                return fDataRiavvio;
            }
            set
            {
                SetPropertyValue<DateTime>("DataRiavvio", ref fDataRiavvio, value);
            }
        }

        //   da qui opzionali

        private string fClasse;
        [Persistent("CLASSE"), System.ComponentModel.DisplayName("Classe")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(100)"), Size(100)]
        public string Classe
        {
            get
            {
                return fClasse;
            }
            set
            {
                SetPropertyValue<string>("Classe", ref fClasse, value);
            }
        }

        private int fClasseValore;
        [Persistent("CLASSEVALORE"), System.ComponentModel.DisplayName("Classe Valore")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int ClasseValore
        {
            get
            {
                return fClasseValore;
            }
            set
            {
                SetPropertyValue<int>("ClasseValore", ref fClasseValore, value);
            }
        }



        private string fSerie;
        [Persistent("SERIE"), System.ComponentModel.DisplayName("Serie")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(100)"), Size(100)]
        public string Serie
        {
            get
            {
                return fSerie;
            }
            set
            {
                SetPropertyValue<string>("Serie", ref fSerie, value);
            }
        }

        private int fSerieValore;
        [Persistent("SERIEVALORE"), System.ComponentModel.DisplayName("Serie Valore")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int SerieValore
        {
            get
            {
                return fSerieValore;
            }
            set
            {
                SetPropertyValue<int>("SerieValore", ref fSerieValore, value);
            }
        }



        #region OIDEDIFICIO, OIDREFERENTECOFELY
        private int fOidEdificio;
        [Persistent("OIDEDIFICIO"), System.ComponentModel.DisplayName("OidEdificio")]
        [System.ComponentModel.Browsable(false)]
        public int OidEdificio
        {
            get
            {
                return fOidEdificio;
            }
            set
            {
                SetPropertyValue<int>("OidEdificio", ref fOidEdificio, value);
            }
        }

        private int fOidImpianto;
        [Persistent("OIDIMPIANTO"), System.ComponentModel.DisplayName("OidImpianto")]
        [System.ComponentModel.Browsable(false)]
        public int OidImpianto
        {
            get
            {
                return fOidImpianto;
            }
            set
            {
                SetPropertyValue<int>("OidImpianto", ref fOidImpianto, value);
            }
        }

        private int fOidApparato;
        [Persistent("OIDAPPARATO"), System.ComponentModel.DisplayName("fOidApparato")]
        [System.ComponentModel.Browsable(false)]
        public int OidApparato
        {
            get
            {
                return fOidApparato;
            }
            set
            {
                SetPropertyValue<int>("OidApparato", ref fOidApparato, value);
            }
        }


        private int fOidReferenteCofely;
        [Persistent("OIDREFERENTECOFELY"), System.ComponentModel.DisplayName("OidReferenteCofely")] // pm
        [System.ComponentModel.Browsable(false)]
        public int OidReferenteCofely
        {
            get
            {
                return fOidReferenteCofely;
            }
            set
            {
                SetPropertyValue<int>("OidReferenteCofely", ref fOidReferenteCofely, value);
            }
        }

   


        #endregion



    }
}
