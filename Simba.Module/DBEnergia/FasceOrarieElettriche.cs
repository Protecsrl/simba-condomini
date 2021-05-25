using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using CAMS.Module.Classi;

namespace CAMS.Module.DBEnergia
{
    [DefaultClassOptions, Persistent("FASCEORARIEELETTRICHE")]
    //[RuleCombinationOfPropertiesIsUnique("UniqueApparatoCarTecniche", DefaultContexts.Save, "Apparato, StdApparatoCaratteristicheTecniche,ParentObject")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "FasceO rarie Elettriche")]
    [System.ComponentModel.DefaultProperty("FullValore")]
    [ImageName("Action_EditModel")]
    //[NavigationItem(false)]//"Consistenza"
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("ApparatoCaratteristicheTecniche", "", "Tutto", true, Index = 1)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("ApparatoCaratteristicheTecniche.UnitamisuraAdim", "DescUnitaMisura = 'Adimensionale'", "UM-Adimensionale", Index = 0)]


    public class FasceOrarieElettriche : XPObject
    {
        public FasceOrarieElettriche() : base() { }
        public FasceOrarieElettriche(Session session) : base(session) { }

        private TipoGiornoSettimana fGiornoSettimana;
        [Persistent("GGSETT"), System.ComponentModel.DisplayName("Giorno Settimana")]
        public TipoGiornoSettimana GiornoSettimana
        {
            get { return fGiornoSettimana; }
            set { SetPropertyValue<TipoGiornoSettimana>("GiornoSettimana", ref fGiornoSettimana, value); }
        }

        private int fOra;
        [Persistent("ORA"), System.ComponentModel.DisplayName("Giorno Ora")]
        public int Ora
        {
            get { return fOra; }
            set { SetPropertyValue<int>("Ora", ref fOra, value); }
        }

        private int fFascia;
        [Persistent("FASCIA"), System.ComponentModel.DisplayName("Giorno Ora")]
        public int Fascia
        {
            get { return fFascia; }
            set { SetPropertyValue<int>("Fascia", ref fFascia, value); }
        }

        [PersistentAlias("GiornoSettimana +' - ' " +
                          " + Ora +' - ' " +
                          " + 'F' + Fascia" )]
        [System.ComponentModel.DisplayName("Descrizione")]
        [VisibleInListView(true)]
        public string FullValore
        {
            get
            {
                object tempObject = EvaluateAlias("FullValore");
                if (tempObject != null)
                {
                    return (string)tempObject.ToString();
                }
                else
                {
                    return "NA";
                }
            }
        }
    }

}
