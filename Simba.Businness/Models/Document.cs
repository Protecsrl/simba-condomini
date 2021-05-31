using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simba.Businness.Models
{
    public class Document
    {
        public Document(int oid, string path, string type)
        {
            this.Oid = oid;
            this.Path = path;
            this.Type = type;
        }
        public int Oid
        {
            get;
            private set;
        }

        public string Path
        {
            get;
            private set;
        }
        public string Type
        {
            get;
            private set;
        }
    }
}