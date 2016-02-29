// // Breeze Project
// // Created by Bartosz Rachwal. 
// // Copyright (c) 2016 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved.

using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Breeze.Properties;

namespace Breeze.Extensions
{
    public static class ClaimsExtensions
    {
        static string GetUserEmail(this ClaimsIdentity identity)
        {
            return
                identity.Claims?.FirstOrDefault(c => c.Type == Resources.ClaimsExtensions_UserEmail)?.Value;
        }

        public static string GetUserEmail(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            return claimsIdentity != null ? GetUserEmail(claimsIdentity) : "";
        }

        static string GetUserNameIdentifier(this ClaimsIdentity identity)
        {
            return
                identity.Claims?.FirstOrDefault(c => c.Type == Resources.ClaimsExtensions_UserNameIdentifier)
                    ?.Value;
        }

        public static string GetUserNameIdentifier(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            return claimsIdentity != null ? GetUserNameIdentifier(claimsIdentity) : "";
        }

        public static string GetProfileId(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var value = claimsIdentity?.Claims.FirstOrDefault(c => c.Type == Resources.ClaimsExtensions_GetProfileId_google_profile)?.Value;
            var id = value?.Substring(value.IndexOf('+'));
            return id;
        }
    }
}