using System;
using CashFlow.ViewModels;
using System.Linq;
using CashFlow.Models;
namespace CashFlow.BLL{
    public class BLL_Users{
        private readonly FlowContext _db;
        public BLL_Users(FlowContext context){
            _db=context;
        }
        public BaseResponse addUser(RegisterUserReqObj user){
            string token = "";
			BaseResponse response = new BaseResponse();
            try
            {

                var db = _db;
                
                var isRegistered = db.users.Where(c => c.email.Equals(user.email));

                


                if (isRegistered.Count() == 0)
                {
                    Random rnd = new Random();
                    token = rnd.Next(100000, 999999).ToString();
                    var insertUserObj = new Users()
                    {
                        email = user.email,
                        phone = user.phone,
                        password = Utils.Utils.CreateMD5(user.password),
                        TokenActivate = token,
                        EmailVerified = 0,
                        status = Constant.USER_ACTIVE,
                        CreatedAt = DateTime.Today,
                        PushId = "No Push",
                        name = user.name,
                        // the user.profileImage is a base64, we can't insert it into the db
                        // its a huge string..... its bad! 
                        //ProfileImage = Encoding.UTF8.GetBytes("")
                    };

                    Console.WriteLine("I'm here");

                    db.users.Add(insertUserObj);
                    response.status = db.SaveChanges();


                    if (response.status == 1)
                    {
                        string subject = CashFlow.Constant.INSERT_USER_EMAIL_SUBJECT;
                        string body = CashFlow.Constant.INSERT_USER_EMAIL_BODY + token;

                        if (Utils.Utils.sendEmail(subject, body, user.email))
                        {
                            response.status = 1;
                            response.developerMessage = "Account Created, Check Your Email";

                        }
                        else
                        {
                            response.status = 2;
                            response.developerMessage = "Couldn't Send Verification Email, Try Again Later!";

                        }
                    }

                    else
                    {
                        response.status = -2;
                        response.developerMessage = "Couldn't Register, Try Again Later!";
                    }
                }
                else
                {
                    var registeredUser = isRegistered.SingleOrDefault();

                    if (registeredUser.status == Constant.USER_ACTIVE)
                    {
                        response.status = -1;
                        response.developerMessage = "Email Already Exist";
                    }
                    else
                    {
                        Random rnd = new Random();
                        token = rnd.Next(100000, 999999).ToString();

                        registeredUser.phone = user.phone;
                        registeredUser.password = Utils.Utils.CreateMD5(user.password);
                        registeredUser.status = Constant.USER_REACTIVE;
                        registeredUser.TokenActivate = token;
                        registeredUser.EmailVerified = 0;
                        db.SaveChanges();

                        string subject = CashFlow.Constant.REACTIVATION_EMAIL_SUBJECT;
                        string body = CashFlow.Constant.REACTIVATION_EMAIL_BODY + token;

                        if (Utils.Utils.sendEmail(subject, body, user.email))
                        {
                            response.status = 1;
                            response.developerMessage = "Account Reactivated, Check Your Email";

                        }
                        else
                        {
                            response.status = 2;
                            response.developerMessage = "Couldn't Send Verification Email, Try Again Later!";

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                response.developerMessage = ex.Message;
                response.status = 3;
            }
			return response;

        }
        public UserResObj loginUser(string email, string password, string pushID)
		{
			UserResObj userLogin = new UserResObj();
			userLogin.response = new BaseResponse();

			string encryptedPassword = Utils.Utils.CreateMD5(password);
            try
            {
                var db = _db;

                var temp = db.users.Where(u => u.email.Equals(email) && u.password.Equals(encryptedPassword))
                            .FirstOrDefault();

                if (temp != null)
                {

                    var user = temp;

                    if (user.status == Constant.USER_INACTIVE)
                    {
                        userLogin.response.status = 3;
                        userLogin.response.developerMessage = "Account Inactive";
                        return userLogin;
                    }

                    int isEmailVerified = (int)user.EmailVerified;
                    if (isEmailVerified == 1)
                    {
                        if (string.IsNullOrEmpty(pushID))
                        {
                            pushID = "pushID";
                        }
                        user.PushId = pushID;
                        db.SaveChanges();

                        userLogin.response.status = 1;
                        userLogin.response.developerMessage = "Login Successfull";

                        userLogin.fillObject(user);
                        return userLogin;
                    }
                    else
                    {
                        userLogin.response.status = 2;
                        userLogin.response.developerMessage = "Email not verified.";
                        return userLogin;
                    }
                }


                userLogin.response.status = -1;
                userLogin.response.developerMessage = "Incorrect Credentials";
                return userLogin;
            }
            catch (Exception e)
            {

                userLogin.response.status = 0;
                userLogin.response.developerMessage = "Something Went Wrong: "+ e.Message;
                return userLogin;
            }
		}

        public UserResObj validateUser(string email, string token, string pushID)
		{
			UserResObj userLogin = new UserResObj();
			userLogin.response = new BaseResponse();

            try
            {

                var db = _db;

                var temp = db.users.Where(u => u.email.Equals(email) && u.TokenActivate.Equals(token));


                if (temp.Count() > 0)
                {
                    if (string.IsNullOrEmpty(pushID))
                        pushID = "pushID";

                    var userObj = temp.SingleOrDefault();
                    userObj.EmailVerified = 1;
                    userObj.status = Constant.USER_ACTIVE;
                    userObj.PushId = pushID;
                    db.SaveChanges();
                    userLogin.response.status = 1;
                    userLogin.response.developerMessage = "Verification Successfull";

                    userLogin.fillObject(userObj);
                }

                else
                {
                    userLogin.response.status = -1;
                    userLogin.response.developerMessage = "Invalid Pin";
                }
            }
            catch (Exception e) {
                userLogin.response.status = -1;
                userLogin.response.developerMessage = "Something went wrong: " + e.Message;
                
            }

			return userLogin;

		}
        public BaseResponse forgetPasswordEmail(string emailAddress)
        {
            BaseResponse res = new BaseResponse();

			Random rnd = new Random();
			int randomNumber = rnd.Next(100000, 999999);
			try
			{
                var db = _db;

                var user = db.users.Where(b => b.email == emailAddress && b.status == 1);

                if (user != null && user.Count() > 0)
                {
                    var obj = user.Single();
                    obj.TokenForgetpw = randomNumber.ToString();
                    res.status = db.SaveChanges();

                    string subject = CashFlow.Constant.FORGET_PASSWORD_EMAIL_SUBJECT;
                    string body = CashFlow.Constant.FORGET_PASSWORD_EMAIL_BODY + randomNumber;

                    if (Utils.Utils.sendEmail(subject, body, emailAddress))
                    {
                        res.status = 1;
                        res.developerMessage = "Email sent with instructions";

                    }
                    else
                    {
                        res.status = 0;
                        res.developerMessage = "Couldn't Send Instruction Email, Try Again Later!";

                    }

                }
                else
                {
                    res.status = 2;
                    res.developerMessage = "No such account exist";
                }
			}
			catch (Exception e)
			{
				res.status = -1;
				res.developerMessage = "Something went wrong: " + e.Message;
			}

			return res;
        }
        public BaseResponse cancelAccount(int userID)

         {
              BaseResponse response = new BaseResponse();

            try
            {
                var db = _db;
                var user = db.users.Where(u => u.id == userID).Single();
                user.status = Constant.USER_INACTIVE;

                db.SaveChanges();

                response.status = 1;
                response.developerMessage = "Account has been cancelled";
            }
            catch (Exception e)
            {

                response.status = -1;
                response.developerMessage = "Something went wrong: " + e.Message;
                return response;

            }

            return response;

        }
        public BaseResponse verifyPin(VerifyPinReqObj verPin)
		{
			BaseResponse res = new BaseResponse();

            try
            {
                var db = _db;

                var obj = db.users.SingleOrDefault(b => b.email == verPin.email);

                if (obj != null)
                {

                    if (obj.TokenForgetpw.Equals(verPin.token))
                    {

                        obj.password = Utils.Utils.CreateMD5(verPin.password);
                        res.status = db.SaveChanges();

                        string subject = CashFlow.Constant.VERIFY_PIN_EMAIL_SUBJECT;
                        string body = CashFlow.Constant.VERIFY_PIN_EMAIL_BODY;
                        string address = obj.email;

                        if (Utils.Utils.sendEmail(subject, body, address))
                        {

                            res.status = 1;
                            res.developerMessage = "Password changed successfully.";
                        }
                        else
                        {
                            res.status = -2;
                            res.developerMessage = "Failed to send email. SMTP Server Down.";
                        }

                    }
                    else
                    {
                        res.status = 0;
                        res.developerMessage = "Wrong token provided.";
                    }
                }
                else
                {
                    res.developerMessage = "Token is not set in DB OR Email is incorrect.";
                    res.status = 2;
                }
            }
            catch (Exception e)
            {
                res.developerMessage = "Something went wrong: " + e.Message;
                res.status = -1;
            }
			return res;
		}
        public BaseResponse changePassword(ChangePassword updatePassword)
		{

            if (updatePassword.password.Equals(updatePassword.newPassword)) {

                return new BaseResponse
                {
                    status = -4,
                    developerMessage = "Current password is same as old password"
                };
            }


            BaseResponse response = new BaseResponse();

            try
            {
                var db = _db;

                string encryptedPassword = Utils.Utils.CreateMD5(updatePassword.password);
                var obj = db.users.SingleOrDefault(b => b.id == updatePassword.userID && b.password == encryptedPassword);
                if (obj != null)
                {
                    obj.password = Utils.Utils.CreateMD5(updatePassword.newPassword);
                    db.SaveChanges();
                    response.status = 1;
                }

                if (response.status == 1)
                {

                    string subject = CashFlow.Constant.CHANGE_PASSWORD_EMAIL_SUBJECT;
                    string body = CashFlow.Constant.CHANGE_PASSWORD_EMAIL_BODY;
                    string address = obj.email;

                    if (Utils.Utils.sendEmail(subject, body, address))
                    {
                        response.status = 1;
                        response.developerMessage = "Password changed successfully";
                    }
                    else
                    {
                        response.status = -2;
                        response.developerMessage = "Failed to send email; SMTP Server Down";

                    }

                }

                else
                {
                    return new BaseResponse
                    {
                        status = -3,
                        developerMessage = "Current Password is not Valid"
                    };
                }
            }
            catch (Exception e)
            {
                return new BaseResponse
                {
                    status = -1,
                    developerMessage = "Something went wrong:" + e.Message
                };
            }

			return response;
		}
        public BaseResponse updateUser(UpdateUserReqObj updatedUserObj)
		{
			BaseResponse res = new BaseResponse();

            try
            {
                var db = _db;

                var obj = db.users.SingleOrDefault(b => b.id == updatedUserObj.userID);
                if (obj != null)
                {
                    obj.phone = updatedUserObj.phone;
                    obj.email = updatedUserObj.email;
                    res.status = db.SaveChanges();
                }
                if (res.status == 1)
                {
                    res.developerMessage = res.developerMessage + "Profile Updated Successfully";
                }
                else
                {
                    res.status = -2;
                    res.developerMessage = res.developerMessage + "Profile Didn't Update, Try Again Later";
                }
            }
            catch (Exception e)
            {
                res.status = -1;
                res.developerMessage = res.developerMessage + "Something went wrong: " + e.Message;
            }


			return res;
		}




    }
}