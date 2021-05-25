using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security.Strategy;
using System;
using System.Diagnostics;
using System.Linq;

namespace CAMS.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class CreaUserRuoloController : ViewController
    {
        public CreaUserRuoloController()
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
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
            //GetDatiUserRole("Collio") ;
        }

        private void popupWinSelezionaEdificio_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();

            if (xpObjectSpace != null)
            {
 
                var listViewId = "MessaggioPopUp_ListView";
                var clTicketLv = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(MessaggioPopUp), listViewId);
                var view =  Application.CreateListView(listViewId, clTicketLv, true);
                e.View = view;
                e.IsSizeable = true;
                var allDataTypes = XafTypesInfo.Instance.PersistentTypes.Where(type => type.IsPersistent && !type.IsAbstract && type.FullName.ToString().Contains("DB"));
                foreach (var type in allDataTypes)
                {
                    Debug.WriteLine("--------------");
                    Debug.WriteLine(type.ToString());
                    Debug.WriteLine(((DevExpress.Xpo.XPBaseCollection)(ObjectSpace.GetObjects(type.Type))).DisplayableProperties);
                    Debug.WriteLine("--------------");
                    //var Mess = xpObjectSpace.CreateObject<MessaggioPopUp>();
                    //Mess.Messaggio = type.ToString();
                    //var view = Application.CreateDetailView(xpObjectSpace, "MessaggioPopUp_DetailView", true, Mess);
                }
            }
        }
        private void popupWinSelezionaEdificio_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            CreaRuoloUtenteBASE();
        }

        private void CreaRuoloUtenteBASE()
        {
            var userBase = ObjectSpace.FindObject<SecuritySystemUser>(new BinaryOperator("UserName", "Collio"));
            if (userBase == null)
            {
                userBase = ObjectSpace.CreateObject<SecuritySystemUser>();
                userBase.UserName = "Collio";
                userBase.SetPassword(string.Empty);
                userBase.Roles.Add(GetUserRole("Collio"));
            }
            else userBase.Roles.Add(GetUserRole("Collio"));

            ObjectSpace.CommitChanges();
        }


        //private void GetDatiUserRole(string NomeRuolo)
        //{
        //    SecuritySystemRole userRole = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", NomeRuolo));

        //    if (userRole != null)
        //    {
        //        //userRole.TypePermissions.
        //    }
        //}

        private SecuritySystemRole GetUserRole(string NomeRuolo)
        {
            SecuritySystemRole userRole = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", NomeRuolo));            
 
            if (userRole == null)
            {
                userRole = ObjectSpace.CreateObject<SecuritySystemRole>();
                userRole.Name = NomeRuolo;

                #region su immobile
                //Cannot navigate to employees. Commesse
              //  IObjectSpace aa = Application.CreateObjectSpace;
                
                SecuritySystemTypePermissionObject userPermissions = ObjectSpace.CreateObject<SecuritySystemTypePermissionObject>();
                userPermissions.TargetType = typeof(Immobile); //typeof(Employee);
                userPermissions.AllowNavigate = true;
                userRole.TypePermissions.Add(userPermissions);
                //Can view employees only from own department. Possono visualizzare dipendenti solo dal proprio reparto.
                //  quindi definisco la OBJET permission e la MEMBER
                //SecuritySystemObjectPermissionsObject canViewEmployeesFromOwnDepartmentPermission = ObjectSpace.CreateObject<SecuritySystemObjectPermissionsObject>();
                // Immobile
                SecuritySystemObjectPermissionsObject PuoVedereilSuoEdificio = ObjectSpace.CreateObject<SecuritySystemObjectPermissionsObject>();
                PuoVedereilSuoEdificio.Criteria = "Oid = 2";
                //PuoVedereCommessedelSuoEdificio.Criteria = new BinaryOperator(new OperandProperty("Department.Oid"),      joinEdificiToAccessOwnCommesse, BinaryOperatorType.Equal).ToString();
                PuoVedereilSuoEdificio.AllowNavigate = true;
                PuoVedereilSuoEdificio.AllowRead = true;
                userPermissions.ObjectPermissions.Add(PuoVedereilSuoEdificio);

                //Can change a couple properties of own user. Può cambiare della coppia proprietà di propria utenza.
                //  SecuritySystemMemberPermissionsObject canEditOwnUserPermission = ObjectSpace.CreateObject<SecuritySystemMemberPermissionsObject>();
                SecuritySystemMemberPermissionsObject PuoModificareleilSuoEdificioUserPermission = ObjectSpace.CreateObject<SecuritySystemMemberPermissionsObject>();
                PuoModificareleilSuoEdificioUserPermission.Members = "CodDescrizione; Descrizione; Indirizzo; DataSheet";
                PuoModificareleilSuoEdificioUserPermission.Criteria = "Oid = 2";  //"Oid=CurrentUserId()";
                PuoModificareleilSuoEdificioUserPermission.AllowRead = true;
                // PuoModificarelesueUserPermission.Criteria = (new OperandProperty("Oid") == new FunctionOperator(CurrentUserIdOperator.OperatorName)).ToString();
                PuoModificareleilSuoEdificioUserPermission.AllowWrite = true;
                userPermissions.MemberPermissions.Add(PuoModificareleilSuoEdificioUserPermission);
                PuoModificareleilSuoEdificioUserPermission = ObjectSpace.CreateObject<SecuritySystemMemberPermissionsObject>();
                PuoModificareleilSuoEdificioUserPermission.Members = "CodDescrizione;Commesse;ClusterEdifici;RegistroCostis;DestinatariControlliNormativis;Documentis;ControlliNormativis;Impianti";
                PuoModificareleilSuoEdificioUserPermission.Criteria = "Oid = 2";  //"Oid=CurrentUserId()";
                PuoModificareleilSuoEdificioUserPermission.AllowRead = true;
                PuoModificareleilSuoEdificioUserPermission.AllowWrite = false;
                userPermissions.MemberPermissions.Add(PuoModificareleilSuoEdificioUserPermission);
                //------------------------------------------------
                #endregion

                #region su commesse
                //Cannot navigate to employees. Commesse
                userPermissions = ObjectSpace.CreateObject<SecuritySystemTypePermissionObject>();
                userPermissions.TargetType = typeof(Contratti); //typeof(Employee);
                userPermissions.AllowNavigate = false;
                userRole.TypePermissions.Add(userPermissions);
                //Can view employees only from own department. Possono visualizzare dipendenti solo dal proprio reparto.
                //  quindi definisco la OBJET permission e la MEMBER
                //SecuritySystemObjectPermissionsObject canViewEmployeesFromOwnDepartmentPermission = ObjectSpace.CreateObject<SecuritySystemObjectPermissionsObject>();
                // Immobile
                SecuritySystemObjectPermissionsObject PuoVedereCommessedelSuoEdificio = ObjectSpace.CreateObject<SecuritySystemObjectPermissionsObject>();
                PuoVedereCommessedelSuoEdificio.Criteria = "Oid = [<Immobile>][Oid = 2].Single(Commesse.Oid)";
                //PuoVedereCommessedelSuoEdificio.Criteria = new BinaryOperator(new OperandProperty("Department.Oid"),      joinEdificiToAccessOwnCommesse, BinaryOperatorType.Equal).ToString();
                PuoVedereCommessedelSuoEdificio.AllowNavigate = true;
                PuoVedereCommessedelSuoEdificio.AllowRead = true;
                userPermissions.ObjectPermissions.Add(PuoVedereCommessedelSuoEdificio);

                //Can change a couple properties of own user. Può cambiare della coppia proprietà di propria utenza.
                //  SecuritySystemMemberPermissionsObject canEditOwnUserPermission = ObjectSpace.CreateObject<SecuritySystemMemberPermissionsObject>();
                SecuritySystemMemberPermissionsObject PuoModificarelesueUserPermission = ObjectSpace.CreateObject<SecuritySystemMemberPermissionsObject>();
                PuoModificarelesueUserPermission.Members = "CodDescrizione; Descrizione";
                PuoModificarelesueUserPermission.Criteria = "Oid = [<Immobile>][Oid = 2].Single(Commesse.Oid)"; //"Oid=CurrentUserId()";
                PuoModificarelesueUserPermission.AllowRead = true;
                PuoModificarelesueUserPermission.AllowWrite = true;
                userPermissions.MemberPermissions.Add(PuoModificarelesueUserPermission);

                PuoModificarelesueUserPermission = ObjectSpace.CreateObject<SecuritySystemMemberPermissionsObject>();
                PuoModificarelesueUserPermission.Members = "Cliente; CentroCosto; CodContratto";
                PuoModificarelesueUserPermission.Criteria = "Oid = [<Immobile>][Oid = 2].Single(Commesse.Oid)"; //"Oid=CurrentUserId()";
                PuoModificarelesueUserPermission.AllowRead = true;
                PuoModificarelesueUserPermission.AllowWrite = false;
                userPermissions.MemberPermissions.Add(PuoModificarelesueUserPermission);
                //------------------------------------------------
                #endregion

                #region su Impianto
                //Cannot navigate to employees. Commesse
                userPermissions = ObjectSpace.CreateObject<SecuritySystemTypePermissionObject>();
                userPermissions.TargetType = typeof(Servizio); //typeof(Employee);
                userPermissions.AllowNavigate = false;
                userPermissions.AllowRead = true;
                userRole.TypePermissions.Add(userPermissions);
                //Can view employees only from own department. Possono visualizzare dipendenti solo dal proprio reparto.
                //  quindi definisco la OBJET permission e la MEMBER
                //SecuritySystemObjectPermissionsObject canViewEmployeesFromOwnDepartmentPermission = ObjectSpace.CreateObject<SecuritySystemObjectPermissionsObject>();
                // Immobile
                SecuritySystemObjectPermissionsObject PuoVedereImpiantidelSuoEdificio = ObjectSpace.CreateObject<SecuritySystemObjectPermissionsObject>();
                PuoVedereImpiantidelSuoEdificio.Criteria = "Oid = [<Immobile>][Oid = 2]";
                //PuoVedereCommessedelSuoEdificio.Criteria = new BinaryOperator(new OperandProperty("Department.Oid"),      joinEdificiToAccessOwnCommesse, BinaryOperatorType.Equal).ToString();
                PuoVedereImpiantidelSuoEdificio.AllowNavigate = true;
                PuoVedereImpiantidelSuoEdificio.AllowRead = true;
                userPermissions.ObjectPermissions.Add(PuoVedereImpiantidelSuoEdificio);

                //Can change a couple properties of own user. Può cambiare della coppia proprietà di propria utenza.
                //  SecuritySystemMemberPermissionsObject canEditOwnUserPermission = ObjectSpace.CreateObject<SecuritySystemMemberPermissionsObject>();
                SecuritySystemMemberPermissionsObject PuoModificareImpiantoUserPermission = ObjectSpace.CreateObject<SecuritySystemMemberPermissionsObject>();
                PuoModificareImpiantoUserPermission.Members = "CodDescrizione; Descrizione";
                PuoModificareImpiantoUserPermission.Criteria = "Oid = [<Immobile>][Oid = 2]"; //"Oid=CurrentUserId()";
                // PuoModificarelesueUserPermission.Criteria = (new OperandProperty("Oid") == new FunctionOperator(CurrentUserIdOperator.OperatorName)).ToString();
                PuoModificareImpiantoUserPermission.AllowWrite = true;
                userPermissions.MemberPermissions.Add(PuoModificareImpiantoUserPermission);
                //------------------------------------------------
                #endregion

                //Cannot access roles.
                SecuritySystemTypePermissionObject rolePermissions = ObjectSpace.CreateObject<SecuritySystemTypePermissionObject>();
                rolePermissions.TargetType = typeof(SecuritySystemRole);
                userRole.TypePermissions.Add(rolePermissions);

                ////Can navigate to tasks, but cannot create them. Può navigare compiti, ma non li può creare.
                //SecuritySystemTypePermissionObject employeeTaskPermissions = ObjectSpace.CreateObject<SecuritySystemTypePermissionObject>();
                //employeeTaskPermissions.TargetType = typeof(Impianto);//typeof(EmployeeTask);
                //employeeTaskPermissions.AllowNavigate = true;
                //employeeTaskPermissions.AllowCreate = false;
                //userRole.TypePermissions.Add(employeeTaskPermissions);
                ////Can view and edit own tasks, but cannot delete them.  Possibile visualizzare e modificare le attività proprie, ma non li può cancellare.
                //SecuritySystemObjectPermissionsObject canManageOwnTasksObjectPermission = ObjectSpace.CreateObject<SecuritySystemObjectPermissionsObject>();
                //canManageOwnTasksObjectPermission.Criteria = "AssignedTo.Oid=CurrentUserId()";
                //canManageOwnTasksObjectPermission.Criteria = (new OperandProperty("AssignedTo.Oid") == new FunctionOperator(CurrentUserIdOperator.OperatorName)).ToString();
                //canManageOwnTasksObjectPermission.AllowNavigate = true;
                //canManageOwnTasksObjectPermission.AllowRead = true;
                //canManageOwnTasksObjectPermission.AllowWrite = true;
                //canManageOwnTasksObjectPermission.AllowDelete = false;
                //canManageOwnTasksObjectPermission.Save();
                //employeeTaskPermissions.ObjectPermissions.Add(canManageOwnTasksObjectPermission);

                ////Can view, but cannot edit tasks from other users within own department.  Può visualizzare, ma non può modificare le attività di altri utenti all'interno di un reparto.
                //SecuritySystemObjectPermissionsObject canSeeTasksOnlyFromOwnDepartmentObjectPermission = ObjectSpace.CreateObject<SecuritySystemObjectPermissionsObject>();
                //canSeeTasksOnlyFromOwnDepartmentObjectPermission.Criteria = "AssignedTo.Department.Oid=[<Employee>][Oid=CurrentUserId()].Single(Department.Oid)";
                //canSeeTasksOnlyFromOwnDepartmentObjectPermission.Criteria = new BinaryOperator(new OperandProperty("AssignedTo.Department.Oid"), joinEdificiToAccessOwnCommesse, BinaryOperatorType.Equal).ToString();
                //canSeeTasksOnlyFromOwnDepartmentObjectPermission.AllowNavigate = true;
                //canSeeTasksOnlyFromOwnDepartmentObjectPermission.AllowRead = true;
                //canSeeTasksOnlyFromOwnDepartmentObjectPermission.AllowWrite = false;
                //canSeeTasksOnlyFromOwnDepartmentObjectPermission.AllowDelete = false;
                //canSeeTasksOnlyFromOwnDepartmentObjectPermission.Save();
                //employeeTaskPermissions.ObjectPermissions.Add(canSeeTasksOnlyFromOwnDepartmentObjectPermission);

                //Cannot navigate to departments. Non è possibile navigare reparti.
                //SecuritySystemTypePermissionObject departmentPermissions = ObjectSpace.CreateObject<SecuritySystemTypePermissionObject>();
                //departmentPermissions.TargetType = typeof(Department); //typeof(Department);
                //userRole.TypePermissions.Add(departmentPermissions);
                ////Can read and navigate to own department.   Può leggere e navigare al proprio reparto.
                //SecuritySystemObjectPermissionsObject canSeeOwnDepartmentObjectPermission = ObjectSpace.CreateObject<SecuritySystemObjectPermissionsObject>();
                //canSeeOwnDepartmentObjectPermission.Criteria = "Oid=[<Employee>][Oid=CurrentUserId()].Single(Department.Oid)";
                //canSeeOwnDepartmentObjectPermission.Criteria = new BinaryOperator(new OperandProperty("Oid"), joinEmployeesToAccessOwnDepartmemnt, BinaryOperatorType.Equal).ToString();
                //canSeeOwnDepartmentObjectPermission.AllowNavigate = true;
                //canSeeOwnDepartmentObjectPermission.AllowRead = true;
                //canSeeOwnDepartmentObjectPermission.Save();
                //departmentPermissions.ObjectPermissions.Add(canSeeOwnDepartmentObjectPermission);
            }
            return userRole;
        }

        private void popupWinSelezionaEdificio_Cancel(object sender, EventArgs e)
        {
            string NomeRuolo =  "Collio";
            var userBase = ObjectSpace.FindObject<SecuritySystemUser>(new BinaryOperator("UserName", NomeRuolo));
            if (userBase != null)
            {
                userBase.Delete();
               ObjectSpace.CommitChanges();
            }
             
            SecuritySystemRole userRole = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", NomeRuolo));
            if (userRole != null)
            {
                userRole.Delete();
                ObjectSpace.CommitChanges();
            }
        }


    }
}
