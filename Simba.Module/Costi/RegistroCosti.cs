using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace CAMS.Module.Costi
{
    [DefaultClassOptions, Persistent("REGISTROCOSTI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Costo Lavori")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [RuleCombinationOfPropertiesIsUnique("Unique.RegistroCosti", DefaultContexts.Save, "RegistroRdL;FondoCosti",
            CustomMessageTemplate = @"Attenzione! il Registro Costi deve essere univoco per RdL e Fondo Costi.",
    SkipNullOrEmptyValues = false)]
    
    [ImageName("BO_Invoice")]
    [NavigationItem("Gestione Contabilità")]
    public class RegistroCosti : XPObject
    {
        public RegistroCosti()
            : base()
        {
        }

        public RegistroCosti(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        //kkk


        private string fDescrizione;
        [Size(200), Persistent("DESCRIZIONE")]
        [DbType("varchar(200)")]
        [RuleRequiredField("RuleReq.RegistroCosti.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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
        [Persistent("IMMOBILE"), DisplayName("Immobile")]
        [RuleRequiredField("RuleReq.RegistroCosti.Immobile", DefaultContexts.Save, "L'Immobile è un campo obbligatorio")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Immobile Immobile
        {
            get
            {
                return GetDelayedPropertyValue<Immobile>("Immobile");
            }
            set
            {
                SetDelayedPropertyValue<Immobile>("Immobile", value);
            }
        }

        private Servizio fServizio;
        [Persistent("SERVIZIO"), DisplayName("Servizio")]///Association(@"RegistroCosti_Impianto"),
        [Appearance("RegistroCosti.Abilita.Servizio", Criteria = "(Immobile  is null) or (not (RegistroRdL  is null))", Enabled = false)]
        [RuleRequiredField("RuleReq.RegistroCosti.Impianto", DefaultContexts.Save, "Servizio è un campo obbligatorio")]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Immobile = '@This.Immobile'")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Servizio Servizio
        {
            get
            {
                return GetDelayedPropertyValue<Servizio>("Servizio");
                // return fFile;
            }
            set
            {
                SetDelayedPropertyValue<Servizio>("Servizio", value);
                //   SetPropertyValue("File", ref fFile, value);
            }

        }

        //        5	Richiesta Chiusa 
        //                           6	Sospesa SO 7	Annullato
        //8	Rendicontazione Operativa
        //9	Rendicontazione Economica

        private RegistroRdL fRegRdl;
        [Persistent("REGRDL"), DisplayName("Registro RdL")]//Association(@"RegistroCosti_RegistroRdL"),
        //[RuleRequiredField("RuleReq.RegistroCosti.RegistroRdL", DefaultContexts.Save, "Registro RdL è un campo obbligatorio")]
        //[RuleUniqueValue("UniqRegRdl", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, CustomMessageTemplate = "Questo Campo deve essere Univoco")]
        [ToolTip("Registro RdL")]
        [Appearance("RegistroCosti.Abilita.RegistroRdL", Criteria = "(Impianto  is null)", Enabled = false)]
        [ImmediatePostData(true)]
        // [DataSourceCriteria("Immobile = '@This.Immobile'")]
        //   [DataSourceCriteria("Iif(Impianto is null, Apparato.Impianto.Immobile = '@This.Immobile' And UltimoStatoSmistamento.Oid In(4,5,8,9) And RegistroCostiAltreRegRdLs.Count = 0  And RegistroCostis.Count = 0, Apparato.Impianto.Immobile = '@This.Immobile' And Apparato.Impianto = '@This.Impianto')")]
        //[DataSourceCriteria("Apparato.Impianto = '@This.Impianto' And Apparato.Impianto.Immobile = '@This.Immobile' And RegistroCostiAltreRegRdLs.Count = 0  And RegistroCostis.Count = 0 And UltimoStatoSmistamento.Oid = 4")]
        //  [DataSourceCriteria("Apparato.Impianto = '@This.Impianto' And Apparato.Impianto.Immobile = '@This.Immobile'")]
        [DataSourceCriteria("[<RdL>][^.Oid == RegistroRdL.Oid And Impianto.Oid == '@This.Impianto.Oid' And UltimoStatoSmistamento.Oid In(4,5,8,9)]")]
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



        private FondoCosti fFondoCosti;
        [Association(@"RegistroCosti_FondoCosti"),
        Persistent("FONDOCOSTI"),
        DisplayName("Fondo di Costo")]
        [RuleRequiredField("RuleReq.RegistroCosti.FondoCosti", DefaultContexts.Save, "Il Fondo del Costo è un campo obbligatorio")]
        [ExplicitLoading()]
        public FondoCosti FondoCosti
        {
            get
            {
                return fFondoCosti;
            }
            set
            {
                SetPropertyValue<FondoCosti>("FondoCosti", ref fFondoCosti, value);
            }
        }
        #region fondo
        [DisplayName("Totale Fondo")]
        [PersistentAlias("Iif(FondoCosti is null,0,FondoCosti.TotaleFondo)")]
        [Appearance("TotaleFondo.BackColor.Red", BackColor = "Red", FontColor = "Black", Priority = 1, Criteria = "RisiduodelFondo = 0")]
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

        [DisplayName("Residuo del Fondo")]
        [PersistentAlias("Iif(FondoCosti is null,0,FondoCosti.TotaleFondo - [<RegistroCostiDettaglio>][^.FondoCosti = RegistroCosti.FondoCosti].Sum(ImpManodopera) - [<RegistroCostiDettaglio>][^.FondoCosti = RegistroCosti.FondoCosti].Sum(ImpMateriale))")]
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
        [PersistentAlias("Iif(RegistroCostiDettaglios.Count >0, RegistroCostiDettaglios[Abilitato == 1].Sum(ImpManodopera),0.0)")]
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
        [PersistentAlias("Iif(RegistroCostiDettaglios.Count >0, RegistroCostiDettaglios[Abilitato == 1].Sum(ImpMateriale),0.0)")]
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
        [PersistentAlias("Iif(RegistroCostiDettaglios.Count >0, RegistroCostiDettaglios[Abilitato == 1].Sum(ImpMateriale+ImpManodopera),0.0)")]
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
     

        [Association(@"RegistroCostiDettaglio_RegistroCosti", typeof(RegistroCostiDettaglio)), Aggregated]
        [DisplayName("Consuntivo")]
        public XPCollection<RegistroCostiDettaglio> RegistroCostiDettaglios
        {
            get
            {
                return GetCollection<RegistroCostiDettaglio>("RegistroCostiDettaglios");
            }
        }
        #endregion
        #region preventivo
        // [PersistentAlias("Edificis.Sum(Impianti.Count)")]NonPersistent
        private double fTotPreventivoManodopera;
        [PersistentAlias("Iif(RegistroCostiPreventivos.Count >0, RegistroCostiPreventivos[Abilitato == 1].Sum(ImpManodopera),0.0)")]
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
        [PersistentAlias("Iif(RegistroCostiPreventivos.Count >0, RegistroCostiPreventivos[Abilitato == 1].Sum(ImpMateriale),0)")]
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


        [PersistentAlias("Iif(RegistroCostiPreventivos.Count >0, RegistroCostiPreventivos[Abilitato == 1].Sum(ImpMateriale+ImpManodopera),0)")]
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
        

        [Association(@"RegistroCostiPreventivo_RegistroCosti", typeof(RegistroCostiPreventivo)), Aggregated]
        [DisplayName("Preventivo")]
        public XPCollection<RegistroCostiPreventivo> RegistroCostiPreventivos
        {
            get
            {
                return GetCollection<RegistroCostiPreventivo>("RegistroCostiPreventivos");
            }
        }
        #endregion
        private RegistroLavori fRegistroLavori;
        //Association(@"RegistroCosti.RegistroLavori"),
        [Persistent("REGISTROLAVORI"),   DisplayName("Registro Lavori")]
        [DataSourceCriteria("Iif(RegistroRdL is null, Impianto.Oid == '@This.Impianto.Oid', RegistroRdL.Oid == '@This.RegistroRdL.Oid')")]
        [ExplicitLoading()]
        public RegistroLavori RegistroLavori
        {
            get
            {
                return fRegistroLavori;
            }
            set
            {
                SetPropertyValue<RegistroLavori>("RegistroLavori", ref fRegistroLavori, value);
            }
        }

        [PersistentAlias("Iif(RegistroLavori is not null,  RegistroLavori.TotConsuntivo , 0)")]
        [DisplayName("Totale Registro Lavori")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        [ImmediatePostData(true)]
        public double TotRegistroLavori
        {
            get
            {
                object tempObject = EvaluateAlias("TotRegistroLavori");
                if (tempObject != null)
                {
                    return Convert.ToDouble(tempObject);
                }
                else { return 0; }
            }

        }

        [PersistentAlias("Iif(RegistroLavori is not null,  RegistroLavori.TotConsuntivo - TotConsuntivo , 0)")]
        [DisplayName("Totale Margine Operativo")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        [ImmediatePostData(true)]
        public double MargineOperativo
        {
            get
            {
                object tempObject = EvaluateAlias("MargineOperativo");
                if (tempObject != null)
                {
                    return Convert.ToDouble(tempObject);
                }
                else { return 0; }
            }

        }

    }
}
