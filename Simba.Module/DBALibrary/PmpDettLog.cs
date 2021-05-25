using System;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

using CAMS.Module.Classi;

namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions,
    Persistent("SCHEDEMPLOGDETT")]
    [VisibleInDashboards(false)]
    [NavigationItem("Procedure Attivita")]
    [System.ComponentModel.DefaultProperty("Pmp")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettaglio Registro Modifiche Schede MP")]
     public class PmpDettLog : XPObject
    {
        public PmpDettLog()
            : base()
        {
        }

        public PmpDettLog(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private SchedaMp fpmp;
        [Persistent("SCHEDAMP")]
        public SchedaMp Pmp
        {
            get
            {
                return fpmp;
            }
            set
            {
                SetPropertyValue<SchedaMp>("SchedaMp", ref fpmp, value);
            }
        }


        private TipoModifica fTipoModifica;
        [Persistent("TIPO_MODIFICA")]
        public TipoModifica TipoModifica
        {
            get
            {
                return fTipoModifica;
            }
            set
            {
                SetPropertyValue<TipoModifica>("TipoModifica", ref fTipoModifica, value);
            }
        }

        private string fDettaglio;
        [Persistent("DETTAGLIO")]
        [DbType("varchar(100)")]
        public string Dettaglio
        {
            get
            {
                return fDettaglio;
            }
            set
            {
                SetPropertyValue<string>("Dettaglio", ref fDettaglio, value);
            }
        }

        private string fUserName;
        [Persistent("USERNAME")]
        [DbType("varchar(150)")]
        public string UserName
        {
            get
            {
                return fUserName;
            }
            set
            {
                SetPropertyValue<string>("UserName", ref fUserName, value);
            }
        }

        private DateTime fDataModifica;
        [Persistent("DATA_MODIFICA")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd.MM.yyyy H:mm:ss tt")]
        public DateTime DataModifica
        {
            get
            {
                return fDataModifica;
            }
            set
            {
                SetPropertyValue<DateTime>("DataModifica", ref fDataModifica, value);
            }
        }

        private FileData fFileMail;
        [Persistent("FILEMAIL")]
        public FileData FileMail
        {
            get
            {
                return fFileMail;
            }
            set
            {
                SetPropertyValue<FileData>("FileMail", ref fFileMail, value);
            }
        }


        private PmpRegistroLog fpmpregistrolog;
        [Persistent("OIDREGISTROLOG"),
        Association("PMPRegistroLog-PMPDettaglioLog")]
        public PmpRegistroLog PmpRegistroLog
        {
            get
            {
                return fpmpregistrolog;
            }
            set
            {
                SetPropertyValue("PmpRegistroLog", ref fpmpregistrolog, value);
            }
        }
    }
}
