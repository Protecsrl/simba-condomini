using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.DBALibrary;
using System.Collections.Generic;
using System.Linq;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions,
    Persistent("IMPIANTOLIBRARY")]
    [VisibleInDashboards(false)]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Libreria Impianti")]
    [ImageName("Categories")]
    [NavigationItem("Configurazione Edifici")]

    public class ServizioLibrary : XPObject
    {
        private const string NA = "N/A";
      

        public ServizioLibrary()
            : base()
        {
        }
        public ServizioLibrary(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        protected override void OnSaving()
        {
            if (!IsDeleted && this.Sistema != null && this.Sistema.CodUni != null && CodDescrizione == NA)
            {
                      string FormattazioneCodice = this.Sistema.CodUni.ToString() + "_{0:0000}";
                      var num = this.Session.Query < ServizioLibrary>().Select(w=>w.Oid).Max();
              // Session.ob.GetObjects(ImpiantoLibrary,"Sistema == ".mthis.Sistema,null);
              //   Convert.ToInt32(Session.Evaluate<ImpiantoLibrary>(CriteriaOperator.Parse("Count"), null)) + 1);
                CodDescrizione = String.Format(FormattazioneCodice,num.ToString());
                   // Convert.ToInt32(Session.Evaluate<ImpiantoLibrary>(CriteriaOperator.Parse("Count"), null)) + 1);
            }
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"),
        Size(100),
        DisplayName("Descrizione")]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.ImpLibrary.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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
        Size(15),
        DisplayName("Cod Descrizione"),
        Appearance("ImpLib.CodDescrizione", Enabled = false)]
        [DbType("varchar(15)")]
        public string CodDescrizione
        {
            get
            {
                if (!IsLoading && !IsSaving && fCodDescrizione == null)
                {
                    fCodDescrizione = NA;
                }
                return fCodDescrizione;
            }
            set
            {
                SetPropertyValue<string>("CodDescrizione", ref fCodDescrizione, value);
            }
        }

        private SistemaTecnologico fSistemaTecnologico;
        [Persistent("SISTEMATECNOLOGICO"), DisplayName("Sistema Tecnologico")]
        [RuleRequiredField("RReqField.ImpLib.SistemaTecnologico", DefaultContexts.Save, "Sistema Tecnologico è un campo obbligatorio")]
        [Appearance("ImpLib.Abilita.SistemaTecnologico", Criteria = "not (Sistema  is null)", Enabled = false, Context = "DetailView")]
        [ImmediatePostData(true)]
        public SistemaTecnologico SistemaTecnologico
        {
            get
            {
                return fSistemaTecnologico;
            }
            set
            {
                SetPropertyValue<SistemaTecnologico>("SistemaTecnologico", ref fSistemaTecnologico, value);
            }
        }

        private Sistema fSistema;
        [Persistent("SISTEMA"),  DisplayName("Unità Tecnologica")]
        [RuleRequiredField("RReqField.ImpLib.Sistema", DefaultContexts.Save, @"Unità Tecnologica è un campo obbligatorio")]
        [Appearance("ImpLib.Sistema", Enabled = false, Criteria = "(SistemaTecnologico  is null) OR QuantitaApparati > 0", Context = "DetailView")]
        [ImmediatePostData(true)]
        [DataSourceCriteria("SistemaClassi.SistemaTecnologico = '@This.SistemaTecnologico'")]
        public Sistema Sistema
        {
            get
            {                
                return fSistema;
            }
            set
            {
                SetPropertyValue<Sistema>("Sistema", ref fSistema, value);
            }
        }

        [NonPersistent,
        DisplayName("Quantità Apparati")]
        public int QuantitaApparati
        {
            get
            {
                var somma = 0;
                foreach (var ele in SERVIZIOLIBRARYDETTAGLIOs)
                {
                    somma += ele.Quantita;
                }

                return somma;
            }
        }

        [Association(@"IMPIANTOLIBRERYRefDETTAGLIO", typeof(ServizioLibraryDettaglio)),
        DisplayName("Apparati Inseriti")]
        public XPCollection<ServizioLibraryDettaglio> SERVIZIOLIBRARYDETTAGLIOs
        {
            get
            {
                return GetCollection<ServizioLibraryDettaglio>("IMPIANTOLIBRARYDETTAGLIOs");
            }
        }

        [Association(@"IMPIANTOLIBRERY_AppInseribili", typeof(ServizioLibraryAppInseribili)),
        DisplayName("Apparati Inseribili in Lib")]
        [VisibleInDetailView(false),VisibleInListView(false),VisibleInLookupListView(false)]
          
        public XPCollection<ServizioLibraryAppInseribili> ImpiantoLibraryAppInseribilis
        {
            get
            {
                return GetCollection<ServizioLibraryAppInseribili>("ImpiantoLibraryAppInseribilis");
            }
        }


        private string f_Utente;
        [Persistent("UTENTE"), Size(100), DisplayName("Utente")]
       // [Appearance("ImpLib.Utente", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        [DbType("varchar(100)")]
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

        private DateTime? fDataAggiornamento;
        [Persistent("DATAUPDATE"), DisplayName("Data Aggiornamento"), Appearance("ImpLib.DataAggiornamento", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        public DateTime? DataAggiornamento
        {
            get
            {
                return fDataAggiornamento;
            }
            set
            {
                SetPropertyValue<DateTime?>("DataAggiornamento", ref fDataAggiornamento, value);
            }
        }
    }
}
