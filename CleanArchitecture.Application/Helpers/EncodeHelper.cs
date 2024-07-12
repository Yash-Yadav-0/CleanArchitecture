using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace CleanArchitecture.Application.Helpers
{
    public static class EncoderHelper
    {
        public static string UrlEncoder(this string value)
        {
            byte[] data = Encoding.UTF8.GetBytes(value);
            return WebEncoders.Base64UrlEncode(data);
        }
        public static string UrlDecoder(this string value)
        {
            var data = WebEncoders.Base64UrlDecode(value);
            return Encoding.UTF8.GetString(data);
        }
    }
}
