using System;
using System.Collections.Generic;
using System.Text;

namespace RS {
    public interface SMSCConnection {
        void login();
        void logout();
        bool isConnected();

        /**
         * The function uses the connection to the server
         * to send the object <code>SMS</code>.
         * Returns an object <code>SendResult</code> with
         * information about the sending result.
         */
        SendResult sendSMS(SMS sms);

        /**
         * The function uses the connection to the server
         * to obtain the array of <code>MessageStatus</code> objects
         * relative to the specified ID message passed as argument.
         * Each <code>MessageStatus</code> object provides
         * for a single recipient.
         */
        List<MessageStatus> getMessageStatus(String order_id);

        /**
         * The function asks the server if the number passed
         * as argument is a valid recipient.
         */
        LookupResult lookup(SMSRecipient recipient);

        /**
         * The function uses the connection to the server
         * to retrieve the informations relative to the credits.
         * With this informations builds an array of 
         * objects <code>Credit</code>, one for each
         * different type of credit.
         */
        List<Credit> getCredits();

        /**
         * The function uses the connection to the server
         * to retrieve the informations relative to the Mobile Originated SMSs.
         */
        List<SMS_MO> getNewSMS_MOs();

        /**
         * The function uses the connection to the server
         * to retrieve the informations relative to the Mobile Originated SMSs.
         * received in the gap between the two dates passed as arguments.
         */
        List<SMS_MO> getSMS_MOHistory(DateTime from_date, DateTime to_date);

        List<SMS_MO> getSMS_MOById(long id_message);

        /**
         * The function uses the connection to the server to
         * retrieve the array of <code>SMS</code> executed in the 
         * gap between the two dates passed as arguments.
         */
        List<SentSMS> getSMSHistory(DateTime from_date, DateTime to_date);

        /**
         * Create a subaccount; no client-side check for parameter validity
         * is done, instead, server interface will try to "adjust" the passed
         * parameters, otherwise an error is sent to the client and, consequently,
         * an exception is thrown.
         */
        Subaccount createSubaccount(Subaccount subaccount);

        /**
         * Requests to the server the list of the subaccounts configured for
         * the logged-in superaccount.
         */
        List<Subaccount> getSubaccounts();

        /**
         * Locks the subaccount; the subaccount can't login anymore, but
         * no data is deleted.
         */
        Subaccount lockSubaccount(Subaccount subaccount);

        /**
         * Unlocks a previously locked subaccount.
         */
        Subaccount unlockSubaccount(Subaccount subaccount);

        /**
         * Move credits from the superaccount to the subaccount or the opposite,
         * depending on the value of the "super_to_sub" field of the CreditMovement
         * object.
         */
        void moveCredits(CreditMovement credit_movement);
        /**
         * The function retrieves the array of <code>Credit</code> of the specified subaccount
         */
        List<Credit> getSubaccountCredits(Subaccount subaccount);
        /**
         * The function creates a new purchase for the specificed subaccount
         */
        void createPurchase(CreditMovement credit_movement);
        /**
         * The function clears a purchase of the specificed subaccount
         */
        void deletePurchase(CreditMovement credit_movement);
        /**
         * The function retrieves the array of <code>CreditMovements</code> of the specified subaccount
         */
        List<CreditMovement> getPurchases(Subaccount subaccount);

    }
}
