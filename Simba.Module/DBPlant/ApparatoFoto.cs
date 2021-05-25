using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System.Drawing;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
//using DevExpress.ExpressApp.Demos;

namespace CAMS.Module.DBPlant
{
    public interface IPictureItem
    {
        string ID { get; }
        Image Image { get; }
        string Text { get; }
    }
    /// <summary>
    /// ////////////////////////////////////////////
    /// </summary>
    
    [Persistent("APPARATOFOTO")]//DefaultClassOptions,
    [System.ComponentModel.DisplayName("Foto")]
    [ImageName("Demo_Image_Library_Standard")]
    [NavigationItem(false)]
    public class ApparatoFoto : BaseObject, IPictureItem
    {
        public ApparatoFoto(Session session) : base(session) { }

        [Persistent("TITOLO"), DisplayName("Titolo")]
        public string Title
        {
            get { return GetPropertyValue<string>("Title"); }
            set { SetPropertyValue<string>("Title", value); }
        }

        [Persistent("FOTO"), DisplayName("Foto")]
        [Size(SizeAttribute.Unlimited), ValueConverter(typeof(ImageValueConverter))]
        public Image Foto
        {
            get { return GetPropertyValue<Image>("Foto"); }
            set { SetPropertyValue<Image>("Foto", value); }
        }
//, Aggregated
        //[Persistent("APPARATO"), DisplayName("Apparato")]
        //[Association(@"Apparato_ApparatoFoto", typeof(ApparatoFoto))]
        //public Apparato Apparato
        //{
        //    get { return GetPropertyValue<Apparato>("Apparato"); }
        //    set { SetPropertyValue<Apparato>("Apparato", value); }
        //}
        #region IPictureItem Members
        Image IPictureItem.Image
        {
            get { return Foto; }
        }
        string IPictureItem.Text
        {
            get { return String.Format("{0} by {1}", Title, "Apparato"); } //Apparato
        }
        string IPictureItem.ID
        {
            get { return Oid.ToString(); }
        }
        #endregion
        
    }
}
