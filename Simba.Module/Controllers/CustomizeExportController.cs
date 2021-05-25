using System;
using DevExpress.ExpressApp;




using DevExpress.XtraPrinting;

namespace CAMS.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CustomizeExportController : ViewController
    {
        public CustomizeExportController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewType = ViewType.ListView;
        }

        //private ExportController exportController;

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            //exportController = Frame.GetController<ExportController>();
            //exportController.CustomGetDefaultFileName += exportController_CustomGetDefaultFileName;
            //exportController.CustomExport += new EventHandler<CustomExportEventArgs>(CustomExport);
        }


//        protected virtual void CustomExport(object sender, CustomExportEventArgs e)
//        {
//            //Customize Export Options
//            if (e.ExportTarget == ExportTarget.Xls)
//            {
//                XlsExportOptions options = (XlsExportOptions)e.ExportOptions;
//                if (options == null)
//                {
//                    options = new XlsExportOptions();
//                }
//                options.SheetName = View.Caption;
//                options.ShowGridLines = true;
//                e.ExportOptions = options;
//            }
//        }
//        void exportController_CustomGetDefaultFileName(object sender, CustomGetDefaultFileNameEventArgs e)
//        {
//#if EASYTEST
//             //Provide a custom file name
//         e.FileName = e.FileName + "_06.25.12";
//#else
//            //Provide a custom file name
//            e.FileName = string.Format("{0}_{1:MM.dd.yy}", e.FileName, DateTime.Now);
//#endif
//        }
      
       



        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
