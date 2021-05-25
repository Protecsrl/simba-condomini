using CAMS.Module.Classi;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAMS.Module.DBAngrafica
{
    [NavigationItem("Amministrazione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Destinatari per Azioni di Spedizione")]
    [DefaultClassOptions, Persistent("AZIONISPEDIZIONEMAIL")]
    [Indices("TipologiaSpedizione;Abilitato")]
    //[RuleCombinationOfPropertiesIsUnique("UniqrRubricaDestinatari", DefaultContexts.Save, "Email,Nome,Cognome,RoleAzione,SecurityRole", SkipNullOrEmptyValues = false)]
    [ImageName("NewContact")]
    [VisibleInDashboards(false)]
    public class AzioniSpedizioneMail : XPObject
    {

        public AzioniSpedizioneMail()
            : base()
        {
        }
        public AzioniSpedizioneMail(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            TipologiaSpedizione TipologiaSpedizione = TipologiaSpedizione.MAIL;

        }

        private TipoAzioniSpedizioneMail fTipoAzioniSpedizioneMail;
        [Persistent("TIPOAZIONISPEDIZIONEMAIL"), DevExpress.Xpo.DisplayName("Tipo Azioni Spedizione Mail")]
        //   [Appearance("Destinatari.DestinatariControlliNormativis", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public TipoAzioniSpedizioneMail TipoAzioniSpedizioneMail
        {
            get
            {
                return fTipoAzioniSpedizioneMail;
            }
            set
            {
                SetPropertyValue<TipoAzioniSpedizioneMail>("TipoAzioniSpedizioneMail", ref fTipoAzioniSpedizioneMail, value);
            }
        }

        private TipologiaSpedizione fTipologiaSpedizione;
        [Persistent("TIPOSPEDIZIONE"), DevExpress.Xpo.DisplayName("Tipo Spedizione")]
        public TipologiaSpedizione TipologiaSpedizione
        {
            get
            {
                return fTipologiaSpedizione;
            }
            set
            {
                SetPropertyValue<TipologiaSpedizione>("TipologiaSpedizione", ref fTipologiaSpedizione, value);
            }
        }

        private Destinatari fDestinatari;
        [Association(@"Destinatari_AzioniMail"), Persistent("DESTINATARI"), DevExpress.Xpo.DisplayName("Destinatari")]
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

    }
}
