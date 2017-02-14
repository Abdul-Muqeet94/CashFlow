namespace CashFlow{
    public class Constant{

        public const int USER_ACTIVE = 1;
        public const int USER_INACTIVE = 0;
        public const int USER_REACTIVE = 2;


        public const string INSERT_USER_EMAIL_SUBJECT = "Verification of email for CashFlow";
        public const string INSERT_USER_EMAIL_BODY = "Thank you for signing up with CashFlow.\nPlease verify your email address with this code: ";

        public const string REACTIVATION_EMAIL_SUBJECT= "Verification of reactivation of CashFlow account";
        public const string REACTIVATION_EMAIL_BODY= "Thank you for reactivating your CashFlow account.\nPlease verify your account with this Code : ";


        public const string FORGET_PASSWORD_EMAIL_SUBJECT = "CashFlow App - Confirm password reset";
        public const string FORGET_PASSWORD_EMAIL_BODY = "Verify your password reset with this code: ";


        public const string VERIFY_PIN_EMAIL_SUBJECT = "Password change notification from CashFlow";
        public const string VERIFY_PIN_EMAIL_BODY = "Your password was successfully changed.";


        public const string CHANGE_PASSWORD_EMAIL_SUBJECT = "Password change notification from CashFlow.";
        public const string CHANGE_PASSWORD_EMAIL_BODY = "Your password was successfully changed.";


        // sendMail module
        public const string MAIL_FROM_ADDRESS_NAME="CashFlow";
        public const string MAIL_FROM_ADDRESS_EMAIL="abdulmuqeet@ngenc.com";
        public const string MAIL_HOST_ADDRESS = "smtpout.secureserver.net";
        public const int MAIL_HOST_PORT = 80;

        public const string MAIL_AUTHENTICAITON_USER = "abdulmuqeet@ngenc.com";
        public const string MAIL_AUTHENTICATION_PASSWORD = "abdul@ngenc123!";








        // SEND GRID email
        public const string SENDGRID_API_KEY = "";
        public const string SENDGRID_API_TOKEN = "";

}
}
