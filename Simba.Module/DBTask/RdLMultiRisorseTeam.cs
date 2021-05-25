using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Drawing;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("RDLMULTIRISORSETEAM")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "RdL Altre RisorseTeam")]
    [ImageName("Risorse")]
    [NavigationItem(false)]
    public class RdLMultiRisorseTeam : XPObject
    {
               public RdLMultiRisorseTeam()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.

        }

               public RdLMultiRisorseTeam(Session session)       : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.

        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        private string fDescrizione;
        [Size(200),
        Persistent("DESCRIZIONE"),
        DisplayName("Descrizione")]
        [DbType("varchar(200)")]
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

        private RdL fRdL;
        [Persistent("RDL"), Association(@"RdLMultiRisorseTeam_RdL"), DisplayName("RdL")]
        [ExplicitLoading()]
        public RdL RdL
        {
            get
            {
                return fRdL;
            }
            set
            {
                SetPropertyValue<RdL>("RdL", ref fRdL, value);
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



 