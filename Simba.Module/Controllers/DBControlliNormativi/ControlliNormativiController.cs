using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;

namespace CAMS.Module.Controllers.DBControlliNormativi
{
    public partial class ControlliNormativiController : ViewController
    {
        public ControlliNormativiController()
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
        }
    }
}
