

using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.Classi;
using CAMS.Module.PropertyEditors;

namespace CAMS.Module.DBMail
{
    [DefaultClassOptions,  Persistent("REPORTPERIODICIDETT")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Report Periodici Dettaglio")]
    //[Appearance("Master.Disabilita.Dettagliomaggioredizero", TargetItems = "Descrizione;Immobile;UnitaMisura", Criteria = "MasterDettaglios.Count() > 0", Enabled = false)]
    [NavigationItem("Amministrazione")]
    [ImageName("Action_CreateDashboard")]
    public class ReportPeriodiciDettaglio : XPObject
    {
        public ReportPeriodiciDettaglio()
            : base()
        {
        }
        public ReportPeriodiciDettaglio(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();           

        }
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private string fDescrizione;
        [Size(200), Persistent("DESCRIZIONE")]
        [DbType("varchar(200)")]
        public string Descrizione
        {
            get
            {
                return fDescrizione;
            }
            set
            {
                SetPropertyValue<string>("Descrizione", ref fDescrizione, value);
            }
        }

        private ReportPeriodici fReportPeriodici;
        [Persistent("REPORTPERIODICI"), DisplayName("ReportPeriodici")]
        [Association(@"ReportPeriodici_Dettaglio") ]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public ReportPeriodici ReportPeriodici
        {
            get
            {
                return fReportPeriodici;
            }
            set
            {
                SetPropertyValue<ReportPeriodici>("ReportPeriodici", ref fReportPeriodici, value);
            }
        }

        private string fCorpo;
        [Persistent("CORPO"), DisplayName("Testo Mail")]
        [Size(SizeAttribute.Unlimited)]
        //[DbType("CLOB")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Corpo
        {
            get
            {
                return fCorpo;
            }
            set
            {
                SetPropertyValue<string>("Corpo", ref fCorpo, value);
            }
        }

        private DateTime fDataPianificataSchedulazione;
        [Persistent("DATAPIANIFICATASK"), System.ComponentModel.DisplayName("Data Pianificata")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_e_OraMin_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_e_OraMin_EditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_RdLPianificata_Editor)]//
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("La data si riferisce alle ore 24:00", "Data di Pianificazione", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[Appearance("RegistroPOI.Abilita.ReportExcel", Criteria = "StatoPOI = 'Eseguito' Or StatoPOI = 'Trasmesso'", FontColor = "Black", Enabled = false)]
        //[ImmediatePostData]
        public DateTime DataPianificataSchedulazione
        {
            get
            {
                return fDataPianificataSchedulazione;
            }
            set
            {
                SetPropertyValue<DateTime>("DataPianificataSchedulazione", ref fDataPianificataSchedulazione, value);
            }
        }

        private DateTime fDataConferma;
        [Persistent("DATACONFERMA"), System.ComponentModel.DisplayName("Data Conferma")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_e_OraMin_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_e_OraMin_EditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_RdLPianificata_Editor)]//
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("La data si riferisce alle ore 24:00", "Data di Conferma del supervisore primo giorno del trimestre", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[Appearance("RegistroPOI.Abilita.ReportExcel", Criteria = "StatoPOI = 'Eseguito' Or StatoPOI = 'Trasmesso'", FontColor = "Black", Enabled = false)]
        //[ImmediatePostData]
        public DateTime DataConferma
        {
            get
            {
                return fDataConferma;
            }
            set
            {
                SetPropertyValue<DateTime>("DataConferma", ref fDataConferma, value);
            }
        }

        

        public override string ToString()
        {
            if(this.Descrizione != null)
            return String.Format("{0}-{1})",  Descrizione, ReportPeriodici);

            return null;
        }
    }
}
