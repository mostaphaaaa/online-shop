using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Enums
{
    public enum RegisterUserResult
    {
        Success,
        InvalidInputs,
        EmailDuplicated,
        UserNameDuplicated,
        SendActivationEmail,
        Failed
    }
}
