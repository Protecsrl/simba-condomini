using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAMS.Module.Classi;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;



namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
    Persistent("AGGREGRDL")]
    [VisibleInDashboards(false)]
    [System.ComponentModel.DefaultProperty("RegRdL")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "AggRegRdL")]
    [ImageName("Time")]
    [NavigationItem("Tabelle Anagrafiche")]
    public class AggRegrdL : XPObject
    {
        public AggRegrdL()
            : base()
        {
        }
        public AggRegrdL(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


       
        //private int fOidRegRdL;
        //[Persistent("OIDREGRDL"),
        //DisplayName("RegRdL")]
        //[Delayed(true)]
        //public int OidRegRdL
        //{
        //    get
        //    {
        //        return GetDelayedPropertyValue<int>("OidRegRdL");
        //    }
        //    set
        //    {
        //        SetDelayedPropertyValue<int>("OidRegRdL", value);
        //    }
        //}

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private DateTime fDataCompletamento;
        [Persistent("DATACOMPLETAMENTO"), System.ComponentModel.DisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
      
        //[Delayed(true)]
        public DateTime DataCompletamento
        {
            get
            {
                return fDataCompletamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataCompletamento", ref fDataCompletamento, value);
            }
        }

        [Persistent("NOTECOMPLETAMENTO"), Size(4000), System.ComponentModel.DisplayName("Note Completamento")]
        [DbType("varchar(4000)")]
        //[ImmediatePostData(true)]
        [Delayed(true)]
        public string NoteCompletamento
        {
            get { return GetDelayedPropertyValue<string>("NoteCompletamento"); }
            set { SetDelayedPropertyValue<string>("NoteCompletamento", value); }

        }

        [Persistent("STRINGAREGISTRI"), System.ComponentModel.DisplayName("Stringa Registri Concatenata")]
       // [DbType("varchar(4000)")]
        //[ImmediatePostData(true)]
        [Delayed(true)]
        public string StringaRegRdL
        {
            get { return GetDelayedPropertyValue<string>("StringaRegRdL"); }
            set { SetDelayedPropertyValue<string>("StringaRegRdL", value); }

        }


        private int fRisorsaTeam;
        [Persistent("OIDRISORSATEAM"),
        DisplayName("RisorsaTeam")]
        [Delayed(true)]
        public int RisorsaTeam
        {
            get
            {
                return GetDelayedPropertyValue<int>("RisorsaTeam");
            }
            set
            {
                SetDelayedPropertyValue<int>("RisorsaTeam", value);
            }
        }


        private DateTime fDataUpdate;
        [Persistent("DATAUPDATE"), System.ComponentModel.DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]

        //[Delayed(true)]
        public DateTime DataUpdate
        {
            get
            {
                return fDataUpdate;
            }
            set
            {
                SetPropertyValue<DateTime>("DataUpdate", ref fDataUpdate, value);
            }
        }

        private Eseguito fCompletamentoEseguito;
        [Persistent("ESEGUITOCOMPLETAMENTO"), Size(100), DevExpress.Xpo.DisplayName("Completamento Eseguito")]
        [DbType("varchar(100)")]
        //[RuleRequiredField("RReqField.RegistroDataImportTentativi.ImportazioneEseguita", DefaultContexts.Save, "La Importazione Eseguita è un campo obbligatorio")]
        public Eseguito CompletamentoEseguito
        {
            get { return fCompletamentoEseguito; }
            set { SetPropertyValue<Eseguito>("CompletamentoEseguito", ref fCompletamentoEseguito, value); }
        }

        private TipoAggiornamento fTipoAggiornamento;
        [Persistent("TIPOAGGIORNAMENTO"), Size(100), DevExpress.Xpo.DisplayName("Tipo Aggiornamento")]
        [DbType("varchar(100)")]
        //[RuleRequiredField("RReqField.RegistroDataImportTentativi.ImportazioneEseguita", DefaultContexts.Save, "La Importazione Eseguita è un campo obbligatorio")]
        public TipoAggiornamento TipoAggiornamento
        {
            get { return fTipoAggiornamento; }
            set { SetPropertyValue<TipoAggiornamento>("TipoAggiornamento", ref fTipoAggiornamento, value); }
        }



    }
}

