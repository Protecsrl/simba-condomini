using System;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using CAMS.Module.DBControlliNormativi;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;



namespace CAMS.Module.DBAngrafica
{
    [DefaultClassOptions, Persistent("REPORTVERIFICASTATICA")]
    [System.ComponentModel.DefaultProperty("Numero")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Report Verifica Statica")]
    [NavigationItem("Avvisi Periodici")]
    [VisibleInDashboards(false)]
    public class ReportVerificaStatica : XPObject
    {
        public ReportVerificaStatica()
            : base()
        {
        }

        public ReportVerificaStatica(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        private int fNumero;
        [Persistent("NUMERO"), DisplayName("Numero")]

        // [RuleRequiredField("Fornitore.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
        public int Numero
        {
            get
            {
                return fNumero;
            }
            set
            {
                SetPropertyValue<int>("Numero", ref fNumero, value);
            }
        }



        private DateTime fData;
        [Persistent("DATA"), System.ComponentModel.DisplayName("Data")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]

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

        private string fCodiceIdentificativoDaMappa;
        [Size(200),
        Persistent("CODICEIDENTIFICATIVODAMAPPA"), DisplayName("Codice Identificativo Da Mappa")]
        public string CodiceIdentificativoDaMappa
        {
            get
            {
                return fCodiceIdentificativoDaMappa;
            }
            set
            {
                SetPropertyValue<string>("CodiceIdentificativoDaMappa", ref fCodiceIdentificativoDaMappa, value);
            }
        }

        private double fLatitudine;
        [Persistent("LATITUDINE"), DisplayName("Latitudine")]

        // [RuleRequiredField("Fornitore.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
        public double Latitudine
        {
            get
            {
                return fLatitudine;
            }
            set
            {
                SetPropertyValue<double>("Latitudine", ref fLatitudine, value);
            }
        }

        private double fLongitudine;
        [Persistent("LONGITUDINE"), DisplayName("Longitudine")]

        // [RuleRequiredField("Fornitore.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
        public double Longitudine
        {
            get
            {
                return fLongitudine;
            }
            set
            {
                SetPropertyValue<double>("Longitudine", ref fLongitudine, value);
            }
        }

        private string fQuadro;
        [Size(100),
        Persistent("QUADRO"), DisplayName("Quadro")]
        public string Quadro
        {
            get
            {
                return fQuadro;
            }
            set
            {
                SetPropertyValue<string>("Quadro", ref fQuadro, value);
            }
        }

        private string fTipoPalo;
        [Size(100),
        Persistent("TIPOPALO"), DisplayName("TIPO PALO")]
        public string TipoPalo
        {
            get
            {
                return fTipoPalo;
            }
            set
            {
                SetPropertyValue<string>("TipoPalo", ref fTipoPalo, value);
            }
        }
        private string fTG;
        [Size(100),
        Persistent("TG"), DisplayName("TG")]
        public string TG
        {
            get
            {
                return fTG;
            }
            set
            {
                SetPropertyValue<string>("TG", ref fTG, value);
            }
        }

        private string fVerticalita;
        [Size(100),
        Persistent("VERTICALITA"), DisplayName("Verticalita")]
        public string Verticalita
        {
            get
            {
                return fVerticalita;
            }
            set
            {
                SetPropertyValue<string>("Verticalita", ref fVerticalita, value);
            }
        }

        private int? fCircCm;
        [Persistent("CIRCCM"), DisplayName("CircCm")]

        // [RuleRequiredField("Fornitore.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
        public int? CircCm
        {
            get
            {
                return fCircCm;
            }
            set
            {
                SetPropertyValue<int?>("CircCm", ref fCircCm, value);
            }
        }

        private int? fhM;
        [Persistent("HM"), DisplayName("hM")]

        // [RuleRequiredField("Fornitore.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
        public int? hM
        {
            get
            {
                return fhM;
            }
            set
            {
                SetPropertyValue<int?>("hM", ref fhM, value);
            }
        }
        private int? fNBracci;
        [Persistent("NBRACCI"), DisplayName("N.Bracci")]

        // [RuleRequiredField("Fornitore.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
        public int? NBracci
        {
            get
            {
                return fNBracci;
            }
            set
            {
                SetPropertyValue<int?>("NBracci", ref fNBracci, value);
            }
        }

        private string fLbracciM;
        [Persistent("LBRACCIM"), DisplayName("L.Bracci")]

        // [RuleRequiredField("Fornitore.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
        public string LbracciM
        {
            get
            {
                return fLbracciM;
            }
            set
            {
                SetPropertyValue<string>("LbracciM", ref fLbracciM, value);
            }
        }

        private string fGuaina;
        [Size(100),
        Persistent("GUAINA"), DisplayName("Guaina")]
        public string Guaina
        {
            get
            {
                return fGuaina;
            }
            set
            {
                SetPropertyValue<string>("Guaina", ref fGuaina, value);
            }
        }

        private string fVernice;
        [Size(100),
        Persistent("VERNICE"), DisplayName("Vernice")]
        public string Vernice
        {
            get
            {
                return fVernice;
            }
            set
            {
                SetPropertyValue<string>("Vernice", ref fVernice, value);
            }
        }

        private decimal? fVCorr;
        [Persistent("VCORR"), DisplayName("VCorr")]

        public decimal? VCorr
        {
            get
            {
                return fVCorr;
            }
            set
            {
                SetPropertyValue<decimal?>("VCorr", ref fVCorr, value);
            }
        }

        private double? fEcorr;
        [Persistent("ECORR"), DisplayName("Ecorr")]

        // [RuleRequiredField("Fornitore.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
        public double? Ecorr
        {
            get
            {
                return fEcorr;
            }
            set
            {
                SetPropertyValue<double?>("Ecorr", ref fEcorr, value);
            }
        }

        private decimal? fSPmax;
        [Persistent("SPMAX"), DisplayName("SPmax")]

        public decimal? SPmax
        {
            get
            {
                return fSPmax;
            }
            set
            {
                SetPropertyValue<decimal?>("SPmax", ref fSPmax, value);
            }
        }

        private decimal? fSPmin;
        [Persistent("SPMIN"), DisplayName("SPmin")]

        public decimal? SPmin
        {
            get
            {
                return fSPmin;
            }
            set
            {
                SetPropertyValue<decimal?>("SPmin", ref fSPmin, value);
            }
        }

        private int? fCircCorr;
        [Persistent("CIRCCORR"), DisplayName("CircCorr")]

        // [RuleRequiredField("Fornitore.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
        public int? CircCorr
        {
            get
            {
                return fCircCorr;
            }
            set
            {
                SetPropertyValue<int?>("CircCorr", ref fCircCorr, value);
            }
        }


        private string fCaricoExtra;
        [Size(100),
        Persistent("CARICOEXTRA"), DisplayName("Carico Extra")]
        public string CaricoExtra
        {
            get
            {
                return fCaricoExtra;
            }
            set
            {
                SetPropertyValue<string>("CaricoExtra", ref fCaricoExtra, value);
            }
        }

        private string fCollareBase;
        [Size(100),
        Persistent("COLLAREBASE"), DisplayName("Collare Base")]
        public string CollareBase
        {
            get
            {
                return fCollareBase;
            }
            set
            {
                SetPropertyValue<string>("CollareBase", ref fCollareBase, value);
            }
        }

        private string fEsitoSulCampo;
        [Size(100),
        Persistent("ESITOSULCAMPO"), DisplayName("Esito Sul Campo")]
        public string EsitoSulCampo
        {
            get
            {
                return fEsitoSulCampo;
            }
            set
            {
                SetPropertyValue<string>("EsitoSulCampo", ref fEsitoSulCampo, value);
            }
        }


        private string fEsitoFinaleMesiGaranzia;
        [Size(150), Persistent("ESITOFINALEMESIGARANZIA"), DisplayName("Esito Finale Mesi Garanzia")]

        public string EsitoFinaleMesiGaranzia
        {
            get
            {
                return fEsitoFinaleMesiGaranzia;
            }
            set
            {
                SetPropertyValue<string>("EsitoFinaleMesiGaranzia", ref fEsitoFinaleMesiGaranzia, value);
            }
        }


        private string fVia;
        [Size(250),
        Persistent("VIA"), DisplayName("Via")]
        public string Via
        {
            get
            {
                return fVia;
            }
            set
            {
                SetPropertyValue<string>("Via", ref fVia, value);
            }
        }

        private string fNote;
        [Size(4000),
        Persistent("NOTE"), DisplayName("Note")]
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


        private DateTime fDataInserimento;
        [Persistent("DATAINSERIMENTO"), System.ComponentModel.DisplayName("Data Acquisizione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]


        public DateTime DataInserimento
        {
            get
            {
                return fDataInserimento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataInserimento", ref fDataInserimento, value);
            }
        }


        private ControlliNormativi fControlliNormativi;
        [Persistent("CONTROLLINORMATIVI"), System.ComponentModel.DisplayName("Controlli Normativi")]
        public ControlliNormativi ControlliNormativi
        {
            get
            {
                return fControlliNormativi;
            }
            set
            {
                SetPropertyValue<ControlliNormativi>("ControlliNormativi", ref fControlliNormativi, value);
            }
        }


        private Immobile fImmobile;
        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
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

        private Servizio fServizio;
        [Persistent("SERVIZIO"), System.ComponentModel.DisplayName("Servizio")]
        public Servizio Servizio
        {
            get
            {
                return fServizio;
            }
            set
            {
                SetPropertyValue<Servizio>("Servizio", ref fServizio, value);
            }
        }

        private Asset fApparato;
        [Persistent("APPARATO"), System.ComponentModel.DisplayName("Apparato")]
        public Asset Apparato
        {
            get
            {
                return fApparato;
            }
            set
            {
                SetPropertyValue<Asset>("Apparato", ref fApparato, value);
            }
        }



        private RegistroRdL fRegRdL;
        [Persistent("REGRDL"), System.ComponentModel.DisplayName("RegistroRdL")]
        public RegistroRdL RegRdL
        {
            get
            {
                return fRegRdL;
            }
            set
            {
                SetPropertyValue<RegistroRdL>("RegRdL", ref fRegRdL, value);
            }
        }




        private string fNomeFileImport;
        [Persistent("NOMEFILEIMPORT"), System.ComponentModel.DisplayName("Nome File Importazione")]
        public string NomeFileImport
        {
            get
            {
                return fNomeFileImport;
            }
            set
            {
                SetPropertyValue<string>("NomeFileImport", ref fNomeFileImport, value);
            }
        }




    }
}




