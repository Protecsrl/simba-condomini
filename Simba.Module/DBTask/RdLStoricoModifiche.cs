using CAMS.Module.Classi;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using CAMS.Module.DBSpazi;
using CAMS.Module.DBTask.Guasti;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Drawing;
namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("RDLSTORICOMODIFICHE")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "RdL Storico Modifiche")]
    [Indices("RdLRiferimento", "Edificio", "Impianto", "Apparato", "Richiedente", "DataRichiesta", "Categoria")]
    [ImageName("BO_Resume")]
    [NavigationItem("Interventi")]

    //[ListViewFilter("Tutto", "", "All Data", true, Index = 0)]
    //[ListViewFilter("RdLStModifiche.Categoria.manprog", "Categoria.Oid = 1", "Manutenzione Programmata", Index = 1)]
    //[ListViewFilter("RdLStModifiche.Categoria.conduzione", "Categoria.Oid = 2", "Conduzione", Index = 2)]
    //[ListViewFilter("RdLStModifiche.Categoria.mancond", "Categoria.Oid = 3", "Manutenzione A Condizione", Index = 3)]
    //[ListViewFilter("RdLStModifiche.Categoria.mangu", "Categoria.Oid = 4", "Manutenzione Guasto", Index = 4)]
    //[ListViewFilter("RdLStModifiche.Categoria.manprogspot", "Categoria.Oid = 5", "Manutenzione Programmata Spot", Index = 5)]


    #region filtro tampone
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModificheUltimi3mesi", "[DataRichiesta] >= AddMonths(LocalDateTimeThisMonth(), -3) And [DataRichiesta] <= LocalDateTimeThisMonth()", "Ultimi 3 Mesi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModifiche_Ultimi6mesi", "[DataRichiesta] >= AddMonths(LocalDateTimeThisMonth(), -6) And [DataRichiesta] <= LocalDateTimeThisMonth()", "Ultimi 6 Mesi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModifiche_AnnoinCorso", "IsThisYear([DataRichiesta])", "Anno in Corso", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModifiche_AnnoScorso", "IsThisYear(AddYears([DataRichiesta],1))", "Anno Scorso", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModificheTutto", "", "Tutto", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModifiche_1TrimAnnoinCorso", "GetMonth([DataRichiesta]) In(1,2,3) And IsThisYear([DataRichiesta])", @"1° Trimestre", Index = 5)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModifiche_2TrimAnnoinCorso", "GetMonth([DataRichiesta]) In(4,5,6) And IsThisYear([DataRichiesta])", @"2° Trimestre", Index = 6)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModifiche_3TrimAnnoinCorso", "GetMonth([DataRichiesta]) In(7,8,9) And IsThisYear([DataRichiesta])", @"3° Trimestre", Index = 7)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModifiche_4TrimAnnoinCorso", "GetMonth([DataRichiesta]) In(10,11,12) And IsThisYear([DataRichiesta])", @"4° Trimestre", Index = 8)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModifiche_1TrimAnnoScorso", "GetMonth([DataRichiesta]) In(1,2,3) And IsThisYear(AddYears([DataRichiesta],1))", @"1° Trimestre (Anno Scorso)", Index = 9)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModifiche_2TrimAnnoScorso", "GetMonth([DataRichiesta]) In(4,5,6) And IsThisYear(AddYears([DataRichiesta],1))", @"2° Trimestre (Anno Scorso)", Index = 10)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModifiche_3TrimAnnoScorso", "GetMonth([DataRichiesta]) In(7,8,9) And IsThisYear(AddYears([DataRichiesta],1))", @"3° Trimestre (Anno Scorso)", Index = 11)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModifiche_4TrimAnnoScorso", "GetMonth([DataRichiesta]) In(10,11,12) And IsThisYear(AddYears([DataRichiesta],1))", @"4° Trimestre (Anno Scorso)", Index = 12)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModificheCategoria.manprog", "Categoria.Oid  = 1", "Manutenzione Programmata", Index = 13)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModificheCategoria.conduzione", "Categoria.Oid  = 2", "Conduzione", Index = 14)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModificheCategoria.mancond", "Categoria.Oid  = 3", "Manutenzione A Condizione", Index = 15)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModificheCategoria.mangu", "Categoria.Oid  = 4", "Manutenzione Guasto", Index = 16)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLStModificheCategoria.manprogspot", "Categoria.Oid  = 5", "Manutenzione Programmata Spot", Index = 17)]

    #endregion



    public class RdLStoricoModifiche : XPObject
    {
         public RdLStoricoModifiche() : base() { }
        public RdLStoricoModifiche(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1) // crea RdL
            {
                this.DataModifica = DateTime.Now;
            }
        }

        public RdLStoricoModifiche CloneFrom(RdL RdLSelezionato)
        {
            this.Apparato = RdLSelezionato.Apparato;
    

            return this;
        }

        
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

         private const int GiorniRitardoRicerca = -7;

        [Persistent("RDL_RIFERIMENTO"), DisplayName("RdL Riferimento")]
        [Delayed(true)]
        public RdL RdLRiferimento
        {
            get
            {
                return GetDelayedPropertyValue<RdL>("RdLRiferimento");
            }
            set
            {
                SetDelayedPropertyValue<RdL>("RdLRiferimento", value);
            }

        }

        private DateTime fDataModifica;
        [Persistent("DATA_MODIFICA"), DisplayName("Data Modifica")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        // [ToolTip("Data di Avvenuta Azione Tampone", null, DevExpress.Utils.ToolTipIconType.Information)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        public DateTime DataModifica
        {
            get
            {
                return fDataModifica;
            }
            set
            {
                SetPropertyValue<DateTime>("DataModifica", ref fDataModifica, value);
            }
        }


        private Richiedente fRichiedente;
        [Persistent("RICHIEDENTE"), System.ComponentModel.DisplayName("Richiedente")]
        [DataSourceCriteria("Commesse = '@This.Edificio.Commesse'")]
        [RuleRequiredField("RdLStoricoModifiche.Rihiedente.Obblig.su.Guasto", DefaultContexts.Save, SkipNullOrEmptyValues = false, TargetCriteria = "[Categoria.Oid] == 4")]
        [Appearance("RdLStoricoModifiche.Abilita.Richiedente", Criteria = "Edificio is null Or [UltimoStatoSmistamento.Oid] > 2", FontColor = "Black", Enabled = false)]
        [Appearance("RdLStoricoModifiche.Richiedente.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Richiedente)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public Richiedente Richiedente
        {
            get
            {
                return GetDelayedPropertyValue<Richiedente>("Richiedente");
            }
            set
            {
                SetDelayedPropertyValue<Richiedente>("Richiedente", value);
            }
        }

        #region RICHIEDENTE     -     invia Mail + SMS


        private TipologiaSpedizioneRdL fTipologiaSpedizione;
        [Persistent("TIPOSPEDIZIONE"), DevExpress.Xpo.DisplayName("Tipo Spedizione")]
        [Appearance("RdLStoricoModifiche.Abilita.TipologiaSpedizioneRdL", Criteria = "Richiedente  is null", FontColor = "Black", Enabled = false)]
        public TipologiaSpedizioneRdL TipologiaSpedizione
        {
            get
            {
                return fTipologiaSpedizione;
            }
            set
            {
                SetPropertyValue<TipologiaSpedizioneRdL>("TipologiaSpedizione", ref fTipologiaSpedizione, value);
            }
        }

        private string fEmail;
        private const string UrlStringEditMask = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        private const string RuleRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMaskType", "RegEx")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", UrlStringEditMask)]
        [RuleRegularExpression("RuleRegularExp.RdLStoricoModifiche.Email", DefaultContexts.Save, RuleRegularExpression,
         CustomMessageTemplate = "Attenzione inserire un corretto indirizzo mail(nome.cognome@dominio.com oppure nomecognome@dominio.xxx).")]
        [ToolTip("Inserire una email, con il seguente formato: nome.cognome@dominio.com oppure nomecognome@dominio.xxx", null)]
        [Size(100), Persistent("DESEMAIL"), DisplayName("Email")]
        [DbType("varchar(100)")]
        [VisibleInListView(false)]
        [Appearance("RdLStoricoModifiche.TipologiaSpedRdL.BrownEmail", AppearanceItemType.LayoutItem, "TipologiaSpedizione == 'MAIL' Or TipologiaSpedizione == 'MAILSMS'", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RdLStoricoModifiche.TipologiaSpedRdL.EnabledEmail", Criteria = "TipologiaSpedizione == 'No' Or TipologiaSpedizione == 'SMS'", Enabled = false)]//FontColor = "Black",
        public string Email
        {
            get
            {
                return fEmail;
            }
            set
            {
                SetPropertyValue<string>("Email", ref fEmail, value);
            }
        }


        private string _PhoneString;
        private const string PhoneStringEditMask = "(0000)000-0000000";
        [DevExpress.ExpressApp.Model.ModelDefault("EditMaskType", "Simple")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + PhoneStringEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", PhoneStringEditMask)]
        [ToolTip("indicare il numero telefonico del mobile che riceve l'SMS, : " + PhoneStringEditMask, null, DevExpress.Utils.ToolTipIconType.Information)]
        [Size(20), Persistent("TELEFONO"), DisplayName("Telefono Mobile")]
        [DbType("varchar(20)")]
        [VisibleInListView(false)]
        [Appearance("RdLStoricoModifiche.TipologiaSpedRdL.Brown.PhoneString", AppearanceItemType.LayoutItem, "TipologiaSpedizione == 'SMS' Or TipologiaSpedizione == 'MAILSMS'", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RdLStoricoModifiche.TipologiaSpedRdL.Enabled.PhoneString", Criteria = "TipologiaSpedizione == 'No' Or TipologiaSpedizione == 'MAIL'", Enabled = false)]//FontColor = "Black",
        public string PhoneString
        {
            get
            {
                return _PhoneString;
            }
            set
            {
                SetPropertyValue("PhoneString", ref _PhoneString, value);
            }
        }
        #endregion

        #region EDIFICIO
        private Edificio fEdificio;
        [Persistent("EDIFICIO"), System.ComponentModel.DisplayName("Edificio")]
        [VisibleInListView(false)]
        [Appearance("RdLStoricoModifiche.Abilita.Edificio", Criteria = "not (Impianto  is null)", FontColor = "Black", Enabled = false)]
        [RuleRequiredField("RuleReq.RdLStoricoModifiche.Edificio", DefaultContexts.Save, "Edificio è un campo obbligatorio")]
        [Appearance("RdLStoricoModifiche.Edificio.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Edificio)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [DataSourceCriteria("Abilitato = 'Si' And Commesse.DataAl <= LocalDateTimeToday()")]  //
        [Delayed(true)]
        public Edificio Edificio
        {
            get
            {
                return GetDelayedPropertyValue<Edificio>("Edificio");
            }
            set
            {
                SetDelayedPropertyValue<Edificio>("Edificio", value);
            }
        }

        [NonPersistent, Size(25)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string RicercaEdificio
        {
            get;
            set;
        }

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [PersistentAlias("Iif(Edificio is not null, Edificio.Note,null)"), DisplayName("Note Edificio")]
        [Appearance("RdLStoricoModifiche.EdificioNote.Black", Criteria = "not (EdificioNote  is null)", FontColor = "Blue")]
        [Appearance("RdLStoricoModifiche.EdificioNote.Italic", FontStyle = FontStyle.Italic)]
        public string EdificioNote
        {
            get
            {
                if (this.Edificio == null) return null;
                return string.Format("{0}", this.Edificio.Note);
            }
        }


        [PersistentAlias("Iif(Edificio is not null, Edificio.RifReperibile + '('+ Edificio.Commesse.ReferenteCofely.NomeCognome + ', ' + Edificio.Commesse.ReferenteCofely.Telefono + ')',null)"), DisplayName("Riferimenti Reperibile Edificio")]
        [Appearance("RdL.EdificioRifReperibile.Black", Criteria = "not (EdificioRifReperibile  is null)", FontColor = "Blue")]
        [Size(4000)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Appearance("RdLStoricoModifiche.EdificioRifReperibile.Italic", FontStyle = FontStyle.Italic)]
        public string EdificioRifReperibile
        {
            get
            {
                if (this.Edificio == null) return null;
                else
                {
                    if (this.Edificio.Commesse != null)
                    {
                        if (this.Edificio.Commesse.ReferenteCofely != null)
                            return string.Format("Reperibile:{0}, PM:({1}, {2})", this.Edificio.RifReperibile, Edificio.Commesse.ReferenteCofely.NomeCognome, Edificio.Commesse.ReferenteCofely.Telefono);
                        else
                            return string.Format("Reperibile:{0}", this.Edificio.RifReperibile);
                    }
                    else
                        return string.Format("Reperibile:{0}", this.Edificio.RifReperibile);
                }

            }
        }
        #endregion

        #region Piano e stanza
        private Piani fPiano;
        [Persistent("PIANO"), System.ComponentModel.DisplayName("Piano")]
        [Appearance("RdLStoricoModifiche.Piano.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Edificio)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RdLStoricoModifiche.Abilita.Piano", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        [DataSourceCriteria("Edificio = '@This.Edificio'")]// QUESTO SIGNIFICA CHE TI DA SOLO I PIANI CHE SONO DELL'EDIFICIO SELEZIONATO
        [ExplicitLoading()]
        public Piani Piano
        {
            get
            {
                return fPiano;
            }
            set
            {
                SetPropertyValue<Piani>("Piano", ref fPiano, value);
            }
        }

        private Locali fLocale;
        [Persistent("LOCALI"), DevExpress.Xpo.DisplayName("Locali")]
        [Appearance("RdLStoricoModifiche.Locali", Enabled = false, Criteria = "!IsNullOrEmpty(Piano)", Context = "DetailView")]
        [DataSourceCriteria("Piano = '@This.Piano'")]// su rdl si chiama piano, su locali piani
        [ExplicitLoading()]
        public Locali Locale
        {
            get { return fLocale; }
            set { SetPropertyValue<Locali>("Locale", ref fLocale, value); }
        }

        [NonPersistent, Size(25)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string RicercaLocale
        {
            get;
            set;
        }

        private string fReparto;
        [Size(250), Persistent("REPARTO"), System.ComponentModel.DisplayName("Reparto")]
        [DbType("varchar(250)")]
        [VisibleInListView(false)]
        public string Reparto
        {
            get
            {
                return fReparto;
            }
            set
            {
                SetPropertyValue<string>("Reparto", ref fReparto, value);
            }
        }

        #endregion


        private Impianto fImpianto;
        [Persistent("IMPIANTO"), System.ComponentModel.DisplayName("Impianto")]
        [Appearance("RdLStoricoModifiche.Abilita.Impianto", Criteria = "(Edificio  is null) OR (not (StdApparato is null))", FontColor = "Black", Enabled = false)]
        [RuleRequiredField("RuleReq.RdLStoricoModifiche.Impianto", DefaultContexts.Save, "Impianto è un campo obbligatorio")]
        [Appearance("RdLStoricoModifiche.Impianto.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Impianto)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ImmediatePostData(true)]
        //[DataSourceCriteria("Edificio = '@This.Edificio'")] '@This.Piano' is null And '@This.Locale' is null,Edificio = '@This.Edificio'," +
        [DataSourceCriteria("Iif(IsNullOrEmpty('@This.Piano') And IsNullOrEmpty('@This.Locale'),Edificio = '@This.Edificio'," +
                              "Iif(!IsNullOrEmpty('@This.Piano') And IsNullOrEmpty('@This.Locale'),Edificio = '@This.Edificio' And APPARATOes[Locale.Piano = '@This.Piano']," +
                               "Iif(!IsNullOrEmpty('@This.Piano') And !IsNullOrEmpty('@This.Locale'),Edificio = '@This.Edificio' And APPARATOes[Locale = '@This.Locale']," +
                               "Edificio = '@This.Edificio' And APPARATOes[Locale = '@This.Locale'])" +
                              ")" +
                             ")"
                             )]
        [ExplicitLoading()]
        //[Delayed(true)]
        public Impianto Impianto
        {
            get
            {
                return fImpianto;
            }
            set
            {
                SetPropertyValue<Impianto>("Impianto", ref fImpianto, value);
            }
        }

        private Apparato fApparato;  ///[UltimoStatoSmistamento.Oid] > 1
        [Persistent("APPARATO"), System.ComponentModel.DisplayName("Apparato")]
        [Appearance("RdLStoricoModifiche.Abilita.Apparato", Criteria = "(Impianto is null) OR (not (Problema is null)) Or  [UltimoStatoSmistamento.Oid] In(2,3,4,5,6,7,8,9,10)", FontColor = "Black", Enabled = false)]
        [Appearance("RdLStoricoModifiche.Apparato.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Apparato)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [RuleRequiredField("RuleReq.RdLStoricoModifiche.Apparato", DefaultContexts.Save, "Apparato è un campo obbligatorio")]
        //[DataSourceCriteria("Impianto = '@This.Impianto'")]
        [DataSourceCriteria("Iif(IsNullOrEmpty('@This.Piano') And IsNullOrEmpty('@This.Locale'),Impianto.Edificio = '@This.Edificio' And Impianto = '@This.Impianto'," +
                      "Iif(!IsNullOrEmpty('@This.Piano') And IsNullOrEmpty('@This.Locale'),Impianto.Edificio = '@This.Edificio' And Locale.Piano = '@This.Piano' And Impianto = '@This.Impianto'," +
                       "Iif(!IsNullOrEmpty('@This.Piano') And !IsNullOrEmpty('@This.Locale'),Impianto.Edificio = '@This.Edificio' And Locale = '@This.Locale' And Impianto = '@This.Impianto'," +
                       "Impianto.Edificio = '@This.Edificio' And Locale = '@This.Locale')" +
                      ")" +
                     ")"
                     )]
        [ExplicitLoading()]
        public Apparato Apparato
        {
            get
            {
                return fApparato;
            }
            set
            {
                SetPropertyValue<Apparato>("Apparato", ref fApparato, value);
            }
        }


        [PersistentAlias("Apparato.StdApparato"), System.ComponentModel.DisplayName("Tipo Apparato")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), ImmediatePostData(true)]
        [ExplicitLoading()]
        public StdApparato StdApparato
        {
            get
            {
                if (Apparato == null)
                    return null;
                var tempObject = EvaluateAlias("StdApparato");
                if (tempObject != null)
                {
                    return (StdApparato)tempObject;
                }
                else
                {
                    return null;
                }

            }
        }


        #region ####################    DATA OPERATIVA
        private DateTime fDataSopralluogo;
        [Persistent("DATA_SOPRALLUOGO"), DisplayName("Data Sopralluogo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //   [ToolTip("Data di Sopralluogo dell intervento", null, DevExpress.Utils.ToolTipIconType.Information)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        public DateTime DataSopralluogo
        {
            get
            {
                return fDataSopralluogo;
            }
            set
            {
                SetPropertyValue<DateTime>("DataSopralluogo", ref fDataSopralluogo, value);
            }
        }

        private DateTime fDataAzioniTampone;
        [Persistent("DATA_AZIONI_TAMPONE"), DisplayName("Data Intervento Sicurezza")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        // [ToolTip("Data di Avvenuta Azione Tampone", null, DevExpress.Utils.ToolTipIconType.Information)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        public DateTime DataAzioniTampone
        {
            get
            {
                return fDataAzioniTampone;
            }
            set
            {
                SetPropertyValue<DateTime>("DataAzioniTampone", ref fDataAzioniTampone, value);
            }
        }

        private DateTime fDataInizioLavori;
        [Persistent("DATA_INIZIO_LAVORI"), DisplayName("Data Inizio Lavori")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        public DateTime DataInizioLavori
        {
            get
            {
                return fDataInizioLavori;
            }
            set
            {
                SetPropertyValue<DateTime>("DataInizioLavori", ref fDataInizioLavori, value);
            }
        }

        #endregion


        private Priorita fPriorita;
        [Persistent("PRIORITA"), System.ComponentModel.DisplayName("Priorità")]
        [RuleRequiredField("RuleReq.RdLStoricoModifiche.Priorita", DefaultContexts.Save, "Priorita è un campo obbligatorio")]
        [Appearance("RdLStoricoModifiche.Priorita.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Priorita)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RdLStoricoModifiche.Abilita.Priorita", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        [ExplicitLoading()]
        public Priorita Priorita
        {
            get
            {
                return fPriorita;
            }
            set
            {
                SetPropertyValue<Priorita>("Priorita", ref fPriorita, value);
            }
        }

        private Categoria fCategoria;
        [Persistent("CATEGORIA"), System.ComponentModel.DisplayName("Categoria Manutenzione")]
        [RuleRequiredField("RuleReq.RdLStoricoModifiche.Categoria", DefaultContexts.Save, "Categoria è un campo obbligatorio")]
        [DataSourceCriteria("Oid In(2,3,4,5)"), ImmediatePostData(true)]
        [Appearance("RdLStoricoModifiche.Abilita.Categoria", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        [Appearance("RdLStoricoModifiche.Categoria.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Categoria)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ExplicitLoading()]
        public Categoria Categoria
        {
            get
            {
                return fCategoria;
            }
            set
            {
                SetPropertyValue<Categoria>("Categoria", ref fCategoria, value);
            }
        }

        private TipoIntervento fTipoIntervento;
        [DataSourceCriteria("Oid In(2,6)")]
        [Persistent("TIPOINTERVENTO"), System.ComponentModel.DisplayName("Tipo Intervento")]
        [RuleRequiredField("RuleReq.RdLStoricoModifiche.TipoIntervento", DefaultContexts.Save, "Tipo Intervento è un campo obbligatorio")]
        [Appearance("RdLStoricoModifiche.TipoIntervento.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(TipoIntervento)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RdLStoricoModifiche.Abilita.TipoIntervento", FontColor = "Black", Enabled = false, Criteria = "[UltimoStatoSmistamento.Oid] > 1")]
        [ExplicitLoading()]
        [Delayed(true)]
        public TipoIntervento TipoIntervento
        {
            get
            {
                return GetDelayedPropertyValue<TipoIntervento>("TipoIntervento");
            }
            set
            {
                SetDelayedPropertyValue<TipoIntervento>("TipoIntervento", value);
            }

        }

        #region  @@@@@@@@@@@   APPARATO PROBLEMA CAUSA E RIMEDIO E COMBO
        private ApparatoProblema fProblema;   /// @@@@@@@@@@@@@  obligatorio per le manutenzioni a guasto oid=4     
        [Persistent("PCRAPPPROBLEMA"), System.ComponentModel.DisplayName("Problema")]
        [Appearance("RdLStoricoModifiche.Abilita.Problema", Criteria = "[Apparato] is null Or [ProblemaCausa] Is Not Null", FontColor = "Black", Enabled = false)]
        [VisibleInListView(false)]      //  [DataSourceProperty("ListaFiltraApparatoProblemas", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        [DataSourceCriteria("StdApparato = '@This.Apparato.StdApparato'")]  //  deve essere persistente @@@@@@@@@@@@@@@ò  filtra per apparato     [ApparatoSchedaMP] Is Not Null   
        [Delayed(true)]
        public ApparatoProblema Problema
        {
            get
            {
                return GetDelayedPropertyValue<ApparatoProblema>("Problema");
            }
            set
            {
                SetDelayedPropertyValue<ApparatoProblema>("Problema", value);
            }

        }

        private ProblemaCausa fProblemaCausa; // @@@@@@@@@@@@@  obligatorio (solo in completamento)  per le manutenzioni a guasto oid=4       
        [Persistent("PCRPROBCAUSA"), System.ComponentModel.DisplayName("Causa")]
        [Appearance("RdLStoricoModifiche.ProblemaCausa", Enabled = false, Criteria = "IsNullOrEmpty(Problema) Or not IsNullOrEmpty(CausaRimedio)", FontColor = "Black", Context = "DetailView")]
        [VisibleInListView(false)]    
        [DataSourceCriteria("ApparatoProblema = '@This.Problema'")]  //filtra per apparato
        [ExplicitLoading()]
        [Delayed(true)]
        public ProblemaCausa ProblemaCausa
        {
            get
            {
                return GetDelayedPropertyValue<ProblemaCausa>("ProblemaCausa");
            }
            set
            {
                SetDelayedPropertyValue<ProblemaCausa>("ProblemaCausa", value);
            }


        }

        private CausaRimedio fCausaRimedio; // @@@@@@@@@@@@@  obligatorio (solo in completamento)  per le manutenzioni a guasto oid=4
        [Persistent("PCRCAUSARIMEDIO"), System.ComponentModel.DisplayName("Rimedio")]
        [Appearance("RdLStoricoModifiche.CausaRimedio", Enabled = false, Criteria = "IsNullOrEmpty(ProblemaCausa)", FontColor = "Black", Context = "DetailView")]
        [ VisibleInListView(false)]
        [DataSourceCriteria("ProblemaCausa = '@This.ProblemaCausa'")]  //filtra per apparato
        [Delayed(true)]
        public CausaRimedio CausaRimedio
        {
            get
            {
                return GetDelayedPropertyValue<CausaRimedio>("CausaRimedio");
            }
            set
            {
                SetDelayedPropertyValue<CausaRimedio>("CausaRimedio", value);
            }

        }

        private StatoImpiantoInArrivo fStatoImpiantoInArrivo;
        [Persistent("RDLSTATOINARRIVO"), System.ComponentModel.DisplayName("Stato Impianto in Arrivo")]
        [Appearance("RdLStoricoModifiche.StatoImpiantoInArrivo.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(StatoImpiantoInArrivo)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RdLStoricoModifiche.Abilita.StatoImpiantoInArrivo", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        [ExplicitLoading()]
        public StatoImpiantoInArrivo StatoImpiantoInArrivo
        {
            get
            {
                return fStatoImpiantoInArrivo;
            }
            set
            {
                SetPropertyValue<StatoImpiantoInArrivo>("StatoImpiantoInArrivo", ref fStatoImpiantoInArrivo, value);
            }
        }

        private DichiarazioneArrivo fDichiarazioneArrivo;
        [Persistent("RDLDICHIARAZIONEARRIVO"), System.ComponentModel.DisplayName("Tempo di Arrivo")]
        [Appearance("RdLStoricoModifiche.DichiarazioneArrivo.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(StatoImpiantoInArrivo)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RdLStoricoModifiche.Abilita.DichiarazioneArrivo", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        [ExplicitLoading()]
        public DichiarazioneArrivo DichiarazioneArrivo
        {
            get
            {
                return fDichiarazioneArrivo;
            }
            set
            {
                SetPropertyValue<DichiarazioneArrivo>("DichiarazioneArrivo", ref fDichiarazioneArrivo, value);
            }
        }



        #endregion

        private string fDescrizione;
        [Size(4000), Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione Intervento")]
        [RuleRequiredField("RdLStoricoModifiche.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        [Appearance("RdLStoricoModifiche.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [DbType("varchar(4000)")]
        [VisibleInListView(false)]
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

        private DateTime fDataCreazione;
        [Persistent("DATACREAZIONE"), System.ComponentModel.DisplayName("Data Creazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [VisibleInListView(false)]
        public DateTime DataCreazione
        {
            get
            {
                return fDataCreazione;
            }
            set
            {
                SetPropertyValue<DateTime>("DataCreazione", ref fDataCreazione, value);
            }
        }

        private DateTime fDataRichiesta;
        [Persistent("DATARICHIESTA"), System.ComponentModel.DisplayName("Data Richiesta")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [Appearance("RdLStoricoModifiche.DataRichiesta.Enabled", @"Oid > 0", FontColor = "Black", Enabled = false)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        public DateTime DataRichiesta
        {
            get
            {
                return fDataRichiesta;
            }
            set
            {
                SetPropertyValue<DateTime>("DataRichiesta", ref fDataRichiesta, value);
            }
        }

        private DateTime fDataPianificata;
        [Persistent("DATAPIANIFICATA"), System.ComponentModel.DisplayName("Data Pianificata")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        public DateTime DataPianificata
        {
            get
            {
                return fDataPianificata;
            }
            set
            {
                SetPropertyValue<DateTime>("DataPianificata", ref fDataPianificata, value);
            }
        }

        private DateTime fDataPrevistoArrivo;
        [Persistent("DATAPREVISTOARRIVO"), System.ComponentModel.DisplayName("Data Previsto Arrivo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        public DateTime DataPrevistoArrivo
        {
            get
            {
                return fDataPrevistoArrivo;
            }
            set
            {
                SetPropertyValue<DateTime>("DataPrevistoArrivo", ref fDataPrevistoArrivo, value);
            }
        }

        private Soddisfazione fSoddisfazione;
        [Persistent("SODDISFAZIONE"), System.ComponentModel.DisplayName("Soddisfazione")]
        [Appearance("RdLStoricoModifiche.Visibility.Soddisfazione", Criteria = "[UltimoStatoSmistamento.Oid] != 4", Visibility = ViewItemVisibility.Hide)]
        public Soddisfazione Soddisfazione
        {
            get
            {
                return fSoddisfazione;
            }
            set
            {
                SetPropertyValue<Soddisfazione>("Soddisfazione", ref fSoddisfazione, value);
            }
        }



        private string fUtenteCreatoRichiesta;
        [Size(25), Persistent("UTENTEINSERIMENTO"), System.ComponentModel.DisplayName("Utente")]
        [DbType("varchar(4000)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string UtenteCreatoRichiesta
        {
            get
            {
                return fUtenteCreatoRichiesta;
            }
            set
            {
                SetPropertyValue<string>("UtenteCreatoRichiesta", ref fUtenteCreatoRichiesta, value);
            }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"), System.ComponentModel.DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [System.ComponentModel.Browsable(false)]
        public DateTime DataAggiornamento
        {
            get
            {
                return fDataAggiornamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value);
            }
        }

        #region
        private RisorseTeam fRisorseTeam;  //  @@@@@@@@@@  obligatoria solo in assegnazione
        [Persistent("RISORSATEAM"), System.ComponentModel.DisplayName("Team")]
        [DataSourceCriteria("RisorsaCapo.CentroOperativo == '@This.Edificio.CentroOperativoBase'")]
        [Appearance("RdLStoricoModifiche.RisorseTeam.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(RisorseTeam) AND ([UltimoStatoSmistamento.Oid] In(2,11))", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RdLStoricoModifiche.RisorseTeam.nero", AppearanceItemType.LayoutItem, "not IsNullOrEmpty(RisorseTeam)", FontStyle = FontStyle.Bold, FontColor = "Black")]
        [Appearance("RdLStoricoModifiche.RisorseTeam.neroItem", AppearanceItemType.ViewItem, "not IsNullOrEmpty(RisorseTeam)", Enabled = false, FontStyle = FontStyle.Bold, FontColor = "Black")]
        [ExplicitLoading()]
        public RisorseTeam RisorseTeam
        {
            get
            {
                return fRisorseTeam;
            }
            set
            {
                SetPropertyValue<RisorseTeam>("RisorseTeam", ref fRisorseTeam, value);
            }
        }

        private string fRicercaRisorseTeam;
        [NonPersistent, Size(25)]//, DisplayName("Filtro"), , Size(25)
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string RicercaRisorseTeam
        {
            get;
            set;

        }

        [PersistentAlias("Iif(RisorseTeam is not null,RisorseTeam.Mansione,null)"), DisplayName("Mansione")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        public string Mansione
        {
            get
            {
                var tempObject = EvaluateAlias("Mansione");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return null;
                }
            }
        }

        [PersistentAlias("Iif(RisorseTeam is not null,RisorseTeam.RisorsaCapo.Telefono + ',' + RisorseTeam.RisorsaCapo.Email,null)"), DisplayName("Telefono/eMail Risorsa")]
        [Size(551)]
        public string RisorseTeamTelefono
        {
            get
            {
                if (this.Oid == -1) return null;
                var tempObject = EvaluateAlias("RisorseTeamTelefono");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return null;
                }
            }
        }


        #endregion


        private DateTime fDataAssegnazioneOdl;
        [Persistent("DATAASSEGNAZIONEODL"), System.ComponentModel.DisplayName("Data di Assegnazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [Appearance("RdLStoricoModifiche.DataAssegnazioneOdl.noneditabile", FontColor = "Black", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime DataAssegnazioneOdl
        {
            get
            {
                return fDataAssegnazioneOdl;
            }
            set
            {
                SetPropertyValue<DateTime>("DataAssegnazioneOdl", ref fDataAssegnazioneOdl, value);
            }
        }

        private OdL fOdL; 
        [Persistent("ODL"), System.ComponentModel.DisplayName("Ordine di Lavoro")]
        [MemberDesignTimeVisibility(false)]
        [Delayed(true)]
        public OdL OdL
        {
            get
            {
                return GetDelayedPropertyValue<OdL>("OdL");
            }
            set
            {
                SetDelayedPropertyValue<OdL>("OdL", value);
            }
        }

        #region stato smistamewnto e operativo

        private StatoSmistamento fUltimoStatoSmistamento;
        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.DisplayName("Stato Smistamento")]
        [RuleRequiredField("RRq.RdLStoricoModifiche.UltimoStatoSmistamento", DefaultContexts.Save, "La StatoSmistamento è un campo obbligatorio")]
        [Appearance("RdLStoricoModifiche.UltimoStatoSmistamento.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(UltimoStatoSmistamento)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RdLStoricoModifiche.UltimoStatoSmistamento.Evidenza", AppearanceItemType.LayoutItem, "not(IsNullOrEmpty(UltimoStatoSmistamento))", FontStyle = FontStyle.Bold, BackColor = "Yellow", FontColor = "Black")]
        [DataSourceCriteria("[<StatoSmistamentoCombo>][^.Oid == StatoSmistamentoxCombo.Oid And StatoSmistamento == '@This.old_SSmistamento']")]
        [ExplicitLoading()]
        public StatoSmistamento UltimoStatoSmistamento
        {
            get
            {

                return fUltimoStatoSmistamento;
            }
            set
            {
                SetPropertyValue<StatoSmistamento>("UltimoStatoSmistamento", ref fUltimoStatoSmistamento, value);
            }
        }

        private StatoOperativo fUltimoStatoOperativo;
        [Persistent("STATOOPERATIVO"), System.ComponentModel.DisplayName("Stato Operativo")]
        //  [DataSourceProperty("old_SSmistamento.StatoSmistamento_SOperativoSOs")] //   [DataSourceProperty("ListaFiltraComboSOperativo")]
        [DataSourceCriteria("[<StatoSmistamento_SOperativoSO>][^.Oid == StatoOperativoSO.Oid And StatoSmistamento == '@This.old_SSmistamento'] ")]
        [ExplicitLoading()]
        public StatoOperativo UltimoStatoOperativo
        {
            get
            {

                return fUltimoStatoOperativo;
            }
            set
            {
                SetPropertyValue<StatoOperativo>("UltimoStatoOperativo", ref fUltimoStatoOperativo, value);
            }
        }


        #endregion

        private RegistroRdL fRegistroRdL;
        [ Persistent("REGRDL"), System.ComponentModel.DisplayName("Registro RdL")]
        [System.ComponentModel.Browsable(false)]
        [ExplicitLoading()]
        public RegistroRdL RegistroRdL
        {
            get
            {
                return fRegistroRdL;
            }
            set
            {
                SetPropertyValue<RegistroRdL>("RegistroRdL", ref fRegistroRdL, value);
            }
        }

        #region Dati completamento
        private DateTime fDataCompletamento;
        [Persistent("DATACOMPLETAMENTO"), System.ComponentModel.DisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        // [ToolTip("Data Completamento dell'Intervento ", null, DevExpress.Utils.ToolTipIconType.Information)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        [Appearance("RdLStoricoModifiche.DataCompletamento.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(DataCompletamento) AND ([UltimoStatoSmistamento.Oid] In(4))", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ImmediatePostData(true)]
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
        [Persistent("NOTECOMPLETAMENTO"), Size(4000), System.ComponentModel.DisplayName("Note Completamento")]
        [DbType("varchar(4000)")]
        [ImmediatePostData(true)]
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


        private DateTime fDataFermo;
        [Persistent("DATAFERMO"), System.ComponentModel.DisplayName("Data Fermo"), VisibleInListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        [ImmediatePostData(true)]
        public DateTime DataFermo
        {
            get
            {
                return fDataFermo;
            }
            set
            {
                SetPropertyValue<DateTime>("DataFermo", ref fDataFermo, value);
            }
        }

        private DateTime fDataRiavvio;
        [Persistent("DATARIAVVIO"), System.ComponentModel.DisplayName("Data Riavvio")]
        [VisibleInListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [Appearance("RdLStoricoModifiche.Abilita.DataRiavvio", Criteria = "DataFermo  is null", Enabled = false)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)] // 
        [ImmediatePostData(true)]
        public DateTime DataRiavvio
        {
            get
            {
                return fDataRiavvio;
            }
            set
            {
                SetPropertyValue<DateTime>("DataRiavvio", ref fDataRiavvio, value);
            }
        }

        private RdL fRdLSuccessiva;
        [Persistent("RDL_SUCCESSIVA"), DisplayName("RdL Successiva")]
        [VisibleInListView(false)]
        [Delayed(true)]
        public RdL RdLSuccessiva
        {
            get
            {
                return GetDelayedPropertyValue<RdL>("RdLSuccessiva");
            }
            set
            {
                SetDelayedPropertyValue<RdL>("RdLSuccessiva", value);
            }

        }

        #endregion

        #region Associazioni e alias
        [PersistentAlias("Iif(RdLRiferimento is not null,RdLRiferimento.Oid,null)")]
        [System.ComponentModel.DisplayName("Codice")]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string Codice
        {
            get
            {
                var tempObject = EvaluateAlias("Codice");
                if (tempObject != null)
                {
                    return (string)tempObject.ToString();
                }
                return null;
            }
        }

        [PersistentAlias("fRdLUnivoco +  Iif(RegistroRdL is not null,'/' + RegistroRdL.Oid,null) +  Iif(fRrdLTTMP is not null,'/' + fRrdLTTMP,null)")]
        [System.ComponentModel.DisplayName("Codice Progressivo/RegRdl/Codifica TTMP")]
        public string CodiciRdLOdL
        {
            get
            {
                if (this.Oid == -1) return null;

                var tempObject = EvaluateAlias("CodiciRdLOdL");
                if (tempObject != null)
                {
                    return (string)tempObject.ToString();
                }
                return null;
            }
        }

        //RRDLTT	N	INTEGER
        //RRDLMP	N	INTEGER
        //RDLUNIVOCO	N	INTEGER    RRDLTTMP
        [Persistent("RRDLTTMP")]
        private int fRrdLTTMP;

        [PersistentAlias("fRrdLTTMP")]
        [DisplayName("Codifica TTMP")]
        [VisibleInListView(true), VisibleInDetailView(false), VisibleInLookupListView(true)]
        public int RrdLTTMP
        {
            get { return fRrdLTTMP; }
        }


        [Persistent("RRDLMP")]
        private int fRrdLMP;

        [PersistentAlias("fRrdLMP")]
        [DisplayName("Codifica MP")]
        public int RrdLMP
        {
            get { return fRrdLMP; }
        }

        [Persistent("RDLUNIVOCO")]
        private int fRdLUnivoco;

        [PersistentAlias("fRdLUnivoco")]
        [DisplayName("Codice Progressivo")]
        [VisibleInListView(true), VisibleInDetailView(false), VisibleInLookupListView(true)]
        public int RdLUnivoco
        {
            get { return fRdLUnivoco; }
        }


      

        

        [PersistentAlias("OdL.Oid"), System.ComponentModel.DisplayName("Cod OdL"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [MemberDesignTimeVisibility(false)]
        public int CodOdL
        {
            get
            {
                if (this.Oid == -1) return 0;
                var tempObject = EvaluateAlias("CodOdL");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        [PersistentAlias("RegistroRdL.Oid"), System.ComponentModel.DisplayName("Cod Reg.RdL")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int CodRegRdL
        {
            get
            {
                if (this.Oid == -1) return 0;
                var tempObject = EvaluateAlias("CodRegRdL");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }



        [NonPersistent]
        [MemberDesignTimeVisibility(false)]
        public bool UserIsAdminRuolo
        {
            get
            {
                return CAMS.Module.Classi.SetVarSessione.IsAdminRuolo;
            }
        }

        [NonPersistent]
        [System.ComponentModel.Browsable(false)]
        public bool VisualizzaDataFermo
        {
            get
            {
                if (Oid == -1)
                {
                    try
                    {
                        return this.Edificio.Commesse.MostraDataOraFermo;
                    }
                    catch
                    {
                        return false;
                    }

                }
                else
                {
                    if (this.RegistroRdL == null)
                    {
                        try
                        {
                            return this.Edificio.Commesse.MostraDataOraFermo;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                    try
                    {
                        return this.RegistroRdL.MostraDataOraFermo;
                    }
                    catch
                    {
                        return false;
                    }

                }
            }
        }

        #endregion
        //[Association(@"Documenti_RdL", typeof(Documenti)), DevExpress.Xpo.Aggregated, System.ComponentModel.DisplayName("Documenti")]
        //[Appearance("RdL.Documentis.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1", Visibility = ViewItemVisibility.Hide)]
        //public XPCollection<Documenti> Documentis
        //{
        //    get   
        //    {
        //        return GetCollection<Documenti>("Documentis");
        //    }
        //}

        ////
        //[Association("RdL_AttivitaPianificateDett", typeof(MpAttivitaPianificateDett)), DevExpress.Xpo.Aggregated]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Elenco Attività Pianificate in Dettaglio")]
        //[Appearance("RdL.MpAttivitaPianificateDetts.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or Categoria.Oid != 1 Or MpAttivitaPianificateDetts.Count = 0", Visibility = ViewItemVisibility.Hide)]
        //public XPCollection<MpAttivitaPianificateDett> MpAttivitaPianificateDetts
        //{
        //    get
        //    {
        //        if (this.Oid == -1) return null;

        //        return GetCollection<MpAttivitaPianificateDett>("MpAttivitaPianificateDetts");

        //    }
        //}

        //[Association(@"Notifiche_RdL", typeof(RegNotificheEmergenze)), System.ComponentModel.DisplayName("Elenco Notifiche Emergenze")]
        //// [Appearance("RdL.RegNEmergenzes.Hide", Criteria = "Oid = -1 Or Categoria.Oid != 4", Visibility = ViewItemVisibility.Hide)]
        //[Appearance("RdL.RegNEmergenzes.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or Categoria.Oid != 4 Or RegNEmergenzes.Count = 0", Visibility = ViewItemVisibility.Hide)]
        //public XPCollection<RegNotificheEmergenze> RegNEmergenzes
        //{
        //    get
        //    {
        //        return GetCollection<RegNotificheEmergenze>("RegNEmergenzes");
        //    }
        //}

        //[Association(@"RdLNote_RdL", typeof(RdLNote)), System.ComponentModel.DisplayName("RdL Note")]
        //[Appearance("RdL.RdLNotes.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or Categoria.Oid != 4", Visibility = ViewItemVisibility.Hide)]
        //public XPCollection<RdLNote> RdLNotes
        //{
        //    get
        //    {
        //        return GetCollection<RdLNote>("RdLNotes");
        //    }
        //}


        //[Association(@"RdLApparatoSchedeMp_RdL", typeof(RdLApparatoSchedeMP)), System.ComponentModel.DisplayName("Elenco Procedure MP Collegate")]
        //// [Appearance("RdL.ApparatoSchedaMPs.Hide", Criteria = "Apparato is null Or Categoria.Oid = 4 Or Categoria.Oid = 1", Visibility = ViewItemVisibility.Hide)]
        //[Appearance("RdL.RdLApparatoSchedaMPs.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or Categoria.Oid = 4 Or RdLApparatoSchedaMPs.Count() = 0", Visibility = ViewItemVisibility.Hide)]
        //[ToolTip("Elenco delle Attività di manutenzione Associate alla Richiesta di Lavoro")]
        //public XPCollection<RdLApparatoSchedeMP> RdLApparatoSchedaMPs
        //{
        //    get
        //    {
        //        return GetCollection<RdLApparatoSchedeMP>("RdLApparatoSchedaMPs");
        //    }
        //}     

        //[Association(@"RdLMultiRisorseTeam_RdL", typeof(RdLMultiRisorseTeam)), DevExpress.Xpo.Aggregated, System.ComponentModel.DisplayName("Elenco RisorseTeam Collegate")]
        //[Appearance("RdL.RdLMultiRisorseTeam.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or UltimoStatoSmistamento.Oid In(1,10)", Visibility = ViewItemVisibility.Hide)]
        //[ToolTip("Elenco RisorseTeam Collegate")]
        //public XPCollection<RdLMultiRisorseTeam> RdLMultiRisorseTeams
        //{
        //    get
        //    {
        //        return GetCollection<RdLMultiRisorseTeam>("RdLMultiRisorseTeams");
        //    }
        //}




        //private XPCollection<RegistroCosti> fRegistroCosti_;
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        //[XafDisplayName("Rigistro Costi")]
        //public XPCollection<RegistroCosti> RegistroCosti_
        //{
        //    get
        //    {
        //        if (this.RegistroRdL != null)
        //        {
        //            if (fRegistroCosti_ == null)
        //            {
        //                //this.RegistroRdL.Oid
        //                CriteriaOperator op = CriteriaOperator.Parse(
        //                        string.Format("RegistroRdL.Oid == '{0}'", this.RegistroRdL.Oid));
        //                fRegistroCosti_ = new XPCollection<RegistroCosti>(Session, op);//.Where(w => w.RegistroRdL.Oid == this.RegistroRdL.Oid);
        //                //fRegistroCosti_.CriteriaString = "RegistroRdL.Oid='@This.RegistroRdL.Oid'";
        //                fRegistroCosti_.BindingBehavior = CollectionBindingBehavior.AllowNone;
        //            }
        //        }
        //        return fRegistroCosti_;
        //    }
        //}



        public override string ToString()
        {
            if (this.Oid == -1) return null;
            if (this.Descrizione == null) return null;

            if (Codice != null)
                return string.Format("{0}({1})", Descrizione.Length < 101 ? Descrizione : Descrizione.Remove(100) + "...", Codice);
            else
                return string.Format("{0}", Descrizione.Length < 101 ? Descrizione : Descrizione.Remove(100) + "...");
        }
    }
}
