using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Xpo;

namespace CAMS.Module.DBTask.Guasti.Viste
{
    [DefaultClassOptions, Persistent("MV_PCRPROBCAUSERIMEDITREE")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Albero Guasti")]
    [NavigationItem("Ticket")]
    public class PCRProblemiCauseRimediTree : TaskTreeObject, ITreeNodeImageProvider
    {
        public PCRProblemiCauseRimediTree(Session session) : base(session) { }

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
                return PCRTreePadre;

            }
        }

        [Association("PCRPadreFiglioTree-PCRPadreFiglioTree"),
        DevExpress.Xpo.DisplayName("Figli"), Aggregated]
        public XPCollection<PCRProblemiCauseRimediTree> Figlis
        {
            get
            {
                return GetCollection<PCRProblemiCauseRimediTree>("Figlis");
            }
        }

        private PCRProblemiCauseRimediTree fPCRTreePadre;
        [Browsable(false)]
        [Persistent("PADRE"), DevExpress.Xpo.DisplayName("Padre")]
        [Association("PCRPadreFiglioTree-PCRPadreFiglioTree")]
        // [DataSourceCriteria("Impianto.Oid = '@This.Impianto.Oid' And Oid != '@This.Oid'")]
        public PCRProblemiCauseRimediTree PCRTreePadre
        {
            get
            {
                return fPCRTreePadre;
            }
            set
            {
                SetPropertyValue<PCRProblemiCauseRimediTree>("PCRTreePadre", ref fPCRTreePadre, value);
            }
        } 
        ////////////////////////        
        public override string Name { get; set; }
        //public override int OidOggetto { get; set; }
        //public override string Tipo { get; set; }
//  OID	NAME	PADRE	TIPO	ORDINE	OIDOGGETTO	OIDSISTEMATECNOLOGICO	OIDSTDAPPARATO	STRTIPO
// 	    SistemaTecnologico1	Edilizia Residenziale(3)		SistemaTecnologico	1-1	1	1	253	Sistema Tecnologico
// 	   SistemaTecnologico1	Edilizia Residenziale(3)		SistemaTecnologico	1-1	1	1	201	Sistema Tecnologico
        private string fTipo;
        [Persistent("TIPO"), DevExpress.Xpo.DisplayName("Tipo ")]
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

        private string fstrTipo;
        [Persistent("STRTIPO"), DevExpress.Xpo.DisplayName("Tipo")]
        public string strTipo
        {
            get
            {
                return fstrTipo;
            }
            set
            {
                SetPropertyValue<string>("strTipo", ref fstrTipo, value);
            }
        }
        //  OID	NAME	PADRE	TIPO	ORDINE	OIDOGGETTO	OIDSISTEMATECNOLOGICO	OIDSTDAPPARATO	STRTIPO
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
        //  OID	NAME	PADRE	TIPO	ORDINE	OIDOGGETTO	OIDSISTEMATECNOLOGICO	OIDSTDAPPARATO	STRTIPO
        private int fOidSistemaTecnologico;
        [Persistent("OIDSISTEMATECNOLOGICO"), DevExpress.Xpo.DisplayName("OidSistemaTecnologico"), Browsable(false)]
        public int OidSistemaTecnologico        
        {
            get
            {
                return fOidSistemaTecnologico;
            }
            set
            {
                SetPropertyValue<int>("OidSistemaTecnologico", ref fOidSistemaTecnologico, value);
            }
        }

        private int fOidStdApparato;
        [Persistent("OIDSTDAPPARATO"), DevExpress.Xpo.DisplayName("OidStdApparato"), Browsable(false)]
        public int OidStdApparato
        {
            get
            {
                return fOidStdApparato;
            }
            set
            {
                SetPropertyValue<int>("OidStdApparato", ref fOidStdApparato, value);
            }
        }


    }
}
