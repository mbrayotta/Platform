using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Domain.Validators
{
    public class OperationValidator : AbstractValidator<Operation>
    {
        public OperationValidator() 
        {
            RuleFor(x => x.Type).Equal("sumar", StringComparer.Ordinal).WithMessage("The property 'Type' isnt equal to 'sumar'");
            RuleFor(x => x.FirstArgument).NotEmpty().WithMessage("First argument can not be empty");
            RuleFor(x => x.SecondArgument).NotEmpty().WithMessage("Second argument can not be empty"); ;
        }

    }
}
