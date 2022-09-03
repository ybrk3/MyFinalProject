using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    //Autofac module'ü !!
    public class AutofacBusinessModule : Module
    {
        //uygulama yayınlandığı zaman Load çalışacak
        protected override void Load(ContainerBuilder builder)
        {
            //IProductService verildiğinde ProductManager'ı çalıştır;;
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
            //SingleInstance ile sadece bir kere örnek oluşturulur ve uygulama kullanılırken her kullanıcı tarafından fazla instance olması engellenir


            //Çalışan uygulama içerisinde implemente edilmiş interface'leri bulur ve onlar için AspectInterceptorSelector() çağırır.

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
        }
    }
}
