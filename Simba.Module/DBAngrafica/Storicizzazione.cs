using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

using DevExpress.Persistent.Validation;
using CAMS.Module.DBTask;

namespace CAMS.Module.DBAngrafica
{
    [DefaultClassOptions, Persistent("STORICIZZAZIONE")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Storicizzazione")]
   
    [NavigationItem("Amministrazione")]
    public class Storicizzazione : XPObject
    {
        public Storicizzazione()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public Storicizzazione(Session session)
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
        [RuleRequiredField("RReqField.Storicizzazione.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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

        private StoricoValori fStoricoValori;
        [Persistent("STORICOVALORI"),
        DevExpress.Xpo.DisplayName("Storico Valori")]       
        [ImmediatePostData(true)]        
        [RuleRequiredField("RReqField.Storicizzazione.StoricoValori", DefaultContexts.Save, "è un campo obbligatorio")]
        [ExplicitLoading]
        [Delayed(true)]
        public StoricoValori StoricoValori
        {
            get
            {
                return GetDelayedPropertyValue<StoricoValori>("StoricoValori");
            }
            set
            {
                SetDelayedPropertyValue<StoricoValori>("StoricoValori", value);
            }
          
        }


        private DateTime? fData;
        [Persistent("DATA"), DisplayName("Data")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        public DateTime? Data
        {
            get
            {
                return fData;
            }
            set
            {
                SetPropertyValue<DateTime?>("Data", ref fData, value);
            }
        }

        

       
    }

}