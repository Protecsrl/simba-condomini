using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.Classi;
using CAMS.Module.DBTask;

namespace CAMS.Module.DBHelp
{
    [DefaultClassOptions,   Persistent("TICKETASSISTENZA")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [NavigationItem(false)]
    public class TicketAssistenza : XPObject
    {
        public TicketAssistenza()
            : base()
        {
        }
        public TicketAssistenza(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
          
            if (this.Oid == -1)
            {
                // this.Commesse.AreaDiPolo.Oid
                this.DataInserimento = DateTime.Now;                
            }
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        private const string DateAndTimeOfDayEditMask1 = "dd/MM/yyyy H:mm";
        private string f_Utente;
        [Size(200), Persistent("UTENTE")]
        [DbType("varchar(200)")]
        public string Utente
        {
            get
            {         return f_Utente;   }
            set
            {        SetPropertyValue<string>("Utente", ref f_Utente, value);     }
        }

        private DateTime fDataInserimento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Inserimento")]
        public DateTime DataInserimento
        {
            get
            {
                return fDataInserimento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataInserimento", ref fDataInserimento, value);
            }
        }

        private string fDescrizione;
        [Size(2000),
        Persistent("DESCRIZIONE"),
        DisplayName("Descrizione")]
        [DbType("varchar(2000)")]
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

        [Persistent("FILE"),        DisplayName("File")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData File
        {
            get
            {
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("File");
            }
            set
            {
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("File", value);
            }
        }

        private TipoStato fStato;
        [Persistent("STATO"),
        DisplayName("Stato")]
        [Appearance("TicketAssistenza.Abilita.Stato", Criteria = "(RuoloCorrente is Null)", Enabled = false)]
        [ImmediatePostData(true)]
        public TipoStato Stato
        {
            get
            {
                return fStato;
            }
            set
            {
                SetPropertyValue<TipoStato>("Stato", ref fStato, value);
            }
        }

        private TipoStatoMAIL fStatoMAIL;
        [Persistent("STATOMAIL"),
        DisplayName("Stato")]
        [Appearance("TicketAssistenza.Abilita.StatoMAIL", Criteria = "(RuoloCorrente is Null)", Enabled = false)]
        [ImmediatePostData(true)]
        public TipoStatoMAIL StatoMAIL
        {
            get
            {
                return fStatoMAIL;
            }
            set
            {
                SetPropertyValue<TipoStatoMAIL>("StatoMAIL", ref fStatoMAIL, value);
            }
        }

        private bool fAssociatoRuolo;
        [Persistent("ASSOCIATORUOLO"),DisplayName("Associato a Ruolo")]
        public bool AssociatoRuolo
        {
            get
            {
                return fAssociatoRuolo;
            }
            set
            {
                SetPropertyValue<bool>("AssociatoRuolo", ref fAssociatoRuolo, value);
            }
        }

        private string fRuolo;
        [Persistent("RUOLO"), DisplayName("Ruolo")]
        //[Appearance("TicketAssistenza.Ruolo", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string Ruolo
        {
            get
            {
                return fRuolo;
            }
            set
            {
                    SetPropertyValue<string>("Ruolo", ref fRuolo, value);
            }
        }

        private string fRuoloCorrente;
        [NonPersistent]
        [Appearance("TicketAssistenza.RuoloCorrente", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string RuoloCorrente
        {
            get
            {
                if (fRuoloCorrente != null)
                {
                    if (!(fRuoloCorrente.Contains("Administrator") || fRuoloCorrente.Contains("GestioneAssistenzaTicket")))
                    {
                        return null;
                    }
                }
                return fRuoloCorrente;
            }
            set
            {
                SetPropertyValue<string>("RuoloCorrente", ref fRuoloCorrente, value);
            }
        }

        private string fGestore;
        [Persistent("USERGESTORE"), DisplayName("Gestore")]
        //[Appearance("TicketAssistenza.RuoloCorrente", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [Appearance("TicketAssistenza.Abilita.Gestore", Enabled = false)]
        public string Gestore
        {
            get
            {
                return fGestore;
            }
            set
            {
                SetPropertyValue<string>("Gestore", ref fGestore, value);
            }
        }

        [Persistent("FILEADMIN"), DisplayName("File Amministrazione")]
        [Appearance("TicketAssistenza.Abilita.FileAdmin", Criteria = "(RuoloCorrente is Null)", Enabled = false)]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [ExplicitLoading()]
        [Delayed(true)]
        public FileData FileAdmin
        {
            get
            {
                //return fFileAdmin;
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("FileAdmin");
            }
            set
            {
                //SetPropertyValue("FileAdmin", ref fFileAdmin, value);
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("FileAdmin", value);
            }
        }
       

        private DateTime fDataInvio;
        [Persistent("DATA_INVIO"), DisplayName("Data Invio")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di invio della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Appearance("TicketAssistenza.Abilita.DataInvio", Criteria = "(RuoloCorrente is Null)", Enabled = false)]
        public DateTime DataInvio
        {
            get
            {
                return fDataInvio;
            }
            set
            {
                SetPropertyValue<DateTime>("DataInvio", ref fDataInvio, value);
            }
        }

        private string fNoteAmministrazione;
        [Size(200),
        Persistent("NOTE_AMMINISTRAZIONE"),
        DisplayName("Note Amministrazione")]
        [DbType("varchar(200)")]
        [Appearance("TicketAssistenza.Abilita.NoteAmministrazione", Criteria = "(RuoloCorrente is Null)", Enabled = false)]
        public string NoteAmministrazione
        {
            get
            {
                return fNoteAmministrazione;
            }
            set
            {
                SetPropertyValue<string>("NoteAmministrazione", ref fNoteAmministrazione, value);
            }
        }

        private DateTime fDataCompletamento;
        [Persistent("DATA_COMPLETAMENTO"),
        DisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Completamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Appearance("TicketAssistenza.Abilita.DataCompletamento", Criteria = "(RuoloCorrente is Null)", Enabled = false)]
        public DateTime DataCompletamento
        {
            get
            {
                return fDataCompletamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataCompletamento", ref fDataCompletamento, value);
            }
        }

        private string fNoteCompletamento;
        [Size(200),
        Persistent("NOTE_COMPLETAMENTO"),
        DisplayName("Note Completamento")]
        [DbType("varchar(200)")]
        [Appearance("TicketAssistenza.Abilita.NoteCompletamento", Criteria = "(RuoloCorrente is Null)", Enabled = false)]
        public string NoteCompletamento
        {
            get
            {
                return fNoteCompletamento;
            }
            set
            {
                SetPropertyValue<string>("NoteCompletamento", ref fNoteCompletamento, value);
            }
        }


        public void InsertTicketbyMobile(int OidTeam, string Messaggio)
        {
            //RisorseTeam> listaSKFiltrata = new XPCollection<SchedaMp>(this.Session).Where(sk => sk.StdApparato.Oid == OidStdApparato).ToList();
            RisorseTeam RT = this.Session.GetObjectByKey<RisorseTeam>(OidTeam);

            if (RT != null)
            {
                
              var xpObjectSpace = DevExpress.ExpressApp.Xpo.XPObjectSpace.FindObjectSpaceByObject(this);                            
                var NuovoTickt = xpObjectSpace.CreateObject<TicketAssistenza>();
                NuovoTickt.Descrizione=Messaggio;
                NuovoTickt.DataInvio = DateTime.Now;
                NuovoTickt.DataInserimento = DateTime.Now;
                NuovoTickt.Stato = TipoStato.Aperto;
                NuovoTickt.Utente = "Sam";
                NuovoTickt.Ruolo = "Administrators";
                NuovoTickt.Gestore = "Sam";

// OID	9
//UTENTE	Sam
//DATAUPDATE	10/09/2015
//DESCRIZIONE	Verifica Indirizzo mail, troppo corto
//FILE	495d6b30-8501-4b1f-9583-77933b0499a2
//STATO	0
//ASSOCIATORUOLO	1
//RUOLO	Administrators
//USERGESTORE	Sam
//FILEADMIN	
//DATA_INVIO	11/09/2015
//NOTE_AMMINISTRAZIONE	verificare anche su altre  maschere
//DATA_COMPLETAMENTO	11/09/2015
//NOTE_COMPLETAMENTO	completato
//OptimisticLockField	3
//GCRecord	            
            }

        }


    }
}
