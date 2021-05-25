
using CAMS.Module.Classi;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CAMS.Module.DBClienti
{
    [DefaultClassOptions, Persistent("CLIENTICONTATTI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Referenti Cliente")]
    [ImageName("PackageProduct")]
    [NavigationItem("Contratti")]
      public class ClientiContatti : XPObject    
    {
        public ClientiContatti() : base() { }
        public ClientiContatti(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private string fDenominazione;
        [Size(150), Persistent("DENOMINAZIONE"), DisplayName("Denominazione")]
        [DbType("varchar(150)")]
        public string Denominazione
        {
            get
            {
                return fDenominazione;
            }
            set
            {
                SetPropertyValue<string>("Denominazione", ref fDenominazione, value);
            }
        }

        private TipoContattoCliente fTipoContattoCliente;  
        [Persistent("TIPOCONTATTO"), DisplayName("Tipo Contatto Cliente")]
        public TipoContattoCliente TipoContattoCliente
        {
            get
            {
                return fTipoContattoCliente;
            }
            set
            {
                SetPropertyValue<TipoContattoCliente>("TipoContattoCliente", ref fTipoContattoCliente, value);
            }
        }

        private string fIndirizzo;
        [Size(250), Persistent("INDIRIZZO"), DisplayName("Indirizzo")]
        [DbType("varchar(250)")]
        public string Indirizzo
        {
            get
            {
                return fIndirizzo;
            }
            set
            {
                SetPropertyValue<string>("Indirizzo", ref fIndirizzo, value);
            }
        }

        private string fTelefono;
        [Size(250), Persistent("TELEFONO"), DisplayName("Contatti Telefonici")] //346 3228369
        [DbType("varchar(250)")]
        public string Telefono
        {
            get
            {
                return fTelefono;
            }
            set
            {
                SetPropertyValue<string>("Telefono", ref fTelefono, value);
            }
        }

        private string fTelefonoRichiedente;
        private const string PhoneStringEditMask = "(0000)000000099999";
        [VisibleInListView(true)]
        [ModelDefault("EditMaskType", "Simple")]
        [ModelDefault("DisplayFormat", "{0:" + PhoneStringEditMask + "}")]
        [ModelDefault("EditMask", PhoneStringEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("indicare il numero telefonico del mobile che riceve l'SMS, : " + PhoneStringEditMask, null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Size(20), Persistent("TELEFONOFISSO"), DisplayName("Telefono Fisso")]
        [DbType("varchar(20)")]
        public string TelefonoRichiedente
        {
            get
            {
                return fTelefonoRichiedente;
            }
            set
            {
                SetPropertyValue("TelefonoRichiedente", ref fTelefonoRichiedente, value);
            }
        }

        private string _PhoneMobString;//                    346-3228369
        private const string PhoneMobStringEditMask = "(0000)000-0000009";
        [VisibleInListView(true)]
        [ModelDefault("EditMaskType", "Simple")]
        [ModelDefault("DisplayFormat", "{0:" + PhoneMobStringEditMask + "}")]
        [ModelDefault("EditMask", PhoneMobStringEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip(@"indicare il numero telefonico del mobile che riceve l'SMS, : " + PhoneMobStringEditMask, null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Size(20), Persistent("TELEFONOMOBILE"), DisplayName("Telefono Mobile")]
        [DbType("varchar(20)")]
        public string PhoneMobString
        {
            get
            {
                return _PhoneMobString;
            }
            set
            {
                SetPropertyValue("PhoneMobString", ref _PhoneMobString, value);
            }
        }

        private string fMail;
        [Size(80), Persistent("EMAIL"), DisplayName("Mail")] //346 3228369
        [RuleRegularExpression("ContattiClienti_Email_RuleRegExpression", DefaultContexts.Save, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        [DbType("varchar(80)")]
        public string Mail
        {
            get
            {
                return fMail;
            }
            set
            {
                SetPropertyValue<string>("Mail", ref fMail, value);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}", this.Denominazione);
        }

    }
}
