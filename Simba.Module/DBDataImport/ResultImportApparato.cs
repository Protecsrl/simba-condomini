

using CAMS.Module.Validation;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using CAMS.Module.DBPlant;
//namespace CAMS.Module.Costi
//{
//    class RegistroLavoriConsuntivi
//    {
//    }
//}

//namespace CAMS.Module.DBDataImport
//{
//    class ResultImportApparato
//    {
//    }
//}

namespace CAMS.Module.DBDataImport
{
    [DefaultClassOptions, Persistent("REGDTIMPORTAPPARATO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Result Import Apparato")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [NavigationItem("Data Import")]
    [ImageName("BO_Opportunity")]
    [VisibleInDashboards(false)]
    public class ResultImportApparato : XPObject
    {
        public ResultImportApparato()
            : base()
        {
        }

        public ResultImportApparato(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Size(250), Persistent("DESCRIZIONE")]
        [DevExpress.Xpo.DisplayName("Descrizione Apparato")]
        [DbType("varchar(250)")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportApparato.Descrizione.250", DefaultContexts.Save, 250, StringLengthComparisonMode.NotMoreThan)]
        public string Descrizione
        {
            get
            {
                return fDescrizione;
            }
            set
            {
                SetPropertyValue<string>("Descrizione", ref fDescrizione, value);
            }
        }
        private string fCodDescrizione;
        [Size(50), Persistent("CODDESCRIZIONE")]
        [DevExpress.Xpo.DisplayName("Codice Apparato")]
        [DbType("varchar(50)")]
        [RuleStringLengthComparison("CustomRule.ResultImportApparato.CodDescrizione.50", DefaultContexts.Save, 50, StringLengthComparisonMode.NotMoreThan)]
        public string CodDescrizione
        {
            get
            {
                return fCodDescrizione;
            }
            set
            {
                SetPropertyValue<string>("CodDescrizione", ref fCodDescrizione, value);
            }
        }

        private int fCommessaID;
        [Persistent("IDX_COMMESSA")]
        [DevExpress.Xpo.DisplayName("Indice Commessa")]
        [RuleValueComparison("RuleCriteria.ResultImportApparato.CommessaID.negativo", DefaultContexts.Save, ValueComparisonType.LessThan, 0)]
        public int CommessaID
        {
            get
            {
                return fCommessaID;
            }
            set
            {
                SetPropertyValue<int>("CommessaID", ref fCommessaID, value);
            }
        }

        private int fEdificioID;
        [Persistent("IDX_EDIFICIO")]
        [DevExpress.Xpo.DisplayName("Indice Immobile")]
        [RuleRequiredField("RuleReq.ResultImportApparato.EdificioID", DefaultContexts.Save, "L'Indice dell'Immobile è un campo obbligatorio")]
        [RuleValueComparison("RuleCriteria.ResultImportApparato.EdificioID.negativo", DefaultContexts.Save, ValueComparisonType.LessThan, 0)]
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
        [RuleRequiredField("RuleReq.ResultImportApparato.ImpiantoID", DefaultContexts.Save, "L'Indice dell'Impianto è un campo obbligatorio")]
        [RuleValueComparison("RuleCriteria.ResultImportApparato.ImpiantoID.negativo", DefaultContexts.Save, ValueComparisonType.LessThan, 0)]
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
        [RuleRequiredField("RuleReq.ResultImportApparato.SistemaID", DefaultContexts.Save, "L' Indice del Sistema è un campo obbligatorio")]
        [RuleValueComparison("RuleCriteria.ResultImportApparato.SistemaID.positivo", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)]
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



        //private int fApparatoID;
        //[Persistent("IDX_APPARATO")]
        //[DevExpress.Xpo.DisplayName("Indice Apparato")]
        ////[RuleRequiredField("RuleReq.ResultImportApparato.ApparatoID", DefaultContexts.Save, "L' Indice dell'Apparato è un campo obbligatorio")]
        //[RuleValueComparison("RuleCriteria.ResultImportApparato.ApparatoID.positivo", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)]
        //public int ApparatoID
        //{
        //    get
        //    {
        //        return fApparatoID;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("ApparatoID", ref fApparatoID, value);
        //    }
        //}
        private string fDescrizioneImpianto;
        [Size(250), Persistent("DESCRIZIONEIMPIANTO")]
        [DevExpress.Xpo.DisplayName("Descrizione Impianto")]
        [DbType("varchar(250)")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.DescrizioneImpianto", DefaultContexts.Save, "La Descrizione dell'Impianto è un campo obbligatorio")]
        //[RuleStringLengthComparison("CustomRule.ResultImportApparato.DescrizioneImpianto.250", DefaultContexts.Save, 250, StringLengthComparisonMode.NotMoreThan)]
        public string DescrizioneImpianto
        {
            get
            {
                return fDescrizioneImpianto;
            }
            set
            {
                SetPropertyValue<string>("DescrizioneImpianto", ref fDescrizioneImpianto, value);
            }
        }

        private int fPianoID;
        [Persistent("IDX_PIANO")]
        [DevExpress.Xpo.DisplayName("Indice Piano")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.PianoID", DefaultContexts.Save, "L' Indice dell'Apparato è un campo obbligatorio")]
        [RuleValueComparison("RuleCriteria.ResultImportApparato.PianoID.positivo", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)]
        public int PianoID
        {
            get
            {
                return fPianoID;
            }
            set
            {
                SetPropertyValue<int>("PianoID", ref fPianoID, value);
            }
        }


        //private int fCategoriaPiano;
        //[Persistent("CATEGORIAPIANO")]
        //[DevExpress.Xpo.DisplayName("Categoria Piano")]
        ////[RuleRequiredField("RuleReq.ResultImportApparato.CategoriaPiano", DefaultContexts.Save, "La Categoria del Piano è un campo obbligatorio")]
        //public int CategoriaPiano
        //{
        //    get
        //    {
        //        return fCategoriaPiano;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("CategoriaPiano", ref fCategoriaPiano, value);
        //    }
        //}

        private string fPiano;
        [Size(100), Persistent("PIANO")]
        [DevExpress.Xpo.DisplayName("Descrizione Piano Da Indice")]
        [DbType("varchar(100)")]
        // [RuleRequiredField("RuleReq.ResultImportApparato.Piano", DefaultContexts.Save, "La Descrizione del Pianoè un campo obbligatorio")]
        public string Piano
        {
            get
            {
                return fPiano;
            }
            set
            {
                SetPropertyValue<string>("Piano", ref fPiano, value);
            }
        }





        private string fDescrizionePiano;
        [Size(100), Persistent("DESCRIZIONEPIANO")]
        [DevExpress.Xpo.DisplayName("Descrizione Piano")]
        [DbType("varchar(100)")]
        // [RuleRequiredField("RuleReq.ResultImportApparato.DescrizionePiano", DefaultContexts.Save, "La Descrizione del Pianoè un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportApparato.DescrizionePiano.100", DefaultContexts.Save, 100, StringLengthComparisonMode.NotMoreThan)]
        public string DescrizionePiano
        {
            get
            {
                return fDescrizionePiano;
            }
            set
            {
                SetPropertyValue<string>("DescrizionePiano", ref fDescrizionePiano, value);
            }
        }


     


        private string fCodiceLocale;
        [Size(25), Persistent("CODICELOCALE")]
        [DevExpress.Xpo.DisplayName("Codice Locale")]
        [DbType("varchar(25)")]
        // [RuleRequiredField("RuleReq.ResultImportApparato.CodiceLocale", DefaultContexts.Save, "Il Locale è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportApparato.CodiceLocale.25", DefaultContexts.Save, 25, StringLengthComparisonMode.NotMoreThan)]
        public string CodiceLocale
        {
            get
            {
                return fCodiceLocale;
            }
            set
            {
                SetPropertyValue<string>("CodiceLocale", ref fCodiceLocale, value);
            }
        }

        private string fDescrizioneLocale;
        [Size(100), Persistent("DESCRIZIONELOCALE")]
        [DevExpress.Xpo.DisplayName("Descrizione Locale")]
        [DbType("varchar(100)")]
        // [RuleRequiredField("RuleReq.ResultImportApparato.DescrizioneLocale", DefaultContexts.Save, "Il Locale è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportApparato.DescrizioneLocale.100", DefaultContexts.Save, 100, StringLengthComparisonMode.NotMoreThan)]
        public string DescrizioneLocale
        {
            get
            {
                return fDescrizioneLocale;
            }
            set
            {
                SetPropertyValue<string>("DescrizioneLocale", ref fDescrizioneLocale, value);
            }
        }



        private int fApparatoStandardID;
        [Persistent("IDX_APPARATOSTANDARD")]
        [DevExpress.Xpo.DisplayName("Indice Apparato Standard")]
        // [RuleRequiredField("RuleReq.ResultImportApparato.ApparatoStandardID", DefaultContexts.Save, "L'Indice dell'Apparato Standard è un campo obbligatorio")]
        [RuleValueComparison("RuleCriteria.ResultImportApparato.ApparatoStandardID.positivo", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)]
        public int ApparatoStandardID
        {
            get
            {
                return fApparatoStandardID;
            }
            set
            {
                SetPropertyValue<int>("ApparatoStandardID", ref fApparatoStandardID, value);
            }
        }



        private string fApparatoStandard;
        [Size(250), Persistent("APPARATOSTANDARD")]
        [DevExpress.Xpo.DisplayName("Apparato Standard")]
        [DbType("varchar(250)")]
       // [RuleRequiredField("RuleReq.ResultImportApparato.ApparatoStandard", DefaultContexts.Save, "L'Apparato Standard è un campo obbligatorio")]
        public string ApparatoStandard
        {
            get
            {
                return fApparatoStandard;
            }
            set
            {
                SetPropertyValue<string>("ApparatoStandard", ref fApparatoStandard, value);
            }
        }


        private string fModello;
        [Size(100), Persistent("MODELLO")]
        [DevExpress.Xpo.DisplayName("Modello")]
        [DbType("varchar(100)")]
        // [RuleRequiredField("RuleReq.ResultImportApparato.Modello", DefaultContexts.Save, "Il Modello è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportApparato.Modello.100", DefaultContexts.Save, 100, StringLengthComparisonMode.NotMoreThan)]
        public string Modello
        {
            get
            {
                return fModello;
            }
            set
            {
                SetPropertyValue<string>("Modello", ref fModello, value);
            }
        }


        private string fMarca;
        [Size(100), Persistent("MARCA")]
        [DevExpress.Xpo.DisplayName("Marca")]
        [DbType("varchar(100)")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.Marca", DefaultContexts.Save, "La Marca è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportApparato.Marca.100", DefaultContexts.Save, 100, StringLengthComparisonMode.NotMoreThan)]
        public string Marca
        {
            get
            {
                return fMarca;
            }
            set
            {
                SetPropertyValue<string>("Marca", ref fMarca, value);
            }
        }

        //****DA modificare NEL DB DIMENSIONi e tipo nvarchar*****************
        private string fMatricola;
        [Size(100), Persistent("MATRICOLA")]
        [DevExpress.Xpo.DisplayName("Matricola")]
        [DbType("varchar(100)")]
        // [RuleRequiredField("RuleReq.ResultImportApparato.Matricola", DefaultContexts.Save, "La Matricola è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportApparato.Matricola.100", DefaultContexts.Save, 100, StringLengthComparisonMode.NotMoreThan)]
        public string Matricola
        {
            get
            {
                return fMatricola;
            }
            set
            {
                SetPropertyValue<string>("Matricola", ref fMatricola, value);
            }
        }

        private string fLatitudine;
        [Size(15), Persistent("LATITUDINE")]
        [DevExpress.Xpo.DisplayName("Latitudine")]
        [DbType("varchar(15)")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.Latitudine", DefaultContexts.Save, "La Latitudine è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportApparato.Latitudine.15", DefaultContexts.Save, 15, StringLengthComparisonMode.NotMoreThan)]
        public string Latitudine
        {
            get
            {
                return fLatitudine;
            }
            set
            {
                SetPropertyValue<string>("Latitudine", ref fLatitudine, value);
            }

        }
        private string fLongitudine;
        [Size(15), Persistent("LONGITUDINE")]
        [DevExpress.Xpo.DisplayName("Longitudine")]
        [DbType("varchar(15)")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.Longitudine", DefaultContexts.Save, "La Longitudine è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportApparato.Longitudine.15", DefaultContexts.Save, 15, StringLengthComparisonMode.NotMoreThan)]
        public string Longitudine
        {
            get
            {
                return fLongitudine;
            }
            set
            {
                SetPropertyValue<string>("Longitudine", ref fLongitudine, value);
            }
        }


        private string fNote;
        [Size(100), Persistent("NOTE")]
        [DevExpress.Xpo.DisplayName("Note")]
        [DbType("varchar(100)")]
        // [RuleRequiredField("RuleReq.ResultImportApparato.Note", DefaultContexts.Save, "Le Note è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportApparato.Note.100", DefaultContexts.Save, 100, StringLengthComparisonMode.NotMoreThan)]
        public string Note
        {
            get
            {
                return fNote;
            }
            set
            {
                SetPropertyValue<string>("Note", ref fNote, value);
            }
        }

        //private RegistroDataImportTentativi fRegistroDataImportTentativi;
        //[Size(250), Persistent("DESCRIZIONE_TENTATIVO")]
        //[DbType("varchar(250)")]
        //[Association(@"RegistroDataImportTentativi.ResultImportApparato")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.RegistroDataImportTentativi", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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


        [Association(@"RegistroDataImportTentativi.ResultImportApparato")]
        [Persistent("REGDATAIMPORTTENTATIVI"), DisplayName("RegistroDataImportTentativi")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.RegistroDataImportTentativi", DefaultContexts.Save, "Il Registro Tentativo Data Import è un campo obbligatorio")]
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

        [Persistent("APPARATO"), DevExpress.Xpo.DisplayName("Apparato")]
        //[RuleRequiredField("RuleReq.ResultImportEdificio.Immobile", DefaultContexts.Save, "Il Commessa è un campo obbligatorio")]
        [Delayed(true)]
        public Asset Apparato
        {
            get { return GetDelayedPropertyValue<Asset>("Apparato"); }
            set { SetDelayedPropertyValue<Asset>("Apparato", value); }
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
