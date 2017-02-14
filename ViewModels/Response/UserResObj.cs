using CashFlow.Models;
namespace CashFlow.ViewModels{
    public class UserResObj:BaseViewModel{
        public int ID { get; set; }
		public string phone { get; set; }
		public string email { get; set; }
        public string name { get; set; }
        public int emailVerified { get; set; }
		public string timestamp { get; set; }
		public BaseResponse response { get; set; }

		public UserResObj()
		{
			response = new BaseResponse();
		}

		public override void fillObject(object entity)
		{

			var obj = (Users)entity;

			ID = obj.id;
			emailVerified = (int)obj.EmailVerified;
			phone = obj.phone;
			email = obj.email;
			timestamp = obj.CreatedAt.ToString();
            name = obj.name;
			//imageURL = Encoding.UTF8.GetString(obj.ProfileImage);
			
          
		}
    }
}