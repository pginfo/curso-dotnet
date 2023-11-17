using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.Payments.API.Models
{
    public class PaymentInfoInputModel
    {
        public int IdProject { get; set; }

        public string CreditCardNumber { get; set; }

        public string Cvv { get; set; }

        public string ExpiresAt { get; set; }

        public string FullName { get; set; }

        [Column(TypeName="decimal(18,2)")]
        public decimal Amount { get; set; }
    }
}
