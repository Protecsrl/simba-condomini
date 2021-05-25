using CAMS.Module.Classi;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
Persistent("REGSPEDIZIONIDETTSMS")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettaglio SMS")]
    [ImageName("BO_StateMachine")]
    [NavigationItem(false)]
    public class RegistroSpedizioniSMSDett : XPObject
    {
        public RegistroSpedizioniSMSDett()
            : base()
        {
        }
        public RegistroSpedizioniSMSDett(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private RegistroSpedizioniDett fRegSpedizioniDett;
        [Association(@"RegSpedizioniDett_RegistroSpedizioniSMSDett")]
        [Persistent("REGSPEDIZIONIDETT"), DisplayName("RegistroSpedizioniDett")]
        [ExplicitLoading()]
        public RegistroSpedizioniDett RegSpedizioniDett
        {
            get
            {
                return fRegSpedizioniDett;
            }
            set
            {
                SetPropertyValue<RegistroSpedizioniDett>("RegSpedizioniDett", ref fRegSpedizioniDett, value);
            }
        }

        private TipoInvio fTipoInvio;
        [Persistent("TIPOINVIO"), DisplayName("Tipo Invio")]
        public TipoInvio TipoInvio
        {
            get
            {
                return fTipoInvio;
            }
            set
            {
                SetPropertyValue<TipoInvio>("TipoInvio", ref fTipoInvio, value);
            }
        }


        private string fDestinatarioSMS;
        [Size(25), Persistent("DESTINATARIOSMS"), DisplayName("Telefono Destinatario")]
        [DbType("varchar(25)")] //[DbType("varchar2(25)")]
        public string DestinatarioSMS
        {
            get
            {
                return fDestinatarioSMS;
            }
            set
            {
                SetPropertyValue<string>("DestinatarioSMS", ref fDestinatarioSMS, value);
            }
        }

        private EsitoInvioMailSMS fEsitoInvioMailSMS;
        [Persistent("ESITO"), DisplayName("Esito")]
        public EsitoInvioMailSMS EsitoInvioMailSMS
        {
            get
            {
                return fEsitoInvioMailSMS;
            }
            set
            {
                SetPropertyValue<EsitoInvioMailSMS>("EsitoInvioMailSMS", ref fEsitoInvioMailSMS, value);
            }
        }

        private DateTime fDataRicezione;
        [Persistent("DATARICEZIONE"),        DisplayName("Data Ricezione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMS.Module.PropertyEditors.CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMS.Module.PropertyEditors.CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask)]
        public DateTime DataRicezione
        {
            get
            {
                return fDataRicezione;
            }
            set
            {
                SetPropertyValue<DateTime>("DataRicezione", ref fDataRicezione, value);
            }
        }

        private DateTime fDataVerificaStato;
        [Persistent("DATAVERIFICA"), DevExpress.Xpo.DisplayName("Data Verifica Stato")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMS.Module.PropertyEditors.CAMSEditorCostantFormat.DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMS.Module.PropertyEditors.CAMSEditorCostantFormat.DateAndTimeOfDayEditMask)]
        public DateTime DataVerificaStato
        {
            get { return fDataVerificaStato; }
            set { SetPropertyValue<DateTime>("DataVerificaStato", ref fDataVerificaStato, value); }
        }

    }
}
