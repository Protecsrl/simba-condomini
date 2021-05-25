using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CAMS.Module.DBAux
{
    [DefaultClassOptions, Persistent("GRUPPOMESI")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Gruppo Mesi")]
    [ImageName("BO_Contract")]
    [VisibleInDashboards(false)]
    #region Abilitazione


    #endregion


    public class GruppoMesi : XPObject
    {
        public GruppoMesi()
            : base()
        {
        }
        public GruppoMesi(Session session)
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

        ////[Association(@"CalendarioCalendarioCadenzeDettaglio", typeof(CalendarioCadenzeDettaglio)),DevExpress.Xpo.Aggregated]
        //[XafDisplayName("Calendario Cadenze Dettaglio")]
        //[Appearance("CalendarioCadenze.CalendarioCadenzeDettaglio.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1", Visibility = ViewItemVisibility.Hide)]
        //public XPCollection<CalendarioCadenzeDettaglio> CalendarioCadenzeDettaglioS
        //{
        //    get
        //    {
        //        return GetCollection<CalendarioCadenzeDettaglio>("CalendarioCadenzeDettaglioS");
        //    }
        //}

        private string fDescrizione;
        [Size(120), Persistent("DESCRIZIONE"), DisplayName("Descrizione")]
        [RuleRequiredField("RReqField.GruppoMesi.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
        [DbType("varchar(120)")]
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

        [Association(@"GruppoMesi_Mesi", typeof(GruppoMesiMesi)), DevExpress.Xpo.Aggregated]
        [XafDisplayName("Mesi")]
        [Appearance("GruppoMesi.Mesi.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<GruppoMesiMesi> Mesi
        {
            get
            {
                return GetCollection<GruppoMesiMesi>("Mesi");
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
 

        //        }
        //    }
        //}

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
