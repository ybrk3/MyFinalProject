using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    //WebAPI'de JWT'nin kullanılabilmesi için 
    //Credentials : Giriş bilgileri (Kullanıcı adı, şifre) SecurityKey formatında verilir.
    //ASP Net'inde gelen JWT'yi doğrulaması için hangi algoritma kullanılacağı bildirilir.
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            //Hashing işlemi için aşağıdaki securityKey'i
            //Şifreleme olarak da alttaki ikinci parametreyi kullan
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            //ASPNet için hangi anahtar ve hangi algoritma kullanılacak o bildirilir 
        }
    }
}
