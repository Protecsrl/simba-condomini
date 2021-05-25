using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//namespace CAMS.Module.BaseObjects
//{
//    class NamedBaseObject
//    {
//    }
//}
using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;

namespace CAMS.Module.BaseObjects
{
	public abstract class NamedBaseObject : BaseObject {
		private string name;
		public NamedBaseObject(Session session) : base(session) { }
        public NamedBaseObject(Session session, string name)
            : this(session) {
            this.name = name;
        }
		public string Name {
			get { return name; }
			set { SetPropertyValue("Name", ref name, value); }
		}
	}
}
