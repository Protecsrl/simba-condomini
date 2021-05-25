using CAMS.Module.Classi;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;

//namespace CAMS.Module.Costi
//{
//    class RegistroApprovazioneConsuntiviLavori
//    {
//    }
//}
namespace CAMS.Module.Costi
{
    [DefaultClassOptions, Persistent("REGAPPROVAZIONECONSUNTIVI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Approvazione Consuntivi Lavori")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [NavigationItem("Gestione Contabilità")]
    [ImageName("BO_Opportunity")]
    public class RegistroApprovazioneConsuntiviLavori : XPObject
    {
        public RegistroApprovazioneConsuntiviLavori()
            : base()
        {
        }

        public RegistroApprovazioneConsuntiviLavori(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private RegistroLavoriConsuntivi fRegistroLavoriConsuntivi;
        [Association(@"RegistroLavoriConsuntivi.RegistroApprovazioneConsuntiviLavori")]
        [Persistent("REGLAVORIPREVENTIVI"), DevExpress.ExpressApp.DC.XafDisplayName("Registro Consuntivi")]
        [ExplicitLoading()]
        public RegistroLavoriConsuntivi RegistroLavoriConsuntivi
        {
            get
            {
                return fRegistroLavoriConsuntivi;
            }
            set
            {
                SetPropertyValue<RegistroLavoriConsuntivi>("RegistroLavoriConsuntivi", ref fRegistroLavoriConsuntivi, value);
            }
        }

        private string fDescrizione;
        [Size(200), Persistent("DESCRIZIONE")]
        [DbType("varchar(200)")]
        [RuleRequiredField("RuleReq.RegistroApprovazioneConsuntiviLavori.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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


        [Persistent("DOCAPPROVAZIONE"), DevExpress.ExpressApp.DC.XafDisplayName("Documento di Approvazione")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData DocApprovazione
        {
            get
            {
                return GetDelayedPropertyValue<FileData>("DocApprovazione");
            }
            set
            {
                SetDelayedPropertyValue<FileData>("DocApprovazione", value);
            }
        }


        [Persistent("DOCAPPROVATO"), DevExpress.ExpressApp.DC.XafDisplayName("Documento Approvato")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData DocApprovato
        {
            get
            {
                return GetDelayedPropertyValue<FileData>("DocApprovato");
            }
            set
            {
                SetDelayedPropertyValue<FileData>("DocApprovato", value);
            }
        }
        [PersistentAlias("ImpManodopera + ImpMateriale"), DevExpress.ExpressApp.DC.XafDisplayName("Imp. Totale")]
        [Appearance("RegistroApprovazioneConsuntiviLavori.ImportoTotale.BackColor.Red", BackColor = "Red", FontColor = "Black", Priority = 1, Criteria = "ImportoTotale = 0")]
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
        [Persistent("IMPMANODOPERA"), DevExpress.ExpressApp.DC.XafDisplayName("Importo Manodopera")]
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
        [Persistent("IMPMATERIALE"), DevExpress.ExpressApp.DC.XafDisplayName("Importo Materiale")]
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

        private FlgApprovato fFlgApprovato;
        [Persistent("APPROVATO"), DevExpress.ExpressApp.DC.XafDisplayName("Approvazione")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        public FlgApprovato FlgApprovato
        {
            get
            {
                return fFlgApprovato;
            }
            set
            {
                SetPropertyValue<FlgApprovato>("FlgApprovato", ref fFlgApprovato, value);
            }
        }


        [Persistent("UTENTEAPPROVAZIONE"), Size(100), DevExpress.ExpressApp.DC.XafDisplayName("Utente Approvazione")]
        [DbType("varchar(100)")]
        [Delayed(true)]
        [VisibleInListViewAttribute(false), VisibleInDetailView(true)]
        public string UtenteApprovazione
        {
            get { return GetDelayedPropertyValue<string>("UtenteApprovazione"); }
            set { SetDelayedPropertyValue<string>("UtenteApprovazione", value); }
        }

  
        [Persistent("DATAAPPROVAZIONE"), DevExpress.ExpressApp.DC.XafDisplayName("Data Approvazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)] 
        [Delayed(true)]
        public DateTime DataApprovazione
        {
            get { return GetDelayedPropertyValue<DateTime>("DataApprovazione"); }
            set { SetDelayedPropertyValue<DateTime>("DataApprovazione", value); }
        }

    }
}


