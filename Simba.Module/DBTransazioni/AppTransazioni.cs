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
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;


namespace CAMS.Module.DBTransazioni
{
    [DefaultClassOptions, Persistent("TRANSAZIONIAPP")]
    [System.ComponentModel.DisplayName("Applicazioni Transazioni")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Applicazioni Transazioni")]
    [ImageName("GroupFieldCollection")]
    //[NavigationItem("Data Import")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    public class AppTransazioni : XPObject
    {
        public AppTransazioni() : base() { }

        public AppTransazioni(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        private string fNomeApp;
        [Size(200), Persistent("NOMEAPP")]
        [DbType("varchar(200)")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string NomeApp
        {
            get { return fNomeApp; }
            set { SetPropertyValue<string>("NomeApp", ref fNomeApp, value); }
        }

        private string fDescrizione;
        [Size(1000), Persistent("DESCRIZIONE")]
        [DbType("varchar(1000)")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        [Association(@"AppTransazioniInstallazioni.AppTransazioni", typeof(AppTransazioniInstallazioni)), Aggregated]
        [DevExpress.ExpressApp.DC.XafDisplayName("Dettaglio")]
        public XPCollection<AppTransazioniInstallazioni> AppTransazioniInstallazionis
        {
            get
            {
                return GetCollection<AppTransazioniInstallazioni>("AppTransazioniInstallazionis");
            }
        }

    }
}
