using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class HTTPException : Exception {
        public HTTPException(string message)
            : base(message) {
        }
    }
}
