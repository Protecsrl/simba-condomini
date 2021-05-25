using System;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions,
    Persistent("RIFERIMENTINORMATIVI")]
    [NavigationItem("Procedure Attivita")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Riferimenti Normativi")]
    [ImageName("BO_Position")]
    [VisibleInDashboards(false)]
    public class RiferimentiNormativi : XPObject
    {
        public RiferimentiNormativi()
            : base()
        {
        }

        public RiferimentiNormativi(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private string fDescrizione;
        [Size(1000),
        Persistent("DESCRIZIONE"),
        DisplayName("Descrizione")]
        [DbType("varchar(1000)")]
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

        private string fCodNorma;
        [Size(1000),
        Persistent("COD_NORMA"),
        DisplayName("Codice Norma")]
        [DbType("varchar(1000)")]
        public string CodNorma
        {
            get
            {
                return fCodNorma;
            }
            set
            {
                SetPropertyValue<string>("CodNorma", ref fCodNorma, value);
            }
        }

        private FileData fFile;
        [Persistent("FILE"),        DisplayName("File")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData File
        {
            get
            {
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("File");
                //return fFile;
            }
            set
            {
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("File", value);
                // SetPropertyValue<FileData>("File", ref fFile, value);
            }
        }

        private string f_Utente;
        [Persistent("UTENTE"),
        Size(100),
        DisplayName("Utente")]
        [DbType("varchar(100)")]
        [System.ComponentModel.Browsable(false)]
       // [Appearance("RiferimentiNormativi.Utente", Enabled = false)]
        public string Utente
        {
            get
            {
                return f_Utente;
            }
            set
            {
                SetPropertyValue<string>("Utente", ref f_Utente, value);
            }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //[Appearance("RiferimentiNormativi.DataAggiornamento", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        public DateTime DataAggiornamento
        {
            get
            {
                return fDataAggiornamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value);
            }
        }
    }
}
