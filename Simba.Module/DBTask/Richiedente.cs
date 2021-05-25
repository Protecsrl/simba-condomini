using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("RICHIEDENTE")]
    //  [System.ComponentModel.DefaultProperty("NomeCognome")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Richiedenti")]
    [ImageName("Person")]
    [NavigationItem("Contratti")]
    [Appearance("Richiedente.inCreazione.noVisibile", TargetItems = "Commesse", Criteria = @"Oid == -1 And DisabilitaCommesse", Visibility = ViewItemVisibility.Hide)]

    public class Richiedente : XPObject
    {
        public Richiedente()
            : base()
        {
        }
        public Richiedente(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // this.DisabilitaCommesse
            this.fTelefonoRichiedente = "0039";
            this._PhoneMobString = "0039";
        }


        private string fNomeCognome;
        [Size(60),
        Persistent("NOMECOGNOME"),
        DisplayName("Nome e Cognome")]
        [RuleRequiredField("RReqObligato.Richiedente.NomeCognome", DefaultContexts.Save, "è un campo obligatorio")]
        [ToolTip("Nominativo del Richiedente")]
        [DbType("varchar(60)")]
        public string NomeCognome
        {
            get
            {
                return fNomeCognome;
            }
            set
            {
                SetPropertyValue<string>("NomeCognome", ref fNomeCognome, value);
            }
        }

        private TipoRichiedente fTipoRichiedente;
        [Persistent("TIPORICHIEDENTE"),
       DisplayName("Tipo Richiedente")]
        [RuleRequiredField("RReqObligato.Richiedente.TipoRichiedente", DefaultContexts.Save, "è un campo obligatorio")]
        [ToolTip("Identificazione del Richiedente")]
        public TipoRichiedente TipoRichiedente
        {
            get
            {
                return fTipoRichiedente;
            }
            set
            {
                SetPropertyValue<TipoRichiedente>("TipoRichiedente", ref fTipoRichiedente, value);
            }
        }

        //private string fTelefonoRichiedente;
        //[Size(250),    Persistent("TELEFONO"),      DisplayName("Telefono Richiedente")]
        //[DbType("varchar(250)")]
        //public string TelefonoRichiedente
        //{
        //    get
        //    {
        //        return fTelefonoRichiedente;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("TelefonoRichiedente", ref fTelefonoRichiedente, value);
        //    }
        //}

        private string fTelefonoRichiedente;
        private const string PhoneStringEditMask = "(0000)000000099999";
        [VisibleInListView(true)]
        [ModelDefault("EditMaskType", "Simple")]
        [ModelDefault("DisplayFormat", "{0:" + PhoneStringEditMask + "}")]
        [ModelDefault("EditMask", PhoneStringEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("indicare il numero telefonico del mobile che riceve l'SMS, : " + PhoneStringEditMask, null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Size(20), Persistent("TELEFONO"), DisplayName("Telefono Fisso")]
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
        [RuleRegularExpression("Richiedente_Email_RuleRegExpression", DefaultContexts.Save, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
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
        private Contratti fCommesse;
        [Persistent("COMMESSE"), DisplayName("Commesse")]
        [RuleRequiredField("RReqObligato.Richiedente.Commesse", DefaultContexts.Save, "è un campo obligatorio")]
        [ToolTip("Identificazione della Commesse")]
        [ExplicitLoading]
        [Delayed(true)]
        public Contratti Commesse
        {
            get { return GetDelayedPropertyValue<Contratti>("Commesse"); }
            set { SetDelayedPropertyValue<Contratti>("Commesse", value); }
        }

        private string fNote;
        [Size(350), Persistent("NOTE"), DisplayName("Note")] //346 3228369
        //[RuleRegularExpression("Richiedente_Email_RuleRegExpression", DefaultContexts.Save, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        [DbType("varchar(350)")]
        [Delayed(true)]
        public string Note
        {
            get { return this.fNote; }
            set { SetDelayedPropertyValue<string>("Note", value); }
         
        }

        [NonPersistent]
        [System.ComponentModel.Browsable(false)]
        public bool DisabilitaCommesse { get; set; }



        public override string ToString()
        {

            bool hasNote = (!string.IsNullOrEmpty(this.Note));
            string Nomecognomeutente = string.Empty;//Infosat
            if (hasNote
                && this.TipoRichiedente != null 
                && this.TipoRichiedente.Descrizione.Contains("Client") 
                &&     this.Note.Length > 4 && this.Note.Length < 21)
                Nomecognomeutente =  string.Format("{0}[{1}]", NomeCognome, Note);
            else
                Nomecognomeutente = string.Format("{0}", this.NomeCognome);

            if (this.TelefonoRichiedente != null && this.TipoRichiedente != null)
            {
                if (this.TelefonoRichiedente.Length>4)
                {
                    return string.Format("{0}({1},{2})", Nomecognomeutente, TipoRichiedente.Descrizione, this.TelefonoRichiedente);
                }
                else
                {
                    return string.Format("{0}({1})", Nomecognomeutente, TipoRichiedente.Descrizione);
                }
            }

           if (TipoRichiedente != null)
                return string.Format("{0}({1})", Nomecognomeutente, TipoRichiedente.Descrizione);
            

                return string.Format("{0}", Nomecognomeutente);
        }

    }
}
