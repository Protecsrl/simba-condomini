using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using CAMS.Module.DBPlant;
using DevExpress.Persistent.Validation;


namespace CAMS.Module.Costi
{
    [DefaultClassOptions,
    Persistent("FONDOCOSTI")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Fondo di Costo")]
    [NavigationItem("Gestione Contabilità")]
    [Appearance("Frequenze.RegistroCosti.Visible", TargetItems = "RegistroCostis", Criteria = @"Oid = -1", Visibility = ViewItemVisibility.Hide)]
    public class FondoCosti : XPObject
    {
        public FondoCosti()
            : base()
        {
        }

        public FondoCosti(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Size(1000),
        Persistent("DESCRIZIONE"),
        DisplayName("Descrizione")]
        [DbType("varchar(1000)")]
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


        private double fTotaleFondo;
        [Persistent("TOTFONDOECONOMICO"), DisplayName("Totale Fondo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:C}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "C")]
        public double TotaleFondo
        {
            get
            {
                return fTotaleFondo;
            }
            set
            {
                SetPropertyValue<double>("TotaleFondo", ref fTotaleFondo, value);
            }
        }


        private Contratti fCommesse;
        [Persistent("CONTRATTO"), DevExpress.ExpressApp.DC.XafDisplayName("Contratto")]
        [RuleRequiredField("Fondocosti.Commesse", DefaultContexts.Save, "La commessa è un campo obbligatorio")]
        //[Appearance("Immobile.Commesse", Criteria = "CodDescrizione != '" + NA + "'", Context = "DetailView")]

        [ExplicitLoading()]
        [Delayed(true)]
        public Contratti Commesse
        {
            get
            {
                return GetDelayedPropertyValue<Contratti>("Commesse");
            }
            set
            {
                SetDelayedPropertyValue<Contratti>("Commesse", value);
            }

        }


        [DisplayName("Residuo del Fondo")]
        [PersistentAlias("Iif(TotaleFondo is null,0,TotaleFondo - [<RegistroCostiDettaglio>][^.Oid = RegistroCosti.FondoCosti.Oid].Sum(ImpManodopera) - [<RegistroCostiDettaglio>][^.Oid = RegistroCosti.FondoCosti.Oid].Sum(ImpMateriale))")]
        [Appearance("FondoCosti.RisiduodelFondo.BackColor.Red", BackColor = "Red", FontColor = "Black", Priority = 1, Criteria = "RisiduodelFondo <= 0")]
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


        //[PersistentAlias("Iif(Oid = -1,Descrizione,Descrizione + ' (' + TotaleFondo + '/' + RisiduodelFondo + ')')"),
        //DisplayName("Nome")]
        //public string FullName
        //{
        //    get
        //    {

        //        var tempObject = EvaluateAlias("FullName");
        //        if (tempObject != null)
        //        {
        //            return (string)tempObject;
        //        }
        //        else
        //        {
        //            return string.Empty;
        //        }
        //    }
        //}


        [Association(@"RegistroCosti_FondoCosti", typeof(RegistroCosti)), DisplayName("Registro Costi")]
        public XPCollection<RegistroCosti> RegistroCostis
        {
            get
            {
                return GetCollection<RegistroCosti>("RegistroCostis");
            }
        }
    }
}
