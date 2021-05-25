using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Xpo;

namespace CAMS.Module.DBALibrary.Vista
{
    [System.ComponentModel.DisplayName("Albero Sistema Tecnologico")]
    [DefaultClassOptions, Persistent("V_SISTECNOLOGICO")]
    [NavigationItem("Procedure Attivita")]
    [ImageName("Demo_ListEditors_TreeList_LargeData")]
    [VisibleInDashboards(false)]
    public class SistemaTecnologicoTree : TreeObject, ITreeNodeImageProvider
    {
        public SistemaTecnologicoTree(Session session) : base(session) { }
        //public override void AfterConstruction() {    base.AfterConstruction();}

        public System.Drawing.Image GetImage(out string imageName)
        {
            imageName = "Action_Chart_Options";
            switch (this.Tipo)
            {
                case "SistemaTecnologico":
                    imageName = "Action_Chart_Options";
                    break;
                case "SistemaClassi":
                    imageName = "BO_Organization";
                    break;
                case "Sistema":
                    imageName = "Action_EditModel";
                    break;
                case "StdApparatoClassi":
                    imageName = "ManageItems";
                    break;
                case "StdApparato":
                    imageName = "ManageItems";
                    break;
                case "SchedaMp":
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
                return SisTecTreePadre;

            }
        }

        [Association("SisTecPadreFiglioTree-SisTecnPadreFiglioTree"),
        DevExpress.Xpo.DisplayName("Figli"), Aggregated]
        public XPCollection<SistemaTecnologicoTree> Figlis
        {
            get
            {
                return GetCollection<SistemaTecnologicoTree>("Figlis");
            }
        }

        private SistemaTecnologicoTree fSisTecTreePadre;
        [Browsable(false)]
        [Persistent("PADRE"), DevExpress.Xpo.DisplayName("Padre")]
        [Association("SisTecPadreFiglioTree-SisTecnPadreFiglioTree")]
        // [DataSourceCriteria("Impianto.Oid = '@This.Impianto.Oid' And Oid != '@This.Oid'")]
        public SistemaTecnologicoTree SisTecTreePadre
        {
            get
            {
                return fSisTecTreePadre;
            }
            set
            {
                SetPropertyValue<SistemaTecnologicoTree>("SisTecTreePadre", ref fSisTecTreePadre, value);
            }
        }

        /// <summary>
        /// /////////////////////////////////////        
        public override string Name { get; set; }
         //public override int OidOggetto { get; set; }
        //public override string Tipo { get; set; }
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
        [Persistent("FONTEMP"), DevExpress.Xpo.DisplayName("Owner"), Browsable(false)]
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
