using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Drawing;
using DevExpress.Xpo.Metadata;
using CAMS.Module.DBTask.Guasti;
using DevExpress.ExpressApp;
using System.ComponentModel;

namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions, Persistent("ASSETSTD"), DevExpress.ExpressApp.Model.ModelDefault("Caption", "Tipo Asset")]
    [ImageName("ListBullets")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    //[Appearance("SuperPosRuleDisaEqstd", TargetItems = "Descrizione;CodDescrizione;Utente;DataAggiornamento", Criteria = "RuleDisableAction == 'True'", Enabled = false)]
    //[Appearance("SuperPosRuleVisyEqstd", TargetItems = "RuleDisableAction", Criteria = "1 == 1", Visibility = ViewItemVisibility.Hide)]
    [RuleCombinationOfPropertiesIsUnique("Unique.APPARATOSTD.Descrizione", DefaultContexts.Save, "CodDescrizione, Descrizione")]
    [NavigationItem("Procedure Attivita")]
    public class StdAsset : XPObject
    {
        private const double INT_ConstantMax = 1.99;
        private const double INT_ConstantMin = 1;

        public StdAsset()
            : base()
        {
        }
        public StdAsset(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";


        private string fCodUni;
        [Size(20), Persistent("COD_UNI")]
        [DbType("varchar(20)")]
        [RuleRequiredField("RuleReq.StdApparato.CodUni", DefaultContexts.Save, "Codice Uni è un campo obbligatorio")]
        public string CodUni
        {
            get
            {
                return fCodUni;
            }
            set
            {
                SetPropertyValue<string>("CodUni", ref fCodUni, value);
            }
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(100), DevExpress.Xpo.DisplayName("Descrizione")]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.StdApparato.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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
        [Persistent("COD_DESCRIZIONE"), Size(40), DevExpress.Xpo.DisplayName("Cod Descrizione")]
        [DbType("varchar(40)")]
        [RuleRequiredField("RReqField.StdApparato.CodDescrizione", DefaultContexts.Save, "Il Codice Descrizione è un campo obbligatorio")]
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

        [Persistent("IMAGEICONA")]
        //[DevExpress.Xpo.Size(SizeAttribute.Unlimited), ValueConverter(typeof(ImageValueConverter))]
        [DevExpress.Xpo.Size(SizeAttribute.Unlimited)]
        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 40)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        [Delayed(true)] //, MemberDesignTimeVisibility(false)
        public byte[] ImageIcona// public Image ImageIcona
        {         
            get
            {
                return GetDelayedPropertyValue<byte[]>("ImageIcona");
            }
            set
            {
                SetDelayedPropertyValue<byte[]>("ImageIcona", value);
            }
        }



        private string fNomeIconaMappa;
        [Persistent("NOMEICONAMAPPA"), Size(40), DevExpress.Xpo.DisplayName("Nome Icona Mappa")]
        [DbType("varchar(40)")]
        //[RuleRequiredField("RReqField.StdApparato.NomeIconaMappa", DefaultContexts.Save, "Il Codice Descrizione è un campo obbligatorio")]
        [VisibleInDetailView(true),VisibleInListView(false),VisibleInLookupListView(false)]
        [Appearance("stdapparato.NomeIconaMappa.nascondi", Criteria = @"!UserIsAdminRuolo", Visibility = ViewItemVisibility.Hide)]//
        public string NomeIconaMappa
        {
            get
            {
                return fNomeIconaMappa;
            }
            set
            {
                SetPropertyValue<string>("NomeIconaMappa", ref fNomeIconaMappa, value);
            }
        }

        [NonPersistent]
        [Browsable(false)]
        public bool UserIsAdminRuolo
        {
            get
            {
                return CAMS.Module.Classi.SetVarSessione.IsAdminRuolo;
            }
        }

        [Association(@"StdApparato_AppProblema", typeof(ApparatoProblema)), DevExpress.Xpo.DisplayName("Problemi di Guasto Associati")]
        public XPCollection<ApparatoProblema> ApparatoProblemas
        {
            get
            {
                return GetCollection<ApparatoProblema>("ApparatoProblemas");
            }
        }

        [Persistent("PCR_PROBLEMADEFAULT"), System.ComponentModel.DisplayName("Problema")]
        //[Appearance("RdL.Abilita.Prob", Criteria = "[Apparato] is null Or [ProblemaCausa] Is Not Null", FontColor = "Black", Enabled = false)]
        [ImmediatePostData(true), VisibleInListView(false)]      //  [DataSourceProperty("ListaFiltraApparatoProblemas", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        //[DataSourceCriteria("StdApparato = '@This.Apparato.StdApparato'")]  // 
        [DataSourceCriteria("[<ApparatoProblema>][^.Oid == Problemi.Oid And StdApparato == '@This.Oid']")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Problemi ProblemaDefault
        {
            get
            {
                return GetDelayedPropertyValue<Problemi>("ProblemaDefault");
            }
            set
            {
                SetDelayedPropertyValue<Problemi>("ProblemaDefault", value);
            }
        }

        //[Persistent("PCR_CAUSADEFAULT"), System.ComponentModel.DisplayName("Causa1")]
        ////[Appearance("RdL.Abilita.Prob", Criteria = "[Apparato] is null Or [ProblemaCausa] Is Not Null", FontColor = "Black", Enabled = false)]
        //[ImmediatePostData(true), VisibleInListView(false)]      //  [DataSourceProperty("ListaFiltraApparatoProblemas", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        ////[DataSourceCriteria("StdApparato = '@This.Apparato.StdApparato'")]  // 
        //[DataSourceCriteria("[<ProblemaCausa>][^.Oid == Cause.Oid And StdApparato == '@This.Oid']")]
        //[ExplicitLoading()]
        //[Delayed(true)]
        //public Cause CausaDefault
        //{
        //    get
        //    {
        //        return GetDelayedPropertyValue<Cause>("CausaDefault");
        //    }
        //    set
        //    {
        //        SetDelayedPropertyValue<Cause>("CausaDefault", value);
        //    }
        //}



        private string f_Utente;
        [Persistent("UTENTE"), Size(100), DevExpress.Xpo.DisplayName("Utente")]
        [DbType("varchar(100)")]
        [System.ComponentModel.Browsable(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public string Utente
        {
            get
            {
                return GetDelayedPropertyValue<string>("Utente");
                //return fStdApparatoClassi;
            }
            set
            {
                SetDelayedPropertyValue<string>("Utente", value);
                //SetPropertyValue<StdApparatoClassi>("StdApparatoClassi", ref fStdApparatoClassi, value);
            }    
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"), DevExpress.Xpo.DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [System.ComponentModel.Browsable(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public DateTime DataAggiornamento
        {
            get
            {
                return GetDelayedPropertyValue<DateTime>("DataAggiornamento");
                //return fStdApparatoClassi;
            }
            set
            {
                SetDelayedPropertyValue<DateTime>("DataAggiornamento", value);
                //SetPropertyValue<StdApparatoClassi>("StdApparatoClassi", ref fStdApparatoClassi, value);
            }

            //get
            //{
            //    return fDataAggiornamento;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value);
            //}
        }

        [Association(@"StdApparatoClassi_StdApparato"), Persistent("APPARATOSTDCLASSI"), DevExpress.Xpo.DisplayName("Classi Tipo Apparato")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public StdApparatoClassi StdApparatoClassi
        {
            get
            {
                return GetDelayedPropertyValue<StdApparatoClassi>("StdApparatoClassi");
                //return fStdApparatoClassi;
            }
            set
            {
                SetDelayedPropertyValue<StdApparatoClassi>("StdApparatoClassi", value);
                //SetPropertyValue<StdApparatoClassi>("StdApparatoClassi", ref fStdApparatoClassi, value);
            }
        }

        #region COEFFICIENTI CORRETTIVI DI DEFAULT

        private KGuasto fKGuasto;
        [Persistent("KGUASTO"), DevExpress.Xpo.DisplayName("KGuasto")]
        [ImmediatePostData]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public KGuasto KGuasto
        {
            get
            {
                return GetDelayedPropertyValue<KGuasto>("KGuasto");
            }
            set
            {
                SetDelayedPropertyValue<KGuasto>("KGuasto", value);

            }

            //get
            //{
            //    return fKGuasto;
            //}
            //set
            //{
            //    if (SetPropertyValue<KGuasto>("KGuasto", ref fKGuasto, value))
            //    {
            //        OnChanged("TempoTotOPT");
            //        OnChanged("TempoMTZ");
            //    }
            //}
        }

        private KCondizione fKCondizione;
        [Persistent("KCONDIZIONE"), DevExpress.Xpo.DisplayName("KCondizione")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public KCondizione KCondizione
        {
            get
            {
                return GetDelayedPropertyValue<KCondizione>("KCondizione");
            }
            set
            {
                SetDelayedPropertyValue<KCondizione>("KCondizione", value);

            }
            //get
            //{
            //    return fKCondizione;
            //}
            //set
            //{
            //    SetPropertyValue<KCondizione>("KCondizione", ref fKCondizione, value);
            //}
        }

        private KUtenza fKUtenza;
        [Persistent("KUTENZA"), DevExpress.Xpo.DisplayName("KUtenza")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public KUtenza KUtenza
        {
            get
            {
                return GetDelayedPropertyValue<KUtenza>("KUtenza");
            }
            set
            {
                SetDelayedPropertyValue<KUtenza>("KUtenza", value);
            }
            //get
            //{
            //    return fKUtenza;
            //}
            //set
            //{
            //    SetPropertyValue<KUtenza>("KUtenza", ref fKUtenza, value);
            //}
        }

        private KUbicazione fKUbicazione;
        [Persistent("KUBICAZIONE"), DevExpress.Xpo.DisplayName("Kubicazione")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public KUbicazione KUbicazione
        {
            get
            {
                return GetDelayedPropertyValue<KUbicazione>("KUbicazione");
            }
            set
            {
                SetDelayedPropertyValue<KUbicazione>("KUbicazione", value);
            }

            //get
            //{
            //    return fKUbicazione;
            //}
            //set
            //{
            //    SetPropertyValue<KUbicazione>("KUbicazione", ref fKUbicazione, value);
            //}
        }

        private KTrasferimento fKTrasferimento;
        [Persistent("KTRASFERIMENTO"), DevExpress.Xpo.DisplayName("KTrasferimento")]
        // [RuleRange("RuleRangeObject.SchedaMp.KTrasferimento", "Save", INT_ConstantMin, INT_ConstantMax)]
        [MemberDesignTimeVisibility(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public KTrasferimento KTrasferimento
        {
            get
            {
                return GetDelayedPropertyValue<KTrasferimento>("KTrasferimento");
            }
            set
            {
                SetDelayedPropertyValue<KTrasferimento>("KTrasferimento", value);
            }

            //get
            //{
            //    return fKTrasferimento;
            //}
            //set
            //{
            //    SetPropertyValue<KTrasferimento>("KTrasferimento", ref fKTrasferimento, value);
            //}
        }

        private KOttimizzazione fKOttimizzazione;
        [Persistent("KOTTIMIZZAZIONE"), DevExpress.Xpo.DisplayName("KOttimizzazione")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public KOttimizzazione KOttimizzazione
        {
            get
            {
                return GetDelayedPropertyValue<KOttimizzazione>("KOttimizzazione");
            }
            set
            {
                SetDelayedPropertyValue<KOttimizzazione>("KOttimizzazione", value);
            }

            //get
            //{
            //    return fKOttimizzazione;
            //}
            //set
            //{
            //    SetPropertyValue<KOttimizzazione>("KOttimizzazione", ref fKOttimizzazione, value);
            //}
        }

        #endregion

        [NonPersistent, DevExpress.Xpo.DisplayName("K Totale"), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]//Context = "DetailView"
        [Appearance("StdApparato.kTotale.Visible", Criteria = "KTotale==0", Visibility = ViewItemVisibility.Hide, Context = "DetailView")]
        [ExplicitLoading()]
        public double KTotale
        {
            get
            {
                if (this.Oid != -1)
                {
                    double somma = 1;
                    foreach (var kdim in KDimensiones)
                    {
                        somma += kdim.Valore;
                    }
                    var ContaK = (KCondizione != null ? KCondizione.Valore : 1) *
                        (KGuasto != null ? KGuasto.Valore : 1) *
                        (KOttimizzazione != null ? KOttimizzazione.Valore : 1) *
                       (KUtenza != null ? KUtenza.Valore : 1) *
                       (KUbicazione != null ? KUbicazione.Valore : 1) *
                       (KCondizione != null ? KCondizione.Valore : 1) * somma; //KGuasto.Valore * KOttimizzazione.Valore * KUtenza.Valore * KUbicazione.Valore
                    return ContaK;
                }
                return 0;
            }
        }

        [NonPersistent, DevExpress.Xpo.DisplayName("k Default")]
        [MemberDesignTimeVisibility(false)]
        [ExplicitLoading()]
        public String DescrizioneKTotale
        {
            get
            {
                var valkdim = "1";
                foreach (var kdim in KDimensiones)
                {
                    if (kdim.Default == Classi.KDefault.Si)
                    {
                        valkdim = kdim.Valore.ToString();
                        break;
                    }
                }
                var DescK = string.Format("kDm({0}),kCd({1}),kUb({2}),kGu({3}),kUt({4})", valkdim,
                    KCondizione == null ? 1 : KCondizione.Valore,
                    KUbicazione == null ? 1 : KUbicazione.Valore,
                    KGuasto == null ? 1 : KGuasto.Valore,
                    KUtenza == null ? 1 : KUtenza.Valore);
                return DescK;
            }
        }

        [Aggregated, Association(@"StdApparato_KDimensione", typeof(KDimensione)), DevExpress.Xpo.DisplayName("KDimensione")] //
        public XPCollection<KDimensione> KDimensiones { get { return GetCollection<KDimensione>("KDimensiones"); } }

        [Association(@"StdApparato_SchedeMP", typeof(SchedaMp)), DevExpress.Xpo.DisplayName("Procedure MP")] //Aggregated
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<SchedaMp> SchedaMps { get { return GetCollection<SchedaMp>("SchedaMps"); } }


        [Association(@"StdApparato_CaratteristicheTecniche", typeof(StdApparatoCaratteristicheTecniche)), Aggregated]
        [DevExpress.Xpo.DisplayName("Caratteristiche Tecniche")]
        public XPCollection<StdApparatoCaratteristicheTecniche> StdApparatoCaratteristicheTecniches
        {
            get { return GetCollection<StdApparatoCaratteristicheTecniche>("StdApparatoCaratteristicheTecniches"); }
        }

       


        #region Metodi
        //public override string ToString()
        //{
        //    return string.Format("{0}({1})", Descrizione, CodUni);
        //}

        //protected override void OnSaving()
        //{
        //    base.OnSaving();
        //    //if (Session.IsNewObject(this))
        //    //{
        //    //    if (this.KDimensiones != null)
        //    //    {    // Retrieve all Accessory objects expre
        //    //        int clkD1 = Session.QueryInTransaction<KDimensione>()
        //    //                .Where(d => d.StandardApparato == this && d.Default == KDefault.Si).ToList().Count();
        //    //        int clkD = Session.Query<KDimensione>()
        //    //                    .Where(d => d.StandardApparato == this && d.Default == KDefault.Si).ToList().Count();
        //    //        if (clkD < 1 || clkD1 < 1)
        //    //        {

        //    //            this.KDimensiones.Add(new KDimensione(Session)
        //    //        {
        //    //            Descrizione = "Valore di Default",
        //    //            Default = KDefault.Si,
        //    //            StandardApparato = this,
        //    //            Valore = 1
        //    //        });
        //    //            this.Reload();
        //    //        }
        //    //    }
        //    //}

        //}

        protected override void OnSaved()
        {
            base.OnSaved();
            //if (Session.IsNewObject(this))
            //{
            //    if (this.ImageIcona != null)
            //    {    // Retrieve all Accessory objects expre
            //        this.ImageIcona.Save(this.Oid.ToString());
            //    }
            //}
            //else
            //{
            //    if (this.ImageIcona != null)
            //    {    // Retrieve all Accessory objects expre
            //        this.ImageIcona.Save(this.Oid.ToString());
            //    }
            //}

        }
        #endregion

    }
}



//private void SetKCorrettivi(StdApparato _Eqstd)
//{
//    try
//    {
//        if (_Eqstd != null)
//        {
//            var db = new Classi.DB();
//            double kCorTm = 1;
//            //kCorTm = db.GetDefault(Classi.SetVarSessione.CorrenteUser, "KDIMENSIONE", _Eqstd.Oid);
//            //SetPropertyValue<double>("KDimensione", ref fKDimensione, kCorTm);
//            //kCorTm = db.GetDefault(Classi.SetVarSessione.CorrenteUser, "KCONDIZIONE", _Eqstd.Oid);
//            //SetPropertyValue<KCondizione>("KCondizione", ref fKCondizione, kCorTm);
//            //kCorTm = db.GetDefault(Classi.SetVarSessione.CorrenteUser, "KUBICAZIONE", _Eqstd.Oid);
//            //SetPropertyValue<KUbicazione>("KUbicazione", ref fKUbicazione, kCorTm);
//            //kCorTm = db.GetDefault(Classi.SetVarSessione.CorrenteUser, "KUTENZA", _Eqstd.Oid);
//            //SetPropertyValue<KUtenza>("KUtenza", ref fKUtenza, kCorTm);
//            //kCorTm = db.GetDefault(Classi.SetVarSessione.CorrenteUser, "KOTTIMIZZAZIONE", _Eqstd.Oid);
//            //SetPropertyValue<double>("KOttimizzazione", ref fKOttimizzazione, kCorTm);
//            //kCorTm = db.GetDefault(Classi.SetVarSessione.CorrenteUser, "KTRASFERIMENTO", _Eqstd.Oid);
//            //SetPropertyValue<double>("KTrasferimento", ref fKTrasferimento, kCorTm);
//        }
//    }
//    catch (Exception ex)
//    {
//        //DBALibrary.KDimensione = 1;
//        KCondizione.Valore = 1;
//        KUbicazione.Valore = 1;
//        KUtenza.Valore = 1;
//        KOttimizzazione.Valore = 1;
//        KGuasto.Valore = 1;
//        //DBALibrary.KTrasferimento = 1;
//        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
//    }
//}



//[NonPersistent]
//public bool RuleDisableAction
//{
//    get
//    {
//        return RuleMethod();
//    }
//}


//public bool RuleMethod()
//{
//    var caseSwitch = Classi.VarGlobali.CorrenteRoleApparence;
//    var caseInsert = Classi.VarGlobali.CorrenteRoleInserimento;

//    if (caseInsert == "Edit")
//    {
//        switch (caseSwitch)
//        {
//            case "OperatoreSTF":
//            case "RuoloSTF":
//            case "RuoloOPT":
//            case "OperatoreOPT":
//                return true;
//                break;
//            default:
//                return false;
//                break;
//        }
//    }
//    else
//    {
//        if (caseInsert == "Insert")
//        {
//            switch (caseSwitch)
//            {
//                case "OperatoreSTF":
//                case "RuoloSTF":
//                case "RuoloOPT":
//                case "OperatoreOPT":
//                    return false;
//                    break;
//                default:
//                    return false;
//                    break;
//            }
//        }
//        else
//        {
//            return false;
//        }
//    }
//    return false;
//}
