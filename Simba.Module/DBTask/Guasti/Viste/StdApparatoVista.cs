using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using CAMS.Module.DBPlant;
using System.ComponentModel;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBTask.Guasti.Viste
{
    [DefaultClassOptions,    Persistent("V_STDAPPARATO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Sfoglia Class. Guasti"),  System.ComponentModel.DisplayName("Sfoglia Abero Guasti")]
    [ImageName("LoadPageSetup")]
    [NavigationItem("Tabelle Anagrafiche")]
    public class StdApparatoVista : TreeObject, ITreeNodeImageProvider
    {
        public StdApparatoVista(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public System.Drawing.Image GetImage(out string imageName)
        {
            imageName = "LoadPageSetup";
            return ImageLoader.Instance.GetImageInfo(imageName).Image;
        }
        

        //private string fDescrizione;
        //[Persistent("DESCRIZIONE")]
        //public string Descrizione
        //{
        //    get
        //    {
        //        return fDescrizione;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Descrizione", ref fDescrizione, value);
        //    }
        //}

        [Association(@"AppProblemaVista_StdApparatoVista", typeof(ApparatoProblemaVista)),
        DevExpress.Xpo.DisplayName("Problemi")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<ApparatoProblemaVista> Problemas
        {
            get
            {
                return GetCollection<ApparatoProblemaVista>("Problemas");
            }
        }


        public override string Name { get; set; }
      

        protected override IBindingList Children
        {
            get
            {
                return Problemas;
            }
        }

        protected override ITreeNode Parent
        {
            get
            {
                return null;
            }
        }
    }
}
