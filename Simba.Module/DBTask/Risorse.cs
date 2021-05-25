using CAMS.Module.Classi;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;


namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("RISORSE")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Risorse")]
    [DefaultProperty("FullName")]
    [ImageName("Risorse")]
    [RuleCombinationOfPropertiesIsUnique("UniqueRisorse", DefaultContexts.Save, @"Nome,Cognome",
    CustomMessageTemplate = "Attenzione:Nome e Cognome già esistente!!\n\r      Si consiglia di Verificare",
    SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Warning)]
    [NavigationItem("Ticket")]

    public class Risorse : XPObject
    {
        public Risorse()
            : base()
        {
        }
        public Risorse(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {
                this.Matricola = Guid.NewGuid().ToString().Substring(1, 8);
                this.Azienda = "Engie Servizi";
                this.Telefono = "0039";
                this.TipoQualifica = Classi.TipoQualifica.Operaio;
            }
        }

        private string fNome;
        [Size(50),
        Persistent("NOME"),
        XafDisplayName("Nome")]
        [RuleRequiredField("RReqField.Risorse.Nome", DefaultContexts.Save, "La Cognome è un campo obbligatorio")]
        [DbType("varchar(50)")]
        public string Nome
        {
            get { return fNome; }
            set { SetPropertyValue<string>("Nome", ref fNome, value); }
        }
        private string fCognome;
        [Size(60),
        Persistent("COGNOME"),
        XafDisplayName("Cognome")]
        [RuleRequiredField("RReqField.Risorse.Cognome", DefaultContexts.Save, "La Cognome è un campo obbligatorio")]
        [DbType("varchar(60)")]
        public string Cognome
        {
            get { return fCognome; }
            set { SetPropertyValue<string>("Cognome", ref fCognome, value); }
        }


        private string fMatricola;
        [Size(20), Persistent("MATRICOLA"), XafDisplayName("Matricola")]
        [DbType("varchar(20)")]
        [RuleUniqueValue("UniqRisorseMatricola", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction,
        CustomMessageTemplate = "La Matricola è un campo Univoco"), ToolTip("Matricola Aziendale")]
        [RuleRequiredField("RReqField.Risorse.Matricola", DefaultContexts.Save, "La Matricola è un campo obbligatorio")]
        [VisibleInListView(false)]
        [Delayed(true)]
        public string Matricola
        {
            get { return GetDelayedPropertyValue<string>("Matricola"); }
            set { SetDelayedPropertyValue<string>("Matricola", value); }
        }

        private string fAzienda;
        [Size(150),
        Persistent("AZIENDA"),
        XafDisplayName("Azienda")]
        [DbType("varchar(150)")]
        [ToolTip("Inserisci l'Azienda")]
        [RuleRequiredField("RReqField.Risorse.Azienda", DefaultContexts.Save, "L'Azienda è un campo obbligatorio")]
        [VisibleInListView(false)]
        [Delayed(true)]
        public string Azienda
        {
            get { return GetDelayedPropertyValue<string>("Azienda"); }
            set { SetDelayedPropertyValue<string>("Azienda", value); }
        }


        private TipoQualifica fTipoQualifica;
        [Persistent("TIPOQUALIFICA"), DevExpress.ExpressApp.DC.XafDisplayName("Qualifica")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        //[ImmediatePostData(true)]
        public TipoQualifica TipoQualifica
        {
            get { return fTipoQualifica; }
            set { SetPropertyValue<TipoQualifica>("TipoQualifica", ref fTipoQualifica, value); }
        }

        private Mansioni fMansione;
        [Persistent("MANSIONE"),
       XafDisplayName("Mansione")]
        [RuleRequiredField("RReqField.Risorse.Mansione", DefaultContexts.Save, "La Mansione è un campo obbligatorio")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Mansioni Mansione
        {
            get { return GetDelayedPropertyValue<Mansioni>("Mansione"); }
            set { SetDelayedPropertyValue<Mansioni>("Mansione", value); }

        }

        private CentroOperativo fCentroOperativo;
        [Persistent("CENTROOPERATIVO"),
        Association(@"CentroOperativo_Risorse"),
        XafDisplayName("Centro Operativo di Appartenenza")]
        [RuleRequiredField("RReqField.Risorse.CentroOperativo", DefaultContexts.Save, "Il Centro Operativo è un campo obbligatorio")]
        [ExplicitLoading]
        [Delayed(true)]
        public CentroOperativo CentroOperativo
        {
            get { return GetDelayedPropertyValue<CentroOperativo>("CentroOperativo"); }
            set { SetDelayedPropertyValue<CentroOperativo>("CentroOperativo", value); }
        }
        //                                                   346-3228369
        private const string PhoneMobStringEditMask = "(0000)000-0000009";
        private string fTelefono;
        [Size(20), Persistent("TELEFONO"), XafDisplayName("Telefono Mobile")] //346 3228369
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip(@"indicare il numero telefonico del mobile che riceve l'SMS, : " + PhoneMobStringEditMask, null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [DevExpress.ExpressApp.Model.ModelDefault("EditMaskType", "Simple")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + PhoneMobStringEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", PhoneMobStringEditMask)]
        [DbType("varchar(20)")]
        [Delayed(true)]
        public string Telefono
        {
            get { return GetDelayedPropertyValue<string>("Telefono"); }
            set { SetDelayedPropertyValue<string>("Telefono", value); }

        }

        private string fTelefonoFisso;
        [Size(100), Persistent("TELEFONOFISSO"), XafDisplayName("Telefono")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip(@"indicare il numero telefonico del mobile che riceve l'SMS, : " + PhoneMobStringEditMask, null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [DbType("varchar(100)")]
        [Delayed(true)]
        public string TelefonoFisso
        {
            get { return GetDelayedPropertyValue<string>("TelefonoFisso"); }
            set { SetDelayedPropertyValue<string>("TelefonoFisso", value); }
        }

        private string fEmail;
        [Size(250), Persistent("EMAIL"), XafDisplayName("eMail")]
        [DbType("varchar(250)")]
        [VisibleInListView(false)]
        [RuleRegularExpression("ClientSide.RuleRegularExpressionObject.Email_RuleRegularExpression", DefaultContexts.Save,
                              @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                              CustomMessageTemplate = "Formato email non corretto!")]
        [Delayed(true)]
        public string Email
        {
            get { return GetDelayedPropertyValue<string>("Email"); }
            set { SetDelayedPropertyValue<string>("Email", value); }
        }

        [Association(@"RISORSE_TEAMRISORSE", typeof(AssRisorseTeam)),
        XafDisplayName("Elenco Associazioni in Team")]
        public XPCollection<AssRisorseTeam> AssRisorseTeam
        {
            get
            {
                return GetCollection<AssRisorseTeam>("AssRisorseTeam");
            }
        }

        [PersistentAlias("AssRisorseTeam.Count")]
        [XafDisplayName("Risorsa Assegnata ")]
        public RisorsaAssegnataaTeam RisorsaAssegnataaTeam
        {
            get
            {
                var tempObject = EvaluateAlias("RisorsaAssegnataaTeam");
                if (tempObject != null)
                {
                    if (Convert.ToInt32(tempObject) > 0)
                        return RisorsaAssegnataaTeam.Assegnato;
                    else
                        return RisorsaAssegnataaTeam.NonAssegnato;
                }
                else
                    return RisorsaAssegnataaTeam.NonAssegnato;
            }
        }

        private SecuritySystemUser fSecurityUser;
        [Persistent("SECURITYUSERID"), XafDisplayName("Security User")]
        [RuleUniqueValue("SecuritySystemUser.Risorsa.Unico", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        [DataSourceProperty("ListaSecurityUserInseribili")]
        [ExplicitLoading()]
        [Delayed(true)]
        public SecuritySystemUser SecurityUser
        {
            get { return GetDelayedPropertyValue<SecuritySystemUser>("SecurityUser"); }
            set { SetDelayedPropertyValue<SecuritySystemUser>("SecurityUser", value); }
        }

        private string fSessionID;
        [Size(50),
        Persistent("SESSIONID"), XafDisplayName("SessioneAttiva")]
        [DbType("varchar(50)")]
        [Browsable(false)]
        public string SessionID
        {
            get { return fSessionID; }
            set { SetPropertyValue<string>("SessionID", ref fSessionID, value); }
        }

        [NonPersistent]
        private XPCollection<SecuritySystemUser> fListaSecurityUserInseribili;
        [MemberDesignTimeVisibility(false)]
        public XPCollection<SecuritySystemUser> ListaSecurityUserInseribili
        {
            get
            {
                if (this.Oid == -1) return null;
                if (this.Cognome != null)
                {
                    if (fListaSecurityUserInseribili == null)
                    {
                        try
                        {
                            GroupOperator criteriaOP2 = new GroupOperator(GroupOperatorType.And);
                            CriteriaOperator joinCriteria = new OperandProperty("^.Oid") == new OperandProperty("SecurityUser.Oid");
                            JoinOperand joinOperand = new JoinOperand("Risorse", joinCriteria, Aggregate.Count, new OperandProperty("SecurityUser.Oid"));
                            BinaryOperator criteria = new BinaryOperator(joinOperand, new OperandValue(0), BinaryOperatorType.Equal);
                            criteriaOP2.Operands.Add(criteria);
                            CriteriaOperator op = CriteriaOperator.Parse("Roles[IsAdministrative = false]");//this.SecurityUser.Roles.Any(a=>a.IsAdministrative = false)
                            criteriaOP2.Operands.Add(op);
                            fListaSecurityUserInseribili = new XPCollection<SecuritySystemUser>(Session);
                            fListaSecurityUserInseribili.Criteria = criteriaOP2;
                        }
                        catch
                        {
                            fListaSecurityUserInseribili = null;
                        }

                        //OnChanged("ListaSecurityUserInseribili");
                    }
                }
                return fListaSecurityUserInseribili;
            }
        }

        private double? fLatUltimaPosiz;
        [Size(50),
        Persistent("GEOLAT"),
        XafDisplayName("Georeferenziazione Ultima Posizione Latitudine")]
        [MemberDesignTimeVisibility(false)]
        public double? GeoLatUltimaPosiz
        {
            get { return fLatUltimaPosiz; }
            set { SetPropertyValue<double?>("GeoLatUltimaPosiz", ref fLatUltimaPosiz, value); }
        }

        private double? fLngUltimaPosiz;
        [Size(50),
        Persistent("GEOLNG"),
        XafDisplayName("Georeferenziazione Ultima Posizione Longitudine")]
        [MemberDesignTimeVisibility(false)]
        public double? GeoLngUltimaPosiz
        {
            get { return fLngUltimaPosiz; }
            set { SetPropertyValue<double?>("GeoLngUltimaPosiz", ref fLngUltimaPosiz, value); }
        }

        private string fIndirizzodaGeo;
        [Size(250),
        Persistent("GEOINDIRIZZO"),
        XafDisplayName("Indirizzo Vicino")]
        [DbType("varchar(250)")]
        [MemberDesignTimeVisibility(false)]
        [Delayed(true)]
        public string IndirizzodaGeo
        {
            get { return GetDelayedPropertyValue<string>("IndirizzodaGeo"); }
            set { SetDelayedPropertyValue<string>("IndirizzodaGeo", value); }
        }

        [Persistent("GEOULTIMADATAAGGIORNAMENTO"),
        XafDisplayName("Ultima Data Aggiornamento"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        [MemberDesignTimeVisibility(false)]
        [Delayed(true)]
        public DateTime UltimaDataAggiornamento
        {
            get { return GetDelayedPropertyValue<DateTime>("UltimaDataAggiornamento"); }
            set { SetDelayedPropertyValue<DateTime>("UltimaDataAggiornamento", value); }
        }


        [Association(@"vRegistroPosizioni_Risorsa", typeof(RegistroPosizioniDettVista)),
       XafDisplayName("Storico Posizioni")]
        public XPCollection<RegistroPosizioniDettVista> RegistroPosizioniDetVistas
        {
            get
            {
                return GetCollection<RegistroPosizioniDettVista>("RegistroPosizioniDetVistas");
            }
        }

        #region stato disponibilità della risorsa
        private Boolean fDisponibilita;
        [Persistent("DISPONIBILE"), XafDisplayName("Disponibilità")]
        [MemberDesignTimeVisibility(false)]
        public Boolean Disponibilita
        {
            get { return fDisponibilita; }
            set { SetPropertyValue<Boolean>("Disponibilita", ref fDisponibilita, value); }
        }

        private Boolean fReperibile;
        [Persistent("REPERIBILITA"),
        XafDisplayName("Reperibilità")]
        public Boolean Reperibile
        {
            get { return fReperibile; }
            set { SetPropertyValue<Boolean>("Reperibile", ref fReperibile, value); }
        }

        private TipoTurnista fTurnista;
        [Persistent("TURNISTA"),
        XafDisplayName("Turnista")]
        public TipoTurnista Turnista
        {
            get { return fTurnista; }
            set { SetPropertyValue<TipoTurnista>("Turnista", ref fTurnista, value); }
        }

        //RESPONSABILE RISORSE.
        private Risorse fResponsabileRisorsa;
        [Persistent("RESPRISORSA"), XafDisplayName("Responsabile Risorsa")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        public Risorse ResponsabileRisorsa
        {
            get { return fResponsabileRisorsa; }
            set { SetPropertyValue<Risorse>("ResponsabileRisorsa", ref fResponsabileRisorsa, value); }
        }

        #endregion

        private TipoOrario fTipoOrario;
        [Persistent("TIPOORARIO"), XafDisplayName("TipoOrario")]
        //[VisibleInListView(false)]
        public TipoOrario TipoOrario
        {
            get { return fTipoOrario; }
            set { SetPropertyValue<TipoOrario>("TipoOrario", ref fTipoOrario, value); }
        }

        [PersistentAlias("Nome + ' ' + Cognome")]
        public string FullName
        {
            get { return string.Format("{0} {1}", Nome, Cognome); }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}

//public override string ToString()
//{
//    return string.Format("{0} - {1}", Nome, Cognome);
//}

//private Boolean fDisponibile;
//[NonPersistent, XafDisplayName("Disponibile")]
//public Boolean Disponibile
//{
//    get
//    {
//        int cntDisponibile = Session.Query<StatoDisponibilita>().Where(w => w.Risorsa == this && w.Disponibilita == true).Count();
//        if (cntDisponibile > 0)
//            fDisponibile = true;
//        else
//            fDisponibile = false;
//        return fDisponibile;
//    }

//}

//private string _Url = string.Empty;
//[NonPersistent,
//DisplayName("Visualizza Ultima Posizione")]
//[Appearance("Risorsa.UltimaPosizione.Url", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "Url Is Null")]
//[VisibleInDetailView(true),
//VisibleInListView(false),
//VisibleInLookupListView(false)]
//[EditorAlias("HyperLinkPropertyEditor")]
//public string Url
//{
//    get
//    {
//        var tempLat = Evaluate("GeoLatUltimaPosiz");
//        var tempLon = Evaluate("GeoLngUltimaPosiz");
//        if (tempLat != null && tempLon != null)
//        {
//            return String.Format("http://www.google.com/maps/place/{0},{1}/@{0},{1},{2}",
//                tempLat.ToString().Replace(",", "."), tempLon.ToString().Replace(",", "."), "16");
//        }
//        return null;
//    }
//    set
//    {
//        SetPropertyValue("Url", ref _Url, value);
//    }
//}


// vecchio codice ricerca Utenti
//XPQuery<SecuritySystemUser> qUtenti = new XPQuery<SecuritySystemUser>(Session);
//XPQuery<Risorse> qRisorse = new XPQuery<Risorse>(Session);
//// Group Join customers with an aggregation on their Orders
//var list = from u in qUtenti
//           join o in qRisorse on u.Oid equals o.SecurityUser.Oid into oo
//           where oo.Count() == 0
//           select new { u };
////////
//string sID = "";
//var UserAdmin = Session.Query<SecuritySystemUser>().Where(w => w.IsActive == true).Distinct().ToList();

//for (var i = 0; i < UserAdmin.Count; i++)
//{
//    //var OidRisorseTeam = UserAssociati[i].ToString();
//    SecuritySystemUser Utente = ((DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)(UserAdmin[i]));
//    int IsAdmin = Utente.Roles.Where(w => w.IsAdministrative == true).Count();
//    if (IsAdmin > 0)
//    {
//        string oid = ((DevExpress.ExpressApp.Security.Strategy.SecuritySystemUserBase)(UserAdmin[i])).Oid.ToString();	//{a7692a96-1295-44f2-b905-2719e39b0895}	System.Guid
//        if (sID == "")
//            sID = string.Format("'{0}'", oid);
//        else
//            sID = string.Format("{0},'{1}'", sID, oid);
//    }
//}

/////////
//var UserAssociati = Session.Query<Risorse>().Where(w => w.SecurityUser != null && w.SecurityUser.IsActive == true).
//    Select(s => s.SecurityUser).Distinct().ToList();

//for (var i = 0; i < UserAssociati.Count; i++)
//{
//    //var OidRisorseTeam = UserAssociati[i].ToString();
//    SecuritySystemUser Utente = ((DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)(UserAssociati[i]));
//    int IsAdmin = Utente.Roles.Where(w => w.IsAdministrative == true).Count();
//    if (IsAdmin > 0) continue;
//    string oid = ((DevExpress.ExpressApp.Security.Strategy.SecuritySystemUserBase)(UserAssociati[i])).Oid.ToString();	//{a7692a96-1295-44f2-b905-2719e39b0895}	System.Guid
//    if (sID == "")
//        sID = string.Format("'{0}'", oid);
//    else
//        sID = string.Format("{0},'{1}'", sID, oid);
//}
//string ParCriteria = string.Format("Oid In ({0})", sID);    
//fListaSecurityUserInseribili.Criteria = CriteriaOperator.Parse(ParCriteria).Not();
//CriteriaOperator op = CriteriaOperator.Parse("[Risorse][[^.Oid ] = SecurityUser.Oid].Count() = 0");