using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.ViewVariantsModule;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using CAMS.Module.BusinessObjects;
using CAMS.Module.Reports;
using CAMS.Module.DBTask.DC;
using CAMS.Module.DBMail;
using DevExpress.ExpressApp.Validation;
using System.Text.RegularExpressions;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Security;

namespace CAMS.Module
{
    public sealed partial class CAMSModule : ModuleBase
    {
        public CAMSModule()
        {
            InitializeComponent();
        }
        public override void CustomizeTypesInfo(ITypesInfo typesInfo)
        {
            base.CustomizeTypesInfo(typesInfo);
            CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
            ///   AGGIUNTO PER DC - DCRisorseTeamRdL
            TypeInfo typeInfo = (TypeInfo)typesInfo.FindTypeInfo(typeof(DCRisorseTeamRdL));
            typeInfo.KeyMember = typeInfo.FindMember("Oid");
            ///   AGGIUNTO PER DC - DCRisorseTeamRdL

            //typeInfo = null;
            //typeInfo = (TypeInfo)typesInfo.FindTypeInfo(typeof(DCDatiSMSMail));
            //typeInfo.KeyMember = typeInfo.FindMember("Oid");
            ///   AGGIUNTO PER DC - DCRisorseTeamRdL
            ///   
            //TypeInfo typeInfonpRdLRisorseTeam = (TypeInfo)typesInfo.FindTypeInfo(typeof(npRdLRisorseTeam));
            //typeInfonpRdLRisorseTeam.KeyMember = typeInfo.FindMember("ID");

        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
        {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);

            PredefinedReportsUpdater predefinedReportsUpdater = new PredefinedReportsUpdater(Application, objectSpace, versionFromDB);

            #region SU esimpi   --------   ESEMPIO
            // definizione report
            predefinedReportsUpdater.AddPredefinedReport<ContactsReport>("Contacts Report", typeof(Contact), true);
            #endregion


            #region SU Rigistro di Lavoro                     isInplaceReport: false(non visualizza il reprt nella classe) true invece lo visualizza
            predefinedReportsUpdater.AddPredefinedReport<XtraReportRegistroRdL>("Report Attività Manutenzione",
                typeof(CAMS.Module.DBTask.RegistroRdL), isInplaceReport: false);
            predefinedReportsUpdater.AddPredefinedReport<xReport.xrPermessoLavoro>("Permesso Lavoro",
                typeof(CAMS.Module.DBTask.RegistroRdL), isInplaceReport: false);
            predefinedReportsUpdater.AddPredefinedReport<xReport.xrRegRdLMP>("Manutenzione Programmata",
                typeof(CAMS.Module.DBTask.RegistroRdL), isInplaceReport: false);

            #endregion
            //  report interventi manutenzione ------------------------------------------------------------------
            #region             CAMS.Module.DBTask.Vista.RegRdLListView      - isInplaceReport: false(non visualizza il reprt nella classe) true invece lo visualizza

            predefinedReportsUpdater.AddPredefinedReport<xReport.xrRegRdL>
                ("Report Interventi Manutenzione",
                typeof(CAMS.Module.DBTask.Vista.RegRdLListView), isInplaceReport: false);// false(non visualizza in listview)   true
            #endregion


            #region   CAMS.Module.DBTask.Vista.RdLListViewReport usato sul pulsante - Report - isInplaceReport: false(non visualizza il reprt nella classe) true invece lo visualizza
            // C:\AssemblaPRT17\EAMS\CAMS.Module\xReport\SubReport\xrRegRdLSubreport.cs
            predefinedReportsUpdater.AddPredefinedReport<xReport.SubReport.xrRegRdLSubreport>
                ("Report Interventi di Manutenzione",
                typeof(CAMS.Module.DBTask.Vista.RdLListViewReport), isInplaceReport: true);


            predefinedReportsUpdater.AddPredefinedReport<xReport.DCRdLListReport>
         ("Report RdL di Manutenzione",
         typeof(CAMS.Module.DBTask.DC.DCRdLListReport), isInplaceReport: true);
            //C:\AssemblaPRT17\EAMS\CAMS.Module\xReport\DCRdLListReport.cs
            predefinedReportsUpdater.AddPredefinedReport<XtraReport3>
           ("DCReportRdL",
           typeof(CAMS.Module.DBTask.DC.DCRdLListReport), isInplaceReport: true);

            #endregion
            #region   CAMS.Module.DBALibrary.SchedaMp usato sul pulsante - Report - isInplaceReport: false(non visualizza il reprt nella classe) true invece lo visualizza

            ///  report su schede mp
            predefinedReportsUpdater.AddPredefinedReport<xReport.xrSchedeMP>("Piano Manutentivo",
                typeof(CAMS.Module.DBALibrary.SchedaMp), isInplaceReport: true);
            predefinedReportsUpdater.AddPredefinedReport<xReport.xrSchedeMPBase>("Piano Manutentivo Base",
                typeof(CAMS.Module.DBALibrary.SchedaMp), isInplaceReport: true);
            predefinedReportsUpdater.AddPredefinedReport<xReport.xrSchedeMPOpt>("Piano Manutentivo Ottimizzato",
                typeof(CAMS.Module.DBALibrary.SchedaMp), isInplaceReport: true);

            #endregion

            #region   CAMS.Module.DBPlanner.Ghost @@@@@usato sul pulsante - Report @@@@@- isInplaceReport: false(non visualizza il reprt nella classe) true invece lo visualizza

            predefinedReportsUpdater.AddPredefinedReport<xReport.xrGhost>("Ghost Pianificati",
                typeof(CAMS.Module.DBPlanner.Ghost), isInplaceReport: true);
            #endregion
            #region   CAMS.Module.DBPlanner.Ghost usato sul pulsante - Report - isInplaceReport: false(non visualizza il reprt nella classe) true invece lo visualizza
            predefinedReportsUpdater.AddPredefinedReport<xReport.xrReportManSG>("Interventi di Manutenzione",
                typeof(CAMS.Module.DBTask.Vista.RdLListViewSG), isInplaceReport: false);  // false non si visualizza sulla sua classe di riferimento
            #endregion


            #region   CAMS.Module.DBPlanner.Ghost usato sul pulsante - Report - isInplaceReport: false(non visualizza il reprt nella classe) true invece lo visualizza
            predefinedReportsUpdater.AddPredefinedReport<xReport.xrReportManSGMP>("Interventi di Manutenzione MP",
                typeof(CAMS.Module.DBTask.Vista.RegRdLListSGReport), isInplaceReport: true);

            #endregion

            #region   CAMS.Module.DBPlanner.Ghost usato sul pulsante - Report - isInplaceReport: false(non visualizza il reprt nella classe) true invece lo visualizza
            predefinedReportsUpdater.AddPredefinedReport<xReport.XtraReport1>("Apparati",
                typeof(CAMS.Module.DBPlant.Vista.AssetoMap), isInplaceReport: true);

            predefinedReportsUpdater.AddPredefinedReport<xrReportApparatoMap>("Apparati Qr-Code",
      typeof(CAMS.Module.DBPlant.Vista.ApparatoListViewQrCode), isInplaceReport: true);
            #endregion

            // eseguo aggioramento modulo   xrReportManSG.cs
            return new ModuleUpdater[] { updater, predefinedReportsUpdater };

        }
        static CAMSModule()
        {
            /*Note that you can specify the required format in a configuration file:
            <appSettings>
               <add key="FullAddressFormat" value="{Country.Name} {City} {Street}">
               <add key="FullAddressPersistentAlias" value="Country.Name+City+Street">
               ...
            </appSettings>

            ... and set the specified format here in code:
            Address.SetFullAddressFormat(ConfigurationManager.AppSettings["FullAddressFormat"], ConfigurationManager.AppSettings["FullAddressPersistentAlias"]);
            */

            Person.SetFullNameFormat("{LastName} {FirstName} {MiddleName}", "concat(FirstName, MiddleName, LastName)");
            Address.SetFullAddressFormat("City: {City}, Street: {Street}", "concat(City, Street)");

        }

