using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class SMSCConnectionException : Exception {
        public SMSCConnectionException(string message) : base(message) {
        }
    }
}
