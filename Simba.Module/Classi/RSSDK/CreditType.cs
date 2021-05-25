using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class CreditType {
        public static readonly CreditType ALTA= new CreditType("N", "ALTA");
        public static readonly CreditType STANDARD = new CreditType("LL", "STANDARD");
        public static readonly CreditType NUMBER_LOOKUP = new CreditType("NL", "Number Lookup");
        public static readonly CreditType OTHER = new CreditType("EE", "Foreign");
        public static readonly CreditType[] ALL_CREDIT_TYPES = { ALTA,STANDARD, OTHER };

        private string code;
        private string description;
        public CreditType(string code, string description) {
            this.code = code;
            this.description = description;
        }

        public override string ToString() {
            return this.description;
        }
        public static CreditType fromCode(string code) {
            foreach (CreditType ct in ALL_CREDIT_TYPES) {
                if (code.Equals(ct.code)) {
                    return ct;
                }
            }
            return null;
        }

        public string Code {
            get { return this.code; }
        }
        public string Description {
            get { return this.description; }
        }

    }
}
