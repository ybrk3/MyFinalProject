using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    //base => Result.cs
    //true/false parametresi vermeden Business'da message girilmesi üzerine çalışacak. true burada oluşur.
    public class SuccessResult : Result
    {
       public SuccessResult (string message):base(true, message)
        {

        }
        //mesaj verilmek istenmezse
        public SuccessResult() : base(true)
        {

        }
    }
}
