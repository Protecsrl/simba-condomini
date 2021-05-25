using System;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.Costi
{
    [DefaultClassOptions,
    Persistent("TIPOCOSTO")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Tipo Costo")]
    [NavigationItem("Gestione Contabilità")]

    public class TipologiaCosto : XPObject
    {
        public TipologiaCosto()
            : base()
        {
        }

        public TipologiaCosto(Session session)
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


        //[Association(@"RegistroCosti_TipologiaCosto", typeof(RegistroCosti)),
        //DisplayName("Registro Costi")]
        //public XPCollection<RegistroCosti> RegistroCostis
        //{
        //    get
        //    {
        //        return GetCollection<RegistroCosti>("RegistroCostis");
        //    }
        //}
    }
}
