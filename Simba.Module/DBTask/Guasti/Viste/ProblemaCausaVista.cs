using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using CAMS.Module.DBPlant;
using System.ComponentModel;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;

namespace CAMS.Module.DBTask.Guasti.Viste
{
    [DefaultClassOptions,    Persistent("V_PROBLEMA_CAUSA")]
    [NavigationItem(false)]
    [ImageName("GroupFieldCollection")]
    public class ProblemaCausaVista : TreeObject, ITreeNodeImageProvider
    {
        public ProblemaCausaVista(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public System.Drawing.Image GetImage(out string imageName)
        {
            imageName = "GroupFieldCollection";
            return ImageLoader.Instance.GetImageInfo(imageName).Image;
        }

        //public override int OidOggetto
        //{
        //    get;
        //    set;

        //}
        //public override string Tipo { get; set; }
        private string fDescrizione;
        [Persistent("DESCRIZIONE")]
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

        private Cause fCause;
        [Persistent("CAUSA"),
        DevExpress.Xpo.DisplayName("Causa")]
        public Cause Cause
        {
            get
            {
                return fCause;
            }
            set
            {
                SetPropertyValue<Cause>("ProblemaCausa", ref fCause, value);
            }
        }

        private ApparatoProblemaVista fApparatoProblema;
        [Association(@"AppProblemaVista_ProbCausaVista"),
        Persistent("PROBLEMA"),
        DevExpress.Xpo.DisplayName("Problema")]
        public ApparatoProblemaVista ApparatoProblemaVista
        {
            get
            {
                return fApparatoProblema;
            }
            set
            {
                SetPropertyValue<ApparatoProblemaVista>("ApparatoProblema", ref fApparatoProblema, value);
            }
        }

        [Association(@"causarimediovista_problemacausavista", typeof(CausaRimedioVista)),
        DevExpress.Xpo.DisplayName("Elenco Rimedio")]
        public XPCollection<CausaRimedioVista> CausaRimedios
        {
            get
            {
                return GetCollection<CausaRimedioVista>("CausaRimedios");
            }
        }


        public override string Name
        {
            get
            {
                return Descrizione;
            }
            set
            {
                SetPropertyValue("Name", ref fDescrizione, value);
            }
        }

        protected override IBindingList Children
        {
            get
            {
                return CausaRimedios;
            }
        }

        protected override ITreeNode Parent
        {
            get
            {
                return ApparatoProblemaVista;
            }
        }
    }
}
