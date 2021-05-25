 
using System;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions,    Persistent("SCHEDEMPOWNER")]
    [NavigationItem("Procedure Attivita")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Procedure MP Owner")]
    [ImageName("BO_Lead")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    public class SchedeMPOwner : XPObject
    {
        public SchedeMPOwner()
            : base()
        {
        }

        public SchedeMPOwner(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private string fDescrizione;
        [Size(250),
        Persistent("DESCRIZIONE"),
        DisplayName("Descrizione")]
        [DbType("varchar(250)")]
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

        private bool fIsAdministrator;
        [Size(5),Persistent("ISADMINISTRATOR"), DisplayName("è Amministratore")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool IsAdministrator
        {
            get
            {
                return fIsAdministrator;
            }
            set
            {
                SetPropertyValue<bool>("IsAdministrator", ref fIsAdministrator, value);
            }
        }

        #region utente e data aggiornamento
        private string f_Utente;
        [Persistent("UTENTE"),
        Size(100),
        DisplayName("Utente")]
        [DbType("varchar(100)")]
      //  [Appearance("SchedeMPOwner.Utente", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
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
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[Appearance("SchedeMPOwner.DataAggiornamento", Enabled = false)]
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
        #endregion

        //public override string ToString()
        //{
        //    return string.Format("{0}", this.Descrizione);
        //}


    }
}
