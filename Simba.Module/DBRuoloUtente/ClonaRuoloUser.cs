using System;
using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CAMS.Module.DBRuoloUtente
{
    [NavigationItem("Amministrazione"),  System.ComponentModel.DisplayName("Gestione Ruoli")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Gestione Ruoli da Immobile")]
    [DefaultClassOptions,  Persistent("CLONARUOLOUSER")]
    [VisibleInDashboards(false)]
    public class ClonaRuoloUser : XPObject
    {
        public ClonaRuoloUser()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public ClonaRuoloUser(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        private SecuritySystemRole fSecurityRole;
        [Persistent("ROLESORGENTE"),  DisplayName("Ruolo")]
        [VisibleInListView(true)]
        public SecuritySystemRole SecurityRole
        {
            get
            {
                return fSecurityRole;
            }
            set
            {
                SetPropertyValue<SecuritySystemRole>("SecurityRole", ref fSecurityRole, value);
            }
        }

        private string fNuovoRuolo;
        [Size(150),  Persistent("NUOVORUOLO"),    DisplayName("Nuovo Ruolo")]
        [ImmediatePostData(true)]
        [DbType("varchar(150)")]
        public string NuovoRuolo
        {
            get
            {
                return fNuovoRuolo;
            }
            set
            {
                SetPropertyValue<string>("NuovoRuolo", ref fNuovoRuolo, value);
            }
        }

        private Immobile fImmobile;
        [ Persistent("IMMOBILE"), DisplayName("Immobile")]
        [RuleRequiredField("RuleReq.ClonaRuoloUser.Immobile", DefaultContexts.Save, "Immobile è un campo obbligatorio")]
        //[Appearance("RegistroCosti.Abilita.Immobile", Criteria = "(Impianto  is null) And (not(Descrizione is null))" , Enabled = false)]
        [ImmediatePostData(true)]
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



    }

}