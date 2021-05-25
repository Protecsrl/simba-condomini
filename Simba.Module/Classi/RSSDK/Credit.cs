using System;
using System.Collections.Generic;
using System.Text;

namespace RS{
    public class Credit {
        private CreditType creditType;		// Type of credit.
        private Nation nation;				// Nation relative to the credit.
        private int availability;			// Credit counter.

        public Credit(CreditType credit_type, Nation nation, int availability) {
            this.creditType = credit_type;
            this.nation = nation;
            this.availability = availability;
        }

        public CreditType CreditType {
            get { return creditType; }
        }
        public Nation Nation {
            get { return this.nation; }
        }
        public int Count {
            get { return this.availability; }
        }

        public override string ToString() {
            StringBuilder toPrint = new StringBuilder()
                .Append("(creditType:").Append(this.creditType).Append(",");
            if (this.nation != null)
                toPrint.Append("nation=").Append(this.nation).Append(",");
            toPrint.Append("count='").Append(this.availability).Append(")");
            return toPrint.ToString();
        }

    }
}
