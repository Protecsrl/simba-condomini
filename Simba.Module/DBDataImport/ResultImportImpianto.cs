using CAMS.Module.Classi;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using CAMS.Module.Validation;
using CAMS.Module.DBPlant;

namespace CAMS.Module.DBDataImport
{
    [DefaultClassOptions, Persistent("REGDTIMPORTIMPIANTO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Result Import Impianto")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [NavigationItem("Data Import")]
    [ImageName("BO_Opportunity")]
    [VisibleInDashboards(false)]
    public class ResultImportImpianto : XPObject
    {
        public ResultImportImpianto()
            : base()
        {
        }

        public ResultImportImpianto(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Size(250), Persistent("DESCRIZIONE")]
        [DbType("varchar(250)")]
        [RuleRequiredField("RuleReq.ResultImportImpianto.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportImpianto.Descrizione.250", DefaultContexts.Save, 250, StringLengthComparisonMode.NotMoreThan)]
        public string Descrizione
        {
            get            {                return fDescrizione;            }
            set            {                SetPropertyValue<string>("Descrizione", ref fDescrizione, value);            }
        }

        private string fDescrizioneSistema;
        [Size(250), Persistent("DESCRIZIONESISTEMA")]
        [DevExpress.Xpo.DisplayName("Descrizione Sistema valore da Indice")]
        [DbType("varchar(250)")]
        //[RuleRequiredField("RuleReq.ResultImportImpianto.DescrizioneSistema", DefaultContexts.Save, "La Descrizione dell' Impianto è un campo obbligatorio")]
        public string DescrizioneSistema
        {
            get { return fDescrizioneSistema; }
            set { SetPropertyValue<string>("DescrizioneSistema", ref fDescrizioneSistema, value); }
        }

        //CommessaID
        private int fCommessaID;
        [Persistent("IDX_COMMESSA")]
        [DevExpress.Xpo.DisplayName("Indice Commessa")]
        [RuleValueComparison("RuleCriteria.ResultImportImpianto.CommessaID.negativo", DefaultContexts.Save, ValueComparisonType.LessThan, 0)]
        public int CommessaID
        {
            get { return fCommessaID; }
            set { SetPropertyValue<int>("CommessaID", ref fCommessaID, value); }
        }

        private int fEdificioID;
        [Persistent("IDX_EDIFICIO")]
        [DevExpress.Xpo.DisplayName("Indice Immobile")]
        [RuleRequiredField("RuleReq.ResultImportImpianto.EdificioID", DefaultContexts.Save, "L' Indice dell'Immobile è un campo obbligatorio")]
        [RuleValueComparison("RuleCriteria.ResultImportImpianto.EdificioID.negativo", DefaultContexts.Save, ValueComparisonType.LessThan, 0)]
        public int EdificioID
        {
            get
            {
                return fEdificioID;
            }
            set
            {
                SetPropertyValue<int>("EdificioID", ref fEdificioID, value);
            }
        }


        private int fImpiantoID;
        [Persistent("IDX_IMPIANTO")]
        [DevExpress.Xpo.DisplayName("Indice Impianto")]
        [RuleRequiredField("RuleReq.ResultImportImpianto.ImpiantoID", DefaultContexts.Save, "L' Indice dell'Impianto è un campo obbligatorio")]
        [RuleValueComparison("RuleCriteria.ResultImportImpianto.ImpiantoID.negativo", DefaultContexts.Save, ValueComparisonType.LessThan, 0)]
        public int ImpiantoID
        {
            get
            {
                return fImpiantoID;
            }
            set
            {
                SetPropertyValue<int>("ImpiantoID", ref fImpiantoID, value);
            }
        }

        private int fSistemaID;
        [Persistent("IDX_SISTEMA")]
        [DevExpress.Xpo.DisplayName("Indice Sistema")]
        [RuleRequiredField("RuleReq.ResultImportImpianto.SistemaID", DefaultContexts.Save, "L' Indice del Sistema è un campo obbligatorio")]
        [RuleValueComparison("RuleCriteria.ResultImportImpianto.SistemaID.positivo", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)]
        public int SistemaID
        {
            get
            {
                return fSistemaID;
            }
            set
            {
                SetPropertyValue<int>("SistemaID", ref fSistemaID, value);
            }
        }



        private FlgAbilitato fGeoreferenziazione;
        [ Persistent("GEOREFERENZIATO")]
        [DevExpress.Xpo.DisplayName("Georeferenziazione")]
        [RuleRequiredField("RuleReq.ResultImportImpianto.Georeferenziazione", DefaultContexts.Save, "La Georeferenziazione è un campo obbligatorio")]
        public FlgAbilitato Georeferenziazione
        {
            get
            {
                return fGeoreferenziazione;
            }
            set
            {
                SetPropertyValue<FlgAbilitato>("Georeferenziazione", ref fGeoreferenziazione, value);
            }
        }

        //private RegistroDataImportTentativi fRegistroDataImportTentativi;
        //[Size(250), Persistent("DESCRIZIONE_TENTATIVO")]
        //[DbType("varchar(250)")]
        //[Association(@"RegistroDataImportTentativi.ResultImportImpianto")]
        //[RuleRequiredField("RuleReq.ResultImportImpianto.RegistroDataImportTentativi", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //public RegistroDataImportTentativi RegistroDataImportTentativi
        //{
        //    get
        //    {
        //        return fRegistroDataImportTentativi;
        //    }
        //    set
        //    {
        //        SetPropertyValue<RegistroDataImportTentativi>("RegistroDataImportTentativi", ref fRegistroDataImportTentativi, value);
        //    }
        //}

        [Association(@"RegistroDataImportTentativi.ResultImportImpianto")]
        [Persistent("REGDATAIMPORTTENTATIVI"), DisplayName("RegistroDataImportTentativi")]
        //[RuleRequiredField("RuleReq.ResultImportImpianto.RegistroDataImportTentativi", DefaultContexts.Save, "Il Registro Tentativo Data Import è un campo obbligatorio")]
        [Delayed(true)]
        public RegistroDataImportTentativi RegistroDataImportTentativi
        {
            get
            {
                return GetDelayedPropertyValue<RegistroDataImportTentativi>("RegistroDataImportTentativi");
            }
            set
            {
                SetDelayedPropertyValue<RegistroDataImportTentativi>("RegistroDataImportTentativi", value);
            }
        }


        [Persistent("SERVIZIO"), DevExpress.Xpo.DisplayName("Servizio")]
        //[RuleRequiredField("RuleReq.ResultImportEdificio.Immobile", DefaultContexts.Save, "Il Commessa è un campo obbligatorio")]
        [Delayed(true)]
        public Servizio Servizio
        {
            get { return GetDelayedPropertyValue<Servizio>("Servizio"); }
            set { SetDelayedPropertyValue<Servizio>("Servizio", value); }
        }
        private int fRiga;
        [Persistent("RIGA"), DisplayName("Riga")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("La riga di occorrenza dell'errore", "Avviso", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public int Riga
        {
            get { return fRiga; }
            set { SetPropertyValue<int>("Riga", ref fRiga, value); }
        }
    }
}
