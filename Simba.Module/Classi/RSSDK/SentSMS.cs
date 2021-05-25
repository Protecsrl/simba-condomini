using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class SentSMS {
        private string order_id;
        private DateTime create_time;
        private SMSType sms_type;
        private SMSSender sender;
        private int recipients_count;
        private DateTime? scheduled_send;

        public SentSMS(string order_id, DateTime create_time, SMSType sms_type, SMSSender sender, int recipients_count, DateTime? scheduled_send) {
            this.order_id = order_id;
            this.create_time = create_time;
            this.sms_type = sms_type;
            this.sender = sender;
            this.recipients_count = recipients_count;
            this.scheduled_send = scheduled_send;
        }

        public string OrderId {
            get { return order_id; }
        }
        public DateTime CreateTime {
            get { return create_time; }
        }
        public SMSType TypeOfSMS {
            get { return sms_type; }
        }
        public SMSSender Sender {
            get { return sender; }
        }
        public int RecipientsCount {
            get { return recipients_count; }
        }
        public DateTime? ScheduledSend {
            get { return scheduled_send; }
        }
        public bool Scheduled {
            get { return this.scheduled_send.HasValue; }
        }
    }

}
