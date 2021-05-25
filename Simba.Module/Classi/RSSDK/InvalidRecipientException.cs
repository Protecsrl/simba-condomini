using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class InvalidRecipientException : Exception {
        public InvalidRecipientException(string message) : base(message) {
        }
    }
}
