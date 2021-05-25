using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;

using DevExpress.Xpo;
using CAMS.Module.DBTask;

using CAMS.Module.DBPlant;

using DevExpress.Persistent.BaseImpl;
using CAMS.Module.DBALibrary;
using System.Data;
using DevExpress.ExpressApp.Xpo;
using CAMS.Module.DBMail;
using System;
using CAMS.Module.DBTask.AppsCAMS;
using CAMS.Module.DBAux;

namespace CAMS.Module.Classi
{

    public partial class UtilController : ViewController
    {        //private IObjectSpace theObjectSpace;        //private XafApplication theApplication;        
        #region funzioni comuni a Rdl e Registro Rdl
        //public string GetFiltraComboRisorseTeam(string OidRdL, Immobile Immobile)
        //{
        //    try
        //    {
        //        SetVarSessione.OidEdificioCalcoloDistanze = Immobile.Oid;
        //        SetVarSessione.OidEdificioCalcoloDistanzeLatitudine = double.Parse(Immobile.Indirizzo.Latitude.ToString());
        //        SetVarSessione.OidEdificioCalcoloDistanzeLongitudine = double.Parse(Immobile.Indirizzo.Longitude.ToString());
        //    }
        //    catch
        //    {
        //        SetVarSessione.OidEdificioCalcoloDistanze = Immobile.Oid;
        //        SetVarSessione.OidEdificioCalcoloDistanzeLatitudine = 0;
        //        SetVarSessione.OidEdificioCalcoloDistanzeLongitudine = 0;
        //    }

        //    string dvRisorse = string.Empty;
        //    using (CAMS.Module.Classi.DB db = new CAMS.Module.Classi.DB())
        //    {
        //        dvRisorse = db.GetRisorsexTask(OidRdL, CAMS.Module.Classi.SetVarSessione.CorrenteUser);
        //    }
        //    var ParCriteria = dvRisorse;
        //    return ParCriteria;
        //}

        public string GetDettaglioRegRdL(string TipoDettaglio, int OidCategoria, object Categoria, string sCodice, object Richiedente, object Descrizione,
      object Scenario, object ClusterEdifici, string sNrRdL)
        {
            string Dettaglio = string.Empty;
            var sCategoria = string.Empty;
            var sRichiedente = string.Empty;
            //var sTipoRichiedente = string.Empty;
            var sClusterEdifici = string.Empty;
            var sScenario = string.Empty;
            var sDescrizione = string.Empty;

            //------------------------

            if (Categoria != null)
            {
                sCategoria = ((Categoria)Categoria).Descrizione;
            }
            if (Descrizione != null)
            {
                sDescrizione = ((string)Descrizione);
            }


            if (Richiedente != null)
            {
                sRichiedente = ((Richiedente)Richiedente).NomeCognome;
            }
            if (Scenario != null)
            {
                sScenario = ((Scenario)Scenario).Descrizione;
            }
            if (ClusterEdifici != null)
            {
                sClusterEdifici = ((ClusterEdifici)ClusterEdifici).Descrizione;
            }

