using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using CAMS.Module.PropertyEditors;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions]
    [ImageName("ShowWorkTimeOnly")]
    [NavigationItem("Ticket")]
    [DefaultProperty("Descrizione")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    [Persistent("REGATTIVITAACCESSORIE")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class RegistroAttivitaAccessorie : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public RegistroAttivitaAccessorie(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        //[MemberDesignTimeVisibility(false)] //       [Delayed(true)]
        [Persistent("RISORSATEAM")]
        [Index(0), VisibleInListView(true)]
        [Delayed(true)]
        public RisorseTeam RisorseTeam
        {
            get { return GetDelayedPropertyValue<RisorseTeam>("RisorseTeam"); }
            set { SetDelayedPropertyValue<RisorseTeam>("RisorseTeam", value); }
        }

        private string fDescrizione;
        [XafDisplayName("Descrizione"), ToolTip("Descrizione")]
        //[ModelDefault("EditMask", "(000)-00")] 
        [Index(1), VisibleInListView(true)]
        [Persistent("DESCRIZIONE"), RuleRequiredField(DefaultContexts.Save)]
        [Size(4000), DbType("varchar(4000)")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue("Descrizione", ref fDescrizione, value); }
        }



        [Persistent("DATAINIZIO"), DevExpress.ExpressApp.DC.XafDisplayName("Data Ora Inizio")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data e Ora Inizio attività accessoria", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Index(2), VisibleInListView(true)]
        [Delayed(true)]
        public DateTime DataOraInizio
        {
            get { return GetDelayedPropertyValue<DateTime>("DataOraInizio"); }
            set { SetDelayedPropertyValue<DateTime>("DataOraInizio", value); }
        }

        [Persistent("DATAFINE"), DevExpress.ExpressApp.DC.XafDisplayName("Data Ora Fine")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data e Ora fine attività accessoria", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Delayed(true)]
        public DateTime DataOraFine
        {
            get { return GetDelayedPropertyValue<DateTime>("DataOraFine"); }
            set { SetDelayedPropertyValue<DateTime>("DataOraFine", value); }
        }

        [Persistent("DATAUPDATE"), DevExpress.ExpressApp.DC.XafDisplayName("Data Ora Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data e Ora Aggiornamento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Index(3), VisibleInListView(true)]   
        [Delayed(true)]
        public DateTime DataOraAggiornamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataOraAggiornamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataOraAggiornamento", value); }
        }

   

        //[MemberDesignTimeVisibility(false)] //       
        [Persistent("RISORSATEAM_ASSISTITO")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Delayed(true)]
        public RisorseTeam RisorseTeamAssistito
        {
            get { return GetDelayedPropertyValue<RisorseTeam>("RisorseTeamAssistito"); }
            set { SetDelayedPropertyValue<RisorseTeam>("RisorseTeamAssistito", value); }
        }

        //[MemberDesignTimeVisibility(false)] //       
        [Persistent("STATOOPERATIVO")]
        [Index(4), VisibleInListView(true)]  
        [Delayed(true)]
        public StatoOperativo StatoOperativo
        {
            get { return GetDelayedPropertyValue<StatoOperativo>("StatoOperativo"); }
            set { SetDelayedPropertyValue<StatoOperativo>("StatoOperativo", value); }
        }


        [Persistent("REGRDL_ASSISTITO")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [Delayed(true)]
        public RegistroRdL RegistroRdLAssistito
        {
            get { return GetDelayedPropertyValue<RegistroRdL>("RegistroRdLAssistito"); }
            set { SetDelayedPropertyValue<RegistroRdL>("RegistroRdLAssistito", value); }
        }

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
    }
}