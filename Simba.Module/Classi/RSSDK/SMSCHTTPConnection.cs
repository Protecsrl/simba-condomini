using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS {
    public class SMSCHTTPConnection : SMSCConnection {
        private string username;
        private string password;
        private string hostname;
        private int port;
        private bool connected;
        private string proxy;
        private int proxyport;

        public SMSCHTTPConnection(string username, string password, string proxy, int proxyport)
        {
            this.username = username;
            this.password = password;
            this.hostname = Config.HOSTNAME;
            this.port = Config.DEFAULT_PORT;
            this.proxy = proxy;
            this.proxyport = proxyport;
            login();
        }

        public SMSCHTTPConnection(string username, string password) {
            this.username = username;
            this.password = password;
            this.hostname = Config.HOSTNAME;
            this.port = Config.DEFAULT_PORT;
            this.proxy = Config.PROXY;
            this.proxyport = Config.PROXY_PORT;
            login();
        }
        public SMSCHTTPConnection() {
            this.username = Config.USERNAME;
            this.password = Config.PASSWORD;
            this.hostname = Config.HOSTNAME;
            this.port = Config.DEFAULT_PORT;
            this.proxy = Config.PROXY;
            this.proxyport = Config.PROXY_PORT;
            login();
        }
        public void login() {
            if (!hostname.StartsWith("http://"))
                hostname = "http://" + hostname;
            if (hostname.EndsWith("/")) {
                hostname = hostname.Substring(0, hostname.Length - 1);
            }
            if ((port != 80) && (port != 0)) {
                hostname = hostname + ":" + port + "/";
            }
            this.connected = false;
            getCredits(); // just to check username+password correctness ...
            this.connected = true;
        }
        public void logout() {
            this.connected = false;
        }

        public List<Credit> getCredits() {
            ResponseParser rp;
            Credit credit;
            List<Credit> credits;
            CreditType credit_type;
            rp = makeStdHTTPRequest(CREDITS_REQ, getBaseHTTPParams());
            credits = new List<Credit>();
            while (rp.GoNextLine())
            {
                credit_type = rp.NextCreditType;
                credit = new Credit(credit_type, rp.NextNation, rp.NextInt);
                credits.Add(credit);
            }
            return credits;
        }
        public SendResult sendSMS(SMS sms) {
            Dictionary<string, string> sparams = getBaseHTTPParams();
            sparams.Add("message", sms.Message);
            sparams.Add("message_type", sms.TypeOfSMS.Code);
            if (sms.TypeOfSMS.CustomSender) {
            	sparams.Add("sender", sms.SMSSender);
            }
            if (!string.IsNullOrEmpty(sms.OrderId))
                sparams.Add("order_id", sms.OrderId);
            StringBuilder recipient_list = new StringBuilder();
            bool isfirst = true;
            foreach (SMSRecipient recipient in sms.Recipients) {
                if (isfirst) {
                    isfirst = false;
                } else {
                    recipient_list.Append(',');
                }
                recipient_list.Append(recipient.getNumber());
            }
            if (recipient_list.Length > 0)
            {
                sparams.Add("recipient", recipient_list.ToString());
                if (!sms.Immediate)
                {
                    sparams.Add("scheduled_delivery_time", DDate.Format(sms.ScheduledDelivery));
                }
                ResponseParser rp = makeStdHTTPRequest(SEND_SMS_REQ, sparams);
                return new SendResult(rp.NextString, rp.NextInt);
            }
            return null;
        }
        public Boolean removeScheduledSend(string order_id)
        {
            Dictionary<string, string> sparams = getBaseHTTPParams();
		    sparams.Add("order_id", order_id);
		    makeStdHTTPRequest(REMOVE_DELAYED_REQ,sparams);
		    return true;
	    }
        public List<MessageStatus> getMessageStatus(string order_id) {
            Dictionary<string, string> sparams = getBaseHTTPParams();
            sparams.Add("order_id", order_id);
            ResponseParser rp = makeStdHTTPRequest(MSG_STATUS_REQ, sparams);
            List<MessageStatus> statuses = new List<MessageStatus>();
            while (rp.GoNextLine())
            {
                statuses.Add(new MessageStatus(rp.NextSMSRecipient, rp.NextMessageStatus_Status, rp.NextDate));
            }
            return statuses;
        }
        public List<SentSMS> getSMSHistory(DateTime from_date, DateTime to_date) {
            Dictionary<string, string> sparams = getBaseHTTPParams();
            sparams.Add("from", DDate.Format(from_date));
            sparams.Add("to", DDate.Format(to_date));
            ResponseParser rp = makeStdHTTPRequest(SMS_HISTORY_REQ, sparams);
            List<SentSMS> sent_smss = new List<SentSMS>();
            while (rp.GoNextLine()) {
                sent_smss.Add(new SentSMS(rp.NextString, rp.NextDate, rp.NextSMSType, rp.NextSMSSender, rp.NextInt, rp.NextOptionalDate));
            }
            return sent_smss;
        }

        public LookupResult lookup(SMSRecipient recipient) {
            Dictionary<string, string> sparams = getBaseHTTPParams();
            sparams.Add("num", recipient.getNumber());
            ResponseParser rp = makeStdHTTPRequest(NUMBER_LOOKUP_REQ, sparams);
            bool valid = "valid".Equals(rp.NextString);
            LookupResult lr;
            if (valid) {
                lr = new LookupResult(rp.NextString, rp.NextSMSRecipient, rp.NextNation, rp.NextString, rp.NextString);
            } else {
                lr = new LookupResult(rp.NextString);
            }
            return lr;
        }

        public List<SMS_MO> getNewSMS_MOs() {
            return getAllSMS_MO(makeStdHTTPRequest(NEW_SMS_MO_REQ, getBaseHTTPParams()));
        }
        public List<SMS_MO> getSMS_MOHistory(DateTime from_date, DateTime to_date) {
            Dictionary<string, string> sparams = getBaseHTTPParams();
            sparams.Add("date_from", DDate.Format(from_date));
            sparams.Add("date_to", DDate.Format(to_date));
            return getAllSMS_MO(makeStdHTTPRequest(SMS_MO_HIST_REQ, sparams));
        }
        public List<SMS_MO> getSMS_MOById(long message_id) {
            Dictionary<string, string> sparams = getBaseHTTPParams();
            sparams.Add("id", Convert.ToString(message_id));
            return getAllSMS_MO(makeStdHTTPRequest(SMS_MO_BYID_REQ, sparams));
        }

        public Subaccount createSubaccount(Subaccount subaccount) {
    		Dictionary<string,string> sparams = getBaseHTTPParams();
    		sparams.Add("op","CREATE_SUBACCOUNT");
	    	sparams.Add("credit_mode",subaccount.Credit_mode.ToString());
		    if (!string.IsNullOrEmpty(subaccount.Company_name))
			    sparams.Add("company_name",subaccount.Company_name);
    		sparams.Add("fiscal_code",subaccount.Fiscal_code);
	    	sparams.Add("vat_number",subaccount.Vat_number);
		    sparams.Add("name",subaccount.Name);
    		sparams.Add("surname",subaccount.Surname);
	    	sparams.Add("email",subaccount.Email);
		    sparams.Add("address",subaccount.Address);
    		sparams.Add("city",subaccount.City);
	    	sparams.Add("province",subaccount.Province);
		    sparams.Add("zip",subaccount.Zip);
    		sparams.Add("mobile",subaccount.Mobile);
	    	if (!string.IsNullOrEmpty(subaccount.Login))
		    	sparams.Add("sub_login",subaccount.Login);
	    	if (!string.IsNullOrEmpty(subaccount.Password))
		    	sparams.Add("sub_password",subaccount.Password);
            ResponseParser rp = makeStdHTTPRequest(SUBACCOUNTS_REQ, sparams);
            subaccount.Login = rp.NextString;
		    subaccount.Password = rp.NextString;
		    return subaccount;
        }
        public List<Subaccount> getSubaccounts() {
    		Dictionary<string,string> sparams = getBaseHTTPParams();
	    	sparams.Add("op","LIST_SUBACCOUNTS");
		    ResponseParser rp = makeStdHTTPRequest(SUBACCOUNTS_REQ,sparams);
    		List<Subaccount> subaccounts = new List<Subaccount>();
	    	Subaccount subaccount;
		    while (rp.GoNextLine()) {
			    subaccount = new Subaccount();
    			subaccount.Login = rp.NextString;
	    		subaccount.Active = rp.NextBool;
		    	subaccount.Credit_mode = rp.NextInt;
                subaccount.SubaccountType = rp.NextString;
                subaccount.Company_name = rp.NextString;
                subaccount.Fiscal_code = rp.NextString;
                subaccount.Vat_number = rp.NextString;
                subaccount.Name = rp.NextString;
                subaccount.Surname = rp.NextString;
                subaccount.Email = rp.NextString;
                subaccount.Address = rp.NextString;
                subaccount.City = rp.NextString;
                subaccount.Province = rp.NextString;
                subaccount.Zip = rp.NextString;
                subaccount.Mobile = rp.NextString;
			    subaccounts.Add(subaccount);
		    }
		    return subaccounts;
        }
	    public Subaccount lockSubaccount(Subaccount subaccount) {
		    Dictionary<string,string> sparams = getBaseHTTPParams();
    		sparams.Add("op","LOCK_SUBACCOUNT");
	    	sparams.Add("subaccount",subaccount.Login);
		    makeStdHTTPRequest(SUBACCOUNTS_REQ,sparams);	
    		subaccount.Active = false;
	    	return subaccount;
        }
    	public Subaccount unlockSubaccount(Subaccount subaccount) {
	    	Dictionary<string,string> sparams = getBaseHTTPParams();
		    sparams.Add("op","UNLOCK_SUBACCOUNT");
    		sparams.Add("subaccount",subaccount.Login);
	    	makeStdHTTPRequest(SUBACCOUNTS_REQ,sparams);		
		    subaccount.Active = true;
    		return subaccount;
	    }
    	public void moveCredits(CreditMovement credit_movement) {
	    	Dictionary<string,string> sparams = getBaseHTTPParams();
		    sparams.Add("op","MOVE_CREDITS");
    		sparams.Add("subaccount",credit_movement.Subaccount_login);
	    	sparams.Add("super_to_sub",credit_movement.Super_to_sub.ToString());
		    sparams.Add("amount",credit_movement.Amount.ToString());
    		sparams.Add("message_type",credit_movement.Sms_type.Code);
	    	makeStdHTTPRequest(SUBACCOUNTS_REQ,sparams);		
	    }
	    public List<Credit> getSubaccountCredits(Subaccount subaccount) {
		Dictionary<string,string> sparams = getBaseHTTPParams();
		sparams.Add("op","GET_CREDITS");
		sparams.Add("subaccount",subaccount.Login);
		ResponseParser rp = makeStdHTTPRequest(SUBACCOUNTS_REQ,sparams);
		Credit credit;
		List<Credit> credits;
		CreditType credit_type;
		credits = new List<Credit>();
		while (rp.GoNextLine()) {
			credit_type = rp.NextCreditType;
			credit = new Credit(credit_type,rp.NextNation,rp.NextInt);
			credits.Add(credit);
		}
		return credits;
	}
	    public void createPurchase(CreditMovement credit_movement) {
		    Dictionary<string,string> sparams = getBaseHTTPParams();
		    sparams.Add("op","CREATE_PURCHASE");
		    sparams.Add("subaccount",credit_movement.Subaccount_login);
		    List<string> mt = new List<String>();
		    foreach (SMSType st in credit_movement.Sms_types)
			    mt.Add(st.Code);
		    sparams.Add("message_types", Str.join(';', mt.ToArray<string>()));

		    List<String> ppm = new List<String>();
		    foreach (double st in credit_movement.PricePerMessage)
			    ppm.Add(st.ToString());
		    sparams.Add("price_per_messages", Str.join(';', ppm.ToArray<string>()));
		
		    sparams.Add("price",credit_movement.Price.ToString());
		    makeStdHTTPRequest(SUBACCOUNTS_REQ,sparams);		
		
	    }
	    public void deletePurchase(CreditMovement credit_movement) {
		    Dictionary<string,string> sparams = getBaseHTTPParams();
		    sparams.Add("op","DELETE_PURCHASE");
		    sparams.Add("subaccount",credit_movement.Subaccount_login);
		    sparams.Add("id_purchase",credit_movement.Id_purchase.ToString());
		    makeStdHTTPRequest(SUBACCOUNTS_REQ,sparams);		
	    }
	    public List<CreditMovement> getPurchases(Subaccount subaccount) {
		    Dictionary<string,string> sparams = getBaseHTTPParams();
		    sparams.Add("op","GET_PURCHASES");
		    sparams.Add("subaccount",subaccount.Login);
		    ResponseParser rp = makeStdHTTPRequest(SUBACCOUNTS_REQ,sparams);
		    List<CreditMovement> credits = new List<CreditMovement>();
		    while (rp.GoNextLine()) {
			    CreditMovement cm = new CreditMovement();
			    cm.Subaccount_login = subaccount.Login;
			    cm.Super_to_sub = rp.NextBool;

			    cm.Amount = ((int)Double.Parse(rp.NextString));

			    cm.Recording_date = rp.NextDate;
			    cm.Id_purchase = rp.NextLong;
			
			    cm.Price = Double.Parse(rp.NextString);
			    cm.AvailableAmount = rp.NextInt;
			
			    string[] strTypes = rp.NextString.Split(';');
			    SMSType[] sms_types = new SMSType[strTypes.Length];
			    for (int i=0; i<strTypes.Length;i++)
				    sms_types[i] = SMSType.fromCode(strTypes[i]);
			    cm.Sms_types = sms_types;

			    String[] ppm = rp.NextString.Split(';');
			    double[] pricePerMessages = new double[ppm.Length];
			    for (int i=0; i<ppm.Length;i++)
				    pricePerMessages[i] = Double.Parse(ppm[i]);
			    cm.PricePerMessage = pricePerMessages;
			    credits.Add(cm);
		    }
		    return credits;
	    }
        
        public bool isConnected() {
            return this.connected;
        }
        private ResponseParser makeStdHTTPRequest(string request, Dictionary<string, string> sparams) {
            string http_response = null;
            try {
                http_response = HTTP.POST(this.hostname + request, sparams, this.proxy, this.proxyport);
            } catch (HTTPException httpe) {
                throw new SMSCConnectionException(httpe.Message);
            }
            ResponseParser rp = new ResponseParser(http_response);
            if (rp.Ok) {
                return rp;
            } else {
                throw new SMSCRemoteException(rp.ErrorCode, rp.ErrorMessage);
            }
        }
        private Dictionary<string, string> getBaseHTTPParams() {
            Dictionary<string, string> base_sparams = new Dictionary<string, string>();
            base_sparams.Add("login", this.username);
            base_sparams.Add("password", this.password);
            return base_sparams;
        }
        private List<SMS_MO> getAllSMS_MO(ResponseParser rp) {
            List<SMS_MO> new_smss = new List<SMS_MO>();
            while (rp.GoNextLine())
            {
                new_smss.Add(new SMS_MO(rp.NextLong, rp.NextSMSRecipient, rp.NextSMSSender, rp.NextString, rp.NextDate, rp.NextString));
            }
            return new_smss;
        }

        private const string CREDITS_REQ = "/Aruba/CREDITS";
        private const string SEND_SMS_REQ = "/Aruba/SENDSMS";
        private const string REMOVE_DELAYED_REQ = "/Aruba/REMOVE_DELAYED";
        private const string MSG_STATUS_REQ = "/Aruba/SMSSTATUS";
        private const string SMS_HISTORY_REQ = "/Aruba/SMSHISTORY";
        private const string NUMBER_LOOKUP_REQ = "/OENL/NUMBERLOOKUP";
        private const string NEW_SMS_MO_REQ = "/OESRs/SRNEWMESSAGES";
        private const string SMS_MO_HIST_REQ = "/OESRs/SRHISTORY";
        private const string SMS_MO_BYID_REQ = "/OESRs/SRHISTORYBYID";
        private const string SUBACCOUNTS_REQ = "/Aruba/SUBACCOUNTS";

    }
}
