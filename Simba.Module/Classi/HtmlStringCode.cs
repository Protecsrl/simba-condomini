using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
namespace CAMS.Module.Classi
{
    [DefaultClassOptions]
    [Persistent("HTMLSTRINGCODE")]
    //[NavigationItem(false)]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "HtmlStringCode")]
    public class HtmlStringCode : XPObject
    {
        public HtmlStringCode(Session session)
            : base(session)
        {
        }
        //public override void AfterConstruction()
        //{
        //    base.AfterConstruction();
        //    // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        //    if (this.Oid == -1)
        //    {
        //        this.DATA = DateTime.Now;
        //    }
        //}

        private TipoOggettoHTML _TipoOggetto;
        [XafDisplayName("data o ora"), ToolTip("data o ora")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        [Persistent("TIPOOGGETTO"), RuleRequiredField(DefaultContexts.Save)]
        public TipoOggettoHTML TipoOggetto
        {
            get { return _TipoOggetto; }
            set { SetPropertyValue("TipoOggetto", ref _TipoOggetto, value); }
        }

        private string _Browser;
        [XafDisplayName("CAMPO TEST"), ToolTip("browser")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        [Persistent("CHIAVE"), RuleRequiredField(DefaultContexts.Save)]
        [DbType("varchar(250)")]
        public string Browser
        {
            get { return _Browser; }
            set { SetPropertyValue("Browser", ref _Browser, value); }
        }

        private string _TipoChiave;
        [XafDisplayName("TIPO CHIAVE TEST"), ToolTip("versione browser")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        [Persistent("TIPOCHIAVE"), RuleRequiredField(DefaultContexts.Save)]
        [DbType("varchar(250)")]
        public string TipoChiave
        {
            get { return _TipoChiave; }
            set { SetPropertyValue("TipoChiave", ref _TipoChiave, value); }
        }

        private string _HtmlCode;
        [XafDisplayName("CAMPO TEST"), ToolTip("codice html della data")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        [Persistent("HTMLCODE"), RuleRequiredField(DefaultContexts.Save)]
        [DbType("varchar(4000)")]
        public string HtmlCode
        {
            get { return _HtmlCode; }
            set { SetPropertyValue("HtmlCode", ref _HtmlCode, value); }
        }

        private string _Format_Data;
        [XafDisplayName("Format Data"), ToolTip("input di Format Data")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        [Persistent("FORMATDATA"), RuleRequiredField(DefaultContexts.Save)]
        [DbType("varchar(4000)")]
        public string Format_Data
        {
            get { return _Format_Data; }
            set { SetPropertyValue("Format_Data", ref _Format_Data, value); }
        }

        //private DateTime _DATA;
        //[XafDisplayName("DATA"), ToolTip("My hint message")]
        ////[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)], RuleRequiredField(DefaultContexts.Save)
        //[Persistent("DATA")]
        //public DateTime DATA
        //{
        //    get { return _DATA; }
        //    set { SetPropertyValue("DATA", ref _DATA, value); }
        //}
        
    }
}

