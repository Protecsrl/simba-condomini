using System;
using CAMS.Module.Classi;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using CAMS.Module.PropertyEditors;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("RDLNOTE")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "RdL Note")]
    [NavigationItem(false)]
    //[Appearance("RdLNote.TipoNota.Richiedente.Visibile", TargetItems = "Richiedente", Criteria = @"TipoNota = 1", Visibility = ViewItemVisibility.Hide)]   TipoNotaRdL.IsConRichiedente == True
    //[Appearance("RdLNote.TipoNota.Richiedente.Visibile", TargetItems = "Richiedente", Criteria = @"TipoNotaRdL.Oid In(1,3)", Visibility = ViewItemVisibility.Hide)]    TipoNotaRdL.IsConRichiedente != True
    [Appearance("RdLNote.TipoNota.Richiedente.Visibile", TargetItems = "Richiedente;panRichiedente;panBotRdLNoteNeRichiedente", 
                                                         Criteria = @"TipoNotaRdL.IsConRichiedente == false", Visibility = ViewItemVisibility.Hide)]
    [Appearance("RdLNote.TipoNota.panRichiedente.Visibile", AppearanceItemType.LayoutItem, @"TipoNotaRdL.IsConRichiedente == false",
             TargetItems = "panRichiedente", Context = "Any", Priority = 1, Visibility = ViewItemVisibility.Hide)]

    [Appearance("RdLNote.TipoNota.RdL.Visibile", TargetItems = "RdL", Visibility = ViewItemVisibility.Hide)]//[Users][[Oid] = CurrentUserId()].
    [Appearance("RdLNote.dataggiornameto.enable", TargetItems = "DataAggiornamento", Enabled = false)]

    [RuleCriteria("RuleInfo.RdLNote.AvvisonoSollecito", DefaultContexts.Save
                                                   //, @"[TipoNota] = 0 And RdL.UltimoStatoSmistamento.Oid = 4",
                                                   , @"TipoNotaRdL.IsConRichiedente == false And RdL.UltimoStatoSmistamento.Oid = 4",
                              CustomMessageTemplate = @"Avvertenza: Non è corretto inserire un sollecito su intevento Completato! inserire una nuova RdL.",
                            SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Warning)]
    public class RdLNote : XPObject
    {
        public RdLNote() : base() { }
        public RdLNote(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {
                this.Utente = CAMS.Module.Classi.SetVarSessione.CorrenteUser;
                this.Data = DateTime.Now;
                this.TipoNotaRdL = Session.GetObjectByKey<RdLNotaTipo>(1);
            }
        }


        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private RdL fRdL;
        [Persistent("RDL"), Association(@"RdLNote_RdL"), DisplayName("RdL")]
        [ExplicitLoading()]
        public RdL RdL
        {
            get { return fRdL; }
            set { SetPropertyValue<RdL>("RdL", ref fRdL, value); }
        }


        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(4000)]
        [DbType("varchar(4000)")]
        [RuleRequiredField("RdLNote.Descrizione", DefaultContexts.Save, "la Descrizione è un campo obbligatorio")]
        [ImmediatePostData(true)]
        public string Descrizione
        {
            get
            {
                return fDescrizione;
            }
            set
            {
                SetPropertyValue<string>("Descrizione", ref fDescrizione, value);
                OnChanged("Desrdl");
            }
        }

        private DateTime fData;
        [Persistent("DATA"), DisplayName("Data Nota")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data della Nota", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime Data
        {
            get            {                return fData;            }
            set            {                SetPropertyValue<DateTime>("Data", ref fData, value);            }
        }

        private Richiedente fRichiedente;
        [Persistent("RICHIEDENTE"), DisplayName("Richiedente")]
        [DataSourceCriteria("Iif('@This.OidCommessa_Richiedente' > 0,'@This.OidCommessa_Richiedente' = Commesse.Oid,'@This.RdL.Immobile.Commesse.Oid' = Commesse.Oid)")]
        [RuleRequiredField("NoteRdL.Richiedente.condizione.Obbligato", DefaultContexts.Save, SkipNullOrEmptyValues = false, TargetCriteria = "TipoNotaRdL.IsConRichiedente == true")]
        [ExplicitLoading()]
        public Richiedente Richiedente
        {    //TipoNotaRdL.IsConRichiedente     
            get { return fRichiedente; }
            set { SetPropertyValue<Richiedente>("Richiedente", ref fRichiedente, value); }
        }
        //TipoNota
        //[XafDisplayName("Sollecito Cliente")]
        //Sollecito, = 0 -> 1
        //[XafDisplayName("Nota Sala Operativa")]
        //NotaSalaOperativa, = 1
        //[XafDisplayName("Reclamo Cliente")]
        //Reclamo, = 2
        //[XafDisplayName("Nota Tecnico")]
        //NotaTecnico =3
        //private TipoNota fTipoNota;
        //[Persistent("TIPONOTA"), DisplayName("Tipo Nota")]
        ////[RuleRequiredField("RuleReq.TipoNota.RdLNote", DefaultContexts.Save, "Richiedente è un campo obbligatorio")]
        //[ImmediatePostData(true)]
        //[VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        //[System.ComponentModel.Browsable(false)]
        //[ExplicitLoading()]
        //public TipoNota TipoNota        //{      get        {           return fTipoNota;         }        //    set     {          SetPropertyValue<TipoNota>("TipoNota", ref fTipoNota, value);      }        //}


        private RdLNotaTipo fTipoNotaRdL;
        [Persistent("TIPONOTARDL"), DisplayName("Tipo Nota")]
        [DataSourceCriteria("Iif('@This.IsCliente',IsCliente = True,Oid > 0)")]
        [RuleRequiredField("RuleReq.TipoNota.RdLNoteTipo", DefaultContexts.Save, "Tipo Nota Obbligatoria")]
        [VisibleInLookupListView(false), VisibleInListView(false), VisibleInDetailView(false)]
        [ImmediatePostData(true)]
        [ExplicitLoading()]

        public RdLNotaTipo TipoNotaRdL
        {
            get { return fTipoNotaRdL; }
            set { SetPropertyValue<RdLNotaTipo>("TipoNotaRdL", ref fTipoNotaRdL, value); }
        }
               

        private string f_Utente;
        [Persistent("UTENTE"), Size(100), DisplayName("Utente")]
        [DbType("varchar(100)")]
        [VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public string Utente
        {
            get
            {
                return f_Utente;
            }
            set
            {
                SetPropertyValue<string>("Utente", ref f_Utente, value);
            }
        }

        private DateTime? fDataAggiornamento;
        [Persistent("DATAUPDATE"), DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask)]
        [VisibleInLookupListView(false), VisibleInListView(false)]
        [ExplicitLoading()]
        public DateTime? DataAggiornamento
        {
            get
            {
                return fDataAggiornamento;
            }
            set
            {
                SetPropertyValue<DateTime?>("DataAggiornamento", ref fDataAggiornamento, value);
            }
        }






        #region
        [NonPersistent]
        [System.ComponentModel.Browsable(false)]
        public int OidCommessa_Richiedente { get; set; }


        [NonPersistent]
        [System.ComponentModel.Browsable(false)]
        public bool IsCliente
        {
            get
            {
                return !SetVarSessione.VisualizzaSLA;
            }

        }

        [NonPersistent, Size(4000)]
        [VisibleInDetailView(true)]
        [System.ComponentModel.Browsable(false)]
        public string Desrdl
        {
            get
            {

                return OidCommessa_Richiedente.ToString();

            }

        }

        #endregion


    }
}