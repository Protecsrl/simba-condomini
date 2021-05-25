using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using CAMS.Module.PropertyEditors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Diagnostics;
using System.Drawing;

namespace CAMS.Module.DBMail
{
    [DefaultClassOptions, Persistent("SEGNALAZIONEMAIL")]  //[System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Segnalazioni Mail")]
    [ImageName("ShowTestReport")]
    [NavigationItem("Segnalazioni")]
    #region
  
    //    //    [RuleCriteria("RuleInfo.RdL.DataInizioLavori11", DefaultContexts.Save, @"[DataRichiesta] <= DataInizioLavori",
    //    //CustomMessageTemplate = "Informazione: La Data di InizioLavori RdL ({DataInizioLavori}) deve essere maggiore della data di Richiesta({DataRichiesta}).",
    //    //SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Information)]
    #endregion

    public class SegnalazioneMail : XPObject
    {
        // private const int GiorniRitardoRicerca = -7;
        public SegnalazioneMail() : base() { }
        public SegnalazioneMail(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1) // crea RdL
            {
              
            }
            else
            {
                Debug.WriteLine("qui carica");
            }
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private string fRicevutoDa;
        [Persistent("RICEVUTODA"), System.ComponentModel.DisplayName("Ricevuto Da")]
        [Index(1)]
        [Size(100), DbType("varchar(100)")]
        [VisibleInListView(false),VisibleInDetailView(true)]
        public string RicevutoDa
        {
            get
            {
                return fRicevutoDa;
            }
            set
            {
                SetPropertyValue<string>("RicevutoDa", ref fRicevutoDa, value);
            }
        }

