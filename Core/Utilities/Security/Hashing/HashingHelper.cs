using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //out dışarıya parametreyi verir

        //Verilen kullanıcının password'unu salt ve hash değerini oluşturuyor
        public static void CreatePasswordHash
            (string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //disposable pattern
            using (var hmac = new System.Security.Cryptography.HMACSHA512())//her kullanıcı için key oluşturur
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//byte[] olması gerekir o yüzden GetBytes()
            }
        }

        //Kullanıcı password'u aynı algoritmayla hashleseydin
        //-verilen passwordHash'i karşılarmıydın
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) //key parametresi için passwordSalt verilir
            {
                //computedHash hesaplanan Hash
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
