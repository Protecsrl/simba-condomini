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

    public partial class Condominium : XPLiteObject
    {
        int fOid;
        [Key(true)]
        public int Oid
        {
            get { return fOid; }
            set { SetPropertyValue<int>(nameof(Oid), ref fOid, value); }
        }
        int fComune;
        public int Comune
        {
            get { return fComune; }
            set { SetPropertyValue<int>(nameof(Comune), ref fComune, value); }
        }
        string fNomeCondominio;
        [Size(50)]
        public string NomeCondominio
        {
            get { return fNomeCondominio; }
            set { SetPropertyValue<string>(nameof(NomeCondominio), ref fNomeCondominio, value); }
        }
        string fIndirizzo;
        [Size(500)]
        public string Indirizzo
        {
            get { return fIndirizzo; }
            set { SetPropertyValue<string>(nameof(Indirizzo), ref fIndirizzo, value); }
        }
        string fPartitaIva;
        [Size(11)]
        public string PartitaIva
        {
            get { return fPartitaIva; }
            set { SetPropertyValue<string>(nameof(PartitaIva), ref fPartitaIva, value); }
        }
        double fLatitudine;
        public double Latitudine
        {
            get { return fLatitudine; }
            set { SetPropertyValue<double>(nameof(Latitudine), ref fLatitudine, value); }
        }
        double fLongitudine;
        public double Longitudine
        {
            get { return fLongitudine; }
            set { SetPropertyValue<double>(nameof(Longitudine), ref fLongitudine, value); }
        }
        [Association(@"CommunicationsReferencesCondominium")]
        public XPCollection<Communications> CommunicationsCollection { get { return GetCollection<Communications>(nameof(CommunicationsCollection)); } }
        [Association(@"TicketReferencesCondominium")]
        public XPCollection<Ticket> Tickets { get { return GetCollection<Ticket>(nameof(Tickets)); } }
        [Association(@"ContractsReferencesCondominium")]
        public XPCollection<Contracts> ContractsCollection { get { return GetCollection<Contracts>(nameof(ContractsCollection)); } }
        [Association(@"BuildingReferencesCondominium")]
        public XPCollection<Building> Buildings { get { return GetCollection<Building>(nameof(Buildings)); } }
        [Association(@"UserCondominiumReferencesCondominium")]
        public XPCollection<UserCondominium> UserCondominiums { get { return GetCollection<UserCondominium>(nameof(UserCondominiums)); } }
    }

}
