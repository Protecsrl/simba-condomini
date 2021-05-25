using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class InvalidMessageContentException : Exception {
        public InvalidMessageContentException(string message) : base(message) {
        }
    }
}
