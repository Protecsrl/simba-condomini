using CAMS.Module.DBALibrary;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace CAMS.Module.DBTask.Guasti
{
    [DefaultClassOptions, Persistent(@"PCRAPPPROBLEMA")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", @"Problema associabile all'Apparato")]

    [RuleCriteria("RuleInfo.ApparatoProblema.cretocausarimedio", DefaultContexts.Save, @"[Creato_CompletoCausaRimedio]",
                 CustomMessageTemplate = "Informazione: La definizione di un Problema senza definire almeno una relativa Causa e Rimedio, Confermando si definirà in automatico Causa e Rimedio (Altro).",
                 SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Warning)]

    [Appearance("ApparatoProblema.Visibile", TargetItems = "UserIsAdminRuolo;Creato_CompletoCausaRimedio;AbilitaStdApp", Criteria = @"not(UserIsAdminRuolo)", Visibility = ViewItemVisibility.Hide)]

    [RuleCombinationOfPropertiesIsUnique("Unique.ApparatoProblema", DefaultContexts.Save, "StdApparato, Problemi")]
    [Appearance("ApparatoProblema.ProblemaCausa.NewDisabled", AppearanceItemType.Action, "1=1", TargetItems = "New", Enabled = false, Context = "ApparatoProblema_ProblemaCausas_ListView")]

    //   [Appearance("ApparatoProblema.RdLsHideLayoutItem", AppearanceItemType.LayoutItem,TargetItems = "UserIsAdminRuolo;Creato_CompletoCausaRimedio;Valore;AbilitaStdApp", "Oid = -1", Visibility = ViewItemVisibility.Hide)]
    [Appearance("ApparatoProblema.RdLsHideLayoutItem.inCreazione", AppearanceItemType.LayoutItem, @"Oid = -1 Or AbilitaStdApp", TargetItems = "RdLs", Visibility = ViewItemVisibility.Hide)]

    [NavigationItem("Ticket")]
    public class ApparatoProblema : XPObject
    {
        public const string NA = "N/A";
        public const string FormattazioneCodice = "{0:000}";

        public ApparatoProblema(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private Problemi fProblemi;
        [Persistent("PCRPROBLEMI"), DevExpress.Xpo.DisplayName("Problemi"), Association(@"StdApparatoProblema_Problemi")]
        [RuleRequiredField("RReqField.ApparatoProblema.Problemi", DefaultContexts.Save, "il Problema è un campo obbligatorio")]
        [DataSourceProperty("ListaFiltraComboProblemi", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectNothing)]
        [Appearance("ApparatoProblema.Problemi.Enabled.FontColor", Enabled = false, FontColor = "Black", FontStyle = FontStyle.Bold, Criteria = "ProblemaCausas.Count > 0")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public Problemi Problemi
        {
            get
            {
                return fProblemi;
            }
            set
            {
                SetPropertyValue<Problemi>("Problemi", ref fProblemi, value);
            }
        }

        //  [NonPersistent]
        private XPCollection<Problemi> fListaFiltraComboProblemi;
        [System.ComponentModel.Browsable(false)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<Problemi> ListaFiltraComboProblemi
        {
            get
            {//  if (this.Oid == -1) return null;
                if (this.StdApparato != null)
                {
                    fListaFiltraComboProblemi = new XPCollection<Problemi>(Session);

                    List<int> InQuery = Session.Query<ApparatoProblema>()
                               .Where(d => d.StdApparato == this.StdApparato)
                              .Select(s => s.Problemi.Oid).Distinct().ToList();


                    // List<int> problemiinstad = InQueryInTransaction.Concat<int>(InQuery).ToList();
                    List<int> problemiinstad = InQuery;
                    // List<int> problemiinstad = StdApparato.ApparatoProblemas.Select(s => s.Problemi.Oid).ToList();
                    CriteriaOperator charFilter = new InOperator("Oid", problemiinstad).Not();
                    fListaFiltraComboProblemi.Criteria = new GroupOperator(GroupOperatorType.And, charFilter);
                    OnChanged("ListaFiltraComboProblemi");
                }
                return fListaFiltraComboProblemi;
            }
        }

        private StdAsset fStdApparato;
        [Persistent("STDAPPARATO"), DevExpress.Xpo.DisplayName("Tipo Apparato"), Association(@"StdApparato_AppProblema")]
        [RuleRequiredField("RReqField.ApparatoProblema.StdApparato", DefaultContexts.Save, "Il Tipo Apparato è un campo obbligatorio")]
        [ImmediatePostData]
        [ToolTip("Valorizzando il tipo di Apparecchiatura saranno saranno selezionati i tipo di problema")]
        [Appearance("ApparatoProblema.StdApparato.Enabled.FontColor", Enabled = false, FontColor = "Black", FontStyle = FontStyle.Bold, Criteria = "AbilitaStdApp")]
        [ExplicitLoading()]
        public StdAsset StdApparato
        {
            get
            {
                return fStdApparato;
            }
            set
            {
                SetPropertyValue<StdAsset>("StdApparato", ref fStdApparato, value);
            }
        }

        [Association(@"AppProblema_Causa", typeof(ProblemaCausa)), Aggregated, DevExpress.Xpo.DisplayName("Elenco Cause Associate")]
        [Appearance(@"ApparatoProblema.ProblemaCausasHideLayoutItem", Criteria = @"[Oid] = -1 And [Problemi] Is Null", Enabled = false)]
        public XPCollection<ProblemaCausa> ProblemaCausas
        {
            get
            {
                return GetCollection<ProblemaCausa>("ProblemaCausas");
            }
        }

        //[Association(@"RdL_Problema", typeof(RdL)), DevExpress.Xpo.DisplayName("Elenco RdL")]
        //[ DevExpress.Xpo.DisplayName("Elenco RdL")]
        //public XPCollection<RdL> RdLs
        //{
        //    get
        //    {
        //        return GetCollection<RdL>("RdLs");
        //    }
        //}

        [NonPersistent]
        [Browsable(false)]
        public bool AbilitaStdApp { get; set; }

        [NonPersistent]
        [Browsable(false)]
        public bool Creato_CompletoCausaRimedio
        {
            get
            {
                if (this.ProblemaCausas.Count > 0)
                    if (this.ProblemaCausas[0].CausaRimedios.Count > 0)
                        return true;
                return false;
            }
        }

        [NonPersistent]
        [MemberDesignTimeVisibility(false)]
        public bool UserIsAdminRuolo
        {
            get
            {
                return CAMS.Module.Classi.SetVarSessione.IsAdminRuolo;
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            Debug.WriteLine(String.Format("e salvato {0} is saving {1}", IsInvalidated, IsSaving));
            if (Session.IsNewObject(this))
            {
                Debug.WriteLine(String.Format("e salvato {0} is saving {1}", IsInvalidated, IsSaving));
            }
        }
        protected override void OnSaved()
        {
            base.OnSaved();
            Debug.WriteLine(String.Format("e salvato {0} is saving {1}", IsInvalidated, IsSaving));
            if (Session.IsNewObject(this))
            {
                Debug.WriteLine(String.Format("e salvato {0} is saving {1}", IsInvalidated, IsSaving));
            }

        }

        //protected override void OnSaving()
        //{
        //    base.OnSaving();
        //    if (Session.IsNewObject(this))
        //    {
        //        if (this.ProblemaCausas != null)
        //        {    // Retrieve all Accessory objects expre
        //            if (ProblemaCausas.Count == 0)
        //            {
        //                this.ProblemaCausas.Add(new ProblemaCausa(Session)
        //                {
        //                    ApparatoProblema = this,
        //                    Cause = Session.FindObject<Cause>("")
        //                });
        //            }
        //            //var Cau = Session.Query<Cause>().Where(w => w.Descrizione.ToUpper().Contains("ALTRO")).FirstOrDefault();
        //            //var Rim = Session.Query<Rimedi>().Where(w => w.Descrizione.ToUpper().Contains("ALTRO")).FirstOrDefault();

        //            //var Cau = this.Session.GetObjects(Cause).Where(w => w.Descrizione.ToUpper().Contains("ALTRO")).FirstOrDefault();
        //            //var Rim = this.ObjectSpace.GetObjects<Rimedi>().Where(w => w.Descrizione.ToUpper().Contains("ALTRO")).FirstOrDefault();
        //            //((CAMS.Module.DBTask.Guasti.ApparatoProblema)(e.CreatedObject)).ProblemaCausas.Add(new ProblemaCausa(Sess)
        //            //{
        //            //    Cause = Sess.GetObjectByKey<Cause>(Cau.Oid),

        //            //    ApparatoProblema = Sess.GetObjectByKey<ApparatoProblema>(((CAMS.Module.DBTask.Guasti.ApparatoProblema)(e.CreatedObject)).Oid)
        //            //}


        //            //int clkD1 = Session.QueryInTransaction<KDimensione>()
        //            //        .Where(d => d.StandardApparato == this && d.Default == KDefault.Si).ToList().Count();
        //            //int clkD = Session.Query<KDimensione>()
        //            //            .Where(d => d.StandardApparato == this && d.Default == KDefault.Si).ToList().Count();
        //            //if (clkD < 1 || clkD1 < 1)
        //            //{

        //            //this.KDimensiones.Add(new KDimensione(Session)
        //            //{
        //            //    Descrizione = "Valore di Default",
        //            //    Default = KDefault.Si,
        //            //    StandardApparato = this,
        //            //    Valore = 1
        //            //});
        //            // this.Reload();
        //        }
        //    }
        //}

        
        public override string ToString()
        {
          //  return string.Format("{0}", Problemi);
            return string.Format("{0}", Problemi);
        }

    }
}

 




























