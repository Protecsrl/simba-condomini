using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Drawing;
using CAMS.Module.DBPlant;

namespace CAMS.Module.DBTask.ParametriPopUp
{
    [DefaultClassOptions]
    [VisibleInDashboards(false)]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    //   SE PROCEDERE CON LE REGOLE DI CAMPO OGBLIGATORIO SU RISORSA E SATATO SMISTAMENTO SE I VACCHI SONO DIVERSI DA ZERO
    //[Appearance("ParametriPopUp.Enabled", TargetItems = "*", Enabled =false, Priority = 1,
    //    Context = "Any", Criteria = "Abilitato = true")] //In Emergenza da Assegnare
    //[Appearance("ParametriPopUp.CentroOperativo.Enabled", TargetItems = "CentroOperativo", Enabled = false, 
    //    Context = "Any")] 
    //[Appearance("ParametriPopUp.Enabled", TargetItems = "CentroOperativo;RisorseTeamNew;RisorseTeamOld", Enabled = false)]
    // [Appearance("RdL.noEditabili.nero", TargetItems = "UtenteCreatoRichiesta;DataCreazione;CodiciRdLOdL", FontColor = "Black", Enabled = false)]


    public class RdLParametriAzioniMassive : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public RdLParametriAzioniMassive(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
       

        #region chiudi interventi
        private bool fStessaPianificata;
        [Persistent("STESSAPIANIFICATA"), XafDisplayName("Con la Data Pianificata")]
        [ImmediatePostData]
        [ToolTipAttribute("selezionare se si vuole utilizzare la stessa data Pianificata di ogni attività come data di completamento")]
        public bool StessaPianificata
        {
            get
            {
                return fStessaPianificata;
            }
            set
            {
                SetPropertyValue<bool>("StessaPianificata", ref fStessaPianificata, value);
            }
        }


        private bool fAbilitato;
        public bool Abilitato
        {
            get
            {
                return fAbilitato;
            }
            set
            {
                SetPropertyValue<bool>("Abilitato", ref fAbilitato, value);
            }
        }


        private string fMessaggio;
        public String Messaggio
        {
            get
            {
                return fMessaggio;
            }
            set
            {
                SetPropertyValue<string>("Messaggio", ref fMessaggio, value);
            }
        }


        private DateTime fDataCompletamento;
        [XafDisplayName("Data Completamento")]
        [Index(0), VisibleInListView(false)]
        [Persistent("DATACOMPLETAMENTO")]
        //[ RuleRequiredField(DefaultContexts.Save)]
        [Appearance("RdLParametriAzioniMassive.DataCompletamento.Enabled", AppearanceItemType.ViewItem, "StessaPianificata", Enabled = false)]
        [ToolTipAttribute("impostare qui la data per completare le attività selezionate con la stessa data. se vuoto su utilizzerà la data pianificata")]
        [ImmediatePostData]
        public DateTime DataCompletamento
        {
            get { return fDataCompletamento; }
            set { SetPropertyValue("DataCompletamento", ref fDataCompletamento, value); }
        }


        private string fNoteCompletamento;
        [XafDisplayName("Note Completamento")]
        //[ModelDefault("EditMask", "(000)-00")]
        [Index(1), VisibleInListView(false)]
        [Persistent("NOTECOMPLETAMENTO")]
        [ToolTipAttribute("impostare qui la nata di completamento che verrà registrata su tutte le attività selezionate. vuoto per nessuna nota di completamento.")]
        public string NoteCompletamento
        {
            get { return fNoteCompletamento; }
            set { SetPropertyValue("NoteCompletamento", ref fNoteCompletamento, value); }
        }

        private SecuritySystemUser fSecurityUser;
        [Persistent("SECURITYUSERID"),
        XafDisplayName("Security User")]
        [RuleUniqueValue("RdLParametriAzioniMassive.SecuritySystemUser.", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        [ExplicitLoading()]
        [Delayed(true)]
        [Browsable(false)]
        public SecuritySystemUser SecurityUser
        {
            get
            {
                return GetDelayedPropertyValue<SecuritySystemUser>("SecurityUser");
            }
            set
            {
                SetDelayedPropertyValue<SecuritySystemUser>("SecurityUser", value);
            }

        }
        #endregion

        #region cambiarisorsa
        private RisorseTeam fRisorseTeamOld;  //  @@@@@@@@@@  obligatoria solo in assegnazione
        [Appearance("RdLParametriAzioniMassive.RisorseTeamOld.Enabled", FontColor = "Black", Enabled = false)]
        [Persistent("RISORSATEAM_OLD"), System.ComponentModel.DisplayName("Team Operativo da Sostituire")]
        //[DataSourceProperty("ListaFiltraComboRisorseTeam")]
        [ExplicitLoading]
        public RisorseTeam RisorseTeamOld
        {
            get
            {
                return fRisorseTeamOld;
            }
            set
            {
                SetPropertyValue<RisorseTeam>("RisorseTeamOld", ref fRisorseTeamOld, value);
            }
        }

        private CentroOperativo fCentroOperativo;  //  @@@@@@@@@@  obligatoria solo in assegnazione
        [NonPersistent, System.ComponentModel.DisplayName("Centro Operativo del Team")]
        [Appearance("RdLParametriAzioniMassive.CentroOperativo.Enabled", FontColor = "Black",  Enabled = false)]
        public CentroOperativo CentroOperativo
        {
            get
            {
                return fCentroOperativo;
            }
            set
            {
                SetPropertyValue<CentroOperativo>("CentroOperativo", ref fCentroOperativo, value);
            }
        }


        private string fRicercaRisorseTeam;
        [NonPersistent, Size(25)]//, DisplayName("Filtro"), , Size(25)
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false), VisibleInDashboards(false), VisibleInReports(false)]
        public string RicercaRisorseTeam
        {
            get;
            set;
        }

        private RisorseTeam fRisorseTeamNew;  //  @@@@@@@@@@  obligatoria solo in assegnazione
        [Persistent("RISORSATEAM"), System.ComponentModel.DisplayName("Team Operativo da Associare")]
        [Appearance("RdLParametriAzioniMassive.RisorseTeamNew.Enabled", FontColor = "Black", Enabled = false)]
        [ExplicitLoading]
        public RisorseTeam RisorseTeamNew
        {
            get
            {
                return fRisorseTeamNew;
            }
            set
            {
                SetPropertyValue<RisorseTeam>("RisorseTeamNew", ref fRisorseTeamNew, value);
            }
        }

        private StatoSmistamento fStatoSmistamento_old;
        [XafDisplayName("Stato Smistamento Attuale")]
        [Appearance("RdLParametriAzMassive.StatoSmistamento_old.disable",  Enabled = false, FontColor = "Black")]
        [Appearance("RdLParametriAzMassive.StatoSmistamento_old1.disable",
        TargetItems = "StatoSmistamento_old;CentroOperativo;RisorseTeamNew;RisorseTeamOld",
        Criteria = @"UltimoStatoSmistamento.Oid >0",
            Enabled = false, FontColor = "Black" )]

        [VisibleInListView(false)]
        [Persistent("STATOSMISTAMENTO_OLD")]
        [ToolTipAttribute("Stato Smistamento da sostituire o confermare per tutte le attività selezionate. vuoto per nessuna nota di completamento .")]
        public StatoSmistamento StatoSmistamento_old
        {
            get { return fStatoSmistamento_old; }
            set { SetPropertyValue("StatoSmistamento_old", ref fStatoSmistamento_old, value); }
        }



        private StatoSmistamento fUltimoStatoSmistamento;
        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.DisplayName("Stato Smistamento di aggiornamento")]
        [RuleRequiredField("RdLParametriAzMassive.ModificaRdL.UltimoStatoSmistamento", DefaultContexts.Save, "La Stato Smistamento è un campo obbligatorio")]
        [DataSourceCriteria("Oid In(1, 2, 11, 6)")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        //[Delayed(true)]
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

   
        #endregion

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (IsInvalidated)
                return;

            if (!this.IsLoading)
            {
                if (newValue != null && propertyName == "StessaPianificata")
                {
                    bool newV = (bool)(newValue);
                    if (newV)
                    {
                        this.DataCompletamento = DateTime.MinValue;
                    }
                }

                if (newValue != null && propertyName == "UltimoStatoSmistamento")
                {
                    StatoSmistamento newSS = (StatoSmistamento)(newValue);
                    this.RisorseTeamNew = null;

                    //if (newSS.Oid == 1)
                    //{
                    //    this.RisorseTeamNew = null;
                    //}
                    //if (newSS.Oid == 2)
                    //{
                    //    if (this.RisorseTeamNew != null)
                    //    {
                    //        if (this.RisorseTeamNew.RisorsaCapo.SecurityUser == null)
                    //            this.RisorseTeamNew = null;

                    //    }

                    //}
                }
            }
        }

    }
}



