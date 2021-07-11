﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Simba.DataLayer.simba_condomini
{

    public partial class Comuni : XPLiteObject
    {
        int fOid;
        [Key]
        public int Oid
        {
            get { return fOid; }
            set { SetPropertyValue<int>(nameof(Oid), ref fOid, value); }
        }
        string fRIPARTIZIONEGEOGRAFICA;
        [Size(1000)]
        public string RIPARTIZIONEGEOGRAFICA
        {
            get { return fRIPARTIZIONEGEOGRAFICA; }
            set { SetPropertyValue<string>(nameof(RIPARTIZIONEGEOGRAFICA), ref fRIPARTIZIONEGEOGRAFICA, value); }
        }
        string fCODICENUTS22010;
        [Size(1000)]
        public string CODICENUTS22010
        {
            get { return fCODICENUTS22010; }
            set { SetPropertyValue<string>(nameof(CODICENUTS22010), ref fCODICENUTS22010, value); }
        }
        string fCODICENUTS32010;
        [Size(1000)]
        public string CODICENUTS32010
        {
            get { return fCODICENUTS32010; }
            set { SetPropertyValue<string>(nameof(CODICENUTS32010), ref fCODICENUTS32010, value); }
        }
        string fCODICEREGIONE;
        [Size(1000)]
        public string CODICEREGIONE
        {
            get { return fCODICEREGIONE; }
            set { SetPropertyValue<string>(nameof(CODICEREGIONE), ref fCODICEREGIONE, value); }
        }
        string fCODICEPROVINCIA;
        [Size(1000)]
        public string CODICEPROVINCIA
        {
            get { return fCODICEPROVINCIA; }
            set { SetPropertyValue<string>(nameof(CODICEPROVINCIA), ref fCODICEPROVINCIA, value); }
        }
        string fCODICECITTAMETROPOLITANA;
        [Size(1000)]
        public string CODICECITTAMETROPOLITANA
        {
            get { return fCODICECITTAMETROPOLITANA; }
            set { SetPropertyValue<string>(nameof(CODICECITTAMETROPOLITANA), ref fCODICECITTAMETROPOLITANA, value); }
        }
        string fNUMPROGRESSIVOCOMUNE;
        [Size(1000)]
        public string NUMPROGRESSIVOCOMUNE
        {
            get { return fNUMPROGRESSIVOCOMUNE; }
            set { SetPropertyValue<string>(nameof(NUMPROGRESSIVOCOMUNE), ref fNUMPROGRESSIVOCOMUNE, value); }
        }
        string fCODICEISTAT;
        [Size(1000)]
        public string CODICEISTAT
        {
            get { return fCODICEISTAT; }
            set { SetPropertyValue<string>(nameof(CODICEISTAT), ref fCODICEISTAT, value); }
        }
        string fCODICEISTATNUM;
        [Size(1000)]
        public string CODICEISTATNUM
        {
            get { return fCODICEISTATNUM; }
            set { SetPropertyValue<string>(nameof(CODICEISTATNUM), ref fCODICEISTATNUM, value); }
        }
        string fCODICEISTAT107PROV;
        [Size(1000)]
        public string CODICEISTAT107PROV
        {
            get { return fCODICEISTAT107PROV; }
            set { SetPropertyValue<string>(nameof(CODICEISTAT107PROV), ref fCODICEISTAT107PROV, value); }
        }
        string fCODICEISTAT103PROV;
        [Size(1000)]
        public string CODICEISTAT103PROV
        {
            get { return fCODICEISTAT103PROV; }
            set { SetPropertyValue<string>(nameof(CODICEISTAT103PROV), ref fCODICEISTAT103PROV, value); }
        }
        string fCODICECATASTALE;
        [Size(1000)]
        public string CODICECATASTALE
        {
            get { return fCODICECATASTALE; }
            set { SetPropertyValue<string>(nameof(CODICECATASTALE), ref fCODICECATASTALE, value); }
        }
        string fDENOMINAZIONEITALIANO;
        [Size(1000)]
        public string DENOMINAZIONEITALIANO
        {
            get { return fDENOMINAZIONEITALIANO; }
            set { SetPropertyValue<string>(nameof(DENOMINAZIONEITALIANO), ref fDENOMINAZIONEITALIANO, value); }
        }
        string fCAPOLUOGODIPROVINCIA;
        [Size(1000)]
        public string CAPOLUOGODIPROVINCIA
        {
            get { return fCAPOLUOGODIPROVINCIA; }
            set { SetPropertyValue<string>(nameof(CAPOLUOGODIPROVINCIA), ref fCAPOLUOGODIPROVINCIA, value); }
        }
        string fZONAALTIMETRICA;
        [Size(1000)]
        public string ZONAALTIMETRICA
        {
            get { return fZONAALTIMETRICA; }
            set { SetPropertyValue<string>(nameof(ZONAALTIMETRICA), ref fZONAALTIMETRICA, value); }
        }
        string fALTITUDINEDELCENTRO;
        [Size(1000)]
        public string ALTITUDINEDELCENTRO
        {
            get { return fALTITUDINEDELCENTRO; }
            set { SetPropertyValue<string>(nameof(ALTITUDINEDELCENTRO), ref fALTITUDINEDELCENTRO, value); }
        }
        string fCOMUNELITORANEO;
        [Size(1000)]
        public string COMUNELITORANEO
        {
            get { return fCOMUNELITORANEO; }
            set { SetPropertyValue<string>(nameof(COMUNELITORANEO), ref fCOMUNELITORANEO, value); }
        }
        string fCOMUNEMONTANO;
        [Size(1000)]
        public string COMUNEMONTANO
        {
            get { return fCOMUNEMONTANO; }
            set { SetPropertyValue<string>(nameof(COMUNEMONTANO), ref fCOMUNEMONTANO, value); }
        }
        string fSUPERFICIETERRITORIALE;
        [Size(1000)]
        public string SUPERFICIETERRITORIALE
        {
            get { return fSUPERFICIETERRITORIALE; }
            set { SetPropertyValue<string>(nameof(SUPERFICIETERRITORIALE), ref fSUPERFICIETERRITORIALE, value); }
        }
        string fPOPOLAZIONE2001;
        [Size(1000)]
        public string POPOLAZIONE2001
        {
            get { return fPOPOLAZIONE2001; }
            set { SetPropertyValue<string>(nameof(POPOLAZIONE2001), ref fPOPOLAZIONE2001, value); }
        }
        string fPOPOLAZIONE2011;
        [Size(1000)]
        public string POPOLAZIONE2011
        {
            get { return fPOPOLAZIONE2011; }
            set { SetPropertyValue<string>(nameof(POPOLAZIONE2011), ref fPOPOLAZIONE2011, value); }
        }
        Provincia fPROVINCIA;
        [Association(@"ComuniReferencesProvincia")]
        public Provincia PROVINCIA
        {
            get { return fPROVINCIA; }
            set { SetPropertyValue<Provincia>(nameof(PROVINCIA), ref fPROVINCIA, value); }
        }
        [Association(@"CondominiumReferencesComuni")]
        public XPCollection<Condominium> Condominiums { get { return GetCollection<Condominium>(nameof(Condominiums)); } }
    }

}
