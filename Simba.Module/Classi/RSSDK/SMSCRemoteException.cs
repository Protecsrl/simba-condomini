using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class SMSCRemoteException : Exception {
        private int code;
        public SMSCRemoteException(int code, string message) : base(message) {
            this.code = code;
        }
        public int ErrorCode {
            get {
                return this.code;
            }
        }
    }
}
