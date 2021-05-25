using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;

using DevExpress.Xpo;
using CAMS.Module.DBTask;
using System.Collections;
using CAMS.Module.Classi;
using DevExpress.ExpressApp.Xpo;

namespace CAMS.Module.Controllers.DBTask
{
    public partial class RisorseTeamController : ViewController
    {
        public RisorseTeamController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            CreaCoppiaLinkata.Active["Disattiva_CreaCoppiaLinkata"] = false;
            if (View is DetailView)
            {
                var Dv = (DetailView)View;
                if (Dv.Id.Contains("RisorseTeam_DetailView") )
                {
                    if (((DevExpress.Xpo.XPObject)(Dv.CurrentObject)).Oid != -1)
                    {
                        var OidRisorseTeam = ((RisorseTeam)(View.CurrentObject)).Oid;
                        var db = new Classi.DB();
                        var List = new Dictionary<int, string>();

                        List = db.RisorsaTeamCaricaCombo(OidRisorseTeam, "user");
                        db.Dispose();

                        foreach (var ele in List)
                        {
                            CreaCoppiaLinkataPar.Items.Add((new ChoiceActionItem() { Id = ele.Key.ToString(), Caption = ele.Value.ToString() }));
                        }
                    }
                }
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            Frame.GetController<DevExpress.ExpressApp.SystemModule.DeleteObjectsViewController>().Actions["Delete"].Active["TRVisibleDelete_DView"] = false;
            CreaCoppiaLinkataPar.Active["disCreaCoppiaLinkataPar"] = false;
            RilasciaRisorse.Active["disRilasciaRisorse"] = false;
            if (View is DetailView)
            {
                var Dv = (DetailView)View;
                if (Dv.Id.Contains("RisorseTeam_DetailView"))
                {
                    CaricaRisorsaTeamCaricaCombo(Dv);
                }
                if (((DevExpress.Xpo.XPObject)(Dv.CurrentObject)).Oid != -1)
                {
                    if (((RisorseTeam)(View.CurrentObject)).Ghost == null)
                    {
                        var CLkRisorseTeam = ((RisorseTeam)(View.CurrentObject)).CoppiaLinkata.ToString();
                        if (CLkRisorseTeam == "No")
                        {
                            Frame.GetController<DevExpress.ExpressApp.SystemModule.DeleteObjectsViewController>().Actions["Delete"].Active["TRVisibleDelete_DView"] = true;
                            CreaCoppiaLinkataPar.Active["disCreaCoppiaLinkataPar"] = true;
                        }

                        if (CLkRisorseTeam == "Si")
                        {
                            Frame.GetController<DevExpress.ExpressApp.SystemModule.DeleteObjectsViewController>().Actions["Delete"].Active["TRVisibleDelete_DView"] = true;
                            RilasciaRisorse.Active["disRilasciaRisorse"] = true;
                        }

                        if (CLkRisorseTeam == "NonDefinito")
                        {
                            Frame.GetController<DevExpress.ExpressApp.SystemModule.DeleteObjectsViewController>().Actions["Delete"].Active["TRVisibleDelete_DView"] = true;
                            CreaCoppiaLinkataPar.Active["disCreaCoppiaLinkataPar"] = false;
                            RilasciaRisorse.Active["disRilasciaRisorse"] = false;
                        }
                    }
                }
            }
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        private void RilasciaRisorse_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var db = new Classi.DB();
            var Messaggio = "Avviso";
            var os = Application.CreateObjectSpace();
            if (View is DetailView)
            {
                var Dv = (DetailView)View;
                var Cur = (RisorseTeam)Dv.CurrentObject;
                var OidRisorsaTeam = Cur.Oid;
                os.CommitChanges();
                Messaggio = db.RilasciaRisorse(OidRisorsaTeam, CAMS.Module.Classi.SetVarSessione.CorrenteUser);
                ConferaRefresh();
                Frame.View.Refresh();
                CreaCoppiaLinkataPar.Active["disCreaCoppiaLinkataPar"] = true;
                RilasciaRisorse.Active["disRilasciaRisorse"] = false;
                CaricaRisorsaTeamCaricaCombo(  Dv);
            }
            else
            {
                foreach (XPLiteObject obj in View.SelectedObjects)
                {
                    var OidRisorsaTeam = int.Parse(obj.GetMemberValue("Oid").ToString()) < 0 ? 0 : int.Parse(obj.GetMemberValue("Oid").ToString());
                    Messaggio = db.RilasciaRisorse(OidRisorsaTeam, CAMS.Module.Classi.SetVarSessione.CorrenteUser);
                }
            }
        }

