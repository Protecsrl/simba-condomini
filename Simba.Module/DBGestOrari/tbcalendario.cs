using CAMS.Module.Classi;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Drawing;

namespace CAMS.Module.DBGestOrari
{

    [DefaultClassOptions, Persistent("TBCALENDARIO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Calendario")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem("Gestione Orari")]
    [DefaultProperty("keyAppic")]

    [Appearance("tbcalendario.disabilita.sempre", AppearanceItemType.LayoutItem, @"1 = 1",
             TargetItems = "keyAppic;stagione;idcircuito;data;vnoflag;idutente", Context = "Any", Priority = 1, Enabled = false)]

    [Appearance("tbcalendario.flag_eccezione.colore", AppearanceItemType.LayoutItem, @"flag_eccezione = 1",
             TargetItems = "f1startUtente;f1endUtente;f1start;f1end;f2startUtente;f2endUtente;f2start;f2end;f3startUtente;f3endUtente;f3start;f3end;f4startUtente;f4endUtente;f4start;f4end;f5startUtente;f5endUtente;f5start;f5end",
                           Context = "Any", Priority = 1, FontColor = "Red")]

    //Codice	File	Riga	Colonna    int idTicketEAMS; C:\Git_Hub\EAMS\EAMS\CAMS.Module\DBGestOrari\tbcalendario.cs	711	22

    [Appearance("tbcalendario.GOraioModificato.idTicketEAMS.colore", AppearanceItemType.ViewItem, @"idTicketEAMS > 0",
             TargetItems = "*", Context = "Any", Priority = 1,  FontStyle = FontStyle.Bold)]

    [Appearance("tbcalendario.GOraioModificato.SecurityUser.colore1", AppearanceItemType.ViewItem, @"SecurityUser.Oid = CurrentUserId() and idTicketEAMS > 0",
             TargetItems = "idTicketEAMS", Context = "Any", Priority = 2, BackColor = "LightCoral")]
    [Appearance("tbcalendario.GOraioModificato.SecurityUser.colore2", AppearanceItemType.ViewItem, @"SecurityUser.Oid != CurrentUserId() and idTicketEAMS > 0",
             TargetItems = "idTicketEAMS", Context = "Any", Priority = 2, BackColor = "LightBlue")]


    [Appearance("tbCalendario.NomeGiorno.SabatoDomentica.BackColor.Grigio", BackColor = "Silver", TargetItems = "data;GiornoSettimana",
        FontColor = "Black", Priority = 1, Criteria = "GetDayOfWeek(data) = 0 Or GetDayOfWeek(data) = 6")]
    [Appearance("tbCalendario.NomeGiorno.SabatoDomentica.BackColor.normale", FontColor = "Black",
        TargetItems = "data;GiornoSettimana", 
        Priority = 1, Criteria = "GetDayOfWeek(data) = 1 Or GetDayOfWeek(data) = 2 Or GetDayOfWeek(data) = 3 Or GetDayOfWeek(data) = 4 Or GetDayOfWeek(data) = 5")]


    //   aggiungi come guid User_Oid     User_Oid = CurrentUserId()

    //[Appearance("tbcalendario.MostraRisorsa.Layout.Visible", AppearanceItemType.LayoutItem, @"Oid = -1",
    //         TargetItems = "panRisorsa;panCausaRimedio;panSmistamentoOperativo;DateOperative;panCompletamento;DataCreazione;UtenteCreatoRichiesta;CodiciRdLOdL", Context = "Any", Priority = 1, Visibility = ViewItemVisibility.Hide)]
    //[RuleCriteria("tbcalendario.orarioFascia1.Validato", DefaultContexts.Save
    //        , @"Iif(f1startUtente >= f1endUtente,0, 1) == 0  ",
    //CustomMessageTemplate = "Errore: La Ora di Dalle deve essere minore della data di Alle.",
    //SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]
    public class tbcalendario : XPObject
    {
        public tbcalendario() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public tbcalendario(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }



