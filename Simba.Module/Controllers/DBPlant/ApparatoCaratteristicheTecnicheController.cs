using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
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
using DevExpress.Xpo;

namespace CAMS.Module.Controllers.DBPlant
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ApparatoCaratteristicheTecnicheController : ViewController
    {
        public ApparatoCaratteristicheTecnicheController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            bool VisualizzaTasto = false;
            using (UtilController uc = new UtilController())
            {
                VisualizzaTasto = uc.GetIsGrantedCreate(xpObjectSpace, "ApparatoCaratteristicheTecniche", "C");
            }

            pupStoricizzaCaratt_ListView.Active.SetItemValue("Active", VisualizzaTasto);

         
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            if (View is ListView)
            {
                //bool VisualizzaTasto = false;
                //ApparatoCaratteristicheTecniche AppCTecs = ((ListView)View).CurrentObject as ApparatoCaratteristicheTecniche;
                //int nrSelezionato = (((ListView)View).Editor).GetSelectedObjects().Count;
                //if (nrSelezionato > 0)
                //    VisualizzaTasto = true;
                //pupStoricizzaCaratt_ListView.Active.SetItemValue("Active", VisualizzaTasto);
            }

        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }


        private void pupStoricizzaCaratt_ListView_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {

        }

        private void pupStoricizzaCaratt_ListView_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            AsettCaratteristicheTecniche AppCTecs = ((ListView)View).CurrentObject as AsettCaratteristicheTecniche;
            IObjectSpace xpObjectSpace = View.ObjectSpace;
            if (xpObjectSpace != null && AppCTecs != null)
            {
                var dvParent = (DetailView)(View.ObjectSpace).Owner;
                if (dvParent.Id.Contains("Apparato_DetailView"))// && dvParent.ViewEditMode == ViewEditMode.Edit)
                {
                    //ApparatoCaratteristicheTecniche curCarTecStorico =
                    //    xpObjectSpace.GetObjectByKey<ApparatoCaratteristicheTecniche>(((XPObject)
                    //    dvParent.CurrentObject).Oid);

                    Asset apparato = AppCTecs.Asset;
                    AsettCaratteristicheTecniche newObj = xpObjectSpace.CreateObject<AsettCaratteristicheTecniche>();
                    newObj.Asset = apparato;
                    newObj.StdApparatoCaratteristicheTecniche = AppCTecs.StdApparatoCaratteristicheTecniche;
                    newObj.DataAggiornamento = DateTime.Now;
                    newObj.AppCaratteristicaValoreSelezione = AppCTecs.AppCaratteristicaValoreSelezione;
                    newObj.UserName = SecuritySystem.CurrentUserName;
                    newObj.ParentObject = AppCTecs;
                    newObj.Save();

                    //curCarTecStorico.NestedObjects.Add(new ApparatoCaratteristicheTecniche {
                    //Apparato= apparato,
                    //UserName = SecuritySystem.CurrentUserName,
                    //StdApparatoCaratteristicheTecniche = curCarTecStorico.StdApparatoCaratteristicheTecniche
                    //});

                    var view = Application.CreateDetailView(xpObjectSpace,
                        "ApparatoCaratteristicheTecniche_DetailView_Storicizza",
                        false, newObj);

                    view.Caption = string.Format("Storicizza Caratteristica Tecnica");
                    //view.ViewEditMode = ViewEditMode.Edit;
                    //e.View = view;
                    e.DialogController.SaveOnAccept = false;

                    view.ViewEditMode = ViewEditMode.Edit;
                    e.View = view;
                }
            }
            else
            {
                IObjectSpace xpObjSpace = Application.CreateObjectSpace();
                var Mess = xpObjSpace.CreateObject<MessaggioPopUp>();
                Mess.Messaggio = "Selezionare una Caratteristica Tercnica prima di Storicizzare";
                var view = Application.CreateDetailView(xpObjSpace, "MessaggioPopUp_DetailView", true, Mess);
                view.ViewEditMode = ViewEditMode.View;
                view.Caption = view.Caption + " Messaggio di AVVISO";
                e.View = view;        
                //SetMessaggioWebAutoAssegnazione(
                //    "selezionare una caratteristica prima!", "titolo", InformationType.Warning);

            }
        }
        private void SetMessaggioWebAutoAssegnazione(string Messaggio, string Titolo, InformationType InformationTypeMsg = InformationType.Info)
        {
            MessageOptions options = new MessageOptions();
            options.Duration = 7000;
            options.Message = Messaggio.ToString();
            options.Web.Position = InformationPosition.Top;
            options.Type = InformationTypeMsg;//            InformationType.Info;
            options.Win.Caption = Titolo;             //options.CancelDelegate = CancelDelegate;              //options.OkDelegate = OkDelegate;
            //options.OkDelegate = () =>
            //{

            //};
            Application.ShowViewStrategy.ShowMessage(options);
        }
    }
}
