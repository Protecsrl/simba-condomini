using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;

using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;

namespace CAMS.Module.DBDocument
{
    [System.ComponentModel.DisplayName("Navigazione Documenti")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Navigazione Documenti")]
    [DefaultClassOptions,    Persistent("V_NAVIGAZIONEDOCUMENTI")]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("tutti", "", "Tutti",  Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Edifici", "Immobile Is Not Null And Impianto Is Null", "Associati a Edifici", true, Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Impianti", "Impianto Is Not Null And Apparato Is Null", "Associati a Impianti", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Apparati", "Apparato Is Not Null", "Associati a Apparati", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdL", "RegistroRdL Is Not Null", "Associati a RegRdL", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdL", "RdL Is Not Null", "Associati a RdL", Index = 5)]
    [FileAttachment("Filedata")]
    [ImageName("Demo_String_InSpecialFormat_Properties")]
    [NavigationItem("Navigazione Documenti")]
    public class NavigazioneDocumenti : XPLiteObject
    {
        public NavigazioneDocumenti()
            : base()
        {
        }

        public NavigazioneDocumenti(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

        private string fcodice;
        [Key(false),
        Persistent("CODICE"),
        MemberDesignTimeVisibility(false)]      
        public string Codice
        {
            get
            {
                return fcodice;
            }
            set
            {
                SetPropertyValue<string>("Codice", ref fcodice, value);
            }
        }

        private TipoDocumento fTipofile;
        [Persistent("TIPOFILE")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Tipo File")]
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

        private string fDescrizione;
        [Persistent("DESCRIZIONE")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Descrizione")]
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

        private Contratti fCommessa;
        [Persistent("CONTRATTO"), System.ComponentModel.DisplayName("Commessa")]
        [ExplicitLoading()]
        public Contratti Commessa
        {
            get
            {
                return fCommessa;
            }
            set
            {
                SetPropertyValue<Contratti>("Commessa", ref fCommessa, value);

            }
        }


        private string fDescEdificio;
        [Persistent("CODEDIFICIO")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Cod. Immobile")]
        public string DescEdificio
        {
            get
            {
                return fDescEdificio;
            }
            set
            {
                SetPropertyValue<string>("DescEdificio", ref fDescEdificio, value);
            }
        }
        private Immobile fImmobile;
        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
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

        private string fCodServizio;
        [Persistent("CODSERVIZIO")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Cod.Servizio")]
        public string CodServizio
        {
            get
            {
                return fCodServizio;
            }
            set
            {
                SetPropertyValue<string>("CodServizio", ref fCodServizio, value);
            }
        }

        private Servizio fServizio;
        [Persistent("SERVIZIO")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Servizio")]
        [ExplicitLoading()]
        public Servizio Servizio
        {
            get
            {
                return fServizio;
            }
            set
            {
                SetPropertyValue<Servizio>("Servizio", ref fServizio, value);
            }
        }

        private string fCodAsset;
        [Persistent("CODASSET")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Cod.Apparato")]
        public string CodAsset
        {
            get
            {
                return fCodAsset;
            }
            set
            {
                SetPropertyValue<string>("CodAsset", ref fCodAsset, value);
            }
        }

        private Asset fAsset;
        [Persistent("ASSET")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Asset")]
        [ExplicitLoading()]
        public Asset Asset
        {
            get
            {
                return fAsset;
            }
            set
            {
                SetPropertyValue<Asset>("Asset", ref fAsset, value);
            }
        }

        private RdL fRdL;
        [Persistent("RDL")]
        [DevExpress.ExpressApp.DC.XafDisplayName("RdL")]
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
        [Persistent("REGRDL")]
        [DevExpress.ExpressApp.DC.XafDisplayName("RegRdL")]
        [ExplicitLoading()]

        public RegistroRdL RegistroRdL
        {
            get
            {
                return fRegistroRdL;
            }
            set
            {
                SetPropertyValue<RegistroRdL>("RegistroRdL", ref fRegistroRdL, value);
            }
        }



        [Persistent("FILEDATA")]    
        //[Aggregated,      ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ImmediatePostData(true)]
        [DevExpress.ExpressApp.DC.XafDisplayName("File")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData Filedata
        {
            get
            {
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("Filedata");
            }
            set
            {
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("Filedata", value);
            }
        }
    }
}
