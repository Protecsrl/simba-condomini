//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace CAMS.Module.DBTask.Vista
//{
//    class RTeamEdificioImpAppTree
//    {
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.DBPlant;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBTask.Vista
{
    [DefaultClassOptions, Persistent("V_RICH_ED_IM_APP")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Richiedenti Tree")]
    [ImageName("Demo_ListEditors_TreeList_LargeData")]
    [NavigationItem("Patrimonio")]
    public class RichiedenteEdificioImpAppTree : TreeObject, ITreeNodeImageProvider
    {
        public RichiedenteEdificioImpAppTree(Session session) : base(session) { }

        public System.Drawing.Image GetImage(out string imageName)
        {
            imageName = "LoadPageSetup";
            switch (this.Tipo)
            {
                case "RisorseTeam":
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
                return TreePadre;

            }
        }

        [Association("RTeamEdificioImpAppPadreFiglioTree-RTeamEdificioImpAppPadreFiglioTree"),
        DevExpress.Xpo.DisplayName("Figli"), Aggregated]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<RichiedenteEdificioImpAppTree> Figlis
        {
            get
            {
                return GetCollection<RichiedenteEdificioImpAppTree>("Figlis");
            }
        }

        private RichiedenteEdificioImpAppTree fTreePadre;
        [Browsable(false)]
        [Persistent("PADRE"), DevExpress.Xpo.DisplayName("Padre")]
        [Association("RTeamEdificioImpAppPadreFiglioTree-RTeamEdificioImpAppPadreFiglioTree")]
        public RichiedenteEdificioImpAppTree TreePadre
        {
            get
            {
                return fTreePadre;
            }
            set
            {
                SetPropertyValue<RichiedenteEdificioImpAppTree>("TreePadre", ref fTreePadre, value);
            }
        }

        /// <summary>
        /// /////////////////////////////////////
        public override string Name { get; set; }

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
