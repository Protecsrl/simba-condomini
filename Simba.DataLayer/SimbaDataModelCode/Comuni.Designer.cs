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
namespace Simba.DataLayer.Database
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
        string fRipartizioneGeografica;
        [Size(1000)]
        public string RipartizioneGeografica
        {
            get { return fRipartizioneGeografica; }
            set { SetPropertyValue<string>(nameof(RipartizioneGeografica), ref fRipartizioneGeografica, value); }
        }
        string fCodiceNuts22010;
        [Size(1000)]
        public string CodiceNuts22010
        {
            get { return fCodiceNuts22010; }
            set { SetPropertyValue<string>(nameof(CodiceNuts22010), ref fCodiceNuts22010, value); }
        }
        string fCodiceNuts32010;
        [Size(1000)]
        public string CodiceNuts32010
        {
            get { return fCodiceNuts32010; }
            set { SetPropertyValue<string>(nameof(CodiceNuts32010), ref fCodiceNuts32010, value); }
        }
        string fCodiceRegione;
        [Size(1000)]
        public string CodiceRegione
        {
            get { return fCodiceRegione; }
            set { SetPropertyValue<string>(nameof(CodiceRegione), ref fCodiceRegione, value); }
        }
        string fCodiceProvincia;
        [Size(1000)]
        public string CodiceProvincia
        {
            get { return fCodiceProvincia; }
            set { SetPropertyValue<string>(nameof(CodiceProvincia), ref fCodiceProvincia, value); }
        }
        string fCodiceCittaMetropolitana;
        [Size(1000)]
        public string CodiceCittaMetropolitana
        {
            get { return fCodiceCittaMetropolitana; }
            set { SetPropertyValue<string>(nameof(CodiceCittaMetropolitana), ref fCodiceCittaMetropolitana, value); }
        }
        string fNumProgressivoComune;
        [Size(1000)]
        public string NumProgressivoComune
        {
            get { return fNumProgressivoComune; }
            set { SetPropertyValue<string>(nameof(NumProgressivoComune), ref fNumProgressivoComune, value); }
        }
        string fCodiceIstat;
        [Size(1000)]
        public string CodiceIstat
        {
            get { return fCodiceIstat; }
            set { SetPropertyValue<string>(nameof(CodiceIstat), ref fCodiceIstat, value); }
        }
        string fCodiceIstatNum;
        [Size(1000)]
        public string CodiceIstatNum
        {
            get { return fCodiceIstatNum; }
            set { SetPropertyValue<string>(nameof(CodiceIstatNum), ref fCodiceIstatNum, value); }
        }
        string fCodiceIstat107Prov;
        [Size(1000)]
        public string CodiceIstat107Prov
        {
            get { return fCodiceIstat107Prov; }
            set { SetPropertyValue<string>(nameof(CodiceIstat107Prov), ref fCodiceIstat107Prov, value); }
        }
        string fCodiceIstat103Prov;
        [Size(1000)]
        public string CodiceIstat103Prov
        {
            get { return fCodiceIstat103Prov; }
            set { SetPropertyValue<string>(nameof(CodiceIstat103Prov), ref fCodiceIstat103Prov, value); }
        }
        string fCodiceCatastale;
        [Size(1000)]
        public string CodiceCatastale
        {
            get { return fCodiceCatastale; }
            set { SetPropertyValue<string>(nameof(CodiceCatastale), ref fCodiceCatastale, value); }
        }
        string fDenominazioneItaliano;
        [Size(1000)]
        public string DenominazioneItaliano
        {
            get { return fDenominazioneItaliano; }
            set { SetPropertyValue<string>(nameof(DenominazioneItaliano), ref fDenominazioneItaliano, value); }
        }
        string fCapoluogoDiProvincia;
        [Size(1000)]
        public string CapoluogoDiProvincia
        {
            get { return fCapoluogoDiProvincia; }
            set { SetPropertyValue<string>(nameof(CapoluogoDiProvincia), ref fCapoluogoDiProvincia, value); }
        }
        string fZonaAltimetrica;
        [Size(1000)]
        public string ZonaAltimetrica
        {
            get { return fZonaAltimetrica; }
            set { SetPropertyValue<string>(nameof(ZonaAltimetrica), ref fZonaAltimetrica, value); }
        }
        string fAltitudineDelCentro;
        [Size(1000)]
        public string AltitudineDelCentro
        {
            get { return fAltitudineDelCentro; }
            set { SetPropertyValue<string>(nameof(AltitudineDelCentro), ref fAltitudineDelCentro, value); }
        }
        string fComuneLitoraneo;
        [Size(1000)]
        public string ComuneLitoraneo
        {
            get { return fComuneLitoraneo; }
            set { SetPropertyValue<string>(nameof(ComuneLitoraneo), ref fComuneLitoraneo, value); }
        }
        string fComuneMontano;
        [Size(1000)]
        public string ComuneMontano
        {
            get { return fComuneMontano; }
            set { SetPropertyValue<string>(nameof(ComuneMontano), ref fComuneMontano, value); }
        }
        string fSuperficieTerritoriale;
        [Size(1000)]
        public string SuperficieTerritoriale
        {
            get { return fSuperficieTerritoriale; }
            set { SetPropertyValue<string>(nameof(SuperficieTerritoriale), ref fSuperficieTerritoriale, value); }
        }
        string fPopolazione2001;
        [Size(1000)]
        public string Popolazione2001
        {
            get { return fPopolazione2001; }
            set { SetPropertyValue<string>(nameof(Popolazione2001), ref fPopolazione2001, value); }
        }
        string fPopolazione2011;
        [Size(1000)]
        public string Popolazione2011
        {
            get { return fPopolazione2011; }
            set { SetPropertyValue<string>(nameof(Popolazione2011), ref fPopolazione2011, value); }
        }
        Provincia fProvincia;
        [Association(@"ComuniReferencesProvincia")]
        public Provincia Provincia
        {
            get { return fProvincia; }
            set { SetPropertyValue<Provincia>(nameof(Provincia), ref fProvincia, value); }
        }
        [Association(@"CondominiumReferencesComuni")]
        public XPCollection<Condominium> Condominiums { get { return GetCollection<Condominium>(nameof(Condominiums)); } }
    }

}
