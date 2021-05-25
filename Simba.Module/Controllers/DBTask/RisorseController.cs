using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp.Xpo;
using System.Diagnostics;
using DevExpress.ExpressApp.Security;
using CAMS.Module.Classi;


namespace CAMS.Module.Controllers.DBTask
{
    public partial class RisorseController : ViewController
    {
        bool IsVisibleAssociaaRisorseTeam { get; set; }
        bool IsVisibleCreaRisorsaTeam { get; set; }

        public RisorseController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            if (!GetRisorseTeamIsGranted())
            {
                this.CreaRisorsaTeam.Active.SetItemValue("Active", false);
                this.AssociaaRisorseTeam.Active.SetItemValue("Active", false);
            }
            else
            {
                if (View is ListView)
                {
                    var dv = View as ListView;
                    if (dv.Id == "Risorse_ListView")
                    {
                        CreaRisorsaTeam.Items.Clear();
                    }
                }
                if (View is DetailView)
                {
                    var Dv = (DetailView)View;
                    if (Dv.Id == "Risorse_DetailView")
                    {
                        if (((DevExpress.Xpo.XPObject)(Dv.CurrentObject)).Oid != -1)
                        {
                            this.CreaRisorsaTeam.Active.SetItemValue("Active", true);
                            this.AssociaaRisorseTeam.Active.SetItemValue("Active", true);
                            CaricaComboAnnoCreaRisorseTeam(Dv);
                            CaricaComboAssocia_aRisorseTeam(Dv);
                        }
                    }
                }
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

        private void CreaRisorsaTeam_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var db = new Classi.DB();
            var ListID = new List<int>();
            var Messaggio = string.Empty;
            var Anno = int.Parse(e.SelectedChoiceActionItem.Id.ToUpper());
            if (View is ListView)
            {
                foreach (XPObject obj in View.SelectedObjects)
                {
                    var Id = int.Parse(obj.GetMemberValue("Oid").ToString()) < 0 ? 0 : int.Parse(obj.GetMemberValue("Oid").ToString());
                    if (!ListID.Contains(Id) && Id > 0)
                    {
                        ListID.Add(Id);
                    }
                }

                foreach (var OidRisorsa in ListID)
                {
                    Messaggio = db.CreaRisorsaTeam(OidRisorsa, Anno, CAMS.Module.Classi.SetVarSessione.CorrenteUser);
                }
            }
            if (View is DetailView)
            {
                var Dv = (DetailView)View;
                var Cur = (Risorse)Dv.CurrentObject;
                var OidRisorsa = Cur.Oid;
                Messaggio = db.CreaRisorsaTeam(OidRisorsa, Anno, CAMS.Module.Classi.SetVarSessione.CorrenteUser);

                ConferaRefresh();
                CaricaComboAnnoCreaRisorseTeam(Dv);
            }
            db.Dispose();
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

        private void AssociaaRisorseTeam_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var dv = (DetailView)View;
            var db = new Classi.DB();
            var OidRisorsa = ((Risorse)(dv.CurrentObject)).Oid;
            var OidCapoRisorsaTeam = int.Parse(e.SelectedChoiceActionItem.Id);
            db.CreaCoppiaLinkataConRisorsa(OidCapoRisorsaTeam, OidRisorsa, "user");

            ConferaRefresh();
            CaricaComboAssocia_aRisorseTeam(dv);
        }

        private void CaricaComboAnnoCreaRisorseTeam(DetailView Dv)
        {
            CreaRisorsaTeam.Items.Clear();
            for (var i = -2; i < 8; i++)
            {
                var Anno = (DateTime.Now.Year + i).ToString();

                var AnnoAssRisorseTeam = (Risorse)(Dv.CurrentObject);
                AnnoAssRisorseTeam.Reload();
                AnnoAssRisorseTeam.AssRisorseTeam.Reload();
                var Conta = AnnoAssRisorseTeam.AssRisorseTeam.Where(mm => mm.DataFineValidita.Year.ToString() == Anno).Count();

                if (Conta == 0)
                {
                    CreaRisorsaTeam.Items.Add(
                        (new ChoiceActionItem()
                        {
                            Id = Anno,
                            Caption = Anno,
                            ImageName = "Action_Debug_Step"
                        }));
                }
            }
        }

        private void CaricaComboAssocia_aRisorseTeam(DetailView Dv)
        {
            AssociaaRisorseTeam.Items.Clear();
            var List = new Dictionary<int, string>();
            var OidRisorsa = ((Risorse)(View.CurrentObject)).Oid;
            var db = new Classi.DB();
            List = db.RisorsaCaricaCombo(OidRisorsa, "user");
            db.Dispose();
            foreach (var ele in List)
            {
                AssociaaRisorseTeam.Items.Add((new ChoiceActionItem() { Id = ele.Key.ToString(), Caption = ele.Value.ToString() }));
            }
        }

        private bool GetRisorseTeamIsGranted()
        {
            //bool isGranted = SecuritySystem.IsGranted(new
                   //  DevExpress.ExpressApp.Security.ClientPermissionRequest(typeof(RisorseTeam),
                   //null, null, DevExpress.ExpressApp.Security.SecurityOperations.Create));

            bool isGranted = SecuritySystem.IsGranted(new PermissionRequest(ObjectSpace,
           typeof(RisorseTeam), SecurityOperations.Create, null, null));

            this.IsVisibleAssociaaRisorseTeam = isGranted;
            this.IsVisibleCreaRisorsaTeam = isGranted;

            return isGranted;
        }

       

      

    }
}










///  crea dettaglio vista











