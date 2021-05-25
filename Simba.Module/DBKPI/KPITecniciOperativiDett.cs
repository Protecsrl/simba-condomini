using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;

using DevExpress.Persistent.Base;
using DevExpress.Xpo;

using CAMS.Module.PropertyEditors;
using CAMS.Module.DBTask;
using CAMS.Module.Classi;

namespace CAMS.Module.DBKPI
{

    [DefaultClassOptions, Persistent("KPITECNICIOPERATIVIDETT")]  //v_regoperativorisorse
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "KPI Tecnici Operativi Dett")]
    [ImageName("Action_Debug_Step")]
    [NavigationItem("KPI")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
    //[ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
    //---
    public class KPITecniciOperativiDett : XPObject
    {
        public KPITecniciOperativiDett()
            : base()
        {
        }

        public KPITecniciOperativiDett(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }




        private const string DateAndTimeOfDayEditMaskhhss = "dddd dd/MM/yyyy";
        private DateTime fDataGiorno;
        [Persistent("DATAGIORNO"), DisplayName("Data Riferimento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMaskhhss + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMaskhhss)]
        public DateTime DataGiorno
        {
            get            {                return fDataGiorno;            }
            set            {                SetPropertyValue<DateTime>("DataGiorno", ref fDataGiorno, value);            }
        }

        private const string DateAndTimeOfDayEditMask = "dddd dd/MM/yyyy H:mm:ss";
        private DateTime fDataOra;
        [Persistent("DATAORA"), DisplayName("Data Ora")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra
        {
            get
            {
                return fDataOra;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOra", ref fDataOra, value);
            }
        }
        private string fUtenteTecnico;

        [Persistent("UTENTE"), Size(150), DisplayName("Utente Tecnico")]
        [DbType("varchar(150)")]

        public string UtenteTecnico
        {
            get
            {
                return fUtenteTecnico;
            }
            set
            {
                SetPropertyValue<string>("UtenteTecnico", ref fUtenteTecnico, value);
            }
        }
        //private string fRisorsaDesc;
        //[Persistent("RISORSADESC"), Size(150), DisplayName("Tecnico")]
        //[DbType("varchar(150)")]

        //public string RisorsaDesc
        //{
        //    get
        //    {
        //        return fRisorsaDesc;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("RisorsaDesc", ref fRisorsaDesc, value);
        //    }
        //}

        //private string fCentroOperativo;
        //[Persistent("CENTROOPERATIVO"), Size(150), DisplayName("Centro Operativo")]
        //[DbType("varchar(150)")]
        //public string CentroOperativo
        //{
        //    get
        //    {
        //        return fCentroOperativo;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("CentroOperativo", ref fCentroOperativo, value);
        //    }
        //}
        private string fTipoRegRdL;
        [Persistent("TIPOREGRDL"), Size(150), DisplayName("Tipo Registro")]
        [DbType("varchar(150)")]
        public string TipoRegRdL
        {
            get
            {
                return fTipoRegRdL;
            }
            set
            {
                SetPropertyValue<string>("TipoRegRdL", ref fTipoRegRdL, value);
            }
        }

        private string fOldStato;
        [Persistent("OLD_STATO"), Size(150), DisplayName("Stato Precedente RegRdL")]
        [DbType("varchar(250)")]
        [VisibleInListView(false)]
        public string OldStato
        {
            get
            {
                return fOldStato;
            }
            set
            {
                SetPropertyValue<string>("OldStato", ref fOldStato, value);
            }
        }

        private string fStato;
        [Persistent("STATO"), Size(150), DisplayName("Stato RegRdL")]
        [DbType("varchar(150)")]
        public string Stato
        {
            get
            {
                return fStato;
            }
            set
            {
                SetPropertyValue<string>("Stato", ref fStato, value);
            }
        }

        private int fRegRdL;
        [Persistent("CODICEREGRDL"), DisplayName("Codice RegRdL")]

        public int RegRdL
        {
            get
            {
                return fRegRdL;
            }
            set
            {
                SetPropertyValue<int>("RegRdL", ref fRegRdL, value);
            }
        }

        private int fDeltaTempi;
        [Persistent("DELTATEMPI"), DisplayName("Delta Tempo (min)")]

        public int DeltaTempi
        {
            get
            {
                return fDeltaTempi;
            }
            set
            {
                SetPropertyValue<int>("DeltaTempi", ref fDeltaTempi, value);
            }
        }

        //private int fEdificio;
        //[Persistent("IMMOBILE"), DisplayName("OidEdificio")]

        //public int Immobile
        //{
        //    get
        //    {
        //        return fEdificio;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("Immobile", ref fEdificio, value);
        //    }
        //}

        private Risorse fRisorse;
        [Persistent("RISORSA"), DisplayName("OidTecnico")]

        public Risorse Risorse
        {
            get
            {
                return fRisorse;
            }
            set
            {
                SetPropertyValue<Risorse>("Risorse", ref fRisorse, value);
            }
        }

        //DATAORA	DATE
        //DATAGIORNO	DATE
        //ORAMINNUM	NUMBER
        //RISORSA	NUMBER
        //RISORSADESC	VARCHAR2(150)
        //UTENTE	NVARCHAR2(100)
        //CENTROOPERATIVO	VARCHAR2(150)
        //DELTATEMPI	NUMBER
        //CODICEREGRDL	NUMBER
        //IMMOBILE	NUMBER
        //TIPOREGRDL	VARCHAR2(25)
        //STATO	VARCHAR2(10)    


        [Association(@"KPITecniciOperativi_Dett"), Persistent("KPITECNICIOPERATIVI"), DisplayName("Tecnico Operativo")]
        //[DataSourceCriteria("Oid = '@This.MasterDettaglio.Apparato.Oid'")]
        //[Appearance("regmisuredettaglio.Apparato", Criteria = "[MasterDettaglio] Is Not Null", Enabled = false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public KPITecniciOperativi KPITecniciOperativi
        {
            get
            {
                return GetDelayedPropertyValue<KPITecniciOperativi>("KPITecniciOperativi");
            }
            set
            {
                SetDelayedPropertyValue<KPITecniciOperativi>("KPITecniciOperativi", value);
            }
        }


        private TipoBloccoDati fBlocco;  // 0=non bloccato , 1 Bloccato Tutto, 2 cloccato solo inserimento, 3 Bloccato solo aggiornamento
        [Persistent("BLOCCO"), DisplayName("Blocco")]
        [VisibleInListView(false)]
        public TipoBloccoDati Blocco
        {
            get { return fBlocco; }
            set { SetPropertyValue<TipoBloccoDati>("Blocco", ref fBlocco, value); }
        }

    }
}
