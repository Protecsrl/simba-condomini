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

    public partial class User : XPLiteObject
    {
        int fOid;
        [Key(true)]
        public int Oid
        {
            get { return fOid; }
            set { SetPropertyValue<int>(nameof(Oid), ref fOid, value); }
        }
        string fUsername;
        [Size(50)]
        public string Username
        {
            get { return fUsername; }
            set { SetPropertyValue<string>(nameof(Username), ref fUsername, value); }
        }
        string fPassword;
        [Size(50)]
        public string Password
        {
            get { return fPassword; }
            set { SetPropertyValue<string>(nameof(Password), ref fPassword, value); }
        }
        UserType fUserType;
        [Association(@"UserReferencesUserType")]
        public UserType UserType
        {
            get { return fUserType; }
            set { SetPropertyValue<UserType>(nameof(UserType), ref fUserType, value); }
        }
        string fNome;
        [Size(50)]
        public string Nome
        {
            get { return fNome; }
            set { SetPropertyValue<string>(nameof(Nome), ref fNome, value); }
        }
        string fCongnome;
        [Size(50)]
        public string Congnome
        {
            get { return fCongnome; }
            set { SetPropertyValue<string>(nameof(Congnome), ref fCongnome, value); }
        }
        int fScala;
        public int Scala
        {
            get { return fScala; }
            set { SetPropertyValue<int>(nameof(Scala), ref fScala, value); }
        }
        Environment fEnvironment;
        [Association(@"UserReferencesEnvironment")]
        public Environment Environment
        {
            get { return fEnvironment; }
            set { SetPropertyValue<Environment>(nameof(Environment), ref fEnvironment, value); }
        }
        int fAzienda;
        public int Azienda
        {
            get { return fAzienda; }
            set { SetPropertyValue<int>(nameof(Azienda), ref fAzienda, value); }
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
        DateTime fDateInsert;
        public DateTime DateInsert
        {
            get { return fDateInsert; }
            set { SetPropertyValue<DateTime>(nameof(DateInsert), ref fDateInsert, value); }
        }
        DateTime fDateUpdate;
        public DateTime DateUpdate
        {
            get { return fDateUpdate; }
            set { SetPropertyValue<DateTime>(nameof(DateUpdate), ref fDateUpdate, value); }
        }
        Building fBuilding;
        [Association(@"UserReferencesBuilding")]
        public Building Building
        {
            get { return fBuilding; }
            set { SetPropertyValue<Building>(nameof(Building), ref fBuilding, value); }
        }
        [Association(@"CommunicationsReferencesUser")]
        public XPCollection<Communications> CommunicationsCollection { get { return GetCollection<Communications>(nameof(CommunicationsCollection)); } }
        [Association(@"TicketStatusesReferencesUser")]
        public XPCollection<TicketStatuses> TicketStatusesCollection { get { return GetCollection<TicketStatuses>(nameof(TicketStatusesCollection)); } }
        [Association(@"TicketDocumentReferencesUser")]
        public XPCollection<TicketDocument> TicketDocuments { get { return GetCollection<TicketDocument>(nameof(TicketDocuments)); } }
        [Association(@"TicketClassificationsReferencesUser")]
        public XPCollection<TicketClassifications> TicketClassificationsCollection { get { return GetCollection<TicketClassifications>(nameof(TicketClassificationsCollection)); } }
        [Association(@"TicketReferencesUser")]
        public XPCollection<Ticket> Tickets { get { return GetCollection<Ticket>(nameof(Tickets)); } }
        [Association(@"UserCondominiumReferencesUser")]
        public XPCollection<UserCondominium> UserCondominiums { get { return GetCollection<UserCondominium>(nameof(UserCondominiums)); } }
    }

}
