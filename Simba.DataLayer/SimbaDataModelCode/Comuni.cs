﻿using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Simba.DataLayer.Database
{

    public partial class Comuni
    {
        public Comuni(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
