using EasyWay;
using Payments.Domain.Payments;

namespace Payments.Application.Payments
{
    internal sealed class CreatePaymentCommandHandler : ICommandHandler<CreatePaymentCommand>
    {
        private readonly IPaymentRepository _paymentRepository;

        public CreatePaymentCommandHandler(IPaymentRepository paymentRepository) 
        {
            _paymentRepository = paymentRepository;
        }

        public async Task Handle(CreatePaymentCommand command)
        {
            var payments = Payment.Create(command.Amount, command.Currency);

            await _paymentRepository.Add(payments);
        }
    }
}
