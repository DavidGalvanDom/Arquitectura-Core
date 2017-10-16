using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Charly.Core.Web.TokenProvider
{
    public static class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret = "leoscore?__card_teletechkey!2244")
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
