using System;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions, Persistent("TIPOLOGIAINTERVENTO"), System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Tipologia Intervento")]
    [NavigationItem("Procedure Attivita")]
    [ImageName("GroupFieldCollectionDett")]
    public class TipologiaIntervento : XPObject
    {
        public TipologiaIntervento()
            : base()
        {
        }

        public TipologiaIntervento(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        private string fDescrizione;
        [Size(200), Persistent("DESCRIZIONE")]
        [DbType("varchar(200)")]
        [RuleRequiredField("RuleReq.TipologiaIntervento.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
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