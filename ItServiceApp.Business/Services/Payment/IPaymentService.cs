using ItServiceApp.Core.Payment;
using System.Collections.Generic;

namespace ItServiceApp.Business.Services.Payment
{
    public interface IPaymentService
    {
        
            public InstallmentModel CheckInstallments(string binNumber, decimal price);
            public PaymentResponseModel Pay(PaymentModel model);
       
    }
}
