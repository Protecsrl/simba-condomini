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
    [DefaultClassOptions,   Persistent("V_CAUSA_RIMEDIO")]
    [ImageName("Suggestion")]
    [NavigationItem(false)]
    public class CausaRimedioVista : TreeObject, ITreeNodeImageProvider
    {
        public CausaRimedioVista(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public System.Drawing.Image GetImage(out string imageName)
        {
            imageName = "Suggestion";
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

        private ProblemaCausaVista fProblemaCausaVista;
        [Association(@"causarimediovista_problemacausavista"),
        Persistent("CAUSA"),
        DevExpress.Xpo.DisplayName("Causa")]
        public ProblemaCausaVista ProblemaCausaVista
        {
            get
            {
                return fProblemaCausaVista;
            }
            set
            {
                SetPropertyValue<ProblemaCausaVista>("ProblemaCausa", ref fProblemaCausaVista, value);
            }
        }

        private Rimedi fRimedio;
        [Persistent("RIMEDIO"),
        DevExpress.Xpo.DisplayName("Rimedio")]
        public Rimedi Rimedi
        {
            get
            {
                return fRimedio;
            }
            set
            {
                SetPropertyValue<Rimedi>("Rimedio", ref fRimedio, value);
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
                return new BindingList<object>();
            }
        }

        protected override ITreeNode Parent
        {
            get
            {
                return ProblemaCausaVista;
            }
        }
    }
}

