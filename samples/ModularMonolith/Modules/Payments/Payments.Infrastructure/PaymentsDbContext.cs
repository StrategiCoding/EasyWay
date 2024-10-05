using Microsoft.EntityFrameworkCore;
using Payments.Domain.Payments;

namespace Payments.Infrastructure
{
    public sealed class PaymentsDbContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }
    }
}
