using EasyWay;
using Payments.Domain.Payments;

namespace Payments.Infrastructure.Payments
{
    internal sealed class PaymentRepository : IPaymentRepository
    {
        private readonly IGenericRepository<Payment> _genericRepository;

        public PaymentRepository(IGenericRepository<Payment> genericRepository) 
        {
            _genericRepository = genericRepository;
        }

        public Task Add(Payment payment)
        {
            return _genericRepository.Add(payment);
        }

        public Task<Payment?> Get(Guid id)
        {
            return _genericRepository.Get(id);
        }
    }
}
