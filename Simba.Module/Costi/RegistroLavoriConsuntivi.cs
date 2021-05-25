using CAMS.Module.Classi;
using CAMS.Module.DBAngrafica;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
 
namespace CAMS.Module.Costi
{
    [DefaultClassOptions,    Persistent("REGLAVORICONSUNTIVI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Consuntivo Lavori")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [NavigationItem("Gestione Contabilità")]
    [ImageName("BO_Opportunity")]
    // regola di validazione in warning per il documento preventivo
    [RuleCriteria("RC.RegistroLavoriConsuntivi.Documento", DefaultContexts.Save, @"[InsDocum] Is Null",
    CustomMessageTemplate = "Attenzione, inserire il Documento di Consuntivo",
    SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Warning)]
    public class RegistroLavoriConsuntivi : XPObject
    {
        public RegistroLavoriConsuntivi()            : base()
        {
        }

        public RegistroLavoriConsuntivi(Session session)            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Size(200), Persistent("DESCRIZIONE")]
        [DbType("varchar(200)")]
        [RuleRequiredField("RuleReq.RegistroLavoriConsuntivi.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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

        private RegistroLavori fRegistroLavori;
        [Association(@"RegistroLavoriConsuntivi.RegistroLavori"),
        Persistent("REGISTROLAVORI"),
        DisplayName("Registro Lavori")]
        [ExplicitLoading()]
        public RegistroLavori RegistroLavori
        {
            get
            {
                return fRegistroLavori;
            }
            set
            {
                SetPropertyValue<RegistroLavori>("RegistroLavori", ref fRegistroLavori, value);
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

        private string fOpereCompiute;
        [Size(250), Persistent("OPERECOMPIUTE"), DisplayName("Opere Compiute")]
        [DbType("varchar(250)")]
       // [RuleRequiredField("RuleReq.RegistroLavoriConsuntivi.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string OpereCompiute
        {
            get
            {
                return fOpereCompiute;
            }
            set
            {
                SetPropertyValue<string>("OpereCompiute", ref fOpereCompiute, value);
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
        

        [Association(@"RegistroLavoriConsuntivi.RegistroApprovazioneConsuntiviLavori", typeof(RegistroApprovazioneConsuntiviLavori)), Aggregated]
        [DisplayName("Approvazioni")]
        public XPCollection<RegistroApprovazioneConsuntiviLavori> RegistroApprovazioneConsuntiviLavoris
        {
            get
            {
                return GetCollection<RegistroApprovazioneConsuntiviLavori>("RegistroApprovazioneConsuntiviLavoris");
            }
        }

    }
}
