using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
    Persistent("STATOOPERATIVO")]
    [System.ComponentModel.DefaultProperty("CodStato")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Stato Operativo")]
    [ImageName("ShowWorkTimeOnly")]
    [NavigationItem("Ticket")]
    public class StatoOperativo : XPObject
    {
        public StatoOperativo()
            : base()
        {
        }
        public StatoOperativo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fCodStato;
        [Persistent("CODSTATO"),
        Size(60)]
        [DbType("varchar(60)")]
        [RuleRequiredField("RReqField.StatoOperativo.CodStato", DefaultContexts.Save, "Il Codice Stato Operativo è un campo obbligatorio")]
        public string CodStato
        {
            get
            {
                return fCodStato;
            }
            set
            {
                SetPropertyValue<string>("CodStato", ref fCodStato, value);
            }
        }

        private string fStatoOperativo;
        [Size(30),        Persistent("STATOOPERATIVO"),        DisplayName("Stato Operativo")]
        [DbType("varchar(30)")]
        public string Stato
        {
            get
            {
                return fStatoOperativo;
            }
            set
            {
                SetPropertyValue<string>("StatoOperativo", ref fStatoOperativo, value);
            }
        }
        private FaseOperativa fFaseOperativa;
        [Persistent("FASEOPERATIVA"),        DisplayName("Fase Operativa")]
        public FaseOperativa FaseOperativa
        {
            get
            {
                return fFaseOperativa;
            }
            set
            {
                SetPropertyValue<FaseOperativa>("FaseOperativa", ref fFaseOperativa, value);
            }
        }


        private int fOrdine;
        [Persistent("ORDINE"), DisplayName("Ordine"), MemberDesignTimeVisibility(false)]
        public int Ordine
        {
            get
            {
                return fOrdine;
            }
            set
            {
                SetPropertyValue<int>("Ordine", ref fOrdine, value);
            }
        }


    }
}
