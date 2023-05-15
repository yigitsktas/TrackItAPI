using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TrackItAPI.Helpers
{
    public static class CookieHelper
    {
        public static void SetCookie(this IResponseCookies cookie, string key, string value, int days)
        {
            CookieOptions options = new()
            {
                Expires = DateTime.Now.AddDays(days)
            };

            cookie.Append(key, value, options);

        }

        public static string GetCookie(this IRequestCookieCollection cookie, string key)
        {
            string MyResult = "";

            MyResult = cookie[key] ?? "";

            return MyResult;
        }

        public static void DeleteCookie(this IResponseCookies cookie, string key)
        {
            cookie.Delete(key);
        }
    }
}
