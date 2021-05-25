using System;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions,
    Persistent("APPARATOSTDCLASSI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Classi Tipo Apparato")]
    [NavigationItem("Procedure Attivita")]
    [ImageName("ManageItems")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    public class StdApparatoClassi : XPObject
    {
        public StdApparatoClassi()
            : base()
        {
        }

        public StdApparatoClassi(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private string fCodUni;
        [Size(20),
        Persistent("COD_UNI")]
        [DbType("varchar(20)")]
        [RuleRequiredField("RuleReq.StdApparatoClassi.CodUni", DefaultContexts.Save, "Codice Uni è un campo obbligatorio")]

        public string CodUni
        {
            get
            {
                return fCodUni;
            }
            set
            {
                SetPropertyValue<string>("CodUni", ref fCodUni, value);
            }
        }

        private string fDescrizione;
        [Size(200),
        Persistent("DESCRIZIONE")]
        [DbType("varchar(200)")]
        [RuleRequiredField("RuleReq.StdApparatoClassi.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]

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

        private string f_Utente;
        [Persistent("UTENTE"),
        Size(100),
        DisplayName("Utente")]
        [DbType("varchar(100)")]
        // [Appearance("StdApparatoClassi.Utente", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public string Utente
        {
            get
            {
                return GetDelayedPropertyValue<string>("Utente");
                //return fStdApparatoClassi;
            }
            set
            {
                SetDelayedPropertyValue<string>("Utente", value);
                //SetPropertyValue<StdApparatoClassi>("StdApparatoClassi", ref fStdApparatoClassi, value);
            }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[Appearance("StdApparatoClassi.DataAggiornamento", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public DateTime DataAggiornamento
        {
            get
            {
                return GetDelayedPropertyValue<DateTime>("DataAggiornamento");
            }
            set
            {
                SetDelayedPropertyValue<DateTime>("DataAggiornamento", value);
            }
        }

        private Sistema fSistema;
        [Association(@"Sistema_StdApparatoClassi"),
        Persistent("SISTEMA"), DisplayName("Unità Tecnologica")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Sistema Sistema
        {
            get
            {
                return GetDelayedPropertyValue<Sistema>("Sistema");
            }
            set
            {
                SetDelayedPropertyValue<Sistema>("Sistema", value);
            }

            //get
            //{
            //    return fSistema;
            //}
            //set
            //{
            //    SetPropertyValue<Sistema>("Sistema", ref fSistema, value);
            //}
        }

        [Association(@"StdApparatoClassi_StdApparato", typeof(StdAsset)),
        DisplayName("Std Apparato")]
        public XPCollection<StdAsset> StdApparato
        {
            get
            {
                return GetCollection<StdAsset>("StdApparato");
            }
        }

        //public override string ToString()
        //{
        //    return string.Format("{0}({1})", Descrizione, CodUni);
        //}

    }
}
