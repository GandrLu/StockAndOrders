using FluentValidation;
using ShopManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManager.Validator
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.Firstname).NotEmpty().Length(2, 100);
            RuleFor(customer => customer.Surname).NotEmpty().Length(2, 100);
            RuleFor(customer => customer.Address.Street).NotEmpty().Length(2, 100);
            RuleFor(customer => customer.Address.Housenumber).NotEmpty().Length(2, 50);
            RuleFor(customer => customer.Address.City).NotEmpty().Length(2, 50);
        }

        //protected bool BeNumber(string number)
        //{
        //    return number.All(Char.IsNumber);
        //}
    }
}
