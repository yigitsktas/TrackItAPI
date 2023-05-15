using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace TrackItAPI.Helpers
{
    public static class IdentityHelper 
    {
        public static int GetMemberID(this IPrincipal user)
        {
            int MyValue = 0;

            if (user != null)
            {
                var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Sid);
                MyValue = claim != null ? Convert.ToInt32(claim.Value) : 0;
            }

            return MyValue;
        }
        public static string GetNameSurname(this IPrincipal user)
        {
            string MyValue = "";

            if (user != null)
            {
                var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Name);
                MyValue = claim != null ? claim.Value : "";
            }

            return MyValue;
        }
        public static string GetGUID(this IPrincipal user)
        {
            string MyValue = "";

            if (user != null)
            {
                var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier);
                MyValue = claim != null ? claim.Value : "";
            }

            return MyValue;
        }
        public static int GetItemType(this IPrincipal user)
        {
            int MyValue = 1201;

            if (user != null)
            {
                var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.PrimarySid);
                MyValue = claim != null ? Convert.ToInt32(claim.Value) : 0;
            }

            return MyValue;
        }
        public static int GetItemID(this IPrincipal user)
        {
            int MyValue = 0;

            if (user != null)
            {
                var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.GroupSid);
                MyValue = claim != null ? Convert.ToInt32(claim.Value) : 0;
            }

            return MyValue;
        }
        public static int GetMemberTypeID(this IPrincipal user)
        {
            int MyValue = 0;

            if (user != null)
            {
                var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Role);
                MyValue = claim != null ? Convert.ToInt32(claim.Value) : 0;
            }

            return MyValue;
        }
    }
}
