using System;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CAMS.Module.DBTask.Guasti
{
    [DefaultClassOptions,
    Persistent(@"PCRRIMEDI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Rimedi")]
    [RuleCombinationOfPropertiesIsUnique("Unique.Combination.Rimedi", DefaultContexts.Save, "Descrizione, CodDescrizione",
        MessageTemplateCombinationOfPropertiesMustBeUnique = " La descrizione ed il codice devono essere univoci!")]
    [NavigationItem("Ticket")]
    public class Rimedi : XPObject
    {
        public Rimedi()
            : base()
        {
        }

        public Rimedi(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public const string NA = "N/A";
        public const string FormattazioneCodice = "{0:000}";

        private string fDescrizione;
        [Size(150),        Persistent("DESCRIZIONE"),        DisplayName("Descrizione")]
        [DbType("varchar(150)")]
        [RuleRequiredField("RReqField.Rimedi.Descrizione", DefaultContexts.Save, "la Descrizione è un campo obbligatorio")]
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
        [Persistent("COD_DESCRIZIONE"),        Size(5),        DisplayName("Cod Descrizione")]
        [RuleRequiredField("RReqField.Rimedi.CodDescrizione", DefaultContexts.Save, "il Codice è un campo obbligatorio")]
        [DbType("varchar(5)")]
        public string CodDescrizione
        {
            get
            {
                if (!IsLoading && !IsSaving && fCodDescrizione == null)
                {
                    fCodDescrizione = NA;
                }
                return fCodDescrizione;
            }
            set
            {
                SetPropertyValue<string>("CodDescrizione", ref fCodDescrizione, value);
            }
        }

        private uint fValore;
        [Persistent("VALORE"),
        DisplayName("Valore")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public uint Valore
        {
            get
            {
                return fValore;
            }
            set
            {
                SetPropertyValue<uint>("Valore", ref fValore, value);
            }
        }
        //private byte fIcona;
        //[Persistent("ICONA"),
        //DisplayName("Icona")]
        //public byte Icona
        //{
        //    get
        //    {
        //        return fIcona;
        //    }
        //    set
        //    {
        //        SetPropertyValue<byte>("Icona", ref fIcona, value);
        //    }
        //}

        [Association(@"CausaRimedio_Rimedi", typeof(CausaRimedio)),
        DisplayName("Elenco Cause/Rimedi Associate")]
        public XPCollection<CausaRimedio> CausaRimedios
        {
            get
            {
                return GetCollection<CausaRimedio>("CausaRimedios");
            }
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", Descrizione, CodDescrizione);
        }
    }
}
