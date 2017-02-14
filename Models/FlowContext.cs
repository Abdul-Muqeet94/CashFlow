using Microsoft.EntityFrameworkCore;
using CashFlow.Models;
namespace CashFlow {

    public class FlowContext:DbContext{
        public FlowContext(DbContextOptions<FlowContext> context):base(context)
        {

        }
        public DbSet<Users> users {get;set;}
    }
}