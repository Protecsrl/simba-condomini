using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;


namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions, Persistent("SISTEMA")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", @"Unità Tecnologica")]
    [NavigationItem("Procedure Attivita")]
    [ImageName("Action_EditModel")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    public class Sistema : XPObject
    {
        public Sistema()
            : base()
        {
        }
        public Sistema(Session session)
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
        [RuleRequiredField("RuleReq.Sistema.CodUni", DefaultContexts.Save, "Codice Uni è un campo obbligatorio")]

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
        [Persistent("DESCRIZIONE"),
        Size(50)]
        [DbType("varchar(50)")]
        [RuleRequiredField("RReqField.Sistema.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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

        private string fCodDescrizione;
        [Persistent("COD_DESCRIZIONE"),
        Size(5)]
        [DbType("varchar(5)")]
        [RuleRequiredField("RReqField.Sistema.CodDescrizione", DefaultContexts.Save, "Il Codice Descrizione è un campo obbligatorio")]
        public string CodDescrizione
        {
            get
            {
                return fCodDescrizione;
            }
            set
            {
                SetPropertyValue<string>("CodDescrizione", ref fCodDescrizione, value);
            }
        }

        private string f_Utente;
        [Persistent("UTENTE"),
        Size(100),
        DisplayName("Utente")]
        [DbType("varchar(100)")]
        // [Appearance("Sistema.Utente", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public string Utente
        {
            get { return GetDelayedPropertyValue<string>("Utente"); }
            set { SetDelayedPropertyValue<string>("Utente", value); }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        // [Appearance("Sistema.DataAggiornamento", Enabled = false)]
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
        private SistemaClassi fSistemaClassi;
        [Association(@"SistemaClassi_Sistema"),
        Persistent("SISTEMACLASSI"),
        DisplayName("Classi Unità Tecnologica")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public SistemaClassi SistemaClassi
        {
            get
            {
                return GetDelayedPropertyValue<SistemaClassi>("SistemaClassi");
            }
            set
            {
                SetDelayedPropertyValue<SistemaClassi>("SistemaClassi", value);
            }

            //get
            //{
            //    return fSistemaClassi;
            //}
            //set
            //{
            //    SetPropertyValue<SistemaClassi>("SistemaClassi", ref fSistemaClassi, value);
            //}
        }

        [Association(@"Sistema_StdApparatoClassi", typeof(StdApparatoClassi)),
        DisplayName("StdApparato Classi")]
        public XPCollection<StdApparatoClassi> StdApparatoClassi
        {
            get
            {
                return GetCollection<StdApparatoClassi>("StdApparatoClassi");
            }
        }

        //public override string ToString()
        //{
        //    return string.Format("{0}({1})", Descrizione, CodUni);
        //}

    }
}