            if (TipoDettaglio == "RegistroRdL")
            {
                switch (OidCategoria)
                {
                    case 1://1	MANUTENZIONE PROGRAMMATA 
                        Dettaglio = string.Format("Codice({0}),Descrizione:{1}, Categoria({2}), Nr RdL({2})", sCodice, sDescrizione, sCategoria, sNrRdL);
                        break;
                    case 2://2	CONDUZIONE 
                    case 3: //3	MANUTENZIONE A CONDIZIONE 
                        Dettaglio = string.Format("Codice({0}),Descrizione:{1}, Categoria({2}), Nr RdL({2})", sCodice, sDescrizione, sCategoria, sNrRdL);
                        break;
                    case 4: //4	MANUTENZIONE GUASTO 
                        Dettaglio = string.Format("Codice({0}),Descrizione:{1}, Categoria({2}), Richiedente({3})", sCodice, sDescrizione, sCategoria, sRichiedente);
                        break;
                    case 5: //5	MANUTENZIONE PROGRAMMATA SPOT
                        Dettaglio = string.Format("Codice({0}),Descrizione:{1}, Categoria({2}), Nr RdL({2})", sCodice, sDescrizione, sCategoria, sNrRdL);
                        break;
                    default:// altro non pervenuto
                        Dettaglio = string.Format("Codice({0}),Descrizione:{1}, Categoria({2})", sCodice, sDescrizione, sCategoria);
                        break;
                }
            }
            else // rdl
            {
                switch (OidCategoria)
                {
                    case 1://1	MANUTENZIONE PROGRAMMATA 
                        Dettaglio = string.Format("Codice({0}),Descrizione:{1}, Categoria({2})", sCodice, sDescrizione, sCategoria);
                        break;
                    case 2://2	CONDUZIONE 
                    case 3: //3	MANUTENZIONE A CONDIZIONE 
                        Dettaglio = string.Format("Codice({0}),Descrizione:{1}, Categoria({2})", sCodice, sDescrizione, sCategoria);
                        break;
                    case 4: //4	MANUTENZIONE GUASTO 
                        Dettaglio = string.Format("Codice({0}),Descrizione:{1}, Categoria({2}), Richiedente({3})", sCodice, sDescrizione, sCategoria, sRichiedente); //, sTipoRichiedente
                        break;
                    case 5: //5	MANUTENZIONE PROGRAMMATA SPOT
                        Dettaglio = string.Format("Codice({0}),Descrizione:{1}, Categoria({2})", sCodice, sDescrizione, sCategoria);
                        break;
                    default:// altro non pervenuto
                        Dettaglio = string.Format("Codice({0}),Descrizione:{1}, Categoria({2})", sCodice, sDescrizione, sCategoria);
                        break;
                }
            }

            return Dettaglio;
        }

        // private XPCollection<StatoSmistamento> fListaFiltraComboSmistamento;// IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
        //public string GetFiltraComboSmistamento(int intSS, Session session)
        //{
        //    //Session session = ((XPObjectSpace)xpObjectSpace).Session;
        //    string ParCriteria = string.Empty;
        //    IList<StatoSmistamentoCombo> IDs = new XPCollection<StatoSmistamentoCombo>(session).Where(Sto => Sto.StatoSmistamento.Oid == intSS).ToList();
        //    string sID = "";
        //    foreach (StatoSmistamentoCombo soi in IDs)
        //    {
        //        if (sID == "")
        //            sID = string.Format("{0}", soi.StatoSmistamentoxCombo.Oid);
        //        else
        //            sID = string.Format("{0},{1}", sID, soi.StatoSmistamentoxCombo.Oid);
        //    }
        //    ParCriteria = string.Format("Oid In ({0})", sID);
        //    return ParCriteria;
        //}


        // private XPCollection<StatoSmistamento> fListaFiltraComboSmistamento;// IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
        //public string GetFiltraComboSOperativo(int intSS, Session session)
        //{
        //    // Session session = ((XPObjectSpace)xpObjectSpace).Session;
        //    string ParCriteria = string.Empty;
        //    IList<StatoSmistamento_SOperativoSO> IDs = new XPCollection<StatoSmistamento_SOperativoSO>(session).Where(Sto => Sto.StatoSmistamento.Oid == intSS).ToList();

        //    string sID = "";
        //    foreach (StatoSmistamento_SOperativoSO soi in IDs)
        //    {
        //        if (sID == "")
        //            sID = string.Format("{0}", soi.StatoOperativoSO.Oid);
        //        else
        //            sID = string.Format("{0},{1}", sID, soi.StatoOperativoSO.Oid);
        //    }
        //    ParCriteria = string.Format("Oid In ({0})", sID);
        //    return ParCriteria;
        //}

