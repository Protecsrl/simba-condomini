using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions,
    Persistent("SKILL"),
    System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Skill")]
    [ImageName("Skill")]
    [NavigationItem("Procedure Attivita")]
     public class Skill : XPObject
    {
        public Skill()
            : base()
        {
        }
        public Skill(Session session)
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
        [RuleRequiredField("RReqField.Skill.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatoria")]
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

        private string fProvenienza;
        [Persistent("PROVENIENZA"),
        Size(10)]
        [DbType("varchar(10)")]
        [RuleRequiredField("RReqField.Skill.Provenienza", DefaultContexts.Save, "Provenienza è un campo obbligatorio")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Provenienza
        {
            get
            {
                return fProvenienza;
            }
            set
            {
                SetPropertyValue<string>("Provenienza", ref fProvenienza, value);
            }
        }

        private string fCodDescrizione;
        [Persistent("COD_DESCRIZIONE"),
        Size(10)]
        [DbType("varchar(10)")]
        [RuleRequiredField("RReqField.Skill.CodDescrizione", DefaultContexts.Save, "Il Cod Descrizione è un campo obbligatorio")]
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

        ///   file data mansionario

        [Persistent("MANSIONARIO")]
        [RuleRequiredField("RReqField.Skill.Mansionario", DefaultContexts.Save, "Mansionario è un campo obbligatorio")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData Mansionario
        {
            get
            {
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("Mansionario");
              //  return fMansionario;
            }
            set
            {
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("Mansionario", value);
              //  SetPropertyValue<FileData>("Mansionario", ref fMansionario, value);
            }
        }

        private string f_Utente;
        [Persistent("UTENTE"),
        Size(100),
        DisplayName("Utente")]
        [DbType("varchar(100)")]
       // [Appearance("Shill.Utente", Enabled = false)]
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
        //[Appearance("Shill.DataAggiornamento", Enabled = false)]
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
