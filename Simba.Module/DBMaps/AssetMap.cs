
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Drawing;
using System;
//namespace CAMS.Module.DBMaps
//{
//    class AssetMap
//    {
//    }
//}
namespace CAMS.Module.DBMaps
{
    //class AssetMap    //{
    [DefaultClassOptions, Persistent("ASSET_MAP")]
    [System.ComponentModel.DefaultProperty("Asset in Mappa")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Asset in Mappa")]
    [ImageName("BO_Country_v92")]
    [NavigationItem("Patrimonio")]
    public class AssetMap : XPObject, IMapsMarker
    {
        public AssetMap()
            : base()
        {
        }

        public AssetMap(Session session)
            : base(session)
        {
        }


        [Persistent("IMPIANTO_DESCRIZIONE")]
        [DevExpress.Xpo.DisplayName("Impianto")]
        [DbType("varchar(400)")]
        [Delayed(true)]
        public string Impianto_Descrizione
        {
            get { return GetDelayedPropertyValue<string>("Impianto_Descrizione"); }
            set { SetDelayedPropertyValue<string>("Impianto_Descrizione", value); }
        }

        [Persistent("OIDAPPARATO")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public int OidApparato
        {
            get { return GetDelayedPropertyValue<int>("OidApparato"); }
            set { SetDelayedPropertyValue<int>("OidApparato", value); }
        }

        [Persistent("DESCRIZIONE")]
        [DevExpress.Xpo.DisplayName("Descrizione")]
        [DbType("varchar(400)")]
        [Delayed(true)]
        public string Descrizione
        {
            get { return GetDelayedPropertyValue<string>("Descrizione"); }
            set { SetDelayedPropertyValue<string>("Descrizione", value); }
        }


        [Persistent("TITLE")]
        [DbType("varchar(4000)")]
        private string ftitle;

        [PersistentAlias("ftitle")]
        [DevExpress.Xpo.DisplayName("ftitle")]
        [Browsable(false)]
        public string Title
        {
            get { return ftitle; }
        }

        [Persistent("LATITUDE")]
        private double flatitude;

        [PersistentAlias("flatitude")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        public double Latitude
        {
            get { return flatitude; }
        }


        [Persistent("LONGITUDE")]
        private double flongitude;

        [PersistentAlias("flongitude")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        public double Longitude
        {
            get { return flongitude; }
        }

        [Persistent("INDIVIDUALICON"), DevExpress.Xpo.DisplayName("ICONA")]
        public string IndividualMarkerIcon { get; set; }
    }
}