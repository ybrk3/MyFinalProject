using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    //Validasyonların yazıldığı class ("Fluent Validation")
    //Package ==> Fluent Validation package
    //AbstractValidator<> Fluent Validation'dan geliyor
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            //Rules ctor içerisine yazılır
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
        //------------------------------------------
            //Mevcutta olmayan kural için;;
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı");
        }
        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
            //"A" ile başlıyorsa true döner. Başlamıyorsa false döner
            //arg===> p.ProductName
        }

        //-----------------------------------------
    }
}
