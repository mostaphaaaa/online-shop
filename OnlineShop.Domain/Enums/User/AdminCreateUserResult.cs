using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Enums.User
{
    public enum AdminCreateUserResult
    {
        Success,
        Error,
        EmailDuplicated,
        UserNameDuplicated,
        MobileDuplicated,
        InvalidImage
    }
}
