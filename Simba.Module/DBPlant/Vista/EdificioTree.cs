using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBPlant.Vista
{  
    [DefaultClassOptions,   Persistent("V_EDIFICIO")]
    [VisibleInDashboards(false)]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Albero Edifici")]
    [ImageName("Demo_ListEditors_TreeList_LargeData")]
    [NavigationItem("Patrimonio")]
    public class EdificioTree : PlantTreeObject, ITreeNodeImageProvider
    {
        //public EdificioTree(Session session)   : base(session){}
        public EdificioTree(Session session) : base(session) { }


        public System.Drawing.Image GetImage(out string imageName)
        {
            imageName = "LoadPageSetup";
            switch (this.Tipo)
            {
                case "Commesse":
                    imageName = "TopCustomers";
                    break;
                case "Immobile":
                    imageName = "BO_Organization";
                    break;
                case "Impianto":
                    imageName = "Action_EditModel";
                    break;
                case "Apparato":
                    imageName = "ManageItems";
                    break;
                default:// altro non pervenuto

                    break;
            }

            return ImageLoader.Instance.GetImageInfo(imageName).Image;
        }

        protected override IBindingList Children
        {
            get
            {
                return Figlis;
            }
        }

        protected override ITreeNode Parent
        {
            get
            {
                return EdificiTreePadre;

            }
        }

        [Association("EdificiPadreFiglioTree-EdificiPadreFiglioTree"),
        DevExpress.Xpo.DisplayName("Figli"), Aggregated]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<EdificioTree> Figlis
        {
            get
            {
                return GetCollection<EdificioTree>("Figlis");
            }
        }

        private EdificioTree fEdificiTreePadre;
        [Browsable(false)]
        [Persistent("PADRE"), DevExpress.Xpo.DisplayName("Padre")]
        [Association("EdificiPadreFiglioTree-EdificiPadreFiglioTree")]
        // [DataSourceCriteria("Impianto.Oid = '@This.Impianto.Oid' And Oid != '@This.Oid'")]
        public EdificioTree EdificiTreePadre
        {
            get
            {
                return fEdificiTreePadre;
            }
            set
            {
                SetPropertyValue<EdificioTree>("EdificiTreePadre", ref fEdificiTreePadre, value);
            }
        }

        /// <summary>
        /// /////////////////////////////////////
        //[DevExpress.Xpo.DisplayName("Impianti")]
        public override string Name { get; set; }
        //[Browsable(false)]
        //public override int OidOggetto { get; set; }
        //[Browsable(false)]
        //public override string Tipo { get; set; }          , Browsable(false)
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

                private int fOidOggetto;
        [Persistent("OIDOGGETTO"), DevExpress.Xpo.DisplayName("OidOggetto"), Browsable(false)]
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
        //private string fOrdine;
        //[Persistent("ORDINE"), DevExpress.Xpo.DisplayName("Ordine"), Browsable(false)]
        //public string Ordine
        //{
        //    get
        //    {
        //        return fOrdine;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Ordine", ref fOrdine, value);
        //    }
        //}

 

        private int fOidCommessa;
        [Persistent("OIDCOMMESSA"), DevExpress.Xpo.DisplayName("Commessa"), Browsable(false)]
        public int OidCommessa
        {
            get
            {
                return fOidCommessa;
            }
            set
            {
                SetPropertyValue<int>("OidCommessa", ref fOidCommessa, value);
            }
        }

        private string fDettaglio;
        [NonPersistent, Size(1000), VisibleInListView(false)]
        public string Dettaglio
        {
            get
            {
                return fDettaglio;
            }
            set
            {
                SetPropertyValue<string>("Dettaglio", ref fDettaglio, value);
            }
        }
   }
}
