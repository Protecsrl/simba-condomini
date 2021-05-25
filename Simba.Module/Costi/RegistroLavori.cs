using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
 
namespace CAMS.Module.Costi
{
    [DefaultClassOptions, Persistent("REGISTROLAVORI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Lavori")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [RuleCombinationOfPropertiesIsUnique("Unique.RegistroLavori", DefaultContexts.Save, "RegistroRdL;FondoLavori",
            CustomMessageTemplate = @"Attenzione! il Registro Costi deve essere univoco per RdL e Fondo Costi.",
    SkipNullOrEmptyValues = false)]
    
    [ImageName("BO_Invoice")]
    [NavigationItem("Gestione Contabilità")]
    public class RegistroLavori : XPObject
    {
        public RegistroLavori()
            : base()
        {
        }

        public RegistroLavori(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Size(200), Persistent("DESCRIZIONE")]
        [DbType("varchar(200)")]
        [RuleRequiredField("RuleReq.RegistroLavori.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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

        private Immobile fImmobile;
        [Association(@"RegistroLavori_Edificio"), Persistent("IMMOBILE"), DisplayName("Immobile")]
        [RuleRequiredField("RuleReq.RegistroLavori.Immobile", DefaultContexts.Save, "L'Immobile è un campo obbligatorio")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Immobile Immobile
        {
            get
            {
                return GetDelayedPropertyValue<Immobile>("Immobile");
                //return fEdificio;
            }
            set
            {
                SetDelayedPropertyValue<Immobile>("Immobile", value);
                //SetPropertyValue("Immobile", ref fEdificio, value);
            }
        }

        private Servizio fServizio;
        [Association(@"RegistroLavori_Servizio"), Persistent("SERVIZIO"), DisplayName("Impianto")]
        [Appearance("RegistroLavori.Abilita.Servizio", Criteria = "(Immobile  is null) or (not (RegistroRdL  is null))", Enabled = false)]
        [RuleRequiredField("RuleReq.RegistroLavori.Servizio", DefaultContexts.Save, "Servizio è un campo obbligatorio")]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Immobile = '@This.Immobile'")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Servizio Servizio
        {
            get
            {
                return GetDelayedPropertyValue<Servizio>("Impianto");
            }
            set
            {
                SetDelayedPropertyValue<Servizio>("Impianto", value);
            }

        }

        //        5	Richiesta Chiusa 
        //                           6	Sospesa SO 7	Annullato
        //8	Rendicontazione Operativa
        //9	Rendicontazione Economica

        private RegistroRdL fRegRdl;
        [Association(@"RegistroLavori_RegistroRdL"),
        Persistent("REGRDL"),
        DisplayName("Registro RdL")]
        [RuleRequiredField("RuleReq.RegistroLavori.RegistroRdL", DefaultContexts.Save, "Registro RdL è un campo obbligatorio")]
        [ToolTip("Registro RdL")]
        [Appearance("RegistroLavori.Abilita.RegistroRdL", Criteria = "(Impianto  is null)", Enabled = false)]
        [ImmediatePostData(true)]
        [DataSourceCriteria("[<RdL>][^.Oid == RegistroRdL.Oid And Impianto.Oid == '@This.Impianto.Oid' And UltimoStatoSmistamento.Oid In(4,5,8,9)] And RegistroLavoris.Count = 0 And RegistroLavoriAltreRegRdLs.Count = 0")]
        [ExplicitLoading()]
        [Delayed(true)]
        public RegistroRdL RegistroRdL
        {
            get
            {
                return GetDelayedPropertyValue<RegistroRdL>("RegistroRdL");
            }
            set
            {
                SetDelayedPropertyValue<RegistroRdL>("RegistroRdL", value);
            }
        }

        private TipologiaCosto fTipologiaCosto;
        [Persistent("TIPOCOSTO"), DisplayName("Tipologia")]
        [RuleRequiredField("RuleReq.RegistroLavori.TipologiaCosto", DefaultContexts.Save, "La Tipologia è un campo obbligatorio")]
        public TipologiaCosto TipologiaCosto
        {
            get
            {
                return fTipologiaCosto;
            }
            set
            {
                SetPropertyValue<TipologiaCosto>("TipologiaCosto", ref fTipologiaCosto, value);
            }
        }

        private FondoLavori fFondoLavori;
        [Association(@"RegistroLavori_FondoLavori"),
        Persistent("FONDOLAVORI"),
        DisplayName("Fondo Lavori")]
        [RuleRequiredField("RuleReq.RegistroLavori.FondoLavori", DefaultContexts.Save, "Il Fondo Lavori è un campo obbligatorio")]
        [ExplicitLoading()]
        public FondoLavori FondoLavori
        {
            get
            {
                return fFondoLavori;
            }
            set
            {
                SetPropertyValue<FondoLavori>("FondoLavori", ref fFondoLavori, value);
            }
        }
        #region fondo
        [DisplayName("Totale Fondo")]
        [PersistentAlias("Iif(FondoLavori is null,0,FondoLavori.TotaleFondo)")]
        [Appearance("RegistroLavori.TotaleFondo.BackColor.Red", BackColor = "Red", FontColor = "Black", Priority = 1, Criteria = "RisiduodelFondo = 0")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        public double TotaleFondo
        {
            get
            {
                var tempObject = EvaluateAlias("TotaleFondo");
                if (tempObject != null)
                    return Convert.ToDouble(tempObject);
                else
                    return 0;
            }
        }
        //  RegistroLavoriConsuntivis
        [DisplayName("Residuo del Fondo")]
      //  [PersistentAlias("Iif(FondoLavori is null,0,FondoLavori.TotaleFondo - [<RegistroCostiDettaglio>][^.FondoCosti = RegistroCosti.FondoCosti].Sum(ImpManodopera) - [<RegistroCostiDettaglio>][^.FondoCosti = RegistroCosti.FondoCosti].Sum(ImpMateriale))")]
        [PersistentAlias("Iif(FondoLavori is null,0,FondoLavori.TotaleFondo - [<RegistroLavoriConsuntivi>][^.FondoLavori = RegistroLavori.FondoLavori].Sum(ImpManodopera) - [<RegistroLavoriConsuntivi>][^.FondoLavori = RegistroLavori.FondoLavori].Sum(ImpMateriale))")]
        [Appearance("RisiduodelFondo.BackColor.Red", BackColor = "Red", FontColor = "Black", Priority = 1, Criteria = "RisiduodelFondo = 0")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        public double RisiduodelFondo
        {
            get
            {
                var tempObject = EvaluateAlias("RisiduodelFondo");
                if (tempObject != null)
                    return Convert.ToDouble(tempObject);
                else
                    return 0;
            }
        }
        #endregion
        #region consuntivo

        private double fTotManodopera;
        [PersistentAlias("Iif(RegistroLavoriConsuntivis.Count >0, RegistroLavoriConsuntivis[Abilitato == 1].Sum(ImpManodopera),0.0)")]
        [DisplayName("Totale Consuntivo Manodopera")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        [ImmediatePostData(true)]
        public double TotManodopera
        {
            get
            {
                object tempObject = EvaluateAlias("TotManodopera");
                if (tempObject != null)
                    return Convert.ToDouble(tempObject);
                else
                    return 0;
            }
        }

        private double fTotMateriale;
        [PersistentAlias("Iif(RegistroLavoriConsuntivis.Count >0, RegistroLavoriConsuntivis[Abilitato == 1].Sum(ImpMateriale),0.0)")]
        [DisplayName("Totale Consuntivo Materiale")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        [ImmediatePostData(true)]
        public double TotMateriale
        {
            get
            {
                object tempObject = EvaluateAlias("TotMateriale");
                if (tempObject != null)
                    return Convert.ToDouble(tempObject);
                else
                    return 0;
            }
        }

        private double fTotConsuntivo;
        [PersistentAlias("Iif(RegistroLavoriConsuntivis.Count >0, RegistroLavoriConsuntivis[Abilitato == 1].Sum(ImpMateriale+ImpManodopera),0.0)")]
        [DisplayName("Totale Consuntivo")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        [ImmediatePostData(true)]
        public double TotConsuntivo
        {
            get
            {
                object tempObject = EvaluateAlias("TotConsuntivo");
                if (tempObject != null)
                    return Convert.ToDouble(tempObject);
                else
                    return 0;
            }
        }


        [Association(@"RegistroLavoriConsuntivi.RegistroLavori", typeof(RegistroLavoriConsuntivi)), Aggregated]
        [DisplayName("Elenco Consuntivi")]
        public XPCollection<RegistroLavoriConsuntivi> RegistroLavoriConsuntivis
        {
            get
            {
                return GetCollection<RegistroLavoriConsuntivi>("RegistroLavoriConsuntivis");
            }
        }
        #endregion
        #region preventivo
        // [PersistentAlias("Edificis.Sum(Impianti.Count)")]NonPersistent
        private double fTotPreventivoManodopera;
        [PersistentAlias("Iif(RegistroLavoriPreventivis.Count >0, RegistroLavoriPreventivis[Abilitato == 1].Sum(ImpManodopera),0.0)")]
        [DisplayName("Totale Preventivo Manodopera")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        [ImmediatePostData(true)]
        public double TotPreventivoManodopera
        {
            get
            {
                object tempObject = EvaluateAlias("TotPreventivoManodopera");
                if (tempObject != null)
                {
                    return Convert.ToDouble(tempObject);
                    //return (double)tempObject;
                }
                else { return 0; }
            }

        }

        private double fTotPreventivoMateriale;
        [PersistentAlias("Iif(RegistroLavoriPreventivis.Count >0, RegistroLavoriPreventivis[Abilitato == 1].Sum(ImpMateriale),0)")]
        [DisplayName("Totale Preventivo Materiale")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        [ImmediatePostData(true)]
        public double TotPreventivoMateriale
        {
            get
            {
                object tempObject = EvaluateAlias("TotPreventivoMateriale");
                if (tempObject != null)
                {
                    return Convert.ToDouble(tempObject);
                }
                else { return 0; }
            }

        }

        //[PersistentAlias("Iif(RegistroCostiPreventivos.Count >0, RegistroCostiPreventivos.Sum(ImpMateriale+ImpManodopera) + RegistroCostiPreventivos.Sum(ImpManodopera),0)")]


        [PersistentAlias("Iif(RegistroLavoriPreventivis.Count >0, RegistroLavoriPreventivis[Abilitato == 1].Sum(ImpMateriale+ImpManodopera),0)")]
        [DisplayName("Totale Preventivo")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        [ImmediatePostData(true)]
        public double TotPreventivo
        {
            get
            {
                object tempObject = EvaluateAlias("TotPreventivo");
                if (tempObject != null)
                {
                    return Convert.ToDouble(tempObject);
                }
                else { return 0; }
            }

        }


        [Association(@"RegistroLavoriPreventivi.RegistroLavori", typeof(RegistroLavoriPreventivi)), Aggregated]
        [DisplayName("Elenco Preventivi")]
        public XPCollection<RegistroLavoriPreventivi> RegistroLavoriPreventivis
        {
            get
            {
                return GetCollection<RegistroLavoriPreventivi>("RegistroLavoriPreventivis");
            }
        }
        #endregion
        //       [Association(@"RegistroCostiRicaviDettaglio_RegistroCosti", typeof(RegistroCostiRicaviDettaglio)), Aggregated,
        //DisplayName("Dettaglio dei Ricavi")]
        //       public XPCollection<RegistroCostiRicaviDettaglio> RegistroCostiRicaviDettaglios
        //       {
        //           get
        //           {
        //               return GetCollection<RegistroCostiRicaviDettaglio>("RegistroCostiRicaviDettaglios");
        //           }
        //       }

        [Association(@"RegistroLavoriAltreRegRdL.RegistroLavori", typeof(RegistroLavoriAltreRegRdL)), Aggregated,
        DisplayName("Altre RdL Associate")]
        public XPCollection<RegistroLavoriAltreRegRdL> RegistroLavoriAltreRegRdLs
        {
            get
            {
                return GetCollection<RegistroLavoriAltreRegRdL>("RegistroLavoriAltreRegRdLs");
            }
        }


    }
}
