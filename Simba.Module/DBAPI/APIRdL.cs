using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CAMS.Module.Validation;
using DevExpress.Persistent.Validation;
using System.Globalization;
using System.Web.Security;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBAPI
{
    [NavigationItem("Amministrazione"), System.ComponentModel.DisplayName("StatusRdL")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "StatusRdL")]
    [DefaultClassOptions, Persistent("APIRDLSTATUS")]
    //[RuleCombinationOfPropertiesIsUnique("UniqrFestivitaDescrizione", DefaultContexts.Save, "Giorno,Mese", SkipNullOrEmptyValues = false)]
    public class APIRdL : XPObject
    {

        public APIRdL()
           : base()
        {
        }

        public APIRdL(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private long fId;
        [Persistent("ID"), DisplayName("Id")]
        public long Id
        {
            get { return fId; }
            set { SetPropertyValue<long>("Id", ref fId, value); }
        }
        private Guid fToken;
        [Persistent("TOKEN"), DisplayName("Token")]
        public Guid Token
        {
            get { return fToken; }
            set { SetPropertyValue<Guid>("Token", ref fToken, value); }
        }
        private long fCodice;
        [Persistent("CODICE"), DisplayName("Codice")]
        public long Codice
        {
            get { return fCodice; }
            set { SetPropertyValue<long>("Codice", ref fCodice, value); }
        }

        private string fCodiceOut;
        [Persistent("CODICE_OUT"), DisplayName("Codice Out")]
        public string CodiceOut
        {
            get { return fCodiceOut; }
            set { SetPropertyValue<string>("CodiceOut", ref fCodiceOut, value); }
        }

        // campi date 


        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        private DateTime fDataOra_UltimoGet;
        [Persistent("DATAORA_ULTIMOGET"), DisplayName("DataOra_UltimoGet")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra_UltimoGet
        {
            get
            {
                return fDataOra_UltimoGet;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOra_UltimoGet", ref fDataOra_UltimoGet, value);
            }
        }
        private DateTime fDataOra_UltimoPut;
        [Persistent("DATAORA_ULTIMOPUT"), DisplayName("DataOra_UltimoPut")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra_UltimoPut
        {
            get
            {
                return fDataOra_UltimoPut;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOra_UltimoPut", ref fDataOra_UltimoPut, value);
            }
        }

        private DateTime fDataOra_St_PVISIONE;
        [Persistent("DATAORA_ST_PVISIONE"), DisplayName("DataOra_St_PVISIONE")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra_St_PVISIONE
        {
            get
            {
                return fDataOra_St_PVISIONE;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOra_St_PVISIONE", ref fDataOra_St_PVISIONE, value);
            }
        }

        //public DateTime DataOra_St_SOPRALLUOGO { get; set; }
        private DateTime fDataOra_St_SOSPESO;
        [Persistent("DATAORA_ST_SOSPESO"), DisplayName("DataOra_St_SOSPESO")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra_St_SOSPESO
        {
            get
            {
                return fDataOra_St_SOSPESO;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOra_St_SOSPESO", ref fDataOra_St_SOSPESO, value);
            }
        }
        private DateTime fDataOra_St_COMPLETATO;
        [Persistent("DATAORA_ST_COMPLETATO"), DisplayName("DataOra_St_COMPLETATO")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra_St_COMPLETATO
        {
            get
            {
                return fDataOra_St_COMPLETATO;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOra_St_COMPLETATO", ref fDataOra_St_COMPLETATO, value);
            }
        }

        private DateTime fDataOra_St_ANNULLATO;
        [Persistent("DATAORA_ST_ANNULLATO"), DisplayName("DataOra_St_ANNULLATO")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra_St_ANNULLATO
        {
            get
            {
                return fDataOra_St_ANNULLATO;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOra_St_ANNULLATO", ref fDataOra_St_ANNULLATO, value);
            }
        }
        private DateTime fDataOra_Sta_SOPRALLUOGO;
        [Persistent("DATAORA_STA_SOPRALLUOGO"), DisplayName("DataOra_Sta_SOPRALLUOGO")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra_Sta_SOPRALLUOGO
        {
            get
            {
                return fDataOra_Sta_SOPRALLUOGO;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOra_Sta_SOPRALLUOGO", ref fDataOra_Sta_SOPRALLUOGO, value);
            }
        }
        private DateTime fDataOra_Stato_SOSPESO;
        [Persistent("DATAORA_STATO_SOSPESO"), DisplayName("DataOra Stato SOSPESO")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra_Stato_SOSPESO
        {
            get
            {
                return fDataOra_Stato_SOSPESO;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOra_Stato_SOSPESO", ref fDataOra_Stato_SOSPESO, value);
            }
        }


        private DateTime fDataOra_Stato_COMP;
        [Persistent("DATAORA_STATO_COMPLETATO"), DisplayName("DataOra Stato COMPLETATO")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra_Stato_COMP
        {
            get
            {
                return fDataOra_Stato_COMP;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOra_Stato_COMP", ref fDataOra_Stato_COMP, value);
            }
        }
        private DateTime fDataOra_Stato_ANN;
        [Persistent("DATAORA_STATO_ANNULLATO"), DisplayName("DataOra Stato ANNULLATO")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra_Stato_ANN
        {
            get
            {
                return fDataOra_Stato_ANN;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOra_Stato_ANN", ref fDataOra_Stato_ANN, value);
            }
        }

        private DateTime fDataOraRdL;
        [Persistent("DATAORARDL"), DisplayName("DataOraRdL")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOraRdL
        {
            get
            {
                return fDataOraRdL;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOraRdL", ref fDataOraRdL, value);
            }
        }

        //[StringLength(8, MinimumLength = 4, ErrorMessage = "è necessario specificare un testo compreso tra 1 e 200 caratteri.")]

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(4000), DisplayName("Descrizione")]
        [DbType("varchar(4000)")]
        //[RuleRequiredField("RReqField.APIRdL.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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

        public string NomeCognomeTecnico { get; set; }

        public string Note_COMPLETAMENTO { get; set; }

        public bool IsReceived { get; set; }
        public bool IsSent { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "La lunghezza di {0} deve essere di almeno {2} caratteri.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Nuova password")]
        //[Required]
        //[StringLength(3999, MinimumLength = 0, ErrorMessage = "è necessario specificare una nota compreso tra 1 e 3999 caratteri.")]
        //[RuleStringLengthComparison("CustomRule.DBAPI.StatusRdL.Note.3999", DefaultContexts.Save, 3999, StringLengthComparisonMode.NotMoreThan)]

        public string CodiceSITO { get; set; }
        public string CodicePiano { get; set; }
        public string CodiceLocale { get; set; }
        public string Codiceimpianto { get; set; }

        //[RuleStringLengthComparison("CustomRule.DBAPI.StatusRdL.CodiceApparato.100", DefaultContexts.Save, 100, StringLengthComparisonMode.NotMoreThan)]

        public string CodiceApparato { get; set; }

        //[RuleStringLengthComparison("CustomRule.DBAPI.StatusRdL.Descrizione.3999", DefaultContexts.Save, 3999, StringLengthComparisonMode.NotMoreThan)]
        public string Descrizione { get; set; }
        public string NomeRichiedente { get; set; }
        public int CommesseOid { get; set; }
    }
}