        private void CreaCoppiaLinkata_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var os = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            var objMPPBLListSelects = new ArrayList(e.SelectedObjects);
            var listViewId = Application.FindListViewId(typeof(Risorse));
            foreach (Object objList in objMPPBLListSelects)
            {
                System.Diagnostics.Debug.WriteLine(objList.ToString());
                var Nome = ((RisorseTeam)(objList)).RisorsaCapo.Nome.ToString();
                var Cognome = ((RisorseTeam)(objList)).RisorsaCapo.Cognome.ToString();
                var CaptionListView = string.Format(" ( Crea Coppia Lincata COn la Risorsa Capo: {0},{1})", Nome, Cognome);

                var db = new DB();
                var OIdRisorsa = ((RisorseTeam)(objList)).RisorsaCapo.Oid;
                var dvRisorse = db.CreaCoppiaLinkata(OIdRisorsa, CAMS.Module.Classi.SetVarSessione.CorrenteUser);
                var clRisorse = (CollectionSource)Application.CreateCollectionSource(os, typeof(Risorse), listViewId);
                var ParCriteria = string.Format("Oid In ({0})", dvRisorse);
                clRisorse.Criteria["FiltroclRisorse"] = CriteriaOperator.Parse(ParCriteria);
                e.ShowViewParameters.CreatedView = Application.CreateListView(listViewId, clRisorse, true);
                e.ShowViewParameters.CreatedView.Caption = e.ShowViewParameters.CreatedView.Caption + CaptionListView;
                e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                e.ShowViewParameters.CreateAllControllers = true;
                var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();

                CAMS.Module.Classi.SetVarSessione.OidSel = OIdRisorsa;
                e.ShowViewParameters.Controllers.Add(dc);
            }
        }

        private void CreaCoppiaLinkataPar_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var dv = (DetailView)View;
            var db = new Classi.DB();
            var OidRisorsaTeam = ((RisorseTeam)(dv.CurrentObject)).Oid;
            var OidRisorsa = int.Parse(e.SelectedChoiceActionItem.Id);
            db.CreaCoppiaLinkataConRisorsa(OidRisorsaTeam, OidRisorsa, "user");
            ConferaRefresh();
            CreaCoppiaLinkataPar.Active["disCreaCoppiaLinkataPar"] = false;
            RilasciaRisorse.Active["disRilasciaRisorse"] = true;
        }

        private void ConferaRefresh()
        {
            ObjectSpace.Refresh();
            if (View is DetailView)
            {
                View.ObjectSpace.ReloadObject(View.CurrentObject);
            }
            else
            {
                                (View as DevExpress.ExpressApp.ListView).CollectionSource.Reload();
            }
            View.Refresh();
        }

        private void CaricaRisorsaTeamCaricaCombo(DetailView Dv)
        {
            CreaCoppiaLinkataPar.Items.Clear();

            if (Dv.Id.Contains("RisorseTeam_DetailView"))
            {
                if (((DevExpress.Xpo.XPObject)(Dv.CurrentObject)).Oid != -1)
                {
                    var OidRisorseTeam = ((RisorseTeam)(View.CurrentObject)).Oid;
                    var db = new Classi.DB();
                    var List = new Dictionary<int, string>();

                    List = db.RisorsaTeamCaricaCombo(OidRisorseTeam, "user");
                    db.Dispose();

                    foreach (var ele in List)
                    {
                        CreaCoppiaLinkataPar.Items.Add((new ChoiceActionItem() { Id = ele.Key.ToString(), Caption = ele.Value.ToString() }));
                    }
                }
            }
        }

        private void RisorseTeamController_ViewControlsCreated(object sender, EventArgs e)
        {
            Frame.GetController<DevExpress.ExpressApp.SystemModule.DeleteObjectsViewController>().Actions["Delete"].Active["TRVisibleDelete_DView"] = false;
            CreaCoppiaLinkataPar.Active["disCreaCoppiaLinkataPar"] = false;
            RilasciaRisorse.Active["disRilasciaRisorse"] = false;
            if (View is DetailView)
            {
                var Dv = (DetailView)View;
                if (Dv.Id.Contains("RisorseTeam_DetailView"))
                {
                    CaricaRisorsaTeamCaricaCombo(Dv);
                }
                if (((DevExpress.Xpo.XPObject)(Dv.CurrentObject)).Oid != -1)
                {
                    if (((RisorseTeam)(View.CurrentObject)).Ghost == null)
                    {
                        var CLkRisorseTeam = ((RisorseTeam)(View.CurrentObject)).CoppiaLinkata.ToString();
                        if (CLkRisorseTeam == "No")
                        {
                            Frame.GetController<DevExpress.ExpressApp.SystemModule.DeleteObjectsViewController>().Actions["Delete"].Active["TRVisibleDelete_DView"] = true;
                            CreaCoppiaLinkataPar.Active["disCreaCoppiaLinkataPar"] = true;
                        }

                        if (CLkRisorseTeam == "Si")
                        {
                            Frame.GetController<DevExpress.ExpressApp.SystemModule.DeleteObjectsViewController>().Actions["Delete"].Active["TRVisibleDelete_DView"] = true;
                            RilasciaRisorse.Active["disRilasciaRisorse"] = true;
                        }

                        if (CLkRisorseTeam == "NonDefinito")
                        {
                            Frame.GetController<DevExpress.ExpressApp.SystemModule.DeleteObjectsViewController>().Actions["Delete"].Active["TRVisibleDelete_DView"] = true;
                            CreaCoppiaLinkataPar.Active["disCreaCoppiaLinkataPar"] = false;
                            RilasciaRisorse.Active["disRilasciaRisorse"] = false;
                        }
                    }
                }
            }
        }
    }
}
