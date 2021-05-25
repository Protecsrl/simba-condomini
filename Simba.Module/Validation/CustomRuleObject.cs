//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CAMS.Module.Validation//{//    class CustomRuleObject //    {//    }//}
using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using System.Collections.ObjectModel;
//using DevExpress.ExpressApp.Demos;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace CAMS.Module.Validation
{
    public enum StringLengthComparisonMode { Equals, NotEquals, MoreThan, LessThan, NotMoreThan, NotLessThan }

    //[NavigationItem(Captions.ValidationGroup), System.ComponentModel.DisplayName(Captions.Validation_CustomRule)]
    //[Hint(Hints.CustomRuleHint)]

    [NavigationItem("Validazione"), System.ComponentModel.DisplayName("Validazione")]
    [ImageName("Validation.Demo_Custom_Rule")]
    public class CustomRuleObject : ValidationDemoBaseObject
    {
        private string moreThan5Symbols;
        private string notMoreThanTenSymbols;

        public CustomRuleObject(Session session) : base(session) { }
        [RuleStringLengthComparison("CustomRule_LengthMoreThan5", DefaultContexts.Save, 5, StringLengthComparisonMode.MoreThan)]
        public string MoreThan5Symbols
        {
            get { return moreThan5Symbols; }
            set { SetPropertyValue("MoreThan5Symbols", ref moreThan5Symbols, value); }
        }
        [RuleStringLengthComparison("CustomRule_LengthLessOrEqualsTo10", DefaultContexts.Save, 10, StringLengthComparisonMode.NotMoreThan)]
        public string NotMoreThanTenSymbols
        {
            get { return notMoreThanTenSymbols; }
            set { SetPropertyValue("NotMoreThanTenSymbols", ref notMoreThanTenSymbols, value); }
        }
    }
    [GenerateMessageTemplatesModel("RuleStringLengthComparison")]
    public interface IRuleStringLengthComparisonProperties : IRulePropertyValueProperties
    {
        [Category("Data")]
        StringLengthComparisonMode ComparisonMode { get; set; }
        [Required]
        [Category("Data")]
        int RightOperand { get; set; }
        [DefaultValue(@"The ""{TargetPropertyName}"" length must {ComparisonModeString} {RightOperand}.")]
        [Localizable(true)]
        [Category("Format")]
        string MessageTemplateStringLengthComparisonFailed { get; set; }
    }

    [NonPersistent]
    public class RuleStringLengthComparisonProperties : RulePropertyValueProperties, IRuleStringLengthComparisonProperties
    {
        private int rightOperand;
        private StringLengthComparisonMode comparisonMode;
        private string messageTemplateStringLengthComparisonFailed;

        public StringLengthComparisonMode ComparisonMode
        {
            get { return comparisonMode; }
            set { comparisonMode = value; }
        }
        public int RightOperand
        {
            get { return rightOperand; }
            set { rightOperand = value; }
        }
        public string MessageTemplateStringLengthComparisonFailed
        {
            get { return messageTemplateStringLengthComparisonFailed; }
            set { messageTemplateStringLengthComparisonFailed = value; }
        }
    }
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RuleStringLengthComparisonAttribute : RuleBaseAttribute
    {
        protected override Type RuleType
        {
            get { return typeof(RuleStringLengthComparison); }
        }
        protected override Type PropertiesType
        {
            get { return typeof(RuleStringLengthComparisonProperties); }
        }
        public RuleStringLengthComparisonAttribute(string id, string targetContextIDs, int rightOperand, StringLengthComparisonMode comparisonMode)
            : this(id, targetContextIDs, rightOperand, comparisonMode, string.Empty)
        {
        }
        public RuleStringLengthComparisonAttribute(string id, DefaultContexts targetContexts, int rightOperand, StringLengthComparisonMode comparisonMode)
            : this(id, targetContexts, rightOperand, comparisonMode, string.Empty)
        {
        }
        public RuleStringLengthComparisonAttribute(string id, string targetContextIDs, int rightOperand, StringLengthComparisonMode comparisonMode, string customMessageTemplate)
            : base(id, targetContextIDs, customMessageTemplate)
        {
            Properties.RightOperand = rightOperand;
            Properties.ComparisonMode = comparisonMode;
        }
        public RuleStringLengthComparisonAttribute(string id, DefaultContexts targetContexts, int rightOperand, StringLengthComparisonMode comparisonMode, string customMessageTemplate)
            : this(id, targetContexts.ToString(), rightOperand, comparisonMode, string.Empty)
        {
        }
        protected new IRuleStringLengthComparisonProperties Properties
        {
            get { return (IRuleStringLengthComparisonProperties)base.Properties; }
        }
    }

    public class RuleStringLengthComparison : RulePropertyValue<string>
    {
        private const string defaultTemplate = @"The ""{TargetPropertyName}"" length must {ComparisonModeString} {RightOperand}.";
        private static IValueManager<string> defaultMessageTemplateStringLengthComparisonFailed = ValueManager.GetValueManager<string>("RuleStringLengthComparison_DefaultMessageTemplateStringLengthComparisonFailed");
        public static string DefaultMessageTemplateStringLengthComparisonFailed
        {
            get
            {
                if (defaultMessageTemplateStringLengthComparisonFailed.Value == null)
                {
                    defaultMessageTemplateStringLengthComparisonFailed.Value = defaultTemplate;
                }
                return defaultMessageTemplateStringLengthComparisonFailed.Value;
            }
            set
            {
                defaultMessageTemplateStringLengthComparisonFailed.Value = value;
            }
        }
        protected override bool IsValueValid(string value, out string errorMessageTemplate)
        {
            int length = value.Length;
            errorMessageTemplate = Properties.MessageTemplateStringLengthComparisonFailed;
            switch (Properties.ComparisonMode)
            {
                case StringLengthComparisonMode.Equals:
                    return length == Properties.RightOperand;
                case StringLengthComparisonMode.NotEquals:
                    return length != Properties.RightOperand;
                case StringLengthComparisonMode.LessThan:
                    return length < Properties.RightOperand;
                case StringLengthComparisonMode.MoreThan:
                    return length > Properties.RightOperand;
                case StringLengthComparisonMode.NotLessThan:
                    return length >= Properties.RightOperand;
                case StringLengthComparisonMode.NotMoreThan:
                    return length <= Properties.RightOperand;
            }
            return true;
        }
        public RuleStringLengthComparison(IRuleStringLengthComparisonProperties properties)
            : base(properties)
        {
        }
        public new IRuleStringLengthComparisonProperties Properties
        {
            get { return (IRuleStringLengthComparisonProperties)base.Properties; }
        }
        public override Type PropertiesType
        {
            get { return typeof(RuleStringLengthComparisonProperties); }
        }
        public string ComparisonModeString
        {
            get
            {
                string result = "";
                switch (Properties.ComparisonMode)
                {
                    case StringLengthComparisonMode.Equals:
                        result = "be equal to";
                        break;
                    case StringLengthComparisonMode.NotEquals:
                        result = "not be equal to";
                        break;
                    case StringLengthComparisonMode.LessThan:
                        result = "be less than";
                        break;
                    case StringLengthComparisonMode.MoreThan:
                        result = "be more than";
                        break;
                    case StringLengthComparisonMode.NotLessThan:
                        result = "not be less than";
                        break;
                    case StringLengthComparisonMode.NotMoreThan:
                        result = "not be more than";
                        break;
                }
                return result;
            }
        }
    }
}
