using System;
using DevExpress.Xpo;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.Xpo' è già presente in questo spazio dei nomi
using DevExpress.Xpo;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.Xpo' è già presente in questo spazio dei nomi
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Validation;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions]
    [VisibleInDashboards(false)]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    [Persistent("STATOCONNESSIONE")]
    public class StatoConnessione : XPObject
    {
        public StatoConnessione()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public StatoConnessione(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        private string fDescrizione;
        [XafDisplayName("Descrizione"), ToolTip("Stato Disponibilità")]
        //[ModelDefault("EditMask", "(000)-00")] 
        [Index(0), VisibleInListView(false)]
        [Persistent("DESCRIZIONE"), RuleRequiredField(DefaultContexts.Save)]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue("Descrizione", ref fDescrizione, value); }
        }

    }

}