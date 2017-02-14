using System;
using System.ComponentModel.DataAnnotations;

namespace CashFlow {
    public class Identity {
        [Key]
        public int id {get;set;}
        public int status {get;set;}
    }
}