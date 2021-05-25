using System;
using CAMS.Module.DBALibrary;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBPlant
{
    [System.ComponentModel.DisplayName("Apparati Inseribili in Library")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Servizi Inseribili in Library")]
    [DefaultClassOptions, Persistent("V_IMPLIBAPPARATIINSERIBILI")]
    [VisibleInDashboards(false)]
    [NavigationItem(false)]

    public class ServizioLibraryAppInseribili : XPLiteObject
    {
        public ServizioLibraryAppInseribili(Session session)
            : base(session)
        {
        }
 

        private string fCodice;
        [Key,
        Persistent("CODICE"),
        MemberDesignTimeVisibility(false)]
        [Appearance("ImpiantoLibraryAppInseribili.Codice", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string Codice
        {
            get
            {
                return fCodice;
            }
            set
            {
                SetPropertyValue<string>("Codice", ref fCodice, value);
            }
        }

        private Sistema fSistema;
        [Persistent("SISTEMA"),
        DisplayName("Unità Tecnologica"),
        MemberDesignTimeVisibility(false)]
        [Appearance("ImpiantoLibraryAppInseribili.Sistema", Enabled = false)]
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

        private StdApparatoClassi fStdApparatoClassi;
        [Persistent("APPARATOSTDCLASSI"),
        DisplayName("Classi Tipo Apparato")]
        public StdApparatoClassi StdApparatoClassi
        {
            get
            {
                return fStdApparatoClassi;
            }
            set
            {
                SetPropertyValue<StdApparatoClassi>("StdApparatoClassi", ref fStdApparatoClassi, value);
            }
        }

        private StdAsset fStdApparato;
        [Persistent("STRANDARTDAPPARATO"),
        DisplayName("Tipo Apparato")]
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

        private KDimensione fKDimensione; ///  default per apparato  quindi prendi quello di default per tipo                                
        [Persistent("KDIMENSIONE"),
        DisplayName("KDimensione"),
        Size(40)]
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


        private ServizioLibrary fImpiantoLibrary;
        [Association(@"IMPIANTOLIBRERY_AppInseribili"),
        DisplayName("Impianto Library")]
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
    }
}
