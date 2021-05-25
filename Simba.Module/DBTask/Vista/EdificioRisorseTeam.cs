

using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBTask.Vista
{
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Relazione Immobile Risorse Team")]
    [DefaultClassOptions, Persistent("V_CR_CO_ED_RISORSETEAM")]
    [NavigationItem(false)]

    public class EdificioRisorseTeam : XPLiteObject
    {
         public EdificioRisorseTeam() : base() { }

         public EdificioRisorseTeam(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        private string fcodice;
        [Key, Persistent("CODICE"), MemberDesignTimeVisibility(false)]
        public string Codice
        {
            get
            {
                return fcodice;
            }
            set
            {
                SetPropertyValue<string>("Codice", ref fcodice, value);
            }
        }
//        CODICE	9082
//AREADIPOLO	PE BioMassa
//IMMOBILE	Centrale BIOMASSA SELLERO
//OIDEDIFICIO	9
//COMMESSA	BIOMASSA SELLERO
//CENTROOPOERATIVO	Centro Operativo Centrale DI COLLIO
//OIDRISORSETEAM	82

        private Immobile fImmobile;
        [MemberDesignTimeVisibility(false)]
        //[Association(@"Scenario_MansioneCarico", typeof(Immobile))]
        [Persistent("OIDIMMOBILE"), DisplayName("Immobile")]
        [ExplicitLoading()]
        public Immobile Immobile
        {
            get
            {
                return fImmobile;
            }
            set
            {
                SetPropertyValue("Immobile", ref fImmobile, value);
            }
        }

        private RisorseTeam fRisorsaTeam;
        [Persistent("OIDRISORSETEAM"),
        DisplayName("Risorsa Team")]
        //[Appearance("ScenarioMansioneCarico.", Enabled = false)]
        [ExplicitLoading]
        public RisorseTeam RisorsaTeam
        {
            get
            {
                return fRisorsaTeam;
            }
            set
            {
                SetPropertyValue<RisorseTeam>("RisorsaTeam", ref fRisorsaTeam, value);
            }
        }

        //private Mansioni fMansione;
        //[Persistent("MANSIONE")]
        //public Mansioni Mansione
        //{
        //    get
        //    {
        //        return fMansione;
        //    }
        //    set
        //    {
        //        SetPropertyValue<Mansioni>("Mansione", ref fMansione, value);
        //    }
        //}


        private Mansioni fMansione;
        [PersistentAlias("RisorsaTeam.RisorsaCapo.Mansione"), DisplayName("Mansione")]
        public Mansioni Mansione
        {
            get
            {
                
                var tempObject = EvaluateAlias("Mansione");
                if (tempObject != null)
                {
                    return (Mansioni)tempObject;
                }
                else
                {
                    return null;
                }
                return fMansione;
            }
        }
    }
}
