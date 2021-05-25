using System;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBPlant
{
    [System.ComponentModel.DisplayName("Carico Impianto per Apparato")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Carico Impianto per Apparato")]
    [DefaultClassOptions,    Persistent("V_IMPIANTO_APPARATO")]
    [NavigationItem(false)]

    public class ServizioApparatoCarico : XPLiteObject
    {
        public ServizioApparatoCarico()
            : base()
        {
        }

        public ServizioApparatoCarico(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private int fcodice;
        [Key,
        Persistent("CODICE"),
        MemberDesignTimeVisibility(false)]
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


        private Servizio fServizio;
        [MemberDesignTimeVisibility(false),
        Persistent("SERVIZIO"),
        DisplayName("Servizio")]
        //[Association(@"Impianti_Apparato_Carico")]
        public Servizio Servizio
        {
            get
            {
                return fServizio;
            }
            set
            {
                SetPropertyValue("Servizio", ref fServizio, value);
            }
        }


        private int fCountApp;
        [Persistent("COUNTAPPARATI"),
        DisplayName("Conta Apparati (min)")]
        [Appearance("ImpiantoApparatoCarico.CountApp", Enabled = false)]
        public int CountApp
        {
            get
            {
                return fCountApp;
            }
            set
            {
                SetPropertyValue<int>("CountApp", ref fCountApp, value);
            }
        }

        private int fSumTempoSchedeMp;
        [Persistent("SUMTEMPOSCHEDEMP"),
        DisplayName("Somma Tempo SchedeMP (min)"),
        MemberDesignTimeVisibility(false)]
        [Appearance("ImpiantoApparatoCarico.SommaSchedeMP", Enabled = false)]
        public int SumTempoSchedeMp
        {
            get
            {
                return fSumTempoSchedeMp;
            }
            set
            {
                SetPropertyValue<int>("SumTempoSchedeMp", ref fSumTempoSchedeMp, value);
            }
        }

        public override string ToString()
        {
            return string.Format("Apparati({0}),Carico Totale({1})", CountApp, SumTempoSchedeMp);
        }
    }
}
