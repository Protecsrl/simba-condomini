using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;

namespace Simba.DataLayer.simba_condomini
{
    [DataContractAttribute]
    public partial class Condominium
    {
        public Condominium(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
