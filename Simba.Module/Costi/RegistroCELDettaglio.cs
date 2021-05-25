using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

using CAMS.Module.Classi;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.DC;

 
namespace CAMS.Module.Costi
{
    [DefaultClassOptions, Persistent("REGISTROCELDET")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro CEL Dettaglio")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    //[RuleCombinationOfPropertiesIsUnique("Unique.RegistroLavori", DefaultContexts.Save, "RegistroRdL;FondoLavori",
    //        CustomMessageTemplate = @"Attenzione! il Registro Costi deve essere univoco per RdL e Fondo Costi.",
    //SkipNullOrEmptyValues = false)]
    
    [ImageName("BO_Invoice")]
    [NavigationItem("Gestione Contabilità")]
    public class RegistroCELDettaglio : XPObject
    {
        public RegistroCELDettaglio()
            : base()
        {
        }

        public RegistroCELDettaglio(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        //kkk


        private string fDescrizione;
        [Size(500), Persistent("DESCRIZIONE")]
        [DbType("varchar(500)")]
        [RuleRequiredField("RuleReq.RegistroCELDettaglio.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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

        [Association(@"RegistroCEL.Dettaglio")]
        [Persistent("REGISTROCEL"), DisplayName("Registro CEL")]
        [RuleRequiredField("RuleReq.RegistroCELDettaglio.RegistroCEL", DefaultContexts.Save, "L'Registro CEL è un campo obbligatorio")]
        [Delayed(true)]
        public RegistroCEL RegistroCEL
        {
            get
            {
                return GetDelayedPropertyValue<RegistroCEL>("RegistroCEL");
            }
            set
            {
                SetDelayedPropertyValue<RegistroCEL>("RegistroCEL", value);
            }
        }

        private CategoriaSOA fCategoriaSOA;
        [Persistent("CATEGORIASOA"), DisplayName("Categoria SOA")]
        [RuleRequiredField("RuleReq.RegistroCELDettaglio.StatoCEL", DefaultContexts.Save, "La Stato CEL è un campo obbligatorio")]
        public CategoriaSOA CategoriaSOA
        {
            get
            {
                return fCategoriaSOA;
            }
            set
            {
                SetPropertyValue<CategoriaSOA>("CategoriaSOA", ref fCategoriaSOA, value);
            }
        }


        private double fImporto ;
        [Persistent("IMPORTO"), DisplayName("Importo")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        public double Importo
        {
            get
            {
                return fImporto;
            }
            set
            {
                SetPropertyValue<double>("Importo", ref fImporto, value);
            }
        }
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";
        //private DateTime fDataDa;
        [Persistent("DATA_DA"), XafDisplayName("Data Inizio")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di inizio validità periodo", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Delayed(true)]
        public DateTime DataDa
        {
            get { return GetDelayedPropertyValue<DateTime>("DataDa"); }
            set { SetDelayedPropertyValue<DateTime>("DataDa", value); }
        }
        //private DateTime fDataDa;
        [Persistent("DATA_A"), XafDisplayName("Data Fine")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di fine validità periodo", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Delayed(true)]
        public DateTime DataA
        {
            get { return GetDelayedPropertyValue<DateTime>("DataA"); }
            set { SetDelayedPropertyValue<DateTime>("DataA", value); }
        }



    }
}
