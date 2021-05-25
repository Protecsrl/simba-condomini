using System;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;


namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions,
    Persistent("SCHEDEMPLOG")]
    [VisibleInDashboards(false)]
    [NavigationItem("Procedure Attivita")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Log Pmp")]
    [ImageName("Action_Debug_Step")]

    public class PmpLog : XPObject
    {
        public PmpLog()
            : base()
        {
        }

        public PmpLog(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private SchedaMp fSchedaMp;
        [Persistent("SCHEDAMP")]
        [ExplicitLoading()]
        public SchedaMp SchedaMp
        {
            get
            {
                return fSchedaMp;
            }
            set
            {
                SetPropertyValue<SchedaMp>("SchedaMp", ref fSchedaMp, value);
            }
        }


        private string fCodPmp; ///  String.Format("{0}{1}", this.Categoria, this.Sistema
        [Persistent("CODPMP"),
        Size(20)]
        [DbType("varchar(20)")]
        public string CodPmp
        {
            get
            {
                return fCodPmp;
            }
            set
            {
                SetPropertyValue<string>("CodPmp", ref fCodPmp, value);
            }
        }

        private Sistema fSistema;
        [Persistent("SISTEMA"), DisplayName(@"Unità Tecnologica")]
        [ExplicitLoading()]
        public Sistema Sistema
        {
            get
            {
                return fSistema;
            }
            set
            {
                SetPropertyValue<Sistema>("Sistema", ref fSistema, value);
            }
        }

        private Categoria fCategoria;
        [Persistent("CATEGORIA")]
        [ExplicitLoading()]
        public Categoria Categoria
        {
            get
            {
                return fCategoria;
            }
            set
            {
                SetPropertyValue<Categoria>("Categoria", ref fCategoria, value);
            }
        }

        private StdAsset fStdApparato;
        [Persistent("EQSTD"), DisplayName("Tipo Apparato")]
        [ExplicitLoading()]
        public StdAsset Eqstd
        {
            get
            {
                return fStdApparato;
            }
            set
            {
                SetPropertyValue<StdAsset>("StdApparato", ref fStdApparato, value);
            }
        }

        private string fSottoComponente;
        [Persistent("SOTTOCOMPONENTE"),
        Size(100)]
        [DbType("varchar(100)")]
        public string SottoComponente
        {
            get
            {
                return fSottoComponente;
            }
            set
            {
                SetPropertyValue<string>("SottoComponente", ref fSottoComponente, value);
            }
        }

        private string fManutenzione;
        [Persistent("MANUTENZIONE"),
        Size(250)]
        [DbType("varchar(250)")]
        public string Manutenzione
        {
            get
            {
                return fManutenzione;
            }
            set
            {
                SetPropertyValue<string>("Manutenzione", ref fManutenzione, value);
            }
        }

        private Frequenze fFrequenzaBase;
        [Persistent("FREQUENZA")]
        [ExplicitLoading()]
        public Frequenze FrequenzaBase
        {
            get
            {
                return fFrequenzaBase;
            }
            set
            {
                SetPropertyValue<Frequenze>("FrequenzaBase", ref fFrequenzaBase, value);
            }
        }

        private Skill fSkillBase;
        [Persistent("SKILLBASE")]
        public Skill SkillBase
        {
            get
            {
                return fSkillBase;
            }
            set
            {
                SetPropertyValue<Skill>("SkillBase", ref fSkillBase, value);
            }
        }

        private int fTempoBase;
        [Persistent("TEMPO")]
        public int TempoBase
        {
            get
            {
                return fTempoBase;
            }
            set
            {
                SetPropertyValue<int>("TempoBase", ref fTempoBase, value);
            }
        }

        private Frequenze fFrequenzaOpt;
        [Persistent("FREQUENZAOPT")]
        [ExplicitLoading()]
        public Frequenze FrequenzaOpt
        {
            get
            {
                return fFrequenzaOpt;
            }
            set
            {
                SetPropertyValue<Frequenze>("FrequenzaOpt", ref fFrequenzaOpt, value);
            }
        }

        private Mansioni fMansioniOpt;
        [Persistent("MANSIONIOPT")]
        [ExplicitLoading()]
        public Mansioni MansioniOpt
        {
            get
            {
                return fMansioniOpt;
            }
            set
            {
                SetPropertyValue<Mansioni>("MansioniOpt", ref fMansioniOpt, value);
            }
        }

        private int fNumMan;// = 1;
        [Persistent("NUMMAN")]
        public int NumMan
        {
            get
            {
                return fNumMan;
            }
            set
            {
                SetPropertyValue<int>("NumMan", ref fNumMan, value);
            }
        }

        private int fTempoOpt = 0;
        [Persistent("TEMPOOPT")]
        public int TempoOpt
        {
            get
            {
                return fTempoOpt;
            }
            set
            {
                SetPropertyValue<int>("TempoOpt", ref fTempoOpt, value);
            }
        }

        private double fKDimensionale = 1;
        [Persistent("KDIMENSIONE")]
        public double KDimensionale
        {
            get
            {
                return fKDimensionale;
            }
            set
            {
                SetPropertyValue<double>("KDimensionale", ref fKDimensionale, value);
            }
        }

        private double fKGuasto = 1;
        [Persistent("KGUASTO")]
        public double KGuasto
        {
            get
            {
                return fKGuasto;
            }
            set
            {
                SetPropertyValue<double>("KGuasto", ref fKGuasto, value);
            }
        }

        private double fKCondizione = 1;
        [Persistent("KCONDIZIONE")]
        public double KCondizione
        {
            get
            {
                return fKCondizione;
            }
            set
            {
                SetPropertyValue<double>("KCondizione", ref fKCondizione, value);
            }
        }

        private double fKUtenza = 1;
        [Persistent("KUTENZA")]
        public double KUtenza
        {
            get
            {
                return fKUtenza;
            }
            set
            {
                SetPropertyValue<double>("KUtenza", ref fKUtenza, value);
            }
        }

        private double fKUbicazione = 1;
        [Persistent("KUBICAZIONE")]
        public double KUbicazione
        {
            get
            {
                return fKUbicazione;
            }
            set
            {
                SetPropertyValue<double>("KUbicazione", ref fKUbicazione, value);
            }
        }

        private double fKTrasferimento = 1;
        [Persistent("KTRASFERIMENTO")]
        public double KTrasferimento
        {
            get
            {
                return fKTrasferimento;
            }
            set
            {
                SetPropertyValue<double>("KTrasferimento", ref fKTrasferimento, value);
            }
        }
    }
}
