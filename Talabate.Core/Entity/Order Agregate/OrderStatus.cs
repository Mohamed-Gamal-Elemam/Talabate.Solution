using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Talabate.Core.Entity.Order_Agregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "PaymentReceived")]
        PaymentRecived,
        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed,
    }
}
