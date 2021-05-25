using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;


namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions,
    Persistent("MANSIONI"),
    System.ComponentModel.DefaultProperty("Descrizione")]
    [ImageName("mansioni")]
    [NavigationItem("Procedure Attivita")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Mansioni")]
     public class Mansioni : XPObject
    {
        public Mansioni()
            : base()
        {
        }
        public Mansioni(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private string fCodDescrizione;
        [Persistent("COD_DESCRIZIONE"),
        Size(10)]
        [DbType("varchar(10)")]
        [RuleRequiredField("RReqField.Mansioni.CodDescrizione", DefaultContexts.Save, "Il Codice Descrizione è un campo obbligatorio")]
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

        private Skill fSkill;
        [Persistent("SKILL")]
        [RuleRequiredField("RReqField.Mansioni.Skill", DefaultContexts.Save, "La Skill è un campo obbligatoria")]
        [ExplicitLoading()]
        public Skill Skill
        {
            get
            {
                return fSkill;
            }
            set
            {
                SetPropertyValue<Skill>("Skill", ref fSkill, value);
            }
        }

        private SkillLevel fSkillLevel;
        [Persistent("SKILLLEVEL")]
        [RuleRequiredField("RReqField.Mansioni.SkillLevel", DefaultContexts.Save, "Lo Skill Level è un campo obbligatorio")]
        [ExplicitLoading()]
        public SkillLevel SkillLevel
        {
            get
            {
                return fSkillLevel;
            }
            set
            {
                SetPropertyValue<SkillLevel>("SkillLevel", ref fSkillLevel, value);
            }
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"),
        Size(150)]
        [DbType("varchar(150)")]
        [RuleRequiredField("RReqField.Mansioni.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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

        private string f_Utente;
        [Persistent("UTENTE"),
        Size(100),
        DisplayName("Utente")]
        [DbType("varchar(100)")]
        //[Appearance("Mansioni.Utente", Enabled = false)]
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
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        [System.ComponentModel.Browsable(false)]
        //[Appearance("Mansioni.DataAggiornamento", Enabled = false)]
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
