using simApi.Base;
using simApi.Data;
using simApi.Schema;

namespace simApi.Service;

public interface IPaymentService:IBaseService<Payment,PaymentRequest,PaymentResponse>
{
    bool MakePayment(PaymentRequest request);
}
