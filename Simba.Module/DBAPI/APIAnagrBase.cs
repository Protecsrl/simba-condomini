

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
    [NavigationItem("Amministrazione"), System.ComponentModel.DisplayName("Anagrafiche Base")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Anagrafiche Base")]
    [DefaultClassOptions, Persistent("APIANAGRBASE")]
    [Indices("CodiceOut", "CommessaId", "Id", "Codice", "Classe")]
    //[RuleCombinationOfPropertiesIsUnique("UniqrFestivitaDescrizione", DefaultContexts.Save, "Giorno,Mese", SkipNullOrEmptyValues = false)]
    public class APIAnagrBase : XPObject
    {
        public APIAnagrBase()
           : base()
        {
        }

        public APIAnagrBase(Session session)
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

        private int fCodice;
        [Persistent("CODICE"), DisplayName("Codice")]
        public int Codice
        {
            get { return fCodice; }
            set { SetPropertyValue<int>("Codice", ref fCodice, value); }
        }

        private Guid fToken;
        [Persistent("TOKEN"), DisplayName("Token")]
        public Guid Token
        {
            get { return fToken; }
            set { SetPropertyValue<Guid>("Token", ref fToken, value); }
        }

        private string fClasse;
        [Persistent("CLASSE"), DisplayName("Classe")]
        [DbType("varchar(100)")]
        public string Classe
        {
            get { return fClasse; }
            set { SetPropertyValue<string>("Classe", ref fClasse, value); }
        }

        private string fCodiceOut;
        [Persistent("CODICEOUT"), DisplayName("CodiceOut")]
        [DbType("varchar(100)")]
        public string CodiceOut
        {
            get { return fCodiceOut; }
            set { SetPropertyValue<string>("CodiceOut", ref fCodiceOut, value); }
        }


        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(4000), DisplayName("Descrizione")]
        [DbType("varchar(4000)")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }


        private int fCommessaId;
        [Persistent("COMMESSAID"), DisplayName("CommessaId")]
        public int CommessaId
        {
            get { return fCommessaId; }
            set { SetPropertyValue<int>("CommessaId", ref fCommessaId, value); }
        }


        // campi date
        
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
        // PER COMUNE

        //Provincia varchar(2)
        //IDDistretto int
        //IDArea  int
        //IDPresidio  int
        private string fProvincia;
        [Persistent("PROVINCIA"), Size(50), DisplayName("Provincia")]     
        public string Provincia
        {
            get { return fProvincia; }
            set { SetPropertyValue<string>("Provincia", ref fProvincia, value); }
        }

        private int fIDDistretto;
        [Persistent("IDDISTRETTO"), DisplayName("IDDistretto")]
        public int IDDistretto
        {
            get { return fIDDistretto; }
            set { SetPropertyValue<int>("IDDistretto", ref fIDDistretto, value); }
        }

        private int fIDArea;
        [Persistent("IDAREA"), DisplayName("IDArea")]
        public int IDArea
        {
            get { return fIDArea; }
            set { SetPropertyValue<int>("IDArea", ref fIDArea, value); }
        }

        private int fIDPresidio;
        [Persistent("IDPRESIDIO"), DisplayName("IDPresidio")]
        public int IDPresidio
        {
            get { return fIDPresidio; }
            set { SetPropertyValue<int>("IDPresidio", ref fIDPresidio, value); }
        }

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
