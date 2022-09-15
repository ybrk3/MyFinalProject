using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IOC
{   //Tüm projelerimizde kullanabileceğimiz injection'lar
    //IserviceCollection injection'ları çözen sınıf. .Net alt yapısına ait
    public interface ICoreModule
    {
        void Load(IServiceCollection serviceCollection);
    }
}
