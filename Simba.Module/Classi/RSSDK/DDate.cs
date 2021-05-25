using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace RS {
    public class DDate {
        public static readonly string STANDARD_FORMAT = "yyyyMMddHHmmss";
        public static readonly string STANDARD_TIME_FORMAT = "HH:mm";

        public static string Format(DateTime date) {
            if (date == null)
                return string.Empty;
            return date.ToString(STANDARD_FORMAT);
        }

        /**
         * Converts a <code>String</code> to a <code>Date</code> object,
         * if the <code>String</code> is empty return <code>null</code>.
         * @param date the <code>String</code> to convert
         * @return the instanced <code>Date</code> from the passed <code>String</code>,
         * <code>null</code> otherwise.
         */
        public static DateTime? Parse(string date) {
            if (string.IsNullOrEmpty(date))
                return null;
            return DateTime.ParseExact(date, STANDARD_FORMAT, CultureInfo.CurrentCulture);
        }

    }
}
