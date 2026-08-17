using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Enums.User
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
