using CAMS.Module.DBALibrary.Vista;
using CAMS.Module.DBPlant;
using CAMS.Module.DBPlant.Vista;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using System;

namespace CAMS.Module.Controllers.Tree
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class TreeController : ViewController
    {
        public TreeController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            System.Diagnostics.Debug.WriteLine("OnActivated() " + this.Name);

            // CAMS.Module.DBALibrary.Vista.SistemaTecnologicoTree // View.ObjectTypeInfo.Type == typeof(ImpiantoTree) || // View.ObjectTypeInfo.Type == typeof(ApparatoTree)   
            if ((View != null && View.ObjectTypeInfo != null))
                if (IsObjTree())
                {  
                    acShowDettaglio.Active.SetItemValue("Active", true);
                    acFindByName.Active.SetItemValue("Active", false);
                    if (View.ObjectTypeInfo.Type == typeof(SistemaTecnologicoTree))
                    {
                        acShowDettaglio.Active.SetItemValue("Active", true);
                        acFindByName.Active.SetItemValue("Active", true);
                    }
                }
                else
                {
                    acShowDettaglio.Active.SetItemValue("Active", false);
                    acFindByName.Active.SetItemValue("Active", false);
                }
        }



 


        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
         
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            //if (LViewProcCurrObjeController != null)
            //{
            //    LViewProcCurrObjeController.CustomProcessSelectedItem -= LViewController_CustomDetailView;
            //}
            acShowDettaglio.Active.SetItemValue("Active", false);
            acFindByName.Active.SetItemValue("Active", false);
            base.OnDeactivated();

        }

        private void acShowDettaglio_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            CAMS.Module.Classi.SetVarSessione.Esegui_DeSelezionaDati = false;
            if (xpObjectSpace != null)
            {
                if (View is ListView) //View.CurrentObject.GetType().FullName	"CAMS.Module.DBPlant.Vista.EdificioTree"	string
                {
                    if (e != null & View.Id == "EdificioTree_ListView")
                        try
                        {
                            string DetailViewId = "";
                            //Type TipoObjCurr = e.GetType();                            CAMS.Module.DBPlant.TreeObject TreeObj = (CAMS.Module.DBPlant.TreeObject)xpObjectSpace.GetObject(e.CurrentObject);
                            CAMS.Module.DBPlant.Vista.EdificioTree EdTree = (CAMS.Module.DBPlant.Vista.EdificioTree)e.CurrentObject;
                            //string Oggetto = ((CAMS.Module.DBPlant.Vista.EdificioTree)(((DevExpress.Xpo.XPBaseObject)(Obj)).This)).Tipo;      //string CodiceObj = Obj.Oid.ToString();
                            int OidOggetto = int.Parse(EdTree.GetMemberValue("OidOggetto").ToString()) <= 0 ? 0 : int.Parse(EdTree.GetMemberValue("OidOggetto").ToString());
                            string TipoOggetto = EdTree.GetMemberValue("Tipo") == null ? "" : EdTree.GetMemberValue("Tipo").ToString();
                            // int OidOggetto = TreeObj.GetMemberValue.OidOggetto;
                            // string TipoOggetto = TreeObj.Tipo;
                            string PathOggetto = "";
                            bool ParametriOggetto = GetParametriOggetto(TipoOggetto, ref   PathOggetto, ref   DetailViewId);
                            if (ParametriOggetto)
                            {
                                Type type = xpObjectSpace.TypesInfo.FindTypeInfo(PathOggetto).Type;           //Type type1 = XafTypesInfo.Instance.FindTypeInfo(PathOggetto).Type;
                                var xpObjClasse = xpObjectSpace.GetObjectByKey(type, OidOggetto);
                                var view = Application.CreateDetailView(xpObjectSpace, DetailViewId, true, xpObjClasse);
                                view.ViewEditMode = ViewEditMode.View;
                                e.ShowViewParameters.CreatedView = view;
                                view.Caption = view.Caption + " - Dettaglio Albero";

                                e.ShowViewParameters.Context = TemplateContext.View;
                                e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                                e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Default;
                            }
                        }
                        catch
                        {
                            throw new Exception(string.Format("Oggetto non esiste!;"));
                        }

                }
            }
        }

        private void acFindByName_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            string paramValue = e.ParameterCurrentValue as string;
            if (!string.IsNullOrEmpty(paramValue))
            {
                paramValue = "%" + paramValue + "%";
            }
            object obj = objectSpace.FindObject(((ListView)View).ObjectTypeInfo.Type,
                new BinaryOperator("Name", paramValue, BinaryOperatorType.Like));
            if (obj != null)
            {
                e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace, obj);
            }

 
        }

        private bool IsObjTree()
        {
            if ((View != null && View.ObjectTypeInfo != null))
                if (View.ObjectTypeInfo.Type == typeof(CommesseTree) ||
                    View.ObjectTypeInfo.Type == typeof(EdificioTree) ||
                     View.ObjectTypeInfo.Type == typeof(Asset) ||
                    View.ObjectTypeInfo.Type == typeof(SistemaTecnologicoTree))
                {
                    return true;
                }
            return false;
        }

        private bool GetParametriOggetto(string TipoOggetto, ref string PathOggetto, ref string DetailViewId)
        {
            bool Ritorno = true;
            switch (TipoOggetto)
            {
                case "Commesse":
                    DetailViewId = TipoOggetto + "_DetailView_Gestione";
                    PathOggetto = "CAMS.Module.DBPlant." + TipoOggetto;
                    break;
                case "Apparato":
                    DetailViewId = TipoOggetto + "_DetailView_Gestione";
                    PathOggetto = "CAMS.Module.DBPlant." + TipoOggetto;
                    break;
                case "Impianto":
                    DetailViewId = TipoOggetto + "_DetailView";
                    PathOggetto = "CAMS.Module.DBPlant." + TipoOggetto;
                    break;
                case "Immobile":
                    DetailViewId = TipoOggetto + "_DetailView_Gestione";
                    PathOggetto = "CAMS.Module.DBPlant." + TipoOggetto;
                    break;
                case "SistemaTecnologico":
                    DetailViewId = TipoOggetto + "_DetailView"; // _Gestione
                    PathOggetto = "CAMS.Module.DBALibrary." + TipoOggetto;
                    break;
                case "SistemaClassi":
                    DetailViewId = TipoOggetto + "_DetailView";
                    PathOggetto = "CAMS.Module.DBALibrary." + TipoOggetto;
                    break;
                case "Sistema":
                    DetailViewId = TipoOggetto + "_DetailView";
                    PathOggetto = "CAMS.Module.DBALibrary." + TipoOggetto;
                    break;
                case "StdApparatoClassi":
                    DetailViewId = TipoOggetto + "_DetailView";
                    PathOggetto = "CAMS.Module.DBALibrary." + TipoOggetto;
                    break;
                case "StdApparato":
                    DetailViewId = TipoOggetto + "_DetailView";
                    PathOggetto = "CAMS.Module.DBALibrary." + TipoOggetto;
                    break;
                case "SchedaMp":
                    DetailViewId = TipoOggetto + "_DetailView";
                    PathOggetto = "CAMS.Module.DBALibrary." + TipoOggetto;
                    break;
                default:
                    Ritorno = false;
                    break;
            }

            return Ritorno;
        }
    }
}
