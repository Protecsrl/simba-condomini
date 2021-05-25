using System;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("COMUNI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Città")]
    [NavigationItem(false)]
    [ImageName("Action_Navigation")]
    public class Comuni : XPObject
    {
        public Comuni()
            : base()
        {
            
        }

        public Comuni(Session session)
            : base(session)
        {
            
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
          
        }

        private string fRipartizioneGeografica;
        [Size(1000), Persistent("RIPARTIZIONEGEOGRAFICA"), DisplayName("Ripartizione Geografica")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string RipartizioneGeografica
        {
            get
            {
                return fRipartizioneGeografica;
            }
            set
            {
                SetPropertyValue<string>("RipartizioneGeografica", ref fRipartizioneGeografica, value);
            }
        }

        private string fCodiceNUTS22010;
        [Size(1000), Persistent("CODICENUTS22010"), DisplayName("Codice NUTS2 2010")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string CodiceNUTS22010
        {
            get
            {
                return fCodiceNUTS22010;
            }
            set
            {
                SetPropertyValue<string>("CodiceNUTS22010", ref fCodiceNUTS22010, value);
            }
        }

        private string fCodiceNUTS32010;
        [Size(1000), Persistent("CODICENUTS32010"), DisplayName("Codice NUTS3 2010")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string CodiceNUTS32010
        {
            get
            {
                return fCodiceNUTS32010;
            }
            set
            {
                SetPropertyValue<string>("CodiceNUTS32010", ref fCodiceNUTS32010, value);
            }
        }

        private string fCodiceRegione;
        [Size(1000), Persistent("CODICEREGIONE"), DisplayName("Codice Regione")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string CodiceRegione
        {
            get
            {
                return fCodiceRegione;
            }
            set
            {
                SetPropertyValue<string>("CodiceRegione", ref fCodiceRegione, value);
            }
        }

        private string fCodiceProvincia;
        [Size(1000), Persistent("CODICEPROVINCIA"), DisplayName("Codice Provincia")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string CodiceProvincia
        {
            get
            {
                return fCodiceProvincia;
            }
            set
            {
                SetPropertyValue<string>("CodiceProvincia", ref fCodiceProvincia, value);
            }
        }

        private string fCodiceCittaMetro;
        [Size(1000), Persistent("CODICECITTAMETROPOLITANA"), DisplayName("Codice Citta Metropolitana")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string CodiceCittaMetro
        {
            get
            {
                return fCodiceCittaMetro;
            }
            set
            {
                SetPropertyValue<string>("CodiceCittaMetro", ref fCodiceCittaMetro, value);
            }
        }

        private string fNumeroProgComune;
        [Size(1000), Persistent("NUMPROGRESSIVOCOMUNE"), DisplayName("Numero Progressivo Comune")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string NumeroProgComune
        {
            get
            {
                return fNumeroProgComune;
            }
            set
            {
                SetPropertyValue<string>("NumeroProgComune", ref fNumeroProgComune, value);
            }
        }

        private string fCodiceIstat;
        [Size(1000), Persistent("CODICEISTAT"), DisplayName("Codice Istat")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string CodiceIstat
        {
            get
            {
                return fCodiceIstat;
            }
            set
            {
                SetPropertyValue<string>("CodiceIstat", ref fCodiceIstat, value);
            }
        }

        private string fCodiceIstatNum;
        [Size(1000), Persistent("CODICEISTATNUM"), DisplayName("Codice Istat Numerico")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string CodiceIstatNum
        {
            get
            {
                return fCodiceIstatNum;
            }
            set
            {
                SetPropertyValue<string>("CodiceIstatNum", ref fCodiceIstatNum, value);
            }
        }

        private string fCodiceIstat107prov;
        [Size(1000), Persistent("CODICEISTAT107PROV"), DisplayName("Codice Istat 107 Province")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string CodiceIstat107prov
        {
            get
            {
                return fCodiceIstat107prov;
            }
            set
            {
                SetPropertyValue<string>("CodiceIstat107prov", ref fCodiceIstat107prov, value);
            }
        }

        private string fCodiceIstat103prov;
        [Size(1000), Persistent("CODICEISTAT103PROV"), DisplayName("Codice Istat 103 Province")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string CodiceIstat103prov
        {
            get
            {
                return fCodiceIstat103prov;
            }
            set
            {
                SetPropertyValue<string>("CodiceIstat103prov", ref fCodiceIstat103prov, value);
            }
        }

        private string fCodiceCatastale;
        [Size(1000), Persistent("CODICECATASTALE"), DisplayName("Codice Catastale")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string CodiceCatastale
        {
            get
            {
                return fCodiceCatastale;
            }
            set
            {
                SetPropertyValue<string>("CodiceCatastale", ref fCodiceCatastale, value);
            }
        }

        private string fDenomIta;
        [Size(1000), Persistent("DENOMINAZIONEITALIANO"), DisplayName("Comune")]
        [DbType("varchar(1000)")]
        public string DenomIta
        {
            get
            {
                return fDenomIta;
            }
            set
            {
                SetPropertyValue<string>("DenomIta", ref fDenomIta, value);
            }
        }

        
        private string fCapoluogoProvincia;
        [Size(1000), Persistent("CAPOLUOGODIPROVINCIA"), DisplayName("Capoluogo di  Provincia")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string CapoluogoProvincia
        {
            get
            {
                return fCapoluogoProvincia;
            }
            set
            {
                SetPropertyValue<string>("CapoluogoProvincia", ref fCapoluogoProvincia, value);
            }
        }

        private string fZonaAltimetrica;
        [Size(1000), Persistent("ZONAALTIMETRICA"), DisplayName("Zona Altimetrica")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string ZonaAltimetrica
        {
            get
            {
                return fZonaAltimetrica;
            }
            set
            {
                SetPropertyValue<string>("ZonaAltimetrica", ref fZonaAltimetrica, value);
            }
        }

        private string fAltitudineCentro;
        [Size(1000), Persistent("ALTITUDINEDELCENTRO"), DisplayName("Altitudine del Centro")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string AltitudineCentro
        {
            get
            {
                return fAltitudineCentro;
            }
            set
            {
                SetPropertyValue<string>("AltitudineCentro", ref fAltitudineCentro, value);
            }
        }

        private string fComuneLitoraneo;
        [Size(1000), Persistent("COMUNELITORANEO"), DisplayName("Comune Litoraneo")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        [Delayed(true)] 
        public string ComuneLitoraneo
        {
            get
            {
                return GetDelayedPropertyValue<string>("ComuneLitoraneo");
                //return fComuneLitoraneo;
            }
            set
            {
                SetDelayedPropertyValue<string>("ComuneLitoraneo", value);
                //SetPropertyValue<string>("ComuneLitoraneo", ref fComuneLitoraneo, value);
            }
        }

        private string fComuneMontano;
        [Size(1000), Persistent("COMUNEMONTANO"), DisplayName("Comune Montano")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        [Delayed(true)]     
        public string ComuneMontano
        {
            get
            {
                return GetDelayedPropertyValue<string>("ComuneMontano");
                //return fComuneMontano;
            }
            set
            {
                SetDelayedPropertyValue<string>("ComuneMontano", value);
                //SetPropertyValue<string>("ComuneMontano", ref fComuneMontano, value);
            }
        }

        private string fSuperficieTerritoriale;
        [Size(1000), Persistent("SUPERFICIETERRITORIALE"), DisplayName("Superficie Territoriale")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string SuperficieTerritoriale
        {
            get
            {
                return fSuperficieTerritoriale;
            }
            set
            {
                SetPropertyValue<string>("SuperficieTerritoriale", ref fSuperficieTerritoriale, value);
            }
        }

        private string fPopolazione2001;
        [Size(1000), Persistent("POPOLAZIONE2001"), DisplayName("Popolazione 2001")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string Popolazione2001
        {
            get
            {
                return fPopolazione2001;
            }
            set
            {
                SetPropertyValue<string>("Popolazione2001", ref fPopolazione2001, value);
            }
        }

        private string fPopolazione2011;
        [Size(1000), Persistent("POPOLAZIONE2011"), DisplayName("Popolazione 2011")]
        [MemberDesignTimeVisibility(false)]
        [DbType("varchar(1000)")]
        public string Popolazione2011
        {
            get
            {
                return fPopolazione2011;
            }
            set
            {
                SetPropertyValue<string>("Popolazione2011", ref fPopolazione2011, value);
            }
        }

        private Provincia fProvincia;
        [Association(@"Comuni_Provincia"), Persistent("PROVINCIA"), DisplayName("Provincia")]
        [MemberDesignTimeVisibility(false)]
        public Provincia Provincia
        {
            get
            {
                return fProvincia;
            }
            set
            {
                SetPropertyValue<Provincia>("Provincia", ref fProvincia, value);
            }
        }

        private Regione fRegione;
         [Persistent("REGIONE"), DisplayName("Regione")]
         [MemberDesignTimeVisibility(false)]

        public Regione Regione
        {
            get
            {
                return fRegione;
            }
            set
            {
                SetPropertyValue<Regione>("Regione", ref fRegione, value);
            }
        }

        private string fCodOut;
        [Persistent("COD_OUT"), Size(50), DevExpress.Xpo.DisplayName("Cod Out")]
        [DbType("varchar(50)")]
        public string CodOut
        {
            get { return fCodOut; }
            set { SetPropertyValue<string>("CodOut", ref fCodOut, value); }
        }


        private int fIDDistretto;
        [Persistent("IDDISTRETTO"), DisplayName("IDDistretto")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public int IDDistretto
        {
            get { return fIDDistretto; }
            set { SetPropertyValue<int>("IDDistretto", ref fIDDistretto, value); }
        }

        private int fIDArea;
        [Persistent("IDAREA"), DisplayName("IDArea")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public int IDArea
        {
            get { return fIDArea; }
            set { SetPropertyValue<int>("IDArea", ref fIDArea, value); }
        }

        private int fIDPresidio;
        [Persistent("IDPRESIDIO"), DisplayName("IDPresidio")]
        [VisibleInListView(false)]
        [VisibleInLookupListView(false)]
        public int IDPresidio
        {
            get { return fIDPresidio; }
            set { SetPropertyValue<int>("IDPresidio", ref fIDPresidio, value); }
        }



        public override string ToString()
         {
             return string.Format("{0}({1})", this.DenomIta,this.Provincia);
         }


    }

}