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
using System.ComponentModel;
using CAMS.Module.DBAux;
using CAMS.Module.DBAngrafica;

namespace CAMS.Module.Controllers.DBAngrafica
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ReportExcelController : ViewController
    {
        public ReportExcelController()
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

        private void pupGetPropertyObjTypeLookUpRExcel_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (e.PopupWindowViewSelectedObjects.Count > 0)
            {
                string pn = ((DCListProperty)e.PopupWindowViewSelectedObjects[0]).PropertyName.ToString();
                ((ReportExcel)(View.CurrentObject)).SetMemberValue("CampoJoinLookUp", pn);           
            }
        }

        private void pupGetPropertyObjTypeRExcel_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            //DevExpress.Xpo.Session Sess = ((DevExpress.ExpressApp.Xpo.XPObjectSpace)xpObjectSpace).Session;
            //StringBuilder Messaggio = new StringBuilder("", 32000000);
            if (e.PopupWindowViewSelectedObjects.Count > 0)
            {
                string pn = ((DCListProperty)e.PopupWindowViewSelectedObjects[0]).PropertyName.ToString();
                ((ReportExcel)(View.CurrentObject)).SetMemberValue("CampoObjectType", pn);
            }
        }


        private void pupGetPropertyObjTypeLookUpRExcel_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            if (View is DetailView)
            {
                NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(DCListProperty));
                objectSpace.ObjectsGetting += DCListProperty_ObjLookUp_objectSpace_ObjectsGetting ;

                CollectionSource DCListProperty_Lookup = (CollectionSource)Application.CreateCollectionSource(objectSpace, typeof(DCListProperty),
                                                                            "DCListProperty_LookupListView");

                ListView lvk = Application.CreateListView("DCListProperty_LookupListView", DCListProperty_Lookup, true);

                var ParCriteria = string.Empty;

                var dc = Application.CreateController<DialogController>();
                e.DialogController.SaveOnAccept = false;
                e.View = lvk;
            }
        }


        private void pupGetPropertyObjTypeRExcel_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            if (View is DetailView)
            {
                NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(DCListProperty));
                objectSpace.ObjectsGetting += DCListProperty_objectSpace_ObjectsGetting;

                CollectionSource DCListProperty_Lookup = (CollectionSource)Application.CreateCollectionSource(objectSpace, typeof(DCListProperty),
                                                                            "DCListProperty_LookupListView");

                ListView lvk = Application.CreateListView("DCListProperty_LookupListView", DCListProperty_Lookup, true);

                var ParCriteria = string.Empty;

                var dc = Application.CreateController<DialogController>();
                e.DialogController.SaveOnAccept = false;
                e.View = lvk;
            }
        }


        void DCListProperty_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            if (View is DetailView)
            {
                System.Collections.ArrayList result = new System.Collections.ArrayList();
                BindingList<DCListProperty> objects = new BindingList<DCListProperty>();

                var mmm = ((ReportExcel)(View.CurrentObject)).ObjectType.GetProperties();
                if (mmm.Count() > 0)
                {

                    foreach (System.Reflection.PropertyInfo m in ((ReportExcel)(View.CurrentObject)).ObjectType.GetProperties())
                    {
                        if (m.PropertyType.FullName == "System.Int32" || m.PropertyType.FullName == "System.String" ||
                             m.PropertyType.FullName.StartsWith("CAMS.Module.") && (m.Name != "Abilitato" && m.Name != "AbilitazioneEreditata"))
                        {
                            result.Add(m.Name);

                            DCListProperty obj = new DCListProperty()
                            {
                                Oid = Guid.NewGuid(),
                                PropertyName = m.Name
                            };
                            objects.Add(obj);
                        }
                    }

                }
                e.Objects = objects;
            }
        }

        void DCListProperty_ObjLookUp_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {

            if (View is DetailView)
            {
                System.Collections.ArrayList result = new System.Collections.ArrayList();
                BindingList<DCListProperty> objects = new BindingList<DCListProperty>();

                var mmm = ((ReportExcel)(View.CurrentObject)).ObjectTypePopUp.GetProperties();
                if (mmm.Count() > 0)
                {

                    foreach (System.Reflection.PropertyInfo m in ((ReportExcel)(View.CurrentObject)).ObjectTypePopUp.GetProperties())
                    {
                        if (m.PropertyType.FullName == "System.Int32" || m.PropertyType.FullName == "System.String" ||
                              m.PropertyType.FullName.StartsWith("CAMS.Module.") && (m.Name != "Abilitato" && m.Name != "AbilitazioneEreditata"))
                        {
                            result.Add(m.Name);

                            DCListProperty obj = new DCListProperty()
                            {
                                Oid = Guid.NewGuid(),
                                PropertyName = m.Name
                            };
                            objects.Add(obj);
                        }
                    }

                }
                e.Objects = objects;
            }
        }


    }
}
