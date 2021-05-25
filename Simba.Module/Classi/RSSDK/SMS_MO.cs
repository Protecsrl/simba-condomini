using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class SMS_MO {
        private long id_message;				// SMS sending date
        private DateTime send_date;				// SMS sending date
        private string message;				// SMS text message
        private string keyword;				// SMS's text first word, if used as a keyword, otherwise null
        private SMSSender sms_sender;			// SMS sender
        private SMSRecipient sms_recipient;	// SMS recipient

        public SMS_MO(long id_message, SMSRecipient sms_recipient, SMSSender sms_sender, string message, DateTime send_date, string keyword) {
            this.id_message = id_message;
            this.sms_recipient = sms_recipient;
            this.sms_sender = sms_sender;
            this.message = message;
            this.send_date = send_date;
            if (string.IsNullOrEmpty(keyword))
                this.keyword = null;
            else
                this.keyword = keyword;
        }

        public DateTime SendDate {
            get { return send_date; }
        }
        public SMSSender Sender {
            get { return sms_sender; }
        }
        public SMSRecipient Recipient {
            get { return sms_recipient; }
        }
        public string Message {
            get { return this.message; }
        }
        public long IdMessage {
            get { return id_message; }
        }
        public string Keyword {
            get { return keyword; }
        }

        public override string ToString() {
            return new StringBuilder()
                .Append("(send_date=").Append(this.send_date)
                .Append(",message=").Append(message)
                .Append(",sms_sender=").Append(this.sms_sender)
                .Append(",sms_recipient=").Append(this.sms_recipient).Append(')').ToString();
        }

    }

}