        string fkeyAppic;
        [Size(300)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string keyAppic
        {
            get { return fkeyAppic; }
            set { SetPropertyValue<string>(nameof(keyAppic), ref fkeyAppic, value); }
        }

        string fstagione;
        [Size(255)]
        public string stagione
        {
            get { return fstagione; }
            set { SetPropertyValue<string>(nameof(stagione), ref fstagione, value); }
        }

        int fidutente;
        public int idutente
        {
            get { return fidutente; }
            set { SetPropertyValue<int>(nameof(idutente), ref fidutente, value); }
        }

        int fidcircuito;
        public int idcircuito
        {
            get { return fidcircuito; }
            set { SetPropertyValue<int>(nameof(idcircuito), ref fidcircuito, value); }
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
        //TipoGiornoSettimana fGiornoSettimana;
        ////[Indexed(Name = @"idx_stato")]
        //[Size(3)]
        //public TipoGiornoSettimana GiornoSettimana
        //{
        //    get { return fGiornoSettimana; }
        //    set { SetPropertyValue<TipoGiornoSettimana>(nameof(GiornoSettimana), ref fGiornoSettimana, value); }
        //}

        //  CustomDateNDayToLabel      [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        private const string DateAndNomeOfDayEditMask = "dd/MM/yyyy";
        DateTime fdata;
        [XafDisplayName("Data"), ToolTip("Giorno della Settimana")]
        [ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndNomeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndNomeOfDayEditMask)]
        //[EditorAlias(CAMSEditorAliases.CustomDateNDayToLabel)]
      //  [Appearance("tbCalendario.NomeGiorno.SabatoDomentica.BackColor.Grigio", BackColor = "Silver", FontColor = "Black", Priority = 1, Criteria = "GetDayOfWeek(data) = 0 Or GetDayOfWeek(data) = 6")]
      //  [Appearance("tbCalendario.NomeGiorno.SabatoDomentica.BackColor.normale", FontColor = "Black", Priority = 1, Criteria = "GetDayOfWeek(data) = 1 Or GetDayOfWeek(data) = 2 Or GetDayOfWeek(data) = 3 Or GetDayOfWeek(data) = 4 Or GetDayOfWeek(data) = 5")]
        public DateTime data
        {
            get { return fdata; }
            set { SetPropertyValue<DateTime>(nameof(data), ref fdata, value); }
        }
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

        string fvnoflag;
        [Size(1)]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [MemberDesignTimeVisibility(false)]
        public string vnoflag
        {
            get { return fvnoflag; }
            set { SetPropertyValue<string>(nameof(vnoflag), ref fvnoflag, value); }
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

        //string ff1startUtente;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeFascia1StartEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public string f1startUtente
        //{
        //    get { return ff1startUtente; }
        //    set { SetPropertyValue<string>(nameof(f1startUtente), ref ff1startUtente, value); }
        //}

        //string ff1endUtente;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeFascia1EndEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public string f1endUtente
        //{
        //    get { return ff1endUtente; }
        //    set { SetPropertyValue<string>(nameof(f1endUtente), ref ff1endUtente, value); }
        //}


        TipoSetOrario ff1startTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F1 Dalle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f1startTipoSetOrario
        {
            get { return ff1startTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f1startTipoSetOrario), ref ff1startTipoSetOrario, value); }
        }
        TipoSetOrario ff1endTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F1 Alle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f1endTipoSetOrario
        {
            get { return ff1endTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f1endTipoSetOrario), ref ff1endTipoSetOrario, value); }
        }


        //string ff1start;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public string f1start
        //{
        //    get { return ff1start; }
        //    set { SetPropertyValue<string>(nameof(f1start), ref ff1start, value); }
        //}
        //string ff1end;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public string f1end
        //{
        //    get { return ff1end; }
        //    set { SetPropertyValue<string>(nameof(f1end), ref ff1end, value); }
        //}
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

        //string ff2startUtente;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public string f2startUtente
        //{
        //    get { return ff2startUtente; }
        //    set { SetPropertyValue<string>(nameof(f2startUtente), ref ff2startUtente, value); }
        //}

        //string ff2endUtente;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public string f2endUtente
        //{
        //    get { return ff2endUtente; }
        //    set { SetPropertyValue<string>(nameof(f2endUtente), ref ff2endUtente, value); }
        //}

        TipoSetOrario ff2startTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F2 Dalle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f2startTipoSetOrario
        {
            get { return ff2startTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f2startTipoSetOrario), ref ff2startTipoSetOrario, value); }
        }
        TipoSetOrario ff2endTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F2 Alle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f2endTipoSetOrario
        {
            get { return ff2endTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f2endTipoSetOrario), ref ff2endTipoSetOrario, value); }
        }

        //string ff2start;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public string f2start
        //{
        //    get { return ff2start; }
        //    set { SetPropertyValue<string>(nameof(f2start), ref ff2start, value); }
        //}
        //string ff2end;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public string f2end
        //{
        //    get { return ff2end; }
        //    set { SetPropertyValue<string>(nameof(f2end), ref ff2end, value); }
        //}
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

        //string ff3startUtente;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public string f3startUtente
        //{
        //    get { return ff3startUtente; }
        //    set { SetPropertyValue<string>(nameof(f3startUtente), ref ff3startUtente, value); }
        //}
        //string ff3endUtente;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public string f3endUtente
        //{
        //    get { return ff3endUtente; }
        //    set { SetPropertyValue<string>(nameof(f3endUtente), ref ff3endUtente, value); }
        //}

        TipoSetOrario ff3startTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F3 Dalle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f3startTipoSetOrario
        {
            get { return ff3startTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f3startTipoSetOrario), ref ff3startTipoSetOrario, value); }
        }
        TipoSetOrario ff3endTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F3 Alle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f3endTipoSetOrario
        {
            get { return ff3endTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f3endTipoSetOrario), ref ff3endTipoSetOrario, value); }
        }

        //string ff3start;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public string f3start
        //{
        //    get { return ff3start; }
        //    set { SetPropertyValue<string>(nameof(f3start), ref ff3start, value); }
        //}
        //string ff3end;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public string f3end
        //{
        //    get { return ff3end; }
        //    set { SetPropertyValue<string>(nameof(f3end), ref ff3end, value); }
        //}
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


        //string ff4startUtente;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public string f4startUtente
        //{
        //    get { return ff4startUtente; }
        //    set { SetPropertyValue<string>(nameof(f4startUtente), ref ff4startUtente, value); }
        //}
        //string ff4endUtente;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public string f4endUtente
        //{
        //    get { return ff4endUtente; }
        //    set { SetPropertyValue<string>(nameof(f4endUtente), ref ff4endUtente, value); }
        //}

        TipoSetOrario ff4startTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F4 Dalle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f4startTipoSetOrario
        {
            get { return ff4startTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f4startTipoSetOrario), ref ff4startTipoSetOrario, value); }
        }
        TipoSetOrario ff4endTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F4 Alle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoSetOrario f4endTipoSetOrario
        {
            get { return ff4endTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f4endTipoSetOrario), ref ff4endTipoSetOrario, value); }
        }

        //string ff4start;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public string f4start
        //{
        //    get { return ff4start; }
        //    set { SetPropertyValue<string>(nameof(f4start), ref ff4start, value); }
        //}
        //string ff4end;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //public string f4end
        //{
        //    get { return ff4end; }
        //    set { SetPropertyValue<string>(nameof(f4end), ref ff4end, value); }
        //}
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

        //string ff5startUtente;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public string f5startUtente
        //{
        //    get { return ff5startUtente; }
        //    set { SetPropertyValue<string>(nameof(f5startUtente), ref ff5startUtente, value); }
        //}
        //string ff5endUtente;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public string f5endUtente
        //{
        //    get { return ff5endUtente; }
        //    set { SetPropertyValue<string>(nameof(f5endUtente), ref ff5endUtente, value); }
        //}

        TipoSetOrario ff5startTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F5 Dalle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public TipoSetOrario f5startTipoSetOrario
        {
            get { return ff5startTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f5startTipoSetOrario), ref ff5startTipoSetOrario, value); }
        }
        TipoSetOrario ff5endTipoSetOrario;
        [DevExpress.ExpressApp.DC.XafDisplayName("F5 Alle")]
        [EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public TipoSetOrario f5endTipoSetOrario
        {
            get { return ff5endTipoSetOrario; }
            set { SetPropertyValue<TipoSetOrario>(nameof(f5endTipoSetOrario), ref ff5endTipoSetOrario, value); }
        }

        //string ff5start;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public string f5start
        //{
        //    get { return ff5start; }
        //    set { SetPropertyValue<string>(nameof(f5start), ref ff5start, value); }
        //}
        //string ff5end;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public string f5end
        //{
        //    get { return ff5end; }
        //    set { SetPropertyValue<string>(nameof(f5end), ref ff5end, value); }
        //}

        #endregion

        //string ff6start;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //[MemberDesignTimeVisibility(false)]
        //public string f6start
        //{
        //    get { return ff6start; }
        //    set { SetPropertyValue<string>(nameof(f6start), ref ff6start, value); }
        //}
        //string ff6end;
        //[Size(5)]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //[MemberDesignTimeVisibility(false)]
        //public string f6end
        //{
        //    get { return ff6end; }
        //    set { SetPropertyValue<string>(nameof(f6end), ref ff6end, value); }
        //}


        string fstato;
        [Size(3)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string stato
        {
            get { return fstato; }
            set { SetPropertyValue<string>(nameof(stato), ref fstato, value); }
        }
        int fwday;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int wday
        {
            get { return fwday; }
            set { SetPropertyValue<int>(nameof(wday), ref fwday, value); }
        }
        string fstrcal;
        [Size(255)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [MemberDesignTimeVisibility(false)]
        public string strcal
        {
            get { return fstrcal; }
            set { SetPropertyValue<string>(nameof(strcal), ref fstrcal, value); }
        }
        string fstrcalst;
        [Size(255)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [MemberDesignTimeVisibility(false)]
        public string strcalst
        {
            get { return fstrcalst; }
            set { SetPropertyValue<string>(nameof(strcalst), ref fstrcalst, value); }
        }
        string ftipologia;
        [Size(3)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string tipologia
        {
            get { return ftipologia; }
            set { SetPropertyValue<string>(nameof(tipologia), ref ftipologia, value); }
        }
        int fffs1;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [MemberDesignTimeVisibility(false)]
        public int ffs1
        {
            get { return fffs1; }
            set { SetPropertyValue<int>(nameof(ffs1), ref fffs1, value); }
        }
        int fffs2;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [MemberDesignTimeVisibility(false)]
        public int ffs2
        {
            get { return fffs2; }
            set { SetPropertyValue<int>(nameof(ffs2), ref fffs2, value); }
        }
        int fffs3;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [MemberDesignTimeVisibility(false)]
        public int ffs3
        {
            get { return fffs3; }
            set { SetPropertyValue<int>(nameof(ffs3), ref fffs3, value); }
        }
        int fffs4;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [MemberDesignTimeVisibility(false)]
        public int ffs4
        {
            get { return fffs4; }
            set { SetPropertyValue<int>(nameof(ffs4), ref fffs4, value); }
        }
        int fffs5;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [MemberDesignTimeVisibility(false)]
        public int ffs5
        {
            get { return fffs5; }
            set { SetPropertyValue<int>(nameof(ffs5), ref fffs5, value); }
        }
        int fffs6;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [MemberDesignTimeVisibility(false)]
        public int ffs6
        {
            get { return fffs6; }
            set { SetPropertyValue<int>(nameof(ffs6), ref fffs6, value); }
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
        int fflag_eccezione;
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public int flag_eccezione
        {
            get { return fflag_eccezione; }
            set { SetPropertyValue<int>(nameof(flag_eccezione), ref fflag_eccezione, value); }
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
        [Persistent("SECURITYUSERID"), XafDisplayName("Security User")]
        //[RuleUniqueValue("ParametriPivot.SecuritySystemUser.", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        [Delayed(true)]
        [Browsable(false)]
        public SecuritySystemUser SecurityUser
        {
            get { return GetDelayedPropertyValue<SecuritySystemUser>("SecurityUser"); }
            set { SetDelayedPropertyValue<SecuritySystemUser>("SecurityUser", value); }

        }

        //private Boolean GetValidateFascia1()
        //{             
        //    int anno = data.Year;
        //    int Mese = data.Month;
        //    int dd = data.Day;
        //    //int hh = f1startUtente.Split(":");
        //    //string s = "You win some. You lose some.";
        //    char[] separators = new char[] { ':' };
        //    string[] subs = f1startUtente.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        //    int hh = 0;
        //    int.TryParse(subs[0], out hh);

        //    int mm = 0;
        //    int.TryParse(subs[1], out mm);

        //    DateTime dt = new DateTime(anno, Mese, dd, hh, mm, 0);

        //    return true;
        //}

        //private DateTime getDatetoOrario(string Orario)
        // {
        //     char[] separators = new char[] { ':' };
        //     string[] subs = f1startUtente.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        //     int hh = 0;
        //     int.TryParse(subs[0], out hh);

        //     int mm = 0;
        //     int.TryParse(subs[1], out mm);

        //     DateTime dt = new DateTime(this.data.Year, this.data.Month, this.data.Day, hh, mm, 0);      

        //     return dt;
        // }
    }
}

//[PersistentAlias("Iif(IEPlantList is not null And Apparato is not null And DataMisura is not null,Apparato.Descrizione + ', ' +  IEPlantList.Descrizione + ', ' + DataMisura  ,null)")]
//[DevExpress.Xpo.DisplayName("FullName")]
//[Browsable(false)]
//public string FullName
//{
//    get
//    {
//        //IEPlantList.Descrizione        
//        var tempObject = EvaluateAlias("FullName");
//        if (tempObject != null)
//        {
//            return tempObject.ToString();
//        }
//        return null;
//    }Aguzzano.20
//}

//public struct CompoundKey1Struct
//{
//    [Size(255)]
//    [Persistent("stagione")]
//    public string stagione { get; set; }
//    [Persistent("idutente")]
//    public int idutente { get; set; }
//    [Persistent("Circuito")]
//    public int Circuito { get; set; }
//    [Persistent("data")]
//    public DateTime data { get; set; }
//    [Persistent("vnoflag")]
//    public char vnoflag { get; set; }
//}
//[Key, Persistent]
//public CompoundKey1Struct CompoundKey1;