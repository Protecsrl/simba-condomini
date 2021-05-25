using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;


namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
    Persistent("FASEOPERATIVA")]
    [VisibleInDashboards(false)]
    [System.ComponentModel.DefaultProperty("FOperativa")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Fase Operativa")]
    [ImageName("SwitchTimeScalesTo")]
    [NavigationItem(false)]

    public class FaseOperativa : XPObject
    {
        public FaseOperativa()
            : base()
        {
        }
        public FaseOperativa(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fFaseOperativa;
        [Size(60),
        Persistent("FASEOPERATIVA"),
        DisplayName("Fase Operativa")]
        [DbType("varchar(60)")]
        public string FOperativa
        {
            get
            {
                return fFaseOperativa;
            }
            set
            {
                SetPropertyValue<string>("FOperativa", ref fFaseOperativa, value);
            }
        }
    }
}
