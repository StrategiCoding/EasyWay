namespace Payments.Domain.Payments
{
    public sealed class Payment : AggregateRoot
    {
        public decimal Amount { get; private set; }

        public string Currency { get; private set; }

        public PaymentStatus Status { get; private set; }

        private Payment(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
            Status = PaymentStatus.Initialized;
        }

        public static Payment Create(decimal amount, string currency) 
        {
            return new Payment(amount, currency);
        }

        public void Confirm()
        {
            Status = PaymentStatus.Success;
        }
    }
}
