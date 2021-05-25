using DevExpress.Persistent.Base;
using DevExpress.Xpo;


namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
    Persistent("RDLNOTATIPO")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "RdL Tipo Nota")]
    [ImageName("Time")]
    [NavigationItem("Tabelle Anagrafiche")]
    public class RdLNotaTipo : XPObject
    {
        public RdLNotaTipo()
            : base()
        {
        }
        public RdLNotaTipo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private string fDescrizione;
        [Size(25),
        Persistent("DESCRIZIONE"),
        DisplayName("Descrizione")]
        [DbType("varchar(25)")]
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

        private bool fIsCliente;
        [Persistent("ISCLIENTE"), DisplayName("Nota Del Cliente SI/NO")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [System.ComponentModel.Browsable(true)]
        [ExplicitLoading()]
        public bool IsCliente
        {
            get
            {
                return fIsCliente;
            }
            set
            {
                SetPropertyValue<bool>("IsCliente", ref fIsCliente, value);
            }
        }

        private bool fIsConRichiedente;
        [Persistent("ISCONRICHIEDENTE"), DisplayName("Nota con Richiedente SI/NO")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [System.ComponentModel.Browsable(true)]
        [ExplicitLoading()]
        public bool IsConRichiedente
        {
            get
            {
                return fIsConRichiedente;
            }
            set
            {
                SetPropertyValue<bool>("IsConRichiedente", ref fIsConRichiedente, value);
            }
        }

    }
}
