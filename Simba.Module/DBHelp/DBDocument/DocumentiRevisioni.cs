using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace CAMS.Module.DBDocument
{
    [DefaultClassOptions, Persistent("DOCUMENTIREV")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Revisione Documenti")]
    //[ImageName("Action_EditModel")]
    //[NavigationItem("Gestione Spazi")]
    [ImageName("Demo_String_InSpecialFormat_Properties")]
    [NavigationItem("Navigazione Documenti")]
    public class DocumentiRevisioni : XPObject
    {
        public DocumentiRevisioni()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public DocumentiRevisioni(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(50), DevExpress.Xpo.DisplayName("Descrizione")]
        [DbType("varchar(250)")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        private int fOidOggetto;
        [Persistent("OIDOGGETTO"), DevExpress.Xpo.DisplayName("OidOggetto"), System.ComponentModel.Browsable(false)]
        public int OidOggetto
        {
            get
            {
                return fOidOggetto;
            }
            set
            {
                SetPropertyValue<int>("OidOggetto", ref fOidOggetto, value);
            }
        }

        private string fTipo;
        [Persistent("TIPO"), DevExpress.Xpo.DisplayName("Tipo")]
        public string Tipo
        {
            get
            {
                return fTipo;
            }
            set
            {
                SetPropertyValue<string>("Tipo", ref fTipo, value);
            }
        }

        private string fDwgPathName;
        [Persistent("DWGNAMEPATH"), Size(50), DevExpress.Xpo.DisplayName("Dwg Name ")]
        [DbType("varchar(50)")]
        [VisibleInListView(false)]
        public string DwgPathName
        {
            get { return fDwgPathName; }
            set { SetPropertyValue<string>("DwgPathName", ref fDwgPathName, value); }
        }


    }

}