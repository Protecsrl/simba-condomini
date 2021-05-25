using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;


namespace CAMS.Module.DBAgenda
{
    //[DefaultClassOptions, Persistent("ATTIVITAEVENT")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
    //[DevExpress.ExpressApp.Model.ModelDefault("Caption", "Agenda Lavoro")]
    //[ImageName("ProjectFile")]
    //[NavigationItem(true)]

    public class AttivitaEvent //:  Event  
    {

   //     public AttivitaEvent(Session session) : base(session) { }
   //     [RuleRequiredField("", "SchedulerValidation")]
   //     public string Notes {
   //         get { return GetPropertyValue<string>("Notes"); }
   //         set { SetPropertyValue<string>("Notes", value); }
   //     }

   ////     public override int Status { get; set; }

   //     protected override void OnSaving() {
   //         base.OnSaving();
   //         Validator.RuleSet.Validate(XPObjectSpace.FindObjectSpaceByObject(this), this, "SchedulerValidation");
   //     }

    }
}

 

//namespace CustomSchedulerEvent.Module {
//    [DefaultClassOptions]
//    public class ExtendedEvent : Event {
//        public ExtendedEvent(Session session) : base(session) { }
//        [RuleRequiredField("", "SchedulerValidation")]
//        public string Notes {
//            get { return GetPropertyValue<string>("Notes"); }
//            set { SetPropertyValue<string>("Notes", value); }
//        }
//        protected override void OnSaving() {
//            base.OnSaving();
//            Validator.RuleSet.Validate(XPObjectSpace.FindObjectSpaceByObject(this), this, "SchedulerValidation");
//        }
//    }

//}


//namespace WinWebSolution.Module {
//    [DefaultClassOptions]
//    public class Activity : BasePersistentObject, IEvent, IRecurrentEvent {

//        [Association("Activity-Employees", UseAssociationNameAsIntermediateTableName = true)]
//        public XPCollection<Employee> Employees {
//            get { return GetCollection<Employee>("Employees"); }
//        }
//        protected override XPCollection<T> CreateCollection<T>(XPMemberInfo property) {
//            XPCollection<T> result = base.CreateCollection<T>(property);
//            if (property.Name == "Employees") {
//                result.ListChanged += Employees_ListChanged;
//            }
//            return result;
//        }
//        public void UpdateEmployeeIds() {
//            _EmployeeIds = string.Empty;
//            foreach (Employee activityUser in Employees) {
//                _EmployeeIds += String.Format(@"<ResourceId Type=""{0}"" Value=""{1}"" />", activityUser.Id.GetType().FullName, activityUser.Id);
//            }
//            _EmployeeIds = String.Format("<ResourceIds>{0}</ResourceIds>", _EmployeeIds);
//        }
//        private void UpdateEmployees() {
//            Employees.SuspendChangedEvents();
//            try {
//                while (Employees.Count > 0)
//                    Employees.Remove(Employees[0]);
//                if (!String.IsNullOrEmpty(_EmployeeIds)) {
//                    XmlDocument xmlDocument = new XmlDocument();
//                    xmlDocument.LoadXml(_EmployeeIds);
//                    foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes) {
//                        Employee activityUser = Session.GetObjectByKey<Employee>(new Guid(xmlNode.Attributes["Value"].Value));
//                        if (activityUser != null)
//                            Employees.Add(activityUser);
//                    }
//                }
//            } finally {
//                Employees.ResumeChangedEvents();
//            }
//        }
//        private void Employees_ListChanged(object sender, ListChangedEventArgs e) {
//            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted) {
//                UpdateEmployeeIds();
//                OnChanged("ResourceId");
//            }
//        }
//        protected override void OnLoaded() {
//            base.OnLoaded();
//            if (Employees.IsLoaded && !Session.IsNewObject(this))
//                Employees.Reload();
//        }
//        [NonPersistent]
//        [Browsable(false)]
//        [RuleFromBoolProperty("EventIntervalValid", DefaultContexts.Save, "The start date must be less than the end date", SkipNullOrEmptyValues = false, UsedProperties = "StartOn, EndOn")]
//        public bool IsIntervalValid { get { return StartOn <= EndOn; } }
//        #region IEvent Members
//        public bool AllDay {
//            get { return _AllDay; }
//            set { SetPropertyValue("AllDay", ref _AllDay, value); }
//        }
//        [Browsable(false), NonPersistent]
//        public object AppointmentId {
//            get { return Oid; }
//        }
//        [Size(SizeAttribute.Unlimited)]
//        public string Description {
//            get { return _Description; }
//            set { SetPropertyValue("Description", ref _Description, value); }
//        }
//        public int Label {
//            get { return _Label; }
//            set { SetPropertyValue("Label", ref _Label, value); }
//        }
//        public string Location {
//            get { return _Location; }
//            set { SetPropertyValue("Location", ref _Location, value); }
//        }
//        [PersistentAlias("_EmployeeIds"), Browsable(false)]
//        public string ResourceId {
//            get {
//                if (_EmployeeIds == null)
//                    UpdateEmployeeIds();
//                return _EmployeeIds;
//            }
//            set {
//                if (_EmployeeIds != value && value != null) {
//                    _EmployeeIds = value;
//                    UpdateEmployees();
//                }
//            }
//        }

