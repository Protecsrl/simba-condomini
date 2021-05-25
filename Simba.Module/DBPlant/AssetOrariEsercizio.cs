using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
//namespace CAMS.Module.DBPlant
//{
//    class ApparatoOrariEsercizio
//    {
//    }
//}
namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("ASSETORARIESERCIZIO")]
    //[RuleCombinationOfPropertiesIsUnique("UniqueApparatoCarTecniche", DefaultContexts.Save, "Apparato, StdApparatoCaratteristicheTecniche,ParentObject")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Orari Funzionamento")]
    [System.ComponentModel.DefaultProperty("FullValore")]
    [ImageName("Action_EditModel")]
    [NavigationItem(false)]//"Consistenza"
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("ApparatoCaratteristicheTecniche", "", "Tutto", true, Index = 1)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("ApparatoCaratteristicheTecniche.UnitamisuraAdim", "DescUnitaMisura = 'Adimensionale'", "UM-Adimensionale", Index = 0)]


    public class AssetOrariEsercizio : XPObject
    {
        public AssetOrariEsercizio() : base() { }
        public AssetOrariEsercizio(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {

            }
        }
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        //apparato, dataggiorno, ore assensione, potenz<a, tot energia
        private Asset fAsset;
        [Persistent("ASSET"), Association(@"Asset_AssetOrariEsercizio"), System.ComponentModel.DisplayName("Apparato")]
        [ExplicitLoading()]
        [VisibleInListView(false)]
        public Asset Asset
        {
            get { return fAsset; }
            set { SetPropertyValue<Asset>("Asset", ref fAsset, value); }
        }

        private DateTime fDataAccensione;
        [Persistent("DATAACCENSIONE"), System.ComponentModel.DisplayName("Data Accensione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Accensione", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime DataAccensione
        {
            get { return fDataAccensione; }
            set { SetPropertyValue<DateTime>("DataAccensione", ref fDataAccensione, value); }
        }

        private DateTime fDataSpegnimento;
        [Persistent("DATASPEGNIMENTO"), System.ComponentModel.DisplayName("Data Spegnimento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Spegnimento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime DataSpegnimento
        {
            get { return fDataSpegnimento; }
            set { SetPropertyValue<DateTime>("DataSpegnimento", ref fDataSpegnimento, value); }
        }

  
        private TimeSpan? fTimeEsercizio;
        public TimeSpan? TimeEsercizio
        {
            get { return fTimeEsercizio; }
            set { SetPropertyValue("TimeEsercizio", ref fTimeEsercizio, value); }
        }

        private TimeSpan? fTimeEsercizioF1;
        public TimeSpan? TimeEsercizioF1
        {
            get { return fTimeEsercizioF1; }
            set { SetPropertyValue("TimeEsercizioF1", ref fTimeEsercizioF1, value); }
        }

        private TimeSpan? fTimeEsercizioF2;
        public TimeSpan? TimeEsercizioF2
        {
            get { return fTimeEsercizioF2; }
            set { SetPropertyValue("TimeEsercizioF2", ref fTimeEsercizioF2, value); }
        }

        private TimeSpan? fTimeEsercizioF3;
        public TimeSpan? TimeEsercizioF3
        {
            get { return fTimeEsercizioF3; }
            set { SetPropertyValue("TimeEsercizioF3", ref fTimeEsercizioF3, value); }
        }

        private double fPotenzaAssorbita;
        [Persistent("POTENZAASSORBITA"), System.ComponentModel.DisplayName("Potenza Assorbita")]
        //[Appearance("ApparatoCaratteristicheTecniche.ValoreIntero", AppearanceItemType.LayoutItem,
        //    @"StdApparatoCaratteristicheTecniche.TipoValoreCaratteristicaTecnica != 'Intero'", Visibility = ViewItemVisibility.Hide)]
        //[VisibleInListView(false)]
        //[ImmediatePostData]
        public double PotenzaAssorbita
        {
            get { return fPotenzaAssorbita; }
            set { SetPropertyValue<double>("PotenzaAssorbita", ref fPotenzaAssorbita, value); }
        }


        private double fEnergiaAttiva;
        [Persistent("ENERGIAATTIVA"), System.ComponentModel.DisplayName("Energia Attiva")]
        //[Appearance("ApparatoCaratteristicheTecniche.ValoreIntero", AppearanceItemType.LayoutItem,
        //    @"StdApparatoCaratteristicheTecniche.TipoValoreCaratteristicaTecnica != 'Intero'", Visibility = ViewItemVisibility.Hide)]
        //[VisibleInListView(false)]
        //[ImmediatePostData]
        public double EnergiaAttiva
        {
            get { return fEnergiaAttiva; }
            set { SetPropertyValue<double>("EnergiaAttiva", ref fEnergiaAttiva, value); }
        }



 

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
            get { return fDataAggiornamento; }
            set { SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value); }
        }

         private string fUserName;
        [Persistent("USERNAME")]
        [System.ComponentModel.Browsable(false)]
        public string UserName
        {
            get { return fUserName; }
            set { SetPropertyValue<string>("UserName", ref fUserName, value); }
        }
         

    }
}
