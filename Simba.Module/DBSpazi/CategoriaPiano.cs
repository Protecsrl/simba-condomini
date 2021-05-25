


using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;


namespace CAMS.Module.DBSpazi
{   
    [DefaultClassOptions, Persistent("CATEGORIAPIANO")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Categoria Piano")]
    [ImageName("Action_EditModel")]
    [NavigationItem("Gestione Spazi")]
  public  class CategoriaPiano : XPObject
    {
        public CategoriaPiano() : base() { }
        public CategoriaPiano(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(100), DevExpress.Xpo.DisplayName("Descrizione")]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.CategoriaPiano.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        private string fCodDescrizione;
        [Persistent("COD_DESCRIZIONE"), Size(7), DevExpress.Xpo.DisplayName("Cod Descrizione")]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.CategoriaPiano.CodDescrizione", DefaultContexts.Save, "Codice è un campo obbligatorio")]
        public string CodDescrizione
        {
            get { return fCodDescrizione; }
            set { SetPropertyValue<string>("CodDescrizione", ref fCodDescrizione, value); }
        }

        private string fCodOut;
        [Persistent("COD_OUT"), Size(50), DevExpress.Xpo.DisplayName("Cod Out")]
        [DbType("varchar(50)")]
        public string CodOut
        {
            get { return fCodOut; }
            set { SetPropertyValue<string>("CodOut", ref fCodOut, value); }
        }

    }
}

 