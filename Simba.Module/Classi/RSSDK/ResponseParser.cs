using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class ResponseParser {
        private static readonly char SEPARATOR = '|';
        private static readonly char NEWLINE = ';';
        private char[] response;
        private int cursor;
        private bool isok;
        private int errcode;
        private string errmsg;

        public ResponseParser(string str_response) {
            this.response = str_response.ToCharArray();
            this.cursor = 0;
            if (response.Length >= 2) {
                string stat = NextString;
                if ("OK".Equals(stat)) {
                    isok = true;
                }
                if ("KO".Equals(stat)) {
                    isok = false;
                    errcode = NextInt;
                    errmsg = NextString;
                }
            }
        }

        public bool GoNextLine() {
            while (response[cursor++] != NEWLINE) {
                if (cursor >= response.Length)
                    return false;
            }
            return response.Length != cursor;
        }

        public string NextString {
            get {
                StringBuilder sb = new StringBuilder();
                while (response[cursor] != SEPARATOR && response[cursor] != NEWLINE) {
                    sb.Append(response[cursor]);
                    cursor++;
                    if (cursor >= response.Length)
                        break;
                }
                if (cursor < response.Length && response[cursor] != NEWLINE)
                    cursor++;
                string res = Uri.UnescapeDataString(sb.ToString());
                return res;
            }
        }
        public int NextInt {
            get {
                string str_i = NextString;
                return int.Parse(str_i);
            }
        }
        public long NextLong {
            get {
                string str_i = NextString;
                return long.Parse(str_i);
            }
        }
        public bool NextBool {
            get {
                string str_b = NextString;
                return bool.Parse(str_b);
            }
        }
        public DateTime NextDate {
            get {
                DateTime? dt = DDate.Parse(NextString);
                if (dt.HasValue)
                    return dt.Value;
                else
                    return DateTime.MinValue;
            }
        }
        public DateTime? NextOptionalDate {
            get {
                return DDate.Parse(NextString);
            }
        }
        public SMSRecipient NextSMSRecipient {
            get {
                return new SMSRecipient(NextString);
            }
        }
        public SMSType NextSMSType {
            get {
                string str_sms_type = NextString;
                return SMSType.fromCode(str_sms_type);
            }
        }
        public SMSSender NextSMSSender {
            get {
                return new SMSSender(NextString);
            }
        }
        public CreditType NextCreditType {
            get {
                string str_credit_type = NextString;
                return CreditType.fromCode(str_credit_type);
            }
        }
        public MessageStatus.SMSStatus NextMessageStatus_Status {
            get {
                return MessageStatus.GetStatus(NextString);
            }
        }
        public Nation NextNation {
            get {
                string str_iso3166 = NextString;
                if (string.IsNullOrEmpty(str_iso3166))
                    return Nations.NO_NATION;
                return Nations.I[str_iso3166];
            }
        }

        public bool Ok {
            get { return this.isok; }
        }
        public int ErrorCode {
            get {
                return this.errcode;
            }
        }
        public string ErrorMessage {
            get {
                return this.errmsg;
            }
        }
    }

}
