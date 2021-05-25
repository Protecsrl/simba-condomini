using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("SSMISTAMENTO_SOPERATIVO")]
    [VisibleInDashboards(false)]
    [System.ComponentModel.DefaultProperty("StatoOperativo")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Stato Smistamento S.Operativo")]
    [ImageName("Demo_ListEditors_Scheduler_Filter")]
    [NavigationItem(false)]
    public class StatoSmistamento_SOperativo : XPObject
    {
        public StatoSmistamento_SOperativo()            : base()
        {
        }
        public StatoSmistamento_SOperativo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private StatoSmistamento fStatoSmistamento;
        [Association(@"StatoSmistamento_rel_SOperativo"), Persistent("STATOSMISTAMENTO"), DisplayName("Stato Smistamento")]
      //  [VisibleInDetailView(false),VisibleInListView(false),VisibleInLookupListView(false)]
        public StatoSmistamento StatoSmistamento
        {
            get
            {
                return fStatoSmistamento;
            }
            set
            {
                SetPropertyValue<StatoSmistamento>("StatoSmistamento", ref fStatoSmistamento, value);
            }
        }

        private StatoOperativo fStatoOperativo;
        [Persistent("STATOOPERATIVO"),DisplayName("Stato Operativo")]
        public StatoOperativo StatoOperativo
        {
            get
            {
                return fStatoOperativo;
            }
            set
            {
                SetPropertyValue<StatoOperativo>("StatoOperativo", ref fStatoOperativo, value);
            }
        }

        

        private int fOrdine;
        [Persistent("ORDINE"), DisplayName("Ordine"), MemberDesignTimeVisibility(false)]
        public int Ordine
        {
            get
            {
                return fOrdine;
            }
            set
            {
                SetPropertyValue<int>("Ordine", ref fOrdine, value);
            }
        }
       
    }
}

