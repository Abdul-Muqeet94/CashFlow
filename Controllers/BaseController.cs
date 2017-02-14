using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Controllers{
    public class BaseController:Controller{
        protected readonly FlowContext _db;
        public BaseController (FlowContext context)
        {
            _db=context;
        }
    }
}