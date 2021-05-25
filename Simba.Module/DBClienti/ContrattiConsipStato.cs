using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
//namespace CAMS.Module.DBClienti
//{
//    class ContrattiConsipStato
//    {
//    }
//}

namespace CAMS.Module.DBClienti
{
    [DefaultClassOptions, Persistent("CONTRATTICONSIPSTATO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Stato contratto CONSIP")]
    [ImageName("PackageProduct")]
    [NavigationItem("Contratti")]
    public class ContrattiConsipStato : XPObject
    {
        public ContrattiConsipStato() : base() { }
        public ContrattiConsipStato(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private string fDescrizione;
        [Size(100), Persistent("DESCRIZIONE"), XafDisplayName("Descrizione")]
        [DbType("varchar(100)")]
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

        private int fTempiPrevisti;
        [Persistent("TEMPIPREVISTI"), XafDisplayName("Tempi Previsti")]
        public int TempiPrevisti
        {
            get
            {
                return fTempiPrevisti;
            }
            set
            {
                SetPropertyValue<int>("TempiPrevisti", ref fTempiPrevisti, value);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}", this.Descrizione);
        }

    }
}

