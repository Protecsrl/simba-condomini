using System;
using DevExpress.ExpressApp;
using System.Collections.Generic;



namespace CAMS.Module.Controllers.DBHelp
{
    /// <summary>
    /// Questo è un WindowController base che gestisce gli eventi della classe ShowNavigationItemController
    /// per visualizzare un modulo personalizzato quando un elemento di navigazione personalizzata si fa 
    /// clic(http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppSystemModuleShowNavigationItemControllertopic).
    /// </summary>
    public abstract class ShowCustomFormWindowController : WindowController
    {
        //private ShowNavigationItemController navigationController;
        public ShowCustomFormWindowController()
        {
            TargetWindowType = WindowType.Main;
        }
        //protected override void OnActivated()
        //{
        //    base.OnActivated();
        //    //navigationController = Frame.GetController<ShowNavigationItemController>();
        //    //if (navigationController != null)
        //    //{
        //    //    navigationController.CustomShowNavigationItem += navigationController_CustomShowNavigationItem;
        //    //}
        //}
        //protected override void OnDeactivated()
        //{
        //    //try
        //    //{
        //    //    if (navigationController != null)
        //    //    {
        //    //        //navigationController.CustomShowNavigationItem -= navigationController_CustomShowNavigationItem;
        //    //    }
        //    //}
        //    //catch { }

        
        //    base.OnDeactivated();
        //}
       
        
        //private void navigationController_CustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e)
        //{
        //    if (e.ActionArguments.SelectedChoiceActionItem.Id == "CustomForm")
        //    {
               
        //        ShowCustomForm(e.ActionArguments.SelectedChoiceActionItem.Model as IModelNavigationItem);
        //        e.Handled = true;
        //    }
            
        //}
        //protected abstract void ShowCustomForm(IModelNavigationItem model);
    }
}
