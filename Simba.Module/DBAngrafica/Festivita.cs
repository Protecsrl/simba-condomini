using System;
using System.Collections.Generic;
using System.ComponentModel;
using CAMS.Module.DBAux;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBAngrafica
{
    [NavigationItem("Amministrazione"), System.ComponentModel.DisplayName("Festività")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Festività")]
    [DefaultClassOptions, Persistent("FESTIVITA")]
    [RuleCombinationOfPropertiesIsUnique("UniqrFestivitaDescrizione", DefaultContexts.Save, "Giorno,Mese", SkipNullOrEmptyValues = false)]
    [VisibleInDashboards(false)]
    public class Festivita : XPObject
    {
        public Festivita()
            : base()
        {
        }

        public Festivita(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"),
        Size(100), DevExpress.Xpo.DisplayName("Descrizione")]
        [RuleRequiredField("RReqField.Festivita.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        [DbType("VARCHAR(100)")] //[DbType("VARCHAR2(100)")]
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


        private int fMese;
        [Persistent("MESE"), DevExpress.Xpo.DisplayName("Mese")]
        [RuleRequiredField("RReqField.Festivita.Mese", DefaultContexts.Save, "Il Mese è un campo obbligatorio")]
        [Appearance("Festivita.Mese", Enabled = false, Criteria = "Giorno != null")]
        [ImmediatePostData(true)]
        [DataSourceProperty("GiorniInseribili")]
        public int Mese
        {
            get
            {
                return fMese;
            }
            set
            {
                SetPropertyValue<int>("Mese", ref fMese, value);
                this.fgiorni = null;
            }
        }

        private Giorni fGiorno;
        [Persistent("GIORNOMESE"), DevExpress.Xpo.DisplayName("Giorno")]
        [RuleRequiredField("RReqField.Festivita.Giorno", DefaultContexts.Save, "Il Giorno è un campo obbligatorio")]
        [Appearance("Festivita.Giorno", Enabled = false, Criteria = "Mese = 0")]
        [ImmediatePostData(true)]
        [DataSourceProperty("GiorniInseribili")]
        public Giorni Giorno
        {
            get
            {
                return fGiorno;
            }
            set
            {
                SetPropertyValue<Giorni>("Giorno", ref fGiorno, value);
            }
        }

        [NonPersistent]
        private XPCollection<Giorni> fgiorni;
        [Browsable(false)] // Prohibits showing the AvailableAccessories collection separately 
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<Giorni> GiorniInseribili
        {
            get
            {
                if (fgiorni == null)
                {    // Retrieve all Accessory objects 
                    fgiorni = new XPCollection<Giorni>(Session);// Filter the retrieved collection according to current conditions  //  RefreshEdificiInseribili();
                     
                        if (Mese > 0)
                        {
                            int length = DateTime.DaysInMonth(DateTime.Now.Year, Mese);
                            CriteriaOperator charFilter = CriteriaOperator.Parse("NrGiorno <= " + length); //new ("Giorni", length);
                            fgiorni.Criteria = charFilter;
                            fgiorni.Sorting.Add(new SortProperty("strGiorno", DevExpress.Xpo.DB.SortingDirection.Ascending));
                        }
                }
                else
                {
                     
                        if (Mese > 0)
                        {
                            int length = DateTime.DaysInMonth(DateTime.Now.Year, Mese);
                            CriteriaOperator charFilter = CriteriaOperator.Parse("NrGiorno <= " + length); //new ("Giorni", length);
                            fgiorni.Criteria = charFilter;
                            fgiorni.Sorting.Add(new SortProperty("strGiorno", DevExpress.Xpo.DB.SortingDirection.Ascending));
                            
                        }
                    
                }
                return fgiorni;
            }
        }

        private XPCollection<int> fMesi;
        [Browsable(false)] // Prohibits showing the AvailableAccessories collection separately 
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<int> MesiInseribili
        {
            get
            {
                fMesi = new XPCollection<int>(Session);
                int length = 13;
                for (int i = 1; i < length; i++)
                {
                    fMesi.Add(i);
                }
                return fMesi;
            }
        }
    }




}
