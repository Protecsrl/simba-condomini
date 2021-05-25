
using CAMS.Module.DBAngrafica;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
namespace CAMS.Module.DBAux
{

       [DefaultClassOptions, Persistent("GRUPPOMESI_MESI")]
    //[ System.ComponentModel.DefaultProperty("Descrizione")]        
    //[NavigationItem(false)]    
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Mesi dei Gruppi")]
    [ImageName("ShowTestReport")]
    [VisibleInDashboards(false)]
    public class GruppoMesiMesi : XPObject

    {
     public GruppoMesiMesi()
            : base()
        {
        }

        public GruppoMesiMesi(Session session)
            : base(session)
        {
        }


        //GruppoMesi_Mesi
        private GruppoMesi fGruppoMesi;
        [Persistent("GRUPPOMESI"), DevExpress.Xpo.DisplayName("Gruppo Mesi")]
        [Association(@"GruppoMesi_Mesi")]
        //[Association(@"GruppoMesi_Mesi", typeof(GruppoMesiMesi)), DevExpress.Xpo.Aggregated]
        //[DbType("varchar(7)")]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading]
        public GruppoMesi GruppoMesi
        {
            get
            {
                return fGruppoMesi;
            }
            set
            {
                SetPropertyValue<GruppoMesi>("GruppoMesi", ref fGruppoMesi, value);
            }
        }

        private Mesi fMesi;
        [Persistent("MESI"), DevExpress.Xpo.DisplayName("Mesi")]
        //[Association(@"GruppoMesi_Mesi")]
        //[DbType("varchar(7)")]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading]
        public Mesi Mesi
        {
            get
            {
                return fMesi;
            }
            set
            {
                SetPropertyValue<Mesi>("Mesi", ref fMesi, value);
            }
        }  


    }
}



