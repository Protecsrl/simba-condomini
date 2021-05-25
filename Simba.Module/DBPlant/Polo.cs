using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

using DevExpress.Persistent.Validation;
using CAMS.Module.DBTask;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions,Persistent("POLO")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Area")]
    [RuleCombinationOfPropertiesIsUnique("Unique.Polo", DefaultContexts.Save, "CodDescrizione, Descrizione")]
    [ImageName("BO_Category")]
    [NavigationItem("Polo")]
    public class Polo : XPObject
    {
        public Polo()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public Polo(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"),     Size(100),   DisplayName("Descrizione")]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.Polo.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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

        private string fCodDescrizione;
        [Persistent("COD_DESCRIZIONE"), Size(5)]
        [RuleRequiredField("RReqField.Polo.CodDescrizione", DefaultContexts.Save, "Il Cod Descrizione è un campo obbligatorio")]
        [DbType("varchar(5)")]
        public string CodDescrizione
        {
            get
            {
                return fCodDescrizione;
            }
            set
            {
                SetPropertyValue<string>("CodDescrizione", ref fCodDescrizione, value);
            }
        }

        [Association(@"Polo_AreaDiPolo", typeof(AreaDiPolo)),Aggregated, DisplayName("Area di Polo")]
        public XPCollection<AreaDiPolo> AreaDiPolos
        {
            get
            {
                return GetCollection<AreaDiPolo>("AreaDiPolos");
            }
        }

        public override string ToString()
        {
            return string.Format("{0}", this.Descrizione);
        }

    }

}