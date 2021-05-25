using CAMS.Module.Classi;
using CAMS.Module.DBALibrary;
using DevExpress.ExpressApp;
//using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;

namespace CAMS.Module.DBDataImport
{
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    [DefaultClassOptions, Persistent("REGDTIMPORT")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Data Import")]
    [DefaultProperty("Descrizione")]
    [ImageName("BO_Invoice")]
    [NavigationItem("Data Import")]
    [VisibleInDashboards(false)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class RegistroDataImport : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).

        public RegistroDataImport()
            : base()
        {
        }

        public RegistroDataImport(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        //kkk

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(100), DevExpress.Xpo.DisplayName("Descrizione")]
        [DbType("varchar(250)")]
        [RuleRequiredField("RReqField.RegistroDataImport.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }


        [Association(@"RegistroDataImport.RegistroDataImportTentativi", typeof(RegistroDataImportTentativi)), Aggregated]
        [DevExpress.ExpressApp.DC.XafDisplayName("Elenco Tentativi")]
        [ToolTip("Elenco Tentativi")]
        public XPCollection<RegistroDataImportTentativi> RegistroDataImportTentativis
        {
            get
            {
                return GetCollection<RegistroDataImportTentativi>("RegistroDataImportTentativis");
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


        // private int fOID;
        // [Persistent("REGDATAIMPORT"), Size(100), DevExpress.Xpo.DisplayName("OID")]
        //// [RuleRequiredField("RReqField.RegistroDataImport.OID", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        // public int OID
        // {
        //     get { return fOID; }
        //     set { SetPropertyValue<int>("OID", ref fOID, value); }
        // }	
        private string fDescImport;
        [Persistent("DESCRIZIONEIMPORT"), Size(250), DevExpress.Xpo.DisplayName("DescrizioneImport")]
        [DbType("varchar(250)")]
        [RuleRequiredField("RReqField.RegistroDataImport.DescrizioneImport", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string DescImport
        {
            get { return fDescImport; }
            set { SetPropertyValue<string>("DescrizioneImport", ref fDescImport, value); }
        }


        private string fCommessa;
        [Persistent("DESCCOMMESSA"), Size(250), DevExpress.Xpo.DisplayName("Commessa")]
        [DbType("varchar(250)")]
        [RuleRequiredField("RReqField.RegistroDataImport.Commessa", DefaultContexts.Save, "La Descrizione della Commessa da importareè un campo obbligatorio")]
        public string Commessa
        {
            get { return fCommessa; }
            set { SetPropertyValue<string>("Commessa", ref fCommessa, value); }
        }




        private FileData fFileImpDataTipizzato;
        [Persistent("FILEIMPORTDATATIPIZZATO"), DevExpress.Xpo.DisplayName("FileImportDataTipizzato")]
        [Delayed(true)]
        public FileData FileImportDataTipizzato
        {
            get { return GetDelayedPropertyValue<FileData>("FileImportDataTipizzato"); }
            set { SetDelayedPropertyValue<FileData>("FileImportDataTipizzato", value); }

        }

        [Persistent("SISTEMA"), DevExpress.Xpo.DisplayName("Sistema")]
        [Delayed(true)]
        public Sistema Sistema
        {
            get { return GetDelayedPropertyValue<Sistema>("Sistema"); }
            set { SetDelayedPropertyValue<Sistema>("Sistema", value); }

        }


        [Persistent("DATAIMPORTMAPPA"), DevExpress.Xpo.DisplayName("DataImportMappa")]
        [Delayed(true)]
        public DataImportMappa DataImportMappa
        {
            get { return GetDelayedPropertyValue<DataImportMappa>("DataImportMappa"); }
            set { SetDelayedPropertyValue<DataImportMappa>("DataImportMappa", value); }
        }



    }
}