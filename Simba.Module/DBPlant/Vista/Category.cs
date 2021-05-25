using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
using System.ComponentModel;

namespace CAMS.Module.DBPlant.Vista
{
    //  https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument113263
    [NavigationItem(false)]
    [DefaultClassOptions, Persistent("V_CATEGORYTREE"), DevExpress.ExpressApp.Model.ModelDefault("Caption", "Category TREE")]
    [VisibleInDashboards(false)]
    [ImageName("ListBullets")]
    public abstract class Category : BaseObject, ITreeNode
    {
        private string name;
        protected abstract ITreeNode Parent { get; }
        protected abstract IBindingList Children { get; }
        public Category(Session session) : base(session) { }
        public string Name
        {
            get
            { return name; }
            set
            { SetPropertyValue("Name", ref name, value); }
        }
        #region ITreeNode
        IBindingList ITreeNode.Children
        {
            get
            { return Children; }
        }
        string ITreeNode.Name
        {
            get
            { return Name; }
        }
        ITreeNode ITreeNode.Parent
        {
            get
            { return Parent; }
        }
        #endregion
    }

}
