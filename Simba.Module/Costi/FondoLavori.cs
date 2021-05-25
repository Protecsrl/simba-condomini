using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAMS.Module.DBPlant;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
 

namespace CAMS.Module.Costi
{
    [DefaultClassOptions,
    Persistent("FONDOLAVORI")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Fondo Lavori")]
    [NavigationItem("Gestione Contabilità")]
    [Appearance("FondoLavori.RegistroLavori.Visible", TargetItems = "RegistroLavoris", Criteria = @"Oid = -1", Visibility = ViewItemVisibility.Hide)]
    public class FondoLavori : XPObject
    {
        public FondoLavori()
            : base()
        {
        }

        public FondoLavori(Session session)
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
        [RuleRequiredField("Fondolavori.Commesse", DefaultContexts.Save, "La commessa è un campo obbligatorio")]
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
      //  [PersistentAlias("Iif(TotaleFondo is null,0,TotaleFondo - [<RegistroCostiDettaglio>][^.Oid = RegistroCosti.FondoCosti.Oid].Sum(ImpManodopera) - [<RegistroCostiDettaglio>][^.Oid = RegistroCosti.FondoCosti.Oid].Sum(ImpMateriale))")]
        [PersistentAlias("Iif(TotaleFondo is null,0,TotaleFondo - [<RegistroLavoriConsuntivi>][^.Oid = RegistroLavori.FondoLavori.Oid].Sum(ImpManodopera) - [<RegistroLavoriConsuntivi>][^.Oid = RegistroLavori.FondoLavori.Oid].Sum(ImpMateriale))")]
        [Appearance("FondoLavori.RisiduodelFondo.BackColor.Red", BackColor = "Red", FontColor = "Black", Priority = 1, Criteria = "RisiduodelFondo <= 0")]
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



        [Association(@"RegistroLavori_FondoLavori", typeof(RegistroLavori)), DisplayName("Registro Lavori")]
        public XPCollection<RegistroLavori> RegistroLavoris
        {
            get
            {
                return GetCollection<RegistroLavori>("RegistroLavoris");
            }
        }
    }
}
