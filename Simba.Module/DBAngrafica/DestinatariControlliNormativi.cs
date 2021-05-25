using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CAMS.Module.DBAngrafica
{
    [DefaultClassOptions, Persistent("DESTINATARI_CN")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Destinatari x Immobile Impianti")]
    [RuleCombinationOfPropertiesIsUnique("RuleCombOfPropertiesIsUniqueObject", DefaultContexts.Save, "Immobile,Servizio;Destinatari,Destinatari.RoleAzione",
        CustomMessageTemplate = "Attenzione è stato già inserito l'immobile({Immobile}) per questo Destinatario ({Destinatari}). \r\nInserire un altro Immobile.",
        SkipNullOrEmptyValues = false)]
    [NavigationItem(false)]
    [ImageName("NewMail")]
    public class DestinatariControlliNormativi : XPObject
    {
        public DestinatariControlliNormativi() : base() { }
        public DestinatariControlliNormativi(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //this.Destinatari.RoleAzione
        }

        //[Delayed(true)]
        //public Richiedente Richiedente
        //{
        //    get
        //    {
        //        return GetDelayedPropertyValue<Richiedente>("Richiedente");
        //    }
        //    set
        //    {
        //        SetDelayedPropertyValue<Richiedente>("Richiedente", value);
        //    }
        //}

        private Immobile fImmobile;
        [Association(@"DestinatariControlliNormativi_Edificio"), Persistent("IMMOBILE"), DevExpress.Xpo.DisplayName("Immobile")]
        [RuleRequiredField("RuleReq.DestinatariControlliNormativi.Immobile", DefaultContexts.Save, "Immobile è un campo obbligatorio")]
        [Appearance("DestinatariControlliNormativi.Abilita.Immobile", Criteria = "not (Servizio  is null)", Enabled = false)]
        [ImmediatePostData(true)]
        //[ExplicitLoading()]
        [Delayed(true)]
        public Immobile Immobile
        {
            get
            {
                return GetDelayedPropertyValue<Immobile>("Immobile");
            }
            set
            {
                SetDelayedPropertyValue<Immobile>("Immobile", value);
            }


        }

        private Servizio fServizio;
        [Association(@"DestinatariControlliNormativi_Servizio"), Persistent("SERVIZIO"), DevExpress.Xpo.DisplayName("Servizio")]
        [Appearance("DestinatariControlliNormativi.Abilita.Servizio", Criteria = "(Immobile  is null)", Enabled = false)]

        [ImmediatePostData(true)]
        [DataSourceCriteria("Immobile = '@This.Immobile'")]
        [Delayed(true)]
        public Servizio Servizio
        {
            get
            {
                return GetDelayedPropertyValue<Servizio>("Servizio");
            }
            set
            {
                SetDelayedPropertyValue<Servizio>("Servizio", value);
            }
        }

        private Destinatari fDestinatari;
        [Association(@"Destinatari_DestinatariCN"), Persistent("DESTINATARI"), DevExpress.Xpo.DisplayName("Destinatari")]
        //   [Appearance("Destinatari.DestinatariControlliNormativis", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [Delayed(true)]
        public Destinatari Destinatari
        {
            get
            {
                return GetDelayedPropertyValue<Destinatari>("Destinatari");
            }
            set
            {
                SetDelayedPropertyValue<Destinatari>("Destinatari", value);
            }

            //get
            //{
            //    return fDestinatari;
            //}
            //set
            //{
            //    SetPropertyValue<Destinatari>("Destinatari", ref fDestinatari, value);
            //}
        }


        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo")]
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
        


        public override string ToString()
        {
            return string.Format("Destinatari {0} ", this.Destinatari);
        }
        //[Association(@"BookingReferencesTutor")]
        //[DataSourceCriteria("Iif(Bookings.Count>0,Bookings[BookingDate != '@This.BookingDate'],True) && Subjects[Oid = '@This.Subject.Oid']")]
        //public Tutor Tutor
        //{
        //    get { return fTutor; }
        //    set { SetPropertyValue<Tutor>("Tutor", ref fTutor, value); }
        //}


    }
}
