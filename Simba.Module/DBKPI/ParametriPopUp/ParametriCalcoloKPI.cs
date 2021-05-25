using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Validation;
using System.ComponentModel;

namespace CAMS.Module.DBKPI.ParametriPopUp
{
    [NavigationItem(false), System.ComponentModel.DisplayName("Parametri Calcolo KPI")]
    [DefaultClassOptions, NonPersistent]
//    [RuleCriteria("ParametriCalcoloKPI.VerificaDate", DefaultContexts.Save, @"[DataOut] > [DataIn]",
//CustomMessageTemplate = "Attenzione: La data di inizio Calcolo ({DataIn}) deve essere Minore della data di fine Calcolo ({DataOut})",
//SkipNullOrEmptyValues = true, InvertResult = false, ResultType = ValidationResultType.Warning)]

    public class ParametriCalcoloKPI : XPObject
    {
        public ParametriCalcoloKPI()
            : base()
        {
        }
        public ParametriCalcoloKPI(Session session)
            : base(session)
        {
        }

        private string fDescrizione;
        [NonPersistent, XafDisplayName("Descrizione")]
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
        

        private DateTime fDataIn;
        [NonPersistent, XafDisplayName("Data Inizio Calcolo")]
        //[RuleValueComparison("ParametriCalcoloKPI.DataIn",
        //    DefaultContexts.Save, ValueComparisonType.GreaterThan, "LocalDateTimeToday()", ParametersMode.Expression)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_EditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di inizio calcolo dei KPI", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
       // [ImmediatePostData(true)]
        public DateTime DataIn
        {
            get
            {
                return fDataIn;
            }
            set
            {
                SetPropertyValue<DateTime>("DataIn", ref fDataIn, value);
            }
        }

        private DateTime fDataOut;
        [NonPersistent, XafDisplayName("Data fine Calcolo")]
        //[RuleValueComparison("ParametriCalcoloKPI.DataOut",
        //         DefaultContexts.Save,                
        //         ValueComparisonType.GreaterThan, "LocalDateTimeToday()",
        //          CustomMessageTemplate= "Attenzione: La data di inizio Calcolo ({DataIn}) deve essere Minore della data di fine Calcolo ({DataOut})",
        //         ParametersMode.Expression)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_EditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di fine calcolo dei KPI, deve essere maggiore della data inizio", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
      //  [ImmediatePostData(true)]
        public DateTime DataOut
        {
            get
            {
                return fDataOut;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOut", ref fDataOut, value);
            }
        }

        [NonPersistent, XafDisplayName("Verifica Inserimento Date")]
        [Browsable(false)]
        [RuleFromBoolProperty("ParametriCalcoloKPI.EspressioneDateValido",
            DefaultContexts.Save,
            CustomMessageTemplate= "Attenzione: La data di inizio Calcolo ({DataIn}) deve essere Minore della data di fine Calcolo ({DataOut})",
             UsedProperties = "DataOut,DataIn", SkipNullOrEmptyValues = false)]
        public bool IsComplexExpressionValid
        {
            get
            {
                return DataOut > DataIn;
            }
        }

        private DateTime DataMinima()
        {
            if (DataIn != DateTime.MinValue)
            {
                return DataIn;
            }
            return DateTime.Now; ;
        }

    }
}
