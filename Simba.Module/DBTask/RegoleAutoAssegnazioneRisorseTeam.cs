using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//namespace CAMS.Module.DBTask
//{
//    class RegoleAutoAssegnazioneRisorseTeam
//    {
//    }
//}
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Drawing;
using CAMS.Module.DBAux;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("REGOLEAUTOMULTITEAM")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Regole Altre RisorseTeam")]
    [ImageName("Risorse")]
    [NavigationItem(false)]
    public class RegoleAutoAssegnazioneRisorseTeam : XPObject
    {
               public RegoleAutoAssegnazioneRisorseTeam()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.

        }

               public RegoleAutoAssegnazioneRisorseTeam(Session session)
                   : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.

        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }



        private RegoleAutoAssegnazioneRdL fRegoleAutoAssegnazioneRdL;
        [Persistent("REGOLEAUTOASSEGNAZIONE"), Association(@"RegoleAutoAssegnazioneRdL_RegoleAutoAssegnazioneRdL")]
        [DisplayName("Regola Auto Assegnazione RdL")]
        [ExplicitLoading()]
        public RegoleAutoAssegnazioneRdL RegoleAutoAssegnazioneRdL
        {
            get
            {
                return fRegoleAutoAssegnazioneRdL;
            }
            set
            {
                SetPropertyValue<RegoleAutoAssegnazioneRdL>("RegoleAutoAssegnazioneRdL", ref fRegoleAutoAssegnazioneRdL, value);
            }
        }

        //Association(@"RdLApparatoSchedeMp_ApparatoSkMP"),
        private RisorseTeam fRisorseTeam;
        [Persistent("RISORSETEAM"), DisplayName("Risorsa Team Associata")]
        [ExplicitLoading]
        public RisorseTeam RisorseTeam
        {
            get
            {

                return fRisorseTeam;
            }
            set
            {
                SetPropertyValue<RisorseTeam>("RisorseTeam", ref fRisorseTeam, value);
            }
        }




    }
}



 