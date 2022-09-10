using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    //Test amaçlı token oluşturmak veya başka teknikle token oluşturulması durumu için interface
    public interface ITokenHelper
    {
        //Kullanıcı ve adı şifre doğru ise, veri tabanından claim'lerini bulacak
        //Orada JWT üretecek ve kullanıcıya verecek
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
