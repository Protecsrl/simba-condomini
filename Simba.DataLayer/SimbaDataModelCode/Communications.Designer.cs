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

    public partial class Communications : XPLiteObject
    {
        int fOid;
        [Key(true)]
        public int Oid
        {
            get { return fOid; }
            set { SetPropertyValue<int>(nameof(Oid), ref fOid, value); }
        }
        string fTesto;
        [Size(50)]
        public string Testo
        {
            get { return fTesto; }
            set { SetPropertyValue<string>(nameof(Testo), ref fTesto, value); }
        }
        int fParentCommunication;
        public int ParentCommunication
        {
            get { return fParentCommunication; }
            set { SetPropertyValue<int>(nameof(ParentCommunication), ref fParentCommunication, value); }
        }
        User fUser;
        [Association(@"CommunicationsReferencesUser")]
        public User User
        {
            get { return fUser; }
            set { SetPropertyValue<User>(nameof(User), ref fUser, value); }
        }
        Condominium fCondominium;
        [Association(@"CommunicationsReferencesCondominium")]
        public Condominium Condominium
        {
            get { return fCondominium; }
            set { SetPropertyValue<Condominium>(nameof(Condominium), ref fCondominium, value); }
        }
        DateTime fDateInsert;
        public DateTime DateInsert
        {
            get { return fDateInsert; }
            set { SetPropertyValue<DateTime>(nameof(DateInsert), ref fDateInsert, value); }
        }
        CommunicationType fType;
        [Association(@"CommunicationsReferencesCommunicationType")]
        public CommunicationType Type
        {
            get { return fType; }
            set { SetPropertyValue<CommunicationType>(nameof(Type), ref fType, value); }
        }
        int fNumber;
        public int Number
        {
            get { return fNumber; }
            set { SetPropertyValue<int>(nameof(Number), ref fNumber, value); }
        }
    }

}
