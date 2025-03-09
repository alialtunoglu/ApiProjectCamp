using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProjeKampi.WebApi.Entities;
using FluentValidation;

namespace ApiProjeKampi.WebApi.ValidationRules
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x=>x.ProductName).NotEmpty().WithMessage("Ürün adı boş geçilemez");
            RuleFor(x=>x.ProductName).MinimumLength(2).WithMessage("Ürün adı en az 2 karakter olmalıdır");
            RuleFor(x=>x.ProductName).MaximumLength(50).WithMessage("Ürün adı en fazla 50 karakter olmalıdır");
            RuleFor(x=>x.Price).NotEmpty().WithMessage("Ürün fiyatı boş geçilemez").
                GreaterThan(0).WithMessage("Ürün fiyatı 0'dan büyük olmalıdır").
                LessThan(1000).WithMessage("Ürün fiyatı 1000'den küçük olmalıdır");

            RuleFor(x=>x.ProductDescription).NotEmpty().WithMessage("Ürün açıklaması boş geçilemez");


        }
    }
}