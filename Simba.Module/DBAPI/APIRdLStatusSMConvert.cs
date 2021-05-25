


using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Drawing;

//namespace CAMS.Module.DBAPI
//{
//    class APIRdLStatusSMConvert
//    {
//    }
//}

namespace CAMS.Module.DBSpazi
{
    [DefaultClassOptions, Persistent("APIRDLSTATUSSMCONV")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "APIRdLStatusSMConvert")]
    [ImageName("Action_EditModel")]
    [NavigationItem("Amministrazione")]


    public class APIRdLStatusSMConvert : XPObject
    {
        public APIRdLStatusSMConvert() : base() { }
        public APIRdLStatusSMConvert(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

        private string fDescrizioneOut;
        [Persistent("DESCRIZIONE_OUT"), Size(250), DevExpress.Xpo.DisplayName("Descrizione Out")]
        [DbType("varchar(250)")]
        public string DescrizioneOut
        {
            get { return fDescrizioneOut; }
            set { SetPropertyValue<string>("DescrizioneOut", ref fDescrizioneOut, value); }
        }

        private string fCodDescrizioneOut;
        [Persistent("COD_DESCRIZIONE_OUT"), Size(100), DevExpress.Xpo.DisplayName("Cod Descrizione")]
        //[Appearance("Locali.CodDescrizione", Enabled = false)]
        [DbType("varchar(100)")]
        public string CodDescrizioneOut
        {
            get { return fCodDescrizioneOut; }
            set { SetPropertyValue<string>("CodDescrizioneOut", ref fCodDescrizioneOut, value); }
        }

        private string fCodOut;
        [Persistent("COD_OUT"), Size(50), DevExpress.Xpo.DisplayName("Cod Out")]
        [DbType("varchar(50)")]
        public string CodOut
        {
            get { return fCodOut; }
            set { SetPropertyValue<string>("CodOut", ref fCodOut, value); }
        }

        private Contratti fCommesse;
        [Persistent("CONTRATTO"), DevExpress.ExpressApp.DC.XafDisplayName("Contratto")]
        [Delayed(true)]
        public Contratti Commesse
        {
            get { return GetDelayedPropertyValue<Contratti>("Commesse"); }
            set { SetDelayedPropertyValue<Contratti>("Commesse", value); }
        }

        #region
        private StatoSmistamento fUltimoStatoSmistamento;
        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.DisplayName("Stato Smistamento")]
        public StatoSmistamento UltimoStatoSmistamento
        {
            get { return fUltimoStatoSmistamento; }
            set { SetPropertyValue<StatoSmistamento>("UltimoStatoSmistamento", ref fUltimoStatoSmistamento, value); }
        }

        private StatoOperativo fUltimoStatoOperativo;
        [Persistent("STATOOPERATIVO"), System.ComponentModel.DisplayName("Stato Operativo")]
        public StatoOperativo UltimoStatoOperativo
        {
            get { return fUltimoStatoOperativo; }
            set { SetPropertyValue<StatoOperativo>("UltimoStatoOperativo", ref fUltimoStatoOperativo, value); }
        }


        #endregion
        #region     ----------   DATE VALIDITA'   ------------------------------------------------------------------------------------

        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
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

        #endregion

    }
}