//        #endregion

//        #region IRecurrentEvent Members
//        [DevExpress.Xpo.DisplayName("Recurrence"), Size(SizeAttribute.Unlimited)]
//        public string RecurrenceInfoXml {
//            get { return _RecurrenceInfoXml; }
//            set { SetPropertyValue("RecurrenceInfoXml", ref _RecurrenceInfoXml, value); }
//        }
//        [Browsable(false)]
//        [PersistentAlias("_RecurrencePattern")]
//        public IRecurrentEvent RecurrencePattern {
//            get { return _RecurrencePattern; }
//            set { SetPropertyValue("RecurrencePattern", ref _RecurrencePattern, value as Activity); }
//        }
//        #endregion
//    }
//}

//namespace CAMS.Module.DBTask
//{


//    public class OdL : XPObject
//    {
//        public OdL()
//            : base()
//        {
//        }
//        public OdL(Session session)
//            : base(session)
//        {
//        }
//        public override void AfterConstruction()
//        {
//            base.AfterConstruction();
//            DataEmissione = DateTime.Now;
//        }

//        /// <summary>
//        /// Crea RegistroRdL da una serie di lstRDLSelezionati
//        /// </summary>
//        /// <param name="xpObjectSpace"></param>
//        /// <param name="lstRDLSelezionati"></param>
//        /// <returns></returns>
//        public static OdL CreateFrom(IObjectSpace xpObjectSpace, IEnumerable<RdL> lstRDLSelezionati)
//        {
//            var newOdL = xpObjectSpace.CreateObject<OdL>();
//            var Session = newOdL.Session;
//            var IDs = lstRDLSelezionati.Select(r => r.Oid).ToList();
//            var lstRDL = Session.Query<RdL>().Where(r => IDs.Contains(r.Oid));
//            //newOdL.oRdLs.AddRange(lstRDL);
//            newOdL.RegistroRdL = ((RdL)lstRDL.First<RdL>()).RegistroRdL;
//            newOdL.DataEmissione = DateTime.Now;
//            newOdL.QuantitaRdlAperte = lstRDL.Count();
//            var newStatoOdl = Session.GetObjectByKey<StatoOdl>(1);
//            newOdL.StatoOdl = newStatoOdl;
//            var newTipoOdl = Session.GetObjectByKey<TipoOdL>(5);
//            newOdL.TipoOdL = newTipoOdl;
//            var  nrTeam = ((RdL)lstRDL.First<RdL>()).RisorseTeam.AssRisorseTeam.Count;
//            newOdL.TotaleRisorse = nrTeam;
//            return newOdL;
//        }


//        /// <summary> 
//        /// </summary>
//        private string fDescrizione;
//        [Size(250),
//        Persistent("DESCRIPTION"),
//        DisplayName("Descrizione")]
//        [DbType("varchar(250)")]
//        public string Descrizione
//        {
//            get
//            {
//                return fDescrizione;
//            }
//            set
//            {
//                SetPropertyValue<string>("Descrizione", ref fDescrizione, value);
//            }
//        }

//        private decimal fQuantitaRdlAperte;
//        [Persistent("QTYOPENRDL"),
//        DisplayName("Quantità RdL Assegnate")]
//        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:N}")]
//        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
//        public decimal QuantitaRdlAperte
//        {
//            get
//            {
//                return fQuantitaRdlAperte;
//            }
//            set
//            {
//                SetPropertyValue<decimal>("QuantitaRdlAperte", ref fQuantitaRdlAperte, value);
//            }
//        }

//        private StatoOdl fStatoOdl;
//        [Association(@"ODLRefSTATOODL", typeof(OdL)),
//        Persistent("STATOODL"),
//        DisplayName("Stato OdL")]
//        public StatoOdl StatoOdl
//        {
//            get
//            {
//                return fStatoOdl;
//            }
//            set
//            {
//                SetPropertyValue<StatoOdl>("StatoOdl", ref fStatoOdl, value);
//            }
//        }

//        //Association(@"OdLRefRdL", typeof(RdL)),
//        //[DisplayName("Richieste di Intervento")]
//        //public XPCollection<RdL> oRdLs
//        //{
//        //    get
//        //    {
//        //        return GetCollection<RdL>("oRdLs");
//        //    }
//        //}

