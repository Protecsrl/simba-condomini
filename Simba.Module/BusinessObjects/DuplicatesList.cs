using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CAMS.Module.BusinessObjects
{
     

    [DomainComponent]
    [NavigationItem(false)]
    public class DuplicatesList
    {
        private BindingList<Duplicate> duplicates;
        public DuplicatesList()
        {
            duplicates = new BindingList<Duplicate>();
        }
        public BindingList<Duplicate> Duplicates { get { return duplicates; } }
    }

}
