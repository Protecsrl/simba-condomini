
using CAMS.Module.Classi;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Drawing;

namespace CAMS.Module.DBAux
{
    [DefaultClassOptions, Persistent("IMPORTFILEDETT")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettaglio File")]
    [ImageName("BO_Organization")]
    #region Abilitazione
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("ImportFileDettaglio_SoloAttivo", "Abilitato == 1", "Attivi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("ImportFileDettaglio_SoloNonAttivo", "Abilitato == 0", "non Attivi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("ImportFileDettaglio_Tutto", "", "Tutto", Index = 2)]
    #endregion
    [NavigationItem("Censimento")]
    [VisibleInDashboards(false)]
    public class ImportFileDettaglio : XPObject
    {
        private const string NA = "N/A";
        private const string FormattazioneCodice = "{0:000}";
                
        public ImportFileDettaglio() : base() { }
        public ImportFileDettaglio(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {
                this.Abilitato = FlgAbilitato.Si;
            }
        }

        //----------------------------
        //private ImportFileDettaglio fImportFileDettaglio;
        [Persistent("IMPORTFILE"), DevExpress.ExpressApp.DC.XafDisplayName("ImportFileDettaglio")]
        [Association(@"ImportFile-ImportFileDettaglio")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        [Delayed(true)]
        public ImportFile ImportFile
        {
            get { return GetDelayedPropertyValue<ImportFile>("ImportFile"); }
            set { SetDelayedPropertyValue<ImportFile>("ImportFile", value); }
            //get
            //{
            //    return ImportFileDettaglio;
            //}
            //set
            //{
            //    SetPropertyValue<ImportFileDettaglio>("ImportFileDettaglio", ref ImportFileDettaglio, value);
            //}
        }

        [Persistent("COLONNA"), DevExpress.Xpo.DisplayName("Colonna Dati")]
        //[ValueConverter(typeof(TypeToStringConverter)), ImmediatePostData]
        //[TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        [DbType("varchar(4000)"), Size(4000)]
        public string Colonna
        {
            get { return GetPropertyValue<string>("Colonna"); }
            set { SetPropertyValue<string>("Colonna", value); }
        }
        [Persistent("RIF"), DevExpress.Xpo.DisplayName("riferimento Dato")]
        //[ValueConverter(typeof(TypeToStringConverter)), ImmediatePostData]
        //[TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        [DbType("varchar(40)"), Size(4000)]
        public string Riferimento
        {
            get { return GetPropertyValue<string>("Riferimento"); }
            set { SetPropertyValue<string>("Riferimento", value); }
        }
        [Persistent("OBJECTTYPE"), DevExpress.Xpo.DisplayName("Tipo Dati")]
        //[ValueConverter(typeof(TypeToStringConverter)), ImmediatePostData]
        //[TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        [DbType("varchar(4000)"), Size(4000)]
        public string ObjectType
        {
            get { return GetPropertyValue<string>("ObjectType"); }
            set { SetPropertyValue<string>("ObjectType", value); }
        }

        private string fDescrizione;
        [Size(4000), Persistent("DESCRIZIONE"), DevExpress.Xpo.DisplayName("Descrizione")]
        [DbType("varchar(4000)")]
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

        private DateTime fDataAggiornamento;
        [Persistent("DATAAGGIORNAMENTO")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy"), DevExpress.Xpo.DisplayName("Data Aggiornamento")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [Delayed(true)]
        public DateTime DataAggiornamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAggiornamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataAggiornamento", value); }
        }

        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        [ImmediatePostData(true)]
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


        /// <summary>
        /// //////////////////
        /// </summary>

        
    }
}
