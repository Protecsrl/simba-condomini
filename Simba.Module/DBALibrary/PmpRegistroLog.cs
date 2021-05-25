using System;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

using CAMS.Module.Classi;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions,
    Persistent("SCHEDAMPREGLOG")]
    [NavigationItem("Procedure Attivita")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Modifiche Schede MP")]
    [VisibleInDashboards(false)]
    public class PmpRegistroLog : XPObject
    {
        public PmpRegistroLog()
            : base()
        {
        }

        public PmpRegistroLog(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private SchedaMp fSchedaMP;
        [Persistent("SCHEDAMP")]
        [ExplicitLoading()]
        public SchedaMp SchedaMp
        {
            get
            {
                return fSchedaMP;
            }
            set
            {
                SetPropertyValue<SchedaMp>("SchedaMp", ref fSchedaMP, value);
            }
        }


        private TipoModifichePMP fStatoModifica;
        [Persistent("TIPO_MODIFICA")]
        public TipoModifichePMP StatoModifica
        {
            get
            {
                return fStatoModifica;
            }
            set
            {
                SetPropertyValue<TipoModifichePMP>("StatoModifica", ref fStatoModifica, value);
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
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
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

        [Association("PMPRegistroLog-PMPDettaglioLog"),     Aggregated]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<PmpDettLog> PmpDettaglioLog
        {
            get
            {
                return GetCollection<PmpDettLog>("PmpDettaglioLog");
            }
        }
    }
}
