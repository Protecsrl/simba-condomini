using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.Drawing;
namespace CAMS.Module.DBTask.Guasti
{
    [DefaultClassOptions,    Persistent(@"PCRPROBLEMI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Problemi")]
    [RuleCombinationOfPropertiesIsUnique("Unique.Combination.Problemi", DefaultContexts.Save, "Descrizione, CodDescrizione",
        MessageTemplateCombinationOfPropertiesMustBeUnique=" La descrizione ed il codice devono essere univoci!")]
    [NavigationItem("Ticket")]
    [Appearance("Problemi.inCreazione.noVisibile", TargetItems = "ApparatoProblemas", Criteria = @"Oid == -1 And DisabilitaApparatoProblemas", Visibility = ViewItemVisibility.Hide)]

    public class Problemi : XPObject
    {
        public Problemi()
            : base()
        {
        }

        public Problemi(Session session)
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
        [RuleRequiredField("RReqField.Problemi.Descrizione", DefaultContexts.Save, "la Descrizione è un campo obbligatorio")]
        [DbType("varchar(150)")]
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
        [RuleRequiredField("RReqField.Problemi.CodDescrizione", DefaultContexts.Save, "il Codice è un campo obbligatorio")]
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
        [Persistent("VALORE"),        DisplayName("Valore")]
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

        [Association(@"StdApparatoProblema_Problemi", typeof(ApparatoProblema)),
        DisplayName("Elenco ApparatoProblemi Associate")]
        public XPCollection<ApparatoProblema> ApparatoProblemas
        {
            get
            {
                return GetCollection<ApparatoProblema>("ApparatoProblemas");
            }
        }

        [NonPersistent]
        [System.ComponentModel.Browsable(false)]
        public bool DisabilitaApparatoProblemas { get; set; }



        public override string ToString()
        {
            return string.Format("{0}({1})", Descrizione, CodDescrizione);
        }
    }
}
