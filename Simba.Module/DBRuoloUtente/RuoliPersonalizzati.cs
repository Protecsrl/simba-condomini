using CAMS.Module.Classi;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBRuoloUtente
{
    [NavigationItem("Amministrazione"), System.ComponentModel.DisplayName("Gestione Ruoli personalizzati")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Gestione Ruoli personalizzati")]
    [DefaultClassOptions, Persistent("RUOLOPERSONALIZZATO")]
    [VisibleInDashboards(false)]

    public class RuoliPersonalizzati : XPObject
    {
        public RuoliPersonalizzati()
           : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public RuoliPersonalizzati(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        private SecuritySystemRole fSecurityRole;
        [Persistent("SECURITYROLE"), DisplayName("Ruolo")]
        [VisibleInListView(true)]
        public SecuritySystemRole SecurityRole
        {
            get { return fSecurityRole; }
            set { SetPropertyValue<SecuritySystemRole>("SecurityRole", ref fSecurityRole, value); }
        }

        [Persistent("SECURITYUSER"), XafDisplayName("Security User")]
        [Delayed(true)]
        public SecuritySystemUser SecurityUser
        {
            get { return GetDelayedPropertyValue<SecuritySystemUser>("SecurityUser"); }
            set { SetDelayedPropertyValue<SecuritySystemUser>("SecurityUser", value); }
        }

        private TipoConfigurazione fTipoConfigurazione;
        [Persistent("TIPOCONFIG"), DisplayName("Tipo Configurazione")]
        public TipoConfigurazione TipoConfigurazione
        {
            get { return fTipoConfigurazione; }
            set { SetPropertyValue<TipoConfigurazione>("TipoConfigurazione", ref fTipoConfigurazione, value); }
        }

        private string fNavigationItemID;
        [Size(200), Persistent("NAVIGATIONID"), DisplayName("NavigationItem")]
        [Appearance("RuoliPersonalizzati.NavigationItemID", @"TipoConfigurazione != 1", Visibility = ViewItemVisibility.Hide)]
        public string NavigationItemID
        {
            get { return fNavigationItemID; }
            set { SetPropertyValue<string>("NavigationItemID", ref fNavigationItemID, value); }
        }

        private TipoNavigationItem fTipoNavigationItem;
        [ Persistent("TIPONAVIGATION"), DisplayName("Tipo Navigation Item")]
        public TipoNavigationItem TipoNavigationItem
        {
            get { return fTipoNavigationItem; }
            set { SetPropertyValue<TipoNavigationItem>("TipoNavigationItem", ref fTipoNavigationItem, value); }
        }

    
        //[Appearance("PriorityBackColorPink", AppearanceItemType = "ViewItem", Criteria = "Priority=2", BackColor = "0xfff0f0")]
        private string fAction;
        [Size(300), Persistent("ACTION"), DisplayName("Azione")]
        [Appearance("RuoliPersonalizzati.TIPO.Action", @"TipoConfigurazione != 2", Visibility = ViewItemVisibility.Hide)]
        public string Action
        {
            get { return fAction; }
            set { SetPropertyValue<string>("Action", ref fAction, value); }
        }

        private Boolean fAttivo;
        [ Persistent("ATTIVO"), DisplayName("Attivo")]
        public Boolean Attivo
        {
            get { return fAttivo; }
            set { SetPropertyValue<Boolean>("Attivo", ref fAttivo, value); }
        }

    }
}
