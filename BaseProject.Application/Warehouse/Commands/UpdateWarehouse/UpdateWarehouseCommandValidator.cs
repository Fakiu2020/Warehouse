
using FluentValidation;
using BaseProject.Application.Infrastructure.Request.Commands.Update;
using BaseProject.Application.Users.Commands.UpdateUser;

namespace BaseProject.Application.Warehouse.Commands.UpdateWarehouse
{

    public class UpdateWarehouseCommandValidator : UpdateCommandValidator<UpdateWarehouseCommand>
    {
        public UpdateWarehouseCommandValidator() 
        {
            RuleFor(v => v.Name).NotEmpty();
            RuleFor(v => v.Address).NotEmpty();
            RuleFor(v => v.Code).NotEmpty();
            RuleFor(v => v.State).NotEmpty();

        }
    }
}
