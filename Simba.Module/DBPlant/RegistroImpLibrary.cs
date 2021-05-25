using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.Classi;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions,
    Persistent("REGIMPLIBRARY")]
    [VisibleInDashboards(false)]
    [System.ComponentModel.DefaultProperty("ImpiantoLibrary")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Libreria Impianti")]
    [ImageName("RegistroLibreriaImpianti")]
    [NavigationItem(false)]

    public class RegistroImpLibrary : XPObject
    {
        public RegistroImpLibrary()
            : base()
        {
        }
        public RegistroImpLibrary(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private ServizioLibrary fImpiantoLibrary;
        [Persistent("IMPIANTOLIBRARY"),
        DisplayName("Libreria di Impianto")]
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
        private TipoModificheLibreriaImp fStatoModifica;
        [Persistent("TIPO_MODIFICA"),
        DisplayName("Tipo di Modifica")]
        public TipoModificheLibreriaImp StatoModifica
        {
            get
            {
                return fStatoModifica;
            }
            set
            {
                SetPropertyValue<TipoModificheLibreriaImp>("StatoModifica", ref fStatoModifica, value);
            }
        }

        [Association(@"REGISTROIMPLIBRARYRefDETTAGLIO", typeof(RegistroImpLibraryDett)),
        DisplayName("Dettaglio")]
        public XPCollection<RegistroImpLibraryDett> IMPIANTOLIBRERYLOGDETTs
        {
            get
            {
                return GetCollection<RegistroImpLibraryDett>("IMPIANTOLIBRERYLOGDETTs");
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

        private DateTime? fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento"),
         System.ComponentModel.Browsable(false)]

        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
      //  [System.ComponentModel.Browsable(false)]
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
