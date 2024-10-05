namespace Payments.Domain.Payments
{
    public interface IPaymentRepository
    {
        Task<Payment?> Get(Guid id);
        Task Add(Payment payment);
    }
}
