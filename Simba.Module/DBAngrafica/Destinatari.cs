using CAMS.Module.Classi;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.Linq;

namespace CAMS.Module.DBAngrafica
{
    [NavigationItem("Amministrazione"),
    System.ComponentModel.DisplayName("Destinatari e-mail")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Destinatari e-Mail")]
    [DefaultClassOptions, Persistent("DESTINATARI")]
    [RuleCombinationOfPropertiesIsUnique("UniqrRubricaDestinatari", DefaultContexts.Save, "Email,Nome,Cognome,RoleAzione,SecurityRole", SkipNullOrEmptyValues = false)]
    [ImageName("NewMail")]

    //        GetMAILvalidate
//    [RuleCriteria("RuleError.Destinatari.GetSMSvalidate", DefaultContexts.Save, @"GetSMSvalidate",
//CustomMessageTemplate = "si è scelto di trasmettere SMS nell'elenco azioni, inserire quindi numero di telefono", SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Error)]

    //        GetMAILvalidate
//    [RuleCriteria("RuleError.Destinatari.GetMAILvalidate", DefaultContexts.Save, @"GetMAILvalidate",
//CustomMessageTemplate = "si è scelto di trasmettere mail nell'elenco azioni, inserire quindi indirizzo mail di destinazione", SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Error)]

    [RuleCriteria("RuleError.Destinatari.Valida.EmailSms", DefaultContexts.Save, "IsNullOrEmpty(Email) and IsNullOrEmpty(PhoneString)",
       CustomMessageTemplate = "Inserire o un email o un numero telefonico  validi!",
       SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    //[RuleCriteria("RuleError.Destinatari.Valida.telefono", DefaultContexts.Save, "PhoneString='(0039)000-0000000'",
    // CustomMessageTemplate = "Numero telefonico non inserito! Modificare",
    // SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]
    //[Appearance("Destinatari.Ruolo.noVisibile", AppearanceItemType.LayoutItem, "*",
    //   TargetItems = "panCompletamento;panRisorsa", Visibility = ViewItemVisibility.Hide)]


    public class Destinatari : XPObject
    {
        public Destinatari()
            : base()
        {
        }
        public Destinatari(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fNome;
        [Size(50),
        Persistent("NOME"),
        DisplayName("Nome")]
        [DbType("varchar(50)")]
        public string Nome
        {
            get
            {
                return fNome;
            }
            set
            {
                SetPropertyValue<string>("Nome", ref fNome, value);
            }
        }

        private SecuritySystemRole fSecurityRole;
        [Persistent("SECURITYROLE"),
        DisplayName("Ruolo")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public SecuritySystemRole SecurityRole
        {
            get
            {
                return GetDelayedPropertyValue<SecuritySystemRole>("SecurityRole");
            }
            set
            {
                SetDelayedPropertyValue<SecuritySystemRole>("SecurityRole", value);
            }

        }

        private TipoAzioneRoleDestinatari fRoleAzione;
        [Persistent("AZIONEROLE"),
        DisplayName("Azione")]
        [ImmediatePostData(true)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public TipoAzioneRoleDestinatari RoleAzione
        {
            get
            {
                return fRoleAzione;
            }
            set
            {
                SetPropertyValue<TipoAzioneRoleDestinatari>("RoleAzione", ref fRoleAzione, value);
            }
        }

        private string fCognome;
        [Size(150),
        Persistent("COGNOME"),
        DisplayName("Cognome")]
        [DbType("varchar(150)")]
        public string Cognome
        {
            get
            {
                return fCognome;
            }
            set
            {
                SetPropertyValue<string>("Cognome", ref fCognome, value);
            }
        }
        private const string RuleRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        private string fEmail;
        [VisibleInListView(true)]
        [RuleRegularExpression("RuleRegularExp.Destinatari.Email", DefaultContexts.Save, RuleRegularExpression,
        CustomMessageTemplate = "Attenzione inserire un corretto indirizzo mail(nome.cognome@dominio.com oppure nomecognome@dominio.xxx).")]
        [ToolTip("Inserire una email, con il seguente formato: nome.cognome@dominio.com oppure nomecognome@dominio.xxx", null)]
        [Size(100), Persistent("DESEMAIL"), DisplayName("Email")]
        [DbType("varchar(100)")]
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

        [Persistent("RISORSE"), DisplayName("Risorsa")]
        //[ImmediatePostData(true)]
        [Delayed(true)]
        public Risorse Risorse
        {
            get
            {
                return GetDelayedPropertyValue<Risorse>("Risorse");
            }
            set
            {
                SetDelayedPropertyValue<Risorse>("Risorse", value);
            }
            //get
            //{
            //    return fRisorse;
            //}
            //set
            //{
            //    SetPropertyValue<Risorse>("Risorse", ref fRisorse, value);
            //}
        }





        private string _PhoneString;
        private const string PhoneStringEditMask = "(0000)000-0000009";
        [VisibleInListView(true)]
        [ModelDefault("EditMaskType", "Simple")]
        [ModelDefault("DisplayFormat", "{0:" + PhoneStringEditMask + "}")]
        [ModelDefault("EditMask", PhoneStringEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("indicare il numero telefonico del mobile che riceve l'SMS, : " + PhoneStringEditMask, null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Size(20), Persistent("TELEFONO"), DisplayName("Telefono Mobile")]
        [DbType("varchar(20)")]
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

        [Association(@"Destinatari_DestinatariCN", typeof(DestinatariControlliNormativi)), Aggregated, DisplayName("Edifici/Impianti Associati")]
        [Appearance("Destinatari.DestinatariControlliNormativis", Criteria = "RoleAzione=='InserimentoModificaSchedaMP' Or RoleAzione=='TrasmissioneTicketAssistenza'", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public XPCollection<DestinatariControlliNormativi> DestinatariControlliNormativis
        {
            get
            {
                return GetCollection<DestinatariControlliNormativi>("DestinatariControlliNormativis");
            }
        }

        [Association(@"Destinatari_AzioniMail", typeof(AzioniSpedizioneMail)), Aggregated, DisplayName("Azioni Spedizione Associate")]
        // [Appearance("Destinatari.DestinatariControlliNormativis", Criteria = "RoleAzione=='InserimentoModificaSchedaMP' Or RoleAzione=='TrasmissioneTicketAssistenza'", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public XPCollection<AzioniSpedizioneMail> AzioniSpedizioneMails
        {
            get
            {
                return GetCollection<AzioniSpedizioneMail>("AzioniSpedizioneMails");
            }
        }
        //[MemberDesignTimeVisibility(false)]
        [System.ComponentModel.Browsable(false)]
        internal bool GetSMSvalidate
        {
            get
            {
                bool Check = true;
                var mio = AzioniSpedizioneMails.Criteria = "";
                int nrSMS = Session.QueryInTransaction<AzioniSpedizioneMail>()
                                          .Where(d => (d.Destinatari.Oid == this.Oid) &&
                                            
                                             (d.TipologiaSpedizione == Classi.TipologiaSpedizione.SMS || d.TipologiaSpedizione == Classi.TipologiaSpedizione.MAILSMS)
                                             //&& (d.Abilitato=FlgAbilitato.Si)
                                             )
                                           .Select(s => s.Oid).ToList().Count();

                if (nrSMS > 0 && string.IsNullOrEmpty(this.PhoneString))
                {
                    Check = false;
                }
                if (string.IsNullOrEmpty(this.PhoneString))
                {
                    Check = false;
                }
                return Check;
            }
        }

        [System.ComponentModel.Browsable(false)]
        internal bool GetMAILvalidate
        {
            get
            {
                bool Check = true;
                var mio = AzioniSpedizioneMails.Criteria = "";
                int nrSMS = Session.QueryInTransaction<AzioniSpedizioneMail>()
                                          .Where(d => d.Destinatari.Oid == this.Oid &&
                                             (d.TipologiaSpedizione == Classi.TipologiaSpedizione.MAIL || d.TipologiaSpedizione == Classi.TipologiaSpedizione.MAILSMS))
                                           .Select(s => s.Oid).ToList().Count();

                if (nrSMS > 0 && string.IsNullOrEmpty(this.Email))
                {
                    Check = false;
                }

                if (string.IsNullOrEmpty(this.Email))
                {
                    Check = false;
                }

                return Check;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1} ", this.Nome, this.Cognome);
        }

    }
}
