

using CAMS.Module.DBTask;
using CAMS.Module.DBTask.AppsCAMS;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;



namespace CAMS.Module.DBPlant.Vista
{
    [DefaultClassOptions, Persistent("V_DIST_RIS_EDIFICIO_RDL")]
    [VisibleInDashboards(false)]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Distanza della Risorsa Dal RDL")]
    //[NavigationItem("Tabelle Anagrafiche")]     [NavigationItem(false)]
    [NavigationItem(false)]

    public class RisorseDistanzeRdL : XPLiteObject
    {

        public RisorseDistanzeRdL(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }
        //codice, NOTIFICHEEMERGENZEREG ,RISORSETEAM,IMMOBILE,IMPIANTO,INDIRIZZO 
        string fCodice;
        [Key, Persistent("CODICE")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Appearance("RisorseDistanzeRdL.codice", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)] //---aggiunto invisibile RR
        public string Codice { get { return fCodice; } set { SetPropertyValue<string>("Codice", ref fCodice, value); } }

        RisorseTeam fRisorsaTeam;
        [Persistent("RISORSETEAM")]
        [ExplicitLoading]
        public RisorseTeam RisorsaTeam { get { return fRisorsaTeam; } set { SetPropertyValue<RisorseTeam>("Risorsa", ref fRisorsaTeam, value); } }

        Risorse fRisorsaCapo;
        [Persistent("RISORSACAPO")]
        [ExplicitLoading]
        public Risorse RisorsaCapo { get { return fRisorsaCapo; } set { SetPropertyValue<Risorse>("RisorsaCapo", ref fRisorsaCapo, value); } }
        
        #region DISTANZA
        double fDist;
        [Persistent("DIST"), DisplayName("Distanza"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        public double Dist
        {
            get { return fDist; }
            set { SetPropertyValue<double>("Dist", ref fDist, value); }
        }
        #endregion
 
        RegNotificheEmergenze fRegNotificheEmergenze;
        [Persistent("NOTIFICHEEMERGENZEREG")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Association(@"Reg_DistanzeRisorse"), DisplayName("Risorse e distanze")]
        [Appearance("RisorseDistanzeRdL.RegNotificheEmergenze", Enabled = false)] //---aggiunto invisibile RR
        public RegNotificheEmergenze RegNotificheEmergenze { get { return fRegNotificheEmergenze; } 
            set { SetPropertyValue<RegNotificheEmergenze>("RegNotificheEmergenze", ref fRegNotificheEmergenze, value); } }




       
    }

       

}