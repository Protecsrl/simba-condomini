using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBControlliNormativi
{
    [DefaultClassOptions,  Persistent("TIPOCONTROLLINORMATIVI")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [NavigationItem(false)]

    public class TipoControlloNormativo : XPObject
    {
        public TipoControlloNormativo()
            : base()
        {
        }
        public TipoControlloNormativo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Size(200),
        Persistent("DESCRIZIONE")]
        [DbType("varchar(200)")]
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

        [Association(@"ControlliNormativi_TipoControlloNormativo", typeof(ControlliNormativi)),  DisplayName("Avvisi Periodici")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<ControlliNormativi> ControlliNormativis
        {
            get
            {
                return GetCollection<ControlliNormativi>("ControlliNormativis");
            }
        }
    }
}
