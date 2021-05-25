using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;
using System.Diagnostics;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBPlant.Vista
{      
      [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Albero Risorse")]
    [DefaultClassOptions, Persistent("MV_CO_RISORSE")]
    [VisibleInDashboards(false)]
    [NavigationItem("Polo")]
    [ImageName("Demo_ListEditors_TreeList_LargeData")]
    public class CORisorseTree : TreeObject, ITreeNodeImageProvider
    {
        public CORisorseTree(Session session) : base(session) { }
        
        public System.Drawing.Image GetImage(out string imageName)
        {
            imageName = "LoadPageSetup";
            switch (this.Tipo)
            {
                case "Polo":
                    imageName = "BO_Category";
                    break;
                case "AreaDiPolo":
                    imageName = "SnapToCells";
                    break;
                case "CentroOperativo":
                    imageName = "Home";
                    break;
                case "Risorse":
                    imageName = "Risorse";
                    break;
                case "Skill":
                    imageName = "mansioni";
                    break;
                default:// altro non pervenuto  0K$8pPlCs

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
                return CORisorseTreePadre;

            }
        }

        [Association("CORisorseTreePadreFiglioTree-CORisorseTreePadreFiglioTree"),
        DevExpress.Xpo.DisplayName("Figli"), Aggregated]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<CORisorseTree> Figlis
        {
            get
            {
                return GetCollection<CORisorseTree>("Figlis");
            }
        }

        private CORisorseTree fCORisorseTreePadre;
        [Browsable(false)]
        [Persistent("PADRE"), DevExpress.Xpo.DisplayName("Padre")]
        [Association("CORisorseTreePadreFiglioTree-CORisorseTreePadreFiglioTree")]
        public CORisorseTree CORisorseTreePadre
        {
            get
            {
                return fCORisorseTreePadre;
            }
            set
            {
                SetPropertyValue<CORisorseTree>("CORisorseTreePadre", ref fCORisorseTreePadre, value);
            }
        }

        /// <summary>
        /// /////////////////////////////////////        
        public override string Name { get; set; }
      

        private string fTipo;
        [Persistent("TIPO"), DevExpress.Xpo.DisplayName("Tipo")]
          [VisibleInListView(true),VisibleInDetailView(false)]
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

        private string fOrdine;
        [Persistent("ORDINE"), DevExpress.Xpo.DisplayName("Ordine"), Browsable(false)]
        public string Ordine
        {
            get
            {
                return fOrdine;
            }
            set
            {
                SetPropertyValue<string>("Ordine", ref fOrdine, value);
            }
        }

        private int fOwner;
        [Persistent("OWNER"), DevExpress.Xpo.DisplayName("Owner"), Browsable(false)]
        public int Owner
        {
            get
            {
                return fOwner;
            }
            set
            {
                SetPropertyValue<int>("Owner", ref fOwner, value);
            }
        }

        private string fStrAreaDiPolo;  //StrAreaDiPolo
        [Persistent("AREADIPOLO"), DevExpress.Xpo.DisplayName("AreaDiPolo")]
        public string StrAreaDiPolo
        {
            get
            {
                return fStrAreaDiPolo;
            }
            set
            {
                SetPropertyValue<string>("StrAreaDiPolo", ref fStrAreaDiPolo, value);
            }
        }



    }
}
