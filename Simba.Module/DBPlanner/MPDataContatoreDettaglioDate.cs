using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions, Persistent("MPDATACONTATOREDETTDATE")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettaglio")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem(false)]
    [VisibleInDashboards(false)]
    public class MPDataContatoreDettaglioDate : XPObject
    {

         public MPDataContatoreDettaglioDate()
            : base()
        {
        }

         public MPDataContatoreDettaglioDate(Session session)
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

        private MPDataContatoreDettaglio fMPDataContatoreDettaglio;
        [Persistent("MPDATACONTATOREDETTAGLIO"), DisplayName("Registro Pianificazione")]
        [Association(@"MPDataContatoreDettaglio_DATE", typeof(MPDataContatoreDettaglio))]
        [MemberDesignTimeVisibility(false)]       
        public MPDataContatoreDettaglio MPDataContatoreDettaglio
        {
            get
            {
                return fMPDataContatoreDettaglio;
            }
            set
            {

                SetPropertyValue<MPDataContatoreDettaglio>("MPDataContatoreDettaglio", ref fMPDataContatoreDettaglio, value);
            }
        }

    }
}
