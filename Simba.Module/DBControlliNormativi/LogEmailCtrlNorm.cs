using System.Collections.Generic;
using CAMS.Module.Classi;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBControlliNormativi
{
    [DefaultClassOptions, Persistent("LOGEMAILCTRLNORM")]
    [System.ComponentModel.DisplayName("eMail Inviate")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "eMail Inviate")]
    [ImageName("NewMail")]
    [NavigationItem("Avvisi Periodici")]
    public class LogEmailCtrlNorm : XPObject
    {
        public LogEmailCtrlNorm() : base() { }
        public LogEmailCtrlNorm(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private string fEmailDest;
        [Size(4000), Persistent("EMAILDEST"), DisplayName("Destinatari Mail")]
        [DbType("varchar(4000)")] //[DbType("varchar2(4000)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string EmailDest
        {
            get
            {
                return fEmailDest;
            }
            set
            {
                SetPropertyValue<string>("EmailDest", ref fEmailDest, value);
            }
        }


        private string fMittente;
        [Size(255), Persistent("MITTENTE"), DisplayName("Mittente")]
        [DbType("varchar(255)")]    //[DbType("varchar2(255)")]
        public string Mittente
        {
            get
            {
                return fMittente;
            }
            set
            {
                SetPropertyValue<string>("Mittente", ref fMittente, value);
            }
        }

        private string fOggetto;
        [Size(255), Persistent("OGGETTO"), DisplayName("Oggetto Mail")]
        [DbType("varchar(500)")] //[DbType("varchar2(500)")]
        public string Oggetto
        {
            get
            {
                return fOggetto;
            }
            set
            {
                SetPropertyValue<string>("Oggetto", ref fOggetto, value);
            }
        }

        private string fDataInvio;
        [Size(255), Persistent("DATAINVIO"), DisplayName("Data Invio Mail")]
        [DbType("varchar(255)")] //[DbType("varchar2(255)")]
        public string DataInvio
        {
            get
            {
                return fDataInvio;
            }
            set
            {
                SetPropertyValue<string>("DataInvio", ref fDataInvio, value);
            }
        }

        private string fMailCC;
        [Size(255), Persistent("MAILCC"), DisplayName("Destinatari CC Mail")]
        [DbType("varchar(255)")] //     [DbType("varchar2(255)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string MailCC
        {
            get
            {
                return fMailCC;
            }
            set
            {
                SetPropertyValue<string>("mailCC", ref fMailCC, value);
            }
        }

        private string fMailFrom;
        [Size(255), Persistent("MAILFROM"), DisplayName("Mittente Mail")]
        [DbType("varchar(255)")] //[DbType("varchar2(255)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string MailFrom
        {
            get
            {
                return fMailFrom;
            }
            set
            {
                SetPropertyValue<string>("mailFrom", ref fMailFrom, value);
            }
        }

        private string fCorpo;
        [Persistent("CORPO"), DisplayName("Testo Mail")]
        [Size(SizeAttribute.Unlimited)]
//#if (   DEBUG && !MYTEST)
//        [DbType("CLOB")]
//#elif (!DEBUG && MYTEST)
//        [DbType("CLOB")]
//#endif
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


        private EsitoInvioMailSMS fEsitoInvioMailSMS;
        [Persistent("ESITO"), DisplayName("Esito")]
        public EsitoInvioMailSMS EsitoInvioMailSMS
        {
            get
            {
                return fEsitoInvioMailSMS;
            }
            set
            {
                SetPropertyValue<EsitoInvioMailSMS>("EsitoInvioMailSMS", ref fEsitoInvioMailSMS, value);
            }
        }

        [Association(@"LogEmailCtrlNorm_Controlli", typeof(ControlliNormativiRifLog)), DevExpress.Xpo.DisplayName("Avvisi Periodici Inviati")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<ControlliNormativiRifLog> ControlliNormativiRifLogS
        {
            get
            {
                return GetCollection<ControlliNormativiRifLog>("ControlliNormativiRifLogS");
            }
        }

        [Association(@"LogEmailCtrlNorm_Destinatari", typeof(LogEmailCtrlNormRifDestinatari)), DevExpress.Xpo.DisplayName("Elenco Destinatari")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<LogEmailCtrlNormRifDestinatari> LogEmailCtrlNormRifDestinataris
        {
            get
            {
                return GetCollection<LogEmailCtrlNormRifDestinatari>("LogEmailCtrlNormRifDestinataris");
            }
        }

        private string fFileMail;
        [Size(4000), Persistent("FILEMAIL"), DisplayName("File Mail")]
        [System.ComponentModel.Browsable(false)]
        [DbType("varchar(4000)")] //[DbType("varchar2(4000)")]
        public string FileMail
        {
            get
            {
                return fFileMail;
            }
            set
            {
                SetPropertyValue<string>("FileMail", ref fFileMail, value);
            }
        }


#region Clona Log
        public void InsertLogEmailCtrlNormRifDestinatari(ref LogEmailCtrlNorm NuovoLogEmailCtrlNorm, IList<LogEmailCtrlNormRifDestinatari> _LogEmailCtrlNormRifDestinatari)
        {
            foreach (LogEmailCtrlNormRifDestinatari item in _LogEmailCtrlNormRifDestinatari)
            {
                NuovoLogEmailCtrlNorm.LogEmailCtrlNormRifDestinataris.Add(new LogEmailCtrlNormRifDestinatari(Session)
                {
                    Destinatari = item.Destinatari,
                    //  LogEmailCtrlNorm = NuovoLogEmailCtrlNorm
                });
            }
        }

        public void InsertControlliNormativiRifLog(ref LogEmailCtrlNorm NuovoLogEmailCtrlNorm, IList<ControlliNormativiRifLog> _ControlliNormativiRifLog)
        {
            foreach (ControlliNormativiRifLog item in _ControlliNormativiRifLog)
            {
                NuovoLogEmailCtrlNorm.ControlliNormativiRifLogS.Add(new ControlliNormativiRifLog(Session)
                {
                    ControlliNormativi = item.ControlliNormativi,
                });
            }
        }
#endregion


        public override string ToString()
        {
            return string.Format("Cod. eMail. {0}({1})", this.Oid, this.Oggetto);
        }
    }
}
