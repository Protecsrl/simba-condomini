using CAMS.Module.DBTask;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;
namespace CAMS.Module.DBAngrafica
{
    [NavigationItem("Amministrazione")]
    [DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Tipo Azioni di Spedizione Mail")]
    [DefaultClassOptions, Persistent("TIPOAZIONISPEDIZIONEMAIL")]
    //[RuleCombinationOfPropertiesIsUnique("UniqrRubricaDestinatari", DefaultContexts.Save, "Email,Nome,Cognome,RoleAzione,SecurityRole", SkipNullOrEmptyValues = false)]
    [ImageName("NewContact")]

    public class TipoAzioniSpedizioneMail : XPObject
    {


        public TipoAzioniSpedizioneMail()
            : base()
        {
        }
        public TipoAzioniSpedizioneMail(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Size(60),
        Persistent("DESCRIZIONE"),
        XafDisplayName("Descrizione")]
        [DbType("varchar(60)")]
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
        private int fValore;
        [Persistent("VALORE"),
        XafDisplayName("Valore")]
        public int Valore
        {
            get
            {
                return fValore;
            }
            set
            {
                SetPropertyValue<int>("Valore", ref fValore, value);
            }
        }


        private StatoSmistamento fUltimoStatoSmistamento;
        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.DisplayName("Stato Smistamento")]
        //[RuleRequiredField("RRq.RdL.UltimoStatoSmistamento", DefaultContexts.Save, "La StatoSmistamento è un campo obbligatorio")]
        //[Appearance("RdL.UltimoStatoSmistamento.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(UltimoStatoSmistamento)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        //[Appearance("RdL.UltimoStatoSmistamento.Evidenza", AppearanceItemType.LayoutItem, "not(IsNullOrEmpty(UltimoStatoSmistamento))", FontStyle = FontStyle.Bold, BackColor = "Yellow", FontColor = "Black")]
        //[DataSourceCriteria("[<StatoSmistamentoCombo>][^.Oid == StatoSmistamentoxCombo.Oid And StatoSmistamento == '@This.old_SSmistamento']")]
        [ExplicitLoading()]
        public StatoSmistamento UltimoStatoSmistamento
        {
            get { return fUltimoStatoSmistamento; }
            set { SetPropertyValue<StatoSmistamento>("UltimoStatoSmistamento", ref fUltimoStatoSmistamento, value); }
        }

    }
}