        // private XPCollection<StatoSmistamento> fListaFiltraComboSmistamento;// IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
        public string GetFiltroCriteriaText(string oggetto, string searchText)
        {
            string Oidfiltro = string.Empty;
            string StrFiltro = string.Empty;
            if (searchText.Length > 2)
            {
                using (CAMS.Module.Classi.DB db = new CAMS.Module.Classi.DB())
                {

                    DataView dtv = new DataView();
                    dtv = db.GetFiltrobyTesto(oggetto, searchText);
                    if (dtv.Count > 0)
                    {
                        Oidfiltro = string.Join(",", dtv.Cast<DataRowView>().Select(rv => rv.Row["Oid"]));
                        StrFiltro = string.Format("Oid In ({0})", Oidfiltro);
                    }


                }
            }
            return StrFiltro;
        }
        #endregion
        //private void UpdateSetTaskActionState()
        //{
        //    bool isGranted = true;
        //    foreach (object selectedObject in View.SelectedObjects)
        //    {
        //        bool isPriorityGranted = SecuritySystem.IsGranted(new PermissionRequest(
        //             ObjectSpace, typeof(DemoTask), "Priority", selectedObject, SecurityOperations.Write));
        //        bool isStatusGranted = SecuritySystem.IsGranted(new PermissionRequest(
        //            ObjectSpace, typeof(DemoTask), "Status", selectedObject, SecurityOperations.Write));
        //        if (!isPriorityGranted || !isStatusGranted)
        //        {
        //            isGranted = false;
        //        }
        //    }
        //    SetTaskAction.Enabled.SetItemValue("SecurityAllowance", isGranted);
        //}
        public bool GetIsGrantedCreate(IObjectSpace xpObjectSpace, string Classe, string Permesso)
        {
            // private IObjectSpace theObjectSpace;   
            // IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;



            bool isGranted = false;
            switch (Classe)
            {
                case "Immobile":
                    switch (Permesso)
                    {
                        case "C":
                            //isGranted = SecuritySystem.IsGranted(new
                            //     DevExpress.ExpressApp.Security.ClientPermissionRequest(typeof(Immobile),
                            //   null, null, DevExpress.ExpressApp.Security.SecurityOperations.Create));
                            isGranted = SecuritySystem.IsGranted(new
                               DevExpress.ExpressApp.Security.PermissionRequest(xpObjectSpace, typeof(Immobile),
                             DevExpress.ExpressApp.Security.SecurityOperations.Create));
                            break;
                        case "W":
                            //      isGranted = SecuritySystem.IsGranted(new
                            //  DevExpress.ExpressApp.Security.ClientPermissionRequest(typeof(Immobile),
                            //null, null, DevExpress.ExpressApp.Security.SecurityOperations.Write));
                            isGranted = SecuritySystem.IsGranted(new
                              DevExpress.ExpressApp.Security.PermissionRequest(xpObjectSpace, typeof(Immobile),
                            DevExpress.ExpressApp.Security.SecurityOperations.Write));
                            break;
                        default:// altro non pervenuto

                            break;
                    }

                    break;
                case "Impianto":
                    //isGranted = SecuritySystem.IsGranted(new
                    //      DevExpress.ExpressApp.Security.ClientPermissionRequest(typeof(Impianto),
                    //    null, null, DevExpress.ExpressApp.Security.SecurityOperations.Create));
                    isGranted = SecuritySystem.IsGranted(new
                             DevExpress.ExpressApp.Security.PermissionRequest(xpObjectSpace, typeof(Servizio),
                           DevExpress.ExpressApp.Security.SecurityOperations.Create));
                    break;

                case "RdL":
                    isGranted = SecuritySystem.IsGranted(new
                             DevExpress.ExpressApp.Security.PermissionRequest(xpObjectSpace, typeof(RdL),
                           DevExpress.ExpressApp.Security.SecurityOperations.Create));
                    break;

                case "CentroOperativo":
                    //isGranted = SecuritySystem.IsGranted(new
                    //             DevExpress.ExpressApp.Security.ClientPermissionRequest(typeof(CentroOperativo),
                    //           null, null, DevExpress.ExpressApp.Security.SecurityOperations.Write));
                    isGranted = SecuritySystem.IsGranted(new
                            DevExpress.ExpressApp.Security.PermissionRequest(xpObjectSpace, typeof(CentroOperativo),
                          DevExpress.ExpressApp.Security.SecurityOperations.Write));
                    break;
                case "LogEmailCtrlNorm":
                    //isGranted = SecuritySystem.IsGranted(new
                    //             DevExpress.ExpressApp.Security.ClientPermissionRequest(typeof(CAMS.Module.DBControlliNormativi.LogEmailCtrlNorm),
                    //           null, null, DevExpress.ExpressApp.Security.SecurityOperations.Write));
                    isGranted = SecuritySystem.IsGranted(new
                          DevExpress.ExpressApp.Security.PermissionRequest(xpObjectSpace, typeof(CAMS.Module.DBControlliNormativi.LogEmailCtrlNorm),
                        DevExpress.ExpressApp.Security.SecurityOperations.Write));
                    break;
                case "RegNotificheEmergenze":
                    //isGranted = SecuritySystem.IsGranted(new
                    //             DevExpress.ExpressApp.Security.ClientPermissionRequest(typeof( CAMS.Module.DBTask.AppsCAMS.RegNotificheEmergenze),
                    //           null, null, DevExpress.ExpressApp.Security.SecurityOperations.Write));
                    isGranted = SecuritySystem.IsGranted(new
                                            DevExpress.ExpressApp.Security.PermissionRequest(xpObjectSpace, typeof(CAMS.Module.DBTask.AppsCAMS.RegNotificheEmergenze),
                                            DevExpress.ExpressApp.Security.SecurityOperations.Write));
                    break;
                case "ApparatoCaratteristicheTecniche":
                    switch (Permesso)
                    {
                        case "C":
                            isGranted = SecuritySystem.IsGranted(new
                                          DevExpress.ExpressApp.Security.PermissionRequest(xpObjectSpace, typeof(AsettCaratteristicheTecniche),
                                          DevExpress.ExpressApp.Security.SecurityOperations.Create));
 
                            break;
                        case "W":
                            isGranted = SecuritySystem.IsGranted(new
                              DevExpress.ExpressApp.Security.PermissionRequest(xpObjectSpace, typeof(AsettCaratteristicheTecniche),
                            DevExpress.ExpressApp.Security.SecurityOperations.Write));
                            break;
                        default:// altro non pervenuto
                            isGranted = false;
                            break;
                    }

                    break;
                default:// altro non pervenuto  

                    break;
            }

            return isGranted;
        }



