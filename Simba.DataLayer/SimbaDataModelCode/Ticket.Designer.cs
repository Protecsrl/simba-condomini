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

    public partial class Ticket : XPLiteObject
    {
        int fOid;
        [Key(true)]
        public int Oid
        {
            get { return fOid; }
            set { SetPropertyValue<int>(nameof(Oid), ref fOid, value); }
        }
        int fNumber;
        public int Number
        {
            get { return fNumber; }
            set { SetPropertyValue<int>(nameof(Number), ref fNumber, value); }
        }
        int fTicketStatus;
        public int TicketStatus
        {
            get { return fTicketStatus; }
            set { SetPropertyValue<int>(nameof(TicketStatus), ref fTicketStatus, value); }
        }
        DateTime fData;
        public DateTime Data
        {
            get { return fData; }
            set { SetPropertyValue<DateTime>(nameof(Data), ref fData, value); }
        }
        string fNote;
        [Size(SizeAttribute.Unlimited)]
        public string Note
        {
            get { return fNote; }
            set { SetPropertyValue<string>(nameof(Note), ref fNote, value); }
        }
        short fRating;
        public short Rating
        {
            get { return fRating; }
            set { SetPropertyValue<short>(nameof(Rating), ref fRating, value); }
        }
        DateTime fDateCreation;
        public DateTime DateCreation
        {
            get { return fDateCreation; }
            set { SetPropertyValue<DateTime>(nameof(DateCreation), ref fDateCreation, value); }
        }
        DateTime fDateUpdate;
        public DateTime DateUpdate
        {
            get { return fDateUpdate; }
            set { SetPropertyValue<DateTime>(nameof(DateUpdate), ref fDateUpdate, value); }
        }
        User fUser;
        [Association(@"TicketReferencesUser")]
        public User User
        {
            get { return fUser; }
            set { SetPropertyValue<User>(nameof(User), ref fUser, value); }
        }
        Condominium fCondominium;
        [Association(@"TicketReferencesCondominium")]
        public Condominium Condominium
        {
            get { return fCondominium; }
            set { SetPropertyValue<Condominium>(nameof(Condominium), ref fCondominium, value); }
        }
        string fDescrizione;
        [Size(SizeAttribute.Unlimited)]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>(nameof(Descrizione), ref fDescrizione, value); }
        }
        string fTitolo;
        [Size(50)]
        public string Titolo
        {
            get { return fTitolo; }
            set { SetPropertyValue<string>(nameof(Titolo), ref fTitolo, value); }
        }
        Building fBuilding;
        [Association(@"TicketReferencesBuilding")]
        public Building Building
        {
            get { return fBuilding; }
            set { SetPropertyValue<Building>(nameof(Building), ref fBuilding, value); }
        }
        Environment fEnviroment;
        [Association(@"TicketReferencesEnvironment")]
        public Environment Enviroment
        {
            get { return fEnviroment; }
            set { SetPropertyValue<Environment>(nameof(Enviroment), ref fEnviroment, value); }
        }
        string fCode;
        [Size(12)]
        public string Code
        {
            get { return fCode; }
            set { SetPropertyValue<string>(nameof(Code), ref fCode, value); }
        }
        [Association(@"TicketClassificationsReferencesTicket")]
        public XPCollection<TicketClassifications> TicketClassificationsCollection { get { return GetCollection<TicketClassifications>(nameof(TicketClassificationsCollection)); } }
        [Association(@"TicketDocumentReferencesTicket")]
        public XPCollection<TicketDocument> TicketDocuments { get { return GetCollection<TicketDocument>(nameof(TicketDocuments)); } }
        [Association(@"TicketStatusesReferencesTicket")]
        public XPCollection<TicketStatuses> TicketStatusesCollection { get { return GetCollection<TicketStatuses>(nameof(TicketStatusesCollection)); } }
    }

}
