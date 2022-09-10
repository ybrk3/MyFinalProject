using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    //Şifreleme olan sistemlerde, herşeyin byte[] formatın oluşturmamız gerekiyor!
    //ASPNet JWT servislerinin anlayacağı hale getirmemiz gerekiyor, byte[] formatı haline dönüştürür
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)  //appsettingjson'daki securityKey'nin SecurityKey
                                                                         // karşılığını verir
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)); //byte[] formatına dönüştürür
        }
    }
}
