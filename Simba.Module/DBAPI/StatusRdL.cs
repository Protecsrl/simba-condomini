using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CAMS.Module.Validation;
using DevExpress.Persistent.Validation;
using System.Globalization;
using System.Web.Security;

namespace CAMS.Module.DBAPI
{
    class StatusRdL
    {
        public long Id { get; set; }
        public Guid Token { get; set; }
        public long Codice { get; set; }
        public DateTime DataOra_UltimoGet { get; set; }
        public DateTime DataOra_UltimoPut { get; set; }

        public StatusRdL(Session session)
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
        //private long fCodice;
        //[Persistent("CODICE"), DisplayName("Codice")]
        //public long Codice
        //{
        //    get { return fCodice; }
        //    set { SetPropertyValue<long>("Codice", ref fCodice, value); }
        //}
        private string fCodiceOut;
        [Persistent("CODICE"), DisplayName("Codice")]
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
        public DateTime DataOra_St_SOSPESO { get; set; }
        public DateTime DataOra_St_COMPLETATO { get; set; }
        public string Note_COMPLETAMENTO { get; set; }
        public DateTime DataOra_St_ANNULLATO { get; set; }

        public bool IsReceived { get; set; }
        public bool IsSent { get; set; }   

 
  
        public DateTime DataOra_Sta_SOPRALLUOGO { get; set; }
        public DateTime DataOra_Stato_SOSPESO { get; set; }

        public DateTime DataOra_Stato_COMP { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "La lunghezza di {0} deve essere di almeno {2} caratteri.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Nuova password")]
        //[Required]
        //[StringLength(3999, MinimumLength = 0, ErrorMessage = "è necessario specificare una nota compreso tra 1 e 3999 caratteri.")]
        [RuleStringLengthComparison("CustomRule.DBAPI.StatusRdL.Note.3999", DefaultContexts.Save, 3999, StringLengthComparisonMode.NotMoreThan)]

        public string NoteStato_COMP { get; set; }
        [RuleStringLengthComparison("CustomRule.DBAPI.StatusRdL.Note.3999", DefaultContexts.Save, 3999, StringLengthComparisonMode.NotMoreThan)]
        public DateTime DataOra_Stato_ANN { get; set; }

 
        public DateTime DataOraRdL { get; set; }
        public string CodiceSITO { get; set; }
        public string CodicePiano { get; set; }
        public string CodiceLocale { get; set; }
        public string Codiceimpianto { get; set; }

        [RuleStringLengthComparison("CustomRule.DBAPI.StatusRdL.CodiceApparato.100", DefaultContexts.Save, 100, StringLengthComparisonMode.NotMoreThan)]

        public string CodiceApparato { get; set; }

        [RuleStringLengthComparison("CustomRule.DBAPI.StatusRdL.Descrizione.3999", DefaultContexts.Save, 3999, StringLengthComparisonMode.NotMoreThan)]
        public string Descrizione { get; set; }
        public string NomeRichiedente { get; set; }
        public int CommesseOid { get; set; }        

    }
}
