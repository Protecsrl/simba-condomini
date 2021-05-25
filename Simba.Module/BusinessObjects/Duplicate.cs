using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAMS.Module.BusinessObjects
{   
    [DomainComponent]
    [NavigationItem(false)]
    public class Duplicate
    {
        //[Browsable(false), Key]
        public int Id;
        public string Title { get; set; }
        public int Count { get; set; }
    }
}
