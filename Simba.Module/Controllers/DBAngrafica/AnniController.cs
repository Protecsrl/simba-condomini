using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;

namespace CAMS.Module.Controllers.DBAngrafica
{
    public partial class AnniController : ViewController
    {
        public AnniController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            string a="k";
        }
    }
}
