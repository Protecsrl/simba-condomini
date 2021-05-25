using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAMS.Module.Classi;
using CAMS.Module.DBALibrary;
using DevExpress.ExpressApp;
//using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;
using CAMS.Module.DBTransazioni;
using DevExpress.ExpressApp.Utils;

namespace CAMS.Module.DBDataImport
{
    [DefaultClassOptions, Persistent("REGDATAMAN")]
    [System.ComponentModel.DisplayName("Registro Gestione Dati")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Gestione Dati")]
    [ImageName("GroupFieldCollection")]
    //[System.ComponentModel.DefaultProperty("NomeCampo")]
    [VisibleInDashboards(false)]
    public class RegistroDataManagemet : XPObject
    {
        public RegistroDataManagemet() : base()
        {
        }

        public RegistroDataManagemet(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        //kkk
        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(100), DevExpress.Xpo.DisplayName("Descrizione")]
        [DbType("varchar(250)")]
        [RuleRequiredField("RReqField.RegistroDataManagemet.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }
        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        public FlgAbilitato Abilitato
        {
            get { return fAbilitato; }
            set { SetPropertyValue<FlgAbilitato>("Abilitato", ref fAbilitato, value); }
        }

        [Persistent("OBJECTTYPE"), DevExpress.Xpo.DisplayName("Tipo Dati")]
        [RuleRequiredField("RReqField.RegistroDataManagemet.ObjectType", DefaultContexts.Save, "Tipo Dati è un campo obbligatorio")]
        [ValueConverter(typeof(TypeToStringConverter)), ImmediatePostData]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        public Type ObjectType
        {
            get { return GetPropertyValue<Type>("ObjectType"); }
            set { SetPropertyValue<Type>("ObjectType", value); }
        }


        [Persistent("TRANSAZIONIAPP"), DevExpress.Xpo.DisplayName("App Transazioni")]
        [Delayed(true)]
        public AppTransazioni AppTransazioni
        {
            get { return GetDelayedPropertyValue<AppTransazioni>("AppTransazioni"); }
            set { SetDelayedPropertyValue<AppTransazioni>("AppTransazioni", value); }
        }

        [Association(@"RegistroDataManagemet.RegistroDataManagemetFiles", typeof(RegistroDataManagemetFiles)), Aggregated]
        [DevExpress.Xpo.DisplayName("File")]
        public XPCollection<RegistroDataManagemetFiles> RegistroDataManagemetFiless
        {
            get
            {
                return GetCollection<RegistroDataManagemetFiles>("RegistroDataManagemetFiless");
            }
        }

        [Association(@"RegistroDataManagemet.RegistroDataManagemetMapData", typeof(RegistroDataManagemetMapData)), Aggregated]
        [DevExpress.Xpo.DisplayName("Mappa Campi")]
        public XPCollection<RegistroDataManagemetMapData> RegistroDataManagemetMapDatas
        {
            get
            {
                return GetCollection<RegistroDataManagemetMapData>("RegistroDataManagemetMapDatas");
            }
        }
        private StatoElaborazioneJob fStatoElaborazioneImport;
        [Persistent("STATOELABORAZIONE"), Size(250), DevExpress.Xpo.DisplayName("Stato Elaborazione")]
        //[RuleRequiredField("RReqField.RegistroDataImportTentativi.TipoDiAcquisizione", DefaultContexts.Save, "Il Tipo Di Acquisizione è un campo obbligatorio")]
        public StatoElaborazioneJob StatoElaborazioneImport
        {
            get { return fStatoElaborazioneImport; }
            set { SetPropertyValue<StatoElaborazioneJob>("StatoElaborazioneImport", ref fStatoElaborazioneImport, value); }
        }
    }
}
