using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask.POI;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Xpo.Metadata.Helpers;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace CAMS.Module.Controllers.DBRuoloUtente
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class RuoloUserController : ViewController
    {
        public RuoloUserController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            System.Diagnostics.Debug.WriteLine("OnActivated() " + this.Name);
            // Perform various tasks depending on the target View.
            //            type.Name	"KOttimizzazione"	string
            //type.FullName	"CAMS.Module.DBALibrary.KOttimizzazione"	string 
            string NomeRuolo = "nuovoruolo";


        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            this.cmdClonaRuolo.Active.SetItemValue("Active", false);
            if (View is DetailView)
            {
                var dv = View as DetailView;
                CAMS.Module.DBRuoloUtente.ClonaRuoloUser curObj = (CAMS.Module.DBRuoloUtente.ClonaRuoloUser)dv.CurrentObject;
                if (!string.IsNullOrEmpty(curObj.NuovoRuolo) && curObj.Immobile != null)
                    this.cmdClonaRuolo.Active.SetItemValue("Active", true);
            }

        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void cmdClonaRuolo_Execute(object sender,  SimpleActionExecuteEventArgs e)
        {
            string NomeRuolo = "nuovoruolo";
            int OidEdificio = 0;
            var dv = View as DetailView;
            CAMS.Module.DBRuoloUtente.ClonaRuoloUser curObj = (CAMS.Module.DBRuoloUtente.ClonaRuoloUser)dv.CurrentObject;
            OidEdificio = curObj.Immobile.Oid;
            NomeRuolo = curObj.NuovoRuolo;
            EseguiRuolo(NomeRuolo, OidEdificio);
        }

        private void EseguiRuolo(string NomeRuolo, int OidEdificio)
        {   //            type.Name	"KOttimizzazione"	string type.FullName	"CAMS.Module.DBALibrary.KOttimizzazione"	string
            SecuritySystemRole userRole = GetSetUserRole(NomeRuolo);
            var allDataTypes = XafTypesInfo.Instance.PersistentTypes.Where(type =>
                type.IsPersistent && !type.IsAbstract && type.FullName.ToString().Contains("DBPlant.Immobile")).ToList();

            foreach (var cType in allDataTypes)
            {
                try
                {
                    DataView dvTipi = GetListDatibyClasse(cType.FullName, OidEdificio, 0);

                    foreach (DataRowView DataRowdv in dvTipi)
                    {
                        // tipo
                        SecuritySystemTypePermissionObject SSTipo = GetSSystemTypePermissionObject(ref  userRole, (Type)cType.Type,
                                                                    GetBoolean(DataRowdv["AllowRead"].ToString()),
                                                                    GetBoolean(DataRowdv["AllowWrite"].ToString()),
                                                                    GetBoolean(DataRowdv["AllowDelete"].ToString()),
                                                                    GetBoolean(DataRowdv["AllowNavigate"].ToString()));


                        // membri
                        DataView dvPermessiMembri = GetListDatibyClasse(cType.FullName, OidEdificio, 1);
                        foreach (DataRowView RowdvMemb in dvPermessiMembri)
                        {
                            bool SSTPMeb = GetSSystemObjectPermissionsMember(ref  SSTipo, RowdvMemb["strMembri"].ToString(), RowdvMemb["strCriteria"].ToString(),
                                                         GetBoolean(RowdvMemb["AllowRead"].ToString()),
                                                          GetBoolean(RowdvMemb["AllowWrite"].ToString())
                                                          );


                        }
                        // oggetti
                        DataView dvPermessiObject = GetListDatibyClasse(cType.FullName, OidEdificio, 2);
                        foreach (DataRowView DataRowdvObj in dvPermessiObject)
                        {
                            bool SSTPObj = GetSSystemObjectPermissionsObject(ref  SSTipo, DataRowdvObj["strCriteria"].ToString(),
                                                           GetBoolean(DataRowdvObj["AllowRead"].ToString()),
                                                            GetBoolean(DataRowdvObj["AllowWrite"].ToString()),
                                                            GetBoolean(DataRowdvObj["AllowDelete"].ToString()),
                                                            GetBoolean(DataRowdvObj["AllowNavigate"].ToString()));



                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
                }

                //Debug.WriteLine("--------------");
                //Debug.WriteLine(cType.ToString());
                //Debug.WriteLine(((DevExpress.Xpo.XPBaseCollection)(ObjectSpace.GetObjects(cType.Type))).DisplayableProperties);
                //Debug.WriteLine("--------------");
                //var Mess = xpObjectSpace.CreateObject<MessaggioPopUp>();                //Mess.Messaggio = type.ToString();                //var view = Application.CreateDetailView(xpObjectSpace, "MessaggioPopUp_DetailView", true, Mess);
            }
            //foreach (XPClassInfo info in XpoDefault.Session.Dictionary.Classes)
            //{  if (info.IsPersistent && info.IsVisibleInDesignTime)    Debug.WriteLine(info);}
            ObjectSpace.CommitChanges();

        }

        private void acCreaRuolo_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            string NomeRuolo = "nuovoruolo";
            int OidEdificio = 0;
            var dv = View as DetailView;
            CAMS.Module.DBRuoloUtente.ClonaRuoloUser curObj = (CAMS.Module.DBRuoloUtente.ClonaRuoloUser)dv.CurrentObject;
            OidEdificio = curObj.Immobile.Oid;
            NomeRuolo = curObj.NuovoRuolo;

            SecuritySystemRole userRole = GetSetUserRole(NomeRuolo);
            var allDataTypes = XafTypesInfo.Instance.PersistentTypes.Where(type =>
                type.IsPersistent && !type.IsAbstract && type.FullName.ToString().Contains(".DB")).ToList();

            foreach (var cType in allDataTypes)
            {
                try
                {
                    DataView dvTipi = GetListDatibyClasseCreaRuole(cType.FullName, OidEdificio, 0);

                    foreach (DataRowView DataRowdv in dvTipi)
                    {
                        // tipo
                        SecuritySystemTypePermissionObject SSTipo = GetSSystemTypePermissionObject(ref  userRole, (Type)cType.Type,
                                                                    GetBoolean(DataRowdv["AllowRead"].ToString()),
                                                                    GetBoolean(DataRowdv["AllowWrite"].ToString()),
                                                                    GetBoolean(DataRowdv["AllowDelete"].ToString()),
                                                                    GetBoolean(DataRowdv["AllowNavigate"].ToString()));


                        // membri
                        DataView dvPermessiMembri = GetListDatibyClasseCreaRuole(cType.FullName, OidEdificio, 1);
                        foreach (DataRowView RowdvMemb in dvPermessiMembri)
                        {
                            bool SSTPMeb = GetSSystemObjectPermissionsMember(ref  SSTipo, RowdvMemb["strMembri"].ToString(), RowdvMemb["strCriteria"].ToString(),
                                                         GetBoolean(RowdvMemb["AllowRead"].ToString()),
                                                          GetBoolean(RowdvMemb["AllowWrite"].ToString())
                                                          );


                        }
                        // oggetti
                        DataView dvPermessiObject = GetListDatibyClasseCreaRuole(cType.FullName, OidEdificio, 2);
                        foreach (DataRowView DataRowdvObj in dvPermessiObject)
                        {
                            bool SSTPObj = GetSSystemObjectPermissionsObject(ref  SSTipo, DataRowdvObj["strCriteria"].ToString(),
                                                           GetBoolean(DataRowdvObj["AllowRead"].ToString()),
                                                            GetBoolean(DataRowdvObj["AllowWrite"].ToString()),
                                                            GetBoolean(DataRowdvObj["AllowDelete"].ToString()),
                                                            GetBoolean(DataRowdvObj["AllowNavigate"].ToString()));



                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
                }

                //Debug.WriteLine("--------------");
                //Debug.WriteLine(cType.ToString());
                //Debug.WriteLine(((DevExpress.Xpo.XPBaseCollection)(ObjectSpace.GetObjects(cType.Type))).DisplayableProperties);
                //Debug.WriteLine("--------------");
                //var Mess = xpObjectSpace.CreateObject<MessaggioPopUp>();                //Mess.Messaggio = type.ToString();           
                //var view = Application.CreateDetailView(xpObjectSpace, "MessaggioPopUp_DetailView", true, Mess);
            }
            //foreach (XPClassInfo info in XpoDefault.Session.Dictionary.Classes)
            //{  if (info.IsPersistent && info.IsVisibleInDesignTime)    Debug.WriteLine(info);}
            ObjectSpace.CommitChanges();

        }

        private void acElencoMembri_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            string NomeRuolo = "nuovoruolo";
            int OidEdificio = 0;
            var dv = View as DetailView;
            CAMS.Module.DBRuoloUtente.ClonaRuoloUser curObj = (CAMS.Module.DBRuoloUtente.ClonaRuoloUser)dv.CurrentObject;
            OidEdificio = curObj.Immobile.Oid;
            NomeRuolo = curObj.NuovoRuolo;

            SecuritySystemRole userRole = GetSetUserRole(NomeRuolo);
            var allDataTypes = XafTypesInfo.Instance.PersistentTypes.Where(type =>
                type.IsPersistent && !type.IsAbstract && type.FullName.ToString().Contains(".DB")).ToList();

            foreach (var cType in allDataTypes)
            {
                try
                {
                    Debug.WriteLine("--------------");
                    Debug.WriteLine(cType.ToString());
                    Debug.WriteLine(((DevExpress.Xpo.XPBaseCollection)(ObjectSpace.GetObjects(cType.Type))).DisplayableProperties);
                    Debug.WriteLine("--------------");

                    var StrMembri = ((DevExpress.Xpo.XPBaseCollection)(ObjectSpace.GetObjects(cType.Type))).DisplayableProperties;                   
                    
                    var SpltProprita = StrMembri.Split(new Char[] { ';' });

                    string elencofiltrato = "";
                    foreach (string  pr in SpltProprita)
                    {
                        if (!(pr.Contains("!") || pr.Contains("This") || pr.Contains("Oid")))
                        {
                            Debug.WriteLine(pr);
                            if (elencofiltrato == "")
                                elencofiltrato = pr;
                            else
                                elencofiltrato =elencofiltrato+";"+ pr;

                        }
                        
                    }
                    Debug.WriteLine(elencofiltrato); 
                     //public string SetMebri(string FullNomeClasse, string strMembri)
                    string nomeClasse = cType.FullName;
                    using (DB db = new DB())
                    {
                        if (elencofiltrato != null ) // membri
                            db.SetMebri(nomeClasse, elencofiltrato);
                    }


                    //foreach (XPClassInfo info in XpoDefault.Session.Dictionary.Classes)
                    //{ 
                    //    if (info.IsPersistent && info.IsVisibleInDesignTime)  
                    //    Debug.WriteLine(info); }
                   
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
                }
            }
        }

        private SecuritySystemRole GetSetUserRole(string NomeRuolo)
        {
            SecuritySystemRole userRole = ObjectSpace.FindObject<SecuritySystemRole>(new BinaryOperator("Name", NomeRuolo));
            if (userRole == null)
            {
                userRole = ObjectSpace.CreateObject<SecuritySystemRole>();
                userRole.Name = NomeRuolo;
            }

            GetSetUser(ref   userRole, NomeRuolo);
            return userRole;
        }

        private bool GetSetUser(ref SecuritySystemRole userRole, string NomeRuolo)
        {
            try
            {
                var userBase = ObjectSpace.FindObject<SecuritySystemUser>(new BinaryOperator("UserName", NomeRuolo));
                if (userBase == null)
                {
                    userBase = ObjectSpace.CreateObject<SecuritySystemUser>();
                    userBase.UserName = NomeRuolo;
                    userBase.SetPassword(string.Empty);
                    userBase.Roles.Add(userRole);
                }
                else userBase.Roles.Add(userRole);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: non applicato accesso ai ruoli; " + ex.Message));
                return false;
            }
            return true;
        }

        private SecuritySystemTypePermissionObject GetSSystemTypePermissionObject(ref SecuritySystemRole userRole, Type TargetType,
                                                                      bool AllowRead, bool AllowWrite, bool AllowDelete, bool AllowNavigate)
        {
            SecuritySystemTypePermissionObject userPermissions = ObjectSpace.CreateObject<SecuritySystemTypePermissionObject>();
            userPermissions.TargetType = TargetType; // typeof(Immobile); //typeof(Employee);
            userPermissions.AllowRead = AllowRead;
            userPermissions.AllowWrite = AllowWrite;
            userPermissions.AllowDelete = AllowDelete;
            userPermissions.AllowNavigate = AllowNavigate;
            userRole.TypePermissions.Add(userPermissions);
            return userPermissions;
        }

        private bool GetSSystemObjectPermissionsObject(ref SecuritySystemTypePermissionObject userPermissions
            , string Criteria, bool AllowRead, bool AllowWrite, bool AllowDelete, bool AllowNavigate)
        {
            try
            {
                SecuritySystemObjectPermissionsObject SSObjPermissionObj = ObjectSpace.CreateObject<SecuritySystemObjectPermissionsObject>();
                SSObjPermissionObj.Criteria = Criteria; // "Oid = 2";
                //PuoVedereCommessedelSuoEdificio.Criteria = new BinaryOperator(new OperandProperty("Department.Oid"),      joinEdificiToAccessOwnCommesse, BinaryOperatorType.Equal).ToString();        
                SSObjPermissionObj.AllowRead = AllowRead;
                SSObjPermissionObj.AllowWrite = AllowWrite;
                SSObjPermissionObj.AllowDelete = AllowDelete;
                SSObjPermissionObj.AllowNavigate = AllowNavigate;
                userPermissions.ObjectPermissions.Add(SSObjPermissionObj);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: non applicato accesso ai ruoli; " + ex.Message));
                return false;
            }
            return true;
        }

        private bool GetSSystemObjectPermissionsMember(ref SecuritySystemTypePermissionObject userPermissions, string Membri, string Criteria
                                                                                      , bool AllowRead, bool AllowWrite)
        {
            try
            {
                SecuritySystemMemberPermissionsObject SSystemMemberPermissionsObject = ObjectSpace.CreateObject<SecuritySystemMemberPermissionsObject>();
                SSystemMemberPermissionsObject.Members = Membri;// "CodDescrizione; Descrizione";
                SSystemMemberPermissionsObject.Criteria = Criteria;// "Oid = [<Immobile>][Oid = 2]"; //"Oid=CurrentUserId()";
                // PuoModificarelesueUserPermission.Criteria = (new OperandProperty("Oid") == new FunctionOperator(CurrentUserIdOperator.OperatorName)).ToString();
                SSystemMemberPermissionsObject.AllowRead = AllowRead; //true;
                SSystemMemberPermissionsObject.AllowWrite = AllowWrite;// true;
                userPermissions.MemberPermissions.Add(SSystemMemberPermissionsObject);

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: non applicato accesso ai ruoli; " + ex.Message));
                return false;
            }
            return true;
        }

        private bool SetNoAccesso_Ruoli(ref SecuritySystemRole userRole, ref SecuritySystemTypePermissionObject userPermissions)
        {
            //Cannot access roles.
            try
            {
                SecuritySystemTypePermissionObject rolePermissions = ObjectSpace.CreateObject<SecuritySystemTypePermissionObject>();
                rolePermissions.TargetType = typeof(SecuritySystemRole);
                userRole.TypePermissions.Add(rolePermissions);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Errore: non applicato accesso ai ruoli; " + ex.Message));
                return false;
            }
            return true;
        }



        private DataView GetListDatibyClasse(string nomeClasse, int OidEdificio, int TipoDati)
        {
            DataView dv = new DataView();

            using (DB db = new DB())
            {
                if (TipoDati == 0) // Tipi
                    dv = db.GetDatiTipi(nomeClasse, OidEdificio, 0);

                if (TipoDati == 1) // membri
                    dv = db.GetDatiPermessiMebri(nomeClasse, OidEdificio, 0);

                if (TipoDati == 2) // oggetti
                    dv = db.GetDatiPermessiOggetti(nomeClasse, OidEdificio, 0);
            }
            return dv;
        }

        private DataView GetListDatibyClasseCreaRuole(string nomeClasse, int OidEdificio, int TipoDati)
        {
            DataView dv = new DataView();

            using (DB db = new DB())
            {
                if (TipoDati == 0) // Tipi
                    dv = db.GetDatiTipi(nomeClasse, OidEdificio, 1);

                if (TipoDati == 1) // membri
                    dv = db.GetDatiPermessiMebri(nomeClasse, OidEdificio, 1);

                if (TipoDati == 2) // oggetti
                    dv = db.GetDatiPermessiOggetti(nomeClasse, OidEdificio, 1);
            }
            return dv;
        }

        private bool GetBoolean(string param1)
        {
            if (param1 == "1")
                return true;

            return false;
            // throw new NotImplementedException();
        }

        private SecuritySystemRole GetUserRole1(string NomeRuolo)
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

        //        using DevExpress.Xpo;
        //using DevExpress.Xpo.Metadata;
        //using DevExpress.Xpo.Metadata.Helpers;

        string[] GetObjectProperties(Type objectType)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;

            XPClassInfo classInfo = Sess.Dictionary.GetClassInfo(objectType);

            if (classInfo != null)
                return GetObjectProperties(classInfo, new ArrayList());

            return new string[] { };
        }



        string[] GetObjectProperties(XPClassInfo xpoInfo, ArrayList processed)
        {

            if (processed.Contains(xpoInfo)) return new string[] { };

            processed.Add(xpoInfo);
            ArrayList result = new ArrayList();

            foreach (XPMemberInfo m in xpoInfo.PersistentProperties)
                if (!(m is ServiceField) && m.IsPersistent)
                {
                    result.Add(m.Name);
                    if (m.ReferenceType != null)
                    {
                        string[] childProps = GetObjectProperties(m.ReferenceType, processed);
                        foreach (string child in childProps)
                            result.Add(string.Format("{0}.{1}", m.Name, child));
                    }

                }



            foreach (XPMemberInfo m in xpoInfo.CollectionProperties)
            {

                string[] childProps = GetObjectProperties(m.CollectionElementType, processed);

                foreach (string child in childProps)
                    result.Add(string.Format("{0}.{1}", m.Name, child));
            }

            return result.ToArray(typeof(string)) as string[];

        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Type objectType=typeof(ListPOI);
               string[] a = GetObjectProperties( objectType);
               foreach (string child in a)
                   Debug.WriteLine(child);
        }


    }
}
