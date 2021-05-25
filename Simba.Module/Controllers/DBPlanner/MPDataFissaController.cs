using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using CAMS.Module.DBPlanner;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Diagnostics;
using DevExpress.Data.Filtering;
using CAMS.Module.DBPlant;

namespace CAMS.Module.Controllers.DBPlanner
{
    public partial class MPDataFissaController : ViewController
    {
        public MPDataFissaController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            //Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().LinkAction.Active.SetItemValue("LinkAction", false);
            //Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().UnlinkAction.Active.SetItemValue("UnlinkAction", false);
            fillCopiaDa();


        }


        private void fillCopiaDa()
        {
            acCopia_da.Items.Clear();
            this.acCopiadaApparato.Active.SetItemValue("Active", false);
            this.acCopiadaImpianto.Active.SetItemValue("Active", false);
            this.acCopiadaEdificio.Active.SetItemValue("Active", false);
            if (View is DetailView)
            {
                if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                {
                    MPDataFissa vMPDataFissaOld = (MPDataFissa)View.CurrentObject;
                    if (vMPDataFissaOld.Oid != -1)
                    {
                        IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                        string filtro = string.Format("Apparato.Oid = {0}", vMPDataFissaOld.Asset.Oid);

                        //int Conta = xpObjectSpace.GetObjects<MPDataFissa>(CriteriaOperator.Parse(filtro)).Count;
                        int Conta = vMPDataFissaOld.ListaComboAppSK.Count();
                        if (Conta > 0)
                            this.acCopiadaApparato.Active.SetItemValue("Active", true);
                        //this.acCopia_da.Items.Add((new ChoiceActionItem() { Id = "MPDataFissa_DetailView_Pianifica", Data = filtro, Caption = "Stesso Apparato" }));

                        filtro = string.Format("Impianto.Oid = {0}", vMPDataFissaOld.Servizio.Oid);
                        Conta = vMPDataFissaOld.ListaComboApp.Count();
                        if (Conta > 0)
                            this.acCopiadaImpianto.Active.SetItemValue("Active", true);
                            //this.acCopia_da.Items.Add((new ChoiceActionItem() { Id = "MPDataFissa_DetailView_Pianifica", Data = filtro, Caption = "Stesso Impianto" }));

                        filtro = string.Format("Immobile.Oid = {0}", vMPDataFissaOld.Immobile.Oid);
                        Conta = vMPDataFissaOld.ListaComboImpianto.Count();
                        if (Conta > 0)
                            this.acCopiadaEdificio.Active.SetItemValue("Active", true);
                            //this.acCopia_da.Items.Add((new ChoiceActionItem() { Id = "MPDataFissa_DetailView_Pianifica", Data = filtro, Caption = "Stesso Immobile" }));

                        //filtro = string.Format("ClusterEdifici.Oid = {0}", vMPDataFissaOld.ClusterEdifici.Oid);
                        //Conta = vMPDataFissaOld.ListaComboEdificio.Count();
                        //if (Conta > 0)
                        //    this.acCopia_da.Items.Add((new ChoiceActionItem() { Id = "MPDataFissa_DetailView_Pianifica", Data = filtro, Caption = "Stesso Cluster Edifici" }));
                    }
                }
                PropertyEditor lookupPropertyEditor = (PropertyEditor)((DetailView)View).FindItem("ApparatoSchedaMP");
                // lookupPropertyEditor.
                CustomAttribute customAttribute = lookupPropertyEditor.MemberInfo.FindAttribute<CustomAttribute>();
            }
        }


        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        private void MPDataFissaController_ViewControlsCreated(object sender, EventArgs e)
        {
            //if (View is ListView)
            //{
            //    if (View.Id == "RegPianificazioneMP_MPDFissas_ListView_Pianifica")
            //    {
            //        RefreshDati();
            //    }
            //}
            Debug.WriteLine(" Conferma ");
        }
    

