
using System.Collections.Generic;
using System.Linq;
using System;
using CAMS.Module.DBAngrafica;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using CAMS.Module.Classi;
using DevExpress.Persistent.Validation;
using CAMS.Module.Validation;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;

namespace CAMS.Module.DBDataImport
{
    [DefaultClassOptions, Persistent("REGDTIMPORTEDIFICIO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Result Import Immobile")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [NavigationItem("Data Import")]
    [ImageName("BO_Opportunity")]
    //[RuleIsReferenced("RuleIsRef.ResultImportEdificio.SistemaTecnologico", DefaultContexts.Save, typeof(SistemaClassi), "SistemaTecnologico", InvertResult = false, MessageTemplateMustBeReferenced = "If you want to delete this object, you must be sure it is not referenced in any other object.")]
    [VisibleInDashboards(false)]
    public class ResultImportEdificio : XPObject
    {
        public ResultImportEdificio()
            : base()
        {
        }

        public ResultImportEdificio(Session session)
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
        [RuleRequiredField("RuleReq.ResultImportEdificio.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportEdificio.Descrizione.250", DefaultContexts.Save, 250, StringLengthComparisonMode.NotMoreThan)]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }


        private int fCommessaID;
        [Persistent("IDX_COMMESSA")]
        [DevExpress.Xpo.DisplayName("Indice Commessa")]
        [RuleRequiredField("RuleReq.ResultImportEdificio.CommessaID", DefaultContexts.Save, "L'Indice della Commessa è un campo obbligatorio")]
        [RuleValueComparison("RuleCriteria.ResultImportEdificio.CommessaID.negativo", DefaultContexts.Save, ValueComparisonType.LessThan, 0)]
        public int CommessaID
        {
            get { return fCommessaID; }
            set { SetPropertyValue<int>("CommessaID", ref fCommessaID, value); }
        }

        private int fSistemaTecnologicoID;
        [Persistent("IDX_SISTEMATECNOLOGICO")]
        [DevExpress.Xpo.DisplayName("Indice SistemaTecnologico")]
        [RuleRequiredField("RuleReq.ResultImportEdificio.SistemaTecnologicoID", DefaultContexts.Save, "L'Indice del SistemaTecnologico è un campo obbligatorio")]
        //[RuleIsReferenced("Rule.IsReferencedObject.AreaPoloID", DefaultContexts.Delete, typeof(RuleIsReferencedObject), "ReferencedObject", InvertResult = true, MessageTemplateMustBeReferenced = "If you want to delete this object, you must be sure it is not referenced in any other object.")]
        [RuleValueComparison("RuleCriteria.ResultImportEdificio.SistemaTecnologicoID.positivo", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)]
        public int SistemaTecnologicoID
        {
            get { return fSistemaTecnologicoID; }
            set {
                if (SetPropertyValue<int>("SistemaTecnologicoID", ref fSistemaTecnologicoID, value))
                {
                    if(!this.IsLoading)
                    SistemaTecnologico = Session.GetObjectByKey<SistemaTecnologico>(value);
                };
            }
        }

        private SistemaTecnologico sistemaTecnologico;
        //[Persistent("SISTEMATECNOLOGICO")]
        [NonPersistent]
        [RuleRequiredField("RuleReq.ResultImportEdificio.SistemaTecnologico", DefaultContexts.Save, "L'Indice del SistemaTecnologico è un campo obbligatorio")]
        public SistemaTecnologico SistemaTecnologico
        {
            get { return sistemaTecnologico; }
            set {
                SetPropertyValue<SistemaTecnologico>("SistemaTecnologico", ref sistemaTecnologico, value);
            }
        }
 

    private int fEdificioID;
        [Persistent("IDX_EDIFICIO")]
        [DevExpress.Xpo.DisplayName("Indice Immobile")]
        [RuleRequiredField("RuleReq.ResultImportEdificio.EdificioID", DefaultContexts.Save, "L'Indice dell' Immobile è un campo obbligatorio")]
        [RuleValueComparison("RuleCriteria.ResultImportEdificio.EdificioID.negativo", DefaultContexts.Save, ValueComparisonType.LessThan, 0)]
        public int EdificioID
        {
            get { return fEdificioID; }
            set { SetPropertyValue<int>("EdificioID", ref fEdificioID, value); }
        }


        private string fCodEdificio;
        [Size(50), Persistent("CODEDIFICIO")]
        [DevExpress.Xpo.DisplayName("Codice Immobile")]
        [DbType("varchar(50)")]
        [RuleRequiredField("RuleReq.ResultImportEdificio.CodEdificio", DefaultContexts.Save, "Il Codice Immobile è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportEdificio.CodEdificio.50", DefaultContexts.Save, 50, StringLengthComparisonMode.NotMoreThan)]
        public string CodEdificio
        {
            get { return fCodEdificio; }
            set { SetPropertyValue<string>("CodEdificio", ref fCodEdificio, value); }
        }


        private int fCentroOperativoID;
        [Persistent("IDX_CENTROOPERATIVO")]
        [DevExpress.Xpo.DisplayName("Indice Centro Operativo")]
        [RuleRequiredField("RuleReq.ResultImportEdificio.CentroOperativoID", DefaultContexts.Save, "L'Indice del Centro Operativo è un campo obbligatorio")]
        [RuleValueComparison("RuleCriteria.ResultImportEdificio.CentroOperativoID.positivo", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)]
        public int CentroOperativoID
        {
            get { return fCentroOperativoID; }
            set { SetPropertyValue<int>("CentroOperativoID", ref fCentroOperativoID, value); }
        }

        //CORRISPONDE a STRADA NELLA TABELLA INDIRIZZO
        private string fVia;
        [Size(100), Persistent("VIA")]
        [DevExpress.Xpo.DisplayName("Via")]
        [DbType("varchar(100)")]
        [RuleRequiredField("RuleReq.ResultImportEdificio.Via", DefaultContexts.Save, "La Via è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportEdificio.Via.100", DefaultContexts.Save, 100, StringLengthComparisonMode.NotMoreThan)]
        public string Via
        {
            get { return fVia; }
            set { SetPropertyValue<string>("Via", ref fVia, value); }
        }


        private string fCivico;
        [Size(50), Persistent("CIVICO")]
        [DevExpress.Xpo.DisplayName("Civico")]
        [DbType("varchar(50)")]
        // [RuleRequiredField("RuleReq.ResultImportEdificio.Civico", DefaultContexts.Save, "Il Civico è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportEdificio.Civico.50", DefaultContexts.Save, 50, StringLengthComparisonMode.NotMoreThan)]
        public string Civico
        {
            get { return fCivico; }
            set { SetPropertyValue<string>("Civico", ref fCivico, value); }
        }


        private string fCAP;
        [Size(50), Persistent("CAP")]
        [DevExpress.Xpo.DisplayName("CAP")]
        [DbType("varchar(50)")]
        //[RuleRequiredField("RuleReq.ResultImportEdificio.CAP", DefaultContexts.Save, "Il CAP è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportEdificio.Cap.50", DefaultContexts.Save, 50, StringLengthComparisonMode.NotMoreThan)]
        public string CAP
        {
            get { return fCAP; }
            set { SetPropertyValue<string>("CAP", ref fCAP, value); }
        }


        private int fComuneID;
        [Persistent("IDX_COMUNE")]
        [DevExpress.Xpo.DisplayName("Indice Comune")]
        [RuleRequiredField("RuleReq.ResultImportEdificio.ComuneID", DefaultContexts.Save, "L' Indice del Comune è un campo obbligatorio")]
        [RuleValueComparison("RuleCriteria.ResultImportEdificio.Comune.positivo", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)]
        public int ComuneID
        {
            get { return fComuneID; }
            set { SetPropertyValue<int>("ComuneID", ref fComuneID, value); }
        }

        private string fLatitudine;
        [Size(15), Persistent("LATITUDINE")]
        [DevExpress.Xpo.DisplayName("Latitudine")]
        [DbType("varchar(15)")]
        //[RuleRequiredField("RuleReq.ResultImportEdificio.Latitudine", DefaultContexts.Save, "La Latitudine è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportEdificio.Latitudine.15", DefaultContexts.Save, 15, StringLengthComparisonMode.NotMoreThan)]
        public string Latitudine
        {
            get { return fLatitudine; }
            set { SetPropertyValue<string>("Latitudine", ref fLatitudine, value); }
        }


        private string fLongitudine;
        [Size(15), Persistent("LONGITUDINE")]
        [DevExpress.Xpo.DisplayName("Longitudine")]
        [DbType("varchar(15)")]
        //[RuleRequiredField("RuleReq.ResultImportEdificio.Longitudine", DefaultContexts.Save, "La Longitudine è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportEdificio.Longitudine.15", DefaultContexts.Save, 15, StringLengthComparisonMode.NotMoreThan)]
        public string Longitudine
        {
            get { return fLongitudine; }
            set { SetPropertyValue<string>("Longitudine", ref fLongitudine, value); }
        }


        private string fNote;
        [Size(4000), Persistent("NOTE")]
        [DevExpress.Xpo.DisplayName("Note")]
        [DbType("varchar(4000)")]
        //[RuleRequiredField("RuleReq.ResultImportEdificio.Note", DefaultContexts.Save, "Le Note sono un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportEdificio.Note.4000", DefaultContexts.Save, 3999, StringLengthComparisonMode.NotMoreThan)]
        public string Note
        {
            get { return fNote; }
            set { SetPropertyValue<string>("Note", ref fNote, value); }
        }


        private string fRifReperibile;
        [Size(1000), Persistent("RIFERIMENTO REPERIBILE")]
        [DevExpress.Xpo.DisplayName("Riferimento Reperibile")]
        [DbType("varchar(1000)")]
        //[RuleRequiredField("RuleReq.ResultImportEdificio.RifReperibile", DefaultContexts.Save, "Il Riferimento del Reperibile è un campo obbligatorio")]
        [RuleStringLengthComparison("CustomRule.ResultImportEdificio.RiferimentoReperibile.4000", DefaultContexts.Save, 3999, StringLengthComparisonMode.NotMoreThan)]
        public string RifReperibile
        {
            get { return fRifReperibile; }
            set { SetPropertyValue<string>("RifReperibile", ref fRifReperibile, value); }
        }


        //da qui partono le collection


        [Association(@"RegistroDataImportTentativi.ResultImportEdificio")]
        [Persistent("REGDATAIMPORTTENTATIVI"), DisplayName("RegistroDataImportTentativi")]
        [RuleRequiredField("RuleReq.ResultImportEdificio.RegistroDataImportTentativi", DefaultContexts.Save, "Il Registro Tentativo Data Import è un campo obbligatorio")]
        [Delayed(true)]
        public RegistroDataImportTentativi RegistroDataImportTentativi
        {
            get { return GetDelayedPropertyValue<RegistroDataImportTentativi>("RegistroDataImportTentativi"); }
            set { SetDelayedPropertyValue<RegistroDataImportTentativi>("RegistroDataImportTentativi", value); }
        }

        [Persistent("IMMOBILE"), DevExpress.Xpo.DisplayName("Immobile")]
        //[RuleRequiredField("RuleReq.ResultImportEdificio.Immobile", DefaultContexts.Save, "Il Commessa è un campo obbligatorio")]
        [Delayed(true)]
        public Immobile Immobile
        {
            get { return GetDelayedPropertyValue<Immobile>("Immobile"); }
            set { SetDelayedPropertyValue<Immobile>("Immobile", value); }
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
