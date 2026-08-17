using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace ShopStock.Application.Extensions
{
    public static class IdentityExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claims)
        {
            if (claims != null)
            {
                var data = claims.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (data != null)
                {
                    return int.Parse(data.Value);
                }
            }
            //throw new ArgumentException("User ID not found");
            return default(int);
        }
        public static int GetUserId(this IPrincipal principal)
           => (principal is ClaimsPrincipal claims) ? GetUserId(claims) : default;

        #region another Identity Extensions
        public static string GetFullName(this ClaimsPrincipal user)
        {
            string fullName = user.FindFirst("FullName").Value;
            return fullName;
        }
        public static string GetMobile(this ClaimsPrincipal user)
        {
            string mobile = user.FindFirst("Mobile").Value;
            return mobile;
        }
        public static string GetAvatar(this ClaimsPrincipal user)
        {
            string avatar = user.FindFirst("Avatar").Value;
            return avatar;
        }
        public static string GetEmail(this ClaimsPrincipal user)
        {
            string email = user.FindFirst("Email").Value;
            return email;
        }
        #endregion

        #region MyidentityExtensions
        public static string GetUserName(this ClaimsPrincipal claims)
        {
            var data = claims.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name);
            return data.ToString();
        }

        public static string GetUserFullName(this ClaimsPrincipal claims)
        {
            if (claims != null)
            {
                var data = claims.Claims.SingleOrDefault(c => c.Type == "FullName");
                if (data != null && !string.IsNullOrEmpty(data.Value))
                {
                    return data.Value;
                }
            }
            throw new ArgumentException("User FullName not found");
        }
        public static string GetUserMobile(this ClaimsPrincipal claims)
        {
            if (claims != null)
            {
               var data= claims.Claims.SingleOrDefault(c => c.Type == "Mobile");
                if(data!=null && !string.IsNullOrEmpty(data.Value))
                {
                    return data.Value; 
                }
            }
            throw new ArgumentException("User Mobile not found");
        }
        public static string GetUserAvatar(this ClaimsPrincipal claims)
        {
            if (claims != null)
            {
                var data = claims.Claims.SingleOrDefault(c => c.Type == "Avatar");
                if (data != null && !string.IsNullOrEmpty(data.Value))
                {
                    return data.Value;
                }
            }
            return "/Avatars/NoPhoto.jpg";
        }
        #endregion
    }
}
