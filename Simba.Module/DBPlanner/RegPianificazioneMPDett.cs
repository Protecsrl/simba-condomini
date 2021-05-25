using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions,
    Persistent("MPREGPIANIFICAZIONEDETT")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettaglio Reg. Pianificazione MP")]
    [ImageName("Action_Copy_CellValue")]
    [NavigationItem(false)]
    public class RegPianificazioneMPDett : XPObject
    {
        public RegPianificazioneMPDett()
            : base()
        {
        }

        public RegPianificazioneMPDett(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private string fDescrizione;
        [Persistent("DESCRIZIONE"),
        Size(1000),
        DisplayName("Descrizione Attività di Dettaglio")]
        [DbType("varchar(1000)")]
        [RuleRequiredField("RReqField.RegPianifMPDett.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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

        private int fSettimana;
        [Persistent("SETTIMANA"),
        DisplayName("Settimana Eseguita"),
        MemberDesignTimeVisibility(false)]
        public int Settimana
        {
            get
            {
                return fSettimana;
            }
            set
            {
                SetPropertyValue<int>("Settimana", ref fSettimana, value);
            }
        }

        private DateTime fDataModifica;
        [Persistent("DATAMODIFICA"),
        DisplayName("Data Aggiornamento")]
        [System.ComponentModel.Browsable(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime DataModifica
        {
            get
            {
                return fDataModifica;
            }
            set
            {
                SetPropertyValue<DateTime>("DataModifica", ref fDataModifica, value);
            }
        }

        private RegPianificazioneMP fRegistroPianificazioneMP;
        [MemberDesignTimeVisibility(false)]
        [Association(@"RegPianMPrefDett", typeof(RegPianificazioneMP))]
        [Persistent("MPREGPIANIFICAZIONE"),
        DisplayName("Registro Pianificazione MP")]
        public RegPianificazioneMP RegistroPianificazioneMP
        {
            get
            {
                return fRegistroPianificazioneMP;
            }
            set
            {
                SetPropertyValue<RegPianificazioneMP>("RegistroPianificazioneMP", ref fRegistroPianificazioneMP, value);
            }
        }
    }
}
