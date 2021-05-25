//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CAMS.Module.DBAPI
//{
//    class APIAnagrBasePatr
//    {
//    }
//}




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
    [NavigationItem("Amministrazione"), System.ComponentModel.DisplayName("Anagrafiche Base Patrimonio")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Anagrafiche Base Patrimonio")]
    [DefaultClassOptions, Persistent("APIANAGRBASEPATR")]
    [Indices("CodiceOut", "CommessaId", "Id", "Codice", "Classe")]
    //[RuleCombinationOfPropertiesIsUnique("UniqrFestivitaDescrizione", DefaultContexts.Save, "Giorno,Mese", SkipNullOrEmptyValues = false)]
    public class APIAnagrBasePatr : XPObject
    {
        public APIAnagrBasePatr()
           : base()
        {
        }

        public APIAnagrBasePatr(Session session)
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

        private string fCodice;
        [Persistent("CODICE"), DisplayName("Codice")]
        [DbType("varchar(100)")]
        public string Codice
        {
            get { return fCodice; }
            set { SetPropertyValue<string>("Codice", ref fCodice, value); }
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

        private int fCodiceOut;
        [Persistent("CODICEOUT"), DisplayName("CodiceOut")]
        [DbType("varchar(100)")]
        public int CodiceOut
        {
            get { return fCodiceOut; }
            set { SetPropertyValue<int>("CodiceOut", ref fCodiceOut, value); }
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
        
        //CONTROLLER INSNEWEDIFICIOCORPO
        //Provincia varchar(2)
        //IDDistretto int
        //IDArea  int
        //IDPresidio  int
        //CodiceIstat string

        private string fProvincia;
        [Persistent("PROVINCIA"), Size(100), DisplayName("Provincia")]
        public string Provincia
        {
            get { return fProvincia; }
            set { SetPropertyValue<string>("Provincia", ref fProvincia, value); }
        }

        private string fCodiceIstat;
        [Persistent("CODICEISTAT"), Size(100), DisplayName("Codice Istat")]
        public string CodiceIstat
        {
            get { return fCodiceIstat; }
            set { SetPropertyValue<string>("CodiceIstat", ref fCodiceIstat, value); }
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
        
        //CONTROLLER INSNEWEDIFICIOCORPO
        //EdificiCorpiDescrizione string maxLength: 50
        //EdificiID integer($int64)
        //EdificiCodice string maxLength: 3
        //EdificiDescrizione string maxLength: 60
        //ComuniID integer($int64)
        //AttivoDismessoInteresse integer($int64)
        //Indirizzo string maxLength: 50 

        private string fEdificiCorpiDescrizione;
        [Persistent("EDIFICICORPIDESCRIZIONE"), Size(100), DisplayName("Edifici Corpi Descrizione")]
        public string EdificiCorpiDescrizione
        {
            get { return fEdificiCorpiDescrizione; }
            set { SetPropertyValue<string>("EdificiCorpiDescrizione", ref fEdificiCorpiDescrizione, value); }
        }

        private int fEdificiID;
        [Persistent("IDEDIFICIO"), DisplayName("EdificiID")]
        public int EdificiID
        {
            get { return fEdificiID; }
            set { SetPropertyValue<int>("EdificiID", ref fEdificiID, value); }
        }

        private string fEdificiCodice;
        [Persistent("EDIFICICODICE"), Size(100), DisplayName("Edifici Codice")]
        public string EdificiCodice
        {
            get { return fEdificiCodice; }
            set { SetPropertyValue<string>("EdificiCodice", ref fEdificiCodice, value); }
        }

        private string fEdificiDescrizione;
        [Persistent("EDIFICIDESCRIZIONE"), Size(100), DisplayName("Edifici Descrizione")]
        public string EdificiDescrizione
        {
            get { return fEdificiDescrizione; }
            set { SetPropertyValue<string>("EdificiDescrizione", ref fEdificiDescrizione, value); }
        }
     
        private int fComuniID;
        [Persistent("IDCOMUNI"), DisplayName("ComuniID")]
        public int ComuniID
        {
            get { return fComuniID; }
            set { SetPropertyValue<int>("ComuniID", ref fComuniID, value); }
        }

        private int fAttivoDismessoInteresse;
        [Persistent("ATTIVODISMESSOINTERESSE"), DisplayName("AttivoDismessoInteresse")]
        public int AttivoDismessoInteresse
        {
            get { return fAttivoDismessoInteresse; }
            set { SetPropertyValue<int>("AttivoDismessoInteresse", ref fAttivoDismessoInteresse, value); }
        }

        private string fIndirizzo;
        [Persistent("INDIRIZZO"), Size(100), DisplayName("Indirizzo")]
        public string Indirizzo
        {
            get { return fIndirizzo; }
            set { SetPropertyValue<string>("Indirizzo", ref fIndirizzo, value); }
        }



        //CONTROLLER INSNEWEDIFICIOCORPOPIANO
        //EdificiCorpiID integer($int64)
        //TipiPianiID integer($int64)

        private int fEdificiCorpiID;
        [Persistent("EDIFICICORPIID"), DisplayName("EdificiCorpiID")]
        public int EdificiCorpiID
        {
            get { return fEdificiCorpiID; }
            set { SetPropertyValue<int>("EdificiCorpiID", ref fEdificiCorpiID, value); }
        }



        private int fTipiPianiID;
        [Persistent("TIPIPIANIID"), DisplayName("TipiPianiID")]
        public int TipiPianiID
        {
            get { return fTipiPianiID; }
            set { SetPropertyValue<int>("TipiPianiID", ref fTipiPianiID, value); }
        }





        //CONTROLLER INSNEWEDIFICIOCORPOPIAN0STANZA
        //NumeroStanza string maxLength: 15
        //DismissioneDal string (date) dd/MM/yyyy
        //EdificiCorpiPianiID    integer($int64)
        //DestinazioniUsoCEIID integer($int64)
        //EdificiCorpiID integer($int64)
        //TipiPianiID integer($int64)

        private string fNumeroStanza;
        [Persistent("NUMEROSTANZA"), Size(100), DisplayName("Numero Stanza")]
        public string NumeroStanza
        {
            get { return fNumeroStanza; }
            set { SetPropertyValue<string>("NumeroStanza", ref fNumeroStanza, value); }
        }

        private string fDismissioneDal;
        [Persistent("DISMISSIONEDAL"), Size(100), DisplayName("Dismissione Dal")]
        public string DismissioneDal
        {
            get { return fDismissioneDal; }
            set { SetPropertyValue<string>("DismissioneDal", ref fDismissioneDal, value); }
        }



        private int fDestinazioniUsoCEIID;
        [Persistent("DESTINAZIONIUSOCEIID"), DisplayName("DestinazioniUsoCEIID")]
        public int DestinazioniUsoCEIID
        {
            get { return fDestinazioniUsoCEIID; }
            set { SetPropertyValue<int>("DestinazioniUsoCEIID", ref fDestinazioniUsoCEIID, value); }
        }
        

        private int fEdificiCorpiPianiID;
        [Persistent("EDIFICICORPIPIANIID"), DisplayName("EdificiCorpiPianiID")]
        public int EdificiCorpiPianiID
        {
            get { return fEdificiCorpiPianiID; }
            set { SetPropertyValue<int>("EdificiCorpiPianiID", ref fEdificiCorpiPianiID, value); }
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
