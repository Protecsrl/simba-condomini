using System;
using DevExpress.Xpo;

using DevExpress.Persistent.Base;

using CAMS.Module.DBALibrary;

namespace CAMS.Module.DBVerifiche
{
    [DefaultClassOptions,
    Persistent("SCHEDEMPDACORREGGERE"),
    System.ComponentModel.DefaultProperty("SchedaMp.CodPmp")]
    [VisibleInDashboards(false)]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Schede MP da Correggere")]
    [ImageName("PMPDaCorreggere")]
    public class PMPdaCorreggere : XPLiteObject
    {
        public PMPdaCorreggere()
            : base()
        {
        }

        public PMPdaCorreggere(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }





        private SchedaMp fSchedaMp;
        [Key,
        Persistent("OID")]
        public SchedaMp Pmp
        {
            get
            {
                return fSchedaMp;
            }
            set
            {
                SetPropertyValue<SchedaMp>("SchedaMp", ref fSchedaMp, value);
            }
        }


        private string fTipoServizio;
        [Persistent("TIPOSERVIZIO"),
        Size(150)]
        public string TipoServizio
        {
            get
            {
                return fTipoServizio;
            }
            set
            {
                SetPropertyValue<string>("TipoServizio", ref fTipoServizio, value);
            }
        }

        private string fAsset;
        [Persistent("ASSET"),
        Size(600)]
        public string Asset
        {
            get
            {
                return fAsset;
            }
            set
            {
                SetPropertyValue<string>("Asset", ref fAsset, value);
            }
        }

        private string fTipoErrore;
        [Persistent("TIPOERRORE"),
        Size(60)]
        public string TipoErrore
        {
            get
            {
                return fTipoErrore;
            }
            set
            {
                SetPropertyValue<string>("TipoErrore", ref fTipoErrore, value);
            }
        }
    }
}