//        private TipoOdL fTipoOdL;
//        [Association(@"ODLRefTIPOODL", typeof(OdL)),
//        Persistent("TIPOODL"),
//        DisplayName("Tipo OdL")]
//        public TipoOdL TipoOdL
//        {
//            get
//            {
//                return fTipoOdL;
//            }
//            set
//            {
//                SetPropertyValue<TipoOdL>("TipoOdL", ref fTipoOdL, value);
//            }
//        }
//        //[NonPersistent]
//        //[System.ComponentModel.Browsable(false)]
//        //public string TipoOdLDescrizione
//        //{
//        //    get
//        //    {
//        //        var Descrizione = String.Empty;
//        //        if (TipoOdL != null)
//        //        {
//        //            Descrizione = TipoOdL.Descrizione;
//        //        }
//        //        return Descrizione;
//        //    }
//        //}

//        private StatoOperativo fUltimoStatoOperativo;
//        [Persistent("STATOOPERATIVO"),
//        DisplayName("Ultimo Stato Operativo")]
//        public StatoOperativo UltimoStatoOperativo
//        {
//            get
//            {
//                return fUltimoStatoOperativo;
//            }
//            set
//            {
//                SetPropertyValue<StatoOperativo>("UltimoStatoOperativo", ref fUltimoStatoOperativo, value);
//            }
//        }

//        private DateTime fDataEmissione;
//        [Persistent("DATAEMISSIONE"),
//        DisplayName("Data Emissione"),
//        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
//        public DateTime DataEmissione
//        {
//            get
//            {
//                return fDataEmissione;
//            }
//            set
//            {
//                SetPropertyValue<DateTime>("DataEmissione", ref fDataEmissione, value);
//            }
//        }


//        private DateTime fDataCompletamento;
//        [Persistent("DATE_COMPLETED"),
//        DisplayName("Data Completamento"),
//        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
//        public DateTime DataCompletamento
//        {
//            get
//            {
//                return fDataCompletamento;
//            }
//            set
//            {
//                SetPropertyValue<DateTime>("DataCompletamento", ref fDataCompletamento, value);
//            }
//        }

//        private decimal fTotaleMinImpegnate;
//        [Persistent("TOTMINIMPEGNATI"),
//        DisplayName("Totale Minuti Impegnati")]
//        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:N}")]
//        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
//        public decimal TotaleMinImpegnate
//        {
//            get
//            {
//                return fTotaleMinImpegnate;
//            }
//            set
//            {
//                SetPropertyValue<decimal>("TotaleMinImpegnate", ref fTotaleMinImpegnate, value);
//            }
//        }

//        private int fTotaleRisorse;
//        [Persistent("TOTRISORSE"),
//        DisplayName("Totale Risorse")]
//        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:N}")]
//        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
//        public int TotaleRisorse
//        {
//            get
//            {
//                return fTotaleRisorse;
//            }
//            set
//            {
//                SetPropertyValue<int>("TotaleRisorse", ref fTotaleRisorse, value);
//            }
//        }


//        private RegistroRdL fRegistroRdL;
//        [Association(@"REGISTRORDLRefOdl"),
//        Persistent("REGRDL"),
//        DisplayName("Registro RdL")]
//        [VisibleInDetailView(false)]
//        [VisibleInListView(false)]
//        [VisibleInLookupListView(false)]
//        public RegistroRdL RegistroRdL
//        {
//            get
//            {
//                return fRegistroRdL;
//            }
//            set
//            {
//                SetPropertyValue<RegistroRdL>("RegistroRdL", ref fRegistroRdL, value);
//            }
//        }



//        //private Impianto fImpianto;
//        //[NonPersistent,
//        //DisplayName("Impianto")]
//        //public Impianto Impianto
//        //{
//        //    get
//        //    {
//        //        if (oRdLs.Count > 0)
//        //        {
//        //            var _Impianto = oRdLs[0].Impianto;
//        //            if (_Impianto != null)
//        //            {
//        //                return (Impianto)_Impianto;
//        //            }
//        //            else
//        //            {
//        //                return null;
//        //            }
//        //        }
//        //        return fImpianto;
//        //    }
//        //}


//        protected override void OnSaving()
//        {
//            base.OnSaving();
//            //if (!IsDeleted)
//            //{
//            //    var CodImpianto = string.Empty;
//            //    if (oRdLs.Count > 0)
//            //    {
//            //        CodImpianto = oRdLs[0].Impianto.Descrizione;
//            //    }
//            //    var NuovoNumRegRdl = 0;
//            //    if (RegistroRdL != null)
//            //    {
//            //        NuovoNumRegRdl = RegistroRdL.Oid;
//            //    }
//            //    var NuovoNumOdl = Oid;
//            //    if (Session.IsNewObject(this))
//            //    {
//            //        NuovoNumOdl = Convert.ToInt32(Session.Evaluate<OdL>(CriteriaOperator.Parse("Max(Oid)"), null)) + 1;
//            //    }
//            //    var DescrizioneRaggruppamentoRdl = string.Format("OdL TT({0}-{1}) Impianto {2}", NuovoNumRegRdl, NuovoNumOdl, CodImpianto);
//            //    Descrizione = DescrizioneRaggruppamentoRdl;
//            //}
//        }
//    }
//}
