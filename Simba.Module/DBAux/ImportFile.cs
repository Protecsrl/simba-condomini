using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMS.Module.Classi;
using DevExpress.ExpressApp.Model;

using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Drawing;
using DevExpress.Persistent.BaseImpl;
using System.Data;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp;
//namespace CAMS.Module.DBAux
//{
//    class ImportFile
//    {
//    }
//}
namespace CAMS.Module.DBAux
{
    [DefaultClassOptions, Persistent("IMPORTFILE")]
    [System.ComponentModel.DefaultProperty("FullName")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Import File")]
    [ImageName("BO_Organization")]
    //[DeferredDeletion(true)]
    //[RuleCombinationOfPropertiesIsUnique("Unique.CENSIMENTOBASE.Descrizione", DefaultContexts.Save, "Commesse,CodDescrizione, Descrizione")]
    //[RuleCriteria("RuleWarning.Ed.Indirizzo.Geo.nonPre", DefaultContexts.Save, @"IndirizzoGeoValorizzato > 0",
    //  CustomMessageTemplate =
    //  "Attenzione:La L'indirizzo Selezionato non ha riferimenti di Georeferenziazione, \n\r inserirli prima di continuare!{IndirizzoGeoValorizzato}",
    //   SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Warning)]
    #region Abilitazione

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("ImportFile_SoloAttivo", "Abilitato == 1", "Attivi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("ImportFile_SoloNonAttivo", "Abilitato == 0", "non Attivi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("ImportFile_Tutto", "", "Tutto", Index = 2)]

    #endregion
    [NavigationItem("Censimento")]
    [VisibleInDashboards(false)]
    public class ImportFile : XPObject
    {
        private const string NA = "N/A";
        private const string FormattazioneCodice = "{0:000}";
        public ImportFile() : base() { }
        public ImportFile(Session session) : base(session) { }
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1) { this.Abilitato = FlgAbilitato.Si; }
        }

        //----------------------------


        private int foidFile;
        [Persistent("OIDFILE"), Size(100), DevExpress.Xpo.DisplayName("Oid Commessa")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [Delayed(true)]
        public int oidFile
        {
            get { return GetDelayedPropertyValue<int>("oidFile"); }
            set { SetDelayedPropertyValue<int>("oidFile", value); }
        }


        [Persistent("FULLNAME"), DevExpress.Xpo.DisplayName("FullName")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(4000)"), Size(4000)]
        [Delayed(true)]
        public string FullName
        {
            get { return GetDelayedPropertyValue<string>("FullName"); }
            set { SetDelayedPropertyValue<string>("FullName", value); }
        }

        [Persistent("NAME"), DevExpress.Xpo.DisplayName("Name")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(250)"), Size(250)]
        [Delayed(true)]
        public string Name
        {
            get { return GetDelayedPropertyValue<string>("Name"); }
            set { SetDelayedPropertyValue<string>("Name", value); }
        }


        [Persistent("ESTENSIONE"), DevExpress.Xpo.DisplayName("Estensione")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(10)"), Size(10)]
        [Delayed(true)]
        public string Estensione
        {
            get { return GetDelayedPropertyValue<string>("Estensione"); }
            set { SetDelayedPropertyValue<string>("Estensione", value); }
        }


        [Persistent("DIMENSIONE"), DevExpress.Xpo.DisplayName("Dimensione")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(20)"), Size(20)]
        [Delayed(true)]
        public string Dimensione
        {
            get { return GetDelayedPropertyValue<string>("Dimensione"); }
            set { SetDelayedPropertyValue<string>("Dimensione", value); }
        }

        [Persistent("ULTIMOACCESSO"), DevExpress.Xpo.DisplayName("UltimoAccesso")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(25)"), Size(25)]
        [Delayed(true)]
        public string UltimoAccesso
        {
            get { return GetDelayedPropertyValue<string>("UltimoAccesso"); }
            set { SetDelayedPropertyValue<string>("UltimoAccesso", value); }
        }

        [Persistent("FILE"), DisplayName("File")]
        //[FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        //[FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        //[ExplicitLoading()]
        [Delayed(true)]
        public FileData File
        {
            get
            {
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("File");
            }
            set
            {
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("File", value);
            }
        }

        [Persistent("ICONA"), DisplayName("Icona")]
        [DevExpress.Xpo.Size(SizeAttribute.Unlimited)]
        [VisibleInListViewAttribute(true)]
        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit,
            ListViewImageEditorCustomHeight = 140, DetailViewImageEditorFixedHeight = 360)]
        [Delayed(true)]
        public byte[] Icona // public Image Icona
        {
            //get            //{            //    return GetDelayedPropertyValue<Image>("Icona");            //}
            //set            //{            //    SetDelayedPropertyValue<Image>("Icona", value);            //}
            get { return GetDelayedPropertyValue<byte[]>("Icona"); }
            set { SetDelayedPropertyValue<byte[]>("Icona", value); }
        }



        [Persistent("ESITO"), DevExpress.Xpo.DisplayName("esito")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(25)"), Size(25)]
        [Delayed(true)]
        public string Esito
        {
            get { return GetDelayedPropertyValue<string>("Esito"); }
            set { SetDelayedPropertyValue<string>("Esito", value); }
        }


        [Association(@"ImportFile-ImportFileDettaglio", typeof(ImportFileDettaglio)), DevExpress.Xpo.Aggregated]
        [DevExpress.Xpo.DisplayName("Parametri Associati")]
        public XPCollection<ImportFileDettaglio> ImportFileDettaglios
        {
            get
            {
                return GetCollection<ImportFileDettaglio>("ImportFileDettaglios");
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



    }
}
