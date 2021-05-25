using System;
using System.Collections.Generic;
using System.Linq;
using CAMS.Module.Classi;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;

using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions, Persistent("MPGHOST")]
    [System.ComponentModel.DefaultProperty("Descrizione"), DevExpress.ExpressApp.Model.ModelDefault("Caption", "Ghost")]
    [ImageName("BO_Employee")]
    [NavigationItem("Planner")]
    public class Ghost : XPObject
    {
        public Ghost()
            : base()
        {
        }
        public Ghost(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(100)]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.Ghost.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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

        [Persistent("SCENARIO"), DisplayName("Scenario")]
        [VisibleInListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Scenario Scenario
        {
            get { return GetDelayedPropertyValue<Scenario>("Scenario"); }
            set { SetDelayedPropertyValue<Scenario>("Scenario", value); }
        }

        private Mansioni fMansione;
        [Persistent("MANSIONE")]
        [Delayed(true)]
        public Mansioni Mansione
        {
            get { return GetDelayedPropertyValue<Mansioni>("Mansione"); }
            set { SetDelayedPropertyValue<Mansioni>("Mansione", value); }
        }
        //public Mansioni Mansione
        //{
        //    get
        //    {
        //        return fMansione;
        //    }
        //    set
        //    {
        //        SetPropertyValue<Mansioni>("Mansione", ref fMansione, value);
        //    }
        //}


        // [NonPersistent]
        // [DevExpress.ExpressApp.DC.XafDisplayName("RisorseTeam Associati")]
        //[Appearance("RegRdL.GhostDettAssociati.ColorBlack", FontColor = "Black")]
        //public string RisorseTeamAssociati
        //{
        //    get
        //    {
        //        string lll = string.Join("; ", GhostDettaglis.Where(w => w.RisorseTeam != null).Select(s => s.RisorseTeam.FullName).Distinct().ToArray<string>());
        //        if (!string.IsNullOrEmpty( lll ))
        //        {
        //            return (string)lll.ToString();
        //        }
        //        else
        //        {
        //            return "na";
        //        }
        //    }
        //}

        private Double fUSLG;
        [Size(10), Persistent("USLG"), DisplayName("Unità Standard Lavoro Giornaliero")]
        [VisibleInListView(false)]
        [Delayed(true)]
        public Double USLG
        {
            get { return GetDelayedPropertyValue<Double>("USLG"); }
            set { SetDelayedPropertyValue<Double>("USLG", value); }
        }
        //public Double USLG
        //{
        //    get
        //    {
        //        return fUSLG;
        //    }
        //    set
        //    {
        //        SetPropertyValue<Double>("USLG", ref fUSLG, value);
        //    }
        //}

        private Double fUSLS;
        [Size(10), Persistent("USLS"), DisplayName("Unità Standard Lavoro Settimanale")]
        [VisibleInListView(false)]
        [Delayed(true)]
        public Double USLS
        {
            get { return GetDelayedPropertyValue<Double>("USLS"); }
            set { SetDelayedPropertyValue<Double>("USLS", value); }
        }
        //public Double USLS
        //{
        //    get
        //    {
        //        return fUSLS;
        //    }
        //    set
        //    {
        //        SetPropertyValue<Double>("USLS", ref fUSLS, value);
        //    }
        //}

        private TipoGhost fTipoGhost;
        [Persistent("TIPOGHOST"), DisplayName("Tipo Ghost")]
        [VisibleInListView(false)]
        public TipoGhost TipoGhost
        {
            get
            {
                return fTipoGhost;
            }
            set
            {
                SetPropertyValue<TipoGhost>("TipoGhost", ref fTipoGhost, value);
            }
        }

        //[XafDisplayName("Non Definito")]
        //NonDefinito,
        //[XafDisplayName("Sala Operativa")]
        //Sala_Operativa,
        //[XafDisplayName("Smartphone")]
        //Smartphone
        private TipoAssociazioneTRisorsa fTipoAssociazioneTRisorsa;
        [Persistent("TIPOASSOCIAZIONETEAM"), DisplayName("Tipo Associazione TRisorsa")]
        public TipoAssociazioneTRisorsa TipoAssociazioneTRisorsa
        {
            get
            {
                return fTipoAssociazioneTRisorsa;
            }
            set
            {
                SetPropertyValue<TipoAssociazioneTRisorsa>("TipoAssociazioneTRisorsa", ref fTipoAssociazioneTRisorsa, value);
            }
        }


        private int fAnno;
        [Persistent("ANNO")]
        [VisibleInListView(false)]
        public int Anno
        {
            get
            {
                return fAnno;
            }
            set
            {
                SetPropertyValue<int>("Anno", ref fAnno, value);
            }
        }


        private TipoNumeroManutentori fNumMan;
        [Persistent("TIPOASSEGNAZIONE"), DisplayName("Tipo Assegnazione")]
        public TipoNumeroManutentori NumMan
        {
            get
            {
                return fNumMan;
            }
            set
            {
                SetPropertyValue<TipoNumeroManutentori>("NumMan", ref fNumMan, value);
            }
        }

        /// <summary>
        /// ///////////////////////////////////

        #region tempi di carico
        //private int fCarico;
        [Persistent("CARICO"), DisplayName("Carico")]
        [Delayed(true)]
        public int Carico
        {
            get { return GetDelayedPropertyValue<int>("Carico"); }
            set { SetDelayedPropertyValue<int>("Carico", value); }
        }

        //private int fCaricoInSito;
        [Persistent("CARICO_INSITO"), DisplayName("Carico In Sito")]
        [Delayed(true)]
        public int CaricoInSito
        {
            get { return GetDelayedPropertyValue<int>("CaricoInSito"); }
            set { SetDelayedPropertyValue<int>("CaricoInSito", value); }
        }
        //public int CaricoInSito
        //{
        //    get
        //    {
        //        return fCaricoInSito;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("CaricoInSito", ref fCaricoInSito, value);
        //    }
        //}
        //private int fCaricoTrasferimento;
        [Persistent("CARICO_TRASFERIMENTO"), DisplayName("Carico Trasferimento")]
        [Delayed(true)]
        public int CaricoTrasferimento
        {
            get { return GetDelayedPropertyValue<int>("CaricoTrasferimento"); }
            set { SetDelayedPropertyValue<int>("CaricoTrasferimento", value); }
        }
        //public int CaricoTrasferimento
        //{
        //    get
        //    {
        //        return fCaricoTrasferimento;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("CaricoTrasferimento", ref fCaricoTrasferimento, value);
        //    }
        //}

        private Saturazione fSaturazione;
        [Persistent("SATURAZIONE"), DisplayName("Saturo")]
        [ModelDefault("AllowClear", "False")]
        public Saturazione Saturazione
        {
            get
            {


                return fSaturazione;
            }
            set
            {

                SetPropertyValue<Saturazione>("Saturazione", ref fSaturazione, value);
            }
        }



        private int fNrGiorniSaturi;
        [Persistent("NR_GIORNISATURI"), Size(5), DisplayName("Nr. Giorni Saturi")]
        //[Appearance("GhostDettaglio.fNrGiorniSaturi", Enabled = false)]
        [ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(true)]
        [Delayed(true)]
        public int NrGiorniSaturi
        {
            get { return GetDelayedPropertyValue<int>("NrGiorniSaturi"); }
            set { SetDelayedPropertyValue<int>("NrGiorniSaturi", value); }

        }
        //public int NrGiorniSaturi
        //{
        //    get
        //    {
        //        return fNrGiorniSaturi;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("NrGiorniSaturi", ref fNrGiorniSaturi, value);
        //    }
        //}


        private double fMinCarico;
        [Persistent("MIN")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public double MinCarico
        {
            get { return GetDelayedPropertyValue<double>("MinCarico"); }
            set { SetDelayedPropertyValue<double>("MinCarico", value); }

        }
        //public double MinCarico
        //{
        //    get
        //    {
        //        return fMinCarico;
        //    }
        //    set
        //    {
        //        SetPropertyValue<double>("MinCarico", ref fMinCarico, value);
        //    }
        //}

        private double fMaxCarico;
        [Persistent("MAX")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public double MaxCarico
        {
            get { return GetDelayedPropertyValue<double>("MaxCarico"); }
            set { SetDelayedPropertyValue<double>("MaxCarico", value); }

        }
        //public double MaxCarico
        //{
        //    get
        //    {
        //        return fMaxCarico;
        //    }
        //    set
        //    {
        //        SetPropertyValue<double>("MaxCarico", ref fMaxCarico, value);
        //    }
        //}

        private double fMediaCarico;
        [Persistent("MEDIA")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public double MediaCarico
        {
            get { return GetDelayedPropertyValue<double>("MediaCarico"); }
            set { SetDelayedPropertyValue<double>("MediaCarico", value); }

        }
        //public double MediaCarico
        //{
        //    get
        //    {
        //        return fMediaCarico;
        //    }
        //    set
        //    {
        //        SetPropertyValue<double>("MediaCarico", ref fMediaCarico, value);
        //    }
        //}

        private double fModaCarico;
        [Persistent("MODA")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public double ModaCarico
        {
            get { return GetDelayedPropertyValue<double>("ModaCarico"); }
            set { SetDelayedPropertyValue<double>("ModaCarico", value); }

        }
        //public double ModaCarico
        //{
        //    get
        //    {
        //        return fModaCarico;
        //    }
        //    set
        //    {
        //        SetPropertyValue<double>("ModaCarico", ref fModaCarico, value);
        //    }
        //}

        private double fMedianaCarico;
        [Persistent("MEDIANA")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public double MedianaCarico
        {
            get { return GetDelayedPropertyValue<double>("MedianaCarico"); }
            set { SetDelayedPropertyValue<double>("MedianaCarico", value); }

        }
        //public double MedianaCarico
        //{
        //    get
        //    {
        //        return fMedianaCarico;
        //    }
        //    set
        //    {
        //        SetPropertyValue<double>("MedianaCarico", ref fMedianaCarico, value);
        //    }
        //}

        private double fRatioTrasferimento;
        [Persistent("RATIO_TRASFERIMENTO")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", @"p")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", @"{0:p}")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public double RatioTrasferimento
        {
            get { return GetDelayedPropertyValue<double>("RatioTrasferimento"); }
            set { SetDelayedPropertyValue<double>("RatioTrasferimento", value); }

        }
        //public double RatioTrasferimento
        //{
        //    get
        //    {
        //        return fRatioTrasferimento;
        //    }
        //    set
        //    {
        //        SetPropertyValue<double>("RatioTrasferimento", ref fRatioTrasferimento, value);
        //    }
        //}

        private double fRatioInSito;//p
        [Persistent("RATIO_INSITO")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", @"p")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", @"{0:p}")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public double RatioInSito
        {
            get { return GetDelayedPropertyValue<double>("RatioInSito"); }
            set { SetDelayedPropertyValue<double>("RatioInSito", value); }

        }
        //public double RatioInSito
        //{
        //    get
        //    {
        //        return fRatioInSito;
        //    }
        //    set
        //    {
        //        SetPropertyValue<double>("RatioInSito", ref fRatioInSito, value);
        //    }
        //}

        #endregion


        [Association("Ghost_Dettaglio", typeof(GhostDettaglio)), DisplayName("Ghost Settimanali")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<GhostDettaglio> GhostDettaglis
        {
            get
            {
                return GetCollection<GhostDettaglio>("GhostDettaglis");
            }
        }
        private RegPianificazioneMP fRegistroPianificazioneMP;
        [MemberDesignTimeVisibility(false), Association(@"RegPianiMP_Ghost", typeof(RegPianificazioneMP))]
        [Persistent("MPREGPIANIFICAZIONE"), DisplayName("Registro Pianificazione MP")]
        [Delayed(true)]
        public RegPianificazioneMP RegistroPianificazioneMP
        {
            get { return GetDelayedPropertyValue<RegPianificazioneMP>("RegistroPianificazioneMP"); }
            set { SetDelayedPropertyValue<RegPianificazioneMP>("RegistroPianificazioneMP", value); }

        }
        //public RegPianificazioneMP RegistroPianificazioneMP
        //{
        //    get
        //    {
        //        return fRegistroPianificazioneMP;
        //    }
        //    set
        //    {
        //        SetPropertyValue<RegPianificazioneMP>("RegistroPianificazioneMP", ref fRegistroPianificazioneMP, value);
        //    }
        //}

        //[NonPersistent, DisplayName("Assegnato"), VisibleInListView(true), VisibleInDetailView(true)]
        //public GhostAssegnataaTeam GhostAssegnato
        //{
        //    get
        //    {
        //        var IsRisosrse = GhostAssegnataaTeam.NonAssegnato;
        //        int contart = Session.QueryInTransaction<GhostDettaglio>()
        //            .Where(w=>w.Ghost == this && w.RisorseTeam != null )
        //            .Select(s=>s.RisorseTeam)
        //            .Distinct()
        //            .Count();
        //        if (contart >0)//RisorseTeames.Count 
        //        {
        //            IsRisosrse = GhostAssegnataaTeam.Assegnato;
        //        }
        //        return IsRisosrse;
        //    }
        //}

        [DevExpress.ExpressApp.DC.XafDisplayName("Assegnato")]
        //  [PersistentAlias("Iif(TotaleFondo is null,0,TotaleFondo - [<RegistroCostiDettaglio>][^.Oid = RegistroCosti.FondoCosti.Oid].Sum(ImpManodopera) - [<RegistroCostiDettaglio>][^.Oid = RegistroCosti.FondoCosti.Oid].Sum(ImpMateriale))")]
        [PersistentAlias("Iif(GhostDettaglis[ RisorseTeam is null].Count > 0,'Non Assegnato', 'Assegnato')")]
        public string GhostAssegnato
        {
            get
            {
                var tempObject = EvaluateAlias("GhostAssegnato");
                if (tempObject != null)
                    return tempObject.ToString();
                else
                    return "Non Assegnato";
            }
        }

        //[Association(@"RisorseTeam_Ghost", typeof(RisorseTeam)), DisplayName("Risorsa Team")]   GhostDettaglis
        ////[Appearance("Ghost.RisorseTeam", Enabled = false)]
        //[VisibleInListView(false)]
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        //public XPCollection<RisorseTeam> RisorseTeames
        //{
        //    get
        //    {
        //        return GetCollection<RisorseTeam>("RisorseTeames").s;
        //    }
        //}
        //[Association(@"RisorseTeam_Ghost", typeof(RisorseTeam))]
        //[DisplayName("Risorsa Team")]   
        ////[Appearance("Ghost.RisorseTeam", Enabled = false)]
        //[VisibleInListView(false)]
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        //public XPCollection<RisorseTeam> RisorseTeames
        //{
        //    get
        //    {
        //        return GetCollection<RisorseTeam>("GhostDettaglis[].Single(RisorseTeam)");
        //    }
        //}
        [Association("Ghost_ANNO_AttivitaPianiDett", typeof(MpAttivitaPianificateDett)), Aggregated]
        //[NonPersistent]
        //private XPCollection<MpAttivitaPianificateDett> noAssociation;
        [DisplayName(@"Attività Pianificate in Dettaglio")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<MpAttivitaPianificateDett> MPAttivitaPianiDetts
        {
            //get
            //{
            //if (this.Oid == -1) return null;

            //if (noAssociation == null)
            //{
            //    noAssociation = new XPCollection<MpAttivitaPianificateDett>(Session, CriteriaOperator.Parse("Ghost.Oid == ?",this.Oid));
            //}
            //return noAssociation;
            get
            {
                return GetCollection<MpAttivitaPianificateDett>("MPAttivitaPianiDetts");
            }
            //}
        }
        //[NonPersistent]
        //[DevExpress.ExpressApp.DC.XafDisplayName("in lavorazione"), VisibleInDetailView(false), VisibleInListView(false)]
        //public bool inLavorazione
        //{
        //    get
        //    {
        //        if (IsInvalidated)
        //            return false;
        //        int Conta_in_Lav = MPAttivitaPianiDetts.Where(w => w.RdL != null).Count();
        //        if (Conta_in_Lav > 0)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //}

        //[NonPersistent]
        //[DevExpress.ExpressApp.DC.XafDisplayName("Stato Pianificazione")]
        //public string StatoPianificazione
        //{
        //    get
        //    {
        //        if (IsInvalidated)
        //            return string.Empty;

        //        var tempObject = Evaluate("inLavorazione");

        //        int Conta_in_Lav = MPAttivitaPianiDetts.Where(w => w.RdL != null).Count();
        //        if (tempObject != null)
        //        {
        //            if (Conta_in_Lav > 0)
        //            {
        //                return "in Smistamento";
        //            }
        //            else
        //            {
        //                tempObject = Evaluate("GhostAssegnato");
        //                if (tempObject != null)
        //                {
        //                    return tempObject.ToString();
        //                }

        //            }
        //        }
        //        else
        //        {
        //            tempObject = Evaluate("GhostAssegnato");
        //            if (tempObject != null)
        //            {
        //                return tempObject.ToString();
        //            }
        //        }
        //        return string.Empty;
        //    }
        //}

        //[DevExpress.ExpressApp.DC.XafDisplayName("Stato Pianificazione")]
        ////  [PersistentAlias("Iif(TotaleFondo is null,0,TotaleFondo - [<RegistroCostiDettaglio>][^.Oid = RegistroCosti.FondoCosti.Oid].Sum(ImpManodopera) - [<RegistroCostiDettaglio>][^.Oid = RegistroCosti.FondoCosti.Oid].Sum(ImpMateriale))")]
        ////[PersistentAlias("Iif(MPAttivitaPianiDetts[ RdL is null].Count > 0,'in Lavorazione', 'Assegnato')")]
        ////[PersistentAlias("Iif([<MpAttivitaPianificateDett>][^.Oid = Ghost.Oid And RdL is null].Count() > 0,'in Lavorazione', 'Assegnato')")]
        //public string StatoPianificazione
        //{
        //    //get
        //    //{
        //    //    var tempObject = EvaluateAlias("StatoPianificazione");
        //    //    if (tempObject != null)
        //    //        return tempObject.ToString();
        //    //    else
        //    //        return "Non Assegnato";
        //    //}
        //}

        //private string StatoPianificazione;
        [Persistent("STATOPIANIFICAZIONE"), Size(100)]
        [DbType("varchar(100)")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Stato Pianificazione")]
        [Delayed(true)]
        public string StatoPianificazione
        {
            get { return GetDelayedPropertyValue<string>("StatoPianificazione"); }
            set { SetDelayedPropertyValue<string>("StatoPianificazione", value); }
        }

        [DevExpress.Xpo.DisplayName("Nr Ghost Settimanali"), VisibleInLookupListView(false)]
        [PersistentAlias("GhostDettaglis.Count")]
        public int nrGhostSet
        {
            get
            {
                if (IsInvalidated)
                    return 0;
                var tempObject = EvaluateAlias("nrGhostSet");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} ", Descrizione, Mansione);
        }
    }
}








