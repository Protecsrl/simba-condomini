using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
//namespace CAMS.Module.DBAux
//{
//    class Cittadini
//    {
//    }
//}

namespace CAMS.Module.DBAux
{
    [DefaultClassOptions]
    [Persistent("CITTADINI")]
    [System.ComponentModel.DefaultProperty("NomeUtente")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Cittadini")]
    [ImageName("BO_Employee")]
    [NavigationItem(true)]
    [VisibleInDashboards(false)]
    public class Cittadini : XPObject
    {

        public Cittadini()
            : base()
        {
        }
        public Cittadini(Session session)
            : base(session)
        {
        }

        private string fNomeUtente;
        [Size(100), Persistent("NOMEUTENTE"), DisplayName("Nome Utente")]
        [DbType("varchar(100)")]
        public string NomeUtente
        {
            get
            {
                return fNomeUtente;
            }
            set
            {
                SetPropertyValue<string>("NomeUtente", ref fNomeUtente, value);
            }
        }

        private string fMail;
        [Size(80), Persistent("EMAIL"), DisplayName("mail")] //346 3228369
        [RuleRegularExpression("cittadino_Email_RuleRegExpression", DefaultContexts.Save, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
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

        private string _Mobile;
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
        public string Mobile
        {
            get
            {
                return _Mobile;
            }
            set
            {
                SetPropertyValue("Mobile", ref _Mobile, value);
            }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
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

        private SecuritySystemUser fSecurityUser;
        [Persistent("SECURITYUSERID"), DevExpress.ExpressApp.DC.XafDisplayName("Security User")]
        [RuleUniqueValue("SecuritySystemUser.Cittadini.Unico", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        [DataSourceProperty("ListaSecurityUserInseribili")]
        [Delayed(true)]
        public SecuritySystemUser SecurityUser
        {
            get { return GetDelayedPropertyValue<SecuritySystemUser>("SecurityUser"); }
            set { SetDelayedPropertyValue<SecuritySystemUser>("SecurityUser", value); }
        }

        [NonPersistent]
        private XPCollection<SecuritySystemUser> fListaSecurityUserInseribili;
        [MemberDesignTimeVisibility(false)]
        public XPCollection<SecuritySystemUser> ListaSecurityUserInseribili
        {
            get
            {
                //if (this.Oid == -1) return null;
                if (this.NomeUtente != null)
                {
                    if (fListaSecurityUserInseribili == null)
                    {
                        try
                        {
                            XPQuery<Cittadini> qCittadini = new XPQuery<Cittadini>(Session);
                            XPQuery<SecuritySystemUser> qSecuritySystemUser = new XPQuery<SecuritySystemUser>(Session);

                            IEnumerable<SecuritySystemUser> query =
                                     ((from su in qSecuritySystemUser
                                       where su.IsActive == true
                                       select su
                                      )
                                     .Except
                                     (
                                          from ci in qCittadini
                                          where ci.SecurityUser != null
                                          select ci.SecurityUser
                                   ).Distinct()
                                   ).ToList();

                            fListaSecurityUserInseribili = new XPCollection<SecuritySystemUser>(Session);
                            fListaSecurityUserInseribili.AddRange(query);                            
                        }
                        catch
                        {
                            fListaSecurityUserInseribili = null;
                        }

                        OnChanged("ListaSecurityUserInseribili");
                    }
                }
                return fListaSecurityUserInseribili;
            }
        }



        private string fSessionID;
        [Size(50),
        Persistent("SESSIONID"), DevExpress.ExpressApp.DC.XafDisplayName("SessioneAttiva")]
        [DbType("varchar(50)")]
        //[Browsable(false)]
        public string SessionID
        {
            get
            { return fSessionID; }
            set
            { SetPropertyValue<string>("SessionID", ref fSessionID, value); }
        }


        [Association(@"Cittadini-CittadiniEdificis", typeof(CittadiniEdifici)), DevExpress.Xpo.Aggregated]
        [DevExpress.Xpo.DisplayName("dettagli")]
        public XPCollection<CittadiniEdifici> CittadiniEdificis
        {
            get
            {
                return GetCollection<CittadiniEdifici>("CittadiniEdificis");
            }
        }
    }
}


