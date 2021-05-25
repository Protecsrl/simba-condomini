
using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using CAMS.Module.Validation;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;


namespace CAMS.Module.DBDataImport  //ResultImportCommessa
{
    [DefaultClassOptions, Persistent("REGDTIMPORTCOMMESSA")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Result Import Commessa")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [NavigationItem("Data Import")]
    [ImageName("BO_Opportunity")]
    [VisibleInDashboards(false)]
    [RuleIsReferenced("RuleIsRef.ResultImportCommessa", DefaultContexts.Save, typeof(SistemaClassi), "SistemaTecnologico", InvertResult = true, MessageTemplateMustBeReferenced = "If you want to delete this object, you must be sure it is not referenced in any other object.")]

    public class ResultImportCommessa : XPObject
    {
        public ResultImportCommessa()
            : base()
        {
        }

        public ResultImportCommessa(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private int fCommessaID;
        [Persistent("IDX_COMMESSA")]
        [DevExpress.Xpo.DisplayName("Indice Commessa")]
        [RuleRequiredField("RuleReq.ResultImportCommessa.CommessaID", DefaultContexts.Save, "L'Indice Commessa è un campo obbligatorio")]
        [RuleValueComparison("RuleCriteria.ResultImportCommessa.CommessaID.negativo", DefaultContexts.Save, ValueComparisonType.LessThan, 0)]
        public int CommessaID
        {
            get { return fCommessaID; }
            set { SetPropertyValue<int>("CommessaID", ref fCommessaID, value); }
        }

        private string fDescrizione;
        [Size(250), Persistent("DESCRIZIONE")]
        [DbType("varchar(250)")]
        [RuleRequiredField("RuleReq.ResultImportCommessa.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportCommessa.Descrizione.250", DefaultContexts.Save, 250, StringLengthComparisonMode.NotMoreThan)]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }



        private string fCodiceCommessa;
        [Persistent("CODICECOMMESSA"), DevExpress.Xpo.DisplayName("Codice Commessa")]
        [Size(50), DbType("varchar(50)")]
        [RuleRequiredField("RuleReq.ResultImportCommessa.CodiceCommessa", DefaultContexts.Save, "Il Codice Commessa è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportCommessa.CodiceCommessa.50", DefaultContexts.Save, 50, StringLengthComparisonMode.NotMoreThan)]
        public string CodiceCommessa
        {
            get { return fCodiceCommessa; }
            set { SetPropertyValue<string>("CodiceCommessa", ref fCodiceCommessa, value); }
        }



        private string fContratto;
        [Size(100), Persistent("DATICONTRATTO")]
        [DevExpress.Xpo.DisplayName("Dati Contratto")]
        [DbType("varchar(100)")]
        [RuleRequiredField("RuleReq.ResultImportCommessa.Contratto", DefaultContexts.Save, "Il Contratto è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportCommessa.Contratto.100", DefaultContexts.Save, 100, StringLengthComparisonMode.NotMoreThan)]
        public string Contratto
        {
            get { return fContratto; }
            set { SetPropertyValue<string>("Contratto", ref fContratto, value); }
        }

        private const string DateAndTimeOfDayEditMaskic = "dd/MM/yyyy H:mm:ss";
        private DateTime fDataInizioCommessa;
        [Persistent("DATAINIZIOCOMMESSA")]
        [DevExpress.Xpo.DisplayName("Data Inizio Commessa")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMaskic + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMaskic)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Inizio Commessa", "RPF", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [RuleRequiredField("RuleReq.ResultImportCommessa.DataInizioCommessa", DefaultContexts.Save, "La Data Inizio Commessa è un campo obbligatorio")]
        //[RuleCriteria("RuleCriteria.ResultImportCommessa.DataInizioCommessa.non+di5anni", DefaultContexts.Save, @"GetYear(AddYears(Now(), -5)) < GetYear(DataFineCommessa)"
        //                                                                                , CustomMessageTemplate = "la data non puo essere piu vecchia di 5 anni")]
        [RuleValueComparison("RuleCriteria.ResultImportCommessa.DataInizioCommessa.non+di5anni", DefaultContexts.Save, ValueComparisonType.GreaterThanOrEqual, "Feb 1, 2015")]
        public DateTime DataInizioCommessa
        {
            get { return fDataInizioCommessa; }
            set { SetPropertyValue<DateTime>("DataInizioCommessa", ref fDataInizioCommessa, value); }

        }

        private const string DateAndTimeOfDayEditMaskfc = "dd/MM/yyyy H:mm:ss";
        private DateTime fDataFineCommessa;
        [Persistent("DATAFINECOMMESSA")]
        [DevExpress.Xpo.DisplayName("Data Fine Commessa")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMaskfc + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMaskfc)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Fine Commessa", "RPF", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [RuleRequiredField("RuleReq.ResultImportCommessa.DataFineCommessa", DefaultContexts.Save, "La Data Fine Commessa è un campo obbligatorio")]

        public DateTime DataFineCommessa
        {
            get { return fDataFineCommessa; }
            set { SetPropertyValue<DateTime>("DataFineCommessa", ref fDataFineCommessa, value); }
        }


        private string fCentroCosto;
        [Size(50), Persistent("CENTRODICOSTO")]
        [DevExpress.Xpo.DisplayName("Contratto")]
        [DbType("varchar(50)")]
        [RuleRequiredField("RuleReq.ResultImportCommessa.CentroCosto", DefaultContexts.Save, "Il CentroCosto è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportCommessa.CentroCosto.50", DefaultContexts.Save, 50, StringLengthComparisonMode.NotMoreThan)]
        public string CentroCosto
        {
            get { return fCentroCosto; }
            set { SetPropertyValue<string>("CentroCosto", ref fCentroCosto, value); }
        }


        private string fCodiceWbs;
        [Persistent("CODICEWBS"), DevExpress.Xpo.DisplayName("Codice WBS")]
        [Size(7), DbType("varchar(7)")]
        [RuleRequiredField("RuleReq.ResultImportCommessa.CodiceWbs", DefaultContexts.Save, "Il Codice WBS è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportCommessa.CodiceWbs.7", DefaultContexts.Save, 7, StringLengthComparisonMode.NotMoreThan)]
        public string CodiceWbs
        {
            get { return fCodiceWbs; }
            set { SetPropertyValue<string>("CodiceWbs", ref fCodiceWbs, value); }
        }


        private int fAreaPoloID;
        [Persistent("IDX_AREAPOLO")]
        [DevExpress.Xpo.DisplayName("Area Di Polo")]
        [RuleRequiredField("RuleReq.ResultImportCommessa.AreaPoloID", DefaultContexts.Save, "L'Area Di Polo è un campo obbligatorio")]
        //[RuleIsReferenced("Rule.IsReferencedObject.AreaPoloID", DefaultContexts.Delete, typeof(RuleIsReferencedObject), "ReferencedObject", InvertResult = true, MessageTemplateMustBeReferenced = "If you want to delete this object, you must be sure it is not referenced in any other object.")]
        [RuleValueComparison("RuleCriteria.ResultImportCommessa.AreaPoloID.positivo", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)]
        public int AreaPoloID
        {
            get { return fAreaPoloID; }
            set { SetPropertyValue<int>("AreaPoloID", ref fAreaPoloID, value); }
        }

        //private SistemaTecnologico sistemaTecnologico;
        //[Persistent("SISTEMATECNOLOGICO")]
        //[DevExpress.Xpo.DisplayName("Sistema Tecnologico")]
        //public SistemaTecnologico SistemaTecnologico
        //{
        //    get { return sistemaTecnologico; }
        //    set { SetPropertyValue<SistemaTecnologico>("SistemaTecnologico", ref sistemaTecnologico, value); }
        //}
        ////INDICE_PM INDICE_AC   INDICE_REFERENTE_OPERATIVO INDICE_TERZO_RESPONSABILE_ENGIE



        private int fPMID;
        [Persistent("IDX_PM")]
        [DevExpress.Xpo.DisplayName("PM Commessa")]
        [RuleValueComparison("RuleCriteria.ResultImportCommessa.PMID.positivo", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)]
        public int PMID
        {
            get { return fPMID; }
            set { SetPropertyValue<int>("PMID", ref fPMID, value); }
        }

      

        private int fPACID;
        [Persistent("IDX_AC")]
        [DevExpress.Xpo.DisplayName("Assistente Commessa")]
        [RuleValueComparison("RuleCriteria.ResultImportCommessa.PACID.positivo", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)]
        public int PACID
        {
            get { return fPACID; }
            set { SetPropertyValue<int>("PACID", ref fPACID, value); }
        }

        private int fReferenteOperativoID;
        [Persistent("IDX_REFERENTEOPERATIVO")]
        [DevExpress.Xpo.DisplayName("Referente Operativo Commessa")]
        [RuleValueComparison("RuleCriteria.ResultImportCommessa.ReferenteOperativoID.positivo", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)]
        public int ReferenteOperativoID
        {
            get { return fReferenteOperativoID; }
            set { SetPropertyValue<int>("ReferenteOperativoID", ref fReferenteOperativoID, value); }
        }

        private int fTerzoResponsabileID;
        [Persistent("IDX_TERZORESPONSABILEENGIE")]
        [DevExpress.Xpo.DisplayName("Terzo Responsabile Commessa")]
        [RuleValueComparison("RuleCriteria.ResultImportCommessa.TerzoResponsabileID.positivo", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)]
        public int TerzoResponsabileID
        {
            get { return fTerzoResponsabileID; }
            set { SetPropertyValue<int>("TerzoResponsabileID", ref fTerzoResponsabileID, value); }
        }

        //private string fPMNomeCognome;
        //[Size(250), Persistent("PMNOMECOGNOME")]
        //[DevExpress.Xpo.DisplayName("Nome e Cognome del PM")]
        //[DbType("varchar(250)")]
        //[RuleRequiredField("RuleReq.ResultImportCommessa.PMNomeCognome", DefaultContexts.Save, "Nome e Cognome del PM è un campo obbligatorio")]
        //public string PMNomeCognome
        //{
        //    get
        //    {
        //        return fPMNomeCognome;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("PMNomeCognome", ref fPMNomeCognome, value);
        //    }
        //}

        //private string fPMNrCellulare;
        //[Size(250), Persistent("PMNRCELLULARE")]
        //[DevExpress.Xpo.DisplayName("Numero di cellulare del PM")]
        //[DbType("varchar(250)")]
        //[RuleRequiredField("RuleReq.ResultImportCommessa.PMNrCellulare", DefaultContexts.Save, "Il Numero di cellulare del PM è un campo obbligatorio")]
        //public string PMNrCellulare
        //{
        //    get
        //    {
        //        return fPMNrCellulare;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("PMNrCellulare", ref fPMNrCellulare, value);
        //    }
        //}


        //private string fACNomeCognome;
        //[Size(250), Persistent("ACNOMECOGNOME")]
        //[DevExpress.Xpo.DisplayName("Nome e Cognome dell'assistente")]
        //[DbType("varchar(250)")]
        //[RuleRequiredField("RuleReq.ResultImportCommessa.ACNomeCognome", DefaultContexts.Save, "Nome e Cognome dell'Assistente è un campo obbligatorio")]
        //public string ACNomeCognome
        //{
        //    get
        //    {
        //        return fACNomeCognome;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("ACNomeCognome", ref fACNomeCognome, value);
        //    }
        //}



        //private string fACCommessaNrCell;
        //[Size(250), Persistent("ACNRCELLULARE")]
        //[DevExpress.Xpo.DisplayName("Numero di cellulare dell'Assistente")]
        //[DbType("varchar(250)")]
        //[RuleRequiredField("RuleReq.ResultImportCommessa.ACCommessaNrCell", DefaultContexts.Save, "Il Numero di cellulare dell'Assistente è un campo obbligatorio")]
        //public string ACCommessaNrCell
        //{
        //    get
        //    {
        //        return fACCommessaNrCell;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("ACCommessaNrCell", ref fACCommessaNrCell, value);
        //    }
        //}

        //CLIENTE_DESCRIZIONE CLIENTE_INDIRIZZO
        private string fClienteDescrizione;
        [Size(100), Persistent("CLIENTEDESCRIZIONE")]
        [DevExpress.Xpo.DisplayName("Cliente")]
        [DbType("varchar(100)")]
        [RuleStringLengthComparison("CustomRule.ResultImportCommessa.ClienteDescrizione.100", DefaultContexts.Save, 100, StringLengthComparisonMode.NotMoreThan)]
        public string ClienteDescrizione
        {
            get { return fClienteDescrizione; }
            set { SetPropertyValue<string>("ClienteDescrizione", ref fClienteDescrizione, value); }
        }

        private string fClienteIndirizzo;
        [Size(250), Persistent("CLIENTEINDIRIZZO")]
        [DevExpress.Xpo.DisplayName("Cliente Indirizzo")]
        [DbType("varchar(250)")]
        [RuleStringLengthComparison("CustomRule.ResultImportCommessa.ClienteIndirizzo.250", DefaultContexts.Save, 250, StringLengthComparisonMode.NotMoreThan)]
        public string ClienteIndirizzo
        {
            get { return fClienteIndirizzo; }
            set { SetPropertyValue<string>("ClienteIndirizzo", ref fClienteIndirizzo, value); }
        }

        //nella tabella DB ClientiContatti unico campo Denominazione da 150 caratteri nel foglio excel abbiamo due campi

        private string fRefClienteCognomeNome;
        [Size(250), Persistent("REFCLIENTENOMECOGNOME")]
        [DevExpress.Xpo.DisplayName("Nome e Cognome Referente Cliente")]
        [DbType("varchar(250)")]
        [RuleStringLengthComparison("CustomRule.ResultImportCommessa.RefClienteCognomeNome.250", DefaultContexts.Save, 250, StringLengthComparisonMode.NotMoreThan)]
        public string RefClienteCognomeNome
        {
            get { return fRefClienteCognomeNome; }
            set { SetPropertyValue<string>("RefClienteCognomeNome", ref fRefClienteCognomeNome, value); }
        }

        private string fRefClienteNrCellulare;
        [Size(20), Persistent("REFCLIENTENRCELLULARE")]
        [DevExpress.Xpo.DisplayName("Numero di Telefono Cellulare del Referente Cliente")]
        [DbType("varchar(20)")]
        [RuleStringLengthComparison("CustomRule.ResultImportCommessa.RefClienteNrCellulare.20", DefaultContexts.Save, 20, StringLengthComparisonMode.NotMoreThan)]
        public string RefClienteNrCellulare
        {
            get { return fRefClienteNrCellulare; }
            set { SetPropertyValue<string>("RefClienteNrCellulare", ref fRefClienteNrCellulare, value); }
        }

        private string fRefClienteNrFissso;
        [Size(20), Persistent("REFCLIENTENRFISSO")]
        [DevExpress.Xpo.DisplayName("Numero di Telefono fisso del Referente Cliente")]
        [DbType("varchar(20)")]
        [RuleStringLengthComparison("CustomRule.ResultImportCommessa.RefClienteNrFissso.20", DefaultContexts.Save, 20, StringLengthComparisonMode.NotMoreThan)]
        public string RefClienteNrFissso
        {
            get { return fRefClienteNrFissso; }
            set { SetPropertyValue<string>("RefClienteNrFissso", ref fRefClienteNrFissso, value); }
        }


        //  da qui partono le collection


        private RegistroDataImportTentativi fRegistroDataImportTentativi;
        [Persistent("REGDATAIMPORTTENTATIVI"), DevExpress.Xpo.DisplayName("RegistroDataImportTentativi")]
        [Association(@"RegistroDataImportTentativi.ResultImportCommessa")]
        // [RuleRequiredField("RuleReq.ResultImportCommessa.RegistroDataImportTentativi", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public RegistroDataImportTentativi RegistroDataImportTentativi
        {
            get
            {
                return fRegistroDataImportTentativi;
            }
            set
            {
                SetPropertyValue<RegistroDataImportTentativi>("RegistroDataImportTentativi", ref fRegistroDataImportTentativi, value);
            }
        }
        //   oggetto creato
     
        [Persistent("CONTRATTO"), DevExpress.Xpo.DisplayName("Commessa")]
        //[RuleRequiredField("RuleReq.ResultImportCommessa.Commessa", DefaultContexts.Save, "Il Commessa è un campo obbligatorio")]
        [Delayed(true)]
        public Contratti Commessa
        {
            get { return GetDelayedPropertyValue<Contratti>("Commessa"); }
            set { SetDelayedPropertyValue<Contratti>("Commessa", value); }
        }
        private int fRiga;
        [Persistent("RIGA"), DevExpress.Xpo.DisplayName("Riga")]
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


