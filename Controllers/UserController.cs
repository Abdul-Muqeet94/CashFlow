using Microsoft.AspNetCore.Mvc;
using CashFlow.ViewModels;
namespace CashFlow.Controllers{
    public class UserController:BaseController{
        public UserController(FlowContext context):base(context)
        {

        }

        [Route("api/users/register"), HttpPost]
        public BaseResponse addUser([FromBody] RegisterUserReqObj user)
        {
            return new BLL.BLL_Users(_db).addUser(user);
        }
        
        [Route("api/users/login"), HttpPost]
		[ProducesResponseType(typeof(UserResObj),200)]
		public IActionResult LoginUser([FromBody] LoginReqObj credential)
		{

			return Ok(new BLL.BLL_Users(_db).loginUser(credential.email, credential.password, credential.pushID));

		}

		[Route("api/users/validate"), HttpPost]
		public UserResObj ValidateUser([FromBody] ValidateUser user)
		{
			return new BLL.BLL_Users(_db).validateUser(user.email, user.token,user.pushID);
		}
        [Route("api/users/forgetPassword"), HttpPost]
		public BaseResponse ForgetPasswordEmail([FromBody] ForgetPasswordEmail userEmail)
		{
			return new BLL.BLL_Users(_db).forgetPasswordEmail(userEmail.email);
		}

		[Route("api/users/verifyForgetPasswordPin"), HttpPost]
		public BaseResponse VerifyPin([FromBody] VerifyPinReqObj verifyPin)
		{
			return new BLL.BLL_Users(_db).verifyPin(verifyPin);
		}

		[Route("api/users/changePassword"), HttpPost]
		public BaseResponse ChangePassword([FromBody] ChangePassword updatePassword)
		{
			return new BLL.BLL_Users(_db).changePassword(updatePassword);
		}

		[Route("api/users/updateUser"), HttpPost]
		public BaseResponse UpdateUser([FromBody] UpdateUserReqObj updatedUserObj)
		{
			return new BLL.BLL_Users(_db).updateUser(updatedUserObj);
		}
        
        [Route("api/users/cancelAccount"), HttpPost]
		public BaseResponse CancelAccount([FromBody] CancelAccountReqObj cancelObj)
        {
            return new BLL.BLL_Users(_db).cancelAccount(cancelObj.userID);
        }
    }
}