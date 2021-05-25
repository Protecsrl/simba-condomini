using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("SSMISTAMENTO_COMBO")]
    [VisibleInDashboards(false)]
    //[System.ComponentModel.DefaultProperty("StatoSmistamentoxCombo.SSmistamento")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Stato Smistamento Combo")]
    [ImageName("Demo_ListEditors_Scheduler_Filter")]
    [NavigationItem(false)]
    public class StatoSmistamentoCombo : XPObject
    {
        public StatoSmistamentoCombo()            : base()
        {
        }
        public StatoSmistamentoCombo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private StatoSmistamento fStatoSmistamento;
        [Association(@"StatoSmistamento_Combo"), Persistent("STATOSMISTAMENTO"), DisplayName("Stato Smistamento")]
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

        private StatoSmistamento fStatoSmistamentoxCombo;
        [Persistent("STATOSMISTAMENTOCOMBO"), DisplayName("Stato Smistamento Combo")]
        public StatoSmistamento StatoSmistamentoxCombo
        {
            get
            {
                //fStatoSmistamentoxCombo.SSmistamento
                return fStatoSmistamentoxCombo;
            }
            set
            {
                SetPropertyValue<StatoSmistamento>("StatoSmistamentoxCombo", ref fStatoSmistamentoxCombo, value);
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


        public override string ToString()
        {
            if (this.Oid == -1) return null;
            if (this.StatoSmistamentoxCombo == null) return null;
 
                return string.Format("{0}", StatoSmistamentoxCombo.SSmistamento);
            
        }
       
    }
}

