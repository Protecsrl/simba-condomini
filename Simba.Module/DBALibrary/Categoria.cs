using System;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.DC;


namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions,  Persistent("CATEGORIA"),   System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Categorie")]
    //[Appearance("SuperPosRuleDisaCategoria", TargetItems = "Descrizione;CodDescrizione;Utente;DataAggiornamento", Criteria = "RuleDisableAction == 'True'", Enabled = false)]
    //[Appearance("SuperPosRuleVisyCategoria", TargetItems = "RuleDisableAction", Criteria = "1 == 1", Visibility = ViewItemVisibility.Hide)]
    [NavigationItem("Procedure Attivita")]
    [ImageName("Categoria")]
    public class Categoria : XPObject
    {
        public Categoria()
            : base()
        {
        }
        public Categoria(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"),
        Size(50)]
        [DbType("varchar(50)")]
        [RuleRequiredField("RReqField.Categoria.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private string fCodDescrizione;
        [Persistent("COD_DESCRIZIONE"),
        Size(5)]
        [DbType("varchar(5)")]
        [RuleRequiredField("RReqField.Categoria.CodDescrizione", DefaultContexts.Save, "Il Codice Descrizione è un campo obbligatorio")]
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


        private string f_Utente;
        [Persistent("UTENTE"),
        Size(100),
        DisplayName("Utente")]
        [DbType("varchar(100)")]
        [System.ComponentModel.Browsable(false)]
        public string Utente
        {
            get
            {
                return f_Utente;
            }
            set
            {
                SetPropertyValue<string>("Utente", ref f_Utente, value);
            }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]        
        [System.ComponentModel.Browsable(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataAggiornamento
        {
            get
            {
                return fDataAggiornamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value);
            }
        }

        private int fOrdinamento;
        [Persistent("ORDINAMENTO")    ,XafDisplayName ("Ordinamento")   ]
        [VisibleInListView(false),VisibleInDetailView(true)]
        public int Ordinamento
        {
            get
            {
                return fOrdinamento;
            }
            set
            {
                SetPropertyValue<int>("Ordinamento", ref fOrdinamento, value);
            }
        }
        public override string ToString()
        {
            return string.Format("{0}", Descrizione); ;
        }

    }
}
