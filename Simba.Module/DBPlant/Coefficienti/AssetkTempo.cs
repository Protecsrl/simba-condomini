using System.Collections.Generic;
using System.Linq;
using System;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBPlant.Coefficienti
{
    [DefaultClassOptions, Persistent("APPKTEMPO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Apparato Coefficienti Tempo")]
    [NavigationItem(false)]
    [ImageName("Ktempo")]
    public class AssetkTempo : XPObject
    {
        public AssetkTempo()
            : base()
        {
        }

        public AssetkTempo(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (Oid == -1)
            {
                KCondizioneValore = 1;
                fKDimensioneValore = 1;
                KGuastoValore = 1;
                KUbicazioneValore = 1;
                KUtenzaValore = 1;
                KOttimizzazioneValore = 1;
            }
        }
        private int fkCondizioneOid;
        [Persistent("KCONDIZIONEOID"),
        DisplayName("Coeff. Condizione Padre")]
        [Delayed(true)]
        public int KCondizioneOid
        {
            get { return GetDelayedPropertyValue<int>("KCondizioneOid"); }
            set { SetDelayedPropertyValue<int>("KCondizioneOid", value); }

            //get
            //{
            //    return fkCondizioneOid;
            //}
            //set
            //{
            //    SetPropertyValue<int>("KCondizioneOid", ref fkCondizioneOid, value);
            //}
        }

        private string fKCondizioneDesc;
        [Persistent("CONDIZIONEDESC"),
        Size(50),
        DisplayName("Coeff. Condizione Descrizione")]
        [DbType("varchar(50)")]
        [Delayed(true)]
        public string KCondizioneDesc
        {
            get { return GetDelayedPropertyValue<string>("KCondizioneDesc"); }
            set { SetDelayedPropertyValue<string>("KCondizioneDesc", value); }
            //get
            //{
            //    return fKCondizioneDesc;
            //}
            //set
            //{
            //    SetPropertyValue<string>("KCondizioneDesc", ref fKCondizioneDesc, value);
            //}
        }

        private double fKCondizioneValore;
        [Persistent("CONDIZIONEVALORE"),
        DisplayName("Coeff. Condizione Valore")]
        [Delayed(true)]
        public double KCondizioneValore
        {
            get { return GetDelayedPropertyValue<double>("KCondizioneValore"); }
            set { SetDelayedPropertyValue<double>("KCondizioneValore", value); }
            //get
            //{
            //    return fKCondizioneValore;
            //}
            //set
            //{
            //    SetPropertyValue<double>("KCondizioneValore", ref fKCondizioneValore, value);
            //}
        }

        private int fkDimensioneOid;
        [Persistent("KDIMENSIONEOID"),
        DisplayName("Coeff. Dimensione Padre")]
        [Delayed(true)]
        public int KDimensioneOid
        {
            get { return GetDelayedPropertyValue<int>("KDimensioneOid"); }
            set { SetDelayedPropertyValue<int>("KDimensioneOid", value); }
            //get
            //{
            //    return fkDimensioneOid;
            //}
            //set
            //{
            //    SetPropertyValue<int>("KDimensioneOid", ref fkDimensioneOid, value);
            //}
        }

        private string fKDimensioneDesc;
        [Persistent("KDIMENSIONEDESC"),
        Size(50),
        DisplayName("Coeff. Dimensione Descrizione")]
        [DbType("varchar(50)")]
        [Delayed(true)]
        public string KDimensioneDesc
        {
            get { return GetDelayedPropertyValue<string>("KDimensioneDesc"); }
            set { SetDelayedPropertyValue<string>("KDimensioneDesc", value); }
            //get
            //{
            //    return fKDimensioneDesc;
            //}
            //set
            //{
            //    SetPropertyValue<string>("KDimensioneDesc", ref fKDimensioneDesc, value);
            //}
        }

        private double fKDimensioneValore;
        [Persistent("KDIMENSIONEVALORE"),
        DisplayName("Coeff. Dimensione Valore")]
        [Delayed(true)]
        public double KDimensioneValore
        {
            get { return GetDelayedPropertyValue<double>("KDimensioneValore"); }
            set { SetDelayedPropertyValue<double>("KDimensioneValore", value); }
            //get
            //{
            //    return fKDimensioneValore;
            //}
            //set
            //{
            //    SetPropertyValue<double>("KDimensioneValore", ref fKDimensioneValore, value);
            //}
        }

        private int fkGuastoOid;
        [Persistent("KGUASTOOID"),
        DisplayName("Coeff. Guasto Padre")]
        [Delayed(true)]
        public int KGuastoOid
        {
            get { return GetDelayedPropertyValue<int>("KGuastoOid"); }
            set { SetDelayedPropertyValue<int>("KGuastoOid", value); }

            //get
            //{
            //    return fkGuastoOid;
            //}
            //set
            //{
            //    SetPropertyValue<int>("KGuastoOid", ref fkGuastoOid, value);
            //}
        }

        private string fKGuastoDesc;
        [Persistent("KGUASTODESC"),
        Size(50),
        DisplayName("Coeff. Guasto Descrizione")]
        [DbType("varchar(50)")]
        [Delayed(true)]
        public string KGuastoDesc
        {
            get { return GetDelayedPropertyValue<string>("KGuastoDesc"); }
            set { SetDelayedPropertyValue<string>("KGuastoDesc", value); }

            //get
            //{
            //    return fKGuastoDesc;
            //}
            //set
            //{
            //    SetPropertyValue<string>("KGuastoDesc", ref fKGuastoDesc, value);
            //}
        }

        private double fKGuastoValore;
        [Persistent("KGUASTOVALORE"),
        DisplayName("Coeff. Guasto Valore")]
        [Delayed(true)]
        public double KGuastoValore
        {
            get { return GetDelayedPropertyValue<double>("KGuastoValore"); }
            set { SetDelayedPropertyValue<double>("KGuastoValore", value); }

            //get
            //{
            //    return fKGuastoValore;
            //}
            //set
            //{
            //    SetPropertyValue<double>("KGuastoValore", ref fKGuastoValore, value);
            //}
        }

        private int fkUbicazioneOid;
        [Persistent("KUBICAZIONEOID"),
        DisplayName("Coeff. Ubicazione Padre")]
        [Delayed(true)]
        public int KUbicazioneOid
        {
            get { return GetDelayedPropertyValue<int>("KUbicazioneOid"); }
            set { SetDelayedPropertyValue<int>("KUbicazioneOid", value); }

            //get
            //{
            //    return fkUbicazioneOid;
            //}
            //set
            //{
            //    SetPropertyValue<int>("KUbicazioneOid", ref fkUbicazioneOid, value);
            //}
        }

        private string fKUbicazioneDesc;
        [Persistent("KUBICAZIONEDESC"),
        Size(200),
        DisplayName("Coeff. Ubicazione Descrizione")]
        [DbType("varchar(200)")]
        [Delayed(true)]
        public string KUbicazioneDesc
        {
            get { return GetDelayedPropertyValue<string>("KUbicazioneDesc"); }
            set { SetDelayedPropertyValue<string>("KUbicazioneDesc", value); }
            //get
            //{
            //    return fKUbicazioneDesc;
            //}
            //set
            //{
            //    SetPropertyValue<string>("KUbicazioneDesc", ref fKUbicazioneDesc, value);
            //}
        }

        private double fKUbicazioneValore;
        [Persistent("KUBICAZIONEVALORE"),
        DisplayName("Coeff. Ubicazione Valore")]
        [Delayed(true)]
        public double KUbicazioneValore
        {
            get { return GetDelayedPropertyValue<double>("KUbicazioneValore"); }
            set { SetDelayedPropertyValue<double>("KUbicazioneValore", value); }

            //get
            //{
            //    return fKUbicazioneValore;
            //}
            //set
            //{
            //    SetPropertyValue<double>("KUbicazioneValore", ref fKUbicazioneValore, value);
            //}
        }

        private int fKUtenzaOid;
        [Persistent("KUTENZAOID"),
        DisplayName("Coeff. Utenza Padre")]
        [Delayed(true)]
        public int KUtenzaOid
        {
            get { return GetDelayedPropertyValue<int>("KUtenzaOid"); }
            set { SetDelayedPropertyValue<int>("KUtenzaOid", value); }

            //get
            //{
            //    return fKUtenzaOid;
            //}
            //set
            //{
            //    SetPropertyValue<int>("KUtenzaOid", ref fKUtenzaOid, value);
            //}
        }

        private string fKUtenzaDesc;
        [Persistent("KUTENZADESC"),
        Size(50),
        DisplayName("Coeff. Utenza Descrizione")]
        [DbType("varchar(50)")]
        [Delayed(true)]
        public string KUtenzaDesc
        {
            get { return GetDelayedPropertyValue<string>("KUtenzaDesc"); }
            set { SetDelayedPropertyValue<string>("KUtenzaDesc", value); }

            //get
            //{
            //    return fKUtenzaDesc;
            //}
            //set
            //{
            //    SetPropertyValue<string>("KUtenzaDesc", ref fKUtenzaDesc, value);
            //}
        }

        private double fKUtenzaValore;
        [Persistent("KUTENZAVALORE"),
        DisplayName("Coeff. Utenza Valore")]
        [Delayed(true)]
        public double KUtenzaValore
        {
            get { return GetDelayedPropertyValue<double>("KUtenzaValore"); }
            set { SetDelayedPropertyValue<double>("KUtenzaValore", value); }
            //get
            //{
            //    return fKUtenzaValore;
            //}
            //set
            //{
            //    SetPropertyValue<double>("KUtenzaValore", ref fKUtenzaValore, value);
            //}
        }

        private int fKOttimizzazioneOid;
        [Persistent("KOTTIMIZZAZIONEOID"),
        DisplayName("Coeff. Ottimizzazione Padre")]
        [Delayed(true)]
        public int KOttimizzazioneOid
        {
            get { return GetDelayedPropertyValue<int>("KOttimizzazioneOid"); }
            set { SetDelayedPropertyValue<int>("KOttimizzazioneOid", value); }

            //get
            //{
            //    return fKOttimizzazioneOid;
            //}
            //set
            //{
            //    SetPropertyValue<int>("KOttimizzazioneOid", ref fKOttimizzazioneOid, value);
            //}
        }

        private string fKOttimizzazioneDesc;
        [Persistent("KOTTIMIZZAZIONEDESC"),
        Size(50),
        DisplayName("Coeff. Ottimizzazione Descrizione")]
        [DbType("varchar(50)")]
        [ExplicitLoading()]
        [Delayed(true)]
        public string KOttimizzazioneDesc
        {
            get { return GetDelayedPropertyValue<string>("KOttimizzazioneDesc"); }
            set { SetDelayedPropertyValue<string>("KOttimizzazioneDesc", value); }

            //get
            //{
            //    return fKOttimizzazioneDesc;
            //}
            //set
            //{
            //    SetPropertyValue<string>("KOttimizzazioneDesc", ref fKOttimizzazioneDesc, value);
            //}
        }

        private double fKOttimizzazioneValore;
        [Persistent("OTTIMIZZAZIONEVALORE"),
        DisplayName("Coeff. Ottimizzazione Valore")]
        [ExplicitLoading()]
        [Delayed(true)]
        public double KOttimizzazioneValore
        {
            get { return GetDelayedPropertyValue<double>("KOttimizzazioneValore"); }
            set { SetDelayedPropertyValue<double>("KOttimizzazioneValore", value); }

            //get
            //{
            //    return fKOttimizzazioneValore;
            //}
            //set
            //{
            //    SetPropertyValue<double>("KOttimizzazioneValore", ref fKOttimizzazioneValore, value);
            //}
        }


        private string f_Utente;
        [Persistent("UTENTE"),
        Size(100),
        DisplayName("Utente")]
        // Appearance("Apparato.Utente", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        [DbType("varchar(100)")]
  
        [Delayed(true)]
        public string Utente
        {
            get { return GetDelayedPropertyValue<string>("Utente"); }
            set { SetDelayedPropertyValue<string>("Utente", value); }

            //get
            //{
            //    return f_Utente;
            //}
            //set
            //{
            //    SetPropertyValue<string>("Utente", ref f_Utente, value);
            //}
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt"),
         System.ComponentModel.Browsable(false)]
      
        [Delayed(true)]
        public DateTime DataAggiornamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAggiornamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataAggiornamento", value); }

            //get
            //{
            //    return fDataAggiornamento;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value);
            //}
        }

        private string fCondizione;
        [NonPersistent,
        DisplayName("Coeff. Condizione")]
        [ExplicitLoading()]
        public string Condizione
        {
            get
            {
                object tempDescrizione = null;

                var sDescrizione = string.Empty;
                var sValore = string.Empty;

                tempDescrizione = Evaluate("KCondizioneDesc");

                if (Evaluate("KCondizioneDesc") != null)
                {
                    sDescrizione = ((string)Evaluate("KCondizioneDesc"));
                }
                if (Evaluate("KCondizioneValore") != null)
                {
                    sValore = ((double)Evaluate("KCondizioneValore")).ToString();
                }
                fCondizione = string.Format("{0}({1})", sDescrizione, sValore);

                return fCondizione;
            }
        }

        private string fDimensione;
        [NonPersistent,
        DisplayName("Coeff. Dimensione")]
        [ExplicitLoading()]
        public string Dimensione
        {
            get
            {
                object tempDescrizione = null;

                var sDescrizione = string.Empty;
                var sValore = string.Empty;



                tempDescrizione = Evaluate("KDimensioneDesc");

                if (Evaluate("KDimensioneDesc") != null)
                {
                    sDescrizione = ((string)Evaluate("KDimensioneDesc"));
                }
                if (Evaluate("KDimensioneValore") != null)
                {
                    sValore = ((double)Evaluate("KDimensioneValore")).ToString();
                }
                fDimensione = string.Format("{0}({1})", sDescrizione, sValore);

                return fDimensione;
            }
        }

        private string fGuasto;
        [NonPersistent,
        DisplayName("Coeff. Guasto")]
        public string Guasto
        {
            get
            {
                object tempDescrizione = null;

                var sDescrizione = string.Empty;
                var sValore = string.Empty;



                tempDescrizione = Evaluate("KGuastoDesc");

                if (Evaluate("KGuastoDesc") != null)
                {
                    sDescrizione = ((string)Evaluate("KGuastoDesc"));
                }
                if (Evaluate("KGuastoValore") != null)
                {
                    sValore = ((double)Evaluate("KGuastoValore")).ToString();
                }
                fGuasto = string.Format("{0}({1})", sDescrizione, sValore);

                return fGuasto;
            }
        }

        private string fUbicazione;
        [NonPersistent,
        DisplayName("Coeff. Ubicazione")]
        public string Ubicazione
        {
            get
            {
                object tempDescrizione = null;

                var sDescrizione = string.Empty;
                var sValore = string.Empty;



                tempDescrizione = Evaluate("KUbicazioneDesc");

                if (Evaluate("KUbicazioneDesc") != null)
                {
                    sDescrizione = ((string)Evaluate("KUbicazioneDesc"));
                }
                if (Evaluate("KUbicazioneValore") != null)
                {
                    sValore = ((double)Evaluate("KUbicazioneValore")).ToString();
                }
                fUbicazione = string.Format("{0}({1})", sDescrizione, sValore);

                return fUbicazione;
            }
        }

        private string fUtenza;
        [NonPersistent,
        DisplayName("Coeff. Utenza")]
        public string Utenza
        {
            get
            {
                object tempDescrizione = null;

                var sDescrizione = string.Empty;
                var sValore = string.Empty;



                tempDescrizione = Evaluate("KUtenzaDesc");

                if (Evaluate("KUtenzaDesc") != null)
                {
                    sDescrizione = ((string)Evaluate("KUtenzaDesc"));
                }
                if (Evaluate("KUtenzaValore") != null)
                {
                    sValore = ((double)Evaluate("KUtenzaValore")).ToString();
                }
                fUtenza = string.Format("{0}({1})", sDescrizione, sValore);

                return fUtenza;
            }
        }

        private string fOttimizzazione;
        [NonPersistent,
        DisplayName("Coeff. Ottimizzazione")]
        public string Ottimizzazione
        {
            get
            {
                object tempDescrizione = null;

                var sDescrizione = string.Empty;
                var sValore = string.Empty;

                tempDescrizione = Evaluate("KOttimizzazioneDesc");

                if (Evaluate("KOttimizzazioneDesc") != null)
                {
                    sDescrizione = ((string)Evaluate("KOttimizzazioneDesc"));
                }
                if (Evaluate("KOttimizzazioneValore") != null)
                {
                    sValore = ((double)Evaluate("KOttimizzazioneValore")).ToString();
                }
                fOttimizzazione = string.Format("{0}({1})", sDescrizione, sValore);

                return fOttimizzazione;
            }
        }


        Asset fApparato;
        [NonPersistent(), DisplayName("Apparato")]
        [Appearance("ApparatokTempo.Apparato", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Show)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Asset Apparato
        {
            get { return GetDelayedPropertyValue<Asset>("Apparato"); }
            set { SetDelayedPropertyValue<Asset>("Apparato", value); }

            //get { return fApparato; }
            //set { SetPropertyValue<Apparato>("Apparato", ref fApparato, value); }
        }

        [PersistentAlias("(1 + ( (KUtenzaValore - 1) +  (KGuastoValore - 1) + (KDimensioneValore - 1) + (KCondizioneValore - 1) + (KUbicazioneValore - 1) ))")]
        [Appearance("ApparatokTempo.BackColor.Red", BackColor = "Red", FontColor = "Black", Priority = 1)]
        [DisplayName("Coefficiente Totale")]
        public double CoefficienteTotale
        {
            get
            {
                var tempObject = EvaluateAlias("CoefficienteTotale");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        public override string ToString()
        {
            return string.Format("kDm({0}),kCd({1}),kUb({2}),kGu({3}),kUt({4})", KDimensioneValore, KCondizioneValore
                                                                    , KUbicazioneValore, KGuastoValore, KUtenzaValore);
        }
    }
}