        public override void Setup(ApplicationModulesManager moduleManager)
        {
            base.Setup(moduleManager);
            //ValidationRulesRegistrator.RegisterRule(moduleManager, typeof(FeatureCenter.Module.Validation.RuleStringLengthComparison), typeof(FeatureCenter.Module.Validation.IRuleStringLengthComparisonProperties));
            ValidationRulesRegistrator.RegisterRule(moduleManager, typeof(CAMS.Module.Validation.RuleStringLengthComparison), typeof(CAMS.Module.Validation.IRuleStringLengthComparisonProperties));

            //ValidationRulesRegistrator.RegisterRule(moduleManager, typeof(FeatureCenter.Module.Validation.CodeRuleObjectIsValidRule), typeof(DevExpress.Persistent.Validation.IRuleBaseProperties));
            // validazione password complessa
            ValidationRulesRegistrator.RegisterRule(moduleManager, typeof(PasswordStrengthCodeRule), typeof(IRuleBaseProperties));

        }
    }


    [CodeRule]
    public class PasswordStrengthCodeRule : RuleBase<ChangePasswordOnLogonParameters>
    {
        public PasswordStrengthCodeRule()
            : base("", "ChangePassword")
        {
            this.Properties.SkipNullOrEmptyValues = false;
        }
        public PasswordStrengthCodeRule(IRuleBaseProperties properties) : base(properties) { }
        protected override bool IsValidInternal(ChangePasswordOnLogonParameters target, out string errorMessageTemplate)
        {
            if (CalculatePasswordStrength(target.NewPassword) < 3)
            {
                errorMessageTemplate = "La sicurezza della password è insufficiente. La password deve contenere almeno otto caratteri ed almeno un carattere maiuscolo ed un Numero.";
                return false;
            }
            else
            {
                string pass = target.NewPassword;
                //errorMessageTemplate = "La sicurezza della password è insufficiente. La password deve contenere almeno otto caratteri ed almeno un carattere maiuscolo ed un Numero.";
                //return false;
            }

            errorMessageTemplate = string.Empty;
            return true;
        }
        private int CalculatePasswordStrength(string pwd)
        {
            int weight = 0;
            if (pwd == null) return weight;
            if (pwd.Length > 1 && pwd.Length < 6)  //    if (pwd.Length > 1 && pwd.Length < 4)  -- per 6
                ++weight;
            else
            {
                if (pwd.Length > 7)  //    if (pwd.Length > 5)    -- per 6
                    ++weight;
                Regex rxUpperCase = new Regex("[A-Z]");
                Regex rxLowerCase = new Regex("[a-z]");
                Regex rxNumerals = new Regex("[0-9]");
                Match match = rxUpperCase.Match(pwd);
                if (match.Success)
                    ++weight;
                match = rxLowerCase.Match(pwd);
                if (match.Success)
                    ++weight;
                match = rxNumerals.Match(pwd);
                if (match.Success)
                    ++weight;
            }
            if (weight == 3 && pwd.Length < 8)  //   if (weight == 3 && pwd.Length < 6)   -- per 6
                --weight;
            if (weight == 4 && pwd.Length > 10)
                ++weight;
            return weight;
        }
    }

}