        public string GetDettaglioApparatoUltimaLettura(object AppDataLettura, object AppValoreUltimaLettura, object AppOreMedieSetEsercizio)
        {
            string sAppDataLettura = "nd";
            string sAppValoreUltimaLettura = "nd";
            string sAppOreMedieSetEsercizio = "nd";
            if (AppDataLettura != null)
            {
                sAppDataLettura = AppDataLettura.ToString();
            }
            if (AppValoreUltimaLettura != null)
            {
                sAppValoreUltimaLettura = AppValoreUltimaLettura.ToString();
            }

            if (AppOreMedieSetEsercizio != null)
            {
                sAppOreMedieSetEsercizio = AppOreMedieSetEsercizio.ToString();
            }
            return string.Format("Data:{0} ,Valore:{1} ,H Med.Sett. Esercizio:{2}", sAppDataLettura, sAppValoreUltimaLettura, sAppOreMedieSetEsercizio);
        }

        public string GetRdLdaSegnalazione()//object AppDataLettura, object AppValoreUltimaLettura, object AppOreMedieSetEsercizio)
        {
            string sAppDataLettura = "nd";
            string sAppValoreUltimaLettura = "nd";
            string sAppOreMedieSetEsercizio = "nd";


            return " mmmio     ";
            //return string.Format("Data:{0} ,Valore:{1} ,H Med.Sett. Esercizio:{2}", sAppDataLettura, sAppValoreUltimaLettura, sAppOreMedieSetEsercizio);
        }

 
    }
}



//public FileData getFileFromViewId(string viewId)
//{

//    ////SingleChoiceAction StandardShowNavigationItemAction =
//    ////DevExpress.ExpressApp.Frame.GetController<DevExpress.ExpressApp.SystemModule.ShowNavigationItemController>().ShowNavigationItemAction;

//    //IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
//    ////Session session = ((XPObjectSpace) ObjectSpace).Session;           


//    //IObjectSpace xpObjectSpace = theApplication.CreateObjectSpace(); 

//    //HelpConfiguration hp = xpObjectSpace.FindObject<HelpConfiguration>( CriteriaOperator.Parse( string.Format("Viste = {0}", viewId )));   

//    return null; // hp.File;
//}