        private void selDATA_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    if (View.Id == "MPDataFissa_DetailView_Pianifica")
                    {

                        if (e.ParameterCurrentValue == null)
                        {
                            var dv = View as DetailView;
                            if (dv.ViewEditMode == ViewEditMode.Edit)
                            {
                                var df = (MPDataFissa)dv.CurrentObject;
                                while (df.MPDataFissaDettaglio.Count > 0)
                                {
                                    df.MPDataFissaDettaglio.Remove(df.MPDataFissaDettaglio[0]);
                                }
                                df.Data = new DateTime();
                                df.Save();
                            }
                        }
                        else
                        {
                            var paramValue = (DateTime)e.ParameterCurrentValue;
                            var dv = View as DetailView;
                            if (dv.ViewEditMode == ViewEditMode.Edit)
                            {
                                var df = (MPDataFissa)dv.CurrentObject;
                                df.Data = paramValue;
                                while (df.MPDataFissaDettaglio.Count > 0)
                                {
                                    df.MPDataFissaDettaglio.Remove(df.MPDataFissaDettaglio[0]);
                                }
                                /// recupero se è una sola data
                                int cadenza = 0;
                                if (int.TryParse(df.ApparatoSchedaMP.FrequenzaOpt.CadenzeAnno.ToString(), out cadenza))
                                {
                                    if (cadenza == 1 || cadenza == 0 || cadenza < 1) // è annuale (cadenza < 1) // è pluriennale dipende dal periodo della commessa
                                    {
                                        // inserisco la data e basta
                                        df.MPDataFissaDettaglio.Add(new MPDataFissaDettaglio(Sess)
                                                {
                                                    MPDataFissa = df,
                                                    Asset = df.Asset,
                                                    ApparatoSchedaMP = df.ApparatoSchedaMP,
                                                    Data = df.Data
                                                }
                                            );
                                    }
                                    else if (cadenza > 1) // è tra giornaliera e sotto annualità
                                    {
                                        System.Data.DataView dvDate = new System.Data.DataView();
                                        dvDate = CalcolaDettaglio(df.Data, df.Asset.Oid, df.ApparatoSchedaMP.SchedaMp.Oid);
                                        foreach (System.Data.DataRowView Row in dvDate)
                                        {
                                            DateTime data = (DateTime)Row["data"];
                                            df.MPDataFissaDettaglio.Add(new MPDataFissaDettaglio(Sess)
                                            {
                                                MPDataFissa = df,
                                                Asset = df.Asset,
                                                ApparatoSchedaMP = df.ApparatoSchedaMP,
                                                Data = data
                                            }
                                           );
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private System.Data.DataView CalcolaDettaglio(DateTime Data, int Apparato_Oid, int SchedaMp_Oid)
        {
            var dv = new System.Data.DataView();
            using (Classi.DB db = new Classi.DB())
            {
                dv = db.SetDataFissaDettaglio(Data, Apparato_Oid, SchedaMp_Oid);

            }
            return dv;
        }

        private void acCopia_da_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {

            //IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    MPDataFissa vMPDataFissaOld = (MPDataFissa)View.CurrentObject;
                    string OggettoDa = e.SelectedChoiceActionItem.Data.ToString();
                    string DView = e.SelectedChoiceActionItem.Id.ToString();
                    var vMPDataFissaNew = xpObjectSpace.CreateObject<MPDataFissa>();
                    if (OggettoDa.Contains("Asset"))
                    {
                        vMPDataFissaNew.RegPianificazioneMP = xpObjectSpace.GetObject<RegPianificazioneMP>(vMPDataFissaOld.RegPianificazioneMP);
                        vMPDataFissaNew.Asset = xpObjectSpace.GetObject<Asset>(vMPDataFissaOld.Asset);
                        vMPDataFissaNew.Servizio = xpObjectSpace.GetObject<Servizio>(vMPDataFissaOld.Servizio);
                        vMPDataFissaNew.Immobile = xpObjectSpace.GetObject<Immobile>(vMPDataFissaOld.Immobile);
                        vMPDataFissaNew.ClusterEdifici = xpObjectSpace.GetObject<ClusterEdifici>(vMPDataFissaOld.ClusterEdifici);
                    }
                    else if (OggettoDa.Contains("Servizio"))
                    {
                        vMPDataFissaNew.RegPianificazioneMP = xpObjectSpace.GetObject<RegPianificazioneMP>(vMPDataFissaOld.RegPianificazioneMP);
                        vMPDataFissaNew.Servizio = xpObjectSpace.GetObject<Servizio>(vMPDataFissaOld.Servizio);
                        vMPDataFissaNew.Immobile = xpObjectSpace.GetObject<Immobile>(vMPDataFissaOld.Immobile);
                        vMPDataFissaNew.ClusterEdifici = xpObjectSpace.GetObject<ClusterEdifici>(vMPDataFissaOld.ClusterEdifici);
                    }
                    else if (OggettoDa.Contains("Immobile"))
                    {
                        vMPDataFissaNew.RegPianificazioneMP = xpObjectSpace.GetObject<RegPianificazioneMP>(vMPDataFissaOld.RegPianificazioneMP);
                        vMPDataFissaNew.Immobile = xpObjectSpace.GetObject<Immobile>(vMPDataFissaOld.Immobile);
                        vMPDataFissaNew.ClusterEdifici = xpObjectSpace.GetObject<ClusterEdifici>(vMPDataFissaOld.ClusterEdifici);
                    }
                    else if (OggettoDa.Contains("ClusterEdifici"))
                    {
                        vMPDataFissaNew.RegPianificazioneMP = xpObjectSpace.GetObject<RegPianificazioneMP>(vMPDataFissaOld.RegPianificazioneMP);
                        vMPDataFissaNew.ClusterEdifici = xpObjectSpace.GetObject<ClusterEdifici>(vMPDataFissaOld.ClusterEdifici);
                    }

                    var view = Application.CreateDetailView(xpObjectSpace, DView, true, vMPDataFissaNew);
                    view.ViewEditMode = ViewEditMode.Edit;

                    e.ShowViewParameters.CreatedView = view;
                    e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;

                    // var dc = Application.CreateController<DialogController>();
                    //dc.Actions.Add(
                    //  e.ShowViewParameters.Controllers.Add(dc);

                    //e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                    //e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                    //e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                }
                fillCopiaDa();
            }


        }

        private void acCopiadaApparato_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Copia_da("Apparato", e);
        }

        private void acCopiadaImpianto_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Copia_da("Impianto", e);
        }

        private void acCopiadaEdificio_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Copia_da("Immobile", e);
        }


        private void Copia_da(string OggettoDa, SimpleActionExecuteEventArgs e)
        {
            //IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    MPDataFissa vMPDataFissaOld = (MPDataFissa)View.CurrentObject;
                    //string OggettoDa = e.SelectedChoiceActionItem.Data.ToString();
                    string DView = "MPDataFissa_DetailView_Pianifica";//e.SelectedChoiceActionItem.Id.ToString();
                    var vMPDataFissaNew = xpObjectSpace.CreateObject<MPDataFissa>();
                    if (OggettoDa.Contains("Apparato"))
                    {
                        vMPDataFissaNew.RegPianificazioneMP = xpObjectSpace.GetObject<RegPianificazioneMP>(vMPDataFissaOld.RegPianificazioneMP);
                        vMPDataFissaNew.Asset = xpObjectSpace.GetObject<Asset>(vMPDataFissaOld.Asset);
                        vMPDataFissaNew.Servizio = xpObjectSpace.GetObject<Servizio>(vMPDataFissaOld.Servizio);
                        vMPDataFissaNew.Immobile = xpObjectSpace.GetObject<Immobile>(vMPDataFissaOld.Immobile);
                        vMPDataFissaNew.ClusterEdifici = xpObjectSpace.GetObject<ClusterEdifici>(vMPDataFissaOld.ClusterEdifici);
                    }
                    else if (OggettoDa.Contains("Impianto"))
                    {
                        vMPDataFissaNew.RegPianificazioneMP = xpObjectSpace.GetObject<RegPianificazioneMP>(vMPDataFissaOld.RegPianificazioneMP);
                        vMPDataFissaNew.Servizio = xpObjectSpace.GetObject<Servizio>(vMPDataFissaOld.Servizio);
                        vMPDataFissaNew.Immobile = xpObjectSpace.GetObject<Immobile>(vMPDataFissaOld.Immobile);
                        vMPDataFissaNew.ClusterEdifici = xpObjectSpace.GetObject<ClusterEdifici>(vMPDataFissaOld.ClusterEdifici);
                    }
                    else if (OggettoDa.Contains("Immobile"))
                    {
                        vMPDataFissaNew.RegPianificazioneMP = xpObjectSpace.GetObject<RegPianificazioneMP>(vMPDataFissaOld.RegPianificazioneMP);
                        vMPDataFissaNew.Immobile = xpObjectSpace.GetObject<Immobile>(vMPDataFissaOld.Immobile);
                        vMPDataFissaNew.ClusterEdifici = xpObjectSpace.GetObject<ClusterEdifici>(vMPDataFissaOld.ClusterEdifici);
                    }
                    else if (OggettoDa.Contains("ClusterEdifici"))
                    {
                        vMPDataFissaNew.RegPianificazioneMP = xpObjectSpace.GetObject<RegPianificazioneMP>(vMPDataFissaOld.RegPianificazioneMP);
                        vMPDataFissaNew.ClusterEdifici = xpObjectSpace.GetObject<ClusterEdifici>(vMPDataFissaOld.ClusterEdifici);
                    }

                    var view = Application.CreateDetailView(xpObjectSpace, DView, true, vMPDataFissaNew);
                    view.ViewEditMode = ViewEditMode.Edit;

                    e.ShowViewParameters.CreatedView = view;
                    e.ShowViewParameters.TargetWindow = TargetWindow.Current;

                    // var dc = Application.CreateController<DialogController>();
                    //dc.Actions.Add(
                    //  e.ShowViewParameters.Controllers.Add(dc);

                    //e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                    //e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                    //e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                }
                fillCopiaDa();
            }


        }


    }
}
