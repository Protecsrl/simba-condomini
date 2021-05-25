using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;

using DevExpress.Persistent.Base;
using DevExpress.Xpo;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using CAMS.Module.PropertyEditors;

namespace CAMS.Module.DBKPI
{
    [DefaultClassOptions, Persistent("KPI_AVANZAMENTO_LT_FASCE")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "KPI di Avanzamento Lavori Fasce")]
    [NavigationItem("KPI")]
    [ImageName("StackedLine")]


    public class KPI_AvanzamentoLT_Fasce : XPObject
    {
        public KPI_AvanzamentoLT_Fasce() : base() { }

        public KPI_AvanzamentoLT_Fasce(Session session) : base(session) { }

        //    , KPI, AMIN, AMAX, ADESC, BMIN, BMAX, BDESC, CMIN, CMAX, CDESC, DMIN, DMAX, DDESC, EMIN, EMAX, EDESC, FMIN, FMAX, FDESC

        private string fKPIDescrizione;
        [Persistent("KPIDESCRIZIONE"), System.ComponentModel.DisplayName("KPI Descrizione")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(250)"), Size(250)]
        public string KPIDescrizione
        {
            get { return fKPIDescrizione; }
            set { SetPropertyValue<string>("KPIDescrizione", ref fKPIDescrizione, value); }
        }
        //-------------------------   a 
        private int fA_Min;
        [Persistent("A_MIN"), System.ComponentModel.DisplayName("A Min")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public int A_Min
        {
            get { return GetDelayedPropertyValue<int>("A_Min"); }
            set { SetDelayedPropertyValue<int>("A_Min", value); }
        }

        private int fA_Max;
        [Persistent("A_MAX"), System.ComponentModel.DisplayName("A Max")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public int A_Max
        {
            get { return GetDelayedPropertyValue<int>("A_Max"); }
            set { SetDelayedPropertyValue<int>("A_Max", value); }
        }

        private string fA_Descrizione;
        [Persistent("A_DESCRIZIONE"), System.ComponentModel.DisplayName("A Descrizione")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(250)"), Size(250)]
        [Delayed(true)]
        public string A_Descrizione
        {
            get { return GetDelayedPropertyValue<string>("A_Descrizione"); }
            set { SetDelayedPropertyValue<string>("A_Descrizione", value); }
        }

        //------------------------------------------------------------------------------------
        private int fB_Min;
        [Persistent("B_MIN"), System.ComponentModel.DisplayName("B Min")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public int B_Min
        {
            get { return GetDelayedPropertyValue<int>("B_Min"); }
            set { SetDelayedPropertyValue<int>("B_Min", value); }
        }

        private int fB_Max;
        [Persistent("B_MAX"), System.ComponentModel.DisplayName("B Max")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public int B_Max
        {
            get { return GetDelayedPropertyValue<int>("B_Max"); }
            set { SetDelayedPropertyValue<int>("B_Max", value); }
        }

        private string fB_Descrizione;
        [Persistent("B_DESCRIZIONE"), System.ComponentModel.DisplayName("B Descrizione")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(250)"), Size(250)]
        [Delayed(true)]
        public string B_Descrizione
        {
            get { return GetDelayedPropertyValue<string>("B_Descrizione"); }
            set { SetDelayedPropertyValue<string>("B_Descrizione", value); }
        }
        //---------------- C        -----------------------------------------------------
        private int fC_Min;
        [Persistent("C_MIN"), System.ComponentModel.DisplayName("C Min")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public int C_Min
        {
            get { return GetDelayedPropertyValue<int>("C_Min"); }
            set { SetDelayedPropertyValue<int>("C_Min", value); }
        }

        private int fC_Max;
        [Persistent("C_MAX"), System.ComponentModel.DisplayName("C Max")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public int C_Max
        {
            get { return GetDelayedPropertyValue<int>("C_Max"); }
            set { SetDelayedPropertyValue<int>("C_Max", value); }
        }

        private string fC_Descrizione;
        [Persistent("C_DESCRIZIONE"), System.ComponentModel.DisplayName("C Descrizione")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(250)"), Size(250)]
        [Delayed(true)]
        public string C_Descrizione
        {
            get { return GetDelayedPropertyValue<string>("C_Descrizione"); }
            set { SetDelayedPropertyValue<string>("C_Descrizione", value); }
        }

        //----------------                    D       -----------------------------------------------------
        private int fD_Min;
        [Persistent("D_MIN"), System.ComponentModel.DisplayName("D Min")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public int D_Min
        {
            get { return GetDelayedPropertyValue<int>("D_Min"); }
            set { SetDelayedPropertyValue<int>("D_Min", value); }
        }

        private int fD_Max;
        [Persistent("D_MAX"), System.ComponentModel.DisplayName("D Max")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public int D_Max
        {
            get { return GetDelayedPropertyValue<int>("D_Max"); }
            set { SetDelayedPropertyValue<int>("D_Max", value); }
        }

        private string fD_Descrizione;
        [Persistent("D_DESCRIZIONE"), System.ComponentModel.DisplayName("D Descrizione")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(250)"), Size(250)]
        [Delayed(true)]
        public string D_Descrizione
        {
            get { return GetDelayedPropertyValue<string>("D_Descrizione"); }
            set { SetDelayedPropertyValue<string>("D_Descrizione", value); }
        }
        //----------------                    E       -----------------------------------------------------
        private int fE_Min;
        [Persistent("E_MIN"), System.ComponentModel.DisplayName("E Min")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public int E_Min
        {
            get { return GetDelayedPropertyValue<int>("E_Min"); }
            set { SetDelayedPropertyValue<int>("E_Min", value); }
        }

        private int fE_Max;
        [Persistent("E_MAX"), System.ComponentModel.DisplayName("E Max")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public int E_Max
        {
            get { return GetDelayedPropertyValue<int>("E_Max"); }
            set { SetDelayedPropertyValue<int>("E_Max", value); }
        }

        private string fE_Descrizione;
        [Persistent("E_DESCRIZIONE"), System.ComponentModel.DisplayName("E Descrizione")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(250)"), Size(250)]
        [Delayed(true)]
        public string E_Descrizione
        {
            get { return GetDelayedPropertyValue<string>("E_Descrizione"); }
            set { SetDelayedPropertyValue<string>("E_Descrizione", value); }
        }
        //----------------                    F       -----------------------------------------------------
        private int fF_Min;
        [Persistent("F_MIN"), System.ComponentModel.DisplayName("F Min")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public int F_Min
        {
            get { return GetDelayedPropertyValue<int>("F_Min"); }
            set { SetDelayedPropertyValue<int>("F_Min", value); }
        }

        private int fF_Max;
        [Persistent("F_MAX"), System.ComponentModel.DisplayName("F Max")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public int F_Max
        {
            get { return GetDelayedPropertyValue<int>("F_Max"); }
            set { SetDelayedPropertyValue<int>("F_Max", value); }
        }

        private string fF_Descrizione;
        [Persistent("F_DESCRIZIONE"), System.ComponentModel.DisplayName("F Descrizione")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(250)"), Size(250)]
        [Delayed(true)]
        public string F_Descrizione
        {
            get { return GetDelayedPropertyValue<string>("F_Descrizione"); }
            set { SetDelayedPropertyValue<string>("F_Descrizione", value); }
        }
    }
}

