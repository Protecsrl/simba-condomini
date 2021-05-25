

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Drawing;
using DevExpress.ExpressApp.ConditionalAppearance;
// 

using System;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("STRADE")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Strade")]
    //[FriendlyKeyProperty("FullName")]
    [DefaultProperty("FullName")]
    //[Appearance("Strade.inCreazione.noVisibile", TargetItems = "Name;Children;Parent;Apparatis;ApparatoPadre;AppSchedaMpes;Documentis;ControlliNormativis;RegMisureDettaglios;MasterDettaglios", Criteria = @"Oid == -1", Visibility = ViewItemVisibility.Hide)]
    //[RuleCombinationOfPropertiesIsUnique("Unique.Apparato.Descrizione", DefaultContexts.Save, "Impianto,CodDescrizione, Descrizione")]
    //// [Appearance("Apparato.BackColor.Salmon", TargetItems = "*", BackColor = "Salmon", FontColor = "Black", Priority = 1, Criteria = "SumTempoMp = 0")]
    ////[Appearance("Apparato.BackColor.Salmon", TargetItems = "*", FontStyle = FontStyle.Bold, FontColor = "Salmon", Priority = 1, Criteria = "NumApp = 0")]
    //[Appearance("Apparato.Abilitato.No", TargetItems = "*;Abilitato", Enabled = false, Criteria = "Abilitato = 'No' Or Impianto.Abilitato = 'No' Or Impianto.Immobile.Abilitato = 'No'")]
    //[Appearance("Apparato.Abilitato.BackColor", TargetItems = "*;Abilitato", FontStyle = FontStyle.Strikeout,
    //    FontColor = "Salmon", Priority = 1, Criteria = "Abilitato = 'No' Or AbilitazioneEreditata == 'No'")]

    #region filtri

    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo", "Abilitato == 1 And AbilitazioneEreditata == 1", "Attivi", true, Index = 0)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloNonAttivo", "Abilitato == 0 Or AbilitazioneEreditata == 0", "non Attivi", Index = 1)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "Tutto", Index = 2)]

    #endregion

    [ImageName("LoadPageSetup")]
    [NavigationItem("Patrimonio")]
    public class Strade : XPObject
    {
        public Strade() : base() { }
        public Strade(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {

            }
        }

        private string fStrada;
        [Persistent("STRADA")]
        [Size(500), DevExpress.Xpo.DisplayName("Strada")]
        [DbType("varchar(500)")]
        [RuleRequiredField("RReqField.Strade.Strada", DefaultContexts.Save, "è un campo obbligatorio")]
        [ImmediatePostData(true)]
        public string Strada
        {
            get
            {
                return fStrada;
            }
            set
            {
                SetPropertyValue<string>("Strada", ref fStrada, value);
            }
        }


        private Comuni fComune;
        [Persistent("COMUNI"),
        DevExpress.Xpo.DisplayName("Comune")]
        //[Appearance("Strade.Abilita.Comuni", Criteria = "Provincia is null", Enabled = false)]
        [ImmediatePostData(true)]
        //[DataSourceCriteria("Provincia = '@This.Provincia'")]
        [RuleRequiredField("RReqField.Strade.Comuni", DefaultContexts.Save, "è un campo obbligatorio")]
        [ExplicitLoading]
        [Delayed(true)]
        public Comuni Comune
        {
            get
            {
                return GetDelayedPropertyValue<Comuni>("Comune");
            }
            set
            {
                SetDelayedPropertyValue<Comuni>("Comune", value);
            }

        }


        private string fCivico;
        [Size(50), Persistent("CIVICO"), DevExpress.Xpo.DisplayName("Civico")]
        [DbType("varchar(50)")]
        [ImmediatePostData(true)]
        public string Civico
        {
            get
            {
                return fCivico;
            }
            set
            {
                SetPropertyValue<string>("Civico", ref fCivico, value);
            }
        }

        private string fCap;
        [Size(50), Persistent("CAP"), DevExpress.Xpo.DisplayName("CAP")]
        [DbType("varchar(50)")]
        //[RuleRequiredField("RReqField.Indirizzo.Civico", DefaultContexts.Save, "è un campo obbligatorio")]
        public string Cap
        {
            get
            {
                return fCap;
            }
            set
            {
                SetPropertyValue<string>("Cap", ref fCap, value);
            }
        }
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"), System.ComponentModel.DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Aggiornamento dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
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


        [PersistentAlias("Iif(Comune is not null And Strada is not null, Strada + ' - ' + Civico,null)")]
        [System.ComponentModel.DisplayName("FullName")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string FullName
        {
            get
            {
                object tempObject = EvaluateAlias("FullName");
                if (tempObject != null)
                    return (string)tempObject;
                else
                    return null;
            }
        }

    }
}



 