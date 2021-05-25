using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using CAMS.Module.DBPlanner;
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.ExpressApp' è già presente in questo spazio dei nomi
using DevExpress.ExpressApp;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.ExpressApp' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.ExpressApp.Actions' è già presente in questo spazio dei nomi
using DevExpress.ExpressApp.Actions;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.ExpressApp.Actions' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.ExpressApp.Editors' è già presente in questo spazio dei nomi
using DevExpress.ExpressApp.Editors;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.ExpressApp.Editors' è già presente in questo spazio dei nomi
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
using System.Linq;
#pragma warning restore CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi

namespace CAMS.Module.Controllers.DBPlanner
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class MPAttivitaPluriAnnualiController : ViewController
    {
        public MPAttivitaPluriAnnualiController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void SelDataPluriIniziale_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            if (xpObjectSpace != null)
            {


                if (View is ListView)
                {
                    if (View.Id == "RegPianificazioneMP_MPAttivitaPluriAnnualis_ListView")
                    {
                        ListView lv = View as ListView;
                        DetailView dv = (DetailView)View.ObjectSpace.Owner;
                        if (dv.ViewEditMode == ViewEditMode.Edit)
                        {
                            RegPianificazioneMP RegPianificazioneMP = (RegPianificazioneMP)dv.CurrentObject;
                            int anno = RegPianificazioneMP.AnnoSchedulazione.Anno;
                            var paramValue = (DateTime)e.ParameterCurrentValue;
                            //if (anno == paramValue.Year)
                            //{
                                var lstMPDataInizialeSel = (((ListView)View).Editor).GetSelectedObjects().Cast<MPAttivitaPluriAnnuali>().ToList<MPAttivitaPluriAnnuali>();
                                foreach (MPAttivitaPluriAnnuali DataIniziale in lstMPDataInizialeSel)
                                {
                                    MPAttivitaPluriAnnuali MPDataIni = View.ObjectSpace.GetObject<MPAttivitaPluriAnnuali>(DataIniziale);
                                    MPDataIni.Data = paramValue;
                                   
                                    MPDataIni.Save();
                                }

                            //}
                        }
                    }
                    View.ObjectSpace.CommitChanges();
                }
            }
        }
    }
}
