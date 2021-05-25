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
using CAMS.Module.DBDocument;
using CAMS.Module.DBPlant;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System.Drawing;
using CAMS.Module.DBTask;
using CAMS.Module.DBALibrary;


//namespace CAMS.Module.DBAPI
//{
//    class APIRdLStatusCategoriaConvert
//    {
//    }
//}

namespace CAMS.Module.DBSpazi
{
    [DefaultClassOptions, Persistent("APIRDLSTATUSCATEGORIACONV")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "APIRdLStatus Categoria Convert")]
    [ImageName("Action_EditModel")]
    [NavigationItem("Amministrazione")]


    public class APIRdLStatusCategoriaConvert : XPObject
    {
        public APIRdLStatusCategoriaConvert() : base() { }
        public APIRdLStatusCategoriaConvert(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

        private string fDescrizioneOut;
        [Persistent("DESCRIZIONE_OUT"), Size(250), DevExpress.Xpo.DisplayName("Descrizione Out")]
        [DbType("varchar(250)")]
        public string DescrizioneOut
        {
            get { return fDescrizioneOut; }
            set { SetPropertyValue<string>("DescrizioneOut", ref fDescrizioneOut, value); }
        }

        private string fCodDescrizioneOut;
        [Persistent("COD_DESCRIZIONE_OUT"), Size(100), DevExpress.Xpo.DisplayName("Cod Descrizione")]
        //[Appearance("Locali.CodDescrizione", Enabled = false)]
        [DbType("varchar(100)")]
        public string CodDescrizioneOut
        {
            get { return fCodDescrizioneOut; }
            set { SetPropertyValue<string>("CodDescrizioneOut", ref fCodDescrizioneOut, value); }
        }

        private string fCodOut;
        [Persistent("COD_OUT"), Size(50), DevExpress.Xpo.DisplayName("Cod Out")]
        [DbType("varchar(50)")]
        public string CodOut
        {
            get { return fCodOut; }
            set { SetPropertyValue<string>("CodOut", ref fCodOut, value); }
        }

        private Contratti fCommesse;
        [Persistent("CONTRATTO"), DevExpress.ExpressApp.DC.XafDisplayName("Contratto")]
        [Delayed(true)]
        public Contratti Commesse
        {
            get { return GetDelayedPropertyValue<Contratti>("Commesse"); }
            set { SetDelayedPropertyValue<Contratti>("Commesse", value); }
        }

        #region
        private Categoria fCategoria;
        [Persistent("CATEGORIA"),
        DisplayName("Categoria Servizio")]
        [RuleRequiredField("RReqField.APISTATUSCATEGORIA CONVERT.Categoria", DefaultContexts.Save, "Categoria è un campo obbligatorio")]
        [ExplicitLoading()]
        public Categoria Categoria
        {
            get { return fCategoria; }
            set { SetPropertyValue<Categoria>("Categoria", ref fCategoria, value); }
        }


        #endregion
        #region     ----------   DATE VALIDITA'   ------------------------------------------------------------------------------------

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

        #endregion

    }
}

