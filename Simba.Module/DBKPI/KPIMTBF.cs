using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;

namespace CAMS.Module.DBKPI
{
    [DefaultClassOptions, Persistent("V_BASE_KPI_MTBF")] //fai fare la vista ad andrea
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "KPI MTBF")]
    [ImageName("StackedLine")]
    [NavigationItem("KPI")]
    public class KPIMTBF : XPLiteObject
    {
        public KPIMTBF()
            : base()
        {
        }

        public KPIMTBF(Session session)
            : base(session)
        {
        }

        private string fcodice;
        [Key, Size(50), Persistent("CODICE"), MemberDesignTimeVisibility(false)]
        //[DbType("varchar(50)")]
        public string Codice
        {
            get
            {
                return fcodice;
            }
            set
            {
                SetPropertyValue<string>("Codice", ref fcodice, value);
            }
        }

        [PersistentAlias("Apparato.Impianto.Edificio.Commesse.AreaDiPolo.Polo"), DisplayName("Polo")]
        [ExplicitLoading()]
        public Polo Polo
        {
            get
            {
                object tempObject = EvaluateAlias("Polo");
                if (tempObject != null)
                {
                    return (Polo)tempObject;
                }
                else
                {
                    return null;
                }
            }
        }


        [PersistentAlias("Apparato.Impianto.Edificio.Commesse.AreaDiPolo"), DisplayName("AreaDiPolo")]
        [ExplicitLoading()]
        public AreaDiPolo AreaDiPolo
        {
            get
            {
                object tempObject = EvaluateAlias("AreaDiPolo");
                if (tempObject != null)
                {
                    return (AreaDiPolo)tempObject;
                }
                else
                {
                    return null;
                }
            }

        }

        [PersistentAlias("Apparato.Impianto.Edificio.Commesse"), DisplayName("Commessa")]
        [ExplicitLoading()]
        public Commesse Commessa
        {
            get
            {
                object tempObject = EvaluateAlias("Commessa");
                if (tempObject != null)
                {
                    return (Commesse)tempObject;
                }
                else
                {
                    return null;
                }
            }

        }

        [PersistentAlias("Apparato.Impianto.Edificio"), System.ComponentModel.DisplayName("Edificio")]
        [ExplicitLoading()]
        public Edificio Edificio
        {
            get
            {
                object tempObject = EvaluateAlias("Edificio");
                if (tempObject != null)
                {
                    return (Edificio)tempObject;
                }
                else
                {
                    return null;
                }
            }
        }

        [PersistentAlias("Apparato.Impianto"), DisplayName("Impianto")]
        [ExplicitLoading()]
        public Impianto Impianto
        {
            get
            {
                object tempObject = EvaluateAlias("Impianto");
                if (tempObject != null)
                {
                    return (Impianto)tempObject;
                }
                else
                {
                    return null;
                }
            }
        }



        private Apparato fApparato;
        [Persistent("APPARATO"), DisplayName("Apparato")]
        [ExplicitLoading()]
        public Apparato Apparato
        {
            get
            {
                return fApparato;
            }
            set
            {
                SetPropertyValue<Apparato>("Apparato", ref fApparato, value);
            }
        }


        private int ftempoMTBF;
        [Persistent("MTBF"), DisplayName("Mean Time Between To Failure")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public int tempoMTBF
        {
            get
            {
                return ftempoMTBF;
            }
            set
            {
                SetPropertyValue<int>("tempoMTBF", ref ftempoMTBF, value);
            }
        }
        //  "dd\.hh\:mm\:ss"   "{0:hh\\:mm}"  mm\\:ss
        private TimeSpan ftMTBF;
        [NonPersistent, DisplayName("MTBF")]
        [ModelDefault("EditMaskType", "RegEx")]
        [ModelDefault("EditMask", "{0:dd\\.hh\\:mm}")]
        [ModelDefault("DisplayFormat", "{0:dd\\.hh\\:mm} days.hh.min")]
        public TimeSpan tMTBF
        {
            get
            {

                var tempObject = Evaluate("tempoMTBF");
                if (tempObject != null)
                {
                    TimeSpan duration = new TimeSpan(0, 0, this.tempoMTBF, 0);
                    return duration;
                }
                else
                {
                    TimeSpan duration = new TimeSpan(0, 0, 0, 0);
                    return duration;
                }                
            }
        }
        // TimeSpan duration = new TimeSpan(1, 12, 23, 62);

        private int ftempoMTTR;
        [Persistent("MTTR"), DisplayName("Mean Time to Repaire")]
        [ToolTip("Espresso in minuti")]
        public int tempoMTTR
        {
            get
            {
                return ftempoMTTR;
            }
            set
            {
                SetPropertyValue<int>("tempoMTTR", ref ftempoMTTR, value);
            }
        }

        private int ftempoMTTF;
        [Persistent("MTTF"), DisplayName("Mean Time to Failure")]
        [ToolTip("Espresso in minuti")]
        public int tempoMTTF
        {
            get
            {
                return ftempoMTTF;
            }
            set
            {
                SetPropertyValue<int>("tempoMTTF", ref ftempoMTTF, value);
            }
        }


        private string fAnno;
        [Persistent("ANNO"), DisplayName("Anno")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public string Anno
        {
            get
            {
                return fAnno;
            }
            set
            {
                SetPropertyValue<string>("Anno", ref fAnno, value);
            }
        }
        private string fMese;
        [Persistent("MESE"), DisplayName("Mese")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public string Mese
        {
            get
            {
                return fMese;
            }
            set
            {
                SetPropertyValue<string>("Mese", ref fMese, value);
            }
        }
        private string fSettimana;
        [Persistent("SETTIMANA"), DisplayName("Settimana")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public string Settimana
        {
            get
            {
                return fSettimana;
            }
            set
            {
                SetPropertyValue<string>("Settimana", ref fSettimana, value);
            }
        }


        private tipoMTBF ftipoMTBF;
        [Persistent("TIPOMTBF"), DisplayName("Tipo kpi Failure")]
        public tipoMTBF tipoMTBF
        {
            get
            {
                return ftipoMTBF;
            }
            set
            {
                SetPropertyValue<tipoMTBF>("tipoMTBF", ref ftipoMTBF, value);
            }
        }
        private int fNrRdL;
        [Persistent("NRRDL"), DisplayName("nr RdL")]
        public int NrRdL
        {
            get
            {
                return fNrRdL;
            }
            set
            {
                SetPropertyValue<int>("NrRdL", ref fNrRdL, value);
            }
        }

    }
}
