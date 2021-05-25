using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CAMS.Module.DBTask.Guasti
{
    [DefaultClassOptions,
    Persistent(@"PCRCAUSARIMEDIO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "CausaRimedio")]
    [RuleCombinationOfPropertiesIsUnique("Unique.Combination.CausaRimedio", DefaultContexts.Save, "ProblemaCausa, Rimedi")]
    [NavigationItem("Ticket")]
    public class CausaRimedio : XPObject
    {
        public const string NA = "N/A";
        public const string FormattazioneCodice = "{0:000}";

        public CausaRimedio(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Rimedi fRimedi;
        [Persistent("PCRRIMEDI"), DevExpress.Xpo.DisplayName("Rimedi"), Association(@"CausaRimedio_Rimedi")]
        [RuleRequiredField("RReqField.CausaRimedio.Rimedi", DefaultContexts.Save, "Rimedi è un campo obbligatorio")]
        [DataSourceProperty("ListaFiltraComboRimedi", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectNothing)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Rimedi Rimedi
        {

             get
            {
                return GetDelayedPropertyValue<Rimedi>("Rimedi");
            }
            set
            {
                SetDelayedPropertyValue<Rimedi>("Rimedi", value);
            }    

            //get
            //{
            //    return fRimedi;
            //}
            //set
            //{
            //    SetPropertyValue<Rimedi>("Rimedi", ref fRimedi, value);
            //}
        }
      

        private ProblemaCausa fProblemaCausa;
        [Association(@"ProblemaCausa_Rimedio"), Persistent("PCRPROBCAUSA"), DevExpress.Xpo.DisplayName("Causa")]
        [RuleRequiredField("RReqField.CausaRimedio.ProblemaCausa", DefaultContexts.Save, "La Causa è un campo obbligatorio")]
        [Appearance("CausaRimedio.ProblemaCausa.Enabled.FontColor", Enabled = false, FontColor = "Black", FontStyle = FontStyle.Bold)]
        [ExplicitLoading()]
        [Delayed(true)]
        public ProblemaCausa ProblemaCausa
        {
              get
            {
                return GetDelayedPropertyValue<ProblemaCausa>("ProblemaCausa");
            }
            set
            {
                SetDelayedPropertyValue<ProblemaCausa>("ProblemaCausa", value);
            }    

            //get
            //{
            //    return fProblemaCausa;
            //}
            //set
            //{
            //    SetPropertyValue<ProblemaCausa>("ProblemaCausa", ref fProblemaCausa, value);
            //}
        }

 

        private XPCollection<Rimedi> fListaFiltraComboRimedi;
        [System.ComponentModel.Browsable(false)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<Rimedi> ListaFiltraComboRimedi
        {
            get
            {//  if (this.Oid == -1) return null;
                if (this.ProblemaCausa != null)
                {
                    fListaFiltraComboRimedi = new XPCollection<Rimedi>(Session);
                    List<int> InQuery = Session.Query<CausaRimedio>()
                               .Where(d => d.ProblemaCausa == this.ProblemaCausa)
                              .Select(s => s.Rimedi.Oid).Distinct().ToList();
                    //List<int> InQueryInTransaction = Session.QueryInTransaction<CausaRimedio>()
                    //      .Where(d => d.ProblemaCausa == this.ProblemaCausa)
                    //     .Select(s => s.Rimedi.Oid).Distinct().ToList();
                   
                    //List<int> problemiinstad = InQueryInTransaction.Concat<int>(InQuery).ToList();

                    List<int> problemiinstad = InQuery;
                    // List<int> problemiinstad = StdApparato.ApparatoProblemas.Select(s => s.Problemi.Oid).ToList();
                    CriteriaOperator charFilter = new InOperator("Oid", problemiinstad).Not();
                    fListaFiltraComboRimedi.Criteria = new GroupOperator(GroupOperatorType.And, charFilter);
                    OnChanged("ListaFiltraComboRimedi");
                }
                return fListaFiltraComboRimedi;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} ", Rimedi);
           //return string.Format("{0}({1})", ProblemaCausa, Rimedi);
          //  return string.Format("{0}",ProblemaCausa.Cause.Descrizione, Rimedi.Descrizione);
        }
    }
}

  //private uint fValore;
  //      [Persistent("VALORE"), DevExpress.Xpo.DisplayName("Valore")]
  //      [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
  //      public uint Valore
  //      {
  //          get
  //          {
  //              return fValore;
  //          }
  //          set
  //          {
  //              SetPropertyValue<uint>("Valore", ref fValore, value);
  //          }
  //      }