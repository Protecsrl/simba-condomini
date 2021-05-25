using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.DBALibrary;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Drawing;
using DevExpress.Xpo.Metadata;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions,
    Persistent("SERVIZIOLIBRARYDETTAGLIO")]
    [VisibleInDashboards(false)]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettaglio Libreria Impianti")]
    [ImageName("DettaglioLibreriaImpianti")]
    [NavigationItem(false)]

    public class ServizioLibraryDettaglio : XPObject
    {
        public ServizioLibraryDettaglio()            : base()
        {
        }
        public ServizioLibraryDettaglio(Session session)            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if(this.Oid<0)
            {
                if (this.ImpiantoLibrary != null)
                {
                    Sistema = ImpiantoLibrary.Sistema;
                }
            }

        }

        protected override void OnSaving()
        {
        }

        private Sistema fSistema;
        [Persistent("SISTEMA"), DisplayName("Unità Tecnologica")]
        [Appearance("ImpiantoLibraryDettaglio.Sistema", Enabled = false, Criteria = "!IsNullOrEmpty(Sistema)", Context = "DetailView")]
        [RuleRequiredField("ImpiantoLibraryDettaglio.Sistema", DefaultContexts.Save, "Sistema è un campo obbligatorio")]
        public Sistema Sistema
        {
            get
            {
                if (this.ImpiantoLibrary != null)
                {
                    fSistema = ImpiantoLibrary.Sistema;
                }
                return fSistema;
            }
            set
            {
                SetPropertyValue<Sistema>("Sistema", ref fSistema, value);
            }
        }

        private StdAsset fStdApparato;
        [Persistent("APPARATOSTD"),
        DisplayName("Tipo Apparato")]
        [RuleRequiredField("RReqField.StdApp.Sist", DefaultContexts.Save, "Tipo Apparato è un campo obbligatorio")]
        [Appearance("ImpiantoLibraryDettaglio.StdApparato", Criteria = "KDimensione Is Not Null", Enabled = false)]
        [ImmediatePostData(true)]
        [DataSourceCriteria("")]

        public StdAsset StdApparato
        {
            get
            {
                
                return fStdApparato;
            }
            set
            {
                SetPropertyValue<StdAsset>("StdApparato", ref fStdApparato, value);
            }
        }

        [NonPersistent]
        [DevExpress.Xpo.Size(SizeAttribute.Unlimited)]
        //[ValueConverter(typeof(ImageValueConverter))]
        [VisibleInListViewAttribute(true)]
        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ListViewImageEditorCustomHeight = 40)]
        [System.ComponentModel.Browsable(false)]
        public byte[] ImageIcona //Image ImageIcona
        {
            get
            {
                return StdApparato.ImageIcona;
            }
        }

        private int fQuantita;
        [Persistent("QUANTITA"),        DisplayName("Q.tà"),        Size(10)]      
        public int Quantita
        {
            get
            {
                return fQuantita;
            }
            set
            {
                SetPropertyValue<int>("Quantita", ref fQuantita, value);
            }
        }

        private KDimensione fKDimensione ;                               
        [Persistent("KDIMENSIONE"),    DisplayName("KDimensione"),   Size(70)]
        [DataSourceCriteria("StandardApparato.Oid = '@This.StdApparato.Oid'")]
        [Appearance("ImpiantoLibraryDettaglio.KDimensione", Criteria = "StdApparato Is Null Or StdApparato.KDimensiones.Count() = 0", Enabled = false)]
        [ImmediatePostData(true)]
        public KDimensione KDimensione
        {
            get
            {
                
                return fKDimensione;
            }
            set
            {
                SetPropertyValue<KDimensione>("KDimensione", ref fKDimensione, value);
            }
        }

        [NonPersistent]
        [System.ComponentModel.Browsable(false)]
        public int OidImpiantoLibrary { get; set; }

        private ServizioLibrary fImpiantoLibrary;
        [Association(@"IMPIANTOLIBRERYRefDETTAGLIO"),
        DisplayName("Impianto Library")]
        [Persistent("IMPIANTOLIBRARY")]
        [MemberDesignTimeVisibility(false)]
        public ServizioLibrary ImpiantoLibrary
        {
            get
            {
                return fImpiantoLibrary;
            }
            set
            {
                SetPropertyValue<ServizioLibrary>("ImpiantoLibrary", ref fImpiantoLibrary, value);
            }
        }

        private string f_Utente;
        [Persistent("UTENTE"),
        Size(100),
        DisplayName("Utente")]
      //  Appearance("ImpLibDett.Utente", Enabled = false)]
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
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento"),
         System.ComponentModel.Browsable(false)]
        //Appearance("ImpLibDett.DataAggiornamento", Enabled = false)]
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















