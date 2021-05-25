using CAMS.Module.DBPlanner;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace EAMSConsole.MP.XPOObj
//{
//    class MPDataInizialeDettaglioDate
//    {
//    }
//}
namespace CAMS.Module.DBPlanner.SK
{
    [DefaultClassOptions, Persistent("MP_DATAINIZIALEDETTDATE")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettaglio")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem(false)]
    public class MPDataInizialeDettaglioDate : XPObject
    {

        public MPDataInizialeDettaglioDate()
           : base()
        {
        }

        public MPDataInizialeDettaglioDate(Session session)
           : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private DateTime fData;
        [Persistent("DATA"), DisplayName("Data Cadenza Obbligata"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", " ddd dd/MM/yyyy")]
        public DateTime Data
        {
            get
            {
                return fData;
            }

            set
            {
                SetPropertyValue<DateTime>("Data", ref fData, value);
            }
        }

        private MPDataInizialeDettaglio fMPDataInizialeDettaglio;
        [Persistent("MPDATAINIZIALEDETTAGLIO"), DisplayName("Registro Pianificazione")]
        [Association(@"MPDataInizialeDettaglio_DATE", typeof(MPDataInizialeDettaglio))]
        [MemberDesignTimeVisibility(false)]
        public MPDataInizialeDettaglio MPDataInizialeDettaglio
        {
            get
            {
                return fMPDataInizialeDettaglio;
            }
            set
            {
                SetPropertyValue<MPDataInizialeDettaglio>("MPDataInizialeDettaglio", ref fMPDataInizialeDettaglio, value);
            }
        }

    }
}
