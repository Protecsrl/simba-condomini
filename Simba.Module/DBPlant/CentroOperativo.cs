using System;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions,
    Persistent("CENTROOPERATIVO")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Centro Operativo")]
    [ImageName("Home")]
    [NavigationItem("Polo")]
    public class CentroOperativo : XPObject
    {
        public CentroOperativo()
            : base()
        {
        }
        public CentroOperativo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
        }

         [PersistentAlias("Iif(Risorses.Count > 0,true,false)")]
        //[Appearance("AbilitaCheckApp", Visibility = ViewItemVisibility.Hide, Criteria = "true", Context = "DetailView")]
       [MemberDesignTimeVisibility(false)]
        public bool CheckApp
        {              
            get
            {//QuantitaRisorse
                var tempObject = EvaluateAlias("CheckApp");
                if (tempObject != null)
                    return (bool)tempObject;
                else         
                return false;
            }
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"),
        Size(100),
        DisplayName("Descrizione")]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.CentroOperativo.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //[Appearance("AbilitaModificaDesc", Enabled = false, Criteria = "CheckApp", Context = "DetailView")]
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
        [Persistent("COD_DESCRIZIONE"),
        Size(10),
        DisplayName("Cod Descrizione")]
        [DbType("varchar(10)")]
        [RuleRequiredField("RReqField.CentroOperativo.CodDescrizione", DefaultContexts.Save, "Il Codice Descrizione è un campo obbligatorio")]
        //[Appearance("AbilitaModificaCodDesc", Enabled = false, Criteria = "CheckApp", Context = "DetailView")]
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

        private bool fVisflussoBreve;
        [Persistent("M_VISFLUSSOBREVE"), DisplayName("Gestione REGRDL APP BREVE ")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool VisflussoBreve
        {
            get
            {
                return fVisflussoBreve;
            }
            set
            {
                SetPropertyValue<bool>("VisflussoBreve", ref fVisflussoBreve, value);
            }
        }

        private int fAgoritmoPosizione;
        [Persistent("M_ALGORITMOPOSIZIONE"), DisplayName("Algoritmo Posizione")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public int AgoritmoPosizione
        {
            get
            {
                return fAgoritmoPosizione;
            }
            set
            {
                SetPropertyValue<int>("AgoritmoPosizione", ref fAgoritmoPosizione, value);
            }
        }

        private Double fUSLG;
        //[Appearance("AbilitaModificaUSGL", Enabled = false, Criteria = "CheckApp", Context = "DetailView")]
        [Size(10),
        Persistent("USLG"),
        DisplayName("Unità Standard Lavoro Giornaliero")]
        //[ExplicitLoading()]
        [Delayed(true)]
        public Double USLG
        {
            get { return GetDelayedPropertyValue<Double>("USLG"); }
            set { SetDelayedPropertyValue<Double>("USLG", value); }
            //get
            //{
            //    return fUSLG;
            //}
            //set
            //{
            //    SetPropertyValue<Double>("USLG", ref fUSLG, value);
            //}
        }

        private Double fUSLS;
        [Size(10),
        Persistent("USLS"),
        DisplayName("Unità Standard Lavoro Settimanale")]
        //[Appearance("AbilitaModificaUSLS", Enabled = false, Criteria = "CheckApp", Context = "DetailView")]
        [Delayed(true)]
        public Double USLS
        {
            get { return GetDelayedPropertyValue<Double>("USLS"); }
            set { SetDelayedPropertyValue<Double>("USLS", value); }
            //get
            //{
            //    return fUSLS;
            //}
            //set
            //{
            //    SetPropertyValue<Double>("USLS", ref fUSLS, value);
            //}
        }

        private Immobile fImmobile;
        [Persistent("IMMOBILE"), DisplayName("Immobile di ubicazione")]
        //[Appearance("AbilitaModificaEdificio", Enabled = false, Criteria = "CheckApp", Context = "DetailView")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Immobile Immobile
        {
            get { return GetDelayedPropertyValue<Immobile>("Immobile"); }
            set { SetDelayedPropertyValue<Immobile>("Immobile", value); }
           
        }
        private AreaDiPolo fAreaDiPolo;
        [RuleRequiredField("RReqField.CentroOperativo.AreaDiPolo", DefaultContexts.Save, "Area di Polo è un campo obbligatorio")]
        [Association(@"AreaDiPolo_CentroOperativo"),
        DisplayName("Area di Polo")]
        [Persistent("AREADIPOLO")]
        [ImmediatePostData(true)]
        [Appearance("AbilitaModificaAreaDiPolo", Enabled = false, Criteria = "CheckApp", Context = "DetailView")]
        [ExplicitLoading()]
        [Delayed(true)]
        public AreaDiPolo AreaDiPolo
        {
            get { return GetDelayedPropertyValue<AreaDiPolo>("AreaDiPolo"); }
            set {
                SetDelayedPropertyValue<AreaDiPolo>("AreaDiPolo", value);
                object TempSkill = value;
                if (TempSkill != null && Oid == -1)
                {
                    if (!IsSaving)
                    {
                        this.USLG = value.USLG;
                        this.USLS = value.USLS;
                        //SetPropertyValue<Double>("USLS", ref fUSLS, value.USLS);
                        //SetPropertyValue<Double>("USLG", ref fUSLG, value.USLG);
                    }
                }
            }
            //get
            //{
            //    return fAreaDiPolo;
            //}
            //set
            //{
            //    if (SetPropertyValue<AreaDiPolo>("AreaDiPolo", ref fAreaDiPolo, value))
            //    {
            //        object TempSkill = value;
            //        if (TempSkill != null && Oid == -1)
            //        {
            //            if (!IsSaving)
            //            {
            //                SetPropertyValue<Double>("USLS", ref fUSLS, value.USLS);
            //                SetPropertyValue<Double>("USLG", ref fUSLG, value.USLG);
            //            }
            //        }
            //    }
            //}
        }

        [Association(@"CentroOperativo_Risorse", typeof(Risorse)), DisplayName("Risorse del C.O.")]
        public XPCollection<Risorse> Risorses
        {
            get
            {
                return GetCollection<Risorse>("Risorses");
            }
        }

        //[NonPersistent,        DisplayName("Risorse Gestite")]
        //public int QuantitaRisorse
        //{
        //    get
        //    {
        //        var ContaRisorse = Risorses.Count;
        //        return ContaRisorse;
        //    }
        //}
        [PersistentAlias("Risorses.Count")]
        [DevExpress.Xpo.DisplayName("Risorse Gestite")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        public int QuantitaRisorse
        {
            get
            {
                object tempObject = EvaluateAlias("QuantitaRisorse");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else { return 0; }
            }
        }
        private DateTime fFestaSantoPatrono;
        [ModelDefault("DisplayFormat", "dd/MMM"),
        ModelDefault("EditMask", "D"),
        DisplayName("Festa Santo Patrono")]
        [Appearance("AbilitaModificaFestaPatrono", Enabled = true, Criteria = "CheckApp", Context = "DetailView")]
        [Delayed(true)]
        public DateTime FestaSantoPatrono
        {
            get { return GetDelayedPropertyValue<DateTime>("FestaSantoPatrono"); }
            set { SetDelayedPropertyValue<DateTime>("FestaSantoPatrono", value); }
            //get
            //{
            //    return fFestaSantoPatrono;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("FestaSantoPatrono", ref fFestaSantoPatrono, value);
            //}
        }

        [Association(@"CentroOperativo_Edificio", typeof(Immobile)), DisplayName("Edifici Associati")]
        public XPCollection<Immobile> COEdificis
        {
            get
            {
                return GetCollection<Immobile>("COEdificis");
            }
        }

        [Association(@"SCENARIORefCENTRIOPERATIVI", typeof(Scenario)),
        DisplayName("Scenario")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<Scenario> Scenari
        {
            get
            {
                return GetCollection<Scenario>("Scenari");
            }
        }

        [Association(@"Cluster_CO", typeof(ClusterEdifici)), DisplayName("Cluster Associati")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<ClusterEdifici> ClusterEdificis
        {
            get
            {
                return GetCollection<ClusterEdifici>("ClusterEdificis");
            }
        }

        //[PersistentAlias("CentroOperativo.Risorse"),        DisplayName("Risorse")]
        //[MemberDesignTimeVisibility(false)]
        //public Risorse Risorse
        //{
        //    get
        //    {
        //        var tempObject = EvaluateAlias("Risorse");
        //        if (tempObject != null)
        //        {
        //            return (Risorse)tempObject;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //[PersistentAlias("CentroOperativo.Impianto"),        DisplayName("Impianto")]
        //[MemberDesignTimeVisibility(false)]
        //[ExplicitLoading()]
        //public Impianto Impianto
        //{
        //    get
        //    {
        //        var tempObject = EvaluateAlias("Impianto");
        //        if (tempObject != null)
        //        {
        //            return (Impianto)tempObject;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //[PersistentAlias("CentroOperativo.Apparato"),        DisplayName("Apparato")]
        //[MemberDesignTimeVisibility(false)]
        //public Apparato Apparato
        //{
        //    get
        //    {
        //        var tempObject = EvaluateAlias("Apparato");
        //        if (tempObject != null)
        //        {
        //            return (Apparato)tempObject;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}       

    }
}
