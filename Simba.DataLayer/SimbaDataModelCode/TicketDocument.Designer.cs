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

    public partial class TicketDocument : XPLiteObject
    {
        DateTime fDate;
        public DateTime Date
        {
            get { return fDate; }
            set { SetPropertyValue<DateTime>(nameof(Date), ref fDate, value); }
        }
        int fUserId;
        public int UserId
        {
            get { return fUserId; }
            set { SetPropertyValue<int>(nameof(UserId), ref fUserId, value); }
        }
        public struct CompoundKey1Struct
        {
            [Persistent("IdTicket")]
            public int IdTicket { get; set; }
            [Persistent("IdDocument")]
            public int IdDocument { get; set; }
        }
        [Key, Persistent]
        public CompoundKey1Struct CompoundKey1;
    }

}
