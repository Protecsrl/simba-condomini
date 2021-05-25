//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CAMS.Module.DBGestOrari
//{
//    class tbSettimanaTipoUtente
//    {
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CAMS.Module.Classi;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
//namespace CAMS.Module.DBGestOrari
//{
//    class tbCalendarioUtente
//    {
//    }
//}
namespace CAMS.Module.DBGestOrari
{

    [DefaultClassOptions, Persistent("TBSETTIMANATIPOUTENTE")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Settimana Tipo Richieste Utente")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem("Gestione Orari")]
    [DefaultProperty("keyAppic")]

    //[Appearance("tbcalendario.disabilita.sempre", AppearanceItemType.LayoutItem, @"1 = 1",
    //         TargetItems = "keyAppic;stagione;idcircuito;data;vnoflag;idutente", Context = "Any", Priority = 1, Enabled = false)]

    //[Appearance("tbcalendario.flag_eccezione.colore", AppearanceItemType.LayoutItem, @"flag_eccezione = 1",
    //         TargetItems = "f1startUtente;f1endUtente;f1start;f1end;f2startUtente;f2endUtente;f2start;f2end;f3startUtente;f3endUtente;f3start;f3end;f4startUtente;f4endUtente;f4start;f4end;f5startUtente;f5endUtente;f5start;f5end", Context = "Any", Priority = 1, FontColor = "Red")]

    ////Codice	File	Riga	Colonna    int idTicketEAMS; C:\Git_Hub\EAMS\EAMS\CAMS.Module\DBGestOrari\tbcalendario.cs	711	22

    //[Appearance("tbcalendario.GOraioModificato.idTicketEAMS.colore", AppearanceItemType.ViewItem, @"idTicketEAMS > 0",
    //         TargetItems = "*", Context = "Any", Priority = 1, FontColor = "Salmon")]
    //   aggiungi come guid User_Oid     User_Oid = CurrentUserId()

    //[Appearance("tbcalendario.MostraRisorsa.Layout.Visible", AppearanceItemType.LayoutItem, @"Oid = -1",
    //         TargetItems = "panRisorsa;panCausaRimedio;panSmistamentoOperativo;DateOperative;panCompletamento;DataCreazione;UtenteCreatoRichiesta;CodiciRdLOdL", Context = "Any", Priority = 1, Visibility = ViewItemVisibility.Hide)]
    //[RuleCriteria("tbcalendario.orarioFascia1.Validato", DefaultContexts.Save
    //        , @"Iif(f1startUtente >= f1endUtente,0, 1) == 0  ",
    //CustomMessageTemplate = "Errore: La Ora di Dalle deve essere minore della data di Alle.",
    //SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]
    public class tbSettimanaTipoUtente : XPObject
    {
        public tbSettimanaTipoUtente() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public tbSettimanaTipoUtente(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }


        [Persistent("GESTIONENUOVIORARI"), System.ComponentModel.DisplayName("GestioneNuoviOrari")]
        [Delayed(true)]
        public GestioneNuoviOrari GestioneNuoviOrari
        {
            get { return GetDelayedPropertyValue<GestioneNuoviOrari>("GestioneNuoviOrari"); }
            set { SetDelayedPropertyValue<GestioneNuoviOrari>("GestioneNuoviOrari", value); }

        }

        string fstatoEAMS;
        [Size(3)]
        [Indexed(Name = @"idx_statoEAMS_tbSettimanaTipoUtente")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string statoEAMS
        {
            get { return fstatoEAMS; }
            set { SetPropertyValue<string>(nameof(statoEAMS), ref fstatoEAMS, value); }
        }

        //string fstatoEAMS;
        //[Size(3)]
        //[Indexed(Name = @"idx_statoEAMS_tbCalendarioUtente")]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //public string statoEAMS
        //{
        //    get { return fstatoEAMS; }
        //    set { SetPropertyValue<string>(nameof(statoEAMS), ref fstatoEAMS, value); }
        //}


        string fkeyAppic;
        [Size(300)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string keyAppic
        {
            get { return fkeyAppic; }
            set { SetPropertyValue<string>(nameof(keyAppic), ref fkeyAppic, value); }
        }



        [Persistent("CIRCUITO"), System.ComponentModel.DisplayName("Circuiti")]      //  *******************  2018 / 2019  ***********[DataSourceCriteria("Iif(IsNullOrEmpty('@This.Immobile.Commesse'),Oid > 0,Immobile.Commesse.CommessePrioritas.Single(Priorita))")]
        //[DataSourceCriteria("stagione == '@This.Stagione'")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [Delayed(true)]
        public tbcircuiti Circuiti
        {
            get { return GetDelayedPropertyValue<tbcircuiti>("Circuiti"); }
            set { SetDelayedPropertyValue<tbcircuiti>("Circuiti", value); }

        }



        ////  CustomDateNDayToLabel      [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //private const string DateAndNomeOfDayEditMask = "dddd dd/MM/yyyy";
        //DateTime fdata;
        //[XafDisplayName("Data"), ToolTip("Giorno della Settimana")]
        //[ModelDefault("AllowEdit", "False")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndNomeOfDayEditMask + "}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndNomeOfDayEditMask)]
        ////[EditorAlias(CAMSEditorAliases.CustomDateNDayToLabel)]
        //[Appearance("tbCalendario.NomeGiorno.SabatoDomentica.BackColor.Grigio", BackColor = "Silver", FontColor = "Black", Priority = 1, Criteria = "GetDayOfWeek(data) = 0 Or GetDayOfWeek(data) = 6")]
        //[Appearance("tbCalendario.NomeGiorno.SabatoDomentica.BackColor.normale", FontColor = "Black", Priority = 1, Criteria = "GetDayOfWeek(data) = 1 Or GetDayOfWeek(data) = 2 Or GetDayOfWeek(data) = 3 Or GetDayOfWeek(data) = 4 Or GetDayOfWeek(data) = 5")]
        //public DateTime data
        //{
        //    get { return fdata; }
        //    set { SetPropertyValue<DateTime>(nameof(data), ref fdata, value); }
        //}


        TipoGiornoSettimana fTipoGiornoSettimana;
        [Persistent("TIPOGIORNOSETTIMANA"), System.ComponentModel.DisplayName("Giorno Settimana")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoGiornoSettimana TipoGiornoSettimana
        {
            get { return fTipoGiornoSettimana; }
            set { SetPropertyValue<TipoGiornoSettimana>(nameof(TipoGiornoSettimana), ref fTipoGiornoSettimana, value); }
        }


        #region   DATE FASCIA ORARIA 1

        TipoSetOrario ff1startTipoSetOrarioU;
        [DevExpress.ExpressApp.DC.XafDisplayName("F1 Utente Dalle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f1startTipoSetOrarioU
        {
            get { return ff1startTipoSetOrarioU; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f1startTipoSetOrarioU), ref ff1startTipoSetOrarioU, value); }
        }
        TipoSetOrario ff1endTipoSetOrarioU;
        [DevExpress.ExpressApp.DC.XafDisplayName("F1 Utente Alle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f1endTipoSetOrarioU
        {
            get { return ff1endTipoSetOrarioU; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f1endTipoSetOrarioU), ref ff1endTipoSetOrarioU, value); }
        }


        #endregion

        #region   DATE FASCIA ORARIA 2

        TipoSetOrario ff2startTipoSetOrarioU;
        [DevExpress.ExpressApp.DC.XafDisplayName("F2 Utente Dalle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f2startTipoSetOrarioU
        {
            get { return ff2startTipoSetOrarioU; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f2startTipoSetOrarioU), ref ff2startTipoSetOrarioU, value); }
        }
        TipoSetOrario ff2endTipoSetOrarioU;
        [DevExpress.ExpressApp.DC.XafDisplayName("F2 Utente Alle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f2endTipoSetOrarioU
        {
            get { return ff2endTipoSetOrarioU; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f2endTipoSetOrarioU), ref ff2endTipoSetOrarioU, value); }
        }


        #endregion

        #region   DATE FASCIA ORARIA 3

        TipoSetOrario ff3startTipoSetOrarioU;
        [DevExpress.ExpressApp.DC.XafDisplayName("F3 Utente Dalle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f3startTipoSetOrarioU
        {
            get { return ff3startTipoSetOrarioU; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f3startTipoSetOrarioU), ref ff3startTipoSetOrarioU, value); }
        }
        TipoSetOrario ff3endTipoSetOrarioU;
        [DevExpress.ExpressApp.DC.XafDisplayName("F3 Utente Alle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f3endTipoSetOrarioU
        {
            get { return ff3endTipoSetOrarioU; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f3endTipoSetOrarioU), ref ff3endTipoSetOrarioU, value); }
        }


        #endregion

        #region   DATE FASCIA ORARIA 4

        TipoSetOrario ff4startTipoSetOrarioU;
        [DevExpress.ExpressApp.DC.XafDisplayName("F4 Utente Dalle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f4startTipoSetOrarioU
        {
            get { return ff4startTipoSetOrarioU; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f4startTipoSetOrarioU), ref ff4startTipoSetOrarioU, value); }
        }
        TipoSetOrario ff4endTipoSetOrarioU;
        [DevExpress.ExpressApp.DC.XafDisplayName("F4 Utente Alle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f4endTipoSetOrarioU
        {
            get { return ff4endTipoSetOrarioU; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f4endTipoSetOrarioU), ref ff4endTipoSetOrarioU, value); }
        }


        #endregion

        #region   DATE FASCIA ORARIA 5

        TipoSetOrario ff5startTipoSetOrarioU;
        [DevExpress.ExpressApp.DC.XafDisplayName("F5 Utente Dalle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public TipoSetOrario f5startTipoSetOrarioU
        {
            get { return ff5startTipoSetOrarioU; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f5startTipoSetOrarioU), ref ff5startTipoSetOrarioU, value); }
        }
        TipoSetOrario ff5endTipoSetOrarioU;
        [DevExpress.ExpressApp.DC.XafDisplayName("F5 Utente Alle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public TipoSetOrario f5endTipoSetOrarioU
        {
            get { return ff5endTipoSetOrarioU; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f5endTipoSetOrarioU), ref ff5endTipoSetOrarioU, value); }
        }



        #endregion




   
        //int fwday;
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //public int wday
        //{
        //    get { return fwday; }
        //    set { SetPropertyValue<int>(nameof(wday), ref fwday, value); }
        //}


        int fidTicketEAMS;
        [Indexed(Name = @"ticketEAMS")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public int idTicketEAMS
        {
            get { return fidTicketEAMS; }
            set { SetPropertyValue<int>(nameof(idTicketEAMS), ref fidTicketEAMS, value); }
        }

        int fidticket;
        [Indexed(Name = @"ticket")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public int idticket
        {
            get { return fidticket; }
            set { SetPropertyValue<int>(nameof(idticket), ref fidticket, value); }
        }

        DateTime fdatains;
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [ModelDefault("AllowEdit", "False")]
        public DateTime datains
        {
            get { return fdatains; }
            set { SetPropertyValue<DateTime>(nameof(datains), ref fdatains, value); }
        }
        int fversione;
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public int versione
        {
            get { return fversione; }
            set { SetPropertyValue<int>(nameof(versione), ref fversione, value); }
        }



    }
}

