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

    public partial class Documents : XPLiteObject
    {
        int fOid;
        [Key(true)]
        public int Oid
        {
            get { return fOid; }
            set { SetPropertyValue<int>(nameof(Oid), ref fOid, value); }
        }
        string fpath;
        [Size(50)]
        public string path
        {
            get { return fpath; }
            set { SetPropertyValue<string>(nameof(path), ref fpath, value); }
        }
        string ftype;
        [Size(50)]
        public string type
        {
            get { return ftype; }
            set { SetPropertyValue<string>(nameof(type), ref ftype, value); }
        }
        [Association(@"TicketDocumentReferencesDocuments")]
        public XPCollection<TicketDocument> TicketDocuments { get { return GetCollection<TicketDocument>(nameof(TicketDocuments)); } }
    }

}
