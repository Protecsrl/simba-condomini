using System;
using CAMS.Module.Classi;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions,   Persistent("SCHEDEMPPASSI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Passi di Attivita MP")]
    [NavigationItem(false)]
    public class SchedaMpPassi : XPObject
    {
        public SchedaMpPassi()
            : base()
        {
            
        }

        public SchedaMpPassi(Session session)
            : base(session)
        {
            
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

        private SchedaMp fSchedaMp;
        [Association(@"SchedeMp_SchedeMpPassi"), Persistent("SCHEDEMP"), DisplayName(@"Procedura Attività MP")]
        [ExplicitLoading()]
        public SchedaMp SchedaMp
        {
            get
            {
                return fSchedaMp;
            }
            set
            {
                SetPropertyValue<SchedaMp>("SchedaMp", ref fSchedaMp, value);
            }
        }

        //private string fNumOrdine;
        //[Persistent("NUMEROORDINE"), Size(100), DevExpress.Xpo.DisplayName("Numero Ordine")]
        //[DbType("varchar(100)")]
        //public string NumOrdine
        //{
        //    get
        //    {
        //        return fNumOrdine;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("NumOrdine", ref fNumOrdine, value);
        //    }
        //}

        private int fNOrdine;
        [Persistent("NORDINE"), Size(10), DevExpress.Xpo.DisplayName("Nr Ordine")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int NOrdine
        {
            get
            {
                return fNOrdine;
            }
            set
            {
                SetPropertyValue<int>("NOrdine", ref fNOrdine, value);
            }
        }

        private string fSchedeMpPassi;
        [Persistent("PASSOSCHEDAMP"), Size(4000), DevExpress.Xpo.DisplayName("Passo Attività MP")]
        [DbType("varchar(4000)")]
        public string SchedeMpPassi
        {
            get
            {
                return fSchedeMpPassi;
            }
            set
            {
                SetPropertyValue<string>("SchedeMpPassi", ref fSchedeMpPassi, value);
            }
        }

        private StatoComponente fStatoComponente;
        [Persistent("STATOCOMPONENTE"), DisplayName("da Eseguire")]
        public StatoComponente StatoComponente
        {
            get
            {
                return fStatoComponente;
            }
            set
            {
                SetPropertyValue<StatoComponente>("StatoComponente", ref fStatoComponente, value);
            }
        }

        public override string ToString()
        {
            return string.Format("Passo.({0}-{1})", SchedaMp,this.NOrdine.ToString());
        }
    }

}