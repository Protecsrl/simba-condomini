using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class LookupResult {
        private bool valid = false;
        private string order_id;
        private SMSRecipient recipient;
        private Nation nation;
        private string nationName;
        private string mobileop;

        public LookupResult(string numLookupID) {
            this.valid = false;
        }
        public LookupResult(string order_id, SMSRecipient recipient, Nation nation, string nationName, string mobileop) {
            this.valid = true;
            this.order_id = order_id;
            this.recipient = recipient;
            this.nation = nation;
            this.nationName = nationName;
            this.mobileop = mobileop;
        }

        /**
         * The method verify if the <code>Nation</code> of 
         * the number lookup recipient is one to which we can send.
         * 
         * @return <code>true</code> in case of success, <code>false</code> otherwise
         */
        public bool CanSendTo() {
            return Valid && this.nation != Nations.UNKNOWN_NATION;
        }

        /**
         * The result of the number lookup call.
         * 
         * @return <code>true</code> in case of success, <code>false</code> otherwise
         */
        public bool Valid {
            get { return valid; }
        }

        public string OrderId {
            get { return order_id; }
        }

        public SMSRecipient Recipient {
            get { return recipient; }
        }

        public Nation Nation {
            get { return nation; }
        }

        public string NationName {
            get { return nationName; }
        }

        public string MobileCompany {
            get { return mobileop; }
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder()
                .Append("(valid=").Append(this.valid)
                .Append(",order_id=").Append(this.order_id);
            if (this.valid) {
                sb.Append(",recipient=").Append(this.recipient)
                    .Append(",nation=").Append(this.nation)
                    .Append(",nationName=").Append(this.nationName)
                    .Append(",mobileop=").Append(this.mobileop).Append(')');
            } else {
                sb.Append(')');
            }
            return sb.ToString();
        }

    }

}
