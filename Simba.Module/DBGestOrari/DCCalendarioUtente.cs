﻿using CAMS.Module.Classi;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CAMS.Module.DBGestOrari
{
    [DomainComponent]
    [DefaultClassOptions]
    //[ImageName("BO_Unknown")]
    //[DefaultProperty("SampleProperty")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class DCCalendarioUtente : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public DCCalendarioUtente()
        {
            Oid = Guid.NewGuid();
        }

        [DevExpress.ExpressApp.Data.Key]
        [Browsable(false)]  // Hide the entity identifier from UI.
        public Guid Oid { get; set; }

        private string fNomeGiornoSettimana;
        [XafDisplayName("Giorno"), ToolTip("Giorno della settimana")]
        //[ModelDefault("EditMask", "(000)-00")]
        [ VisibleInListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string NomeGiornoSettimana
        {
            get { return fNomeGiornoSettimana; }
            set
            {
                if (fNomeGiornoSettimana != value)
                {
                    fNomeGiornoSettimana = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime fData;
        [XafDisplayName("fData"), ToolTip("fData ")]
        //[ModelDefault("EditMask", "(000)-00")]
        [VisibleInListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime Data
        {
            get { return fData; }
            set
            {
                if (fData != value)
                {
                    fData = value;
                    OnPropertyChanged();
                }
            }
        }

        private TipoSetOrario fTipoSetOrario;
        [XafDisplayName("fData"), ToolTip("fData ")]
        //[ModelDefault("EditMask", "(000)-00")]
        [VisibleInListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public TipoSetOrario TipoSetOrario
        {
            get { return fTipoSetOrario; }
            set
            {
                if (fTipoSetOrario != value)
                {
                    fTipoSetOrario = value;
                    OnPropertyChanged();
                }
            }
        }
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
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}