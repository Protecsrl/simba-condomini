﻿using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Simba.DataLayer.simba_condomini
{

    public partial class TicketDocument
    {
        public TicketDocument(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
