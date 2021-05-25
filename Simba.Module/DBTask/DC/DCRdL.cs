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
using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace CAMS.Module.DBTask.DC
{
    [DomainComponent]
    [DefaultClassOptions]
    [ImageName("ShowTestReport")]
    [DefaultProperty("RdL")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [NavigationItem(false)]
    [ModelDefault("AllowDelete", "False")] //AllowDelete
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "RdL x Risorse Team")]
    [Appearance("DCRdL.Conduttore.evidenzia.FontColorRed", AppearanceItemType = "ViewItem", TargetItems = "*",
   Context = "DCRdL_LookupListView", Criteria = "idColore = 2", BackColor = "Yellow", FontColor = "Black")]

    [Appearance("DCRdL.Ordinamento.evidenzia.FontColorRed", AppearanceItemType = "ViewItem", TargetItems = "*",
Context = "DCRdL_LookupListView", Criteria = "idColore = 1", BackColor = "PaleGreen", FontColor = "Black")]
    public class DCRdL //: IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        //private void OnPropertyChanged(String propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
        //public DCRdL()
        //{
        //}
        // Add this property as the key member in the CustomizeTypesInfo event
        [Browsable(false)]  // Hide the entity identifier from UI.
        [DevExpress.ExpressApp.Data.Key]
        public int ID { get; set; }

        //[XafDisplayName("RdL")]
        //public RdL RdL { get; set; }

        [XafDisplayName("Codice RdL")]
        public string RdLCodice { get; set; }

        [XafDisplayName("Descrizione")]
        public string RdLDescrizione { get; set; }

        [XafDisplayName("Sollecito")]
        public string RdLSollecito { get; set; }

        [XafDisplayName("Richiedente")]
        public string RichiedenteDescrizione { get; set; }

        [XafDisplayName("Immobile")]
        public string EdificioDescrizione { get; set; }

        [XafDisplayName("Indirizzo")]
        public string IndirizzoDescrizione { get; set; }

        [XafDisplayName("Impianto")]
        public string ImpiantoDescrizione { get; set; }

        [XafDisplayName("Data Pianificata")]
        public string DataPianificata { get; set; }

        [Browsable(false)]
        public int idColore { get; set; }

    }
}
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

//#region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
//void IXafEntityObject.OnCreated()
//{
//    // Place the entity initialization code here.
//    // You can initialize reference properties using Object Space methods; e.g.:
//    // this.Address = objectSpace.CreateObject<Address>();
//}
//void IXafEntityObject.OnLoaded()
//{
//    // Place the code that is executed each time the entity is loaded here.
//}
//void IXafEntityObject.OnSaving()
//{
//    // Place the code that is executed each time the entity is saved here.
//}
//#endregion

//#region IObjectSpaceLink members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIObjectSpaceLinktopic.aspx)
//// Use the Object Space to access other entities from IXafEntityObject methods (see https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113707.aspx).
//IObjectSpace IObjectSpaceLink.ObjectSpace
//{
//    get { return objectSpace; }
//    set { objectSpace = value; }
//}
//#endregion

//#region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
//public event PropertyChangedEventHandler PropertyChanged;
//#endregion
