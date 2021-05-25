using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

//namespace CAMS.Module.DBAux
//{
//    class tabimpfieldDet
//    {
//    }
//}
namespace CAMS.Module.DBAux
{
    [NavigationItem(false), System.ComponentModel.DisplayName("Migra")]
    [DefaultClassOptions, Persistent("TABIMPFIELD_DETT")]
    [VisibleInDashboards(false)]
    public class tabimpfieldDet : XPObject
    {
        public tabimpfieldDet()
            : base()
        {
        }
        public tabimpfieldDet(Session session)
            : base(session)
        {
        }

        private tabimpfield ftabimpfield;
        [Persistent("TABIMPFIELD"), DisplayName("TABIMPFIELD")]
        [Association(@"tabimpfields-tabimpfieldsdett")]
        public tabimpfield tabimpfield
        {
            get { return ftabimpfield; }
            set { SetPropertyValue<tabimpfield>("tabimpfield", ref ftabimpfield, value); }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy"), DevExpress.Xpo.DisplayName("DATAUPDATE")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [Delayed(true)]
        public DateTime DataAggiornamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAggiornamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataAggiornamento", value); }
        }

        [Persistent("CAMPINONIMPORTATI"), DevExpress.Xpo.DisplayName("CAMPINONIMPORTATI")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(4000)"), Size(4000)]
        [Delayed(true)]
        public string CampinonImportati
        {
            get { return GetDelayedPropertyValue<string>("CampinonImportati"); }
            set { SetDelayedPropertyValue<string>("CampinonImportati", value); }
        }

        [Persistent("CAMPIIMPORTATI"), DevExpress.Xpo.DisplayName("CAMPIIMPORTATI")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(4000)"), Size(4000)]
        [Delayed(true)]
        public string CampiImportati
        {
            get { return GetDelayedPropertyValue<string>("CampiImportati"); }
            set { SetDelayedPropertyValue<string>("CampiImportati", value); }
        }

        [Persistent("CLASSEEAMS"), DevExpress.Xpo.DisplayName("CLASSEEAMS")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(4000)"), Size(4000)]
        [Delayed(true)]
        public string ClasseEams
        {
            get { return GetDelayedPropertyValue<string>("ClasseEams"); }
            set { SetDelayedPropertyValue<string>("ClasseEams", value); }
        }

        private int FIndgiroord;
        [Persistent("INDGIROORD"), DisplayName("INDGIROORD")]
        public int Indgiroord
        {
            get
            {
                return FIndgiroord;
            }
            set
            {
                SetPropertyValue<int>("Indgiroord", ref FIndgiroord, value);
            }
        }
        [Persistent("FILTER"), DevExpress.Xpo.DisplayName("FILTER")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(4000)"), Size(4000)]
        [Delayed(true)]
        public string Filter
        {
            get { return GetDelayedPropertyValue<string>("Filter"); }
            set { SetDelayedPropertyValue<string>("Filter", value); }
        }

        [Persistent("CLASSEFILTRO"), DevExpress.Xpo.DisplayName("Classe di filtro")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(4000)"), Size(4000)]
        [Delayed(true)]
        public string ClassediFiltro
        {
            get { return GetDelayedPropertyValue<string>("ClassediFiltro"); }
            set { SetDelayedPropertyValue<string>("ClassediFiltro", value); }
        }

        [Persistent("NOTE"), DevExpress.Xpo.DisplayName("Note")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(4000)"), Size(4000)]
        [Delayed(true)]
        public string Note
        {
            get { return GetDelayedPropertyValue<string>("Note"); }
            set { SetDelayedPropertyValue<string>("Note", value); }
        }

        private int fTotRowSorgente;
        [Persistent("TOTROWSORGENTE"), DisplayName("TotRowSorgente")]
        public int TotRowSorgente
        {
            get
            {
                return fTotRowSorgente;
            }
            set
            {
                SetPropertyValue<int>("TotRowSorgente", ref fTotRowSorgente, value);
            }
        }

        private int fTotRowDestinazione;
        [Persistent("TOTROWDESTINAZIONE"), DisplayName("TotRowDestinazione")]
        public int TotRowDestinazione
        {
            get
            {
                return fTotRowDestinazione;
            }
            set
            {
                SetPropertyValue<int>("TotRowDestinazione", ref fTotRowDestinazione, value);
            }
        }

        private Guid fSessioneID;
        [Persistent("SESSIONE"), DisplayName("SessioneID")]
        public Guid SessioneID
        {
            get { return fSessioneID; }
            set { SetPropertyValue<Guid>("SessioneID", ref fSessioneID, value); }
        }

        private int fTempoEsecuzione;
        [Persistent("TEMPO"), DisplayName("Tempo Esecuzione")]
        public int TempoEsecuzione
        {
            get { return fTempoEsecuzione; }
            set { SetPropertyValue<int>("TempoEsecuzione", ref fTempoEsecuzione, value); }
        }

        [Persistent("DATAINIZIO")]
        [DevExpress.Xpo.DisplayName("DataInizio")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [Delayed(true)]
        public DateTime DataInizio
        {
            get { return GetDelayedPropertyValue<DateTime>("DataInizio"); }
            set { SetDelayedPropertyValue<DateTime>("DataInizio", value); }
        }
    }



}