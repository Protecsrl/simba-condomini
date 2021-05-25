using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi


namespace CAMS.Module.DBKPI.Vista
{
    //class kpitecniciopdett
    //{
    //}
    [DefaultClassOptions, Persistent("V_KPITECNICIOPDETT")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettaglio Oper Tecnici")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem("KPI")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
    //[ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]



    public class kpitecniciopdett : XPLiteObject
    {
        public kpitecniciopdett() : base() { }
        public kpitecniciopdett(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private int fcodice;
        [Key, Persistent("CODICE"), System.ComponentModel.DisplayName("Codice")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]

        public int Codice
        {
            get
            {
                return fcodice;
            }
            set
            {
                SetPropertyValue<int>("Codice", ref fcodice, value);
            }
        }


        //CODICE	132354
        //TIPOREGRDL	TT
        //CODICEREGRDL	132354
        //UTENTEE	NannipieriC
        //DATAEMISSIONE	08/04/2019 15:49:24
        //DATACOMPLETAMENTO	09/04/2019 11:46:32
        //TEMPO	19:57:08
        //NREPISODI	1


        private string fTipoRegrdl;
        [Persistent("TIPOREGRDL"), System.ComponentModel.DisplayName("Tipo Registro Rdl")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(20)")]
        public string TipoRegrdl
        {
            get
            {
                return fTipoRegrdl;
            }
            set
            {
                SetPropertyValue<string>("TipoRegrdl", ref fTipoRegrdl, value);
            }
        }

        private string fUtente;
        [Persistent("UTENTEE"), System.ComponentModel.DisplayName("Utente")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(30)")]
        public string Utente
        {
            get
            {
                return fUtente;
            }
            set
            {
                SetPropertyValue<string>("Utente", ref fUtente, value);
            }
        }


        private DateTime fDataEmissione;
        [Persistent("DATAEMISSIONE"), System.ComponentModel.DisplayName("Data Emesso in lavorazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Emesso in lavorazione", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [System.ComponentModel.Browsable(false)]
        public DateTime DataEmissione
        {
            get
            {
                return fDataEmissione;
            }
            set
            {
                SetPropertyValue<DateTime>("DataEmissione", ref fDataEmissione, value);
            }
        }

        private DateTime fDataCompletamento;
        [Persistent("DATACOMPLETAMENTO"), System.ComponentModel.DisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Completamento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [System.ComponentModel.Browsable(false)]
        public DateTime DataCompletamento
        {
            get
            {
                return fDataCompletamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataCompletamento", ref fDataCompletamento, value);
            }
        }

        private string fTempo;
        [Persistent("TEMPO"), System.ComponentModel.DisplayName("Tempo")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(20)")]
        public string Tempo
        {
            get
            {
                return fTempo;
            }
            set
            {
                SetPropertyValue<string>("Tempo", ref fTempo, value);
            }
        }

        private int fNrEpisodi;
        [Persistent("NREPISODI"), System.ComponentModel.DisplayName("Nr Episodi")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        public int NrEpisodi
        {
            get
            {
                return fNrEpisodi;
            }
            set
            {
                SetPropertyValue<int>("NrEpisodi", ref fNrEpisodi, value);
            }
        }


    }

}



