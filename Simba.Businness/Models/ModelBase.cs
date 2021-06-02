using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simba.Businness.Models
{
    public abstract class ModelBase<TOrig, TDest> where TOrig : ModelBase<TOrig, TDest> where TDest : XPLiteObject
    {
        public abstract TDest ToXpoModel(TOrig obj);
    }
}