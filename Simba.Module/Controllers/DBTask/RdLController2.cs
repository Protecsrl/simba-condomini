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
using CAMS.Module.DBTask;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using CAMS.Module.DBTask.DC;
using CAMS.Module.DBTask.Vista;
using System.ComponentModel;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.BaseImpl;
using CAMS.Module.Classi;


namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RdLController2 : ViewController
    {
        private SimpleAction DCReport;
        public string Rdlcodici;
        public RdLController2()
        {
            TargetViewType = ViewType.DetailView;
            TargetObjectType = typeof(RdL);
            // Target required Views (via the TargetXXX properties) and create their Actions.
            DCReport = new SimpleAction(this, "DCReport", PredefinedCategory.Edit);
            DCReport.ImageName = "State_Task_Completed";
            DCReport.ToolTip = "DCReport";
            DCReport.Execute += DCReport_Execute;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            //Application.ObjectSpaceCreated += Application_ObjectSpaceCreated;
            //DCReport.Active.SetItemValue("Active", VisualizzaTasto);
            //if (View.Id == "RdL_DetailView_Gestione")
            //{
            //}

            //if (View.Id.Contains("RdLListView_ListView"))
            //{
            //}

        }
    
        protected override void OnDeactivated()
        {
            Application.ObjectSpaceCreated -= Application_ObjectSpaceCreated;
            base.OnDeactivated();
        }
        private void Application_ObjectSpaceCreated(Object sender, ObjectSpaceCreatedEventArgs e)
        {
            if (e.ObjectSpace is NonPersistentObjectSpace)
            {
                ((NonPersistentObjectSpace)e.ObjectSpace).ObjectsGetting += DCRdLListReport_objectSpace_ObjectsGetting;
            }
        }

        private void DCReport_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Application.ObjectSpaceCreated += Application_ObjectSpaceCreated;
            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                RdL rdl = View.ObjectSpace.GetObject<RdL>((RdL)dv.CurrentObject);
                try
                {
                    OidRdL = rdl.Oid;
                    //https://documentation.devexpress.com/eXpressAppFramework/114516/Task-Based-Help/Business-Model-Design/Non-Persistent-Objects/How-to-Display-Non-Persistent-Objects-in-a-Report
                    //                                                ReportObjectSpaceProvider 
                    IObjectSpace objectSpaceRpt = ReportDataProvider.ReportObjectSpaceProvider.CreateObjectSpace(typeof(ReportDataV2));

                    IReportDataV2 reportData = objectSpaceRpt.FindObject<ReportDataV2>(
                        CriteriaOperator.Parse("[DisplayName] = 'Report RdL di Manutenzione'")); //' Report Interventi di Manutenzione --> ex <<sub Report Interventi Manutenzione>>

                    string handle = ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);
                    Frame.GetController<DevExpress.ExpressApp.ReportsV2.ReportServiceController>().ShowPreview(handle);
                }
                catch
                { }
            }
            //Application.ObjectSpaceCreated -= Application_ObjectSpaceCreated;
        }

        int OidRdL = 0;

         
        void DCRdLListReport_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            //string Rdlcodici;
            if (e.ObjectType.FullName == "CAMS.Module.DBTask.DC.DCRdLListReport")
            {
                //Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    foreach (XPObject cur in View.SelectedObjects)
                    {
                        Rdlcodici = string.Concat(Rdlcodici, cur.Oid.ToString() + ",");
                    }

                    BindingList<DCRdLListReport> objects = new BindingList<DCRdLListReport>();
                    using (DB db = new DB())
                    {
                        objects = db.GetReport_RdL(Rdlcodici.ToString());
                    }  
                    
                    e.Objects = objects;
                }                
            }
          


        }
    }
}
