using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.Classi;

using System.Linq;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("ASSRISORSETEAM")]
    [System.ComponentModel.DefaultProperty("Risorsa")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "AssRisorseTeam di Risorse")]
    [NavigationItem(false)]
    [ImageName("BO_Department")]
    public class AssRisorseTeam : XPObject
    {
        public AssRisorseTeam()
            : base()
        {
        }

        public AssRisorseTeam(Session session)
            : base(session)
        {
        }

        private Risorse fRisorsa;
        [Persistent("RISORSA"), Association(@"RISORSE_TEAMRISORSE"), DisplayName("Risorsa")]
        [ExplicitLoading]
        [Delayed(true)]
        public Risorse Risorsa
        {
            get
            {
                return GetDelayedPropertyValue<Risorse>("Risorsa");
            }
            set
            {
                SetDelayedPropertyValue<Risorse>("Risorsa", value);
            }

            //get
            //{
            //    return fRisorsa;
            //}
            //set
            //{
            //    SetPropertyValue<Risorse>("Risorsa", ref fRisorsa, value);
            //}
        }

        private RisorseTeam fRisorsaTeam;
        [Persistent("RISORSETEAM"), Association(@"TEAMRISORSE_RISORSE"), DisplayName("Team")]
        [DataSourceCriteria("CoppiaLinkata = 'No'")]
        [ExplicitLoading]
        [Delayed(true)]
        public RisorseTeam Team
        {
            get
            {
                return GetDelayedPropertyValue<RisorseTeam>("Team");
            }
            set
            {
                SetDelayedPropertyValue<RisorseTeam>("Team", value);
            }
            //get
            //{
            //    return fRisorsaTeam;
            //}
            //set
            //{
            //    SetPropertyValue<RisorseTeam>("Team", ref fRisorsaTeam, value);
            //}
        }

        private DateTime fDataInizio;
        [Persistent("DATAINIZIO"),
        DisplayName("Data Inizio Validità")]
        [Delayed(true)]
        public DateTime DataInizioValidita
        {
            get
            {
                return GetDelayedPropertyValue<DateTime>("DataInizioValidita");
            }
            set
            {
                SetDelayedPropertyValue<DateTime>("DataInizioValidita", value);
            }

            //get
            //{
            //    return fDataInizio;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DataInizioValidita", ref fDataInizio, value);
            //}
        }

        private DateTime fDataFine;
        [Persistent("DATAFINE"),
        DisplayName("Data Fine Validità")]
        [Delayed(true)]
        public DateTime DataFineValidita
        {
            get
            {
                return GetDelayedPropertyValue<DateTime>("DataFineValidita");
            }
            set
            {
                SetDelayedPropertyValue<DateTime>("DataFineValidita", value);
            }
            //get
            //{
            //    return fDataFine;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DataFineValidita", ref fDataFine, value);
            //}
        }

        private TipoAssRisorseTeam fTipo;
        [Persistent("TIPOASSOCIAZIONE"),
        DisplayName("Tipo Associazione")]
        public TipoAssRisorseTeam TipoAssociazione
        {
            get
            {
                return fTipo;
            }
            set
            {
                SetPropertyValue<TipoAssRisorseTeam>("TipoAssociazione", ref fTipo, value);
            }
        }

        [PersistentAlias("Team.CoppiaLinkata"), DisplayName("Coppia Linkata")]
        public TipoNumeroManutentori TeamCoppiaLinkata
        {
            get
            {
                var tempObject = EvaluateAlias("TeamCoppiaLinkata");
                if (tempObject != null)
                {
                    return (TipoNumeroManutentori)tempObject;
                }
                else
                {
                    return TipoNumeroManutentori.NonDefinito;
                    ;
                }
            }
        }

        public void EliminaAssociazione(int OidAssociazione)
        {
            var lstAssRisorseTeam = new XPCollection<AssRisorseTeam>(Session);
            lstAssRisorseTeam.Where(sk => sk.Oid == OidAssociazione);
            var listaSKFiltrata = new XPCollection<AssRisorseTeam>(Session, false);
            listaSKFiltrata.Criteria = CriteriaOperator.Parse("Oid=" + OidAssociazione);
            var mySe = Session;
            mySe.Delete(listaSKFiltrata);
            mySe.Save(listaSKFiltrata);
            mySe.CommitTransaction();
        }
    }

    public enum TipoAssRisorseTeam
    {
        Ordinaria,
        Straordinaria
    }
}
