using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using DevExpress.Xpo;
using CAMS.Module.DBPlant;
using DevExpress.Persistent.Base;
using CAMS.Module.DBALibrary;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Validation;
using CAMS.Module.Classi;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.Editors;
using CAMS.Module.DBTLC;

namespace CAMS.Module.DBMisure
{
    [DefaultClassOptions, Persistent("REGMISUREDETTAGLIODETT")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettaglio")]
    [System.ComponentModel.DisplayName("Dettaglio")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem(false)]
    [Indices("DataMisura", "Note")]
    [System.ComponentModel.DefaultProperty("FullName")]

    #region filtro tampone  IsThisYear
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegMisureDettaglioDett_oggi", "[DataMisura] >= Today()", "Oggi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegMisureDettaglioDett_ultimitregiorni", "[DataMisura] >= AddDays(Today(),-3)", "ultimi tre giorni", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegMisureDettaglioDett_questasettimana", "IsThisWeek([DataMisura])", "Questa Settimana", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegMisureDettaglioDett_questomese", "IsThisMonth([DataMisura])", "questo mese", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegMisureDettaglioDett_mesescorso", "IsLastMonth([DataMisura])", "scorso mese", Index = 4)] 
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegMisureDettaglioDett_1TrimAnnoinCorso", "DateDiffMonth(Today(),[DataMisura]) < 4", @" ultimo trimestre", Index = 5)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegMisureDettaglioDett_questoanno", "IsThisYear([DataMisura])", @"questo Anno", Index = 6)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegMisureDettaglioDett_annoscrso", "IsLastYear([DataMisura])", @"Anno scorso", Index = 7)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegMisureDettaglioDettTutto", "", "Tutto", Index = 8)]
    #endregion
    public class RegMisureDettaglioDett : XPObject
    {
        public RegMisureDettaglioDett()
            : base()
        {
        }

        public RegMisureDettaglioDett(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        
        [Association(@"RegMisureDettaglio_Dettaglio"),
        Persistent("REGMISUREDETTAGLIO"),
        DisplayName("Registro Misure Dettaglio")]
        [ExplicitLoading()]
        [ImmediatePostData(true)] 
        [Delayed(true)]
        public RegMisureDettaglio RegMisureDettaglio
        {
            get { return GetDelayedPropertyValue<RegMisureDettaglio>("RegMisureDettaglio"); }
            set { SetDelayedPropertyValue<RegMisureDettaglio>("RegMisureDettaglio", value); }
        }


        private Double fValore;
        [Persistent("VALORE"), DisplayName("Valore")]
        //[RuleRequiredField("RReqFieldObJ.RegMisureDettaglioDett.Valore", DefaultContexts.Save, "Il Valore è un campo obligatorio")]
        [Appearance("RegMisureDettaglioDett.Valore", AppearanceItemType.LayoutItem, @"MasterTipoMisure.TipoValoreMisura != 'Intero'", Visibility = ViewItemVisibility.Hide)]
        public Double Valore
        {
            get
            {
                return fValore;
            }
            set
            {
                SetPropertyValue<Double>("Valore", ref fValore, value);
            }
        }

        private string fValoreStr;
        [Persistent("VALORETXT")]
        [Appearance("RegMisureDettaglioDett.ValoreStringa", AppearanceItemType.LayoutItem, @"MasterTipoMisure.TipoValoreMisura != 'Testo'", Visibility = ViewItemVisibility.Hide)]
        [VisibleInListView(false)]
        [ImmediatePostData]
        public string ValoreStr
        {
            get
            {
                return fValoreStr;
            }
            set
            {
                SetPropertyValue<string>("ValoreStr", ref fValoreStr, value);
            }
        }

        private DateTime? fDataMisura;
        //[PersistentAlias("RegMisure.DataInserimento"), DisplayName("Data Misura")]
        [Persistent("DATAMISURA"), DisplayName("Data Misura")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask)]
        [RuleRequiredField("RReqFO.RegMisureDettaglioDett.DataMisura", DefaultContexts.Save, "La Data Misura è un campo obligatorio")]
        //[VisibleInDetailView(false)]
        public DateTime? DataMisura
        {
            get
            {
                return fDataMisura;
            }
            set
            {
                SetPropertyValue<DateTime?>("DataMisura", ref fDataMisura, value);
            }
        }
       

        [Size(1000), Persistent("NOTE"), DisplayName("Note")]
        [DbType("varchar(1000)")]        
        [Delayed(true)]
        public String Note
        {
            get { return GetDelayedPropertyValue<String>("Note"); }
            set { SetDelayedPropertyValue<String>("Note", value); }
        }

        [Persistent("TLC_IEVALUELIST"), DisplayName("TLC Value List")]
        [Appearance("regmisuredettaglioDett.IEValueList.Visibility", Criteria = "IEValueList is null", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [Appearance("regmisuredettaglioDett.IEValueList.Enabled", Criteria = "1==1", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [Delayed(true)]
        public IEValueList IEValueList
        {
            get { return GetDelayedPropertyValue<IEValueList>("IEValueList"); }
            set { SetDelayedPropertyValue<IEValueList>("IEValueList", value); }
        }


        [PersistentAlias("Iif(DataMisura is not null And this.ValoreStr is not null,this.Apparato.Descrizione + ', ' +  this.DataMisura + ', ' + this.ValoreStr  ,null)")]
        [DevExpress.Xpo.DisplayName("FullName")]
        [System.ComponentModel.Browsable(false)]
        public string FullName
        {
            get
            {
                //IEPlantList.Descrizione        
                var tempObject = EvaluateAlias("FullName");
                if (tempObject != null)
                {
                    return tempObject.ToString();
                }
                return null;
            }
        }
    }
}
