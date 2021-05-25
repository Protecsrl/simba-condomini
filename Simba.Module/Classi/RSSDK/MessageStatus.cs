using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class MessageStatus {
        private SMSRecipient rcpt_number;		// SMS recipient
        private MessageStatus.SMSStatus status;	// Message status
        private DateTime deliveryDate;			// Delivery date

        public MessageStatus(SMSRecipient rcpt_number, MessageStatus.SMSStatus status, DateTime deliveryDate) {
            this.rcpt_number = rcpt_number;
            this.status = status;
            this.deliveryDate = deliveryDate;
        }

        public SMSRecipient Recipient {
            get { return rcpt_number; }
        }
        public SMSStatus Status {
            get { return status; }
        }
        public DateTime DeliveryDate {
            get { return deliveryDate; }
        }

        public override string ToString() {
            return new StringBuilder()
                .Append("(rcpt_number=").Append(this.rcpt_number)
                .Append(",status=").Append(this.status)
                .Append(",deliveryDate=").Append(this.deliveryDate == null ? "*immediate*" : this.deliveryDate.ToString())
                .Append(')').ToString();
        }

        public static SMSStatus GetStatus(string status_code) {
            try {
                return (SMSStatus)Enum.Parse(typeof(SMSStatus), status_code, true);
            } catch (Exception) {
                return SMSStatus.UNKNOWN;
            }
        }

        public bool IsError {
            get {
                switch (this.status) {
                    case SMSStatus.ERROR:
                    case SMSStatus.TIMEOUT:
                    case SMSStatus.TOOM4NUM:
                    case SMSStatus.TOOM4USER:
                    case SMSStatus.UNKNPFX:
                    case SMSStatus.UNKNRCPT: return true;
                }
                return false;
            }
        }

        public enum SMSStatus {
            SCHEDULED,	// postponed, not jet arrived
            SENT,		// sent, wait for delivery notification (depending on message type)
            DLVRD,		// the sms has been correctly delivered to the mobile phone
            ERROR,		// error sending sms
            TIMEOUT,	// cannot deliver sms to the mobile in 48 hours
            TOOM4NUM,	// too many messages sent to this number (spam warning)
            TOOM4USER,	// too many messages sent by this user
            UNKNPFX,	// unknown/unparsable mobile phone prefix
            UNKNRCPT,	// unknown recipient
            WAIT4DLVR,	// message sent, waiting for delivery notification
            WAITING,	// not yet sent (still active)
            UNKNOWN		// received an unknown status code from server (should never happen!)
        }
    }
}
