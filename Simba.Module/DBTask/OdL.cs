using System;
using DevExpress.Xpo;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using CAMS.Module.DBPlant;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
    Persistent("ODL")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Ordini di Lavoro")]
    [ImageName("ProjectFile")]
    [NavigationItem(false)]

    public class OdL : XPObject
    {
        public OdL()
            : base()
        {
        }
        public OdL(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            DataEmissione = DateTime.Now;
        }

        /// <summary>
        /// Crea RegistroRdL da una serie di lstRDLSelezionati
        /// </summary>
        /// <param name="xpObjectSpace"></param>
        /// <param name="lstRDLSelezionati"></param>
        /// <returns></returns>
        public static OdL CreateFrom(IObjectSpace xpObjectSpace, IEnumerable<RdL> lstRDLSelezionati)
        {
            var newOdL = xpObjectSpace.CreateObject<OdL>();
            var Session = newOdL.Session;
            var IDs = lstRDLSelezionati.Select(r => r.Oid).ToList();
            var lstRDL = Session.Query<RdL>().Where(r => IDs.Contains(r.Oid));
            //newOdL.oRdLs.AddRange(lstRDL);
            newOdL.RegistroRdL = ((RdL)lstRDL.First<RdL>()).RegistroRdL;
            newOdL.DataEmissione = DateTime.Now;
            newOdL.QuantitaRdlAperte = lstRDL.Count();
            var newStatoOdl = Session.GetObjectByKey<StatoOdl>(1);
            newOdL.StatoOdl = newStatoOdl;
            var newTipoOdl = Session.GetObjectByKey<TipoOdL>(5);
            newOdL.TipoOdL = newTipoOdl;
            var  nrTeam = ((RdL)lstRDL.First<RdL>()).RisorseTeam.AssRisorseTeam.Count;
            newOdL.TotaleRisorse = nrTeam;
            return newOdL;
        }


        /// <summary> 
        /// </summary>
        private string fDescrizione;
        [Size(4000),
        Persistent("DESCRIPTION"),
        DisplayName("Descrizione")]
        [DbType("varchar(4000)")]
        public string Descrizione
        {
            get
            {
                return fDescrizione;
            }
            set
            {
                SetPropertyValue<string>("Descrizione", ref fDescrizione, value);
            }
        }

        private decimal fQuantitaRdlAperte;
        [Persistent("QTYOPENRDL"),
        DisplayName("Quantità RdL Assegnate")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:N}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public decimal QuantitaRdlAperte
        {
            get
            {
                return fQuantitaRdlAperte;
            }
            set
            {
                SetPropertyValue<decimal>("QuantitaRdlAperte", ref fQuantitaRdlAperte, value);
            }
        }

        private StatoOdl fStatoOdl;
        [Association(@"ODLRefSTATOODL", typeof(OdL)),
        Persistent("STATOODL"),
        DisplayName("Stato OdL")]
        public StatoOdl StatoOdl
        {
            get
            {
                return fStatoOdl;
            }
            set
            {
                SetPropertyValue<StatoOdl>("StatoOdl", ref fStatoOdl, value);
            }
        }

       

        private TipoOdL fTipoOdL;
        [Association(@"ODLRefTIPOODL", typeof(OdL)),
        Persistent("TIPOODL"),
        DisplayName("Tipo OdL")]
        public TipoOdL TipoOdL
        {
            get
            {
                return fTipoOdL;
            }
            set
            {
                SetPropertyValue<TipoOdL>("TipoOdL", ref fTipoOdL, value);
            }
        }
        //[NonPersistent]
        //[System.ComponentModel.Browsable(false)]
        //public string TipoOdLDescrizione
        //{
        //    get
        //    {
        //        var Descrizione = String.Empty;
        //        if (TipoOdL != null)
        //        {
        //            Descrizione = TipoOdL.Descrizione;
        //        }
        //        return Descrizione;
        //    }
        //}

        private StatoOperativo fUltimoStatoOperativo;
        [Persistent("STATOOPERATIVO"),
        DisplayName("Ultimo Stato Operativo")]
        public StatoOperativo UltimoStatoOperativo
        {
            get
            {
                return fUltimoStatoOperativo;
            }
            set
            {
                SetPropertyValue<StatoOperativo>("UltimoStatoOperativo", ref fUltimoStatoOperativo, value);
            }
        }

        private DateTime fDataEmissione;
        [Persistent("DATAEMISSIONE"),
        DisplayName("Data Emissione"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        public DateTime DataEmissione
        {
            get
            {
                return fDataEmissione;
            }
            set
            {
                SetPropertyValue<DateTime>("DataEmissione", ref fDataEmissione, value);
            }
        }


        private DateTime fDataCompletamento;
        [Persistent("DATE_COMPLETED"),
        DisplayName("Data Completamento"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        public DateTime DataCompletamento
        {
            get
            {
                return fDataCompletamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataCompletamento", ref fDataCompletamento, value);
            }
        }

        private decimal fTotaleMinImpegnate;
        [Persistent("TOTMINIMPEGNATI"),
        DisplayName("Totale Minuti Impegnati")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:N}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public decimal TotaleMinImpegnate
        {
            get
            {
                return fTotaleMinImpegnate;
            }
            set
            {
                SetPropertyValue<decimal>("TotaleMinImpegnate", ref fTotaleMinImpegnate, value);
            }
        }

        private int fTotaleRisorse;
        [Persistent("TOTRISORSE"),
        DisplayName("Totale Risorse")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:N}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int TotaleRisorse
        {
            get
            {
                return fTotaleRisorse;
            }
            set
            {
                SetPropertyValue<int>("TotaleRisorse", ref fTotaleRisorse, value);
            }
        }


        private RegistroRdL fRegistroRdL;
        [Association(@"REGISTRORDLRefOdl"),
        Persistent("REGRDL"),
        DisplayName("Registro RdL")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public RegistroRdL RegistroRdL
        {
            get
            {
                return fRegistroRdL;
            }
            set
            {
                SetPropertyValue<RegistroRdL>("RegistroRdL", ref fRegistroRdL, value);
            }
        }



        //private Impianto fServizio;
        //[NonPersistent,
        //DisplayName("Impianto")]
        //public Impianto Impianto
        //{
        //    get
        //    {
        //        if (oRdLs.Count > 0)
        //        {
        //            var _Impianto = oRdLs[0].Impianto;
        //            if (_Impianto != null)
        //            {
        //                return (Impianto)_Impianto;
        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }
        //        return fServizio;
        //    }
        //}


        protected override void OnSaving()
        {
            base.OnSaving();
            //if (!IsDeleted)
            //{
            //    var CodImpianto = string.Empty;
            //    if (oRdLs.Count > 0)
            //    {
            //        CodImpianto = oRdLs[0].Impianto.Descrizione;
            //    }
            //    var NuovoNumRegRdl = 0;
            //    if (RegistroRdL != null)
            //    {
            //        NuovoNumRegRdl = RegistroRdL.Oid;
            //    }
            //    var NuovoNumOdl = Oid;
            //    if (Session.IsNewObject(this))
            //    {
            //        NuovoNumOdl = Convert.ToInt32(Session.Evaluate<OdL>(CriteriaOperator.Parse("Max(Oid)"), null)) + 1;
            //    }
            //    var DescrizioneRaggruppamentoRdl = string.Format("OdL TT({0}-{1}) Impianto {2}", NuovoNumRegRdl, NuovoNumOdl, CodImpianto);
            //    Descrizione = DescrizioneRaggruppamentoRdl;
            //}
        }
    }
}
