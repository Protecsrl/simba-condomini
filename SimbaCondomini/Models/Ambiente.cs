using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimbaCondomini.Models
{
    public class Ambiente
    {
        public Ambiente(int oid, string name, string description, int building, bool valid)
        {
            this.Oid = oid;
            this.Name = name;
            this.Description = description;
            this.Building = building;
            this.Valid = valid;
        }

        public int Oid
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            private set;
        }

        public int Building
        {
            get;
            private set;
        }

        public bool Valid
        {
            get;
            private set;
        }
    }
}