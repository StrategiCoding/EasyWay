using EasyWay;

namespace Payments.Application.Payments
{
    public sealed class CreatePaymentCommand : Command
    {
        public decimal Amount { get; init; }

        public string Currency { get; init; }
    }
}
