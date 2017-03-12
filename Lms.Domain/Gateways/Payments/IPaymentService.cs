using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Gateways.Payments
{
    public interface IPaymentService
    {
        void Charge(int amount, string currency, string description, string cardNumber, string expireYear, string expireMonth, string cvv2);
        void ChargePlan(string companyId, string planId, string cardNumber, string expireYear, string expireMonth, string cvv2);
    }
}
