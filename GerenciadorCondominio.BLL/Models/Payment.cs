using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominio.BLL.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int RentId { get; set; }
        public virtual Rent Rent { get; set; }
        public DateTime? PaymentDate { get; set; }

        public PaymentStatus Status { get; set; }
    }

    public enum PaymentStatus
    {
        Pending, Paid, Late
    }
}
