using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using CAMS.Module.Classi;
using DevExpress.Persistent.Validation;
using CAMS.Module.DBSpazi;

namespace CAMS.Module.DBDocument
{
    [DefaultClassOptions, Persistent("DOCUMENTI")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [ImageName("Demo_String_InSpecialFormat_Properties")]
    [NavigationItem("Navigazione Documenti")]

    public class Documenti : XPObject
    {
        public Documenti()
            : base()
        {
        }
        public Documenti(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Size(200), Persistent("DESCRIZIONE")]
        [DbType("varchar(200)")]
        [RuleRequiredField("Documenti.Descrizione", DefaultContexts.Save, "la Descrizione è un campo obbligatorio")]
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

        [Persistent("FILE"), DisplayName("File")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [RuleRequiredField("Documenti.File", DefaultContexts.Save, "il File del documento è un campo obbligatorio")]
        [ExplicitLoading()]
        [Delayed(true)]
        public FileData File
        {
            get
            {
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("File");
                // return fFile;
            }
            set
            {
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("File", value);
                //   SetPropertyValue("File", ref fFile, value);
            }
        }

        [Association(@"Documenti_Servizio"), Persistent("SERVIZIO"), DisplayName("Servizio")]
        //[Appearance("Documenti.Impianto", Visibility = ViewItemVisibility.Hide)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Servizio Servizio
        {
            get
            {
                return GetDelayedPropertyValue<Servizio>("Servizio");
                // return fFile;
            }
            set
            {
                SetDelayedPropertyValue<Servizio>("Servizio", value);
                //   SetPropertyValue("File", ref fFile, value);
            }
            //get
            //{
            //    return fServizio;
            //}
            //set
            //{
            //    SetPropertyValue<Impianto>("Impianto", ref fServizio, value);
            //}
        }

        private Asset fApparato;
        [Association(@"Documenti_Asset"), Persistent("ASSET"), DisplayName("Asset")]
        //[Appearance("Documenti.Apparato", Visibility = ViewItemVisibility.Hide)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Asset Asset
        {

             get
            {
                return GetDelayedPropertyValue<Asset>("Asset");
                // return fFile;
            }
            set
            {
                SetDelayedPropertyValue<Asset>("Asset", value);
                //   SetPropertyValue("File", ref fFile, value);
            }

            //get
            //{
            //    return fApparato;
            //}
            //set
            //{
            //    SetPropertyValue<Apparato>("Apparato", ref fApparato, value);
            //}
        }



        private Contratti fCommesse;
        [Association(@"Documenti_Commessa"), Persistent("CONTRATTO"), DisplayName("Commessa")]
        //[Appearance("Documenti.Immobile", Visibility = ViewItemVisibility.Hide)]Aggregated,
        [ExplicitLoading()]
        public Contratti Commesse
        {
            get
            {
                return fCommesse;
            }
            set
            {
                SetPropertyValue<Contratti>("Commesse", ref fCommesse, value);
            }
        }



        private Immobile fImmobile;
        [Association(@"Documenti_Edificio"), Persistent("IMMOBILE"), DisplayName("Immobile")]
        //[Appearance("Documenti.Immobile", Visibility = ViewItemVisibility.Hide)]Aggregated,
        [ExplicitLoading()]
        public Immobile Immobile
        {
            get
            {
                return fImmobile;
            }
            set
            {
                SetPropertyValue<Immobile>("Immobile", ref fImmobile, value);
            }
        }
                                
        private RdL fRdL;
        [Association(@"Documenti_RdL"), Persistent("RDL"), DisplayName("RdL")]
       // [Appearance("Documenti.RdL", Visibility = ViewItemVisibility.Hide)]
        [ExplicitLoading()]
        public RdL RdL
        {
            get
            {
                return fRdL;
            }
            set
            {
                SetPropertyValue<RdL>("RdL", ref fRdL, value);
            }
        }

        private RegistroRdL fRegistroRdL;
        [Association(@"Documenti_RegRdL"), Persistent("REGRDL"), DisplayName("Reg.RdL")]
        //[Appearance("Documenti.RegRdL", Visibility = ViewItemVisibility.Hide)]
        [ExplicitLoading()]
        [Delayed(true)]
        public RegistroRdL RegistroRdL
        {
             get
            {
                return GetDelayedPropertyValue<RegistroRdL>("RegistroRdL");
                // return fFile;
            }
            set
            {
                SetDelayedPropertyValue<RegistroRdL>("RegistroRdL", value);
                //   SetPropertyValue("File", ref fFile, value);
            }             
        }

        [Association(@"Documenti_Piani"), Persistent("PIANO"), DisplayName("Piano")]
        //[Appearance("Documenti.Piani", Visibility = ViewItemVisibility.Hide)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Piani Piano
        {
            get
            {
                return GetDelayedPropertyValue<Piani>("Piano");
            }
            set
            {
                SetDelayedPropertyValue<Piani>("Piano", value);                 
            }
        }

        [Association(@"Documenti_Locale"), Persistent("LOCALE"), DisplayName("Locale")]
        //[Appearance("Documenti.Locale", Visibility = ViewItemVisibility.Hide)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Locali Locale
        {
            get
            {
                return GetDelayedPropertyValue<Locali>("Locale");
            }
            set
            {
                SetDelayedPropertyValue<Locali>("Locale", value);
            }
        }


        private TipoDocumento fTipofile;
        [Persistent("TIPOFILE")]
       // [Appearance("RegMisure.Abilita.Impianto", Criteria = "(not (Immobile is null)) or (not (Impianto is null)) or (not (RdL is null)) or (not (Apparato is null))", Enabled = false)]
        public TipoDocumento Tipofile
        {
            get
            {
                return fTipofile;
            }
            set
            {
                SetPropertyValue<TipoDocumento>("Tipofile", ref fTipofile, value);
            }
        }
    }
}
