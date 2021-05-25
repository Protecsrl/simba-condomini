using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;


namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions,
    Persistent("SKILLLEVEL"),
    System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Skill Level")]
    [ImageName("Skill level")]
    [NavigationItem("Procedure Attivita")]
     public class SkillLevel : XPObject
    {
        public SkillLevel()
            : base()
        {
        }
        public SkillLevel(Session session)
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
        Size(50)]
        [DbType("varchar(50)")]
        [RuleRequiredField("RReqField.SkillLevel.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatoria")]
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

        private string fCodDescrizione;
        [Persistent("COD_DESCRIZIONE"),
        Size(5)]
        [DbType("varchar(5)")]
        [RuleRequiredField("RReqField.SkillLevel.CodDescrizione", DefaultContexts.Save, "Il Codice Descrizione è un campo obbligatorio")]
        public string CodDescrizione
        {
            get
            {
                return fCodDescrizione;
            }
            set
            {
                SetPropertyValue<string>("CodDescrizione", ref fCodDescrizione, value);
            }
        }

        private string fEsperienza;
        [Persistent("ESPERIENZA"),
        Size(150)]
        [DbType("varchar(150)")]
        [RuleRequiredField("RReqField.SkillLevel.Esperienza", DefaultContexts.Save, "Esperienza è un campo obbligatorio")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Esperienza
        {
            get
            {
                return fEsperienza;
            }
            set
            {
                SetPropertyValue<string>("Esperienza", ref fEsperienza, value);
            }
        }

        private string fFormazione;
        [Persistent("FORMAZIONE"),
        Size(150)]
        [DbType("varchar(150)")]
        [RuleRequiredField("RReqField.SkillLevel.Formazione", DefaultContexts.Save, "Formazione è un campo obbligatorio")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Formazione
        {
            get
            {
                return fFormazione;
            }
            set
            {
                SetPropertyValue<string>("Formazione", ref fFormazione, value);
            }
        }

        private string fTestCompetenze;
        [Persistent("TESTCOMPETENZE"),
        Size(150)]
        [DbType("varchar(150)")]
        [RuleRequiredField("RReqField.SkillLevel.TestCompentenze", DefaultContexts.Save, "Test Competenze è un campo obbligatorio")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string TestCompetenze
        {
            get
            {
                return fTestCompetenze;
            }
            set
            {
                SetPropertyValue<string>("TestCompetenze", ref fTestCompetenze, value);
            }
        }

        private string fAbilitazione;
        [Persistent("ABILITAZIONE"),
        Size(150)]
        [DbType("varchar(150)")]
        [RuleRequiredField("RReqField.SkillLevel.Abilitazione", DefaultContexts.Save, "Abilitazione è un campo obbligatorio")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Abilitazione
        {
            get
            {
                return fAbilitazione;
            }
            set
            {
                SetPropertyValue<string>("Abilitazione", ref fAbilitazione, value);
            }
        }

        private string f_Utente;
        [Persistent("UTENTE"),
        Size(100),
        DisplayName("Utente")]
        [DbType("varchar(100)")]
       // [Appearance("ShillLiv.Utente", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        public string Utente
        {
            get
            {
                return f_Utente;
            }
            set
            {
                SetPropertyValue<string>("Utente", ref f_Utente, value);
            }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[Appearance("ShillLiv.DataAggiornamento", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        public DateTime DataAggiornamento
        {
            get
            {
                return fDataAggiornamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value);
            }
        }
 
    }
}