        private string fDataInvio;
        [Persistent("DATAINVIO"), System.ComponentModel.DisplayName("Data Invio")] 
        [Size(100), DbType("varchar(100)")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public string DataInvio
        {
            get
            {
                return fDataInvio;
            }
            set
            {
                SetPropertyValue<string>("DataInvio", ref fDataInvio, value);
            }
        }

        private string fOggetto;
        [Persistent("OGGETTO"), System.ComponentModel.DisplayName("Oggetto")]
        [Size(300), DbType("varchar(300)")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public string Oggetto
        {
            get
            {
                return fOggetto;
            }
            set
            {
                SetPropertyValue<string>("Oggetto", ref fOggetto, value);
            }
        }

        private string fCorpo;
        [Persistent("CORPO"), System.ComponentModel.DisplayName("Corpo")]      // [VisibleInListView(false)]
        [Size(SizeAttribute.Unlimited)]
        //[DbType("CLOB")]
        [Index(2)]
        public string Corpo
        {
            get
            {
                return fCorpo;
            }
            set
            {
                SetPropertyValue<string>("Corpo", ref fCorpo, value);
            }
        }

       

        private string fReceived;
        [Persistent("RICEVUTO"), System.ComponentModel.DisplayName("Received")]
        [Size(100), DbType("varchar(100)")]
        [VisibleInListView(false),VisibleInDetailView(true)]
        public string Received
        {
            get
            {
                return fReceived;
            }
            set
            {
                SetPropertyValue<string>("Received", ref fReceived, value);
            }
        }

        private string fMessaggioID;
        [Persistent("MESSAGGIOID"), System.ComponentModel.DisplayName("Messaggio ID")]
        [Size(100), DbType("varchar(100)")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public string MessaggioID
        {
            get
            {
                return fMessaggioID;
            }
            set
            {
                SetPropertyValue<string>("MessaggioID", ref fMessaggioID, value);
            }
        }
        //   Data_e_Ora_Min_EditMask
        private DateTime fDatadiCreazione;
        [Persistent("DATACREAZIONE"), System.ComponentModel.DisplayName("Data di Registrazione")]
        [Index(0)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask)]
        public DateTime DatadiCreazione
        {
            get
            {
                return fDatadiCreazione;
            }
            set
            {
                SetPropertyValue<DateTime>("DatadiCreazione", ref fDatadiCreazione, value);
            }
        }

        private RdL fRdL;
        [Persistent("RDL"),        DisplayName("RdL")]
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




        #region  campi aggiornati per spedizione
        private string fstrAvviso;
        [Persistent("STRAVVISO"), System.ComponentModel.DisplayName("Stringa Avviso")]
        [Size(100), DbType("varchar(100)")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        public string strAvviso
        {
            get
            {
                return fstrAvviso;
            }
            set
            {
                SetPropertyValue<string>("strAvviso", ref fstrAvviso, value);
            }
        }
        private string fstrAssemblaggio;
        [Persistent("ASSEMBLAGGIO"), System.ComponentModel.DisplayName("Stringa Assemblaggio")]
        [Size(100), DbType("varchar(100)")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        public string strAssemblaggio
        {
            get
            {
                return fstrAssemblaggio;
            }
            set
            {
                SetPropertyValue<string>("strAssemblaggio", ref fstrAssemblaggio, value);
            }
        }
        private string fstrDescrizione;
        [Persistent("STRDESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione")]
        [Size(300), DbType("varchar(300)")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        public string strDescrizione
        {
            get
            {
                return fstrDescrizione;
            }
            set
            {
                SetPropertyValue<string>("strDescrizione", ref fstrDescrizione, value);
            }
        }

        private string fstrEdificio;
        [Persistent("STREDIFICIO"), System.ComponentModel.DisplayName("str Immobile")]
        [Size(300), DbType("varchar(300)")]
        [VisibleInListView(true), VisibleInDetailView(true)]
        public string strEdificio
        {
            get
            {
                return fstrEdificio;
            }
            set
            {
                SetPropertyValue<string>("strEdificio", ref fstrEdificio, value);
            }
        }

        private Immobile fImmobile;
        [Persistent("IMMOBILE"), DevExpress.Xpo.DisplayName("Immobile")]
        [RuleRequiredField("RuleReq.Segnalazioni.Immobile", DefaultContexts.Save, "Immobile è un campo obbligatorio")]
        [VisibleInListView(true), ImmediatePostData(true)]
        [Index(4)]
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

        private StatoSegnalazione fStatoSegnalazione;  //TipoContattoCliente
        [Persistent("STATO"), DisplayName("Stato")]
        [Index(5)]
        public StatoSegnalazione StatoSegnalazione
        {
            get
            {
                return fStatoSegnalazione;
            }
            set
            {
                SetPropertyValue<StatoSegnalazione>("StatoSegnalazione", ref fStatoSegnalazione, value);
            }
        }

        #endregion



        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!this.IsLoading)
            {
                if (this.Oid == -1)
                {
                    if (newValue != null && propertyName == "Categoria")
                    {
                        int newOid = ((DevExpress.Xpo.XPObject)(newValue)).Oid;
                        //if (newOid != 4)
                        //{
                        //    this.TipoIntervento = Session.GetObjectByKey<TipoIntervento>(0);
                        //}
                    }
                    //
                    if (newValue != null && propertyName == "Immobile")
                    {
                        Immobile newEdificio = ((Immobile)(newValue));
                        if (newValue != oldValue && newValue != null)
                        {
                            //this.Richiedente = (Richiedente)Session.Query<Richiedente>().Where(w => w.Commesse == newEdificio.Commesse).FirstOrDefault();
                            ////this.Session.GetObjects<Richiedente>().Where(w => w.Commesse == newEdificio.Commesse).FirstOrDefault();
                            //CAMS.Module.Classi.SetVarSessione.OidEdificioCalcoloDistanze = newEdificio.Oid;
                            //SetVarSessione.OidEdificioCalcoloDistanzeLatitudine = double.Parse(newEdificio.Indirizzo.Latitude.ToString());
                            //SetVarSessione.OidEdificioCalcoloDistanzeLongitudine = double.Parse(newEdificio.Indirizzo.Longitude.ToString());
                        }
                    }

                    if (newValue != null && propertyName == "Apparato")
                    {
                        Asset newApparato = (Asset)(newValue);
                        if (newValue != oldValue || newValue != null)
                        {
                            //this.Problema = null;
                            //this.ProblemaCausa = null;
                        }
                    }
                }

                if (this.Oid > 1)
                {
                    if (newValue != null && propertyName == "UltimoStatoSmistamento")
                    {
                        if (newValue != oldValue && newValue != null)
                        {

                        }
                    }
                }

            }
            //Richiedente

        }
        
        
        protected override void OnSaved()
        {
            base.OnSaved();

        }

        public override string ToString()
        {
            return this.Oggetto;
        }
    }
}
