using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Drawing;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
    Persistent("STATOAUTORIZZATIVO")]
    [VisibleInDashboards(false)]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Stato Autorizzativo")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [ImageName("Demo_ListEditors_Scheduler_Filter")]
    [NavigationItem("Ticket")]
    public class StatoAutorizzativo : XPObject
    {
        public StatoAutorizzativo()
            : base()
        {
        }
        public StatoAutorizzativo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private string fDescrizione;
        [Size(30), Persistent("STATOAUTORIZZATIVO"), DisplayName("Stato Autorizzativo")]
        [DbType("varchar(30)")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        private string fFase;
        [Persistent("FASE"),
        DisplayName("Stato Fase")]
        public string Fase
        {
            get { return fFase; }
            set { SetPropertyValue<string>("Fase", ref fFase, value); }
        }

        private int fOrdine;
        [Persistent("ORDINE"), DisplayName("Ordine"), MemberDesignTimeVisibility(false)]
        public int Ordine
        {
            get { return fOrdine; }
            set { SetPropertyValue<int>("Ordine", ref fOrdine, value); }
        }

        private string fEscalation;
        [Size(150), Persistent("ESCALATIONSTATO"), DisplayName("Escalation Stato")]
        [DbType("varchar(150)")]
        public string Escalation
        {
            get { return fEscalation; }
            set { SetPropertyValue<string>("Escalation", ref fEscalation, value); }
        }

        private string fMessaggioInformativo;
        [Size(500), Persistent("MESSAGGIOINFORMATIVO"), DisplayName("Messaggio Informativo")]
        [DbType("varchar(500)")]
        public string MessaggioInformativo
        {
            get { return fMessaggioInformativo; }
            set { SetPropertyValue<string>("MessaggioInformativo", ref fMessaggioInformativo, value); }
        }

    }
}
