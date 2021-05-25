using CAMS.Module.Classi;
using CAMS.Module.DBNotifiche;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
Persistent("REGSPEDIZIONIDETT")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Reg. Spedizioni")]
    [ImageName("BO_StateMachine")]
    //[NavigationItem(false)]

    public class RegistroSpedizioniDett : XPObject
    {

        public RegistroSpedizioniDett()
            : base()
        {
        }
        public RegistroSpedizioniDett(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private TipoInvio fTipoInvio;
        [Persistent("TIPOINVIO"), DisplayName("Tipo Invio")]
        public TipoInvio TipoInvio
        {
            get { return fTipoInvio; }
            set { SetPropertyValue<TipoInvio>("TipoInvio", ref fTipoInvio, value); }
        }


        private string fDescrizioneEsito;
        [Size(255), Persistent("DESCRIZIONEESITO"), DisplayName("Descrizione Esito")]
        [DbType("varchar(255)")] //[DbType("varchar2(255)")]
        public string DescrizioneEsito
        {
            get { return fDescrizioneEsito; }
            set { SetPropertyValue<string>("DescrizioneEsito", ref fDescrizioneEsito, value); }
        }

        private RegistroRdL fRegistroRdL;
        [Persistent("REGRDL"), DisplayName("Registro RdL")]
        [ExplicitLoading]
        public RegistroRdL RegistroRdL
        {
            get { return fRegistroRdL; }
            set { SetPropertyValue<RegistroRdL>("RegistroRdL", ref fRegistroRdL, value); }
        }

        private AvvisiSpedizioni fAvvisiSpedizioni;
        [Persistent("AvvisiSpedizioni"), DisplayName("AvvisiSpedizioni")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Association(@"Avvisispedizione_RegistroSpedizioneDett")]
        [ExplicitLoading]
        public AvvisiSpedizioni AvvisiSpedizioni
        {
            get { return fAvvisiSpedizioni; }
            set { SetPropertyValue<AvvisiSpedizioni>("AvvisiSpedizioni", ref fAvvisiSpedizioni, value); }
        }

        private DateTime fDataOra;
        [Persistent("DATAORA"), DisplayName("Data Ora"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        public DateTime DataOra // data e ora della registrazione del log (data creazione record)
        {
            get { return fDataOra; }
            set { SetPropertyValue<DateTime>("DataOra", ref fDataOra, value); }
        }

        private DateTime fDataOraLancio;
        [Persistent("DATAORALANCIO"), DisplayName("Data Ora Lancio"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        [System.ComponentModel.Browsable(false)]
        public DateTime DataOraLancio // data e ora della registrazione del log (data creazione record)
        {
            get { return fDataOraLancio; }
            set { SetPropertyValue<DateTime>("DataOraLancio", ref fDataOraLancio, value); }
        }

        private string fUtenteSO;
        [Size(250), Persistent("UTENTE"), System.ComponentModel.DisplayName("Utente S.O.")]
        [DbType("varchar(250)")]
        public string UtenteSO
        {
            get { return fUtenteSO; }
            set { SetPropertyValue<string>("UtenteSO", ref fUtenteSO, value); }
        }

        private string fOggetto;
        [Size(500), Persistent("OGGETTO"), DisplayName("Oggetto Mail/SMS")]
        [DbType("varchar(500)")]
        public string Oggetto
        {
            get { return fOggetto; }
            set { SetPropertyValue<string>("Oggetto", ref fOggetto, value); }
        }

        private string fCorpo;
        [Persistent("CORPO"), DisplayName("Testo Mail/SMS")]
        [Size(SizeAttribute.Unlimited)]      //[DbType("CLOB")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Corpo
        {
            get { return fCorpo; }
            set { SetPropertyValue<string>("Corpo", ref fCorpo, value); }
        }

        #region MAIL
        private string fMailFrom;
        [Size(255), Persistent("MAILFROM"), DisplayName("Mittente Mail")]
        [DbType("varchar(255)")] //[DbType("varchar2(255)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string MailFrom
        {
            get { return fMailFrom; }
            set { SetPropertyValue<string>("mailFrom", ref fMailFrom, value); }
        }    
        
        //  non usato
        //private string fMittente;
        //[Size(255), Persistent("MITTENTE"), DisplayName("Mittente")]
        //[DbType("varchar(255)")] //[DbType("varchar2(255)")]
        //public string Mittente
        //{
        //    get { return fMittente; }
        //    set { SetPropertyValue<string>("Mittente", ref fMittente, value); }
        //}

        private string fMailCC;
        [Size(255), Persistent("MAILCC"), DisplayName("Destinatari CC Mail")]
        [DbType("varchar(4000)")] //[DbType("varchar2(4000)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string MailCC
        {
            get { return fMailCC; }
            set { SetPropertyValue<string>("mailCC", ref fMailCC, value); }
        }

        private string fEmailDest;
        [Size(4000), Persistent("EMAILDEST"), DisplayName("Destinatari Mail")]
        [DbType("varchar(4000)")] //[DbType("varchar2(4000)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string EmailDest
        {
            get { return fEmailDest; }
            set { SetPropertyValue<string>("EmailDest", ref fEmailDest, value); }
        }


        #endregion
        private string fDataInvio;
        [Size(255), Persistent("DATAINVIO"), DisplayName("Data Invio Mail/Sms a Server")]
        [DbType("varchar(255)")] //[DbType("varchar2(255)")]
        public string DataInvio
        {
            get { return fDataInvio; }
            set { SetPropertyValue<string>("DataInvio", ref fDataInvio, value); }
        }

        private DateTime fDataOraCompletamento;
        [Persistent("DATAORACOMPLETAMENTO"), DisplayName("Data Ora Invio"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        [System.ComponentModel.Browsable(false)]
        public DateTime DataOraCompletamento // data e ora della registrazione del log (data creazione record)
        {
            get { return fDataOraCompletamento; }
            set { SetPropertyValue<DateTime>("DataOraCompletamento", ref fDataOraCompletamento, value); }
        }


        private EsitoInvioMailSMS fEsitoInvioMailSMS;
        [Persistent("ESITO"), DisplayName("Esito")]
        public EsitoInvioMailSMS EsitoInvioMailSMS
        {
            get { return fEsitoInvioMailSMS; }
            set { SetPropertyValue<EsitoInvioMailSMS>("EsitoInvioMailSMS", ref fEsitoInvioMailSMS, value); }
        }

        private string fSMSDest;
        [Size(4000), Persistent("SMSDEST"), DisplayName("Destinatari SMS")]
        [DbType("varchar(4000)")] //[DbType("varchar2(4000)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string SMSDest
        {
            get { return fSMSDest; }
            set { SetPropertyValue<string>("SMSDest", ref fSMSDest, value); }
        }

        private string fSMSID;
        [Size(40), Persistent("SMSID"), DisplayName("SMS ID")]
        [DbType("varchar(40)")] //[DbType("varchar2(40)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string SMSID
        {
            get { return fSMSID; }
            set { SetPropertyValue<string>("SMSID", ref fSMSID, value); }
        }


        private int fNRSMS;
        [Persistent("NRSMS"), DisplayName("Numero SMS Spediti")]     
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int NRSMS
        {
            get { return fNRSMS; }
            set { SetPropertyValue<int>("NRSMS", ref fNRSMS, value); }
        }


        #region SU SMS


        #endregion

        private string fFileMail;
        [Size(4000), Persistent("FILEMAIL"), DisplayName("File Mail")]
        [System.ComponentModel.Browsable(false)]
        [DbType("varchar(4000)")] //[DbType("varchar2(4000)")]
        public string FileMail
        {
            get { return fFileMail; }
            set { SetPropertyValue<string>("FileMail", ref fFileMail, value); }
        }



        [Association(@"RegSpedizioniDett_RegistroSpedizioniSMSDett", typeof(RegistroSpedizioniSMSDett)), Aggregated, DisplayName("Dettaglio invio SMS")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<RegistroSpedizioniSMSDett> RegSpedizioniSMSDetts
        {
            get
            {
                return GetCollection<RegistroSpedizioniSMSDett>("RegSpedizioniSMSDetts");
            }
        }






    }
}


