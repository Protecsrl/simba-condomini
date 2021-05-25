using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace CAMS.Module.DBTask.Guasti
{
    [DefaultClassOptions,
    Persistent(@"PCRPROBCAUSA")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "ProblemaCausa")]
    [RuleCombinationOfPropertiesIsUnique("Unique.Combination.ProblemaCausa", DefaultContexts.Save, "ApparatoProblema, Cause")]
    [Appearance("ProblemaCausa.NewDisabled", AppearanceItemType.Action, "CausaRimedios.Count() > 0", TargetItems = "New", Enabled = false, Context = "ApparatoProblema_ProblemaCausas_ListView")]

    [RuleCriteria("RuleInfo.ProblemaCausa.Creato_CausaRimedio", DefaultContexts.Save, @"[Creato_CausaRimedio]",
             CustomMessageTemplate = "Informazione: La definizione di una Causa necessita della definizione di un rimedio.",
             SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Warning)]

    [Appearance("ProblemaCausa.RdLsHideLayoutItem.inCreazione", AppearanceItemType.LayoutItem, @"Oid = -1 Or [Creato_CausaRimedio]", TargetItems = "RdLs", Visibility = ViewItemVisibility.Hide)]

    [NavigationItem("Ticket")]
    public class ProblemaCausa : XPObject
    {
        public const string NA = "N/A";
        public const string FormattazioneCodice = "{0:000}";

        public ProblemaCausa(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Cause fCause;
        [Persistent("PCRCAUSE"),        DevExpress.Xpo.DisplayName("Cause"),        Association(@"ProblemaCausa_Cause")]
        [RuleRequiredField("RReqField.ProblemaCausa.Cause", DefaultContexts.Save, "La Causa è un campo obbligatorio")]
        [DataSourceProperty("ListaFiltraComboCause", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectNothing)]
        [Appearance("ProblemaCausa.Cause.Enabled.FontColor", Enabled = false, FontColor = "Black", FontStyle = FontStyle.Bold, Criteria = "CausaRimedios.Count > 0")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Cause Cause
        {
            get { return GetDelayedPropertyValue<Cause>("Cause"); }
            set { SetDelayedPropertyValue<Cause>("Cause", value); }
            //get
            //{
               
            //    return fCause;
            //}
            //set
            //{
            //    SetPropertyValue<Cause>("Cause", ref fCause, value);
            //}
        }

        private ApparatoProblema fApparatoProblema;
        [Association(@"AppProblema_Causa"), Persistent("PCRAPPPROBLEMA"), DevExpress.Xpo.DisplayName("Problema")]
        [RuleRequiredField("RReqField.ProblemaCausa.ApparatoProblema", DefaultContexts.Save, "il Problema è un campo obbligatorio")]
        [Appearance("ProblemaCausa.ApparatoProblema.Enabled.FontColor", Enabled = false, FontColor = "Black", FontStyle = FontStyle.Bold)]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public ApparatoProblema ApparatoProblema
        {
            get { return GetDelayedPropertyValue<ApparatoProblema>("ApparatoProblema"); }
            set { SetDelayedPropertyValue<ApparatoProblema>("ApparatoProblema", value); }
            //get
            //{
            //    return fApparatoProblema;
            //}
            //set
            //{
            //    SetPropertyValue<ApparatoProblema>("ApparatoProblema", ref fApparatoProblema, value);
            //}
        }
        [Association(@"ProblemaCausa_Rimedio", typeof(CausaRimedio)), Aggregated, DevExpress.Xpo.DisplayName("Elenco Rimedio")]
        [Appearance(@"ProblemaCausa.CausaRimediosideLayoutItem", Criteria = @"[Oid] = -1 And [Cause] Is Null", Enabled = false)]
        public XPCollection<CausaRimedio> CausaRimedios
        {
            get
            {
                return GetCollection<CausaRimedio>("CausaRimedios");
            }
        }

        //[Association(@"RdL_Causa", typeof(RdL)), DevExpress.Xpo.DisplayName("Elenco RdL")]
      //  [DevExpress.Xpo.DisplayName("Elenco RdL")]
      ////  [Appearance("ApparatoProblema.RdLsHideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1", Visibility = ViewItemVisibility.Hide)]
      //  public XPCollection<RdL> RdLs
      //  {
      //      get
      //      {
      //          return GetCollection<RdL>("RdLs");
      //      }
      //  }


        //  [NonPersistent]
        private XPCollection<Cause> fListaFiltraComboCause;
        [System.ComponentModel.Browsable(false)]
        public XPCollection<Cause> ListaFiltraComboCause
        {
            get
            {//  if (this.Oid == -1) return null;
                if (this.ApparatoProblema != null)
                {
                    fListaFiltraComboCause = new XPCollection<Cause>(Session);
                    List<int> InQuery = Session.Query<ProblemaCausa>()
                               .Where(d => d.ApparatoProblema == this.ApparatoProblema)
                              .Select(s => s.Cause.Oid).Distinct().ToList();
                    List<int> problemiinstad = InQuery;
                    //List<int> InQueryInTransaction = Session.QueryInTransaction<ProblemaCausa>()
                    //           .Where(d => d.ApparatoProblema == this.ApparatoProblema)
                    //          .Select(s => s.Cause.Oid).Distinct().ToList();
                    //List<int> InQueryInTransaction = this.ApparatoProblema..QueryInTransaction<ProblemaCausa>()
                    //         .Where(d => d.ApparatoProblema == this.ApparatoProblema)
                    //        .Select(s => s.Cause.Oid).Distinct().ToList();
                    // 
                    if (this.ApparatoProblema.ProblemaCausas.Count > 0)
                    {
                        List<int> InQueryInTransaction = this.ApparatoProblema.ProblemaCausas.
                            Where(w => w.ApparatoProblema == this.ApparatoProblema && w.Cause != null).
                            Select(s => s.Cause.Oid).Distinct().ToList();
                        //{DevExpress.Xpo.XPCollection`1[[CAMS.Module.DBTask.Guasti.ProblemaCausa, CAMS.Module, Version=1.1.1.513, Culture=neutral, PublicKeyToken=null]](2149) Count(2)}	DevExpress.Xpo.XPCollection<CAMS.Module.DBTask.Guasti.ProblemaCausa>

                          problemiinstad = InQueryInTransaction.Concat<int>(InQuery).ToList();
                        //
                    }
                    

                    CriteriaOperator charFilter = new InOperator("Oid", problemiinstad).Not();
                    fListaFiltraComboCause.Criteria = new GroupOperator(GroupOperatorType.And, charFilter);
                    OnChanged("ListaFiltraComboCause");
                }
                return fListaFiltraComboCause;
            }
        }

        [NonPersistent]
        [Browsable(false)]
        public bool Creato_CausaRimedio
        {
            get
            {
                if (this.CausaRimedios.Count > 0)                    
                        return true;
                return false;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}",  Cause);
            //return string.Format("{0}({1})", ApparatoProblema, Cause);
           // return string.Format("{0}/{1}",ApparatoProblema.Problemi.Descrizione, Cause.Descrizione);
        }
    }
}




//private uint fValore;
//[Persistent("VALORE"), DevExpress.Xpo.DisplayName("Valore")]
//[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
//public uint Valore
//{
//    get
//    {
//        return fValore;
//    }
//    set
//    {
//        SetPropertyValue<uint>("Valore", ref fValore, value);
//    }
//}




















