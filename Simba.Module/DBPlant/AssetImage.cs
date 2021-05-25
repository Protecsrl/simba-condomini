using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Helpers;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System.Drawing;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp;

//using CAMS.Module.BaseObjects;

namespace CAMS.Module.DBPlant
{

    // [NavigationItem(Captions.PropertyEditorsGroup), DefaultListViewOptions(true, NewItemRowPosition.Top), System.ComponentModel.DisplayName(Captions.PropertyEditors_ImageProperties)]
    //[Hint(Hints.ImagePropertiesHint)]
    [Persistent("ASSETIMAGE")]//DefaultClassOptions,
    [System.ComponentModel.DisplayName("Immagini")]
    [NavigationItem(false)]
    [ImageName("PropertyEditors.Demo_Image_Properties")]

    public class AssetImage : BaseObject
    {
        public AssetImage(Session session) : base(session) { }

        [Persistent("ASSET"), DisplayName("Asset")]
        [Association(@"Asset_AssetImage", typeof(Asset))]//[Association(@"Apparato_ApparatoImage", typeof(ApparatoFoto))]
        public Asset Asset
        {
            get { return GetPropertyValue<Asset>("Asset"); }
            set { SetPropertyValue<Asset>("Asset", value); }
        }

        [Persistent("TITOLO"), DisplayName("Titolo")]
        public string Title
        {
            get { return GetPropertyValue<string>("Title"); }
            set { SetPropertyValue<string>("Title", value); }
        }

                     
        [Persistent("ICONA"), DisplayName("Icona")]
        [DevExpress.Xpo.Size(SizeAttribute.Unlimited)]
        [VisibleInListViewAttribute(true)]
        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit,
            ListViewImageEditorCustomHeight = 140, DetailViewImageEditorFixedHeight = 360)]
        [Delayed(true)]
        public byte[] Icona // Image Icona
        {
            get { return GetDelayedPropertyValue<byte[]>("Icona"); }
            set { SetDelayedPropertyValue<byte[]>("Icona", value); }
        }

    }
}


