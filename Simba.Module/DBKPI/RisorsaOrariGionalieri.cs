using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CAMS.Module.Classi;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
#pragma warning disable CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
using System.Linq;
#pragma warning restore CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
using CAMS.Module.PropertyEditors;

//namespace CAMS.Module.DBKPI
//{
//    class RisorsaOrariGionalieri
//    {
//    }
//}
namespace CAMS.Module.DBKPI
{
    [NavigationItem("KPI"), System.ComponentModel.DisplayName("Connessione Giornaliera Risorsa")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Risorse Time Sheet")]
    [DefaultClassOptions, Persistent("RISORSETIMESHEET")]
    [Indices("DataUltimaLetturaMG", "Data", "DataInizio", "DataFine")]
    //[RuleCombinationOfPropertiesIsUnique("UniqrRubricaDestinatari", DefaultContexts.Save, "Email,Nome,Cognome,RoleAzione,SecurityRole", SkipNullOrEmptyValues = false)] 
    [ImageName("NewMail")] 


    public class RisorsaOrariGionalieri : XPObject
    {
        public RisorsaOrariGionalieri()
            : base()
        {
        }
        public RisorsaOrariGionalieri(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private const string DateOfDayEditMask = "dd/MM/yyyy";
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        [Persistent("RISORSE"), DisplayName("Risorsa")]
        //[ImmediatePostData(true)]
        [Delayed(true)]
        public Risorse Risorse
        {
            get
            {
                return GetDelayedPropertyValue<Risorse>("Risorse");
            }
            set
            {
                SetDelayedPropertyValue<Risorse>("Risorse", value);
            }

        }

        private DateTime fData;
        [Persistent("DATACONFERMA"), System.ComponentModel.DisplayName("Data")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateOfDayEditMask)]
        //[EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("La data si riferisce alle ore 24:00", "Data di Lavoro Giornaliero", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[Appearance("RegistroPOI.Abilita.ReportExcel", Criteria = "StatoPOI = 'Eseguito' Or StatoPOI = 'Trasmesso'", FontColor = "Black", Enabled = false)]
        //[ImmediatePostData]
        public DateTime Data
        {
            get
            {
                return fData;
            }
            set
            {
                SetPropertyValue<DateTime>("Data", ref fData, value);
            }
        }

        private DateTime fDataInizio;
        [Persistent("DATAINIZIO"), System.ComponentModel.DisplayName("Data Inizio Attivita")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_e_OraMin_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_e_OraMin_EditMask)]
        //[EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("La data si riferisce alle ore 24:00", "Data di Primo aggiornamento Attività", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[Appearance("RegistroPOI.Abilita.ReportExcel", Criteria = "StatoPOI = 'Eseguito' Or StatoPOI = 'Trasmesso'", FontColor = "Black", Enabled = false)]
        //[ImmediatePostData]
        public DateTime DataInizio
        {
            get
            {
                return fDataInizio;
            }
            set
            {
                SetPropertyValue<DateTime>("DataInizio", ref fDataInizio, value);
            }
        }


        private DateTime fDataFine;
        [Persistent("DATAFINE"), System.ComponentModel.DisplayName("Data Fine Attivita")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_e_OraMin_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_e_OraMin_EditMask)]
        //[EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("La data si riferisce alle ore 24:00", "Data di Ultimo aggiornamento Attività", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[Appearance("RegistroPOI.Abilita.ReportExcel", Criteria = "StatoPOI = 'Eseguito' Or StatoPOI = 'Trasmesso'", FontColor = "Black", Enabled = false)]
        //[ImmediatePostData]
        public DateTime DataFine
        {
            get
            {
                return fDataFine;
            }
            set
            {
                SetPropertyValue<DateTime>("DataFine", ref fDataFine, value);
            }
        }


        [Persistent("MPINAGENDA"), System.ComponentModel.DisplayName("MP In Agenda")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int NumMPinAgenda
        {
            get { return GetDelayedPropertyValue<int>("NumMPinAgenda"); }
            set { SetDelayedPropertyValue<int>("NumMPinAgenda", value); }
        }

        [Persistent("MGINAGENDA"), System.ComponentModel.DisplayName("MG In Agenda")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int NumMGinAgenda
        {
            get { return GetDelayedPropertyValue<int>("NumMGinAgenda"); }
            set { SetDelayedPropertyValue<int>("NumMGinAgenda", value); }
        }

        [Persistent("MGNUOVENONLETTE"), System.ComponentModel.DisplayName("MG Nuove non Lette")] // zero quando legge in aggenza lavori
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int NumGuastoNonLette
        {
            get { return GetDelayedPropertyValue<int>("NumGuastoNonLette"); }
            set { SetDelayedPropertyValue<int>("NumGuastoNonLette", value); }
        }

        [Persistent("MGLETTE"), System.ComponentModel.DisplayName("MG Lette")] // zero quando legge in aggenza lavori
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int NumGuastoLette
        {
            get { return GetDelayedPropertyValue<int>("NumGuastoLette"); }
            set { SetDelayedPropertyValue<int>("NumGuastoLette", value); }
        }

        [Persistent("MPCOMPLETATE"), System.ComponentModel.DisplayName("MP Completate")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int NumMPCompletate
        {
            get { return GetDelayedPropertyValue<int>("NumMPCompletate"); }
            set { SetDelayedPropertyValue<int>("NumMPCompletate", value); }
        }

        [Persistent("MPCOMPLETATEJT"), System.ComponentModel.DisplayName("MP Completate in JT")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int NumMPCompletateJT
        {
            get { return GetDelayedPropertyValue<int>("NumMPCompletateJT"); }
            set { SetDelayedPropertyValue<int>("NumMPCompletateJT", value); }
        }

        [Persistent("MGCOMPLETATE"), System.ComponentModel.DisplayName("MG Completate")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int NumGuastoCompletate
        {
            get { return GetDelayedPropertyValue<int>("NumGuastoCompletate"); }
            set { SetDelayedPropertyValue<int>("NumGuastoCompletate", value); }
        }

        [Persistent("MGCOMPLETATEJT"), System.ComponentModel.DisplayName("MG Completate in JT")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int NumGuastoCompletateJT
        {
            get { return GetDelayedPropertyValue<int>("NumGuastoCompletateJT"); }
            set { SetDelayedPropertyValue<int>("NumGuastoCompletateJT", value); }
        }

        private DateTime? fDataUltimaLetturaMG;
        [Persistent("DATAULTIMALETTURAMG"), System.ComponentModel.DisplayName("Data Ultima Lettura MG")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_e_OraMin_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_e_OraMin_EditMask)]
        //[EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("La data si riferisce alle ore 24:00", "Data di Ultimo aggiornamento Attività", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[Appearance("RegistroPOI.Abilita.ReportExcel", Criteria = "StatoPOI = 'Eseguito' Or StatoPOI = 'Trasmesso'", FontColor = "Black", Enabled = false)]
        [ImmediatePostData]
        public DateTime? DataUltimaLetturaMG
        {
            get
            {
                return fDataUltimaLetturaMG;
            }
            set
            {
                SetPropertyValue<DateTime?>("DataUltimaLetturaMG", ref fDataUltimaLetturaMG, value);
            }
        }
  

        public override string ToString()
        {
            if (Data != null && Risorse != null)
                return string.Format("{0}, {1} {2}", Data.ToShortDateString(), this.Risorse.Nome, this.Risorse.Cognome);
            else
                return "Non Definito";
        }

    }
}
