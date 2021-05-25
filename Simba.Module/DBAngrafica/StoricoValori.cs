
using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using CAMS.Module.DBTask;

namespace CAMS.Module.DBAngrafica
{
    [DefaultClassOptions, Persistent("STORICOVALORI")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "StoricoValori")]
   //[RuleCombinationOfPropertiesIsUnique("Unique.StoricoValori", DefaultContexts.Save, "CodDescrizione, Descrizione")]
    [ImageName("BO_Category")]
    [NavigationItem("Amministrazione")]
    public class StoricoValori : XPObject
    {
        public StoricoValori()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public StoricoValori(Session session)
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
        [Persistent("DESCRIZIONE"), Size(100), DisplayName("Descrizione")]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.StoricoValori.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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

        
    }

}