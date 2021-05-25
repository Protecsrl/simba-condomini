using CAMS.Module.Classi;
using CAMS.Module.DBPlant.Vista;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask.AppsCAMS;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CAMS.Module.Controllers.DBTask.AppsCAMS
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class RegNotificheEmergenzeController : ViewController
    {
        private IObjectSpace xpObjectSpace;
        public RegNotificheEmergenzeController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
         
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            RegNotificaEmergAssegnaTeams.Active["AttivaRegNotificaEmergAssegnaTeams"] = false;
            if (View is DetailView)
            {
                if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                {
                    if (View.Id == "RegNotificheEmergenze_DetailView")
                    {
                        RegNotificaEmergAssegnaTeams.Active["AttivaRegNotificaEmergAssegnaTeams"] = true;
                        

                    }
                }
            }
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        

        private void RdLAssegnaTeam_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //String DetailViewId = "RegNotificheEmergenze_DetailView_PopUp";

            xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                RegNotificheEmergenze reg = (RegNotificheEmergenze)((DetailView)View).CurrentObject;
 
                string listViewId = "RisorseDistanzeRdL_ListView";
                var RisorseDistanzeRdLs = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(RisorseDistanzeRdL), listViewId);
     
                var view = Application.CreateListView(listViewId, RisorseDistanzeRdLs, false);
                e.View = view;
                e.IsSizeable = true;
                //e.IsSizeable = true;
            }
        }
    

        private void RdLAssegnaTeam_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var xpObjectSpace = View is DevExpress.ExpressApp.ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            var lstRisorseSelezionate = ((List<RisorseDistanzeRdL>)((((DevExpress.ExpressApp.Frame)(e.PopupWindow)).View).SelectedObjects.Cast<RisorseDistanzeRdL>().ToList<RisorseDistanzeRdL>()));
           
            RegNotificheEmergenze reg = (RegNotificheEmergenze)(View.CurrentObject);
            if (View.ObjectSpace != null)
            {
                #region Get RisorseTeam dalla maschera di popup
                foreach (RisorseDistanzeRdL r in lstRisorseSelezionate)
                {
                    reg.NotificheEmergenzes.Add(new NotificheEmergenze(reg.Session)
                    {
                        DataCreazione = DateTime.Now,
                        Team = reg.Session.GetObjectByKey<RisorseTeam>( r.RisorsaTeam.Oid),
                        CodiceNotifica = Guid.NewGuid(),
                        StatoNotifica = StatiNotificaEmergenza.NonVisualizzato,
                        DataAggiornamento = DateTime.Now
                    });  //DB db = new DB();//db.InserisciNotificaEmergenza(r.RegNotificheEmergenze.Oid, r.RisorsaTeam.Oid, r.RisorsaCapo.Oid);
                }
                reg.Save();
                View.ObjectSpace.CommitChanges();
                #endregion
            }
            /////////////////
            //xpObjectSpace = Application.CreateObjectSpace();
            //if (xpObjectSpace != null)
            //{
            //    #region Get RisorseTeam dalla maschera di popup
            //    List<RisorseDistanzeRdL> lstRisorseTeam = null;

            //    foreach (PropertyEditor editor in ((DevExpress.ExpressApp.ListView)e.PopupWindow.View).SelectedObjects<PropertyEditor>())
            //    {
                    
            //        if (editor.GetType() == typeof(ListPropertyEditor) 	&&	(editor.MemberInfo).Name !=	"NotificheEmergenzes"	)
            //        {
                        
            //        // System.Collections.IList<RisorseDistanzeRdL>
            //             var ListaSel =   
            //             ((DevExpress.ExpressApp.Web.NestedFrameControlBase)(editor.Control)).View.SelectedObjects;

            //             if (ListaSel == null || ListaSel.Count == 0)
            //                 throw new UserFriendlyException("Occorre selezionare almeno una risorsa team");
            //            //qui li lavori
            //             foreach (Object o in ListaSel)
            //             {
            //                 RisorseDistanzeRdL r = (RisorseDistanzeRdL)o;
            //                 DB db = new DB();
            //                 db.InserisciNotificaEmergenza(r.RegNotificheEmergenze.Oid,r.RisorsaTeam.Oid,r.RisorsaCapo.Oid);
            //             }

            //        }
            //    }              
            //   // RisorseDistanzeRdL RisorseTeamSelezionata = lstRisorseTeam[0];
            //    #endregion
            //}
            
        }

        private void RefreshDati()
        {
            try
            {
                if (View is DetailView)
                {
                    View.ObjectSpace.ReloadObject(View.CurrentObject);
                }
                else
                {
                    (View as DevExpress.ExpressApp.ListView).CollectionSource.Reload();
                }
                View.ObjectSpace.Refresh();
                View.Refresh();
            }
            catch
            {
            }
            base.Frame.View.Refresh();
        }
        private RisorseDistanzeRdL NuovaListaRisorseDistanza ;

    
    }
}
