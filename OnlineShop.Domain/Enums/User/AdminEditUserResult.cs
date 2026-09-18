using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.Enums.User
{
    public enum AdminEditUserResult
    {
        Success,
        Error,
        EmailDuplicated,
        UserNameDuplicated,
        MobileDuplicated,
        InvalidImage
    }
}
