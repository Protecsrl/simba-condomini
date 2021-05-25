using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.Validation;
using DevExpress.Persistent.Validation;
using System.Globalization;
using System.Web.Security;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBAPI
{
    [NavigationItem("Amministrazione"), System.ComponentModel.DisplayName("RdLStatusIns")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "RdLStatusIns")]
    [DefaultClassOptions, Persistent("APIRDLSTATUSINS")]
    //[RuleCombinationOfPropertiesIsUnique("UniqrFestivitaDescrizione", DefaultContexts.Save, "Giorno,Mese", SkipNullOrEmptyValues = false)]
    public class APIRdLStatusIns : XPObject
    {

        public APIRdLStatusIns()
           : base()
        {
        }

        public APIRdLStatusIns(Session session)
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

        private int fRdLOId;
        [Persistent("RDLOID"), DisplayName("RdL Oid")]
        public int RdLOId
        {
            get { return fRdLOId; }
            set { SetPropertyValue<int>("RdLOId", ref fRdLOId, value); }
        }

        private Guid fToken;
        [Persistent("TOKEN"), DisplayName("Token")]
        public Guid Token
        {
            get { return fToken; }
            set { SetPropertyValue<Guid>("Token", ref fToken, value); }
        }

        private long fCodiceRegRdL;
        [Persistent("CODICEREGRDL"), DisplayName("Codice Reg RdL")]
        public long CodiceRegRdL
        {
            get { return fCodiceRegRdL; }
            set { SetPropertyValue<long>("CodiceRegRdL", ref fCodiceRegRdL, value); }
        }


        private int fCodice;
        [Persistent("CODICE"), DisplayName("Codice")]
        public int Codice
        {
            get { return fCodice; }
            set { SetPropertyValue<int>("Codice", ref fCodice, value); }
        }

        private string fCodiceOut;
        [Persistent("CODICEOUT"), DisplayName("Codice Out")]
        [DbType("varchar(100)")]
        public string CodiceOut
        {
            get { return fCodiceOut; }
            set { SetPropertyValue<string>("CodiceOut", ref fCodiceOut, value); }
        }

        private string fPriorita;
        [Persistent("PRIORITA"), DisplayName("Priorita")]
        [DbType("varchar(100)")]
        public string Priorita
        {
            get { return fPriorita; }
            set { SetPropertyValue<string>("Priorita", ref fPriorita, value); }
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(4000), DisplayName("Descrizione")]
        [DbType("varchar(4000)")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        private string fNomeCognomeTecnico;
        [Persistent("NOMECOGNOMETECNICO"), Size(250), DisplayName("NomeCognomeTecnico")]
        [DbType("varchar(250)")]
        public string NomeCognomeTecnico
        {
            get { return fNomeCognomeTecnico; }
            set { SetPropertyValue<string>("NomeCognomeTecnico", ref fNomeCognomeTecnico, value); }
        }

        private string fNote_COMPLETAMENTO;
        [Persistent("NOTECOMPLETAMENTO"), Size(4000), DisplayName("Note COMPLETAMENTO")]
        [DbType("varchar(4000)")]
        public string Note_COMPLETAMENTO
        {
            get { return fNote_COMPLETAMENTO; }
            set { SetPropertyValue<string>("Note_COMPLETAMENTO", ref fNote_COMPLETAMENTO, value); }
        }

        //public string NomeRichiedente { get; set; }
        private string fNomeRichiedente;
        [Persistent("NOMERICHIEDENTE"), Size(250), DisplayName("Nome Richiedente")]
        [DbType("varchar(250)")]
        public string NomeRichiedente
        {
            get { return fNomeRichiedente; }
            set { SetPropertyValue<string>("NomeRichiedente", ref fNomeRichiedente, value); }
        }

        ////caso Intercent-ER NoteRichiedente
        private string fIdUtente;
        [Persistent("IDUTENTE"), Size(50), DisplayName("IdUser")]
        [DbType("varchar(50)")]
        public string IdUtente
        {
            get { return fIdUtente; }
            set { SetPropertyValue<string>("IdUtente", ref fIdUtente, value); }
        }

        #region caso Intercent-ER   ---   NUOVI CAMPI  
        //// 
        private string fRichReparto;
        [Persistent("RICHREPARTO"), Size(50), DisplayName("Richiedente Reparto")]
        [DbType("varchar(50)")]
        public string RichReparto
        {
            get { return fRichReparto; }
            set { SetPropertyValue<string>("RichReparto", ref fRichReparto, value); }
        }

        private string fRichReferente;
        [Persistent("RICHREFERENTE"), Size(50), DisplayName("Richiedente Referente")]
        [DbType("varchar(50)")]
        public string RichReferente
        {
            get { return fRichReferente; }
            set { SetPropertyValue<string>("RichReferente", ref fRichReferente, value); }
        }

        private string fRichTelefono;
        [Persistent("RICHTELEFONO"), Size(20), DisplayName("Richiedente Telefono")]
        [DbType("varchar(20)")]
        public string RichTelefono
        {
            get { return fRichTelefono; }
            set { SetPropertyValue<string>("RichTelefono", ref fRichTelefono, value); }
        }

        #endregion
        private string fNote_SOSPESO;
        [Persistent("NOTESOSPESO"), Size(4000), DisplayName("Note SOSPESO")]
        [DbType("varchar(4000)")]
        public string Note_SOSPESO
        {
            get { return fNote_SOSPESO; }
            set { SetPropertyValue<string>("Note_SOSPESO", ref fNote_SOSPESO, value); }
        }

        //public int CommesseOid { get; set; }

        private string fCodiceCategoria;
        [Persistent("CODICECATEGORIA"), Size(250), DisplayName("Codice Categoria")]
        [DbType("varchar(250)")]
        public string CodiceCategoria
        {
            get { return fCodiceCategoria; }
            set { SetPropertyValue<string>("CodiceCategoria", ref fCodiceCategoria, value); }
        }


        private int fCommesseOid;
        [Persistent("COMMESSAOID"), DisplayName("Commesse Oid")]
        public int CommesseOid
        {
            get { return fCommesseOid; }
            set { SetPropertyValue<int>("CommesseOid", ref fCommesseOid, value); }
        }



        #region    LOCALIZZAZIONE INTERVENTO
        private string fCodiceSITO;
        [Persistent("CODICESITO"), Size(250), DisplayName("Codice SITO")]
        [DbType("varchar(250)")]
        public string CodiceSITO
        {
            get { return fCodiceSITO; }
            set { SetPropertyValue<string>("CodiceSITO", ref fCodiceSITO, value); }
        }
        //public string CodiceSITO { get; set; }

        //public string Codiceimpianto { get; set; }
        private string fCodiceImpianto;
        [Persistent("CODICEIMPIANTO"), Size(250), DisplayName("Codice Impianto")]
        [DbType("varchar(250)")]
        public string CodiceImpianto
        {
            get { return fCodiceImpianto; }
            set { SetPropertyValue<string>("CodiceImpianto", ref fCodiceImpianto, value); }
        }

        private string fCodiceApparato;
        [Persistent("CODICEAPPARATO"), Size(250), DisplayName("Codice Apparato")]
        [DbType("varchar(250)")]
        public string CodiceApparato
        {
            get { return fCodiceApparato; }
            set { SetPropertyValue<string>("CodiceApparato", ref fCodiceApparato, value); }
        }

        private string fCodicePiano;
        [Persistent("CODICEPIANO"), Size(250), DisplayName("Codice Piano")]
        [DbType("varchar(250)")]
        public string CodicePiano
        {
            get { return fCodicePiano; }
            set { SetPropertyValue<string>("CodicePiano", ref fCodicePiano, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        private string fCodiceEdificioCorpo;
        [Persistent("CODICEEDIFICIOCORPO"), Size(250), DisplayName("Codice Immobile Corpo")]
        [DbType("varchar(250)")]
        public string CodiceEdificioCorpo
        {
            get { return fCodiceEdificioCorpo; }
            set { SetPropertyValue<string>("CodiceEdificioCorpo", ref fCodiceEdificioCorpo, value); }
        }



        //public string CodicePiano { get; set; }
        private string fCodiceLocale;
        [Persistent("CODICELOCALE"), Size(250), DisplayName("Codice Locale")]
        [DbType("varchar(250)")]
        public string CodiceLocale
        {
            get { return fCodiceLocale; }
            set { SetPropertyValue<string>("CodiceLocale", ref fCodiceLocale, value); }
        }
        #endregion    LOCALIZZAZIONE INTERVENTO

        // campi date
        #region CAMPI DELLE DATE DI STATO
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        private DateTime fDataOra_UltimoGet;
        [Persistent("DATAORA_ULTIMOGET"), DisplayName("DataOra_UltimoGet")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra_UltimoGet
        {
            get { return fDataOra_UltimoGet; }
            set { SetPropertyValue<DateTime>("DataOra_UltimoGet", ref fDataOra_UltimoGet, value); }
        }
        private DateTime fDataOra_UltimoPut;
        [Persistent("DATAORA_ULTIMOPUT"), DisplayName("DataOra_UltimoPut")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra_UltimoPut
        {
            get { return fDataOra_UltimoPut; }
            set { SetPropertyValue<DateTime>("DataOra_UltimoPut", ref fDataOra_UltimoPut, value); }
        }

        private DateTime fDataOraUpdate;
        [Persistent("DATAORAUPDATE"), DisplayName("DataOraUpdate")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOraUpdate
        {
            get { return fDataOraUpdate; }
            set { SetPropertyValue<DateTime>("DataOraUpdate", ref fDataOraUpdate, value); }
        }

        private DateTime fDataOraRdL;
        [Persistent("DATAORARDL"), DisplayName("DataOraRdL")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOraRdL
        {
            get { return fDataOraRdL; }
            set { SetPropertyValue<DateTime>("DataOraRdL", ref fDataOraRdL, value); }
        }

        private DateTime fDataOra_St_PVISIONE;
        [Persistent("DATAORA_ST_PVISIONE"), DisplayName("DataOra_St_PVISIONE")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra_St_PVISIONE
        {
            get { return fDataOra_St_PVISIONE; }
            set { SetPropertyValue<DateTime>("DataOra_St_PVISIONE", ref fDataOra_St_PVISIONE, value); }
        }

        private DateTime fDataOra_St_SOPRALLUOGO;
        [Persistent("DATAORA_ST_SOPRALLUOGO"), DisplayName("DataOra_St_SOPRALLUOGO")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra_St_SOPRALLUOGO
        {
            get { return fDataOra_St_SOPRALLUOGO; }
            set { SetPropertyValue<DateTime>("DataOra_St_SOPRALLUOGO", ref fDataOra_St_SOPRALLUOGO, value); }
        }

        private DateTime fDataOra_St_COMPLETATO;
        [Persistent("DATAORA_ST_COMPLETATO"), DisplayName("DataOra_St_COMPLETATO")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra_St_COMPLETATO
        {
            get { return fDataOra_St_COMPLETATO; }
            set { SetPropertyValue<DateTime>("DataOra_St_COMPLETATO", ref fDataOra_St_COMPLETATO, value); }
        }

        private DateTime fDataOra_St_ANNULLATO;
        [Persistent("DATAORA_ST_ANNULLATO"), DisplayName("DataOra_St_ANNULLATO")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra_St_ANNULLATO
        {
            get { return fDataOra_St_ANNULLATO; }
            set { SetPropertyValue<DateTime>("DataOra_St_ANNULLATO", ref fDataOra_St_ANNULLATO, value); }
        }

        private DateTime fDataOra_St_SOSPESO;
        [Persistent("DATAORA_ST_SOSPESO"), DisplayName("DataOra_St_SOSPESO")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra_St_SOSPESO
        {
            get { return fDataOra_St_SOSPESO; }
            set { SetPropertyValue<DateTime>("DataOra_St_SOSPESO", ref fDataOra_St_SOSPESO, value); }
        }


        #endregion CAMPI DELLE DATE DI STATO

        private bool fIsReceived;
        [Persistent("ISRECEIVED"), DisplayName("Is Received")]//  ricevuto dal sistema esterno
        public bool IsReceived
        {
            get { return fIsReceived; }
            set { SetPropertyValue<bool>("IsReceived", ref fIsReceived, value); }
        }
        //public bool IsReceived { get; set; }

        private bool fIsSent;
        [Persistent("ISSENT"), DisplayName("Is Sent")] // completato allineamento
        public bool IsSent
        {
            get { return fIsSent; }
            set { SetPropertyValue<bool>("IsSent", ref fIsSent, value); }
        }

        private bool fIsClosed;
        [Persistent("ISCLOSED"), DisplayName("Is Closed")] // completato allineamento
        public bool IsClosed
        {
            get { return fIsClosed; }
            set { SetPropertyValue<bool>("IsClosed", ref fIsClosed, value); }
        }


    }
}
