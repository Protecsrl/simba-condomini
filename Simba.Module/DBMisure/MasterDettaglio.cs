using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.DBPlant;
using CAMS.Module.DBSpazi;

namespace CAMS.Module.DBMisure
{
    [DefaultClassOptions, Persistent("DETTAGLIOMISMASTER")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettaglio Master Misure")]
    [ImageName("LoadPageSetup")]
    [NavigationItem(false)]
    public class MasterDettaglio : XPObject
    {
        public MasterDettaglio()
            : base()
        {
        }
        public MasterDettaglio(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private Asset fAsset;
        [Persistent("ASSET"), DisplayName("Asset")]
        [DataSourceCriteria("Servizio.Oid = '@This.Servizio.Oid'")]    //  Impianto  [DataSourceCriteria("Iif(Bookings.Count>0,Bookings[BookingDate != '@This.BookingDate'],True) && Subjects[Oid = '@This.Subject.Oid']")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public Asset Asset
        {
            get
            {
                return fAsset;
            }
            set
            {
                if (SetPropertyValue<Asset>("Asset", ref fAsset, value))
                {
                    //OnChanged("Immobile");
                    //OnChanged("Impianto");
                }
            }
        }

        private Servizio fServizio;
        // [PersistentAlias("Apparato.Impianto"), DisplayName("Impianto")]
        [NonPersistent, DisplayName("Servizio")]
        [DataSourceCriteria("Immobile.Oid = '@This.Master.Immobile.Oid'")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public Servizio Servizio
        {
            get
            {
                if (this.Asset != null)
                    return Asset.Servizio;

                return fServizio;
            }
            set
            {
                SetPropertyValue<Servizio>("Servizio", ref fServizio, value);
            }

        }


        private Locali fLocali;
        [PersistentAlias("Iif(Asset is null,null,Asset.Locale)")]
        //[NonPersistent]
        [DisplayName("Locale")]
        //[DataSourceCriteria("Local.Oid = '@This.Master.Immobile.Oid'")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        //[ImmediatePostData(true)]
        //[ExplicitLoading()]
        public Locali Locali
        {
            get
            {
                if (this.Servizio != null && this.Asset != null)
                    if (this.Servizio.ServizioGeoreferenziato == Classi.FlgAbilitato.No)
                    {
                        var tempObject = EvaluateAlias("Locali");
                        if (tempObject != null)
                            return (Locali)(tempObject);
                        else
                            return null;
                    }
                return null;
            }
            //set
            //{
            //    SetPropertyValue<Locali>("Locali", ref fLocali, value);
            //}

        }


        private Piani fPiani;
        [PersistentAlias("Iif(Asset is null,null,Iif(Apparato.Locale is null,null,Asset.Locale.Piano))")]
        //[NonPersistent]
        [DisplayName("Piano")]
        //[DataSourceCriteria("Local.Oid = '@This.Master.Immobile.Oid'")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        //[ImmediatePostData(true)]
        //[ExplicitLoading()]
        public Piani Piani
        {
            get
            {
                if (this.Servizio != null && this.Asset != null)
                    if (this.Servizio.ServizioGeoreferenziato == Classi.FlgAbilitato.No)
                    {
                        var tempObject = EvaluateAlias("Piani");
                        if (tempObject != null)
                            return (Piani)(tempObject);
                        else
                            return null;
                    }
                return null;
            }
            //set { SetPropertyValue<Piani>("Piani", ref fPiani, value); }

        }


        private Immobile fImmobile;  //   
        [PersistentAlias("Iif(Master is not null,Master.Immobile,null)")]
        //[PersistentAlias("Apparato.Impianto.Immobile"), DisplayName("Immobile")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [ExplicitLoading()]
        public Immobile Immobile
        {
            get
            {
                var tempObject = EvaluateAlias("Immobile");
                if (tempObject != null)
                {
                    return (Immobile)tempObject;
                }
                else
                {
                    return fImmobile;
                }
            }
        }




        private Master fMaster;
        [Association(@"MasterDettaglio_Master"), Persistent("MASTER")]
        //[System.ComponentModel.Browsable(false)]
        [ExplicitLoading()]
        public Master Master
        {
            get
            {
                return fMaster;
            }
            set
            {
                SetPropertyValue<Master>("Master", ref fMaster, value);
            }
        }



        public override string ToString()
        {

            if (Asset != null)
                if (Asset.Servizio != null)
                    return string.Format("{0}({1})", Asset, Asset.Servizio.Descrizione);
                else
                    return string.Format("{0}", Asset);
            else
                return string.Format("{0}", "non associato");


        }

    }
}
