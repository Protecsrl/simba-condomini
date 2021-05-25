using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
    Persistent("TIPOINTERVENTO")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Tipo Intervento")]
    [ImageName("Italic")]
    [NavigationItem("Tabelle Anagrafiche")]

    public class TipoIntervento : XPObject
    {
        public TipoIntervento()
            : base()
        {
        }
        public TipoIntervento(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private string fDescrizione;
        [Size(50), Persistent("DESCRIZIONE"), DisplayName("Descrizione")]
        [DbType("varchar(50)")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        private int fValore;
        [Persistent("VALORE"), DisplayName("Valore")]
        [Delayed(true)]
        public int Valore
        {
            get { return GetDelayedPropertyValue<int>("Valore"); }
            set { SetDelayedPropertyValue<int>("Valore", value); }
        }

        private int fVal;
        [Persistent("VAL"), DisplayName("Valore Numerico")]
        [Delayed(true)]
        public int Val
        {
            get { return GetDelayedPropertyValue<int>("Val"); }
            set { SetDelayedPropertyValue<int>("Val", value); }
        }

        private string htmlStringProperty;
        [Size(SizeAttribute.Unlimited)]
        [VisibleInListView(true)]
        [Persistent("NOTE"), DisplayName("Note")]
        public string HtmlStringProperty
        {
            get { return htmlStringProperty; }
            set { SetPropertyValue("HtmlStringProperty", ref htmlStringProperty, value); }
        }

        private byte fIcona;
        [Persistent("ICONA"), DisplayName("Icona")]
        [Delayed(true)]
        public byte Icona
        {
            get { return GetDelayedPropertyValue<byte>("Icona"); }
            set { SetDelayedPropertyValue<byte>("Icona", value); }
        }


    }
}
