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


using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
using System.Linq;
#pragma warning restore CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
using CAMS.Module.Classi;
using DevExpress.Persistent.BaseImpl;
 
namespace CAMS.Module.Costi
{
    [DefaultClassOptions, Persistent("REGISTROCEL")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro CEL")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    //[RuleCombinationOfPropertiesIsUnique("Unique.RegistroLavori", DefaultContexts.Save, "RegistroRdL;FondoLavori",
    //        CustomMessageTemplate = @"Attenzione! il Registro Costi deve essere univoco per RdL e Fondo Costi.",
    //SkipNullOrEmptyValues = false)]
    
    [ImageName("BO_Invoice")]
    [NavigationItem("Gestione Contabilità")]
    public class RegistroCEL : XPObject
    {
        public RegistroCEL()
            : base()
        {
        }

        public RegistroCEL(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        //kkk


        private string fDescrizione;
        [Size(500), Persistent("DESCRIZIONE")]
        [DbType("varchar(500)")]
        [RuleRequiredField("RuleReq.RegistroCEL.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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

        private Immobile fImmobile;
        [ Persistent("IMMOBILE"), DisplayName("Immobile")]
        [RuleRequiredField("RuleReq.RegistroCEL.Immobile", DefaultContexts.Save, "L'Immobile è un campo obbligatorio")]
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

        private StatoCEL fStatoCEL;
        [Persistent("STATOCEL"), DisplayName("Stato CEL")]
        [RuleRequiredField("RuleReq.RegistroCEL.StatoCEL", DefaultContexts.Save, "La Stato CEL è un campo obbligatorio")]
        public StatoCEL StatoCEL
        {
            get
            {
                return fStatoCEL;
            }
            set
            {
                SetPropertyValue<StatoCEL>("StatoCEL", ref fStatoCEL, value);
            }
        }


        private FileData fInsDocum;
        [Persistent("CELDOC"), DisplayName("Documento")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData InsDocum
        {
            get
            {
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("InsDocum");
            }
            set
            {
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("InsDocum", value);
            }
        }

        [Association(@"RegistroCEL.Dettaglio", typeof(RegistroCELDettaglio)), Aggregated,
     DisplayName("Registro CEL Dettaglio")]
        public XPCollection<RegistroCELDettaglio> RegistroCELDettaglios
        {
            get
            {
                return GetCollection<RegistroCELDettaglio>("RegistroCELDettaglios");
            }
        }
      

    }
}
