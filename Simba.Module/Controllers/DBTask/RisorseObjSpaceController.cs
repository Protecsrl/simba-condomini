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
using CAMS.Module.Classi;
using CAMS.Module.DBAgenda;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBAngrafica;
using CAMS.Module.DBAux;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask.AppsCAMS;
using CAMS.Module.DBTask.Guasti;
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.Data.Filtering' è già presente in questo spazio dei nomi
using DevExpress.Data.Filtering;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.Data.Filtering' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.ExpressApp' è già presente in questo spazio dei nomi
using DevExpress.ExpressApp;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.ExpressApp' è già presente in questo spazio dei nomi
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
using System.Collections.Generic;
#pragma warning restore CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
using System.ComponentModel;
using System.Diagnostics;
#pragma warning disable CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
using System.Linq;
#pragma warning restore CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi

namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RisorseObjSpaceController : ViewController
    {
        public RisorseObjSpaceController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            //View.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
            View.ObjectSpace.Committing += ObjectSpace_Committing;
            //View.ObjectSpace.Committed += ObjectSpace_Committed;
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            try { View.ObjectSpace.Committing -= ObjectSpace_Committing; }
            catch { }
            //try { View.ObjectSpace.Committed -= ObjectSpace_Committed; }
            //catch { }
            //try { View.ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged; }
            //catch { }
            base.OnDeactivated();
        }

        //private void SalvaRisorsa()
        //{
        //    string Messaggio = string.Empty;
        //    if (View is DetailView)
        //    {
        //        DetailView Dv = View as DetailView;
        //        if (Dv.Id.Contains("Risorse_DetailView") && ((DetailView)Dv).ObjectTypeInfo.Type == typeof(Risorse))
        //        {
        //            Risorse Ris = Dv.CurrentObject as Risorse;
        //            if (Ris != null)
        //                if (Ris.AssRisorseTeam.Count == 0)
        //                {
        //                    /// sistema di refresh FUNZIONATE !!!!!!!!!!!!!!!!!
        //                    //  View.ObjectSpace.CommitChanges();
        //                    using (CAMS.Module.Classi.DB db = new CAMS.Module.Classi.DB())
        //                    {
        //                        Messaggio = db.CreaRisorsaTeam(Ris.Oid, DateTime.Now.Year, CAMS.Module.Classi.SetVarSessione.CorrenteUser);
        //                    }
        //                    View.ObjectSpace.Refresh();
        //                }
        //        }
        //    }
        //}

        void ObjectSpace_Committing(object sender, CancelEventArgs e)
        {
            IObjectSpace os = (IObjectSpace)sender;
            for (int i = os.ModifiedObjects.Count - 1; i >= 0; i--)
            {
                object item = os.ModifiedObjects[i];
                if (typeof(Risorse).IsAssignableFrom(item.GetType()))
                {
                    Risorse Risorsa = (Risorse)item;
                    //var xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                    //Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                    if (Risorsa.Oid == -1)
                    {
                        if (Risorsa.AssRisorseTeam.Count == 0 && (Risorsa.TipoQualifica == TipoQualifica.Operaio || Risorsa.TipoQualifica == TipoQualifica.Operaio_Impiegato))
                        {
                            RisorseTeam rt = os.CreateObject<RisorseTeam>();
                            rt.Anno = DateTime.Now.Year;
                            //rt.CentroOperativo = Risorsa.CentroOperativo;
                            rt.RisorsaCapo = Risorsa;
                            rt.Save();
                            //rt.CoppiaLinkata = TipoNumeroManutentori.unaPersona
                            Risorsa.AssRisorseTeam.Add(new AssRisorseTeam(Risorsa.Session)
                            {
                                Risorsa = Risorsa,
                                Team = rt,
                                DataInizioValidita = new DateTime(DateTime.Now.Year, 1, 1),
                                DataFineValidita = new DateTime(DateTime.Now.Year,12, 31),
                                TipoAssociazione = TipoAssRisorseTeam.Ordinaria
                            });
                            Risorsa.Save();
                            //using (CAMS.Module.Classi.DB db = new CAMS.Module.Classi.DB())
                            //{
                            //    Messaggio = db.CreaRisorsaTeam(Ris.Oid, DateTime.Now.Year, CAMS.Module.Classi.SetVarSessione.CorrenteUser);
                            //}
                            //View.ObjectSpace.Refresh();                        }
                        }
                    }
                }
            }
        }


    }
}
