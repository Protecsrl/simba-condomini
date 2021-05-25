using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using DevExpress.Xpo;
#pragma warning disable CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
using System.Collections.Generic;
#pragma warning restore CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
using DevExpress.Persistent.Base;

namespace CAMS.Module.DBAngrafica
{
    [DefaultClassOptions, Persistent("SYSTEMLOG")]
    [NavigationItem(true)]
    //[System.ComponentModel.DefaultProperty("Log di Sistema")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Log di Sistema")]
    [ImageName("ShowTestReport")]
    public class LogSystemTrace : XPObject
    {

        public LogSystemTrace()
            : base()
        {
        }

        public LogSystemTrace(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (Oid == -1)
                DataAggiornamento = DateTime.MinValue;
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private string f_Utente;
        [Persistent("UTENTE"),
        Size(100),
        DisplayName("Utente")]
        [DbType("varchar(100)")]
        //[System.ComponentModel.Browsable(false)]
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


        private string fDescrizione;
        [Persistent("DESCRIZIONE"),
        Size(4000)]
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

        //private string fCorpo;
        //[Persistent("URL"), DisplayName("Testo URL")]
        //[Size(SizeAttribute.Unlimited)]
        //[DbType("CLOB")]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //public string Corpo
        //{
        //    get
        //    {
        //        return fCorpo;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Corpo", ref fCorpo, value);
        //    }
        //}

        private string fCorpo;
        [Persistent("BROWSER"), DisplayName("Testo BROWSER")]
        [Size(4000)]
        [DbType("varchar(4000)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Corpo
        {
            get
            {
                return fCorpo;
            }
            set
            {
                SetPropertyValue<string>("Corpo", ref fCorpo, value);
            }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        [System.ComponentModel.Browsable(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
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

        private string fSessioneWEB;
        [Persistent("SESSIONEWEB"),
        Size(100),
        DisplayName("Sessione WEB")]
        [DbType("varchar(100)")]
        //[System.ComponentModel.Browsable(false)]
        public string SessioneWEB
        {
            get
            {
                return fSessioneWEB;
            }
            set
            {
                SetPropertyValue<string>("SessioneWEB", ref fSessioneWEB, value);
            }
        }

    }
}



