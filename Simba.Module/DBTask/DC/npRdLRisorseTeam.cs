using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Xpo;
using CAMS.Module.Classi;

namespace CAMS.Module.DBTask.DC
{
    [DomainComponent]
    [DefaultClassOptions]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Risorse Team x RdL")]
    [Appearance("npRdLRisorseTeam.Conduttore.evidenzia.FontColorRed", AppearanceItemType = "ViewItem", TargetItems = "*",
          Context = "DCRisorseTeamRdL_LookupListView", Criteria = "Conduttore", BackColor = "Yellow", FontColor = "Black")]

    [Appearance("npRdLRisorseTeam.Ordinamento.evidenzia.FontColorRed", AppearanceItemType = "ViewItem", TargetItems = "*",
          Context = "DCRisorseTeamRdL_LookupListView", Criteria = "Ordinamento = 1", BackColor = "PaleGreen", FontColor = "Black")]
    [NavigationItem(false)]
    //[ImageName("BO_Unknown")]
    //[DefaultProperty("SampleProperty")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class npRdLRisorseTeam : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public npRdLRisorseTeam()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        [Browsable(false)]  // Hide the entity identifier from UI.
        [DevExpress.ExpressApp.Data.Key]
        public int ID { get; set; }
        //public Guid Oid { get; set; }

        [XafDisplayName("Risorsa Capo Squadra")]
        [Index(1)]
        public string RisorsaCapo { get; set; }

        [XafDisplayName("Telefono")]  //Azienda
        [Index(2)]
        public string Telefono { get; set; }

        [XafDisplayName("Azienda")]  //Azienda
        [Index(16)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string Azienda { get; set; }

        [XafDisplayName("Coppia Linkata")]
        [Index(4)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public TipoNumeroManutentori CoppiaLinkata { get; set; }

        [XafDisplayName("Mansione")]
        [Index(3)]
        public string Mansione { get; set; }

        [XafDisplayName("Centro Operativo")]
        [Index(5)]
        public string CentroOperativo { get; set; }

        [XafDisplayName("Ultimo stato operativo")]
        [Index(6)]
        public string UltimoStatoOperativo { get; set; }

        [XafDisplayName(@"Attività in Agenda")]
        [Index(7)]
        public int NumeroAttivitaAgenda { get; set; }

        [XafDisplayName(@"Attività Sospese")]
        [Index(8)]
        public int NumeroAttivitaSospese { get; set; }

        [XafDisplayName(@"Attività in Emergenza")]
        [Index(9)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public int NumeroAttivitaEmergenza { get; set; }

        [XafDisplayName("Posizione")]
        [EditorAlias("HyperLinkPropertyEditor")]
        [Index(15)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string Url { get; set; }

        [XafDisplayName("Reg.RdL in Lavorazione")]
        [Index(10)]
        public string RegistroRdL { get; set; }

        [XafDisplayName("Conduttore")]
        [Index(11)]
        public Boolean Conduttore { get; set; }

        #region calcolo distanze
        [XafDisplayName("Distanza Impianto"), Size(50)]
        [Index(12)]
        public string DistanzaImpianto { get; set; }

        [XafDisplayName("Ultimo Edificio Visitato"), Size(50)]
        [Index(13)]
        public string UltimoEdificio { get; set; }

        [XafDisplayName("Interventi su Edificio"), Size(50)]
        [Index(14)]
        public string InterventosuEdificio { get; set; }

        #endregion

        #region indici classi e user
        [XafDisplayName("OID Centro Operativo")]
        [Browsable(false)]
        public int OidCentroOperativo { get; set; }

        [XafDisplayName("OID Edificio")]
        [Browsable(false)]
        public int OidEdificio { get; set; }

        [XafDisplayName("OID OidRisorsaTeam")]
        [Browsable(false)]
        public int OidRisorsaTeam { get; set; }

        [XafDisplayName("Ordinamento")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int Ordinamento { get; set; }

        [XafDisplayName("User")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public string UserName { get; set; }
        #endregion
   

        //private string sampleProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public string SampleProperty
        //{
        //    get { return sampleProperty; }
        //    set
        //    {
        //        if (sampleProperty != value)
        //        {
        //            sampleProperty = value;
        //            OnPropertyChanged("SampleProperty");
        //        }
        //    }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.SampleProperty = "Paid";
        //}

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
        }
        void IXafEntityObject.OnLoaded()
        {
            // Place the code that is executed each time the entity is loaded here.
        }
        void IXafEntityObject.OnSaving()
        {
            // Place the code that is executed each time the entity is saved here.
        }
        #endregion

        #region IObjectSpaceLink members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIObjectSpaceLinktopic.aspx)
        // Use the Object Space to access other entities from IXafEntityObject methods (see https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113707.aspx).
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }

        // IObjectSpaceLink
        [Browsable(false)]
        public IObjectSpace ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }

        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}