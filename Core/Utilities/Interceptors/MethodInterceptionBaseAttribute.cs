using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    //AOP = methodlar loglamak isteniyor; uygulamanın method başında, sonunda, hata verdiğinde
    //çalışması isteniyorsa AOP kullanılır. Ve bu yönteme Interceptor denilir.
    //Attribute amacı, method çağırıldığında method'un üzerine bakar.(priority'e göre)

    //IInterceptor yapısı Autofac DynamicProxy (Autofac interceptor özelliği) üzerinden gelir o yüzden
    //Autofac.extra.DynamicProxy vb. eklenir


    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
