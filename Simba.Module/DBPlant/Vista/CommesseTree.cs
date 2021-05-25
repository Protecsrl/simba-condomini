using System;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;
using System.Diagnostics;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBPlant.Vista
{
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Albero Commesse")]
    [DefaultClassOptions, Persistent("MV_COMMESSE")]
    [VisibleInDashboards(false)]
    [ImageName("Demo_ListEditors_TreeList_LargeData")]
    [NavigationItem("Polo")]
    [ModelDefault("AllowEdit", "False")]
    [ModelDefault("AllowClear", "False")]
    //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
    public class CommesseTree : TreeObject, ITreeNodeImageProvider
    {
        public CommesseTree(Session session) : base(session) { }

        public System.Drawing.Image GetImage(out string imageName)
        {
            imageName = "LoadPageSetup";
            switch (this.Tipo)
            {
                case "Commesse":
                    imageName = "BO_Contract";
                    break;
                case "Immobile":
                    imageName = "BO_Organization";
                    break;
                case "Impianto":
                    imageName = "Action_EditModel";
                    break;
                case "Apparato":
                    imageName = "LoadPageSetup";
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
                return CommesseTreePadre;

            }
        }

        [Association("CommessePadreFiglioTree-CommessePadreFiglioTree"),
        DevExpress.Xpo.DisplayName("Figli"), Aggregated]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<CommesseTree> Figlis
        {
            get
            {
                return GetCollection<CommesseTree>("Figlis");
            }
        }

        private CommesseTree fCommesseTreePadre;
        [Browsable(false)]
        [Persistent("PADRE"), DevExpress.Xpo.DisplayName("Padre")]
        [Association("CommessePadreFiglioTree-CommessePadreFiglioTree")]
        // [DataSourceCriteria("Impianto.Oid = '@This.Impianto.Oid' And Oid != '@This.Oid'")]
        public CommesseTree CommesseTreePadre
        {
            get
            {
                return fCommesseTreePadre;
            }
            set
            {
                SetPropertyValue<CommesseTree>("CommesseTreePadre", ref fCommesseTreePadre, value);
            }
        }

        /// <summary>
        /// /////////////////////////////////////        
        public override string Name { get; set; }
        //  [Browsable(false)]
        //  public override int OidOggetto { get; set; }
        //  [Browsable(false)]
        //  public override string Tipo { get; set; }       

        //[Persistent("TIPO")]
        //public abstract string Tipo { get; set; }

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

        //[Persistent("OIDOGGETTO")]
        //       public  int OidOggetto { get; set; }
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


    }
}
