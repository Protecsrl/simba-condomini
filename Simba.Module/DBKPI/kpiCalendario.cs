using CAMS.Module.Classi;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBKPI
{

    [DefaultClassOptions, Persistent("CALENDARIO")]  
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "KPI Calendario")]
    [ImageName("Action_Debug_Step")]
    [NavigationItem("KPI")]

    #region filtro tampone  IsThisYear
    [ListViewFilter("kpiCalendario.Blocco.no", "[Blocco] = 0", "Libero", true, Index = 0)]
    [ListViewFilter("kpiCalendario.Blocco.si", "[Blocco] = 1", "Bloccato", Index = 1)]
    [ListViewFilter("kpiCalendario.Blocco.tutto", "", "Tutto", Index = 2)]
 
    #endregion
    //---
    public class kpiCalendario : XPObject
    {
        public kpiCalendario()
            : base()
        {
        }

        public kpiCalendario(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        //  SELECT
        // [DATAGIORNO]  ----------
        //,[ANNO]
        //,[MESE]
        //,[GIORNO]
        //,[SETTIMANA]
        //,[FESTANAZIONALE]
        //,[BLOCCO]
        //  FROM[dbo].[CALENDARIO]


        private const string DateAndTimeOfDayEditMaskhhss = "dddd dd/MM/yyyy";
        private DateTime fDataGiorno;
        [Persistent("DATAGIORNO"), DisplayName("Data Riferimento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMaskhhss + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMaskhhss)]
        //[DbType("Date")]
        public DateTime DataGiorno
        {
            get { return fDataGiorno; }
            set { SetPropertyValue<DateTime>("DataGiorno", ref fDataGiorno, value); }
        }


        private Boolean fFestaNazione;
        [Persistent("FESTANAZIONALE"), DisplayName("Festa Nazionale")]
        [VisibleInListView(false)]
        public Boolean FestaNazione
        {
            get { return fFestaNazione; }
            set { SetPropertyValue<Boolean>("FestaNazione", ref fFestaNazione, value); }
        }
       
   
        private TipoBloccoDati fBlocco;  // 0=non bloccato , 1 Bloccato Tutto, 2 cloccato solo inserimento, 3 Bloccato solo aggiornamento
        [Persistent("BLOCCO"), DisplayName("Blocco")]
        public TipoBloccoDati Blocco
        {
            get { return fBlocco; }
            set { SetPropertyValue<TipoBloccoDati>("Blocco", ref fBlocco, value); }
        }
        public override string ToString()
        {
            return string.Format("{0}[{1}]", this.DataGiorno.ToString("dddd, MMMM dd yyyy"),this.Blocco.ToString());
        }
    }
}
