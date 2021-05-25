using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace RS {
    public class Nation {
        private string iso3166;	// Country code in ISO 3166 standard.
        private string prefix;		// International phone prefix.

        public Nation(string iso3166, string prefix) {
            this.iso3166 = iso3166.ToUpper();
            this.prefix = Str.stripNonNumeric(prefix);
        }

        public string Iso3166 {
            get { return iso3166; }
        }
        public string Prefix {
            get { return prefix; }
        }

        public override bool Equals(Object obj) {
            if (obj is Nation) {
                Nation tmp_nation = (Nation)obj;
                return tmp_nation.Iso3166.Equals(this.Iso3166);
            }
            if (obj is string) {
                string iso3166 = (string)obj;
                return iso3166.Equals(this.iso3166, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override string ToString() {
            if (Nations.NO_NATION.Equals(this.iso3166))
                return "no nation";
            if (Nations.UNKNOWN_NATION.Equals(this.iso3166))
                return "unknown nation";
            RegionInfo ri = new RegionInfo(this.iso3166.ToUpper());
            return ri.DisplayName;
        }

        public static bool operator ==(Nation n1, Nation n2) {
            return n1.Equals(n2);
        }
        public static bool operator !=(Nation n1, Nation n2) {
            return !(n1.Equals(n2));
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

    }

}
