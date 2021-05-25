using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class SMSCException : Exception {
        public SMSCException(string message) : base(message) {
        }
    }
}
