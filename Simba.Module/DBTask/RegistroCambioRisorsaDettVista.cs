using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.ComponentModel;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("V_REGCAMBIORISORSADETT")]
    [System.ComponentModel.DefaultProperty("FullName")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettaglio Cambio Risorsa")]
    [ImageName("BO_Country_v92")]
    [NavigationItem(false)]
    public class RegistroCambioRisorsaDettVista : XPLiteObject
    {

        public RegistroCambioRisorsaDettVista() : base() { }

        public RegistroCambioRisorsaDettVista(Session session) : base(session) { }

        private string fcodice;
        [Key, Persistent("CODICE"), MemberDesignTimeVisibility(false)]
        public string Codice
        {
            get
            {
                return fcodice;
            }
            set
            {
                SetPropertyValue<string>("Codice", ref fcodice, value);
            }
        }

        private RegistroRdL fRegistroRdL;
        [Persistent("REGRDL"), XafDisplayName("Registro RdL")]
        public RegistroRdL RegistroRdL
        {
            get
            {
                return fRegistroRdL;
            }
            set
            {
                SetPropertyValue<RegistroRdL>("RegistroRdL", ref fRegistroRdL, value);
            }
        }


        private RisorseTeam fTRisorsa;
        [Persistent("TRISORSA"), XafDisplayName("RisorsaTeam")]
        [ExplicitLoading]
        public RisorseTeam TRisorsa
        {
            get
            {
                return fTRisorsa;
            }
            set
            {
                SetPropertyValue<RisorseTeam>("TRisorsa", ref fTRisorsa, value);
            }
        }

        private DateTime fDataOraCambio;
        [Persistent("DATAORACAMBIO"), XafDisplayName("Data Ora Cambio"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        public DateTime DataOraCambio
        {
            get
            {
                return fDataOraCambio;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOraCambio", ref fDataOraCambio, value);
            }
        }

        // [Calculated("FirstName + ' ' + LastName")]
        [PersistentAlias("Iif(TRisorsa is not null,this.TRisorsa.RisorsaCapo.Cognome + ', ' + this.TRisorsa.RisorsaCapo.Nome  ,null)")]  
        [XafDisplayName("FullName")]
        [Browsable(false)]
        public string FullName
        {
            get
            {
            var tempObject = EvaluateAlias("FullName");
                if (tempObject != null)
                {
                    return tempObject.ToString();
                }
                   return null;
            }
        }

    }
}

 