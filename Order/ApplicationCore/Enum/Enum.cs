using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Enum
{
    public enum OrderStat
    {
        OrderPlaced = 1,
        Cancel = 2,
        Complete =3
    }

    public enum PaymentType
    {
        CreditCard = 1,
        PayPal = 2,
        GooglePay = 3,
        ApplePay = 4,
        DebitCard = 5
    }

    public enum ShippingMethod
    {
        StandardShipping = 1,
        ExpressShipping = 2,
        FreeShipping = 3,
        TwoDayShipping = 4,
        InStorePickup = 5,
    }
}
