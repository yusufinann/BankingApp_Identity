using EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.BusinessLayer.ValidationRules.AppUserValidationRules
{
    public class AppUserRegisterValidator:AbstractValidator<AppUserRegisterDto>
    {
        public AppUserRegisterValidator()
        {
                RuleFor(x=>x.Name).NotEmpty().WithMessage("Ad alanı boş geçielemez");  //Boş geçilemez
            RuleFor(x => x.Username).NotEmpty().WithMessage("Soyad alanı boş geçielemez");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail alanı boş geçielemez");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş geçielemez");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre tekrar alanı boş geçielemez");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("Lütfen en fazla 30 karakter girişi yapınız.");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Lütfen en az 2 karakter veri girişi yapın");
            RuleFor(x => x.ConfirmPassword).Equal(y => y.Password).WithMessage("Parolalarınız eşleşmiyor");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen geçerli bir mail adresi giriniz");
        }
    }
} 
