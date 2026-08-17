using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Enums.User
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
