using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class SendResult {
        private int sent_smss;			// The number of reached recipients
        private string order_id;		// sms order ID

        public SendResult(string order_id, int sent_smss) {
            this.order_id = order_id;
            this.sent_smss = sent_smss;
        }

        public int CountSentSMS {
            get { return sent_smss; }
        }

        public string OrderId {
            get { return order_id; }
        }

        public override string ToString() {
            return new StringBuilder()
                .Append("(order_id=").Append(this.order_id)
                .Append(",sent_smss=").Append(this.sent_smss)
                .Append(')').ToString();
        }

    }
}
