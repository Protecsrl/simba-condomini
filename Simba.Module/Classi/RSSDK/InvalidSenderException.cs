using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class InvalidSenderException : Exception {
        public InvalidSenderException(string message) : base(message) {
        }
    }
}
