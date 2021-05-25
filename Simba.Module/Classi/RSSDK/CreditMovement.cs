using System;

namespace RS {
    public class CreditMovement {
        private String subaccount_login;
        private Boolean super_to_sub;
        private int amount;
        private SMSType sms_type;
        private SMSType[] sms_types;
        private double price;
        private double[] pricePerMessage;
        private Boolean is_donation = false;
        private long id_purchase;
        private DateTime recording_date;
        private int availableAmount;

        public String Subaccount_login
        {
            get { return this.subaccount_login; }
            set { this.subaccount_login = value; }
        }
        public Boolean Super_to_sub
        {
            get { return this.super_to_sub; }
            set { this.super_to_sub = value; }
        }
        public int Amount
        {
            get { return this.amount; }
            set { this.amount = value; }
        }
        public SMSType Sms_type
        {
            get { return this.sms_type; }
            set { this.sms_type = value; }
        }
        public SMSType[] Sms_types
        {
            get { return this.sms_types; }
            set { this.sms_types = value; }
        }
        public double Price
        {
            get { return this.price; }
            set { this.price = value; }
        }
        public double[] PricePerMessage
        {
            get { return this.pricePerMessage; }
            set { this.pricePerMessage = value; }
        }
        public Boolean Is_donation
        {
            get { return this.is_donation; }
            set { this.is_donation = value; }
        }
        public long Id_purchase
        {
            get { return this.id_purchase; }
            set { this.id_purchase = value; }
        }
        public DateTime Recording_date
        {
            get { return this.recording_date; }
            set { this.recording_date = value; }
        }
        public int AvailableAmount
        {
            get { return this.availableAmount; }
            set { this.availableAmount = value; }
        }

    }
}
