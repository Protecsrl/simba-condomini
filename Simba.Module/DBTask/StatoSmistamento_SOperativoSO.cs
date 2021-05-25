using DevExpress.Persistent.Base;
using DevExpress.Xpo;
namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("SSMISTAMENTO_SOPERATIVO_SO")]
    [VisibleInDashboards(false)]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Stato Smistamento x SO")]
    [System.ComponentModel.DefaultProperty("StatoOperativoSO.CodStato")]
    [ImageName("Demo_ListEditors_Scheduler_Filter")]
    [NavigationItem(false)]
    public class StatoSmistamento_SOperativoSO : XPObject
    {
        public StatoSmistamento_SOperativoSO()            : base()
        {
        }
        public StatoSmistamento_SOperativoSO(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private StatoSmistamento fStatoSmistamento;
        [Association(@"StatoSmistamento_rel_SOperativoSO"), Persistent("STATOSMISTAMENTO"), DisplayName("Stato Smistamento")]
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

        private StatoOperativo fStatoOperativoSO;
        [Persistent("STATOOPERATIVO"),DisplayName("Stato Operativo")]
        public StatoOperativo StatoOperativoSO
        {
            get
            {
                return fStatoOperativoSO;
            }
            set
            {
                SetPropertyValue<StatoOperativo>("StatoOperativoSO", ref fStatoOperativoSO, value);
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

