using System;
using CAMS.Module.DBAngrafica;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using CAMS.Module.Classi;

namespace CAMS.Module.Costi
{
    [DefaultClassOptions,    Persistent("REGCOSTIDETTAGLIO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Consuntivo Costi")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [NavigationItem("Gestione Contabilità")]
    [ImageName("BO_Opportunity")]
    public class RegistroCostiDettaglio : XPObject
    {
        public RegistroCostiDettaglio()
            : base()
        {
        }

        public RegistroCostiDettaglio(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private RegistroCosti fRegistroCosti;
        [Association(@"RegistroCostiDettaglio_RegistroCosti"),
        Persistent("REGISTROCOSTI"),
        DisplayName("Registro Costi")]
        [ExplicitLoading()]
        public RegistroCosti RegistroCosti
        {
            get
            {
                return fRegistroCosti;
            }
            set
            {
                SetPropertyValue<RegistroCosti>("RegistroCosti", ref fRegistroCosti, value);
            }
        }

        private Fornitore fFornitore;
        //[Association(@"RegistroCostiDettaglio_Fornitore"),
     [   Persistent("FORNITORE"),        DisplayName("Fornitore")]
        [ExplicitLoading()]
        public Fornitore Fornitore
        {
            get
            {
                return fFornitore;
            }
            set
            {
                SetPropertyValue<Fornitore>("Fornitore", ref fFornitore, value);
            }
        }


        private FileData fInsDocum;
        [Persistent("INSERIMENTODOCUMENTO"),        DisplayName("Inserimento Documento")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData InsDocum
        {
            get
            {
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("InsDocum");
                //return fInsDocum;
            }
            set
            {
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("InsDocum", value);
               // SetPropertyValue<FileData>("InsDocum", ref fInsDocum, value);
            }
        }

        private double fImpTot;
        [Persistent("IMPORTOTOTALE"), DisplayName("Importo Totale")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")] 
        [MemberDesignTimeVisibility(false)]
        public double ImpTot
        {
            get
            {
                return fImpTot;
            }
            set
            {
                SetPropertyValue<double>("ImpTot", ref fImpTot, value);
            }
        }

        [PersistentAlias("ImpManodopera + ImpMateriale"), DisplayName("Imp. Totale")]
        [Appearance("ImportoTotale.BackColor.Red", BackColor = "Red", FontColor = "Black", Priority = 1, Criteria = "ImportoTotale = 0")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")] 
        public double ImportoTotale
        {
            get
            {
                var tempObject = EvaluateAlias("ImportoTotale");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

         private double fImpManodopera;
        [Persistent("IMPMANODOPERA"), DisplayName("Importo Manodopera")]
         [ModelDefault("DisplayFormat", "{0:C}")]
         [ModelDefault("EditMask", "C")] 
        [ImmediatePostData(true)]
        public double ImpManodopera
        {
            get
            {
                return fImpManodopera;
            }
            set
            {
                if (SetPropertyValue<double>("ImpManodopera", ref fImpManodopera, value))
                { OnChanged("ImportoTotale"); }
            }
        }


        private double fImpMateriale;
        [Persistent("IMPMATERIALE"), DisplayName("Importo Materiale")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")] 
        [ImmediatePostData(true)]
        public double ImpMateriale
        {
            get
            {
                return fImpMateriale;
            }
            set
            {
                if (SetPropertyValue<double>("ImpMateriale", ref fImpMateriale, value))
                { OnChanged("ImportoTotale"); }
            }
        }

        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
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
