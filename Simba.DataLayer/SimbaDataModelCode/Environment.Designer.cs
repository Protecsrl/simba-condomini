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

    public partial class Environment : XPLiteObject
    {
        int fOid;
        [Key(true)]
        public int Oid
        {
            get { return fOid; }
            set { SetPropertyValue<int>(nameof(Oid), ref fOid, value); }
        }
        string fName;
        [Size(50)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>(nameof(Name), ref fName, value); }
        }
        string fDescription;
        [Size(500)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>(nameof(Description), ref fDescription, value); }
        }
        Building fBuilding;
        [Association(@"EnvironmentReferencesBuilding")]
        public Building Building
        {
            get { return fBuilding; }
            set { SetPropertyValue<Building>(nameof(Building), ref fBuilding, value); }
        }
        bool fValid;
        public bool Valid
        {
            get { return fValid; }
            set { SetPropertyValue<bool>(nameof(Valid), ref fValid, value); }
        }
        [Association(@"TicketReferencesEnvironment")]
        public XPCollection<Ticket> Tickets { get { return GetCollection<Ticket>(nameof(Tickets)); } }
        [Association(@"UserReferencesEnvironment")]
        public XPCollection<User> Users { get { return GetCollection<User>(nameof(Users)); } }
    }

}
