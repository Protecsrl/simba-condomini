﻿using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
using System.ComponentModel;

namespace CAMS.Module.DBPlant.Vista
{
     [DevExpress.ExpressApp.Model.ModelDefault("Caption", "CategoryProjectGroup TREE")]
     [NavigationItem(false)]
    [VisibleInDashboards(false)]
    public class ProjectGroup : Category
    {
        protected override ITreeNode Parent
        {
            get
            {
                return null;
            }
        }
        protected override IBindingList Children
        {
            get
            {
                return Projects;
            }
        }
        public ProjectGroup(Session session) : base(session) { }
        public ProjectGroup(Session session, string name)
            : base(session)
        {
            this.Name = name;
        }
        [Association("ProjectGroup-Projects"), Aggregated]
        public XPCollection<Project> Projects
        {
            get
            {
                return GetCollection<Project>("Projects");
            }
        }
    }
}
