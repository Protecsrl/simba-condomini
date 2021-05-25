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
using CAMS.Module.DBTask.POI;

namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ListPOIController : ViewController
    {
        public ListPOIController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        private int SelectDefault = 0;
        protected override void OnActivated()
        {
            base.OnActivated();
            if (View.Id == "ListPOI_ListView")
            {
                IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
                #region carica Anno
                var ListAnno = xpObjectSpace.GetObjectsQuery<ListPOI>()
                    .GroupBy(g => new { Anno = g.Anno })
                    .Distinct().ToList();
                int i = 0;
                string filtroAnno = string.Empty;
                string filtroAnnoCaption = string.Empty;
                scafiltroAnno.Items.Clear();
                foreach (var r in ListAnno)
                {
                    if (i == 0)
                        this.scafiltroAnno.Items.Add((
                           new ChoiceActionItem()
                           {
                               Id = "0",
                               Data = "",
                               Caption = "Tutto"
                           }));

                    this.scafiltroAnno.Items.Add((
                        new ChoiceActionItem()
                        {
                            Id = r.Key.Anno.ToString(),
                            Data = string.Format("Anno == '{0}'", r.Key.Anno.ToString()),
                            Caption = r.Key.Anno.ToString()
                        }));

                    SelectDefault = i;
                    filtroAnno = string.Format("Anno == '{0}'", r.Key.Anno.ToString());
                    filtroAnnoCaption = r.Key.Anno.ToString();
                    i++;
                }
                //if (SelectDefault > 0)
                //    scafiltroAnno.SelectedIndex = SelectDefault;

                #endregion
                #region carica Anno
                i = 0;
                SelectDefault = 0;
                List<string> ListEdificio = xpObjectSpace.GetObjectsQuery<ListPOI>().Select(s=>s.Immobile).Distinct().ToList();

                scafiltroEdificio.Items.Clear();
                string filtroEdificio = string.Empty;
                string filtroedificioCaption = string.Empty;
                foreach (var r in ListEdificio)
                {
                    if (i == 0)
                    {
                        this.scafiltroEdificio.Items.Add((
                    new ChoiceActionItem()
                    {
                        Id = "0",
                        Data = string.Format("Immobile == '{0}'", "xxxx"),
                        Caption = "Seleziona un ......"
                    }));

                        filtroEdificio = string.Format("Immobile == '{0}'", "xxxx");
                        filtroedificioCaption = "Seleziona un ......";
                        this.scafiltroEdificio.Items.Add((
                           new ChoiceActionItem()
                           {
                               Id = "1",
                               Data = "",
                               Caption = "Tutto"
                           }));
                    }
                    this.scafiltroEdificio.Items.Add((
                        new ChoiceActionItem()
                        {
                            Id = r,
                            Data = string.Format("Immobile == '{0}'", r),
                            Caption = r
                        }));


                    SelectDefault = i;
                    i++;
                }

                #endregion


            }
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

        private void scafiltroAnno_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {

            ((ListView)View).CollectionSource.BeginUpdateCriteria();
            ((ListView)View).CollectionSource.Criteria.Clear();

            ((ListView)View).CollectionSource.Criteria[e.SelectedChoiceActionItem.Caption] =
               CriteriaEditorHelper.GetCriteriaOperator(
               e.SelectedChoiceActionItem.Data as string, View.ObjectTypeInfo.Type, ObjectSpace);

            ((ListView)View).CollectionSource.EndUpdateCriteria();


        }

        private void scafiltroEdificio_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {

            ((ListView)View).CollectionSource.BeginUpdateCriteria();
            ((ListView)View).CollectionSource.Criteria.Clear();
            string filtro = string.Empty;



            if (this.scafiltroAnno.SelectedIndex == -1)
            {

                filtro = string.Format("{0}", e.SelectedChoiceActionItem.Data as string);
            }
            else
            {
                string filtroAnno = this.scafiltroAnno.SelectedItem.Data.ToString();
                filtro = string.Format("{0} And {1}", e.SelectedChoiceActionItem.Data as string, filtroAnno);
            }

            ((ListView)View).CollectionSource.Criteria[e.SelectedChoiceActionItem.Caption] =
               CriteriaEditorHelper.GetCriteriaOperator(filtro, View.ObjectTypeInfo.Type, ObjectSpace);

            ((ListView)View).CollectionSource.EndUpdateCriteria();

        }
    }
}
