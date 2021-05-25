

using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBTask.AppsCAMS
{
    [DefaultClassOptions, Persistent("V_TRISORSEEMERGENZA")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Risorse Disponibili in Emergenza per RDL")]
    [NavigationItem(false)]
    public class TRisorseEmergenza : XPLiteObject
    {
        public TRisorseEmergenza(Session session) : base(session) { }

        int fCodice;
        [Key, Persistent("CODICE")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Appearance("TRisorseEmergenza.codice", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)] //---aggiunto invisibile RR
        public int Codice { get { return fCodice; } set { SetPropertyValue<int>("Codice", ref fCodice, value); } }

        RisorseTeam fRisorsaTeam;
        [Persistent("RISORSETEAM"), DisplayName("Squadra Risosrse")]
        [ExplicitLoading]
        public RisorseTeam RisorsaTeam { get { return fRisorsaTeam; } set { SetPropertyValue<RisorseTeam>("Risorsa", ref fRisorsaTeam, value); } }

        Risorse fRisorsaCapo;
        [Persistent("RISORSACAPO"), DisplayName("Capo Squadra")]
        [ExplicitLoading]
        public Risorse RisorsaCapo { get { return fRisorsaCapo; } set { SetPropertyValue<Risorse>("RisorsaCapo", ref fRisorsaCapo, value); } }

        private Immobile fImmobile;
        [Persistent("IMMOBILE"), DisplayName("Immobile")]
        [ExplicitLoading()]
        public Immobile Immobile
        {
            get
            {
                return fImmobile;
            }
            set
            {
                SetPropertyValue<Immobile>("Immobile", ref fImmobile, value);
            }
        }

        double fDist;
        [Persistent("DIST"), DisplayName("Distanza "), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        public double Dist
        {
            get { return fDist; }
            set { SetPropertyValue<double>("Dist", ref fDist, value); }
        }

        //  private string fInterventiInEdificio;
        [NonPersistent, DisplayName("Distanza"), Size(50)]
        public string DistanzaIntervento
        {
            get
            {
                try
                {
                    var Tmp = Evaluate(this.Dist);
                    if (Tmp != null)
                    {
                        using (CAMS.Module.Classi.Util ut = new CAMS.Module.Classi.Util())
                        {
                            double Distan = (double)Tmp;
                            return ut.GetDescrizioneDistanza(Distan);
                        }
                    }
                }
                catch
                {
                    return "non calcolabile";
                }

                return "non calcolabile";
            }

        }

    }
}
