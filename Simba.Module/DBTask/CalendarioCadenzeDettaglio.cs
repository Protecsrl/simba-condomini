using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//namespace CAMS.Module.DBTask
//{
//    class CalendarioCadenzeDettaglio
//    {
//    }
//}


#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
using System.Collections.Generic;
#pragma warning restore CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
using System.Linq;
#pragma warning restore CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Text' è già presente in questo spazio dei nomi
using System.Text;
#pragma warning restore CS0105 // La direttiva using per 'System.Text' è già presente in questo spazio dei nomi


using CAMS.Module.Classi;
using CAMS.Module.DBClienti;
using CAMS.Module.DBSpazi;
using CAMS.Module.DBTask;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using CAMS.Module.DBAux;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("CALENDARIOCADENZEDETT")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Date Cadenze")]
    [ImageName("BO_Contract")]
    [NavigationItem(false)]
    //[Appearance("Commesse.scadute.BKColor.Red", TargetItems = "*", FontStyle = System.Drawing.FontStyle.Strikeout, FontColor = "Salmon", Priority = 2, Criteria = "Oid > 0 And (Abilitato = 'No' Or DataAl <= LocalDateTimeToday())")]
    //[RuleCriteria("RuleCriteriaObject_RuleCriteria3", DefaultContexts.Save, @"DataAl > DataDal", SkipNullOrEmptyValues = false, CustomMessageTemplate = @"la data di inizio deve essere magiore della data di fine")]

    #region Abilitazione

    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo", "Abilitato == 1", "Attivi", true, Index = 0)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloNonAttivo", "Abilitato == 0", "non Attivi", Index = 1)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "Tutto", Index = 2)]

    #endregion


    public class CalendarioCadenzeDettaglio : XPObject
    {
        public CalendarioCadenzeDettaglio()
            : base()
        {
        }
        public CalendarioCadenzeDettaglio(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {
                Abilitato = FlgAbilitato.Si;// = "10;10;10";// tre livelli autorizzativi da 10 min ogni uno
            }
        }


        //private CalendarioCadenze CalendarioCadenze;
        [Association(@"CalendarioCadenze_CalendarioCadenzeDettaglio"), Persistent("CALENDARIOCADENZE"), System.ComponentModel.DisplayName("Calendario Cadenze")]
        [System.ComponentModel.Browsable(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public CalendarioCadenze CalendarioCadenze
        {
            get { return GetDelayedPropertyValue<CalendarioCadenze>("CalendarioCadenze"); }
            set { SetDelayedPropertyValue<CalendarioCadenze>("CalendarioCadenze", value); }            
        }


        private TimeSpan timeDalle;
          [Persistent("TIMEDALLE"), DisplayName("Dalle")]
        public TimeSpan TimeDalle
        {
            get { return timeDalle; }
            set { SetPropertyValue("TimeDalle", ref timeDalle, value); }
        }


          private TimeSpan timeAlle;
          [Persistent("TIMEALLE"), DisplayName("Alle")]
          public TimeSpan TimeAlle
          {
              get { return timeAlle; }
              set { SetPropertyValue("TimeAlle", ref timeAlle, value); }
          }

        //private DateTime fDataDal;
        //[Persistent("DATADAL"), DisplayName("Data Dal:")]
        ////[RuleRequiredField("Commesse.DataDal", "Save", @"Data Obligata", SkipNullOrEmptyValues = false)]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_EditMask + "}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_EditMask)]
        //[EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        ////[Appearance("commessa_Abilita.DataDal", Criteria = "DataDal >= LocalDateTimeToday()", BackColor = "Red", FontColor = "Black")]
        //public DateTime DataDal
        //{
        //    get
        //    {
        //        return fDataDal;
        //    }
        //    set
        //    {
        //        SetPropertyValue<DateTime>("DataDal", ref fDataDal, value);
        //    }
        //}
        //private DateTime fDataAl;
        //[Persistent("DATAAL"), DisplayName("Data Al:")]
        ////[RuleRequiredField("Commesse.fDataAl", "Save", @"Data Obligata", SkipNullOrEmptyValues = false)]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_EditMask + "}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_EditMask)]
        //[EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        ////[Appearance("commessa_Abilita.DataAl", Criteria = "DataAl <= LocalDateTimeToday()", BackColor = "Red", FontColor = "Black")]
        //public DateTime DataAl
        //{
        //    get
        //    {
        //        return fDataAl;
        //    }
        //    set
        //    {
        //        SetPropertyValue<DateTime>("DataAl", ref fDataAl, value);
        //    }
        //}


        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        [ImmediatePostData(true)]
        public FlgAbilitato Abilitato
        {
            get
            {
                return fAbilitato;
            }
            set
            {
                SetPropertyValue<FlgAbilitato>("Abilitato", ref fAbilitato, value);
            }
        }
        
        //  referente commerciale  villa Cavalleri 
        // referente operativo: Damerio, Rossetti, Di Palma
        // centro operativo: Milano.              

        private TipoGiornoSettimana fTipoGiornoSettimana;
        [Persistent("TIPOGIORNOSETTIMANA"), DevExpress.ExpressApp.DC.XafDisplayName("Giorno Settimana")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        //[ImmediatePostData(true)]
        public TipoGiornoSettimana TipoGiornoSettimana
        {
            get
            {
                return fTipoGiornoSettimana;
            }
            set
            {
                SetPropertyValue<TipoGiornoSettimana>("TipoGiornoSettimana", ref fTipoGiornoSettimana, value);
            }
        }


        private GruppoMesi fGruppoMesi;
        [Persistent("GRUPPOMESI"), DevExpress.Xpo.DisplayName("Gruppo Mesi")]
        //[Association(@"CalendarioCadenze_CalendarioCadenzeDettaglio")]
        //[DbType("varchar(7)")]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading]
        public GruppoMesi GruppoMesi
        {
            get
            {
                return fGruppoMesi;
            }
            set
            {
                SetPropertyValue<GruppoMesi>("GruppoMesi", ref fGruppoMesi, value);
            }
        }  

        //protected override void OnChanged(string propertyName, object oldValue, object newValue)
        //{
        //    base.OnChanged(propertyName, oldValue, newValue);

        //    if (IsInvalidated)
        //        return;

        //    if (!this.IsLoading)
        //    {
        //        if (newValue != null && propertyName == "Abilitato")
        //        {
        //            //FlgAbilitato newV = (FlgAbilitato)(newValue);
        //            //if (newV == FlgAbilitato.Si)
        //            //{
        //            //    this.DateUnService = DateTime.MinValue;
        //            //}
        //            //else
        //            //{
        //            //    this.DateUnService = DateTime.Now;
        //            //}
        //            //AggiornaAbilitatoErede(newV);

        //        }
        //    }
        //}

      

    }
}
