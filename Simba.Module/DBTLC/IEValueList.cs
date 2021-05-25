using CAMS.Module.DBMisure;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Drawing;
namespace CAMS.Module.DBTLC
{
    [DefaultClassOptions, Persistent("TLC_IEVALUELIST")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Value List")]
    [ImageName("ShowTestReport")]
    [NavigationItem("Telecontrollo")]
    #region filtro tampone  IsThisYear
    [ListViewFilter("RegMisureDettaglioDett_oggi", "[DataValue] >= Today()", "Oggi", true, Index = 0)]
    [ListViewFilter("RegMisureDettaglioDett_ieri", "[DataValue] >= AddDays(Today(),-1)", "ieri", Index = 1)]
    [ListViewFilter("RegMisureDettaglioDett_ultimi2giorni", "[DataValue] >= AddDays(Today(),-2)", "ultimi due Giorni", Index = 2)]
    [ListViewFilter("RegMisureDettaglioDett_ultimi3giorni", "[DataValue] >= AddDays(Today(),-3)", "ultimi tre Giorni", Index = 3)]
    [ListViewFilter("RegMisureDettaglioDett_questasettimana", "IsThisWeek([DataValue])", "Questa Settimana", Index = 4)]
    [ListViewFilter("RegMisureDettaglioDett_questomese", "IsThisMonth([DataValue])", "questo mese", Index = 5)]
    [ListViewFilter("RegMisureDettaglioDett_mesescorso", "IsLastMonth([DataValue])", "scorso Mese", Index = 6)]
    [ListViewFilter("RegMisureDettaglioDett_1TrimAnnoinCorso", "DateDiffMonth([DataMisura],Today()) < 4", @" ultimo Trimestre", Index = 7)]
    [ListViewFilter("RegMisureDettaglioDett_annoincorso", "IsThisYear([DataValue])", "Anno in corso", Index = 8)]
    [ListViewFilter("RegMisureDettaglioDett_annoscorso", "IsLastYear([DataValue])", "Anno scorso", Index = 9)]
    [ListViewFilter("RegMisureDettaglioDett_annistorici", " DateDiffYear(DataValue,Today()) > 2", "Anni Storici", Index = 10)]
    [ListViewFilter("RegMisureDettaglioDettTutto", "", "Tutto", Index = 11)]
    #endregion

    public class IEValueList : XPObject
    {
        public IEValueList() : base() { }
        public IEValueList(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        [Persistent("TLC_IEPLANTLIST"), System.ComponentModel.DisplayName("Plant List")]
        [Association(@"Plant.IEValueList")]
        [VisibleInListView(false)]
        [Delayed(true)]
        public IEPlantList IEPlantList
        {
            get { return GetDelayedPropertyValue<IEPlantList>("IEPlantList"); }
            set { SetDelayedPropertyValue<IEPlantList>("IEPlantList", value); }
        }

        [Size(1024), Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione Valore")]
        ///  [RuleRequiredField("SegnalazioneCC.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //  [Appearance("SegnalazioneCC.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [DbType("varchar(1024)")]
        [Delayed(true)]
        public string Descrizione
        {
            get { return GetDelayedPropertyValue<string>("Descrizione"); }
            set { SetDelayedPropertyValue<string>("Descrizione", value); }
        }

        [Persistent("DATAVALUE"), System.ComponentModel.DisplayName("Data Rilevazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //  [ToolTip("Data di Inserimento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]   In(1,2,11))
        //  [Appearance("SegnalazioneCC.DataRichiesta.Enabled", @"Oid > 0 And UtenteCreatoRichiesta != CurrentUserId() And !([UltimoStatoSmistamento.Oid] In(1))", FontColor = "Black", Enabled = false)]
        //[EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        [Delayed(true)]
        public DateTime DataValue
        {
            get { return GetDelayedPropertyValue<DateTime>("DataValue"); }
            set { SetDelayedPropertyValue<DateTime>("DataValue", value); }
        }

        [Persistent("DATAVALUESTR"), System.ComponentModel.DisplayName("Data Rilevazione Stringa")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //  [ToolTip("Data di Inserimento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]   In(1,2,11))
        //  [Appearance("SegnalazioneCC.DataRichiesta.Enabled", @"Oid > 0 And UtenteCreatoRichiesta != CurrentUserId() And !([UltimoStatoSmistamento.Oid] In(1))", FontColor = "Black", Enabled = false)]
        //[DbType("varchar(48)")]
        [Delayed(true)]
        public string DataValueString
        {
            get { return GetDelayedPropertyValue<string>("DataValueString"); }
            set { SetDelayedPropertyValue<string>("DataValueString", value); }
        }

        [Size(250), Persistent("VALUE"), System.ComponentModel.DisplayName("Valore")]
        ///  [RuleRequiredField("SegnalazioneCC.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //  [Appearance("SegnalazioneCC.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        //[DbType("varchar(250)")]
        [Delayed(true)]
        public string Value
        {
            get { return GetDelayedPropertyValue<string>("Value"); }
            set { SetDelayedPropertyValue<string>("Value", value); }
        }


        [Persistent("DATAUPDATE"), System.ComponentModel.DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //  [ToolTip("Data di Inserimento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]   In(1,2,11))
        //  [Appearance("SegnalazioneCC.DataRichiesta.Enabled", @"Oid > 0 And UtenteCreatoRichiesta != CurrentUserId() And !([UltimoStatoSmistamento.Oid] In(1))", FontColor = "Black", Enabled = false)]
        //[EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        [VisibleInListView(false)]
        [Delayed(true)]
        public DateTime DataAggiornamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAggiornamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataAggiornamento", value); }
        }


        [Persistent("REGMISUREDETTAGLIO"), System.ComponentModel.DisplayName("Registro")]
        [VisibleInListView(false)]
        [Delayed(true)]
        public RegMisureDettaglio RegMisureDettaglio
        {
            get { return GetDelayedPropertyValue<RegMisureDettaglio>("RegMisureDettaglio"); }
            set { SetDelayedPropertyValue<RegMisureDettaglio>("RegMisureDettaglio", value); }
        }

        public override string ToString()
        {
            return this.Descrizione;
        }
    }
}
