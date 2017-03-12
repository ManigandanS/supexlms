using Lms.Domain.Gateways.Payments;
using Lms.Domain.Models.Companies;
using Lms.Domain.Models.Exceptions;
using Lms.Domain.Repositories;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Infrastructure.Gateways.Payments
{
    public class StripeServiceImpl : IPaymentService
    {
        protected IUnitOfWork unitOfWork;

        public StripeServiceImpl()
        {
        }


        public StripeServiceImpl(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Charge(int amount, string currency, string description, string cardNumber, string expireYear, string expireMonth, string cvv2)
        {
            try
            {
                // setting up the card
                var myCharge = new StripeChargeCreateOptions();

                // always set these properties
                myCharge.Amount = amount;
                myCharge.Currency = currency;

                // set this if you want to
                //myCharge.Description = "Charge it like it's hot";

                myCharge.SourceCard = new SourceCard()
                {
                    Number = cardNumber,
                    ExpirationYear = expireYear,
                    ExpirationMonth = expireMonth,
                    Cvc = cvv2
                };

                // set this property if using a customer
                //myCharge.CustomerId = *customerId*;

                // set this if you have your own application fees (you must have your application configured first within Stripe)
                //myCharge.ApplicationFee = 25;

                // (not required) set this to false if you don't want to capture the charge yet - requires you call capture later
                myCharge.Capture = true;

                var chargeService = new StripeChargeService();
                StripeCharge stripeCharge = chargeService.Create(myCharge);
            }
            catch (Exception ex)
            {
                throw new PaymentException(ex.Message);
            }
        }



        public void ChargePlan(string companyId, string planId, string cardNumber, string expireYear, string expireMonth, string cvv2)
        {
            var company = unitOfWork.CompanyRepository.GetById(companyId);
            var plan = unitOfWork.PlanRepository.GetById(planId);

            try
            {
                Charge((int)(plan.Price * 100), "usd", "", cardNumber, expireYear, expireMonth, cvv2);
                ExtendExpireDate(company);
            }
            catch (PaymentException pex)
            {
                throw pex;
            }
        }

        private void ExtendExpireDate(Company company)
        {
            company.IsTrial = false;
            if (company.Expiry > DateTime.UtcNow)
                company.Expiry = company.Expiry.AddYears(1);
            else
                company.Expiry = DateTime.UtcNow.AddYears(1);

            unitOfWork.CompanyRepository.Update(company);
            unitOfWork.SaveChanges();
        }

    }
}
