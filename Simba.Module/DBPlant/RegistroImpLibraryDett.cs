using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.Classi;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions,
    Persistent("REGIMPLIBRARYDETT")]
    [VisibleInDashboards(false)]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettaglio Registro Libreria Impianti")]
    [ImageName("DettRegistroLibreriaImpianti")]
    [NavigationItem(false)]

    public class RegistroImpLibraryDett : XPObject
    {
        public RegistroImpLibraryDett()
            : base()
        {
        }
        public RegistroImpLibraryDett(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private ServizioLibrary fImpiantoLibrary;
        [ DisplayName("Impianto Library")]
        [Persistent("IMPIANTOLIBRARY")]
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

        private TipoModifica fTipoModifica;
        [Persistent("TIPO_MODIFICA")]
        public TipoModifica TipoModifica
        {
            get
            {
                return fTipoModifica;
            }
            set
            {
                SetPropertyValue<TipoModifica>("TipoModifica", ref fTipoModifica, value);
            }
        }


        private string fDettaglio;
        [Size(4000),
        Persistent("DETTAGLIO"),
        DisplayName("Dettaglio")]
        [DbType("varchar(4000)")]
        public string Dettaglio
        {
            get
            {
                return fDettaglio;
            }
            set
            {
                SetPropertyValue<string>("Dettaglio", ref fDettaglio, value);
            }
        }

        private DateTime fDataModifica;
        [Persistent("DATAMODIFICA"),
        DisplayName("Data Modifica"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        public DateTime DataModifica
        {
            get
            {
                return fDataModifica;
            }
            set
            {
                SetPropertyValue<DateTime>("DataModifica", ref fDataModifica, value);
            }
        }
        private RegistroImpLibrary fRegistroImpLibrary;
        [Association(@"REGISTROIMPLIBRARYRefDETTAGLIO"),
        DisplayName("Registro Libreria Impianti")]
        [Persistent("REGIMPLIBRARY")]
        public RegistroImpLibrary RegistroImpLibrary
        {
            get
            {
                return fRegistroImpLibrary;
            }
            set
            {
                SetPropertyValue<RegistroImpLibrary>("RegistroImpLibrary", ref fRegistroImpLibrary, value);
            }
        }
    }
}
