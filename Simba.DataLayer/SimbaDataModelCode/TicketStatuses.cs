using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Simba.DataLayer.Database
{

    public partial class TicketStatuses
    {
        public TicketStatuses(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
