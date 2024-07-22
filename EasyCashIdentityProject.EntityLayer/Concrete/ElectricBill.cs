using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.EntityLayer.Concrete
{
    public class ElectricBill
    {
        public int ElectricBillID { get; set; }

        public int ContractNumber { get; set; }

        public string CustomerName { get; set; }

        public string BillingPeriod { get; set; }
        public DateTime LastPaymentDueDate { get; set; }
        public decimal Amount { get; set; }
        public bool PaidStatus { get; set; }

    }
}
