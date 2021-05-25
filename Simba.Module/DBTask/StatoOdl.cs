using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
    Persistent("STATOODL")]
    [VisibleInDashboards(false)]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Stato Ordini di Lavoro")]
    [ImageName("ShowTestReport")]
    [NavigationItem(false)]
    public class StatoOdl : XPObject
    {
        public StatoOdl()
            : base()
        {
        }
        public StatoOdl(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private string fDescrizione;
        [Size(1000),
        Persistent("DESCRIZIONE"),
        DisplayName("Descrizione")]
        [DbType("varchar(1000)")]
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

        [Association(@"ODLRefSTATOODL", typeof(OdL)),
        DisplayName("Ordine di Lavoro")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<OdL> ODLs
        {
            get
            {
                return GetCollection<OdL>("ODLs");
            }
        }
    }
}
