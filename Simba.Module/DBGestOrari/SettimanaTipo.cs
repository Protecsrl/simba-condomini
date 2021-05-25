using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using CAMS.Module.Classi;
using CAMS.Module.PropertyEditors;

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

namespace CAMS.Module.DBGestOrari
{
    [DefaultClassOptions, Persistent("TBSETTIMANATIPO")]
    //[RuleCombinationOfPropertiesIsUnique("UniqueApparatoCarTecniche", DefaultContexts.Save, "Apparato, StdApparatoCaratteristicheTecniche,ParentObject")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Settimana Tipo")]
    [System.ComponentModel.DefaultProperty("FullValore")]
    [ImageName("Action_EditModel")]
    [NavigationItem("Gestione Orari")]

    #region
    [Appearance("SettimanaTipo.disabilita.sempre", AppearanceItemType.LayoutItem, @"1 = 1",
             TargetItems = "TipoGiornoSettimana", Context = "Any", Priority = 1, Enabled = false)]

    //[Appearance("SettimanaTipo.flag_eccezione.colore", AppearanceItemType.LayoutItem, @"idTicketEAMS > 0",
    //         TargetItems = "f1startUtente;f1endUtente;f1start;f1end;f2startUtente;f2endUtente;f2start;f2end;f3startUtente;f3endUtente;f3start;f3end;f4startUtente;f4endUtente;f4start;f4end;f5startUtente;f5endUtente;f5start;f5end",
    //                       Context = "Any", Priority = 1, FontColor = "Red")]

    //Codice	File	Riga	Colonna    int idTicketEAMS; C:\Git_Hub\EAMS\EAMS\CAMS.Module\DBGestOrari\tbcalendario.cs	711	22

    //[Appearance("SettimanaTipo.GOraioModificato.idTicketEAMS.colore", AppearanceItemType.ViewItem, @"idTicketEAMS > 0",
    //         TargetItems = "*", Context = "Any", Priority = 1, FontColor = "Salmon")]

    [Appearance("SettimanaTipo.GOraioModificato.SecurityUser.colore1", AppearanceItemType.ViewItem, @"SecurityUser.Oid = CurrentUserId() and idTicketEAMS > 0",
             TargetItems = "idTicketEAMS", Context = "Any", Priority = 1, BackColor = "LightCoral", FontColor = "Black")]

    [Appearance("SettimanaTipo.GOraioModificato.SecurityUser.colore2", AppearanceItemType.ViewItem, @"SecurityUser.Oid != CurrentUserId() and idTicketEAMS > 0",
             TargetItems = "idTicketEAMS", Context = "Any", Priority = 1, BackColor = "LightBlue" , FontColor = "Black")]

    [Appearance("SettimanaTipo.NomeGiorno.SabatoDomentica.BackColor.Grigio", FontColor = "Silver", TargetItems = "TipoGiornoSettimana",
                  Priority = 1, Criteria = "TipoGiornoSettimana = 0 Or TipoGiornoSettimana = 6")]

    [Appearance("SettimanaTipo.NomeGiorno.SabatoDomentica.BackColor.normale", FontColor = "Black",
        TargetItems = "TipoGiornoSettimana",
        Priority = 1, Criteria = "TipoGiornoSettimana = 1 Or TipoGiornoSettimana = 2 Or TipoGiornoSettimana = 3 Or TipoGiornoSettimana = 4 Or TipoGiornoSettimana = 5")]

    #endregion

