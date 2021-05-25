using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class SMSRecipient : PhoneNumber {
        private bool international;

        /**
         * The constructor add the default international prefix clean the phone
         * number string from non numeric digits.
         * @throws InvalidRecipientException 
         */
        public SMSRecipient(string number) : base(number) {
            cleanIntl();
        }

        private void cleanIntl() {
            if (!string.IsNullOrEmpty(this.number)) {
                if (this.number[0] == '+') {
                    this.number = this.number.Substring(1);
                    this.international = true;
                } else {
                    if (this.number.StartsWith("00")) {
                        this.number = this.number.Substring(2);
                        this.international = true;
                    } else {
                        this.international = false;
                    }
                }
                this.number = Str.stripNonNumeric(this.number);
            }
        }

        /**
         * Check that the number is a number :)
         * 
         * @return true or false
         */
        public override bool isValid() {
            if (string.IsNullOrEmpty(this.number))
                return false;
            return this.number.Length > 2;
        }

        public bool International {
            get { return this.international; }
        }

        /**
         * The function get the Nation of recipient.
         * 
         * @return an instance of <code>Nation</code> relative to the recipient,
         * <code>NO_NATION</code> if the recipient hasn't an international prefix,
         * <code>UNKNOWN_NATION</code> if the prefix is unknown, <code>null</code>
         * if the object <code>Recipient</code> isn't a valid recipient.
         */
        public Nation Nation {
            get {
                if (!isValid())
                    return Nations.UNKNOWN_NATION;
                if (!International)
                    return Nations.NO_NATION;
                return Nations.I.getPhoneNumberNation(this.number);
            }
        }

        public string getNumber() {
            return International ? "+" + this.number : this.number;
        }

        public override string ToString() {
            return getNumber();
        }
    }
}
