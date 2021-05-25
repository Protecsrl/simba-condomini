using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using DevExpress.ExpressApp.ConditionalAppearance;
// 
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

using System.ComponentModel;
using CAMS.Module.PropertyEditors;
using DevExpress.Persistent.BaseImpl;
using CAMS.Module.DBPlant;

using CAMS.Module.DBTask.MP;
using System.Drawing;

using DevExpress.Persistent.Validation;

using CAMS.Module.Classi;
using CAMS.Module.DBAngrafica;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;


namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("REGREPORTMPDETT")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Report MP File")]
    [ImageName("Apparato")]
    [NavigationItem(false)]
    public class RegistroReportMPDett : XPObject
    {
        public RegistroReportMPDett()
     : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.

        }

        public RegistroReportMPDett(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.

        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        

        private RegistroReportMP fRegistroReportMP;
        [Persistent("REGISTROREPORTMP"), Association(@"RegistroReportMP_File"), System.ComponentModel.DisplayName("Registro Report MP")]             
        [ExplicitLoading()]
        public RegistroReportMP RegoleAutoAssegnazioneRdL
        {
            get
            {
                return fRegistroReportMP;
            }
            set
            {
                SetPropertyValue<RegistroReportMP>("RegistroReportMP", ref fRegistroReportMP, value);
            }
        }

        [Persistent("FILEREPORT")]
        [DevExpress.ExpressApp.DC.XafDisplayName("File Report")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.xls", "*.xlsx")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [VisibleInListView(false)]
        [Delayed(true)]
        public FileData FileReportMP
        {
            get
            {
                return GetDelayedPropertyValue<FileData>("FileReportMP");
            }
            set
            {
                SetDelayedPropertyValue<FileData>("FileReportMP", value);
            }
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private DateTime fDataElaborazioneReportMP;
        [Persistent("DATAELABORAZIONEREPORT")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Data Elaborazione Report MP")]
        [VisibleInListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [Appearance("RegistroReportMPDett.Abilita.DataElaborazioneReportMP", FontColor = "Black", Enabled = false)]
        public DateTime DataElaborazioneReportMP
        {
            get
            {
                return fDataElaborazioneReportMP;
            }
            set
            {
                SetPropertyValue<DateTime>("DataElaborazioneReportMP", ref fDataElaborazioneReportMP, value);
            }
        }

    }
}



