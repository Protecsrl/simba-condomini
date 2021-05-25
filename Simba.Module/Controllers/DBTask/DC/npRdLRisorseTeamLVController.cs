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
using CAMS.Module.DBTask.DC;
using CAMS.Module.BusinessObjects;

namespace CAMS.Module.Controllers.DBTask.DC
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class npRdLRisorseTeamLVController : ObjectViewController<ListView, npRdLRisorseTeam>
    {
        string parameterValue = "dd";

        public npRdLRisorseTeamLVController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            //NonPersistentObjectSpace nonPersistentObjectSpace = (NonPersistentObjectSpace)ObjectSpace;
            //nonPersistentObjectSpace.AdditionalObjectSpaces.Add(Application.CreateObjectSpace(typeof(Contact)));
            //nonPersistentObjectSpace.ObjectsGetting += nonPersistentObjectSpace_ObjectsGetting;
           parameterValue = "Default";
            View.CollectionSource.ResetCollection();
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

        void nonPersistentObjectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            if (e.ObjectType == typeof(npRdLRisorseTeam))
            {
                List<npRdLRisorseTeam> objects = new List<npRdLRisorseTeam>();
                for (int i = 1; i < 6; i++)
                {
                    npRdLRisorseTeam obj = ObjectSpace.CreateObject<npRdLRisorseTeam>();
                    obj.ID = i;
                    obj.Azienda =  " vvv" + i.ToString();
                    objects.Add(obj);
                }
                e.Objects = objects;
            }
        }

    }
}



//public class ViewController1 : ObjectViewController<ListView, NonPersistentClass1>
//{
//    string parameterValue;
//    public ViewController1()
//    {
//        SimpleAction createObjectAction = new SimpleAction(this, "CreateNonPersistentObject", PredefinedCategory.View);
//        createObjectAction.Execute += createObjectAction_Execute;
//        ParametrizedAction fillObjectsAction = new ParametrizedAction(this, "FillNonPersistentObjects", PredefinedCategory.View, typeof(String));
//        fillObjectsAction.Execute += fillObjectsAction_Execute;
//    }

//    protected override void OnActivated()
//    {
//        base.OnActivated();
//        NonPersistentObjectSpace nonPersistentObjectSpace = (NonPersistentObjectSpace)ObjectSpace;
//        nonPersistentObjectSpace.AdditionalObjectSpaces.Add(Application.CreateObjectSpace(typeof(Contact)));
//        nonPersistentObjectSpace.ObjectsGetting += nonPersistentObjectSpace_ObjectsGetting;
//        parameterValue = "Default";
//        View.CollectionSource.ResetCollection();
//    }

//    void nonPersistentObjectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
//    {
//        if (e.ObjectType == typeof(NonPersistentClass1))
//        {
//            List<NonPersistentClass1> objects = new List<NonPersistentClass1>();
//            for (int i = 1; i < 6; i++)
//            {
//                NonPersistentClass1 obj = ObjectSpace.CreateObject<NonPersistentClass1>();
//                obj.Name = parameterValue + " " + i.ToString();
//                objects.Add(obj);
//            }
//            e.Objects = objects;
//        }
//    }

//    void createObjectAction_Execute(object sender, SimpleActionExecuteEventArgs e)
//    {
//        NonPersistentClass1 obj = ObjectSpace.CreateObject<NonPersistentClass1>();
//        obj.Name = "New Object";
//        View.CollectionSource.Add(obj);
//    }

//    void fillObjectsAction_Execute(object sender, ParametrizedActionExecuteEventArgs e)
//    {
//        parameterValue = e.ParameterCurrentValue as String;
//        View.CollectionSource.ResetCollection();
//    }

//    protected override void OnDeactivated()
//    {
//        base.OnDeactivated();
//        NonPersistentObjectSpace nonPersistentObjectSpace = (NonPersistentObjectSpace)ObjectSpace;
//        nonPersistentObjectSpace.AdditionalObjectSpaces[0].Dispose();
//    }
//}



//[DevExpress.ExpressApp.DC.DomainComponent, NavigationItem]
//public class NonPersistentClass1
//{
//    public string Name { get; set; }
//    public Contact Contact { get; set; }
//}







//ObjectViewController<ListView, NonPersistentClass1>
//    public class NonPersistentObjectActivatorController : ObjectViewController<ListView, npRdLRisorseTeam>
//    {
//        protected override void OnActivated()
//        {
//            base.OnActivated();
//            if ((ObjectSpace is NonPersistentObjectSpace) && (View.CurrentObject == null))
//            {
//                View.CurrentObject = View.ObjectTypeInfo.CreateInstance();
//                //View.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
//            }
//        }
//    }
