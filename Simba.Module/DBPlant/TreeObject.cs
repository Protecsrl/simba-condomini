using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base.General;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, NonPersistent]
    [NavigationItem(false)]
    [VisibleInDashboards(false)]
    public abstract class TreeObject : XPLiteObject, ITreeNode
    {
        protected abstract ITreeNode Parent { get; }

        private string fOid;
        [Key, Persistent("OID"), MemberDesignTimeVisibility(false)]
        public string Oid
        {
            get
            {
                return fOid;
            }
            set
            {
                SetPropertyValue<string>("Oid", ref fOid, value);
            }
        }
        protected abstract IBindingList Children { get; }
        public TreeObject(Session session) : base(session) { }

        [Persistent("NAME"), DevExpress.Xpo.DisplayName("Descrizione")]
        public abstract string Name { get; set; }

        IBindingList ITreeNode.Children
        {
            get
            {
                return Children;
            }
        }
        string ITreeNode.Name
        {
            get
            {
                return Name;
            }
        }
        ITreeNode ITreeNode.Parent
        {
            get
            {
                return Parent;
            }
        }
    }
}
