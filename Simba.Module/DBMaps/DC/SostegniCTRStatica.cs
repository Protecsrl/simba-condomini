using CAMS.Module.Classi;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
//using DevExpress.Xpo;
using System.ComponentModel;
using System.Drawing;
//namespace CAMS.Module.DBMaps.DC
//{
//    class SostegniCTRStatica
//    {
//    }
//}
namespace CAMS.Module.DBMaps.DC
{
    // [DevExpress.ExpressApp.DC.DomainComponent] https://www.devexpress.com/Support/Center/Question/Details/T403748, Enabled = false
    [DomainComponent]
    [NavigationItem(false)]
    //    [Appearance("DCRisorseTeamRdL.Ordinamento_3.evidenzia.Lime", AppearanceItemType = "ViewItem", TargetItems = "*",
    //Context = "DCRisorseTeamRdL_LookupListView;DCRisorseTeamRdL_LookupListView_MP", Criteria = "Ordinamento = 3", FontStyle = FontStyle.Bold, BackColor = "Lime", FontColor = "Black")]
    //    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Risorse Team x RdL")]
    //    [Appearance("DCRisorseTeamRdL.Conduttore.evidenzia.Yellow", AppearanceItemType = "ViewItem", TargetItems = "*",
    //   Context = "DCRisorseTeamRdL_LookupListView;DCRisorseTeamRdL_LookupListView_MP", Criteria = "Ordinamento = 2", BackColor = "Yellow", FontColor = "Black")]
    //    [Appearance("DCRisorseTeamRdL.Ordinamento.evidenzia.PaleGreen", AppearanceItemType = "ViewItem", TargetItems = "*",
    //Context = "DCRisorseTeamRdL_LookupListView;DCRisorseTeamRdL_LookupListView_MP", Criteria = "Ordinamento = 1", BackColor = "PaleGreen", FontColor = "Black")]
    //    [Appearance("DCRisorseTeamRdL.Ordinamento.evidenzia.Salmon", AppearanceItemType = "ViewItem", TargetItems = "*",
    //Context = "DCRisorseTeamRdL_LookupListView;DCRisorseTeamRdL_LookupListView_MP", Criteria = "Ordinamento = -1", BackColor = "Salmon", FontColor = "Black")]
    //  CAMS.Module.DBMaps.DC
    public class SostegniCTRStatica : IMapsMarker
    {
        [DevExpress.ExpressApp.Data.Key]
        [Browsable(false)]
        public Guid Oid { get; set; }

        [Index(1)]
        [XafDisplayName("Title")]
        public string Title { get; set; }   

        [XafDisplayName("Anno")]
        [Index(2)]
        public string Anno { get; set; }

        [XafDisplayName("Stato")]
        [Index(3)]
        public TipoVerificaStatica Stato { get; set; }

        #region IMapsMarker 
        [Index(4)]
        [XafDisplayName("Latitude")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        public double Latitude { get; set; }

        [Index(5)]
        [XafDisplayName("Longitude")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        public double Longitude { get; set; }

        [Index(6)]
        [XafDisplayName("ICONA")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public string IndividualMarkerIcon { get; set; }

        [XafDisplayName("Prescrizione")]
        [Index(7)]
        public string Prescrizione { get; set; }


        #endregion   IMapsMarker

        #region indici classi e user      
        [XafDisplayName("Oid Apparato")]
        [Browsable(false)]
        public int OidApparato { get; set; }
  
        [XafDisplayName("Ordinamento")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int Ordinamento { get; set; }

        [XafDisplayName("User")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string UserName { get; set; }
        #endregion

    }

     




}

