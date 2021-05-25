using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class SMSSender : PhoneNumber {

        public SMSSender(String tpoa) : base(tpoa) {
        }

        public override bool isValid() {
            return this.Number != null && Str.isValidTPOA(this.Number);
        }

        public override string ToString() {
            return new StringBuilder().Append("(tpoa=").Append(this.number).Append(')').ToString();
        }

    }
}
