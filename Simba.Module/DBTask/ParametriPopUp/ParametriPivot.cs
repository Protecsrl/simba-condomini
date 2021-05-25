using CAMS.Module.DBAux;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask.Vista;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace CAMS.Module.DBTask.ParametriPopUp
{
    [DefaultClassOptions, Persistent("PARAMETRIPIVOT")]
    [VisibleInDashboards(false)]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Parametri Pivot")]
    [ImageName("Time")]
    [NavigationItem("Tabelle Anagrafiche")]
    [RuleCriteria("ParametriPivot.ControlloDate", DefaultContexts.Save, @"[Data_DA] < [Data_A]",
   CustomMessageTemplate = "La data DA deve essere minore della data A!",
   SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Error)]
    public class ParametriPivot : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public ParametriPivot() : base() { }
        public ParametriPivot(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {
                if (this.Session.IsNewObject(this))
                {
                    this.SecurityUser =
                        this.Session.GetObjectByKey<SecuritySystemUser>
                        (DevExpress.ExpressApp.SecuritySystem.CurrentUserId);
                    //SecurityUser./*UserName*/ //FilteringCriterion.Description
                }
            }
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (IsLoading) return;
            if (propertyName == "FilteringCriterionCommessa")
            {
                FilteringCriterion flt = (FilteringCriterion)newValue;
                if (flt.Criterion != null)
                {
                    DescrizioneFiltroCommesse = flt.DescrizioneFiltro;
                    //XPView view2 = new XPView(Session, typeof(Commesse));
                    //view2.Properties.AddRange(new ViewProperty[] { new ViewProperty("[Descrizione]", SortDirection.Ascending, "[Descrizione]", true, true), });
                    //string filtro = flt.Criterion.Replace("OidCommessa", "Oid");
                    //view2.Criteria = CriteriaOperator.Parse(filtro);
                    //foreach (ViewRecord r in view2)
                    //{
                    //    DescrizioneFiltroCommesse = DescrizioneFiltroCommesse + r[0] + "; ";
                    //}
                }
            }
        }

        #region filtro per COMMESSA   
        private FilteringCriterion fFilteringCriterionCommessa;
        [Persistent("FILTROCOMMESSA")]
        [Index(3), XafDisplayName("Filtro Commessa")]
        //[DataSourceCriteria("SecurityUser.UserName = 'sysfilter' And IsExactType(myObjectType, 'CAMS.Module.DBTask.Vista.RdLListViewSinottico')")]
        [DataSourceProperty("ListFilteringCriterionCommessa", DataSourcePropertyIsNullMode.SelectAll)]
        [ToolTipAttribute("impostare il filtro COMMESSE.")]
        [ImmediatePostData(true)]
        public FilteringCriterion FilteringCriterionCommessa
        {
            get { return fFilteringCriterionCommessa; }
            set { SetPropertyValue("FilteringCriterionCommessa", ref fFilteringCriterionCommessa, value); }
        }

        #region filtro list di criteriofilter COMMESSA      ListFilteringCriterionCommessa
        private List<FilteringCriterion> _ListFilteringCriterionCommessa = null;
        protected IList<FilteringCriterion> ListFilteringCriterionCommessa
        {
            get
            {
                if (!IsLoading)
                    if (_ListFilteringCriterionCommessa == null)
                    {
                        //FilteringCriterion.Description Description
                        _ListFilteringCriterionCommessa = new XPQuery<FilteringCriterion>(Session)
                                .Where(w => w.myObjectType == typeof(RdLListViewSinottico))
                                .Where(w => w.SecurityUser.UserName == this.SecurityUser.UserName || w.SecurityUser.UserName == "sysfilter")
                                   .Where(w => w.Criterion.Contains("OidCommessa") || w.Criterion == null)
                                .ToList();
                    }
                return _ListFilteringCriterionCommessa;
            }
        }

        [Persistent("FILTROCOMMESSE"), XafDisplayName("Descrizione Filtro Commesse")]
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string DescrizioneFiltroCommesse
        {
            get { return GetPropertyValue<string>("DescrizioneFiltroCommesse"); }
            set { SetPropertyValue<string>("DescrizioneFiltroCommesse", value); }
        }

        [NonPersistent, Size(50)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false), VisibleInDashboards(false), VisibleInReports(false)]
        [ImmediatePostData(true)]
        public string NuovoFiltro
        {
            get;
            set;
        }
        #endregion

        #region FILTRO DELLE DATE
        private DateTime fData_DA;
        [Persistent("DATA_DA"), XafDisplayName("Data inizio Selezione Dati")]
        [Index(1)]   //[ RuleRequiredField(DefaultContexts.Save)]  Iif(RdL is not null,2,RdL.UltimoStatoSmistamento.Oid)=2""FilteringCriterion.Description != 'Personalizzato'
        //[Appearance("ParametriPivot.DATA_DA.Enabled", AppearanceItemType.ViewItem, "Iif(FilteringCriterionDATE is not null,FilteringCriterionDATE.Description,null) != 'Personalizzato'", Enabled = false)]
        [ToolTipAttribute("impostare qui la data di fine dell'elaborazione dei dati di caricamento Pivot.")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData(true)]
        public DateTime Data_DA
        {
            get { return fData_DA; }
            set { SetPropertyValue("Data_DA", ref fData_DA, value); }
        }

        private DateTime fData_A;
        [Persistent("DATA_A"), XafDisplayName("Data fine Selezione Dati")]
        [Index(2)]
        //[Appearance("ParametriPivot.DATA_A.Enabled", AppearanceItemType.ViewItem, "Iif(FilteringCriterionDATE is not null,FilteringCriterionDATE.Description,null) != 'Personalizzato'", Enabled = false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ToolTipAttribute("impostare qui la data di inizio dell'elaborazione dei dati di caricamento Pivot.")]
        [ImmediatePostData(true)]
        public DateTime Data_A
        {
            get { return fData_A; }
            set { SetPropertyValue("Data_A", ref fData_A, value); }
        }
        #endregion

        //    'CAMS.Module.DBTask.Vista.RdLListViewSinottico' = myObjectType       //[DataSourceCriteriaProperty()]       '@This.Oid'         //[DataSourceProperty("PostponeTimeList")]
        //AND ObjectType == getObjectType()
        [Persistent("SECURITYUSERID"), XafDisplayName("Security User")]
        [RuleUniqueValue("ParametriPivot.SecuritySystemUser.", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        [Delayed(true)]
        [Browsable(false)]
        public SecuritySystemUser SecurityUser
        {
            get { return GetDelayedPropertyValue<SecuritySystemUser>("SecurityUser"); }
            set { SetDelayedPropertyValue<SecuritySystemUser>("SecurityUser", value); }

        }
        #endregion

        bool mioflag = false;
        #region COLLECTION - AZIONI DI ELABORAZIONE DELLE PIVOT   , Category = ""Category="",
        //[Action(Caption = "Carica Pivot",ImageName = "Demo_ListEditors_PivotGrid_32x32",ToolTip ="Carica Pivot", ConfirmationMessage = "Qusta attività richiederà qualche secondo, sei sicuro che vuoi procedere?",  AutoCommit = true)]
        public void ActionMethodCarica()
        {
            // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
            _ListViewSinottico = new XPCollection<RdLListViewSinottico>(Session);
            _ListViewSinottico.BindingBehavior = CollectionBindingBehavior.AllowNone;
            GroupOperator criteria = new GroupOperator(GroupOperatorType.And);

            if (this.FilteringCriterionCommessa != null)
                if (!string.IsNullOrEmpty(this.FilteringCriterionCommessa.Criterion))
                {
                    CriteriaOperator opCommessa = CriteriaOperator.Parse(this.FilteringCriterionCommessa.Criterion);
                    criteria.Operands.Add(opCommessa);
                }
                else
                {  /// non vede nulla
                    CriteriaOperator op = CriteriaOperator.Parse(string.Format("OidCommessa == {0}", 0));
                    criteria.Operands.Add(op);
                }

            if (this.Data_DA != null && this.Data_A != null && this.Data_DA > DateTime.MinValue && this.Data_A > DateTime.MinValue)
            {
                CriteriaOperator filtroDATEPersonalizzate = new BetweenOperator("DataPianificata", this.Data_DA, this.Data_A);
                criteria.Operands.Add(filtroDATEPersonalizzate);
            }
            else
            {
                DateTime d_da = DateTime.Now;
                DateTime d_a = DateTime.Now;
                CriteriaOperator filtroDATEPersonalizzate = new BetweenOperator("DataPianificata", d_da, d_a);
                criteria.Operands.Add(filtroDATEPersonalizzate);
            }
            _ListViewSinottico.Criteria = criteria;
            mioflag = false;
            OnChanged("RdLListViewSinottico");
        }
        //[Action(Caption = "Reset", ConfirmationMessage = "Reset della Pivot. sei sicuro di procedere?", ImageName = "Attention", AutoCommit = true)]
        public void ActionMethodReset()
        {
            // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
            _ListViewSinottico = new XPCollection<RdLListViewSinottico>(Session);
            _ListViewSinottico.BindingBehavior = CollectionBindingBehavior.AllowNone;
            GroupOperator criteria = new GroupOperator(GroupOperatorType.And);
            CriteriaOperator op = CriteriaOperator.Parse(
                      string.Format("OidCommessa == {0}", 0));
            criteria.Operands.Add(op);
            _ListViewSinottico.Criteria = criteria;
            mioflag = false;
            //this.Esegui = false;
            OnChanged("RdLListViewSinottico");
        }
        #endregion

        #region RdLListViewSinottico   

        private XPCollection<RdLListViewSinottico> _ListViewSinottico;
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<RdLListViewSinottico> RdLListViewSinottico
        {
            get
            {
                return _ListViewSinottico;//|| Esegui == true

            }
        }

        private XPCollection<RdLListViewSinottico> _ListViewSinotticoPRC;
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<RdLListViewSinottico> RdLListViewSinotticoPRC
        {
            get
            {
                return _ListViewSinottico;
            }
        }



        #endregion
    }

    public enum PivotType
    {
        ultimi_tre_mesi = 1,
        Tests = 2,
        Documentation = 3,
        Diagrams = 4,
        ScreenShots = 5,
        Unknown = 6
    };
}
//private FilteringCriterion fFilteringCriterionDATE;
//[Persistent("FILTRO")]
//[Index(3), XafDisplayName("Filtro")]
////[DataSourceCriteria("SecurityUser.UserName = 'sysfilter' And IsExactType(myObjectType, 'CAMS.Module.DBTask.Vista.RdLListViewSinottico')")]
//[DataSourceProperty("ListFilteringCriterionDATE", DataSourcePropertyIsNullMode.SelectAll)]
//[ToolTipAttribute("impostare il filtro Temporale.")]
//[ImmediatePostData(true)]
//public FilteringCriterion FilteringCriterionDATE
//{
//    get { return fFilteringCriterionDATE; }
//    set { SetPropertyValue("FilteringCriterionDATE", ref fFilteringCriterionDATE, value); }
//}

