using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class SMSType {
        public static readonly SMSType ALTA = new SMSType("N","ALTA",true);
        public static readonly SMSType STANDARD = new SMSType("LL", "STANDARD", false);

        public static readonly SMSType[] ALL_MESSAGE_TYPES = { ALTA, STANDARD };

        private string code;
        private string description;
        private bool has_custom_tpoa;
        private SMSType(string code, string description, bool has_custom_tpoa) {
            this.code = code;
            this.description = description;
            this.has_custom_tpoa = has_custom_tpoa;
        }

        public override string ToString() {
            return this.description;
        }
        public string Code {
            get {
                return this.code;
            }
        }
        public bool CustomSender {
            get { return this.has_custom_tpoa; }
        }
        public static SMSType fromCode(string code) {
            foreach (SMSType smstype in ALL_MESSAGE_TYPES) {
                if (smstype.code.Equals(code)) {
                    return smstype;
                }
            }
            return null;
        }

    }
}
