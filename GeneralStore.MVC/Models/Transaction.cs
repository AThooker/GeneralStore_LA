using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStore.MVC.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        [Display(Name = "Date/Time Created")]
        public DateTimeOffset TimeCreated
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
        [ForeignKey("Customer")]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer {get; set;}
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
    }
}