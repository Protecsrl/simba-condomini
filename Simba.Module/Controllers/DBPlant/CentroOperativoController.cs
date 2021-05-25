using CAMS.Module.DBPlant;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAMS.Module.Controllers.DBPlant
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class CentroOperativoController : ViewController
    {

        bool TastoacSincronizzaUSLSUSLGWrite { get; set; }
        public CentroOperativoController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            TastoacSincronizzaUSLSUSLGWrite = false;
             using (CAMS.Module.Classi.UtilController uc = new CAMS.Module.Classi.UtilController())
            {
                IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                TastoacSincronizzaUSLSUSLGWrite = uc.GetIsGrantedCreate(xpObjectSpace,"CentroOperativo", "W");  
            }
             acSincronizzaUSLSUSLG.Active.SetItemValue("Active", TastoacSincronizzaUSLSUSLGWrite);    
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

        private void acSincronizzaUSLSUSLG_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //    [Action(Caption = "Sincronizza USLS,USLG",
            //ToolTip = "Sincronizza le Unità Standard Lavorative in relazione all'Area di Polo Aggiornata",
            //ConfirmationMessage = "vuoi aggiornare i dati?")]
            //public void UpdateUSLbyAreadiPolo()
            //{
            if (View is DetailView)
            {
                if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                {
                    DetailView Dv = View as DetailView;
                    CentroOperativo co = Dv.CurrentObject as CentroOperativo;
                    if (co.AreaDiPolo != null)
                    {
                        co.SetMemberValue("USLG", (Double)co.AreaDiPolo.USLG);
                        co.SetMemberValue("USLS", (Double)co.AreaDiPolo.USLS);
                    }
                    else
                    {
                        co.SetMemberValue("USLG", 0);
                        co.SetMemberValue("USLS", 0);
                    }
                }
            }
            if (View is ListView)
            {
                List<CentroOperativo> lco = View.ObjectSpace.GetObject((((ListView)View).Editor).GetSelectedObjects().Cast<CentroOperativo>().ToList());
                foreach (CentroOperativo co in lco)
                {
                    if (co.AreaDiPolo != null)
                    {
                        co.SetMemberValue("USLG", (Double)co.AreaDiPolo.USLG);
                        co.SetMemberValue("USLS", (Double)co.AreaDiPolo.USLS);
                    }
                    else
                    {
                        co.SetMemberValue("USLG", 0);
                        co.SetMemberValue("USLS", 0);
                    }
                }

            }


            //}
        }
    }
}
