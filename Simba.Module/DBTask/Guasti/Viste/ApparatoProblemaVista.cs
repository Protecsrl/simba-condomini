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
    [DefaultClassOptions,    Persistent("V_APPARATO_PROBLEMA")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Albero Problema Causa Rimedio")]
    [NavigationItem(false)]
    [ImageName("BO_Attention")]

    public class ApparatoProblemaVista : TreeObject, ITreeNodeImageProvider
    {
        public ApparatoProblemaVista(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public System.Drawing.Image GetImage(out string imageName)
        {
            imageName = "BO_Attention";
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

        private Problemi fProblemi;
        [Persistent("PROBLEMA"),
        DevExpress.Xpo.DisplayName("Problema")]
        public Problemi Problemi
        {
            get
            {
                return fProblemi;
            }
            set
            {
                SetPropertyValue<Problemi>("Problemi", ref fProblemi, value);
            }
        }

        private StdApparatoVista fStdApparatoVista;
        [Association(@"AppProblemaVista_StdApparatoVista"),
        Persistent("STDAPPARATO"),
        DevExpress.Xpo.DisplayName("StdApparatoVista")]
        public StdApparatoVista StdApparatoVista
        {
            get
            {
                return fStdApparatoVista;
            }
            set
            {
                SetPropertyValue<StdApparatoVista>("StdApparatoVista", ref fStdApparatoVista, value);
            }
        }

        [Association(@"AppProblemaVista_ProbCausaVista", typeof(ProblemaCausaVista)),
        DevExpress.Xpo.DisplayName("Elenco Cause Associate")]
        public XPCollection<ProblemaCausaVista> ProblemaCausas
        {
            get
            {
                return GetCollection<ProblemaCausaVista>("ProblemaCausas");
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
                return ProblemaCausas;
            }
        }

        protected override ITreeNode Parent
        {
            get
            {
                return StdApparatoVista;
            }
        }
    }
}

