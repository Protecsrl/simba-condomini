using CAMS.Module.DBALibrary;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;


namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions,Persistent("APPCARVALORESELEZIONE")]
    [NavigationItem(false)]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", " Valore a Selezione")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    //[RuleCombinationOfPropertiesIsUnique("RuleCombIsUnique_AppCaratteristicaValoreSelezione", DefaultContexts.Save, "StdApparatoCaratteristicheTecniche,Descrizione",
    //CustomMessageTemplate = @"Attenzione è stato già inserito una Descrizione({Descrizione}) già presente questo Tipo di Apparato ({StandardApparato}). \r\nInserire Nuovamente.",
    //SkipNullOrEmptyValues = false)]

   public class AppCaratteristicaValoreSelezione : XPObject
    {
        public AppCaratteristicaValoreSelezione()
            : base()
        {
        }

        public AppCaratteristicaValoreSelezione(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private string fDescrizione;
        [Size(250),
        Persistent("DESCRIZIONE"),
        DisplayName("Descrizione")]
        [DbType("varchar(250)")]
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


        private StdApparatoCaratteristicheTecniche fStdApparatoCaratteristicheTecniche;
        [Persistent("VALORESELEZIONE"), DisplayName("Caratteristica Tecnica")]
        [Association("StdApparatoCaratteristicheTecniche_AppCaratteristichaValoreSelezione")]
        [ImmediatePostData]
        public StdApparatoCaratteristicheTecniche StdApparatoCaratteristicheTecniche
        {
            get
            {
                return fStdApparatoCaratteristicheTecniche;
            }
            set
            {
                SetPropertyValue<StdApparatoCaratteristicheTecniche>("StdApparatoCaratteristicheTecniche", ref fStdApparatoCaratteristicheTecniche, value);
            }
        }
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"), System.ComponentModel.DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Aggiornamento dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
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

    }
}

