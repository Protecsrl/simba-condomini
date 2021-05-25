using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
namespace CAMS.Module.DBSpazi
{
    [DefaultClassOptions, Persistent("LOCALIFINITURETIPO")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Tipo Finituta Locale")]
    [ImageName("Action_EditModel")]
    [NavigationItem("Gestione Spazi")]

    public class TipoFinituraLocali : XPObject
    {
          
        public TipoFinituraLocali() : base() { }
        public TipoFinituraLocali(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(100), DevExpress.Xpo.DisplayName("Descrizione")]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.TipoFinituraLocali.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        private string fCodDescrizione;
        [Persistent("COD_DESCRIZIONE"), Size(25), DevExpress.Xpo.DisplayName("Cod Descrizione"), Appearance("TipoFinituraLocali.CodDescrizione", Enabled = true)]
        [DbType("varchar(25)")]
        public string CodDescrizione
        {
            get { return fCodDescrizione; }
            set { SetPropertyValue<string>("CodDescrizione", ref fCodDescrizione, value); }
        }

    }
}
