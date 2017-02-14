using System;

namespace CashFlow.Models {
    public class Users:Identity{
        public string name {get;set;}
        public string email {get;set;}
        public string token {get;set;}
        public string password {get;set;}
        public string phone {get;set;}
        public int EmailVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TokenActivate { get; set; }
        public string TokenForgetpw { get; set; }
        public string PushId { get; set; }
    }
}