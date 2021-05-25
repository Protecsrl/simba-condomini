using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;

namespace CAMS.Module.DBAux
{
    [DomainComponent]
    [NavigationItem(false)]
    [VisibleInDashboards(false)]
    public class DCListProperty 
    {
        // [DevExpress.ExpressApp.Data.Key]  quando poassa a 16.1 @@@@@##############################àà
        // chiave        // prove
        //public  int Oid { get; set; }
        //public string x { get; set; }
        //public string y { get; set; }
        // ------------------------------
              [DevExpress.ExpressApp.Data.Key]
        public Guid Oid { get; set; }

        [XafDisplayName("Nome Campo")]
        [Index(1)]  
        public string PropertyName { get; set; }

    }




}
