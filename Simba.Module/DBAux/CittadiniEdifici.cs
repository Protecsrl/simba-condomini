using CAMS.Module.DBPlant;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
//namespace CAMS.Module.DBAux
//{
//    class CittadiniEdifici
//    {
//    }
//}

namespace CAMS.Module.DBAux
{
    [DefaultClassOptions]
    [Persistent("CITTADINIEDIFICI")]
    [System.ComponentModel.DefaultProperty("Edifici")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Edifici Abilitati")]
    [ImageName("BO_Employee")]
    [NavigationItem(true)]
    [VisibleInDashboards(false)]
    public class CittadiniEdifici : XPObject
    {
        public CittadiniEdifici() : base() { }
        public CittadiniEdifici(Session session) : base(session) { }

        private Cittadini fCittadino;
        [Persistent("CITTADINI"), DisplayName("Cittadini")]
        [Association(@"Cittadini-CittadiniEdificis")]
        public Cittadini Cittadino
        {
            get { return fCittadino; }
            set { SetPropertyValue<Cittadini>("Cittadino", ref fCittadino, value); }
        }

        private Immobile fImmobile;
        [Persistent("IMMOBILE"), DisplayName("Immobile")]
        public Immobile Immobile
        {
            get { return fImmobile; }
            set { SetPropertyValue<Immobile>("Immobile", ref fImmobile, value); }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"), DisplayName("Data Aggiornamento")]
        public DateTime DataAggiornamento
        {
            get { return fDataAggiornamento; }
            set { SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value); }
        }
    }
}


