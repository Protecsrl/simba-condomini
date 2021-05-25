using System;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions,
    Persistent("SISTEMACLASSI"),DevExpress.ExpressApp.Model.ModelDefault("Caption", "Classi Unità Tecnologica")]
    [NavigationItem("Procedure Attivita")]
    [ImageName("BO_Organization")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    public class SistemaClassi : XPObject
    {
        public SistemaClassi()
            : base()
        {
        }

        public SistemaClassi(Session session)
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
        [RuleRequiredField("RuleReq.SistemaClassi.CodUni", DefaultContexts.Save, "Codice Uni è un campo obbligatorio")]

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
        [RuleRequiredField("RuleReq.SistemaClassi.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]

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
        //[Appearance("SistemaClassi.Utente", Enabled = false)]
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

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[Appearance("SistemaClassi.DataAggiornamento", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        public DateTime DataAggiornamento
        {
            get
            {
                return fDataAggiornamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value);
            }
        }


        private SistemaTecnologico fSistemaTecnologico;
        [Association(@"SistemaTecnologico_SistemaClassi"),
        Persistent("SISTEMATECNOLOGICO"),
        DisplayName("Sistema Tecnologico")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public SistemaTecnologico SistemaTecnologico
        {
            get
            {
                return fSistemaTecnologico;
            }
            set
            {
                SetPropertyValue<SistemaTecnologico>("SistemaTecnologico", ref fSistemaTecnologico, value);
            }
        }

        [Association(@"SistemaClassi_Sistema", typeof(Sistema)),
        DisplayName("Sistema")]
        public XPCollection<Sistema> Sistema
        {
            get
            {
                return GetCollection<Sistema>("Sistema");
            }
        }

        //public override string ToString()
        //{

        //    return string.Format("{0}({1})", Descrizione, CodUni);
        //}

    }
}