//private bool fEsegui;
//[NonPersistent, Size(25)]//, DisplayName("Filtro"), , Size(25)
//[VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false), VisibleInDashboards(false), VisibleInReports(false)]
//public bool Esegui
//{
//    get;
//    set;
//}
#region filtro list di criteriofilter
//private List<FilteringCriterion> _FilteringCriterion = null;
//protected IList<FilteringCriterion> ListFilteringCriterionDATE
//{
//    get
//    {
//        if (!IsLoading)
//            if (_FilteringCriterion == null)
//            {
//                _FilteringCriterion = new XPQuery<FilteringCriterion>(Session)
//                        .Where(w => w.myObjectType == typeof(RdLListViewSinottico))
//                        .Where(w => w.SecurityUser.UserName == "sysfilter")
//                        .ToList();
//            }
//        return _FilteringCriterion;
//    }
//}
#endregion

//private IList<string> postponeTimes;
//[Browsable(false), NonPersistent]
//public IEnumerable<string> PostponeTimeList
//{
//    get
//    {
//        if (postponeTimes == null)
//        {
//            postponeTimes = CreatePostponeTimes();
//        }
//        return postponeTimes;
//    }
//}
//private IList<string> CreatePostponeTimes()
//{
//    IList<string> result = new List<string>();
//    result.Add("None");
//    result.Add("At Start Time");
//    return result;
//}