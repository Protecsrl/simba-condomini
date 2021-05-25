using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Drawing;

namespace CAMS.Module.DBAPI
{
    [DefaultClassOptions, Persistent("APIRDLSTATUSDETT")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "API RdL Status Dettaglio")]
    [ImageName("Risorse")]
    [NavigationItem(false)]
    public class APIRdLStatusDett : XPObject
    {
        public APIRdLStatusDett()
     : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.

        }

        public APIRdLStatusDett(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }



        private APIRdLStatus fAPIRdLStatus;
        [Persistent("APIRDLSTATUS"), Association(@"ApiRdLStatus_Dett")]
        [DisplayName("APIRdLStatus")]
        [ExplicitLoading()]
        public APIRdLStatus APIRdLStatus
        {
            get { return fAPIRdLStatus; }
            set { SetPropertyValue<APIRdLStatus>("APIRdLStatus", ref fAPIRdLStatus, value); }
        }

        //Association(@"RdLApparatoSchedeMp_ApparatoSkMP"),
        private int fAnno;
        [Persistent("ANNO"), DisplayName("Anno")]
        [ExplicitLoading]
        public int Anno
        {
            get { return fAnno; }
            set { SetPropertyValue<int>("Anno", ref fAnno, value); }
        }

        private string fCodiceOut;
        [Persistent("CODICEOUT"), DisplayName("Codice Out")]
        [DbType("varchar(100)")]
        public string CodiceOut
        {
            get { return fCodiceOut; }
            set { SetPropertyValue<string>("CodiceOut", ref fCodiceOut, value); }
        }

        private string fStato;
        [Persistent("STATO"), Size(10), DisplayName("Stato")]
        [DbType("varchar(10)")]
        public string Stato
        {
            get { return fStato; }
            set { SetPropertyValue<string>("Stato", ref fStato, value); }
        }

        private string fDescrizioneStato;
        [Persistent("DESCRIZIONESTATO"), Size(100), DisplayName("Descrizione Stato")]
        [DbType("varchar(100)")]
        public string DescrizioneStato
        {
            get { return fDescrizioneStato; }
            set { SetPropertyValue<string>("DescrizioneStato", ref fDescrizioneStato, value); }
        }
        //IDUtente DescrizioneUtente
        //caso Intercent-ER NomeRichiedente
        private string fDescrizioneUtente;
        [Persistent("DESCRIZIONEUTENTE"), Size(60), DisplayName("Nome Utente")]
        [DbType("varchar(60)")]
        public string DescrizioneUtente
        {
            get { return fDescrizioneUtente; }
            set { SetPropertyValue<string>("DescrizioneUtente", ref fDescrizioneUtente, value); }
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


        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        private DateTime fDataOra;
        [Persistent("DATAORA"), DisplayName("DataOra")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra
        {
            get { return fDataOra; }
            set { SetPropertyValue<DateTime>("DataOra", ref fDataOra, value); }
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(4000), DisplayName("Descrizione")]
        [DbType("varchar(4000)")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        //caso I  •	CodiceTecnico
        private string fDescrizioneTecnico;
        [Persistent("DESCRIZIONETECNICO"), Size(60), DisplayName("Nome TECNICO")]
        [DbType("varchar(250)")]
        public string DescrizioneTecnico
        {
            get { return fDescrizioneTecnico; }
            set { SetPropertyValue<string>("DescrizioneTecnico", ref fDescrizioneTecnico, value); }
        }

        //caso I  •	CodiceTecnico
        private string fCodiceTecnico;
        [Persistent("CODICETECNICO"), Size(60), DisplayName("Codice TECNICO")]
        [DbType("varchar(250)")]
        public string CodiceTecnico
        {
            get { return fCodiceTecnico; }
            set { SetPropertyValue<string>("CodiceTecnico", ref fCodiceTecnico, value); }
        }

        private bool fIsReceived;
        [Persistent("ISRECEIVED"), DisplayName("Is Received")]//  ricevuto dal sistema esterno
        public bool IsReceived
        {
            get { return fIsReceived; }
            set { SetPropertyValue<bool>("IsReceived", ref fIsReceived, value); }
        }

        private string FCodiceAudit;
        [Persistent("CODICEAUDIT"), DisplayName("Codice Audit")]
        public string CodiceAudit
        {
            get { return FCodiceAudit; }
            set { SetPropertyValue<string>("CodiceAudit", ref FCodiceAudit, value); }
        }

    }


}




