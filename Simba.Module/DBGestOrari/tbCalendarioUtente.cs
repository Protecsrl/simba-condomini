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

    [DefaultClassOptions, Persistent("TBCALENDARIOUTENTE")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Calendario Richieste Utente")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem("Gestione Orari")]
    [DefaultProperty("keyAppic")]

    [Appearance("tbcalendario.disabilita.sempre", AppearanceItemType.LayoutItem, @"1 = 1",
             TargetItems = "keyAppic;stagione;idcircuito;data;vnoflag;idutente", Context = "Any", Priority = 1, Enabled = false)]

    [Appearance("tbcalendario.flag_eccezione.colore", AppearanceItemType.LayoutItem, @"flag_eccezione = 1",
             TargetItems = "f1startUtente;f1endUtente;f1start;f1end;f2startUtente;f2endUtente;f2start;f2end;f3startUtente;f3endUtente;f3start;f3end;f4startUtente;f4endUtente;f4start;f4end;f5startUtente;f5endUtente;f5start;f5end", Context = "Any", Priority = 1, FontColor = "Red")]

    //Codice	File	Riga	Colonna    int idTicketEAMS; C:\Git_Hub\EAMS\EAMS\CAMS.Module\DBGestOrari\tbcalendario.cs	711	22

    [Appearance("tbcalendario.GOraioModificato.idTicketEAMS.colore", AppearanceItemType.ViewItem, @"idTicketEAMS > 0",
             TargetItems = "*", Context = "Any", Priority = 1, FontColor = "Salmon")]
    //   aggiungi come guid User_Oid     User_Oid = CurrentUserId()

    //[Appearance("tbcalendario.MostraRisorsa.Layout.Visible", AppearanceItemType.LayoutItem, @"Oid = -1",
    //         TargetItems = "panRisorsa;panCausaRimedio;panSmistamentoOperativo;DateOperative;panCompletamento;DataCreazione;UtenteCreatoRichiesta;CodiciRdLOdL", Context = "Any", Priority = 1, Visibility = ViewItemVisibility.Hide)]
    //[RuleCriteria("tbcalendario.orarioFascia1.Validato", DefaultContexts.Save
    //        , @"Iif(f1startUtente >= f1endUtente,0, 1) == 0  ",
    //CustomMessageTemplate = "Errore: La Ora di Dalle deve essere minore della data di Alle.",
    //SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]
    public class tbCalendarioUtente : XPObject
    {
        public tbCalendarioUtente() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public tbCalendarioUtente(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }


        [Persistent("GESTIONEORARI"), System.ComponentModel.DisplayName("GestioneOrari")]
        //[Association("GestioneOrari_tbCalendarioUtente")]
        //  ******************************[DataSourceCriteria("Iif(IsNullOrEmpty('@This.Immobile.Commesse'),Oid > 0,Immobile.Commesse.CommessePrioritas.Single(Priorita))")]
        //[DataSourceCriteria("[<CommessePriorita>][^.Oid == Priorita.Oid And Commesse.Oid == '@This.Immobile.Commesse.Oid']")]
        //[DataSourceCriteria("[<CommessePriorita>][^.Oid == Priorita.Oid And Commesse.Oid = '@This.Immobile.Commesse.Oid' And Gruppo=='@This.Impianto.Gruppo']")]
        //[RuleRequiredField("RuleReq.RdL.Priorita", DefaultContexts.Save, "Priorita è un campo obbligatorio")]
        //[Appearance("RdL.Priorita.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Priorita)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        //[Appearance("RdL.Abilita.Priorita", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        [Delayed(true)]
        public GestioneOrari GestioneOrari
        {
            get { return GetDelayedPropertyValue<GestioneOrari>("GestioneOrari"); }
            set { SetDelayedPropertyValue<GestioneOrari>("GestioneOrari", value); }

        }

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

        [NonPersistent]
        public string GiornoSettimana
        {
            get
            {
                string nomeday = string.Empty;
                if (data != null || data > DateTime.MinValue)
                    nomeday = data.ToString("dddd");
                return nomeday;
            }
        }


        //  CustomDateNDayToLabel      [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        private const string DateAndNomeOfDayEditMask = "dddd dd/MM/yyyy";
        DateTime fdata;
        [XafDisplayName("Data"), ToolTip("Giorno della Settimana")]
        [ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndNomeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndNomeOfDayEditMask)]
        //[EditorAlias(CAMSEditorAliases.CustomDateNDayToLabel)]
        [Appearance("tbCalendario.NomeGiorno.SabatoDomentica.BackColor.Grigio", BackColor = "Silver", FontColor = "Black", Priority = 1, Criteria = "GetDayOfWeek(data) = 0 Or GetDayOfWeek(data) = 6")]
        [Appearance("tbCalendario.NomeGiorno.SabatoDomentica.BackColor.normale", FontColor = "Black", Priority = 1, Criteria = "GetDayOfWeek(data) = 1 Or GetDayOfWeek(data) = 2 Or GetDayOfWeek(data) = 3 Or GetDayOfWeek(data) = 4 Or GetDayOfWeek(data) = 5")]
        public DateTime data
        {
            get { return fdata; }
            set { SetPropertyValue<DateTime>(nameof(data), ref fdata, value); }
        }
       


        //---------------------------
        int fmaxfasce;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int maxfasce
        {
            get { return fmaxfasce; }
            set { SetPropertyValue<int>(nameof(maxfasce), ref fmaxfasce, value); }
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

        


        string fstatoEAMS;
        [Size(3)]
        [Indexed(Name = @"idx_statoEAMS_tbCalendarioUtente")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string statoEAMS
        {
            get { return fstatoEAMS; }
            set { SetPropertyValue<string>(nameof(statoEAMS), ref fstatoEAMS, value); }
        }
        int fwday;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int wday
        {
            get { return fwday; }
            set { SetPropertyValue<int>(nameof(wday), ref fwday, value); }
        }
     

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

//TipoGiornoSettimana fGiornoSettimana;
////[Indexed(Name = @"idx_stato")]
//[Size(3)]
//public TipoGiornoSettimana GiornoSettimana
//{
//    get { return fGiornoSettimana; }
//    set { SetPropertyValue<TipoGiornoSettimana>(nameof(GiornoSettimana), ref fGiornoSettimana, value); }
//}

//string fstagione;
//[Size(255)]
//public string stagione
//{
//    get { return fstagione; }
//    set { SetPropertyValue<string>(nameof(stagione), ref fstagione, value); }
//}

//int fidutente;
//public int idutente
//{
//    get { return fidutente; }
//    set { SetPropertyValue<int>(nameof(idutente), ref fidutente, value); }
//}

//int fidcircuito;
//public int idcircuito
//{
//    get { return fidcircuito; }
//    set { SetPropertyValue<int>(nameof(idcircuito), ref fidcircuito, value); }
//}
// CriteriaOperator.Parse("GetDayOfWeek(Field1)
//[XafDisplayName("Giorno")]
//[PersistentAlias("Iif(data is null,null, GetDayOfWeek(data)  )")]
//[Appearance("tbCalendario.NomeGiorno.SabatoDomentica.BackColor.Grigio", BackColor = "Red", FontColor = "Black", Priority = 1, Criteria = "Giorno = 1")]
//public string Giorno
//{
//    get
//    {
//        var tempObject = EvaluateAlias("Giorno");
//        if (tempObject != null)
//            return Convert.ToString(tempObject);
//        else
//            return null;
//    }
//}

//string fvnoflag;
//[Size(1)]
////[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
//[MemberDesignTimeVisibility(false)]
//public string vnoflag
//{
//    get { return fvnoflag; }
//    set { SetPropertyValue<string>(nameof(vnoflag), ref fvnoflag, value); }
//}
//int fflag_eccezione;
//[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
//public int flag_eccezione
//{
//    get { return fflag_eccezione; }
//    set { SetPropertyValue<int>(nameof(flag_eccezione), ref fflag_eccezione, value); }
//}

//string fstrcal;
//[Size(255)]
//[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
//[MemberDesignTimeVisibility(false)]
//public string strcal
//{
//    get { return fstrcal; }
//    set { SetPropertyValue<string>(nameof(strcal), ref fstrcal, value); }
//}
//string fstrcalst;
//[Size(255)]
//[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
//[MemberDesignTimeVisibility(false)]
//public string strcalst
//{
//    get { return fstrcalst; }
//    set { SetPropertyValue<string>(nameof(strcalst), ref fstrcalst, value); }
//}
//string ftipologia;
//[Size(3)]
//[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
//public string tipologia
//{
//    get { return ftipologia; }
//    set { SetPropertyValue<string>(nameof(tipologia), ref ftipologia, value); }
//}
//int fffs1;
//[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
//[MemberDesignTimeVisibility(false)]
//public int ffs1
//{
//    get { return fffs1; }
//    set { SetPropertyValue<int>(nameof(ffs1), ref fffs1, value); }
//}
//int fffs2;
//[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
//[MemberDesignTimeVisibility(false)]
//public int ffs2
//{
//    get { return fffs2; }
//    set { SetPropertyValue<int>(nameof(ffs2), ref fffs2, value); }
//}
//int fffs3;
//[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
//[MemberDesignTimeVisibility(false)]
//public int ffs3
//{
//    get { return fffs3; }
//    set { SetPropertyValue<int>(nameof(ffs3), ref fffs3, value); }
//}
//int fffs4;
//[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
//[MemberDesignTimeVisibility(false)]
//public int ffs4
//{
//    get { return fffs4; }
//    set { SetPropertyValue<int>(nameof(ffs4), ref fffs4, value); }
//}
//int fffs5;
//[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
//[MemberDesignTimeVisibility(false)]
//public int ffs5
//{
//    get { return fffs5; }
//    set { SetPropertyValue<int>(nameof(ffs5), ref fffs5, value); }
//}
//int fffs6;
//[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
//[MemberDesignTimeVisibility(false)]
//public int ffs6
//{
//    get { return fffs6; }
//    set { SetPropertyValue<int>(nameof(ffs6), ref fffs6, value); }
//}