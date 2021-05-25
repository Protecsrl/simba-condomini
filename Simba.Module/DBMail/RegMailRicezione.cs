
using CAMS.Module.Classi;
using CAMS.Module.DBAux;
using CAMS.Module.DBNotifiche;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
namespace CAMS.Module.DBMail
{
    [DefaultClassOptions,
Persistent("REGMAILRICEZIONE")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Reg. Lettura Mail")]
    [ImageName("BO_StateMachine")]
    [NavigationItem("Segnalazioni")]

    public class RegMailRicezione : XPObject
    {

        public RegMailRicezione()
            : base()
        {
        }
        public RegMailRicezione(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fIDMail;
        [Size(4000), Persistent("IDMAIL"), DisplayName("ID Mail")]
        [DbType("varchar(4000)")]
        public string IDMail
        {
            get { return fIDMail; }
            set { SetPropertyValue<string>("IDMail", ref fIDMail, value); }
        }

        private string fMail;
        [Size(4000), Persistent("MAIL"), DisplayName("Mail Ricezione")]
        [DbType("varchar(4000)")]
        public string Mail
        {
            get { return fMail; }
            set { SetPropertyValue<string>("Mail", ref fMail, value); }
        }


        private DateTime fDataOraMail;
        [Persistent("DATAORAEMAIL"), DisplayName("Data Ora Mail"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        public DateTime DataOraMail // data e ora della registrazione del log (data creazione record)
        {
            get { return fDataOraMail; }
            set { SetPropertyValue<DateTime>("DataOraMail", ref fDataOraMail, value); }
        }
        
        private string fOggetto;
        [Size(4000), Persistent("OGGETTO"), DisplayName("Oggetto Mail")]
        [DbType("varchar(4000)")]
        public string Oggetto
        {
            get { return fOggetto; }
            set { SetPropertyValue<string>("Oggetto", ref fOggetto, value); }
        }

        private string fCorpo;
        [Persistent("CORPO"), DisplayName("Testo Mail")]
        [Size(SizeAttribute.Unlimited)]      //[DbType("CLOB")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Corpo
        {
            get { return fCorpo; }
            set { SetPropertyValue<string>("Corpo", ref fCorpo, value); }
        }
        
        private string fMailFrom;
        [Size(4000), Persistent("MAILFROM"), DisplayName("Mittente Mail")]
        [DbType("varchar(4000)")] //[DbType("varchar2(255)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string MailFrom
        {
            get { return fMailFrom; }
            set { SetPropertyValue<string>("mailFrom", ref fMailFrom, value); }
        }

        private string fMailcc;
        [Size(4000), Persistent("MAILCC"), DisplayName("CC Mail")]
        [DbType("varchar(4000)")] //[DbType("varchar2(255)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Mailcc
        {
            get { return fMailcc; }
            set { SetPropertyValue<string>("Mailcc", ref fMailcc, value); }
        }


        private FileData fFileMail;
        [ Persistent("FILE_MAIL"), DisplayName("File Mail")]      
        public FileData FileMail
        {
            get { return fFileMail; }
            set { SetPropertyValue<FileData>("FileMail", ref fFileMail, value); }
        }

        private FileDataMail fFileDataMail;
        [Persistent("FILEEMAIL"), DisplayName("File eMail")]
        public FileDataMail FileDataMail
        {
            get { return fFileDataMail; }
            set { SetPropertyValue<FileDataMail>("FileDataMail", ref fFileDataMail, value); }
        }

        private DateTime fDataUpdate;
        [Persistent("DATAUPDATE"), DisplayName("Data Ora Update"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        public DateTime DataUpdate // data e ora della registrazione del log (data creazione record)
        {
            get { return fDataUpdate; }
            set { SetPropertyValue<DateTime>("DataUpdate", ref fDataUpdate, value); }
        }


    }
}


