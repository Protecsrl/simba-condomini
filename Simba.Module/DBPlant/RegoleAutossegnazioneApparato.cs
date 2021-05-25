
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Drawing;
using CAMS.Module.DBAux;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("REGOLEAUTOAPPARATO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Regole Apparati")]
    [ImageName("Apparato")]
    [NavigationItem(false)]
    public class RegoleAutossegnazioneApparato : XPObject
    {
        public RegoleAutossegnazioneApparato()
     : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.

        }

        public RegoleAutossegnazioneApparato(Session session)
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
        [Persistent("REGOLEAUTOASSEGNAZIONE"), Association(@"RegoleAutoAssegnazioneRdL_RegoleAutoAssegnazioneRdLA")]
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
        private Asset fApparato;
        [Persistent("APPARATO"), DisplayName("Apparato Associato")]
        [DataSourceCriteria("Impianto.Oid = '@This.RegoleAutoAssegnazioneRdL.Impianto.Oid'")]
        //[DataSourceCriteria("[<Apparato>][^.Oid == RegoleAutoAssegnazioneRdL.Impianto And Impianto.Oid == '@This.RegoleAutoAssegnazioneRdL.Impianto.Oid'")]
        //[DataSourceCriteria("[<RdL>][^.Oid == RegistroRdL.Oid And Impianto.Oid == '@This.RegistroLavori.Impianto.Oid' And UltimoStatoSmistamento.Oid In(11,2,3,4,5,8,9)] And RegistroLavoris.Count = 0 And RegistroLavoriAltreRegRdLs.Count = 0")]
        [ExplicitLoading]
        public Asset Apparato
        {
            get
            {

                return fApparato;
            }
            set
            {
                SetPropertyValue<Asset>("Apparato", ref fApparato, value);
            }
        }




    }
}



