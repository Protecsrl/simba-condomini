using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using CAMS.Module.Classi;

namespace CAMS.Module.DBDocument
{
    [DefaultClassOptions,
    Persistent("DOCUMENTIGUIDA")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [FileAttachment("File")]
    [ImageName("Demo_String_InSpecialFormat_Properties")]
    [NavigationItem("Navigazione Documenti")]
    public class DocumentiGuida : XPObject
    {
        public DocumentiGuida()
            : base()
        {
        }
        public DocumentiGuida(Session session)
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

        [Persistent("FILE"),  DisplayName("File")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData File
        {
            get
            {
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("File");
            }
            set
            {
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("File", value);
            }
        }
        
        
    }
}

