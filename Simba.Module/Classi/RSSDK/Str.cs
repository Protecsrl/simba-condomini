using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RS {
    public class Str {

        private static Regex nonnumeric = new Regex("[^0-9]");
        public static string stripNonNumeric(string toclean) {
            if (string.IsNullOrEmpty(toclean))
                return toclean;
            return nonnumeric.Replace(toclean, string.Empty);
        }

        /**<p>
         * The message origin, also known as TPOA (Transmission Path Originating Address) 
         * is the SMS header field that contains the message sender's number.
         * ValueSMS can alter this field to include reply path information 
         * (the user's own phone number) or other branding information, 
         * such as the company name.
         * </p>
         * <p>
         * The TPOA field is limited by GSM standards to:<br>
         * - maximum 16 digits if the origin is numeric (e.g. a phone number), or<br>
         * - maximum 11 alphabet characters and digits if the origin is alphanumeric (e.g. a company name).
         * </p>
         * 
         */
        private static Regex zerozerointernational = new Regex("00[0-9]{7,16}");
        private static Regex plusinternational = new Regex("\\+[0-9]{7,16}");
        public static bool isValidTPOA(string tpoa) {
            return zerozerointernational.IsMatch(tpoa)
                || plusinternational.IsMatch(tpoa)
                || tpoa.Length < 12;
        }

        public static string nullify(string str)
        {
            if (str == null) return null;
            if (str.Trim().Equals(string.Empty)) return null;
            return str;
        }

        public static int countGSMChars(string msg) {
            char[] msgc = msg.ToCharArray();
            int len = 0;
            foreach (char c in msgc) {
                switch (c) {
                    case '|':
                    case '^':
                    case '€':
                    case '}':
                    case '{':
                    case '[':
                    case '~':
                    case ']':
                    case '\\': len++; break;
                }
                len++;
            }
            return len;
        }

        public static String join(char separator, string[] strings) {
               StringBuilder sb = new StringBuilder();
               foreach (string s in strings)
               {
                       if (sb.Length > 0)
                               sb.Append(separator);
                       sb.Append(s);
               }
               return sb.ToString();
       }

    }

}
