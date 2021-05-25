using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Utils;

namespace CAMS.Module.DBHelp
{
    [DefaultClassOptions, Persistent("HELPCONFIGURATION")]
    [System.ComponentModel.DefaultProperty("Descrizione")]

    public class HelpConfiguration : XPObject
    {
        public HelpConfiguration()
            : base()
        {
        }
        public HelpConfiguration(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        [ModelDefault("PropertyEditorType", "ViewsPropertyEditor")]
        [Size(200),
        Persistent("VISTE"),
        DisplayName("Viste")]
        [DbType("varchar(200)")]
        public String Viste
        {
            get { return GetPropertyValue<String>("Viste"); }
            set { SetPropertyValue("Viste", value); }
        }

        private string fDescrizione;
        [Size(200),
        Persistent("DESCRIZIONE"),
        DisplayName("Descrizione")]
        [DbType("varchar(200)")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        [Persistent("FILE"), DisplayName("File")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [ExplicitLoading()]
        [Delayed(true)]
        public FileData File
        {
            get { return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("File"); }
            set { SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("File", value); }
        }

        [DevExpress.Xpo.ValueConverter(typeof(TypeToStringConverter))]
        public Type ObjectType
        {
            get { return GetPropertyValue<Type>("ObjectType"); }
            set { SetPropertyValue<Type>("ObjectType", value); }
        }

        [Size(-1)]
        public string Text
        {
            get { return GetPropertyValue<string>("Text"); }
            set { SetPropertyValue<string>("Text", value); }
        }


    }
}
