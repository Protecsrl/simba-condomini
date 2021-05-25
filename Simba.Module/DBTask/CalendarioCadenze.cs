using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//namespace CAMS.Module.DBTask
//{
//    class CalendarioCadenze
//    {
//    }
//}


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
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.DC;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("CALENDARIOCADENZE")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Calendario Cadenze")]
    [ImageName("BO_Contract")]
    //[NavigationItem("Contratti")]
    //[Appearance("Commesse.scadute.BKColor.Red", TargetItems = "*", FontStyle = System.Drawing.FontStyle.Strikeout, FontColor = "Salmon", Priority = 2, Criteria = "Oid > 0 And (Abilitato = 'No' Or DataAl <= LocalDateTimeToday())")]
    //[RuleCriteria("RuleCriteriaObject_RuleCriteria3", DefaultContexts.Save, @"DataAl > DataDal", SkipNullOrEmptyValues = false, CustomMessageTemplate = @"la data di inizio deve essere magiore della data di fine")]

    #region Abilitazione

    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo", "Abilitato == 1", "Attivi", true, Index = 0)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloNonAttivo", "Abilitato == 0", "non Attivi", Index = 1)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "Tutto", Index = 2)]

    #endregion


    public class CalendarioCadenze : XPObject
    {
        public CalendarioCadenze()
            : base()
        {
        }
        public CalendarioCadenze(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {
                //TempoLivelloAutorizzazioneGuasto = "10;10;10";// tre livelli autorizzativi da 10 min ogni uno
            }
        }

        [Association(@"CalendarioCadenze_CalendarioCadenzeDettaglio", typeof(CalendarioCadenzeDettaglio)),DevExpress.Xpo.Aggregated]
        [XafDisplayName("Calendario Cadenze Dettaglio")]
        [Appearance("CalendarioCadenze.CalendarioCadenzeDettaglio.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<CalendarioCadenzeDettaglio> CalendarioCadenzeDettaglioS
        {
            get
            {
                return GetCollection<CalendarioCadenzeDettaglio>("CalendarioCadenzeDettaglioS");
            }
        }

        private string fDescrizione;
        [Size(250), Persistent("DESCRIZIONE"), DisplayName("Descrizione")]
        [RuleRequiredField("RReqField.CalendarioCadenze.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
        [DbType("varchar(250)")]
        public string Descrizione
        {
            get
            {
                return fDescrizione;
            }
            set
            {
                SetPropertyValue<string>("Descrizione", ref fDescrizione, value);
            }
        }

        private bool fFesteNazionali;
        [Persistent("FESTENAZIONALI"), DisplayName("Feste Nazionali")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool FesteNazionali
        {
            get
            {
                return fFesteNazionali;
            }
            set
            {
                SetPropertyValue<bool>("FesteNazionali", ref fFesteNazionali, value);
            }
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM";
        private DateTime? fSantoPadrono;
        [ImmediatePostData(true)]
        [Persistent("SANTOPATRONO"), DisplayName("Santo Patrono")]
        public DateTime? SantoPadrono
        {
            get
            {
                return fSantoPadrono;
            }
            set
            {
                SetPropertyValue<DateTime?>("SantoPadrono", ref fSantoPadrono, value);
            }
        }


        //  referente commerciale  villa Cavalleri 
        // referente operativo: Damerio, Rossetti, Di Palma
        // centro operativo: Milano.              
 

        //private string fWBS;
        //[Persistent("WBS"), Size(7), DevExpress.Xpo.DisplayName("Cod WBS")]
        //[DbType("varchar(7)")]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //[ExplicitLoading]
        //public string WBS
        //{
        //    get
        //    {
        //        return fWBS;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("WBS", ref fWBS, value);
        //    }
        //}  

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (IsInvalidated)
                return;

            if (!this.IsLoading)
            {
                if (newValue != null && propertyName == "Abilitato")
                {
                    //FlgAbilitato newV = (FlgAbilitato)(newValue);
                    //if (newV == FlgAbilitato.Si)
                    //{
                    //    this.DateUnService = DateTime.MinValue;
                    //}
                    //else
                    //{
                    //    this.DateUnService = DateTime.Now;
                    //}
                    //AggiornaAbilitatoErede(newV);

                }
            }
        }

        //void AggiornaAbilitatoErede(FlgAbilitato newV)
        //{
        //    foreach (Immobile ed in this.Edificios)
        //    {
        //        ed.AbilitazioneEreditata = newV;

        //        foreach (Impianto im in ed.Impianti)
        //        {
        //            im.AbilitazioneEreditata = newV;

        //            foreach (Apparato Ap in im.APPARATOes)
        //            {
        //                Ap.AbilitazioneEreditata = newV;

        //                foreach (ApparatoSchedaMP ApSK in Ap.AppSchedaMpes)
        //                {
        //                    ApSK.AbilitazioneEreditata = newV;
        //                }
        //            }
        //        }

        //        foreach (Piani pi in ed.Pianies)
        //        {
        //            pi.AbilitazioneEreditata = newV;
        //            foreach (Locali lo in pi.Localies)
        //            {
        //                lo.AbilitazioneEreditata = newV;
        //            }
        //        }

        //    }
        //}

    }
}
