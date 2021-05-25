using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



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
#pragma warning disable CS0105 // La direttiva using per 'System.Threading.Tasks' è già presente in questo spazio dei nomi
using System.Threading.Tasks;
#pragma warning restore CS0105 // La direttiva using per 'System.Threading.Tasks' è già presente in questo spazio dei nomi




using CAMS.Module.Classi;
using CAMS.Module.DBAngrafica;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;


//namespace CAMS.Module.Costi
//{
//    class RegistroApprovazionePreventiviLavori
//    {
//    }
//}

namespace CAMS.Module.Costi
{
    [DefaultClassOptions, Persistent("REGAPPROVAZIONEPREVENTIVILAV")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Approvazione Preventivo Lavori")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [NavigationItem("Gestione Contabilità")]
    [ImageName("BO_Opportunity")]
    public class RegistroApprovazionePreventiviLavori : XPObject
    {
        public RegistroApprovazionePreventiviLavori()
            : base()
        {
        }

        public RegistroApprovazionePreventiviLavori(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";
        private RegistroLavoriPreventivi fRegistroLavoriPreventivi;
        [Association(@"RegistroLavoriPreventivi.RegistroApprovazionePreventiviLavori")]
        [Persistent("REGLAVORIPREVENTIVI"),     DisplayName("Registro Preventivi")]
        [ExplicitLoading()]
        public RegistroLavoriPreventivi RegistroLavoriPreventivi
        {
            get
            {
                return fRegistroLavoriPreventivi;
            }
            set
            {
                SetPropertyValue<RegistroLavoriPreventivi>("RegistroLavoriPreventivi", ref fRegistroLavoriPreventivi, value);
            }
        }

        private string fDescrizione;
        [Size(200), Persistent("DESCRIZIONE")]
        [DbType("varchar(200)")]
        [RuleRequiredField("RuleReq.RegistroApprovazionePreventiviLavori.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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
           
     
        [Persistent("DOCAPPROVAZIONE"), DisplayName("Documento di Approvazione")]
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
                SetDelayedPropertyValue< FileData>("DocApprovazione", value);
            }
        }


        [Persistent("DOCAPPROVATO"), DisplayName("Documento Approvato")]
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
        [PersistentAlias("ImpManodopera + ImpMateriale"), DisplayName("Imp. Totale")]
        [Appearance("RegistroLavoriPreventivi.ImportoTotale.BackColor.Red", BackColor = "Red", FontColor = "Black", Priority = 1, Criteria = "ImportoTotale = 0")]
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

        private FlgApprovato fFlgApprovato;
        [Persistent("APPROVATO"), DisplayName("Approvazione")]
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


