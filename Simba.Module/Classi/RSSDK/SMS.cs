using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class SMS {
        private string order_id;					// sms order ID
        private SMSType sms_type;					// SMS type
        private DateTime? scheduled_delivery;		// scheduled send date (null if immediate)
        private string message;					    // SMS text
        private SMSSender sms_sender;				// SMS sender
        private List<SMSRecipient> sms_recipients;	// The list of recipients

        public SMS() {
            this.sms_recipients = new List<SMSRecipient>();
            this.sms_type = SMSType.ALTA;
        }

        public string Message {
            get {
                return this.message;
            }
            set {
                if ((value.Length == 0) || (Str.countGSMChars(value) > 1000))
                    throw new InvalidMessageContentException("invalid message content length (" + value.Length + ")");
                this.message = value;
            }
        }

        public int Length {
            get {
                return Str.countGSMChars(this.message);
            }
        }

        public int CountSMS {
            get {
                return Length <= 160 ? 1 : ((Length - 1) / 153) + 1;
            }
        }

        /**
         * Sets the message ID, assigned by the server when null
         */
        public string OrderId {
            get { return this.order_id; }
            set { this.order_id = value; }
        }

        public DateTime ScheduledDelivery {
            get {
                if (this.scheduled_delivery.HasValue)
                    return this.scheduled_delivery.Value;
                else
                    return DateTime.Now;
            }
            set {
                if (value.Ticks <= DateTime.Now.Ticks)
                    this.scheduled_delivery = null;
                else
                    this.scheduled_delivery = value;
            }
        }
        public void setImmediate() {
            this.scheduled_delivery = null;
        }
        public bool Immediate {
            get {
                return !this.scheduled_delivery.HasValue;
            }
        }

        public SMSType TypeOfSMS {
            get { return this.sms_type; }
            set { this.sms_type = value; }
        }

        public string SMSSender {
            get {
                return this.sms_sender.Number;
            }
            set {
                this.sms_sender = new SMSSender(value);
            }
        }

        public IList<SMSRecipient> Recipients {
            get {
                return this.sms_recipients.AsReadOnly();
            }
        }

        /**
         * The function checks the <code>SMSRrecipient</tt> and then adds it to the sms.
         * 
         * @param recipient the <code>SMSRrecipient</tt> of the sms
         * @throws InvalidRecipientException
         */
        public void addSMSRecipient(SMSRecipient recipient) {
            if (!recipient.isValid())
                throw new InvalidRecipientException("Invalid SMS recipient: "+recipient);
            this.sms_recipients.Add(recipient);
        }
        /**
         * The function adds a sms recipient to the sms.
         * 
         * @param str_recipient the recipient phone number.
         * @throws InvalidRecipientException
         */
        public void addSMSRecipient(string str_recipient) {
            this.addSMSRecipient(new SMSRecipient(str_recipient));
        }

        /**
         * Checks that sender, recipient, message and smsType are all OK.
         * 
         */
        public void validate() {
            if (sms_recipients.Count == 0)
                throw new InvalidRecipientException("Invalid SMS recipient: no recipients specified!");
            if (sms_type == null)
                throw new SMSCException("Invalid NULL message type");
            if (sms_type.CustomSender && sms_sender == null)
                throw new InvalidSenderException("Invalid NULL sender");
            if (message == null) {
                throw new InvalidMessageContentException("message is empty");
            }
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder()
                .Append("(id_message=").Append(this.order_id)
                .Append(",smsType=").Append(this.sms_type)
                .Append(",send_date=").Append(this.scheduled_delivery)
                .Append(",message=").Append(this.message)
                .Append(",smsSender=").Append(this.sms_sender)
                .Append(",smsRecipients:");
            int i = 0;
            foreach (SMSRecipient recipient in sms_recipients) {
                if (i++ > 0) sb.Append(',');
                sb.Append(recipient);
            }
            sb.Append(')');
            return sb.ToString();
        }

    }

}
