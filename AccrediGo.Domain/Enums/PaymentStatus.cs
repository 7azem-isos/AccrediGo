using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccrediGo.Domain.Enums
{
    public enum PaymentStatus
    {
        Paid,
        Pending,
        Failed,
        Refunded,
        Cancelled
    }
}
