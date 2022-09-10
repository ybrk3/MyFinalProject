using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{   //Erişim anahtarı
    public class AccessToken
    {
        public string Token { get; set; } //Kullanıcıya verilen token
        public DateTime Expiration { get; set; } //Verilen token'ın bitiş tarihi
    }
}
