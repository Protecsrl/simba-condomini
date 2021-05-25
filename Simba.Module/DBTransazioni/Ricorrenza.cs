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

//namespace CAMS.Module.DBTransazioni
//{
//    class Ricorrenza
//    {
//    }
//}

namespace CAMS.Module.DBTransazioni
{
    [DefaultClassOptions, Persistent("RICORRENZA")]
    [System.ComponentModel.DisplayName("Ricorrenza")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Ricorrenze")]
    [ImageName("GroupFieldCollection")]
    //[NavigationItem("Data Import")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    public class Ricorrenza : XPObject
    {
        public Ricorrenza() : base() { }

        public Ricorrenza(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }      

        private string fDescrizione;
        [Size(250), Persistent("DESCRIZIONE")]
        [DbType("varchar(250)")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        private int fValore;
        [ Persistent("VALORE")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public int Valore
        {
            get { return fValore; }
            set { SetPropertyValue<int>("Valore", ref fValore, value); }
        }
        private TipoRicorrenze fTipoRicorrenze;
        [Persistent("TIPORICORRENZA")]
        public TipoRicorrenze TipoRicorrenze
        {
            get { return fTipoRicorrenze; }
            set { SetPropertyValue<TipoRicorrenze>("TipoRicorrenze", ref fTipoRicorrenze, value); }
        }


    }
}