    public class SettimanaTipo : XPObject
    {
        public SettimanaTipo() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public SettimanaTipo(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        //int fCircuito;
        //public int Circuito
        //{
        //    get { return fCircuito; }
        //    set { SetPropertyValue<int>(nameof(Circuito), ref fCircuito, value); }
        //}

        [Persistent("CIRCUITO"), System.ComponentModel.DisplayName("Circuiti")]
        //[DataSourceCriteria("stagione == '2020 / 2021'")]
        //[DataSourceCriteria("[<CommessePriorita>][^.Oid == Priorita.Oid And Commesse.Oid = '@This.Immobile.Commesse.Oid' And Gruppo=='@This.Impianto.Gruppo']")]
        //[RuleRequiredField("RuleReq.RdL.Priorita", DefaultContexts.Save, "Priorita è un campo obbligatorio")]
        [Delayed(true)]
        public tbcircuiti Circuito
        {
            get { return GetDelayedPropertyValue<tbcircuiti>("Circuito"); }
            set { SetDelayedPropertyValue<tbcircuiti>("Circuito", value); }

        }


        TipoGiornoSettimana fTipoGiornoSettimana;
        [Persistent("TIPOGIORNOSETTIMANA"), System.ComponentModel.DisplayName("Giorno Settimana")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public TipoGiornoSettimana TipoGiornoSettimana
        {
            get { return fTipoGiornoSettimana; }
            set { SetPropertyValue<TipoGiornoSettimana>(nameof(TipoGiornoSettimana), ref fTipoGiornoSettimana, value); }
        }


        #region   DATE FASCIA ORARIA 1

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


        #endregion



        #region   DATE FASCIA ORARIA 2

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


        #endregion

        #region   DATE FASCIA ORARIA 3

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

        #endregion

        #region   DATE FASCIA ORARIA 4

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


        #endregion

        #region   DATE FASCIA ORARIA 5

        //TipoSetOrario ff5startTipoSetOrario;
        //[DevExpress.ExpressApp.DC.XafDisplayName("F5 Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public TipoSetOrario f5startTipoSetOrario
        //{
        //    get { return ff5startTipoSetOrario; }
        //    set { SetPropertyValue<TipoSetOrario>(nameof(f5startTipoSetOrario), ref ff5startTipoSetOrario, value); }
        //}
        //TipoSetOrario ff5endTipoSetOrario;
        //[DevExpress.ExpressApp.DC.XafDisplayName("F5 Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public TipoSetOrario f5endTipoSetOrario
        //{
        //    get { return ff5endTipoSetOrario; }
        //    set { SetPropertyValue<TipoSetOrario>(nameof(f5endTipoSetOrario), ref ff5endTipoSetOrario, value); }
        //}

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

        [Persistent("SECURITYUSERID"), XafDisplayName("Security User")]
        //[RuleUniqueValue("ParametriPivot.SecuritySystemUser.", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        [Delayed(true)]
        [Browsable(false)]
        public SecuritySystemUser SecurityUser
        {
            get { return GetDelayedPropertyValue<SecuritySystemUser>("SecurityUser"); }
            set { SetDelayedPropertyValue<SecuritySystemUser>("SecurityUser", value); }

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

        #region   DATE FASCIA ORARIA 5

        //TipoSetOrario ff5startTipoSetOrarioU;
        //[DevExpress.ExpressApp.DC.XafDisplayName("F5 Utente Dalle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public TipoSetOrario f5startTipoSetOrarioU
        //{
        //    get { return ff5startTipoSetOrarioU; }
        //    set { SetPropertyValue<TipoSetOrario>(nameof(f5startTipoSetOrarioU), ref ff5startTipoSetOrarioU, value); }
        //}
        //TipoSetOrario ff5endTipoSetOrarioU;
        //[DevExpress.ExpressApp.DC.XafDisplayName("F5 Utente Alle")]
        //[EditorAlias(CAMSEditorAliases.CustomStringTimeEditor)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public TipoSetOrario f5endTipoSetOrarioU
        //{
        //    get { return ff5endTipoSetOrarioU; }
        //    set { SetPropertyValue<TipoSetOrario>(nameof(f5endTipoSetOrarioU), ref ff5endTipoSetOrarioU, value); }
        //}



        #endregion


        //string ff1start;
        //[Size(5)]
        //[Persistent("F1_START"), System.ComponentModel.DisplayName("Giorno Ora")]
        //public string f1start
        //{
        //    get { return ff1start; }
        //    set { SetPropertyValue<string>(nameof(f1start), ref ff1start, value); }
        //}
        //string ff1end;
        //[Size(5)]
        //public string f1end
        //{
        //    get { return ff1end; }
        //    set { SetPropertyValue<string>(nameof(f1end), ref ff1end, value); }
        //}
        //string ff2start;
        //[Size(5)]
        //public string f2start
        //{
        //    get { return ff2start; }
        //    set { SetPropertyValue<string>(nameof(f2start), ref ff2start, value); }
        //}
        //string ff2end;
        //[Size(5)]
        //public string f2end
        //{
        //    get { return ff2end; }
        //    set { SetPropertyValue<string>(nameof(f2end), ref ff2end, value); }
        //}
        //string ff3start;
        //[Size(5)]
        //public string f3start
        //{
        //    get { return ff3start; }
        //    set { SetPropertyValue<string>(nameof(f3start), ref ff3start, value); }
        //}
        //string ff3end;
        //[Size(5)]
        //public string f3end
        //{
        //    get { return ff3end; }
        //    set { SetPropertyValue<string>(nameof(f3end), ref ff3end, value); }
        //}
        //string ff4start;
        //[Size(5)]
        //public string f4start
        //{
        //    get { return ff4start; }
        //    set { SetPropertyValue<string>(nameof(f4start), ref ff4start, value); }
        //}
        //string ff4end;
        //[Size(5)]
        //public string f4end
        //{
        //    get { return ff4end; }
        //    set { SetPropertyValue<string>(nameof(f4end), ref ff4end, value); }
        //}
        //string ff5start;
        //[Size(5)]
        //public string f5start
        //{
        //    get { return ff5start; }
        //    set { SetPropertyValue<string>(nameof(f5start), ref ff5start, value); }
        //}
        //string ff5end;
        //[Size(5)]
        //public string f5end
        //{
        //    get { return ff5end; }
        //    set { SetPropertyValue<string>(nameof(f5end), ref ff5end, value); }
        //}
        //string ff6start;
        //[Size(5)]
        //public string f6start
        //{
        //    get { return ff6start; }
        //    set { SetPropertyValue<string>(nameof(f6start), ref ff6start, value); }
        //}
        //string ff6end;
        //[Size(5)]
        //public string f6end
        //{
        //    get { return ff6end; }
        //    set { SetPropertyValue<string>(nameof(f6end), ref ff6end, value); }
        //}


        //int fffs1;
        //public int ffs1
        //{
        //    get { return fffs1; }
        //    set { SetPropertyValue<int>(nameof(ffs1), ref fffs1, value); }
        //}
        //int fffs2;
        //public int ffs2
        //{
        //    get { return fffs2; }
        //    set { SetPropertyValue<int>(nameof(ffs2), ref fffs2, value); }
        //}
        //int fffs3;
        //public int ffs3
        //{
        //    get { return fffs3; }
        //    set { SetPropertyValue<int>(nameof(ffs3), ref fffs3, value); }
        //}
        //int fffs4;
        //public int ffs4
        //{
        //    get { return fffs4; }
        //    set { SetPropertyValue<int>(nameof(ffs4), ref fffs4, value); }
        //}
        //int fffs5;
        //public int ffs5
        //{
        //    get { return fffs5; }
        //    set { SetPropertyValue<int>(nameof(ffs5), ref fffs5, value); }
        //}
        //int fffs6;
        //public int ffs6
        //{
        //    get { return fffs6; }
        //    set { SetPropertyValue<int>(nameof(ffs6), ref fffs6, value); }
        //}


    }

}
 