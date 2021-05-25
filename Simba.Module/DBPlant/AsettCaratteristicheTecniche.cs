using CAMS.Module.DBALibrary;
using CAMS.Module.DBMisure;
using CAMS.Module.DBAngrafica;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using DevExpress.Persistent.Base.General;
using System.ComponentModel;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("APPARATOCARTECNICHE")]
    [RuleCombinationOfPropertiesIsUnique("UniqueApparatoCarTecniche", DefaultContexts.Save, "Apparato, StdApparatoCaratteristicheTecniche,ParentObject")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Caratteristiche Tecniche Asset")]
    [System.ComponentModel.DefaultProperty("FullValore")]
    [ImageName("Action_EditModel")]
    [NavigationItem(false)]//"Consistenza"
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("ApparatoCaratteristicheTecniche", "", "Tutto", true, Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("ApparatoCaratteristicheTecniche.UnitamisuraAdim", "DescUnitaMisura = 'Adimensionale'" , "UM-Adimensionale", Index = 0)]


    public class AsettCaratteristicheTecniche : XPObject, ITreeNode
    {
        public AsettCaratteristicheTecniche() : base() { }
        public AsettCaratteristicheTecniche(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {
                if (this.ParentObject != null)
                this.Asset = ParentObject.Asset;
              
            }
        }
        #region Struttura  Intefaccia ad Albero
        private string fUserName;
        //[Appearance("ApparatoCaratteristicheTecnicheTree.Caption.Visualizza", Visibility = ViewItemVisibility.Hide, Criteria = "ParentObject = null")]
        [Persistent("USERNAME")]
        public string UserName
        {
            get { return fUserName; }
            set { SetPropertyValue<string>("UserName", ref fUserName, value); }
        }

        private AsettCaratteristicheTecniche parentObject;
        [Browsable(false)]
        [Association("ApparatoCaratteristicheTecnicheParentObject-NestedObjects")]
        [Persistent("PARENT")]
        public AsettCaratteristicheTecniche ParentObject
        {
            get { return parentObject; }
            set { SetPropertyValue<AsettCaratteristicheTecniche>("ParentObject", ref parentObject, value); }
        }

        [Association("ApparatoCaratteristicheTecnicheParentObject-NestedObjects"), Aggregated]
        public XPCollection<AsettCaratteristicheTecniche> NestedObjects
        {
            get { return GetCollection<AsettCaratteristicheTecniche>("NestedObjects"); }
        }


        IBindingList ITreeNode.Children
        {
            get { return NestedObjects; }
        }
        string ITreeNode.Name
        {
            get { return UserName; }
        }
        ITreeNode ITreeNode.Parent
        {
            get { return ParentObject; }
        }

        #endregion

        private Asset fAsset;
        [Persistent("ASSET"), Association(@"AsettCaratteristicheTecniche"), System.ComponentModel.DisplayName("Asset")]
        [ExplicitLoading()]
        [VisibleInListView(false)]
        public Asset Asset
        {
            get { return fAsset; }
            set { SetPropertyValue<Asset>("Asset", ref fAsset, value); }
        }

        private StdApparatoCaratteristicheTecniche fStdApparatoCaratteristicheTecniche;
        [Persistent("APPARATOSTDCARTECNICHE"), System.ComponentModel.DisplayName("Caratteristica Tecnica")]
        [DataSourceCriteria("Iif('@This.Asse' is null,null, StandardApparato = '@This.Asset.StdApparato')")]
        [ImmediatePostData(true)]
        [VisibleInListView(false)]
        public StdApparatoCaratteristicheTecniche StdApparatoCaratteristicheTecniche
        {
            get { return fStdApparatoCaratteristicheTecniche; }
            set { SetPropertyValue<StdApparatoCaratteristicheTecniche>("StdApparatoCaratteristicheTecniche", ref fStdApparatoCaratteristicheTecniche, value); }
        }

        private double fValore;
        [Persistent("VALORE")]
        [Appearance("ApparatoCaratteristicheTecniche.ValoreIntero", AppearanceItemType.LayoutItem,
            @"StdApparatoCaratteristicheTecniche.TipoValoreCaratteristicaTecnica != 'Intero'", Visibility = ViewItemVisibility.Hide)]
        [VisibleInListView(false)]
        [ImmediatePostData]
        public double Valore
        {
            get { return fValore; }
            set { SetPropertyValue<double>("Valore", ref fValore, value); }
        }

        private string fValoreStr;
        [Persistent("VALORETXT")]
        [Appearance("ApparatoCaratteristicheTecniche.ValoreStringa", AppearanceItemType.LayoutItem,
            @"StdApparatoCaratteristicheTecniche.TipoValoreCaratteristicaTecnica != 'Testo'", Visibility = ViewItemVisibility.Hide)]
        [VisibleInListView(false)]
        [ImmediatePostData]
        public string ValoreStr
        {
            get { return fValoreStr; }
            set { SetPropertyValue<string>("ValoreStr", ref fValoreStr, value); }
        }

        private AppCaratteristicaValoreSelezione fAppCaratteristicaValoreSelezione;
        [Persistent("APPCARVALORESELEZIONE"), System.ComponentModel.DisplayName("Valore")]
        [DataSourceCriteria("Iif('@This.Apparato' is null Or StdApparatoCaratteristicheTecniche is null,null,  StdApparatoCaratteristicheTecniche = '@This.StdApparatoCaratteristicheTecniche')")]
        [Appearance("ApparatoCaratteristicheTecniche.AppCaratteristicaValoreSelezione", AppearanceItemType.LayoutItem,
        @"StdApparatoCaratteristicheTecniche.TipoValoreCaratteristicaTecnica != 'Tendina'", Visibility = ViewItemVisibility.Hide)]
        [ImmediatePostData]
        [VisibleInListView(false)]
        public AppCaratteristicaValoreSelezione AppCaratteristicaValoreSelezione
        {
            get { return fAppCaratteristicaValoreSelezione; }
            set { SetPropertyValue<AppCaratteristicaValoreSelezione>("AppCaratteristicaValoreSelezione", ref fAppCaratteristicaValoreSelezione, value); }
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private DateTime fDataSostituzione;
        [Persistent("DATASOSTITUZIONE"), System.ComponentModel.DisplayName("Data Sostituzione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Sostituzione della Caratteristica", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Appearance("ApparatoCaratteristicheTecnicheTree.DataSostituzione.Visualizza", Visibility = ViewItemVisibility.Hide, Criteria = "ParentObject = null")]
        public DateTime DataSostituzione
        {
            get { return fDataSostituzione; }
            set { SetPropertyValue<DateTime>("DataSostituzione", ref fDataSostituzione, value); }
        }


        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"), System.ComponentModel.DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Aggiornamento dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [System.ComponentModel.Browsable(false)]
        public DateTime DataAggiornamento
        {
            get { return fDataAggiornamento; }
            set { SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value); }
        }

        //private Storicizzazione fStoricizzazione;
        //[Persistent("STORICIZZAZIONE"), DisplayName("Storicizzazione CT")]       
        //[ImmediatePostData(true)]
        //[VisibleInListView(false)]
        //public Storicizzazione Storicizzazione
        //{
        //    get
        //    {
        //        return fStoricizzazione;
        //    }
        //    set
        //    {
        //        SetPropertyValue<Storicizzazione>("Storicizzazione", ref fStoricizzazione, value);

        //    }
        //}

        [PersistentAlias("Iif(AppCaratteristicaValoreSelezione != null,  AppCaratteristicaValoreSelezione.Descrizione,'NA')")]
        [MemberDesignTimeVisibility(false)]
        [VisibleInListView(true)]
        public string FullValore2
        {
            get
            {
                object tempObject = EvaluateAlias("FullValore2");
                if (tempObject != null)
                {
                    return (string)tempObject.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        //[PersistentAlias("StdApparatoCaratteristicheTecniche.TipoValoreCaratteristicaTecnica")]
        [PersistentAlias("Iif(StdApparatoCaratteristicheTecniche != null, " +
                            "Iif(StdApparatoCaratteristicheTecniche.TipoValoreCaratteristicaTecnica != null, " +
                                                           "StdApparatoCaratteristicheTecniche.TipoValoreCaratteristicaTecnica , 'NA')" +
                                                                         " , 'NA')")]
        [VisibleInListView(true)]
        [MemberDesignTimeVisibility(false)]
        public string FullValore3
        {
            get
            {
                object tempObject = EvaluateAlias("FullValore3");
                if (tempObject != null)
                {

                    return (string)tempObject.ToString();
                }
                else
                {
                    return "NA";
                }
            }
        }
        [PersistentAlias("Iif(StdApparatoCaratteristicheTecniche is null,'NA',  " +
                                      "Iif(StdApparatoCaratteristicheTecniche.UnitaMisura is not null, StdApparatoCaratteristicheTecniche.UnitaMisura,'NA') )")]
        [System.ComponentModel.DisplayName("Unità di misura")]
        public string DescUnitaMisura
        {
            get
            {
                object tempObject = EvaluateAlias("DescUnitaMisura");
                if (tempObject != null)
                {
                    return (string)tempObject.ToString();
                }
                else
                {
                    return "NA";
                }
            }
        }

        [PersistentAlias("Iif(FullValore3 = 'Tendina', FullValore2 , " +
                        "    Iif(FullValore3 = 'Testo', ValoreStr   , " +
                        "      Iif(FullValore3 = 'Intero', Valore   , 'NA'" +
                        "     )  ) )")]
        [System.ComponentModel.DisplayName("Descrizione Caratteristica Tecnica")]
        [VisibleInListView(true)]
        public string FullValore4
        {
            get
            {
                object tempObject = EvaluateAlias("FullValore4");
                if (tempObject != null)
                {

                    return (string)tempObject.ToString();
                }
                else
                {
                    return "NA";
                }
            }
        }

    }
}
