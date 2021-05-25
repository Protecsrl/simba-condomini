using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System;
using System.ComponentModel;

namespace CAMS.Module.DBAudit.DC
{
    [DomainComponent]
    [DefaultClassOptions]
    [ImageName("ShowTestReport")]
    //[DefaultProperty("RdL")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [NavigationItem(false)]
    [ModelDefault("AllowDelete", "False")] //AllowDelete
    [ModelDefault("AllowWrite", "False")] //AllowDelete
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Operativo Risorsa")]
    //    [Appearance("DCRdL.Conduttore.evidenzia.FontColorRed", AppearanceItemType = "ViewItem", TargetItems = "*",
    //   Context = "DCRdL_LookupListView", Criteria = "idColore = 2", BackColor = "Yellow", FontColor = "Black")]

    //    [Appearance("DCRdL.Ordinamento.evidenzia.FontColorRed", AppearanceItemType = "ViewItem", TargetItems = "*",
    //Context = "DCRdL_LookupListView", Criteria = "idColore = 1", BackColor = "PaleGreen", FontColor = "Black")]
    public class DCRegistroOperativoRisorsa
    {
        private IObjectSpace objectSpace;

        [Browsable(false)]  // Hide the entity identifier from UI.
        [DevExpress.ExpressApp.Data.Key]
        public int ID { get; set; }

    
        [XafDisplayName("Codice RegRdL")]
        public string RegrdLCodice
        { get; set; }
        //{
        //    get { return RdLCodice; }
        //    set { RdLCodice = value; }
        //}

        [XafDisplayName("Descrizione")]
        public string Descrizione { get; set; }

        [XafDisplayName("DataOra")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.DateAndTimeOfDayEditMask)]


        public DateTime DataOra { get; set; }

        [XafDisplayName("Utente")]
        public string Utente { get; set; }

        [XafDisplayName("Nuovo Valore")]
        public string newValue { get; set; }

        [XafDisplayName("Valore Precedente")]
        public string oldValue { get; set; }

        [XafDisplayName("Campo Modificato")]
        public string PropertyName { get; set; }

        [XafDisplayName("Azione Eseguita")]
        public string AzionePropertyName { get; set; }
       
        [XafDisplayName("Oid Risorsa")]
        public int OidRisorsa { get; set; }
       
   

    }
}
