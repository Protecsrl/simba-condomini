using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.ComponentModel;

namespace CAMS.Module.DBBollette
{
    [DefaultClassOptions, Persistent("BOLLETTEREG")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Bollette")]
    [ImageName("Action_Inline_Edit")]

    [NavigationItem("Misure")]

    public class RegistroBollette : XPObject
    {
        public RegistroBollette() : base() { }

        public RegistroBollette(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        private Immobile fImmobile;
        [Persistent("IMMOBILE"), DevExpress.Xpo.DisplayName("Immobile")]
        [Appearance("RegistroBollette.Abilita.Immobile", Criteria = "not (Impianto is null)", Enabled = false)]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Immobile Immobile
        {
            get { return GetDelayedPropertyValue<Immobile>("Immobile"); }
            set { SetDelayedPropertyValue<Immobile>("Immobile", value); }
   
        }

        private Servizio fServizio;
        [Persistent("SERVIZIO"), DevExpress.Xpo.DisplayName("Servizio")]
        [Appearance("RegistroBollette.Abilita.Servizio", Criteria = "Immobile is null", Enabled = false)]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Immobile = '@This.Immobile'")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Servizio Servizio
        {
            get { return GetDelayedPropertyValue<Servizio>("Servizio"); }
            set { SetDelayedPropertyValue<Servizio>("Servizio", value); }

        }

        private Asset fApparato;
        [Persistent("APPARATO"), DevExpress.Xpo.DisplayName("Apparato")]
        [DataSourceCriteria("Impianto.Oid = '@This.Impianto.Oid'")]
        //  [DataSourceCriteria("Iif(Bookings.Count>0,Bookings[BookingDate != '@This.BookingDate'],True) && Subjects[Oid = '@This.Subject.Oid']")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Asset Apparato
        {
            get { return GetDelayedPropertyValue<Asset>("Apparato"); }
            set { SetDelayedPropertyValue<Asset>("Apparato", value); }
            //get
            //{  return fApparato;     //}
            //set{
            //    if (SetPropertyValue<Apparato>("Apparato", ref fApparato, value))
            //    {
            //        //OnChanged("Immobile");
            //        //OnChanged("Impianto");
            //    }}
        }

        


        [Association(@"RegBollette_Bellette", typeof(Bollette)), Aggregated, DevExpress.Xpo.DisplayName("Dettaglio Bollette")]
        //[Appearance("RegMisure.Abilita.RegMisureDettaglios", Criteria = "Oid < 0", Enabled = false)]
        public XPCollection<DBBollette.Bollette> Bollettes
        {
            get
            {
                return GetCollection<Bollette>("Bollettes");
            }
        }

        private string fUtente;
        [Size(100), Persistent("UTENTE"), DevExpress.Xpo.DisplayName("Utente")]
        [Appearance("RegBollette.Abilita.Utente", Enabled = false)]
        [DbType("varchar(100)")]
        [Browsable(false)]
        [Delayed(true)]
        public string Utente
        {
            get { return GetDelayedPropertyValue<string>("Utente"); }
            set { SetDelayedPropertyValue<string>("Utente", value); }

            //get { return fUtente; }
            //set { SetPropertyValue<string>("Utente", ref fUtente, value); }
        }

        private DateTime fDataInserimento;
        [Persistent("DATAUPDATE"), DevExpress.Xpo.DisplayName("Data Upadate")]
        [Appearance("RegBollette.Abilita.DataInserimento", Enabled = false)]
        [Browsable(false)]
        public DateTime DataInserimento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataInserimento"); }
            set { SetDelayedPropertyValue<DateTime>("DataInserimento", value); }
            //get { return fDataInserimento; }
            //set { SetPropertyValue<DateTime>("DataInserimento", ref fDataInserimento, value); }
        }

    }
}
